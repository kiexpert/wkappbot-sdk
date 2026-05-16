using System.Text;
using System.Text.Json;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Verifies Chrome actually landed at <paramref name="targetRect"/>; if not,
    /// re-issues SetWindowPos and re-checks, up to <paramref name="maxAttempts"/>
    /// times. Returns (success, finalRect, attemptCount).
    ///
    /// Delta thresholds: horizontal/vertical &lt; 20px, width/height &lt; 30px.
    /// Bails early if the window becomes invalid/invisible/cloaked during
    /// correction, or if GetWindowRect takes &gt;200ms. Recomputes the target if
    /// the underlying monitor work area changes mid-loop (multi-monitor reflow).
    ///
    /// Each attempt is appended to cdp-state.jsonl as a placement_validation record
    /// so operators can audit Chrome landing accuracy after the fact.
    /// </summary>
    internal static (bool success, RECT finalRect, int attemptCount) TryValidateAndCorrectPlacement(
        IntPtr chromeHwnd, RECT targetRect, int maxAttempts = 5)
    {
        Console.Error.WriteLine($"[PLACEMENT:VALIDATE-ENTRY] chrome=0x{chromeHwnd.ToInt64():X} target=(L={targetRect.Left},T={targetRect.Top},R={targetRect.Right},B={targetRect.Bottom}) maxAttempts={maxAttempts}");
        const int DeltaPosThreshold  = 15; // x/y px — allow up to ±15px for frame/border variance
        const int DeltaSizeThreshold = 10; // w/h px — strict enough to verify nominal 800x600
        const int StabilizationMs    = 150; // longer settle time for DWM composition
        const int GetRectTimeoutMs   = 200;
        const uint SWP_NOZORDER   = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;

        RECT finalRect = default;
        int attempt = 0;

        // Snapshot the monitor work area at entry so we can detect mid-loop
        // multi-monitor reflows (taskbar repositioning, resolution change, etc.)
        // and recompute the target if the work area shifts under us.
        RECT? initialWorkArea = null;
        try
        {
            int centerX = targetRect.Left + Math.Max(1, targetRect.Width) / 2;
            int centerY = targetRect.Top + Math.Max(1, targetRect.Height) / 2;
            if (TryGetWorkArea(centerX, centerY, out var wa)) initialWorkArea = wa;
        }
        catch { /* ignore -- fall back to no reflow detection */ }

        for (attempt = 1; attempt <= maxAttempts; attempt++)
        {
            // Step 1: wait for the window to settle. SetWindowPos returns before
            // DWM has finished compositing the new bounds, and Chrome's own
            // browser_process_init may still be adjusting the rect on first launch.
            System.Threading.Thread.Sleep(StabilizationMs);

            // Step 2: bail if the window has gone away or become unfit for
            // correction. Continuing to SetWindowPos a cloaked/destroyed window
            // would silently fail and just burn attempts.
            if (!IsWindow(chromeHwnd))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} chrome hwnd no longer valid -- abort");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "hwnd_invalid");
                return (false, finalRect, attempt);
            }
            if (!IsWindowVisible(chromeHwnd) || IsWindowCloaked(chromeHwnd))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} chrome invisible/cloaked -- abort");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "invisible_or_cloaked");
                return (false, finalRect, attempt);
            }

            // Step 3: read actual rect with a soft timeout. GetWindowRect is a
            // synchronous send-message to the window's thread, so a hung Chrome
            // could in principle stall us. The 200ms cap keeps us responsive.
            bool gotRect = false;
            RECT actual = default;
            var rectTask = System.Threading.Tasks.Task.Run(() =>
            {
                gotRect = TryGetWindowRectLTRB(chromeHwnd, out actual);
            });
            if (!rectTask.Wait(GetRectTimeoutMs))
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} GetWindowRect timeout (>{GetRectTimeoutMs}ms) -- skip iteration");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "get_rect_timeout");
                continue;
            }
            if (!gotRect)
            {
                Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} GetWindowRect failed -- skip iteration");
                AppendPlacementValidation(attempt, targetRect, default, success: false, note: "get_rect_failed");
                continue;
            }

            finalRect = actual;

            // Step 4: compute deltas. Use absolute values so a window that
            // overshot vs. undershot both count.
            int dX = Math.Abs(actual.Left   - targetRect.Left);
            int dY = Math.Abs(actual.Top    - targetRect.Top);
            int dW = Math.Abs(actual.Width  - targetRect.Width);
            int dH = Math.Abs(actual.Height - targetRect.Height);
            bool withinThreshold = dX < DeltaPosThreshold
                                 && dY < DeltaPosThreshold
                                 && dW < DeltaSizeThreshold
                                 && dH < DeltaSizeThreshold;

            Console.Error.WriteLine(
                $"[PLACEMENT:VALIDATE] attempt={attempt}/{maxAttempts} "
                + $"expected=(L={targetRect.Left},T={targetRect.Top},R={targetRect.Right},B={targetRect.Bottom},W={targetRect.Width},H={targetRect.Height}) "
                + $"actual=(L={actual.Left},T={actual.Top},R={actual.Right},B={actual.Bottom},W={actual.Width},H={actual.Height}) "
                + $"delta=(dX={dX},dY={dY},dW={dW},dH={dH}) within={withinThreshold}");

            AppendPlacementValidation(attempt, targetRect, actual, success: withinThreshold, note: null);

            if (withinThreshold)
                return (true, finalRect, attempt);

            // Step 5: detect mid-loop monitor reflow. If the work area has
            // shifted (e.g. user dragged the window to another monitor between
            // attempts, or the taskbar moved), recompute the target so we
            // converge on the new work area instead of fighting it.
            if (initialWorkArea.HasValue)
            {
                int centerX = targetRect.Left + Math.Max(1, targetRect.Width) / 2;
                int centerY = targetRect.Top + Math.Max(1, targetRect.Height) / 2;
                if (TryGetWorkArea(centerX, centerY, out var currentWa))
                {
                    var prev = initialWorkArea.Value;
                    if (currentWa.Left != prev.Left || currentWa.Top != prev.Top
                        || currentWa.Right != prev.Right || currentWa.Bottom != prev.Bottom)
                    {
                        Console.Error.WriteLine($"[PLACEMENT:VALIDATE] attempt={attempt} monitor work area changed -- recomputing target");
                        // Clamp to the new work area so we don't push Chrome
                        // off-screen on the corrected attempt.
                        int newL = targetRect.Left;
                        int newT = targetRect.Top;
                        int newW = targetRect.Width;
                        int newH = targetRect.Height;
                        if (newW > currentWa.Right - currentWa.Left) newW = currentWa.Right - currentWa.Left;
                        if (newH > currentWa.Bottom - currentWa.Top) newH = currentWa.Bottom - currentWa.Top;
                        if (newL < currentWa.Left)              newL = currentWa.Left;
                        if (newT < currentWa.Top)               newT = currentWa.Top;
                        if (newL + newW > currentWa.Right)      newL = currentWa.Right - newW;
                        if (newT + newH > currentWa.Bottom)     newT = currentWa.Bottom - newH;
                        targetRect = new RECT { Left = newL, Top = newT, Right = newL + newW, Bottom = newT + newH };
                        initialWorkArea = currentWa;
                    }
                }
            }

            // Step 6: re-issue SetWindowPos. Don't bother on the final attempt --
            // there's no follow-up validation that would catch a late correction.
            if (attempt < maxAttempts)
            {
                SetWindowPos(chromeHwnd, IntPtr.Zero,
                    targetRect.Left, targetRect.Top, targetRect.Width, targetRect.Height,
                    SWP_NOZORDER | SWP_NOACTIVATE);
                // Trace every SetWindowPos to diagnose Chrome drift.
                // coord_system=win32_physical because SetWindowPos accepts PHYSICAL
                // pixels on a PerMonitorV2-aware process. logical_x/logical_y let
                // operators compare against any CDP cdp_logical entry instantly.
                {
                    var swpLog = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? ".",
                        "wkappbot.hq", "logs", "setwindowpos-trace.jsonl");
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(swpLog)!);
                    var _swpDpi = TryGetWindowDpiSafe(chromeHwnd);
                    var _swpScale = _swpDpi > 0 ? _swpDpi / 96.0 : 1.0;
                    var _swpScaleStr = _swpScale.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                    WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(swpLog,
                        $"{{\"ts\":\"{DateTimeOffset.UtcNow:O}\",\"action\":\"SetWindowPos\",\"coord_system\":\"win32_physical\",\"hwnd\":\"0x{chromeHwnd.ToInt64():X}\",\"x\":{targetRect.Left},\"y\":{targetRect.Top},\"w\":{targetRect.Width},\"h\":{targetRect.Height},\"dpi_scale\":{_swpScaleStr},\"logical_x\":{(int)(targetRect.Left/_swpScale)},\"logical_y\":{(int)(targetRect.Top/_swpScale)},\"attempt\":{attempt}}}");
                }
            }
        }

        return (false, finalRect, attempt - 1);
    }

    /// <summary>
    /// Appends a placement_validation record to cdp-state.jsonl. Mirrors the
    /// shape used elsewhere in this file so suggest triage / cdp-mon can pick
    /// it up alongside the existing caller-validation telemetry.
    /// </summary>
    static void AppendPlacementValidation(int attempt, RECT expected, RECT actual, bool success, string? note)
    {
        try
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? ".";
            var runtimeDir = Path.Combine(exeDir, "wkappbot.hq", "runtime");
            Directory.CreateDirectory(runtimeDir);
            var jsonlPath = Path.Combine(runtimeDir, "cdp-state.jsonl");

            using var ms = new MemoryStream();
            using (var writer = new Utf8JsonWriter(ms))
            {
                writer.WriteStartObject();
                writer.WriteString("ts", DateTimeOffset.UtcNow);
                writer.WriteString("kind", "placement_validation");
                writer.WriteNumber("attempt", attempt);
                writer.WriteBoolean("success", success);
                if (!string.IsNullOrEmpty(note)) writer.WriteString("note", note);

                writer.WritePropertyName("expected");
                writer.WriteStartArray();
                writer.WriteNumberValue(expected.Left);
                writer.WriteNumberValue(expected.Top);
                writer.WriteNumberValue(expected.Right);
                writer.WriteNumberValue(expected.Bottom);
                writer.WriteNumberValue(expected.Width);
                writer.WriteNumberValue(expected.Height);
                writer.WriteEndArray();

                writer.WritePropertyName("actual");
                writer.WriteStartArray();
                writer.WriteNumberValue(actual.Left);
                writer.WriteNumberValue(actual.Top);
                writer.WriteNumberValue(actual.Right);
                writer.WriteNumberValue(actual.Bottom);
                writer.WriteNumberValue(actual.Width);
                writer.WriteNumberValue(actual.Height);
                writer.WriteEndArray();

                writer.WritePropertyName("delta");
                writer.WriteStartArray();
                writer.WriteNumberValue(actual.Left   - expected.Left);
                writer.WriteNumberValue(actual.Top    - expected.Top);
                writer.WriteNumberValue(actual.Width  - expected.Width);
                writer.WriteNumberValue(actual.Height - expected.Height);
                writer.WriteEndArray();

                writer.WriteEndObject();
            }
            var line = Encoding.UTF8.GetString(ms.ToArray());
            WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(jsonlPath, line);
        }
        catch
        {
            // best-effort telemetry only -- never throw from the placement loop
        }
    }
}
