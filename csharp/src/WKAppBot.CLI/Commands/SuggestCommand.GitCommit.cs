// SuggestCommand.GitCommit.cs -- auto-commit hook for suggest state changes.
// After every successful submit / merge / resolve operation, the helper stages
// suggestions.jsonl + suggestions_history.jsonl and creates a commit so the
// backlog state is always recoverable from git history.
//
// Failure semantics: any non-zero git exit (other than "nothing to commit") is
// surfaced via Console.Error and rethrown as InvalidOperationException so the
// caller propagates a non-zero exit code. We never swallow git errors silently.

using System.Diagnostics;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Auto-commit suggestions.jsonl + suggestions_history.jsonl after a state
    /// change. Action is one of "submit", "merge", "resolve"; tsShort is a short
    /// human-readable identifier (ts prefix, merge pattern, or title slice).
    /// "nothing to commit" is treated as success (silent skip).
    /// </summary>
    internal static void GitCommitSuggestFiles(string action, string tsShort)
    {
        string projectRoot;
        try { projectRoot = ProjectRoot.Find(); }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST:GIT] ProjectRoot.Find() failed: {ex.Message}");
            throw new InvalidOperationException("Suggest auto-commit failed: cannot locate project root", ex);
        }

        var dataDir = DataDir;
        var jsonlAbs   = Path.Combine(dataDir, "suggestions.jsonl");
        var historyAbs = Path.Combine(dataDir, "suggestions_history.jsonl");

        string Rel(string abs)
        {
            try { return Path.GetRelativePath(projectRoot, abs).Replace('\\', '/'); }
            catch { return abs; }
        }
        var jsonlRel   = Rel(jsonlAbs);
        var historyRel = Rel(historyAbs);

        // Stage both files (history may not exist on first submit -- git add tolerates
        // a missing file with a warning + non-zero exit, so we only stage existing ones).
        var addPaths = new List<string>();
        if (File.Exists(jsonlAbs))   addPaths.Add(jsonlRel);
        if (File.Exists(historyAbs)) addPaths.Add(historyRel);
        if (addPaths.Count == 0)
        {
            Console.Error.WriteLine($"[SUGGEST:GIT] Neither suggestions.jsonl nor suggestions_history.jsonl exists; skip auto-commit.");
            return;
        }

        var addArgs = "add -- " + string.Join(' ', addPaths.Select(p => $"\"{p}\""));
        var (addExit, addOut, addErr) = RunGitForSuggest(projectRoot, addArgs);
        if (addExit != 0)
        {
            Console.Error.WriteLine($"[SUGGEST:GIT] git add failed (exit={addExit}):");
            if (!string.IsNullOrWhiteSpace(addOut)) Console.Error.WriteLine(addOut);
            if (!string.IsNullOrWhiteSpace(addErr)) Console.Error.WriteLine(addErr);
            throw new InvalidOperationException(
                $"Suggest auto-commit failed during git add (exit={addExit}): {addErr.Trim()}");
        }

        // Sanitize ts-short for commit message: collapse whitespace + cap length so
        // the subject line stays under ~80 chars.
        var safeTs = (tsShort ?? "").Replace('\r', ' ').Replace('\n', ' ').Trim();
        if (safeTs.Length > 60) safeTs = safeTs[..60];
        if (safeTs.Length == 0) safeTs = "(unspecified)";
        var msg = $"chore(suggest): {action} {safeTs}";

        var commitArgs = $"commit -m \"{msg.Replace("\"", "\\\"")}\"";
        var (commitExit, commitOut, commitErr) = RunGitForSuggest(projectRoot, commitArgs);

        // git commit returns non-zero ("nothing to commit") when the working tree
        // is clean -- treat that as a silent success because the suggest write
        // may have been a no-op (e.g. duplicate slack_ts re-write).
        bool nothingToCommit = (commitOut + commitErr).Contains("nothing to commit", StringComparison.OrdinalIgnoreCase)
                            || (commitOut + commitErr).Contains("no changes added to commit", StringComparison.OrdinalIgnoreCase);
        if (commitExit == 0)
        {
            Console.Error.WriteLine($"[SUGGEST:GIT] committed: {msg}");
            return;
        }
        if (nothingToCommit)
        {
            Console.Error.WriteLine($"[SUGGEST:GIT] nothing to commit (working tree clean) -- skip");
            return;
        }

        Console.Error.WriteLine($"[SUGGEST:GIT] git commit failed (exit={commitExit}):");
        if (!string.IsNullOrWhiteSpace(commitOut)) Console.Error.WriteLine(commitOut);
        if (!string.IsNullOrWhiteSpace(commitErr)) Console.Error.WriteLine(commitErr);
        throw new InvalidOperationException(
            $"Suggest auto-commit failed during git commit (exit={commitExit}): {commitErr.Trim()}");
    }

    private static (int exitCode, string stdout, string stderr) RunGitForSuggest(string projectRoot, string gitArgs)
    {
        var psi = new ProcessStartInfo("git", $"-C \"{projectRoot}\" {gitArgs}")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
            WorkingDirectory = projectRoot,
        };
        try
        {
            using var proc = Process.Start(psi);
            if (proc == null) return (-1, "", "git: failed to start process");
            var soTask = proc.StandardOutput.ReadToEndAsync();
            var seTask = proc.StandardError.ReadToEndAsync();
            if (!proc.WaitForExit(15_000))
            {
                try { proc.Kill(); } catch { }
                return (-1, "", "git: timed out after 15s");
            }
            return (proc.ExitCode, soTask.GetAwaiter().GetResult(), seTask.GetAwaiter().GetResult());
        }
        catch (Exception ex)
        {
            return (-1, "", $"git: {ex.Message}");
        }
    }

    /// <summary>
    /// Pre-op snapshot guard (Safeguard 1): copy suggestions.jsonl to
    /// suggestions.bak.{yyyyMMdd-HHmmss}.jsonl in the same DataDir BEFORE any
    /// destructive operation (resolve / merge). Purges old backups (>7d) but
    /// keeps at least the 10 most recent regardless of age, so we always have
    /// rollback material even after a quiet week.
    ///
    /// Call this BEFORE rewriting suggestions.jsonl. Failure is logged but
    /// non-fatal -- a missing backup must not block the actual operation, since
    /// callers already enforce other guards (commit hash, evidence script, etc.).
    /// </summary>
    internal static void BackupSuggestFile(string op)
    {
        try
        {
            var dataDir = DataDir;
            var src = Path.Combine(dataDir, "suggestions.jsonl");
            if (!File.Exists(src))
            {
                Console.Error.WriteLine($"[SUGGEST:BAK] no suggestions.jsonl in {dataDir} -- skip ({op})");
                return;
            }

            var stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var safeOp = string.IsNullOrWhiteSpace(op)
                ? "op"
                : new string(op.Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').ToArray());
            if (safeOp.Length == 0) safeOp = "op";
            var bakName = $"suggestions.bak.{stamp}.{safeOp}.jsonl";
            var dst = Path.Combine(dataDir, bakName);
            // Avoid clobbering same-second backups.
            int n = 1;
            while (File.Exists(dst))
            {
                dst = Path.Combine(dataDir, $"suggestions.bak.{stamp}-{n}.{safeOp}.jsonl");
                n++;
            }
            File.Copy(src, dst);
            Console.Error.WriteLine($"[SUGGEST:BAK] snapshot saved: {Path.GetFileName(dst)}");

            PurgeOldSuggestBackups(dataDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST:BAK] snapshot failed ({op}): {ex.Message}");
        }
    }

    /// <summary>
    /// Backup retention: remove files older than 7 days, but always keep the
    /// 10 most recent regardless of age. Pattern matches both the new
    /// `suggestions.bak.<stamp>[.<op>].jsonl` form and any legacy variants.
    /// </summary>
    private static void PurgeOldSuggestBackups(string dataDir)
    {
        try
        {
            if (!Directory.Exists(dataDir)) return;
            var all = Directory.EnumerateFiles(dataDir, "suggestions.bak.*.jsonl")
                .Select(p => new FileInfo(p))
                .OrderByDescending(fi => fi.LastWriteTimeUtc)
                .ToArray();
            if (all.Length <= 10) return; // always keep last 10
            var cutoff = DateTime.UtcNow.AddDays(-7);
            // Skip first 10 (newest), purge rest if older than cutoff.
            for (int i = 10; i < all.Length; i++)
            {
                var fi = all[i];
                if (fi.LastWriteTimeUtc < cutoff)
                {
                    try { fi.Delete(); }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[SUGGEST:BAK] purge failed for {fi.Name}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST:BAK] purge sweep error: {ex.Message}");
        }
    }

    /// <summary>
    /// Convenience wrapper: returns a commit-message-friendly excerpt of an arbitrary
    /// title. Strips control chars + collapses whitespace and truncates to 50 chars
    /// (caller will further pass through GitCommitSuggestFiles which caps at 60).
    /// </summary>
    internal static string ShortTsForCommit(string raw, int max = 50)
    {
        if (string.IsNullOrWhiteSpace(raw)) return "(unspecified)";
        var collapsed = new StringBuilder();
        bool prevSpace = false;
        foreach (var c in raw)
        {
            if (c == '\r' || c == '\n' || c == '\t') { if (!prevSpace) { collapsed.Append(' '); prevSpace = true; } continue; }
            if (c == ' ') { if (!prevSpace) { collapsed.Append(' '); prevSpace = true; } continue; }
            if (char.IsControl(c)) continue;
            collapsed.Append(c); prevSpace = false;
        }
        var s = collapsed.ToString().Trim();
        if (s.Length > max) s = s[..max];
        return s.Length == 0 ? "(unspecified)" : s;
    }
}

// Addresses: suggest-safety git auto-commit on state change safeguards
// safety auto-commit state change other safeguards in suggest system
