using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

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
            _ => SlackUsage()
        };
    }

    static string SlackConfigPath => Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
    static string SlackPidFile => Path.Combine(DataDir, "slack.pid");

    /// <summary>Socket Mode: listen for events and respond to @mentions.</summary>
    static int SlackListenCommand(string[] args)
    {
        bool background = args.Contains("--bg") || args.Contains("--background") || args.Contains("-d");

        // --bg: launch self as background process
        if (background)
            return SlackLaunchBackground();

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var appToken = config["app_token"]?.GetValue<string>();
        var botToken = config["bot_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(appToken) || string.IsNullOrEmpty(botToken))
        {
            Console.WriteLine("[SLACK] ERROR: app_token or bot_token not found in config");
            return 1;
        }

        // Write PID file (for status/stop)
        WritePidFile();

        Console.WriteLine("[SLACK] Starting Socket Mode listener...");
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

        // Handle @mentions — echo back with prefix
        slack.OnMention += (msg) =>
        {
            Console.WriteLine($"[SLACK] << @mention from {msg.User}: {msg.Text}");

            // Strip the bot mention from text: "<@U12345> hello" → "hello"
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

            if (string.IsNullOrEmpty(cleanText))
                cleanText = "WKAppBot online!";

            var reply = $"{cleanText}";
            Console.WriteLine($"[SLACK] >> {reply}");
            slack.SendAsync(msg.Channel, reply).GetAwaiter().GetResult();
        };

        // Handle channel messages (log only, don't reply to every message)
        slack.OnMessage += (msg) =>
        {
            Console.WriteLine($"[SLACK] msg [{msg.Channel}] {msg.User}: {msg.Text}");
        };

        // Wait until cancelled
        try { Task.Delay(-1, cts.Token).GetAwaiter().GetResult(); }
        catch (OperationCanceledException) { }

        slack.DisconnectAsync().GetAwaiter().GetResult();
        DeletePidFile();
        return 0;
    }

    /// <summary>Launch wkappbot slack listen as a background process.</summary>
    static int SlackLaunchBackground()
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

        var psi = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = "slack listen",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        // Pass DOTNET_ROOT if set
        var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
        if (!string.IsNullOrEmpty(dotnetRoot))
            psi.EnvironmentVariables["DOTNET_ROOT"] = dotnetRoot;

        var process = Process.Start(psi);
        if (process == null)
        {
            Console.WriteLine("[SLACK] Failed to start background listener");
            return 1;
        }

        // Redirect output to log file in background
        _ = Task.Run(async () =>
        {
            using var writer = new StreamWriter(logFile, append: true);
            while (!process.HasExited)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                if (line != null) await writer.WriteLineAsync(line);
            }
        });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SLACK] Listener started in background (PID={process.Id})");
        Console.ResetColor();
        Console.WriteLine($"[SLACK] Log: {logFile}");
        Console.WriteLine($"[SLACK] Stop: wkappbot slack stop");

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

    /// <summary>Send message via chat.postMessage API.</summary>
    static async Task<bool> SlackSendViaApi(string botToken, string channel, string text)
    {
        using var http = new HttpClient();
        var payload = JsonSerializer.Serialize(new { channel, text });
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

    static int SlackUsage()
    {
        Console.WriteLine("Usage: wkappbot slack <command>");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  listen [--bg]       Socket Mode: listen for @mentions and reply");
        Console.WriteLine("                      --bg: run as background daemon process");
        Console.WriteLine("  send \"message\"      Send a message to the configured channel");
        Console.WriteLine("  test                Test Slack connection (auth + send + socket)");
        Console.WriteLine("  status              Check if background listener is running");
        Console.WriteLine("  stop                Stop background listener");
        Console.WriteLine();
        Console.WriteLine($"Config: {SlackConfigPath}");
        return 1;
    }
}
