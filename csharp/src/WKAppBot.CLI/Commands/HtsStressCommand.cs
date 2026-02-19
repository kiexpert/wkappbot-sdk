using System.Diagnostics;
using WKAppBot.Core.Runner;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: hts-stress command
internal partial class Program
{
    // ── hts-stress ─────────────────────────────────────────────

    static int HtsStressCommand(string[] args)
    {
        // Parse args
        string? formPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (!args[i].StartsWith("-") && formPath == null)
            { formPath = args[i]; continue; }
        }

        string pattern = GetArgValue(args, "--pattern") ?? "repeat";
        int iterations = int.TryParse(GetArgValue(args, "-n"), out var n) ? n : 20;
        int delayMs = int.TryParse(GetArgValue(args, "--delay"), out var dl) ? dl : 800;
        int openMs = int.TryParse(GetArgValue(args, "--open-ms"), out var o) ? o : 1000;
        int closeMs = int.TryParse(GetArgValue(args, "--close-ms"), out var c) ? c : 1000;
        int openCount = int.TryParse(GetArgValue(args, "--open"), out var oc) ? oc : 3;
        int closeCount = int.TryParse(GetArgValue(args, "--close"), out var cc) ? cc : 1;
        int batchSize = int.TryParse(GetArgValue(args, "--batch"), out var bs) ? bs : 1;
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 500;
        string processName = GetArgValue(args, "--process") ?? "HTSRun";

        if (string.IsNullOrEmpty(formPath))
            return Error(@"Usage: wkappbot hts-stress <form.xmf> [options]

Patterns (--pattern):
  repeat    Simple open/close N times (default)
  memory    Open M / close K per cycle + memory table
  ctx-only  Anchor form + repeated open/close (V8 Context isolation)

Options:
  -n N              Iterations (default: 20)
  --pattern <name>  Test pattern: repeat|memory|ctx-only
  --delay N         Delay between actions in ms (default: 800)
  --open-ms N       Open delay for repeat pattern (default: 1000)
  --close-ms N      Close delay for repeat pattern (default: 1000)
  --open N          Forms to open per cycle for memory pattern (default: 3)
  --close N         Forms to close per cycle for memory pattern (default: 1)
  --batch N         Batch size for ctx-only pattern (default: 1)
  --process <name>  Target process name (default: HTSRun)
  --no-watch        Disable background [WATCH] element tracker
  --watch-interval  Watcher polling ms (default: 500)");

        // Find target process + MDI frame
        var proc = Process.GetProcessesByName(processName).FirstOrDefault();
        if (proc == null)
            return Error($"{processName}.exe not found! Is the app running?");

        var hMain = WindowFinder.FindMDIMainFrame((uint)proc.Id);
        if (hMain == IntPtr.Zero)
            return Error($"No MDI main frame found in {processName} (PID: {proc.Id})");

        // Validate form path
        if (!File.Exists(formPath))
        {
            // Try relative to VIGSOne
            var alt = $@"W:\VIGSOne\BinD\Win32\MultiByte\HTSRun\screen\{formPath}";
            if (File.Exists(alt)) formPath = alt;
            else return Error($"Form file not found: {formPath}");
        }

        // Verify COPYDATASTRUCT SendMessage support
        var hMdi = NativeMethods.FindWindowExW(hMain, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero)
            return Error("MDIClient child not found — is this an MDI application?");

        var baseMdi = WindowFinder.CountMDIChildren(hMain);
        var baseMem = HtsInterop.TakeMemorySnapshot(proc);

        // ── Banner ──
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  WKAppBot HTS Stress — {pattern.ToUpper(),-10}                          ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"  Target   : {processName} (PID:{proc.Id}) MDI 0x{hMain:X8}");
        Console.WriteLine($"  Form     : {Path.GetFileName(formPath)}");
        Console.WriteLine($"  Pattern  : {pattern} x {iterations}");
        Console.WriteLine($"  Baseline : WS={baseMem.WorkingSetMB}MB  Priv={baseMem.PrivateMemMB}MB  MDI={baseMdi}");
        Console.WriteLine();

        // ── Initialize Simple OCR (free, offline) ──
        SimpleOcrAnalyzer? simpleOcr = null;
        try
        {
            var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
            var primaryLang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
            simpleOcr = new SimpleOcrAnalyzer(primaryLanguage: primaryLang, confidenceThreshold: 0.7f);
        }
        catch { /* OCR not available — continue without it */ }

        // ── Start background watcher (optional) ──
        BackgroundWatcher? watcher = null;
        var ctx = new RuntimeContext { MainWindowHandle = hMain, AppTitle = processName };
        object consoleLock = new();

        if (!noWatch)
        {
            watcher = new BackgroundWatcher(watchMs, ctx: ctx, ocr: simpleOcr);
            consoleLock = watcher.ConsoleLock;
            ctx.ConsoleLock = consoleLock;
            watcher.Start();

            var ocrTag = simpleOcr != null ? " + OCR" : "";
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[WATCH] ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Started — tracking MDI form changes{ocrTag}");
                Console.ResetColor();
            }
        }

        // ── Memory table header ──
        lock (consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  {"#",-6} {"WS(MB)",-9} {"Priv(MB)",-10} {"WS_Chg",-9} {"Priv_Chg",-10} {"MDI",-5} Phase");
            Console.WriteLine($"  {"─────",-6} {"───────",-9} {"────────",-10} {"──────",-9} {"────────",-10} {"───",-5} ─────");
            Console.ResetColor();
        }

        // ── Cycle event handler (all 3 patterns share this) ──
        int lastNudgeCycle = -1;
        void OnCycle(StressCycleEvent evt)
        {
            // Nudge watcher on cycle boundaries (not every open/close)
            if (watcher != null && evt.Cycle != lastNudgeCycle && evt.Phase != "OPEN")
            {
                watcher.Nudge();
                lastNudgeCycle = evt.Cycle;
            }

            // Only print summary rows (not individual OPEN/CLOSE for repeat)
            bool printRow = pattern switch
            {
                "repeat" => evt.Phase == "CLOSE",   // 1 row per cycle (after close)
                "memory" => evt.Phase == "CYCLE",    // 1 row per cycle
                "ctx-only" => true,                  // all phases
                _ => true
            };

            if (!printRow) return;

            var mem = evt.Memory;
            var wsDelta = mem.WsDeltaMB(evt.Baseline);
            var privDelta = mem.PrivDeltaMB(evt.Baseline);
            var wsSign = wsDelta >= 0 ? "+" : "";
            var privSign = privDelta >= 0 ? "+" : "";

            // Color: red>20MB, yellow>5MB, green otherwise
            var color = privDelta > 20 ? ConsoleColor.Red
                      : privDelta > 5 ? ConsoleColor.Yellow
                      : ConsoleColor.Green;

            lock (consoleLock)
            {
                // Clear any [WATCH] remnant
                ClearCurrentLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[STRESS] ");
                Console.ForegroundColor = color;
                Console.Write($"{evt.Cycle,-5} ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{mem.WorkingSetMB,-9} {mem.PrivateMemMB,-10} ");
                Console.ForegroundColor = color;
                Console.Write($"{wsSign}{wsDelta,-8} {privSign}{privDelta,-9} ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{(evt.MdiAfter >= 0 ? evt.MdiAfter.ToString() : "?"),-5} ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(evt.Phase);

                if (!evt.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" FAIL");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // ── Run the selected pattern ──
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        StressResult result;
        try
        {
            result = pattern.ToLower() switch
            {
                "repeat" => HtsInterop.RunRepeat(
                    hMain, formPath, proc, iterations, openMs, closeMs, OnCycle, cts.Token),
                "memory" => HtsInterop.RunMemory(
                    hMain, formPath, proc, iterations, openCount, closeCount, delayMs, OnCycle, cts.Token),
                "ctx-only" or "ctx_only" or "ctxonly" => HtsInterop.RunCtxOnly(
                    hMain, formPath, proc, iterations, batchSize, delayMs, OnCycle, cts.Token),
                _ => throw new ArgumentException($"Unknown pattern: {pattern}. Use: repeat|memory|ctx-only")
            };
        }
        catch (OperationCanceledException)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[STRESS] Cancelled by user (Ctrl+C)");
                Console.ResetColor();
            }

            watcher?.Stop(printSummary: true);
            watcher?.Dispose();
            simpleOcr?.Dispose();
            return 0;
        }

        // ── Stop watcher ──
        watcher?.Stop(printSummary: true);
        watcher?.Dispose();
        simpleOcr?.Dispose();

        // ── Results summary ──
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  Results ({result.Elapsed:hh\\:mm\\:ss})                                       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"  Pattern    : {result.Pattern}");
        Console.WriteLine($"  Iterations : {result.Iterations}");
        Console.WriteLine($"  Baseline   : WS={result.Baseline.WorkingSetMB}MB  Priv={result.Baseline.PrivateMemMB}MB");
        Console.WriteLine($"  Final      : WS={result.Final.WorkingSetMB}MB  Priv={result.Final.PrivateMemMB}MB  MDI={result.RemainingMdi}");

        var growthColor = result.PrivGrowthMB > 20 ? ConsoleColor.Red
                        : result.PrivGrowthMB > 5 ? ConsoleColor.Yellow
                        : ConsoleColor.Green;
        Console.ForegroundColor = growthColor;
        Console.WriteLine($"  Growth     : WS +{result.WsGrowthMB}MB  Priv +{result.PrivGrowthMB}MB");
        Console.ResetColor();
        Console.WriteLine($"  Actions    : {result.OpenCount} opens ({result.OpenOk} ok), {result.CloseCount} closes ({result.CloseOk} ok)");

        if (result.PerCycleKB != 0)
        {
            var perCycleColor = result.PerCycleKB > 100 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = perCycleColor;
            Console.WriteLine($"  Per cycle  : ~{result.PerCycleKB} KB/cycle");
            Console.ResetColor();
        }
        if (result.TotalContexts > 0 && result.PerContextKB != 0)
        {
            var perCtxColor = result.PerContextKB > 50 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = perCtxColor;
            Console.WriteLine($"  Per context: ~{result.PerContextKB} KB ({result.TotalContexts} contexts)");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("══════════════════════════════════════════════════════════");
        Console.ResetColor();

        return 0;
    }
}
