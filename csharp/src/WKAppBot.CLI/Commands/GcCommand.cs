namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot gc [target] [--days N] [--dry-run]
    /// Purge old files under wkappbot.hq/ — empty folders deleted automatically.
    /// </summary>
    static int GcCommand(string[] args)
    {
        string? target = null;
        int overrideDays = 0; // 0 = use per-target defaults
        bool dryRun = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--days" && i + 1 < args.Length) { int.TryParse(args[++i], out overrideDays); }
            else if (args[i] is "--dry-run" or "-n") dryRun = true;
            else if (args[i] is "-h" or "--help" or "help") { GcUsage(); return 0; }
            else if (!args[i].StartsWith("-")) target = args[i].ToLowerInvariant();
        }

        // Per-target retention days (conservative defaults)
        var targets = new (string name, string subDir, int defaultDays, string desc)[]
        {
            ("logs",        "logs",             7,  "log files"),
            ("whisper",     "whisper",         14,  "whisper mp3/wav recordings"),
            ("cache",       "",                30,  "gap_cache + gap_screenshots"),  // LocalAppData
            ("triad",       "triad",           30,  "triad session files"),
            ("output",      "output",          30,  "command output files"),
            ("temp",        "temp",            14,  "temporary files"),
            ("experience",  "experience",      90,  "experience DB (knowhow, cca_params, dyn_a11y)"),
        };

        int totalDeleted = 0;
        long totalBytes = 0;

        foreach (var (name, subDir, defaultDays, desc) in targets)
        {
            if (target != null && target != "all" && target != name) continue;

            int days = overrideDays > 0 ? overrideDays : defaultDays;
            var cutoff = DateTime.Now.AddDays(-days);

            // Determine root directory
            string rootDir;
            if (name == "cache")
            {
                rootDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "WKAppBot");
            }
            else
            {
                rootDir = Path.Combine(DataDir, subDir);
            }

            if (!Directory.Exists(rootDir)) continue;

            var (deleted, bytes) = PurgeOldFiles(rootDir, cutoff, dryRun);
            if (deleted > 0 || dryRun)
            {
                var action = dryRun ? "would delete" : "deleted";
                Console.WriteLine($"[GC] {name}: {action} {deleted} file(s), {bytes / 1024.0:F0}KB ({desc}, >{days}d)");
            }
            totalDeleted += deleted;
            totalBytes += bytes;

            // Clean empty folders (bottom-up)
            if (!dryRun)
                PurgeEmptyDirs(rootDir);
        }

        if (totalDeleted == 0 && !dryRun)
            Console.WriteLine("[GC] Nothing to clean — all files within retention period");
        else
        {
            var action = dryRun ? "would free" : "freed";
            Console.WriteLine($"[GC] Total: {totalDeleted} file(s), {action} {totalBytes / 1024.0:F0}KB");
        }

        return 0;
    }

    static (int deleted, long bytes) PurgeOldFiles(string dir, DateTime cutoff, bool dryRun)
    {
        int deleted = 0;
        long bytes = 0;

        try
        {
            foreach (var file in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
            {
                try
                {
                    var fi = new FileInfo(file);
                    // Use the LATEST of: creation, write, access — most generous retention
                    var lastUsed = new[] { fi.CreationTime, fi.LastWriteTime, fi.LastAccessTime }.Max();
                    if (lastUsed < cutoff)
                    {
                        bytes += fi.Length;
                        deleted++;
                        if (!dryRun)
                            fi.Delete();
                    }
                }
                catch { /* skip locked/protected files */ }
            }
        }
        catch { /* directory access error */ }

        return (deleted, bytes);
    }

    static void PurgeEmptyDirs(string root)
    {
        try
        {
            // Bottom-up: deepest first
            foreach (var dir in Directory.EnumerateDirectories(root, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Length))
            {
                try
                {
                    if (!Directory.EnumerateFileSystemEntries(dir).Any())
                        Directory.Delete(dir);
                }
                catch { }
            }
        }
        catch { }
    }

    static void GcUsage()
    {
        Console.WriteLine(@"
wkappbot gc [target] [--days N] [--dry-run]

Purge old files under wkappbot.hq/ and related caches.
Empty folders are automatically deleted after file cleanup.

Targets (default: all):
  all          All targets below
  logs         Log files (default: 7 days)
  whisper      Whisper recordings mp3/wav (default: 14 days)
  cache        Gap cache + screenshots in LocalAppData (default: 30 days)
  triad        Triad session files (default: 30 days)
  output       Command output files (default: 30 days)
  temp         Temporary files (default: 14 days)
  experience   Experience DB — knowhow, cca_params, dyn_a11y (default: 90 days)

Options:
  --days N     Override retention days for selected target(s)
  --dry-run    Show what would be deleted without deleting
");
    }
}
