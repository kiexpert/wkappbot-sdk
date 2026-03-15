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
// Split into multiple files:
//   SlackCommands.cs         — dispatcher, config, helpers, send/test/status/stop
//   SlackListenCommand.cs    — listen loop + background launcher
//   SlackUploadCommands.cs   — file upload + screenshot
//   SlackPromptCommands.cs   — prompt, reply, inbox, catch-up
//   SlackMonitorCommands.cs  — WebBot monitor, Claude busy detection, prompt lost handler
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
            "route" => SlackRouteCommand(args[1..]),
            "catch-up" or "catchup" => SlackCatchUpCommand(args),
            "prompt" => SlackPromptCommand(args),
            "schedule" => SlackScheduleCommand(args),
            "list" or "ls" => SlackListCommand(args),
            "delete" or "del" => SlackDeleteCommand(args),
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
        "클롣", "클롯", "클로드", "앱봇",          // Korean + typos (클롣 = primary)
        "claude", "appbot", "wkappbot", "클봇",    // English + mixed
    };

    /// <summary>Keywords that trigger Ctrl+Enter (accept/approve) on Claude Code prompt.</summary>
    static readonly string[] AcceptKeywords = new[]
    {
        "수락", "승인", "ㅅㄹ", "accept", "approve", "yes", "ㅇㅇ",
    };

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
            Console.WriteLine("Usage: wkappbot slack send \"message\" [file1.png] [\"message2\"] [file2.png] ...");
            return 1;
        }

        // Parse: text parts + file attachments (shared ParseTextAndFiles)
        var (textParts, filePaths) = ParseTextAndFiles(args, startIndex: 1);
        var message = string.Join("\n", textParts);
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

        // Send text message (get thread_ts for file uploads)
        string? threadTs = null;
        var senderName = GetSendReplyUsername(printDecision: true);
        if (!string.IsNullOrWhiteSpace(message))
        {
            var (ok, ts) = SlackSendViaApi(botToken, channel, message, username: senderName).GetAwaiter().GetResult();
            if (ok)
            {
                Console.WriteLine($"[SLACK] Sent: {message.Split('\n')[0]}{(textParts.Count > 1 ? $" (+{textParts.Count - 1} lines)" : "")}");
                threadTs = ts;
            }
            else
            {
                Console.WriteLine("[SLACK] Failed to send message");
                return 1;
            }
        }

        // Upload files (threaded to the message if we got a ts)
        foreach (var fp in filePaths)
        {
            var uploadArgs = new List<string> { "upload", fp };
            if (threadTs != null) { uploadArgs.Add("--msg"); uploadArgs.Add(threadTs); }
            SlackUploadCommand(uploadArgs.ToArray());
        }

        return 0;
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
            var (sendOk, _) = SlackSendViaApi(botToken!, channel, "WKAppBot test — connection verified!", username: BotUsername).GetAwaiter().GetResult();
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
    static async Task<(bool ok, string? ts)> SlackSendViaApi(string botToken, string channel, string text, string? threadTs = null, string? username = null)
    {
        using var http = new HttpClient();
        // Build payload with optional username override for multi-bot identification
        var dict = new Dictionary<string, object> { ["channel"] = channel, ["text"] = text };
        if (!string.IsNullOrEmpty(threadTs)) dict["thread_ts"] = threadTs;
        if (!string.IsNullOrEmpty(username)) dict["username"] = username;
        var payload = JsonSerializer.Serialize(dict);
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
    /// Respond to a Slack interactive action via response_url.
    /// Used to update the original message after a button click (e.g., disable buttons).
    /// </summary>
    static async Task SlackRespondViaUrl(string responseUrl, string text, bool replaceOriginal = true)
    {
        using var http = new HttpClient();
        var payload = new JsonObject
        {
            ["text"] = text,
            ["replace_original"] = replaceOriginal
        };
        var content = new StringContent(payload.ToJsonString(), System.Text.Encoding.UTF8, "application/json");
        try
        {
            await http.PostAsync(responseUrl, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] response_url error: {ex.Message}");
        }
    }

    /// <summary>
    /// Send a Block Kit message via chat.postMessage API.
    /// blocks is a JSON array string (serialized Block Kit blocks).
    /// </summary>
    static async Task<(bool ok, string? ts)> SlackSendBlocksViaApi(
        string botToken, string channel, string fallbackText, string blocksJson, string? threadTs = null)
    {
        using var http = new HttpClient();
        // Build JSON payload manually to include blocks array
        var payloadObj = new JsonObject
        {
            ["channel"] = channel,
            ["text"] = fallbackText,
            ["blocks"] = JsonNode.Parse(blocksJson)
        };
        if (!string.IsNullOrEmpty(threadTs))
            payloadObj["thread_ts"] = threadTs;

        var payload = payloadObj.ToJsonString();
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] Block API error: {json?["error"]}");

        var messageTs = json?["ts"]?.GetValue<string>();
        return (ok, messageTs);
    }

    /// <summary>
    /// Build Block Kit blocks JSON for plan approval message.
    /// Includes plan content in a code block + [수락] [피드백] action buttons.
    /// </summary>
    static string BuildPlanApprovalBlocks(string planContent, string sourceLabel)
    {
        // Slack Block Kit has 3000 char limit per text block — split if needed
        var truncPlan = planContent.Length > 2900
            ? planContent[..2900] + "\n... (truncated)"
            : planContent;

        var blocks = new JsonArray
        {
            // Header
            new JsonObject
            {
                ["type"] = "header",
                ["text"] = new JsonObject
                {
                    ["type"] = "plain_text",
                    ["text"] = $"플랜 승인 대기 (via {sourceLabel})"
                }
            },
            // Plan content
            new JsonObject
            {
                ["type"] = "section",
                ["text"] = new JsonObject
                {
                    ["type"] = "mrkdwn",
                    ["text"] = $"```\n{truncPlan}\n```"
                }
            },
            // Action buttons
            new JsonObject
            {
                ["type"] = "actions",
                ["elements"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "button",
                        ["text"] = new JsonObject
                        {
                            ["type"] = "plain_text",
                            ["text"] = "수락"
                        },
                        ["style"] = "primary",
                        ["action_id"] = "plan_approve",
                        ["value"] = "approve"
                    },
                    new JsonObject
                    {
                        ["type"] = "button",
                        ["text"] = new JsonObject
                        {
                            ["type"] = "plain_text",
                            ["text"] = "거절"
                        },
                        ["style"] = "danger",
                        ["action_id"] = "plan_reject",
                        ["value"] = "reject"
                    }
                }
            },
            // Context: hint for text-based approval
            new JsonObject
            {
                ["type"] = "context",
                ["elements"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "mrkdwn",
                        ["text"] = "버튼 또는 스레드 답글로 승인/피드백 가능 (`승인` `V` `ok` `ㅇ` `ㄱㄱ`)"
                    }
                }
            }
        };

        return blocks.ToJsonString();
    }

    /// <summary>
    /// Build Block Kit blocks JSON for permission prompt message.
    /// Dynamically creates buttons from the permission button names found via UIA.
    /// action_id = "perm_{buttonText}" so OnBlockAction can identify the click.
    /// </summary>
    static string BuildPermissionBlocks(List<string> buttonNames, string statusText)
    {
        var buttonElements = new JsonArray();
        foreach (var name in buttonNames)
        {
            // "Allow" style buttons → primary (green), "Deny" → danger (red), else default
            string? style = null;
            if (name.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                name.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                name.Contains("수락", StringComparison.OrdinalIgnoreCase))
                style = "primary";
            else if (name.Contains("Deny", StringComparison.OrdinalIgnoreCase) ||
                     name.Contains("거부", StringComparison.OrdinalIgnoreCase))
                style = "danger";

            var btn = new JsonObject
            {
                ["type"] = "button",
                ["text"] = new JsonObject
                {
                    ["type"] = "plain_text",
                    ["text"] = name
                },
                ["action_id"] = $"perm_{name.Replace(" ", "_").ToLowerInvariant()}",
                ["value"] = name // exact button text for UIA matching
            };
            if (style != null)
                btn["style"] = style;

            buttonElements.Add(btn);
        }

        var blocks = new JsonArray
        {
            // Header
            new JsonObject
            {
                ["type"] = "header",
                ["text"] = new JsonObject
                {
                    ["type"] = "plain_text",
                    ["text"] = "🔒 수락 요구"
                }
            },
            // Status text
            new JsonObject
            {
                ["type"] = "section",
                ["text"] = new JsonObject
                {
                    ["type"] = "mrkdwn",
                    ["text"] = statusText
                }
            },
            // Dynamic permission buttons
            new JsonObject
            {
                ["type"] = "actions",
                ["elements"] = buttonElements
            },
            // Context
            new JsonObject
            {
                ["type"] = "context",
                ["elements"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["type"] = "mrkdwn",
                        ["text"] = "버튼을 눌러 Claude에 응답하세요"
                    }
                }
            }
        };

        return blocks.ToJsonString();
    }

    /// <summary>Schedule a message via chat.scheduleMessage API.</summary>
    static async Task<(bool ok, string? scheduledId)> SlackScheduleMessageAsync(
        string botToken, string channel, string text, long postAtUnix)
    {
        using var http = new HttpClient();
        var payloadObj = new { channel, text, post_at = postAtUnix };
        var payload = JsonSerializer.Serialize(payloadObj);
        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.scheduleMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        req.Content = content;

        var resp = await http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = json?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] Schedule error: {json?["error"]}");

        var scheduledId = json?["scheduled_message_id"]?.GetValue<string>();
        return (ok, scheduledId);
    }

    /// <summary>
    /// wkappbot slack schedule "message" --in 20m | --at 14:30
    /// Schedule a message to be sent later via Slack chat.scheduleMessage API.
    /// </summary>
    static int SlackScheduleCommand(string[] args)
    {
        // Parse: slack schedule "message" --in 20m  OR  slack schedule "message" --at 14:30
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: wkappbot slack schedule \"message\" --in <duration>  (e.g. 5m, 1h, 30s)");
            Console.WriteLine("       wkappbot slack schedule \"message\" --at <HH:mm>     (e.g. 14:30)");
            return 1;
        }

        var message = args[1];
        var flag = args[2].ToLowerInvariant();
        var value = args[3];

        DateTimeOffset postAt;

        if (flag == "--in")
        {
            // Parse duration: 5m, 1h, 30s, 20m
            var durationStr = value.Trim().ToLowerInvariant();
            double amount;
            TimeSpan duration;
            if (durationStr.EndsWith("s") && double.TryParse(durationStr[..^1], out amount))
                duration = TimeSpan.FromSeconds(amount);
            else if (durationStr.EndsWith("m") && double.TryParse(durationStr[..^1], out amount))
                duration = TimeSpan.FromMinutes(amount);
            else if (durationStr.EndsWith("h") && double.TryParse(durationStr[..^1], out amount))
                duration = TimeSpan.FromHours(amount);
            else
            {
                Console.WriteLine($"[SLACK] Invalid duration: {value} (use 5m, 1h, 30s)");
                return 1;
            }
            postAt = DateTimeOffset.Now.Add(duration);
        }
        else if (flag == "--at")
        {
            // Parse time: HH:mm
            if (TimeOnly.TryParse(value, out var time))
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                postAt = new DateTimeOffset(today.ToDateTime(time), TimeZoneInfo.Local.GetUtcOffset(DateTime.Now));
                if (postAt <= DateTimeOffset.Now)
                    postAt = postAt.AddDays(1); // next day if time already passed
            }
            else
            {
                Console.WriteLine($"[SLACK] Invalid time: {value} (use HH:mm like 14:30)");
                return 1;
            }
        }
        else
        {
            Console.WriteLine($"[SLACK] Unknown flag: {flag} (use --in or --at)");
            return 1;
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = config["channel"]?.GetValue<string>();
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found in config");
            return 1;
        }

        var unixTs = postAt.ToUnixTimeSeconds();
        var (ok2, scheduledId) = SlackScheduleMessageAsync(botToken, channel, message, unixTs)
            .GetAwaiter().GetResult();

        if (ok2)
        {
            var localTime = postAt.ToLocalTime().ToString("HH:mm:ss");
            var delta = postAt - DateTimeOffset.Now;
            Console.WriteLine($"[SLACK] Scheduled: \"{message}\"");
            Console.WriteLine($"[SLACK] Delivery: {localTime} ({delta.TotalMinutes:F0}분 후)");
            Console.WriteLine($"[SLACK] ID: {scheduledId}");
        }
        else
            Console.WriteLine("[SLACK] Failed to schedule message");

        return ok2 ? 0 : 1;
    }

    static JsonNode? LoadSlackConfig()
    {
        if (!File.Exists(SlackConfigPath))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SLACK] Config not found: {SlackConfigPath}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Slack setup required — create the config file with these fields:");
            Console.WriteLine();
            Console.WriteLine("  IMPORTANT: The human must log into Slack admin first.");
            Console.WriteLine("  You (AI) cannot authenticate — ask the user, then continue your work.");
            Console.WriteLine("  Run this to notify the user via TTS while you proceed:");
            Console.WriteLine("    wkappbot speak \"Slack setup needs your login. Please open api.slack.com and log in.\" --bg");
            Console.WriteLine();
            Console.WriteLine($"  File: {SlackConfigPath}");
            Console.WriteLine("  {{");
            Console.WriteLine("    \"bot_token\": \"xoxb-...\",       // Bot User OAuth Token");
            Console.WriteLine("    \"app_token\": \"xapp-...\",       // App-Level Token (Socket Mode)");
            Console.WriteLine("    \"channel\":   \"C0...\",           // Channel ID to post to");
            Console.WriteLine("    \"socket_mode\": true");
            Console.WriteLine("  }}");
            Console.WriteLine();
            Console.WriteLine("How to get tokens (human must be logged into Slack admin):");
            Console.WriteLine("  1. Go to https://api.slack.com/apps → Create New App (From Scratch)");
            Console.WriteLine("  2. OAuth & Permissions → Bot Token Scopes: add these scopes:");
            Console.WriteLine("     chat:write, chat:write.customize, channels:read,");
            Console.WriteLine("     channels:history, app_mentions:read, files:write");
            Console.WriteLine("  3. Install to Workspace → copy Bot User OAuth Token (xoxb-...)");
            Console.WriteLine("  4. Basic Information → App-Level Tokens → Generate (connections:write)");
            Console.WriteLine("  5. Socket Mode → Enable Socket Mode");
            Console.WriteLine("  6. Event Subscriptions → Enable → Subscribe: app_mention, message.channels");
            Console.WriteLine("  7. Invite bot to your channel: /invite @YourBotName");
            Console.WriteLine("  8. Get channel ID: right-click channel name → View channel details → ID at bottom");
            return null;
        }

        var json = File.ReadAllText(SlackConfigPath);
        return JsonSerializer.Deserialize<JsonNode>(json);
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
            // 1. Find Claude Desktop window and bring to foreground
            var claudeHwnd = FindClaudeDesktopWindow();
            if (claudeHwnd == IntPtr.Zero)
            {
                Console.WriteLine("[SLACK] >> Claude Desktop window not found!");
                SlackSendViaApi(botToken, channel, "Claude Desktop 윈도우를 찾을 수 없습니다!", threadTs, username: BotUsername)
                    .GetAwaiter().GetResult();
                return;
            }

            // Bring Claude Desktop to foreground for SendInput
            // Use Smart method (AttachThreadInput trick) — background process can't SetForegroundWindow
            NativeMethods.SmartSetForegroundWindow(claudeHwnd);
            Thread.Sleep(300); // Wait for focus to settle

            // 2. Send Ctrl+Enter
            KeyboardInput.Hotkey(new[] { "ctrl", "enter" });
            Thread.Sleep(100);
            Console.WriteLine("[SLACK] >> Ctrl+Enter sent to Claude Desktop!");
            SlackSendViaApi(botToken, channel, "Ctrl+Enter 전송 완료! 수락 처리했습니다.", threadTs, username: BotUsername)
                .GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] >> Accept keystroke failed: {ex.Message}");
            SlackSendViaApi(botToken, channel, $"수락 전송 실패: {ex.Message}", threadTs, username: BotUsername)
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

    // ── Display name resolution ─────────────────────────────────────
    // Slack user IDs (e.g. U0A21P4RK29) are resolved to display names via users.info API.
    // Token is set once when a listener/eye session starts (single-process, static is safe).
    // Fallback: webhook.json["known_users"] = { "U0A21P4RK29": "Will" } static mapping.
    static string? _displayNameBotToken;
    static readonly Dictionary<string, string> _displayNameCache = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Load known_users mapping from webhook.json into the display name cache.
    /// Call once at session start to pre-populate cache before any API calls.
    /// </summary>
    static void LoadKnownUsersFromConfig()
    {
        try
        {
            var cfgPath = SlackConfigPath;
            if (!File.Exists(cfgPath)) return;
            var node = JsonNode.Parse(File.ReadAllText(cfgPath));
            var knownUsers = node?["known_users"];
            if (knownUsers == null) return;
            foreach (var kv in knownUsers.AsObject())
            {
                if (!string.IsNullOrWhiteSpace(kv.Key) && kv.Value != null)
                {
                    var displayName = kv.Value.GetValue<string>();
                    if (!string.IsNullOrWhiteSpace(displayName))
                        _displayNameCache[kv.Key] = displayName;
                }
            }
            Console.WriteLine($"[SLACK] Loaded {_displayNameCache.Count} known_users from config");
        }
        catch { }
    }

    /// <summary>
    /// Resolve a Slack user ID to a human-readable display name.
    /// Falls back to the raw user ID on API error or if no token is set.
    /// Results are cached per session to avoid repeated API calls.
    /// </summary>
    static string ResolveSlackDisplayName(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId)) return userId;
        if (_displayNameCache.TryGetValue(userId, out var cached)) return cached;
        if (string.IsNullOrWhiteSpace(_displayNameBotToken))
        {
            _displayNameCache[userId] = userId;
            return userId;
        }

        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
            using var req = new HttpRequestMessage(HttpMethod.Get,
                $"https://slack.com/api/users.info?user={Uri.EscapeDataString(userId)}");
            req.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _displayNameBotToken);
            var resp = http.Send(req);
            using var reader = new StreamReader(resp.Content.ReadAsStream());
            var rawJson = reader.ReadToEnd();
            var json = JsonSerializer.Deserialize<JsonNode>(rawJson);

            if (json?["ok"]?.GetValue<bool>() == true)
            {
                var profile = json["user"]?["profile"];
                var name = profile?["display_name"]?.GetValue<string>();
                if (string.IsNullOrWhiteSpace(name))
                    name = profile?["real_name"]?.GetValue<string>();
                if (string.IsNullOrWhiteSpace(name))
                    name = json["user"]?["name"]?.GetValue<string>();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _displayNameCache[userId] = name!;
                    return name!;
                }
                Console.Error.WriteLine($"[SLACK] users.info ok=true but no name found for {userId}");
            }
            else
            {
                var err = json?["error"]?.GetValue<string>() ?? "unknown";
                Console.Error.WriteLine($"[SLACK] users.info failed for {userId}: error={err}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLACK] users.info exception for {userId}: {ex.Message}");
        }

        _displayNameCache[userId] = userId; // cache failure to prevent hammering API
        return userId;
    }

    /// <summary>Get bot user ID via auth.test (sync helper).</summary>
    static string? SlackGetBotUserId(string botToken)
    {
        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };
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
        Console.WriteLine("  listen [--bg] [--ai] [--claude|--webbot] [--name N]");
        Console.WriteLine("                        Socket Mode: listen for @mentions and reply");
        Console.WriteLine("                        --bg: run as background daemon process");
        Console.WriteLine("                        --ai: use Claude API for intelligent responses");
        Console.WriteLine("                        Prompt + Keywords: ALWAYS ON (no flags needed)");
        Console.WriteLine("                        --claude: stream Claude Desktop status to Slack");
        Console.WriteLine("                        --webbot: alias for --claude (legacy name)");
        Console.WriteLine("                        --name N: instance name for multi-bot ID (default: cwd folder)");
        Console.WriteLine("  send \"message\"      Send a message to the configured channel");
        Console.WriteLine("  test                Test Slack connection (auth + send + socket)");
        Console.WriteLine("  status              Check if background listener is running");
        Console.WriteLine("  stop                Stop background listener");
        Console.WriteLine("  inbox               Show unread @mention messages");
        Console.WriteLine("  reply \"text\" [--channel ID] [--msg TS]");
        Console.WriteLine("                        Reply to Slack and clear inbox");
        Console.WriteLine("                        --msg: reply in-thread (request message timestamp)");
        Console.WriteLine("  upload <file> [--channel ID] [--msg TS] [--title \"name\"]");
        Console.WriteLine("                        Upload a file to Slack channel");
        Console.WriteLine("  screenshot [window-title] [--channel ID] [--msg TS]");
        Console.WriteLine("                        Capture screenshot and upload to Slack");
        Console.WriteLine("                        (no title = full screen capture)");
        Console.WriteLine("  catch-up [--channel ID] [--limit N]");
        Console.WriteLine("                        Fetch missed messages since last bookmark");
        Console.WriteLine("                        (always forwards @mentions to Claude prompt)");
        Console.WriteLine("  prompt [--watch]    Type inbox messages into Claude Code prompt");
        Console.WriteLine("                      --watch: continuously poll for new messages");
        Console.WriteLine("                      --interval N: poll interval seconds (default: 3)");
        Console.WriteLine("  list [channel|msg_ts] [--limit N] [--delete-pattern \"pat\"]");
        Console.WriteLine("                        List channel messages or thread replies");
        Console.WriteLine("                        C0... = channel, 1234.5678 = message → auto thread");
        Console.WriteLine("                        reply ts → parent thread auto-detected");
        Console.WriteLine("                        --delete-pattern: delete matching r=0 messages");
        Console.WriteLine();
        Console.WriteLine($"Config: {SlackConfigPath}");
        return 1;
    }
}
