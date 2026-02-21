using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: Slack WebBot monitor, Claude busy detection, prompt lost handler
internal partial class Program
{
    /// <summary>
    /// When Claude prompt is lost (FindPrompt returns null), capture foreground window screenshot
    /// and send it to Slack so the user can see what's blocking the prompt (e.g. permission dialog).
    /// Also writes the original message to inbox as fallback.
    /// </summary>
    static void HandlePromptLost(string botToken, string channel, string threadTs,
        string user, string cleanText, string ts)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[SLACK] >> Prompt lost! Claude 프롬프트를 찾을 수 없습니다.");
        Console.WriteLine("[SLACK]    가능한 원인: 계획승인 중 / 권한 묻는 창 / Claude 비활성");
        Console.WriteLine("[SLACK]    대응: 전경 윈도우 스냅샷 캡처 후 Slack에 전송합니다.");
        Console.ResetColor();

        // Take UIA snapshot for diagnosis
        try
        {
            Console.WriteLine("[SLACK]    Diagnosing foreground window...");
        }
        catch { }

        try
        {
            var fgWindow = WKAppBot.Win32.Native.NativeMethods.GetForegroundWindow();
            if (fgWindow != IntPtr.Zero)
            {
                // Get foreground window info
                var fgTitle = WKAppBot.Win32.Window.WindowFinder.GetWindowText(fgWindow);
                var fgClass = WKAppBot.Win32.Window.WindowFinder.GetClassName(fgWindow);
                Console.WriteLine($"[SLACK]    Foreground: \"{fgTitle}\" (class={fgClass})");

                // Capture foreground window screenshot
                var outputDir = Path.Combine(DataDir, "output", "screenshots");
                Directory.CreateDirectory(outputDir);
                var screenshotPath = Path.Combine(outputDir,
                    $"prompt_blocked_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(fgWindow);
                if (bmp != null && !WKAppBot.Win32.Input.ScreenCapture.IsBlankBitmap(bmp))
                {
                    bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                    Console.WriteLine($"[SLACK]    Screenshot: {screenshotPath}");

                    // Upload to Slack thread with diagnostic info
                    var comment = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";

                    SlackUploadFileAsync(botToken, channel, screenshotPath,
                        $"Blocked: {fgTitle}", threadTs, comment).GetAwaiter().GetResult();
                }
                else
                {
                    bmp?.Dispose();
                    // Screenshot failed, send text message instead
                    var msg = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";
                    SlackSendViaApi(botToken, channel, msg, threadTs).GetAwaiter().GetResult();
                }
            }
            else
            {
                // No foreground window — just notify
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! 원래 메시지: {cleanText}",
                    threadTs).GetAwaiter().GetResult();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK]    Diagnosis failed: {ex.Message}");
            // Fallback: text notification
            try
            {
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! (진단 실패: {ex.Message})\n원래 메시지: {cleanText}",
                    threadTs).GetAwaiter().GetResult();
            }
            catch { }
        }

        // Always write to inbox as fallback
        WriteInbox(channel, user, cleanText, ts);
    }

    /// <summary>
    /// Claude Desktop monitor tick — called every ~100ms from the listen loop.
    /// Detects 3 Claude states via UIA: executing, plan_approval_pending, prompt_ready.
    /// Streams status to Slack via chat.postMessage (create) + chat.update (update).
    /// Handles thread reply relocation: when someone replies to the streaming message,
    /// creates a new message for continued streaming and acknowledges in the old thread.
    /// </summary>
    static void WebBotMonitorTick(
        string botToken, string channel,
        ref CdpClient? cdp, ref string? statusTs,
        ref string? lastUrl, ref string? lastContent,
        ref bool claudeBusy, ref int reconnectBackoff,
        ref IntPtr chromeHwnd,
        ClaudePromptHelper? promptHelper,
        ConcurrentQueue<(string user, string text, string threadTs)>? threadReplies = null,
        string? instanceName = null)
    {
        // 0. Handle thread replies to streaming message -> relocate to new message
        if (threadReplies != null && statusTs != null)
        {
            bool relocated = false;
            while (threadReplies.TryDequeue(out var reply))
            {
                if (relocated) continue; // drain queue, only relocate once per tick

                try
                {
                    bool isChannelMsg = reply.text == "__channel_msg__";

                    if (!isChannelMsg)
                    {
                        // 1. Reply in old thread (acknowledge) — only for thread replies
                        var ackMsg = "(이 쓰레드의 스트리밍은 새 메시지로 이동했어요!)";
                        Task.Run(async () => await SlackSendViaApi(botToken, channel, ackMsg, reply.threadTs)).Wait(3000);
                    }

                    // 2. Delete old status message (clean channel) or update if thread reply
                    var localOldTs = statusTs;
                    if (isChannelMsg)
                    {
                        // Channel msg relocation: just delete the old status message for clean look
                        Task.Run(async () => await SlackDeleteMessageAsync(botToken, channel, localOldTs!)).Wait(3000);
                    }
                    else
                    {
                        // Thread reply: update old message with "moved" notice
                        var oldContent = lastContent ?? "Claude 상태";
                        var oldMsg = oldContent + $"\n_:speech_balloon: 댓글 달림 — 새 메시지로 이동_";
                        Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localOldTs!, oldMsg)).Wait(3000);
                    }

                    // 3. Create new message for continued streaming
                    var curStatus = DetectClaudeDesktopStatus(FindClaudeDesktopWindow());
                    var newEmoji = GetStatusEmoji(curStatus?.Item1);
                    var newText = curStatus?.Item2 ?? "대기 중";
                    var newContent = curStatus != null
                        ? $"{newEmoji} *{GetStatusLabel(curStatus.Item1)}* — {newText} ({DateTime.Now:HH:mm})"
                        : $":white_check_mark: *Claude 대기 중* ({DateTime.Now:HH:mm})";

                    var botUser = GetBotUsername(instanceName);
                    var sendTask = Task.Run(async () => await SlackSendViaApi(botToken, channel, newContent, username: botUser));
                    if (sendTask.Wait(5000))
                    {
                        var (ok, newTs) = sendTask.Result;
                        if (ok && newTs != null)
                        {
                            statusTs = newTs;
                            lastContent = newContent;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"[SLACK] Streaming relocated -> new ts={newTs}");
                            Console.ResetColor();
                        }
                    }
                    relocated = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SLACK] Relocation error: {ex.Message}");
                }
            }
        }

        // 1. Detect Claude Desktop status via UIA (3 states)
        var claudeHwnd = FindClaudeDesktopWindow();
        var claudeStatus = DetectClaudeDesktopStatus(claudeHwnd);
        bool nowActive = claudeStatus != null;
        string? statusKey = claudeStatus?.Item1;
        string? statusText = claudeStatus?.Item2;
        string emoji = GetStatusEmoji(statusKey);
        string label = GetStatusLabel(statusKey);

        if (nowActive && !claudeBusy)
        {
            // Transition: idle → active
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SLACK] Claude: {label} — {statusText}");
            Console.ResetColor();

            var displayText = statusText ?? "작업 중...";
            lastContent = $"{emoji} *{label}* — {displayText} ({DateTime.Now:HH:mm})";

            // Create or update status message
            if (statusTs == null)
            {
                var localContent = lastContent; // ref param can't be captured in lambda
                var botUser = GetBotUsername(instanceName);
                var sendTask = Task.Run(async () => await SlackSendViaApi(botToken, channel, localContent, username: botUser));
                if (sendTask.Wait(5000))
                {
                    (bool ok, string? ts) = sendTask.Result;
                    if (ok && ts != null)
                    {
                        statusTs = ts;
                        Console.WriteLine($"[SLACK] Status: {displayText} (ts={ts})");
                    }
                }
            }
            else
            {
                var localTs = statusTs;
                var localContent = lastContent; // ref param can't be captured in lambda
                Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, localContent)).Wait(3000);
                Console.WriteLine($"[SLACK] Status: {displayText}");
            }
        }
        else if (!nowActive && claudeBusy)
        {
            // Transition: active → idle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SLACK] Claude: done");
            Console.ResetColor();

            if (statusTs != null)
            {
                var doneMsg = $":white_check_mark: *Claude 응답 완료* ({DateTime.Now:HH:mm})";
                lastContent = doneMsg;
                var localTs = statusTs;
                Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, doneMsg)).Wait(5000);
            }
        }
        else if (nowActive && claudeBusy)
        {
            // Still active — update only when status text changes
            var newContent = $"{emoji} *{label}* — {statusText ?? "..."} ({DateTime.Now:HH:mm})";
            if (newContent != lastContent)
            {
                lastContent = newContent;
                if (statusTs != null)
                {
                    var localTs = statusTs;
                    Task.Run(async () => await SlackUpdateMessageAsync(botToken, channel, localTs, newContent)).Wait(3000);
                }
                Console.WriteLine($"[SLACK] Status: {statusText}");
            }
        }

        claudeBusy = nowActive;
    }

    /// <summary>Get Slack emoji for Claude status key.</summary>
    static string GetStatusEmoji(string? statusKey) => statusKey switch
    {
        "executing" => ":gear:",
        "plan_approval_pending" => ":clipboard:",
        "prompt_ready" => ":speech_balloon:",
        "rate_limit" => ":warning:",
        _ => ":robot_face:"
    };

    /// <summary>Get human-readable label for Claude status key.</summary>
    static string GetStatusLabel(string? statusKey) => statusKey switch
    {
        "executing" => "Claude 작업 중",
        "plan_approval_pending" => "Claude 계획승인 대기",
        "prompt_ready" => "Claude 프롬프트 대기",
        "rate_limit" => "Claude 한도 초과",
        _ => "Claude"
    };

    /// <summary>Build Slack username override from instance name. null = use default bot name.
    /// Requires chat:write.customize scope — Slack silently ignores if scope missing.
    /// When scope works: Slack shows "클봇 [WKAppBot]" as sender name (clean, no text prefix needed).
    /// When scope missing: username is ignored, but text prefix still shows instance name.</summary>
    static string? GetBotUsername(string? instanceName) =>
        instanceName != null ? $"클봇 [{instanceName}]" : null;

    /// <summary>Find Chrome main window handle by PID.</summary>
    static IntPtr FindChromeHwndByPid(int pid)
    {
        if (pid <= 0) return IntPtr.Zero;

        IntPtr found = IntPtr.Zero;
        var sb = new System.Text.StringBuilder(256);
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint wpid);
            if ((int)wpid != pid) return true;

            // Check for WS_CAPTION (main window, not popup)
            var style = NativeMethods.GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            found = hwnd;
            return false;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>
    /// Check if a thread's parent message was sent by our bot.
    /// Uses conversations.replies API to get the parent message and check for bot_id.
    /// Results are cached in ownMessageTimestamps to avoid repeated API calls.
    /// </summary>
    static bool IsOwnThread(string botToken, string channel, string threadTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // Get only the parent message (limit=1, inclusive=true)
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return false;

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return false;

            var parent = messages[0];
            // Bot messages have "bot_id" field
            var botId = parent?["bot_id"]?.GetValue<string>();
            return !string.IsNullOrEmpty(botId);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get the bot's parent message text from a thread (for context when replying).
    /// Returns the first message in the thread (the bot's original message).
    /// </summary>
    /// <summary>
    /// Build thread context for Claude prompt: bot's original message + previous user message.
    /// Returns formatted context string, or null if unavailable.
    /// Layout: [클봇 원본]\n{parent}\n\n[이전 메시지]\n{prev}
    /// </summary>
    static string? GetThreadContext(string botToken, string channel, string threadTs, string currentMsgTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // Fetch entire thread (up to 50 messages — enough for context)
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=50&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return null;

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return null;

            // 1. Bot's original message (thread parent = first message)
            var parentText = messages[0]?["text"]?.GetValue<string>();

            // 2. Find previous message (right before current — could be user OR bot)
            // In a thread, conversation flows: bot→user→bot→user, so prev msg gives context
            string? prevText = null;
            bool prevIsBot = false;
            for (int i = messages.Count - 1; i >= 1; i--)
            {
                var msgTs = messages[i]?["ts"]?.GetValue<string>();
                if (msgTs == currentMsgTs) continue; // skip current message

                prevText = messages[i]?["text"]?.GetValue<string>();
                prevIsBot = messages[i]?["bot_id"] != null;
                break;
            }

            // Build context
            var sb = new System.Text.StringBuilder();

            if (!string.IsNullOrEmpty(parentText))
            {
                if (parentText.Length > 300) parentText = parentText[..297] + "...";
                sb.AppendLine("[쓰레드 시작]");
                sb.AppendLine(parentText);
            }

            if (!string.IsNullOrEmpty(prevText))
            {
                if (prevText.Length > 200) prevText = prevText[..197] + "...";
                var label = prevIsBot ? "[직전 클봇 응답]" : "[직전 메시지]";
                sb.AppendLine();
                sb.AppendLine(label);
                sb.AppendLine(prevText);
            }

            var result = sb.ToString().Trim();
            return string.IsNullOrEmpty(result) ? null : result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Update an existing Slack message via chat.update API.
    /// Returns (ok, ts) — ts is the message timestamp.
    /// </summary>
    static async Task<(bool ok, string? ts)> SlackUpdateMessageAsync(
        string botToken, string channel, string ts, string text)
    {
        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, ts, text });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.update");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] chat.update error: {json?["error"]}");

        return (ok, json?["ts"]?.GetValue<string>());
    }

    /// <summary>
    /// Delete a Slack message via chat.delete API.
    /// Used to clean up old status messages when relocating to bottom of channel.
    /// </summary>
    static async Task<bool> SlackDeleteMessageAsync(string botToken, string channel, string ts)
    {
        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, ts });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.delete");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] chat.delete error: {json?["error"]}");

        return ok;
    }
}
