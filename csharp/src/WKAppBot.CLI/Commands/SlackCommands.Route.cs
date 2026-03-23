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
            Console.Error.WriteLine("Usage: slack route <msgJson> | slack route --file <path>");
            return 1;
        }

        // Support --file for process-separated route (avoids shell escaping)
        string jsonStr;
        if (args.Length >= 2 && args[0] == "--file")
        {
            var filePath = args[1];
            if (!File.Exists(filePath)) { Console.Error.WriteLine($"[ROUTE] File not found: {filePath}"); return 1; }
            jsonStr = File.ReadAllText(filePath);
        }
        else
        {
            jsonStr = args[0];
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

        if (!string.IsNullOrEmpty(eyeBotName))
            Console.WriteLine($"[ROUTE] Eye bot={eyeBotName} cwd={eyeCwd} promptNames={promptNameMap.Count}");

        if (!File.Exists(SlackConfigPath)) { Console.WriteLine("[ROUTE] No Slack config — skip"); return 0; }
        var cfg = JsonNode.Parse(File.ReadAllText(SlackConfigPath));
        var botToken = cfg?["bot_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken)) { Console.WriteLine("[ROUTE] No bot_token — skip"); return 0; }

        // Clean @mention tokens
        var cleanText = Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
        if (string.IsNullOrEmpty(cleanText)) return 0;

        // Noise filter
        if (SlackRouteNoise.Any(n => cleanText.Equals(n, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"[ROUTE] Noise filter — skip: {cleanText}");
            return 0;
        }

        var textLower = text.ToLowerInvariant();

        // ── Determine delivery mode ──
        // ANY thread reply (threadTs != null) → thread-scoped routing (find owner Claude)
        // Only non-threaded messages go to catch-all/keyword path
        bool isTrackedThread = !string.IsNullOrEmpty(threadTs);
        bool isKeyword = !isTrackedThread &&
            SlackRouteKeywords.Any(kw => textLower.Contains(kw, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"[ROUTE] from={user} thread={isTrackedThread} kw={isKeyword} text={cleanText[..Math.Min(cleanText.Length, 60)]}");

        // ── Build thread context ──
        string threadContext = "";
        if (!string.IsNullOrEmpty(threadTs))
        {
            var ctx = GetThreadContext(botToken, channel, threadTs, ts);
            if (!string.IsNullOrEmpty(ctx)) threadContext = $"\n{ctx}\n";
        }

        // ── Find prompts ──
        ClaudePromptHelper.AllowFocusSteal = true;
        using var promptHelper = new ClaudePromptHelper();
        var allPrompts = promptHelper.FindAllPrompts();

        List<ClaudePromptHelper.PromptInfo> targets;
        string replyTs;
        string? label;

        if (isTrackedThread && !string.IsNullOrEmpty(threadTs))
        {
            targets = ResolveThreadScopedPrompts(promptHelper, botToken, channel, threadTs, eyeBotName ?? BotUsername);
            if (targets.Count == 0)
            {
                // Fallback 1: workspace-scoped prompt (CWD match)
                var own = ResolveWorkspaceScopedPrompt(promptHelper);
                if (own != null)
                {
                    targets = [own];
                    Console.WriteLine($"[ROUTE] Thread fallback → workspace prompt: {ExtractProjectName(own)}");
                }
                else
                {
                    // Fallback 2: appbot VS Code prompt (WKAppBot CWD — always-on relay target)
                    var appbot = allPrompts.FirstOrDefault(p =>
                        p.WindowTitle.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) &&
                        p.HostType is "vscode-claudecode");
                    if (appbot != null)
                    {
                        targets = [appbot];
                        Console.WriteLine($"[ROUTE] Thread fallback → appbot VS Code: 0x{appbot.WindowHandle:X}");
                    }
                }
            }
            replyTs = threadTs;
            label = "thread reply";
        }
        else
        {
            // keyword or catch-all → workspace-scoped prompt first, then appbot CWD fallback
            var scoped = ResolveWorkspaceScopedPrompts(promptHelper);
            if (scoped.Count > 0)
            {
                targets = scoped;
            }
            else
            {
                // Fallback: find the appbot VS Code prompt (CWD = WKAppBot)
                var appbotPrompt = allPrompts.FirstOrDefault(p =>
                    p.WindowTitle.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase) &&
                    p.HostType is "vscode-claudecode");
                targets = appbotPrompt != null ? [appbotPrompt] : allPrompts;
                if (appbotPrompt != null)
                    Console.WriteLine($"[ROUTE] Fallback → appbot VS Code prompt: 0x{appbotPrompt.WindowHandle:X}");
            }
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
        }

        // ── No prompts found ──
        if (targets.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[ROUTE] No target prompts found — scheduling retry #{retryCount + 1} in 1 min");
            Console.ResetColor();
            RouteRetryQueue.Enqueue(node, retryCount + 1);
            return 0;
        }

        // ── Deliver ──
        int sent = 0;
        var results = new List<DeliveryResult>();
        foreach (var pi in targets)
        {
            try
            {
                var promptText = label == null
                    ? $"{cleanText}\n{SlackSendSuffix(user)}"
                    : $"{cleanText}{threadContext}\n{SlackReplySuffix(user, replyTs, label)}";

                var ok = ProbeAndSubmit(promptHelper, pi, promptText, ts);
                results.Add(new DeliveryResult(ExtractProjectName(pi), ok));
                if (ok) sent++;
            }
            catch (Exception ex)
            {
                results.Add(new DeliveryResult(ExtractProjectName(pi), false));
                Console.WriteLine($"[ROUTE] Delivery error for {ExtractProjectName(pi)}: {ex.Message}");
            }
        }

        Console.ForegroundColor = sent > 0 ? ConsoleColor.Cyan : ConsoleColor.Yellow;
        Console.WriteLine($"[ROUTE] Delivered {sent}/{targets.Count}");
        Console.ResetColor();

        if (sent == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[ROUTE] All deliveries failed — scheduling retry #{retryCount + 1} in 1 min");
            Console.ResetColor();
            RouteRetryQueue.Enqueue(node, retryCount + 1);
            return 0;
        }

        // ── Send ack ──
        SendRouteAck(botToken, channel, replyTs, sent, targets.Count, results);
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
        else if (sent > 1)
            ackText = $"Claude {sent}곳에 전달했습니다!";
        else
            ackText = "Claude에 전달했습니다!";

        if (results != null && results.Count > 0)
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
