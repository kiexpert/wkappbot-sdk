// AgentFileTracker.cs -- persistent cross-process file modification tracker
//
// Each wkappbot invocation is a separate process, so state is persisted to disk:
//   agent-session.json   -- list of tracked files + checkpoint index (in workspace root)
//   agent-checkpoints/   -- orig/ and cp{n}/ directories (in workspace root)
//
// Flow (across any number of processes):
//   AgentFileTracker.Track(path)          <- before file-write: copy orig, append to session
//   AgentFileTracker.Checkpoint("label")  <- snapshot current state of tracked files
//   AgentFileTracker.DumpPatch()          <- git diff HEAD for cumulative diff
//   AgentFileTracker.SessionClear()       <- delete session.json + checkpoints dir
//
// Patch file:
//   - git diff HEAD (all tracked files, cumulative across processes)
//   - Per-checkpoint: git diff --no-index orig cp{n}
//   - Restore hints: git checkout HEAD -- files

using System.Text.Json;
using System.Text.Json.Serialization;

static class AgentFileTracker
{
    // -- Session data model ----------------------------------------------------

    class SessionData
    {
        [JsonPropertyName("startTime")]   public string StartTime   { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        [JsonPropertyName("workspace")]   public string Workspace   { get; set; } = "";
        [JsonPropertyName("trackedFiles")]public List<string> TrackedFiles { get; set; } = new();
        [JsonPropertyName("checkpoints")] public List<CpEntry> Checkpoints { get; set; } = new();
    }

    class CpEntry
    {
        [JsonPropertyName("id")]    public int    Id    { get; set; }
        [JsonPropertyName("label")] public string Label { get; set; } = "";
        [JsonPropertyName("time")]  public string Time  { get; set; } = "";
        [JsonPropertyName("dir")]   public string Dir   { get; set; } = "";
    }

    // -- Workspace / session paths --------------------------------------------─

    static readonly string? _workspace = ResolveGitRoot();
    static readonly string? _sessionFile  = _workspace is null ? null : Path.Combine(_workspace, "agent-session.json");
    static readonly string? _cpDir        = _workspace is null ? null : Path.Combine(_workspace, "agent-checkpoints");

    static string? ResolveGitRoot()
    {
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo("git", "rev-parse --show-toplevel")
            {
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
            };
            using var p = AppBotPipe.Start(psi)!;
            var root = p.StandardOutput.ReadToEnd().Trim();
            p.WaitForExit();
            if (p.ExitCode == 0 && Directory.Exists(root))
                return Path.GetFullPath(root.Replace('/', Path.DirectorySeparatorChar));
        }
        catch { }
        return null;
    }

    // -- Session I/O ----------------------------------------------------------─

    static SessionData LoadSession()
    {
        if (_sessionFile is null) return new SessionData();
        try
        {
            if (File.Exists(_sessionFile))
            {
                var json = File.ReadAllText(_sessionFile);
                return JsonSerializer.Deserialize<SessionData>(json) ?? new SessionData();
            }
        }
        catch { }
        return new SessionData { Workspace = _workspace ?? "" };
    }

    static void SaveSession(SessionData s)
    {
        if (_sessionFile is null) return;
        try
        {
            var opts = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_sessionFile, JsonSerializer.Serialize(s, opts));
        }
        catch { }
    }

    // -- Public API ------------------------------------------------------------

    /// <summary>
    /// Register a file before first modification.
    /// Copies original to agent-checkpoints/orig/ and records in session.json.
    /// Safe to call multiple times for the same path.
    /// </summary>
    public static void Track(string path)
    {
        if (_cpDir is null) return;
        path = Path.GetFullPath(path);

        var session = LoadSession();
        if (session.TrackedFiles.Contains(path, StringComparer.OrdinalIgnoreCase))
            return; // already tracked this session

        // Copy original to orig/
        Directory.CreateDirectory(Path.Combine(_cpDir, "orig"));
        var origDest = TempPathFor(Path.Combine(_cpDir, "orig"), path);
        try
        {
            if (File.Exists(path))
                File.Copy(path, origDest, overwrite: true);
            else
                File.WriteAllBytes(origDest, Array.Empty<byte>()); // new file marker
        }
        catch { }

        session.TrackedFiles.Add(path);
        if (string.IsNullOrEmpty(session.Workspace)) session.Workspace = _workspace ?? "";
        SaveSession(session);

        var sizeStr = File.Exists(origDest) ? $"{new FileInfo(origDest).Length}B" : "new";
        Console.WriteLine($"[TRACKER] tracking: {Path.GetFileName(path)}  (orig={sizeStr})");
    }

    /// <summary>
    /// Save a mid-session snapshot of all tracked files.
    /// Returns checkpoint ID.
    /// </summary>
    public static int Checkpoint(string label = "")
    {
        if (_cpDir is null)
        {
            Console.WriteLine("[TRACKER] No git workspace found -- checkpoint skipped.");
            return 0;
        }

        var session = LoadSession();
        var id = session.Checkpoints.Count + 1;
        var cpSubDir = Path.Combine(_cpDir, $"cp{id}");
        Directory.CreateDirectory(cpSubDir);

        foreach (var path in session.TrackedFiles)
        {
            var dest = TempPathFor(cpSubDir, path);
            try
            {
                if (File.Exists(path))
                    File.Copy(path, dest, overwrite: true);
            }
            catch { }
        }

        var entry = new CpEntry
        {
            Id    = id,
            Label = label,
            Time  = DateTime.Now.ToString("HH:mm:ss"),
            Dir   = cpSubDir,
        };
        session.Checkpoints.Add(entry);
        SaveSession(session);

        Console.WriteLine($"[TRACKER] checkpoint {id} \"{label}\" -- {session.TrackedFiles.Count} files");
        return id;
    }

    /// <summary>
    /// Generate patch file using git diff HEAD for cumulative diff.
    /// Per-checkpoint patches use git diff --no-index.
    /// Returns path to saved patch file, or null if nothing tracked.
    /// </summary>
    public static string? DumpPatch(string? workspace = null, string? outPath = null)
    {
        var session = LoadSession();
        if (session.TrackedFiles.Count == 0)
        {
            Console.WriteLine("[TRACKER] No files tracked in current session.");
            return null;
        }

        workspace ??= _workspace ?? Directory.GetCurrentDirectory();
        outPath ??= Path.Combine(workspace, $"agent-patch-{DateTime.Now:yyyyMMdd_HHmmss}.patch");

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# WKAppBot Agent Session Patch -- {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"# Workspace  : {workspace}");
        sb.AppendLine($"# Session    : started {session.StartTime}");
        sb.AppendLine($"# Tracked    : {session.TrackedFiles.Count} file(s)   Checkpoints: {session.Checkpoints.Count}");
        sb.AppendLine("#");

        // Apply hint
        sb.AppendLine("# -- 적용 (변경사항 반영) ------------------------------");
        sb.AppendLine($"#   git apply {Path.GetFileName(outPath)}");
        sb.AppendLine("#");

        // Checkpoint restore hints
        if (session.Checkpoints.Count > 0)
        {
            sb.AppendLine("# -- 체크포인트 복구 (시간 기준 중간 상태) ----------─");
            foreach (var cp in session.Checkpoints)
            {
                sb.AppendLine($"#   cp{cp.Id}: \"{cp.Label}\" ({cp.Time})");
                foreach (var path in session.TrackedFiles)
                {
                    var tmp = TempPathFor(cp.Dir, path);
                    if (File.Exists(tmp))
                        sb.AppendLine($"#     copy \"{tmp}\" \"{path}\"");
                }
            }
            sb.AppendLine("#");
        }

        // Original restore hints
        var origDir = Path.Combine(_cpDir ?? workspace, "orig");
        var existed  = session.TrackedFiles.Where(p => { var o = TempPathFor(origDir, p); return File.Exists(o) && new FileInfo(o).Length > 0; }).ToList();
        var newFiles = session.TrackedFiles.Except(existed, StringComparer.OrdinalIgnoreCase).ToList();

        sb.AppendLine("# -- 원본 복구 (세션 시작 전 상태) ------------------");
        if (existed.Count > 0)
        {
            var rels = existed.Select(p => Path.GetRelativePath(workspace, p).Replace('\\', '/'));
            sb.AppendLine($"#   git checkout HEAD -- {string.Join(" ", rels)}");
            sb.AppendLine("#   또는 파일별 copy:");
            foreach (var path in existed)
            {
                var tmp = TempPathFor(origDir, path);
                sb.AppendLine($"#     copy \"{tmp}\" \"{path}\"");
            }
            sb.AppendLine("#");
        }
        if (newFiles.Count > 0)
        {
            sb.AppendLine("# -- 새 파일 삭제 ----------------------------------");
            foreach (var path in newFiles)
                sb.AppendLine($"#     del \"{path}\"");
            sb.AppendLine("#");
        }
        sb.AppendLine("# =======================================================");
        sb.AppendLine();

        // Main diff: git diff HEAD (cumulative across all processes)
        sb.AppendLine("# -- diff: HEAD -> current (세션 전체 누적 변경) ------");
        var trackedRels = session.TrackedFiles
            .Where(p => existed.Contains(p, StringComparer.OrdinalIgnoreCase))
            .Select(p => Path.GetRelativePath(workspace, p).Replace('\\', '/'))
            .ToList();

        if (trackedRels.Count > 0)
        {
            try
            {
                var fileArgs = string.Join(" ", trackedRels.Select(r => $"\"{r}\""));
                var psi = new System.Diagnostics.ProcessStartInfo("git", $"diff HEAD -- {fileArgs}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    UseShellExecute        = false,
                    WorkingDirectory       = workspace,
                };
                using var p = AppBotPipe.Start(psi)!;
                var diff = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (!string.IsNullOrWhiteSpace(diff))
                    sb.Append(diff);
                else
                    sb.AppendLine("# (no diff vs HEAD -- files unchanged or not committed)");
            }
            catch (Exception ex)
            {
                sb.AppendLine($"# [git diff error: {ex.Message}]");
            }
        }

        // New files: git diff --no-index /dev/null newfile
        foreach (var path in newFiles)
        {
            AppendNewFileDiff(sb, workspace, path);
        }

        File.WriteAllText(outPath, sb.ToString(), System.Text.Encoding.UTF8);
        Console.WriteLine($"[TRACKER] patch saved: {outPath}");

        // Per-checkpoint patches
        foreach (var cp in session.Checkpoints)
        {
            DumpCheckpointPatch(session, workspace, cp, origDir);
        }

        return outPath;
    }

    /// <summary>
    /// Print session status to console.
    /// </summary>
    public static void SessionStatus()
    {
        var session = LoadSession();
        Console.WriteLine($"[TRACKER] Session: started {session.StartTime}  workspace={session.Workspace}");
        Console.WriteLine($"[TRACKER] Tracked files ({session.TrackedFiles.Count}):");
        foreach (var f in session.TrackedFiles)
            Console.WriteLine($"  {f}");
        if (session.Checkpoints.Count == 0)
            Console.WriteLine("[TRACKER] No checkpoints.");
        else
        {
            Console.WriteLine($"[TRACKER] Checkpoints ({session.Checkpoints.Count}):");
            foreach (var cp in session.Checkpoints)
                Console.WriteLine($"  cp{cp.Id} \"{cp.Label}\" @ {cp.Time}");
        }
    }

    /// <summary>
    /// Delete session.json + agent-checkpoints/ to start fresh.
    /// </summary>
    public static void SessionClear()
    {
        if (_sessionFile != null && File.Exists(_sessionFile))
        {
            File.Delete(_sessionFile);
            Console.WriteLine($"[TRACKER] Deleted {_sessionFile}");
        }
        if (_cpDir != null && Directory.Exists(_cpDir))
        {
            Directory.Delete(_cpDir, recursive: true);
            Console.WriteLine($"[TRACKER] Deleted {_cpDir}");
        }
        Console.WriteLine("[TRACKER] Session cleared.");
    }

    // -- Helpers --------------------------------------------------------------─

    static string FileKey(string path)
    {
        var hash = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(path));
        return Convert.ToHexString(hash)[..8];
    }

    static string TempPathFor(string dir, string path) =>
        Path.Combine(dir, $"{FileKey(path)}_{Path.GetFileName(path)}");

    static string SanitizeLabel(string label) =>
        new string(label.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray())
            .Replace(' ', '-')[..Math.Min(label.Length, 20)];

    static void DumpCheckpointPatch(SessionData session, string workspace, CpEntry cp, string origDir)
    {
        if (_cpDir is null) return;

        var cpLabel = string.IsNullOrWhiteSpace(cp.Label) ? "" : $"-{SanitizeLabel(cp.Label)}";
        var cpFile  = Path.Combine(workspace, $"agent-patch-cp{cp.Id}-{cp.Time.Replace(":", "")[..4]}{cpLabel}.patch");

        var cpSb = new System.Text.StringBuilder();
        cpSb.AppendLine($"# WKAppBot Checkpoint {cp.Id} -- {DateTime.Now:yyyy-MM-dd} {cp.Time}  \"{cp.Label}\"");
        cpSb.AppendLine($"# Workspace: {workspace}");
        cpSb.AppendLine($"# Files: {session.TrackedFiles.Count}");
        cpSb.AppendLine("#");
        cpSb.AppendLine("# -- 이 체크포인트로 복구 ------------------------------");
        foreach (var path in session.TrackedFiles)
        {
            var tmp = TempPathFor(cp.Dir, path);
            if (File.Exists(tmp))
                cpSb.AppendLine($"#   copy \"{tmp}\" \"{path}\"");
        }
        cpSb.AppendLine("#");
        cpSb.AppendLine("# -- diff: original -> this checkpoint ----------------");

        foreach (var path in session.TrackedFiles)
        {
            var origFile = TempPathFor(origDir, path);
            var cpFile2  = TempPathFor(cp.Dir, path);
            if (!File.Exists(cpFile2)) continue;

            var left  = (File.Exists(origFile) && new FileInfo(origFile).Length > 0) ? origFile : "/dev/null";
            var rel   = Path.GetRelativePath(workspace, path).Replace('\\', '/');

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo("git", $"diff --no-index -- \"{left}\" \"{cpFile2}\"")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    UseShellExecute        = false,
                    WorkingDirectory       = workspace,
                };
                using var p = AppBotPipe.Start(psi)!;
                var diff = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (!string.IsNullOrWhiteSpace(diff))
                {
                    diff = diff
                        .Replace(left.Replace('\\', '/'), $"a/{rel}")
                        .Replace(cpFile2.Replace('\\', '/'), $"b/{rel}");
                    cpSb.Append(diff);
                }
            }
            catch (Exception ex)
            {
                cpSb.AppendLine($"# [diff error for {Path.GetFileName(path)}: {ex.Message}]");
            }
        }

        try { File.WriteAllText(cpFile, cpSb.ToString(), System.Text.Encoding.UTF8); }
        catch { }
        Console.WriteLine($"[TRACKER] cp{cp.Id} patch: {cpFile}");
    }

    static void AppendNewFileDiff(System.Text.StringBuilder sb, string workspace, string path)
    {
        if (!File.Exists(path)) return;
        var rel = Path.GetRelativePath(workspace, path).Replace('\\', '/');
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo("git", $"diff --no-index -- /dev/null \"{path}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                WorkingDirectory       = workspace,
            };
            using var p = AppBotPipe.Start(psi)!;
            var diff = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (!string.IsNullOrWhiteSpace(diff))
            {
                diff = diff.Replace(path.Replace('\\', '/'), $"b/{rel}");
                sb.Append(diff);
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine($"# [new-file diff error for {Path.GetFileName(path)}: {ex.Message}]");
        }
    }
}
