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
        // Ensure UTF-8 output (may be lost in Task.Run / pipe contexts)
        try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }

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

        var hackLog = Console.Error;
        PulseStep.Init("a11y-hack");

        // Parse grap#scope
        var grapFull = string.Join(" ", args.TakeWhile(a => !a.StartsWith("--")));
        var hashIdx = grapFull.IndexOf('#');
        var grap = hashIdx >= 0 ? grapFull[..hashIdx] : grapFull;
        var uiaScope = hashIdx >= 0 ? grapFull[(hashIdx + 1)..] : null;
        bool hasExplicitScope = hashIdx >= 0 && !string.IsNullOrWhiteSpace(uiaScope);
        bool focusDrillRequested = grapFull.TrimEnd().EndsWith('/')
            || (hashIdx >= 0 && string.IsNullOrWhiteSpace(uiaScope));

        var targets = WindowFinder.FindWindows(grap);
        if (!targets.Any())
        {
            hackLog.WriteLine($"[HACK] No window found: \"{grap}\"");
            return 1;
        }
        var win = targets.First();
        var hwnd = win.Handle;
        hackLog.WriteLine($"[HACK] Target: 0x{hwnd.ToInt64():X} \"{win.Title}\"");
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
            hackLog.WriteLine($"[HACK] --ltrb override: rect=({ar.Left},{ar.Top} {ar.Width}x{ar.Height})");
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
                    hackLog.WriteLine($"[HACK] Scoped to: \"{uiaScope}\" rect=({r.X},{r.Y} {r.Width}x{r.Height})");
                }
                else hackLog.WriteLine($"[HACK] Scope \"{uiaScope}\" not found — using full window");
            }
            catch { hackLog.WriteLine($"[HACK] Scope error — using full window"); }
        }
        int w = wr.Right - wr.Left, h = wr.Bottom - wr.Top;
        if (w <= 0 || h <= 0)
        {
            hackLog.WriteLine("[HACK] Window has zero size");
            return 1;
        }
        var analysisTargetRect = TryResolveAnalysisTargetRect(wr);
        // Overlay covers full root window (UIA tree needs full coverage)
        int fullWw = fullWr.Right - fullWr.Left, fullWh = fullWr.Bottom - fullWr.Top;
        // CCA offset: narrowed area position relative to full window
        int ccaOffX = wr.Left - fullWr.Left, ccaOffY = wr.Top - fullWr.Top;
        _hackOverlayHwnd = hwnd;
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
                hackLog.WriteLine("[HACK] User input detected — aborting");
                if (!_hackHoverAnalyzing) liveOverlay?.Hide();
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
                hackLog.WriteLine("[HACK] Window moved/resized — aborting");
                if (!_hackHoverAnalyzing) liveOverlay?.Hide();
                return true;
            }
            // 2. Target node pixels changed?
            if (baselinePixelHash != 0 && !targetScreenRect.IsEmpty)
            {
                var cur = SampleCenterPixels(targetScreenRect);
                if (cur != baselinePixelHash)
                {
                    hackAborted = true;
                    hackLog.WriteLine("[HACK] Content changed — aborting for re-analysis");
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
            hackLog.WriteLine($"[HACK] Capture failed: {ex.Message}");
            return 1;
        }
        hackLog.WriteLine($"[HACK] Captured: {w}x{h}");
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

        hackLog.WriteLine($"[HACK] CCA: {regions.Count} regions ??Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount} Noise={noiseCount}");
        SaveHackOverlayPreview(bmp, regions, "cca", wr.Left, wr.Top);
        var stageLabels = new Dictionary<int, string>();
        UpdateHackOverlay(liveOverlay, bmp, regions, null, stageLabels, null, ccaOffX, ccaOffY);

        // Table detection
        var allRegions = cca.Analyze(bmp); // non-merged for table detection
        var table = cca.DetectTable(allRegions, w, h);
        if (table != null)
            hackLog.WriteLine($"[HACK] Table detected: {table.Rows} rows x {table.Cols} cols");
        PulseStep.Mark("table-detect");

        if (HackTargetChanged()) { bmp.Dispose(); return 0; }
        // UIA answer key: scan UIA tree, build rect→info map for cross-reference
        var uiaAnswers = new Dictionary<int, (string name, string type, string aid)>();
        var uiaSibIdx = new Dictionary<int, int>(); // region → sibling index (1-based)
        var uiaBounds = new Dictionary<int, Rectangle>(); // UIA element screen rect (most specific)
        var _uiaBestArea = new Dictionary<int, int>();
        List<A11yHackOverlayBox>? uiaStandaloneBoxes = null;
        try
        {
            PulseStep.Mark("uia-scan");
            using var automation = new FlaUI.UIA3.UIA3Automation();
            var uiaRoot = automation.FromHandle(hwnd);
            // Depth-limited BFS instead of FindAllDescendants (Electron can take 60s+)
            var uiaElements = new List<(FlaUI.Core.AutomationElements.AutomationElement el, int sibIdx)>();
            {
                var q = new Queue<(FlaUI.Core.AutomationElements.AutomationElement el, int d, int sibIdx)>();
                q.Enqueue((uiaRoot, 0, 0));
                const int scanDepth = 4;
                while (q.Count > 0 && uiaElements.Count < 500)
                {
                    var (n, d, sib) = q.Dequeue();
                    uiaElements.Add((n, sib));
                    if (d < scanDepth)
                    {
                        try
                        {
                            var children = n.FindAllChildren();
                            for (int ci = 0; ci < children.Length; ci++)
                                q.Enqueue((children[ci], d + 1, ci + 1));
                        }
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
                foreach (var (el, elSibIdx) in uiaElements)
                {
                    try
                    {
                        var elRect = el.Properties.BoundingRectangle.ValueOrDefault;
                        var elR = new Rectangle(elRect.X, elRect.Y, elRect.Width, elRect.Height);
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
                                if (!existing || elArea < _uiaBestArea.GetValueOrDefault(ri, int.MaxValue))
                                {
                                    uiaAnswers[ri] = (name, ct, aid);
                                    uiaSibIdx[ri] = elSibIdx;
                                    uiaBounds[ri] = elR;
                                    _uiaBestArea[ri] = elArea;
                                    if (!existing) uiaMatched++;
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            hackLog.WriteLine($"[HACK] UIA answer key: {uiaMatched}/{regions.Count} segments matched");

            // Collect standalone UIA boxes from FULL root window (depth-limited for speed)
            // FindAllDescendants on Electron can take 60s+, so use breadth-limited walk (depth ≤ 3)
            int fullW = fullWr.Right - fullWr.Left, fullH = fullWr.Bottom - fullWr.Top;
            uiaStandaloneBoxes = new List<A11yHackOverlayBox>();
            try
            {
                var uiaQueue = new Queue<(FlaUI.Core.AutomationElements.AutomationElement el, int depth, int sibIdx)>();
                uiaQueue.Enqueue((uiaRoot, 0, 0));
                const int maxDepth = 5;
                const int maxBoxes = 200;
                while (uiaQueue.Count > 0 && uiaStandaloneBoxes.Count < maxBoxes)
                {
                    var (cur, depth, sibIdx) = uiaQueue.Dequeue();
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
                                var aid = cur.Properties.AutomationId.ValueOrDefault ?? "";
                                var ct = ""; try { ct = cur.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                                var typePart = !string.IsNullOrWhiteSpace(ct) ? ct : "?";
                                var label = $"{WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(typePart, aid, sibIdx + 1)} {bw}x{bh}";
                                uiaStandaloneBoxes.Add(new A11yHackOverlayBox(
                                    new System.Windows.Rect(bx, by, bw, bh), label, null, HackBoxRole.Known));
                            }
                        }
                        if (depth < maxDepth)
                        {
                            try
                            {
                                var children = cur.FindAllChildren();
                                for (int ci = 0; ci < children.Length; ci++)
                                    uiaQueue.Enqueue((children[ci], depth + 1, ci));
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
            hackLog.WriteLine($"[HACK] UIA standalone boxes: {uiaStandaloneBoxes.Count}");

            // Print UIA tree summary: window root → children (depth-limited)
            try
            {
                var rootName = uiaRoot.Properties.Name.ValueOrDefault ?? "";
                var rootAid = uiaRoot.Properties.AutomationId.ValueOrDefault ?? "";
                var rootCt = ""; try { rootCt = uiaRoot.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                var rootClass = uiaRoot.Properties.ClassName.ValueOrDefault ?? "";
                hackLog.WriteLine($"[HACK] UIA tree root: {rootCt} \"{rootName}\" aid={rootAid} class={rootClass}");
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
                        hackLog.WriteLine($"  |--{cc} \"{TrimPreview(cn, 30)}\" aid={ca} ({(int)cr.Width}x{(int)cr.Height})");
                    }
                    catch { hackLog.WriteLine($"  |--(error reading child #{ci})"); }
                }
                if (rootChildren.Length > 30) hackLog.WriteLine($"  `--... +{rootChildren.Length - 30} more");
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
                    hackLog.WriteLine($"[HACK] Region[{ri}] narrowed: {seg.Bounds.Width}x{seg.Bounds.Height} → {narrowed.Width}x{narrowed.Height} (UIA)");
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
        catch (Exception ex) { hackLog.WriteLine($"[HACK] UIA scan error: {ex.Message}"); }

        if (HackTargetChanged()) { bmp.Dispose(); return 0; }

        // ── OCR + Vision: background workers (overlay stays responsive) ──
        var gapCollector = new OcrGapCollector();
        var workerCtx = new HackWorkerContext
        {
            Bmp = bmp, Regions = regions, UiaAnswers = uiaAnswers,
            StageLabels = stageLabels, UiaStandaloneBoxes = uiaStandaloneBoxes,
            LiveOverlay = liveOverlay, CcaOffX = ccaOffX, CcaOffY = ccaOffY,
            Hwnd = hwnd, ExpDir = _hackExpDir, GapCollector = gapCollector,
        };

        var ocrThread = new Thread(() => RunOcrWorker(workerCtx, () => HackShouldAbort()))
        { IsBackground = true, Name = "HackOcr" };
        ocrThread.Start();
        ocrThread.Join();
        PulseStep.Mark("ocr-done");

        if (!HackShouldAbort() && gapCollector.HasGaps)
        {
            var visionThread = new Thread(() => RunVisionWorker(workerCtx, visionEngine, () => HackShouldAbort()))
            { IsBackground = true, Name = "HackVision" };
            visionThread.Start();
            visionThread.Join();
            PulseStep.Mark("vision-done");
        }
        gapCollector.Dispose();

        // ── Summary + MD table ──
        hackLog.WriteLine($"[HACK] ── Summary ──");
        hackLog.WriteLine($"  Segments: {regions.Count} (Text={textCount} Icon={iconCount} Sep={sepCount} Container={contCount})");
        hackLog.WriteLine($"  OCR: {workerCtx.OcrOk} ok, {workerCtx.OcrEmpty} failed");
        if (table != null) hackLog.WriteLine($"  Table: {table.Rows}x{table.Cols}");

        var positions = DynamicA11yAnalyzer.AssignGridPositions(
            regions, r => r.Bounds, rowGap: 5);

        // (OCR/Vision moved to background workers — see OcrWorker.cs / VisionWorker.cs)

        bool hasRows = regions.Any(r => r.Type != ConnectedComponentAnalyzer.RegionType.DyNoise);
        if (hasRows)
        {
        Console.WriteLine("| # | Tag | WxH | OCR/Label |");
        Console.WriteLine("|---|-----|-----|-----------|");
        }
        for (int si = 0; si < regions.Count; si++)
        {
            var sr = regions[si];
            if (sr.Type == ConnectedComponentAnalyzer.RegionType.DyNoise) continue;
            var pos = positions.FirstOrDefault(p => ReferenceEquals(p.region, sr));
            var dynId = DynamicA11yAnalyzer.GenerateDynId(pos.row, pos.col, sr.Bounds.Width, sr.Bounds.Height);
            string tag;
            if (uiaAnswers != null && uiaAnswers.TryGetValue(si, out var su))
                tag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(su.type ?? "?", su.aid, uiaSibIdx.GetValueOrDefault(si));
            else
                tag = $"Dy{sr.Type}_{dynId}";
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
            Console.WriteLine($"| {si} | {tag} | {size} | {TrimPreview(labelCol, 30)} |");
        }
        Console.WriteLine();

        bmp.Dispose();
        if (!_hackHoverAnalyzing) liveOverlay?.Hide();
        PulseStep.Done();
        return 0;
    }
}
