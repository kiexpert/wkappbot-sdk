using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WKAppBot.Vision;

/// <summary>
/// Connected Component Analysis (CCA) for owner-drawn Win32/MFC controls.
/// Finds individual glyphs, icons, and UI elements in a screenshot region.
///
/// Pipeline:
///   1. Grayscale conversion
///   2. Adaptive threshold (Sauvola-like: local mean + std dev)
///   3. Connected component labeling (two-pass union-find)
///   4. Component classification: text / icon / noise / separator
///   5. Merge nearby text components into word groups
///
/// Usage:
///   var cca = new ConnectedComponentAnalyzer();
///   var regions = cca.Analyze(screenshot);
///   foreach (var r in regions)
///       Console.WriteLine($"{r.Type}: rect={r.Bounds} pixels={r.PixelCount}");
/// </summary>
public sealed class ConnectedComponentAnalyzer
{
    /// <summary>Region type classification.</summary>
    public enum RegionType { Text, Icon, Noise, Separator, Container }

    /// <summary>A detected region with bounding rect and classification.</summary>
    public sealed class Region
    {
        public Rectangle Bounds { get; init; }
        public RegionType Type { get; init; }
        public int PixelCount { get; init; }
        public int Perimeter { get; init; }        // edge pixel count (border detection)
        public int Label { get; init; }
        public double AspectRatio => Bounds.Height == 0 ? 0 : (double)Bounds.Width / Bounds.Height;
        public double Density => Bounds.Width * Bounds.Height == 0 ? 0
            : (double)PixelCount / (Bounds.Width * Bounds.Height);
        /// <summary>Perimeter/Area ratio — high = thin border, low = solid fill.</summary>
        public double Thinness => PixelCount == 0 ? 0 : (double)Perimeter / PixelCount;
    }

    // Adaptive threshold parameters
    private const int BlockSize = 15;       // local window size (must be odd)
    private const double K = 0.2;           // Sauvola sensitivity (0.0-0.5)
    private const int MinComponentPixels = 4;  // ignore tiny noise

    /// <summary>Optional tuned parameters (from CcaParameterTuner). Null = use defaults.</summary>
    public CcaParams? TunedParams { get; set; }

    /// <summary>
    /// Analyze a screenshot region: find all connected components and classify them.
    /// </summary>
    public List<Region> Analyze(Bitmap bmp)
    {
        int w = bmp.Width, h = bmp.Height;
        var gray = ToGrayscale(bmp, w, h);
        var binary = AdaptiveThreshold(gray, w, h);
        var (labels, count) = LabelComponents(binary, w, h);
        var regions = ExtractRegions(labels, w, h, count);
        return Classify(regions, w, h);
    }

    /// <summary>
    /// Analyze and merge nearby text components into word groups.
    /// Returns merged text regions + standalone icon/separator regions.
    /// </summary>
    public List<Region> AnalyzeAndMerge(Bitmap bmp, int mergeGapX = 5, int mergeGapY = 3)
    {
        var regions = Analyze(bmp);
        var textRegions = regions.Where(r => r.Type == RegionType.Text).OrderBy(r => r.Bounds.Y).ThenBy(r => r.Bounds.X).ToList();
        var others = regions.Where(r => r.Type != RegionType.Text).ToList();

        // Merge text regions that are close horizontally and on the same line
        var merged = new List<Region>();
        var used = new bool[textRegions.Count];
        for (int i = 0; i < textRegions.Count; i++)
        {
            if (used[i]) continue;
            var group = textRegions[i].Bounds;
            int totalPixels = textRegions[i].PixelCount;
            used[i] = true;

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int j = i + 1; j < textRegions.Count; j++)
                {
                    if (used[j]) continue;
                    var r = textRegions[j].Bounds;
                    // Same line: vertical overlap and horizontal gap within threshold
                    bool sameLine = r.Top < group.Bottom + mergeGapY && r.Bottom > group.Top - mergeGapY;
                    bool closeH = r.Left < group.Right + mergeGapX && r.Right > group.Left - mergeGapX;
                    if (sameLine && closeH)
                    {
                        group = Rectangle.Union(group, r);
                        totalPixels += textRegions[j].PixelCount;
                        used[j] = true;
                        changed = true;
                    }
                }
            }
            merged.Add(new Region { Bounds = group, Type = RegionType.Text, PixelCount = totalPixels, Label = -1 });
        }

        merged.AddRange(others);
        return merged.OrderBy(r => r.Bounds.Y).ThenBy(r => r.Bounds.X).ToList();
    }

    // ── Grayscale ──────────────────────────────────────────────────────────

    private static byte[] ToGrayscale(Bitmap bmp, int w, int h)
    {
        var gray = new byte[w * h];
        var data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            var stride = data.Stride;
            var buf = new byte[stride * h];
            Marshal.Copy(data.Scan0, buf, 0, buf.Length);
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                int off = y * stride + x * 4;
                // ITU-R BT.601 luma
                gray[y * w + x] = (byte)(buf[off + 2] * 0.299 + buf[off + 1] * 0.587 + buf[off] * 0.114);
            }
        }
        finally { bmp.UnlockBits(data); }
        return gray;
    }

    // ── Adaptive Threshold (Sauvola-like) ──────────────────────────────────

    private static bool[] AdaptiveThreshold(byte[] gray, int w, int h)
    {
        var binary = new bool[w * h]; // true = foreground (dark pixel)

        // Integral image + integral squared for fast local mean/stddev
        var integral = new long[w * h];
        var integralSq = new long[w * h];
        for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            int idx = y * w + x;
            long val = gray[idx];
            integral[idx] = val
                + (x > 0 ? integral[idx - 1] : 0)
                + (y > 0 ? integral[idx - w] : 0)
                - (x > 0 && y > 0 ? integral[idx - w - 1] : 0);
            integralSq[idx] = val * val
                + (x > 0 ? integralSq[idx - 1] : 0)
                + (y > 0 ? integralSq[idx - w] : 0)
                - (x > 0 && y > 0 ? integralSq[idx - w - 1] : 0);
        }

        int half = BlockSize / 2;
        for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            int x0 = Math.Max(0, x - half), y0 = Math.Max(0, y - half);
            int x1 = Math.Min(w - 1, x + half), y1 = Math.Min(h - 1, y + half);
            int count = (x1 - x0 + 1) * (y1 - y0 + 1);

            long sum = integral[y1 * w + x1]
                - (x0 > 0 ? integral[y1 * w + x0 - 1] : 0)
                - (y0 > 0 ? integral[(y0 - 1) * w + x1] : 0)
                + (x0 > 0 && y0 > 0 ? integral[(y0 - 1) * w + x0 - 1] : 0);
            long sumSq = integralSq[y1 * w + x1]
                - (x0 > 0 ? integralSq[y1 * w + x0 - 1] : 0)
                - (y0 > 0 ? integralSq[(y0 - 1) * w + x1] : 0)
                + (x0 > 0 && y0 > 0 ? integralSq[(y0 - 1) * w + x0 - 1] : 0);

            double mean = (double)sum / count;
            double variance = (double)sumSq / count - mean * mean;
            double stddev = Math.Sqrt(Math.Max(0, variance));
            double threshold = mean * (1.0 + K * (stddev / 128.0 - 1.0));

            binary[y * w + x] = gray[y * w + x] < threshold;
        }
        return binary;
    }

    // ── Two-pass Connected Component Labeling (union-find) ─────────────────

    private static (int[] labels, int count) LabelComponents(bool[] binary, int w, int h)
    {
        var labels = new int[w * h];
        var parent = new int[w * h / 2 + 1]; // union-find
        int nextLabel = 1;

        int Find(int x) { while (parent[x] != x) x = parent[x] = parent[parent[x]]; return x; }
        void Union(int a, int b) { a = Find(a); b = Find(b); if (a != b) parent[Math.Max(a, b)] = Math.Min(a, b); }

        // Pass 1: assign provisional labels
        for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            int idx = y * w + x;
            if (!binary[idx]) continue;

            int left = x > 0 ? labels[idx - 1] : 0;
            int up   = y > 0 ? labels[idx - w] : 0;

            if (left == 0 && up == 0)
            {
                labels[idx] = nextLabel;
                parent[nextLabel] = nextLabel;
                nextLabel++;
                if (nextLabel >= parent.Length) Array.Resize(ref parent, parent.Length * 2);
            }
            else if (left != 0 && up == 0) labels[idx] = left;
            else if (left == 0 && up != 0) labels[idx] = up;
            else
            {
                labels[idx] = Math.Min(left, up);
                if (left != up) Union(left, up);
            }
        }

        // Pass 2: flatten labels
        var remap = new int[nextLabel];
        int finalCount = 0;
        for (int i = 1; i < nextLabel; i++)
        {
            int root = Find(i);
            if (remap[root] == 0) remap[root] = ++finalCount;
            remap[i] = remap[root];
        }
        for (int i = 0; i < labels.Length; i++)
            if (labels[i] > 0) labels[i] = remap[labels[i]];

        return (labels, finalCount);
    }

    // ── Extract bounding rects per component ───────────────────────────────

    private static List<Region> ExtractRegions(int[] labels, int w, int h, int count)
    {
        var minX = new int[count + 1];
        var minY = new int[count + 1];
        var maxX = new int[count + 1];
        var maxY = new int[count + 1];
        var pixels = new int[count + 1];
        var perim = new int[count + 1]; // perimeter: pixels with at least one non-labeled neighbor
        Array.Fill(minX, int.MaxValue);
        Array.Fill(minY, int.MaxValue);

        for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            int lbl = labels[y * w + x];
            if (lbl <= 0) continue;
            if (x < minX[lbl]) minX[lbl] = x;
            if (y < minY[lbl]) minY[lbl] = y;
            if (x > maxX[lbl]) maxX[lbl] = x;
            if (y > maxY[lbl]) maxY[lbl] = y;
            pixels[lbl]++;

            // Perimeter: has any 4-connected neighbor that's different label or edge
            bool edge = x == 0 || x == w - 1 || y == 0 || y == h - 1
                || labels[(y-1)*w+x] != lbl || labels[(y+1)*w+x] != lbl
                || labels[y*w+x-1] != lbl || labels[y*w+x+1] != lbl;
            if (edge) perim[lbl]++;
        }

        var regions = new List<Region>();
        for (int i = 1; i <= count; i++)
        {
            if (pixels[i] < MinComponentPixels) continue;
            regions.Add(new Region
            {
                Bounds = new Rectangle(minX[i], minY[i], maxX[i] - minX[i] + 1, maxY[i] - minY[i] + 1),
                PixelCount = pixels[i],
                Perimeter = perim[i],
                Label = i,
                Type = RegionType.Text, // classified later
            });
        }
        return regions;
    }

    // ── Classification ─────────────────────────────────────────────────────

    private List<Region> Classify(List<Region> regions, int imgW, int imgH)
    {
        return regions.Select(r =>
        {
            var type = ClassifyOne(r, imgW, imgH);
            return new Region { Bounds = r.Bounds, PixelCount = r.PixelCount, Label = r.Label, Type = type };
        }).ToList();
    }

    private RegionType ClassifyOne(Region r, int imgW, int imgH)
    {
        int w = r.Bounds.Width, h = r.Bounds.Height;
        var p = TunedParams; // null = defaults

        // Tiny: noise
        int noiseMax = p?.NoiseMaxPixels ?? MinComponentPixels;
        if (w < 3 || h < 3 || r.PixelCount <= noiseMax) return RegionType.Noise;

        // Separator: very elongated horizontal or vertical line
        if (w > imgW * 0.6 && h <= 3) return RegionType.Separator;
        if (h > imgH * 0.6 && w <= 3) return RegionType.Separator;

        // Container: round border / frame — high thinness (mostly perimeter, little fill)
        // Large component with perimeter/area ratio > 0.6 = hollow frame
        if (r.Thinness > 0.6 && (w > 20 || h > 20) && r.Density < 0.3)
            return RegionType.Container;

        // Text: tunable thresholds
        double minDensity = p?.TextMinDensity ?? 0.10;
        double maxDensity = p?.TextMaxDensity ?? 0.85;
        double minAR = p?.TextMinAR ?? 0.20;
        double maxAR = p?.TextMaxAR ?? 5.0;
        int maxSize = p?.TextMaxSize ?? 80;

        double ar = r.AspectRatio;
        if (ar >= minAR && ar <= maxAR && r.Density >= minDensity && r.Density <= maxDensity
            && w <= maxSize && h <= maxSize)
            return RegionType.Text;

        // Icon: tunable min size
        int iconMin = p?.IconMinSize ?? 8;
        if (w >= iconMin && h >= iconMin && r.Density > 0.05)
            return RegionType.Icon;

        return RegionType.Noise;
    }
}
