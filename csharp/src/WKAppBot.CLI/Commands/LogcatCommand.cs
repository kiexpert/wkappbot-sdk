using System.Text;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot logcat "*.txt;*.jsonl" "A11Y|ACT|FALLBACK"
    static int LogcatCommand(string[] args)
    {
        var selfPid = Environment.ProcessId;
        var selfLogMarker = $"pid={selfPid}.txt";
        var fileFilterArg = args.Length > 0 ? args[0] : "*.txt";
        var messageFilterArg = args.Length > 1 ? args[1] : "*";

        var filePatterns = fileFilterArg.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (filePatterns.Length == 0) filePatterns = new[] { "*.txt" };

        Regex? msgRegex = null;
        if (!string.IsNullOrWhiteSpace(messageFilterArg) && messageFilterArg != "*")
            msgRegex = new Regex(messageFilterArg, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        Console.WriteLine($"[LOGCAT] start file='{fileFilterArg}' msg='{messageFilterArg}' (Ctrl+C to stop)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        var dirs = new List<string>
        {
            Path.Combine(DataDir, "logs"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "logs")
        };

        var watchers = new List<FileSystemWatcher>();
        var offsets = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        var lineCounts = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        string lastHeaderPath = "";

        foreach (var dir in dirs.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (!Directory.Exists(dir)) continue;

            var w = new FileSystemWatcher(dir)
            {
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = "*.*",
                EnableRaisingEvents = true
            };

            FileSystemEventHandler onChange = (_, e) =>
            {
                try
                {
                    var fn = Path.GetFileName(e.FullPath);
                    if (!IsFilePatternMatch(fn, filePatterns)) return;
                    if (fn.Contains(selfLogMarker, StringComparison.OrdinalIgnoreCase)) return;
                    EmitDeltaLines(e.FullPath, offsets, lineCounts, msgRegex, ref lastHeaderPath);
                }
                catch { }
            };

            RenamedEventHandler onRename = (_, e) =>
            {
                try
                {
                    var fn = Path.GetFileName(e.FullPath);
                    if (!IsFilePatternMatch(fn, filePatterns)) return;
                    if (fn.Contains(selfLogMarker, StringComparison.OrdinalIgnoreCase)) return;
                    EmitDeltaLines(e.FullPath, offsets, lineCounts, msgRegex, ref lastHeaderPath);
                }
                catch { }
            };

            w.Changed += onChange;
            w.Created += onChange;
            w.Renamed += onRename;
            watchers.Add(w);
        }

        while (!cts.IsCancellationRequested)
            Thread.Sleep(120);

        foreach (var w in watchers)
        {
            try { w.EnableRaisingEvents = false; w.Dispose(); } catch { }
        }

        Console.WriteLine("[LOGCAT] stopped");
        return 0;
    }

    static bool IsFilePatternMatch(string fileName, string[] patterns)
    {
        foreach (var p in patterns)
        {
            if (WildcardMatch(fileName, p)) return true;
        }
        return false;
    }

    static bool WildcardMatch(string input, string pattern)
    {
        var regex = "^" + Regex.Escape(pattern)
            .Replace("\\*", ".*")
            .Replace("\\?", ".") + "$";
        return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
    }

    static void EmitDeltaLines(
        string path,
        Dictionary<string, long> offsets,
        Dictionary<string, long> lineCounts,
        Regex? msgRegex,
        ref string lastHeaderPath)
    {
        if (!File.Exists(path)) return;

        long start = 0;
        if (offsets.TryGetValue(path, out var prev)) start = prev;

        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        if (start > fs.Length) start = 0;
        fs.Seek(start, SeekOrigin.Begin);
        using var sr = new StreamReader(fs, Encoding.UTF8, true);

        long emitted = lineCounts.TryGetValue(path, out var lc) ? lc : 0;
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (msgRegex != null && !msgRegex.IsMatch(line)) continue;

            emitted++;
            if (!string.Equals(lastHeaderPath, path, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"풀경로: {path}");
                lastHeaderPath = path;
            }
            Console.WriteLine($"\t...({emitted}): {line} ({DateTime.Now:HH:mm:ss})");
        }

        lineCounts[path] = emitted;
        offsets[path] = fs.Position;
    }
}
