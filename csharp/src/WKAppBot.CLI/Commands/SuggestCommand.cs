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
        if (args.Length > 0 && args[0] is "resolve")
            return SuggestResolveCommand(args[1..]);

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
            Console.WriteLine("Subcommands:");
            Console.WriteLine("  resolve <ts> \"note\"  Mark suggestion as resolved + Slack reply");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot suggest \"Magnifier doesn't appear when form opens\"");
            Console.WriteLine("  wkappbot suggest \"Need UIA support for MDI child windows\" screenshot.png");
            Console.WriteLine("  wkappbot suggest \"Bug found\" error.png \"Steps to reproduce: ...\"");
            Console.WriteLine("  wkappbot suggest resolve 2026-03-17T05:00:00 \"Fixed in v4.4.5\"");
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
            // Use shared SlackSendViaApi with sender's display name (same as 'slack send')
            var senderName = GetSendReplyUsername();
            var (ok, ts) = SlackSendViaApi(botToken, SuggestChannel, header, username: senderName).GetAwaiter().GetResult();
            if (ok)
            {
                messageTs = ts;
                Console.WriteLine($"[SUGGEST] Slack sent: {header.Split('\n')[0]}{(overflow != null || files.Count > 0 ? " (+thread)" : "")}");
                // Post overflow as thread reply
                if (overflow != null)
                {
                    foreach (var chunk in ChunkText(overflow, 3900))
                        SlackSendViaApi(botToken, SuggestChannel, chunk, threadTs: messageTs, username: senderName).GetAwaiter().GetResult();
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
                files = files.Select(Path.GetFileName).ToArray(),
                slack_ts = messageTs  // Slack message ts for thread reply in resolve
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

    /// <summary>suggest resolve &lt;ts_prefix&gt; "note" — mark done in JSONL + Slack reply.</summary>
    static int SuggestResolveCommand(string[] args)
    {
        if (args.Length < 1 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest resolve <ts> \"note\"");
            Console.WriteLine("  ts  : ISO timestamp prefix (e.g. 2026-03-17T05) or full ts");
            Console.WriteLine("  note: resolution summary (English preferred)");
            return 0;
        }

        var tsPrefix = args[0];
        var note = args.Length >= 2 ? string.Join(" ", args[1..]) : "resolved";

        var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
        var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");
        var historyPath = Path.Combine(hqDir, "suggestions_history.jsonl");

        if (!File.Exists(jsonlPath))
        {
            Console.Error.WriteLine("[RESOLVE] suggestions.jsonl not found");
            return 1;
        }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        int matchIdx = -1;
        JsonNode? matchEntry = null;

        for (int i = 0; i < lines.Count; i++)
        {
            try
            {
                var node = JsonSerializer.Deserialize<JsonNode>(lines[i]);
                var entryTs = node?["ts"]?.GetValue<string>() ?? "";
                if (entryTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    matchIdx = i;
                    matchEntry = node;
                    break;
                }
            }
            catch { }
        }

        if (matchIdx < 0 || matchEntry == null)
        {
            Console.Error.WriteLine($"[RESOLVE] No suggestion found with ts prefix: {tsPrefix}");
            return 1;
        }

        var entryText = matchEntry["text"]?.GetValue<string>() ?? "";
        var slackTs = matchEntry["slack_ts"]?.GetValue<string>();
        var from = matchEntry["from"]?.GetValue<string>() ?? "unknown";
        var preview = entryText.Split('\n')[0];
        if (preview.Length > 80) preview = preview[..80] + "…";
        Console.WriteLine($"[RESOLVE] Found: [{from}] {preview}");

        // Build resolved entry for history
        var resolvedObj = new Dictionary<string, object?>
        {
            ["ts"] = matchEntry["ts"]?.GetValue<string>(),
            ["from"] = from,
            ["cwd"] = matchEntry["cwd"]?.GetValue<string>(),
            ["text"] = entryText,
            ["files"] = matchEntry["files"]?.AsArray().Select(f => f?.GetValue<string>()).ToArray(),
            ["slack_ts"] = slackTs,
            ["review_status"] = "done",
            ["review_note"] = note,
            ["review_ts"] = DateTime.UtcNow.ToString("o")
        };
        var resolvedJson = JsonSerializer.Serialize(resolvedObj);

        // Move: append to history, remove from active
        File.AppendAllText(historyPath, resolvedJson + Environment.NewLine);
        lines.RemoveAt(matchIdx);
        File.WriteAllLines(jsonlPath, lines);
        Console.WriteLine($"[RESOLVE] Moved to history: {note}");

        // Slack reply if slack_ts is available — uses shared SlackSendViaApi (same as 'slack reply')
        if (!string.IsNullOrEmpty(slackTs))
        {
            var botToken = LoadSlackBotToken();
            if (!string.IsNullOrEmpty(botToken))
            {
                var replyText = $":white_check_mark: *RESOLVED* — {note}";
                var (ok, _) = SlackSendViaApi(botToken, SuggestChannel, replyText, threadTs: slackTs).GetAwaiter().GetResult();
                if (ok)
                    Console.WriteLine($"[RESOLVE] Slack reply sent to thread {slackTs}");
                else
                    Console.Error.WriteLine($"[RESOLVE] Slack reply failed");
            }
            else
            {
                Console.Error.WriteLine("[RESOLVE] No bot_token — Slack reply skipped");
            }
        }
        else
        {
            Console.WriteLine("[RESOLVE] No slack_ts recorded — Slack reply skipped");
        }

        return 0;
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
