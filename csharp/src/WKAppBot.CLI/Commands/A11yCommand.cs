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
    // wkappbot a11y <action> <grap>[#uia-scope] [options]
    // Window-level: close, minimize, maximize, restore, move, resize, focus
    // Element-level (grap#scope): read, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range
    // Unified pattern: find all → select (--nth/--all) → dispatch targets
    static int A11yCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot a11y <action> <grap>[#uia-scope] [options]");
            Console.WriteLine();
            Console.WriteLine("Window-level actions:");
            Console.WriteLine("  close, minimize, maximize, restore, focus");
            Console.WriteLine("  move --x N --y N");
            Console.WriteLine("  resize --w N --h N");
            Console.WriteLine();
            Console.WriteLine("Element-level actions (use #scope to target UIA elements):");
            Console.WriteLine("  read         Read element state: Name, Value, Toggle, Range, Children (TTS friendly)");
            Console.WriteLine("  invoke       Invoke/click element (UIA Invoke -> BM_CLICK -> WM_LBUTTON)");
            Console.WriteLine("  click        Physical click at element center (BoundingRect -> SendInput)");
            Console.WriteLine("  toggle       Toggle checkbox/button (UIA Toggle -> BM_CLICK)");
            Console.WriteLine("  expand       Expand tree/combo (UIA ExpandCollapse)");
            Console.WriteLine("  collapse     Collapse tree/combo (UIA ExpandCollapse)");
            Console.WriteLine("  select       Select list/tab item (UIA SelectionItem)");
            Console.WriteLine("  scroll       Scroll container (--direction up|down|left|right) (--amount small|large)");
            Console.WriteLine("  type         Type text (--text \"...\") (UIA Value -> WM_CHAR)");
            Console.WriteLine("  set-value    Set value (--text \"...\") (UIA Value -> WM_SETTEXT)");
            Console.WriteLine("  set-range    Set range (--value N) (UIA RangeValue)");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --nth N   Target Nth match (1-based): 3, 3~, ~3, 2~4");
            Console.WriteLine("  --all     Apply to all matching windows (default: first only)");
            Console.WriteLine("  --force   close: kill process if WM_CLOSE fails");
            Console.WriteLine("  --force-close-ancestors  include own process tree (default: skip ancestors)");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot a11y close \"*메모장*\"");
            Console.WriteLine("  wkappbot a11y close \"*메모장*\" --nth 2~     # 2번째부터 전부 닫기");
            Console.WriteLine("  wkappbot a11y invoke \"*메모장*#*파일*\"       # invoke '파일' menu in Notepad");
            Console.WriteLine("  wkappbot a11y toggle \"*Settings*#*Dark mode*\"");
            Console.WriteLine("  wkappbot a11y type \"*영웅문*#*종목코드*\" --text \"005930\"");
            return 1;
        }

        var action = args[0].ToLowerInvariant();
        var grap = args[1];
        bool all = args.Any(a => a == "--all");
        bool force = args.Any(a => a == "--force");
        bool forceCloseAncestors = args.Any(a => a == "--force-close-ancestors");

        // Parse action-specific params
        int? mx = null, my = null, mw = null, mh = null;
        string? text = null;
        double? rangeValue = null;
        string scrollDir = "down";
        string scrollAmount = "small";
        string? nthRaw = null;
        for (int i = 2; i < args.Length; i++)
        {
            var a = args[i].ToLowerInvariant();
            if (a == "--x" && i + 1 < args.Length && int.TryParse(args[i + 1], out var xv)) { mx = xv; i++; }
            else if (a == "--y" && i + 1 < args.Length && int.TryParse(args[i + 1], out var yv)) { my = yv; i++; }
            else if (a == "--w" && i + 1 < args.Length && int.TryParse(args[i + 1], out var wv)) { mw = wv; i++; }
            else if (a == "--h" && i + 1 < args.Length && int.TryParse(args[i + 1], out var hv)) { mh = hv; i++; }
            else if (a == "--text" && i + 1 < args.Length) { text = args[++i]; }
            else if (a == "--value" && i + 1 < args.Length && double.TryParse(args[i + 1], out var rv)) { rangeValue = rv; i++; }
            else if (a == "--direction" && i + 1 < args.Length) { scrollDir = args[++i].ToLowerInvariant(); }
            else if (a == "--amount" && i + 1 < args.Length) { scrollAmount = args[++i].ToLowerInvariant(); }
            else if (a == "--nth" && i + 1 < args.Length) { nthRaw = args[++i]; }
        }

        // Validate action-specific params
        if (action == "move" && (mx == null || my == null))
            return Error("move requires --x N --y N");
        if (action == "resize" && (mw == null || mh == null))
            return Error("resize requires --w N --h N");
        if ((action == "type" || action == "set-value") && text == null)
            return Error($"{action} requires --text \"...\"");
        if (action == "set-range" && rangeValue == null)
            return Error("set-range requires --value N");

        var elementActions = new HashSet<string> {
            "invoke", "click", "toggle", "expand", "collapse", "select",
            "scroll", "type", "set-value", "set-range", "read"
        };
        bool isElementAction = elementActions.Contains(action);

        // ═══ STEP 1: Split grap into win32 path + UIA scope ═══
        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grap);
        if (win32Segments.Length == 0)
            return Error("No window title in grap pattern");

        // ═══ STEP 2: Find all matching windows (first segment, `;` OR support) ═══
        var firstSegPatterns = win32Segments[0]
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var allWindows = new List<WindowInfo>();
        var seen = new HashSet<IntPtr>();
        foreach (var pat in firstSegPatterns)
        {
            foreach (var w in WindowFinder.FindByTitle(pat))
            {
                if (seen.Add(w.Handle))
                    allWindows.Add(w);
            }
        }

        // Exclude self and ancestor processes
        if (!forceCloseAncestors)
        {
            var selfPids = GetSelfAndAncestorPids();
            allWindows.RemoveAll(w =>
            {
                NativeMethods.GetWindowThreadProcessId(w.Handle, out var pid);
                if (selfPids.Contains((int)pid))
                {
                    Console.WriteLine($"[A11Y] skip (ancestor pid={pid}): \"{w.Title}\" (hwnd={w.Handle:X8})");
                    return true;
                }
                return false;
            });
        }

        if (allWindows.Count == 0)
            return Error($"No window found: \"{win32Segments[0]}\"");

        // ═══ STEP 3: Select targets (--all / --nth / default first) ═══
        List<WindowInfo> targets;
        if (all)
        {
            targets = allWindows;
        }
        else if (nthRaw != null)
        {
            var parsed = ParseNthTargets(allWindows, nthRaw);
            if (parsed == null)
                return Error($"--nth {nthRaw}: invalid range or out of bounds (found {allWindows.Count} match(es))");
            targets = parsed;
        }
        else
        {
            targets = new List<WindowInfo> { allWindows[0] };
        }

        // ═══ STEP 4: Display matches ═══
        if (allWindows.Count > 1)
        {
            for (int idx = 0; idx < allWindows.Count; idx++)
            {
                var w = allWindows[idx];
                var r = w.Rect;
                var marker = targets.Contains(w) ? ">" : " ";
                Console.WriteLine($"[A11Y] {marker} #{idx + 1} [{w.ClassName}] \"{w.Title}\" (hwnd={w.Handle:X8} {r.Right - r.Left}x{r.Bottom - r.Top})");
            }
        }
        else
        {
            var w = targets[0];
            var r = w.Rect;
            Console.WriteLine($"[A11Y] matched: [{w.ClassName}] \"{w.Title}\" (hwnd={w.Handle:X8} {r.Right - r.Left}x{r.Bottom - r.Top})");
        }

        // ═══ STEP 5: Execute action on each target ═══
        int ok = 0, fail = 0;
        using var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);
        var readiness = CreateInputReadiness();

        foreach (var win in targets)
        {
            var hwnd = win.Handle;
            var title = win.Title;
            var tag = $"0x{hwnd.ToInt64():X} \"{title}\"";

            // Walk Win32 children (segments[1..] before '#')
            bool childError = false;
            for (int i = 1; i < win32Segments.Length; i++)
            {
                var child = WindowFinder.FindChildByPattern(hwnd, win32Segments[i]);
                if (child == null)
                {
                    Console.Error.WriteLine($"[A11Y] Win32 child not found: \"{win32Segments[i]}\" in {tag}");
                    childError = true;
                    break;
                }
                hwnd = child.Handle;
                tag = $"0x{hwnd.ToInt64():X} \"{WindowFinder.GetWindowText(hwnd)}\"";
            }
            if (childError) { fail++; continue; }

            // Blocker check
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.WriteLine($"[A11Y] blocker: {blocker.ClassName} \"{blocker.Title}\" — dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(300);
            }

            // Minimize restore (except close/minimize)
            if (action != "close" && action != "minimize")
            {
                if (NativeMethods.IsIconic(hwnd))
                {
                    Console.WriteLine($"[A11Y] {tag} minimized — restoring");
                    NativeMethods.ShowWindow(hwnd, 9 /* SW_RESTORE */);
                    Thread.Sleep(300);
                }
            }

            bool success;

            if (isElementAction)
            {
                // Resolve UIA scope for element-level actions
                AutomationElement root;
                try { root = automation.FromHandle(hwnd); }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[A11Y] UIA FromHandle failed for {tag}: {ex.Message}");
                    fail++;
                    continue;
                }

                if (!string.IsNullOrEmpty(uiaPath))
                {
                    var scoped = GrapHelper.FindUiaScope(root, uiaPath);
                    if (scoped == null)
                    {
                        Console.Error.WriteLine($"[A11Y] UIA scope not found: \"{uiaPath}\" in {tag}");
                        fail++;
                        continue;
                    }
                    root = scoped;
                }

                // Show resolved element info
                var elName = root.Properties.Name.ValueOrDefault ?? "";
                var elType = "?";
                try { elType = root.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                var elAid = root.Properties.AutomationId.ValueOrDefault ?? "";
                Console.WriteLine($"[A11Y] element: {elType} \"{elName}\" (aid=\"{elAid}\") in {tag}");

                success = action switch
                {
                    "invoke" => A11yInvoke(root, hwnd),
                    "click" => A11yClick(root, hwnd),
                    "toggle" => A11yToggle(root, hwnd),
                    "expand" => A11yExpand(root),
                    "collapse" => A11yCollapse(root),
                    "select" => A11ySelectItem(root),
                    "scroll" => A11yScrollAction(root, hwnd, scrollDir, scrollAmount),
                    "type" => A11yType(root, hwnd, text!),
                    "set-value" => A11ySetValue(root, hwnd, text!),
                    "set-range" => A11ySetRange(root, rangeValue!.Value),
                    "read" => A11yRead(root),
                    _ => A11yNotYet(action)
                };
            }
            else
            {
                // Window-level actions
                success = action switch
                {
                    "close" => A11yClose(automation, hwnd, tag, force, readiness),
                    "minimize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Minimized),
                    "maximize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Maximized),
                    "restore" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Normal),
                    "move" => A11yMove(automation, hwnd, tag, mx!.Value, my!.Value),
                    "resize" => A11yResize(automation, hwnd, tag, mw!.Value, mh!.Value),
                    "focus" => A11yFocus(hwnd, tag),
                    _ => A11yNotYet(action)
                };
            }

            if (success) ok++; else fail++;
        }

        Console.WriteLine($"[A11Y] Done: {ok} ok, {fail} fail (total {targets.Count})");
        return fail > 0 ? 1 : 0;
    }

    static bool A11yNotYet(string action)
    {
        Console.Error.WriteLine($"[A11Y] ERROR: '{action}' is not a valid action");
        Console.Error.WriteLine("[A11Y] Window: close, minimize, maximize, restore, move, resize, focus");
        Console.Error.WriteLine("[A11Y] Element: read, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range");
        return false;
    }

    // ═══ Element-Level Action Implementations ═══════════════

    // -- Invoke: UIA Invoke -> LegacyIA -> BM_CLICK -> WM_LBUTTON fallback --
    static bool A11yInvoke(AutomationElement el, IntPtr hwnd)
    {
        // Tier 1: UIA Invoke pattern (focusless)
        if (UiaLocator.TryInvoke(el))
        {
            Console.WriteLine("[A11Y] invoke — UIA Invoke");
            return true;
        }

        // Tier 2: BM_CLICK via hwnd (if element has NativeWindowHandle)
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.PostMessageW(elHwnd, 0x00F5 /* BM_CLICK */, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("[A11Y] invoke — Win32 BM_CLICK");
            return true;
        }

        // Tier 3: physical click at BoundingRect center
        return A11yClick(el, hwnd);
    }

    // -- Click: BoundingRect center -> SendInput (requires focus) --
    static bool A11yClick(AutomationElement el, IntPtr hwnd)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] click — no BoundingRect available");
            return false;
        }

        int cx = (rect.Value.Left + rect.Value.Right) / 2;
        int cy = (rect.Value.Top + rect.Value.Bottom) / 2;

        // Try LegacyIA DoDefaultAction first
        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                {
                    legacy.Pattern.DoDefaultAction();
                    Console.WriteLine($"[A11Y] click — LegacyIA DoDefaultAction at ({cx},{cy})");
                    return true;
                }
            }
        }
        catch { }

        // Win32: PostMessage WM_LBUTTONDOWN/UP (focusless if we have element hwnd)
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            var pt = new POINT { X = cx, Y = cy };
            NativeMethods.ScreenToClient(elHwnd, ref pt);
            var lParam = (IntPtr)(pt.X | (pt.Y << 16));
            NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
            Thread.Sleep(50);
            NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
            Console.WriteLine($"[A11Y] click — Win32 WM_LBUTTON at ({cx},{cy})");
            return true;
        }

        // Fallback: physical SendInput (needs focus)
        NativeMethods.SetCursorPos(cx, cy);
        Thread.Sleep(30);
        var inputs = new INPUT[2];
        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
        inputs[1].type = INPUT.INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
        NativeMethods.SendInput(2, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
        Console.WriteLine($"[A11Y] click — SendInput at ({cx},{cy})");
        return true;
    }

    // -- Toggle: UIA Toggle -> BM_CLICK fallback --
    static bool A11yToggle(AutomationElement el, IntPtr hwnd)
    {
        var before = UiaLocator.GetToggleState(el);
        if (before != null)
            Console.WriteLine($"[A11Y] toggle state before: {before}");

        if (UiaLocator.TryToggle(el))
        {
            var after = UiaLocator.GetToggleState(el);
            Console.WriteLine($"[A11Y] toggle — UIA Toggle (now: {after})");
            return true;
        }

        Console.WriteLine("[A11Y] toggle — UIA not supported, falling back to invoke");
        return A11yInvoke(el, hwnd);
    }

    // -- Expand: UIA ExpandCollapse --
    static bool A11yExpand(AutomationElement el)
    {
        if (UiaLocator.TryExpand(el))
        {
            Console.WriteLine("[A11Y] expand — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] expand — not supported on this element");
        return false;
    }

    // -- Collapse: UIA ExpandCollapse --
    static bool A11yCollapse(AutomationElement el)
    {
        if (UiaLocator.TryCollapse(el))
        {
            Console.WriteLine("[A11Y] collapse — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] collapse — not supported on this element");
        return false;
    }

    // -- Select: UIA SelectionItem --
    static bool A11ySelectItem(AutomationElement el)
    {
        if (UiaLocator.TrySelect(el))
        {
            Console.WriteLine("[A11Y] select — UIA SelectionItem");
            return true;
        }
        Console.WriteLine("[A11Y] select — UIA not supported, falling back to invoke");
        return UiaLocator.TryInvoke(el);
    }

    // -- Scroll: UIA Scroll -> WM_VSCROLL/WM_HSCROLL fallback --
    static bool A11yScrollAction(AutomationElement el, IntPtr hwnd, string direction, string amount)
    {
        var hAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;
        var vAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;

        var scrollAmt = amount == "large"
            ? FlaUI.Core.Definitions.ScrollAmount.LargeIncrement
            : FlaUI.Core.Definitions.ScrollAmount.SmallIncrement;

        var scrollAmtNeg = amount == "large"
            ? FlaUI.Core.Definitions.ScrollAmount.LargeDecrement
            : FlaUI.Core.Definitions.ScrollAmount.SmallDecrement;

        switch (direction)
        {
            case "down": vAmount = scrollAmt; break;
            case "up": vAmount = scrollAmtNeg; break;
            case "right": hAmount = scrollAmt; break;
            case "left": hAmount = scrollAmtNeg; break;
            default: return Error($"Invalid scroll direction: {direction}") != 0 ? false : false;
        }

        if (UiaLocator.TryScroll(el, hAmount, vAmount))
        {
            Console.WriteLine($"[A11Y] scroll {direction} ({amount}) — UIA Scroll");
            return true;
        }

        // Win32 fallback
        var elHwnd = GetElementHwnd(el);
        if (elHwnd == IntPtr.Zero) elHwnd = hwnd;

        uint msg = (direction == "left" || direction == "right") ? 0x0114u /* WM_HSCROLL */ : 0x0115u /* WM_VSCROLL */;
        int wParam = (direction == "down" || direction == "right") ? 1 /* SB_LINEDOWN */ : 0 /* SB_LINEUP */;
        if (amount == "large")
            wParam = (direction == "down" || direction == "right") ? 3 /* SB_PAGEDOWN */ : 2 /* SB_PAGEUP */;

        NativeMethods.PostMessageW(elHwnd, msg, (IntPtr)wParam, IntPtr.Zero);
        Console.WriteLine($"[A11Y] scroll {direction} ({amount}) — Win32 WM_{(msg == 0x0114u ? "H" : "V")}SCROLL");
        return true;
    }

    // -- Type: UIA Value -> WM_CHAR fallback (character by character) --
    static bool A11yType(AutomationElement el, IntPtr hwnd, string text)
    {
        // Tier 1: UIA ValuePattern (focusless, instant)
        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine($"[A11Y] type — UIA Value.SetValue ({text.Length} chars)");
                return true;
            }
        }
        catch { }

        // Tier 2: PostMessage WM_CHAR (focusless, per-character)
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            foreach (char c in text)
                NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
            Console.WriteLine($"[A11Y] type — Win32 WM_CHAR ({text.Length} chars)");
            return true;
        }

        // Tier 3: LegacyIAccessible SetValue
        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine($"[A11Y] type — LegacyIA SetValue ({text.Length} chars)");
                return true;
            }
        }
        catch { }

        Console.Error.WriteLine("[A11Y] type — no input method available (no Value/WM_CHAR/LegacyIA)");
        return false;
    }

    // -- SetValue: UIA Value -> WM_SETTEXT fallback (replace entire value) --
    static bool A11ySetValue(AutomationElement el, IntPtr hwnd, string text)
    {
        // Tier 1: UIA ValuePattern
        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — UIA Value.SetValue");
                return true;
            }
        }
        catch { }

        // Tier 2: Win32 WM_SETTEXT
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(elHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
            Console.WriteLine("[A11Y] set-value — Win32 WM_SETTEXT");
            return true;
        }

        // Tier 3: LegacyIAccessible
        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — LegacyIA SetValue");
                return true;
            }
        }
        catch { }

        Console.Error.WriteLine("[A11Y] set-value — no method available");
        return false;
    }

    // -- Read: dump element's accessible state (TTS friendly) --
    static bool A11yRead(AutomationElement el)
    {
        var lines = new List<string>();

        var name = el.Properties.Name.ValueOrDefault;
        if (!string.IsNullOrEmpty(name))
            lines.Add($"Name: {name}");

        try
        {
            var ct = el.Properties.ControlType.ValueOrDefault;
            lines.Add($"Type: {ct}");
        }
        catch { }

        var aid = el.Properties.AutomationId.ValueOrDefault;
        if (!string.IsNullOrEmpty(aid))
            lines.Add($"AutomationId: {aid}");

        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported)
            {
                var val = vp.Pattern.Value.ValueOrDefault ?? "";
                var ro = vp.Pattern.IsReadOnly.ValueOrDefault;
                lines.Add($"Value: \"{val}\"{(ro ? " (readonly)" : "")}");
            }
        }
        catch { }

        var toggle = UiaLocator.GetToggleState(el);
        if (toggle != null)
            lines.Add($"ToggleState: {toggle}");

        var sel = UiaLocator.IsSelected(el);
        if (sel != null)
            lines.Add($"IsSelected: {sel}");

        var ec = UiaLocator.GetExpandCollapseState(el);
        if (ec != null)
            lines.Add($"ExpandState: {ec}");

        var range = UiaLocator.GetRangeValueInfo(el);
        if (range != null)
            lines.Add($"Range: {range.Value} ({range.Minimum}..{range.Maximum}, step={range.SmallChange}{(range.IsReadOnly ? ", readonly" : "")})");

        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0)
                lines.Add($"Rect: ({(int)r.X},{(int)r.Y}) {(int)r.Width}x{(int)r.Height}");
        }
        catch { }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var desc = legacy.Pattern.Description.ValueOrDefault;
                if (!string.IsNullOrEmpty(desc))
                    lines.Add($"Description: {desc}");
                var help = legacy.Pattern.Help.ValueOrDefault;
                if (!string.IsNullOrEmpty(help))
                    lines.Add($"Help: {help}");
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                    lines.Add($"DefaultAction: {defAction}");
            }
        }
        catch { }

        try
        {
            var children = el.FindAllChildren();
            if (children.Length > 0)
            {
                lines.Add($"Children: {children.Length}");
                foreach (var child in children.Take(10))
                {
                    try
                    {
                        var cn = child.Properties.Name.ValueOrDefault ?? "";
                        var cct = "?";
                        try { cct = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var caid = child.Properties.AutomationId.ValueOrDefault ?? "";
                        var label = !string.IsNullOrEmpty(cn) ? $"\"{cn}\"" : (caid != "" ? $"aid={caid}" : "(unnamed)");
                        lines.Add($"  [{cct}] {label}");
                    }
                    catch { }
                }
                if (children.Length > 10)
                    lines.Add($"  ... +{children.Length - 10} more");
            }
        }
        catch { }

        if (lines.Count == 0)
        {
            Console.Error.WriteLine("[A11Y] read — no accessible information available");
            return false;
        }

        foreach (var line in lines)
            Console.WriteLine($"[A11Y] {line}");
        return true;
    }

    // -- SetRange: UIA RangeValue --
    static bool A11ySetRange(AutomationElement el, double value)
    {
        var info = UiaLocator.GetRangeValueInfo(el);
        if (info != null)
            Console.WriteLine($"[A11Y] range: {info.Minimum}..{info.Maximum} (current={info.Value}, step={info.SmallChange})");

        if (UiaLocator.TrySetRangeValue(el, value))
        {
            Console.WriteLine($"[A11Y] set-range — UIA RangeValue = {value}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] set-range — RangeValue not supported on this element");
        return false;
    }

    // ═══ Window-Level Action Implementations ═══════════════

    // -- Focus: SetForegroundWindow --
    static bool A11yFocus(IntPtr hwnd, string tag)
    {
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9);

        NativeMethods.SetForegroundWindow(hwnd);
        NativeMethods.BringWindowToTop(hwnd);
        Console.WriteLine($"[A11Y] focus {tag} — SetForegroundWindow");
        return true;
    }

    // -- Close: UIA WindowPattern.Close -> Win32 WM_CLOSE -> Process.Kill fallback --
    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag, bool force, InputReadiness readiness)
    {
        // Tier 1: UIA WindowPattern
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

        // Tier 2: Win32 WM_CLOSE
        try
        {
            NativeMethods.SendMessageTimeoutW(hwnd, 0x0010 /* WM_CLOSE */, IntPtr.Zero, IntPtr.Zero,
                0x0002 /* SMTO_ABORTIFHUNG */, 3000, out _);
            Thread.Sleep(1000);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE (closed)");
                return true;
            }
            // Tier 2.5: blocker dialog after WM_CLOSE
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
            Console.WriteLine($"[A11Y] close {tag} — WM_CLOSE sent but window still alive, --force -> Process.Kill");
        }
        catch (Exception ex)
        {
            if (!force)
            {
                Console.Error.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE failed ({ex.Message}), use --force to kill");
                return false;
            }
            Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE failed ({ex.Message}), --force -> Process.Kill");
        }

        // Tier 3: Process.Kill (--force only)
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

        int cmd = state switch
        {
            WindowVisualState.Minimized => 6,
            WindowVisualState.Maximized => 3,
            WindowVisualState.Normal => 9,
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
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanMove)
            {
                el.Patterns.Transform.Pattern.Move(x, y);
                Console.WriteLine($"[A11Y] move {tag} -> ({x},{y}) — UIA TransformPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] move {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] move {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 0x0015);
            Console.WriteLine($"[A11Y] move {tag} -> ({x},{y}) — Win32 SetWindowPos");
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
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanResize)
            {
                el.Patterns.Transform.Pattern.Resize(w, h);
                Console.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) — UIA TransformPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] resize {tag} — UIA not supported, trying Win32 fallback");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] resize {tag} — UIA failed ({ex.Message}), trying Win32 fallback");
        }

        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 0x0016);
            Console.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) — Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] resize {tag} — Win32 fallback FAIL: {ex.Message}");
            return false;
        }
    }

    // ═══ Helpers ═══════════════

    static IntPtr GetElementHwnd(AutomationElement el)
    {
        try
        {
            var nh = el.Properties.NativeWindowHandle.ValueOrDefault;
            if (nh != IntPtr.Zero) return nh;
        }
        catch { }
        return IntPtr.Zero;
    }

    static System.Drawing.Rectangle? GetBoundingRect(AutomationElement el)
    {
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0 && r.Height > 0)
                return new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Parse --nth range: "3" = single, "3~" = from 3 to end, "~3" = first to 3rd, "2~4" = range (1-based).
    /// Returns null on invalid input or out of bounds.
    /// </summary>
    static List<WindowInfo>? ParseNthTargets(List<WindowInfo> windows, string nthRaw)
    {
        // "~3" = from 1st to 3rd
        if (nthRaw.StartsWith('~'))
        {
            if (!int.TryParse(nthRaw[1..], out var to) || to < 1)
                return null;
            to = Math.Min(to, windows.Count);
            return windows.Take(to).ToList();
        }

        // "3~" = from 3rd to end
        if (nthRaw.EndsWith('~'))
        {
            if (!int.TryParse(nthRaw[..^1], out var from) || from < 1 || from > windows.Count)
                return null;
            return windows.Skip(from - 1).ToList();
        }

        // "2~4" = range (inclusive)
        if (nthRaw.Contains('~'))
        {
            var parts = nthRaw.Split('~');
            if (parts.Length != 2 ||
                !int.TryParse(parts[0], out var from) ||
                !int.TryParse(parts[1], out var to) ||
                from < 1 || to < from || from > windows.Count)
                return null;
            to = Math.Min(to, windows.Count);
            return windows.Skip(from - 1).Take(to - from + 1).ToList();
        }

        // "3" = single
        if (int.TryParse(nthRaw, out var idx) && idx >= 1 && idx <= windows.Count)
            return new List<WindowInfo> { windows[idx - 1] };

        return null;
    }

    /// <summary>Collect PIDs of self + ancestor processes (bash, code, claude, etc.).</summary>
    static HashSet<int> GetSelfAndAncestorPids()
    {
        var pids = new HashSet<int>();
        try
        {
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            pids.Add(proc.Id);
            int pid = proc.Id;
            for (int i = 0; i < 10 && pid > 0; i++)
            {
                int ppid = GetParentPid(pid);
                if (ppid <= 0 || !pids.Add(ppid)) break;
                pid = ppid;
            }
        }
        catch { }
        return pids;
    }

}
