using System.Runtime.InteropServices;
using System.Text;

namespace WKAppBot.Win32.Native;

/// <summary>
/// Win32 P/Invoke declarations.
/// All Unicode (W) variants for Korean text compatibility.
/// </summary>
public static partial class NativeMethods
{
    // ── Window finding ───────────────────────────────────────────
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    public delegate bool EnumChildWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern bool EnumWindows(EnumWindowsProc proc, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildWindowsProc proc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsWindowEnabled(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindowW(string? lpClassName, string? lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindWindowExW(
        IntPtr hWndParent, IntPtr hWndChildAfter, string? lpszClass, string? lpszWindow);

    [DllImport("user32.dll")]
    public static extern int GetDlgCtrlID(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    // ── Window rect / state ──────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(POINT point);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    // ── Messages ─────────────────────────────────────────────────
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

    [DllImport("user32.dll")]
    public static extern bool PostMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    // ── Input ────────────────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    public static extern short VkKeyScanW(char ch);

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKeyW(uint uCode, uint uMapType);

    // ── Child window from point ─────────────────────────────────
    [DllImport("user32.dll")]
    public static extern IntPtr ChildWindowFromPointEx(IntPtr hWndParent, POINT pt, uint uFlags);

    public const uint CWP_SKIPINVISIBLE = 0x0001;
    public const uint CWP_SKIPDISABLED = 0x0002;
    public const uint CWP_SKIPTRANSPARENT = 0x0004;
    public const uint CWP_ALL = CWP_SKIPINVISIBLE | CWP_SKIPTRANSPARENT;

    [DllImport("user32.dll")]
    public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    // ── Z-order / hit-test ────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    public const uint GW_HWNDFIRST = 0; // topmost child in Z-order
    public const uint GW_HWNDNEXT = 2;  // next in Z-order (behind)
    public const uint GW_CHILD = 5;     // first child window

    [DllImport("user32.dll")]
    public static extern bool PtInRect(ref RECT lprc, POINT pt);

    [DllImport("user32.dll")]
    public static extern int GetWindowLongW(IntPtr hWnd, int nIndex);

    public const int GWL_STYLE = -16;
    public const int GWL_EXSTYLE = -20;

    // WS_* — Window Styles (GWL_STYLE)
    public const int WS_POPUP       = unchecked((int)0x80000000);
    public const int WS_CHILD       = 0x40000000;
    public const int WS_VISIBLE     = 0x10000000;
    public const int WS_DISABLED    = 0x08000000;
    public const int WS_CAPTION     = 0x00C00000;  // WS_BORDER | WS_DLGFRAME
    public const int WS_THICKFRAME  = 0x00040000;  // resizable
    public const int WS_VSCROLL     = 0x00200000;
    public const int WS_HSCROLL     = 0x00100000;

    // WS_EX_* — Extended Window Styles (GWL_EXSTYLE)
    public const int WS_EX_TRANSPARENT = 0x00000020;
    public const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int WS_EX_TOPMOST    = 0x00000008;
    public const int WS_EX_LAYERED    = 0x00080000;
    public const int WS_EX_NOACTIVATE = 0x08000000;

    [DllImport("user32.dll")]
    public static extern byte GetWindowBandID(IntPtr hWnd);  // undocumented, may not exist

    /// <summary>
    /// Walk Z-order from the given hwnd downward to find the real window at a point,
    /// skipping transparent overlays (Chrome extensions, screen capture tools, etc.)
    /// </summary>
    public static IntPtr FindRealWindowFromPoint(POINT pt, IntPtr topHwnd, string topClassName)
    {
        // If the top window is not a known overlay, return it directly
        if (!IsLikelyOverlay(topHwnd, topClassName))
            return topHwnd;

        // Walk Z-order behind topHwnd
        var next = GetWindow(topHwnd, GW_HWNDNEXT);
        int tries = 0;
        while (next != IntPtr.Zero && tries < 50)
        {
            tries++;
            if (IsWindowVisible(next))
            {
                GetWindowRect(next, out var rc);
                if (PtInRect(ref rc, pt))
                {
                    // Found a visible window containing the point
                    return next;
                }
            }
            next = GetWindow(next, GW_HWNDNEXT);
        }

        // Fallback: return original
        return topHwnd;
    }

    private static bool IsLikelyOverlay(IntPtr hWnd, string className)
    {
        // Chrome extension overlay
        if (className == "Chrome_WidgetWin_1" || className == "Chrome_WidgetWin_0")
        {
            // Check if it has transparent/layered style
            int exStyle = GetWindowLongW(hWnd, GWL_EXSTYLE);
            if ((exStyle & WS_EX_TRANSPARENT) != 0 || (exStyle & WS_EX_LAYERED) != 0)
                return true;

            // Also check by automation ID pattern (BTN_BKGRND = background button overlay)
            // We can't access UIA here, but the caller already knows
        }
        return false;
    }

    // ── DPI ──────────────────────────────────────────────────────
    [DllImport("shcore.dll")]
    public static extern int SetProcessDpiAwareness(int awareness);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    // SM_XVIRTUALSCREEN = 76, SM_YVIRTUALSCREEN = 77
    // SM_CXVIRTUALSCREEN = 78, SM_CYVIRTUALSCREEN = 79

    // ── MDI ──────────────────────────────────────────────────────
    public const uint WM_CLOSE = 0x0010;
    public const uint WM_COPYDATA = 0x004A;
    public const uint WM_GETTEXT = 0x000D;
    public const uint WM_GETTEXTLENGTH = 0x000E;
    public const uint WM_SETTEXT = 0x000C;
    public const uint WM_MDIGETACTIVE = 0x0229;
    public const uint WM_CHAR = 0x0102;
    public const uint WM_KEYDOWN = 0x0100;
    public const uint WM_KEYUP = 0x0101;
    public const uint WM_MOUSEMOVE = 0x0200;
    public const uint WM_LBUTTONDOWN = 0x0201;
    public const uint WM_LBUTTONUP = 0x0202;

    /// <summary>Pack (x, y) client coords into lParam for mouse messages.</summary>
    public static IntPtr MakeLParam(int x, int y) => (IntPtr)((y << 16) | (x & 0xFFFF));

    // ── Process ──────────────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("kernel32.dll")]
    public static extern uint GetCurrentThreadId();

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool GetTokenInformation(
        IntPtr TokenHandle, int TokenInformationClass,
        out int TokenInformation, int TokenInformationLength, out int ReturnLength);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr hObject);

    private const uint TOKEN_QUERY = 0x0008;
    private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
    private const int TokenElevation = 20;

    /// <summary>
    /// Check if a process (by PID) is running elevated (as administrator).
    /// Returns null if access denied (itself suggests elevation difference).
    /// </summary>
    public static bool? IsProcessElevated(uint processId)
    {
        var hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
        if (hProcess == IntPtr.Zero) return null; // access denied → likely elevated

        try
        {
            if (!OpenProcessToken(hProcess, TOKEN_QUERY, out var hToken))
                return null;

            try
            {
                if (GetTokenInformation(hToken, TokenElevation, out int elevation, 4, out _))
                    return elevation != 0;
                return null;
            }
            finally { CloseHandle(hToken); }
        }
        finally { CloseHandle(hProcess); }
    }

    /// <summary>
    /// Check if the current process is running elevated.
    /// </summary>
    public static bool IsCurrentProcessElevated()
    {
        using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
        var principal = new System.Security.Principal.WindowsPrincipal(identity);
        return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }

    // ── Screen capture ───────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(
        IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
        IntPtr hdcSrc, int xSrc, int ySrc, uint dwRop);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr hdc);

    [DllImport("user32.dll")]
    public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

    public const uint SRCCOPY = 0x00CC0020;
    public const uint PW_CLIENTONLY = 0x01;
    public const uint PW_RENDERFULLCONTENT = 0x02;

    // ── Window Z-order positioning ─────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(
        IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public static readonly IntPtr HWND_TOP = IntPtr.Zero;           // 0
    public static readonly IntPtr HWND_BOTTOM = new(1);
    public static readonly IntPtr HWND_TOPMOST = new(-1);
    public static readonly IntPtr HWND_NOTOPMOST = new(-2);
    public const uint SWP_NOMOVE = 0x0002;
    public const uint SWP_NOSIZE = 0x0001;
    public const uint SWP_NOACTIVATE = 0x0010;
    public const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll")]
    public static extern bool BringWindowToTop(IntPtr hWnd);

    // ── Focus / Alert ──────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool MessageBeep(uint uType);

    public const uint MB_ICONEXCLAMATION = 0x00000030;
    public const uint MB_ICONASTERISK = 0x00000040;

    [DllImport("user32.dll")]
    public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

    [DllImport("user32.dll")]
    public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    /// <summary>
    /// Check if a window's process is the foreground window.
    /// </summary>
    public static bool IsWindowForeground(IntPtr hWnd)
    {
        var fg = GetForegroundWindow();
        if (fg == hWnd) return true;

        // Also check if foreground belongs to same process (e.g. dialog on top)
        GetWindowThreadProcessId(hWnd, out uint targetPid);
        GetWindowThreadProcessId(fg, out uint fgPid);
        return targetPid == fgPid;
    }

    /// <summary>
    /// Smart SetForegroundWindow using AttachThreadInput trick.
    /// Windows normally blocks SetForegroundWindow from non-foreground threads.
    /// By attaching our input queue to the foreground thread, we gain permission.
    /// </summary>
    public static bool SmartSetForegroundWindow(IntPtr hWnd)
    {
        // Already foreground?
        if (IsWindowForeground(hWnd)) return true;

        // Simple attempt first
        SetForegroundWindow(hWnd);
        if (IsWindowForeground(hWnd)) return true;

        // AttachThreadInput trick
        var fgHwnd = GetForegroundWindow();
        uint fgThread = GetWindowThreadProcessId(fgHwnd, out _);
        uint ourThread = GetCurrentThreadId();

        if (fgThread != ourThread)
        {
            AttachThreadInput(ourThread, fgThread, true);
            try
            {
                SetForegroundWindow(hWnd);
            }
            finally
            {
                AttachThreadInput(ourThread, fgThread, false);
            }
        }

        return IsWindowForeground(hWnd);
    }
}

// ── Structs ──────────────────────────────────────────────────

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left, Top, Right, Bottom;
    public int Width => Right - Left;
    public int Height => Bottom - Top;
}

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X, Y;
    public POINT(int x, int y) { X = x; Y = y; }
}

[StructLayout(LayoutKind.Sequential)]
public struct COPYDATASTRUCT
{
    public IntPtr dwData;
    public int cbData;
    public IntPtr lpData;
}

// ── SendInput structs ────────────────────────────────────────

[StructLayout(LayoutKind.Sequential)]
public struct INPUT
{
    public uint type;
    public InputUnion u;

    public const uint INPUT_MOUSE = 0;
    public const uint INPUT_KEYBOARD = 1;
}

[StructLayout(LayoutKind.Explicit)]
public struct InputUnion
{
    [FieldOffset(0)] public MOUSEINPUT mi;
    [FieldOffset(0)] public KEYBDINPUT ki;
}

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
    public int dx, dy;
    public uint mouseData;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct KEYBDINPUT
{
    public ushort wVk;
    public ushort wScan;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

public static class MouseFlags
{
    public const uint MOUSEEVENTF_MOVE = 0x0001;
    public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    public const uint MOUSEEVENTF_LEFTUP = 0x0004;
    public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
}

public static class KeyFlags
{
    public const uint KEYEVENTF_KEYUP = 0x0002;
    public const uint KEYEVENTF_SCANCODE = 0x0008;
    public const uint KEYEVENTF_UNICODE = 0x0004;
}

[StructLayout(LayoutKind.Sequential)]
public struct FLASHWINFO
{
    public uint cbSize;
    public IntPtr hwnd;
    public uint dwFlags;
    public uint uCount;
    public uint dwTimeout;

    public const uint FLASHW_CAPTION = 0x00000001;
    public const uint FLASHW_TRAY = 0x00000002;
    public const uint FLASHW_ALL = FLASHW_CAPTION | FLASHW_TRAY;
    public const uint FLASHW_TIMER = 0x00000004;
    public const uint FLASHW_TIMERNOFG = 0x0000000C;
    public const uint FLASHW_STOP = 0;
}
