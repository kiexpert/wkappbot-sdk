// AppBotEyeSlackHandlers.cs — Shared Slack event handlers + schedule executor for AppBotEye.
// Used by EyeGlobalPollingLoop (GlobalMode) — the only Eye mode.
// Kept as separate partial class file for readability (SetupSlackEventHandlers is ~440 lines).

using System.Text.Json.Nodes;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Build the "(Slack ... — wkappbot slack reply ...)" suffix appended to every forwarded prompt.
    /// ONE place to change the format — never duplicate this string elsewhere!
    /// </summary>
    static string SlackReplySuffix(string user, string replyTs, string? label = null)
    {
        var tag = string.IsNullOrEmpty(label) ? $"Slack @{user}" : $"Slack {label} @{user}";
        return $"({tag} — wkappbot slack reply \"MUST reply here\" --msg {replyTs})";
    }

    /// <summary>
    /// Set up Slack event handlers for AppBotEye-integrated Slack daemon.
    /// Handles @mentions, keyword monitoring, thread reply forwarding, and plan approval responses.
    /// RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
    /// </summary>
    private static void SetupSlackEventHandlers(SlackSocketClient slack, string botToken, string? channel,
        IntPtr claudeHwnd = default, Func<string?>? getPlanApprovalTs = null,
        Func<string?>? getPermissionApprovalTs = null,
        string? startupTs = null, string? botUsername = null,
        Func<string?>? getStatusStreamingTs = null, Action? resetStatusStreaming = null)
    {
        // Track threads where bot is engaged (for thread reply forwarding)
        // RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
        var activeThreads = new HashSet<string>();
        // Dedup: messages already forwarded by OnMention → skip in OnMessage thread handler
        var handledByMention = new HashSet<string>();
        if (!string.IsNullOrEmpty(startupTs))
            activeThreads.Add(startupTs);

        // Restore active threads from pending_acks.json (survives Eye restart)
        var savedAcks = LoadPendingAcks();
        foreach (var threadTs in savedAcks.Keys)
            activeThreads.Add(threadTs);
        if (savedAcks.Count > 0)
            Console.WriteLine($"[EYE][SLACK] Restored {savedAcks.Count} active thread(s) from pending_acks.json");

        // Track "전달했습니다" ack messages per thread → delete when Claude responds via this bot
        // Key: threadTs, Value: (channel, ackMessageTs)
        var pendingAcks = new Dictionary<string, (string channel, string ackTs)>();

        // Local helper: send with bot username (multi-instance identification)
        async Task<(bool ok, string? ts)> Send(string ch, string text, string? threadTs = null)
            => await SlackSendViaApi(botToken, ch, text, threadTs, username: botUsername);

        // Local helper: delete previous "전달했습니다" ack in a thread (file-based IPC)
        void DeletePendingAck(string threadKey)
        {
            // In-memory cleanup
            if (pendingAcks.TryGetValue(threadKey, out var ack))
            {
                pendingAcks.Remove(threadKey);
                Task.Run(async () => await SlackDeleteMessageAsync(botToken, ack.channel, ack.ackTs)).Wait(3000);
            }
            // File-based cleanup (for cross-process sharing with CLI)
            DeletePendingAckFromFile(botToken, threadKey);
        }

        // Local helper: send ack "전달했습니다" and track it for later deletion
        // Deleted when slack reply is sent (not auto-deleted — stays visible until response)
        void SendAndTrackAck(string ch, string threadKey)
        {
            var ackText = $"Claude에 전달했습니다! (thread={threadKey})";
            var (ackOk, ackTs) = Task.Run(async () => await Send(ch,
                ackText, threadKey)).GetAwaiter().GetResult();
            if (ackOk && ackTs != null)
            {
                pendingAcks[threadKey] = (ch, ackTs);
                activeThreads.Add(ackTs);
                // Persist to file for cross-process access (CLI can delete when replying)
                SavePendingAck(threadKey, ch, ackTs);
            }
        }

        // Handle @mentions
        slack.OnMention += (msg) =>
        {
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
            Console.WriteLine($"[EYE][SLACK] @mention from {msg.User}: {cleanText}");

            // Track this thread so ALL follow-up messages come through
            var threadKey = msg.ThreadTs ?? msg.Timestamp;
            activeThreads.Add(threadKey);

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread @mention → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
            }

            // ── Plan approval via Slack @mention ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && claudeHwnd != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(claudeHwnd);
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(claudeHwnd, cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via Slack @mention or thread reply ──
            var permTs = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs) && msg.ThreadTs == permTs && claudeHwnd != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText)) // "승인", "ㄱㄱ", "ㅇㅇ" etc
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var permButtons = GetPermissionButtons(claudeHwnd);
                    var allowBtn = permButtons.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permButtons.FirstOrDefault(); // fallback to first button

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(claudeHwnd, allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Normal @mention: forward to Claude Code prompt ──
            var promptHelper = new ClaudePromptHelper();
            var promptInfo = promptHelper.FindPrompt();
            if (promptInfo != null)
            {
                // Build thread context (starter + previous message) for Claude
                var threadContext = "";
                var ackThread = msg.ThreadTs ?? msg.Timestamp;
                if (msg.ThreadTs != null)
                {
                    var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                    if (!string.IsNullOrEmpty(ctx))
                        threadContext = $"\n{ctx}\n";
                }

                var replyThread = msg.ThreadTs ?? msg.Timestamp;
                var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, replyThread)}";
                promptHelper.TypeAndSubmit(promptInfo, promptText);
                handledByMention.Add(msg.Timestamp); // dedup: OnMessage won't re-forward
                Console.WriteLine("[EYE][SLACK] >> Sent to Claude prompt (with thread context)");

                DeletePendingAck(ackThread);
                SendAndTrackAck(msg.Channel, ackThread);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[EYE][SLACK] Prompt not found! Claude 상태 확인 중...");
                Console.ResetColor();

                var ackThread = msg.ThreadTs ?? msg.Timestamp;
                try
                {
                    // Check Claude Desktop status for better diagnosis
                    var claudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                    var statusInfo = claudeStatus != null
                        ? $"Claude 상태: {claudeStatus.Item2}"
                        : "Claude 상태: 감지 불가";

                    var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                        $"{statusInfo}\n" +
                        $"받은 메시지: {cleanText}";
                    Task.Run(async () => await Send(msg.Channel, diagMsg, ackThread)).Wait(5000);
                }
                catch
                {
                    Task.Run(async () => await Send(msg.Channel,
                        $"(Claude 프롬프트 없음) 받은 메시지: {cleanText}", ackThread)).Wait(5000);
                }
            }
        };

        // Handle bot's own messages in threads → delete pending "전달했습니다" ack
        slack.OnSelfMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.ThreadTs)) return;
            // If this bot just posted a REAL response (not ack), delete the pending ack
            if (!msg.Text.StartsWith("Claude에 전달했습니다!"))
            {
                DeletePendingAck(msg.ThreadTs);
                Console.WriteLine($"[EYE][SLACK] Deleted ack in thread {msg.ThreadTs} (bot replied)");
            }
        };

        // Handle channel messages (thread reply forwarding + keyword monitoring + plan approval)
        var keywords = new[] { "클롣", "클롯", "클봇", "claude", "appbot", "wkappbot" };
        slack.OnMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.Text)) return;

            // Debug: log thread info for diagnosis
            Console.WriteLine($"[EYE][SLACK] MSG from={msg.User} ch={msg.Channel} thread={msg.ThreadTs ?? "(none)"} text={msg.Text[..Math.Min(msg.Text.Length, 40)]}");

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread reply detected → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
                // Don't return — continue to forward the message to Claude prompt as well
            }

            // ── Plan approval via thread reply (no @mention needed) ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && claudeHwnd != IntPtr.Zero)
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(claudeHwnd);
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
                else if (cleanText.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack thread: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(claudeHwnd, cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via thread reply (no @mention needed) ──
            var permTs2 = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs2) && msg.ThreadTs == permTs2 && claudeHwnd != IntPtr.Zero)
            {
                var cleanPerm = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanPerm))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var permBtns = GetPermissionButtons(claudeHwnd);
                    var allowBtn = permBtns.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permBtns.FirstOrDefault();

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(claudeHwnd, allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Auto-track threads on bot's own messages (API check) ──
            if (msg.ThreadTs != null && !activeThreads.Contains(msg.ThreadTs))
            {
                if (IsOwnThread(botToken, msg.Channel, msg.ThreadTs))
                {
                    activeThreads.Add(msg.ThreadTs);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[EYE][SLACK] Auto-tracking thread on bot's message (ts={msg.ThreadTs})");
                    Console.ResetColor();
                }
            }

            // ── Thread reply to bot's message → ALWAYS forward to Claude prompt ──
            if (msg.ThreadTs != null && activeThreads.Contains(msg.ThreadTs))
            {
                // Dedup: already forwarded by OnMention handler → skip
                if (handledByMention.Remove(msg.Timestamp)) return;

                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrEmpty(cleanText)) return;

                // "그만" / "stop" → stop tracking this thread
                var trimmedLower = cleanText.Trim().ToLowerInvariant();
                if (trimmedLower == "그만" || trimmedLower == "stop" || trimmedLower == "ㄱㅁ")
                {
                    activeThreads.Remove(msg.ThreadTs);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[EYE][SLACK] << thread stopped by {msg.User} (ts={msg.ThreadTs})");
                    Console.ResetColor();
                    Task.Run(async () => await Send(msg.Channel,
                        "알겠습니다~ 이 쓰레드에서 물러납니다!", msg.ThreadTs)).Wait(5000);
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[EYE][SLACK] << thread reply from {msg.User}: {cleanText}");
                Console.ResetColor();

                var trPromptHelper = new ClaudePromptHelper();
                var trPromptInfo = trPromptHelper.FindPrompt();
                if (trPromptInfo != null)
                {
                    // Build thread context (starter + previous message) for Claude
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n{ctx}\n";
                    }

                    var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, msg.ThreadTs!, "thread reply")}";
                    trPromptHelper.TypeAndSubmit(trPromptInfo, promptText);
                    Console.WriteLine("[EYE][SLACK] >> Thread reply sent to Claude prompt (with context)");

                    DeletePendingAck(msg.ThreadTs!);
                    SendAndTrackAck(msg.Channel, msg.ThreadTs!);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Thread reply: Prompt not found!");
                    Console.ResetColor();

                    try
                    {
                        var trClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var trStatusInfo = trClaudeStatus != null
                            ? $"Claude 상태: {trClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";
                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{trStatusInfo}\n" +
                            $"스레드 답장: {cleanText}";
                        Task.Run(async () => await Send(msg.Channel, diagMsg, msg.ThreadTs)).Wait(5000);
                    }
                    catch
                    {
                        Task.Run(async () => await Send(msg.Channel,
                            $"(Claude 프롬프트 없음) 스레드 답장: {cleanText}", msg.ThreadTs)).Wait(5000);
                    }
                }
                return;
            }

            // ── Keyword monitoring → forward to Claude prompt ──
            var textLower = msg.Text.ToLowerInvariant();
            var matchedKw = keywords.FirstOrDefault(kw => textLower.Contains(kw.ToLowerInvariant()));
            if (matchedKw != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[EYE][SLACK] keyword \"{matchedKw}\" from {msg.User}: {msg.Text}");
                Console.ResetColor();

                // Start tracking this thread so follow-up messages also come through
                var kwThreadKey = msg.ThreadTs ?? msg.Timestamp;
                activeThreads.Add(kwThreadKey);

                var cleanKwText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrEmpty(cleanKwText)) return;

                var kwPromptHelper = new ClaudePromptHelper();
                var kwPromptInfo = kwPromptHelper.FindPrompt();
                if (kwPromptInfo != null)
                {
                    // Build thread context (starter + previous message) for Claude
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n{ctx}\n";
                    }

                    var kwReplyThread = msg.ThreadTs ?? msg.Timestamp;
                    var promptText = $"{cleanKwText}{threadContext}\n{SlackReplySuffix(msg.User, kwReplyThread, $"keyword:\"{matchedKw}\"")}";
                    kwPromptHelper.TypeAndSubmit(kwPromptInfo, promptText);
                    Console.WriteLine("[EYE][SLACK] >> Keyword match sent to Claude prompt (with context)");

                    DeletePendingAck(kwThreadKey);
                    SendAndTrackAck(msg.Channel, kwThreadKey);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Keyword match: Prompt not found!");
                    Console.ResetColor();

                    try
                    {
                        var kwClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var kwStatusInfo = kwClaudeStatus != null
                            ? $"Claude 상태: {kwClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";
                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{kwStatusInfo}\n" +
                            $"키워드 \"{matchedKw}\" 감지: {cleanKwText}";
                        Task.Run(async () => await Send(msg.Channel, diagMsg, kwThreadKey)).Wait(5000);
                    }
                    catch
                    {
                        Task.Run(async () => await Send(msg.Channel,
                            $"(Claude 프롬프트 없음) 키워드 \"{matchedKw}\" 감지: {cleanKwText}", kwThreadKey)).Wait(5000);
                    }
                }
            }
        };
    }

    /// <summary>
    /// Execute a single schedule item: resolve prompt text, find Claude prompt, type and submit.
    /// Updates schedule status to done/failed. Sends Slack notification.
    /// </summary>
    private static void ExecuteScheduleItem(ScheduleItem item, string? slackBotToken, string? slackChannel)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[SCHEDULE] Executing: {item.Id} ({item.Type})");
        Console.ResetColor();

        try
        {
            // 1. Resolve prompt text
            string? promptText = item.Prompt;
            if (!string.IsNullOrEmpty(item.PromptFile))
            {
                if (File.Exists(item.PromptFile))
                    promptText = File.ReadAllText(item.PromptFile);
                else
                {
                    ScheduleManager.UpdateStatus(item.Id, "failed", $"Prompt file not found: {item.PromptFile}");
                    ScheduleNotifySlack(slackBotToken, slackChannel,
                        $":x: 스케줄 실패 ({item.Id}): 프롬프트 파일 없음 — {item.PromptFile}");
                    return;
                }
            }

            if (string.IsNullOrEmpty(promptText))
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Empty prompt");
                return;
            }

            // 2. Slack pre-notification
            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":rocket: 스케줄 실행 중: {item.Id} ({item.Type})");

            // 3. Find Claude prompt and type
            var promptHelper = new ClaudePromptHelper();
            var promptInfo = promptHelper.FindPrompt();
            if (promptInfo == null)
            {
                // Retry once after 3 seconds (Claude may still be loading)
                Thread.Sleep(3000);
                promptInfo = promptHelper.FindPrompt();
            }

            if (promptInfo == null)
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Claude prompt not found");
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":x: 스케줄 실패 ({item.Id}): Claude 프롬프트를 찾을 수 없습니다");
                return;
            }

            // 4. Type and submit
            var suffix = $"\n\n(자동 복구 — schedule {item.Id}, {item.Type})";
            promptHelper.TypeAndSubmit(promptInfo, promptText + suffix);

            // 5. Mark done + advance recurring
            ScheduleManager.UpdateStatus(item.Id, "done");
            if (item.Type == "recurring")
                ScheduleManager.AdvanceRecurring(item.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SCHEDULE] Done: {item.Id}");
            Console.ResetColor();

            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":white_check_mark: 스케줄 완료: {item.Id} — 프롬프트 입력 완료");
        }
        catch (Exception ex)
        {
            ScheduleManager.UpdateStatus(item.Id, "failed", ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SCHEDULE] Failed: {item.Id} — {ex.Message}");
            Console.ResetColor();
            ScheduleNotifySlack(slackBotToken, slackChannel,
                $":x: 스케줄 실패 ({item.Id}): {ex.Message}");
        }
    }

    /// <summary>Send Slack notification for schedule events (best-effort, non-blocking).</summary>
    private static void ScheduleNotifySlack(string? botToken, string? channel, string message)
    {
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
        try
        {
            Task.Run(async () => await SlackSendViaApi(botToken!, channel!, message, username: BotUsername)).Wait(5000);
        }
        catch { /* best-effort */ }
    }
}
