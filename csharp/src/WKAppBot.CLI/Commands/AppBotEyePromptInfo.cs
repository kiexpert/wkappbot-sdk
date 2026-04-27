// AppBotEyePromptInfo.cs -- Per-prompt-window CWD/display-name/last-output resolution.
// Single source of truth for all "hwnd -> who is this? what are they doing?" queries.
//
// Key API:
//   GetPromptDisplayInfo(hwnd)  -> (displayName, lastLine, cwdLabel)
//   GetLastOutputLine(text)     -> last meaningful line from JSONL text
//   ReadClotThoughtForCwd(cwd)  -> most recent assistant output from JSONL session
//   ExtractCwdFromVsCodeTitle() -> "... - WKAppBot - Visual Studio Code" -> D:\GitHub\WKAppBot
//   GetContextInfoForCwdEx()    -> JSONL size -> context usage %

// FlaUI removed from Eye -- ExtractCwdFromCodexWindow routed through MCP
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- JSONL last-output cache ----------------------------------------------
    // Key: CWD path. Value: (file, preview, fileLen, fileMtime)
    static readonly Dictionary<string, (string file, string? preview, long len, DateTime mtime)>
        _clotThoughtCache = new();
    static readonly Dictionary<string, string?> _projectFolderPathCache = new(StringComparer.OrdinalIgnoreCase);

    // -- VS Code title -> CWD ------------------------------------------------─

    /// <summary>
    /// Extract full CWD path from VS Code window title.
    /// Pattern: "filename - FolderName - Visual Studio Code"
    ///          "# Heading -- FolderName - Visual Studio Code"
    /// Strategy: strip VSCode suffix -> take last " - " segment -> search known roots.
    /// Fallback: reverse-map from ~/.claude/projects/ dir names.
    /// </summary>
    static string? ExtractCwdFromVsCodeTitle(string title)
    {
        var vscIdx = title.IndexOf(" - Visual Studio Code", StringComparison.OrdinalIgnoreCase);
        if (vscIdx < 0) return null;
        var withoutSuffix = title[..vscIdx];
        var lastDash = withoutSuffix.LastIndexOf(" - ", StringComparison.Ordinal);
        var folderName = lastDash >= 0 ? withoutSuffix[(lastDash + 3)..].Trim() : withoutSuffix.Trim();
        if (string.IsNullOrEmpty(folderName)) return null;
        return ResolveProjectFolderToPath(folderName);
    }

    // -- Codex window CWD extraction ----------------------------------------─

    /// <summary>
    /// Extract project folder name from Codex (OpenAI) desktop window UIA tree.
    ///
    /// [KNOWHOW] Codex CWD extraction:
    ///   Codex is an Electron app -- title is always "Codex" (no folder info in title).
    ///   UIA inspect: inside Group[aid=app-header-portal-main], the second Button holds
    ///   the currently opened project name. e.g. [Button] "WKAppBot".
    ///   Button text -> reverse-mapped to actual folder path via searchRoots.
    ///
    ///   UIA path: RootWebArea -> Group[aid=app-header-portal-main] -> Button[1] (project name)
    ///   (Button[0] = thread title, Button[1] = project name)
    /// </summary>
    static string? ExtractCwdFromCodexWindow(IntPtr hwnd)
    {
        try
        {
            // Route through MCP to avoid FlaUI loading in Eye
            var grap = $"*hwnd={hwnd.ToInt64():X}*#app-header-portal-main";
            var (output, code) = EyeMcpClient.CallAsync(
                ["a11y", "inspect", grap, "--depth", "3"], timeoutMs: 5_000).GetAwaiter().GetResult();
            if (code != 0 || string.IsNullOrWhiteSpace(output)) return null;

            // Parse inspect output for Button name (project folder)
            foreach (var line in output.Split('\n'))
            {
                if (!line.Contains("[Button]", StringComparison.OrdinalIgnoreCase)) continue;
                var q1 = line.IndexOf('"');
                var q2 = q1 >= 0 ? line.IndexOf('"', q1 + 1) : -1;
                if (q1 >= 0 && q2 > q1)
                {
                    var folderName = line.Substring(q1 + 1, q2 - q1 - 1);
                    if (!string.IsNullOrWhiteSpace(folderName))
                        return ResolveProjectFolderToPath(folderName);
                }
            }
            return null;
        }
        catch { return null; }
    }

    /// <summary>Resolve a project folder name to a full path via searchRoots + ~/.claude/projects/ reverse-map.</summary>
    static string? ResolveProjectFolderToPath(string folderName)
    {
        var searchRoots = new[] { @"D:\GitHub", @"D:\HTS_Project", @"C:\Users\kiexp\projects" };
        if (_projectFolderPathCache.TryGetValue(folderName, out var cached))
            return cached;

        foreach (var root in searchRoots)
        {
            var candidate = Path.Combine(root, folderName);
            if (Directory.Exists(candidate))
            {
                _projectFolderPathCache[folderName] = candidate;
                return candidate;
            }
        }

        foreach (var root in searchRoots)
        {
            var recursive = FindProjectFolderUnderRoot(root, folderName);
            if (!string.IsNullOrEmpty(recursive))
            {
                _projectFolderPathCache[folderName] = recursive;
                return recursive;
            }
        }
        // Reverse-map from ~/.claude/projects/ dir names
        var cpDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".claude", "projects");
        if (Directory.Exists(cpDir))
        {
            var suffix = "-" + folderName.Replace('_', '-');
            foreach (var dir in Directory.GetDirectories(cpDir))
            {
                var dirName = Path.GetFileName(dir);
                if (dirName != null && dirName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    var reversed = dirName.Replace("--", ":\\", StringComparison.Ordinal).Replace('-', '\\');
                    _projectFolderPathCache[folderName] = reversed;
                    return reversed;
                }
            }
        }
        _projectFolderPathCache[folderName] = null;
        return null;
    }

    static string? FindProjectFolderUnderRoot(string root, string folderName)
    {
        try
        {
            if (!Directory.Exists(root)) return null;

            var matches = Directory.EnumerateDirectories(root, folderName, SearchOption.AllDirectories)
                .Where(path => !ShouldIgnoreProjectFolderCandidate(path))
                .OrderBy(GetProjectFolderCandidateScore)
                .ThenBy(path => path.Count(ch => ch == '\\' || ch == '/'))
                .ThenBy(path => path.Length)
                .ToList();

            return matches.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }

    static bool ShouldIgnoreProjectFolderCandidate(string path)
    {
        var norm = path.Replace('/', '\\');
        var parts = norm.Split('\\', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            if (part.Equals("obj", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("objd", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("bin", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("debug", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("release", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("node_modules", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("packages", StringComparison.OrdinalIgnoreCase) ||
                part.Equals(".git", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    static int GetProjectFolderCandidateScore(string path)
    {
        var norm = path.Replace('/', '\\');
        int score = 0;
        if (norm.Contains(@"\Source\", StringComparison.OrdinalIgnoreCase)) score -= 30;
        if (norm.Contains(@"\GitHub\", StringComparison.OrdinalIgnoreCase)) score -= 20;
        if (norm.Contains(@"\projects\", StringComparison.OrdinalIgnoreCase)) score -= 10;
        return score;
    }

    // -- JSONL context usage --------------------------------------------------

    /// <summary>Get context usage % and JSONL age for a specific CWD's session.</summary>
    static (int pct, TimeSpan? age) GetContextInfoForCwd(string? cwd)
    {
        var (pct, age, _, _) = GetContextInfoForCwdEx(cwd);
        return (pct, age);
    }

    /// <summary>
    /// Extended: also returns JSONL path and file size (for context monitor dedup).
    /// Estimate: 20MB = 100% context. JSONL ÷ 20MB = ctx%.
    /// </summary>
    static (int pct, TimeSpan? age, string? jsonlPath, long fileSize) GetContextInfoForCwdEx(string? cwd, string? preferredHostType = null)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return (-1, null, null, 0);
        try
        {
            var session = ResolveAiSessionFileForCwd(cwd, preferredHostType);
            if (session == null || string.IsNullOrWhiteSpace(session.Value.path) || !File.Exists(session.Value.path))
                return (-1, null, null, 0);
            var latestFile = session.Value.path;
            var latestMtime = File.GetLastWriteTimeUtc(latestFile);
            var size = new FileInfo(latestFile).Length;
            var pct = size > 0 ? (int)(size / (1024.0 * 1024.0) / 20.0 * 100) : -1;
            var age = DateTime.UtcNow - latestMtime;
            return (pct, age, latestFile, size);
        }
        catch { return (-1, null, null, 0); }
    }

    /// <summary>
    /// Compute context % directly from a known JSONL file path (no CWD scan needed).
    /// Bypasses _aiSessionFileCache -- always uses the exact file from session registry.
    /// </summary>
    static (int pct, TimeSpan? age, string? jsonlPath, long fileSize) GetContextInfoForJsonl(string jsonlPath)
    {
        try
        {
            var fi = new FileInfo(jsonlPath);
            if (!fi.Exists) return (-1, null, null, 0);
            var size = fi.Length;
            var pct = size > 0 ? (int)(size / (1024.0 * 1024.0) / 20.0 * 100) : -1;
            var age = DateTime.UtcNow - fi.LastWriteTimeUtc;
            return (pct, age, jsonlPath, size);
        }
        catch { return (-1, null, null, 0); }
    }

    /// <summary>
    /// Read last user+assistant thought directly from a known JSONL path.
    /// Determines parser (claude vs codex) from hostType.
    /// </summary>
    static string? ReadClotThoughtFromJsonl(string jsonlPath, string? hostType)
    {
        try
        {
            var fi = new FileInfo(jsonlPath);
            if (!fi.Exists) return "";

            var cacheKey = $"jsonl|{jsonlPath.Replace('\\', '/').ToLowerInvariant()}";
            if (_clotThoughtCache.TryGetValue(cacheKey, out var cached) &&
                cached.file == jsonlPath && cached.len == fi.Length && cached.mtime == fi.LastWriteTimeUtc)
                return cached.preview;

            var isCodex = ClaudePromptHelper.IsCodexHostType(hostType);
            string? selected = null;
            foreach (var tailSize in new[] { 8 * 1024, 32 * 1024 })
            {
                var lines = ReadTailLinesShared(jsonlPath, tailSize);
                string bestAssistant = "", bestUser = "";
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    bool parsed = isCodex
                        ? TryExtractRoleAndTextFromCodex(line, out var role, out var text)
                        : TryExtractRoleAndText(line, out role, out text);
                    if (parsed)
                    {
                        text = NormalizePrompt(text);
                        if (string.IsNullOrWhiteSpace(text)) continue;
                        if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                            bestAssistant = text;
                        if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                            bestUser = text;
                        if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                            break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}\n🤖 {bestAssistant}";
                else if (!string.IsNullOrWhiteSpace(bestAssistant))
                    selected = bestAssistant;
                else if (!string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}";
                if (!string.IsNullOrWhiteSpace(selected)) break;
            }
            _clotThoughtCache[cacheKey] = (jsonlPath, selected, fi.Length, fi.LastWriteTimeUtc);
            return selected;
        }
        catch { return ""; }
    }

    // -- JSONL last-output reading --------------------------------------------

    /// <summary>
    /// Read most recent assistant output from Claude Code session JSONL for a CWD.
    /// CWD -> ~/.claude/projects/{mapped-name}/*.jsonl (most recently modified).
    /// Reads only last 8–32 KB (tail) -- session files grow to 35MB+.
    /// Per-CWD cache: skips re-read when file size/mtime unchanged.
    /// </summary>
    static string? ReadClotThoughtForCwd(string? cwd, string? preferredHostType = null)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        try
        {
            var session = ResolveAiSessionFileForCwd(cwd, preferredHostType);
            if (session == null || string.IsNullOrWhiteSpace(session.Value.path) || !File.Exists(session.Value.path))
                return "";
            var provider = session.Value.provider;
            var latestFile = session.Value.path;

            var fi = new FileInfo(latestFile);
            var cacheKey = $"{provider}|{cwd}";
            if (_clotThoughtCache.TryGetValue(cacheKey, out var cached) &&
                cached.file == latestFile && cached.len == fi.Length && cached.mtime == fi.LastWriteTimeUtc)
                return cached.preview;

            // Lightweight tail read: 8KB first, 32KB fallback
            string? selected = null;
            foreach (var tailSize in new[] { 8 * 1024, 32 * 1024 })
            {
                var lines = ReadTailLinesShared(latestFile, tailSize);
                string bestAssistant = "", bestUser = "";
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    bool parsed = provider == "codex"
                        ? TryExtractRoleAndTextFromCodex(line, out var role, out var text)
                        : TryExtractRoleAndText(line, out role, out text);
                    if (parsed)
                    {
                        text = NormalizePrompt(text);
                        if (string.IsNullOrWhiteSpace(text)) continue;
                        if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                            bestAssistant = text;
                        if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                            bestUser = text;
                        if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                            break;
                    }
                }
                // Show both: user prompt + assistant thought
                if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}\n🤖 {bestAssistant}";
                else if (!string.IsNullOrWhiteSpace(bestAssistant))
                    selected = bestAssistant;
                else if (!string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}";
                if (!string.IsNullOrWhiteSpace(selected)) break;
            }
            _clotThoughtCache[cacheKey] = (latestFile, selected, fi.Length, fi.LastWriteTimeUtc);
            return selected;
        }
        catch { return ""; }
    }


    static (string provider, string path)? ResolveAiSessionFileForCwd(string? cwd, string? preferredHostType = null)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return null;

        // No cache -- always re-scan so newchat picks up the newest JSONL immediately.
        return ClaudePromptHelper.IsCodexHostType(preferredHostType)
            ? ResolveCodexSessionFileForCwd(cwd) ?? ResolveClaudeSessionFileForCwd(cwd)
            : ResolveClaudeSessionFileForCwd(cwd) ?? ResolveCodexSessionFileForCwd(cwd);
    }

    static (string provider, string path)? ResolveClaudeSessionFileForCwd(string cwd)
    {
        var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
        var projDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".claude", "projects", projName);
        if (!Directory.Exists(projDir)) return null;

        string? latestFile = null;
        DateTime latestMtime = DateTime.MinValue;
        foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
        {
            try
            {
                var mtime = File.GetLastWriteTimeUtc(jsonl);
                if (mtime > latestMtime) { latestMtime = mtime; latestFile = jsonl; }
            }
            catch { }
        }
        return latestFile != null ? ("claude", latestFile) : null;
    }

    static (string provider, string path)? ResolveCodexSessionFileForCwd(string cwd)
    {
        var latestFile = FindCodexSessionJsonl(cwd);
        return string.IsNullOrWhiteSpace(latestFile) ? null : ("codex", latestFile);
    }

    static bool TryExtractRoleAndTextFromCodex(string line, out string role, out string text)
    {
        role = "";
        text = "";

        try
        {
            var node = JsonNode.Parse(line);
            if (node == null) return false;

            var type = node["type"]?.GetValue<string>() ?? "";
            if (string.Equals(type, "response_item", StringComparison.OrdinalIgnoreCase))
            {
                var payload = node["payload"];
                if (!string.Equals(payload?["type"]?.GetValue<string>(), "message", StringComparison.OrdinalIgnoreCase))
                    return false;

                var msgRole = payload?["role"]?.GetValue<string>() ?? "";
                if (msgRole is not ("assistant" or "user"))
                    return false;

                var parts = new List<string>();
                if (payload?["content"] is JsonArray contentArray)
                {
                    foreach (var item in contentArray)
                    {
                        var part =
                            item?["text"]?.GetValue<string>() ??
                            item?["output_text"]?.GetValue<string>() ??
                            item?["input_text"]?.GetValue<string>();
                        if (!string.IsNullOrWhiteSpace(part))
                            parts.Add(part);
                    }
                }

                text = string.Join("\n", parts.Where(p => !string.IsNullOrWhiteSpace(p)));
                role = msgRole;
                return !string.IsNullOrWhiteSpace(text);
            }

            if (string.Equals(type, "event_msg", StringComparison.OrdinalIgnoreCase))
            {
                var payload = node["payload"];
                var msgType = payload?["type"]?.GetValue<string>() ?? "";
                if (string.Equals(msgType, "agent_message", StringComparison.OrdinalIgnoreCase))
                {
                    role = "assistant";
                    text = payload?["message"]?.GetValue<string>() ?? "";
                    return !string.IsNullOrWhiteSpace(text);
                }
                if (string.Equals(msgType, "user_message", StringComparison.OrdinalIgnoreCase))
                {
                    role = "user";
                    text = payload?["message"]?.GetValue<string>() ?? "";
                    return !string.IsNullOrWhiteSpace(text);
                }
            }
        }
        catch { }

        return false;
    }

    // -- Per-prompt display info ----------------------------------------------

    /// <summary>
    /// 최근 AI 출력에서 의미 있는 마지막 한 줄 추출 (4자 이상, 최대 120자).
    /// ReadClotThoughtForCwd() 결과나 ai.status 후보 텍스트에 공통 적용.
    /// </summary>
    internal static string GetLastOutputLine(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return "(empty)";
        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var last = lines.LastOrDefault(l => l.Length >= 4) ?? text.Trim();
        return last.Length > 120 ? last[..120] + "..." : last;
    }

    /// <summary>
    /// Returns (displayName, lastLine, cwdLabel) for a single hwnd in one call.
    /// Single entry point for standard info lookup when iterating prompt windows.
    ///
    /// [KNOWHOW] CWD detection war stories -- don't repeat these mistakes!
    ///
    ///   Problem 1 -- PID-based CWD is wrong with multiple VS Code windows:
    ///     Two VS Code windows (WKAppBot, lucy_securepad) share the same PID (Code.exe 37252).
    ///     _cachedCards.FirstOrDefault(c => c.ParentPid == pid) -> always hits the first card
    ///     -> only one side's CWD ever appears, display names get swapped.
    ///     ★ Fix: always extract CWD from VS Code window title first (ExtractCwdFromVsCodeTitle).
    ///
    ///   Problem 2 -- GetAllCachedPrompts() missing VS Code/Codex entries:
    ///     ClaudePromptHelper.FindAllPrompts() did not add VS Code/Codex items to _turnFormCache,
    ///     so GetAllCachedPrompts() returned an empty list.
    ///     -> claudeInstances empty -> no status streaming at all.
    ///     ★ Fix: FindAllPrompts() now adds vscode-claudecode/codex-desktop to _turnFormCache too.
    ///
    ///   Problem 3 -- Display name fallback was "most recently updated card" -> wrong instance:
    ///     GetBotUsernameFromCachedCards() step 2 picked the most recent card;
    ///     if lucy_securepad ticked more recently, WKAppBot commands sent under lucy's name.
    ///     ★ Fix: callerHwnd hint (step 0) -> title-based CWD match (step 1b) -> direct build (step 1c)
    ///             most-recent fallback (step 2) only when callerCwd is unknown.
    ///
    ///   Problem 4 -- Slack thread routing: matching via display name parsing was fragile:
    ///     "클롣[WG-lucy_securepad]" -> extract cwdTag -> title Contains -> unreliable.
    ///     ★ Fix: reverse-lookup threadTs == state.SlackStatusTs (FindHwndBySlackStatusTs)
    ///             find hwnd directly -- window handle is the authoritative key.
    ///
    /// displayName : Slack display name (e.g. "클롣[WG-WKAppBot]")
    /// lastLine    : last assistant output line from JSONL
    /// cwdLabel    : abbreviated CWD (e.g. "WG-WKAppBot")
    /// </summary>
    internal static (string displayName, string lastLine, string cwdLabel) GetPromptDisplayInfo(IntPtr hwnd)
    {
        // 1. PromptInfo from cache
        var pi = FindAllPromptsViaMcp()
            .FirstOrDefault(p => p.WindowHandle == hwnd);
        if (pi == null)
            pi = ClaudePromptHelper.GetAllCachedPrompts()
                .FirstOrDefault(p => p.WindowHandle == hwnd);

        // 2. CWD: 호스트 타입별 추출 우선순위
        //   VS Code -> 타이틀 우선 (PID 공유 문제, 문제1 참조)
        //   Codex   -> UIA app-header-portal-main 버튼 (타이틀은 항상 "Codex")
        //   기타    -> PID 카드 fallback
        string? cwd = null;
        if (ClaudePromptHelper.IsVsCodeHostType(pi?.HostType) && !string.IsNullOrEmpty(pi?.WindowTitle))
            cwd = ExtractCwdFromVsCodeTitle(pi.WindowTitle);
        else if (pi?.HostType == "codex-desktop")
            cwd = ExtractCwdFromCodexWindow(hwnd);
        if (string.IsNullOrEmpty(cwd))
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            var card = _cachedCards.FirstOrDefault(c => c.ParentPid == (int)pid);
            cwd = card?.Cwd;
        }

        // 3. CwdLabel (시스템/설치폴더 -> 태그 없음)
        var cwdLabel = (!string.IsNullOrEmpty(cwd) && !IsSystemOrInstallDirectory(cwd))
            ? AbbreviateCwd(cwd) : "";

        // 4. Display name
        string displayName = pi != null
            ? FormatSlackDisplayName(pi.HostType, cwdLabel)
            : FormatSlackDisplayName(null, cwdLabel);

        // 5. Last output line from JSONL
        var lastLine = GetLastOutputLine(ReadClotThoughtForCwd(cwd, pi?.HostType));

        return (displayName, lastLine, cwdLabel);
    }
}
