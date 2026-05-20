// Stage 3 (DPI-aware match) placement correction for post-launch Chrome windows.
//
// Split out of MyCdpContext.Stage23.cs (2026-05-16) to keep individual partial
// class files under the ~400-line soft cap. Holds the DPI re-evaluation logic
// and the GetDpiForWindow P/Invoke wrapper used by both the watcher entry
// point in Stage23.cs and Stage 3 itself.

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Stage 3: re-check Chrome's DPI context post-load and correct any rect
    /// mismatch caused by an intervening WM_DPICHANGED. If the caller-DPI
    /// captured at watcher entry differs from Chrome's current DPI by more
    /// than the threshold (e.g. caller=96, Chrome=144), the target rect is
    /// recomputed in the new DPI context and SetWindowPos re-issued.
    ///
    /// Logical-to-physical / physical-to-logical: since the launcher process
    /// is PerMonitorV2-aware, both rects are already in physical pixels for
    /// their respective monitors. The "rescale" therefore means scaling the
    /// stage1 width/height by (currentDpi / callerDpi) so the visible size
    /// stays consistent across the migration.
    /// </summary>
    internal static void TryStage3DpiAwareMatch(IntPtr chromeHwnd, RECT stage1Target, uint callerDpi, string cmd, IntPtr callerHwnd = default)
    {
        const int DeltaThreshold = 8;             // px -- minimum drift to bother correcting
        const uint MinDpi = 72, MaxDpi = 480;     // sanity bounds, anything outside means a bad read
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;

        try
        {
            if (!IsWindow(chromeHwnd))
            {
                AppendStage3Record(0, 0, false, false, false, stage1Target, default, "hwnd_invalid", cmd);
                Console.Error.WriteLine("[PLACEMENT:STAGE3] hwnd no longer valid -- skip");
                return;
            }

            uint currentDpi = TryGetWindowDpiSafe(chromeHwnd);
            if (currentDpi < MinDpi || currentDpi > MaxDpi) currentDpi = 96;
            if (callerDpi  < MinDpi || callerDpi  > MaxDpi) callerDpi  = 96;

            double currentScale = currentDpi / 96.0;
            double callerScale  = callerDpi  / 96.0;
            bool dpiMatch = currentDpi == callerDpi;

            // Read Chrome's actual rect now (after Stage 2 stabilized things).
            RECT actual = default;
            if (!TryGetWindowRectLTRB(chromeHwnd, out actual))
            {
                AppendStage3Record(callerDpi, currentDpi, dpiMatch, false, false, stage1Target, default, "rect_unavailable", cmd);
                Console.Error.WriteLine("[PLACEMENT:STAGE3] could not read rect -- skip");
                return;
            }

            // If DPIs match and the rect is close to target, nothing to do.
            int dX = Math.Abs(actual.Left - stage1Target.Left);
            int dY = Math.Abs(actual.Top  - stage1Target.Top);
            int dW = Math.Abs(actual.Width  - stage1Target.Width);
            int dH = Math.Abs(actual.Height - stage1Target.Height);

            if (dpiMatch && dX < DeltaThreshold && dY < DeltaThreshold && dW < DeltaThreshold && dH < DeltaThreshold)
            {
                AppendStage3Record(callerDpi, currentDpi, true, false, true, stage1Target, actual, "match_confirmed", cmd);
                Console.Error.WriteLine(
                    $"[PLACEMENT:STAGE3] DPI match confirmed (caller={callerDpi}/{callerScale * 100:F0}% chrome={currentDpi}/{currentScale * 100:F0}%), no correction needed");
                return;
            }

            // Recompute target in the current DPI context. The visible size
            // we want is whatever Stage 1 targeted at the caller's DPI; if
            // Chrome ended up on a higher-DPI monitor, scale up accordingly
            // so the user sees the same physical size on the new display.
            int newW = stage1Target.Width;
            int newH = stage1Target.Height;
            int newL = stage1Target.Left;
            int newT = stage1Target.Top;

            if (!dpiMatch && callerDpi > 0)
            {
                double r = (double)currentDpi / (double)callerDpi;
                newW = (int)Math.Round(stage1Target.Width  * r);
                newH = (int)Math.Round(stage1Target.Height * r);
            }

            // Clamp the recomputed rect to the work area of the monitor the
            // caller actually wants Chrome on (i.e. stage1Target's monitor),
            // NOT Chrome's current (drifted) monitor. Bug history: Chrome's
            // session-restore can snap the window back to its remembered
            // monitor between Stage 1 and Stage 3. If we anchored the work
            // area on Chrome's drifted center we would clamp newL/newT to
            // the DRIFTED monitor's edge -- effectively yanking Chrome onto
            // the wrong display permanently and overriding the Launcher's
            // stage 1 placement (e.g. caller at -1732,-1093 left monitor
            // becomes 1740,20 right monitor). Use stage1Target as the
            // authoritative anchor; fall back to actual only if stage1's
            // monitor cannot be resolved (extreme edge case).
            int cX = stage1Target.Left + Math.Max(1, stage1Target.Width) / 2;
            int cY = stage1Target.Top  + Math.Max(1, stage1Target.Height) / 2;
            if (!TryGetWorkArea(cX, cY, out var workArea))
            {
                cX = actual.Left + Math.Max(1, actual.Width) / 2;
                cY = actual.Top  + Math.Max(1, actual.Height) / 2;
                Console.Error.WriteLine(
                    $"[PLACEMENT:STAGE3] stage1Target center ({stage1Target.Left + stage1Target.Width / 2},{stage1Target.Top + stage1Target.Height / 2}) off-monitor -- falling back to actual center for work-area probe");
                TryGetWorkArea(cX, cY, out workArea);
            }
            // workArea may be default(RECT) if both probes failed; in that
            // case skip the clamp entirely (rather than clamp to (0,0)) so
            // stage1Target's intended position survives untouched. Default
            // RECT has Right=Bottom=0 so the `newW > workArea.Right - Left`
            // check would otherwise corrupt the recomputed width.
            if (workArea.Right > workArea.Left && workArea.Bottom > workArea.Top)
            {
                if (newW > workArea.Right - workArea.Left) newW = workArea.Right - workArea.Left;
                if (newH > workArea.Bottom - workArea.Top) newH = workArea.Bottom - workArea.Top;
                if (newL < workArea.Left)              newL = workArea.Left;
                if (newT < workArea.Top)               newT = workArea.Top;
                if (newL + newW > workArea.Right)      newL = workArea.Right - newW;
                if (newT + newH > workArea.Bottom)     newT = workArea.Bottom - newH;
            }

            var corrected = new RECT { Left = newL, Top = newT, Right = newL + newW, Bottom = newT + newH };
            int correctedDx = corrected.Left - actual.Left;
            int correctedDy = corrected.Top  - actual.Top;

            // Noop guard: if the recomputed rect equals stage1Target AND the
            // actual rect is already within tolerance of stage1Target, skip
            // the redundant SetWindowPos. Prevents visible re-snap ("dancing")
            // and confusing duplicate trace records (stage1_initial /
            // validate_retry / stage3_dpi_correct all logging same position).
            // Trigger condition: callerDpi != chromeDpi made dpiMatch false,
            // but the rescale math produced no actual delta (e.g. rounding
            // returned identical W/H), so the "correction" would just re-fire
            // Stage 1's coordinates.
            bool correctionIsNoop = newL == stage1Target.Left
                && newT == stage1Target.Top
                && newW == stage1Target.Width
                && newH == stage1Target.Height;
            if (correctionIsNoop && dX < DeltaThreshold && dY < DeltaThreshold
                && dW < DeltaThreshold && dH < DeltaThreshold)
            {
                AppendStage3Record(callerDpi, currentDpi, dpiMatch, false, true,
                    stage1Target, actual, "noop_skip", cmd);
                Console.Error.WriteLine(
                    $"[PLACEMENT:STAGE3] correction is no-op (recomputed rect == stage1Target, actual within {DeltaThreshold}px) -- skip SetWindowPos");
                return;
            }

            SetWindowPos(chromeHwnd, IntPtr.Zero, newL, newT, newW, newH, SWP_NOZORDER | SWP_NOACTIVATE);
            AppendSetWindowPosTrace(chromeHwnd, new RECT { Left = newL, Top = newT, Right = newL + newW, Bottom = newT + newH }, attempt: 0, callerHwnd: callerHwnd, stage: "stage3_dpi_correct");
            Console.Error.WriteLine(
                $"[PLACEMENT:STAGE3] DPI mismatch detected at caller={callerScale * 100:F0}% chrome={currentScale * 100:F0}%, "
                + $"correcting by [dX={correctedDx},dY={correctedDy}] new=(L={newL},T={newT},W={newW},H={newH})");

            AppendStage3Record(callerDpi, currentDpi, dpiMatch, true, true, corrected, actual, "corrected", cmd);
        }
        catch (Exception ex)
        {
            try { Console.Error.WriteLine($"[PLACEMENT:STAGE3] fatal: {ex.GetType().Name}: {ex.Message}"); } catch { }
        }
    }

    // ---- DPI helpers (stage 3) -----------------------------------------------------------------

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern uint GetDpiForWindow(IntPtr hwnd);

    /// <summary>
    /// GetDpiForWindow wrapped against older Win10 builds that may not export
    /// it. Falls back to 96 (100% scale) on any failure so Stage 3 still has
    /// a non-zero baseline to reason about.
    /// </summary>
    static uint TryGetWindowDpiSafe(IntPtr hwnd)
    {
        try
        {
            if (hwnd == IntPtr.Zero) return 96;
            uint dpi = GetDpiForWindow(hwnd);
            return dpi == 0 ? 96 : dpi;
        }
        catch
        {
            return 96;
        }
    }
}
