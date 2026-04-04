using System.Drawing;
using System.Drawing.Imaging;
using FlaUI.Core.AutomationElements;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot a11y hack <grap> ??Force DYN-A11Y analysis on target window/element.
    /// Captures screenshot ??CCA segmentation ??OCR ??Vision (if needed) ??dynamic a11y tree.
    /// </summary>
    static int A11yHackCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot a11y hack <grap>[#scope] [--engine gemini|gpt]");
            Console.WriteLine("  Force DYN-A11Y segmentation on target window or element.");
            Console.WriteLine("  Pipeline: capture ??CCA ??OCR ??Vision ??dynamic a11y tree");
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
        bool hasExplicitScope = hashIdx >= 0 && !string.IsNullOrWhiteSpace(uiaScope);
        bool focusDrillRequested = grapFull.TrimEnd().EndsWith('/')
            || (hashIdx >= 0 && string.IsNullOrWhiteSpace(uiaScope));

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
        TryAutoNarrowHackRect(hwnd, grapFull, uiaScope, focusDrillRequested, hasExplicitScope, ref wr);
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
                else Console.WriteLine($"[HACK] Scope \"{uiaScope}\" not found ??using full window");
            }
            catch { Console.WriteLine($"[HACK] Scope error ??using full window"); }
        }
        int w = wr.Right - wr.Left, h = wr.Bottom - wr.Top;
        if (w <= 0 || h <= 0)
        {
            Console.Error.WriteLine("[HACK] Window has zero size");
            return 1;
        }
        var analysisTargetRect = TryResolveAnalysisTargetRect(wr);
        var liveOverlay = A11yHackOverlayHost.GetOrCreate(OverlaySlot.Session, wr.Left, wr.Top, w, h);
        liveOverlay?.Update(new A11yHackOverlayModel(new[]
        {
            new A11yHackOverlayBox(
                new System.Windows.Rect(0, 0, Math.Max(1, w - 1), Math.Max(1, h - 1)),
                "target", Role: HackBoxRole.Target)
        }));
        liveOverlay?.StartHoverTracking(wr.Left, wr.Top);

        // ── Abort checks (two tiers) ──
        var hackAborted = false;
        var inputBaselineTick = Environment.TickCount;
        RECT baselineWr = wr;
        long baselinePixelHash = 0;
        Rectangle targetScreenRect = Rectangle.Empty;
        int lastChangCheckTick = Environment.TickCount;

        // Tier 1: user input only (cheap — called per OCR item, 1s cooldown)
        bool HackShouldAbort()
        {
            if (hackAborted) return true;
            var idleMs = NativeMethods.GetUserIdleMs();
            var elapsed = (uint)(Environment.TickCount - inputBaselineTick);
            if (idleMs < elapsed && idleMs < 1000)
            {
                hackAborted = true;
                Console.WriteLine("[HACK] User input detected — aborting");
                liveOverlay?.Hide();
                return true;
            }
            return false;
        }

        // Tier 2: position + content change (stage boundaries, min 1s interval)
        bool HackTargetChanged()
        {
            if (hackAborted) return true;
            var now = Environment.TickCount;
            if (now - lastChangCheckTick < 1000) return false; // 1s cooldown
            lastChangCheckTick = now;

            // 1. Window moved/resized?
            NativeMethods.GetWindowRect(hwnd, out var curWr);
            if (curWr.Left != baselineWr.Left || curWr.Top != baselineWr.Top
                || curWr.Right != baselineWr.Right || curWr.Bottom != baselineWr.Bottom)
            {
                hackAborted = true;
                Console.WriteLine("[HACK] Window moved/resized — aborting");
                liveOverlay?.Hide();
                return true;
            }
            // 2. Target node pixels changed?
            if (baselinePixelHash != 0 && !targetScreenRect.IsEmpty)
            {
                var cur = SampleCenterPixels(targetScreenRect);
                if (cur != baselinePixelHash)
                {
                    hackAborted = true;
                    Console.WriteLine("[HACK] Content changed — aborting for re-analysis");
                    liveOverlay?.Hide();
                    return true;
                }
            }
            return false;
        }

        // Sample 9 pixels (3x3 grid at target center) — no GDI Bitmap, just GetPixel
        static long SampleCenterPixels(Rectangle r)
        {
            try
            {
                var hdc = NativeMethods.GetDC(IntPtr.Zero);
                if (hdc == IntPtr.Zero) return 0;
                try
                {
                    long hash = unchecked((long)0xcbf29ce484222325);
                    int cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;
                    for (int dy = -1; dy <= 1; dy++)
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        uint px = NativeMethods.GetPixel(hdc, cx + dx, cy + dy);
                        hash = unchecked((hash ^ (long)px) * (long)0x100000001b3);
                    }
                    return hash;
                }
                finally { NativeMethods.ReleaseDC(IntPtr.Zero, hdc); }
            }
            catch { return 0; }
        }

        Bitmap bmp;
        try
        {
            bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(wr.Left, wr.Top, 0, 0, new Size(w, h));
            // baselineHash set after CCA when target node bounds are known
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[HACK] Capture failed: {ex.Message}");
            return 1;
        }
        Console.WriteLine($"[HACK] Captured: {w}x{h}");
        InitHackExpDir(hwnd);
        PulseStep.Mark("captured");

        if (HackShouldAbort()) { bmp.Dispose(); return 0; }

        // CCA segmentation
        var cca = new ConnectedComponentAnalyzer();
        var regions = cca.AnalyzeAndMerge(bmp);
        regions = OrderRegionsForTarget(regions, analysisTargetRect, w, h);
        PulseStep.Mark("cca-done");

        // Set baseline for content change detection (4-pixel sample on target node)
        if (regions.Count > 0)
        {
            var tb = regions[0].Bounds;
            targetScreenRect = new Rectangle(wr.Left + tb.X, wr.Top + tb.Y, tb.Width, tb.Height);
            baselinePixelHash = SampleCenterPixels(targetScreenRect);
        }

        int textCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.DyText);
        int iconCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.DyIcon);
        int sepCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.DySeparator);
        int contCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.DyContainer);
        int noiseCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.DyNoise);

        Console.WriteLine($"[HACK] CCA: {regions.Count} regions ??Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount} Noise={noiseCount}");
        SaveHackOverlayPreview(bmp, regions, "cca", wr.Left, wr.Top);
        var stageLabels = new Dictionary<int, string>();
        UpdateHackOverlay(liveOverlay, bmp, regions, null, stageLabels);

        // Table detection
        var allRegions = cca.Analyze(bmp); // non-merged for table detection
        var table = cca.DetectTable(allRegions, w, h);
        if (table != null)
            Console.WriteLine($"[HACK] Table detected: {table.Rows} rows 횞 {table.Cols} cols");
        PulseStep.Mark("table-detect");

        if (HackTargetChanged()) { bmp.Dispose(); return 0; }
        // UIA answer key: scan UIA tree, build rect→info map for cross-reference
        var uiaAnswers = new Dictionary<int, (string name, string type, string aid)>();
        var uiaBounds = new Dictionary<int, Rectangle>(); // UIA element screen rect (most specific)
        var _uiaBestArea = new Dictionary<int, int>();
        try
        {
            PulseStep.Mark("uia-scan");
            using var automation = new FlaUI.UIA3.UIA3Automation();
            var uiaRoot = automation.FromHandle(hwnd);
            var uiaElements = uiaRoot.FindAllDescendants();
            int uiaMatched = 0;
            for (int ri = 0; ri < regions.Count; ri++)
            {
                var seg = regions[ri];
                // Convert segment rect to screen coordinates
                var segScreen = new Rectangle(seg.Bounds.X + wr.Left, seg.Bounds.Y + wr.Top, seg.Bounds.Width, seg.Bounds.Height);
                foreach (var el in uiaElements)
                {
                    try
                    {
                        var elRect = el.Properties.BoundingRectangle.ValueOrDefault;
                        var elR = new Rectangle(elRect.X, elRect.Y, elRect.Width, elRect.Height);
                        // Overlap > 50% of segment area = match
                        // Pick smallest matching UIA element (most specific)
                        var overlap = Rectangle.Intersect(segScreen, elR);
                        if (overlap.Width * overlap.Height > seg.Bounds.Width * seg.Bounds.Height * 0.5)
                        {
                            var name = el.Properties.Name.ValueOrDefault ?? "";
                            var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
                            var ct = ""; try { ct = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                            if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(aid))
                            {
                                int elArea = elR.Width * elR.Height;
                                bool existing = uiaAnswers.ContainsKey(ri);
                                // Keep smallest (most specific) match
                                if (!existing || elArea < _uiaBestArea.GetValueOrDefault(ri, int.MaxValue))
                                {
                                    uiaAnswers[ri] = (name, ct, aid);
                                    uiaBounds[ri] = elR; // screen coords
                                    _uiaBestArea[ri] = elArea;
                                    if (!existing) uiaMatched++;
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            Console.WriteLine($"[HACK] UIA answer key: {uiaMatched}/{regions.Count} segments matched");

            // Narrow target + parent bounds using UIA (most specific element rect)
            for (int ri = 0; ri < Math.Min(regions.Count, 2); ri++) // target(0) + parent candidate
            {
                if (!uiaBounds.TryGetValue(ri, out var uiaR)) continue;
                var seg = regions[ri];
                // Convert UIA screen rect to bitmap coords
                var narrowed = new Rectangle(
                    uiaR.X - wr.Left, uiaR.Y - wr.Top, uiaR.Width, uiaR.Height);
                narrowed = Rectangle.Intersect(narrowed, new Rectangle(0, 0, w, h));
                if (narrowed.Width > 0 && narrowed.Height > 0 && narrowed.Width * narrowed.Height < seg.Bounds.Width * seg.Bounds.Height)
                {
                    Console.WriteLine($"[HACK] Region[{ri}] narrowed: {seg.Bounds.Width}x{seg.Bounds.Height} → {narrowed.Width}x{narrowed.Height} (UIA)");
                    regions[ri] = new ConnectedComponentAnalyzer.Region
                    {
                        Bounds = narrowed, Type = seg.Type, PixelCount = seg.PixelCount,
                        Perimeter = seg.Perimeter, Label = seg.Label
                    };
                }
            }

            SaveHackOverlayPreview(bmp, regions, "uia", wr.Left, wr.Top, uiaAnswers, stageLabels);
            for (int ri = 0; ri < regions.Count; ri++)
            {
                if (!uiaAnswers.TryGetValue(ri, out var uia)) continue;
                var text = !string.IsNullOrWhiteSpace(uia.name) ? uia.name : uia.aid;
                if (!string.IsNullOrWhiteSpace(text))
                    stageLabels[ri] = $"uia {TrimPreview(text, 24)}";
            }
            UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
        }
        catch (Exception ex) { Console.WriteLine($"[HACK] UIA scan error: {ex.Message}"); }

        if (HackTargetChanged()) { bmp.Dispose(); return 0; }
        // OCR on text regions + tree output
        var positions = DynamicA11yAnalyzer.AssignGridPositions(
            regions, r => r.Bounds, rowGap: 5);

        using var ocr = new SimpleOcrAnalyzer();
        var gapCollector = new OcrGapCollector();
        // OcrCorrectionDb: self-learning pixel-hash → correct text dictionary
        WKAppBot.Vision.OcrCorrectionDb? correctionDb = null;
        try
        {
            if (_hackExpDir != null)
            {
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint cpid);
                var cproc = System.Diagnostics.Process.GetProcessById((int)cpid);
                var csb = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(hwnd, csb, csb.Capacity);
                correctionDb = WKAppBot.Vision.OcrCorrectionDb.ForProcess(
                    cproc.ProcessName, csb.ToString());
            }
        }
        catch { }
        int ocrOk = 0, ocrEmpty = 0;

        // Group by row for tree display
        var rowGroups = positions.GroupBy(p => p.row).OrderBy(g => g.Key).ToList();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[HACK] ── DYN-A11Y Tree ({w}횞{h}) ──");
        Console.ResetColor();

        foreach (var rowGroup in rowGroups)
        {
            var items = rowGroup.OrderBy(p => p.col).ToList();
            bool isLastRow = rowGroup.Key == rowGroups.Last().Key;
            string rowPrefix = isLastRow ? "└── " : "├── ";
            string childPrefix = isLastRow ? "    " : "??  ";

            for (int ci = 0; ci < items.Count; ci++)
            {
                if (HackShouldAbort()) { bmp.Dispose(); return 0; }
                var (row, col, region) = items[ci];
                int regionIdx = regions.IndexOf(region);
                var dynId = DynamicA11yAnalyzer.GenerateDynId(row, col, region.Bounds.Width, region.Bounds.Height);
                string ocrText = "";
                string uiaLabel = "";
                bool isLastChild = ci == items.Count - 1;
                string prefix = ci == 0 ? rowPrefix : childPrefix + (isLastChild ? "??" : "??");

                // Check UIA answer key first ??skip OCR if matched
                if (regionIdx >= 0 && uiaAnswers.TryGetValue(regionIdx, out var uia))
                {
                    uiaLabel = !string.IsNullOrEmpty(uia.name) ? uia.name : uia.aid;
                    ocrOk++;
                    CacheSegment(bmp, region.Bounds, w, h, dynId, uiaLabel,
                        isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer);
                    // Mismatch detection: CCA type vs UIA type ??stderr for self-healing
                    var ccaType = region.Type.ToString();
                    var uiaType = uia.type;
                    if (!string.IsNullOrEmpty(uiaType))
                    {
                        bool match = (ccaType == "DyText" && uiaType is "Text" or "Edit" or "Document")
                                  || (ccaType == "DyIcon" && uiaType is "Image" or "Button")
                                  || (ccaType == "DyContainer" && uiaType is "Group" or "Pane" or "Window")
                                  || (ccaType == "Separator" && uiaType is "Separator");
                        if (!match)
                            Console.Error.WriteLine($"[MISMATCH] {dynId}: CCA={ccaType} UIA={uiaType} \"{uiaLabel}\" d={region.Density:F2} ar={region.AspectRatio:F1}");
                    }

                    if (region.Type == ConnectedComponentAnalyzer.RegionType.DyText
                        || region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer)
                    {
                        try
                        {
                            var cropRect = ConnectedComponentAnalyzer.TrimBorders(bmp,
                                Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h)));
                            if (cropRect.Width > 0 && cropRect.Height > 0)
                            {
                                using var crop = bmp.Clone(cropRect, bmp.PixelFormat);
                                var verify = ocr.RecognizeAll(crop).GetAwaiter().GetResult();
                                var verifyText = string.Join(" ", verify.Words.Select(x => x.Text)).Trim();
                                if (!string.IsNullOrWhiteSpace(verifyText))
                                {
                                    ocrText = verifyText;
                                    var sim = CalculateHackTextSimilarity(uiaLabel, verifyText);
                                    stageLabels[regionIdx] = sim >= 0.95
                                        ? $"ocr={sim:P0}"
                                        : $"ocr={sim:P0} mismatch";
                                    // Learn: OCR ≠ UIA → store correction (UIA is ground truth)
                                    if (sim < 0.95 && correctionDb != null)
                                    {
                                        correctionDb.Learn(crop, verifyText, uiaLabel, "uia");
                                        Console.Error.WriteLine($"[OCR-LEARN] {dynId}: \"{verifyText}\"→\"{uiaLabel}\" (uia, sim={sim:P0})");
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
                else if (region.Type == ConnectedComponentAnalyzer.RegionType.DyText
                    || region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer)
                {
                    try
                    {
                        var cropRect = ConnectedComponentAnalyzer.TrimBorders(bmp,
                            Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h)));
                        if (cropRect.Width > 0 && cropRect.Height > 0)
                        {
                            using var crop = bmp.Clone(cropRect, bmp.PixelFormat);
                            var result = ocr.RecognizeAll(crop).GetAwaiter().GetResult();
                            ocrText = string.Join(" ", result.Words.Select(x => x.Text)).Trim();
                            // OcrCorrectionDb: auto-correct known misreads
                            if (correctionDb != null)
                            {
                                var corrected = correctionDb.TryCorrect(crop, ocrText);
                                if (corrected != null)
                                {
                                    Console.Error.WriteLine($"[OCR-FIX] {dynId}: \"{ocrText}\"→\"{corrected}\" (correction db)");
                                    ocrText = corrected;
                                }
                            }
                            if (ocrText.Length > 0)
                            {
                                ocrOk++;
                                CacheSegment(bmp, region.Bounds, w, h, dynId, ocrText,
                                    isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer);
                                stageLabels[regionIdx] = $"ocr {TrimPreview(ocrText, 24)}";
                                UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
                            }
                            else
                            {
                                // OCR empty — try correction db before going to Vision API
                                var corrected2 = correctionDb?.TryCorrect(crop, "");
                                if (corrected2 != null)
                                {
                                    ocrText = corrected2;
                                    ocrOk++;
                                    CacheSegment(bmp, region.Bounds, w, h, dynId, ocrText,
                                        isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer);
                                    stageLabels[regionIdx] = $"fix {TrimPreview(ocrText, 24)}";
                                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
                                }
                                else
                                {
                                    ocrEmpty++;
                                    if (gapCollector.Add(cropRect, null, null, out var cachedDescription))
                                        stageLabels[regionIdx] = "vision pending";
                                    else if (!string.IsNullOrWhiteSpace(cachedDescription))
                                        stageLabels[regionIdx] = $"cache 100% {TrimPreview(cachedDescription, 18)}";
                                    else
                                        stageLabels[regionIdx] = "vision pending";
                                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
                                }
                            }
                        }
                    }
                    catch
                    {
                        ocrEmpty++;
                        stageLabels[regionIdx] = "ocr error";
                        UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
                    }
                }

                // Tree line
                var typeIcon = region.Type switch
                {
                    ConnectedComponentAnalyzer.RegionType.DyText => "DyText",
                    ConnectedComponentAnalyzer.RegionType.DyIcon => "DyIcon",
                    ConnectedComponentAnalyzer.RegionType.DyContainer => "DyContainer",
                    ConnectedComponentAnalyzer.RegionType.DySeparator => "──",
                    _ => "쨌"
                };
                if (region.Type == ConnectedComponentAnalyzer.RegionType.DyNoise) continue; // skip noise in tree

                string label;
                if (uiaLabel.Length > 0)
                    label = $" \"{(uiaLabel.Length > 50 ? uiaLabel[..50] + "..." : uiaLabel)}\"";
                else if (ocrText.Length > 0)
                    label = $" \"{(ocrText.Length > 50 ? ocrText[..50] + "..." : ocrText)}\"";
                else
                    label = region.Type == ConnectedComponentAnalyzer.RegionType.DyText ? " [?]" : "";
                if (regionIdx >= 0 && stageLabels.TryGetValue(regionIdx, out var stageLabel) && !string.IsNullOrWhiteSpace(stageLabel))
                    label += $" [{stageLabel}]";
                Console.WriteLine($"{prefix}{typeIcon} {dynId}{label}  ({region.Bounds.Width}횞{region.Bounds.Height})");
            }
        }
        PulseStep.Mark("ocr-done");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[HACK] ── Summary ──");
        Console.ResetColor();
        Console.WriteLine($"  Segments: {regions.Count} (Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount})");
        Console.WriteLine($"  OCR: {ocrOk} ok, {ocrEmpty} failed");
        if (table != null)
            Console.WriteLine($"  Table: {table.Rows}횞{table.Cols}");
        if (HackTargetChanged()) { bmp.Dispose(); return 0; }
        if (gapCollector.HasGaps)
        {
            Console.WriteLine($"  Vision needed: {gapCollector.Count} blind region(s)");
            for (int ri = 0; ri < regions.Count; ri++)
            {
                if (!stageLabels.ContainsKey(ri) && regions[ri].Type is ConnectedComponentAnalyzer.RegionType.DyText or ConnectedComponentAnalyzer.RegionType.DyContainer)
                    stageLabels[ri] = "vision pending";
            }
            UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
            PulseStep.Mark("vision-query");

            // Build triple composite ??save ??ask Gemini
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
                    for (int ri = 0; ri < regions.Count; ri++)
                    {
                        if (stageLabels.TryGetValue(ri, out var cur) && cur == "vision pending")
                            stageLabels[ri] = $"vision {visionEngine}";
                    }
                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels);
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

        // ── MD table summary: segment inventory ──
        Console.WriteLine();
        Console.WriteLine("| # | Type | Id | WxH | OCR/Label |");
        Console.WriteLine("|---|------|-----|-----|-----------|");
        for (int si = 0; si < regions.Count; si++)
        {
            var sr = regions[si];
            if (sr.Type == ConnectedComponentAnalyzer.RegionType.DyNoise) continue;
            var pos = positions.FirstOrDefault(p => ReferenceEquals(p.region, sr));
            var dynId = DynamicA11yAnalyzer.GenerateDynId(pos.row, pos.col, sr.Bounds.Width, sr.Bounds.Height);
            var uiaType = ""; var uiaId = "";
            if (uiaAnswers != null && uiaAnswers.TryGetValue(si, out var su))
            {
                uiaType = su.type ?? "";
                uiaId = !string.IsNullOrWhiteSpace(su.aid) ? su.aid
                      : !string.IsNullOrWhiteSpace(su.name) ? TrimPreview(su.name, 20) : $"#{si + 1}";
            }
            var typeCol = !string.IsNullOrEmpty(uiaType) ? uiaType : sr.Type.ToString();
            var idCol = !string.IsNullOrEmpty(uiaId) ? uiaId : dynId;
            var size = $"{sr.Bounds.Width}x{sr.Bounds.Height}";
            var labelCol = "";
            if (stageLabels.TryGetValue(si, out var stl))
            {
                if (stl.StartsWith("ocr ")) labelCol = stl[4..];
                else if (stl.StartsWith("fix ")) labelCol = stl[4..];
                else if (stl.StartsWith("uia ")) labelCol = stl[4..];
                else if (stl.StartsWith("cache 100% ")) labelCol = stl[11..];
                else labelCol = stl;
            }
            Console.WriteLine($"| {si} | {typeCol} | {idCol} | {size} | {TrimPreview(labelCol, 30)} |");
        }
        Console.WriteLine();

        bmp.Dispose();
        liveOverlay?.Hide();
        PulseStep.Done();
        return 0;
    }

    /// <summary>
    /// Cache segment to experience DB. Container segments become subdirectories.
    /// Path: experience/{proc}/{class}/hack_{containerId}/{dynId}'{hash}={desc}.png
    /// </summary>
    static string? _hackExpDir; // set once per hack run
    static string? _currentContainerDir; // current container subfolder

    static void TryAutoNarrowHackRect(
        IntPtr hwnd,
        string grapFull,
        string? uiaScope,
        bool focusDrillRequested,
        bool hasExplicitScope,
        ref RECT wr)
    {
        try
        {
            using var automation = new FlaUI.UIA3.UIA3Automation();

            if (hasExplicitScope || focusDrillRequested)
            {
                var resolved = GrapHelper.ResolveFullGrap(grapFull, automation);
                if (resolved.HasValue)
                {
                    var (_, root, error) = resolved.Value;
                    if (error == null && root != null)
                    {
                        var r = root.Properties.BoundingRectangle.ValueOrDefault;
                        if (r.Width > 0 && r.Height > 0)
                        {
                            wr.Left = r.X;
                            wr.Top = r.Y;
                            wr.Right = r.X + r.Width;
                            wr.Bottom = r.Y + r.Height;
                            Console.WriteLine($"[HACK] Scoped to grap root rect=({r.X},{r.Y} {r.Width}x{r.Height})");
                        }
                    }
                }
                return;
            }

            if (!string.IsNullOrEmpty(uiaScope)) return;

            // ── 분석 범위 결정 ──
            // UIA 정보가 있으면: 타겟 노드의 Parent 영역으로 좁힘
            // UIA 정보 없으면: 타겟 창(hwnd)의 부모 창으로 제한
            // 타겟 창이 루트 창이면: 그냥 루트 창 전체 분석 (이미 wr에 설정됨)
            try
            {
                // 1. UIA로 타겟 찾아서 Parent로 좁히기
                var root = automation.FromHandle(hwnd);
                var grapPattern = grapFull.Split('#')[0];
                var grapResult = GrapHelper.FindByNameOrAid(root, grapPattern);
                if (grapResult != null)
                {
                    var parent = grapResult.Parent;
                    var analysisEl = parent ?? grapResult; // parent 없으면 타겟 자체
                    var aRect = analysisEl.Properties.BoundingRectangle.ValueOrDefault;
                    if (aRect.Width > 0 && aRect.Height > 0)
                    {
                        wr.Left = aRect.X; wr.Top = aRect.Y;
                        wr.Right = aRect.X + aRect.Width; wr.Bottom = aRect.Y + aRect.Height;
                        Console.WriteLine($"[HACK] UIA parent-narrowed: rect=({aRect.X},{aRect.Y} {aRect.Width}x{aRect.Height})");
                        return;
                    }
                }
            }
            catch { }

            // 1b. Cursor-based: UIA ElementFromPoint → most specific element under cursor
            try
            {
                NativeMethods.GetCursorPos(out var cursorPt);
                var pointed = automation.FromPoint(new System.Drawing.Point(cursorPt.X, cursorPt.Y));
                if (pointed != null)
                {
                    // Walk up to find an element with reasonable size (not 1px noise)
                    var target = pointed;
                    var tRect = target.Properties.BoundingRectangle.ValueOrDefault;

                    // Use parent for analysis context (shows target + siblings)
                    var parent = target.Parent;
                    if (parent != null)
                    {
                        var pRect = parent.Properties.BoundingRectangle.ValueOrDefault;
                        if (pRect.Width > 0 && pRect.Height > 0
                            && pRect.Width < (wr.Right - wr.Left) * 0.95) // parent smaller than full window
                        {
                            wr.Left = pRect.X; wr.Top = pRect.Y;
                            wr.Right = pRect.X + pRect.Width; wr.Bottom = pRect.Y + pRect.Height;
                            Console.WriteLine($"[HACK] ElementFromPoint parent: rect=({pRect.X},{pRect.Y} {pRect.Width}x{pRect.Height}) target=\"{tRect.Width}x{tRect.Height}\"");
                            return;
                        }
                    }

                    // No useful parent → use target element itself if smaller than window
                    if (tRect.Width > 0 && tRect.Height > 0
                        && tRect.Width * tRect.Height < (wr.Right - wr.Left) * (wr.Bottom - wr.Top) * 0.8)
                    {
                        wr.Left = tRect.X; wr.Top = tRect.Y;
                        wr.Right = tRect.X + tRect.Width; wr.Bottom = tRect.Y + tRect.Height;
                        Console.WriteLine($"[HACK] ElementFromPoint direct: rect=({tRect.X},{tRect.Y} {tRect.Width}x{tRect.Height})");
                        return;
                    }
                }
            }
            catch { }

            // 2. UIA 실패 → 타겟 창의 부모 창(Win32 Owner/Parent)으로 제한
            var parentHwnd = NativeMethods.GetParent(hwnd);
            if (parentHwnd == IntPtr.Zero)
                parentHwnd = NativeMethods.GetAncestor(hwnd, 2); // GA_ROOT
            if (parentHwnd != IntPtr.Zero && parentHwnd != hwnd)
            {
                NativeMethods.GetWindowRect(parentHwnd, out var parentWr);
                if (parentWr.Right - parentWr.Left > 0 && parentWr.Bottom - parentWr.Top > 0)
                {
                    // 타겟 창 RECT를 부모 창으로 클램프
                    wr.Left = Math.Max(wr.Left, parentWr.Left);
                    wr.Top = Math.Max(wr.Top, parentWr.Top);
                    wr.Right = Math.Min(wr.Right, parentWr.Right);
                    wr.Bottom = Math.Min(wr.Bottom, parentWr.Bottom);
                    Console.WriteLine($"[HACK] Win32 parent-clamped: rect=({wr.Left},{wr.Top} {wr.Right - wr.Left}x{wr.Bottom - wr.Top})");
                    return;
                }
            }
            // 3. 타겟 창 = 루트 창 → wr 그대로 (창 전체 분석)
            Console.WriteLine($"[HACK] Root window — analyzing full: rect=({wr.Left},{wr.Top} {wr.Right - wr.Left}x{wr.Bottom - wr.Top})");
        }
        catch
        {
            Console.WriteLine("[HACK] Scope/focus narrowing error - using full window");
        }
    }

    static Rectangle TryResolveAnalysisTargetRect(RECT wr)
    {
        var captureRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        if (captureRect.Width <= 1 || captureRect.Height <= 1)
            return new Rectangle(0, 0, 1, 1);

        try
        {
            if (NativeMethods.TryGetCurrentCursorRect(out var cursorRect) && captureRect.Contains(cursorRect))
            {
                return new Rectangle(
                    cursorRect.X - wr.Left,
                    cursorRect.Y - wr.Top,
                    Math.Max(1, cursorRect.Width),
                    Math.Max(1, cursorRect.Height));
            }
        }
        catch
        {
        }

        return new Rectangle(
            Math.Max(0, captureRect.Width / 2),
            Math.Max(0, captureRect.Height / 2),
            1,
            1);
    }

    static List<ConnectedComponentAnalyzer.Region> OrderRegionsForTarget(
        List<ConnectedComponentAnalyzer.Region> regions,
        Rectangle targetRect,
        int captureWidth,
        int captureHeight)
    {
        if (regions.Count <= 1)
            return regions;

        var captureBounds = new Rectangle(0, 0, Math.Max(1, captureWidth), Math.Max(1, captureHeight));
        if (!captureBounds.Contains(targetRect))
            targetRect = new Rectangle(
                Math.Clamp(targetRect.X, 0, captureBounds.Width - 1),
                Math.Clamp(targetRect.Y, 0, captureBounds.Height - 1),
                Math.Max(1, targetRect.Width),
                Math.Max(1, targetRect.Height));

        int TargetPriority(ConnectedComponentAnalyzer.Region region)
        {
            var bounds = region.Bounds;
            if (ContainsRect(bounds, targetRect))
                return 0;
            if (bounds.IntersectsWith(targetRect))
                return 1;
            return 2;
        }

        int DistanceToTarget(ConnectedComponentAnalyzer.Region region)
        {
            var bounds = region.Bounds;
            var rx = bounds.Left + bounds.Width / 2;
            var ry = bounds.Top + bounds.Height / 2;
            var tx = targetRect.Left + targetRect.Width / 2;
            var ty = targetRect.Top + targetRect.Height / 2;
            return Math.Abs(rx - tx) + Math.Abs(ry - ty);
        }

        return regions
            .OrderBy(TargetPriority)
            .ThenByDescending(region => ContainsRect(region.Bounds, targetRect) ? region.Bounds.Width * region.Bounds.Height : 0)
            .ThenBy(DistanceToTarget)
            .ThenBy(region => region.Bounds.Width * region.Bounds.Height)
            .ThenBy(region => region.Bounds.Top)
            .ThenBy(region => region.Bounds.Left)
            .ToList();
    }

    static bool TryResolveFocusedCaptureRect(
        FlaUI.UIA3.UIA3Automation automation,
        IntPtr hwnd,
        out Rectangle rect,
        out string description)
    {
        rect = Rectangle.Empty;
        description = "";

        try
        {
            if (NativeMethods.GetForegroundWindow() != hwnd)
                return false;

            var root = automation.FromHandle(hwnd);
            if (root == null) return false;

            var focused = GrapHelper.FindFocusedLeaf(automation, root, hwnd);
            if (focused == null) return false;

            var focusedRect = TryGetScreenRect(focused);
            if (focusedRect.Width <= 1 || focusedRect.Height <= 1) return false;
            NativeMethods.GetWindowRect(hwnd, out var wr);
            var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
            var anchorRect = TryGetPreferredFocusAnchorRect(focused, focusedRect, winRect);

            AutomationElement? best = focused;
            int bestScore = ScoreHackFocusCandidate(focused, focusedRect, anchorRect, winRect);
            var cur = focused;
            for (int depth = 0; depth < 6 && cur != null; depth++)
            {
                var candidateRect = TryGetScreenRect(cur);
                if (candidateRect.Width <= 1 || candidateRect.Height <= 1) break;
                if (!IsRectInsideWindow(candidateRect, hwnd)) break;
                if (!ContainsRect(candidateRect, anchorRect))
                {
                    try { cur = cur.Parent; } catch { break; }
                    continue;
                }

                int candidateScore = ScoreHackFocusCandidate(cur, focusedRect, anchorRect, winRect);
                if (candidateScore > bestScore)
                {
                    best = cur;
                    bestScore = candidateScore;
                }

                try { cur = cur.Parent; } catch { break; }
            }

            var bestRect = InflateWithinWindow(TryGetScreenRect(best), hwnd, 12, 10);
            if (bestRect.Width <= 1 || bestRect.Height <= 1) return false;

            rect = bestRect;
            description = $"[{SafeControlType(best)}] \"{SafeName(best)}\"";
            return true;
        }
        catch
        {
            return false;
        }
    }

    static Rectangle TryGetPreferredFocusAnchorRect(AutomationElement focused, Rectangle focusedRect, Rectangle winRect)
    {
        try
        {
            if (focused.Patterns.Text.IsSupported)
            {
                var ranges = focused.Patterns.Text.Pattern.GetSelection();
                if (ranges != null)
                {
                    var best = Rectangle.Empty;
                    foreach (var range in ranges)
                    {
                        try
                        {
                            var rects = range.GetBoundingRectangles();
                            if (rects == null) continue;
                            foreach (var rr in rects)
                            {
                                var candidate = Rectangle.FromLTRB(
                                    (int)Math.Floor((double)rr.X),
                                    (int)Math.Floor((double)rr.Y),
                                    (int)Math.Ceiling((double)(rr.X + rr.Width)),
                                    (int)Math.Ceiling((double)(rr.Y + rr.Height)));
                                if (candidate.Width <= 0 || candidate.Height <= 0) continue;
                                candidate = Rectangle.Intersect(candidate, winRect);
                                if (candidate.Width <= 0 || candidate.Height <= 0) continue;
                                if (best == Rectangle.Empty || candidate.Width * candidate.Height < best.Width * best.Height)
                                    best = candidate;
                            }
                        }
                        catch { }
                    }

                    if (best != Rectangle.Empty)
                        return best;
                }
            }
        }
        catch { }

        return focusedRect;
    }

    static int ScoreHackFocusCandidate(
        AutomationElement candidate,
        Rectangle focusedRect,
        Rectangle anchorRect,
        Rectangle winRect)
    {
        var rect = TryGetScreenRect(candidate);
        if (rect.Width <= 1 || rect.Height <= 1) return int.MinValue;
        if (!ContainsRect(rect, anchorRect)) return int.MinValue;

        var ct = SafeControlType(candidate);
        bool isPrimary = ct is "Document" or "Edit";
        bool isSecondary = ct is "Pane";
        bool isFallback = ct is "Group";
        if (!isPrimary && !isSecondary && !isFallback) return int.MinValue;

        int minWidth = isPrimary ? Math.Max(320, anchorRect.Width * 3) : Math.Max(260, focusedRect.Width * 2);
        int minHeight = isPrimary ? Math.Max(72, anchorRect.Height * 4) : Math.Max(48, focusedRect.Height * 2);
        int maxWidth = isPrimary ? Math.Min(winRect.Width, 1800) : Math.Min(winRect.Width, 1400);
        int maxHeight = isPrimary ? Math.Min(winRect.Height, 1100) : Math.Min(winRect.Height, 520);

        if (rect.Width < minWidth || rect.Height < minHeight) return int.MinValue / 2;
        if (rect.Width > maxWidth || rect.Height > maxHeight) return int.MinValue / 2;

        int score = ct switch
        {
            "Document" => 150,
            "Edit" => 135,
            "Pane" => 90,
            "Group" => 45,
            _ => 0
        };

        score += Math.Min(90, rect.Width / 18);
        score += Math.Min(90, rect.Height / 12);

        bool nearBottom = rect.Bottom >= winRect.Bottom - Math.Max(90, winRect.Height / 7);
        bool bannerLike = rect.Height <= Math.Max(160, winRect.Height / 4) && rect.Width <= Math.Max(700, winRect.Width / 2);
        if (nearBottom && bannerLike)
            score -= 160;

        if (ct == "Group" && rect.Height <= Math.Max(180, winRect.Height / 3))
            score -= 50;

        if (ct == "Pane" && rect.Height <= Math.Max(90, anchorRect.Height * 3))
            score -= 30;

        var name = SafeName(candidate);
        if (!string.IsNullOrWhiteSpace(name) && name.Length <= 60)
            score += 8;

        return score;
    }

    static bool ContainsRect(Rectangle outer, Rectangle inner)
    {
        return inner.Left >= outer.Left
            && inner.Top >= outer.Top
            && inner.Right <= outer.Right
            && inner.Bottom <= outer.Bottom;
    }

    static Rectangle TryGetScreenRect(AutomationElement? element)
    {
        if (element == null) return Rectangle.Empty;
        try
        {
            var r = element.Properties.BoundingRectangle.ValueOrDefault;
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }
        catch
        {
            return Rectangle.Empty;
        }
    }

    static string SafeControlType(AutomationElement? element)
    {
        if (element == null) return "?";
        try { return element.Properties.ControlType.ValueOrDefault.ToString(); }
        catch { return "?"; }
    }

    static string SafeName(AutomationElement? element)
    {
        if (element == null) return "";
        try { return element.Properties.Name.ValueOrDefault ?? ""; }
        catch { return ""; }
    }

    static bool IsRectInsideWindow(Rectangle rect, IntPtr hwnd)
    {
        NativeMethods.GetWindowRect(hwnd, out var wr);
        var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        return winRect.IntersectsWith(rect) && rect.Left >= winRect.Left && rect.Top >= winRect.Top;
    }

    static Rectangle InflateWithinWindow(Rectangle rect, IntPtr hwnd, int dx, int dy)
    {
        NativeMethods.GetWindowRect(hwnd, out var wr);
        var winRect = Rectangle.FromLTRB(wr.Left, wr.Top, wr.Right, wr.Bottom);
        return Rectangle.Intersect(Rectangle.Inflate(rect, dx, dy), winRect);
    }

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
                // Skip tiny segments (smaller than readable character)
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

                // Label: UIA type or Dy type
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
                        var trimmed = TrimPreview(ocrTxt, (int)(halfW / 5)); // ~5px per char
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
        Dictionary<int, string>? stageLabels = null)
    {
        var boxes = new List<A11yHackOverlayBox>(regions.Count);

        // Determine scope: target's parent (contains target) and siblings (share same parent)
        var targetBounds = regions.Count > 0 ? regions[0].Bounds : Rectangle.Empty;
        // Parent = smallest region that fully contains the target (excluding target itself)
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

            // Skip tiny segments (smaller than a readable character ~10x10)
            if (i > 0 && region.Bounds.Width < 10 && region.Bounds.Height < 10)
                continue;

            var bounds = new System.Windows.Rect(
                region.Bounds.X,
                region.Bounds.Y,
                Math.Max(1, region.Bounds.Width),
                Math.Max(1, region.Bounds.Height));

            // Determine role
            bool hasUia = uiaAnswers != null && uiaAnswers.ContainsKey(i);
            HackBoxRole role;
            if (i == 0)
                role = HackBoxRole.Target;
            else if (i == parentIdx)
                role = HackBoxRole.Scope;
            else if (parentIdx >= 0 && parentBounds.Contains(region.Bounds))
                role = HackBoxRole.Scope;
            else if (hasUia)
                role = HackBoxRole.Known; // system a11y node → dashed
            else
                continue; // no UIA, not in scope → skip

            // Label: UIA type or Dy type + id + size
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
                // Dy type label for non-UIA segments
                label = $"{region.Type} {region.Bounds.Width}x{region.Bounds.Height}";
            }

            // Extract OCR text from stageLabel + promote to Cached if experience DB hit
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
        return boxes;
    }

    static void UpdateHackOverlay(
        A11yHackOverlayHost? liveOverlay,
        Bitmap source,
        List<ConnectedComponentAnalyzer.Region> regions,
        Dictionary<int, (string name, string type, string aid)>? uiaAnswers = null,
        Dictionary<int, string>? stageLabels = null)
    {
        if (liveOverlay == null) return;
        var boxes = BuildHackOverlayBoxes(regions, uiaAnswers, stageLabels);
        liveOverlay.Update(new A11yHackOverlayModel(boxes));
    }

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

    static void CacheSegment(Bitmap source, Rectangle bounds, int imgW, int imgH,
        string dynId, string description, bool isContainer = false)
    {
        try
        {
            var cropRect = Rectangle.Intersect(bounds, new Rectangle(0, 0, imgW, imgH));
            if (cropRect.Width <= 0 || cropRect.Height <= 0) return;
            using var crop = source.Clone(cropRect, source.PixelFormat);
            var hash = OcrGapCollector.ComputePixelHash(crop);

            // Build experience DB path
            var baseDir = _hackExpDir ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WKAppBot", "gap_cache");

            string targetDir;
            if (isContainer)
            {
                // Container = subfolder in experience DB
                targetDir = Path.Combine(baseDir, $"hack_{dynId}");
                _currentContainerDir = targetDir;
            }
            else
            {
                // Child goes into current container folder, or root if no container
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

