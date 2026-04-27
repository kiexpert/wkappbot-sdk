using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    static async Task<(bool ready, IntPtr prevFg, ClickZoomHelper? zoom)> EnsureCdpReadyAsync(
        CdpClient cdp, string action, string? cssSelector = null, string? label = null, IntPtr prevFgHint = default)
    {
        // Use caller-supplied prevFg if available (captures focus before any CDP tab-switch ops).
        // Falling back to GetForegroundWindow() here would miss focus stolen during tab switch.
        var prevFg = prevFgHint != IntPtr.Zero ? prevFgHint : NativeMethods.GetForegroundWindow();
        ClickZoomHelper? zoom = null;
        try
        {
            // -- Mid-input check 1: if another Win32 session is typing, wait before proceeding --
            if (KeyboardInput.IsInputLockedByOther())
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Error.WriteLine($"[ASK:FOCUS] ??Another session is typing ??waiting up to 5s before {action}...");
                Console.ResetColor();
                var waited = 0;
                while (waited < 5000 && KeyboardInput.IsInputLockedByOther())
                {
                    await Task.Delay(200);
                    waited += 200;
                }
                if (KeyboardInput.IsInputLockedByOther())
                    Console.Error.WriteLine($"[ASK:FOCUS] Still locked after wait ??proceeding anyway");
                else
                    Console.Error.WriteLine($"[ASK:FOCUS] Lock released after {waited}ms ??proceeding");
            }

            // -- Mid-input check 2: overlay check for focus steals --
            // Condition A: before "send"/"type" + user typed within last 3s
            // Condition B: Chrome FocusStealer-ask prop detected (focus was stolen in prior run
            //          i.e. screen takeover -- user must yield before proceeding)
            // Overlay only when Chrome previously stole focus (FocusStealer-ask prop set).
            // userIsActive check removed -- SW_SHOWNOACTIVATE handles background tabs without stealing focus.
            if (action is "send" or "type" or "input-cdp")
            {
                var chromeHwndEarly = cdp.GetChromeWindowHandle();
                bool focusStealerKnown = chromeHwndEarly != IntPtr.Zero
                    && ActionApi.HasFocusStealerProp(chromeHwndEarly, "ask");

                if (focusStealerKnown)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Error.WriteLine($"[ASK:FOCUS] FocusStealer-ask prop detected -- yield confirmation required");
                    Console.ResetColor();
                    var lii = new NativeMethods.LASTINPUTINFO { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.LASTINPUTINFO>() };
                    NativeMethods.GetLastInputInfo(ref lii);
                    var idleMs = unchecked((uint)Environment.TickCount) - lii.dwTime;
                    var yieldResult = new UserInputWaitAdapter(noSound: false).WaitForUserYield(
                        chromeHwndEarly, userIdleMs: idleMs, timeoutSeconds: 30,
                        positionHwnd: chromeHwndEarly);
                    if (!yieldResult.Approved)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine($"[ASK:FOCUS] yield denied -- {action} aborted");
                        Console.ResetColor();
                        return (false, prevFg, null);
                    }
                    Console.Error.WriteLine($"[ASK:FOCUS] yield approved -- {action} running");
                }
            }

            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd == IntPtr.Zero)
            {
                Console.Error.WriteLine($"[AAR:CDP] Chrome window not found for {action}");
                return (false, prevFg, null);
            }

            // -- InputReadiness.Probe(): fires OnProbeSuccess (auto-hack/zoom), sets ReadinessCalled flag --
            // SkipBlockerCheck/SkipKnowhow/SkipZoom: Chrome is not a Win32 form; CDP zoom handled below.
            // AutoApproveYield: CDP actions are AI-initiated, no user yield required.
            // WaitForUserIfBusy=false: same reason. FocusStealer-ask prop check (above) handles the one
            // case where we DO need user confirmation.
            try
            {
                var readiness = CreateInputReadiness();
                readiness.Probe(new WKAppBot.Win32.Input.InputReadinessRequest
                {
                    TargetHwnd = chromeHwnd,
                    IntendedAction = action,
                    SkipBlockerCheck = true,
                    SkipKnowhow = true,
                    SkipZoom = true,
                    AutoApproveYield = true,
                    WaitForUserIfBusy = false,
                });
            }
            catch { } // best-effort: Probe failure must not block CDP operation

            // Magnifier on exact CSS element position (or Chrome window as fallback)
            zoom = !string.IsNullOrEmpty(cssSelector)
                ? BeginCdpZoom(cdp, cssSelector, action, label ?? action)
                : null;
            if (zoom == null)
            {
                // Fallback: zoom on Chrome window
                try
                {
                    NativeMethods.GetWindowRect(chromeHwnd, out var winRect);
                    if (winRect.Width > 0 && winRect.Height > 0)
                        zoom = ClickZoomHelper.BeginFromRect(
                            new System.Drawing.Rectangle(winRect.Left, winRect.Top, winRect.Width, winRect.Height),
                            chromeHwnd, $"cdp-{action}", label ?? action);
                }
                catch { }
            }

            // Restore Chrome (SW_SHOWNOACTIVATE) so renderer is active for input
            // without stealing OS focus from the user's current window.
            if (action is "input-cdp" or "send" or "type")
            {
                // Suppress focus watchdog for 500ms: Chrome legitimately raises its window
                // after SW_SHOWNOACTIVATE + prompt inject (renderer becomes active internally).
                // Without suppression this fires a false-positive "focus stolen" alert.
                SuppressFocusWatchdog(500);
                cdp.RestoreChromeNoActivate();
                await cdp.EmulateActiveTabAsync();
                await cdp.InjectFocusLockScriptAsync();
                await cdp.SetFocusLockAsync(true);
                // Visual: DWM border + CSS overlay, color-coded per AI host
                var (dwmColor, hexColor) = GetBotColors(label);
                cdp.SetDwmBorderColor(dwmColor);
                await cdp.SetBotOverlayAsync(true, hexColor);
            }

            Console.Error.WriteLine($"[AAR:CDP] Ready: {action}");
            return (true, prevFg, zoom);
        }
        catch (Exception ex)
        {
            LogWarning("AAR:CDP", "Focus rect error", ex);
            return (true, prevFg, zoom);
        }
    }

    // -- CDP Zoom: show magnifier on CDP target element --
    // Gets element's bounding rect (viewport coords) + Chrome window offset ??screen coords ??ClickZoomHelper.

    /// <summary>
    /// Begin zoom overlay on a CDP element identified by CSS selector.
    /// Returns null on failure (non-critical ??zoom is informational only).
    /// </summary>
    static ClickZoomHelper? BeginCdpZoom(CdpClient cdp, string cssSelector, string action, string label)
    {
        try
        {
            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd == IntPtr.Zero) return null;

            // Get element bounding rect in viewport coords
            // Task.Run avoids sync-over-async deadlock when caller is already on async context
            string? rectStr = null;
            try
            {
                rectStr = Task.Run(() => cdp.GetElementRectAsync(cssSelector))
                    .WaitAsync(TimeSpan.FromSeconds(3)).GetAwaiter().GetResult();
            }
            catch { } // Fresh tab or stale context -- fall through to client area fallback

            System.Drawing.Rectangle screenRect;
            if (!string.IsNullOrEmpty(rectStr)
                && rectStr.Split(',') is { Length: 4 } parts
                && int.TryParse(parts[0], out int vx) && int.TryParse(parts[1], out int vy)
                && int.TryParse(parts[2], out int vw) && int.TryParse(parts[3], out int vh)
                && vw > 0 && vh > 0)
            {
                // Exact element rect
                var pt0 = new WKAppBot.Win32.Native.POINT(0, 0);
                NativeMethods.ClientToScreen(chromeHwnd, ref pt0);
                screenRect = new System.Drawing.Rectangle(pt0.X + vx, pt0.Y + vy, vw, vh);
            }
            else
            {
                // Fallback: Chrome tab client area (JS eval failed -- fresh tab or stale context)
                NativeMethods.GetClientRect(chromeHwnd, out var cr);
                var pt0 = new WKAppBot.Win32.Native.POINT(0, 0);
                NativeMethods.ClientToScreen(chromeHwnd, ref pt0);
                screenRect = new System.Drawing.Rectangle(pt0.X, pt0.Y, cr.Right, cr.Bottom);
            }

            Console.Error.Write($"[ZOOM:CDP {action}] ");
            return ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label);
        }
        catch (Exception ex)
        {
            Console.Error.Write($"[ZOOM:CDP:ERR {ex.GetType().Name}] ");
            return null;
        }
    }

    /// <summary>
    /// Send a trusted mouse click via CDP Input.dispatchMouseEvent.
    /// This creates a real user gesture that Chrome trusts for file dialog opening.
    /// </summary>
    static async Task CdpTrustedClick(CdpClient cdp, int x, int y)
    {
        await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "mousePressed", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
        await Task.Delay(50);
        await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "mouseReleased", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
    }

    /// <summary>
    /// Enumerate all top-level #32770 dialogs and find one that looks like a file-open dialog.
    /// Strategy: title match ("열기"/"Open") first, then structural match (ComboBoxEx32/DUIViewWndClassName child).
    /// </summary>
    static IntPtr FindFileOpenDialog()
    {
        IntPtr result = IntPtr.Zero;
        var classBuf = new StringBuilder(256);
        var titleBuf = new StringBuilder(256);

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            classBuf.Clear();
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            if (classBuf.ToString() != "#32770") return true;

            titleBuf.Clear();
            NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
            var title = titleBuf.ToString();

            // Title match ??most reliable
            if (title is "열기" or "Open" or "파일 열기" or "Open File")
            {
                result = hWnd;
                Console.Error.WriteLine($"[ASK] FindFileOpenDialog: title match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            // Structural match ??file dialog has ComboBoxEx32 (filename field) + DUIViewWndClassName (file list)
            var combo = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "ComboBoxEx32", null);
            var dui = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "DUIViewWndClassName", null);
            if (combo != IntPtr.Zero && dui != IntPtr.Zero)
            {
                result = hWnd;
                Console.Error.WriteLine($"[ASK] FindFileOpenDialog: structural match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            return true; // continue
        }, IntPtr.Zero);

        return result;
    }

    /// <summary>
    /// Per-AI color coding for DWM border + CSS overlay.
    /// Claude=orange, GPT=green, Gemini=blue, default=orange.
    /// </summary>
    static ((byte R, byte G, byte B) dwm, string hex) GetBotColors(string? label) =>
        label switch
        {
            "ChatGPT" or "GPT" => ((16, 163, 127), "#10A37F"),
            "Gemini"           => ((66, 133, 244), "#4285F4"),
            _                  => ((255, 120, 30),  "#ff781e"), // Claude / default
        };

    static bool IsTextFile(string ext) => ext is ".txt" or ".log" or ".md" or ".cs" or ".js"
        or ".ts" or ".py" or ".java" or ".json" or ".yaml" or ".yml" or ".xml" or ".html"
        or ".htm" or ".csv" or ".css" or ".sql" or ".sh" or ".bat" or ".cfg" or ".ini";

    // -- Response Image Detection & Download --

    /// <summary>
    /// Detect new images in AI response and download them.
    /// Returns list of saved file paths for inline markers.
    /// Tracks already-downloaded images via knownImageUrls set.
    /// </summary>
}
