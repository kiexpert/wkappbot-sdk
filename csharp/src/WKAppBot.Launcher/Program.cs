using System.Diagnostics;
using System.Text;
using STARTUPINFOW = AppBotPipe.STARTUPINFOW;
using PROCESS_INFORMATION = AppBotPipe.PROCESS_INFORMATION;

namespace WKAppBot.Launcher;

/// <summary>
/// Ultra-thin relay launcher for WKAppBot.
/// Happy path: delegates to Eye via named pipe (~200ms total, no cold-start).
/// Fallback: spawns wkappbot-core.exe directly (~3s, rare — Eye not running).
///
/// Routing control flags (parsed here, stripped before forwarding to Eye/Core):
///   --only-eye       Eye pipe required — fail with exit 3 if Eye unavailable (no Core fallback)
///   --only-core      Skip Eye pipe — run Core directly regardless of Eye state
///   --timeout N      Kill Core after N seconds (Launcher-level watchdog, exit 2 on timeout)
///   --timeout-exit N Override timeout exit code (default: 2); applies to both normal and mcp mode
///
/// Fixed routing (flag-independent):
///   mcp  → Launcher owns stdio pipe permanently; Core runs behind proxy (restartable)
///   eye  → Core directly (eye IS the daemon)
/// </summary>
partial class Program
{
    // Busybox aliases: symlink names that map to an implicit first argument
    static readonly (string name, string cmd)[] BusyboxAliases =
    {
        ("a11y",   "a11y"),
        ("wka11y", "a11y"),
        ("inspect","inspect"),
        ("ocr",    "ocr"),
        ("logcat", "logcat"),
        ("grep",   "logcat"), // grep → logcat alias (busybox-style)
        ("grap",   "logcat"), // grap → logcat alias (GRab Accessible Pattern)
        ("scan",   "scan"),
        ("wkedit", "file"),   // wkedit.exe → file edit (busybox-style)
    };

    // Aliases that bypass Launcher and point directly to Core (no relay needed)
    static readonly HashSet<string> CoreDirectAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        "grap", "grep", "logcat",
    };

    // WKAPPBOT_PROFILE=1 support — static so RunCore() can log too
    static readonly System.Diagnostics.Stopwatch _sw = System.Diagnostics.Stopwatch.StartNew();
    static readonly bool _prof = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
    // Prof: always outputs if stderr is redirected (piped to log), or if WKAPPBOT_PROFILE=1
    static readonly bool _stderrRedirected = Console.IsErrorRedirected;
    static void Prof(string label) { if (_prof || _stderrRedirected) try { Console.Error.WriteLine($"[LAUNCHER] {_sw.ElapsedMilliseconds}ms {label}"); } catch { } }

    [STAThread]
    static int Main(string[] args)
    {
        // backward-compat local alias so existing prof("...") calls in Main still work
        Action<string> prof = Prof;

        // Dim all Launcher stderr via ANSI codes — MCP relay now writes raw UTF-8 bytes (preserves ANSI + encoding)
        Console.SetError(new DimStderrWriter(Console.Error));

        // stderr AutoFlush: when redirected (piped to file/log), ensure real-time output
        if (Console.IsErrorRedirected && Console.Error is System.IO.StreamWriter errSw)
            errSw.AutoFlush = true;
        prof("Main() entered");

        // ── Console encoding ──────────────────────────────────────────────────────────────────
        // Core runs as DETACHED_PROCESS → no console attached → Console.OutputEncoding
        // falls back to system ACP (CP949 on Korean Windows), not UTF-8.
        // So Core outputs CP949 bytes to the pipe.
        //
        // Passthrough policy:
        //   CP949 CMD  → passthrough (CP949→CP949) ✓
        //   UTF-8 terminal (CP65001, TERM, MSYSTEM, etc.) → Core outputs CP949 → need transcode CP949→UTF-8
        //
        // Do NOT call SetConsoleOutputCP — changes the terminal's CP, breaks host shell.
        _consoleCodePage = (int)GetConsoleOutputCP();
        {
            // Git Bash PTY: GetConsoleOutputCP() returns 949 (Windows ACP) but expects UTF-8 output.
            // Must check MSYSTEM/TERM/TERM_PROGRAM regardless of sysCP value.
            // WT_SESSION intentionally excluded: inherited by CMD children even after `chcp 949`.
            static string? Env(string k) => Environment.GetEnvironmentVariable(k);
            bool isUtf8Term = _consoleCodePage == 65001
                || !string.IsNullOrEmpty(Env("MSYSTEM"))
                || !string.IsNullOrEmpty(Env("TERM"))
                || !string.IsNullOrEmpty(Env("TERM_PROGRAM"));
            // Core always outputs UTF-8. Transcode UTF-8→CP for non-UTF-8 terminals (e.g. CP949 CMD).
            // UTF-8 terminals: passthrough.
            // EyeCmdPipeClient uses _consoleCodePage to decide encoding; normalize to 65001 for UTF-8 mode.
            _needsTranscode = !isUtf8Term;
            if (isUtf8Term) _consoleCodePage = 65001;
        }
        prof($"encoding-recovery: sysCP={GetConsoleOutputCP()} needsTranscode={_needsTranscode}");

        // ── Identity: who am I, who's my parent, what terminal am I in? ──
        try
        {
            var myPid = Environment.ProcessId;
            var parentPid = 0;
            var parentName = "?";
            var consoleHwnd = GetConsoleWindow();
            var consoleName = "(none)";
            try
            {
                using var me = System.Diagnostics.Process.GetCurrentProcess();
                // .NET doesn't expose PPID directly on Windows — use WMI-free P/Invoke
                parentPid = GetParentProcessId(myPid);
                if (parentPid > 0) try { parentName = System.Diagnostics.Process.GetProcessById(parentPid).ProcessName; } catch { }
            }
            catch { }
            if (consoleHwnd != IntPtr.Zero)
            {
                var cls = new System.Text.StringBuilder(256);
                GetClassNameW(consoleHwnd, cls, 256);
                consoleName = cls.ToString();
            }
            var cwd = Environment.CurrentDirectory;
            var exePath = Environment.ProcessPath ?? "";
            var sid = System.Diagnostics.Process.GetCurrentProcess().SessionId;
            // Host window: parent process's main window (VS Code, Terminal, etc.)
            var hostHwnd = IntPtr.Zero;
            var hostTitle = "";
            try
            {
                if (parentPid > 0)
                {
                    var pp = System.Diagnostics.Process.GetProcessById(parentPid);
                    hostHwnd = pp.MainWindowHandle;
                    if (hostHwnd != IntPtr.Zero)
                    {
                        var tb = new System.Text.StringBuilder(256);
                        GetWindowTextW(hostHwnd, tb, 256);
                        hostTitle = tb.ToString();
                        if (hostTitle.Length > 60) hostTitle = hostTitle[..57] + "...";
                    }
                }
            }
            catch { }
            // Foreground window at launch time
            var fgHwnd = GetForegroundWindow();
            var fgTitle = "";
            try { var fb = new System.Text.StringBuilder(256); GetWindowTextW(fgHwnd, fb, 256); fgTitle = fb.ToString(); if (fgTitle.Length > 60) fgTitle = fgTitle[..57] + "..."; } catch { }
            // Build JSON with stealth \r after each field — cursor resets, no wrap
            var err = Console.Error;
            void F(string s) { err.Write(s); err.Write('\r'); } // field + reset cursor
            F($"{{\"_\":\"LAUNCH\",\"pid\":{myPid},\"sid\":{sid}");
            if (consoleHwnd != IntPtr.Zero) F($",\"con\":\"0x{consoleHwnd:X}\",\"cls\":\"{consoleName}\"");
            if (fgHwnd != IntPtr.Zero) { F($",\"fg\":\"0x{fgHwnd:X}\""); if (fgTitle.Length > 0) F($",\"fgT\":\"{fgTitle.Replace("\"","'")}\""); }
            F($",\"cwd\":\"{cwd.Replace("\\","\\\\")}\"");
            // Parent chain
            F(",\"chain\":[");
            var chainPid = myPid;
            for (int ci = 0; ci < 10; ci++)
            {
                var pp = GetParentProcessId(chainPid);
                if (pp <= 0 || pp == chainPid) break;
                var pName = "?"; var pHwnd = IntPtr.Zero; var pTitle = "";
                try
                {
                    var p = System.Diagnostics.Process.GetProcessById(pp);
                    pName = p.ProcessName;
                    pHwnd = p.MainWindowHandle;
                    if (pHwnd != IntPtr.Zero) { var tb = new System.Text.StringBuilder(80); GetWindowTextW(pHwnd, tb, 80); pTitle = tb.ToString(); }
                }
                catch { }
                if (ci > 0) err.Write(',');
                F($"{{\"pid\":{pp},\"name\":\"{pName}\"");
                if (pHwnd != IntPtr.Zero) F($",\"hwnd\":\"0x{pHwnd:X}\"");
                if (pTitle.Length > 0) F($",\"title\":\"{(pTitle.Length > 50 ? pTitle[..47] + "..." : pTitle).Replace("\"","'")}\"");
                err.Write('}');
                chainPid = pp;
            }
            err.Write("]}");
            err.Write("\r" + new string(' ', 80) + "\r"); // final erase
            err.Flush();
        }
        catch { }

        // ── Encoding recovery: GetCommandLineA() → system codepage raw bytes ──
        // bash (Git Bash/MSYS2) corrupts Korean args at UTF-8↔CP949 boundary.
        // GetCommandLineA() returns raw system-codepage bytes — decode with system encoding.
        // If decoded args differ from Unicode args, the A version is the correct one.
        args = TryRecoverEncodingFromAnsiCommandLine(args);
        prof("encoding-recovery");

        // Busybox-style: if invoked as a11y.exe / wka11y.exe / etc., prepend implicit command.
        // Must be done in Launcher because Core is spawned as a new process — argv[0] loses the symlink name.
        // Also auto-create missing symlinks when running as wkappbot.exe.
        var argv0      = Environment.GetCommandLineArgs().FirstOrDefault() ?? "";
        var exeBase    = Path.GetFileNameWithoutExtension(argv0).ToLowerInvariant();
        prof($"exeBase={exeBase}");
        var implicitCmd = BusyboxAliases
            .Where(x => exeBase == x.name || exeBase.Contains(x.name))
            .Select(x => x.cmd)
            .FirstOrDefault();
        if (implicitCmd != null)
        {
            // grap/grep: pass alias name to Core (not "logcat") — Core handles arg-order translation + help.
            // This allows Core's help fast path to show grap-specific help for "grap" with no args.
            // wkedit: prepend "file" + "edit" (two args, not one)
            var prependCmd = (exeBase == "grap" || exeBase == "grep") ? exeBase : implicitCmd;
            if (exeBase == "wkedit")
                args = new[] { "file", "edit" }.Concat(args).ToArray();
            else if (args.Length == 0 || args[0].ToLowerInvariant() != prependCmd)
                args = new[] { prependCmd }.Concat(args).ToArray();
            prof($"busybox-prepend={prependCmd}");
        }

        // ── FAST EXITS ────────────────────────────────────────────────────────────────────────────
        // Encoding is set by app.manifest activeCodePage=UTF-8 (OS load, no runtime API call needed).
        // No Console.OutputEncoding/InputEncoding assignments in Launcher — encoding set via manifest.

        // grap/grep with no args (or --help/-h): print help directly in Launcher — no Core needed.
        if (args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep"
            && (args.Length == 1 || args.Any(a => a is "--help" or "-h")))
        {
            PrintGrapHelp(args[0].ToLowerInvariant());
            Console.Out.Flush();
            TerminateSelf(0);
            return 0; // unreachable
        }

        // wkappbot no-args: print usage directly in Launcher (same pattern as grap help path).
        // No Core spawn — avoids ConPTY handle issues entirely. TerminateSelf before encoding setup → fast.
        if (args.Length == 0)
        {
            prof("no-args → PrintUsage + TerminateSelf");
            PrintUsage();
            Console.Out.Flush();
            TerminateSelf(1);
            return 1; // unreachable
        }

        // Encoding: app.manifest activeCodePage=UTF-8 sets CP65001 at OS load.

        // --args-file <path>: read args from UTF-8 text file (one arg per line) to bypass
        // bash→PowerShell CP949/UTF-8 mismatch that corrupts Korean command-line args.
        // Scan after busybox prepend so implicit command is already present if needed.
        // File format: one arg per line, empty lines ignored. No quoting needed.
        // Usage: printf '%s\n' a11y type "hello" > /tmp/a.txt && wkappbot --args-file /tmp/a.txt
        {
            var argsFileIdx = Array.FindIndex(args, a => a == "--args-file");
            if (argsFileIdx >= 0 && argsFileIdx + 1 < args.Length)
            {
                var argsFilePath = args[argsFileIdx + 1];
                if (!File.Exists(argsFilePath))
                {
                    Console.Error.WriteLine($"[LAUNCHER] --args-file: not found: {argsFilePath}");
                    return 1;
                }
                var fileArgs = File.ReadAllLines(argsFilePath, Encoding.UTF8)
                    .Where(l => l.Length > 0).ToArray();
                args = args[..argsFileIdx].Concat(fileArgs).Concat(args[(argsFileIdx + 2)..]).ToArray();
                prof($"--args-file: {fileArgs.Length} args loaded");
            }
        }

        // Auto-create missing busybox symlinks (runs only when argv0 == wkappbot)
        if (exeBase == "wkappbot")
        {
            prof("EnsureBusyboxAliases start");
            EnsureBusyboxAliases();
            prof("EnsureBusyboxAliases done");
        }

        // --stderr: show stderr in real-time (default: buffered, shown only on error)
        bool showStderr = args.Any(a => a == "--stderr");
        var stderrBuf = !showStderr ? new System.Collections.Generic.List<(long ms, string msg)>() : null;
        _stderrBuf = stderrBuf; // for AppBotExit
        _originalStderr = Console.Error; // save before redirect
        if (stderrBuf != null)
        {
            Console.SetError(new LauncherStderrCapture(_originalStderr, stderrBuf, _sw));
        }

        // --core <path>: override core module name (default: wkappbot-core.exe next to launcher)
        {
            var coreIdx = Array.FindIndex(args, a => a == "--core");
            if (coreIdx >= 0 && coreIdx + 1 < args.Length)
            {
                _coreExePath = args[coreIdx + 1];
                args = args[..coreIdx].Concat(args[(coreIdx + 2)..]).ToArray();
                prof($"--core override: {_coreExePath}");
            }
        }

        // Routing control flags — parsed by Launcher, stripped before forwarding to Eye/Core
        bool onlyEye  = args.Any(a => a == "--only-eye");
        bool onlyCore = args.Any(a => a == "--only-core");

        if (onlyEye && onlyCore)
        {
            Console.Error.WriteLine("[LAUNCHER] --only-eye and --only-core are mutually exclusive");
            return 1;
        }

        // Strip routing flags from args forwarded downstream
        var forwardArgs = onlyEye || onlyCore
            ? args.Where(a => a != "--only-eye" && a != "--only-core").ToArray()
            : args;

        if (forwardArgs.Length == 0)
        {
            prof("no command after launcher-only flags -> PrintUsage + TerminateSelf");
            PrintUsage();
            Console.Out.Flush();
            TerminateSelf(1);
            return 1; // unreachable
        }

        var cmd = forwardArgs[0].ToLowerInvariant();
        prof($"cmd={cmd}");

        // mcp: Launcher holds the stdio pipe to Claude Code and manages Core lifecycle
        if (cmd == "mcp")
        {
            prof("mcp → RunMcpProxy");
            return RunMcpProxy(forwardArgs);
        }

        // eye (no subcommand / --elevated): IS the daemon, must run core directly
        // eye tick: one-shot status query — can go through Eye pipe (fast-path if Eye running, falls to Core if not)
        // file read-pdf/ocr: may take several seconds — use --only-core for long PDF/OCR jobs
        // file edit/read/grep/glob: fast operations — route through Eye pipe for zero cold-start
        // help/no-args: fast path — skip Eye pipe, run Core directly (Core is ~22ms for help)
        // logcat/grep/grap: streaming log monitor — needs direct stdout, TeeConsole, full error handling
        var isSlowFileCmd = cmd == "file" && args.Length > 1
            && args[1].ToLowerInvariant() is "read-pdf";
        // First-output guard: if Eye doesn't produce output within 100ms, fall back to Core.
        // Applies to most commands — prevents 30s+ stall when Eye is busy but Core can handle it.
        // Excluded: slack (Eye owns WebSocket), ask/newchat (long-running, double-run risk),
        //           logcat/grep/grap (streaming, already excluded below), eye daemon (isEyeDaemon).
        var isFirstOutputGuardCmd = cmd != "slack" && cmd != "ask" && cmd != "newchat";
        // eye tick / eye hotswap / eye homework: one-shot subcommands — route through Eye pipe if running
        var eyeSubcmd = forwardArgs.Length > 1 ? forwardArgs[1].ToLowerInvariant() : "";
        var isEyeDaemon = cmd == "eye"
            && eyeSubcmd is not ("tick" or "hotswap" or "homework" or "shutdown");
        var isWorkerMode = Environment.GetEnvironmentVariable("WKAPPBOT_WORKER") == "1";
        // hack-* workers are long-running → bypass Eye pipe (would timeout)
        var isHackWorker = cmd == "a11y" && forwardArgs.Length > 1
            && forwardArgs[1].StartsWith("hack-", StringComparison.OrdinalIgnoreCase);
        if (!onlyCore && !isEyeDaemon && !isSlowFileCmd && !isWorkerMode && !isHackWorker
            && cmd != "logcat" && cmd != "grep" && cmd != "grap"
            && cmd != "help" && cmd != "--help" && cmd != "-h")
        {
            // Parse --timeout / --timeout-exit for Eye pipe enforcement
            int eyeTimeoutMs = 0, eyeTimeoutExit = 2;
            for (int i = 0; i < forwardArgs.Length - 1; i++)
            {
                if (forwardArgs[i] == "--timeout" && int.TryParse(forwardArgs[i + 1], out var t) && t > 0) eyeTimeoutMs = t * 1000;
                if (forwardArgs[i] == "--timeout-exit" && int.TryParse(forwardArgs[i + 1], out var e)) eyeTimeoutExit = e;
            }

            int firstOutputMs = isFirstOutputGuardCmd ? 100 : 0; // 100ms first-output guard → Core fallback
            prof($"Eye pipe attempt cmd={cmd}");
            if (EyeCmdPipeClient.TryDelegate(forwardArgs, out int code, eyeTimeoutMs, eyeTimeoutExit, firstOutputMs))
            {
                prof("Eye pipe: delegated");
                // TerminateSelf: all output already flushed by TryDelegate; hard-kill Launcher immediately.
                Console.Out.Flush();
                Console.Error.Flush();
                TerminateSelf((uint)code);
                return code; // unreachable
            }
            prof("Eye pipe: unavailable, falling back to Core");

            if (onlyEye)
            {
                Console.Error.WriteLine("[LAUNCHER] --only-eye: Eye pipe unavailable — refusing Core fallback");
                return 3; // distinct exit: Eye required but not running
            }
        }

        prof($"→ RunCore cmd={cmd}");

        // grap/grep with args: spawn Core via UseShellExecute=true (ShellExecuteEx).
        // ShellExecuteEx detaches from bash's ConPTY/job tracking → bash
        // exits as soon as Launcher (wkappbot.exe) exits. Core runs in background.
        // Output relay: Core writes to WKAPPBOT_RELAY_FILE, signals .ready; Launcher reads+relays.
        // Follow mode (-f/--follow) runs normally (streaming, Ctrl+C to stop).
        if (cmd is "grap" or "grep")
        {
            bool isFollowMode = forwardArgs.Any(a => a is "-f" or "--follow");
            if (!isFollowMode)
            {
                var _dir2 = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
                var _core2 = ResolveCoreExe();
                var _relayBase = Directory.Exists(@"C:\Temp") ? @"C:\Temp" : Path.GetTempPath();
                var _relayFile    = Path.Combine(_relayBase, $"wkappbot-relay-{Environment.ProcessId}.txt");
                var _readyPath    = _relayFile + ".ready";
                var _ackPath      = _relayFile + ".ack";
                var _exitCodePath = _relayFile + ".exitcode";
                foreach (var p in new[] { _relayFile, _readyPath, _ackPath, _exitCodePath }) try { File.Delete(p); } catch { }

                // Set relay env var on our own process — inherited by UseShellExecute spawn
                Environment.SetEnvironmentVariable("WKAPPBOT_RELAY_FILE", _relayFile);

                var _tp = new System.Diagnostics.Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _core2,
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                    }
                };
                foreach (var a in forwardArgs) _tp.StartInfo.ArgumentList.Add(a);
                _tp.Start();
                var _lDbg = Path.Combine(@"C:\Temp", "launcher_relay_dbg.txt");
                void LDbg(string s) { try { File.AppendAllText(_lDbg, $"{_sw.ElapsedMilliseconds}ms {s}\n"); } catch { } }
                LDbg($"spawn done pid={_tp.Id}");
                prof("UseShellExecute spawn done, waiting for relay");

                // Poll for .ready (Core signals after flushing relay file, while still alive)
                var _relaySw = System.Diagnostics.Stopwatch.StartNew();
                bool _relayFound = false;
                while (_relaySw.ElapsedMilliseconds < 300_000) // 5min max
                {
                    if (GetFileAttributesW(_readyPath) != 0xFFFFFFFF) { _relayFound = true; break; }
                    System.Threading.Thread.Sleep(10);
                }
                LDbg($"poll done relayFound={_relayFound} elapsed={_relaySw.ElapsedMilliseconds}ms");

                if (_relayFound)
                {
                    LDbg("reading relay file");
                    try
                    {
                        var _content = File.ReadAllText(_relayFile, Encoding.UTF8);
                        LDbg($"relay read bytes={_content.Length}, writing stdout");
                        Console.Out.Write(_content);
                        LDbg("stdout write done, flushing");
                        Console.Out.Flush();
                        LDbg("flush done");
                    }
                    catch (Exception ex) { LDbg($"relay read/write FAILED: {ex.Message}"); }
                    LDbg("writing .ack");
                    try { File.WriteAllText(_ackPath, "1"); LDbg(".ack written"); } catch (Exception ex) { LDbg($".ack FAILED: {ex.Message}"); }
                }

                int _relayExitCode = 0;
                try { if (File.Exists(_exitCodePath)) _relayExitCode = int.Parse(File.ReadAllText(_exitCodePath).Trim()); } catch { }
                LDbg($"cleanup + TerminateSelf exitCode={_relayExitCode}");
                foreach (var p in new[] { _relayFile, _readyPath, _ackPath, _exitCodePath }) try { File.Delete(p); } catch { }
                prof("relay done, TerminateSelf");
                TerminateSelf((uint)_relayExitCode);
                return _relayExitCode; // unreachable
            }
            // follow mode: run normally (streaming, no timeout)
        }

        // Fast-exit commands (help): watchdog reports exact step if process hangs >3s.
        // _lDiagStep is a static field so RunCore() can update it directly.
        // --regression runs test scripts → needs more time, not a fast-exit
        bool isFastExit = (cmd is "help" or "--help" or "-h")
            && !forwardArgs.Any(a => a == "--regression");
        if (isFastExit)
        {
            _lDiagStep = "before-RunCore";
            var _lSw = System.Diagnostics.Stopwatch.StartNew();
            AppDomain.CurrentDomain.ProcessExit += (_, _) =>
                Console.Error.WriteLine($"[HANG-DIAG-L] ProcessExit: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
            var _lWatchdog = new Thread(() =>
            {
                Thread.Sleep(3_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 3s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
                Thread.Sleep(5_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 8s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
                Thread.Sleep(18_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 26s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
            }) { IsBackground = true, Name = "LauncherHangDiag" };
            _lWatchdog.Start();

            // Core calls FastExit (TerminateProcess) after flushing help output.
            // proc.WaitForExit detects termination immediately — no named-event needed.
            var code = RunCore(forwardArgs, fastExitTimeoutMs: 2000);
            _lDiagStep = "post-RunCore";
            TerminateSelf((uint)code);
            return code; // unreachable
        }

        int finalCode = RunCoreDetachedNormal(forwardArgs, showStderr, stderrBuf);
        AppBotExit(finalCode);
        return finalCode; // unreachable
    }

    // Shared step name for fast-exit watchdog — updated in both Main() and RunCore().
    static volatile string _lDiagStep = "";

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern uint GetFileAttributesW(string lpFileName);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern IntPtr GetCurrentProcess();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern IntPtr GetStdHandle(int nStdHandle);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    // GetConsoleOutputCP/SetConsoleOutputCP/SetConsoleCP declared below (shared with CoreRunner)
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    static extern int GetWindowTextW(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    static extern int GetClassNameW(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

    static int GetParentProcessId(int pid)
    {
        try
        {
            var handle = System.Diagnostics.Process.GetProcessById(pid).Handle;
            var pbi = new byte[48]; // PROCESS_BASIC_INFORMATION
            int retLen = 0;
            NtQueryInformationProcess(handle, 0, pbi, pbi.Length, ref retLen);
            return (int)BitConverter.ToInt64(pbi, 24); // InheritedFromUniqueProcessId at offset 24
        }
        catch { return 0; }
    }
    [System.Runtime.InteropServices.DllImport("ntdll.dll")]
    static extern int NtQueryInformationProcess(IntPtr handle, int infoClass, byte[] info, int infoLen, ref int retLen);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool SetConsoleOutputCP(uint wCodePageID);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool SetConsoleCP(uint wCodePageID);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool FreeConsole();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    static extern IntPtr GetCommandLineA();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint GetConsoleOutputCP();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint GetConsoleCP();

    // CreateProcessW with DETACHED_PROCESS: spawns Core outside bash's ConPTY session.
    // Structs + guard: AppBotPipe.cs (shared between Launcher and Core)
    // Thin delegate: all calls route through AppBotPipe.CreateProcess (null CWD guard)
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hRead, out IntPtr hWrite, IntPtr lpPipeAttributes, uint size);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetHandleInformation(IntPtr h, uint mask, uint flags);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool ReadFile(IntPtr h, byte[] buf, uint toRead, out uint read, IntPtr ov);
    // Overlapped ReadFile for IOCP
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, EntryPoint = "ReadFile")]
    static extern bool ReadFileOv(IntPtr h, byte[] buf, uint toRead, out uint read, ref System.Threading.NativeOverlapped ov);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteFile(IntPtr h, byte[] buf, uint toWrite, out uint written, IntPtr ov);
    // Named pipe + IOCP
    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern IntPtr CreateNamedPipeW(string name, uint openMode, uint pipeMode,
        uint maxInstances, uint outBufSize, uint inBufSize, uint defaultTimeout, IntPtr secAttr);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool ConnectNamedPipe(IntPtr hPipe, ref System.Threading.NativeOverlapped ov);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern IntPtr CreateFileW(string name, uint access, uint share, IntPtr secAttr,
        uint disposition, uint flags, IntPtr template);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr CreateIoCompletionPort(IntPtr fileHandle, IntPtr existingPort,
        UIntPtr completionKey, uint numThreads);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetQueuedCompletionStatus(IntPtr port, out uint bytes,
        out UIntPtr key, out IntPtr ov, uint timeout);
    const uint PIPE_ACCESS_INBOUND  = 0x00000001;
    const uint FILE_FLAG_OVERLAPPED = 0x40000000;
    const uint GENERIC_WRITE        = 0x40000000;
    const uint OPEN_EXISTING        = 3;
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern bool FlushFileBuffers(IntPtr h);
    const uint HANDLE_FLAG_INHERIT = AppBotPipe.HANDLE_FLAG_INHERIT;
    const uint STARTF_USESTDHANDLES = AppBotPipe.STARTF_USESTDHANDLES;
    static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);

    // Named event for Core→Launcher exit signaling (bypasses FS visibility delay + process exit delay)
    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern IntPtr CreateEventW(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetEvent(IntPtr hEvent);

    /// <summary>Thin delegate to AppBotPipe.CreateProcess — null CWD guard + trace.</summary>
    static bool CreateProcessW(string? app, char[] cmd, IntPtr pa, IntPtr ta,
        bool inh, uint flags, IntPtr env, string? cwd, ref STARTUPINFOW si, out PROCESS_INFORMATION pi)
        => AppBotPipe.CreateProcess(app, cmd, pa, ta, inh, flags, env, cwd, ref si, out pi, out _, "LAUNCHER");

    const uint DETACHED_PROCESS          = AppBotPipe.DETACHED_PROCESS;
    const uint CREATE_BREAKAWAY_FROM_JOB = AppBotPipe.CREATE_BREAKAWAY_FROM_JOB;
    const uint CREATE_UNICODE_ENVIRONMENT = AppBotPipe.CREATE_UNICODE_ENVIRONMENT;

    /// <summary>
    /// Build a Unicode environment block for Core: current env + WKAPPBOT_RELAY_FILE, minus PTY vars.
    /// The block is double-null-terminated (Win32 CreateProcess format).
    /// Caller must free the returned pointer with Marshal.FreeHGlobal.
    /// </summary>
    static IntPtr BuildDetachedEnvBlock(string relayFilePath)
    {
        var strip = new System.Collections.Generic.HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
        {
            "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL", "ConEmuANSI",
            "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST", "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE",
            "WKAPPBOT_RELAY_FILE",
        };
        var sb = new StringBuilder();
        foreach (System.Collections.DictionaryEntry kv in Environment.GetEnvironmentVariables())
        {
            var k = kv.Key?.ToString() ?? "";
            if (strip.Contains(k)) continue;
            sb.Append(k).Append('=').Append(kv.Value?.ToString() ?? "").Append('\0');
        }
        sb.Append("WKAPPBOT_RELAY_FILE=").Append(relayFilePath).Append('\0');
        sb.Append('\0'); // double-null terminator
        var bytes = Encoding.Unicode.GetBytes(sb.ToString());
        var ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(bytes.Length);
        System.Runtime.InteropServices.Marshal.Copy(bytes, 0, ptr, bytes.Length);
        return ptr;
    }

    /// <summary>
    /// Spawn Core with DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB.
    /// Core runs outside bash's ConPTY session and job object → bash doesn't wait for Core.
    /// Returns Core's process handle (caller must CloseHandle) or IntPtr.Zero on failure.
    /// </summary>
    static IntPtr SpawnDetachedCore(string core, string[] args, IntPtr envBlock)
    {
        var cmd = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmd.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        var cmdArr = (cmd.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW { cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>() };
        bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, false,
            DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB | CREATE_UNICODE_ENVIRONMENT,
            envBlock, Environment.CurrentDirectory, ref si, out var pi);
        if (!ok) return IntPtr.Zero;
        CloseHandle(pi.hThread);
        return pi.hProcess;
    }

    /// <summary>
    /// Spawn Core with DETACHED_PROCESS + pipe handles for stdout and stderr.
    /// DETACHED_PROCESS prevents console LPC init (avoids bash/ConPTY deadlock).
    /// Returns Core's process handle and pipe read handles. Caller must CloseHandle all.
    /// Returns false on failure (caller should fall back to RunCore).
    /// </summary>
    static bool SpawnDetachedCoreWithPipes(string core, string[] args, IntPtr envBlock,
        out IntPtr hProc, out IntPtr hStdoutRead, out IntPtr hStderrRead)
    {
        hProc = hStdoutRead = hStderrRead = IntPtr.Zero;
        // Create pipes with no SA (default: non-inheritable). Then make write ends inheritable.
        if (!CreatePipe(out hStdoutRead, out var hStdoutWrite, IntPtr.Zero, 0)) return false;
        if (!CreatePipe(out hStderrRead, out var hStderrWrite, IntPtr.Zero, 0)) { CloseHandle(hStdoutRead); CloseHandle(hStdoutWrite); return false; }
        // Make write ends inheritable (child needs them); read ends stay non-inheritable.
        SetHandleInformation(hStdoutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        SetHandleInformation(hStderrWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        // Make Launcher's own stdout/stderr non-inheritable so Core doesn't inherit bash's pipe.
        // Without this, Core holds bash's pipe write end → bash waits for Core to die (~30s) after Launcher exits.
        var hLauncherOut = GetStdHandle(-11); var hLauncherErr = GetStdHandle(-12);
        if (hLauncherOut != IntPtr.Zero && hLauncherOut != (IntPtr)(-1)) SetHandleInformation(hLauncherOut, HANDLE_FLAG_INHERIT, 0);
        if (hLauncherErr != IntPtr.Zero && hLauncherErr != (IntPtr)(-1)) SetHandleInformation(hLauncherErr, HANDLE_FLAG_INHERIT, 0);

        var cmd = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmd.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        var cmdArr = (cmd.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW
        {
            cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>(),
            dwFlags = STARTF_USESTDHANDLES,
            hStdInput  = INVALID_HANDLE, // no stdin (DETACHED_PROCESS has no console)
            hStdOutput = hStdoutWrite,
            hStdError  = hStderrWrite,
        };
        bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, true, // bInheritHandles=true for pipe handles
            DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB | CREATE_UNICODE_ENVIRONMENT,
            envBlock, Environment.CurrentDirectory, ref si, out var pi);
        // Close write ends in parent — child holds them; closing here causes EOF when child exits
        CloseHandle(hStdoutWrite);
        CloseHandle(hStderrWrite);
        if (!ok) { CloseHandle(hStdoutRead); CloseHandle(hStderrRead); hStdoutRead = hStderrRead = IntPtr.Zero; return false; }
        CloseHandle(pi.hThread);
        hProc = pi.hProcess;
        return true;
    }

    /// <summary>
    /// grap/grep one-shot: spawn Core detached.
    /// Core writes output to relay file (WKAPPBOT_RELAY_FILE). Launcher polls for .ready sentinel
    /// (created by Core while still alive → immediately visible). Reads relay, exits fast.
    /// </summary>
    static int RunGrapDetached(string[] args, int timeoutMs)
    {
        var dir  = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = ResolveCoreExe();
        if (!File.Exists(core))
        {
            Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found: {core}");
            return 1;
        }

        var relayBase = Directory.Exists(@"C:\Temp") ? @"C:\Temp" : Path.GetTempPath();
        var relayFile = Path.Combine(relayBase, $"wkappbot-relay-{Environment.ProcessId}.txt");
        var readyPath = relayFile + ".ready";
        var ackPath   = relayFile + ".ack";
        foreach (var p in new[] { relayFile, readyPath, ackPath }) try { File.Delete(p); } catch { }

        var envPtr = BuildDetachedEnvBlock(relayFile);
        try
        {
            var hProc = SpawnDetachedCore(core, args, envPtr);
            if (hProc == IntPtr.Zero)
            {
                Prof($"detached spawn failed (err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()}), fallback");
                { int fb = RunCore(args); Console.Out.Flush(); Console.Error.Flush(); TerminateSelf((uint)fb); return fb; } // fallback
            }
            Prof($"detached core spawned pid=?, polling .ready");

            // Poll for .ready (Core creates it while alive → visible on local FS immediately)
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool found = false;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (GetFileAttributesW(readyPath) != 0xFFFFFFFF) { found = true; break; }
                Thread.Sleep(5);
            }
            Prof(found ? $"detached: .ready found t={sw.ElapsedMilliseconds}ms"
                       : $"detached: .ready timeout t={sw.ElapsedMilliseconds}ms");

            if (found)
            {
                try
                {
                    var content = File.ReadAllText(relayFile, Encoding.UTF8);
                    Console.Out.Write(content);
                    Console.Out.Flush();
                    Prof($"detached: {content.Length} chars written");
                }
                catch (Exception ex) { Prof($"detached: relay read error: {ex.Message}"); }
                try { File.WriteAllText(ackPath, "1"); } catch { } // signal Core: file read
            }
            else
            {
                // Core never signaled — kill it (abnormal)
                try { TerminateProcess(hProc, 1); } catch { }
            }
            CloseHandle(hProc);
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(envPtr);
            foreach (var p in new[] { relayFile, readyPath, ackPath }) try { File.Delete(p); } catch { }
        }
        TerminateSelf(0);
        return 0; // unreachable
    }

    static void TerminateSelf(uint code) => TerminateProcess(GetCurrentProcess(), code);

    /// <summary>Core exe path override (--core flag). Null = default (wkappbot-core.exe next to launcher).</summary>
    static string? _coreExePath;

    /// <summary>True when console can't do UTF-8 — IOCP relay must transcode Core's UTF-8 output.</summary>
    static bool _needsTranscode;
    /// <summary>Console output code page (e.g. 949 for Korean CMD). Used by TranscodeStream + EyeCmdPipeClient.</summary>
    internal static int _consoleCodePage;

    /// <summary>Resolve core exe path: --core override or default.</summary>
    static string ResolveCoreExe()
    {
        if (_coreExePath != null) return _coreExePath;
        var dir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        return Path.Combine(dir, "wkappbot-core.exe");
    }

    /// <summary>
    /// Universal exit — flushes timestamped stderr log on error, then TerminateSelf.
    /// Usage: AppBotExit(0);  // success, discard errors
    ///        AppBotExit(1);  // error, flush stderr log
    /// </summary>
    [ThreadStatic] static System.Collections.Generic.List<(long ms, string msg)>? _stderrBuf;
    static System.IO.TextWriter? _originalStderr;

    static void AppBotExit(int code)
    {
        // Restore original stderr FIRST (bypass buffer), then write error log
        if (_originalStderr != null) Console.SetError(_originalStderr);
        if (code != 0 && _stderrBuf != null && _stderrBuf.Count > 0)
        {
            try
            {
                Console.Error.WriteLine("\n--- Error Log ---");
                foreach (var (ms, msg) in _stderrBuf)
                    Console.Error.WriteLine($"[+{ms / 1000.0:F1}s] {msg}");
            }
            catch { }
        }
        Console.Out.Flush(); Console.Error.Flush();
        TerminateSelf((uint)code);
    }

    /// <summary>Lightweight stderr tee for Launcher: passes through + buffers with timestamps.</summary>
    sealed class LauncherStderrCapture : System.IO.TextWriter
    {
        readonly System.IO.TextWriter _inner;
        readonly System.Collections.Generic.List<(long ms, string msg)> _buf;
        readonly System.Diagnostics.Stopwatch _sw;
        public LauncherStderrCapture(System.IO.TextWriter inner,
            System.Collections.Generic.List<(long ms, string msg)> buf,
            System.Diagnostics.Stopwatch sw) { _inner = inner; _buf = buf; _sw = sw; }
        public override System.Text.Encoding Encoding => _inner.Encoding;
        public override void WriteLine(string? value)
        {
            _inner.WriteLine(value);
            if (!string.IsNullOrEmpty(value))
                lock (_buf) _buf.Add((_sw.ElapsedMilliseconds, value));
        }
        public override void Write(char value) => _inner.Write(value);
        public override void Write(string? value) => _inner.Write(value);
        public override void Flush() => _inner.Flush();
    }

    /// <summary>
    /// Normal (non-fast-exit, non-relay) Core spawn via DETACHED_PROCESS + pipes.
    /// DETACHED_PROCESS prevents .NET 8 AppHost console LPC deadlock in bash/ConPTY context.
}
