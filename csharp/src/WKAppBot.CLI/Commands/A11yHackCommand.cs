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
            Console.WriteLine("Usage: wkappbot a11y hack <grap>[#scope] [--engine gemini|gpt]");
            Console.WriteLine("  Force DYN-A11Y segmentation on target window or element.");
            Console.WriteLine("  Pipeline: capture → CCA → OCR → Vision → dynamic a11y tree");
            return 1;
        }

        // Parse options
        string visionEngine = "gemini";
        for (int ai = 0; ai < args.Length; ai++)
            if (args[ai] == "--engine" && ai + 1 < args.Length) visionEngine = args[++ai].ToLowerInvariant();

        PulseStep.Init("a11y-hack");

        // Parse grap#scope
        var grapFull = string.Join(" ", args.TakeWhile(a => !a.StartsWith("--")));
        var hashIdx = grapFull.IndexOf('#');
        var grap = hashIdx >= 0 ? grapFull[..hashIdx] : grapFull;
        var uiaScope = hashIdx >= 0 ? grapFull[(hashIdx + 1)..] : null;

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

        // If #scope specified, find UIA element and use its BoundingRectangle
        NativeMethods.GetWindowRect(hwnd, out var wr);
        if (!string.IsNullOrEmpty(uiaScope))
        {
            try
            {
                using var automation = new FlaUI.UIA3.UIA3Automation();
                var root = automation.FromHandle(hwnd);
                var scoped = WKAppBot.Win32.Accessibility.GrapHelper.FindUiaScope(root, uiaScope);
                if (scoped != null)
                {
                    var r = scoped.Properties.BoundingRectangle.ValueOrDefault;
                    wr.Left = r.X; wr.Top = r.Y; wr.Right = r.X + r.Width; wr.Bottom = r.Y + r.Height;
                    Console.WriteLine($"[HACK] Scoped to: \"{uiaScope}\" rect=({r.X},{r.Y} {r.Width}x{r.Height})");
                }
                else Console.WriteLine($"[HACK] Scope \"{uiaScope}\" not found — using full window");
            }
            catch { Console.WriteLine($"[HACK] Scope error — using full window"); }
        }
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

        // OCR on text regions + tree output
        var positions = DynamicA11yAnalyzer.AssignGridPositions(
            regions, r => r.Bounds, rowGap: 5);

        using var ocr = new SimpleOcrAnalyzer();
        var gapCollector = new OcrGapCollector();
        int ocrOk = 0, ocrEmpty = 0;
        int lastRow = -1;

        // Group by row for tree display
        var rowGroups = positions.GroupBy(p => p.row).OrderBy(g => g.Key).ToList();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[HACK] ── DYN-A11Y Tree ({w}×{h}) ──");
        Console.ResetColor();

        foreach (var rowGroup in rowGroups)
        {
            var items = rowGroup.OrderBy(p => p.col).ToList();
            bool isLastRow = rowGroup.Key == rowGroups.Last().Key;
            string rowPrefix = isLastRow ? "└── " : "├── ";
            string childPrefix = isLastRow ? "    " : "│   ";

            for (int ci = 0; ci < items.Count; ci++)
            {
                var (row, col, region) = items[ci];
                var dynId = DynamicA11yAnalyzer.GenerateDynId(row, col, region.Bounds.Width, region.Bounds.Height);
                string ocrText = "";
                bool isLastChild = ci == items.Count - 1;
                string prefix = ci == 0 ? rowPrefix : childPrefix + (isLastChild ? "└ " : "├ ");

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
                            if (ocrText.Length > 0) ocrOk++;
                            else
                            {
                                ocrEmpty++;
                                gapCollector.Add(cropRect, null);
                            }
                        }
                    }
                    catch { ocrEmpty++; }
                }

                // Tree line
                var typeIcon = region.Type switch
                {
                    ConnectedComponentAnalyzer.RegionType.Text => "📝",
                    ConnectedComponentAnalyzer.RegionType.Icon => "🎨",
                    ConnectedComponentAnalyzer.RegionType.Container => "📦",
                    ConnectedComponentAnalyzer.RegionType.Separator => "──",
                    _ => "·"
                };
                if (region.Type == ConnectedComponentAnalyzer.RegionType.Noise) continue; // skip noise in tree

                var label = ocrText.Length > 0
                    ? $" \"{(ocrText.Length > 50 ? ocrText[..50] + "..." : ocrText)}\""
                    : (region.Type == ConnectedComponentAnalyzer.RegionType.Text ? " [?]" : "");
                Console.WriteLine($"{prefix}{typeIcon} {dynId}{label}  ({region.Bounds.Width}×{region.Bounds.Height})");
            }
        }
        PulseStep.Mark("ocr-done");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[HACK] ── Summary ──");
        Console.ResetColor();
        Console.WriteLine($"  Segments: {regions.Count} (Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount})");
        Console.WriteLine($"  OCR: {ocrOk} ok, {ocrEmpty} failed");
        if (table != null)
            Console.WriteLine($"  Table: {table.Rows}×{table.Cols}");
        if (gapCollector.HasGaps)
        {
            Console.WriteLine($"  Vision needed: {gapCollector.Count} blind region(s)");
            PulseStep.Mark("vision-query");

            // Build triple composite → save → ask Gemini
            try
            {
                var (images, prompt) = gapCollector.BuildTripleComposite();
                if (images.Count > 0)
                {
                    var gapDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "WKAppBot", "gap_screenshots");
                    Directory.CreateDirectory(gapDir);
                    var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    // Save first image for Gemini ask
                    var compositePath = Path.Combine(gapDir, $"hack_{ts}_A.png");
                    File.WriteAllBytes(compositePath, images[0]);
                    var promptPath = Path.Combine(gapDir, $"hack_{ts}.prompt.txt");
                    File.WriteAllText(promptPath, prompt);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"[HACK] Asking Gemini Vision ({gapCollector.Count} regions)...");
                    Console.ResetColor();

                    // Ask Gemini with composite image
                    Console.WriteLine($"[HACK] Engine: {visionEngine}");
                    int exitCode = visionEngine switch
                    {
                        "gpt" => AskChatGpt(prompt, slackReport: false, timeoutSec: 60,
                            attachFiles: new List<string> { compositePath }, noWait: false),
                        "claude" => AskClaude(prompt, slackReport: false, timeoutSec: 60,
                            noWait: false),
                        _ => AskGemini(prompt, slackReport: false, timeoutSec: 60,
                            attachFiles: new List<string> { compositePath }, noWait: false),
                    };

                    PulseStep.Mark("vision-done");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[HACK] Vision query complete (exit={exitCode})");
                    Console.ResetColor();

                    // Cache: save composite with pixel hash for future lookups
                    try
                    {
                        var cacheDir = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "WKAppBot", "gap_cache");
                        Directory.CreateDirectory(cacheDir);
                        using var hashBmp = new Bitmap(compositePath);
                        var hash = OcrGapCollector.ComputePixelHash(hashBmp);
                        var cacheName = $"hack'{hash}={ts}.png";
                        var cachePath = Path.Combine(cacheDir, cacheName);
                        if (!File.Exists(cachePath))
                            File.Copy(compositePath, cachePath);
                        Console.WriteLine($"[HACK] Cached: {cacheName}");
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HACK] Vision error: {ex.Message}");
            }
        }

        gapCollector.Dispose();
        bmp.Dispose();
        PulseStep.Done();
        return 0;
    }
}
