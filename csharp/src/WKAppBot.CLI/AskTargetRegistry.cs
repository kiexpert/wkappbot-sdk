using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// File-backed registry mapping target tags (e.g. "gemini-a1b2c3d4") to Chrome target IDs.
/// Survives across CLI invocations within the same session.
/// </summary>
internal static class AskTargetRegistry
{
    private static readonly string _filePath = Path.Combine(
        AppContext.BaseDirectory, "wkappbot.hq", "runtime", "ask_targets.json");

    private static Dictionary<string, string> Load()
    {
        try
        {
            if (!File.Exists(_filePath)) return new(StringComparer.OrdinalIgnoreCase);
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? new(StringComparer.OrdinalIgnoreCase);
        }
        catch { return new(StringComparer.OrdinalIgnoreCase); }
    }

    private static void Save(Dictionary<string, string> dict)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(dict));
        }
        catch { /* best effort */ }
    }

    internal static string? GetTargetId(string tag)
    {
        var dict = Load();
        return dict.TryGetValue(tag, out var id) ? id : null;
    }

    internal static void SetTargetId(string tag, string targetId)
    {
        var dict = Load();
        dict[tag] = targetId;
        Save(dict);
    }

    internal static void RemoveTargetId(string tag)
    {
        var dict = Load();
        if (dict.Remove(tag))
            Save(dict);
    }
}
