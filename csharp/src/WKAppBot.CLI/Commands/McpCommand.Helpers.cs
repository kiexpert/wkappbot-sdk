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

internal partial class Program
{
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

    // -- tools/call ----------------------------------------------

    static JsonNode HandleToolsCall(JsonObject? @params, StreamWriter writer, JsonNode? requestId)
    {
        var toolName = @params?["name"]?.GetValue<string>() ?? "";
        var arguments = @params?["arguments"] as JsonObject ?? new JsonObject();
        var action = arguments["action"]?.GetValue<string>() ?? "";

        var _toolSw = Stopwatch.StartNew();
        Console.Error.WriteLine($"[MCP] Tool: {toolName} args={arguments.ToJsonString()}");

        // Extract APSP progressToken from _meta
        var progressToken = @params?["_meta"]?["progressToken"];
        var progressCounter = new int[1];
        var progressSw = Stopwatch.StartNew();
        Action<string> emitProgress = line =>
            EmitToolProgress(writer, progressToken, line, progressCounter, progressSw);

        var _result = RunToolCore(@params, emitProgress);
        Console.Error.WriteLine($"[MCP-PERF] {toolName} total={_toolSw.ElapsedMilliseconds}ms");
        return _result ?? new JsonObject
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
        var _toolStartMs = Environment.TickCount64;
        var _toolStartCpuMs = 0L;
        try { _toolStartCpuMs = (long)System.Diagnostics.Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds; } catch { }

        // Extract per-call caller context from _meta (set by EyeMcpClient)
        // Fallback to _mcpDetectedCwd (VS Code parent process title -> workspace folder)
        var meta = @params?["_meta"] as JsonObject;
        var metaCwd = meta?["callerCwd"]?.GetValue<string>() ?? _mcpDetectedCwd;
        var metaHwnd = meta?["callerHwnd"]?.GetValue<string>();
        if (metaCwd != null) EyeCmdPipeServer.CallerCwd.Value = metaCwd;
        if (metaHwnd != null && long.TryParse(metaHwnd, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal))
            EyeCmdPipeServer.CallerHwnd.Value = new IntPtr(hwndVal);

        // Update session card: command start
        var cmdSummary = toolName == "wkappbot_cli"
            ? string.Join(" ", (arguments["argv"] as System.Text.Json.Nodes.JsonArray)?.Select(a => a?.GetValue<string>() ?? "") ?? [])
            : $"{toolName} {action}".Trim();
        SessionUpdate(command: cmdSummary, tag: cmdSummary.Split(' ').FirstOrDefault(), status: "executing");

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

            // Check if elevation was needed -- signal Launcher via special error code
            if (McpElevationRequired)
            {
                McpElevationRequired = false;
                return new JsonObject
                {
                    ["content"] = new JsonArray
                    {
                        new JsonObject { ["type"] = "text", ["text"] = output }
                    },
                    ["isError"] = true,
                    ["_elevationRequired"] = true // Launcher intercepts this
                };
            }

            // Update session card: command done
            SessionUpdate(status: exitCode == 0 ? "result=ok" : $"result=err:{exitCode}");

            // Write errors.jsonl for non-zero exits (no TeeTextWriter in MCP in-process path)
            if (exitCode != 0)
            {
                try
                {
                    var logsDir = System.IO.Path.Combine(DataDir, "logs");
                    var cmd = $"mcp {toolName} {action}".Trim();
                    TeeTextWriter.TryAppendErrorRecordFromOutput(
                        output, cmd, exitCode,
                        Environment.TickCount64 - _toolStartMs, _toolStartCpuMs, logsDir);
                }
                catch { /* best-effort */ }
            }

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

    // -- Argument builders --------------------------------------─

    static string[] BuildUnifiedArgs(JsonObject args)
    {
        var list = new List<string>();
        var action = args["action"]?.GetValue<string>() ?? "inspect";
        list.Add(action);
        bool isFileAction = action is "file-read" or "file-write" or "file-edit";
        // image_path is the preferred param for ask-* vision actions; grap is backward-compat alias
        var isAskAction = action.StartsWith("ask-", StringComparison.OrdinalIgnoreCase);
        if (isAskAction && args["image_path"] is JsonNode ip)
            list.Add(ip.GetValue<string>());
        else if (isFileAction && args["path"] is JsonNode fp)
            list.Add(fp.GetValue<string>());
        else if (args["grap"] is JsonNode g)
            list.Add(g.GetValue<string>());
        // Action-specific params
        if (args["text"] is JsonNode t) { list.Add("--text"); list.Add(t.GetValue<string>()); }
        else if (args["new_string"] is JsonNode ns) { list.Add("--text"); list.Add(ns.GetValue<string>()); }
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
        if (args["dry_run"]?.GetValue<bool>() == true) list.Add("--dry-run");
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
    /// grapCompat=true: grap tool (pattern first) -- positional order is [fileFilter, pattern].
    /// grapCompat=false: logcat tool (fileFilter first) -- positional order is [fileFilter, pattern].
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
            "windows" => RunCliCaptureWithCode("find", rest.Length == 0 ? ["wkappbot*"] : rest, emitProgress),
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
        // NO ErrorScope here -- MCP worker is a piped process, stderr must pass through!
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

                // 툴 출력 -> MCP 세션 탭에 믹스 (별도 탭 없음)
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

    static long _lastFlushTicks;

    static void WriteJsonRpc(StreamWriter writer, JsonObject payload)
    {
        var json = payload.ToJsonString(McpJsonOptions);
        lock (McpWriteGate)
        {
            writer.WriteLine(json);
            // Smart flush: complete line written -> flush if >1s since last flush or if this is a response (has "id")
            var now = Environment.TickCount64;
            bool isResponse = payload.ContainsKey("id");
            if (isResponse || now - Interlocked.Read(ref _lastFlushTicks) > 1000)
            {
                writer.Flush();
                Interlocked.Exchange(ref _lastFlushTicks, now);
            }
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
    // progressToken: from _meta.progressToken in tools/call request (null -> fallback to notifications/message)
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

    // -- CLI execution with output capture ----------------------─

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

}
