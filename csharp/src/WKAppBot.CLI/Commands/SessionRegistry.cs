// SessionRegistry.cs — session file-based card system (replaces tick-only cards).
// Each MCP server / CLI invocation registers a session file in runtime/sessions/.
// Eye reads these files to build cards. Workers (WKAPPBOT_WORKER=1) never register.
//
// Session file: runtime/sessions/{pid}.json
// Heartbeat: mtime updated every command. Eye cleans stale (>5min) + dead PIDs.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>Session info written to runtime/sessions/{pid}.json.</summary>
    sealed class SessionInfo
    {
        public string Id { get; set; } = "";
        public int Pid { get; set; }
        public int ParentPid { get; set; }
        public string HostType { get; set; } = "";       // vscode, claude-desktop, codex, copilot, terminal, mcp-client
        public string HostTitle { get; set; } = "";
        public string Cwd { get; set; } = "";
        public string PromptHwnd { get; set; } = "";
        public string SessionJsonl { get; set; } = "";    // ~/.claude/projects/.../session.jsonl
        public string StartedAt { get; set; } = "";
        public string Heartbeat { get; set; } = "";
        public string LastCommand { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public string LastTag { get; set; } = "";
        public bool IsWorker { get; set; }
    }

    static string SessionsDir => Path.Combine(DataDir, "runtime", "sessions");

    /// <summary>Current session file path (null if not registered).</summary>
    static string? _sessionFilePath;
    static readonly object _sessionLock = new();

    /// <summary>
    /// Register this process as an active session. Call once at startup.
    /// Workers (WKAPPBOT_WORKER=1) skip registration.
    /// </summary>
    static void SessionRegister(string? cwd, string? hostType = null, string? hostTitle = null, string? promptHwnd = null)
    {
        if (RunningInEye && !IsMcpMode) return; // Eye pipe commands don't register (Eye owns the card)

        var pid = Environment.ProcessId;
        var ppid = GetParentPid(pid);
        var sessDir = SessionsDir;
        Directory.CreateDirectory(sessDir);

        var sessionJsonl = FindSessionJsonl(cwd);

        var info = new SessionInfo
        {
            Id = $"session-{pid}",
            Pid = pid,
            ParentPid = ppid,
            HostType = hostType ?? DetectHostType(ppid),
            HostTitle = hostTitle ?? "",
            Cwd = cwd ?? "",
            PromptHwnd = promptHwnd ?? "",
            SessionJsonl = sessionJsonl ?? "",
            StartedAt = DateTime.UtcNow.ToString("O"),
            Heartbeat = DateTime.UtcNow.ToString("O"),
            IsWorker = false,
        };

        _sessionFilePath = Path.Combine(sessDir, $"{pid}.json");
        try
        {
            File.WriteAllText(_sessionFilePath, JsonSerializer.Serialize(info));
        }
        catch { }
    }

    /// <summary>Update session heartbeat + current command/status. Fast (< 1ms).</summary>
    static void SessionUpdate(string? command = null, string? tag = null, string? status = null)
    {
        if (_sessionFilePath == null || !File.Exists(_sessionFilePath)) return;
        lock (_sessionLock)
        {
            try
            {
                var json = File.ReadAllText(_sessionFilePath);
                var info = JsonSerializer.Deserialize<SessionInfo>(json);
                if (info == null) return;

                info.Heartbeat = DateTime.UtcNow.ToString("O");
                if (command != null) info.LastCommand = command;
                if (tag != null) info.LastTag = tag;
                if (status != null) info.LastStatus = status;

                // Update promptHwnd if newly available
                if (string.IsNullOrEmpty(info.PromptHwnd)
                    && EyeCmdPipeServer.CallerHwnd.Value is IntPtr h && h != IntPtr.Zero)
                    info.PromptHwnd = $"0x{h.ToInt64():X}";

                File.WriteAllText(_sessionFilePath, JsonSerializer.Serialize(info));
            }
            catch { }
        }
    }

    /// <summary>Unregister session (process exit). Best-effort delete.</summary>
    static void SessionUnregister()
    {
        if (_sessionFilePath == null) return;
        try { File.Delete(_sessionFilePath); } catch { }
        _sessionFilePath = null;
    }

    /// <summary>Read all active sessions. Eye calls this to build cards.</summary>
    static List<SessionInfo> ReadActiveSessions(int staleSeconds = 300)
    {
        var result = new List<SessionInfo>();
        var dir = SessionsDir;
        if (!Directory.Exists(dir)) return result;

        var now = DateTime.UtcNow;
        foreach (var file in Directory.GetFiles(dir, "*.json"))
        {
            try
            {
                var json = File.ReadAllText(file);
                var info = JsonSerializer.Deserialize<SessionInfo>(json);
                if (info == null || info.IsWorker) continue;

                // Stale check: heartbeat too old
                if (DateTime.TryParse(info.Heartbeat, out var hb) && (now - hb.ToUniversalTime()).TotalSeconds > staleSeconds)
                {
                    try { File.Delete(file); } catch { }
                    continue;
                }

                // Dead PID check
                try { System.Diagnostics.Process.GetProcessById(info.Pid); }
                catch
                {
                    try { File.Delete(file); } catch { }
                    continue;
                }

                // Skip empty/system32 CWD
                var cwdLower = (info.Cwd ?? "").Replace('\\', '/').ToLowerInvariant();
                if (string.IsNullOrEmpty(cwdLower) || cwdLower.EndsWith("/system32"))
                    continue;

                result.Add(info);
            }
            catch { }
        }
        return result;
    }

    /// <summary>Detect host type from parent process chain.</summary>
    static string DetectHostType(int parentPid)
    {
        int cur = parentPid;
        for (int depth = 0; cur > 0 && depth < 12; depth++)
        {
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(cur);
                var name = (p.ProcessName ?? "").ToLowerInvariant();
                var title = GetMainWindowTitleSafe(p);

                if (name == "code" || title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                    return "vscode";
                if (name == "claude" || name.StartsWith("claude"))
                    return "claude-desktop";
                if (name == "codex" || title.Contains("Codex", StringComparison.OrdinalIgnoreCase))
                    return "codex";
                if (name == "cursor" || title.Contains("Cursor", StringComparison.OrdinalIgnoreCase))
                    return "cursor";
                if (name == "copilot")
                    return "copilot";

                var next = GetParentPid(cur);
                if (next <= 0 || next == cur) break;
                cur = next;
            }
            catch { break; }
        }
        return "terminal";
    }

    /// <summary>Find Claude session JSONL for a given CWD via ~/.claude/projects/ mapping.</summary>
    static string? FindSessionJsonl(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return null;
        try
        {
            // ~/.claude/projects/{cwd-slug}/ → find most recent .jsonl
            var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
            var projDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "projects");
            if (!Directory.Exists(projDir)) return null;

            // Find directory matching this CWD (partial match on slug)
            var cwdSlug = projName.ToLowerInvariant().TrimStart('-');
            foreach (var dir in Directory.GetDirectories(projDir))
            {
                var dirName = Path.GetFileName(dir)?.ToLowerInvariant() ?? "";
                if (dirName == cwdSlug || dirName.EndsWith("-" + Path.GetFileName(cwd)?.ToLowerInvariant()))
                {
                    // Find most recent .jsonl
                    var jsonls = Directory.GetFiles(dir, "*.jsonl")
                        .OrderByDescending(File.GetLastWriteTimeUtc)
                        .FirstOrDefault();
                    if (jsonls != null) return jsonls;
                }
            }
        }
        catch { }
        return null;
    }
}
