using System.Text;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot logcat <fileFilter> [regex1 regex2 ...] [--basedir <dir>] [-r[=N]] [--hq] [--past Ns/Nm/Nh]
    // fileFilter : glob pattern, ';' = OR  (e.g. "*.file.*;*.eye.*")
    // regex args : pure regex, multiple = AND  (e.g. "OCR-DEEP" "block(len|size)")
    // Default: current directory only. -r unlimited depth. -r=3 depth limit. --hq adds HQ+openclaw.
    static int LogcatCommand(string[] args)
    {
        var selfPid = Environment.ProcessId;
        var selfLogMarker = $"pid={selfPid}.txt";
        var fileFilterArg = "*.txt";
        var messageFilterArg = "*";
        string? baseDirOverride = null;
        int maxDepth = 0; // 0 = current dir only, -1 = unlimited, N = depth limit
        bool includeHq = false;
        double pastSeconds = 0; // 0 = no past scan; >0 = scan existing files this old
        double timeoutSeconds = 0; // 0 = run forever

        // Parse positional + named args
        var positional = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a == "--basedir" && i + 1 < args.Length) { baseDirOverride = args[++i]; }
            else if (a == "--recursive") { maxDepth = -1; }
            else if (a == "-r") { maxDepth = -1; }
            else if (a.StartsWith("-r=") && int.TryParse(a[3..], out var d)) { maxDepth = d; }
            else if (a.StartsWith("--recursive=") && int.TryParse(a[12..], out var d2)) { maxDepth = d2; }
            else if (a == "--hq") { includeHq = true; }
            else if (a == "--past" && i + 1 < args.Length) { pastSeconds = ParsePastDuration(args[++i]); }
            else if (a.StartsWith("--past=")) { pastSeconds = ParsePastDuration(a[7..]); }
            else if (a == "--timeout" && i + 1 < args.Length) { timeoutSeconds = ParsePastDuration(args[++i]); }
            else if (a.StartsWith("--timeout=")) { timeoutSeconds = ParsePastDuration(a[10..]); }
            else { positional.Add(a); }
        }
        if (positional.Count > 0) fileFilterArg = positional[0];
        // positional[1..] = regex patterns → AND lookahead: (?=.*pat1)(?=.*pat2)...
        var keywords = positional.Skip(1).ToList();
        if (keywords.Count == 1)
            messageFilterArg = keywords[0];
        else if (keywords.Count > 1)
            messageFilterArg = string.Concat(keywords.Select(k => $"(?=.*(?:{k}))"));

        var filePatterns = fileFilterArg.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (filePatterns.Length == 0) filePatterns = new[] { "*.txt" };

        Regex? msgRegex = null;
        if (!string.IsNullOrWhiteSpace(messageFilterArg) && messageFilterArg != "*")
            msgRegex = new Regex(messageFilterArg, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Build watch directories: default = CWD (or --basedir)
        var baseDir = baseDirOverride ?? Environment.CurrentDirectory;
        var dirs = new List<string> { baseDir };

        // --hq: add wkappbot.hq/logs + openclaw dirs
        if (includeHq)
        {
            dirs.Add(Path.Combine(DataDir, "logs"));
            dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions"));
            dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "logs"));
        }

        var depthLabel   = maxDepth == 0 ? "" : maxDepth == -1 ? " -r" : $" -r={maxDepth}";
        var pastLabel    = pastSeconds    > 0 ? $" --past {FormatDuration(pastSeconds)}" : "";
        var timeoutLabel = timeoutSeconds > 0 ? $" --timeout {FormatDuration(timeoutSeconds)}" : "";
        Console.WriteLine($"[LOGCAT] start file='{fileFilterArg}' msg='{messageFilterArg}' basedir='{baseDir}'{depthLabel}{(includeHq ? " --hq" : "")}{pastLabel}{timeoutLabel} (Ctrl+C to stop)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        if (timeoutSeconds > 0) cts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

        // --past: scan existing files modified within the window before entering live mode
        if (pastSeconds > 0)
        {
            var cutoff = DateTime.UtcNow.AddSeconds(-pastSeconds);
            var pastDirs = new List<string>(dirs);
            // Always include HQ logs/old when --past is active (that's where finished logs live)
            if (includeHq)
                pastDirs.Add(Path.Combine(DataDir, "logs", "old"));

            var pastOffsets   = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            var pastLineCounts = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            string pastHeader = "";

            var pastFiles = pastDirs.Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(Directory.Exists)
                .SelectMany(dir => Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly))
                .Select(p => new FileInfo(p))
                .Where(f => IsFilePatternMatch(f.Name, filePatterns)
                         && !f.Name.Contains(selfLogMarker, StringComparison.OrdinalIgnoreCase)
                         && f.LastWriteTimeUtc >= cutoff)
                .OrderBy(f => f.LastWriteTimeUtc)
                .ToList();

            if (pastFiles.Count > 0)
            {
                Console.WriteLine($"[LOGCAT] --past: scanning {pastFiles.Count} file(s) from last {FormatDuration(pastSeconds)}");
                foreach (var f in pastFiles)
                    EmitDeltaLines(f.FullName, pastOffsets, pastLineCounts, msgRegex, ref pastHeader);
                Console.WriteLine("[LOGCAT] --past: done, entering live mode...");
            }
            else
            {
                Console.WriteLine($"[LOGCAT] --past: no files found in last {FormatDuration(pastSeconds)}");
            }
        }

        var watchers = new List<FileSystemWatcher>();
        var offsets = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        var lineCounts = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        string lastHeaderPath = "";

        foreach (var dir in dirs.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (!Directory.Exists(dir)) continue;

            var watchDir = dir;
            var w = new FileSystemWatcher(watchDir)
            {
                IncludeSubdirectories = maxDepth != 0,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = "*.*",
                EnableRaisingEvents = true
            };

            var capturedDir = watchDir; // closure capture
            FileSystemEventHandler onChange = (_, e) =>
            {
                try
                {
                    if (maxDepth > 0 && GetRelativeDepth(e.FullPath, capturedDir) > maxDepth) return;
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
                    if (maxDepth > 0 && GetRelativeDepth(e.FullPath, capturedDir) > maxDepth) return;
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
        {
            Thread.Sleep(120);
            // Broken pipe: stdout closed (e.g. piped to head/grep that exited)
            try { if (Console.IsOutputRedirected) Console.Out.Flush(); }
            catch (IOException) { break; }
        }

        foreach (var w in watchers)
        {
            try { w.EnableRaisingEvents = false; w.Dispose(); } catch { }
        }

        Console.WriteLine("[LOGCAT] stopped");
        return 0;
    }

    // grap-style file pattern: "regex:..." for regex, "*" wildcard, ";" OR
    static bool IsFilePatternMatch(string fileName, string[] patterns)
    {
        foreach (var p in patterns)
        {
            if (p.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
            {
                if (Regex.IsMatch(fileName, p[6..], RegexOptions.IgnoreCase)) return true;
            }
            else
            {
                if (WildcardMatch(fileName, p)) return true;
            }
        }
        return false;
    }

    static int GetRelativeDepth(string fullPath, string baseDir)
    {
        var rel = Path.GetRelativePath(baseDir, Path.GetDirectoryName(fullPath) ?? fullPath);
        if (rel == ".") return 0;
        return rel.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Length;
    }

    static bool WildcardMatch(string input, string pattern)
    {
        var regex = "^" + Regex.Escape(pattern)
            .Replace("\\*", ".*")
            .Replace("\\?", ".") + "$";
        return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
    }

    // Parse duration string: "30s" / "5m" / "1h" / "3600" (bare number = seconds)
    static double ParsePastDuration(string s)
    {
        s = s.Trim().ToLowerInvariant();
        if (s.EndsWith("h") && double.TryParse(s[..^1], out var h)) return h * 3600;
        if (s.EndsWith("m") && double.TryParse(s[..^1], out var m)) return m * 60;
        if (s.EndsWith("s") && double.TryParse(s[..^1], out var sec)) return sec;
        if (double.TryParse(s, out var bare)) return bare;
        return 3600; // default 1h
    }

    static string FormatDuration(double seconds)
    {
        if (seconds >= 3600) return $"{seconds / 3600:0.#}h";
        if (seconds >= 60)   return $"{seconds / 60:0.#}m";
        return $"{seconds:0}s";
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
