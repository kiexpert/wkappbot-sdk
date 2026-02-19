using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WKAppBot.Vision;

/// <summary>
/// HTS-agnostic candlestick chart pixel analyzer.
/// Extracts OHLC + Volume from any chart screenshot — no API required.
///
/// ── Design Intent (설계의도) ──────────────────────────────────────
///
/// PURPOSE:
///   ANY trading platform's candlestick chart screenshot → OHLC + Volume data.
///   Works offline, no OpenAPI needed. Only requires a chart image.
///
/// 7-STEP PIPELINE:
///   [1] Load       — Bitmap (file or window capture)
///   [2] Detect     — Chart region auto-detection (panel border, Y-axis OCR position)
///   [3] Y-Axis     — Right-edge OCR → (pixelY, price) calibration points
///   [4] X-Axis     — Bottom-edge OCR → (pixelX, datetime) labels
///   [5] Candles    — 3-strategy detection (A: body-first, B: column-scan, C: HTS-style)
///                    Winner = strategy with most detected candles (with sanity checks)
///   [6] Volume     — Detect volume sub-panel by horizontal gap → bar height → volume value
///   [7] Output     — JSON (per-candle OHLC + metadata)
///
/// ── 3 Detection Strategies (캔들 탐지 3전략) ─────────────────────
///
/// Strategy A: BODY-FIRST (default for normal-width candles)
///   Find rectangular bodies ≥3px wide → trace wicks from body center.
///   MA lines are 1-2px thin → filtered. Text is scattered → no solid runs.
///   Retries with width≥2, then width≥1 if too few candles found.
///   Auto-triggers column-scan (B) if gap analysis shows missing candles.
///
/// Strategy B: COLUMN-SCAN (fallback for ultra-thin candles)
///   Per-column red/blue pixel counting → grouping → body estimation.
///   Triggered when body-first finds large gaps or density mismatch.
///   Good for ~2-3px candles, but still misses HTS-specific elements.
///
/// Strategy C: HTS-STYLE (specialized for 영웅문/HTS GDI rendering)
///   Recognizes 3 distinct HTS candle elements:
///     Fill   — cream (246,234,214) / pale blue (214,226,239)
///     Border — brown (134,104,53) / teal (57,154,156)
///     Wick   — vivid red (255,0,0) / vivid blue (17,91,203)
///   These colors DON'T pass normal red/blue checks → A/B miss them entirely.
///   HTS renders with GDI raster — NO anti-aliasing, only ~21 unique colors.
///
///   Ultra-dense mode (600-day charts, ~1.04px per candle):
///     "캔들은 중간에 사라지지 않고 각각이 공평한 거리를 유지하며 반드시 있다"
///     → Every column is a candle. No gaps exist.
///     → Candle width is fractional (float-to-int boundary creates 1px vs 2px candles).
///     → When expectedCandleCount is known from OCR, use deltaX-based placement:
///       deltaX = pixelSpan / candleCount → each candle owns [floor(i*dX), floor((i+1)*dX))
///       This gives exactly N candles matching actual trading days.
///
///   Indicator line filtering (MA line removal):
///     MA lines are drawn in wick colors (red/blue) → per-pixel indicator mask.
///     Key insight: MA lines form horizontal runs ≥8px at any Y level,
///     while real candle wicks in ultra-dense are 1-2px runs.
///     (Distribution: len 1-7 = real candle wick, len 8-9 = MA at candle pitch)
///
/// ── 눈의 진화 (Eye Evolution) History ────────────────────────────
///
/// This analyzer evolved through iterative discovery of HTS rendering behavior:
///
/// v1-v3: Body-first only → 35 candles on normal chart (form 0606)
/// v4:    Added column-scan fallback → helped thin-candle charts
/// v5:    Added HTS-style detection → recognized fill/border/wick elements
/// v6:    Y-frequency indicator filter + ultra-dense mode → 524 candles (form 0600)
/// v7:    Horizontal-run indicator mask (replaced Y-frequency) → 554 candles
///        Key fix: Y-frequency removed real wicks at popular price Y levels
/// v8:    Threshold tuning (3→8) → 546 candles
///        Key fix: threshold=3 masked clustered candle wicks at same price
/// v9:    Post-filter skip in ultra-dense + htsScanBottom extended → 626 candles (0 gaps!)
///        Key fix: FilterTextOverlayOutliers was deleting valid ultra-dense candles
///        Key fix: candleScanBottom=volumeTop-25 was cutting off real wicks at Y=360-384
/// v10:   Border-as-body estimation → O/C approximation in ultra-dense
/// v11:   DeltaX-based candle placement → exactly N candles from OCR count
///        "전체봉수는 OCR로 알고 있으니 공평한 delta로 계산하면 된다"
///
/// ── Other Design Decisions ───────────────────────────────────────
///
/// Y-AXIS OCR ISSUES:
///   HTS price labels are small → OCR often truncates: "35,000" → "35," or "25."
///   Solution: Detect trailing comma/period → multiply by 1000
///   Scale consistency: If median value > 1000 and some values are < median/100, scale up ×1000
///
/// LEFT BOUNDARY DETECTION:
///   HTS indicator panels (추세지표, 변동성지표 etc.) are to the left of the chart.
///   Detect the dark vertical divider line (panel | chart border) to find the chart start.
///
/// COLOR CONVENTION:
///   Auto-detect Korean (Red=Up, Blue=Down) vs US (Green=Up, Red=Down)
///   by checking presence of green candles.
///
/// Phase B (DONE): Tooltip-based Y-axis recalibration — see TooltipCalibrator.cs
///   Hover mouse on extreme candles → capture tooltips_class32 → OCR exact OHLC
///   → rebuild Y-axis with server-accurate prices (replaces OCR-only calibration)
///   → recalibrate Y-axis for much better accuracy (WM_MOUSEMOVE first, SendInput fallback)
/// ──────────────────────────────────────────────────────────────────
/// </summary>
public sealed class ChartAnalyzer
{
    private readonly SimpleOcrAnalyzer _ocr;

    public ChartAnalyzer(SimpleOcrAnalyzer? ocr = null)
    {
        // English primary for numbers, Korean fallback for labels
        _ocr = ocr ?? new SimpleOcrAnalyzer("en-US", "ko");
    }

    /// <summary>
    /// Main analysis pipeline — works on any chart screenshot.
    ///
    /// ── Pipeline Order (설계의도) ──
    /// 1. Y-axis OCR FIRST (determines chart right boundary + price calibration)
    /// 2. Chart region detection (uses Y-axis position)
    /// 3. Refine chart top from Y-axis topmost point
    /// 4. X-axis OCR for datetime labels
    /// 5. Volume panel detection BEFORE candle scan (to exclude volume bars)
    /// 6. Filter Y-axis points inside volume area
    /// 7. Candle detection: Strategy A (body-first) → B (column-scan) → C (HTS-style)
    ///    - For Strategies A/B: scan price area only (top → volumeTop − 25px)
    ///    - For Strategy C (HTS): scan up to volumeTop (indicator mask handles noise)
    ///    - Winner = strategy with most candles (with sanity checks)
    /// 8. Convert pixel → price using calibration (with clamp to valid range)
    ///
    /// Key design choice: Y-axis OCR runs before everything else because its position
    /// determines the chart's right boundary, which is needed for all other steps.
    /// Volume panel detection runs before candle scan to prevent volume bars
    /// from being misdetected as candle bodies.
    ///
    /// expectedCandleCount: If known from chart UI OCR (e.g., "600/600"), enables
    /// deltaX-based equal-spacing candle placement in Strategy C ultra-dense mode.
    /// When 0, falls back to "every column with HTS elements = 1 candle".
    /// ──
    /// </summary>
    public async Task<ChartAnalysisResult> Analyze(
        Bitmap screenshot, int expectedCandleCount = 0, CancellationToken ct = default)
    {
        var result = new ChartAnalysisResult
        {
            ImageWidth = screenshot.Width,
            ImageHeight = screenshot.Height
        };

        // Step 3 first: Y-axis OCR → find price labels and chart right boundary
        // We scan the rightmost 15% of the image for price numbers
        var yAxisScan = await ScanYAxisRegion(screenshot);
        result.YAxisPoints = yAxisScan.points;
        int yAxisLeft = yAxisScan.axisLeftX; // where Y-axis numbers start

        using var grid = new PixelGrid(screenshot);

        // Step 2: Detect chart region (informed by Y-axis position and calibration)
        var region = DetectChartRegion(grid, yAxisLeft);

        // Refine chart top using Y-axis calibration:
        // Chart top should be slightly above the topmost Y-axis label
        if (result.YAxisPoints.Count > 0)
        {
            int topCalibY = result.YAxisPoints[0].Pixel;
            // Chart top = at most 20px above topmost calibration point
            region.top = Math.Max(region.top, topCalibY - 20);
        }

        result.ChartLeft = region.left;
        result.ChartTop = region.top;
        result.ChartRight = region.right;
        result.ChartBottom = region.bottom;

        if (region.right - region.left < 50 || region.bottom - region.top < 30)
        {
            // Chart region too small — can't analyze
            return result;
        }

        // Step 4: X-axis OCR → datetime labels
        result.XAxisPoints = await ExtractXAxis(screenshot, region.left, region.right, region.bottom);

        // Step 6 (before Step 5): Detect volume panel FIRST
        // Volume bars are colored (red/blue) and would be misdetected as candles.
        // We need to exclude the volume region from candle scanning.
        var volumeTop = DetectVolumePanelTop(grid, region.left, region.right, region.bottom, screenshot.Height);
        if (volumeTop != null)
        {
            result.VolumeTop = volumeTop.Value;
            result.VolumeBottom = screenshot.Height - 20; // approximate

            // Remove Y-axis calibration points that fall in the volume area
            // (volume axis labels like "10,000K" would corrupt price calibration)
            result.YAxisPoints = result.YAxisPoints.Where(p => p.Pixel < volumeTop.Value - 10).ToList();
        }

        // Candle scan region: from chart top to volume panel (or chart bottom).
        // Also exclude X-axis label area (typically 20~30px above volume panel).
        int candleScanBottom = volumeTop ?? region.bottom;
        if (candleScanBottom > region.top + 50)
            candleScanBottom -= 25; // exclude X-axis labels + separator line

        // Step 5: Detect candles (only in price chart area, excluding volume panel)
        //
        // Two-strategy approach:
        //   Strategy A: Body-first (default) — finds rectangular bodies ≥3px wide.
        //     Good for normal candle widths. Retries with width≥2, then width≥1 if too few.
        //   Strategy B: Column-scan fallback — scans each X column for colored pixels.
        //     Used when chart density is high (600+ candles in narrow space → ~1px per candle).
        //     Triggered when body-first finds far fewer candles than the chart width suggests.
        //
        // Expected candle count estimation:
        //   Chart width / (average candle spacing from body-first results) ≈ expected total.
        //   If body-first finds < 40% of expected, switch to column-scan.

        var rawCandles = DetectCandles(grid, region.left, region.top, region.right, candleScanBottom);

        if (rawCandles.Count < 15)
        {
            rawCandles = DetectCandles(grid, region.left, region.top, region.right, candleScanBottom, minBodyWidthOverride: 2);
        }
        if (rawCandles.Count < 15)
        {
            rawCandles = DetectCandles(grid, region.left, region.top, region.right, candleScanBottom, minBodyWidthOverride: 1);
        }

        // Strategy B: Column-scan fallback for ultra-dense charts.
        //
        // Detection: Look for large gaps or density mismatch in body-first results.
        // If candles are spaced at ~20px intervals but the chart could fit hundreds
        // of 1px candles, body-first is missing ultra-thin candles.
        //
        // Trigger conditions (ANY of):
        //   (a) Large gap inconsistency: max gap > 8× median gap (missing candles in gaps)
        //   (b) Density mismatch: chart width / min candle width >> found count
        //   (c) Too few candles (< 15)
        int chartWidth = region.right - region.left;
        bool needColumnScan = rawCandles.Count < 15;

        if (!needColumnScan && rawCandles.Count >= 3 && chartWidth > 100)
        {
            // Check gap uniformity between adjacent candles
            var gaps = new List<int>();
            for (int gi = 1; gi < rawCandles.Count; gi++)
                gaps.Add(rawCandles[gi].CenterX - rawCandles[gi - 1].CenterX);

            gaps.Sort();
            double medianGap = gaps[gaps.Count / 2];
            double maxGap = gaps[^1];

            // (a) Large gap inconsistency = likely missing thin candles in that region.
            // A gap > 6× the median suggests a region where thin candles were missed.
            // Example: thin chart has 30 "fat" candles at ~16px spacing, but a 116px gap
            //   (7.25× median) where dozens of 1px thin candles exist but weren't detected.
            // Normal charts have relatively uniform gaps (maxGap < 3× median), so
            //   this condition is specific to ultra-dense charts with unevenly detected candles.
            if (medianGap > 0 && maxGap > medianGap * 6 && maxGap > 30)
                needColumnScan = true;

            // (b) Density mismatch: candle bodies are tiny relative to candle spacing.
            // Guard: only trigger if medianGap > 5× body width (bodies < 20% of pitch)
            // AND the chart could theoretically fit 3× more candles based on body width.
            // In normal charts: medianGap=15, minWidth=3 → 15 > 20? NO → safe.
            // In thin charts: medianGap=16, minWidth=1-2 → 16 > 10-15? YES → triggers.
            int minWidth = rawCandles.Min(c => Math.Max(1, c.Width));
            if (medianGap > (minWidth + 1) * 5)
            {
                int potentialCandles = chartWidth / Math.Max(1, minWidth + 1);
                if (potentialCandles > rawCandles.Count * 3)
                    needColumnScan = true;
            }
        }

        if (needColumnScan)
        {
            var columnCandles = DetectCandlesByColumnScan(
                grid, region.left, region.top, region.right, candleScanBottom);
            if (columnCandles.Count > rawCandles.Count)
                rawCandles = columnCandles;
        }

        // Strategy C: HTS-style detection (fill + border + wick elements).
        // HTS renders candles with 3 distinct elements that don't match the simple
        // red/blue color checks of Strategies A/B:
        //   - Body fill is cream/pale (not red/blue)
        //   - Body border is brown/teal (not red/blue)
        //   - Only wick is vivid red/blue
        // HTS-style uses its own horizontal-run indicator masking to handle MA lines,
        // so it can safely scan a wider Y range (up to volumeTop) without volume bars
        // being misdetected — the indicator mask removes them.
        int htsScanBottom = volumeTop ?? region.bottom;
        var htsCandles = DetectCandlesByHtsStyle(
            grid, region.left, region.top, region.right, htsScanBottom,
            expectedCandleCount);
        // Sanity check: if hts_style finds way too many candles compared to A/B,
        // it's likely picking up indicator lines as fake candles. Only accept if
        // it's a modest improvement (< 5x) or if A/B found very few.
        // Exception: when expectedCandleCount is specified (deltaX mode), trust the
        // HTS result — the user told us the exact candle count via --candles N.
        bool htsPlausible = expectedCandleCount > 0 ||
            rawCandles.Count < 5 ||
            htsCandles.Count <= rawCandles.Count * 5;
        if (htsPlausible && htsCandles.Count > rawCandles.Count)
        {
            rawCandles = htsCandles;
            result.DetectionStrategy = "hts_style";
        }
        else if (rawCandles.Count > 0 && result.DetectionStrategy == null)
        {
            result.DetectionStrategy = needColumnScan ? "column_scan" : "body_first";
        }

        // Detect color convention
        result.BullishColor = DetectColorConvention(rawCandles);
        result.BearishColor = result.BullishColor == "red" ? "blue" : "red";

        // Convert raw candles to CandleData with price values.
        // Clamp prices to Y-axis range to prevent wild extrapolation from noise.
        double priceFloor = result.YAxisPoints.Count > 0
            ? result.YAxisPoints.Min(p => p.Value) * 0.8 // allow 20% below lowest calibration
            : 0;
        double priceCeil = result.YAxisPoints.Count > 0
            ? result.YAxisPoints.Max(p => p.Value) * 1.2 // allow 20% above highest calibration
            : double.MaxValue;

        foreach (var raw in rawCandles)
        {
            bool isBullish = (result.BullishColor == "red" && raw.Color == CandleColor.Red)
                          || (result.BullishColor == "green" && raw.Color == CandleColor.Green);

            // Map pixel Y to price, then clamp to valid range
            double high = Math.Min(priceCeil, Math.Max(priceFloor, Interpolate(raw.WickTopY, result.YAxisPoints)));
            double low = Math.Min(priceCeil, Math.Max(priceFloor, Interpolate(raw.WickBottomY, result.YAxisPoints)));
            double bodyTop = Math.Min(priceCeil, Math.Max(priceFloor, Interpolate(raw.BodyTopY, result.YAxisPoints)));
            double bodyBottom = Math.Min(priceCeil, Math.Max(priceFloor, Interpolate(raw.BodyBottomY, result.YAxisPoints)));

            var candle = new CandleData
            {
                PixelX = raw.CenterX,
                PixelHighY = raw.WickTopY,
                PixelLowY = raw.WickBottomY,
                IsBullish = isBullish,
                High = high,
                Low = low,
            };

            if (isBullish)
            {
                candle.Open = bodyBottom;   // bullish: open at bottom
                candle.Close = bodyTop;      // close at top (higher)
                candle.PixelOpenY = raw.BodyBottomY;
                candle.PixelCloseY = raw.BodyTopY;
            }
            else
            {
                candle.Open = bodyTop;       // bearish: open at top
                candle.Close = bodyBottom;   // close at bottom (lower)
                candle.PixelOpenY = raw.BodyTopY;
                candle.PixelCloseY = raw.BodyBottomY;
            }

            // Assign nearest X-axis label
            candle.DateTime = FindNearestXLabel(raw.CenterX, result.XAxisPoints);

            result.Candles.Add(candle);
        }

        // Step 7: Extract volume bars from volume panel
        if (result.VolumeTop.HasValue && result.VolumeBottom.HasValue && result.Candles.Count > 0)
        {
            int volTop = result.VolumeTop.Value;
            int volBottom = result.VolumeBottom.Value;
            int panelHeight = volBottom - volTop;

            var barTops = ExtractVolumeBars(grid, result.Candles, volTop, volBottom, region.left, region.right);

            if (barTops.Count > 0)
            {
                // Find max bar height for relative volume calculation
                int maxBarHeight = barTops.Values.Min(topY => volBottom - topY); // min topY = tallest bar

                foreach (var (idx, barTopY) in barTops)
                {
                    int barHeight = volBottom - barTopY;
                    // Store as relative volume (0.0 ~ 1.0) — will be recalibrated by tooltip if available
                    result.Candles[idx].Volume = maxBarHeight > 0
                        ? (double)barHeight / maxBarHeight
                        : 0;
                    result.Candles[idx].VolumeBarTopY = barTopY;
                }

                // Candles without a detected bar get volume = 0
                for (int i = 0; i < result.Candles.Count; i++)
                {
                    if (!barTops.ContainsKey(i))
                    {
                        result.Candles[i].Volume = 0;
                        result.Candles[i].VolumeBarTopY = volBottom;
                    }
                }
            }
        }

        return result;
    }

    // ── Step 2: Chart Region Detection ─────────────────────────

    /// <summary>
    /// Auto-detect chart drawing area.
    ///
    /// ── Design Intent: Chart Region Detection ──
    /// Right boundary: determined by Y-axis OCR position (where price numbers start).
    /// Left boundary: detect the dark vertical divider line between HTS indicator panel
    ///   and chart grid. Fallback: use 8% margin + first colored-pixel column.
    /// Top boundary: refined using topmost Y-axis calibration point (−20px).
    /// Bottom boundary: last row with colored pixels (further refined by volume panel exclusion).
    /// ──
    /// </summary>
    private static (int left, int top, int right, int bottom) DetectChartRegion(
        PixelGrid grid, int yAxisLeftX)
    {
        int w = grid.Width, h = grid.Height;

        // Right boundary: just left of Y-axis numbers
        int right = yAxisLeftX > 0 ? yAxisLeftX - 5 : (int)(w * 0.85);

        // LEFT boundary: Find where chart grid starts.
        // Strategy 1: Look for a dark vertical line (panel border) in the left 30% of image.
        //   HTS indicator panels end with a 1-2px dark vertical divider line.
        // Strategy 2: Fallback — find columns with sustained candle-colored pixels
        //   but skip leftmost region where indicator text creates false positives.
        int left = 0;
        int scanYFrom = h / 4;
        int scanYTo = h * 3 / 4;
        int sampleCount = (scanYTo - scanYFrom) / 2;

        // Strategy 1: Dark vertical divider line (panel | chart boundary)
        // Look for a column where most pixels are dark (gray/black border)
        int maxScanX = Math.Min(right / 3, 200); // only check first 1/3 or 200px
        for (int x = 5; x < maxScanX; x++)
        {
            int darkCount = 0;
            for (int y = scanYFrom; y < scanYTo; y += 2)
            {
                var (r, g, b) = grid.GetPixel(x, y);
                // Dark pixel: all channels low (border line)
                if (r < 100 && g < 100 && b < 100) darkCount++;
            }

            // Column is >50% dark → might be a panel border
            if (darkCount > sampleCount * 0.5)
            {
                // Verify: right side of this line should be brighter (chart background)
                int brightAfter = 0;
                for (int y = scanYFrom; y < scanYTo; y += 2)
                {
                    if (x + 2 < w && grid.IsBright(x + 2, y)) brightAfter++;
                }

                if (brightAfter > sampleCount * 0.3)
                {
                    left = x + 1;
                    break;
                }
            }
        }

        // Strategy 2: If no divider found, find the leftmost X where candle-colored pixels
        // form at regular intervals (actual candles) vs. scattered text
        if (left == 0)
        {
            // Fallback: use 8% of image width as minimum left margin
            // (most HTS have some left panel)
            left = Math.Max(5, w / 12);

            // Then scan for the first column with sustained colored pixels
            for (int x = left; x < right / 2; x++)
            {
                int colorCount = 0;
                for (int y = scanYFrom; y < scanYTo; y += 3)
                {
                    if (grid.IsReddish(x, y) || grid.IsBluish(x, y))
                        colorCount++;
                }
                if (colorCount >= 3) { left = Math.Max(left, x - 2); break; }
            }
        }

        // TOP boundary: find first row with significant colored (candle) pixels
        // Skip header/toolbar area
        int top = h / 8;
        for (int y = 0; y < h / 2; y++)
        {
            int colorCount = 0;
            for (int x = left + 20; x < right; x += 5) // skip leftmost 20px
            {
                if (grid.IsReddish(x, y) || grid.IsBluish(x, y))
                    colorCount++;
            }
            if (colorCount >= 3) { top = Math.Max(0, y - 5); break; }
        }

        // BOTTOM boundary: find last row with significant colored pixels
        int bottom = h - 20;
        for (int y = h - 1; y > h / 2; y--)
        {
            int colorCount = 0;
            for (int x = left + 20; x < right; x += 5)
            {
                if (grid.IsReddish(x, y) || grid.IsBluish(x, y))
                    colorCount++;
            }
            if (colorCount >= 3) { bottom = Math.Min(h - 1, y + 5); break; }
        }

        return (left, top, right, bottom);
    }

    // ── Step 3: Y-Axis OCR → Price Calibration ────────────────

    /// <summary>
    /// Scan rightmost region of image for Y-axis price labels.
    /// Returns calibration points AND the X position where axis starts.
    ///
    /// ── Design Intent: Y-Axis OCR Calibration ──
    /// 1. Crop rightmost 10% → 2× upscale (HTS fonts are tiny) → OCR
    /// 2. ParsePriceText handles: "47,500" → 47500, "35," → 35000 (trailing separator = ×1000)
    /// 3. Scale consistency check: if median > 1000 and some values < median/100, scale up ×1000
    /// 4. Monotonicity filter: prices must decrease as Y increases (top=high price)
    /// 5. After volume panel detection, remove any calibration points inside volume area
    /// ──
    /// </summary>
    private async Task<(List<AxisPoint> points, int axisLeftX)> ScanYAxisRegion(Bitmap screenshot)
    {
        var points = new List<AxisPoint>();
        int w = screenshot.Width;
        int h = screenshot.Height;

        // Scan the rightmost 10% of image for price numbers
        // Use wider strip + upscale for small HTS fonts
        int stripLeft = (int)(w * 0.90);
        int stripWidth = w - stripLeft;

        if (stripWidth < 20) return (points, w - 60);

        using var strip = CropRegion(screenshot, stripLeft, 0, stripWidth, h);

        // Upscale 2x for better OCR on small price labels
        int upscale = 2;
        using var upscaled = new Bitmap(strip.Width * upscale, strip.Height * upscale);
        using (var g = Graphics.FromImage(upscaled))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(strip, 0, 0, upscaled.Width, upscaled.Height);
        }
        var ocrResult = await _ocr.RecognizeAll(upscaled);

        int minWordX = stripWidth; // track leftmost price label X

        foreach (var word in ocrResult.Words)
        {
            var price = ParsePriceText(word.Text);
            if (price == null || price < 1) continue; // skip tiny numbers

            // Scale coordinates back to original image space
            int pixelY = word.Y / upscale + word.Height / (2 * upscale);

            points.Add(new AxisPoint
            {
                Pixel = pixelY,
                Value = price.Value,
                Label = word.Text
            });

            int origX = word.X / upscale;
            if (origX < minWordX) minWordX = origX;
        }

        // Sort by pixel Y
        points.Sort((a, b) => a.Pixel.CompareTo(b.Pixel));

        // Scale consistency check: if some values are much smaller than others,
        // they may need ×1000 correction. E.g. [44600, 35, 30, 25] → 35→35000
        if (points.Count >= 3)
        {
            // Find the median value to determine expected scale
            var sortedValues = points.Select(p => p.Value).OrderBy(v => v).ToList();
            double median = sortedValues[sortedValues.Count / 2];

            // If median is large (>1000), check for suspiciously small values
            if (median > 1000)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    // Value is at least 100x smaller than median → likely needs ×1000
                    if (points[j].Value < median / 100 && points[j].Value > 0)
                    {
                        points[j].Value *= 1000;
                        points[j].Label += " (×1000)";
                    }
                }
            }
            // If median is small (<100) but one value is huge → the huge one might be correct
            // and the small ones need scaling. Use the largest correctly-parsed value as reference.
            else if (median < 100)
            {
                double maxVal = sortedValues[^1];
                if (maxVal > 1000)
                {
                    // Scale up all small values
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (points[j].Value < maxVal / 100 && points[j].Value > 0)
                        {
                            points[j].Value *= 1000;
                            points[j].Label += " (×1000)";
                        }
                    }
                }
            }
        }

        // Remove outliers: prices should decrease as Y increases (top=high price)
        if (points.Count >= 2)
        {
            var cleaned = new List<AxisPoint> { points[0] };
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].Value < cleaned[^1].Value)
                    cleaned.Add(points[i]);
            }
            // If we lost too many, just keep original
            if (cleaned.Count >= 2)
                points = cleaned;
        }

        int axisLeftX = stripLeft + minWordX;
        return (points, axisLeftX);
    }

    /// <summary>
    /// Extract Y-axis price labels from right edge of chart (legacy — now uses ScanYAxisRegion).
    /// </summary>
    private async Task<List<AxisPoint>> ExtractYAxis(
        Bitmap screenshot, int chartRight, int chartTop, int chartBottom)
    {
        var points = new List<AxisPoint>();

        // Crop right strip (Y-axis area): from chartRight to image edge
        int stripLeft = Math.Max(0, chartRight - 10);
        int stripWidth = screenshot.Width - stripLeft;
        int stripTop = chartTop;
        int stripHeight = chartBottom - chartTop;

        if (stripWidth < 20 || stripHeight < 20) return points;

        using var strip = CropRegion(screenshot, stripLeft, stripTop, stripWidth, stripHeight);
        var ocrResult = await _ocr.RecognizeAll(strip);

        foreach (var word in ocrResult.Words)
        {
            // Parse price value from OCR text: "47,500" "44600" "35.400" etc.
            var price = ParsePriceText(word.Text);
            if (price == null) continue;

            // Y center in original image coordinates
            int pixelY = word.Y + word.Height / 2 + stripTop;

            points.Add(new AxisPoint
            {
                Pixel = pixelY,
                Value = price.Value,
                Label = word.Text
            });
        }

        // Sort by pixel Y (top to bottom)
        points.Sort((a, b) => a.Pixel.CompareTo(b.Pixel));

        // Remove outliers: prices should decrease as Y increases (top=high price)
        // Keep only monotonically decreasing sequences
        if (points.Count >= 2)
        {
            var cleaned = new List<AxisPoint> { points[0] };
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].Value < cleaned[^1].Value)
                    cleaned.Add(points[i]);
            }
            points = cleaned;
        }

        return points;
    }

    // ── Step 4: X-Axis OCR → DateTime Labels ──────────────────

    /// <summary>
    /// Extract X-axis date/time labels from bottom of chart.
    /// </summary>
    private async Task<List<AxisPoint>> ExtractXAxis(
        Bitmap screenshot, int chartLeft, int chartRight, int chartBottom)
    {
        var points = new List<AxisPoint>();

        // Crop bottom strip (X-axis area)
        int stripTop = chartBottom;
        int stripHeight = Math.Min(40, screenshot.Height - chartBottom);
        int stripLeft = chartLeft;
        int stripWidth = chartRight - chartLeft;

        if (stripWidth < 50 || stripHeight < 10) return points;

        using var strip = CropRegion(screenshot, stripLeft, stripTop, stripWidth, stripHeight);
        var ocrResult = await _ocr.RecognizeAll(strip);

        foreach (var word in ocrResult.Words)
        {
            var text = word.Text.Trim();
            if (string.IsNullOrEmpty(text)) continue;

            // Accept date/time patterns: "02/13", "2026/01", "10", "11:30"
            if (!IsDateTimeLabel(text)) continue;

            int pixelX = word.X + word.Width / 2 + stripLeft;

            points.Add(new AxisPoint
            {
                Pixel = pixelX,
                Value = 0, // datetime as string, not numeric
                Label = text
            });
        }

        points.Sort((a, b) => a.Pixel.CompareTo(b.Pixel));
        return points;
    }

    // ── Step 5: Candle Detection ──────────────────────────────

    /// <summary>
    /// Detect candlesticks by scanning for red/blue pixel columns.
    ///
    /// ── Design Intent: Body-First Candle Detection ──
    /// Problem: HTS charts have many overlays (MA lines, Bollinger bands, text labels)
    ///   that use the same red/blue colors as candles. Column-based scanning picks up
    ///   all colored pixels in a column, making wicks impossibly long.
    ///
    /// Solution: "Body-First" 4-phase approach
    ///   Phase 1: Scan all rows for horizontal runs of same-color pixels (width ≥ 3).
    ///            MA lines are 1-2px thin → filtered out. Text is scattered → no solid runs.
    ///   Phase 2: Cluster horizontal runs into rectangular bodies (vertically contiguous).
    ///   Phase 3: Filter by minimum size + fill ratio → reject text fragments.
    ///   Phase 4: Trace wicks from body center with MAX LENGTH CLAMP (3× body height)
    ///            → prevents wick from connecting to MA lines below/above body.
    ///
    /// Why not column-based: A single MA line crossing a candle's X position would create
    ///   a "wick" spanning from the MA line to the actual candle body — wildly wrong.
    /// ──
    /// </summary>
    private static List<RawCandle> DetectCandles(
        PixelGrid grid, int left, int top, int right, int bottom,
        int minBodyWidthOverride = 0)
    {
        var candles = new List<RawCandle>();

        // Phase 1: Build horizontal density map
        // For each Y row, find horizontal runs of same-color pixels.
        // Body run width threshold is ADAPTIVE:
        //   Normal candles: width >= 3 (filters MA lines)
        //   Thin candles (minimized width setting): width >= 2
        //   Ultra-thin: width >= 1 (single pixel column candles)
        //
        // First pass: collect ALL runs (width >= 1) to determine typical candle width.
        // Then apply adaptive threshold.

        var allRuns = new List<BodySegment>();

        for (int y = top; y <= bottom; y++)
        {
            int runStart = -1;
            CandleColor runColor = CandleColor.None;
            int runLen = 0;

            for (int x = left; x <= right + 1; x++)
            {
                CandleColor c = CandleColor.None;
                if (x <= right)
                {
                    if (grid.IsReddish(x, y)) c = CandleColor.Red;
                    else if (grid.IsBluish(x, y)) c = CandleColor.Blue;
                    else if (grid.IsGreenish(x, y)) c = CandleColor.Green;
                }

                if (c == runColor && c != CandleColor.None)
                {
                    runLen++;
                }
                else
                {
                    if (runLen >= 1 && runColor != CandleColor.None)
                    {
                        allRuns.Add(new BodySegment
                        {
                            X = runStart,
                            Y = y,
                            Width = runLen,
                            Color = runColor
                        });
                    }

                    runStart = x;
                    runColor = c;
                    runLen = 1;
                }
            }
        }

        // Adaptive body width threshold:
        // First cluster ALL runs into bodies to see what sizes emerge.
        // The key insight: in thin-candle mode, real candles form tall narrow columns
        // (height >> width), while text forms short wide fragments (width >> height).
        // So we use runs >= 1px but apply stricter HEIGHT filter in Phase 3.
        //
        // Strategy: always include 3px+ runs. For 1-2px runs, check if enough
        // form vertically continuous structures (potential thin candles).
        int runs1px = allRuns.Count(r => r.Width == 1);
        int runs2px = allRuns.Count(r => r.Width == 2);
        int runs3plus = allRuns.Count(r => r.Width >= 3);

        // Adaptive width: use override if specified, else default to 3
        int minBodyWidth = minBodyWidthOverride > 0 ? minBodyWidthOverride : 3;

        // Thin-candle adaptive threshold is handled by retry in Analyze()
        // (if too few candles at width=3, caller retries with width=2, then 1)

        var bodySegments = allRuns.Where(r => r.Width >= minBodyWidth).ToList();

        // Phase 2: Cluster body segments into candle bodies
        // Group segments by X overlap and same color → form rectangular bodies
        // Sort by X position for clustering
        bodySegments.Sort((a, b) => a.X != b.X ? a.X.CompareTo(b.X) : a.Y.CompareTo(b.Y));

        var candleBodies = new List<CandleBody>();

        for (int si = 0; si < bodySegments.Count; si++)
        {
            var seg = bodySegments[si];
            // Try to attach to an existing body
            bool attached = false;
            for (int bi = candleBodies.Count - 1; bi >= 0; bi--)
            {
                var body = candleBodies[bi];
                if (body.Color != seg.Color) continue;

                // Check X overlap
                int overlapLeft = Math.Max(body.Left, seg.X);
                int overlapRight = Math.Min(body.Right, seg.X + seg.Width - 1);
                if (overlapRight < overlapLeft) continue; // no overlap
                int overlap = overlapRight - overlapLeft + 1;
                int minWidth = Math.Min(body.Right - body.Left + 1, seg.Width);
                if (overlap < minWidth / 2) continue; // less than half overlap

                // Check Y proximity (must be within 2px of existing body rows)
                if (seg.Y > body.BottomY + 2) continue; // too far below
                if (seg.Y < body.TopY - 2) continue;     // too far above

                // Attach
                body.TopY = Math.Min(body.TopY, seg.Y);
                body.BottomY = Math.Max(body.BottomY, seg.Y);
                body.Left = Math.Min(body.Left, seg.X);
                body.Right = Math.Max(body.Right, seg.X + seg.Width - 1);
                body.RowCount++;
                attached = true;
                break;
            }

            if (!attached)
            {
                candleBodies.Add(new CandleBody
                {
                    Left = seg.X,
                    Right = seg.X + seg.Width - 1,
                    TopY = seg.Y,
                    BottomY = seg.Y,
                    Color = seg.Color,
                    RowCount = 1
                });
            }
        }

        // Phase 3: Filter valid bodies (adaptive to candle width)
        // Normal mode (minBodyWidth=3): require width >= 3, height >= 2
        // Thin mode (minBodyWidth=1-2): relax width requirement, rely more on height
        candleBodies = candleBodies.Where(b =>
        {
            int bodyH = b.BottomY - b.TopY + 1;
            int bodyW = b.Right - b.Left + 1;

            // Minimum size adapts to candle width mode
            if (bodyW < minBodyWidth) return false;

            // Height requirement: at least 2px (even for thin candles)
            if (bodyH < 2) return false;

            // Body must be reasonably filled (not just 2 rows with a gap)
            if (b.RowCount < bodyH * 0.5) return false;

            // Reject very wide, very short bodies (horizontal text or MA line)
            if (bodyW > 15 && bodyH < bodyW / 5) return false;

            // In thin mode, additional check: single-column candles must be tall enough
            // to distinguish from scattered noise pixels
            if (minBodyWidth <= 2 && bodyW <= 2 && bodyH < 4) return false;

            // Reject bodies too close to right edge — likely Y-axis price label text.
            // Real candles end at least 10px before the chart right boundary.
            if (b.Left > right - 8) return false;

            return true;
        }).ToList();

        // Merge bodies that overlap horizontally (same X range, close Y)
        candleBodies = MergeCandleBodies(candleBodies);

        // Phase 4: For each body, trace wick upward and downward
        // Wick: thin (1-2px) column of same color extending from body center.
        // MAX WICK LENGTH: clamp to 5× body height to prevent MA-line contamination.
        // (Real candle wicks rarely exceed 3× body height in a single bar)
        foreach (var body in candleBodies)
        {
            int centerX = (body.Left + body.Right) / 2;
            int bodyW = body.Right - body.Left + 1;
            int bodyH = body.BottomY - body.TopY + 1;
            int maxWickLen = Math.Max(bodyH * 3, 15); // conservative: avoid MA contamination

            // Trace wick upward from body top
            int wickTopY = body.TopY;
            for (int y = body.TopY - 1; y >= top && (body.TopY - y) <= maxWickLen; y--)
            {
                // Check if there's a colored pixel at or near center (within 1px)
                bool found = false;
                for (int dx = -1; dx <= 1; dx++)
                {
                    int px = centerX + dx;
                    if (px < 0 || px >= grid.Width) continue;
                    bool match = body.Color switch
                    {
                        CandleColor.Red => grid.IsReddish(px, y),
                        CandleColor.Blue => grid.IsBluish(px, y),
                        CandleColor.Green => grid.IsGreenish(px, y),
                        _ => false
                    };
                    if (match) { found = true; break; }
                }
                if (found) wickTopY = y;
                else break; // wick ends at first gap
            }

            // Trace wick downward from body bottom
            int wickBottomY = body.BottomY;
            for (int y = body.BottomY + 1; y <= bottom && (y - body.BottomY) <= maxWickLen; y++)
            {
                bool found = false;
                for (int dx = -1; dx <= 1; dx++)
                {
                    int px = centerX + dx;
                    if (px < 0 || px >= grid.Width) continue;
                    bool match = body.Color switch
                    {
                        CandleColor.Red => grid.IsReddish(px, y),
                        CandleColor.Blue => grid.IsBluish(px, y),
                        CandleColor.Green => grid.IsGreenish(px, y),
                        _ => false
                    };
                    if (match) { found = true; break; }
                }
                if (found) wickBottomY = y;
                else break;
            }

            candles.Add(new RawCandle
            {
                CenterX = centerX,
                Width = bodyW,
                WickTopY = wickTopY,
                WickBottomY = wickBottomY,
                BodyTopY = body.TopY,
                BodyBottomY = body.BottomY,
                Color = body.Color
            });
        }

        // Sort by X
        candles.Sort((a, b) => a.CenterX.CompareTo(b.CenterX));

        // Merge candles that are too close (within 3px) — likely same candle split
        candles = MergeCloseCandles(candles, minGap: 3);

        // Post-filter: remove remaining noise
        int chartHeight = bottom - top;
        if (candles.Count > 5 && chartHeight > 20)
        {
            // Remove candles that span >70% of chart height (still contaminated)
            int heightThreshold = (int)(chartHeight * 0.70);
            candles = candles.Where(c => (c.WickBottomY - c.WickTopY) < heightThreshold).ToList();
        }

        // Post-filter 2: Remove text overlay outliers
        // Text overlays (e.g. "최고 48,700(02/13)", price annotations) appear as colored text
        // in the chart area. Their body Y-positions are often extreme outliers compared to
        // neighboring candles. Use rolling median comparison to detect.
        if (candles.Count >= 5)
        {
            candles = FilterTextOverlayOutliers(candles, top, bottom);
        }

        return candles;
    }

    /// <summary>
    /// Merge candle bodies that overlap horizontally and are same color.
    /// </summary>
    private static List<CandleBody> MergeCandleBodies(List<CandleBody> bodies)
    {
        if (bodies.Count <= 1) return bodies;

        // Sort by center X
        bodies.Sort((a, b) => ((a.Left + a.Right) / 2).CompareTo((b.Left + b.Right) / 2));

        var merged = new List<CandleBody> { bodies[0] };
        for (int i = 1; i < bodies.Count; i++)
        {
            var prev = merged[^1];
            var curr = bodies[i];

            // Check if they overlap in X and have same color
            if (curr.Color == prev.Color && curr.Left <= prev.Right + 2)
            {
                // Merge
                prev.Left = Math.Min(prev.Left, curr.Left);
                prev.Right = Math.Max(prev.Right, curr.Right);
                prev.TopY = Math.Min(prev.TopY, curr.TopY);
                prev.BottomY = Math.Max(prev.BottomY, curr.BottomY);
                prev.RowCount += curr.RowCount;
            }
            else
            {
                merged.Add(curr);
            }
        }
        return merged;
    }

    /// <summary>
    /// Merge candles that are within minGap pixels of each other and same color.
    /// </summary>
    private static List<RawCandle> MergeCloseCandles(List<RawCandle> candles, int minGap)
    {
        if (candles.Count <= 1) return candles;

        var merged = new List<RawCandle> { candles[0] };
        for (int i = 1; i < candles.Count; i++)
        {
            var prev = merged[^1];
            var curr = candles[i];

            int gap = curr.CenterX - curr.Width / 2 - (prev.CenterX + prev.Width / 2);
            if (gap <= minGap && curr.Color == prev.Color)
            {
                // Merge
                prev.WickTopY = Math.Min(prev.WickTopY, curr.WickTopY);
                prev.WickBottomY = Math.Max(prev.WickBottomY, curr.WickBottomY);
                prev.BodyTopY = Math.Min(prev.BodyTopY, curr.BodyTopY);
                prev.BodyBottomY = Math.Max(prev.BodyBottomY, curr.BodyBottomY);
                int newRight = curr.CenterX + curr.Width / 2;
                int newLeft = prev.CenterX - prev.Width / 2;
                prev.Width = newRight - newLeft;
                prev.CenterX = (newLeft + newRight) / 2;
            }
            else
            {
                merged.Add(curr);
            }
        }
        return merged;
    }

    /// <summary>
    /// Column-scan fallback for ultra-dense charts where body-first detection fails.
    ///
    /// ── Design Intent: Column-Scan for 1px Candles ──
    /// In 600-day charts with minimized candle width, each candle gets ~1px of horizontal space.
    /// Body-first detection requires rectangular bodies (≥2px height), which don't exist at this density.
    ///
    /// Column-scan approach:
    ///   1. For each X column in the chart region, count red/blue/green pixels vertically
    ///   2. Determine dominant color per column (majority wins)
    ///   3. Group consecutive same-color columns into candle segments
    ///   4. For each segment: topmost colored pixel = wick top, bottommost = wick bottom
    ///      Densest colored pixel range = body
    ///   5. Single-column candles are valid (1px body = doji or tiny range candle)
    ///
    /// Noise handling:
    ///   - MA lines are continuous across many columns → filter by checking if a column's
    ///     colored pixel count is suspiciously low (1-2 scattered pixels = MA/indicator line)
    ///   - Require at least 2 colored pixels per column to count as a candle
    ///   - Use rolling window: isolated colored columns (no neighbors) are likely noise
    /// ──
    /// </summary>
    private static List<RawCandle> DetectCandlesByColumnScan(
        PixelGrid grid, int left, int top, int right, int bottom)
    {
        int chartHeight = bottom - top;
        if (chartHeight < 10) return new List<RawCandle>();

        // Step 1: For each X column, collect colored pixel profile
        var columns = new List<ColumnProfile>();
        for (int x = left; x <= right; x++)
        {
            // Skip columns too close to right edge (Y-axis labels)
            if (x > right - 8) continue;

            int redCount = 0, blueCount = 0, greenCount = 0;
            int minColoredY = int.MaxValue, maxColoredY = int.MinValue;
            // Track longest continuous run of colored pixels (to distinguish candle body from MA scatter)
            int curRun = 0, maxRun = 0;

            for (int y = top; y <= bottom; y++)
            {
                CandleColor c = CandleColor.None;
                if (grid.IsReddish(x, y)) c = CandleColor.Red;
                else if (grid.IsBluish(x, y)) c = CandleColor.Blue;
                else if (grid.IsGreenish(x, y)) c = CandleColor.Green;

                if (c != CandleColor.None)
                {
                    if (c == CandleColor.Red) redCount++;
                    else if (c == CandleColor.Blue) blueCount++;
                    else greenCount++;

                    if (y < minColoredY) minColoredY = y;
                    if (y > maxColoredY) maxColoredY = y;

                    curRun++;
                    if (curRun > maxRun) maxRun = curRun;
                }
                else
                {
                    curRun = 0;
                }
            }

            int totalColored = redCount + blueCount + greenCount;
            if (totalColored < 2) continue; // skip single MA dots

            // Skip columns that span too much of the chart (MA line crossing entire height)
            if (maxColoredY - minColoredY > chartHeight * 0.6) continue;

            // Key MA-line filter: if colored pixels are scattered (no continuous run ≥ 2),
            // this is likely a column where multiple MA lines cross, not a candle.
            // Real candles have at least a small body (≥ 2 continuous colored pixels).
            // Exception: if total colored is high (≥ 4), keep even with short runs
            // (could be a doji with wick pixels)
            if (maxRun < 2 && totalColored < 4) continue;

            // Determine dominant color
            CandleColor dominant;
            if (redCount >= blueCount && redCount >= greenCount) dominant = CandleColor.Red;
            else if (blueCount >= redCount && blueCount >= greenCount) dominant = CandleColor.Blue;
            else dominant = CandleColor.Green;

            columns.Add(new ColumnProfile
            {
                X = x,
                Color = dominant,
                TopY = minColoredY,
                BottomY = maxColoredY,
                PixelCount = totalColored
            });
        }

        if (columns.Count == 0) return new List<RawCandle>();

        // Step 2: Group consecutive columns into candle segments
        // Allow 1px gap (for anti-aliased candle edges)
        var candles = new List<RawCandle>();
        int segStart = 0;
        for (int i = 1; i <= columns.Count; i++)
        {
            bool isGap = (i == columns.Count) ||
                         (columns[i].X > columns[i - 1].X + 2) || // gap > 2px
                         (columns[i].Color != columns[segStart].Color); // color change

            if (isGap)
            {
                // Build candle from segment [segStart, i-1]
                int segEnd = i - 1;
                int minY = int.MaxValue, maxY = int.MinValue;
                int totalPx = 0;
                for (int j = segStart; j <= segEnd; j++)
                {
                    if (columns[j].TopY < minY) minY = columns[j].TopY;
                    if (columns[j].BottomY > maxY) maxY = columns[j].BottomY;
                    totalPx += columns[j].PixelCount;
                }

                int width = columns[segEnd].X - columns[segStart].X + 1;
                int centerX = (columns[segStart].X + columns[segEnd].X) / 2;
                int height = maxY - minY + 1;

                // Skip segments too close to right edge (Y-axis price label text)
                if (columns[segStart].X > right - 8) { segStart = i; continue; }

                // Estimate body: densest vertical range within the segment
                // For thin candles, body ≈ the main cluster of colored pixels
                var (bodyTop, bodyBot) = EstimateBody(grid, centerX, minY, maxY, columns[segStart].Color);

                // Clamp wick length relative to body size.
                // Without this, MA lines at different Y positions create fake long wicks.
                // For column-scan candles (thin), use tighter clamp: max 5× body height or 20px.
                int bodyH = Math.Max(1, bodyBot - bodyTop + 1);
                int maxWick = Math.Max(bodyH * 5, 20);
                int clampedWickTop = Math.Max(minY, bodyTop - maxWick);
                int clampedWickBot = Math.Min(maxY, bodyBot + maxWick);

                candles.Add(new RawCandle
                {
                    CenterX = centerX,
                    Width = width,
                    WickTopY = clampedWickTop,
                    WickBottomY = clampedWickBot,
                    BodyTopY = bodyTop,
                    BodyBottomY = bodyBot,
                    Color = columns[segStart].Color
                });

                segStart = i;
            }
        }

        // Step 3: Filter obvious noise
        // Remove isolated candles with very few pixels
        if (candles.Count > 10)
        {
            double avgPixels = candles.Average(c => c.BodyBottomY - c.BodyTopY + 1);
            candles = candles.Where(c =>
            {
                int bodyH = c.BodyBottomY - c.BodyTopY + 1;
                // Keep if body has at least 1px height (even doji)
                return bodyH >= 1;
            }).ToList();
        }

        // Merge close candles and apply text overlay filter
        candles = MergeCloseCandles(candles, minGap: 1); // tighter merge for dense charts

        if (candles.Count >= 5)
        {
            candles = FilterTextOverlayOutliers(candles, top, bottom);
        }

        return candles;
    }

    // ── Strategy C: HTS-Style Candle Detection ──────────────────

    /// <summary>
    /// Strategy C: Detect candles by recognizing the 3-element HTS rendering style.
    ///
    /// ── Design Intent: HTS-Aware Multi-Element Detection ──
    ///
    /// WHY THIS EXISTS:
    ///   Strategies A/B use IsReddish()/IsBluish() which check for saturated red/blue.
    ///   But HTS candle bodies are cream/pale-blue (fill) and brown/teal (border) —
    ///   they DON'T pass those checks! Only the wick is vivid red/blue.
    ///   This strategy uses ClassifyHts() to recognize all 4 HTS elements.
    ///
    /// HTS RENDERING CHARACTERISTICS (GDI raster):
    ///   - NO anti-aliasing, NO sub-pixel rendering
    ///   - Only ~21 unique colors in the entire chart area
    ///   - Wick pixels are 1px wide, pixel-snapped, with pure white neighbors
    ///   - 4 element types: Fill, Border, Wick, Outline (see HtsElement enum)
    ///
    /// 4-PHASE ALGORITHM:
    ///   Phase 1: Build indicator mask (horizontal-run analysis per Y row)
    ///            Wick runs ≥8px = MA line → mask those pixels.
    ///            Real candle wicks = 1-7px runs → preserved.
    ///   Phase 2: Build per-column HTS profile using masked data
    ///   Phase 3: Ultra-dense detection + candle segmentation
    ///            >70% columns have HTS elements → ultra-dense mode
    ///   Phase 4: Post-filter (height threshold + text overlay, skipped in ultra-dense)
    ///
    /// ULTRA-DENSE MODE (600-day charts, ~1.04px per candle):
    ///   Key insight from user: "캔들은 중간에 사라지지 않고 공평한 거리를 유지한다"
    ///   (Candles never disappear — each maintains equal spacing)
    ///
    ///   When expectedCandleCount > 0 (from OCR):
    ///     → DeltaX-based placement: deltaX = pixelSpan / candleCount
    ///     → Each candle owns columns [floor(i*dX), floor((i+1)*dX))
    ///     → Fractional candle width: some get 1px, some 2px (mirrors HTS float rendering)
    ///     → Gives exactly N candles matching actual trading days
    ///
    ///   When expectedCandleCount = 0 (unknown):
    ///     → Fallback: every column with any HTS element = 1 candle
    ///     → May produce more candles than actual (float boundary duplicates)
    ///
    ///   Body estimation in ultra-dense:
    ///     → Border pixels within wick Y range = body extent (O/C)
    ///     → If no border, estimate body as wick midpoint ±20%
    ///
    /// NORMAL-DENSITY MODE:
    ///   Wick-based segmentation: find columns with wick, extend to adjacent same-color.
    ///   Body from fill/border extent. Wick clamped to 5× body height.
    /// ──
    /// </summary>
    private static List<RawCandle> DetectCandlesByHtsStyle(
        PixelGrid grid, int left, int top, int right, int bottom,
        int expectedCandleCount = 0)
    {
        int chartHeight = bottom - top;
        if (chartHeight < 10) return new List<RawCandle>();

        int rightEdge = right - 8; // exclude Y-axis label area
        int totalCols = rightEdge - left;
        if (totalCols <= 0) return new List<RawCandle>();

        // Phase 1: Build indicator mask using horizontal run analysis.
        // Moving average (MA) lines in HTS are drawn in wick colors (red/blue).
        // In ultra-dense charts, MA lines form horizontal runs of ≥3px at a given Y,
        // while real candle wicks are only 1-2px wide (one column per candle).
        // For each Y, scan horizontal wick runs. Mark pixels in runs ≥ indicatorRunLen
        // as indicator (not real wick). This preserves real 1-2px candle wicks while
        // removing MA lines regardless of how many columns they cross.
        //
        // indicatorRunLen threshold:
        //   HTS draws MA lines by connecting per-candle center points with LineTo.
        //   In ultra-dense charts, MA lines form runs matching the candle pitch:
        //     e.g., 600 candles in 624px → ~9px between candle gaps.
        //   Real candle wicks are 1-2px per candle (ultra-dense) or body-width (normal).
        //   At Y levels where candle wicks cluster (same price), runs can be 3-7px
        //   (consecutive candles at similar price), but NOT 8-9px (that's MA pitch).
        //
        //   Distribution analysis from 600-day chart:
        //     len 1: 602 (real candle wick)
        //     len 2: 32  (candle wick at float boundary)
        //     len 3-7: 119 (clustered candle wicks at same price)
        //     len 8-9: 908 (MA lines matching candle pitch)
        //
        //   Threshold = 8: preserves candle wick clusters up to 7px,
        //   masks MA lines at 8-9px. Safe for ultra-dense.
        //   For normal charts, MA lines are even longer (20+px) so 8 is conservative.
        var indicatorMask = new bool[grid.Width, grid.Height];
        var borderMask = new bool[grid.Width, grid.Height];
        int indicatorRunLen = 8; // runs of wick-colored pixels ≥ this length = MA indicator
        // Border mask threshold: 매물대/수평밴드는 차트 폭의 상당 부분을 차지.
        // Real candle borders are 1-3px wide. Use same threshold (8) to mask
        // long horizontal runs of border-colored pixels (매물대 bands).
        int borderRunLen = 8;

        for (int y = top; y <= bottom; y++)
        {
            // Scan this row for wick-colored runs AND border-colored runs separately
            int wickRunStart = -1;
            int borderRunStart = -1;
            for (int x = left; x <= rightEdge; x++)
            {
                var (elem, _) = grid.ClassifyHts(x, y);

                // Wick run tracking
                if (elem == HtsElement.Wick)
                {
                    if (wickRunStart < 0) wickRunStart = x;
                }
                else
                {
                    if (wickRunStart >= 0)
                    {
                        int runLen = x - wickRunStart;
                        if (runLen >= indicatorRunLen)
                        {
                            for (int mx = wickRunStart; mx < x; mx++)
                                indicatorMask[mx, y] = true;
                        }
                        wickRunStart = -1;
                    }
                }

                // Border run tracking (매물대 bands are long horizontal border-colored runs)
                if (elem == HtsElement.Border)
                {
                    if (borderRunStart < 0) borderRunStart = x;
                }
                else
                {
                    if (borderRunStart >= 0)
                    {
                        int runLen = x - borderRunStart;
                        if (runLen >= borderRunLen)
                        {
                            for (int mx = borderRunStart; mx < x; mx++)
                                borderMask[mx, y] = true;
                        }
                        borderRunStart = -1;
                    }
                }
            }
            // End of row — flush pending runs
            if (wickRunStart >= 0)
            {
                int runLen = rightEdge + 1 - wickRunStart;
                if (runLen >= indicatorRunLen)
                {
                    for (int mx = wickRunStart; mx <= rightEdge; mx++)
                        indicatorMask[mx, y] = true;
                }
            }
            if (borderRunStart >= 0)
            {
                int runLen = rightEdge + 1 - borderRunStart;
                if (runLen >= borderRunLen)
                {
                    for (int mx = borderRunStart; mx <= rightEdge; mx++)
                        borderMask[mx, y] = true;
                }
            }
        }

        // Phase 1b: Y-frequency dashed-line indicator mask
        // Dashed indicators (e.g., 프로그램순매수금액) have wick-colored pixels at
        // the same Y across many columns but with gaps (dash pattern), so each
        // individual run is short (1-3px) and passes the horizontal-run mask.
        // Detect: count wick-colored columns per Y row. If > 20% of chart width,
        // it's an indicator line (real candle wicks are 1px wide, so they can't
        // appear on the same Y in >20% of columns even in ultra-dense 600-day charts).
        // This is SAFE unlike the old Y-frequency approach because:
        //   - Old approach: filtered ANY Y with high total pixel count → killed same-price wick clusters
        //   - New approach: counts COLUMNS (not pixels) and uses high threshold (20%)
        //   - Same-price wick clusters: many columns have wick at similar Y, but they
        //     span a Y RANGE (not exact same Y). Indicator lines hit exact Y rows.
        {
            int chartWidth = rightEdge - left + 1;
            int freqThreshold = Math.Max(20, chartWidth * 15 / 100); // 15% of chart width
            var wickYFreq = new int[grid.Height];
            var borderYFreq = new int[grid.Height];
            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= rightEdge; x++)
                {
                    if (indicatorMask[x, y]) continue; // already masked by run-based
                    var (elem, _) = grid.ClassifyHts(x, y);
                    if (elem == HtsElement.Wick) wickYFreq[y]++;
                    if (elem == HtsElement.Border && !borderMask[x, y]) borderYFreq[y]++;
                }
            }
            for (int y = top; y <= bottom; y++)
            {
                if (wickYFreq[y] >= freqThreshold)
                {
                    for (int x = left; x <= rightEdge; x++)
                        indicatorMask[x, y] = true;
                }
                if (borderYFreq[y] >= freqThreshold)
                {
                    for (int x = left; x <= rightEdge; x++)
                        borderMask[x, y] = true;
                }
            }
        }

        // Phase 2: Build per-column HTS profile using indicator mask
        var profiles = new List<HtsColumnProfile>();
        for (int x = left; x <= rightEdge; x++)
        {
            var p = BuildHtsColumnProfileWithMask(grid, x, top, bottom, indicatorMask, borderMask);
            profiles.Add(p);
        }

        if (profiles.Count == 0) return new List<RawCandle>();

        // Phase 3: Detect ultra-dense mode and perform segmentation
        // Ultra-dense: >50% of columns have any candle element (fill/border/wick).
        // In ultra-dense, every column with any HTS element IS a candle.
        // "Candles don't disappear in the middle — each has equal deltaX spacing."
        int elementCols = 0;
        int wickCols = 0;
        for (int idx = 0; idx < profiles.Count; idx++)
        {
            if (profiles[idx].HasWick || profiles[idx].HasFill || profiles[idx].HasBorder)
                elementCols++;
            if (profiles[idx].HasWick)
                wickCols++;
        }
        bool ultraDense = elementCols > profiles.Count * 70 / 100;

        // DEBUG: dump Y-distribution of wick bottom values
        if (expectedCandleCount > 0)
        {
            var wickBottoms = profiles.Where(p => p.HasWick).Select(p => p.WickBottomY).ToList();
            var wickTops = profiles.Where(p => p.HasWick).Select(p => p.WickTopY).ToList();
            var borderBottoms = profiles.Where(p => p.HasBorder).Select(p => p.BorderBottomY).ToList();
            var borderTops = profiles.Where(p => p.HasBorder).Select(p => p.BorderTopY).ToList();
            if (wickBottoms.Count > 0)
            {
                var wbGroups = wickBottoms.GroupBy(y => y).OrderByDescending(g => g.Count()).Take(5);
                Console.Error.WriteLine($"  [DX-DBG] Wick bottoms (top5): {string.Join(", ", wbGroups.Select(g => $"Y={g.Key}×{g.Count()}"))}");
                var wtGroups = wickTops.GroupBy(y => y).OrderByDescending(g => g.Count()).Take(5);
                Console.Error.WriteLine($"  [DX-DBG] Wick tops (top5): {string.Join(", ", wtGroups.Select(g => $"Y={g.Key}×{g.Count()}"))}");
            }
            if (borderBottoms.Count > 0)
            {
                var bbGroups = borderBottoms.GroupBy(y => y).OrderByDescending(g => g.Count()).Take(5);
                Console.Error.WriteLine($"  [DX-DBG] Border bottoms (top5): {string.Join(", ", bbGroups.Select(g => $"Y={g.Key}×{g.Count()}"))}");
                var btGroups = borderTops.GroupBy(y => y).OrderByDescending(g => g.Count()).Take(5);
                Console.Error.WriteLine($"  [DX-DBG] Border tops (top5): {string.Join(", ", btGroups.Select(g => $"Y={g.Key}×{g.Count()}"))}");
            }
            // Check MaxWickRun distribution
            var mwrDist = profiles.Where(p => p.HasWick).GroupBy(p => p.MaxWickRun)
                .OrderBy(g => g.Key).Select(g => $"run{g.Key}={g.Count()}").ToList();
            Console.Error.WriteLine($"  [DX-DBG] MaxWickRun dist: {string.Join(", ", mwrDist)}");
            // Wick pixel counts
            var wickCounts = profiles.Where(p => p.HasWick).Select(p => p.WickCount).ToList();
            Console.Error.WriteLine($"  [DX-DBG] WickCount: min={wickCounts.Min()} max={wickCounts.Max()} avg={wickCounts.Average():F1}");
        }

        // ── DeltaX-based candle placement ──
        // When expectedCandleCount is known (from OCR: "600/600", "900" etc.),
        // use equal-spacing to assign exact pixel columns to each candle.
        //   deltaX = pixelSpan / candleCount  (fractional!)
        //   candle[i].centerX = startX + i * deltaX + deltaX/2
        // Each candle owns a range of columns: [floor(i*dX), floor((i+1)*dX))
        // Collect wick/border/fill from ALL columns in that range → best H/L/O/C.
        //
        // Why this is better than "every column = 1 candle":
        //   - Exactly N candles (matches actual trading day count)
        //   - Float-width candles: some get 1px, some get 2px — mirrors HTS rendering
        //   - No duplicate candles from float→int boundary pixels
        if (ultraDense && expectedCandleCount > 0 && profiles.Count > 0)
        {
            // Find first and last columns with any HTS element
            int firstCol = 0, lastCol = profiles.Count - 1;
            while (firstCol < profiles.Count &&
                   !profiles[firstCol].HasWick && !profiles[firstCol].HasFill && !profiles[firstCol].HasBorder)
                firstCol++;
            while (lastCol > firstCol &&
                   !profiles[lastCol].HasWick && !profiles[lastCol].HasFill && !profiles[lastCol].HasBorder)
                lastCol--;

            int pixelSpan = lastCol - firstCol + 1;
            double deltaX = (double)pixelSpan / expectedCandleCount;

            var candles = new List<RawCandle>();
            for (int ci = 0; ci < expectedCandleCount; ci++)
            {
                int colStart = firstCol + (int)(ci * deltaX);
                int colEnd = firstCol + (int)((ci + 1) * deltaX) - 1;
                if (colEnd < colStart) colEnd = colStart;
                if (colEnd >= profiles.Count) colEnd = profiles.Count - 1;
                if (colStart >= profiles.Count) break;

                // Gather elements from all columns in this candle's range
                int wickMinY = int.MaxValue, wickMaxY = int.MinValue;
                int borderMinY = int.MaxValue, borderMaxY = int.MinValue;
                int redWick = 0, blueWick = 0;
                int redBorder = 0, blueBorder = 0;
                int redFill = 0, blueFill = 0;
                bool hasWick = false, hasBorder = false;

                // In ultra-dense, filter out noise from indicator overlays:
                // - Wick: only trust columns with MaxWickRun >= 2 (real candle wicks
                //   are vertically continuous; indicator dots are isolated pixels)
                // - Border: only trust columns where border span is < 50% of chart height
                //   (real candle bodies are small; 매물대 bands span the entire chart)
                int maxBorderSpan = (bottom - top) / 2; // 50% of chart height

                for (int col = colStart; col <= colEnd; col++)
                {
                    var p = profiles[col];
                    // Use BestWickRun (longest contiguous wick segment) instead of
                    // full WickTopY/BottomY. This filters out isolated indicator pixels
                    // that survived masking. Real candle wicks are vertically continuous.
                    if (p.HasWick && p.MaxWickRun >= 2)
                    {
                        hasWick = true;
                        if (p.BestWickRunTopY < wickMinY) wickMinY = p.BestWickRunTopY;
                        if (p.BestWickRunBottomY > wickMaxY) wickMaxY = p.BestWickRunBottomY;
                        if (p.WickColor == CandleColor.Red) redWick += p.WickCount;
                        else blueWick += p.WickCount;
                    }
                    // Fallback: if no wick run >= 2, still use wick (1px runs)
                    // to avoid empty candles. Filter downstream with body clamping.
                    else if (p.HasWick && !hasWick)
                    {
                        hasWick = true;
                        if (p.WickTopY < wickMinY) wickMinY = p.WickTopY;
                        if (p.WickBottomY > wickMaxY) wickMaxY = p.WickBottomY;
                        if (p.WickColor == CandleColor.Red) redWick += p.WickCount;
                        else blueWick += p.WickCount;
                    }
                    if (p.HasBorder)
                    {
                        int borderSpan = p.BorderBottomY - p.BorderTopY;
                        if (borderSpan < maxBorderSpan)
                        {
                            hasBorder = true;
                            if (p.BorderTopY < borderMinY) borderMinY = p.BorderTopY;
                            if (p.BorderBottomY > borderMaxY) borderMaxY = p.BorderBottomY;
                            if (p.BorderColor == CandleColor.Red) redBorder += p.BorderCount;
                            else blueBorder += p.BorderCount;
                        }
                    }
                    if (p.HasFill)
                    {
                        if (p.FillColor == CandleColor.Red) redFill += p.FillCount;
                        else blueFill += p.FillCount;
                    }
                }

                // Color: prefer wick, then border, then fill
                CandleColor color;
                if (hasWick)
                    color = redWick >= blueWick ? CandleColor.Red : CandleColor.Blue;
                else if (hasBorder)
                    color = redBorder >= blueBorder ? CandleColor.Red : CandleColor.Blue;
                else
                    color = redFill >= blueFill ? CandleColor.Red : CandleColor.Blue;

                // H/L from wick, fallback to border.
                // For body estimation, prefer border range (it represents the actual
                // candle body). Then clamp wick to be within reasonable distance of body.
                // This filters out distant indicator noise that survived masking.
                int wTop, wBot;
                int bodyTop, bodyBot;

                if (hasBorder && hasWick)
                {
                    // Best case: both border and wick present
                    bodyTop = borderMinY;
                    bodyBot = borderMaxY;
                    // Clamp wick to body ± reasonable wick extension.
                    // Real wick extends from body edges. Max wick length = 3× body height
                    // (very generous). Anything further is noise.
                    int bodyH = Math.Max(1, bodyBot - bodyTop);
                    int maxWickExt = Math.Max(bodyH * 3, 15); // at least 15px extension
                    wTop = Math.Max(wickMinY, bodyTop - maxWickExt);
                    wBot = Math.Min(wickMaxY, bodyBot + maxWickExt);
                }
                else if (hasWick && !hasBorder)
                {
                    // Wick only (thin candle, no visible body): use wick as-is
                    wTop = wickMinY;
                    wBot = wickMaxY;
                    int wickH = wBot - wTop;
                    int mid = (wTop + wBot) / 2;
                    int bodyHalf = Math.Max(1, wickH * 2 / 10);
                    bodyTop = mid - bodyHalf;
                    bodyBot = mid + bodyHalf;
                }
                else if (hasBorder)
                {
                    // Border only (no wick): use border as entire candle
                    wTop = borderMinY;
                    wBot = borderMaxY;
                    bodyTop = borderMinY;
                    bodyBot = borderMaxY;
                }
                else
                {
                    // No wick or border — skip this candle slot
                    wTop = (top + bottom) / 2;
                    wBot = wTop;
                    bodyTop = wTop;
                    bodyBot = wBot;
                }

                int centerX = profiles[(colStart + colEnd) / 2].X;
                candles.Add(new RawCandle
                {
                    CenterX = centerX,
                    Width = colEnd - colStart + 1,
                    WickTopY = wTop,
                    WickBottomY = wBot,
                    BodyTopY = bodyTop,
                    BodyBottomY = bodyBot,
                    Color = color
                });
            }

            return candles;
        }

        var candleList = new List<RawCandle>();
        int i = 0;
        while (i < profiles.Count)
        {
            if (ultraDense)
            {
                // Ultra-dense without expectedCandleCount:
                // every column with any HTS element = 1 candle.
                var p = profiles[i];
                if (!p.HasWick && !p.HasFill && !p.HasBorder)
                {
                    i++;
                    continue;
                }

                CandleColor color;
                if (p.HasWick)
                    color = p.WickColor;
                else if (p.HasBorder)
                    color = p.BorderColor;
                else
                    color = p.FillColor;

                int wTop, wBot;
                if (p.HasWick)
                {
                    wTop = p.WickTopY;
                    wBot = p.WickBottomY;
                }
                else if (p.HasBorder)
                {
                    wTop = p.BorderTopY;
                    wBot = p.BorderBottomY;
                }
                else
                {
                    wTop = p.FillTopY;
                    wBot = p.FillBottomY;
                }

                int bodyTop, bodyBot;
                bool borderIsBody = p.HasBorder && p.HasWick &&
                    p.BorderTopY >= wTop && p.BorderBottomY <= wBot;

                if (borderIsBody)
                {
                    bodyTop = p.BorderTopY;
                    bodyBot = p.BorderBottomY;
                }
                else
                {
                    int wickH = wBot - wTop;
                    int mid = (wTop + wBot) / 2;
                    int bodyHalf = Math.Max(1, wickH * 2 / 10);
                    bodyTop = mid - bodyHalf;
                    bodyBot = mid + bodyHalf;
                }

                candleList.Add(new RawCandle
                {
                    CenterX = p.X,
                    Width = 1,
                    WickTopY = wTop,
                    WickBottomY = wBot,
                    BodyTopY = bodyTop,
                    BodyBottomY = bodyBot,
                    Color = color
                });

                i++;
            }
            else
            {
                // Normal density: wick-based segmentation with adjacent merging
                if (!profiles[i].HasWick)
                {
                    i++;
                    continue;
                }

                // Require stronger wick evidence for non-dense charts
                if (profiles[i].MaxWickRun < 2 && !profiles[i].HasBorder)
                {
                    i++;
                    continue;
                }

                CandleColor wickColor = profiles[i].WickColor;
                int segStart = i;
                int segEnd = i;

                // Extend: adjacent columns with same wick color = same candle
                for (int j = i + 1; j < profiles.Count; j++)
                {
                    var pj = profiles[j];

                    if (pj.HasWick && pj.WickColor == wickColor &&
                        (pj.MaxWickRun >= 2 || pj.HasBorder))
                    {
                        segEnd = j;
                        continue;
                    }

                    // Body interior column (no wick but has fill/border)
                    if (!pj.HasWick && (pj.HasBorder || pj.HasFill))
                    {
                        if (j + 1 < profiles.Count && profiles[j + 1].HasWick &&
                            profiles[j + 1].WickColor == wickColor)
                        {
                            segEnd = j;
                            continue;
                        }
                    }

                    break;
                }

                // Gather Y extents from segment
                int fillMinY = int.MaxValue, fillMaxY = int.MinValue;
                int borderMinY = int.MaxValue, borderMaxY = int.MinValue;
                int wickMinY = int.MaxValue, wickMaxY = int.MinValue;

                for (int j = segStart; j <= segEnd; j++)
                {
                    var pj = profiles[j];
                    if (pj.HasWick)
                    {
                        if (pj.WickTopY < wickMinY) wickMinY = pj.WickTopY;
                        if (pj.WickBottomY > wickMaxY) wickMaxY = pj.WickBottomY;
                    }
                    if (pj.HasFill)
                    {
                        if (pj.FillTopY < fillMinY) fillMinY = pj.FillTopY;
                        if (pj.FillBottomY > fillMaxY) fillMaxY = pj.FillBottomY;
                    }
                    if (pj.HasBorder)
                    {
                        if (pj.BorderTopY < borderMinY) borderMinY = pj.BorderTopY;
                        if (pj.BorderBottomY > borderMaxY) borderMaxY = pj.BorderBottomY;
                    }
                }

                // Body extent (normal mode: use fill/border)
                int bodyTop, bodyBot;
                if (fillMinY != int.MaxValue)
                {
                    bodyTop = fillMinY;
                    bodyBot = fillMaxY;
                    if (borderMinY != int.MaxValue)
                    {
                        bodyTop = Math.Min(bodyTop, borderMinY);
                        bodyBot = Math.Max(bodyBot, borderMaxY);
                    }
                }
                else if (borderMinY != int.MaxValue)
                {
                    bodyTop = borderMinY;
                    bodyBot = borderMaxY;
                }
                else
                {
                    int wickH = wickMaxY - wickMinY;
                    int mid = (wickMinY + wickMaxY) / 2;
                    int bodyHalf = Math.Max(1, wickH * 2 / 10);
                    bodyTop = mid - bodyHalf;
                    bodyBot = mid + bodyHalf;
                }

                // Wick extent = wick pixel range (clamped)
                int wTop = wickMinY;
                int wBot = wickMaxY;
                int bodyH = Math.Max(1, bodyBot - bodyTop + 1);
                int maxWickExt = Math.Max(bodyH * 5, 30);
                wTop = Math.Max(wTop, bodyTop - maxWickExt);
                wBot = Math.Min(wBot, bodyBot + maxWickExt);

                int segWidth = segEnd - segStart + 1;
                int centerX = profiles[(segStart + segEnd) / 2].X;

                candleList.Add(new RawCandle
                {
                    CenterX = centerX,
                    Width = segWidth,
                    WickTopY = wTop,
                    WickBottomY = wBot,
                    BodyTopY = bodyTop,
                    BodyBottomY = bodyBot,
                    Color = wickColor
                });

                i = segEnd + 1;
            }
        }

        // Post-filter: remove spanning artifacts (indicator lines classified as wick)
        // In ultra-dense mode, skip aggressive filtering — every column IS a candle.
        // The horizontal-run indicator mask already handles MA lines.
        if (!ultraDense)
        {
            if (candleList.Count > 5)
            {
                int heightThreshold = (int)(chartHeight * 0.60);
                candleList = candleList.Where(c => (c.WickBottomY - c.WickTopY) < heightThreshold).ToList();
            }

            if (candleList.Count >= 5)
                candleList = FilterTextOverlayOutliers(candleList, top, bottom);
        }

        return candleList;
    }

    /// <summary>
    /// Per-column HTS element profile.
    /// </summary>
    private struct HtsColumnProfile
    {
        public int X;
        public bool HasFill, HasBorder, HasWick, HasOutline;
        public CandleColor FillColor, BorderColor, WickColor;
        public int FillTopY, FillBottomY;
        public int BorderTopY, BorderBottomY;
        public int WickTopY, WickBottomY;
        public int AnyMinY, AnyMaxY;  // extent of any candle element
        public int FillCount, WickCount, BorderCount, OutlineCount;
        public int MaxWickRun;  // longest contiguous wick pixel run (filters indicator lines)
        public int BestWickRunTopY;   // start Y of longest contiguous wick run
        public int BestWickRunBottomY; // end Y of longest contiguous wick run
    }

    private static HtsColumnProfile BuildHtsColumnProfile(
        PixelGrid grid, int x, int top, int bottom, HashSet<int>? indicatorYs = null)
    {
        var p = new HtsColumnProfile
        {
            X = x,
            FillTopY = int.MaxValue, FillBottomY = int.MinValue,
            BorderTopY = int.MaxValue, BorderBottomY = int.MinValue,
            WickTopY = int.MaxValue, WickBottomY = int.MinValue,
            AnyMinY = int.MaxValue, AnyMaxY = int.MinValue,
        };

        int redFill = 0, blueFill = 0;
        int redBorder = 0, blueBorder = 0;
        int redWick = 0, blueWick = 0;
        int wickRun = 0;   // current contiguous wick pixel run
        int wickRunStartY = 0; // start Y of current wick run
        int prevY = -2;    // previous Y with wick pixel

        for (int y = top; y <= bottom; y++)
        {
            var (elem, color) = grid.ClassifyHts(x, y);
            if (elem == HtsElement.None) continue;

            // Skip wick pixels at indicator line Y levels
            if (elem == HtsElement.Wick && indicatorYs != null && indicatorYs.Contains(y))
                continue;

            if (y < p.AnyMinY) p.AnyMinY = y;
            if (y > p.AnyMaxY) p.AnyMaxY = y;

            switch (elem)
            {
                case HtsElement.Fill:
                    if (color == CandleColor.Red) redFill++;
                    else if (color == CandleColor.Blue) blueFill++;
                    if (y < p.FillTopY) p.FillTopY = y;
                    if (y > p.FillBottomY) p.FillBottomY = y;
                    p.FillCount++;
                    break;
                case HtsElement.Border:
                    if (color == CandleColor.Red) redBorder++;
                    else if (color == CandleColor.Blue) blueBorder++;
                    if (y < p.BorderTopY) p.BorderTopY = y;
                    if (y > p.BorderBottomY) p.BorderBottomY = y;
                    p.BorderCount++;
                    break;
                case HtsElement.Wick:
                    if (color == CandleColor.Red) redWick++;
                    else if (color == CandleColor.Blue) blueWick++;
                    if (y < p.WickTopY) p.WickTopY = y;
                    if (y > p.WickBottomY) p.WickBottomY = y;
                    p.WickCount++;
                    // Track longest contiguous wick run and its Y range
                    if (y == prevY + 1)
                    {
                        wickRun++;
                    }
                    else
                    {
                        wickRun = 1;
                        wickRunStartY = y;
                    }
                    if (wickRun > p.MaxWickRun)
                    {
                        p.MaxWickRun = wickRun;
                        p.BestWickRunTopY = wickRunStartY;
                        p.BestWickRunBottomY = y;
                    }
                    prevY = y;
                    break;
                case HtsElement.Outline:
                    p.OutlineCount++;
                    break;
            }
        }

        p.HasFill = p.FillCount > 0;
        p.HasBorder = p.BorderCount > 0;
        p.HasWick = p.WickCount > 0;
        p.HasOutline = p.OutlineCount > 0;

        // Dominant color for each element
        p.FillColor = redFill >= blueFill ? CandleColor.Red : CandleColor.Blue;
        p.BorderColor = redBorder >= blueBorder ? CandleColor.Red : CandleColor.Blue;
        p.WickColor = redWick >= blueWick ? CandleColor.Red : CandleColor.Blue;

        return p;
    }

    /// <summary>
    /// Build column profile using a per-pixel indicator mask instead of Y-level sets.
    /// The mask marks individual wick pixels that belong to horizontal runs ≥ N
    /// (moving average lines), allowing finer-grained indicator filtering that
    /// preserves real candle wicks even at densely-populated Y levels.
    /// </summary>
    private static HtsColumnProfile BuildHtsColumnProfileWithMask(
        PixelGrid grid, int x, int top, int bottom, bool[,] indicatorMask, bool[,] borderMask)
    {
        var p = new HtsColumnProfile
        {
            X = x,
            FillTopY = int.MaxValue, FillBottomY = int.MinValue,
            BorderTopY = int.MaxValue, BorderBottomY = int.MinValue,
            WickTopY = int.MaxValue, WickBottomY = int.MinValue,
            AnyMinY = int.MaxValue, AnyMaxY = int.MinValue,
        };

        int redFill = 0, blueFill = 0;
        int redBorder = 0, blueBorder = 0;
        int redWick = 0, blueWick = 0;
        int wickRun = 0;
        int prevY = -2;

        for (int y = top; y <= bottom; y++)
        {
            var (elem, color) = grid.ClassifyHts(x, y);
            if (elem == HtsElement.None) continue;

            // Skip wick pixels masked as indicator (part of horizontal run ≥ N)
            if (elem == HtsElement.Wick && indicatorMask[x, y])
                continue;
            // Skip border pixels masked as 매물대 band (part of horizontal run ≥ N)
            if (elem == HtsElement.Border && borderMask[x, y])
                continue;

            if (y < p.AnyMinY) p.AnyMinY = y;
            if (y > p.AnyMaxY) p.AnyMaxY = y;

            switch (elem)
            {
                case HtsElement.Fill:
                    if (color == CandleColor.Red) redFill++;
                    else if (color == CandleColor.Blue) blueFill++;
                    if (y < p.FillTopY) p.FillTopY = y;
                    if (y > p.FillBottomY) p.FillBottomY = y;
                    p.FillCount++;
                    break;
                case HtsElement.Border:
                    if (color == CandleColor.Red) redBorder++;
                    else if (color == CandleColor.Blue) blueBorder++;
                    if (y < p.BorderTopY) p.BorderTopY = y;
                    if (y > p.BorderBottomY) p.BorderBottomY = y;
                    p.BorderCount++;
                    break;
                case HtsElement.Wick:
                    if (color == CandleColor.Red) redWick++;
                    else if (color == CandleColor.Blue) blueWick++;
                    if (y < p.WickTopY) p.WickTopY = y;
                    if (y > p.WickBottomY) p.WickBottomY = y;
                    p.WickCount++;
                    wickRun = (y == prevY + 1) ? wickRun + 1 : 1;
                    if (wickRun > p.MaxWickRun) p.MaxWickRun = wickRun;
                    prevY = y;
                    break;
                case HtsElement.Outline:
                    p.OutlineCount++;
                    break;
            }
        }

        p.HasFill = p.FillCount > 0;
        p.HasBorder = p.BorderCount > 0;
        p.HasWick = p.WickCount > 0;
        p.HasOutline = p.OutlineCount > 0;

        p.FillColor = redFill >= blueFill ? CandleColor.Red : CandleColor.Blue;
        p.BorderColor = redBorder >= blueBorder ? CandleColor.Red : CandleColor.Blue;
        p.WickColor = redWick >= blueWick ? CandleColor.Red : CandleColor.Blue;

        return p;
    }

    /// <summary>
    /// Estimate candle body within a wick range by finding the densest vertical cluster.
    /// For thin candles: scan the center column, find the longest continuous colored run.
    /// </summary>
    private static (int bodyTop, int bodyBot) EstimateBody(
        PixelGrid grid, int centerX, int wickTop, int wickBot, CandleColor color)
    {
        // Find continuous colored runs at center X
        int bestRunStart = wickTop, bestRunEnd = wickTop;
        int curRunStart = -1, curRunLen = 0;

        for (int y = wickTop; y <= wickBot; y++)
        {
            bool match = color switch
            {
                CandleColor.Red => grid.IsReddish(centerX, y),
                CandleColor.Blue => grid.IsBluish(centerX, y),
                CandleColor.Green => grid.IsGreenish(centerX, y),
                _ => false
            };

            if (match)
            {
                if (curRunStart < 0) curRunStart = y;
                curRunLen++;
            }
            else
            {
                if (curRunLen > bestRunEnd - bestRunStart + 1)
                {
                    bestRunStart = curRunStart;
                    bestRunEnd = curRunStart + curRunLen - 1;
                }
                curRunStart = -1;
                curRunLen = 0;
            }
        }
        // Check last run
        if (curRunLen > bestRunEnd - bestRunStart + 1 && curRunStart >= 0)
        {
            bestRunStart = curRunStart;
            bestRunEnd = curRunStart + curRunLen - 1;
        }

        return (bestRunStart, bestRunEnd);
    }

    /// <summary>
    /// Per-column colored pixel profile used by column-scan.
    /// </summary>
    private struct ColumnProfile
    {
        public int X;
        public CandleColor Color;
        public int TopY;
        public int BottomY;
        public int PixelCount;
    }

    /// <summary>
    /// Remove text overlay outliers from candle list.
    ///
    /// ── Design Intent: Text Overlay Noise ──
    /// HTS charts have colored text overlays that get detected as candle bodies:
    ///   - "최고 48,700(02/13)" (peak price annotation) — red text near chart top
    ///   - "6,253,426(1.3%)" (volume/percent annotations) — scattered colored text
    ///   - Price event labels: "배당락", "권리락" etc.
    ///
    /// Detection strategy:
    ///   1. Calculate rolling median of body center Y for neighboring candles (window=7)
    ///   2. If a candle's body center Y differs from local median by > 40% of chart height,
    ///      it's likely a text overlay (real candles don't jump that far between adjacent bars)
    ///   3. Also filter candles whose body is entirely in the top 15% of chart AND
    ///      disconnected from neighbors (isolated high-Y outlier)
    /// ──
    /// </summary>
    private static List<RawCandle> FilterTextOverlayOutliers(
        List<RawCandle> candles, int chartTop, int chartBottom)
    {
        if (candles.Count < 5) return candles;

        int chartHeight = chartBottom - chartTop;
        if (chartHeight <= 0) return candles;

        // Calculate body center Y for each candle
        var bodyCenterY = candles.Select(c => (c.BodyTopY + c.BodyBottomY) / 2).ToArray();

        var keep = new bool[candles.Count];
        int windowHalf = 3; // look at 3 neighbors each side (window=7)

        for (int i = 0; i < candles.Count; i++)
        {
            // Collect neighbor body center Y values (excluding self)
            var neighborYs = new List<int>();
            for (int j = Math.Max(0, i - windowHalf); j <= Math.Min(candles.Count - 1, i + windowHalf); j++)
            {
                if (j != i) neighborYs.Add(bodyCenterY[j]);
            }

            if (neighborYs.Count < 2)
            {
                keep[i] = true; // not enough neighbors to judge
                continue;
            }

            neighborYs.Sort();
            double median = neighborYs[neighborYs.Count / 2];

            double diff = Math.Abs(bodyCenterY[i] - median);
            double diffRatio = diff / chartHeight;

            // Outlier: body center Y differs from local median by > 45% of chart height.
            // Real candles can have large price jumps (gap-up/down), but even a 2x price
            // movement rarely exceeds 40% of chart height. Text overlays typically jump
            // from mid-chart to very top or very bottom (>50% difference).
            if (diffRatio > 0.45)
            {
                keep[i] = false;
                continue;
            }

            // Additional check: if body is entirely in the top 10% of chart area
            // AND it's significantly above its neighbors, likely a top-edge text annotation
            // (e.g. "최고 48,700(02/13)" printed in red at the top)
            double topThreshold = chartTop + chartHeight * 0.10;
            if (candles[i].BodyBottomY < topThreshold && bodyCenterY[i] < median - chartHeight * 0.30)
            {
                keep[i] = false;
                continue;
            }

            keep[i] = true;
        }

        var result = new List<RawCandle>();
        for (int i = 0; i < candles.Count; i++)
        {
            if (keep[i]) result.Add(candles[i]);
        }

        return result;
    }

    // ── Step 5 helper: Color Convention Detection ─────────────

    /// <summary>
    /// Auto-detect bullish color convention.
    /// Korean: Red=Up, Blue=Down. US: Green=Up, Red=Down.
    /// Only consider candles with reasonable body size (width ≥ 3, height ≥ 3)
    /// to avoid being fooled by indicator/text remnants.
    /// </summary>
    private static string DetectColorConvention(List<RawCandle> candles)
    {
        // Filter to only "real-looking" candles (body-size threshold)
        var significant = candles.Where(c =>
            c.Width >= 3 && (c.BodyBottomY - c.BodyTopY) >= 3).ToList();

        if (significant.Count == 0) significant = candles; // fallback

        int greenCount = significant.Count(c => c.Color == CandleColor.Green);
        int redCount = significant.Count(c => c.Color == CandleColor.Red);
        int blueCount = significant.Count(c => c.Color == CandleColor.Blue);

        // US convention: green candles must be a significant portion
        // (at least 30% of total and more than just a few)
        int total = greenCount + redCount + blueCount;
        if (total > 0 && greenCount > 5 && greenCount > total * 0.25)
            return "green";

        // Default: Korean convention (red=bullish, blue=bearish)
        return "red";
    }

    // ── Step 6: Volume Panel Detection ────────────────────────

    /// <summary>
    /// Detect volume sub-panel by finding a horizontal gap (empty row)
    /// between main chart and volume area.
    /// </summary>
    private static int? DetectVolumePanelTop(
        PixelGrid grid, int chartLeft, int chartRight, int chartBottom, int imageHeight)
    {
        // Scan downward from chartBottom area looking for a row of mostly bright pixels
        // followed by colored (volume bar) rows
        int scanStart = Math.Max(0, chartBottom - 30);
        int scanEnd = Math.Min(imageHeight - 1, chartBottom + 80);
        int width = chartRight - chartLeft;

        for (int y = scanStart; y < scanEnd; y++)
        {
            // Check if this row is mostly bright (separator)
            int brightCount = 0;
            for (int x = chartLeft; x < chartRight; x += 3)
            {
                if (grid.IsBright(x, y)) brightCount++;
            }

            bool isEmptyRow = brightCount > width / 4; // >75% of sampled pixels bright

            if (isEmptyRow)
            {
                // Check if rows below have colored pixels (volume bars)
                int checkY = Math.Min(y + 5, imageHeight - 1);
                int coloredBelow = 0;
                for (int x = chartLeft; x < chartRight; x += 5)
                {
                    if (grid.IsReddish(x, checkY) || grid.IsBluish(x, checkY) || grid.IsGreenish(x, checkY))
                        coloredBelow++;
                }

                if (coloredBelow > 3)
                    return y;
            }
        }

        return null;
    }

    /// <summary>
    /// Extract volume bar heights for each candle by scanning the volume panel.
    ///
    /// ── Strategy ──
    /// Volume bars share the same X position as their candle. For each candle:
    ///   1. Scan the column at candle.PixelX in the volume panel (volumeTop ~ volumeBottom)
    ///   2. Find the topmost colored pixel = bar top Y
    ///   3. Bar height = volumeBottom - barTopY (taller bar = more volume)
    ///   4. Relative volume = barHeight / maxBarHeight (0.0 ~ 1.0)
    ///
    /// Volume bars in HTS use the same red/blue color convention as candles.
    /// We also check HTS-specific fill/border colors for accuracy.
    ///
    /// Returns: Dictionary of candleIndex → barTopY pixel coordinate.
    /// The caller converts barTopY to absolute volume using tooltip calibration.
    /// If no tooltip calibration, relative volume (0.0~1.0) is used as a ratio.
    /// </summary>
    internal static Dictionary<int, int> ExtractVolumeBars(
        PixelGrid grid, List<CandleData> candles,
        int volumeTop, int volumeBottom, int chartLeft, int chartRight)
    {
        var barTops = new Dictionary<int, int>(); // candle index → barTopY

        // Volume panel effective area: skip separator rows near volumeTop
        int panelTop = volumeTop + 3;
        int panelBottom = volumeBottom;

        if (panelBottom <= panelTop || panelBottom - panelTop < 5)
            return barTops;

        for (int i = 0; i < candles.Count; i++)
        {
            int cx = candles[i].PixelX;
            if (cx < chartLeft || cx >= chartRight) continue;

            // Scan column from panelTop downward to find topmost colored pixel (= bar top)
            // Volume bars are drawn from bottom up, so the first colored pixel from top = bar top.
            // Also scan ±1 pixel for narrow bars that may be offset by 1px.
            int barTopY = -1;
            for (int y = panelTop; y < panelBottom; y++)
            {
                // Check center and ±1 pixel columns for bar presence
                if (IsVolumeBarPixel(grid, cx, y) ||
                    IsVolumeBarPixel(grid, cx - 1, y) ||
                    IsVolumeBarPixel(grid, cx + 1, y))
                {
                    barTopY = y;
                    break;
                }
            }

            if (barTopY > 0)
                barTops[i] = barTopY;
        }

        return barTops;
    }

    /// <summary>
    /// Check if a pixel belongs to a volume bar (colored: red, blue, green, or HTS fill/border).
    /// </summary>
    private static bool IsVolumeBarPixel(PixelGrid grid, int x, int y)
    {
        if (x < 0 || x >= grid.Width || y < 0 || y >= grid.Height)
            return false;

        // Standard color check (most chart types)
        if (grid.IsReddish(x, y) || grid.IsBluish(x, y) || grid.IsGreenish(x, y))
            return true;

        // HTS-specific: fill and border colors don't pass standard checks
        var (elem, _) = grid.ClassifyHts(x, y);
        if (elem == HtsElement.Fill || elem == HtsElement.Border || elem == HtsElement.Wick)
            return true;

        // Dark non-background pixel (some volume bars use near-black outlines)
        var (r, g, b) = grid.GetPixel(x, y);
        if (r < 80 && g < 80 && b < 80 && !(r > 40 && g > 40 && b > 40 && Math.Max(r, Math.Max(g, b)) - Math.Min(r, Math.Min(g, b)) < 15))
        {
            // Exclude grid lines (uniform gray) — only count if non-uniform
            return false;
        }

        return false;
    }

    // ── Utility Methods ───────────────────────────────────────

    /// <summary>
    /// Linear interpolation: convert pixel coordinate to price using axis calibration.
    /// Y-axis: pixel increases downward, price increases upward (inverse relationship).
    /// </summary>
    internal static double Interpolate(int pixel, List<AxisPoint> axisPoints)
    {
        if (axisPoints.Count == 0) return 0;
        if (axisPoints.Count == 1) return axisPoints[0].Value;

        // Find bracketing points
        for (int i = 0; i < axisPoints.Count - 1; i++)
        {
            var p1 = axisPoints[i];
            var p2 = axisPoints[i + 1];

            if (pixel >= p1.Pixel && pixel <= p2.Pixel)
            {
                // Linear interpolation between p1 and p2
                double t = (double)(pixel - p1.Pixel) / (p2.Pixel - p1.Pixel);
                return p1.Value + t * (p2.Value - p1.Value);
            }
        }

        // Extrapolate beyond range
        if (pixel < axisPoints[0].Pixel)
        {
            var p1 = axisPoints[0];
            var p2 = axisPoints[1];
            double slope = (p2.Value - p1.Value) / (p2.Pixel - p1.Pixel);
            return p1.Value + slope * (pixel - p1.Pixel);
        }
        else
        {
            var p1 = axisPoints[^2];
            var p2 = axisPoints[^1];
            double slope = (p2.Value - p1.Value) / (p2.Pixel - p1.Pixel);
            return p2.Value + slope * (pixel - p2.Pixel);
        }
    }

    /// <summary>
    /// Find nearest X-axis label for a given pixel X coordinate.
    /// </summary>
    private static string? FindNearestXLabel(int pixelX, List<AxisPoint> xAxisPoints)
    {
        if (xAxisPoints.Count == 0) return null;

        AxisPoint? nearest = null;
        int minDist = int.MaxValue;
        foreach (var p in xAxisPoints)
        {
            int dist = Math.Abs(p.Pixel - pixelX);
            if (dist < minDist) { minDist = dist; nearest = p; }
        }

        // Only assign if reasonably close (within 30px)
        return nearest != null && minDist < 30 ? nearest.Label : null;
    }

    /// <summary>
    /// Parse price text from OCR: "47,500" → 47500, "35.400" → 35400, "7320" → 7320.
    /// Handles truncated OCR: "35," → 35000, "25." → 25000 (trailing separator = ×1000).
    ///
    /// ── Design Intent: Why trailing separator detection matters ──
    /// HTS Y-axis price labels are very small. OCR often reads "35,000" as "35," or "25."
    /// because the trailing digits are too faint. If we parse "35," as 35 instead of 35000,
    /// all price calculations are off by 1000×. The trailing comma/period is the clue.
    /// ──
    /// </summary>
    internal static double? ParsePriceText(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;

        // Remove whitespace
        var cleaned = text.Trim();

        // Remove common OCR artifacts
        cleaned = cleaned.Replace(" ", "").Replace("l", "1").Replace("O", "0").Replace("o", "0");

        // Detect trailing separator: "35," or "25." → OCR truncated the thousands part
        // This happens when HTS price labels are small and OCR misses trailing digits
        bool hasTrailingSeparator = false;
        if (cleaned.Length >= 2 && (cleaned[^1] == ',' || cleaned[^1] == '.'))
        {
            hasTrailingSeparator = true;
            cleaned = cleaned[..^1]; // remove trailing separator
        }

        // Try parse as number (handle comma/period as thousands separator)
        // Korean prices: "47,500" or "47500" (comma = thousands)
        // Some charts: "35.400" (period = thousands in European notation)
        if (Regex.IsMatch(cleaned, @"^[\d,.\-]+$"))
        {
            // If has both comma and period, determine which is decimal
            bool hasComma = cleaned.Contains(',');
            bool hasPeriod = cleaned.Contains('.');

            if (hasComma && !hasPeriod)
            {
                // "47,500" — comma is thousands separator
                cleaned = cleaned.Replace(",", "");
            }
            else if (hasPeriod && !hasComma)
            {
                // Could be "35.400" (thousands) or "0.5" (decimal)
                // If digits after period >= 3, treat as thousands separator
                var periodIdx = cleaned.LastIndexOf('.');
                var afterPeriod = cleaned[(periodIdx + 1)..];
                if (afterPeriod.Length >= 3)
                    cleaned = cleaned.Replace(".", ""); // thousands separator
                // else keep as decimal
            }
            else if (hasComma && hasPeriod)
            {
                // "1,234.56" or "1.234,56"
                cleaned = cleaned.Replace(",", "");
            }

            if (double.TryParse(cleaned, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double val))
            {
                if (val <= 0) return null;

                // If trailing separator detected and value is small (likely truncated)
                // "35," → 35 → 35,000; "5." → 5 → 5,000
                if (hasTrailingSeparator && val < 1000)
                    val *= 1000;

                return val;
            }
        }

        return null;
    }

    /// <summary>
    /// Check if text looks like a date/time label.
    /// </summary>
    private static bool IsDateTimeLabel(string text)
    {
        // Date patterns: "02/13", "2026/01", "2025/11"
        if (Regex.IsMatch(text, @"^\d{1,4}[/\-\.]\d{1,2}")) return true;

        // Time patterns: "10", "11:30", "14:00"
        if (Regex.IsMatch(text, @"^\d{1,2}(:\d{2})?$")) return true;

        return false;
    }

    /// <summary>
    /// Crop a region from a bitmap.
    /// </summary>
    private static Bitmap CropRegion(Bitmap source, int x, int y, int w, int h)
    {
        // Clamp to bounds
        x = Math.Max(0, x);
        y = Math.Max(0, y);
        w = Math.Min(w, source.Width - x);
        h = Math.Min(h, source.Height - y);
        if (w <= 0 || h <= 0)
            return new Bitmap(1, 1);

        var rect = new Rectangle(x, y, w, h);
        return source.Clone(rect, source.PixelFormat);
    }

    /// <summary>
    /// Generate debug overlay image showing detected chart regions and candles.
    ///
    /// ── Design Intent: Visual Verification ──
    /// Draws recognized OHLC candles at 50% alpha over the original chart screenshot.
    /// This lets you visually compare "what the analyzer sees" vs "what's actually there".
    ///
    /// Candle rendering:
    ///   - Wick: thin vertical line from High to Low pixel Y
    ///   - Body: filled rectangle from Open to Close pixel Y
    ///   - Bullish (Close > Open): lime green, body bottom=Open, top=Close
    ///   - Bearish (Close < Open): magenta, body top=Open, bottom=Close
    ///   - All at 50% alpha so the original chart is visible underneath
    ///
    /// Also draws: chart region (lime box), volume region (cyan box),
    ///   Y-axis calibration lines (yellow), candle index labels (every 20th).
    /// ──
    /// </summary>
    public static Bitmap GenerateDebugOverlay(Bitmap original, ChartAnalysisResult result)
    {
        var overlay = new Bitmap(original);
        using var g = Graphics.FromImage(overlay);

        // Draw chart region
        using var chartPen = new Pen(Color.Lime, 2);
        g.DrawRectangle(chartPen, result.ChartLeft, result.ChartTop,
            result.ChartRight - result.ChartLeft, result.ChartBottom - result.ChartTop);

        // Draw volume region
        if (result.VolumeTop.HasValue && result.VolumeBottom.HasValue)
        {
            using var volPen = new Pen(Color.Cyan, 1);
            g.DrawRectangle(volPen, result.ChartLeft, result.VolumeTop.Value,
                result.ChartRight - result.ChartLeft, result.VolumeBottom.Value - result.VolumeTop.Value);
        }

        // Draw Y-axis calibration lines
        using var yFont = new Font("Consolas", 7);
        using var calibPen = new Pen(Color.FromArgb(80, 255, 255, 0), 1); // subtle yellow
        foreach (var yp in result.YAxisPoints)
        {
            g.DrawLine(calibPen, result.ChartLeft, yp.Pixel, result.ChartRight, yp.Pixel);
            g.DrawString($"{yp.Value:N0}", yFont, Brushes.Yellow, result.ChartRight + 2, yp.Pixel - 6);
        }

        // ── Draw recognized OHLC candles at 50% alpha ──
        // Bullish: lime green wick + body.  Bearish: magenta wick + body.
        // This visually shows "what the analyzer thinks the candles are"
        // overlaid on the real chart for instant verification.

        int alpha = 128; // 50% alpha
        using var bullWickPen = new Pen(Color.FromArgb(alpha, 0, 255, 0), 1);
        using var bearWickPen = new Pen(Color.FromArgb(alpha, 255, 0, 255), 1);
        using var bullBodyBrush = new SolidBrush(Color.FromArgb(alpha, 0, 255, 0));
        using var bearBodyBrush = new SolidBrush(Color.FromArgb(alpha, 255, 0, 255));

        // Determine candle body width from spacing
        // Ultra-dense: 1px body. Normal: scale with spacing.
        int candleBodyWidth = 1;
        if (result.Candles.Count >= 2)
        {
            var sorted = result.Candles.OrderBy(c => c.PixelX).ToList();
            var gaps = new List<int>();
            for (int ci = 1; ci < sorted.Count && ci < 50; ci++)
                gaps.Add(sorted[ci].PixelX - sorted[ci - 1].PixelX);
            if (gaps.Count > 0)
            {
                gaps.Sort();
                int medianGap = gaps[gaps.Count / 2];
                candleBodyWidth = Math.Max(1, medianGap * 3 / 5); // body = 60% of pitch
            }
        }
        int halfBody = candleBodyWidth / 2;

        int idx = 0;
        foreach (var candle in result.Candles)
        {
            // Convert OHLC prices back to pixel Y coordinates
            int highY = Interpolate_Reverse(candle.High, result.YAxisPoints);
            int lowY = Interpolate_Reverse(candle.Low, result.YAxisPoints);
            int openY = Interpolate_Reverse(candle.Open, result.YAxisPoints);
            int closeY = Interpolate_Reverse(candle.Close, result.YAxisPoints);

            // Clamp to chart region
            highY = Math.Max(result.ChartTop, Math.Min(result.ChartBottom, highY));
            lowY = Math.Max(result.ChartTop, Math.Min(result.ChartBottom, lowY));
            openY = Math.Max(result.ChartTop, Math.Min(result.ChartBottom, openY));
            closeY = Math.Max(result.ChartTop, Math.Min(result.ChartBottom, closeY));

            var wickPen = candle.IsBullish ? bullWickPen : bearWickPen;
            var bodyBrush = candle.IsBullish ? bullBodyBrush : bearBodyBrush;

            int x = candle.PixelX;

            // Draw wick (thin vertical line: High to Low)
            if (highY != lowY)
                g.DrawLine(wickPen, x, highY, x, lowY);

            // Draw body (filled rectangle: Open to Close)
            int bodyTopY = Math.Min(openY, closeY);
            int bodyBotY = Math.Max(openY, closeY);
            int bodyH = Math.Max(1, bodyBotY - bodyTopY);
            g.FillRectangle(bodyBrush, x - halfBody, bodyTopY, candleBodyWidth, bodyH);

            // Draw candle index label (every 20th for readability)
            if (idx % 20 == 0)
                g.DrawString($"{idx}", yFont, Brushes.White, x - 4, result.ChartTop - 10);
            idx++;
        }

        // Draw strategy + count label at top-left
        string strategyLabel = $"{result.DetectionStrategy ?? "unknown"}: {result.Candles.Count} candles";
        using var labelFont = new Font("Consolas", 9, FontStyle.Bold);
        using var labelBg = new SolidBrush(Color.FromArgb(180, 0, 0, 0));
        var labelSize = g.MeasureString(strategyLabel, labelFont);
        g.FillRectangle(labelBg, result.ChartLeft + 4, result.ChartTop + 4, labelSize.Width + 4, labelSize.Height + 2);
        g.DrawString(strategyLabel, labelFont, Brushes.White, result.ChartLeft + 6, result.ChartTop + 5);

        return overlay;
    }

    /// <summary>
    /// Reverse interpolation: price → pixel Y (for debug overlay).
    /// </summary>
    private static int Interpolate_Reverse(double price, List<AxisPoint> axisPoints)
    {
        if (axisPoints.Count < 2) return 0;
        var p1 = axisPoints[0];
        var p2 = axisPoints[^1];
        if (Math.Abs(p2.Value - p1.Value) < 0.001) return p1.Pixel;
        double t = (price - p1.Value) / (p2.Value - p1.Value);
        return (int)(p1.Pixel + t * (p2.Pixel - p1.Pixel));
    }
}

// ── Internal Types ────────────────────────────────────────────

internal enum CandleColor { None, Red, Blue, Green }

/// <summary>
/// HTS candle pixel element type.
/// HTS renders candles with 3 distinct visual elements, each with very different colors:
///   Fill   — light cream (246,234,214) for bullish, light blue (214,226,239) for bearish
///   Border — dark brown (134,104,53) for bullish, dark teal (57,154,156) for bearish
///   Wick   — vivid red (255,0,0) for bullish, vivid blue (17,91,203) for bearish
///   Outline — near-black (57,57,52) used as general candle outline/grid
/// </summary>
internal enum HtsElement { None, Fill, Border, Wick, Outline }

internal sealed class RawCandle
{
    public int CenterX { get; set; }
    public int Width { get; set; }
    public int WickTopY { get; set; }    // min Y (highest price)
    public int WickBottomY { get; set; }  // max Y (lowest price)
    public int BodyTopY { get; set; }     // body min Y
    public int BodyBottomY { get; set; }  // body max Y
    public CandleColor Color { get; set; }
}

/// <summary>
/// Horizontal body segment: a single row of colored pixels that could be part of a candle body.
/// </summary>
internal struct BodySegment
{
    public int X;       // leftmost pixel X
    public int Y;       // row Y
    public int Width;   // pixel width
    public CandleColor Color;
}

/// <summary>
/// Clustered candle body: multiple body segments forming a rectangle.
/// </summary>
internal sealed class CandleBody
{
    public int Left { get; set; }
    public int Right { get; set; }
    public int TopY { get; set; }
    public int BottomY { get; set; }
    public CandleColor Color { get; set; }
    public int RowCount { get; set; }
}

// ── PixelGrid: Fast pixel access via LockBits ────────────────

/// <summary>
/// High-performance pixel access wrapper using LockBits.
/// ~100x faster than Bitmap.GetPixel() for bulk scanning.
/// </summary>
internal sealed class PixelGrid : IDisposable
{
    private readonly byte[] _pixels;
    private readonly int _stride;

    public int Width { get; }
    public int Height { get; }

    public PixelGrid(Bitmap bmp)
    {
        Width = bmp.Width;
        Height = bmp.Height;

        // Copy pixel data to managed byte array (safe, no AccessViolation)
        var data = bmp.LockBits(
            new Rectangle(0, 0, Width, Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);
        try
        {
            _stride = data.Stride;
            _pixels = new byte[_stride * Height];
            Marshal.Copy(data.Scan0, _pixels, 0, _pixels.Length);
        }
        finally
        {
            bmp.UnlockBits(data);
        }
    }

    /// <summary>
    /// Get RGB values at (x, y). Format is BGRA in memory.
    /// </summary>
    public (byte R, byte G, byte B) GetPixel(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return (0, 0, 0);

        int offset = y * _stride + x * 4;
        byte b = _pixels[offset];
        byte g = _pixels[offset + 1];
        byte r = _pixels[offset + 2];
        return (r, g, b);
    }

    /// <summary>Red-dominant pixel (Korean bullish candle: 양봉)</summary>
    public bool IsReddish(int x, int y)
    {
        var (r, g, b) = GetPixel(x, y);
        return r > 140 && r > g * 1.4 && r > b * 1.4 && g < 180 && b < 180;
    }

    /// <summary>Blue-dominant pixel (Korean bearish candle: 음봉)</summary>
    public bool IsBluish(int x, int y)
    {
        var (r, g, b) = GetPixel(x, y);
        return b > 140 && b > r * 1.2 && b > g * 1.1 && r < 180;
    }

    /// <summary>Green-dominant pixel (US bullish candle)</summary>
    public bool IsGreenish(int x, int y)
    {
        var (r, g, b) = GetPixel(x, y);
        return g > 140 && g > r * 1.3 && g > b * 1.3;
    }

    /// <summary>Bright pixel (chart background: white, light gray)</summary>
    public bool IsBright(int x, int y)
    {
        var (r, g, b) = GetPixel(x, y);
        return r > 200 && g > 200 && b > 200;
    }

    // ── HTS Candle Element Classification ────────────────────
    //
    // HTS renders candles with a distinctive multi-element style:
    //   Fill:    Very light tinted background (lum ~90%, sat ~44-64%)
    //            Bullish: warm cream (246,234,214)  R > G > B
    //            Bearish: cool blue  (214,226,239)  B > G > R
    //   Border:  Dark colored outline around body (lum ~37%, sat ~43%)
    //            Bullish: brown (134,104,53)   R > G > B, warm hue
    //            Bearish: teal  (57,154,156)   B ≈ G > R, cool hue
    //   Wick:    Vivid saturated thin line (lum ~47%, sat >85%)
    //            Bullish: red  (255,0,0) or (231,25,9)
    //            Bearish: blue (17,91,203)
    //   Outline: Near-black candle edge (57,57,52) — shared by both
    //
    // None of these pass the existing IsReddish/IsBluish checks (designed for
    // solid-color candles), which is why the old algorithm misses HTS candles.

    /// <summary>
    /// Classify a pixel as an HTS candle element with direction.
    /// Returns (element_type, candle_color) tuple.
    /// </summary>
    public (HtsElement Element, CandleColor Color) ClassifyHts(int x, int y)
    {
        var (r, g, b) = GetPixel(x, y);

        // ── Background / near-white → None ──
        if (r > 225 && g > 225 && b > 225) return (HtsElement.None, CandleColor.None);

        // ── Outline: near-black (57,57,52) — candle edge, shared ──
        // All channels close together, all < 80
        if (r < 80 && g < 80 && b < 80)
        {
            int maxC = Math.Max(r, Math.Max(g, b));
            int minC = Math.Min(r, Math.Min(g, b));
            if (maxC - minC < 20) return (HtsElement.Outline, CandleColor.None);
        }

        // ── Wick: high saturation, medium luminosity ──
        // Red wick: R dominant, very high (R > 200, G < 50, B < 50)
        if (r > 200 && g < 60 && b < 60)
            return (HtsElement.Wick, CandleColor.Red);
        // Blue wick: B dominant (B > 150, R < 80)
        if (b > 150 && r < 80 && g < 120)
            return (HtsElement.Wick, CandleColor.Blue);

        // ── Fill: very light tinted (lum > 80%) ──
        // Bullish fill: warm cream (R > G > B, R > 230, B < 225)
        if (r > 230 && g > 220 && b < 225 && r > b + 15)
            return (HtsElement.Fill, CandleColor.Red);
        // Bearish fill: cool blue (B > R, B > 225, R < 225)
        if (b > 225 && g > 215 && r < 225 && b > r + 15)
            return (HtsElement.Fill, CandleColor.Blue);

        // ── Border: dark colored (lum 30-55%) ──
        // Bullish border: brown/olive (R > G > B, R > 100, B < 80)
        if (r > 100 && g > 70 && b < 80 && r > g && g > b)
            return (HtsElement.Border, CandleColor.Red);
        // Bearish border: teal/cyan (G ≈ B > R, G > 120)
        if (g > 120 && b > 120 && r < 80 && Math.Abs(g - b) < 30)
            return (HtsElement.Border, CandleColor.Blue);

        return (HtsElement.None, CandleColor.None);
    }

    /// <summary>Check if pixel is any HTS candle element (fill, border, wick, outline).</summary>
    public bool IsHtsCandle(int x, int y)
    {
        var (elem, _) = ClassifyHts(x, y);
        return elem != HtsElement.None;
    }

    /// <summary>Check if pixel is any HTS candle element of specified color.</summary>
    public bool IsHtsCandleColor(int x, int y, CandleColor color)
    {
        var (elem, c) = ClassifyHts(x, y);
        if (elem == HtsElement.None) return false;
        if (elem == HtsElement.Outline) return true; // outline is shared
        return c == color;
    }

    public void Dispose()
    {
        // Pixel data is in managed byte array — nothing to release
    }
}

// ── Public Data Models ────────────────────────────────────────

/// <summary>
/// Per-candle OHLC + volume data extracted from chart screenshot.
/// </summary>
public sealed class CandleData
{
    [JsonPropertyName("datetime")]
    public string? DateTime { get; set; }

    [JsonPropertyName("x")]
    public int PixelX { get; set; }

    /// <summary>Pixel Y coordinates for OHLC — needed by TooltipCalibrator to recalibrate prices.</summary>
    [JsonPropertyName("pixelHighY")]
    public int PixelHighY { get; set; }

    [JsonPropertyName("pixelLowY")]
    public int PixelLowY { get; set; }

    [JsonPropertyName("pixelOpenY")]
    public int PixelOpenY { get; set; }

    [JsonPropertyName("pixelCloseY")]
    public int PixelCloseY { get; set; }

    [JsonPropertyName("open")]
    public double Open { get; set; }

    [JsonPropertyName("high")]
    public double High { get; set; }

    [JsonPropertyName("low")]
    public double Low { get; set; }

    [JsonPropertyName("close")]
    public double Close { get; set; }

    [JsonPropertyName("volume")]
    public double? Volume { get; set; }

    /// <summary>Pixel Y of volume bar top — needed for tooltip-based volume calibration.</summary>
    [JsonPropertyName("volumeBarTopY")]
    public int VolumeBarTopY { get; set; }

    [JsonPropertyName("bullish")]
    public bool IsBullish { get; set; }

    public override string ToString() =>
        $"{DateTime ?? $"x={PixelX}"} O={Open:N0} H={High:N0} L={Low:N0} C={Close:N0}" +
        (IsBullish ? " [UP]" : " [DN]") +
        (Volume.HasValue ? $" V={Volume:N0}" : "");
}

/// <summary>
/// Axis calibration point: maps pixel coordinate to a value.
/// </summary>
public sealed class AxisPoint
{
    [JsonPropertyName("pixel")]
    public int Pixel { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = "";
}

/// <summary>
/// Complete chart analysis result with all extracted data.
/// </summary>
public sealed class ChartAnalysisResult
{
    [JsonPropertyName("candles")]
    public List<CandleData> Candles { get; set; } = new();

    [JsonPropertyName("yAxis")]
    public List<AxisPoint> YAxisPoints { get; set; } = new();

    [JsonPropertyName("xAxis")]
    public List<AxisPoint> XAxisPoints { get; set; } = new();

    [JsonPropertyName("chartLeft")]
    public int ChartLeft { get; set; }

    [JsonPropertyName("chartTop")]
    public int ChartTop { get; set; }

    [JsonPropertyName("chartRight")]
    public int ChartRight { get; set; }

    [JsonPropertyName("chartBottom")]
    public int ChartBottom { get; set; }

    [JsonPropertyName("volumeTop")]
    public int? VolumeTop { get; set; }

    [JsonPropertyName("volumeBottom")]
    public int? VolumeBottom { get; set; }

    [JsonPropertyName("bullishColor")]
    public string BullishColor { get; set; } = "red";

    [JsonPropertyName("bearishColor")]
    public string BearishColor { get; set; } = "blue";

    [JsonPropertyName("strategy")]
    public string? DetectionStrategy { get; set; }

    [JsonPropertyName("imageWidth")]
    public int ImageWidth { get; set; }

    [JsonPropertyName("imageHeight")]
    public int ImageHeight { get; set; }

    [JsonIgnore]
    public int CandleCount => Candles.Count;

    [JsonIgnore]
    public double? PriceMin => Candles.Count > 0 ? Candles.Min(c => c.Low) : null;

    [JsonIgnore]
    public double? PriceMax => Candles.Count > 0 ? Candles.Max(c => c.High) : null;

    /// <summary>
    /// Serialize to JSON string (per-candle object array format).
    /// </summary>
    public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    });

    /// <summary>
    /// Serialize to columnar JSON — each field as a 1D array.
    /// Output format:
    /// {
    ///   "count": 600,
    ///   "datetime": ["2023/10/05", ...],
    ///   "open":  [125000, 137700, ...],
    ///   "high":  [130800, 138100, ...],
    ///   "low":   [123500, 132100, ...],
    ///   "close": [130000, 133500, ...],
    ///   "volume": [null, null, ...],
    ///   "bullish": [true, false, ...]
    /// }
    /// Compact and easy to consume from Python/JS: just zip the arrays.
    /// </summary>
    public string ToColumnarJson()
    {
        var obj = new Dictionary<string, object>
        {
            ["count"] = Candles.Count,
            ["datetime"] = Candles.Select(c => (object?)c.DateTime ?? null!).ToArray(),
            ["open"] = Candles.Select(c => Math.Round(c.Open)).ToArray(),
            ["high"] = Candles.Select(c => Math.Round(c.High)).ToArray(),
            ["low"] = Candles.Select(c => Math.Round(c.Low)).ToArray(),
            ["close"] = Candles.Select(c => Math.Round(c.Close)).ToArray(),
            ["volume"] = Candles.Select(c => c.Volume.HasValue ? (object)Math.Round(c.Volume.Value) : null!).ToArray(),
            ["bullish"] = Candles.Select(c => (object)c.IsBullish).ToArray(),
        };
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}
