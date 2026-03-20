using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

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
        bool sweep = false;   // default = dry-run (safe preview)

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--days" && i + 1 < args.Length) { int.TryParse(args[++i], out overrideDays); }
            else if (args[i] is "--sweep" or "-s") sweep = true;
            else if (args[i] is "-h" or "--help" or "help") { GcUsage(); return 0; }
            else if (!args[i].StartsWith("-")) target = args[i].ToLowerInvariant();
        }
        bool dryRun = !sweep;

        // Known retention overrides (days) for specific folder names
        var retentionDays = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["logs"]       = 7,
            ["temp"]       = 14,
            ["whisper"]    = 14,
            ["triad"]      = 30,
            ["output"]     = 30,
            ["experience"] = 90,
        };
        const int DefaultRetentionDays = 30;

        // Scan all subdirectories under HQ + LocalAppData/WKAppBot
        var roots = new List<string> { DataDir };
        var localAppData = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WKAppBot");
        if (Directory.Exists(localAppData)) roots.Add(localAppData);

        int totalDeleted = 0;
        long totalBytes = 0;

        foreach (var root in roots)
        {
            if (!Directory.Exists(root)) continue;

            // Get immediate subdirectories
            string[] subDirs;
            try { subDirs = Directory.GetDirectories(root); }
            catch { continue; }

            foreach (var dir in subDirs)
            {
                var folderName = Path.GetFileName(dir);

                // Filter by grap pattern if target specified
                // "exp" → "*exp*" (auto-wrap for partial match)
                if (!string.IsNullOrEmpty(target) && target != "all")
                {
                    var pattern = target.Contains('*') || target.Contains('?')
                        ? target : $"*{target}*";
                    var rxPattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
                    if (!Regex.IsMatch(folderName, rxPattern, RegexOptions.IgnoreCase))
                        continue;
                }

                // Determine retention
                int days = overrideDays > 0 ? overrideDays
                    : retentionDays.TryGetValue(folderName, out int known) ? known
                    : DefaultRetentionDays;
                var cutoff = DateTime.Now.AddDays(-days);

                var (deleted, bytes) = PurgeOldFiles(dir, cutoff, dryRun);
                if (deleted > 0)
                {
                    var action = dryRun ? "→ recycle" : "recycled";
                    Console.WriteLine($"[GC] {folderName}/: {action} {deleted} file(s), {bytes / 1024.0:F0}KB (>{days}d)");
                }
                totalDeleted += deleted;
                totalBytes += bytes;

                if (!dryRun)
                    PurgeEmptyDirs(dir);
            }

            // Also scan loose files in root (not in subdirectories)
            if (string.IsNullOrEmpty(target) || target == "all")
            {
                try
                {
                    var cutoff = DateTime.Now.AddDays(-(overrideDays > 0 ? overrideDays : DefaultRetentionDays));
                    foreach (var file in Directory.EnumerateFiles(root))
                    {
                        var fi = new FileInfo(file);
                        var lastUsed = new[] { fi.CreationTime, fi.LastWriteTime, fi.LastAccessTime }.Max();
                        if (lastUsed < cutoff)
                        {
                            totalBytes += fi.Length;
                            totalDeleted++;
                            if (!dryRun)
                                FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            Console.WriteLine($"[GC] {fi.Name}: {(dryRun ? "→ recycle" : "recycled")} {fi.Length / 1024.0:F0}KB");
                        }
                    }
                }
                catch { }
            }
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
            foreach (var file in Directory.EnumerateFiles(dir, "*", System.IO.SearchOption.AllDirectories))
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
                            FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
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
            foreach (var dir in Directory.EnumerateDirectories(root, "*", System.IO.SearchOption.AllDirectories)
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
wkappbot gc [pattern] [--days N] [--sweep]

Preview (dry-run) old files under wkappbot.hq/ for recycling.
Add --sweep to actually move files to Recycle Bin.
Empty folders are automatically deleted after sweep.

Pattern: partial match on HQ subdirectory names (auto-wrapped with *)
  gc              All HQ subdirs (dry-run)
  gc logs         logs/ only
  gc exp          *exp* → experience/, kiwoom_exp/, com_exp/
  gc tri*         triad/
  gc all          Everything

Retention (days, per folder — override with --days N):
  logs=7  temp=14  whisper=14  triad=30  output=30  experience=90  others=30

Options:
  --sweep    Actually move to Recycle Bin (default: dry-run preview)
  --days N   Override retention days
");
    }
}
