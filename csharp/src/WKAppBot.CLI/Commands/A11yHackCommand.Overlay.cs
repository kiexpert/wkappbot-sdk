// A11yHackCommand.Overlay.cs — Overlay preview, box building, caching, text helpers
// Split from A11yHackCommand.cs for maintainability (~320 lines)

using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Experience DB state (shared between main pipeline, CacheSegment, SavePreview) --
    static string? _hackExpDir;          // set once per hack run
    static string? _currentContainerDir; // current container subfolder

    static void SaveHackOverlayPreview(
        Bitmap source,
        List<ConnectedComponentAnalyzer.Region> regions,
        string stage,
        int screenLeft,
        int screenTop,
        Dictionary<int, (string name, string type, string aid)>? uiaAnswers = null,
        Dictionary<int, string>? stageLabels = null)
    {
        try
        {
            var baseDir = _hackExpDir ?? Path.Combine(DataDir, "hack-preview");
            Directory.CreateDirectory(baseDir);

            using var overlay = new Bitmap(source);
            using var g = Graphics.FromImage(overlay);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            using var font = new Font("Consolas", 9, FontStyle.Bold);
            using var textBg = new SolidBrush(Color.FromArgb(190, 15, 20, 28));
            using var textFg = new SolidBrush(Color.White);

            for (int i = 0; i < regions.Count; i++)
            {
                var region = regions[i];
                if (region.Type == ConnectedComponentAnalyzer.RegionType.DyNoise) continue;
                if (i > 0 && region.Bounds.Width < 10 && region.Bounds.Height < 10) continue;

                var color = region.Type switch
                {
                    ConnectedComponentAnalyzer.RegionType.DyText => Color.LimeGreen,
                    ConnectedComponentAnalyzer.RegionType.DyIcon => Color.DeepSkyBlue,
                    ConnectedComponentAnalyzer.RegionType.DyContainer => Color.Orange,
                    ConnectedComponentAnalyzer.RegionType.DySeparator => Color.Gold,
                    _ => Color.Gray
                };

                using var pen = new Pen(Color.FromArgb(230, color), region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer ? 2.5f : 1.6f);
                g.DrawRectangle(pen, region.Bounds);

                // Node label: UIA type or Dy type
                string nodeLabel;
                if (uiaAnswers != null && uiaAnswers.TryGetValue(i, out var uia))
                {
                    var parts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(uia.type)) parts.Add(uia.type);
                    if (!string.IsNullOrWhiteSpace(uia.aid)) parts.Add(uia.aid);
                    else if (!string.IsNullOrWhiteSpace(uia.name)) parts.Add(TrimPreview(uia.name, 20));
                    else parts.Add($"#{i + 1}");
                    nodeLabel = $"{string.Join("_", parts)} {region.Bounds.Width}x{region.Bounds.Height}";
                }
                else
                {
                    nodeLabel = $"{region.Type} {region.Bounds.Width}x{region.Bounds.Height}";
                }
                {
                    using var smallFont = new Font("Consolas", 7f, FontStyle.Regular);
                    var nlSize = g.MeasureString(nodeLabel, smallFont);
                    var nlx = Math.Max(region.Bounds.Left + 2,
                        Math.Min(region.Bounds.Right - (int)nlSize.Width - 5, overlay.Width - (int)nlSize.Width - 5));
                    var nly = region.Bounds.Top + 2;
                    if (nly < 0) nly = 2;
                    using var nodeBg = new SolidBrush(Color.FromArgb(170, 0, 0, 80));
                    using var nodeFg = new SolidBrush(Color.LightCyan);
                    g.FillRectangle(nodeBg, nlx - 2, nly, nlSize.Width + 4, nlSize.Height + 1);
                    g.DrawString(nodeLabel, smallFont, nodeFg, nlx, nly);
                }

                // OCR text: inside box, right half, gold color
                if (stageLabels != null && stageLabels.TryGetValue(i, out var stl) && !string.IsNullOrWhiteSpace(stl))
                {
                    string? ocrTxt = null;
                    if (stl.StartsWith("ocr ")) ocrTxt = stl[4..];
                    else if (stl.StartsWith("fix ")) ocrTxt = stl[4..];
                    else if (stl.StartsWith("uia ")) ocrTxt = stl[4..];
                    else if (stl.StartsWith("cache 100% ")) ocrTxt = stl[11..];
                    if (ocrTxt != null)
                    {
                        using var ocrFont = new Font("Consolas", 6.5f, FontStyle.Regular);
                        var halfW = Math.Max(20, region.Bounds.Width / 2);
                        var trimmed = TrimPreview(ocrTxt, (int)(halfW / 5));
                        var ocrSize = g.MeasureString(trimmed, ocrFont);
                        var ox = region.Bounds.Right - (int)ocrSize.Width - 3;
                        if (ox < region.Bounds.Left) ox = region.Bounds.Left + 1;
                        var oy = region.Bounds.Top + (region.Bounds.Height - (int)ocrSize.Height) / 2;
                        if (oy < region.Bounds.Top) oy = region.Bounds.Top;
                        using var ocrBg = new SolidBrush(Color.FromArgb(150, 40, 20, 0));
                        using var ocrFg = new SolidBrush(Color.Gold);
                        g.FillRectangle(ocrBg, ox - 1, oy, ocrSize.Width + 2, ocrSize.Height);
                        g.DrawString(trimmed, ocrFont, ocrFg, ox, oy);
                    }
                }
            }

            using var headerBg = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
            using var headerFg = new SolidBrush(Color.Cyan);
            var header = $"a11y-hack {stage}  screen=({screenLeft},{screenTop})  regions={regions.Count}";
            g.FillRectangle(headerBg, 0, 0, Math.Min(overlay.Width, 760), 24);
            g.DrawString(header, font, headerFg, 6, 4);

            var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            var path = Path.Combine(baseDir, $"hack-overlay-{stage}-{stamp}.png");
            overlay.Save(path, ImageFormat.Png);
            Console.WriteLine($"[HACK] Overlay preview: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[HACK] Overlay preview save failed: {ex.Message}");
        }
    }

    static IReadOnlyList<A11yHackOverlayBox> BuildHackOverlayBoxes(
        List<ConnectedComponentAnalyzer.Region> regions,
        Dictionary<int, (string name, string type, string aid)>? uiaAnswers = null,
        Dictionary<int, string>? stageLabels = null,
        IReadOnlyList<A11yHackOverlayBox>? uiaStandaloneBoxes = null,
        int ccaOffX = 0, int ccaOffY = 0)
    {
        var boxes = new List<A11yHackOverlayBox>(regions.Count);

        var targetBounds = regions.Count > 0 ? regions[0].Bounds : Rectangle.Empty;
        int parentIdx = -1;
        long parentArea = long.MaxValue;
        for (int i = 1; i < regions.Count; i++)
        {
            var rb = regions[i].Bounds;
            if (rb.Contains(targetBounds))
            {
                long area = (long)rb.Width * rb.Height;
                if (area < parentArea) { parentIdx = i; parentArea = area; }
            }
        }
        var parentBounds = parentIdx >= 0 ? regions[parentIdx].Bounds : Rectangle.Empty;

        for (int i = 0; i < regions.Count; i++)
        {
            var region = regions[i];
            if (i > 0 && region.Bounds.Width < 10 && region.Bounds.Height < 10) continue;

            // CCA regions in narrowed-bitmap coords -> offset to full-window coords
            var bounds = new System.Windows.Rect(
                region.Bounds.X + ccaOffX,
                region.Bounds.Y + ccaOffY,
                Math.Max(1, region.Bounds.Width),
                Math.Max(1, region.Bounds.Height));

            // Role: UIA/cache take priority over spatial containment
            bool hasUia = uiaAnswers != null && uiaAnswers.ContainsKey(i);
            HackBoxRole role;
            if (i == 0)
                role = HackBoxRole.Target;
            else if (i == parentIdx)
                role = HackBoxRole.Scope;
            else if (hasUia)
                role = HackBoxRole.Known;
            else if (parentIdx >= 0 && parentBounds.Contains(region.Bounds))
                role = HackBoxRole.Scope;
            else
                continue;

            string? label = null;
            if (uiaAnswers != null && uiaAnswers.TryGetValue(i, out var uia))
            {
                var idPart = !string.IsNullOrWhiteSpace(uia.aid) ? uia.aid
                           : !string.IsNullOrWhiteSpace(uia.name) ? TrimPreview(uia.name, 20)
                           : $"#{i + 1}";
                var typePart = !string.IsNullOrWhiteSpace(uia.type) ? uia.type : "";
                label = $"{typePart}_{idPart} {region.Bounds.Width}x{region.Bounds.Height}";
            }
            else if (region.Type != ConnectedComponentAnalyzer.RegionType.DyNoise)
            {
                label = $"{region.Type} {region.Bounds.Width}x{region.Bounds.Height}";
            }

            string? ocrText = null;
            if (stageLabels != null && stageLabels.TryGetValue(i, out var stl) && !string.IsNullOrWhiteSpace(stl))
            {
                if (stl.StartsWith("ocr ")) ocrText = stl[4..];
                else if (stl.StartsWith("fix ")) ocrText = stl[4..];
                else if (stl.StartsWith("uia ")) ocrText = stl[4..];
                else if (stl.StartsWith("cache 100% "))
                {
                    ocrText = stl[11..];
                    if (role != HackBoxRole.Target) role = HackBoxRole.Cached;
                }
            }

            boxes.Add(new A11yHackOverlayBox(bounds, label, ocrText, role));
        }

        // Append ALL standalone UIA tree boxes
        if (uiaStandaloneBoxes != null)
        {
            foreach (var uiaBox in uiaStandaloneBoxes)
                boxes.Add(uiaBox);
        }

        return boxes;
    }

    static void UpdateHackOverlay(
        A11yHackOverlayHost? liveOverlay,
        Bitmap source,
        List<ConnectedComponentAnalyzer.Region> regions,
        Dictionary<int, (string name, string type, string aid)>? uiaAnswers = null,
        Dictionary<int, string>? stageLabels = null,
        IReadOnlyList<A11yHackOverlayBox>? uiaStandaloneBoxes = null,
        int ccaOffX = 0, int ccaOffY = 0)
    {
        if (liveOverlay == null) return;
        var boxes = BuildHackOverlayBoxes(regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
        liveOverlay.Update(new A11yHackOverlayModel(boxes));
    }

    // -- Text utilities --

    static string TrimPreview(string text, int maxLen)
    {
        if (string.IsNullOrWhiteSpace(text)) return "";
        text = text.Replace("\r", " ").Replace("\n", " ").Trim();
        return text.Length <= maxLen ? text : text[..maxLen] + "...";
    }

    static double CalculateHackTextSimilarity(string a, string b)
    {
        var na = NormalizeHackText(a);
        var nb = NormalizeHackText(b);
        if (na.Length == 0 || nb.Length == 0) return 0;
        if (na == nb) return 1.0;
        if (na.Contains(nb) || nb.Contains(na))
            return (double)Math.Min(na.Length, nb.Length) / Math.Max(na.Length, nb.Length);

        var tokA = new HashSet<string>(na.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var tokB = new HashSet<string>(nb.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        if (tokA.Count == 0 || tokB.Count == 0) return 0;
        int intersection = tokA.Count(t => tokB.Contains(t));
        int union = tokA.Count + tokB.Count - intersection;
        return union > 0 ? (double)intersection / union : 0;
    }

    static string NormalizeHackText(string text)
    {
        text = text.Normalize(System.Text.NormalizationForm.FormC).ToLowerInvariant();
        var chars = text.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
        return new string(chars).Trim();
    }

    // -- Experience DB caching --

    static void CacheSegment(Bitmap source, Rectangle bounds, int imgW, int imgH,
        string dynId, string description, bool isContainer = false)
    {
        try
        {
            var cropRect = Rectangle.Intersect(bounds, new Rectangle(0, 0, imgW, imgH));
            if (cropRect.Width <= 0 || cropRect.Height <= 0) return;
            using var crop = source.Clone(cropRect, source.PixelFormat);
            var hash = OcrGapCollector.ComputePixelHash(crop);

            var baseDir = _hackExpDir ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WKAppBot", "gap_cache");

            string targetDir;
            if (isContainer)
            {
                targetDir = Path.Combine(baseDir, $"hack_{dynId}");
                _currentContainerDir = targetDir;
            }
            else
            {
                targetDir = _currentContainerDir ?? baseDir;
            }

            Directory.CreateDirectory(targetDir);
            var desc = description.Length > 50 ? description[..50] : description;
            foreach (var c in Path.GetInvalidFileNameChars()) desc = desc.Replace(c, '_');
            desc = desc.Replace('=', '_').Replace('\'', '_');
            var fileName = $"{dynId}'{hash}={desc}.png";
            var filePath = Path.Combine(targetDir, fileName);
            if (!File.Exists(filePath))
                crop.Save(filePath, ImageFormat.Png);
        }
        catch { }
    }

    static void InitHackExpDir(IntPtr hwnd)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var proc = System.Diagnostics.Process.GetProcessById((int)pid);
            var procName = proc.ProcessName;
            var className = "";
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            className = sb.ToString();
            _hackExpDir = Path.Combine(DataDir, "experience",
                procName.Length > 0 ? procName : "unknown",
                className.Length > 0 ? className : "unknown");
            Directory.CreateDirectory(_hackExpDir);
            _currentContainerDir = null;
            Console.Error.WriteLine($"[HACK] ExpDB: {_hackExpDir}");
        }
        catch { }
    }
}
