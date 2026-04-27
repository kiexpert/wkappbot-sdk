using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
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

    // -- Grid-Read: clipboard bridge for owner-drawn grids (HTS/MFC) --
    //
    // MFC/GDI owner-drawn grids expose no UIA/MSAA tree nodes per cell, so
    // A11yRead returns only the container. This action drives the grid's
    // native "copy" path instead:
    //   1. Focus the target control (required for hotkeys to route to it)
    //   2. Clear the clipboard so stale data doesn't masquerade as a result
    //   3. Send Ctrl+A + Ctrl+C via AttachThreadInput-safe KeyboardInput
    //   4. Poll the clipboard for up to 2s
    //   5. Print captured text verbatim (typically TSV from HTS grids)
    //
    // Users who need a non-standard copy sequence can pass the chord via
    // --text (e.g. --text "ctrl+shift+c" for apps with a dedicated
    // "copy all" binding, or --text "ctrl+a,ctrl+c" to customize the
    // default chain).
    static bool A11yGridRead(IntPtr hwnd, string hotkeyOverride)
    {
        var hotkey = string.IsNullOrEmpty(hotkeyOverride) ? "ctrl+a,ctrl+c" : hotkeyOverride;

        // -- Clear clipboard (STA) --
        bool clearedOk = false;
        void DoClear()
        {
            try { System.Windows.Forms.Clipboard.Clear(); clearedOk = true; } catch { }
        }
        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) DoClear();
        else
        {
            var t = new Thread(DoClear); t.SetApartmentState(ApartmentState.STA); t.Start(); t.Join(1500);
        }
        if (!clearedOk)
            Console.Error.WriteLine("[A11Y] grid-read: clipboard clear failed (continuing)");

        // -- Focus + send hotkey sequence(s) --
        NativeMethods.SmartSetForegroundWindow(hwnd);
        Thread.Sleep(120);
        foreach (var chord in hotkey.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var keys = chord.Split('+', StringSplitOptions.RemoveEmptyEntries)
                            .Select(k => k.Trim()).ToArray();
            if (keys.Length == 0) continue;
            try { WKAppBot.Win32.Input.KeyboardInput.Hotkey(keys); }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[A11Y] grid-read: hotkey {chord} failed: {ex.Message}");
                return false;
            }
            Thread.Sleep(80);
        }

        // -- Poll clipboard (STA) up to 2s --
        string? captured = null;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < 2000 && captured == null)
        {
            void DoRead()
            {
                try
                {
                    if (System.Windows.Forms.Clipboard.ContainsText())
                    {
                        var t = System.Windows.Forms.Clipboard.GetText();
                        if (!string.IsNullOrEmpty(t)) captured = t;
                    }
                }
                catch { }
            }
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) DoRead();
            else
            {
                var t = new Thread(DoRead); t.SetApartmentState(ApartmentState.STA); t.Start(); t.Join(500);
            }
            if (captured == null) Thread.Sleep(100);
        }

        if (string.IsNullOrEmpty(captured))
        {
            Console.Error.WriteLine($"[A11Y] grid-read: clipboard empty after {sw.ElapsedMilliseconds}ms (hotkey={hotkey}). " +
                "Target may not respond to Ctrl+A/Ctrl+C -- try --hotkey with the app's actual copy binding.");
            return false;
        }

        Console.Error.WriteLine($"[A11Y] grid-read: {captured.Length} chars from clipboard ({sw.ElapsedMilliseconds}ms, hotkey={hotkey})");
        Console.Write(captured);
        if (!captured.EndsWith('\n')) Console.WriteLine();
        return true;
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
            Console.Error.WriteLine("[A11Y] read: no accessible information available");
            return false;
        }

        foreach (var line in lines)
            Console.Error.WriteLine($"[A11Y] {line}");
        return true;
    }
}
