using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WKAppBot.CLI;

// MCP (Model Context Protocol) stdio server
// JSON-RPC 2.0 over stdin/stdout -- all logs go to stderr
internal partial class Program
{
    static readonly object McpWriteGate = new();
    static readonly ConcurrentDictionary<string, SemaphoreSlim> McpActionGates = new();

    // -- APSP v1 프로파일링 메트릭 ------------------------------------
    /// <summary>툴 child process 프로파일링 기본 정보. progress 알림에 확장 필드로 포함.</summary>
    record McpProgressMeta(long MemMb, double? CpuPct, int Threads, int Handles);

    /// <summary>AsyncLocal 사이드채널: PushLine -> EmitToolProgress 메트릭 전달 (시그니처 변경 없음).</summary>
    static readonly AsyncLocal<McpProgressMeta?> _currentProgressMeta = new();

    /// <summary>CPU% 델타 계산용 이전 샘플 (PID 키).</summary>
    static readonly ConcurrentDictionary<int, (TimeSpan cpu, DateTime time)> _cpuPrevSamples = new();

    static McpProgressMeta? SampleProcessMetrics(int pid)
    {
        try
        {
            using var p = Process.GetProcessById(pid);
            p.Refresh();
            var memMb   = p.WorkingSet64 / (1024 * 1024);
            var threads = p.Threads.Count;
            var handles = p.HandleCount;
            var cpuNow  = p.TotalProcessorTime;
            var timeNow = DateTime.UtcNow;

            double? cpuPct = null;
            if (_cpuPrevSamples.TryGetValue(pid, out var prev))
            {
                var cpuDelta  = (cpuNow - prev.cpu).TotalMilliseconds;
                var timeDelta = (timeNow - prev.time).TotalMilliseconds;
                if (timeDelta > 50)
                    cpuPct = Math.Round(cpuDelta / timeDelta / Environment.ProcessorCount * 100, 1);
            }
            _cpuPrevSamples[pid] = (cpuNow, timeNow);
            return new McpProgressMeta(memMb, cpuPct, threads, handles);
        }
        catch { return null; }
    }

    /// <summary>
    /// Launcher mode: all tool calls spawn fresh subprocess (wkappbot.exe on disk).
    /// Enabled by --launcher flag. MCP connection survives hot-swap of core binary.
    /// </summary>
    static bool McpLauncherMode = false;

    /// <summary>CWD detected from parent VS Code/Claude Desktop at MCP startup. Fallback for _meta.callerCwd.</summary>
    static string? _mcpDetectedCwd;

    /// <summary>
    /// Detect workspace CWD for MCP server session. Multi-strategy:
    /// 1. Parent process chain: VS Code title -> ExtractCwdFromVsCodeTitle
    /// 2. CWD heuristic: if current directory has .mcp.json or .git -> likely project root
    /// 3. ~/.claude/projects/: find most recently modified session matching CWD
    /// Works for VS Code, Claude Desktop, Codex, and any MCP client.
    /// </summary>
    static string? DetectMcpParentCwd()
    {
        // Strategy 1: walk parent process chain for VS Code / Claude Desktop
        try
        {
            int cur = GetParentPid(Environment.ProcessId);
            for (int depth = 0; cur > 0 && depth < 12; depth++)
            {
                try
                {
                    var p = Process.GetProcessById(cur);
                    var name = (p.ProcessName ?? "").ToLowerInvariant();
                    var title = GetMainWindowTitleSafe(p);

                    // VS Code: extract CWD from window title
                    if (name == "code" || title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                    {
                        var cwd = ExtractCwdFromVsCodeTitle(title);
                        if (cwd != null) return cwd;
                    }

                    var next = GetParentPid(cur);
                    if (next <= 0 || next == cur) break;
                    cur = next;
                }
                catch { break; }
            }
        }
        catch { }

        // Strategy 2: CWD heuristic -- MCP clients often set child CWD to project root
        var envCwd = Environment.CurrentDirectory;
        if (!string.IsNullOrEmpty(envCwd)
            && !envCwd.Contains("system32", StringComparison.OrdinalIgnoreCase)
            && !envCwd.Equals(Path.GetDirectoryName(Environment.ProcessPath), StringComparison.OrdinalIgnoreCase))
        {
            // Project root markers: .mcp.json, .git, CLAUDE.md
            if (File.Exists(Path.Combine(envCwd, ".mcp.json"))
                || Directory.Exists(Path.Combine(envCwd, ".git"))
                || File.Exists(Path.Combine(envCwd, "CLAUDE.md")))
                return envCwd;
        }

        return null;
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern bool FreeConsole();

    static int McpCommand(string[] args)
    {
        McpLauncherMode = args.Contains("--launcher");

        // Detach from parent console when spawned by EyeMcpClient -- prevents LPC deadlock
        // in UIA calls (FindAllPrompts, DetectClaudeDesktopStatus) that csrss.exe mediates.
        if (Environment.GetEnvironmentVariable("WKAPPBOT_MCP_DETACH") == "1")
        {
            try { FreeConsole(); } catch { }
        }

        // Admin pipe mode: use named pipes instead of stdin/stdout
        // Launched by Launcher via ShellExecuteExW runas with --admin-pipes <stdin> <stdout>
        string? adminStdinPipe = null, adminStdoutPipe = null;
        for (int ai = 0; ai < args.Length - 2; ai++)
        {
            if (args[ai] == "--admin-pipes")
            {
                adminStdinPipe = args[ai + 1];
                adminStdoutPipe = args[ai + 2];
                break;
            }
        }

        Stream jsonOut;
        Stream jsonIn;
        if (adminStdinPipe != null && adminStdoutPipe != null)
        {
            // Connect to Launcher's named pipes
            Console.Error.WriteLine($"[MCP:ADMIN] Connecting to pipes: in={adminStdinPipe} out={adminStdoutPipe}");
            try
            {
                var pipeIn = new System.IO.Pipes.NamedPipeClientStream(".", adminStdinPipe, System.IO.Pipes.PipeDirection.In);
                pipeIn.Connect(10_000); // 10s timeout
                var pipeOut = new System.IO.Pipes.NamedPipeClientStream(".", adminStdoutPipe, System.IO.Pipes.PipeDirection.Out);
                pipeOut.Connect(10_000);
                jsonIn = pipeIn;
                jsonOut = pipeOut;
                Console.Error.WriteLine("[MCP:ADMIN] Named pipes connected -- admin MCP ready");
                // Admin is live and UAC-approved: this is the cheapest
                // opportunity to clear a stuck hot-swap. The normal-core
                // path can't touch admin-protected orphans or rename the
                // live exe if Eye is stale; admin core can. Fire-and-forget
                // so the primary admin request isn't delayed.
                _ = System.Threading.Tasks.Task.Run(() => TryAdminHotSwap());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[MCP:ADMIN] Pipe connection failed: {ex.Message}");
                return 1;
            }
        }
        else
        {
            // Normal mode: stdin/stdout
            jsonOut = Console.OpenStandardOutput();
            jsonIn = Console.OpenStandardInput();
        }

        // -- 앱봇관리 콘솔 호스트 설정 --
        // stderr -> TeeTextWriter(stderr, mainLogFile) -> WT 탭 (no-focus)
        // Console.Out -> ThreadRoutingWriter(Console.Error)
        //   -> 도구별 탭: HandleToolsCallAsync 내에서 Route(toolTee) 로 분기
        TrySetupMcpConsoleHost(args);

        // Console.Out -> ThreadRoutingWriter(stderr) so all logging goes through stderr
        // Tool invocations will use ThreadRoutingWriter.Route() for per-tab output
        Console.SetOut(new ThreadRoutingWriter(Console.Error));

        IsMcpMode = true; // global flag: no console windows, no Eye spawn, no elevation launch, no AllocConsole

        // Detect parent VS Code/Claude Desktop -> extract workspace CWD for card system.
        // MCP server is spawned by VS Code extension; parent chain: VS Code -> node -> wkappbot.
        _mcpDetectedCwd = DetectMcpParentCwd();
        if (_mcpDetectedCwd != null)
            EyeCmdPipeServer.CallerCwd.Value = _mcpDetectedCwd;

        Console.Error.WriteLine($"[MCP] Server starting... (launcher={McpLauncherMode}, cwd={_mcpDetectedCwd ?? "(none)"})");

        // Register session for card system (MCP server = long-lived -> one session per server)
        SessionRegister(_mcpDetectedCwd);
        AppDomain.CurrentDomain.ProcessExit += (_, _) => SessionUnregister();
        Console.Error.WriteLine($"[MCP] Session registered: pid={Environment.ProcessId} cwd={_mcpDetectedCwd ?? "(none)"}");

        // -- FlaUI/UIA preload: background fire-and-forget --
        // Cold-start FlaUI assembly loading takes ~30s. Preload while waiting for first command.
        // By the time first a11y request arrives, assemblies are already in memory.
        _ = Task.Run(() =>
        {
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                // Touch FlaUI types to trigger assembly loading
                _ = typeof(FlaUI.UIA3.UIA3Automation).Assembly;
                _ = typeof(FlaUI.Core.AutomationElements.AutomationElement).Assembly;
                Console.Error.WriteLine($"[MCP] FlaUI assemblies loaded in {sw.ElapsedMilliseconds}ms");
                // Actually create UIA3Automation to trigger COM initialization (the real 30s bottleneck)
                using var uia = new FlaUI.UIA3.UIA3Automation();
                uia.ConnectionTimeout = TimeSpan.FromSeconds(2);
                Console.Error.WriteLine($"[MCP] FlaUI UIA3Automation initialized in {sw.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[MCP] FlaUI preload failed (non-fatal): {ex.Message}");
            }
        });

        var writer = new StreamWriter(jsonOut, new UTF8Encoding(false)) { AutoFlush = true };

        try
        {
            using var reader = new StreamReader(jsonIn, Encoding.UTF8);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var request = JsonNode.Parse(line);
                    if (request == null) continue;

                    var method = request["method"]?.GetValue<string>() ?? "";
                    var id = request["id"];
                    var @params = request["params"] as JsonObject;

                    Console.Error.WriteLine($"[MCP] << {method} (id={id})");

                    JsonNode? result = null;
                    var dispatchedAsync = false;

                    switch (method)
                    {
                        case "initialize":
                            result = HandleInitialize(@params);
                            break;
                        case "notifications/initialized":
                            result = null; // notification, no response
                            break;
                        case "tools/list":
                            result = HandleToolsList();
                            break;
                        case "tools/call":
                            if (ShouldDispatchToolsCallAsync(@params))
                            {
                                dispatchedAsync = true;
                                _ = Task.Run(() => HandleToolsCallAsync(@params, writer, id));
                            }
                            else
                            {
                                result = HandleToolsCall(@params, writer, id);
                            }
                            break;
                        case "ping":
                            result = new JsonObject { ["pong"] = true };
                            break;
                        default:
                            result = null;
                            break;
                    }

                    // Notifications (no id) don't get responses
                    if (id == null) continue;
                    if (dispatchedAsync) continue;

                    if (result != null)
                    {
                        var response = new JsonObject
                        {
                            ["jsonrpc"] = "2.0",
                            ["id"] = id?.DeepClone(),
                            ["result"] = result
                        };
                        WriteJsonRpc(writer, response);
                        Console.Error.WriteLine($"[MCP] >> {method} OK");
                    }
                    else if (method.StartsWith("notifications/"))
                    {
                        // Notifications don't need responses
                    }
                    else
                    {
                        // Unknown method
                        var error = new JsonObject
                        {
                            ["jsonrpc"] = "2.0",
                            ["id"] = id?.DeepClone(),
                            ["error"] = new JsonObject
                            {
                                ["code"] = -32601,
                                ["message"] = $"Method not found: {method}"
                            }
                        };
                        WriteJsonRpc(writer, error);
                        Console.Error.WriteLine($"[MCP] >> ERROR: unknown method {method}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[MCP] Error processing request: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[MCP] Fatal: {ex.Message}");
            return 1;
        }

        Console.Error.WriteLine("[MCP] Server stopped.");
        return 0;
    }

    static readonly JsonSerializerOptions McpJsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // -- initialize ---------------------------------------------

    static JsonNode HandleInitialize(JsonObject? @params)
    {
        return new JsonObject
        {
            ["protocolVersion"] = "2024-11-05",
            ["capabilities"] = new JsonObject
            {
                ["tools"] = new JsonObject { },
            },
            ["serverInfo"] = new JsonObject
            {
                ["name"] = "wkappbot",
                ["version"] = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "5.1",
            }
        };
    }

    // -- tools/list ---------------------------------------------

    // Shared between MCP tools/list and loop persona -- keep in sync
    internal const string McpActionDesc =
        "Action to perform.\n" +
        "Control: close, minimize, maximize, restore, focus, move, resize\n" +
        "Element: invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range\n" +
        "Query: find, read, highlight\n" +
        "Discovery: inspect (UIA tree), windows (list windows), screenshot (capture), ocr (text extraction)\n" +
        "Async: wait (poll until element appears), eval (execute JavaScript via CDP)\n" +
        "AI Agents: ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini), ask-claude (ask Claude Desktop) - vision-capable, auto image capture\n" +
        "File I/O: file-read (read file as Unicode, encoding-aware), file-write (write Unicode to target encoding with auto backup, @file reference)\n" +
        "Utility: clipboard-read, clipboard-write, suggest (send feature request to Slack+HQ), slack (send Slack message), eye (eye tick - status snapshot)\n" +
        "Diagnostics: prompt-probe (scan all AI prompt windows - Claude Desktop, VS Code Claude Code, Codex - and report certainty, Slack display names, CWD; use all=true to include hidden/minimized windows)\n" +
        "Log search: logcat / grap / grep (search+stream wkappbot logs; use wkappbot_cli for full options)\n" +
        "  Token-efficient log access pattern: wkappbot_cli [\"logcat\",\"--hq\",\"--past\",\"30s\",\"*.file.*\",\"keyword\"] -> grep-style exit\n" +
        "  grep-compat alias: wkappbot_cli [\"grap\",\"keyword\",\"*.log\",\"--hq\",\"--past\",\"30s\"] (pattern first, files second)\n" +
        "  file glob: *.file.* / *.eye.* / ** (all) - supports ';' OR. text filters: pure regex, multiple = AND\n" +
        "Filesystem: file-read (encoding-aware read), file-write (write with encoding; creates .bak backup by default), file-edit (search-replace with backup)\n" +
        "Agent session: agent-checkpoint (snapshot before risky change), agent-dump-patch (write unified patch + restore hints to repo root)\n" +
        "Web: web-fetch (HTTP GET), web-search (Google via CDP), web-read (navigate+extract text)\n" +
        "Build/publish: prefer signaling Claude Code (Slack) to build. If Claude Code is offline, you may publish directly - always checkpoint first";

    static JsonNode HandleToolsList()
    {
        var tools = new JsonArray
        {
            McpTool("wkappbot", A11yDesc,
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["action"] = Prop("string", McpActionDesc),
                        ["grap"] = Prop("string",
                            "Window#element grap pattern. Required for window/element actions.\n" +
                            "For file-read/file-write: grap is the TARGET FILE PATH (e.g. \"src/legacy.cpp\").\n" +
                            "For suggest: grap is an optional FILE ATTACHMENT path.\n" +
                            "Window examples: \"*Notepad*\", \"*Chrome*#button.submit\", \"*App*#*MenuBar*#*File*\""),
                        ["path"] = Prop("string",
                            "Path alias for file-read/file-write/file-edit. Prefer this over grap when targeting files."),
                        ["image_path"] = Prop("string",
                            "Image file path for vision AI actions (ask-gpt, ask-gemini, ask-claude).\n" +
                            "Pass a screenshot or image file to attach it as visual context.\n" +
                            "Example: image_path=\"D:/SDK/bin/wkappbot.hq/output/capture_001.png\"\n" +
                            "Note: 'grap' also works for backward compatibility, but image_path is preferred for clarity."),
                        ["text"] = Prop("string", "Text for type/set-value/file-write/ask-*/slack actions. Use @filename to reference a temp file (e.g. \"@/tmp/edit.txt\")"),
                        ["new_string"] = Prop("string", "For file-edit: alias of text. Replacement text to write."),
                        ["depth"] = Prop("integer", "Tree depth for inspect/find (default: 3)"),
                        ["process"] = Prop("string", "Filter by process name (for windows action)"),
                        ["all"] = Prop("boolean", "Apply to ALL matching windows, or include hidden windows (for windows action)"),
                        ["timeout"] = Prop("integer", "Timeout in ms for wait action (default: 10000)"),
                        ["interval"] = Prop("integer", "Polling interval in ms for wait action (default: 500)"),
                        ["parallel"] = Prop("boolean", "If true, run tools/call asynchronously so MCP loop stays responsive. Unsafe actions are internally queued with 100ms gate polling."),
                        ["encoding"] = Prop("string", "File encoding for file-read/file-write/file-edit: 949 (CP949/Korean), 932 (Shift-JIS/Japanese), 65001 (UTF-8, default), utf-16. Enables Claude to read/write CP949 Korean source files without encoding corruption."),
                        ["i_really_want_no_backup"] = Prop("boolean", "For file-write/file-edit: skip creating a .bak-TIMESTAMP.txt backup file. Default: backup is ON."),
                        ["dry_run"] = Prop("boolean", "For file-write/file-edit: emit backup preview without modifying the original file."),
                        ["old_string"] = Prop("string", "For file-edit: the exact string to search for in the file. Must match exactly once unless replace_all=true."),
                        ["replace_all"] = Prop("boolean", "For file-edit: replace ALL occurrences instead of requiring exactly one match."),
                        ["use_regex"] = Prop("boolean", "For file-edit: treat old_string as a .NET regex pattern. Use $1/$2 capture groups in text (new_string)."),
                        ["i_really_want_lossy_encoding"] = Prop("boolean", "For file-edit: allow '?' substitution when new text contains chars not encodable in the target charset (e.g. emoji in CP949 file)."),
                        ["context"] = Prop("integer", "For file-edit: number of context lines to show around each change in output (default: 1).")
                    },
                    ["required"] = new JsonArray { "action" }
                }),
            McpTool("grap",
                "Search wkappbot logs - grep-compatible syntax (grap = GRab Accessible Pattern).\n" +
                "Pattern comes first, files second -- exactly like classic grep/rg.\n" +
                "Internally rewrites to logcat with translated args.\n\n" +
                "Examples:\n" +
                "  {pattern:\"exception\"}                                    -> search *.txt in CWD\n" +
                "  {pattern:\"NullRef\", files:\"*.log\", past:\"1h\"}           -> last 1h, exit\n" +
                "  {pattern:\"error\", files:\"*.log\", past:\"30m\", follow:true}-> scan then stream\n" +
                "  {pattern:\"error\", hq:true, recursive:true}               -> all HQ logs\n" +
                "  {pattern:\"OCR\", files:\"*.file.*\", timeout:\"30s\"}        -> watch 30s\n" +
                "  {pattern:\"err\", context:3}                               -> 3 lines context\n",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["pattern"]        = Prop("string",  "Regex pattern to search (case-insensitive by default). Required."),
                        ["files"]          = Prop("string",  "File glob(s). ';' OR. e.g. \"*.log\" / \"*.log;*.txt\" / \"**\". Default: *.log"),
                        ["past"]           = Prop("string",  "Scan files modified within duration (1h / 30m / 2d). Without follow: exits after scan."),
                        ["follow"]         = Prop("boolean", "Follow new entries after --past scan (tail -f style)."),
                        ["timeout"]        = Prop("string",  "Watch for duration then exit (e.g. 30s, 5m). Implies follow."),
                        ["hq"]             = Prop("boolean", "Include wkappbot.hq + openclaw log dirs."),
                        ["recursive"]      = Prop("boolean", "Recursive search (unlimited depth)."),
                        ["basedir"]        = Prop("string",  "Search root directory (default: CWD)."),
                        ["invert"]         = Prop("boolean", "Invert match (-v)."),
                        ["files_only"]     = Prop("boolean", "Print filenames only (-l)."),
                        ["count"]          = Prop("boolean", "Count matches per file (-c)."),
                        ["context"]        = Prop("integer", "Context lines before+after match (-C N)."),
                        ["case_sensitive"] = Prop("boolean", "Case-sensitive (default: insensitive)."),
                    },
                    ["required"] = new JsonArray { "pattern" }
                }),
            McpTool("logcat",
                "Search+stream wkappbot logs - native logcat syntax (fileFilter first, pattern second).\n" +
                "Prefer grap if you think in grep terms. Use logcat for full fileFilter control.\n\n" +
                "Examples:\n" +
                "  {files:\"*.file.*\", pattern:\"OCR-DEEP\", past:\"30s\"}     -> one-shot scan, exit\n" +
                "  {files:\"**\", pattern:\"exception\", past:\"1h\", hq:true}  -> all logs last 1h\n" +
                "  {files:\"*.eye.*\", pattern:\"error\", timeout:\"1m\"}       -> watch 1 minute\n",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["files"]          = Prop("string",  "File glob(s). ';' OR. e.g. \"*.log\" / \"*.file.*\" / \"**\" (all). Default: *.log"),
                        ["pattern"]        = Prop("string",  "Regex pattern to search (case-insensitive). Default: match all."),
                        ["past"]           = Prop("string",  "Scan files modified within duration (1h / 30m / 2d). Without follow: exits after scan."),
                        ["follow"]         = Prop("boolean", "Follow new entries after --past scan."),
                        ["timeout"]        = Prop("string",  "Watch for duration then exit (e.g. 30s, 5m). Implies follow."),
                        ["hq"]             = Prop("boolean", "Include wkappbot.hq + openclaw log dirs."),
                        ["recursive"]      = Prop("boolean", "Recursive search (unlimited depth)."),
                        ["basedir"]        = Prop("string",  "Search root directory (default: CWD)."),
                        ["invert"]         = Prop("boolean", "Invert match (-v)."),
                        ["files_only"]     = Prop("boolean", "Print filenames only (-l)."),
                        ["count"]          = Prop("boolean", "Count matches per file (-c)."),
                        ["context"]        = Prop("integer", "Context lines before+after match (-C N)."),
                        ["case_sensitive"] = Prop("boolean", "Case-sensitive (default: insensitive)."),
                    },
                    ["required"] = new JsonArray()
                }),
            McpTool("wkappbot_cli",
                // Auto-generated from GetUsageText() -- stays in sync with CLI help automatically
                "Run any wkappbot CLI command via argv array. argv[0] = wkappbot command name.\n" +
                "Examples: [\"a11y\",\"invoke\",\"*OK*\"], [\"file\",\"read\",\"src/foo.cs\"], [\"web\",\"search\",\"query\"],\n" +
                "          [\"agent\",\"checkpoint\",\"--label\",\"before compile\"], [\"slack\",\"send\",\"hello\"],\n" +
                "          [\"logcat\",\"--hq\",\"--past\",\"30s\",\"*.file.*\",\"OCR-DEEP\"]  <- grep-style log search (exits after scan)\n" +
                "          [\"logcat\",\"--hq\",\"--past\",\"1h\",\"**\",\"exception\"]          <- search all logs last 1h for 'exception'\n" +
                "          [\"grap\",\"exception\",\"*.log\",\"--past\",\"1h\"]               <- grap alias: grep-compat order (pattern first)\n" +
                "          [\"grep\",\"exception\",\"*.log\",\"--past\",\"1h\"]               <- grep alias: same as grap\n" +
                "! Build/publish: prefer signaling Claude Code via Slack. If Claude Code is offline, checkpoint first then publish.\n\n" +
                GetUsageText(),
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["argv"] = new JsonObject {
                            ["type"] = "array",
                            ["items"] = new JsonObject { ["type"] = "string" },
                            ["description"] = "Command + arguments. argv[0] = wkappbot command name."
                        },
                        ["parallel"] = Prop("boolean", "Run asynchronously (non-blocking MCP loop). Unsafe interactive commands are queued.")
                    },
                    ["required"] = new JsonArray { "argv" }
                }),
        };

        return new JsonObject { ["tools"] = tools };
    }

}
