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
    // -- Focus Watchdog: background loop monitors focus throughout a CDP ask operation --
    // Started before input, stopped when ask completes. Retries indefinitely until restored.
    static CancellationTokenSource? _focusWatchdogCts;

    // Grace period: suppress watchdog false-positives for 500ms after RestoreChromeNoActivate.
    // Chrome legitimately raises its window after SW_SHOWNOACTIVATE + prompt inject, which
    // FocusSentinel would otherwise flag as a steal. Caller sets this before the restore.
    static long _focusWatchdogSuppressUntilTick = 0;
    static void SuppressFocusWatchdog(int ms = 500) =>
        Interlocked.Exchange(ref _focusWatchdogSuppressUntilTick,
            DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ms);

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

                    // Suppress during grace window (e.g. right after RestoreChromeNoActivate)
                    if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < Interlocked.Read(ref _focusWatchdogSuppressUntilTick))
                        continue;

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
                        Console.Error.WriteLine($"[FOCUS-WD] Chrome stole focus @ {context} -- restoring...");
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
                        Console.Error.WriteLine($"[FOCUS-WD] restored @ {context}");
                        notifiedOnce = false;
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { Console.Error.WriteLine($"[FOCUS-WD] error: {ex.Message}"); }
        });

        return new ActionDisposable(() => cts.Cancel());
    }

    sealed class ActionDisposable(Action onDispose) : IDisposable
    {
        public void Dispose() => onDispose();
    }

    /// <summary>
    /// Restore focus with retry loop -- keeps trying until GetForegroundWindow()==prevFg or timeout.
    /// Shows focusless notification on first detection. IME + cursor also restored on success.
    /// </summary>
    static async Task<bool> RestoreFocusWithRetryAsync(IntPtr prevFg, string step, CdpClient? cdp,
        int retryIntervalMs = 150, int timeoutMs = 5000)
    {
        if (prevFg == IntPtr.Zero) return false;
        var cur = NativeMethods.GetForegroundWindow();
        if (cur == prevFg) { Console.Error.WriteLine($"[ASK:FOCUS] ok @ {step}"); return false; }

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
                    Console.Error.WriteLine($"[ASK:FOCUS] user-switch @ {step}: now={cur:X8} (not Chrome)");
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
        Console.Error.WriteLine($"[ASK:FOCUS] STOLEN @ {step}: was={prevFg:X8} now={cur:X8} -- retrying...");
        Console.ResetColor();

        // Retry loop until restored or timeout. If the user becomes active
        // mid-retry we bail out quietly -- ripping focus back from an
        // interactive user is worse than giving up, and the 32-retry spam
        // that motivated the merged bug report is almost always this shape
        // (user clicked somewhere else intentionally).
        var sw = Stopwatch.StartNew();
        int attempt = 0;
        bool restored = false;
        bool userBailed = false;
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            attempt++;
            NativeMethods.ForceForegroundWindow(prevFg);
            await Task.Delay(retryIntervalMs);
            if (NativeMethods.GetForegroundWindow() == prevFg) { restored = true; break; }
            if (NativeMethods.GetUserIdleMs() < 1500)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Error.WriteLine($"[ASK:FOCUS] user active @ {step} attempt={attempt} -- yielding");
                Console.ResetColor();
                userBailed = true;
                break;
            }
            if (attempt % 10 == 0)
                Console.Error.WriteLine($"[ASK:FOCUS] still retrying @ {step} attempt={attempt} elapsed={sw.ElapsedMilliseconds}ms");
        }

        if (userBailed)
        {
            // Silent return -- not a bug, user made a choice. IME / cursor
            // stay where the user put them.
            return false;
        }

        // Last-resort: if Chrome keeps re-stealing, minimize it so Windows hands foreground
        // to the next Z-order window (usually prevFg). User-visible but reliable.
        if (!restored && cdp != null)
        {
            try
            {
                var chromeHwnd = cdp.GetChromeWindowHandle();
                var stuckFg = NativeMethods.GetForegroundWindow();
                if (chromeHwnd != IntPtr.Zero && stuckFg == chromeHwnd)
                {
                    Console.Error.WriteLine($"[ASK:FOCUS] last-resort: minimizing Chrome to release foreground");
                    cdp.MinimizeChrome();
                    await Task.Delay(100);
                    NativeMethods.ForceForegroundWindow(prevFg);
                    await Task.Delay(100);
                    restored = NativeMethods.GetForegroundWindow() == prevFg;
                }
            }
            catch (Exception ex) { Console.Error.WriteLine($"[ASK:FOCUS] last-resort error: {ex.Message}"); }
        }

        if (restored)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine($"[ASK:FOCUS] restored @ {step} after {attempt} attempt(s) ({sw.ElapsedMilliseconds}ms)");
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
            Console.Error.WriteLine($"[ASK:FOCUS] RESTORE FAILED @ {step} after {attempt} attempts ({timeoutMs}ms timeout)");
            Console.ResetColor();

            // UIPI check: if prevFg is an elevated process, SetForeground silently fails -- known limitation.
            // Route via admin Eye proxy (if available) before reporting as unrecoverable.
            bool prevFgElevated = false;
            try
            {
                NativeMethods.GetWindowThreadProcessId(prevFg, out uint prevPid);
                prevFgElevated = NativeMethods.IsProcessElevated(prevPid) == true
                                 && !NativeMethods.IsCurrentProcessElevated();
            }
            catch { }

            if (prevFgElevated)
            {
                // Try admin Eye proxy to restore focus to elevated window
                var elevated = ElevationHelper.TryDelegateIfElevated(prevFg, "a11y",
                    new[] { "focus", $"hwnd:0x{prevFg.ToInt64():X}" });
                if (elevated.delegated && elevated.exitCode == 0)
                {
                    Console.Error.WriteLine($"[ASK:FOCUS] restored via admin proxy @ {step}");
                    return true;
                }
                // Known UIPI limitation -- log as warning, not BUG-AUTO
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine($"[ASK:FOCUS] UIPI: prevFg=0x{prevFg:X8} is elevated -- focus restore requires --sudo");
                Console.ResetColor();
            }
            else
            {
                // Unrecoverable: report to Slack for human awareness
                AutoBugReport($"focus-steal unrecoverable @ {step}: prevFg=0x{prevFg:X8} stuck on 0x{NativeMethods.GetForegroundWindow():X8} after {attempt} attempts");
            }
        }

        return restored;
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
        // Remove bot decoration after send completes
        cdp.SetDwmBorderColor(null);
        _ = cdp.SetBotOverlayAsync(false);
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
            Console.Error.WriteLine($"[ASK:FOCUS] ok @ {step} fg={cur:X8}");
            return false;
        }
        // Fire-and-forget retry restore (handles Chrome-PID check, IME, cursor, overlay, OnFocusStealer)
        _ = Task.Run(() => RestoreFocusWithRetryAsync(prevFg, step, cdp));
        return true;
    }
}
