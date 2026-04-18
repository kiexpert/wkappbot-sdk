using System.Diagnostics;
using System.Text;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Live-swap: if the user invoked us via a stale alias (typically a
    /// hardlink whose inode points at an older wkappbot.exe binary because a
    /// rebuild rotated the original to wkappbot.old-*.exe), respawn the
    /// CURRENT wkappbot.exe in-place with inherited stdio, wait for it, and
    /// TerminateSelf with its exit code. The user sees a single session --
    /// only the PID changes, and a one-line "[SWAP]" banner is logged to
    /// stderr. Pipes / ConPTY aren't involved at Launcher level, so stdio
    /// inheritance is enough for continuity. The canonical wkappbot.exe
    /// never enters this path (caller guards exeBase != "wkappbot"), so
    /// there's no respawn loop.
    /// </summary>
    static void MaybeLiveSwap(string[] args, string myPath)
    {
        try
        {
            var dir = System.IO.Path.GetDirectoryName(myPath);
            if (string.IsNullOrEmpty(dir)) return;
            var target = System.IO.Path.Combine(dir, "wkappbot.exe");
            if (!System.IO.File.Exists(target)) return;

            // Heuristic: if mtimes match (within 2s tolerance) we assume this
            // alias is a genuine hardlink to the current wkappbot.exe and
            // skip. If target is older than us, also skip (we're the newer
            // one somehow). Only swap when target is meaningfully newer.
            var myTime = System.IO.File.GetLastWriteTimeUtc(myPath);
            var targetTime = System.IO.File.GetLastWriteTimeUtc(target);
            if (System.Math.Abs((targetTime - myTime).TotalMilliseconds) < 2000) return;
            if (targetTime <= myTime) return;

            Console.Error.WriteLine(
                $"[SWAP] stale {System.IO.Path.GetFileName(myPath)} ({myTime:HH:mm:ss}) -> wkappbot.exe ({targetTime:HH:mm:ss}) pid={Environment.ProcessId}");

            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = target,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = false,
                WorkingDirectory = Environment.CurrentDirectory,
            };
            foreach (var a in args) psi.ArgumentList.Add(a);
            using var proc = System.Diagnostics.Process.Start(psi);
            if (proc == null) return; // spawn failed -- fall through to normal path
            Console.Error.WriteLine($"[SWAP] new-pid={proc.Id} running");
            proc.WaitForExit();
            // Heal runs from AppBotExit (the centralized exit point) so it
            // fires on every exit path from here on, not just this one.
            _pendingHealLink  = myPath;
            _pendingHealTarget = target;
            AppBotExit(proc.ExitCode);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SWAP] failed: {ex.Message}");
        }
    }

    // Remembered by MaybeLiveSwap; consumed by AppBotExit.
    static string? _pendingHealLink;
    static string? _pendingHealTarget;

    /// <summary>
    /// Call from AppBotExit. If MaybeLiveSwap recorded a stale alias path,
    /// fire a detached Launcher self-dispatch (`wkappbot.exe --heal-link
    /// &lt;link&gt; &lt;target&gt;`) that polls the alias until our exe lock
    /// releases, then deletes + re-links. Keeping this in-binary avoids any
    /// PowerShell / cmd dependency and keeps the lock-wait logic in one
    /// language we already own.
    /// </summary>
    static void RunPendingHardlinkHealOnExit()
    {
        var linkPath   = _pendingHealLink;
        var targetPath = _pendingHealTarget;
        if (string.IsNullOrEmpty(linkPath) || string.IsNullOrEmpty(targetPath)) return;

        try
        {
            // Cheap is-it-still-stale check: matching mtimes (within 2s)
            // means someone else already healed it while we were running.
            var linkTime = System.IO.File.GetLastWriteTimeUtc(linkPath);
            var targetTime = System.IO.File.GetLastWriteTimeUtc(targetPath);
            if (System.Math.Abs((targetTime - linkTime).TotalMilliseconds) < 2000) return;

            Console.Error.WriteLine($"[SWAP] heal scheduled: {System.IO.Path.GetFileName(linkPath)} (polling ~1s for lock release)");
            try { Console.Out.Flush(); Console.Error.Flush(); } catch { }

            // Spawn the canonical wkappbot.exe with the --heal-link internal
            // flag. Redirect child stdio + close our ends so the user's outer
            // shell doesn't hold a pipe to the helper after we TerminateSelf.
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName  = targetPath,
                UseShellExecute = false,
                CreateNoWindow  = true,
                WindowStyle     = System.Diagnostics.ProcessWindowStyle.Hidden,
                RedirectStandardInput  = true,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
            };
            psi.ArgumentList.Add("--heal-link");
            psi.ArgumentList.Add(linkPath);
            psi.ArgumentList.Add(targetPath);
            var p = System.Diagnostics.Process.Start(psi);
            if (p != null)
            {
                try { p.StandardInput.Close();  } catch { }
                try { p.StandardOutput.Close(); } catch { }
                try { p.StandardError.Close();  } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SWAP] heal schedule failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Entry point for the `--heal-link &lt;link&gt; &lt;target&gt;` internal
    /// subcommand. Tries to delete the stale alias for up to ~1s (the
    /// previous Launcher's exe lock has to drop first), then recreates a
    /// fresh hardlink to the current wkappbot.exe. No console output -- this
    /// helper runs in the background after the user's session has already
    /// returned a prompt.
    /// </summary>
    static int HealLinkSelfDispatch(string linkPath, string targetPath)
    {
        try
        {
            // Already fresh (mtimes match): someone else healed it.
            if (System.IO.File.Exists(linkPath) && System.IO.File.Exists(targetPath))
            {
                var lt = System.IO.File.GetLastWriteTimeUtc(linkPath);
                var tt = System.IO.File.GetLastWriteTimeUtc(targetPath);
                if (System.Math.Abs((tt - lt).TotalMilliseconds) < 2000) return 0;
            }

            // Step 1: rename the stale alias. Windows allows MoveFile on a
            // running exe (loader holds the file by inode -- the directory
            // entry is renameable). DELETE would fail until the owning PID
            // terminates, so we don't try yet.
            // Rename target: ms precision + helper PID, with an "existence"
            // loop as final insurance so parallel heals (same ms, same PID
            // impossible, but keep belt+suspenders) never MoveFile onto an
            // existing file and fail.
            var stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");
            var pid = Environment.ProcessId;
            string renamedPath = linkPath + $".old-{stamp}-{pid}.exe";
            for (int seq = 2; System.IO.File.Exists(renamedPath) && seq < 100; seq++)
                renamedPath = linkPath + $".old-{stamp}-{pid}-{seq}.exe";
            try { System.IO.File.Move(linkPath, renamedPath); }
            catch { return 1; }

            // Step 2: fresh hardlink to current wkappbot.exe -- instant.
            // Next invocation of the alias is now on the new binary.
            try { CreateHardLink(linkPath, targetPath, IntPtr.Zero); }
            catch { return 2; }

            // Step 3: best-effort delete of the renamed corpse. The previous
            // Launcher is about to / has just TerminateSelf'd, so its exe
            // lock drops within one or two poll intervals. Max 10 x 100ms
            // = 1s total; on failure the orphan stays and a later invocation
            // can sweep it.
            for (int i = 0; i < 10 && System.IO.File.Exists(renamedPath); i++)
            {
                try { System.IO.File.Delete(renamedPath); }
                catch { /* still locked, retry */ }
                if (!System.IO.File.Exists(renamedPath)) break;
                System.Threading.Thread.Sleep(100);
            }
            return 0;
        }
        catch { return 3; }
    }

    /// <summary>
    /// Auto-create busybox symlinks (a11y.exe, wka11y.exe -> wkappbot.exe) if missing.
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
                // grap/grep/logcat -> Core directly (no Launcher relay needed, ~0.2s startup)
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
                    else continue; // symlink exists -> keep as-is
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
    /// Core's 27s SMB cleanup continues in the background -- Launcher is already gone by then.
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
    /// Prints wkappbot usage directly from the Launcher -- no Core spawn required.
    /// Kept in sync with UsageCommand.GetUsageText in WKAppBot.CLI.
    /// Called from no-args fast path (before Console.OutputEncoding setup) -> TerminateSelf is fast.
    /// </summary>
    static void PrintUsage()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        Console.Write($@"
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
               Arrays are for lookup; actions take the top target.
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
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye -- live overlay + Slack daemon (always on).
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
  chat [""question""] [-p] [--no-fallback]        (alias: wkchat.exe)
      Claude Code CLI passthrough. On rate-limit -> auto-fallback to ask triad.
      No args: exec claude interactively (inherits stdio).
      `chat cmd` enters a ConPTY cmd.exe with Enter-intercept: trailing ?/!
      or leading space routes to the fallback AI, wkappbot/a11y commands
      bypass cmd.exe's tokenizer. Typed input on edit keys (arrows/Tab/Home)
      passes straight through so cmd.exe's line editor keeps working.
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
    /// Prints grap/grep help directly from the Launcher -- no Core spawn required.
    /// Kept in sync with UsageCommand.PrintGrapHelp in WKAppBot.CLI.
    /// </summary>
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
/// Only use for restore patterns -- not for acquiring focus.
/// </summary>
static class FocusGuard
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
}

/// <summary>Dim stderr writer -- ANSI dim for console, passthrough for pipes.</summary>
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
