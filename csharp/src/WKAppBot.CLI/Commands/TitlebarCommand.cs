using System.Drawing;
using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

// partial class: titlebar button probe + click for ETK_CHILDFRAME MDI children
internal partial class Program
{
    /// <summary>
    /// Probe and click custom-drawn titlebar buttons in HTS MDI child frames.
    /// ETK_CHILDFRAME_WINDOW has owner-drawn titlebar buttons with no HWND/UIA.
    /// Discovery: WM_LBUTTONDOWN/UP with negative client-y -> PostMessage focusless click!
    /// </summary>
    static int TitlebarCommand(string[] args)
    {
        if (args.Length < 2)
            return Error(@"Usage: wkappbot titlebar <window-title> <form-id> [button-index]
  Probe and click custom-drawn titlebar buttons in HTS MDI child frames.
  These buttons have no HWND, no UIA, no class name -- they're owner-drawn in non-client area.
  Clicking uses PostMessage with negative client-y coordinates (fully focusless!).

Options:
  (no index)           List all detected titlebar buttons with OCR labels
  <index>              Click button by 0-based index (left to right)
  --ocr                OCR the titlebar region to label buttons
  --save               Save titlebar screenshot (3x upscaled) to output/
  --probe-step N       Hittest probe step in pixels (default: 2)

Examples:
  wkappbot titlebar ""투혼"" 1101              # probe and list buttons
  wkappbot titlebar ""투혼"" 1101 0            # click leftmost button
  wkappbot titlebar ""투혼"" 1101 --ocr --save # probe + OCR + save screenshot");

        string title = args[0];
        string targetFormId = args[1];
        int? clickIndex = args.Length >= 3 && int.TryParse(args[2], out var idx) ? idx : null;
        bool doOcr = args.Contains("--ocr");
        bool doSave = args.Contains("--save");
        int probeStep = int.TryParse(GetArgValue(args, "--probe-step"), out var ps) ? ps : 2;

        // Find target window
        var windows = WindowFinder.FindWindows(title);
        if (windows.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Scan to find MDI forms
        var scanResult = AppScanner.Scan(win.Handle);

        // Find the specific form by form_id
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found. Available forms:");
            Console.ResetColor();
            foreach (var f in scanResult.Forms.Where(f => f.FormId != null).GroupBy(f => f.FormId!))
            {
                var first = f.First();
                Console.WriteLine($"  [{first.FormId}] {first.FormName}");
            }
            return 1;
        }

        var formHwnd = targetForm.Handle;
        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName}");

        // Get window class
        var clsBuf = new StringBuilder(256);
        NativeMethods.GetClassNameW(formHwnd, clsBuf, 256);
        var className = clsBuf.ToString();
        Console.WriteLine($"Class: {className}");

        // Get window rect and client rect to calculate titlebar geometry
        NativeMethods.GetWindowRect(formHwnd, out var windowRect);
        NativeMethods.GetClientRect(formHwnd, out var clientRect);

        // Calculate non-client (titlebar) offset
        // Client area starts below the titlebar
        var clientOrigin = new POINT { X = 0, Y = 0 };
        NativeMethods.ClientToScreen(formHwnd, ref clientOrigin);

        int titlebarHeight = clientOrigin.Y - windowRect.Top;
        int titlebarLeft = windowRect.Left;
        int titlebarRight = windowRect.Right;
        int titlebarTop = windowRect.Top;

        Console.WriteLine($"Window: ({windowRect.Left},{windowRect.Top}) {windowRect.Right - windowRect.Left}x{windowRect.Bottom - windowRect.Top}");
        Console.WriteLine($"Client origin: ({clientOrigin.X},{clientOrigin.Y})");
        Console.WriteLine($"Titlebar height: {titlebarHeight}px");
        Console.WriteLine();

        if (titlebarHeight <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No titlebar detected (height=0). This window may not have a custom titlebar.");
            Console.ResetColor();
            return 1;
        }

        // Probe titlebar using WM_NCHITTEST at various x positions
        // y = middle of titlebar (screen coords)
        int probeY = titlebarTop + titlebarHeight / 2;
        var hitResults = new List<(int screenX, int hitTest)>();

        for (int x = titlebarLeft + 2; x < titlebarRight - 2; x += probeStep)
        {
            var lParam = NativeMethods.MakeScreenLParam(x, probeY);
            var result = (int)NativeMethods.SendMessageW(formHwnd, NativeMethods.WM_NCHITTEST, IntPtr.Zero, lParam);
            hitResults.Add((x, result));
        }

        // Find button regions: sequences of HTCLIENT hits (custom buttons return HTCLIENT)
        // Caption area returns HTCAPTION, system buttons return HTMINBUTTON/HTMAXBUTTON/HTCLOSE
        var buttons = new List<TitlebarButton>();
        int? regionStart = null;

        for (int i = 0; i < hitResults.Count; i++)
        {
            var (screenX, hit) = hitResults[i];
            bool isButton = (hit == NativeMethods.HTCLIENT);

            if (isButton && regionStart == null)
            {
                regionStart = screenX;
            }
            else if (!isButton && regionStart != null)
            {
                // End of button region
                buttons.Add(new TitlebarButton
                {
                    ScreenLeft = regionStart.Value,
                    ScreenRight = hitResults[i - 1].screenX,
                    ScreenY = probeY,
                    TitlebarHeight = titlebarHeight,
                    FormHwnd = formHwnd,
                    ClientOriginX = clientOrigin.X,
                    ClientOriginY = clientOrigin.Y,
                });
                regionStart = null;
            }
        }

        // Close last region
        if (regionStart != null)
        {
            buttons.Add(new TitlebarButton
            {
                ScreenLeft = regionStart.Value,
                ScreenRight = hitResults[^1].screenX,
                ScreenY = probeY,
                TitlebarHeight = titlebarHeight,
                FormHwnd = formHwnd,
                ClientOriginX = clientOrigin.X,
                ClientOriginY = clientOrigin.Y,
            });
        }

        // Filter out tiny regions (noise, < 8px wide)
        buttons = buttons.Where(b => b.Width >= 8).ToList();

        // Also detect standard NC buttons (HTMINBUTTON, HTMAXBUTTON, HTCLOSE, HTSYSMENU)
        var ncButtons = new List<(string name, int screenX)>();
        foreach (var (screenX, hit) in hitResults)
        {
            string? name = hit switch
            {
                NativeMethods.HTSYSMENU => "SysMenu",
                NativeMethods.HTMINBUTTON => "Minimize",
                NativeMethods.HTMAXBUTTON => "Maximize",
                NativeMethods.HTCLOSE => "Close",
                _ => null
            };
            if (name != null && (ncButtons.Count == 0 || ncButtons[^1].name != name))
                ncButtons.Add((name, screenX));
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"-- Titlebar Buttons ({buttons.Count} custom + {ncButtons.Count} standard) --------------------");
        Console.ResetColor();

        // Save titlebar screenshot (3x upscale) for button identification
        if (doSave || doOcr || clickIndex == null)
        {
            try
            {
                SaveTitlebarScreenshot(formHwnd, windowRect, titlebarHeight, buttons, doSave);
            }
            catch { }
        }

        // OCR titlebar region for button labels
        string[]? ocrLabels = null;
        if (doOcr || clickIndex == null)
        {
            try
            {
                ocrLabels = OcrTitlebarButtons(formHwnd, windowRect, titlebarHeight, buttons);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  (OCR failed: {ex.Message})");
                Console.ResetColor();
            }
        }

        // Display buttons
        for (int i = 0; i < buttons.Count; i++)
        {
            var btn = buttons[i];
            var label = ocrLabels != null && i < ocrLabels.Length ? ocrLabels[i] : "?";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  [{i}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" \"{label}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  screen=({btn.ScreenLeft}..{btn.ScreenRight}) width={btn.Width}px");
            Console.Write($"  client=({btn.ClientCenterX},{btn.ClientCenterY})");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Standard NC buttons
        foreach (var (name, screenX) in ncButtons)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"  [NC]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($" {name}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  screen=({screenX},...)");
            Console.ResetColor();
        }

        // Hittest distribution summary
        var hitDist = hitResults.GroupBy(h => h.hitTest).OrderByDescending(g => g.Count());
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  HitTest distribution: ");
        foreach (var g in hitDist)
        {
            var name = g.Key switch
            {
                NativeMethods.HTCLIENT => "CLIENT",
                NativeMethods.HTCAPTION => "CAPTION",
                NativeMethods.HTSYSMENU => "SYSMENU",
                NativeMethods.HTMINBUTTON => "MIN",
                NativeMethods.HTMAXBUTTON => "MAX",
                NativeMethods.HTCLOSE => "CLOSE",
                _ => $"HT{g.Key}"
            };
            Console.Write($"{name}={g.Count()} ");
        }
        Console.ResetColor();
        Console.WriteLine();

        // Click if index specified
        if (clickIndex != null)
        {
            if (clickIndex.Value < 0 || clickIndex.Value >= buttons.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  X Button index {clickIndex.Value} out of range (0..{buttons.Count - 1})");
                Console.ResetColor();
                return 1;
            }

            var target = buttons[clickIndex.Value];
            var label = ocrLabels != null && clickIndex.Value < ocrLabels.Length
                ? ocrLabels[clickIndex.Value] : $"#{clickIndex.Value}";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  -> Clicking [{clickIndex.Value}] \"{label}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" via PostMessage WM_LBUTTON at client=({target.ClientCenterX},{target.ClientCenterY})");
            Console.ResetColor();
            Console.WriteLine(" (focusless!)");

            // [ZOOM] Start zoom overlay for the titlebar button
            var btnScreenRect = new Rectangle(
                target.ScreenLeft,
                target.ScreenY - target.TitlebarHeight / 2,
                target.Width,
                target.TitlebarHeight);
            using var zoom = ClickZoomHelper.BeginFromRect(btnScreenRect, target.FormHwnd,
                "titlebar", label);

            // PostMessage WM_LBUTTONDOWN + WM_LBUTTONUP with negative client-y
            var lParam = NativeMethods.MakeLParam(target.ClientCenterX, target.ClientCenterY);
            NativeMethods.PostMessageW(target.FormHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)0x0001 /*MK_LBUTTON*/, lParam);
            System.Threading.Thread.Sleep(50);
            NativeMethods.PostMessageW(target.FormHwnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);

            // [ZOOM] Show result
            zoom?.ShowPass($"PostMessage click sent");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  v Click sent (focusless PostMessage)");
            Console.ResetColor();

            // ActionState for AppBotEye
            try
            {
                ActionState.Write(new ActionState
                {
                    Source = "titlebar",
                    ActionName = "titlebar_click",
                    ActionDetail = $"[{targetFormId}] button [{clickIndex.Value}] \"{label}\" (focusless)",
                    WindowTitle = win.Title,
                    Status = "clicked",
                });
            }
            catch { }
        }

        return 0;
    }

    /// <summary>Save titlebar screenshot with 3x upscale and button region markers.</summary>
    private static void SaveTitlebarScreenshot(
        IntPtr formHwnd, RECT windowRect, int titlebarHeight,
        List<TitlebarButton> buttons, bool saveToDisk)
    {
        int width = windowRect.Right - windowRect.Left;
        if (width <= 0 || titlebarHeight <= 0) return;

        using var fullBitmap = ScreenCapture.CaptureWindow(formHwnd);
        if (fullBitmap == null) return;

        using var titlebarBmp = ScreenCapture.CropRegion(fullBitmap, 0, 0, width, titlebarHeight);
        if (titlebarBmp == null) return;

        // 3x upscale for visibility
        int scale = 3;
        using var scaled = new Bitmap(titlebarBmp.Width * scale, titlebarBmp.Height * scale);
        using (var g = Graphics.FromImage(scaled))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.DrawImage(titlebarBmp, 0, 0, scaled.Width, scaled.Height);

            // Draw button region markers
            using var pen = new Pen(Color.Lime, 2);
            using var font = new Font("Consolas", 10);
            using var brush = new SolidBrush(Color.Lime);
            for (int i = 0; i < buttons.Count; i++)
            {
                var btn = buttons[i];
                int bx = (btn.ScreenLeft - windowRect.Left) * scale;
                int bw = btn.Width * scale;
                g.DrawRectangle(pen, bx, 0, bw, scaled.Height - 1);
                g.DrawString($"{i}", font, brush, bx + 2, 2);
            }
        }

        if (saveToDisk)
        {
            var outDir = Path.Combine(DataDir, "output", "titlebar");
            Directory.CreateDirectory(outDir);
            var outPath = Path.Combine(outDir, $"titlebar_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            scaled.Save(outPath);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  Saved: {outPath} ({scaled.Width}x{scaled.Height})");
            Console.ResetColor();
        }
    }

    /// <summary>OCR the titlebar region to extract button labels.</summary>
    private static string[] OcrTitlebarButtons(
        IntPtr formHwnd, RECT windowRect, int titlebarHeight,
        List<TitlebarButton> buttons)
    {
        // Capture the titlebar region using PrintWindow
        int width = windowRect.Right - windowRect.Left;
        if (width <= 0 || titlebarHeight <= 0)
            return Array.Empty<string>();

        using var fullBitmap = ScreenCapture.CaptureWindow(formHwnd);
        if (fullBitmap == null)
            return Array.Empty<string>();

        // Crop titlebar region (top portion of the window bitmap)
        using var titlebarBmp = ScreenCapture.CropRegion(fullBitmap, 0, 0, width, titlebarHeight);
        if (titlebarBmp == null)
            return Array.Empty<string>();

        // OCR the titlebar (3x upscale for small titlebar text)
        using var ocr = new SimpleOcrAnalyzer();
        var ocrResult = ocr.RecognizeAll(titlebarBmp).GetAwaiter().GetResult();
        if (ocrResult == null || ocrResult.Words.Count == 0)
            return buttons.Select(_ => "?").ToArray();

        // Match OCR words to button regions by X-coordinate overlap
        var labels = new string[buttons.Count];
        for (int i = 0; i < buttons.Count; i++)
        {
            var btn = buttons[i];
            // Convert button screen coords to bitmap-local coords
            int btnLocalLeft = btn.ScreenLeft - windowRect.Left;
            int btnLocalRight = btn.ScreenRight - windowRect.Left;

            // Find OCR words that overlap with this button's X range
            var matchWords = ocrResult.Words
                .Where(w => w.X + w.Width > btnLocalLeft && w.X < btnLocalRight
                         && w.Y < titlebarHeight) // within titlebar height
                .OrderBy(w => w.X)
                .Select(w => w.Text)
                .ToList();

            labels[i] = matchWords.Count > 0 ? string.Join("", matchWords) : "?";
        }

        return labels;
    }

    /// <summary>Represents a custom-drawn titlebar button detected via WM_NCHITTEST probe.</summary>
    private class TitlebarButton
    {
        public int ScreenLeft { get; init; }
        public int ScreenRight { get; init; }
        public int ScreenY { get; init; }
        public int TitlebarHeight { get; init; }
        public IntPtr FormHwnd { get; init; }
        public int ClientOriginX { get; init; }
        public int ClientOriginY { get; init; }

        public int Width => ScreenRight - ScreenLeft;
        public int ScreenCenterX => (ScreenLeft + ScreenRight) / 2;

        /// <summary>Client-space X (relative to client origin).</summary>
        public int ClientCenterX => ScreenCenterX - ClientOriginX;

        /// <summary>Client-space Y (NEGATIVE = above client area = in titlebar).</summary>
        public int ClientCenterY => ScreenY - ClientOriginY;
    }
}
