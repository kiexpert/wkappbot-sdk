using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace WKAppBot.CLI;

/// <summary>
/// Collects OCR gap screenshots during a11y actions, then composites them
/// into a single image for batch Vision API query (Gemini).
///
/// Usage:
///   var collector = new OcrGapCollector();
///   collector.Add(bounds, ocrPartialText);   // during action pipeline
///   collector.Add(bounds2, null);             // blind node
///   if (collector.HasGaps)
///   {
///       var (image, prompt) = collector.BuildComposite();
///       // send to Gemini Vision...
///   }
/// </summary>
internal sealed class OcrGapCollector
{
    private readonly List<GapEntry> _entries = new();
    private const int Padding = 4;
    private const int LabelHeight = 18;

    private record GapEntry(Rectangle Bounds, string? OcrPartial, string? Aid, Bitmap Screenshot, string PixelHash);

    public bool HasGaps => _entries.Count > 0;
    public int Count => _entries.Count;

    private static readonly string CacheDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "WKAppBot", "gap_cache");

    /// <summary>
    /// Capture and store a gap screenshot. If pixel hash matches cache, returns cached description.
    /// </summary>
    /// <param name="screenRect">Element bounding rect in screen coordinates</param>
    /// <param name="ocrPartial">Partial OCR text with [?] markers, or null if fully blind</param>
    /// <param name="aid">UIA AutomationId (may be empty)</param>
    /// <param name="cachedDescription">If cache hit, returns the stored description</param>
    /// <returns>true if added to gap list (needs Vision), false if cache hit</returns>
    public bool Add(Rectangle screenRect, string? ocrPartial, string? aid, out string? cachedDescription)
    {
        cachedDescription = null;
        try
        {
            if (screenRect.Width <= 0 || screenRect.Height <= 0) return false;
            var bmp = new Bitmap(screenRect.Width, screenRect.Height);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(screenRect.X, screenRect.Y, 0, 0, screenRect.Size);

            var hash = ComputePixelHash(bmp);
            var aidKey = SanitizeFileName(string.IsNullOrWhiteSpace(aid) ? "unk" : aid);

            // Cache lookup: {aid}-{hash}=*.png
            cachedDescription = LookupCache(aidKey, hash);
            if (cachedDescription != null)
                return false; // cache hit -- no need for Vision

            _entries.Add(new GapEntry(screenRect, ocrPartial, aid, bmp, hash));
            return true;
        }
        catch { return false; }
    }

    /// <summary>Backward compat: Add without cache check.</summary>
    public void Add(Rectangle screenRect, string? ocrPartial)
    {
        Add(screenRect, ocrPartial, null, out _);
    }

    /// <summary>
    /// Save Vision results to cache: {aid}-{hash}={description}.png
    /// </summary>
    public void SaveToCache(Dictionary<int, (string text, int score)> verified)
    {
        Directory.CreateDirectory(CacheDir);
        for (int i = 0; i < _entries.Count; i++)
        {
            if (!verified.TryGetValue(i + 1, out var result)) continue;
            if (result.score < 67) continue; // only cache reliable results

            var e = _entries[i];
            var aidKey = SanitizeFileName(string.IsNullOrWhiteSpace(e.Aid) ? "unk" : e.Aid);
            var descKey = SanitizeFileName(result.text);
            if (descKey.Length > 60) descKey = descKey[..60];
            var fileName = $"{aidKey}'{e.PixelHash}={descKey}.png";
            var filePath = Path.Combine(CacheDir, fileName);

            try
            {
                if (!File.Exists(filePath))
                    e.Screenshot.Save(filePath, ImageFormat.Png);
            }
            catch { /* best effort */ }
        }
    }

    private static string? LookupCache(string aidKey, string hash)
    {
        if (!Directory.Exists(CacheDir)) return null;
        // Pattern: {aid}-{hash}=*.png
        var pattern = $"{aidKey}'{hash}=*.png";
        var files = Directory.GetFiles(CacheDir, pattern);
        if (files.Length == 0) return null;

        // Extract description from filename: after '=' and before '.png'
        var name = Path.GetFileNameWithoutExtension(files[0]);
        var eqIdx = name.IndexOf('=');
        return eqIdx >= 0 ? name[(eqIdx + 1)..] : null;
    }

    internal static string ComputePixelHash(Bitmap bmp)
    {
        // Fast pixel hash: sample every 4th pixel, MD5 -> 8-char hex
        using var md5 = MD5.Create();
        int step = 4;
        int w = bmp.Width, h = bmp.Height;
        var buf = new byte[((w / step) + 1) * ((h / step) + 1) * 3];
        int bi = 0;
        for (int y = 0; y < h; y += step)
        for (int x = 0; x < w; x += step)
        {
            var c = bmp.GetPixel(x, y);
            if (bi + 2 < buf.Length)
            {
                buf[bi++] = c.R; buf[bi++] = c.G; buf[bi++] = c.B;
            }
        }
        var hash = md5.ComputeHash(buf, 0, bi);
        return Convert.ToHexString(hash)[..8].ToLowerInvariant();
    }

    private static string SanitizeFileName(string s)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new System.Text.StringBuilder(s.Length);
        foreach (var c in s)
            sb.Append(invalid.Contains(c) || c == '=' ? '-' : c);
        return sb.ToString().Trim('-');
    }

    /// <summary>
    /// Build 3 composite images (forward, reverse, shuffle) + prompt for cross-verification.
    /// Returns (list of PNG bytes, prompt text for Vision API).
    /// Gemini sees all 3 in one request -- inconsistent answers = suspect.
    /// </summary>
    public (List<byte[]> pngImages, string prompt) BuildTripleComposite()
    {
        if (_entries.Count == 0)
            return (new(), "");

        var indices = Enumerable.Range(0, _entries.Count).ToList();

        // 3 orderings: forward, reverse, shuffle
        var forward = indices.ToList();
        var reverse = indices.AsEnumerable().Reverse().ToList();
        var shuffle = indices.ToList();
        var rng = new Random();
        for (int i = shuffle.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (shuffle[i], shuffle[j]) = (shuffle[j], shuffle[i]);
        }

        var orderings = new[] { ("A", forward), ("B", reverse), ("C", shuffle) };
        var images = new List<byte[]>();

        foreach (var (label, order) in orderings)
            images.Add(RenderComposite(order, label));

        // Build prompt
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Three images are attached. Each shows the SAME UI regions in DIFFERENT order.");
        sb.AppendLine("Read the text in each numbered region of ALL three images independently.");
        sb.AppendLine("Do NOT copy answers between images -- read each one fresh.");
        sb.AppendLine("If a region contains no readable text but has an icon, symbol, or image,");
        sb.AppendLine("describe it in parentheses: (icon: description) or (image: description).");
        sb.AppendLine("Format your answer as:");
        sb.AppendLine();
        sb.AppendLine("Image A:");
        sb.AppendLine("[1] \"text\"");
        sb.AppendLine("[2] (icon: green play button)");
        sb.AppendLine("Image B:");
        sb.AppendLine("[1] \"text\"");
        sb.AppendLine("...");
        sb.AppendLine();
        sb.AppendLine("Region info:");
        for (int i = 0; i < _entries.Count; i++)
        {
            var e = _entries[i];
            sb.Append($"  Region {i + 1}: {e.Bounds.Width}x{e.Bounds.Height}px");
            if (!string.IsNullOrEmpty(e.OcrPartial))
                sb.Append($" -- OCR partial: \"{e.OcrPartial}\"");
            else
                sb.Append(" -- OCR: (none)");
            sb.AppendLine();
        }

        return (images, sb.ToString());
    }

    /// <summary>
    /// Single composite for backward compatibility.
    /// </summary>
    public (byte[] pngBytes, string prompt) BuildComposite()
    {
        var (images, prompt) = BuildTripleComposite();
        return (images.Count > 0 ? images[0] : Array.Empty<byte>(), prompt);
    }

    private byte[] RenderComposite(List<int> order, string imageLabel)
    {
        int maxW = 0;
        int totalH = 0;
        foreach (var idx in order)
        {
            var e = _entries[idx];
            maxW = Math.Max(maxW, e.Screenshot.Width + Padding * 2);
            totalH += LabelHeight + e.Screenshot.Height + Padding;
        }
        totalH += LabelHeight + Padding; // image label + bottom padding

        using var composite = new Bitmap(maxW, totalH);
        using (var g = Graphics.FromImage(composite))
        {
            g.Clear(Color.FromArgb(30, 30, 30));
            using var font = new Font("Consolas", 10f, FontStyle.Bold);
            using var headerFont = new Font("Consolas", 11f, FontStyle.Bold);
            using var labelBrush = new SolidBrush(Color.FromArgb(0, 200, 255));
            using var headerBrush = new SolidBrush(Color.FromArgb(255, 200, 0)); // gold
            using var borderPen = new Pen(Color.FromArgb(80, 80, 80), 1);

            int y = Padding;
            g.DrawString($"Image {imageLabel}", headerFont, headerBrush, Padding, y);
            y += LabelHeight;

            for (int pos = 0; pos < order.Count; pos++)
            {
                var e = _entries[order[pos]];
                var label = $"[{pos + 1}] {e.Bounds.Width}x{e.Bounds.Height}";
                if (!string.IsNullOrEmpty(e.OcrPartial))
                    label += $"  OCR: \"{e.OcrPartial}\"";

                g.DrawString(label, font, labelBrush, Padding, y);
                y += LabelHeight;
                g.DrawImage(e.Screenshot, Padding, y);
                g.DrawRectangle(borderPen, Padding - 1, y - 1,
                    e.Screenshot.Width + 1, e.Screenshot.Height + 1);
                y += e.Screenshot.Height + Padding;
            }
        }

        using var ms = new MemoryStream();
        composite.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }

    /// <summary>
    /// Parse Vision API response containing 3 image sections (A, B, C).
    /// Returns per-image results: "A" -> {1: "text", 2: "(icon: arrow)"}, etc.
    /// </summary>
    public static Dictionary<string, Dictionary<int, string>> ParseTripleResponse(string response)
    {
        var all = new Dictionary<string, Dictionary<int, string>>();
        string currentImage = "A";
        var currentMap = new Dictionary<int, string>();
        all[currentImage] = currentMap;

        foreach (var line in response.Split('\n'))
        {
            var trimmed = line.Trim();

            // Detect image section header: "Image A:", "Image B:", "Image C:"
            var headerMatch = System.Text.RegularExpressions.Regex.Match(trimmed, @"^Image\s+([A-C])\s*:");
            if (headerMatch.Success)
            {
                currentImage = headerMatch.Groups[1].Value;
                currentMap = new Dictionary<int, string>();
                all[currentImage] = currentMap;
                continue;
            }

            // Match: [N] "text" or [N] (icon: desc) or [N] text
            var entryMatch = System.Text.RegularExpressions.Regex.Match(trimmed, @"^\[(\d+)\]\s*(.+)");
            if (entryMatch.Success && int.TryParse(entryMatch.Groups[1].Value, out int idx))
            {
                var val = entryMatch.Groups[2].Value.Trim().Trim('"');
                currentMap[idx] = val;
            }
        }
        return all;
    }

    /// <summary>
    /// Cross-verify triple results: score each region by consistency.
    /// Returns region index -> (bestText, score%) where:
    ///   100% = 3/3 agree, 67% = 2/3 agree (majority wins), 0% = all different.
    /// </summary>
    public static Dictionary<int, (string text, int score)> CrossVerify(
        Dictionary<string, Dictionary<int, string>> tripleResults)
    {
        var verified = new Dictionary<int, (string text, int score)>();

        // Collect all region indices
        var allIndices = tripleResults.Values.SelectMany(m => m.Keys).Distinct().OrderBy(i => i);

        foreach (var idx in allIndices)
        {
            var answers = tripleResults.Values
                .Where(m => m.ContainsKey(idx))
                .Select(m => m[idx].Trim())
                .ToList();

            if (answers.Count == 0) continue;

            // Group by normalized text (case-insensitive)
            var groups = answers.GroupBy(a => a, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count())
                .ToList();

            var best = groups[0].Key;
            int agreeing = groups[0].Count();
            int total = answers.Count;

            int score = total switch
            {
                3 when agreeing == 3 => 100, // unanimous
                3 when agreeing == 2 => 67,  // majority
                3 when agreeing == 1 => 0,   // all different
                _ => agreeing * 100 / Math.Max(1, total)
            };

            verified[idx] = (best, score);
        }
        return verified;
    }

    public void Dispose()
    {
        foreach (var e in _entries)
            e.Screenshot.Dispose();
        _entries.Clear();
    }
}
