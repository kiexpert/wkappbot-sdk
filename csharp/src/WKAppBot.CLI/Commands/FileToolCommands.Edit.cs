using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using WKAppBot.Vision;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int FileUndoCommand(string[] args)
    {
        string? atPrefix = null;
        string? searchPath = null;
        bool listOnly = false;
        bool dryRun = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] is "--at" or "--timestamp" && i + 1 < args.Length) { atPrefix = args[++i]; continue; }
            if (args[i] is "--path" or "--root" && i + 1 < args.Length) { searchPath = args[++i]; continue; }
            if (args[i] == "--list") { listOnly = true; continue; }
            if (args[i] == "--dry-run") { dryRun = true; continue; }
        }

        if (string.IsNullOrEmpty(atPrefix))
        {
            Console.WriteLine("Usage: wkappbot file undo --at <timestamp> [--path dir] [--list] [--dry-run]");
            Console.WriteLine("       wkappbot file undo --timestamp <timestamp> [--root dir] [--list] [--dry-run]");
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

        // Search for backups in two formats:
        // 1. New: .bak/ subfolder — {dir}/.bak/{filename}.bak-{yyyyMMdd-HHmmss.fff}.txt
        // 2. Legacy: sibling files — {original}.bak-{timestamp}.txt
        var restorePairs = new List<(string backup, string original)>();

        // New format: search .bak/ folders recursively
        var bakDirs = Directory.EnumerateDirectories(baseDir, ".bak", SearchOption.AllDirectories);
        foreach (var bakDir in bakDirs)
        {
            var parentDir = Path.GetDirectoryName(bakDir)!;
            foreach (var bakFile in Directory.EnumerateFiles(bakDir))
            {
                var fn = Path.GetFileName(bakFile);
                // Format: {originalFilename}.bak-{YYYYMMDD-HHmmss.fff}
                if (!fn.Contains(atPrefix)) continue;
                var bakMarker = fn.LastIndexOf(".bak-", StringComparison.Ordinal);
                if (bakMarker < 0) continue;
                var tsCandidate = fn[(bakMarker + 5)..]; // after ".bak-", may end in .txt
                if (tsCandidate.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    tsCandidate = tsCandidate[..^4];
                // Timestamp should be >= 15 chars (YYYYMMDD-HHmmss)
                if (tsCandidate.Length >= 15 && tsCandidate.Contains(atPrefix))
                {
                    var origName = fn[..bakMarker];
                    var origPath = Path.Combine(parentDir, origName);
                    restorePairs.Add((bakFile, origPath));
                }
            }
        }

        // Legacy format: sibling .bak-{ts}.txt files
        var legacyPattern = $".bak-{atPrefix}";
        var legacyFiles = Directory.EnumerateFiles(baseDir, "*.txt", SearchOption.AllDirectories)
            .Where(f => Path.GetFileName(f).Contains(legacyPattern));
        foreach (var bak in legacyFiles)
        {
            var bakIdx = bak.LastIndexOf(".bak-", StringComparison.Ordinal);
            if (bakIdx < 0) continue;
            var originalPath = bak[..bakIdx];
            restorePairs.Add((bak, originalPath));
        }

        restorePairs = restorePairs.OrderBy(p => p.backup).ToList();

        if (restorePairs.Count == 0)
        {
            Console.WriteLine($"[file undo] no backups found matching *{atPrefix}* in {baseDir}");
            return 1;
        }

        Console.WriteLine($"[file undo] found {restorePairs.Count} backup(s) matching *{atPrefix}*");

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
                // Save current file before undo (undo-of-undo safety net) → .bak/ subfolder
                if (File.Exists(orig))
                {
                    var undoTs = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
                    var undoBakDir = Path.Combine(Path.GetDirectoryName(orig)!, ".bak");
                    var undoBak = Path.Combine(undoBakDir, $"{Path.GetFileName(orig)}.bak-{undoTs}-undo.txt");
                    try { Directory.CreateDirectory(undoBakDir); File.Copy(orig, undoBak); } catch { }
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
    /// Also accepts option-style aliases for tool compatibility:
    ///   --old-string/--text/--new-string/--path/--dry-run
    /// Same interface as Claude Code Edit tool. Auto-detects encoding (BOM → system ANSI/CP949).
    /// Fails if old_string not found (exact match). Preserves original encoding on save.
    /// </summary>
    static int FileEditCommand(string[] args)
    {
        var positional = new List<string>();
        var optionPaths = new List<string>();
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
        string? oldStringOpt = null;
        string? newStringOpt = null;

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
            if (args[i] == "--dry-run") { _dryRunMode.Value = true; continue; }
            if (args[i] == "--old-string" && i + 1 < args.Length) { oldStringOpt = args[++i]; continue; }
            if ((args[i] is "--new-string" or "--text") && i + 1 < args.Length) { newStringOpt = args[++i]; continue; }
            if ((args[i] is "--path" or "--file") && i + 1 < args.Length) { optionPaths.Add(args[++i]); continue; }
            // --old-file/--new-file: read old/new strings from file (avoids bash Korean encoding issues)
            if (args[i] == "--old-file" && i + 1 < args.Length) { oldFile = args[++i]; continue; }
            if (args[i] == "--new-file" && i + 1 < args.Length) { newFile = args[++i]; continue; }
            // --undo <timestamp>: restore file from .bak-{timestamp} backup before applying edit
            if (args[i] == "--undo" && i + 1 < args.Length) { undoTimestamp = args[++i]; continue; }
            positional.Add(args[i]);
        }

        if (oldStringOpt != null || newStringOpt != null || optionPaths.Count > 0)
        {
            if (oldStringOpt != null) positional.Insert(0, oldStringOpt);
            if (newStringOpt != null)
            {
                var insertIndex = oldStringOpt != null ? 1 : 0;
                positional.Insert(insertIndex, newStringOpt);
            }
            positional.AddRange(optionPaths);
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
            Console.WriteLine("       wkappbot file edit --old-string <old> --text <new> --path <file> [--replace-all] [--dry-run]");
            Console.WriteLine("       wkappbot file edit --undo <timestamp> <old> <new> <path>  # restore backup, then edit");
            return 1;
        }

        // C-style escape: \n \t \r \\ → actual characters (unless --old-file mode)
        string oldStr = UnescapeCString(positional[0]);
        string newStr = UnescapeCString(positional[1]);
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

        // --undo: restore each file from its .bak/{filename}.{timestamp} or legacy .bak-{timestamp}.txt
        if (undoTimestamp != null)
        {
            int restored = 0;
            foreach (var path in paths)
            {
                var dir = Path.GetDirectoryName(path) ?? ".";
                var baseName = Path.GetFileName(path);

                // Try new format first: .bak/{baseName}.{timestamp}
                string? bakFile = null;
                var bakSubDir = Path.Combine(dir, ".bak");
                if (Directory.Exists(bakSubDir))
                {
                    bakFile = Directory.EnumerateFiles(bakSubDir, $"{baseName}.bak-*.txt")
                        .Where(f => Path.GetFileName(f).Contains(undoTimestamp))
                        .OrderByDescending(f => f)
                        .FirstOrDefault();
                }

                // Fallback: legacy sibling format {path}.bak-{timestamp}*.txt
                if (bakFile == null && Directory.Exists(dir))
                {
                    var legacyPattern = $".bak-{undoTimestamp}";
                    bakFile = Directory.EnumerateFiles(dir, $"{baseName}.bak-*", SearchOption.TopDirectoryOnly)
                        .Where(f => Path.GetFileName(f).Contains(legacyPattern))
                        .OrderByDescending(f => f)
                        .FirstOrDefault();
                }

                if (bakFile == null)
                {
                    Console.Error.WriteLine($"[file edit --undo] no backup matching {baseName}*{undoTimestamp}* in {dir}");
                    return 1;
                }

                // Safety: backup current file before undo (undo-of-undo) → .bak/ subfolder
                if (File.Exists(path))
                {
                    var undoTs = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
                    var undoBakDir = Path.Combine(Path.GetDirectoryName(path)!, ".bak");
                    var undoBak = Path.Combine(undoBakDir, $"{Path.GetFileName(path)}.bak-{undoTs}-undo.txt");
                    try { Directory.CreateDirectory(undoBakDir); File.Copy(path, undoBak); } catch { }
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

        // Adapt old/new line endings to match file's style (fixes LF-only file + CRLF old_string mismatch)
        bool fileCrlf = text.Contains("\r\n");
        if (!fileCrlf && oldStr.Contains("\r\n"))
        {
            oldStr = oldStr.Replace("\r\n", "\n");
            newStr = newStr.Replace("\r\n", "\n");
        }
        else if (fileCrlf && !oldStr.Contains("\r\n") && oldStr.Contains("\n"))
        {
            oldStr = oldStr.Replace("\n", "\r\n");
            newStr = newStr.Replace("\n", "\r\n");
        }

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
            // Backup goes to .bak/ subfolder to keep source directories clean
            var origMtime = File.GetLastWriteTime(plan.Path);
            var ts = origMtime.ToString("yyyyMMdd-HHmmss.fff");
            var fileName = Path.GetFileName(plan.Path);
            var bakDir = Path.Combine(Path.GetDirectoryName(plan.Path)!, ".bak");
            var bakPath = Path.Combine(bakDir, $"{fileName}.bak-{ts}.txt");
            try
            {
                Directory.CreateDirectory(bakDir);
                File.Copy(plan.Path, bakPath, overwrite: true);
                File.SetCreationTime(bakPath, File.GetCreationTime(plan.Path));
                File.SetLastWriteTime(bakPath, File.GetLastWriteTime(plan.Path));
                Console.WriteLine($"[file edit] backup → {bakPath}");

                // Auto-migrate legacy sibling .bak-*.txt files into .bak/ subfolder
                MigrateLegacyBackups(Path.GetDirectoryName(plan.Path)!, bakDir);
            }
            catch (Exception ex)
            {
                return Error($"[file edit] backup failed ({ex.Message}) — file NOT written. Use --i-really-want-no-backup to skip backup.");
            }
        }

        if (!noChange)
        {
            // Dry-run gate: save proposed edit as backup instead of writing
            if (_dryRunMode.Value)
            {
                var aiName = Environment.GetEnvironmentVariable("WKAPPBOT_LOOP_CALLER") ?? "ai";
                var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var dryBakDir = Path.Combine(Path.GetDirectoryName(plan.Path)!, ".bak");
                var dryFileName = Path.GetFileName(plan.Path);
                var backupPath = Path.Combine(dryBakDir, $"{dryFileName}.bak-{ts}-{aiName}.txt");
                try
                {
                    Directory.CreateDirectory(dryBakDir);
                    File.WriteAllBytes(backupPath, plan.OutBytes);
                    Console.Error.WriteLine("[DRY-RUN] ⚠ Your edit has been PROPOSED. The original file was NOT modified.");
                    Console.WriteLine($"[file edit] backup → {backupPath}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[DRY-RUN] file edit: {plan.Count} replacement(s) — backup failed ({ex.Message})");
                    Console.ResetColor();
                }
                return 0;
            }

            // Sanity check: re-detect encoding of output bytes to catch accidental encoding change
            var verifyEnc = DetectFileEncoding(plan.OutBytes, null);
            bool encodingChanged = verifyEnc.CodePage != plan.Enc.CodePage;
            if (encodingChanged)
                Console.Error.WriteLine($"[file edit] WARNING: output encoding changed! {plan.Enc.WebName} → {verifyEnc.WebName} — possible corruption");

            // Corruption check: look for U+FFFD replacement chars (indicates CP949→UTF-8 mojibake)
            // U+FFFD in UTF-8 = 0xEF 0xBF 0xBD
            bool hasReplacement = false;
            if (plan.Enc.CodePage != 65001 /* non-UTF-8 original */ || encodingChanged)
            {
                var outStr = plan.Enc.GetString(plan.OutBytes);
                hasReplacement = outStr.Contains('\uFFFD');
            }
            if (hasReplacement)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"[file edit] ⚠ ENCODING CORRUPTION: U+FFFD found in output — Korean/CJK chars may be mojibake!");
                Console.Error.WriteLine($"  File: {plan.Path}");
                Console.Error.WriteLine($"  Original encoding: {plan.Enc.WebName} (CP{plan.Enc.CodePage})");
                Console.Error.WriteLine($"  Restore with: wkappbot file edit --undo {DateTime.Now:yyyyMMdd-HHmmss} \"{plan.Path}\"");
                Console.ResetColor();
                // Send Slack alert via Eye pipe if available
                try
                {
                    var alertMsg = $":warning: *[인코딩 오염]* `{Path.GetFileName(plan.Path)}`\n원본 인코딩: {plan.Enc.WebName} → U+FFFD 발견. 한글 깨짐 가능성!\n`{plan.Path}`";
                    var slackCfg = LoadSlackConfig();
                    var tok = slackCfg?["bot_token"]?.GetValue<string>();
                    var ch  = slackCfg?["channel"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(tok) && !string.IsNullOrEmpty(ch))
                        PostWithOverflow(tok, ch, alertMsg);
                }
                catch { /* Slack alert is best-effort */ }
            }

            // WriteAllBytes uses FileMode.Create (truncate-in-place) — no delete event fired
            File.WriteAllBytes(plan.Path, plan.OutBytes);
            Console.WriteLine($"[file edit] {plan.Count} replacement(s) — encoding={plan.Enc.WebName}{(hasReplacement ? " ⚠CORRUPTION" : "")}");
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
                            Console.WriteLine($"< {oi + 1,5}| {origLines[oi]}");
                    }
                    rangeIdx++;
                }

                Console.WriteLine($"{(changed ? ">" : " ")} {li + 1,5}| {resultLines[li]}");
                prev = li;
            }
        }
        return 0;
    }

    /// <summary>
    /// Migrate all legacy backup formats into current format (.bak/{original}.bak-{ts}.txt).
    /// Phase 1: sibling .bak-*.txt files → move into .bak/ subfolder
    /// Phase 2: .bak/ files without .txt extension (intermediate format) → rename to add .txt
    /// Called once per file-edit, walks up from sourceDir to git/svn root to catch all dirs.
    /// </summary>
    static void MigrateLegacyBackups(string sourceDir, string bakDir)
    {
        // Walk up to project root (git/.svn/.wkappbot marker) — migrate all dirs en route
        var dirs = new List<string>();
        var cur = sourceDir;
        while (!string.IsNullOrEmpty(cur))
        {
            dirs.Add(cur);
            if (Directory.Exists(Path.Combine(cur, ".git")) ||
                Directory.Exists(Path.Combine(cur, ".svn")) ||
                Directory.Exists(Path.Combine(cur, ".wkappbot"))) break;
            var parent = Path.GetDirectoryName(cur);
            if (parent == cur || parent == null) break;
            cur = parent;
        }

        int totalMoved = 0;
        foreach (var dir in dirs)
        {
            try
            {
                // Phase 1: sibling .bak-*.txt → .bak/ subfolder
                var legacyFiles = Directory.EnumerateFiles(dir, "*.bak-*.txt", SearchOption.TopDirectoryOnly).ToList();
                var targetBakDir = Path.Combine(dir, ".bak");
                foreach (var legacy in legacyFiles)
                {
                    var fn = Path.GetFileName(legacy);
                    var bakIdx = fn.LastIndexOf(".bak-", StringComparison.Ordinal);
                    if (bakIdx < 0) continue;
                    var origName = fn[..bakIdx];
                    var tsPart = fn[(bakIdx + 5)..];
                    if (tsPart.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) tsPart = tsPart[..^4];
                    var newPath = Path.Combine(targetBakDir, $"{origName}.bak-{tsPart}.txt");
                    try
                    {
                        Directory.CreateDirectory(targetBakDir);
                        if (!File.Exists(newPath)) { File.Move(legacy, newPath); totalMoved++; }
                        else { File.Delete(legacy); totalMoved++; }
                    }
                    catch { }
                }

                // Phase 2: .bak/ files missing .txt extension → rename
                if (Directory.Exists(targetBakDir))
                {
                    foreach (var f in Directory.EnumerateFiles(targetBakDir, "*", SearchOption.TopDirectoryOnly))
                    {
                        var fn = Path.GetFileName(f);
                        if (fn.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) continue; // already correct
                        if (!fn.Contains(".bak-")) continue; // not a backup file
                        var renamed = f + ".txt";
                        try { if (!File.Exists(renamed)) { File.Move(f, renamed); totalMoved++; } }
                        catch { }
                    }
                }

                // Phase 3: sibling .undo-*.txt files (old undo-of-undo format) → .bak/ subfolder
                // Old: {file}.undo-{ts}.txt  →  New: .bak/{file}.bak-{ts}-undo.txt
                var undoFiles = Directory.EnumerateFiles(dir, "*.undo-*.txt", SearchOption.TopDirectoryOnly).ToList();
                foreach (var undoFile in undoFiles)
                {
                    var fn = Path.GetFileName(undoFile);
                    var undoIdx = fn.LastIndexOf(".undo-", StringComparison.Ordinal);
                    if (undoIdx < 0) continue;
                    var origName = fn[..undoIdx];
                    var tsPart = fn[(undoIdx + 6)..]; // after ".undo-"
                    if (tsPart.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)) tsPart = tsPart[..^4];
                    var newPath = Path.Combine(targetBakDir, $"{origName}.bak-{tsPart}-undo.txt");
                    try
                    {
                        Directory.CreateDirectory(targetBakDir);
                        if (!File.Exists(newPath)) { File.Move(undoFile, newPath); totalMoved++; }
                        else { File.Delete(undoFile); totalMoved++; }
                    }
                    catch { }
                }
            }
            catch { }
        }

        if (totalMoved > 0)
            Console.WriteLine($"[file edit] migrated {totalMoved} legacy backup(s) to current format");
    }

    static int GetLineIndent(string[] lines, int tabSize, int idx)
    {
        if (idx < 0 || idx >= lines.Length) return 0;
        var line = lines[idx];
        int leadEnd = line.Length - line.TrimStart().Length;
        var leading = line[..leadEnd];
        // Normalize: convert N-space groups and space+tab combos to tabs, then count all tabs
        var pattern = $@"( {{{tabSize},{tabSize}}}| +\t)";
        while (leading != (leading = System.Text.RegularExpressions.Regex.Replace(leading, pattern, "\t"))) ;
        // Count tabs (including pre-existing tabs in tab-indented files)
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
  wkappbot file read --path <path> [--start N] [--end N] [--count N] [--no-line-numbers]
      Read file with line numbers. Encoding: --encoding > BOM > system ANSI (CP949 on KR).

  wkappbot file read-pdf <path> [--pages N-M] [--max-chars N] [--ocr]
  wkappbot file read-pdf --path <path> [--start-page N] [--end-page N]
      Extract text from PDF (Korean/CJK safe). --pages 1-5, --max-chars 50000.
      --ocr: also OCR each page via Windows OCR; appends [+OCR: ...] for content PdfPig missed.

  wkappbot file edit <old_string> <new_string> <path>... [--replace-all] [--regex] [--i-really-want-lossy-encoding] [--encoding N] [--context N] [--backup]
      Exact-match replace. sed/grep-style: old/new first, then one or more file paths/globs.
      Auto-detects encoding (BOM > UTF-8 validity scan > system ANSI/CP949).
      Without --replace-all: fails if old_string not found or matches multiple locations.
      --regex: old_string is a .NET regex; new_string may use $1/$2 capture groups.
      --i-really-want-lossy-encoding: allow '?' substitution for chars not encodable in target charset.
      --i-really-want-no-backup: skip backup (default: backup ON — saves .bak/<file>.bak-yyyyMMdd-HHmmss.fff.txt).
      --context N: lines of context around changes (default 1; 0 = header only).

  wkappbot file write <path> [--encoding N] (--stdin | --text ""..."" | --file <src>) [--append]
  wkappbot file write --path <path> [--content ""...""] [--source-file <src>] [--dry-run]
      Write content to file, re-encoding to target charset.
      --encoding N  target codepage (e.g. 949 for CP949/EUC-KR)
      --stdin       read content from stdin (pipe mode)
      --text ""...""  inline content
      --file <src>  copy from source file (auto-detect source encoding)
      --append      append instead of overwrite
      backup ON by default (.bak/<file>.bak-yyyyMMdd-HHmmss.fff.txt)

  wkappbot file grep <pattern> [--path dir/file] [--type ext] [-i] [-C N] [--max N] [--encoding N]
  wkappbot file grep --pattern <regex> [--root dir] [--file file]
      Regex search across files.

  wkappbot file glob <pattern> [--path dir]
  wkappbot file glob --pattern <glob> [--path dir]
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
