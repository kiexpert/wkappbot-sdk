using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int FileReadCommand(string[] args)
    {
        string? path = null;
        int offset = 0;
        int limit  = 2000;
        int? encoding = null;
        int? endLine = null;
        bool showLineNumbers = true;

        for (int i = 0; i < args.Length; i++)
        {
            if ((args[i] is "--offset" or "--start") && i + 1 < args.Length) int.TryParse(args[++i], out offset);
            else if ((args[i] is "--limit" or "--count") && i + 1 < args.Length) int.TryParse(args[++i], out limit);
            else if (args[i] == "--end" && i + 1 < args.Length) { if (int.TryParse(args[++i], out var parsedEnd)) endLine = parsedEnd; }
            else if (args[i] is "--path" or "--file" && i + 1 < args.Length) path = args[++i];
            else if (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if (args[i] == "--no-line-numbers") showLineNumbers = false;
            else if (!args[i].StartsWith("--")) path = args[i];
        }

        if (endLine.HasValue)
            limit = Math.Max(0, endLine.Value - Math.Max(0, offset));

        if (string.IsNullOrEmpty(path))
            return Error("Usage: file read <path> [--offset N] [--limit N] [--encoding N]");
        if (!File.Exists(path)) { var nfd = path.Normalize(NormalizationForm.FormD); if (File.Exists(nfd)) path = nfd; else return Error($"File not found: {path}"); }

        // Auto-route: .pdf → read-pdf (PdfPig text extraction, not raw bytes)
        if (path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            return FileReadPdfCommand(new[] { path }.Concat(args.Where(a => a != path)).ToArray());

        try
        {
            var bytes = File.ReadAllBytes(path);
            var enc   = DetectFileEncoding(bytes, encoding);
            var lines = enc.GetString(bytes).ReplaceLineEndings("\n").Split('\n');
            int total  = lines.Length;
            int start  = Math.Max(0, offset);
            int end    = Math.Min(total, start + limit);

            Console.Error.WriteLine($"[FILE] {path} ({total} lines, showing {start + 1}-{end})");
            for (int i = start; i < end; i++)
            {
                if (showLineNumbers) Console.WriteLine($"{i + 1,6}\t{lines[i]}");
                else Console.WriteLine(lines[i]);
            }

            if (end < total)
                Console.WriteLine($"... ({total - end} more lines — use --offset {end} to continue)");

            return 0;
        }
        catch (Exception ex) { return Error($"Read failed: {ex.Message}"); }
    }

    // ── file grep ──────────────────────────────────────────────────────────
    static int FileGrepCommand(string[] args)
    {
        string? pattern     = null;
        string  searchRoot  = FileDefaultDir();
        string? typeFilter  = null;
        bool    ignoreCase  = false;
        int     context     = 0;
        int     maxResults  = 200;
        int?    encoding    = null;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] is "--pattern" or "--query" && i + 1 < args.Length) pattern = args[++i];
            else if (args[i] is "--path" or "--root" && i + 1 < args.Length) searchRoot = args[++i];
            else if (args[i] is "--file" && i + 1 < args.Length) searchRoot = args[++i];
            else if (args[i] == "--type"  && i + 1 < args.Length) typeFilter = args[++i].TrimStart('.');
            else if (args[i] is "-i" or "--ignore-case")           ignoreCase = true;
            else if ((args[i] is "-C" or "--context") && i + 1 < args.Length) int.TryParse(args[++i], out context);
            else if (args[i] == "--max"   && i + 1 < args.Length) int.TryParse(args[++i], out maxResults);
            else if (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if (!args[i].StartsWith("-"))
            {
                if (pattern == null) pattern = args[i];           // 1st positional = pattern
                else if (Directory.Exists(args[i]) || File.Exists(args[i])) searchRoot = args[i]; // 2nd positional = folder/file
            }
        }

        if (string.IsNullOrEmpty(pattern))
            return Error("Usage: file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N]");

        var rxOpts = RegexOptions.None;
        if (ignoreCase) rxOpts |= RegexOptions.IgnoreCase;

        Regex regex;
        try { regex = new Regex(pattern, rxOpts); }
        catch (Exception ex) { return Error($"Invalid regex pattern: {ex.Message}"); }

        try
        {
            // ';' in path segments = OR expansion: "W:/A;B/logs" → two roots
            var searchRoots = ExpandGlobSegments(searchRoot).ToList();
            IEnumerable<string> files;
            if (searchRoots.Count == 1 && File.Exists(searchRoots[0]))
                files = searchRoots;
            else
            {
                var ext = typeFilter != null ? $"*.{typeFilter}" : "*";
                files = searchRoots
                    .Where(Directory.Exists)
                    .SelectMany(r => Directory.EnumerateFiles(r, ext, SearchOption.AllDirectories));
                if (!files.Any() && searchRoots.All(r => !Directory.Exists(r) && !File.Exists(r)))
                    return Error($"Path not found: {searchRoot}");
            }

            int matchCount = 0, fileCount = 0;

            foreach (var file in files)
            {
                if (matchCount >= maxResults) break;

                string[] lines;
                try
                {
                    var fileBytes = File.ReadAllBytes(file);
                    // Skip binary files: check first 8KB for null bytes
                    int checkLen = Math.Min(8192, fileBytes.Length);
                    if (Array.IndexOf(fileBytes, (byte)0, 0, checkLen) >= 0) continue;
                    var enc = DetectFileEncoding(fileBytes, encoding);
                    lines = enc.GetString(fileBytes).ReplaceLineEndings("\n").Split('\n');
                }
                catch { continue; }

                bool headerPrinted = false;
                int lastCtxEnd = -1; // for elision between context blocks
                for (int i = 0; i < lines.Length && matchCount < maxResults; i++)
                {
                    if (!regex.IsMatch(lines[i])) continue;

                    if (!headerPrinted)
                    {
                        Console.WriteLine($"\n[FILE] {file}");
                        headerPrinted = true;
                        fileCount++;
                    }

                    int ctxStart = Math.Max(0, i - context);
                    int ctxEnd   = Math.Min(lines.Length - 1, i + context);
                    // Elision: show "   ..." between non-contiguous context blocks
                    if (lastCtxEnd >= 0 && ctxStart > lastCtxEnd + 1)
                        Console.WriteLine("   ...");
                    for (int ci = ctxStart; ci <= ctxEnd; ci++)
                    {
                        if (ci == i) // match line: arrow marker
                            Console.WriteLine($"> {ci + 1,5}| {lines[ci]}");
                        else         // context line
                            Console.WriteLine($"  {ci + 1,5}| {lines[ci]}");
                    }
                    lastCtxEnd = ctxEnd;

                    matchCount++;
                }
            }

            if (matchCount == 0)
                Console.Error.WriteLine($"[GREP] No matches for: {pattern}");
            else
                Console.WriteLine($"\n[GREP] {matchCount} match(es) in {fileCount} file(s)");

            return 0;
        }
        catch (Exception ex) { return Error($"Grep failed: {ex.Message}"); }
    }

    // ── file json-grep ──────────────────────────────────────────────────────
    /// <summary>
    /// Structural JSON/JSONL search: match objects where key matches key-regex AND value matches value-regex.
    /// Pattern: '{ "key_regex": "value_regex" }' or simple string pattern (matches any value).
    /// Streaming line-by-line for JSONL. Multi-line JSON uses brace-depth counting.
    ///
    /// Usage: wkappbot file json-grep '{ "role": "user" }' session.jsonl [--max N] [--context N] [-i]
    ///        wkappbot json-grep "screensaver" *.jsonl --path ~/.claude
    /// </summary>
    static int FileJsonGrepCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "--help" or "-h")
        {
            Console.WriteLine(@"Usage: wkappbot file json-grep <pattern> [files...] [--path dir] [--max N] [-i]

Pattern formats:
  Simple:     ""keyword""           — matches any JSON value containing keyword
  Structural: '{ ""key"": ""val"" }'  — matches objects where key matches key-regex AND value matches val-regex
              Multiple pairs: AND logic (all must match)

Examples:
  wkappbot file json-grep '{ ""role"": ""user"" }' session.jsonl
  wkappbot file json-grep '{ ""role"": ""user"", ""content"": ""screensaver"" }' *.jsonl
  wkappbot file json-grep ""error"" --path W:/SDK/bin/wkappbot.hq --max 20
  wkappbot json-grep ""TypeAndSubmit"" ~/.claude/projects/**/sessions/*.jsonl");
            return 0;
        }

        // Parse args
        string pattern = args[0];
        var filePaths = new List<string>();
        string? searchPath = null;
        int maxResults = 50;
        bool ignoreCase = false;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--path" && i + 1 < args.Length) { searchPath = args[++i]; continue; }
            if (args[i] == "--max" && i + 1 < args.Length) { int.TryParse(args[++i], out maxResults); continue; }
            if (args[i] == "-i") { ignoreCase = true; continue; }
            filePaths.Add(args[i]);
        }

        // Resolve file paths
        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        var resolvedFiles = new List<string>();
        if (filePaths.Count == 0 && searchPath != null)
            filePaths.Add("**/*.jsonl");

        foreach (var fp in filePaths)
        {
            var full = Path.IsPathRooted(fp) ? fp : Path.Combine(searchPath ?? cwd, fp);
            var dir = Path.GetDirectoryName(full) ?? cwd;
            var glob = Path.GetFileName(full);
            if (glob.Contains('*') || glob.Contains('?'))
            {
                if (full.Contains("**"))
                {
                    var baseDir = full.Split(new[] { "**" }, StringSplitOptions.None)[0].TrimEnd('/', '\\');
                    if (!Directory.Exists(baseDir)) continue;
                    var ext = glob.TrimStart('*').TrimStart('.');
                    resolvedFiles.AddRange(Directory.GetFiles(baseDir, $"*.{ext}", SearchOption.AllDirectories));
                }
                else if (Directory.Exists(dir))
                    resolvedFiles.AddRange(Directory.GetFiles(dir, glob));
            }
            else resolvedFiles.Add(full);
        }

        if (resolvedFiles.Count == 0)
        {
            Console.WriteLine("[JSON-GREP] No files to search");
            return 1;
        }

        // Parse structural pattern: { "key": "val", "key2": "val2" }
        var kvPatterns = new List<(Regex keyRx, Regex valRx)>();
        bool isStructural = false;
        var regexOpts = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

        var trimmed = pattern.Trim();
        if (trimmed.StartsWith("{") && trimmed.EndsWith("}"))
        {
            isStructural = true;
            // Parse key-value pairs from pseudo-JSON pattern
            var inner = trimmed[1..^1].Trim();
            // Split on commas that are between pairs (not inside quotes)
            foreach (var pair in SplitJsonPatternPairs(inner))
            {
                var colonIdx = pair.IndexOf(':');
                if (colonIdx < 0) continue;
                var keyPart = pair[..colonIdx].Trim().Trim('"');
                var valPart = pair[(colonIdx + 1)..].Trim().Trim('"');
                kvPatterns.Add((new Regex(keyPart, regexOpts), new Regex(valPart, regexOpts)));
            }
        }

        Regex? simpleRx = !isStructural ? new Regex(pattern, regexOpts) : null;

        // Search
        int matchCount = 0;
        int fileCount = 0;

        foreach (var file in resolvedFiles)
        {
            if (matchCount >= maxResults) break;
            if (!File.Exists(file)) continue;

            bool headerPrinted = false;
            try
            {
                int lineNo = 0;
                foreach (var line in File.ReadLines(file))
                {
                    lineNo++;
                    if (matchCount >= maxResults) break;
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    bool matched = false;

                    if (isStructural)
                    {
                        // Parse line as JSON and check key-value patterns
                        try
                        {
                            var node = System.Text.Json.Nodes.JsonNode.Parse(line);
                            if (node is System.Text.Json.Nodes.JsonObject obj)
                                matched = MatchJsonObject(obj, kvPatterns);
                        }
                        catch { } // not valid JSON — skip
                    }
                    else
                    {
                        matched = simpleRx!.IsMatch(line);
                    }

                    if (matched)
                    {
                        if (!headerPrinted)
                        {
                            Console.WriteLine($"\n[FILE] {file}");
                            headerPrinted = true;
                            fileCount++;
                        }
                        // Truncate long lines for display
                        var display = line.Length > 200 ? line[..200] + "..." : line;
                        Console.WriteLine($"> {lineNo,5}| {display}");
                        matchCount++;
                    }
                }
            }
            catch (Exception ex) { Console.Error.WriteLine($"[JSON-GREP] Error reading {file}: {ex.Message}"); }
        }

        if (matchCount == 0)
            Console.Error.WriteLine($"[JSON-GREP] No matches for: {pattern}");
        else
            Console.WriteLine($"\n[JSON-GREP] {matchCount} match(es) in {fileCount} file(s)");

        return 0;
    }

    /// <summary>Match a JsonObject against key-value regex patterns (AND logic).</summary>
    static bool MatchJsonObject(System.Text.Json.Nodes.JsonObject obj, List<(Regex keyRx, Regex valRx)> patterns)
    {
        foreach (var (keyRx, valRx) in patterns)
        {
            bool pairMatched = false;
            foreach (var prop in obj)
            {
                if (!keyRx.IsMatch(prop.Key)) continue;
                var valStr = prop.Value?.ToString() ?? "";
                if (valRx.IsMatch(valStr)) { pairMatched = true; break; }

                // Recurse into nested objects
                if (prop.Value is System.Text.Json.Nodes.JsonObject nested)
                {
                    if (MatchJsonObject(nested, new List<(Regex, Regex)> { (keyRx, valRx) }))
                    { pairMatched = true; break; }
                }
            }
            if (!pairMatched) return false; // AND: all pairs must match
        }
        return true;
    }

    /// <summary>Split pseudo-JSON pattern pairs by commas (respecting quotes).</summary>
    static List<string> SplitJsonPatternPairs(string inner)
    {
        var pairs = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;
        for (int i = 0; i < inner.Length; i++)
        {
            if (inner[i] == '"' && (i == 0 || inner[i - 1] != '\\')) { inQuotes = !inQuotes; current.Append(inner[i]); }
            else if (inner[i] == ',' && !inQuotes) { pairs.Add(current.ToString()); current.Clear(); }
            else current.Append(inner[i]);
        }
        if (current.Length > 0) pairs.Add(current.ToString());
        return pairs;
    }

    // ── file glob ──────────────────────────────────────────────────────────
}
