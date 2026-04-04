using System.Collections.Concurrent;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.Json;

namespace WKAppBot.Vision;

/// <summary>
/// Self-learning OCR correction dictionary: pixel-hash → corrected text.
///
/// When SimpleOCR misreads a glyph/region, verified ground truth (from UIA, Vision API,
/// or manual correction) is stored as pixel-hash → correct text. On subsequent OCR of
/// identical pixel patterns, the correction is applied instantly.
///
/// Especially effective for HTS/MFC bitmap fonts where glyphs render identically.
///
/// Storage: {baseDir}/ocr_corrections/{procName}/{className}.jsonl
/// Each line: { "hash":"ab12cd34", "wrong":"8", "correct":"B", "source":"uia", "hits":5, "ts":"..." }
///
/// Usage:
///   var db = OcrCorrectionDb.ForProcess("HeroMts", "AfxWnd140s");
///   var corrected = db.TryCorrect(cropBitmap, ocrText);   // returns corrected or null
///   db.Learn(cropBitmap, wrongText, correctText, "uia");   // save correction
/// </summary>
public sealed class OcrCorrectionDb
{
    readonly string _jsonlPath;
    readonly ConcurrentDictionary<string, CorrectionEntry> _cache = new();
    static readonly ConcurrentDictionary<string, OcrCorrectionDb> _instances = new();

    /// <summary>Get or create a per-process/class correction DB.</summary>
    public static OcrCorrectionDb ForProcess(string baseDir, string procName, string className)
    {
        var key = $"{procName}|{className}";
        return _instances.GetOrAdd(key, _ =>
        {
            var dir = Path.Combine(baseDir, "ocr_corrections", SanitizePath(procName));
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, $"{SanitizePath(className)}.jsonl");
            return new OcrCorrectionDb(path);
        });
    }

    /// <summary>Get correction DB for the default experience directory.</summary>
    public static OcrCorrectionDb ForProcess(string procName, string className)
    {
        var baseDir = Path.Combine(
            Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".",
            "wkappbot.hq", "experience");
        return ForProcess(baseDir, procName, className);
    }

    OcrCorrectionDb(string jsonlPath)
    {
        _jsonlPath = jsonlPath;
        Load();
    }

    /// <summary>
    /// Try to correct OCR text using pixel-hash lookup.
    /// Returns corrected text if a correction exists for this pixel pattern, null otherwise.
    /// Thread-safe.
    /// </summary>
    public string? TryCorrect(Bitmap crop, string ocrText)
    {
        var hash = ComputePixelHash(crop);
        if (_cache.TryGetValue(hash, out var entry))
        {
            // Only correct if the OCR text matches the known-wrong text (or is empty)
            if (string.IsNullOrEmpty(ocrText) || IsSimilar(ocrText, entry.Wrong))
            {
                entry.Hits++;
                entry.LastHitUtc = DateTime.UtcNow;
                return entry.Correct;
            }
        }
        return null;
    }

    /// <summary>
    /// Try to correct OCR text by pixel-hash only (no bitmap — use precomputed hash).
    /// </summary>
    public string? TryCorrect(string pixelHash, string ocrText)
    {
        if (_cache.TryGetValue(pixelHash, out var entry))
        {
            if (string.IsNullOrEmpty(ocrText) || IsSimilar(ocrText, entry.Wrong))
            {
                entry.Hits++;
                entry.LastHitUtc = DateTime.UtcNow;
                return entry.Correct;
            }
        }
        return null;
    }

    /// <summary>
    /// Record a correction: this pixel pattern was misread as wrongText, correct is correctText.
    /// Source: "uia" (UIA verification), "vision" (Gemini/GPT), "manual" (user correction).
    /// Thread-safe.
    /// </summary>
    public void Learn(Bitmap crop, string wrongText, string correctText, string source)
    {
        if (string.Equals(wrongText, correctText, StringComparison.Ordinal)) return;
        var hash = ComputePixelHash(crop);
        LearnByHash(hash, wrongText, correctText, source);
    }

    /// <summary>Record a correction by precomputed hash.</summary>
    public void LearnByHash(string pixelHash, string wrongText, string correctText, string source)
    {
        if (string.Equals(wrongText, correctText, StringComparison.Ordinal)) return;

        var entry = _cache.AddOrUpdate(pixelHash,
            _ => new CorrectionEntry
            {
                Hash = pixelHash, Wrong = wrongText, Correct = correctText,
                Source = source, Hits = 0, CreatedUtc = DateTime.UtcNow, LastHitUtc = DateTime.UtcNow
            },
            (_, existing) =>
            {
                // Update if same wrong→correct mapping, or overwrite with higher-priority source
                if (SourcePriority(source) >= SourcePriority(existing.Source))
                {
                    existing.Wrong = wrongText;
                    existing.Correct = correctText;
                    existing.Source = source;
                    existing.LastHitUtc = DateTime.UtcNow;
                }
                return existing;
            });

        AppendLine(entry);
    }

    /// <summary>Number of stored corrections.</summary>
    public int Count => _cache.Count;

    /// <summary>All entries (for diagnostics).</summary>
    public IEnumerable<CorrectionEntry> Entries => _cache.Values;

    // ── Pixel hash (shared algorithm with OcrGapCollector) ──────────────────

    /// <summary>Fast pixel hash: sample every 4th pixel, MD5 → 8-char hex. Same as OcrGapCollector.</summary>
    public static string ComputePixelHash(Bitmap bmp)
    {
        using var md5 = MD5.Create();
        int step = 4;
        int w = bmp.Width, h = bmp.Height;
        var buf = new byte[((w / step) + 1) * ((h / step) + 1) * 3];
        int bi = 0;
        for (int y = 0; y < h; y += step)
        for (int x = 0; x < w; x += step)
        {
            var c = bmp.GetPixel(x, y);
            if (bi + 2 < buf.Length) { buf[bi++] = c.R; buf[bi++] = c.G; buf[bi++] = c.B; }
        }
        var hash = md5.ComputeHash(buf, 0, bi);
        return Convert.ToHexString(hash)[..8].ToLowerInvariant();
    }

    // ── Persistence ─────────────────────────────────────────────────────────

    void Load()
    {
        if (!File.Exists(_jsonlPath)) return;
        try
        {
            foreach (var line in File.ReadAllLines(_jsonlPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var e = JsonSerializer.Deserialize<CorrectionEntry>(line);
                    if (e != null && !string.IsNullOrEmpty(e.Hash))
                        _cache[e.Hash] = e;
                }
                catch { }
            }
        }
        catch { }
    }

    void AppendLine(CorrectionEntry entry)
    {
        try
        {
            var dir = Path.GetDirectoryName(_jsonlPath);
            if (dir != null) Directory.CreateDirectory(dir);
            var json = JsonSerializer.Serialize(entry);
            File.AppendAllText(_jsonlPath, json + "\n");
        }
        catch { }
    }

    // Compact: rewrite full cache (call periodically or on shutdown)
    public void Compact()
    {
        try
        {
            var lines = _cache.Values.Select(e => JsonSerializer.Serialize(e));
            File.WriteAllLines(_jsonlPath, lines);
        }
        catch { }
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    static bool IsSimilar(string a, string b)
    {
        if (string.Equals(a, b, StringComparison.OrdinalIgnoreCase)) return true;
        if (a.Length == 0 || b.Length == 0) return false;
        // Levenshtein distance ≤ 2 for short strings (MFC bitmap OCR instability)
        if (a.Length <= 4 && b.Length <= 4)
            return LevenshteinDistance(a, b) <= 2;
        // For longer strings, check containment
        return a.Contains(b, StringComparison.OrdinalIgnoreCase)
            || b.Contains(a, StringComparison.OrdinalIgnoreCase);
    }

    static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length, m = t.Length;
        var d = new int[n + 1, m + 1];
        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;
        for (int i = 1; i <= n; i++)
        for (int j = 1; j <= m; j++)
        {
            int cost = s[i - 1] == t[j - 1] ? 0 : 1;
            d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
        }
        return d[n, m];
    }

    static int SourcePriority(string source) => source switch
    {
        "manual" => 3,  // user correction = highest
        "uia" => 2,     // UIA verified
        "vision" => 1,  // Gemini/GPT
        _ => 0
    };

    static string SanitizePath(string s)
    {
        var invalid = Path.GetInvalidFileNameChars();
        return new string(s.Select(c => Array.IndexOf(invalid, c) >= 0 ? '_' : c).ToArray());
    }

    // ── Data ────────────────────────────────────────────────────────────────

    public sealed class CorrectionEntry
    {
        public string Hash { get; set; } = "";
        public string Wrong { get; set; } = "";
        public string Correct { get; set; } = "";
        public string Source { get; set; } = "";  // "uia", "vision", "manual"
        public int Hits { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastHitUtc { get; set; }
    }
}
