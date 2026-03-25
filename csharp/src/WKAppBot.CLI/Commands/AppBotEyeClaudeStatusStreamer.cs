// AppBotEyeClaudeStatusStreamer.cs — Per-instance Claude Desktop status streaming.
// Tracks multiple Claude Desktop windows independently, streams each to Slack.
// Extracted from AppBotEyeGlobalMode.cs for readability.

using System.Diagnostics;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Emoji prefixes used for Claude/Eye STATUS messages in Slack.
    /// Only these are eligible for auto-cleanup on Eye restart.
    /// NOTE: :memo: is NOT a status emoji — it's used by suggest (건의) messages.
    /// </summary>
    static readonly HashSet<string> StatusEmojis = new(StringComparer.Ordinal)
    {
        ":zzz:",        // idle
        ":runner:",     // executing
        ":gear:",       // executing (eye tick)
        ":clipboard:",  // plan approval pending
        ":warning:",    // rate limit / context alert
        ":large_green_circle:", // Eye alive
        ":red_circle:", // Eye stopped
        ":robot_face:", // generic bot status
    };

    static bool IsStatusEmoji(string text)
    {
        foreach (var emoji in StatusEmojis)
            if (text.StartsWith(emoji)) return true;
        return false;
    }

    // ── Per-instance state ──────────────────────────────────────────────────

    /// <summary>Per-Claude-Desktop-instance state: spinner detection + Slack status streaming.</summary>
    sealed class ClaudeInstanceState
    {
        // ── Spinner detection (per-hwnd, filled by DetectSpinnerIdle) ──
        public uint LastSpinnerHash;
        public int ConsecutiveNoChange;
        public IntPtr CachedRenderHwnd;

        // ── Slack status streaming ──
        public string? SlackStatusTs;
        public string? LastSlackStatusText;
        public string? LastExecutingText;
        public string? IdleAfterText;
        public bool IdleMessageSent;

        // ── Rate limit ──
        public bool WasRateLimited;
        public DateTime? RateLimitDetectedAt;
        public DateTime? RateLimitResetTime;
        public DateTime? LastRateLimitAlertTime;

        // ── Plan/permission approval ──
        public bool PlanApprovalSentToSlack;
        public string? PendingPlanApprovalSlackTs;
        public bool PermissionPromptSentToSlack;
        public string? PendingPermissionSlackTs;
        public DateTime? PermissionPromptFirstSeen;

        // ── Display label (CWD short name for Slack messages) ──
        public string CwdLabel = "";

        // ── Last status type (executing/idle/etc.) for edit-vs-delete decision ──
        public string? LastStatusType;

        // ── Homework injection: idle 1min → pending suggestions → newchat ──
        public DateTime? IdleStartedAt;
        public bool HomeworkNotified;
        public DateTime? LastHomeworkAt; // global cooldown — prevents re-fire across session resets
        public string? FullCwd;          // full workspace path (for JSONL new-session detection)

        // ── Streaming dedup: minimum 1s between Slack chat.update calls ──
        public DateTime LastSlackStreamAt;

        // ── File-size watermark: only stream edit when JSONL has grown since last post ──
        // On first tick with known CWD, watermark is calibrated to current size (skip re-streaming old state).
        public long LastPostedJsonlSize;
        public bool JsonlSizeInitialized;
    }

    // Per-hwnd instance state dictionary. Entries added on first encounter, purged when window gone.
    static readonly Dictionary<IntPtr, ClaudeInstanceState> _instanceStates = new();

    // Stale status messages collected at Eye startup — deleted after first successful new post.
    // Avoids the gap: "old deleted → 1s pause → new posted".
    static readonly List<string> _staleStatusTsOnStartup = new();

    // Cached hwnd of appbot's own VS Code window (the WKAppBot project window).
    // Updated by status streaming loop. Used as routing fallback when title-based CWD match fails.
    internal static IntPtr _cachedAppbotOwnHwnd = IntPtr.Zero;

    static ClaudeInstanceState GetOrCreateInstanceState(IntPtr hwnd)
    {
        if (!_instanceStates.TryGetValue(hwnd, out var state))
        {
            state = new ClaudeInstanceState();
            // Try to find a CWD label from cached cards matching this hwnd's PID
            state.CwdLabel = ResolveInstanceCwdLabel(hwnd);
            // Restore Slack TS + last text + status type from window props (persisted across Eye restart)
            state.SlackStatusTs = GetWindowStringProp(hwnd, PropSlackTs);
            state.LastSlackStatusText = GetWindowStringProp(hwnd, PropAiOut);
            state.LastStatusType = GetWindowStringProp(hwnd, PropStatusType);

            // Fallback: if window prop didn't have ts, try Slack history recovery by username
            if (string.IsNullOrEmpty(state.SlackStatusTs) && !string.IsNullOrEmpty(state.CwdLabel))
            {
                // Build expected username pattern: "클롣[{cwdLabel}]" or "크로[{cwdLabel}]"
                foreach (var prefix in new[] { SlackClaudePrefix, "크로" })
                {
                    var expectedUser = $"{prefix}[{state.CwdLabel}]";
                    if (_recoveredStatusByUsername.TryGetValue(expectedUser, out var recovered))
                    {
                        state.SlackStatusTs = recovered.ts;
                        state.LastSlackStatusText = recovered.text;
                        Console.WriteLine($"[EYE] Restored status ts from Slack for '{expectedUser}': {recovered.ts}");
                        _recoveredStatusByUsername.Remove(expectedUser); // consume — one-time recovery
                        break;
                    }
                }
            }

            _instanceStates[hwnd] = state;
        }
        else if (string.IsNullOrEmpty(state.CwdLabel))
        {
            state.CwdLabel = ResolveInstanceCwdLabel(hwnd);
        }
        return state;
    }

    /// <summary>
    /// Resolve a short display label for a Claude Desktop hwnd.
    /// Priority: _cachedCards PID match → window title short form → hwnd hex.
    /// </summary>
    static string ResolveInstanceCwdLabel(IntPtr hwnd)
    {
        // Priority 1: VS Code — use window title (each window has unique title with folder name)
        // Multiple VS Code windows share the same PID, so title-based CWD is the only reliable source.
        try
        {
            var pi = FindAllPromptsViaMcp().FirstOrDefault(p => p.WindowHandle == hwnd);
            if (pi?.HostType == "vscode-claudecode" && !string.IsNullOrEmpty(pi.WindowTitle))
            {
                var titleCwd = ExtractCwdFromVsCodeTitle(pi.WindowTitle);
                if (!string.IsNullOrEmpty(titleCwd) && !IsSystemOrInstallDirectory(titleCwd))
                    return AbbreviateCwd(titleCwd);
            }
        }
        catch { }

        // Priority 2: _cachedCards PID match (claude-desktop / codex — single window per process)
        try
        {
            GetWindowThreadProcessId(hwnd, out var pid);
            if (pid > 0)
            {
                var card = _cachedCards.FirstOrDefault(c => c.ParentPid == (int)pid);
                if (card != null && !string.IsNullOrEmpty(card.Cwd))
                    return AbbreviateCwd(card.Cwd);
            }
        }
        catch { }

        // Fallback: short form of Claude Desktop window title
        try
        {
            var pi = FindAllPromptsViaMcp().FirstOrDefault(p => p.WindowHandle == hwnd);
            if (pi != null && !string.IsNullOrEmpty(pi.WindowTitle))
            {
                var t = pi.WindowTitle.Replace("— Claude", "").Trim();
                if (t.Length > 15) t = t[..15] + "…";
                if (!string.IsNullOrEmpty(t)) return t;
            }
        }
        catch { }

        return "";  // no label — claude-desktop is single global instance
    }

    // ── Main per-tick entry point ───────────────────────────────────────────

    /// <summary>
    /// Detect and stream status for ALL Claude Desktop instances.
    /// Called every ~5s (frame % 50). Returns combined status text or null if idle.
    /// Mutates <paramref name="claudeHwnd"/> (primary window re-find on loss).
    /// </summary>
    static string? RunClaudeStatusTick(
        ref IntPtr claudeHwnd,
        string? slackBotToken, string? slackChannel, string botUsername,
        SlackSocketClient? slackClient,
        string statusTsFile,
        Dictionary<string, (int mb, string? path)> contextWarnedPcts)
    {
        const int RateLimitCooldownMinutes = 30;
        const int RateLimitAlertCooldownMinutes = 30;

        // ── Purge dead hwnds (clean up window prop atoms before removing) ──
        var deadHwnds = _instanceStates.Keys.Where(h => !IsWindow(h)).ToList();
        foreach (var h in deadHwnds)
        {
            // Clear atoms stored in the props (prevents global atom table leak across restarts)
            SetWindowStringProp(h, PropSlackTs, null);
            SetWindowStringProp(h, PropAiOut, null);
            _instanceStates.Remove(h);
        }

        // ── Re-find primary Claude window if lost ──
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            claudeHwnd = FindClaudeDesktopWindow();

        // ── Collect all AI instances (Desktop + VS Code + Codex) from prompt cache ──
        var allClaudePrompts = FindAllPromptsViaMcp()
            .Where(p => (p.HostType == "claude-desktop" || p.HostType == "vscode-claudecode" || p.HostType == "codex-desktop") && IsWindow(p.WindowHandle))
            .GroupBy(p => p.WindowHandle).Select(g => g.First()).ToList();
        var claudeInstances = allClaudePrompts.Select(p => p.WindowHandle).ToList();
        var instanceHostTypes = allClaudePrompts.ToDictionary(p => p.WindowHandle, p => p.HostType);

        // Include primary window if not already in list (claude-desktop may not be in prompt cache yet)
        if (claudeHwnd != IntPtr.Zero && !claudeInstances.Contains(claudeHwnd))
            claudeInstances.Add(claudeHwnd);

        // ── Per-card context usage monitor (runs once per tick, not per instance) ──
        try
        {
            foreach (var card in _cachedCards)
            {
                if (string.IsNullOrWhiteSpace(card.Cwd)) continue;
                var (pct, _, jsonlPath, fileSize) = GetContextInfoForCwdEx(card.Cwd);
                var sizeMB = fileSize / (1024.0 * 1024.0);
                const double ContextLimitMB = 40.0;
                const double WarnStartMB = 20.0;   // 경고 시작 (토큰 절약 여유)
                const double UrgentMB = 30.0;       // 긴급 (75% 이상)
                if (sizeMB < WarnStartMB || jsonlPath == null) continue;

                var cwdKey = card.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                contextWarnedPcts.TryGetValue(cwdKey, out var prevWarned);
                var curMB = (int)sizeMB; // 1MB 단위 dedup

                // New session detected: jsonlPath changed (ctime-new file selected by mtime) → reset counter
                if (prevWarned.path != null && prevWarned.path != jsonlPath)
                {
                    Console.WriteLine($"[EYE] [{AbbreviateCwd(card.Cwd)}] New session JSONL — context warn counter reset (was {prevWarned.mb}MB)");
                    prevWarned = (0, null);
                }

                if (curMB <= prevWarned.mb) continue;

                var cwdTag = AbbreviateCwd(card.Cwd);
                contextWarnedPcts[cwdKey] = (curMB, jsonlPath);

                if (sizeMB >= UrgentMB && !string.IsNullOrEmpty(slackBotToken))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[EYE] 🚨 [{cwdTag}] Context {pct}%! ({sizeMB:F1}MB/{ContextLimitMB}MB) — 즉시 인수인계하세요!");
                    Console.ResetColor();

                    // 프창에도 긴급 경고 전송 (MCP subprocess에서 실행)
                    try
                    {
                        var urgentNudge = $"🚨 컨텍스트 {pct}%! ({sizeMB:F1}/{ContextLimitMB}MB) — 즉시 인수인계하세요!\\nwkappbot newchat 실행 필요";
                        EyeMcpClient.CallFireAndForget(["prompt", "send", cwdTag, urgentNudge]);
                        Console.WriteLine($"[EYE] 🚨 [{cwdTag}] Urgent nudge sent via MCP");
                    }
                    catch { /* best-effort */ }

                    Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                        $":rotating_light: *[{cwdTag}] 컨텍스트 {pct}%!* ({sizeMB:F1}/{ContextLimitMB}MB)\n클롣이 아직 인수인계 안 했습니다! `wkappbot newchat` 실행 필요",
                        username: BuildSlackBotUsername(SlackClaudePrefix, cwdTag))).Wait(3000);
                }
                else if (!string.IsNullOrEmpty(slackBotToken))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] ⚠️ [{cwdTag}] Context {sizeMB:F1}MB/{ContextLimitMB}MB — 인수인계 준비하세요");
                    Console.ResetColor();

                    var handoff = BuildHandoffPrompt(jsonlPath, _cachedCards, sizeMB, ContextLimitMB);
                    try
                    {
                        var nudge = $"⚠️ 컨텍스트 {sizeMB:F1}/{ContextLimitMB}MB 도달!\\n준비되면 아래 명령을 실행:\\n\\nwkappbot newchat \"{handoff.Replace("\"", "\\\"")}\"";
                        EyeMcpClient.CallFireAndForget(["prompt", "send", cwdTag, nudge]);
                        Console.WriteLine($"[EYE] ✅ [{cwdTag}] Handoff nudge sent via MCP");
                        Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                            $":warning: *[{cwdTag}] 컨텍스트 {sizeMB:F1}/{ContextLimitMB}MB!*\n클롣에게 인수인계 프롬프트를 전달했습니다.",
                            username: BuildSlackBotUsername(SlackClaudePrefix, cwdTag))).Wait(3000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[EYE] ⚠️ [{cwdTag}] Nudge delivery failed: {ex.Message}");
                        Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                            $":warning: *[{cwdTag}] 컨텍스트 {sizeMB:F1}/{ContextLimitMB}MB!*\n인수인계 전달 실패! 직접 확인해주세요.",
                            username: SlackBroadcastUsername)).Wait(3000);
                    }
                }
            }
        }
        catch { /* best-effort */ }

        // ── Per-instance status detection + streaming ──
        string? combinedStatusText = null;

        if (!claudeInstances.Any())
        {
            // No Claude windows — update any stale Slack status messages to "idle"
            foreach (var (_, stale) in _instanceStates)
            {
                if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel)
                    && stale.SlackStatusTs != null
                    && stale.LastSlackStatusText != null
                    && !stale.LastSlackStatusText.Contains("프롬프트 대기")
                    && !stale.LastSlackStatusText.Contains("창 없음"))
                {
                    try
                    {
                        var idleText = ":zzz: Claude 창 없음 — 프롬프트 대기";
                        Task.Run(async () => await SlackUpdateMessageAsync(
                            slackBotToken!, slackChannel!, stale.SlackStatusTs, idleText))
                            .Wait(3000);
                        stale.LastSlackStatusText = idleText;
                        Console.WriteLine("[EYE] Claude window lost → Slack idle");
                    }
                    catch { }
                }
            }
            return null;
        }

        var primaryHwnd = claudeHwnd; // local copy — ref params cannot be captured in lambdas

        foreach (var hwnd in claudeInstances)
        {
            var state = GetOrCreateInstanceState(hwnd);
            instanceHostTypes.TryGetValue(hwnd, out var hostType);
            // UIA-idle mode: no pixel spinner — use turn-form prompt_ready as idle signal (VS Code + Codex)
            bool isVsCode = hostType == "vscode-claudecode" || hostType == "codex-desktop";
            var label = string.IsNullOrEmpty(state.CwdLabel) ? "" : $"[{state.CwdLabel}] ";

            // Cache appbot's own VS Code window hwnd for routing fallback (see ResolveWorkspaceScopedPrompts)
            var appbotFolder = Path.GetFileName(Environment.CurrentDirectory); // "WKAppBot"
            if (!string.IsNullOrEmpty(appbotFolder) && !string.IsNullOrEmpty(state.CwdLabel) &&
                state.CwdLabel.Contains(appbotFolder, StringComparison.OrdinalIgnoreCase))
                _cachedAppbotOwnHwnd = hwnd;
            // Slack status text omits label — CWD already shown in bot username (e.g. "클롣[WG-WKAppBot]")
            const string slackLabel = "";

            Tuple<string, string>? claudeStatus = null;
            try
            {
                claudeStatus = DetectClaudeDesktopStatusViaRoute(hwnd);

                // VS Code / Codex: no turn-form in UIA → DetectClaudeDesktopStatus always null.
                // Use JSONL card-based detection instead.
                if (claudeStatus == null && isVsCode)
                {
                    GetWindowThreadProcessId(hwnd, out var vsPid);
                    string? vsCwd = null;
                    if (hostType == "vscode-claudecode")
                    {
                        var pi = FindAllPromptsViaMcp().FirstOrDefault(p => p.WindowHandle == hwnd);
                        if (pi != null) vsCwd = ExtractCwdFromVsCodeTitle(pi.WindowTitle);
                    }
                    if (string.IsNullOrEmpty(vsCwd))
                    {
                        var vsCard = _cachedCards.FirstOrDefault(c => c.ParentPid == (int)vsPid);
                        vsCwd = vsCard?.Cwd;
                    }
                    if (!string.IsNullOrEmpty(vsCwd))
                    {
                        state.FullCwd ??= vsCwd; // store once for homework JSONL detection
                        var vsMatchCard = _cachedCards.FirstOrDefault(c =>
                            string.Equals(c.Cwd, vsCwd, StringComparison.OrdinalIgnoreCase));
                        var ageSec = vsMatchCard != null
                            ? (DateTime.UtcNow - vsMatchCard.LastTsUtc).TotalSeconds
                            : double.MaxValue;
                        var lastText = ReadClotThoughtForCwd(vsCwd);
                        var lastLine = GetLastOutputLine(lastText);
                        if (ageSec < 30 && !string.IsNullOrEmpty(lastLine))
                        {
                            // Recent JSONL activity → executing
                            claudeStatus = Tuple.Create("executing", lastLine);
                        }
                        else if (!string.IsNullOrEmpty(lastLine))
                        {
                            // Stale → idle (prompt_ready equivalent)
                            claudeStatus = Tuple.Create("prompt_ready", lastLine);
                        }
                    }
                }

                if (claudeStatus != null)
                {
                    var statusText = claudeStatus.Item2;

                    // Skip prompt edit node placeholder: pure ASCII short text from the input field
                    // (e.g. "ctrl esc to focus or unfocus Claude", "Type a message")
                    // Real AI output always contains non-ASCII chars or is longer than placeholder
                    bool isEditPlaceholder = statusText != null
                        && claudeStatus.Item1 == "prompt_ready" // only from prompt edit node
                        && statusText.Length < 80
                        && statusText.Length > 0
                        && !statusText.Any(c => c > 0x7F); // pure ASCII = placeholder

                    if (claudeStatus.Item1 == "executing" && statusText != null)
                        state.LastExecutingText = statusText;

                    combinedStatusText = isEditPlaceholder ? null : $"{label}Claude: {statusText}";

                    // ── Rate limit detection ──
                    bool justHitRateLimit = false;
                    if (claudeStatus.Item1 == "rate_limit")
                    {
                        justHitRateLimit = !state.WasRateLimited;
                        state.WasRateLimited = true;
                        if (state.RateLimitDetectedAt == null)
                            state.RateLimitDetectedAt = DateTime.Now;
                        var resetDt = GetResetTimeFromDisplayText(claudeStatus.Item2);
                        if (resetDt != null) state.RateLimitResetTime = resetDt;
                    }
                    else if (state.WasRateLimited)
                    {
                        // Any non-rate_limit state = limit cleared
                        var now = DateTime.Now;
                        bool cooldownPassed = state.RateLimitDetectedAt != null &&
                            (now - state.RateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                        bool resetTimePassed = state.RateLimitResetTime != null && now >= state.RateLimitResetTime.Value;

                        state.WasRateLimited = false;
                        state.RateLimitDetectedAt = null;
                        state.RateLimitResetTime = null;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[EYE] {label}Rate limit cleared (newState={claudeStatus.Item1}, cooldown={cooldownPassed}, resetTime={resetTimePassed})");
                        Console.ResetColor();

                        try
                        {
                            Console.WriteLine($"[SCHEDULE] {label}Rate limit cleared! Checking on_limit_reset schedules...");
                            Thread.Sleep(3000);
                            var resetItems = ScheduleManager.GetOnLimitResetItems();
                            foreach (var resetItem in resetItems)
                            {
                                ExecuteScheduleItem(resetItem, slackBotToken, slackChannel);
                                Thread.Sleep(2000);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[SCHEDULE] on_limit_reset error: {ex.Message}");
                        }
                    }

                    // ── Rate limit alert to Slack ──
                    bool alertCooldownOk = state.LastRateLimitAlertTime == null ||
                        (DateTime.Now - state.LastRateLimitAlertTime.Value).TotalMinutes >= RateLimitAlertCooldownMinutes;
                    if (justHitRateLimit && alertCooldownOk && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                    {
                        state.LastRateLimitAlertTime = DateTime.Now;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[EYE] ★ {label}Rate limit detected! {claudeStatus.Item2}");
                        Console.ResetColor();
                        try
                        {
                            var alertMsg = $":rotating_light: *{label}Rate Limit!* {claudeStatus.Item2}";
                            var rateLimitUsername = BuildSlackBotUsername(SlackClaudePrefix, state.CwdLabel);
                            Task.Run(async () =>
                                await SlackSendViaApi(slackBotToken!, slackChannel!, alertMsg, username: rateLimitUsername))
                                .Wait(5000);
                        }
                        catch { }
                    }

                    // ── Slack status streaming ──
                    if (slackClient != null && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                    {
                        try
                        {
                            if (claudeStatus.Item1 == "permission_prompt")
                                goto skipStatusStreaming;

                            string slackText = "";
                            bool wasIdle = false;
                            if (claudeStatus.Item1 == "prompt_ready")
                            {
                                // VS Code: UIA-confirmed idle → post :zzz: message directly (no spinner)
                                if (isVsCode && !state.IdleMessageSent
                                    && state.LastSlackStatusText?.Contains(":zzz:") != true)
                                {
                                    // Extract last status text as idle suffix (fallback to LastSlackStatusText)
                                    string? idleHint = state.LastExecutingText;
                                    if (idleHint == null && state.LastSlackStatusText != null)
                                    {
                                        var idx = state.LastSlackStatusText.IndexOf("Claude: ", StringComparison.Ordinal);
                                        if (idx >= 0) idleHint = state.LastSlackStatusText[(idx + 8)..];
                                    }
                                    // Strip rate_limit artifacts from idle hint
                                    if (idleHint != null && (idleHint.Contains("한도") || idleHint.Contains("rate") || idleHint.Contains("리셋") || idleHint.Contains("Rate")))
                                        idleHint = null;
                                    var idleSuffix = !string.IsNullOrEmpty(idleHint) ? $" after: {idleHint}" : "";
                                    var idleMsg = $":zzz: {slackLabel}Idle{idleSuffix}";
                                    var instUser2 = BuildSlackBotUsername(SlackClaudePrefix, state.CwdLabel);
                                    Task.Run(async () => await PostOrUpdateAiStatusAsync(
                                        hwnd, state, idleMsg, "idle",
                                        slackBotToken!, slackChannel!, instUser2, statusTsFile, primaryHwnd))
                                        .Wait(5000);
                                    state.IdleMessageSent = true;
                                    state.IdleStartedAt = DateTime.UtcNow;
                                    state.HomeworkNotified = false;
                                    state.LastExecutingText = null;
                                    Console.WriteLine($"[EYE] {label}VS Code → idle: {idleMsg}");
                                }
                                goto skipStatusStreaming; // Skip — spinner handles idle for claude-desktop
                            }
                            else
                            {
                                // Active: only reset idle if spinner is animating again (VS Code: UIA-confirmed)
                                wasIdle = state.IdleMessageSent;
                                if (state.IdleMessageSent)
                                {
                                    if (!isVsCode && DetectSpinnerIdle(hwnd, state)) goto skipStatusStreaming;
                                    // Spinner animating again (or VS Code executing) → activity resumed
                                    if (!isVsCode) ResetSpinnerDetection(state);
                                    state.IdleAfterText = null;
                                    Console.WriteLine($"[EYE] {label}Idle badge cleared — activity detected");
                                }
                                state.IdleMessageSent = false;
                                state.IdleStartedAt = null;
                                var statusEmoji = claudeStatus.Item1 switch
                                {
                                    "executing" => ":runner:",
                                    "plan_approval_pending" => ":clipboard:",
                                    "plan_mode" => ":memo:",
                                    "rate_limit" => ":warning:",
                                    _ => ":robot_face:"
                                };
                                slackText = $"{statusEmoji} {slackLabel}Claude: {claudeStatus.Item2}";
                            }

                            // File-size watermark: only stream if JSONL has grown since last successful post/edit.
                            // Exception: idle→active transition bypasses watermark to remove idle badge immediately.
                            var (_, _, _, curJsonlSize) = GetContextInfoForCwdEx(state.FullCwd);
                            if (wasIdle)
                            {
                                state.LastPostedJsonlSize = curJsonlSize; // advance watermark (avoid double-post next tick)
                            }
                            else
                            {
                                if (!state.JsonlSizeInitialized && curJsonlSize > 0)
                                {
                                    state.LastPostedJsonlSize = curJsonlSize;
                                    state.JsonlSizeInitialized = true;
                                    goto skipStatusStreaming;
                                }
                                if (curJsonlSize > 0 && curJsonlSize <= state.LastPostedJsonlSize) goto skipStatusStreaming;
                            }

                            // Streaming dedup: skip if last Slack update was < 1s ago (prevents API spam on rapid JSONL changes)
                            if ((DateTime.UtcNow - state.LastSlackStreamAt).TotalSeconds < 1.0) goto skipStatusStreaming;
                            state.LastSlackStreamAt = DateTime.UtcNow;

                            {
                                var instanceUsername = BuildSlackBotUsername(SlackClaudePrefix, state.CwdLabel);
                                var capturedJsonlSize = curJsonlSize;
                                // Fire-and-forget — do NOT Wait() here; FSW can trigger status updates rapidly
                                // and blocking the Eye loop for 5s per update causes visible streaming lag.
                                _ = Task.Run(async () =>
                                {
                                    try { await PostOrUpdateAiStatusAsync(hwnd, state, slackText, claudeStatus.Item1, slackBotToken!, slackChannel!, instanceUsername, statusTsFile, primaryHwnd, capturedJsonlSize); }
                                    catch (Exception ex) { Console.WriteLine($"[EYE] PostOrUpdateAiStatus error: {ex.Message}"); }
                                });
                            }
                        }
                        catch { /* best-effort */ }

                        skipStatusStreaming:
                        // ── Spinner pixel idle / active transition (Claude Desktop only) ──
                        if (!isVsCode && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                        {
                            try
                            {
                                bool spinnerIdle = DetectSpinnerIdle(hwnd, state);
                                if (!state.IdleMessageSent && spinnerIdle)
                                {
                                    // Spinner settled → Claude is idle → post idle message
                                    if (state.IdleAfterText == null)
                                    {
                                        if (state.LastExecutingText != null)
                                            state.IdleAfterText = state.LastExecutingText;
                                        state.LastExecutingText = null;
                                    }
                                    // Fallback: extract text from last Slack status if no executing hint available
                                    if (state.IdleAfterText == null && state.LastSlackStatusText != null)
                                    {
                                        var cidx = state.LastSlackStatusText.IndexOf("Claude: ", StringComparison.Ordinal);
                                        if (cidx >= 0) state.IdleAfterText = state.LastSlackStatusText[(cidx + 8)..];
                                    }
                                    // Strip rate_limit artifacts
                                    if (state.IdleAfterText != null && (state.IdleAfterText.Contains("한도") || state.IdleAfterText.Contains("rate") || state.IdleAfterText.Contains("리셋") || state.IdleAfterText.Contains("Rate")))
                                        state.IdleAfterText = null;
                                    var idleSuffix = !string.IsNullOrEmpty(state.IdleAfterText) ? $" after: {state.IdleAfterText}" : "";
                                    var idleMsg = $":zzz: {slackLabel}Idle{idleSuffix}";

                                    var instanceUsername2 = BuildSlackBotUsername(SlackClaudePrefix, state.CwdLabel);
                                    Task.Run(async () => await PostOrUpdateAiStatusAsync(
                                        hwnd, state, idleMsg, "idle",
                                        slackBotToken!, slackChannel!, instanceUsername2, statusTsFile, primaryHwnd))
                                        .Wait(5000);
                                    state.IdleMessageSent = true;
                                    state.IdleStartedAt = DateTime.UtcNow;
                                    state.HomeworkNotified = false;
                                    Console.WriteLine($"[EYE] {label}Spinner idle: {idleMsg}");
                                }
                                else if (state.IdleMessageSent && !spinnerIdle)
                                {
                                    // Spinner active again → Claude resumed → clear idle so next tick posts executing
                                    state.IdleMessageSent = false;
                                    state.IdleStartedAt = null;
                                    state.LastSlackStatusText = null;
                                    ResetSpinnerDetection(state);
                                    Console.WriteLine($"[EYE] {label}Activity resumed — idle cleared");
                                }
                            }
                            catch { }
                        }

                        // ── Homework injection: idle 1min + pending suggestions → newchat (WKAppBot only) ──
                        // LastHomeworkAt persisted to disk — survives Eye restarts (no re-fire on restart).
                        // 4h cooldown prevents loop: homework opens newchat → new session idle → re-fire.
                        if (state.LastHomeworkAt == null && !string.IsNullOrEmpty(state.FullCwd ?? state.CwdLabel))
                        {
                            var ck = state.FullCwd ?? state.CwdLabel;
                            state.LastHomeworkAt = LoadHomeworkAt(ck);
                        }
                        if (state.IdleMessageSent && !state.HomeworkNotified
                            && state.IdleStartedAt != null
                            && (DateTime.UtcNow - state.IdleStartedAt.Value).TotalMinutes >= 1
                            && (state.LastHomeworkAt == null || (DateTime.UtcNow - state.LastHomeworkAt.Value).TotalHours >= 1))
                        {
                            try { CheckAndSendHomework(state, hwnd, label); }
                            catch { }
                        }

                        // ── Plan approval → Slack ──
                        if (claudeStatus.Item1 == "plan_approval_pending" && !state.PlanApprovalSentToSlack)
                        {
                            try
                            {
                                var plan = ExtractPlanContentViaRoute(hwnd);
                                if (plan != null)
                                {
                                    var planContent = plan.Value.content;
                                    if (planContent.Length > 3500)
                                        planContent = planContent[..3500] + "\n\n... (truncated)";
                                    var sourceLabel = plan.Value.source == "UIA" ? "UIA" : $"`{plan.Value.source}`";
                                    var fallbackText = $":clipboard: {label}플랜 승인 대기 (via {sourceLabel})\n\n{planContent}";
                                    var blocks = BuildPlanApprovalBlocks(planContent, sourceLabel);
                                    var (sendOk, sendTs) = Task.Run(async () =>
                                        await SlackSendBlocksViaApi(slackBotToken!, slackChannel!, fallbackText, blocks))
                                        .GetAwaiter().GetResult();
                                    if (sendOk)
                                    {
                                        state.PendingPlanApprovalSlackTs = sendTs;
                                        state.PlanApprovalSentToSlack = true;
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"[EYE] {label}Plan sent to Slack via {plan.Value.source} (ts={sendTs})");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    state.PlanApprovalSentToSlack = true; // don't retry
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[EYE] {label}Plan Slack share error: {ex.Message}");
                            }
                        }
                        if (claudeStatus.Item1 != "plan_approval_pending" && state.PlanApprovalSentToSlack)
                        {
                            state.PlanApprovalSentToSlack = false;
                            state.PendingPlanApprovalSlackTs = null;
                        }

                        // ── Permission prompt → Slack (3s debounce) ──
                        if (claudeStatus.Item1 == "permission_prompt" && !state.PermissionPromptSentToSlack)
                        {
                            if (state.PermissionPromptFirstSeen == null)
                                state.PermissionPromptFirstSeen = DateTime.Now;

                            if ((DateTime.Now - state.PermissionPromptFirstSeen.Value).TotalSeconds >= 3.0)
                            {
                                try
                                {
                                    var permButtons = GetPermissionButtons(hwnd);
                                    if (permButtons.Count >= 2)
                                    {
                                        var btnList = string.Join(" / ", permButtons);
                                        var fallbackText = $":lock: {label}수락 요구: [{btnList}]";
                                        var blocks = BuildPermissionBlocks(permButtons, claudeStatus.Item2);
                                        var (sendOk, sendTs) = Task.Run(async () =>
                                            await SlackSendBlocksViaApi(slackBotToken!, slackChannel!, fallbackText, blocks))
                                            .GetAwaiter().GetResult();
                                        if (sendOk)
                                        {
                                            state.PendingPermissionSlackTs = sendTs;
                                            state.PermissionPromptSentToSlack = true;
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            Console.WriteLine($"[EYE] {label}Permission buttons sent to Slack: [{btnList}] (ts={sendTs})");
                                            Console.ResetColor();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[EYE] {label}Permission Slack share error: {ex.Message}");
                                }
                            }
                        }
                        if (claudeStatus.Item1 != "permission_prompt")
                        {
                            state.PermissionPromptFirstSeen = null;
                            if (state.PermissionPromptSentToSlack)
                            {
                                state.PermissionPromptSentToSlack = false;
                                state.PendingPermissionSlackTs = null;
                            }
                        }
                    }
                }
                else
                {
                    // claudeStatus null → idle
                    if (state.WasRateLimited)
                    {
                        state.WasRateLimited = false;
                        state.RateLimitDetectedAt = null;
                        state.RateLimitResetTime = null;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[EYE] {label}Rate limit cleared (idle status detected)");
                        Console.ResetColor();

                        try
                        {
                            var resetItems = ScheduleManager.GetOnLimitResetItems();
                            foreach (var resetItem in resetItems)
                            {
                                ExecuteScheduleItem(resetItem, slackBotToken, slackChannel);
                                Thread.Sleep(2000);
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { /* best-effort per-instance */ }

            // ── Fallback: when claudeStatus==null (UIA failed), still track spinner state (Claude Desktop only) ──
            if (!isVsCode && claudeStatus == null && state.IdleMessageSent && state.SlackStatusTs != null
                && slackClient != null && !string.IsNullOrEmpty(slackBotToken))
            {
                try
                {
                    if (!DetectSpinnerIdle(hwnd, state))
                    {
                        state.IdleMessageSent = false;
                        state.LastSlackStatusText = null;
                        ResetSpinnerDetection(state);
                        Console.WriteLine($"[EYE] {label}UIA null but spinner active — idle cleared");
                    }
                }
                catch { }
            }
        } // end foreach claudeInstances

        return combinedStatusText;
    }

    /// <summary>Get the CwdLabel for a specific Claude instance hwnd (e.g. "WG-WKAppBot").</summary>
    internal static string? GetCwdLabel(IntPtr hwnd)
        => _instanceStates.TryGetValue(hwnd, out var s) ? s.CwdLabel : null;

    /// <summary>Get the Slack status ts for any instance (for ack detection).</summary>
    static string? GetAnyInstanceSlackStatusTs()
        => _instanceStates.Values.FirstOrDefault(s => s.SlackStatusTs != null)?.SlackStatusTs;

    /// <summary>Get the pending plan approval ts for any instance.</summary>
    static string? GetAnyPlanApprovalTs()
        => _instanceStates.Values.FirstOrDefault(s => s.PendingPlanApprovalSlackTs != null)?.PendingPlanApprovalSlackTs;

    /// <summary>Get the pending permission ts for any instance.</summary>
    static string? GetAnyPermissionTs()
        => _instanceStates.Values.FirstOrDefault(s => s.PendingPermissionSlackTs != null)?.PendingPermissionSlackTs;

    /// <summary>Reset all instances' Slack status (ack delete callback).</summary>
    static void ResetAllInstancesSlackStatus(string statusTsFile)
    {
        foreach (var s in _instanceStates.Values)
        {
            s.SlackStatusTs = null;
            s.LastSlackStatusText = null;
        }
        try { File.WriteAllText(statusTsFile, ""); } catch { }
    }

    // ── Window prop string helpers (GlobalAtom-based, survives Eye restart) ──────────
    // Prop names written to the AI app window (VS Code / Codex / Claude Desktop)
    private const string PropAiOut      = "WKAppBot.AiOut";    // last AI output line
    private const string PropSlackTs    = "WKAppBot.SlTs";     // last Slack status message TS
    private const string PropStatusType = "WKAppBot.StType";   // last status type (executing/idle/etc.)

    /// <summary>Store a short string (≤240 chars) in a window property via GlobalAtom.</summary>
    static void SetWindowStringProp(IntPtr hwnd, string propName, string? value)
    {
        try
        {
            var old = NativeMethods.GetPropW(hwnd, propName);
            if (old != IntPtr.Zero)
            {
                NativeMethods.GlobalDeleteAtom((ushort)(old.ToInt32() & 0xFFFF));
                NativeMethods.RemovePropW(hwnd, propName);
            }
            if (value == null) return;
            if (value.Length > 240) value = value[..240];
            var atom = NativeMethods.GlobalAddAtomW(value);
            if (atom != 0) NativeMethods.SetPropW(hwnd, propName, (IntPtr)atom);
        }
        catch { }
    }

    /// <summary>Read a string from a window property (GlobalAtom).</summary>
    static string? GetWindowStringProp(IntPtr hwnd, string propName)
    {
        try
        {
            var handle = NativeMethods.GetPropW(hwnd, propName);
            if (handle == IntPtr.Zero) return null;
            var atom = (ushort)(handle.ToInt32() & 0xFFFF);
            var sb = new System.Text.StringBuilder(256);
            var len = NativeMethods.GlobalGetAtomNameW(atom, sb, sb.Capacity);
            return len > 0 ? sb.ToString() : null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Post or update a Slack status message for an AI instance.
    /// Same state type → edit in place (no channel spam).
    /// State type changed → delete old + post new (moves to bottom = "latest message").
    /// Also persists SlackTs and text to window props for restart resilience.
    /// </summary>
    static async Task PostOrUpdateAiStatusAsync(
        IntPtr hwnd, ClaudeInstanceState state,
        string slackText, string statusType,
        string slackBotToken, string slackChannel, string instanceUsername,
        string statusTsFile, IntPtr claudeHwnd, long jsonlSize = 0)
    {
        // ── Step 0: idle→idle skip — no change, no spam ──
        if (statusType == "idle" && state.LastStatusType == "idle" && state.SlackStatusTs != null)
            return;

        // ── Step 1: Is our status message still the latest in the channel? ──────────────────
        // "Latest = ours" → edit in place (streaming).
        // "Latest = someone else" → delete our old status + post new below.
        bool latestIsOurs = false;
        bool hasReplies = false;

        if (state.SlackStatusTs != null)
        {
            try
            {
                var (latestTs, _, _, _) = await GetChannelLatestMessageInfo(slackBotToken, slackChannel);
                latestIsOurs = latestTs == state.SlackStatusTs;
            }
            catch { }

            try { hasReplies = await SlackMessageHasRepliesAsync(slackBotToken, slackChannel, state.SlackStatusTs); }
            catch { }
        }

        // ── Step 2: Edit in place if we are the latest (and no replies on our message) ──────
        // EXCEPTION: idle↔active transition → always delete old + post new (shows exact transition time).
        bool isIdleTransition = state.LastStatusType != null && state.LastStatusType != statusType
                                && (statusType == "idle" || state.LastStatusType == "idle");
        if (latestIsOurs && !hasReplies && !isIdleTransition)
        {
            var (ok, _, editError) = await SlackUpdateMessageAsync(slackBotToken, slackChannel, state.SlackStatusTs!, slackText);
            if (ok)
            {
                state.LastStatusType = statusType;
                state.LastSlackStatusText = slackText;
                if (jsonlSize > 0) state.LastPostedJsonlSize = jsonlSize; // advance watermark
                SetWindowStringProp(hwnd, PropAiOut, slackText);
                SetWindowStringProp(hwnd, PropStatusType, statusType);
                Console.WriteLine($"[EYE] Edit [{statusType}]: {slackText[..Math.Min(slackText.Length, 80)]}");
            }
            else
            {
                Console.WriteLine($"[EYE] Edit failed ({editError}) {slackText.Length}B");
            }
            return;
        }

        // ── Step 3: Not latest (or idle↔active transition) → delete old + post new ──────

        // Delete our previous status (unless it has replies — never destroy a thread)
        if (state.SlackStatusTs != null && !hasReplies)
        {
            var oldTs = state.SlackStatusTs;
            state.SlackStatusTs = null;
            SetWindowStringProp(hwnd, PropSlackTs, null);
            await SlackDeleteMessageAsync(slackBotToken, slackChannel, oldTs);
            Console.WriteLine($"[EYE] Deleted old status (not latest) → posting new");
        }
        else if (hasReplies)
        {
            // Thread started on old status → leave it, post fresh below
            state.SlackStatusTs = null;
            SetWindowStringProp(hwnd, PropSlackTs, null);
        }

        // Post new status at bottom
        var (postOk, ts) = await SlackSendViaApi(slackBotToken, slackChannel, slackText, username: instanceUsername);
        if (postOk && ts != null)
        {
            state.SlackStatusTs = ts;
            state.LastStatusType = statusType;
            state.LastSlackStatusText = slackText;
            if (jsonlSize > 0) state.LastPostedJsonlSize = jsonlSize; // advance watermark
            if (hwnd == claudeHwnd)
                try { File.WriteAllText(statusTsFile, ts); } catch { }
            SetWindowStringProp(hwnd, PropSlackTs, ts);
            SetWindowStringProp(hwnd, PropAiOut, slackText);
            SetWindowStringProp(hwnd, PropStatusType, statusType);
            Console.WriteLine($"[EYE] Post [{statusType}]: {slackText[..Math.Min(slackText.Length, 80)]}");

            // Sweep same-username stale status messages (duplicates from Eye restart/hot-swap)
            _ = Task.Run(async () =>
            {
                try
                {
                    using var http = new System.Net.Http.HttpClient();
                    http.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", slackBotToken);
                    var resp = await http.GetStringAsync($"https://slack.com/api/conversations.history?channel={slackChannel}&limit=15");
                    var json = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(resp);
                    if (json?["ok"]?.GetValue<bool>() != true) return;
                    var msgs = json["messages"] as System.Text.Json.Nodes.JsonArray;
                    if (msgs == null) return;
                    foreach (var m in msgs)
                    {
                        var mTs = m?["ts"]?.GetValue<string>();
                        var mUser = m?["username"]?.GetValue<string>();
                        var mText = m?["text"]?.GetValue<string>() ?? "";
                        if (mTs == null || mTs == ts || mUser != instanceUsername) continue;
                        // Only delete status messages (defined in StatusEmojis), not user conversations
                        if (!IsStatusEmoji(mText)) continue;
                        var replyCount = m?["reply_count"]?.GetValue<int>() ?? 0;
                        if (replyCount > 0) continue; // protect threads
                        await SlackDeleteMessageAsync(slackBotToken, slackChannel, mTs);
                        Console.WriteLine($"[EYE] Cleaned stale status for '{instanceUsername}': {mTs}");
                    }
                }
                catch { /* best effort */ }
            });

            // Sweep stale startup messages now that the new post is visible (no gap).
            // Delay 8s so all instances finish their first tick and populate _instanceStates —
            // then read active ts from window atom props directly (most reliable source).
            if (_staleStatusTsOnStartup.Count > 0)
            {
                var pendingStale = _staleStatusTsOnStartup.ToList();
                _staleStatusTsOnStartup.Clear();
                var newTs = ts;
                _ = Task.Run(async () =>
                {
                    await Task.Delay(8000); // let all instances load state
                    // Read active ts from window atom props (authoritative, even if _instanceStates not yet populated)
                    var activeFromProps = FindAllPromptsViaMcp()
                        .Select(p => GetWindowStringProp(p.WindowHandle, PropSlackTs))
                        .Where(t => t != null)
                        .ToHashSet()!;
                    // Also include in-memory instance states (belt-and-suspenders)
                    foreach (var s in _instanceStates.Values)
                        if (s.SlackStatusTs != null) activeFromProps.Add(s.SlackStatusTs);
                    activeFromProps.Add(newTs);
                    var stale = pendingStale.Where(x => !activeFromProps.Contains(x)).ToList();
                    if (stale.Count > 0)
                    {
                        foreach (var staleTs in stale)
                            await SlackDeleteMessageAsync(slackBotToken, slackChannel, staleTs);
                        Console.WriteLine($"[EYE] Swept {stale.Count} stale status message(s) after first post");
                    }
                });
            }
        }
        else
        {
            Console.WriteLine($"[EYE] Post FAILED [{statusType}] ({slackText.Length}B)");
        }
    }

    // ── Independent startup stale sweep ──────────────────────────────────────────────────
    // Runs 20s after Eye startup regardless of whether a new post was made.
    // Fixes: if all ticks are "idle" at startup (idle → skip new post → sweep never fires).
    internal static async Task SweepStaleOnStartupAsync(string slackBotToken, string slackChannel)
    {
        if (_staleStatusTsOnStartup.Count == 0) return;
        await Task.Delay(20_000); // let all instances post their first status
        if (_staleStatusTsOnStartup.Count == 0) return; // already swept by first-post mechanism
        var pending = _staleStatusTsOnStartup.ToList();
        _staleStatusTsOnStartup.Clear();
        var activeFromProps = FindAllPromptsViaMcp()
            .Select(p => GetWindowStringProp(p.WindowHandle, PropSlackTs))
            .Where(t => t != null)
            .ToHashSet()!;
        foreach (var s in _instanceStates.Values)
            if (s.SlackStatusTs != null) activeFromProps.Add(s.SlackStatusTs);
        var stale = pending.Where(x => !activeFromProps.Contains(x)).ToList();
        if (stale.Count > 0)
        {
            foreach (var staleTs in stale)
                await SlackDeleteMessageAsync(slackBotToken, slackChannel, staleTs);
            Console.WriteLine($"[EYE] Startup sweep (timer): {stale.Count} stale status(es) deleted");
        }
    }

    // ── Homework injection ────────────────────────────────────────────────────────────────
    // Persisted homework cooldown file — survives Eye restarts (per CWD key).
    static string _homeworkStatePath => Path.Combine(DataDir, "homework_state.json");

    static DateTime? LoadHomeworkAt(string cwdKey)
    {
        try
        {
            if (!File.Exists(_homeworkStatePath)) return null;
            var json = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(_homeworkStatePath));
            var val = json?[cwdKey]?.GetValue<string>();
            return val != null ? DateTime.Parse(val, null, System.Globalization.DateTimeStyles.RoundtripKind) : null;
        }
        catch { return null; }
    }

    static void SaveHomeworkAt(string cwdKey, DateTime at)
    {
        try
        {
            System.Text.Json.Nodes.JsonObject obj;
            if (File.Exists(_homeworkStatePath))
            {
                var existing = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(_homeworkStatePath));
                obj = existing as System.Text.Json.Nodes.JsonObject ?? new();
            }
            else obj = new();
            obj[cwdKey] = at.ToString("O");
            File.WriteAllText(_homeworkStatePath, obj.ToJsonString());
        }
        catch { }
    }

    // Fired once per idle session (HomeworkNotified guard) when Claude has been idle 1+ min.
    // All idle instances (Codex, Claude Desktop, OpenClaw, etc.). Reads suggestions.jsonl for pending items,
    // types prompt directly via TypeAndSubmit (NO /clear, NO policy injection).
    static void CheckAndSendHomework(ClaudeInstanceState state, IntPtr hwnd, string label)
    {
        state.HomeworkNotified = true;  // set first — prevent double-fire even on exception
        // NOTE: LastHomeworkAt (1h cooldown) is set only after successful delivery — not here.
        // This allows skip-and-retry without waiting 1h.
        var cwdKey = state.FullCwd ?? state.CwdLabel;

        var suggPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(suggPath)) return;

        // Count non-done/non-archived items that belong to this instance's CWD
        var instanceCwd = state.FullCwd ?? state.CwdLabel;
        var pending = new List<string>();
        foreach (var line in File.ReadAllLines(suggPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var node = System.Text.Json.Nodes.JsonNode.Parse(line);
                var status = node?["status"]?.GetValue<string>() ?? "pending";
                if (status is "done" or "archived") continue;
                // Route to submitter's CWD — skip items not meant for this instance
                var suggCwd = node?["cwd"]?.GetValue<string>();
                if (!string.IsNullOrEmpty(suggCwd) && !string.IsNullOrEmpty(instanceCwd))
                {
                    // Match if suggCwd starts with or equals instanceCwd (handles trailing slash variance)
                    if (!suggCwd.StartsWith(instanceCwd, StringComparison.OrdinalIgnoreCase) &&
                        !instanceCwd.StartsWith(suggCwd, StringComparison.OrdinalIgnoreCase))
                        continue;
                }
                var text = node?["text"]?.GetValue<string>() ?? "";
                // First line only for summary
                var summary = text.Split('\n')[0];
                if (summary.Length > 100) summary = summary[..100] + "...";
                pending.Add(summary);
            }
            catch { }
        }

        if (pending.Count == 0) return; // no pending → nothing to do

        // Build prompt (English, as user requested)
        var summaryLines = string.Join("\n", pending.Take(5).Select((s, i) => $"  {i + 1}. {s}"));
        var moreNote = pending.Count > 5 ? $"\n  ... and {pending.Count - 5} more" : "";
        var prompt =
            $"You have {pending.Count} pending suggestion homework item(s) in suggestions.jsonl.\n" +
            $"File: {suggPath}\n\n" +
            $"Items:\n{summaryLines}{moreNote}\n\n" +
            $"Please review and process them when you have a chance.";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[EYE] {label}Homework injection → {pending.Count} pending suggestions");
        Console.ResetColor();

        // Deliver via MCP subprocess (prompt send with --when-idle) — keeps UIA out of Eye memory.
        // If window gone, TrySpawnAppbotVsCode will open VS Code and deliver after it's ready.
        try
        {
            var cwdFilter = state.FullCwd ?? state.CwdLabel;

            // Set 1h cooldown now — persisted to disk
            state.LastHomeworkAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(cwdKey))
                SaveHomeworkAt(cwdKey, state.LastHomeworkAt.Value);

            // Route through MCP: prompt send finds window by name, respects idle check
            var (output, exitCode) = EyeMcpClient.CallAsync(
                ["prompt", "send", cwdFilter ?? "", prompt, "--when-idle", "30s", "--timeout", "2m"],
                timeoutMs: 150_000).GetAwaiter().GetResult();

            if (exitCode != 0)
            {
                Console.WriteLine($"[EYE] {label}Homework: MCP delivery failed (exit={exitCode}) — spawning VS Code");
                state.HomeworkNotified = false; // re-arm
                TrySpawnAppbotVsCode(prompt);
            }
            else
            {
                Console.WriteLine($"[EYE] {label}Homework delivered via MCP: {output.TrimEnd()}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] Homework delivery failed: {ex.Message}");
        }
    }

}
