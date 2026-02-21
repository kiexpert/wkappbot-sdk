using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Core.Runner;

/// <summary>
/// IPC state shared via JSON file between CLI commands and AppBotEye.
/// Every CLI command writes its "last touched" info here.
/// AppBotEye reads it to display appbot's current focus.
/// </summary>
public sealed class ActionState
{
    // ── Source identifier ──────────────────────────────────────
    /// <summary>
    /// Which command wrote this: "scenario"|"do"|"web"|"scan"|"input"|"dismiss"|"slack"
    /// </summary>
    public string Source { get; set; } = "";

    // ── Window info ────────────────────────────────────────────
    public string? WindowTitle { get; set; }
    public string? WindowClass { get; set; }
    public string? ProcessName { get; set; }

    // ── UIA element info (핵심! — 액빌 내용) ──────────────────
    /// <summary>Control type tag: [Button], [Edit], [Pane], ...</summary>
    public string? ControlType { get; set; }

    /// <summary>UIA Name or descriptive name: "plusButton", "확인", ...</summary>
    public string? ElementName { get; set; }

    /// <summary>UIA AutomationId: aid="btnOK"</summary>
    public string? AutomationId { get; set; }

    /// <summary>Supported UIA patterns: Invoke, Value, Toggle, ...</summary>
    public List<string> Patterns { get; set; } = new();

    /// <summary>Value pattern text (if available)</summary>
    public string? ElementValue { get; set; }

    // ── Action info (핵심! — 액션 이름) ────────────────────────
    /// <summary>Action name: "click", "type_text", "assert", ...</summary>
    public string? ActionName { get; set; }

    /// <summary>Detailed action description: "Click plusButton (Invoke, automation_id, focusless)"</summary>
    public string? ActionDetail { get; set; }

    /// <summary>Step name from scenario: "[Addition] Click Plus"</summary>
    public string? StepName { get; set; }

    /// <summary>Step result: "pass"|"fail"|"executing"</summary>
    public string? Status { get; set; }

    /// <summary>How the element was found: "automation_id"|"name"|"ocr"|"vision"|"coordinate"</summary>
    public string? LocatorMethod { get; set; }

    // ── Scenario progress (present only during scenario run) ──
    public string? ScenarioName { get; set; }

    /// <summary>Progress: "5/32"</summary>
    public string? Progress { get; set; }

    // ── Web info (present only for web commands) ──────────────
    public string? WebUrl { get; set; }
    public string? WebTitle { get; set; }

    // ── Claude Desktop status (Phase 3) ──────────────────────
    public string? ClaudeStatus { get; set; }
    public string? ClaudeStatusText { get; set; }

    // ── Rate limit info (populated by AppBotEye when rate limit detected) ──
    /// <summary>ISO 8601 datetime when rate limit resets. null = not rate limited.</summary>
    public string? RateLimitResetAt { get; set; }

    /// <summary>true when rate_limit state is currently active.</summary>
    public bool? IsRateLimited { get; set; }

    // ── Timestamp ────────────────────────────────────────────
    /// <summary>ISO 8601 timestamp of last update</summary>
    public string UpdatedAt { get; set; } = "";

    // ── Static helpers ───────────────────────────────────────

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Path to the shared action state file.
    /// Located at: {exe_dir}/wkappbot.hq/runtime/action_state.json
    /// </summary>
    public static string FilePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "action_state.json");
        }
    }

    /// <summary>
    /// Atomically write ActionState to the shared JSON file.
    /// Uses .tmp file + File.Move for crash-safe writes.
    /// </summary>
    public static void Write(ActionState state)
    {
        try
        {
            state.UpdatedAt = DateTime.Now.ToString("O"); // ISO 8601

            var filePath = FilePath;
            var dir = Path.GetDirectoryName(filePath)!;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(state, _jsonOptions);
            var tmpPath = filePath + ".tmp";

            File.WriteAllText(tmpPath, json);
            File.Move(tmpPath, filePath, overwrite: true);
        }
        catch
        {
            // Best-effort: never block the main flow
        }
    }

    /// <summary>
    /// Read ActionState from the shared JSON file.
    /// Returns null if file doesn't exist or is unreadable.
    /// </summary>
    public static ActionState? Read()
    {
        try
        {
            var filePath = FilePath;
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ActionState>(json, _jsonOptions);
        }
        catch
        {
            // Best-effort: file might be mid-write
            return null;
        }
    }

    /// <summary>
    /// Check if the state is stale (older than given seconds).
    /// </summary>
    public bool IsStale(double maxAgeSeconds = 30.0)
    {
        if (string.IsNullOrEmpty(UpdatedAt))
            return true;

        if (DateTime.TryParse(UpdatedAt, out var updatedAt))
        {
            return (DateTime.Now - updatedAt).TotalSeconds > maxAgeSeconds;
        }
        return true;
    }

    /// <summary>
    /// Format for AppBotEye display (UIA content + action names).
    /// </summary>
    public string ToDisplayText()
    {
        var lines = new List<string>();

        // Line 1: [ControlType] "ElementName"
        if (!string.IsNullOrEmpty(ControlType) || !string.IsNullOrEmpty(ElementName))
        {
            var ct = ControlType ?? "";
            var name = ElementName != null ? $" \"{ElementName}\"" : "";
            lines.Add($"{ct}{name}");
        }

        // Line 2: aid="AutomationId"
        if (!string.IsNullOrEmpty(AutomationId))
            lines.Add($"aid=\"{AutomationId}\"");

        // Line 3: (Patterns)
        if (Patterns.Count > 0)
            lines.Add($"({string.Join(", ", Patterns)})");

        // Line 4: Value
        if (!string.IsNullOrEmpty(ElementValue))
            lines.Add($"Value: \"{ElementValue}\"");

        // Separator
        if (lines.Count > 0 && !string.IsNullOrEmpty(ActionName))
            lines.Add("");

        // Line 5: action → STATUS
        if (!string.IsNullOrEmpty(ActionName))
        {
            var statusTag = !string.IsNullOrEmpty(Status) ? $" → {Status.ToUpperInvariant()}" : "";
            lines.Add($"{ActionName}{statusTag}");
        }

        // Line 6: StepName
        if (!string.IsNullOrEmpty(StepName))
            lines.Add($"\"{StepName}\"");

        // Line 7: ScenarioName / Progress
        var scenarioLine = new List<string>();
        if (!string.IsNullOrEmpty(WindowTitle))
            scenarioLine.Add(WindowTitle);
        if (!string.IsNullOrEmpty(ScenarioName))
            scenarioLine.Add(ScenarioName);
        if (!string.IsNullOrEmpty(Progress))
            scenarioLine.Add(Progress);
        if (scenarioLine.Count > 0)
            lines.Add(string.Join(" / ", scenarioLine));

        // Web info
        if (!string.IsNullOrEmpty(WebUrl))
            lines.Add(WebUrl);

        // Claude status
        if (!string.IsNullOrEmpty(ClaudeStatus))
            lines.Add($"Claude: {ClaudeStatusText ?? ClaudeStatus}");

        return string.Join("\n", lines);
    }
}
