using System.Diagnostics;
using System.Text;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Auto-create busybox symlinks (a11y.exe, wka11y.exe → wkappbot.exe) if missing.
    /// Symlink preferred; falls back to hardlink if no permission.
    /// Stale hardlinks (size mismatch after hot-swap) are deleted and recreated.
    /// </summary>
    static void EnsureBusyboxAliases()
    {
        try
        {
            var exePath = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exePath)) return;
            var dir = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(dir)) return;

            var launcherName = Path.GetFileName(exePath); // wkappbot.exe
            var coreName     = Path.GetFileNameWithoutExtension(exePath) + "-core.exe"; // wkappbot-core.exe
            var corePath     = Path.Combine(dir, coreName);
            long launcherSize = new FileInfo(exePath).Length;

            var aliasNames = BusyboxAliases.Select(x => x.name).Distinct();
            foreach (var alias in aliasNames)
            {
                var linkPath   = Path.Combine(dir, alias + ".exe");
                // grap/grep/logcat → Core directly (no Launcher relay needed, ~0.2s startup)
                var targetName = CoreDirectAliases.Contains(alias) && File.Exists(corePath)
                    ? coreName : launcherName;
                var targetSize = targetName == coreName
                    ? new FileInfo(corePath).Length : launcherSize;

                if (File.Exists(linkPath))
                {
                    bool isSymlink = File.ResolveLinkTarget(linkPath, returnFinalTarget: false) != null;
                    if (!isSymlink)
                    {
                        // Stale hardlink check
                        if (new FileInfo(linkPath).Length != targetSize)
                            try { File.Delete(linkPath); } catch { continue; }
                        else continue;
                    }
                    else continue; // symlink exists → keep as-is
                }
                else
                {
                    try { if (File.GetAttributes(linkPath) != 0) continue; } catch { }
                }

                try
                {
                    File.CreateSymbolicLink(linkPath, targetName);
                }
                catch (UnauthorizedAccessException)
                {
                    var targetFull = Path.Combine(dir, targetName);
                    try { CreateHardLink(linkPath, targetFull, IntPtr.Zero); } catch { }
                }
                catch { }
            }
        }
        catch { }
    }

    /// <summary>
    /// grap/grep one-shot: named pipe relay.
    /// Creates a named pipe server, spawns Core with WKAPPBOT_NAMED_PIPE env var (no stream redirect),
    /// reads output until Core closes the pipe (before TerminateProcess), outputs to Console, exits.
    /// Core's 27s SMB cleanup continues in the background — Launcher is already gone by then.
    /// </summary>
    static int RunGrapWithNamedPipe(string[] args)
    {
        var core = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? ".", "wkappbot-core.exe");
        if (!File.Exists(core)) { Console.Error.WriteLine($"[ERROR] wkappbot-core.exe not found at: {core}"); return 1; }

        // DIAGNOSTIC: start Core with NO pipe, just env var, and see if it starts
        using var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = core,
                UseShellExecute = false,
            }
        };
        // Minimal env var to trigger startup log
        proc.StartInfo.EnvironmentVariables["WKAPPBOT_NAMED_PIPE"] = "test-no-pipe";
        foreach (var a in args)
            proc.StartInfo.ArgumentList.Add(a);

        Prof("RunGrapWithNamedPipe: proc.Start()");
        proc.Start();
        Prof($"RunGrapWithNamedPipe: waiting pid={proc.Id}");

        // Just wait 5s and see if Core reaches Main()
        System.Threading.Thread.Sleep(5000);
        Prof("RunGrapWithNamedPipe: done waiting");
        TerminateSelf(0);
        return 0; // unreachable
    }

    /// <summary>
    /// Prints wkappbot usage directly from the Launcher — no Core spawn required.
    /// Kept in sync with UsageCommand.GetUsageText in WKAppBot.CLI.
    /// Called from no-args fast path (before Console.OutputEncoding setup) → TerminateSelf is fast.
    /// </summary>
    static void PrintUsage()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        Console.Write($@"
WKAppBot{verStr} - Windows App Automation Test Framework
Give AI eyes and hands — operate any app, help any human. No API, no rewrite needed.
All AI agents welcome — Claude, GPT, Gemini, Copilot, and beyond.
Your testing, coding, and ideas are appreciated. Let's build together.

Usage:
  wkappbot <command> [options]

=== Public Commands ===================================================

  a11y <action> <grap>[#uia-scope] [options]        (alias: a11y.exe / wka11y.exe)
      Universal accessibility interface — 20 standard actions for ANY window.
      3-tier fallback: UIA → Win32 → SendInput. Busybox: symlink `a11y.exe` works.

      Auto-pipeline per action: blocker dismiss → minimize restore → tab activate
        → zoom/magnifier → execute (3-tier) → result feedback (green/amber) → fade

      Window (7):  close  minimize  maximize  restore  focus  move  resize
      Element (13): read  find  highlight  invoke  click  toggle
                    expand  collapse  select  scroll  type  set-value  set-range
      Utility:     clipboard  clipboard-read  clipboard-write (text/files/mixed)

      Target:  --nth 3 | 3~ | ~3 | 2~4 | 1,3~    --all    (default: first match)
      Options: --depth N  --force  --value N  --direction  --amount
      Grap:    AI target language for windows + a11y nodes.
               Human sees windows; AI points with grap.
               First exact match stops interactive actions fast.
               Arrays are for lookup; actions take the top target.
               ';' = OR   # = scope drill-down
               Ex: ""*Notepad*#*File*"" → Notepad's File menu

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
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye — live overlay + Slack daemon (always on).
      ctx=N% in tick output, auto-deletes stale idle messages on restart.
  newchat ""prompt"" [--file prompt.txt]
      Open new Claude Desktop chat + submit prompt (all focusless UIA).
      Use for session handoff when context reaches 90%+.
  slack send|reply|upload|screenshot|listen|catch-up
      Slack messaging (Socket Mode, always-on prompt forwarding).
  mcp
      Start MCP stdio server (wkappbot_cli tool for JSON-RPC clients).
  web fetch|search|read|html
      Chrome DevTools Protocol web automation.
  file read|grep|glob
      File reading, search, and pattern matching.
  ask gpt|gemini|claude|triad ""question"" [file.png]
      Ask AI via CDP (focusless). ask triad = parallel 3-way.
  logcat [regex] [files] [--past N] [-f] [--timeout N] [--hq]
      Stream/search logs. grep/grap = aliases with grep-compat arg order.

General Options:
  -v, --verbose         Verbose output
  --timeout <duration>  Hard kill after N time (exit 124). e.g. 30, 2m, 500ms
  -h, --help            Show this help message

Data Directory:
  Runtime data (profiles, logs, handlers, output) stored in:
  {{exe_dir}}/wkappbot.hq/

Run 'wkappbot --help' for full command reference.
");
    }

    /// <summary>
    /// Prints grap/grep help directly from the Launcher — no Core spawn required.
    /// Kept in sync with UsageCommand.PrintGrapHelp in WKAppBot.CLI.
    /// </summary>
    static void PrintGrapHelp(string alias)
    {
        var isgrap = alias == "grap";
        Console.WriteLine($"""
            {alias} — WKAppBot log search ({(isgrap ? "grab accessible pattern, official WKAppBot name" : "legacy grep alias")})
            Internally: {alias} <pattern> [files...] → logcat <files> <pattern>

            Usage:
              {alias} <pattern> [files...] [options]

            Arguments:
              <pattern>     Regex pattern to search (case-insensitive by default)
              [files...]    File glob(s) to search, ';' OR  e.g. "*.log;*.txt"
                            Default: *.txt in current directory

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
}

/// <summary>
/// Minimal focus-restore helper for Launcher (no Win32 project reference available).
/// Only use for restore patterns — not for acquiring focus.
/// </summary>
static class FocusGuard
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
}

/// <summary>Dim stderr writer — ANSI dim for console, passthrough for pipes.</summary>
sealed class DimStderrWriter : System.IO.TextWriter
{
    private readonly System.IO.TextWriter _inner;
    private readonly bool _useAnsi;
    public DimStderrWriter(System.IO.TextWriter inner)
    {
        _inner = inner;
        if (!Console.IsErrorRedirected)
        {
            // Enable VT processing on stderr for ANSI codes on Windows console
            try
            {
                var hErr = GetStdHandle(-12);
                if (GetConsoleMode(hErr, out uint mode))
                    _useAnsi = SetConsoleMode(hErr, mode | 0x0004); // ENABLE_VIRTUAL_TERMINAL_PROCESSING
            }
            catch { }
        }
    }
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)] static extern IntPtr GetStdHandle(int n);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")] static extern bool GetConsoleMode(IntPtr h, out uint m);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")] static extern bool SetConsoleMode(IntPtr h, uint m);
    public override System.Text.Encoding Encoding => _inner.Encoding;
    public override void Write(char value) => _inner.Write(value);
    public override void Write(string? value) { if (value == null) return; if (_useAnsi) { _inner.Write("\x1b[2m"); _inner.Write(value); _inner.Write("\x1b[0m"); } else _inner.Write(value); }
    public override void WriteLine(string? value) { if (_useAnsi) { _inner.Write("\x1b[2m"); _inner.WriteLine(value); _inner.Write("\x1b[0m"); } else _inner.WriteLine(value); }
    public override void WriteLine() => _inner.WriteLine();
    public override void Flush() => _inner.Flush();
}
