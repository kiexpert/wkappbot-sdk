using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

// partial class: wkappbot file <subcommand> — read-only filesystem tools for loop agents
// file read <path> [--offset N] [--limit N]
// file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N]
// file glob <pattern> [--path dir]
internal partial class Program
{
    static int FileCommand(string[] args)
    {
        if (args.Length == 0) return FileToolUsage();
        return args[0].ToLowerInvariant() switch
        {
            "read"                   => FileReadCommand(args[1..]),
            "grep"                   => FileGrepCommand(args[1..]),
            "glob"                   => FileGlobCommand(args[1..]),
            "--help" or "-h" or "help" => FileToolUsage(),
            _ => Error($"Unknown file subcommand: {args[0]}")
        };
    }

    static string FileDefaultDir() =>
        EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;

    // ── file read ──────────────────────────────────────────────────────────
    static int FileReadCommand(string[] args)
    {
        string? path = null;
        int offset = 0;
        int limit  = 2000;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--offset" && i + 1 < args.Length) int.TryParse(args[++i], out offset);
            else if (args[i] == "--limit" && i + 1 < args.Length) int.TryParse(args[++i], out limit);
            else if (!args[i].StartsWith("--")) path = args[i];
        }

        if (string.IsNullOrEmpty(path))
            return Error("Usage: file read <path> [--offset N] [--limit N]");
        if (!File.Exists(path))
            return Error($"File not found: {path}");

        try
        {
            var lines = File.ReadAllLines(path);
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

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--path"  && i + 1 < args.Length) searchRoot = args[++i];
            else if (args[i] == "--type"  && i + 1 < args.Length) typeFilter = args[++i].TrimStart('.');
            else if (args[i] is "-i" or "--ignore-case")           ignoreCase = true;
            else if ((args[i] is "-C" or "--context") && i + 1 < args.Length) int.TryParse(args[++i], out context);
            else if (args[i] == "--max"   && i + 1 < args.Length) int.TryParse(args[++i], out maxResults);
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
            IEnumerable<string> files;
            if (File.Exists(searchRoot))
                files = [searchRoot];
            else if (Directory.Exists(searchRoot))
            {
                var ext = typeFilter != null ? $"*.{typeFilter}" : "*";
                files = Directory.EnumerateFiles(searchRoot, ext, SearchOption.AllDirectories);
            }
            else
                return Error($"Path not found: {searchRoot}");

            int matchCount = 0, fileCount = 0;

            foreach (var file in files)
            {
                if (matchCount >= maxResults) break;

                string[] lines;
                try
                {
                    // Skip binary files: check first 8KB for null bytes
                    using (var fs = File.OpenRead(file))
                    {
                        var buf = new byte[Math.Min(8192, fs.Length)];
                        int n = fs.Read(buf, 0, buf.Length);
                        if (Array.IndexOf(buf, (byte)0, 0, n) >= 0) continue; // binary
                    }
                    lines = File.ReadAllLines(file);
                }
                catch { continue; }

                bool headerPrinted = false;
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
                    for (int ci = ctxStart; ci <= ctxEnd; ci++)
                        Console.WriteLine($"{(ci == i ? ">" : " ")} {ci + 1,5}: {lines[ci]}");
                    if (context > 0 && ctxEnd < lines.Length - 1)
                        Console.WriteLine("--");

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
            var results = GlobFiles(root, pattern);
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

    // ── usage ──────────────────────────────────────────────────────────────
    static int FileToolUsage()
    {
        Console.WriteLine(@"
wkappbot file — read-only filesystem tools

Usage:
  wkappbot file read <path> [--offset N] [--limit N]
      Read file with line numbers. Default: 2000 lines.
      --offset N  start at line N (0-based)
      --limit N   max lines

  wkappbot file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N]
      Regex search across files. Default root: caller CWD.
      --path dir  search directory (or single file)
      --type ext  filter by extension (e.g. cs, ts, py)
      -i          ignore case
      -C N        N context lines around each match
      --max N     max matches (default: 200)

  wkappbot file glob <pattern> [--path dir]
      Find files. Supports ** for recursive.
      --path dir  root directory (default: caller CWD)

Examples:
  wkappbot file read W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Program.cs --limit 50
  wkappbot file grep ""static int.*Command"" --path W:/GitHub/WKAppBot/csharp --type cs
  wkappbot file glob ""**/*.cs"" --path W:/GitHub/WKAppBot/csharp/src
  wkappbot file glob ""Commands/*.cs"" --path W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI
");
        return 1;
    }
}
