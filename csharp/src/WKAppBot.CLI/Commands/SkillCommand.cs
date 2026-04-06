using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

// partial class: wkappbot skill <subcommand> — executable automation skills
// Storage:
//   project skills (managed): {callerCwd}/skills/   ← contribute/delete here
//   hq skills (installed):    {DataDir}/skills/     ← copied via `skill install`
//   list/search merges both
internal partial class Program
{
    // Project skills dir: callerCwd/skills/ (version-controlled, editable)
    static string ProjectSkillsDir
    {
        get
        {
            var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            return Path.Combine(cwd, "skills");
        }
    }

    // HQ skills dir: wkappbot.hq/skills/ (installed, read-only at runtime)
    static string HqSkillsDir => Path.Combine(DataDir, "skills");

    static int SkillCommand(string[] args)
    {
        if (args.Length == 0)
            return SkillUsage();

        return args[0].ToLowerInvariant() switch
        {
            "list"       => SkillListCommand(args.Skip(1).ToArray()),
            "show"       => SkillShowCommand(args.Skip(1).ToArray()),
            "contribute" => SkillContributeCommand(args.Skip(1).ToArray()),
            "delete"     => SkillDeleteCommand(args.Skip(1).ToArray()),
            "search"     => SkillSearchCommand(args.Skip(1).ToArray()),
            "install"    => SkillInstallCommand(args.Skip(1).ToArray()),
            "export"     => SkillExportCommand(args.Skip(1).ToArray()),
            "import"     => SkillImportCommand(args.Skip(1).ToArray()),
            "verify"     => SkillVerifyCommand(args.Skip(1).ToArray()),
            "audit"      => SkillAuditCommand(args.Skip(1).ToArray()),
            "--help" or "-h" or "help" => SkillUsage(),
            var s => Error($"Unknown skill sub-command: {s}")
        };
    }

    // ── List ──────────────────────────────────────────────────────────────────

    static int SkillListCommand(string[] args)
    {
        string? appFilter = args.Length > 0 && !args[0].StartsWith('-') ? args[0] : null;

        var all = LoadAllSkills(appFilter);
        if (all.Count == 0)
        {
            Console.WriteLine(appFilter != null
                ? $"[SKILL] No skills for app: {appFilter}"
                : "[SKILL] No skills yet. Use: wkappbot skill contribute --app X --title Y --desc Z");
            return 0;
        }

        var byApp = all
            .GroupBy(s => s.App, StringComparer.OrdinalIgnoreCase)
            .OrderBy(g => g.Key);

        int total = all.Count;
        Console.WriteLine($"[SKILL] {total} skill(s){(appFilter != null ? $" in '{appFilter}'" : "")}:");
        foreach (var group in byApp)
        {
            Console.WriteLine($"  [{group.Key}]");
            foreach (var s in group.OrderBy(x => x.Title))
            {
                var ver = s.Version != null ? $" v{s.Version}" : "";
                var src = s.Source == SkillSource.Hq ? " [HQ]" : "";
                var titleSlug = Slugify(s.Title);
                var idPart = titleSlug != s.Id ? $" ({s.Id})" : "";
                Console.WriteLine($"    • {s.Title}{idPart}{ver}{src} — {s.Desc}");
            }
        }

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

        var skill = FindSkill(args[0]);
        if (skill == null)
        {
            Console.WriteLine($"[SKILL] Not found: {args[0]}");
            Console.WriteLine("  Hint: wkappbot skill list  or  wkappbot skill search <keyword>");
            return 1;
        }

        Console.WriteLine($"[SKILL] {skill.Title}");
        Console.WriteLine($"  ID     : {skill.Id}");
        Console.WriteLine($"  App    : {skill.App}");
        Console.WriteLine($"  Desc   : {skill.Desc}");
        if (skill.Tags?.Count > 0)
            Console.WriteLine($"  Tags   : {string.Join(", ", skill.Tags)}");
        if (skill.Steps?.Count > 0)
        {
            Console.WriteLine("  Steps  :");
            for (int i = 0; i < skill.Steps.Count; i++)
                Console.WriteLine($"    {i + 1}. {skill.Steps[i]}");
        }
        if (skill.SourceRefs?.Count > 0)
        {
            Console.WriteLine("  Refs   :");
            foreach (var r in skill.SourceRefs)
            {
                var suffix = r.Line.HasValue ? $":{r.Line}" : "";
                var pat = !string.IsNullOrEmpty(r.Pattern) ? $" → \"{r.Pattern}\"" : "";
                var note = !string.IsNullOrEmpty(r.Note) ? $" ({r.Note})" : "";
                Console.WriteLine($"    {r.File}{suffix}{pat}{note}");
            }
        }
        if (!string.IsNullOrEmpty(skill.Author))
            Console.WriteLine($"  Author : {skill.Author}");
        Console.WriteLine($"  Created: {skill.Created:yyyy-MM-dd}  Version: {skill.Version ?? "1.0"}");
        return 0;
    }

    // ── Search ────────────────────────────────────────────────────────────────

    static int SkillSearchCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill search <keyword> [--app X]");
            return 1;
        }

        string? appFilter = null;
        var keywords = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];
            else keywords.Add(args[i].ToLowerInvariant());
        }

        var hits = LoadAllSkills(appFilter)
            .Where(s => keywords.All(kw =>
                s.Id.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                s.Title.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                s.Desc.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                (s.Tags?.Any(t => t.Contains(kw, StringComparison.OrdinalIgnoreCase)) ?? false) ||
                (s.Steps?.Any(t => t.Contains(kw, StringComparison.OrdinalIgnoreCase)) ?? false)))
            .ToList();

        if (hits.Count == 0)
        {
            Console.WriteLine($"[SKILL] No results for: {string.Join(" ", keywords)}");
            return 0;
        }

        Console.WriteLine($"[SKILL] {hits.Count} match(es):");
        foreach (var s in hits)
            Console.WriteLine($"  [{s.App}] {s.Id} — {s.Title}");
        Console.WriteLine($"\n  Use: wkappbot skill show <id>");
        return 0;
    }

    // ── Contribute ────────────────────────────────────────────────────────────

    static int SkillContributeCommand(string[] args)
    {
        string? app = null, title = null, desc = null, author = null, id = null;
        var steps = new List<string>();
        var tags = new List<string>();
        var refs = new List<SkillSourceRef>();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--app"    when i + 1 < args.Length: app    = args[++i]; break;
                case "--title"  when i + 1 < args.Length: title  = args[++i]; break;
                case "--desc"   when i + 1 < args.Length: desc   = args[++i]; break;
                case "--author" when i + 1 < args.Length: author = args[++i]; break;
                case "--id"     when i + 1 < args.Length: id     = args[++i]; break;
                case "--steps"  when i + 1 < args.Length: steps.AddRange(args[++i].Split('|')); break;
                case "--tags"   when i + 1 < args.Length: tags.AddRange(args[++i].Split(',')); break;
                // --refs "file.cs:42:pattern|file2.cs::pattern2"
                case "--refs"   when i + 1 < args.Length:
                    foreach (var r in args[++i].Split('|'))
                    {
                        var parts = r.Split(':', 3);
                        refs.Add(new SkillSourceRef
                        {
                            File    = parts[0],
                            Line    = parts.Length > 1 && int.TryParse(parts[1], out int ln) ? ln : null,
                            Pattern = parts.Length > 2 && !string.IsNullOrEmpty(parts[2]) ? parts[2] : null
                        });
                    }
                    break;
            }
        }

        if (string.IsNullOrEmpty(app) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(desc))
        {
            Console.WriteLine("Usage: wkappbot skill contribute --app <app> --title <title> --desc <desc>");
            Console.WriteLine("  Options: --steps \"s1|s2\"  --tags \"t1,t2\"  --id <slug>  --author <name>");
            return 1;
        }

        var slug = id ?? Slugify(title);
        if (string.IsNullOrEmpty(slug))
        {
            Console.WriteLine("[SKILL] Could not generate ID from title. Use --id <slug> explicitly.");
            return 1;
        }

        var appDir = Path.Combine(ProjectSkillsDir, app);
        Directory.CreateDirectory(appDir);
        var path = Path.Combine(appDir, $"{slug}.skill.json");

        var existing = File.Exists(path) ? SkillRecord.Load(path) : null;
        var skill = new SkillRecord
        {
            Id         = slug,
            App        = app,
            Title      = title,
            Desc       = desc,
            Steps      = steps.Count > 0 ? steps : existing?.Steps ?? [],
            Tags       = tags.Count  > 0 ? tags  : existing?.Tags  ?? [],
            SourceRefs = refs.Count  > 0 ? refs  : existing?.SourceRefs,
            Author     = author ?? existing?.Author ?? "claude",
            Created    = existing?.Created ?? DateTime.UtcNow,
            Version    = existing != null ? BumpVersion(existing.Version) : "1.0"
        };

        skill.Save(path);
        var action = existing != null ? "Updated" : "Created";
        Console.WriteLine($"[SKILL] {action}: [{app}] {title} (id={slug}, v{skill.Version})");
        return 0;
    }

    // ── Delete ────────────────────────────────────────────────────────────────

    static int SkillDeleteCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill delete <id>");
            return 1;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills directory.");
            return 1;
        }

        var id = args[0];
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var s = SkillRecord.Load(f);
            if (s == null || !s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)) continue;
            File.Delete(f);
            Console.WriteLine($"[SKILL] Deleted: [{s.App}] {s.Title} ({id})");
            return 0;
        }

        Console.WriteLine($"[SKILL] Not found in project skills: {id}");
        return 1;
    }

    // ── Install ───────────────────────────────────────────────────────────────

    static int SkillInstallCommand(string[] args)
    {
        string? appFilter = null;
        bool force = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app"   && i + 1 < args.Length) appFilter = args[++i];
            if (args[i] == "--force") force = true;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills to install.");
            return 0;
        }

        int copied = 0, skipped = 0;
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var rel  = Path.GetRelativePath(ProjectSkillsDir, f);
            if (appFilter != null && !rel.StartsWith(appFilter + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                continue;

            var dest = Path.Combine(HqSkillsDir, rel);
            if (!force && File.Exists(dest))
            {
                // Only overwrite if project version is newer
                var src  = SkillRecord.Load(f);
                var existing = SkillRecord.Load(dest);
                if (src != null && existing != null && CompareVersions(src.Version, existing.Version) <= 0)
                { skipped++; continue; }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            File.Copy(f, dest, overwrite: true);
            copied++;
            Console.WriteLine($"[SKILL] Installed: {rel}");
        }

        Console.WriteLine($"[SKILL] Install done: {copied} installed, {skipped} skipped (already up-to-date).");
        return 0;
    }

    static int CompareVersions(string? a, string? b)
    {
        static (int maj, int min) Parse(string? v)
        {
            var parts = (v ?? "1.0").Split('.');
            int.TryParse(parts.ElementAtOrDefault(0), out int maj);
            int.TryParse(parts.ElementAtOrDefault(1), out int min);
            return (maj, min);
        }
        var (am, an) = Parse(a);
        var (bm, bn) = Parse(b);
        var cmp = am.CompareTo(bm);
        return cmp != 0 ? cmp : an.CompareTo(bn);
    }

    // ── Export ────────────────────────────────────────────────────────────────

    static int SkillExportCommand(string[] args)
    {
        string? appFilter = null, outPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];
            if (args[i] == "--out" && i + 1 < args.Length) outPath   = args[++i];
        }

        outPath ??= Path.Combine(Directory.GetCurrentDirectory(),
            appFilter != null ? $"skills-{appFilter}.zip" : "skills.zip");

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills to export.");
            return 1;
        }

        if (File.Exists(outPath)) File.Delete(outPath);
        using var zip = ZipFile.Open(outPath, ZipArchiveMode.Create);

        int count = 0;
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(ProjectSkillsDir, f).Replace('\\', '/');
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

        Directory.CreateDirectory(ProjectSkillsDir);
        int count = 0;
        using var zip = ZipFile.OpenRead(zipPath);
        foreach (var entry in zip.Entries.Where(e => e.Name.EndsWith(".skill.json")))
        {
            var dest = Path.Combine(ProjectSkillsDir, entry.FullName.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
            entry.ExtractToFile(dest, overwrite: true);
            count++;
        }

        Console.WriteLine($"[SKILL] Imported {count} skill(s) from {zipPath}");
        return 0;
    }

    // ── Verify ────────────────────────────────────────────────────────────────

    static int SkillVerifyCommand(string[] args)
    {
        if (args.Length == 0) { Console.WriteLine("Usage: wkappbot skill verify <id>"); return 1; }
        var skill = FindSkill(args[0]);
        if (skill == null) { Console.WriteLine($"[SKILL] Not found: {args[0]}"); return 1; }
        var (ok, missing, stale) = RunVerify(skill, verbose: true);
        return (missing + stale > 0) ? 1 : 0;
    }

    // ── Audit ─────────────────────────────────────────────────────────────────

    static int SkillAuditCommand(string[] args)
    {
        string? appFilter = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];

        var skills = LoadAllSkills(appFilter);
        if (skills.Count == 0) { Console.WriteLine("[SKILL] No skills to audit."); return 0; }

        int totalOk = 0, totalIssues = 0, noRefs = 0;
        var issueIds = new List<string>();

        Console.WriteLine($"[SKILL] Auditing {skills.Count} skill(s)...");
        foreach (var skill in skills.OrderBy(s => s.App).ThenBy(s => s.Id))
        {
            if (skill.SourceRefs == null || skill.SourceRefs.Count == 0) { noRefs++; continue; }
            var (ok, missing, stale) = RunVerify(skill, verbose: false);
            if (missing + stale > 0)
            {
                Console.WriteLine($"  ⚠ [{skill.App}] {skill.Id}:");
                RunVerify(skill, verbose: true);
                issueIds.Add(skill.Id);
                totalIssues++;
            }
            else totalOk++;
        }

        Console.WriteLine();
        Console.WriteLine($"[SKILL] Audit: {totalOk} ok, {totalIssues} stale/missing, {noRefs} without refs");
        if (issueIds.Count > 0)
        {
            Console.WriteLine("  → Fix: wkappbot skill show <id>  then  wkappbot skill contribute ...");
            foreach (var id in issueIds) Console.WriteLine($"    wkappbot skill show {id}");
        }
        return totalIssues > 0 ? 1 : 0;
    }

    // Returns (ok, missing, stale). When verbose=true prints per-ref status.
    static (int ok, int missing, int stale) RunVerify(SkillRecord skill, bool verbose)
    {
        if (skill.SourceRefs == null || skill.SourceRefs.Count == 0)
        {
            if (verbose) Console.WriteLine($"[SKILL] {skill.Id}: no source_refs (nothing to verify)");
            return (0, 0, 0);
        }

        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        int ok = 0, missing = 0, stale = 0;

        foreach (var r in skill.SourceRefs)
        {
            var absPath = Path.IsPathRooted(r.File) ? r.File : Path.Combine(cwd, r.File);
            if (!File.Exists(absPath))
            {
                if (verbose) Console.WriteLine($"    ❌ FILE MISSING: {r.File}");
                missing++; continue;
            }
            if (string.IsNullOrEmpty(r.Pattern))
            {
                if (verbose) Console.WriteLine($"    ✅ {r.File} — file exists");
                ok++; continue;
            }

            string[] lines;
            try { lines = File.ReadAllLines(absPath); }
            catch { missing++; continue; }

            int foundLine = -1;
            for (int i = 0; i < lines.Length; i++)
                if (lines[i].Contains(r.Pattern, StringComparison.Ordinal)) { foundLine = i + 1; break; }

            if (foundLine > 0)
            {
                if (verbose) Console.WriteLine($"    ✅ {r.File}:{foundLine} — \"{r.Pattern}\"");
                ok++;
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine($"    ⚠ PATTERN NOT FOUND: \"{r.Pattern}\" in {r.File}");
                    if (r.Line.HasValue)
                    {
                        int start = Math.Max(0, r.Line.Value - 2);
                        int count = Math.Min(5, lines.Length - start);
                        Console.WriteLine($"    (was line {r.Line}, now reads:)");
                        foreach (var l in lines.Skip(start).Take(count)) Console.WriteLine($"      | {l.TrimEnd()}");
                    }
                }
                stale++;
            }
        }

        if (verbose && missing + stale == 0)
            Console.WriteLine($"[SKILL] {skill.Id}: ✅ all {ok} ref(s) OK");
        return (ok, missing, stale);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    // Loads from project dir + hq dir, deduplicating by id (project wins)
    static List<SkillRecord> LoadAllSkills(string? appFilter = null)
    {
        var seen = new Dictionary<string, SkillRecord>(StringComparer.OrdinalIgnoreCase);

        void LoadFrom(string dir, SkillSource source)
        {
            if (!Directory.Exists(dir)) return;
            foreach (var f in Directory.GetFiles(dir, "*.skill.json", SearchOption.AllDirectories))
            {
                var s = SkillRecord.Load(f);
                if (s == null) continue;
                if (appFilter != null && !s.App.Contains(appFilter, StringComparison.OrdinalIgnoreCase)) continue;
                s.Source = source;
                if (!seen.ContainsKey(s.Id)) seen[s.Id] = s; // project wins if loaded first
            }
        }

        LoadFrom(ProjectSkillsDir, SkillSource.Project);
        LoadFrom(HqSkillsDir, SkillSource.Hq);
        return [.. seen.Values];
    }

    static SkillRecord? FindSkill(string id)
    {
        // Project takes priority over HQ
        foreach (var dir in new[] { ProjectSkillsDir, HqSkillsDir })
        {
            if (!Directory.Exists(dir)) continue;
            foreach (var f in Directory.GetFiles(dir, "*.skill.json", SearchOption.AllDirectories))
            {
                var s = SkillRecord.Load(f);
                if (s != null && s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return s;
            }
        }
        return null;
    }

    static string Slugify(string title)
    {
        // Strip non-ASCII (CJK etc.) — caller must use --id for Korean-only titles
        var ascii = Regex.Replace(title.ToLowerInvariant(), @"[^a-z0-9]+", "-").Trim('-');
        return ascii; // empty string = caller must supply --id
    }

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
        Console.WriteLine("Storage: {callerCwd}/skills/  (project, git-tracked)");
        Console.WriteLine("         {hq}/skills/          (installed, [HQ] tag in list)");
        Console.WriteLine();
        Console.WriteLine("  list [app]                        List skills grouped by app");
        Console.WriteLine("  show <id>                         Show skill details");
        Console.WriteLine("  search <keyword> [--app X]        Search by keyword in title/desc/tags");
        Console.WriteLine("  contribute --app X --title Y --desc Z");
        Console.WriteLine("             [--steps \"s1|s2\"] [--tags \"t1,t2\"] [--id <slug>]");
        Console.WriteLine("                                    Create or update skill in project dir");
        Console.WriteLine("  delete <id>                       Remove skill from project dir");
        Console.WriteLine("  install [--app X] [--force]       Copy project skills → HQ (on deploy)");
        Console.WriteLine("  export [--app X] [--out f.zip]    Export project skills to zip");
        Console.WriteLine("  import <file.zip>                 Import skills into project dir");
        Console.WriteLine("  verify <id>                       Check if source_refs still match code");
        Console.WriteLine("  audit [--app X]                   Audit all skills for stale/missing refs");
        Console.WriteLine();
        Console.WriteLine("  --refs \"file:line:pattern|...\"   Source code anchor for self-heal verify");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot skill list");
        Console.WriteLine("  wkappbot skill audit");
        Console.WriteLine("  wkappbot skill verify chatgpt-blank-false-positive-guard");
        Console.WriteLine("  wkappbot skill search retry");
        Console.WriteLine("  wkappbot skill show cdp-eval-taskcancel-retry");
        Console.WriteLine("  wkappbot skill contribute --app wkappbot-webbot \\");
        Console.WriteLine("    --title \"CDP EvalAsync Retry\" --desc \"retry on cancel/timeout\" \\");
        Console.WriteLine("    --steps \"catch TaskCanceledException|500ms backoff\" --tags \"cdp,retry\" \\");
        Console.WriteLine("    --refs \"csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs::TaskCanceledException\"");
        Console.WriteLine("  wkappbot skill install           # deploy to HQ after publish");
        Console.WriteLine("  wkappbot skill delete test-skill");
        return 0;
    }
}

// ── SkillSource / SkillRecord ─────────────────────────────────────────────────

internal enum SkillSource { Project, Hq }

internal sealed class SkillSourceRef
{
    [JsonPropertyName("file")]    public string  File    { get; set; } = "";
    [JsonPropertyName("line")]    public int?    Line    { get; set; }
    [JsonPropertyName("pattern")] public string? Pattern { get; set; }
    [JsonPropertyName("note")]    public string? Note    { get; set; }
}

internal sealed class SkillRecord
{
    [JsonPropertyName("id")]          public string Id      { get; set; } = "";
    [JsonPropertyName("app")]         public string App     { get; set; } = "";
    [JsonPropertyName("title")]       public string Title   { get; set; } = "";
    [JsonPropertyName("desc")]        public string Desc    { get; set; } = "";
    [JsonPropertyName("steps")]       public List<string> Steps { get; set; } = [];
    [JsonPropertyName("tags")]        public List<string> Tags  { get; set; } = [];
    [JsonPropertyName("source_refs")] public List<SkillSourceRef>? SourceRefs { get; set; }
    [JsonPropertyName("author")]      public string? Author  { get; set; }
    [JsonPropertyName("created")]     public DateTime Created { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("version")]     public string? Version  { get; set; }

    [JsonIgnore] public SkillSource Source { get; set; } = SkillSource.Project;

    static readonly JsonSerializerOptions Opts = new() { WriteIndented = true };

    public void Save(string path) =>
        File.WriteAllText(path, JsonSerializer.Serialize(this, Opts));

    public static SkillRecord? Load(string path)
    {
        try { return JsonSerializer.Deserialize<SkillRecord>(File.ReadAllText(path)); }
        catch { return null; }
    }
}
