// A11yHackCommand.OcrWorker.cs — Background OCR worker for hack analysis
// Processes text/container segments off the main thread so overlay stays responsive.
// Updates stageLabels + overlay incrementally as each segment completes.

using System.Drawing;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Context shared between main thread and OCR/Vision workers.
    /// All mutable state is guarded by the Lock object.
    /// </summary>
    sealed class HackWorkerContext
    {
        public required Bitmap Bmp;
        public required List<ConnectedComponentAnalyzer.Region> Regions;
        public required Dictionary<int, (string name, string type, string aid)> UiaAnswers;
        public required Dictionary<int, string> StageLabels;
        public required IReadOnlyList<A11yHackOverlayBox>? UiaStandaloneBoxes;
        public required A11yHackOverlayHost? LiveOverlay;
        public required int CcaOffX, CcaOffY;
        public required IntPtr Hwnd;
        public required string? ExpDir;
        public OcrGapCollector? GapCollector;
        public int OcrOk, OcrEmpty;
        public readonly object Lock = new();

        public void UpdateOverlay()
        {
            UpdateHackOverlay(LiveOverlay, Bmp, Regions, UiaAnswers, StageLabels, UiaStandaloneBoxes, CcaOffX, CcaOffY);
        }
    }

    /// <summary>
    /// Run OCR on all text/container segments in background.
    /// Returns when all segments are processed. Updates overlay per segment.
    /// </summary>
    static void RunOcrWorker(HackWorkerContext ctx, Func<bool> shouldAbort)
    {
        using var ocr = new SimpleOcrAnalyzer();

        // OcrCorrectionDb
        OcrCorrectionDb? correctionDb = null;
        try
        {
            if (ctx.ExpDir != null)
            {
                NativeMethods.GetWindowThreadProcessId(ctx.Hwnd, out uint cpid);
                var cproc = System.Diagnostics.Process.GetProcessById((int)cpid);
                var csb = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(ctx.Hwnd, csb, csb.Capacity);
                correctionDb = OcrCorrectionDb.ForProcess(cproc.ProcessName, csb.ToString());
            }
        }
        catch { }

        var w = ctx.Bmp.Width;
        var h = ctx.Bmp.Height;

        for (int ri = 0; ri < ctx.Regions.Count; ri++)
        {
            if (shouldAbort()) break;
            var region = ctx.Regions[ri];
            if (region.Type is not (ConnectedComponentAnalyzer.RegionType.DyText
                or ConnectedComponentAnalyzer.RegionType.DyContainer))
                continue;

            // Skip if UIA already answered
            bool hasUia;
            lock (ctx.Lock) hasUia = ctx.UiaAnswers.ContainsKey(ri);
            if (hasUia)
            {
                // Still do OCR verify for UIA-matched segments
                RunOcrVerify(ctx, ocr, correctionDb, ri, region, w, h);
                continue;
            }

            // OCR this segment
            try
            {
                var cropRect = ConnectedComponentAnalyzer.TrimBorders(ctx.Bmp,
                    Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h)));
                if (cropRect.Width <= 0 || cropRect.Height <= 0) continue;

                Bitmap crop;
                lock (ctx.Lock) crop = ctx.Bmp.Clone(cropRect, ctx.Bmp.PixelFormat);

                using (crop)
                {
                    var result = ocr.RecognizeAll(crop).GetAwaiter().GetResult();
                    var ocrText = string.Join(" ", result.Words.Select(x => x.Text)).Trim();

                    // Auto-correct
                    if (correctionDb != null)
                    {
                        var corrected = correctionDb.TryCorrect(crop, ocrText);
                        if (corrected != null) ocrText = corrected;
                    }

                    if (ocrText.Length > 0)
                    {
                        lock (ctx.Lock)
                        {
                            ctx.OcrOk++;
                            ctx.StageLabels[ri] = $"ocr {TrimPreview(ocrText, 24)}";
                        }
                        var dynId = GetDynId(ctx, ri);
                        CacheSegment(ctx.Bmp, region.Bounds, w, h, dynId, ocrText,
                            isContainer: region.Type == ConnectedComponentAnalyzer.RegionType.DyContainer);
                    }
                    else
                    {
                        // Try correction db for empty
                        var corrected2 = correctionDb?.TryCorrect(crop, "");
                        if (corrected2 != null)
                        {
                            lock (ctx.Lock)
                            {
                                ctx.OcrOk++;
                                ctx.StageLabels[ri] = $"fix {TrimPreview(corrected2, 24)}";
                            }
                        }
                        else
                        {
                            lock (ctx.Lock)
                            {
                                ctx.OcrEmpty++;
                                string? cachedDesc = null;
                                ctx.GapCollector?.Add(cropRect, null, null, out cachedDesc);
                                ctx.StageLabels[ri] = !string.IsNullOrWhiteSpace(cachedDesc)
                                    ? $"cache 100% {TrimPreview(cachedDesc, 18)}"
                                    : "vision pending";
                            }
                        }
                    }
                }

                lock (ctx.Lock) ctx.UpdateOverlay();
            }
            catch
            {
                lock (ctx.Lock)
                {
                    ctx.OcrEmpty++;
                    ctx.StageLabels[ri] = "ocr error";
                    ctx.UpdateOverlay();
                }
            }
        }
    }

    static void RunOcrVerify(HackWorkerContext ctx, SimpleOcrAnalyzer ocr,
        OcrCorrectionDb? correctionDb, int ri,
        ConnectedComponentAnalyzer.Region region, int w, int h)
    {
        string uiaLabel;
        lock (ctx.Lock)
        {
            if (!ctx.UiaAnswers.TryGetValue(ri, out var uia)) return;
            uiaLabel = !string.IsNullOrWhiteSpace(uia.name) ? uia.name : uia.aid;
            ctx.OcrOk++;
        }

        if (region.Type is not (ConnectedComponentAnalyzer.RegionType.DyText
            or ConnectedComponentAnalyzer.RegionType.DyContainer))
            return;

        try
        {
            var cropRect = ConnectedComponentAnalyzer.TrimBorders(ctx.Bmp,
                Rectangle.Intersect(region.Bounds, new Rectangle(0, 0, w, h)));
            if (cropRect.Width <= 0 || cropRect.Height <= 0) return;

            Bitmap crop;
            lock (ctx.Lock) crop = ctx.Bmp.Clone(cropRect, ctx.Bmp.PixelFormat);
            using (crop)
            {
                var verify = ocr.RecognizeAll(crop).GetAwaiter().GetResult();
                var verifyText = string.Join(" ", verify.Words.Select(x => x.Text)).Trim();
                if (!string.IsNullOrWhiteSpace(verifyText))
                {
                    var sim = CalculateHackTextSimilarity(uiaLabel, verifyText);
                    lock (ctx.Lock)
                    {
                        ctx.StageLabels[ri] = sim >= 0.95 ? $"ocr={sim:P0}" : $"ocr={sim:P0} mismatch";
                    }
                    if (sim < 0.95 && correctionDb != null)
                        correctionDb.Learn(crop, verifyText, uiaLabel, "uia");
                }
            }
        }
        catch { }
    }

    static string GetDynId(HackWorkerContext ctx, int ri)
    {
        var positions = DynamicA11yAnalyzer.AssignGridPositions(
            ctx.Regions, r => r.Bounds, rowGap: 5);
        var pos = positions.FirstOrDefault(p => ReferenceEquals(p.region, ctx.Regions[ri]));
        return DynamicA11yAnalyzer.GenerateDynId(pos.row, pos.col,
            ctx.Regions[ri].Bounds.Width, ctx.Regions[ri].Bounds.Height);
    }
}
