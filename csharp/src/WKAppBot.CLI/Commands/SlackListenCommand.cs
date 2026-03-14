using System.Collections.Concurrent;
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

// partial class: Slack listen command + background launcher
internal partial class Program
{
    // Routing ownership: decide target host first, then pick prompt only from that host.
    // This prevents cross-agent misdelivery when multiple AI apps are open.
    static string GetStrictSlackTargetHost()
    {
        if (IsRunningFromCodexAppCertain()) return "codex";
        if (IsRunningFromClaudeApp()) return "claude";
        return "any";
    }

    static bool IsPromptForHost(ClaudePromptHelper.PromptInfo prompt, string host)
    {
        if (host == "codex")
            return string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
        if (host == "claude")
            return !string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
        return true;
    }

    // Routing policy for Slack listener:
    // 1) Try cwd-scoped prompt for this workspace
    // 2) Enforce host filter when host is certain (codex/claude)
    // 3) For "any", prefer codex then fallback
    static ClaudePromptHelper.PromptInfo? FindSlackPreferredPrompt(ClaudePromptHelper helper)
    {
        var targetHost = GetStrictSlackTargetHost();
        var codexAlive = Process.GetProcessesByName("codex").Length > 0;
        var effectiveHost = (targetHost == "any" && codexAlive) ? "codex" : targetHost;
        var cwd = Environment.CurrentDirectory;
        var cwdPrompt = helper.FindPromptForCwd(cwd);
        if (cwdPrompt != null && IsPromptForHost(cwdPrompt, effectiveHost))
            return cwdPrompt;

        var all = helper.FindAllPrompts();
        if (effectiveHost == "codex")
            return all.FirstOrDefault(p => string.Equals(p.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase));
        if (effectiveHost == "claude")
            return all.FirstOrDefault(p => !string.Equals(p.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase));

        // Safety rule: when Codex app is running, never fallback delivery to Claude.
        // If Codex prompt is not discoverable right now, return null and skip delivery.
        var codex = all.FirstOrDefault(p => p.HostType == "codex-desktop");
        if (codex != null) return codex;
        if (codexAlive) return null;

        return helper.FindPrompt();
    }

    /// <summary>Socket Mode: listen for events and respond to @mentions.</summary>
    static int SlackListenCommand(string[] args)
    {
        bool background = args.Contains("--bg") || args.Contains("--background") || args.Contains("-d");
        bool aiMode = args.Contains("--ai");
        // Prompt forwarding + keyword monitoring: ALWAYS ON (no flags)
        bool claudeMode = args.Contains("--claude");
        bool webBotMode = args.Contains("--webbot") || args.Contains("--web") || claudeMode;

        // --name: instance name for multi-bot identification (default: cwd folder name)
        string? explicitName = null;
        var nameIdx = Array.IndexOf(args, "--name");
        if (nameIdx >= 0 && nameIdx + 1 < args.Length)
            explicitName = args[nameIdx + 1];
        var instanceName = explicitName ?? Path.GetFileName(Environment.CurrentDirectory) ?? Environment.MachineName;

        // --bg: launch self as background process (pass flags through)
        if (background)
            return SlackLaunchBackground(aiMode: aiMode, webBotMode: webBotMode, claudeMode: claudeMode, instanceName: explicitName);

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var appToken = config["app_token"]?.GetValue<string>();
        var botToken = config["bot_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(appToken) || string.IsNullOrEmpty(botToken))
        {
            Console.WriteLine("[SLACK] ERROR: app_token or bot_token not found in config");
            return 1;
        }
        _displayNameBotToken = botToken; // enable display name resolution for suffix builders
        LoadKnownUsersFromConfig();      // pre-populate cache from webhook.json known_users

        // Initialize Claude AI if --ai flag
        ClaudeChat? ai = null;
        if (aiMode)
        {
            // Try config file → ANTHROPIC_API_KEY → CLAUDE_CODE_OAUTH_TOKEN (Claude Code session)
            var aiKey = config["anthropic_api_key"]?.GetValue<string>();
            if (string.IsNullOrEmpty(aiKey))
                aiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
            if (string.IsNullOrEmpty(aiKey))
                aiKey = Environment.GetEnvironmentVariable("CLAUDE_CODE_OAUTH_TOKEN");
            if (string.IsNullOrEmpty(aiKey))
            {
                Console.WriteLine("[SLACK] AI mode: no API key found (webhook.json / ANTHROPIC_API_KEY / CLAUDE_CODE_OAUTH_TOKEN)");
                Console.WriteLine("[SLACK] Falling back to echo mode");
            }
            else
            {
                try
                {
                    ai = new ClaudeChat(apiKey: aiKey);
                    Console.WriteLine("[SLACK] AI mode enabled (Claude API)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SLACK] AI mode unavailable: {ex.Message}");
                    Console.WriteLine("[SLACK] Falling back to echo mode");
                }
            }
        }

        // Always initialize Claude prompt helper (always forward to Claude)
        var promptHelper = new ClaudePromptHelper();
        var promptMissStreak = 0;
        var promptLostNotifyCooldown = TimeSpan.FromSeconds(20);
        var lastPromptLostNotifyUtc = DateTime.MinValue;

        List<ClaudePromptHelper.PromptInfo> ResolvePromptsWithRecovery()
        {
            var targetHost = GetStrictSlackTargetHost();
            var codexAlive = Process.GetProcessesByName("codex").Length > 0;
            var effectiveHost = (targetHost == "any" && codexAlive) ? "codex" : targetHost;

            var prompts = promptHelper.FindAllPrompts()
                .Where(p => IsPromptForHost(p, effectiveHost))
                .GroupBy(p => p.WindowHandle)
                .Select(g => g.First())
                .ToList();
            if (prompts.Count > 0)
            {
                if (promptMissStreak >= 3)
                    Console.WriteLine($"[SLACK] Prompt recovered after {promptMissStreak} miss(es): {prompts.Count} target(s)");
                promptMissStreak = 0;
                return prompts;
            }

            promptMissStreak++;
            Console.WriteLine($"[SLACK] Prompt miss streak={promptMissStreak}");
            if (promptMissStreak < 3) return new List<ClaudePromptHelper.PromptInfo>();

            try
            {
                var cwdPrompt = promptHelper.FindPromptForCwd(Environment.CurrentDirectory);
                if (cwdPrompt != null)
                {
                    promptMissStreak = 0;
                    Console.WriteLine($"[SLACK] Prompt recovery succeeded (cwd): {cwdPrompt.HostType}");
                    return new List<ClaudePromptHelper.PromptInfo> { cwdPrompt };
                }

                var codexCandidate = promptHelper.FindAllPrompts()
                    .FirstOrDefault(p => string.Equals(p.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase));
                if (codexCandidate != null)
                {
                    promptMissStreak = 0;
                    Console.WriteLine("[SLACK] Prompt recovery succeeded (codex re-scan)");
                    return new List<ClaudePromptHelper.PromptInfo> { codexCandidate };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SLACK] Prompt recovery error: {ex.Message}");
            }

            return new List<ClaudePromptHelper.PromptInfo>();
        }

        void ReportPromptLostThrottled(string channelId, string threadKey, string user, string cleanText, string msgTs)
        {
            var now = DateTime.UtcNow;
            if (now - lastPromptLostNotifyUtc < promptLostNotifyCooldown)
            {
                Console.WriteLine("[SLACK] Prompt-lost notice suppressed by cooldown");
                return;
            }
            lastPromptLostNotifyUtc = now;
            HandlePromptLost(botToken!, channelId, threadKey, user, cleanText, msgTs, username: BotUsername);
        }

        var startupPrompts = ResolvePromptsWithRecovery();
        if (startupPrompts.Count > 0)
        {
            var promptInfo = startupPrompts[0];
            Console.WriteLine($"[SLACK] Prompt: {promptInfo.HostType} — {promptInfo.WindowTitle}");
            Console.WriteLine($"[SLACK]   Rect: ({promptInfo.PromptRect.X},{promptInfo.PromptRect.Y} {promptInfo.PromptRect.Width}x{promptInfo.PromptRect.Height})");
        }
        else
        {
            Console.WriteLine("[SLACK] WARNING: Could not find Claude prompt — will retry per message");
        }

        // Write PID file (for status/stop)
        WritePidFile();

        var modeStr = new List<string> { "Prompt", $"Keywords({DefaultKeywords.Length})" };
        if (ai != null) modeStr.Add("AI");
        if (webBotMode) modeStr.Add(claudeMode ? "Claude" : "WebBot");
        var modeSuffix = $" + {string.Join(" + ", modeStr)}";
        Console.WriteLine($"[SLACK] Instance: [{instanceName}]");
        Console.WriteLine($"[SLACK] Starting Socket Mode listener{modeSuffix}...");
        Console.WriteLine("[SLACK] Press Ctrl+C to stop");

        using var slack = new SlackSocketClient();
        var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
            Console.WriteLine("\n[SLACK] Shutting down...");
        };

        // Connect
        slack.ConnectAsync(appToken, botToken).GetAwaiter().GetResult();

        // Track threads where bot is engaged (for thread reply forwarding)
        var activeThreads = new HashSet<string>();

        // Track message timestamps that THIS bot sent (for ping response thread tracking)
        var ownMessageTimestamps = new HashSet<string>();

        // Thread reply relocation: OnMessage → MonitorTick bridge (declared early for lambda capture)
        var streamingThreadReplies = new ConcurrentQueue<(string user, string text, string threadTs)>();
        string? statusTsForCapture = null; // readable from OnMessage closure (ref can't be captured)

        // Announce presence on startup
        var defaultChannel = config["channel"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(defaultChannel))
        {
            var host = Environment.MachineName;
            var pid = Environment.ProcessId;
            var startupMsg = $"클봇 온라인! `{host}` pid={pid}";
            try
            {
                var sendTask = Task.Run(async () => await SlackSendViaApi(botToken!, defaultChannel, startupMsg, username: BotUsername));
                if (sendTask.Wait(10_000))
                {
                    var (ok, sentTs) = sendTask.Result;
                    if (ok && sentTs != null)
                    {
                        ownMessageTimestamps.Add(sentTs);
                        activeThreads.Add(sentTs);
                        Console.WriteLine($"[SLACK] Startup announcement sent (ts={sentTs})");
                    }
                }
                else
                    Console.WriteLine("[SLACK] Startup announcement timeout — skipping");
            }
            catch { Console.WriteLine("[SLACK] Startup announcement failed — skipping"); }
        }

        // Handle @mentions — always reply in-thread
        slack.OnMention += (msg) =>
        {
            Console.WriteLine($"[SLACK] << @mention from {msg.User}: {msg.Text}");

            // Track last processed timestamp for catch-up
            SaveLastTs(msg.Channel, msg.Timestamp);

            // Strip the bot mention from text: "<@U12345> hello" → "hello"
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

            if (string.IsNullOrEmpty(cleanText))
                cleanText = "ping";

            // Track this thread so ALL follow-up messages come through (until "그만")
            var threadKey = msg.ThreadTs ?? msg.Timestamp;
            activeThreads.Add(threadKey);

            // Save reply context so 'slack reply' works with no flags
            SaveReplyContext(msg.Channel, threadKey);

            // "수락" / "accept" → send Ctrl+Enter to Claude Code prompt (remote approval)
            if (IsAcceptCommand(cleanText))
            {
                SendAcceptKeystroke(botToken!, msg.Channel, threadKey);
                return;
            }

            // Always forward to Claude Code prompt
            {
                var promptText = $"{cleanText}\n\n(Slack @{msg.User} #{msg.Channel} — reply: wkappbot slack reply \"...\")";
                var replyHint = SlackReplySuffix(msg.User, threadKey, $"#{msg.Channel}");
                if (!promptText.Contains("--msg", StringComparison.OrdinalIgnoreCase))
                    promptText = $"{cleanText}\n\n{replyHint}";
                Console.WriteLine($"[SLACK] >> Typing into Claude prompt...");

                var targets = ResolvePromptsWithRecovery();
                if (targets.Count > 0)
                {
                    var sent = 0;
                    foreach (var target in targets)
                    {
                        promptHelper.TypeAndSubmit(target, promptText);
                        sent++;
                    }
                    Console.WriteLine($"[SLACK] >> Sent to {sent}/{targets.Count} prompt(s)");
                }
                else
                {
                    ReportPromptLostThrottled(msg.Channel, threadKey, msg.User, cleanText, msg.Timestamp);
                }
                return;
            }
        };

        // Handle channel messages — ping response + thread replies + keyword monitoring
        slack.OnMessage += (msg) =>
        {
            // Check if reply is to the streaming status message → relocate streaming
            if (webBotMode && msg.ThreadTs != null && msg.ThreadTs == statusTsForCapture)
            {
                // Skip bot's own messages (relocation reply should not trigger another relocation)
                if (!string.IsNullOrEmpty(msg.BotId)) return;

                streamingThreadReplies.Enqueue((msg.User, msg.Text ?? "", msg.ThreadTs));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[SLACK] Streaming msg got reply from {msg.User} -> will relocate");
                Console.ResetColor();

                // Track this thread for future conversation
                activeThreads.Add(msg.ThreadTs);
                SaveReplyContext(msg.Channel, msg.ThreadTs);
                return;
            }

            // Any user channel message (not a thread reply) while streaming → relocate to keep status at bottom
            // Uses special marker "__channel_msg__" to distinguish from thread reply relocation
            if (webBotMode && statusTsForCapture != null && msg.ThreadTs == null && string.IsNullOrEmpty(msg.BotId))
            {
                streamingThreadReplies.Enqueue((msg.User, "__channel_msg__", statusTsForCapture));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[SLACK] Channel msg from {msg.User} -> relocating status to bottom");
                Console.ResetColor();
                // Don't return — continue processing (ping, keywords, etc.)
            }

            // "핑" / "ping" command: bot responds in channel with identity + CWD
            if (msg.ThreadTs == null && !string.IsNullOrEmpty(msg.Text))
            {
                var trimmed = msg.Text.Trim().ToLowerInvariant();
                if (trimmed == "핑" || trimmed == "ping")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[SLACK] << ping from {msg.User}");
                    Console.ResetColor();
                    SaveLastTs(msg.Channel, msg.Timestamp);

                    var cwd = Environment.CurrentDirectory;
                    var host = Environment.MachineName;
                    var pid = Environment.ProcessId;
                    var pingReply = $"퐁! `{host}` pid={pid} `{cwd}`";

                    var (ok, sentTs) = SlackSendViaApi(botToken!, msg.Channel, pingReply, username: BotUsername).GetAwaiter().GetResult();
                    if (ok && sentTs != null)
                    {
                        // Track our own response so thread replies come to US
                        ownMessageTimestamps.Add(sentTs);
                        activeThreads.Add(sentTs);
                        Console.WriteLine($"[SLACK] >> pong sent (ts={sentTs})");
                    }
                    return;
                }
            }

            // Auto-track threads on bot's own messages (query Slack API)
            if (msg.ThreadTs != null && !activeThreads.Contains(msg.ThreadTs))
            {
                // Check if the thread parent is our bot's message
                if (IsOwnThread(botToken!, msg.Channel, msg.ThreadTs))
                {
                    activeThreads.Add(msg.ThreadTs);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[SLACK] Auto-tracking thread on bot's message (ts={msg.ThreadTs})");
                    Console.ResetColor();
                }
            }

            // Check if this is a thread reply to a conversation we're tracking
            if (msg.ThreadTs != null && activeThreads.Contains(msg.ThreadTs))
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                // "그만" / "stop" → stop tracking this thread
                if (!string.IsNullOrEmpty(cleanText))
                {
                    var trimmedLower = cleanText.Trim().ToLowerInvariant();
                    if (trimmedLower == "그만" || trimmedLower == "stop" || trimmedLower == "ㄱㅁ")
                    {
                        activeThreads.Remove(msg.ThreadTs);
                        ownMessageTimestamps.Remove(msg.ThreadTs);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"[SLACK] << thread stopped by {msg.User} (ts={msg.ThreadTs})");
                        Console.ResetColor();
                        // Send farewell in-thread
                        SlackSendViaApi(botToken!, msg.Channel, "알겠습니다~ 이 쓰레드에서 물러납니다!", msg.ThreadTs)
                            .GetAwaiter().GetResult();
                        return;
                    }
                }

                Console.WriteLine($"[SLACK] << thread reply from {msg.User}: {msg.Text}");
                SaveLastTs(msg.Channel, msg.Timestamp);
                SaveReplyContext(msg.Channel, msg.ThreadTs!);

                if (string.IsNullOrEmpty(cleanText)) return;

                // "수락" / "accept" → send Ctrl+Enter to Claude Code prompt (remote approval)
                if (IsAcceptCommand(cleanText))
                {
                    SendAcceptKeystroke(botToken!, msg.Channel, msg.ThreadTs!);
                    return;
                }

                // Always forward thread replies to Claude Code
                {
                    // Build thread context: bot's original + previous user message
                    var threadContext = "";
                    if (msg.ThreadTs != null && !ownMessageTimestamps.Contains(msg.ThreadTs))
                    {
                        var ctx = GetThreadContext(botToken!, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n\n{ctx}\n";
                    }
                    var promptText = $"{cleanText}{threadContext}\n\n(Slack @{msg.User} #{msg.Channel} thread — reply: wkappbot slack reply \"...\")";
                    var replyThread = msg.ThreadTs ?? msg.Timestamp;
                    var replyHint = SlackReplySuffix(msg.User, replyThread, $"#{msg.Channel} thread");
                    if (!promptText.Contains("--msg", StringComparison.OrdinalIgnoreCase))
                        promptText = $"{cleanText}{threadContext}\n\n{replyHint}";
                    Console.WriteLine($"[SLACK] >> Typing thread reply into Claude prompt...");

                    var targets = ResolvePromptsWithRecovery();
                    if (targets.Count > 0)
                    {
                        var sent = 0;
                        foreach (var target in targets)
                        {
                            promptHelper.TypeAndSubmit(target, promptText);
                            sent++;
                        }
                        Console.WriteLine($"[SLACK] >> Sent thread reply to {sent}/{targets.Count} prompt(s)");
                    }
                    else
                    {
                        ReportPromptLostThrottled(msg.Channel, msg.ThreadTs!, msg.User, cleanText, msg.Timestamp);
                    }
                    return;
                }
            }

            // Keyword monitoring: check if message contains any watched keywords
            // Keyword monitoring — always ON, forward to Claude prompt
            if (!string.IsNullOrEmpty(msg.Text))
            {
                var textLower = msg.Text.ToLowerInvariant();
                var matchedKeyword = DefaultKeywords.FirstOrDefault(kw => textLower.Contains(kw.ToLowerInvariant()));
                if (matchedKeyword != null)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"[SLACK] << keyword hit \"{matchedKeyword}\" from {msg.User}: {msg.Text}");
                    Console.ResetColor();
                    SaveLastTs(msg.Channel, msg.Timestamp);

                    // Start tracking this thread so follow-up messages also come through
                    var threadKey = msg.ThreadTs ?? msg.Timestamp;
                    activeThreads.Add(threadKey);
                    SaveReplyContext(msg.Channel, threadKey);

                    var cleanText = System.Text.RegularExpressions.Regex.Replace(
                        msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (string.IsNullOrEmpty(cleanText)) return;

                    // Always forward keyword matches to Claude Code prompt
                    {
                        var promptText = $"{cleanText}\n\n(Slack keyword:\"{matchedKeyword}\" @{msg.User} #{msg.Channel} — reply: wkappbot slack reply \"...\")";
                        var replyHint = SlackReplySuffix(msg.User, threadKey, $"keyword:{matchedKeyword} #{msg.Channel}");
                        if (!promptText.Contains("--msg", StringComparison.OrdinalIgnoreCase))
                            promptText = $"{cleanText}\n\n{replyHint}";
                        Console.WriteLine($"[SLACK] >> Typing keyword match into Claude prompt...");

                        var targets = ResolvePromptsWithRecovery();
                        if (targets.Count > 0)
                        {
                            var sent = 0;
                            foreach (var target in targets)
                            {
                                promptHelper.TypeAndSubmit(target, promptText);
                                sent++;
                            }
                            Console.WriteLine($"[SLACK] >> Sent keyword match to {sent}/{targets.Count} prompt(s)");
                        }
                        else
                        {
                            ReportPromptLostThrottled(msg.Channel, threadKey, msg.User, cleanText, msg.Timestamp);
                        }
                        return;
                    }
                }
            }

            Console.WriteLine($"[SLACK] msg [{msg.Channel}] {msg.User}: {msg.Text}");
        };

        // Keep display awake while daemon is running (prevent lock screen)
        // ES_CONTINUOUS keeps the state until explicitly cleared
        NativeMethods.SetThreadExecutionState(
            NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_DISPLAY_REQUIRED);
        Console.WriteLine("[SLACK] Display sleep prevention: ON");

        // Hot-reload: track current EXE timestamp for detecting new builds
        var currentExePath = Environment.ProcessPath!;
        var currentExeTime = File.GetLastWriteTimeUtc(currentExePath);
        bool hotReload = false;

        // WebBot monitoring state (--webbot)
        // Simplified: no CDP/Chrome content — only Claude status via UIA + Slack profile
        CdpClient? webBotCdp = null;       // Kept for Dispose cleanup
        string? webBotStatusTs = null;     // Slack message ts for chat.update
        string? webBotLastUrl = null;      // Unused (kept for ref param compat)
        string? webBotLastContent = null;  // Reused: tracks last Claude status text
        bool webBotClaudeBusy = false;     // Claude is currently busy
        int webBotReconnectBackoff = 0;    // Unused (kept for ref param compat)
        string? webBotStatusChannel = defaultChannel; // Channel for status messages
        IntPtr webBotChromeHwnd = IntPtr.Zero; // Unused (kept for ref param compat)
        var webBotIdlePollSw = Stopwatch.StartNew(); // Throttle idle busy-check to ~2s

        // WebBot: lazy CDP connection — connect on first busy detection
        // Avoids sync-over-async deadlock during startup
        if (webBotMode)
        {
            Console.WriteLine("[SLACK] WebBot mode ON — will connect to Chrome on demand");
            Console.Out.Flush();
        }

        // Wait until cancelled — fast 100ms tick loop for instant WebBot streaming
        // UIA/CDP queries themselves take ~50-200ms, acting as natural variable delay
        // Result: ~0.2-0.3s effective loop = broadcast-like real-time streaming
        try
        {
            var hotReloadSw = Stopwatch.StartNew();
            var keepAliveSw = Stopwatch.StartNew();
            while (!cts.Token.IsCancellationRequested)
            {
                // Fixed 100ms sleep — UIA queries add variable delay on top
                Task.Delay(100, cts.Token).GetAwaiter().GetResult();

                // Check stop signal file (written by 'slack stop')
                if (File.Exists(SlackStopSignalFile))
                {
                    try { File.Delete(SlackStopSignalFile); } catch { }
                    Console.WriteLine("[SLACK] Stop signal received — shutting down gracefully...");
                    break;
                }

                // Hot-reload: check every ~10s (skip during busy streaming)
                if (!webBotClaudeBusy && hotReloadSw.ElapsedMilliseconds >= 10_000)
                {
                    hotReloadSw.Restart();
                    try
                    {
                        // Look for newer EXE in publish directory (sibling to source)
                        var exeDir = Path.GetDirectoryName(currentExePath)!;
                        var hotReloadSource = Path.Combine(exeDir, "wkappbot.new.exe");

                        // Also check the publish output directly
                        if (!File.Exists(hotReloadSource))
                        {
                            // Convention: csproj PostPublish copies to W:\SDK\bin\
                            // After publish, the new EXE replaces the old one via rename trick
                            // Check if current EXE timestamp changed (another process renamed+copied)
                            var newTime = File.GetLastWriteTimeUtc(currentExePath);
                            if (newTime != currentExeTime)
                            {
                                // Someone replaced our EXE while we were renamed — restart
                                Console.WriteLine($"[SLACK] Hot-reload: EXE updated ({currentExeTime:HH:mm:ss} → {newTime:HH:mm:ss})");
                                hotReload = true;
                                break;
                            }
                        }
                        else
                        {
                            // wkappbot.new.exe detected — perform rename swap
                            Console.WriteLine("[SLACK] Hot-reload: new build detected (wkappbot.new.exe)");
                            var oldPath = currentExePath + ".old";
                            try { File.Delete(oldPath); } catch { }
                            File.Move(currentExePath, oldPath);     // running EXE → .old (allowed on Windows!)
                            File.Move(hotReloadSource, currentExePath); // .new → current name
                            Console.WriteLine("[SLACK] Hot-reload: EXE swapped");
                            hotReload = true;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[SLACK] Hot-reload check error: {ex.Message}");
                    }
                }

                // WebBot monitoring:
                // - When busy: every tick (~100ms + UIA ~100ms = ~200ms effective) — broadcast streaming!
                // - When idle: every ~2s (just busy-check, no heavy UIA/CDP work)
                if (webBotMode)
                {
                    bool shouldCheck = webBotClaudeBusy || webBotIdlePollSw.ElapsedMilliseconds >= 2_000;
                    if (shouldCheck)
                    {
                        if (!webBotClaudeBusy)
                            webBotIdlePollSw.Restart();
                        try
                        {
                            WebBotMonitorTick(
                                botToken!, webBotStatusChannel!,
                                ref webBotCdp, ref webBotStatusTs,
                                ref webBotLastUrl, ref webBotLastContent,
                                ref webBotClaudeBusy, ref webBotReconnectBackoff,
                                ref webBotChromeHwnd,
                                promptHelper,
                                streamingThreadReplies,
                                instanceName);

                            // Sync statusTs for OnMessage closure (ref can't be captured in lambda)
                            statusTsForCapture = webBotStatusTs;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[SLACK] WebBot monitor error: {ex.Message}");
                        }
                    }
                }

                // Refresh keep-awake every 60s
                if (keepAliveSw.ElapsedMilliseconds >= 60_000)
                {
                    keepAliveSw.Restart();
                    NativeMethods.SetThreadExecutionState(
                        NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_DISPLAY_REQUIRED);
                }
            }
        }
        catch (OperationCanceledException) { }

        // Clear keep-awake state on exit
        NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);
        Console.WriteLine("[SLACK] Display sleep prevention: OFF");

        // Send shutdown message to latest thread
        try
        {
            var host = Environment.MachineName;
            var pid = Environment.ProcessId;
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var uptimeStr = uptime.TotalHours >= 1
                ? $"{uptime.Hours}h{uptime.Minutes}m"
                : $"{uptime.Minutes}m{uptime.Seconds}s";
            var shutdownMsg = hotReload
                ? $"클봇 핫리로드! 새 빌드 감지 → 재시작. `{host}` (uptime {uptimeStr})"
                : $"클봇 오프라인. `{host}` (uptime {uptimeStr})";
            var (replyChannel, replyThread) = LoadReplyContext();
            var ch = replyChannel ?? defaultChannel!;
            Task.Run(async () => await SlackSendViaApi(botToken!, ch, shutdownMsg, replyThread, username: BotUsername)).Wait(5000);
            Console.WriteLine("[SLACK] Shutdown message sent");
        }
        catch { /* best-effort */ }

        slack.DisconnectAsync().GetAwaiter().GetResult();
        ai?.Dispose();
        promptHelper.Dispose();
        webBotCdp?.Dispose();
        DeletePidFile();

        // Hot-reload: restart self with same arguments
        if (hotReload)
        {
            Console.WriteLine($"[SLACK] Hot-reload: restarting {currentExePath}...");
            try
            {
                var cmdArgs = Environment.GetCommandLineArgs();
                // cmdArgs[0] = exe path, cmdArgs[1..] = actual arguments
                var restartArgs = string.Join(" ", cmdArgs.Skip(1).Select(a => a.Contains(' ') ? $"\"{a}\"" : a));
                var psi = new ProcessStartInfo
                {
                    FileName = currentExePath,
                    Arguments = restartArgs,
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory,
                };
                // Pass through environment variables
                psi.Environment["DOTNET_ROOT"] = Environment.GetEnvironmentVariable("DOTNET_ROOT") ?? "";
                Process.Start(psi);
                Console.WriteLine("[SLACK] Hot-reload: new process launched");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SLACK] Hot-reload restart failed: {ex.Message}");
            }
        }

        return 0;
    }

    /// <summary>Launch wkappbot slack listen as a background process.</summary>
    static int SlackLaunchBackground(bool aiMode = false, bool webBotMode = false, bool claudeMode = false, string? instanceName = null)
    {
        // Check if already running
        if (IsSlackListenerRunning(out int existingPid))
        {
            Console.WriteLine($"[SLACK] Listener already running (PID={existingPid})");
            return 0;
        }

        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var logFile = Path.Combine(DataDir, "logs", $"slack-listen-{DateTime.Now:yyyyMMdd_HHmmss}.txt");

        // Ensure log dir exists
        Directory.CreateDirectory(Path.GetDirectoryName(logFile)!);

        var listenArgs = "slack listen";
        if (aiMode) listenArgs += " --ai";
        // promptMode and keywordMode are always ON — no flags needed
        if (claudeMode) listenArgs += " --claude";
        else if (webBotMode) listenArgs += " --webbot";
        if (instanceName != null) listenArgs += $" --name \"{instanceName}\"";
        var psi = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = listenArgs,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        // Pass essential env vars to child process
        // Use psi.Environment (inherits all parent env vars, then we add/override)
        var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
        if (!string.IsNullOrEmpty(dotnetRoot))
            psi.Environment["DOTNET_ROOT"] = dotnetRoot;

        // Pass API keys for --ai mode (Claude Code session token as fallback)
        var apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
        if (!string.IsNullOrEmpty(apiKey))
            psi.Environment["ANTHROPIC_API_KEY"] = apiKey;
        var oauthToken = Environment.GetEnvironmentVariable("CLAUDE_CODE_OAUTH_TOKEN");
        if (!string.IsNullOrEmpty(oauthToken))
            psi.Environment["CLAUDE_CODE_OAUTH_TOKEN"] = oauthToken;

        var process = Process.Start(psi);
        if (process == null)
        {
            Console.WriteLine("[SLACK] Failed to start background listener");
            return 1;
        }

        // Redirect stdout + stderr to log file (auto-flush for real-time monitoring)
        // Use OutputDataReceived/ErrorDataReceived instead of manual ReadLineAsync
        // so the launcher process can exit without killing the reader
        using var writer = new StreamWriter(logFile, append: true) { AutoFlush = true };
        process.OutputDataReceived += (_, e) => { if (e.Data != null) try { writer.WriteLine(e.Data); } catch { } };
        process.ErrorDataReceived += (_, e) => { if (e.Data != null) try { writer.WriteLine($"[ERR] {e.Data}"); } catch { } };
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        // Wait a moment for initialization output
        Thread.Sleep(3000);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SLACK] Listener started in background (PID={process.Id})");
        Console.ResetColor();
        Console.WriteLine($"[SLACK] Log: {logFile}");
        Console.WriteLine($"[SLACK] Stop: wkappbot slack stop");

        // Show first few lines of log (open with shared read to avoid lock conflict)
        try
        {
            using var reader = new StreamReader(
                new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            for (int i = 0; i < 5; i++)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                Console.WriteLine($"  {line}");
            }
        }
        catch { }

        return 0;
    }
}
