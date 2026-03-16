using WKAppBot.Vision;

namespace WKAppBot.CLI;

// Deep-scan OCR for PDF pages: pixel-based line detection → segment slicing → per-segment OCR.
// Complements full-page OCR by catching math formula variables, subscripts, isolated symbols
// that PdfPig can't decode and full-page OCR misses due to scale/font issues.
//
// Algorithm:
//   1. Row scan  — find rows with dark pixels → group into line bands
//   2. Col scan  — within each band, find column gaps → split into segments
//   3. Strip OCR — OCR full band strip first (fast path, catches normal text)
//   4. Seg OCR   — if strip got < 5 chars/100px, OCR segments individually (slow path, catches formulas)
internal partial class Program
{
    // Background luminance threshold: pixels darker than this are "content".
    const int PdfBgLum = 220;

    /// <summary>
    /// Deep-scan OCR: detect pixel line bands → segment slicing → OCR.
    /// Returns all text found, joining strip and segment results.
    /// Called as a second OCR pass after full-page OCR.
    /// </summary>
    static async Task<string> DeepScanOcrAsync(System.Drawing.Bitmap bmp, SimpleOcrAnalyzer ocr)
    {
        int w = bmp.Width, h = bmp.Height;

        // Copy bitmap pixels to managed array for fast access (avoid GetPixel overhead)
        var bmpData = bmp.LockBits(
            new System.Drawing.Rectangle(0, 0, w, h),
            System.Drawing.Imaging.ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        var pixels = new byte[bmpData.Stride * h];
        System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, pixels.Length);
        bmp.UnlockBits(bmpData);
        int stride = bmpData.Stride;

        // Step 1: detect active rows (rows with ≥0.3% dark pixels)
        var activeRows = new bool[h];
        for (int y = 0; y < h; y++)
        {
            int dark = 0, off = y * stride;
            for (int x = 0; x < w; x++)
            {
                int lum = (pixels[off + x * 4 + 2] + pixels[off + x * 4 + 1] + pixels[off + x * 4]) / 3;
                if (lum < PdfBgLum && ++dark > w * 0.003) break;
            }
            activeRows[y] = dark > w * 0.003;
        }

        // Step 2: group active rows into line bands (gap > 6px = new band)
        var bands = new List<(int top, int bottom)>();
        int bandStart = -1, lastActiveRow = -1;
        for (int y = 0; y <= h; y++)
        {
            bool act = y < h && activeRows[y];
            if (act) { if (bandStart < 0) bandStart = y; lastActiveRow = y; }
            else if (bandStart >= 0 && (y - lastActiveRow > 6 || y == h))
            {
                bands.Add((Math.Max(0, bandStart - 2), Math.Min(h - 1, lastActiveRow + 2)));
                bandStart = -1;
            }
        }

        // Step 3 & 4: OCR each band (cap total segment calls at 40/page to stay fast)
        var results = new List<string>();
        int totalSegs = 0;
        foreach (var (top, bottom) in bands)
        {
            int bh = bottom - top + 1;
            if (bh < 3) continue;

            // Strip OCR: full-width band, upscale to min 80px height for accuracy
            string stripText = "";
            try
            {
                var stripRect = new System.Drawing.Rectangle(0, top, w, bh);
                using var strip = bmp.Clone(stripRect, bmp.PixelFormat);
                var ocrR = await ocr.RecognizeAll(strip);
                stripText = ocrR.FullText.Trim();
                if (!string.IsNullOrWhiteSpace(stripText))
                    results.Add(stripText);
            }
            catch { }

            // Segment OCR slow path: if strip got < 5 chars per 100px width → probably formula/symbols
            double stripDensity = (double)stripText.Replace(" ", "").Length / (w / 100.0);
            if (stripDensity >= 5 || totalSegs >= 40) continue;

            // Detect column gaps within this band to find segments
            var activeCols = new bool[w];
            for (int x = 0; x < w; x++)
                for (int y = top; y <= bottom; y++)
                {
                    int lum = (pixels[y * stride + x * 4 + 2] + pixels[y * stride + x * 4 + 1] + pixels[y * stride + x * 4]) / 3;
                    if (lum < PdfBgLum) { activeCols[x] = true; break; }
                }

            // Group active columns into segments (gap >= 5px = new segment)
            var segs = new List<(int left, int right)>();
            int segStart = -1, lastActiveCol = -1;
            for (int x = 0; x <= w; x++)
            {
                bool act = x < w && activeCols[x];
                if (act) { if (segStart < 0) segStart = x; lastActiveCol = x; }
                else if (segStart >= 0 && (x - lastActiveCol > 5 || x == w))
                {
                    if (lastActiveCol - segStart >= 5)
                        segs.Add((Math.Max(0, segStart - 1), Math.Min(w - 1, lastActiveCol + 1)));
                    segStart = -1;
                }
            }

            // OCR each segment (skip slivers < 6px wide, cap at 10 segs/band, 40 total/page)
            int segCount = 0;
            foreach (var (left, right) in segs)
            {
                int sw = right - left + 1;
                if (sw < 6 || bh < 3) continue;
                if (++segCount > 10 || ++totalSegs > 40) break;
                try
                {
                    var segRect = new System.Drawing.Rectangle(left, top, sw, bh);
                    using var seg = bmp.Clone(segRect, bmp.PixelFormat);
                    var ocrR = await ocr.RecognizeAll(seg);
                    var text = ocrR.FullText.Trim();
                    if (text.Length >= 1)
                        results.Add(text);
                }
                catch { }
            }
        }

        return string.Join(" ", results.Where(t => !string.IsNullOrWhiteSpace(t)));
    }
}
