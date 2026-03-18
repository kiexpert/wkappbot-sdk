using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

// ── Enums & Records ───────────────────────────────────────────────

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

// ── Request ───────────────────────────────────────────────────────

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

    // ── 플래그 ──
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

    /// <summary>true면 유저 양보 확인 오버레이 스킵 — 즉시 자동승인.
    /// 슬랙 요청 등 유저가 명시적으로 요청한 액션에서 사용.
    /// 돋보기/포커스확보는 정상 수행됨 — 승인만 자동.</summary>
    public bool AutoApproveYield { get; init; }

    /// <summary>
    /// 선택: 입력위치 확보 콜백. yield 승인 후 Probe 마지막 단계에서 호출.
    /// 반환 true = 확보 성공, false = 실패 (report.InputPositionEnsured에 기록).
    /// 호출자가 클로저로 컨텍스트 캡처 (PromptInfo, rendererHwnd 등).
    /// 예: EnsureInputPosition = () => promptHelper.EnsureCaretInPrompt(prompt, renderer)
    /// </summary>
    public Func<bool>? EnsureInputPosition { get; init; }
}

// ── Report ────────────────────────────────────────────────────────

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

// ── Callback Interfaces ──────────────────────────────────────────

/// <summary>
/// 방해꾼 자동 dismiss 콜백. CLI 레이어에서 DialogHandlerManager 래핑.
/// </summary>
public interface IBlockerHandler
{
    (bool handled, bool shouldRetry) TryHandle(IntPtr mainHwnd, BlockerInfo blocker);
}

/// <summary>
/// 노하우 방송 콜백. (formId, action) 키로 1회만 방송 보장.
/// knowhow.md → 파일명+첫문단, knowhow-{action}.md → 파일명만.
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
    /// Returns true if relaunched (process will exit — caller should return).
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

// ── Main Class ────────────────────────────────────────────────────

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
public sealed class InputReadiness
{
    public IBlockerHandler? BlockerHandler { get; set; }
    public IKnowhowBroadcaster? KnowhowBroadcaster { get; set; }
    public IReadinessZoom? ZoomFactory { get; set; }
    public IUserInputWait? UserInputWait { get; set; }
    public IElevationRequester? ElevationRequester { get; set; }

    /// <summary>
    /// Set to true when Probe() or ProbeAtPoint() is called for this invocation.
    /// SmartSetForegroundWindow and physical input entry points check this flag.
    /// Commands that skip readiness trigger [IDLE⚠] warning — "no-readiness detected".
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
    /// 그레이스 3초: 승인→SmartSetForeground 사이에 유저가 다시 타이핑 시작하면 여전히 차단됨.
    /// → Probe approved(T=0) → user starts typing(T=1s) → SmartSetForeground(T=5s) → 차단 O
    /// → Probe approved(T=0) → SmartSetForeground(T=0.5s) → 차단 X (정상 승인 후 즉시 포커스)
    /// </summary>
    internal static DateTime _lastYieldApprovedAt = DateTime.MinValue;

    /// <summary>
    /// Call from physical input entry points (SendInput, SetCursorPos, SmartSetForegroundWindow).
    /// Throws if Probe()/ProbeAtPoint() was not called first — forces all callers to set up readiness.
    /// "정식 입력확보 셋업절차 강제" — no warn-only, no exceptions.
    /// </summary>
    public static void AssertReadiness(string caller)
    {
        if (ReadinessCalled) return;
        var idleMs = NativeMethods.GetUserIdleMs();
        var idleStr = idleMs >= 60000 ? $"{idleMs / 60000}m {idleMs / 1000 % 60}s"
                    : idleMs >= 1000  ? $"{idleMs / 1000.0:F1}s"
                    :                   $"{idleMs}ms";
        // ── 코딩 가이드 출력: 후배 클롣이 원인과 수정방법을 바로 알 수 있도록 ──
        var msg = $"[NO-READINESS:{caller}] user input {idleStr} ago — Probe() not called before {caller}!";
        Console.Error.WriteLine(msg);
        Console.Error.WriteLine($"  → 이 함수({caller})는 유저 입력을 방해할 수 있는 focus-stealing 경로입니다.");
        Console.Error.WriteLine($"  → 코딩 규칙: 포커스를 빼앗는 코드 앞에 반드시 InputReadiness.Probe() 호출 후 ReadinessCalled=true 설정!");
        Console.Error.WriteLine($"  → 빠른 수정: 'ReadinessCalled = true;' 로 건너뛰는 것은 ✖ 금지. ProbeAndSubmit() 또는 Probe()를 호출할 것.");
        throw new InvalidOperationException(msg);
    }

    /// <summary>
    /// [FOCUS-GUARD] 유저 활성 중 포커스 스틸 차단 가드.
    ///
    /// ── 설계 의도 (후배 클롣 필독!) ──────────────────────────────────────────────
    /// 앱봇이 focus-stealing 함수(SmartSetForegroundWindow, SendInput 등)를 호출하기 전,
    /// "지금 유저가 타이핑/클릭 중인가?"를 확인해서 방해를 차단하는 가드.
    ///
    /// 문제의 시나리오:
    ///   T=0s  Probe() 실행 → 유저 idle → 자동 승인 (ReadinessCalled=true)
    ///   T=1s  유저가 타이핑 시작 (앱봇은 모름)
    ///   T=5s  SmartSetForegroundWindow 실행 → 유저 타이핑 중인 창을 빼앗아 버림 ← BAD!
    ///
    /// 그레이스 예외 (_lastYieldApprovedAt):
    ///   유저가 오버레이에서 "승인" 클릭 → 마우스 클릭 = 유저 입력 → idleMs ≈ 0ms
    ///   타임스탬프 없으면 "승인 즉시 포커스 스틸"도 차단됨 → 이건 잘못된 차단!
    ///   그래서 승인 후 3초 이내는 idleMs 무시하고 허용.
    ///
    /// Phase 2 예고:
    ///   차단 시 타겟 hwnd에 WKAppBot_FocusPending 프로퍼티 스탬프 → 재진입 시 자동 팝업.
    ///   현재는 error 출력 후 false 반환으로 호출자가 abort 처리함.
    ///
    /// ─────────────────────────────────────────────────────────────────────────────
    /// ⚠ 이 함수를 호출하지 않는 새 focus-stealing 코드를 만들면 안 됩니다!
    ///   포커스를 빼앗는 코드 → 반드시 AssertReadiness() + CheckActiveGuard() 쌍으로 호출.
    /// ─────────────────────────────────────────────────────────────────────────────
    ///
    /// Returns true = safe to proceed, false = block (user is active, no approval obtained).
    /// Call from every focus-stealing entry point AFTER AssertReadiness.
    /// If ActiveGuardYieldCallback is set, shows yield popup synchronously and blocks main thread
    /// until user approves (or times out). Approved → returns true + sets grace timestamp.
    /// </summary>
    public static bool CheckActiveGuard(string caller, int thresholdMs = 2000)
    {
        var idleMs = NativeMethods.GetUserIdleMs();
        if (idleMs >= thresholdMs) return true; // user idle long enough — safe

        // ── 그레이스 예외: Probe 승인 직후 3초 이내는 허용 ──
        // 오버레이 "승인" 클릭 자체가 마우스 입력(idleMs≈0) → 이 예외 없으면 정당한 포커스 스틸도 차단됨.
        var graceSec = (DateTime.UtcNow - _lastYieldApprovedAt).TotalSeconds;
        if (graceSec < 3.0)
        {
            Console.WriteLine($"[FOCUS-GUARD:{caller}] grace {graceSec:F1}s since yield-approved — allow");
            return true;
        }

        // ── 유저 활성 감지 — 팝업으로 승인 대기 (동기) ──
        var idleStr = idleMs >= 1000 ? $"{idleMs / 1000.0:F1}s" : $"{idleMs}ms";
        var kbFocusHwnd = NativeMethods.GetKeyboardFocusHwnd();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[FOCUS-GUARD:{caller}] 유저 활성 ({idleStr} ago) — 포커스 스틸 전 승인 팝업");
        Console.ResetColor();

        // [PHASE 2] 팝업 콜백 있으면 동기 대기 — 메인 스레드 블로킹 승인
        var cb = ActiveGuardYieldCallback;
        if (cb != null)
        {
            var targetHwnd = NativeMethods.GetForegroundWindow(); // 현재 포그라운드를 타겟으로
            var result = cb.WaitForUserYield(targetHwnd, userIdleMs: 0, timeoutSeconds: 30);
            if (result.Approved)
            {
                if (result.FocusAcquired)
                    _lastYieldApprovedAt = DateTime.UtcNow; // 유저가 오버레이 클릭 → grace 부여
                Console.WriteLine($"[FOCUS-GUARD:{caller}] 승인됨 → proceed");
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[FOCUS-GUARD:{caller}] ✖ 취소됨 → abort");
            Console.ResetColor();
            return false;
        }

        // 콜백 없음 — 에러 출력 후 차단
        Console.Error.WriteLine(
            $"[FOCUS-GUARD:{caller}] ✖ BLOCKED — user input {idleStr} ago (threshold {thresholdMs}ms).");
        Console.Error.WriteLine(
            $"  → 키보드 포커스: 0x{kbFocusHwnd:X8} (fg=0x{NativeMethods.GetForegroundWindow():X8})");
        Console.Error.WriteLine(
            $"  → 입력위치확보(Probe) 후 유저가 다시 활성됨. 포커스 스틸 중단.");
        return false;
    }

    /// <summary>
    /// [FOCUS-GUARD] CheckActiveGuard 팝업 콜백 — CLI 레이어에서 설정.
    /// 유저가 활성 중일 때 포커스 스틸 시도 시 동기 승인 팝업을 표시.
    /// null이면 에러 출력 후 차단만.
    /// </summary>
    public static IUserInputWait? ActiveGuardYieldCallback { get; set; }

    // ── Probe: 전수조사 ──────────────────────────────────────────

    public InputReadinessReport Probe(InputReadinessRequest req)
    {
        ReadinessCalled = true;
        var swProbe = Stopwatch.StartNew();
        long msInit = 0, msElevation = 0, msZoom = 0, msUia = 0, msWin32 = 0, msSendInput = 0;
        long msBlocker = 0, msKnowhow = 0, msYield = 0;

        // MainHwnd 자동 유도: GetAncestor(GA_ROOT) → 진짜 최상위 윈도우로 방해꾼 감지 스코프 확장
        var swStep = Stopwatch.StartNew();
        var mainHwnd = req.MainHwnd != IntPtr.Zero
            ? req.MainHwnd
            : NativeMethods.GetAncestor(req.TargetHwnd, NativeMethods.GA_ROOT);
        if (mainHwnd == IntPtr.Zero) mainHwnd = req.TargetHwnd;
        var methods = new List<InputMethod>();
        BlockerInfo? blocker = null;
        IActionZoom? zoom = null;

        // ── 타겟 기본 정보 ──
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

        // ── 윈도우 상태 ──
        bool formVisible = NativeMethods.IsWindowVisible(mainHwnd);
        bool formEnabled = NativeMethods.IsWindowEnabled(mainHwnd);
        bool formIconic = NativeMethods.IsIconic(mainHwnd);

        // ── 권한 ──
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();
        bool targetElevated = NativeMethods.IsProcessElevated(targetPid) ?? true;
        bool elevationMismatch = targetElevated && !weAreElevated;
        swStep.Stop();
        msInit = swStep.ElapsedMilliseconds;

        // ── 권한 상승 요청 (UIPI 차단 방지) ──
        if (elevationMismatch && ElevationRequester != null && !req.QuickMode)
        {
            swStep.Restart();
            zoom?.UpdateStatus("관리자 권한 필요 — UAC 요청 중...");
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

        // ── 유저 입력 간섭 분석 ──
        uint idleMs = NativeMethods.GetUserIdleMs();
        bool userRecent = idleMs < req.UserIdleThresholdMs;

        // ── 돋보기 (QuickMode가 아닐 때만) ──
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
            // ── 1. UIA 패턴 전수조사 (focusless) ──
            swStep.Restart();
            if (req.UiaElement != null)
                ProbeUiaPatterns(req.UiaElement, req.IntendedAction, methods);
            swStep.Stop();
            msUia = swStep.ElapsedMilliseconds;

            // ── 2. Win32 메시지 (focusless) ──
            swStep.Restart();
            ProbeWin32Messages(targetClass, methods);
            swStep.Stop();
            msWin32 = swStep.ElapsedMilliseconds;

            // ── 3. SendInput (focus-needed) ──
            swStep.Restart();
            ProbeSendInput(targetElevated, weAreElevated, methods);
            swStep.Stop();
            msSendInput = swStep.ElapsedMilliseconds;
        }

        // ── 방해꾼 감지 ──
        swStep.Restart();
        if (!req.SkipBlockerCheck)
            blocker = DetectBlocker(mainHwnd);
        swStep.Stop();
        msBlocker = swStep.ElapsedMilliseconds;

        // ── 노하우 방송 ──
        swStep.Restart();
        if (!req.SkipKnowhow && !req.QuickMode)
            KnowhowBroadcaster?.Broadcast(mainHwnd, req.FormId, req.ControlId, req.IntendedAction);
        swStep.Stop();
        msKnowhow = swStep.ElapsedMilliseconds;

        // ── 유저 입력 간섭 대기 ──
        bool yieldRequested = false;
        bool yieldConfirmed = false;
        bool yieldFocusAcquired = false;
        // [FOCUS-GUARD] 오버레이 클릭 여부 — 유저가 실제로 클릭했을 때만 grace 부여.
        // 자동승인(AutoApproveYield, 3초 timeout)은 클릭 없으므로 grace 불필요.
        // 오버레이 클릭 → 마우스 입력 → idleMs≈0ms → CheckActiveGuard가 오탐 차단하는 문제 방지용.
        bool explicitUserClickApproved = false;

        // ── FocusStealer / MouseStealer prop check ──
        // ActionApi stamps these props when a nominally-focusless UIA action
        // steals keyboard focus OR moves the mouse cursor without user input.
        // Two separate props → both can coexist on same hwnd+action:
        //   "WKAppBot_FocusStealer-{action}" — foreground window stolen
        //   "WKAppBot_MouseStealer-{action}"  — cursor moved
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
                // → force yield on ANY subsequent action on this hwnd until cleared
                bool midInputStealer = NativeMethods.GetPropW(mainHwnd, $"{FocusStealerPrefix}midInput") != IntPtr.Zero;
                // Generic stamp (action-agnostic): set whenever ANY action stole focus
                // Catches cross-action mismatches (e.g., click stole → next probe is type)
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
                        $"  [READINESS] ⚠ {kinds.ToUpperInvariant()} STEALER{midTag}: '{req.IntendedAction}' " +
                        $"previously grabbed {kinds} on hwnd=0x{mainHwnd:X} → forcing yield popup");
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
                // ── 자동승인 즉시 (AutoApproveYield=true: Slack 프롬프트 전달 등 완전 자동화) ──
                // focusStealerFlagged=true 시 절대 묵살 금지 → 팝업 강제 표시
                yieldRequested = true;
                yieldConfirmed = true;
                yieldFocusAcquired = true;

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"  [READINESS] Target foreground + user idle {idleMs / 1000}s — silent auto-approve (AutoApproveYield)");
                Console.ResetColor();
            }
            else if (targetIsForeground && !userRecent && UserInputWait != null)
            {
                // ── 타겟 전경 + 장기 idle → 3초 팝업 (잘보이게, 졸다가 막을 수 있게) ──
                yieldRequested = true;
                zoom?.UpdateStatus($"유저 idle {idleMs / 1000}s — 3초 후 자동 진행...");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"  [READINESS] Target foreground + user idle {idleMs / 1000}s — 3s popup (cancellable)");
                Console.ResetColor();
                Console.Out.Flush();

                swStep.Restart();
                var yieldResult = UserInputWait.WaitForUserYield(mainHwnd, idleMs, timeoutSeconds: 3,
                    positionHwnd: mainHwnd);
                Console.WriteLine($"  [READINESS] 3s popup: approved={yieldResult.Approved} [{swStep.ElapsedMilliseconds}ms]");
                yieldConfirmed = yieldResult.Approved;
                yieldFocusAcquired = yieldResult.FocusAcquired || targetIsForeground;
            }
            else if (req.AutoApproveYield)
            {
                // ── 자동승인 즉시 포커스 확보 (키보드+마우스) — 갭 없이! ──
                yieldRequested = true;
                yieldConfirmed = true;

                if (mainHwnd != IntPtr.Zero)
                {
                    yieldFocusAcquired = NativeMethods.SmartSetForegroundWindow(mainHwnd);
                    Console.ForegroundColor = yieldFocusAcquired ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"  [READINESS] Auto-yield → focus={(yieldFocusAcquired ? "OK" : "FAIL")} (fg=0x{NativeMethods.GetForegroundWindow():X}, target=0x{mainHwnd:X})");
                    Console.ResetColor();
                }
            }
            else if (UserInputWait != null)
            {
                // ── 타겟이 비전경 → 무조건 팝업 (30초), 유저 활동 시에도 팝업 ──
                yieldRequested = true;
                int yieldTimeout = targetIsForeground
                    ? req.UserYieldTimeoutSeconds  // 전경+유저활동: 기존 타임아웃
                    : 30;                          // 비전경: 무조건 30초

                var reason = targetIsForeground
                    ? $"유저 입력 감지 ({idleMs}ms ago)"
                    : "타겟이 전경이 아님";
                zoom?.UpdateStatus($"{reason} — 확인 대기 중...");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [READINESS] {reason} — showing yield overlay ({yieldTimeout}s)...");
                Console.ResetColor();
                Console.Out.Flush();

                swStep.Restart();
                var yieldResult = UserInputWait.WaitForUserYield(mainHwnd, idleMs, yieldTimeout,
                    positionHwnd: req.TargetHwnd);
                swStep.Stop();
                msYield = swStep.ElapsedMilliseconds;
                yieldConfirmed = yieldResult.Approved;
                yieldFocusAcquired = yieldResult.FocusAcquired;
                // 유저가 오버레이를 직접 클릭해서 포커스를 pre-acquire한 경우에만 grace 부여
                if (yieldResult.FocusAcquired) explicitUserClickApproved = true;

                if (yieldConfirmed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(yieldFocusAcquired
                        ? "  [READINESS] User confirmed — focus pre-acquired"
                        : "  [READINESS] Auto-approved (user idle) — proceeding");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [READINESS] Yield safety timeout — proceeding anyway");
                    Console.ResetColor();
                }
            }

            // ── FocusStealer props 클리어 (유저 승인 후 재시도에서 반복 팝업 방지) ──
            if (yieldConfirmed && mainHwnd != IntPtr.Zero)
            {
                try { NativeMethods.RemovePropW(mainHwnd, "WKAppBot_FocusStealer-midInput"); } catch { }
                try { NativeMethods.RemovePropW(mainHwnd, "WKAppBot_FocusStealer"); } catch { }
            }

            // ── CheckActiveGuard 그레이스 타임스탬프 ──
            // [FOCUS-GUARD] 오직 유저가 오버레이를 직접 클릭한 경우에만 grace 부여!
            //
            // Grace가 필요한 이유:
            //   오버레이 클릭 → 마우스 입력 → idleMs≈0ms → CheckActiveGuard가 오탐 차단
            //   → 유저가 명시적으로 승인했는데 포커스 스틸이 막히는 문제
            //
            // Grace가 필요 없는 경우:
            //   AutoApproveYield (유저 클릭 없음) → idle timer 미리셋 → 오탐 없음
            //   3초 popup 자동 timeout → 유저 클릭 없음 → 오탐 없음
            //   → 이 경우 grace를 주면 유저가 타이핑 시작해도 포커스 스틸 허용되는 버그!
            if (explicitUserClickApproved)
                _lastYieldApprovedAt = DateTime.UtcNow;
        }

        // ── 입력위치 확보 콜백 (yield 승인 후) ──
        bool? inputPositionEnsured = null;
        if (req.EnsureInputPosition != null)
        {
            swStep.Restart();
            try
            {
                inputPositionEnsured = req.EnsureInputPosition();
                Console.WriteLine($"  [READINESS] EnsureInputPosition: {(inputPositionEnsured.Value ? "✓ secured" : "✗ failed")} [{swStep.ElapsedMilliseconds}ms]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [READINESS] EnsureInputPosition error: {ex.Message}");
                inputPositionEnsured = false;
            }
        }

        // ── 프로파일링 출력 ──
        swProbe.Stop();
        Console.WriteLine($"  [PROF:PROBE] init={msInit}ms zoom={msZoom}ms uia={msUia}ms win32={msWin32}ms send={msSendInput}ms blocker={msBlocker}ms knowhow={msKnowhow}ms yield={msYield}ms TOTAL={swProbe.ElapsedMilliseconds}ms");
        Console.Out.Flush();

        // ── 돋보기 상태 업데이트 ──
        if (zoom != null)
        {
            if (yieldRequested && !yieldConfirmed)
                zoom.UpdateStatus("안전 타임아웃 — EnsureFocus 진행");
            else if (blocker != null)
                zoom.UpdateStatus($"BLOCKED: {blocker.Title}");
            else if (methods.Any(m => m.Available))
                zoom.UpdateStatus($"Ready: {methods.First(m => m.Available).Name}");
            else
                zoom.UpdateStatus("No methods available");
        }

        return new InputReadinessReport
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
    }

    // ── DetectBlocker: 방해꾼 빠른 감지 (~5ms) ────────────────────

    public BlockerInfo? DetectBlocker(IntPtr mainHwnd)
    {
        IntPtr blockerHwnd = IntPtr.Zero;

        NativeMethods.GetWindowThreadProcessId(mainHwnd, out uint targetPid);
        uint myPid = (uint)Environment.ProcessId;

        // Strategy 1: 전경 윈도우 체크 — 같은 프로세스의 팝업/다이얼로그만 blocker
        // 같은 클래스의 최상위 윈도우(예: VS Code 다중창)는 blocker가 아님!
        var fg = NativeMethods.GetForegroundWindow();
        if (fg != mainHwnd && fg != IntPtr.Zero)
        {
            NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
            if (fgPid != myPid && fgPid == targetPid)
            {
                var fgClsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(fg, fgClsSb, 256);
                var mainClsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(mainHwnd, mainClsSb, 256);
                var fgCls = fgClsSb.ToString();
                var mainCls = mainClsSb.ToString();

                // 같은 최상위 클래스(Chrome_WidgetWin_1 등) → 형제 윈도우, blocker 아님
                bool isSiblingWindow = fgCls == mainCls
                    && NativeMethods.GetAncestor(fg, NativeMethods.GA_ROOT) == fg;
                // mainHwnd의 자손(child/grandchild) → 편집 영역 등, blocker 아님
                bool isDescendant = NativeMethods.GetAncestor(fg, NativeMethods.GA_ROOT) == mainHwnd
                    || NativeMethods.IsChild(mainHwnd, fg);
                if (!isSiblingWindow && !isDescendant)
                    blockerHwnd = fg;
            }
        }

        // Strategy 2: EnumWindows — 같은 프로세스의 팝업/다이얼로그 탐색
        if (blockerHwnd == IntPtr.Zero)
        {
            var candidates = new List<IntPtr>();
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (hWnd == mainHwnd) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != targetPid) return true;

                var clsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(hWnd, clsSb, 256);
                var cls = clsSb.ToString();

                NativeMethods.GetWindowRect(hWnd, out var r);
                bool isDialog = cls == "#32770";
                bool isPopup = r.Width > 50 && r.Height > 30 && r.Width < 800 && r.Height < 600;

                if (isDialog || isPopup)
                    candidates.Add(hWnd);

                return true;
            }, IntPtr.Zero);

            if (candidates.Count > 0)
                blockerHwnd = candidates[0];
        }

        if (blockerHwnd == IntPtr.Zero)
            return null;

        // 방해꾼 정보 수집
        var titleSb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(blockerHwnd, titleSb, 256);

        var classSb2 = new StringBuilder(256);
        NativeMethods.GetClassNameW(blockerHwnd, classSb2, 256);

        var classPath = BuildClassPath(blockerHwnd);

        NativeMethods.GetWindowThreadProcessId(blockerHwnd, out uint blockerPid);
        string procName = "";
        try { procName = Process.GetProcessById((int)blockerPid).ProcessName; } catch { }

        // Static 컨트롤에서 메시지 텍스트 읽기 (간단 버전)
        string? messageText = ReadStaticText(blockerHwnd);

        return new BlockerInfo(
            Handle: blockerHwnd,
            ClassName: classSb2.ToString(),
            ClassPath: classPath,
            Title: titleSb.ToString(),
            MessageText: messageText,
            ProcessId: blockerPid,
            ProcessName: procName
        );
    }

    // ── TryDismissBlocker: IBlockerHandler 위임 ──────────────────

    public (bool handled, bool shouldRetry) TryDismissBlocker(IntPtr mainHwnd, BlockerInfo blocker)
    {
        if (BlockerHandler == null)
            return (false, false);
        return BlockerHandler.TryHandle(mainHwnd, blocker);
    }

    // ── PrintReport: 콘솔 출력 ───────────────────────────────────

    public static void PrintReport(InputReadinessReport report)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[READINESS] ");
        Console.ResetColor();
        Console.Write($"Target: [{report.TargetClass}]");
        if (report.TargetAutomationId != null)
            Console.Write($" aid={report.TargetAutomationId}");
        if (report.TargetName != null)
            Console.Write($" \"{report.TargetName}\"");
        Console.Write($" (pid={report.TargetPid} {report.ProcessName})");
        Console.WriteLine();

        // 윈도우 상태
        if (!report.FormVisible || !report.FormEnabled || report.FormIconic)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  [STATE] ");
            if (!report.FormVisible) Console.Write("HIDDEN ");
            if (!report.FormEnabled) Console.Write("DISABLED ");
            if (report.FormIconic) Console.Write("MINIMIZED ");
            Console.ResetColor();
            Console.WriteLine();
        }

        // 권한
        if (report.ElevationMismatch)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ELEVATION] Target elevated, wkappbot is not! Run as admin.");
            Console.ResetColor();
        }

        // 유저 입력 간섭
        if (report.UserInputRecent)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [USER] Recent input detected ({report.UserIdleMs}ms ago) — focus steal may interfere");
            Console.ResetColor();
        }

        // 포커스 양보 결과
        if (report.UserYieldRequested)
        {
            Console.ForegroundColor = report.UserYieldConfirmed ? ConsoleColor.Green : ConsoleColor.Yellow;
            var msg = report.UserYieldConfirmed
                ? (report.UserYieldFocusAcquired ? "User confirmed focus yield" : "Auto-approved (user idle)")
                : "Yield safety timeout";
            Console.WriteLine($"  [YIELD] {msg}");
            Console.ResetColor();
        }

        // 메서드 목록
        if (report.Methods.Count > 0)
        {
            Console.Write("  [METHODS] ");
            foreach (var m in report.Methods)
            {
                if (m.Available)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"✓{m.Name}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"✗{m.Name}");
                }
                Console.Write(m.Focusless ? "(FL) " : "(FN) ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // 방해꾼
        if (report.ActiveBlocker != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [BLOCKER] [{report.ActiveBlocker.ClassPath}] \"{report.ActiveBlocker.Title}\"");
            if (report.ActiveBlocker.MessageText != null)
                Console.WriteLine($"            msg: {report.ActiveBlocker.MessageText}");
            Console.ResetColor();
        }

        // 종합 판정
        Console.ForegroundColor = report.Ready ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write($"  [VERDICT] {(report.Ready ? "READY" : "NOT READY")}");
        if (report.RecommendedMethod != null)
            Console.Write($" → {report.RecommendedMethod}");
        if (report.RequiresFocus)
            Console.Write(" (focus needed)");
        Console.ResetColor();
        Console.WriteLine();
    }

    // ── Private: UIA 패턴 전수조사 ───────────────────────────────

    private static void ProbeUiaPatterns(AutomationElement el, string? action, List<InputMethod> methods)
    {
        // Invoke (focusless click)
        bool invokeOk = SafePatternCheck(() => el.Patterns.Invoke.IsSupported);
        methods.Add(new InputMethod("UIA.Invoke", InputMethodCategory.UiaPattern, true,
            invokeOk, invokeOk ? null : "Pattern not supported"));

        // LegacyIAccessible DoDefaultAction (Electron Hyperlink fallback)
        bool legacyInvoke = false;
        string? legacyAction = null;
        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacyAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                legacyInvoke = !string.IsNullOrEmpty(legacyAction);
            }
        }
        catch { }
        methods.Add(new InputMethod("MSAA.DoDefaultAction", InputMethodCategory.MsaaAccessible, true,
            legacyInvoke, legacyInvoke ? null : "No default action"));

        // Value (focusless type)
        bool valueOk = SafePatternCheck(() => el.Patterns.Value.IsSupported);
        bool valueRo = false;
        if (valueOk)
        {
            try { valueRo = el.Patterns.Value.Pattern.IsReadOnly.Value; } catch { }
        }
        methods.Add(new InputMethod("UIA.Value", InputMethodCategory.UiaPattern, true,
            valueOk && !valueRo, valueOk ? (valueRo ? "Read-only" : null) : "Pattern not supported"));

        // Toggle (focusless toggle)
        bool toggleOk = SafePatternCheck(() => el.Patterns.Toggle.IsSupported);
        methods.Add(new InputMethod("UIA.Toggle", InputMethodCategory.UiaPattern, true,
            toggleOk, toggleOk ? null : "Pattern not supported"));

        // SelectionItem
        bool selectOk = SafePatternCheck(() => el.Patterns.SelectionItem.IsSupported);
        methods.Add(new InputMethod("UIA.Select", InputMethodCategory.UiaPattern, true,
            selectOk, selectOk ? null : "Pattern not supported"));

        // ExpandCollapse
        bool expandOk = SafePatternCheck(() => el.Patterns.ExpandCollapse.IsSupported);
        methods.Add(new InputMethod("UIA.ExpandCollapse", InputMethodCategory.UiaPattern, true,
            expandOk, expandOk ? null : "Pattern not supported"));

        // RangeValue
        bool rangeOk = SafePatternCheck(() => el.Patterns.RangeValue.IsSupported);
        methods.Add(new InputMethod("UIA.RangeValue", InputMethodCategory.UiaPattern, true,
            rangeOk, rangeOk ? null : "Pattern not supported"));

        // NOTE: Scroll 패턴은 GetSupportedPatterns() COM 오염 위험 → 개별 체크만
        bool scrollOk = SafePatternCheck(() => el.Patterns.Scroll.IsSupported);
        methods.Add(new InputMethod("UIA.Scroll", InputMethodCategory.UiaPattern, true,
            scrollOk, scrollOk ? null : "Pattern not supported"));
    }

    /// <summary>UIA 패턴 체크: COM 예외 방지용 개별 try-catch.</summary>
    private static bool SafePatternCheck(Func<bool> check)
    {
        try { return check(); } catch { return false; }
    }

    // ── Private: Win32 메시지 전수조사 ───────────────────────────

    private static void ProbeWin32Messages(string targetClass, List<InputMethod> methods)
    {
        bool isEdit = targetClass.Contains("Edit") || targetClass.Contains("EDIT");
        bool isButton = targetClass == "Button" || targetClass.Contains("Button");
        bool isMfc = targetClass.StartsWith("Afx") || targetClass.Contains("CMaskEdit");
        bool isCombo = targetClass.Contains("ComboBox") || targetClass.Contains("COMBOBOX");

        // PostMessage WM_CHAR (focusless for MFC/Edit)
        methods.Add(new InputMethod("PostMessage.WmChar", InputMethodCategory.Win32Message, true,
            isEdit || isMfc, (isEdit || isMfc) ? null : $"Class '{targetClass}' is not Edit/MFC"));

        // WM_SETTEXT (Edit controls)
        methods.Add(new InputMethod("Win32.WmSetText", InputMethodCategory.Win32Message, true,
            isEdit, isEdit ? null : "Not an Edit control"));

        // EM_REPLACESEL (Edit/RichEdit)
        methods.Add(new InputMethod("Win32.EmReplaceSel", InputMethodCategory.Win32Message, true,
            isEdit, isEdit ? null : "Not an Edit control"));

        // BM_CLICK (Button controls)
        methods.Add(new InputMethod("Win32.BmClick", InputMethodCategory.Win32Message, true,
            isButton, isButton ? null : "Not a Button control"));

        // WM_LBUTTON (any window, focusless PostMessage click)
        methods.Add(new InputMethod("PostMessage.WmLButton", InputMethodCategory.Win32Message, true,
            true, null)); // always possible to attempt
    }

    // ── Private: SendInput 전수조사 ─────────────────────────────

    private static void ProbeSendInput(bool targetElevated, bool weAreElevated, List<InputMethod> methods)
    {
        bool elevOk = !targetElevated || weAreElevated;
        bool focuslessBlocked = FocuslessGuard.Enabled;

        // Mouse click via SendInput
        methods.Add(new InputMethod("SendInput.Mouse", InputMethodCategory.SendInput, false,
            elevOk && !focuslessBlocked,
            !elevOk ? "Elevation mismatch" : focuslessBlocked ? "FocuslessGuard active" : null));

        // Keyboard via SendInput
        methods.Add(new InputMethod("SendInput.Keyboard", InputMethodCategory.SendInput, false,
            elevOk && !focuslessBlocked,
            !elevOk ? "Elevation mismatch" : focuslessBlocked ? "FocuslessGuard active" : null));
    }

    // ── Private: 액션별 포커스 필요 판단 ──────────────────────────

    /// <summary>
    /// 해당 액션에 대해 포커스리스 메서드가 없어서 포커스니드로 폴백해야 하는지 판단.
    /// 포커스리스 경로가 있으면 false → 알림창 불필요.
    /// </summary>
    private static bool NeedsFocusForAction(List<InputMethod> methods, string? action)
    {
        var act = action?.ToLowerInvariant();

        // 핫키/키입력은 항상 SendInput 필요 (포커스리스 대안 없음)
        if (act is "hotkey" or "key" or "press_key")
            return true;

        // input 명령: focusless 여부와 관계없이 항상 yield 팝업 로직 진입
        // (내부에서 전경+idle이면 자동승인, 비전경/유저활동이면 팝업)
        if (act is "input")
            return true;

        // 텍스트 입력: UIA.Value / WmChar / WmSetText / EmReplaceSel / MSAA 중 하나 필요
        if (act is "type" or "type_text")
            return !methods.Any(m => m.Available && m.Focusless
                && (m.Name.Contains("Value") || m.Name.Contains("WmChar")
                    || m.Name.Contains("WmSetText") || m.Name.Contains("EmReplaceSel")
                    || m.Name.Contains("Accessible")));

        // 그 외 (click, toggle, scroll 등): 포커스리스 메서드가 있으면 OK
        return !methods.Any(m => m.Available && m.Focusless);
    }

    // ── Private: 타겟 Rect ──────────────────────────────────────

    private static System.Drawing.Rectangle GetTargetRect(IntPtr hwnd, AutomationElement? uia)
    {
        // UIA BoundingRectangle 우선
        if (uia != null)
        {
            try
            {
                var rect = uia.Properties.BoundingRectangle.ValueOrDefault;
                if (rect.Width > 0 && rect.Height > 0)
                    return new System.Drawing.Rectangle(
                        (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            }
            catch { }
        }

        // Win32 GetWindowRect 폴백
        NativeMethods.GetWindowRect(hwnd, out var wr);
        return new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
    }

    // ── Private: ClassPath 빌드 ─────────────────────────────────

    private static string BuildClassPath(IntPtr hWnd)
    {
        var parts = new List<string>();
        var current = hWnd;
        for (int i = 0; i < 5 && current != IntPtr.Zero; i++)
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(current, sb, 256);
            var cls = sb.ToString();
            if (string.IsNullOrEmpty(cls)) break;
            parts.Add(cls);
            current = NativeMethods.GetParent(current);
        }
        parts.Reverse();
        return string.Join("/", parts);
    }

    // ── ProbeAtPoint: 좌표 기반 입력확보 ─────────────────────────

    /// <summary>
    /// 좌표 기반 입력확보: 해당 좌표에 겹쳐있는 윈도우 스택을 Z-order로 수집,
    /// 앞에서부터 포커스리스 클릭 시도, 방해꾼 dismiss, 타겟을 앞으로 보내기 등.
    /// 필수 인수: ScreenX, ScreenY, TargetHwnd — 나머지 자동 유도.
    /// Tag: [READINESS]
    /// </summary>
    public PointReadinessReport ProbeAtPoint(PointReadinessRequest req)
    {
        ReadinessCalled = true;
        var sw = Stopwatch.StartNew();

        // ── Step 0: 자동 유도 ──
        var mainHwnd = req.MainHwnd != IntPtr.Zero
            ? req.MainHwnd
            : NativeMethods.GetAncestor(req.TargetHwnd, NativeMethods.GA_ROOT);
        if (mainHwnd == IntPtr.Zero) mainHwnd = req.TargetHwnd;

        NativeMethods.GetWindowThreadProcessId(req.TargetHwnd, out uint targetPid);
        string procName = "";
        try { procName = Process.GetProcessById((int)targetPid).ProcessName; } catch { }

        var action = req.IntendedAction ?? "click";

        // ── Step 0.5: 미니마이즈 감지 ──
        bool isMinimized = NativeMethods.IsIconic(mainHwnd) || NativeMethods.IsIconic(req.TargetHwnd);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("[READINESS] ");
        Console.ResetColor();
        Console.Write($"ProbeAtPoint ({req.ScreenX},{req.ScreenY}) target=0x{req.TargetHwnd:X}");
        Console.Write($" main=0x{mainHwnd:X} pid={targetPid} {procName}");
        if (isMinimized) Console.Write(" [MINIMIZED]");
        Console.WriteLine();

        // ── Step 1: 윈도우 스택 수집 (Z-order) — 미니마이즈면 스킵 ──
        var windowStack = new List<WindowAtPoint>();
        var classification = PointClassification.TargetNotFound;

        if (isMinimized)
        {
            // 미니마이즈: Z-order 의미 없음 → 바로 포커스리스 경로
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
                if (w.IsTarget) Console.Write(" ←TARGET");
                if (w.IsBlocker) Console.Write(" ←BLOCKER");
                Console.WriteLine();
            }

            // ── Step 2: 분류 ──
            classification = ClassifyPoint(windowStack, req.TargetHwnd, targetPid, mainHwnd);
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("  [CLASS] ");
        Console.ResetColor();
        Console.WriteLine(classification.ToString());

        // ── Step 3: 포커스리스 클릭 ──
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
                // 미니마이즈: 순간 복원(NOACTIVATE) → UIA 클릭 → 재미니마이즈
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
                Console.Write(foregroundStolen ? "  [FL] ⚠ " : "  [FL] ");
                string offTag = isOffScreen ? (isMinimized ? "minimized " : "offscreen ") : "";
                Console.WriteLine($"Focusless {offTag}{(foregroundStolen ? "OK but fg stolen" : "OK")} → {resolvedDetail}");
                Console.ResetColor();
            }
        }

        // ── Step 4: 포커스리스 실패 → 물리클릭 경로 (Z-order 정리 필요) ──
        // off-screen이면 물리클릭 불가 → 포커스리스 실패 = 전체 실패
        if (!focuslessClicked && !isOffScreen)
        {
            switch (classification)
            {
                case PointClassification.TargetOnTop:
                    // 이미 최상단 → 물리클릭 준비 완료
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
                    // 다른 앱이 앞 → 타겟을 앞으로 (focusless: SetWindowPos NOACTIVATE)
                    BringTargetForward(req.TargetHwnd, mainHwnd);
                    pathCleared = true;
                    pathClearMethod = "brought_forward";
                    Thread.Sleep(200);
                    break;
            }
        }

        if (isOffScreen && !focuslessClicked)
            resolvedDetail = isMinimized
                ? "Target minimized — focusless failed, physical click impossible"
                : "Target off-screen — focusless failed, physical click impossible";

        bool needsPhysical = !focuslessClicked && !isOffScreen;
        bool ready = focuslessClicked || needsPhysical;

        // ── 결과 출력 ──
        Console.ForegroundColor = focuslessClicked ? ConsoleColor.Green
            : needsPhysical ? ConsoleColor.Yellow
            : ConsoleColor.Red;
        Console.Write("  [RESULT] ");
        Console.ResetColor();

        if (focuslessClicked)
            Console.WriteLine($"FocuslessClick → {resolvedDetail} ({sw.ElapsedMilliseconds}ms)");
        else if (needsPhysical)
            Console.WriteLine($"NeedsPhysicalClick → {resolvedDetail ?? "fallback"} ({sw.ElapsedMilliseconds}ms)");
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

    // ── Private: 윈도우 스택 수집 ────────────────────────────────

    private List<WindowAtPoint> CollectWindowStack(int screenX, int screenY, IntPtr targetHwnd, uint targetPid)
    {
        var stack = new List<WindowAtPoint>();
        int zOrder = 0;

        // WindowFromPoint → 최상단 leaf 윈도우
        var topLeaf = NativeMethods.WindowFromPoint(new POINT { X = screenX, Y = screenY });
        var topRoot = topLeaf != IntPtr.Zero
            ? NativeMethods.GetAncestor(topLeaf, NativeMethods.GA_ROOT)
            : IntPtr.Zero;

        // EnumWindows: Z-order 순 (앞→뒤) — 해당 좌표를 포함하는 visible top-level 윈도우
        // 우리 프로세스(앱봇) 윈도우는 제외 (돋보기/Eye/경고팝업 등)
        var myPid = (uint)Environment.ProcessId;
        var collected = new List<(IntPtr handle, int z)>();
        int enumZ = 0;
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint wPid);
            if (wPid == myPid) return true; // skip our own windows (zoom/eye/warning)
            NativeMethods.GetWindowRect(hWnd, out var r);
            if (screenX >= r.Left && screenX <= r.Right && screenY >= r.Top && screenY <= r.Bottom)
            {
                collected.Add((hWnd, enumZ++));
            }
            return true;
        }, IntPtr.Zero);

        // WindowFromPoint의 루트가 EnumWindows 결과에 없으면 추가 (자식 윈도우인 경우)
        // 단, 우리 프로세스 윈도우는 제외
        if (topRoot != IntPtr.Zero && !collected.Any(c => c.handle == topRoot))
        {
            NativeMethods.GetWindowThreadProcessId(topRoot, out uint topPid);
            if (topPid != myPid)
                collected.Insert(0, (topRoot, -1));
        }

        foreach (var (hWnd, z) in collected)
        {
            var clsSb = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, clsSb, 256);
            var cls = clsSb.ToString();

            var titleSb = new StringBuilder(256);
            NativeMethods.GetWindowTextW(hWnd, titleSb, 256);
            var title = titleSb.ToString();

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);

            // IsTarget: 타겟 자체, 또는 타겟의 조상/자손
            bool isTarget = IsTargetOrRelated(hWnd, targetHwnd);

            // IsBlocker: 같은 PID, #32770 팝업 또는 작은 다이얼로그
            NativeMethods.GetWindowRect(hWnd, out var rect);
            bool isBlocker = !isTarget && pid == targetPid
                && (cls == "#32770" || (rect.Width > 50 && rect.Height > 30 && rect.Width < 800 && rect.Height < 600));

            stack.Add(new WindowAtPoint(
                Handle: hWnd,
                ClassName: cls,
                Title: string.IsNullOrEmpty(title) ? null : title,
                ProcessId: pid,
                IsTarget: isTarget,
                IsBlocker: isBlocker,
                ZOrder: zOrder++
            ));
        }

        return stack;
    }

    /// <summary>hWnd가 targetHwnd 자체이거나, 부모/자손 관계인지 체크.</summary>
    private static bool IsTargetOrRelated(IntPtr hWnd, IntPtr targetHwnd)
    {
        if (hWnd == targetHwnd) return true;

        // hWnd가 target의 조상?
        var walk = targetHwnd;
        for (int i = 0; i < 10 && walk != IntPtr.Zero; i++)
        {
            walk = NativeMethods.GetParent(walk);
            if (walk == hWnd) return true;
        }

        // target이 hWnd의 조상?
        walk = hWnd;
        for (int i = 0; i < 10 && walk != IntPtr.Zero; i++)
        {
            walk = NativeMethods.GetParent(walk);
            if (walk == targetHwnd) return true;
        }

        return false;
    }

    // ── Private: 분류 ────────────────────────────────────────────

    private static PointClassification ClassifyPoint(
        List<WindowAtPoint> stack, IntPtr targetHwnd, uint targetPid, IntPtr mainHwnd)
    {
        if (stack.Count == 0)
            return PointClassification.TargetNotFound;

        var top = stack[0];

        if (top.IsTarget)
            return PointClassification.TargetOnTop;

        if (top.IsBlocker)
            return PointClassification.BlockerOnTop;

        if (top.ProcessId == targetPid)
            return PointClassification.SiblingOnTop;

        // 다른 프로세스 → ForeignOnTop, but check if target even exists in stack
        bool targetInStack = stack.Any(w => w.IsTarget);
        if (!targetInStack)
            return PointClassification.TargetNotFound;

        return PointClassification.ForeignOnTop;
    }

    // ── Private: 포커스리스 클릭 시도 ──────────────────────────────

    /// <summary>
    /// UiaLocator.TryFocuslessClickAtPoint 래핑.
    /// 글로벌 윈도우 Z-order는 절대 안 건드림 (자식 윈도우만 조정 가능).
    /// MFC Invoke 부작용(메인창 튀어오름) 감지 → 즉시 복원(prevFg=#1, 도둑=#2) + fgStolen=true.
    /// </summary>
    private static (bool ok, string? detail, bool fgStolen) TryFocuslessAtPoint(
        int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            var prevFg = NativeMethods.GetForegroundWindow();

            using var uia = new UiaLocator();
            var (ok, detail) = uia.TryFocuslessClickAtPoint(screenX, screenY, hWnd);

            // 전경 변경 감지: invoke 전후 전경 비교
            // MFC Invoke 부작용 → 타겟 앱이 SetForegroundWindow 호출 → 전경 도둑질
            bool fgStolen = false;
            if (ok && prevFg != IntPtr.Zero)
            {
                Thread.Sleep(50); // MFC 내부 SetForegroundWindow 전파 대기
                var nowFg = NativeMethods.GetForegroundWindow();
                if (nowFg != prevFg && nowFg != IntPtr.Zero)
                {
                    fgStolen = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  [FL] ⚠ Foreground stolen! 0x{prevFg:X} → 0x{nowFg:X}");

                    // 즉시 복원: 원래 전경(prevFg)을 다시 전경으로! → 도둑(nowFg)은 자동으로 2등
                    // Raw restore (no guard) — we're undoing a steal, not acquiring focus
                    NativeMethods.SetForegroundWindowRaw(prevFg);
                    Console.WriteLine($"  [FL] ⚠ Restored: 0x{prevFg:X}→fg, 0x{nowFg:X}→#2");
                    Console.ResetColor();
                }
            }

            return (ok, detail, fgStolen);
        }
        catch (Exception ex)
        {
            return (false, $"UIA error: {ex.Message}", false);
        }
    }

    // ── Private: 미니마이즈 UIA 클릭 (2단계) ──────────────────

    /// <summary>
    /// 미니마이즈 윈도우에서 UIA 포커스리스 클릭. 2단계 전략:
    /// 1단계: BoundingRect로 정확 매칭 (리스토어 없이) — 좌표에 맞는 요소 찾으면 바로 Invoke
    /// 2단계: BoundingRect 없으면 포커스리스 리스토어 — SetWindowPlacement(SHOWNOACTIVATE) → UIA 클릭 → 재미니마이즈
    /// </summary>
    private static (bool ok, string? detail, bool fgStolen) TryFocuslessOnMinimized(
        int screenX, int screenY, IntPtr hWnd, IntPtr mainHwnd)
    {
        try
        {
            // rcNormalPosition → 상대좌표 계산
            var wp = new NativeMethods.WINDOWPLACEMENT
            {
                length = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.WINDOWPLACEMENT>()
            };
            NativeMethods.GetWindowPlacement(mainHwnd, ref wp);
            var normalRect = wp.rcNormalPosition;

            NativeMethods.GetWindowRect(hWnd, out var wRect);
            int relX = screenX - wRect.Left;
            int relY = screenY - wRect.Top;
            int targetX = normalRect.Left + relX;
            int targetY = normalRect.Top + relY;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  [MIN-FL] ");
            Console.ResetColor();
            Console.Write($"rcNormal=({normalRect.Left},{normalRect.Top} {normalRect.Width}x{normalRect.Height})");
            Console.Write($" rel=({relX},{relY})");

            // ── 1단계: BoundingRect로 정확 매칭 (리스토어 없이) ──
            var prevFg = NativeMethods.GetForegroundWindow();
            using var uia = new UiaLocator();
            var candidates = uia.FindInvocableDescendants(hWnd, targetX, targetY, normalRect);

            // BoundingRect이 있고 가까운 요소가 있는지 체크
            var exactMatch = candidates.FirstOrDefault(c => c.HasBounds && c.Distance < 200);
            if (exactMatch != null)
            {
                Console.Write($" → exact [{exactMatch.ControlType}]");
                if (!string.IsNullOrEmpty(exactMatch.AutomationId))
                    Console.Write($" aid={exactMatch.AutomationId}");
                Console.Write($" dist={exactMatch.Distance:F0}");

                bool ok1 = uia.TryInvokeElement(exactMatch.Element);
                if (ok1)
                {
                    string desc1 = FormatElementDesc(exactMatch);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" → Invoke OK (no restore)");
                    Console.ResetColor();
                    var (_, _, fgStolen1) = DetectAndRestoreForeground(prevFg);
                    return (true, $"{desc1} (Invoke, focusless, no restore)", fgStolen1);
                }
            }

            // ── 2단계: 포커스리스 리스토어 ──
            Console.Write(" → no exact match, restoring...");

            // 리매핑: rcNormalPosition 기준 + 클램핑
            int probeX = Math.Clamp(targetX, normalRect.Left, normalRect.Right - 1);
            int probeY = Math.Clamp(targetY, normalRect.Top, normalRect.Bottom - 1);

            // 순간 복원: SW_SHOWNOACTIVATE — 포커스 안 뺏고 원래 위치에 표시
            wp.showCmd = NativeMethods.SW_SHOWNOACTIVATE;
            NativeMethods.SetWindowPlacement(mainHwnd, ref wp);
            Thread.Sleep(100); // UIA BoundingRectangle 업데이트 대기

            Console.Write($" probe=({probeX},{probeY})");

            // UIA 포커스리스 클릭 (복원 상태)
            var (ok2, detail2) = uia.TryFocuslessClickAtPoint(probeX, probeY, hWnd);

            // 즉시 재미니마이즈: SW_SHOWMINNOACTIVE
            wp.showCmd = NativeMethods.SW_SHOWMINNOACTIVE;
            NativeMethods.SetWindowPlacement(mainHwnd, ref wp);

            if (ok2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" → {detail2} (restored)");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" → FAIL: {detail2}");
                Console.ResetColor();
                return (false, $"Minimized restore: {detail2}", false);
            }

            var (_, _, fgStolen2) = DetectAndRestoreForeground(prevFg);
            return (true, $"{detail2} (focusless, restored)", fgStolen2);
        }
        catch (Exception ex)
        {
            return (false, $"Minimized UIA error: {ex.Message}", false);
        }
    }

    /// <summary>전경 도둑질 감지 → SetForegroundWindow로 즉시 복원.</summary>
    private static (bool detected, IntPtr thief, bool restored) DetectAndRestoreForeground(IntPtr expectedFg)
    {
        if (expectedFg == IntPtr.Zero) return (false, IntPtr.Zero, false);
        Thread.Sleep(50);
        var nowFg = NativeMethods.GetForegroundWindow();
        if (nowFg == expectedFg || nowFg == IntPtr.Zero) return (false, IntPtr.Zero, false);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [FL] ⚠ Foreground stolen! 0x{expectedFg:X} → 0x{nowFg:X}");
        NativeMethods.SetForegroundWindowRaw(expectedFg); // raw restore — undoing steal
        Console.WriteLine($"  [FL] ⚠ Restored: 0x{expectedFg:X}→fg, 0x{nowFg:X}→#2");
        Console.ResetColor();
        return (true, nowFg, true);
    }

    /// <summary>InvocableCandidate → "[ControlType] Name aid=X" 형식.</summary>
    private static string FormatElementDesc(UiaLocator.InvocableCandidate c)
    {
        string desc = $"[{c.ControlType}]";
        if (!string.IsNullOrEmpty(c.Name))
            desc += $" \"{(c.Name.Length > 30 ? c.Name[..30] : c.Name)}\"";
        if (!string.IsNullOrEmpty(c.AutomationId))
            desc += $" aid={c.AutomationId}";
        return desc;
    }

    // ── Private: off-screen 좌표 리매핑 ──────────────────────────

    /// <summary>
    /// 미니마이즈 윈도우: GetWindowRect(아이콘 위치, 매우 작음) → UIA BoundingRectangle 기준으로
    /// 상대좌표 리매핑 + 클램핑. UIA BoundingRect이 비정상이면 원본 좌표 반환.
    /// </summary>
    private static (int probeX, int probeY) RemapToUiaBounds(int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            // 현재 GetWindowRect (아이콘 위치 = 매우 작은 rect)
            NativeMethods.GetWindowRect(hWnd, out var wRect);
            int relX = screenX - wRect.Left;
            int relY = screenY - wRect.Top;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  [REMAP] ");
            Console.ResetColor();
            Console.Write($"rel=({relX},{relY}) wRect=({wRect.Left},{wRect.Top} {wRect.Width}x{wRect.Height})");

            // UIA root의 BoundingRectangle (미니마이즈에서도 유효한 경우 있음)
            using var uia = new UiaLocator();
            var root = uia.Automation.FromHandle(hWnd);
            if (root == null)
            {
                Console.WriteLine(" → UIA root=null, using original");
                return (screenX, screenY);
            }

            var uiaRect = root.BoundingRectangle;
            Console.Write($" uiaRect=({uiaRect.Left},{uiaRect.Top} {uiaRect.Width}x{uiaRect.Height})");

            if (uiaRect.IsEmpty || uiaRect.Width <= 0 || uiaRect.Height <= 0)
            {
                Console.WriteLine(" → UIA rect empty, using original");
                return (screenX, screenY);
            }

            // UIA rect 기준 리매핑 + 클램핑
            int newX = Math.Clamp(uiaRect.Left + relX, uiaRect.Left, uiaRect.Right - 1);
            int newY = Math.Clamp(uiaRect.Top + relY, uiaRect.Top, uiaRect.Bottom - 1);

            bool clamped = newX != uiaRect.Left + relX || newY != uiaRect.Top + relY;
            bool remapped = newX != screenX || newY != screenY;

            Console.Write($" → probe=({newX},{newY})");
            if (clamped) Console.Write(" [CLAMPED]");
            if (!remapped) Console.Write(" [SAME]");
            Console.WriteLine();

            return (newX, newY);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [REMAP] Failed: {ex.Message} — using original coords");
            Console.ResetColor();
            return (screenX, screenY);
        }
    }

    // ── Private: 타겟을 앞으로 (Focusless) ──────────────────────

    private static void BringTargetForward(IntPtr targetHwnd, IntPtr mainHwnd)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  [PATH] BringForward: ");

        // MDI 자식이면 WM_MDIACTIVATE (focusless!)
        var parentClass = new StringBuilder(256);
        var parent = NativeMethods.GetParent(targetHwnd);
        if (parent != IntPtr.Zero)
        {
            NativeMethods.GetClassNameW(parent, parentClass, 256);
            if (parentClass.ToString() == "MDIClient")
            {
                Console.Write("WM_MDIACTIVATE ");
                NativeMethods.SendMessageW(parent, 0x0222 /*WM_MDIACTIVATE*/,
                    targetHwnd, IntPtr.Zero);
                Console.WriteLine("OK (focusless)");
                Console.ResetColor();
                return;
            }
        }

        // 일반: SetWindowPos HWND_TOP + NOACTIVATE (focusless!)
        Console.Write("SetWindowPos HWND_TOP ");
        NativeMethods.SetWindowPos(mainHwnd, NativeMethods.HWND_TOP,
            0, 0, 0, 0,
            NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
        Console.WriteLine("OK (focusless)");
        Console.ResetColor();
    }

    // ── Private: BlockerInfo 빌드 ────────────────────────────────

    private BlockerInfo? BuildBlockerInfo(IntPtr blockerHwnd)
    {
        if (blockerHwnd == IntPtr.Zero || !NativeMethods.IsWindow(blockerHwnd))
            return null;

        var titleSb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(blockerHwnd, titleSb, 256);

        var classSb = new StringBuilder(256);
        NativeMethods.GetClassNameW(blockerHwnd, classSb, 256);

        var classPath = BuildClassPath(blockerHwnd);

        NativeMethods.GetWindowThreadProcessId(blockerHwnd, out uint pid);
        string procName = "";
        try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }

        string? messageText = ReadStaticText(blockerHwnd);

        return new BlockerInfo(
            Handle: blockerHwnd,
            ClassName: classSb.ToString(),
            ClassPath: classPath,
            Title: titleSb.ToString(),
            MessageText: messageText,
            ProcessId: pid,
            ProcessName: procName
        );
    }

    // ── Private: Static 텍스트 읽기 (간단 버전) ─────────────────

    private static string? ReadStaticText(IntPtr hDialog)
    {
        var texts = new List<string>();
        NativeMethods.EnumChildWindows(hDialog, (hChild, _) =>
        {
            var sb = new StringBuilder(256);
            NativeMethods.GetClassNameW(hChild, sb, 256);
            if (sb.ToString() == "Static")
            {
                var textSb = new StringBuilder(512);
                NativeMethods.GetWindowTextW(hChild, textSb, 512);
                var t = textSb.ToString();
                if (!string.IsNullOrWhiteSpace(t))
                    texts.Add(t);
            }
            return true;
        }, IntPtr.Zero);

        return texts.Count > 0 ? string.Join(" | ", texts) : null;
    }

    // ── PrintPointReport: 좌표 기반 결과 콘솔 출력 ──────────────

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

// ── Point Readiness Types ────────────────────────────────────────

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

    /// <summary>선택: 메인 윈도우 핸들. 미지정 → GetAncestor(GA_ROOT) 자동.</summary>
    public IntPtr MainHwnd { get; init; }

    /// <summary>선택: 예정 액션. 미지정 → "click".</summary>
    public string? IntendedAction { get; init; }

    /// <summary>선택: 포커스리스만 허용 (--fl 플래그).</summary>
    public bool FocuslessOnly { get; init; }
}

/// <summary>좌표 기반 입력확보 결과.</summary>
public record PointReadinessReport
{
    /// <summary>해당 좌표의 윈도우 스택 (Z-order, 앞→뒤).</summary>
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
    /// MFC Invoke 부작용: BN_CLICKED → 내부 SetForegroundWindow → 타겟이 전경으로.
    /// 이 플래그가 true면 호출자가 경고 알림을 표시해야 함.
    /// </summary>
    public bool ForegroundStolen { get; init; }

    /// <summary>클릭 가능 상태.</summary>
    public bool Ready { get; init; }
    /// <summary>포커스리스 실패 → 물리클릭 필요.</summary>
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
    /// <summary>타겟이 최상단 → 바로 클릭.</summary>
    TargetOnTop,
    /// <summary>방해꾼이 최상단 → dismiss 후 재시도.</summary>
    BlockerOnTop,
    /// <summary>같은 앱의 다른 창이 최상단 → 타겟을 앞으로.</summary>
    SiblingOnTop,
    /// <summary>다른 앱 창이 최상단.</summary>
    ForeignOnTop,
    /// <summary>타겟이 스택에 없음 (좌표 오류).</summary>
    TargetNotFound,
    /// <summary>타겟이 미니마이즈됨 → Z-order 무관, UIA 핸들 기반 포커스리스.</summary>
    TargetMinimized
}
