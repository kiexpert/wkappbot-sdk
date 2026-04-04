using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.CLI;

/// <summary>
/// File-backed registry mapping sandbox keys to Chrome target IDs + expected hosts.
///
/// Key format: "{command}+{subcommand}+{promptHwnd:X8}"
///   e.g. "ask+claude+001A2B3C"  "ask+gemini+001A2B3C"  "web+google.com+001A2B3C"
///
/// Contract:
///   - key ↔ expectedHost: 1:1 fixed. Same HWND+command → always the same tab.
///   - URL mismatch → immediate invalidation + new tab (fast-fail).
///   - Eye restart: PurgeDeadHwnds() removes entries for dead HWND handles.
/// </summary>
internal static class AskTargetRegistry
{
    private static readonly string _filePath = Path.Combine(
        AppContext.BaseDirectory, "wkappbot.hq", "runtime", "ask_targets.json");

    internal record RegistryEntry(
        [property: JsonPropertyName("targetId")]  string TargetId,
        [property: JsonPropertyName("expectedHost")] string ExpectedHost);

    private static Dictionary<string, RegistryEntry> Load()
    {
        try
        {
            if (!File.Exists(_filePath)) return new(StringComparer.OrdinalIgnoreCase);
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Dictionary<string, RegistryEntry>>(json,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                   ?? new(StringComparer.OrdinalIgnoreCase);
        }
        catch { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static void Save(Dictionary<string, RegistryEntry> dict)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(dict));
        }
        catch { /* best effort */ }
    }

    /// <summary>Get the registry entry for a sandbox key. Returns null if not found.</summary>
    internal static RegistryEntry? GetEntry(string key)
    {
        var dict = Load();
        return dict.TryGetValue(key, out var entry) ? entry : null;
    }

    /// <summary>Register or update a sandbox key → (targetId, expectedHost).</summary>
    internal static void SetEntry(string key, string targetId, string expectedHost)
    {
        var dict = Load();
        dict[key] = new RegistryEntry(targetId, expectedHost);
        Save(dict);
    }

    /// <summary>Immediately invalidate a key (fast-fail on URL mismatch).</summary>
    internal static void RemoveEntry(string key)
    {
        var dict = Load();
        if (dict.Remove(key))
            Save(dict);
    }

    /// <summary>
    /// Remove all entries whose HWND segment is dead (IsWindow == false).
    /// Called on Eye restart to clean up orphan entries from the previous session.
    /// Key format: "cmd+sub+HWND8" — last '+'-segment is 8-char hex HWND.
    /// </summary>
    internal static void PurgeDeadHwnds()
    {
        var dict = Load();
        var removed = new List<string>();
        foreach (var key in dict.Keys)
        {
            var parts = key.Split('+');
            if (parts.Length < 3) continue;
            var hwndStr = parts[^1];
            // Only purge HWND-based keys (8 hex chars, non-zero)
            if (hwndStr.Length != 8) continue;
            if (!long.TryParse(hwndStr, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal)) continue;
            if (hwndVal == 0) continue; // cwdHash fallback key — skip
            var hwnd = new IntPtr(hwndVal);
            if (!WKAppBot.Win32.Native.NativeMethods.IsWindow(hwnd))
                removed.Add(key);
        }
        if (removed.Count == 0) return;
        foreach (var k in removed) dict.Remove(k);
        Save(dict);
        Console.WriteLine($"[SANDBOX] Purged {removed.Count} dead-HWND registry entry/entries");
    }

    /// <summary>Wipe all registry entries (used on Chrome restart).</summary>
    internal static void ClearAll()
    {
        try { File.Delete(_filePath); } catch { }
        Console.WriteLine("[SANDBOX] Registry cleared (Chrome restart)");
    }

    // ── Per-tab question ID counter ──────────────────────────────────────────
    // Each ask on the same tab gets a unique monotonic Q# (Q1, Q2, Q3...).
    // Persisted to ask_qid.json so counter survives process restarts.
    // Triad uses its own Game ID — skip Qid for triad mode.

    private static readonly string _qidFilePath = Path.Combine(
        AppContext.BaseDirectory, "wkappbot.hq", "runtime", "ask_qid.json");

    private static readonly object _qidLock = new();

    /// <summary>
    /// Atomically assign and return the next question ID for a sandbox key.
    /// Returns 0 if key is empty (triad/system calls that bypass Q#).
    /// </summary>
    internal static int AssignNextQid(string sandboxKey)
    {
        if (string.IsNullOrEmpty(sandboxKey)) return 0;
        lock (_qidLock)
        {
            try
            {
                var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                if (File.Exists(_qidFilePath))
                    dict = JsonSerializer.Deserialize<Dictionary<string, int>>(File.ReadAllText(_qidFilePath))
                           ?? dict;
                dict.TryGetValue(sandboxKey, out var current);
                var next = current + 1;
                dict[sandboxKey] = next;
                Directory.CreateDirectory(Path.GetDirectoryName(_qidFilePath)!);
                File.WriteAllText(_qidFilePath, JsonSerializer.Serialize(dict));
                return next;
            }
            catch { return 0; }
        }
    }

    // ── Per-page Slack thread persistence ────────────────────────────────────
    // Maps sandboxKey → Slack thread ts. One thread per (command+ai+hwnd) tuple.
    // File: wkappbot.hq/runtime/ask_slack_threads.json

    private static readonly string _slackThreadsFilePath = Path.Combine(
        AppContext.BaseDirectory, "wkappbot.hq", "runtime", "ask_slack_threads.json");

    internal static string? GetSlackThread(string sandboxKey)
    {
        try
        {
            if (!File.Exists(_slackThreadsFilePath)) return null;
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(
                File.ReadAllText(_slackThreadsFilePath));
            return dict != null && dict.TryGetValue(sandboxKey, out var ts) ? ts : null;
        }
        catch { return null; }
    }

    internal static void SetSlackThread(string sandboxKey, string threadTs)
    {
        try
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(_slackThreadsFilePath))
            {
                dict = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    File.ReadAllText(_slackThreadsFilePath))
                    ?? new(StringComparer.OrdinalIgnoreCase);
            }
            dict[sandboxKey] = threadTs;
            Directory.CreateDirectory(Path.GetDirectoryName(_slackThreadsFilePath)!);
            File.WriteAllText(_slackThreadsFilePath, JsonSerializer.Serialize(dict));
        }
        catch { /* best effort */ }
    }

    // ── Legacy shims: backward-compat for any remaining old callers ────────

    internal static string? GetTargetId(string tag)
        => GetEntry(tag)?.TargetId;

    internal static void SetTargetId(string tag, string targetId)
    {
        // Legacy: expectedHost unknown — preserve existing if any
        var dict = Load();
        var existing = dict.TryGetValue(tag, out var e) ? e : null;
        dict[tag] = new RegistryEntry(targetId, existing?.ExpectedHost ?? "");
        Save(dict);
    }

    internal static void RemoveTargetId(string tag) => RemoveEntry(tag);
}
