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
    }

    // Per-hwnd instance state dictionary. Entries added on first encounter, purged when window gone.
    static readonly Dictionary<IntPtr, ClaudeInstanceState> _instanceStates = new();

    static ClaudeInstanceState GetOrCreateInstanceState(IntPtr hwnd)
    {
        if (!_instanceStates.TryGetValue(hwnd, out var state))
        {
            state = new ClaudeInstanceState();
            // Try to find a CWD label from cached cards matching this hwnd's PID
            state.CwdLabel = ResolveInstanceCwdLabel(hwnd);
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
        // Try to find matching EyeParentCard via PID
        try
        {
            GetWindowThreadProcessId(hwnd, out var pid);
            if (pid > 0)
            {
                // Check _cachedCards for a card whose ParentPid matches
                var card = _cachedCards.FirstOrDefault(c => c.ParentPid == (int)pid);
                if (card != null && !string.IsNullOrEmpty(card.Cwd))
                    return AbbreviateCwd(card.Cwd);
            }
        }
        catch { }

        // Fallback: short form of Claude Desktop window title
        try
        {
            var pi = ClaudePromptHelper.GetAllCachedPrompts().FirstOrDefault(p => p.WindowHandle == hwnd);
            if (pi != null && !string.IsNullOrEmpty(pi.WindowTitle))
            {
                // Window title is usually "Claude" or a project name — take up to 15 chars
                var t = pi.WindowTitle.Replace("— Claude", "").Trim();
                if (t.Length > 15) t = t[..15] + "…";
                if (!string.IsNullOrEmpty(t)) return t;
            }
        }
        catch { }

        return $"0x{hwnd:X8}";
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
        Dictionary<string, long> contextWarnedSizes)
    {
        const int RateLimitCooldownMinutes = 30;
        const int RateLimitAlertCooldownMinutes = 30;

        // ── Purge dead hwnds ──
        var deadHwnds = _instanceStates.Keys.Where(h => !IsWindow(h)).ToList();
        foreach (var h in deadHwnds) _instanceStates.Remove(h);

        // ── Re-find primary Claude window if lost ──
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            claudeHwnd = FindClaudeDesktopWindow();

        // ── Collect all Claude Desktop instances from prompt cache ──
        var claudeInstances = ClaudePromptHelper.GetAllCachedPrompts()
            .Where(p => p.HostType == "claude-desktop" && IsWindow(p.WindowHandle))
            .Select(p => p.WindowHandle)
            .Distinct()
            .ToList();

        // Include primary window if not already in list
        if (claudeHwnd != IntPtr.Zero && !claudeInstances.Contains(claudeHwnd))
            claudeInstances.Add(claudeHwnd);

        // ── Per-card context usage monitor (runs once per tick, not per instance) ──
        try
        {
            foreach (var card in _cachedCards)
            {
                if (string.IsNullOrWhiteSpace(card.Cwd)) continue;
                var (pct, _, jsonlPath, fileSize) = GetContextInfoForCwdEx(card.Cwd);
                if (pct < 90 || jsonlPath == null) continue;

                var cwdKey = card.Cwd.Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                contextWarnedSizes.TryGetValue(cwdKey, out long prevWarnedSize);
                if (fileSize <= prevWarnedSize) continue; // already warned at this size

                var sizeMB = fileSize / (1024.0 * 1024.0);
                const double ContextLimitMB = 40.0;
                var cwdTag = AbbreviateCwd(card.Cwd);
                contextWarnedSizes[cwdKey] = fileSize;

                if (pct >= 95 && !string.IsNullOrEmpty(slackBotToken))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[EYE] 🚨 [{cwdTag}] Context {pct}%! ({sizeMB:F1}MB/{ContextLimitMB}MB) — 즉시 인수인계하세요!");
                    Console.WriteLine($"[EYE] 명령: wkappbot newchat \"인수인계 프롬프트\"");
                    Console.ResetColor();
                    Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                        $":rotating_light: *[{cwdTag}] 컨텍스트 {pct}%!* ({sizeMB:F1}/{ContextLimitMB}MB)\n클롣이 아직 인수인계 안 했습니다! `wkappbot newchat` 실행 필요",
                        username: "클롣아이")).Wait(3000);
                }
                else if (pct >= 90 && !string.IsNullOrEmpty(slackBotToken))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] ⚠️ [{cwdTag}] Context {pct}%! ({sizeMB:F1}MB/{ContextLimitMB}MB) — 인수인계 준비하세요");
                    Console.ResetColor();

                    var handoff = BuildHandoffPrompt(jsonlPath, _cachedCards, sizeMB, ContextLimitMB);
                    try
                    {
                        using var ctxHelper = new ClaudePromptHelper();
                        var pi = ctxHelper.FindPromptForCwd(card.Cwd);
                        if (pi != null)
                        {
                            ClaudePromptHelper.AllowFocusSteal = true;
                            try
                            {
                                var nudge = $"⚠️ 컨텍스트 {pct}% 도달! ({sizeMB:F1}/{ContextLimitMB}MB)\n"
                                    + "준비되면 아래 명령을 실행:\n\n"
                                    + $"wkappbot newchat \"{handoff.Replace("\"", "\\\"")}\"";
                                ctxHelper.TypeAndSubmit(pi, nudge);
                                Console.WriteLine($"[EYE] ✅ [{cwdTag}] Handoff nudge sent");
                                Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                    $":warning: *[{cwdTag}] 컨텍스트 {pct}%!* ({sizeMB:F1}/{ContextLimitMB}MB)\n클롣에게 인수인계 프롬프트를 전달했습니다.",
                                    username: "클롣아이")).Wait(3000);
                            }
                            finally { ClaudePromptHelper.AllowFocusSteal = false; }
                        }
                        else
                        {
                            Console.WriteLine($"[EYE] ⚠️ [{cwdTag}] No matching prompt — check Slack");
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                $":warning: *[{cwdTag}] 컨텍스트 {pct}%!* ({sizeMB:F1}/{ContextLimitMB}MB)\nClaude 창을 찾지 못해 인수인계 미전달! 직접 확인해주세요.",
                                username: "클롣아이")).Wait(3000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[EYE] ⚠️ [{cwdTag}] Handoff failed: {ex.Message}");
                        Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                            $":warning: *[{cwdTag}] 컨텍스트 {pct}%!* ({sizeMB:F1}/{ContextLimitMB}MB)\n인수인계 전달 오류: {ex.Message}",
                            username: "클롣아이")).Wait(3000);
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

        foreach (var hwnd in claudeInstances)
        {
            var state = GetOrCreateInstanceState(hwnd);
            var label = string.IsNullOrEmpty(state.CwdLabel) ? "" : $"[{state.CwdLabel}] ";

            try
            {
                var claudeStatus = DetectClaudeDesktopStatus(hwnd);

                if (claudeStatus != null)
                {
                    if (claudeStatus.Item1 == "executing")
                        state.LastExecutingText = claudeStatus.Item2;

                    combinedStatusText = $"{label}Claude: {claudeStatus.Item2}";

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
                            Task.Run(async () =>
                                await SlackSendViaApi(slackBotToken!, slackChannel!, alertMsg, username: "클롣아이"))
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

                            string slackText;
                            if (claudeStatus.Item1 == "prompt_ready")
                            {
                                // Skip — spinner detection handles idle below
                                goto skipStatusStreaming;
                            }
                            else
                            {
                                // Active: only reset idle if spinner is animating again
                                if (state.IdleMessageSent)
                                {
                                    if (DetectSpinnerIdle(hwnd, state)) goto skipStatusStreaming;
                                    // Spinner animating again → activity resumed
                                    ResetSpinnerDetection(state);
                                    state.IdleAfterText = null;
                                }
                                state.IdleMessageSent = false;
                                var statusEmoji = claudeStatus.Item1 switch
                                {
                                    "executing" => ":gear:",
                                    "plan_approval_pending" => ":clipboard:",
                                    "plan_mode" => ":memo:",
                                    "rate_limit" => ":warning:",
                                    _ => ":robot_face:"
                                };
                                slackText = $"{statusEmoji} {label}Claude: {claudeStatus.Item2}";
                            }

                            if (slackText == state.LastSlackStatusText) goto skipStatusStreaming;

                            // Delete old → post new (fresh timestamp)
                            if (state.SlackStatusTs != null)
                            {
                                var oldTs = state.SlackStatusTs;
                                Task.Run(async () => await SlackDeleteMessageAsync(
                                    slackBotToken!, slackChannel!, oldTs)).Wait(3000);
                                state.SlackStatusTs = null;
                            }
                            {
                                var (ok, ts) = Task.Run(async () =>
                                    await SlackSendViaApi(slackBotToken!, slackChannel!, slackText, username: botUsername))
                                    .GetAwaiter().GetResult();
                                if (ok && ts != null)
                                {
                                    state.SlackStatusTs = ts;
                                    if (hwnd == claudeHwnd) // persist primary instance ts to file
                                        try { File.WriteAllText(statusTsFile, ts); } catch { }
                                }
                                state.LastSlackStatusText = slackText;
                            }
                        }
                        catch { /* best-effort */ }

                        skipStatusStreaming:
                        // ── Spinner pixel idle ──
                        if (!state.IdleMessageSent
                            && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                        {
                            try
                            {
                                if (DetectSpinnerIdle(hwnd, state))
                                {
                                    if (state.IdleAfterText == null)
                                    {
                                        if (state.LastExecutingText != null)
                                            state.IdleAfterText = state.LastExecutingText;
                                        state.LastExecutingText = null;
                                    }
                                    var idleSuffix = state.IdleAfterText != null ? $" after: {state.IdleAfterText}" : "";
                                    var idleMsg = $":zzz: {label}Idle{idleSuffix}";

                                    if (state.SlackStatusTs != null)
                                    {
                                        var oldTs = state.SlackStatusTs;
                                        Task.Run(async () => await SlackDeleteMessageAsync(
                                            slackBotToken!, slackChannel!, oldTs)).Wait(3000);
                                        state.SlackStatusTs = null;
                                    }
                                    var (idleOk, idleTs) = Task.Run(async () =>
                                        await SlackSendViaApi(slackBotToken!, slackChannel!, idleMsg, username: botUsername))
                                        .GetAwaiter().GetResult();
                                    if (idleOk && idleTs != null)
                                    {
                                        state.SlackStatusTs = idleTs;
                                        if (hwnd == claudeHwnd)
                                            try { File.WriteAllText(statusTsFile, idleTs); } catch { }
                                    }
                                    state.LastSlackStatusText = idleMsg;
                                    state.IdleMessageSent = true;
                                    Console.WriteLine($"[EYE] {label}Spinner idle: {idleMsg}");
                                }
                            }
                            catch { }
                        }

                        // ── Plan approval → Slack ──
                        if (claudeStatus.Item1 == "plan_approval_pending" && !state.PlanApprovalSentToSlack)
                        {
                            try
                            {
                                var plan = ExtractPlanContent(hwnd);
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
        } // end foreach claudeInstances

        return combinedStatusText;
    }

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
}
