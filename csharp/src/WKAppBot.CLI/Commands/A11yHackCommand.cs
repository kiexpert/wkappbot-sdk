using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot a11y hack <grap> — Force DYN-A11Y analysis on target window/element.
    /// Captures screenshot → CCA segmentation → OCR → Vision (if needed) → dynamic a11y tree.
    /// </summary>
    static int A11yHackCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot a11y hack <grap> [--depth N]");
            Console.WriteLine("  Force DYN-A11Y segmentation on target window or element.");
            Console.WriteLine("  Pipeline: capture → CCA → OCR → Vision → dynamic a11y tree");
            return 1;
        }

        PulseStep.Init("a11y-hack");

        // Find target window
        var grap = args[0];
        var targets = WindowFinder.FindByTitle(grap);
        if (!targets.Any())
        {
            Console.Error.WriteLine($"[HACK] No window found: \"{grap}\"");
            return 1;
        }
        var win = targets.First();
        var hwnd = win.Handle;
        Console.WriteLine($"[HACK] Target: 0x{hwnd.ToInt64():X} \"{win.Title}\"");
        PulseStep.Mark("target-found");

        // Capture screenshot
        NativeMethods.GetWindowRect(hwnd, out var wr);
        int w = wr.Right - wr.Left, h = wr.Bottom - wr.Top;
        if (w <= 0 || h <= 0)
        {
            Console.Error.WriteLine("[HACK] Window has zero size");
            return 1;
        }

        Bitmap bmp;
        try
        {
            bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(wr.Left, wr.Top, 0, 0, new Size(w, h));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[HACK] Capture failed: {ex.Message}");
            return 1;
        }
        Console.WriteLine($"[HACK] Captured: {w}x{h}");
        PulseStep.Mark("captured");

        // CCA segmentation
        var cca = new ConnectedComponentAnalyzer();
        var regions = cca.AnalyzeAndMerge(bmp);
        PulseStep.Mark("cca-done");

        int textCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Text);
        int iconCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Icon);
        int sepCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Separator);
        int contCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Container);
        int noiseCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Noise);

        Console.WriteLine($"[HACK] CCA: {regions.Count} regions — Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount} Noise={noiseCount}");

        // Table detection
        var allRegions = cca.Analyze(bmp); // non-merged for table detection
        var table = cca.DetectTable(allRegions, w, h);
        if (table != null)
            Console.WriteLine($"[HACK] Table detected: {table.Rows} rows × {table.Cols} cols");
        PulseStep.Mark("table-detect");

        // OCR on text regions
        Console.WriteLine($"[HACK] ── Segments ──");
        var positions = DynamicA11yAnalyzer.AssignGridPositions(
            regions, r => r.Bounds, rowGap: 5);

        using var ocr = new SimpleOcrAnalyzer();
        int ocrOk = 0, ocrEmpty = 0;
        foreach (var (row, col, region) in positions)
        {
            var dynId = DynamicA11yAnalyzer.GenerateDynId(row, col, region.Bounds.Width, region.Bounds.Height);
            string typeStr = region.Type.ToString();
            string ocrText = "";

            if (region.Type == ConnectedComponentAnalyzer.RegionType.Text
                || region.Type == ConnectedComponentAnalyzer.RegionType.Container)
            {
                try
                {
                    var cropRect = Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h));
                    if (cropRect.Width > 0 && cropRect.Height > 0)
                    {
                        using var crop = bmp.Clone(cropRect, bmp.PixelFormat);
                        var result = ocr.RecognizeAll(crop).GetAwaiter().GetResult();
                        ocrText = string.Join(" ", result.Words.Select(x => x.Text)).Trim();
                        if (ocrText.Length > 0) ocrOk++; else ocrEmpty++;
                    }
                }
                catch { ocrEmpty++; }
            }

            var label = ocrText.Length > 0 ? $" \"{(ocrText.Length > 40 ? ocrText[..40] + "..." : ocrText)}\"" : "";
            var density = region.Density > 0 ? $" d={region.Density:F2}" : "";
            var thin = region.Thinness > 0 ? $" thin={region.Thinness:F2}" : "";
            Console.WriteLine($"  {dynId} [{typeStr}]{label} rect=({region.Bounds.X},{region.Bounds.Y} {region.Bounds.Width}x{region.Bounds.Height}){density}{thin}");
        }
        PulseStep.Mark("ocr-done");

        Console.WriteLine($"[HACK] OCR: {ocrOk} recognized, {ocrEmpty} empty");
        Console.WriteLine($"[HACK] Total: {positions.Count} nodes");

        bmp.Dispose();
        PulseStep.Done();
        return 0;
    }
}
