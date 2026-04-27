using System.Drawing;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class WinClickCommand (Program): win-click -- maximally focusless coordinate-based click.
// Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl] [--abs] [--dismiss-blocker]
//
// 3-tier focusless cascade (single left-click):
//   1. UIA Invoke/Toggle/Select/ExpandCollapse at point  (ProbeAtPoint)
//   2. PostMessage WM_LBUTTONDOWN/UP to deepest child    (TryPostMessageClick)
//   3. Physical click -- Probe() yield popup, SmartSetForeground
//
// --dbl / --right: UIA has no dbl/right concept, so start at tier 2 (PostMessage), then tier 3.
// --fl: stop at tier 2 (no physical click).
internal partial class Program
{
    static int WinClickCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot win-click <window-title> <x> <y> [--dbl] [--right] [--fl] [--abs] [--dismiss-blocker]\n" +
                "  x y = window-relative coords (from window top-left including title bar).\n" +
                "  --abs: treat x y as absolute screen coordinates instead.\n" +
                "  --dismiss-blocker: auto-dismiss #32770 dialog blocking the click, then retry.\n" +
                "  --fl: Force focusless only (no physical click fallback)\n" +
                "  --dbl: Double-click\n" +
                "  --right: Right-click");

        bool isDouble = args.Any(a => a == "--dbl" || a == "--double");
        bool isRight = args.Any(a => a == "--right");
        bool forceFocusless = args.Any(a => a == "--fl" || a == "--focusless");
        bool absCoords = args.Any(a => a == "--abs" || a == "--absolute");
        bool dismissBlocker = args.Any(a => a == "--dismiss-blocker" || a == "--dismiss");

        // Support both positional (<title> <x> <y>) and named (--x N --y N) coordinate syntax
        int relX, relY;
        string? xStr = GetArgValue(args, "--x") ?? GetArgValue(args, "--X");
        string? yStr = GetArgValue(args, "--y") ?? GetArgValue(args, "--Y");
        if (xStr != null || yStr != null)
        {
            if (!int.TryParse(xStr, out relX) || !int.TryParse(yStr, out relY))
                return Error("Invalid coordinates. Usage: wkappbot win-click <title> --x N --y N");
        }
        else
        {
            if (args.Length < 3 || !int.TryParse(args[1], out relX) || !int.TryParse(args[2], out relY))
                return Error("Invalid coordinates. Usage: wkappbot win-click <title> <x> <y> or --x N --y N");
        }

        var (win32Segments, _) = GrapHelper.SplitGrap(args[0]);
        if (win32Segments.Length == 0) return Error("Empty grap pattern");

        var found = WindowFinder.FindWindows(win32Segments[0]);
        if (found.Count == 0)
            return Error($"Window not found: \"{win32Segments[0]}\"");

        var hWnd = found[0].Handle;
        for (int si = 1; si < win32Segments.Length; si++)
        {
            var child = WindowFinder.FindChildByPattern(hWnd, win32Segments[si]);
            if (child == null) return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            hWnd = child.Handle;
        }
        var winInfo = WindowInfo.FromHwnd(hWnd);

        BroadcastInspectKnowhow(hWnd, winInfo.ClassName, null, winInfo.Title);

        NativeMethods.GetWindowRect(hWnd, out var wRect);
        int screenX = absCoords ? relX : wRect.Left + relX;
        int screenY = absCoords ? relY : wRect.Top + relY;
        string clickType = isDouble ? "DblClick" : isRight ? "RightClick" : "Click";

        // -- 돋보기 --
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
                if (!elemFitsZoom) uiaInfo += $"(->click@{screenX},{screenY})";
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

        // ── Tier 1: UIA 포커스리스 (단순 좌클릭만) ────────────────────────
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

                if (pointReport.FocuslessClicked)
                {
                    if (pointReport.ForegroundStolen)
                    {
                        zoom?.ShowFail($"!fg {pointReport.ResolvedDetail}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[WIN] FocuslessClick !fg ");
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
                        zoom?.ShowPass($"UIA {pointReport.ResolvedDetail}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[WIN] ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("FocuslessClick(UIA) ");
                        Console.ResetColor();
                    }
                    Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) -> {pointReport.ResolvedDetail}");
                    return 0;
                }

                if (forceFocusless)
                {
                    // --fl: tier 2 (PostMessage) before giving up
                    zoom?.UpdateStatus("UIA 불가 -> PostMessage 시도...");
                }
                else
                {
                    zoom?.UpdateStatus("UIA 불가 -> PostMessage 시도...");
                }
            }
            catch (Exception ex)
            {
                if (forceFocusless)
                    zoom?.UpdateStatus($"UIA 오류 -> PostMessage 시도...");
                else
                    zoom?.UpdateStatus($"UIA 오류 -> PostMessage 시도...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Error.WriteLine($"[WIN] ProbeAtPoint error ({ex.GetType().Name}) -> tier 2");
                Console.ResetColor();
            }
        }

        // ── Tier 2: PostMessage WM_LBUTTONDOWN/UP (포커스리스) ─────────────
        // UIA 패턴 없는 MFC/GDI 컨트롤에 효과적. 포커스 강탈 없음.
        {
            zoom?.UpdateStatus($"PostMessage {clickType}...");
            var (pmOk, pmDetail) = TryPostMessageClick(hWnd, screenX, screenY, isDouble, isRight);
            if (pmOk)
            {
                zoom?.ShowPass($"PostMessage {pmDetail}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[WIN] ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"FocuslessClick(PostMsg) ");
                Console.ResetColor();
                Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) -> {pmDetail}");
                return 0;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Error.WriteLine($"[WIN] PostMessage failed ({pmDetail}) -> tier 3");
            Console.ResetColor();

            if (forceFocusless)
            {
                zoom?.ShowFail($"No focusless: {pmDetail}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[WIN] ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("FocuslessFail ");
                Console.ResetColor();
                Console.WriteLine($"\"{winInfo.Title}\" at ({relX},{relY}) -> {pmDetail}");
                return 1;
            }
        }

        // ── Tier 3: 물리 클릭 (포커스 강탈 필요, 팝업 가능) ───────────────
        zoom?.UpdateStatus("물리클릭...");
        var physReadiness = CreateInputReadiness();
        var physReport = physReadiness.Probe(new InputReadinessRequest
        {
            TargetHwnd     = hWnd,
            IntendedAction = isDouble ? "dbl-click" : isRight ? "right-click" : "click",
        });
        if (physReport.ActiveBlocker != null)
        {
            var blocker = physReport.ActiveBlocker;
            if (dismissBlocker)
            {
                Console.Error.WriteLine($"[WIN] blocker detected: \"{blocker.Title}\" -- attempting auto-dismiss");
                zoom?.UpdateStatus($"dismiss blocker: {blocker.Title}");
                physReadiness.TryDismissBlocker(hWnd, blocker);
                Thread.Sleep(400);
                var physReport2 = CreateInputReadiness().Probe(new InputReadinessRequest
                {
                    TargetHwnd     = hWnd,
                    IntendedAction = isDouble ? "dbl-click" : isRight ? "right-click" : "click",
                });
                if (physReport2.ActiveBlocker != null)
                {
                    zoom?.ShowFail($"dismiss failed: {physReport2.ActiveBlocker.Title}");
                    return Error($"[WIN] blocker persists after dismiss: \"{physReport2.ActiveBlocker.Title}\"");
                }
                zoom?.UpdateStatus($"{clickType} 물리클릭...");
            }
            else
            {
                bool isTransparentOverlay = string.IsNullOrEmpty(blocker.Title) && blocker.ClassName != "#32770";
                if (isTransparentOverlay)
                {
                    Console.WriteLine($"[WIN] empty-title overlay [{blocker.ClassName}] auto-dismissed");
                    physReadiness.TryDismissBlocker(hWnd, blocker);
                    Thread.Sleep(200);
                }
                else
                {
                    zoom?.ShowFail($"blocker: {blocker.Title}");
                    return Error($"[WIN] physical click blocked by: \"{blocker.Title}\" (use --dismiss-blocker to auto-dismiss)");
                }
            }
        }

        if (physReport.UserYieldRequested && !physReport.UserYieldConfirmed)
        {
            zoom?.ShowFail("포커스양보 거부됨");
            return Error("[WIN] physical click blocked: user denied focus yield");
        }

        zoom?.UpdateStatus($"{clickType} 물리클릭...");
        if (!physReport.FocusPreAcquired)
            NativeMethods.SmartSetForegroundWindow(hWnd);
        Thread.Sleep(150);

        NativeMethods.GetWindowRect(hWnd, out wRect);
        screenX = absCoords ? relX : wRect.Left + relX;
        screenY = absCoords ? relY : wRect.Top + relY;

        Console.Write($"[WIN] {clickType} \"{winInfo.Title}\" at ({relX},{relY}) -> screen ({screenX},{screenY}){uiaInfo}... ");

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

    /// <summary>
    /// Tier-2 focusless click: PostMessage WM_LBUTTON*/WM_RBUTTON* to the deepest visible
    /// child window at the given screen point. No focus steal.
    /// Returns (true, detail) on success, (false, reason) on failure.
    /// </summary>
    static (bool ok, string detail) TryPostMessageClick(
        IntPtr hWnd, int screenX, int screenY, bool isDouble = false, bool isRight = false)
    {
        try
        {
            // Find deepest child at screen point within hWnd
            var clientPtForRoot = new POINT { X = screenX, Y = screenY };
            NativeMethods.ScreenToClient(hWnd, ref clientPtForRoot);

            // Drill into deepest non-invisible/non-disabled child
            var targetHwnd = hWnd;
            var childHwnd = NativeMethods.ChildWindowFromPointEx(
                hWnd, clientPtForRoot,
                NativeMethods.CWP_SKIPINVISIBLE | NativeMethods.CWP_SKIPDISABLED);
            if (childHwnd != IntPtr.Zero && childHwnd != hWnd)
                targetHwnd = childHwnd;

            // Client coords relative to target child
            var clientPt = new POINT { X = screenX, Y = screenY };
            NativeMethods.ScreenToClient(targetHwnd, ref clientPt);
            var lParam = (IntPtr)(((clientPt.Y & 0xFFFF) << 16) | (clientPt.X & 0xFFFF));

            string childDesc = targetHwnd != hWnd
                ? $"child 0x{targetHwnd:X} [{WindowFinder.GetClassName(targetHwnd)}]"
                : "root";

            if (isDouble)
            {
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)NativeMethods.MK_LBUTTON, lParam);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONUP,   (IntPtr)0, lParam);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONDBLCLK, (IntPtr)NativeMethods.MK_LBUTTON, lParam);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONUP,   (IntPtr)0, lParam);
            }
            else if (isRight)
            {
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_RBUTTONDOWN, (IntPtr)NativeMethods.MK_RBUTTON, lParam);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_RBUTTONUP,   (IntPtr)0, lParam);
            }
            else
            {
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)NativeMethods.MK_LBUTTON, lParam);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_LBUTTONUP,   (IntPtr)0, lParam);
            }

            Thread.Sleep(50);
            return (true, $"PostMsg at client ({clientPt.X},{clientPt.Y}) -> {childDesc}");
        }
        catch (Exception ex)
        {
            return (false, $"PostMsg error: {ex.Message}");
        }
    }
}
