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
            ShowWindow(hwnd, SW_SHOWMINNOACTIVE);
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
            if (prevFg != IntPtr.Zero && prevFg != hwnd)
                SetForegroundWindow(prevFg);
        }
        else
        {
            ShowWindow(hwnd, SW_SHOWNOACTIVATE);
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

            // Match by PID — REQUIRED to avoid hitting VS Code or other Electron apps
            GetWindowThreadProcessId(hwnd, out var pid);
            if (targetPid > 0 && pid != targetPid) return true; // wrong Chrome instance
            if (targetPid == 0) return true; // PID unknown → refuse to guess (could be VS Code)

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

    /// <summary>
    /// Set files on a file input element (e.g., for file upload).
    /// Uses CDP DOM.setFileInputFiles to programmatically set files without opening a file dialog.
    /// </summary>
    public async Task<bool> SetFileInputAsync(string selector, string filePath)
    {
        // Enable DOM domain (required for DOM operations)
        await SendAsync("DOM.enable");

        // Get document root first
        await SendAsync("DOM.getDocument");

        // Get the element's remote object via Runtime.evaluate
        var evalResult = await SendAsync("Runtime.evaluate", new JsonObject
        {
            ["expression"] = $"document.querySelector('{selector}')",
            ["returnByValue"] = false
        });

        var exDetailsFile = evalResult?["exceptionDetails"];
        if (exDetailsFile != null)
        {
            var msg = exDetailsFile["exception"]?["description"]?.GetValue<string>()
                   ?? exDetailsFile["text"]?.GetValue<string>() ?? "unknown JS error";
            Console.WriteLine($"[CDP:JS-ERR] SetFileInput: {msg}");
        }
        var objectId = evalResult?["result"]?["objectId"]?.GetValue<string>();
        if (string.IsNullOrEmpty(objectId))
            return false;

        // Request DOM node for the JS object
        var reqResult = await SendAsync("DOM.requestNode", new JsonObject
        {
            ["objectId"] = objectId
        });

        var nodeId = reqResult?["nodeId"]?.GetValue<int>() ?? 0;
        if (nodeId == 0)
            return false;

        // Set the file using DOM.setFileInputFiles with nodeId
        await SendAsync("DOM.setFileInputFiles", new JsonObject
        {
            ["files"] = new JsonArray { filePath },
            ["nodeId"] = nodeId
        });

        return true;
    }

    /// <summary>Wait for an element to appear (polling).</summary>
    public async Task<bool> WaitForElementAsync(string selector, int timeoutMs = 5000)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var js = $"document.querySelector('{selector}') !== null";
            var result = await EvalAsync(js);
            if (result == "true") return true;
            await Task.Delay(200);
        }
        return false;
    }

    /// <summary>Get page HTML source.</summary>
    public async Task<string?> GetHtmlAsync()
    {
        return await EvalAsync("document.documentElement.outerHTML");
    }

    // ── Window management (CDP Browser domain + Target domain) ──

    /// <summary>
    /// Expected WebBot window bounds (right side of virtual screen, near top).
    /// Position: rightmost monitor upper area, 800x600.
    /// Uses GetSystemMetrics (physical pixels) — same coordinate space as SetWindowPos/CDP.
    /// For WPF Eye alignment, CLI computes separately using SystemParameters (logical px).
    /// </summary>
    public static (int X, int Y, int W, int H) ExpectedBounds => ComputeExpectedBounds();
    private const int BoundsTolerance = 50;

    private static (int X, int Y, int W, int H) ComputeExpectedBounds()
    {
        const int W = 800, H = 600;
        const int Corner = 20; // offset from top-right corner of rightmost monitor
        try
        {
            // Virtual screen = union of all monitors
            int vx = GetSystemMetrics(76);  // SM_XVIRTUALSCREEN
            int vw = GetSystemMetrics(78);  // SM_CXVIRTUALSCREEN
            int rightEdge = vx + vw; // rightmost pixel of virtual screen

            // Find the rightmost monitor's top-Y using MonitorFromPoint + GetMonitorInfo
            // Point at top-right corner of virtual screen → belongs to rightmost monitor
            int monitorTop = 0;
            var pt = new POINT { x = rightEdge - 1, y = 0 };
            var hMon = MonitorFromPoint(pt, 2 /* MONITOR_DEFAULTTONEAREST */);
            if (hMon != IntPtr.Zero)
            {
                var mi = new MONITORINFO { cbSize = 40 }; // sizeof(MONITORINFO) = 40
                if (GetMonitorInfo(hMon, ref mi))
                    monitorTop = mi.rcMonitor_top;
            }

            int x = rightEdge - W - Corner;
            int y = monitorTop + Corner;
            return (x, y, W, H);
        }
        catch
        {
            return (100, Corner, W, H);
        }
    }

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT { public int x, y; }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int cbSize;
        public int rcMonitor_left, rcMonitor_top, rcMonitor_right, rcMonitor_bottom;
        public int rcWork_left, rcWork_top, rcWork_right, rcWork_bottom;
        public int dwFlags;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Get the Chrome window bounds for a given target via CDP Browser.getWindowForTarget.
    /// Returns (windowId, left, top, width, height) or null if unavailable.
    /// </summary>
    public async Task<(int windowId, int left, int top, int width, int height)?> GetWindowForTargetAsync(string? targetId = null)
    {
        try
        {
            var param = new JsonObject();
            if (targetId != null) param["targetId"] = targetId;
            var result = await SendAsync("Browser.getWindowForTarget", param, timeoutMs: 3000);
            if (result == null) return null;

            var windowId = result["windowId"]?.GetValue<int>() ?? 0;
            var bounds = result["bounds"];
            if (bounds == null) return null;

            return (windowId,
                bounds["left"]?.GetValue<int>() ?? 0,
                bounds["top"]?.GetValue<int>() ?? 0,
                bounds["width"]?.GetValue<int>() ?? 0,
                bounds["height"]?.GetValue<int>() ?? 0);
        }
        catch { return null; }
    }

    /// <summary>Set Chrome window bounds via CDP Browser.setWindowBounds.</summary>
    public async Task<bool> SetWindowBoundsAsync(int windowId, int left, int top, int width, int height)
    {
        try
        {
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = windowId,
                ["bounds"] = new JsonObject
                {
                    ["left"] = left,
                    ["top"] = top,
                    ["width"] = width,
                    ["height"] = height,
                    ["windowState"] = "normal"
                }
            });
            return true;
        }
        catch { return false; }
    }

    /// <summary>Minimize Chrome window via CDP Browser.setWindowBounds. Focusless — no OS activation.</summary>
    public async Task<bool> MinimizeWindowAsync(string? targetId = null)
    {
        try
        {
            var wb = await GetWindowForTargetAsync(targetId);
            if (wb == null) return false;
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = wb.Value.windowId,
                ["bounds"] = new JsonObject { ["windowState"] = "minimized" }
            });
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Create a new Chrome window with a blank tab via CDP Target.createTarget.
    /// Returns the new target's ID, or null on failure.
    /// </summary>
    public async Task<string?> CreateTargetInNewWindowAsync(string url = "about:blank")
    {
        try
        {
            var result = await SendAsync("Target.createTarget", new JsonObject
            {
                ["url"] = url,
                ["newWindow"] = true
            });
            return result?["targetId"]?.GetValue<string>();
        }
        catch { return null; }
    }
}
