// AgentFileTracker.cs — tracks file modifications during an agent session
//
// Flow:
//   AgentFileTracker.Track(path)         ← call BEFORE file-write (captures original bytes)
//   AgentFileTracker.Checkpoint("label") ← mid-session snapshot (e.g. before compile)
//   AgentFileTracker.DumpPatch(workspace) ← generate patch + restore hints
//
// Patch file contains:
//   - Apply hint  (git apply agent-patch-*.patch)
//   - Per-checkpoint restore hints (file copy from temp dir)
//   - Original restore hints (git checkout HEAD or file copy or git apply --reverse)
//   - New-file delete hints
//   - Actual unified diffs (git diff --no-index, works without git commits)

using System.Security.Cryptography;
using System.Text;

static class AgentFileTracker
{
    record CpSnapshot(int Id, string Label, DateTime Time, Dictionary<string, byte[]> Files);

    static readonly Dictionary<string, byte[]> _originals  = new(StringComparer.OrdinalIgnoreCase);
    static readonly List<CpSnapshot>           _checkpoints = new();
    static readonly object                     _lock        = new();

    // Temp dir: %TEMP%\wkab-{PID} — auto-cleaned at process exit
    static readonly string TempDir =
        Path.Combine(Path.GetTempPath(), $"wkab-{Environment.ProcessId}");

    public static int TrackedCount    { get { lock (_lock) return _originals.Count; } }
    public static int CheckpointCount { get { lock (_lock) return _checkpoints.Count; } }

    // ── Register file before first modification ──────────────────────────────
    public static void Track(string path)
    {
        path = Path.GetFullPath(path);
        lock (_lock)
        {
            if (_originals.ContainsKey(path)) return;
            try   { _originals[path] = File.Exists(path) ? File.ReadAllBytes(path) : []; }
            catch { _originals[path] = []; }
            Console.WriteLine($"[TRACKER] tracking: {Path.GetFileName(path)}  (orig={_originals[path].Length}B)");
        }
    }

    // ── Mid-session snapshot ─────────────────────────────────────────────────
    public static int Checkpoint(string label = "")
    {
        lock (_lock)
        {
            var snap = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
            foreach (var path in _originals.Keys)
                try   { snap[path] = File.Exists(path) ? File.ReadAllBytes(path) : []; }
                catch { snap[path] = []; }

            var cp = new CpSnapshot(_checkpoints.Count + 1, label, DateTime.Now, snap);
            _checkpoints.Add(cp);
            Console.WriteLine($"[TRACKER] checkpoint {cp.Id} \"{label}\" — {snap.Count} files");
            return cp.Id;
        }
    }

    // ── Generate patch + hints ────────────────────────────────────────────────
    /// <returns>Path to saved patch file, or null if nothing tracked.</returns>
    public static string? DumpPatch(string workspace, string? outPath = null)
    {
        lock (_lock)
        {
            if (_originals.Count == 0) return null;

            outPath ??= Path.Combine(workspace,
                $"agent-patch-{DateTime.Now:yyyyMMdd_HHmmss}.patch");
            var patchName = Path.GetFileName(outPath);

            // Write originals + checkpoints to temp dir for copy-restore hints
            var origDir = Path.Combine(TempDir, "orig");
            Directory.CreateDirectory(origDir);
            foreach (var (path, bytes) in _originals)
                WriteTemp(origDir, path, bytes);

            foreach (var cp in _checkpoints)
            {
                var cpDir = Path.Combine(TempDir, $"cp{cp.Id}");
                Directory.CreateDirectory(cpDir);
                foreach (var (path, bytes) in cp.Files)
                    WriteTemp(cpDir, path, bytes);
            }

            var sb = new StringBuilder();

            // ── Header ────────────────────────────────────────────────────────
            sb.AppendLine($"# WKAppBot Agent Session Patch — {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"# Workspace : {workspace}");
            sb.AppendLine($"# Modified  : {_originals.Count} file(s)   Checkpoints: {_checkpoints.Count}");
            sb.AppendLine("#");

            // Apply
            sb.AppendLine("# ── 적용 (변경사항 반영) ──────────────────────────────");
            sb.AppendLine($"#   git apply {patchName}");
            sb.AppendLine("#");

            // Checkpoint restore
            if (_checkpoints.Count > 0)
            {
                sb.AppendLine("# ── 체크포인트 복구 (시간 기준 중간 상태) ───────────");
                foreach (var cp in _checkpoints)
                {
                    sb.AppendLine($"#   cp{cp.Id}: \"{cp.Label}\" ({cp.Time:HH:mm:ss})");
                    var cpDir = Path.Combine(TempDir, $"cp{cp.Id}");
                    foreach (var path in cp.Files.Keys)
                    {
                        var tmp = TempPathFor(cpDir, path);
                        sb.AppendLine($"#     copy \"{tmp}\" \"{path}\"");
                    }
                }
                sb.AppendLine("#");
            }

            // Classify: git-tracked originals vs new files
            var existed  = _originals.Where(kv => kv.Value.Length > 0).Select(kv => kv.Key).ToList();
            var newFiles = _originals.Where(kv => kv.Value.Length == 0).Select(kv => kv.Key).ToList();

            // Original restore
            sb.AppendLine("# ── 원본 복구 (세션 시작 전 상태) ──────────────────");
            if (existed.Count > 0)
            {
                sb.AppendLine($"#   git apply --reverse {patchName}");
                sb.AppendLine("#   또는 git checkout HEAD 로 복구:");
                var rels = existed.Select(p => Path.GetRelativePath(workspace, p).Replace('\\', '/'));
                sb.AppendLine($"#   git checkout HEAD -- {string.Join(" ", rels)}");
                sb.AppendLine("#   또는 파일별 copy (git 없이도 가능):");
                foreach (var path in existed)
                {
                    var tmp = TempPathFor(origDir, path);
                    sb.AppendLine($"#     copy \"{tmp}\" \"{path}\"");
                }
                sb.AppendLine("#");
            }
            if (newFiles.Count > 0)
            {
                sb.AppendLine("# ── 새 파일 복구 (삭제) ─────────────────────────");
                foreach (var path in newFiles)
                    sb.AppendLine($"#     del \"{path}\"");
                sb.AppendLine("#");
            }
            sb.AppendLine("# ═══════════════════════════════════════════════════════");
            sb.AppendLine();

            // ── Main patch: orig → current (full session diff) ────────────────
            sb.AppendLine("# ── diff: original → current (세션 전체 변경) ──");
            foreach (var path in _originals.Keys)
                AppendDiff(sb, workspace, origDir, path, path, "orig→current");

            File.WriteAllText(outPath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"[TRACKER] patch saved: {outPath}");

            // ── Per-checkpoint individual patch files ─────────────────────────
            // agent-patch-cp1-HHmm-label.patch, agent-patch-cp2-HHmm.patch, ...
            foreach (var cp in _checkpoints)
            {
                var cpDir = Path.Combine(TempDir, $"cp{cp.Id}");
                var cpLabel = string.IsNullOrWhiteSpace(cp.Label)
                    ? "" : $"-{SanitizeLabel(cp.Label)}";
                var cpFile = Path.Combine(workspace,
                    $"agent-patch-cp{cp.Id}-{cp.Time:HHmm}{cpLabel}.patch");

                var cpSb = new StringBuilder();
                cpSb.AppendLine($"# WKAppBot Checkpoint {cp.Id} — {cp.Time:yyyy-MM-dd HH:mm:ss}  \"{cp.Label}\"");
                cpSb.AppendLine($"# Workspace: {workspace}");
                cpSb.AppendLine($"# Files: {cp.Files.Count}");
                cpSb.AppendLine("#");
                cpSb.AppendLine("# ── 이 체크포인트로 복구 ──────────────────────────────");
                foreach (var path in cp.Files.Keys)
                {
                    var tmp = TempPathFor(cpDir, path);
                    cpSb.AppendLine($"#   copy \"{tmp}\" \"{path}\"");
                }
                cpSb.AppendLine("#");
                cpSb.AppendLine("# ── diff: original → this checkpoint ────────────────");
                foreach (var path in cp.Files.Keys)
                    AppendDiff(cpSb, workspace, origDir, TempPathFor(cpDir, path), path, $"orig→cp{cp.Id}");

                File.WriteAllText(cpFile, cpSb.ToString(), Encoding.UTF8);
                Console.WriteLine($"[TRACKER] cp{cp.Id} patch: {cpFile}");
            }

            return outPath;
        }
    }

    public static void Clear()
    {
        lock (_lock) { _originals.Clear(); _checkpoints.Clear(); }
    }

    public static void Cleanup()
    {
        try { if (Directory.Exists(TempDir)) Directory.Delete(TempDir, recursive: true); }
        catch { }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    static string SanitizeLabel(string label) =>
        new string(label.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray())
            .Replace(' ', '-')[..Math.Min(label.Length, 20)];

    static string FileKey(string path) =>
        Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(path)))[..8];

    static string TempPathFor(string dir, string path) =>
        Path.Combine(dir, $"{FileKey(path)}_{Path.GetFileName(path)}");

    static void WriteTemp(string dir, string path, byte[] bytes)
    {
        try { File.WriteAllBytes(TempPathFor(dir, path), bytes); }
        catch { }
    }

    static void AppendDiff(StringBuilder sb, string workspace,
        string origDir, string leftPath, string rightPath, string tag)
    {
        try
        {
            var rel = Path.GetRelativePath(workspace, rightPath).Replace('\\', '/');
            var origFile = TempPathFor(origDir, rightPath);

            // left = original (from temp), right = current (or checkpoint snapshot)
            bool isNew = !File.Exists(origFile) || new FileInfo(origFile).Length == 0;
            var left  = isNew ? "/dev/null" : origFile;

            var psi = new System.Diagnostics.ProcessStartInfo("git",
                $"diff --no-index -- \"{left}\" \"{leftPath}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                WorkingDirectory       = workspace,
            };
            using var proc = System.Diagnostics.Process.Start(psi)!;
            var diff = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            if (!string.IsNullOrWhiteSpace(diff))
            {
                // Normalize paths in diff header to relative workspace paths
                diff = diff
                    .Replace(origFile.Replace('\\', '/'), $"a/{rel}")
                    .Replace(leftPath.Replace('\\', '/'), $"b/{rel}");
                sb.Append(diff);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine($"# [diff error for {Path.GetFileName(rightPath)}: {ex.Message}]");
        }
    }
}
