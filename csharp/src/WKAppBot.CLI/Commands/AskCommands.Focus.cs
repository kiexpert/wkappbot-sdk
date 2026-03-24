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
        CdpClient cdp, string action, string? cssSelector = null, string? label = null)
    {
        var prevFg = NativeMethods.GetForegroundWindow();
        ClickZoomHelper? zoom = null;
        try
        {
            // ?? Mid-input check 1: ?ㅻⅨ ?몄뀡??Win32 ?ㅻ낫???낅젰 以묒씠硫??좉퉸 ?묐낫 ??
            if (KeyboardInput.IsInputLockedByOther())
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[ASK:FOCUS] ??Another session is typing ??waiting up to 5s before {action}...");
                Console.ResetColor();
                var waited = 0;
                while (waited < 5000 && KeyboardInput.IsInputLockedByOther())
                {
                    await Task.Delay(200);
                    waited += 200;
                }
                if (KeyboardInput.IsInputLockedByOther())
                    Console.WriteLine($"[ASK:FOCUS] Still locked after wait ??proceeding anyway");
                else
                    Console.WriteLine($"[ASK:FOCUS] Lock released after {waited}ms ??proceeding");
            }

            // ?? Mid-input check 2: ?ъ빱???묐낫 ?앹뾽 ??
            // 議곌굔 A: "send"/"type" 吏곸쟾 + ?좎?媛 3珥??대궡 ?낅젰 以?
            // 議곌굔 B: Chrome??FocusStealer-ask prop??李랁? ?덉쓬 (?댁쟾 run?먯꽌 媛뺥깉 媛먯???
            //          ????梨꾪똿???쒕??섏씠???욎씠???ш퀬 諛⑹?
            // Overlay only when Chrome previously stole focus (FocusStealer-ask prop set).
            // userIsActive check removed — SW_SHOWNOACTIVATE handles background tabs without stealing focus.
            if (action is "send" or "type" or "input-cdp")
            {
                var chromeHwndEarly = cdp.GetChromeWindowHandle();
                bool focusStealerKnown = chromeHwndEarly != IntPtr.Zero
                    && ActionApi.HasFocusStealerProp(chromeHwndEarly, "ask");

                if (focusStealerKnown)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[ASK:FOCUS] FocusStealer-ask prop detected -- yield confirmation required");
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
                        Console.WriteLine($"[ASK:FOCUS] yield denied -- {action} aborted");
                        Console.ResetColor();
                        return (false, prevFg, null);
                    }
                    Console.WriteLine($"[ASK:FOCUS] yield approved -- {action} running");
                }
            }

            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd == IntPtr.Zero)
            {
                Console.WriteLine($"[AAR:CDP] Chrome window not found for {action}");
                return (false, prevFg, null);
            }

            // Blocker/minimize already handled in EnsureCdpConnection.
            // Here: zoom overlay ??show WHICH TAB is being targeted.
            // Background tab = user can't see the editor, so zoom on the tab strip item.

            // Strategy 1: Find the tab via UIA (shows tab title = user knows which AI)
            try
            {
                var pageTitle = await cdp.GetTitleAsync() ?? "";
                if (!string.IsNullOrEmpty(pageTitle))
                {
                    using var automation = new UIA3Automation();
                    var chromeEl = automation.FromHandle(chromeHwnd);
                    // Walk tab strip for matching title (substring match)
                    var tabs = chromeEl.FindAllDescendants(cf =>
                        cf.ByControlType(FlaUI.Core.Definitions.ControlType.TabItem));
                    foreach (var tab in tabs)
                    {
                        var tabName = tab.Name ?? "";
                        if (tabName.Contains(label ?? "", StringComparison.OrdinalIgnoreCase)
                            || tabName.Contains(pageTitle[..Math.Min(20, pageTitle.Length)], StringComparison.OrdinalIgnoreCase))
                        {
                            var tabRect = tab.BoundingRectangle;
                            if (tabRect.Width > 0 && tabRect.Height > 0)
                            {
                                var screenRect = new System.Drawing.Rectangle(
                                    (int)tabRect.X, (int)tabRect.Y, (int)tabRect.Width, (int)tabRect.Height);
                                Console.Write($"[ZOOM:TAB \"{tabName[..Math.Min(20, tabName.Length)]}\"] ");
                                zoom = ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label ?? action);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception tex)
            {
                Console.Write($"[ZOOM:TAB:ERR {tex.GetType().Name}] ");
            }

            // Fallback: zoom on Chrome window itself
            if (zoom == null)
            {
                try
                {
                    NativeMethods.GetWindowRect(chromeHwnd, out var winRect);
                    if (winRect.Width > 0 && winRect.Height > 0)
                    {
                        var screenRect = new System.Drawing.Rectangle(
                            winRect.Left, winRect.Top, winRect.Width, winRect.Height);
                        zoom = ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label ?? action);
                    }
                }
                catch { }
            }

            // Restore Chrome (SW_SHOWNOACTIVATE) so renderer is active for input
            // without stealing OS focus from the user's current window.
            if (action is "input-cdp" or "send" or "type")
                cdp.RestoreChromeNoActivate();

            Console.WriteLine($"[AAR:CDP] Ready: {action}");
            return (true, prevFg, zoom);
        }
        catch (Exception ex)
        {
            LogWarning("AAR:CDP", "Focus rect error", ex);
            return (true, prevFg, zoom);
        }
    }

    /// <summary>
    /// Check if Chrome stole focus and restore if so.
    /// </summary>
    static void GuardCdpFocusTheft(CdpClient cdp, IntPtr prevFg, string action)
    {
        if (prevFg == IntPtr.Zero) return;
        var curFg = NativeMethods.GetForegroundWindow();
        var chromeHwnd = cdp.GetChromeWindowHandle();

        if (curFg == prevFg)
        {
            // No steal — clear the prop so next ask doesn't require approval unnecessarily
            if (chromeHwnd != IntPtr.Zero)
                try { NativeMethods.RemovePropW(chromeHwnd, $"{ActionApi.FocusStealerPropPrefix}ask"); } catch { }
            return;
        }

        if (chromeHwnd == IntPtr.Zero) return;
        NativeMethods.GetWindowThreadProcessId(curFg, out uint curPid);
        NativeMethods.GetWindowThreadProcessId(chromeHwnd, out uint chromePid);

        if (curPid == chromePid)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[AAR:CDP:FOCUS] Chrome stole focus during {action}! Restoring...");
            Console.ResetColor();
            NativeMethods.SetForegroundWindowRaw(prevFg); // restore stolen fg
            // FocusStealer prop NOT stamped — SW_SHOWNOACTIVATE prevents repeat steals
            ActionApi.OnFocusStealer?.Invoke(chromeHwnd, $"ask-{action}");
        }
    }

    /// <summary>
    /// Log focus state and restore to prevFg if changed (any thief, not just Chrome).
    /// Also restores IME conversion state + mouse cursor if stolen.
    /// Fires ActionApi.OnFocusStealer callback ??knowhow recording + overlay.
    /// Returns true if focus was stolen (and restored).
    /// </summary>
    static bool LogRestoreFocus(IntPtr prevFg, string step)
    {
        if (prevFg == IntPtr.Zero) return false;
        var cur = NativeMethods.GetForegroundWindow();
        if (cur == prevFg)
        {
            Console.WriteLine($"[ASK:FOCUS] ok @ {step} fg={cur:X8}");
            return false;
        }

        // ?? Snapshot IME state of prevFg (per-window context, readable even when not foreground)
        uint imeConv = 0, imeSent = 0;
        try
        {
            var himc = NativeMethods.ImmGetContext(prevFg);
            if (himc != IntPtr.Zero)
            {
                NativeMethods.ImmGetConversionStatus(himc, out imeConv, out imeSent);
                NativeMethods.ImmReleaseContext(prevFg, himc);
            }
        }
        catch { }

        // ?? Snapshot cursor position before restore (?낅젰?꾩튂?뺣낫)
        NativeMethods.GetCursorPos(out var cursorSnap);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ASK:FOCUS] ??STOLEN @ {step}: was={prevFg:X8} now={cur:X8} ??restoring");
        Console.ResetColor();

        // ?? Knowhow recording on the thief (Chrome) ??stamps WKAppBot_FocusStealer-ask-{step}
        ActionApi.OnFocusStealer?.Invoke(cur, $"ask-{step}");
        // Also stamp a generic "ask" marker ??EnsureCdpReadyAsync detects it ??forces yield popup next run
        // FocusStealer prop NOT stamped — SW_SHOWNOACTIVATE prevents repeat steals; avoid false-positive overlays

        NativeMethods.SetForegroundWindowRaw(prevFg); // restore stolen fg

        // ?? Alert on MY window (prevFg) ??user sees exactly when/where Gemini stole focus
        try
        {
            NativeMethods.GetWindowThreadProcessId(cur, out uint thiefPid);
            string thiefName = "unknown";
            try { thiefName = System.Diagnostics.Process.GetProcessById((int)thiefPid).ProcessName; } catch { }
            FocuslessWarningOverlay.Show(prevFg, $"@ {step} focus stolen by {thiefName}(0x{cur:X8})", thiefName);
        }
        catch { }

        // ?? Restore IME conversion state (Chrome resets to English; we put Korean back)
        try
        {
            var himc2 = NativeMethods.ImmGetContext(prevFg);
            if (himc2 != IntPtr.Zero)
            {
                NativeMethods.ImmSetConversionStatus(himc2, imeConv, imeSent);
                NativeMethods.ImmReleaseContext(prevFg, himc2);
                Console.WriteLine($"[ASK:FOCUS] IME restored conv=0x{imeConv:X} sent=0x{imeSent:X}");
            }
        }
        catch { }

        // ?? Restore cursor if moved during steal (>4px threshold)
        NativeMethods.GetCursorPos(out var cursorNow);
        int cdx = Math.Abs(cursorNow.X - cursorSnap.X), cdy = Math.Abs(cursorNow.Y - cursorSnap.Y);
        if (cdx > 4 || cdy > 4)
        {
            NativeMethods.SetCursorPos(cursorSnap.X, cursorSnap.Y);
            Console.WriteLine($"[ASK:FOCUS] Cursor restored ({cursorSnap.X},{cursorSnap.Y}) ?({cdx},{cdy})");
        }

        return true;
    }

    // ?? CDP Zoom: show magnifier on CDP target element ??
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
            var rectStr = Task.Run(() => cdp.GetElementRectAsync(cssSelector))
                .WaitAsync(TimeSpan.FromSeconds(3)).GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(rectStr)) return null;

            var parts = rectStr.Split(',');
            if (parts.Length != 4) return null;
            int vx = int.Parse(parts[0]), vy = int.Parse(parts[1]);
            int vw = int.Parse(parts[2]), vh = int.Parse(parts[3]);
            if (vw <= 0 || vh <= 0) return null;

            // Chrome window client area offset ??screen coords
            var pt = new WKAppBot.Win32.Native.POINT(0, 0);
            NativeMethods.ClientToScreen(chromeHwnd, ref pt);

            var screenRect = new System.Drawing.Rectangle(pt.X + vx, pt.Y + vy, vw, vh);
            Console.Write($"[ZOOM:CDP {action}] ");
            return ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label);
        }
        catch (Exception ex)
        {
            Console.Write($"[ZOOM:CDP:ERR {ex.GetType().Name}] ");
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
    /// Strategy: title match ("?닿린"/"Open") first, then structural match (ComboBoxEx32/DUIViewWndClassName child).
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
            if (title is "?닿린" or "Open" or "?뚯씪 ?닿린" or "Open File")
            {
                result = hWnd;
                Console.WriteLine($"[ASK] FindFileOpenDialog: title match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            // Structural match ??file dialog has ComboBoxEx32 (filename field) + DUIViewWndClassName (file list)
            var combo = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "ComboBoxEx32", null);
            var dui = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "DUIViewWndClassName", null);
            if (combo != IntPtr.Zero && dui != IntPtr.Zero)
            {
                result = hWnd;
                Console.WriteLine($"[ASK] FindFileOpenDialog: structural match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            return true; // continue
        }, IntPtr.Zero);

        return result;
    }

    static bool IsTextFile(string ext) => ext is ".txt" or ".log" or ".md" or ".cs" or ".js"
        or ".ts" or ".py" or ".java" or ".json" or ".yaml" or ".yml" or ".xml" or ".html"
        or ".htm" or ".csv" or ".css" or ".sql" or ".sh" or ".bat" or ".cfg" or ".ini";

    // ?? Response Image Detection & Download ??

    /// <summary>
    /// Detect new images in AI response and download them.
    /// Returns list of saved file paths for inline markers.
    /// Tracks already-downloaded images via knownImageUrls set.
    /// </summary>
}
