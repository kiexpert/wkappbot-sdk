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
    /// Trigger one animation tick on the bot overlay (call on each CDP command).
    /// Fire-and-forget: does NOT await to avoid adding latency.
    /// </summary>
    public void TickBotOverlay()
    {
        if (!_overlayInjected) return;
        _ = Task.Run(async () =>
        {
            try { await EvalAsync("window.__wkBotTick?.()"); } catch { }
        });
    }

    /// <summary>
    /// Switch overlay to continuous pulse (streaming=true) or back to tick-on-demand (false).
    /// Call when AI response streaming starts/ends to show live activity animation.
    /// </summary>
    public void SetBotOverlayStreaming(bool streaming)
    {
        if (!_overlayInjected) return;
        _ = Task.Run(async () =>
        {
            try { await EvalAsync($"window.__wkBotStreaming?.({(streaming ? "true" : "false")})"); } catch { }
        });
    }

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

    // -- Window management (CDP Browser domain + Target domain) --

    /// <summary>
    /// Expected WebBot window bounds (right side of virtual screen, near top).
    /// Position: rightmost monitor upper area, 800x600.
    /// Uses GetSystemMetrics (physical pixels) -- same coordinate space as SetWindowPos/CDP.
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
            // Point at top-right corner of virtual screen -> belongs to rightmost monitor
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

    /// <summary>Set Chrome window bounds via CDP Browser.setWindowBounds.
    /// Two-step: restore from minimized first (Chrome ignores bounds while minimized),
    /// then apply position/size. Also syncs Win32 WINDOWPLACEMENT.rcNormalPosition so
    /// taskbar-click restore lands at the correct position (not a stale/off-screen spot).
    /// Caller should re-minimize after if needed.</summary>
    public async Task<bool> SetWindowBoundsAsync(int windowId, int left, int top, int width, int height)
    {
        try
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            // Chrome ignores bounds changes while minimized -- must restore first
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = windowId,
                ["bounds"] = new JsonObject { ["windowState"] = "normal" }
            });
            // Poll until windowState is "normal" using shared helper
            await WaitForWindowStateAsync(windowId, "normal", timeoutMs: 2000);
            var restoreMs = sw.ElapsedMilliseconds;
            // Now set actual bounds (window confirmed normal)
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = windowId,
                ["bounds"] = new JsonObject
                {
                    ["left"] = left,
                    ["top"] = top,
                    ["width"] = width,
                    ["height"] = height,
                }
            });
            Console.Error.WriteLine($"[CDP:BOUNDS] wid={windowId} restore={restoreMs}ms bounds={sw.ElapsedMilliseconds}ms -> ({left},{top} {width}x{height})");

            // Sync Win32 WINDOWPLACEMENT.rcNormalPosition so taskbar-click restore
            // goes to the expected position (CDP bounds and Win32 placement are independent).
            var hwnd = FindChromeMainWindow();
            if (hwnd != IntPtr.Zero)
            {
                var wp = new WINDOWPLACEMENT { length = 44 };
                if (GetWindowPlacement(hwnd, ref wp))
                {
                    wp.rcNormalPosition = new System.Drawing.Rectangle(left, top, left + width, top + height);
                    SetWindowPlacement(hwnd, ref wp);
                }
            }

            return true;
        }
        catch { return false; }
    }

    /// <summary>Minimize Chrome window via CDP Browser.setWindowBounds. Focusless -- no OS activation.</summary>
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
    /// Poll Browser.getWindowBounds until windowState matches expected value.
    /// Replaces fragile fixed delays with state-based synchronization.
    /// </summary>
    public async Task<bool> WaitForWindowStateAsync(int windowId, string expectedState, int timeoutMs = 2000, int pollMs = 50)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            try
            {
                var wb = await SendAsync("Browser.getWindowBounds", new JsonObject { ["windowId"] = windowId }, timeoutMs: 3000);
                var state = wb?["bounds"]?["windowState"]?.GetValue<string>();
                if (state == expectedState) return true;
            }
            catch { break; }
            await Task.Delay(pollMs);
        }
        return false;
    }

    /// <summary>Ensure Chrome window is minimized before tab operations (prevent focus theft).
    /// Polls windowState instead of fixed 100ms delay.</summary>
    public async Task EnsureMinimizedAsync(string? targetId = null)
    {
        try
        {
            var wb = await GetWindowForTargetAsync(targetId);
            if (wb == null) return;
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = wb.Value.windowId,
                ["bounds"] = new JsonObject { ["windowState"] = "minimized" }
            });
            await WaitForWindowStateAsync(wb.Value.windowId, "minimized", timeoutMs: 1000);
        }
        catch { }
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
