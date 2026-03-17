using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Vision;

/// <summary>
/// A single OCR-recognized text segment — one logical UI element from vision.
///
/// Built from OcrEngine line results: each line becomes one segment.
/// Can also be populated by Vision API for unreadable blobs.
/// Stored with relative coordinates (0.0-1.0) so cache survives window moves.
/// </summary>
public sealed class OcrSegment
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";

    /// <summary>Center X relative to form (0.0-1.0)</summary>
    [JsonPropertyName("rel_x")]
    public double RelX { get; set; }

    /// <summary>Center Y relative to form (0.0-1.0)</summary>
    [JsonPropertyName("rel_y")]
    public double RelY { get; set; }

    /// <summary>Width ratio (0.0-1.0)</summary>
    [JsonPropertyName("rel_w")]
    public double RelW { get; set; }

    /// <summary>Height ratio (0.0-1.0)</summary>
    [JsonPropertyName("rel_h")]
    public double RelH { get; set; }

    /// <summary>OCR confidence (0.0-1.0)</summary>
    [JsonPropertyName("conf")]
    public double Confidence { get; set; } = 0.9;

    /// <summary>"ocr" | "vision_api" | "gemini"</summary>
    [JsonPropertyName("src")]
    public string Source { get; set; } = "ocr";

    /// <summary>Control type if known from Vision API ("Button", "Label", "TextBox", ...)</summary>
    [JsonPropertyName("ctrl")]
    public string? ControlType { get; set; }
}

/// <summary>
/// Cached segment list for one form at a specific window size.
/// Invalidated when the form screenshot hash changes (UI update detected).
/// </summary>
public sealed class OcrSegmentCacheEntry
{
    /// <summary>MD5 of 32x32 downscaled form screenshot — change = UI update</summary>
    [JsonPropertyName("form_hash")]
    public string FormHash { get; set; } = "";

    [JsonPropertyName("build_at")]
    public DateTime BuildAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("win_w")]
    public int WindowWidth { get; set; }

    [JsonPropertyName("win_h")]
    public int WindowHeight { get; set; }

    [JsonPropertyName("segs")]
    public List<OcrSegment> Segments { get; set; } = new();
}

/// <summary>
/// Form-level OCR segment cache — the "dynamic a11y tree" generated from vision.
///
/// Core concept:
///   Run RecognizeAll ONCE per form → build segment list → save to disk.
///   Next element lookup: text search in cached segments (no OCR re-run).
///   Form hash mismatch → automatic cache invalidation + rebuild.
///
/// This is Tier 2.5 in the vision pipeline:
///   UIA → VisionCache → OcrSegCache → (seg miss) → VisionAPI
///
/// Vision API results are also stored here (source="vision_api" | "gemini")
/// so all elements of a form accumulate over time.
///
/// Storage: {cacheDir}/ocr_segs/{classHash}_{winW}x{winH}.json
/// TTL:     7 days (or form hash mismatch → immediate rebuild)
/// </summary>
public sealed class OcrSegmentCache
{
    private static readonly TimeSpan Ttl = TimeSpan.FromDays(7);
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly string _cacheDir;

    public OcrSegmentCache(string cacheDir)
    {
        _cacheDir = cacheDir;
        Directory.CreateDirectory(Path.Combine(cacheDir, "ocr_segs"));
    }

    // ── Storage ─────────────────────────────────────────────────────────

    private string CachePath(string classPath, int winW, int winH)
    {
        var hash = ClassHash(classPath);
        return Path.Combine(_cacheDir, "ocr_segs", $"{hash}_{winW}x{winH}.json");
    }

    public OcrSegmentCacheEntry? Load(string classPath, int winW, int winH)
    {
        var path = CachePath(classPath, winW, winH);
        if (!File.Exists(path)) return null;
        try
        {
            var age = DateTime.UtcNow - File.GetLastWriteTimeUtc(path);
            if (age > Ttl) { File.Delete(path); return null; }
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<OcrSegmentCacheEntry>(json, JsonOpts);
        }
        catch { return null; }
    }

    public void Save(string classPath, int winW, int winH, OcrSegmentCacheEntry entry)
    {
        var path = CachePath(classPath, winW, winH);
        try
        {
            var json = JsonSerializer.Serialize(entry, JsonOpts);
            File.WriteAllText(path, json);
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Add a Vision API / Gemini-sourced segment to the cached entry for this form.
    /// Call after Vision API identifies an element OCR couldn't read.
    /// </summary>
    public void LearnSegment(string classPath, string formHash, int winW, int winH, OcrSegment seg)
    {
        var entry = Load(classPath, winW, winH);
        if (entry == null || entry.FormHash != formHash) return;  // stale — don't pollute

        // Replace existing segment with same approximate position (update source→vision)
        var existing = entry.Segments.FirstOrDefault(s =>
            Math.Abs(s.RelX - seg.RelX) < 0.03 && Math.Abs(s.RelY - seg.RelY) < 0.03);
        if (existing != null)
            entry.Segments.Remove(existing);
        entry.Segments.Add(seg);
        Save(classPath, winW, winH, entry);
    }

    // ── Query ────────────────────────────────────────────────────────────

    /// <summary>
    /// Find the best matching element for a description in the cached segment list.
    ///
    /// Returns null if:
    ///   - No cache file exists
    ///   - Form hash mismatch (UI changed since last build) — caller should rebuild
    ///   - No segment matches the description above threshold
    /// </summary>
    public OcrSegment? FindElement(string classPath, string formHash, int winW, int winH, string description)
    {
        var entry = Load(classPath, winW, winH);
        if (entry == null) return null;
        if (entry.FormHash != formHash) return null;  // stale → caller rebuilds

        return BestMatch(entry.Segments, description);
    }

    /// <summary>
    /// Check whether cached segments exist and are fresh (matching form hash).
    /// Returns null = no cache; non-null = entry exists (may still have no match).
    /// </summary>
    public OcrSegmentCacheEntry? LoadIfFresh(string classPath, string formHash, int winW, int winH)
    {
        var entry = Load(classPath, winW, winH);
        if (entry == null || entry.FormHash != formHash) return null;
        return entry;
    }

    /// <summary>
    /// Search a segment list for the best text match to a description.
    /// Minimum score threshold: 0.4.
    /// </summary>
    public static OcrSegment? BestMatch(IEnumerable<OcrSegment> segments, string description)
    {
        var desc = description.Trim();
        OcrSegment? best = null;
        double bestScore = 0.4;

        foreach (var seg in segments)
        {
            var score = SimpleOcrAnalyzer.MatchScore(seg.Text, desc);
            if (score > bestScore)
            {
                bestScore = score;
                best = seg;
            }
        }
        return best;
    }

    // ── Blob image store ─────────────────────────────────────────────────

    /// <summary>
    /// Look up a bitmap crop in the blob store by pixel hash.
    /// Returns the previously-identified label if found, null if unseen.
    ///
    /// Same pixel pattern → same hash → instant label without any AI query.
    /// Companion .txt (if exists) holds the full label when the filename was truncated.
    /// </summary>
    public string? LookupBlob(System.Drawing.Bitmap crop)
    {
        var hash = ComputeBitmapHash(crop);
        var blobDir = Path.Combine(_cacheDir, "ocr_segs", "blobs");
        if (!Directory.Exists(blobDir)) return null;

        var matches = Directory.GetFiles(blobDir, $"{hash}=*.png");
        if (matches.Length == 0) return null;

        var baseName = Path.GetFileNameWithoutExtension(matches[0]);
        var sep = baseName.IndexOf('=');
        if (sep < 0) return null;
        var safeLabel = baseName[(sep + 1)..];

        // Check companion .txt for full unsanitized label
        var txtPath = Path.Combine(blobDir, $"{baseName}.txt");
        if (File.Exists(txtPath))
        {
            try { return File.ReadAllText(txtPath, Encoding.UTF8).Trim(); } catch { }
        }
        return safeLabel;
    }

    /// <summary>
    /// Save a Vision/Gemini-identified element crop as {pixelHash}={safeLabel}.png.
    /// Filename acts as a lookup key: same pixel pattern → same hash → fast retrieval.
    ///
    /// If the label contains filename-unsafe chars or exceeds 60 chars,
    /// a companion {pixelHash}={safeLabel}.txt is saved with the full original label.
    ///
    /// Storage: {cacheDir}/ocr_segs/blobs/
    /// Returns saved PNG path, or null on failure.
    /// </summary>
    public string? SaveBlob(System.Drawing.Bitmap crop, string label)
    {
        try
        {
            var blobDir = Path.Combine(_cacheDir, "ocr_segs", "blobs");
            Directory.CreateDirectory(blobDir);

            var pixelHash = ComputeBitmapHash(crop);
            var safeLabel = MakeSafeLabel(label);
            var pngPath = Path.Combine(blobDir, $"{pixelHash}={safeLabel}.png");

            if (!File.Exists(pngPath))
                crop.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);

            // Companion .txt when label was sanitized or truncated
            if (safeLabel != label)
            {
                var txtPath = Path.Combine(blobDir, $"{pixelHash}={safeLabel}.txt");
                if (!File.Exists(txtPath))
                    File.WriteAllText(txtPath, label, Encoding.UTF8);
            }

            return pngPath;
        }
        catch { return null; }
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private static string ClassHash(string classPath)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(classPath));
        return Convert.ToHexString(bytes)[..16].ToLowerInvariant();
    }

    /// <summary>
    /// Pixel hash of a bitmap — samples first 4 KB of raw pixel data for speed.
    /// Identical visual blobs → same hash (fast lookup key).
    /// </summary>
    private static string ComputeBitmapHash(System.Drawing.Bitmap bmp)
    {
        var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
        var data = bmp.LockBits(rect,
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        int byteCount = Math.Min(data.Stride * bmp.Height, 4096);  // sample up to 4 KB
        var bytes = new byte[byteCount];
        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, byteCount);
        bmp.UnlockBits(data);
        return Convert.ToHexString(MD5.HashData(bytes))[..16].ToLowerInvariant();
    }

    /// <summary>
    /// Sanitize a label for use as a filename component.
    /// Invalid chars → '_', max 60 chars, companion .txt when truncated/changed.
    /// </summary>
    private static string MakeSafeLabel(string label)
    {
        var invalid = Path.GetInvalidFileNameChars().ToHashSet();
        // Also exclude '=' since we use it as separator
        invalid.Add('=');

        var sb = new StringBuilder();
        foreach (var c in label)
            sb.Append(invalid.Contains(c) ? '_' : c);

        var safe = sb.ToString().Trim('_', ' ');
        if (safe.Length > 60) safe = safe[..60].TrimEnd('_', ' ');
        return string.IsNullOrEmpty(safe) ? "unknown" : safe;
    }
}
