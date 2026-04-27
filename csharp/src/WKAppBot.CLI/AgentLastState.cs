// AgentLastState.cs -- persist wkappbot agent invocations to .wkappbot/agents/
// File per tier: .wkappbot/agents/last-{tier}.jsonl  (append log, newest = last line)
// Used for: follow-up via `wkappbot agent resume ["follow-up"]`
//           inspect via `wkappbot agent last`

using System.Text.Json;
using System.Text.Json.Serialization;

static class AgentLastState
{
    static string AgentsDir => Path.Combine(Directory.GetCurrentDirectory(), ".wkappbot", "agents");

    static string FilePath(string tier) => Path.Combine(AgentsDir, $"last-{tier}.jsonl");

    public class Entry
    {
        [JsonPropertyName("ts")]   public string Ts   { get; set; } = "";
        [JsonPropertyName("task")] public string Task { get; set; } = "";
        [JsonPropertyName("tier")] public string Tier { get; set; } = "";
        [JsonPropertyName("cwd")]  public string Cwd  { get; set; } = "";
    }

    public static void Save(string task, string tier)
    {
        try
        {
            Directory.CreateDirectory(AgentsDir);
            var entry = new Entry
            {
                Ts   = DateTime.UtcNow.ToString("o"),
                Task = task,
                Tier = tier,
                Cwd  = Directory.GetCurrentDirectory()
            };
            File.AppendAllText(FilePath(tier),
                JsonSerializer.Serialize(entry) + "\n");
        }
        catch { }
    }

    public static Entry? Load(string? tier = null)
    {
        try
        {
            if (tier != null)
                return ReadLastLine(FilePath(tier));

            // No tier specified — find most recently modified last-*.jsonl
            if (!Directory.Exists(AgentsDir)) return null;
            var latest = Directory.GetFiles(AgentsDir, "last-*.jsonl")
                .OrderByDescending(File.GetLastWriteTimeUtc)
                .FirstOrDefault();
            return latest != null ? ReadLastLine(latest) : null;
        }
        catch { }
        return null;
    }

    static Entry? ReadLastLine(string path)
    {
        if (!File.Exists(path)) return null;
        var lines = File.ReadAllLines(path);
        for (int i = lines.Length - 1; i >= 0; i--)
        {
            var line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;
            return JsonSerializer.Deserialize<Entry>(line);
        }
        return null;
    }

    public static void Show(string? tier = null)
    {
        var e = Load(tier);
        if (e == null) { Console.WriteLine("[AGENT] No previous task recorded."); return; }
        var age = (DateTime.UtcNow - DateTime.Parse(e.Ts)).TotalMinutes;
        var ageStr = age < 60 ? $"{(int)age}min ago" : $"{(int)(age / 60)}h ago";
        Console.WriteLine($"[AGENT] last | tier={e.Tier} | {ageStr} | cwd={e.Cwd}");
        Console.WriteLine($"  task: {e.Task}");
        Console.WriteLine($"  resume: wkappbot agent resume \"<follow-up>\"");
    }
}
