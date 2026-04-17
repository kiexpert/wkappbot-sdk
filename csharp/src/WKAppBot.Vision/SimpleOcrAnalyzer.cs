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
///   1. Capture screenshot -> WinRT SoftwareBitmap
///   2. OcrEngine.RecognizeAsync() -> all text + bounding boxes
///   3. Match description against recognized text (fuzzy)
///   4. Return center of matched text region
///
/// Confidence:
///   - OCR recognizes text -> erase recognized pixels -> remaining pixel ratio = noise
///   - High text coverage = high confidence
///   - Low coverage / no match = low confidence -> fall back to Claude API
///
/// Position in chain: UIA -> Vision Cache -> **Simple OCR** -> Claude API -> Coordinate
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
                "Install via Settings -> Time & Language -> Language.");
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
        // Auto-upscale small images for better OCR accuracy
        var bmpToUse = screenshot;
        int scale = 1;
        bool needDispose = false;
        if (screenshot.Height < 200 || screenshot.Width < 400)
        {
            scale = screenshot.Height < 80 ? 4 : screenshot.Height < 200 ? 3 : 2;
            bmpToUse = UpscaleBitmap(screenshot, scale);
            needDispose = true;
        }

        try
        {
            using var softwareBitmap = await ConvertToSoftwareBitmap(bmpToUse);
            var result = await _ocrEngine.RecognizeAsync(softwareBitmap);
            var match = FindBestMatch(result, description, bmpToUse.Width, bmpToUse.Height);

            if (match == null && _ocrEngineFallback != null)
            {
                var fallbackResult = await _ocrEngineFallback.RecognizeAsync(softwareBitmap);
                match = FindBestMatch(fallbackResult, description, bmpToUse.Width, bmpToUse.Height);
            }

            // Scale coordinates back to original image space
            if (match != null && scale > 1)
            {
                match.X /= scale;
                match.Y /= scale;
                match.Width /= scale;
                match.Height /= scale;
            }

            return match;
        }
        finally
        {
            if (needDispose) bmpToUse.Dispose();
        }
    }

    /// <summary>
    /// Get ALL recognized text from a screenshot.
    /// Auto-upscales small images for better OCR accuracy.
    /// Useful for debugging / assert verification.
    /// </summary>
    public async Task<OcrFullResult> RecognizeAll(
        Bitmap screenshot,
        CancellationToken ct = default)
    {
        // Auto-upscale small images for better OCR accuracy
        // Windows.Media.Ocr works best with text >= 40px height
        var bmpToUse = screenshot;
        int scale = 1;
        bool needDispose = false;
        if (screenshot.Height < 200 || screenshot.Width < 400)
        {
            scale = screenshot.Height < 80 ? 4 : screenshot.Height < 200 ? 3 : 2;
            bmpToUse = UpscaleBitmap(screenshot, scale);
            needDispose = true;
        }

        try
        {
            using var softwareBitmap = await ConvertToSoftwareBitmap(bmpToUse);
            var result = await _ocrEngine.RecognizeAsync(softwareBitmap);

            var words = new List<OcrWord>();
            foreach (var line in result.Lines)
            {
                foreach (var word in line.Words)
                {
                    // Scale coordinates back to original image space
                    words.Add(new OcrWord
                    {
                        Text = word.Text,
                        X = (int)(word.BoundingRect.X / scale),
                        Y = (int)(word.BoundingRect.Y / scale),
                        Width = (int)(word.BoundingRect.Width / scale),
                        Height = (int)(word.BoundingRect.Height / scale)
                    });
                }
            }

            return new OcrFullResult
            {
                FullText = result.Text,
                Words = words,
                Angle = result.TextAngle ?? 0,
                UpscaleFactor = scale
            };
        }
        finally
        {
            if (needDispose) bmpToUse.Dispose();
        }
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
    /// Recognize ALL text in a screenshot as line-level segments.
    /// Each OcrEngine line becomes one OcrSegment with relative coordinates.
    /// Runs both primary + fallback engines; deduplicates by position overlap.
    ///
    /// Used to build the OcrSegmentCache (dynamic a11y tree from vision).
    /// Coordinates are normalized against screenshot dimensions (0.0-1.0).
    /// </summary>
    public async Task<List<OcrSegment>> SegmentAll(
        Bitmap screenshot,
        CancellationToken ct = default)
    {
        var bmpToUse = screenshot;
        int scale = 1;
        bool needDispose = false;
        if (screenshot.Height < 200 || screenshot.Width < 400)
        {
            scale = screenshot.Height < 80 ? 4 : screenshot.Height < 200 ? 3 : 2;
            bmpToUse = UpscaleBitmap(screenshot, scale);
            needDispose = true;
        }

        try
        {
            using var softwareBitmap = await ConvertToSoftwareBitmap(bmpToUse);
            var result = await _ocrEngine.RecognizeAsync(softwareBitmap);
            var segments = ExtractLineSegments(result, scale, bmpToUse.Width / scale, bmpToUse.Height / scale);

            if (_ocrEngineFallback != null)
            {
                var fbResult = await _ocrEngineFallback.RecognizeAsync(softwareBitmap);
                var fbSegs = ExtractLineSegments(fbResult, scale, bmpToUse.Width / scale, bmpToUse.Height / scale);
                foreach (var seg in fbSegs)
                {
                    bool hasOverlap = segments.Any(s =>
                        Math.Abs(s.RelX - seg.RelX) < 0.02 && Math.Abs(s.RelY - seg.RelY) < 0.02);
                    if (!hasOverlap) segments.Add(seg);
                }
            }
            return segments;
        }
        finally
        {
            if (needDispose) bmpToUse.Dispose();
        }
    }

    private static List<OcrSegment> ExtractLineSegments(
        Windows.Media.Ocr.OcrResult result, int scale, int imgW, int imgH)
    {
        var segments = new List<OcrSegment>();
        if (imgW <= 0 || imgH <= 0) return segments;

        foreach (var line in result.Lines)
        {
            var text = line.Text?.Trim();
            if (string.IsNullOrEmpty(text)) continue;

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
            minX /= scale; minY /= scale; maxX /= scale; maxY /= scale;

            double cx = (minX + maxX) / 2;
            double cy = (minY + maxY) / 2;
            double w = maxX - minX;
            double h = maxY - minY;

            segments.Add(new OcrSegment
            {
                Text = text,
                RelX = cx / imgW,
                RelY = cy / imgH,
                RelW = w / imgW,
                RelH = h / imgH,
                Confidence = 0.9,
                Source = "ocr"
            });
        }
        return segments;
    }

    /// <summary>
    /// Fast form fingerprint: MD5 of 32×32 downscaled screenshot.
    /// Used by OcrSegmentCache to detect UI changes (cache invalidation).
    /// </summary>
    public static string ComputeFormHash(Bitmap bmp)
    {
        using var small = new Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(small))
            g.DrawImage(bmp, 0, 0, 32, 32);

        var rect = new Rectangle(0, 0, 32, 32);
        var data = small.LockBits(rect,
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        var bytes = new byte[data.Stride * 32];
        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
        small.UnlockBits(data);

        return Convert.ToHexString(System.Security.Cryptography.MD5.HashData(bytes)).ToLowerInvariant();
    }

    /// <summary>
    /// Text match score (0.0 ~ 1.0) -- public for use by OcrSegmentCache.BestMatch.
    /// Supports exact, contains, starts-with, and Dice-coefficient fuzzy matching.
    /// </summary>
    public static double MatchScore(string ocrText, string description)
        => CalculateMatchScore(ocrText, description);

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
    /// Upscale a bitmap by a factor using high-quality bicubic interpolation.
    /// Small text (under ~40px) is hard for OCR; upscaling 2-4x dramatically improves accuracy.
    /// </summary>
    private static Bitmap UpscaleBitmap(Bitmap original, int factor)
    {
        var newWidth = original.Width * factor;
        var newHeight = original.Height * factor;
        // Always use 32bppArgb -- indexed formats (8bpp, 4bpp, 1bpp) can't be used with Graphics
        var scaled = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        scaled.SetResolution(original.HorizontalResolution * factor, original.VerticalResolution * factor);
        using var g = Graphics.FromImage(scaled);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        g.DrawImage(original, 0, 0, newWidth, newHeight);
        return scaled;
    }

    /// <summary>
    /// Convert System.Drawing.Bitmap -> WinRT SoftwareBitmap for OCR.
    /// </summary>
    private static async Task<SoftwareBitmap> ConvertToSoftwareBitmap(Bitmap bitmap)
    {
        // Normalize pixel format -- indexed/non-standard formats throw ArgumentException on BMP save
        Bitmap? owned = null;
        if (bitmap.PixelFormat is not (System.Drawing.Imaging.PixelFormat.Format32bppArgb
            or System.Drawing.Imaging.PixelFormat.Format32bppRgb
            or System.Drawing.Imaging.PixelFormat.Format24bppRgb
            or System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
        {
            owned = new Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using var g = Graphics.FromImage(owned);
            g.DrawImage(bitmap, 0, 0);
            bitmap = owned;
        }
        try
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
        finally
        {
            owned?.Dispose();
        }
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
    /// <summary>Upscale factor applied before OCR (1 = no upscale). Coordinates are already scaled back.</summary>
    public int UpscaleFactor { get; set; } = 1;
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
