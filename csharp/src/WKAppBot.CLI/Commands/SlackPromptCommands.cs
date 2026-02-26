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

// partial class: Slack prompt, reply, inbox, catch-up commands
internal partial class Program
{
    /// <summary>Show unread inbox messages (from @mentions).</summary>
    static int SlackInboxCommand()
    {
        if (!File.Exists(SlackInboxFile))
        {
            Console.WriteLine("[SLACK] Inbox is empty");
            return 0;
        }

        var lines = File.ReadAllLines(SlackInboxFile);
        if (lines.Length == 0)
        {
            Console.WriteLine("[SLACK] Inbox is empty");
            return 0;
        }

        Console.WriteLine($"[SLACK] {lines.Length} message(s) in inbox:");
        foreach (var line in lines)
        {
            try
            {
                var msg = JsonSerializer.Deserialize<JsonNode>(line);
                var time = msg?["time"]?.GetValue<string>() ?? "";
                var user = msg?["user"]?.GetValue<string>() ?? "";
                var text = msg?["text"]?.GetValue<string>() ?? "";
                var channel = msg?["channel"]?.GetValue<string>() ?? "";
                Console.WriteLine($"  [{time}] {user}: {text}");
            }
            catch { Console.WriteLine($"  {line}"); }
        }
        return 0;
    }

    /// <summary>
    /// Reply to a Slack message (and clear inbox).
    /// Usage: wkappbot slack reply "response text" [--channel C0AFXJBMB2N] [--thread 1234567890.123456]
    ///   --channel: target channel ID (default: from latest inbox message or config)
    ///   --thread: reply in-thread to the original message (Slack thread_ts)
    /// </summary>
    static int SlackReplyCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack reply \"response text\" [--channel ID] [--thread TS]");
            return 1;
        }

        // Parse --channel and --thread flags
        string? explicitChannel = null;
        string? threadTs = null;
        var textParts = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
            {
                explicitChannel = args[++i];
            }
            else if (args[i] == "--thread" && i + 1 < args.Length)
            {
                threadTs = args[++i];
            }
            else
            {
                textParts.Add(args[i]);
            }
        }

        var replyText = string.Join(" ", textParts);
        // Bash history expansion escapes ! to \! even in single quotes — undo it
        replyText = replyText.Replace("\\!", "!");
        if (string.IsNullOrWhiteSpace(replyText))
        {
            Console.WriteLine("[SLACK] ERROR: reply text is empty");
            return 1;
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();

        // Channel/thread priority: --flags > reply context file > inbox > config default
        string? channel = explicitChannel;

        // Try saved reply context (written by listener on every forwarded message)
        if (string.IsNullOrEmpty(channel) || threadTs == null)
        {
            var (ctxChannel, ctxThread) = LoadReplyContext();
            channel ??= ctxChannel;
            threadTs ??= ctxThread;
        }

        // Fallback: inbox last message
        if (string.IsNullOrEmpty(channel) && File.Exists(SlackInboxFile))
        {
            try
            {
                var lines = File.ReadAllLines(SlackInboxFile);
                if (lines.Length > 0)
                {
                    var lastMsg = JsonSerializer.Deserialize<JsonNode>(lines[^1]);
                    channel ??= lastMsg?["channel"]?.GetValue<string>();
                    threadTs ??= lastMsg?["ts"]?.GetValue<string>();
                }
            }
            catch { }
        }

        // Final fallback: config default channel
        channel ??= config["channel"]?.GetValue<string>();

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found");
            return 1;
        }

        // Delete pending "전달했습니다" ack before replying (file-based IPC from AppBotEye)
        if (!string.IsNullOrEmpty(threadTs))
            DeletePendingAckFromFile(botToken, threadTs);

        var (ok, _) = SlackSendViaApi(botToken, channel, replyText, threadTs, username: BotUsername).GetAwaiter().GetResult();

        var threadNote = !string.IsNullOrEmpty(threadTs) ? " (in-thread)" : "";
        if (ok)
        {
            Console.WriteLine($"[SLACK] Replied to #{channel}{threadNote}: {replyText}");
            // Clear inbox after reply
            try { File.Delete(SlackInboxFile); } catch { }
        }
        else
            Console.WriteLine("[SLACK] Failed to send reply");

        return ok ? 0 : 1;
    }

    /// <summary>
    /// Find the Claude Code prompt and type inbox messages into it.
    /// Usage: wkappbot slack prompt [--watch] [--interval N]
    ///   --watch: continuously watch inbox for new messages
    ///   --interval N: polling interval in seconds (default: 3)
    /// </summary>
    static int SlackPromptCommand(string[] args)
    {
        bool watchMode = args.Contains("--watch") || args.Contains("-w");
        int intervalSec = 3;

        // Parse --interval N
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--interval" && int.TryParse(args[i + 1], out int val))
                intervalSec = val;
        }

        Console.WriteLine("[SLACK] Finding Claude Code prompt...");

        using var helper = new ClaudePromptHelper();
        var prompt = helper.FindPrompt();

        if (prompt == null)
        {
            Console.WriteLine("[SLACK] ERROR: Could not find Claude Code prompt input field");
            Console.WriteLine("[SLACK] Make sure Claude Desktop or VS Code with Claude Code is open");
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SLACK] Found prompt: {prompt.HostType} ({prompt.ProcessName})");
        Console.WriteLine($"[SLACK]   Window: {prompt.WindowTitle}");
        Console.WriteLine($"[SLACK]   Rect: ({prompt.PromptRect.X},{prompt.PromptRect.Y} {prompt.PromptRect.Width}x{prompt.PromptRect.Height})");
        Console.ResetColor();

        if (!watchMode)
        {
            // One-shot: process current inbox
            return ProcessInboxToPrompt(helper, prompt);
        }

        // Watch mode: continuously poll inbox
        Console.WriteLine($"[SLACK] Watch mode: polling inbox every {intervalSec}s (Ctrl+C to stop)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\n[SLACK] Stopping prompt watcher...");
        };

        while (!cts.Token.IsCancellationRequested)
        {
            ProcessInboxToPrompt(helper, prompt);

            try { Task.Delay(intervalSec * 1000, cts.Token).GetAwaiter().GetResult(); }
            catch (OperationCanceledException) { break; }
        }

        return 0;
    }

    /// <summary>Read inbox and type each message into the Claude prompt.</summary>
    static int ProcessInboxToPrompt(ClaudePromptHelper helper, ClaudePromptHelper.PromptInfo prompt)
    {
        if (!File.Exists(SlackInboxFile))
            return 0;

        string[] lines;
        try
        {
            lines = File.ReadAllLines(SlackInboxFile);
        }
        catch
        {
            return 0;
        }

        if (lines.Length == 0) return 0;

        Console.WriteLine($"[SLACK] {lines.Length} message(s) to forward to Claude prompt");

        foreach (var line in lines)
        {
            try
            {
                var msg = JsonSerializer.Deserialize<JsonNode>(line);
                var user = msg?["user"]?.GetValue<string>() ?? "unknown";
                var text = msg?["text"]?.GetValue<string>() ?? "";
                var time = msg?["time"]?.GetValue<string>() ?? "";
                var channel = msg?["channel"]?.GetValue<string>() ?? "";

                if (string.IsNullOrWhiteSpace(text)) continue;

                // Format: message first, then source attribution + reply hint (with thread_ts)
                var ts = msg?["ts"]?.GetValue<string>() ?? "";
                var replyCmd = $"wkappbot slack reply \"your response\" --channel {channel} --thread {ts}";
                var promptText = $"{text}\n\n(Slack @{user} #{channel} — reply: {replyCmd})";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[SLACK] >> Typing into prompt: {promptText}");
                Console.ResetColor();

                // Re-find prompt each time (window may have moved)
                var freshPrompt = helper.FindPrompt();
                if (freshPrompt == null)
                {
                    Console.WriteLine("[SLACK] ERROR: Lost prompt window!");
                    return 1;
                }

                var ok = helper.TypeAndSubmit(freshPrompt, promptText);
                if (!ok)
                {
                    Console.WriteLine("[SLACK] ERROR: Failed to type into prompt");
                    return 1;
                }

                // Wait for Claude to process before next message
                Console.WriteLine("[SLACK] Waiting for Claude to process...");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SLACK] Error processing inbox message: {ex.Message}");
            }
        }

        // Clear inbox after processing
        try { File.Delete(SlackInboxFile); } catch { }
        Console.WriteLine("[SLACK] Inbox cleared");

        return 0;
    }

    /// <summary>
    /// Fetch missed messages from channel history since last processed timestamp.
    /// Usage: wkappbot slack catch-up [--channel ID] [--limit N]
    /// Always forwards @mentions to Claude prompt.
    /// </summary>
    static int SlackCatchUpCommand(string[] args)
    {
        string? explicitChannel = null;
        int limit = 20;
        bool toPrompt = true; // Always forward to Claude prompt

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if (args[i] == "--limit" && i + 1 < args.Length && int.TryParse(args[i + 1], out int n))
            { limit = n; i++; }
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = explicitChannel ?? config["channel"]?.GetValue<string>();

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found");
            return 1;
        }

        var lastTs = LoadLastTs(channel);
        if (lastTs != null)
            Console.WriteLine($"[SLACK] Fetching messages after ts={lastTs}");
        else
            Console.WriteLine($"[SLACK] No bookmark — fetching last {limit} messages");

        var messages = SlackFetchHistoryAsync(botToken, channel, lastTs, limit).GetAwaiter().GetResult();

        if (messages.Count == 0)
        {
            Console.WriteLine("[SLACK] No new messages");
            return 0;
        }

        // Get bot user ID to filter own messages
        var botUserId = SlackGetBotUserId(botToken);

        Console.WriteLine($"[SLACK] {messages.Count} message(s) fetched:");

        // Messages come newest-first from API, reverse to chronological order
        messages.Reverse();

        string? newestTs = null;
        int forwarded = 0;

        // Always initialize prompt helper (forward to Claude)
        var promptHelper = new ClaudePromptHelper();
        {
            var promptInfo = promptHelper.FindPrompt();
            if (promptInfo == null)
            {
                Console.WriteLine("[SLACK] WARNING: Could not find Claude prompt — writing to inbox instead");
                toPrompt = false;
            }
        }

        foreach (var msg in messages)
        {
            var user = msg["user"]?.GetValue<string>() ?? "";
            var text = msg["text"]?.GetValue<string>() ?? "";
            var ts = msg["ts"]?.GetValue<string>() ?? "";
            var threadTs = msg["thread_ts"]?.GetValue<string>();
            var subtype = msg["subtype"]?.GetValue<string>();

            // Skip bot's own messages and subtypes
            if (user == botUserId || subtype != null) continue;
            // Skip empty
            if (string.IsNullOrWhiteSpace(text)) continue;

            // Strip bot mentions
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                text, @"<@[A-Z0-9]+>\s*", "").Trim();
            if (string.IsNullOrEmpty(cleanText)) continue;

            // Check if it mentions our bot (has @mention)
            var isMention = text.Contains($"<@{botUserId}>");

            var prefix = isMention ? "@mention" : "msg";
            Console.WriteLine($"  [{prefix}] {user}: {cleanText}");

            // Only forward @mentions (not all channel chatter)
            if (isMention)
            {
                var replyThread = threadTs ?? ts;
                if (toPrompt && promptHelper != null)
                {
                    var replyHint = $"wkappbot slack reply \"your response\" --channel {channel} --thread {replyThread}";
                    var promptText = $"{cleanText}\n\n(Slack @{user} #{channel} — reply: {replyHint})";

                    var fresh = promptHelper.FindPrompt();
                    if (fresh != null)
                    {
                        promptHelper.TypeAndSubmit(fresh, promptText);
                        Console.WriteLine("    >> Sent to Claude prompt");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    WriteInbox(channel, user, cleanText, ts);
                }
                forwarded++;
            }

            newestTs = ts;
        }

        // Update bookmark
        if (newestTs != null)
        {
            SaveLastTs(channel, newestTs);
            Console.WriteLine($"[SLACK] Bookmark updated: ts={newestTs}");
        }

        if (forwarded > 0)
            Console.WriteLine($"[SLACK] {forwarded} @mention(s) forwarded");
        else
            Console.WriteLine("[SLACK] No @mentions to forward");

        promptHelper?.Dispose();
        return 0;
    }

    /// <summary>Fetch channel history via conversations.history API.</summary>
    static async Task<List<JsonNode>> SlackFetchHistoryAsync(string botToken, string channel,
        string? oldest = null, int limit = 20, bool inclusive = false)
    {
        using var http = new HttpClient();
        var url = $"https://slack.com/api/conversations.history?channel={channel}&limit={limit}";
        if (!string.IsNullOrEmpty(oldest))
            url += $"&oldest={oldest}";
        if (inclusive)
            url += "&inclusive=true";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        if (json?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] conversations.history failed: {json?["error"]}");
            return new List<JsonNode>();
        }

        var messages = json["messages"]?.AsArray();
        if (messages == null) return new List<JsonNode>();

        return messages.Where(m => m != null).Select(m => m!).ToList();
    }

    /// <summary>
    /// Fetch thread replies via Slack conversations.replies API.
    /// Returns all messages in the thread (parent first, then replies oldest→newest).
    /// </summary>
    static async Task<List<JsonNode>> SlackFetchRepliesAsync(string botToken, string channel,
        string threadTs, int limit = 20)
    {
        using var http = new HttpClient();
        var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit={limit}";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        if (json?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] conversations.replies failed: {json?["error"]}");
            return new List<JsonNode>();
        }

        var messages = json["messages"]?.AsArray();
        if (messages == null) return new List<JsonNode>();

        return messages.Where(m => m != null).Select(m => m!).ToList();
    }
}
