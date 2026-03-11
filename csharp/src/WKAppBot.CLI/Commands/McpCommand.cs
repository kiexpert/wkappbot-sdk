using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace WKAppBot.CLI;

// MCP (Model Context Protocol) stdio server
// JSON-RPC 2.0 over stdin/stdout — all logs go to stderr
internal partial class Program
{
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

                    JsonNode? result = method switch
                    {
                        "initialize" => HandleInitialize(@params),
                        "notifications/initialized" => null, // notification, no response
                        "tools/list" => HandleToolsList(),
                        "tools/call" => HandleToolsCall(@params),
                        "ping" => new JsonObject { ["pong"] = true },
                        _ => null
                    };

                    // Notifications (no id) don't get responses
                    if (id == null) continue;

                    if (result != null)
                    {
                        var response = new JsonObject
                        {
                            ["jsonrpc"] = "2.0",
                            ["id"] = id?.DeepClone(),
                            ["result"] = result
                        };
                        var json = response.ToJsonString(McpJsonOptions);
                        writer.WriteLine(json);
                        Console.Error.WriteLine($"[MCP] >> {method} OK ({json.Length} bytes)");
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
                        writer.WriteLine(error.ToJsonString(McpJsonOptions));
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
                            "AI Agents: ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini) — vision-capable, auto image capture\n" +
                            "File I/O: file-read (read file as Unicode, encoding-aware), file-write (write Unicode→target encoding, @file reference)\n" +
                            "Utility: clipboard-read, clipboard-write, suggest (send feature request to Slack+HQ)"),
                        ["grap"] = Prop("string",
                            "Window#element grap pattern. Required for most actions.\n" +
                            "⚠ For ask-gpt/ask-gemini: grap is an IMAGE FILE PATH (e.g. \"screenshot.png\"), NOT a window pattern.\n" +
                            "⚠ For suggest: grap is an optional FILE ATTACHMENT path.\n" +
                            "⚠ For file-read/file-write: grap is the TARGET FILE PATH (e.g. \"src/legacy.cpp\").\n" +
                            "Window examples: \"*Notepad*\", \"*Chrome*#button.submit\", \"*App*#*MenuBar*#*File*\""),
                        ["text"] = Prop("string", "Text for type/set-value/file-write actions. Use @filename to reference a temp file (e.g. \"@/tmp/edit.txt\")"),
                        ["depth"] = Prop("integer", "Tree depth for inspect/find (default: 3)"),
                        ["process"] = Prop("string", "Filter by process name (for windows action)"),
                        ["all"] = Prop("boolean", "Apply to ALL matching windows, or include hidden windows (for windows action)"),
                        ["timeout"] = Prop("integer", "Timeout in ms for wait action (default: 10000)"),
                        ["interval"] = Prop("integer", "Polling interval in ms for wait action (default: 500)"),
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

    static JsonNode HandleToolsCall(JsonObject? @params)
    {
        var toolName = @params?["name"]?.GetValue<string>() ?? "";
        var arguments = @params?["arguments"] as JsonObject ?? new JsonObject();

        Console.Error.WriteLine($"[MCP] Tool: {toolName} args={arguments.ToJsonString()}");

        try
        {
            // All MCP calls route through unified "a11y" command
            // a11y handles: inspect, windows, screenshot, ocr as delegate actions
            var (output, exitCode) = toolName switch
            {
                "wkappbot" => RunCliCaptureWithCode("a11y", BuildUnifiedArgs(arguments)),
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
        // grap is required for all except "windows" (optional)
        if (args["grap"] is JsonNode g) list.Add(g.GetValue<string>());
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

    // ── CLI execution with output capture ───────────────────────

    /// <summary>
    /// Run a wkappbot CLI command and capture its console output as a string.
    /// Temporarily redirects Console.Out to a StringWriter.
    /// Returns (output, exitCode) for error structuring.
    /// </summary>
    static (string output, int exitCode) RunCliCaptureWithCode(string command, string[] args)
    {
        var sw = new StringWriter();
        var errSw = new StringWriter();
        var prevOut = Console.Out;
        var prevErr = Console.Error;
        Console.SetOut(sw);
        Console.SetError(errSw);

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
            sw.WriteLine($"Error: {ex.Message}");
            exitCode = 1;
        }
        finally
        {
            Console.SetOut(prevOut);
            Console.SetError(prevErr);
        }

        // Merge stdout + stderr (stderr contains error details)
        var output = sw.ToString().Trim();
        var errOutput = errSw.ToString().Trim();
        if (!string.IsNullOrEmpty(errOutput))
            output = string.IsNullOrEmpty(output) ? errOutput : $"{output}\n{errOutput}";

        return (output, exitCode);
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
