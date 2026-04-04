// CommandHelp.cs ??Per-command --help descriptions for future Claude instances.
// Usage: wkappbot <command> --help  |  wkappbot <command> <subcommand> --help
//        wkappbot <command> [<subcommand>] --regression  ??help + run stored test scripts
//
// STRUCTURE NOTE: Currently a manual dictionary.
// FUTURE: Replace with [Command] / [SubCommand] / [Description] attributes on
//   handler methods + Reflection scan ??code changes auto-expose in --help.
//   See: suggestions.jsonl "Reflection-based --help auto-generation

namespace WKAppBot.CLI;

internal partial class Program
{
    static bool IsEvidenceScript(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext is ".sh" or ".ps1" or ".cmd" or ".bat";
    }

    /// <summary>
    /// Check if args contain --help/-h and print focused help.
    /// Returns true if help was printed (caller should exit 0).
    /// </summary>
    internal static bool TryPrintCommandHelp(string command, string[] restArgs)
    {
        if (!restArgs.Any(a => a is "--help" or "-h")) return false;

        // subcommand: first non-flag arg before --help
        var sub = restArgs.FirstOrDefault(a => !a.StartsWith('-'));
        var key = sub != null ? $"{command} {sub}" : command;

        if (!CommandHelpMap.TryGetValue(key, out var text)
            && !CommandHelpMap.TryGetValue(command, out text))
        {
            Console.WriteLine($"No --help entry for: {command}{(sub != null ? " " + sub : "")}");
            Console.WriteLine("Run 'wkappbot --help' for full command list.");
            return true;
        }

        Console.WriteLine(text.TrimStart('\n'));
        return true;
    }

    /// <summary>
    /// --regression: print help + run all stored test scripts for this command/subcommand.
    /// Returns true if handled (caller should exit 0).
    /// Script location: {DataDir}/experience/tests/{cmd}/{subcmd}/*.(sh|ps1|cmd)
    /// </summary>
    internal static bool TryRunRegression(string command, string[] restArgs)
    {
        if (!restArgs.Any(a => a is "--regression")) return false;

        // subcommand: first non-flag arg
        var sub = restArgs.FirstOrDefault(a => !a.StartsWith('-'));

        // ── Step 1: print --help ──────────────────────────────────────────────
        var key = sub != null ? $"{command} {sub}" : command;
        if (!CommandHelpMap.TryGetValue(key, out var helpText)
            && !CommandHelpMap.TryGetValue(command, out helpText))
        {
            Console.WriteLine($"No --help entry for: {command}{(sub != null ? " " + sub : "")}");
        }
        else
        {
            Console.WriteLine(helpText.TrimStart('\n'));
        }

        // ── Step 2: find test scripts ─────────────────────────────────────────
        var testsRoot = Path.Combine(DataDir, "experience", "tests");
        string[] scriptFiles;

        if (sub != null)
        {
            // exact subcmd dir
            var subDir = Path.Combine(testsRoot, command, sub);
            scriptFiles = Directory.Exists(subDir)
                ? Directory.GetFiles(subDir)
                    .Where(f => IsEvidenceScript(f))
                    .OrderBy(f => f)
                    .ToArray()
                : [];
        }
        else
        {
            // all scripts under command dir (recursive)
            var cmdDir = Path.Combine(testsRoot, command);
            scriptFiles = Directory.Exists(cmdDir)
                ? Directory.GetFiles(cmdDir, "*", SearchOption.AllDirectories)
                    .Where(f => IsEvidenceScript(f))
                    .OrderBy(f => f)
                    .ToArray()
                : [];
        }

        if (scriptFiles.Length == 0)
        {
            Console.WriteLine();
            Console.WriteLine($"[REGRESSION] No test scripts found for: {command}{(sub != null ? " " + sub : "")}");
            Console.WriteLine($"  Expected: {Path.Combine(testsRoot, command, sub ?? "*")}/*.(sh|ps1|cmd)");
            return true;
        }

        // ── Step 3: run each script ───────────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine($"[REGRESSION] {scriptFiles.Length} script(s) for {command}{(sub != null ? " " + sub : "")}");
        Console.WriteLine(new string('-', 60));

        int passed = 0, failed = 0;
        var gitBash = @"C:\Program Files\Git\usr\bin\bash.exe";
        var bashExe = File.Exists(gitBash) ? gitBash : "bash";

        // Convert Windows path to POSIX for bash (e.g. W:\path ??/w/path)
        static string ToPosix(string winPath) =>
            System.Text.RegularExpressions.Regex.Replace(
                winPath.Replace('\\', '/'),
                @"^([A-Za-z]):/", m => $"/{m.Groups[1].Value.ToLower()}/");

        foreach (var script in scriptFiles)
        {
            var name = Path.GetFileName(script);
            try
            {
                var ext = Path.GetExtension(script).ToLowerInvariant();
                var runner = ext switch
                {
                    ".sh" => bashExe,
                    ".ps1" => "powershell.exe",
                    ".cmd" or ".bat" => "cmd.exe",
                    _ => throw new InvalidOperationException($"Unsupported regression script: {script}")
                };
                var args = ext switch
                {
                    ".sh" => $"\"{ToPosix(script)}\"",
                    ".ps1" => $"-NoProfile -ExecutionPolicy Bypass -File \"{script}\"",
                    ".cmd" or ".bat" => $"/c \"{script}\"",
                    _ => throw new InvalidOperationException()
                };
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = runner,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                };
                using var proc = AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "HELP-REGRESSION")!;
                var stdout = proc.StandardOutput.ReadToEnd();
                var stderr = proc.StandardError.ReadToEnd();
                proc.WaitForExit(60_000);
                var exit = proc.ExitCode;

                if (exit == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[PASS] ");
                    Console.ResetColor();
                    Console.WriteLine(name);
                    passed++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[FAIL] ");
                    Console.ResetColor();
                    Console.WriteLine($"{name} (exit={exit})");
                    // show last few lines of output on failure
                    var combined = (stdout + stderr).Trim();
                    if (!string.IsNullOrEmpty(combined))
                    {
                        var lines = combined.Split('\n');
                        foreach (var line in lines.TakeLast(5))
                            Console.WriteLine($"       {line.TrimEnd()}");
                    }
                    failed++;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[FAIL] ");
                Console.ResetColor();
                Console.WriteLine($"{name} ({ex.Message})");
                failed++;
            }
        }

        Console.WriteLine(new string('-', 60));
        if (failed == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[REGRESSION] ALL PASS ({passed}/{passed})");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[REGRESSION] {failed} FAILED, {passed} passed ({passed + failed} total)");
        }
        Console.ResetColor();
        return true;
    }

    // ── Help text per command (and command+subcommand) ──────────────────────────
    // Keep entries short (~10 lines). Focus on: what it does, key options, gotchas,
    // examples. Avoid restating obvious things.
    //
    // Key format: "command" or "command subcommand"
    // FUTURE: auto-generated from [Description] attributes via reflection.

    static readonly Dictionary<string, string> CommandHelpMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["a11y"] = """
            a11y <action> <grap>[#uia-scope] [options]
            Universal accessibility control ??UIA ??Win32 ??SendInput 3-tier fallback.

            Actions: close  minimize  maximize  restore  focus  move  resize
                     read  find  highlight  invoke  click  toggle  expand  collapse
                     select  scroll  type  set-value  set-range
                     clipboard  clipboard-read  clipboard-write  wait  eval

            Grap patterns:
              AI target language for windows and accessibility nodes.
              Humans browse windows; AIs point with grap.
              Interactive actions stop at the first exact match.
              Full lookup stays array-shaped for ranking and review.
              "*App*"              glob wildcard
              "regex:btn_\\d+"     regex
              "*App*;*Calc*"       OR (semicolon)
              "App#MenuBar"        # = UIA scope drill-down
              "Chrome#ChatGPT#btn" tab portal (auto tab switch)
              "adb://device#elem"  Android ADB

            Key options:
              --nth N / N~ / ~N / 2~4 / 1,3~   which match (default: first)
              --all                      apply to all matches
              --depth N                  UIA tree depth
              --force                    skip safety guards
              --hotkey                   type: label/menu dispatch (not raw keys)
              --eval-js "js"             pre-hook or primary output (web)

            Examples:
              a11y invoke "*MenuBar*#*File*"
              a11y type "*edit*" "hello world"
              a11y type "*app*#*MenuBar*" "File/Save" --hotkey
              a11y read "*Chrome*#chatgpt.com" --eval-js "document.title"
              a11y close "*Chrome*" --nth 2~
            """,

        ["slack"] = """
            slack <subcommand> [options]
            Slack messaging via Bot API (Socket Mode).

            Subcommands:
              send "<msg>" [file.png]             send to main channel
              reply "<msg>" --msg <ts> [file.png] thread reply
              upload <file> [--msg <ts>]          upload file (optionally to thread)
              route --queue                       drain slack_queue/ (internal, Eye only)
              route --file <path>                 single message route (test use)
              route '<json>'                      inline JSON route (test use)

            ??RULE: Always reply to Slack messages IN SLACK via `slack reply --msg <ts>`.
              Answering only in the Claude Code prompt is NOT acceptable.
            ??`slack route` is an internal Eye command ??do NOT call manually unless testing.
            """,

        ["suggest"] = "suggest [subcommand]\n"
            + "AI suggestion management ??propose ??review ??resolve workflow.\n\n"
            + "Subcommands:\n"
            + "  (no args)         submit a suggestion (opens editor or reads stdin)\n"
            + "  list              show active (unresolved) suggestions\n"
            + "  resolve <ts>      mark resolved (REQUIRES evidence script ??see below!)\n"
            + "  repost            re-send suggestions to Slack if ts missing\n\n"
            + "??RESOLVE GUARD ??mandatory test evidence required:\n"
            + "  wkappbot suggest resolve <ts> \"note\"\n"
            + "    --i-completed-<cmd>-<subcmd>-willkim-allowed-this-script <test.sh|test.ps1|test.cmd>\n\n"
            + "  Evidence filename must match: test-{cmd}-{subcmd}-*.<sh|ps1|cmd>\n"
            + "  Script must: reference the command, exit 0 (all tests pass)\n"
            + "  Failure ??resolve BLOCKED (fix the test or the code first)\n\n"
            + "  Regression: ALL previously stored scripts in experience/tests/{cmd}/{subcmd}/\n"
            + "  are re-run on each resolve. If any fail ??blocked.\n\n"
            + "evidence_file is saved to history so you can trace what was tested.\n\n"
            + "Examples:\n"
            + "  wkappbot suggest resolve 2026-03-17T05 \"fixed logcat --dbg race\"\n"
            + "    --i-completed-logcat-dbg-willkim-allowed-this-script test/test-logcat-dbg-listener.sh",

        ["eye"] = """
            eye [options]
            WK AppBot Eye ??persistent Slack daemon + status overlay.
            Runs as background process, auto-spawned by most commands.

            Key behaviors:
              ??Receives Slack messages ??queues to slack_queue/ ??routes to Claude
              ??Posts Eye status card to Slack (context%, current task, cwd)
              ??Auto-deletes stale idle messages on restart (spam prevention)
              ??Hot-swap: detects .new.exe ??transparent swap, no restart needed

            eye tick           one-shot status check (ctx%, cards, queue stats)
            eye --interval N   loop interval in ms (default: 100)

            ??Do NOT spawn Eye directly ??let normal commands trigger it.
            ??eye tick does NOT spawn Eye (by design).

            Queue: wkappbot.hq/runtime/slack_queue/*.json
              Each received Slack message = one file, drain every 1s.
              [EYE_QUEUE] in tick output shows pending/processing counts.
            """,

        ["slack route"] = """
            slack route --queue | --file <path> | '<json>'
            Internal Eye command ??routes a Slack message to a Claude prompt window.

            --queue       drain all *.json in slack_queue/ serially (Eye spawns this)
            --file <path> process single JSON file (kept for test scripts)
            '<json>'      inline JSON (legacy, for manual testing)

            JSON fields: text, user, ts, threadTs, channel, eyeCwd, botUsername, promptNames

            ??This is an INTERNAL command spawned by Eye ??do not call manually in production.
              For testing: slack route --file test-msg.json
            """,

        ["logcat"] = """
            logcat [regex] [file1.glob ...] [options]
            Stream or search wkappbot logs. grep-style argument order.

            First arg = content regex (';' = OR). Remaining = file globs (default: *.log).
            Path segment ';' OR: "logs/媛;????*.log" expands to 3 patterns.

            Key options:
              --hq            include wkappbot.hq/logs (finished logs live here)
              --past <dur>    scan existing files (1h, 30m, 2d). Without -f: grep-exit.
              -f / --follow   live tail after --past scan
              --timeout <dur> auto-exit after N seconds (30s, 5m)
              --dbg [pid]     capture OutputDebugString via DBWIN_BUFFER shared memory
              --json          structural JSON key+value matching (AND logic)
              -i / -n / -C N  case-insensitive / line numbers / context lines
              -v / -l / -c    invert / filenames-only / count

            Examples:
              logcat "SLACK\|ROUTE" --hq --past 1h
              logcat "error" *.log --past 30m -f
              logcat '{"role":"user"}' *.jsonl --json --hq --past 2h
              grap "keyword" *.log --hq --past 30m     (grep-compat alias)
            """,

        ["file"] = """
            file <subcommand> [options]
            File operations with 4-stage encoding detection (UTF-8/CP949).

            Subcommands:
              open <path>[:line[:col]]
                  Smart open in responsible VS Code window, then goto line/column.
              read <path> [--offset N] [--limit N] [--encoding 949|utf-16]
                  Read with line numbers. .pdf ??auto-routes to read-pdf.
                  Aliases: --path/--file, --start/--end/--count, --no-line-numbers.
              write <path> [--encoding N] (--stdin | --text "..." | --file <src>) [--append]
                  Auto backup ON. Aliases: --path, --content, --source-file, --dry-run.
              edit <old> <new> <path> [--replace-all] [--context N] [--tab-size N]
                  Indent-based block context. C-style escapes (\\n \\t \\r \\\\).
                  Same old+new = search-only (no write). --old-file/--new-file for Korean args.
                  Option aliases also work: --old-string/--text/--path/--dry-run.
              grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
                  Regex search. ';' OR in --path: "W:/A;B" expands.
                  Aliases: --pattern/--query, --root, --file.
              glob <pattern> [--path <dir>]
                  ** glob. ??ALWAYS use **/ prefix: "**/*.cs" not "*.cs".
                  Alias: --pattern.

            Aliases: file-open, file-read, file-edit, file-grep, file-glob (hyphenated)
            wkedit.exe ??busybox symlink ??file edit auto-routing

            ??CP949 source files: some WKAppBot sources have Korean in CP949.
              Check encoding before editing! Corruption = unrecoverable.
            """,

        ["code"] = """
            code open <path>[:line[:col]]
            Convenience alias for: file open
            Uses the same workspace-aware smart open logic.
            """,

        ["vscode"] = """
            vscode open <path>[:line[:col]]
            Convenience alias for: file open
            Uses the same workspace-aware smart open logic.
            """,

        ["ask"] = """
            ask gpt|gemini|claude "<question>" [file.png] [--slack] [--new-tab]
            Ask AI via CDP (Chrome DevTools Protocol, focusless).

            ask triad "<question>" [--debate [N]]
              Parallel GPT + Gemini + Claude. --debate = triad-style multi-round synthesis.

            ask gpt|gemini|claude (agent mode):
              Autonomous sub-agent loop with tools.

            ??Always ask in ENGLISH ??Korean = 3-4x token waste.
            ??CDP requires Chrome/Edge open with target AI tab.
            ??Result artifact = chat text only (CDP eval DOM read for full content).
            """,

        ["newchat"] = """
            newchat "<prompt>" [--file prompt.txt]
            Open new Claude Desktop chat + submit prompt (all focusless UIA).

            Use for session handoff when context reaches 90%+.
            Prompt is typed via WM_CHAR (focusless) ??no focus steal.

            ??Large prompts: use --file to avoid shell escaping issues.
            ??Claude Desktop must be open and visible.
            """,

        ["schedule"] = """
            schedule add|list|remove|clear [options]
            Manage scheduled prompts for auto-delivery to Claude.

            schedule add "<prompt>" --at "2026-03-30T15:00" [--repeat 1h]
            schedule list     show pending items
            schedule remove <id>
            schedule clear    remove all

            Schedules are executed by Eye (must be running).
            """,

        ["agent"] = """
            agent gemini|gpt|claude|triad "<task>" [--max-steps N] [--fresh]
            Autonomous sub-agent loop: think ??act ??observe, with tools.

            agent checkpoint [--label "text"]
              Snapshot all tracked files before risky changes.

            agent dump-patch [--out file.patch]
              git diff + per-checkpoint diffs ??unified patch file.

            agent session-status / session-clear
              Show or reset tracked files + checkpoints.

            ??checkpoint BEFORE any compile or destructive change.
            ??Restore: git apply --reverse agent-patch-*.patch
            """,

        ["help"] = "Global meta-options ??work on ANY command, ANY position in args.\n\n"
            + "--help / -h\n"
            + "  Print command description, subcommands, key options, examples.\n"
            + "  wkappbot <cmd> --help\n"
            + "  wkappbot <cmd> <subcmd> --help\n"
            + "  Looks up CommandHelpMap ??prints focused entry ??exits 0.\n\n"
            + "--regression\n"
            + "  Print --help THEN run all stored test scripts for this command/subcommand.\n"
            + "  wkappbot <cmd> [<subcmd>] --regression\n"
            + "  Script location: {DataDir}/experience/tests/{cmd}/{subcmd}/*.(sh|ps1|cmd)\n"
            + "  Shows [PASS]/[FAIL] per script + summary line.\n"
            + "  Failure output: last 5 lines of stdout/stderr shown.\n"
            + "  Self-test: wkappbot help --regression\n\n"
            + "Adding test scripts:\n"
            + "  Use suggest resolve with --i-completed-...-willkim-allowed-this-script <test.sh|test.ps1|test.cmd>\n"
            + "  Scripts auto-copied to experience/tests/{cmd}/{subcmd}/ on resolve.",
    };
}

