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
    /// <summary>Window title must contain this string.</summary>
    public string? TitleContains { get; set; }

    /// <summary>Window class name (e.g. "#32770").</summary>
    public string? Class { get; set; }

    /// <summary>Process name (e.g. "nkrunlite").</summary>
    public string? Process { get; set; }

    /// <summary>Dialog message text (Static/Label) must contain this string.</summary>
    public string? MessageContains { get; set; }
}

public sealed class DialogActionParams
{
    /// <summary>Button index to click (0 = first/leftmost).</summary>
    public int ButtonIndex { get; set; }

    /// <summary>Button text to match (alternative to index).</summary>
    public string? ButtonText { get; set; }

    /// <summary>Milliseconds to wait after handling.</summary>
    public int WaitAfter { get; set; }

    /// <summary>Whether to retry the original action after handling.</summary>
    public bool Retry { get; set; }
}
