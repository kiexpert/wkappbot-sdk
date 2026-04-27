using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// partial class: wkappbot suggest "건의 내용" [file.png] -- slack send to current channel
internal partial class Program
{

    static int SuggestCommand(string[] args)
    {
        Environment.SetEnvironmentVariable("WKAPPBOT_SUGGEST_BYPASS", "1");

        // Surface half-resolved entries (1/2 co-resolve, awaiting confirm) at
        // the very top BEFORE any subcommand dispatch. Silent when empty.
        PrintPendingCoResolveBanner();

        if (args.Length > 0 && args[0] is "resolve")
            return SuggestResolveCommand(args[1..]);
        if (args.Length > 0 && args[0] is "list" or "ls")
            return SuggestListCommand(args[1..]);
        if (args.Length > 0 && args[0] is "repost")
            return SuggestRepostCommand(args[1..]);
        if (args.Length > 0 && args[0] is "merge")
            return SuggestMergeCommand(args[1..]);
        if (args.Length > 0 && args[0] is "show" or "get" or "view")
            return SuggestShowCommand(args[1..]);
        if (args.Length > 0 && args[0] is "delegate")
            return SuggestDelegateCommand(args[1..]);
        if (args.Length > 0 && args[0] is "audit")
            return SuggestAuditCommand(args[1..]);
        if (args.Length > 0 && args[0] is "add-requirement" or "add-req")
            return SuggestAddRequirementCommand(args[1..]);

        // Guard: single lowercase word that looks like an unrecognized subcommand -> error instead of saving as text.
        // Prevents accidental "suggest show 19" -> garbage JSONL entry.
        if (args.Length > 0 && args[0].All(ch => char.IsLower(ch) || ch == '-') && args[0].Length <= 12
            && !args[0].StartsWith('-'))
        {
            Console.Error.WriteLine($"[SUGGEST] Unknown subcommand: '{args[0]}'");
            Console.Error.WriteLine($"  Known subcommands: list, show, merge, resolve, repost, audit");
            Console.Error.WriteLine($"  To suggest text starting with '{args[0]}', quote it: wkappbot suggest \"{string.Join(" ", args)}\"");
            return 1;
        }

        if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage: wkappbot suggest \"text\" [file.png] [\"more text\"]");
            Console.WriteLine("  Send a suggestion/feature request to #클봇-전체 via webhook.");
            Console.WriteLine("  Args = lines (like ask/slack send). Files auto-detected & attached.");
            Console.WriteLine("  Automatically tags sender's workspace (CWD shortname).");
            Console.WriteLine("  Also saves to wkappbot.hq/suggestions.jsonl for local record.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ! Write in ENGLISH to save tokens (Korean = 2-3x token cost).");
            Console.WriteLine("     Short & precise wins. Senior Claudes will thank you. 🙏");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Subcommands:");
            Console.WriteLine("  list / ls              Show pending suggestions");
            Console.WriteLine("  show <n|ts>            Show full text of suggestion #n or by ts prefix");
            Console.WriteLine("  merge <ts1> [ts2..] --title \"...\"  Merge related suggestions into one self-healing record");
            Console.WriteLine("  resolve <ts> \"note\"   Mark resolved + Slack thread reply");
            Console.WriteLine("  repost [ts]            Re-post to Slack entries missing slack_ts, write ID back");
            Console.WriteLine("  audit                  Health-check the suggest store (split-brain detector, exit=1 on mismatch)");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot suggest \"Magnifier doesn't appear when form opens\" \\");
            Console.WriteLine("    --requirement \"wkappbot a11y inspect *App* => magnifier appears\" \\");
            Console.WriteLine("    --requirement \"wkappbot a11y screenshot *App* => form visible\" \\");
            Console.WriteLine("    --requirement \"wkappbot a11y click *App*#*OK* => no focus steal\"");
            Console.WriteLine("  wkappbot suggest list");
            Console.WriteLine("  wkappbot suggest resolve 2026-03-17T05:00:00 \"Fixed in v4.4.5\"");
            Console.WriteLine();
            Console.WriteLine("Requirements (3 required for human suggests):");
            Console.WriteLine("  --requirement \"wkappbot <cmd> <args> => <expected output>\"");
            Console.WriteLine("  NOTE: do NOT include launcher flags (--sudo, --no-wait) in requirement cmd.");
            Console.WriteLine("        Strip them: 'wkappbot --sudo a11y ...' -> 'wkappbot a11y ...'");
            Console.WriteLine();
            Console.WriteLine("Merge (combine duplicates into one self-healing record):");
            Console.WriteLine("  wkappbot suggest merge <ts1> [ts2] --title \"...\" \\");
            Console.WriteLine("    --root-cause \"...\" --components \"...\" \\");
            Console.WriteLine("    --work \"1h\" --affected-cmds \"cmd1,cmd2\"");
            Console.WriteLine("  Options: --keep-originals  --force  --auto-heal-script <path>");
            Console.WriteLine("  Example:");
            Console.WriteLine("    wkappbot suggest merge 2026-04-01T10 2026-04-01T11 \\");
            Console.WriteLine("      --title \"CDP timeout on Ghost tabs\" --root-cause \"Chrome hang\" \\");
            Console.WriteLine("      --work \"1h\" --affected-cmds ask --components CdpClient");
            Console.WriteLine();
            // Show experience DB test scripts summary
            var testsRoot = Path.Combine(DataDir, "experience", "tests");
            if (Directory.Exists(testsRoot))
            {
                var allTests = Directory.GetFiles(testsRoot, "*", SearchOption.AllDirectories);
                if (allTests.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"📚 Experience DB: {allTests.Length} test script(s) from previous resolves");
                    Console.WriteLine($"   Path: {testsRoot}");
                    foreach (var catDir in Directory.GetDirectories(testsRoot).OrderBy(d => d))
                    {
                        var cat = Path.GetFileName(catDir);
                        foreach (var subDir in Directory.GetDirectories(catDir).OrderBy(d => d))
                        {
                            var sub = Path.GetFileName(subDir);
                            var subFiles = Directory.GetFiles(subDir).Select(Path.GetFileName).ToArray();
                            if (subFiles.Length > 0)
                                Console.WriteLine($"   {cat}/{sub}: {string.Join(", ", subFiles)}");
                        }
                    }
                    Console.WriteLine($"   Tip: reference these when writing your own resolve evidence!");
                    Console.ResetColor();
                }
            }
            return 0;
        }

        // Separate text vs files (shared ParseTextAndFiles)
        bool force = args.Contains("--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste");
        var suggestRequirements = new List<string>();
        var filteredArgs = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--requirement" && i + 1 < args.Length)
            {
                var raw = args[++i];
                if (!raw.Contains(" => "))
                {
                    Console.Error.WriteLine($"[SUGGEST] --requirement must use ' => ' separator: \"{raw}\"");
                    Console.Error.WriteLine($"  Correct format: --requirement \"wkappbot <cmd> <args> => <expected result>\"");
                    Console.Error.WriteLine($"  Example:        --requirement \"wkappbot a11y click *App* => button clicked, no focus steal\"");
                    return 1;
                }
                suggestRequirements.Add(raw);
            }
            else if (args[i] != "--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste")
                filteredArgs.Add(args[i]);
        }
        var (textParts, files) = ParseTextAndFiles(filteredArgs.ToArray());
        var text = string.Join("\n", textParts);
        if (!force && HasEncodingRisk(text, "suggest")) return 1;

        // Require at least 3 --requirements for human-submitted suggests (not auto-generated).
        bool isAutoSuggest = text.StartsWith("[BUG-AUTO]", StringComparison.OrdinalIgnoreCase)
            || text.StartsWith("[CDP-FALLBACK]", StringComparison.OrdinalIgnoreCase)
            || text.StartsWith("[AI-NEWS]", StringComparison.OrdinalIgnoreCase)
            || text.StartsWith("[HN]", StringComparison.OrdinalIgnoreCase)
            || text.StartsWith("[YT:", StringComparison.OrdinalIgnoreCase);
        if (!isAutoSuggest && suggestRequirements.Count < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SUGGEST] FAILED: at least 3 --requirement flags required for human suggests (got {suggestRequirements.Count}).");
            Console.WriteLine("  Each requirement = one concrete wkappbot command + expected output:");
            Console.WriteLine("    --requirement \"wkappbot <cmd> <args> => <expected output pattern>\"");
            Console.WriteLine("  Requirements become behavioral contracts written into the skill on resolve.");
            Console.ResetColor();
            return 1;
        }
        // Validate each requirement: cmd must start with 'wkappbot ' + known subcommand.
        if (!isAutoSuggest)
        {
            string[] knownSubcmds = ["a11y", "skill", "suggest", "ask", "eye", "slack", "file",
                "gc", "logcat", "windows", "mcp", "claude-usage", "newchat", "validate", "win-move",
                "schedule", "android", "speak", "screenshot", "snapshot"];
            foreach (var req in suggestRequirements)
            {
                var sep = req.IndexOf(" => ", StringComparison.Ordinal);
                var cmd = sep >= 0 ? req[..sep].Trim() : req.Trim();
                if (!cmd.StartsWith("wkappbot ", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[SUGGEST] FAILED: requirement cmd must start with 'wkappbot ': \"{cmd}\"");
                    Console.ResetColor();
                    return 1;
                }
                var parts = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2 || !knownSubcmds.Contains(parts[1], StringComparer.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (parts.Length >= 2 && parts[1].StartsWith("--"))
                        Console.WriteLine($"[SUGGEST] FAILED: requirement cmd contains launcher flag '{parts[1]}' -- strip it from requirements (launcher flags are not wkappbot subcommands). Example: 'wkappbot {string.Join(" ", parts[2..])}'. Known subcommands: {string.Join(", ", knownSubcmds)}");
                    else
                        Console.WriteLine($"[SUGGEST] FAILED: unknown wkappbot subcommand in requirement: '{(parts.Length >= 2 ? parts[1] : "?")}'. Known: {string.Join(", ", knownSubcmds)}");
                    Console.ResetColor();
                    return 1;
                }
            }
        }
        var cwdTag = AbbreviateCwd(Environment.CurrentDirectory);
        if (string.IsNullOrEmpty(cwdTag)) cwdTag = "unknown";

        Console.Error.WriteLine($"[SUGGEST] from [{cwdTag}]");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  ! Tip: write suggestions in ENGLISH -- Korean = 2-3x token cost.");
        Console.ResetColor();
        Console.Error.WriteLine($"[SUGGEST] {text}");
        if (files.Count > 0)
            Console.Error.WriteLine($"[SUGGEST] {files.Count} file(s): {string.Join(", ", files.Select(Path.GetFileName))}");

        // Build Slack message with [file:name] markers for attached files
        var msgLines = new List<string> { $":memo: *[건의]* from `{cwdTag}`" };
        foreach (var part in textParts)
            msgLines.Add(part);
        foreach (var f in files)
            msgLines.Add($"[file:{Path.GetFileName(f)}]");
        var slackMsg = string.Join("\n", msgLines);

        string? messageTs = null;

        // Send via normal slack send channel (LoadSlackConfig -- same as 'wkappbot slack send')
        var config = LoadSlackConfig();
        var botToken = config?["bot_token"]?.GetValue<string>();
        var channel  = config?["channel"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
        {
            var senderName = GetSuggestBypassUsername();
            var (ok, ts) = PostWithOverflow(botToken, channel, slackMsg, username: senderName);
            if (ok)
            {
                messageTs = ts;
                Console.Error.WriteLine($"[SUGGEST] Slack sent (+{files.Count} file(s))");
                Console.Error.WriteLine($"[SUGGEST] ID: {messageTs}  (resolve: suggest resolve {messageTs} \"note\")");
                foreach (var f in files)
                {
                    Console.Error.WriteLine($"[SUGGEST] Uploading {Path.GetFileName(f)}...");
                    SlackUploadFileAsync(botToken, channel, f,
                        title: Path.GetFileName(f), threadTs: messageTs).GetAwaiter().GetResult();
                }
                if (files.Count > 0) Console.WriteLine("[SUGGEST] File(s) uploaded OK");
            }
            else
                Console.Error.WriteLine("[SUGGEST] Slack send failed");
        }
        else
        {
            Console.Error.WriteLine("[SUGGEST] No Slack config found -- saved locally only (run 'wkappbot slack setup' to configure)");
        }

        // Save to HQ suggestions.jsonl
        var newEntryTs = DateTime.UtcNow.ToString("o");
        bool saveOk = false;
        try
        {
            var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
            Directory.CreateDirectory(hqDir);
            var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");
            var entry = new
            {
                ts = newEntryTs,
                from = cwdTag,
                cwd = Environment.CurrentDirectory,
                text = text,
                files = files.Select(Path.GetFileName).ToArray(),
                slack_ts = messageTs,  // Slack message ts for thread reply in resolve
                // Slack channel ID where the thread lives -- required for cross-project
                // resolves so chat.postMessage can target the correct channel + thread.
                // Same workspace/bot, so resolver's bot_token works for replies.
                slack_channel = channel ?? "",
                requirements = suggestRequirements.Count > 0 ? suggestRequirements.ToArray() : null
            };
            var json = JsonSerializer.Serialize(entry);
            File.AppendAllText(jsonlPath, json + Environment.NewLine);
            Console.Error.WriteLine($"[SUGGEST] Saved to {jsonlPath}");
            saveOk = true;

            // Auto-commit: subject = first ~50 chars of the title (line 1 of text).
            var firstLine = (text ?? "").Split('\n', 2)[0];
            var titleSlice = ShortTsForCommit(firstLine, 50);
            GitCommitSuggestFiles("submit", titleSlice);
        }
        catch (InvalidOperationException)
        {
            // Auto-commit failure -- bubble up as non-zero exit per spec (no silent swallow).
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SUGGEST] Failed to save locally: {ex.Message}");
        }

        // Similarity nudge: scan open suggests for >=40% title overlap and
        // print a ready-to-run merge command. Silent when none. Best-effort;
        // never fails the submit.
        if (saveOk)
            SuggestSimilarityNudge(text ?? "", newEntryTs);

        return 0;
    }

    /// <summary>suggest list -- show pending suggestions in a pretty format. Auto-syncs slack_ts from Slack.</summary>
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

        // -- Auto-sync slack_ts: fetch Slack messages since latest HQ entry --
        var entries = lines
            .Select(l => System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(l)!)
            .Where(e => e["review_status"]?.GetValue<string>() != "done"   // skip resolved
                     && e["status"]?.GetValue<string>() != "done") // skip done
            .ToList();
        if (entries.Count == 0)
        {
            Console.WriteLine("[SUGGEST] No pending suggestions. 🎉");
            return 0;
        }
        var unsynced = entries.Where(e => e["slack_ts"] == null || e["slack_ts"]?.GetValue<string>() == null).ToList();

        if (unsynced.Count > 0)
        {
            var syncCfg = LoadSlackConfig();
            var botToken = syncCfg?["bot_token"]?.GetValue<string>();
            var syncChannel = syncCfg?["channel"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(syncChannel))
            {
                // Use HQ file mtime as oldest -- only fetch Slack messages since last local update
                var fileMtime = File.GetLastWriteTimeUtc(jsonlPath);
                var oldestUnix = ((DateTimeOffset)fileMtime).AddMinutes(-5).ToUnixTimeSeconds(); // -5min buffer
                Console.Error.WriteLine($"[SUGGEST] Syncing slack_ts from Slack (since {fileMtime.ToLocalTime():MM-dd HH:mm})...");
                {

                    try
                    {
                        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
                        http.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
                        var url = $"https://slack.com/api/conversations.history?channel={syncChannel}&limit=200&oldest={oldestUnix}";
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
                            Console.Error.WriteLine($"[SUGGEST] Synced {synced} slack_ts from Slack v");
                        else
                            Console.Error.WriteLine($"[SUGGEST] No new matches found in Slack.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[SUGGEST] Slack sync failed: {ex.Message}");
                    }
                }
            }
        }

        Console.Error.WriteLine($"[SUGGEST] Pending: {lines.Count} suggestion(s)");
        Console.WriteLine(new string('─', 100));

        for (int i = 0; i < lines.Count; i++)
        {
            try
            {
                var d = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(lines[i]);
                var ts       = d?["ts"]?.GetValue<string>()       ?? "";
                var from     = d?["from"]?.GetValue<string>()     ?? "?";
                var slackTs  = d?["slack_ts"]?.GetValue<string>();
                var entryType = d?["type"]?.GetValue<string>()    ?? "";
                var slackMark = slackTs != null ? " 🔗" : "   ";

                // Date: ISO -> MM-dd HH:mm
                string dateStr = ts;
                if (DateTime.TryParse(ts, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt))
                    dateStr = dt.ToLocalTime().ToString("MM-dd HH:mm");

                var tsPrefix = ts.Length >= 16 ? ts[..16] : ts;

                if (entryType == "merge")
                {
                    // -- Merge record display ------------------------------------------
                    var mergeTitle   = d?["title"]?.GetValue<string>()       ?? "?";
                    var rootCause    = d?["rootCause"]?.GetValue<string>();
                    var count        = d?["count"]?.GetValue<int>()           ?? 0;
                    var freq         = d?["frequencyPerHour"]?.GetValue<double>() ?? 0;
                    var risk         = d?["recurrenceRisk"]?.GetValue<string>() ?? "";
                    var prio         = d?["priority"]?.GetValue<int>()        ?? 0;
                    var work         = d?["estimatedWork"]?.GetValue<string>();
                    var components   = d?["components"]?.AsArray().Select(x => x?.GetValue<string>()).Where(x => x != null).ToArray() ?? [];
                    var affCmds      = d?["affectedCommands"]?.AsArray().Select(x => x?.GetValue<string>()).Where(x => x != null).ToArray() ?? [];
                    var errPat       = d?["errorPattern"]?.GetValue<string>();
                    var healScript   = d?["autoHealScript"]?.GetValue<string>();
                    var healResult   = d?["autoHealResult"]?.GetValue<string>();
                    var prioStars    = new string('★', prio) + new string('☆', Math.Max(0, 5 - prio));
                    var riskColor    = risk switch { "critical" => ConsoleColor.Red, "high" => ConsoleColor.Yellow, "medium" => ConsoleColor.DarkYellow, _ => ConsoleColor.DarkGray };

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"  {i + 1,2}.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" {dateStr}");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($" [MERGE×{count}]");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" [{from}]");
                    Console.Write(slackMark);
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"      {(mergeTitle.Length > 72 ? mergeTitle[..72] + "..." : mergeTitle)}");
                    Console.ResetColor();

                    // Priority + risk + freq row
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"      {prioStars}({prio}/5)");
                    Console.ForegroundColor = riskColor;
                    Console.Write($"  risk={risk}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"  freq={freq}/hr");
                    if (work != null) Console.Write($"  est={work}");
                    Console.WriteLine();
                    Console.ResetColor();

                    if (rootCause != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"      root: {(rootCause.Length > 90 ? rootCause[..90] + "..." : rootCause)}");
                        Console.ResetColor();
                    }
                    if (errPat != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"      pattern: {errPat}");
                        Console.ResetColor();
                    }
                    if (components.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"      components: {string.Join(", ", components)}");
                        Console.ResetColor();
                    }
                    if (affCmds.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"      affected: {string.Join(", ", affCmds)}");
                        Console.ResetColor();
                    }
                    if (healScript != null)
                    {
                        Console.ForegroundColor = healResult != null ? ConsoleColor.Green : ConsoleColor.DarkYellow;
                        Console.WriteLine($"      heal: {healScript}{(healResult != null ? $" -> {healResult}" : " (pending)")}");
                        Console.ResetColor();
                    }
                }
                else
                {
                    // -- Regular suggestion display ------------------------------------
                    var text  = d?["text"]?.GetValue<string>()  ?? "";
                    var files = d?["files"]?.AsArray();

                    var textLines = text.Split('\n');
                    var title = textLines[0];
                    var body  = textLines.Length > 1 ? string.Join(" ", textLines[1..].Where(l => l.Trim().Length > 0)) : "";
                    if (title.Length > 70) title = title[..70] + "...";
                    if (body.Length > 80)  body  = body[..80]  + "...";

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
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"      ts={tsPrefix}");
                Console.ResetColor();
            }
            catch { Console.WriteLine($"  {i + 1}. [parse error]"); }
        }

        Console.WriteLine(new string('─', 100));
        Console.WriteLine($"  resolve: wkappbot suggest resolve <ts> \"note\" --i-completed-... <test.sh>");
        Console.WriteLine($"  merge:   wkappbot suggest merge --all-matching \"pattern\" --title \"title\" --work \"1h\"");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  >> 쉬운 것 / 긴급한 것부터 즉시 작업 시작! 중복은 merge로 정리하세요.");
        Console.ResetColor();
        return 0;
    }

    /// <summary>suggest show &lt;n|ts&gt; -- show full content of suggestion by list number or ts prefix.</summary>
    // Hand off a suggest entry to a local AI CLI for analysis + fix proposal.
    // Designed for the Codex CLI's local-code strength but symmetric with
    // claude-cli. Does NOT resolve the suggest -- that still requires the
    // existing evidence + skill + confirmation guard. This is the triage
    // step: "look at this backlog item and tell me if it's actionable".
    static int SuggestDelegateCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest delegate <n|ts> [--to codex|claude]");
            Console.WriteLine("  Hand off a suggest to a local AI CLI for analysis + fix proposal.");
            Console.WriteLine("  Default: codex (strong at local code work).");
            Console.WriteLine("  Does NOT resolve the suggest -- use `suggest resolve` after manual verification.");
            return 0;
        }

        var query = args[0];
        var target = "codex";
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--to" && i + 1 < args.Length)
                target = args[i + 1].ToLowerInvariant();
        }

        var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(jsonlPath))
        {
            Console.Error.WriteLine("[SUGGEST] No suggestions.jsonl");
            return 1;
        }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        var entries = lines
            .Select(l => System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(l)!)
            .Where(e => e["review_status"]?.GetValue<string>() != "done"
                     && e["status"]?.GetValue<string>() != "done")
            .ToList();

        System.Text.Json.Nodes.JsonNode? entry = null;
        if (int.TryParse(query, out var n) && n >= 1 && n <= entries.Count)
            entry = entries[n - 1];
        else
            entry = entries.FirstOrDefault(e => (e["ts"]?.GetValue<string>() ?? "").StartsWith(query));

        if (entry == null)
        {
            Console.Error.WriteLine($"[SUGGEST] Not found: {query}");
            Console.Error.WriteLine($"  Run 'suggest list' to see numbers. Total: {entries.Count}");
            return 1;
        }

        var ts = entry["ts"]?.GetValue<string>() ?? "";
        var text = entry["text"]?.GetValue<string>() ?? "";
        var title = entry["title"]?.GetValue<string>() ?? "";

        var prompt =
$@"You are reviewing a backlog suggest/bug-report for the WKAppBot repo at D:/GitHub/WKAppBot/.
Read the suggest below, investigate the relevant source if you have repo access, and respond with ONE of:
  (a) A concrete code fix: file path, line range, current-vs-new diff, and a 1-line rationale.
  (b) A clarifying question if the suggest is ambiguous.
  (c) ""Not actionable: <reason>"" if the issue is already fixed, out of scope, or unreproducible.
Keep the response tight (under 250 words). No preamble.

--- SUGGEST {ts} ---
{(string.IsNullOrEmpty(title) ? "" : $"TITLE: {title}\n")}{text}
---";

        Console.Error.WriteLine($"[SUGGEST:DELEGATE] ts={ts} -> {target}");
        return target switch
        {
            "codex" => AskCodexCli(prompt, newSession: true),
            "claude" or "claude-cli" => AskClaudeCli(prompt, newSession: true),
            _ => Error($"Unknown --to target: {target} (use codex or claude)")
        };
    }

    static int SuggestShowCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest show <n|ts>");
            Console.WriteLine("  n  : list number (1-based, from 'suggest list')");
            Console.WriteLine("  ts : timestamp prefix (e.g. 2026-04-02T01)");
            return 0;
        }

        var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(jsonlPath)) { Console.Error.WriteLine("[SUGGEST] No suggestions.jsonl"); return 1; }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        var entries = lines
            .Select(l => System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(l)!)
            .Where(e => e["review_status"]?.GetValue<string>() != "done"
                     && e["status"]?.GetValue<string>() != "done")
            .ToList();

        System.Text.Json.Nodes.JsonNode? target = null;
        var query = args[0];

        if (int.TryParse(query, out var n) && n >= 1 && n <= entries.Count)
        {
            target = entries[n - 1];
        }
        else
        {
            target = entries.FirstOrDefault(e => (e["ts"]?.GetValue<string>() ?? "").StartsWith(query));
        }

        if (target == null)
        {
            Console.Error.WriteLine($"[SUGGEST] Not found: {query}");
            Console.Error.WriteLine($"  Run 'suggest list' to see numbers. Total: {entries.Count}");
            return 1;
        }

        var ts      = target["ts"]?.GetValue<string>() ?? "";
        var from    = target["from"]?.GetValue<string>() ?? "";
        var text    = target["text"]?.GetValue<string>() ?? "";
        var status  = target["status"]?.GetValue<string>() ?? "pending";
        var slackTs = target["slack_ts"]?.GetValue<string>();
        var title   = target["title"]?.GetValue<string>();

        Console.Error.WriteLine($"[SUGGEST] ts={ts}  from=[{from}]  status={status}");
        if (slackTs != null) Console.WriteLine($"          slack_ts={slackTs}");
        if (title != null) { Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"  {title}"); Console.ResetColor(); }
        Console.WriteLine();
        Console.WriteLine(text);
        Console.WriteLine();
        Console.WriteLine($"  resolve: wkappbot suggest resolve {ts} \"note\" --i-completed-... <evidence.sh>");
        return 0;
    }

    /// <summary>suggest repost [ts] -- re-post entries missing slack_ts and write the new ID back.</summary>
    static int SuggestRepostCommand(string[] args)
    {
        var tsPrefix = args.Length > 0 ? args[0] : null;

        var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
        var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");

        if (!File.Exists(jsonlPath))
        {
            Console.Error.WriteLine("[REPOST] suggestions.jsonl not found");
            return 1;
        }

        var repostCfg = LoadSlackConfig();
        var botToken = repostCfg?["bot_token"]?.GetValue<string>();
        var channel  = repostCfg?["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.Error.WriteLine("[REPOST] No Slack config -- cannot send to Slack");
            return 1;
        }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        bool changed = false;
        int reposted = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            JsonNode? node;
            try { node = JsonSerializer.Deserialize<JsonNode>(lines[i]); } catch { continue; }
            if (node == null) continue;

            var entryTs = node["ts"]?.GetValue<string>() ?? "";
            var reviewStatus = node["review_status"]?.GetValue<string>() ?? "";
            var existingSlackTs = node["slack_ts"]?.GetValue<string>();

            // Skip resolved entries
            if (reviewStatus == "done") continue;
            // If ts filter given, skip non-matching
            if (tsPrefix != null && !entryTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase)) continue;
            // Skip if already has slack_ts (unless explicit ts filter)
            if (tsPrefix == null && existingSlackTs != null) continue;

            var text = node["text"]?.GetValue<string>() ?? "";
            var from = node["from"]?.GetValue<string>() ?? "unknown";
            var preview = text.Split('\n')[0];
            if (preview.Length > 60) preview = preview[..60] + "...";
            Console.Error.WriteLine($"[REPOST] {entryTs[..16]} [{from}] {preview}");

            var msgLines = new List<string> { $":memo: *[건의]* from `{from}`" };
            foreach (var part in text.Split('\n'))
                msgLines.Add(part);
            var slackMsg = string.Join("\n", msgLines);
            var (header, overflow) = SplitMessageForChannel(slackMsg);
            var senderName = GetSuggestBypassUsername();

            var (ok, newTs) = SlackSendViaApi(botToken, channel, header, username: senderName).GetAwaiter().GetResult();
            if (!ok || string.IsNullOrEmpty(newTs))
            {
                Console.Error.WriteLine($"[REPOST] Slack send failed for {entryTs}");
                continue;
            }

            // Post overflow
            if (overflow != null)
            {
                foreach (var chunk in ChunkText(overflow, 3900))
                    SlackSendViaApi(botToken, channel, chunk, threadTs: newTs, username: senderName).GetAwaiter().GetResult();
            }

            Console.Error.WriteLine($"[REPOST] Sent -> slack_ts={newTs}");

            // Write slack_ts back into the line
            var obj = JsonSerializer.Deserialize<Dictionary<string, object?>>(lines[i])!;
            obj["slack_ts"] = (object?)newTs;
            lines[i] = JsonSerializer.Serialize(obj);
            changed = true;
            reposted++;
        }

        if (changed)
            File.WriteAllLines(jsonlPath, lines);

        Console.WriteLine(reposted > 0 ? $"[REPOST] Done: {reposted} reposted, slack_ts saved." : "[REPOST] Nothing to repost.");
        return 0;
    }

    // -- Encoding Risk Guard ----------------------------------------------------
    // Returns true (and prints error) if text contains risky (Korean/emoji)
    // characters above the app-appropriate threshold. Korean in AI-facing text
    // costs 2-3x tokens; emojis corrupt in CP949 builds.
    // Threshold policy:
    //   - Default:      >0.1% triggers (English-first rule)
    //   - Korean-UI apps (hts-*, heroes*, kiwoom*, tuhon*, xingq*, invest-kr,
    //     claude-desktop): >30% -- these skills document real Korean UIA
    //     labels / menu text verbatim, so some Korean is expected and required.
    internal static bool HasEncodingRisk(string text, string context, string? app = null)
    {
        if (string.IsNullOrEmpty(text)) return false;
        int total = text.Length;
        int risky = 0;
        foreach (var ch in text)
        {
            // Korean syllables / Jamo
            if (ch >= '\uAC00' && ch <= '\uD7A3') { risky++; continue; }
            if (ch >= '\u1100' && ch <= '\u11FF') { risky++; continue; }
            if (ch >= '\u3130' && ch <= '\u318F') { risky++; continue; }
            // Emoji blocks (BMP dingbats, misc symbols)
            if (ch >= '\u2600' && ch <= '\u27BF') { risky++; continue; }
            // Supplementary emoji via surrogate pairs
            if (char.IsHighSurrogate(ch)) { risky++; continue; }
        }
        double ratio = (double)risky / total;

        // Apps that legitimately document Korean UIA labels. For these, we
        // still warn users away from prose-Korean, but allow enough Korean
        // to transcribe menu/button labels without --i-acknowledge flag.
        bool koreanUiApp = app != null && (
            app.StartsWith("hts-", StringComparison.OrdinalIgnoreCase)
            || app.StartsWith("heroes", StringComparison.OrdinalIgnoreCase)
            || app.StartsWith("kiwoom", StringComparison.OrdinalIgnoreCase)
            || app.StartsWith("tuhon",  StringComparison.OrdinalIgnoreCase)
            || app.StartsWith("xingq",  StringComparison.OrdinalIgnoreCase)
            || app.Equals("invest-kr",  StringComparison.OrdinalIgnoreCase)
            || app.Equals("claude-desktop", StringComparison.OrdinalIgnoreCase));
        double threshold = koreanUiApp ? 0.30 : 0.001;

        if (ratio > threshold)
        {
            var pctStr = (threshold * 100).ToString(threshold < 0.01 ? "F1" : "F0");
            Console.Error.WriteLine($"[ENCODING-GUARD] {context}: {risky}/{total} risky chars ({ratio:P1}) > {pctStr}% threshold{(koreanUiApp ? $" (Korean-UI app '{app}' has relaxed limit)" : "")}.");
            Console.Error.WriteLine($"  Korean/emoji in AI-facing text = 2-3x token waste + CP949 corruption risk.");
            Console.Error.WriteLine($"  Write in English. Use --i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste to bypass.");
            return true;
        }
        return false;
    }

}
