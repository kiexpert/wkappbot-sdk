using System.Text.Json;

namespace WKAppBot.CLI;

// partial class: wkappbot skill <subcommand> -- executable automation skills
// Storage:
//   project skills (managed): {callerCwd}/skills/   <- contribute/delete here
//   hq skills (installed):    {DataDir}/skills/     <- copied via `skill install`
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
            "read"       => SkillShowCommand(args.Skip(1).ToArray()),
            "show"       => SkillShowCommand(args.Skip(1).ToArray()),
            "contribute" => SkillContributeCommand(args.Skip(1).ToArray()),
            "edit"       => SkillEditCommand(args.Skip(1).ToArray()),
            "delete"     => SkillDeleteCommand(args.Skip(1).ToArray()),
            "search"     => SkillSearchCommand(args.Skip(1).ToArray()),
            "news"       => SkillNewsCommand(args.Skip(1).ToArray()),
            "install"    => SkillInstallCommand(args.Skip(1).ToArray()),
            "export"     => SkillExportCommand(args.Skip(1).ToArray()),
            "import"     => SkillImportCommand(args.Skip(1).ToArray()),
            "verify"     => SkillVerifyCommand(args.Skip(1).ToArray()),
            "audit"      => SkillAuditCommand(args.Skip(1).ToArray()),
            "sync"       => SkillSyncCommand(args.Skip(1).ToArray()),
            "--help" or "-h" or "help" => SkillUsage(),
            var s => Error($"Unknown skill sub-command: {s}")
        };
    }

    // -- List --------------------------------------------------------------------─

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

    // -- Show --------------------------------------------------------------------─

    static int SkillShowCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill read <id> [--developer]");
            return 1;
        }

        bool explicitDev = args.Any(a => a == "--developer" || a == "--dev");
        var id = args.First(a => !a.StartsWith("--"));

        var skill = FindSkill(id);
        if (skill == null)
        {
            Console.Error.WriteLine($"[SKILL] Not found: {id}");
            Console.WriteLine("  Hint: wkappbot skill list  or  wkappbot skill search <keyword>");
            return 1;
        }

        // Project skills (loaded from CWD/skills/) are shown in full by default --
        // you're already in the right codebase context. HQ skills from other projects
        // show step titles only; full content requires --developer.
        bool developerMode = explicitDev || skill.Source == SkillSource.Project;
        bool hqForeignSkill = !explicitDev && skill.Source == SkillSource.Hq;

        Console.Error.WriteLine($"[SKILL] {skill.Title}");
        Console.WriteLine($"  ID     : {skill.Id}");
        Console.WriteLine($"  App    : {skill.App}");
        Console.WriteLine($"  Desc   : {skill.Desc}");
        Console.WriteLine($"  Audience: {AudienceProfile(skill)}");
        if (skill.Tags?.Count > 0)
            Console.WriteLine($"  Tags   : {string.Join(", ", skill.Tags)}");

        if (skill.Steps?.Count > 0)
        {
            if (hqForeignSkill)
            {
                // HQ skill from another project -- show step titles only (first sentence),
                // full content hidden behind --developer
                Console.WriteLine("  Steps  :");
                for (int i = 0; i < skill.Steps.Count; i++)
                {
                    var title = StepTitle(skill.Steps[i]);
                    var hasMore = skill.Steps[i].Length > title.Length;
                    Console.WriteLine($"    {i + 1}. {title}{(hasMore ? "  (developer only)" : "")}");
                }
            }
            else
            {
                // Project skill (full) or --developer: apply [DEV] filtering unless explicit dev
                var opSteps  = skill.Steps.Where(s => !IsDevStep(s)).ToList();
                var devSteps = skill.Steps.Where(s =>  IsDevStep(s)).ToList();

                if (explicitDev)
                {
                    Console.WriteLine("  Steps  :");
                    for (int i = 0; i < skill.Steps.Count; i++)
                        Console.WriteLine($"    {i + 1}. {skill.Steps[i]}");
                }
                else if (opSteps.Count == 0)
                {
                    Console.WriteLine($"  Steps  : (all {devSteps.Count} step(s) are [DEV] -- use --developer to show)");
                }
                else
                {
                    Console.WriteLine("  Steps  :");
                    for (int i = 0; i < opSteps.Count; i++)
                        Console.WriteLine($"    {i + 1}. {opSteps[i]}");
                    if (devSteps.Count > 0)
                        Console.WriteLine($"    (+{devSteps.Count} [DEV] step(s) hidden -- use --developer to show)");
                }
            }
        }

        if (!string.IsNullOrEmpty(skill.PrimaryCmd))
            Console.WriteLine($"  Primary: {skill.PrimaryCmd}");

        if (skill.Requirements?.Count > 0)
        {
            Console.WriteLine("  Require:");
            foreach (var r in skill.Requirements)
                Console.WriteLine($"    $ {r.Cmd}");
            Console.WriteLine("  Expect :");
            foreach (var r in skill.Requirements)
                Console.WriteLine($"    => {r.Expect}");
        }

        if (developerMode && skill.SourceRefs?.Count > 0)
        {
            Console.WriteLine("  Refs   :");
            foreach (var r in skill.SourceRefs)
            {
                var suffix = r.Line.HasValue ? $":{r.Line}" : "";
                var pat = !string.IsNullOrEmpty(r.Pattern) ? $" -> \"{r.Pattern}\"" : "";
                var note = !string.IsNullOrEmpty(r.Note) ? $" ({r.Note})" : "";
                Console.WriteLine($"    {r.File}{suffix}{pat}{note}");
            }
        }

        if (!string.IsNullOrEmpty(skill.Author))
            Console.WriteLine($"  Author : {skill.Author}");
        Console.WriteLine($"  Created: {skill.Created:yyyy-MM-dd}  Version: {skill.Version ?? "1.0"}");
        AppendSkillInvocationLog(skill.Id, skill.App);
        return 0;
    }

    static void AppendSkillInvocationLog(string skillId, string? app)
    {
        try
        {
            var path = Path.Combine(DataDir, "runtime", "skill_invocations.jsonl");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            var entry = JsonSerializer.Serialize(new
            {
                ts  = DateTime.UtcNow.ToString("o"),
                id  = skillId,
                app = app ?? "",
                cwd = Environment.CurrentDirectory
            });
            if (File.Exists(path) && new FileInfo(path).Length > 500_000)
                File.Move(path, path + ".1", overwrite: true);
            File.AppendAllText(path, entry + "\n", System.Text.Encoding.UTF8);
        }
        catch { }
    }

    /// <summary>
    /// Extract step title: text up to first sentence boundary (. ! ? or --) or 80 chars.
    /// Preserves bracket tags like [RULE], [DEV] etc. at the start.
    /// </summary>
    static string StepTitle(string step)
    {
        // Keep leading [TAG] if present, then cut at first sentence end
        var m = System.Text.RegularExpressions.Regex.Match(step, @"^(\[[A-Z:]+\]\s*)?(.{0,120})");
        var text = step;
        // Cut at sentence boundary: '. ' or '-- ' or end of 80 chars
        var cut = System.Text.RegularExpressions.Regex.Match(text, @"^.{10,}?(?:[.!?](?:\s|$)|(?:\s--\s))");
        if (cut.Success && cut.Value.Length < text.Length)
            return cut.Value.TrimEnd();
        // Fallback: hard cut at 80 chars with ellipsis
        return text.Length <= 80 ? text : text[..77] + "...";
    }

    // A step is developer-only if:
    // (1) explicitly tagged [DEV], OR
    // (2) auto-detected as implementation detail -- strong signals only:
    //     - Windows/Unix source path (C:\, D:\, csharp/, src/)
    //     - Source file reference with line number (Foo.cs:123)
    //     - Implementation note: "in Foo.cs", "see Foo.cs", "-> Foo.cs"
    //     - Multiple chained method calls indicating code walkthrough (Foo() -> Bar())
    // Intentionally NOT flagged: single method name mention in conceptual description
    // e.g. "stderrRelay.Wait(2000)" in a rule explanation stays as operator.
    static readonly System.Text.RegularExpressions.Regex _devStepPattern =
        new(@"
            \[DEV\]                                     # explicit tag
            | [A-Za-z]:\\                               # Windows absolute path (C:\, D:\)
            | \bcsharp/|\bsrc/                          # Unix-style source root path
            | \w+\.cs:\d+                               # Foo.cs:123 -- file+line ref
            | \b(in|see|->)\s+\w+\.cs\b                 # 'in Foo.cs' / 'see Foo.cs' / '-> Foo.cs'
            | \w+\(\)\s*->\s*\w+\(\)                    # Foo() -> Bar() -- chained calls = code walkthrough
        ", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace |
           System.Text.RegularExpressions.RegexOptions.IgnoreCase |
           System.Text.RegularExpressions.RegexOptions.Compiled);

    static bool IsDevStep(string step) => _devStepPattern.IsMatch(step);

    // -- Search ------------------------------------------------------------------─

    static int SkillSearchCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill search <keyword|/regex/|~regex~> [--app X] [--audience operator|developer|project|user]");
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

        // Build per-keyword matchers: /regex/, ~regex~, re:regex -> Regex + plain inner OR; else plain Contains.
        // For regex forms, inner plain string is also OR'd so /chrome/ matches "chrome" substring too.
        static bool KeywordMatches(string field, string kw, System.Text.RegularExpressions.Regex? rx, string? inner) =>
            rx != null
                ? rx.IsMatch(field) || (inner != null && field.Contains(inner, StringComparison.OrdinalIgnoreCase))
                : field.Contains(kw, StringComparison.OrdinalIgnoreCase);

        var matchers = keywords.Select(kw =>
        {
            // /pattern/ -- bash MSYS converts /x/ to Windows path; use ~x~ or re:x as alias
            var isSlash = kw.Length > 2 && kw.StartsWith('/') && kw.EndsWith('/');
            var isTilde = kw.Length > 2 && kw.StartsWith('~') && kw.EndsWith('~');
            var isRe    = kw.Length > 3 && kw.StartsWith("re:", StringComparison.OrdinalIgnoreCase);
            var rxPat   = isSlash || isTilde ? kw[1..^1] : isRe ? kw[3..] : null;
            if (rxPat != null)
            {
                try { return (kw, rx: new System.Text.RegularExpressions.Regex(rxPat, System.Text.RegularExpressions.RegexOptions.IgnoreCase), inner: rxPat); }
                catch { /* invalid regex -- treat as plain */ }
            }
            return (kw, rx: (System.Text.RegularExpressions.Regex?)null, inner: (string?)null);
        }).ToList();

        var hits = LoadAllSkills(appFilter)
            .Where(s => string.IsNullOrWhiteSpace(audienceFilter) || HasAudience(s, audienceFilter))
            .Where(s => matchers.All(m =>
                KeywordMatches(s.Id,    m.kw, m.rx, m.inner) ||
                KeywordMatches(s.Title, m.kw, m.rx, m.inner) ||
                KeywordMatches(s.Desc,  m.kw, m.rx, m.inner) ||
                (s.Tags?.Any(t  => KeywordMatches(t, m.kw, m.rx, m.inner)) ?? false) ||
                (s.Steps?.Any(t => KeywordMatches(t, m.kw, m.rx, m.inner)) ?? false)))
            .Select(s => (skill: s, score: SkillCoverageScore(s, matchers.Select(m =>
                // Score on inner plain string (strip delimiters); regex symbols treated as literals in glob path.
                m.inner ?? m.kw))))
            .OrderByDescending(x => x.score)
            .ToList();

        Console.Error.WriteLine($"[DBG] kws={string.Join(",", keywords)} hits={hits.Count}");
        if (hits.Count == 0)
        {
            Console.Error.WriteLine($"[SKILL] No results for: {string.Join(" ", keywords)}");
            return 0;
        }

        Console.Error.WriteLine($"[SKILL] {hits.Count} match(es):");
        foreach (var (s, score) in hits)
            Console.WriteLine($"  [{s.App}] {s.Id} [{AudienceProfile(s)}] cov={score:F2} - {s.Title}");
        Console.WriteLine($"\n  Use: wkappbot skill read <id>");
        return 0;
    }
}
