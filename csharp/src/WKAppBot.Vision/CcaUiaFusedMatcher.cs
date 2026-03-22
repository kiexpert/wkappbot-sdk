// CcaUiaFusedMatcher.cs — CCA↔UIA Spatial+Semantic Fused Matching
// Inspired by GPT(weighted score), Gemini(Grid Hash), Claude(Hungarian+Containment).
//
// Pipeline:
//   1. Grid Hash index UIA elements (50px buckets) → O(N) candidate generation
//   2. Asymmetric dilation (±6px) → absorb border/shadow pixel error
//   3. Weighted score: 0.40×IoU + 0.20×contain + 0.15×centerDist + 0.15×sizeDelta + 0.10×rowColAlign
//   4. Greedy 1:1 assignment (sort by score desc, first-come-first-serve)
//   5. Unmatched CCA regions → synthetic UIA nodes (gap filling)

using System.Drawing;

namespace WKAppBot.Vision;

/// <summary>
/// Fused matcher for CCA regions ↔ UIA elements.
/// Returns matched pairs + unmatched CCA (synthetic candidates) + unmatched UIA.
/// </summary>
public sealed class CcaUiaFusedMatcher
{
    const int GridSize = 50;   // px per grid bucket
    const int DilationPx = 6;  // asymmetric dilation for ±5px tolerance
    const double MinScore = 0.15; // minimum match score threshold

    /// <summary>UIA element info for matching (lightweight, no COM dependency).</summary>
    public sealed class UiaInfo
    {
        public string Name { get; init; } = "";
        public string ControlType { get; init; } = "";
        public string AutomationId { get; init; } = "";
        public Rectangle Bounds { get; init; }
    }

    /// <summary>Optional OCR text for CCA regions (set before calling Match).</summary>
    public sealed class CcaTextInfo
    {
        public int RegionIndex { get; init; }
        public string OcrText { get; init; } = "";
    }

    /// <summary>Match result: CCA region paired with UIA element + score.</summary>
    public sealed class MatchResult
    {
        public ConnectedComponentAnalyzer.Region CcaRegion { get; init; } = null!;
        public UiaInfo? UiaElement { get; init; }
        public double Score { get; init; }
        public string MatchType { get; init; } = ""; // "spatial", "synthetic", "unmatched-uia"
    }

    /// <summary>
    /// Run fused matching: CCA regions vs UIA elements.
    /// </summary>
    public static List<MatchResult> Match(
        List<ConnectedComponentAnalyzer.Region> ccaRegions,
        List<UiaInfo> uiaElements,
        List<CcaTextInfo>? ccaTexts = null)
    {
        var results = new List<MatchResult>();
        if (ccaRegions.Count == 0) return results;

        // ── Step 1: Grid Hash index for UIA elements ──
        var grid = new Dictionary<(int, int), List<int>>();
        for (int i = 0; i < uiaElements.Count; i++)
        {
            var b = uiaElements[i].Bounds;
            if (b.Width <= 0 || b.Height <= 0) continue;
            // Insert into all grid cells the element overlaps
            int gx0 = b.Left / GridSize, gy0 = b.Top / GridSize;
            int gx1 = b.Right / GridSize, gy1 = b.Bottom / GridSize;
            for (int gx = gx0; gx <= gx1; gx++)
                for (int gy = gy0; gy <= gy1; gy++)
                {
                    var key = (gx, gy);
                    if (!grid.TryGetValue(key, out var list))
                        grid[key] = list = new List<int>();
                    list.Add(i);
                }
        }

        // ── Step 2: Score each CCA↔UIA candidate pair ──
        var candidates = new List<(int ccaIdx, int uiaIdx, double score)>();
        var uiaUsed = new HashSet<int>();

        for (int ci = 0; ci < ccaRegions.Count; ci++)
        {
            var cr = ccaRegions[ci].Bounds;
            if (cr.Width <= 0 || cr.Height <= 0) continue;

            // Find UIA candidates from grid (with dilation)
            var dilated = Rectangle.Inflate(cr, DilationPx, DilationPx);
            int gx0 = dilated.Left / GridSize, gy0 = dilated.Top / GridSize;
            int gx1 = dilated.Right / GridSize, gy1 = dilated.Bottom / GridSize;

            var candidateUia = new HashSet<int>();
            for (int gx = gx0; gx <= gx1; gx++)
                for (int gy = gy0; gy <= gy1; gy++)
                    if (grid.TryGetValue((gx, gy), out var list))
                        foreach (var idx in list) candidateUia.Add(idx);

            // Get OCR text for this CCA region (if available)
            var ccaOcr = ccaTexts?.FirstOrDefault(t => t.RegionIndex == ci)?.OcrText ?? "";

            foreach (var ui in candidateUia)
            {
                var score = ComputeScore(cr, uiaElements[ui].Bounds);
                // Text similarity bonus (Claude: IoU > 0.1 gate)
                if (score >= 0.1 && !string.IsNullOrEmpty(ccaOcr) && !string.IsNullOrEmpty(uiaElements[ui].Name))
                    score += 0.15 * TextSimilarity(ccaOcr, uiaElements[ui].Name);
                if (score >= MinScore)
                    candidates.Add((ci, ui, score));
            }
        }

        // ── Step 3: Greedy 1:1 assignment (sort by score desc) ──
        candidates.Sort((a, b) => b.score.CompareTo(a.score));
        var ccaMatched = new HashSet<int>();

        foreach (var (ci, ui, score) in candidates)
        {
            if (ccaMatched.Contains(ci) || uiaUsed.Contains(ui)) continue;
            ccaMatched.Add(ci);
            uiaUsed.Add(ui);
            results.Add(new MatchResult
            {
                CcaRegion = ccaRegions[ci],
                UiaElement = uiaElements[ui],
                Score = score,
                MatchType = "spatial"
            });
        }

        // ── Step 4: Unmatched CCA → synthetic (gap filling) ──
        for (int ci = 0; ci < ccaRegions.Count; ci++)
        {
            if (ccaMatched.Contains(ci)) continue;
            results.Add(new MatchResult
            {
                CcaRegion = ccaRegions[ci],
                UiaElement = null,
                Score = 0,
                MatchType = "synthetic"
            });
        }

        // ── Step 5: Unmatched UIA (no CCA counterpart) ──
        for (int ui = 0; ui < uiaElements.Count; ui++)
        {
            if (uiaUsed.Contains(ui)) continue;
            if (uiaElements[ui].Bounds.Width <= 0) continue;
            results.Add(new MatchResult
            {
                CcaRegion = null!,
                UiaElement = uiaElements[ui],
                Score = 0,
                MatchType = "unmatched-uia"
            });
        }

        return results;
    }

    /// <summary>Apply DPI normalization to a rectangle (Claude recommendation).</summary>
    public static Rectangle NormalizeDpi(Rectangle r, double dpiScale)
    {
        if (dpiScale <= 0 || Math.Abs(dpiScale - 1.0) < 0.01) return r;
        return new Rectangle(
            (int)(r.X / dpiScale), (int)(r.Y / dpiScale),
            (int)(r.Width / dpiScale), (int)(r.Height / dpiScale));
    }

    /// <summary>
    /// Weighted spatial score (GPT formula):
    /// 0.40×IoU + 0.20×contain + 0.15×(1-centerDistNorm) + 0.15×(1-sizeDeltaNorm) + 0.10×rowColAlign
    /// With asymmetric dilation: try exact, dilated-CCA, dilated-UIA → max IoU.
    /// </summary>
    static double ComputeScore(Rectangle cca, Rectangle uia)
    {
        // IoU with asymmetric dilation (max of 3 attempts)
        double iou = Math.Max(
            Math.Max(IoU(cca, uia), IoU(Rectangle.Inflate(cca, DilationPx, DilationPx), uia)),
            IoU(cca, Rectangle.Inflate(uia, DilationPx, DilationPx)));

        // Containment: max(intersection/cca.area, intersection/uia.area)
        var inter = Rectangle.Intersect(cca, uia);
        double interArea = Math.Max(0, inter.Width) * Math.Max(0, inter.Height);
        double ccaArea = cca.Width * cca.Height;
        double uiaArea = uia.Width * uia.Height;
        double contain = Math.Max(
            ccaArea > 0 ? interArea / ccaArea : 0,
            uiaArea > 0 ? interArea / uiaArea : 0);

        // Center distance (normalized by union diagonal)
        double cx1 = cca.X + cca.Width / 2.0, cy1 = cca.Y + cca.Height / 2.0;
        double cx2 = uia.X + uia.Width / 2.0, cy2 = uia.Y + uia.Height / 2.0;
        var union = Rectangle.Union(cca, uia);
        double diag = Math.Sqrt(union.Width * union.Width + union.Height * union.Height);
        double centerDist = Math.Sqrt((cx1 - cx2) * (cx1 - cx2) + (cy1 - cy2) * (cy1 - cy2));
        double centerDistNorm = diag > 0 ? Math.Min(1, centerDist / (0.5 * diag)) : 1;

        // Size delta (log ratio)
        double wRatio = cca.Width > 0 && uia.Width > 0 ? Math.Abs(Math.Log((double)cca.Width / uia.Width)) : 1;
        double hRatio = cca.Height > 0 && uia.Height > 0 ? Math.Abs(Math.Log((double)cca.Height / uia.Height)) : 1;
        double sizeDeltaNorm = Math.Min(1, wRatio + hRatio);

        // Row/Column alignment (same Y band within 8px)
        double rowColAlign = Math.Abs(cy1 - cy2) < 8 ? 1.0 : Math.Abs(cx1 - cx2) < 8 ? 0.5 : 0;

        return 0.40 * iou + 0.20 * contain + 0.15 * (1 - centerDistNorm)
             + 0.15 * (1 - sizeDeltaNorm) + 0.10 * rowColAlign;
    }

    /// <summary>
    /// Text similarity: normalized exact → substring → token Jaccard.
    /// Claude formula: exact=1.0, substring=0.85, else 0.6×tokenJaccard + 0.4×editSim.
    /// Simplified for speed: exact/substring/token Jaccard only.
    /// </summary>
    static double TextSimilarity(string a, string b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return 0;
        var na = Normalize(a);
        var nb = Normalize(b);
        if (na == nb) return 1.0;
        if (na.Contains(nb) || nb.Contains(na)) return 0.85;
        // Token Jaccard
        var tokA = new HashSet<string>(na.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var tokB = new HashSet<string>(nb.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        if (tokA.Count == 0 || tokB.Count == 0) return 0;
        int intersect = tokA.Count(t => tokB.Contains(t));
        int union = tokA.Count + tokB.Count - intersect;
        return union > 0 ? (double)intersect / union : 0;
    }

    static string Normalize(string s) =>
        new string(s.ToLowerInvariant().Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray()).Trim();

    static double IoU(Rectangle a, Rectangle b)
    {
        var inter = Rectangle.Intersect(a, b);
        double interArea = Math.Max(0, inter.Width) * Math.Max(0, inter.Height);
        double unionArea = (double)a.Width * a.Height + (double)b.Width * b.Height - interArea;
        return unionArea > 0 ? interArea / unionArea : 0;
    }

    /// <summary>Summary stats for quick display.</summary>
    public static string Summarize(List<MatchResult> results)
    {
        int spatial = results.Count(r => r.MatchType == "spatial");
        int synthetic = results.Count(r => r.MatchType == "synthetic");
        int unmatchedUia = results.Count(r => r.MatchType == "unmatched-uia");
        double avgScore = spatial > 0 ? results.Where(r => r.MatchType == "spatial").Average(r => r.Score) : 0;
        return $"matched={spatial} (avg={avgScore:F2}) synthetic={synthetic} unmatched-uia={unmatchedUia}";
    }
}
