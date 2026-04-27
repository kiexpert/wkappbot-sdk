using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public sealed partial class InputReadiness
{
    // -- ProbeAtPoint: 좌표 기반 입력확보 ------------------------─

    /// <summary>
    /// 좌표 기반 입력확보: 해당 좌표에 겹쳐있는 윈도우 스택을 Z-order로 수집,
    /// 앞에서부터 포커스리스 클릭 시도, 방해꾼 dismiss, 타겟을 앞으로 보내기 등.
    /// 필수 인수: ScreenX, ScreenY, TargetHwnd -- 나머지 자동 유도.
    /// Tag: [READINESS]
    /// </summary>
    public PointReadinessReport ProbeAtPoint(PointReadinessRequest req)
    {
        ReadinessCalled = true;
        var sw = Stopwatch.StartNew();

        // -- Step 0: 자동 유도 --
        var mainHwnd = req.MainHwnd != IntPtr.Zero
            ? req.MainHwnd
            : NativeMethods.GetAncestor(req.TargetHwnd, NativeMethods.GA_ROOT);
        if (mainHwnd == IntPtr.Zero) mainHwnd = req.TargetHwnd;

        NativeMethods.GetWindowThreadProcessId(req.TargetHwnd, out uint targetPid);
        string procName = "";
        try { procName = Process.GetProcessById((int)targetPid).ProcessName; } catch { }

        var action = req.IntendedAction ?? "click";

        // -- Step 0.5: 미니마이즈 감지 --
        bool isMinimized = NativeMethods.IsIconic(mainHwnd) || NativeMethods.IsIconic(req.TargetHwnd);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[READINESS] ");
        Console.ResetColor();
        Console.Write($"ProbeAtPoint ({req.ScreenX},{req.ScreenY}) target=0x{req.TargetHwnd:X}");
        Console.Write($" main=0x{mainHwnd:X} pid={targetPid} {procName}");
        if (isMinimized) Console.Write(" [MINIMIZED]");
        Console.WriteLine();

        // -- Step 1: 윈도우 스택 수집 (Z-order) -- 미니마이즈면 스킵 --
        var windowStack = new List<WindowAtPoint>();
        var classification = PointClassification.TargetNotFound;

        if (isMinimized)
        {
            // 미니마이즈: Z-order 의미 없음 -> 바로 포커스리스 경로
            classification = PointClassification.TargetMinimized;
        }
        else
        {
            windowStack = CollectWindowStack(req.ScreenX, req.ScreenY, req.TargetHwnd, targetPid);

            // Print stack for diagnostics
            foreach (var w in windowStack)
            {
                var color = w.IsTarget ? ConsoleColor.Green
                    : w.IsBlocker ? ConsoleColor.Yellow
                    : ConsoleColor.DarkGray;
                Console.ForegroundColor = color;
                Console.Write($"  [{w.ZOrder}] ");
                Console.ResetColor();
                Console.Write($"0x{w.Handle:X} [{w.ClassName}]");
                if (!string.IsNullOrEmpty(w.Title)) Console.Write($" \"{w.Title}\"");
                if (w.IsTarget) Console.Write(" <-TARGET");
                if (w.IsBlocker) Console.Write(" <-BLOCKER");
                Console.WriteLine();
            }

            // -- Step 2: 분류 --
            classification = ClassifyPoint(windowStack, req.TargetHwnd, targetPid, mainHwnd);
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("  [CLASS] ");
        Console.ResetColor();
        Console.WriteLine(classification.ToString());

        // -- Step 3: 포커스리스 클릭 --
        // UIA Invoke/Toggle/Select는 핸들 기반이라 앞에 뭐가 있든 동작함.
        // 미니마이즈/off-screen도 UIA 핸들 기반으로 동작! (좌표는 UIA rect 기준으로 리매핑)
        bool pathCleared = false;
        string? pathClearMethod = null;
        bool focuslessClicked = false;
        bool foregroundStolen = false;
        IntPtr resolvedHwnd = req.TargetHwnd;
        string? resolvedDetail = null;

        // 핸들이 유효하면 무조건 포커스리스 시도!
        // TargetNotFound여도 핸들 살아있으면 UIA Invoke 가능 (off-screen, 미니마이즈 등)
        bool targetHandleValid = NativeMethods.IsWindow(req.TargetHwnd);
        bool isOffScreen = isMinimized || classification == PointClassification.TargetNotFound;

        if (targetHandleValid)
        {
            if (isMinimized)
            {
                // 미니마이즈: 순간 복원(NOACTIVATE) -> UIA 클릭 -> 재미니마이즈
                (focuslessClicked, resolvedDetail, foregroundStolen) =
                    TryFocuslessOnMinimized(req.ScreenX, req.ScreenY, req.TargetHwnd, mainHwnd);
            }
            else
            {
                int probeX = req.ScreenX, probeY = req.ScreenY;

                if (isOffScreen)
                {
                    // off-screen (미니마이즈 아닌): UIA rect 기준으로 좌표 리매핑 + 클램핑
                    (probeX, probeY) = RemapToUiaBounds(req.ScreenX, req.ScreenY, req.TargetHwnd);
                }

                (focuslessClicked, resolvedDetail, foregroundStolen) = TryFocuslessAtPoint(
                    probeX, probeY, req.TargetHwnd);
            }

            if (focuslessClicked)
            {
                Console.ForegroundColor = foregroundStolen ? ConsoleColor.Yellow : ConsoleColor.Green;
                Console.Write(foregroundStolen ? "  [FL] ! " : "  [FL] ");
                string offTag = isOffScreen ? (isMinimized ? "minimized " : "offscreen ") : "";
                Console.WriteLine($"Focusless {offTag}{(foregroundStolen ? "OK but fg stolen" : "OK")} -> {resolvedDetail}");
                Console.ResetColor();
            }
        }

        // -- Step 4: 포커스리스 실패 -> 물리클릭 경로 (Z-order 정리 필요) --
        // off-screen이면 물리클릭 불가 -> 포커스리스 실패 = 전체 실패
        if (!focuslessClicked && !isOffScreen)
        {
            switch (classification)
            {
                case PointClassification.TargetOnTop:
                    // 이미 최상단 -> 물리클릭 준비 완료
                    break;

                case PointClassification.BlockerOnTop:
                    // 방해꾼 dismiss (좌표 무관, 핸들러가 처리)
                    var topBlocker = windowStack.FirstOrDefault(w => w.IsBlocker);
                    if (topBlocker != null && BlockerHandler != null)
                    {
                        var blockerInfo = BuildBlockerInfo(topBlocker.Handle);
                        if (blockerInfo != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  [PATH] Dismissing blocker: \"{blockerInfo.Title}\"");
                            Console.ResetColor();

                            var (handled, _) = BlockerHandler.TryHandle(mainHwnd, blockerInfo);
                            if (handled)
                            {
                                pathCleared = true;
                                pathClearMethod = "blocker_dismissed";
                                Thread.Sleep(300);
                            }
                        }
                    }
                    break;

                case PointClassification.SiblingOnTop:
                    // 같은 앱 자식 윈도우 순서 조정 (focusless)
                    BringTargetForward(req.TargetHwnd, mainHwnd);
                    pathCleared = true;
                    pathClearMethod = "sibling_reordered";
                    Thread.Sleep(200);
                    break;

                case PointClassification.ForeignOnTop:
                    // 다른 앱이 앞 -> 타겟을 앞으로 (focusless: SetWindowPos NOACTIVATE)
                    BringTargetForward(req.TargetHwnd, mainHwnd);
                    pathCleared = true;
                    pathClearMethod = "brought_forward";
                    Thread.Sleep(200);
                    break;
            }
        }

        if (isOffScreen && !focuslessClicked)
            resolvedDetail = isMinimized
                ? "Target minimized -- focusless failed, physical click impossible"
                : "Target off-screen -- focusless failed, physical click impossible";

        bool needsPhysical = !focuslessClicked && !isOffScreen;
        bool ready = focuslessClicked || needsPhysical;

        // -- 결과 출력 --
        Console.ForegroundColor = focuslessClicked ? ConsoleColor.Green
            : needsPhysical ? ConsoleColor.Yellow
            : ConsoleColor.Red;
        Console.Write("  [RESULT] ");
        Console.ResetColor();

        if (focuslessClicked)
            Console.WriteLine($"FocuslessClick -> {resolvedDetail} ({sw.ElapsedMilliseconds}ms)");
        else if (needsPhysical)
            Console.WriteLine($"NeedsPhysicalClick -> {resolvedDetail ?? "fallback"} ({sw.ElapsedMilliseconds}ms)");
        else
            Console.WriteLine($"Failed: {resolvedDetail} ({sw.ElapsedMilliseconds}ms)");

        return new PointReadinessReport
        {
            WindowStack = windowStack,
            Classification = classification,
            ResolvedHwnd = resolvedHwnd,
            ResolvedDetail = resolvedDetail,
            PathCleared = pathCleared,
            PathClearMethod = pathClearMethod,
            FocuslessClicked = focuslessClicked,
            ForegroundStolen = foregroundStolen,
            Ready = ready,
            NeedsPhysicalClick = needsPhysical,
        };
    }

    // -- PrintPointReport: 좌표 기반 결과 콘솔 출력 --------------

    public static void PrintPointReport(PointReadinessReport report)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[READINESS] ");
        Console.ResetColor();
        Console.Write($"PointProbe: {report.Classification}");
        Console.Write($" stack={report.WindowStack.Count}");
        if (report.FocuslessClicked) Console.Write(" FOCUSLESS_OK");
        else if (report.NeedsPhysicalClick) Console.Write(" NEEDS_PHYSICAL");
        if (report.PathCleared) Console.Write($" path={report.PathClearMethod}");
        Console.WriteLine();
    }
}

// -- Point Readiness Types ----------------------------------------

/// <summary>
/// 좌표 기반 입력확보 요청. 필수 인수 3개만: ScreenX, ScreenY, TargetHwnd.
/// </summary>
public record PointReadinessRequest
{
    /// <summary>필수: 스크린 X 좌표.</summary>
    public required int ScreenX { get; init; }

    /// <summary>필수: 스크린 Y 좌표.</summary>
    public required int ScreenY { get; init; }

    /// <summary>필수: 의도한 타겟 윈도우 핸들.</summary>
    public required IntPtr TargetHwnd { get; init; }

    /// <summary>선택: 메인 윈도우 핸들. 미지정 -> GetAncestor(GA_ROOT) 자동.</summary>
    public IntPtr MainHwnd { get; init; }

    /// <summary>선택: 예정 액션. 미지정 -> "click".</summary>
    public string? IntendedAction { get; init; }

    /// <summary>선택: 포커스리스만 허용 (--fl 플래그).</summary>
    public bool FocuslessOnly { get; init; }
}

/// <summary>좌표 기반 입력확보 결과.</summary>
public record PointReadinessReport
{
    /// <summary>해당 좌표의 윈도우 스택 (Z-order, 앞->뒤).</summary>
    public List<WindowAtPoint> WindowStack { get; init; } = new();

    /// <summary>분류 결과.</summary>
    public PointClassification Classification { get; init; }

    /// <summary>최종 클릭 대상 핸들.</summary>
    public IntPtr ResolvedHwnd { get; init; }
    /// <summary>최종 클릭 상세 (예: "[TabItem] 라인설정 (Invoke, focusless)").</summary>
    public string? ResolvedDetail { get; init; }

    /// <summary>경로 정리 수행됨 (방해꾼 dismiss, 타겟 앞으로 등).</summary>
    public bool PathCleared { get; init; }
    /// <summary>경로 정리 방법 ("blocker_dismissed", "brought_forward" 등).</summary>
    public string? PathClearMethod { get; init; }

    /// <summary>ProbeAtPoint 내에서 포커스리스 클릭 완료됨!</summary>
    public bool FocuslessClicked { get; init; }

    /// <summary>
    /// 포커스리스 클릭은 성공했지만, 전경이 의도치 않게 변경됨!
    /// MFC Invoke 부작용: BN_CLICKED -> 내부 SetForegroundWindow -> 타겟이 전경으로.
    /// 이 플래그가 true면 호출자가 경고 알림을 표시해야 함.
    /// </summary>
    public bool ForegroundStolen { get; init; }

    /// <summary>클릭 가능 상태.</summary>
    public bool Ready { get; init; }
    /// <summary>포커스리스 실패 -> 물리클릭 필요.</summary>
    public bool NeedsPhysicalClick { get; init; }
}

/// <summary>특정 좌표에 걸쳐있는 윈도우 정보.</summary>
public record WindowAtPoint(
    IntPtr Handle,
    string ClassName,
    string? Title,
    uint ProcessId,
    bool IsTarget,
    bool IsBlocker,
    int ZOrder
);

/// <summary>좌표 기반 윈도우 스택 분류.</summary>
public enum PointClassification
{
    /// <summary>타겟이 최상단 -> 바로 클릭.</summary>
    TargetOnTop,
    /// <summary>방해꾼이 최상단 -> dismiss 후 재시도.</summary>
    BlockerOnTop,
    /// <summary>같은 앱의 다른 창이 최상단 -> 타겟을 앞으로.</summary>
    SiblingOnTop,
    /// <summary>다른 앱 창이 최상단.</summary>
    ForeignOnTop,
    /// <summary>타겟이 스택에 없음 (좌표 오류).</summary>
    TargetNotFound,
    /// <summary>타겟이 미니마이즈됨 -> Z-order 무관, UIA 핸들 기반 포커스리스.</summary>
    TargetMinimized
}
