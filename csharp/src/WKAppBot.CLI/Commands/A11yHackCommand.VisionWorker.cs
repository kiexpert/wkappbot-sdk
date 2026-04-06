// A11yHackCommand.VisionWorker.cs — Background Vision API worker for hack analysis
// Runs Gemini/GPT/Claude vision on OCR-failed segments (gaps).
// Called after OCR worker completes, only if gaps remain.

using WKAppBot.Vision;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Run Vision API query on OCR-failed segments in background.
    /// Builds triple composite image, sends to Gemini, updates overlay.
    /// </summary>
    static void RunVisionWorker(HackWorkerContext ctx, string visionEngine, Func<bool> shouldAbort)
    {
        OcrGapCollector? gapCollector;
        lock (ctx.Lock) gapCollector = ctx.GapCollector;
        if (gapCollector == null || !gapCollector.HasGaps) return;
        if (shouldAbort()) return;

        int gapCount = gapCollector.Count;

        Console.Error.WriteLine($"[HACK:VISION] Querying {visionEngine} ({gapCount} blind regions)...");

        // Mark pending in overlay
        lock (ctx.Lock)
        {
            for (int ri = 0; ri < ctx.Regions.Count; ri++)
            {
                if (ctx.StageLabels.TryGetValue(ri, out var cur) && cur == "vision pending")
                    continue; // already marked
                if (!ctx.StageLabels.ContainsKey(ri)
                    && ctx.Regions[ri].Type is ConnectedComponentAnalyzer.RegionType.DyText
                        or ConnectedComponentAnalyzer.RegionType.DyContainer)
                    ctx.StageLabels[ri] = "vision pending";
            }
            ctx.UpdateOverlay();
        }

        try
        {
            var (images, prompt) = gapCollector.BuildTripleComposite();
            if (images.Count == 0) return;

            var gapDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WKAppBot", "gap_screenshots");
            Directory.CreateDirectory(gapDir);
            var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var compositePath = Path.Combine(gapDir, $"hack_{ts}_A.png");
            File.WriteAllBytes(compositePath, images[0]);
            File.WriteAllText(Path.Combine(gapDir, $"hack_{ts}.prompt.txt"), prompt);

            int exitCode = visionEngine switch
            {
                "gpt" => AskChatGpt(prompt, slackReport: false, timeoutSec: 60,
                    attachFiles: new List<string> { compositePath }, noWait: false),
                "claude" => AskClaude(prompt, slackReport: false, timeoutSec: 60, noWait: false),
                _ => AskGemini(prompt, slackReport: false, timeoutSec: 60,
                    attachFiles: new List<string> { compositePath }, noWait: false),
            };

            lock (ctx.Lock)
            {
                for (int ri = 0; ri < ctx.Regions.Count; ri++)
                {
                    if (ctx.StageLabels.TryGetValue(ri, out var cur) && cur == "vision pending")
                        ctx.StageLabels[ri] = $"vision {visionEngine}";
                }
                ctx.UpdateOverlay();
            }


            Console.Error.WriteLine($"[HACK:VISION] Complete (exit={exitCode})");
    
            // Cache composite
            try
            {
                var cacheDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "WKAppBot", "gap_cache");
                Directory.CreateDirectory(cacheDir);
                using var hashBmp = new System.Drawing.Bitmap(compositePath);
                var hash = OcrGapCollector.ComputePixelHash(hashBmp);
                var cachePath = Path.Combine(cacheDir, $"hack'{hash}={ts}.png");
                if (!File.Exists(cachePath)) File.Copy(compositePath, cachePath);
            }
            catch { }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[HACK:VISION] Error: {ex.Message}");
        }
    }
}
