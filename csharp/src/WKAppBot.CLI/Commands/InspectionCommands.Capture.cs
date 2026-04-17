using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int CaptureCommand(string[] args)
    {
        using var focusSentinel = new FocusStealSentinel("a11y-screenshot");
        if (args.Length == 0)
            return Error(@"Usage: appbot capture <window-title> [-o output.png] [--form <form-id>] [--no-learn]
  --form <id>: Capture a specific MDI child form (brings it to front first).
  --no-learn:  Skip per-control experience DB learning.");

        string title = args[0];
        string? output = GetArgValue(args, "-o");
        string? formId = GetArgValue(args, "--form");
        bool skipLearn = args.Any(a => a.Equals("--no-learn", StringComparison.OrdinalIgnoreCase));

        // Ambiguity-guard + readiness Probe (magnifier) on the target.
        var win = ResolveA11yTarget(title, "screenshot");
        if (win == null) return 1;
        AppScanResult? scanResult = null;

        // Default save location: experience/{proc}/{class}/ (same dir as inspect/scan data)
        if (output == null)
        {
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint capPid);
            string? capProc = null;
            try { capProc = System.Diagnostics.Process.GetProcessById((int)capPid).ProcessName; } catch { }
            var safeProc = SanitizePathToken(capProc ?? "unknown");
            var safeClass = SanitizePathToken(win.ClassName);
            var expDir = Path.Combine(DataDir, "experience", safeProc, safeClass);
            Directory.CreateDirectory(expDir);
            output = Path.Combine(expDir, $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        }

        if (formId != null)
        {
            // Find MDI child form and bring to front before capture
            scanResult = AppScanner.Scan(win.Handle);
            var form = scanResult.Forms.FirstOrDefault(f =>
                f.FormId != null && f.FormId.Contains(formId, StringComparison.OrdinalIgnoreCase));
            if (form == null)
            {
                Console.WriteLine($"Form [{formId}] not found in \"{win.Title}\"");
                Console.WriteLine($"Available forms:");
                foreach (var f in scanResult.Forms)
                    Console.WriteLine($"  [{f.FormId}] {f.FormName}");
                return 1;
            }

            Console.WriteLine($"Capturing form: [{form.FormId}] {form.FormName}");

            // Bring MDI child to front using BringWindowToTop + SetWindowPos(TOPMOST)
            NativeMethods.BringWindowToTop(form.Handle);
            // Also use SetWindowPos SWP_NOACTIVATE to avoid stealing focus
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOP,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
            Thread.Sleep(200); // let repaint happen

            // Capture the MDI child window directly
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(form.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {Path.GetFullPath(output)} ({bmp.Width}x{bmp.Height})");
        }
        else
        {
            Console.WriteLine($"Capturing: {win}");
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {Path.GetFullPath(output)} ({bmp.Width}x{bmp.Height})");
        }

        // Per-control experience learning (auto when profile exists)
        if (!skipLearn)
        {
            try
            {
                NativeMethods.GetWindowThreadProcessId(win.Handle, out uint capPid);
                string? capProcName = null;
                try { capProcName = System.Diagnostics.Process.GetProcessById((int)capPid).ProcessName; } catch { }

                var profileStore = new ProfileStore();
                var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                    ?? (!string.IsNullOrEmpty(capProcName) ? profileStore.FindByMatch("", capProcName) : null);

                if (profileMatch != null)
                {
                    var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                    var expDb = new ExperienceDb(expDir);
                    scanResult ??= AppScanner.Scan(win.Handle);
                    var (forms, controls, screenshots) = AppScanner.QuickTouchControls(scanResult, expDb);

                    if (controls > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Error.WriteLine($"[CAPTURE] 경험DB 학습: {forms} forms, {controls} controls, {screenshots} new screenshots (profile={profileMatch.Value.name})");
                        Console.ResetColor();
                    }
                }
            }
            catch { /* best-effort */ }
        }

        return 0;
    }

    // -- ocr ----------------------------------------------------

    static int OcrCommand(string[] args)
    {
        using var focusSentinel = new FocusStealSentinel("a11y-ocr");
        if (args.Length == 0)
            return Error(@"Usage: wkappbot ocr <window-title|image.png> [--save] [-o output.txt]
  Runs OCR on a window screenshot or image file and prints all recognized text with coordinates.
  --save: Save annotated screenshot with OCR bounding boxes");

        string target = args[0];
        bool save = args.Contains("--save");
        string? outputFile = GetArgValue(args, "-o");

        System.Drawing.Bitmap screenshot;
        string sourceDesc;

        // Check if target is an image file
        if (File.Exists(target) && (target.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)))
        {
            screenshot = new System.Drawing.Bitmap(target);
            sourceDesc = Path.GetFileName(target);
        }
        else
        {
            // Treat as window title -- ambiguity-guard + readiness Probe (magnifier).
            var win = ResolveA11yTarget(target, "ocr");
            if (win == null) return 1;
            Console.WriteLine($"Capturing: {win}");
            screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            sourceDesc = win.Title;
        }

        using (screenshot)
        {
            Console.WriteLine($"Image: {screenshot.Width}x{screenshot.Height} -- {sourceDesc}");
            Console.WriteLine();

            // Run OCR
            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
            var result = ocr.RecognizeAll(screenshot).GetAwaiter().GetResult();

            Console.WriteLine($"-- OCR Results ({result.Words.Count} words) ----------------------");
            Console.WriteLine();

            // Print full text first
            if (!string.IsNullOrWhiteSpace(result.FullText))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[Full Text]");
                Console.ResetColor();
                // Limit to first 2000 chars
                var text = result.FullText.Length > 2000
                    ? result.FullText[..2000] + "..."
                    : result.FullText;
                Console.WriteLine(text);
                Console.WriteLine();
            }

            // Print words with coordinates (grouped by Y position -> lines)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Words with Coordinates]");
            Console.ResetColor();

            // Group words into lines by Y proximity (within 8px)
            var sortedWords = result.Words.OrderBy(w => w.Y).ThenBy(w => w.X).ToList();
            int lastLineY = -100;
            var lineWords = new List<WKAppBot.Vision.OcrWord>();

            foreach (var word in sortedWords)
            {
                if (Math.Abs(word.Y - lastLineY) > 8 && lineWords.Count > 0)
                {
                    // Print previous line
                    PrintOcrLine(lineWords);
                    lineWords.Clear();
                }
                lineWords.Add(word);
                lastLineY = word.Y;
            }
            if (lineWords.Count > 0) PrintOcrLine(lineWords);

            Console.WriteLine();
            Console.WriteLine($"Total: {result.Words.Count} words");

            // Save output
            if (outputFile != null)
            {
                var dir = Path.GetDirectoryName(outputFile);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(outputFile, result.FullText ?? "");
                Console.WriteLine($"Text saved: {outputFile}");
            }

            if (save)
            {
                var savePath = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                WKAppBot.Win32.Input.ScreenCapture.SaveToFile(screenshot, savePath);
                Console.WriteLine($"Screenshot saved: {savePath}");
            }
        }

        return 0;
    }

    static void PrintOcrLine(List<WKAppBot.Vision.OcrWord> words)
    {
        var lineText = string.Join(" ", words.Select(w => w.Text));
        var firstWord = words[0];
        Console.Write($"  Y={firstWord.Y,4} | ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(lineText);
        Console.ResetColor();
        Console.WriteLine($"  ({words.Count} words, x={firstWord.X}..{words.Last().X + words.Last().Width})");
    }

    // -- windows -- list all top-level windows --------------------
    // Usage: wkappbot windows [--filter <pattern>] [--process <name>] [--class <name>]

}
