using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WKAppBot.CLI;

// MCP (Model Context Protocol) stdio server
// JSON-RPC 2.0 over stdin/stdout — all logs go to stderr
internal partial class Program
{
    static readonly object McpWriteGate = new();
    static readonly ConcurrentDictionary<string, SemaphoreSlim> McpActionGates = new();

    static int McpCommand(string[] args)
    {
        // Redirect Console.Out to stderr so stdout is pure JSON-RPC
        var jsonOut = Console.OpenStandardOutput();
        Console.SetOut(new StreamWriter(Console.OpenStandardError(), Encoding.UTF8) { AutoFlush = true });
        Console.Error.WriteLine("[MCP] Server starting...");

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
                ["tools"] = new JsonObject { }
            },
            ["serverInfo"] = new JsonObject
            {
                ["name"] = "wkappbot",
                ["version"] = "3.0.0"
            }
        };
    }

    // ── tools/list ──────────────────────────────────────────────

    static JsonNode HandleToolsList()
    {
        var tools = new JsonArray
        {
            McpTool("wkappbot", A11yDesc,
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["action"] = Prop("string",
                            "Action to perform.\n" +
                            "Control: close, minimize, maximize, restore, focus, move, resize\n" +
                            "Element: invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range\n" +
                            "Query: find, read, highlight\n" +
                            "Discovery: inspect (UIA tree), windows (list windows), screenshot (capture), ocr (text extraction)\n" +
                            "Async: wait (poll until element appears), eval (execute JavaScript via CDP)\n" +
                            "AI Agents: ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini), ask-claude (ask Claude Desktop) — vision-capable, auto image capture\n" +
                            "File I/O: file-read (read file as Unicode, encoding-aware), file-write (write Unicode→target encoding, @file reference)\n" +
                            "Utility: clipboard-read, clipboard-write, suggest (send feature request to Slack+HQ), slack (send Slack message), eye (eye tick — status snapshot)"),
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
                        ["encoding"] = Prop("string", "File encoding for file-read/file-write: 949 (CP949/Korean), 932 (Shift-JIS/Japanese), 65001 (UTF-8, default), utf-16. Enables Claude to read/write CP949 Korean source files without encoding corruption.")
                    },
                    ["required"] = new JsonArray { "action" }
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

        try
        {
            // All MCP calls route through unified "a11y" command
            // a11y handles: inspect, windows, screenshot, ocr as delegate actions
            var (output, exitCode) = toolName switch
            {
                "wkappbot" => ShouldRunOutOfProc(action)
                    ? RunCliCaptureWithCodeExternal(
                        "a11y",
                        BuildUnifiedArgs(arguments),
                        line => EmitToolProgress(writer, requestId, line))
                    : RunCliCaptureWithCode(
                        "a11y",
                        BuildUnifiedArgs(arguments),
                        line => EmitToolProgress(writer, requestId, line)),
                _ => ($"Unknown tool: {toolName}", 1)
            };

            var result = new JsonObject
            {
                ["content"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "text",
                        ["text"] = output
                    }
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
                    new JsonObject
                    {
                        ["type"] = "text",
                        ["text"] = $"Error: {ex.Message}"
                    }
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
        return list.ToArray();
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
        try
        {
            var args = @params?["arguments"] as JsonObject ?? new JsonObject();
            var action = args["action"]?.GetValue<string>() ?? "";
            var grap = args["grap"]?.GetValue<string>() ?? "";
            var gateKey = GetActionGateKey(action, grap);
            var gate = McpActionGates.GetOrAdd(gateKey, _ => new SemaphoreSlim(1, 1));

            var queued = false;
            var waitSince = Stopwatch.StartNew();
            while (!await gate.WaitAsync(100).ConfigureAwait(false))
            {
                if (!queued)
                {
                    queued = true;
                    EmitToolProgress(writer, requestId, $"[queued] waiting gate={gateKey}");
                }
                else
                {
                    EmitToolProgress(writer, requestId, $"[queued] {waitSince.ElapsedMilliseconds}ms gate={gateKey}");
                }
            }

            try
            {
                if (queued)
                    EmitToolProgress(writer, requestId, $"[start] gate acquired after {waitSince.ElapsedMilliseconds}ms");

                var result = HandleToolsCall(@params, writer, requestId) as JsonObject ?? new JsonObject
                {
                    ["content"] = new JsonArray
                    {
                        new JsonObject
                        {
                            ["type"] = "text",
                            ["text"] = "Error: empty MCP result"
                        }
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
            "read" or "find" or "eval" or "highlight";
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

    static void EmitToolProgress(StreamWriter writer, JsonNode? requestId, string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

        // Keep notification payload small and line-framed.
        const int max = 800;
        var text = line.Length > max ? line[..max] + "..." : line;
        var idText = requestId?.ToJsonString() ?? "null";

        var note = new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["method"] = "notifications/message",
            ["params"] = new JsonObject
            {
                ["level"] = "info",
                ["data"] = $"[tools/call:{idText}] {text}"
            }
        };
        WriteJsonRpc(writer, note);
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

        void PushLine(string line)
        {
            if (line == null) return;
            lock (gate)
            {
                sb.AppendLine(line);
            }
            onOutputLine?.Invoke(line);
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

            var outTask = PumpAsync(proc.StandardOutput);
            var errTask = PumpAsync(proc.StandardError);

            proc.WaitForExit();
            Task.WaitAll(outTask, errTask);

            return (sb.ToString().Trim(), proc.ExitCode);
        }
        catch (Exception ex)
        {
            return ($"Error: out-of-proc execution failed: {ex.Message}", 1);
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
}
