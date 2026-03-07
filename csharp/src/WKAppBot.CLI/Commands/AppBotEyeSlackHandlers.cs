// AppBotEyeSlackHandlers.cs — Shared Slack event handlers + schedule executor for AppBotEye.
// Used by EyeGlobalPollingLoop (GlobalMode) — the only Eye mode.
// Kept as separate partial class file for readability (SetupSlackEventHandlers is ~440 lines).

using System.Text.Json.Nodes;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>현재 실행 중인 exe 경로 (설치 위치 무관).</summary>
    static readonly string ExePath = (Environment.ProcessPath ?? "wkappbot").Replace('\\', '/');

    /// <summary>
    /// Build the "(Slack ... — wkappbot slack reply ...)" suffix appended to every forwarded prompt.
    /// ONE place to change the format — never duplicate this string elsewhere!
    /// </summary>
    static string SlackReplySuffix(string user, string replyTs, string? label = null)
    {
        var tag = string.IsNullOrEmpty(label) ? $"Slack @{user}" : $"Slack {label} @{user}";
        return $"({tag} → \"{ExePath}\" slack reply \"MUST reply your answer here\" --msg {replyTs})";
    }

    /// <summary>채널 브로드캐스트용 suffix — reply 대신 send (채널에 답장).</summary>
    static string SlackSendSuffix(string user)
    {
        return $"(Slack @{user} → \"{ExePath}\" slack send \"퐁~! 지금 뭐뭐 완료하고 머머 하고있습니다~!\")";
    }

    /// <summary>
    /// CWD 기반으로 "내 창" 프롬프트를 정확히 찾는다.
    /// FindAllPrompts → CWD 폴더명이 윈도우 타이틀에 포함된 창 우선 → fallback: FindPrompt.
    /// 동료 창이 전경에 있어도 정확한 창을 찾아 전달.
    /// </summary>
    static ClaudePromptHelper.PromptInfo? FindMyPrompt(ClaudePromptHelper promptHelper)
    {
        var cwdFolder = Path.GetFileName(Environment.CurrentDirectory); // "WKAppBot"
        var allPrompts = promptHelper.FindAllPrompts();

        if (allPrompts.Count == 0)
        {
            Console.WriteLine("[EYE][SLACK] FindMyPrompt: no prompts found at all");
            return promptHelper.FindPrompt(); // last resort
        }

        if (allPrompts.Count == 1)
            return allPrompts[0];

        // CWD 폴더명으로 윈도우 타이틀 매칭
        var myPrompt = allPrompts.FirstOrDefault(p =>
            p.WindowTitle.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase));

        if (myPrompt != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] FindMyPrompt: CWD match \"{cwdFolder}\" → \"{(myPrompt.WindowTitle.Length > 50 ? myPrompt.WindowTitle[..47] + "..." : myPrompt.WindowTitle)}\" (out of {allPrompts.Count})");
            Console.ResetColor();
            return myPrompt;
        }

        // CWD 매칭 실패 → 첫 번째 반환
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[EYE][SLACK] FindMyPrompt: no CWD match for \"{cwdFolder}\" in {allPrompts.Count} prompts, using first");
        Console.ResetColor();
        return allPrompts[0];
    }

    /// <summary>
    /// 쓰레드에 응답한 클롣 대화명을 조회 → 해당 프롬프트들만 반환.
    /// 쓰레드 메시지의 bot username ("클롣 [WKAppBot]") → 대괄호 안 폴더명 추출 → 프롬프트 매칭.
    /// 매칭 결과가 없으면 전체 프롬프트 반환 (fallback).
    /// </summary>
    static List<ClaudePromptHelper.PromptInfo> FindPromptsForThread(
        ClaudePromptHelper promptHelper, string botToken, string channel, string threadTs)
    {
        var allPrompts = promptHelper.FindAllPrompts();
        if (allPrompts.Count <= 1)
            return allPrompts;

        // 쓰레드 전체 조회 → 봇 대화명(username) 수집
        var botUsernames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // 시작 메시지(1) + 최근 7개 = 충분히 참가 클롣 파악
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=8&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = System.Text.Json.Nodes.JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() == true)
            {
                foreach (var m in json["messages"]?.AsArray() ?? new System.Text.Json.Nodes.JsonArray())
                {
                    var uname = m?["username"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(uname))
                        botUsernames.Add(uname);
                }
            }
        }
        catch { }

        if (botUsernames.Count == 0)
        {
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no bot usernames found, sending to all");
            return allPrompts;
        }

        // "클롣 [WKAppBot]" → "WKAppBot" 추출
        var cwdNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var uname in botUsernames)
        {
            var start = uname.IndexOf('[');
            var end = uname.IndexOf(']');
            if (start >= 0 && end > start)
                cwdNames.Add(uname[(start + 1)..end]);
        }

        // 프롬프트 윈도우 타이틀에서 매칭
        var matched = new List<ClaudePromptHelper.PromptInfo>();
        foreach (var p in allPrompts)
        {
            foreach (var cwd in cwdNames)
            {
                if (p.WindowTitle.Contains(cwd, StringComparison.OrdinalIgnoreCase))
                {
                    matched.Add(p);
                    break;
                }
            }
        }

        if (matched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: {matched.Count}/{allPrompts.Count} prompts matched (usernames: {string.Join(", ", botUsernames)})");
            Console.ResetColor();
            return matched;
        }

        Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no prompt match for usernames [{string.Join(", ", botUsernames)}], sending to all");
        return allPrompts;
    }

    /// <summary>
    /// 위치확보 후 프롬프트 입력. 모든 자동입력 전에 Probe 호출.
    /// 슬랙 요청 = AutoApproveYield (승인만 자동, 돋보기+확보는 정상).
    /// 최소화 창 → SW_SHOWNOACTIVATE 포커스리스 리스토어 후 재시도.
    /// </summary>
    static bool ProbeAndSubmit(ClaudePromptHelper promptHelper, ClaudePromptHelper.PromptInfo prompt, string text)
    {
        var shortTitle = prompt.WindowTitle.Length > 40
            ? prompt.WindowTitle[..37] + "..." : prompt.WindowTitle;
        Console.WriteLine($"  [SLACK→PROMPT] 위치확보 시작: 0x{prompt.WindowHandle:X} \"{shortTitle}\"");

        var readiness = CreateInputReadiness();
        var report = readiness.Probe(new InputReadinessRequest
        {
            TargetHwnd = prompt.WindowHandle,
            IntendedAction = "key", // 키보드 입력 → 항상 포커스 필요
            AutoApproveYield = true, // 슬랙 요청 → 양보 자동승인
            SkipKnowhow = true, // 프롬프트는 노하우 불필요
        });

        // ── 최소화 감지 → 포커스리스 리스토어 후 재 Probe ──
        if (report.FormIconic)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [SLACK→PROMPT] 최소화 감지 → 포커스리스 리스토어 시도");
            Console.ResetColor();
            report.Zoom?.Dispose();

            // SW_SHOWNOACTIVATE: 포커스 안 뺏고 원래 위치에 복원
            var wp = new NativeMethods.WINDOWPLACEMENT();
            wp.length = System.Runtime.InteropServices.Marshal.SizeOf(wp);
            if (NativeMethods.GetWindowPlacement(prompt.WindowHandle, ref wp))
            {
                wp.showCmd = NativeMethods.SW_SHOWNOACTIVATE;
                NativeMethods.SetWindowPlacement(prompt.WindowHandle, ref wp);
                Console.WriteLine($"  [SLACK→PROMPT] 포커스리스 리스토어 완료 (SW_SHOWNOACTIVATE)");
                Thread.Sleep(300); // UI 갱신 대기

                // 재 Probe
                report = readiness.Probe(new InputReadinessRequest
                {
                    TargetHwnd = prompt.WindowHandle,
                    IntendedAction = "key",
                    AutoApproveYield = true,
                    SkipKnowhow = true,
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [SLACK→PROMPT] GetWindowPlacement 실패");
                Console.ResetColor();
                return false;
            }
        }

        // ── 위치확보 결과 요약 ──
        var issues = new List<string>();
        if (report.ActiveBlocker != null)
            issues.Add($"blocker={report.ActiveBlocker.Title}");
        if (report.ElevationMismatch)
            issues.Add("elevation-mismatch");
        if (!report.FormVisible)
            issues.Add("not-visible");
        if (!report.FormEnabled)
            issues.Add("not-enabled");
        if (report.FormIconic)
            issues.Add("still-minimized");
        if (report.UserYieldRequested && !report.UserYieldConfirmed)
            issues.Add("yield-denied");
        if (report.UserYieldConfirmed && !report.UserYieldFocusAcquired)
            issues.Add("focus-acquire-failed");

        if (issues.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [SLACK→PROMPT] 위치확보 실패: {string.Join(", ", issues)}");
            Console.ResetColor();
            report.Zoom?.Dispose();
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  [SLACK→PROMPT] 위치확보 OK — 입력 시작");
        Console.ResetColor();

        // 입력 전 전경 기억 — TypeAndSubmit이 포커스를 뺏으면 복구용
        var prevFg = NativeMethods.GetForegroundWindow();

        var result = promptHelper.TypeAndSubmit(prompt, text);
        report.Zoom?.Dispose();

        // 포커스가 바뀌었으면 직전 전경으로 복구
        if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle
            && NativeMethods.GetForegroundWindow() != prevFg)
        {
            Thread.Sleep(200);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine($"  [SLACK→PROMPT] 직전 전경 복구: 0x{prevFg:X}");
        }

        return result;
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
        const string AckUsername = "클롣아이";
        void SendAndTrackAck(string ch, string threadKey, int promptCount = 1, int totalAttempted = 0)
        {
            string ackText;
            if (totalAttempted > 0 && promptCount < totalAttempted)
                ackText = $":warning: Claude {promptCount}/{totalAttempted}곳에 전달 (일부 실패)";
            else if (promptCount > 1)
                ackText = $"Claude {promptCount}곳에 전달했습니다!";
            else
                ackText = "Claude에 전달했습니다!";
            var (ackOk, ackTs) = Task.Run(async () =>
                await SlackSendViaApi(botToken, ch, ackText, threadKey, username: AckUsername))
                .GetAwaiter().GetResult();
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

            // ── Normal @mention: broadcast to ALL prompts (멘션=폴더구분 없음) ──
            ClaudePromptHelper.AllowFocusSteal = true; // fallback path용
            var promptHelper = new ClaudePromptHelper();
            var allMentionPrompts = promptHelper.FindAllPrompts();
            if (allMentionPrompts.Count > 0)
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
                int mentionSent = 0;
                foreach (var pi in allMentionPrompts)
                {
                    try
                    {
                        var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, replyThread)}";
                        if (ProbeAndSubmit(promptHelper, pi, promptText))
                            mentionSent++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[EYE][SLACK] Mention broadcast error: {ex.Message}");
                    }
                }
                handledByMention.Add(msg.Timestamp); // dedup: OnMessage won't re-forward
                Console.WriteLine($"[EYE][SLACK] >> Mention broadcast: {mentionSent}/{allMentionPrompts.Count} prompts");

                DeletePendingAck(ackThread);
                SendAndTrackAck(msg.Channel, ackThread, mentionSent, allMentionPrompts.Count);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[EYE][SLACK] Prompt not found! 새 채팅 폴백 시도...");
                Console.ResetColor();

                var ackThread = msg.ThreadTs ?? msg.Timestamp;

                // ★ New-chat fallback: if Claude Desktop window exists, try click+paste
                if (claudeHwnd != IntPtr.Zero)
                {
                    var replyThread = msg.ThreadTs ?? msg.Timestamp;
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx)) threadContext = $"\n{ctx}\n";
                    }
                    var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, replyThread)}";

                    if (promptHelper.TryNewChatInput(claudeHwnd, promptText))
                    {
                        Console.WriteLine("[EYE][SLACK] >> New-chat fallback SUCCESS!");
                        handledByMention.Add(msg.Timestamp);
                        DeletePendingAck(ackThread);
                        SendAndTrackAck(msg.Channel, ackThread);
                        return; // skip diagnosis
                    }
                    Console.WriteLine("[EYE][SLACK] New-chat fallback FAILED, running diagnosis...");
                }

                try
                {
                    // Comprehensive diagnosis
                    var claudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                    var statusInfo = claudeStatus != null
                        ? $"Claude 상태: {claudeStatus.Item2}"
                        : "Claude 상태: 감지 불가";

                    string diagDetail;
                    try { diagDetail = promptHelper.DiagnosePromptSearch(); }
                    catch (Exception dex) { diagDetail = $"(진단 실패: {dex.Message})"; }

                    Console.WriteLine(diagDetail);
                    try
                    {
                        var diagPath = Path.Combine(DataDir, "logs", $"prompt_diag_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                        File.WriteAllText(diagPath, $"{statusInfo}\n{diagDetail}\n받은 메시지: {cleanText}");
                        Console.WriteLine($"[EYE] Diagnosis saved: {diagPath}");
                    }
                    catch { }

                    var slackMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                        $"{statusInfo}\n" +
                        $"받은 메시지: {cleanText}\n" +
                        $"```\n{(diagDetail.Length > 800 ? diagDetail.Substring(0, 800) + "\n...(truncated)" : diagDetail)}\n```";
                    Task.Run(async () => await Send(msg.Channel, slackMsg, ackThread)).Wait(5000);
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

                ClaudePromptHelper.AllowFocusSteal = true; // fallback path용
                var trPromptHelper = new ClaudePromptHelper();
                var allTrPrompts = FindPromptsForThread(trPromptHelper, botToken, msg.Channel, msg.ThreadTs!);
                if (allTrPrompts.Count > 0)
                {
                    // Build thread context (starter + previous message) for Claude
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n{ctx}\n";
                    }

                    int trSent = 0;
                    foreach (var pi in allTrPrompts)
                    {
                        try
                        {
                            var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, msg.ThreadTs!, "thread reply")}";
                            if (ProbeAndSubmit(trPromptHelper, pi, promptText))
                                trSent++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[EYE][SLACK] Thread broadcast error: {ex.Message}");
                        }
                    }
                    Console.WriteLine($"[EYE][SLACK] >> Thread broadcast: {trSent}/{allTrPrompts.Count} prompts");

                    DeletePendingAck(msg.ThreadTs!);
                    SendAndTrackAck(msg.Channel, msg.ThreadTs!, trSent, allTrPrompts.Count);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Thread reply: Prompt not found! 새 채팅 폴백 시도...");
                    Console.ResetColor();

                    // ★ New-chat fallback
                    if (claudeHwnd != IntPtr.Zero)
                    {
                        var threadContext = "";
                        if (msg.ThreadTs != null)
                        {
                            var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                            if (!string.IsNullOrEmpty(ctx)) threadContext = $"\n{ctx}\n";
                        }
                        var fallbackText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, msg.ThreadTs!, "thread reply")}";
                        if (trPromptHelper.TryNewChatInput(claudeHwnd, fallbackText))
                        {
                            Console.WriteLine("[EYE][SLACK] >> Thread reply: New-chat fallback SUCCESS!");
                            DeletePendingAck(msg.ThreadTs!);
                            SendAndTrackAck(msg.Channel, msg.ThreadTs!);
                            return;
                        }
                        Console.WriteLine("[EYE][SLACK] New-chat fallback FAILED, running diagnosis...");
                    }

                    try
                    {
                        var trClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var trStatusInfo = trClaudeStatus != null
                            ? $"Claude 상태: {trClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";

                        string trDiagDetail;
                        try { trDiagDetail = trPromptHelper.DiagnosePromptSearch(); }
                        catch (Exception dex) { trDiagDetail = $"(진단 실패: {dex.Message})"; }

                        Console.WriteLine(trDiagDetail);
                        try
                        {
                            var diagPath = Path.Combine(DataDir, "logs", $"prompt_diag_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                            File.WriteAllText(diagPath, $"{trStatusInfo}\n{trDiagDetail}\n스레드 답장: {cleanText}");
                            Console.WriteLine($"[EYE] Diagnosis saved: {diagPath}");
                        }
                        catch { }

                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{trStatusInfo}\n" +
                            $"스레드 답장: {cleanText}\n" +
                            $"```\n{(trDiagDetail.Length > 800 ? trDiagDetail.Substring(0, 800) + "\n...(truncated)" : trDiagDetail)}\n```";
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

                ClaudePromptHelper.AllowFocusSteal = true; // fallback path용
                var kwPromptHelper = new ClaudePromptHelper();
                var allKwPrompts = kwPromptHelper.FindAllPrompts();
                if (allKwPrompts.Count > 0)
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
                    int kwSent = 0;
                    foreach (var pi in allKwPrompts)
                    {
                        try
                        {
                            var promptText = $"{cleanKwText}{threadContext}\n{SlackReplySuffix(msg.User, kwReplyThread, $"keyword:\"{matchedKw}\"")}";
                            if (ProbeAndSubmit(kwPromptHelper, pi, promptText))
                                kwSent++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[EYE][SLACK] Keyword broadcast error: {ex.Message}");
                        }
                    }
                    Console.WriteLine($"[EYE][SLACK] >> Keyword broadcast: {kwSent}/{allKwPrompts.Count} prompts");

                    DeletePendingAck(kwThreadKey);
                    SendAndTrackAck(msg.Channel, kwThreadKey, kwSent, allKwPrompts.Count);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Keyword match: Prompt not found! 새 채팅 폴백 시도...");
                    Console.ResetColor();

                    // ★ New-chat fallback
                    if (claudeHwnd != IntPtr.Zero)
                    {
                        var kwFbThread = msg.ThreadTs ?? msg.Timestamp;
                        var kwFbContext = "";
                        if (msg.ThreadTs != null)
                        {
                            var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                            if (!string.IsNullOrEmpty(ctx)) kwFbContext = $"\n{ctx}\n";
                        }
                        var kwFallbackText = $"{cleanKwText}{kwFbContext}\n{SlackReplySuffix(msg.User, kwFbThread, $"keyword:\"{matchedKw}\"")}";
                        if (kwPromptHelper.TryNewChatInput(claudeHwnd, kwFallbackText))
                        {
                            Console.WriteLine("[EYE][SLACK] >> Keyword match: New-chat fallback SUCCESS!");
                            DeletePendingAck(kwThreadKey);
                            SendAndTrackAck(msg.Channel, kwThreadKey);
                            return;
                        }
                        Console.WriteLine("[EYE][SLACK] New-chat fallback FAILED, running diagnosis...");
                    }

                    try
                    {
                        var kwClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var kwStatusInfo = kwClaudeStatus != null
                            ? $"Claude 상태: {kwClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";

                        string kwDiagDetail;
                        try { kwDiagDetail = kwPromptHelper.DiagnosePromptSearch(); }
                        catch (Exception dex) { kwDiagDetail = $"(진단 실패: {dex.Message})"; }

                        Console.WriteLine(kwDiagDetail);
                        try
                        {
                            var diagPath = Path.Combine(DataDir, "logs", $"prompt_diag_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                            File.WriteAllText(diagPath, $"{kwStatusInfo}\n{kwDiagDetail}\n키워드 \"{matchedKw}\" 감지: {cleanKwText}");
                            Console.WriteLine($"[EYE] Diagnosis saved: {diagPath}");
                        }
                        catch { }

                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{kwStatusInfo}\n" +
                            $"키워드 \"{matchedKw}\" 감지: {cleanKwText}\n" +
                            $"```\n{(kwDiagDetail.Length > 800 ? kwDiagDetail.Substring(0, 800) + "\n...(truncated)" : kwDiagDetail)}\n```";
                        Task.Run(async () => await Send(msg.Channel, diagMsg, kwThreadKey)).Wait(5000);
                    }
                    catch
                    {
                        Task.Run(async () => await Send(msg.Channel,
                            $"(Claude 프롬프트 없음) 키워드 \"{matchedKw}\" 감지: {cleanKwText}", kwThreadKey)).Wait(5000);
                    }
                }
                return;
            }

            // ── Catch-all: ALL remaining messages → BROADCAST to ALL Claude prompts ──
            // CLAUDE.md: "Slack 수신 메시지는 항상 Claude 프롬프트에 전달 (옵션 없음)"
            // 핑 같은 브로드캐스트: 모든 클롣에게 전달 → 각자 퐁 응답
            {
                var cleanAll = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrEmpty(cleanAll)) return;

                // Noise filter: very short noise tokens
                var noise = new[] { "NO_REPLY", "ㄱㄱ" };
                if (noise.Any(n => cleanAll.Equals(n, StringComparison.OrdinalIgnoreCase))) return;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"[EYE][SLACK] << catch-all BROADCAST from {msg.User}: {cleanAll}");
                Console.ResetColor();

                // Track thread for follow-up
                var allThreadKey = msg.ThreadTs ?? msg.Timestamp;
                activeThreads.Add(allThreadKey);

                ClaudePromptHelper.AllowFocusSteal = true; // fallback path용
                using var allPromptHelper = new ClaudePromptHelper();
                var allPrompts = allPromptHelper.FindAllPrompts();

                if (allPrompts.Count > 0)
                {
                    int sent = 0;
                    foreach (var pi in allPrompts)
                    {
                        try
                        {
                            var promptText = $"{cleanAll}\n{SlackSendSuffix(msg.User)}";
                            if (ProbeAndSubmit(allPromptHelper, pi, promptText))
                            {
                                sent++;
                                Console.WriteLine($"[EYE][SLACK] >> Broadcast [{sent}/{allPrompts.Count}]: sent to \"{(pi.WindowTitle.Length > 40 ? pi.WindowTitle[..37] + "..." : pi.WindowTitle)}\"");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[EYE][SLACK] >> Broadcast error: {ex.Message}");
                        }
                    }

                    if (sent > 0)
                    {
                        DeletePendingAck(allThreadKey);
                        SendAndTrackAck(msg.Channel, allThreadKey, sent, allPrompts.Count);
                        Console.WriteLine($"[EYE][SLACK] >> Broadcast complete: {sent}/{allPrompts.Count} prompts");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Catch-all: No prompts found!");
                    Console.ResetColor();

                    Task.Run(async () => await Send(msg.Channel,
                        $":warning: Claude 프롬프트를 찾을 수 없습니다 (메시지: {cleanAll})", allThreadKey)).Wait(5000);
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
            var promptInfo = FindMyPrompt(promptHelper);
            if (promptInfo == null)
            {
                // Retry once after 3 seconds (Claude may still be loading)
                Thread.Sleep(3000);
                promptInfo = FindMyPrompt(promptHelper);
            }

            if (promptInfo == null)
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Claude prompt not found");
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":x: 스케줄 실패 ({item.Id}): Claude 프롬프트를 찾을 수 없습니다");
                return;
            }

            // 4. 위치확보 + Type and submit
            ClaudePromptHelper.AllowFocusSteal = true; // schedule = auto request
            var suffix = $"\n\n(자동 복구 — schedule {item.Id}, {item.Type})";
            ProbeAndSubmit(promptHelper, promptInfo, promptText + suffix);

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
