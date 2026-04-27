using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Executes individual step actions.
/// 5-tier Chain of Responsibility:
///   1. UIA (Accessibility) -- fast, reliable
///   2. Vision Cache -- cached results (경험치!)
///   3. Simple OCR -- Windows.Media.Ocr text matching (free, fast, offline)
///   4. Vision API -- Claude screenshot analysis (expensive, cached)
///   5. Coordinate -- raw x,y fallback
///
/// Focusless-First principle:
///   - UIA Invoke (click), UIA Value (type) -> no focus needed, user undisturbed
///   - SendInput (mouse/keyboard) -> EnsureFocus required
/// </summary>
public sealed partial class ActionExecutor : IDisposable
{
    private readonly UiaLocator _uia;
    private readonly RuntimeContext _ctx;
    private readonly bool _verbose;

    // Vision tier (optional -- only when vision_enabled: true)
    private VisionCache? _visionCache;
    private VisionAnalyzer? _visionAnalyzer;
    private SimpleOcrAnalyzer? _simpleOcr;
    private OcrSegmentCache? _segmentCache;  // Tier 2.5: form-level dynamic a11y tree

    // [ZOOM] Overlay factory -- set by CLI layer (ClickZoomAdapter)
    // Parameters: (screenRect, formHandle, actionName, label) -> IActionZoom?
    private IActionZoom? _currentZoom;

    /// <summary>
    /// Optional zoom overlay factory. Set by CLI layer to enable visual feedback.
    /// Parameters: (System.Drawing.Rectangle screenRect, IntPtr formHandle, string action, string label)
    /// </summary>
    public Func<System.Drawing.Rectangle, IntPtr, string, string, IActionZoom?>? CreateZoom { get; set; }

    /// <summary>
    /// Optional Vision AI ask delegate -- CLI wires this to Gemini/Claude CDP for blob identification.
    ///
    /// Called when OcrSegmentCache has no text match AND Vision API isn't configured.
    /// Parameters: (formScreenshot, elementDescription)
    /// Returns: OcrSegment with x/y/w/h from Gemini JSON, or null on failure.
    ///
    /// Coordinates come directly from Gemini JSON -- no BestMatch step needed.
    /// Result is used to:
    ///   1. Save blob crop: {pixelHash}={label}.png
    ///   2. Teach OcrSegmentCache (source="gemini") for future lookups
    /// </summary>
    public Func<System.Drawing.Bitmap, string, Task<OcrSegment?>>? AskVisionFn { get; set; }

    public ActionExecutor(RuntimeContext ctx, bool verbose = false,
                          VisionCache? visionCache = null, VisionAnalyzer? visionAnalyzer = null,
                          SimpleOcrAnalyzer? simpleOcr = null,
                          OcrSegmentCache? segmentCache = null)
    {
        _ctx = ctx;
        _verbose = verbose;
        _uia = new UiaLocator();
        _visionCache = visionCache;
        _visionAnalyzer = visionAnalyzer;
        _segmentCache = segmentCache;
        _simpleOcr = simpleOcr;
    }

    // -- [READINESS] Pre-action input readiness check ------------

    /// <summary>
    /// Optional InputReadiness instance. Set by CLI layer to enable pre-action readiness checks.
    /// When set, every a11y action triggers: readiness check -> zoom -> actual call.
    /// </summary>
    public InputReadiness? Readiness { get; set; }

    /// <summary>
    /// Set action point to the center of the test target window.
    /// Used for non-positional actions (type_text, hotkey, etc.) that affect the focused window.
    /// </summary>
    private void SetActionPointToWindowCenter(string stepName, string action, string? desc = null)
    {
        try
        {
            var rect = WindowFinder.GetWindowRect(_ctx.MainWindowHandle);
            int cx = rect.Left + rect.Width / 2;
            int cy = rect.Top + rect.Height / 2;
            _ctx.SetActionPoint(cx, cy, stepName, action, desc);
        }
        catch { /* ignore -- non-critical */ }
    }

    private void Log(string msg)
    {
        if (_verbose) Console.WriteLine(msg);
    }

    public void Dispose()
    {
        _uia.Dispose();
    }
}
