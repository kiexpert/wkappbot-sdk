using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot miniview [--port N] [--interval N] [--size WxH] [--pos X,Y]
/// Opens "Claude's Eye" overlay — a semi-transparent window showing WebBot's live view.
/// </summary>
internal partial class Program
{
    static int MiniViewCommand(string[] args)
    {
        // Parse arguments
        int port = 9222;
        int intervalMs = 100; // ~10 fps target (actual: limited by CDP screenshot speed)
        int width = 320, height = 220;
        int posX = -1, posY = -1;

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
            }
        }

        Console.WriteLine($"[MINIVIEW] Starting Claude's Eye ({width}x{height}, interval={intervalMs}ms)");

        // Connect to CDP
        CdpClient? cdp = null;
        try
        {
            cdp = new CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();
            // Enable Page domain (needed for captureScreenshot to work reliably)
            cdp.EvalAsync("1").GetAwaiter().GetResult(); // warm up
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[MINIVIEW] Connected to WebBot Chrome (port {port})");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[MINIVIEW] Cannot connect to Chrome on port {port}: {ex.Message}");
            Console.WriteLine($"[MINIVIEW] Run 'wkappbot web open <url>' first to launch WebBot Chrome.");
            Console.ResetColor();
            return 1;
        }

        // Find Chrome window handle for click-to-restore
        var chromeHwnd = FindChromeWindow(cdp.ChromePid);

        // Start overlay
        using var host = new MiniViewHost();
        host.Start(width, height, posX, posY);

        if (chromeHwnd != IntPtr.Zero)
            host.SetChromeHwnd(chromeHwnd);

        Console.WriteLine($"[MINIVIEW] Overlay active — press Ctrl+C to stop");

        // Handle Ctrl+C gracefully
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

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

                // Get URL periodically (every 10 frames to reduce overhead)
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
                }

                // Log stats periodically
                if (frameCount % 100 == 0)
                {
                    var fps = frameCount / sw.Elapsed.TotalSeconds;
                    Console.WriteLine($"[MINIVIEW] frame #{frameCount}, {fps:F1} fps, {png?.Length / 1024}KB");
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
                Console.WriteLine($"[MINIVIEW] Error: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(1000); // back off on error
            }
        }

        Console.WriteLine($"[MINIVIEW] Stopped ({frameCount} frames, {sw.Elapsed.TotalSeconds:F1}s)");
        cdp.Dispose();
        return 0;
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

    // P/Invoke for Chrome window finding
    private delegate bool EnumWindowsProcMV(IntPtr hWnd, IntPtr lParam);

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
}
