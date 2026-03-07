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
                ["version"] = "1.0.0"
            }
        };
    }

    // ── tools/list ──────────────────────────────────────────────

    static JsonNode HandleToolsList()
    {
        var tools = new JsonArray
        {
            McpTool("wkappbot_inspect", "Inspect a window's UI Automation tree. Returns element hierarchy with names, types, and automation IDs.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["grap"] = Prop("string", "Window pattern to match (wildcard * supported). Example: \"*Notepad*\""),
                        ["depth"] = Prop("integer", "Tree depth limit (default: 3)")
                    },
                    ["required"] = new JsonArray { "grap" }
                }),

            McpTool("wkappbot_a11y", "Perform accessibility actions on windows/elements. Actions: close, minimize, maximize, restore, focus, invoke, click, toggle, type, set-value, find.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["action"] = Prop("string", "Action to perform: close, minimize, maximize, restore, focus, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range, find"),
                        ["grap"] = Prop("string", "Window/element pattern. Use # for UIA scope: \"*Notepad*#*File*\""),
                        ["text"] = Prop("string", "Text for type/set-value actions"),
                        ["all"] = Prop("boolean", "Apply to all matching windows")
                    },
                    ["required"] = new JsonArray { "action", "grap" }
                }),

            McpTool("wkappbot_windows", "List visible windows. Returns window titles, class names, handles, and sizes.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["grap"] = Prop("string", "Optional filter pattern (wildcard * supported)"),
                        ["process"] = Prop("string", "Filter by process name"),
                        ["all"] = Prop("boolean", "Include hidden windows")
                    }
                }),

            McpTool("wkappbot_web_eval", "Evaluate JavaScript in a Chrome tab via CDP. Chrome must be running with --remote-debugging-port.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["expression"] = Prop("string", "JavaScript expression to evaluate"),
                        ["tab"] = Prop("string", "Tab pattern to find by URL/title (wildcard *). If omitted, uses active tab.")
                    },
                    ["required"] = new JsonArray { "expression" }
                }),

            McpTool("wkappbot_web_tabs", "List all Chrome tabs (title, URL, ID). Requires Chrome with --remote-debugging-port.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_screenshot", "Capture a window screenshot. Returns base64-encoded PNG.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["grap"] = Prop("string", "Window pattern to capture. If omitted, captures foreground window.")
                    }
                }),

            McpTool("wkappbot_ocr", "Run OCR on a window or image. Returns recognized text.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["grap"] = Prop("string", "Window pattern or image file path")
                    },
                    ["required"] = new JsonArray { "grap" }
                }),

            McpTool("wkappbot_navigate", "Navigate a Chrome tab to a URL.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["url"] = Prop("string", "URL to navigate to"),
                        ["tab"] = Prop("string", "Tab pattern to find (wildcard *)")
                    },
                    ["required"] = new JsonArray { "url" }
                }),

            McpTool("wkappbot_web_click", "Click a web element by CSS selector in a Chrome tab.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of element to click"),
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    },
                    ["required"] = new JsonArray { "selector" }
                }),

            McpTool("wkappbot_web_type", "Type text into a web element by CSS selector.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of input element"),
                        ["text"] = Prop("string", "Text to type"),
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    },
                    ["required"] = new JsonArray { "selector", "text" }
                }),

            McpTool("wkappbot_web_text", "Get text content of a web element by CSS selector.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of element"),
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    },
                    ["required"] = new JsonArray { "selector" }
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
            var output = toolName switch
            {
                "wkappbot_inspect" => RunCliCapture("inspect", BuildInspectArgs(arguments)),
                "wkappbot_a11y" => RunCliCapture("a11y", BuildA11yArgs(arguments)),
                "wkappbot_windows" => RunCliCapture("windows", BuildWindowsArgs(arguments)),
                "wkappbot_web_eval" => RunCliCapture("web", BuildWebEvalArgs(arguments)),
                "wkappbot_web_tabs" => RunCliCapture("web", new[] { "tabs" }),
                "wkappbot_screenshot" => RunCliCaptureScreenshot(arguments),
                "wkappbot_ocr" => RunCliCapture("ocr", BuildOcrArgs(arguments)),
                "wkappbot_navigate" => RunCliCapture("web", BuildNavigateArgs(arguments)),
                "wkappbot_web_click" => RunCliCapture("web", BuildWebClickArgs(arguments)),
                "wkappbot_web_type" => RunCliCapture("web", BuildWebTypeArgs(arguments)),
                "wkappbot_web_text" => RunCliCapture("web", BuildWebTextArgs(arguments)),
                _ => $"Unknown tool: {toolName}"
            };

            return new JsonObject
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

    static string[] BuildInspectArgs(JsonObject args)
    {
        var list = new List<string>();
        if (args["grap"] is JsonNode g) list.Add(g.GetValue<string>());
        if (args["depth"] is JsonNode d) { list.Add("--depth"); list.Add(d.ToString()); }
        return list.ToArray();
    }

    static string[] BuildA11yArgs(JsonObject args)
    {
        var list = new List<string>();
        if (args["action"] is JsonNode a) list.Add(a.GetValue<string>());
        if (args["grap"] is JsonNode g) list.Add(g.GetValue<string>());
        if (args["text"] is JsonNode t) { list.Add("--text"); list.Add(t.GetValue<string>()); }
        if (args["all"]?.GetValue<bool>() == true) list.Add("--all");
        return list.ToArray();
    }

    static string[] BuildWindowsArgs(JsonObject args)
    {
        var list = new List<string>();
        if (args["grap"] is JsonNode g) list.Add(g.GetValue<string>());
        if (args["process"] is JsonNode p) { list.Add("--process"); list.Add(p.GetValue<string>()); }
        if (args["all"]?.GetValue<bool>() == true) list.Add("--all");
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

    // ── CLI execution with output capture ───────────────────────

    /// <summary>
    /// Run a wkappbot CLI command and capture its console output as a string.
    /// Temporarily redirects Console.Out to a StringWriter.
    /// </summary>
    static string RunCliCapture(string command, string[] args)
    {
        var sw = new StringWriter();
        var prevOut = Console.Out;
        Console.SetOut(sw);

        try
        {
            _ = command switch
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
        }
        finally
        {
            Console.SetOut(prevOut);
        }

        return sw.ToString().Trim();
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
