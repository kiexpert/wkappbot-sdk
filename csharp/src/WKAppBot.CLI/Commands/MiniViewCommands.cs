using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot miniview [--port N] [--interval N] [--size WxH] [--pos X,Y]
/// Opens "Claude's Eye" overlay — a semi-transparent window showing WebBot's live view.
/// Auto-positions at the top-right corner of the Claude Desktop window when possible.
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
                    Console.WriteLine($"[MINIVIEW] Found Claude Desktop at ({rect.Left},{rect.Top} {rect.Right - rect.Left}x{rect.Bottom - rect.Top})");
                    Console.WriteLine($"[MINIVIEW] Auto-placing at ({posX},{posY}) — top-right corner");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("[MINIVIEW] Claude Desktop not found — using default position");
            }
        }

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

        // Start overlay — attach to Claude Desktop window as owner if found
        using var host = new MiniViewHost();
        host.Start(width, height, posX, posY, claudeHwnd);

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

                    // Get focused element accessibility text
                    try
                    {
                        var axJs = @"(() => {
                            const el = document.activeElement;
                            if (!el || el === document.body) return document.title;
                            const tag = el.tagName?.toLowerCase() || '';
                            const role = el.getAttribute('role') || '';
                            const label = el.getAttribute('aria-label') || '';
                            const text = (el.textContent || '').trim().substring(0, 100);
                            const val = el.value ? `""${el.value.substring(0, 50)}""` : '';
                            const id = el.id ? `#${el.id}` : '';
                            return `[${tag}${id}] ${role?'role='+role+' ':''}${label?label+' ':''}${val||text}`;
                        })()";
                        var axTask = cdp.EvalAsync(axJs);
                        if (axTask.Wait(2000))
                        {
                            var axText = axTask.Result?.ToString() ?? "";
                            if (axText.Length > 0)
                                host.UpdateAccessibilityText(axText);
                        }
                    }
                    catch { /* skip AX update if busy */ }
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

    /// <summary>
    /// Find Claude Desktop main window by walking the parent process tree.
    /// wkappbot → ... → claude.exe (Electron) — find its main window.
    /// Much faster than scanning all 14+ claude.exe processes!
    /// </summary>
    private static IntPtr FindClaudeDesktopWindow()
    {
        // Walk up: wkappbot.exe → parent → grandparent → ... find claude.exe
        var claudePids = new HashSet<int>();
        try
        {
            int pid = Process.GetCurrentProcess().Id;
            for (int depth = 0; depth < 10; depth++)
            {
                var parentPid = GetParentPid(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    Console.WriteLine($"[MINIVIEW] Ancestor: {parent.ProcessName} (PID={parentPid})");
                    if (parent.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                    {
                        claudePids.Add(parentPid);
                        // Also add siblings (Electron uses multiple processes under same parent tree)
                        // Get the parent's parent — the root claude.exe
                        var rootPid = GetParentPid(parentPid);
                        if (rootPid > 0)
                        {
                            try
                            {
                                var root = Process.GetProcessById(rootPid);
                                if (root.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                                    claudePids.Add(rootPid);
                            }
                            catch { }
                        }
                        break;
                    }
                    pid = parentPid;
                }
                catch { break; }
            }
        }
        catch { }

        // If ancestor walk didn't find claude.exe, try all claude processes (slow fallback)
        if (claudePids.Count == 0)
        {
            Console.WriteLine("[MINIVIEW] Ancestor walk didn't find claude.exe, scanning all...");
            foreach (var proc in Process.GetProcessesByName("claude"))
                claudePids.Add(proc.Id);
        }

        // Find the visible main window (with title bar + sizable) from claude PIDs
        IntPtr found = IntPtr.Zero;
        EnumWindows((hwnd, _) =>
        {
            if (!IsWindowVisible(hwnd)) return true;

            GetWindowThreadProcessId(hwnd, out var pid);
            if (!claudePids.Contains(pid)) return true;

            // Check for WS_CAPTION (main window with title bar)
            var style = GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            // Verify it's a sizable window (not a small popup or notification)
            if (GetWindowRect(hwnd, out var rect))
            {
                var w = rect.Right - rect.Left;
                var h = rect.Bottom - rect.Top;
                if (w > 400 && h > 300)
                {
                    found = hwnd;
                    return false; // stop enumeration
                }
            }

            return true;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>Get parent PID using NtQueryInformationProcess (fast, no WMI dependency).</summary>
    private static int GetParentPid(int pid)
    {
        try
        {
            var handle = OpenProcess(0x0400 /* PROCESS_QUERY_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero)
                handle = OpenProcess(0x1000 /* PROCESS_QUERY_LIMITED_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero) return -1;

            try
            {
                var pbi = new PROCESS_BASIC_INFORMATION();
                int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out _);
                if (status == 0)
                    return (int)pbi.InheritedFromUniqueProcessId;
            }
            finally
            {
                CloseHandle(handle);
            }
        }
        catch { }
        return -1;
    }

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
}
