using System.Drawing;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: win-click — ProbeAtPoint 입력확보 → UIA focusless (main) → physical click (fallback)
// Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl]
//
// Flow: FindWindow → ProbeAtPoint (Z-order 전수조사 + 포커스리스 시도) → (fallback) Physical Click
// ProbeAtPoint: 좌표에 겹쳐있는 창들을 앞에서부터 포커스리스 시도, 방해꾼 dismiss, 타겟을 앞으로.
// --fl: Force focusless only (no fallback to physical click)
// --dbl/--right: Skip focusless attempt (UIA has no double-click/right-click concept)
internal partial class Program
{
    static int WinClickCommand(string[] args)
    {
        if (args.Length < 3)
            return Error("Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl]\n" +
                "  ProbeAtPoint 입력확보 + UIA focusless click (main) + physical click (fallback).\n" +
                "  --fl: Force focusless only (fail if UIA pattern not available)\n" +
                "  --dbl: Double-click (physical only)\n" +
                "  --right: Right-click (physical only)");

        bool isDouble = args.Any(a => a == "--dbl" || a == "--double");
        bool isRight = args.Any(a => a == "--right");
        bool forceFocusless = args.Any(a => a == "--fl" || a == "--focusless");

        if (!int.TryParse(args[1], out int relX) || !int.TryParse(args[2], out int relY))
            return Error("Invalid coordinates. Usage: wkappbot win-click <title> <x> <y>");

        // Resolve grap: "window/child" — '/' = Win32 child, '#' part ignored (coordinate-based click)
        var (win32Segments, _) = GrapHelper.SplitGrap(args[0]);
        if (win32Segments.Length == 0) return Error("Empty grap pattern");

        var found = WindowFinder.FindByTitle(win32Segments[0]);
        if (found.Count == 0)
            return Error($"Window not found: \"{win32Segments[0]}\"");

        var hWnd = found[0].Handle;
        // Walk Win32 children (segments before '#')
        for (int si = 1; si < win32Segments.Length; si++)
        {
            var child = WindowFinder.FindChildByPattern(hWnd, win32Segments[si]);
            if (child == null) return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            hWnd = child.Handle;
        }
        var winInfo = WindowInfo.FromHwnd(hWnd);

        // Knowhow broadcast: show existing knowhow for this window/profile
        BroadcastInspectKnowhow(hWnd, winInfo.ClassName, null, winInfo.Title);

        // Get window rect + compute screen coordinates
        NativeMethods.GetWindowRect(hWnd, out var wRect);
        int screenX = wRect.Left + relX;
        int screenY = wRect.Top + relY;
        string clickType = isDouble ? "DblClick" : isRight ? "RightClick" : "Click";

        // ── 돋보기 최우선: 입력확보 전에 무조건 띄움 ──
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
                bool elemFitsZoom = br.Width <= 300 && br.Height <= 200;
                zoomRect = elemFitsZoom ? br : new Rectangle(screenX - 60, screenY - 20, 120, 40);
                uiaLabel = string.IsNullOrEmpty(name) ? elem.ControlType : name;
                uiaInfo = $" [{elem.ControlType}] {br.Width}x{br.Height}";
                if (!elemFitsZoom) uiaInfo += $"(→click@{screenX},{screenY})";
                if (!string.IsNullOrEmpty(name)) uiaInfo += $" \"{name}\"";
                if (!string.IsNullOrEmpty(elem.AutomationId)) uiaInfo += $" aid={elem.AutomationId}";
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

        string actionLabel = !string.IsNullOrEmpty(uiaLabel) ? uiaLabel : $"({relX},{relY})";
        using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, hWnd, $"win_{clickType.ToLower()}", actionLabel);
        zoom?.UpdateStatus("입력확보 중...");

        // ── Main path: ProbeAtPoint 좌표 기반 입력확보 ──
        // Single left-click: 포커스리스 시도 (Z-order 전수조사 + 방해꾼 dismiss + 경로 정리)
        if (!isDouble && !isRight)
        {
            try
            {
                var readiness = CreateInputReadiness();
                var pointReport = readiness.ProbeAtPoint(new PointReadinessRequest
                {
                    ScreenX = screenX,
                    ScreenY = screenY,
                    TargetHwnd = hWnd,
                    IntendedAction = "click",
                    FocuslessOnly = forceFocusless,
                });

                // 이미 포커스리스 클릭 성공?
                if (pointReport.FocuslessClicked)
                {
                    // 전경 도둑질 감지 → 알림 팝업 (타겟 소유, 다시알림 체크박스)
                    if (pointReport.ForegroundStolen)
                    {
                        zoom?.ShowFail($"⚠fg {pointReport.ResolvedDetail}");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[WIN] ");
                        Console.Write("FocuslessClick ⚠fg ");
                        Console.ResetColor();

                        string? procName = null;
                        try
                        {
                            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                            procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName;
                        }
                        catch { }

                        FocuslessWarningOverlay.Show(
                            NativeMethods.GetAncestor(hWnd, NativeMethods.GA_ROOT),
                            pointReport.ResolvedDetail,
                            procName);
                    }
                    else
                    {
                        zoom?.ShowPass($"Focusless {pointReport.ResolvedDetail}");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[WIN] ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("FocuslessClick ");
                        Console.ResetColor();
                    }
                    Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) → {pointReport.ResolvedDetail}");
                    return 0;
                }

                // 포커스리스 전용 모드인데 실패?
                if (forceFocusless)
                {
                    zoom?.ShowFail($"No focusless: {pointReport.ResolvedDetail}");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[WIN] ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FocuslessFail ");
                    Console.ResetColor();
                    Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) → {pointReport.ResolvedDetail}");
                    return 1;
                }

                // Fall through to physical click
                zoom?.UpdateStatus("포커스리스 불가 → 물리클릭...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[WIN] Focusless unavailable ({pointReport.ResolvedDetail}) → fallback to physical click");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                if (forceFocusless)
                {
                    zoom?.ShowFail(ex.Message);
                    return Error($"ProbeAtPoint failed: {ex.Message}");
                }
                zoom?.UpdateStatus($"ProbeAtPoint 오류 → 물리클릭...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[WIN] ProbeAtPoint error ({ex.GetType().Name}) → fallback to physical click");
                Console.ResetColor();
            }
        }

        // ── Fallback: Foreground + Physical Click ──
        zoom?.UpdateStatus($"{clickType} 물리클릭...");
        NativeMethods.SmartSetForegroundWindow(hWnd);
        Thread.Sleep(150);

        // Re-read rect after foreground (window position may have changed)
        NativeMethods.GetWindowRect(hWnd, out wRect);
        screenX = wRect.Left + relX;
        screenY = wRect.Top + relY;

        Console.Write($"[WIN] {clickType} \"{winInfo.Title}\" at ({relX},{relY}) → screen ({screenX},{screenY}){uiaInfo}... ");

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
