using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: wkappbot slack route <msgJson>
// One-shot OnMessage delivery worker — stateless, fire-and-forget via EyeCmdPipeServer.DispatchBg.
// Receives a Slack message as JSON, finds matching prompt windows, injects text, sends ack.
// Can also be run standalone for testing (no Eye required for the delivery logic itself).
internal partial class Program
{
    static readonly string[] SlackRouteKeywords = ["클롣", "클롯", "클봇", "claude", "appbot", "wkappbot"];
    static readonly string[] SlackRouteNoise = ["NO_REPLY", "ㄱㄱ", "send ㄱㄱ"];
    const string RouteAckUsername = "앱봇아이";

    /// <summary>
    /// wkappbot slack route <msgJson>
    /// Delivers a Slack message to matched Claude/Codex prompt windows and sends ack.
    /// msgJson fields: text, user, ts, threadTs (nullable), channel
    /// </summary>
    static int SlackRouteCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: slack route --queue | slack route --file <path> | slack route <msgJson>");
            return 1;
        }

        // --queue: drain SlackQueueDir serially (Eye spawns this; no file arg needed)
        if (args[0] == "--queue")
        {
            var qp = new StepProfiler("route-queue");
            var queueDir = Path.Combine(DataDir, "runtime", "slack_queue");
            qp.Init($"start queueDir={queueDir}");
            if (!Directory.Exists(queueDir)) { qp.Line("dir missing — exit"); return 0; }
            var files = Directory.GetFiles(queueDir, "*.json");
            qp.Line($"found {files.Length} file(s)");
            if (files.Length == 0) return 0;
            Array.Sort(files);
            int processed = 0;
            foreach (var file in files)
            {
                var procFile = Path.ChangeExtension(file, ".processing");
                qp.Line($"claim → {Path.GetFileName(procFile)}");
                try { File.Move(file, procFile, overwrite: false); }
                catch (Exception ex) { qp.Line($"claim failed: {ex.Message}"); continue; }
                try
                {
                    var json = File.ReadAllText(procFile);
                    qp.Line($"routing {Path.GetFileName(file)} ({json.Length}B)");
                    SlackRouteCommand([json]);
                    processed++;
                    qp.Line($"done {Path.GetFileName(file)}");
                }
                catch (Exception ex) { qp.Line($"error: {ex.Message}"); }
                finally { try { File.Delete(procFile); qp.Line($"deleted {Path.GetFileName(procFile)}"); } catch { } }
            }
            qp.Finish($"drained {processed}/{files.Length}");
            return 0;
        }

        // --dry-run: trace routing without delivering or sending ack
        var argList = args.ToList();
        bool isDryRun = argList.Remove("--dry-run");

        // Support --file for single-message route (kept for tests / backward compat)
        string jsonStr;
        if (argList.Count >= 2 && argList[0] == "--file")
        {
            var filePath = argList[1];
            if (!File.Exists(filePath)) { Console.Error.WriteLine($"[ROUTE] File not found: {filePath}"); return 1; }
            jsonStr = File.ReadAllText(filePath);
        }
        else if (argList.Count >= 1)
        {
            jsonStr = argList[0];
        }
        else
        {
            Console.Error.WriteLine("Usage: slack route --queue | slack route [--dry-run] --file <path> | slack route [--dry-run] <msgJson>");
            return 1;
        }

        var node = JsonNode.Parse(jsonStr);
        if (node == null) { Console.Error.WriteLine("[ROUTE] Invalid JSON"); return 1; }

        var text       = node["text"]?.GetValue<string>() ?? "";
        var user       = node["user"]?.GetValue<string>() ?? "";
        var ts         = node["ts"]?.GetValue<string>() ?? "";
        var threadTs   = node["threadTs"]?.GetValue<string>();
        var channel    = node["channel"]?.GetValue<string>() ?? "";
        var retryCount = node["retryCount"]?.GetValue<int>() ?? 0;
        var eyeCwd     = node["eyeCwd"]?.GetValue<string>();
        var eyeBotName = node["botUsername"]?.GetValue<string>();
        var isReaction = node["isReaction"]?.GetValue<bool>() ?? false;
        var reaction   = node["reaction"]?.GetValue<string>();

        // Set Eye CWD/botUsername for routing functions (they read CallerCwd + BotUsername)
        if (!string.IsNullOrEmpty(eyeCwd))
        {
            EyeCmdPipeServer.CallerCwd.Value = eyeCwd;
            try { Environment.CurrentDirectory = eyeCwd; } catch { }
        }
        // Parse per-prompt display name map from Eye (hwnd → botUsername)
        var promptNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (node["promptNames"] is JsonObject pn)
        {
            foreach (var kv in pn)
                if (kv.Value != null) promptNameMap[kv.Key] = kv.Value.GetValue<string>();
        }

        var rp = new StepProfiler("route");
        rp.Init($"ts={ts} user={user} thread={threadTs ?? "none"} cwd={eyeCwd} bot={eyeBotName} promptNames={promptNameMap.Count}");

        if (!File.Exists(SlackConfigPath) && !isDryRun) { rp.Line("No Slack config — skip"); return 0; }
        var cfg = File.Exists(SlackConfigPath) ? JsonNode.Parse(File.ReadAllText(SlackConfigPath)) : null;
        var botToken = cfg?["bot_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) && !isDryRun) { rp.Line("No bot_token — skip"); return 0; }

        // Clean @mention tokens
        var cleanText = Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
        if (string.IsNullOrEmpty(cleanText)) { rp.Line("Empty text after clean — skip"); return 0; }

        // Noise filter (skip for reactions — they have no user text)
        if (!isReaction && SlackRouteNoise.Any(n => cleanText.Equals(n, StringComparison.OrdinalIgnoreCase)))
        {
            rp.Line($"Noise filter — skip: {cleanText}");
            return 0;
        }

        var textLower = text.ToLowerInvariant();

        // ╔══════════════════════════════════════════════════════════════════════╗
        // ║  ★ LEGACY ROUTING LOGIC — battle-tested. DO NOT MODIFY! ★         ║
        // ║                                                                    ║
        // ║  This routing logic survived dozens of production bug fixes.       ║
        // ║  Any "simplification" or "improvement" WILL introduce bugs.        ║
        // ║  If you need changes: only swap called functions (UIA→MCP etc).    ║
        // ║  NEVER change the flow, branches, or conditions themselves.        ║
        // ║                                                                    ║
        // ║  v4.9 lessons learned:                                             ║
        // ║  - "Simplify" OnMention → broadcast bug (all prompts got msg)      ║
        // ║  - Change thread condition → wrong Claude got the message          ║
        // ║  - Change catch-all to workspace-scoped → ping only reached 1/4   ║
        // ╚══════════════════════════════════════════════════════════════════════╝

        // ── Step 1: Determine delivery mode ──
        // threadTs present → thread-scoped routing (find owner Claude)
        // threadTs absent → keyword or full broadcast (ping etc)
        // ⚠ Changing this condition breaks thread replies (broadcast or lost)
        bool isTrackedThread = !string.IsNullOrEmpty(threadTs);

        // Keyword detection: channel messages only (not thread replies)
        bool isKeyword = !isTrackedThread &&
            SlackRouteKeywords.Any(kw => textLower.Contains(kw, StringComparison.OrdinalIgnoreCase));

        rp.Line($"step1: thread={isTrackedThread} kw={isKeyword} text={cleanText[..Math.Min(cleanText.Length, 60)]}");

        // ── Step 2: Collect thread context (previous messages for Claude) ──
        string threadContext = "";
        if (!string.IsNullOrEmpty(threadTs))
        {
            rp.Line($"step2: GetThreadContext channel={channel} thread={threadTs}");
            var ctx = GetThreadContext(botToken ?? "", channel, threadTs, ts);
            if (!string.IsNullOrEmpty(ctx)) threadContext = $"\n{ctx}\n";
            rp.Line($"step2: context len={threadContext.Length}");
        }

        // ── Step 3: Discover all prompt windows ──
        rp.Line("step3: FindAllPrompts");
        ClaudePromptHelper.AllowFocusSteal = true;
        using var promptHelper = new ClaudePromptHelper();
        var allPrompts = promptHelper.FindAllPrompts();
        rp.Line($"step3: found {allPrompts.Count} prompt(s) — {string.Join(", ", allPrompts.Select(p => $"0x{p.WindowHandle:X}({p.HostType})"))}");

        List<ClaudePromptHelper.PromptInfo> targets;
        string replyTs;
        string? label;

        // ── Step 4: Select target prompts (CRITICAL BRANCH) ──
        rp.Line($"step4: isTrackedThread={isTrackedThread} isKeyword={isKeyword} eyeBot=\"{eyeBotName}\"");
        if (isTrackedThread && !string.IsNullOrEmpty(threadTs))
        {
            // ★ Thread reply: find owning Claude via ResolveThreadScopedPrompts
            targets = ResolveThreadScopedPrompts(promptHelper, botToken ?? "", channel, threadTs, eyeBotName ?? BotUsername);
            rp.Line($"step4: ResolveThreadScoped → {targets.Count} match(es)");

            if (targets.Count == 0)
            {
                // Fallback 1: workspace CWD match (Eye's working directory)
                var own = ResolveWorkspaceScopedPrompt(promptHelper);
                if (own != null)
                {
                    targets = [own];
                    rp.Line($"step4: fallback1 → workspace: {ExtractProjectName(own)} 0x{own.WindowHandle:X}");
                }
                else
                {
                    // Fallback 2: appbot master (WKAppBot VS Code) — final receiver
                    var appbot = allPrompts.FirstOrDefault(p =>
                        p.WindowTitle.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) &&
                        p.HostType is "vscode-claudecode");
                    if (appbot != null)
                    {
                        targets = [appbot];
                        rp.Line($"step4: fallback2 → appbot 0x{appbot.WindowHandle:X}");
                    }
                    else
                    {
                        rp.Line($"step4: fallback2 — no appbot! allPrompts={allPrompts.Count}");
                    }
                }
            }
            replyTs = threadTs;
            label = "thread reply";
        }
        else
        {
            // ★ Non-thread (channel message): broadcast to ALL prompts
            targets = allPrompts;
            replyTs = threadTs ?? ts;
            if (isKeyword)
            {
                var kw = SlackRouteKeywords.First(k => textLower.Contains(k, StringComparison.OrdinalIgnoreCase));
                label = $"keyword:\"{kw}\"";
            }
            else
            {
                label = null; // catch-all → SlackSendSuffix
            }
            rp.Line($"step4: broadcast → {targets.Count} prompt(s) label={label ?? "catch-all"}");
        }

        // ── Step 5: 타겟 없음 → 재시도 큐 ──
        if (targets.Count == 0)
        {
            rp.Line($"step5: No targets — retry #{retryCount + 1}");
            RouteRetryQueue.Enqueue(node, retryCount + 1);
            return 0;
        }

        // ── Step 5(deliver): TypeAndSubmit to each target ──
        rp.Line($"step5: deliver to {targets.Count} target(s){(isDryRun ? " [DRY-RUN]" : "")}");
        int sent = 0;
        var results = new List<DeliveryResult>();
        foreach (var pi in targets)
        {
            var dispName = promptNameMap.TryGetValue($"0x{pi.WindowHandle.ToInt64():X}", out var n) ? n : ExtractProjectName(pi);
            rp.Line($"step5: → {dispName} 0x{pi.WindowHandle:X} host={pi.HostType}");
            if (isDryRun)
            {
                Console.WriteLine($"[DRY-RUN] would deliver to: {dispName} (0x{pi.WindowHandle:X} {pi.HostType})");
                results.Add(new DeliveryResult(dispName, true));
                sent++;
                continue;
            }
            try
            {
                var promptText = label == null
                    ? $"{cleanText}\n{SlackSendSuffix(user)}"
                    : $"{cleanText}{threadContext}\n{SlackReplySuffix(user, replyTs, label)}";

                var ok = ProbeAndSubmit(promptHelper, pi, promptText, ts);
                rp.Line($"step5: {dispName} → {(ok ? "OK" : "FAIL")}");
                results.Add(new DeliveryResult(dispName, ok));
                if (ok) sent++;
            }
            catch (Exception ex)
            {
                rp.Line($"step5: {dispName} → ERROR: {ex.Message}");
                results.Add(new DeliveryResult(dispName, false));
            }
        }

        rp.Line($"Delivered {sent}/{targets.Count}");

        if (isDryRun)
        {
            Console.WriteLine($"[DRY-RUN] routing complete: {sent}/{targets.Count} target(s)");
            rp.Finish("dry-run done");
            return 0;
        }

        if (sent == 0)
        {
            rp.Line($"All failed — retry #{retryCount + 1}");
            RouteRetryQueue.Enqueue(node, retryCount + 1);
            return 0;
        }

        // ── Send ack ──
        SendRouteAck(botToken!, channel, replyTs, sent, targets.Count, results);
        rp.Finish($"done sent={sent}/{targets.Count}");
        return 0;
    }

    /// <summary>
    /// Static ack sender for the route worker.
    /// Sends "전달했습니다" reply, deletes old ack, persists new ack ts to pending_acks.json.
    /// </summary>
    static void SendRouteAck(string botToken, string channel, string threadKey,
        int sent, int total, List<DeliveryResult>? results)
    {
        string ackText;
        if (total > 0 && sent < total)
            ackText = $":warning: 전달 {sent}/{total} (partial failure)";
        else if (results?.Count == 1)
            ackText = $"{results[0].ShortName}에 전달했습니다! {(results[0].Sent ? ":white_check_mark:" : ":x:")}";
        else if (sent > 1)
            ackText = $"{sent}곳에 전달했습니다!";
        else
            ackText = "전달했습니다!";

        if (results != null && results.Count > 1)
        {
            var lines = results.Select(r => $"• {r.ShortName} {(r.Sent ? ":white_check_mark:" : ":x:")}");
            ackText += "\n" + string.Join("\n", lines);
        }

        var (ackOk, ackTs) = Task.Run(async () =>
            await SlackSendViaApi(botToken, channel, ackText, threadKey, username: RouteAckUsername))
            .GetAwaiter().GetResult();

        if (ackOk && !string.IsNullOrEmpty(ackTs))
        {
            // Delete previous ack for this thread (if any)
            var oldAcks = LoadPendingAcks();
            if (oldAcks.TryGetValue(threadKey, out var oldAck))
                Task.Run(async () => await SlackDeleteMessageAsync(botToken, oldAck.Channel, oldAck.AckTs, guardThreadStarter: false)).Wait(3000);

            SavePendingAck(threadKey, channel, ackTs);
        }
        else
        {
            Console.WriteLine($"[ROUTE] Ack send failed (thread={threadKey})");
        }
    }

    /// <summary>
    /// wkappbot slack route-flush
    /// Processes all due retry items in the queue — intended for Windows Task Scheduler.
    /// Runs standalone (no Eye required). Eye-independent safety net.
    /// </summary>
    static int SlackRouteFlushCommand()
    {
        var dueItems = RouteRetryQueue.GetDueItems();
        if (dueItems.Count == 0)
        {
            Console.WriteLine("[ROUTE-FLUSH] No due items");
            return 0;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[ROUTE-FLUSH] Processing {dueItems.Count} due item(s)");
        Console.ResetColor();

        foreach (var item in dueItems)
            SlackRouteCommand([item]);

        return 0;
    }
}
