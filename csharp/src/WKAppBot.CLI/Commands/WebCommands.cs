using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

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

        // Auto-launch AppBotEye for all CDP commands (not just "open")
        // "open" handles it internally, help/status don't need it
        if (sub != "open" && sub != "--help" && sub != "-h" && sub != "help"
            && sub != "status" && sub != "fetch" && sub != "search" && sub != "read")
        {
            // Parse port from args (--port N pattern)
            int mvPort = 9222;
            for (int i = 0; i < restArgs.Length; i++)
                if (restArgs[i] == "--port" && i + 1 < restArgs.Length)
                    int.TryParse(restArgs[i + 1], out mvPort);
            LaunchAppBotEyeIfNeeded(mvPort);
        }

        // Removed (use a11y instead): eval, click, dblclick, type, text, screenshot, wait, check, select, restore/show
        // Migration: a11y <action> "*Chrome*#<css-selector>" — CSS auto-routed to CDP
        return sub switch
        {
            "open" => WebOpenCommand(restArgs),
            "tabs" => WebTabsCommand(restArgs),
            "capture" => WebCaptureCommand(restArgs),
            "navigate" or "nav" => WebNavigateCommand(restArgs),
            "html" => WebHtmlCommand(restArgs),
            "url" => WebUrlCommand(restArgs),
            "title" => WebTitleCommand(restArgs),
            "close" => WebCloseCommand(restArgs),
            "status" => WebStatusCommand(restArgs),
            "run" => WebRunCommand(restArgs),
            "file"   => WebFileInputCommand(restArgs),
            "fetch"  => WebFetchCommand(restArgs),
            "search" => WebSearchCommand(restArgs),
            "read"   => WebReadCommand(restArgs),
            "--help" or "-h" or "help" => WebUsage(),
            "eval" or "click" or "dblclick" or "double-click" or "type" or "text"
                or "screenshot" or "wait" or "check" or "select" or "restore" or "show"
                => Error($"[WEB] 'web {sub}' removed — use a11y instead:\n"
                       + $"  a11y {MapRemovedWebCmd(sub)} \"*Chrome*#<css-selector>\" [args]\n"
                       + $"  CSS selectors auto-routed to CDP (no --port needed)"),
            _ => Error($"Unknown web subcommand: {sub}")
        };
    }

    static string MapRemovedWebCmd(string sub) => sub switch
    {
        "click" or "dblclick" or "double-click" => "click",
        "type" => "type",
        "text" => "read",
        "screenshot" => "screenshot",
        "wait" => "wait",
        "check" => "click",
        "select" => "select",
        "restore" or "show" => "restore",
        "eval" => "read --eval-js",
        _ => sub
    };

    static int WebUsage()
    {
        Console.WriteLine(@"
WKAppBot WebBot — Chrome DevTools Protocol web automation

Usage:
  wkappbot web <subcommand> [options]

Session Management:
  open [url] [--port N] [--headless] [--no-launch] [--browser] [--no-minimize] [--show]
      Open Chrome with CDP and navigate to URL.
      --port N: CDP port (default: 9222)
      --headless: Run Chrome headless (no visible window)
      --no-launch: Connect to already-running Chrome (don't launch)
      --browser: Normal Chrome UI with address bar/tabs (for debugging)
      --no-minimize / --show: Keep Chrome window visible (default: minimize)
  close [--port N]
      Disconnect from Chrome (does not close the browser).
  status [--port N]
      Check if Chrome CDP is active on a port.

Navigation:
  navigate <url> [--port N]    Navigate current tab to URL.
  url [--port N]               Get current page URL.
  title [--port N]             Get current page title.

Removed — use a11y instead (CSS auto-routed to CDP):
  click <css>      →  a11y click ""*Chrome*#<css>""
  type <css> <txt> →  a11y type  ""*Chrome*#<css>"" ""text""
  text <css>       →  a11y read  ""*Chrome*#<css>""
  screenshot       →  a11y screenshot ""*Chrome*""
  wait <css>       →  a11y wait  ""*Chrome*#<css>""
  check/select     →  a11y click/select ""*Chrome*#<css>""
  restore/show     →  a11y restore ""*Chrome*""
  eval <js>        →  a11y read  ""*Chrome*"" --eval-js ""<js>""

Output:
  capture [-o output.png] [--port N]
      Capture Chrome window including title bar (Win32 PrintWindow).
  html [<url>] [-o out.html] [--port N]
      Get full page HTML source. If <url> given, navigates to it first (sandbox tab).

Tab Discovery:
  tabs [--port N]
      List all Chrome tabs (title + URL).

Batch:
  run <steps-file.txt> [--port N] [--delay N]
      Run a batch of web commands from a file.
      Each line is a web subcommand (open/navigate/html/capture/url/title/close).

Fetch / Search:
  fetch <url> [--max-chars N]   HTTP GET + extract text content.
  search <query> [--limit N]    Web search.
  read <url> [--max-chars N]    Article reader (main content extraction).

Options:
  --port N   CDP port (default: 9222)
  -o <file>  Output file path (for capture/html)
");
        PrintRelatedSkills("web");
        return 0;
    }

    // ── Helper: connect to CDP ───────────────────────────────────

    static int GetPort(string[] args) =>
        int.TryParse(GetArgValue(args, "--port"), out var p) ? p : 9222;

    /// <summary>
    /// Compute a short session tag from the caller's CWD.
    /// Same CWD (= same project) → same hash → same Chrome tab across CLI invocations.
    /// In Eye mode: CWD is passed by Launcher via pipe (AsyncLocal CallerCwd).
    /// Direct mode: falls back to Environment.CurrentDirectory.
    /// This ensures parallel sessions from different projects get isolated tabs.
    /// </summary>
    static string? GetSessionTag()
    {
        try
        {
            // Eye mode: use CWD passed by Launcher (AsyncLocal, per-command)
            // Direct mode: use process CWD
            var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(cwd.ToLowerInvariant()));
            return Convert.ToHexString(hash, 0, 4).ToLowerInvariant(); // 8 hex chars
        }
        catch { return null; }
    }

    static CdpClient ConnectCdp(int port, bool withBar = false, bool verifyWebBot = false, bool ensureWindow = true, string? navigateUrl = null)
    {
        var cdp = new CdpClient();
        // Prefer domain-based tab lookup over title (titles are unstable — GPT changes them)
        string? domainHint = null;
        try { if (!string.IsNullOrWhiteSpace(navigateUrl)) domainHint = new Uri(navigateUrl).Host; } catch { }

        try
        {
            cdp.ConnectAsync(port, preferredTargetTag: domainHint).GetAwaiter().GetResult();
        }
        catch
        {
            // Auto-launch Chrome WebBot if not running
            Console.Error.WriteLine($"[WEB] CDP port {port} not available — launching Chrome...");
            var proc = WKAppBot.WebBot.ChromeLauncher.LaunchAsync(port, navigateUrl).GetAwaiter().GetResult();
            if (proc != null)
                Console.Error.WriteLine($"[WEB] Chrome launched (pid={proc.Id})");
            cdp.ConnectAsync(port, preferredTargetTag: domainHint).GetAwaiter().GetResult();
        }

        // Ensure we're connected to this session's tab in the correctly-positioned window.
        // When navigateUrl is provided: use sandbox key "web+{host}+{hwnd:X8}" — guaranteed isolated tab per host+HWND.
        // When no navigateUrl: legacy "web-{cwdHash}" path (general web browsing, no host constraint).
        if (ensureWindow)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(navigateUrl))
                {
                    // Sandboxed: web+{host}+{hwnd} key → GetOrCreateSandboxedTabAsync
                    var host = new Uri(navigateUrl).Host;
                    var sandboxKey = BuildSandboxKey("web", host);
                    var targetId = Task.Run(() => GetOrCreateSandboxedTabAsync(cdp, port, sandboxKey, host))
                                       .GetAwaiter().GetResult();
                    if (targetId != null && targetId != cdp.TargetId)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Error.WriteLine($"[WEB] Sandboxed tab (key={sandboxKey}, target={targetId[..Math.Min(8,targetId.Length)]})");
                        Console.ResetColor();
                    }
                }
                else
                {
                    // Legacy: general web tab by cwdHash (no host constraint)
                    var sessionTag = GetSessionTag();
                    var targetName = $"web-{sessionTag ?? "default"}";
                    var targetId = cdp.EnsureCorrectWindowAsync(port, targetName, "about:blank").GetAwaiter().GetResult();
                    if (targetId != null && targetId != cdp.TargetId)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Error.WriteLine($"[WEB] Switched to WebBot tab (tag={sessionTag ?? "?"}, target={targetId?[..8]})");
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Error.WriteLine($"[WEB] Window check skipped: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Verify we're connected to a WebBot-managed Chrome.
        // CDP port-based connection is the primary guard; title check is advisory only
        // (bar injection may still be pending when this runs).
        if (verifyWebBot)
        {
            var title = cdp.GetTitleAsync().GetAwaiter().GetResult() ?? "";
            if (!title.Contains("WKWebBot"))
                Console.Error.WriteLine("[WEB] WARN: WKWebBot bar not yet in title (run 'web open <url>' first if not done)");
        }

        return cdp;
    }

    // ── Web Target: renderer HWND-based tab identity ─────────────────────────
    // Each Chrome tab has its own Chrome_RenderWidgetHostHWND (child window, renderer process).
    // This HWND is the most reliable per-tab identifier:
    //   - hwnd:XXXXXXXX works in windows/a11y/inspect via WindowFinder direct lookup
    //   - web commands accept hwnd:XXXXXXXX to reconnect to the exact tab
    //
    // In-memory cache: HWND(hex) → (port, targetId, url)
    static readonly Dictionary<string, (int port, string targetId, string url)> _webHwndCache = new();

    /// <summary>Find Chrome_RenderWidgetHostHWND for the current CDP tab.</summary>
    static IntPtr GetRenderWidgetHwnd(CdpClient cdp)
    {
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero) return IntPtr.Zero;
        IntPtr renderHwnd = IntPtr.Zero;
        NativeMethods.EnumChildWindows(chromeHwnd, (hwnd, _) =>
        {
            var sb = new System.Text.StringBuilder(128);
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_RenderWidgetHostHWND") return true;
            // Match renderer PID to this tab's targetId via CDP process list
            // Best effort: use the first visible render widget (active tab)
            NativeMethods.GetWindowRect(hwnd, out var r);
            if (r.Right - r.Left > 10 && r.Bottom - r.Top > 10)
            {
                renderHwnd = hwnd;
                return false;
            }
            return true;
        }, IntPtr.Zero);
        return renderHwnd != IntPtr.Zero ? renderHwnd : chromeHwnd;
    }

    /// <summary>
    /// Save renderHwnd → (port, targetId, url) to cache and print the TARGET line.
    /// Output: [WEB] TARGET: hwnd:XXXXXXXX  ← this string re-attaches to this exact tab
    /// </summary>
    static void PrintWebTarget(CdpClient cdp, int port)
    {
        try
        {
            var renderHwnd = GetRenderWidgetHwnd(cdp);
            var targetId   = cdp.TargetId ?? "";
            var url        = cdp.GetUrlAsync().GetAwaiter().GetResult() ?? "";

            // Cache mapping in memory so future commands can reconnect by HWND
            if (renderHwnd != IntPtr.Zero)
            {
                var key = renderHwnd.ToInt64().ToString("X8");
                _webHwndCache[key] = (port, targetId, url);
                // Prune stale entries (HWND no longer valid)
                foreach (var k in _webHwndCache.Keys.ToList())
                    if (k != key && IntPtr.TryParse(k, System.Globalization.NumberStyles.HexNumber, null, out var hh) && !NativeMethods.IsWindow(hh))
                        _webHwndCache.Remove(k);
            }

            // Prefer render widget HWND (tab-isolated PID); fall back to chrome root window
            var targetHwnd = renderHwnd != IntPtr.Zero ? renderHwnd : cdp.ChromeWindowHandle;
            var cdpTitle   = cdp.GetTitleAsync().GetAwaiter().GetResult() ?? "";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine($"[WEB] TARGET: {WindowFinder.BuildTargetJson5(targetHwnd, cdpTitle)}");
            Console.ResetColor();
        }
        catch { }
    }

    /// <summary>
    /// Connect to CDP tab by renderer HWND (from hwnd:XXXXXXXX target string).
    /// Looks up runtime cache → reconnects to exact tab by targetId.
    /// Falls back to URL-based tab search if cache miss.
    /// </summary>
    static CdpClient? ConnectCdpByHwnd(IntPtr renderHwnd)
    {
        var key = renderHwnd.ToInt64().ToString("X8");

        // 1. Try in-memory cache lookup
        if (_webHwndCache.TryGetValue(key, out var cached))
        {
            try
            {
                var cdp = new CdpClient();
                cdp.ConnectAsync(cached.port).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(cached.targetId))
                {
                    var ok = cdp.ConnectToTabAsync(cached.port, cached.targetId).GetAwaiter().GetResult();
                    if (ok) return cdp;
                }
                return cdp; // cache hit but targetId stale — still connected to port
            }
            catch { }
        }

        // 2. Cache miss — walk up to find Chrome browser HWND → CDP port
        try
        {
            var parent = renderHwnd;
            for (int i = 0; i < 10; i++)
            {
                var p = NativeMethods.GetAncestor(parent, 2 /*GA_ROOT*/);
                if (p == parent || p == IntPtr.Zero) break;
                parent = p;
            }
            NativeMethods.GetWindowThreadProcessId(parent, out uint browserPid);
            var cmdLine = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine((int)browserPid) ?? "";
            var m = System.Text.RegularExpressions.Regex.Match(cmdLine, @"--remote-debugging-port=(\d+)");
            if (m.Success && int.TryParse(m.Groups[1].Value, out int port))
            {
                // Find which tab's renderer HWND matches ours via URL from GetBrowserUrl
                var cdp = new CdpClient();
                cdp.ConnectAsync(port).GetAwaiter().GetResult();
                // Try to match by URL (UIA omnibox of target HWND)
                NativeMethods.GetWindowThreadProcessId(renderHwnd, out uint rendPid);
                var url = WKAppBot.Win32.Window.WindowFinder.GetBrowserUrl(renderHwnd, rendPid);
                if (!string.IsNullOrEmpty(url))
                {
                    var ok = cdp.ConnectToTabAsync(port, url).GetAwaiter().GetResult();
                    if (!ok) Console.Error.WriteLine($"[WEB] WARN: tab URL match failed for hwnd:{key}, using default tab");
                }
                return cdp;
            }
        }
        catch { }

        Console.Error.WriteLine($"[WEB] ERROR: Cannot reconnect to hwnd:{key} — no cache entry and no CDP port found");
        return null;
    }

    /// <summary>Connect to CDP and switch to tab matching --tab pattern (if specified).</summary>
    static CdpClient? ConnectCdpWithTab(string[] args, bool withBar = false)
    {
        // hwnd:XXXXXXXX target — reconnect to exact tab by renderer HWND
        var hwndTarget = args.FirstOrDefault(a => a.StartsWith("hwnd:", StringComparison.OrdinalIgnoreCase)
                                                && !a.StartsWith("--"));
        if (hwndTarget != null)
        {
            var hexStr = hwndTarget[5..].TrimStart('0', 'x', 'X');
            if (IntPtr.TryParse(hexStr, System.Globalization.NumberStyles.HexNumber, null, out var renderHwnd))
                return ConnectCdpByHwnd(renderHwnd);
        }

        var port = GetPort(args);
        var tabPattern = GetArgValue(args, "--tab");

        if (string.IsNullOrEmpty(tabPattern))
            return ConnectCdp(port, withBar);

        // Connect to first available tab, then find the right one
        var cdp = new CdpClient();
        cdp.ConnectAsync(port).GetAwaiter().GetResult();

        var matched = cdp.ConnectToTabAsync(port, tabPattern).GetAwaiter().GetResult();
        if (!matched)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[WEB] No tab matching \"{tabPattern}\" found. Use 'wkappbot web tabs' to list.");
            Console.ResetColor();
            cdp.Dispose();
            return null;
        }
        return cdp;
    }

    static int WebTabsCommand(string[] args)
    {
        var port = GetPort(args);
        var cdp = new CdpClient();
        cdp.ConnectAsync(port).GetAwaiter().GetResult();
        var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();
        cdp.Dispose();

        if (tabs.Count == 0)
        {
            Console.WriteLine("[WEB] No tabs found");
            return 1;
        }

        Console.Error.WriteLine($"[WEB] {tabs.Count} tab(s):");
        foreach (var tab in tabs)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  {tab.Title}");
            Console.ResetColor();
            Console.WriteLine($"  {tab.Url}  [{tab.Id[..8]}]");
        }
        return 0;
    }

    // ── open ─────────────────────────────────────────────────────
    // ★ IMPORTANT: App mode (--app) is the DEFAULT and MUST stay that way!
    //   - App mode creates an isolated Chrome window: no address bar, no tabs, no extensions
    //   - This prevents mixing with user's normal Chrome sessions/profiles
    //   - User's bookmarks, history, and cookies stay completely separate
    //   - WebBot uses its own temp user-data-dir for session isolation
    //   - Use --browser ONLY for debugging (address bar visible, tabs enabled)

    static int WebOpenCommand(string[] args)
    {
        int port = GetPort(args);
        bool headless = args.Contains("--headless");
        bool noLaunch = args.Contains("--no-launch");
        bool appMode = false; // always normal browser UI (tabs + address bar)

        // Get URL (first non-flag argument)
        string? url = args.FirstOrDefault(a => !a.StartsWith("--") && a != "true" && a != "false");

        bool freshLaunch = false;
        Console.Write($"[WEB] ");

        if (!noLaunch)
        {
            Console.Write($"Launching Chrome (port={port}){(appMode ? " [APP mode]" : "")}... ");
            // In app mode: pass actual URL to --app flag so Chrome opens clean window with it
            //   (navigating away from --app=about:blank re-enables Chrome UI)
            // In normal mode: pass about:blank, navigate later via CDP
            var process = ChromeLauncher.LaunchAsync(port, url, headless).GetAwaiter().GetResult();
            if (process == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Chrome already running on port {port}");
                Console.ResetColor();
            }
            else
            {
                freshLaunch = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Chrome started (PID={process.Id})");
                Console.ResetColor();
            }
        }

        // Connect via CDP — pass url so sandboxed tab reuse path is used (prevents tab accumulation)
        Console.Write($"[WEB] Connecting via CDP... ");
        using var cdp = ConnectCdp(port, navigateUrl: url);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Connected ({cdp.WebSocketUrl})");
        Console.ResetColor();

        // Navigate to URL — always via CDP NavigateAsync so WebBot bar gets injected
        if (!string.IsNullOrEmpty(url))
        {
            if (freshLaunch)
            {
                // Wait for Chrome renderer to be responsive instead of fixed 1-2s sleep.
                // Runtime.enable is already done by ConnectCdp; verify eval works.
                var rdySw = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < 20; i++) // max 2s (20 × 100ms)
                {
                    try
                    {
                        var rs = cdp.EvalAsync("document.readyState").GetAwaiter().GetResult();
                        if (!string.IsNullOrEmpty(rs)) break;
                    }
                    catch { }
                    Thread.Sleep(100);
                }
                Console.Error.WriteLine($"[WEB] Chrome renderer ready in {rdySw.ElapsedMilliseconds}ms");
            }

            {
                Console.Write($"[WEB] Navigating to {url}... ");
                cdp.NavigateAsync(url).GetAwaiter().GetResult();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK");
            Console.ResetColor();
        }

        // Print page info
        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        var pageUrl = cdp.GetUrlAsync().GetAwaiter().GetResult();
        // Resolve Chrome window handle for reliable targeting
        var chromeHwnd = cdp.GetChromeWindowHandle();
        cdp.ChromeWindowHandle = (nint)chromeHwnd;
        var tabId = cdp.TargetId ?? "";

        Console.Error.WriteLine($"[WEB] Title: {title}");
        Console.Error.WriteLine($"[WEB] URL:   {pageUrl}");
        Console.Error.WriteLine($"[WEB] TabID: {tabId}");
        PrintWebTarget(cdp, port);

        // Verify this is our WebBot window (CDP port-based connection is already sufficient;
        // title check can fail due to async bar injection timing — warn only, don't abort).
        if (title == null || !title.Contains("WKWebBot"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WEB] WARN: WKWebBot bar not yet in title (injection may still be pending) — continuing.");
            Console.ResetColor();
        }

        // Minimize Chrome window (unless --no-minimize)
        if (!args.Contains("--no-minimize") && !args.Contains("--show"))
        {
            cdp.MinimizeChromeWindow();
            Console.WriteLine("[WEB] Chrome minimized (CDP works in background)");
        }
        else
        {
            Console.WriteLine("[WEB] Chrome window visible");
        }

        // Show past knowhow for this domain
        if (pageUrl != null)
            ShowDomainKnowhow(pageUrl);

        // Auto-launch AppBotEye overlay if not already running
        LaunchAppBotEyeIfNeeded(port);

        return 0;
    }

    // ── navigate ─────────────────────────────────────────────────

    static int WebNavigateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot web navigate <url> [--port N]");

        int port = GetPort(args);
        string url = args[0];

        using var cdp = ConnectCdp(port, navigateUrl: url);

        Console.Write($"[WEB] Navigating to {url}... ");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        cdp.NavigateAsync(url).GetAwaiter().GetResult();

        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        var pageUrl = cdp.GetUrlAsync().GetAwaiter().GetResult();
        var chromeHwnd = cdp.GetChromeWindowHandle();
        cdp.ChromeWindowHandle = (nint)chromeHwnd;
        var tabId = cdp.TargetId ?? "";
        cdp.UpdateStatusAsync($"Navigate: {url}", elapsedMs: (int)sw.ElapsedMilliseconds).GetAwaiter().GetResult();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();
        Console.Error.WriteLine($"[WEB] Title: {title}");
        Console.Error.WriteLine($"[WEB] URL:   {pageUrl}");
        Console.Error.WriteLine($"[WEB] TabID: {tabId}");
        PrintWebTarget(cdp, port);

        // Show past knowhow for this domain
        ShowDomainKnowhow(url);

        // ── ActionState IPC ──
        try { ActionState.Write(new ActionState { Source = "web", WebUrl = url, WebTitle = title, ActionName = "navigate", ActionDetail = $"Navigate: {url}", Status = "pass" }); } catch { }

        return 0;
    }


    // ── capture (Win32 window — includes title bar) ─────────────

    static int WebCaptureCommand(string[] args)
    {
        int port = GetPort(args);
        string output = GetArgValue(args, "-o")
            ?? Path.Combine(DataDir, "output", $"web_capture_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        using var cdp = ConnectCdp(port);

        Console.Write($"[WEB] Window capture (PID={cdp.ChromePid})... ");
        var hwnd = cdp.GetChromeWindowHandle();

        if (hwnd == IntPtr.Zero)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAILED (Chrome window not found)");
            Console.ResetColor();
            return 1;
        }

        // Use Win32 ScreenCapture (PrintWindow) — captures title bar + chrome
        var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hwnd);
        if (bmp == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAILED (PrintWindow returned null)");
            Console.ResetColor();
            return 1;
        }

        var dir = Path.GetDirectoryName(output);
        if (dir != null && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        bmp.Save(output, System.Drawing.Imaging.ImageFormat.Png);
        var fileInfo = new FileInfo(output);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Saved ({fileInfo.Length:N0} bytes, {bmp.Width}x{bmp.Height})");
        Console.ResetColor();
        Console.Error.WriteLine($"[WEB] File: {output}");
        bmp.Dispose();

        return 0;
    }

    // ── html ─────────────────────────────────────────────────────

    static int WebHtmlCommand(string[] args)
    {
        int port = GetPort(args);
        string? output = GetArgValue(args, "-o");
        string? url = args.FirstOrDefault(a => !a.StartsWith("-") && (a.StartsWith("http://") || a.StartsWith("https://")));

        // Pass url as navigateUrl: ensures a dedicated web tab, never steals an AI chat tab
        using var cdp = ConnectCdp(port, withBar: false, navigateUrl: url);

        if (url != null)
        {
            Console.Error.WriteLine($"[HTML] Navigating: {url}");
            cdp.NavigateAsync(url).GetAwaiter().GetResult();
        }

        var html = cdp.GetHtmlAsync().GetAwaiter().GetResult();

        if (output != null)
        {
            File.WriteAllText(output, html);
            Console.Error.WriteLine($"[WEB] HTML saved to {output} ({html?.Length ?? 0:N0} chars)");
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
        Console.Error.WriteLine($"[WEB] URL: {url}");

        return 0;
    }

    // ── title ────────────────────────────────────────────────────

    static int WebTitleCommand(string[] args)
    {
        int port = GetPort(args);
        using var cdp = ConnectCdp(port);

        var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
        Console.Error.WriteLine($"[WEB] Title: {title}");

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
            Console.Error.WriteLine($"[WEB] Disconnected from port {port}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[WEB] Not connected or already closed: {ex.Message}");
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
                    Console.Error.WriteLine($"[WEB] Tabs: {targets.Count}");
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

    // ── file (set file input) ──────────────────────────────────

    static int WebFileInputCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot web file <selector> <file-path> [--port N]");

        string selector = args[0];
        string filePath = args[1];
        int port = GetPort(args);

        if (!File.Exists(filePath))
            return Error($"File not found: {filePath}");

        // Convert to absolute path (CDP requires absolute)
        filePath = Path.GetFullPath(filePath);

        Console.Write($"[WEB] Setting file on {selector}... ");

        try
        {
            using var cdp = ConnectCdp(port);
            var ok = cdp.SetFileInputAsync(selector, filePath).GetAwaiter().GetResult();
            if (ok)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"OK ({Path.GetFileName(filePath)})");
                Console.ResetColor();
                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAILED (element not found)");
                Console.ResetColor();
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
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

        Console.Error.WriteLine($"[WEB] Running {lines.Length} steps from {Path.GetFileName(filePath)} (delay={delayMs}ms)");
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

            // Update status bar step counter
            try
            {
                using var stepCdp = ConnectCdp(port);
                stepCdp.UpdateStatusAsync($"Running: {line}", stepNum: i + 1, totalSteps: lines.Length).GetAwaiter().GetResult();
            }
            catch { }

            try
            {
                var sub = stepArgs[0].ToLowerInvariant();
                var subRest = stepArgs.Skip(1).ToArray();

                int result = sub switch
                {
                    "open"          => WebOpenCommand(subRest),
                    "capture"       => WebCaptureCommand(subRest),
                    "navigate" or "nav" => WebNavigateCommand(subRest),
                    "html"          => WebHtmlCommand(subRest),
                    "url"           => WebUrlCommand(subRest),
                    "title"         => WebTitleCommand(subRest),
                    "close"         => WebCloseCommand(subRest),
                    "status"        => WebStatusCommand(subRest),
                    "eval" or "click" or "dblclick" or "double-click" or "type" or "text"
                        or "screenshot" or "wait" or "check" or "select" or "restore" or "show"
                        => Error($"[WEB] 'web {sub}' removed — use a11y in your script: a11y {MapRemovedWebCmd(sub)} \"*Chrome*#<css>\""),
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
        Console.Error.WriteLine($"[WEB] Done: {passed} passed, {failed} failed ({sw.ElapsedMilliseconds}ms)");

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

    /// <summary>
    /// Show domain knowhow when navigating to a URL.
    /// "선배의 메모" — future Claude sessions see past lessons automatically.
    /// </summary>
    static void ShowDomainKnowhow(string url)
    {
        try
        {
            // Extract domain from URL
            string domain;
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                domain = uri.Host;
            else if (url.Contains('.'))
                domain = url.Split('/')[0]; // bare domain like "naver.com"
            else
                return;

            if (string.IsNullOrEmpty(domain)) return;

            // Read site-level knowhow (not element-level — too verbose)
            var sitePath = Path.Combine(WebProfilesDir, WebKnowhow.SanitizeDomain(domain), "knowhow.md");
            if (!File.Exists(sitePath)) return;

            var content = File.ReadAllText(sitePath).Trim();
            if (string.IsNullOrEmpty(content)) return;

            // Count entries (## headers)
            var entryCount = content.Split('\n').Count(l => l.StartsWith("## "));

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[KNOWHOW] {domain} — {entryCount} note(s) from past sessions:");
            Console.ResetColor();

            // Show compact summary: each ## header as bullet
            foreach (var line in content.Split('\n'))
            {
                if (line.StartsWith("## "))
                {
                    // Strip timestamp: "## [2026-02-21 03:18:45] category" → "category"
                    var header = line[3..].Trim();
                    var bracketEnd = header.IndexOf(']');
                    if (bracketEnd > 0 && header.StartsWith('['))
                        header = header[(bracketEnd + 1)..].Trim();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("  * ");
                    Console.ResetColor();
                    Console.WriteLine(header);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  read:  wkappbot knowhow web-read {domain}");
            Console.WriteLine($"  add:   wkappbot knowhow web {domain} \"lesson\" [--category \"...\"]");
            Console.WriteLine($"  file:  {sitePath}");
            Console.ResetColor();
        }
        catch { /* best-effort */ }
    }
}
