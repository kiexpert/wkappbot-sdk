using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Slack Report ──

    // AsyncLocal: triad parent can pre-create a shared thread ts before spawning Task.Run children.
    // Each child Task inherits the value and posts to the same thread (no duplicate headers).
    internal static readonly System.Threading.AsyncLocal<string?> _slackSessionThreadTs = new();

    // Suppress Slack for internal sub-calls (e.g. whisper study → AskAiForStudy).
    // Set to true before calling AskCommand internally to prevent channel noise.
    internal static readonly System.Threading.AsyncLocal<bool> _suppressSlackSession = new();

    // Persona posted flag — triad posts persona once (first AI wins), others skip.
    static int _slackPersonaPostedFlag = 0; // Interlocked: 0=not posted, 1=posted

    // Per-session last-post tracker: sessionThreadTs → (msgTs, username, text).
    // Used to detect "latest comment is mine" and append via chat.update instead of new post.
    // Tool ⏳ markers update this too, so AI answers won't falsely append over tool messages.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string msgTs, string user, string text)>
        _slackThreadLastPost = new();
    const int SlackMaxAppendLength = 3800; // Slack message text limit (conservative)

    /// Post to thread and return the message ts (for later in-place updates). Null if no thread or error.
    /// Also updates _slackThreadLastPost so subsequent calls know the current "last poster".
    internal static string? SlackPostToThreadAndGetTs(string msg, string? username = null)
    {
        var threadTs = _slackSessionThreadTs.Value;
        if (threadTs == null) return null;
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;
            var uname = username ?? BotUsername;
            var (ok, ts) = SlackSendViaApi(botToken, channel, msg,
                threadTs: threadTs, username: uname).GetAwaiter().GetResult();
            if (ok && ts != null)
                _slackThreadLastPost[threadTs] = (ts, uname, msg);
            return ok ? ts : null;
        }
        catch { return null; }
    }

    /// Update a specific Slack message in-place by its ts. Noop if config unavailable.
    internal static void SlackUpdateThreadMessage(string msgTs, string text)
    {
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, msgTs, text).GetAwaiter().GetResult();
            if (!ok) Console.WriteLine($"[SLACK] SlackUpdateThreadMessage failed for ts={msgTs}");
        }
        catch { }
    }

    /// Post text to the current session's Slack thread. Noop if no active thread.
    /// If the latest thread message was posted by the same username, appends via chat.update
    /// instead of creating a new message (per user request: "최신 댓글이 자기꺼인 경우만 편집").
    internal static void SlackPostToThread(string msg, string? username = null)
    {
        var sessionTs = _slackSessionThreadTs.Value;
        if (sessionTs == null) return;
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var uname = username ?? BotUsername;

            // Append via chat.update if the latest thread comment is from the same AI
            if (_slackThreadLastPost.TryGetValue(sessionTs, out var last) && last.user == uname)
            {
                var combined = last.text + "\n\n" + msg;
                if (combined.Length <= SlackMaxAppendLength)
                {
                    var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, last.msgTs, combined).GetAwaiter().GetResult();
                    if (ok)
                    {
                        _slackThreadLastPost[sessionTs] = (last.msgTs, uname, combined);
                        return;
                    }
                    // If update fails, fall through to post new
                }
                // Combined too long — fall through to new post
            }

            // Post new message (chunked for long content)
            const int chunkSize = 3000;
            string? newMsgTs = null;
            for (int pos = 0; pos < msg.Length; pos += chunkSize)
            {
                var chunk = msg[pos..Math.Min(pos + chunkSize, msg.Length)];
                // Extract emoji from username for icon_emoji (e.g. "🦊 GPT(SKEPTIC)" → ":fox:")
                var iconEmoji = ExtractDebateIconEmoji(uname);
                var (ok, ts) = SlackSendViaApiWithIcon(botToken, channel, chunk,
                    threadTs: sessionTs, username: uname, iconEmoji: iconEmoji).GetAwaiter().GetResult();
                if (ok && ts != null && pos == 0) newMsgTs = ts;
            }
            if (newMsgTs != null)
                _slackThreadLastPost[sessionTs] = (newMsgTs, uname, msg.Length > SlackMaxAppendLength ? msg[..SlackMaxAppendLength] : msg);
        }
        catch { }
    }

    /// <summary>Map Unicode emoji in username to Slack :shortcode: for icon_emoji.</summary>
    static string? ExtractDebateIconEmoji(string? username) => username switch
    {
        _ when username?.Contains("🦉") == true => ":owl:",
        _ when username?.Contains("🦊") == true => ":fox_face:",
        _ when username?.Contains("🐬") == true => ":dolphin:",
        _ when username?.Contains("🐙") == true => ":octopus:",
        _ => null,
    };

    /// <summary>SlackSendViaApi wrapper that injects icon_emoji for debate posts.</summary>
    static async Task<(bool ok, string? ts)> SlackSendViaApiWithIcon(
        string botToken, string channel, string text,
        string? threadTs = null, string? username = null, string? iconEmoji = null)
    {
        using var http = new HttpClient();
        var dict = new Dictionary<string, object> { ["channel"] = channel, ["text"] = text };
        if (!string.IsNullOrEmpty(threadTs)) dict["thread_ts"] = threadTs;
        if (!string.IsNullOrEmpty(username)) dict["username"] = username;
        if (!string.IsNullOrEmpty(iconEmoji)) dict["icon_emoji"] = iconEmoji;
        else
        {
            // Fall back to workspace custom icon
            var callerCwd = EyeCmdPipeServer.CallerCwd.Value;
            var icon = GetCustomSlackIcon(callerCwd);
            if (icon != null)
            {
                if (icon.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    dict["icon_url"] = icon;
                else
                    dict["icon_emoji"] = icon;
            }
        }
        var payload = System.Text.Json.JsonSerializer.Serialize(dict);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;
        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = System.Text.Json.JsonDocument.Parse(body);
        var ok = json.RootElement.GetProperty("ok").GetBoolean();
        var ts = json.RootElement.TryGetProperty("ts", out var tsProp) ? tsProp.GetString() : null;
        return (ok, ts);
    }

    /// <summary>
    /// Open a Slack thread for the current session if not already open.
    /// Returns the thread ts. Safe to call multiple times (idempotent per AsyncLocal context).
    /// </summary>
    internal static string? EnsureSlackThread(string label, string question)
    {
        if (_suppressSlackSession.Value) return null; // internal sub-call (e.g. whisper study)
        if (_slackSessionThreadTs.Value != null)
            return _slackSessionThreadTs.Value;   // already opened by triad parent

        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;

            var qTrunc = question.Length > 200 ? question[..200] + "..." : question;
            var (ok, ts) = SlackSendViaApi(botToken, channel, $"*[{label}]* {qTrunc}", username: GetSendReplyUsername())
                               .GetAwaiter().GetResult();
            if (ok) _slackSessionThreadTs.Value = ts;
            return ok ? ts : null;
        }
        catch { return null; }
    }

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel  = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            // Open (or reuse) the session thread — triad shares one thread, single AI gets its own
            var ts = EnsureSlackThread(aiName, question);
            if (ts == null) { Console.WriteLine("[ASK] Slack report: no thread ts"); return; }

            // Thread: answer in 3000-char chunks (no truncation — split as needed)
            const int chunkSize = 3000;
            int pos = 0, part = 1;
            while (pos < answer.Length)
            {
                var chunk = answer[pos..Math.Min(pos + chunkSize, answer.Length)];
                SlackSendViaApi(botToken, channel, chunk, threadTs: ts, username: GetSendReplyUsername()).GetAwaiter().GetResult();
                pos += chunkSize;
                part++;
            }
            Console.WriteLine($"[ASK] Reported to Slack (thread {ts}, {part - 1} part(s))");
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "Slack report failed", ex);
        }
    }
}
