namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Post-launch Chrome placement: locate the newly launched project Chrome
    /// (windows of class "Chrome_WidgetWin_1" owned by chrome.exe) and SetWindowPos
    /// it next to the validated caller window. Uses SWP_NOZORDER | SWP_NOACTIVATE
    /// so focus is never stolen from the terminal that invoked this command.
    ///
    /// Best-effort: returns silently on any failure (no caller anchor, no Chrome
    /// window found, off-screen rect, etc.). Logged to wkappbot.hq/runtime/cdp-state.jsonl
    /// via the existing telemetry path.
    /// </summary>
    internal static void TryMoveWebBotNearCaller(string cmd)
    {
        try
        {
            var caller = LastValidatedCallerHwnd;
            var callerRect = LastValidatedCallerRect;
            Console.Error.WriteLine($"[PLACEMENT:STEP1] cmd={cmd} caller=0x{caller.ToInt64():X} rect=({callerRect.Left},{callerRect.Top},{callerRect.Right},{callerRect.Bottom})");
            if (caller == IntPtr.Zero || callerRect == System.Drawing.Rectangle.Empty)
            {
                Console.Error.WriteLine($"[PLACEMENT:STEP1] no caller anchor -- skip move (cmd={cmd})");
                return;
            }


            // Coordinate space: by the time this runs the launcher has called
            // TrySetPerMonitorV2DpiAwareness() in Main(), so callerRect is in
            // physical pixels — the same coordinate space Chrome (PerMonitorV2)
            // sees in SetWindowPos. No further LogicalToPhysical conversion
            // needed; mixing the two systems was the root cause of the
            // "Chrome ends up huge in the wrong place" bug.
            int callerLeft = callerRect.Left;
            int callerTop = callerRect.Top;

            // Default WebBot size: fixed 800x600 (matches Core's ChromeLauncher
            // default and what users expect from `cdp open` / `web open`).
            // Chrome_WidgetWin_1 renderer window consistently reports 18×9 pixels
            // smaller than SetWindowPos dimensions, likely due to Chrome's internal
            // client area calculation. Compensate by requesting slightly larger dimensions.
            const int DefaultChromeW = 800;
            const int DefaultChromeH = 710;  // outer height = content(600) + toolbar(~110px)
            const int CompensationW  = 18;  // pixels to add for Chrome internal sizing
            const int CompensationH  = 9;   // pixels to add for Chrome internal sizing
            int targetW = DefaultChromeW + CompensationW;
            int targetH = DefaultChromeH + CompensationH;

            // Compute target position: offset slightly down-right of the caller's
            // upper-left so the terminal stays visible behind/beside Chrome.
            // If caller center is not on any monitor, fall back to primary (100,100).
            int baseX = callerLeft;
            int baseY = callerTop;

            // Caller is already resolved to on-screen via ResolveValidCallerWindow above
            var callerCenterPt = new POINT
            {
                X = callerLeft + Math.Max(1, callerRect.Width) / 2,
                Y = callerTop + Math.Max(1, callerRect.Height) / 2
            };

            // Offset Chrome to the UPPER-LEFT of the terminal so the caller
            // terminal stays visible to the lower-right of Chrome (the user's
            // mental model: "Chrome pops up next to my terminal, not on top of
            // it"). This was previously +30 (down-right INSIDE the terminal),
            // which buried the terminal under Chrome. Mirrors the Core fix in
            // ChromeLauncher; soft monitor clamp below keeps the rect on
            // screen when the caller sits near the monitor's top-left edge.
            int targetX = baseX - 30;
            int targetY = baseY - 30;

            // Soft monitor clamp: with the upper-left offset (-30, -30) the
            // target rect can legitimately land slightly to the left/above
            // rcWork when the caller terminal sits near the top-left of the
            // monitor. Hard-clamping to rcWork.Left / rcWork.Top would snap
            // Chrome back onto the terminal, defeating Fix 1. So:
            //   LEFT/TOP: clamp to rcMonitor.Left/Top - 100 (allow 100px
            //             tolerance off the monitor edge -- still visible
            //             enough for the user to grab and move).
            //   RIGHT/BOTTOM: clamp inside rcMonitor minus a 200px guard so
            //                 Chrome's title bar / close button never run
            //                 off the right or bottom edge of the display.
            // Mirrors the Core ChromeLauncher fix.
            //
            // GUARD: If caller is off-screen, fall back to primary monitor (0,0).
            // This prevents Chrome from inheriting off-screen placement.
            int callerCenterX = callerLeft + Math.Max(1, (callerRect.Right - callerRect.Left)) / 2;
            int callerCenterY = callerTop + Math.Max(1, (callerRect.Bottom - callerRect.Top)) / 2;

            // Try caller's monitor first; fall back to primary (0,0) if caller is off-screen
            RECT rcMonitor = default;
            RECT rcWork = default;
            bool hasMonitor = TryGetMonitorRects(callerCenterX, callerCenterY, out rcMonitor, out rcWork);
            if (!hasMonitor)
            {
                // Caller off-screen or off-monitor: query primary monitor at (0,0)
                hasMonitor = TryGetMonitorRects(0, 0, out rcMonitor, out rcWork);
            }

            if (hasMonitor)
            {
                // Make sure Chrome fits inside the monitor bounds.
                if (targetW > rcMonitor.Right - rcMonitor.Left) targetW = rcMonitor.Right - rcMonitor.Left;
                if (targetH > rcMonitor.Bottom - rcMonitor.Top) targetH = rcMonitor.Bottom - rcMonitor.Top;
                // Soft clamp LEFT/TOP: allow up to 100px outside the monitor edge.
                int minX = rcMonitor.Left - 100;
                int minY = rcMonitor.Top  - 100;
                if (targetX < minX) targetX = minX;
                if (targetY < minY) targetY = minY;
                // Hard clamp RIGHT/BOTTOM: leave 200px so the title bar / close
                // button remain on-screen and reachable.
                int maxX = rcMonitor.Right  - 200;
                int maxY = rcMonitor.Bottom - 200;
                if (targetX + targetW > rcMonitor.Right)  targetX = Math.Min(rcMonitor.Right  - targetW, maxX);
                if (targetY + targetH > rcMonitor.Bottom) targetY = Math.Min(rcMonitor.Bottom - targetH, maxY);
            }
            else
            {
                // Monitor query failed (caller off-screen, no primary monitor found, etc.)
                // Force fallback to safe default: (100, 100) on primary display
                Console.Error.WriteLine($"[PLACEMENT:FALLBACK] monitor query failed for caller ({callerCenterX},{callerCenterY}) and primary (0,0) -> force (100,100)");
                targetX = 100;
                targetY = 100;
            }
            Console.Error.WriteLine($"[PLACEMENT:VALIDATE] caller=({callerLeft},{callerTop}) target=({targetX},{targetY}) offset=({targetX - callerLeft},{targetY - callerTop})");

            // Publish the validated target to Core via env var. Core's
            // ChromeLauncher.ComputePlacementNearCaller + CdpClient
            // SetWindowBoundsWaitStableAsync read WKAPPBOT_CHROME_TARGET first
            // so the --window-position flag, the post-launch stability loop,
            // and any per-command reposition use the SAME coordinates the
            // Launcher just computed. Without this Core falls back to
            // ExpectedBounds (rightmost monitor, ~1740,20) and "corrects"
            // Chrome back to that anchor every time, fighting the Launcher's
            // SetWindowPos and producing the well-known "Chrome ends up on
            // the wrong monitor" regression. Width/height passed are the
            // DESIRED final outer size (DefaultChromeW x DefaultChromeH),
            // matching Core's CdpClient.OuterWidthPx/OuterHeightPx constants.
            try
            {
                Environment.SetEnvironmentVariable(
                    "WKAPPBOT_CHROME_TARGET",
                    $"{targetX},{targetY},{DefaultChromeW},{DefaultChromeH}");
                Console.Error.WriteLine(
                    $"[PLACEMENT:ENV] WKAPPBOT_CHROME_TARGET={targetX},{targetY},{DefaultChromeW},{DefaultChromeH}");
            }
            catch { /* env set failure is non-fatal -- Core falls back to ExpectedBounds */ }

            // Find chrome.exe Browser window. Chrome_BrowserWindow is the main frame;
            // Chrome_WidgetWin_1 is a renderer tab window (different process).
            // Prioritize Chrome_BrowserWindow. If not found, fall back to Chrome_WidgetWin_1.
            var browserWindowCandidates = new System.Collections.Generic.List<(IntPtr hwnd, int pid, DateTime startedAt)>();
            var rendererCandidates = new System.Collections.Generic.List<(IntPtr hwnd, int pid, DateTime startedAt)>();

            EnumWindowsLocal((hwnd, _) =>
            {
                if (!IsWindowVisibleLocal(hwnd)) return true;
                var cls = new System.Text.StringBuilder(64);
                GetClassNameW(hwnd, cls, cls.Capacity);
                var clsStr = cls.ToString();

                GetWindowThreadProcessIdLocal(hwnd, out int wpid);
                if (wpid <= 0) return true;
                try
                {
                    using var p = System.Diagnostics.Process.GetProcessById(wpid);
                    if (!string.Equals(p.ProcessName, "chrome", StringComparison.OrdinalIgnoreCase))
                        return true;

                    if (clsStr == "Chrome_BrowserWindow")
                        browserWindowCandidates.Add((hwnd, wpid, p.StartTime.ToUniversalTime()));
                    else if (clsStr == "Chrome_WidgetWin_1")
                        rendererCandidates.Add((hwnd, wpid, p.StartTime.ToUniversalTime()));
                }
                catch { }
                return true;
            }, IntPtr.Zero);

            // Prefer browser window; fall back to renderer only if no browser window found
            var candidates = browserWindowCandidates.Count > 0 ? browserWindowCandidates : rendererCandidates;

            Console.Error.WriteLine($"[PLACEMENT:STEP2] found {candidates.Count} Chrome candidate(s)");
            if (candidates.Count == 0)
            {
                Console.Error.WriteLine($"[PLACEMENT:STEP2] no Chrome_WidgetWin_1 windows found -- skip move (cmd={cmd})");
                return;
            }

            // Pick the most recently started chrome.exe top-level window -- that's
            // the Chrome instance Core just launched (or attached to / reused).
            // Secondary key: largest visible window area, so the main browser window
            // (800x600+) is preferred over a small popup or app window from the same
            // process when startedAt times are equal (same chrome.exe process).
            candidates.Sort((a, b) => {
                int cmp = b.startedAt.CompareTo(a.startedAt);
                if (cmp != 0) return cmp;
                // Same process: prefer larger window (browser > popup)
                bool okA = TryGetWindowRectLTRB(a.hwnd, out var ra);
                bool okB = TryGetWindowRectLTRB(b.hwnd, out var rb);
                int areaA = okA ? ra.Width * ra.Height : 0;
                int areaB = okB ? rb.Width * rb.Height : 0;
                return areaB.CompareTo(areaA);
            });
            var target = candidates[0].hwnd;
            Console.Error.WriteLine($"[PLACEMENT:STEP3] target=0x{target.ToInt64():X} pid={candidates[0].pid} startedAt={candidates[0].startedAt:HH:mm:ss}");

            // SWP_NOZORDER (0x0004) | SWP_NOACTIVATE (0x0010) -- move without
            // disturbing focus or Z-order.
            const uint SWP_NOZORDER   = 0x0004;
            const uint SWP_NOACTIVATE = 0x0010;

            // Before SetWindowPos, restore Chrome to normal state (SW_RESTORE = 9)
            // so it releases any session-restore size lock and accepts our SetWindowPos.
            ShowWindow(target, 9); // SW_RESTORE
            System.Threading.Thread.Sleep(50);

            SetWindowPos(target, IntPtr.Zero, targetX, targetY, targetW, targetH, SWP_NOZORDER | SWP_NOACTIVATE);
            Console.Error.WriteLine($"[LAUNCHER] post-launch placed Chrome 0x{target.ToInt64():X} at ({targetX},{targetY},{targetW},{targetH}) near caller 0x{caller.ToInt64():X} (cmd={cmd})");

            // Triple-check + auto-correct: Chrome may ignore SetWindowPos when its
            // own session-restore positioner fires after our move, or when DWM is
            // mid-animation. Validate the final landing rect and re-issue
            // SetWindowPos until we're within the allowed delta.
            //
            // NOTE: We request (targetW, targetH) which includes compensation for
            // Chrome's internal sizing. But validation should check against the
            // DESIRED final size (DefaultChromeW, DefaultChromeH) not the compensation target.
            var expected = new RECT
            {
                Left   = targetX,
                Top    = targetY,
                Right  = targetX + DefaultChromeW,  // Compare against desired 800x600
                Bottom = targetY + DefaultChromeH,  // not the compensated 818x609
            };
            var (placementOk, finalRect, attempts) = TryValidateAndCorrectPlacement(target, expected, maxAttempts: 5);
            if (!placementOk)
            {
                Console.Error.WriteLine($"[LAUNCHER:WARN] Chrome placement failed validation after {attempts} attempts. "
                    + $"expected=(L={expected.Left},T={expected.Top},R={expected.Right},B={expected.Bottom}) "
                    + $"final=(L={finalRect.Left},T={finalRect.Top},R={finalRect.Right},B={finalRect.Bottom}) cmd={cmd}");
            }

            // Stage 1 done. Fork a detached child to handle Stage 2 (wait
            // for Page.loadEventFired and re-validate placement) and Stage 3
            // (DPI-aware match against Chrome's final monitor). The child
            // runs ~10s in the background while the user's prompt returns
            // immediately. See MyCdpContext.Stage23.cs for the implementation.
            //
            // Only fire if Stage 1 itself didn't bail with an invalid hwnd --
            // the helpers there assume a live Chrome window to act on.
            if (IsWindow(target))
            {
                SpawnBackgroundPlacementWatcher(target, expected, cmd);
            }
        }
        catch
        {
            // best-effort -- never crash the launcher exit path
        }
    }
}
