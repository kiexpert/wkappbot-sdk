using System.Diagnostics;
using System.Text;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public sealed partial class InputReadiness
{
    // -- Private: 윈도우 스택 수집 --------------------------------

    private List<WindowAtPoint> CollectWindowStack(int screenX, int screenY, IntPtr targetHwnd, uint targetPid)
    {
        var stack = new List<WindowAtPoint>();
        int zOrder = 0;

        // WindowFromPoint -> 최상단 leaf 윈도우
        var topLeaf = NativeMethods.WindowFromPoint(new POINT { X = screenX, Y = screenY });
        var topRoot = topLeaf != IntPtr.Zero
            ? NativeMethods.GetAncestor(topLeaf, NativeMethods.GA_ROOT)
            : IntPtr.Zero;

        // EnumWindows: Z-order 순 (앞->뒤) -- 해당 좌표를 포함하는 visible top-level 윈도우
        // 우리 프로세스(앱봇) 윈도우는 제외 (돋보기/Eye/경고팝업 등)
        var myPid = (uint)Environment.ProcessId;
        var collected = new List<(IntPtr handle, int z)>();
        int enumZ = 0;
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint wPid);
            if (wPid == myPid) return true; // skip our own windows (zoom/eye/warning)
            NativeMethods.GetWindowRect(hWnd, out var r);
            if (screenX >= r.Left && screenX <= r.Right && screenY >= r.Top && screenY <= r.Bottom)
            {
                collected.Add((hWnd, enumZ++));
            }
            return true;
        }, IntPtr.Zero);

        // WindowFromPoint의 루트가 EnumWindows 결과에 없으면 추가 (자식 윈도우인 경우)
        // 단, 우리 프로세스 윈도우는 제외
        if (topRoot != IntPtr.Zero && !collected.Any(c => c.handle == topRoot))
        {
            NativeMethods.GetWindowThreadProcessId(topRoot, out uint topPid);
            if (topPid != myPid)
                collected.Insert(0, (topRoot, -1));
        }

        foreach (var (hWnd, z) in collected)
        {
            var clsSb = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, clsSb, 256);
            var cls = clsSb.ToString();

            var titleSb = new StringBuilder(256);
            NativeMethods.GetWindowTextW(hWnd, titleSb, 256);
            var title = titleSb.ToString();

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);

            // IsTarget: 타겟 자체, 또는 타겟의 조상/자손
            bool isTarget = IsTargetOrRelated(hWnd, targetHwnd);

            // IsBlocker: 같은 PID, #32770 팝업 또는 작은 다이얼로그
            // Skip if hWnd is an ancestor of targetHwnd (parent dialog is NOT blocking its own child)
            NativeMethods.GetWindowRect(hWnd, out var rect);
            bool isBlocker = !isTarget && pid == targetPid
                && (cls == "#32770" || (rect.Width > 50 && rect.Height > 30 && rect.Width < 800 && rect.Height < 600))
                && !NativeMethods.IsChild(hWnd, targetHwnd); // ancestor of target = container, not blocker

            stack.Add(new WindowAtPoint(
                Handle: hWnd,
                ClassName: cls,
                Title: string.IsNullOrEmpty(title) ? null : title,
                ProcessId: pid,
                IsTarget: isTarget,
                IsBlocker: isBlocker,
                ZOrder: zOrder++
            ));
        }

        return stack;
    }

    /// <summary>hWnd가 targetHwnd 자체이거나, 부모/자손 관계인지 체크.</summary>
    private static bool IsTargetOrRelated(IntPtr hWnd, IntPtr targetHwnd)
    {
        if (hWnd == targetHwnd) return true;

        // hWnd가 target의 조상?
        var walk = targetHwnd;
        for (int i = 0; i < 10 && walk != IntPtr.Zero; i++)
        {
            walk = NativeMethods.GetParent(walk);
            if (walk == hWnd) return true;
        }

        // target이 hWnd의 조상?
        walk = hWnd;
        for (int i = 0; i < 10 && walk != IntPtr.Zero; i++)
        {
            walk = NativeMethods.GetParent(walk);
            if (walk == targetHwnd) return true;
        }

        return false;
    }

    // -- Private: 분류 --------------------------------------------

    private static PointClassification ClassifyPoint(
        List<WindowAtPoint> stack, IntPtr targetHwnd, uint targetPid, IntPtr mainHwnd)
    {
        if (stack.Count == 0)
            return PointClassification.TargetNotFound;

        var top = stack[0];

        if (top.IsTarget)
            return PointClassification.TargetOnTop;

        if (top.IsBlocker)
            return PointClassification.BlockerOnTop;

        if (top.ProcessId == targetPid)
            return PointClassification.SiblingOnTop;

        // 다른 프로세스 -> ForeignOnTop, but check if target even exists in stack
        bool targetInStack = stack.Any(w => w.IsTarget);
        if (!targetInStack)
            return PointClassification.TargetNotFound;

        return PointClassification.ForeignOnTop;
    }

    // -- Private: 포커스리스 클릭 시도 ------------------------------

    /// <summary>
    /// UiaLocator.TryFocuslessClickAtPoint 래핑.
    /// 글로벌 윈도우 Z-order는 절대 안 건드림 (자식 윈도우만 조정 가능).
    /// MFC Invoke 부작용(메인창 튀어오름) 감지 -> 즉시 복원(prevFg=#1, 도둑=#2) + fgStolen=true.
    /// </summary>
    private static (bool ok, string? detail, bool fgStolen) TryFocuslessAtPoint(
        int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            var prevFg = NativeMethods.GetForegroundWindow();

            using var uia = new UiaLocator();
            var (ok, detail) = uia.TryFocuslessClickAtPoint(screenX, screenY, hWnd);

            // 전경 변경 감지: invoke 전후 전경 비교
            // MFC Invoke 부작용 -> 타겟 앱이 SetForegroundWindow 호출 -> 전경 도둑질
            bool fgStolen = false;
            if (ok && prevFg != IntPtr.Zero)
            {
                Thread.Sleep(50); // MFC 내부 SetForegroundWindow 전파 대기
                var nowFg = NativeMethods.GetForegroundWindow();
                if (nowFg != prevFg && nowFg != IntPtr.Zero)
                {
                    fgStolen = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  [FL] ! Foreground stolen! 0x{prevFg:X} -> 0x{nowFg:X}");

                    // 즉시 복원: 원래 전경(prevFg)을 다시 전경으로! -> 도둑(nowFg)은 자동으로 2등
                    // Raw restore (no guard) -- we're undoing a steal, not acquiring focus
                    NativeMethods.SetForegroundWindowRaw(prevFg);
                    Console.WriteLine($"  [FL] ! Restored: 0x{prevFg:X}->fg, 0x{nowFg:X}->#2");
                    Console.ResetColor();
                }
            }

            return (ok, detail, fgStolen);
        }
        catch (Exception ex)
        {
            return (false, $"UIA error: {ex.Message}", false);
        }
    }

    // -- Private: 미니마이즈 UIA 클릭 (2단계) ------------------

    /// <summary>
    /// 미니마이즈 윈도우에서 UIA 포커스리스 클릭. 2단계 전략:
    /// 1단계: BoundingRect로 정확 매칭 (리스토어 없이) -- 좌표에 맞는 요소 찾으면 바로 Invoke
    /// 2단계: BoundingRect 없으면 포커스리스 리스토어 -- SetWindowPlacement(SHOWNOACTIVATE) -> UIA 클릭 -> 재미니마이즈
    /// </summary>
    private static (bool ok, string? detail, bool fgStolen) TryFocuslessOnMinimized(
        int screenX, int screenY, IntPtr hWnd, IntPtr mainHwnd)
    {
        try
        {
            // rcNormalPosition -> 상대좌표 계산
            var wp = new NativeMethods.WINDOWPLACEMENT
            {
                length = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.WINDOWPLACEMENT>()
            };
            NativeMethods.GetWindowPlacement(mainHwnd, ref wp);
            var normalRect = wp.rcNormalPosition;

            NativeMethods.GetWindowRect(hWnd, out var wRect);
            int relX = screenX - wRect.Left;
            int relY = screenY - wRect.Top;
            int targetX = normalRect.Left + relX;
            int targetY = normalRect.Top + relY;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  [MIN-FL] ");
            Console.ResetColor();
            Console.Write($"rcNormal=({normalRect.Left},{normalRect.Top} {normalRect.Width}x{normalRect.Height})");
            Console.Write($" rel=({relX},{relY})");

            // -- 1단계: BoundingRect로 정확 매칭 (리스토어 없이) --
            var prevFg = NativeMethods.GetForegroundWindow();
            using var uia = new UiaLocator();
            var candidates = uia.FindInvocableDescendants(hWnd, targetX, targetY, normalRect);

            // BoundingRect이 있고 가까운 요소가 있는지 체크
            var exactMatch = candidates.FirstOrDefault(c => c.HasBounds && c.Distance < 200);
            if (exactMatch != null)
            {
                Console.Write($" -> exact [{exactMatch.ControlType}]");
                if (!string.IsNullOrEmpty(exactMatch.AutomationId))
                    Console.Write($" aid={exactMatch.AutomationId}");
                Console.Write($" dist={exactMatch.Distance:F0}");

                bool ok1 = uia.TryInvokeElement(exactMatch.Element);
                if (ok1)
                {
                    string desc1 = FormatElementDesc(exactMatch);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" -> Invoke OK (no restore)");
                    Console.ResetColor();
                    var (_, _, fgStolen1) = DetectAndRestoreForeground(prevFg);
                    return (true, $"{desc1} (Invoke, focusless, no restore)", fgStolen1);
                }
            }

            // -- 2단계: 포커스리스 리스토어 --
            Console.Write(" -> no exact match, restoring...");

            // 리매핑: rcNormalPosition 기준 + 클램핑
            int probeX = Math.Clamp(targetX, normalRect.Left, normalRect.Right - 1);
            int probeY = Math.Clamp(targetY, normalRect.Top, normalRect.Bottom - 1);

            // 순간 복원: SW_SHOWNOACTIVATE -- 포커스 안 뺏고 원래 위치에 표시
            wp.showCmd = NativeMethods.SW_SHOWNOACTIVATE;
            NativeMethods.SetWindowPlacement(mainHwnd, ref wp);
            Thread.Sleep(100); // UIA BoundingRectangle 업데이트 대기

            Console.Write($" probe=({probeX},{probeY})");

            // UIA 포커스리스 클릭 (복원 상태)
            var (ok2, detail2) = uia.TryFocuslessClickAtPoint(probeX, probeY, hWnd);

            // 즉시 재미니마이즈: SW_SHOWMINNOACTIVE
            wp.showCmd = NativeMethods.SW_SHOWMINNOACTIVE;
            NativeMethods.SetWindowPlacement(mainHwnd, ref wp);

            if (ok2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" -> {detail2} (restored)");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" -> FAIL: {detail2}");
                Console.ResetColor();
                return (false, $"Minimized restore: {detail2}", false);
            }

            var (_, _, fgStolen2) = DetectAndRestoreForeground(prevFg);
            return (true, $"{detail2} (focusless, restored)", fgStolen2);
        }
        catch (Exception ex)
        {
            return (false, $"Minimized UIA error: {ex.Message}", false);
        }
    }

    /// <summary>전경 도둑질 감지 -> SetForegroundWindow로 즉시 복원.</summary>
    private static (bool detected, IntPtr thief, bool restored) DetectAndRestoreForeground(IntPtr expectedFg)
    {
        if (expectedFg == IntPtr.Zero) return (false, IntPtr.Zero, false);
        Thread.Sleep(50);
        var nowFg = NativeMethods.GetForegroundWindow();
        if (nowFg == expectedFg || nowFg == IntPtr.Zero) return (false, IntPtr.Zero, false);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  [FL] ! Foreground stolen! 0x{expectedFg:X} -> 0x{nowFg:X}");
        NativeMethods.SetForegroundWindowRaw(expectedFg); // raw restore -- undoing steal
        Console.WriteLine($"  [FL] ! Restored: 0x{expectedFg:X}->fg, 0x{nowFg:X}->#2");
        Console.ResetColor();
        return (true, nowFg, true);
    }

    /// <summary>InvocableCandidate -> "[ControlType] Name aid=X" 형식.</summary>
    private static string FormatElementDesc(UiaLocator.InvocableCandidate c)
    {
        string desc = $"[{c.ControlType}]";
        if (!string.IsNullOrEmpty(c.Name))
            desc += $" \"{(c.Name.Length > 30 ? c.Name[..30] : c.Name)}\"";
        if (!string.IsNullOrEmpty(c.AutomationId))
            desc += $" aid={c.AutomationId}";
        return desc;
    }

    // -- Private: off-screen 좌표 리매핑 --------------------------

    /// <summary>
    /// 미니마이즈 윈도우: GetWindowRect(아이콘 위치, 매우 작음) -> UIA BoundingRectangle 기준으로
    /// 상대좌표 리매핑 + 클램핑. UIA BoundingRect이 비정상이면 원본 좌표 반환.
    /// </summary>
    private static (int probeX, int probeY) RemapToUiaBounds(int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            // 현재 GetWindowRect (아이콘 위치 = 매우 작은 rect)
            NativeMethods.GetWindowRect(hWnd, out var wRect);
            int relX = screenX - wRect.Left;
            int relY = screenY - wRect.Top;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  [REMAP] ");
            Console.ResetColor();
            Console.Write($"rel=({relX},{relY}) wRect=({wRect.Left},{wRect.Top} {wRect.Width}x{wRect.Height})");

            // UIA root의 BoundingRectangle (미니마이즈에서도 유효한 경우 있음)
            using var uia = new UiaLocator();
            var root = uia.Automation.FromHandle(hWnd);
            if (root == null)
            {
                Console.WriteLine(" -> UIA root=null, using original");
                return (screenX, screenY);
            }

            var uiaRect = root.BoundingRectangle;
            Console.Write($" uiaRect=({uiaRect.Left},{uiaRect.Top} {uiaRect.Width}x{uiaRect.Height})");

            if (uiaRect.IsEmpty || uiaRect.Width <= 0 || uiaRect.Height <= 0)
            {
                Console.WriteLine(" -> UIA rect empty, using original");
                return (screenX, screenY);
            }

            // UIA rect 기준 리매핑 + 클램핑
            int newX = Math.Clamp(uiaRect.Left + relX, uiaRect.Left, uiaRect.Right - 1);
            int newY = Math.Clamp(uiaRect.Top + relY, uiaRect.Top, uiaRect.Bottom - 1);

            bool clamped = newX != uiaRect.Left + relX || newY != uiaRect.Top + relY;
            bool remapped = newX != screenX || newY != screenY;

            Console.Write($" -> probe=({newX},{newY})");
            if (clamped) Console.Write(" [CLAMPED]");
            if (!remapped) Console.Write(" [SAME]");
            Console.WriteLine();

            return (newX, newY);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [REMAP] Failed: {ex.Message} -- using original coords");
            Console.ResetColor();
            return (screenX, screenY);
        }
    }

}
