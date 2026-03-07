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

            McpTool("wkappbot_a11y", "Unified accessibility actions on windows AND web elements. Works on native apps (UIA) and Chrome/Electron web views (CDP auto-fallback). Actions: close, minimize, maximize, restore, focus, invoke, click, toggle, type, set-value, find, read. For web views: use CSS selectors after # (e.g. \"*Chrome*#button.submit\").",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["action"] = Prop("string", "Action: close, minimize, maximize, restore, focus, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range, find, read, highlight"),
                        ["grap"] = Prop("string", "Window#element pattern. Native: \"*Notepad*#*File*\" (UIA Name). Web: \"*Chrome*#button.submit\" (CSS selector, auto-detected). Mixed: Win32/child#UIA or CSS."),
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

            McpTool("wkappbot_web_open", "Open Chrome with CDP and navigate to URL. App mode (clean window) by default.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["url"] = Prop("string", "URL to open"),
                        ["headless"] = Prop("boolean", "Run headless (no visible window)"),
                        ["browser"] = Prop("boolean", "Normal Chrome UI with address bar (for debugging)")
                    }
                }),

            McpTool("wkappbot_web_dblclick", "Double-click a web element by CSS selector.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of element to double-click"),
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    },
                    ["required"] = new JsonArray { "selector" }
                }),

            McpTool("wkappbot_web_screenshot", "Capture page screenshot as PNG via CDP (page content only). Returns base64.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    }
                }),

            McpTool("wkappbot_web_capture", "Capture Chrome window screenshot including title bar (Win32 PrintWindow). Returns base64.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_wait", "Wait for a web element to appear (polling).",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector to wait for"),
                        ["timeout"] = Prop("integer", "Timeout in ms (default: 5000)")
                    },
                    ["required"] = new JsonArray { "selector" }
                }),

            McpTool("wkappbot_web_check", "Set checkbox checked state.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of checkbox"),
                        ["checked"] = Prop("boolean", "Check state (default: true)")
                    },
                    ["required"] = new JsonArray { "selector" }
                }),

            McpTool("wkappbot_web_select", "Select an option in a <select> element.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of <select> element"),
                        ["value"] = Prop("string", "Option value or text to select")
                    },
                    ["required"] = new JsonArray { "selector", "value" }
                }),

            McpTool("wkappbot_web_html", "Get full page HTML source.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["tab"] = Prop("string", "Tab pattern (wildcard *)")
                    }
                }),

            McpTool("wkappbot_web_url", "Get current page URL.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_title", "Get current page title.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_close", "Disconnect from Chrome CDP (does not close the browser).",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_status", "Check if Chrome CDP is active.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_restore", "Restore minimized Chrome window.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject { }
                }),

            McpTool("wkappbot_web_run", "Run a batch of web commands from a file.",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["file"] = Prop("string", "Path to steps file (each line = web subcommand)"),
                        ["delay"] = Prop("integer", "Delay between steps in ms")
                    },
                    ["required"] = new JsonArray { "file" }
                }),

            McpTool("wkappbot_web_file", "Set a file input element's value (for file uploads).",
                new JsonObject {
                    ["type"] = "object",
                    ["properties"] = new JsonObject {
                        ["selector"] = Prop("string", "CSS selector of file input"),
                        ["path"] = Prop("string", "File path to upload")
                    },
                    ["required"] = new JsonArray { "selector", "path" }
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
                "wkappbot_web_open" => RunCliCapture("web", BuildWebOpenArgs(arguments)),
                "wkappbot_web_dblclick" => RunCliCapture("web", BuildWebDblClickArgs(arguments)),
                "wkappbot_web_screenshot" => RunCliCapture("web", BuildWebScreenshotArgs(arguments)),
                "wkappbot_web_capture" => RunCliCapture("web", new[] { "capture" }),
                "wkappbot_web_wait" => RunCliCapture("web", BuildWebWaitArgs(arguments)),
                "wkappbot_web_check" => RunCliCapture("web", BuildWebCheckArgs(arguments)),
                "wkappbot_web_select" => RunCliCapture("web", BuildWebSelectArgs(arguments)),
                "wkappbot_web_html" => RunCliCapture("web", BuildWebHtmlArgs(arguments)),
                "wkappbot_web_url" => RunCliCapture("web", new[] { "url" }),
                "wkappbot_web_title" => RunCliCapture("web", new[] { "title" }),
                "wkappbot_web_close" => RunCliCapture("web", new[] { "close" }),
                "wkappbot_web_status" => RunCliCapture("web", new[] { "status" }),
                "wkappbot_web_restore" => RunCliCapture("web", new[] { "restore" }),
                "wkappbot_web_run" => RunCliCapture("web", BuildWebRunArgs(arguments)),
                "wkappbot_web_file" => RunCliCapture("web", BuildWebFileArgs(arguments)),
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
