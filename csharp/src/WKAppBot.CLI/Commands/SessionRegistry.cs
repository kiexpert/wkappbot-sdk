// SessionRegistry.cs -- session file-based card system (replaces tick-only cards).
// Each MCP server / CLI invocation registers a session file in runtime/sessions/.
// Eye reads these files to build cards. Workers (WKAPPBOT_WORKER=1) never register.
//
// Session file: runtime/sessions/{pid}.json
// Heartbeat: mtime updated every command. Eye cleans stale (>5min) + dead PIDs.

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json.Nodes;
using WKAppBot.Win32.Window;

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

        var resolvedHostType = hostType ?? DetectHostType(ppid);
        var sessionJsonl = FindSessionJsonl(cwd, resolvedHostType);

        var info = new SessionInfo
        {
            Id = $"session-{pid}",
            Pid = pid,
            ParentPid = ppid,
            HostType = resolvedHostType,
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

    /// <summary>Detect host type from parent process chain + environment variables.</summary>
    static string DetectHostType(int parentPid)
    {
        // Env-var detection (fastest, most reliable -- set by the AI host in all child processes).
        // Check CODEX_HOME first: Codex sets this even in VS Code terminal mode.
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CODEX_HOME")))
            return "vscode-codex";

        // CLAUDE_CODE_ENTRYPOINT=claude-vscode: set by Claude Code VS Code extension.
        var claudeEntrypoint = Environment.GetEnvironmentVariable("CLAUDE_CODE_ENTRYPOINT");
        if (!string.IsNullOrWhiteSpace(claudeEntrypoint))
        {
            // Classify from VSCODE_PID window title if available.
            var vscodePidStr = Environment.GetEnvironmentVariable("VSCODE_PID");
            if (!string.IsNullOrWhiteSpace(vscodePidStr) && int.TryParse(vscodePidStr, out var vscodePid) && vscodePid > 0)
            {
                try
                {
                    var vsProc = System.Diagnostics.Process.GetProcessById(vscodePid);
                    return ClaudePromptHelper.ClassifyVsCodeHostType(GetMainWindowTitleSafe(vsProc));
                }
                catch { }
            }
            return "vscode-claudecode";
        }

        // VSCODE_PID without CLAUDE_CODE_ENTRYPOINT: generic VS Code terminal (Codex ext, Copilot, etc.)
        // Only use as fallback -- real AI host env vars should have been checked above.
        var vscodePidOnly = Environment.GetEnvironmentVariable("VSCODE_PID");
        if (!string.IsNullOrWhiteSpace(vscodePidOnly) && int.TryParse(vscodePidOnly, out var vscPid) && vscPid > 0)
        {
            try
            {
                var vsProc = System.Diagnostics.Process.GetProcessById(vscPid);
                return ClaudePromptHelper.ClassifyVsCodeHostType(GetMainWindowTitleSafe(vsProc));
            }
            catch { }
            return "vscode-claudecode";
        }

        // Parent process chain walk (handles Claude Desktop, standalone Codex, Cursor, etc.)
        int cur = parentPid;
        for (int depth = 0; cur > 0 && depth < 12; depth++)
        {
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(cur);
                var name = (p.ProcessName ?? "").ToLowerInvariant();
                var title = GetMainWindowTitleSafe(p);

                if (name == "code" || title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                    return ClaudePromptHelper.ClassifyVsCodeHostType(title);
                if (name == "claude" || name.StartsWith("claude"))
                    return "claude-desktop";
                if (name == "codex" || title.Contains("Codex", StringComparison.OrdinalIgnoreCase))
                    return "codex-desktop";   // standalone Codex Desktop app -> 코덳앱
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
    static string? FindSessionJsonl(string? cwd, string? hostType = null)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return null;
        if (ClaudePromptHelper.IsCodexHostType(hostType))
            return FindCodexSessionJsonl(cwd) ?? FindClaudeSessionJsonl(cwd);
        return FindClaudeSessionJsonl(cwd) ?? FindCodexSessionJsonl(cwd);
    }

    static string? FindClaudeSessionJsonl(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return null;
        try
        {
            // ~/.claude/projects/{cwd-slug}/ -> find most recent .jsonl
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

    static readonly Dictionary<string, string?> _codexSessionJsonlCache = new(StringComparer.OrdinalIgnoreCase);

    static string? FindCodexSessionJsonl(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return null;

        var cacheKey = cwd.Replace('/', '\\').TrimEnd('\\');
        if (_codexSessionJsonlCache.TryGetValue(cacheKey, out var cached) &&
            !string.IsNullOrWhiteSpace(cached) &&
            File.Exists(cached))
            return cached;

        try
        {
            var sessionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".codex", "sessions");
            if (!Directory.Exists(sessionsDir)) return null;

            var normalizedCwd = cacheKey.ToLowerInvariant();
            var recentFiles = Directory.EnumerateFiles(sessionsDir, "*.jsonl", SearchOption.AllDirectories)
                .OrderByDescending(File.GetLastWriteTimeUtc)
                .Take(200);

            foreach (var file in recentFiles)
            {
                var sessionCwd = TryReadCodexSessionCwd(file);
                if (string.IsNullOrWhiteSpace(sessionCwd)) continue;

                var normalizedSessionCwd = sessionCwd.Replace('/', '\\').TrimEnd('\\').ToLowerInvariant();
                if (!string.Equals(normalizedSessionCwd, normalizedCwd, StringComparison.OrdinalIgnoreCase))
                    continue;

                _codexSessionJsonlCache[cacheKey] = file;
                return file;
            }
        }
        catch { }

        _codexSessionJsonlCache[cacheKey] = null;
        return null;
    }

    static string? TryReadCodexSessionCwd(string path)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            using var reader = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var firstLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(firstLine)) return null;

            var node = JsonNode.Parse(firstLine);
            if (!string.Equals(node?["type"]?.GetValue<string>(), "session_meta", StringComparison.OrdinalIgnoreCase))
                return null;

            return node?["payload"]?["cwd"]?.GetValue<string>();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Inject a user+assistant message pair into a Claude Code session JSONL file.
    /// Appended to the end -- Claude Code renders new entries on next scroll/reload.
    /// </summary>
    /// <param name="jsonlPath">Path to the .jsonl session file.</param>
    /// <param name="cwd">Workspace CWD (written into each entry).</param>
    /// <param name="userText">Text for the injected user turn.</param>
    /// <param name="assistantText">Text for the injected assistant turn.</param>
    /// <param name="model">Model label shown in Claude Code (e.g. "gemini-2.5-pro").</param>
    /// <returns>True if both entries were written successfully.</returns>
    internal static bool InjectMessageToClaudeSession(
        string jsonlPath, string cwd, string userText, string assistantText, string model = "gemini-2.5-pro")
    {
        try
        {
            // Read last non-empty line to extract chain context
            string lastLine = "";
            using (var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                    if (!string.IsNullOrWhiteSpace(line)) lastLine = line;
            }
            if (string.IsNullOrEmpty(lastLine)) return false;

            var last = JsonNode.Parse(lastLine);
            var lastUuid   = last?["uuid"]?.GetValue<string>() ?? Guid.NewGuid().ToString();
            var sessionId  = last?["sessionId"]?.GetValue<string>() ?? Path.GetFileNameWithoutExtension(jsonlPath);
            var version    = last?["version"]?.GetValue<string>() ?? "2.1.88";
            var gitBranch  = last?["gitBranch"]?.GetValue<string>() ?? "main";
            var cwdNorm    = cwd.Replace('/', '\\');

            var now          = DateTime.UtcNow;
            var userUuid     = Guid.NewGuid().ToString();
            var promptId     = Guid.NewGuid().ToString();
            var assistantUuid = Guid.NewGuid().ToString();
            var msgId        = "msg_gem_" + Guid.NewGuid().ToString("N")[..16];

            var userEntry = new JsonObject
            {
                ["parentUuid"]    = lastUuid,
                ["isSidechain"]   = false,
                ["promptId"]      = promptId,
                ["type"]          = "user",
                ["uuid"]          = userUuid,
                ["timestamp"]     = now.ToString("O"),
                ["userType"]      = "external",
                ["entrypoint"]    = "claude-vscode",
                ["cwd"]           = cwdNorm,
                ["sessionId"]     = sessionId,
                ["version"]       = version,
                ["gitBranch"]     = gitBranch,
                ["message"]       = new JsonObject
                {
                    ["role"]    = "user",
                    ["content"] = new JsonArray(new JsonObject { ["type"] = "text", ["text"] = userText })
                }
            };

            var assistantEntry = new JsonObject
            {
                ["parentUuid"]  = userUuid,
                ["isSidechain"] = false,
                ["type"]        = "assistant",
                ["uuid"]        = assistantUuid,
                ["timestamp"]   = now.AddSeconds(1).ToString("O"),
                ["userType"]    = "external",
                ["entrypoint"]  = "claude-vscode",
                ["cwd"]         = cwdNorm,
                ["sessionId"]   = sessionId,
                ["version"]     = version,
                ["gitBranch"]   = gitBranch,
                ["message"]     = new JsonObject
                {
                    ["model"]         = model,
                    ["id"]            = msgId,
                    ["type"]          = "message",
                    ["role"]          = "assistant",
                    ["content"]       = new JsonArray(new JsonObject { ["type"] = "text", ["text"] = assistantText }),
                    ["stop_reason"]   = "end_turn",
                    ["stop_sequence"] = JsonValue.Create((string?)null),
                    ["usage"]         = new JsonObject { ["input_tokens"] = 0, ["output_tokens"] = 0 }
                }
            };

            using var sw = new StreamWriter(jsonlPath, append: true, new UTF8Encoding(false));
            sw.WriteLine(userEntry.ToJsonString());
            sw.WriteLine(assistantEntry.ToJsonString());
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] InjectMessageToClaudeSession failed: {ex.Message}");
            return false;
        }
    }
}
