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
    static readonly string[] SkillAudiences = ["operator", "developer", "project", "user"];

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
            "edit"       => SkillEditCommand(args.Skip(1).ToArray()),
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

    // ── List ─────────────────────────────────────────────────────────────────────

    static int SkillListCommand(string[] args)
    {
        string? appFilter = null;
        string? audienceFilter = null;
        foreach (var arg in args.Where(a => !a.StartsWith('-')))
        {
            if (audienceFilter == null && IsSkillAudience(arg))
                audienceFilter = arg;
            else if (appFilter == null)
                appFilter = arg;
        }

        var all = LoadAllSkills(appFilter);
        if (!string.IsNullOrWhiteSpace(audienceFilter))
            all = [.. all.Where(s => HasAudience(s, audienceFilter))];
        if (all.Count == 0)
        {
            Console.WriteLine(audienceFilter != null
                ? $"[SKILL] No skills for audience: {audienceFilter}"
                : appFilter != null
                    ? $"[SKILL] No skills for app: {appFilter}"
                    : "[SKILL] No skills yet. Use: wkappbot skill contribute --app X --title Y --desc Z");
            return 0;
        }

        var byApp = all
            .GroupBy(s => s.App, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(g => g.Max(s => s.LastActivity)); // app with most recent skill first

        int total = all.Count;
        var filterBits = new List<string>();
        if (appFilter != null) filterBits.Add($"app='{appFilter}'");
        if (!string.IsNullOrWhiteSpace(audienceFilter)) filterBits.Add($"audience='{audienceFilter}'");
        var filterSuffix = filterBits.Count > 0 ? $" ({string.Join(", ", filterBits)})" : "";
        Console.Error.WriteLine($"[SKILL] {total} skill(s){filterSuffix} - most recent first:");
        foreach (var group in byApp)
        {
            Console.WriteLine($"  [{group.Key}]");
            foreach (var s in group.OrderByDescending(x => x.LastActivity))
            {
                var ver = s.Version != null ? $" v{s.Version}" : "";
                var src = s.Source == SkillSource.Hq ? " [HQ]" : "";
                var titleSlug = Slugify(s.Title);
                var idPart = titleSlug != s.Id ? $" ({s.Id})" : "";
                var age = FormatAge(s.LastActivity);
                Console.WriteLine($"    - {s.Title}{idPart}{ver}{src} [{age}] [{AudienceProfile(s)}] - {s.Desc}");
            }
        }

        return 0;
    }

    // ── Show ─────────────────────────────────────────────────────────────────────

    static int SkillShowCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill show <id> [--developer]");
            return 1;
        }

        bool developerMode = args.Any(a => a == "--developer" || a == "--dev");
        var id = args.First(a => !a.StartsWith("--"));

        var skill = FindSkill(id);
        if (skill == null)
        {
            Console.Error.WriteLine($"[SKILL] Not found: {id}");
            Console.WriteLine("  Hint: wkappbot skill list  or  wkappbot skill search <keyword>");
            return 1;
        }

        Console.Error.WriteLine($"[SKILL] {skill.Title}");
        Console.WriteLine($"  ID     : {skill.Id}");
        Console.WriteLine($"  App    : {skill.App}");
        Console.WriteLine($"  Desc   : {skill.Desc}");
        Console.WriteLine($"  Audience: {AudienceProfile(skill)}");
        if (skill.Tags?.Count > 0)
            Console.WriteLine($"  Tags   : {string.Join(", ", skill.Tags)}");

        if (skill.Steps?.Count > 0)
        {
            // Steps prefixed with [DEV] are developer-only — hidden in default view.
            // All other steps are operator-visible.
            var opSteps  = skill.Steps.Select((s, i) => (s, i)).Where(x => !IsDevStep(x.s)).ToList();
            var devSteps = skill.Steps.Select((s, i) => (s, i)).Where(x =>  IsDevStep(x.s)).ToList();

            Console.WriteLine("  Steps  :");
            int display = 1;
            foreach (var (s, _) in opSteps)
                Console.WriteLine($"    {display++}. {s}");

            if (developerMode)
            {
                foreach (var (s, _) in devSteps)
                    Console.WriteLine($"    {display++}. {s}");
            }
            else if (devSteps.Count > 0)
            {
                Console.WriteLine($"    (+{devSteps.Count} [DEV] step(s) hidden — use --developer to show)");
            }
        }

        if (developerMode && skill.SourceRefs?.Count > 0)
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

    // A step is developer-only if:
    // (1) explicitly tagged [DEV], OR
    // (2) auto-detected as implementation detail:
    //     - contains a source file reference (path with .cs/.vbs/.json + : or /)
    //     - contains a method/function call (Word followed by parentheses)
    //     - contains a Windows absolute path (X:\ or csharp/ or src/)
    //     - contains a line-number reference (SomeFile.cs:123)
    static readonly System.Text.RegularExpressions.Regex _devStepPattern =
        new(@"
            \[DEV\]                             # explicit tag
            | [A-Za-z]:\\                       # Windows path (C:\, W:\)
            | \bcsharp/|\bsrc/                  # Unix-style source path
            | \w+\.cs[:/\s]                     # .cs file reference
            | \w+\.[A-Za-z]+\(\)               # MethodCall() or Class.Method()
            | \w+\.[A-Za-z]+\([^)]{1,40}\)     # Method(args) — code call with args
            | :\d{2,}                           # :123 line number reference
        ", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace |
           System.Text.RegularExpressions.RegexOptions.IgnoreCase |
           System.Text.RegularExpressions.RegexOptions.Compiled);

    static bool IsDevStep(string step) => _devStepPattern.IsMatch(step);

    // ── Search ───────────────────────────────────────────────────────────────────

    static int SkillSearchCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill search <keyword> [--app X] [--audience operator|developer|project|user]");
            return 1;
        }

        string? appFilter = null;
        string? audienceFilter = null;
        var keywords = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];
            else if (args[i] == "--audience" && i + 1 < args.Length) audienceFilter = args[++i];
            else keywords.Add(args[i].ToLowerInvariant());
        }

        var hits = LoadAllSkills(appFilter)
            .Where(s => string.IsNullOrWhiteSpace(audienceFilter) || HasAudience(s, audienceFilter))
            .Where(s => keywords.All(kw =>
                s.Id.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                s.Title.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                s.Desc.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                (s.Tags?.Any(t => t.Contains(kw, StringComparison.OrdinalIgnoreCase)) ?? false) ||
                (s.Steps?.Any(t => t.Contains(kw, StringComparison.OrdinalIgnoreCase)) ?? false)))
            .ToList();

        if (hits.Count == 0)
        {
            Console.Error.WriteLine($"[SKILL] No results for: {string.Join(" ", keywords)}");
            return 0;
        }

        Console.Error.WriteLine($"[SKILL] {hits.Count} match(es):");
        foreach (var s in hits)
            Console.WriteLine($"  [{s.App}] {s.Id} [{AudienceProfile(s)}] - {s.Title}");
        Console.WriteLine($"\n  Use: wkappbot skill show <id>");
        return 0;
    }

    // ── Contribute ───────────────────────────────────────────────────────────────

    static int SkillContributeCommand(string[] args)
    {
        string? app = null, title = null, desc = null, author = null, id = null, regressionScript = null;
        var steps = new List<string>();
        var tags = new List<string>();
        var refs = new List<SkillSourceRef>();

        bool force = args.Contains("--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste");
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--app"               when i + 1 < args.Length: app              = args[++i]; break;
                case "--title"             when i + 1 < args.Length: title            = args[++i]; break;
                case "--desc"              when i + 1 < args.Length: desc             = args[++i]; break;
                case "--author"            when i + 1 < args.Length: author           = args[++i]; break;
                case "--id"                when i + 1 < args.Length: id               = args[++i]; break;
                case "--steps"             when i + 1 < args.Length: steps.AddRange(args[++i].Split('|')); break;
                case "--tags"              when i + 1 < args.Length: tags.AddRange(args[++i].Split(',')); break;
                case "--regression-script" when i + 1 < args.Length: regressionScript = args[++i]; break;
                case "--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste": break;
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
            Console.WriteLine("           --regression-script <path>  (registers test script into experience/tests/)");
            return 1;
        }

        // Encoding risk check: reject Korean/emoji in skill content
        var allContent = string.Join(" ", new[] { title, desc }.Concat(steps).Where(s => s != null)!);
        if (!force && HasEncodingRisk(allContent, "skill contribute")) return 1;

        var slug = id ?? Slugify(title);
        if (string.IsNullOrEmpty(slug))
        {
            Console.WriteLine("[SKILL] Could not generate ID from title. Use --id <slug> explicitly.");
            return 1;
        }

        var appDir = Path.Combine(ProjectSkillsDir, app);
        Directory.CreateDirectory(appDir);
        var path = Path.Combine(appDir, $"{slug}.skill.json");

        // Register regression script into experience/tests/{cmd}/{subcmd}/
        string? regScriptRef = null;
        if (!string.IsNullOrEmpty(regressionScript))
        {
            regScriptRef = RegisterRegressionScript(regressionScript, slug);
            if (regScriptRef == null) return 1; // error already printed
        }

        var existing = File.Exists(path) ? SkillRecord.Load(path) : null;
        var skill = new SkillRecord
        {
            Id               = slug,
            App              = app,
            Title            = title,
            Desc             = desc,
            Steps            = steps.Count > 0 ? steps : existing?.Steps ?? [],
            Tags             = tags.Count  > 0 ? tags  : existing?.Tags  ?? [],
            SourceRefs       = refs.Count  > 0 ? refs  : existing?.SourceRefs,
            RegressionScript = regScriptRef ?? existing?.RegressionScript,
            Author           = author ?? existing?.Author ?? "claude",
            Created          = existing?.Created ?? DateTime.UtcNow,
            Updated          = DateTime.UtcNow,
            Version          = existing != null ? BumpVersion(existing.Version) : "1.0"
        };

        skill.Save(path);
        var action = existing != null ? "Updated" : "Created";
        Console.Error.WriteLine($"[SKILL] {action}: [{app}] {title} (id={slug}, v{skill.Version})");
        if (regScriptRef != null)
            Console.Error.WriteLine($"[SKILL] Regression script: {regScriptRef} (auto-runs on suggest resolve)");
        return 0;
    }

    // Copies regression script into experience/tests/{cmd}/{subcmd}/ and returns relative path from DataDir.
    // Filename convention: test-{cmd}-{subcmd}[-desc].sh  →  cmd + subcmd extracted from filename.
    static string? RegisterRegressionScript(string scriptPath, string skillSlug)
    {
        if (!File.Exists(scriptPath))
        {
            Console.Error.WriteLine($"[SKILL] --regression-script not found: {scriptPath}");
            return null;
        }

        var fileName = Path.GetFileNameWithoutExtension(scriptPath);
        var parts    = fileName.Split('-');
        // "test-{cmd}-{subcmd}" or fallback
        var cmd    = parts.Length > 1 ? parts[1] : "misc";
        var subcmd = parts.Length > 2 ? parts[2] : "general";

        var testsDir = Path.Combine(DataDir, "experience", "tests", cmd, subcmd);
        Directory.CreateDirectory(testsDir);
        var destName = Path.GetFileName(scriptPath);
        var destPath = Path.Combine(testsDir, destName);
        File.Copy(scriptPath, destPath, overwrite: true);

        // Update .manifest (used by RunRegressionTests deleted-test detection)
        var manifestPath = Path.Combine(testsDir, ".manifest");
        var manifest = File.Exists(manifestPath)
            ? new HashSet<string>(File.ReadAllLines(manifestPath).Select(l => l.Trim()).Where(l => l.Length > 0 && !l.StartsWith('#')))
            : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (manifest.Add(destName))
            File.WriteAllLines(manifestPath, manifest.OrderBy(x => x));

        var relPath = Path.GetRelativePath(DataDir, destPath).Replace('\\', '/');
        Console.Error.WriteLine($"[SKILL] Regression → experience/tests/{cmd}/{subcmd}/{destName}");
        return relPath;
    }

    // ── Edit (partial update) ────────────────────────────────────────────────────

    static int SkillEditCommand(string[] args)
    {
        if (args.Length == 0 || args[0].StartsWith('-'))
        {
            Console.WriteLine("Usage: wkappbot skill edit <id> [--title X] [--desc X] [--tags t1,t2] [--steps s1|s2]");
            Console.WriteLine("                           [--step N --content X]   edit single step by 1-based index");
            Console.WriteLine("                           [--add-step X]           append a new step");
            Console.WriteLine("  Partial update — only specified fields are changed. Version auto-bumped.");
            Console.WriteLine("  Note: to rename the file slug, use delete + contribute --id <new-slug>.");
            return 1;
        }

        var id = args[0];
        string? title = null, desc = null, stepContent = null;
        List<string>? steps = null, tags = null;
        int stepIndex = -1;   // 1-based; -1 = not set
        bool addStep  = false;
        bool force    = args.Contains("--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste");

        for (int i = 1; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--title"     when i + 1 < args.Length: title       = args[++i]; break;
                case "--desc"      when i + 1 < args.Length: desc        = args[++i]; break;
                case "--steps"     when i + 1 < args.Length: steps       = [.. args[++i].Split('|')]; break;
                case "--tags"      when i + 1 < args.Length: tags        = [.. args[++i].Split(',')]; break;
                case "--step"      when i + 1 < args.Length:
                    if (int.TryParse(args[++i], out var si)) stepIndex = si; break;
                case "--content"   when i + 1 < args.Length: stepContent = args[++i]; break;
                case "--add-step"  when i + 1 < args.Length: stepContent = args[++i]; addStep = true; break;
                case "--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste": break;
            }
        }

        // Encoding risk check on edited content
        var editedContent = string.Join(" ", new[] { title, desc, stepContent }.Concat(steps ?? []).Where(s => s != null)!);
        if (!force && !string.IsNullOrEmpty(editedContent) && HasEncodingRisk(editedContent, "skill edit")) return 1;

        bool hasSingleStep = stepIndex > 0 && stepContent != null;
        if (title == null && desc == null && steps == null && tags == null && !hasSingleStep && !addStep)
        {
            Console.WriteLine("[SKILL] Nothing to edit — specify at least one of: --title, --desc, --tags, --steps, --step N --content X, --add-step X");
            return 1;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills directory. Run from project root.");
            return 1;
        }

        string? skillPath = null;
        SkillRecord? skill = null;
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var s = SkillRecord.Load(f);
            if (s != null && s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            { skill = s; skillPath = f; break; }
        }

        if (skill == null || skillPath == null)
        {
            Console.Error.WriteLine($"[SKILL] Not found in project skills: {id}");
            Console.WriteLine("  Hint: 'skill edit' only works for project skills, not HQ-only.");
            Console.WriteLine("  To edit an HQ skill, first copy it: wkappbot skill contribute --app X --title ... --id " + id);
            return 1;
        }

        if (title != null) skill.Title = title;
        if (desc  != null) skill.Desc  = desc;
        if (steps != null) skill.Steps = steps;
        if (tags  != null) skill.Tags  = tags;

        // --step N --content X : edit single step by 1-based index
        if (hasSingleStep)
        {
            skill.Steps ??= [];
            int idx = stepIndex - 1;
            if (idx < 0 || idx >= skill.Steps.Count)
                return Error($"--step {stepIndex} out of range (skill has {skill.Steps.Count} step(s))");
            var old = skill.Steps[idx];
            skill.Steps[idx] = stepContent!;
            Console.Error.WriteLine($"[SKILL] step {stepIndex}: \"{old}\" → \"{stepContent}\"");
        }

        // --add-step X : append new step
        if (addStep && stepContent != null)
        {
            skill.Steps ??= [];
            skill.Steps.Add(stepContent);
            Console.Error.WriteLine($"[SKILL] added step {skill.Steps.Count}: \"{stepContent}\"");
        }

        skill.Updated = DateTime.UtcNow;
        skill.Version = BumpVersion(skill.Version);

        skill.Save(skillPath);
        Console.Error.WriteLine($"[SKILL] Updated: [{skill.App}] {skill.Title} (id={skill.Id}, v{skill.Version})");
        return 0;
    }

    // ── Delete ───────────────────────────────────────────────────────────────────

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
            Console.Error.WriteLine($"[SKILL] Deleted: [{s.App}] {s.Title} ({id})");
            return 0;
        }

        Console.Error.WriteLine($"[SKILL] Not found in project skills: {id}");
        return 1;
    }

    // ── Install ──────────────────────────────────────────────────────────────────

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
            Console.Error.WriteLine($"[SKILL] Installed: {rel}");
        }

        Console.Error.WriteLine($"[SKILL] Install done: {copied} installed, {skipped} skipped (already up-to-date).");
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

    // ── Export ───────────────────────────────────────────────────────────────────

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

        Console.Error.WriteLine($"[SKILL] Exported {count} skill(s) → {outPath}");
        return 0;
    }

    // ── Import ───────────────────────────────────────────────────────────────────

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
            Console.Error.WriteLine($"[SKILL] File not found: {zipPath}");
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

        Console.Error.WriteLine($"[SKILL] Imported {count} skill(s) from {zipPath}");
        return 0;
    }

    // ── Verify ───────────────────────────────────────────────────────────────────

    static int SkillVerifyCommand(string[] args)
    {
        if (args.Length == 0) { Console.WriteLine("Usage: wkappbot skill verify <id>"); return 1; }
        var skill = FindSkill(args[0]);
        if (skill == null) { Console.Error.WriteLine($"[SKILL] Not found: {args[0]}"); return 1; }
        var (ok, missing, stale) = RunVerify(skill, verbose: true);
        var regResult = RunSkillRegressionScript(skill);
        if (regResult == -1)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[SKILL] No regression script registered for '{skill.Id}'.");
            Console.Error.WriteLine($"  → To add one: wkappbot skill contribute --id {skill.Id} --regression-script <test-{skill.App}-*.sh>");
            Console.ResetColor();
        }
        return (missing + stale > 0 || regResult > 0) ? 1 : 0;
    }

    // ── Audit ────────────────────────────────────────────────────────────────────

    static int SkillAuditCommand(string[] args)
    {
        string? appFilter = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];

        var skills = LoadAllSkills(appFilter);
        if (skills.Count == 0) { Console.WriteLine("[SKILL] No skills to audit."); return 0; }

        int totalOk = 0, totalIssues = 0, noRefs = 0;
        var issueIds = new List<string>();

        Console.Error.WriteLine($"[SKILL] Auditing {skills.Count} skill(s)...");
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
        Console.Error.WriteLine($"[SKILL] Audit: {totalOk} ok, {totalIssues} stale/missing, {noRefs} without refs");
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
            if (verbose) Console.Error.WriteLine($"[SKILL] {skill.Id}: no source_refs (nothing to verify)");
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

        // Regression script existence check
        if (!string.IsNullOrEmpty(skill.RegressionScript))
        {
            var scriptAbs = Path.IsPathRooted(skill.RegressionScript)
                ? skill.RegressionScript
                : Path.Combine(DataDir, skill.RegressionScript);
            if (!File.Exists(scriptAbs))
            {
                if (verbose) Console.WriteLine($"    ❌ REGRESSION SCRIPT MISSING: {skill.RegressionScript}");
                missing++;
            }
            else if (verbose)
                Console.Error.WriteLine($"[SKILL] {skill.Id}: regression script ✅ {Path.GetFileName(scriptAbs)}");
        }

        if (verbose && missing + stale == 0)
            Console.Error.WriteLine($"[SKILL] {skill.Id}: ✅ all {ok} ref(s) OK");
        return (ok, missing, stale);
    }

    // Runs the skill's regression script (if any). Returns 0=pass, 1=fail, -1=no script.
    static int RunSkillRegressionScript(SkillRecord skill)
    {
        if (string.IsNullOrEmpty(skill.RegressionScript)) return -1;
        var scriptAbs = Path.IsPathRooted(skill.RegressionScript)
            ? skill.RegressionScript
            : Path.Combine(DataDir, skill.RegressionScript);
        if (!File.Exists(scriptAbs))
        {
            Console.Error.WriteLine($"[SKILL] Regression script missing: {skill.RegressionScript}");
            return 1;
        }

        var ext = Path.GetExtension(scriptAbs).ToLowerInvariant();
        var (runner, runnerArgs) = ext switch
        {
            ".sh"  => (File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe")
                        ? @"C:\Program Files\Git\usr\bin\bash.exe" : "bash",
                       $"\"{scriptAbs}\""),
            ".ps1" => ("powershell.exe", $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptAbs}\""),
            ".cmd" or ".bat" => ("cmd.exe", $"/c \"{scriptAbs}\""),
            _ => (null as string, null as string)
        };
        if (runner == null)
        {
            Console.Error.WriteLine($"[SKILL] Unsupported regression script type: {ext}");
            return 1;
        }

        Console.Error.WriteLine($"[SKILL] Running regression: {Path.GetFileName(scriptAbs)}");
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = runner, Arguments = runnerArgs,
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true,
        };
        psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";
        var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "SKILL");
        var rOut = Task.Run(() => { string? l; while ((l = proc?.StandardOutput.ReadLine()) != null) Console.WriteLine($"  {l}"); });
        var rErr = Task.Run(() => { string? l; while ((l = proc?.StandardError.ReadLine()) != null) Console.Error.WriteLine($"  {l}"); });
        proc?.WaitForExit(60_000);
        rOut.Wait(3_000); rErr.Wait(1_000);
        var exit = proc?.ExitCode ?? 1;
        if (exit == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine($"[SKILL] Regression PASS");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[SKILL] Regression FAIL (exit={exit})");
            Console.ResetColor();
        }
        return exit == 0 ? 0 : 1;
    }

    // ── Helpers ──────────────────────────────────────────────────────────────────

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

    // Returns list of skill IDs with stale/missing source_refs. Used by Eye daily audit.
    internal static List<string> RunSkillAuditSilent(string cwd)
    {
        var issues = new List<string>();
        // temporarily override CallerCwd for dir resolution
        var prev = EyeCmdPipeServer.CallerCwd.Value;
        EyeCmdPipeServer.CallerCwd.Value = cwd;
        try
        {
            foreach (var skill in LoadAllSkills())
            {
                if (skill.SourceRefs == null || skill.SourceRefs.Count == 0) continue;
                var (_, missing, stale) = RunVerify(skill, verbose: false);
                if (missing + stale > 0) issues.Add(skill.Id);
            }
        }
        finally { EyeCmdPipeServer.CallerCwd.Value = prev; }
        return issues;
    }

    /// <summary>
    /// Print skills related to a command (+ optional subcommand/action) at end of help output.
    /// sub-terms from the actual args (e.g. "hack", "type", "contribute") narrow the results.
    /// </summary>
    internal static void PrintRelatedSkills(string command, string? sub = null, int limit = 4)
    {
        try
        {
            var cmd  = command.ToLowerInvariant();
            var terms = new[] { cmd }.Concat(
                sub != null ? [sub.ToLowerInvariant()] : Array.Empty<string>()
            ).ToArray();

            var scored = LoadAllSkills()
                .Select(s => (skill: s, score: SkillRelevanceScore(s, terms)))
                .Where(x => x.score >= 5)
                .OrderByDescending(x => x.score)
                .Take(limit)
                .ToList();

            if (scored.Count == 0) return;

            var label = sub != null ? $"'{command} {sub}'" : $"'{command}'";
            Console.WriteLine();
            Console.WriteLine($"Skills for {label}: (wkappbot skill show <id>)");
            foreach (var (s, score) in scored)
                Console.WriteLine($"  - {s.Title}  [{s.Id}] [{AudienceProfile(s)}]");
        }
        catch { /* never break usage output */ }
    }

    static int SkillRelevanceScore(SkillRecord s, string[] terms)
    {
        int score = 0;
        foreach (var term in terms)
        {
            // tag exact match is the strongest signal
            if (s.Tags?.Any(t => t.Equals(term, StringComparison.OrdinalIgnoreCase)) ?? false) score += 10;
            // id starting with term (e.g. "a11y-command-cheatsheet" for cmd="a11y")
            if (s.Id.StartsWith(term + "-", StringComparison.OrdinalIgnoreCase)) score += 6;
            if (s.Id.Contains(term, StringComparison.OrdinalIgnoreCase))          score += 3;
            if (s.Title.Contains(term, StringComparison.OrdinalIgnoreCase))       score += 2;
            if (s.Desc.Contains(term, StringComparison.OrdinalIgnoreCase))        score += 1;
        }
        // alias expansion for top-level command terms
        string primary = terms[0];
        score += primary switch {
            "a11y"    => (s.Tags?.Any(t => t is "uia" or "win32" or "automation") ?? false) ? 2 : 0,
            "ask"     => (s.Tags?.Any(t => t is "gpt" or "gemini" or "triad" or "delegation") ?? false) ? 2 : 0,
            "web"     => (s.Tags?.Any(t => t is "cdp" or "http" or "fetch") ?? false) ? 1 : 0,
            "eye"     => (s.Tags?.Any(t => t is "mcp" or "daemon" or "hotswap") ?? false) ? 2 : 0,
            "logcat"  => (s.Tags?.Any(t => t is "log" or "tail") ?? false) ? 1 : 0,
            "suggest" => (s.Tags?.Any(t => t is "workflow" or "bug" or "resolve") ?? false) ? 1 : 0,
            _         => 0,
        };
        return score;
    }

    static string FormatAge(DateTime dt)
    {
        var age = DateTime.UtcNow - dt.ToUniversalTime();
        if (age.TotalMinutes < 1) return "just now";
        if (age.TotalDays < 1)   return $"{(int)age.TotalHours}h ago";
        if (age.TotalDays < 30)  return $"{(int)age.TotalDays}d ago";
        if (age.TotalDays < 365) return $"{(int)(age.TotalDays / 30)}mo ago";
        return $"{(int)(age.TotalDays / 365)}y ago";
    }

    static string BumpVersion(string? v)
    {
        if (string.IsNullOrEmpty(v)) return "1.1";
        var parts = v.Split('.');
        if (parts.Length >= 2 && int.TryParse(parts[1], out int minor))
            return $"{parts[0]}.{minor + 1}";
        return v + ".1";
    }

    static bool IsSkillAudience(string value) =>
        SkillAudiences.Any(a => a.Equals(value, StringComparison.OrdinalIgnoreCase));

    static bool HasAudience(SkillRecord skill, string? audience) =>
        !string.IsNullOrWhiteSpace(audience) &&
        skill.Tags?.Any(t => t.Equals(audience, StringComparison.OrdinalIgnoreCase)) == true;

    static string AudienceLabel(SkillRecord skill)
    {
        var labels = skill.Tags?
            .Where(IsSkillAudience)
            .Select(t => t.ToLowerInvariant())
            .Distinct()
            .ToList() ?? [];

        return labels.Count switch
        {
            0 => "unclassified",
            1 => labels[0],
            _ => string.Join("/", labels)
        };
    }

    static string AudienceProfile(SkillRecord skill)
    {
        var labels = skill.Tags?
            .Where(IsSkillAudience)
            .Select(t => t.ToLowerInvariant())
            .Distinct()
            .ToList() ?? [];

        return labels.Count switch
        {
            0 => "unclassified",
            1 => $"{labels[0]} (pure)",
            _ => $"{string.Join("/", labels)} (mixed)"
        };
    }

    static int SkillUsage()
    {
        Console.WriteLine("Usage: wkappbot skill <command>");
        Console.WriteLine();
        Console.WriteLine("Storage: {callerCwd}/skills/  (project, git-tracked)");
        Console.WriteLine("         {hq}/skills/          (installed, [HQ] tag in list)");
        Console.WriteLine();
        Console.WriteLine("  list [app|audience]               List skills grouped by app or audience");
        Console.WriteLine("  show <id>                         Show skill details");
        Console.WriteLine("  search <keyword> [--app X] [--audience X]  Search by keyword in title/desc/tags");
        Console.WriteLine("  contribute --app X --title Y --desc Z");
        Console.WriteLine("             [--steps \"s1|s2\"] [--tags \"t1,t2\"] [--id <slug>]");
        Console.WriteLine("                                    Create or update skill in project dir");
        Console.WriteLine("  edit <id> [--title X] [--desc X] [--tags X] [--steps X]");
        Console.WriteLine("                                    Partial update — only specified fields changed");
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
        Console.WriteLine("  wkappbot skill list operator");
        Console.WriteLine("  wkappbot skill audit");
        Console.WriteLine("  wkappbot skill list developer");
        Console.WriteLine("  wkappbot skill list project");
        Console.WriteLine("  wkappbot skill list user");
        Console.WriteLine("  wkappbot skill verify chatgpt-blank-false-positive-guard");
        Console.WriteLine("  wkappbot skill search retry");
        Console.WriteLine("  wkappbot skill search retry --audience developer");
        Console.WriteLine("  wkappbot skill search retry --audience operator");
        Console.WriteLine("  wkappbot skill show cdp-eval-taskcancel-retry");
        Console.WriteLine("  wkappbot skill contribute --app wkappbot-webbot \\");
        Console.WriteLine("    --title \"CDP EvalAsync Retry\" --desc \"retry on cancel/timeout\" \\");
        Console.WriteLine("    --steps \"catch TaskCanceledException|500ms backoff\" --tags \"cdp,retry\" \\");
        Console.WriteLine("    --refs \"csharp/src/WKAppBot.WebBot/CdpClient.Actions.cs::TaskCanceledException\"");
        Console.WriteLine("  wkappbot skill install           # deploy to HQ after publish");
        Console.WriteLine("  wkappbot skill delete test-skill");
        PrintRelatedSkills("skill");
        return 0;
    }
}

// ── SkillSource / SkillRecord ─────────────────────────────────────────────

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
    [JsonPropertyName("source_refs")]        public List<SkillSourceRef>? SourceRefs      { get; set; }
    [JsonPropertyName("regression_script")]  public string? RegressionScript              { get; set; }
    [JsonPropertyName("author")]             public string? Author                        { get; set; }
    [JsonPropertyName("created")]            public DateTime Created                      { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("updated")]            public DateTime? Updated                     { get; set; }
    [JsonPropertyName("version")]            public string? Version                       { get; set; }

    /// <summary>Most recent activity date — updated if set, otherwise created.</summary>
    [JsonIgnore] public DateTime LastActivity => Updated ?? Created;

    [JsonIgnore] public SkillSource Source   { get; set; } = SkillSource.Project;
    /// <summary>Absolute path to the loaded .skill.json file (set by Load).</summary>
    [JsonIgnore] public string? FilePath     { get; set; }

    static readonly JsonSerializerOptions Opts = new() { WriteIndented = true };

    public void Save(string path) =>
        File.WriteAllText(path, JsonSerializer.Serialize(this, Opts));

    public static SkillRecord? Load(string path)
    {
        try
        {
            var s = JsonSerializer.Deserialize<SkillRecord>(File.ReadAllText(path));
            if (s != null) s.FilePath = path;
            return s;
        }
        catch { return null; }
    }
}
