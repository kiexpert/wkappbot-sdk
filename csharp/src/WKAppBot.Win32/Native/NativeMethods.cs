using System.Drawing;
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

    [DllImport("user32.dll", EntryPoint = "GetClassLongPtrW")]
    public static extern IntPtr GetClassLongPtrW(IntPtr hWnd, int nIndex);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern uint GetModuleFileNameW(IntPtr hModule, StringBuilder lpFilename, uint nSize);

    [DllImport("user32.dll")]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    // ── Menu API ─────────────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern IntPtr GetMenu(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int GetMenuItemCount(IntPtr hMenu);

    [DllImport("user32.dll")]
    public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

    [DllImport("user32.dll")]
    public static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetMenuStringW(IntPtr hMenu, uint uIDItem, StringBuilder lpString, int nMaxCount, uint uFlag);

    public const uint MF_BYPOSITION = 0x00000400u;
    public const uint MF_SEPARATOR  = 0x00000800u;
    public const uint MF_GRAYED     = 0x00000001u;
    public const uint MF_DISABLED   = 0x00000002u;

    // ── Menu resource scanning (multi-language) ───────────────────
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern uint GetWindowModuleFileNameW(IntPtr hwnd, StringBuilder pszFileName, uint cchFileNameMax);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr LoadLibraryExW(string lpFileName, IntPtr hFile, uint dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    public delegate bool EnumResNameProc(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam);
    public delegate bool EnumResLangProc(IntPtr hModule, IntPtr lpType, IntPtr lpName, ushort wLanguage, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern bool EnumResourceNamesW(IntPtr hModule, IntPtr lpType, EnumResNameProc lpEnumFunc, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern bool EnumResourceLanguagesW(IntPtr hModule, IntPtr lpType, IntPtr lpName, EnumResLangProc lpEnumFunc, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr FindResourceExW(IntPtr hModule, IntPtr lpType, IntPtr lpName, ushort wLanguage);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LockResource(IntPtr hResData);

    public static readonly IntPtr RT_MENU = (IntPtr)4;
    public const uint LOAD_LIBRARY_AS_DATAFILE       = 0x00000002u;
    public const uint LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020u;

    [DllImport("user32.dll")]
    public static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    }

    public const int SW_SHOWNOACTIVATE = 4;
    public const int SW_SHOWMINNOACTIVE = 7;
    public const int SW_RESTORE = 9;

    // ── Screen Reader announcement (SPI_SETSCREENREADER) ──
    // Tells all apps "a screen reader is running" → Chromium/Electron auto-enable A11Y tree
    public const uint SPI_GETSCREENREADER = 0x0046;
    public const uint SPI_SETSCREENREADER = 0x0047;
    public const uint SPIF_SENDCHANGE = 0x0002;

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SystemParametersInfoW(uint uiAction, uint uiParam, out int pvParam, uint fWinIni);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SystemParametersInfoW(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

    [DllImport("user32.dll")]
    public static extern bool IsWindowEnabled(IntPtr hWnd);
    [DllImport("user32.dll")] public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
    [DllImport("user32.dll")] public static extern bool UpdateWindow(IntPtr hWnd);

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
    public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);
    public const uint GA_PARENT = 1;
    public const uint GA_ROOT = 2;
    public const uint GA_ROOTOWNER = 3;

    [DllImport("user32.dll")]
    public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern bool SetPropW(IntPtr hWnd, string lpString, IntPtr hData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr GetPropW(IntPtr hWnd, string lpString);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr RemovePropW(IntPtr hWnd, string lpString);

    // Global atom table — used to cache strings in window props (survives Eye restart)
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern void OutputDebugStringW(string lpOutputString);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern ushort GlobalAddAtomW(string lpString);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern uint GlobalGetAtomNameW(ushort nAtom, System.Text.StringBuilder lpBuffer, int nSize);

    [DllImport("kernel32.dll")]
    public static extern ushort GlobalDeleteAtom(ushort nAtom);

    public delegate bool PropEnumProcEx(IntPtr hWnd, IntPtr lpszString, IntPtr hData, UIntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int EnumPropsExW(IntPtr hWnd, PropEnumProcEx lpEnumFunc, UIntPtr lParam);

    /// <summary>Enumerate all Win32 properties on a window. Returns list of (name, value).</summary>
    public static List<(string name, long value)> EnumWindowProps(IntPtr hWnd)
    {
        var props = new List<(string, long)>();
        EnumPropsExW(hWnd, (hwnd, lpszString, hData, dwData) =>
        {
            try
            {
                var name = System.Runtime.InteropServices.Marshal.PtrToStringUni(lpszString);
                if (name != null)
                    props.Add((name, hData.ToInt64()));
            }
            catch { }
            return true; // continue
        }, UIntPtr.Zero);
        return props;
    }

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    // ── GUI thread info (cross-validate focus target) ────────────
    [StructLayout(LayoutKind.Sequential)]
    public struct GUITHREADINFO
    {
        public int cbSize;
        public int flags;
        public IntPtr hwndActive;
        public IntPtr hwndFocus;
        public IntPtr hwndCapture;
        public IntPtr hwndMenuOwner;
        public IntPtr hwndMoveSize;
        public IntPtr hwndCaret;
        public RECT rcCaret;
    }

    [DllImport("user32.dll")]
    public static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO pgui);

    /// <summary>
    /// Get the currently focused window in a target process's GUI thread.
    /// Returns IntPtr.Zero if not available.
    /// </summary>
    public static IntPtr GetFocusedHwndInProcess(IntPtr anyHwndInProcess)
    {
        GetWindowThreadProcessId(anyHwndInProcess, out _);
        uint threadId = GetWindowThreadProcessId(anyHwndInProcess, out _);
        var gti = new GUITHREADINFO { cbSize = Marshal.SizeOf<GUITHREADINFO>() };
        if (GetGUIThreadInfo(threadId, ref gti))
            return gti.hwndFocus;
        return IntPtr.Zero;
    }

    // ── Window rect / state ──────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(POINT point);

    /// <summary>
    /// Like WindowFromPoint but ignores transparent/disabled windows and drills into real child controls.
    /// Returns the deepest child that actually owns the point — useful for WPF/Electron host windows.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern IntPtr RealChildWindowFromPoint(IntPtr hwndParent, POINT ptParentClientCoords);

    [DllImport("user32.dll", EntryPoint = "SetFocus")]
    public static extern IntPtr SetFocusRaw(IntPtr hWnd);

    /// <summary>
    /// Smart SetFocus — [FOCUS-GUARD] blocks if user is actively typing.
    /// Use for all NEW focus acquisitions (not restores).
    /// For focus-restore patterns (FocusSnapshot.Restore, MidInputFocusCheck): use SetFocusRaw.
    /// </summary>
    public static IntPtr SetFocus(IntPtr hWnd)
    {
        if (!Input.InputReadiness.CheckActiveGuard("SetFocus")) return IntPtr.Zero;
        return SetFocusRaw(hWnd);
    }

    /// <summary>
    /// Raw SetForegroundWindow P/Invoke — NO guards, NO focusless check.
    /// Use ONLY for focus-restore patterns (undoing a previous steal).
    /// For all other uses: SetForegroundWindow (proxy) or SmartSetForegroundWindow (full gate).
    /// </summary>
    [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    public static extern bool SetForegroundWindowRaw(IntPtr hWnd);

    // ── IME (Korean/CJK input mode + composition) ────────────────────────────
    [DllImport("imm32.dll")]
    public static extern IntPtr ImmGetContext(IntPtr hWnd);
    [DllImport("imm32.dll")]
    public static extern bool ImmGetConversionStatus(IntPtr hIMC, out uint lpConversion, out uint lpSentence);
    [DllImport("imm32.dll")]
    public static extern bool ImmSetConversionStatus(IntPtr hIMC, uint dwConversion, uint dwSentence);
    [DllImport("imm32.dll")]
    public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
    // GCS_COMPSTR=0x0008: get current composition (조합중) string length/content
    [DllImport("imm32.dll", CharSet = CharSet.Unicode)]
    public static extern int ImmGetCompositionStringW(IntPtr hIMC, uint dwIndex, char[]? lpBuf, uint dwBufLen);
    // SCS_SETSTR=0x0002: re-inject composition string into IME buffer
    [DllImport("imm32.dll", CharSet = CharSet.Unicode)]
    public static extern bool ImmSetCompositionStringW(IntPtr hIMC, uint dwIndex, string lpComp, uint dwCompLen, IntPtr lpRead, uint dwReadLen);
    // NI_COMPOSITIONSTR=0x0015, CPS_COMPLETE=0x0001: finalize composition → commit to app
    [DllImport("imm32.dll")]
    public static extern bool ImmNotifyIME(IntPtr hIMC, uint dwAction, uint dwIndex, uint dwValue);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    /// <summary>
    /// [FOCUS-GUARD] 현재 키보드 포커스를 가진 HWND 반환.
    /// GetForegroundWindow()보다 정밀 — 같은 창 내에서도 컨트롤 간 포커스 이동을 감지.
    /// 예: VS Code 코드 에디터 → Message input 이동 = 같은 hwnd이지만 hwndFocus 다름.
    /// idThread=0 → 포그라운드 스레드의 키보드 포커스 반환.
    /// </summary>
    public static IntPtr GetKeyboardFocusHwnd()
    {
        var info = new GUITHREADINFO { cbSize = Marshal.SizeOf<GUITHREADINFO>() };
        return GetGUIThreadInfo(0, ref info) ? info.hwndFocus : IntPtr.Zero;
    }

    /// <summary>
    /// 특정 스레드의 키보드 포커스 hwnd 반환.
    /// 전경 여부 무관 — 백그라운드 창의 per-thread focus 조회 가능.
    /// </summary>
    public static IntPtr GetThreadFocusHwnd(uint tid)
    {
        var info = new GUITHREADINFO { cbSize = Marshal.SizeOf<GUITHREADINFO>() };
        return GetGUIThreadInfo(tid, ref info) ? info.hwndFocus : IntPtr.Zero;
    }

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

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

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
    public const uint GW_HWNDPREV = 3;  // prev in Z-order (in front of)
    public const uint GW_HWNDNEXT = 2;  // next in Z-order (behind)
    public const uint GW_CHILD = 5;     // first child window

    [DllImport("user32.dll")]
    public static extern bool PtInRect(ref RECT lprc, POINT pt);

    [DllImport("user32.dll")]
    public static extern int GetWindowLongW(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    public static extern int SetWindowLongW(IntPtr hWnd, int nIndex, int dwNewLong);

    public const int GWL_STYLE = -16;
    public const int GWL_EXSTYLE = -20;

    // WS_* — Window Styles (GWL_STYLE)
    public const int WS_POPUP       = unchecked((int)0x80000000);
    public const int WS_CHILD       = 0x40000000;
    public const int WS_VISIBLE     = 0x10000000;
    public const int WS_DISABLED    = 0x08000000;
    public const int WS_TABSTOP     = 0x00010000;  // tab stop (dialog navigation)
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
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    public const uint LWA_ALPHA = 0x02;

    // ── Cursor detection (EditCursor hint for DYN-A11Y) ──
    [StructLayout(LayoutKind.Sequential)]
    public struct CURSORINFO
    {
        public int cbSize;
        public int flags;
        public IntPtr hCursor;
        public POINT ptScreenPos;
    }
    [DllImport("user32.dll")] public static extern bool GetCursorInfo(ref CURSORINFO pci);
    [DllImport("user32.dll")] public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);
    [DllImport("user32.dll")] public static extern bool DestroyIcon(IntPtr hIcon);
    [DllImport("user32.dll")] public static extern IntPtr LoadCursorW(IntPtr hInstance, int lpCursorName);
    public const int IDC_IBEAM = 32513;
    public const int IDC_HAND  = 32649;
    public const int SM_CXCURSOR = 13;
    public const int SM_CYCURSOR = 14;

    [StructLayout(LayoutKind.Sequential)]
    public struct ICONINFO
    {
        public bool fIcon;
        public uint xHotspot;
        public uint yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    /// <summary>
    /// Returns the current cursor screen rectangle based on the hotspot and system cursor size.
    /// This is used for prompt containment checks, not just pointer-point containment.
    /// </summary>
    public static bool TryGetCurrentCursorRect(out Rectangle rect)
    {
        rect = Rectangle.Empty;
        try
        {
            var ci = new CURSORINFO { cbSize = Marshal.SizeOf<CURSORINFO>() };
            if (!GetCursorInfo(ref ci) || ci.hCursor == IntPtr.Zero)
                return false;

            if (!GetIconInfo(ci.hCursor, out var ii))
                return false;

            try
            {
                int width = GetSystemMetrics(SM_CXCURSOR);
                int height = GetSystemMetrics(SM_CYCURSOR);
                if (width <= 0 || height <= 0)
                    return false;

                rect = new Rectangle(
                    ci.ptScreenPos.X - (int)ii.xHotspot,
                    ci.ptScreenPos.Y - (int)ii.yHotspot,
                    width,
                    height);
                return true;
            }
            finally
            {
                if (ii.hbmMask != IntPtr.Zero) DeleteObject(ii.hbmMask);
                if (ii.hbmColor != IntPtr.Zero) DeleteObject(ii.hbmColor);
            }
        }
        catch
        {
            rect = Rectangle.Empty;
            return false;
        }
    }

    /// <summary>
    /// Check if current cursor is IBeam (text edit indicator).
    /// Call during/after mouse-based actions (click, hover) for free Edit hint.
    /// </summary>
    public static bool IsCurrentCursorIBeam()
    {
        var ci = new CURSORINFO { cbSize = Marshal.SizeOf<CURSORINFO>() };
        if (!GetCursorInfo(ref ci)) return false;
        var ibeam = LoadCursorW(IntPtr.Zero, IDC_IBEAM);
        return ci.hCursor == ibeam;
    }

    /// <summary>Check if current cursor is Hand (link/clickable indicator).</summary>
    public static bool IsCurrentCursorHand()
    {
        var ci = new CURSORINFO { cbSize = Marshal.SizeOf<CURSORINFO>() };
        if (!GetCursorInfo(ref ci)) return false;
        var hand = LoadCursorW(IntPtr.Zero, IDC_HAND);
        return ci.hCursor == hand;
    }

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

    // ── Console codepage ──────────────────────────────────────────
    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleCP(uint wCodePageID);
    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleOutputCP(uint wCodePageID);

    // ── DPI ──────────────────────────────────────────────────────
    [DllImport("shcore.dll")]
    public static extern int SetProcessDpiAwareness(int awareness);

    /// <summary>Returns the DPI for a window (e.g. 96=100%, 144=150%, 192=200%). Win10 1607+.</summary>
    [DllImport("user32.dll")]
    public static extern uint GetDpiForWindow(IntPtr hwnd);

    /// <summary>Thread DPI awareness context switch. Win10 1607+. Returns previous context.</summary>
    [DllImport("user32.dll")]
    public static extern IntPtr SetThreadDpiAwarenessContext(IntPtr dpiContext);

    // DPI awareness contexts
    public static readonly IntPtr DPI_AWARENESS_CONTEXT_UNAWARE = new IntPtr(-1);
    public static readonly IntPtr DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = new IntPtr(-2);
    public static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);

    /// <summary>
    /// Get DPI scale factor by comparing physical vs logical client rect.
    /// Temporarily switches to DPI-unaware to get the MFC app's logical coords.
    /// Returns (dpiScale, logicalClientW, logicalClientH).
    /// </summary>
    public static (double scale, int logicalW, int logicalH) GetDpiScaleForMfc(IntPtr hwnd)
    {
        // Physical client (DPI-aware)
        GetClientRect(hwnd, out var physRect);
        int physW = physRect.Right - physRect.Left;
        int physH = physRect.Bottom - physRect.Top;

        // Logical client (temporarily DPI-unaware)
        int logW, logH;
        var prev = SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_UNAWARE);
        try
        {
            GetClientRect(hwnd, out var logRect);
            logW = logRect.Right - logRect.Left;
            logH = logRect.Bottom - logRect.Top;
        }
        finally
        {
            SetThreadDpiAwarenessContext(prev);
        }

        double scale = logW > 0 ? (double)physW / logW : 1.0;
        return (scale, logW, logH);
    }

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    // SM_XVIRTUALSCREEN = 76, SM_YVIRTUALSCREEN = 77
    // SM_CXVIRTUALSCREEN = 78, SM_CYVIRTUALSCREEN = 79

    // ── Clipboard ────────────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    public static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    public static extern bool EmptyClipboard();

    [DllImport("user32.dll")]
    public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll")]
    public static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll")]
    public static extern bool GlobalUnlock(IntPtr hMem);

    // ── SendMessageTimeout (responsive check: SMTO_ABORTIFHUNG) ──
    public const uint WM_NULL = 0x0000;
    public const uint SMTO_ABORTIFHUNG = 0x0002;

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr SendMessageTimeoutW(
        IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam,
        uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

    // ── MDI ──────────────────────────────────────────────────────
    public const uint WM_CLOSE = 0x0010;
    public const uint WM_COPYDATA = 0x004A;
    public const uint WM_GETTEXT = 0x000D;
    public const uint WM_GETTEXTLENGTH = 0x000E;
    public const uint WM_SETTEXT = 0x000C;
    public const uint WM_MDIGETACTIVE = 0x0229;
    public const uint WM_CHAR       = 0x0102;
    public const uint WM_KEYDOWN    = 0x0100;
    public const uint WM_KEYUP      = 0x0101;
    public const uint WM_SYSKEYDOWN = 0x0104;
    public const uint WM_SYSKEYUP   = 0x0105;

    public const uint VK_SHIFT   = 0x10;
    public const uint VK_CONTROL = 0x11;
    public const uint VK_MENU    = 0x12; // Alt
    public const uint WM_MOUSEMOVE = 0x0200;
    public const uint WM_LBUTTONDOWN = 0x0201;
    public const uint WM_LBUTTONUP = 0x0202;
    public const uint WM_NCHITTEST = 0x0084;

    // WM_NCHITTEST return values
    public const int HTCLIENT = 1;
    public const int HTCAPTION = 2;
    public const int HTSYSMENU = 3;
    public const int HTMINBUTTON = 8;
    public const int HTMAXBUTTON = 9;
    public const int HTCLOSE = 20;

    /// <summary>Pack (x, y) client coords into lParam for mouse messages.</summary>
    public static IntPtr MakeLParam(int x, int y) => (IntPtr)((y << 16) | (x & 0xFFFF));

    /// <summary>Pack (x, y) screen coords into lParam for WM_NCHITTEST.</summary>
    public static IntPtr MakeScreenLParam(int x, int y) => (IntPtr)((y << 16) | (x & 0xFFFF));

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

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern bool QueryFullProcessImageNameW(
        IntPtr hProcess,
        uint dwFlags,
        StringBuilder lpExeName,
        ref uint lpdwSize);

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

    /// <summary>
    /// Fast best-effort process name via QueryFullProcessImageNameW.
    /// Returns executable file name without extension, or null on failure.
    /// Avoids slower Process.GetProcessById(...).ProcessName paths that may be hooked/stalled.
    /// </summary>
    public static string? TryGetProcessNameFast(uint processId)
    {
        var hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
        if (hProcess == IntPtr.Zero) return null;
        try
        {
            uint size = 1024;
            var sb = new StringBuilder((int)size);
            if (!QueryFullProcessImageNameW(hProcess, 0, sb, ref size))
                return null;

            var path = sb.ToString();
            if (string.IsNullOrWhiteSpace(path)) return null;
            var file = Path.GetFileName(path);
            if (string.IsNullOrWhiteSpace(file)) return null;
            return Path.GetFileNameWithoutExtension(file);
        }
        catch { return null; }
        finally { CloseHandle(hProcess); }
    }

    // ── UIPI integrity level ───────────────────────────────────

    [DllImport("advapi32.dll", EntryPoint = "GetTokenInformation", SetLastError = true)]
    private static extern bool GetTokenInformationPtr(
        IntPtr TokenHandle, int TokenInformationClass,
        IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

    private const int TokenIntegrityLevel = 25;

    /// <summary>
    /// Get integrity level of a process. Returns 0x1000=Low, 0x2000=Medium, 0x3000=High, 0x4000=System.
    /// Returns 0x2000 (Medium) on any access failure.
    /// </summary>
    public static uint GetProcessIntegrityLevel(uint processId)
    {
        var hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
        if (hProcess == IntPtr.Zero) return 0x2000;
        try
        {
            if (!OpenProcessToken(hProcess, TOKEN_QUERY, out var hToken)) return 0x2000;
            try { return ReadIntegrityLevel(hToken); }
            finally { CloseHandle(hToken); }
        }
        finally { CloseHandle(hProcess); }
    }

    /// <summary>Get integrity level of the current process.</summary>
    public static uint GetCurrentProcessIntegrityLevel()
    {
        if (!OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, TOKEN_QUERY, out var hToken))
            return 0x2000;
        try { return ReadIntegrityLevel(hToken); }
        finally { CloseHandle(hToken); }
    }

    private static uint ReadIntegrityLevel(IntPtr hToken)
    {
        GetTokenInformationPtr(hToken, TokenIntegrityLevel, IntPtr.Zero, 0, out uint size);
        if (size == 0) return 0x2000;
        var buf = Marshal.AllocHGlobal((int)size);
        try
        {
            if (!GetTokenInformationPtr(hToken, TokenIntegrityLevel, buf, size, out _)) return 0x2000;
            // TOKEN_MANDATORY_LABEL: SID_AND_ATTRIBUTES { PSID Sid; DWORD Attributes; }
            // Sid ptr at buf+0, SID layout: Revision[1] SubAuthorityCount[1] IdentifierAuthority[6] SubAuthority[n][4]
            var sidPtr = Marshal.ReadIntPtr(buf, 0);
            byte subAuthCount = Marshal.ReadByte(sidPtr, 1);
            int subAuthOffset = 8 + (subAuthCount - 1) * 4;
            return (uint)Marshal.ReadInt32(sidPtr, subAuthOffset);
        }
        finally { Marshal.FreeHGlobal(buf); }
    }

    // ── User idle detection ────────────────────────────────────

    [StructLayout(LayoutKind.Sequential)]
    public struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("user32.dll")]
    public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    /// <summary>
    /// Milliseconds since the last user keyboard/mouse input event.
    /// Uses GetLastInputInfo + Environment.TickCount delta.
    /// </summary>
    public static uint GetUserIdleMs()
    {
        var lii = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<LASTINPUTINFO>() };
        if (!GetLastInputInfo(ref lii)) return uint.MaxValue;
        return (uint)((Environment.TickCount & 0x7FFFFFFF) - (lii.dwTime & 0x7FFFFFFF));
    }

    // ── Sleep / Display prevention ─────────────────────────────
    [DllImport("kernel32.dll")]
    public static extern uint SetThreadExecutionState(uint esFlags);

    public const uint ES_CONTINUOUS = 0x80000000;
    public const uint ES_SYSTEM_REQUIRED = 0x00000001;
    public const uint ES_DISPLAY_REQUIRED = 0x00000002;

    // ── Screen capture ───────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    public static extern uint GetPixel(IntPtr hdc, int x, int y);

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

    // ── GDI clip region + viewport (PrintWindow sub-region optimization) ──

    [DllImport("gdi32.dll")]
    public static extern bool SetViewportOrgEx(IntPtr hdc, int x, int y, out POINT lppt);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

    [DllImport("gdi32.dll")]
    public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

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

    // ── Paint detection (hollow invoke check) ──────────────────
    /// <summary>Returns true if update region is non-empty (pending paint = something was redrawn).</summary>
    [DllImport("user32.dll")]
    public static extern bool GetUpdateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

    /// <summary>Validates (clears) the update region so next GetUpdateRect starts fresh.</summary>
    [DllImport("user32.dll")]
    public static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

    // ── Focus / Alert ──────────────────────────────────────────
    [DllImport("user32.dll")]
    public static extern bool MessageBeep(uint uType);

    public const uint MB_ICONEXCLAMATION = 0x00000030;
    public const uint MB_ICONASTERISK = 0x00000040;

    [DllImport("user32.dll")]
    public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

    // GDI/USER object counts for a process handle (GR_GDIOBJECTS=0, GR_USEROBJECTS=1)
    [DllImport("user32.dll")]
    public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

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
    /// Core SetForegroundWindow with AttachThreadInput trick.
    /// No guards — guards are applied by callers (SetForegroundWindow / SmartSetForegroundWindow).
    /// </summary>
    private static bool SetForegroundWindowCore(IntPtr hWnd)
    {
        BringWindowToTop(hWnd);
        SetForegroundWindowRaw(hWnd);
        if (GetForegroundWindow() == hWnd) return true;

        // AttachThreadInput trick — Z-order + keyboard focus 동시 확보
        var fgHwnd = GetForegroundWindow();
        uint fgThread = GetWindowThreadProcessId(fgHwnd, out _);
        uint ourThread = GetCurrentThreadId();

        if (fgThread != ourThread)
        {
            AttachThreadInput(ourThread, fgThread, true);
            try
            {
                BringWindowToTop(hWnd);
                SetForegroundWindowRaw(hWnd);
            }
            finally
            {
                AttachThreadInput(ourThread, fgThread, false);
            }
        }

        return GetForegroundWindow() == hWnd;
    }

    /// <summary>
    /// Force foreground window — AttachThreadInput trick, no FocuslessGuard.
    /// Use ONLY for focus RESTORATION after a detected theft (defensive).
    /// Do NOT use for offensive focus acquisition.
    /// </summary>
    public static bool ForceForegroundWindow(IntPtr hWnd)
        => SetForegroundWindowCore(hWnd);

    /// <summary>
    /// Public SetForegroundWindow proxy — routes through FocuslessGuard + AttachThreadInput trick.
    /// Use for any focus action (window activation, bring-to-front).
    /// BLOCKED by FocuslessGuard in focusless mode.
    /// Does NOT require Probe() (no AssertReadiness) — safe for dialog/restore-adjacent contexts.
    /// For full input acquisition (SendInput path): use SmartSetForegroundWindow.
    /// </summary>
    public static bool SetForegroundWindow(IntPtr hWnd)
    {
        if (GetForegroundWindow() == hWnd) return true;
        Input.FocuslessGuard.AssertAllowed("SetForegroundWindow");
        return SetForegroundWindowCore(hWnd);
    }

    /// <summary>
    /// Smart SetForegroundWindow — full input acquisition gate.
    /// Windows normally blocks SetForegroundWindow from non-foreground threads.
    /// By attaching our input queue to the foreground thread, we gain permission.
    /// BLOCKED by FocuslessGuard + AssertReadiness (requires Probe() before call).
    /// </summary>
    public static bool SmartSetForegroundWindow(IntPtr hWnd)
    {
        // 정확 핸들 비교 — 같은 프로세스의 다른 창은 "foreground"가 아님!
        if (GetForegroundWindow() == hWnd) return true;

        // FocuslessGuard: block if enabled (only when we actually NEED to steal focus)
        Input.FocuslessGuard.AssertAllowed("SetForegroundWindow");

        // Readiness gate: delegates to AssertReadiness which throws if Probe() was not called.
        // Also logs [IDLE] idle time on success.
        WKAppBot.Win32.Input.InputReadiness.AssertReadiness("SmartSetForegroundWindow");
        var _idleMs = GetUserIdleMs();
        var _idleStr = _idleMs >= 60000 ? $"{_idleMs / 60000}m {_idleMs / 1000 % 60}s"
                     : _idleMs >= 1000  ? $"{_idleMs / 1000.0:F1}s"
                     :                    $"{_idleMs}ms";
        Console.WriteLine($"[IDLE] user input {_idleStr} ago");

        // [FOCUS-GUARD] Active-user guard: block if user typed/clicked recently.
        // Probe() approved at T=0 (idle), but user may have started typing between
        // Probe() and this focus steal → race condition → steal disrupts user input.
        // CheckActiveGuard blocks (return false) if idleMs < threshold AND no grace period.
        // Grace period: Probe yield-approval click itself resets idle → allowed for 3s.
        if (!WKAppBot.Win32.Input.InputReadiness.CheckActiveGuard("SmartSetForegroundWindow"))
            return false;

        // Keyboard input lock: if another session is currently sending keystrokes, yield focus.
        // "먼저 잡은 넘이 우선권" — don't interrupt ongoing typing in another process.
        if (Input.KeyboardInput.IsInputLockedByOther())
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[FOCUS] ⚠ SmartSetForegroundWindow skipped — another session is typing (hwnd={hWnd:X8})");
            Console.ResetColor();
            return false;
        }

        return SetForegroundWindowCore(hWnd);
    }

    /// <summary>Get window text as string (convenience wrapper).</summary>
    public static string GetWindowTextW(IntPtr hWnd)
    {
        var sb = new System.Text.StringBuilder(1024);
        GetWindowTextW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    /// <summary>
    /// Get window text with timeout protection. Uses SendMessageTimeoutW + SMTO_ABORTIFHUNG
    /// to avoid blocking on hung windows. Returns "" if timeout or hung.
    /// </summary>
    public static string GetWindowTextSafe(IntPtr hWnd, uint timeoutMs = 50)
    {
        // Step 1: Get text length (with timeout)
        var ok = SendMessageTimeoutW(hWnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero,
            SMTO_ABORTIFHUNG, timeoutMs, out var lenResult);
        if (ok == IntPtr.Zero) return ""; // hung or timeout
        int len = lenResult.ToInt32();
        if (len <= 0) return "";

        // Step 2: Get text (with timeout)
        var buf = System.Runtime.InteropServices.Marshal.AllocHGlobal((len + 1) * 2);
        try
        {
            ok = SendMessageTimeoutW(hWnd, WM_GETTEXT, (IntPtr)(len + 1), buf,
                SMTO_ABORTIFHUNG, timeoutMs, out _);
            if (ok == IntPtr.Zero) return ""; // hung or timeout
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(buf) ?? "";
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(buf);
        }
    }

    // ── Process CWD reading via NtQueryInformationProcess → PEB ──────
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
        [Out] byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

    [DllImport("ntdll.dll")]
    public static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass,
        ref ProcessBasicInformation processInformation, int processInformationLength, out int returnLength);

    public const uint PROCESS_QUERY_INFORMATION = 0x0400;
    public const uint PROCESS_VM_READ           = 0x0010;

    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessBasicInformation
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr Reserved3;
    }

    /// <summary>
    /// Read the current working directory of any process via NtQueryInformationProcess → PEB.
    /// Returns null on failure (access denied, 32/64-bit mismatch, etc.).
    /// </summary>
    public static string? GetProcessCurrentDirectory(int pid)
    {
        IntPtr hProcess = IntPtr.Zero;
        try
        {
            hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (uint)pid);
            if (hProcess == IntPtr.Zero) return null;

            var pbi = new ProcessBasicInformation();
            if (NtQueryInformationProcess(hProcess, 0, ref pbi,
                Marshal.SizeOf<ProcessBasicInformation>(), out _) != 0)
                return null;

            // PEB: ProcessParameters pointer at offset 0x20 (64-bit) or 0x10 (32-bit)
            int ppOffset = IntPtr.Size == 8 ? 0x20 : 0x10;
            var pebBuf = new byte[ppOffset + IntPtr.Size];
            if (!ReadProcessMemory(hProcess, pbi.PebBaseAddress, pebBuf, pebBuf.Length, out _))
                return null;

            var ppAddr = IntPtr.Size == 8
                ? new IntPtr(BitConverter.ToInt64(pebBuf, ppOffset))
                : new IntPtr(BitConverter.ToInt32(pebBuf, ppOffset));

            // RTL_USER_PROCESS_PARAMETERS: CurrentDirectory.DosPath UNICODE_STRING
            // 64-bit offset 0x38: Length(2) MaxLen(2) pad(4) Buffer*(8)
            // 32-bit offset 0x24: Length(2) MaxLen(2) Buffer*(4)
            int cdOffset = IntPtr.Size == 8 ? 0x38 : 0x24;
            int cdSize   = 2 + 2 + (IntPtr.Size == 8 ? 4 : 0) + IntPtr.Size;
            var ppBuf = new byte[cdOffset + cdSize];
            if (!ReadProcessMemory(hProcess, ppAddr, ppBuf, ppBuf.Length, out _))
                return null;

            ushort pathLen = BitConverter.ToUInt16(ppBuf, cdOffset);
            var bufPtr = IntPtr.Size == 8
                ? new IntPtr(BitConverter.ToInt64(ppBuf, cdOffset + 8))
                : new IntPtr(BitConverter.ToInt32(ppBuf, cdOffset + 4));

            if (pathLen == 0 || bufPtr == IntPtr.Zero) return null;

            var pathBuf = new byte[pathLen];
            if (!ReadProcessMemory(hProcess, bufPtr, pathBuf, pathLen, out _)) return null;

            var result = System.Text.Encoding.Unicode.GetString(pathBuf).TrimEnd('\\', '/');
            return string.IsNullOrWhiteSpace(result) ? null : result;
        }
        catch { return null; }
        finally { if (hProcess != IntPtr.Zero) CloseHandle(hProcess); }
    }

    /// <summary>
    /// Read the command line of any process via NtQueryInformationProcess → PEB.
    /// Returns null on failure.
    /// </summary>
    /// <summary>
    /// Returns the parent process ID for the given PID using NtQueryInformationProcess.
    /// Returns 0 on failure.
    /// </summary>
    public static int GetParentProcessId(int pid)
    {
        IntPtr hProcess = IntPtr.Zero;
        try
        {
            hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, false, (uint)pid);
            if (hProcess == IntPtr.Zero) return 0;
            var pbi = new ProcessBasicInformation();
            if (NtQueryInformationProcess(hProcess, 0, ref pbi, Marshal.SizeOf<ProcessBasicInformation>(), out _) != 0)
                return 0;
            return (int)pbi.Reserved3.ToInt64(); // Reserved3 = InheritedFromUniqueProcessId
        }
        catch { return 0; }
        finally { if (hProcess != IntPtr.Zero) CloseHandle(hProcess); }
    }

    public static string? GetProcessCommandLine(int pid)
    {
        IntPtr hProcess = IntPtr.Zero;
        try
        {
            hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (uint)pid);
            if (hProcess == IntPtr.Zero) return null;

            var pbi = new ProcessBasicInformation();
            if (NtQueryInformationProcess(hProcess, 0, ref pbi,
                Marshal.SizeOf<ProcessBasicInformation>(), out _) != 0)
                return null;

            int ppOffset = IntPtr.Size == 8 ? 0x20 : 0x10;
            var pebBuf = new byte[ppOffset + IntPtr.Size];
            if (!ReadProcessMemory(hProcess, pbi.PebBaseAddress, pebBuf, pebBuf.Length, out _))
                return null;

            var ppAddr = IntPtr.Size == 8
                ? new IntPtr(BitConverter.ToInt64(pebBuf, ppOffset))
                : new IntPtr(BitConverter.ToInt32(pebBuf, ppOffset));

            // RTL_USER_PROCESS_PARAMETERS: CommandLine UNICODE_STRING
            // 64-bit offset 0x70: Length(2) MaxLen(2) pad(4) Buffer*(8)
            // 32-bit offset 0x40: Length(2) MaxLen(2) Buffer*(4)
            int clOffset = IntPtr.Size == 8 ? 0x70 : 0x40;
            int clSize   = 2 + 2 + (IntPtr.Size == 8 ? 4 : 0) + IntPtr.Size;
            var ppBuf = new byte[clOffset + clSize];
            if (!ReadProcessMemory(hProcess, ppAddr, ppBuf, ppBuf.Length, out _))
                return null;

            ushort pathLen = BitConverter.ToUInt16(ppBuf, clOffset);
            var bufPtr = IntPtr.Size == 8
                ? new IntPtr(BitConverter.ToInt64(ppBuf, clOffset + 8))
                : new IntPtr(BitConverter.ToInt32(ppBuf, clOffset + 4));

            if (pathLen == 0 || bufPtr == IntPtr.Zero) return null;

            var pathBuf = new byte[pathLen];
            if (!ReadProcessMemory(hProcess, bufPtr, pathBuf, pathLen, out _)) return null;

            var result = System.Text.Encoding.Unicode.GetString(pathBuf);
            return string.IsNullOrWhiteSpace(result) ? null : result;
        }
        catch { return null; }
        finally { if (hProcess != IntPtr.Zero) CloseHandle(hProcess); }
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
    public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
    public const uint KEYEVENTF_KEYUP = 0x0002;
    public const uint KEYEVENTF_UNICODE = 0x0004;
    public const uint KEYEVENTF_SCANCODE = 0x0008;
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
