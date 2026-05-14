namespace WKAppBot.Launcher;

partial class Program
{
    // Win32 RECT (left, top, right, bottom). MUST NOT be marshalled as
    // System.Drawing.Rectangle: Rectangle's fields are (X, Y, Width, Height), so
    // the same 4 LONGs from Win32 get reinterpreted as (left, top, width=right,
    // height=bottom), and accessing .Right/.Bottom adds them again -- yielding
    // garbage like Right = left+right_coord. This was the long-standing reason
    // CASCADIA_HOSTING_WINDOW_CLASS (Windows Terminal) on a left/secondary
    // monitor (negative coords like -2414..-1285) was falsely flagged as
    // off-screen and rejected the caller.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public int Width => Right - Left;
        public int Height => Bottom - Top;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out int pvAttribute, int cbAttribute);

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool IsWindow(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    const int DWMWA_CLOAKED = 14;
    const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

    /// <summary>
    /// Returns the window's bounding rect as (Left, Top, Right, Bottom). Prefers
    /// DWM extended frame bounds (more accurate, excludes drop-shadow padding),
    /// falls back to GetWindowRect. Returns false only if both APIs fail.
    /// </summary>
    static bool TryGetWindowRectLTRB(IntPtr hwnd, out RECT rect)
    {
        rect = default;
        if (hwnd == IntPtr.Zero) return false;
        try
        {
            // DWM extended frame bounds is the "real" visible rect (excludes
            // the 8px invisible resize border on Win10+ Aero windows).
            if (DwmGetWindowAttribute(hwnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT dwmRect, 16) == 0
                && (dwmRect.Right > dwmRect.Left) && (dwmRect.Bottom > dwmRect.Top))
            {
                rect = dwmRect;
                return true;
            }
        }
        catch { /* fall through to GetWindowRect */ }
        return GetWindowRect(hwnd, out rect);
    }

    /// <summary>
    /// True iff window is DWM-cloaked (UWP suspended app, Win+Tab thumbnail,
    /// virtual-desktop hidden). Cloaked windows are not visible to the user even
    /// though GetWindowRect returns valid coords.
    /// </summary>
    static bool IsWindowCloaked(IntPtr hwnd)
    {
        try
        {
            if (DwmGetWindowAttribute(hwnd, DWMWA_CLOAKED, out int cloaked, 4) == 0)
                return cloaked != 0;
        }
        catch { }
        return false;
    }

    // AOT-friendly: collect monitor rects into a static field via a non-capturing
    // delegate. We cannot use closures here (NativeAOT cannot synthesize the
    // marshal stub for a capturing lambda). The collection is guarded by a
    // monitor lock so concurrent validations don't clobber the shared list.
    static readonly object s_monitorRectsLock = new();
    static List<RECT>? s_monitorRectsScratch;
    static readonly MonitorEnumProc s_collectMonitorRects = CollectMonitorRect;

    static bool CollectMonitorRect(IntPtr hMon, IntPtr hdc, ref RECT mr, IntPtr _)
    {
        try { s_monitorRectsScratch?.Add(mr); } catch { }
        return true;
    }

    /// <summary>
    /// True iff the given rect does not intersect ANY monitor's bounds. Walks
    /// the full multi-monitor virtual screen via EnumDisplayMonitors so windows
    /// living entirely on a left/secondary monitor with negative coords are
    /// correctly classified as on-screen.
    /// </summary>
    static bool IsRectOutsideAllMonitors(RECT r)
    {
        List<RECT> rects;
        lock (s_monitorRectsLock)
        {
            s_monitorRectsScratch = new List<RECT>(4);
            try
            {
                if (!EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, s_collectMonitorRects, IntPtr.Zero))
                    return false; // fail open
                rects = s_monitorRectsScratch;
            }
            catch
            {
                return false; // fail open
            }
            finally
            {
                s_monitorRectsScratch = null;
            }
        }

        foreach (var mr in rects)
        {
            if (r.Left   < mr.Right
             && r.Right  > mr.Left
             && r.Top    < mr.Bottom
             && r.Bottom > mr.Top)
            {
                return false; // intersects this monitor → on-screen
            }
        }
        return true; // no monitor intersected → off-screen
    }

    static bool IsWindowOffScreen(IntPtr hwnd)
    {
        try
        {
            if (!IsWindow(hwnd)) { LogValidation(hwnd, default, false, false, false, "not_a_window"); return true; }
            bool visible = IsWindowVisible(hwnd);
            bool cloaked = IsWindowCloaked(hwnd);
            bool gotRect = TryGetWindowRectLTRB(hwnd, out var rect);

            if (!gotRect)               { LogValidation(hwnd, default, true, visible, cloaked, "get_rect_failed"); return true; }
            if (cloaked)                { LogValidation(hwnd, rect,    true, visible, cloaked, "cloaked");        return true; }
            if (!visible)               { LogValidation(hwnd, rect,    true, visible, cloaked, "not_visible");    return true; }
            if (rect.Right <= rect.Left || rect.Bottom <= rect.Top)
                                        { LogValidation(hwnd, rect,    true, visible, cloaked, "degenerate_rect"); return true; }
            if (IsRectOutsideAllMonitors(rect))
                                        { LogValidation(hwnd, rect,    true, visible, cloaked, "outside_all_monitors"); return true; }

            LogValidation(hwnd, rect, true, visible, cloaked, "on_screen");
            return false;
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[VALIDATION] hwnd=0x{hwnd.ToInt64():X} exception={ex.GetType().Name}: {ex.Message}"); } catch { }
            return true;
        }
    }

    static void LogValidation(IntPtr hwnd, RECT r, bool isWindow, bool isVisible, bool cloaked, string verdict)
    {
        try
        {
            var cls = new System.Text.StringBuilder(64);
            GetClassNameW(hwnd, cls, cls.Capacity);
            Console.Error.WriteLine(
                $"[VALIDATION] hwnd=0x{hwnd.ToInt64():X} class={cls} "
                + $"rect=(L={r.Left},T={r.Top},R={r.Right},B={r.Bottom}) "
                + $"size=({r.Width}x{r.Height}) "
                + $"IsWindow={isWindow} IsVisible={isVisible} cloaked={cloaked} verdict={verdict}");
        }
        catch { }
    }

    static string? GetWindowSnapshot(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return null;
        try
        {
            var cls = new System.Text.StringBuilder(256);
            var title = new System.Text.StringBuilder(256);
            GetClassNameW(hwnd, cls, 256);
            GetWindowTextW(hwnd, title, 256);
            var clsText = cls.ToString();
            var titleText = title.ToString();
            if (titleText.Length > 80) titleText = titleText[..77] + "...";
            if (string.IsNullOrWhiteSpace(clsText) && string.IsNullOrWhiteSpace(titleText))
                return $"0x{hwnd.ToInt64():X}";
            return $"0x{hwnd.ToInt64():X} {clsText} {titleText}".Trim();
        }
        catch
        {
            return $"0x{hwnd.ToInt64():X}";
        }
    }

    static IntPtr GetHostWindowSnapshot()
    {
        try
        {
            var parentPid = GetParentProcessId(Environment.ProcessId);
            if (parentPid <= 0) return IntPtr.Zero;
            using var parent = System.Diagnostics.Process.GetProcessById(parentPid);
            return parent.MainWindowHandle;
        }
        catch
        {
            return IntPtr.Zero;
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
    static extern bool IsWindowVisibleLocal(IntPtr hWnd);

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "EnumWindows")]
    static extern bool EnumWindowsLocal(EnumWindowsProcLocal lpEnumFunc, IntPtr lParam);

    delegate bool EnumWindowsProcLocal(IntPtr hWnd, IntPtr lParam);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr GetParent(IntPtr hWnd);

    // Monitor enumeration for "place Chrome on caller's monitor" clamping.
    // The launcher is PerMonitorV2-aware, so the coordinates we read here match
    // the physical-pixel space Chrome uses in SetWindowPos.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct POINT
    {
        public int X;
        public int Y;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    const uint MONITOR_DEFAULTTONULL    = 0x00000000;
    const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern bool GetMonitorInfoW(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Returns the work area (monitor minus taskbar) of the monitor that
    /// contains the given physical-pixel point. Returns false on failure
    /// (caller should fall back to no clamping in that case).
    /// </summary>
    static bool TryGetWorkArea(int physX, int physY, out RECT workArea)
    {
        workArea = default;
        try
        {
            var pt = new POINT { X = physX, Y = physY };
            var hMon = MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST);
            if (hMon == IntPtr.Zero) return false;
            var mi = new MONITORINFO();
            mi.cbSize = System.Runtime.InteropServices.Marshal.SizeOf<MONITORINFO>();
            if (!GetMonitorInfoW(hMon, ref mi)) return false;
            workArea = mi.rcWork;
            return workArea.Right > workArea.Left && workArea.Bottom > workArea.Top;
        }
        catch
        {
            return false;
        }
    }
}
