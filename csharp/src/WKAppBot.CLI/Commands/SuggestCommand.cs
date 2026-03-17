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
        if (args.Length > 0 && args[0] is "list" or "ls")
            return SuggestListCommand(args[1..]);

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
            Console.WriteLine("  list / ls              Show pending suggestions");
            Console.WriteLine("  resolve <ts> \"note\"   Mark resolved + Slack thread reply");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot suggest \"Magnifier doesn't appear when form opens\"");
            Console.WriteLine("  wkappbot suggest \"Need UIA support for MDI child windows\" screenshot.png");
            Console.WriteLine("  wkappbot suggest list");
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

    /// <summary>suggest list — show pending suggestions in a pretty format. Auto-syncs slack_ts from Slack.</summary>
    static int SuggestListCommand(string[] args)
    {
        var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
        var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");

        if (!File.Exists(jsonlPath))
        {
            Console.WriteLine("[SUGGEST] No suggestions.jsonl found.");
            return 0;
        }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        if (lines.Count == 0)
        {
            Console.WriteLine("[SUGGEST] No pending suggestions. 🎉");
            return 0;
        }

        // ── Auto-sync slack_ts: fetch Slack messages since latest HQ entry ──
        var entries = lines.Select(l => System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(l)!).ToList();
        var unsynced = entries.Where(e => e["slack_ts"] == null || e["slack_ts"]?.GetValue<string>() == null).ToList();

        if (unsynced.Count > 0)
        {
            var botToken = LoadSlackBotToken();
            if (!string.IsNullOrEmpty(botToken))
            {
                // Use HQ file mtime as oldest — only fetch Slack messages since last local update
                var fileMtime = File.GetLastWriteTimeUtc(jsonlPath);
                var oldestUnix = ((DateTimeOffset)fileMtime).AddMinutes(-5).ToUnixTimeSeconds(); // -5min buffer
                Console.WriteLine($"[SUGGEST] Syncing slack_ts from Slack (since {fileMtime.ToLocalTime():MM-dd HH:mm})...");
                {

                    try
                    {
                        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
                        http.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
                        var url = $"https://slack.com/api/conversations.history?channel={SuggestChannel}&limit=200&oldest={oldestUnix}";
                        var resp = http.GetAsync(url).GetAwaiter().GetResult();
                        var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var json = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(body);
                        var msgs = json?["messages"]?.AsArray() ?? new System.Text.Json.Nodes.JsonArray();

                        // Filter 건의 messages only
                        var suggestMsgs = msgs
                            .Where(m => (m?["text"]?.GetValue<string>() ?? "").Contains(":memo: *[건의]*"))
                            .Select(m => (ts: m!["ts"]!.GetValue<string>(), body: string.Join("\n", (m["text"]?.GetValue<string>() ?? "").Split('\n').Skip(1))))
                            .ToList();

                        int synced = 0;
                        bool changed = false;
                        for (int i = 0; i < entries.Count; i++)
                        {
                            if (entries[i]["slack_ts"] != null) continue;
                            var eText = (entries[i]["text"]?.GetValue<string>() ?? "").Trim();
                            var eKey = eText[..Math.Min(40, eText.Length)].ToLower();

                            var best = suggestMsgs
                                .Select(sm => (sm.ts, score: Enumerable.Zip(eKey, sm.body.Trim().ToLower()).Count(p => p.First == p.Second)))
                                .Where(x => x.score >= 20)
                                .OrderByDescending(x => x.score)
                                .FirstOrDefault();

                            if (best.ts != null)
                            {
                                var obj = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object?>>(lines[i])!;
                                obj["slack_ts"] = best.ts;
                                lines[i] = System.Text.Json.JsonSerializer.Serialize(obj);
                                entries[i] = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(lines[i])!;
                                synced++;
                                changed = true;
                            }
                        }

                        if (changed)
                            File.WriteAllLines(jsonlPath, lines);
                        if (synced > 0)
                            Console.WriteLine($"[SUGGEST] Synced {synced} slack_ts from Slack ✓");
                        else
                            Console.WriteLine($"[SUGGEST] No new matches found in Slack.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[SUGGEST] Slack sync failed: {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine($"[SUGGEST] Pending: {lines.Count} suggestion(s)");
        Console.WriteLine(new string('─', 100));

        for (int i = 0; i < lines.Count; i++)
        {
            try
            {
                var d = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(lines[i]);
                var ts = d?["ts"]?.GetValue<string>() ?? "";
                var from = d?["from"]?.GetValue<string>() ?? "?";
                var text = d?["text"]?.GetValue<string>() ?? "";
                var slackTs = d?["slack_ts"]?.GetValue<string>();
                var files = d?["files"]?.AsArray();

                // Date: ISO → MM-dd HH:mm
                string dateStr = ts;
                if (DateTime.TryParse(ts, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt))
                    dateStr = dt.ToLocalTime().ToString("MM-dd HH:mm");

                // Slack link indicator
                var slackMark = slackTs != null ? " 🔗" : "   ";

                // First line of text as title, rest as body
                var textLines = text.Split('\n');
                var title = textLines[0];
                var body = textLines.Length > 1 ? string.Join(" ", textLines[1..].Where(l => l.Trim().Length > 0)) : "";

                // Trim title to fit
                if (title.Length > 70) title = title[..70] + "…";
                if (body.Length > 80) body = body[..80] + "…";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  {i + 1,2}.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" {dateStr}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" [{from}]");
                Console.Write(slackMark);
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"      {title}");
                Console.ResetColor();

                if (body.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"      {body}");
                    Console.ResetColor();
                }

                if (files != null && files.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"      📎 {string.Join(", ", files.Select(f => f?.GetValue<string>()))}");
                    Console.ResetColor();
                }

                // ts prefix for resolve
                var tsPrefix = ts.Length >= 16 ? ts[..16] : ts;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"      ts={tsPrefix}");
                Console.ResetColor();
            }
            catch { Console.WriteLine($"  {i + 1}. [parse error]"); }
        }

        Console.WriteLine(new string('─', 100));
        Console.WriteLine($"  🔗 = Slack ts recorded  |  resolve: wkappbot suggest resolve <ts> \"note\"");
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
                var resolverName = GetSendReplyUsername();
                var (ok, _) = SlackSendViaApi(botToken, SuggestChannel, replyText, threadTs: slackTs, username: resolverName).GetAwaiter().GetResult();
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
