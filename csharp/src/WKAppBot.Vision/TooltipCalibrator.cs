using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WKAppBot.Vision;

/// <summary>
/// Phase B: Tooltip-based Y-axis recalibration for ChartAnalyzer.
///
/// ── Design Intent ──
/// ChartAnalyzer v11 calibrates Y-axis using OCR on the right-side price labels.
/// Problem: OCR labels only cover a subset of the price range (e.g., 20,000~80,000),
/// so candles outside this range get clamped to priceFloor/priceCeil.
///
/// Solution: Hover mouse over "calibration candles" (those at extreme high/low prices),
/// read the HTS tooltip that appears (tooltips_class32 window), OCR the OHLC values,
/// and use those as additional calibration points to extend the Y-axis mapping.
///
/// ── HTS Tooltip Format (observed) ──
/// Line order is fixed (critical for parsing — OCR may garble label text):
///   일자     : 2026/02/04
///   시간     : 14:51:00         (분봉 only)
///   종목명(코드)
///   시가     : 900,000 (-0.44%)
///   고가     : 900,000 (-0.44%)
///   저가     : 830,000 (-8.19%)
///   종가     : 840,000 (-7.08%)
///   거래량   : 11,353,860 (...)
///   ... (additional indicators)
///
/// Parsing strategy: Find lines with price-like numbers (\d{1,3}(,\d{3})*),
/// use line ORDER (not label text) to identify OHLC, since OCR often garbles
/// Korean labels (e.g., "저가" → "•", "SK하이닉스" → "류하미닉스").
///
/// ── Calibration Flow ──
/// 1. SelectCalibrationCandles: Pick candles with extreme PixelHighY/PixelLowY
/// 2. HoverAndReadTooltip: Move mouse → wait → find visible tooltips_class32 → screenshot → OCR
/// 3. ParseTooltipOhlc: Extract OHLC prices from OCR text
/// 4. RecalibrateAllCandles: Build new Y-axis from tooltip data, re-interpolate all candles
///
/// ── Key Implementation Notes ──
/// - Tooltip hWnd varies per form (not shared). EnumWindows to find visible tooltips_class32.
/// - GetWindowText returns empty for HTS tooltips (owner-drawn). Must use screenshot+OCR.
/// - The tooltip window position changes with mouse movement but content corresponds to
///   the candle nearest to the mouse cursor.
/// - Mouse coordinates are SCREEN coordinates (chart pixel + window offset).
/// - After hovering, we need a small delay (~300ms) for the tooltip to render.
/// - We save/restore cursor position to minimize user disruption.
/// </summary>

/// <summary>
/// Parsed tooltip OHLCV data (immutable record).
/// Volume may be null if not parseable from the tooltip.
/// </summary>
public sealed record TooltipOhlcv(double Open, double High, double Low, double Close, double? Volume);

public sealed class TooltipCalibrator
{
    // Uses NativeMethods from WKAppBot.Win32 (no duplicate P/Invoke needed)
    // Additional import for PrintWindow with PW_RENDERFULLCONTENT
    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);
    private const uint PW_RENDERFULLCONTENT = 0x02;

    // ── Configuration ──

    /// <summary>Delay after mouse move before reading tooltip (ms).</summary>
    public int TooltipWaitMs { get; set; } = 400;

    /// <summary>Number of calibration candles to probe (from extreme high + extreme low).</summary>
    public int MaxProbeCandles { get; set; } = 6;

    /// <summary>Process ID of the HTS application (for EnumWindows filtering).</summary>
    public uint TargetProcessId { get; set; }

    /// <summary>The chart window handle — for sending WM_MOUSEMOVE messages.</summary>
    public IntPtr ChartWindowHandle { get; set; }

    /// <summary>Window offset: chart image (0,0) corresponds to this screen coordinate.</summary>
    public int WindowLeft { get; set; }

    /// <summary>Window offset: chart image (0,0) corresponds to this screen coordinate.</summary>
    public int WindowTop { get; set; }

    /// <summary>If true, save tooltip screenshots to output/tooltips/ for debugging.</summary>
    public bool SaveDebugScreenshots { get; set; }

    /// <summary>Debug output directory.</summary>
    public string DebugOutputDir { get; set; } = "";

    // ── Public API ──

    /// <summary>
    /// Main entry point: hover over calibration candles, read tooltips, recalibrate Y-axis.
    /// Returns the number of successfully recalibrated candles, or 0 if calibration failed.
    /// </summary>
    public int CalibrateFromTooltips(ChartAnalysisResult result)
    {
        if (result.Candles.Count == 0) return 0;

        // Step 1: Select calibration candles with backup candidates for MISS retry.
        // SelectCalibrationCandidates returns primary picks first, then backups.
        var allCandidates = SelectCalibrationCandidates(result, MaxProbeCandles);
        if (allCandidates.Count == 0)
        {
            Console.WriteLine("[TOOLTIP] No calibration candles selected");
            return 0;
        }

        int primaryCount = Math.Min(MaxProbeCandles, allCandidates.Count);
        var probeTargets = allCandidates.Take(primaryCount).ToList();
        var backupTargets = allCandidates.Skip(primaryCount).ToList();

        Console.WriteLine($"[TOOLTIP] Selected {probeTargets.Count} calibration candles for probing" +
            (backupTargets.Count > 0 ? $" (+{backupTargets.Count} backups)" : ""));

        // Step 2: Activate chart — MFC tooltip only works when window has focus AND
        // mouse has entered the chart area. We do a "warm-up" by:
        // 1. Bring window to foreground
        // 2. Move mouse to chart center (triggers TrackMouseEvent in MFC)
        // DO NOT click — clicking can change chart state (zoom/select/crosshair).
        if (ChartWindowHandle != IntPtr.Zero)
        {
            WKAppBot.Win32.Native.NativeMethods.SmartSetForegroundWindow(ChartWindowHandle);
            Thread.Sleep(200);

            // Warm-up: move mouse to chart center to activate tooltip tracking.
            // Just moving the mouse into the chart area is enough to trigger TrackMouseEvent.
            int warmupX = WindowLeft + (result.ChartLeft + result.ChartRight) / 2;
            int warmupY = WindowTop + (result.ChartTop + (result.VolumeTop ?? result.ChartBottom)) / 2;
            WKAppBot.Win32.Input.MouseInput.MoveTo(warmupX, warmupY);
            Thread.Sleep(500);

            Console.WriteLine($"[TOOLTIP] Activated chart at ({warmupX},{warmupY})");
        }

        // Save original cursor position
        WKAppBot.Win32.Native.NativeMethods.GetCursorPos(out var originalCursor);

        var calibrationPoints = new List<(int pixelY, double price)>();
        var volumeCalibPoints = new List<(int barTopY, double volume)>(); // for volume calibration
        int hitCount = 0;
        int missCount = 0;

        try
        {
            // Step 3: Hover each calibration candle, read tooltip.
            // On MISS, try backup candidates from the reserve pool.
            // Hover at body center (not wick top) to avoid text overlay false positives.
            int maxY = (result.VolumeTop ?? result.ChartBottom) - 5;
            int backupIdx = 0;

            for (int probeIdx = 0; probeIdx < probeTargets.Count; probeIdx++)
            {
                var candle = probeTargets[probeIdx];
                int screenX = WindowLeft + candle.PixelX;

                // PRIMARY hover: body center (safer than wick top for text overlay defense).
                // Wick top was used before, but text annotations ("최고 903,000") extend
                // wicks far above the real candle body → hovering there hits empty space.
                int bodyCenter = (candle.PixelOpenY > 0 && candle.PixelCloseY > 0)
                    ? (candle.PixelOpenY + candle.PixelCloseY) / 2
                    : (candle.PixelHighY + candle.PixelLowY) / 2;
                bodyCenter = Math.Min(bodyCenter, maxY);
                int screenY = WindowTop + bodyCenter;

                Console.Write($"[TOOLTIP] Probing candle x={candle.PixelX} (screen {screenX},{screenY})...");

                var ohlcv = HoverAndReadTooltip(screenX, screenY, candle.PixelX);

                // Retry with wick top if body center missed
                if (ohlcv == null)
                {
                    int wickTop = Math.Min(candle.PixelHighY, maxY);
                    if (wickTop != bodyCenter)
                    {
                        int retryScreenY = WindowTop + wickTop;
                        Console.Write(" retry(wick)...");
                        Thread.Sleep(100);
                        ohlcv = HoverAndReadTooltip(screenX, retryScreenY, candle.PixelX);
                    }
                }

                // Still MISS → try backup candidates
                if (ohlcv == null)
                {
                    while (backupIdx < backupTargets.Count)
                    {
                        var backup = backupTargets[backupIdx++];
                        Console.Write($" MISS → backup x={backup.PixelX}...");

                        int bScreenX = WindowLeft + backup.PixelX;
                        int bBodyCenter = (backup.PixelOpenY > 0 && backup.PixelCloseY > 0)
                            ? (backup.PixelOpenY + backup.PixelCloseY) / 2
                            : (backup.PixelHighY + backup.PixelLowY) / 2;
                        bBodyCenter = Math.Min(bBodyCenter, maxY);
                        int bScreenY = WindowTop + bBodyCenter;

                        Thread.Sleep(100);
                        ohlcv = HoverAndReadTooltip(bScreenX, bScreenY, backup.PixelX);

                        if (ohlcv != null)
                        {
                            candle = backup; // use backup's pixel positions for calibration
                            break;
                        }
                    }

                    if (ohlcv == null)
                    {
                        Console.WriteLine(" MISS");
                        missCount++;
                        continue;
                    }
                }

                // Sanity check: H >= L, H >= O, H >= C, L <= O, L <= C
                if (ohlcv.High < ohlcv.Low || ohlcv.High < ohlcv.Open || ohlcv.High < ohlcv.Close ||
                    ohlcv.Low > ohlcv.Open || ohlcv.Low > ohlcv.Close)
                {
                    Console.WriteLine($" SANITY_FAIL O={ohlcv.Open:N0} H={ohlcv.High:N0} L={ohlcv.Low:N0} C={ohlcv.Close:N0}");
                    missCount++;
                    continue;
                }

                hitCount++;
                string volStr = ohlcv.Volume.HasValue ? $" V={ohlcv.Volume.Value:N0}" : "";
                Console.WriteLine($" O={ohlcv.Open:N0} H={ohlcv.High:N0} L={ohlcv.Low:N0} C={ohlcv.Close:N0}{volStr}");

                // Add calibration points: PixelHighY → tooltip High, PixelLowY → tooltip Low
                calibrationPoints.Add((candle.PixelHighY, ohlcv.High));
                calibrationPoints.Add((candle.PixelLowY, ohlcv.Low));

                // Also add Open/Close points for denser calibration
                if (candle.PixelOpenY > 0)
                    calibrationPoints.Add((candle.PixelOpenY, ohlcv.Open));
                if (candle.PixelCloseY > 0)
                    calibrationPoints.Add((candle.PixelCloseY, ohlcv.Close));

                // Collect volume calibration point (barTopY → absolute volume)
                if (ohlcv.Volume.HasValue && candle.VolumeBarTopY > 0)
                    volumeCalibPoints.Add((candle.VolumeBarTopY, ohlcv.Volume.Value));
            }
        }
        finally
        {
            // Always restore cursor
            WKAppBot.Win32.Native.NativeMethods.SetCursorPos(originalCursor.X, originalCursor.Y);
        }

        Console.WriteLine($"[TOOLTIP] Probing complete: {hitCount} hit, {missCount} miss");

        if (calibrationPoints.Count < 2)
        {
            Console.WriteLine($"[TOOLTIP] Only {calibrationPoints.Count} calibration points — need at least 2");
            return 0;
        }

        // Step 4: Build new Y-axis calibration and recalculate all candle prices
        Console.WriteLine($"[TOOLTIP] {calibrationPoints.Count} calibration points collected, recalibrating...");
        int recalibrated = RecalibrateAllCandles(result, calibrationPoints);
        Console.WriteLine($"[TOOLTIP] Recalibrated {recalibrated} candles using tooltip data");

        // Step 5: Calibrate volume using tooltip data
        if (volumeCalibPoints.Count > 0 && result.VolumeBottom.HasValue)
        {
            int volCalibrated = CalibrateVolume(result, volumeCalibPoints);
            Console.WriteLine($"[TOOLTIP] Volume calibrated: {volCalibrated} candles (from {volumeCalibPoints.Count} reference points)");
        }

        return recalibrated;
    }

    // ── Step 1: Select calibration candles ──

    /// <summary>
    /// Pick candles for tooltip probing from 3 zones: high-price, mid-price, low-price.
    /// Returns ranked candidates per zone so MISS can be retried with next candidate.
    ///
    /// Strategy: Divide probes into 3 groups:
    ///   - Top: candles with smallest PixelHighY (= highest price) — use BODY top, not wick
    ///   - Mid: candles near the vertical center of the chart
    ///   - Bottom: candles with largest PixelLowY (= lowest price) — use BODY bottom, not wick
    ///
    /// TEXT OVERLAY DEFENSE: HTS charts overlay colored text annotations like
    /// "최고 903,000 (09:03)" or "배당락(0.00%)" that get detected as wicks.
    /// A candle whose wick extends far beyond its body is suspicious — prefer
    /// candles where wick length is proportional to body size.
    /// We use body-center Y (not wick extremes) as the primary sort key.
    /// </summary>
    internal List<CandleData> SelectCalibrationCandles(ChartAnalysisResult result)
    {
        return SelectCalibrationCandidates(result, MaxProbeCandles);
    }

    /// <summary>
    /// Returns ranked candidates per zone. The first MaxProbeCandles are the primary picks,
    /// followed by backup candidates for MISS retry.
    /// </summary>
    internal List<CandleData> SelectCalibrationCandidates(ChartAnalysisResult result, int maxProbes)
    {
        // Only consider candles within the chart region (not volume panel)
        int chartTop = result.ChartTop;
        int chartBottom = result.VolumeTop ?? result.ChartBottom;
        int chartHeight = chartBottom - chartTop;

        var validCandles = result.Candles
            .Where(c => c.PixelHighY > 0 && c.PixelLowY > 0
                     && c.PixelHighY >= chartTop && c.PixelLowY <= chartBottom)
            .ToList();

        if (validCandles.Count == 0) return new List<CandleData>();

        // Calculate body-center Y for sorting (more reliable than wick extremes).
        // Wick can be extended by text overlay pixels, but body is stable.
        int BodyCenterY(CandleData c)
        {
            int openY = c.PixelOpenY > 0 ? c.PixelOpenY : c.PixelHighY;
            int closeY = c.PixelCloseY > 0 ? c.PixelCloseY : c.PixelLowY;
            return (openY + closeY) / 2;
        }

        // Wick suspicion score: high wick-to-body ratio = text overlay suspect.
        // body height 0 → treat as 1 to avoid div-by-zero
        double WickSuspicion(CandleData c)
        {
            int bodyTop = Math.Min(c.PixelOpenY > 0 ? c.PixelOpenY : c.PixelHighY,
                                   c.PixelCloseY > 0 ? c.PixelCloseY : c.PixelLowY);
            int bodyBottom = Math.Max(c.PixelOpenY > 0 ? c.PixelOpenY : c.PixelHighY,
                                     c.PixelCloseY > 0 ? c.PixelCloseY : c.PixelLowY);
            int bodyH = Math.Max(bodyBottom - bodyTop, 1);
            int wickAbove = bodyTop - c.PixelHighY;  // wick extending above body
            int wickBelow = c.PixelLowY - bodyBottom; // wick extending below body
            int totalWick = wickAbove + wickBelow;
            return (double)totalWick / bodyH;
        }

        // Filter out candles with extremely suspicious wick ratios (>10x body = almost certainly text)
        // Also filter candles with wick extending > 30% of chart height beyond body (unrealistic)
        var cleanCandles = validCandles.Where(c =>
        {
            double suspicion = WickSuspicion(c);
            if (suspicion > 10.0) return false; // wick 10x longer than body = text overlay

            int bodyTop = Math.Min(c.PixelOpenY > 0 ? c.PixelOpenY : c.PixelHighY,
                                   c.PixelCloseY > 0 ? c.PixelCloseY : c.PixelLowY);
            int wickAbove = bodyTop - c.PixelHighY;
            if (chartHeight > 0 && wickAbove > chartHeight * 0.30) return false; // unrealistic upper wick

            return true;
        }).ToList();

        // If filtering removed too many, fall back to all valid candles
        if (cleanCandles.Count < maxProbes)
            cleanCandles = validCandles;

        var selected = new List<CandleData>();
        var usedX = new HashSet<int>();

        // Candidates per zone (generous — 3x the needed count for MISS retries)
        int perZone = Math.Max(2, maxProbes);

        // TOP zone: highest price candles — sort by body-center Y ascending (smallest = highest price)
        // Prefer low wick-suspicion as tiebreaker
        var topCandidates = cleanCandles
            .OrderBy(c => BodyCenterY(c))
            .ThenBy(c => WickSuspicion(c))
            .Take(perZone)
            .ToList();

        // BOTTOM zone: lowest price candles — sort by body-center Y descending (largest = lowest price)
        var bottomCandidates = cleanCandles
            .OrderByDescending(c => BodyCenterY(c))
            .ThenBy(c => WickSuspicion(c))
            .Take(perZone)
            .ToList();

        // MID zone: closest to vertical center
        int chartMidY = (chartTop + chartBottom) / 2;
        var midCandidates = cleanCandles
            .OrderBy(c => Math.Abs(BodyCenterY(c) - chartMidY))
            .ThenBy(c => WickSuspicion(c))
            .Take(perZone)
            .ToList();

        // Pick primary candidates: round-robin from zones
        int topIdx = 0, midIdx = 0, bottomIdx = 0;
        int topCount = Math.Max(1, maxProbes / 3);
        int bottomCount = Math.Max(1, maxProbes / 3);
        int midCount = Math.Max(1, maxProbes - topCount - bottomCount);

        void PickFromZone(List<CandleData> candidates, ref int idx, int count)
        {
            int picked = 0;
            while (picked < count && idx < candidates.Count)
            {
                var c = candidates[idx++];
                if (usedX.Add(c.PixelX))
                {
                    selected.Add(c);
                    picked++;
                }
            }
        }

        PickFromZone(topCandidates, ref topIdx, topCount);
        PickFromZone(bottomCandidates, ref bottomIdx, bottomCount);
        PickFromZone(midCandidates, ref midIdx, midCount);

        // Add backup candidates for MISS retries (remaining from each zone)
        PickFromZone(topCandidates, ref topIdx, perZone);
        PickFromZone(bottomCandidates, ref bottomIdx, perZone);
        PickFromZone(midCandidates, ref midIdx, perZone);

        // Sort by X position for predictable left-to-right probe order
        // But keep primary candidates first, backups after
        int primaryCount = Math.Min(maxProbes, selected.Count);
        var primary = selected.Take(primaryCount).OrderBy(c => c.PixelX).ToList();
        var backups = selected.Skip(primaryCount).OrderBy(c => c.PixelX).ToList();

        var final = new List<CandleData>(primary);
        final.AddRange(backups);
        return final;
    }

    // ── Step 2: Hover and read tooltip ──

    /// <summary>
    /// Move cursor to screen position, send WM_MOUSEMOVE, wait for tooltip, capture and OCR it.
    /// Returns parsed OHLC or null if tooltip not found/readable.
    ///
    /// HTS tooltip requires SendInput mouse move (SetCursorPos alone is insufficient).
    /// We also send WM_MOUSEMOVE to the deepest child window as backup.
    /// Client coordinates for WM_MOUSEMOVE = screen coords - window rect origin.
    /// </summary>
    internal TooltipOhlcv?
        HoverAndReadTooltip(int screenX, int screenY, int candlePixelX)
    {
        // Use SendInput to physically move mouse — MFC chart requires real mouse
        // input queue events (not just SetCursorPos or WM_MOUSEMOVE message).
        // SendInput generates WM_MOUSEMOVE through the proper input pipeline.
        WKAppBot.Win32.Input.MouseInput.MoveTo(screenX, screenY);

        // Also send WM_MOUSEMOVE to the chart child window as backup
        if (ChartWindowHandle != IntPtr.Zero)
        {
            // Walk down to deepest child window at the mouse position
            IntPtr targetHwnd = ChartWindowHandle;
            IntPtr current = ChartWindowHandle;
            for (int depth = 0; depth < 10; depth++)
            {
                var pt = new WKAppBot.Win32.Native.POINT { X = screenX, Y = screenY };
                WKAppBot.Win32.Native.NativeMethods.ScreenToClient(current, ref pt);
                var child = WKAppBot.Win32.Native.NativeMethods.ChildWindowFromPointEx(
                    current, pt, WKAppBot.Win32.Native.NativeMethods.CWP_SKIPINVISIBLE);
                if (child == IntPtr.Zero || child == current) break;
                current = child;
            }
            targetHwnd = current;

            var clientPt = new WKAppBot.Win32.Native.POINT { X = screenX, Y = screenY };
            WKAppBot.Win32.Native.NativeMethods.ScreenToClient(targetHwnd, ref clientPt);
            var lParam = WKAppBot.Win32.Native.NativeMethods.MakeLParam(clientPt.X, clientPt.Y);
            WKAppBot.Win32.Native.NativeMethods.SendMessageW(
                targetHwnd,
                WKAppBot.Win32.Native.NativeMethods.WM_MOUSEMOVE,
                IntPtr.Zero, lParam);

            var sb = new StringBuilder(256);
            WKAppBot.Win32.Native.NativeMethods.GetClassNameW(targetHwnd, sb, 256);
            Console.Write($" [{sb}@({clientPt.X},{clientPt.Y})]");
        }

        Thread.Sleep(TooltipWaitMs);

        // Find visible tooltip window (try multiple times with increasing delay)
        IntPtr tooltipHwnd = IntPtr.Zero;
        for (int retry = 0; retry < 3 && tooltipHwnd == IntPtr.Zero; retry++)
        {
            tooltipHwnd = FindVisibleTooltip();
            if (tooltipHwnd == IntPtr.Zero && retry < 2)
                Thread.Sleep(200); // extra wait for tooltip to appear
        }
        if (tooltipHwnd == IntPtr.Zero)
            return null;

        // Capture tooltip screenshot
        WKAppBot.Win32.Native.NativeMethods.GetWindowRect(tooltipHwnd, out var rect);
        int w = rect.Right - rect.Left;
        int h = rect.Bottom - rect.Top;
        if (w <= 0 || h <= 0) return null;

        Bitmap? screenshot = null;

        // Try PrintWindow first (works even if tooltip overlaps other windows)
        try
        {
            screenshot = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using var g = Graphics.FromImage(screenshot);
            var hdc = g.GetHdc();
            bool ok = PrintWindow(tooltipHwnd, hdc, PW_RENDERFULLCONTENT);
            g.ReleaseHdc(hdc);

            if (!ok)
            {
                screenshot.Dispose();
                screenshot = null;
            }
        }
        catch
        {
            screenshot?.Dispose();
            screenshot = null;
        }

        // Fallback: screen region capture
        if (screenshot == null)
        {
            try
            {
                screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(
                    rect.Left, rect.Top, w, h);
            }
            catch
            {
                return null;
            }
        }

        // Save debug screenshot
        if (SaveDebugScreenshots && !string.IsNullOrEmpty(DebugOutputDir))
        {
            try
            {
                Directory.CreateDirectory(DebugOutputDir);
                var path = Path.Combine(DebugOutputDir, $"tooltip_x{candlePixelX}_{DateTime.Now:HHmmss}.png");
                screenshot.Save(path, ImageFormat.Png);
            }
            catch { /* best-effort */ }
        }

        // OCR the tooltip
        try
        {
            var ocr = new SimpleOcrAnalyzer();
            var ocrResult = ocr.RecognizeAll(screenshot).GetAwaiter().GetResult();
            screenshot.Dispose();

            // Always show tooltip size; show OCR text only in debug mode
            Console.Write($" {w}x{h}");
            if (SaveDebugScreenshots)
                Console.Write($" OCR=\"{ocrResult.FullText.Replace("\n", "|")[..Math.Min(ocrResult.FullText.Length, 120)]}\"");

            var parsed = ParseTooltipOhlc(ocrResult.FullText);
            if (parsed == null)
                Console.Write(" [PARSE_FAIL]");
            return parsed;
        }
        catch (Exception ex)
        {
            screenshot.Dispose();
            Console.Write($" [OCR_ERR:{ex.Message}]");
            return null;
        }
    }

    /// <summary>
    /// Find the largest visible tooltips_class32 window belonging to the target process.
    /// HTS may have multiple tooltip windows (per form, per toolbar, etc.).
    /// The chart tooltip is typically the largest one — pick by area.
    /// </summary>
    private IntPtr FindVisibleTooltip()
    {
        IntPtr bestHwnd = IntPtr.Zero;
        int bestArea = 0;

        WKAppBot.Win32.Native.NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!WKAppBot.Win32.Native.NativeMethods.IsWindowVisible(hWnd)) return true;

            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != TargetProcessId) return true;

            var sb = new StringBuilder(256);
            WKAppBot.Win32.Native.NativeMethods.GetClassNameW(hWnd, sb, 256);
            if (!sb.ToString().Contains("tooltips_class", StringComparison.OrdinalIgnoreCase))
                return true;

            // Check it has non-zero size — pick the largest tooltip
            WKAppBot.Win32.Native.NativeMethods.GetWindowRect(hWnd, out var r);
            int w = r.Right - r.Left;
            int h = r.Bottom - r.Top;
            int area = w * h;
            if (w > 10 && h > 10 && area > bestArea)
            {
                bestHwnd = hWnd;
                bestArea = area;
            }

            return true; // continue enumeration to find the largest
        }, IntPtr.Zero);

        return bestHwnd;
    }

    // ── Step 3: Parse tooltip OHLC ──

    /// <summary>
    /// Parse OHLC from tooltip OCR text.
    ///
    /// ── Tooltip format (fixed line order) ──
    ///   일자 → 시간(분봉 only) → 종목명(코드) → 시가 → 고가 → 저가 → 종가 → 거래량
    ///
    /// ── Key insight: OCR FullText often has NO line breaks ──
    /// The entire tooltip is returned as one long string. Example:
    /// "일자 2026/02/09( ) 류하미닉스(000660) 시가 고가 거래량 • 880,000 (2.561) ..."
    ///
    /// ── Parsing strategy (v2) ──
    /// 1. Find stock code pattern "(\d{6})" to anchor OHLC start position
    /// 2. Extract ALL comma-formatted numbers (\d{1,3}(,\d{3})+) after stock code
    /// 3. Filter: val >= 1000 (skip percentages, small codes)
    /// 4. Take first 4 = Open, High, Low, Close (fixed order)
    ///
    /// ── Known pitfalls (learned from testing) ──
    /// - Stock code "(000660)" → OCR reads "660" as a number. Anchoring past code avoids this.
    /// - Date "2026/02/09" → OCR may insert comma "2,026". Skipped by >= 1000 filter + position.
    /// - DON'T truncate at "거래량" — OCR may merge "시가 고가 거래량" as one segment.
    /// - "[거래량]" prefix = volume-only tooltip from volume panel → return null.
    /// </summary>
    internal static TooltipOhlcv? ParseTooltipOhlc(string fullText)
    {
        if (string.IsNullOrWhiteSpace(fullText)) return null;

        // Check if this is a volume-only tooltip (거래량 tooltip from volume panel)
        // These don't have OHLC data — detected by "[거래량]" at start
        if (fullText.Contains("[거래량]") || fullText.Contains("[거래 량]"))
            return null;

        // Find stock code to know where OHLC starts
        var codeMatch = Regex.Match(fullText, @"\(\d{6}\)");
        string searchText = codeMatch.Success
            ? fullText[(codeMatch.Index + codeMatch.Length)..]
            : fullText;

        // DON'T truncate at "거래량" — OCR may merge "시가 고가 거래량" as one line
        // because "저가 종가" labels get garbled. Instead, just extract first 4+ prices.

        // Extract all comma-formatted numbers (NNN,NNN pattern) after stock code
        var priceRegex = new Regex(@"\d{1,3}(?:,\d{3})+");
        var matches = priceRegex.Matches(searchText);

        var numbers = new List<double>();
        foreach (Match m in matches)
        {
            var val = double.Parse(m.Value.Replace(",", ""));
            if (val >= 1000) // skip small numbers (percentages, codes)
            {
                numbers.Add(val);
                if (numbers.Count >= 5) break; // OHLC + Volume = 5 numbers
            }
        }

        if (numbers.Count < 4) return null;

        // Order: Open, High, Low, Close, [Volume]
        // Volume validation: The 5th number is volume ONLY if it's clearly different
        // from the OHLC price range. In 분봉 tooltips, OCR often captures a truncated
        // price as the 5th number (e.g., "896,00" → 896,000) which is in the same
        // magnitude as OHLC prices. Real volume is typically:
        //   - Much larger than price (e.g., price=886,000, volume=5,142,348)
        //   - Or much smaller (e.g., price=886,000, volume=1,430)
        // Heuristic: if 5th number is within 50%~200% of average OHLC price,
        // it's likely a misread price, not volume.
        double? volume = null;
        if (numbers.Count >= 5)
        {
            double avgPrice = (numbers[0] + numbers[1] + numbers[2] + numbers[3]) / 4.0;
            double candidate = numbers[4];
            double ratio = avgPrice > 0 ? candidate / avgPrice : 0;

            // Volume is valid only if it's outside the price magnitude range
            // (ratio < 0.5 means volume << price, ratio > 2.0 means volume >> price)
            if (ratio < 0.5 || ratio > 2.0)
                volume = candidate;
            // else: 5th number is too close to OHLC prices — likely a misread price fragment
        }

        return new TooltipOhlcv(
            Open: numbers[0], High: numbers[1], Low: numbers[2], Close: numbers[3],
            Volume: volume);
    }

    /// <summary>Find index of volume-related keyword in text.</summary>
    private static int FindVolumeKeyword(string text)
    {
        // Try various OCR variants of "거래량"
        string[] keywords = { "거래량", "거래 량", "거래랑", "거래럄" };
        int minIdx = -1;
        foreach (var kw in keywords)
        {
            int idx = text.IndexOf(kw, StringComparison.Ordinal);
            if (idx >= 0 && (minIdx < 0 || idx < minIdx))
                minIdx = idx;
        }
        return minIdx;
    }

    // ── Step 4: Recalibrate all candles ──

    /// <summary>
    /// Build a new Y-axis calibration from tooltip-derived points and recalculate
    /// all candle OHLC prices.
    ///
    /// The new calibration REPLACES the original OCR-based axis points,
    /// since tooltip data is far more accurate (exact server prices).
    ///
    /// Strategy: Collect (pixelY, price) pairs from tooltip readings,
    /// sort by pixelY, build new AxisPoints, then re-interpolate every candle.
    /// </summary>
    internal static int RecalibrateAllCandles(
        ChartAnalysisResult result,
        List<(int pixelY, double price)> calibrationPoints)
    {
        // Deduplicate: same pixelY with very close price → keep one
        var deduped = new Dictionary<int, double>();
        foreach (var (py, price) in calibrationPoints)
        {
            if (!deduped.ContainsKey(py))
                deduped[py] = price;
            else
            {
                // Average if same pixel has multiple readings
                deduped[py] = (deduped[py] + price) / 2.0;
            }
        }

        // Build new axis points sorted by pixel (ascending = top to bottom)
        var newAxis = deduped
            .Select(kv => new AxisPoint { Pixel = kv.Key, Value = kv.Value })
            .OrderBy(p => p.Pixel) // top (small Y) = high price first
            .ToList();

        if (newAxis.Count < 2)
            return 0;

        // Verify monotonicity: pixel increases → price decreases (Y-axis inverted)
        // If not monotonic, there's bad data — filter outliers
        var filtered = new List<AxisPoint> { newAxis[0] };
        for (int i = 1; i < newAxis.Count; i++)
        {
            if (newAxis[i].Value < filtered[^1].Value) // price should decrease as Y increases
                filtered.Add(newAxis[i]);
            // else skip non-monotonic point
        }

        if (filtered.Count < 2)
        {
            Console.WriteLine("[TOOLTIP] WARNING: Calibration points are not monotonic — keeping original calibration");
            return 0;
        }

        // Replace Y-axis calibration
        Console.WriteLine($"[TOOLTIP] New Y-axis: {filtered.Count} points, " +
            $"price range {filtered[^1].Value:N0} ~ {filtered[0].Value:N0}, " +
            $"pixel range {filtered[0].Pixel} ~ {filtered[^1].Pixel}");

        // Also merge with original axis points that fall within the new range
        // This gives us more interpolation points for better accuracy
        var mergedAxis = new List<AxisPoint>(filtered);
        int mergedFromOcr = 0;
        foreach (var origPoint in result.YAxisPoints)
        {
            // Only include original points that are within the new pixel range
            if (origPoint.Pixel >= filtered[0].Pixel && origPoint.Pixel <= filtered[^1].Pixel)
            {
                // Check if the original point is consistent with the new calibration
                double expectedPrice = ChartAnalyzer.Interpolate(origPoint.Pixel, filtered);
                double diff = Math.Abs(origPoint.Value - expectedPrice);
                double tolerance = Math.Abs(expectedPrice) * 0.05; // 5% tolerance

                if (diff <= tolerance)
                {
                    // Consistent — add to merged axis
                    if (!mergedAxis.Any(p => Math.Abs(p.Pixel - origPoint.Pixel) < 3))
                    {
                        mergedAxis.Add(origPoint);
                        mergedFromOcr++;
                    }
                }
            }
        }

        mergedAxis = mergedAxis.OrderBy(p => p.Pixel).ToList();

        if (mergedFromOcr > 0)
            Console.WriteLine($"[TOOLTIP] Merged {mergedFromOcr} consistent OCR axis points → {mergedAxis.Count} total");

        // Update result's Y-axis
        result.YAxisPoints = mergedAxis;

        // Recalculate all candle prices using the new axis
        int count = 0;
        foreach (var candle in result.Candles)
        {
            if (candle.PixelHighY <= 0 && candle.PixelLowY <= 0) continue;

            candle.High = ChartAnalyzer.Interpolate(candle.PixelHighY, mergedAxis);
            candle.Low = ChartAnalyzer.Interpolate(candle.PixelLowY, mergedAxis);

            if (candle.PixelOpenY > 0)
                candle.Open = ChartAnalyzer.Interpolate(candle.PixelOpenY, mergedAxis);
            if (candle.PixelCloseY > 0)
                candle.Close = ChartAnalyzer.Interpolate(candle.PixelCloseY, mergedAxis);

            count++;
        }

        return count;
    }

    // ── Step 5: Volume calibration ──

    /// <summary>
    /// Convert relative volume (0.0~1.0) to absolute volume using tooltip reference points.
    ///
    /// Strategy: Volume bars are linear — bar height is proportional to volume.
    ///   barTopY → volume (from tooltip)  +  volumeBottom → 0
    /// We use the simplest model: volume = barHeight * scale
    /// where scale = tooltipVolume / tooltipBarHeight
    ///
    /// If multiple reference points exist, average the scale factor for robustness.
    /// </summary>
    internal static int CalibrateVolume(
        ChartAnalysisResult result,
        List<(int barTopY, double volume)> volumeCalibPoints)
    {
        if (!result.VolumeBottom.HasValue) return 0;
        int volBottom = result.VolumeBottom.Value;

        // Calculate scale factor: volume / barHeight for each reference point
        var scales = new List<double>();
        foreach (var (barTopY, volume) in volumeCalibPoints)
        {
            int barHeight = volBottom - barTopY;
            if (barHeight > 0 && volume > 0)
                scales.Add(volume / barHeight);
        }

        if (scales.Count == 0) return 0;

        // Use median scale factor (robust to outliers)
        scales.Sort();
        double scale = scales[scales.Count / 2];

        Console.WriteLine($"[TOOLTIP] Volume scale: {scale:N1} per pixel (from {scales.Count} reference points)");

        // Apply scale to all candles
        int count = 0;
        foreach (var candle in result.Candles)
        {
            if (candle.VolumeBarTopY > 0 && candle.VolumeBarTopY < volBottom)
            {
                int barHeight = volBottom - candle.VolumeBarTopY;
                candle.Volume = Math.Round(barHeight * scale);
                count++;
            }
            else
            {
                candle.Volume = 0;
            }
        }

        return count;
    }
}
