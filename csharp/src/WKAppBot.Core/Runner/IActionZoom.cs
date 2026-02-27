namespace WKAppBot.Core.Runner;

/// <summary>
/// Zoom overlay interface for visual feedback during automation actions.
/// Implemented by CLI layer (ClickZoomHelper adapter).
/// Core layer creates via factory callback — no WPF dependency.
///
/// Tag: [ZOOM]
/// </summary>
public interface IActionZoom : IDisposable
{
    void UpdateStatus(string text);
    void ShowPass(string detail);
    void ShowFail(string detail);
}
