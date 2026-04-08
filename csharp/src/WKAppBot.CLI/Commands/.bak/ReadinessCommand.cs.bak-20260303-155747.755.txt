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
        if (args.Contains("--help") || args.Contains("-h"))
            return Error(@"Usage: wkappbot readiness [grap] [--point X Y] [--yield]
  Probe InputReadiness on target window (completely focusless).
  Zoom overlay always shown (global --i-dont-want-to-see-the-zoom-magnifier-overlay to hide).

Target:
  (no args)           Foreground window (current focus)
  <grap>              Window title pattern match

Options:
  --point X Y         ProbeAtPoint (coordinate-based Z-order analysis)
  --yield             Force yield overlay (focus approval popup test)");

        bool pointMode = false;
        int pointX = 0, pointY = 0;
        string? grap = null;
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
                SkipZoom = false, // 돋보기 항상 표시 (글로벌 옵션으로만 숨김 가능)
            });
            swStep.Stop();
            Console.WriteLine($"[PROF] Probe={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            InputReadiness.PrintReport(report);

            swTotal.Stop();
            Console.WriteLine($"[PROF] TOTAL={swTotal.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // 돋보기에 결과 표시 — BeginFadeOut이 포그라운드 스레드로 승격하므로
            // 메인 스레드 종료 후에도 돋보기가 자동 페이드아웃까지 살아남음 (유령 돋보기)
            if (report.Zoom != null)
            {
                var verdict = report.Ready ? "READY" : "NOT READY";
                var best = report.Methods.FirstOrDefault(m => m.Available);
                report.Zoom.ShowPass($"{verdict}: {best?.Name ?? "none"}");
            }
        }

        return 0;
    }
}
