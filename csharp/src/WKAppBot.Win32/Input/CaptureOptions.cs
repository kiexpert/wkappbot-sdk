namespace WKAppBot.Win32.Input;

/// <summary>
/// Options for <see cref="ScreenCapture.CaptureWindow(System.IntPtr, CaptureOptions)"/>.
/// Allows callers to opt into blank-rejection, save-to-disk debug dumps, and custom step logging
/// without polluting the static <see cref="ScreenCapture.StepLogger"/> channel.
/// </summary>
public sealed class CaptureOptions
{
    /// <summary>
    /// When true (default), <see cref="ScreenCapture.CaptureWindow(System.IntPtr, CaptureOptions)"/>
    /// returns null if the final bitmap is detected as blank via <see cref="ScreenCapture.IsBlankBitmap"/>.
    /// Blank captures pollute OCR / Vision / ExperienceDB data -- callers that prefer a null over
    /// bad data should leave this enabled.
    /// </summary>
    public bool RejectBlank { get; init; } = true;

    /// <summary>
    /// When true (default), the capture pipeline prefers focusless tiers (PrintWindow, BringToFront
    /// with SWP_NOACTIVATE, layered-overlay hiding) and only uses focus-borrow as a last resort.
    /// Currently informational; the existing <see cref="ScreenCapture.CaptureWindow(System.IntPtr)"/>
    /// ladder is already focusless-first. Reserved for future tier-gating.
    /// </summary>
    public bool Focusless { get; init; } = true;

    /// <summary>
    /// When non-null, the captured bitmap is saved to this path via
    /// <see cref="ScreenCapture.SaveToFile"/> (PNG/JPEG/BMP by extension).
    /// Blank captures are NOT saved when <see cref="RejectBlank"/> is true.
    /// </summary>
    public string? SaveDebug { get; init; }

    /// <summary>
    /// Optional per-call step logger. When provided, it is swapped into
    /// <see cref="ScreenCapture.StepLogger"/> for the duration of the call and restored afterwards.
    /// </summary>
    public Action<string>? StepLogger { get; init; }

    /// <summary>Default options: RejectBlank=true, Focusless=true, no debug save, no logger.</summary>
    public static CaptureOptions Default => new();
}
