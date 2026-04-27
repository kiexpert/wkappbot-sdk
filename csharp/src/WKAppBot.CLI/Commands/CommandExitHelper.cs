// CommandExitHelper.cs -- command history JSONL + skill hints (error + success + install news)
// Partial Program class so it can access LoadAllSkills, DataDir, SkillCoverageScore, mode flags.

using System.Text.Json;

namespace WKAppBot.CLI;

internal partial class Program
{
    static readonly JsonSerializerOptions _historyOpts = new() { WriteIndented = false };

    // Shared: extract meaningful tokens from argv (strips noise flags, unwraps grap JSON5 proc names)
    static List<string> ExtractTokens(string[] argv)
    {
        var noisy = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "--all", "--force", "--nth", "--timeout", "--help", "-h", "--dry-run",
              "--deep", "--process", "--condition", "--not", "--eval-js", "--speak" };
        return argv
            .Where(a => !noisy.Contains(a))
            .Select(a =>
            {
                if (a.StartsWith("--")) return a[2..];
                var m = System.Text.RegularExpressions.Regex.Match(a, @"proc:'([^']+)'");
                if (m.Success) return m.Groups[1].Value;
                return a;
            })
            .Where(a => a.Length >= 3 && !a.StartsWith("{") && !a.StartsWith("-"))
            .Select(a => a.ToLowerInvariant())
            .Distinct()
            .ToList();
    }

    // Append one JSONL line per user-facing invocation.
    // Pre-extracts tokens (toks field) so Trigger A reads skip re-extraction.
    // Skips Eye subprocess (WKAPPBOT_WORKER=1), grap, pipe modes to avoid noise.
    static void AppendCommandHistory(string[] argv, int exitCode)
    {
        if (RunningInEye || argv.Length == 0) return;
        if (GrapMode || IsPipeMode) return;
        try
        {
            var path = Path.Combine(DataDir, "runtime", "command_history.jsonl");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var toks = ExtractTokens(argv);
            var entry = JsonSerializer.Serialize(new
            {
                ts   = DateTime.UtcNow.ToString("o"),
                cwd  = Environment.CurrentDirectory,
                argv = argv,
                rc   = exitCode,
                toks = toks,
            }, _historyOpts);

            // Rotate at 1000 lines: overwrite .1 and start fresh
            if (File.Exists(path) && CountLines(path) >= 1000)
                File.Move(path, path + ".1", overwrite: true);

            File.AppendAllText(path, entry + "\n", System.Text.Encoding.UTF8);
        }
        catch { }
    }

    // Load deduplicated tokens from command_history.jsonl for last N days.
    // Reads pre-extracted toks field when available; falls back to argv re-extraction.
    internal static HashSet<string> LoadHistoryTokens(int days = 7)
    {
        var path = Path.Combine(DataDir, "runtime", "command_history.jsonl");
        if (!File.Exists(path)) return [];
        var cutoff = DateTime.UtcNow.AddDays(-days);
        var tokens = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var line in File.ReadLines(path, System.Text.Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                using var doc = JsonDocument.Parse(line);
                var root = doc.RootElement;
                if (root.TryGetProperty("ts", out var tsEl) &&
                    DateTime.TryParse(tsEl.GetString(), out var ts) && ts < cutoff) continue;
                if (root.TryGetProperty("toks", out var toksEl))
                {
                    foreach (var t in toksEl.EnumerateArray())
                        tokens.Add(t.GetString() ?? "");
                }
                else if (root.TryGetProperty("argv", out var argvEl))
                {
                    var argv = argvEl.EnumerateArray().Select(x => x.GetString() ?? "").ToArray();
                    foreach (var tok in ExtractTokens(argv)) tokens.Add(tok);
                }
            }
        }
        catch { }
        return tokens;
    }

    static int CountLines(string path)
    {
        int n = 0;
        using var sr = new StreamReader(path);
        while (sr.ReadLine() != null) n++;
        return n;
    }

    // -- Trigger B: on rc=0 exit, hint skills user hasn't seen in last 24h for this cwd ─

    static void PrintSkillHintOnSuccess(string[] argv, int exitCode)
    {
        if (exitCode != 0 || argv.Length == 0) return;
        if (GrapMode || IsPipeMode || RunningInEye) return;
        var cmd0 = argv[0].ToLowerInvariant();
        if (cmd0 is "eye" or "slack" or "help" or "mcp" or "suggest" or "skill") return;

        try
        {
            var tokens = ExtractTokens(argv);
            if (tokens.Count < 2) return;

            var cwd      = Environment.CurrentDirectory;
            var seenPath = Path.Combine(DataDir, "runtime", "skill_seen.jsonl");
            var seen     = LoadSeenSkillIds(seenPath, cwd);

            var hits = LoadAllSkills(null)
                .Select(s => (skill: s, score: SkillCoverageScore(s, tokens)))
                .Where(x => x.score >= 0.3f && !seen.Contains(x.skill.Id))
                .OrderByDescending(x => x.score)
                .Take(2)
                .ToList();

            if (hits.Count == 0) return;

            AppendSeenSkillIds(seenPath, cwd, hits.Select(x => x.skill.Id));

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Error.WriteLine($"\n[SKILL TIP] Skills for '{string.Join(" ", argv.Take(3))}':");
            foreach (var (s, score) in hits)
            {
                var hint = s.Steps?.Count > 0 ? s.Steps[0].Split('\n')[0].Trim() : s.Desc;
                if (hint.Length > 100) hint = hint[..100] + "...";
                Console.Error.WriteLine($"  wkappbot skill read {s.Id}  [cov={score:F2}]");
                Console.Error.WriteLine($"    {hint}");
            }
            Console.ResetColor();
        }
        catch { }
    }

    static HashSet<string> LoadSeenSkillIds(string path, string cwd)
    {
        var cutoff = DateTime.UtcNow.AddHours(-24);
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(path)) return seen;
        try
        {
            foreach (var line in File.ReadLines(path, System.Text.Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                using var doc = JsonDocument.Parse(line);
                var root = doc.RootElement;
                if (!root.TryGetProperty("ts", out var tsEl) ||
                    !DateTime.TryParse(tsEl.GetString(), out var ts) || ts < cutoff) continue;
                if (!root.TryGetProperty("cwd", out var cwdEl) ||
                    !string.Equals(cwdEl.GetString(), cwd, StringComparison.OrdinalIgnoreCase)) continue;
                if (root.TryGetProperty("id", out var idEl))
                    seen.Add(idEl.GetString() ?? "");
            }
        }
        catch { }
        return seen;
    }

    static void AppendSeenSkillIds(string path, string cwd, IEnumerable<string> ids)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            var ts = DateTime.UtcNow.ToString("o");
            var sb = new System.Text.StringBuilder();
            foreach (var id in ids)
                sb.Append(JsonSerializer.Serialize(new { id, cwd, ts }, _historyOpts) + "\n");
            if (File.Exists(path) && CountLines(path) >= 500)
                File.Move(path, path + ".1", overwrite: true);
            File.AppendAllText(path, sb.ToString(), System.Text.Encoding.UTF8);
        }
        catch { }
    }

    static List<string> LoadRecentPendingSuggestionTokens(int maxEntries = 5)
    {
        var path = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(path)) return [];

        var recent = new List<(DateTime ts, List<string> tokens)>();
        try
        {
            foreach (var line in File.ReadLines(path, System.Text.Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                using var doc = JsonDocument.Parse(line);
                var root = doc.RootElement;

                if (root.TryGetProperty("review_status", out var reviewStatusEl) &&
                    string.Equals(reviewStatusEl.GetString(), "done", StringComparison.OrdinalIgnoreCase))
                    continue;
                if (root.TryGetProperty("status", out var statusEl) &&
                    string.Equals(statusEl.GetString(), "done", StringComparison.OrdinalIgnoreCase))
                    continue;
                var fields = new List<string>();
                if (root.TryGetProperty("title", out var titleEl))
                    fields.Add(titleEl.GetString() ?? "");
                if (root.TryGetProperty("text", out var textEl))
                    fields.Add(textEl.GetString() ?? "");

                if (root.TryGetProperty("type", out var typeEl) &&
                    string.Equals(typeEl.GetString(), "merge", StringComparison.OrdinalIgnoreCase))
                {
                    if (root.TryGetProperty("rootCause", out var rootCauseEl))
                        fields.Add(rootCauseEl.GetString() ?? "");
                    if (root.TryGetProperty("work", out var workEl))
                        fields.Add(workEl.GetString() ?? "");
                    if (root.TryGetProperty("errorPattern", out var errorPatternEl))
                        fields.Add(errorPatternEl.GetString() ?? "");
                    if (root.TryGetProperty("autoHealScript", out var autoHealScriptEl))
                        fields.Add(autoHealScriptEl.GetString() ?? "");
                    if (root.TryGetProperty("components", out var componentsEl) && componentsEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in componentsEl.EnumerateArray())
                            fields.Add(item.GetString() ?? "");
                    }
                    if (root.TryGetProperty("affectedCommands", out var affectedCommandsEl) && affectedCommandsEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in affectedCommandsEl.EnumerateArray())
                            fields.Add(item.GetString() ?? "");
                    }
                }

                var tokens = ExtractNewsTokens(fields);
                if (tokens.Count == 0) continue;

                var ts = DateTime.MinValue;
                if (root.TryGetProperty("ts", out var tsEl) &&
                    DateTime.TryParse(tsEl.GetString(), out var parsedTs))
                    ts = parsedTs;

                recent.Add((ts, tokens));
            }
        }
        catch { }

        return recent
            .OrderByDescending(x => x.ts)
            .Take(maxEntries)
            .SelectMany(x => x.tokens)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    static List<string> ExtractNewsTokens(IEnumerable<string> fields)
    {
        var tokens = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var field in fields)
        {
            if (string.IsNullOrWhiteSpace(field)) continue;
            foreach (System.Text.RegularExpressions.Match m in
                     System.Text.RegularExpressions.Regex.Matches(field, @"[\p{L}\p{Nd}][\p{L}\p{Nd}_/-]{2,}"))
            {
                var tok = m.Value.Trim().ToLowerInvariant();
                if (tok.Length >= 3) tokens.Add(tok);
            }
        }
        return tokens.ToList();
    }

    // -- Trigger A: called from SkillInstallCommand after copying new/updated skills ─

    // Single exit hook for skill-news emission.
    // Main() should only call this one function at end-of-command.
    static void AppBotExit(int exitCode, string[] argv)
    {
        try
        {
            if (argv.Length == 0) return;
            if (GrapMode || IsPipeMode || RunningInEye) return;

            var cmd0 = argv[0].ToLowerInvariant();
            if (cmd0 is "eye" or "slack" or "help" or "mcp" or "suggest" or "skill") return;

            var newsTokens = LoadRecentPendingSuggestionTokens(maxEntries: 3);
            if (newsTokens.Count == 0) return;

            var hits = LoadAllSkills(null)
                .Select(s => (skill: s, score: SkillCoverageScore(s, newsTokens)))
                .Where(x => x.score >= 0.35f)
                .OrderByDescending(x => x.score)
                .ThenByDescending(x => x.skill.LastActivity)
                .Take(2)
                .ToList();

            if (hits.Count == 0) return;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine("\n[SKILL NEWS] Relevant skills for your recent pending items:");
            foreach (var (s, score) in hits)
            {
                var hint = s.Steps?.Count > 0 ? s.Steps[0].Split('\n')[0].Trim() : s.Desc;
                if (hint.Length > 100) hint = hint[..100] + "...";
                Console.Error.WriteLine($"  - wkappbot skill read {s.Id}  [cov={score:F2}]");
                Console.Error.WriteLine($"    {hint}");
            }
            Console.ResetColor();
        }
        catch { }
    }

    internal static void NotifyNewSkillsMatchingHistory(List<SkillRecord> installed)
    {
        if (GrapMode || IsPipeMode || RunningInEye) return;
        if (installed.Count == 0) return;
        try
        {
            var lastCheck = LoadSkillNewsTsFile();
            var fresh = installed.Where(s => s.LastActivity > lastCheck).ToList();
            SaveSkillNewsTsFile(); // always advance checkpoint
            if (fresh.Count == 0) return;

            var histTokens = LoadHistoryTokens(7);
            if (histTokens.Count == 0) return;

            var hits = fresh
                .Select(s => (skill: s, score: SkillCoverageScore(s, histTokens)))
                .Where(x => x.score >= 0.3f)
                .OrderByDescending(x => x.score)
                .Take(3)
                .ToList();

            if (hits.Count == 0) return;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine($"\n[SKILL NEWS] {hits.Count} new/updated skill(s) match your recent commands:");
            foreach (var (s, score) in hits)
            {
                var hint = s.Steps?.Count > 0 ? s.Steps[0].Split('\n')[0].Trim() : s.Desc;
                if (hint.Length > 100) hint = hint[..100] + "...";
                Console.Error.WriteLine($"  wkappbot skill read {s.Id}  [cov={score:F2}]");
                Console.Error.WriteLine($"    {s.Title}");
                Console.Error.WriteLine($"    {hint}");
            }
            Console.ResetColor();
        }
        catch { }
    }

    static DateTime LoadSkillNewsTsFile()
    {
        var path = Path.Combine(DataDir, "runtime", "skill_news_ts");
        try { return File.Exists(path) ? DateTime.Parse(File.ReadAllText(path).Trim()) : DateTime.MinValue; }
        catch { return DateTime.MinValue; }
    }

    static void SaveSkillNewsTsFile()
    {
        var path = Path.Combine(DataDir, "runtime", "skill_news_ts");
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, DateTime.UtcNow.ToString("o"));
        }
        catch { }
    }

    // -- Trigger: on error exit ─

    static void PrintSkillHintOnError(string[] argv, int exitCode)
    {
        if (exitCode == 0 || argv.Length == 0) return;
        if (GrapMode || IsPipeMode || RunningInEye) return;
        var cmd0 = argv[0].ToLowerInvariant();
        if (cmd0 is "eye" or "slack" or "help" or "mcp" or "suggest" or "skill") return;

        try
        {
            var tokens = ExtractTokens(argv);
            if (tokens.Count < 2) return;

            var hits = LoadAllSkills(null)
                .Select(s => (skill: s, score: SkillCoverageScore(s, tokens)))
                .Where(x => x.score >= 0.3f)
                .OrderByDescending(x => x.score)
                .Take(3)
                .ToList();

            if (hits.Count == 0) return;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Error.WriteLine($"\n[SKILL HINT] Related skills (tokens: {string.Join(", ", tokens.Take(5))}):");
            foreach (var (s, score) in hits)
            {
                var excerpt = s.Steps?.Count > 0 ? s.Steps[0].Split('\n')[0].Trim() : s.Desc;
                if (excerpt.Length > 80) excerpt = excerpt[..80] + "...";
                Console.Error.WriteLine($"  wkappbot skill read {s.Id}  [cov={score:F2}]");
                Console.Error.WriteLine($"    {s.Title}");
                Console.Error.WriteLine($"    {excerpt}");
            }
            Console.ResetColor();
        }
        catch { }
    }
}
