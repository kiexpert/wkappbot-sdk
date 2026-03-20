using System.Drawing;
using System.Drawing.Imaging;

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

    private record GapEntry(Rectangle Bounds, string? OcrPartial, Bitmap Screenshot);

    public bool HasGaps => _entries.Count > 0;
    public int Count => _entries.Count;

    /// <summary>
    /// Capture and store a gap screenshot.
    /// </summary>
    /// <param name="screenRect">Element bounding rect in screen coordinates</param>
    /// <param name="ocrPartial">Partial OCR text with [?] markers, or null if fully blind</param>
    public void Add(Rectangle screenRect, string? ocrPartial)
    {
        try
        {
            if (screenRect.Width <= 0 || screenRect.Height <= 0) return;
            var bmp = new Bitmap(screenRect.Width, screenRect.Height);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(screenRect.X, screenRect.Y, 0, 0, screenRect.Size);
            _entries.Add(new GapEntry(screenRect, ocrPartial, bmp));
        }
        catch { /* best effort */ }
    }

    /// <summary>
    /// Build 3 composite images (forward, reverse, shuffle) + prompt for cross-verification.
    /// Returns (list of PNG bytes, prompt text for Vision API).
    /// Gemini sees all 3 in one request — inconsistent answers = suspect.
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
        sb.AppendLine("Do NOT copy answers between images — read each one fresh.");
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
                sb.Append($" — OCR partial: \"{e.OcrPartial}\"");
            else
                sb.Append(" — OCR: (none)");
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
    /// Returns per-image results: "A" → {1: "text", 2: "(icon: arrow)"}, etc.
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
    /// Returns region index → (bestText, score%) where:
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
