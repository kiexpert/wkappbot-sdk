using System.Runtime.InteropServices;

namespace WKAppBot.CLI;

// partial class: PrintUsage + Error + GetArgValue (shared utilities)
internal partial class Program
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    static void CreateHardLinkNative(string linkPath, string targetPath) =>
        CreateHardLink(linkPath, targetPath, IntPtr.Zero);
    /// <summary>Full help text — single source of truth for CLI help AND MCP tool description.</summary>
    internal static string GetUsageText()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        return $@"
WKAppBot{verStr} - Windows App Automation Test Framework
Building the Eyes of Claude to realize WilKim's vision of autonomous secretarial ops.
All AI agents welcome — Claude, GPT, Gemini, Copilot, and beyond.
Your testing, coding, and ideas are appreciated. Let's build together.

Usage:
  wkappbot <command> [options]

═══ Public Commands ═══════════════════════════════════════════

  a11y <action> <grap>[#uia-scope] [options]        (alias: a11y.exe / wka11y.exe)
      Universal accessibility interface — 20 standard actions for ANY window.
      3-tier fallback: UIA → Win32 → SendInput. Busybox: symlink `a11y.exe` works.

      Auto-pipeline per action: blocker dismiss → minimize restore → tab activate
        → zoom/magnifier → execute (3-tier) → result feedback (green/amber) → fade

      Window (7):  close  minimize  maximize  restore  focus  move  resize
      Element (13): read  find  highlight  invoke  click  toggle
                    expand  collapse  select  scroll  type  set-value  set-range
      Utility:     clipboard  clipboard-read  clipboard-write (text/files/mixed)

      Target:  --nth 3 | 3~ | ~3 | 2~4    --all    (default: first match)
      Options: --depth N  --force  --value N  --direction  --amount
      Grap:    ';' OR  #scope  Ex: ""*메모장*#*파일*"" → Notepad's File menu

      a11y find ""*app*"" --depth 5       # MUD: look (Win32 + UIA children)
      a11y highlight ""*app*#*button*""   # visualize target with zoom overlay
      a11y invoke ""*app*#*button*""      # click (UIA Invoke → BM_CLICK → SendInput)
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
      Sort wav/ root: voice→_unknown, noise→Recycle Bin.
  whisper index [--in <slices-dir>] [--out <db-dir>] [--move] [--dry-run]
      Extract first-syllable soundCode → phoneme_db/<octal>/word.mp3.
      Files with <9 voiced frames (~90ms) → Recycle Bin automatically.
  mcp
      Start MCP stdio server (wkappbot_cli tool for JSON-RPC clients).
      Add to .mcp.json: {{ ""wkappbot"": {{ ""command"": ""wkappbot"", ""args"": [""mcp""] }} }}
  web <subcommand> [options]
      Chrome DevTools Protocol web automation (open/click/type/eval).
      Type 'wkappbot web help' for all subcommands.
  web fetch <url> [--max-chars N]
      HTTP GET — returns response body (no browser needed).
  web search <query> [--limit N]
      Google search via Chrome CDP (no API key required).
  web read <url> [--max-chars N]
      Navigate URL + extract rendered text content.
  file read <path> [--offset N] [--limit N]
      Read any file with line numbers (encoding-aware: --encoding 949/utf-16).
  file grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
      Regex search across files. -i=case-insensitive, -C=context lines.
  file glob <pattern> [--path <dir>]
      Find files by ** glob pattern. ⚠ ALWAYS use **/ prefix (e.g. **/*.cs).
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye — live overlay + Slack daemon (always on).
      ctx=N% in tick output, auto-deletes stale idle messages on restart.
  newchat ""prompt"" [--file prompt.txt]
      Open new Claude Desktop chat + submit prompt (all focusless UIA).
      Use for session handoff when context reaches 90%+.
  slack send|reply|upload|screenshot|listen|catch-up
      Slack messaging (Socket Mode, always-on prompt forwarding).

═══ Detail Commands ═══════════════════════════════════════════

Inspection:
  windows [filter] [--uia] [--deep] [--process <name>] [--limit N]
      List visible windows in Z-order. --uia: also search UIA elements.
  inspect <grap>[#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]
      Dump UIA tree of a window. # narrows to UIA element by name.
      --filter: search A11Y elements. Ex: *영웅문*#*실시간계좌*
  win-click <window-title> <x> <y> [--uia]
      Click a coordinate inside a window + detect UIA element.
  focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
      Inspect the currently focused window (countdown + dump).
  watch [--duration N] [--live] [--win32] [--interval N]
      Real-time element tracking under mouse cursor.

  tab-select <grap>[#<uia-scope>] --aid <id> [--list|--select <text>|--index N]
      Focusless tab switching via UIA SelectionItem pattern.
      # scopes to specific form. Ex: *영웅문*#*실시간계좌* --aid 1000

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
      # narrows UIA root. Ex: *영웅문*#*실시간계좌* --invoke 예수금
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
  knowhow write|read|web|web-read
      Record/read per-control automation notes.
  schedule add|list|remove|clear
      Manage scheduled prompts for auto-recovery.
  logcat <fileFilter> <messageFilter> [--basedir <dir>] [-r[=N]] [--hq]
      Stream logs in real-time. Default: CWD only. -r unlimited, -r=3 depth 3. --hq adds HQ+openclaw.
      File filter supports grap patterns: wildcards, regex: prefix, ';' OR.
  ask gpt|gemini|claude ""question"" [file.png] [--slack] [--new-tab]
      Ask AI via CDP (focusless). Auto-closes blank tabs, validates URL.
  ask triad ""question""
      Parallel GPT + Gemini + Claude — three answers at once.
  agent gemini|gpt|claude|triad ""task"" [--max-steps N] [--fresh]
      Autonomous sub-agent loop with filesystem + web tools.
  agent checkpoint [--label ""text""]
      Save mid-session snapshot of all tracked files (before compile / risky change).
      Tracked: every a11y file-write is registered in agent-session.json (persistent).
  agent dump-patch [--out file.patch] [--apply]
      git diff HEAD → unified patch + per-checkpoint diffs saved to repo root.
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
  suggest ""text""
      Send suggestion/feature request to Slack (auto-tags CWD workspace).
      ⚠ Write in ENGLISH — Korean = 2-3x token cost. Short & precise wins.
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
    /// e.g. "a11y" → "a11y", "inspect" → "inspect", "wka11y" → "a11y"
    /// </summary>
    static string? DetectCommandFromExeName(string exeBaseName)
    {
        // Skip if running as wkappbot itself
        if (exeBaseName == "wkappbot") return null;

        // Exact match first (symlink named exactly as command)
        string[] knownCommands = {
            "a11y", "inspect", "ocr", "logcat", "grep", "capture", "scan",
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
    static readonly string[] BusyboxAliases = { "a11y", "wka11y", "grep" };

    /// <summary>
    /// Ensure busybox-style symlinks exist next to wkappbot.exe.
    /// Called once at startup — silently skips on permission error or if already present.
    ///
    /// Fallback chain:
    ///   1. Symlink (preferred) — follows filename across hot-swap
    ///   2. Hardlink (fallback when no permission) — stale after hot-swap; re-upgraded next run
    ///
    /// Stale hardlink detection: after hot-swap wkappbot.exe is a new file (different size/time)
    /// while the hardlink still points to the old inode. We detect this by comparing file sizes —
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
            if (!File.Exists(targetPath)) return; // wrapper not present yet — skip

            long targetSize = new FileInfo(targetPath).Length;

            foreach (var alias in BusyboxAliases)
            {
                var linkPath = Path.Combine(dir, alias + ".exe");

                // Check for stale hardlink: no symlink target + size mismatch → delete and recreate
                if (File.Exists(linkPath))
                {
                    bool isSymlink = File.ResolveLinkTarget(linkPath, returnFinalTarget: false) != null;
                    if (!isSymlink)
                    {
                        // It's a hardlink (or copy). Check if stale after hot-swap.
                        long linkSize = new FileInfo(linkPath).Length;
                        if (linkSize != targetSize)
                        {
                            // Stale hardlink — delete and fall through to recreate
                            try { File.Delete(linkPath); } catch { continue; }
                        }
                        else continue; // hardlink still matches, keep it
                    }
                    else continue; // symlink exists, good
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
                    // No symlink permission — create hardlink as temporary fallback.
                    // Will be upgraded to symlink on next run if permission is granted.
                    try { CreateHardLinkNative(linkPath, targetPath); } catch { }
                }
                catch { }
            }
        }
        catch { }
    }
}
