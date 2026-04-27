using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>
    /// Minimize Chrome window (focusless -- does not steal focus from user's active window).
    /// CDP still works perfectly when Chrome is minimized!
    /// </summary>
    public void MinimizeChromeWindow()
    {
        var hwnd = FindChromeMainWindow();
        if (hwnd != IntPtr.Zero)
        {
            ShowWindow(hwnd, SW_SHOWMINNOACTIVE);
            ScheduleMinimizeDump("minimize-window", hwnd);
        }
    }

    /// <summary>
    /// Restore Chrome window without stealing focus.
    /// Uses SetWindowPlacement(SW_SHOWNOACTIVATE) which actually restores from
    /// minimized state, unlike ShowWindow(SW_SHOWNOACTIVATE) which is a no-op
    /// when the window is iconic.
    /// </summary>
    public void RestoreChromeWindow()
    {
        var hwnd = FindChromeMainWindow();
        if (hwnd == IntPtr.Zero) return;

        var iconic = IsIconic(hwnd);

        if (iconic)
        {
            // ShowWindow(SW_RESTORE=9) is the only reliable way to un-minimize.
            // It briefly activates, so restore user's foreground immediately after.
            var prevFg = GetForegroundWindow();
            ShowWindow(hwnd, 9); // SW_RESTORE
            CancelMinimizeDump("restore-window");
            if (prevFg != IntPtr.Zero && prevFg != hwnd)
                ForceForegroundWindow(prevFg); // AttachThreadInput -- SW_RESTORE briefly activates
        }
        else
        {
            ShowWindow(hwnd, SW_SHOWNOACTIVATE);
            CancelMinimizeDump("restore-window-noactivate");
        }
    }

    /// <summary>
    /// Set the Chrome window title bar directly via Win32 SetWindowText.
    /// Chrome ignores document.title for the window title in some profiles,
    /// so we force it via Win32 P/Invoke on the Chrome main window.
    /// </summary>
    public async Task SetWindowTitleAsync(string? customTitle = null)
    {
        try
        {
            // Get page title from CDP
            var pageTitle = customTitle ?? await EvalAsync("document.title") ?? "";
            if (string.IsNullOrEmpty(pageTitle))
                pageTitle = "WKWebBot v0.1";

            // Find Chrome process from CDP port (localhost:{port})
            var chromeHwnd = FindChromeMainWindow();
            if (chromeHwnd != IntPtr.Zero)
            {
                SetWindowTextW(chromeHwnd, pageTitle);
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>
    /// Find the main Chrome window handle by enumerating top-level windows.
    /// Uses ChromePid to find the exact Chrome instance connected via CDP.
    /// Falls back to first visible Chrome_WidgetWin_1 if PID not available.
    /// </summary>

    // Reuse shared browser list from TabManagement for minimize guard.

    private IntPtr FindChromeMainWindow()
    {
        IntPtr found = IntPtr.Zero;
        var sb = new StringBuilder(512);
        var targetPid = ChromePid;

        EnumWindows((hwnd, _) =>
        {
            GetClassNameW(hwnd, sb, sb.Capacity);
            var cls = sb.ToString();
            if (cls != "Chrome_WidgetWin_1") return true; // continue

            // Check if visible main window (not popup/child)
            if (!IsWindowVisible(hwnd)) return true;
            var style = GetWindowLong(hwnd, -16); // GWL_STYLE
            if ((style & 0x00C00000) == 0) return true; // WS_CAPTION required

            // Match by PID -- REQUIRED to avoid hitting VS Code or other Electron apps
            GetWindowThreadProcessId(hwnd, out var pid);
            if (targetPid > 0 && pid != targetPid) return true; // wrong process
            if (targetPid == 0) return true; // PID unknown -> refuse to guess

            // Final safety: reject non-browser Electron apps (VS Code, Slack, Discord...).
            // DetectCdpPort can mistakenly pick up VS Code's Chromium DevTools endpoint,
            // setting ChromePid to VS Code's PID -> this check prevents VS Code minimize.
            if (!IsBrowserProcess((int)pid))
            {
                try { using var p2 = System.Diagnostics.Process.GetProcessById((int)pid);
                      Console.Error.WriteLine($"[CDP:MINIMIZE] BLOCKED -- '{p2.ProcessName}' is not a browser (hwnd=0x{hwnd:X8})"); }
                catch { }
                return true; // skip
            }

            found = hwnd;
            return false; // stop
        }, IntPtr.Zero);

        return found;
    }

    // Win32 P/Invoke for window title management
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SetWindowTextW(IntPtr hWnd, string lpString);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("user32.dll")]
    private static extern bool BringWindowToTop(IntPtr hWnd);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    /// <summary>Force foreground via AttachThreadInput -- works even when we lost focus rights.</summary>
    private static void ForceForegroundWindow(IntPtr hWnd)
    {
        BringWindowToTop(hWnd);
        SetForegroundWindow(hWnd);
        if (GetForegroundWindow() == hWnd) return;

        var fgHwnd = GetForegroundWindow();
        uint fgThread = GetWindowThreadProcessId(fgHwnd, out _);
        uint ourThread = GetCurrentThreadId();
        if (fgThread == ourThread) return;

        AttachThreadInput(ourThread, fgThread, true);
        try { BringWindowToTop(hWnd); SetForegroundWindow(hWnd); }
        finally { AttachThreadInput(ourThread, fgThread, false); }
    }

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [StructLayout(LayoutKind.Sequential)]
    private struct WINDOWPLACEMENT
    {
        public int length, flags, showCmd;
        public System.Drawing.Point ptMinPosition, ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }

    private const int SW_MINIMIZE = 6;
    private const int SW_SHOWNOACTIVATE = 4;   // Show at previous size/position without activating
    private const int SW_SHOWMINNOACTIVE = 7;  // Show minimized without activating

    // -- Bot window decoration --------------------------------------------------

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);
    private const int DWMWA_BORDER_COLOR = 34; // Windows 11 only

    // COLORREF = 0x00BBGGRR
    private static int ToCOLORREF(byte r, byte g, byte b) => r | (g << 8) | (b << 16);
    private static readonly int COLORREF_DEFAULT = unchecked((int)0xFFFFFFFF); // DWMWA_COLOR_DEFAULT

    /// <summary>
    /// Set the DWM window border color (Windows 11+). Pass null to restore default.
    /// Color tuple: (R, G, B). Non-blocking, best-effort.
    /// </summary>
    public void SetDwmBorderColor((byte R, byte G, byte B)? color)
    {
        var hwnd = FindChromeMainWindow();
        if (hwnd == IntPtr.Zero) return;
        try
        {
            var col = color.HasValue ? ToCOLORREF(color.Value.R, color.Value.G, color.Value.B) : COLORREF_DEFAULT;
            DwmSetWindowAttribute(hwnd, DWMWA_BORDER_COLOR, ref col, sizeof(int));
        }
        catch { /* Windows 10 or earlier -- no-op */ }
    }

    private bool _overlayInjected;

    /// <summary>
    /// Inject a thin fixed-position CSS overlay into the page that pulses on each bot tick.
    /// Call TickBotOverlayAsync() on each CDP command to animate it.
    /// Call SetBotOverlayAsync(false) to remove.
    /// </summary>
    public async Task SetBotOverlayAsync(bool show, string hexColor = "#ff6600")
    {
        if (!show)
        {
            _overlayInjected = false;
            try { await EvalAsync("(()=>{var el=document.getElementById('__wkbot_overlay');if(el)el.remove();})()"); } catch { }
            return;
        }
        _overlayInjected = true;
        var css = $@"
(()=>{{
  if(document.getElementById('__wkbot_overlay'))return;
  var s=document.createElement('style');
  s.id='__wkbot_overlay_css';
  s.textContent=`
    #__wkbot_overlay{{
      position:fixed;inset:0;pointer-events:none;z-index:2147483647;
      border:3px solid {hexColor};box-sizing:border-box;opacity:0;
    }}
    @keyframes __wkbot_fade{{0%{{opacity:0.9;}}100%{{opacity:0;}}}}
    @keyframes __wkbot_pulse{{
      0%,100%{{opacity:0.7;border-width:3px;}}
      50%{{opacity:0.25;border-width:6px;}}
    }}
    #__wkbot_overlay.streaming{{
      animation:__wkbot_pulse 0.9s ease-in-out infinite!important;
    }}
  `;
  document.head.appendChild(s);
  var d=document.createElement('div');
  d.id='__wkbot_overlay';
  document.body.appendChild(d);
  window.__wkBotTick=function(){{
    var el=document.getElementById('__wkbot_overlay');
    if(!el||el.classList.contains('streaming'))return;
    el.style.animation='none';
    el.offsetHeight;
    el.style.animation='__wkbot_fade 0.6s ease-out forwards';
  }};
  window.__wkBotStreaming=function(on){{
    var el=document.getElementById('__wkbot_overlay');
    if(!el)return;
    if(on){{el.classList.add('streaming');el.style.animation='';}}
    else{{el.classList.remove('streaming');el.style.animation='none';el.offsetHeight;el.style.animation='__wkbot_fade 0.4s ease-out forwards';}}
  }};
}})()";
        try { await EvalAsync(css); } catch { }
    }
}
