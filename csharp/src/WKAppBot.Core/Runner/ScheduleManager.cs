using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Core.Runner;

/// <summary>
/// A single scheduled task — prompt injection at a specified time.
/// Persisted in schedule.json. Executed by AppBotEye's polling loop.
/// </summary>
public sealed class ScheduleItem
{
    /// <summary>Unique ID: "s20260221153000_042"</summary>
    public string Id { get; set; } = "";

    /// <summary>"once" | "on_limit_reset" | "recurring"</summary>
    public string Type { get; set; } = "once";

    /// <summary>ISO 8601 execution time for "once" type. For "recurring": next execution time.</summary>
    public string? ExecuteAt { get; set; }

    /// <summary>Recurrence interval for "recurring" type: "30m", "1h", "2h".</summary>
    public string? Interval { get; set; }

    /// <summary>Prompt text to type into Claude Desktop.</summary>
    public string? Prompt { get; set; }

    /// <summary>Path to file containing prompt text (overrides Prompt if present).</summary>
    public string? PromptFile { get; set; }

    /// <summary>Shell command to execute (alternative to Prompt). e.g. "cmd /c echo hi"</summary>
    public string? Command { get; set; }

    /// <summary>"pending" | "done" | "failed" | "expired"</summary>
    public string Status { get; set; } = "pending";

    /// <summary>ISO 8601 creation timestamp.</summary>
    public string CreatedAt { get; set; } = "";

    /// <summary>ISO 8601 execution timestamp (when actually executed).</summary>
    public string? ExecutedAt { get; set; }

    /// <summary>Working directory where this was created.</summary>
    public string? Cwd { get; set; }

    /// <summary>Who created this: instance name or "cli".</summary>
    public string? CreatedBy { get; set; }

    /// <summary>Slack notification after execution? Default true.</summary>
    public bool NotifySlack { get; set; } = true;

    /// <summary>For recurring: max executions. 0 = unlimited.</summary>
    public int MaxRuns { get; set; } = 0;

    /// <summary>For recurring: how many times executed so far.</summary>
    public int RunCount { get; set; } = 0;

    /// <summary>For recurring: ISO 8601 expiration datetime. null = no expiry.</summary>
    public string? ExpiresAt { get; set; }

    /// <summary>Error message if status == "failed".</summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>Root container for schedule.json.</summary>
public sealed class ScheduleFile
{
    public List<ScheduleItem> Schedules { get; set; } = new();
}

/// <summary>
/// CRUD operations for schedule.json.
/// Atomic file reads/writes following ActionState pattern.
/// </summary>
public static class ScheduleManager
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>Path: {exe_dir}/wkappbot.hq/runtime/schedule.json</summary>
    public static string FilePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "schedule.json");
        }
    }

    public static ScheduleFile Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return new ScheduleFile();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<ScheduleFile>(json, _jsonOptions) ?? new ScheduleFile();
        }
        catch { return new ScheduleFile(); }
    }

    public static void Save(ScheduleFile file)
    {
        try
        {
            var dir = Path.GetDirectoryName(FilePath)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var json = JsonSerializer.Serialize(file, _jsonOptions);
            var tmpPath = FilePath + ".tmp";
            File.WriteAllText(tmpPath, json);
            File.Move(tmpPath, FilePath, overwrite: true);
        }
        catch { /* best-effort */ }
    }

    public static string GenerateId()
        => $"s{DateTime.Now:yyyyMMddHHmmss}_{Random.Shared.Next(1000):D3}";

    /// <summary>Add a new schedule item. Returns the generated ID.</summary>
    public static string Add(ScheduleItem item)
    {
        var file = Load();
        if (string.IsNullOrEmpty(item.Id))
            item.Id = GenerateId();
        if (string.IsNullOrEmpty(item.CreatedAt))
            item.CreatedAt = DateTime.Now.ToString("O");
        if (string.IsNullOrEmpty(item.Cwd))
            item.Cwd = Environment.CurrentDirectory;
        file.Schedules.Add(item);
        Save(file);
        return item.Id;
    }

    /// <summary>Remove by ID. Returns true if found and removed.</summary>
    public static bool Remove(string id)
    {
        var file = Load();
        var removed = file.Schedules.RemoveAll(s =>
            s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (removed > 0) Save(file);
        return removed > 0;
    }

    /// <summary>Clear schedules. Returns count removed.</summary>
    public static int Clear(bool onlyPending = false)
    {
        var file = Load();
        int removed;
        if (onlyPending)
            removed = file.Schedules.RemoveAll(s => s.Status == "pending");
        else
        {
            removed = file.Schedules.Count;
            file.Schedules.Clear();
        }
        Save(file);
        return removed;
    }

    /// <summary>Mark item as done/failed and save.</summary>
    public static void UpdateStatus(string id, string status, string? errorMessage = null)
    {
        var file = Load();
        var item = file.Schedules.FirstOrDefault(s => s.Id == id);
        if (item != null)
        {
            item.Status = status;
            item.ExecutedAt = DateTime.Now.ToString("O");
            if (errorMessage != null) item.ErrorMessage = errorMessage;
            if (status == "done" && item.Type == "recurring") item.RunCount++;
            Save(file);
        }
    }

    /// <summary>Get all pending items whose execution time has passed.</summary>
    public static List<ScheduleItem> GetDueItems()
    {
        var file = Load();
        var now = DateTime.Now;
        var due = new List<ScheduleItem>();

        foreach (var item in file.Schedules)
        {
            if (item.Status != "pending") continue;

            switch (item.Type)
            {
                case "once":
                    if (!string.IsNullOrEmpty(item.ExecuteAt) &&
                        DateTime.TryParse(item.ExecuteAt, out var execAt) &&
                        execAt <= now)
                        due.Add(item);
                    break;

                case "recurring":
                    if (!string.IsNullOrEmpty(item.ExecuteAt) &&
                        DateTime.TryParse(item.ExecuteAt, out var nextRun) &&
                        nextRun <= now)
                    {
                        // Check expiry
                        if (!string.IsNullOrEmpty(item.ExpiresAt) &&
                            DateTime.TryParse(item.ExpiresAt, out var expires) &&
                            now > expires)
                        {
                            item.Status = "expired";
                            continue;
                        }
                        // Check max runs
                        if (item.MaxRuns > 0 && item.RunCount >= item.MaxRuns)
                        {
                            item.Status = "expired";
                            continue;
                        }
                        due.Add(item);
                    }
                    break;

                // "on_limit_reset" is handled separately via GetOnLimitResetItems()
            }
        }

        // Save any status changes (expired items)
        if (due.Count > 0 || file.Schedules.Any(s => s.Status == "expired"))
            Save(file);

        return due;
    }

    /// <summary>Get pending on_limit_reset items (triggered by rate_limit -> prompt_ready transition).</summary>
    public static List<ScheduleItem> GetOnLimitResetItems()
    {
        var file = Load();
        return file.Schedules
            .Where(s => s.Type == "on_limit_reset" && s.Status == "pending")
            .ToList();
    }

    /// <summary>After executing a recurring item, compute and set next execution time.</summary>
    public static void AdvanceRecurring(string id)
    {
        var file = Load();
        var item = file.Schedules.FirstOrDefault(s => s.Id == id);
        if (item == null || item.Type != "recurring") return;

        var interval = ParseInterval(item.Interval);
        if (interval == null)
        {
            item.Status = "failed";
            item.ErrorMessage = $"Invalid interval: {item.Interval}";
        }
        else
        {
            item.ExecuteAt = DateTime.Now.Add(interval.Value).ToString("O");
            item.Status = "pending"; // re-arm
        }
        Save(file);
    }

    /// <summary>Parse interval string: "30m", "1h", "2h", "90s".</summary>
    public static TimeSpan? ParseInterval(string? intervalStr)
    {
        if (string.IsNullOrEmpty(intervalStr)) return null;
        var s = intervalStr.Trim().ToLowerInvariant();
        if (s.EndsWith("m") && double.TryParse(s[..^1], out var m)) return TimeSpan.FromMinutes(m);
        if (s.EndsWith("h") && double.TryParse(s[..^1], out var h)) return TimeSpan.FromHours(h);
        if (s.EndsWith("s") && double.TryParse(s[..^1], out var sec)) return TimeSpan.FromSeconds(sec);
        return null;
    }
}
