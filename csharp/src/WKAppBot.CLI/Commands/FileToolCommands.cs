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
        if (!IsValidUtf8NoBom(bytes)) return KoreanAnsi();     // structural errors → CP949
        var decoded = new UTF8Encoding(false).GetString(bytes);
        return decoded.Contains('\uFFFD')
            ? KoreanAnsi()                                      // U+FFFD in decoded → CP949
            : new UTF8Encoding(false);                          // clean decode → UTF-8
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
        int context = 1; // lines of context around each change (--context N to expand)

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--replace-all") { replaceAll = true; continue; }
            if (args[i] == "--regex")       { useRegex   = true; continue; }
            if (args[i] == "--i-really-want-lossy-encoding") { force  = true; continue; }
            if (args[i] == "--i-really-want-no-backup")            { backup = false; continue; }
            if (args[i] == "--encoding" && i + 1 < args.Length) { encoding = int.Parse(args[++i]); continue; }
            if (args[i] == "--context"  && i + 1 < args.Length) { int.TryParse(args[++i], out context); continue; }
            positional.Add(args[i]);
        }

        // sed/grep-style: first two positional args are old_string and new_string; rest are file paths/globs
        if (positional.Count < 3)
        {
            Console.WriteLine("Usage: wkappbot file edit <old_string> <new_string> <path>... [--replace-all] [--regex] [--i-really-want-lossy-encoding] [--encoding N] [--context N]");
            return 1;
        }

        string oldStr = positional[0];
        string newStr = positional[1];
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

        // All-or-nothing for multi-file edits: validate ALL files first, then write all.
        // Phase 1: validate (dry-run) — produces edited content for each file but does not write
        var pending = new List<FileEditPlan>();
        foreach (var path in paths)
        {
            var (ok, plan, errMsg) = FileEditValidate(path, oldStr, newStr, replaceAll, useRegex, force, encoding);
            if (!ok) { ErrOut(errMsg); if (multi) return Error("[file edit] validation failed — no files written"); else return 1; }
            pending.Add(plan!);
        }

        // Phase 2: write all (backups first if enabled)
        int totalEdited = 0;
        foreach (var plan in pending)
        {
            if (FileEditWrite(plan, backup, context, multi) == 0) totalEdited++;
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
            if (count == 0) return (false, null, $"[file edit] old_string not found in {Path.GetFileName(path)}");
            result = text.Replace(oldStr, newStr, StringComparison.Ordinal);
        }
        else
        {
            int idx = text.IndexOf(oldStr, StringComparison.Ordinal);
            if (idx < 0) return (false, null, $"[file edit] old_string not found in {Path.GetFileName(path)}");
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
    static int FileEditWrite(FileEditPlan plan, bool backup, int context, bool multi)
    {
        if (plan.HasUnmappable)
            ErrOut($"[file edit] WARNING(--i-really-want-lossy-encoding): character(s) replaced with '?' in {plan.Enc.WebName} — data loss accepted");

        // Print file path first so AI always sees which file is being edited
        Console.WriteLine($"[file edit] {plan.Path}");

        if (backup)
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

        // WriteAllBytes uses FileMode.Create (truncate-in-place) — no delete event fired
        File.WriteAllBytes(plan.Path, plan.OutBytes);
        Console.WriteLine($"[file edit] {plan.Count} replacement(s) — encoding={plan.Enc.WebName}");

        // ── Context output: show ±{context} lines around each change ──
        if (context > 0 && plan.MatchPositions.Count > 0)
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
                for (int li = Math.Max(0, start - context); li <= Math.Min(resultLines.Length - 1, end + context); li++)
                    toShow.Add(li);
            var changedSet = new HashSet<int>(changedRanges.SelectMany(r => Enumerable.Range(r.start, r.end - r.start + 1)));
            int? prev = null;
            foreach (var li in toShow)
            {
                if (prev.HasValue && li > prev + 1) Console.WriteLine("   ...");
                bool changed = changedSet.Contains(li);
                Console.WriteLine($"{(changed ? "→" : " ")} {li + 1,5}│ {resultLines[li]}");
                prev = li;
            }
        }
        return 0;
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
