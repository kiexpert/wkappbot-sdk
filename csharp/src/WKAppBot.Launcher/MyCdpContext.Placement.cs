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
            const int DefaultChromeH = 600;
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

            int targetX = baseX + 30;
            int targetY = baseY + 30;

            // Clamp target rect to the caller's monitor work area so Chrome
            // never spills onto another display. Uses MonitorFromPoint at the
            // caller's centre + GetMonitorInfo to read MONITORINFO.rcWork
            // (excludes taskbar). Same DPI context as the caller because the
            // launcher is now PerMonitorV2-aware.
            //
            // GUARD: If caller is off-screen, fall back to primary monitor (0,0).
            // This prevents Chrome from inheriting off-screen placement.
            int callerCenterX = callerLeft + Math.Max(1, (callerRect.Right - callerRect.Left)) / 2;
            int callerCenterY = callerTop + Math.Max(1, (callerRect.Bottom - callerRect.Top)) / 2;

            // Try caller's monitor first; fall back to primary (0,0) if caller is off-screen
            RECT workArea = default;
            bool hasWorkArea = TryGetWorkArea(callerCenterX, callerCenterY, out workArea);
            if (!hasWorkArea)
            {
                // Caller off-screen or off-monitor: query primary monitor at (0,0)
                hasWorkArea = TryGetWorkArea(0, 0, out workArea);
            }

            if (hasWorkArea)
            {
                // Make sure Chrome fits inside the work area.
                if (targetW > workArea.Right - workArea.Left) targetW = workArea.Right - workArea.Left;
                if (targetH > workArea.Bottom - workArea.Top) targetH = workArea.Bottom - workArea.Top;
                // Clamp X/Y so the full Chrome rect lives on this monitor.
                if (targetX < workArea.Left)                   targetX = workArea.Left;
                if (targetY < workArea.Top)                    targetY = workArea.Top;
                if (targetX + targetW > workArea.Right)        targetX = workArea.Right - targetW;
                if (targetY + targetH > workArea.Bottom)       targetY = workArea.Bottom - targetH;
            }
            else
            {
                // workArea query failed (caller off-screen, no primary monitor found, etc.)
                // Force fallback to safe default: (100, 100) on primary display
                Console.Error.WriteLine($"[PLACEMENT:FALLBACK] workArea query failed for caller ({callerCenterX},{callerCenterY}) and primary (0,0) -> force (100,100)");
                targetX = 100;
                targetY = 100;
            }

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
            // Note: on tab-reuse, the start time will be the original Chrome's
            // start time, but among multiple chrome.exe processes the recycled
            // one is typically also the most-recently-used (Chrome reuses the
            // newest in its session-restore queue).
            candidates.Sort((a, b) => b.startedAt.CompareTo(a.startedAt));
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
