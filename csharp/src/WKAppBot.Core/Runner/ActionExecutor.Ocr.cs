using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Input;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    /// <summary>
    /// OCR Preview: run OCR in background even when UIA succeeds.
    /// Compares UIA text vs OCR text for accuracy benchmarking.
    /// Does NOT affect actual element location -- purely informational.
    /// </summary>
    private void RunOcrPreview(StepDefinition step)
    {
        try
        {
            using var bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle, new WKAppBot.Win32.Input.CaptureOptions
            {
                RejectBlank = true,
                StepLogger = Log,
            });
            if (bmp == null)
            {
                Log("  OCR preview: blank/failed capture -- skipping");
                return;
            }
            var screenshotPath = SaveVisionScreenshot(bmp, step.Target!.Description!);

            // Get UIA text for comparison
            string? uiaText = null;
            try
            {
                var (uiaElem, _) = _uia.FindElement(
                    _ctx.MainWindowHandle,
                    step.Target.AutomationId,
                    step.Target.Name,
                    step.Target.ControlType);
                if (uiaElem != null)
                    uiaText = uiaElem.Properties.Name.ValueOrDefault;
            }
            catch { /* ignore */ }

            // Run OCR full recognition
            var fullResult = _simpleOcr!.RecognizeAll(bmp).GetAwaiter().GetResult();

            // Run OCR element matching
            var ocrMatch = _simpleOcr.FindElement(bmp, step.Target.Description!)
                .GetAwaiter().GetResult();

            if (ocrMatch != null)
            {
                // Calculate UIA vs OCR match rate
                string matchInfo = "";
                if (!string.IsNullOrEmpty(uiaText))
                {
                    var rate = CalculateTextMatchRate(uiaText, ocrMatch.MatchedText);
                    matchInfo = rate >= 0.9
                        ? $" | UIA↔OCR={rate:P0} ★"
                        : $" | UIA↔OCR={rate:P0}";
                }

                Log($"  [OCR PREVIEW] found \"{ocrMatch.MatchedText}\" ({ocrMatch.X},{ocrMatch.Y}) conf={ocrMatch.Confidence:F2} [{ocrMatch.MatchType}]{matchInfo}");
            }
            else
            {
                var textSnippet = fullResult.FullText.Length > 80
                    ? fullResult.FullText[..80] + "..."
                    : fullResult.FullText;
                Log($"  [OCR PREVIEW] no match for \"{step.Target.Description}\" | OCR saw: \"{textSnippet.Replace('\n', ' ')}\"");
            }

            // Summary line
            Log($"  [OCR PREVIEW] {fullResult.Words.Count} words | UIA=\"{uiaText ?? "(none)"}\" | img={screenshotPath ?? "n/a"}");
        }
        catch (Exception ex)
        {
            Log($"  [OCR PREVIEW] error: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculate similarity between UIA text and OCR text (0.0~1.0).
    /// </summary>
    private static double CalculateTextMatchRate(string uiaText, string ocrText)
    {
        var a = uiaText.Trim().ToLowerInvariant();
        var b = ocrText.Trim().ToLowerInvariant();

        if (a == b) return 1.0;
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return 0;

        // Contains check
        if (a.Contains(b) || b.Contains(a))
        {
            double shorter = Math.Min(a.Length, b.Length);
            double longer = Math.Max(a.Length, b.Length);
            return shorter / longer;
        }

        // Character overlap (Dice coefficient)
        var setA = new HashSet<char>(a.Where(c => !char.IsWhiteSpace(c)));
        var setB = new HashSet<char>(b.Where(c => !char.IsWhiteSpace(c)));
        if (setA.Count == 0 || setB.Count == 0) return 0;

        int intersection = setA.Intersect(setB).Count();
        return 2.0 * intersection / (setA.Count + setB.Count);
    }
}
