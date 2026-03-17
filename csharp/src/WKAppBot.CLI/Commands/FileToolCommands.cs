using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

// partial class: wkappbot file <subcommand> — read-only filesystem tools for loop agents
// file read <path> [--offset N] [--limit N] [--encoding N]
// file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr]
// file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N] [--encoding N]
// file glob <pattern> [--path dir]
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
            "grep"                   => FileGrepCommand(args[1..]),
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
        // Fallback: system ANSI codepage (CP949 on Korean Windows)
        return Encoding.Default;
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
                    var fileBytes = File.ReadAllBytes(file);
                    // Skip binary files: check first 8KB for null bytes
                    int checkLen = Math.Min(8192, fileBytes.Length);
                    if (Array.IndexOf(fileBytes, (byte)0, 0, checkLen) >= 0) continue;
                    var enc = DetectFileEncoding(fileBytes, encoding);
                    lines = enc.GetString(fileBytes).ReplaceLineEndings("\n").Split('\n');
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

    // ── usage ──────────────────────────────────────────────────────────────
    static int FileToolUsage()
    {
        Console.WriteLine(@"
wkappbot file — filesystem tools (read + write + PDF)

Usage:
  wkappbot file read <path> [--offset N] [--limit N] [--encoding N]
      Read file with line numbers. Encoding: --encoding > BOM > system ANSI (CP949 on KR).

  wkappbot file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr]
      Extract text from PDF (Korean/CJK safe). --pages 1-5, --max-chars 50000.
      --ocr: also OCR each page via Windows OCR; appends [+OCR: ...] for content PdfPig missed.

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
  wkappbot file grep ""static int.*Command"" --path W:/GitHub/WKAppBot/csharp --type cs
  wkappbot file glob ""**/*.cs"" --path W:/GitHub/WKAppBot/csharp/src
");
        return 1;
    }
}
