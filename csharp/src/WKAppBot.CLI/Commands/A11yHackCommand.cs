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
            Console.WriteLine("Usage: wkappbot a11y hack <grap>[#scope] [options]");
            Console.WriteLine("  --at x,y       screen point (override cursor for ElementFromPoint)");
            Console.WriteLine("  --ltrb l,t,r,b screen rect (left,top,right,bottom)");
            Console.WriteLine("  --ltwh l,t,w,h screen rect (left,top,width,height)");
            Console.WriteLine("  --engine gemini|gpt  vision engine");
            Console.WriteLine("  Pipeline: capture → CCA → OCR → Vision → dynamic a11y tree");
            return 1;
        }

        // Parse options
        string visionEngine = "gemini";
        int? atX = null, atY = null; // --at x,y (point) or --ltrb l,t,r,b (rect)
        Rectangle? atRect = null;
        for (int ai = 0; ai < args.Length; ai++)
        {
            if (args[ai] == "--engine" && ai + 1 < args.Length) visionEngine = args[++ai].ToLowerInvariant();
            else if (args[ai] == "--at" && ai + 1 < args.Length)
            {
                var parts = args[++ai].Split(',');
                if (parts.Length >= 2 && int.TryParse(parts[0], out var px) && int.TryParse(parts[1], out var py))
                { atX = px; atY = py; }
            }
            else if (args[ai] is "--ltrb" or "--ltwh" && ai + 1 < args.Length)
            {
                var isLtwh = args[ai] == "--ltwh";
                var parts = args[++ai].Split(',');
                if (parts.Length == 4 && int.TryParse(parts[0], out var a1) && int.TryParse(parts[1], out var a2)
                    && int.TryParse(parts[2], out var a3) && int.TryParse(parts[3], out var a4))
                {
                    atRect = isLtwh ? new Rectangle(a1, a2, a3, a4) : Rectangle.FromLTRB(a1, a2, a3, a4);
                    atX = atRect.Value.Left + atRect.Value.Width / 2;
                    atY = atRect.Value.Top + atRect.Value.Height / 2;
                }
            }
        }

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
        if (!_hackHoverQuiet)
            if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Target: 0x{hwnd.ToInt64():X} \"{win.Title}\"");
        PulseStep.Mark("target-found");

        // If #scope specified, find UIA element and use its BoundingRectangle
        NativeMethods.GetWindowRect(hwnd, out var wr);
        // Save full window rect for UIA standalone overlay (before narrowing)
        var fullWr = wr;
        if (atRect.HasValue)
        {
            // --ltrb direct rect override (skip auto-narrow)
            var ar = atRect.Value;
            wr.Left = ar.Left; wr.Top = ar.Top; wr.Right = ar.Right; wr.Bottom = ar.Bottom;
            if (!_hackHoverQuiet) Console.WriteLine($"[HACK] --ltrb override: rect=({ar.Left},{ar.Top} {ar.Width}x{ar.Height})");
        }
        else
        {
            TryAutoNarrowHackRect(hwnd, grapFull, uiaScope, focusDrillRequested, hasExplicitScope, ref wr, atX, atY);
        }
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
                    if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Scoped to: \"{uiaScope}\" rect=({r.X},{r.Y} {r.Width}x{r.Height})");
                }
                else if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Scope \"{uiaScope}\" not found ??using full window");
            }
            catch { if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Scope error ??using full window"); }
        }
        int w = wr.Right - wr.Left, h = wr.Bottom - wr.Top;
        if (w <= 0 || h <= 0)
        {
            Console.Error.WriteLine("[HACK] Window has zero size");
            return 1;
        }
        var analysisTargetRect = TryResolveAnalysisTargetRect(wr);
        // Overlay covers full root window (UIA tree needs full coverage)
        int fullWw = fullWr.Right - fullWr.Left, fullWh = fullWr.Bottom - fullWr.Top;
        // CCA offset: narrowed area position relative to full window
        int ccaOffX = wr.Left - fullWr.Left, ccaOffY = wr.Top - fullWr.Top;
        var liveOverlay = A11yHackOverlayHost.GetOrCreate(OverlaySlot.Session, fullWr.Left, fullWr.Top, fullWw, fullWh);
        liveOverlay?.Update(new A11yHackOverlayModel(new[]
        {
            new A11yHackOverlayBox(
                new System.Windows.Rect(ccaOffX, ccaOffY, Math.Max(1, w - 1), Math.Max(1, h - 1)),
                "target", Role: HackBoxRole.Target)
        }));
        liveOverlay?.StartHoverTracking(fullWr.Left, fullWr.Top);

        // ── Abort checks (two tiers) ──
        var hackAborted = false;
        var inputBaselineTick = Environment.TickCount;
        RECT baselineWr = wr;
        long baselinePixelHash = 0;
        Rectangle targetScreenRect = Rectangle.Empty;
        int lastChangCheckTick = Environment.TickCount;

        // Auto-hack (WKAPPBOT_WORKER=1) = sensitive abort; manual = lenient
        bool isAutoHack = Environment.GetEnvironmentVariable("WKAPPBOT_WORKER") == "1";
        int abortIdleThresholdMs = isAutoHack ? 1000 : 3000;

        // Tier 1: user input only (cheap — called per OCR item)
        bool HackShouldAbort()
        {
            if (hackAborted) return true;
            var idleMs = NativeMethods.GetUserIdleMs();
            var elapsed = (uint)(Environment.TickCount - inputBaselineTick);
            if (idleMs < elapsed && idleMs < abortIdleThresholdMs)
            {
                hackAborted = true;
                if (!_hackHoverQuiet) Console.WriteLine("[HACK] User input detected — aborting");
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
                if (!_hackHoverQuiet) Console.WriteLine("[HACK] Window moved/resized — aborting");
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
                    if (!_hackHoverQuiet) Console.WriteLine("[HACK] Content changed — aborting for re-analysis");
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
        if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Captured: {w}x{h}");
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

        if (!_hackHoverQuiet) Console.WriteLine($"[HACK] CCA: {regions.Count} regions ??Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount} Noise={noiseCount}");
        SaveHackOverlayPreview(bmp, regions, "cca", wr.Left, wr.Top);
        var stageLabels = new Dictionary<int, string>();
        UpdateHackOverlay(liveOverlay, bmp, regions, null, stageLabels, null, ccaOffX, ccaOffY);

        // Table detection
        var allRegions = cca.Analyze(bmp); // non-merged for table detection
        var table = cca.DetectTable(allRegions, w, h);
        if (table != null)
            if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Table detected: {table.Rows} rows 횞 {table.Cols} cols");
        PulseStep.Mark("table-detect");

        if (HackTargetChanged()) { bmp.Dispose(); return 0; }
        // UIA answer key: scan UIA tree, build rect→info map for cross-reference
        var uiaAnswers = new Dictionary<int, (string name, string type, string aid)>();
        var uiaBounds = new Dictionary<int, Rectangle>(); // UIA element screen rect (most specific)
        var _uiaBestArea = new Dictionary<int, int>();
        List<A11yHackOverlayBox>? uiaStandaloneBoxes = null;
        try
        {
            PulseStep.Mark("uia-scan");
            using var automation = new FlaUI.UIA3.UIA3Automation();
            var uiaRoot = automation.FromHandle(hwnd);
            // Depth-limited BFS instead of FindAllDescendants (Electron can take 60s+)
            var uiaElements = new List<FlaUI.Core.AutomationElements.AutomationElement>();
            {
                var q = new Queue<(FlaUI.Core.AutomationElements.AutomationElement el, int d)>();
                q.Enqueue((uiaRoot, 0));
                const int scanDepth = 4;
                while (q.Count > 0 && uiaElements.Count < 500)
                {
                    var (n, d) = q.Dequeue();
                    uiaElements.Add(n);
                    if (d < scanDepth)
                    {
                        try { foreach (var c in n.FindAllChildren()) q.Enqueue((c, d + 1)); }
                        catch { }
                    }
                }
            }
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
            if (!_hackHoverQuiet) Console.WriteLine($"[HACK] UIA answer key: {uiaMatched}/{regions.Count} segments matched");

            // Collect standalone UIA boxes from FULL root window (depth-limited for speed)
            // FindAllDescendants on Electron can take 60s+, so use breadth-limited walk (depth ≤ 3)
            int fullW = fullWr.Right - fullWr.Left, fullH = fullWr.Bottom - fullWr.Top;
            uiaStandaloneBoxes = new List<A11yHackOverlayBox>();
            try
            {
                var uiaQueue = new Queue<(FlaUI.Core.AutomationElements.AutomationElement el, int depth)>();
                uiaQueue.Enqueue((uiaRoot, 0));
                const int maxDepth = 5; // deeper for Electron apps (actual UI at depth 4-5)
                const int maxBoxes = 200;
                while (uiaQueue.Count > 0 && uiaStandaloneBoxes.Count < maxBoxes)
                {
                    var (cur, depth) = uiaQueue.Dequeue();
                    try
                    {
                        var elRect = cur.Properties.BoundingRectangle.ValueOrDefault;
                        if (elRect.Width >= 5 && elRect.Height >= 5)
                        {
                            int bx = (int)elRect.X - fullWr.Left, by = (int)elRect.Y - fullWr.Top;
                            int bw = (int)elRect.Width, bh = (int)elRect.Height;
                            if (bx < 0) { bw += bx; bx = 0; } if (by < 0) { bh += by; by = 0; }
                            if (bx + bw > fullW) bw = fullW - bx; if (by + bh > fullH) bh = fullH - by;
                            if (bw >= 5 && bh >= 5)
                            {
                                var name = cur.Properties.Name.ValueOrDefault ?? "";
                                var aid = cur.Properties.AutomationId.ValueOrDefault ?? "";
                                var ct = ""; try { ct = cur.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                                // Include if has name/aid OR has controlType (for unnamed Pane/Group nodes)
                                {
                                    var idPart = !string.IsNullOrWhiteSpace(aid) ? aid
                                               : !string.IsNullOrWhiteSpace(name) ? TrimPreview(name, 20)
                                               : $"d{depth}";
                                    var typePart = !string.IsNullOrWhiteSpace(ct) ? ct : "?";
                                    var label = $"{typePart}_{idPart} {bw}x{bh}";
                                    uiaStandaloneBoxes.Add(new A11yHackOverlayBox(
                                        new System.Windows.Rect(bx, by, bw, bh), label, null, HackBoxRole.Known));
                                }
                            }
                        }
                        // Enqueue children (depth-limited)
                        if (depth < maxDepth)
                        {
                            try
                            {
                                foreach (var child in cur.FindAllChildren())
                                    uiaQueue.Enqueue((child, depth + 1));
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
            if (!_hackHoverQuiet) Console.WriteLine($"[HACK] UIA standalone boxes: {uiaStandaloneBoxes.Count}");

            // Print UIA tree summary: window root → children (depth-limited)
            try
            {
                var rootName = uiaRoot.Properties.Name.ValueOrDefault ?? "";
                var rootAid = uiaRoot.Properties.AutomationId.ValueOrDefault ?? "";
                var rootCt = ""; try { rootCt = uiaRoot.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                var rootClass = uiaRoot.Properties.ClassName.ValueOrDefault ?? "";
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (!_hackHoverQuiet) Console.WriteLine($"[HACK] UIA tree root: {rootCt} \"{rootName}\" aid={rootAid} class={rootClass}");
                Console.ResetColor();
                var rootChildren = uiaRoot.FindAllChildren();
                for (int ci = 0; ci < rootChildren.Length && ci < 30; ci++)
                {
                    var c = rootChildren[ci];
                    try
                    {
                        var cn = c.Properties.Name.ValueOrDefault ?? "";
                        var ca = c.Properties.AutomationId.ValueOrDefault ?? "";
                        var cc = ""; try { cc = c.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var cr = c.Properties.BoundingRectangle.ValueOrDefault;
                        Console.WriteLine($"  |--{cc} \"{TrimPreview(cn, 30)}\" aid={ca} ({(int)cr.Width}×{(int)cr.Height})");
                    }
                    catch { Console.WriteLine($"  |--(error reading child #{ci})"); }
                }
                if (rootChildren.Length > 30) Console.WriteLine($"  `--... +{rootChildren.Length - 30} more");
            }
            catch { }

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
                    if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Region[{ri}] narrowed: {seg.Bounds.Width}x{seg.Bounds.Height} → {narrowed.Width}x{narrowed.Height} (UIA)");
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
            UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
        }
        catch (Exception ex) { if (!_hackHoverQuiet) Console.WriteLine($"[HACK] UIA scan error: {ex.Message}"); }

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
        if (!_hackHoverQuiet) Console.WriteLine($"[HACK] ── DYN-A11Y Tree ({w}횞{h}) ──");
        Console.ResetColor();

        foreach (var rowGroup in rowGroups)
        {
            var items = rowGroup.OrderBy(p => p.col).ToList();
            bool isLastRow = rowGroup.Key == rowGroups.Last().Key;
            string rowPrefix = isLastRow ? "`-- " : "|-- ";
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
                                UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
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
                                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
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
                                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
                                }
                            }
                        }
                    }
                    catch
                    {
                        ocrEmpty++;
                        stageLabels[regionIdx] = "ocr error";
                        UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
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
        if (!_hackHoverQuiet) Console.WriteLine($"[HACK] ── Summary ──");
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
            UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
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
                    if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Asking Gemini Vision ({gapCollector.Count} regions)...");
                    Console.ResetColor();

                    // Ask Gemini with composite image
                    if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Engine: {visionEngine}");
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
                    UpdateHackOverlay(liveOverlay, bmp, regions, uiaAnswers, stageLabels, uiaStandaloneBoxes, ccaOffX, ccaOffY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Vision query complete (exit={exitCode})");
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
                        if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Cached: {cacheName}");
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                if (!_hackHoverQuiet) Console.WriteLine($"[HACK] Vision error: {ex.Message}");
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
}

