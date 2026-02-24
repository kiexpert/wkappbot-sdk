using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot eye [--port N] [--interval N] [--size WxH] [--pos X,Y]
/// Opens "AppBot Eye" overlay — a semi-transparent window showing WebBot's live view.
/// Auto-positions at the top-right corner of the Claude Desktop window when possible.
///
/// Entry point + Web Mode (CDP) + P/Invoke declarations.
/// App Mode loop: AppBotEyeAppMode.cs
/// Claude Desktop detection: AppBotEyeClaudeDetector.cs
/// App Mode helpers: AppBotEyeAppHelpers.cs
/// </summary>
internal partial class Program
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;

    static void TryHideConsoleWindow()
    {
        try
        {
            var h = GetConsoleWindow();
            if (h != IntPtr.Zero)
                ShowWindow(h, SW_HIDE);
        }
        catch { }
    }

    static int AppBotEyeCommand(string[] args)
    {
        // one-shot diagnostic tick (must not enter global loop)
        if (args.Length > 0 && string.Equals(args[0], "tick", StringComparison.OrdinalIgnoreCase))
            return EyeTickCommand(args.Skip(1).ToArray());

        // Parse arguments
        int port = 9222;
        int intervalMs = 100; // ~10 fps target (actual: limited by CDP screenshot speed)
        int width = 320, height = 220;
        int posX = -1, posY = -1;
        string? appTitle = null;   // --app or --title: match window title
        string? appProcess = null; // --process: match process name
        // Slack is ALWAYS ON — no option to disable
        bool slackMode = true;
        bool legacyMode = args.Any(a => string.Equals(a, "--legacy", StringComparison.OrdinalIgnoreCase));
        bool globalMode = !legacyMode;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--port" when i + 1 < args.Length:
                    int.TryParse(args[++i], out port);
                    break;
                case "--interval" when i + 1 < args.Length:
                    int.TryParse(args[++i], out intervalMs);
                    break;
                case "--size" when i + 1 < args.Length:
                    var sizeParts = args[++i].Split('x', 'X');
                    if (sizeParts.Length == 2)
                    {
                        int.TryParse(sizeParts[0], out width);
                        int.TryParse(sizeParts[1], out height);
                    }
                    break;
                case "--pos" when i + 1 < args.Length:
                    var posParts = args[++i].Split(',');
                    if (posParts.Length == 2)
                    {
                        int.TryParse(posParts[0], out posX);
                        int.TryParse(posParts[1], out posY);
                    }
                    break;
                case "--app" when i + 1 < args.Length:
                case "--title" when i + 1 < args.Length:
                    appTitle = args[++i];
                    break;
                case "--process" when i + 1 < args.Length:
                    appProcess = args[++i];
                    break;
            }
        }

        // Global single-window monitor mode (multi-parent cards) — default mode
        if (globalMode)
        {
            TryHideConsoleWindow();
            Console.WriteLine("[EYE] Global mode enabled (default)");
            return EyeGlobalPollingLoop(width, height, posX, posY, intervalMs);
        }

        // Mode detection: app mode vs web mode
        bool isAppMode = !string.IsNullOrEmpty(appTitle) || !string.IsNullOrEmpty(appProcess);

        Console.WriteLine($"[EYE] Starting AppBot Eye ({width}x{height}, interval={intervalMs}ms)" +
            $"{(isAppMode ? $" [App: {appTitle ?? appProcess}]" : "")}" +
            " [Slack+Prompt]");

        // Find Claude Desktop window for auto-placement at top-right corner
        IntPtr claudeHwnd = IntPtr.Zero;
        if (posX < 0 && posY < 0) // only auto-place if no explicit --pos
        {
            claudeHwnd = FindClaudeDesktopWindow();
            if (claudeHwnd != IntPtr.Zero)
            {
                if (GetWindowRect(claudeHwnd, out var rect))
                {
                    // Place at the top-right corner of Claude Desktop, inside the window
                    posX = rect.Right - width - 8; // 8px inset from right edge
                    posY = rect.Top + 40;           // below title bar (~40px)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[EYE] Found Claude Desktop at ({rect.Left},{rect.Top} {rect.Right - rect.Left}x{rect.Bottom - rect.Top})");
                    Console.WriteLine($"[EYE] Auto-placing at ({posX},{posY}) — top-right corner");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("[EYE] Claude Desktop not found — using default position");
            }
        }

        // ── Unified Mode (default): ActionState IPC + cursor fallback ──
        // Also handles --app/--process for explicit target window
        // Web Mode (CDP) is only used with explicit --port flag
        if (isAppMode || !args.Any(a => a == "--port"))
            return AppModePollingLoop(appTitle, appProcess, width, height, posX, posY,
                claudeHwnd, intervalMs, slackMode);

        // ── Web Mode (legacy): Chrome/WebBot CDP — only with explicit --port ──

        // Connect to CDP
        CdpClient? cdp = null;
        try
        {
            cdp = new CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();
            // Enable Page domain (needed for captureScreenshot to work reliably)
            cdp.EvalAsync("1").GetAwaiter().GetResult(); // warm up
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[EYE] Connected to WebBot Chrome (port {port})");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[EYE] Cannot connect to Chrome on port {port}: {ex.Message}");
            Console.WriteLine($"[EYE] Run 'wkappbot web open <url>' first to launch WebBot Chrome.");
            Console.ResetColor();
            return 1;
        }

        // Find Chrome window handle for click-to-restore
        var chromeHwnd = FindChromeWindow(cdp.ChromePid);

        // Start overlay — attach to Claude Desktop window as owner if found
        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, claudeHwnd);

        if (chromeHwnd != IntPtr.Zero)
            host.SetChromeHwnd(chromeHwnd);

        Console.WriteLine($"[EYE] Overlay active — press Ctrl+C to stop");

        // Handle Ctrl+C gracefully
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        // Hot-reload: track EXE file timestamp at startup
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        var exeStartTime = File.Exists(exePath) ? File.GetLastWriteTimeUtc(exePath) : DateTime.MinValue;

        // Polling loop: screenshot → update overlay
        int frameCount = 0;
        var sw = Stopwatch.StartNew();
        string? lastUrl = null;

        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            try
            {
                var frameSw = Stopwatch.StartNew();

                // Capture screenshot via CDP (short timeout — skip if CDP busy with navigate)
                byte[]? png = null;
                try
                {
                    var screenshotTask = cdp.ScreenshotAsync();
                    if (screenshotTask.Wait(3000))
                        png = screenshotTask.Result;
                    // else: timeout → skip this frame silently
                }
                catch { /* skip frame on error */ }

                if (png != null && png.Length > 0)
                {
                    host.UpdateScreenshot(png);
                    frameCount++;
                }

                // Get URL + accessibility info periodically (every 10 frames)
                if (frameCount % 10 == 1)
                {
                    try
                    {
                        var url = cdp.GetUrlAsync().GetAwaiter().GetResult();
                        if (url != lastUrl)
                        {
                            lastUrl = url;
                            var title = cdp.GetTitleAsync().GetAwaiter().GetResult();
                            host.UpdateInfo(url, title);

                            // Re-find Chrome hwnd if lost
                            if (chromeHwnd == IntPtr.Zero || !IsWindow(chromeHwnd))
                            {
                                chromeHwnd = FindChromeWindow(cdp.ChromePid);
                                if (chromeHwnd != IntPtr.Zero)
                                    host.SetChromeHwnd(chromeHwnd);
                            }
                        }
                    }
                    catch { /* skip URL update if CDP busy */ }

                    // Get page content via UIA (Accessibility) — "장애인의 눈으로 본 웹"
                    try
                    {
                        var axText = ExtractWebContentViaUia(chromeHwnd);
                        if (!string.IsNullOrEmpty(axText))
                            host.UpdateAccessibilityText(axText);
                    }
                    catch { /* skip content update if busy */ }
                }

                // Log stats periodically
                if (frameCount % 100 == 0)
                {
                    var fps = frameCount / sw.Elapsed.TotalSeconds;
                    Console.WriteLine($"[EYE] frame #{frameCount}, {fps:F1} fps, {png?.Length / 1024}KB");
                }

                // Hot-reload: check if EXE was replaced (every ~5 seconds = 10 frames at 500ms)
                if (frameCount % 10 == 5 && exeStartTime != DateTime.MinValue)
                {
                    try
                    {
                        var currentTime = File.GetLastWriteTimeUtc(exePath);
                        if (currentTime != exeStartTime)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("[EYE] Hot-reload: EXE changed — shutting down gracefully");
                            Console.ResetColor();
                            break; // exit polling loop → cleanup + exit
                        }
                    }
                    catch { /* best effort */ }
                }

                // Wait for next frame
                var elapsed = (int)frameSw.ElapsedMilliseconds;
                var delay = Math.Max(0, intervalMs - elapsed);
                if (delay > 0)
                    Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE] Error: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(1000); // back off on error
            }
        }

        Console.WriteLine($"[EYE] Stopped ({frameCount} frames, {sw.Elapsed.TotalSeconds:F1}s)");
        cdp.Dispose();
        return 0;
    }

    /// <summary>
    /// Extract web page content via UIA (Accessibility tree) — "장애인의 눈으로 본 웹".
    /// Reads Chrome's accessibility tree instead of executing JavaScript.
    /// Requires Chrome launched with --force-renderer-accessibility flag.
    /// </summary>
    /// <param name="chromeHwnd">Chrome window handle (for UIA access)</param>
    /// <param name="maxLen">Maximum content length</param>
    /// <returns>Page title + body text, or null if not available</returns>
    internal static string? ExtractWebContentViaUia(IntPtr chromeHwnd, int maxLen = 500)
    {
        if (chromeHwnd == IntPtr.Zero) return null;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(chromeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Find the web document: Document with aid="RootWebArea"
            var docs = window.FindAllDescendants(
                cf.ByControlType(ControlType.Document));
            if (docs == null || docs.Length == 0) return null;

            // Find the Document that is NOT "Claude" (that would be the Claude Desktop overlay)
            // The web page Document has its title as Name or empty name
            FlaUI.Core.AutomationElements.AutomationElement? webDoc = null;
            foreach (var doc in docs)
            {
                try
                {
                    var name = doc.Name ?? "";
                    if (name != "Claude" && doc.AutomationId == "RootWebArea")
                    {
                        webDoc = doc;
                        break;
                    }
                }
                catch { continue; }
            }

            // Fallback: if standalone Chrome (not inside Claude Desktop), just take the first Document
            webDoc ??= docs.FirstOrDefault(d =>
            {
                try { return d.AutomationId == "RootWebArea"; }
                catch { return false; }
            });

            if (webDoc == null) return null;

            var parts = new List<string>();

            // 1. Try to find article title via known aid patterns
            string? title = null;
            try
            {
                // Naver news: aid="title_area"
                var titleElem = webDoc.FindFirstDescendant(
                    cf.ByAutomationId("title_area"));
                title = titleElem?.Name?.Trim();
            }
            catch { }

            if (string.IsNullOrEmpty(title))
            {
                // Fallback: first Heading element
                try
                {
                    var headings = webDoc.FindAllDescendants(
                        cf.ByControlType(ControlType.Header));
                    foreach (var h in headings)
                    {
                        try
                        {
                            var text = h.Name?.Trim();
                            if (!string.IsNullOrEmpty(text) && text.Length > 5)
                            {
                                title = text;
                                break;
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }

            if (string.IsNullOrEmpty(title))
            {
                // Use document name as title (Chrome sets this from <title>)
                title = webDoc.Name?.Trim();
                // Remove " - WKWebBot v0.1" suffix
                if (title != null)
                {
                    var idx = title.IndexOf(" - WKWebBot", StringComparison.Ordinal);
                    if (idx > 0) title = title[..idx];
                }
            }

            if (!string.IsNullOrEmpty(title))
                parts.Add(title);

            // 2. Extract article body text via known aid (dic_area for Naver news)
            var bodyBuilder = new StringBuilder();
            try
            {
                // Try known article container IDs
                var articleContainer = webDoc.FindFirstDescendant(cf.ByAutomationId("dic_area"))
                    ?? webDoc.FindFirstDescendant(cf.ByAutomationId("articleBodyContents"))
                    ?? webDoc.FindFirstDescendant(cf.ByAutomationId("articeBody"))
                    ?? webDoc.FindFirstDescendant(cf.ByAutomationId("newsct_article"));

                if (articleContainer != null)
                {
                    // Collect all Text elements inside the article container
                    var texts = articleContainer.FindAllDescendants(
                        cf.ByControlType(ControlType.Text));
                    foreach (var t in texts)
                    {
                        try
                        {
                            var text = t.Name?.Trim();
                            if (!string.IsNullOrEmpty(text) && text.Length > 1 && text != "\n")
                            {
                                bodyBuilder.Append(text);
                                bodyBuilder.Append(' ');
                            }
                            if (bodyBuilder.Length > maxLen) break;
                        }
                        catch { }
                    }
                }
            }
            catch { }

            // 3. Fallback: collect all visible Text elements from the page
            if (bodyBuilder.Length < 30)
            {
                try
                {
                    var allTexts = webDoc.FindAllDescendants(
                        cf.ByControlType(ControlType.Text));
                    foreach (var t in allTexts)
                    {
                        try
                        {
                            var rect = t.BoundingRectangle;
                            // Only on-screen elements (Y > 0, reasonable position)
                            if (rect.Y < 0 || rect.Height < 5) continue;

                            var text = t.Name?.Trim();
                            if (string.IsNullOrEmpty(text) || text.Length <= 2) continue;
                            // Skip navigation/UI labels
                            if (text == "|" || text == "WKWebBot") continue;
                            // Skip already collected title
                            if (text == title) continue;

                            bodyBuilder.Append(text);
                            bodyBuilder.Append(' ');
                            if (bodyBuilder.Length > maxLen) break;
                        }
                        catch { }
                    }
                }
                catch { }
            }

            var body = bodyBuilder.ToString().Trim();
            if (body.Length > maxLen)
                body = body[..(maxLen - 3)] + "...";
            if (!string.IsNullOrEmpty(body))
                parts.Add(body);

            return parts.Count > 0 ? string.Join("\n\n", parts) : null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Find Chrome main window by PID (for click-to-restore).</summary>
    private static IntPtr FindChromeWindow(int chromePid)
    {
        IntPtr found = IntPtr.Zero;
        if (chromePid <= 0) return found;

        var sb = new StringBuilder(256);
        EnumWindows((hwnd, _) =>
        {
            GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;
            if (!IsWindowVisible(hwnd)) return true;

            GetWindowThreadProcessId(hwnd, out var pid);
            if (pid != chromePid) return true;

            // Check for WS_CAPTION (main window, not popup)
            var style = GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            found = hwnd;
            return false;
        }, IntPtr.Zero);

        return found;
    }

    // ── P/Invoke declarations (shared across all AppBotEye partial class files) ──

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_BASIC_INFORMATION
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    // P/Invoke
    private delegate bool EnumWindowsProcMV(IntPtr hWnd, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProcMV lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern int GetWindowLongW(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out MV_RECT lpRect);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("ntdll.dll")]
    private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass,
        ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out MV_POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_POINT { public int X, Y; }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
}
