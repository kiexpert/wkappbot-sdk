using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// ── BlockerHandlerAdapter ─────────────────────────────────────────

/// <summary>
/// IBlockerHandler 구현: 기존 TryHandleBlocker + DialogHandlerManager 래핑.
/// Tag: [READINESS]
/// </summary>
internal sealed class BlockerHandlerAdapter : IBlockerHandler
{
    private readonly DialogHandlerManager? _handlerMgr;

    public BlockerHandlerAdapter(DialogHandlerManager? handlerMgr)
    {
        _handlerMgr = handlerMgr;
    }

    public (bool handled, bool shouldRetry) TryHandle(IntPtr mainHwnd, BlockerInfo blocker)
    {
        // Delegate to Program.TryHandleBlocker (partial class)
        return Program.TryHandleBlockerViaReadiness(mainHwnd, _handlerMgr);
    }
}

// ── KnowhowBroadcasterAdapter ─────────────────────────────────────

/// <summary>
/// IKnowhowBroadcaster 구현: 1회만 방송 보장.
/// knowhow.md → 파일명+첫문단, knowhow-{action}.md → 파일명만.
/// Tag: [READINESS]
/// </summary>
internal sealed class KnowhowBroadcasterAdapter : IKnowhowBroadcaster
{
    private readonly HashSet<string> _broadcastedKeys = new();

    public void Broadcast(IntPtr mainHwnd, string? formId, int? controlId, string? actionName)
    {
        var key = $"{mainHwnd:X}|{formId}|{controlId}|{actionName}";
        if (!_broadcastedKeys.Add(key))
            return; // 이미 방송함 — 1회만!

        Program.BroadcastKnowhowViaReadiness(mainHwnd, formId, actionName);
    }
}

// ── UserInputWaitAdapter ─────────────────────────────────────────

/// <summary>
/// IUserInputWait 구현: WPF 알림창으로 포커스 양보 대기.
/// 소유자를 타겟 윈도우로 설정, 카운트다운 확인 버튼.
/// Tag: [READINESS]
/// </summary>
internal sealed class UserInputWaitAdapter : IUserInputWait
{
    private readonly bool _noSound;
    public UserInputWaitAdapter(bool noSound = false) => _noSound = noSound;

    public UserYieldResult WaitForUserYield(IntPtr targetMainHwnd, uint userIdleMs, int timeoutSeconds,
                                             IntPtr positionHwnd = default)
    {
        if (ShouldAutoApproveAll())
        {
            var focusOk = targetMainHwnd == IntPtr.Zero ||
                NativeMethods.SmartSetForegroundWindow(targetMainHwnd);
            Console.WriteLine("  [READINESS] auto-approve-all: yield popup skipped");
            return new UserYieldResult(true, focusOk);
        }

        // Dot animation: "[승인팝업]" immediately on popup start, then one ' .' per second
        using var dotCts = new CancellationTokenSource();
        var dotThread = new Thread(() =>
        {
            Console.Write("  [승인팝업]");
            Console.Out.Flush();
            while (!dotCts.Token.WaitHandle.WaitOne(1000))
            {
                Console.Write(" .");
                Console.Out.Flush();
            }
        }) { IsBackground = true, Name = "YieldDotAnim" };

        var sw = System.Diagnostics.Stopwatch.StartNew();
        dotThread.Start();

        var result = new UserYieldResult(false, false);
        try
        {
            var actionArgs = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
            var (approved, focusAcquired) = UserInputWaitOverlay.Show(targetMainHwnd, userIdleMs, timeoutSeconds,
                positionHwnd: positionHwnd, noSound: _noSound, actionInfo: actionArgs);
            result = new UserYieldResult(approved, focusAcquired);
            return result;
        }
        finally
        {
            sw.Stop();
            dotCts.Cancel();
            dotThread.Join(200);
            var label = result.Approved ? "확인" : "취소";
            Console.WriteLine($" → {label} ({sw.Elapsed.TotalSeconds:F1}s)");
            Console.Out.Flush();
        }
    }

    static bool ShouldAutoApproveAll()
    {
        // [FOCUS-GUARD] 환경변수로만 제어 — wkappbot-core.exe 자동 활성화 제거!
        //
        // 이전: wkappbot-core.exe 직접 실행 시 auto-approve-all=true (Eye 데몬 용도)
        // 문제: prompt-probe --input 같은 유저 직접 실행 명령에도 적용 → yield popup 스킵
        //        → 유저가 타이핑 중에도 포커스 강탈이 그냥 통과됨 (유저 입력 방해!)
        //
        // 올바른 방식: 자동화 케이스는 개별 Probe() 호출에서 AutoApproveYield=true 사용.
        // 전체 프로세스 블랭킷 auto-approve는 유저 보호를 무력화하므로 금지.
        //
        // 필요하다면 환경변수로 명시적 설정: WKAPPBOT_AUTO_APPROVE_ALL=1
        var env = Environment.GetEnvironmentVariable("WKAPPBOT_AUTO_APPROVE_ALL");
        if (!string.IsNullOrWhiteSpace(env))
        {
            var v = env.Trim().ToLowerInvariant();
            if (v is "0" or "false" or "no" or "off")
                return false;
            return true;
        }

        return false; // 기본값: auto-approve-all 비활성화
    }
}

// ── ElevationRequesterAdapter ────────────────────────────────────

/// <summary>
/// IElevationRequester 구현: Elevated Eye Proxy 우선, 없으면 UAC runas.
/// 1. Elevated Eye 프록시가 살아있으면 → Pipe로 명령 위임 (투명 중계)
/// 2. 없으면 → Elevated Eye 시작 시도 → 프록시로 명령 위임
/// 3. UAC 취소 → false 반환 → 포커스리스 메서드로 계속
/// Tag: [READINESS]
/// </summary>
internal sealed class ElevationRequesterAdapter : IElevationRequester
{
    public bool RequestElevation(string targetProcessName, uint targetPid)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [ELEVATION] {targetProcessName} (PID {targetPid}) is elevated — requesting admin rights...");
        Console.ResetColor();

        // Strategy 1: Use existing elevated Eye proxy
        if (ElevatedEyeClient.IsAvailable())
        {
            Console.WriteLine("  [ELEVATION] Elevated Eye proxy found — delegating command");
            return DelegateViaProxy();
        }

        // Strategy 2: Launch elevated Eye, then delegate
        Console.WriteLine("  [ELEVATION] No elevated Eye — launching admin Eye proxy...");
        if (ElevationHelper.LaunchElevatedEye())
        {
            return DelegateViaProxy();
        }

        // Strategy 3: Fallback — relaunch self as admin (legacy)
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";
            var rawArgs = Environment.GetCommandLineArgs();
            var args = string.Join(" ", rawArgs.Skip(1).Select(a =>
                a.Contains(' ') || a.Contains('"') ? $"\"{a.Replace("\"", "\\\"")}\"" : a));

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args,
                UseShellExecute = true,
                Verb = "runas",
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  [ELEVATION] Fallback: relaunching as admin: {Path.GetFileName(exePath)} {args}");
            Console.ResetColor();

            var proc = Process.Start(psi);
            if (proc != null)
            {
                proc.WaitForExit();
                Environment.Exit(proc.ExitCode);
                return true;
            }
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 1223) // ERROR_CANCELLED
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ELEVATION] UAC denied — continuing with focusless methods only");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [ELEVATION] Relaunch failed: {ex.Message}");
            Console.ResetColor();
        }

        return false;
    }

    /// <summary>Delegate current command to elevated Eye proxy and exit.</summary>
    private bool DelegateViaProxy()
    {
        var rawArgs = Environment.GetCommandLineArgs();
        // args[0] = exe path, args[1] = command, args[2..] = rest
        if (rawArgs.Length < 2) return false;

        var command = rawArgs[1];
        var args = rawArgs.Skip(2).ToArray();

        var exitCode = ElevatedEyeClient.ExecuteViaProxy(command, args);
        if (exitCode >= 0)
        {
            Environment.Exit(exitCode);
            return true; // unreachable after Exit
        }

        Console.WriteLine("  [ELEVATION] Proxy delegation failed — falling back to runas");
        return false;
    }
}

// ── ReadinessZoomAdapter ──────────────────────────────────────────

/// <summary>
/// IReadinessZoom 구현: ClickZoomHelper 래핑 + 이전 돋보기 재활용.
/// Tag: [READINESS] [ZOOM]
/// </summary>
internal sealed class ReadinessZoomAdapter : IReadinessZoom
{
    private ClickZoomHelper? _lastZoom;

    public IActionZoom? Begin(Rectangle screenRect, IntPtr formHandle,
                              string action, string label)
    {
        // 이전 돋보기가 아직 살아있으면 재활용
        if (_lastZoom != null)
        {
            try
            {
                _lastZoom.UpdateStatus($"[READINESS] {action}: {label}");
                return new ClickZoomAdapter(_lastZoom);
            }
            catch
            {
                _lastZoom = null; // 죽은 돋보기 — 새로 생성
            }
        }

        // 새로 생성
        _lastZoom = ClickZoomHelper.BeginFromRect(screenRect, formHandle,
                                                   "readiness", label);
        if (_lastZoom == null) return null;
        return new ClickZoomAdapter(_lastZoom);
    }
}
