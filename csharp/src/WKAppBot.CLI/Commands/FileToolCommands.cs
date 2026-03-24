using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

// partial class: wkappbot file <subcommand> — filesystem tools for loop agents
// file read <path> [--offset N] [--limit N] [--encoding N]
// file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr]
// file edit <path> <old> <new> [--replace-all] [--encoding N]
// file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N] [--encoding N]
// file glob <pattern> [--path dir]
// file undo --at <timestamp> [--path dir] [--list] [--dry-run]
internal partial class Program
{
    static int FileCommand(string[] args)
    {
        if (args.Length == 0) return FileToolUsage();
        return args[0].ToLowerInvariant() switch
        {
            "read"                   => FileReadCommand(args[1..]),
            "read-pdf"               => FileReadPdfCommand(args[1..]),
            "write"                  => FileWriteCommand(args[1..]),
            "edit"                   => FileEditCommand(args[1..]),
            "undo"                   => FileUndoCommand(args[1..]),
            "grep"                   => FileGrepCommand(args[1..]),
            // json-grep removed — use "grap ... --json" instead (unified with logcat ecosystem)
            "glob"                   => FileGlobCommand(args[1..]),
            "--help" or "-h" or "help" => FileToolUsage(),
            _ => Error($"Unknown file subcommand: {args[0]}")
        };
    }

    static string FileDefaultDir() =>
        EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;

    /// <summary>
    /// Detect file encoding: --encoding flag > BOM > Encoding.Default (system ANSI codepage).
    /// On Korean Windows, Encoding.Default = CP949 automatically.
    /// </summary>
    static Encoding DetectFileEncoding(byte[] bytes, int? explicitCodepage)
    {
        if (explicitCodepage.HasValue)
            return Encoding.GetEncoding(explicitCodepage.Value);
        // BOM detection
        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            return new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
        if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE)
            return Encoding.Unicode;   // UTF-16 LE
        if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF)
            return Encoding.BigEndianUnicode; // UTF-16 BE
        // No BOM: two-stage UTF-8 detection (avoids false positives for CP949 files).
        // Stage 1 (zero-alloc): byte-scan for structural UTF-8 validity.
        //   CP949 bytes can accidentally form valid UTF-8 sequences, so stage 1 alone is
        //   insufficient — we add stage 2 to reject misidentified CP949.
        // Stage 2 (string-alloc, only if stage 1 passes): decode as UTF-8 and check for
        //   U+FFFD replacement characters. Their presence means the CP949 byte sequences
        //   happened to be structurally valid UTF-8 but decoded to garbage/replacement chars
        //   → treat as CP949.
        // Helper: return CP949 encoding, falling back to system ANSI if CP949 unavailable.
        static Encoding KoreanAnsi()
        {
            try { return Encoding.GetEncoding(949); }
            catch { return Encoding.Default; }
        }
        // Stage 0: pure ASCII — no multi-byte at all → UTF-8 (ASCII is a subset)
        bool hasHighByte = false;
        for (int bi = 0; bi < bytes.Length; bi++)
            if (bytes[bi] >= 0x80) { hasHighByte = true; break; }
        if (!hasHighByte) return new UTF8Encoding(false);       // pure ASCII → UTF-8, done

        // Stage 1: structural UTF-8 validity
        if (!IsValidUtf8NoBom(bytes)) return KoreanAnsi();     // structural errors → CP949

        // Stage 2: decode as UTF-8, then strip all known-good characters.
        // Whatever remains = suspicious. If suspicious chars dominate → CP949.
        var decoded = new UTF8Encoding(false).GetString(bytes);
        if (decoded.Contains('\uFFFD')) return KoreanAnsi();    // replacement char → not UTF-8

        // Strip known-good chars → collapse runs to single space (preserves context)
        // e.g. "abc뮤def" → " 뮤 "  (isolated = suspicious)
        //      "뮤뮤뮤"   → "뮤뮤뮤" (clustered = possibly legit rare script)
        var goodPattern = @"[\x00-\x7F"    // ASCII
          + @"\u00A0-\u024F"                // Latin Extended
          + @"\u0370-\u03FF"                // Greek
          + @"\u2000-\u2BFF"                // Punctuation, Symbols, Arrows, Box Drawing
          + @"\u3000-\u9FFF"                // CJK + Hangul Compat + Kana
          + @"\uAC00-\uD7AF"               // Hangul Syllables
          + @"\uD800-\uDFFF"               // Surrogates (emoji pairs)
          + @"\uF900-\uFAFF"               // CJK Compatibility Ideographs
          + @"\uFE00-\uFE0F"               // Variation Selectors
          + @"\uFF00-\uFFEF]+";             // Halfwidth/Fullwidth — '+' collapses runs
        var suspicious = System.Text.RegularExpressions.Regex.Replace(decoded, goodPattern, " ").Trim();

        // Any PUA (U+E000-U+F8FF) or non-characters → instant CP949
        if (System.Text.RegularExpressions.Regex.IsMatch(suspicious, @"[\uE000-\uF8FF\uFDD0-\uFDEF\uFFFE\uFFFF]"))
            return KoreanAnsi();

        // Context: isolated suspicious chars (surrounded by spaces) are more suspect
        // " X " = isolated → 2 penalty;  "XX" = clustered → 1 penalty each
        int nonAscii = decoded.Count(c => c >= 0x80);
        int suspCount = suspicious.Count(c => c != ' ');
        int isolated = 0;
        var parts = suspicious.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var p in parts)
            if (p.Length == 1) isolated++;  // single char between good text = very suspicious

        // Scoring: isolated chars count double
        int score = suspCount + isolated;
        if (nonAscii > 0 && score * 100 / nonAscii > 30)
            return KoreanAnsi();

        return new UTF8Encoding(false);                          // all checks passed → UTF-8
    }

    /// <summary>
    /// Byte-scan UTF-8 validity without allocating a string.
    /// Returns true if every multi-byte sequence is structurally valid UTF-8.
    /// Pure ASCII files (no bytes ≥ 0x80) also return true — use as UTF-8.
    /// </summary>
    static bool IsValidUtf8NoBom(byte[] b)
    {
        int i = 0;
        while (i < b.Length)
        {
            byte c = b[i++];
            if (c < 0x80) continue;                         // ASCII — ok
            int extra;
            if      (c < 0xC2) return false;                // invalid leader (0x80-0xBF / 0xC0-0xC1)
            else if (c < 0xE0) extra = 1;
            else if (c < 0xF0) extra = 2;
            else if (c < 0xF5) extra = 3;
            else               return false;                // > U+10FFFF
            for (int j = 0; j < extra; j++)
                if (i >= b.Length || (b[i++] & 0xC0) != 0x80) return false; // bad continuation
        }
        return true;
    }

    // ── file read ──────────────────────────────────────────────────────────
    static int FileReadCommand(string[] args)
    {
        string? path = null;
        int offset = 0;
        int limit  = 2000;
        int? encoding = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--offset" && i + 1 < args.Length) int.TryParse(args[++i], out offset);
            else if (args[i] == "--limit" && i + 1 < args.Length) int.TryParse(args[++i], out limit);
            else if (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if (!args[i].StartsWith("--")) path = args[i];
        }

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

            Console.WriteLine($"[FILE] {path} ({total} lines, showing {start + 1}-{end})");
            for (int i = start; i < end; i++)
                Console.WriteLine($"{i + 1,6}\t{lines[i]}");

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
            if      (args[i] == "--path"  && i + 1 < args.Length) searchRoot = args[++i];
            else if (args[i] == "--type"  && i + 1 < args.Length) typeFilter = args[++i].TrimStart('.');
            else if (args[i] is "-i" or "--ignore-case")           ignoreCase = true;
            else if ((args[i] is "-C" or "--context") && i + 1 < args.Length) int.TryParse(args[++i], out context);
            else if (args[i] == "--max"   && i + 1 < args.Length) int.TryParse(args[++i], out maxResults);
            else if (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if (!args[i].StartsWith("-"))                     pattern = args[i];
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
                            Console.WriteLine($"→ {ci + 1,5}│ {lines[ci]}");
                        else         // context line
                            Console.WriteLine($"  {ci + 1,5}│ {lines[ci]}");
                    }
                    lastCtxEnd = ctxEnd;

                    matchCount++;
                }
            }

            if (matchCount == 0)
                Console.WriteLine($"[GREP] No matches for: {pattern}");
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
                        var display = line.Length > 200 ? line[..200] + "…" : line;
                        Console.WriteLine($"→ {lineNo,5}│ {display}");
                        matchCount++;
                    }
                }
            }
            catch (Exception ex) { Console.Error.WriteLine($"[JSON-GREP] Error reading {file}: {ex.Message}"); }
        }

        if (matchCount == 0)
            Console.WriteLine($"[JSON-GREP] No matches for: {pattern}");
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
    static int FileGlobCommand(string[] args)
    {
        string? pattern = null;
        string  root    = FileDefaultDir();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--path" && i + 1 < args.Length) root = args[++i];
            else if (!args[i].StartsWith("--")) pattern = args[i];
        }

        if (string.IsNullOrEmpty(pattern))
            return Error("Usage: file glob <pattern> [--path dir]");
        if (!Directory.Exists(root))
            return Error($"Directory not found: {root}");

        try
        {
            // ';' in path segments = OR expansion: "src/가;나;다/*.cs" → 3 patterns
            var results = ExpandGlobSegments(pattern)
                .SelectMany(p => GlobFiles(root, p))
                .Distinct().OrderBy(f => f).ToList();
            if (results.Count == 0)
            {
                Console.WriteLine($"[GLOB] No files matching: {pattern}");
                return 0;
            }
            foreach (var f in results)
                Console.WriteLine(f);
            Console.WriteLine($"[GLOB] {results.Count} file(s)");
            return 0;
        }
        catch (Exception ex) { return Error($"Glob failed: {ex.Message}"); }
    }

    /// <summary>Simple glob with ** support. e.g. "**/*.cs", "src/**/*.ts", "*.json"</summary>
    static List<string> GlobFiles(string root, string pattern)
    {
        // Normalize separators
        pattern = pattern.Replace('\\', '/');
        bool recursive = pattern.Contains("**");

        string fileExt;
        string searchDir = root;

        if (recursive)
        {
            // Split on "**/" — everything before is a subdir, everything after is the file pattern
            var idx = pattern.IndexOf("**/", StringComparison.Ordinal);
            var before = pattern[..idx].TrimEnd('/');
            fileExt = pattern[(idx + 3)..]; // after "**/"
            if (!string.IsNullOrEmpty(before))
                searchDir = Path.Combine(root, before.Replace('/', Path.DirectorySeparatorChar));
        }
        else
        {
            // Non-recursive: handle subdir prefix in pattern (e.g. "src/*.cs")
            var slash = pattern.LastIndexOf('/');
            if (slash >= 0)
            {
                searchDir = Path.Combine(root, pattern[..slash].Replace('/', Path.DirectorySeparatorChar));
                fileExt   = pattern[(slash + 1)..];
            }
            else
                fileExt = pattern;
        }

        if (!Directory.Exists(searchDir)) return [];

        var option = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        return Directory.EnumerateFiles(searchDir, fileExt, option)
                        .OrderBy(f => f)
                        .ToList();
    }

    // ── file write ─────────────────────────────────────────────────────────
    // Write Unicode content to a file, re-encoding to target charset.
    // Content sources (pick one):
    //   --stdin        read from stdin until EOF (pipe mode)
    //   --text "..."   inline text content
    //   --file <src>   read from source file (auto-detect encoding), write to target
    // Use case: Claude edits CP949 Korean source files without byte-level splicing.
    //   wkappbot file write legacy.cpp --encoding 949 --stdin  < edited_unicode.txt
    //   wkappbot file write legacy.cpp --encoding 949 --file tmp_edit.txt
    static int FileWriteCommand(string[] args)
    {
        string? path     = null;
        int?    encoding = null;
        string? text     = null;
        string? srcFile  = null;
        bool    stdin    = false;
        bool    append   = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if (args[i] == "--text"     && i + 1 < args.Length) text    = args[++i];
            else if (args[i] == "--file"     && i + 1 < args.Length) srcFile = args[++i];
            else if (args[i] == "--stdin")  stdin  = true;
            else if (args[i] == "--append") append = true;
            else if (!args[i].StartsWith("--")) path = args[i];
        }

        if (string.IsNullOrEmpty(path))
            return Error("Usage: file write <path> [--encoding N] (--stdin | --text \"...\" | --file <src>) [--append]");

        string content;
        if (stdin)
        {
            Console.InputEncoding = Encoding.UTF8;
            content = Console.In.ReadToEnd();
        }
        else if (srcFile != null)
        {
            if (!File.Exists(srcFile)) return Error($"Source file not found: {srcFile}");
            var srcBytes = File.ReadAllBytes(srcFile);
            content = DetectFileEncoding(srcBytes, null).GetString(srcBytes);
        }
        else if (text != null)
        {
            content = text;
        }
        else
        {
            return Error("Specify content via --stdin, --text, or --file");
        }

        try
        {
            var enc = encoding.HasValue ? Encoding.GetEncoding(encoding.Value) : new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            var bytes = enc.GetBytes(content);
            if (append)
                using (var fs = File.Open(path, FileMode.Append)) fs.Write(bytes, 0, bytes.Length);
            else
                File.WriteAllBytes(path, bytes);

            var encName = encoding.HasValue ? $"CP{encoding}" : "UTF-8";
            Console.WriteLine($"[FILE] write OK → {path} ({bytes.Length} bytes, {encName})");
            return 0;
        }
        catch (Exception ex) { return Error($"Write failed: {ex.Message}"); }
    }

    // ── file read-pdf ──────────────────────────────────────────────────────
    // Extracts text from a PDF file page by page using PdfPig (pure .NET).
    // --ocr: also OCR-renders each page via Windows.Data.Pdf + Windows OCR,
    //        appending [+OCR: ...] for content present in OCR but missing from PdfPig
    //        (catches math formula variables, diagram labels, etc.)
    static int FileReadPdfCommand(string[] args)
    {
        string? path     = null;
        int     pgFrom   = 1;
        int     pgTo     = int.MaxValue;
        int     maxChars = 200_000;
        bool    useOcr   = false;
        bool    deepOcr  = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--pages" && i + 1 < args.Length)
            {
                var pages = args[++i];
                var dash  = pages.IndexOf('-');
                if (dash > 0 &&
                    int.TryParse(pages[..dash],       out int a) &&
                    int.TryParse(pages[(dash + 1)..], out int b))
                { pgFrom = a; pgTo = b; }
                else if (int.TryParse(pages, out int single))
                { pgFrom = pgTo = single; }
                else return Error($"Invalid --pages value: {pages} (expected N or N-M)");
            }
            else if (args[i] == "--max-chars" && i + 1 < args.Length)
                int.TryParse(args[++i], out maxChars);
            else if (args[i] == "--ocr")
                useOcr = true;
            else if (args[i] == "--ocr-deep")
                useOcr = deepOcr = true;
            else if (!args[i].StartsWith("--"))
                path = args[i];
        }

        if (string.IsNullOrEmpty(path))
            return Error("Usage: file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr] [--ocr-deep]");

        // NFD fallback: NTFS may store Korean filenames in NFD (decomposed jamo) while CLI passes NFC.
        if (!File.Exists(path))
        {
            var nfd = path.Normalize(NormalizationForm.FormD);
            if (File.Exists(nfd)) path = nfd;
            else return Error($"File not found: {path}");
        }

        return Task.Run(() => FileReadPdfAsync(path, pgFrom, pgTo, maxChars, useOcr, deepOcr)).GetAwaiter().GetResult();
    }

    static async Task<int> FileReadPdfAsync(string path, int pgFrom, int pgTo, int maxChars, bool useOcr, bool deepOcr = false)
    {
        try
        {
            using var pdf = PdfDocument.Open(path);
            int totalPages = pdf.NumberOfPages;
            int from = Math.Max(1, pgFrom);
            int to   = Math.Min(totalPages, pgTo == int.MaxValue ? totalPages : pgTo);

            Console.WriteLine($"[PDF] {path} ({totalPages} pages, showing {from}-{to}){(deepOcr ? " [+OCR-DEEP]" : useOcr ? " [+OCR]" : "")}");

            // Load Windows.Data.Pdf for rendering (OCR mode)
            Windows.Data.Pdf.PdfDocument? winPdf = null;
            SimpleOcrAnalyzer? ocr = null;
            if (useOcr)
            {
                try
                {
                    var absPath = Path.GetFullPath(path).Replace('/', '\\');
                    var storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(absPath);
                    winPdf = await Windows.Data.Pdf.PdfDocument.LoadFromFileAsync(storageFile);
                    ocr = new SimpleOcrAnalyzer("ko", "en-US");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PDF] OCR init failed: {ex.Message} — continuing without OCR");
                    useOcr = false;
                }
            }

            int  chars     = 0;
            bool truncated = false;

            for (int p = from; p <= to && !truncated; p++)
            {
                var page     = pdf.GetPage(p);
                var words    = page.GetWords().ToList();
                var pageText = words.Count > 0
                    ? string.Join(" ", words.Select(w => w.Text))
                    : page.Text;

                // OCR additions: render page → full-page OCR + deep-scan OCR → diff vs PdfPig
                // Garble detection: if PdfPig output has >20% '?' chars (custom font encoding failure),
                // OCR text replaces the page text entirely instead of being appended as additions.
                string ocrAdditions = "";
                if (useOcr && winPdf != null && ocr != null)
                {
                    try
                    {
                        var pdfPage = winPdf.GetPage((uint)(p - 1));
                        using var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                        var opts = new Windows.Data.Pdf.PdfPageRenderOptions { DestinationWidth = 1700 };
                        await pdfPage.RenderToStreamAsync(stream, opts);
                        stream.Seek(0);
                        using var bmp = new System.Drawing.Bitmap(stream.AsStream());

                        // Pass 1: full-page OCR (fast, catches normal text + word coords)
                        Console.Write($"[PDF+OCR] p{p} fullpage...");
                        Console.Out.Flush();
                        var pageOcr = await ocr.RecognizeAll(bmp);
                        Console.WriteLine($" {pageOcr.Words.Count} words");
                        var combined = pageOcr.FullText;
                        // Pass 2: coverage-guided fallback — only for uncovered pixel regions
                        if (deepOcr)
                        {
                            int deepHits = 0;
                            Console.Write($"[PDF+OCR] p{p} deep...");
                            Console.Out.Flush();
                            var deepText = await DeepScanOcrAsync(bmp, ocr, pageOcr, t =>
                            {
                                if (deepHits == 0) Console.WriteLine();
                                Console.WriteLine($"[OCR-DEEP] → {t}");
                                deepHits++;
                            });
                            if (deepHits == 0) Console.WriteLine(" nothing new");
                            combined += " " + deepText;
                        }

                        // Garble check: PdfPig custom-font failure → '?' ratio > 20%
                        // In this case OCR is the ground truth; replace pageText entirely.
                        bool pdfGarbled = pageText.Length > 10 &&
                            (double)pageText.Count(c => c == '?') / pageText.Length > 0.20;
                        if (pdfGarbled)
                        {
                            Console.WriteLine($"[PDF+OCR] p{p} PdfPig garbled (custom font) — using OCR as primary text");
                            pageText = combined.Trim();
                        }
                        else
                        {
                            ocrAdditions = ComputePdfOcrAdditions(pageText, combined);
                        }
                    }
                    catch (Exception ocrEx) { Console.WriteLine($"[PDF+OCR] page {p} error: {ocrEx.GetType().Name}: {ocrEx.Message}"); }
                }

                Console.WriteLine($"\n--- Page {p} ---");

                int remaining = maxChars - chars;
                var output = pageText;
                if (output.Length > remaining)
                {
                    Console.Write(output[..remaining]);
                    Console.WriteLine($"\n... (truncated at {maxChars} chars — use --max-chars to increase)");
                    truncated = true;
                }
                else
                {
                    Console.Write(output);
                    chars += output.Length;
                }

                if (!string.IsNullOrWhiteSpace(ocrAdditions) && !truncated)
                {
                    Console.WriteLine($"\n[+OCR: {ocrAdditions}]");
                    chars += ocrAdditions.Length;
                }
            }

            ocr?.Dispose();
            if (!truncated)
                Console.WriteLine($"\n[PDF] {chars} chars extracted from {to - from + 1} page(s)");

            return 0;
        }
        catch (Exception ex) { return Error($"PDF read failed: {ex.Message}"); }
    }

    // Compare PdfPig text vs OCR text — return OCR-only lines (content PdfPig missed).
    // Strategy: for each OCR line, if >40% of its words are absent from pdfText → include as addition.
    static string ComputePdfOcrAdditions(string pdfText, string ocrText)
    {
        if (string.IsNullOrWhiteSpace(ocrText)) return "";

        static string Norm(string w) => w.ToLower().Trim('.', ',', '(', ')', ':', ';', '「', '」', '『', '』');

        var pdfWords = new HashSet<string>(
            pdfText.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(Norm).Where(w => w.Length >= 2),
            StringComparer.OrdinalIgnoreCase);

        var additions = new List<string>();
        foreach (var line in ocrText.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = line.Trim();
            if (trimmed.Length < 3) continue;
            var lineWords = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (lineWords.Length == 0) continue;
            int newCount = lineWords.Count(w => !pdfWords.Contains(Norm(w)));
            double ratio = (double)newCount / lineWords.Length;
            // Include line if most of it is novel AND has at least 2 new words
            if (ratio >= 0.5 && newCount >= 2)
                additions.Add(trimmed);
        }

        return additions.Count == 0 ? "" : string.Join(" | ", additions);
    }

    // ── file undo ─────────────────────────────────────────────────────────
    /// <summary>
    /// file undo --at &lt;timestamp&gt; [--path dir] [--list] [--dry-run]
    /// Batch-restore .bak files by timestamp prefix.
    /// Searches current dir (or --path) recursively for *.bak-{timestamp}*.txt files,
    /// strips the .bak-YYYYMMDD-HHmmss.fff.txt suffix to find the original path,
    /// and copies backup → original.
    ///
    /// Examples:
    ///   file undo --at 20260320-1126        # restore all edits from 11:26
    ///   file undo --at 20260320-112611.500  # exact millisecond match
    ///   file undo --at 20260320-1126 --list # just show what would be restored
    /// </summary>
    static int FileUndoCommand(string[] args)
    {
        string? atPrefix = null;
        string? searchPath = null;
        bool listOnly = false;
        bool dryRun = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--at" && i + 1 < args.Length) { atPrefix = args[++i]; continue; }
            if (args[i] == "--path" && i + 1 < args.Length) { searchPath = args[++i]; continue; }
            if (args[i] == "--list") { listOnly = true; continue; }
            if (args[i] == "--dry-run") { dryRun = true; continue; }
        }

        if (string.IsNullOrEmpty(atPrefix))
        {
            Console.WriteLine("Usage: wkappbot file undo --at <timestamp> [--path dir] [--list] [--dry-run]");
            Console.WriteLine("  --at     timestamp prefix: YYYYMMDD-HHmmss or partial (e.g. 20260320-1126)");
            Console.WriteLine("  --path   search directory (default: caller CWD)");
            Console.WriteLine("  --list   show matching backups without restoring");
            Console.WriteLine("  --dry-run  show what would be restored without writing");
            return 1;
        }

        var baseDir = searchPath ?? FileDefaultDir();
        if (!Directory.Exists(baseDir))
        {
            Console.WriteLine($"[file undo] directory not found: {baseDir}");
            return 1;
        }

        // Search recursively for .bak-{atPrefix}*.txt files
        var bakPattern = $".bak-{atPrefix}";
        var bakFiles = Directory.EnumerateFiles(baseDir, "*.txt", SearchOption.AllDirectories)
            .Where(f => Path.GetFileName(f).Contains(bakPattern))
            .OrderBy(f => f)
            .ToList();

        if (bakFiles.Count == 0)
        {
            Console.WriteLine($"[file undo] no backups found matching .bak-{atPrefix}* in {baseDir}");
            return 1;
        }

        Console.WriteLine($"[file undo] found {bakFiles.Count} backup(s) matching .bak-{atPrefix}*");

        // Parse each backup → derive original file path
        // Backup format: {original}.bak-YYYYMMDD-HHmmss.fff.txt
        var restorePairs = new List<(string backup, string original)>();
        foreach (var bak in bakFiles)
        {
            var fileName = bak;
            // Find ".bak-" to split: everything before is the original path
            var bakIdx = fileName.LastIndexOf(".bak-", StringComparison.Ordinal);
            if (bakIdx < 0) continue;
            var originalPath = fileName[..bakIdx];
            restorePairs.Add((bak, originalPath));
        }

        if (restorePairs.Count == 0)
        {
            Console.WriteLine("[file undo] could not parse backup filenames");
            return 1;
        }

        // Display matches
        foreach (var (bak, orig) in restorePairs)
        {
            var bakName = Path.GetFileName(bak);
            var origRel = Path.GetRelativePath(baseDir, orig);
            if (listOnly || dryRun)
                Console.WriteLine($"  {bakName} → {origRel}");
            else
                Console.WriteLine($"  → {origRel}");
        }

        if (listOnly) return 0;
        if (dryRun)
        {
            Console.WriteLine($"[file undo] dry-run: {restorePairs.Count} file(s) would be restored");
            return 0;
        }

        // Restore: backup current → copy bak → original
        int restored = 0;
        int errors = 0;
        foreach (var (bak, orig) in restorePairs)
        {
            try
            {
                // Save current file before undo (undo-of-undo safety net)
                if (File.Exists(orig))
                {
                    var undoTs = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
                    var undoBak = $"{orig}.undo-{undoTs}.txt";
                    File.Copy(orig, undoBak);
                    Console.WriteLine($"  [backup] {Path.GetFileName(undoBak)}");
                }
                File.Copy(bak, orig, overwrite: true);
                restored++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [ERROR] {Path.GetFileName(orig)}: {ex.Message}");
                errors++;
            }
        }

        Console.WriteLine($"[file undo] restored {restored} file(s)" +
            (errors > 0 ? $", {errors} error(s)" : ""));
        return errors > 0 ? 1 : 0;
    }

    // In Eye pipe mode Console.Error is not forwarded to the Launcher; use Console.Out instead.
    static void ErrOut(string? msg) {
        if (Program.RunningInEye) Console.WriteLine(msg);
        else Console.Error.WriteLine(msg);
    }

    // ── file edit ──────────────────────────────────────────────────────────
    /// <summary>
    /// file edit &lt;old_string&gt; &lt;new_string&gt; &lt;path&gt;... [--replace-all] [--regex] [--i-really-want-lossy-encoding] [--encoding N] [--context N]
    /// Same interface as Claude Code Edit tool. Auto-detects encoding (BOM → system ANSI/CP949).
    /// Fails if old_string not found (exact match). Preserves original encoding on save.
    /// </summary>
    static int FileEditCommand(string[] args)
    {
        var positional = new List<string>();
        bool replaceAll = false;
        bool useRegex = false;
        bool force = false;
        bool backup = true; // backup is ON by default; use --i-really-want-no-backup to skip
        int? encoding = null;
        int context = 1; // extra lines beyond indent boundary (--context N)
        int tabSize = 4; // tab width for indent tracking (--tab-size N)
        int indentContext = 7; // max lines per indent block direction (--indent-context N, default 7)

        string? oldFile = null, newFile = null;
        string? undoTimestamp = null; // --undo <timestamp>: restore backup first, then edit

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--replace-all") { replaceAll = true; continue; }
            if (args[i] == "--regex")       { useRegex   = true; continue; }
            if (args[i] == "--i-really-want-lossy-encoding") { force  = true; continue; }
            if (args[i] == "--i-really-want-no-backup")            { backup = false; continue; }
            if (args[i] == "--encoding" && i + 1 < args.Length) { encoding = int.Parse(args[++i]); continue; }
            if (args[i] == "--context"  && i + 1 < args.Length) { int.TryParse(args[++i], out context); continue; }
            if (args[i] == "--tab-size" && i + 1 < args.Length) { int.TryParse(args[++i], out tabSize); if (tabSize < 1) tabSize = 4; continue; }
            if (args[i] == "--indent-context" && i + 1 < args.Length) { int.TryParse(args[++i], out indentContext); if (indentContext < 1) indentContext = 7; continue; }
            // --old-file/--new-file: read old/new strings from file (avoids bash Korean encoding issues)
            if (args[i] == "--old-file" && i + 1 < args.Length) { oldFile = args[++i]; continue; }
            if (args[i] == "--new-file" && i + 1 < args.Length) { newFile = args[++i]; continue; }
            // --undo <timestamp>: restore file from .bak-{timestamp} backup before applying edit
            if (args[i] == "--undo" && i + 1 < args.Length) { undoTimestamp = args[++i]; continue; }
            positional.Add(args[i]);
        }

        // --old-file/--new-file mode: read strings from files, remaining positionals are target paths
        if (oldFile != null || newFile != null)
        {
            if (oldFile == null || newFile == null)
                return Error("[file edit] --old-file and --new-file must both be specified");
            if (!File.Exists(oldFile)) return Error($"[file edit] --old-file not found: {oldFile}");
            if (!File.Exists(newFile)) return Error($"[file edit] --new-file not found: {newFile}");
            var oldContent = File.ReadAllText(oldFile);
            var newContent = File.ReadAllText(newFile);
            // Normalize line endings: bash heredoc/echo creates LF, but Windows source files use CRLF.
            // Strip CR from old/new so matching works regardless of source line endings.
            // Target file's original line endings are preserved in the output.
            oldContent = oldContent.Replace("\r\n", "\n").Replace("\r", "");
            newContent = newContent.Replace("\r\n", "\n").Replace("\r", "");
            // Prepend to positional: old, new, then paths remain
            positional.InsertRange(0, new[] { oldContent, newContent });
        }

        // sed/grep-style: first two positional args are old_string and new_string; rest are file paths/globs
        if (positional.Count < 3)
        {
            Console.WriteLine("Usage: wkappbot file edit <old_string> <new_string> <path>... [--replace-all] [--regex]");
            Console.WriteLine("       wkappbot file edit --old-file old.txt --new-file new.txt <path>... [--replace-all]");
            Console.WriteLine("       wkappbot file edit --undo <timestamp> <old> <new> <path>  # restore backup, then edit");
            return 1;
        }

        string oldStr = oldFile != null ? positional[0] : UnescapeCString(positional[0]);
        string newStr = newFile != null ? positional[1] : UnescapeCString(positional[1]);
        bool fromFile = oldFile != null; // track for line-ending normalization in validate
        var pathPatterns = positional[2..];

        // Expand globs for each path pattern
        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        var paths = new List<string>();
        foreach (var pat in pathPatterns)
        {
            var full = Path.IsPathRooted(pat) ? pat : Path.Combine(cwd, pat);
            var dir  = Path.GetDirectoryName(full) ?? cwd;
            var glob = Path.GetFileName(full);
            if (glob.Contains('*') || glob.Contains('?'))
            {
                var matched = Directory.Exists(dir) ? Directory.GetFiles(dir, glob) : [];
                if (matched.Length == 0) ErrOut($"[file edit] no files matched: {pat}");
                paths.AddRange(matched);
            }
            else { paths.Add(full); }
        }
        if (paths.Count == 0) return Error("[file edit] no target files resolved");
        bool multi = paths.Count > 1;

        // --undo: restore each file from its .bak-{timestamp} backup before editing
        if (undoTimestamp != null)
        {
            var bakPattern = $".bak-{undoTimestamp}";
            int restored = 0;
            foreach (var path in paths)
            {
                // Search for backup: {path}.bak-{timestamp}*.txt
                var dir = Path.GetDirectoryName(path) ?? ".";
                var baseName = Path.GetFileName(path);
                var bakFile = Directory.Exists(dir)
                    ? Directory.EnumerateFiles(dir, $"{baseName}.bak-*", SearchOption.TopDirectoryOnly)
                        .Where(f => Path.GetFileName(f).Contains(bakPattern))
                        .OrderByDescending(f => f) // latest matching backup
                        .FirstOrDefault()
                    : null;

                if (bakFile == null)
                {
                    Console.Error.WriteLine($"[file edit --undo] no backup matching {baseName}{bakPattern}* in {dir}");
                    return 1;
                }

                // Safety: backup current file before undo (undo-of-undo)
                if (File.Exists(path))
                {
                    var undoTs = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
                    var undoBak = $"{path}.undo-{undoTs}.txt";
                    File.Copy(path, undoBak);
                    Console.WriteLine($"[file edit --undo] safety backup → {Path.GetFileName(undoBak)}");
                }

                File.Copy(bakFile, path, overwrite: true);
                Console.WriteLine($"[file edit --undo] restored {Path.GetFileName(path)} from {Path.GetFileName(bakFile)}");
                restored++;
            }
            Console.WriteLine($"[file edit --undo] {restored} file(s) restored, now applying edit...");
        }

        // All-or-nothing for multi-file edits: validate ALL files first, then write all.
        // Phase 1: validate (dry-run) — produces edited content for each file but does not write
        var pending = new List<FileEditPlan>();
        foreach (var path in paths)
        {
            // --old-file mode: adapt line endings to match target file
            var effOld = oldStr;
            var effNew = newStr;
            if (fromFile)
            {
                var sample = File.Exists(path) ? File.ReadAllText(path) : "";
                bool targetCrlf = sample.Contains("\r\n");
                if (targetCrlf && !effOld.Contains("\r\n"))
                {
                    effOld = effOld.Replace("\n", "\r\n");
                    effNew = effNew.Replace("\n", "\r\n");
                }
            }
            var (ok, plan, errMsg) = FileEditValidate(path, effOld, effNew, replaceAll, useRegex, force, encoding);
            if (!ok) { ErrOut(errMsg); if (multi) return Error("[file edit] validation failed — no files written"); else return 1; }
            pending.Add(plan!);
        }

        // Phase 2: write all (backups first if enabled)
        int totalEdited = 0;
        foreach (var plan in pending)
        {
            if (FileEditWrite(plan, backup, context, multi, tabSize, indentContext) == 0) totalEdited++;
        }

        if (multi) Console.WriteLine($"[file edit] {totalEdited}/{paths.Count} file(s) edited");
        return totalEdited == paths.Count ? 0 : 1;
    }

    record FileEditPlan(string Path, byte[] OutBytes, Encoding Enc, int Count,
        List<int> MatchPositions, string OrigText, string Result, string OldStr, string NewStr,
        bool HasUnmappable);

    /// <summary>Validate a file edit without writing — returns plan on success or error message.</summary>
    static (bool ok, FileEditPlan? plan, string? errMsg) FileEditValidate(
        string path, string oldStr, string newStr, bool replaceAll, bool useRegex, bool force, int? encoding)
    {
        if (!File.Exists(path))
            return (false, null, $"[file edit] not found: {path}");

        var bytes = File.ReadAllBytes(path);
        var enc   = DetectFileEncoding(bytes, encoding);
        var text  = enc.GetString(bytes);

        var matchPositions = new List<int>();
        int count = 0;
        string result;

        // Helper: build "not found" error with encoding hint when Korean chars detected
        string NotFoundError(string file)
        {
            var msg = $"[file edit] old_string not found in {file}";
            bool hasKorean = oldStr.Any(c => c >= 0xAC00 && c <= 0xD7AF); // Hangul syllables
            bool hasBadBytes = oldStr.Any(c => c == 0xFFFD || (c >= 0x80 && c < 0xA0)); // replacement char or high bytes
            if (hasKorean || hasBadBytes)
            {
                msg += $"\n  ⚠ Encoding mismatch? File={enc.WebName}";
                if (hasBadBytes)
                    msg += ", old_string contains corrupted bytes (U+FFFD or high-byte)";
                msg += "\n  💡 bash corrupts Korean CLI args — use --old-file instead:";
                msg += "\n     echo '원본텍스트' > /tmp/old.txt && wkappbot file edit --old-file /tmp/old.txt --new-file /tmp/new.txt <path>";
                msg += "\n  💡 dry-run (search only): wkappbot file edit '같은텍스트' '같은텍스트' <path>";
            }
            return msg;
        }

        if (useRegex)
        {
            Regex rx;
            try { rx = new Regex(oldStr, RegexOptions.Multiline); }
            catch (Exception ex) { return (false, null, $"[file edit] invalid regex: {ex.Message}"); }
            var matches = rx.Matches(text);
            count = matches.Count;
            if (count == 0) return (false, null, $"[file edit] regex pattern not found in {Path.GetFileName(path)}");
            if (!replaceAll && count > 1) return (false, null, $"[file edit] regex matches {count} locations — use --replace-all or narrow the pattern");
            foreach (Match m in matches) matchPositions.Add(m.Index);
            result = rx.Replace(text, newStr);
        }
        else if (replaceAll)
        {
            int pos = 0;
            while ((pos = text.IndexOf(oldStr, pos, StringComparison.Ordinal)) >= 0)
            { matchPositions.Add(pos); count++; pos += oldStr.Length; }
            if (count == 0) return (false, null, NotFoundError(Path.GetFileName(path)));
            result = text.Replace(oldStr, newStr, StringComparison.Ordinal);
        }
        else
        {
            int idx = text.IndexOf(oldStr, StringComparison.Ordinal);
            if (idx < 0) return (false, null, NotFoundError(Path.GetFileName(path)));
            int second = text.IndexOf(oldStr, idx + oldStr.Length, StringComparison.Ordinal);
            if (second >= 0) return (false, null, $"[file edit] old_string matches multiple locations — use --replace-all or provide more context");
            matchPositions.Add(idx);
            result = text[..idx] + newStr + text[(idx + oldStr.Length)..];
            count = 1;
        }

        // Check for unmappable characters via roundtrip.
        // Use '?' (ASCII 0x3F) as fallback — always encodable in any codepage.
        // U+FFFD would recurse if CP949 itself can't encode it.
        var safeEnc = Encoding.GetEncoding(enc.CodePage,
            new EncoderReplacementFallback("?"), DecoderFallback.ReplacementFallback);
        var outBytes = safeEnc.GetBytes(result);
        var roundtrip = safeEnc.GetString(outBytes);
        int unmappable = 0;
        for (int ci = 0; ci < result.Length; ci++)
            if (ci < roundtrip.Length && roundtrip[ci] == '?' && result[ci] != '?') unmappable++;
        if (unmappable > 0 && !force)
            return (false, null, $"[file edit] {unmappable} character(s) in new_string cannot be encoded in {enc.WebName} — file NOT written. Use --i-really-want-lossy-encoding to allow '?' substitution, or --encoding to target a different charset.");

        return (true, new FileEditPlan(path, outBytes, enc, count, matchPositions, text, result, oldStr, newStr, unmappable > 0), null);
    }

    /// <summary>Write a validated plan to disk (backup + write + print context). Returns 0 on success.</summary>
    static int FileEditWrite(FileEditPlan plan, bool backup, int context, bool multi, int tabSize = 4, int indentContext = 7)
    {
        if (plan.HasUnmappable)
            ErrOut($"[file edit] WARNING(--i-really-want-lossy-encoding): character(s) replaced with '?' in {plan.Enc.WebName} — data loss accepted");

        // Print file path first so AI always sees which file is being edited
        Console.WriteLine($"[file edit] {plan.Path}");

        // No actual change → skip backup + write, show context only (search mode)
        bool noChange = plan.OldStr == plan.NewStr;
        if (noChange)
        {
            Console.WriteLine($"[file edit] {plan.Count} match(es), no change — skip backup & write (search only)");
        }
        else if (backup)
        {
            // Timestamp in backup name = original file's LastWriteTime (ms precision) — "파일타임표준"
            var origMtime = File.GetLastWriteTime(plan.Path);
            var ts = origMtime.ToString("yyyyMMdd-HHmmss.fff");
            var bakPath = $"{plan.Path}.bak-{ts}.txt"; // .txt so build systems ignore it
            try
            {
                File.Copy(plan.Path, bakPath, overwrite: true);
                File.SetCreationTime(bakPath, File.GetCreationTime(plan.Path));
                File.SetLastWriteTime(bakPath, File.GetLastWriteTime(plan.Path));
                Console.WriteLine($"[file edit] backup → {bakPath}");
            }
            catch (Exception ex)
            {
                return Error($"[file edit] backup failed ({ex.Message}) — file NOT written. Use --i-really-want-no-backup to skip backup.");
            }
        }

        if (!noChange)
        {
            // Sanity check: re-detect encoding of output bytes to catch accidental encoding change
            var verifyEnc = DetectFileEncoding(plan.OutBytes, null);
            if (verifyEnc.CodePage != plan.Enc.CodePage)
                Console.Error.WriteLine($"[file edit] WARNING: output encoding changed! {plan.Enc.WebName} → {verifyEnc.WebName} — possible corruption");

            // WriteAllBytes uses FileMode.Create (truncate-in-place) — no delete event fired
            File.WriteAllBytes(plan.Path, plan.OutBytes);
            Console.WriteLine($"[file edit] {plan.Count} replacement(s) — encoding={plan.Enc.WebName}");
        }

        // ── Context output: indent-based block expansion + N extra lines ──
        // context = N means: expand to indent boundary, then show N more lines beyond.
        // Default (context=1): show enclosing block + 1 line outside (method signature, etc.)
        if (plan.MatchPositions.Count > 0)
        {
            var resultLines = plan.Result.ReplaceLineEndings("\n").Split('\n');
            var newLineCount = plan.NewStr.Split('\n').Length - 1;
            var oldLineCount = plan.OldStr.Split('\n').Length - 1;
            int lineShift = 0;
            var changedRanges = new List<(int start, int end)>();
            foreach (var mpos in plan.MatchPositions)
            {
                int origLine   = plan.OrigText[..mpos].Count(c => c == '\n');
                int resultLine = origLine + lineShift;
                int resultEnd  = resultLine + newLineCount;
                changedRanges.Add((resultLine, resultEnd));
                lineShift += newLineCount - oldLineCount;
            }

            var toShow = new SortedSet<int>();
            foreach (var (start, end) in changedRanges)
            {
                // Find indent level of the match line
                int matchIndent = GetLineIndent(resultLines, tabSize, start);

                // Expand upward: while indent >= matchIndent (capped by --indent-context)
                int top = start;
                int upCount = 0;
                while (top > 0 && upCount < indentContext)
                {
                    var line = resultLines[top - 1];
                    if (line.Trim().Length > 0 && GetLineIndent(resultLines, tabSize, top - 1) < matchIndent)
                        break;
                    top--;
                    if (line.Trim().Length > 0) upCount++;
                }

                // Expand downward: while indent >= matchIndent (capped by --indent-context)
                int bot = end;
                int downCount = 0;
                while (bot < resultLines.Length - 1 && downCount < indentContext)
                {
                    var line = resultLines[bot + 1];
                    if (line.Trim().Length > 0 && GetLineIndent(resultLines, tabSize, bot + 1) < matchIndent)
                        break;
                    bot++;
                    if (line.Trim().Length > 0) downCount++;
                }

                // Add N extra lines beyond indent boundary
                top = Math.Max(0, top - context);
                bot = Math.Min(resultLines.Length - 1, bot + context);

                for (int li = top; li <= bot; li++)
                    toShow.Add(li);
            }

            var changedSet = new HashSet<int>(changedRanges.SelectMany(r => Enumerable.Range(r.start, r.end - r.start + 1)));
            // Build old lines for "before" display (← marker)
            var origLines = plan.OrigText.ReplaceLineEndings("\n").Split('\n');
            var oldLineRanges = new List<(int origStart, int origEnd)>();
            {
                var olc = plan.OldStr.Split('\n').Length - 1;
                foreach (var mpos in plan.MatchPositions)
                {
                    int origLine = plan.OrigText[..mpos].Count(c => c == '\n');
                    oldLineRanges.Add((origLine, origLine + olc));
                }
            }

            int? prev = null;
            int rangeIdx = 0;
            int lineShiftAccum = 0;
            foreach (var li in toShow)
            {
                // Skip empty lines (waste of tokens, annoys AI agents)
                if (resultLines[li].Trim().Length == 0) continue;
                if (prev.HasValue && li > prev + 1) Console.WriteLine("   ...");
                bool changed = changedSet.Contains(li);

                // Show old lines (← marker) before the first changed line of each range
                if (changed && rangeIdx < changedRanges.Count && li == changedRanges[rangeIdx].start)
                {
                    var (origStart, origEnd) = oldLineRanges[rangeIdx];
                    for (int oi = origStart; oi <= origEnd && oi < origLines.Length; oi++)
                    {
                        if (origLines[oi].Trim().Length > 0)
                            Console.WriteLine($"← {oi + 1,5}│ {origLines[oi]}");
                    }
                    rangeIdx++;
                }

                Console.WriteLine($"{(changed ? "→" : " ")} {li + 1,5}│ {resultLines[li]}");
                prev = li;
            }
        }
        return 0;
    }

    static int GetLineIndent(string[] lines, int tabSize, int idx)
    {
        if (idx < 0 || idx >= lines.Length) return 0;
        var line = lines[idx];
        int leadEnd = line.Length - line.TrimStart().Length;
        var leading = line[..leadEnd];
        var pattern = $@"( {{{tabSize},{tabSize}}}| +\t)";
        while (leading != (leading = System.Text.RegularExpressions.Regex.Replace(leading, pattern, "\t"))) ;
        return leading.Count(c => c == '\t');
    }

    /// <summary>C-style escape: \n \t \r \\ \" \0 → actual characters.</summary>
    static string UnescapeCString(string s)
    {
        var sb = new System.Text.StringBuilder(s.Length);
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '\\' && i + 1 < s.Length)
            {
                switch (s[i + 1])
                {
                    case 'n': sb.Append('\n'); i++; break;
                    case 't': sb.Append('\t'); i++; break;
                    case 'r': sb.Append('\r'); i++; break;
                    case '\\': sb.Append('\\'); i++; break;
                    case '"': sb.Append('"'); i++; break;
                    case '0': sb.Append('\0'); i++; break;
                    default: sb.Append(s[i]); break; // unknown escape → keep as-is
                }
            }
            else sb.Append(s[i]);
        }
        return sb.ToString();
    }

    // ── usage ──────────────────────────────────────────────────────────────
    static int FileToolUsage()
    {
        Console.WriteLine(@"
wkappbot file — filesystem tools (read + write + edit + PDF)

Usage:
  wkappbot file read <path> [--offset N] [--limit N] [--encoding N]
      Read file with line numbers. Encoding: --encoding > BOM > system ANSI (CP949 on KR).

  wkappbot file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr]
      Extract text from PDF (Korean/CJK safe). --pages 1-5, --max-chars 50000.
      --ocr: also OCR each page via Windows OCR; appends [+OCR: ...] for content PdfPig missed.

  wkappbot file edit <old_string> <new_string> <path>... [--replace-all] [--regex] [--i-really-want-lossy-encoding] [--encoding N] [--context N] [--backup]
      Exact-match replace. sed/grep-style: old/new first, then one or more file paths/globs.
      Auto-detects encoding (BOM > UTF-8 validity scan > system ANSI/CP949).
      Without --replace-all: fails if old_string not found or matches multiple locations.
      --regex: old_string is a .NET regex; new_string may use $1/$2 capture groups.
      --i-really-want-lossy-encoding: allow '?' substitution for chars not encodable in target charset.
      --i-really-want-no-backup: skip backup (default: backup ON — saves <file>.bak-HHMMSS.txt before writing).
      --context N: lines of context around changes (default 1; 0 = header only).

  wkappbot file write <path> [--encoding N] (--stdin | --text ""..."" | --file <src>) [--append]
      Write content to file, re-encoding to target charset.
      --encoding N  target codepage (e.g. 949 for CP949/EUC-KR)
      --stdin       read content from stdin (pipe mode)
      --text ""...""  inline content
      --file <src>  copy from source file (auto-detect source encoding)
      --append      append instead of overwrite

  wkappbot file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N] [--encoding N]
      Regex search across files.

  wkappbot file glob <pattern> [--path dir]
      Find files. Supports ** for recursive.

Examples:
  wkappbot file read legacy.cpp --encoding 949
  wkappbot file read-pdf report.pdf --pages 1-10
  wkappbot file write legacy.cpp --encoding 949 --file tmp_edit.txt
  wkappbot file write legacy.cpp --encoding 949 --stdin  < edited.txt
  wkappbot file edit legacy.cpp 'old function body' 'new function body'
  wkappbot file edit AskCommands.Entry.cs '??one-command' '- one-command'
  wkappbot file edit config.cs 'DEBUG' 'RELEASE' --replace-all
  wkappbot file grep ""static int.*Command"" --path W:/GitHub/WKAppBot/csharp --type cs
  wkappbot file glob ""**/*.cs"" --path W:/GitHub/WKAppBot/csharp/src
");
        return 1;
    }
}
