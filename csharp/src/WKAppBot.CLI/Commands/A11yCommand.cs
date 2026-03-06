using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot a11y <action> <grap> [options]
    // Actions: close, minimize, maximize, restore, move, resize
    // A11y-first (UIA WindowPattern/TransformPattern) -> Win32 fallback
    //
    // TODO (future):
    // - grap에 hwnd 직접 지정: "#0x1A2B3C" 또는 "hwnd=0x..." 구문
    // - --all로 매칭 전부 처리 (현재 구현됨)
    // - invoke/click 액션 추가 (기존 uia-test --invoke / win-click --uia 통합)
    // - Android ADB 폴백 (Phase 14): UIA → Win32 → ADB 3티어
    static int A11yCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot a11y <action> <grap> [options]");
            Console.WriteLine("Actions: close, minimize, maximize, restore, move, resize");
            Console.WriteLine("Options:");
            Console.WriteLine("  move:   --x N --y N");
            Console.WriteLine("  resize: --w N --h N");
            Console.WriteLine("  --all     Apply to all matching windows (default: first only)");
            Console.WriteLine("  --force   close: kill process if WM_CLOSE fails (default: stop at WM_CLOSE)");
            return 1;
        }

        var action = args[0].ToLowerInvariant();
        var grap = args[1];
        bool all = args.Any(a => a == "--all");
        bool force = args.Any(a => a == "--force");

        // Parse move/resize params
        int? mx = null, my = null, mw = null, mh = null;
        for (int i = 2; i < args.Length; i++)
        {
            var a = args[i].ToLowerInvariant();
            if (a == "--x" && i + 1 < args.Length && int.TryParse(args[i + 1], out var xv)) { mx = xv; i++; }
            else if (a == "--y" && i + 1 < args.Length && int.TryParse(args[i + 1], out var yv)) { my = yv; i++; }
            else if (a == "--w" && i + 1 < args.Length && int.TryParse(args[i + 1], out var wv)) { mw = wv; i++; }
            else if (a == "--h" && i + 1 < args.Length && int.TryParse(args[i + 1], out var hv)) { mh = hv; i++; }
        }

        // Validate action-specific params
        if (action == "move" && (mx == null || my == null))
            return Error("move requires --x N --y N");
        if (action == "resize" && (mw == null || mh == null))
            return Error("resize requires --w N --h N");

        // Find windows by grap (title pattern)
        var windows = WindowFinder.FindByTitle(grap);
        if (windows.Count == 0)
            return Error($"No window found: \"{grap}\"");

        var targets = all ? windows : new List<WindowInfo> { windows[0] };
        int ok = 0, fail = 0;

        using var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);

        var readiness = CreateInputReadiness();

        foreach (var win in targets)
        {
            var hwnd = win.Handle;
            var title = WindowFinder.GetWindowText(hwnd);
            var tag = $"0x{hwnd.ToInt64():X} \"{title}\"";

            // 입력위치확보: 방해꾼 감지 + dismiss (~5ms)
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.WriteLine($"[A11Y] blocker detected: {blocker.ClassName} \"{blocker.Title}\" — dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(300);
            }

            // 미니마이즈 상태면 복원 (close 제외 — close는 미니마이즈 상태에서도 가능)
            if (action != "close" && action != "minimize")
            {
                if (NativeMethods.IsIconic(hwnd))
                {
                    Console.WriteLine($"[A11Y] {tag} is minimized — restoring first");
                    NativeMethods.ShowWindow(hwnd, 9 /* SW_RESTORE */);
                    Thread.Sleep(300);
                }
            }

            bool success = action switch
            {
                "close" => A11yClose(automation, hwnd, tag, force, readiness),
                "minimize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Minimized),
                "maximize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Maximized),
                "restore" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Normal),
                "move" => A11yMove(automation, hwnd, tag, mx!.Value, my!.Value),
                "resize" => A11yResize(automation, hwnd, tag, mw!.Value, mh!.Value),
                _ => A11yNotYet(action)
            };

            if (success) ok++; else fail++;
        }

        Console.WriteLine($"[A11Y] Done: {ok} ok, {fail} fail (total {targets.Count})");
        return fail > 0 ? 1 : 0;
    }

    static bool A11yNotYet(string action)
    {
        Console.Error.WriteLine($"[A11Y] ERROR: '{action}' command is not implemented");
        Console.Error.WriteLine("[A11Y] Supported: close, minimize, maximize, restore, move, resize");
        Console.Error.WriteLine("[A11Y] TODO (not yet): invoke, click, scroll, toggle, select, set-range");
        return false;
    }

    // -- Close: UIA WindowPattern.Close -> Win32 WM_CLOSE -> Process.Kill fallback --
    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag, bool force, InputReadiness readiness)
    {
        // Tier 1: UIA WindowPattern (예의바른 닫기)
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.Close();
                Console.WriteLine($"[A11Y] close {tag} — UIA WindowPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] close {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] close {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        // Tier 2: Win32 WM_CLOSE (정중한 종료 요청 — SendMessage로 매너있게 기다림)
        try
        {
            NativeMethods.SendMessageTimeoutW(hwnd, 0x0010 /* WM_CLOSE */, IntPtr.Zero, IntPtr.Zero,
                0x0002 /* SMTO_ABORTIFHUNG */, 3000, out _);
            // 1초 대기 후 윈도우가 닫혔는지 확인
            Thread.Sleep(1000);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE (closed)");
                return true;
            }
            // Tier 2.5: 방해꾼 다이얼로그 감지 → dismiss → 재확인
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.WriteLine($"[A11Y] close {tag} — blocker: {blocker.ClassName} \"{blocker.Title}\" — dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(500);
                if (!NativeMethods.IsWindow(hwnd))
                {
                    Console.WriteLine($"[A11Y] close {tag} — closed after dismissing blocker");
                    return true;
                }
            }

            if (!force)
            {
                Console.WriteLine($"[A11Y] close {tag} — WM_CLOSE sent but window still alive (use --force to kill)");
                return false;
            }
            Console.WriteLine($"[A11Y] close {tag} — WM_CLOSE sent but window still alive, --force → Process.Kill");
        }
        catch (Exception ex)
        {
            if (!force)
            {
                Console.Error.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE failed ({ex.Message}), use --force to kill");
                return false;
            }
            Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE failed ({ex.Message}), --force → Process.Kill");
        }

        // Tier 3: Process.Kill (--force 전용 — 강제 종료)
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            if (pid != 0)
            {
                var proc = System.Diagnostics.Process.GetProcessById((int)pid);
                proc.Kill();
                Console.WriteLine($"[A11Y] close {tag} — Process.Kill (pid={pid})");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} — all 3 tiers FAIL: {ex.Message}");
        }
        return false;
    }

    // -- Minimize/Maximize/Restore: UIA WindowPattern -> Win32 ShowWindow fallback --
    static bool A11ySetVisualState(UIA3Automation automation, IntPtr hwnd, string tag, WindowVisualState state)
    {
        var name = state.ToString().ToLower();
        // Try UIA WindowPattern
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.SetWindowVisualState(state);
                Console.WriteLine($"[A11Y] {name} {tag} — UIA WindowPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] {name} {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] {name} {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        // Win32 fallback
        int cmd = state switch
        {
            WindowVisualState.Minimized => 6,  // SW_MINIMIZE
            WindowVisualState.Maximized => 3,  // SW_MAXIMIZE
            WindowVisualState.Normal => 9,     // SW_RESTORE
            _ => 9
        };
        try
        {
            NativeMethods.ShowWindow(hwnd, cmd);
            Console.WriteLine($"[A11Y] {name} {tag} — Win32 ShowWindow({cmd})");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] {name} {tag} — Win32 fallback FAIL: {ex.Message}");
            return false;
        }
    }

    // -- Move: UIA TransformPattern -> Win32 SetWindowPos fallback --
    static bool A11yMove(UIA3Automation automation, IntPtr hwnd, string tag, int x, int y)
    {
        // Try UIA TransformPattern
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanMove)
            {
                el.Patterns.Transform.Pattern.Move(x, y);
                Console.WriteLine($"[A11Y] move {tag} → ({x},{y}) — UIA TransformPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] move {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] move {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        // Win32 fallback: SetWindowPos with NOSIZE
        try
        {
            // SWP_NOSIZE(0x1) | SWP_NOZORDER(0x4) | SWP_NOACTIVATE(0x10)
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 0x0015);
            Console.WriteLine($"[A11Y] move {tag} → ({x},{y}) — Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] move {tag} — Win32 fallback FAIL: {ex.Message}");
            return false;
        }
    }

    // -- Resize: UIA TransformPattern -> Win32 SetWindowPos fallback --
    static bool A11yResize(UIA3Automation automation, IntPtr hwnd, string tag, int w, int h)
    {
        // Try UIA TransformPattern
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanResize)
            {
                el.Patterns.Transform.Pattern.Resize(w, h);
                Console.WriteLine($"[A11Y] resize {tag} → ({w}x{h}) — UIA TransformPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] resize {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] resize {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        // Win32 fallback: SetWindowPos with NOMOVE
        try
        {
            // SWP_NOMOVE(0x2) | SWP_NOZORDER(0x4) | SWP_NOACTIVATE(0x10)
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 0x0016);
            Console.WriteLine($"[A11Y] resize {tag} → ({w}x{h}) — Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] resize {tag} — Win32 fallback FAIL: {ex.Message}");
            return false;
        }
    }
}
