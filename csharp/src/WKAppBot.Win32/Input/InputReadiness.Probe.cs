using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public sealed partial class InputReadiness
{
    // -- Probe: 전수조사 ------------------------------------------

    public InputReadinessReport Probe(InputReadinessRequest req)
    {
        // Dry-run gate: block all input actions at the native readiness level.
        // This is the final safety net -- even if higher-level gates are accidentally bypassed,
        // no write/input action can proceed without Probe() approval.
        if (DryRunMode.Value)
        {
            ReadinessCalled = true;
            var action = req.IntendedAction ?? "unknown";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[DRY-RUN] InputReadiness.Probe() blocked: {action} on hwnd=0x{req.TargetHwnd:X}");
            Console.ResetColor();
            return new InputReadinessReport
            {
                TargetHwnd = req.TargetHwnd,
                Methods = new List<InputMethod>() // empty = no available methods = action cannot proceed
            };
        }

        ReadinessCalled = true;
        var swProbe = Stopwatch.StartNew();
        long msInit = 0, msElevation = 0, msZoom = 0, msUia = 0, msWin32 = 0, msSendInput = 0;
        long msBlocker = 0, msKnowhow = 0, msYield = 0;

        // MainHwnd 자동 유도: GetAncestor(GA_ROOT) -> 진짜 최상위 윈도우로 방해꾼 감지 스코프 확장
        var swStep = Stopwatch.StartNew();
        var mainHwnd = req.MainHwnd != IntPtr.Zero
            ? req.MainHwnd
            : NativeMethods.GetAncestor(req.TargetHwnd, NativeMethods.GA_ROOT);
        if (mainHwnd == IntPtr.Zero) mainHwnd = req.TargetHwnd;
        var methods = new List<InputMethod>();
        BlockerInfo? blocker = null;
        IActionZoom? zoom = null;

        // -- 타겟 기본 정보 --
        var classSb = new StringBuilder(256);
        NativeMethods.GetClassNameW(req.TargetHwnd, classSb, 256);
        var targetClass = classSb.ToString();

        NativeMethods.GetWindowThreadProcessId(req.TargetHwnd, out uint targetPid);
        string procName = "";
        try { procName = Process.GetProcessById((int)targetPid).ProcessName; } catch { }

        string? targetAid = null;
        string? targetName = null;
        try
        {
            if (req.UiaElement != null)
            {
                targetAid = req.UiaElement.Properties.AutomationId.ValueOrDefault;
                targetName = req.UiaElement.Properties.Name.ValueOrDefault;
            }
        }
        catch { }

        // -- 윈도우 상태 --
        bool formVisible = NativeMethods.IsWindowVisible(mainHwnd);
        bool formEnabled = NativeMethods.IsWindowEnabled(mainHwnd);
        bool formIconic = NativeMethods.IsIconic(mainHwnd);

        // -- 권한 --
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();
        bool targetElevated = NativeMethods.IsProcessElevated(targetPid) ?? true;
        bool elevationMismatch = targetElevated && !weAreElevated;
        swStep.Stop();
        msInit = swStep.ElapsedMilliseconds;

        // -- 권한 상승 요청 (UIPI 차단 방지) --
        if (elevationMismatch && ElevationRequester != null && !req.QuickMode)
        {
            swStep.Restart();
            zoom?.UpdateStatus("관리자 권한 필요 -- UAC 요청 중...");
            if (ElevationRequester.RequestElevation(procName, targetPid))
            {
                return new InputReadinessReport
                {
                    TargetHwnd = req.TargetHwnd, TargetClass = targetClass,
                    TargetPid = targetPid, ProcessName = procName,
                    Methods = methods, TargetElevated = targetElevated,
                    WeAreElevated = weAreElevated, FormVisible = formVisible,
                    FormEnabled = formEnabled, FormIconic = formIconic,
                };
            }
            swStep.Stop();
            msElevation = swStep.ElapsedMilliseconds;
        }

        // -- 유저 입력 간섭 분석 --
        uint idleMs = NativeMethods.GetUserIdleMs();
        bool userRecent = idleMs < req.UserIdleThresholdMs;

        // -- 돋보기 (QuickMode가 아닐 때만) --
        if (!req.QuickMode && !req.SkipZoom && ZoomFactory != null)
        {
            swStep.Restart();
            try
            {
                var rect = GetTargetRect(req.TargetHwnd, req.UiaElement);
                if (rect.Width > 0 && rect.Height > 0)
                {
                    var action = req.IntendedAction ?? "probe";
                    var label = targetAid ?? targetName ?? targetClass;
                    zoom = ZoomFactory.Begin(rect, mainHwnd, action, label);
                    zoom?.UpdateStatus("전수조사 중...");
                }
            }
            catch { }
            swStep.Stop();
            msZoom = swStep.ElapsedMilliseconds;
        }

        if (!req.QuickMode)
        {
            // -- 1. UIA 패턴 전수조사 (focusless) --
            swStep.Restart();
            if (req.UiaElement != null)
                ProbeUiaPatterns(req.UiaElement, req.IntendedAction, methods);
            swStep.Stop();
            msUia = swStep.ElapsedMilliseconds;

            // -- 2. Win32 메시지 (focusless) --
            swStep.Restart();
            ProbeWin32Messages(targetClass, methods);
            swStep.Stop();
            msWin32 = swStep.ElapsedMilliseconds;

            // -- 3. SendInput (focus-needed) --
            swStep.Restart();
            ProbeSendInput(targetElevated, weAreElevated, methods);
            swStep.Stop();
            msSendInput = swStep.ElapsedMilliseconds;
        }

        // -- 방해꾼 감지 --
        swStep.Restart();
        if (!req.SkipBlockerCheck)
            blocker = DetectBlocker(mainHwnd);
        swStep.Stop();
        msBlocker = swStep.ElapsedMilliseconds;

        // -- 노하우 방송 --
        swStep.Restart();
        if (!req.SkipKnowhow && !req.QuickMode)
            KnowhowBroadcaster?.Broadcast(mainHwnd, req.FormId, req.ControlId, req.IntendedAction);
        swStep.Stop();
        msKnowhow = swStep.ElapsedMilliseconds;

        // -- 유저 입력 간섭 대기 --
        bool yieldRequested = false;
        bool yieldConfirmed = false;
        bool yieldFocusAcquired = false;
        // [FOCUS-GUARD] 오버레이 클릭 여부 -- 유저가 실제로 클릭했을 때만 grace 부여.
        // 자동승인(AutoApproveYield, 3초 timeout)은 클릭 없으므로 grace 불필요.
        // 오버레이 클릭 -> 마우스 입력 -> idleMs≈0ms -> CheckActiveGuard가 오탐 차단하는 문제 방지용.
        bool explicitUserClickApproved = false;

        // -- FocusStealer / MouseStealer prop check --
        // ActionApi stamps these props when a nominally-focusless UIA action
        // steals keyboard focus OR moves the mouse cursor without user input.
        // Two separate props -> both can coexist on same hwnd+action:
        //   "WKAppBot_FocusStealer-{action}" -- foreground window stolen
        //   "WKAppBot_MouseStealer-{action}"  -- cursor moved
        // Force yield popup on next Probe() so user gets warned.
        bool focusStealerFlagged = false;
        try
        {
            if (!req.QuickMode && mainHwnd != IntPtr.Zero)
            {
                const string FocusStealerPrefix = "WKAppBot_FocusStealer-";
                const string MouseStealerPrefix  = "WKAppBot_MouseStealer-";
                var act = req.IntendedAction?.ToLowerInvariant() ?? "";

                bool focusStolen = NativeMethods.GetPropW(mainHwnd, $"{FocusStealerPrefix}{act}") != IntPtr.Zero;
                // midInput: stamped when mid-keystroke focus drift aborted a type/set-value action
                // -> force yield on ANY subsequent action on this hwnd until cleared
                bool midInputStealer = NativeMethods.GetPropW(mainHwnd, $"{FocusStealerPrefix}midInput") != IntPtr.Zero;
                // Generic stamp (action-agnostic): set whenever ANY action stole focus
                // Catches cross-action mismatches (e.g., click stole -> next probe is type)
                bool anyFocusStealer = NativeMethods.GetPropW(mainHwnd, "WKAppBot_FocusStealer") != IntPtr.Zero;
                // Mouse prop stored as "mouse-{action}" by ActionApi
                bool mouseStolen = NativeMethods.GetPropW(mainHwnd, $"{MouseStealerPrefix}mouse-{act}") != IntPtr.Zero;

                if (focusStolen || midInputStealer || anyFocusStealer || mouseStolen)
                {
                    focusStealerFlagged = true;
                    var kinds = string.Join("+",
                        new[] { (focusStolen || midInputStealer || anyFocusStealer) ? "focus" : null, mouseStolen ? "mouse" : null }
                        .Where(s => s != null));
                    if (string.IsNullOrEmpty(kinds)) kinds = "focus";
                    var midTag = midInputStealer ? " (mid-input drift)" : anyFocusStealer ? " (generic)" : "";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        $"  [READINESS] ! {kinds.ToUpperInvariant()} STEALER{midTag}: '{req.IntendedAction}' " +
                        $"previously grabbed {kinds} on hwnd=0x{mainHwnd:X} -> forcing yield popup");
                    Console.ResetColor();
                }
            }
        }
        catch { }

        if (!req.QuickMode && (NeedsFocusForAction(methods, req.IntendedAction) || focusStealerFlagged))
        {
            bool targetIsForeground = mainHwnd != IntPtr.Zero
                && NativeMethods.GetForegroundWindow() == mainHwnd;

            if (targetIsForeground && !userRecent && req.AutoApproveYield && !focusStealerFlagged)
            {
                // -- 자동승인 즉시 (AutoApproveYield=true: Slack 프롬프트 전달 등 완전 자동화) --
                // focusStealerFlagged=true 시 절대 묵살 금지 -> 팝업 강제 표시
                yieldRequested = true;
                yieldConfirmed = true;
                yieldFocusAcquired = true;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"  [READINESS] Target foreground + user idle {idleMs / 1000}s -- silent auto-approve (AutoApproveYield)");
                Console.ResetColor();
            }
            else if (targetIsForeground && !userRecent && UserInputWait != null)
            {
                // -- 타겟 전경 + 장기 idle -> 3초 팝업 (잘보이게, 졸다가 막을 수 있게) --
                yieldRequested = true;
                zoom?.UpdateStatus($"유저 idle {idleMs / 1000}s -- 3초 후 자동 진행...");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"  [READINESS] Target foreground + user idle {idleMs / 1000}s -- 3s popup (cancellable)");
                Console.ResetColor();
                Console.Out.Flush();

                swStep.Restart();
                var yieldResult = UserInputWait.WaitForUserYield(mainHwnd, idleMs, timeoutSeconds: 3,
                    positionHwnd: mainHwnd);
                Console.WriteLine($"  [READINESS] 3s popup: approved={yieldResult.Approved} [{swStep.ElapsedMilliseconds}ms]");
                yieldConfirmed = yieldResult.Approved;
                yieldFocusAcquired = yieldResult.FocusAcquired || targetIsForeground;
                // Set grace so CheckActiveGuard doesn't re-prompt for subsequent mouse/key ops
                if (yieldResult.Approved) explicitUserClickApproved = true;
            }
            else if (req.AutoApproveYield)
            {
                // -- 자동승인 즉시 포커스 확보 (키보드+마우스) -- 갭 없이! --
                yieldRequested = true;
                yieldConfirmed = true;

                if (mainHwnd != IntPtr.Zero)
                {
                    yieldFocusAcquired = NativeMethods.SmartSetForegroundWindow(mainHwnd);
                    Console.ForegroundColor = yieldFocusAcquired ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"  [READINESS] Auto-yield -> focus={(yieldFocusAcquired ? "OK" : "FAIL")} (fg=0x{NativeMethods.GetForegroundWindow():X}, target=0x{mainHwnd:X})");
                    Console.ResetColor();
                }
            }
            else if (UserInputWait != null)
            {
                // -- 타겟이 비전경 -> 무조건 팝업 (30초), 유저 활동 시에도 팝업 --
                yieldRequested = true;
                int yieldTimeout = targetIsForeground
                    ? req.UserYieldTimeoutSeconds  // 전경+유저활동: 기존 타임아웃
                    : 30;                          // 비전경: 무조건 30초

                var reason = targetIsForeground
                    ? $"유저 입력 감지 ({idleMs}ms ago)"
                    : "타겟이 전경이 아님";
                zoom?.UpdateStatus($"{reason} -- 확인 대기 중...");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [READINESS] {reason} -- showing yield overlay ({yieldTimeout}s)...");
                Console.ResetColor();
                Console.Out.Flush();

                swStep.Restart();
                var yieldResult = UserInputWait.WaitForUserYield(mainHwnd, idleMs, yieldTimeout,
                    positionHwnd: req.TargetHwnd);
                swStep.Stop();
                msYield = swStep.ElapsedMilliseconds;
                yieldConfirmed = yieldResult.Approved;
                yieldFocusAcquired = yieldResult.FocusAcquired;
                // Set grace on any approval so FOCUS-GUARD doesn't re-prompt
                if (yieldResult.Approved) explicitUserClickApproved = true;

                if (yieldConfirmed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(yieldFocusAcquired
                        ? "  [READINESS] User confirmed -- focus pre-acquired"
                        : "  [READINESS] Auto-approved (user idle) -- proceeding");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [READINESS] Yield safety timeout -- proceeding anyway");
                    Console.ResetColor();
                }
            }

            // -- FocusStealer props 클리어 (유저 승인 후 재시도에서 반복 팝업 방지) --
            if (yieldConfirmed && mainHwnd != IntPtr.Zero)
            {
                try { NativeMethods.RemovePropW(mainHwnd, "WKAppBot_FocusStealer-midInput"); } catch { }
                try { NativeMethods.RemovePropW(mainHwnd, "WKAppBot_FocusStealer"); } catch { }
            }

            // -- CheckActiveGuard 그레이스 타임스탬프 --
            // [FOCUS-GUARD] 오직 유저가 오버레이를 직접 클릭한 경우에만 grace 부여!
            //
            // Grace가 필요한 이유:
            //   오버레이 클릭 -> 마우스 입력 -> idleMs≈0ms -> CheckActiveGuard가 오탐 차단
            //   -> 유저가 명시적으로 승인했는데 포커스 스틸이 막히는 문제
            //
            // Grace가 필요 없는 경우:
            //   AutoApproveYield (유저 클릭 없음) -> idle timer 미리셋 -> 오탐 없음
            //   3초 popup 자동 timeout -> 유저 클릭 없음 -> 오탐 없음
            //   -> 이 경우 grace를 주면 유저가 타이핑 시작해도 포커스 스틸 허용되는 버그!
            if (explicitUserClickApproved)
                _lastYieldApprovedAt = DateTime.UtcNow;
        }

        // -- 입력위치 확보 콜백 (yield 승인 후) --
        bool? inputPositionEnsured = null;
        if (req.EnsureInputPosition != null)
        {
            swStep.Restart();
            try
            {
                inputPositionEnsured = req.EnsureInputPosition();
                Console.WriteLine($"  [READINESS] EnsureInputPosition: {(inputPositionEnsured.Value ? "v secured" : "X failed")} [{swStep.ElapsedMilliseconds}ms]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [READINESS] EnsureInputPosition error: {ex.Message}");
                inputPositionEnsured = false;
            }
        }

        // -- 프로파일링 출력 --
        swProbe.Stop();
        Console.Error.WriteLine($"  [PROF:PROBE] init={msInit}ms zoom={msZoom}ms uia={msUia}ms win32={msWin32}ms send={msSendInput}ms blocker={msBlocker}ms knowhow={msKnowhow}ms yield={msYield}ms TOTAL={swProbe.ElapsedMilliseconds}ms");
        Console.Out.Flush();

        // -- 돋보기 상태 업데이트 --
        if (zoom != null)
        {
            if (yieldRequested && !yieldConfirmed)
                zoom.UpdateStatus("안전 타임아웃 -- EnsureFocus 진행");
            else if (blocker != null)
                zoom.UpdateStatus($"BLOCKED: {blocker.Title}");
            else if (methods.Any(m => m.Available))
                zoom.UpdateStatus($"Ready: {methods.First(m => m.Available).Name}");
            else
                zoom.UpdateStatus("No methods available");
        }

        var report = new InputReadinessReport
        {
            TargetHwnd = req.TargetHwnd,
            TargetClass = targetClass,
            TargetAutomationId = targetAid,
            TargetName = targetName,
            TargetPid = targetPid,
            ProcessName = procName,
            Methods = methods,
            ActiveBlocker = blocker,
            TargetElevated = targetElevated,
            WeAreElevated = weAreElevated,
            FormVisible = formVisible,
            FormEnabled = formEnabled,
            FormIconic = formIconic,
            UserIdleMs = idleMs,
            UserInputRecent = userRecent,
            UserYieldRequested = yieldRequested,
            UserYieldConfirmed = yieldConfirmed,
            UserYieldFocusAcquired = yieldFocusAcquired,
            InputPositionEnsured = inputPositionEnsured,
            Zoom = zoom,
        };

        // Fire auto a11y hack event (non-blocking)
        try { OnProbeSuccess?.Invoke(req.TargetHwnd, procName, targetClass); } catch { }

        return report;
    }
}
