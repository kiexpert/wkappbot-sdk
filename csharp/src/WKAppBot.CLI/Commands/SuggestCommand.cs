using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// partial class: wkappbot suggest "건의 내용" [file.png] — webhook + file upload to #클봇-전체
internal partial class Program
{
    // Hardcoded webhook + channel for #클봇-전체
    const string SuggestWebhookUrl = "https://hooks.slack.com/services/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";
    const string SuggestChannel = "C0A21P52EAH";

    static int SuggestCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage: wkappbot suggest \"text\" [file.png] [\"more text\"]");
            Console.WriteLine("  Send a suggestion/feature request to #클봇-전체 via webhook.");
            Console.WriteLine("  Args = lines (like ask/slack send). Files auto-detected & attached.");
            Console.WriteLine("  Automatically tags sender's workspace (CWD shortname).");
            Console.WriteLine("  Also saves to wkappbot.hq/suggestions.jsonl for local record.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ⚠ Write in ENGLISH to save tokens (Korean = 2-3x token cost).");
            Console.WriteLine("     Short & precise wins. Senior Claudes will thank you. 🙏");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot suggest \"Magnifier doesn't appear when form opens\"");
            Console.WriteLine("  wkappbot suggest \"Need UIA support for MDI child windows\" screenshot.png");
            Console.WriteLine("  wkappbot suggest \"Bug found\" error.png \"Steps to reproduce: ...\"");
            return 0;
        }

        // Separate text vs files (shared ParseTextAndFiles)
        var (textParts, files) = ParseTextAndFiles(args);
        var text = string.Join("\n", textParts);
        var cwdTag = AbbreviateCwd(Environment.CurrentDirectory);
        if (string.IsNullOrEmpty(cwdTag)) cwdTag = "unknown";

        Console.WriteLine($"[SUGGEST] from [{cwdTag}]");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  ⚠ Tip: write suggestions in ENGLISH — Korean = 2-3x token cost.");
        Console.ResetColor();
        Console.WriteLine($"[SUGGEST] {text}");
        if (files.Count > 0)
            Console.WriteLine($"[SUGGEST] {files.Count} file(s): {string.Join(", ", files.Select(Path.GetFileName))}");

        // Build Slack message with [file:name] markers for attached files
        var msgLines = new List<string> { $":memo: *[건의]* from `{cwdTag}`" };
        foreach (var part in textParts)
            msgLines.Add(part);
        foreach (var f in files)
            msgLines.Add($"[file:{Path.GetFileName(f)}]");
        var slackMsg = string.Join("\n", msgLines);

        string? messageTs = null;

        // Split message: short channel header + optional thread overflow (same as slack send)
        var (header, overflow) = SplitMessageForChannel(slackMsg);

        var botToken = LoadSlackBotToken();
        if (!string.IsNullOrEmpty(botToken))
        {
            // Bot API path: full threading support
            messageTs = SuggestPostMessage(botToken, header);
            if (messageTs != null)
            {
                Console.WriteLine($"[SUGGEST] Slack sent: {header.Split('\n')[0]}{(overflow != null || files.Count > 0 ? " (+thread)" : "")}");
                // Post overflow as thread reply
                if (overflow != null)
                {
                    foreach (var chunk in ChunkText(overflow, 3900))
                        SuggestPostMessage(botToken, chunk, threadTs: messageTs);
                    Console.WriteLine($"[SUGGEST] Thread: {overflow.Split('\n').Length} overflow line(s)");
                }
                // Upload files as thread replies
                foreach (var f in files)
                {
                    Console.WriteLine($"[SUGGEST] Uploading {Path.GetFileName(f)}...");
                    SlackUploadFileAsync(botToken, SuggestChannel, f,
                        title: Path.GetFileName(f), threadTs: messageTs).GetAwaiter().GetResult();
                }
                if (files.Count > 0) Console.WriteLine("[SUGGEST] File(s) uploaded OK");
            }
            else
                Console.Error.WriteLine("[SUGGEST] chat.postMessage failed");
        }
        else if (files.Count == 0)
        {
            // No bot token, no files — use webhook (fast fallback)
            try
            {
                using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
                var payload = JsonSerializer.Serialize(new { text = slackMsg });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var resp = http.PostAsync(SuggestWebhookUrl, content).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                    Console.WriteLine("[SUGGEST] Slack webhook OK");
                else
                    Console.Error.WriteLine($"[SUGGEST] Webhook failed: {resp.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SUGGEST] Webhook error: {ex.Message}");
            }
        }
        else
        {
            Console.Error.WriteLine("[SUGGEST] No bot_token found — cannot upload files.");
        }

        // Save to HQ suggestions.jsonl
        try
        {
            var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
            Directory.CreateDirectory(hqDir);
            var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");
            var entry = new
            {
                ts = DateTime.UtcNow.ToString("o"),
                from = cwdTag,
                cwd = Environment.CurrentDirectory,
                text = text,
                files = files.Select(Path.GetFileName).ToArray()
            };
            var json = JsonSerializer.Serialize(entry);
            File.AppendAllText(jsonlPath, json + Environment.NewLine);
            Console.WriteLine($"[SUGGEST] Saved to {jsonlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST] Failed to save locally: {ex.Message}");
        }

        return 0;
    }

    /// <summary>Post a message via chat.postMessage and return the ts (for threading file uploads).</summary>
    static string? SuggestPostMessage(string botToken, string text, string? threadTs = null)
    {
        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
            var payload = threadTs != null
                ? JsonSerializer.Serialize(new { channel = SuggestChannel, text = text, thread_ts = threadTs })
                : JsonSerializer.Serialize(new { channel = SuggestChannel, text = text });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var resp = http.PostAsync("https://slack.com/api/chat.postMessage", content).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonSerializer.Deserialize<JsonNode>(body);
            if (json?["ok"]?.GetValue<bool>() == true)
                return json["ts"]?.GetValue<string>();
            Console.Error.WriteLine($"[SUGGEST] chat.postMessage error: {json?["error"]}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST] chat.postMessage failed: {ex.Message}");
        }
        return null;
    }

    /// <summary>Load bot_token from webhook.json.</summary>
    static string? LoadSlackBotToken()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq", "profiles", "slack_exp", "webhook.json");
        if (!File.Exists(path)) return null;
        try
        {
            var json = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(path));
            return json?["bot_token"]?.GetValue<string>();
        }
        catch { return null; }
    }
}
