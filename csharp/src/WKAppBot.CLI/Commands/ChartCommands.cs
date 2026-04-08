using System.Drawing;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: chart-analyze + tooltip-probe commands
internal partial class Program
{
    // ── chart-analyze ───────────────────────────────────────────

    /// <summary>
    /// chart-analyze: Extract OHLC candlestick data from chart screenshots.
    ///
    /// ── Design Intent ──
    /// Runs ChartAnalyzer pipeline on a live window capture or saved image.
    /// Supports 3 detection strategies (body-first, column-scan, hts_style).
    ///
    /// --candles N: Known candle count from chart UI OCR (e.g., "600/600").
    ///   Enables deltaX-based equal-spacing candle placement in ultra-dense mode.
    ///   Without this, ultra-dense mode uses "every column = 1 candle" fallback
    ///   which may produce more candles than actual (float boundary duplicates).
    ///
    /// --form <id>: MDI child form ID to capture. Uses AppScanner to find the form,
    ///   then BringWindowToTop + SWP_NOACTIVATE to capture without focus steal.
    ///
    /// --debug: Saves debug overlay image showing detected regions, candle markers,
    ///   and Y-axis calibration lines. Useful for visual verification.
    /// ──
    /// </summary>
    static int ChartAnalyzeCommand(string[] args)
    {
        if (args.Length == 0)
            return Error(@"Usage: appbot chart-analyze <window-title|image.png> [--form <id>] [--candles N] [--tooltip] [-o output.json] [--debug]
  Analyzes candlestick chart screenshot and extracts OHLC + volume data.
  Supports any HTS — detects chart region, axis labels, and candle colors automatically.

  <window-title>  Live window capture + analyze
  <image.png>     Analyze saved image file
  --form <id>     MDI child form to capture (brings to front)
  --candles N     Expected candle count (from chart UI OCR). Enables deltaX placement.
  --tooltip       Phase B: hover extreme candles, read tooltip for Y-axis recalibration
  --tooltip-wait N  Tooltip render delay in ms (default: 400)
  --probes N      Number of calibration candles to probe (default: 6)
  -o <file>       Output JSON file (default: stdout)
  --debug         Save debug overlay image showing detected regions");

        string input = args[0];
        string? outputPath = GetArgValue(args, "-o");
        string? formId = GetArgValue(args, "--form");
        bool debug = args.Contains("--debug");
        bool useTooltip = args.Contains("--tooltip");

        // Parse expected candle count (enables deltaX-based placement in ultra-dense mode)
        int expectedCandleCount = 0;
        var candlesStr = GetArgValue(args, "--candles");
        if (candlesStr != null && int.TryParse(candlesStr, out var cc) && cc > 0)
            expectedCandleCount = cc;

        // Tooltip calibration settings
        int tooltipWaitMs = int.TryParse(GetArgValue(args, "--tooltip-wait"), out var tw) ? tw : 400;
        int probeCount = int.TryParse(GetArgValue(args, "--probes"), out var pc) ? pc : 6;

        Bitmap screenshot;
        IntPtr capturedHwnd = IntPtr.Zero; // For tooltip: the window we captured
        uint capturedPid = 0;              // For tooltip: process ID

        // Determine if input is image file or window title
        if (File.Exists(input) && (input.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            || input.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            || input.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"Loading image: {input}");
            screenshot = new Bitmap(input);
            if (useTooltip)
                Console.WriteLine("[TOOLTIP] WARNING: --tooltip requires live window, not image file. Skipping tooltip calibration.");
        }
        else
        {
            // Live window capture
            var windows = WindowFinder.FindWindows(input);
            if (windows.Count == 0) return Error($"Window not found: \"{input}\"");
            var win = windows[0];

            if (formId != null)
            {
                // Find MDI child form and bring to front
                var scanResult = AppScanner.Scan(win.Handle);
                var form = scanResult.Forms.FirstOrDefault(f =>
                    f.FormId != null && f.FormId.Contains(formId, StringComparison.OrdinalIgnoreCase));
                if (form == null) return Error($"Form [{formId}] not found in \"{win.Title}\"");

                Console.WriteLine($"Capturing form: [{form.FormId}] {form.FormName}");
                NativeMethods.BringWindowToTop(form.Handle);
                NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOP,
                    0, 0, 0, 0,
                    NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
                Thread.Sleep(200);
                screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(form.Handle);
                capturedHwnd = form.Handle;
            }
            else
            {
                Console.WriteLine($"Capturing: {win}");
                screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
                capturedHwnd = win.Handle;
            }

            // Get PID for tooltip enumeration
            if (capturedHwnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(capturedHwnd, out capturedPid);
            }
        }

        Console.WriteLine($"Image: {screenshot.Width}x{screenshot.Height}");

        // Run analysis (pass expectedCandleCount for deltaX placement in ultra-dense mode)
        if (expectedCandleCount > 0)
            Console.WriteLine($"  Expected candles: {expectedCandleCount} (deltaX mode)");

        var analyzer = new ChartAnalyzer();
        var result = analyzer.Analyze(screenshot, expectedCandleCount).GetAwaiter().GetResult();

        // Print summary
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n── Chart Analysis ──────────────────────────");
        Console.ResetColor();

        Console.WriteLine($"  Chart region: ({result.ChartLeft},{result.ChartTop}) - ({result.ChartRight},{result.ChartBottom})");
        Console.WriteLine($"  Y-axis calibration: {result.YAxisPoints.Count} points");
        foreach (var yp in result.YAxisPoints)
            Console.WriteLine($"    Y={yp.Pixel} → {yp.Value:N0} ({yp.Label})");

        Console.WriteLine($"  X-axis labels: {result.XAxisPoints.Count} points");
        foreach (var xp in result.XAxisPoints)
            Console.WriteLine($"    X={xp.Pixel} → \"{xp.Label}\"");

        Console.WriteLine($"  Color convention: {result.BullishColor}=bullish, {result.BearishColor}=bearish");

        if (result.VolumeTop.HasValue)
            Console.WriteLine($"  Volume panel: Y={result.VolumeTop}~{result.VolumeBottom}");
        else
            Console.WriteLine($"  Volume panel: not detected");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  Candles: {result.CandleCount}");
        Console.ResetColor();

        if (result.Candles.Count > 0)
        {
            Console.WriteLine($"  Price range: {result.PriceMin:N0} ~ {result.PriceMax:N0}");
            Console.WriteLine();

            // Print first/last few candles
            int show = Math.Min(result.Candles.Count, 5);
            for (int i = 0; i < show; i++)
            {
                var c = result.Candles[i];
                Console.ForegroundColor = c.IsBullish ? ConsoleColor.Red : ConsoleColor.Blue;
                Console.Write(c.IsBullish ? "  [UP] " : "  [DN] ");
                Console.ResetColor();
                Console.WriteLine(c);
            }
            if (result.Candles.Count > 10)
            {
                Console.WriteLine($"  ... ({result.Candles.Count - 10} more) ...");
                for (int i = result.Candles.Count - 5; i < result.Candles.Count; i++)
                {
                    var c = result.Candles[i];
                    Console.ForegroundColor = c.IsBullish ? ConsoleColor.Red : ConsoleColor.Blue;
                    Console.Write(c.IsBullish ? "  [UP] " : "  [DN] ");
                    Console.ResetColor();
                    Console.WriteLine(c);
                }
            }
        }

        // ── Tooltip-based Y-axis recalibration (Phase B) ──
        if (useTooltip && capturedHwnd != IntPtr.Zero && capturedPid != 0 && result.Candles.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n── Tooltip Calibration ─────────────────────");
            Console.ResetColor();

            // Get window rect for pixel-to-screen coordinate mapping
            NativeMethods.GetWindowRect(capturedHwnd, out var winRect);

            Console.Error.WriteLine($"[TOOLTIP] Window hWnd=0x{capturedHwnd:X8} PID={capturedPid}");
            Console.Error.WriteLine($"[TOOLTIP] Window rect: ({winRect.Left},{winRect.Top}) - ({winRect.Right},{winRect.Bottom})");

            var calibrator = new TooltipCalibrator
            {
                TargetProcessId = capturedPid,
                ChartWindowHandle = capturedHwnd,
                WindowLeft = winRect.Left,
                WindowTop = winRect.Top,
                TooltipWaitMs = tooltipWaitMs,
                MaxProbeCandles = probeCount,
                SaveDebugScreenshots = debug,
                DebugOutputDir = debug ? Path.Combine(DataDir, "output", "tooltips") : "",
            };

            int recalibrated = calibrator.CalibrateFromTooltips(result);
            if (recalibrated > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  Price range (recalibrated): {result.PriceMin:N0} ~ {result.PriceMax:N0}");
                Console.ResetColor();
            }
        }

        // Output JSON — save both per-candle and columnar formats
        if (outputPath != null)
        {
            // Per-candle object format (original)
            File.WriteAllText(outputPath, result.ToJson());

            // Columnar format: separate 1D arrays for O/H/L/C/V
            var columnarPath = Path.ChangeExtension(outputPath, ".arrays.json");
            File.WriteAllText(columnarPath, result.ToColumnarJson());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  JSON saved: {outputPath}");
            Console.WriteLine($"  Arrays JSON: {columnarPath}");
            Console.ResetColor();
        }

        // Always print columnar summary to stdout (truncated for readability)
        if (result.Candles.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n── OHLC Arrays ({result.Candles.Count} candles) ───────────────");
            Console.ResetColor();

            int n = result.Candles.Count;
            int preview = Math.Min(5, n); // show first/last 5
            var candles = result.Candles;

            // Helper: format array preview with "..." for long lists
            string ArrayPreview(Func<CandleData, string> selector)
            {
                var head = candles.Take(preview).Select(selector);
                if (n <= preview * 2)
                    return $"[{string.Join(", ", candles.Select(selector))}]";
                var tail = candles.Skip(n - preview).Select(selector);
                return $"[{string.Join(", ", head)}, ... , {string.Join(", ", tail)}]";
            }

            Console.WriteLine($"  open:   {ArrayPreview(c => c.Open.ToString("N0"))}");
            Console.WriteLine($"  high:   {ArrayPreview(c => c.High.ToString("N0"))}");
            Console.WriteLine($"  low:    {ArrayPreview(c => c.Low.ToString("N0"))}");
            Console.WriteLine($"  close:  {ArrayPreview(c => c.Close.ToString("N0"))}");
            if (candles.Any(c => c.Volume.HasValue))
                Console.WriteLine($"  volume: {ArrayPreview(c => c.Volume?.ToString("N0") ?? "null")}");
        }

        // Debug overlay
        if (debug)
        {
            var debugPath = outputPath != null
                ? Path.ChangeExtension(outputPath, ".debug.png")
                : $"chart_debug_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            using var overlay = ChartAnalyzer.GenerateDebugOverlay(screenshot, result);
            overlay.Save(debugPath);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Debug overlay: {debugPath}");
            Console.ResetColor();
        }

        screenshot.Dispose();
        return 0;
    }

    // ── tooltip-probe ─────────────────────────────────────────

    /// <summary>
    /// tooltip-probe: Enumerate all top-level windows for a process to find tooltip windows.
    /// Used for Phase B tooltip-based Y-axis recalibration in ChartAnalyzer.
    /// </summary>
    static int TooltipProbeCommand(string[] args)
    {
        string processName = args.Length > 0 && !args[0].StartsWith("-") ? args[0] : "nkrunlite";
        bool capture = args.Contains("--capture");
        TooltipProbe.Run(processName, capture);
        return 0;
    }
}
