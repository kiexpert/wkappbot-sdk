using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

// partial class: wkappbot skill <subcommand> — executable automation skills
// Storage: wkappbot.hq/skills/{app}/{slug}.skill.json
internal partial class Program
{
    static string SkillsDir => Path.Combine(DataDir, "skills");

    static int SkillCommand(string[] args)
    {
        if (args.Length == 0)
            return SkillUsage();

        return args[0].ToLowerInvariant() switch
        {
            "list" => SkillListCommand(args.Skip(1).ToArray()),
            "show" => SkillShowCommand(args.Skip(1).ToArray()),
            "contribute" => SkillContributeCommand(args.Skip(1).ToArray()),
            "export" => SkillExportCommand(args.Skip(1).ToArray()),
            "import" => SkillImportCommand(args.Skip(1).ToArray()),
            "--help" or "-h" or "help" => SkillUsage(),
            var s => Error($"Unknown skill sub-command: {s}")
        };
    }

    // ── List ──────────────────────────────────────────────────────────────────

    static int SkillListCommand(string[] args)
    {
        string? appFilter = args.Length > 0 && !args[0].StartsWith('-') ? args[0] : null;

        if (!Directory.Exists(SkillsDir))
        {
            Console.WriteLine("[SKILL] No skills directory. Use: wkappbot skill contribute --app X --title Y --desc Z");
            return 0;
        }

        var allSkills = new List<SkillRecord>();
        foreach (var appDir in Directory.GetDirectories(SkillsDir))
        {
            var app = Path.GetFileName(appDir);
            if (appFilter != null && !app.Contains(appFilter, StringComparison.OrdinalIgnoreCase))
                continue;
            foreach (var f in Directory.GetFiles(appDir, "*.skill.json"))
            {
                var s = SkillRecord.Load(f);
                if (s != null) allSkills.Add(s);
            }
        }

        if (allSkills.Count == 0)
        {
            Console.WriteLine(appFilter != null
                ? $"[SKILL] No skills found for app: {appFilter}"
                : "[SKILL] No skills found.");
            return 0;
        }

        Console.WriteLine($"[SKILL] {allSkills.Count} skill(s){(appFilter != null ? $" for '{appFilter}'" : "")}:");
        foreach (var s in allSkills.OrderBy(x => x.App).ThenBy(x => x.Id))
            Console.WriteLine($"  [{s.App}] {s.Id} — {s.Title}");

        return 0;
    }

    // ── Show ──────────────────────────────────────────────────────────────────

    static int SkillShowCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill show <id>");
            return 1;
        }

        var id = args[0];
        var skill = FindSkill(id);
        if (skill == null)
        {
            Console.WriteLine($"[SKILL] Skill not found: {id}");
            return 1;
        }

        Console.WriteLine($"[SKILL] {skill.Id}");
        Console.WriteLine($"  App    : {skill.App}");
        Console.WriteLine($"  Title  : {skill.Title}");
        Console.WriteLine($"  Desc   : {skill.Desc}");
        if (skill.Tags?.Count > 0)
            Console.WriteLine($"  Tags   : {string.Join(", ", skill.Tags)}");
        if (skill.Steps?.Count > 0)
        {
            Console.WriteLine("  Steps  :");
            for (int i = 0; i < skill.Steps.Count; i++)
                Console.WriteLine($"    {i + 1}. {skill.Steps[i]}");
        }
        if (!string.IsNullOrEmpty(skill.Author))
            Console.WriteLine($"  Author : {skill.Author}");
        Console.WriteLine($"  Created: {skill.Created:yyyy-MM-dd}");
        return 0;
    }

    // ── Contribute ────────────────────────────────────────────────────────────

    static int SkillContributeCommand(string[] args)
    {
        string? app = null, title = null, desc = null, author = null;
        var steps = new List<string>();
        var tags = new List<string>();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--app"   when i + 1 < args.Length: app    = args[++i]; break;
                case "--title" when i + 1 < args.Length: title  = args[++i]; break;
                case "--desc"  when i + 1 < args.Length: desc   = args[++i]; break;
                case "--author"when i + 1 < args.Length: author = args[++i]; break;
                case "--steps" when i + 1 < args.Length: steps.AddRange(args[++i].Split('|')); break;
                case "--tags"  when i + 1 < args.Length: tags.AddRange(args[++i].Split(',')); break;
            }
        }

        if (string.IsNullOrEmpty(app) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc))
        {
            Console.WriteLine("Usage: wkappbot skill contribute --app <app> --title <title> --desc <desc> [--steps \"s1|s2\"] [--tags \"t1,t2\"]");
            return 1;
        }

        var slug = Slugify(title);
        var appDir = Path.Combine(SkillsDir, app);
        Directory.CreateDirectory(appDir);
        var path = Path.Combine(appDir, $"{slug}.skill.json");

        // Merge if exists
        SkillRecord? existing = File.Exists(path) ? SkillRecord.Load(path) : null;
        var skill = new SkillRecord
        {
            Id      = existing?.Id ?? slug,
            App     = app,
            Title   = title,
            Desc    = desc,
            Steps   = steps.Count > 0 ? steps : existing?.Steps ?? [],
            Tags    = tags.Count  > 0 ? tags  : existing?.Tags  ?? [],
            Author  = author ?? existing?.Author ?? "claude",
            Created = existing?.Created ?? DateTime.UtcNow,
            Version = existing != null ? BumpVersion(existing.Version) : "1.0"
        };

        skill.Save(path);
        Console.WriteLine($"[SKILL] Saved: {app}/{slug}.skill.json (v{skill.Version})");
        return 0;
    }

    // ── Export ────────────────────────────────────────────────────────────────

    static int SkillExportCommand(string[] args)
    {
        string? appFilter = null, outPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app"  && i + 1 < args.Length) appFilter = args[++i];
            if (args[i] == "--out"  && i + 1 < args.Length) outPath   = args[++i];
        }

        outPath ??= Path.Combine(Directory.GetCurrentDirectory(),
            appFilter != null ? $"skills-{appFilter}.zip" : "skills.zip");

        if (!Directory.Exists(SkillsDir))
        {
            Console.WriteLine("[SKILL] No skills to export.");
            return 1;
        }

        if (File.Exists(outPath)) File.Delete(outPath);
        using var zip = ZipFile.Open(outPath, ZipArchiveMode.Create);

        int count = 0;
        foreach (var f in Directory.GetFiles(SkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(SkillsDir, f).Replace('\\', '/');
            if (appFilter != null && !rel.StartsWith(appFilter + "/", StringComparison.OrdinalIgnoreCase))
                continue;
            zip.CreateEntryFromFile(f, rel);
            count++;
        }

        Console.WriteLine($"[SKILL] Exported {count} skill(s) → {outPath}");
        return 0;
    }

    // ── Import ────────────────────────────────────────────────────────────────

    static int SkillImportCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill import <file.zip>");
            return 1;
        }

        var zipPath = args[0];
        if (!File.Exists(zipPath))
        {
            Console.WriteLine($"[SKILL] File not found: {zipPath}");
            return 1;
        }

        Directory.CreateDirectory(SkillsDir);
        int count = 0;
        using var zip = ZipFile.OpenRead(zipPath);
        foreach (var entry in zip.Entries.Where(e => e.Name.EndsWith(".skill.json")))
        {
            var dest = Path.Combine(SkillsDir, entry.FullName.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            entry.ExtractToFile(dest, overwrite: true);
            count++;
        }

        Console.WriteLine($"[SKILL] Imported {count} skill(s) from {zipPath}");
        return 0;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    static SkillRecord? FindSkill(string id)
    {
        if (!Directory.Exists(SkillsDir)) return null;
        foreach (var f in Directory.GetFiles(SkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var s = SkillRecord.Load(f);
            if (s != null && s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                return s;
        }
        return null;
    }

    static string Slugify(string title) =>
        Regex.Replace(title.ToLowerInvariant(), @"[^a-z0-9]+", "-").Trim('-');

    static string BumpVersion(string? v)
    {
        if (string.IsNullOrEmpty(v)) return "1.1";
        var parts = v.Split('.');
        if (parts.Length >= 2 && int.TryParse(parts[1], out int minor))
            return $"{parts[0]}.{minor + 1}";
        return v + ".1";
    }

    static int SkillUsage()
    {
        Console.WriteLine("Usage: wkappbot skill <command>");
        Console.WriteLine();
        Console.WriteLine("  list [app]                        List skills (optionally filtered by app)");
        Console.WriteLine("  show <id>                         Show skill details");
        Console.WriteLine("  contribute --app X --title Y --desc Z [--steps \"s1|s2\"] [--tags \"t1,t2\"]");
        Console.WriteLine("                                    Save a new or updated skill");
        Console.WriteLine("  export [--app X] [--out file.zip] Export skills to zip");
        Console.WriteLine("  import <file.zip>                 Import skills from zip");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot skill list");
        Console.WriteLine("  wkappbot skill list wkappbot-webbot");
        Console.WriteLine("  wkappbot skill show cdp-eval-taskcancel-retry");
        Console.WriteLine("  wkappbot skill contribute --app wkappbot-webbot --title \"CDP Retry\" --desc \"retry on cancel\"");
        Console.WriteLine("  wkappbot skill export --app wkappbot-webbot --out skills.zip");
        Console.WriteLine("  wkappbot skill import skills.zip");
        return 0;
    }
}

// ── SkillRecord ───────────────────────────────────────────────────────────────

internal sealed class SkillRecord
{
    [JsonPropertyName("id")]      public string Id      { get; set; } = "";
    [JsonPropertyName("app")]     public string App     { get; set; } = "";
    [JsonPropertyName("title")]   public string Title   { get; set; } = "";
    [JsonPropertyName("desc")]    public string Desc    { get; set; } = "";
    [JsonPropertyName("steps")]   public List<string> Steps { get; set; } = [];
    [JsonPropertyName("tags")]    public List<string> Tags  { get; set; } = [];
    [JsonPropertyName("author")]  public string? Author  { get; set; }
    [JsonPropertyName("created")] public DateTime Created { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("version")] public string? Version  { get; set; }

    static readonly JsonSerializerOptions Opts = new() { WriteIndented = true };

    public void Save(string path) =>
        File.WriteAllText(path, JsonSerializer.Serialize(this, Opts));

    public static SkillRecord? Load(string path)
    {
        try { return JsonSerializer.Deserialize<SkillRecord>(File.ReadAllText(path)); }
        catch { return null; }
    }
}
