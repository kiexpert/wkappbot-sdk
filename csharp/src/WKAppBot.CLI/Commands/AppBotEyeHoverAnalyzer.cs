// AppBotEyeHoverAnalyzer.cs — A11Y Heal Scan (작전명: 엑빌 힐링 분석)
// Background worker in Eye: mouse still 7s → UIA tree dump → Gemini fallback if sparse.
//
// DESIGN INTENT:
//   When the user parks the mouse on a window for 7 seconds, Eye automatically:
//   1. Dumps the UIA element tree (a11y analysis)
//   2. If UIA is sparse (< MinUiaLines) or throws → falls back to Gemini screenshot analysis
//   One job at a time: if a previous scan is still running, new triggers are ignored.
//
//   Gemini-only (no Claude tokens consumed).  Output goes to Eye console + per-job log.

using System.Text;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── A11Y Heal Scan state ─────────────────────────────────────────────────
    const int HoverTriggerMs  = 7000;  // ms of stillness before scan fires
    const int MinUiaLines     = 8;     // UIA result below this → Gemini fallback
    const int HoverPollMs     = 300;   // mouse position poll interval

    // Fixed sentinel HWND for HoverAnalyzer Gemini dispatches.
    // Ensures all hover scans share a single Gemini tab (sandbox key = "ask+gemini+00A11EEE").
    static readonly IntPtr HoverGeminiSentinel = new IntPtr(0x00A11EEE);

    static volatile Task? _hoverScanTask;        // current scan job (null = idle)
    static volatile IntPtr _lastAnalyzedHwnd;    // prevent re-scan same window

    /// <summary>
    /// Start the A11Y Heal Scan background worker.
    /// Call once during Eye startup.
    /// </summary>
    internal static void StartHoverAnalyzer(CancellationToken ct) =>
        Task.Run(() => HoverAnalyzerLoop(ct), ct);

    static async Task HoverAnalyzerLoop(CancellationToken ct)
    {
        Console.WriteLine("[HOVER] A11Y Heal Scan worker started (trigger=7s, fallback=Gemini)");
        var lastPos   = new POINT();
        var lastMoved = DateTime.UtcNow;
        var prevHwnd  = IntPtr.Zero;

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(HoverPollMs, ct);

                NativeMethods.GetCursorPos(out var pt);
                if (pt.X != lastPos.X || pt.Y != lastPos.Y)
                {
                    lastPos   = pt;
                    lastMoved = DateTime.UtcNow;
                    prevHwnd  = IntPtr.Zero; // reset so next stillness re-evaluates
                    continue;
                }

                // Mouse still — check elapsed
                var stillMs = (DateTime.UtcNow - lastMoved).TotalMilliseconds;
                if (stillMs < HoverTriggerMs) continue;

                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;
                // Skip if same window already scanned, or another scan is running
                if (hwnd == _lastAnalyzedHwnd) continue;
                if (_hoverScanTask is { IsCompleted: false }) continue;

                _lastAnalyzedHwnd = hwnd;
                prevHwnd = hwnd;

                // Fire scan (non-blocking)
                _hoverScanTask = Task.Run(() => RunHoverScan(hwnd));
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[HOVER] Loop error: {ex.Message}"); }
        }
        Console.WriteLine("[HOVER] A11Y Heal Scan worker stopped");
    }

    static void RunHoverScan(IntPtr hwnd)
    {
        try
        {
            var titleBuf = new StringBuilder(256);
            NativeMethods.GetWindowTextW(hwnd, titleBuf, titleBuf.Capacity);
            var title = titleBuf.ToString();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[HOVER] A11Y Heal Scan — \"{title}\" (0x{hwnd:X8})");
            Console.ResetColor();

            // ── Step 1: UIA tree dump ────────────────────────────────────────
            string? uiaTree = null;
            int uiaLines    = 0;
            try
            {
                using var uia = new UiaLocator();
                uiaTree  = uia.DumpTree(hwnd, maxDepth: 6);
                uiaLines = uiaTree.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                foreach (var line in uiaTree.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    Console.WriteLine($"  {line}");
                Console.ResetColor();
                Console.WriteLine($"[HOVER] UIA: {uiaLines} element line(s)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HOVER] UIA dump failed: {ex.Message} → Gemini fallback");
                uiaLines = 0;
            }

            // ── Step 2: Gemini fallback if UIA is sparse ─────────────────────
            if (uiaLines >= MinUiaLines)
            {
                Console.WriteLine($"[HOVER] UIA sufficient ({uiaLines} lines) — skip Gemini");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[HOVER] UIA sparse ({uiaLines} lines) → Gemini screenshot analysis");
            Console.ResetColor();

            // Capture window screenshot
            string? screenshotPath = null;
            try
            {
                var bmp = ScreenCapture.CaptureWindow(hwnd);
                if (bmp != null)
                {
                    var dir = Path.Combine(DataDir, "output", "hover_scan");
                    Directory.CreateDirectory(dir);
                    screenshotPath = Path.Combine(dir, $"hover_{DateTime.Now:yyyyMMdd_HHmmss}_0x{hwnd:X8}.png");
                    bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                }
            }
            catch (Exception ex) { Console.WriteLine($"[HOVER] Screenshot failed: {ex.Message}"); }

            // Build Gemini prompt — JSON format for structured parsing
            var uiaSnippet = uiaTree != null
                ? string.Join("\n", uiaTree.Split('\n').Take(10))
                : "(none)";
            var prompt =
                $"A11Y analysis request. Respond ONLY with valid JSON — no markdown fences, no preamble.\n\n" +
                $"Window: \"{title}\" (hwnd=0x{hwnd:X8}), UIA elements: {uiaLines} (sparse → visual analysis needed)\n" +
                $"UIA snippet:\n{uiaSnippet}\n\n" +
                "Return this exact JSON shape:\n" +
                "{\n" +
                "  \"ui_type\": \"<app/widget category>\",\n" +
                "  \"summary\": \"<one sentence plain-text description>\",\n" +
                "  \"elements\": [{\"name\": \"\", \"type\": \"button|edit|list|label|etc\", \"position\": \"top-left|center|etc\"}],\n" +
                "  \"automation\": [{\"method\": \"WM_COMMAND|PostMessage|SendInput|UIA|CDP\", \"detail\": \"<specific hint>\"}],\n" +
                "  \"class_names\": [\"<Win32 class if guessable>\"],\n" +
                "  \"confidence\": \"high|medium|low\"\n" +
                "}";

            // --slack: result delivered to Slack so user can see structured JSON response
            var geminiArgs = screenshotPath != null
                ? new[] { "ask", "gemini", prompt, screenshotPath, "--slack" }
                : new[] { "ask", "gemini", prompt, "--slack" };

            Console.WriteLine($"[HOVER] Dispatching Gemini JSON query{(screenshotPath != null ? " + screenshot" : "")}...");
            // HoverGeminiSentinel → fixed tab key "ask+gemini+00A11EEE" so all hover scans share one tab
            EyeCmdPipeServer.DispatchBg(geminiArgs, HoverGeminiSentinel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[HOVER] Scan error: {ex.Message}");
        }
    }
}
