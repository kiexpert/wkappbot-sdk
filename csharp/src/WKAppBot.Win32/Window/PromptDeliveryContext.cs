using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// What the caller wants to do with the prompt.
/// </summary>
public enum PromptAction
{
    TypeAndSubmit,  // type text + press Enter
    TypeOnly,       // type text, do NOT submit (leave in prompt for user review)
    Submit,         // press Enter only (text already in prompt)
    ClearAndType,   // clear existing content, then type
}

/// <summary>
/// Delivery strategy decided by PromptDeliveryContext.Decide().
/// </summary>
public enum PromptDeliveryDecision
{
    Focusless,   // target not focused -> use focusless input, don't steal focus
    FocusSteal,  // target focused + user idle -> steal focus OK
    Skip,        // user actively typing -> abort, re-arm for next idle cycle
    Abort,       // cannot deliver (no window, unsupported action, etc.)
}

/// <summary>
/// User-interference prevention context for prompt delivery.
///
/// DESIGN INTENT: Before injecting text into a Claude/Codex prompt window,
/// we must check whether the USER is currently interacting with that window.
/// Barging in while the user is typing disrupts their work -- this context
/// captures just enough situational data to make that judgment and choose
/// the least-intrusive delivery strategy.
///
/// NOT a general-purpose "situation room" -- scope is deliberately narrow:
///   1. Is the user currently focused on the target window?
///   2. Was there recent keyboard/mouse input?
///   -> If both yes: Skip (re-arm for next idle cycle, don't disturb)
///   -> If not focused: Focusless (inject silently, no focus steal)
///   -> If focused + idle: FocusSteal (user is idle there, safe to activate)
///
/// Usage:
///   var ctx = PromptDeliveryContext.Snapshot(pi.WindowHandle, PromptAction.TypeAndSubmit);
///   var decision = ctx.Decide();   // Focusless / FocusSteal / Skip / Abort
///   ph.TypeAndSubmit(pi, text, ctx);
/// </summary>
public record PromptDeliveryContext
{
    // -- Situation ------------------------------------------------
    /// <summary>Is the target window currently the foreground window?</summary>
    public bool IsTargetFocused { get; init; }

    /// <summary>Seconds elapsed since the last user keyboard/mouse input (system-wide).</summary>
    public double IdleSeconds { get; init; }

    /// <summary>True when user is actively typing in the target window (focused + input &lt;30s ago).</summary>
    public bool UserIsTyping => IsTargetFocused && IdleSeconds < 30;

    // -- Desired action ------------------------------------------─
    public PromptAction Action { get; init; } = PromptAction.TypeAndSubmit;

    // -- Decision ------------------------------------------------─
    /// <summary>Compute the optimal delivery strategy based on current situation.</summary>
    public PromptDeliveryDecision Decide() => UserIsTyping
        ? PromptDeliveryDecision.Skip
        : !IsTargetFocused
            ? PromptDeliveryDecision.Focusless
            : PromptDeliveryDecision.FocusSteal;

    // -- Factory --------------------------------------------------
    /// <summary>
    /// Snapshot current window state for the given target HWND.
    /// </summary>
    public static PromptDeliveryContext Snapshot(IntPtr targetHwnd,
        PromptAction action = PromptAction.TypeAndSubmit)
    {
        var fg = NativeMethods.GetForegroundWindow();
        bool focused = fg == targetHwnd
                    || NativeMethods.GetAncestor(fg, 2 /*GA_ROOT*/) == targetHwnd;
        double idleSec = NativeMethods.GetUserIdleMs() / 1000.0;
        return new() { IsTargetFocused = focused, IdleSeconds = idleSec, Action = action };
    }

    public override string ToString() =>
        $"[focused={IsTargetFocused} idle={IdleSeconds:F0}s action={Action}] -> {Decide()}";
}
