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
// JSON-RPC 2.0 over stdin/stdout — all logs go to stderr
internal partial class Program
{
    static readonly object McpWriteGate = new();
    static readonly ConcurrentDictionary<string, SemaphoreSlim> McpActionGates = new();

    // ── APSP v1 프로파일링 메트릭 ────────────────────────────────────
    /// <summary>툴 child process 프로파일링 기본 정보. progress 알림에 확장 필드로 포함.</summary>
    record McpProgressMeta(long MemMb, double? CpuPct, int Threads, int Handles);

    /// <summary>AsyncLocal 사이드채널: PushLine → EmitToolProgress 메트릭 전달 (시그니처 변경 없음).</summary>
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

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern bool FreeConsole();

    static int McpCommand(string[] args)
    {
        McpLauncherMode = args.Contains("--launcher");

        // Detach from parent console when spawned by EyeMcpClient — prevents LPC deadlock
        // in UIA calls (FindAllPrompts, DetectClaudeDesktopStatus) that csrss.exe mediates.
        if (Environment.GetEnvironmentVariable("WKAPPBOT_MCP_DETACH") == "1")
        {
            try { FreeConsole(); } catch { }
        }

        // Redirect Console.Out to stderr so stdout is pure JSON-RPC
        var jsonOut = Console.OpenStandardOutput();

        // ── 앱봇관리 콘솔 호스트 설정 ──
        // stderr → TeeTextWriter(stderr, mainLogFile) → WT 탭 (no-focus)
        // Console.Out → ThreadRoutingWriter(Console.Error)
        //   → 도구별 탭: HandleToolsCallAsync 내에서 Route(toolTee) 로 분기
        TrySetupMcpConsoleHost(args);

        // Console.Out → ThreadRoutingWriter(stderr) so all logging goes through stderr
        // Tool invocations will use ThreadRoutingWriter.Route() for per-tab output
        Console.SetOut(new ThreadRoutingWriter(Console.Error));

        IsMcpMode = true; // global flag: no console windows, no Eye spawn, no elevation launch
        Console.Error.WriteLine($"[MCP] Server starting... (launcher={McpLauncherMode})");

        var writer = new StreamWriter(jsonOut, new UTF8Encoding(false)) { AutoFlush = true };

        try
        {
            using var reader = new StreamReader(Console.OpenStandardInput(), Encoding.UTF8);
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

    // ── initialize ──────────────────────────────────────────────

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

    // ── tools/list ──────────────────────────────────────────────

    // Shared between MCP tools/list and loop persona — keep in sync
    internal const string McpActionDesc =
        "Action to perform.\n" +
        "Control: close, minimize, maximize, restore, focus, move, resize\n" +
        "Element: invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range\n" +
        "Query: find, read, highlight\n" +
        "Discovery: inspect (UIA tree), windows (list windows), screenshot (capture), ocr (text extraction)\n" +
        "Async: wait (poll until element appears), eval (execute JavaScript via CDP)\n" +
        "AI Agents: ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini), ask-claude (ask Claude Desktop) — vision-capable, auto image capture\n" +
        "File I/O: file-read (read file as Unicode, encoding-aware), file-write (write Unicode→target encoding, @file reference)\n" +
        "Utility: clipboard-read, clipboard-write, suggest (send feature request to Slack+HQ), slack (send Slack message), eye (eye tick — status snapshot)\n" +
        "Diagnostics: prompt-probe (scan all AI prompt windows — Claude Desktop, VS Code Claude Code, Codex — and report certainty, Slack display names, CWD; use all=true to include hidden/minimized windows)\n" +
        "Log search: logcat / grap / grep (search+stream wkappbot logs; use wkappbot_cli for full options)\n" +
        "  ⭐ Token-efficient log access pattern: wkappbot_cli [\"logcat\",\"--hq\",\"--past\",\"30s\",\"*.file.*\",\"keyword\"] → grep-style exit\n" +
        "  ⭐ grep-compat alias: wkappbot_cli [\"grap\",\"keyword\",\"*.log\",\"--hq\",\"--past\",\"30s\"] (pattern first, files second)\n" +
        "  file glob: *.file.* / *.eye.* / ** (all) — supports ';' OR. text filters: pure regex, multiple = AND\n" +
        "Filesystem (read-only): file-read (encoding-aware read), file-write (write with encoding; auto-tracks original for patch restore)\n" +
        "Agent session: agent-checkpoint (snapshot before risky change), agent-dump-patch (write unified patch + restore hints to repo root)\n" +
        "Web: web-fetch (HTTP GET), web-search (Google via CDP), web-read (navigate+extract text)\n" +
        "⚠ Build/publish: prefer signaling Claude Code (Slack) to build. If Claude Code is offline, you may publish directly — always checkpoint first";

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
                            "⚠ For file-read/file-write: grap is the TARGET FILE PATH (e.g. \"src/legacy.cpp\").\n" +
                            "⚠ For suggest: grap is an optional FILE ATTACHMENT path.\n" +
                            "Window examples: \"*Notepad*\", \"*Chrome*#button.submit\", \"*App*#*MenuBar*#*File*\""),
                        ["image_path"] = Prop("string",
                            "Image file path for vision AI actions (ask-gpt, ask-gemini, ask-claude).\n" +
                            "Pass a screenshot or image file to attach it as visual context.\n" +
                            "Example: image_path=\"W:/SDK/bin/wkappbot.hq/output/capture_001.png\"\n" +
                            "Note: 'grap' also works for backward compatibility, but image_path is preferred for clarity."),
                        ["text"] = Prop("string", "Text for type/set-value/file-write/ask-*/slack actions. Use @filename to reference a temp file (e.g. \"@/tmp/edit.txt\")"),
                        ["depth"] = Prop("integer", "Tree depth for inspect/find (default: 3)"),
                        ["process"] = Prop("string", "Filter by process name (for windows action)"),
                        ["all"] = Prop("boolean", "Apply to ALL matching windows, or include hidden windows (for windows action)"),
                        ["timeout"] = Prop("integer", "Timeout in ms for wait action (default: 10000)"),
                        ["interval"] = Prop("integer", "Polling interval in ms for wait action (default: 500)"),
                        ["parallel"] = Prop("boolean", "If true, run tools/call asynchronously so MCP loop stays responsive. Unsafe actions are internally queued with 100ms gate polling."),
                        ["encoding"] = Prop("string", "File encoding for file-read/file-write/file-edit: 949 (CP949/Korean), 932 (Shift-JIS/Japanese), 65001 (UTF-8, default), utf-16. Enables Claude to read/write CP949 Korean source files without encoding corruption."),
                        ["old_string"] = Prop("string", "For file-edit: the exact string to search for in the file. Must match exactly once unless replace_all=true."),
                        ["replace_all"] = Prop("boolean", "For file-edit: replace ALL occurrences instead of requiring exactly one match."),
                        ["use_regex"] = Prop("boolean", "For file-edit: treat old_string as a .NET regex pattern. Use $1/$2 capture groups in text (new_string)."),
                        ["i_really_want_lossy_encoding"] = Prop("boolean", "For file-edit: allow '?' substitution when new text contains chars not encodable in the target charset (e.g. emoji in CP949 file)."),
                        ["i_really_want_no_backup"] = Prop("boolean", "For file-edit: skip creating a .bak-TIMESTAMP.txt backup file. Default: backup is ON."),
                        ["context"] = Prop("integer", "For file-edit: number of context lines to show around each change in output (default: 1).")
                    },
                    ["required"] = new JsonArray { "action" }
                }),
            McpTool("grap",
                "Search wkappbot logs — grep-compatible syntax (grap = GRab Accessible Pattern).\n" +
                "Pattern comes first, files second — exactly like classic grep/rg.\n" +
                "Internally rewrites to logcat with translated args.\n\n" +
                "Examples:\n" +
                "  {pattern:\"exception\"}                                    → search *.txt in CWD\n" +
                "  {pattern:\"NullRef\", files:\"*.log\", past:\"1h\"}           → last 1h, exit\n" +
                "  {pattern:\"error\", files:\"*.log\", past:\"30m\", follow:true}→ scan then stream\n" +
                "  {pattern:\"error\", hq:true, recursive:true}               → all HQ logs\n" +
                "  {pattern:\"OCR\", files:\"*.file.*\", timeout:\"30s\"}        → watch 30s\n" +
                "  {pattern:\"err\", context:3}                               → 3 lines context\n",
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
                "Search+stream wkappbot logs — native logcat syntax (fileFilter first, pattern second).\n" +
                "Prefer grap if you think in grep terms. Use logcat for full fileFilter control.\n\n" +
                "Examples:\n" +
                "  {files:\"*.file.*\", pattern:\"OCR-DEEP\", past:\"30s\"}     → one-shot scan, exit\n" +
                "  {files:\"**\", pattern:\"exception\", past:\"1h\", hq:true}  → all logs last 1h\n" +
                "  {files:\"*.eye.*\", pattern:\"error\", timeout:\"1m\"}       → watch 1 minute\n",
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
                // Auto-generated from GetUsageText() — stays in sync with CLI help automatically
                "Run any wkappbot CLI command via argv array. argv[0] = wkappbot command name.\n" +
                "Examples: [\"a11y\",\"invoke\",\"*OK*\"], [\"file\",\"read\",\"src/foo.cs\"], [\"web\",\"search\",\"query\"],\n" +
                "          [\"agent\",\"checkpoint\",\"--label\",\"before compile\"], [\"slack\",\"send\",\"hello\"],\n" +
                "          [\"logcat\",\"--hq\",\"--past\",\"30s\",\"*.file.*\",\"OCR-DEEP\"]  ← grep-style log search (exits after scan)\n" +
                "          [\"logcat\",\"--hq\",\"--past\",\"1h\",\"**\",\"exception\"]          ← search all logs last 1h for 'exception'\n" +
                "          [\"grap\",\"exception\",\"*.log\",\"--past\",\"1h\"]               ← grap alias: grep-compat order (pattern first)\n" +
                "          [\"grep\",\"exception\",\"*.log\",\"--past\",\"1h\"]               ← grep alias: same as grap\n" +
                "⚠ Build/publish: prefer signaling Claude Code via Slack. If Claude Code is offline, checkpoint first then publish.\n\n" +
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

    static JsonObject McpTool(string name, string description, JsonObject inputSchema)
    {
        return new JsonObject
        {
            ["name"] = name,
            ["description"] = description,
            ["inputSchema"] = inputSchema
        };
    }

    static JsonObject Prop(string type, string description)
    {
        return new JsonObject { ["type"] = type, ["description"] = description };
    }

    // ── tools/call ──────────────────────────────────────────────

    static JsonNode HandleToolsCall(JsonObject? @params, StreamWriter writer, JsonNode? requestId)
    {
        var toolName = @params?["name"]?.GetValue<string>() ?? "";
        var arguments = @params?["arguments"] as JsonObject ?? new JsonObject();
        var action = arguments["action"]?.GetValue<string>() ?? "";

        Console.Error.WriteLine($"[MCP] Tool: {toolName} args={arguments.ToJsonString()}");

        // Extract APSP progressToken from _meta
        var progressToken = @params?["_meta"]?["progressToken"];
        var progressCounter = new int[1];
        var progressSw = Stopwatch.StartNew();
        Action<string> emitProgress = line =>
            EmitToolProgress(writer, progressToken, line, progressCounter, progressSw);

        return RunToolCore(@params, emitProgress) ?? new JsonObject
        {
            ["content"] = new JsonArray { new JsonObject { ["type"] = "text", ["text"] = "Error: unknown tool" } },
            ["isError"] = true
        };
    }

    static JsonObject? RunToolCore(JsonObject? @params, Action<string> emitProgress)
    {
        var toolName = @params?["name"]?.GetValue<string>() ?? "";
        var arguments = @params?["arguments"] as JsonObject ?? new JsonObject();
        var action = arguments["action"]?.GetValue<string>() ?? "";

        // Extract per-call caller context from _meta (set by EyeMcpClient)
        var meta = @params?["_meta"] as JsonObject;
        var metaCwd = meta?["callerCwd"]?.GetValue<string>();
        var metaHwnd = meta?["callerHwnd"]?.GetValue<string>();
        if (metaCwd != null) EyeCmdPipeServer.CallerCwd.Value = metaCwd;
        if (metaHwnd != null && long.TryParse(metaHwnd, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal))
            EyeCmdPipeServer.CallerHwnd.Value = new IntPtr(hwndVal);

        try
        {
            // All MCP calls route through unified "a11y" command
            // Launcher mode: always external subprocess so core hot-swap applies immediately
            var (output, exitCode) = McpLauncherMode
                ? toolName switch
                {
                    "wkappbot" when action == "prompt-probe"
                        => RunCliCaptureWithCodeExternal("prompt-probe", BuildPromptProbeArgs(arguments), emitProgress),
                    "wkappbot"
                        => RunCliCaptureWithCodeExternal("a11y", BuildUnifiedArgs(arguments), emitProgress),
                    "wkappbot_cli"
                        => RunWkappbotCliExternal(arguments, emitProgress),
                    "logcat"
                        => RunCliCaptureWithCodeExternal("logcat", BuildLogcatArgs(arguments, grapCompat: false), emitProgress),
                    "grap"
                        => RunCliCaptureWithCodeExternal("logcat", BuildLogcatArgs(arguments, grapCompat: true), emitProgress),
                    _ => ($"Unknown tool: {toolName}", 1)
                }
                : toolName switch
                {
                    "wkappbot" when action == "prompt-probe"
                        => RunCliCaptureWithCode("prompt-probe", BuildPromptProbeArgs(arguments), emitProgress),
                    "wkappbot" => ShouldRunOutOfProc(action)
                        ? RunCliCaptureWithCodeExternal("a11y", BuildUnifiedArgs(arguments), emitProgress)
                        : RunCliCaptureWithCode("a11y", BuildUnifiedArgs(arguments), emitProgress),
                    "wkappbot_cli" => RunWkappbotCli(arguments, emitProgress),
                    "logcat"
                        => RunCliCaptureWithCode("logcat", BuildLogcatArgs(arguments, grapCompat: false), emitProgress),
                    "grap"
                        => RunCliCaptureWithCode("logcat", BuildLogcatArgs(arguments, grapCompat: true), emitProgress),
                    _ => ($"Unknown tool: {toolName}", 1)
                };

            var result = new JsonObject
            {
                ["content"] = new JsonArray
                {
                    new JsonObject { ["type"] = "text", ["text"] = output }
                }
            };
            if (exitCode != 0)
                result["isError"] = true;
            return result;
        }
        catch (Exception ex)
        {
            return new JsonObject
            {
                ["content"] = new JsonArray
                {
                    new JsonObject { ["type"] = "text", ["text"] = $"Error: {ex.Message}" }
                },
                ["isError"] = true
            };
        }
    }

    // ── Argument builders ───────────────────────────────────────

    static string[] BuildUnifiedArgs(JsonObject args)
    {
        var list = new List<string>();
        var action = args["action"]?.GetValue<string>() ?? "inspect";
        list.Add(action);
        // image_path is the preferred param for ask-* vision actions; grap is backward-compat alias
        var isAskAction = action.StartsWith("ask-", StringComparison.OrdinalIgnoreCase);
        if (isAskAction && args["image_path"] is JsonNode ip)
            list.Add(ip.GetValue<string>());
        else if (args["grap"] is JsonNode g)
            list.Add(g.GetValue<string>());
        // Action-specific params
        if (args["text"] is JsonNode t) { list.Add("--text"); list.Add(t.GetValue<string>()); }
        if (args["depth"] is JsonNode d) { list.Add("--depth"); list.Add(d.ToString()); }
        if (args["process"] is JsonNode p) { list.Add("--process"); list.Add(p.GetValue<string>()); }
        if (args["timeout"] is JsonNode to) { list.Add("--timeout"); list.Add(to.ToString()); }
        if (args["interval"] is JsonNode iv) { list.Add("--interval"); list.Add(iv.ToString()); }
        if (args["all"]?.GetValue<bool>() == true) list.Add("--all");
        if (args["encoding"] is JsonNode enc) { list.Add("--encoding"); list.Add(enc.GetValue<string>()); }
        // file-edit specific params
        if (args["old_string"] is JsonNode os) { list.Add("--old-string"); list.Add(os.GetValue<string>()); }
        if (args["replace_all"]?.GetValue<bool>()         == true) list.Add("--replace-all");
        if (args["use_regex"]?.GetValue<bool>()           == true) list.Add("--regex");
        if (args["i_really_want_lossy_encoding"]?.GetValue<bool>()== true) list.Add("--i-really-want-lossy-encoding");
        if (args["i_really_want_no_backup"]?.GetValue<bool>()           == true) list.Add("--i-really-want-no-backup");
        if (args["context"] is JsonNode ctx) { list.Add("--context"); list.Add(ctx.GetValue<int>().ToString()); }
        return list.ToArray();
    }

    static string[] BuildPromptProbeArgs(JsonObject args)
    {
        var list = new List<string>();
        if (args["all"]?.GetValue<bool>() == true) list.Add("--all");
        if (args["text"] is JsonNode t) list.Add(t.GetValue<string>()); // optional name filter
        return list.ToArray();
    }

    /// <summary>
    /// Build logcat CLI args from MCP tool params.
    /// grapCompat=true: grap tool (pattern first) — positional order is [fileFilter, pattern].
    /// grapCompat=false: logcat tool (fileFilter first) — positional order is [fileFilter, pattern].
    /// Both end up with same logcat positionals; difference is which param maps to which.
    /// </summary>
    static string[] BuildLogcatArgs(JsonObject args, bool grapCompat)
    {
        var list = new List<string>();

        var pattern = args["pattern"]?.GetValue<string>() ?? "";
        var files   = args["files"]?.GetValue<string>() ?? "";

        // logcat positionals: fileFilter then pattern
        if (!string.IsNullOrEmpty(files))   list.Add(files);
        if (!string.IsNullOrEmpty(pattern)) list.Add(pattern);

        if (args["hq"]?.GetValue<bool>()        == true) list.Add("--hq");
        if (args["recursive"]?.GetValue<bool>() == true) list.Add("-r");
        if (args["follow"]?.GetValue<bool>()    == true) list.Add("-f");
        if (args["invert"]?.GetValue<bool>()    == true) list.Add("-v");
        if (args["files_only"]?.GetValue<bool>()== true) list.Add("-l");
        if (args["count"]?.GetValue<bool>()     == true) list.Add("-c");
        if (args["case_sensitive"]?.GetValue<bool>() == true) list.Add("-i");
        if (args["basedir"] is JsonNode bd)  { list.Add("--basedir"); list.Add(bd.GetValue<string>()); }
        if (args["past"]    is JsonNode ps)  { list.Add("--past");    list.Add(ps.GetValue<string>()); }
        if (args["timeout"] is JsonNode to)  { list.Add("--timeout"); list.Add(to.GetValue<string>()); }
        if (args["context"] is JsonNode ctx) { list.Add("-C");        list.Add(ctx.GetValue<int>().ToString()); }

        return list.ToArray();
    }

    static string[] GetArgvFromArgs(JsonObject args)
    {
        if (args["argv"] is not JsonArray arr) return [];
        return [.. arr.Select(n => n?.GetValue<string>() ?? "").Where(s => s.Length > 0)];
    }

    // Launcher mode: always external, no in-proc routing
    static (string output, int exitCode) RunWkappbotCliExternal(JsonObject args, Action<string>? emitProgress)
    {
        var argv = GetArgvFromArgs(args);
        if (argv.Length == 0) return ("Error: argv is required and must not be empty", 1);
        return RunCliCaptureWithCodeExternal(argv[0], argv[1..], emitProgress);
    }

    static (string output, int exitCode) RunWkappbotCli(JsonObject args, Action<string>? emitProgress)
    {
        var argv = GetArgvFromArgs(args);
        if (argv.Length == 0) return ("Error: argv is required and must not be empty", 1);
        // CallerCwd/CallerHwnd already set by RunToolCore from _meta

        var cmd = argv[0];
        var rest = argv[1..];
        // Fast in-proc path for known safe commands; external for everything else
        return cmd switch
        {
            "a11y" => ShouldRunOutOfProc(rest.Length > 0 ? rest[0] : "")
                ? RunCliCaptureWithCodeExternal("a11y", rest, emitProgress)
                : RunCliCaptureWithCode("a11y", rest, emitProgress),
            "inspect" => RunCliCaptureWithCode("inspect", rest, emitProgress),
            "windows" => RunCliCaptureWithCode("windows", rest, emitProgress),
            "ocr" => RunCliCaptureWithCode("ocr", rest, emitProgress),
            "prompt-probe" => RunCliCaptureWithCode("prompt-probe", rest, emitProgress),
            "claude-detect" => RunCliCaptureWithCode("claude-detect", rest, emitProgress),
            "find-prompts" => RunCliCaptureWithCode("find-prompts", rest, emitProgress),
            // All other commands: spawn external process (safest, no in-proc side-effects)
            _ => RunCliCaptureWithCodeExternal(cmd, rest, emitProgress)
        };
    }

    static string[] BuildWebEvalArgs(JsonObject args)
    {
        var list = new List<string> { "eval" };
        if (args["expression"] is JsonNode e) list.Add(e.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildOcrArgs(JsonObject args)
    {
        var list = new List<string>();
        if (args["grap"] is JsonNode g) list.Add(g.GetValue<string>());
        return list.ToArray();
    }

    static string[] BuildNavigateArgs(JsonObject args)
    {
        var list = new List<string> { "navigate" };
        if (args["url"] is JsonNode u) list.Add(u.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebClickArgs(JsonObject args)
    {
        var list = new List<string> { "click" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebTypeArgs(JsonObject args)
    {
        var list = new List<string> { "type" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["text"] is JsonNode x) list.Add(x.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebTextArgs(JsonObject args)
    {
        var list = new List<string> { "text" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebOpenArgs(JsonObject args)
    {
        var list = new List<string> { "open" };
        if (args["url"] is JsonNode u) list.Add(u.GetValue<string>());
        if (args["headless"]?.GetValue<bool>() == true) list.Add("--headless");
        if (args["browser"]?.GetValue<bool>() == true) list.Add("--browser");
        return list.ToArray();
    }

    static string[] BuildWebDblClickArgs(JsonObject args)
    {
        var list = new List<string> { "dblclick" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebScreenshotArgs(JsonObject args)
    {
        var list = new List<string> { "screenshot" };
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebWaitArgs(JsonObject args)
    {
        var list = new List<string> { "wait" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["timeout"] is JsonNode t) { list.Add("--timeout"); list.Add(t.ToString()); }
        return list.ToArray();
    }

    static string[] BuildWebCheckArgs(JsonObject args)
    {
        var list = new List<string> { "check" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        var isChecked = args["checked"]?.GetValue<bool>() ?? true;
        list.Add(isChecked.ToString().ToLowerInvariant());
        return list.ToArray();
    }

    static string[] BuildWebSelectArgs(JsonObject args)
    {
        var list = new List<string> { "select" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["value"] is JsonNode v) list.Add(v.GetValue<string>());
        return list.ToArray();
    }

    static string[] BuildWebHtmlArgs(JsonObject args)
    {
        var list = new List<string> { "html" };
        if (args["tab"] is JsonNode t) { list.Add("--tab"); list.Add(t.GetValue<string>()); }
        return list.ToArray();
    }

    static string[] BuildWebRunArgs(JsonObject args)
    {
        var list = new List<string> { "run" };
        if (args["file"] is JsonNode f) list.Add(f.GetValue<string>());
        if (args["delay"] is JsonNode d) { list.Add("--delay"); list.Add(d.ToString()); }
        return list.ToArray();
    }

    static string[] BuildWebFileArgs(JsonObject args)
    {
        var list = new List<string> { "file" };
        if (args["selector"] is JsonNode s) list.Add(s.GetValue<string>());
        if (args["path"] is JsonNode p) list.Add(p.GetValue<string>());
        return list.ToArray();
    }

    static bool ShouldDispatchToolsCallAsync(JsonObject? @params)
    {
        var arguments = @params?["arguments"] as JsonObject;
        if (arguments == null) return false;

        // Caller-selected parallel mode.
        // parallel=true means "don't block MCP request loop"; execution still
        // respects action gates for unsafe interactive actions.
        return arguments["parallel"]?.GetValue<bool>() == true;
    }

    static async Task HandleToolsCallAsync(JsonObject? @params, StreamWriter writer, JsonNode? requestId)
    {
        JsonObject? response = null;
        // Extract APSP progressToken from _meta
        var progressToken = @params?["_meta"]?["progressToken"];
        var progressCounter = new int[1];
        var progressSw = Stopwatch.StartNew();
        Action<string> emitProgress = line =>
            EmitToolProgress(writer, progressToken, line, progressCounter, progressSw);

        try
        {
            var toolName = @params?["name"]?.GetValue<string>() ?? "";
            var args = @params?["arguments"] as JsonObject ?? new JsonObject();
            string action, grap;
            if (toolName == "wkappbot_cli")
            {
                var argv = GetArgvFromArgs(args);
                action = argv.Length > 0 ? argv[0] : "";
                // For "a11y <action> <grap>", extract inner action and grap
                if (action.Equals("a11y", StringComparison.OrdinalIgnoreCase))
                {
                    action = argv.Length > 1 ? argv[1] : "";
                    grap = argv.Length > 2 ? argv[2] : "";
                }
                else
                {
                    grap = argv.Length > 1 ? argv[1] : "";
                }
            }
            else
            {
                action = args["action"]?.GetValue<string>() ?? "";
                grap = args["grap"]?.GetValue<string>() ?? "";
            }
            var gateKey = GetActionGateKey(action, grap);
            var gate = McpActionGates.GetOrAdd(gateKey, _ => new SemaphoreSlim(1, 1));

            var queued = false;
            var waitSince = Stopwatch.StartNew();
            while (!await gate.WaitAsync(100).ConfigureAwait(false))
            {
                if (!queued)
                {
                    queued = true;
                    emitProgress($"[queued] waiting gate={gateKey}");
                }
                else
                {
                    emitProgress($"[queued] {waitSince.ElapsedMilliseconds}ms gate={gateKey}");
                }
            }

            try
            {
                if (queued)
                    emitProgress($"[start] gate acquired after {waitSince.ElapsedMilliseconds}ms");

                // 툴 출력 → MCP 세션 탭에 믹스 (별도 탭 없음)
                var result = RunToolCore(@params, emitProgress) ?? new JsonObject
                {
                    ["content"] = new JsonArray
                    {
                        new JsonObject { ["type"] = "text", ["text"] = "Error: empty MCP result" }
                    },
                    ["isError"] = true
                };

                response = new JsonObject
                {
                    ["jsonrpc"] = "2.0",
                    ["id"] = requestId?.DeepClone(),
                    ["result"] = result
                };
            }
            finally
            {
                gate.Release();
            }
        }
        catch (Exception ex)
        {
            response = new JsonObject
            {
                ["jsonrpc"] = "2.0",
                ["id"] = requestId?.DeepClone(),
                ["result"] = new JsonObject
                {
                    ["content"] = new JsonArray
                    {
                        new JsonObject
                        {
                            ["type"] = "text",
                            ["text"] = $"Error: {ex.Message}"
                        }
                    },
                    ["isError"] = true
                }
            };
        }

        if (response != null)
            WriteJsonRpc(writer, response);
    }

    static string GetActionGateKey(string action, string grap)
    {
        if (string.IsNullOrWhiteSpace(action))
            return "action:default";

        var a = action.ToLowerInvariant();

        // Read-only-ish actions can run in parallel without shared input lock.
        if (IsParallelSafeAction(a))
            return $"read:{NormalizeGateToken(grap)}:{a}";

        // Input/interactive actions are kept globally serialized to avoid
        // focus theft and keystroke interleaving across sessions.
        if (RequiresGlobalInteractiveGate(a))
            return "interactive:global";

        // Window/target scoped actions can run concurrently on different targets.
        if (RequiresTargetScopedGate(a))
            return $"target:{NormalizeGateToken(grap)}:{a}";

        // Conservative default.
        return $"target:{NormalizeGateToken(grap)}:{a}";
    }

    static bool IsParallelSafeAction(string action)
    {
        return action is
            "inspect" or "windows" or "screenshot" or "ocr" or
            "read" or "find" or "eval" or "highlight" or "prompt-probe";
    }

    static bool RequiresGlobalInteractiveGate(string action)
    {
        return action.StartsWith("ask-", StringComparison.OrdinalIgnoreCase) ||
               action is
                   "type" or "set-value" or "set-range" or
                   "click" or "invoke" or "toggle" or
                   "expand" or "collapse" or "select" or "scroll" or
                   "clipboard-write" or "file-write";
    }

    static bool RequiresTargetScopedGate(string action)
    {
        return action is
            "wait" or "focus" or "move" or "resize" or
            "close" or "minimize" or "maximize" or "restore" or
            "clipboard-read" or "file-read";
    }

    static string NormalizeGateToken(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "_";
        var trimmed = s.Trim();
        if (trimmed.Length > 64) trimmed = trimmed[..64];
        return trimmed.Replace('\\', '/').Replace(' ', '_');
    }

    static void WriteJsonRpc(StreamWriter writer, JsonObject payload)
    {
        var json = payload.ToJsonString(McpJsonOptions);
        lock (McpWriteGate)
        {
            writer.WriteLine(json);
        }
    }

    static bool ShouldRunOutOfProc(string action)
    {
        if (string.IsNullOrWhiteSpace(action)) return false;
        // Slow/interactive actions benefit most from process isolation + streaming.
        if (action.StartsWith("ask-", StringComparison.OrdinalIgnoreCase)) return true;
        if (action.Equals("wait", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    // APSP v1: emit standard MCP notifications/progress with APSP extended fields.
    // progressToken: from _meta.progressToken in tools/call request (null → fallback to notifications/message)
    // progressCounter: shared int[1] incremented per notification (progress field)
    // sw: elapsed time since tool start
    static void EmitToolProgress(StreamWriter writer, JsonNode? progressToken, string line, int[] progressCounter, Stopwatch sw)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

        // \x00STATUS\x00 prefix: 워치독 등에서 status 필드 오버라이드 (e.g. "\x00waiting\x00msg")
        string status = "running";
        if (line.Length > 2 && line[0] == '\x00')
        {
            var end = line.IndexOf('\x00', 1);
            if (end > 1)
            {
                status = line[1..end];
                line = line[(end + 1)..];
            }
        }
        if (string.IsNullOrWhiteSpace(line)) return;

        const int max = 800;
        var text = line.Length > max ? line[..max] + "..." : line;
        var n = System.Threading.Interlocked.Increment(ref progressCounter[0]);
        var elapsed = sw.Elapsed.TotalSeconds;

        // APSP v1: child process 프로파일링 메트릭 (AsyncLocal 사이드채널)
        var meta = _currentProgressMeta.Value;

        if (progressToken != null)
        {
            // Standard MCP notifications/progress with APSP extended fields
            var prms = new JsonObject
            {
                ["progressToken"] = progressToken.DeepClone(),
                ["progress"] = n,
                // APSP v1 extensions
                ["status"]  = status,
                ["elapsed"] = Math.Round(elapsed, 2),
                ["data"]    = $"{n}> {text}"   // N> prefix: progress번호와 콘솔출력 1:1 매칭
            };
            if (meta != null)
            {
                prms["mem_mb"]  = meta.MemMb;
                if (meta.CpuPct.HasValue) prms["cpu_pct"] = meta.CpuPct.Value;
                prms["threads"] = meta.Threads;
                prms["handles"] = meta.Handles;
            }
            var note = new JsonObject { ["jsonrpc"] = "2.0", ["method"] = "notifications/progress", ["params"] = prms };
            WriteJsonRpc(writer, note);
        }
        else
        {
            // Fallback: notifications/message (no progressToken supplied)
            var note = new JsonObject
            {
                ["jsonrpc"] = "2.0",
                ["method"] = "notifications/message",
                ["params"] = new JsonObject
                {
                    ["level"] = "info",
                    ["data"] = text
                }
            };
            WriteJsonRpc(writer, note);
        }
    }

    // ── CLI execution with output capture ───────────────────────

    /// <summary>
    /// Run a wkappbot CLI command and capture its console output as a string.
    /// Temporarily redirects Console.Out to a StringWriter.
    /// Returns (output, exitCode) for error structuring.
    /// </summary>
    sealed class LineCaptureWriter : TextWriter
    {
        readonly StringBuilder _all = new();
        readonly StringBuilder _line = new();
        readonly Action<string>? _onLine;

        public LineCaptureWriter(Action<string>? onLine) => _onLine = onLine;
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            _all.Append(value);
            if (value == '\n')
            {
                EmitLine();
                return;
            }
            if (value != '\r') _line.Append(value);
        }

        public override void Write(string? value)
        {
            if (string.IsNullOrEmpty(value)) return;
            foreach (var ch in value) Write(ch);
        }

        void EmitLine()
        {
            if (_line.Length == 0) return;
            _onLine?.Invoke(_line.ToString());
            _line.Clear();
        }

        public void FlushPartialLine()
        {
            if (_line.Length == 0) return;
            _onLine?.Invoke(_line.ToString());
            _line.Clear();
        }

        public string GetAllText() => _all.ToString();
    }

    static (string output, int exitCode) RunCliCaptureWithCode(string command, string[] args, Action<string>? onOutputLine = null)
    {
        var capture = new LineCaptureWriter(onOutputLine);
        var prevOut = Console.Out;
        var prevErr = Console.Error;
        Console.SetOut(capture);
        Console.SetError(capture);

        int exitCode = 0;
        try
        {
            exitCode = command switch
            {
                "inspect" => InspectCommand(args),
                "a11y" => A11yCommand(args),
                "windows" => WindowsCommand(args),
                "web" => WebCommand(args),
                "ocr" => OcrCommand(args),
                "prompt-probe" => PromptProbeCommand(args),
                "claude-detect" => ClaudeDetectCommand(args),
                "find-prompts" => FindPromptsCommand(args),
                _ => -1
            };
        }
        catch (Exception ex)
        {
            capture.WriteLine($"Error: {ex.Message}");
            exitCode = 1;
        }
        finally
        {
            capture.FlushPartialLine();
            Console.SetOut(prevOut);
            Console.SetError(prevErr);
        }

        return (capture.GetAllText().Trim(), exitCode);
    }

    static (string output, int exitCode) RunCliCaptureWithCodeExternal(string command, string[] args, Action<string>? onOutputLine = null)
    {
        var exe = Environment.ProcessPath;
        if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
            return ($"Error: process path unavailable for out-of-proc run ({exe})", 1);

        var psi = new ProcessStartInfo(exe)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        psi.ArgumentList.Add(command);
        foreach (var arg in args) psi.ArgumentList.Add(arg);

        var sb = new StringBuilder();
        object gate = new();
        int pushedPid = 0; // child PID — set after proc.Start()

        void PushLine(string line)
        {
            if (line == null) return;
            lock (gate) { sb.AppendLine(line); }
            // AsyncLocal 사이드채널: EmitToolProgress가 읽어서 progress 알림에 메트릭 포함
            _currentProgressMeta.Value = pushedPid > 0 ? SampleProcessMetrics(pushedPid) : null;
            onOutputLine?.Invoke(line);
            _currentProgressMeta.Value = null;
        }

        async Task PumpAsync(StreamReader reader)
        {
            while (true)
            {
                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (line == null) break;
                PushLine(line);
            }
        }

        try
        {
            using var proc = Process.Start(psi);
            if (proc == null) return ("Error: failed to start child process", 1);

            pushedPid = proc.Id; // PushLine이 메트릭 샘플링에 사용
            var outTask = PumpAsync(proc.StandardOutput);
            var errTask = PumpAsync(proc.StandardError);

            using var watchdogCts = new CancellationTokenSource();
            var watchdogTask = McpToolWaitWatchdog(proc.Id, command, onOutputLine, watchdogCts.Token);

            proc.WaitForExit();
            watchdogCts.Cancel();
            try { watchdogTask.Wait(500); } catch { }
            Task.WaitAll(outTask, errTask);

            return (sb.ToString().Trim(), proc.ExitCode);
        }
        catch (Exception ex)
        {
            return ($"Error: out-of-proc execution failed: {ex.Message}", 1);
        }
    }

    /// <summary>
    /// MCP 툴 프로세스 블로킹 대기 감지 워치독.
    /// ProcessThread.ThreadState/WaitReason API 사용 (NtQuerySystemInformation 불필요).
    /// 모든 스레드가 Wait 상태일 때 MCP 콜러에 즉시 통보 + 콘솔 출력 + Slack 알림.
    ///
    /// WaitReason별 태그:
    ///   UserRequest  → [WAIT_INPUT]  (stdin/콘솔 읽기 대기 — e.g. cmd.exe 무인수 실행)
    ///   LpcReply     → [WAIT_IPC]    (IPC 대기)
    ///   Executive    → [WAIT_KERNEL] (커널 객체 대기)
    ///   기타         → [WAIT_OTHER]
    /// </summary>
    static async Task McpToolWaitWatchdog(int pid, string cmdLabel, Action<string>? onOutputLine, CancellationToken cancel)
    {
        var reported = new HashSet<string>(StringComparer.Ordinal);
        // 프로세스 시작 직후 안정화 대기
        try { await Task.Delay(1000, cancel).ConfigureAwait(false); } catch (OperationCanceledException) { return; }

        bool isBlocking = false;
        while (!cancel.IsCancellationRequested)
        {
            try
            {
                using var p = Process.GetProcessById(pid);
                if (p.HasExited) break;

                var threads = p.Threads.Cast<ProcessThread>().ToList();
                if (threads.Count == 0) break;

                var waitReasons = new List<ThreadWaitReason>();
                foreach (var t in threads)
                {
                    try
                    {
                        if (t.ThreadState == System.Diagnostics.ThreadState.Wait)
                            waitReasons.Add(t.WaitReason);
                    }
                    catch { }
                }

                if (waitReasons.Count > 0 && waitReasons.Count >= threads.Count - 1)
                {
                    isBlocking = true;
                    var dominant = waitReasons
                        .GroupBy(r => r)
                        .OrderByDescending(g => g.Count())
                        .First().Key;

                    var (tag, statusVal, desc) = dominant switch
                    {
                        ThreadWaitReason.UserRequest    => ("[WAIT_INPUT]", "wait_input",  "stdin/콘솔 입력 대기 (cmd.exe 등)"),
                        ThreadWaitReason.LpcReply       => ("[WAIT_IPC]",   "wait_ipc",   "IPC 응답 대기"),
                        ThreadWaitReason.LpcReceive     => ("[WAIT_IPC]",   "wait_ipc",   "IPC 수신 대기"),
                        ThreadWaitReason.Executive      => ("[WAIT_LOCK]",  "wait_lock",  "동기화 객체 대기 (mutex/event/semaphore)"),
                        ThreadWaitReason.Suspended      => ("[WAIT_SUSP]",  "suspended",  "일시정지됨"),
                        ThreadWaitReason.PageIn         => ("[WAIT_IO]",    "wait_io",    "디스크 I/O 대기"),
                        ThreadWaitReason.ExecutionDelay => ("[WAIT_SLEEP]", "sleeping",   "sleep/delay 대기"),
                        _                               => ("[WAIT_OTHER]", "waiting",    $"대기중 ({dominant})")
                    };

                    if (reported.Add(tag))
                    {
                        var msg = $"{tag} 툴 프로세스 블로킹! {desc} — pid={pid} cmd={cmdLabel}";
                        Console.Error.WriteLine($"[MCP] ⚠ {msg}");
                        onOutputLine?.Invoke($"\x00{statusVal}\x00⚠ {msg}");
                    }
                }
                else
                {
                    if (isBlocking)
                    {
                        // 블로킹 해제 → 재개 알림 + 재알림 허용
                        isBlocking = false;
                        reported.Clear();
                        onOutputLine?.Invoke($"\x00running\x00[RESUMED] 툴 프로세스 재개됨 — pid={pid}");
                    }
                }
            }
            catch (ArgumentException) { break; }
            catch { }

            // 블로킹 중: 50ms 거의 실시간 / 정상: 500ms
            var delay = isBlocking ? 50 : 500;
            try { await Task.Delay(delay, cancel).ConfigureAwait(false); } catch (OperationCanceledException) { break; }
        }
    }

    static string RunCliCapture(string command, string[] args)
    {
        var (output, _) = RunCliCaptureWithCode(command, args);
        return output;
    }

    static string RunCliCaptureScreenshot(JsonObject args)
    {
        var grap = args["grap"]?.GetValue<string>();
        var tempFile = Path.Combine(Path.GetTempPath(), $"mcp_screenshot_{Guid.NewGuid():N}.png");

        try
        {
            var cliArgs = new List<string>();
            if (!string.IsNullOrEmpty(grap)) cliArgs.Add(grap);
            cliArgs.Add("-o");
            cliArgs.Add(tempFile);

            var sw = new StringWriter();
            var prevOut = Console.Out;
            Console.SetOut(sw);
            try { CaptureCommand(cliArgs.ToArray()); }
            finally { Console.SetOut(prevOut); }

            if (File.Exists(tempFile))
            {
                var bytes = File.ReadAllBytes(tempFile);
                return $"Screenshot saved ({bytes.Length} bytes). Base64 PNG:\n{Convert.ToBase64String(bytes)}";
            }
            return "Screenshot failed: no output file.";
        }
        finally
        {
            try { if (File.Exists(tempFile)) File.Delete(tempFile); } catch { }
        }
    }

    // ────────────────────────────────────────────────────────────────
    // MCP 앱봇관리 콘솔 호스트
    // ────────────────────────────────────────────────────────────────

    // 콘솔 호스트 고정 위치: 맨오른쪽 모니터 우측경계 - 1610, 모니터 상측 + 10, 800x600
    const int McpWtOffsetFromRight = 1610;
    const int McpWtOffsetFromTop   = 10;
    const int McpWtWidth           = 800;
    const int McpWtHeight          = 600;

    // ── P/Invoke: 모니터 위치 + WT 창 찾기 ──────────────────────────
    [DllImport("ntdll.dll")]
    static extern int NtQueryInformationProcess(
        IntPtr hProcess, int processInfoClass,
        ref McpProcessBasicInfo pbi, int size, out int returnLen);

    [StructLayout(LayoutKind.Sequential)]
    struct McpProcessBasicInfo
    {
        IntPtr ExitStatus, PebBase, AffinityMask, BasePriority, UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    [DllImport("user32.dll")]
    static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
        McpMonitorEnumProc lpfnEnum, IntPtr dwData);
    delegate bool McpMonitorEnumProc(IntPtr hMon, IntPtr hdcMon, ref McpRect lprcMon, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern bool GetMonitorInfoW(IntPtr hMon, ref McpMonitorInfo lpmi);

    [StructLayout(LayoutKind.Sequential)]
    struct McpRect { public int left, top, right, bottom; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct McpMonitorInfo
    {
        public uint  cbSize;
        public McpRect rcMonitor;
        public McpRect rcWork;
        public uint  dwFlags;
    }

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int x, int y, int cx, int cy, uint uFlags);
    const uint SWP_NOACTIVATE = 0x0010;
    const uint SWP_NOZORDER   = 0x0004;

    // WT 메인 창 클래스 (GetClassNameW는 AppBotEyeCommands.cs에 이미 선언됨)
    const string WtWindowClass    = "CASCADIA_HOSTING_WINDOW_CLASS";
    // MCP 전용 WT 창 이름 (Eye 창과 완전 분리)
    const string McpWtWindowName  = "apbot-mcp";

    static int McpGetParentProcessId()
    {
        try
        {
            var pbi = new McpProcessBasicInfo();
            var status = NtQueryInformationProcess(
                Process.GetCurrentProcess().Handle, 0,
                ref pbi, Marshal.SizeOf<McpProcessBasicInfo>(), out _);
            return status == 0 ? (int)pbi.InheritedFromUniqueProcessId : -1;
        }
        catch { return -1; }
    }

    // EnumDisplayMonitors callback — delegate with ref parameter can't be a lambda
    static McpRect? _monitorEnumBest;
    static bool MonitorEnumCallback(IntPtr hMon, IntPtr hdcMon, ref McpRect lprc, IntPtr dwData)
    {
        var mi = new McpMonitorInfo { cbSize = (uint)Marshal.SizeOf<McpMonitorInfo>() };
        if (GetMonitorInfoW(hMon, ref mi))
        {
            if (_monitorEnumBest == null || mi.rcMonitor.right > _monitorEnumBest.Value.right)
                _monitorEnumBest = mi.rcMonitor;
        }
        return true;
    }

    /// <summary>맨오른쪽 모니터의 rcMonitor 반환. 없으면 null.</summary>
    static McpRect? GetRightmostMonitorRect()
    {
        _monitorEnumBest = null;
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumCallback, IntPtr.Zero);
        return _monitorEnumBest;
    }

    /// <summary>콘솔 호스트 고정 위치 (X,Y) 계산.</summary>
    static (int x, int y) GetMcpWtTargetPos()
    {
        var mon = GetRightmostMonitorRect();
        if (mon == null) return (0, 0);
        return (mon.Value.right - McpWtOffsetFromRight, mon.Value.top + McpWtOffsetFromTop);
    }

    /// <summary>
    /// WT 창 찾기 (CASCADIA_HOSTING_WINDOW_CLASS) → SetWindowPos(SWP_NOACTIVATE).
    /// 딜레이 후 비동기 실행 — 창이 뜨기 전에 호출하면 실패하므로.
    /// wtPid > 0 이면 해당 프로세스 소유 창만 이동 (Eye WT 창 오이동 방지).
    /// </summary>
    static void ScheduleWtWindowPosition(int delayMs = 900, int wtPid = 0)
    {
        var (targetX, targetY) = GetMcpWtTargetPos();
        _ = Task.Delay(delayMs).ContinueWith(_ =>
        {
            try
            {
                var sb   = new System.Text.StringBuilder(256);
                IntPtr found = IntPtr.Zero;

                // Pass 1: PID 기반 (새로 생성된 WT 창 정확히 매칭)
                if (wtPid > 0)
                {
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((hWnd, _) =>
                    {
                        sb.Clear(); GetClassNameW(hWnd, sb, 256);
                        if (sb.ToString() == WtWindowClass)
                        {
                            GetWindowThreadProcessId(hWnd, out int wPid);
                            if (wPid == wtPid) { found = hWnd; return false; }
                        }
                        return true;
                    }, IntPtr.Zero);
                }

                // Pass 2: fallback — wt.exe가 기존 창에 탭 추가 후 즉시 종료 시 PID 없음
                // → 임의의 WT 창 중 apbot-mcp 창(이미 존재)에 위치 적용
                if (found == IntPtr.Zero)
                {
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((hWnd, _) =>
                    {
                        sb.Clear(); GetClassNameW(hWnd, sb, 256);
                        if (sb.ToString() == WtWindowClass) { found = hWnd; return false; }
                        return true;
                    }, IntPtr.Zero);
                }

                if (found != IntPtr.Zero)
                    SetWindowPos(found, IntPtr.Zero, targetX, targetY, McpWtWidth, McpWtHeight,
                        SWP_NOACTIVATE | SWP_NOZORDER);
            }
            catch { }
        });
    }

    /// <summary>
    /// MCP 시작 시 앱봇관리 콘솔 탭 설정.
    /// - stderr → TeeTextWriter → log 파일 (FileShare.ReadWrite)
    /// - WT 탭으로 열기 (hot-swap 시 lock file로 중복 방지)
    /// - 열린 후 고정 위치로 이동 (맨오른쪽 모니터 기준)
    /// - --no-wt 플래그로 비활성화 가능
    /// </summary>
    static void TrySetupMcpConsoleHost(string[] args)
    {
        if (args.Contains("--no-wt")) return;
        try
        {
            var parentPid = McpGetParentProcessId();
            var sessionKey = parentPid > 0 ? parentPid.ToString() : Environment.ProcessId.ToString();
            var tmpDir   = Path.GetTempPath();
            var lockFile = Path.Combine(tmpDir, $"wkappbot-mcp-wt-{sessionKey}.lock");
            var logFile  = Path.Combine(tmpDir, $"wkappbot-mcp-{sessionKey}.log");

            // Tee Console.Error → real stderr + log file (항상, hot-swap 포함)
            var tee = new TeeTextWriter(Console.Error, logFile, moveToOldOnDispose: false);
            Console.SetError(tee);

            if (!File.Exists(lockFile))
            {
                File.WriteAllText(lockFile, sessionKey);
                // Eye FSW가 logFile 생성을 감지해서 WT 탭 자동 오픈
                Console.Error.WriteLine($"[MCP] 콘솔 로그 시작 → {logFile} (Eye가 WT 탭 오픈)");
            }
            else
            {
                Console.Error.WriteLine($"[MCP] hot-swap — 기존 콘솔 탭 재사용 (session={sessionKey})");
            }
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[MCP] 콘솔 호스트 설정 실패: {ex.Message}"); } catch { }
        }
    }

}
