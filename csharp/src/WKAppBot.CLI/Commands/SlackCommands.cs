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

// partial class: wkappbot slack <subcommand> — Slack Socket Mode integration
internal partial class Program
{
    static int SlackCommand(string[] args)
    {
        if (args.Length == 0)
            return SlackUsage();

        var sub = args[0].ToLowerInvariant();
        return sub switch
        {
            "listen" => SlackListenCommand(args),
            "send" => SlackSendCommand(args),
            "test" => SlackTestCommand(args),
            "status" => SlackStatusCommand(),
            "stop" => SlackStopCommand(),
            "inbox" => SlackInboxCommand(),
            "reply" => SlackReplyCommand(args),
            "upload" => SlackUploadCommand(args),
            "screenshot" => SlackScreenshotCommand(args),
            "catch-up" or "catchup" => SlackCatchUpCommand(args),
            "prompt" => SlackPromptCommand(args),
            _ => SlackUsage()
        };
    }

    static string SlackConfigPath => Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
    static string SlackPidFile => Path.Combine(DataDir, "slack.pid");
    static string SlackStopSignalFile => Path.Combine(DataDir, "slack.stop");
    static string SlackInboxFile => Path.Combine(DataDir, "slack_inbox.jsonl");
    static string SlackLastTsFile => Path.Combine(DataDir, "slack_last_ts.txt");
    /// <summary>Last reply context file: "channel=C0xxx\nthread=1234.5678" — so 'slack reply' needs no flags.</summary>
    static string SlackReplyContextFile => Path.Combine(DataDir, "slack_reply_ctx.txt");

    /// <summary>Default keywords for keyword monitoring (Korean typos + English variants).</summary>
    static readonly string[] DefaultKeywords = new[]
    {
        "클롯", "클롣", "클로드", "앱봇",          // Korean + typos
        "claude", "appbot", "wkappbot", "클봇",    // English + mixed
    };

    /// <summary>Keywords that trigger Ctrl+Enter (accept/approve) on Claude Code prompt.</summary>
    static readonly string[] AcceptKeywords = new[]
    {
        "수락", "승인", "ㅅㄹ", "accept", "approve", "yes", "ㅇㅇ",
    };

    /// <summary>Socket Mode: listen for events and respond to @mentions.</summary>
    static int SlackListenCommand(string[] args)
    {
        bool background = args.Contains("--bg") || args.Contains("--background") || args.Contains("-d");
        bool aiMode = args.Contains("--ai");
        bool promptMode = args.Contains("--prompt");
        bool keywordMode = args.Contains("--keywords") || args.Contains("-k");
        bool webBotMode = args.Contains("--webbot") || args.Contains("--web");

        // --bg: launch self as background process (pass flags through)
        if (background)
            return SlackLaunchBackground(aiMode, promptMode, keywordMode, webBotMode);

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var appToken = config["app_token"]?.GetValue<string>();
        var botToken = config["bot_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(appToken) || string.IsNullOrEmpty(botToken))
        {
            Console.WriteLine("[SLACK] ERROR: app_token or bot_token not found in config");
            return 1;
        }

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

        // Initialize Claude prompt helper if --prompt flag
        ClaudePromptHelper? promptHelper = null;
        ClaudePromptHelper.PromptInfo? promptInfo = null;
        if (promptMode)
        {
            promptHelper = new ClaudePromptHelper();
            promptInfo = promptHelper.FindPrompt();
            if (promptInfo != null)
            {
                Console.WriteLine($"[SLACK] Prompt mode: {promptInfo.HostType} — {promptInfo.WindowTitle}");
                Console.WriteLine($"[SLACK]   Rect: ({promptInfo.PromptRect.X},{promptInfo.PromptRect.Y} {promptInfo.PromptRect.Width}x{promptInfo.PromptRect.Height})");
            }
            else
            {
                Console.WriteLine("[SLACK] WARNING: Could not find Claude prompt — falling back to inbox mode");
            }
        }

        // Write PID file (for status/stop)
        WritePidFile();

        var modeStr = new List<string>();
        if (ai != null) modeStr.Add("AI");
        if (promptInfo != null) modeStr.Add("Prompt");
        if (keywordMode) modeStr.Add($"Keywords({DefaultKeywords.Length})");
        if (webBotMode) modeStr.Add("WebBot");
        var modeSuffix = modeStr.Count > 0 ? $" + {string.Join(" + ", modeStr)}" : "";
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

        // Announce presence on startup (channel message, not thread)
        var defaultChannel = config["channel"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(defaultChannel))
        {
            var cwd = Environment.CurrentDirectory;
            var host = Environment.MachineName;
            var pid = Environment.ProcessId;
            var startupMsg = $"클봇 온라인! `{host}` pid={pid} `{cwd}`";
            var (ok, sentTs) = SlackSendViaApi(botToken!, defaultChannel, startupMsg).GetAwaiter().GetResult();
            if (ok && sentTs != null)
            {
                ownMessageTimestamps.Add(sentTs);
                activeThreads.Add(sentTs);
                Console.WriteLine($"[SLACK] Startup announcement sent (ts={sentTs})");
            }
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

            // Prompt mode: type directly into Claude Code prompt
            if (promptHelper != null && promptInfo != null)
            {
                var promptText = $"{cleanText}\n\n(Slack @{msg.User} #{msg.Channel} — reply: wkappbot slack reply \"...\")";
                // reply context auto-saved, no need for --channel/--thread in hint
                Console.WriteLine($"[SLACK] >> Typing into Claude prompt...");

                var fresh = promptHelper.FindPrompt();
                if (fresh != null)
                {
                    promptHelper.TypeAndSubmit(fresh, promptText);
                    Console.WriteLine("[SLACK] >> Sent to Claude prompt");
                }
                else
                {
                    HandlePromptLost(botToken!, msg.Channel, threadKey,
                        msg.User, cleanText, msg.Timestamp);
                }
                return;
            }

            // Inbox + AI/echo fallback
            WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);

            string reply;
            if (ai != null)
            {
                Console.Write("[SLACK] [AI] thinking... ");
                try
                {
                    reply = ai.AskAsync(cleanText).GetAwaiter().GetResult();
                    Console.WriteLine($"({reply.Length} chars)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                    reply = $"(AI error: {ex.Message})";
                }
            }
            else
            {
                reply = null!;
            }

            if (!string.IsNullOrEmpty(reply))
            {
                // Always reply in-thread (not channel)
                Console.WriteLine($"[SLACK] >> {reply}");
                SlackSendViaApi(botToken!, msg.Channel, reply, threadKey).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("[SLACK] >> (queued for Claude Code)");
            }
        };

        // Handle channel messages — ping response + thread replies + keyword monitoring
        slack.OnMessage += (msg) =>
        {
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

                    var (ok, sentTs) = SlackSendViaApi(botToken!, msg.Channel, pingReply).GetAwaiter().GetResult();
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

                // Prompt mode: forward to Claude Code
                if (promptHelper != null && promptInfo != null)
                {
                    // If this is a reply to bot's own message, include parent context
                    var parentContext = "";
                    if (msg.ThreadTs != null && !ownMessageTimestamps.Contains(msg.ThreadTs))
                    {
                        // Auto-tracked thread — fetch bot's original message for context
                        var parentText = GetThreadParentText(botToken!, msg.Channel, msg.ThreadTs);
                        if (!string.IsNullOrEmpty(parentText))
                        {
                            // Truncate long bot messages
                            if (parentText.Length > 300)
                                parentText = parentText[..297] + "...";
                            parentContext = $"\n\n[이전 클봇 메시지]\n{parentText}\n";
                        }
                    }
                    var promptText = $"{cleanText}{parentContext}\n\n(Slack @{msg.User} #{msg.Channel} thread — reply: wkappbot slack reply \"...\")";
                    // reply context auto-saved
                    Console.WriteLine($"[SLACK] >> Typing thread reply into Claude prompt...");

                    var fresh = promptHelper.FindPrompt();
                    if (fresh != null)
                    {
                        promptHelper.TypeAndSubmit(fresh, promptText);
                        Console.WriteLine("[SLACK] >> Sent to Claude prompt");
                    }
                    else
                    {
                        HandlePromptLost(botToken!, msg.Channel, msg.ThreadTs!,
                            msg.User, cleanText, msg.Timestamp);
                    }
                    return;
                }

                // Fallback: write to inbox
                WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);
                return;
            }

            // Keyword monitoring: check if message contains any watched keywords
            if (keywordMode && !string.IsNullOrEmpty(msg.Text))
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

                    // Prompt mode: forward to Claude Code with keyword context
                    if (promptHelper != null && promptInfo != null)
                    {
                        var promptText = $"{cleanText}\n\n(Slack keyword:\"{matchedKeyword}\" @{msg.User} #{msg.Channel} — reply: wkappbot slack reply \"...\")";
                        // reply context auto-saved
                        Console.WriteLine($"[SLACK] >> Typing keyword match into Claude prompt...");

                        var fresh = promptHelper.FindPrompt();
                        if (fresh != null)
                        {
                            promptHelper.TypeAndSubmit(fresh, promptText);
                            Console.WriteLine("[SLACK] >> Sent to Claude prompt");
                        }
                        else
                        {
                            var kwThreadKey = msg.ThreadTs ?? msg.Timestamp;
                            HandlePromptLost(botToken!, msg.Channel, kwThreadKey,
                                msg.User, cleanText, msg.Timestamp);
                        }
                        return;
                    }

                    // Fallback: write to inbox
                    WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);
                    return;
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
        CdpClient? webBotCdp = null;
        string? webBotStatusTs = null;     // Slack message ts for chat.update
        string? webBotLastUrl = null;      // Last URL (change detection)
        string? webBotLastContent = null;  // Last page content
        bool webBotClaudeBusy = false;     // Claude is currently busy
        int webBotReconnectBackoff = 0;    // Reconnect backoff counter
        string? webBotStatusChannel = defaultChannel; // Channel for status messages
        IntPtr webBotChromeHwnd = IntPtr.Zero; // Chrome window handle for UIA content extraction

        // WebBot: initial CDP connection attempt
        if (webBotMode)
        {
            try
            {
                webBotCdp = new CdpClient();
                webBotCdp.ConnectAsync(9222).GetAwaiter().GetResult();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[SLACK] WebBot: Connected to Chrome (port 9222)");
                Console.ResetColor();
            }
            catch
            {
                Console.WriteLine("[SLACK] WebBot: Chrome not available yet (will retry)");
                webBotCdp?.Dispose();
                webBotCdp = null;
            }
        }

        // Wait until cancelled — tick every 2s to check stop signal + hot-reload + keep-awake
        try
        {
            int tickCount = 0;
            while (!cts.Token.IsCancellationRequested)
            {
                Task.Delay(2_000, cts.Token).GetAwaiter().GetResult();

                // Check stop signal file (written by 'slack stop')
                if (File.Exists(SlackStopSignalFile))
                {
                    try { File.Delete(SlackStopSignalFile); } catch { }
                    Console.WriteLine("[SLACK] Stop signal received — shutting down gracefully...");
                    break;
                }

                // Hot-reload: check if a newer EXE exists in publish output
                if (++tickCount % 5 == 0) // every 10s
                {
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

                // WebBot monitoring: check Claude status + stream to Slack (~6 seconds = 3 ticks)
                if (webBotMode && tickCount % 3 == 1)
                {
                    try
                    {
                        WebBotMonitorTick(
                            botToken!, webBotStatusChannel!,
                            ref webBotCdp, ref webBotStatusTs,
                            ref webBotLastUrl, ref webBotLastContent,
                            ref webBotClaudeBusy, ref webBotReconnectBackoff,
                            ref webBotChromeHwnd,
                            promptHelper);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[SLACK] WebBot monitor error: {ex.Message}");
                    }
                }

                // Refresh keep-awake every 60s (every 30 ticks)
                if (tickCount % 30 == 0)
                {
                    NativeMethods.SetThreadExecutionState(
                        NativeMethods.ES_CONTINUOUS | NativeMethods.ES_SYSTEM_REQUIRED | NativeMethods.ES_DISPLAY_REQUIRED);
                }
            }
        }
        catch (OperationCanceledException) { }

        // Clear keep-awake state on exit
        NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);
        Console.WriteLine("[SLACK] Display sleep prevention: OFF");

        // Send shutdown/reload message to Slack (in latest thread)
        try
        {
            var host = Environment.MachineName;
            var pid = Environment.ProcessId;
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var uptimeStr = uptime.TotalHours >= 1
                ? $"{uptime.Hours}h{uptime.Minutes}m"
                : $"{uptime.Minutes}m{uptime.Seconds}s";
            var shutdownMsg = hotReload
                ? $"클봇 핫리로드! 새 빌드 감지 → 재시작합니다. `{host}` pid={pid} (uptime {uptimeStr})"
                : $"클봇 오프라인. `{host}` pid={pid} (uptime {uptimeStr})";
            var (replyChannel, replyThread) = LoadReplyContext();
            var ch = replyChannel ?? defaultChannel!;
            SlackSendViaApi(botToken!, ch, shutdownMsg, replyThread).GetAwaiter().GetResult();
            Console.WriteLine("[SLACK] Shutdown message sent");
        }
        catch { /* best-effort */ }

        slack.DisconnectAsync().GetAwaiter().GetResult();
        ai?.Dispose();
        promptHelper?.Dispose();
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
    static int SlackLaunchBackground(bool aiMode = false, bool promptMode = false, bool keywordMode = false, bool webBotMode = false)
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
        if (promptMode) listenArgs += " --prompt";
        if (keywordMode) listenArgs += " --keywords";
        if (webBotMode) listenArgs += " --webbot";
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

    /// <summary>Check if Slack listener is running.</summary>
    static int SlackStatusCommand()
    {
        if (IsSlackListenerRunning(out int pid))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SLACK] Listener is RUNNING (PID={pid})");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[SLACK] Listener is NOT running");
            Console.ResetColor();
        }
        return 0;
    }

    /// <summary>Stop background Slack listener (graceful → force kill).</summary>
    static int SlackStopCommand()
    {
        if (!IsSlackListenerRunning(out int pid))
        {
            Console.WriteLine("[SLACK] Listener is not running");
            return 0;
        }

        try
        {
            var proc = Process.GetProcessById(pid);

            // Phase 1: graceful — write stop signal file, wait up to 10s
            Console.WriteLine($"[SLACK] Sending stop signal to PID={pid}...");
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SlackStopSignalFile)!);
                File.WriteAllText(SlackStopSignalFile, "stop");
            }
            catch { }

            if (proc.WaitForExit(10_000))
            {
                Console.WriteLine($"[SLACK] Listener stopped gracefully (PID={pid})");
                DeletePidFile();
                return 0;
            }

            // Phase 2: force kill
            Console.WriteLine($"[SLACK] Graceful timeout — force killing PID={pid}...");
            proc.Kill(entireProcessTree: true);
            proc.WaitForExit(3000);
            Console.WriteLine($"[SLACK] Listener killed (PID={pid})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] Failed to stop PID={pid}: {ex.Message}");
        }

        try { File.Delete(SlackStopSignalFile); } catch { }
        DeletePidFile();
        return 0;
    }

    /// <summary>Check if listener process is alive via PID file.</summary>
    static bool IsSlackListenerRunning(out int pid)
    {
        pid = 0;
        if (!File.Exists(SlackPidFile)) return false;

        var text = File.ReadAllText(SlackPidFile).Trim();
        if (!int.TryParse(text, out pid)) return false;

        try
        {
            var proc = Process.GetProcessById(pid);
            // Verify it's actually wkappbot (not a recycled PID)
            if (proc.HasExited) { DeletePidFile(); return false; }
            var name = proc.ProcessName.ToLowerInvariant();
            if (name.Contains("wkappbot")) return true;

            // PID recycled to different process
            DeletePidFile();
            return false;
        }
        catch
        {
            DeletePidFile();
            return false;
        }
    }

    static void WritePidFile()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SlackPidFile)!);
            File.WriteAllText(SlackPidFile, Environment.ProcessId.ToString());
        }
        catch { }
    }

    static void DeletePidFile()
    {
        try { File.Delete(SlackPidFile); } catch { }
    }

    /// <summary>Send a message to the configured channel.</summary>
    static int SlackSendCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack send \"message text\"");
            return 1;
        }

        var message = string.Join(" ", args.Skip(1));
        // Bash history expansion escapes ! to \! even in single quotes — undo it
        message = message.Replace("\\!", "!");
        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found in config");
            return 1;
        }

        var (ok, _) = SlackSendViaApi(botToken, channel, message).GetAwaiter().GetResult();

        if (ok)
            Console.WriteLine($"[SLACK] Sent: {message}");
        else
            Console.WriteLine("[SLACK] Failed to send message");

        return ok ? 0 : 1;
    }

    /// <summary>Test Slack connection.</summary>
    static int SlackTestCommand(string[] args)
    {
        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var appToken = config["app_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();

        Console.WriteLine("[SLACK] Testing connection...");

        // Test auth.test
        using var http = new HttpClient();
        var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/auth.test");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

        var resp = http.Send(req);
        using var reader = new StreamReader(resp.Content.ReadAsStream());
        var body = reader.ReadToEnd();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        var ok = json?["ok"]?.GetValue<bool>() ?? false;
        if (ok)
        {
            var team = json?["team"]?.GetValue<string>();
            var user = json?["user"]?.GetValue<string>();
            var userId = json?["user_id"]?.GetValue<string>();
            Console.WriteLine($"[SLACK] auth.test OK — team={team}, bot={user} ({userId})");
        }
        else
        {
            Console.WriteLine($"[SLACK] auth.test FAILED: {json?["error"]}");
            return 1;
        }

        // Test send
        if (!string.IsNullOrEmpty(channel))
        {
            var (sendOk, _) = SlackSendViaApi(botToken!, channel, "WKAppBot test — connection verified!").GetAwaiter().GetResult();
            Console.WriteLine(sendOk ? "[SLACK] send OK" : "[SLACK] send FAILED");
        }

        // Test Socket Mode connection
        if (!string.IsNullOrEmpty(appToken))
        {
            Console.Write("[SLACK] Socket Mode... ");
            try
            {
                using var slack = new SlackSocketClient();
                slack.ConnectAsync(appToken, botToken!).GetAwaiter().GetResult();
                Console.WriteLine("OK");
                slack.DisconnectAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED: {ex.Message}");
            }
        }

        return 0;
    }

    /// <summary>
    /// Send message via chat.postMessage API. If threadTs is provided, replies in-thread.
    /// Returns (ok, message_ts) — message_ts is the timestamp of the sent message (for thread tracking).
    /// </summary>
    static async Task<(bool ok, string? ts)> SlackSendViaApi(string botToken, string channel, string text, string? threadTs = null)
    {
        using var http = new HttpClient();
        object payloadObj = string.IsNullOrEmpty(threadTs)
            ? new { channel, text }
            : (object)new { channel, text, thread_ts = threadTs };
        var payload = JsonSerializer.Serialize(payloadObj);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] API error: {json?["error"]}");

        var messageTs = json?["ts"]?.GetValue<string>();
        return (ok, messageTs);
    }

    /// <summary>
    /// Upload a file to Slack channel (v2 API: getUploadURLExternal → PUT → completeUploadExternal).
    /// </summary>
    static async Task<bool> SlackUploadFileAsync(string botToken, string channel, string filePath,
        string? title = null, string? threadTs = null, string? initialComment = null)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[SLACK] File not found: {filePath}");
            return false;
        }

        var fileInfo = new FileInfo(filePath);
        var fileName = fileInfo.Name;
        var fileSize = fileInfo.Length;
        title ??= fileName;

        using var http = new HttpClient();

        // Step 1: Get upload URL
        var getUrlParams = $"filename={Uri.EscapeDataString(fileName)}&length={fileSize}";
        using var getUrlReq = new HttpRequestMessage(HttpMethod.Get,
            $"https://slack.com/api/files.getUploadURLExternal?{getUrlParams}");
        getUrlReq.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var getUrlResp = await http.SendAsync(getUrlReq);
        var getUrlBody = await getUrlResp.Content.ReadAsStringAsync();
        var getUrlJson = JsonSerializer.Deserialize<JsonNode>(getUrlBody);

        if (getUrlJson?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] getUploadURLExternal failed: {getUrlJson?["error"]}");
            return false;
        }

        var uploadUrl = getUrlJson["upload_url"]?.GetValue<string>();
        var fileId = getUrlJson["file_id"]?.GetValue<string>();
        if (string.IsNullOrEmpty(uploadUrl) || string.IsNullOrEmpty(fileId))
        {
            Console.WriteLine("[SLACK] Missing upload_url or file_id in response");
            return false;
        }

        Console.WriteLine($"[SLACK] Uploading {fileName} ({fileSize:N0} bytes)...");

        // Step 2: Upload file content via POST to the upload URL
        using var fileContent = new StreamContent(File.OpenRead(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        var uploadResp = await http.PostAsync(uploadUrl, fileContent);
        if (!uploadResp.IsSuccessStatusCode)
        {
            Console.WriteLine($"[SLACK] File upload failed: HTTP {uploadResp.StatusCode}");
            return false;
        }

        // Step 3: Complete the upload (share to channel)
        var completePayload = new JsonObject
        {
            ["files"] = new JsonArray(new JsonObject
            {
                ["id"] = fileId,
                ["title"] = title
            })
        };

        if (!string.IsNullOrEmpty(channel))
            completePayload["channel_id"] = channel;
        if (!string.IsNullOrEmpty(threadTs))
            completePayload["thread_ts"] = threadTs;
        if (!string.IsNullOrEmpty(initialComment))
            completePayload["initial_comment"] = initialComment;

        using var completeReq = new HttpRequestMessage(HttpMethod.Post,
            "https://slack.com/api/files.completeUploadExternal");
        completeReq.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        completeReq.Content = new StringContent(completePayload.ToJsonString(),
            System.Text.Encoding.UTF8, "application/json");

        var completeResp = await http.SendAsync(completeReq);
        var completeBody = await completeResp.Content.ReadAsStringAsync();
        var completeJson = JsonSerializer.Deserialize<JsonNode>(completeBody);

        if (completeJson?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] completeUploadExternal failed: {completeJson?["error"]}");
            return false;
        }

        Console.WriteLine($"[SLACK] File uploaded: {fileName}");
        return true;
    }

    /// <summary>
    /// Upload a file to Slack.
    /// Usage: wkappbot slack upload &lt;file-path&gt; [--channel ID] [--thread TS] [--title "name"]
    /// </summary>
    static int SlackUploadCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack upload <file-path> [--channel ID] [--thread TS] [--title \"name\"]");
            return 1;
        }

        string? filePath = null;
        string? explicitChannel = null;
        string? threadTs = null;
        string? title = null;
        string? comment = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if (args[i] == "--thread" && i + 1 < args.Length)
                threadTs = args[++i];
            else if (args[i] == "--title" && i + 1 < args.Length)
                title = args[++i];
            else if (args[i] == "--comment" && i + 1 < args.Length)
                comment = args[++i];
            else if (filePath == null)
                filePath = args[i];
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("[SLACK] ERROR: file path required");
            return 1;
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

        var ok = SlackUploadFileAsync(botToken, channel, filePath, title, threadTs, comment)
            .GetAwaiter().GetResult();

        return ok ? 0 : 1;
    }

    /// <summary>
    /// Capture a screenshot and upload to Slack.
    /// Usage: wkappbot slack screenshot [window-title] [--channel ID] [--thread TS]
    /// </summary>
    static int SlackScreenshotCommand(string[] args)
    {
        string? windowTitle = null;
        string? explicitChannel = null;
        string? threadTs = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if (args[i] == "--thread" && i + 1 < args.Length)
                threadTs = args[++i];
            else if (windowTitle == null)
                windowTitle = args[i];
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

        // Capture screenshot
        string screenshotPath;
        var outputDir = Path.Combine(DataDir, "output", "screenshots");
        Directory.CreateDirectory(outputDir);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        if (!string.IsNullOrEmpty(windowTitle))
        {
            // Capture specific window
            var windows = WKAppBot.Win32.Window.WindowFinder.FindByTitle(windowTitle);
            if (windows.Count == 0)
            {
                Console.WriteLine($"[SLACK] Window not found: {windowTitle}");
                return 1;
            }

            var hwnd = windows[0].Handle;
            var safeTitle = windowTitle.Replace(" ", "_").Replace("/", "_").Replace("\\", "_");
            screenshotPath = Path.Combine(outputDir, $"slack_{timestamp}_{safeTitle}.png");
            var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hwnd);
            if (bmp == null)
            {
                Console.WriteLine("[SLACK] Screenshot capture failed");
                return 1;
            }
            bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
            Console.WriteLine($"[SLACK] Captured: {windows[0].Title} -> {screenshotPath}");
        }
        else
        {
            // Capture entire primary screen
            screenshotPath = Path.Combine(outputDir, $"slack_{timestamp}_screen.png");
            var bounds = System.Windows.Forms.Screen.PrimaryScreen!.Bounds;
            using var bmp = new System.Drawing.Bitmap(bounds.Width, bounds.Height);
            using var g = System.Drawing.Graphics.FromImage(bmp);
            g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
            bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"[SLACK] Captured: full screen -> {screenshotPath}");
        }

        // Upload to Slack
        var title = !string.IsNullOrEmpty(windowTitle)
            ? $"Screenshot: {windowTitle}"
            : "Screenshot: Full Screen";

        var ok = SlackUploadFileAsync(botToken, channel, screenshotPath, title, threadTs)
            .GetAwaiter().GetResult();

        return ok ? 0 : 1;
    }

    static JsonNode? LoadSlackConfig()
    {
        if (!File.Exists(SlackConfigPath))
        {
            Console.WriteLine($"[SLACK] Config not found: {SlackConfigPath}");
            Console.WriteLine("[SLACK] Run Slack setup first (see CLAUDE.md)");
            return null;
        }

        var json = File.ReadAllText(SlackConfigPath);
        return JsonSerializer.Deserialize<JsonNode>(json);
    }

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

        Console.WriteLine($"[SLACK] {lines.Length} message(s) in inbox:");
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
    /// Usage: wkappbot slack reply "response text" [--channel C0AFXJBMB2N] [--thread 1234567890.123456]
    ///   --channel: target channel ID (default: from latest inbox message or config)
    ///   --thread: reply in-thread to the original message (Slack thread_ts)
    /// </summary>
    static int SlackReplyCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack reply \"response text\" [--channel ID] [--thread TS]");
            return 1;
        }

        // Parse --channel and --thread flags
        string? explicitChannel = null;
        string? threadTs = null;
        var textParts = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
            {
                explicitChannel = args[++i];
            }
            else if (args[i] == "--thread" && i + 1 < args.Length)
            {
                threadTs = args[++i];
            }
            else
            {
                textParts.Add(args[i]);
            }
        }

        var replyText = string.Join(" ", textParts);
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

        var (ok, _) = SlackSendViaApi(botToken, channel, replyText, threadTs).GetAwaiter().GetResult();

        var threadNote = !string.IsNullOrEmpty(threadTs) ? " (in-thread)" : "";
        if (ok)
        {
            Console.WriteLine($"[SLACK] Replied to #{channel}{threadNote}: {replyText}");
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
        Console.WriteLine($"[SLACK] Found prompt: {prompt.HostType} ({prompt.ProcessName})");
        Console.WriteLine($"[SLACK]   Window: {prompt.WindowTitle}");
        Console.WriteLine($"[SLACK]   Rect: ({prompt.PromptRect.X},{prompt.PromptRect.Y} {prompt.PromptRect.Width}x{prompt.PromptRect.Height})");
        Console.ResetColor();

        if (!watchMode)
        {
            // One-shot: process current inbox
            return ProcessInboxToPrompt(helper, prompt);
        }

        // Watch mode: continuously poll inbox
        Console.WriteLine($"[SLACK] Watch mode: polling inbox every {intervalSec}s (Ctrl+C to stop)");

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

        Console.WriteLine($"[SLACK] {lines.Length} message(s) to forward to Claude prompt");

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
                var replyCmd = $"wkappbot slack reply \"your response\" --channel {channel} --thread {ts}";
                var promptText = $"{text}\n\n(Slack @{user} #{channel} — reply: {replyCmd})";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[SLACK] >> Typing into prompt: {promptText}");
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
                Console.WriteLine($"[SLACK] Error processing inbox message: {ex.Message}");
            }
        }

        // Clear inbox after processing
        try { File.Delete(SlackInboxFile); } catch { }
        Console.WriteLine("[SLACK] Inbox cleared");

        return 0;
    }

    /// <summary>Write a mention to inbox file for Claude Code to pick up.</summary>
    static void WriteInbox(string channel, string user, string text, string ts)
    {
        try
        {
            var entry = JsonSerializer.Serialize(new
            {
                channel,
                user,
                text,
                ts,
                time = DateTime.Now.ToString("HH:mm:ss")
            });
            File.AppendAllText(SlackInboxFile, entry + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] Failed to write inbox: {ex.Message}");
        }
    }

    /// <summary>Save last processed message timestamp per channel.</summary>
    static void SaveLastTs(string channel, string ts)
    {
        try
        {
            // Format: one line per channel — "channel_id=timestamp"
            var entries = new Dictionary<string, string>();
            if (File.Exists(SlackLastTsFile))
            {
                foreach (var line in File.ReadAllLines(SlackLastTsFile))
                {
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2) entries[parts[0]] = parts[1];
                }
            }
            entries[channel] = ts;
            File.WriteAllLines(SlackLastTsFile, entries.Select(kv => $"{kv.Key}={kv.Value}"));
        }
        catch { }
    }

    /// <summary>Load last processed timestamp for a channel.</summary>
    static string? LoadLastTs(string channel)
    {
        try
        {
            if (!File.Exists(SlackLastTsFile)) return null;
            foreach (var line in File.ReadAllLines(SlackLastTsFile))
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2 && parts[0] == channel) return parts[1];
            }
        }
        catch { }
        return null;
    }

    /// <summary>Save reply context so 'slack reply "text"' works without --channel/--thread.</summary>
    static void SaveReplyContext(string channel, string threadTs)
    {
        try { File.WriteAllText(SlackReplyContextFile, $"channel={channel}\nthread={threadTs}"); }
        catch { }
    }

    /// <summary>Check if the message is an accept/approve command.</summary>
    static bool IsAcceptCommand(string text)
    {
        var trimmed = text.Trim().ToLowerInvariant();
        return AcceptKeywords.Any(kw => trimmed == kw.ToLowerInvariant());
    }

    /// <summary>Send Ctrl+Enter to Claude Code prompt (remote approval via Slack).</summary>
    static void SendAcceptKeystroke(string botToken, string channel, string threadTs)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[SLACK] >> Accept command received — sending Ctrl+Enter to Claude prompt");
        Console.ResetColor();

        try
        {
            KeyboardInput.Hotkey(new[] { "ctrl", "enter" });
            Console.WriteLine("[SLACK] >> Ctrl+Enter sent!");
            SlackSendViaApi(botToken, channel, "Ctrl+Enter 전송 완료! 수락 처리했습니다.", threadTs)
                .GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] >> Accept keystroke failed: {ex.Message}");
            SlackSendViaApi(botToken, channel, $"수락 전송 실패: {ex.Message}", threadTs)
                .GetAwaiter().GetResult();
        }
    }

    /// <summary>Load saved reply context (channel, threadTs).</summary>
    static (string? channel, string? threadTs) LoadReplyContext()
    {
        string? channel = null, threadTs = null;
        try
        {
            if (!File.Exists(SlackReplyContextFile)) return (null, null);
            foreach (var line in File.ReadAllLines(SlackReplyContextFile))
            {
                var parts = line.Split('=', 2);
                if (parts.Length != 2) continue;
                if (parts[0] == "channel") channel = parts[1];
                else if (parts[0] == "thread") threadTs = parts[1];
            }
        }
        catch { }
        return (channel, threadTs);
    }

    /// <summary>
    /// Fetch missed messages from channel history since last processed timestamp.
    /// Usage: wkappbot slack catch-up [--channel ID] [--limit N] [--prompt]
    /// </summary>
    static int SlackCatchUpCommand(string[] args)
    {
        string? explicitChannel = null;
        int limit = 20;
        bool toPrompt = args.Contains("--prompt");

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
            Console.WriteLine($"[SLACK] Fetching messages after ts={lastTs}");
        else
            Console.WriteLine($"[SLACK] No bookmark — fetching last {limit} messages");

        var messages = SlackFetchHistoryAsync(botToken, channel, lastTs, limit).GetAwaiter().GetResult();

        if (messages.Count == 0)
        {
            Console.WriteLine("[SLACK] No new messages");
            return 0;
        }

        // Get bot user ID to filter own messages
        var botUserId = SlackGetBotUserId(botToken);

        Console.WriteLine($"[SLACK] {messages.Count} message(s) fetched:");

        // Messages come newest-first from API, reverse to chronological order
        messages.Reverse();

        string? newestTs = null;
        int forwarded = 0;

        // Initialize prompt helper if --prompt
        ClaudePromptHelper? promptHelper = null;
        if (toPrompt)
        {
            promptHelper = new ClaudePromptHelper();
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
                    var replyHint = $"wkappbot slack reply \"your response\" --channel {channel} --thread {replyThread}";
                    var promptText = $"{cleanText}\n\n(Slack @{user} #{channel} — reply: {replyHint})";

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
            Console.WriteLine($"[SLACK] Bookmark updated: ts={newestTs}");
        }

        if (forwarded > 0)
            Console.WriteLine($"[SLACK] {forwarded} @mention(s) forwarded");
        else
            Console.WriteLine("[SLACK] No @mentions to forward");

        promptHelper?.Dispose();
        return 0;
    }

    /// <summary>Fetch channel history via conversations.history API.</summary>
    static async Task<List<JsonNode>> SlackFetchHistoryAsync(string botToken, string channel,
        string? oldest = null, int limit = 20)
    {
        using var http = new HttpClient();
        var url = $"https://slack.com/api/conversations.history?channel={channel}&limit={limit}";
        if (!string.IsNullOrEmpty(oldest))
            url += $"&oldest={oldest}";

        using var req = new HttpRequestMessage(HttpMethod.Get, url);
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        if (json?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] conversations.history failed: {json?["error"]}");
            return new List<JsonNode>();
        }

        var messages = json["messages"]?.AsArray();
        if (messages == null) return new List<JsonNode>();

        return messages.Where(m => m != null).Select(m => m!).ToList();
    }

    /// <summary>Get bot user ID via auth.test (sync helper).</summary>
    static string? SlackGetBotUserId(string botToken)
    {
        using var http = new HttpClient();
        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/auth.test");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        var resp = http.Send(req);
        using var reader = new StreamReader(resp.Content.ReadAsStream());
        var json = JsonSerializer.Deserialize<JsonNode>(reader.ReadToEnd());
        return json?["user_id"]?.GetValue<string>();
    }

    /// <summary>
    /// When Claude prompt is lost (FindPrompt returns null), capture foreground window screenshot
    /// and send it to Slack so the user can see what's blocking the prompt (e.g. permission dialog).
    /// Also writes the original message to inbox as fallback.
    /// </summary>
    static void HandlePromptLost(string botToken, string channel, string threadTs,
        string user, string cleanText, string ts)
    {
        Console.WriteLine("[SLACK] >> Prompt lost! Diagnosing foreground window...");

        try
        {
            var fgWindow = WKAppBot.Win32.Native.NativeMethods.GetForegroundWindow();
            if (fgWindow != IntPtr.Zero)
            {
                // Get foreground window info
                var fgTitle = WKAppBot.Win32.Window.WindowFinder.GetWindowText(fgWindow);
                var fgClass = WKAppBot.Win32.Window.WindowFinder.GetClassName(fgWindow);
                Console.WriteLine($"[SLACK]    Foreground: \"{fgTitle}\" (class={fgClass})");

                // Capture foreground window screenshot
                var outputDir = Path.Combine(DataDir, "output", "screenshots");
                Directory.CreateDirectory(outputDir);
                var screenshotPath = Path.Combine(outputDir,
                    $"prompt_blocked_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(fgWindow);
                if (bmp != null && !WKAppBot.Win32.Input.ScreenCapture.IsBlankBitmap(bmp))
                {
                    bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                    Console.WriteLine($"[SLACK]    Screenshot: {screenshotPath}");

                    // Upload to Slack thread with diagnostic info
                    var comment = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";

                    SlackUploadFileAsync(botToken, channel, screenshotPath,
                        $"Blocked: {fgTitle}", threadTs, comment).GetAwaiter().GetResult();
                }
                else
                {
                    bmp?.Dispose();
                    // Screenshot failed, send text message instead
                    var msg = $"Claude 프롬프트를 찾을 수 없어요!\n" +
                        $"전경 윈도우: \"{fgTitle}\" (class={fgClass})\n" +
                        $"원래 메시지: {cleanText}\n\n" +
                        $"승인 다이얼로그가 떠있으면 수락해주세요!";
                    SlackSendViaApi(botToken, channel, msg, threadTs).GetAwaiter().GetResult();
                }
            }
            else
            {
                // No foreground window — just notify
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! 원래 메시지: {cleanText}",
                    threadTs).GetAwaiter().GetResult();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK]    Diagnosis failed: {ex.Message}");
            // Fallback: text notification
            try
            {
                SlackSendViaApi(botToken, channel,
                    $"Claude 프롬프트를 찾을 수 없어요! (진단 실패: {ex.Message})\n원래 메시지: {cleanText}",
                    threadTs).GetAwaiter().GetResult();
            }
            catch { }
        }

        // Always write to inbox as fallback
        WriteInbox(channel, user, cleanText, ts);
    }

    /// <summary>
    /// WebBot monitor tick — called every ~6 seconds from the listen loop.
    /// Checks if Claude is busy, then streams WebBot page content to Slack.
    /// Content extracted via UIA (Accessibility) instead of CDP JavaScript.
    /// </summary>
    static void WebBotMonitorTick(
        string botToken, string channel,
        ref CdpClient? cdp, ref string? statusTs,
        ref string? lastUrl, ref string? lastContent,
        ref bool claudeBusy, ref int reconnectBackoff,
        ref IntPtr chromeHwnd,
        ClaudePromptHelper? promptHelper)
    {
        // 1. Check if Claude is busy (UIA: "중단" button visible)
        bool nowBusy = IsClaudeBusy(promptHelper);

        if (nowBusy && !claudeBusy)
        {
            // Transition: idle → busy
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[SLACK] WebBot: Claude is busy — starting status stream");
            Console.ResetColor();

            // Get Claude's latest status text (what tool is being used)
            var statusText = GetClaudeStatusText(promptHelper);
            if (!string.IsNullOrEmpty(statusText))
                Console.WriteLine($"[SLACK] WebBot: Claude status: {statusText}");
        }
        else if (!nowBusy && claudeBusy)
        {
            // Transition: busy → idle
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SLACK] WebBot: Claude is done — stopping status stream");
            Console.ResetColor();

            // Update final status message
            if (statusTs != null)
            {
                var doneMsg = FormatWebBotStatus(lastUrl, lastContent, "Claude 응답 완료");
                SlackUpdateMessageAsync(botToken, channel, statusTs, doneMsg).GetAwaiter().GetResult();
            }
        }

        claudeBusy = nowBusy;

        // Only stream when Claude is busy
        if (!claudeBusy)
            return;

        // 2. Connect to CDP if not connected (for URL retrieval)
        if (cdp == null || !cdp.IsConnected)
        {
            if (reconnectBackoff > 0)
            {
                reconnectBackoff--;
                return;
            }

            try
            {
                cdp?.Dispose();
                cdp = new CdpClient();
                cdp.ConnectAsync(9222).GetAwaiter().GetResult();
                Console.WriteLine("[SLACK] WebBot: Reconnected to Chrome");
                reconnectBackoff = 0;

                // Find Chrome window handle for UIA content extraction
                if (cdp.ChromePid > 0)
                    chromeHwnd = FindChromeHwndByPid(cdp.ChromePid);
            }
            catch
            {
                cdp?.Dispose();
                cdp = null;
                reconnectBackoff = Math.Min(reconnectBackoff + 2, 15); // backoff: 4s → 8s → ... → 30s
                return;
            }
        }

        // 3. Get URL from CDP (fast, lightweight)
        string? url = null;
        try
        {
            var urlTask = cdp!.GetUrlAsync();
            if (urlTask.Wait(3000))
                url = urlTask.Result;
        }
        catch { }

        // 4. Get content via UIA (Accessibility) — "장애인의 눈"
        string? content = null;
        if (chromeHwnd == IntPtr.Zero && cdp?.ChromePid > 0)
            chromeHwnd = FindChromeHwndByPid(cdp.ChromePid);

        if (chromeHwnd != IntPtr.Zero)
        {
            content = ExtractWebContentViaUia(chromeHwnd, 400);
            if (!string.IsNullOrEmpty(content))
            {
                // Log UIA content to console for debugging
                var preview = content.Length > 80 ? content[..77] + "..." : content;
                Console.WriteLine($"[SLACK] WebBot [UIA]: {preview}");
            }
        }

        if (string.IsNullOrEmpty(url) && string.IsNullOrEmpty(content))
            return;

        // 5. Check if anything changed
        bool urlChanged = url != lastUrl;
        bool contentChanged = content != lastContent;
        if (!urlChanged && !contentChanged && statusTs != null)
            return; // nothing new

        lastUrl = url;
        lastContent = content;

        // 6. Get Claude's current status for the message
        var claudeStatus = GetClaudeStatusText(promptHelper);
        var message = FormatWebBotStatus(url, content, claudeStatus);

        // 7. Send or update Slack message
        if (statusTs == null)
        {
            // First message — create new
            var (ok, ts) = SlackSendViaApi(botToken, channel, message).GetAwaiter().GetResult();
            if (ok && ts != null)
            {
                statusTs = ts;
                Console.WriteLine($"[SLACK] WebBot: Status message created (ts={ts})");
            }
        }
        else
        {
            // Update existing message
            var (ok, _) = SlackUpdateMessageAsync(botToken, channel, statusTs, message).GetAwaiter().GetResult();
            if (!ok)
            {
                // Message may have been deleted — create new one
                var (ok2, ts2) = SlackSendViaApi(botToken, channel, message).GetAwaiter().GetResult();
                if (ok2 && ts2 != null)
                    statusTs = ts2;
            }
        }
    }

    /// <summary>Find Chrome main window handle by PID.</summary>
    static IntPtr FindChromeHwndByPid(int pid)
    {
        if (pid <= 0) return IntPtr.Zero;

        IntPtr found = IntPtr.Zero;
        var sb = new System.Text.StringBuilder(256);
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint wpid);
            if ((int)wpid != pid) return true;

            // Check for WS_CAPTION (main window, not popup)
            var style = NativeMethods.GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            found = hwnd;
            return false;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>
    /// Check if a thread's parent message was sent by our bot.
    /// Uses conversations.replies API to get the parent message and check for bot_id.
    /// Results are cached in ownMessageTimestamps to avoid repeated API calls.
    /// </summary>
    static bool IsOwnThread(string botToken, string channel, string threadTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // Get only the parent message (limit=1, inclusive=true)
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return false;

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return false;

            var parent = messages[0];
            // Bot messages have "bot_id" field
            var botId = parent?["bot_id"]?.GetValue<string>();
            return !string.IsNullOrEmpty(botId);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get the bot's parent message text from a thread (for context when replying).
    /// Returns the first message in the thread (the bot's original message).
    /// </summary>
    static string? GetThreadParentText(string botToken, string channel, string threadTs)
    {
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=1&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true) return null;

            var messages = json["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return null;

            return messages[0]?["text"]?.GetValue<string>();
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Format WebBot status message for Slack (text only, no images).</summary>
    static string FormatWebBotStatus(string? url, string? content, string? claudeStatus)
    {
        var sb = new System.Text.StringBuilder();

        // Header: Claude status
        if (!string.IsNullOrEmpty(claudeStatus))
            sb.AppendLine($"*[{claudeStatus}]* {DateTime.Now:HH:mm:ss}");
        else
            sb.AppendLine($"*[현황]* {DateTime.Now:HH:mm:ss}");

        // URL
        if (!string.IsNullOrEmpty(url))
        {
            // Shorten URL for display
            try
            {
                var uri = new Uri(url);
                var shortUrl = uri.Host + uri.AbsolutePath;
                if (shortUrl.Length > 60) shortUrl = shortUrl[..57] + "...";
                sb.AppendLine($"`{shortUrl}`");
            }
            catch
            {
                sb.AppendLine($"`{url}`");
            }
        }

        sb.AppendLine("──────────────────────");

        // Content
        if (!string.IsNullOrEmpty(content))
            sb.Append(content);
        else
            sb.Append("(페이지 내용 없음)");

        return sb.ToString();
    }

    /// <summary>
    /// Check if Claude Desktop is currently busy (generating response / compacting).
    /// Detection: UIA tree has a "중단" (Stop) button visible when Claude is working.
    /// </summary>
    static bool IsClaudeBusy(ClaudePromptHelper? promptHelper)
    {
        if (promptHelper == null) return false;

        try
        {
            // Find Claude Desktop window
            var claudeHwnd = FindClaudeDesktopHwnd();
            if (claudeHwnd == IntPtr.Zero) return false;

            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return false;

            // Look for "중단" button in the turn-form group
            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Method 1: Find "중단" button by name (most reliable)
            var stopButton = window.FindFirstDescendant(
                cf.ByControlType(ControlType.Button).And(cf.ByName("중단")));
            if (stopButton != null)
                return true;

            // Method 2: Check for turn-form group with "Elucidating..." or similar loading text
            // (visible during context compaction)
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get Claude's current status text from UIA StatusBar elements.
    /// Returns the most recent status (e.g., "도구 사용함", "파일 읽음", "명령 실행함").
    /// </summary>
    static string? GetClaudeStatusText(ClaudePromptHelper? promptHelper)
    {
        if (promptHelper == null) return null;

        try
        {
            var claudeHwnd = FindClaudeDesktopHwnd();
            if (claudeHwnd == IntPtr.Zero) return null;

            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Find ALL StatusBar elements (they contain the tool usage descriptions)
            var statusBars = window.FindAllDescendants(cf.ByControlType(ControlType.StatusBar));
            if (statusBars == null || statusBars.Length == 0) return null;

            // The last visible StatusBar with positive Y coordinate is the most recent action
            string? latestText = null;
            int maxY = int.MinValue;

            foreach (var sb in statusBars)
            {
                try
                {
                    var rect = sb.BoundingRectangle;
                    // Only consider elements on-screen (Y > 0) and in the Claude window area
                    if (rect.Y < 0 || rect.Height < 1) continue;

                    // Get text from child Text element
                    var textChild = sb.FindFirstChild(cf.ByControlType(ControlType.Text));
                    if (textChild == null) continue;

                    var text = textChild.Name;
                    if (string.IsNullOrEmpty(text)) continue;

                    if (rect.Y > maxY)
                    {
                        maxY = (int)rect.Y;
                        latestText = text;
                    }
                }
                catch { continue; }
            }

            return latestText;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Find Claude Desktop main window handle.</summary>
    static IntPtr FindClaudeDesktopHwnd()
    {
        IntPtr found = IntPtr.Zero;
        var sb = new System.Text.StringBuilder(256);

        NativeMethods.EnumWindows((hwnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;

            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;

            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            try
            {
                var proc = Process.GetProcessById((int)pid);
                if (!proc.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            catch { return true; }

            // Check for WS_CAPTION + sizable window (main window, not popup)
            var style = NativeMethods.GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            NativeMethods.GetWindowRect(hwnd, out var rect);
            var w = rect.Right - rect.Left;
            var h = rect.Bottom - rect.Top;
            if (w > 400 && h > 300)
            {
                found = hwnd;
                return false;
            }

            return true;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>
    /// Update an existing Slack message via chat.update API.
    /// Returns (ok, ts) — ts is the message timestamp.
    /// </summary>
    static async Task<(bool ok, string? ts)> SlackUpdateMessageAsync(
        string botToken, string channel, string ts, string text)
    {
        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, ts, text });
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.update");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] chat.update error: {json?["error"]}");

        return (ok, json?["ts"]?.GetValue<string>());
    }

    static int SlackUsage()
    {
        Console.WriteLine("Usage: wkappbot slack <command>");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  listen [--bg] [--ai] [--prompt] [--keywords] [--webbot]");
        Console.WriteLine("                        Socket Mode: listen for @mentions and reply");
        Console.WriteLine("                        --bg: run as background daemon process");
        Console.WriteLine("                        --ai: use Claude API for intelligent responses");
        Console.WriteLine("                        --prompt: type @mentions into Claude Code prompt");
        Console.WriteLine("                        --keywords/-k: engage when keywords detected");
        Console.WriteLine("                        --webbot: stream WebBot status to Slack when Claude is busy");
        Console.WriteLine("  send \"message\"      Send a message to the configured channel");
        Console.WriteLine("  test                Test Slack connection (auth + send + socket)");
        Console.WriteLine("  status              Check if background listener is running");
        Console.WriteLine("  stop                Stop background listener");
        Console.WriteLine("  inbox               Show unread @mention messages");
        Console.WriteLine("  reply \"text\" [--channel ID] [--thread TS]");
        Console.WriteLine("                        Reply to Slack and clear inbox");
        Console.WriteLine("                        --channel: target channel (default: from inbox/config)");
        Console.WriteLine("                        --thread: reply in-thread (Slack message timestamp)");
        Console.WriteLine("  upload <file> [--channel ID] [--thread TS] [--title \"name\"]");
        Console.WriteLine("                        Upload a file to Slack channel");
        Console.WriteLine("  screenshot [window-title] [--channel ID] [--thread TS]");
        Console.WriteLine("                        Capture screenshot and upload to Slack");
        Console.WriteLine("                        (no title = full screen capture)");
        Console.WriteLine("  catch-up [--channel ID] [--limit N] [--prompt]");
        Console.WriteLine("                        Fetch missed messages since last bookmark");
        Console.WriteLine("                        --prompt: forward @mentions to Claude Code prompt");
        Console.WriteLine("  prompt [--watch]    Type inbox messages into Claude Code prompt");
        Console.WriteLine("                      --watch: continuously poll for new messages");
        Console.WriteLine("                      --interval N: poll interval seconds (default: 3)");
        Console.WriteLine();
        Console.WriteLine($"Config: {SlackConfigPath}");
        return 1;
    }
}
