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

        Console.Error.WriteLine($"[SLACK] {lines.Length} message(s) in inbox:");
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
    /// Usage: wkappbot slack reply "response text" [--channel C0AFXJBMB2N] [--msg 1234567890.123456]
    ///   --channel: target channel ID (default: from latest inbox message or config)
    ///   --msg: reply in-thread to the request message (Slack message timestamp)
    /// </summary>
    static int SlackReplyCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack reply \"response text\" [--channel ID] [--msg TS]");
            return 1;
        }

        // Parse --channel and --msg flags, then text+files via shared ParseTextAndFiles
        string? explicitChannel = null;
        string? threadTs = null;
        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if ((args[i] == "--msg" || args[i] == "--thread") && i + 1 < args.Length)
                threadTs = args[++i];
            else
                remaining.Add(args[i]);
        }
        var (textParts, filePaths) = ParseTextAndFiles(remaining.ToArray());
        var replyText = JoinShellGroupedTextParts(textParts);
        // C-style escape decode: \n → newline, \t → tab, \\ → backslash
        replyText = DecodeCEscapes(replyText);
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

        var senderName = GetSendReplyUsername(printDecision: true);

        // Detect completion/report messages by Claude's report format patterns
        static bool IsCompletionMessage(string text)
        {
            // Check marks (emoji or Slack shortcode)
            if (text.Contains("✅") || text.Contains(":white_check_mark:")) return true;
            // Bullet list + completion keyword (typical report format)
            bool hasBullets = text.Contains("• ") || text.Contains("- ") || text.Contains("✅");
            bool hasCompletion = text.Contains("완료") || text.Contains("수정") || text.Contains("성공")
                || text.Contains("리졸브") || text.Contains("커밋") || text.Contains("fixed")
                || text.Contains("Done") || text.Contains("Complete");
            if (hasBullets && hasCompletion) return true;
            // Debate/test results
            if (text.Contains("═══") && text.Contains("완료")) return true;
            if (text.Contains("PASS") && text.Contains("FAIL")) return true;
            return false;
        }

        // Thread reply: send as-is (no splitting — already in thread context).
        // Channel post (no threadTs): use PostWithOverflow — first paragraph + overflow as thread.
        bool hasTargetThread = !string.IsNullOrEmpty(threadTs);
        bool ok = false;
        string? postedTs = null;

        // Auto-broadcast: completion/report messages also appear in channel
        bool autoBroadcast = hasTargetThread && IsCompletionMessage(replyText);
        if (autoBroadcast)
            Console.WriteLine("[SLACK] Auto-broadcast: completion message detected");

        // Smart merge: if last message in thread is from same sender, no attachments,
        // same minute, and combined size < 3800 → edit instead of new message (saves message quota)
        bool merged = false;
        if (hasTargetThread && filePaths.Count == 0)
        {
            try
            {
                var historyUrl = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&oldest={threadTs}";
                using var http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
                // Get latest reply in thread
                var histResp = http.GetStringAsync($"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=50").GetAwaiter().GetResult();
                var histJson = JsonSerializer.Deserialize<JsonNode>(histResp);
                var msgs = histJson?["messages"]?.AsArray();
                if (msgs != null && msgs.Count > 1)
                {
                    var last = msgs[^1]; // last reply (not thread starter)
                    var lastUser = last?["username"]?.GetValue<string>() ?? "";
                    var lastTs = last?["ts"]?.GetValue<string>() ?? "";
                    var lastText = last?["text"]?.GetValue<string>() ?? "";
                    var lastFiles = last?["files"]?.AsArray();
                    var lastTime = double.TryParse(lastTs, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var lt) ? lt : 0;
                    var nowTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    bool sameAuthor = lastUser == senderName;
                    bool noFiles = lastFiles == null || lastFiles.Count == 0;
                    bool sameMinute = (nowTime - lastTime) < 60;
                    bool sizeOk = (lastText.Length + replyText.Length + 2) < 3800;
                    if (sameAuthor && noFiles && sameMinute && sizeOk)
                    {
                        var combined = lastText + "\n\n" + replyText;
                        var (updOk, _, _) = SlackUpdateMessageAsync(botToken, channel, lastTs, combined).GetAwaiter().GetResult();
                        if (updOk) { merged = true; ok = true; Console.WriteLine("[SLACK] Merged with previous message (same author, <1min)"); }
                    }
                }
            }
            catch { }
        }

        if (!merged)
        {
            if (hasTargetThread)
            {
                (ok, _) = SlackSendViaApi(botToken, channel, replyText, threadTs, username: senderName, replyBroadcast: autoBroadcast).GetAwaiter().GetResult();
                // Fallback: thread not found (old workspace, deleted thread) → post as new message
                if (!ok)
                {
                    Console.WriteLine("[SLACK] Thread reply failed — falling back to new message");
                    (ok, postedTs) = PostWithOverflow(botToken, channel, replyText, username: senderName);
                }
            }
            else
                (ok, postedTs) = PostWithOverflow(botToken, channel, replyText, username: senderName);
        }

        var threadNote = hasTargetThread ? " (in-thread)" : "";
        if (ok)
        {
            Console.Error.WriteLine($"[SLACK] Replied to #{channel}{threadNote}: {replyText.Split('\n')[0]}");
            // Upload files in the same thread
            var uploadThreadTs = threadTs ?? postedTs;
            foreach (var fp in filePaths)
            {
                var uploadArgs = new List<string> { "upload", fp };
                if (!string.IsNullOrEmpty(uploadThreadTs)) { uploadArgs.Add("--msg"); uploadArgs.Add(uploadThreadTs); }
                else { uploadArgs.Add("--channel"); uploadArgs.Add(channel); }
                SlackUploadCommand(uploadArgs.ToArray());
            }
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
        Console.Error.WriteLine($"[SLACK] Found prompt: {prompt.HostType} ({prompt.ProcessName})");
        Console.Error.WriteLine($"[SLACK]   Window: {prompt.WindowTitle}");
        Console.Error.WriteLine($"[SLACK]   Rect: ({prompt.PromptRect.X},{prompt.PromptRect.Y} {prompt.PromptRect.Width}x{prompt.PromptRect.Height})");
        Console.ResetColor();

        if (!watchMode)
        {
            // One-shot: process current inbox
            return ProcessInboxToPrompt(helper, prompt);
        }

        // Watch mode: continuously poll inbox
        Console.Error.WriteLine($"[SLACK] Watch mode: polling inbox every {intervalSec}s (Ctrl+C to stop)");

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

        Console.Error.WriteLine($"[SLACK] {lines.Length} message(s) to forward to Claude prompt");

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
                    var replyCmd = $"MUST REPLY VIA SLACK ONLY: wkappbot slack reply \"MUST PUT FINAL ANSWER HERE\" --msg {ts}";
                var promptText = $"{text}\n\n(Slack @{user} → {replyCmd})";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Error.WriteLine($"[SLACK] >> Typing into prompt: {promptText}");
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
                Console.Error.WriteLine($"[SLACK] Error processing inbox message: {ex.Message}");
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
            Console.Error.WriteLine($"[SLACK] Fetching messages after ts={lastTs}");
        else
            Console.Error.WriteLine($"[SLACK] No bookmark — fetching last {limit} messages");

        var messages = SlackFetchHistoryAsync(botToken, channel, lastTs, limit).GetAwaiter().GetResult();

        if (messages.Count == 0)
        {
            Console.WriteLine("[SLACK] No new messages");
            return 0;
        }

        // Get bot user ID to filter own messages
        var botUserId = SlackGetBotUserId(botToken);

        Console.Error.WriteLine($"[SLACK] {messages.Count} message(s) fetched:");

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
            var replyHint = $"MUST REPLY VIA SLACK ONLY: wkappbot slack reply \"MUST PUT FINAL ANSWER HERE\" --msg {replyThread}";
                    var promptText = $"{cleanText}\n\n(Slack @{user} → {replyHint})";

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
            Console.Error.WriteLine($"[SLACK] Bookmark updated: ts={newestTs}");
        }

        if (forwarded > 0)
            Console.Error.WriteLine($"[SLACK] {forwarded} @mention(s) forwarded");
        else
            Console.WriteLine("[SLACK] No @mentions to forward");

        promptHelper?.Dispose();
        return 0;
    }

    /// <summary>
    /// Parse a user-supplied time string to a Slack epoch ts string.
    /// Accepts: Slack ts (1234567890.123), Unix epoch (1234567890), ISO date/datetime (2026-03-17 or 2026-03-17T05:00).
    /// </summary>
    static string? ParseSlackTs(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        // Already a Slack ts or Unix epoch
        if (double.TryParse(s, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out _))
            return s;
        // ISO date/datetime → Unix epoch
        if (DateTime.TryParse(s, null, System.Globalization.DateTimeStyles.AssumeLocal, out var dt))
            return ((DateTimeOffset)dt).ToUnixTimeSeconds().ToString();
        Console.Error.WriteLine($"[SLACK] --oldest/--latest: cannot parse '{s}' as timestamp");
        return null;
    }

    /// <summary>Fetch channel history via conversations.history API.</summary>
    static async Task<List<JsonNode>> SlackFetchHistoryAsync(string botToken, string channel,
        string? oldest = null, int limit = 20, bool inclusive = false, string? latest = null)
    {
        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };
        var url = $"https://slack.com/api/conversations.history?channel={channel}&limit={limit}";
        if (!string.IsNullOrEmpty(oldest))
            url += $"&oldest={oldest}";
        if (!string.IsNullOrEmpty(latest))
            url += $"&latest={latest}";
        if (inclusive)
            url += "&inclusive=true";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        if (json?["ok"]?.GetValue<bool>() != true)
        {
            Console.Error.WriteLine($"[SLACK] conversations.history failed: {json?["error"]}");
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
            Console.Error.WriteLine($"[SLACK] conversations.replies failed: {json?["error"]}");
            return new List<JsonNode>();
        }

        var messages = json["messages"]?.AsArray();
        if (messages == null) return new List<JsonNode>();

        return messages.Where(m => m != null).Select(m => m!).ToList();
    }

    /// <summary>
    /// wkappbot slack list [thread_ts] [--limit N] [--search "keyword"] [--from "user"] [--delete-pattern "pat"]
    /// List recent channel messages or thread replies with full Slack metadata.
    /// thread_ts auto-detected: if first non-flag arg looks like "1234.5678", treat as thread.
    /// --search / --grep / -g : filter messages by text (case-insensitive substring or regex)
    /// --from "user" : filter by sender username
    /// </summary>
    static int SlackListCommand(string[] args)
    {
        if (args.Length >= 2 && args[1] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage: wkappbot slack list [ts] [options]");
            Console.WriteLine("  --limit N                     Max messages to fetch (default 20)");
            Console.WriteLine("  --search / --grep / -g        Filter by text (substring or regex)");
            Console.WriteLine("  --from \"user\"                Filter by sender username");
            Console.WriteLine("  --oldest / --since / --after  Show messages after ts/date");
            Console.WriteLine("  --latest / --before           Show messages before ts/date");
            Console.WriteLine("  --thread ts                   Fetch replies in thread");
            Console.WriteLine("  --channel / -c id             Override channel ID");
            Console.WriteLine("  --delete-pattern pat          Delete r=0 messages matching glob");
            return 0;
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;
        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] bot_token or channel missing in config.");
            return 1;
        }

        int limit = 20;
        string? deletePattern = null;
        string? threadTs = null;
        string? searchPattern = null;
        string? fromFilter = null;
        string? oldest = null;   // --oldest: Slack epoch ts or parseable date
        string? latest = null;   // --latest / --before
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--limit" && i + 1 < args.Length)
                int.TryParse(args[++i], out limit);
            else if (args[i] == "--delete-pattern" && i + 1 < args.Length)
                deletePattern = args[++i];
            else if (args[i] == "--thread" && i + 1 < args.Length)
                threadTs = args[++i];
            else if ((args[i] == "--channel" || args[i] == "-c") && i + 1 < args.Length)
                channel = args[++i];
            else if ((args[i] == "--search" || args[i] == "--grep" || args[i] == "-g") && i + 1 < args.Length)
                searchPattern = args[++i];
            else if (args[i] == "--from" && i + 1 < args.Length)
                fromFilter = args[++i];
            else if ((args[i] == "--oldest" || args[i] == "--since" || args[i] == "--after") && i + 1 < args.Length)
                oldest = ParseSlackTs(args[++i]);
            else if ((args[i] == "--latest" || args[i] == "--before") && i + 1 < args.Length)
                latest = ParseSlackTs(args[++i]);
            else if (args[i].StartsWith("C0", StringComparison.Ordinal) && args[i].Length >= 8)
                channel = args[i]; // auto-detect channel ID: "C0A21P52EAH"
            else if (args[i].Contains('.') && char.IsDigit(args[i][0]))
                threadTs = args[i]; // auto-detect ts: "1234567890.123456"
        }

        // If search filter is active, fetch more messages to have enough after filtering
        if (searchPattern != null || fromFilter != null)
            limit = Math.Max(limit, 200);

        List<JsonNode> messages;
        if (threadTs != null)
        {
            // Try as thread parent first
            messages = Task.Run(async () =>
                await SlackFetchRepliesAsync(botToken, channel, threadTs, limit))
                .GetAwaiter().GetResult();


            // If empty or single (no replies) → maybe it's a reply ts, not the parent
            // Fetch that message to find its thread_ts (parent)
            if (messages.Count <= 1)
            {
                var probe = Task.Run(async () =>
                    await SlackFetchHistoryAsync(botToken, channel, oldest: threadTs, limit: 1, inclusive: true))
                    .GetAwaiter().GetResult();
                var parentTs = probe.FirstOrDefault()?["thread_ts"]?.GetValue<string>();
                if (parentTs != null && parentTs != threadTs)
                {
                    Console.Error.WriteLine($"[SLACK] ts={threadTs} is a reply → parent={parentTs}");
                    threadTs = parentTs;
                    messages = Task.Run(async () =>
                        await SlackFetchRepliesAsync(botToken, channel, threadTs, limit))
                        .GetAwaiter().GetResult();
                }
            }
            Console.Error.WriteLine($"[SLACK] Thread ts={threadTs}:");
        }
        else
        {
            messages = Task.Run(async () =>
                await SlackFetchHistoryAsync(botToken, channel, limit: limit, oldest: oldest, latest: latest))
                .GetAwaiter().GetResult();
        }

        if (messages.Count == 0)
        {
            Console.WriteLine("[SLACK] No messages found.");
            return 0;
        }

        // ── Client-side filtering ──
        System.Text.RegularExpressions.Regex? searchRegex = null;
        if (searchPattern != null)
        {
            try { searchRegex = new System.Text.RegularExpressions.Regex(searchPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase); }
            catch { /* invalid regex → fallback to substring */ }
        }

        if (searchPattern != null || fromFilter != null)
        {
            messages = messages.Where(m => {
                var text = m["text"]?.GetValue<string>() ?? "";
                var user = m["username"]?.GetValue<string>() ?? m["user"]?.GetValue<string>() ?? "";
                bool textMatch = searchPattern == null || (searchRegex != null
                    ? searchRegex.IsMatch(text)
                    : text.Contains(searchPattern, StringComparison.OrdinalIgnoreCase));
                bool fromMatch = fromFilter == null ||
                    user.Contains(fromFilter, StringComparison.OrdinalIgnoreCase);
                return textMatch && fromMatch;
            }).ToList();
        }

        // Collect delete candidates
        var toDelete = new List<(string ts, string preview)>();
        var toSkipThreadStarter = new List<(string ts, string preview, int replies)>();

        var filterDesc = "";
        if (searchPattern != null) filterDesc += $" search=\"{searchPattern}\"";
        if (fromFilter != null) filterDesc += $" from=\"{fromFilter}\"";
        var header = threadTs != null
            ? $"[SLACK] Thread ({messages.Count} messages){filterDesc}:"
            : $"[SLACK] Channel messages ({messages.Count}){filterDesc}:";
        Console.WriteLine(header);
        Console.WriteLine(new string('─', 120));

        foreach (var m in messages)
        {
          try {
            var ts = m["ts"]?.GetValue<string>() ?? "?";
            var user = m["username"]?.GetValue<string>()
                    ?? m["user"]?.GetValue<string>() ?? "?";
            var subtype = m["subtype"]?.GetValue<string>() ?? "";
            var text = (m["text"]?.GetValue<string>() ?? "").Replace("\n", " ");
            var replyCount = m["reply_count"]?.GetValue<int>() ?? 0;
            var replyUsers = m["reply_users_count"]?.GetValue<int>() ?? 0;
            var latestReply = m["latest_reply"]?.GetValue<string>();

            // Timestamp → human-readable
            string timeStr = ts;
            if (ts.IndexOf('.') is int dot and > 0
                && long.TryParse(ts.AsSpan(0, dot), out var epoch))
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(epoch).ToLocalTime();
                timeStr = dt.ToString("MM-dd HH:mm:ss");
            }

            // Reactions
            var reactions = m["reactions"]?.AsArray();
            var reactStr = "";
            if (reactions != null && reactions.Count > 0)
            {
                var parts = reactions.Select(r =>
                    $":{r!["name"]}:{r["count"]}").Take(3);
                reactStr = $" [{string.Join(" ", parts)}]";
            }

            // Files/attachments
            var files = m["files"]?.AsArray();
            var fileStr = "";
            if (files != null && files.Count > 0)
            {
                var names = files.Select(f => f!["name"]?.GetValue<string>() ?? "?").Take(2);
                fileStr = $" 📎{string.Join(",", names)}";
            }

            // Attachments (link unfurls, etc.)
            var attachments = m["attachments"]?.AsArray();
            var attachStr = attachments != null && attachments.Count > 0
                ? $" 🔗{attachments.Count}" : "";

            // Thread info
            var threadStr = "";
            if (replyCount > 0)
            {
                threadStr = $" 💬{replyCount}({replyUsers}人)";
                if (latestReply != null && latestReply.IndexOf('.') is int ld and > 0
                    && long.TryParse(latestReply.AsSpan(0, ld), out var lrEpoch))
                {
                    var lrDt = DateTimeOffset.FromUnixTimeSeconds(lrEpoch).ToLocalTime();
                    threadStr += $" last={lrDt:HH:mm}";
                }
            }

            // Edited
            var edited = m["edited"] != null ? " ✏️" : "";

            // Pinned
            var pinned = m["pinned_to"] != null ? " 📌" : "";

            // Bot/app info
            var botLabel = "";
            if (!string.IsNullOrEmpty(subtype)) botLabel += $" ({subtype})";
            var botId = m["bot_id"]?.GetValue<string>();
            if (botId != null) botLabel += $" bot={botId[..Math.Min(8, botId.Length)]}";

            // Truncate text (Console.WindowWidth throws when stdout is redirected — fallback to 120)
            int maxLen;
            try { maxLen = Console.WindowWidth > 40 ? Console.WindowWidth - 30 : 120; } catch { maxLen = 120; }
            var preview = text.Length > maxLen ? text[..maxLen] + "…" : text;

            Console.Write($"  {timeStr} | ");
            Console.ForegroundColor = replyCount > 0 ? ConsoleColor.Cyan : ConsoleColor.Gray;
            Console.Write($"{user,-22}");
            Console.ResetColor();
            Console.Write($"{threadStr}{reactStr}{fileStr}{attachStr}{edited}{pinned}{botLabel}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"    ts={ts}  {preview}");
            Console.ResetColor();

            // Check delete pattern
            if (deletePattern != null && WildcardMatch(text, deletePattern))
            {
                if (replyCount > 0)
                    toSkipThreadStarter.Add((ts, preview, replyCount)); // thread starter — refuse delete
                else
                    toDelete.Add((ts, preview));
            }
          } catch (Exception _ex) { Console.WriteLine($"  [SLACK:LIST_ERR] {_ex.GetType().Name}: {_ex.Message}"); }
        }

        Console.WriteLine(new string('─', 120));
        Console.WriteLine($"  Total: {messages.Count} messages");

        // ── Thread-starter protection: warn and refuse ──
        if (deletePattern != null && toSkipThreadStarter.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[SLACK] ✗ REFUSED — {toSkipThreadStarter.Count} thread-starter(s) matched but will NOT be deleted (has replies):");
            Console.ResetColor();
            foreach (var (ts, preview, rc) in toSkipThreadStarter)
                Console.WriteLine($"  ✗ SKIP {ts} | 💬{rc} | {preview}");
        }

        // ── Delete matching messages ──
        if (deletePattern != null && toDelete.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[SLACK] Deleting {toDelete.Count} messages matching \"{deletePattern}\" (r=0 only):");
            Console.ResetColor();
            int deleted = 0;
            foreach (var (ts, preview) in toDelete)
            {
                var ok = Task.Run(async () =>
                    await SlackDeleteMessageAsync(botToken, channel, ts, guardThreadStarter: false)) // pre-scanned r=0
                    .GetAwaiter().GetResult();
                var status = ok ? "✓" : "✗";
                Console.WriteLine($"  {status} {ts} | {preview}");
                if (ok) deleted++;
                Thread.Sleep(300); // rate limit
            }
            Console.WriteLine($"  Deleted: {deleted}/{toDelete.Count}");
        }
        else if (deletePattern != null && toSkipThreadStarter.Count == 0)
        {
            Console.WriteLine($"\n[SLACK] No r=0 messages matching \"{deletePattern}\".");
        }

        return 0;
    }

    /// <summary>
    /// wkappbot slack delete &lt;ts&gt; [ts2 ts3 ...] [--i-really-want-to-delete-including-replies]
    /// Delete one or more Slack messages by timestamp.
    /// Thread-starter messages (has replies) are refused unless --i-really-want-to-delete-including-replies is given.
    /// </summary>
    static int SlackDeleteCommand(string[] args)
    {
        var config = LoadSlackConfig();
        if (config == null) return 1;
        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] bot_token or channel missing in config.");
            return 1;
        }

        bool force = args.Contains("--i-really-want-to-delete-including-replies");
        var tsList = args.Skip(1)
            .Where(a => !a.StartsWith("--") && a.Contains('.') && char.IsDigit(a[0]))
            .ToList();

        if (tsList.Count == 0)
        {
            Console.WriteLine("Usage: wkappbot slack delete <ts> [ts2 ...] [--i-really-want-to-delete-including-replies]");
            Console.WriteLine("  ts: message timestamp (e.g. 1773554407.386809)");
            Console.WriteLine("  --i-really-want-to-delete-including-replies: delete even if the message is a thread starter (has replies)");
            return 1;
        }

        int deleted = 0, skipped = 0, failed = 0;
        foreach (var ts in tsList)
        {
            var ok = Task.Run(async () =>
                await SlackDeleteMessageAsync(botToken, channel, ts, guardThreadStarter: !force))
                .GetAwaiter().GetResult();

            if (ok)
            {
                Console.WriteLine($"  ✓ {ts}");
                deleted++;
            }
            else
            {
                // Check if it was a thread-starter skip or an actual API failure
                // SlackDeleteMessageAsync logs SKIP message itself when guardThreadStarter fires
                Console.WriteLine($"  ✗ {ts}");
                if (!force)
                    skipped++; // thread-starter protection fired — need --i-really-want-to-delete-including-replies to override
                else
                    failed++;
            }
            if (tsList.Count > 1) Thread.Sleep(300); // rate limit
        }

        Console.WriteLine($"  Deleted: {deleted}/{tsList.Count}" + (skipped > 0 ? $" | Skipped (thread-starter): {skipped}" : ""));
        return deleted > 0 ? 0 : 1;
    }

}
