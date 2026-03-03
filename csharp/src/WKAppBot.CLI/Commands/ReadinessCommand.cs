using System.Diagnostics;
using System.Text;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: readiness command — probe InputReadiness on foreground/target window
// Completely focusless — no input, no focus steal, just diagnostics + profiling.
internal partial class Program
{
    static int ReadinessCommand(string[] args)
    {
        // Usage: wkappbot readiness [grap] [--point X Y] [--zoom]
        // No args → foreground window. grap → WindowFinder match. --point → ProbeAtPoint. --zoom → show zoom overlay.

        bool pointMode = false;
        int pointX = 0, pointY = 0;
        string? grap = null;
        bool showZoom = args.Contains("--zoom");
        bool testYield = args.Contains("--yield");

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--point" && i + 2 < args.Length)
            {
                pointMode = true;
                int.TryParse(args[i + 1], out pointX);
                int.TryParse(args[i + 2], out pointY);
                i += 2;
            }
            else if (!args[i].StartsWith("--"))
            {
                grap = args[i];
            }
        }

        // ── Step 1: Find target window ──
        var swTotal = Stopwatch.StartNew();
        var swStep = Stopwatch.StartNew();
        IntPtr targetHwnd;

        if (!string.IsNullOrEmpty(grap))
        {
            var matches = WindowFinder.FindByTitle(grap);
            if (matches.Count == 0)
                return Error($"Window not found: \"{grap}\"");
            targetHwnd = matches[0].Handle;
            Console.WriteLine($"[READINESS] grap match: 0x{targetHwnd:X} \"{matches[0].Title}\" [{matches[0].ClassName}]");
        }
        else
        {
            targetHwnd = NativeMethods.GetForegroundWindow();
            if (targetHwnd == IntPtr.Zero)
                return Error("No foreground window found");

            var title = WindowFinder.GetWindowText(targetHwnd);
            var cls = WindowFinder.GetClassName(targetHwnd);
            Console.WriteLine($"[READINESS] foreground: 0x{targetHwnd:X} \"{title}\" [{cls}]");
        }
        swStep.Stop();
        Console.WriteLine($"[PROF] FindTarget={swStep.ElapsedMilliseconds}ms");
        Console.Out.Flush();

        // ── Step 2: Create InputReadiness ──
        swStep.Restart();
        var readiness = CreateInputReadiness();
        swStep.Stop();
        Console.WriteLine($"[PROF] CreateReadiness={swStep.ElapsedMilliseconds}ms");
        Console.Out.Flush();

        if (pointMode)
        {
            // ── ProbeAtPoint mode ──
            Console.WriteLine($"[READINESS] ProbeAtPoint ({pointX},{pointY})");
            Console.Out.Flush();

            swStep.Restart();
            var pointReport = readiness.ProbeAtPoint(new PointReadinessRequest
            {
                ScreenX = pointX,
                ScreenY = pointY,
                TargetHwnd = targetHwnd,
            });
            swStep.Stop();
            Console.WriteLine($"[PROF] ProbeAtPoint={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // Print classification result
            Console.ForegroundColor = pointReport.FocuslessClicked ? ConsoleColor.Green
                : pointReport.NeedsPhysicalClick ? ConsoleColor.Yellow
                : ConsoleColor.Red;
            Console.WriteLine($"[READINESS] Result: {pointReport.Classification} " +
                $"focusless={pointReport.FocuslessClicked} physical={pointReport.NeedsPhysicalClick} " +
                $"stolen={pointReport.ForegroundStolen}");
            Console.ResetColor();
        }
        else
        {
            // ── Probe mode (full survey) ──
            Console.WriteLine("[READINESS] Full probe (survey)");
            Console.Out.Flush();

            swStep.Restart();
            var report = readiness.Probe(new InputReadinessRequest
            {
                TargetHwnd = targetHwnd,
                IntendedAction = testYield ? "input" : "readiness-probe", // --yield: force yield overlay
                MainHwnd = NativeMethods.GetAncestor(targetHwnd, NativeMethods.GA_ROOT),
                SkipZoom = !showZoom, // --zoom: show zoom overlay for testing
            });
            swStep.Stop();
            Console.WriteLine($"[PROF] Probe={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            InputReadiness.PrintReport(report);
        }

        Console.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
        Console.Out.Flush();
        return 0;
    }
}
