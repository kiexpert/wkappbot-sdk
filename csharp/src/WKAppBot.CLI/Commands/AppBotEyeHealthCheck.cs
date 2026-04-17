using System.Diagnostics;
using System.Text.Json;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // Commands to skip when building idle-after text (meta + communication noise)
    static readonly HashSet<string> _idleSkipCommands = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "slack", "tick" };

    /// <summary>
    /// Get age (seconds since last modification) of the most recent Claude Code session JSONL.
    /// Claude Code writes to ~/.claude/projects/{project}/*.jsonl during ALL activity
    /// (file reads, edits, builds, tool calls) -- not just wkappbot commands.
    /// Returns 9999 if no session file found.
    /// </summary>
    static double GetClaudeCodeSessionAge()
    {
        try
        {
            var claudeDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects");
            if (!Directory.Exists(claudeDir)) return 9999;

            // Find most recently modified .jsonl across all projects
            DateTime latestUtc = DateTime.MinValue;
            foreach (var projDir in Directory.GetDirectories(claudeDir))
            {
                try
                {
                    foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
                    {
                        var mtime = File.GetLastWriteTimeUtc(jsonl);
                        if (mtime > latestUtc) latestUtc = mtime;
                    }
                }
                catch { }
            }

            if (latestUtc == DateTime.MinValue) return 9999;
            return (DateTime.UtcNow - latestUtc).TotalSeconds;
        }
        catch { return 9999; }
    }

    /// <summary>
    /// Read the last meaningful tick's tag from eye_ticks.jsonl.
    /// Skips meta tags AND communication commands (slack, eye) to find actual work.
    /// Returns like "publish/build" or "inspect" -- shown as idle-after text.
    /// </summary>
    static string? GetLastTickTag()
    {
        try
        {
            if (!File.Exists(EyeTicksPath)) return null;
            var lines = ReadTailLinesShared(EyeTicksPath, 8192); // ~last 30 ticks (may need deeper scan)
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var t = System.Text.Json.JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t == null) continue;
                // Skip meta tags (eye, snapshot, validate, help)
                if (IsMetaTag(t.Tag)) continue;
                // Skip communication commands (slack send/reply, eye tick)
                if (_idleSkipCommands.Contains(t.Command ?? "")) continue;
                var tag = t.Tag ?? "";
                // Include command for context: "publish/build" or "run/click"
                if (!string.IsNullOrEmpty(t.Command) && !tag.StartsWith(t.Command))
                    return $"{t.Command}/{tag}";
                return tag;
            }
        }
        catch { }
        return null;
    }

    static (string progress, string done, string next, string block) BuildKroStatus3(EyeTick t)
    {
        var report = $"{t.Command}/{t.Tag}";
        var status = (t.Status ?? "").ToLowerInvariant();
        var aiRecent = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds <= 45;

        if (status == "start")
            return (report, "작업 시작", "응답 처리 중", "");

        if (status.StartsWith("step:"))
        {
            if (aiRecent)
                return (report, "최근 대화 수신", "응답 처리 중", "");
            return (report, "중간 단계", "상태 갱신 대기", "");
        }

        if (status.StartsWith("end:"))
        {
            var codeText = status[4..].Trim();
            if (int.TryParse(codeText, out var code) && code == 0)
            {
                if (aiRecent)
                    return ("응답 처리 중", "최근 대화 수신", "후속 입력 대기", "");
                return (report, "작업 완료", "다음 작업 대기", "");
            }
            return (report, "오류 종료", "에러 분석/폴백", $"종료코드 {codeText}");
        }

        if (aiRecent)
            return (report, "최근 대화 수신", "응답 처리 중", "");

        return (report, "상태 대기", "상태 갱신 대기", "");
    }

    // Meta tags: diagnostic/overhead commands that shouldn't hide meaningful work in cards
    // e.g. "eye tick" checks status, "snapshot" captures state -- not the real work itself
    static readonly HashSet<string> _metaTags = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "snapshot", "eye tick", "validate", "help" };

    static bool IsMetaTag(string? tag) =>
        !string.IsNullOrWhiteSpace(tag) && _metaTags.Contains(tag!);

    /// <summary>
    /// Dead-card detector + WM_NULL health check.
    /// For each card:
    ///   1. If PID/HWND is gone -> [DEAD_CARD] Slack alert + zombie kill attempt.
    ///   2. If PID exists but WM_NULL > 100ms -> [SLOW_CARD] Slack alert + card marked "불량".
    /// Results cached in _cardHealthCache; dead pids cached in _reportedDeadPids to suppress repeats.
    /// </summary>
    static void CheckAndReportDeadCards(List<EyeParentCard> cards)
    {
        foreach (var card in cards.ToList())
        {
            var pid = card.ParentPid;
            if (pid <= 0) continue;

            // -- Step 1: is the process/window still alive? --
            bool isPromptDiscovered = card.LastTag == "prompt-discovered";
            bool alive;
            IntPtr hwnd = IntPtr.Zero;
            if (isPromptDiscovered)
            {
                // ParentPid = HWND for prompt-discovered cards
                hwnd = (IntPtr)pid;
                alive = WKAppBot.Win32.Native.NativeMethods.IsWindow(hwnd);
            }
            else
            {
                try
                {
                    using var p = Process.GetProcessById(pid);
                    alive = !p.HasExited;
                    // Find any top-level HWND for this PID (for WM_NULL health check)
                    WKAppBot.Win32.Native.NativeMethods.EnumWindows((h, _) =>
                    {
                        WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(h, out uint wpid);
                        if (wpid == (uint)pid && hwnd == IntPtr.Zero) hwnd = h;
                        return hwnd == IntPtr.Zero; // stop once found
                    }, IntPtr.Zero);
                }
                catch { alive = false; }
            }

            if (!alive)
            {
                if (_reportedDeadPids.Add(pid))
                {
                    var cwdTag = AbbreviateCwd(card.Cwd);
                    var label = string.IsNullOrEmpty(cwdTag) ? $"{card.ParentName}:{pid}" : $"[{cwdTag}] {card.ParentName}:{pid}";
                    Console.Error.WriteLine($"[DEAD_CARD] {label} -- process gone");
                    _cardHealthCache[pid] = "dead";
                    // Zombie cleanup attempt (no-op if already gone)
                    try { Process.GetProcessById(pid).Kill(); } catch { }
                }
                cards.Remove(card);
                continue;
            }

            // -- Step 2: WM_NULL health check (0 = skip if no HWND found) --
            if (hwnd == IntPtr.Zero) continue;
            if (_reportedDeadPids.Contains(pid)) continue;

            var sw = Stopwatch.StartNew();
            WKAppBot.Win32.Native.NativeMethods.SendMessageTimeoutW(
                hwnd, WKAppBot.Win32.Native.NativeMethods.WM_NULL,
                IntPtr.Zero, IntPtr.Zero,
                WKAppBot.Win32.Native.NativeMethods.SMTO_ABORTIFHUNG,
                100, out _);
            sw.Stop();

            // health% = max(0, 100 - responseMs): 1ms->99%, 99ms->1%, 100ms+->0%(불량)
            var responseMs = (int)sw.ElapsedMilliseconds;
            var healthPct = Math.Max(0, 100 - responseMs);
            var health = healthPct == 0 ? "불량" : (healthPct < 50 ? "느림" : "ok");

            var prevHealth = _cardHealthCache.GetValueOrDefault(pid, "ok");
            _cardHealthCache[pid] = health;

            if (health == "불량" && prevHealth != "불량")
            {
                var cwdTag = AbbreviateCwd(card.Cwd);
                var label = string.IsNullOrEmpty(cwdTag) ? $"{card.ParentName}:{pid}" : $"[{cwdTag}] {card.ParentName}:{pid}";
                Console.Error.WriteLine($"[SLOW_CARD] {label} -- WM_NULL={responseMs}ms (건강{healthPct}%)");
            }
            else if (health != "불량" && prevHealth == "불량")
            {
                Console.Error.WriteLine($"[HEALTH] {card.ParentName}:{pid} recovered -> {responseMs}ms (건강{healthPct}%)");
            }

            // Annotate card for display (show health% if not perfect)
            if (healthPct < 100)
                card.LastStatus = $"{card.LastStatus} [건강{healthPct}%]".Trim();
        }
    }

    /// <summary>Supplement tick-based cards with VS Code windows that have no tick (idle Claude Code sessions).</summary>
    static void SupplementCardsFromPrompts(List<EyeParentCard> cards)
    {
        try
        {
            static string HostFamily(string? hostType) =>
                ClaudePromptHelper.IsCodexHostType(hostType) ? "codex"
                : ClaudePromptHelper.IsVsCodeHostType(hostType) || string.Equals(hostType, "claude-desktop", StringComparison.OrdinalIgnoreCase) ? "claude"
                : (hostType ?? "").ToLowerInvariant();

            static string BuildCardKey(string cwd, string? hostType) =>
                $"{cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/')}|{HostFamily(hostType)}";

            var cardKeys = new HashSet<string>(cards
                    .Where(c => !string.IsNullOrWhiteSpace(c.Cwd))
                    .Select(c => BuildCardKey(c.Cwd, c.HostType)),
                StringComparer.OrdinalIgnoreCase);

            // 1s cooldown after last scan -- FindAllPrompts is fast now (per-hwnd UIA cache in ClaudePromptHelper),
            // but EnumWindows + process name lookup still costs ~5-20ms per tick; limit to once/second.
            List<ClaudePromptHelper.PromptInfo> allPrompts;
            var now = DateTime.UtcNow;
            if (_cachedAllPrompts != null && (now - _lastFindAllPromptsAt).TotalMilliseconds < 60_000)
            {
                allPrompts = _cachedAllPrompts;
            }
            else
            {
                var sw = Stopwatch.StartNew();
                allPrompts = FindAllPromptsViaMcp();
                sw.Stop();
                _cachedAllPrompts = allPrompts;
                _lastFindAllPromptsAt = DateTime.UtcNow;
                // Cache appbot master prompt (WKAppBot VS Code -- always-on relay target)
                CachedAppbotMasterPrompt = allPrompts.FirstOrDefault(p =>
                    p.WindowTitle.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) &&
                    ClaudePromptHelper.IsVsCodeHostType(p.HostType));
                if (sw.ElapsedMilliseconds > 50)
                    Console.Error.WriteLine($"[EYE] FindAllPrompts(MCP): {sw.ElapsedMilliseconds}ms ({allPrompts.Count} prompts)");
            }
            foreach (var p in allPrompts)
            {
                if (!ClaudePromptHelper.IsVsCodeHostType(p.HostType) && p.HostType != "Code") continue;
                var cwd = ExtractCwdFromVsCodeTitle(p.WindowTitle);
                if (string.IsNullOrEmpty(cwd)) continue;
                var cardKey = BuildCardKey(cwd, p.HostType);
                if (cardKeys.Contains(cardKey)) continue;
                cards.Add(new EyeParentCard
                {
                    ParentPid = p.WindowHandle.ToInt32(),
                    ParentName = "Code",
                    ParentTitle = p.WindowTitle,
                    HostType = p.HostType,
                    LastTag = "prompt-discovered",
                    LastStatus = "idle",
                    LastTsUtc = DateTime.UtcNow.AddMinutes(-1),
                    Cwd = cwd,
                });
                cardKeys.Add(cardKey);
            }
        }
        catch { }
    }

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 86400)
    {
        // KEY = sessionJsonl path when available (one card per conversation, survives PID restart).
        // Falls back to promptHwnd, then CWD (legacy).
        var cards = new Dictionary<string, EyeParentCard>(StringComparer.OrdinalIgnoreCase);
        var now = DateTime.UtcNow;

        // -- Phase 1: Session registry (primary -- MCP servers) --
        // Session files are authoritative: they have correct CWD, host type, and heartbeat.
        try
        {
            var sessions = ReadActiveSessions(staleSeconds);
            foreach (var s in sessions)
            {
                var cwdKey = (s.Cwd ?? "").Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                if (string.IsNullOrEmpty(cwdKey)) continue;

                // Use SessionJsonl as primary key -- deduplicates multiple PIDs sharing the same conversation.
                var jsonlKey = !string.IsNullOrEmpty(s.SessionJsonl)
                    ? s.SessionJsonl.Replace('\\', '/').ToLowerInvariant()
                    : null;
                var cardKey = jsonlKey ?? (!string.IsNullOrEmpty(s.PromptHwnd) ? s.PromptHwnd : cwdKey);
                var hbUtc = DateTime.TryParse(s.Heartbeat, out var hb) ? hb.ToUniversalTime() : now;

                // When multiple sessions point to the same JSONL, keep the most recent heartbeat.
                if (cards.TryGetValue(cardKey, out var existing) && existing.LastTsUtc >= hbUtc)
                    continue;

                cards[cardKey] = new EyeParentCard
                {
                    ParentPid = s.ParentPid > 0 ? s.ParentPid : s.Pid,
                    ParentName = s.HostType,
                    ParentTitle = s.HostTitle,
                    HostType = s.HostType,
                    LastTag = s.LastTag,
                    LastStatus = s.LastStatus,
                    LastTsUtc = hbUtc,
                    Cwd = s.Cwd ?? "",
                    SessionJsonl = s.SessionJsonl ?? "",  // authoritative JSONL from session registry
                };
            }
        }
        catch { }

        // -- Phase 2: Tick fallback (legacy -- direct CLI commands) --
        // Only adds cards for CWDs not already covered by sessions.
        var path = EyeTicksPath;
        if (File.Exists(path))
        {
            var lines = ReadTailLinesShared(path, 64 * 1024);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var t = JsonSerializer.Deserialize<EyeTick>(line);
                    if (t == null) continue;
                    if (!DateTime.TryParse(t.Ts, out var tsLocal)) continue;
                    var tsUtc = tsLocal.ToUniversalTime();
                    var ppid = t.HostPid > 0 ? t.HostPid : (t.ParentPid > 0 ? t.ParentPid : t.Pid);
                    var pname = !string.IsNullOrWhiteSpace(t.HostName) ? t.HostName : (string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName);
                    var ptitle = t.HostTitle ?? "";

                    // Skip ticks with unresolved CWD (empty, system32) -- no real session matched.
                    var cwdRaw = (t.Cwd ?? "").Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                    if (string.IsNullOrEmpty(cwdRaw)
                        || cwdRaw.EndsWith("/system32")
                        || cwdRaw.EndsWith("/windows/system32"))
                        continue;

                    var promptHwnd = t.PromptHwnd ?? "";
                    string cardKey = !string.IsNullOrEmpty(promptHwnd) ? promptHwnd : cwdRaw;

                    if (!cards.TryGetValue(cardKey, out var c))
                    {
                        // Phase 1 may have used sessionJsonl as key (different from cwdRaw/promptHwnd).
                        // Match by CWD + AI family so Claude Code ticks don't update Codex cards (or vice versa)
                        // when both AIs are running concurrently in the same workspace.
                        static string AiFamily(string? ht) =>
                            ClaudePromptHelper.IsCodexHostType(ht) ? "codex"
                            : ClaudePromptHelper.IsVsCodeHostType(ht) ||
                              string.Equals(ht, "claude-desktop", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(ht, "vscode-claudecode", StringComparison.OrdinalIgnoreCase) ? "claude"
                            : (ht ?? "").ToLowerInvariant();
                        var tickFamily = AiFamily(pname);
                        c = cards.Values.FirstOrDefault(x =>
                            !string.IsNullOrEmpty(x.Cwd) &&
                            x.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/') == cwdRaw &&
                            AiFamily(x.HostType) == tickFamily);
                        // Fall back to any CWD match if no family-matched card exists (single-AI scenario).
                        c ??= cards.Values.FirstOrDefault(x =>
                            !string.IsNullOrEmpty(x.Cwd) &&
                            x.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/') == cwdRaw &&
                            string.IsNullOrEmpty(x.HostType));
                    }

                    if (c == null)
                    {
                        cards[cardKey] = new EyeParentCard
                        {
                            ParentPid = ppid,
                            ParentName = pname,
                            ParentTitle = ptitle,
                            LastTag = t.Tag,
                            LastStatus = t.Status,
                            LastTsUtc = tsUtc,
                            Cwd = t.Cwd ?? "",
                        };
                    }
                    else if (tsUtc > c.LastTsUtc)
                    {
                        c.LastTsUtc = tsUtc;
                        c.ParentPid = ppid;
                        c.ParentName = pname;
                        c.ParentTitle = ptitle;
                        if (!string.IsNullOrWhiteSpace(t.Cwd)) c.Cwd = t.Cwd;

                        if (!IsMetaTag(t.Tag) || IsMetaTag(c.LastTag))
                        {
                            c.LastTag = t.Tag;
                            c.LastStatus = t.Status;
                        }
                    }
                }
                catch { }
            }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
    }
}
