using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// Element-level a11y action implementations (partial class of Program)
// Split from A11yCommand.cs per ~400 line file size policy
internal partial class Program
{
    // -- Highlight: show zoom/highlight overlay on target element --
    static bool A11yHighlight(AutomationElement el, IntPtr hwnd, int durationMs = 3000)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] highlight — no BoundingRect available");
            return false;
        }

        var name = el.Properties.Name.ValueOrDefault ?? "";
        var type = "?";
        try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";

        // Try element hwnd first (better capture), fall back to screen rect
        var elHwnd = GetElementHwnd(el);
        ClickZoomHelper? zoom;
        if (elHwnd != IntPtr.Zero)
            zoom = ClickZoomHelper.Begin(elHwnd, hwnd, "a11y-highlight", $"{type} \"{name}\"");
        else
            zoom = ClickZoomHelper.BeginFromRect(rect.Value, hwnd, "a11y-highlight", $"{type} \"{name}\"");

        if (zoom == null)
        {
            // Fallback: just print position info without overlay
            Console.WriteLine($"[A11Y] highlight — overlay failed, element at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");
            return true;
        }

        var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : (aid != "" ? $"aid={aid}" : "(unnamed)");
        zoom.UpdateStatus($"[{type}] {label}");
        Console.WriteLine($"[A11Y] highlight — [{type}] {label} at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");

        // Show for duration then fade out
        zoom.ShowPass($"{type} {label}");
        Thread.Sleep(durationMs);
        zoom.Dispose();
        return true;
    }

    // -- Find: list Win32 child windows + UIA children tree (MUD game "look" command) --
    static bool A11yFind(AutomationElement root, IntPtr hwnd, int depth)
    {
        bool hasOutput = false;

        // Part 1: Win32 child windows
        var win32Children = WindowFinder.GetChildrenZOrder(hwnd);
        if (win32Children.Count > 0)
        {
            hasOutput = true;
            Console.WriteLine($"[A11Y] Win32 children ({win32Children.Count}):");
            foreach (var child in win32Children)
            {
                var r = child.Rect;
                var w = r.Right - r.Left;
                var h = r.Bottom - r.Top;
                var vis = NativeMethods.IsWindowVisible(child.Handle) ? "" : " [hidden]";
                Console.WriteLine($"[A11Y]   [{child.ClassName}] \"{child.Title}\" hwnd={child.Handle:X8} cid={child.ControlId} {w}x{h}{vis}");
            }
        }

        // Part 2: UIA children tree
        try
        {
            var children = root.FindAllChildren();
            if (children.Length > 0)
            {
                hasOutput = true;
                Console.WriteLine($"[A11Y] UIA children ({children.Length}):");
                foreach (var child in children)
                {
                    DumpUiaElement(child, 1, depth);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] UIA tree error: {ex.Message}");
        }

        if (!hasOutput)
            Console.WriteLine("[A11Y] find — no children found");

        return hasOutput;
    }

    /// <summary>
    /// Recursively dump UIA element with detail: Type, Name, AutomationId, Rect, Patterns.
    /// </summary>
    static void DumpUiaElement(AutomationElement el, int level, int maxDepth)
    {
        if (level > maxDepth) return;

        var indent = new string(' ', level * 2);
        try
        {
            var name = el.Properties.Name.ValueOrDefault ?? "";
            var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
            var type = "?";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }

            // BoundingRect
            var rectStr = "";
            try
            {
                var r = el.Properties.BoundingRectangle.ValueOrDefault;
                if (r.Width > 0)
                    rectStr = $" ({(int)r.X},{(int)r.Y} {(int)r.Width}x{(int)r.Height})";
            }
            catch { }

            // Supported patterns (compact)
            var patterns = GetSupportedPatternNames(el);
            var patStr = patterns.Count > 0 ? $" ({string.Join(",", patterns)})" : "";

            // NativeWindowHandle
            var nhStr = "";
            var nh = GetElementHwnd(el);
            if (nh != IntPtr.Zero)
                nhStr = $" hwnd={nh:X8}";

            // Label: prefer name, then aid
            var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : "";
            var aidStr = !string.IsNullOrEmpty(aid) ? $" aid=\"{aid}\"" : "";

            Console.WriteLine($"[A11Y] {indent}[{type}] {label}{aidStr}{nhStr}{rectStr}{patStr}");

            // Recurse children
            if (level < maxDepth)
            {
                try
                {
                    foreach (var child in el.FindAllChildren())
                        DumpUiaElement(child, level + 1, maxDepth);
                }
                catch { }
            }
        }
        catch { /* COM timeout on some elements */ }
    }

    /// <summary>Get compact list of supported UIA pattern names.</summary>
    static List<string> GetSupportedPatternNames(AutomationElement el)
    {
        var names = new List<string>();
        try { if (el.Patterns.Invoke.IsSupported) names.Add("Invoke"); } catch { }
        try { if (el.Patterns.Value.IsSupported) names.Add("Value"); } catch { }
        try { if (el.Patterns.Toggle.IsSupported) names.Add("Toggle"); } catch { }
        try { if (el.Patterns.SelectionItem.IsSupported) names.Add("Select"); } catch { }
        try { if (el.Patterns.ExpandCollapse.IsSupported) names.Add("Expand"); } catch { }
        try { if (el.Patterns.Scroll.IsSupported) names.Add("Scroll"); } catch { }
        try { if (el.Patterns.RangeValue.IsSupported) names.Add("Range"); } catch { }
        try { if (el.Patterns.Window.IsSupported) names.Add("Window"); } catch { }
        try { if (el.Patterns.Transform.IsSupported) names.Add("Transform"); } catch { }
        try { if (el.Patterns.Grid.IsSupported) names.Add("Grid"); } catch { }
        try { if (el.Patterns.LegacyIAccessible.IsSupported) names.Add("LegacyIA"); } catch { }
        return names;
    }

    // -- Invoke: UIA Invoke -> LegacyIA -> BM_CLICK -> WM_LBUTTON fallback --
    static bool A11yInvoke(AutomationElement el, IntPtr hwnd)
    {
        if (UiaLocator.TryInvoke(el))
        {
            Console.WriteLine("[A11Y] invoke — UIA Invoke");
            return true;
        }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.PostMessageW(elHwnd, 0x00F5 /* BM_CLICK */, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("[A11Y] invoke — Win32 BM_CLICK");
            return true;
        }

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

    // -- Expand/Collapse: UIA ExpandCollapse --
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
        var hAmount = ScrollAmount.NoAmount;
        var vAmount = ScrollAmount.NoAmount;

        var scrollAmt = amount == "large" ? ScrollAmount.LargeIncrement : ScrollAmount.SmallIncrement;
        var scrollAmtNeg = amount == "large" ? ScrollAmount.LargeDecrement : ScrollAmount.SmallDecrement;

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

        var elHwnd = GetElementHwnd(el);
        if (elHwnd == IntPtr.Zero) elHwnd = hwnd;

        uint msg = (direction == "left" || direction == "right") ? 0x0114u : 0x0115u;
        int wParam = (direction == "down" || direction == "right") ? 1 : 0;
        if (amount == "large")
            wParam = (direction == "down" || direction == "right") ? 3 : 2;

        NativeMethods.PostMessageW(elHwnd, msg, (IntPtr)wParam, IntPtr.Zero);
        Console.WriteLine($"[A11Y] scroll {direction} ({amount}) — Win32 WM_{(msg == 0x0114u ? "H" : "V")}SCROLL");
        return true;
    }

    // -- Type: UIA Value -> WM_CHAR -> LegacyIA SetValue --
    static bool A11yType(AutomationElement el, IntPtr hwnd, string text)
    {
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

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            foreach (char c in text)
                NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
            Console.WriteLine($"[A11Y] type — Win32 WM_CHAR ({text.Length} chars)");
            return true;
        }

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

        Console.Error.WriteLine("[A11Y] type — no input method available");
        return false;
    }

    // -- SetValue: UIA Value -> WM_SETTEXT -> LegacyIA --
    static bool A11ySetValue(AutomationElement el, IntPtr hwnd, string text)
    {
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

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(elHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
            Console.WriteLine("[A11Y] set-value — Win32 WM_SETTEXT");
            return true;
        }

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

    // ═══ Element Helpers ═══

    /// <summary>
    /// Ensure target element's tab is active. Walks up UIA parent chain looking for
    /// unselected TabItem ancestors → auto-select them (focusless UIA SelectionItem).
    /// Returns true if a tab was activated (caller may need to re-resolve scope).
    /// </summary>
    static bool EnsureTabActive(AutomationElement el)
    {
        try
        {
            var current = el;
            for (int i = 0; i < 20; i++) // max 20 levels up
            {
                AutomationElement? parent;
                try { parent = current.Parent; } catch { break; }
                if (parent == null) break;

                try
                {
                    var ct = parent.Properties.ControlType.ValueOrDefault;
                    if (ct == ControlType.TabItem)
                    {
                        // Check if this TabItem is selected
                        try
                        {
                            var selPat = parent.Patterns.SelectionItem;
                            if (selPat.IsSupported && !selPat.Pattern.IsSelected.Value)
                            {
                                var tabName = parent.Properties.Name.ValueOrDefault ?? "(unnamed)";
                                selPat.Pattern.Select();
                                Console.WriteLine($"[A11Y] tab activated: \"{tabName}\"");
                                Thread.Sleep(200);
                                return true;
                            }
                        }
                        catch { }
                    }
                }
                catch { }

                current = parent;
            }
        }
        catch { }
        return false;
    }

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
}
