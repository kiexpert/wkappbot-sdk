using System.Runtime.InteropServices;

namespace WKAppBot.CLI;

// partial class: PrintUsage + Error + GetArgValue (shared utilities)
internal partial class Program
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    static void CreateHardLinkNative(string linkPath, string targetPath) =>
        CreateHardLink(linkPath, targetPath, IntPtr.Zero);
    /// <summary>Full help text -- single source of truth for CLI help AND MCP tool description.</summary>
    internal static string GetUsageText()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        return $@"
WKAppBot{verStr} - Windows App Automation Test Framework
Give AI eyes and hands -- operate any app, help any human. No API, no rewrite needed.
All AI agents welcome -- Claude, GPT, Gemini, Copilot, and beyond.
Your testing, coding, and ideas are appreciated. Let's build together.

Usage:
  wkappbot <command> [options]

=== Public Commands ===================================================

  a11y <action> <grap>[#uia-scope] [options]        (alias: a11y.exe / wka11y.exe)
      Universal accessibility interface -- 31 standard actions for ANY window.
      3-tier fallback: UIA -> Win32 -> SendInput. Busybox: symlink `a11y.exe` works.

      Auto-pipeline per action: blocker dismiss -> minimize restore -> tab activate
        -> zoom/magnifier -> execute (3-tier) -> result feedback (green/amber) -> fade

      Window (8):  close  kill  minimize  maximize  restore  focus  move  resize
      Element (13): read  find  highlight  invoke  click  toggle
                    expand  collapse  select  scroll  type  set-value  set-range
      Async (2):   wait  eval
      Utility (4): clipboard  clipboard-write  file-read  file-write
      Discovery (4): inspect  windows  screenshot  ocr

      Target:  --nth 3 | 3~ | ~3 | 2~4 | 1,3~    --all    (default: first match)
      Options: --depth N  --force  --value N  --direction  --amount
      Grap:    AI target language for windows + a11y nodes.
               Human sees windows; AI points with grap.
               First exact match stops interactive actions fast.
               Arrays are for lookups; actions consume the highest-priority target.
               ';' = OR   # = scope drill-down
               Ex: ""*Notepad*#*File*"" -> Notepad's File menu

      a11y find ""*app*"" --depth 5       # MUD: look (Win32 + UIA children)
      a11y highlight ""*app*#*button*""   # visualize target with zoom overlay
      a11y invoke ""*app*#*button*""      # click (UIA Invoke -> BM_CLICK -> SendInput)
      a11y close ""*Chrome*"" --nth 2~    # close 2nd window onwards
      a11y type ""*app*#*edit*"" ""hello""

  find <keyword> [--deep] [--limit N] [--process <name>]
      Unified search: window titles + UIA accessibility elements.
      --deep: Thorough search (depth 12, slower but finds more).
  run <scenario.yaml> [-v] [--no-watch]
      Run a YAML test scenario with background element tracking.
  do <window-title> <form-id> <button-text> [--confirm]
      Full automation: combo select + button click + dialog handling.
  scan <window-title> [--save] [--ocr] [--detail]
      Scan app structure, learn controls, build Experience DB.
  ocr <window-title|image.png> [--save] [-o file]
      Extract text from window/image using Windows.Media.Ocr.
  capture <window-title> [-o output.png] [--form <id>]
      Capture a screenshot of a window or MDI child form.
  dismiss <window-title> [keywords...]
      Auto-dismiss notice/popup windows (OCR importance check).
  input <window-title> <text>
      Type text into a window (focusless PostMessage preferred).
  whisper [options]
      Real-time speech recognition + sound tokenization overlay.
  whisper study [--batch N] [--for <duration>] [--engine gemini|gpt]
      Batch-transcribe MP3 segments via AI. --for loops until timeout.
      Duration: 30=30s, 2m, 500ms, 1.1s, 2h
  whisper slice [--in <dir>] [--min-ms N] [--max-ms N]
      Slice long audio into word-level MP3 segments.
  whisper clean [--wav-dir <dir>] [--dry-run]
      Sort wav/ root: voice->_unknown, noise->Recycle Bin.
  whisper index [--in <slices-dir>] [--out <db-dir>] [--move] [--dry-run]
      Extract first-syllable soundCode -> phoneme_db/<octal>/word.mp3.
      Files with <9 voiced frames (~90ms) -> Recycle Bin automatically.
  mcp
      Start MCP stdio server (wkappbot_cli tool for JSON-RPC clients).
      Add to .mcp.json: {{ ""wkappbot"": {{ ""command"": ""wkappbot"", ""args"": [""mcp""] }} }}
  web <subcommand> [options]
      Chrome DevTools Protocol web automation (open/click/type/eval).
      Type 'wkappbot web help' for all subcommands.
  web fetch <url> [--max-chars N]
      HTTP GET -- returns response body (no browser needed).
  web search <query> [--limit N]
      Google search via Chrome CDP (no API key required).
  web html <url>
      Capture raw HTML of browser tab via CDP.
  file read <path> [--offset N] [--limit N] [--encoding 949|utf-16]
      Read file with line numbers. .pdf -> auto-routes to read-pdf.
  file read-pdf <path> [--ocr] [--page N] [--pages N-M]
      Extract text from PDF. --ocr: Windows.Media.Ocr fallback for scanned PDFs.
  file grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
      Regex search across files. -i=case-insensitive, -C=context lines.
  file glob <pattern> [--path <dir>]
      Find files by ** glob pattern. ! ALWAYS use **/ prefix (e.g. **/*.cs).
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye -- live overlay + Slack daemon (always on).
      ctx=N% in tick output, auto-deletes stale idle messages on restart.
  newchat ""prompt"" [--file prompt.txt]
      Open new Claude Desktop chat + submit prompt (all focusless UIA).
      Use for session handoff when context reaches 90%+.
  slack send|reply|upload|screenshot|listen|catch-up
      Slack messaging (Socket Mode, always-on prompt forwarding).

=== Detail Commands ===================================================

Inspection:
  windows [filter] [--uia] [--deep] [--process <name>] [--limit N]
      List visible windows in Z-order. --uia: also search UIA elements.
  inspect <grap>[#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]
      Dump UIA tree of a window. # narrows to UIA element by name.
      --filter: search A11Y elements. Ex: *App*#*Panel*
  win-click <window-title> <x> <y> [--uia]
      Click a coordinate inside a window + detect UIA element.
  focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
      Inspect the currently focused window (countdown + dump).
  watch [--duration N] [--live] [--win32] [--interval N]
      Real-time element tracking under mouse cursor.

  tab-select <grap>[#<uia-scope>] --aid <id> [--list|--select <text>|--index N]
      Focusless tab switching via UIA SelectionItem pattern.
      # scopes to specific form. Ex: *App*#*Panel* --aid 1000

Automation:
  click <window-title> <form-id> [button-text] [--combo N INDEX]
      Click a button in MDI form (lower-level than 'do').
  dialog-click <dialog-title> [button-index]
      Click a button in a top-level dialog (physical click).
  toolbar-ocr <window-title> [--click ""text""] [--save]
      OCR-scan MFC toolbar panes + click by text match.
  titlebar <window-title> <form-id> [button-index] [--ocr]
      Access custom title bar buttons (focusless PostMessage).
  form-dump <window-title> <form-id> [--depth N]
      Recursively dump all controls in an MDI child form.

Testing & Analysis:
  validate <scenario.yaml>
      Validate a YAML scenario file (syntax + structure check).
  uia-test <grap>[#<uia-scope>] [--invoke <name>]
      Systematic 7-phase MFC UIA pattern test with zoom overlay.
      # narrows UIA root. Ex: *App*#*Panel* --invoke BtnName
  chart-analyze <window-title|image.png> [--form <id>] [--candles N]
      Extract OHLC+volume from chart screenshots (3 strategies).
      --tooltip: Y-axis recalibration. --debug: overlay image.
  tooltip-probe <process-name> [--capture]
      Enumerate tooltip windows for a process.
  hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only]
      HTS MDI stress test with memory tracking.

Utility:
  snapshot <window-title> [--tag N] [--depth N]
      One-shot diagnostic: UIA tree + screenshot + OCR.
  knowhow write|read|web|web-list
      Record/read per-control automation notes.
  skill list|show|contribute|export|import
      Manage executable automation skills (YAML-like structured knowledge).
  schedule add|list|remove|clear
      Manage scheduled prompts for auto-recovery.
  logcat [regex] [file1.glob] [file2.glob ...] [options]
      Stream/search logs. grep-style: first arg=content regex, rest=file globs.
      --hq              Include wkappbot.hq/logs + openclaw dirs
      --past <dur>      Scan existing files (e.g. 1h, 30m, 2d). Without -f: grep-style exit.
      -f, --follow      Live tail after --past scan
      --timeout <dur>   Auto-exit after duration (e.g. 30s, 5m)
      -r[=N]            Recursive. -r=unlimited, -r=3=depth 3
      --dbg [pid]       Capture OutputDebugString (DBWIN_BUFFER shared memory)
      --json            Structural JSON key+value matching
      -A/-B/-C N        After/before/context lines
      -v -l -c -m N     Invert, filenames-only, count, max-matches
      -i / -n           Case-insensitive / line numbers
      Grap patterns: wildcards, regex: prefix, ';' OR, path-segment ';' expansion.
  grep / grap  <pattern> [files]   (logcat aliases, grep-compat arg order -- run `grap --help` for details)
  ask gpt|gemini|claude ""question"" [file.png] [--slack] [--new-tab]
      Ask AI via CDP (focusless). Auto-closes blank tabs, validates URL.
  ask triad ""question"" [--debate [N]]
      Parallel GPT + Gemini + Claude. --debate: dialectic loop (max 3 rounds, cross-prompting).
      --debate N: early exit at N consensus. Without --debate: R0 only (one answer each).
  chat [""question""] [-p] [--no-fallback]
      Claude Code CLI passthrough. On rate-limit markers -> auto-fallback to ask triad.
      No args: exec claude interactively (inherits stdio).
  agent gemini|gpt|claude|triad ""task"" [--max-steps N] [--fresh]
      Autonomous sub-agent loop with filesystem + web tools.
  agent checkpoint [--label ""text""]
      Save mid-session snapshot of all tracked files (before compile / risky change).
      Tracked: every a11y file-write is registered in agent-session.json (persistent).
  agent dump-patch [--out file.patch] [--apply]
      git diff HEAD -> unified patch + per-checkpoint diffs saved to repo root.
      Patch header includes: apply / reverse / checkpoint-restore / original-restore hints.
  agent session-status
      Show tracked files + checkpoints in current agent session.
  agent session-clear
      Delete agent-session.json + agent-checkpoints/ (start fresh).
  win-move <window-title> [--right-top] [--x N --y N]
      Move a window to a specific position.
  screen off [--no-check]
      Turn off monitor immediately.
  com ls|use|current|methods|call
      COM adapter commands (session per folder).
  telegram send ""text""
      Send message via Telegram (A11Y-first).
  suggest ""text"" [file.png]
      Send suggestion/feature request to Slack (auto-tags CWD workspace).
      ! Write in ENGLISH -- Korean = 2-3x token cost. Short & precise wins.
  suggest resolve <ts> ""note""
      Mark suggestion resolved + Slack thread reply.
  claude-usage
      Probe Claude Desktop usage (session/weekly %) via UIA. Slack alerts at 85%/95%.
  prompt-probe [names...] [--names a,b,c] [--depth N] [--limit N] [--all]
      Probe prompt candidates with thread-author names + dump UIA hits/submit state.

General Options:
  -v, --verbose         Verbose output
  --report <dir>        Generate HTML report in directory
  --timeout <duration>  Hard kill after N time (exit 124). e.g. 30, 2m, 500ms
  -h, --help            Show this help message

Rules (for fellow Claude Code agents):
  * Questions to user: ALWAYS send via BOTH Slack AND here simultaneously.
    (Do not ask only in Slack or only here - always both!)
  * AppBotEye auto-launches for all commands (except eye/slack/help/validate).
  * Slack + Prompt forwarding + keyword monitoring: ALWAYS ON (no disable flags).
  * On prompt input failure: diagnose foreground window + share via Slack.
  * NEVER create options to disable forwarding to Claude or disable AppBotEye.

Data Directory:
  Runtime data (profiles, logs, handlers, output) stored in:
  {{exe_dir}}/wkappbot.hq/
";
    }

    static int PrintUsage()
    {
        Console.WriteLine(GetUsageText());
        return 0;
    }

    /// <summary>
    /// Lightweight tick emitter -- advertise work being done on the card.
    /// Usage: wkappbot tick <tag> [status] [--find-host]
    /// --find-host: auto-detect the most recent Claude session from eye_ticks.jsonl
    ///   (for PostPublish where process tree doesn't reach Claude Desktop)
    /// Not a meta tag, so it won't be filtered out of cards.
    /// </summary>
    static int TickCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot tick <tag> [status] [--find-host]");
            Console.WriteLine("  e.g. wkappbot tick build \"building feature X\"");
            Console.WriteLine("  --find-host: auto-attach to most recent Claude session card");
            return 1;
        }

        var findHost = args.Any(a => a == "--find-host");
        var filtered = args.Where(a => a != "--find-host").ToArray();
        var tag = filtered.Length > 0 ? filtered[0] : "tick";
        var status = filtered.Length > 1 ? string.Join(" ", filtered.Skip(1)) : "done";

        if (findHost)
        {
            // Find most recent Claude host from eye_ticks.jsonl and emit tick with that host
            EmitEyeTickWithHost("tick", tag, status);
        }
        else
        {
            EmitEyeTick("tick", tag, status);
        }
        Console.Error.WriteLine($"[TICK] tag={tag} status={status}");
        return 0;
    }

    static int PrintVersion()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $"{ver.Major}.{ver.Minor}.{ver.Build}" : "unknown";
        Console.WriteLine($"wkappbot v{verStr}");
        return 0;
    }

    static int Error(string msg)
    {
        // In Eye pipe mode, Console.Error is not forwarded to the pipe client -- use Console.Out instead.
        if (Program.RunningInEye)
        {
            Console.WriteLine(msg);
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(msg);
        Console.ResetColor();
        return 1;
    }

    /// <summary>
    /// Parse human-friendly duration: "30"=30s, "5m"=5min, "500ms"=500ms, "2h"=2h.
    /// Bare number = seconds. Returns TimeSpan.Zero on parse failure.
    /// </summary>
    internal static TimeSpan ParseDuration(string s)
    {
        s = s.Trim().ToLowerInvariant();
        var ic = System.Globalization.CultureInfo.InvariantCulture;
        var ns = System.Globalization.NumberStyles.Float;
        if (s.EndsWith("ms") && double.TryParse(s[..^2], ns, ic, out var ms))  return TimeSpan.FromMilliseconds(ms);
        if (s.EndsWith("d")  && double.TryParse(s[..^1], ns, ic, out var d))   return TimeSpan.FromDays(d);
        if (s.EndsWith("h")  && double.TryParse(s[..^1], ns, ic, out var h))   return TimeSpan.FromHours(h);
        if (s.EndsWith("m")  && double.TryParse(s[..^1], ns, ic, out var m))   return TimeSpan.FromMinutes(m);
        if (s.EndsWith("s")  && double.TryParse(s[..^1], ns, ic, out var sec)) return TimeSpan.FromSeconds(sec);
        if (double.TryParse(s, ns, ic, out var plain))                          return TimeSpan.FromSeconds(plain);
        return TimeSpan.Zero;
    }

    static string? GetArgValue(string[] args, string flag)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == flag) return args[i + 1];
        }
        return null;
    }

    /// <summary>
    /// Busybox-style: detect command from exe name (symlink-friendly).
    /// e.g. "a11y" -> "a11y", "inspect" -> "inspect", "wka11y" -> "a11y"
    /// </summary>
    static string? DetectCommandFromExeName(string exeBaseName)
    {
        // Skip if running as wkappbot itself
        if (exeBaseName == "wkappbot") return null;

        // Exact match first (symlink named exactly as command)
        string[] knownCommands = {
            "a11y", "inspect", "ocr", "logcat", "grep", "grap", "capture", "scan",
            "windows", "snapshot", "readiness", "ask"
        };
        foreach (var cmd in knownCommands)
        {
            if (exeBaseName == cmd) return cmd;
        }

        // Fuzzy: exe name contains command (e.g. "wka11y" contains "a11y")
        foreach (var cmd in knownCommands)
        {
            if (exeBaseName.Contains(cmd)) return cmd;
        }

        return null;
    }

    // Busybox aliases to auto-create as symlinks next to wkappbot.exe
    static readonly string[] BusyboxAliases = { "a11y", "wka11y", "grep", "grap", "wkedit", "taskkill" };

    static void PrintGrapHelp(string alias)
    {
        var isgrap = alias == "grap";
        Console.WriteLine($"""
            {alias} -- WKAppBot log search ({(isgrap ? "grab accessible pattern, official WKAppBot name" : "legacy grep alias")})
            Internally: {alias} <pattern> [files...] -> logcat <files> <pattern>

            Usage:
              {alias} <pattern> [files...] [options]

            Arguments:
              <pattern>     Regex pattern to search (case-insensitive by default)
              [files...]    File glob(s) to search, ';' OR  e.g. "*.log;*.eye.*"
                            Default: *.log in current directory

            Options:
              -r, --recursive       Recursive (unlimited depth)
              -r=N                  Recursive up to depth N
              --basedir <dir>       Search root directory (default: CWD)
              --hq                  Include wkappbot.hq + openclaw log dirs
              --past <duration>     Scan files modified within duration (e.g. 1h, 30m, 2d)
                                    Without --follow: scan and exit (grep-style, one-shot)
              -f, --follow          Follow new log entries after --past scan (tail -f style)
              --timeout <duration>  Watch mode: follow for duration then exit
                                    e.g. --timeout 30s, --timeout 5m  (implies --follow)

              -i, --case-sensitive  Case-sensitive match (default: insensitive)
              -v, --invert-match    Invert match
              -l, --files-with-matches  Print filenames only
              -c, --count           Count matches per file
              -m N, --max-count N   Stop after N matches per file
              -A N                  N lines after match
              -B N                  N lines before match
              -C N                  N lines context (before + after)

            Examples:
              {alias} error                          # search *.txt in CWD
              {alias} error *.log                    # search *.log files
              {alias} "NullRef" *.log -C3            # 3 lines context
              {alias} error *.log --past 1h          # last 1h, then exit
              {alias} error *.log --past 1h -f       # last 1h, then follow
              {alias} error *.log --timeout 30s      # watch for 30 seconds
              {alias} error --hq -r                  # recursive in HQ logs
            """);
    }

    /// <summary>
    /// Translates grep-style args to logcat-style args for the `grap` busybox alias.
    /// grep:   grap [opts] pattern [file/dir...]
    /// logcat: logcat fileFilter pattern [opts]
    ///
    /// Transformation: swap positional order -- pattern (first) ↔ fileFilter (rest joined with ';')
    /// Flags (-v, -l, -c, -r, -A, -B, -C, -m, -i) pass through unchanged (logcat already supports them).
    /// </summary>
    static string[] GrapArgsToLogcat(string[] args)
    {
        var flags = new List<string>();
        var positional = new List<string>();

        // Separate flags from positionals (flags = anything starting with '-', including their value args)
        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a.StartsWith('-'))
            {
                flags.Add(a);
                // Flags that consume the next arg as value
                if (a is "-A" or "-B" or "-C" or "--context" or "--after-context" or "--before-context"
                        or "-m" or "--max-count" or "--basedir" or "--past" or "--timeout"
                    && i + 1 < args.Length)
                    flags.Add(args[++i]);
            }
            else positional.Add(a);
        }

        // grep positionals: [pattern, file1, file2, ...]
        // logcat positionals: [fileFilter, pattern]
        if (positional.Count == 0)
            return flags.ToArray(); // no positionals -- pass through as-is

        // Auto-detect if first arg looks like a file glob (contains * ? or looks like *.ext)
        // and swap to [fileFilter, pattern] order. Handles common mistake: grap *.log error
        static bool IsGlobLike(string s) =>
            s.Contains('*') || s.Contains('?') || s.Contains('/') || s.Contains('\\') ||
            (s.StartsWith("*.") || System.IO.Path.HasExtension(s));

        string pattern, fileFilter;
        if (positional.Count == 1 && IsGlobLike(positional[0]))
        {
            // grap *.log -> fileFilter=*.log, pattern=* (show all)
            fileFilter = positional[0];
            pattern    = "";
        }
        else if (positional.Count >= 2 && IsGlobLike(positional[0]) && !IsGlobLike(positional[1]))
        {
            // grap *.log error -> auto-swap: fileFilter=*.log, pattern=error
            fileFilter = string.Join(";", positional.Take(positional.Count - 1));
            pattern    = positional[positional.Count - 1];
        }
        else
        {
            // Normal grep order: pattern first, files second
            pattern    = positional[0];
            fileFilter = positional.Count > 1
                ? string.Join(";", positional.Skip(1))  // multiple files -> ';' OR glob
                : "*.log";                               // no file arg -> default logcat filter
        }

        // If fileFilter contains path separators, extract --basedir so logcat searches the right dir.
        bool hasBasedir = flags.Any(f => f == "--basedir");
        if (!hasBasedir && (fileFilter.Contains('/') || fileFilter.Contains('\\')))
        {
            // Case 1: directory path (e.g. /tmp/testdir/) -> use as basedir, filter=*
            if (System.IO.Directory.Exists(fileFilter.TrimEnd('/', '\\')))
            {
                flags.Add("--basedir");
                flags.Add(fileFilter.TrimEnd('/', '\\'));
                fileFilter = "*";
            }
            else
            {
                // Case 2: one or more absolute file paths (shell-expanded glob or single path)
                // Split on ';', extract common directory, keep filenames as filter
                var parts    = fileFilter.Split(';', StringSplitOptions.RemoveEmptyEntries);
                var dirs2    = parts.Select(p => System.IO.Path.GetDirectoryName(p) ?? "").Distinct().ToArray();
                var names    = parts.Select(p => System.IO.Path.GetFileName(p)).ToArray();
                var commonDir = dirs2.Length == 1 ? dirs2[0] : "";
                if (!string.IsNullOrEmpty(commonDir) && System.IO.Directory.Exists(commonDir))
                {
                    fileFilter = string.Join(";", names);
                    flags.Add("--basedir");
                    flags.Add(commonDir);
                }
            }
        }

        // grap/grep: GrapMode flag (set in Program.cs) triggers autoOneShot scan-and-exit in logcat.
        // No --past needed: autoOneShot scans all files directly without a time cutoff.
        // Only add --past if user explicitly requested it or wants follow mode (already in flags above).

        // When --basedir already set and fileFilter has path separators, strip to filename only
        // (logcat will search in basedir; keeping full path confuses the grep-style parser)
        if (hasBasedir && !string.IsNullOrEmpty(fileFilter)
            && (fileFilter.Contains('/') || fileFilter.Contains('\\')))
        {
            fileFilter = string.Join(";", fileFilter.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => (p.Contains('/') || p.Contains('\\'))
                    ? System.IO.Path.GetFileName(p) : p));
        }

        // Translate grep BRE special sequences to .NET regex equivalents
        // \| (BRE OR) -> | ; \( \) (BRE groups) -> ( )
        if (!string.IsNullOrEmpty(pattern))
            pattern = pattern.Replace("\\|", "|").Replace("\\(", "(").Replace("\\)", ")");

        // Return in grep-style order: [pattern, fileFilter, ...flags]
        // logcat parser expects positional[0] = content regex, positional[1..] = file globs
        var result = new List<string>();
        if (!string.IsNullOrEmpty(pattern)) result.Add(pattern);
        if (!string.IsNullOrEmpty(fileFilter)) result.Add(fileFilter);
        result.AddRange(flags);
        return result.ToArray();
    }


    /// <summary>
    /// Ensure busybox-style symlinks exist next to wkappbot.exe.
    /// Called once at startup -- silently skips on permission error or if already present.
    ///
    /// Fallback chain:
    ///   1. Symlink (preferred) -- follows filename across hot-swap
    ///   2. Hardlink (fallback when no permission) -- stale after hot-swap; re-upgraded next run
    ///
    /// Stale hardlink detection: after hot-swap wkappbot.exe is a new file (different size/time)
    /// while the hardlink still points to the old inode. We detect this by comparing file sizes --
    /// if they diverge, delete the hardlink and retry as symlink.
    /// </summary>
    internal static void EnsureBusyboxAliases()
    {
        try
        {
            var exePath = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exePath)) return;

            // Only auto-create when running as wkappbot itself, not as an alias
            // Accept both "wkappbot.exe" and "wkappbot-core.exe" (publish artifact name)
            var exeBase = Path.GetFileNameWithoutExtension(exePath);
            if (!exeBase.StartsWith("wkappbot", StringComparison.OrdinalIgnoreCase))
                return;

            var dir = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(dir)) return;

            // Symlink target is always wkappbot.exe (the wrapper), regardless of which exe is running
            var targetName = "wkappbot.exe";
            var targetPath = Path.Combine(dir, targetName);
            if (!File.Exists(targetPath)) return; // wrapper not present yet -- skip

            long targetSize = new FileInfo(targetPath).Length;

            foreach (var alias in BusyboxAliases)
            {
                var linkPath = Path.Combine(dir, alias + ".exe");

                // Check for stale hardlink: no symlink target + size mismatch -> delete and recreate
                if (File.Exists(linkPath))
                {
                    var linkTarget = File.ResolveLinkTarget(linkPath, returnFinalTarget: false)?.ToString();
                    if (linkTarget != null)
                    {
                        // Symlink exists -- verify it points to the correct target
                        var linkTargetName = Path.GetFileName(linkTarget);
                        if (string.Equals(linkTargetName, targetName, StringComparison.OrdinalIgnoreCase))
                            continue; // correct symlink, keep it
                        // Wrong target (e.g. wkappbot-core.exe instead of wkappbot.exe) -- recreate
                        try { File.Delete(linkPath); } catch { continue; }
                    }
                    else
                    {
                        // It's a hardlink (or copy). Check if stale after hot-swap.
                        long linkSize = new FileInfo(linkPath).Length;
                        if (linkSize != targetSize)
                        {
                            // Stale hardlink -- delete and fall through to recreate
                            try { File.Delete(linkPath); } catch { continue; }
                        }
                        else continue; // hardlink still matches, keep it
                    }
                }
                // Also skip dangling symlinks (File.Exists=false but path exists)
                else
                {
                    try { if (File.GetAttributes(linkPath) != 0) continue; } catch { }
                }

                // Try symlink first, fall back to hardlink
                try
                {
                    File.CreateSymbolicLink(linkPath, targetName);
                }
                catch (UnauthorizedAccessException)
                {
                    // No symlink permission -- create hardlink as temporary fallback.
                    // Will be upgraded to symlink on next run if permission is granted.
                    try { CreateHardLinkNative(linkPath, targetPath); } catch { }
                }
                catch { }
            }
        }
        catch { }
    }

    /// <summary>
    /// enc-test -- outputs known Korean text + hex bytes for encoding diagnosis.
    /// Core has activeCodePage UTF-8, so pipe bytes are always UTF-8.
    /// Launcher must transcode to terminal CP if not UTF-8 (e.g. CP949 CMD).
    /// </summary>
    static int EncTestCommand()
    {
        var testStr = "가나다라마바사아자차카타파하";
        var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(testStr);
        var cp949Bytes = System.Text.Encoding.GetEncoding(949).GetBytes(testStr);

        Console.Error.WriteLine($"[ENC-TEST] text={testStr}");
        Console.Error.WriteLine($"[ENC-TEST] UTF-8 hex: {string.Join(" ", utf8Bytes.Select(b => b.ToString("X2")))}");
        Console.Error.WriteLine($"[ENC-TEST] CP949 hex: {string.Join(" ", cp949Bytes.Select(b => b.ToString("X2")))}");
        Console.Error.WriteLine($"[ENC-TEST] Console.OutputEncoding={Console.OutputEncoding.CodePage} GetConsoleOutputCP={EncTestGetConsoleOutputCP()}");
        Console.WriteLine("[ENC-TEST] done");
        return 0;
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false, EntryPoint = "GetConsoleOutputCP")]
    static extern uint EncTestGetConsoleOutputCP();
}
