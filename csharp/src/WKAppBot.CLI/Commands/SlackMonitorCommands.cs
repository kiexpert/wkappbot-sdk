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
    static bool IsRunningFromHostApp(string hostToken, string? forceEnvVar = null)
    {
        try
        {
            // Explicit override for edge cases.
            var forced = string.IsNullOrWhiteSpace(forceEnvVar) ? null : Environment.GetEnvironmentVariable(forceEnvVar);
            if (!string.IsNullOrWhiteSpace(forced))
            {
                var v = forced.Trim().ToLowerInvariant();
                if (v is "1" or "true" or "yes" or "on") return true;
                if (v is "0" or "false" or "no" or "off") return false;
            }

            // Strict condition: parent/ancestor process must be Codex app.
            // Do not treat CODEX_HOME alone as sufficient.
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == hostToken || name.Contains(hostToken))
                    return true;
                pid = ppid;
            }

            // Heuristic fallback:
            // some launch paths (MCP/agent wrappers) hide original parent chain.
            // In that case, use host-specific runtime signals.
            if (hostToken == "codex")
            {
                // Codex app environment + alive process is a strong hint.
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CODEX_HOME")) &&
                    IsProcessAlive("codex"))
                    return true;

                // Hidden windows included: do not rely on foreground/focus.
                if (HasTopLevelWindowForProcess("codex", "Chrome_WidgetWin_1"))
                    return true;
            }
            else if (hostToken == "claude")
            {
                // Hidden windows included: do not rely on foreground/focus.
                if (HasTopLevelWindowForProcess("claude", "Chrome_WidgetWin_1"))
                    return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    static bool IsRunningFromCodexApp() =>
        IsRunningFromHostApp("codex", "WKAPPBOT_ASSUME_CODEX_APP");

    static bool IsRunningFromClaudeApp() =>
        IsRunningFromHostApp("claude", "WKAPPBOT_ASSUME_CLAUDE_APP");

    static string? GetSendReplyUsername(bool printDecision = false)
    {
        string? username = null;
        if (IsRunningFromCodexAppCertain())
            username = BuildSlackBotUsername(SlackCodexPrefix, null, spaceBeforeBracket: false);
        else if (IsRunningFromClaudeAppCertain())
            username = BuildSlackBotUsername(SlackClaudePrefix, null, spaceBeforeBracket: false);

        if (printDecision)
            Console.WriteLine($"[SLACK] bot-name: {(string.IsNullOrEmpty(username) ? "(default-bot)" : username)}");

        return username;
    }

    static bool IsRunningFromClaudeAppCertain()
    {
        try
        {
            var forced = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CLAUDE_APP")?.Trim().ToLowerInvariant();
            if (forced is "1" or "true" or "yes" or "on") return true;
            if (forced is "0" or "false" or "no" or "off") return false;

            // Certainty: parent/ancestor chain contains claude process.
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == "claude" || name.StartsWith("claude"))
                    return true;
                pid = ppid;
            }
            return false;
        }
        catch { return false; }
    }

    static bool IsRunningFromCodexAppCertain()
    {
        try
        {
            // Explicit override first.
            var forced = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CODEX_APP")?.Trim().ToLowerInvariant();
            if (forced is "1" or "true" or "yes" or "on") return true;
            if (forced is "0" or "false" or "no" or "off") return false;

            // Certainty rule: parent/ancestor process chain contains codex.
            using var cur = Process.GetCurrentProcess();
            int pid = cur.Id;
            for (int depth = 0; depth < 6; depth++)
            {
                var ppid = GetParentPidForCodexCheck(pid);
                if (ppid <= 0) break;
                using var p = Process.GetProcessById(ppid);
                var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                if (name == "codex" || name.Contains("codex"))
                    return true;
                pid = ppid;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    static int GetParentPidForCodexCheck(int pid)
    {
        try
        {
            using var searcher = new System.Management.ManagementObjectSearcher(
                $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {pid}");
            foreach (var o in searcher.Get())
            {
                var mo = (System.Management.ManagementObject)o;
                return Convert.ToInt32(mo["ParentProcessId"]);
            }
        }
        catch { }
        return -1;
    }

    static bool IsProcessAlive(string processToken)
    {
        try
        {
            var token = processToken.ToLowerInvariant();
            return Process.GetProcesses()
                .Any(p =>
                {
                    try
                    {
                        var n = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                        return n == token || n.Contains(token);
                    }
                    catch { return false; }
                });
        }
        catch
        {
            return false;
        }
    }

    static bool HasTopLevelWindowForProcess(string processToken, string windowClass)
    {
        try
        {
            var token = processToken.ToLowerInvariant();
            var found = false;
            var sb = new System.Text.StringBuilder(256);

            NativeMethods.EnumWindows((hwnd, _) =>
            {
                try
                {
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                    if (pid == 0) return true;
                    using var p = Process.GetProcessById((int)pid);
                    var name = (p.ProcessName ?? string.Empty).ToLowerInvariant();
                    if (!(name == token || name.Contains(token))) return true;

                    sb.Clear();
                    NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
                    var cls = sb.ToString();
                    if (string.Equals(cls, windowClass, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        return false; // stop enum
                    }
                }
                catch { }
                return true;
            }, IntPtr.Zero);

            return found;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// When Claude prompt is lost (FindPrompt returns null), capture foreground window screenshot
    /// and send it to Slack so the user can see what's blocking the prompt (e.g. permission dialog).
    /// Also writes the original message to inbox as fallback.
    /// </summary>
    static void HandlePromptLost(string botToken, string channel, string threadTs,
        string user, string cleanText, string ts, string? username = null)
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
                    SlackSendViaApi(botToken, channel, msg, threadTs, username: username).GetAwaiter().GetResult();
                }
            }
            else
            {
                // No foreground window — just notify
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! 원래 메시지: {cleanText}",
                    threadTs, username: username).GetAwaiter().GetResult();
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
                    threadTs, username: username).GetAwaiter().GetResult();
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
                        Task.Run(async () => await SlackSendViaApi(botToken, channel, ackMsg, reply.threadTs, username: GetBotUsername(instanceName))).Wait(3000);
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

    static readonly string SlackCodexPrefix =
        string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_CODEX_PREFIX"))
            ? "코뎃"
            : Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_CODEX_PREFIX")!.Trim();
    static readonly string SlackClaudePrefix =
        string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_CLAUDE_PREFIX"))
            ? "클롣"
            : Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_CLAUDE_PREFIX")!.Trim();
    static readonly string SlackGenericPrefix =
        string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_GENERIC_PREFIX"))
            ? "앱봇"
            : Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_GENERIC_PREFIX")!.Trim();
    static readonly string SlackBroadcastUsername =
        string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_BROADCAST_NAME"))
            ? "앱봇아이"
            : Environment.GetEnvironmentVariable("WKAPPBOT_SLACK_BROADCAST_NAME")!.Trim();

    static string GetSlackFolderTag()
    {
        var cwd = Environment.CurrentDirectory;
        if (string.IsNullOrWhiteSpace(cwd))
            return Environment.MachineName;

        var trimmed = cwd.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var name = Path.GetFileName(trimmed);
        return string.IsNullOrWhiteSpace(name) ? Environment.MachineName : name;
    }

    static string BuildSlackBotUsername(string prefix, string? instanceName = null, bool spaceBeforeBracket = false)
    {
        var tag = string.IsNullOrWhiteSpace(instanceName) ? GetSlackFolderTag() : instanceName!.Trim();
        var sep = spaceBeforeBracket ? " " : string.Empty;
        return $"{prefix}{sep}[{tag}]";
    }

    static string GetGeneralBotUsername(string? instanceName = null)
    {
        // Explicit override precedence.
        var forceCodex = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CODEX_APP")?.Trim().ToLowerInvariant();
        var forceClaude = Environment.GetEnvironmentVariable("WKAPPBOT_ASSUME_CLAUDE_APP")?.Trim().ToLowerInvariant();
        if (forceCodex is "1" or "true" or "yes" or "on")
            return BuildSlackBotUsername(SlackCodexPrefix, instanceName, spaceBeforeBracket: false);
        if (forceClaude is "1" or "true" or "yes" or "on")
            return BuildSlackBotUsername(SlackClaudePrefix, instanceName, spaceBeforeBracket: false);

        // Auto detect host app.
        if (IsRunningFromCodexApp())
            return BuildSlackBotUsername(SlackCodexPrefix, instanceName, spaceBeforeBracket: false);
        if (IsRunningFromClaudeApp())
            return BuildSlackBotUsername(SlackClaudePrefix, instanceName, spaceBeforeBracket: false);

        // Fallback: generic appbot identity.
        return BuildSlackBotUsername(SlackGenericPrefix, instanceName, spaceBeforeBracket: false);
    }

    /// <summary>Default Slack bot username from unified policy.</summary>
    static readonly string BotUsername = GetGeneralBotUsername();

    /// <summary>Build Slack username override from instance name.</summary>
    static string? GetBotUsername(string? instanceName) =>
        GetGeneralBotUsername(instanceName);

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
            if (json?["ok"]?.GetValue<bool>() != true)
            {
                Console.WriteLine($"[SLACK] IsOwnThread API failed: {json?["error"]}");
                return false;
            }

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return false;

            var parent = messages[0];
            // Bot messages have "bot_id" field
            var botId = parent?["bot_id"]?.GetValue<string>();
            var result = !string.IsNullOrEmpty(botId);
            if (result)
                Console.WriteLine($"[SLACK] IsOwnThread: YES (bot_id={botId}, ts={threadTs})");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] IsOwnThread exception: {ex.Message}");
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
            // Skip ack messages ("전달했습니다") — they are transient and not meaningful context
            string? prevText = null;
            bool prevIsBot = false;
            for (int i = messages.Count - 1; i >= 1; i--)
            {
                var msgTs = messages[i]?["ts"]?.GetValue<string>();
                if (msgTs == currentMsgTs) continue; // skip current message

                var candidateText = messages[i]?["text"]?.GetValue<string>();
                // Skip ack messages (not useful context for Claude)
                if (candidateText != null && candidateText.StartsWith("Claude에 전달했습니다!")) continue;

                prevText = candidateText;
                prevIsBot = messages[i]?["bot_id"] != null;
                break;
            }

            // Build context
            var sb = new System.Text.StringBuilder();

            if (!string.IsNullOrEmpty(parentText) && !parentText.StartsWith("Claude에 전달했습니다!"))
            {
                if (parentText.Length > 300) parentText = parentText[..297] + "...";
                sb.AppendLine("[쓰레드 시작]");
                sb.AppendLine(parentText);
            }

            if (!string.IsNullOrEmpty(prevText))
            {
                if (prevText.Length > 200) prevText = prevText[..197] + "...";
                var label = prevIsBot ? "[직전 클롣 응답]" : "[직전 메시지]";
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

    /// <summary>
    /// Get the latest message in a channel: (ts, reply_count).
    /// Used to check if our status streaming message is still at the bottom and has no replies.
    /// </summary>
    static async Task<(string? ts, int replyCount)> GetChannelLatestMessageInfo(string botToken, string channel)
    {
        using var http = new HttpClient();
        using var req = new HttpRequestMessage(HttpMethod.Get,
            $"https://slack.com/api/conversations.history?channel={channel}&limit=1");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;
        if (!ok) return (null, 0);

        var messages = json?["messages"]?.AsArray();
        if (messages == null || messages.Count == 0) return (null, 0);

        var msg = messages[0];
        var ts = msg?["ts"]?.GetValue<string>();
        var replyCount = msg?["reply_count"]?.GetValue<int>() ?? 0;
        return (ts, replyCount);
    }

    /// Backward-compat wrapper.
    static async Task<string?> GetChannelLatestMessageTs(string botToken, string channel)
    {
        var (ts, _) = await GetChannelLatestMessageInfo(botToken, channel);
        return ts;
    }

    // ── Pending Ack file-based IPC ──────────────────────────────
    // Shared between AppBotEye (writes ack ts) and CLI (reads + deletes ack before replying)
    // File: wkappbot.hq/runtime/pending_acks.json
    // Format: { "threadTs": { "channel": "C...", "ackTs": "1234.5678" }, ... }

    static string PendingAcksFilePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "pending_acks.json");
        }
    }

    /// <summary>Save a pending ack ts for a thread (atomic write).</summary>
    static void SavePendingAck(string threadTs, string channel, string ackTs)
    {
        try
        {
            var all = LoadPendingAcks();
            all[threadTs] = new PendingAckEntry { Channel = channel, AckTs = ackTs };
            WritePendingAcks(all);
        }
        catch { /* best-effort */ }
    }

    /// <summary>Delete a pending ack for a thread and remove the Slack message. Returns true if deleted.</summary>
    static bool DeletePendingAckFromFile(string botToken, string threadTs)
    {
        try
        {
            var all = LoadPendingAcks();
            if (!all.TryGetValue(threadTs, out var entry)) return false;

            all.Remove(threadTs);
            WritePendingAcks(all);

            // Actually delete the Slack message
            var deleted = Task.Run(async () =>
                await SlackDeleteMessageAsync(botToken, entry.Channel, entry.AckTs)).GetAwaiter().GetResult();
            if (deleted)
                Console.WriteLine($"[SLACK] Deleted pending ack in thread {threadTs}");
            return deleted;
        }
        catch { return false; }
    }

    /// <summary>Delete ALL pending acks for a channel (cleanup after bot replies).</summary>
    static void DeleteAllPendingAcksInThread(string botToken, string threadTs)
    {
        DeletePendingAckFromFile(botToken, threadTs);
    }

    static Dictionary<string, PendingAckEntry> LoadPendingAcks()
    {
        try
        {
            var path = PendingAcksFilePath;
            if (!File.Exists(path)) return new();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, PendingAckEntry>>(json, _pendingAckJsonOpts) ?? new();
        }
        catch { return new(); }
    }

    static void WritePendingAcks(Dictionary<string, PendingAckEntry> data)
    {
        var path = PendingAcksFilePath;
        var dir = Path.GetDirectoryName(path)!;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(data, _pendingAckJsonOpts);
        var tmp = path + ".tmp";
        File.WriteAllText(tmp, json);
        File.Move(tmp, path, overwrite: true);
    }

    static readonly JsonSerializerOptions _pendingAckJsonOpts = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    sealed class PendingAckEntry
    {
        public string Channel { get; set; } = "";
        public string AckTs { get; set; } = "";
    }
}
