using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

// -- Enums & Records ----------------------------------------------─

/// <summary>Input method categories, ordered by preference (focusless first).</summary>
public enum InputMethodCategory { UiaPattern, MsaaAccessible, Win32Message, SendInput }

/// <summary>A single input method availability entry.</summary>
public record InputMethod(
    string Name,
    InputMethodCategory Category,
    bool Focusless,
    bool Available,
    string? UnavailableReason
);

/// <summary>Detected blocker/popup info.</summary>
public record BlockerInfo(
    IntPtr Handle,
    string ClassName,
    string ClassPath,
    string Title,
    string? MessageText,
    uint ProcessId,
    string ProcessName
);

// -- Request ------------------------------------------------------─

/// <summary>
/// 입력위치확보 요청 구조체.
/// 명확한 필드명으로 필요사항과 플래그를 담아서 넘김.
/// </summary>
public record InputReadinessRequest
{
    /// <summary>필수: 타겟 윈도우/컨트롤 핸들.</summary>
    public required IntPtr TargetHwnd { get; init; }

    /// <summary>선택: UIA 요소 (있으면 UIA 패턴 전수조사 빠름).</summary>
    public AutomationElement? UiaElement { get; init; }

    /// <summary>선택: 예정 액션 ("click", "type", "scroll", "toggle", "msaa-probe" 등).</summary>
    public string? IntendedAction { get; init; }

    /// <summary>선택: 메인 윈도우 핸들 (방해꾼 감지 스코프). 없으면 TargetHwnd 사용.</summary>
    public IntPtr MainHwnd { get; init; }

    /// <summary>선택: 경험DB/노하우용 폼 ID.</summary>
    public string? FormId { get; init; }

    /// <summary>선택: 경험DB/노하우용 컨트롤 ID.</summary>
    public int? ControlId { get; init; }

    // -- 플래그 --
    /// <summary>방해꾼 체크 생략.</summary>
    public bool SkipBlockerCheck { get; init; }

    /// <summary>노하우 방송 생략.</summary>
    public bool SkipKnowhow { get; init; }

    /// <summary>돋보기 생략.</summary>
    public bool SkipZoom { get; init; }

    /// <summary>QuickMode: DetectBlocker만 (전수조사 생략, ~5ms).</summary>
    public bool QuickMode { get; init; }

    /// <summary>유저 입력 간섭 판단 임계값 (ms). 기본 30000 (30초).
    /// 이 시간 이내 입력 있으면 승인창, 넘으면 무조건 자동승인.</summary>
    public uint UserIdleThresholdMs { get; init; } = 30_000;

    /// <summary>UserInputRecent 시 포커스 양보 확인 대기창 표시. 기본 true.</summary>
    public bool WaitForUserIfBusy { get; init; } = true;

    /// <summary>포커스 양보 대기 타임아웃 (초). 기본 15.</summary>
    public int UserYieldTimeoutSeconds { get; init; } = 15;

    /// <summary>true면 유저 양보 확인 오버레이 스킵 -- 즉시 자동승인.
    /// 슬랙 요청 등 유저가 명시적으로 요청한 액션에서 사용.
    /// 돋보기/포커스확보는 정상 수행됨 -- 승인만 자동.</summary>
    public bool AutoApproveYield { get; init; }

    /// <summary>
    /// 선택: 입력위치 확보 콜백. yield 승인 후 Probe 마지막 단계에서 호출.
    /// 반환 true = 확보 성공, false = 실패 (report.InputPositionEnsured에 기록).
    /// 호출자가 클로저로 컨텍스트 캡처 (PromptInfo, rendererHwnd 등).
    /// 예: EnsureInputPosition = () => promptHelper.EnsureCaretInPrompt(prompt, renderer)
    /// </summary>
    public Func<bool>? EnsureInputPosition { get; init; }
}

// -- Report --------------------------------------------------------

/// <summary>
/// 입력위치확보 전수조사 결과.
/// </summary>
public record InputReadinessReport
{
    // 타겟 정보
    public IntPtr TargetHwnd { get; init; }
    public string? TargetClass { get; init; }
    public string? TargetAutomationId { get; init; }
    public string? TargetName { get; init; }
    public uint TargetPid { get; init; }
    public string? ProcessName { get; init; }

    // 사용 가능한 메서드 (focusless 우선 정렬)
    public List<InputMethod> Methods { get; init; } = new();

    // 편의 쿼리
    public IEnumerable<InputMethod> Available => Methods.Where(m => m.Available);
    public IEnumerable<InputMethod> Focusless => Available.Where(m => m.Focusless);

    // 방해꾼
    public BlockerInfo? ActiveBlocker { get; init; }

    // 권한
    public bool TargetElevated { get; init; }
    public bool WeAreElevated { get; init; }
    public bool ElevationMismatch => TargetElevated && !WeAreElevated;

    // 윈도우 상태
    public bool FormVisible { get; init; }
    public bool FormEnabled { get; init; }
    public bool FormIconic { get; init; }

    // 유저 입력 간섭 분석
    public uint UserIdleMs { get; init; }
    public bool UserInputRecent { get; init; }

    // 포커스 양보 대기 결과
    public bool UserYieldRequested { get; init; }
    public bool UserYieldConfirmed { get; init; }
    public bool UserYieldFocusAcquired { get; init; }

    // 종합 판정
    public bool Ready => Available.Any() && ActiveBlocker == null && !ElevationMismatch;
    public string? RecommendedMethod => Available.FirstOrDefault()?.Name;
    public bool RequiresFocus => Available.FirstOrDefault()?.Focusless == false;

    /// <summary>
    /// 포커스 사전 확보됨: yield 확인 + SetForegroundWindow 성공 검증 완료.
    /// true면 EnsureFocus 스킵하고 즉시 입력 디스패치 가능!
    /// Manual click = always true, auto-approve = verified via GetForegroundWindow.
    /// </summary>
    public bool FocusPreAcquired => UserYieldConfirmed && UserYieldFocusAcquired;

    /// <summary>
    /// 입력위치 확보 콜백 결과. EnsureInputPosition 콜백이 없으면 null.
    /// true = 캐럿 PromptRect 안에 확보됨, false = 확보 실패.
    /// </summary>
    public bool? InputPositionEnsured { get; init; }

    // 돋보기 세션 (호출자가 ShowPass/ShowFail 가능)
    public IActionZoom? Zoom { get; init; }
}

// -- Callback Interfaces ------------------------------------------

/// <summary>
/// 방해꾼 자동 dismiss 콜백. CLI 레이어에서 DialogHandlerManager 래핑.
/// </summary>
public interface IBlockerHandler
{
    (bool handled, bool shouldRetry) TryHandle(IntPtr mainHwnd, BlockerInfo blocker);
}

/// <summary>
/// 노하우 방송 콜백. (formId, action) 키로 1회만 방송 보장.
/// knowhow.md -> 파일명+첫문단, knowhow-{action}.md -> 파일명만.
/// </summary>
public interface IKnowhowBroadcaster
{
    void Broadcast(IntPtr mainHwnd, string? formId, int? controlId, string? actionName);
}

/// <summary>
/// 돋보기 팩토리 콜백. 이전 돋보기 재활용 가능.
/// </summary>
public interface IReadinessZoom
{
    IActionZoom? Begin(System.Drawing.Rectangle screenRect, IntPtr formHandle,
                       string action, string label);
}

/// <summary>
/// 유저 입력 간섭 대기 결과.
/// Approved: 진행 허가됨 (수동 클릭 or 자동승인).
/// FocusAcquired: SetForegroundWindow 성공 검증됨 (수동=보장, 자동=검증).
/// </summary>
public readonly record struct UserYieldResult(bool Approved, bool FocusAcquired);

/// <summary>
/// 권한 상승 요청 콜백. UIPI 차단 시 관리자 재시작 제안.
/// Tag: [READINESS]
/// </summary>
public interface IElevationRequester
{
    /// <summary>
    /// Target is elevated, wkappbot is not. Offer to relaunch as admin via UAC.
    /// Returns true if relaunched (process will exit -- caller should return).
    /// Returns false if UAC denied or skipped (continue with focusless methods).
    /// </summary>
    bool RequestElevation(string targetProcessName, uint targetPid);
}

/// <summary>
/// 유저 입력 간섭 대기 콜백. 포커스 확보 전 유저 확인 대기.
/// 포커스 방해 안하는 알림창 + 카운트다운 확인 버튼.
/// 타임아웃 시 자동승인 (유저 idle), 유저 활동 감지 시 30초 리셋.
/// Tag: [READINESS]
/// </summary>
public interface IUserInputWait
{
    /// <summary>
    /// Show non-focus-stealing alert and wait for user confirmation or auto-approve.
    /// Alert is owned by targetMainHwnd (stays above target app).
    /// positionHwnd: overlay positioned near this window (control or form). Falls back to targetMainHwnd.
    /// Auto-approves when user is idle; resets timer on mouse/keyboard activity.
    /// </summary>
    UserYieldResult WaitForUserYield(IntPtr targetMainHwnd, uint userIdleMs, int timeoutSeconds,
                                     IntPtr positionHwnd = default);
}

// -- Main Class ----------------------------------------------------

/// <summary>
/// 입력위치확보 공용 모듈.
/// 전수조사(Probe) + 방해꾼 감지(DetectBlocker) + 노하우 방송 + 돋보기.
///
/// Usage:
///   var ir = new InputReadiness { BlockerHandler = ..., KnowhowBroadcaster = ..., ZoomFactory = ... };
///   var report = ir.Probe(new InputReadinessRequest { TargetHwnd = hWnd, IntendedAction = "click" });
///   if (report.Ready) { /* 액션 수행 */ report.Zoom?.ShowPass("OK"); }
///
/// Tag: [READINESS]
/// </summary>
public sealed partial class InputReadiness
{
    public IBlockerHandler? BlockerHandler { get; set; }
    public IKnowhowBroadcaster? KnowhowBroadcaster { get; set; }
    public IReadinessZoom? ZoomFactory { get; set; }
    public IUserInputWait? UserInputWait { get; set; }
    public IElevationRequester? ElevationRequester { get; set; }

    /// <summary>
    /// <summary>Event fired after Probe() succeeds -- for auto a11y hack trigger.</summary>
    public static event Action<IntPtr, string, string>? OnProbeSuccess; // (targetHwnd, processName, className)

    /// Set to true when Probe() or ProbeAtPoint() is called for this invocation.
    /// SmartSetForegroundWindow and physical input entry points check this flag.
    /// Commands that skip readiness trigger [IDLE!] warning -- "no-readiness detected".
    /// </summary>
    // AsyncLocal: flows through async/await continuations even on thread-pool thread switches.
    // [ThreadStatic] would lose the flag after any 'await' that resumes on a different thread.
    private static readonly AsyncLocal<bool> _readinessCalled = new();
    public static bool ReadinessCalled
    {
        get => _readinessCalled.Value;
        set => _readinessCalled.Value = value;
    }

    /// <summary>
    /// [FOCUS-GUARD] CheckActiveGuard 그레이스 타임스탬프.
    ///
    /// Probe()에서 유저가 입력위치양보를 승인하는 순간 설정됨.
    /// 오버레이 "승인" 클릭 자체가 유저 마우스 입력이므로, 클릭 직후 idleMs ≈ 0ms가 됨.
    /// 이 타임스탬프가 없으면 CheckActiveGuard가 "유저 방금 활성!" 로 정당한 포커스 스틸을 차단함.
    ///
    /// 그레이스 3초: 승인->SmartSetForeground 사이에 유저가 다시 타이핑 시작하면 여전히 차단됨.
    /// -> Probe approved(T=0) -> user starts typing(T=1s) -> SmartSetForeground(T=5s) -> 차단 O
    /// -> Probe approved(T=0) -> SmartSetForeground(T=0.5s) -> 차단 X (정상 승인 후 즉시 포커스)
    /// </summary>
    internal static DateTime _lastYieldApprovedAt = DateTime.MinValue;

    /// <summary>Grant 60s approval grace -- bypasses CheckActiveGuard for autonomous automation commands.</summary>
    public static void SetApprovalGrace() => _lastYieldApprovedAt = DateTime.UtcNow;

    /// <summary>
    /// Call from physical input entry points (SendInput, SetCursorPos, SmartSetForegroundWindow).
    /// Throws if Probe()/ProbeAtPoint() was not called first -- forces all callers to set up readiness.
    /// "정식 입력확보 셋업절차 강제" -- no warn-only, no exceptions.
    /// </summary>
    public static void AssertReadiness(string caller)
    {
        if (ReadinessCalled) return;
        var idleMs = NativeMethods.GetUserIdleMs();
        var idleStr = idleMs >= 60000 ? $"{idleMs / 60000}m {idleMs / 1000 % 60}s"
                    : idleMs >= 1000  ? $"{idleMs / 1000.0:F1}s"
                    :                   $"{idleMs}ms";
        // -- 코딩 가이드 출력: 후배 클롣이 원인과 수정방법을 바로 알 수 있도록 --
        var msg = $"[NO-READINESS:{caller}] user input {idleStr} ago -- Probe() not called before {caller}!";
        Console.Error.WriteLine(msg);
        Console.Error.WriteLine($"  -> 이 함수({caller})는 유저 입력을 방해할 수 있는 focus-stealing 경로입니다.");
        Console.Error.WriteLine($"  -> 코딩 규칙: 포커스를 빼앗는 코드 앞에 반드시 InputReadiness.Probe() 호출 후 ReadinessCalled=true 설정!");
        Console.Error.WriteLine($"  -> 빠른 수정: 'ReadinessCalled = true;' 로 건너뛰는 것은 ✖ 금지. ProbeAndSubmit() 또는 Probe()를 호출할 것.");
        throw new InvalidOperationException(msg);
    }

    /// <summary>
    /// [FOCUS-GUARD] 유저 활성 중 포커스 스틸 차단 가드.
    ///
    /// -- 설계 의도 (후배 클롣 필독!) ----------------------------------------------
    /// 앱봇이 focus-stealing 함수(SmartSetForegroundWindow, SendInput 등)를 호출하기 전,
    /// "지금 유저가 타이핑/클릭 중인가?"를 확인해서 방해를 차단하는 가드.
    ///
    /// 문제의 시나리오:
    ///   T=0s  Probe() 실행 -> 유저 idle -> 자동 승인 (ReadinessCalled=true)
    ///   T=1s  유저가 타이핑 시작 (앱봇은 모름)
    ///   T=5s  SmartSetForegroundWindow 실행 -> 유저 타이핑 중인 창을 빼앗아 버림 <- BAD!
    ///
    /// 그레이스 예외 (_lastYieldApprovedAt):
    ///   유저가 오버레이에서 "승인" 클릭 -> 마우스 클릭 = 유저 입력 -> idleMs ≈ 0ms
    ///   타임스탬프 없으면 "승인 즉시 포커스 스틸"도 차단됨 -> 이건 잘못된 차단!
    ///   그래서 승인 후 3초 이내는 idleMs 무시하고 허용.
    ///
    /// Phase 2 예고:
    ///   차단 시 타겟 hwnd에 WKAppBot_FocusPending 프로퍼티 스탬프 -> 재진입 시 자동 팝업.
    ///   현재는 error 출력 후 false 반환으로 호출자가 abort 처리함.
    ///
    /// ----------------------------------------------------------------------------─
    /// ! 이 함수를 호출하지 않는 새 focus-stealing 코드를 만들면 안 됩니다!
    ///   포커스를 빼앗는 코드 -> 반드시 AssertReadiness() + CheckActiveGuard() 쌍으로 호출.
    /// ----------------------------------------------------------------------------─
    ///
    /// Returns true = safe to proceed, false = block (user is active, no approval obtained).
    /// Call from every focus-stealing entry point AFTER AssertReadiness.
    /// If ActiveGuardYieldCallback is set, shows yield popup synchronously and blocks main thread
    /// until user approves (or times out). Approved -> returns true + sets grace timestamp.
    /// </summary>
    public static bool CheckActiveGuard(string caller, int thresholdMs = 2000)
    {
        var idleMs = NativeMethods.GetUserIdleMs();
        if (idleMs >= thresholdMs) return true; // user idle long enough -- safe

        // -- 그레이스 예외: Probe 승인 직후 3초 이내는 허용 --
        // 오버레이 "승인" 클릭 자체가 마우스 입력(idleMs≈0) -> 이 예외 없으면 정당한 포커스 스틸도 차단됨.
        var graceSec = (DateTime.UtcNow - _lastYieldApprovedAt).TotalSeconds;
        if (graceSec < 60.0)
        {
            Console.WriteLine($"[FOCUS-GUARD:{caller}] grace {graceSec:F1}s since yield-approved -- allow");
            return true;
        }

        // -- 유저 활성 감지 -- 팝업으로 승인 대기 (동기) --
        var idleStr = idleMs >= 1000 ? $"{idleMs / 1000.0:F1}s" : $"{idleMs}ms";
        var kbFocusHwnd = NativeMethods.GetKeyboardFocusHwnd();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[FOCUS-GUARD:{caller}] 유저 활성 ({idleStr} ago) -- 포커스 스틸 전 승인 팝업");
        Console.ResetColor();

        // [PHASE 2] 팝업 콜백 있으면 동기 대기 -- 메인 스레드 블로킹 승인
        var cb = ActiveGuardYieldCallback;
        if (cb != null)
        {
            var targetHwnd = NativeMethods.GetForegroundWindow(); // 현재 포그라운드를 타겟으로
            var result = cb.WaitForUserYield(targetHwnd, userIdleMs: 0, timeoutSeconds: 30);
            if (result.Approved)
            {
                _lastYieldApprovedAt = DateTime.UtcNow; // approved → always set 60s grace
                Console.WriteLine($"[FOCUS-GUARD:{caller}] 승인됨 -> proceed");
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[FOCUS-GUARD:{caller}] ✖ 취소됨 -> abort");
            Console.ResetColor();
            return false;
        }

        // 콜백 없음 -- 에러 출력 후 차단
        Console.Error.WriteLine(
            $"[FOCUS-GUARD:{caller}] ✖ BLOCKED -- user input {idleStr} ago (threshold {thresholdMs}ms).");
        Console.Error.WriteLine(
            $"  -> 키보드 포커스: 0x{kbFocusHwnd:X8} (fg=0x{NativeMethods.GetForegroundWindow():X8})");
        Console.Error.WriteLine(
            $"  -> 입력위치확보(Probe) 후 유저가 다시 활성됨. 포커스 스틸 중단.");
        return false;
    }

    /// <summary>
    /// [FOCUS-GUARD] CheckActiveGuard 팝업 콜백 -- CLI 레이어에서 설정.
    /// 유저가 활성 중일 때 포커스 스틸 시도 시 동기 승인 팝업을 표시.
    /// null이면 에러 출력 후 차단만.
    /// </summary>
    public static IUserInputWait? ActiveGuardYieldCallback { get; set; }

    // -- Probe: 전수조사 (Probe partial) --
    // -- DetectBlocker / TryDismissBlocker (Blocker partial) --
    // -- ProbeAtPoint and helpers (PointProbe partial) --
    // -- ProbeUiaPatterns / ProbeWin32Messages / ProbeSendInput / GetTargetRect / BuildClassPath / PrintReport (Helpers partial) --

    /// <summary>Global dry-run flag (AsyncLocal, set by CLI ask/agent entry points).</summary>
    public static readonly System.Threading.AsyncLocal<bool> DryRunMode = new();
}
