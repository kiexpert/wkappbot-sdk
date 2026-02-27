using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: win-click — Physical click/dblclick at screen coordinates with zoom overlay + OCR verification
// Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right]
//
// Flow: FindWindow → Foreground → OCR → Zoom → Click → DiagScreenshot
// Key: Foreground MUST come before OCR/Zoom because CopyFromScreen needs the window visible.
// Chrome/Electron GPU-composited content is invisible to PrintWindow → CopyFromScreen is the only reliable path.
internal partial class Program
{
    static int WinClickCommand(string[] args)
    {
        if (args.Length < 3)
            return Error("Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right]\n  Physical click at window-relative coordinates with zoom overlay.");

        string title = args[0];
        bool isDouble = args.Any(a => a == "--dbl" || a == "--double");
        bool isRight = args.Any(a => a == "--right");

        if (!int.TryParse(args[1], out int relX) || !int.TryParse(args[2], out int relY))
            return Error("Invalid coordinates. Usage: wkappbot win-click <title> <x> <y>");

        // Find window
        var found = WindowFinder.FindByTitle(title);
        if (found.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var winInfo = found[0];
        var hWnd = winInfo.Handle;

        // Get window rect + compute screen coordinates
        NativeMethods.GetWindowRect(hWnd, out var wRect);
        int screenX = wRect.Left + relX;
        int screenY = wRect.Top + relY;
        string clickType = isDouble ? "DblClick" : isRight ? "RightClick" : "Click";

        // ── Step 1: Bring window to foreground FIRST ──
        // Essential for CopyFromScreen to capture correct content (GPU-composited areas)
        NativeMethods.SmartSetForegroundWindow(hWnd);
        Thread.Sleep(150); // wait for window to be fully painted in foreground

        // Re-read rect after foreground (window position may have changed)
        NativeMethods.GetWindowRect(hWnd, out wRect);
        screenX = wRect.Left + relX;
        screenY = wRect.Top + relY;

        // ── Step 2: OCR verification — identify text at click point ──
        string ocrText = "";
        try
        {
            int ocrW = 120, ocrH = 40;
            int ocrScrX = screenX - ocrW / 2;
            int ocrScrY = screenY - ocrH / 2;
            // Use CopyFromScreen directly (reliable for GPU-composited Electron/Chrome)
            using var ocrBmp = ScreenCapture.CaptureScreenRegion(ocrScrX, ocrScrY, ocrW, ocrH);
            if (ocrBmp != null)
            {
                // Save debug capture
                try
                {
                    string dbgDir = Path.Combine(Program.DataDir, "output", "win-click");
                    Directory.CreateDirectory(dbgDir);
                    string ts2 = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    ocrBmp.Save(Path.Combine(dbgDir, $"ocr_{ts2}_{relX}_{relY}.png"), ImageFormat.Png);
                }
                catch { }

                if (!ScreenCapture.IsBlankBitmap(ocrBmp))
                {
                    using var ocr = new SimpleOcrAnalyzer();
                    var ocrResult = ocr.RecognizeAll(ocrBmp).GetAwaiter().GetResult();
                    ocrText = ocrResult?.FullText?.Trim() ?? "";
                }
            }
        }
        catch { /* OCR is best-effort */ }

        string ocrSnippet = ocrText.Length > 20 ? ocrText[..20] : ocrText;
        string ocrInfo = string.IsNullOrEmpty(ocrText) ? "" : $" OCR=\"{ocrSnippet}\"";

        // ── Step 3: UIA element detection at click point → accurate zoom rect ──
        // Uses UiaLocator.GetElementAtPointInWindow() which walks the UIA tree recursively
        // (FindDeepestAtPoint) instead of just FromPoint — this digs into Chrome/Electron's
        // UIA tree to find actual sub-elements (e.g., "입력하세요" prompt, tabs, buttons)
        // that FromPoint alone can't reach.
        Rectangle zoomRect;
        string uiaLabel = "";
        string uiaInfo = "";
        try
        {
            using var uia = new UiaLocator();
            var elem = uia.GetElementAtPointInWindow(screenX, screenY, hWnd);
            if (elem != null && elem.BoundsW > 0 && elem.BoundsH > 0)
            {
                var br = new Rectangle(elem.BoundsX, elem.BoundsY, elem.BoundsW, elem.BoundsH);
                string name = elem.Name ?? "";
                if (name.Length > 30) name = name[..30];

                // Use UIA rect if it represents a specific sub-element (not the whole window).
                // Heuristic: if element covers >50% of window area, it's too coarse → fallback.
                double elemArea = (double)br.Width * br.Height;
                double winArea = (double)wRect.Width * wRect.Height;
                bool tooCoarse = winArea > 0 && elemArea > winArea * 0.5;

                if (!tooCoarse)
                {
                    zoomRect = br;
                    uiaLabel = string.IsNullOrEmpty(name) ? elem.ControlType : name;
                    uiaInfo = $" [{elem.ControlType}] {br.Width}x{br.Height}";
                    if (!string.IsNullOrEmpty(name)) uiaInfo += $" \"{name}\"";
                    if (!string.IsNullOrEmpty(elem.AutomationId)) uiaInfo += $" aid={elem.AutomationId}";
                }
                else
                {
                    // Element too coarse or zero → fall back to fixed 120x40
                    zoomRect = new Rectangle(screenX - 60, screenY - 20, 120, 40);
                    uiaInfo = $" [{elem.ControlType}] {br.Width}x{br.Height}(coarse→120x40)";
                }
            }
            else
            {
                zoomRect = new Rectangle(screenX - 60, screenY - 20, 120, 40);
            }
        }
        catch
        {
            zoomRect = new Rectangle(screenX - 60, screenY - 20, 120, 40);
        }

        Console.Write($"[WIN] {clickType} \"{winInfo.Title}\" at ({relX},{relY}) → screen ({screenX},{screenY}){ocrInfo}{uiaInfo}... ");

        // Best label: UIA name > OCR snippet > coordinates
        string actionLabel = !string.IsNullOrEmpty(uiaLabel) ? uiaLabel
            : !string.IsNullOrEmpty(ocrSnippet) ? ocrSnippet
            : $"({relX},{relY})";

        using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, hWnd, $"win_{clickType.ToLower()}", actionLabel);

        // ── Step 4: Physical click ──
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            if (isDouble)
                MouseInput.DoubleClick(screenX, screenY);
            else if (isRight)
                MouseInput.RightClick(screenX, screenY);
            else
                MouseInput.Click(screenX, screenY);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"OK ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            zoom?.ShowPass($"{clickType} OK ({sw.ElapsedMilliseconds}ms)");

            // ── Desktop screenshot for diagnostics (captures zoom overlay too) ──
            Thread.Sleep(300);
            try
            {
                int diagW = 600, diagH = 400;
                int diagX = screenX - diagW / 2;
                int diagY = screenY - diagH / 2;
                using var desktop = ScreenCapture.CaptureScreenRegion(diagX, diagY, diagW, diagH);
                string diagDir = Path.Combine(Program.DataDir, "output", "win-click");
                Directory.CreateDirectory(diagDir);
                string ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string diagPath = Path.Combine(diagDir, $"diag_{ts}_{relX}_{relY}.png");
                desktop.Save(diagPath, ImageFormat.Png);
                Console.WriteLine($"[DIAG] Desktop screenshot: {diagPath}");
            }
            catch (Exception diagEx)
            {
                Console.WriteLine($"[DIAG] Desktop capture failed: {diagEx.Message}");
            }

            // Hold zoom visible so user can see what was clicked
            Thread.Sleep(1200);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"FAIL: {ex.Message}");
            Console.ResetColor();
            zoom?.ShowFail(ex.Message);
            return 1;
        }

        return 0;
    }
}
