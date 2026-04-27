using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;
using FlaUI.Core.AutomationElements;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    private bool CheckElementVisible(TargetDefinition? target)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        return el != null && !el.IsOffscreen;
    }

    private bool CheckElementEnabled(TargetDefinition? target)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        return el != null && el.IsEnabled;
    }

    private bool CheckElementFocused(TargetDefinition? target)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        return el != null && el.Properties.HasKeyboardFocus.ValueOrDefault;
    }

    /// <summary>ValuePattern.Value contains substring (case-insensitive).</summary>
    private bool CheckValueContains(TargetDefinition? target, string value)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        if (el == null) return false;
        return ReadElementValue(el).Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>ValuePattern.Value equals (case-insensitive).</summary>
    private bool CheckValueEquals(TargetDefinition? target, string value)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        if (el == null) return false;
        return ReadElementValue(el).Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Read an element's text value: UIA ValuePattern.Value first (edit controls,
    /// the canonical "value"), then TextPattern.DocumentRange.GetText as a fallback
    /// for rich-text documents like Windows 11 Notepad RichEditD2DPT where
    /// ValuePattern returns empty/stale. Name property is used as final label fallback.
    /// </summary>
    private static string ReadElementValue(AutomationElement el)
    {
        try { if (el.Patterns.Value.IsSupported) { var v = el.Patterns.Value.Pattern.Value.Value; if (!string.IsNullOrEmpty(v)) return v; } } catch { }
        try { if (el.Patterns.Text.IsSupported) { var t = el.Patterns.Text.Pattern.DocumentRange.GetText(-1); if (!string.IsNullOrEmpty(t)) return t; } } catch { }
        return el.Properties.Name.ValueOrDefault ?? "";
    }

    private bool CheckWindowPresent(TargetDefinition? target)
    {
        if (target == null) return false;
        var name = target.Name ?? target.AutomationId ?? "";
        var windows = WindowFinder.FindWindows(name);
        return windows.Count > 0;
    }

    /// <summary>UIA TogglePattern.ToggleState matches expected on/off.</summary>
    private bool CheckToggleState(TargetDefinition? target, bool expectedOn)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        if (el == null) return false;
        try
        {
            if (!el.Patterns.Toggle.IsSupported) return false;
            var state = el.Patterns.Toggle.Pattern.ToggleState.Value;
            return expectedOn
                ? state == FlaUI.Core.Definitions.ToggleState.On
                : state == FlaUI.Core.Definitions.ToggleState.Off;
        }
        catch { return false; }
    }

    /// <summary>UIA SelectionItemPattern.IsSelected.</summary>
    private bool CheckSelected(TargetDefinition? target)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        if (el == null) return false;
        try
        {
            return el.Patterns.SelectionItem.IsSupported
                && el.Patterns.SelectionItem.Pattern.IsSelected.Value;
        }
        catch { return false; }
    }

    /// <summary>UIA ExpandCollapsePattern.ExpandCollapseState matches expanded/collapsed.</summary>
    private bool CheckExpandState(TargetDefinition? target, bool expanded)
    {
        if (target == null) return false;
        var (el, _) = LocateElement(new StepDefinition { Target = target, Action = "assert", Name = "_expect" });
        if (el == null) return false;
        try
        {
            if (!el.Patterns.ExpandCollapse.IsSupported) return false;
            var state = el.Patterns.ExpandCollapse.Pattern.ExpandCollapseState.Value;
            return expanded
                ? state == FlaUI.Core.Definitions.ExpandCollapseState.Expanded
                : state == FlaUI.Core.Definitions.ExpandCollapseState.Collapsed;
        }
        catch { return false; }
    }
}
