using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;
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
    static string SlackInboxFile => Path.Combine(DataDir, "slack_inbox.jsonl");
    static string SlackLastTsFile => Path.Combine(DataDir, "slack_last_ts.txt");

    /// <summary>Socket Mode: listen for events and respond to @mentions.</summary>
    static int SlackListenCommand(string[] args)
    {
        bool background = args.Contains("--bg") || args.Contains("--background") || args.Contains("-d");
        bool aiMode = args.Contains("--ai");
        bool promptMode = args.Contains("--prompt");

        // --bg: launch self as background process (pass flags through)
        if (background)
            return SlackLaunchBackground(aiMode, promptMode);

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
        // Key = thread_ts, Value = channel
        var activeThreads = new HashSet<string>();

        // Handle @mentions — AI / inbox / echo
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

            // Track this thread so we receive follow-up replies without @mention
            // thread_ts = the original message's ts (thread parent)
            var threadKey = msg.ThreadTs ?? msg.Timestamp;  // if already in thread, use thread_ts; else this msg starts the thread
            activeThreads.Add(threadKey);

            // Prompt mode: type directly into Claude Code prompt
            if (promptHelper != null && promptInfo != null)
            {
                // Include thread_ts so Claude Code can reply in-thread
                var replyHint = $"wkappbot slack reply \"your response\" --channel {msg.Channel} --thread {threadKey}";
                var promptText = $"{cleanText}\n\n(Slack @{msg.User} #{msg.Channel} — reply: {replyHint})";
                Console.WriteLine($"[SLACK] >> Typing into Claude prompt...");

                // Re-find prompt (window may have moved/resized)
                var fresh = promptHelper.FindPrompt();
                if (fresh != null)
                {
                    promptHelper.TypeAndSubmit(fresh, promptText);
                    Console.WriteLine("[SLACK] >> Sent to Claude prompt");
                }
                else
                {
                    Console.WriteLine("[SLACK] >> Prompt lost! Writing to inbox instead");
                    WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);
                }
                return;
            }

            // Inbox + AI/echo fallback
            WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);

            string reply;
            if (ai != null)
            {
                // Claude AI response
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
                // No AI — just ack, Claude Code will reply via 'slack reply'
                reply = null!;
            }

            if (!string.IsNullOrEmpty(reply))
            {
                Console.WriteLine($"[SLACK] >> {reply}");
                slack.SendAsync(msg.Channel, reply).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("[SLACK] >> (queued for Claude Code)");
            }
        };

        // Handle channel messages — forward thread replies to prompt
        slack.OnMessage += (msg) =>
        {
            // Check if this is a thread reply to a conversation we're tracking
            if (msg.ThreadTs != null && activeThreads.Contains(msg.ThreadTs))
            {
                Console.WriteLine($"[SLACK] << thread reply from {msg.User}: {msg.Text}");
                SaveLastTs(msg.Channel, msg.Timestamp);

                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (string.IsNullOrEmpty(cleanText)) return;

                // Prompt mode: forward to Claude Code
                if (promptHelper != null && promptInfo != null)
                {
                    var replyHint = $"wkappbot slack reply \"your response\" --channel {msg.Channel} --thread {msg.ThreadTs}";
                    var promptText = $"{cleanText}\n\n(Slack @{msg.User} #{msg.Channel} thread — reply: {replyHint})";
                    Console.WriteLine($"[SLACK] >> Typing thread reply into Claude prompt...");

                    var fresh = promptHelper.FindPrompt();
                    if (fresh != null)
                    {
                        promptHelper.TypeAndSubmit(fresh, promptText);
                        Console.WriteLine("[SLACK] >> Sent to Claude prompt");
                    }
                    else
                    {
                        Console.WriteLine("[SLACK] >> Prompt lost! Writing to inbox instead");
                        WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);
                    }
                    return;
                }

                // Fallback: write to inbox
                WriteInbox(msg.Channel, msg.User, cleanText, msg.Timestamp);
                return;
            }

            Console.WriteLine($"[SLACK] msg [{msg.Channel}] {msg.User}: {msg.Text}");
        };

        // Wait until cancelled
        try { Task.Delay(-1, cts.Token).GetAwaiter().GetResult(); }
        catch (OperationCanceledException) { }

        slack.DisconnectAsync().GetAwaiter().GetResult();
        ai?.Dispose();
        promptHelper?.Dispose();
        DeletePidFile();
        return 0;
    }

    /// <summary>Launch wkappbot slack listen as a background process.</summary>
    static int SlackLaunchBackground(bool aiMode = false, bool promptMode = false)
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

    /// <summary>Stop background Slack listener.</summary>
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
            proc.Kill(entireProcessTree: true);
            proc.WaitForExit(5000);
            Console.WriteLine($"[SLACK] Listener stopped (PID={pid})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] Failed to stop PID={pid}: {ex.Message}");
        }

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
        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found in config");
            return 1;
        }

        var ok = SlackSendViaApi(botToken, channel, message).GetAwaiter().GetResult();

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
            var sendOk = SlackSendViaApi(botToken!, channel, "WKAppBot test — connection verified!").GetAwaiter().GetResult();
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

    /// <summary>Send message via chat.postMessage API. If threadTs is provided, replies in-thread.</summary>
    static async Task<bool> SlackSendViaApi(string botToken, string channel, string text, string? threadTs = null)
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

        return ok;
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
        if (string.IsNullOrWhiteSpace(replyText))
        {
            Console.WriteLine("[SLACK] ERROR: reply text is empty");
            return 1;
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();

        // Channel priority: --channel flag > inbox last message > config default
        string? channel = explicitChannel;

        if (string.IsNullOrEmpty(channel) && File.Exists(SlackInboxFile))
        {
            try
            {
                var lines = File.ReadAllLines(SlackInboxFile);
                if (lines.Length > 0)
                {
                    var lastMsg = JsonSerializer.Deserialize<JsonNode>(lines[^1]);
                    channel = lastMsg?["channel"]?.GetValue<string>();
                    // Also get thread_ts from inbox if not explicitly provided
                    threadTs ??= lastMsg?["ts"]?.GetValue<string>();
                }
            }
            catch { }
        }

        channel ??= config["channel"]?.GetValue<string>();

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found");
            return 1;
        }

        var ok = SlackSendViaApi(botToken, channel, replyText, threadTs).GetAwaiter().GetResult();

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

    static int SlackUsage()
    {
        Console.WriteLine("Usage: wkappbot slack <command>");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  listen [--bg] [--ai] [--prompt]");
        Console.WriteLine("                        Socket Mode: listen for @mentions and reply");
        Console.WriteLine("                        --bg: run as background daemon process");
        Console.WriteLine("                        --ai: use Claude API for intelligent responses");
        Console.WriteLine("                        --prompt: type @mentions into Claude Code prompt");
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
