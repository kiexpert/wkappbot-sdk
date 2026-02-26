namespace WKAppBot.Core.Scenario;

/// <summary>
/// YAML model for dialog handler files (handlers/*.yaml).
/// Filename = primary keyword trigger, match section = precise filter.
/// </summary>
public sealed class DialogHandlerConfig
{
    /// <summary>Precise matching conditions (beyond filename keyword).</summary>
    public DialogMatch? Match { get; set; }

    /// <summary>Action to take: click_button, dismiss, report.</summary>
    public string Action { get; set; } = "report";

    /// <summary>Action parameters.</summary>
    public DialogActionParams? Params { get; set; }
}

public sealed class DialogMatch
{
    /// <summary>
    /// Window title matching. Literal = contains check. Glob/regex = full match.
    /// Examples: "로그인" (contains), "투혼*로그인" (glob), "regex:.*투혼.*" (regex).
    /// </summary>
    public string? TitleContains { get; set; }

    /// <summary>
    /// Window class matching. Supports 3 modes:
    /// <list type="bullet">
    ///   <item>Literal: exact match vs leaf className. e.g. "#32770"</item>
    ///   <item>Standard glob (* ?): match vs leaf className. e.g. "#327??"</item>
    ///   <item>Path glob (contains / or **): GitHub-style match vs full classPath hierarchy.
    ///     * = single segment (slash-sensitive), ** = any depth (slash-insensitive).
    ///     e.g. "*/#32770", "**/#32770", "E*Trade*/#32770", "*_59648/**/Button_*"</item>
    /// </list>
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// Process name matching. Literal = exact. Glob = full match.
    /// Examples: "nkrunlite" (exact), "nkrun*" (glob), "XingQ*" (glob).
    /// </summary>
    public string? Process { get; set; }

    /// <summary>
    /// Dialog message text matching. Literal = contains. Glob = full match.
    /// Matched against Static/Label control text (OCR fallback for owner-drawn).
    /// </summary>
    public string? MessageContains { get; set; }
}

public sealed class DialogActionParams
{
    /// <summary>Button index to click (0 = first/leftmost).</summary>
    public int ButtonIndex { get; set; }

    /// <summary>Button text to match (alternative to index).</summary>
    public string? ButtonText { get; set; }

    /// <summary>Milliseconds to wait after handling (replaced by patrol loop if > 0).</summary>
    public int WaitAfter { get; set; }

    /// <summary>Whether to retry the original action after handling.</summary>
    public bool Retry { get; set; }

    /// <summary>
    /// Wait-until condition: patrol loop that handles blockers while waiting.
    /// If set, replaces passive Thread.Sleep(WaitAfter) with active blocker patrol.
    /// If WaitUntil is null but WaitAfter > 0, patrol runs for WaitAfter duration.
    /// </summary>
    public WaitUntilCondition? WaitUntil { get; set; }
}

/// <summary>
/// Condition for active-patrol wait loop after handler action.
/// Loop polls every ~500ms: checks condition + runs TryHandleBlocker.
/// "핸들러 개입환영" — welcomes handler intervention during wait.
/// </summary>
public sealed class WaitUntilCondition
{
    /// <summary>Wait until a window with this title pattern exists (glob: *, ?).</summary>
    public string? WindowExists { get; set; }

    /// <summary>Wait until the handled dialog is gone (default: true if no other condition).</summary>
    public bool? DialogGone { get; set; }

    /// <summary>
    /// Wait until the target window's rendered content stabilizes.
    /// N consecutive identical PrintWindow frames = "loading complete".
    /// Blocker handling resets the frame counter (screen changes after popup dismissed).
    /// Default: 0 (disabled). Recommended: 3.
    /// </summary>
    public int StableFrames { get; set; }

    /// <summary>Interval between stability check frames in ms (default: 500).</summary>
    public int StableInterval { get; set; } = 500;

    /// <summary>
    /// Wait until the target window's message loop is responsive.
    /// Uses SendMessageTimeout(WM_NULL, SMTO_ABORTIFHUNG) — returns true when the app
    /// finishes heavy initialization and can process messages again.
    /// Combined with window_exists: first wait for window, then wait for it to respond.
    /// Default: false.
    /// </summary>
    public bool Responsive { get; set; }

    /// <summary>Maximum wait time in seconds (default: 15).</summary>
    public int Timeout { get; set; } = 15;
}
