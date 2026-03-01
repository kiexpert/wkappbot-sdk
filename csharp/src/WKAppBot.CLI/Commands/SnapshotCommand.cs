using System.Drawing;
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
    /// wkappbot snapshot <window-title> [--tag <name>] [--depth N] [--cid N] [--no-learn]
    /// Captures UIA tree, screenshot, and OCR text for a window in one shot.
    /// Saves to wkappbot.hq/output/snapshots/{tag}_{datetime}/
    /// Auto-learns per-control experience when a matching profile exists.
    /// </summary>
    static int SnapshotCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot snapshot <window-title> [--tag <name>] [--depth N] [--cid N] [--no-learn]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --tag <name>   Tag for output folder (default: snap)");
            Console.WriteLine("  --depth N      UIA tree depth (default: 8)");
            Console.WriteLine("  --cid N        Optional control-id hint (for experience file naming)");
            Console.WriteLine("  --no-learn     Skip per-control experience DB learning");
            Console.WriteLine();
            Console.WriteLine("Captures UIA tree + screenshot + OCR in one shot.");
            Console.WriteLine("Auto-learns per-control experience when a matching app profile exists.");
            return 1;
        }

        string title = args[0];
        string tag = GetArgValue(args, "--tag") ?? "snap";
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 8;
        int? cid = int.TryParse(GetArgValue(args, "--cid"), out var cidVal) ? cidVal : null;
        bool skipLearn = args.Any(a => a.Equals("--no-learn", StringComparison.OrdinalIgnoreCase));

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
        var snapshotsRoot = Path.Combine(DataDir, "output", "snapshots");
        Directory.CreateDirectory(snapshotsRoot);
        var outDir = Path.Combine(snapshotsRoot, $"{tag}_{DateTime.Now:yyyyMMdd_HHmmss}");
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
        string? screenshotPath = null;
        try
        {
            bmp = ScreenCapture.CaptureWindow(win.Handle);
            if (bmp != null && !ScreenCapture.IsBlankBitmap(bmp))
            {
                screenshotPath = Path.Combine(outDir, "screenshot.png");
                bmp.Save(screenshotPath, ImageFormat.Png);
                Console.WriteLine($"[SNAPSHOT] Screenshot: {screenshotPath} ({bmp.Width}x{bmp.Height})");
                savedCount++;

                // 2-1. Experience DB save (class-path + ring buffer + blend)
                try
                {
                    SaveExperienceSnapshot(winProcessName ?? "unknown", win.ClassName, cid, bmp, out var expCurrentPath, out var expBlendPath);
                    Console.WriteLine($"[SNAPSHOT] 경험 current 저장: {expCurrentPath} (ring 0..9, class={win.ClassName}{(cid.HasValue ? $", cid={cid.Value}" : "")})");
                    if (!string.IsNullOrWhiteSpace(expBlendPath))
                    {
                        Console.WriteLine($"[SNAPSHOT] 경험블렌드 저장 완료 (50% alpha, prev+curr): {expBlendPath} (AI 참조용)");
                    }
                    savedCount++;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[SNAPSHOT] 경험DB 저장 실패: {ex.Message}");
                    Console.ResetColor();
                }
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

        // 5. Per-control experience learning (auto when profile exists)
        if (!skipLearn)
        {
            try
            {
                var profileStore = new ProfileStore();
                var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                    ?? (!string.IsNullOrEmpty(winProcessName) ? profileStore.FindByMatch("", winProcessName) : null);

                if (profileMatch != null)
                {
                    var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                    var expDb = new ExperienceDb(expDir);
                    var scanResult = AppScanner.Scan(win.Handle);
                    var (forms, controls, screenshots) = AppScanner.QuickTouchControls(scanResult, expDb);

                    if (controls > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[SNAPSHOT] 경험DB 학습: {forms} forms, {controls} controls touched, {screenshots} new screenshots (profile={profileMatch.Value.name})");
                        Console.ResetColor();
                        savedCount++;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[SNAPSHOT] 경험DB: 매칭 프로파일 없음 (wkappbot scan --save로 프로파일 생성 필요)");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SNAPSHOT] 경험DB 학습 실패: {ex.Message}");
                Console.ResetColor();
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SNAPSHOT] Done! {savedCount} files saved to {outDir}");
        Console.ResetColor();
        return 0;
    }

    static void SaveExperienceSnapshot(string processName, string className, int? cid, Bitmap current, out string currentPath, out string? blendPath)
    {
        var safeProc = SanitizePathToken(processName);
        var safeClass = SanitizePathToken(className);
        var expDir = Path.Combine(DataDir, "experience", safeProc, safeClass);
        Directory.CreateDirectory(expDir);

        var cidSuffix = cid.HasValue ? $"_cid{cid.Value}" : "";
        var slots = Enumerable.Range(0, 10)
            .Select(i => new
            {
                Index = i,
                Path = Path.Combine(expDir, $"current_{i}{cidSuffix}.png")
            })
            .ToList();

        var missing = slots.FirstOrDefault(s => !File.Exists(s.Path));
        int targetIndex;
        if (missing != null) targetIndex = missing.Index;
        else
        {
            targetIndex = slots
                .OrderBy(s => File.GetLastWriteTimeUtc(s.Path))
                .First().Index;
        }

        var prevPath = slots
            .Where(s => File.Exists(s.Path) && s.Index != targetIndex)
            .OrderByDescending(s => File.GetLastWriteTimeUtc(s.Path))
            .Select(s => s.Path)
            .FirstOrDefault();

        currentPath = Path.Combine(expDir, $"current_{targetIndex}{cidSuffix}.png");
        current.Save(currentPath, ImageFormat.Png);

        blendPath = null;
        if (!string.IsNullOrWhiteSpace(prevPath))
        {
            using var prev = (Bitmap)Image.FromFile(prevPath);
            using var blend = CreateAlphaBlend50(prev, current);
            blendPath = Path.Combine(expDir, $"blend_{targetIndex}{cidSuffix}.png");
            blend.Save(blendPath, ImageFormat.Png);
        }
    }

    static string SanitizePathToken(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "unknown";
        var invalid = Path.GetInvalidFileNameChars();
        var chars = s.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray();
        return new string(chars);
    }

    static Bitmap CreateAlphaBlend50(Bitmap prev, Bitmap curr)
    {
        var w = Math.Min(prev.Width, curr.Width);
        var h = Math.Min(prev.Height, curr.Height);
        var dst = new Bitmap(w, h, PixelFormat.Format32bppArgb);

        using var g = Graphics.FromImage(dst);
        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        g.DrawImage(prev, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel);

        var cm = new ColorMatrix();
        cm.Matrix33 = 0.5f;
        using var ia = new ImageAttributes();
        ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        g.DrawImage(curr, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel, ia);

        return dst;
    }
}
