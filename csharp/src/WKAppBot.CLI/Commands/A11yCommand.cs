using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot a11y <action> <grap> [options]
    // Actions: close, minimize, maximize, restore, move, resize
    // A11y-first (UIA WindowPattern/TransformPattern) -> Win32 fallback
    static int A11yCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot a11y <action> <grap> [options]");
            Console.WriteLine("Actions: close, minimize, maximize, restore, move, resize");
            Console.WriteLine("Options:");
            Console.WriteLine("  move:   --x N --y N");
            Console.WriteLine("  resize: --w N --h N");
            Console.WriteLine("  --all   Apply to all matching windows (default: first only)");
            return 1;
        }

        var action = args[0].ToLowerInvariant();
        var grap = args[1];
        bool all = args.Any(a => a == "--all");

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

        foreach (var win in targets)
        {
            var hwnd = win.Handle;
            var title = WindowFinder.GetWindowText(hwnd);
            var tag = $"0x{hwnd.ToInt64():X} \"{title}\"";

            bool success = action switch
            {
                "close" => A11yClose(automation, hwnd, tag),
                "minimize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Minimized),
                "maximize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Maximized),
                "restore" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Normal),
                "move" => A11yMove(automation, hwnd, tag, mx!.Value, my!.Value),
                "resize" => A11yResize(automation, hwnd, tag, mw!.Value, mh!.Value),
                _ => false
            };

            if (success) ok++; else fail++;
        }

        Console.WriteLine($"[A11Y] Done: {ok} ok, {fail} fail (total {targets.Count})");
        return fail > 0 ? 1 : 0;
    }

    // -- Close: UIA WindowPattern.Close -> Win32 WM_CLOSE fallback --
    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag)
    {
        // Try UIA WindowPattern first
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.Close();
                Console.WriteLine($"[A11Y] close {tag} — UIA WindowPattern");
                return true;
            }
        }
        catch { }

        // Win32 fallback
        try
        {
            NativeMethods.PostMessageW(hwnd, 0x0010 /* WM_CLOSE */, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] close {tag} — FAIL: {ex.Message}");
            return false;
        }
    }

    // -- Minimize/Maximize/Restore: UIA WindowPattern -> Win32 ShowWindow fallback --
    static bool A11ySetVisualState(UIA3Automation automation, IntPtr hwnd, string tag, WindowVisualState state)
    {
        // Try UIA WindowPattern
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.SetWindowVisualState(state);
                Console.WriteLine($"[A11Y] {state.ToString().ToLower()} {tag} — UIA WindowPattern");
                return true;
            }
        }
        catch { }

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
            Console.WriteLine($"[A11Y] {state.ToString().ToLower()} {tag} — Win32 ShowWindow({cmd})");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] {state.ToString().ToLower()} {tag} — FAIL: {ex.Message}");
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
        }
        catch { }

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
            Console.WriteLine($"[A11Y] move {tag} — FAIL: {ex.Message}");
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
        }
        catch { }

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
            Console.WriteLine($"[A11Y] resize {tag} — FAIL: {ex.Message}");
            return false;
        }
    }
}
