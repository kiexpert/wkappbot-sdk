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
            "open"                   => FileOpenCommand(args[1..]),
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

    static bool TryCreateFileBackup(string path, string tag, out string? bakPath, out string? errMsg)
    {
        bakPath = null;
        errMsg = null;
        try
        {
            if (!File.Exists(path)) return true; // new file: nothing to back up

            var origMtime = File.GetLastWriteTime(path);
            var ts = origMtime.ToString("yyyyMMdd-HHmmss.fff");
            var fileName = Path.GetFileName(path);
            var bakDir = Path.Combine(Path.GetDirectoryName(path)!, ".bak");
            bakPath = Path.Combine(bakDir, $"{fileName}.bak-{ts}.txt");

            Directory.CreateDirectory(bakDir);
            File.Copy(path, bakPath, overwrite: true);
            File.SetCreationTime(bakPath, File.GetCreationTime(path));
            File.SetLastWriteTime(bakPath, File.GetLastWriteTime(path));
            Console.WriteLine($"[{tag}] backup -> {bakPath}");

            MigrateLegacyBackups(Path.GetDirectoryName(path)!, bakDir);
            return true;
        }
        catch (Exception ex)
        {
            errMsg = $"[{tag}] backup failed ({ex.Message}) - file NOT written. Use --i-really-want-no-backup to skip backup.";
            return false;
        }
    }
}
