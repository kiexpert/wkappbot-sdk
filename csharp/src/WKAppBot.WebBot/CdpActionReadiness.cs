using WKAppBot.Abstractions;

namespace WKAppBot.WebBot;

/// <summary>
/// Action-Aware Readiness (AAR) for Chrome DevTools Protocol (CDP) web elements.
/// Lightweight pipeline: Pass-through -> Target validation -> Action-specific.
///
/// Return convention:
///   null        -> blocked (hard fail)
///   == target   -> success (proceed)
///   != target   -> retarget (not used for CDP yet)
///
/// Tag: [AAR]
/// </summary>
public sealed class CdpActionReadiness : IActionReadiness
{
    public IActionTarget? Ensure(string action, IActionTarget target, ReadinessContext ctx)
    {
        var act = action.ToLowerInvariant();

        // -- Pass-through actions --
        if (IsPassThrough(act))
            return target;

        // -- Stage 0: Global -- no lock screen check for web (handled by Windows AAR above) --

        // -- Stage 1: Target validation (--force bypasses visible/enabled/readonly soft gates) --
        if (!target.Visible)
        {
            if (act is "type" or "set-value" or "click" or "invoke" && !ctx.Force)
            {
                Console.Error.WriteLine($"[AAR:CDP] BLOCKED: target not visible (hidden/display:none) for {action}: \"{target.DisplayName}\" (hint: --force to override)");
                return null;
            }
            Console.Error.WriteLine($"[AAR:CDP] WARNING: target not visible for {action}: \"{target.DisplayName}\"");
        }

        if (!target.Enabled)
        {
            if (act is "type" or "set-value" or "click" or "invoke" or "toggle" or "select" && !ctx.Force)
            {
                Console.Error.WriteLine($"[AAR:CDP] BLOCKED: target disabled for {action}: \"{target.DisplayName}\" (hint: --force to override)");
                return null;
            }
        }

        // -- Stage 2: Action-specific --
        if (target is CdpActionTarget cdpTarget)
        {
            switch (act)
            {
                case "type" or "set-value":
                    if (cdpTarget.IsReadOnly && !ctx.Force)
                    {
                        Console.Error.WriteLine($"[AAR:CDP] BLOCKED: target is readonly for {action}: \"{target.DisplayName}\" (hint: --force to override)");
                        return null;
                    }
                    // Warn if not a typical input element
                    if (cdpTarget.TagName is not ("INPUT" or "TEXTAREA" or "DIV"))
                    {
                        Console.Error.WriteLine($"[AAR:CDP] WARNING: unusual tag <{cdpTarget.TagName}> for {action}: \"{target.DisplayName}\"");
                    }
                    break;

                case "toggle":
                    // Warn if not a checkbox/radio/switch
                    if (cdpTarget.InputType is not ("checkbox" or "radio"))
                    {
                        Console.Error.WriteLine($"[AAR:CDP] WARNING: not checkbox/radio for toggle: \"{target.DisplayName}\" (type={cdpTarget.InputType})");
                    }
                    break;

                case "select":
                    if (cdpTarget.TagName is not ("SELECT"))
                    {
                        Console.Error.WriteLine($"[AAR:CDP] WARNING: not <select> for select action: \"{target.DisplayName}\" (tag={cdpTarget.TagName})");
                    }
                    break;
            }
        }

        Console.Error.WriteLine($"[AAR:CDP] OK: {action} on \"{target.DisplayName}\"");
        return target;
    }

    private static bool IsPassThrough(string action)
        => action is "read" or "find" or "inspect" or "highlight"
            or "windows" or "screenshot" or "ocr" or "eval";
}
