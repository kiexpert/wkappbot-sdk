using System.Drawing;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
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
    public bool WaitForUserYield(IntPtr targetMainHwnd, uint userIdleMs, int timeoutSeconds)
    {
        return UserInputWaitOverlay.Show(targetMainHwnd, userIdleMs, timeoutSeconds);
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
