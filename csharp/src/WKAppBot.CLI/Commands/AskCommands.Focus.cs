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
    // ── Focus Watchdog: background loop monitors focus throughout a CDP ask operation ──
    // Started before input, stopped when ask completes. Retries indefinitely until restored.
    static CancellationTokenSource? _focusWatchdogCts;

    static IDisposable StartFocusWatchdog(IntPtr prevFg, CdpClient cdp, string context)
    {
        _focusWatchdogCts?.Cancel();
        var cts = new CancellationTokenSource();
        _focusWatchdogCts = cts;
        bool notifiedOnce = false;

        Task.Run(async () =>
        {
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(200, cts.Token);
                    var cur = NativeMethods.GetForegroundWindow();
                    if (cur == prevFg) { notifiedOnce = false; continue; }

                    // Only act if Chrome stole it (not user switching)
                    var chromeHwnd = cdp.GetChromeWindowHandle();
                    if (chromeHwnd == IntPtr.Zero) continue;
                    NativeMethods.GetWindowThreadProcessId(cur, out uint curPid);
                    NativeMethods.GetWindowThreadProcessId(chromeHwnd, out uint chromePid);
                    if (curPid != chromePid) continue; // user switched

                    if (!notifiedOnce)
                    {
                        notifiedOnce = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[FOCUS-WD] Chrome stole focus @ {context} -- restoring...");
                        Console.ResetColor();
                        try
                        {
                            string thiefName = "chrome";
                            try { thiefName = System.Diagnostics.Process.GetProcessById((int)curPid).ProcessName; } catch { }
                            FocuslessWarningOverlay.Show(prevFg, $"focus stolen by {thiefName} @ {context}", thiefName);
                        }
                        catch { }
                    }

                    NativeMethods.ForceForegroundWindow(prevFg);
                    if (NativeMethods.GetForegroundWindow() == prevFg)
                    {
                        Console.WriteLine($"[FOCUS-WD] restored @ {context}");
                        notifiedOnce = false;
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { Console.WriteLine($"[FOCUS-WD] error: {ex.Message}"); }
        });

        return new ActionDisposable(() => cts.Cancel());
    }

    sealed class ActionDisposable(Action onDispose) : IDisposable
    {
        public void Dispose() => onDispose();
    }

    /// <summary>
    /// Restore focus with retry loop — keeps trying until GetForegroundWindow()==prevFg or timeout.
    /// Shows focusless notification on first detection. IME + cursor also restored on success.
    /// </summary>
    static async Task<bool> RestoreFocusWithRetryAsync(IntPtr prevFg, string step, CdpClient? cdp,
        int retryIntervalMs = 150, int timeoutMs = 5000)
    {
        if (prevFg == IntPtr.Zero) return false;
        var cur = NativeMethods.GetForegroundWindow();
        if (cur == prevFg) { Console.WriteLine($"[ASK:FOCUS] ok @ {step}"); return false; }

        // Only act if Chrome stole it
        if (cdp != null)
        {
            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(cur, out uint curPid);
                NativeMethods.GetWindowThreadProcessId(chromeHwnd, out uint chromePid);
                if (curPid != chromePid)
                {
                    Console.WriteLine($"[ASK:FOCUS] user-switch @ {step}: now={cur:X8} (not Chrome)");
                    return false;
                }
            }
        }

        // Snapshot IME + cursor before any restore attempt
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
        NativeMethods.GetCursorPos(out var cursorSnap);

        // Knowhow + focusless notification (once)
        ActionApi.OnFocusStealer?.Invoke(cur, $"ask-{step}");
        try
        {
            NativeMethods.GetWindowThreadProcessId(cur, out uint thiefPid);
            string thiefName = "chrome";
            try { thiefName = System.Diagnostics.Process.GetProcessById((int)thiefPid).ProcessName; } catch { }
            FocuslessWarningOverlay.Show(prevFg, $"STOLEN @ {step} by {thiefName}(0x{cur:X8}) -- restoring", thiefName);
        }
        catch { }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ASK:FOCUS] STOLEN @ {step}: was={prevFg:X8} now={cur:X8} -- retrying...");
        Console.ResetColor();

        // Retry loop until restored or timeout
        var sw = Stopwatch.StartNew();
        int attempt = 0;
        bool restored = false;
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            attempt++;
            NativeMethods.ForceForegroundWindow(prevFg);
            await Task.Delay(retryIntervalMs);
            if (NativeMethods.GetForegroundWindow() == prevFg) { restored = true; break; }
            if (attempt % 10 == 0)
                Console.WriteLine($"[ASK:FOCUS] still retrying @ {step} attempt={attempt} elapsed={sw.ElapsedMilliseconds}ms");
        }

        if (restored)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[ASK:FOCUS] restored @ {step} after {attempt} attempt(s) ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            // Restore IME
            try
            {
                var himc2 = NativeMethods.ImmGetContext(prevFg);
                if (himc2 != IntPtr.Zero)
                {
                    NativeMethods.ImmSetConversionStatus(himc2, imeConv, imeSent);
                    NativeMethods.ImmReleaseContext(prevFg, himc2);
                }
            }
            catch { }
            // Restore cursor
            NativeMethods.GetCursorPos(out var cursorNow);
            if (Math.Abs(cursorNow.X - cursorSnap.X) > 4 || Math.Abs(cursorNow.Y - cursorSnap.Y) > 4)
                NativeMethods.SetCursorPos(cursorSnap.X, cursorSnap.Y);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ASK:FOCUS] RESTORE FAILED @ {step} after {attempt} attempts ({timeoutMs}ms timeout)");
            Console.ResetColor();
            // Unrecoverable: mechanical restore exhausted — report to Slack for human awareness
            AutoBugReport($"focus-steal unrecoverable @ {step}: prevFg=0x{prevFg:X8} stuck on 0x{NativeMethods.GetForegroundWindow():X8} after {attempt} attempts");
        }

        return restored;
    }

    static async Task<(bool ready, IntPtr prevFg, ClickZoomHelper? zoom)> EnsureCdpReadyAsync(
        CdpClient cdp, string action, string? cssSelector = null, string? label = null, IntPtr prevFgHint = default)
    {
        // Use caller-supplied prevFg if available (captures focus before any CDP tab-switch ops).
        // Falling back to GetForegroundWindow() here would miss focus stolen during tab switch.
        var prevFg = prevFgHint != IntPtr.Zero ? prevFgHint : NativeMethods.GetForegroundWindow();
        ClickZoomHelper? zoom = null;
        try
        {
            // ── Mid-input check 1: if another Win32 session is typing, wait before proceeding ──
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

            // ── Mid-input check 2: overlay check for focus steals ──
            // Condition A: before "send"/"type" + user typed within last 3s
            // Condition B: Chrome FocusStealer-ask prop detected (focus was stolen in prior run
            //          i.e. screen takeover -- user must yield before proceeding)
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

            // ── InputReadiness.Probe(): fires OnProbeSuccess (auto-hack/zoom), sets ReadinessCalled flag ──
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
                cdp.RestoreChromeNoActivate();
                // TODO-13: Emulate tab focused at CDP level (Emulation.setFocusEmulationEnabled).
                // Chrome accepts input events as if focused without OS foreground --SW(8) dance becomes optional.
                await cdp.EmulateActiveTabAsync();
                // TODO-11: Inject JS focus-lock monkey-patch (once per tab) and lock before input.
                // Prevents JS .focus() / window.focus() from stealing OS foreground during CDP send.
                await cdp.InjectFocusLockScriptAsync();
                await cdp.SetFocusLockAsync(true);
            }

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
            if (chromeHwnd != IntPtr.Zero)
                try { NativeMethods.RemovePropW(chromeHwnd, $"{ActionApi.FocusStealerPropPrefix}ask"); } catch { }
            return;
        }

        if (chromeHwnd == IntPtr.Zero) return;
        NativeMethods.GetWindowThreadProcessId(curFg, out uint curPid);
        NativeMethods.GetWindowThreadProcessId(chromeHwnd, out uint chromePid);

        if (curPid == chromePid)
        {
            // Kick off async retry restore (fire-and-forget from sync context)
            Task.Run(() => RestoreFocusWithRetryAsync(prevFg, $"guard-{action}", cdp));
        }
        // Unlock JS focus() so page resumes normal keyboard navigation after send
        _ = cdp.SetFocusLockAsync(false);
    }

    /// <summary>
    /// Sync wrapper: fire-and-forget RestoreFocusWithRetryAsync.
    /// Returns true if theft was detected (Chrome stole focus), false if focus was OK or user-switched.
    /// IME/cursor restore, overlay, and OnFocusStealer callback all handled inside RestoreFocusWithRetryAsync.
    /// </summary>
    static bool LogRestoreFocus(IntPtr prevFg, string step, CdpClient? cdp = null)
    {
        if (prevFg == IntPtr.Zero) return false;
        var cur = NativeMethods.GetForegroundWindow();
        if (cur == prevFg)
        {
            Console.WriteLine($"[ASK:FOCUS] ok @ {step} fg={cur:X8}");
            return false;
        }
        // Fire-and-forget retry restore (handles Chrome-PID check, IME, cursor, overlay, OnFocusStealer)
        _ = Task.Run(() => RestoreFocusWithRetryAsync(prevFg, step, cdp));
        return true;
    }

    // ── CDP Zoom: show magnifier on CDP target element ──
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
            catch { } // Fresh tab or stale context — fall through to client area fallback

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
                // Fallback: Chrome tab client area (JS eval failed — fresh tab or stale context)
                NativeMethods.GetClientRect(chromeHwnd, out var cr);
                var pt0 = new WKAppBot.Win32.Native.POINT(0, 0);
                NativeMethods.ClientToScreen(chromeHwnd, ref pt0);
                screenRect = new System.Drawing.Rectangle(pt0.X, pt0.Y, cr.Right, cr.Bottom);
            }

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

    // ── Response Image Detection & Download ──

    /// <summary>
    /// Detect new images in AI response and download them.
    /// Returns list of saved file paths for inline markers.
    /// Tracks already-downloaded images via knownImageUrls set.
    /// </summary>
}
