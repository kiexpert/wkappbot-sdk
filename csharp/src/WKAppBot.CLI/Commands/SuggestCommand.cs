using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// partial class: wkappbot suggest "건의 내용" [file.png] — slack send to current channel
internal partial class Program
{

    static int SuggestCommand(string[] args)
    {
        if (args.Length > 0 && args[0] is "resolve")
            return SuggestResolveCommand(args[1..]);
        if (args.Length > 0 && args[0] is "list" or "ls")
            return SuggestListCommand(args[1..]);
        if (args.Length > 0 && args[0] is "repost")
            return SuggestRepostCommand(args[1..]);

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
            Console.WriteLine("  repost [ts]            Re-post to Slack entries missing slack_ts, write ID back");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  wkappbot suggest \"Magnifier doesn't appear when form opens\"");
            Console.WriteLine("  wkappbot suggest \"Need UIA support for MDI child windows\" screenshot.png");
            Console.WriteLine("  wkappbot suggest list");
            Console.WriteLine("  wkappbot suggest resolve 2026-03-17T05:00:00 \"Fixed in v4.4.5\"");
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

        // Send via normal slack send channel (LoadSlackConfig — same as 'wkappbot slack send')
        var config = LoadSlackConfig();
        var botToken = config?["bot_token"]?.GetValue<string>();
        var channel  = config?["channel"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
        {
            var senderName = GetSendReplyUsername();
            var (ok, ts) = PostWithOverflow(botToken, channel, slackMsg, username: senderName);
            if (ok)
            {
                messageTs = ts;
                Console.WriteLine($"[SUGGEST] Slack sent (+{files.Count} file(s))");
                foreach (var f in files)
                {
                    Console.WriteLine($"[SUGGEST] Uploading {Path.GetFileName(f)}...");
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
            Console.Error.WriteLine("[SUGGEST] No Slack config found — saved locally only (run 'wkappbot slack setup' to configure)");
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
        var entries = lines
            .Select(l => System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(l)!)
            .Where(e => e["review_status"]?.GetValue<string>() != "done") // skip resolved items
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

    /// <summary>suggest repost [ts] — re-post entries missing slack_ts and write the new ID back.</summary>
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
            Console.Error.WriteLine("[REPOST] No Slack config — cannot send to Slack");
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
            if (preview.Length > 60) preview = preview[..60] + "…";
            Console.WriteLine($"[REPOST] {entryTs[..16]} [{from}] {preview}");

            var msgLines = new List<string> { $":memo: *[건의]* from `{from}`" };
            foreach (var part in text.Split('\n'))
                msgLines.Add(part);
            var slackMsg = string.Join("\n", msgLines);
            var (header, overflow) = SplitMessageForChannel(slackMsg);
            var senderName = GetSendReplyUsername();

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

            Console.WriteLine($"[REPOST] Sent → slack_ts={newTs}");

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

        // ── Resolve guard: require confirmation flag + evidence script ──
        // Flag format: --i-completed-...-and-willkim-allowed-this-script <file>
        // The script/log/screenshot is REQUIRED — no evidence = no resolve.
        // Uploaded to Slack thread as proof of testing.
        const string ConfirmFlag = "--i-completed-the-code-and-built-successfully-and-deployed-and-tested-with-real-scenarios-and-confirmed-meaningful-results-and-have-evidence-and-willkim-allowed-this-script";
        string? evidenceFile = null;
        bool hasConfirm = false;
        for (int ei = 0; ei < args.Length; ei++)
        {
            if (args[ei] == ConfirmFlag && ei + 1 < args.Length)
            {
                hasConfirm = true;
                evidenceFile = args[ei + 1];
                break;
            }
        }

        if (!hasConfirm || evidenceFile == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️  RESOLVE GUARD — no resolve without test evidence!");
            Console.WriteLine();
            Console.WriteLine("  Required: confirmation flag + test script/log file");
            Console.WriteLine("  The evidence file is uploaded to Slack as proof of testing.");
            Console.WriteLine();
            Console.WriteLine($"  wkappbot suggest resolve <ts> \"note\" {ConfirmFlag} test_result.sh");
            Console.ResetColor();
            return 1;
        }

        if (!File.Exists(evidenceFile))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ❌ Evidence file not found: {evidenceFile}");
            Console.ResetColor();
            return 1;
        }

        // Validate filename convention: test-{cmd}-{subcmd}-{desc}.{ext}
        var evidenceName = Path.GetFileNameWithoutExtension(evidenceFile);
        var evidenceParts = evidenceName.Split('-');
        if (evidenceParts.Length < 3 || !evidenceParts[0].Equals("test", StringComparison.OrdinalIgnoreCase))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ❌ Evidence filename must follow: test-{{cmd}}-{{subcmd}}-{{description}}.sh");
            Console.WriteLine($"     Got: {Path.GetFileName(evidenceFile)}");
            Console.WriteLine($"     Example: test-a11y-wait-condition.sh, test-file-edit-korean.sh");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine($"[RESOLVE] Evidence: {evidenceFile} ({new FileInfo(evidenceFile).Length} bytes)");

        // Show existing test scripts for this cmd/subcmd (help newcomer Claudes learn from predecessors)
        {
            var cmd1 = evidenceParts.Length > 1 ? evidenceParts[1] : "";
            var sub1 = evidenceParts.Length > 2 ? evidenceParts[2] : "";
            var existingDir = Path.Combine(DataDir, "experience", "tests", cmd1, sub1);
            if (Directory.Exists(existingDir))
            {
                var existing = Directory.GetFiles(existingDir).Select(Path.GetFileName).ToArray();
                if (existing.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"  📚 Existing tests for {cmd1}/{sub1}: {string.Join(", ", existing)}");
                    Console.ResetColor();
                }
            }
        }

        // Verify script content: must contain the command from filename (test-{cmd}-{subcmd}-*)
        try
        {
            var cmd2 = evidenceParts.Length > 1 ? evidenceParts[1] : "";
            var subcmd2 = evidenceParts.Length > 2 ? evidenceParts[2] : "";
            var scriptContent = File.ReadAllText(evidenceFile);
            var expectedCmd = $"{cmd2} {subcmd2}".Trim(); // e.g. "a11y wait", "file edit"
            if (!string.IsNullOrEmpty(expectedCmd) && !scriptContent.Contains(expectedCmd, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ❌ Script doesn't contain \"{expectedCmd}\" — filename says test-{cmd2}-{subcmd2} but command not used!");
                Console.ResetColor();
                return 1;
            }
        }
        catch { }

        // Duplicate guard: reject if same filename in experience DB has >50% line overlap
        try
        {
            var cmd3 = evidenceParts.Length > 1 ? evidenceParts[1] : "misc";
            var sub3 = evidenceParts.Length > 2 ? evidenceParts[2] : "general";
            var existDir = Path.Combine(DataDir, "experience", "tests", cmd3, sub3);
            if (Directory.Exists(existDir))
            {
                var newLines = File.ReadAllLines(evidenceFile).Where(l => l.Trim().Length > 0).ToArray();
                foreach (var existing in Directory.GetFiles(existDir))
                {
                    var oldLines = File.ReadAllLines(existing).Where(l => l.Trim().Length > 0).ToArray();
                    if (oldLines.Length == 0 || newLines.Length == 0) continue;
                    int overlap = newLines.Count(nl => oldLines.Any(ol => ol.Trim() == nl.Trim()));
                    double ratio = (double)overlap / Math.Max(newLines.Length, oldLines.Length);
                    if (ratio > 0.5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"  ❌ Evidence too similar to existing {Path.GetFileName(existing)} ({ratio:P0} overlap)");
                        Console.WriteLine($"     Write a NEW test specific to this fix, don't reuse old scripts!");
                        Console.ResetColor();
                        return 1;
                    }
                }
            }
        }
        catch { }

        // Run evidence script — must pass (exit 0) to allow resolve
        var ext = Path.GetExtension(evidenceFile).ToLowerInvariant();
        var shell = ext switch
        {
            ".sh"  => "bash", // resolved below with full path
            ".ps1" => "powershell -NoProfile -ExecutionPolicy Bypass -File",
            ".bat" or ".cmd" => "cmd /c",
            ".py"  => "python",
            ".js"  => "node",
            _ => null
        };
        if (shell != null)
        {
            Console.WriteLine($"[RESOLVE] Running evidence script ({ext} → {shell.Split(' ')[0]})...");
            try
            {
                // Resolve bash to absolute path (Git Bash) to avoid PATH issues in Eye environment
                var resolvedShell = shell;
                if (resolvedShell == "bash" && File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe"))
                    resolvedShell = @"C:\Program Files\Git\usr\bin\bash.exe";
                var shellParts = resolvedShell.Contains(' ') && !resolvedShell.Contains('"')
                    ? new[] { resolvedShell } // full path with spaces — don't split
                    : resolvedShell.Split(' ', 2);
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = shellParts[0],
                    Arguments = (shellParts.Length > 1 ? shellParts[1] + " " : "") + $"\"{evidenceFile}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                // ── CMD execution guard: capture OutputDebugString in real-time during script run ──
                // wkappbot-core mirrors all stderr → OutputDebugStringW([WKBOT/{pid}] ...).
                // [CMD] lines appear on every command invocation → grep-only scripts produce 0 hits.
                var dbgCapture = new System.Text.StringBuilder();
                var dbgListener = new DbgViewListener();
                dbgListener.MessageReceived += (pid, msg) => { lock (dbgCapture) dbgCapture.AppendLine(msg); };
                bool dbgStarted = dbgListener.TryStart();

                var proc = System.Diagnostics.Process.Start(psi);
                var stdoutTask = Task.Run(() => proc?.StandardOutput.ReadToEnd() ?? "");
                var stderrTask = Task.Run(() => proc?.StandardError.ReadToEnd() ?? "");
                proc?.WaitForExit(120_000);
                var exitCode = proc?.ExitCode ?? 1;
                var stdout = stdoutTask.GetAwaiter().GetResult();
                var stderr = stderrTask.GetAwaiter().GetResult();

                dbgListener.Dispose();
                var capturedDbg = dbgCapture.ToString();

                Console.WriteLine(stdout);
                if (!string.IsNullOrWhiteSpace(stderr)) Console.Error.WriteLine(stderr);

                if (exitCode != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ❌ Evidence script FAILED (exit={exitCode}). Fix the test before resolving!");
                    Console.ResetColor();
                    return 1;
                }

                // Derive expected cmd pattern from script filename: test-{cmd}-{subcmd}-*.sh → "{cmd}-{subcmd}"
                var scriptBaseName = Path.GetFileNameWithoutExtension(evidenceFile); // e.g. test-web-open-real
                var scriptParts = scriptBaseName.Split('-');
                var expectedCmdPattern = scriptParts.Length >= 3 ? $"{scriptParts[1]}-{scriptParts[2]}" : null; // e.g. "web-open"

                var dbgLines = capturedDbg.Split('\n');
                var cmdCount = dbgLines.Count(l => l.Contains("[CMD]"));
                bool cmdPatternHit = expectedCmdPattern == null
                    || dbgLines.Any(l => l.Contains($"-{expectedCmdPattern}>"));

                if (!dbgStarted)
                {
                    Console.WriteLine("  [RESOLVE] CMD guard: dbg listener unavailable (another listener active) — skipped");
                }
                else if (cmdCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ❌ CMD execution guard FAILED: no [CMD] entries in debug output during script run.");
                    Console.WriteLine("     테스트 스크립트는 실제 wkappbot 명령을 실행해야 합니다 (grep-only 테스트 불가).");
                    Console.ResetColor();
                    return 1;
                }
                else if (!cmdPatternHit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ❌ CMD execution guard FAILED: expected '{expectedCmdPattern}' command in debug output, not found.");
                    Console.WriteLine($"     스크립트 파일명 test-{scriptParts[1]}-{scriptParts[2]}-*.sh → 'wkappbot {scriptParts[1]} {scriptParts[2]}' 명령을 실제 실행해야 합니다.");
                    Console.ResetColor();
                    return 1;
                }
                else
                {
                    Console.WriteLine($"  [RESOLVE] CMD execution verified: {cmdCount} command(s) captured, pattern '-{expectedCmdPattern ?? "any"}>' confirmed");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  ✅ Evidence script PASSED (exit=0)");
                Console.WriteLine();
                Console.WriteLine("  All checks passed! Deploy (publish core+launcher if changed), update CLAUDE.md/MEMORY.md/usage/MCP docs, then push — by WillKim");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ❌ Evidence script execution error: {ex.Message}");
                Console.ResetColor();
                return 1;
            }
        }
        else
        {
            Console.WriteLine($"[RESOLVE] Non-executable evidence ({ext}) — skip execution, upload only");
        }

        // Regression test: re-run all previously stored scripts in experience/tests/{cmd}/{subcmd}/
        // Blocks resolve on failure — fix the regression or update the failing test first.
        if (RunRegressionTests(evidenceFile) != 0) return 1;

        args = args.Where(a => a != ConfirmFlag && a != evidenceFile).ToArray();

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
            ["review_ts"] = DateTime.UtcNow.ToString("o"),
            ["evidence_file"] = Path.GetFileName(evidenceFile)
        };
        var resolvedJson = JsonSerializer.Serialize(resolvedObj);

        // Move: append to history, remove from active
        File.AppendAllText(historyPath, resolvedJson + Environment.NewLine);
        lines.RemoveAt(matchIdx);
        File.WriteAllLines(jsonlPath, lines);
        Console.WriteLine($"[RESOLVE] Moved to history: {note}");

        // Copy evidence to experience DB: experience/tests/{cmd}/{subcmd}/{filename}
        // Filename convention: test-{cmd}-{subcmd}-{desc}.sh → folder: tests/{cmd}/{subcmd}/
        try
        {
            var parts = Path.GetFileNameWithoutExtension(evidenceFile).Split('-');
            // parts[0]=test, parts[1]=cmd, parts[2]=subcmd, parts[3..]=desc
            var cmd = parts.Length > 1 ? parts[1] : "misc";
            var subcmd = parts.Length > 2 ? parts[2] : "general";
            var testsDir = Path.Combine(hqDir, "experience", "tests", cmd, subcmd);
            Directory.CreateDirectory(testsDir);
            var destName = Path.GetFileName(evidenceFile);
            var destPath = Path.Combine(testsDir, destName);
            // Avoid overwriting: if file exists with different content, add timestamp suffix
            if (File.Exists(destPath))
            {
                var existingHash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(destPath)))[..8];
                var newHash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(evidenceFile)))[..8];
                if (existingHash != newHash)
                {
                    var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    var destExt = Path.GetExtension(destName);
                    destName = $"{Path.GetFileNameWithoutExtension(destName)}-{ts}{destExt}";
                    destPath = Path.Combine(testsDir, destName);
                }
                // else: same content — overwrite is fine (idempotent)
            }
            File.Copy(evidenceFile, destPath, overwrite: true);
            Console.WriteLine($"[RESOLVE] Evidence → experience/tests/{cmd}/{subcmd}/{Path.GetFileName(evidenceFile)}");
        }
        catch (Exception ex) { Console.Error.WriteLine($"[RESOLVE] Evidence copy failed: {ex.Message}"); }

        // Slack reply if slack_ts is available — uses shared SlackSendViaApi (same as 'slack reply')
        if (!string.IsNullOrEmpty(slackTs))
        {
            var resolveCfg = LoadSlackConfig();
            var botToken = resolveCfg?["bot_token"]?.GetValue<string>();
            var channel  = resolveCfg?["channel"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
            {
                var replyText = $":white_check_mark: *RESOLVED* — {note}\n:page_facing_up: Evidence: `{Path.GetFileName(evidenceFile)}`";
                var resolverName = GetSendReplyUsername();

                // 1. Text reply with reply_broadcast → visible in channel + thread
                var (textOk, _) = SlackSendViaApi(botToken, channel, replyText,
                    threadTs: slackTs, username: resolverName, replyBroadcast: true).GetAwaiter().GetResult();
                if (textOk)
                    Console.WriteLine($"[RESOLVE] Slack reply sent to thread {slackTs} (+ channel broadcast)");
                else
                    Console.Error.WriteLine($"[RESOLVE] Slack reply failed");

                // 2. Upload evidence file to thread (thread-only, no broadcast)
                try
                {
                    SlackUploadFileAsync(botToken, channel, evidenceFile,
                        title: Path.GetFileName(evidenceFile), threadTs: slackTs).GetAwaiter().GetResult();
                    Console.WriteLine($"[RESOLVE] Evidence uploaded to thread");
                }
                catch (Exception ex) { Console.Error.WriteLine($"[RESOLVE] Evidence upload failed: {ex.Message}"); }
            }
            else
            {
                Console.Error.WriteLine("[RESOLVE] No Slack config — Slack reply skipped");
            }
        }
        else
        {
            Console.WriteLine("[RESOLVE] No slack_ts recorded — Slack reply skipped");
        }

        return 0;
    }

    /// <summary>
    /// Re-run all previously stored test scripts for the same cmd/subcmd as the evidence file.
    /// Returns 0 if all pass, 1 if any fail (resolve is blocked until fixed).
    /// To fix: update the failing test script or fix the regression in code.
    /// </summary>
    static int RunRegressionTests(string evidenceFile)
    {
        try
        {
            var parts = Path.GetFileNameWithoutExtension(evidenceFile).Split('-');
            var cmd = parts.Length > 1 ? parts[1] : "misc";
            var subcmd = parts.Length > 2 ? parts[2] : "general";
            var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
            var testsDir = Path.Combine(hqDir, "experience", "tests", cmd, subcmd);
            if (!Directory.Exists(testsDir)) return 0;

            var evidenceAbs = Path.GetFullPath(evidenceFile);
            var scripts = Directory.GetFiles(testsDir, "*.sh")
                .Where(f => !string.Equals(Path.GetFullPath(f), evidenceAbs, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f)
                .ToArray();
            if (scripts.Length == 0) return 0;

            Console.WriteLine($"\n[REGRESSION] Running {scripts.Length} existing test(s) for {cmd}/{subcmd}...");
            var bashExe = File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe")
                ? @"C:\Program Files\Git\usr\bin\bash.exe" : "bash";

            int passed = 0, failed = 0;
            var failures = new List<(string name, string path)>();
            foreach (var script in scripts)
            {
                var name = Path.GetFileName(script);
                Console.Write($"  {name}... ");
                try { Console.Out.Flush(); } catch { }
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = bashExe,
                        Arguments = $"\"{script}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    };
                    // WKAPPBOT_WORKER=1: prevents nested wkappbot calls from connecting to Eye pipe
                    // (avoids deadlock when suggest resolve is already running inside Eye)
                    psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";
                    var proc = System.Diagnostics.Process.Start(psi);
                    // Stream output live to console (prevents pipe-buffer deadlock + shows progress)
                    var rOut = Task.Run(() => {
                        string? line;
                        while ((line = proc?.StandardOutput.ReadLine()) != null)
                        { Console.WriteLine($"    {line}"); try { Console.Out.Flush(); } catch { } }
                    });
                    var rErr = Task.Run(() => {
                        string? line;
                        while ((line = proc?.StandardError.ReadLine()) != null)
                            Console.Error.WriteLine($"    {line}");
                    });
                    proc?.WaitForExit(60_000);
                    rOut.Wait(3_000); rErr.Wait(1_000);
                    var exitCode = proc?.ExitCode ?? 1;
                    if (exitCode == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("PASS");
                        Console.ResetColor();
                        passed++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"FAIL (exit={exitCode})");
                        Console.ResetColor();
                        failed++;
                        failures.Add((name, script));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ERROR ({ex.Message})");
                    Console.ResetColor();
                    failed++;
                    failures.Add((name, script));
                }
            }

            if (failed == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[REGRESSION] All {passed} regression test(s) passed ✅");
                Console.ResetColor();
                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[REGRESSION] ❌ {failed}/{scripts.Length} regression test(s) FAILED — resolve BLOCKED");
                Console.ResetColor();
                Console.WriteLine($"  Fix the regression in code, or edit the failing test if the check is outdated.");
                Console.WriteLine($"  Edit commands:");
                foreach (var (name, path) in failures)
                    Console.WriteLine($"    wkappbot file edit \"old\" \"new\" \"{path.Replace('\\', '/')}\"");

                // Slack alert with edit hints
                try
                {
                    var regCfg = LoadSlackConfig();
                    var botToken = regCfg?["bot_token"]?.GetValue<string>();
                    var channel  = regCfg?["channel"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
                    {
                        var lines = new System.Text.StringBuilder();
                        lines.AppendLine($":x: *[REGRESSION BLOCKED]* `{cmd}/{subcmd}` — {failed}/{scripts.Length} test(s) failed:");
                        foreach (var (name, path) in failures)
                            lines.AppendLine($"• `{name}` — `wkedit \"old\" \"new\" \"{path.Replace('\\', '/')}\"` 로 수정");
                        lines.Append("_코드 버그 수정 or 구버전 테스트 wkedit 후 re-resolve_");
                        SlackSendViaApi(botToken, channel, lines.ToString()).GetAwaiter().GetResult();
                    }
                }
                catch { }
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[REGRESSION] Error running regression tests: {ex.Message}");
        }
        return 0;
    }

}
