using System.Diagnostics;
using System.Text.Json.Nodes;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// EyeTick one-shot: check Slack for new messages via conversations.history API
    /// and forward them to Claude prompt. Uses slack_last_ts.txt to track position.
    /// Also checks slack_inbox.jsonl as fallback (from standalone slack listen).
    /// </summary>
    static void EyeTickForwardSlackInbox()
    {
        try
        {
            var swSlack = Stopwatch.StartNew();

            // ── Slack Step 1: Load config ──
            var swStep = Stopwatch.StartNew();
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[EYE_TICK] slack=no_config");
                Console.Out.Flush();
                return;
            }

            var json = JsonNode.Parse(File.ReadAllText(configPath));
            var botToken = json?["bot_token"]?.GetValue<string>();
            var channel = json?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
            {
                Console.WriteLine("[EYE_TICK] slack=missing_token_or_channel");
                Console.Out.Flush();
                return;
            }
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] LoadConfig={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 2: auth.test (get bot user ID) — HTTP call ──
            swStep.Restart();
            var botUserId = SlackGetBotUserId(botToken);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] auth.test={swStep.ElapsedMilliseconds}ms (botUserId={botUserId ?? "null"})");
            Console.Out.Flush();

            // ── Slack Step 3: LoadLastTs ──
            swStep.Restart();
            var lastTs = LoadLastTs(channel);
            var lastTsDouble = 0.0;
            if (!string.IsNullOrEmpty(lastTs))
                double.TryParse(lastTs, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out lastTsDouble);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] LoadLastTs={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 4: conversations.history — HTTP call ──
            swStep.Restart();
            var messages = SlackFetchHistoryAsync(botToken, channel, limit: 20)
                .GetAwaiter().GetResult();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] conversations.history={swStep.ElapsedMilliseconds}ms (msgs={messages.Count})");
            Console.Out.Flush();

            if (messages.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                Console.Out.Flush();
                return;
            }

            // ── Slack Step 5: Screen reader broadcast + UIA init ──
            swStep.Restart();
            WKAppBot.Win32.Native.ScreenReaderMode.Enable();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] ScreenReaderBroadcast={swStep.ElapsedMilliseconds}ms (enabled={WKAppBot.Win32.Native.ScreenReaderMode.IsEnabled})");
            Console.Out.Flush();

            swStep.Restart();
            using var helper = new ClaudePromptHelper();
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] UIA3Automation_Init={swStep.ElapsedMilliseconds}ms");
            Console.Out.Flush();

            // ── Slack Step 6: Filter messages ──
            swStep.Restart();
            string? contextLine = null;
            var newMsgs = new List<(string user, string text, string ts)>();
            foreach (var msg in messages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";
                var msgTs = msg["ts"]?.GetValue<string>() ?? "";
                var subtype = msg["subtype"]?.GetValue<string>();

                if (!string.IsNullOrEmpty(subtype)) continue;
                if (string.IsNullOrWhiteSpace(text)) continue;

                double.TryParse(msgTs, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var tsDouble);

                if (msgTs == lastTs)
                {
                    var ctxClean = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                    var who = user == botUserId ? "클롣" : $"@{user}";
                    contextLine = $"[직전 대화] {who}: {ctxClean}";
                    continue;
                }

                if (lastTsDouble > 0 && tsDouble <= lastTsDouble) continue;
                if (user == botUserId) continue;

                newMsgs.Add((user, text, msgTs));
            }
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] FilterMessages={swStep.ElapsedMilliseconds}ms (new={newMsgs.Count})");
            Console.Out.Flush();

            if (newMsgs.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                // Still check thread replies even if no new channel messages
                swStep.Restart();
                EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] ThreadReplies={swStep.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                return;
            }

            newMsgs.Reverse();
            Console.WriteLine($"[EYE_TICK] slack={newMsgs.Count} new message(s) — forwarding to AI prompt...");
            Console.Out.Flush();

            // ── Slack Step 7: FindPrompt (UIA tree walk) ──
            swStep.Restart();
            var promptInfo = FindSlackPreferredPrompt(helper);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] FindPrompt={swStep.ElapsedMilliseconds}ms (found={promptInfo != null}, host={promptInfo?.HostType ?? "n/a"})");
            Console.Out.Flush();
            if (promptInfo == null)
            {
                Console.WriteLine("[EYE_TICK] WARNING: AI prompt not found — will retry next tick");
                Console.Out.Flush();
                return;
            }

            // ── Slack Step 8: Forward messages ──
            int forwarded = 0;
            string? latestTs = null;
            foreach (var (user, text, msgTs) in newMsgs)
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrWhiteSpace(cleanText)) continue;

                var contextPrefix = (contextLine != null && forwarded == 0) ? $"{contextLine}\n\n" : "";
                var promptText = $"{contextPrefix}{cleanText}\n\n{SlackReplySuffix(user, msgTs)}";

                swStep.Restart();
                var fresh = FindSlackPreferredPrompt(helper);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] Re-FindPrompt={swStep.ElapsedMilliseconds}ms");
                Console.Out.Flush();
                if (fresh == null)
                {
                    Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping forward");
                    Console.Out.Flush();
                    break;
                }

                Console.WriteLine($"[EYE_TICK] [FORWARD] Slack @{user} → {fresh.HostType} prompt");
                Console.Out.Flush();
                swStep.Restart();
                var ok = helper.TypeAndSubmit(fresh, promptText);
                swStep.Stop();
                Console.WriteLine($"[PROF:SLACK] TypeAndSubmit={swStep.ElapsedMilliseconds}ms (ok={ok})");
                Console.Out.Flush();
                if (ok)
                {
                    forwarded++;
                    latestTs = msgTs;
                    Console.WriteLine($"[EYE_TICK] [DELIVERED] Slack @{user}: {cleanText}");

                    try
                    {
                        swStep.Restart();
                        var ackText = $"전달했습니다! (thread={msgTs})";
                        var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, msgTs, username: "앱봇아이")
                            .GetAwaiter().GetResult();
                        swStep.Stop();
                        Console.WriteLine($"[PROF:SLACK] AckSend={swStep.ElapsedMilliseconds}ms (ok={ackOk})");
                        Console.Out.Flush();
                        if (ackOk && ackTs != null)
                            SavePendingAck(msgTs, channel, ackTs);
                    }
                    catch { }

                    if (newMsgs.Count > 1)
                        Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine($"[EYE_TICK] WARNING: TypeAndSubmit failed for @{user}");
                    Console.Out.Flush();
                }
            }

            if (latestTs != null)
            {
                SaveLastTs(channel, latestTs);
                Console.WriteLine($"[EYE_TICK] slack forwarded={forwarded}/{newMsgs.Count} — lastTs updated");
            }

            // ── Thread reply detection ──
            swStep.Restart();
            EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
            swStep.Stop();
            Console.WriteLine($"[PROF:SLACK] ThreadReplies={swStep.ElapsedMilliseconds}ms");
            Console.WriteLine($"[PROF:SLACK] SlackTotal={swSlack.ElapsedMilliseconds}ms");
            Console.Out.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] slack error: {ex.Message}");
            Console.Out.Flush();
        }
    }

    /// <summary>
    /// Check recent bot messages for new thread replies from users.
    /// Responds when: (1) parent is from bot (클롣) → any user reply, or (2) @mention in reply.
    /// </summary>
    static void EyeTickCheckThreadReplies(List<JsonNode> channelMessages,
        string botToken, string channel, string? botUserId, ClaudePromptHelper helper)
    {
        try
        {
            // Find bot messages with replies (max 5 threads to check)
            var botThreads = new List<(string threadTs, string parentText)>();
            foreach (var msg in channelMessages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var botId = msg["bot_id"]?.GetValue<string>();
                var subtype = msg["subtype"]?.GetValue<string>();
                var replyCount = msg["reply_count"]?.GetValue<int>() ?? 0;
                var ts = msg["ts"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";

                if (replyCount <= 0) continue;

                // Skip threads older than 1 hour — prevents old message flood on Eye restart
                if (!string.IsNullOrEmpty(ts))
                {
                    if (double.TryParse(ts, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var tsEpoch))
                    {
                        var ageSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - tsEpoch;
                        if (ageSeconds > 3600)
                        {
                            // Silently skip — old thread, no need to scan replies
                            continue;
                        }
                    }
                }

                // Is this a bot message? (either user==botUserId or has bot_id or subtype==bot_message)
                bool isBotMsg = user == botUserId
                    || !string.IsNullOrEmpty(botId)
                    || subtype == "bot_message";
                if (!isBotMsg) continue;

                botThreads.Add((ts, text));
                if (botThreads.Count >= 5) break;
            }

            if (botThreads.Count == 0) return;

            int threadReplies = 0;
            foreach (var (threadTs, parentText) in botThreads)
            {
                // Load per-thread last reply ts
                var threadKey = $"{channel}_t_{threadTs}";
                var lastReplyTs = LoadLastTs(threadKey);
                double lastReplyTsDouble = 0;
                if (!string.IsNullOrEmpty(lastReplyTs))
                    double.TryParse(lastReplyTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out lastReplyTsDouble);

                // Fetch thread replies
                var replies = SlackFetchRepliesAsync(botToken, channel, threadTs, limit: 10)
                    .GetAwaiter().GetResult();

                string? latestReplyTs = null;
                foreach (var reply in replies)
                {
                    var rTs = reply["ts"]?.GetValue<string>() ?? "";
                    var rUser = reply["user"]?.GetValue<string>() ?? "";
                    var rText = reply["text"]?.GetValue<string>() ?? "";
                    var rBotId = reply["bot_id"]?.GetValue<string>();
                    var rSubtype = reply["subtype"]?.GetValue<string>();

                    // Skip parent message itself
                    if (rTs == threadTs) continue;
                    // Skip bot's own replies
                    if (rUser == botUserId || !string.IsNullOrEmpty(rBotId) || rSubtype == "bot_message") continue;
                    // Skip empty
                    if (string.IsNullOrWhiteSpace(rText)) continue;

                    // Skip already processed
                    double.TryParse(rTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var rTsDouble);
                    if (lastReplyTsDouble > 0 && rTsDouble <= lastReplyTsDouble) continue;

                    // Check: @mention tag → always respond
                    bool hasMention = !string.IsNullOrEmpty(botUserId) && rText.Contains($"<@{botUserId}>");

                    // For non-mention replies, parent must be from bot (already filtered above)
                    // Both cases pass — bot thread reply OR @mention

                    // Clean text
                    var cleanReply = System.Text.RegularExpressions.Regex.Replace(rText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (string.IsNullOrWhiteSpace(cleanReply)) continue;

                    // Build context: bot's original message (truncated)
                    var cleanParent = System.Text.RegularExpressions.Regex.Replace(parentText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (cleanParent.Length > 80) cleanParent = cleanParent[..80] + "…";

                    var promptText = $"[쓰레드 시작] {cleanParent}\n\n{cleanReply}\n\n{SlackReplySuffix(rUser, threadTs, "thread reply")}";

                    var fresh = FindSlackPreferredPrompt(helper);
                    if (fresh == null)
                    {
                        Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping thread reply forward");
                        return;
                    }

                    Console.WriteLine($"[EYE_TICK] [FORWARD] Thread @{rUser} → {fresh.HostType} prompt");
                    var ok = helper.TypeAndSubmit(fresh, promptText);
                    if (ok)
                    {
                        threadReplies++;
                        latestReplyTs = rTs;
                        Console.WriteLine($"[EYE_TICK] [DELIVERED] Thread @{rUser}: {cleanReply}");

                        // Send "전달했습니다" ack — deleted when slack reply is sent
                        try
                        {
                            var ackText = $"전달했습니다! (thread={threadTs})";
                            var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, threadTs, username: "앱봇아이")
                                .GetAwaiter().GetResult();
                            if (ackOk && ackTs != null)
                                SavePendingAck(threadTs, channel, ackTs);
                        }
                        catch { }

                        Thread.Sleep(2000);
                    }
                }

                // Update per-thread last reply ts
                if (latestReplyTs != null)
                    SaveLastTs(threadKey, latestReplyTs);
            }

            if (threadReplies > 0)
                Console.WriteLine($"[EYE_TICK] thread_replies={threadReplies} forwarded");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] thread check error: {ex.Message}");
        }
    }
}
