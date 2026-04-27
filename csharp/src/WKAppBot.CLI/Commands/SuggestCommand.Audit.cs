// SuggestCommand.Audit.cs -- `wkappbot suggest audit` health-check subcommand.
// Safeguard 3 against fake bulk-resolves: surface DataDir mismatches and basic
// store stats so Claude/operators can spot a split-brain (Eye writing to one
// path, suggest commands reading another) before any destructive operation.
//
// Exit code: 0 on OK, 1 on SPLIT-BRAIN detected. Other anomalies print
// warnings but don't block (audit is meant to be cheap to run).

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// suggest audit -- print suggestions store health summary.
    ///
    ///   * open suggest count (suggestions.jsonl line count)
    ///   * history count       (suggestions_history.jsonl line count, if present)
    ///   * backup file count   (suggestions.bak.*.jsonl in DataDir)
    ///   * DataDir path
    ///   * status              (OK / SPLIT-BRAIN / EMPTY-NEW-LEGACY-HAS-DATA)
    ///
    /// Split-brain check: legacy DataDir = <exe-dir>/wkappbot.hq. If both legacy
    /// and Program.DataDir exist with different line counts, that's a smoking
    /// gun -- exactly the situation the 2026-04-26 silent bulk-resolve incident
    /// produced. Exit 1 in that case so CI / Eye watchdogs can act.
    /// </summary>
    static int SuggestAuditCommand(string[] args)
    {
        if (args.Length > 0 && args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest audit");
            Console.WriteLine("  Health-check the suggestions store. Detects DataDir split-brain,");
            Console.WriteLine("  reports open/history/backup counts, and exits 1 on mismatch.");
            return 0;
        }

        var dataDir   = DataDir;
        var jsonlPath = Path.Combine(dataDir, "suggestions.jsonl");
        var historyPath = Path.Combine(dataDir, "suggestions_history.jsonl");

        int openCount = SafeNonEmptyLineCount(jsonlPath);
        int historyCount = SafeNonEmptyLineCount(historyPath);

        // Backup file count (suggestions.bak.*.jsonl)
        int bakCount = 0;
        try
        {
            if (Directory.Exists(dataDir))
                bakCount = Directory.EnumerateFiles(dataDir, "suggestions.bak.*.jsonl").Count();
        }
        catch { }

        // Legacy DataDir comparison (split-brain detector)
        var legacyDir = Path.Combine(
            Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".",
            "wkappbot.hq");
        var legacyJsonl = Path.Combine(legacyDir, "suggestions.jsonl");

        bool sameDir = false;
        try
        {
            sameDir = string.Equals(
                Path.GetFullPath(legacyDir),
                Path.GetFullPath(dataDir),
                StringComparison.OrdinalIgnoreCase);
        }
        catch { }

        bool splitBrain = false;
        int legacyCount = 0;
        if (!sameDir && File.Exists(legacyJsonl) && File.Exists(jsonlPath))
        {
            legacyCount = SafeNonEmptyLineCount(legacyJsonl);
            if (legacyCount != openCount)
            {
                splitBrain = true;
                Console.WriteLine($"[WARN:SPLIT-BRAIN] DataDir mismatch: {jsonlPath}={openCount} lines vs {legacyJsonl}={legacyCount} lines");
            }
        }
        else if (!sameDir && File.Exists(legacyJsonl) && !File.Exists(jsonlPath))
        {
            legacyCount = SafeNonEmptyLineCount(legacyJsonl);
            splitBrain = true;
            Console.WriteLine($"[WARN:SPLIT-BRAIN] new DataDir has no suggestions.jsonl but legacy {legacyJsonl}={legacyCount} lines exists. Run Eye once or `wkappbot suggest list` to migrate.");
        }

        // History sanity
        if (File.Exists(historyPath) && historyCount == 0)
            Console.WriteLine($"[WARN:HISTORY] {historyPath} exists but is empty -- expected resolved entries");

        var status = splitBrain ? "SPLIT-BRAIN" : "OK";
        Console.WriteLine($"[SUGGEST:AUDIT] open={openCount} history={historyCount} bak_files={bakCount} DataDir={dataDir} status={status}");

        return splitBrain ? 1 : 0;
    }

    /// <summary>
    /// Count non-empty lines in a JSONL file. Returns 0 if file missing or
    /// unreadable -- audit must never throw.
    /// </summary>
    private static int SafeNonEmptyLineCount(string path)
    {
        try
        {
            if (!File.Exists(path)) return 0;
            int n = 0;
            using var sr = new StreamReader(path);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line)) n++;
            }
            return n;
        }
        catch { return 0; }
    }
}
