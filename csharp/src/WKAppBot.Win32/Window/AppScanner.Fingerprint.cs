using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WKAppBot.Win32.Input;

namespace WKAppBot.Win32.Window;

public static partial class AppScanner
{
    // -- Structural Fingerprint + OCR Keywords ------------------

    /// <summary>
    /// Generate a structural fingerprint from a form's control list.
    /// Each control -> "{NormalizedClass}:{Cid}:{SizeBucket}:{PosBucket}" token.
    /// Tokens sorted -> joined -> SHA256 hash (first 16 hex chars).
    ///
    /// The fingerprint captures the STABLE structure of a form type:
    ///   - ClassName normalized (AfxWnd110/140 -> "AfxWnd", Afx:hex... -> "Afx:*")
    ///   - ControlId (stable per form type)
    ///   - SizeBucket (XS/S/M/L/XL based on area)
    ///   - PosBucket (TL/TC/TR/ML/MC/MR/BL/BC/BR based on relative position)
    /// </summary>
    public static (string fingerprint, string fingerprintHash) GenerateFingerprint(
        IReadOnlyList<ControlExperience> controls)
    {
        var tokens = new List<string>();

        foreach (var ctrl in controls)
        {
            var normalizedClass = NormalizeClassName(ctrl.ClassName ?? "?");
            var sizeBucket = CategorizeSizeArea(ctrl.Width * ctrl.Height);
            var posBucket = CategorizePosition(ctrl.RelativeX, ctrl.RelativeY);

            tokens.Add($"{normalizedClass}:{ctrl.ControlId}:{sizeBucket}:{posBucket}");
        }

        tokens.Sort(StringComparer.Ordinal);
        var fingerprint = string.Join("\n", tokens);

        // SHA256 -> first 16 hex chars
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(fingerprint));
        var fingerprintHash = Convert.ToHexString(hashBytes)[..16].ToLowerInvariant();

        return (fingerprint, fingerprintHash);
    }

    /// <summary>
    /// Refine OCR keywords for a form type.
    /// First scan (scan_count==1): all OCR text words become keyword candidates.
    /// Subsequent scans: intersect with existing keywords -> filter out dynamic data.
    /// </summary>
    public static List<string> RefineOcrKeywords(FormExperience form)
    {
        // Collect all current OCR text words from controls
        var currentWords = new HashSet<string>(StringComparer.Ordinal);
        foreach (var ctrl in form.Controls)
        {
            if (string.IsNullOrWhiteSpace(ctrl.OcrText)) continue;

            foreach (var word in SplitOcrWords(ctrl.OcrText))
            {
                if (IsKeywordCandidate(word))
                    currentWords.Add(word);
            }
        }

        if (form.OcrKeywords == null || form.OcrKeywords.Count == 0 || form.ScanCount <= 1)
        {
            // First scan: all words are candidates
            return currentWords.OrderBy(w => w, StringComparer.Ordinal).ToList();
        }

        // Subsequent scans: intersect with existing keywords
        var refined = form.OcrKeywords
            .Where(kw => currentWords.Contains(kw))
            .ToList();

        return refined;
    }

    /// <summary>
    /// Normalize class name for fingerprint comparison.
    /// MFC version numbers and session-unique hex IDs are stripped.
    /// </summary>
    internal static string NormalizeClassName(string className)
    {
        // AfxWnd110, AfxWnd140, AfxWnd70s etc. -> "AfxWnd"
        if (Regex.IsMatch(className, @"^AfxWnd\d+[su]?$"))
            return "AfxWnd";

        // Afx:00E80000:b:00010005:... -> "Afx:*" (session-unique identifiers)
        if (className.StartsWith("Afx:", StringComparison.Ordinal))
            return "Afx:*";

        // AfxFrameOrView, AfxMDIFrame, etc. -> keep as-is (stable class names)
        return className;
    }

    /// <summary>Categorize area (width×height) into size buckets.</summary>
    internal static string CategorizeSizeArea(int area)
    {
        return area switch
        {
            < 1_000 => "XS",      // tiny icons, small buttons
            < 5_000 => "S",       // normal buttons
            < 20_000 => "M",      // large buttons, input fields
            < 100_000 => "L",     // table sections, partial charts
            _ => "XL",            // main charts, grids
        };
    }

    /// <summary>Categorize position (0.0~1.0) into 9-grid buckets.</summary>
    internal static string CategorizePosition(double relX, double relY)
    {
        var col = relX switch
        {
            < 0.333 => "L",
            < 0.666 => "C",
            _ => "R",
        };
        var row = relY switch
        {
            < 0.333 => "T",
            < 0.666 => "M",
            _ => "B",
        };
        return $"{row}{col}";
    }

    /// <summary>Split OCR text into individual words for keyword analysis.</summary>
    private static IEnumerable<string> SplitOcrWords(string ocrText)
    {
        // Split by whitespace, commas, and common separators
        return Regex.Split(ocrText, @"[\s,;:|\[\](){}]+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .Select(w => w.Trim());
    }

    /// <summary>Is this word a good keyword candidate? Filters out dynamic data.</summary>
    private static bool IsKeywordCandidate(string word)
    {
        // Too short (single char often noise)
        if (word.Length < 2) return false;

        // Pure numbers (prices, quantities) -> dynamic
        if (Regex.IsMatch(word, @"^[\d.,+\-]+$")) return false;

        // Time patterns -> dynamic
        if (Regex.IsMatch(word, @"^\d{1,2}:\d{2}")) return false;

        // Stock code patterns (4-8 digits, optional letter prefix) -> dynamic
        if (Regex.IsMatch(word, @"^[A-Z]?\d{4,8}$")) return false;

        // Percentage -> dynamic
        if (Regex.IsMatch(word, @"^\d+\.?\d*[%t]$")) return false;

        return true;
    }

    // -- Pretty-print ----------------------------------------─

    /// <summary>
    /// Format the scan result as a human-readable summary string.
    /// </summary>
    public static string FormatSummary(AppScanResult result, string? profileName = null)
    {
        var sb = new StringBuilder(2048);

        // Header
        sb.AppendLine($"=== {result.WindowTitle} -- App Scan ===");
        sb.AppendLine($"Class: {result.WindowClass}  Process: {result.ProcessName} (PID:{result.ProcessId})");
        sb.AppendLine($"Size: {result.Rect.Width}x{result.Rect.Height}");
        if (profileName != null)
            sb.AppendLine($"Profile: {profileName}");
        sb.AppendLine();

        // Zones (skip forms -- they're in MDI section)
        foreach (var z in result.Zones)
        {
            if (z.Zone.Type == ZoneType.Form) continue; // forms shown in MDI section

            var sizeStr = z.Rect.Width > 0 ? $"({z.Rect.Width}x{z.Rect.Height})" : "(0x0)";
            var vis = z.IsVisible ? "" : " [hidden]";
            var titleStr = !string.IsNullOrEmpty(z.Title) ? $"\"{z.Title}\"" : "";

            sb.AppendLine($"  [{z.Zone.Tag,-10}] cid={z.ControlId,-8} {sizeStr,-14} {titleStr}{vis}");

            // Notable children (inputs, webviews found inside bars)
            foreach (var nc in z.NotableChildren)
            {
                var ncTitle = !string.IsNullOrEmpty(nc.Title) ? $"\"{nc.Title}\"" : "";
                sb.AppendLine($"    [{nc.Zone.Tag,-8}] [{nc.ClassName}] cid={nc.ControlId} {ncTitle}");
            }
        }

        // MDI Forms
        if (result.Forms.Count > 0)
        {
            sb.AppendLine();
            sb.AppendLine($"-- MDI Forms ({result.Forms.Count}) ------------------------");

            // Group by form_id
            var groups = result.Forms
                .GroupBy(f => f.FormId ?? "???")
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key);

            foreach (var g in groups)
            {
                var first = g.First();
                var formLabel = first.FormId != null
                    ? $"[{first.FormId}] {first.FormName ?? "?"}"
                    : first.Title;

                var stockCodes = g
                    .Where(f => f.StockCode != null)
                    .Select(f => f.StockCode!)
                    .Distinct()
                    .ToList();

                var stockStr = stockCodes.Count > 0
                    ? " {" + string.Join(", ", stockCodes) + "}"
                    : "";

                var countStr = g.Count() > 1 ? $" x{g.Count()}" : "";

                sb.AppendLine($"  [form] {formLabel,-30}{countStr}{stockStr}");
            }
        }

        // Count service windows
        int serviceCount = result.Zones.Count(z => z.Zone.Type == ZoneType.Service);
        if (serviceCount > 0)
        {
            sb.AppendLine();
            sb.AppendLine($"  ({serviceCount} background service windows)");
        }

        return sb.ToString();
    }
}
