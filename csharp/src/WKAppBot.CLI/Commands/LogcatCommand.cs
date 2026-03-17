using System.Text;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot logcat <fileFilter> [regex ...] [grep-opts] [--hq] [--past N] [-f] [--timeout N] [-r]
    // fileFilter : glob + ';' OR  (e.g. "*.file.*;*.eye.*" / "**" = all)
    // regex args : pure regex, multiple = AND. grep-compatible options:
    //   -A N / -B N / -C N  after/before/context lines
    //   -v                  invert match
    //   -l                  filenames only (no content)
    //   -c                  count matches per file
    //   -m N                stop after N matches per file
    //   -i                     ignore case (grap default: case-sensitive; logcat default: insensitive)
    //   -n                     show line numbers
    static int LogcatCommand(string[] args)
    {
        var selfPid = Environment.ProcessId;
        var selfLogMarker = $"pid={selfPid}.txt";
        var fileFilterArg = "*.txt";
        var messageFilterArg = "*";
        string? baseDirOverride = null;
        int maxDepth = 0; // 0 = current dir only, -1 = unlimited, N = depth limit
        bool includeHq = false;
        double pastSeconds = 0;    // 0 = no past scan; >0 = scan existing files this old
        bool follow = false;       // --past without --follow: scan and exit (grep-style)
        double timeoutSeconds = 0; // 0 = run forever
        int contextLines = 0;      // -C N: lines before+after each match
        int afterLines = 0;        // -A N
        int beforeLines = 0;       // -B N
        bool invertMatch = false;  // -v
        bool filesOnly = false;    // -l: filenames only
        bool countOnly = false;    // -c: count per file
        int maxCount = int.MaxValue; // -m N: max matches per file
        bool showLineNums = false; // -n: prepend line numbers
        // Case: grap/grep default = case-sensitive (grep compat); logcat default = insensitive
        bool ignoreCase = !GrapMode;

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
            else if (a is "--follow" or "-f") { follow = true; }
            else if ((a == "-C" || a == "--context")       && i + 1 < args.Length && int.TryParse(args[++i], out var c))   { contextLines = c; }
            else if (a.StartsWith("-C")                    && int.TryParse(a[2..], out var c2))  { contextLines = c2; }
            else if ((a == "-A" || a == "--after-context") && i + 1 < args.Length && int.TryParse(args[++i], out var ac))  { afterLines = ac; }
            else if (a.StartsWith("-A")                    && int.TryParse(a[2..], out var ac2)) { afterLines = ac2; }
            else if ((a == "-B" || a == "--before-context")&& i + 1 < args.Length && int.TryParse(args[++i], out var bc))  { beforeLines = bc; }
            else if (a.StartsWith("-B")                    && int.TryParse(a[2..], out var bc2)) { beforeLines = bc2; }
            else if (a is "-v" or "--invert-match")        { invertMatch = true; }
            else if (a is "-l" or "--files-with-matches")  { filesOnly = true; }
            else if (a is "-c" or "--count")               { countOnly = true; }
            else if ((a == "-m" || a == "--max-count")     && i + 1 < args.Length && int.TryParse(args[++i], out var mx))  { maxCount = mx; }
            else if (a.StartsWith("-m")                    && int.TryParse(a[2..], out var mx2)) { maxCount = mx2; }
            else if (a is "-i" or "--ignore-case")           { ignoreCase = true; }
            else if (a is "--case-sensitive" or "--no-ignore-case") { ignoreCase = false; }
            else if (a is "-n" or "--line-number")           { showLineNums = true; }
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

        // -C overrides -A/-B
        if (contextLines > 0) { afterLines = contextLines; beforeLines = contextLines; }

        Regex? msgRegex = null;
        if (!string.IsNullOrWhiteSpace(messageFilterArg) && messageFilterArg != "*")
        {
            var rxOpts = RegexOptions.Compiled | (ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            msgRegex = new Regex(messageFilterArg, rxOpts);
        }

        // Build watch directories: default = CWD (or --basedir)
        var baseDir = baseDirOverride ?? Environment.CurrentDirectory;
        var dirs = new List<string> { baseDir };

        // --hq: add wkappbot.hq/logs + all old-*/ subdirs + openclaw dirs
        if (includeHq)
        {
            var hqLogsDir = Path.Combine(DataDir, "logs");
            dirs.Add(hqLogsDir);
            // Add all old-*/ subdirs (new scheme) + legacy old/ (backward compat)
            if (Directory.Exists(hqLogsDir))
            {
                foreach (var sub in Directory.GetDirectories(hqLogsDir, "old*"))
                    dirs.Add(sub);
            }
            dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions"));
            dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "logs"));
        }

        // Non-TTY (piped/redirected) without explicit watch flags → one-shot grep mode.
        // result=$(grap pattern *.txt) should work like grep: scan files, output matches, exit.
        // Diagnostic lines go to stderr so stdout contains only matches (grep-compatible).
        // grep-compat: one-shot when piped OR when called as grap/grep (TTY or not)
        bool autoOneShot = (Console.IsOutputRedirected || GrapMode) && pastSeconds == 0 && !follow && timeoutSeconds == 0;

        var depthLabel   = maxDepth == 0 ? "" : maxDepth == -1 ? " -r" : $" -r={maxDepth}";
        var pastLabel    = pastSeconds    > 0 ? $" --past {FormatDuration(pastSeconds)}" : "";
        var timeoutLabel = timeoutSeconds > 0 ? $" --timeout {FormatDuration(timeoutSeconds)}" : "";
        var diagOut = autoOneShot ? Console.Error : Console.Out;
        if (!autoOneShot || !GrapMode)
            diagOut.WriteLine($"[LOGCAT] start file='{fileFilterArg}' msg='{messageFilterArg}' basedir='{baseDir}'{depthLabel}{(includeHq ? " --hq" : "")}{pastLabel}{timeoutLabel}{(autoOneShot ? " (grep-mode)" : " (Ctrl+C to stop)")}");

        // Debug: write timing to C:\Temp\grap_fast_exit.txt (shared with FastExit debug)
        var _lcSw = System.Diagnostics.Stopwatch.StartNew();
        void LcDbg(string s) { try { using var lf = System.IO.File.AppendText(@"C:\Temp\grap_fast_exit.txt"); lf.WriteLine($"LC {_lcSw.ElapsedMilliseconds}ms {s}"); } catch { } }
        LcDbg($"entered autoOneShot={autoOneShot} pastSeconds={pastSeconds} isOutputRedirected={Console.IsOutputRedirected}");

        // Auto one-shot: scan all matching files, emit matches, exit (no watch loop)
        if (autoOneShot)
        {
            var searchOpt = maxDepth != 0 ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var oneShotOffsets    = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            var oneShotLineCounts = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            string oneShotHeader  = "";

            var allFiles = dirs.Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(Directory.Exists)
                .SelectMany(dir => { try { return Directory.GetFiles(dir, "*", searchOpt); } catch { return Array.Empty<string>(); } })
                .Where(p => { var fn = Path.GetFileName(p); return IsFilePatternMatch(fn, filePatterns) && !fn.Contains(selfLogMarker, StringComparison.OrdinalIgnoreCase); })
                .OrderBy(p => { try { return new FileInfo(p).LastWriteTimeUtc; } catch { return DateTime.MinValue; } })
                .ToList();

            var grepOut = OriginalStdout;
            bool showFilename = allFiles.Count != 1; // grep: suppress filename for single file
            foreach (var filePath in allFiles)
                EmitDeltaLines(filePath, oneShotOffsets, oneShotLineCounts, msgRegex, ref oneShotHeader, contextLines, afterLines, beforeLines, invertMatch, filesOnly, countOnly, maxCount, grepOut, showFilename, showLineNums);

            try { grepOut.Flush(); } catch { }
            // grep exit code convention: 0=match found, 1=no match, 2=error
            return oneShotLineCounts.Values.Sum() > 0 ? 0 : 1;
        }

        LcDbg("before CancelKeyPress setup");
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        if (timeoutSeconds > 0) cts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));
        LcDbg("after CancelKeyPress setup");

        // --past: scan existing files modified within the window before entering live mode
        if (pastSeconds > 0)
        {
            var cutoff = DateTime.UtcNow.AddSeconds(-pastSeconds);
            var pastDirs = new List<string>(dirs);
            // Always include HQ logs/old* when --past is active (that's where finished logs live)
            if (includeHq)
            {
                var hqLogsDir = Path.Combine(DataDir, "logs");
                // new scheme: old-{subkey}/ dirs; legacy: old/
                if (Directory.Exists(hqLogsDir))
                    foreach (var sub in Directory.GetDirectories(hqLogsDir, "old*"))
                        pastDirs.Add(sub);
            }

            var pastOffsets   = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            var pastLineCounts = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
            string pastHeader = "";

            LcDbg($"building pastFiles from dirs: {string.Join(", ", pastDirs)}");
            var pastFiles = pastDirs.Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(Directory.Exists)
                .SelectMany(dir => { try { return Directory.GetFiles(dir, "*", SearchOption.TopDirectoryOnly); } catch { return Array.Empty<string>(); } })
                .Select(p => new FileInfo(p))
                .Where(f =>
                {
                    try
                    {
                        return IsFilePatternMatch(f.Name, filePatterns)
                            && !f.Name.Contains(selfLogMarker, StringComparison.OrdinalIgnoreCase)
                            && f.LastWriteTimeUtc >= cutoff;
                    }
                    catch { return false; } // e.g. \\.\nul device file throws on LastWriteTimeUtc
                })
                .OrderBy(f => { try { return f.LastWriteTimeUtc; } catch { return DateTime.MinValue; } })
                .ToList();

            LcDbg($"pastFiles.Count={pastFiles.Count}");
            if (pastFiles.Count > 0)
            {
                Console.Error.WriteLine($"[LOGCAT] --past: scanning {pastFiles.Count} file(s) from last {FormatDuration(pastSeconds)}");
                foreach (var f in pastFiles)
                    EmitDeltaLines(f.FullName, pastOffsets, pastLineCounts, msgRegex, ref pastHeader, contextLines, afterLines, beforeLines, invertMatch, filesOnly, countOnly, maxCount);
            }
            else
            {
                Console.Error.WriteLine($"[LOGCAT] --past: no files found in last {FormatDuration(pastSeconds)}");
            }

            LcDbg($"EmitDeltaLines done for {pastFiles.Count} files");
            // --past without --follow/--timeout: grep-style, exit after scan
            if (!follow && timeoutSeconds == 0)
            {
                Console.Error.WriteLine($"[LOGCAT] --past: done ({pastFiles.Count} file(s))");
                LcDbg("returning 0");
                return pastLineCounts.Values.Sum() > 0 ? 0 : 1;
            }
            Console.Error.WriteLine("[LOGCAT] --past: done, entering live mode...");
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
                Filter = "*",
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
                    EmitDeltaLines(e.FullPath, offsets, lineCounts, msgRegex, ref lastHeaderPath, contextLines, afterLines, beforeLines, invertMatch, filesOnly, countOnly, maxCount);
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
                    EmitDeltaLines(e.FullPath, offsets, lineCounts, msgRegex, ref lastHeaderPath, contextLines, afterLines, beforeLines, invertMatch, filesOnly, countOnly, maxCount);
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

        diagOut.WriteLine("[LOGCAT] stopped");
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

    // Parse duration string: "30s" / "5m" / "1h" / "2d" / "3600" (bare number = seconds)
    static double ParsePastDuration(string s)
    {
        s = s.Trim().ToLowerInvariant();
        if (s.EndsWith("d") && double.TryParse(s[..^1], out var d)) return d * 86400;
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

    const int MaxLineLengthForEmit = 32_768; // 32 KB — guard against binary files with no newlines

    static void EmitDeltaLines(
        string path,
        Dictionary<string, long> offsets,
        Dictionary<string, long> lineCounts,
        Regex? msgRegex,
        ref string lastHeaderPath,
        int contextLines = 0,
        int afterLines = 0, int beforeLines = 0,
        bool invertMatch = false, bool filesOnly = false,
        bool countOnly = false, int maxCount = int.MaxValue,
        TextWriter? grepOut = null,  // non-null → grep-mode: clean output to this writer
        bool showFilename = true,    // false → suppress filename prefix (single-file grep compat)
        bool showLineNums = false)   // -n: prepend line numbers
    {
        if (!File.Exists(path)) return;

        long start = 0;
        if (offsets.TryGetValue(path, out var prev)) start = prev;

        FileStream? fs = null;
        try { fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete); }
        catch { return; } // locked or inaccessible — skip silently
        if (start > fs.Length) start = 0;
        fs.Seek(start, SeekOrigin.Begin);
        using var sr = new StreamReader(fs, Encoding.UTF8, true); // sr owns fs

        long emitted = lineCounts.TryGetValue(path, out var lc) ? lc : 0;

        // No filter: tail-preview mode — path + last 7 non-empty lines
        if (msgRegex == null && !invertMatch)
        {
            var tail = new Queue<string>(8);
            while (!sr.EndOfStream)
            {
                string? line;
                try { line = sr.ReadLine(); } catch { break; }
                if (line == null) break;
                if (line.Length > MaxLineLengthForEmit) break; // binary file guard
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (tail.Count == 7) tail.Dequeue();
                tail.Enqueue(line);
            }
            if (grepOut != null)
            {
                foreach (var line in tail) grepOut.WriteLine(showFilename ? $"{path}:{line}" : line);
            }
            else
            {
                Console.WriteLine($"{path}");
                foreach (var line in tail) Console.WriteLine($"  {line}");
            }
            lastHeaderPath = path;
            offsets[path] = fs.Length;
            lineCounts[path] = emitted;
            return;
        }

        // Read all new lines into buffer (needed for before-context and -n line numbers)
        var buf = new List<string>();
        var lineNums = new List<int>(); // actual line numbers (parallel to buf)
        int lineNum = 0;
        while (!sr.EndOfStream)
        {
            string? line;
            try { line = sr.ReadLine(); } catch { break; }
            lineNum++;
            if (line == null || line.Length > MaxLineLengthForEmit) break; // binary file guard
            if (!string.IsNullOrWhiteSpace(line)) { buf.Add(line); lineNums.Add(lineNum); }
        }

        // -l: filenames only
        if (filesOnly)
        {
            bool any = buf.Any(l => msgRegex == null ? !invertMatch : msgRegex.IsMatch(l) != invertMatch);
            if (any) (grepOut ?? Console.Out).WriteLine(path);
            offsets[path] = fs.Length; lineCounts[path] = any ? emitted + 1 : emitted;
            return;
        }

        // -c: count only
        int matchCount = 0;
        if (countOnly)
        {
            matchCount = buf.Count(l => msgRegex == null ? !invertMatch : msgRegex.IsMatch(l) != invertMatch);
            // Always output count (even 0) when showFilename; grep outputs "file:count"
            var countOut = grepOut ?? Console.Out;
            if (showFilename) countOut.WriteLine($"{path}:{matchCount}");
            else countOut.WriteLine($"{matchCount}");
            lineCounts[path] = (long)matchCount;
            offsets[path] = fs.Length;
            return;
        }

        string FormatLine(int j, bool isMatch) {
            var prefix = showFilename ? $"{path}:" : "";
            var numPfx = showLineNums ? $"{lineNums[j]}:" : "";
            return $"{prefix}{numPfx}{buf[j]}";
        }

        int printUntil = -1;
        for (int i = 0; i < buf.Count && emitted < maxCount; i++)
        {
            bool matched = msgRegex == null ? true : msgRegex.IsMatch(buf[i]);
            if (matched == invertMatch) continue; // -v: flip

            emitted++;
            if (grepOut != null)
            {
                // Grep-mode: clean output — file:content (no timestamps, no markers)
                int bef  = Math.Max(beforeLines, 0);
                int aft  = Math.Max(afterLines, 0);
                int from = Math.Max(printUntil + 1, i - bef);
                int to   = Math.Min(buf.Count - 1, i + aft);
                // -- separator only BETWEEN groups (not before first group)
                if ((bef > 0 || aft > 0) && printUntil >= 0 && from > printUntil + 1)
                    grepOut.WriteLine("--");
                for (int j = from; j <= to; j++)
                    grepOut.WriteLine(FormatLine(j, j == i));
                printUntil = to;
            }
            else
            {
                // Interactive mode: file header + marker + timestamp
                if (!string.Equals(lastHeaderPath, path, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"풀경로: {path}");
                    lastHeaderPath = path;
                }

                int bef  = Math.Max(beforeLines, 0);
                int aft  = Math.Max(afterLines, 0);
                int from = Math.Max(printUntil + 1, i - bef);
                int to   = Math.Min(buf.Count - 1, i + aft);

                if ((bef > 0 || aft > 0) && from > printUntil + 1)
                    Console.WriteLine("--");

                for (int j = from; j <= to; j++)
                {
                    var marker = j == i ? $"\t...({emitted})" : "\t      ";
                    Console.WriteLine($"{marker}: {buf[j]} ({DateTime.Now:HH:mm:ss})");
                }
                printUntil = to;
            }
        }

        lineCounts[path] = emitted;
        offsets[path] = fs.Position;
    }
}
