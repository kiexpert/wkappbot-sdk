using System.Diagnostics;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// [FOCUS-GUARD] 외부 프로그램 실행 시 포커스 강탈 감시 및 차단 가드.
///
/// ── 설계 의도 (후배 클롣 필독!) ────────────────────────────────────────────────
/// 앱봇이 외부 프로그램(Process.Start)을 실행할 때, 해당 프로그램이 OS 포커스를
/// 빼앗을 수 있습니다. 이 가드는 실행 전후 포커스를 비교해서:
///   1. 강탈 감지 → 콘솔 경고 출력 + 포커스 복원
///   2. 강탈 기록 → _knownFocusStealers에 EXE 이름 저장
///   3. 재진입 시 경고 → 이미 강탈 전력이 있는 EXE는 사전에 알림
///
/// Phase 2 예고:
///   - 강탈 전력 EXE + ReadinessCalled=false → 차단 (현재는 warn-only)
///   - 강탈 방지 플래그 강제: CreateProcess SW_SHOWNOACTIVATE (8)
///   - 강탈 전력 목록 파일 영속화 (wkappbot.hq/profiles/focus_stealers.json)
///
/// ─────────────────────────────────────────────────────────────────────────────
/// ⚠ 외부 프로그램 실행 코드 → 반드시 Process.Start() 대신 GuardedStart() 사용!
/// ─────────────────────────────────────────────────────────────────────────────
/// </summary>
public static class ProcessLaunchGuard
{
    /// <summary>
    /// Set to true when this process IS the Eye daemon.
    /// Eye manages its own focus intentionally — skip all guard checks.
    /// Set once at Eye startup (AppBotEyeCommand).
    /// </summary>
    public static bool IsEyeProcess { get; set; }

    // 이미 포커스 강탈 전력이 확인된 EXE 이름 목록 (현재 세션 in-memory).
    // Phase 2에서 파일로 영속화 예정.
    private static readonly HashSet<string> _knownFocusStealers =
        new(StringComparer.OrdinalIgnoreCase);

    // 포커스 변화 감지 대기 시간 (ms). 프로그램 창이 생성되기까지 기다림.
    private const int FocusCheckDelayMs = 600;

    /// <summary>
    /// Process.Start() 대체 — 실행 전후 포커스 변화 감지 + 복원 + 강탈 기록.
    /// Eye 프로세스에서는 가드 스킵 (IsEyeProcess=true).
    /// </summary>
    /// <param name="psi">ProcessStartInfo</param>
    /// <param name="callerName">로그에 표시할 호출자 이름 (예: "ScenarioRunner")</param>
    public static Process? GuardedStart(ProcessStartInfo psi, string callerName = "")
    {
        // Eye daemon: skip guard entirely — Eye controls its own focus intentionally
        if (IsEyeProcess) return Process.Start(psi);

        var exeName = Path.GetFileName(psi.FileName) ?? psi.FileName;
        var tag = string.IsNullOrEmpty(callerName) ? exeName : $"{callerName}:{exeName}";

        // ── 강탈 전력 EXE 사전 경고 ──
        if (_knownFocusStealers.Contains(exeName))
        {
            Console.Error.WriteLine(
                $"[LAUNCH-GUARD:{tag}] ⚠ '{exeName}' 은 이전에 포커스 강탈이 확인된 프로그램입니다.");
            if (!InputReadiness.ReadinessCalled)
            {
                Console.Error.WriteLine(
                    $"  → 입력위치확보(Probe()) 없이 실행합니다. 포커스 강탈 발생 시 자동 복원합니다.");
                Console.Error.WriteLine(
                    $"  → 코딩 가이드: 강탈 전력 프로그램 실행 전 InputReadiness.Probe() 호출 권장!");
            }
        }

        // ── 실행 전 포커스 스냅샷 ──
        var fgBefore = NativeMethods.GetForegroundWindow();

        // ── 프로세스 실행 ──
        Process? proc;
        try
        {
            proc = Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCH-GUARD:{tag}] Process.Start failed: {ex.Message}");
            return null;
        }

        if (proc == null)
        {
            Console.WriteLine($"[LAUNCH-GUARD:{tag}] Process.Start returned null (shell execute handled externally?)");
            return null;
        }

        // ── 창 생성 대기 후 포커스 변화 확인 ──
        Thread.Sleep(FocusCheckDelayMs);

        var fgAfter = NativeMethods.GetForegroundWindow();
        if (fgAfter != fgBefore && fgAfter != IntPtr.Zero)
        {
            // 포커스 강탈 감지!
            _knownFocusStealers.Add(exeName);
            var targetHwnd = fgAfter; // the launched window that stole focus

            Console.Error.WriteLine(
                $"[LAUNCH-GUARD:{tag}] ✖ '{exeName}' 이 포커스를 강탈했습니다!");
            Console.Error.WriteLine(
                $"  before=0x{fgBefore:X8}, after=0x{fgAfter:X8}");
            Console.Error.WriteLine(
                $"  → 포커스 강탈자로 등록. 포커스 복원 + 뒤배치 중...");

            // Restore: user fg back on top, launched window placed right behind it.
            // SetWindowPos(target, fgBefore, ...) positions target AFTER fgBefore in Z-order.
            // SWP_NOACTIVATE ensures no focus flip-flop during repositioning.
            if (fgBefore != IntPtr.Zero)
            {
                NativeMethods.SetForegroundWindow(fgBefore);
                PlaceBehind(targetHwnd, fgBefore, tag);
            }
        }
        else
        {
            // No focus steal — but still position launched window behind user fg
            // for consistency (focusless-first: launched app visible but not intrusive)
            if (proc.MainWindowHandle != IntPtr.Zero && fgBefore != IntPtr.Zero)
            {
                PlaceBehind(proc.MainWindowHandle, fgBefore, tag);
                Console.WriteLine($"[LAUNCH-GUARD:{tag}] '{exeName}' 포커스 변화 없음 ✓ (뒤배치 완료)");
            }
            else
            {
                Console.WriteLine($"[LAUNCH-GUARD:{tag}] '{exeName}' 포커스 변화 없음 ✓");
            }
        }

        return proc;
    }

    /// <summary>
    /// 강탈 전력 EXE 목록 조회 (디버그/프루브 출력용).
    /// </summary>
    public static IReadOnlyCollection<string> KnownFocusStealers => _knownFocusStealers;

    /// <summary>
    /// 강탈 전력 EXE 수동 등록 (YAML 시나리오 선언 또는 테스트용).
    /// </summary>
    public static void RegisterKnownStealer(string exeName) =>
        _knownFocusStealers.Add(exeName);

    /// <summary>
    /// Place a window right behind another in Z-order without activating.
    /// User keeps their focus; target is visible but non-intrusive.
    /// </summary>
    private static void PlaceBehind(IntPtr targetHwnd, IntPtr inFrontHwnd, string tag)
    {
        try
        {
            // SetWindowPos insertAfter=inFrontHwnd positions target BELOW inFrontHwnd in Z-order.
            NativeMethods.SetWindowPos(targetHwnd, inFrontHwnd,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
            Console.Error.WriteLine($"[LAUNCH-GUARD:{tag}] 유저 창 뒤에 배치 완료 (0x{targetHwnd:X8} behind 0x{inFrontHwnd:X8})");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCH-GUARD:{tag}] PlaceBehind failed: {ex.Message}");
        }
    }
}
