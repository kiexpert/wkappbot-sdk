namespace WKAppBot.CLI;

// partial class: wkappbot skill contribute -- create/update skill in project dir
internal partial class Program
{
    // -- Contribute --------------------------------------------------------------─

    static int SkillContributeCommand(string[] args)
    {
        string? app = null, title = null, desc = null, author = null, id = null, regressionScript = null;
        var steps = new List<string>();
        var tags = new List<string>();
        var refs = new List<SkillSourceRef>();
        var requirements = new List<SkillRequirement>();

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
                case "--requirement"       when i + 1 < args.Length:
                {
                    var raw = args[++i];
                    var sep = raw.IndexOf(" => ", StringComparison.Ordinal);
                    if (sep < 0) { Console.Error.WriteLine($"[SKILL] --requirement must use ' => ' separator: \"{raw}\""); return 1; }
                    requirements.Add(new SkillRequirement { Cmd = raw[..sep].Trim(), Expect = raw[(sep + 4)..].Trim() });
                    break;
                }
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

        // Encoding risk check: reject Korean/emoji in skill content.
        // Korean-UI app categories (hts-*, heroes*, kiwoom*, invest-kr, ...)
        // get a relaxed threshold inside HasEncodingRisk because documenting
        // real UIA labels verbatim is the skill's job.
        var allContent = string.Join(" ", new[] { title, desc }.Concat(steps).Where(s => s != null)!);
        if (!force && HasEncodingRisk(allContent, "skill contribute", app)) return 1;

        // Requirement guard: at least 3 behavioral contracts required.
        var slug0 = id ?? Slugify(title ?? "");
        var existingPath0 = Path.Combine(ProjectSkillsDir, app ?? "", $"{slug0}.skill.json");
        var existingReqs = File.Exists(existingPath0) ? SkillRecord.Load(existingPath0)?.Requirements : null;
        var totalReqs = requirements.Count + (existingReqs?.Count ?? 0);
        if (totalReqs < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SKILL] FAILED: requirements total {totalReqs}/3 (new={requirements.Count}, existing={existingReqs?.Count ?? 0}).");
            Console.WriteLine("  Provide at least 3 behavioral contracts:");
            Console.WriteLine("  --requirement \"wkappbot <cmd> <args> => <expected output pattern>\"");
            Console.ResetColor();
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
            Steps            = steps.Count        > 0 ? steps        : existing?.Steps        ?? [],
            Requirements     = requirements.Count > 0 ? requirements : existing?.Requirements,
            Tags             = tags.Count         > 0 ? tags         : existing?.Tags         ?? [],
            SourceRefs       = refs.Count         > 0 ? refs         : existing?.SourceRefs,
            RegressionScript = regScriptRef ?? existing?.RegressionScript,
            Author           = author ?? existing?.Author ?? "claude",
            Created          = existing?.Created ?? DateTime.UtcNow,
            Updated          = DateTime.UtcNow,
            Version          = existing != null ? BumpVersion(existing.Version) : "1.0"
        };

        skill.Save(path);
        var action = existing != null ? "Updated" : "Created";
        // User-visible confirmation MUST go to stdout. IocpPipeRelay buffers
        // stderr and drops the buffer on exit=0 by default, so Console.Error
        // here vanishes for non-interactive callers (bash, MCP, agents) --
        // which makes the command look like it timed out with no output even
        // though the skill was written to disk. See skill
        // 'iocp-stderr-default-buffered' for the full architecture note.
        Console.WriteLine($"[SKILL] {action}: [{app}] {title} (id={slug}, v{skill.Version})");
        if (regScriptRef != null)
            Console.WriteLine($"[SKILL] Regression script: {regScriptRef} (auto-runs on suggest resolve)");
        // Skill contributed/updated = made public -- immediately sync to peer repos' HQ installs.
        try { var (n, _, _) = SyncSkillsFromPeerRepos(); if (n > 0) Console.Error.WriteLine($"[SKILL:SYNC] +{n} from peers"); }
        catch { }
        return 0;
    }

    // Copies regression script into experience/tests/{cmd}/{subcmd}/ and returns relative path from DataDir.
    // Filename convention: test-{cmd}-{subcmd}[-desc].sh  ->  cmd + subcmd extracted from filename.
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
        Console.Error.WriteLine($"[SKILL] Regression -> experience/tests/{cmd}/{subcmd}/{destName}");
        return relPath;
    }
}
