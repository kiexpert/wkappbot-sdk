// CommandExitHelper.cs -- command history JSONL + skill hint on error
// Partial Program class so it can access LoadAllSkills, DataDir, mode flags.

using System.Text.Json;

namespace WKAppBot.CLI;

internal partial class Program
{
    static readonly JsonSerializerOptions _historyOpts = new() { WriteIndented = false };

    // Append one JSONL line per user-facing invocation.
    // Enables: skill-match notification (#68), project-scoped history (#70).
    // Skips Eye subprocess invocations (WKAPPBOT_WORKER=1) to avoid noise.
    static void AppendCommandHistory(string[] argv, int exitCode)
    {
        if (RunningInEye || argv.Length == 0) return;
        if (GrapMode || IsPipeMode) return;
        try
        {
            var path = Path.Combine(DataDir, "runtime", "command_history.jsonl");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var entry = JsonSerializer.Serialize(new
            {
                ts   = DateTime.UtcNow.ToString("o"),
                cwd  = Environment.CurrentDirectory,
                argv = argv,
                rc   = exitCode,
            }, _historyOpts);

            // Rotate at 1000 lines: overwrite .1 and start fresh
            if (File.Exists(path) && CountLines(path) >= 1000)
                File.Move(path, path + ".1", overwrite: true);

            File.AppendAllText(path, entry + "\n", System.Text.Encoding.UTF8);
        }
        catch { }
    }

    static int CountLines(string path)
    {
        int n = 0;
        using var sr = new StreamReader(path);
        while (sr.ReadLine() != null) n++;
        return n;
    }

    // On non-zero exit, OR-search installed skills by argv tokens.
    // Prints up to 3 skills with >= 2 matching tokens as contextual hints.
    static void PrintSkillHintOnError(string[] argv, int exitCode)
    {
        if (exitCode == 0 || argv.Length == 0) return;
        if (GrapMode || IsPipeMode || RunningInEye) return;
        // Skip internal/daemon commands where hints add no value
        var cmd0 = argv[0].ToLowerInvariant();
        if (cmd0 is "eye" or "slack" or "help" or "mcp" or "suggest" or "skill") return;

        try
        {
            // Extract meaningful tokens: strip common noise flags, pull proc name from grap JSON5
            var noisy = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                { "--all", "--force", "--nth", "--timeout", "--help", "-h", "--dry-run",
                  "--deep", "--process", "--condition", "--not", "--eval-js", "--speak" };

            var tokens = argv
                .Where(a => !noisy.Contains(a))
                .Select(a =>
                {
                    if (a.StartsWith("--")) return a[2..]; // strip flag prefix
                    var m = System.Text.RegularExpressions.Regex.Match(a, @"proc:'([^']+)'");
                    if (m.Success) return m.Groups[1].Value; // {proc:'name'} -> name
                    return a;
                })
                .Where(a => a.Length >= 3 && !a.StartsWith("{") && !a.StartsWith("-"))
                .Select(a => a.ToLowerInvariant())
                .Distinct()
                .ToList();

            if (tokens.Count < 2) return;

            var hits = LoadAllSkills(null)
                .Select(s =>
                {
                    int score = tokens.Count(tok =>
                        s.Id.Contains(tok, StringComparison.OrdinalIgnoreCase) ||
                        s.Title.Contains(tok, StringComparison.OrdinalIgnoreCase) ||
                        s.Desc.Contains(tok, StringComparison.OrdinalIgnoreCase) ||
                        (s.Tags?.Any(t => t.Contains(tok, StringComparison.OrdinalIgnoreCase)) ?? false));
                    return (skill: s, score);
                })
                .Where(x => x.score >= 2)
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
                Console.Error.WriteLine($"  wkappbot skill read {s.Id}  [{score} matches]");
                Console.Error.WriteLine($"    {s.Title}");
                Console.Error.WriteLine($"    {excerpt}");
            }
            Console.ResetColor();
        }
        catch { }
    }
}
