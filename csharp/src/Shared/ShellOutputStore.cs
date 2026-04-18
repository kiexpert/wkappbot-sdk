using System.IO;
using System.Text.Json;

namespace WKAppBot.Shared;

/// <summary>
/// One shell command execution captured to disk with a stable numeric id.
/// Lets AI prompts reference past output by id (e.g. "/out 2501") so the
/// fallback model can fetch full content on demand instead of being fed
/// every byte up front.
/// </summary>
public sealed class ShellOutputRecord
{
    public int Id { get; init; }
    public string Command { get; init; } = "";
    public string Cwd { get; init; } = "";
    public int ExitCode { get; init; }
    public int LineCount { get; init; }
    public DateTime TimestampUtc { get; init; }
    public string LastLine { get; init; } = "";
}

/// <summary>
/// File-backed registry of captured shell outputs. One raw log file per id
/// plus a jsonl index for quick listing. Threadsafe for single-process use.
/// </summary>
public static class ShellOutputStore
{
    private static readonly object _lock = new();
    private static readonly string Dir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".",
        "wkappbot.hq", "runtime", "shell-out");
    private static readonly string IndexFile = Path.Combine(Dir, "index.jsonl");
    private static int _nextId = -1;

    private static void EnsureInit()
    {
        if (_nextId >= 0) return;
        try
        {
            Directory.CreateDirectory(Dir);
            // Seed _nextId from the highest existing id on disk so restart-safety holds.
            int maxId = 0;
            if (File.Exists(IndexFile))
            {
                foreach (var line in File.ReadAllLines(IndexFile))
                {
                    try
                    {
                        var r = JsonSerializer.Deserialize<ShellOutputRecord>(line);
                        if (r != null && r.Id > maxId) maxId = r.Id;
                    }
                    catch { }
                }
            }
            _nextId = maxId;
        }
        catch { _nextId = 0; }
    }

    /// <summary>
    /// Store a shell command's output and return the assigned record.
    /// Output is written verbatim to Dir/{id}.log; a trimmed record is
    /// appended to the index for cheap listing.
    /// </summary>
    public static ShellOutputRecord Store(string command, string cwd, int exitCode, string output)
    {
        lock (_lock)
        {
            EnsureInit();
            var id = System.Threading.Interlocked.Increment(ref _nextId);
            var lines = string.IsNullOrEmpty(output) ? 0 : output.Split('\n').Length;
            var lastLine = "";
            if (!string.IsNullOrEmpty(output))
            {
                var split = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0)
                {
                    lastLine = split[^1].TrimEnd('\r');
                    if (lastLine.Length > 120) lastLine = lastLine[..117] + "...";
                }
            }

            var rec = new ShellOutputRecord
            {
                Id = id,
                Command = command,
                Cwd = cwd,
                ExitCode = exitCode,
                LineCount = lines,
                TimestampUtc = DateTime.UtcNow,
                LastLine = lastLine,
            };

            try
            {
                File.WriteAllText(Path.Combine(Dir, $"{id}.log"), output ?? "");
                File.AppendAllText(IndexFile, JsonSerializer.Serialize(rec) + "\n");
            }
            catch { /* best-effort */ }

            return rec;
        }
    }

    /// <summary>Read full captured output by id. Null when the log file is missing.</summary>
    public static string? ReadById(int id)
    {
        try
        {
            var p = Path.Combine(Dir, $"{id}.log");
            return File.Exists(p) ? File.ReadAllText(p) : null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Trim old outputs -- keeps only the most recent <paramref name="keepLast"/>
    /// log files. Index file is NOT pruned so the historical list still works.
    /// </summary>
    public static void Cleanup(int keepLast = 100)
    {
        try
        {
            if (!Directory.Exists(Dir)) return;
            var logs = new DirectoryInfo(Dir).GetFiles("*.log")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .Skip(keepLast)
                .ToList();
            foreach (var f in logs) try { f.Delete(); } catch { }
        }
        catch { }
    }
}
