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
        using var liveOverlay = A11yHackOverlayHost.TryStart(wr.Left, wr.Top, w, h);
        liveOverlay?.Update(new A11yHackOverlayModel(new[]
        {
            new A11yHackOverlayBox(
                new System.Windows.Rect(0, 0, Math.Max(1, w - 1), Math.Max(1, h - 1)),
                System.Windows.Media.Colors.Cyan,
                "target",
                2.5)
        }));
        liveOverlay?.StartHoverTracking(wr.Left, wr.Top);

        // ── User input abort: any keyboard/mouse activity → stop analysis + hide overlay ──
        var hackAborted = false;
        var inputBaselineTick = Environment.TickCount; // baseline: ignore input before hack starts
        bool HackShouldAbort()
        {
            if (hackAborted) return true;
            var idleMs = WKAppBot.Win32.Native.NativeMethods.GetUserIdleMs();
            // Only abort if input happened AFTER hack started (idle < elapsed since start)
            var elapsed = (uint)(Environment.TickCount - inputBaselineTick);
            if (idleMs < elapsed && idleMs < 500)
            {
                hackAborted = true;
                Console.WriteLine("[HACK] User input detected — aborting analysis, hiding overlay");
                liveOverlay?.Hide();
                return true;
            }
            return false;
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
        InitHackExpDir(hwnd);
        PulseStep.Mark("captured");

        if (HackShouldAbort()) { bmp.Dispose(); return 0; }

        // CCA segmentation
        var cca = new ConnectedComponentAnalyzer();
        var regions = cca.AnalyzeAndMerge(bmp);
        regions = OrderRegionsForTarget(regions, analysisTargetRect, w, h);
        PulseStep.Mark("cca-done");

        int textCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Text);
        int iconCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Icon);
        int sepCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Separator);
        int contCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Container);
        int noiseCount = regions.Count(r => r.Type == ConnectedComponentAnalyzer.RegionType.Noise);

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

        if (HackShouldAbort()) { bmp.Dispose(); return 0; }
        // UIA answer key: scan UIA tree, build rect→info map for cross-reference
        var uiaAnswers = new Dictionary<int, (string name, string type, string aid)>();
        var _uiaBestArea = new Dictionary<int, int>(); // track smallest match per region
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
            SaveHackOverlayPreview(bmp, regions, "uia", wr.Left, wr.Top, uiaAnswers);
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

        if (HackShouldAbort()) { bmp.Dispose(); return 0; }
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
                        isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.Container);
                    // Mismatch detection: CCA type vs UIA type ??stderr for self-healing
                    var ccaType = region.Type.ToString();
                    var uiaType = uia.type;
                    if (!string.IsNullOrEmpty(uiaType))
                    {
                        bool match = (ccaType == "Text" && uiaType is "Text" or "Edit" or "Document")
                                  || (ccaType == "Icon" && uiaType is "Image" or "Button")
                                  || (ccaType == "Container" && uiaType is "Group" or "Pane" or "Window")
                                  || (ccaType == "Separator" && uiaType is "Separator");
                        if (!match)
                            Console.Error.WriteLine($"[MISMATCH] {dynId}: CCA={ccaType} UIA={uiaType} \"{uiaLabel}\" d={region.Density:F2} ar={region.AspectRatio:F1}");
                    }

                    if (region.Type == ConnectedComponentAnalyzer.RegionType.Text
                        || region.Type == ConnectedComponentAnalyzer.RegionType.Container)
                    {
                        try
                        {
                            var cropRect = Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h));
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
                else if (region.Type == ConnectedComponentAnalyzer.RegionType.Text
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
                                    isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.Container);
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
                                        isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.Container);
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
                    ConnectedComponentAnalyzer.RegionType.Text => "Text",
                    ConnectedComponentAnalyzer.RegionType.Icon => "Icon",
                    ConnectedComponentAnalyzer.RegionType.Container => "Container",
                    ConnectedComponentAnalyzer.RegionType.Separator => "──",
                    _ => "쨌"
                };
                if (region.Type == ConnectedComponentAnalyzer.RegionType.Noise) continue; // skip noise in tree

                string label;
                if (uiaLabel.Length > 0)
                    label = $" \"{(uiaLabel.Length > 50 ? uiaLabel[..50] + "..." : uiaLabel)}\"";
                else if (ocrText.Length > 0)
                    label = $" \"{(ocrText.Length > 50 ? ocrText[..50] + "..." : ocrText)}\"";
                else
                    label = region.Type == ConnectedComponentAnalyzer.RegionType.Text ? " [?]" : "";
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
        if (gapCollector.HasGaps)
        {
            Console.WriteLine($"  Vision needed: {gapCollector.Count} blind region(s)");
            for (int ri = 0; ri < regions.Count; ri++)
            {
                if (!stageLabels.ContainsKey(ri) && regions[ri].Type is ConnectedComponentAnalyzer.RegionType.Text or ConnectedComponentAnalyzer.RegionType.Container)
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
        bmp.Dispose();
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
            if (!TryResolveFocusedCaptureRect(automation, hwnd, out var focusRect, out var focusDesc)) return;

            wr.Left = focusRect.Left;
            wr.Top = focusRect.Top;
            wr.Right = focusRect.Right;
            wr.Bottom = focusRect.Bottom;
            Console.WriteLine($"[HACK] Focus-narrowed: {focusDesc} rect=({focusRect.Left},{focusRect.Top} {focusRect.Width}x{focusRect.Height})");
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
        Dictionary<int, (string name, string type, string aid)>? uiaAnswers = null)
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
                if (region.Type == ConnectedComponentAnalyzer.RegionType.Noise) continue;

                var color = region.Type switch
                {
                    ConnectedComponentAnalyzer.RegionType.Text => Color.LimeGreen,
                    ConnectedComponentAnalyzer.RegionType.Icon => Color.DeepSkyBlue,
                    ConnectedComponentAnalyzer.RegionType.Container => Color.Orange,
                    ConnectedComponentAnalyzer.RegionType.Separator => Color.Gold,
                    _ => Color.Gray
                };

                using var pen = new Pen(Color.FromArgb(230, color), region.Type == ConnectedComponentAnalyzer.RegionType.Container ? 2.5f : 1.6f);
                g.DrawRectangle(pen, region.Bounds);

                // Top-left label: index + type (above rectangle)
                var label = $"{i}:{region.Type}";
                var labelSize = g.MeasureString(label, font);
                var lx = Math.Max(0, region.Bounds.Left);
                var ly = Math.Max(0, region.Bounds.Top - (int)labelSize.Height - 2);
                if (ly + labelSize.Height > overlay.Height) ly = Math.Max(0, overlay.Height - (int)labelSize.Height - 1);
                if (lx + labelSize.Width > overlay.Width) lx = Math.Max(0, overlay.Width - (int)labelSize.Width - 1);

                g.FillRectangle(textBg, lx, ly, labelSize.Width + 6, labelSize.Height + 2);
                g.DrawString(label, font, textFg, lx + 3, ly + 1);

                // Top-right label: UIA ControlType:AutomationId (e.g. "Button:btnOk", "Edit:#3")
                // No AutomationId → sibling index (#1, #2, ...). Small font, inside rectangle.
                if (uiaAnswers != null && uiaAnswers.TryGetValue(i, out var uia))
                {
                    var parts = new List<string>();
                    if (!string.IsNullOrWhiteSpace(uia.type)) parts.Add(uia.type);
                    if (!string.IsNullOrWhiteSpace(uia.aid)) parts.Add(uia.aid);
                    else if (!string.IsNullOrWhiteSpace(uia.name)) parts.Add(TrimPreview(uia.name, 20));
                    else parts.Add($"#{i + 1}"); // sibling index fallback
                    if (parts.Count > 0)
                    {
                        var nodeLabel = string.Join(":", parts);
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
        for (int i = 0; i < regions.Count; i++)
        {
            var region = regions[i];

            var bounds = new System.Windows.Rect(
                region.Bounds.X,
                region.Bounds.Y,
                Math.Max(1, region.Bounds.Width),
                Math.Max(1, region.Bounds.Height));
            var title = i == 0 ? "TARGET" : $"NODE {i:00}";
            var label = $"{title} | {region.Type}";

            if (uiaAnswers != null && uiaAnswers.TryGetValue(i, out var uia))
            {
                var name = !string.IsNullOrWhiteSpace(uia.name) ? uia.name : uia.aid;
                if (!string.IsNullOrWhiteSpace(name))
                    label += $" | {TrimPreview(name, 24)}";
            }
            if (stageLabels != null && stageLabels.TryGetValue(i, out var stage) && !string.IsNullOrWhiteSpace(stage))
                label += $" | {TrimPreview(stage, 28)}";

            boxes.Add(new A11yHackOverlayBox(
                bounds,
                region.Type == ConnectedComponentAnalyzer.RegionType.Noise
                    ? System.Windows.Media.Color.FromArgb(128, 0x00, 0x64, 0x00)
                    : System.Windows.Media.Color.FromArgb(128, 0x32, 0xCD, 0x32),
                label,
                0.9));
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

    static A11yHackOverlayPreview? BuildHackOverlayPreview(
        Bitmap source,
        List<ConnectedComponentAnalyzer.Region> regions,
        Dictionary<int, string>? stageLabels = null)
    {
        if (regions.Count == 0) return null;

        int targetIndex = 0;
        if (regions[targetIndex].Type == ConnectedComponentAnalyzer.RegionType.Noise)
        {
            var firstNonNoise = regions.FindIndex(r => r.Type != ConnectedComponentAnalyzer.RegionType.Noise);
            if (firstNonNoise >= 0) targetIndex = firstNonNoise;
        }

        var target = regions[targetIndex].Bounds;
        if (target.Width <= 0 || target.Height <= 0) return null;

        int srcW = source.Width;
        int srcH = source.Height;
        int focusCx = target.Left + target.Width / 2;
        int focusCy = target.Top + target.Height / 2;
        int sampleW = Math.Clamp(Math.Max(target.Width * 2, 260), 260, Math.Min(srcW, 900));
        int sampleH = Math.Clamp(Math.Max(target.Height * 3, 160), 160, Math.Min(srcH, 520));
        var sample = Rectangle.FromLTRB(
            Math.Max(0, focusCx - sampleW / 2),
            Math.Max(0, focusCy - sampleH / 2),
            Math.Min(srcW, focusCx + sampleW / 2),
            Math.Min(srcH, focusCy + sampleH / 2));
        if (sample.Width <= 8 || sample.Height <= 8) return null;

        const int previewW = 360;
        int previewH = Math.Clamp((int)Math.Round(previewW * (sample.Height / (double)sample.Width)), 180, 300);
        using var warped = new Bitmap(previewW, previewH, source.PixelFormat);
        double centerX = (focusCx - sample.Left) / (double)sample.Width;
        double centerY = (focusCy - sample.Top) / (double)sample.Height;
        centerX = Math.Clamp(centerX, 0.18, 0.82);
        centerY = Math.Clamp(centerY, 0.18, 0.82);
        double maxZoom = 3.0;

        for (int y = 0; y < previewH; y++)
        {
            double ny = previewH <= 1 ? 0 : y / (double)(previewH - 1);
            for (int x = 0; x < previewW; x++)
            {
                double nx = previewW <= 1 ? 0 : x / (double)(previewW - 1);
                double dx = nx - centerX;
                double dy = ny - centerY;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                double zoom = 1.0 + (maxZoom - 1.0) * Math.Pow(Math.Max(0.0, 1.0 - Math.Min(1.0, dist * 2.15)), 2.15);
                double sxNorm = centerX + dx / zoom;
                double syNorm = centerY + dy / zoom;
                int sx = Math.Clamp(sample.Left + (int)Math.Round(sxNorm * (sample.Width - 1)), 0, srcW - 1);
                int sy = Math.Clamp(sample.Top + (int)Math.Round(syNorm * (sample.Height - 1)), 0, srcH - 1);
                warped.SetPixel(x, y, source.GetPixel(sx, sy));
            }
        }

        using (var g = Graphics.FromImage(warped))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.LimeGreen, 2f);
            int markerX = Math.Clamp((int)Math.Round(centerX * previewW), 12, previewW - 12);
            int markerY = Math.Clamp((int)Math.Round(centerY * previewH), 12, previewH - 12);
            g.DrawEllipse(pen, markerX - 7, markerY - 7, 14, 14);
            g.DrawLine(pen, markerX - 10, markerY, markerX + 10, markerY);
            g.DrawLine(pen, markerX, markerY - 10, markerX, markerY + 10);
        }

        var bitmapSource = ConvertHackPreviewToBitmapSource(warped);
        if (bitmapSource == null) return null;
        string header = stageLabels != null && stageLabels.TryGetValue(targetIndex, out var stage) && !string.IsNullOrWhiteSpace(stage)
            ? $"TARGET 3x | {TrimPreview(stage, 40)}"
            : "TARGET 3x";
        return new A11yHackOverlayPreview(bitmapSource, previewW + 8, previewH + 28, header);
    }

    static System.Windows.Media.Imaging.BitmapSource? ConvertHackPreviewToBitmapSource(Bitmap bitmap)
    {
        try
        {
            using var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            var image = new System.Windows.Media.Imaging.PngBitmapDecoder(
                ms,
                System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat,
                System.Windows.Media.Imaging.BitmapCacheOption.OnLoad).Frames[0];
            image.Freeze();
            return image;
        }
        catch
        {
            return null;
        }
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

