using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

// partial class: schedule command — manage scheduled prompt injections
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
            _ => ScheduleUsage()
        };
    }

    static int ScheduleAddCommand(string[] args)
    {
        string? atTime = null, prompt = null, promptFile = null, command = null;
        string? every = null, maxRunsStr = null, expiresAt = null;
        bool onLimitReset = false;

        for (int i = 1; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--at" when i + 1 < args.Length: atTime = args[++i]; break;
                case "--prompt" when i + 1 < args.Length: prompt = args[++i]; break;
                case "--prompt-file" when i + 1 < args.Length: promptFile = args[++i]; break;
                case "--cmd" when i + 1 < args.Length: command = args[++i]; break;
                case "--every" when i + 1 < args.Length: every = args[++i]; break;
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
                    item.ExpiresAt = expiresAt; // assume ISO 8601
            }
        }
        else if (!string.IsNullOrEmpty(atTime))
        {
            item.Type = "once";
            if (TimeOnly.TryParse(atTime, out var time))
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
                return Error($"Invalid time: {atTime} (use HH:mm like 16:00 or ISO datetime)");
            }
        }
        else
        {
            return Error("schedule add: --at, --every, or --on-limit-reset required");
        }

        var id = ScheduleManager.Add(item);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SCHEDULE] Added: {id} ({item.Type})");
        Console.ResetColor();

        if (!string.IsNullOrEmpty(item.ExecuteAt) && DateTime.TryParse(item.ExecuteAt, out var dt))
            Console.WriteLine($"[SCHEDULE] Execute at: {dt:yyyy-MM-dd HH:mm}");
        if (item.Type == "on_limit_reset")
            Console.WriteLine("[SCHEDULE] Trigger: rate limit reset detected by AppBotEye");
        if (!string.IsNullOrEmpty(item.Interval))
            Console.WriteLine($"[SCHEDULE] Interval: {item.Interval}");

        var promptPreview = item.Prompt ?? $"(file: {item.PromptFile})";
        if (promptPreview.Length > 60) promptPreview = promptPreview[..57] + "...";
        Console.WriteLine($"[SCHEDULE] Prompt: {promptPreview}");
        Console.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");
        return 0;
    }

    static int ScheduleListCommand()
    {
        var file = ScheduleManager.Load();
        if (file.Schedules.Count == 0)
        {
            Console.WriteLine("[SCHEDULE] No schedules");
            Console.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");
            return 0;
        }

        Console.WriteLine($"[SCHEDULE] {file.Schedules.Count} schedule(s):");
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
        Console.WriteLine($"[SCHEDULE] File: {ScheduleManager.FilePath}");
        return 0;
    }

    static int ScheduleRemoveCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: wkappbot schedule remove <id>");

        var id = args[1];
        if (ScheduleManager.Remove(id))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SCHEDULE] Removed: {id}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[SCHEDULE] Not found: {id}");
            Console.ResetColor();
        }
        return 0;
    }

    static int ScheduleClearCommand(string[] args)
    {
        bool onlyPending = args.Contains("--pending");
        var count = ScheduleManager.Clear(onlyPending);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SCHEDULE] Cleared {count} schedule(s){(onlyPending ? " (pending only)" : "")}");
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
        return 1;
    }
}
