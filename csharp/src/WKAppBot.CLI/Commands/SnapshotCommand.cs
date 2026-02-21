using System.Drawing.Imaging;
using System.Text;
using System.Text.Json;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: snapshot command — one-shot UIA tree + screenshot + OCR capture
internal partial class Program
{
    /// <summary>
    /// wkappbot snapshot <window-title> [--tag <name>] [--depth N]
    /// Captures UIA tree, screenshot, and OCR text for a window in one shot.
    /// Saves to wkappbot.hq/output/snapshots/{tag}_{datetime}/
    /// </summary>
    static int SnapshotCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot snapshot <window-title> [--tag <name>] [--depth N]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --tag <name>   Tag for output folder (default: snap)");
            Console.WriteLine("  --depth N      UIA tree depth (default: 8)");
            Console.WriteLine();
            Console.WriteLine("Captures UIA tree + screenshot + OCR in one shot.");
            Console.WriteLine("Use during rate limit to record Claude Desktop's UIA structure.");
            return 1;
        }

        string title = args[0];
        string tag = GetArgValue(args, "--tag") ?? "snap";
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 8;

        // Find window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
            return Error($"No window found matching: \"{title}\"");

        var win = windows[0];
        // Get process info from window handle
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint winPid);
        string? winProcessName = null;
        try { winProcessName = System.Diagnostics.Process.GetProcessById((int)winPid).ProcessName; } catch { }
        Console.WriteLine($"[SNAPSHOT] Window: \"{win.Title}\" (class={win.ClassName}, pid={winPid})");

        // Create output directory
        var outDir = Path.Combine(DataDir, "output", "snapshots", $"{tag}_{DateTime.Now:yyyyMMdd_HHmmss}");
        Directory.CreateDirectory(outDir);

        int savedCount = 0;

        // 1. UIA tree dump
        try
        {
            using var uia = new UiaLocator();
            var tree = uia.DumpTree(win.Handle, depth);
            var uiaPath = Path.Combine(outDir, "uia_tree.txt");
            File.WriteAllText(uiaPath, tree, Encoding.UTF8);
            Console.WriteLine($"[SNAPSHOT] UIA tree: {uiaPath} ({tree.Length} chars)");
            savedCount++;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SNAPSHOT] UIA tree failed: {ex.Message}");
            Console.ResetColor();
        }

        // 2. Screenshot
        System.Drawing.Bitmap? bmp = null;
        try
        {
            bmp = ScreenCapture.CaptureWindow(win.Handle);
            if (bmp != null && !ScreenCapture.IsBlankBitmap(bmp))
            {
                var screenshotPath = Path.Combine(outDir, "screenshot.png");
                bmp.Save(screenshotPath, ImageFormat.Png);
                Console.WriteLine($"[SNAPSHOT] Screenshot: {screenshotPath} ({bmp.Width}x{bmp.Height})");
                savedCount++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[SNAPSHOT] Screenshot: blank or failed");
                Console.ResetColor();
                bmp?.Dispose();
                bmp = null;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SNAPSHOT] Screenshot failed: {ex.Message}");
            Console.ResetColor();
        }

        // 3. OCR
        if (bmp != null)
        {
            try
            {
                var ocr = new SimpleOcrAnalyzer();
                var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                var ocrPath = Path.Combine(outDir, "ocr.txt");

                var sb = new StringBuilder();
                sb.AppendLine($"── OCR Full Text ({ocrResult.Words.Count} words) ──");
                sb.AppendLine(ocrResult.FullText);
                sb.AppendLine();
                sb.AppendLine("── Words (x, y, w, h, text) ──");
                foreach (var word in ocrResult.Words)
                {
                    sb.AppendLine($"  ({word.X,4}, {word.Y,4}, {word.Width,3}, {word.Height,3}) \"{word.Text}\"");
                }

                File.WriteAllText(ocrPath, sb.ToString(), Encoding.UTF8);
                Console.WriteLine($"[SNAPSHOT] OCR: {ocrPath} ({ocrResult.Words.Count} words)");
                savedCount++;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SNAPSHOT] OCR failed: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                bmp.Dispose();
            }
        }

        // 4. info.json metadata
        try
        {
            NativeMethods.GetWindowRect(win.Handle, out var rect);
            var info = new
            {
                window_title = win.Title,
                class_name = win.ClassName,
                process_id = (int)winPid,
                process_name = winProcessName ?? "unknown",
                timestamp = DateTime.Now.ToString("O"),
                rect = new { left = rect.Left, top = rect.Top, right = rect.Right, bottom = rect.Bottom },
                width = rect.Right - rect.Left,
                height = rect.Bottom - rect.Top,
                tag,
                depth
            };
            var jsonPath = Path.Combine(outDir, "info.json");
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(info, new JsonSerializerOptions { WriteIndented = true }));
            savedCount++;
        }
        catch { }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SNAPSHOT] Done! {savedCount} files saved to {outDir}");
        Console.ResetColor();
        return 0;
    }
}
