namespace WKAppBot.Core.Scenario;

/// <summary>
/// Root scenario document parsed from YAML.
/// </summary>
public sealed class ScenarioDocument
{
    public ScenarioMeta Scenario { get; set; } = new();
    public ScenarioConfig Config { get; set; } = new();
    public AppConfig App { get; set; } = new();
    public Dictionary<string, string> Variables { get; set; } = new();
    public List<StepDefinition> Steps { get; set; } = new();
    public List<StepDefinition>? Teardown { get; set; }
}

public sealed class ScenarioMeta
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Version { get; set; }
}

public sealed class ScenarioConfig
{
    public double Timeout { get; set; } = 10.0;
    public bool ScreenshotOnStep { get; set; } = true;
    public bool ScreenshotOnFailure { get; set; } = true;
    public bool ContinueOnError { get; set; } = false;
    public int RetryCount { get; set; } = 2;
    public double RetryDelay { get; set; } = 1.0;

    // -- Smart Focus ("위치확보") ----------------------------─
    /// <summary>
    /// Enable focus check before SendInput actions. Default: true.
    /// When true, EnsureFocus is called before mouse/keyboard SendInput.
    /// </summary>
    public bool FocusCheck { get; set; } = true;

    /// <summary>
    /// Total timeout (seconds) for focus recovery before failing a step.
    /// Phase 1 (smart recovery): first half. Phase 2 (user alert): second half.
    /// </summary>
    public double FocusTimeout { get; set; } = 5.0;

    /// <summary>
    /// Delay between focus recovery retries (seconds).
    /// </summary>
    public double FocusRetryDelay { get; set; } = 0.3;

    /// <summary>
    /// Alert delay before forceful focus grab (seconds).
    /// When focus is lost, beep + wait this long before forcing focus.
    /// Gives user time to finish their input. 0 = force immediately.
    /// </summary>
    public double FocusAlertDelay { get; set; } = 3.0;

    /// <summary>
    /// Prefer focusless input methods (UIA Invoke/Value) over SendInput.
    /// Default: true -- minimizes disruption to user's work.
    /// </summary>
    public bool PreferFocusless { get; set; } = true;

    // -- Vision Cache ("경험치 축적") ------------------------
    /// <summary>
    /// Enable Vision API fallback when UIA can't find elements.
    /// Default: false (opt-in -- API costs money).
    /// </summary>
    public bool VisionEnabled { get; set; } = false;

    /// <summary>
    /// Directory for Vision cache database (relative to working dir).
    /// </summary>
    public string VisionCacheDir { get; set; } = "data/vision_cache/entries";

    /// <summary>
    /// Cache TTL in days. Entries older than this are considered stale.
    /// </summary>
    public int VisionCacheTtlDays { get; set; } = 7;

    /// <summary>
    /// Minimum confidence threshold for Vision API results.
    /// Results below this are discarded.
    /// </summary>
    public double VisionConfidenceThreshold { get; set; } = 0.7;

    /// <summary>
    /// Vision model to use for Claude API fallback.
    /// </summary>
    public string VisionModel { get; set; } = "claude-sonnet-4-20250514";

    /// <summary>
    /// OCR preview mode: run OCR on every step even when UIA succeeds.
    /// Logs OCR results for performance testing / data collection.
    /// Does NOT affect actual element location -- just logs what OCR would find.
    /// Default: false. Set true for OCR training/benchmarking.
    /// </summary>
    public bool OcrPreview { get; set; } = false;
}

public sealed class AppConfig
{
    public string Launch { get; set; } = "";
    public WaitForWindowConfig? WaitForWindow { get; set; }
}

public sealed class WaitForWindowConfig
{
    public string? TitleContains { get; set; }
    public string? ClassName { get; set; }
    public double Timeout { get; set; } = 10.0;
}

public sealed class StepDefinition
{
    public string Name { get; set; } = "";
    public string Action { get; set; } = "";
    public TargetDefinition? Target { get; set; }
    public StepParams? Params { get; set; }

    /// <summary>
    /// Post-action state gate: poll until expected condition is met.
    /// If timeout expires, run recovery (if defined) then retry or fail.
    /// </summary>
    public ExpectDefinition? Expect { get; set; }

    /// <summary>
    /// Recovery action executed when expect fails. Can retry original action.
    /// </summary>
    public RecoveryDefinition? Recovery { get; set; }
}

/// <summary>
/// Expected UI state after action -- polled with focusless UIA queries only.
/// No SendInput, no focus steal during polling.
/// </summary>
public sealed class ExpectDefinition
{
    /// <summary>
    /// UIA-aligned expect condition. Naming extends UIA property/pattern names:
    ///   element_visible / element_enabled / element_absent / element_focused
    ///       -- AutomationElement properties (IsOffscreen, IsEnabled, HasKeyboardFocus)
    ///   value_contains / value_equals
    ///       -- ValuePattern.Value string match (UIA ValuePattern)
    ///   window_present / window_absent
    ///       -- window enumeration via WindowFinder
    ///   toggle_on / toggle_off -- TogglePattern.ToggleState
    ///   selected               -- SelectionItemPattern.IsSelected
    ///   expanded / collapsed   -- ExpandCollapsePattern.ExpandCollapseState
    /// </summary>
    public string Condition { get; set; } = "element_visible";

    /// <summary>Target to check. Defaults to the step's own target if null.</summary>
    public TargetDefinition? Target { get; set; }

    /// <summary>Expected value for text-based conditions.</summary>
    public string? Value { get; set; }

    /// <summary>Polling timeout in seconds. Default 10.</summary>
    public double Timeout { get; set; } = 10.0;

    /// <summary>Polling interval in seconds. Default 0.3.</summary>
    public double Interval { get; set; } = 0.3;
}

/// <summary>
/// Recovery handler: mini-step executed when expect fails.
/// </summary>
public sealed class RecoveryDefinition
{
    /// <summary>Action to take (press_key, click, etc.)</summary>
    public string Action { get; set; } = "";

    /// <summary>Parameters for recovery action.</summary>
    public StepParams? Params { get; set; }

    /// <summary>Target for recovery action (optional).</summary>
    public TargetDefinition? Target { get; set; }

    /// <summary>Retry the original step action after recovery. Default true.</summary>
    public bool Retry { get; set; } = true;

    /// <summary>Max recovery attempts before hard fail. Default 2.</summary>
    public int MaxRetries { get; set; } = 2;
}

public sealed class TargetDefinition
{
    public string Strategy { get; set; } = "auto";
    public string? AutomationId { get; set; }
    public string? Name { get; set; }
    public string? ControlType { get; set; }
    public string? Description { get; set; }
    public int? X { get; set; }
    public int? Y { get; set; }
}

/// <summary>
/// Flexible params bag - holds all possible action parameters.
/// Each action uses only the fields it needs.
/// </summary>
public sealed class StepParams
{
    // type_text
    public string? Text { get; set; }

    // press_key / hotkey
    public string? Key { get; set; }
    public List<string>? Keys { get; set; }

    // wait
    public double? Seconds { get; set; }

    // assert
    public string? Type { get; set; }
    public string? Expected { get; set; }

    // screenshot
    public string? Filename { get; set; }

    // scroll
    public string? Direction { get; set; }
    public int? Amount { get; set; }

    // toggle -- desired checkbox state (true=on, false=off, null=just toggle)
    public bool? Checked { get; set; }

    // expand/collapse, window_close/minimize/maximize -- target state
    public string? State { get; set; }

    // set_range -- target value for slider/progress bar
    public double? Value { get; set; }

    // select -- item text or index to select in list/combo
    public string? ItemText { get; set; }
    public int? ItemIndex { get; set; }
}
