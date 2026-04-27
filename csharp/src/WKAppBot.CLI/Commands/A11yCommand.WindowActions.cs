using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    // === Window-Level Actions (see A11yElementActions.cs for element-level) ===

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref OverlayRect pvParam, uint fWinIni);
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct OverlayRect { public int left, top, right, bottom; }
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    static extern uint GetPixel(IntPtr hdc, int x, int y);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hWnd);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

    // Track how many times each HWND was suppressed -- kill if ≥4 times
    static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, int> _overlaySuppressCount = new();

    /// <summary>
    /// Suppress any visible window that:
    ///   (1) covers ≥80% of the work area (taskbar excluded), AND
    ///   (2) average of 5 sample pixels is near-black (avg RGB brightness &lt; 80/765).
    /// Forces alpha=0 on WS_EX_LAYERED windows; hides others.
    /// If same HWND suppressed ≥4 times: kill the process.
    /// Fired as a pre-check on every a11y command entry.
    /// </summary>
    static void SuppressFullscreenOpaqueOverlays()
    {
        try
        {
            // Work area = screen minus taskbar
            var wa = new OverlayRect();
            SystemParametersInfo(0x0030 /*SPI_GETWORKAREA*/, 0, ref wa, 0);
            long waW = wa.right - wa.left;
            long waH = wa.bottom - wa.top;
            if (waW <= 0 || waH <= 0) return;
            long minArea = (long)(waW * waH * 0.8);

            var screenDc = GetDC(IntPtr.Zero);
            try
            {
                NativeMethods.EnumWindows((hwnd, _) =>
                {
                    try
                    {
                        if (!NativeMethods.IsWindowVisible(hwnd)) return true;
                        NativeMethods.GetWindowRect(hwnd, out var rect);
                        long w = rect.Right - rect.Left;
                        long h = rect.Bottom - rect.Top;
                        if (w * h < minArea) return true;

                        // Sample 5 points (center + 4 quadrant centers) -- average brightness
                        // More reliable than single center pixel (avoids wallpaper bleed-through)
                        int[] sampleX = { rect.Left + (int)(w/2), rect.Left + (int)(w/4), rect.Left + (int)(w*3/4), rect.Left + (int)(w/4), rect.Left + (int)(w*3/4) };
                        int[] sampleY = { rect.Top + (int)(h/2), rect.Top + (int)(h/4), rect.Top + (int)(h/4), rect.Top + (int)(h*3/4), rect.Top + (int)(h*3/4) };
                        int totalBrightness = 0; int validSamples = 0;
                        for (int si = 0; si < 5; si++)
                        {
                            uint px = GetPixel(screenDc, sampleX[si], sampleY[si]);
                            if (px == 0xFFFFFFFF) continue;
                            totalBrightness += (int)((px & 0xFF) + ((px >> 8) & 0xFF) + ((px >> 16) & 0xFF));
                            validSamples++;
                        }
                        if (validSamples == 0) return true;
                        int avgBrightness = totalBrightness / validSamples; // max 765 (255*3)
                        if (avgBrightness > 80) return true; // not near-black (avg channel < ~27)

                        var cls = new System.Text.StringBuilder(128);
                        NativeMethods.GetClassNameW(hwnd, cls, cls.Capacity);
                        int ex = NativeMethods.GetWindowLongW(hwnd, -20);
                        bool layered = (ex & NativeMethods.WS_EX_LAYERED) != 0;

                        if (layered)
                        {
                            // Force alpha=0 (invisible but still running)
                            NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 0, NativeMethods.LWA_ALPHA);
                        }
                        else
                        {
                            // Non-layered: minimize to get it out of the way
                            NativeMethods.ShowWindow(hwnd, 6); // SW_MINIMIZE
                        }
                        int count = _overlaySuppressCount.AddOrUpdate(hwnd, 1, (_, c) => c + 1);
                        Console.Error.WriteLine($"[OVERLAY-GUARD] Suppressed black overlay: 0x{hwnd.ToInt64():X} cls={cls} {w}x{h} avg_brightness={avgBrightness} layered={layered} count={count}");
                        if (count >= 4)
                        {
                            // Persistent offender -- kill the process
                            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pidU);
                            int pid = (int)pidU;
                            try
                            {
                                System.Diagnostics.Process.GetProcessById(pid).Kill();
                                Console.Error.WriteLine($"[OVERLAY-GUARD] KILLED pid={pid} (reappeared {count} times)");
                                _overlaySuppressCount.TryRemove(hwnd, out int _removed);
                            }
                            catch (Exception kex) { Console.Error.WriteLine($"[OVERLAY-GUARD] Kill failed: {kex.Message}"); }
                        }
                    }
                    catch { }
                    return true;
                }, IntPtr.Zero);
            }
            finally { ReleaseDC(IntPtr.Zero, screenDc); }
        }
        catch { }
    }

    static bool A11yFocus(IntPtr hwnd, string tag)
    {
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9);
        NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
        NativeMethods.BringWindowToTop(hwnd);
        Console.Error.WriteLine($"[A11Y] focus {tag} -- SetForegroundWindow");
        return true;
    }

    /// <summary>
    /// Print structured BLOCKED output with dismiss grap when a blocker dialog is detected.
    /// stdout line: "# BLOCKED {hwnd_grap}#{btn_scope} -- dismiss first then retry"
    /// Makes it easy for callers (Claude, scripts) to dismiss and retry automatically.
    /// </summary>
    static void EmitBlockerGrap(WKAppBot.Win32.Input.BlockerInfo blocker)
    {
        try
        {
            // Find dismiss button: enumerate child Button windows
            NativeMethods.GetWindowThreadProcessId(blocker.Handle, out uint pid);
            string proc = "";
            try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            var grapBase = $"{{hwnd:0x{blocker.Handle.ToInt64():X},proc:'{proc}',cls:'{blocker.ClassName}'}}";

            // Enumerate child buttons (Win32 class "Button")
            var buttons = new List<string>();
            var sb = new StringBuilder(128);
            NativeMethods.EnumChildWindows(blocker.Handle, (hChild, _) =>
            {
                sb.Clear();
                NativeMethods.GetClassNameW(hChild, sb, sb.Capacity);
                if (sb.ToString().Equals("Button", StringComparison.OrdinalIgnoreCase))
                {
                    var titleBuf = new StringBuilder(64);
                    NativeMethods.GetWindowTextW(hChild, titleBuf, titleBuf.Capacity);
                    var btnText = titleBuf.ToString().Trim();
                    if (!string.IsNullOrEmpty(btnText)) buttons.Add(btnText);
                }
                return true;
            }, IntPtr.Zero);

            // First button = likely dismiss (OK/확인/Close/닫기)
            var dismissBtn = buttons.FirstOrDefault(b =>
                b.Equals("OK", StringComparison.OrdinalIgnoreCase) ||
                b.Contains("확인") || b.Contains("OK") || b.Contains("닫기") || b.Contains("Close"))
                ?? buttons.FirstOrDefault();

            var btnScope = dismissBtn != null ? $"#*{dismissBtn}*" : "";
            var btnList = buttons.Count > 0 ? $" buttons=[{string.Join(",", buttons)}]" : "";

            // stdout: machine-parseable BLOCKED line
            Console.WriteLine($"# BLOCKED {grapBase}{btnScope} title=\"{blocker.Title}\"{btnList}");
            Console.Error.WriteLine($"[A11Y] BLOCKED dismiss: wkappbot a11y invoke {grapBase}{btnScope}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] EmitBlockerGrap error: {ex.Message}");
        }
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
        // Yield to active user: WindowPattern.Close / WM_CLOSE / Process.Kill all steal
        // foreground when the target is in front. FocusSentinel only catches the steal
        // AFTER it happens; by then the user has lost their typing context. Mirrors the
        // pre-op guard in InspectionCommands.InspectCommand. --force opts out (explicit
        // user intent: kill regardless of activity).
        // BUG-AUTO patterns addressed: "FOCUS-STEAL end during a11y-close",
        // "focus-steal unrecoverable after trusted-click-menu-item",
        // "FOCUS-STEAL a11y-close remaining batch A C",
        // "UIA focus steal ask-trusted-click-menu-item", "trusted-click-upload-btn"
        if (!force && FocusSafe.ShouldYieldToActiveUser(out var idleMs))
        {
            Console.Error.WriteLine($"[A11Y:CLOSE] {tag} -- user active ({idleMs}ms idle) -- skipping to avoid focus steal (use --force to override)");
            return false;
        }

        // Pre-flight: show the plan -- process name + any pre-existing modal -- so the
        // user sees what's about to happen before any WM_CLOSE fires.
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint targetPid);
            string procName = "?";
            try { procName = System.Diagnostics.Process.GetProcessById((int)targetPid).ProcessName; } catch { }
            string modalNote = "";
            try
            {
                if (HasUiaInternalModal(automation, hwnd, out var preModalBtn))
                    modalNote = $" [modal: \"{preModalBtn}\" -- use --force to dismiss]";
            }
            catch { }
            Console.Error.WriteLine($"[CLOSE-PLAN] {tag} proc={procName}{modalNote}");
        }
        catch { }

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
                // Window still alive after 500ms -- dump current focus node so the caller
                // sees what's blocking (save dialog, modal, Korean IME composition, etc.)
                LogCurrentFocusNode(automation, "UIA-Close-500ms", tag);
                // Save dialog may have appeared (WinUI internal modal)
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
            Thread.Sleep(500);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.Error.WriteLine($"[A11Y] close {tag} -- Win32 WM_CLOSE");
                return true;
            }
            // Still alive 500ms after WM_CLOSE -- dump focus node so caller sees the blocker
            LogCurrentFocusNode(automation, "WM_CLOSE-500ms", tag);
            Thread.Sleep(500);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.Error.WriteLine($"[A11Y] close {tag} -- Win32 WM_CLOSE (delayed)");
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
    /// <summary>
    /// Dump current UIA focused element so the caller can see what's blocking a close.
    /// Typical causes: save dialog modal, IME composition, focus handed off to a child window.
    /// </summary>
    static void LogCurrentFocusNode(UIA3Automation automation, string phase, string tag)
    {
        try
        {
            var focused = automation.FocusedElement();
            if (focused == null) { Console.Error.WriteLine($"[A11Y] close {tag} -- [{phase}] focus=none"); return; }
            string fType = "?"; try { fType = focused.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            string fName = "";  try { fName = focused.Properties.Name.ValueOrDefault ?? ""; } catch { }
            if (fName.Length > 60) fName = fName[..57] + "...";
            string fAid = "";   try { fAid = focused.Properties.AutomationId.ValueOrDefault ?? ""; } catch { }
            var focHwnd = IntPtr.Zero;
            try { focHwnd = focused.Properties.NativeWindowHandle.ValueOrDefault; } catch { }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[A11Y] close {tag} -- [{phase}] still alive. focus: {fType} \"{fName}\" aid=\"{fAid}\" hwnd=0x{focHwnd.ToInt64():X}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} -- [{phase}] still alive. focus-probe error: {ex.Message}");
        }
    }

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
        // wkappbot is a console app with no visible window, so EnumWindows never
        // finds a window for our own PID -- walking from our windows yields nothing.
        // Instead, walk the PROCESS ancestor chain (shell -> terminal -> VS Code host)
        // and collect every window owned by any ancestor PID. That catches hosts like
        // VS Code that aren't reachable via HWND owner/parent chain from us.
        var ancestors = new HashSet<IntPtr>();
        try
        {
            var myPid = Environment.ProcessId;
            // Build ancestor PID set inline (can't reference _cachedAncestorPids -- not yet initialized)
            var ancestorPids = new HashSet<int>();
            int pid = myPid;
            for (int i = 0; i < 20 && pid > 0; i++)
            {
                int ppid = GetParentPid(pid);
                if (ppid <= 0 || !ancestorPids.Add(ppid)) break;
                pid = ppid;
            }
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint wpid);
                if ((int)wpid != myPid && ancestorPids.Contains((int)wpid))
                    ancestors.Add(hWnd);
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
