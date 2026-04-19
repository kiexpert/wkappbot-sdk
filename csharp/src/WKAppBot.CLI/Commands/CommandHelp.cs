// CommandHelp.cs -- Per-command --help descriptions for future Claude instances.
// Usage: wkappbot <command> --help  |  wkappbot <command> <subcommand> --help
//        wkappbot <command> [<subcommand>] --regression  -- help + run stored test scripts
//
// STRUCTURE NOTE: Currently a manual dictionary.
// FUTURE: Replace with [Command] / [SubCommand] / [Description] attributes on
//   handler methods + Reflection scan -- code changes auto-expose in --help.
//   See: suggestions.jsonl "Reflection-based --help auto-generation

namespace WKAppBot.CLI;

internal partial class Program
{
    static bool IsEvidenceScript(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        var name = Path.GetFileNameWithoutExtension(path);
        return (ext is ".sh" or ".ps1" or ".cmd" or ".bat") && !name.Contains(".bak");
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
        PrintRelatedSkills(command, sub);

        // Auto-regression: run stored test scripts unless opted out
        if (!restArgs.Any(a => a is "--no-regression"))
            RunRegressionScripts(command, sub, autoMode: true);

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

        var sub = restArgs.FirstOrDefault(a => !a.StartsWith('-'));

        var key = sub != null ? $"{command} {sub}" : command;
        if (!CommandHelpMap.TryGetValue(key, out var helpText)
            && !CommandHelpMap.TryGetValue(command, out helpText))
            Console.WriteLine($"No --help entry for: {command}{(sub != null ? " " + sub : "")}");
        else
            Console.WriteLine(helpText.TrimStart('\n'));

        RunRegressionScripts(command, sub, autoMode: false);
        return true;
    }

    /// <summary>
    /// Find and run all regression scripts for the given command/subcommand.
    /// autoMode=true: from --help (silent if no scripts). false: from --regression.
    /// </summary>
    internal static (int passed, int failed) RunRegressionScripts(string command, string? sub, bool autoMode = false)
    {
        var testsRoot = Path.Combine(DataDir, "experience", "tests");
        string[] scriptFiles;

        if (sub != null)
        {
            var subDir = Path.Combine(testsRoot, command, sub);
            scriptFiles = Directory.Exists(subDir)
                ? Directory.GetFiles(subDir)
                    .Where(f => IsEvidenceScript(f))
                    .OrderBy(f => f)
                    .ToArray()
                : [];
        }
        else if (autoMode)
        {
            // Auto-mode (from --help): prefer smoke/ subdir for fast tests; fall back to non-recursive root
            var smokeDir = Path.Combine(testsRoot, command, "smoke");
            var searchDir = Directory.Exists(smokeDir) ? smokeDir : Path.Combine(testsRoot, command);
            scriptFiles = Directory.Exists(searchDir)
                ? Directory.GetFiles(searchDir)  // non-recursive: root or smoke/ only
                    .Where(f => IsEvidenceScript(f))
                    .OrderBy(f => f)
                    .ToArray()
                : [];
        }
        else
        {
            // Full regression (--regression): all scripts under command dir
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
            if (!autoMode)
            {
                Console.WriteLine();
                Console.Error.WriteLine($"[REGRESSION] No test scripts found for: {command}{(sub != null ? " " + sub : "")}");
                Console.WriteLine($"  Expected: {Path.Combine(testsRoot, command, sub ?? "*")}/*.(sh|ps1|cmd)");
            }
            return (0, 0);
        }

        // Header: list scripts naturally so user knows what will run
        var label = $"{command}{(sub != null ? " " + sub : "")}";
        Console.WriteLine();
        Console.WriteLine("-- Regression: " + label + " (" + scriptFiles.Length + " script(s)) " + new string('-', Math.Max(0, 44 - label.Length)));
        foreach (var s in scriptFiles)
            Console.WriteLine("   " + Path.GetFileName(s));
        Console.WriteLine();

        int passed = 0, failed = 0;
        var gitBash = @"C:\Program Files\Git\usr\bin\bash.exe";
        var bashExe = File.Exists(gitBash) ? gitBash : "bash";

        // Convert Windows path to POSIX for bash (e.g. D:\path \u2192 /w/path)
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
                var scriptArgs = ext switch
                {
                    ".sh" => $"\"{ToPosix(script)}\"",
                    ".ps1" => $"-NoProfile -ExecutionPolicy Bypass -File \"{script}\"",
                    ".cmd" or ".bat" => $"/c \"{script}\"",
                    _ => throw new InvalidOperationException()
                };
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = runner,
                    Arguments = scriptArgs,
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
            Console.WriteLine("ALL PASS (" + passed + "/" + passed + ")  OK");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAIL: " + failed + " failed, " + passed + " passed (" + (passed + failed) + " total)");
        }
        Console.ResetColor();
        return (passed, failed);
    }

    // -- Help text per command (and command+subcommand) --------------------------
    // Keep entries short (~10 lines). Focus on: what it does, key options, gotchas,
    // examples. Avoid restating obvious things.
    //
    // Key format: "command" or "command subcommand"
    // FUTURE: auto-generated from [Description] attributes via reflection.

    internal static readonly Dictionary<string, string> CommandHelpMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["a11y"] = """
            a11y <action> <grap>[#uia-scope] [options]
            Universal accessibility control: UIA -> Win32 -> SendInput 3-tier fallback.

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

            RULE: Always reply to Slack messages IN SLACK via `slack reply --msg <ts>`.
              Answering only in the Claude Code prompt is NOT acceptable.
            NOTE: `slack route` is an internal Eye command -- do NOT call manually unless testing.
            """,

        ["suggest"] = "suggest [subcommand]\n"
            + "AI suggestion management: propose / review / resolve workflow.\n\n"
            + "Subcommands:\n"
            + "  (no args)         submit a suggestion (opens editor or reads stdin)\n"
            + "  list              show active (unresolved) suggestions\n"
            + "  resolve <ts>      mark resolved (REQUIRES evidence script -- see below!)\n"
            + "  repost            re-send suggestions to Slack if ts missing\n\n"
            + "RESOLVE GUARD -- mandatory test evidence required:\n"
            + "  wkappbot suggest resolve <ts> \"note\"\n"
            + "    --i-completed-<cmd>-<subcmd>-willkim-allowed-this-script <test.sh|test.ps1|test.cmd>\n\n"
            + "  Evidence filename must match: test-{cmd}-{subcmd}-*.<sh|ps1|cmd>\n"
            + "  Script must: reference the command, exit 0 (all tests pass)\n"
            + "  Failure -> resolve BLOCKED (fix the test or the code first)\n\n"
            + "  Regression: ALL previously stored scripts in experience/tests/{cmd}/{subcmd}/\n"
            + "  are re-run on each resolve. If any fail -> blocked.\n\n"
            + "evidence_file is saved to history so you can trace what was tested.\n\n"
            + "Examples:\n"
            + "  wkappbot suggest resolve 2026-03-17T05 \"fixed logcat --dbg race\"\n"
            + "    --i-completed-logcat-dbg-willkim-allowed-this-script test/test-logcat-dbg-listener.sh",

        ["eye"] = """
            eye [options]
            WK AppBot Eye -- persistent Slack daemon + status overlay.
            Runs as background process, auto-spawned by most commands.

            Key behaviors:
              - Receives Slack messages -> queues to slack_queue/ -> routes to Claude
              - Posts Eye status card to Slack (context%, current task, cwd)
              - Auto-deletes stale idle messages on restart (spam prevention)
              - Hot-swap: detects .new.exe -> transparent swap, no restart needed

            eye tick           one-shot status check (ctx%, cards, queue stats)
            eye --interval N   loop interval in ms (default: 100)

            NOTE: Do NOT spawn Eye directly -- let normal commands trigger it.
            NOTE: eye tick does NOT spawn Eye (by design).

            Queue: wkappbot.hq/runtime/slack_queue/*.json
              Each received Slack message = one file, drain every 1s.
              [EYE_QUEUE] in tick output shows pending/processing counts.
            """,

        ["slack route"] = """
            slack route --queue | --file <path> | '<json>'
            Internal Eye command -- routes a Slack message to a Claude prompt window.

            --queue       drain all *.json in slack_queue/ serially (Eye spawns this)
            --file <path> process single JSON file (kept for test scripts)
            '<json>'      inline JSON (legacy, for manual testing)

            JSON fields: text, user, ts, threadTs, channel, eyeCwd, botUsername, promptNames

            NOTE: Internal command spawned by Eye -- do not call manually in production.
              For testing: slack route --file test-msg.json
            """,

        ["logcat"] = """
            logcat [regex] [file1.glob ...] [options]
            Stream or search wkappbot logs. grep-style argument order.

            First arg = content regex (';' = OR). Remaining = file globs (default: *.log).
            Path segment ';' OR: "logs/媛;<name>*.log" expands to 3 patterns.

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
                  Read with line numbers. .pdf -> auto-routes to read-pdf.
                  Aliases: --path/--file, --start/--end/--count, --no-line-numbers.
              write <path> [--encoding N] (--stdin | --text "..." | --file <src>) [--append]
                  Auto backup ON. Aliases: --path, --content, --source-file, --dry-run.
              edit <old> <new> <path> [--replace-all] [--context N] [--tab-size N]
                  Indent-based block context. C-style escapes (\\n \\t \\r \\\\).
                  Same old+new = search-only (no write). --old-file/--new-file for Korean args.
                  Option aliases also work: --old-string/--text/--path/--dry-run.
              grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
                  Regex search. ';' OR in --path: "D:/A;B" expands.
                  Aliases: --pattern/--query, --root, --file.
              glob <pattern> [--path <dir>]
                  ** glob. NOTE: ALWAYS use **/ prefix: "**/*.cs" not "*.cs".
                  Alias: --pattern.

            Aliases: file-open, file-read, file-edit, file-grep, file-glob (hyphenated)
            wkedit.exe -- busybox symlink -- file edit auto-routing

            NOTE: CP949 source files -- some WKAppBot sources have Korean in CP949.
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

            NOTE: Always ask in ENGLISH -- Korean = 3-4x token waste.
            NOTE: CDP requires Chrome/Edge open with target AI tab.
            NOTE: Result artifact = chat text only (CDP eval DOM read for full content).
            """,

        ["newchat"] = """
            newchat "<prompt>" [--file prompt.txt]
            Open new Claude Desktop chat + submit prompt (all focusless UIA).

            Use for session handoff when context reaches 90%+.
            Prompt is typed via WM_CHAR (focusless) -- no focus steal.

            NOTE: Large prompts: use --file to avoid shell escaping issues.
            NOTE: Claude Desktop must be open and visible.
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
            Autonomous sub-agent loop: think -> act -> observe, with tools.

            agent checkpoint [--label "text"]
              Snapshot all tracked files before risky changes.

            agent dump-patch [--out file.patch]
              git diff + per-checkpoint diffs -> unified patch file.

            agent session-status / session-clear
              Show or reset tracked files + checkpoints.

            NOTE: checkpoint BEFORE any compile or destructive change.
            NOTE: Restore: git apply --reverse agent-patch-*.patch
            """,

        ["help"] = "Global meta-options -- work on ANY command, ANY position in args.\n\n"
            + "--help / -h\n"
            + "  Print command description, subcommands, key options, examples.\n"
            + "  wkappbot <cmd> --help\n"
            + "  wkappbot <cmd> <subcmd> --help\n"
            + "  Looks up CommandHelpMap -> prints focused entry -> exits 0.\n\n"
            + "--regression\n"
            + "  Print --help THEN run all stored test scripts for this command/subcommand.\n"
            + "  wkappbot <cmd> [<subcmd>] --regression\n"
            + "  Script location: {DataDir}/experience/tests/{cmd}/{subcmd}/*.(sh|ps1|cmd)\n"
            + "  Shows [PASS]/[FAIL] per script + summary line.\n"
            + "  Failure output: last 5 lines of stdout/stderr shown.\n"
            + "  Self-test: wkappbot help --regression\n\n"
            + "--no-regression\n"
            + "  Skip auto-regression when using --help.\n"
            + "  wkappbot <cmd> --help --no-regression\n"
            + "  (Auto-regression runs by default on --help)\n\n"
            + "Adding test scripts:\n"
            + "  Use suggest resolve with --i-completed-...-willkim-allowed-this-script <test.sh|test.ps1|test.cmd>\n"
            + "  Scripts auto-copied to experience/tests/{cmd}/{subcmd}/ on resolve.",

        ["web"] = """
            web open [url] [--port N] [--headless] [--no-launch]
            web navigate <url> [--port N]
            web tabs [--port N]
            web html [<url>] [-o out.html] [--port N]
            web capture [-o out.png] [--port N]
            web url [--port N]  /  web title [--port N]
            web close [--port N]  /  web status [--port N]
            web fetch <url>  /  web search <query>  (no web read -- use a11y read <grap>)
            web run <steps-file.txt> [--port N]

            Removed (use a11y instead -- CSS auto-routed to CDP):
              web click/type/text/screenshot/wait/check/select/restore/eval
              -> a11y <action> "*Chrome*#<css-selector>"
            """,

        ["windows"] = """
            windows [grap] [--deep] [--process <name>] [--cmd <substr>]
            List top-level windows with title, class, PID, size.

            No args: all visible windows. With grap: filter by pattern.
            --deep: include child windows. --process: filter by process name.
            """,

        ["mcp"] = "mcp\nRun as MCP (Model Context Protocol) stdio server.\nExposes wkappbot_cli tool for JSON-RPC calls.\nUsed by Claude Desktop, VS Code MCP clients.",

        ["gc"] = "gc [pattern] [--days N] [--sweep]\nGarbage collect old logs, temp files, stale caches.\n--days N: only files older than N days (default: 7).\n--sweep: aggressive cleanup.",

        ["clipboard"] = "clipboard [read|write] [text]\nRead or write system clipboard.\nNo args: read. With text: write.",

        ["hack"] = "a11y hack <grap>[#scope] [--at x,y] [--ltrb l,t,r,b] [--engine gemini|gpt]\nForce DYN-A11Y analysis: capture -> CCA segmentation -> OCR -> Vision -> dynamic a11y tree.",

        ["hack-hover"] = "a11y hack-hover [--parent-pid N] [--timeout Ns]\nMouse hover analysis worker. Tracks mouse -> UIA element -> grap pattern + verification.\n9s idle triggers full hack analysis. Eye auto-quiets polling. Ctrl+C to stop.",

        ["hack-input"] = "a11y hack-input [--parent-pid N] [--timeout Ns]\nKeyboard focus analysis worker. Tracks focused element -> input capabilities + parent chain.\nShows: patterns (Value/Text/Invoke/Toggle), grap, process info. Ctrl+C to stop.",

        ["speak"] = "speak \"text\" [--bg] [--mouse] [--target <grap>] [--voice <name|culture>] [--size N]\nWindows TTS voice output + karaoke overlay.\n--bg: background (return immediately).\n--mouse: overlay at cursor position.\n--target <grap>: overlay on specified window.\n--voice <name|culture>: select an installed voice.\n--size N: font size px (default 32).",

        ["whisper"] = "whisper [options]\nReal-time speech analysis and tokenization.\nSubcommands:\n  dictate [--target <grap>] [--lang ko|en] [--confidence N] [--no-space] [--enter]\n    Live dictation into the focused control as text.\n  study [--batch N] [--for <duration>] [--engine gemini|gpt]\n    Batch-transcribe MP3 segments via AI.\n  pipeline [--batch N] [--interval N] [--engine gemini|gpt] [--loop|--once] [--for <duration>] [--script <path>|--appbot-cmd <path>]\n    Stable orchestration: study -> auto-slice/index -> phoneme harvest.\n    Runs the external learning script if present, otherwise falls back to the built-in pipeline.\n    Phoneme maintenance runs automatically when coverage is missing.\n  slice | clean | index [--split]\n    Offline segment processing and phoneme indexing.\n  phoneme-search [--db <dir>] [--query <file|text>] [--top N] [--no-harvest]\n    Rank phoneme_db candidates and harvest query chunks back into the DB.\n  phoneme-loop [--in <slices-dir>] [--out <db-dir>] [--interval N] [--move] [--dry-run] [--once]\n    Repeated phoneme split loop for new MP3s. Default runs until Ctrl+C; --once exits after one scan.\n    Basic phoneme buckets are maintained automatically.\n    If no source audio exists, bootstrap samples are synthesized with rotated TTS voices and then sent to study.\n",

        ["whisper dictate"] = "whisper dictate [--target <grap>] [--lang ko|en] [--confidence N] [--no-space] [--enter]\nLive microphone dictation into the focused control.\nDefault target: focused control in the foreground window.\nUse --target <grap> to force a specific window/control.\nUse --confidence N to ignore weak recognitions (default 0.45).\nUse --no-space to suppress the trailing space after each phrase.\nUse --enter to send Enter after each accepted phrase.",

        ["whisper-ring"] = "whisper-ring [x y] [--keyboard|--dictate]\nStandalone Whisper Ring worker. Default: overlay/analysis only.\nWith --keyboard or --dictate: reuse whisper dictate behavior and type recognized text.\nPosition arguments x y place the ring window when not dictating.",

        ["screen"] = "screen blank [--duration N] | restore\nBlank all monitors (privacy/automation). Auto-restore after N seconds.",

        ["hotswap"] = "hotswap\nManually trigger Eye hot-swap check (normally automatic on publish).",

        ["tick"] = "tick\nOne-shot Eye status check (ctx%, memory, cards, guardian).",

        ["dashboard"] = "dashboard\nOpen WKAppBot dashboard (WPF UI).",

        ["model"] = "model [list|set <name>]\nManage AI model selection for ask/agent commands.",

        ["knowhow"] = "knowhow [show|list] [--app <proc>] [--action <action>]\nView recorded automation knowhow from experience DB.",

        ["run"] = "run <scenario.yaml> [--dry-run]\nExecute YAML automation scenario.",

        ["validate"] = "validate <scenario.yaml>\nValidate YAML scenario syntax without executing.",

        ["capture"] = "capture [grap] [--output file.png]\nScreenshot a window or screen region.",

        ["ocr"] = "ocr [grap] [--lang kor+eng]\nOCR text from a window or element.",

        ["inspect"] = "inspect [grap] [--depth N]\nInspect UIA tree of a window.",

        ["find"] = "find [grap] [--depth N]\nFind elements in a window's UIA tree.",

        ["focus"] = "focus [grap]\nBring a window to foreground.",

        ["prompt"] = "prompt send \"<name>\" \"<msg>\" [--after 60s] [--when-idle 9s]\nprompt list\nDeliver prompts to AI chat windows.",

        ["win-move"] = "win-move <grap> --x N --y N [--w N] [--h N]\nMove/resize a window by grap pattern.",

        ["com"] = "com ls|use|current|methods|call [args]\nCOM adapter commands (session per folder).\n  ls              List profiles/adapters\n  use <name>      Select profile for current folder (.wkcom/session.json)\n  current         Show selected profile\n  methods         List callable methods\n  call <m> [p]    Invoke method via selected adapter",

        ["telegram"] = "telegram send \"text\" [--window <title>] [--no-fallback-enter]\nSend message via Telegram desktop client (A11Y-first, Enter fallback).",

        ["watch"] = "watch [--duration N] [--live] [--win32] [--interval N]\nReal-time element tracking under mouse cursor. --live: print cursor position updates.",

        ["win-click"] = "win-click <window-title> <x> <y> [--uia]\nClick a coordinate inside a window + detect UIA element at that point.",

        ["do"] = "do <window-title> <form-id> <button-text> [--confirm]\nFull automation: combo select + button click + dialog handling.",

        ["snapshot"] = "snapshot <window-title> [--tag N] [--depth N]\nOne-shot diagnostic: UIA tree + screenshot + OCR.",

        ["find-prompts"] = "find-prompts\nList all Claude Code prompt windows (FindAllPrompts). Shows usable/blocked/hidden state.",

        ["chat"] = """
            chat [<question>] [-p] [--no-fallback]
            Claude Code CLI passthrough with auto-fallback to ask triad on rate-limit.

              wkappbot chat                Interactive session (exec claude, inherit stdio)
              wkappbot chat "<q>"          Non-interactive: claude -p <q>, fallback on limit
              wkappbot chat -p "<q>"       Same as above (explicit print mode)
              wkappbot chat --no-fallback  Disable auto-fallback to ask triad

            Fallback triggers:
              1) `claude` binary not on PATH  -> route to ask triad
              2) stdout/stderr contains:      usage limit, rate limit, 5-hour limit,
                                              session exhausted, HTTP 429,
                                              Claude is temporarily unavailable
              3) exit != 0 + one of the above signatures in output
            """,

        ["a11y hack-hover"] = """
            a11y hack-hover [--parent-pid N]
            Standalone hover analysis worker -- mouse CCA + UIA + overlay.

            Independent process: runs until Ctrl+C or parent exits.
            Logs to eye-hack.log (consolidated).
            Eye spawns this automatically; can also run from console directly.
            """,

        ["skill"] = """
            skill <subcommand> [options]
            AI skill management -- executable knowhow for Claude instances.
            Storage: {callerCwd}/skills/ (project, git-tracked)
                     {hq}/skills/       (installed, [HQ] tag in list)

            Subcommands:
              list [app]                        List skills grouped by app, most recent first
              read <id>                         Full detail: steps, refs, version
              search <keyword> [--app X]        Search title/desc/tags/steps
              contribute --app X --title Y --desc Z
                         [--steps "s1|s2"] [--tags "t1,t2"] [--id <slug>]
                         [--refs "file:line:pattern|..."]
                                               Create or update (auto version bump)
              delete <id>                       Remove from project dir
              install [--app X] [--force]       Copy project -> HQ (runs on publish)
              export [--app X] [--out f.zip]    Export to zip
              import <file.zip>                 Import into project dir
              verify <id>                       Check source_refs still match code
              audit [--app X]                   Audit all skills for stale/missing refs

            Examples:
              wkappbot skill list
              wkappbot skill audit
              wkappbot skill read handoff-checklist
              wkappbot skill search retry
              wkappbot skill contribute --app wkappbot-webbot --title "X" --desc "Y" \\
                --refs "csharp/src/WKAppBot.WebBot/CdpClient.cs::pattern"
            """,

        ["claude-usage"] = """
            claude-usage
            Show Claude API token usage: JSONL size + ctx% estimate.

            ctx% = total JSONL size / ~20MB context limit.
            Warns at 8MB, urgent at 10MB.

            Also probes claude.ai/settings/usage via CDP for plan usage %
            (fallback: UIA scrape of Claude Desktop).
            """,

        ["claude-proxy"] = """
            claude-proxy [--port 7070] [--inject-context] [--verbose]

            Local Anthropic API proxy -- sits between Claude Code and the real API.
            Configure: ANTHROPIC_BASE_URL=http://localhost:7070 (in .claude/settings.json or env).

            Features:
              - Transparent SSE streaming proxy to https://api.anthropic.com
              - --inject-context: prepends WKAppBot session info (ctx%, pending suggests)
                into every request's system message
              - Context-limit detection: injects handoff note into the error response
                so Claude sees it directly instead of a generic error

            stdout: the proxy URL (for scripting)
            stderr: request log
            """,
    };
}
