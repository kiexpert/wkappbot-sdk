using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

// partial class: skill helpers -- loaders, scoring, news, audience, usage
internal partial class Program
{
    // -- Helpers ------------------------------------------------------------------

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
        // Strip non-ASCII (CJK etc.) -- caller must use --id for Korean-only titles
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
            Console.WriteLine($"Skills for {label}: (wkappbot skill read <id>)");
            foreach (var (s, score) in scored)
                Console.WriteLine($"  - {s.Title}  [{s.Id}] [{AudienceProfile(s)}]");
        }
        catch { /* never break usage output */ }
    }

    // -- Coverage scoring (shared) -----------------------------------------------─

    /// <summary>
    /// GlobCoverageScore-based ranking: Σ(segment_len × occurrence_count) / field_len per field.
    /// Tokens prefixed with "re:" are scored by literal-char-count (symbols penalized).
    /// Multiple hits of same token in a field boost score proportionally.
    /// Used by skill search, skill news, and PrintSkillHintOnError.
    /// </summary>
    internal static float SkillCoverageScore(SkillRecord s, IEnumerable<string> tokens)
    {
        float score = 0f;
        foreach (var tok in tokens)
        {
            if (s.Id.Length > 0)    score += (float)WKAppBot.Win32.Window.WindowFinder.GlobCoverageScore(tok, s.Id);
            if (s.Title.Length > 0) score += (float)WKAppBot.Win32.Window.WindowFinder.GlobCoverageScore(tok, s.Title);
            if (s.Desc.Length > 0)  score += (float)WKAppBot.Win32.Window.WindowFinder.GlobCoverageScore(tok, s.Desc);
            if (s.Tags?.Count > 0)
            {
                // Best tag match (not sum — tags are short, summing inflates score for many-tagged skills)
                var best = s.Tags.Max(t => t.Length > 0
                    ? WKAppBot.Win32.Window.WindowFinder.GlobCoverageScore(tok, t) : 0.0);
                score += (float)best;
            }
        }
        return score;
    }

    // -- News --------------------------------------------------------------------─

    static int SkillNewsCommand(string[] args)
    {
        int days = 7;
        var queryTokens = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--days" && i + 1 < args.Length && int.TryParse(args[++i], out var d))
                days = d;
            else if (!args[i].StartsWith("--"))
                queryTokens.Add(args[i].ToLowerInvariant());
        }

        var since = DateTime.UtcNow.AddDays(-days);
        var recent = LoadAllSkills()
            .Where(s => s.LastActivity >= since)
            .ToList();

        if (recent.Count == 0)
        {
            Console.Error.WriteLine($"[SKILL NEWS] No skills updated in the last {days} day(s).");
            return 0;
        }

        IEnumerable<(SkillRecord skill, float score, bool isNew)> results = recent
            .Select(s => (skill: s, score: queryTokens.Count > 0 ? SkillCoverageScore(s, queryTokens) : 0f, isNew: s.Updated == null));

        results = queryTokens.Count > 0
            ? results.OrderByDescending(x => x.score).ThenByDescending(x => x.skill.LastActivity)
            : results.OrderByDescending(x => x.skill.LastActivity);

        var list = results.ToList();
        var header = queryTokens.Count > 0
            ? $"[SKILL NEWS] {list.Count} skill(s) in last {days}d matching '{string.Join(" ", queryTokens)}':"
            : $"[SKILL NEWS] {list.Count} skill(s) updated in last {days} day(s):";
        Console.Error.WriteLine(header);

        foreach (var (s, score, isNew) in list)
        {
            var tag   = isNew ? "NEW" : "UPD";
            var age   = FormatAge(s.LastActivity);
            var ver   = s.Version != null ? $" v{s.Version}" : "";
            var cov   = queryTokens.Count > 0 ? $" cov={score:F2}" : "";
            Console.WriteLine($"  [{tag}] {s.Id}{ver} ({age}){cov} - {s.Title}");
        }
        Console.WriteLine($"\n  Use: wkappbot skill read <id>");
        return 0;
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
        Console.WriteLine("  read <id>                         Show skill details");
        Console.WriteLine("  search <keyword> [--app X] [--audience X]  Search by keyword in title/desc/tags (coverage ranked)");
        Console.WriteLine("  news [query] [--days N]           Show recently added/updated skills (default: last 7 days)");
        Console.WriteLine("  contribute --app X --title Y --desc Z");
        Console.WriteLine("             [--steps \"s1|s2\"] [--tags \"t1,t2\"] [--id <slug>]");
        Console.WriteLine("                                    Create or update skill in project dir");
        Console.WriteLine("  edit <id> [--title X] [--desc X] [--tags X] [--steps X]");
        Console.WriteLine("                                    Partial update -- only specified fields changed");
        Console.WriteLine("  delete <id>                       Remove skill from project dir");
        Console.WriteLine("  install [--app X] [--force]       Copy project skills -> HQ (on deploy)");
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
        Console.WriteLine("  wkappbot skill read cdp-eval-taskcancel-retry");
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
