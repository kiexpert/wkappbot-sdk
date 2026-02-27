namespace WKAppBot.CLI;

// partial class: PrintUsage + Error + GetArgValue (shared utilities)
internal partial class Program
{
    static int PrintUsage()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        Console.WriteLine($@"
WKAppBot{verStr} - Windows App Automation Test Framework
Building the Eyes of Claude to realize WilKim's vision of autonomous secretarial ops.
All AI agents welcome — Claude, GPT, Gemini, Copilot, and beyond.
Your testing, coding, and ideas are appreciated. Let's build together.

Usage:
  wkappbot <command> [options]

Scenario Commands:
  run <scenario.yaml> [-v] [--no-watch] [--watch-interval N]
      Run a test scenario with passive [WATCH] background tracker.
  validate <scenario.yaml>
      Validate a YAML scenario file (syntax + structure check).

Search Commands:
  find <keyword> [--deep] [--limit N] [--process <name>] [--class <name>]
      Unified search: window titles + UIA accessibility elements.
      --deep: Thorough UIA tree search (depth 12, slower but finds more).

Inspection Commands:
  inspect <window-title> [--depth N] [--win32] [--filter <pattern>]
      Dump UI Automation tree of a window (by title substring match).
      --filter: Search A11Y tree for matching elements (supports A11Y: prefix).
  windows [filter] [--uia] [--deep] [--process <name>] [--class <name>] [--limit N]
      List visible windows in Z-order (front to back). ★=foreground.
      --uia: Unified search — also find UIA elements inside windows.
      --deep: Include MDI child windows. --limit N: Stop after N matches.
  win-click <window-title> <x> <y> [--uia]
      Click a coordinate inside a window + detect UIA element at point.
      --uia: Show UIA element details with zoom overlay.
  focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
      Inspect the currently focused window (countdown + UIA/Win32 dump).
  watch [--duration N] [--live] [--win32] [--interval N] [--save file]
      Real-time element tracking under mouse cursor (Ctrl+C to stop).
  capture <window-title> [-o output.png] [--form <id>]
      Capture a screenshot of a window or specific MDI child form.

App Scanning Commands:
  scan <window-title> [--save] [--ocr] [--detail] [--depth N]
      Scan window structure: auto-classify zones, MDI forms, Experience DB.
      --save: Save profile + experience data. --ocr: OCR learning.
      --detail: Per-control screenshots + text history (opt-in).
  form-dump <window-title> <form-id> [--depth N]
      Recursively dump all controls in an MDI child form.

Automation Commands:
  click <window-title> <form-id> [button-text] [--combo N INDEX] [options]
      Find an MDI form and click a button (with MFC combo selection).
  do <window-title> <form-id> <button-text> [--delay N] [--confirm] [--no-dismiss]
      Full automation: combos + button click + dialog handling + notice dismiss.
      Integrates Experience DB for smart click strategy ordering.
  dismiss <window-title> [--force] [keyword1] [keyword2] ...
      Auto-dismiss MDI child notice/popup windows matching keywords.
      Default keywords: 怨듭?, ?몄궗, ?덈궡, ?뚮┝, POP-UP.
      Reads content via OCR and classifies importance (critical = no close).

Dialog & Toolbar Commands:
  dialog-click <dialog-title> [button-index]
      Click a button in a top-level dialog using physical mouse click.
  toolbar-ocr <window-title> [--click ""text""] [--save]
      OCR-scan MFC toolbar panes and show recognized button text.
      --click: Click the region matching text. --save: Save toolbar screenshots.
  titlebar <window-title> <form-id> [button-index] [--ocr] [--save]
      Access custom title bar buttons (ETK_CHILDFRAME, focusless PostMessage).
      --ocr: Show OCR labels for button identification.

UIA Testing Commands:
  uia-test <window-title> [--invoke <name>]
      Systematic 7-phase MFC UIA pattern test with zoom overlay.
      --invoke: Quick invoke a single button by name (focusless).

Chart Analysis Commands:
  chart-analyze <window-title|image.png> [--form <id>] [--candles N] [options]
      Extract OHLC + volume candlestick data from chart screenshots.
      3 strategies: body-first, column-scan, hts_style (auto-selected).
      --tooltip: Y-axis recalibration via mouse hover (Phase B).
      --debug: Save debug overlay image with detection visualization.
  tooltip-probe <process-name> [--capture]
      Enumerate tooltip windows for a process (diagnostic tool).

WebBot Commands (Chrome DevTools Protocol):
  web open [url] [--port N] [--headless] [--no-launch]
      Launch Chrome and connect via CDP. Auto-finds chrome.exe.
  web navigate <url>           Navigate to URL.
  web click <css-selector>     Click an element.
  web type <selector> <text>   Type text into an input.
  web eval <expression>        Evaluate JavaScript.
  web text <css-selector>      Get element text content.
  web screenshot [-o file]     Capture page screenshot.
  web status [--port N]        Check Chrome CDP status and list tabs.
  web run <steps-file.txt>     Run batch of web commands from file.
  web help                     Show all web subcommands.

HTS Stress Test Commands:
  hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only] [options]
      HTS MDI stress test with memory tracking (3 patterns).
      --process <name>: Target process (default: HTSRun).

Run Options:
  --no-watch          Disable passive background element watcher
  --watch-interval N  Watcher polling interval in ms (default: 200)

Focus Options:
  --title <text>  Find window by title (skip countdown)
  --delay N       Seconds to wait before capturing focus (default: 3)
  --depth N       UIA tree depth (default: 6)
  --win32         Show Win32 child windows list
  -b, --buttons   Show all clickable buttons with AutomationId

Watch Options:
  --duration N    Stop after N seconds (default: unlimited)
  --live          Single-line overwrite mode (instead of scroll history)
  --win32         Also show Win32 hwnd/class under cursor
  --interval N    Polling interval in ms (default: 150)
  --save <file>   Save log to specific file

AppBotEye + Slack Commands:
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye (GlobalMode) ??live text overlay on Claude Desktop.
      Slack + Prompt forwarding + keyword monitoring: ALWAYS ON.
  eye tick
      Run one immediate info-acquire tick and print Kro card snapshot.
  slack listen [--bg] [--ai] [--claude|--webbot] [--name N]
      Slack Socket Mode: listen for @mentions and forward to Claude.
      Prompt + keyword monitoring always enabled.
  slack send ""message""             Send message to configured channel.
  slack reply ""text""               Reply to latest thread.
  slack upload <file>              Upload file to Slack.
  slack screenshot [title]         Capture + upload screenshot.
  slack catch-up [--limit N]       Fetch missed messages (auto-forward).

Utility Commands:
  ocr <window-title|image.png> [--save] [-o file]
      Extract text from window/image using Windows.Media.Ocr.
  knowhow write|read|web|web-read  Record/read per-control automation notes.
  schedule add|list|remove|clear   Manage scheduled prompts for auto-recovery.
  snapshot <window-title> [--tag N] [--depth N] [--cid N]
      One-shot diagnostic capture: UIA tree + screenshot + OCR (+experience blend).
  screen off [--no-check]          Turn off monitor immediately; optional skip health check.
  win-move <window-title> [--right-top] [--x N --y N] [--margin N]
      Move a window (default: right-top on the right-most monitor).
  logcat <fileFilter> <messageFilter>
      Stream changed logs until Ctrl+C (fileFilter supports ';' wildcards).
      Example: wkappbot logcat ""*.txt;*.jsonl"" ""A11Y|ACT|FALLBACK|EYE_PLAN""
  input <window-title> <text>      Type text into a window.
  kro-trial-YYYYMMDD [args...]     Special: run real AppBot input trial (default: 투혼 1101 005930).

COM Commands (session per current folder):
  com ls                           List available COM profiles/adapters.
  com use <name>                   Select profile for this folder (.wkcom/session.json).
  com current                      Show current profile.
  com methods                      Show methods for current profile.
  com call <method> [params...]    Call method through selected adapter.

Telegram Commands:
  telegram send ""text"" [--window WkQuant] [--no-fallback-enter]
                                   Accessibility-first send, Enter fallback.

General Options:
  -v, --verbose   Verbose output
  --report <dir>  Generate HTML report in directory
  -h, --help      Show this help message

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
");
        return 0;
    }

    /// <summary>
    /// Lightweight tick emitter — advertise work being done on the card.
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
            Console.WriteLine("  e.g. wkappbot tick build \"CardCache 시스템 구현\"");
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
        Console.WriteLine($"[TICK] tag={tag} status={status}");
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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(msg);
        Console.ResetColor();
        return 1;
    }

    static string? GetArgValue(string[] args, string flag)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == flag) return args[i + 1];
        }
        return null;
    }
}

