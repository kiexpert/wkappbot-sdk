using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using WKAppBot.Win32.Input;

namespace WKAppBot.Vision;

/// <summary>
/// Simple OCR-based element locator using Windows.Media.Ocr (WinRT).
/// FREE, offline, Korean + English built-in on Korean Windows.
///
/// Strategy:
///   1. Capture screenshot → WinRT SoftwareBitmap
///   2. OcrEngine.RecognizeAsync() → all text + bounding boxes
///   3. Match description against recognized text (fuzzy)
///   4. Return center of matched text region
///
/// Confidence:
///   - OCR recognizes text → erase recognized pixels → remaining pixel ratio = noise
///   - High text coverage = high confidence
///   - Low coverage / no match = low confidence → fall back to Claude API
///
/// Position in chain: UIA → Vision Cache → **Simple OCR** → Claude API → Coordinate
/// </summary>
public sealed class SimpleOcrAnalyzer : IDisposable
{
    private readonly OcrEngine _ocrEngine;
    private readonly OcrEngine? _ocrEngineFallback;  // secondary language
    private readonly double _confidenceThreshold;

    public string PrimaryLanguage { get; }
    public string? FallbackLanguage { get; }

    public SimpleOcrAnalyzer(
        string primaryLanguage = "ko",
        string? fallbackLanguage = "en-US",
        double confidenceThreshold = 0.5)
    {
        _confidenceThreshold = confidenceThreshold;
        PrimaryLanguage = primaryLanguage;
        FallbackLanguage = fallbackLanguage;

        // Initialize primary OCR engine
        var primaryLang = new Windows.Globalization.Language(primaryLanguage);
        if (!OcrEngine.IsLanguageSupported(primaryLang))
            throw new InvalidOperationException(
                $"OCR language '{primaryLanguage}' not installed. " +
                "Install via Settings → Time & Language → Language.");
        _ocrEngine = OcrEngine.TryCreateFromLanguage(primaryLang)
            ?? throw new InvalidOperationException($"Failed to create OCR engine for '{primaryLanguage}'");

        // Initialize fallback OCR engine (optional)
        if (!string.IsNullOrEmpty(fallbackLanguage))
        {
            var fallbackLang = new Windows.Globalization.Language(fallbackLanguage);
            if (OcrEngine.IsLanguageSupported(fallbackLang))
                _ocrEngineFallback = OcrEngine.TryCreateFromLanguage(fallbackLang);
        }
    }

    /// <summary>
    /// Find a UI element by matching OCR text against the description.
    /// Returns ElementLocation with pixel coordinates, or null if not found.
    /// </summary>
    public async Task<OcrMatchResult?> FindElement(
        Bitmap screenshot,
        string description,
        CancellationToken ct = default)
    {
        // Convert System.Drawing.Bitmap → WinRT SoftwareBitmap
        using var softwareBitmap = await ConvertToSoftwareBitmap(screenshot);

        // Run OCR (primary language first)
        var result = await _ocrEngine.RecognizeAsync(softwareBitmap);

        // Try matching
        var match = FindBestMatch(result, description, screenshot.Width, screenshot.Height);

        // If no match and fallback exists, try fallback language
        if (match == null && _ocrEngineFallback != null)
        {
            var fallbackResult = await _ocrEngineFallback.RecognizeAsync(softwareBitmap);
            match = FindBestMatch(fallbackResult, description, screenshot.Width, screenshot.Height);
        }

        return match;
    }

    /// <summary>
    /// Get ALL recognized text from a screenshot.
    /// Useful for debugging / assert verification.
    /// </summary>
    public async Task<OcrFullResult> RecognizeAll(
        Bitmap screenshot,
        CancellationToken ct = default)
    {
        using var softwareBitmap = await ConvertToSoftwareBitmap(screenshot);
        var result = await _ocrEngine.RecognizeAsync(softwareBitmap);

        var words = new List<OcrWord>();
        foreach (var line in result.Lines)
        {
            foreach (var word in line.Words)
            {
                words.Add(new OcrWord
                {
                    Text = word.Text,
                    X = (int)word.BoundingRect.X,
                    Y = (int)word.BoundingRect.Y,
                    Width = (int)word.BoundingRect.Width,
                    Height = (int)word.BoundingRect.Height
                });
            }
        }

        return new OcrFullResult
        {
            FullText = result.Text,
            Words = words,
            Angle = result.TextAngle ?? 0
        };
    }

    /// <summary>
    /// Match OCR results against a description string.
    /// Supports: exact match, contains, starts_with, and multi-word fuzzy.
    /// </summary>
    private OcrMatchResult? FindBestMatch(OcrResult ocrResult, string description, int imgW, int imgH)
    {
        if (ocrResult.Lines.Count == 0) return null;

        var desc = description.Trim();
        OcrMatchResult? bestMatch = null;
        double bestScore = 0;

        // Strategy 1: Line-level matching (for phrases / labels)
        foreach (var line in ocrResult.Lines)
        {
            var lineText = line.Text.Trim();
            var score = CalculateMatchScore(lineText, desc);

            if (score > bestScore && score >= _confidenceThreshold)
            {
                // Get bounding rect of the entire line
                double minX = double.MaxValue, minY = double.MaxValue;
                double maxX = 0, maxY = 0;
                foreach (var word in line.Words)
                {
                    var r = word.BoundingRect;
                    if (r.X < minX) minX = r.X;
                    if (r.Y < minY) minY = r.Y;
                    if (r.X + r.Width > maxX) maxX = r.X + r.Width;
                    if (r.Y + r.Height > maxY) maxY = r.Y + r.Height;
                }

                bestScore = score;
                bestMatch = new OcrMatchResult
                {
                    X = (int)((minX + maxX) / 2),
                    Y = (int)((minY + maxY) / 2),
                    Width = (int)(maxX - minX),
                    Height = (int)(maxY - minY),
                    MatchedText = lineText,
                    Confidence = score,
                    MatchType = "line"
                };
            }
        }

        // Strategy 2: Word-level matching (for single-word labels)
        foreach (var line in ocrResult.Lines)
        {
            foreach (var word in line.Words)
            {
                var wordText = word.Text.Trim();
                var score = CalculateMatchScore(wordText, desc);

                if (score > bestScore && score >= _confidenceThreshold)
                {
                    var r = word.BoundingRect;
                    bestScore = score;
                    bestMatch = new OcrMatchResult
                    {
                        X = (int)(r.X + r.Width / 2),
                        Y = (int)(r.Y + r.Height / 2),
                        Width = (int)r.Width,
                        Height = (int)r.Height,
                        MatchedText = wordText,
                        Confidence = score,
                        MatchType = "word"
                    };
                }
            }
        }

        return bestMatch;
    }

    /// <summary>
    /// Calculate text match score (0.0 ~ 1.0).
    /// Supports exact, contains, and fuzzy matching.
    /// </summary>
    private static double CalculateMatchScore(string ocrText, string description)
    {
        if (string.IsNullOrWhiteSpace(ocrText)) return 0;

        var a = ocrText.ToLowerInvariant().Trim();
        var b = description.ToLowerInvariant().Trim();

        // Exact match
        if (a == b) return 1.0;

        // OCR text contains description
        if (a.Contains(b)) return 0.9;

        // Description contains OCR text (partial label)
        if (b.Contains(a)) return 0.7;

        // Starts with
        if (a.StartsWith(b) || b.StartsWith(a)) return 0.8;

        // Simple character overlap ratio (Dice coefficient)
        var setA = new HashSet<char>(a.Where(c => !char.IsWhiteSpace(c)));
        var setB = new HashSet<char>(b.Where(c => !char.IsWhiteSpace(c)));
        if (setA.Count == 0 || setB.Count == 0) return 0;

        int intersection = setA.Intersect(setB).Count();
        double dice = 2.0 * intersection / (setA.Count + setB.Count);

        return dice * 0.6;  // scale down fuzzy matches
    }

    /// <summary>
    /// Convert System.Drawing.Bitmap → WinRT SoftwareBitmap for OCR.
    /// </summary>
    private static async Task<SoftwareBitmap> ConvertToSoftwareBitmap(Bitmap bitmap)
    {
        // Save bitmap to in-memory stream as BMP
        using var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Bmp);
        ms.Position = 0;

        // Create WinRT IRandomAccessStream from MemoryStream
        var winrtStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
        var bytes = ms.ToArray();
        var writer = new Windows.Storage.Streams.DataWriter(winrtStream.GetOutputStreamAt(0));
        writer.WriteBytes(bytes);
        await writer.StoreAsync();
        await writer.FlushAsync();
        writer.DetachStream();

        // Decode image
        var decoder = await BitmapDecoder.CreateAsync(winrtStream);
        var softwareBitmap = await decoder.GetSoftwareBitmapAsync(
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

        return softwareBitmap;
    }

    public void Dispose()
    {
        // OcrEngine doesn't implement IDisposable, nothing to clean up
    }

    /// <summary>
    /// Get available OCR languages on this system.
    /// </summary>
    public static IReadOnlyList<string> GetAvailableLanguages()
    {
        return OcrEngine.AvailableRecognizerLanguages
            .Select(l => l.LanguageTag)
            .ToList()
            .AsReadOnly();
    }
}

/// <summary>
/// Result of OCR text matching against a description.
/// </summary>
public sealed class OcrMatchResult
{
    /// <summary>Center X in screenshot pixel coordinates</summary>
    public int X { get; set; }

    /// <summary>Center Y in screenshot pixel coordinates</summary>
    public int Y { get; set; }

    /// <summary>Matched region width</summary>
    public int Width { get; set; }

    /// <summary>Matched region height</summary>
    public int Height { get; set; }

    /// <summary>Actual text matched by OCR</summary>
    public string MatchedText { get; set; } = "";

    /// <summary>Match confidence (0.0~1.0)</summary>
    public double Confidence { get; set; }

    /// <summary>How the match was found: "word", "line"</summary>
    public string MatchType { get; set; } = "";

    /// <summary>Convert to ElementLocation for unified handling</summary>
    public ElementLocation ToElementLocation() => new()
    {
        X = X,
        Y = Y,
        Width = Width,
        Height = Height,
        Confidence = Confidence,
        Label = MatchedText,
        ControlType = "OcrText"
    };
}

/// <summary>
/// Full OCR recognition result (all words).
/// </summary>
public sealed class OcrFullResult
{
    public string FullText { get; set; } = "";
    public List<OcrWord> Words { get; set; } = new();
    public double Angle { get; set; }
}

/// <summary>
/// Single recognized word with position.
/// </summary>
public sealed class OcrWord
{
    public string Text { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}
