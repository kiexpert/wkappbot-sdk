using System.Diagnostics;
using System.Text.Json;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

// partial class: wkappbot web <subcommand> — Chrome DevTools Protocol web automation
internal partial class Program
{
    static int WebCommand(string[] args)
    {
        if (args.Length == 0)
            return WebUsage();

        var sub = args[0].ToLowerInvariant();
        var restArgs = args.Skip(1).ToArray();

        return sub switch
        {
            "open" => WebOpenCommand(restArgs),
            "eval" => WebEvalCommand(restArgs),
            "click" => WebClickCommand(restArgs),
            "type" => WebTypeCommand(restArgs),
            "text" => WebGetTextCommand(restArgs),
            "screenshot" => WebScreenshotCommand(restArgs),
            "navigate" or "nav" => WebNavigateCommand(restArgs),
            "wait" => WebWaitCommand(restArgs),
            "check" => WebCheckCommand(restArgs),
            "select" => WebSelectCommand(restArgs),
            "html" => WebHtmlCommand(restArgs),
            "url" => WebUrlCommand(restArgs),
            "title" => WebTitleCommand(restArgs),
            "close" => WebCloseCommand(restArgs),
            "status" => WebStatusCommand(restArgs),
            "run" => WebRunCommand(restArgs),
            "--help" or "-h" or "help" => WebUsage(),
            _ => Error($"Unknown web subcommand: {sub}")
        };
    }

    static int WebUsage()
    {
        Console.WriteLine(@"
WKAppBot WebBot — Chrome DevTools Protocol web automation

Usage:
  wkappbot web <subcommand> [options]

Session Management:
  open [url] [--port N] [--headless] [--no-launch]
      Open Chrome with CDP and navigate to URL.
      --port N: CDP port (default: 9222)
      --headless: Run Chrome headless (no visible window)
      --no-launch: Connect to already-running Chrome (don't launch)
  close [--port N]
      Disconnect from Chrome (does not close the browser).
  status [--port N]
      Check if Chrome CDP is active on a port.

Navigation:
  navigate <url> [--port N]    Navigate current tab to URL.
  url [--port N]               Get current page URL.
  title [--port N]             Get current page title.

Page Interaction:
  click <css-selector> [--port N]
      Click an element by CSS selector.
  type <css-selector> <text> [--port N]
      Type text into an input element.
  text <css-selector> [--port N]
      Get text content of an element.
  check <css-selector> [true|false] [--port N]
      Set checkbox checked state.
  select <css-selector> <value-or-text> [--port N]
      Select an option in a <select> element.
  wait <css-selector> [--timeout N] [--port N]
      Wait for an element to appear (polling).

JavaScript:
  eval <expression> [--port N]
      Evaluate JavaScript and print the result.

Output:
  screenshot [-o output.png] [--port N]
      Capture page screenshot as PNG.
  html [--port N]
      Get full page HTML source.

Batch:
  run <steps-file.txt> [--port N] [--delay N]
      Run a batch of web commands from a file.
      Each line is a web subcommand (e.g., ""click #btn"", ""type #name hello"").

Options:
  --port N     CDP port (default: 9222)
  --timeout N  Timeout in ms for wait commands (default: 5000)
  -o <file>    Output file path (for screenshot/html)
");
        return 0;
    }

    // ── Helper: connect to CDP ───────────────────────────────────

    static int GetPort(string[] args) =>
        int.TryParse(GetArgValue(args, "--port"), out var p) ? p : 9222;

    static CdpClient ConnectCdp(int port, bool withBar = true)
    {
        var cdp = new CdpClient();
        cdp.ConnectAsync(port).GetAwaiter().GetResult();
        if (withBar)
            cdp.InjectWebBotBar = true;
        return cdp;
    }

    // ── open ─────────────────────────────────────────────────────

    static int WebOpenCommand(string[] args)
    {
        int port = GetPort(args);
        bool headless = args.Contains("--headless");
        bool noLaunch = args.Contains("--no-launch");

        // Get URL (first non-flag argument)
        string? url = args.FirstOrDefault(a => !a.StartsWith("--") && a != "true" && a != "false");

        Console.Write($"[WEB] ");

        if (!noLaunch)
        {
            Console.Write($"Launching Chrome (port={port})... ");
            var process = ChromeLauncher.LaunchAsync(port, url, headless).GetAwaiter().GetResult();
            if (process == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Chrome already running on port {port}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Chrome started (PID={process.Id})");
                Console.ResetColor();
            }
        }

        // Connect via CDP
        Console.Write($"[WEB] Connecting via CDP... ");
        using var cdp = ConnectCdp(port);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Connected ({cdp.WebSocketUrl})");
        Console.ResetColor();

        // Navigate if URL provided and Chrome was already running
        if (!string.IsNullOrEmpty(url) && noLaunch)
        {
            Console.Write($"[WEB] Navigating to {url}... ");
            cdp.NavigateAsync(url).GetAwaiter().GetResult();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK");
            Console.ResetColor();
        }

        // Print page info
        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        var pageUrl = cdp.GetUrlAsync().GetAwaiter().GetResult();
        Console.WriteLine($"[WEB] Title: {title}");
        Console.WriteLine($"[WEB] URL:   {pageUrl}");

        return 0;
    }

    // ── navigate ─────────────────────────────────────────────────

    static int WebNavigateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web navigate <url> [--port N]");

        int port = GetPort(args);
        string url = args[0];

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Navigating to {url}... ");
        cdp.NavigateAsync(url).GetAwaiter().GetResult();

        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();
        Console.WriteLine($"[WEB] Title: {title}");

        return 0;
    }

    // ── eval ─────────────────────────────────────────────────────

    static int WebEvalCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web eval <expression> [--port N]");

        int port = GetPort(args);
        // Join all non-flag args as the expression (allows spaces)
        var expression = string.Join(" ", args.TakeWhile(a => a != "--port"));

        using var cdp = ConnectCdp(port);

        var result = cdp.EvalAsync(expression).GetAwaiter().GetResult();
        Console.WriteLine($"[WEB] eval: {result}");

        return 0;
    }

    // ── click ────────────────────────────────────────────────────

    static int WebClickCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web click <css-selector> [--port N]");

        int port = GetPort(args);
        string selector = args[0];

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Click '{selector}'... ");
        cdp.ClickAsync(selector).GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        return 0;
    }

    // ── type ─────────────────────────────────────────────────────

    static int WebTypeCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot web type <css-selector> <text> [--port N]");

        int port = GetPort(args);
        string selector = args[0];
        // Join remaining args before --port as the text
        var textArgs = args.Skip(1).TakeWhile(a => a != "--port").ToArray();
        string text = string.Join(" ", textArgs);

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Type '{text}' into '{selector}'... ");
        cdp.TypeAsync(selector, text).GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        return 0;
    }

    // ── text ─────────────────────────────────────────────────────

    static int WebGetTextCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web text <css-selector> [--port N]");

        int port = GetPort(args);
        string selector = args[0];

        using var cdp = ConnectCdp(port);

        var text = cdp.GetTextAsync(selector).GetAwaiter().GetResult();
        Console.WriteLine($"[WEB] text('{selector}'): {text}");

        return 0;
    }

    // ── screenshot ───────────────────────────────────────────────

    static int WebScreenshotCommand(string[] args)
    {
        int port = GetPort(args);
        string output = GetArgValue(args, "-o")
            ?? Path.Combine(DataDir, "output", $"web_screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Screenshot... ");
        var png = cdp.ScreenshotAsync().GetAwaiter().GetResult();

        var dir = Path.GetDirectoryName(output);
        if (dir != null && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllBytes(output, png);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Saved ({png.Length:N0} bytes)");
        Console.ResetColor();
        Console.WriteLine($"[WEB] File: {output}");

        return 0;
    }

    // ── wait ─────────────────────────────────────────────────────

    static int WebWaitCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web wait <css-selector> [--timeout N] [--port N]");

        int port = GetPort(args);
        string selector = args[0];
        int timeout = int.TryParse(GetArgValue(args, "--timeout"), out var t) ? t : 5000;

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Waiting for '{selector}' (timeout={timeout}ms)... ");
        var found = cdp.WaitForElementAsync(selector, timeout).GetAwaiter().GetResult();

        if (found)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Found");
            Console.ResetColor();
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NOT FOUND (timeout)");
            Console.ResetColor();
            return 1;
        }
    }

    // ── check (checkbox) ─────────────────────────────────────────

    static int WebCheckCommand(string[] args)
    {
        if (args.Length < 1)
            return Error("Usage: wkappbot web check <css-selector> [true|false] [--port N]");

        int port = GetPort(args);
        string selector = args[0];
        bool check = args.Length > 1 && args[1] != "--port" ? bool.Parse(args[1]) : true;

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] SetChecked '{selector}' = {check}... ");
        cdp.SetCheckedAsync(selector, check).GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        return 0;
    }

    // ── select ───────────────────────────────────────────────────

    static int WebSelectCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot web select <css-selector> <value-or-text> [--port N]");

        int port = GetPort(args);
        string selector = args[0];
        string value = string.Join(" ", args.Skip(1).TakeWhile(a => a != "--port"));

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Select '{selector}' = '{value}'... ");
        cdp.SelectAsync(selector, value).GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        return 0;
    }

    // ── html ─────────────────────────────────────────────────────

    static int WebHtmlCommand(string[] args)
    {
        int port = GetPort(args);
        string? output = GetArgValue(args, "-o");

        using var cdp = ConnectCdp(port);

        var html = cdp.GetHtmlAsync().GetAwaiter().GetResult();

        if (output != null)
        {
            File.WriteAllText(output, html);
            Console.WriteLine($"[WEB] HTML saved to {output} ({html?.Length ?? 0:N0} chars)");
        }
        else
        {
            Console.WriteLine(html);
        }

        return 0;
    }

    // ── url ──────────────────────────────────────────────────────

    static int WebUrlCommand(string[] args)
    {
        int port = GetPort(args);
        using var cdp = ConnectCdp(port);

        var url = cdp.GetUrlAsync().GetAwaiter().GetResult();
        Console.WriteLine($"[WEB] URL: {url}");

        return 0;
    }

    // ── title ────────────────────────────────────────────────────

    static int WebTitleCommand(string[] args)
    {
        int port = GetPort(args);
        using var cdp = ConnectCdp(port);

        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        Console.WriteLine($"[WEB] Title: {title}");

        return 0;
    }

    // ── close ────────────────────────────────────────────────────

    static int WebCloseCommand(string[] args)
    {
        int port = GetPort(args);

        try
        {
            using var cdp = ConnectCdp(port);
            cdp.DisconnectAsync().GetAwaiter().GetResult();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[WEB] Disconnected from port {port}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WEB] Not connected or already closed: {ex.Message}");
            Console.ResetColor();
        }

        return 0;
    }

    // ── status ───────────────────────────────────────────────────

    static int WebStatusCommand(string[] args)
    {
        int port = GetPort(args);

        Console.Write($"[WEB] Checking CDP on port {port}... ");
        var active = ChromeLauncher.IsPortActiveAsync(port).GetAwaiter().GetResult();

        if (active)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ACTIVE");
            Console.ResetColor();

            // Show connected tabs
            try
            {
                using var http = new System.Net.Http.HttpClient();
                var json = http.GetStringAsync($"http://localhost:{port}/json").GetAwaiter().GetResult();
                var targets = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonArray>(json);
                if (targets != null)
                {
                    Console.WriteLine($"[WEB] Tabs: {targets.Count}");
                    int idx = 0;
                    foreach (var target in targets)
                    {
                        var type = target?["type"]?.GetValue<string>();
                        var tabTitle = target?["title"]?.GetValue<string>();
                        var tabUrl = target?["url"]?.GetValue<string>();
                        Console.WriteLine($"  [{idx++}] ({type}) {tabTitle}");
                        Console.WriteLine($"       {tabUrl}");
                    }
                }
            }
            catch { }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("INACTIVE");
            Console.ResetColor();
        }

        return 0;
    }

    // ── run (batch) ──────────────────────────────────────────────

    static int WebRunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web run <steps-file.txt> [--port N] [--delay N]");

        string filePath = args[0];
        int port = GetPort(args);
        int delayMs = int.TryParse(GetArgValue(args, "--delay"), out var d) ? d : 300;

        if (!File.Exists(filePath))
            return Error($"File not found: {filePath}");

        var lines = File.ReadAllLines(filePath)
            .Where(l => !string.IsNullOrWhiteSpace(l) && !l.TrimStart().StartsWith('#'))
            .ToArray();

        Console.WriteLine($"[WEB] Running {lines.Length} steps from {Path.GetFileName(filePath)} (delay={delayMs}ms)");
        Console.WriteLine();

        int passed = 0, failed = 0;
        var sw = Stopwatch.StartNew();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            var stepArgs = SplitArgs(line);
            if (stepArgs.Length == 0) continue;

            // Inject --port if not already present
            if (!stepArgs.Contains("--port"))
                stepArgs = [.. stepArgs, "--port", port.ToString()];

            Console.Write($"  [{i + 1}/{lines.Length}] {line} ... ");

            try
            {
                var sub = stepArgs[0].ToLowerInvariant();
                var subRest = stepArgs.Skip(1).ToArray();

                int result = sub switch
                {
                    "open" => WebOpenCommand(subRest),
                    "eval" => WebEvalCommand(subRest),
                    "click" => WebClickCommand(subRest),
                    "type" => WebTypeCommand(subRest),
                    "text" => WebGetTextCommand(subRest),
                    "screenshot" => WebScreenshotCommand(subRest),
                    "navigate" or "nav" => WebNavigateCommand(subRest),
                    "wait" => WebWaitCommand(subRest),
                    "check" => WebCheckCommand(subRest),
                    "select" => WebSelectCommand(subRest),
                    "html" => WebHtmlCommand(subRest),
                    "url" => WebUrlCommand(subRest),
                    "title" => WebTitleCommand(subRest),
                    "close" => WebCloseCommand(subRest),
                    "status" => WebStatusCommand(subRest),
                    _ => Error($"Unknown step: {sub}")
                };

                if (result == 0) passed++;
                else failed++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"FAIL: {ex.Message}");
                Console.ResetColor();
                failed++;
            }

            if (delayMs > 0 && i < lines.Length - 1)
                Thread.Sleep(delayMs);
        }

        Console.WriteLine();
        Console.WriteLine($"[WEB] Done: {passed} passed, {failed} failed ({sw.ElapsedMilliseconds}ms)");

        return failed > 0 ? 1 : 0;
    }

    /// <summary>Split a command line string into args (respects quotes).</summary>
    static string[] SplitArgs(string line)
    {
        var args = new List<string>();
        var current = new System.Text.StringBuilder();
        bool inQuote = false;
        char quoteChar = '"';

        foreach (char c in line)
        {
            if (inQuote)
            {
                if (c == quoteChar)
                    inQuote = false;
                else
                    current.Append(c);
            }
            else if (c == '"' || c == '\'')
            {
                inQuote = true;
                quoteChar = c;
            }
            else if (c == ' ')
            {
                if (current.Length > 0)
                {
                    args.Add(current.ToString());
                    current.Clear();
                }
            }
            else
            {
                current.Append(c);
            }
        }

        if (current.Length > 0)
            args.Add(current.ToString());

        return args.ToArray();
    }
}
