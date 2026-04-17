using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // === Window-Level Actions (see A11yElementActions.cs for element-level) ===

    static bool A11yFocus(IntPtr hwnd, string tag)
    {
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9);
        NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
        NativeMethods.BringWindowToTop(hwnd);
        Console.Error.WriteLine($"[A11Y] focus {tag} -- SetForegroundWindow");
        return true;
    }

    static bool A11yHotkey(IntPtr hwnd, string tag, string keyCombo)
    {
        // Ensure target window has focus (SendInput requires foreground)
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9); // SW_RESTORE
        NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
        NativeMethods.BringWindowToTop(hwnd);
        Thread.Sleep(100); // let focus settle

        try
        {
            // Windows menu label: "새 탭\tCtrl+T" or "Save\tCtrl+S" -> strip prefix before \t
            var tab = keyCombo.LastIndexOf('\t');
            if (tab >= 0) keyCombo = keyCombo[(tab + 1)..].Trim();

            // +/- notation -> SendKeys (e.g., "+Shift h e l l o -Shift")
            // Otherwise legacy Ctrl+S notation
            if (keyCombo.Contains(" +") || keyCombo.Contains(" -") || keyCombo.StartsWith("+") || keyCombo.StartsWith("-"))
            {
                // Pass hwnd for per-token mid-input focus check+restore
                WKAppBot.Win32.Input.KeyboardInput.SendKeys(keyCombo, hwnd);
            }
            else
            {
                var keys = keyCombo.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (keys.Length == 1)
                    WKAppBot.Win32.Input.KeyboardInput.PressKey(keys[0]);
                else
                    WKAppBot.Win32.Input.KeyboardInput.Hotkey(keys);
            }

            Console.Error.WriteLine($"[A11Y] hotkey {tag} -- \"{keyCombo}\"");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] hotkey failed: {ex.Message}");
            return false;
        }
    }

    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag, bool force, InputReadiness readiness)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.Close();
                Thread.Sleep(500);
                if (!NativeMethods.IsWindow(hwnd))
                {
                    Console.Error.WriteLine($"[A11Y] close {tag} -- UIA WindowPattern");
                    return true;
                }
                // Window still alive -- save dialog may have appeared (WinUI internal modal)
                Console.Error.WriteLine($"[A11Y] close {tag} -- UIA Close sent but window still alive, checking internal modal...");
                if (HasUiaInternalModal(automation, hwnd, out var modalButtonName))
                {
                    if (force)
                    {
                        DismissUiaInternalModal(automation, hwnd, tag, modalButtonName);
                        Thread.Sleep(500);
                        if (!NativeMethods.IsWindow(hwnd))
                        {
                            Console.Error.WriteLine($"[A11Y] close {tag} -- closed after UIA modal dismiss");
                            return true;
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"[A11Y] close {tag} -- save dialog detected! use --force to dismiss without saving");
                        return false;
                    }
                }
                // Fall through to Win32 WM_CLOSE tier
            }
            Console.Error.WriteLine($"[A11Y] close {tag} -- UIA not sufficient, trying Win32");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} -- UIA failed ({ex.Message}), trying Win32");
        }

        try
        {
            NativeMethods.SendMessageTimeoutW(hwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
            Thread.Sleep(1000);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.Error.WriteLine($"[A11Y] close {tag} -- Win32 WM_CLOSE");
                return true;
            }
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.Error.WriteLine($"[A11Y] close {tag} -- blocker: {blocker.ClassName} \"{blocker.Title}\" -- dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(500);
                if (!NativeMethods.IsWindow(hwnd))
                {
                    Console.Error.WriteLine($"[A11Y] close {tag} -- closed after dismiss");
                    return true;
                }
            }
            // UIA internal modal detection (WinUI/WPF save dialogs -- no separate Win32 popup)
            if (HasUiaInternalModal(automation, hwnd, out var wmModalBtn))
            {
                if (force)
                {
                    DismissUiaInternalModal(automation, hwnd, tag, wmModalBtn);
                    Thread.Sleep(500);
                    if (!NativeMethods.IsWindow(hwnd))
                    {
                        Console.Error.WriteLine($"[A11Y] close {tag} -- closed after UIA modal dismiss");
                        return true;
                    }
                    // Retry WM_CLOSE after dismiss
                    NativeMethods.SendMessageTimeoutW(hwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
                    Thread.Sleep(500);
                    if (!NativeMethods.IsWindow(hwnd))
                    {
                        Console.Error.WriteLine($"[A11Y] close {tag} -- closed after retry WM_CLOSE");
                        return true;
                    }
                }
                else
                {
                    Console.Error.WriteLine($"[A11Y] close {tag} -- save dialog detected! use --force to dismiss without saving");
                    return false;
                }
            }
            if (!force)
            {
                Console.Error.WriteLine($"[A11Y] close {tag} -- still alive (use --force to kill)");
                return false;
            }
        }
        catch (Exception ex)
        {
            if (!force) { Console.Error.WriteLine($"[A11Y] close {tag} -- WM_CLOSE failed: {ex.Message}"); return false; }
        }

        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            if (pid != 0)
            {
                System.Diagnostics.Process.GetProcessById((int)pid).Kill();
                Console.Error.WriteLine($"[A11Y] close {tag} -- Process.Kill (pid={pid})");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} -- all tiers FAIL: {ex.Message}");
        }
        return false;
    }

    /// <summary>Known dismiss button names for save dialogs (priority-ordered: don't-save first).</summary>
    static readonly string[] SaveDialogDismissNames = [
        "저장하지 않음", "Don't Save", "Don\u2019t Save",
        "아니오", "No",
        "취소", "Cancel",
        "닫기", "Close",
    ];

    /// <summary>
    /// Detect UIA internal modal dialog (WinUI/WPF save dialogs -- no separate Win32 popup).
    /// Returns true if a dismiss-able button was found, with the button name in <paramref name="buttonName"/>.
    /// Does NOT click the button -- caller decides based on --force flag.
    /// </summary>
    static bool HasUiaInternalModal(UIA3Automation automation, IntPtr hwnd, out string buttonName)
    {
        buttonName = "";
        try
        {
            var root = automation.FromHandle(hwnd);
            if (root == null) return false;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = root.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null || buttons.Length == 0) return false;

            foreach (var dismissName in SaveDialogDismissNames)
            {
                foreach (var btn in buttons)
                {
                    try
                    {
                        var name = btn.Name;
                        if (!string.IsNullOrEmpty(name) && name.Equals(dismissName, StringComparison.OrdinalIgnoreCase))
                        {
                            buttonName = name;
                            return true;
                        }
                    }
                    catch { }
                }
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Click the dismiss button in a UIA internal modal dialog.
    /// Called only when --force is specified.
    /// </summary>
    static void DismissUiaInternalModal(UIA3Automation automation, IntPtr hwnd, string tag, string buttonName)
    {
        try
        {
            var root = automation.FromHandle(hwnd);
            if (root == null) return;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = root.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null) return;

            foreach (var btn in buttons)
            {
                try
                {
                    var name = btn.Name;
                    if (!string.IsNullOrEmpty(name) && name.Equals(buttonName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Error.WriteLine($"[A11Y] close {tag} -- UIA internal modal: clicking \"{name}\" (--force)");
                        if (btn.Patterns.Invoke.IsSupported)
                            btn.Patterns.Invoke.Pattern.Invoke();
                        else
                            btn.Patterns.LegacyIAccessible.Pattern.DoDefaultAction();
                        return;
                    }
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} -- UIA modal dismiss failed: {ex.Message}");
        }
    }

    static bool A11ySetVisualState(UIA3Automation automation, IntPtr hwnd, string tag, WindowVisualState state)
    {
        var name = state.ToString().ToLower();
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.SetWindowVisualState(state);
                Console.Error.WriteLine($"[A11Y] {name} {tag} -- UIA WindowPattern");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] {name} {tag} -- UIA failed ({ex.Message}), trying Win32");
        }

        int cmd = state switch
        {
            WindowVisualState.Minimized => 6,
            WindowVisualState.Maximized => 3,
            _ => 9
        };
        try
        {
            NativeMethods.ShowWindow(hwnd, cmd);
            Console.Error.WriteLine($"[A11Y] {name} {tag} -- Win32 ShowWindow({cmd})");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] {name} {tag} -- FAIL: {ex.Message}");
            return false;
        }
    }

    static bool A11yMove(UIA3Automation automation, IntPtr hwnd, string tag, int x, int y)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanMove)
            {
                el.Patterns.Transform.Pattern.Move(x, y);
                Console.Error.WriteLine($"[A11Y] move {tag} -> ({x},{y}) -- UIA Transform");
                return true;
            }
        }
        catch { }
        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 0x0015);
            Console.Error.WriteLine($"[A11Y] move {tag} -> ({x},{y}) -- Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] move {tag} -- FAIL: {ex.Message}");
            return false;
        }
    }

    static bool A11yResize(UIA3Automation automation, IntPtr hwnd, string tag, int w, int h)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanResize)
            {
                el.Patterns.Transform.Pattern.Resize(w, h);
                Console.Error.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) -- UIA Transform");
                return true;
            }
        }
        catch { }
        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 0x0016);
            Console.Error.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) -- Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] resize {tag} -- FAIL: {ex.Message}");
            return false;
        }
    }

    // === Target Selection Helpers ===

    static List<WindowInfo>? ParseNthTargets(List<WindowInfo> windows, string nthRaw)
    {
        if (windows.Count == 0) return [];
        if (string.IsNullOrWhiteSpace(nthRaw)) return null;

        var picked = new List<WindowInfo>();
        var seen = new HashSet<int>();

        foreach (var rawTerm in nthRaw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var indexes = ParseNthIndexes(rawTerm, windows.Count);
            if (indexes == null) return null;
            foreach (var idx in indexes)
            {
                if (idx < 1 || idx > windows.Count) return null;
                if (seen.Add(idx))
                    picked.Add(windows[idx - 1]);
            }
        }

        return picked.Count > 0 ? picked : null;
    }

    static List<int>? ParseNthIndexes(string nthRaw, int count)
    {
        if (string.IsNullOrWhiteSpace(nthRaw)) return null;

        if (nthRaw.StartsWith('~'))
        {
            if (!int.TryParse(nthRaw[1..], out var to) || to < 1) return null;
            return Enumerable.Range(1, Math.Min(to, count)).ToList();
        }
        if (nthRaw.EndsWith('~'))
        {
            if (!int.TryParse(nthRaw[..^1], out var from) || from < 1 || from > count) return null;
            return Enumerable.Range(from, count - from + 1).ToList();
        }
        if (nthRaw.Contains('~'))
        {
            var parts = nthRaw.Split('~');
            if (parts.Length != 2
                || !int.TryParse(parts[0], out var from)
                || !int.TryParse(parts[1], out var to)
                || from < 1 || to < from || from > count)
                return null;
            return Enumerable.Range(from, Math.Min(to, count) - from + 1).ToList();
        }
        if (int.TryParse(nthRaw, out var idx) && idx >= 1 && idx <= count)
            return [idx];

        return null;
    }

    // Window hierarchy ancestors: root windows that own/parent our process's windows.
    // Catches hosts like VS Code that aren't in the process tree but contain our window.
    static readonly HashSet<IntPtr> _cachedWindowAncestors = BuildWindowHierarchyAncestors();

    static HashSet<IntPtr> BuildWindowHierarchyAncestors()
    {
        var ancestors = new HashSet<IntPtr>();
        try
        {
            uint myPid = (uint)Environment.ProcessId;
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != myPid) return true;
                // Walk owner + parent chain from our window up to root
                var cur = hWnd;
                for (int i = 0; i < 20 && cur != IntPtr.Zero; i++)
                {
                    var owner = NativeMethods.GetWindow(cur, 4u); // GW_OWNER = 4
                    var parent = NativeMethods.GetParent(cur);
                    var next = owner != IntPtr.Zero ? owner : parent;
                    if (next == IntPtr.Zero) break;
                    NativeMethods.GetWindowThreadProcessId(next, out uint nextPid);
                    if (nextPid != myPid) ancestors.Add(NativeMethods.GetAncestor(next, NativeMethods.GA_ROOT));
                    cur = next;
                }
                return true;
            }, IntPtr.Zero);
        }
        catch { }
        return ancestors;
    }

    static HashSet<IntPtr> GetWindowHierarchyAncestors() => _cachedWindowAncestors;

    // Cached at startup -- parent processes may exit mid-run, breaking GetParentPid chain.
    // Capture the full chain NOW while all ancestors are still alive.
    static readonly HashSet<int> _cachedAncestorPids = BuildAncestorPids();

    static HashSet<int> BuildAncestorPids()
    {
        var pids = new HashSet<int>();
        try
        {
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            pids.Add(proc.Id);
            int pid = proc.Id;
            for (int i = 0; i < 20 && pid > 0; i++) // 20 levels: covers Code.exe -> shell -> wkappbot
            {
                int ppid = GetParentPid(pid);
                if (ppid <= 0 || !pids.Add(ppid)) break;
                pid = ppid;
            }
        }
        catch { }
        return pids;
    }

    static HashSet<int> GetSelfAndAncestorPids() => _cachedAncestorPids;
}
