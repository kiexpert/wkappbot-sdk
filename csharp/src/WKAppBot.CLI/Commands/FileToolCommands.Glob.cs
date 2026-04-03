using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int FileGlobCommand(string[] args)
    {
        string? pattern = null;
        string  root    = FileDefaultDir();

        for (int i = 0; i < args.Length; i++)
        {
            if ((args[i] is "--path" or "--root") && i + 1 < args.Length) root = args[++i];
            else if (args[i] == "--pattern" && i + 1 < args.Length) pattern = args[++i];
            else if (!args[i].StartsWith("--"))
            {
                if (pattern == null) pattern = args[i];
                else if (Directory.Exists(args[i])) root = args[i]; // 2nd positional = folder
            }
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
        bool    backup   = true;
        bool    dryRun   = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--encoding" && i + 1 < args.Length) { if (int.TryParse(args[++i], out int cp)) encoding = cp; }
            else if ((args[i] is "--text" or "--content" or "--new-string") && i + 1 < args.Length) text = args[++i];
            else if ((args[i] is "--file" or "--source-file") && i + 1 < args.Length) srcFile = args[++i];
            else if ((args[i] is "--path" or "--target") && i + 1 < args.Length) path = args[++i];
            else if (args[i] == "--stdin")  stdin  = true;
            else if (args[i] == "--append") append = true;
            else if (args[i] == "--i-really-want-no-backup") backup = false;
            else if (args[i] == "--dry-run") dryRun = true;
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

            if (backup)
            {
                if (!TryCreateFileBackup(path, "file write", out _, out var backupErr))
                    return Error(backupErr ?? "[file write] backup failed");
            }

            if (dryRun)
            {
                var aiName = Environment.GetEnvironmentVariable("WKAPPBOT_LOOP_CALLER") ?? "ai";
                var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var dryBakDir = Path.Combine(Path.GetDirectoryName(path) ?? ".", ".bak");
                var dryFileName = Path.GetFileName(path);
                var previewPath = Path.Combine(dryBakDir, $"{dryFileName}.bak-{ts}-{aiName}.txt");
                Directory.CreateDirectory(dryBakDir);
                File.WriteAllBytes(previewPath, bytes);
                Console.Error.WriteLine("[DRY-RUN] Your write has been PROPOSED. The original file was NOT modified.");
                Console.WriteLine($"[file write] backup -> {previewPath}");
                return 0;
            }

            if (append)
                using (var fs = File.Open(path, FileMode.Append)) fs.Write(bytes, 0, bytes.Length);
            else
                File.WriteAllBytes(path, bytes);

            var encName = encoding.HasValue ? $"CP{encoding}" : "UTF-8";
            Console.WriteLine($"[FILE] write OK -> {path} ({bytes.Length} bytes, {encName})");
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
            if ((args[i] is "--path" or "--file") && i + 1 < args.Length)
                path = args[++i];
            else if (args[i] == "--pages" && i + 1 < args.Length)
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
            else if (args[i] == "--start-page" && i + 1 < args.Length)
                int.TryParse(args[++i], out pgFrom);
            else if (args[i] == "--end-page" && i + 1 < args.Length)
                int.TryParse(args[++i], out pgTo);
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
}
