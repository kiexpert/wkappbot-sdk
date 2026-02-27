using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// Adapts ClickZoomHelper to IActionZoom interface for use in ActionExecutor.
/// CLI layer bridge: Core calls IActionZoom → adapter forwards to ClickZoomHelper (WPF overlay).
///
/// Tag: [ZOOM]
/// </summary>
internal sealed class ClickZoomAdapter : IActionZoom
{
    private ClickZoomHelper? _helper;

    public ClickZoomAdapter(ClickZoomHelper helper) => _helper = helper;

    public void UpdateStatus(string text) => _helper?.UpdateStatus(text);

    public void ShowPass(string detail)
    {
        _helper?.ShowPass(detail);
        _helper = null; // release — STA thread cleans up via fade-out
    }

    public void ShowFail(string detail)
    {
        _helper?.ShowFail(detail);
        _helper = null;
    }

    public void Dispose()
    {
        _helper?.Dispose();
        _helper = null;
    }
}
