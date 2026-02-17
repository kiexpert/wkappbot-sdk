namespace WKAppBot.Core.Runner;

/// <summary>
/// Runtime state shared across steps during scenario execution.
/// Holds the target window, variables, and step results.
/// </summary>
public sealed class RuntimeContext
{
    public IntPtr MainWindowHandle { get; set; }
    public string AppTitle { get; set; } = "";
    public Dictionary<string, string> Variables { get; } = new();
    public List<StepResult> StepResults { get; } = new();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // ── Smart Focus config ("위치확보") ──────────────────────
    public bool FocusCheck { get; set; } = true;
    public double FocusTimeout { get; set; } = 5.0;
    public double FocusRetryDelay { get; set; } = 0.3;
    public double FocusAlertDelay { get; set; } = 3.0;
    public bool PreferFocusless { get; set; } = true;

    // ── Vision config ("경험치 축적") ────────────────────────
    public bool VisionEnabled { get; set; } = false;
    public string VisionCacheDir { get; set; } = "data/vision_cache/entries";
    public int VisionCacheTtlDays { get; set; } = 7;
    public double VisionConfidenceThreshold { get; set; } = 0.7;
    public string VisionModel { get; set; } = "claude-sonnet-4-20250514";
    public bool OcrPreview { get; set; } = false;

    /// <summary>
    /// Shared console lock for thread-safe output.
    /// Set by ScenarioRunner when background watcher is active.
    /// </summary>
    public object? ConsoleLock { get; set; }

    // ── Background watcher coordination ─────────────────────
    // ActionExecutor writes the latest action point here.
    // BackgroundWatcher reads it to track test position instead of mouse cursor.
    // Thread-safe via volatile + Interlocked.

    private volatile ActionPoint? _lastActionPoint;

    /// <summary>
    /// Latest action point from the running test.
    /// BackgroundWatcher uses this instead of mouse cursor when available.
    /// </summary>
    public ActionPoint? LastActionPoint => _lastActionPoint;

    /// <summary>
    /// Record the screen coordinates where an action was performed.
    /// </summary>
    public void SetActionPoint(int screenX, int screenY, string stepName, string action, string? elementDesc = null)
    {
        _lastActionPoint = new ActionPoint
        {
            X = screenX,
            Y = screenY,
            StepName = stepName,
            Action = action,
            ElementDescription = elementDesc,
            Timestamp = DateTime.Now
        };
    }

    /// <summary>
    /// Record that the test is performing a non-positional action (hotkey, wait, etc.).
    /// Watcher will fall back to mouse cursor tracking.
    /// </summary>
    public void ClearActionPoint()
    {
        _lastActionPoint = null;
    }

    /// <summary>
    /// Resolve ${variable} references in a string.
    /// </summary>
    public string Resolve(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input ?? "";

        foreach (var (key, value) in Variables)
        {
            input = input.Replace($"${{{key}}}", value);
        }
        return input;
    }
}

/// <summary>
/// Result of a single step execution.
/// </summary>
public sealed class StepResult
{
    public string StepName { get; set; } = "";
    public string Action { get; set; } = "";
    public StepStatus Status { get; set; }
    public string? Message { get; set; }
    public double ElapsedMs { get; set; }
    public string? ScreenshotPath { get; set; }
    public string? LocatorMethod { get; set; }
    public int RetryCount { get; set; }
    /// <summary>
    /// Human-readable description of what the action actually did.
    /// e.g. "Click plusButton (Invoke)", "Type \"15\"", "Press escape"
    /// </summary>
    public string? ActionDetail { get; set; }
}

public enum StepStatus
{
    Pass,
    Fail,
    Skip,
    Error
}

/// <summary>
/// Screen coordinates where the test last performed an action.
/// Shared between ActionExecutor (writer) and BackgroundWatcher (reader).
/// </summary>
public sealed class ActionPoint
{
    public int X { get; init; }
    public int Y { get; init; }
    public string StepName { get; init; } = "";
    public string Action { get; init; } = "";
    public string? ElementDescription { get; init; }
    public DateTime Timestamp { get; init; }
}
