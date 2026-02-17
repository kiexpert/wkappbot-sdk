using System.Diagnostics;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Find windows by title, class name, process, or HWND.
/// All Unicode (W) functions for Korean text.
/// </summary>
public static class WindowFinder
{
    /// <summary>
    /// Find all visible top-level windows matching a title substring.
    /// </summary>
    public static List<WindowInfo> FindByTitle(string titleSubstring)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var title = GetWindowText(hWnd);
            if (title.Contains(titleSubstring, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(WindowInfo.FromHwnd(hWnd));
            }
            return true;
        }, IntPtr.Zero);
        return results;
    }

    /// <summary>
    /// Find top-level window by class name.
    /// </summary>
    public static List<WindowInfo> FindByClassName(string className)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var cls = GetClassName(hWnd);
            if (cls.Equals(className, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(WindowInfo.FromHwnd(hWnd));
            }
            return true;
        }, IntPtr.Zero);
        return results;
    }

    /// <summary>
    /// Find the MDI main frame of a process (has MDIClient child).
    /// Pattern from leak_test_repeat.ps1.
    /// </summary>
    public static IntPtr FindMDIMainFrame(uint processId)
    {
        IntPtr found = IntPtr.Zero;
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != processId) return true;
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var hMdi = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "MDIClient", null);
            if (hMdi != IntPtr.Zero)
            {
                found = hWnd;
                return false; // stop enumeration
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }

    /// <summary>
    /// Find the MDI main frame by process name (e.g., "HTSRun").
    /// </summary>
    public static IntPtr FindMDIMainFrameByProcessName(string processName)
    {
        var proc = Process.GetProcessesByName(processName).FirstOrDefault();
        if (proc == null) return IntPtr.Zero;
        return FindMDIMainFrame((uint)proc.Id);
    }

    /// <summary>
    /// Enumerate child windows of a parent.
    /// </summary>
    public static List<WindowInfo> GetChildren(IntPtr hWndParent)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumChildWindows(hWndParent, (hWnd, _) =>
        {
            if (NativeMethods.GetParent(hWnd) == hWndParent)
            {
                results.Add(WindowInfo.FromHwnd(hWnd));
            }
            return true;
        }, IntPtr.Zero);
        return results;
    }

    /// <summary>
    /// Count MDI children (direct children of MDIClient).
    /// </summary>
    public static int CountMDIChildren(IntPtr hMainWnd)
    {
        var hMdi = NativeMethods.FindWindowExW(hMainWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero) return -1;

        int count = 0;
        NativeMethods.EnumChildWindows(hMdi, (hWnd, _) =>
        {
            if (NativeMethods.GetParent(hWnd) == hMdi) count++;
            return true;
        }, IntPtr.Zero);
        return count;
    }

    /// <summary>
    /// Get the active MDI child window.
    /// </summary>
    public static IntPtr GetActiveMDIChild(IntPtr hMainWnd)
    {
        var hMdi = NativeMethods.FindWindowExW(hMainWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero) return IntPtr.Zero;
        return NativeMethods.SendMessageW(hMdi, NativeMethods.WM_MDIGETACTIVE, IntPtr.Zero, IntPtr.Zero);
    }

    /// <summary>
    /// Build Win32 window hierarchy path from a child hwnd up to the desktop.
    /// Returns class name path like: "ApplicationFrameWindow/Windows.UI.Core.CoreWindow/..."
    /// </summary>
    public static string GetWindowHierarchyPath(IntPtr hWnd, int maxDepth = 8)
    {
        var parts = new List<string>();
        var current = hWnd;
        int depth = 0;

        while (current != IntPtr.Zero && depth < maxDepth)
        {
            var cls = GetClassName(current);
            if (string.IsNullOrEmpty(cls)) break;

            // Stop at desktop window classes
            if (cls == "Progman" || cls == "#32769") break;

            parts.Add(cls);
            current = NativeMethods.GetParent(current);
            depth++;
        }

        parts.Reverse();
        return string.Join("/", parts);
    }

    /// <summary>
    /// Build Win32 window class hierarchy by drilling DOWN from hWnd to the deepest
    /// child window at the given screen coordinate.
    /// Returns top-down path like: "ApplicationFrameWindow/Windows.UI.Core.CoreWindow/..."
    /// </summary>
    public static string GetWindowHierarchyPathAtPoint(IntPtr hWnd, int screenX, int screenY, int maxDepth = 8)
    {
        var parts = new List<string>();
        var current = hWnd;

        for (int depth = 0; depth < maxDepth; depth++)
        {
            var cls = GetClassName(current);
            if (string.IsNullOrEmpty(cls)) break;
            if (cls == "Progman" || cls == "#32769") break;

            parts.Add(cls);

            // Convert screen coords to client coords for this window
            var clientPt = new POINT { X = screenX, Y = screenY };
            NativeMethods.ScreenToClient(current, ref clientPt);

            // Find child window at that point
            var child = NativeMethods.ChildWindowFromPointEx(current, clientPt, NativeMethods.CWP_ALL);

            // No child, or child is self = we've reached the deepest
            if (child == IntPtr.Zero || child == current) break;

            current = child;
        }

        return string.Join("/", parts);
    }

    // ── Helpers ──────────────────────────────────────────────────

    public static string GetWindowText(IntPtr hWnd)
    {
        var sb = new StringBuilder(512);
        NativeMethods.GetWindowTextW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    public static string GetClassName(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        NativeMethods.GetClassNameW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    public static RECT GetWindowRect(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        return rect;
    }

    /// <summary>
    /// Bring window to foreground and set focus (simple version).
    /// </summary>
    public static void BringToFront(IntPtr hWnd)
    {
        NativeMethods.ShowWindow(hWnd, 9); // SW_RESTORE
        NativeMethods.SetForegroundWindow(hWnd);
    }

    /// <summary>
    /// Smart focus acquisition with multi-phase recovery.
    /// Returns (success, method) — method describes how focus was obtained.
    ///
    /// Phase 0: Already focused? → return immediately (0ms)
    /// Phase 1: Alert + wait (alertDelaySec) → beep + flash, give user time to finish
    /// Phase 2: Smart recovery (remaining timeout) → AttachThreadInput trick
    /// Phase 3: Timeout → fail
    ///
    /// Design: "알림 후 3초 대기 → 기회 오거나 응답 없으면 즉시 입력 수행"
    /// </summary>
    /// <param name="hWnd">Target window handle</param>
    /// <param name="timeoutSec">Total timeout in seconds</param>
    /// <param name="retryDelaySec">Delay between retries</param>
    /// <param name="alertDelaySec">How long to wait after beep before forcing focus. 0 = force immediately.</param>
    /// <param name="consoleLock">Shared console lock for [FOCUS] output</param>
    /// <returns>(success, method description)</returns>
    public static (bool success, string method) SmartBringToFront(
        IntPtr hWnd,
        double timeoutSec = 5.0,
        double retryDelaySec = 0.3,
        double alertDelaySec = 3.0,
        object? consoleLock = null)
    {
        consoleLock ??= new object();

        // Phase 0: Already focused?
        if (NativeMethods.IsWindowForeground(hWnd))
            return (true, "already_focused");

        // ── Phase 1: Alert + graceful wait ─────────────────────
        // Beep + flash to warn user, then wait alertDelaySec for user to switch back
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Focus lost — alerting user...");
            Console.ResetColor();
        }

        NativeMethods.MessageBeep(NativeMethods.MB_ICONEXCLAMATION);

        // Flash the target window's taskbar button
        var flashInfo = new FLASHWINFO
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<FLASHWINFO>(),
            hwnd = hWnd,
            dwFlags = FLASHWINFO.FLASHW_ALL | FLASHWINFO.FLASHW_TIMERNOFG,
            uCount = 5,
            dwTimeout = 0
        };
        NativeMethods.FlashWindowEx(ref flashInfo);

        // Wait alertDelaySec — user might click back voluntarily
        var alertSw = System.Diagnostics.Stopwatch.StartNew();
        while (alertSw.Elapsed.TotalSeconds < alertDelaySec)
        {
            Thread.Sleep((int)(retryDelaySec * 1000));

            if (NativeMethods.IsWindowForeground(hWnd))
            {
                StopFlash(hWnd);
                lock (consoleLock)
                {
                    ClearLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[FOCUS] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Focus restored by user ← {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "user_restored");
            }
        }

        // ── Phase 2: Smart recovery (force) ────────────────────
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Forcing focus recovery...");
            Console.ResetColor();
        }

        var remainingTimeout = timeoutSec - alertDelaySec;
        var recoverySw = System.Diagnostics.Stopwatch.StartNew();

        while (recoverySw.Elapsed.TotalSeconds < Math.Max(remainingTimeout, 1.0))
        {
            // Restore + SmartSetForegroundWindow (AttachThreadInput trick)
            NativeMethods.ShowWindow(hWnd, 9); // SW_RESTORE
            NativeMethods.SmartSetForegroundWindow(hWnd);

            Thread.Sleep((int)(retryDelaySec * 1000));

            if (NativeMethods.IsWindowForeground(hWnd))
            {
                StopFlash(hWnd);
                lock (consoleLock)
                {
                    ClearLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[FOCUS] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Focus recovered ← {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "smart_recovery");
            }

            // Beep every 2 seconds during recovery
            if ((int)recoverySw.Elapsed.TotalSeconds % 2 == 0)
                NativeMethods.MessageBeep(NativeMethods.MB_ICONEXCLAMATION);
        }

        // ── Phase 3: Timeout ───────────────────────────────────
        StopFlash(hWnd);
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"FOCUS RECOVERY FAILED — timeout after {timeoutSec:F1}s");
            Console.ResetColor();
        }

        return (false, "timeout");
    }

    private static void StopFlash(IntPtr hWnd)
    {
        var stop = new FLASHWINFO
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<FLASHWINFO>(),
            hwnd = hWnd,
            dwFlags = FLASHWINFO.FLASHW_STOP,
            uCount = 0,
            dwTimeout = 0
        };
        NativeMethods.FlashWindowEx(ref stop);
    }

    private static void ClearLine()
    {
        int w;
        try { w = Console.WindowWidth; } catch { w = 120; }
        Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");
    }
}

/// <summary>
/// Snapshot of window properties at query time.
/// </summary>
public sealed class WindowInfo
{
    public IntPtr Handle { get; init; }
    public string Title { get; init; } = "";
    public string ClassName { get; init; } = "";
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }

    public static WindowInfo FromHwnd(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        return new WindowInfo
        {
            Handle = hWnd,
            Title = WindowFinder.GetWindowText(hWnd),
            ClassName = WindowFinder.GetClassName(hWnd),
            ControlId = NativeMethods.GetDlgCtrlID(hWnd),
            Rect = rect,
            IsVisible = NativeMethods.IsWindowVisible(hWnd),
        };
    }

    public override string ToString() =>
        $"[{Handle:X8}] \"{Title}\" ({ClassName}) cid={ControlId} ({Rect.Width}x{Rect.Height})";
}
