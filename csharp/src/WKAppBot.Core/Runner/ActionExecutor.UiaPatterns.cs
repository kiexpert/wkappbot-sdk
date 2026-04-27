using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Scroll ------------------------------------------------─

    private void DoScroll(StepDefinition step, StepResult result)
    {
        var direction = step.Params?.Direction ?? "down";
        var amount = step.Params?.Amount ?? 3;

        // -- Try UIA Scroll pattern first (focusless!) --
        if (_ctx.PreferFocusless && step.Target != null)
        {
            try
            {
                var (element, method) = LocateElement(step);
                EnsureInputReady(element, step.Action);
                BeginZoomForElement(element, step);
                if (element != null)
                {
                    var hAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;
                    var vAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;

                    switch (direction.ToLowerInvariant())
                    {
                        case "down":
                            vAmount = amount > 3
                                ? FlaUI.Core.Definitions.ScrollAmount.LargeIncrement
                                : FlaUI.Core.Definitions.ScrollAmount.SmallIncrement;
                            break;
                        case "up":
                            vAmount = amount > 3
                                ? FlaUI.Core.Definitions.ScrollAmount.LargeDecrement
                                : FlaUI.Core.Definitions.ScrollAmount.SmallDecrement;
                            break;
                        case "right":
                            hAmount = FlaUI.Core.Definitions.ScrollAmount.SmallIncrement;
                            break;
                        case "left":
                            hAmount = FlaUI.Core.Definitions.ScrollAmount.SmallDecrement;
                            break;
                    }

                    if (UiaLocator.TryScroll(element, hAmount, vAmount))
                    {
                        var elemDesc = step.Target.AutomationId ?? step.Target.Name ?? "?";
                        result.ActionDetail = $"Scroll {direction} {amount} on {elemDesc} (UIA Scroll, {method}, focusless)";
                        Log($"  Scrolled {direction} via UIA Scroll ({method}, focusless)");
                        return;
                    }
                }
            }
            catch { /* UIA Scroll not available -- fall through to SendInput */ }
        }

        // -- Fallback: SendInput mouse wheel (requires focus) --
        int clicks = direction.ToLowerInvariant() == "up" ? amount : -amount;
        EnsureFocus();
        MouseInput.Scroll(clicks);
        result.ActionDetail = $"Scroll {direction} {amount}";
        Log($"  Scrolled {direction} {amount}");
    }

    // -- UIA Pattern actions (all focusless!) --------------------─

    /// <summary>
    /// Toggle a checkbox or toggle button.
    /// Focusless via UIA Toggle pattern, click fallback.
    /// </summary>
    private void DoToggle(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for toggle: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        // Read current state first
        var beforeState = UiaLocator.GetToggleState(element);
        var stateStr = beforeState?.ToString() ?? "?";

        // If specific state requested, use TrySetToggle
        if (step.Params?.Checked != null)
        {
            if (UiaLocator.TrySetToggle(element, step.Params.Checked.Value))
            {
                var afterState = UiaLocator.GetToggleState(element);
                result.ActionDetail = $"Toggle {elemDesc} -> {(step.Params.Checked.Value ? "ON" : "OFF")} ({method}, focusless)";
                Log($"  Toggle set to {(step.Params.Checked.Value ? "ON" : "OFF")} via UIA ({method}, focusless) [{stateStr} -> {afterState}]");
                return;
            }
        }
        else
        {
            // Just toggle (no specific target state)
            if (UiaLocator.TryToggle(element))
            {
                var afterState = UiaLocator.GetToggleState(element);
                result.ActionDetail = $"Toggle {elemDesc} ({method}, focusless) [{stateStr} -> {afterState}]";
                Log($"  Toggled via UIA ({method}, focusless) [{stateStr} -> {afterState}]");
                return;
            }
        }

        // Fallback: click the element
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"Toggle {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            Log($"  Toggled via click fallback ({verify})");
            return;
        }

        throw new InvalidOperationException($"Toggle failed: no UIA Toggle pattern and no click target for {elemDesc}");
    }

    /// <summary>
    /// Focusless UIA Invoke -- click a button/menu without stealing focus.
    /// Pure COM call: IUIAutomationInvokePattern::Invoke().
    /// </summary>
    private void DoInvoke(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for invoke: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        BeginZoomForElement(element, step);

        if (UiaLocator.TryInvoke(element))
        {
            result.ActionDetail = $"Invoke {elemDesc} ({method}, focusless COM)";
            Log($"  Invoked via UIA ({method}, focusless COM)");
            return;
        }

        // Fallback: click
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"Invoke {elemDesc} (click fallback)";
            Log($"  Invoke fallback -> click at ({center.Value.x},{center.Value.y})");
            return;
        }

        throw new InvalidOperationException($"Invoke failed: no UIA Invoke pattern and no click target for {elemDesc}");
    }

    /// <summary>
    /// Focusless UIA SetValue -- type text without focus or keyboard.
    /// Pure COM call: IUIAutomationValuePattern::SetValue().
    /// </summary>
    private void DoSetValue(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for set_value: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");

        var text = _ctx.Resolve(step.Params?.Text ?? "");
        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        BeginZoomForElement(element, step);

        // UIA ValuePattern.SetValue -- pure COM, focusless.
        // Win11 RichEditD2DPT and Chrome inputs cache ValuePattern.Value and don't
        // sync immediately after SetValue. Brief sleep + TextPattern fallback read-back
        // detect the stale case early so expect/assert steps don't see empty values.
        try
        {
            var vp = element.Patterns.Value;
            if (vp.IsSupported)
            {
                vp.Pattern.SetValue(text);
                System.Threading.Thread.Sleep(50);
                var readBack = ReadElementValue(element);
                if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(readBack))
                    Log($"  SetValue via UIA: value appears empty on read-back (stale cache?) -- control may still have the value");
                result.ActionDetail = $"SetValue \"{text}\" on {elemDesc} ({method}, focusless COM)";
                Log($"  SetValue via UIA ({method}, focusless COM): \"{text}\"");
                return;
            }
        }
        catch { }

        throw new InvalidOperationException($"SetValue failed: no UIA Value pattern on {elemDesc}. Use type_text for SendInput fallback.");
    }

    /// <summary>
    /// Expand or collapse a tree node, combo box, or group.
    /// Focusless via UIA ExpandCollapse pattern.
    /// </summary>
    private void DoExpandCollapse(StepDefinition step, StepResult result, bool expand)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for {(expand ? "expand" : "collapse")}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        var action = expand ? "Expand" : "Collapse";

        var beforeState = UiaLocator.GetExpandCollapseState(element);

        bool success = expand
            ? UiaLocator.TryExpand(element)
            : UiaLocator.TryCollapse(element);

        if (success)
        {
            var afterState = UiaLocator.GetExpandCollapseState(element);
            result.ActionDetail = $"{action} {elemDesc} ({method}, focusless) [{beforeState} -> {afterState}]";
            Log($"  {action}ed via UIA ({method}, focusless) [{beforeState} -> {afterState}]");
            return;
        }

        // Fallback: click + arrow key for expand, click for collapse
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"{action} {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            return;
        }

        throw new InvalidOperationException($"{action} failed for {elemDesc}");
    }

    /// <summary>
    /// Select an item in a list, combo, or tab.
    /// Focusless via UIA SelectionItem pattern.
    /// Params: itemText (find child by name), or target directly identifies the item.
    /// </summary>
    private void DoSelect(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for select");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        // If itemText specified, find the child item to select
        if (step.Params?.ItemText != null)
        {
            // Search children for the item with matching name
            var itemText = step.Params.ItemText;
            FlaUI.Core.AutomationElements.AutomationElement? item = null;

            try
            {
                var children = element.FindAllChildren();
                foreach (var child in children)
                {
                    try
                    {
                        if (string.Equals(child.Name, itemText, StringComparison.OrdinalIgnoreCase))
                        {
                            item = child;
                            break;
                        }
                    }
                    catch { }
                }
            }
            catch { }

            if (item != null && UiaLocator.TrySelect(item))
            {
                result.ActionDetail = $"Select \"{itemText}\" in {elemDesc} ({method}, focusless)";
                Log($"  Selected \"{itemText}\" via UIA SelectionItem ({method}, focusless)");
                return;
            }

            throw new InvalidOperationException($"Cannot select item \"{itemText}\" in {elemDesc}");
        }

        // Direct selection (target is the item itself)
        if (UiaLocator.TrySelect(element))
        {
            result.ActionDetail = $"Select {elemDesc} ({method}, focusless)";
            Log($"  Selected via UIA SelectionItem ({method}, focusless)");
            return;
        }

        // Fallback: click
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"Select {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            return;
        }

        throw new InvalidOperationException($"Select failed for {elemDesc}");
    }

    /// <summary>
    /// Set a range value (slider, progress bar, spinner).
    /// Focusless via UIA RangeValue pattern.
    /// </summary>
    private void DoSetRange(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for set_range");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        var value = step.Params?.Value ?? throw new InvalidOperationException("set_range requires params.value");

        var before = UiaLocator.GetRangeValueInfo(element);
        if (UiaLocator.TrySetRangeValue(element, value))
        {
            var after = UiaLocator.GetRangeValueInfo(element);
            result.ActionDetail = $"SetRange {elemDesc} -> {value} ({method}, focusless) [{before?.Value} -> {after?.Value}]";
            Log($"  Set range to {value} via UIA RangeValue ({method}, focusless)");
            return;
        }

        throw new InvalidOperationException($"SetRange failed for {elemDesc} (value={value}, range={before})");
    }
}
