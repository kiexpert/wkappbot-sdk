using System.Text.Json;
using System.Text.Json.Serialization;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

/// <summary>
/// Named GRAP alias store -- persisted to wkappbot.hq/profiles/grap_aliases.json.
/// Supports @name expansion and self-healing for stale hwnd fields.
/// </summary>
internal static class GrapAliasStore
{
    internal record GrapAlias(
        [property: JsonPropertyName("name")]  string Name,
        [property: JsonPropertyName("grap")]  string Grap,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("saved")] string Saved   // ISO-8601
    );

    static string StorePath => Path.Combine(Program.DataDir, "profiles", "grap_aliases.json");

    // -- CRUD ------------------------------------------------------------------

    internal static List<GrapAlias> LoadAll()
    {
        try
        {
            if (!File.Exists(StorePath)) return [];
            var json = File.ReadAllText(StorePath);
            return JsonSerializer.Deserialize<List<GrapAlias>>(json) ?? [];
        }
        catch { return []; }
    }

    static void SaveAll(List<GrapAlias> list)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(StorePath)!);
        var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(StorePath, json);
    }

    internal static void Set(string name, string grap, string title = "")
    {
        var list = LoadAll();
        var idx  = list.FindIndex(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        var entry = new GrapAlias(name, grap, title, DateTime.UtcNow.ToString("o"));
        if (idx >= 0) list[idx] = entry;
        else          list.Add(entry);
        SaveAll(list);
    }

    internal static GrapAlias? Get(string name)
        => LoadAll().FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    internal static bool Remove(string name)
    {
        var list = LoadAll();
        var before = list.Count;
        list.RemoveAll(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (list.Count == before) return false;
        SaveAll(list);
        return true;
    }

    // -- @name expansion ------------------------------------------------------─

    /// <summary>
    /// Expand @name references in a grap string.
    /// If stale hwnd detected, strips it and logs a warning.
    /// </summary>
    internal static string Expand(string grap, out string? healNote)
    {
        healNote = null;
        if (!grap.StartsWith('@')) return grap;

        var name = grap[1..].Split(' ', '#')[0]; // @name or @name#scope or @name rest
        var suffix = grap.Length > name.Length + 1 ? grap[(name.Length + 1)..] : "";

        var alias = Get(name);
        if (alias == null)
        {
            healNote = $"[GRAP-ALIAS] @{name} not found -- check `wkappbot grap list`";
            return grap; // return original so error surfaces
        }

        var resolved = alias.Grap;

        // Self-heal: if hwnd present and window no longer exists, strip it
        var hwndMatch = System.Text.RegularExpressions.Regex.Match(resolved, @"hwnd:0x([0-9A-Fa-f]+)");
        if (hwndMatch.Success)
        {
            var hwndVal = Convert.ToInt64(hwndMatch.Groups[1].Value, 16);
            var handle  = new IntPtr(hwndVal);
            if (!WKAppBot.Win32.Native.NativeMethods.IsWindow(handle))
            {
                var stripped = System.Text.RegularExpressions.Regex.Replace(resolved, @",?\s*hwnd:0x[0-9A-Fa-f]+", "").TrimEnd('{', '}');
                // Rebuild minimal JSON5
                stripped = stripped.TrimEnd(',').TrimEnd();
                // Fix dangling commas / empty braces
                stripped = System.Text.RegularExpressions.Regex.Replace(stripped, @",\s*\}", "}");
                resolved = stripped;
                healNote = $"[GRAP-ALIAS] @{name} hwnd stale -- stripped, using: {resolved}";
            }
        }

        return string.IsNullOrEmpty(suffix) ? resolved : $"{resolved}{suffix}";
    }
}
