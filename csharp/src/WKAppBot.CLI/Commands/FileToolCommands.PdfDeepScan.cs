using WKAppBot.Vision;

namespace WKAppBot.CLI;

// Deep-scan OCR fallback for PDF pages.
//
// Step 1 (caller): full-page OCR → OcrFullResult with word coordinates  (1 call, fast)
// Step 2 (here):   pixel line detection → segment list
//                  segments NOT covered by step-1 OCR words → crop → additional OCR  (fallback)
//
// "Not covered" = segment rect has zero overlapping OcrWords, or OCR word density < threshold.
// This catches math formula variables, subscripts, table cells that Windows OCR misses at full scale.
internal partial class Program
{
    const int PdfBgLum = 220; // pixels darker than this are "content"

    /// <summary>
    /// Additional OCR pass for regions the full-page OCR missed.
    /// Call AFTER RecognizeAll() on the full page — pass in the OcrFullResult for coverage check.
    /// Returns supplemental text found in uncovered regions.
    /// </summary>
    static async Task<string> DeepScanOcrAsync(
        System.Drawing.Bitmap bmp,
        SimpleOcrAnalyzer ocr,
        OcrFullResult fullOcr,
        Action<string>? onFound = null)
    {
        int w = bmp.Width, h = bmp.Height;

        // ── Copy pixels for fast row/col scanning ──────────────────────────────
        var bmpData = bmp.LockBits(
            new System.Drawing.Rectangle(0, 0, w, h),
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        var pixels = new byte[bmpData.Stride * h];
        System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, pixels.Length);
        bmp.UnlockBits(bmpData);
        int stride = bmpData.Stride;

        // ── Step 1: detect pixel-active rows → line bands ─────────────────────
        var activeRows = new bool[h];
        for (int y = 0; y < h; y++)
        {
            int dark = 0, off = y * stride;
            for (int x = 0; x < w; x++)
            {
                int lum = (pixels[off + x*4+2] + pixels[off + x*4+1] + pixels[off + x*4]) / 3;
                if (lum < PdfBgLum && ++dark > w * 0.003) break;
            }
            activeRows[y] = dark > w * 0.003;
        }

        var bands = new List<(int top, int bottom)>();
        int bandStart = -1, lastActive = -1;
        for (int y = 0; y <= h; y++)
        {
            bool act = y < h && activeRows[y];
            if (act) { if (bandStart < 0) bandStart = y; lastActive = y; }
            else if (bandStart >= 0 && (y - lastActive > 6 || y == h))
            {
                bands.Add((Math.Max(0, bandStart - 2), Math.Min(h - 1, lastActive + 2)));
                bandStart = -1;
            }
        }

        // ── Step 2: build OCR coverage map (word bounding boxes) ─────────────
        // OcrWord coords are in bitmap pixels (already scaled back by RecognizeAll)
        var covered = fullOcr.Words
            .Select(ww => new System.Drawing.Rectangle(ww.X, ww.Y, Math.Max(1, ww.Width), Math.Max(1, ww.Height)))
            .ToList();

        bool IsCovered(System.Drawing.Rectangle seg)
        {
            foreach (var box in covered)
                if (box.IntersectsWith(seg)) return true;
            return false;
        }

        // ── Step 3: per-band column scan → segments → fallback OCR ───────────
        var results = new List<string>();
        int totalFallback = 0;

        foreach (var (top, bottom) in bands)
        {
            int bh = bottom - top + 1;
            if (bh < 3) continue;

            // Column scan → active cols + pixel count (used for coverage judgment below)
            var activeCols = new bool[w];
            int darkPixels = 0;
            for (int x = 0; x < w; x++)
                for (int y = top; y <= bottom; y++)
                {
                    int lum = (pixels[y*stride + x*4+2] + pixels[y*stride + x*4+1] + pixels[y*stride + x*4]) / 3;
                    if (lum < PdfBgLum) { activeCols[x] = true; darkPixels++; break; }
                }

            // "미진" check: OCR chars vs pixel activity ratio
            // If OCR got plenty of text relative to pixel density → well-covered, skip
            int bandOcrChars = fullOcr.Words
                .Where(ww => ww.Y >= top - 4 && ww.Y <= bottom + 4)
                .Sum(ww => ww.Text.Length);
            double pixelDensity  = (double)darkPixels / w;          // active cols / total cols
            double charPerPixel  = pixelDensity > 0 ? bandOcrChars / (pixelDensity * w) : 0;
            if (charPerPixel >= 0.4) continue; // OCR coverage sufficient → skip this band

            int segStart = -1, lastActiveCol = -1;
            var segs = new List<System.Drawing.Rectangle>();
            for (int x = 0; x <= w; x++)
            {
                bool act = x < w && activeCols[x];
                if (act) { if (segStart < 0) segStart = x; lastActiveCol = x; }
                else if (segStart >= 0 && (x - lastActiveCol > 5 || x == w))
                {
                    int sw = lastActiveCol - segStart + 1;
                    if (sw >= 6)
                        segs.Add(new System.Drawing.Rectangle(
                            Math.Max(0, segStart - 1), top,
                            Math.Min(sw + 2, w - segStart + 1), bh));
                    segStart = -1;
                }
            }

            // Fallback OCR only for uncovered segments
            foreach (var seg in segs)
            {
                if (totalFallback >= 60) goto done;
                if (IsCovered(seg)) continue; // full-page OCR already got this
                try
                {
                    using var crop = bmp.Clone(seg, bmp.PixelFormat);
                    var r = await ocr.RecognizeAll(crop);
                    var t = r.FullText.Trim();
                    if (t.Length >= 1) { onFound?.Invoke(t); results.Add(t); totalFallback++; }
                }
                catch { }
            }
        }
        done:
        return string.Join(" ", results.Where(t => !string.IsNullOrWhiteSpace(t)));
    }
}
