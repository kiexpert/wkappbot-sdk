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
    /// (file reads, edits, builds, tool calls) — not just wkappbot commands.
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
    /// Returns like "publish/build" or "inspect" — shown as idle-after text.
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
    // e.g. "eye tick" checks status, "snapshot" captures state — not the real work itself
    static readonly HashSet<string> _metaTags = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "snapshot", "eye tick", "validate", "help" };

    static bool IsMetaTag(string? tag) =>
        !string.IsNullOrWhiteSpace(tag) && _metaTags.Contains(tag!);

    /// <summary>
    /// Dead-card detector + WM_NULL health check.
    /// For each card:
    ///   1. If PID/HWND is gone → [DEAD_CARD] Slack alert + zombie kill attempt.
    ///   2. If PID exists but WM_NULL > 100ms → [SLOW_CARD] Slack alert + card marked "불량".
    /// Results cached in _cardHealthCache; dead pids cached in _reportedDeadPids to suppress repeats.
    /// </summary>
    static void CheckAndReportDeadCards(List<EyeParentCard> cards)
    {
        foreach (var card in cards.ToList())
        {
            var pid = card.ParentPid;
            if (pid <= 0) continue;

            // ── Step 1: is the process/window still alive? ──
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
                    Console.WriteLine($"[DEAD_CARD] {label} — process gone");
                    _cardHealthCache[pid] = "dead";
                    // Zombie cleanup attempt (no-op if already gone)
                    try { Process.GetProcessById(pid).Kill(); } catch { }
                }
                cards.Remove(card);
                continue;
            }

            // ── Step 2: WM_NULL health check (0 = skip if no HWND found) ──
            if (hwnd == IntPtr.Zero) continue;
            if (_reportedDeadPids.Contains(pid)) continue;

            var sw = Stopwatch.StartNew();
            WKAppBot.Win32.Native.NativeMethods.SendMessageTimeoutW(
                hwnd, WKAppBot.Win32.Native.NativeMethods.WM_NULL,
                IntPtr.Zero, IntPtr.Zero,
                WKAppBot.Win32.Native.NativeMethods.SMTO_ABORTIFHUNG,
                100, out _);
            sw.Stop();

            // health% = max(0, 100 - responseMs): 1ms→99%, 99ms→1%, 100ms+→0%(불량)
            var responseMs = (int)sw.ElapsedMilliseconds;
            var healthPct = Math.Max(0, 100 - responseMs);
            var health = healthPct == 0 ? "불량" : (healthPct < 50 ? "느림" : "ok");

            var prevHealth = _cardHealthCache.GetValueOrDefault(pid, "ok");
            _cardHealthCache[pid] = health;

            if (health == "불량" && prevHealth != "불량")
            {
                var cwdTag = AbbreviateCwd(card.Cwd);
                var label = string.IsNullOrEmpty(cwdTag) ? $"{card.ParentName}:{pid}" : $"[{cwdTag}] {card.ParentName}:{pid}";
                Console.WriteLine($"[SLOW_CARD] {label} — WM_NULL={responseMs}ms (건강{healthPct}%)");
            }
            else if (health != "불량" && prevHealth == "불량")
            {
                Console.WriteLine($"[HEALTH] {card.ParentName}:{pid} recovered → {responseMs}ms (건강{healthPct}%)");
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
            var cardCwds = new HashSet<string>(cards.Select(c => c.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/')),
                StringComparer.OrdinalIgnoreCase);

            // 1s cooldown after last scan — FindAllPrompts is fast now (per-hwnd UIA cache in ClaudePromptHelper),
            // but EnumWindows + process name lookup still costs ~5-20ms per tick; limit to once/second.
            List<ClaudePromptHelper.PromptInfo> allPrompts;
            var now = DateTime.UtcNow;
            if (_cachedAllPrompts != null && (now - _lastFindAllPromptsAt).TotalMilliseconds < 10_000)
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
                // Cache appbot master prompt (WKAppBot VS Code — always-on relay target)
                CachedAppbotMasterPrompt = allPrompts.FirstOrDefault(p =>
                    p.WindowTitle.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) &&
                    p.HostType is "vscode-claudecode");
                if (sw.ElapsedMilliseconds > 50)
                    Console.WriteLine($"[EYE] FindAllPrompts(MCP): {sw.ElapsedMilliseconds}ms ({allPrompts.Count} prompts)");
            }
            foreach (var p in allPrompts)
            {
                if (p.HostType != "vscode-claudecode" && p.HostType != "Code") continue;
                var cwd = ExtractCwdFromVsCodeTitle(p.WindowTitle);
                if (string.IsNullOrEmpty(cwd)) continue;
                var cwdKey = cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                if (cardCwds.Contains(cwdKey)) continue;
                cards.Add(new EyeParentCard
                {
                    ParentPid = p.WindowHandle.ToInt32(),
                    ParentName = "Code",
                    ParentTitle = p.WindowTitle,
                    LastTag = "prompt-discovered",
                    LastStatus = "idle",
                    LastTsUtc = DateTime.UtcNow.AddMinutes(-1),
                    Cwd = cwd,
                });
                cardCwds.Add(cwdKey);
            }
        }
        catch { }
    }

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 86400)
    {
        // KEY = normalized CWD (folder-based grouping: same folder = one card, survives PID restart)
        var cards = new Dictionary<string, EyeParentCard>(StringComparer.OrdinalIgnoreCase);
        var path = EyeTicksPath;
        if (!File.Exists(path)) return cards.Values.ToList();

        // 2MB tail: enough for ~24h of ticks (each tick ~200 bytes, tick every ~10s = ~1.7MB/day)
        var lines = ReadTailLinesShared(path, 64 * 1024);
        var now = DateTime.UtcNow;

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

                // CWD-based key: normalize to lowercase forward-slash, trimmed
                var cwdRaw = (t.Cwd ?? "").Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                var cardKey = string.IsNullOrEmpty(cwdRaw) ? $"pid_{ppid}" : cwdRaw;

                // Always update timestamp with latest tick
                // But for tag/status: meta tags (eye, snapshot) don't overwrite meaningful work tags
                if (!cards.TryGetValue(cardKey, out var c))
                {
                    // First tick for this CWD — always accept
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
                    // Newer tick — always update timestamp and process info
                    c.LastTsUtc = tsUtc;
                    c.ParentPid = ppid; // latest PID for this CWD
                    c.ParentName = pname;
                    c.ParentTitle = ptitle;
                    if (!string.IsNullOrWhiteSpace(t.Cwd)) c.Cwd = t.Cwd;

                    // Only update tag/status if:
                    // 1. New tick is non-meta (real work always wins), OR
                    // 2. Existing tag is also meta (meta→meta is fine)
                    if (!IsMetaTag(t.Tag) || IsMetaTag(c.LastTag))
                    {
                        c.LastTag = t.Tag;
                        c.LastStatus = t.Status;
                    }
                }
            }
            catch { }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
    }
}
