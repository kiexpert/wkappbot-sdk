using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

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

    // -- Find: CURSOR → TARGET → VERDICT structured output --
    // stdout: clean result sections only
    // stderr: diagnostic/processing info ([DIAG:find])
    static bool A11yFind(AutomationElement root, IntPtr hwnd, int depth)
    {
        // ── CURSOR ──────────────────────────────────────────────────
        Console.WriteLine("━━━ CURSOR ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        FocusedElementInfo? focInfo = null;
        try { using var loc = new UiaLocator(); focInfo = loc.GetFocusedElementInfo(); }
        catch (Exception ex) { Console.Error.WriteLine($"[DIAG:find] focus error: {ex.Message}"); }

        bool cursorIsBlocking = false;
        if (focInfo != null)
        {
            var curTag = GrapHelper.FormatNodeLabel(focInfo.ControlType, focInfo.AutomationId, focInfo.Name);
            Console.WriteLine($"  {curTag}");
            foreach (var (pType, pName) in focInfo.ParentChain)
            {
                if (string.IsNullOrEmpty(pType)) continue;
                var pTag = GrapHelper.FormatNodeLabel(pType, "", pName);
                Console.WriteLine($"    ← {pTag}");
                if (pType == "Dialog") cursorIsBlocking = true;
            }
        }
        else
        {
            Console.WriteLine("  (no focused element)");
        }

        // ── TARGET ──────────────────────────────────────────────────
        Console.WriteLine("━━━ TARGET ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

        var targetName = root.Properties.Name.ValueOrDefault ?? "";
        var targetAid  = root.Properties.AutomationId.ValueOrDefault ?? "";
        var targetType = "?"; try { targetType = root.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        var patterns   = GetSupportedPatternNames(root);

        var targetTag = GrapHelper.FormatNodeLabel(targetType, targetAid, targetName, actions: patterns);
        Console.WriteLine($"  {targetTag}");

        string targetGrap = "";
        try { targetGrap = GrapHelper.BuildGrapExpression(hwnd, root); } catch { }
        if (!string.IsNullOrEmpty(targetGrap))
            Console.WriteLine($"  grap: {targetGrap}");

        try
        {
            var r = root.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0)
                Console.WriteLine($"  rect: ({(int)r.X},{(int)r.Y}) {(int)r.Width}x{(int)r.Height}");
        }
        catch { }

        // parent + siblings
        try
        {
            var parent = root.Parent;
            if (parent != null)
            {
                Console.WriteLine($"  parent: {GrapHelper.FormatNodeLabel(parent)}");

                var siblings = parent.FindAllChildren()
                    .Where(c => {
                        try { return !(c.Properties.Name.ValueOrDefault == targetName
                                    && c.Properties.AutomationId.ValueOrDefault == targetAid); }
                        catch { return true; }
                    })
                    .Take(8).ToList();

                if (siblings.Count > 0)
                {
                    var sibTags = siblings.Select(s => GrapHelper.FormatNodeLabel(s));
                    Console.WriteLine($"  siblings: {string.Join(" · ", sibTags)}");
                }
            }
        }
        catch (Exception ex) { Console.Error.WriteLine($"[DIAG:find] parent/siblings error: {ex.Message}"); }

        // ── VERDICT ─────────────────────────────────────────────────
        Console.WriteLine("━━━ VERDICT ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        if (cursorIsBlocking)
            Console.WriteLine("  ⚠ cursor in Dialog — verify target is reachable");
        else if (focInfo == null)
            Console.WriteLine("  ? focus unknown");
        else
            Console.WriteLine("  ✓ cursor accessible");

        // Win32 children → stderr only
        var win32Children = WindowFinder.GetChildrenZOrder(hwnd);
        if (win32Children.Count > 0)
        {
            Console.Error.WriteLine($"[DIAG:find] Win32 children ({win32Children.Count}):");
            foreach (var child in win32Children)
            {
                var cr = child.Rect;
                var cw = cr.Right - cr.Left;
                var ch = cr.Bottom - cr.Top;
                var vis = NativeMethods.IsWindowVisible(child.Handle) ? "" : " [hidden]";
                Console.Error.WriteLine($"[DIAG:find]   [{child.ClassName}] \"{child.Title}\" hwnd={child.Handle:X8} cid={child.ControlId} {cw}x{ch}{vis}");
            }
        }

        return true;
    }

    /// <summary>
    /// Recursively dump UIA element tree to stderr (diagnostic use only).
    /// </summary>
    static void DumpUiaElement(AutomationElement el, int level, int maxDepth)
    {
        if (level > maxDepth) return;
        var indent = new string(' ', level * 2);
        try
        {
            var patterns = GetSupportedPatternNames(el);
            var tag = GrapHelper.FormatNodeLabel(el, includeRect: false);
            var nhStr = "";
            var nh = GetElementHwnd(el);
            if (nh != IntPtr.Zero) nhStr = $" hwnd={nh:X8}";
            var patStr = patterns.Count > 0 ? $" ({string.Join(",", patterns)})" : "";
            Console.Error.WriteLine($"[DIAG:uia] {indent}{tag}{nhStr}{patStr}");
            if (level < maxDepth)
                try { foreach (var child in el.FindAllChildren()) DumpUiaElement(child, level + 1, maxDepth); } catch { }
        }
        catch { }
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

    // -- Read: dump element's accessible state (TTS friendly) --
    static bool A11yRead(AutomationElement el)
    {
        var lines = new List<string>();

        var name = el.Properties.Name.ValueOrDefault ?? "";
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
        string controlType = "?";
        try { controlType = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        System.Drawing.Rectangle? rect = null;
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0) rect = new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
        catch { }
        lines.Add($"Tag: {GrapHelper.FormatNodeLabel(controlType, aid, name, rect: rect)}");
        if (!string.IsNullOrEmpty(name))
            lines.Add($"Name: {name}");
        lines.Add($"Type: {controlType}");
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
                        lines.Add($"  {GrapHelper.FormatNodeLabel(cct, caid, cn)}");
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
}
