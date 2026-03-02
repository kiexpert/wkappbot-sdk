using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: win-click — UIA focusless click (main) + physical click (fallback)
// Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl]
//
// Flow: FindWindow → UIA Focusless Click → (fallback) Foreground + Physical Click
// UIA Invoke/Toggle/Select = focusless! No foreground steal.
// --fl: Force focusless only (no fallback to physical click)
// --dbl/--right: Skip focusless attempt (UIA has no double-click/right-click concept)
internal partial class Program
{
    static int WinClickCommand(string[] args)
    {
        if (args.Length < 3)
            return Error("Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl]\n" +
                "  UIA focusless click (main) + physical click (fallback).\n" +
                "  --fl: Force focusless only (fail if UIA pattern not available)\n" +
                "  --dbl: Double-click (physical only)\n" +
                "  --right: Right-click (physical only)");

        bool isDouble = args.Any(a => a == "--dbl" || a == "--double");
        bool isRight = args.Any(a => a == "--right");
        bool forceFocusless = args.Any(a => a == "--fl" || a == "--focusless");

        if (!int.TryParse(args[1], out int relX) || !int.TryParse(args[2], out int relY))
            return Error("Invalid coordinates. Usage: wkappbot win-click <title> <x> <y>");

        // Resolve grap: "window/child" — '/' and '#' resolve to target window
        // Note: win-click doesn't use UIA scope, just window resolution
        var segments = args[0].Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0) return Error("Empty grap pattern");

        var found = WindowFinder.FindByTitle(segments[0]);
        if (found.Count == 0)
            return Error($"Window not found: \"{segments[0]}\"");

        var hWnd = found[0].Handle;
        // Resolve child window segments (Win32 only, no UIA needed for click)
        for (int si = 1; si < segments.Length; si++)
        {
            var child = WindowFinder.FindChildByPattern(hWnd, segments[si]);
            if (child != null) { hWnd = child.Handle; continue; }
            break; // Remaining segments ignored for win-click (coordinate-based)
        }
        var winInfo = WindowInfo.FromHwnd(hWnd);

        // Knowhow broadcast: show existing knowhow for this window/profile
        BroadcastInspectKnowhow(hWnd, winInfo.ClassName, null, winInfo.Title);

        // Get window rect + compute screen coordinates
        NativeMethods.GetWindowRect(hWnd, out var wRect);
        int screenX = wRect.Left + relX;
        int screenY = wRect.Top + relY;
        string clickType = isDouble ? "DblClick" : isRight ? "RightClick" : "Click";

        // ── Main path: UIA Focusless Click ──
        // Single left-click only (UIA has no double-click/right-click concept)
        if (!isDouble && !isRight)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                using var uia = new UiaLocator();
                var (ok, detail) = uia.TryFocuslessClickAtPoint(screenX, screenY, hWnd);

                if (ok)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[WIN] ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("FocuslessClick ");
                    Console.ResetColor();
                    Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) → {detail} ({sw.ElapsedMilliseconds}ms)");
                    return 0;
                }

                // Focusless not available
                if (forceFocusless)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[WIN] ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FocuslessFail ");
                    Console.ResetColor();
                    Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) → {detail}");
                    return 1;
                }

                // Fall through to physical click
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[WIN] UIA focusless unavailable ({detail}) → fallback to physical click");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                if (forceFocusless)
                    return Error($"Focusless click failed: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[WIN] UIA error ({ex.GetType().Name}) → fallback to physical click");
                Console.ResetColor();
            }
        }

        // ── Fallback: Foreground + OCR + Physical Click ──

        // Bring to foreground (required for CopyFromScreen on GPU-composited content)
        NativeMethods.SmartSetForegroundWindow(hWnd);
        Thread.Sleep(150);

        // Re-read rect after foreground (window position may have changed)
        NativeMethods.GetWindowRect(hWnd, out wRect);
        screenX = wRect.Left + relX;
        screenY = wRect.Top + relY;

        // OCR verification (best-effort)
        string ocrSnippet = "";
        string ocrInfo = "";
        try
        {
            int ocrW = 120, ocrH = 40;
            using var ocrBmp = ScreenCapture.CaptureScreenRegion(
                screenX - ocrW / 2, screenY - ocrH / 2, ocrW, ocrH);
            if (ocrBmp != null)
            {
                try
                {
                    string dbgDir = Path.Combine(Program.DataDir, "output", "win-click");
                    Directory.CreateDirectory(dbgDir);
                    ocrBmp.Save(Path.Combine(dbgDir,
                        $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}_{relX}_{relY}.png"), ImageFormat.Png);
                }
                catch { }

                if (!ScreenCapture.IsBlankBitmap(ocrBmp))
                {
                    using var ocr = new SimpleOcrAnalyzer();
                    var ocrResult = ocr.RecognizeAll(ocrBmp).GetAwaiter().GetResult();
                    string ocrText = ocrResult?.FullText?.Trim() ?? "";
                    ocrSnippet = ocrText.Length > 20 ? ocrText[..20] : ocrText;
                    if (!string.IsNullOrEmpty(ocrSnippet)) ocrInfo = $" OCR=\"{ocrSnippet}\"";
                }
            }
        }
        catch { }

        // UIA element detection → zoom rect
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

        string actionLabel = !string.IsNullOrEmpty(uiaLabel) ? uiaLabel
            : !string.IsNullOrEmpty(ocrSnippet) ? ocrSnippet
            : $"({relX},{relY})";

        using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, hWnd, $"win_{clickType.ToLower()}", actionLabel);

        // Physical click
        var swPhys = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            if (isDouble)
                MouseInput.DoubleClick(screenX, screenY);
            else if (isRight)
                MouseInput.RightClick(screenX, screenY);
            else
                MouseInput.Click(screenX, screenY);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"OK ({swPhys.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            zoom?.ShowPass($"{clickType} OK ({swPhys.ElapsedMilliseconds}ms)");

            // Desktop screenshot for diagnostics
            Thread.Sleep(300);
            try
            {
                int diagW = 600, diagH = 400;
                using var desktop = ScreenCapture.CaptureScreenRegion(
                    screenX - diagW / 2, screenY - diagH / 2, diagW, diagH);
                string diagDir = Path.Combine(Program.DataDir, "output", "win-click");
                Directory.CreateDirectory(diagDir);
                string diagPath = Path.Combine(diagDir,
                    $"diag_{DateTime.Now:yyyyMMdd_HHmmss}_{relX}_{relY}.png");
                desktop.Save(diagPath, ImageFormat.Png);
                Console.WriteLine($"[DIAG] Desktop screenshot: {diagPath}");
            }
            catch (Exception diagEx)
            {
                Console.WriteLine($"[DIAG] Desktop capture failed: {diagEx.Message}");
            }

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
