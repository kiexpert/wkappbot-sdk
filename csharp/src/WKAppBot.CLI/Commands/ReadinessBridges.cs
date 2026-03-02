using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: InputReadiness bridge methods — expose internal helpers to adapters.
// Tag: [READINESS]
internal partial class Program
{
    // ── 팩토리: InputReadiness 생성 + 모든 어댑터 연결 ──

    /// <summary>
    /// Create a fully-wired InputReadiness instance with blocker handler, knowhow broadcaster, and zoom factory.
    /// </summary>
    internal static InputReadiness CreateInputReadiness(string? handlersDir = null)
    {
        handlersDir ??= Path.Combine(DataDir, "handlers");
        var handlerMgr = Directory.Exists(handlersDir) ? new DialogHandlerManager(handlersDir) : null;

        return new InputReadiness
        {
            BlockerHandler = new BlockerHandlerAdapter(handlerMgr),
            KnowhowBroadcaster = new KnowhowBroadcasterAdapter(),
            ZoomFactory = new ReadinessZoomAdapter(),
            UserInputWait = new UserInputWaitAdapter(),
        };
    }

    // ── Bridge: TryHandleBlocker → adapter ──

    /// <summary>
    /// Bridge for BlockerHandlerAdapter: delegates to existing TryHandleBlocker.
    /// </summary>
    internal static (bool handled, bool shouldRetry) TryHandleBlockerViaReadiness(
        IntPtr mainHwnd, DialogHandlerManager? handlerMgr)
    {
        return TryHandleBlocker(mainHwnd, handlerMgr);
    }

    // ── Bridge: Knowhow broadcast → adapter ──

    /// <summary>
    /// Bridge for KnowhowBroadcasterAdapter: delegates to BroadcastInspectKnowhow.
    /// Resolves profile matching and form ID from window handle.
    /// </summary>
    internal static void BroadcastKnowhowViaReadiness(IntPtr mainHwnd, string? formId, string? actionName)
    {
        try
        {
            var className = WindowFinder.GetClassName(mainHwnd);
            var title = WindowFinder.GetWindowText(mainHwnd);
            BroadcastInspectKnowhow(mainHwnd, className, formId, title);
        }
        catch { }
    }
}
