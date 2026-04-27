using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Click actions ------------------------------------------

    private string DoClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "click", elemDesc);

            // Focusless path: UIA Invoke (no focus needed -- user undisturbed!)
            if (_ctx.PreferFocusless && UiaLocator.TryInvoke(element))
            {
                result.ActionDetail = $"Click {elemDesc} (Invoke, {method}, focusless)";
                Log($"  Invoked via UIA ({method}, focusless)");
                return method!;
            }

            if (center != null)
            {
                // SendInput path: pre-verify -> focus -> post-verify -> click
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.Click(center.Value.x, center.Value.y);
                result.ActionDetail = $"Click {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                Log($"  Clicked at ({center.Value.x},{center.Value.y}) via UIA ({method}, {verify})");
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "click", "coordinate");
            // SendInput path: pre-verify -> focus -> post-verify -> click
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.Click(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"Click ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            Log($"  Clicked at ({step.Target.X},{step.Target.Y}) via coordinate ({verify})");
            return "coordinate";
        }

        throw new InvalidOperationException(
            $"Cannot locate element for click: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");
    }

    private string DoDoubleClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "double_click", elemDesc);
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.DoubleClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"DblClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "double_click", "coordinate");
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.DoubleClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"DblClick ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for double_click");
    }

    private string DoRightClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "right_click", elemDesc);
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.RightClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"RightClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "right_click", "coordinate");
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.RightClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"RightClick ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for right_click");
    }
}
