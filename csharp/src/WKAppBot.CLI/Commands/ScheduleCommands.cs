using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

// partial class: schedule command -- manage scheduled prompt injections
internal partial class Program
{
    /// <summary>
    /// wkappbot schedule <subcommand>
    /// Manage scheduled prompt injections for Claude Desktop auto-recovery.
    /// </summary>
    static int ScheduleCommand(string[] args)
    {
        if (args.Length == 0) return ScheduleUsage();
        var sub = args[0].ToLowerInvariant();
        return sub switch
        {
            "add" => ScheduleAddCommand(args),
            "list" or "ls" => ScheduleListCommand(),
            "remove" or "rm" => ScheduleRemoveCommand(args),
            "clear" => ScheduleClearCommand(args),
            "exec" => ScheduleExecCommand(args),
            _ => ScheduleUsage()
        };
    }

    static int ScheduleAddCommand(string[] args)
    {
        string? atTime = null, prompt = null, promptFile = null, command = null;
        string? every = null, everyDay = null, maxRunsStr = null, expiresAt = null;
        bool onLimitReset = false;

        for (int i = 1; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--at" or "--after" when i + 1 < args.Length: atTime = args[++i]; break;
                case "--prompt" when i + 1 < args.Length: prompt = args[++i]; break;
                case "--prompt-file" when i + 1 < args.Length: promptFile = args[++i]; break;
                case "--cmd" when i + 1 < args.Length: command = args[++i]; break;
                case "--every" when i + 1 < args.Length: every = args[++i]; break;
                case "--every-day" when i + 1 < args.Length: everyDay = args[++i]; break;
                case "--on-limit-reset": onLimitReset = true; break;
                case "--max-runs" when i + 1 < args.Length: maxRunsStr = args[++i]; break;
                case "--expires" when i + 1 < args.Length: expiresAt = args[++i]; break;
            }
        }

        if (string.IsNullOrEmpty(prompt) && string.IsNullOrEmpty(promptFile) && string.IsNullOrEmpty(command))
            return Error("schedule add: --prompt, --prompt-file, or --cmd required");

        // Validate prompt file exists
        if (!string.IsNullOrEmpty(promptFile) && !File.Exists(promptFile))
        {
            // Try relative to CWD
            var absPath = Path.GetFullPath(promptFile);
            if (!File.Exists(absPath))
                return Error($"Prompt file not found: {promptFile}");
            promptFile = absPath;
        }

        var item = new ScheduleItem
        {
            Prompt = prompt,
            PromptFile = promptFile,
            Command = command,
            CreatedBy = "cli",
        };

        if (onLimitReset)
        {
            item.Type = "on_limit_reset";
        }
        else if (!string.IsNullOrEmpty(everyDay))
        {
            // --every-day 13:00 -> daily recurring at fixed time
            if (!TimeOnly.TryParse(everyDay, out var dailyTime))
                return Error($"Invalid time for --every-day: {everyDay} (use HH:mm like 13:00)");
            item.Type = "recurring";
            item.Interval = "24h";
            var execDt = DateTime.Today.Add(dailyTime.ToTimeSpan());
            if (execDt <= DateTime.Now) execDt = execDt.AddDays(1);
            item.ExecuteAt = execDt.ToString("O");
            if (maxRunsStr != null && int.TryParse(maxRunsStr, out var mr)) item.MaxRuns = mr;
        }
        else if (!string.IsNullOrEmpty(every))
        {
            item.Type = "recurring";
            item.Interval = every;
            var interval = ScheduleManager.ParseInterval(every);
            if (interval == null)
                return Error($"Invalid interval: {every} (use 30m, 1h, 2h, etc.)");
            item.ExecuteAt = DateTime.Now.Add(interval.Value).ToString("O");
            if (maxRunsStr != null && int.TryParse(maxRunsStr, out var mr)) item.MaxRuns = mr;
            if (expiresAt != null)
            {
                if (TimeOnly.TryParse(expiresAt, out var expTime))
                {
                    var expDt = DateTime.Today.Add(expTime.ToTimeSpan());
                    if (expDt <= DateTime.Now) expDt = expDt.AddDays(1);
                    item.ExpiresAt = expDt.ToString("O");
                }
                else
                    item.ExpiresAt = expiresAt;
            }
        }
        else if (!string.IsNullOrEmpty(atTime))
        {
            item.Type = "once";
            // Relative: "1m", "30m", "2h" etc.
            var rel = ScheduleManager.ParseInterval(atTime.TrimStart('+'));
            if (rel != null)
            {
                item.ExecuteAt = DateTime.Now.Add(rel.Value).ToString("O");
            }
            else if (TimeOnly.TryParse(atTime, out var time))
            {
                var execDt = DateTime.Today.Add(time.ToTimeSpan());
                if (execDt <= DateTime.Now) execDt = execDt.AddDays(1);
                item.ExecuteAt = execDt.ToString("O");
            }
            else if (DateTime.TryParse(atTime, out var fullDt))
            {
                item.ExecuteAt = fullDt.ToString("O");
            }
            else
            {
                return Error($"Invalid time: {atTime} (use 1m/30m/2h, HH:mm, or ISO datetime)");
            }
        }
        else
        {
            return Error("schedule add: --at/--after, --every, --every-day, or --on-limit-reset required");
        }

        // Warn if --cmd starts with a shell interpreter that may not be in cmd.exe PATH.
        if (!string.IsNullOrEmpty(command))
        {
            var resolved = ResolveShellCommand(ref command);
            item.Command = command; // update with resolved path if changed
            if (resolved != null)
                Console.Error.WriteLine($"[SCHEDULE] NOTE: {resolved}");
        }

        var id = ScheduleManager.Add(item);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[SCHEDULE] Added: {id} ({item.Type})");
        Console.ResetColor();

        if (!string.IsNullOrEmpty(item.ExecuteAt) && DateTime.TryParse(item.ExecuteAt, out var dt))
            Console.Error.WriteLine($"[SCHEDULE] Execute at: {dt:yyyy-MM-dd HH:mm}");
        if (item.Type == "on_limit_reset")
            Console.WriteLine("[SCHEDULE] Trigger: rate limit reset detected by AppBotEye");
        if (!string.IsNullOrEmpty(item.Interval))
            Console.Error.WriteLine($"[SCHEDULE] Interval: {item.Interval}");

        var promptPreview = item.Command ?? item.Prompt ?? $"(file: {item.PromptFile})";
        if (promptPreview.Length > 60) promptPreview = promptPreview[..57] + "...";
        Console.Error.WriteLine($"[SCHEDULE] Prompt: {promptPreview}");
        Console.Error.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");

        // Register with Windows Task Scheduler for once/recurring -- survives Eye kill
        if (item.Type is "once" or "recurring" && !string.IsNullOrEmpty(item.ExecuteAt)
            && DateTime.TryParse(item.ExecuteAt, out var schtaskDt))
        {
            RegisterSchtask(id, schtaskDt, item.Type == "recurring" ? item.Interval : null);
        }
        return 0;
    }

    /// <summary>Register (or re-register) a Windows Task Scheduler entry for this schedule item.</summary>
    static void RegisterSchtask(string id, DateTime executeAt, string? interval = null)
    {
        var exePath = (Environment.ProcessPath ?? "wkappbot.exe").Replace('/', '\\');
        var taskName = $"WKAppBot_{id}";
        var tr = $"\"{exePath}\" schedule exec {id}";
        var date = executeAt.ToString("MM/dd/yyyy");
        var time = executeAt.ToString("HH:mm");
        // For recurring, use /sc daily as a baseline -- Eye will re-register on next run
        var sc = interval != null ? "daily" : "once";
        var schtasksArgs = $"/create /tn \"{taskName}\" /tr \"{tr}\" /sc {sc} /sd {date} /st {time} /f";
        try
        {
            using var p = AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo(
                "schtasks.exe", schtasksArgs) { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true }, Environment.CurrentDirectory, "SCHEDULE")!;
            p.WaitForExit(5000);
            Console.Error.WriteLine($"[SCHEDULE] schtasks registered: {taskName} at {executeAt:HH:mm}");
        }
        catch (Exception ex) { Console.Error.WriteLine($"[SCHEDULE] schtasks register failed: {ex.Message}"); }
    }

    /// <summary>Delete Windows Task Scheduler entry for this schedule item.</summary>
    static void DeleteSchtask(string id)
    {
        try
        {
            using var p = AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo(
                "schtasks.exe", $"/delete /tn \"WKAppBot_{id}\" /f") { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true }, Environment.CurrentDirectory, "SCHEDULE")!;
            p.WaitForExit(3000);
        }
        catch { /* schtask may not exist -- ignore */ }
    }

    /// <summary>wkappbot schedule exec &lt;id&gt; -- directly execute a scheduled item (called by schtasks, no Eye needed).</summary>
    static int ScheduleExecCommand(string[] args)
    {
        if (args.Length < 2) return Error("Usage: wkappbot schedule exec <id>");
        var id = args[1];
        var file = ScheduleManager.Load();
        var item = file.Schedules.FirstOrDefault(s => s.Id == id);
        if (item == null) return Error($"[SCHEDULE] Not found: {id}");

        // Load Slack config (same as Eye)
        string? slackBotToken = null, slackChannel = null;
        try
        {
            var configPath = System.IO.Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (System.IO.File.Exists(configPath))
            {
                var json = System.Text.Json.Nodes.JsonNode.Parse(System.IO.File.ReadAllText(configPath));
                slackBotToken = json?["bot_token"]?.GetValue<string>();
                slackChannel = json?["channel"]?.GetValue<string>();
            }
        }
        catch { }

        ExecuteScheduleItem(item, slackBotToken, slackChannel);

        // Re-register schtask for next run if recurring
        if (item.Type == "recurring" && !string.IsNullOrEmpty(item.ExecuteAt)
            && DateTime.TryParse(item.ExecuteAt, out var nextDt))
            RegisterSchtask(id, nextDt, item.Interval);
        else
            DeleteSchtask(id); // one-shot -- remove from schtasks

        return 0;
    }

    static int ScheduleListCommand()
    {
        var file = ScheduleManager.Load();
        if (file.Schedules.Count == 0)
        {
            Console.WriteLine("[SCHEDULE] No schedules");
            Console.Error.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");
            return 0;
        }

        Console.Error.WriteLine($"[SCHEDULE] {file.Schedules.Count} schedule(s):");
        Console.WriteLine();

        foreach (var s in file.Schedules)
        {
            var statusColor = s.Status switch
            {
                "pending" => ConsoleColor.Yellow,
                "done" => ConsoleColor.Green,
                "failed" => ConsoleColor.Red,
                "expired" => ConsoleColor.DarkGray,
                _ => ConsoleColor.Gray
            };

            Console.ForegroundColor = statusColor;
            Console.Write($"  [{s.Status,-7}]");
            Console.ResetColor();
            Console.Write($" {s.Id} ({s.Type})");

            if (!string.IsNullOrEmpty(s.ExecuteAt) && DateTime.TryParse(s.ExecuteAt, out var dt))
                Console.Write($" at {dt:HH:mm}");
            if (s.Type == "on_limit_reset")
                Console.Write(" (trigger: limit reset)");
            if (s.Type == "recurring" && !string.IsNullOrEmpty(s.Interval))
                Console.Write($" every {s.Interval}");
            if (s.RunCount > 0)
                Console.Write($" (ran {s.RunCount}x)");

            var promptPreview = s.Prompt ?? $"(file: {s.PromptFile})";
            if (promptPreview.Length > 50) promptPreview = promptPreview[..47] + "...";
            Console.WriteLine($" -- {promptPreview}");

            if (!string.IsNullOrEmpty(s.ErrorMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"           Error: {s.ErrorMessage}");
                Console.ResetColor();
            }
        }

        Console.WriteLine();
        Console.Error.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");
        return 0;
    }

    static int ScheduleRemoveCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot schedule remove <id>");

        var id = args[1];
        if (ScheduleManager.Remove(id))
        {
            DeleteSchtask(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine($"[SCHEDULE] Removed: {id}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[SCHEDULE] Not found: {id}");
            Console.ResetColor();
        }
        return 0;
    }

    static int ScheduleClearCommand(string[] args)
    {
        bool onlyPending = args.Contains("--pending");
        var count = ScheduleManager.Clear(onlyPending);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[SCHEDULE] Cleared {count} schedule(s){(onlyPending ? " (pending only)" : "")}");
        Console.ResetColor();
        return 0;
    }

    static int ScheduleUsage()
    {
        Console.WriteLine("Usage: wkappbot schedule <command>");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  add --at HH:mm --prompt \"text\"           Schedule prompt at specific time");
        Console.WriteLine("  add --on-limit-reset --prompt \"text\"     Execute when rate limit resets");
        Console.WriteLine("  add --every 30m --prompt \"text\"          Recurring schedule");
        Console.WriteLine("  add ... --prompt-file ./path.md          Read prompt from file");
        Console.WriteLine("  add ... --max-runs N --expires HH:mm     Recurring limits");
        Console.WriteLine("  list                                     Show all schedules");
        Console.WriteLine("  remove <id>                              Remove a schedule");
        Console.WriteLine("  clear [--pending]                        Remove all (or pending only)");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot schedule add --at 16:00 --prompt \"리밋 해제. 작업 재개해주세요\"");
        Console.WriteLine("  wkappbot schedule add --on-limit-reset --prompt \"CLAUDE.md 읽고 작업 재개\"");
        Console.WriteLine("  wkappbot schedule add --every 30m --prompt \"slack catch-up 확인\" --max-runs 5");
        Console.WriteLine();
        Console.WriteLine($"File: {ScheduleManager.FilePath}");
        Console.WriteLine("AppBotEye polls this file and executes pending schedules automatically.");
        PrintRelatedSkills("schedule");
        return 1;
    }

    /// <summary>
    /// Resolve bash/sh/python prefix in --cmd on Windows.
    /// Mutates cmd to use full interpreter path if found.
    /// Returns a warning string if interpreter needs PATH resolution, null if already fine.
    /// </summary>
    internal static string? ResolveShellCommand(ref string cmd)
    {
        if (string.IsNullOrEmpty(cmd)) return null;
        var first = cmd.Split(' ', 2)[0].ToLowerInvariant().TrimEnd('"');
        if (first is not ("bash" or "sh" or "python" or "python3" or "node")) return null;

        // Already a full path
        if (first.Contains('/') || first.Contains('\\')) return null;

        // Try to find in PATH or common locations
        var resolved = FindInterpreterPath(first);
        if (resolved != null)
        {
            cmd = $"\"{resolved}\" {cmd.Split(' ', 2).ElementAtOrDefault(1) ?? ""}".Trim();
            return $"'{first}' resolved to {resolved} (auto-fixed in command)";
        }

        return $"WARNING: '{first}' not found in PATH. Command may fail at execution time. " +
               $"Use full path: e.g. \"C:\\Program Files\\Git\\bin\\bash.exe ...\"";
    }

    static string? FindInterpreterPath(string name)
    {
        // Common Windows locations
        var candidates = name switch
        {
            "bash" or "sh" => new[]
            {
                @"C:\Program Files\Git\bin\bash.exe",
                @"C:\Program Files\Git\usr\bin\bash.exe",
                @"C:\Windows\System32\bash.exe",  // WSL
            },
            "python" or "python3" => new[]
            {
                @"C:\Python312\python.exe", @"C:\Python311\python.exe", @"C:\Python310\python.exe",
            },
            "node" => new[] { @"C:\Program Files\nodejs\node.exe" },
            _ => Array.Empty<string>()
        };

        foreach (var p in candidates)
            if (File.Exists(p)) return p;

        // Check PATH
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in pathEnv.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var full = Path.Combine(dir.Trim(), name + ".exe");
            if (File.Exists(full)) return full;
            full = Path.Combine(dir.Trim(), name);
            if (File.Exists(full)) return full;
        }
        return null;
    }
}
