using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.Json;
using WKAppBot.Core.Scenario;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

/// <summary>
/// CLI entry point and command dispatcher.
/// Commands are implemented in partial class files under Commands/ and Helpers/.
///
/// File layout:
///   Program.cs                          — Main, DataDir, run, validate (this file)
///   Commands/UsageCommand.cs            — PrintUsage, Error, GetArgValue
///   Commands/InspectionCommands.cs      — inspect, focus, watch, capture + watch helpers
///   Commands/ChartCommands.cs           — chart-analyze, tooltip-probe
///   Commands/HtsStressCommand.cs        — hts-stress
///   Commands/ScanCommands.cs            — scan, form-dump
///   Commands/AutomationCommands.cs      — click, do, SmartClickButton, CheckDialogGone
///   Commands/DialogCommands.cs          — dismiss, dialog-click, toolbar-ocr, TryHandleBlocker
///   Helpers/ControlHelpers.cs           — FindAllChildrenFlat, FindMfcCombos, DumpFormTree,
///                                         BuildClassPath, notice/dialog content helpers,
///                                         InteractiveButtonLearn, GenerateLearnedHandler
/// </summary>
internal partial class Program
{
    /// <summary>Set by EyeCmdPipeServer before calling Main() — skips TeeWriter, crash handler, LaunchEye.</summary>
    internal static volatile bool RunningInEye = false;
    internal static volatile bool IsMcpMode = false;
    [ThreadStatic] internal static bool McpElevationRequired;

    /// <summary>True when logcat runs in piped stdout (grep-mode): matches → OriginalStdout, diagnostics → stderr.</summary>
    internal static bool GrepModeActive = false;
    internal static bool GrapMode = false; // true when invoked as grap/grep → one-shot scan even on TTY
    /// <summary>True when stdout is redirected (pipe/file). Suppresses diagnostic output ([ACT], cmd echo)
    /// so downstream tools receive clean data only. Set early in Main, before TeeWriter.</summary>
    internal static bool IsPipeMode = false;
    internal static bool QuietFindOutput = Environment.GetEnvironmentVariable("WKAPPBOT_QUIET_FIND") == "1";
    internal static volatile bool GlobalTimeoutRequested = false;
    internal static long GlobalTimeoutRequestedTicks = 0;
    private static readonly HashSet<string> _autoBugDedup = new();

    /// <summary>
    /// [BUG-AUTO] Auto bug report — captures callstack + log tail, posts as suggest via Eye pipe.
    /// Call from anywhere: Program.AutoBugReport("description").
    /// Dedup per session (same context → once only). Zero process spawn.
    /// </summary>
    internal static void AutoBugReport(string context)
    {
        if (!_autoBugDedup.Add(context)) return;

        var stack = new System.Diagnostics.StackTrace(1, true);
        var callerFrame = stack.GetFrame(0);
        var callerInfo = $"{callerFrame?.GetMethod()?.DeclaringType?.Name}.{callerFrame?.GetMethod()?.Name}:{callerFrame?.GetFileLineNumber()}";

        var lastLog = _currentLogPath != null && File.Exists(_currentLogPath)
            ? string.Join("\n", File.ReadLines(_currentLogPath).TakeLast(5)) : "(no log)";

        var bugText = $"[BUG-AUTO] {context}\\ncaller: {callerInfo}\\ncommand: {string.Join(" ", EyeCmdPipeServer.CallerArgs.Value ?? [])}\\nlog tail:\\n{lastLog}";
        Console.Error.WriteLine($"[BUG-AUTO] {context} at {callerInfo}");

        EyeCmdPipeServer.DispatchBg(["suggest", bugText]);
    }
    /// <summary>When true, UIA write operations (TypeAndSubmit, Click, etc.) are blocked.
    /// Set via --read-only CLI flag. Callers can pass this when they need read-only UIA access.</summary>
    internal static bool ReadOnlyMode = false;

    internal static bool IsElevated()
    {
        try
        {
            using var id = System.Security.Principal.WindowsIdentity.GetCurrent();
            return new System.Security.Principal.WindowsPrincipal(id)
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        catch { return false; }
    }
    // Relay file path for grap/grep one-shot mode (WKAPPBOT_RELAY_FILE env var).
    // FastExit creates {relayFilePath}.done sentinel after closing the file.
    internal static string? RelayFilePath = null;
    static string? _exitFilePath = null; // --exit-file arg from Launcher (IOCP exit sentinel)
    static string? _exitEventName = null; // --exit-event arg from Launcher (named event for instant signaling)
    static volatile int _exitCleanupDone = 0; // interlocked guard: DoRequiredExitCleanup runs exactly once
    static TeeTextWriter? _activeTee = null;  // set after tee is created; used by ProcessExit hook

    /// <summary>Original Console.Out before TeeTextWriter is installed. Used by grep-mode to write matches to real stdout.</summary>
    internal static TextWriter OriginalStdout = Console.Out;

    /// <summary>
    /// Runtime data directory: {exe_dir}/wkappbot.hq/
    /// All logs, profiles, output go here — keeps SDK/bin clean.
    /// HQ = HeadQuarters (본부: 경험치 축적 + 작전 기록 + 전과 보고)
    /// </summary>
    internal static readonly string DataDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq");

    internal static string GetCurrentSessionHash()
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir))
                return "none";

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrEmpty(latestFile) || !File.Exists(latestFile))
                return "none";

            using var sha = SHA256.Create();
            var data = Encoding.UTF8.GetBytes(Path.GetFullPath(latestFile));
            var hash = sha.ComputeHash(data);
            return Convert.ToHexString(hash).ToLowerInvariant()[..16];
        }
        catch
        {
            return "none";
        }
    }

    internal static int Main(string[] args)
    {
        // Hook ALL exit paths — on CLR shutdown, call TerminateProcess to skip DLL-detach (~27s).
        // AppDomain.ProcessExit fires when: return from Main, Environment.Exit(), or unhandled exception.
        // NOTE: FastExit (TerminateProcess) already bypasses this hook, so no double-kill.
        int _exitCode = 1;
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            try { XRayHelper.RestoreAll(); } catch { }
            try { Console.Out.Flush(); } catch { }
            // DoRequiredExitCleanup: tee.Dispose (log move) + WriteExitFile, idempotent.
            // signalLauncher=false — we signal via UIT + TerminateProcess below instead,
            // to avoid the named-event SetEvent racing with TerminateProcess handle closure.
            // FastExit (TerminateProcess) bypasses CLR entirely → never reaches here.
            // RunningInEye/IsMcpMode: no _exitFilePath/_exitEventName → WriteExitFile is no-op.
            DoRequiredExitCleanup(_exitCode, signalLauncher: false);
            // Signal Launcher to TerminateSelf immediately — don't wait for Core's ~30s OS cleanup.
            try
            {
                var raw = Console.OpenStandardError();
                raw.Write(new byte[] { 0, (byte)'U', (byte)'I', (byte)'T' });
                raw.Flush();
            }
            catch { }
            TerminateProcess(GetCurrentProcess(), (uint)_exitCode);
        };

        var _mainStarted = System.Diagnostics.Stopwatch.StartNew();

        // WKAPPBOT_PROFILE=1 → emit [PROFILE] Xms label to stderr for startup diagnostics.
        // Usage: WKAPPBOT_PROFILE=1 wkappbot grep foo
        bool _profiling = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
        Action<string> prof = _profiling
            ? (label => { try { Console.Error.WriteLine($"[PROFILE] {_mainStarted.ElapsedMilliseconds}ms {label}"); } catch { } })
            : (_ => { });

        prof("Main() entered");

        // ── FAST EXITS: must run BEFORE SetConsoleCP/SetConsoleOutputCP ──────────────────────────
        // SetConsoleCP/SetConsoleOutputCP initialize SMB-backed console kernel objects.
        // FastExit (TerminateProcess) after those calls causes OS SMB cleanup taking ~27-35s.
        // By exiting before any console setup, cleanup is <200ms.
        // Help/usage text is ASCII-only so no encoding setup is needed.
        {
            // grap alias sub-commands: save/list/show/remove/verify → GrapAliasCommand (not grep)
            if (args.Length >= 2 && args[0].ToLowerInvariant() == "grap"
                && args[1].ToLowerInvariant() is "save" or "list" or "show" or "remove" or "rm"
                    or "delete" or "del" or "verify")
            {
                try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
                Console.OutputEncoding = new System.Text.UTF8Encoding(false);
                return GrapAliasCommand(args[1..]);
            }

            // grap/grep help: no args or --help/-h
            if (args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep"
                && (args.Length == 1 || args.Any(a => a is "--help" or "-h")))
            {
                try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
                PrintGrapHelp(args[0].ToLowerInvariant());
                Console.Out.Flush();
                FastExit(0);
                return 0; // unreachable
            }

            // wkappbot no-args: print usage and exit
            // GetUsageText() is ASCII-only — no Unicode/Korean → no SetConsoleOutputCP trigger.
            // FastExit (TerminateProcess) is fast when SetConsoleOutputCP was never called (<200ms).
            // DO NOT set Console.OutputEncoding here — that calls SetConsoleOutputCP(65001),
            // initializing SMB-backed console kernel objects, making TerminateProcess take ~32s.
            var _noArgsExeBase = Path.GetFileNameWithoutExtension(
                Environment.GetCommandLineArgs().FirstOrDefault() ?? "").ToLowerInvariant();
            if (args.Length == 0 && DetectCommandFromExeName(_noArgsExeBase) == null)
            {
                // No encoding setup here — avoids SetConsoleOutputCP(65001) which initializes
                // SMB-backed console kernel objects causing ~30s OS cleanup on TerminateProcess.
                // GetUsageText() is ASCII-only, so no encoding setup needed.
                PrintUsage();
                FastExit(1);
                return 1; // unreachable
            }
        }

        // Force UTF-8 globally — console + child processes inherit codepage 65001
        // Use no-BOM variant: BOM is noise in pipes/relay, Console.Out is not a file
        try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }
        try { Console.InputEncoding = Encoding.UTF8; } catch { }
        try { WKAppBot.Win32.Native.NativeMethods.SetConsoleCP(65001); } catch { }
        try { WKAppBot.Win32.Native.NativeMethods.SetConsoleOutputCP(65001); } catch { }
        // DETACHED_PROCESS fallback: no console attached → SetConsoleOutputCP no-ops,
        // Console.OutputEncoding silently stays at system ACP (CP949 on Korean Windows).
        // Force UTF-8 StreamWriter directly on the raw stdout handle.
        if (Console.OutputEncoding.CodePage != 65001)
        {
            try
            {
                var raw = Console.OpenStandardOutput();
                var utf8Out = new System.IO.StreamWriter(raw, new System.Text.UTF8Encoding(false)) { AutoFlush = true };
                Console.SetOut(utf8Out);
            }
            catch { }
        }


        // Worker processes spawned by Eye (screensaver, whisper-ring, find-prompts, analyze-hack, slack-route)
        // inherit WKAPPBOT_WORKER=1 env → suppress ticks, TeeWriter, crash handler, LaunchEye (same as RunningInEye).
        if (Environment.GetEnvironmentVariable("WKAPPBOT_WORKER") == "1")
            RunningInEye = true;

        // Mirror all stderr to OutputDebugString — useful for MCP server debugging (no pipe pollution).
        // Capture with: wkappbot logcat --dbg [pid]
        // Prefix: wkappbot:{pid}-{cmd}-{subcmd}> so each line identifies which command produced it.
        {
            var dbgCmd = args.FirstOrDefault(a => !a.StartsWith('-'));
            var dbgSub = dbgCmd != null ? args.SkipWhile(a => a != dbgCmd).Skip(1).FirstOrDefault(a => !a.StartsWith('-')) : null;
            Console.SetError(new DebugStringWriter(Console.Error, dbgCmd, dbgSub));
            // [CMD] marker: emitted immediately so DbgViewListener can verify real command execution.
            // grep-only test scripts produce no wkappbot process → no [CMD] in captured debug output.
            if (!QuietFindOutput)
                Console.Error.WriteLine($"[CMD] {string.Join(" ", args)}");
        }

        // --args-file <path>: UTF-8 file fallback for Korean args garbled via bash→PowerShell CP949
        {
            var argsFileIdx = Array.FindIndex(args, a => a == "--args-file");
            if (argsFileIdx >= 0 && argsFileIdx + 1 < args.Length)
            {
                var argsFilePath = args[argsFileIdx + 1];
                if (File.Exists(argsFilePath))
                {
                    var fileArgs = File.ReadAllLines(argsFilePath, Encoding.UTF8)
                        .Where(l => l.Length > 0).ToArray();
                    args = args[..argsFileIdx].Concat(fileArgs).Concat(args[(argsFileIdx + 2)..]).ToArray();
                }
            }
        }

        // --exit-file <path>: Launcher passes exit sentinel file path for IOCP relay.
        // Core writes exit code to this file in WriteExitFile() → Launcher polls and exits fast.
        {
            var efIdx = Array.FindIndex(args, a => a == "--exit-file");
            if (efIdx >= 0 && efIdx + 1 < args.Length)
            {
                _exitFilePath = args[efIdx + 1];
                args = args[..efIdx].Concat(args[(efIdx + 2)..]).ToArray();
            }
        }
        // --exit-event <name>: Launcher passes named event for instant Core→Launcher signaling.
        // Core opens and signals this event in WriteExitFile() → Launcher WaitForSingleObject returns immediately.
        {
            var eeIdx = Array.FindIndex(args, a => a == "--exit-event");
            if (eeIdx >= 0 && eeIdx + 1 < args.Length)
            {
                _exitEventName = args[eeIdx + 1];
                args = args[..eeIdx].Concat(args[(eeIdx + 2)..]).ToArray();
            }
        }

        // MCP stdio server — must run BEFORE TeeTextWriter (stdout = JSON-RPC only)
        if (args.Length > 0 && args[0] == "mcp")
        {
            try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }
            return McpCommand(args.Skip(1).ToArray());
        }

        // Enable DPI awareness
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }

        // Startup hot-swap: if .new.exe was staged while Eye/Core was busy, apply it now.
        // Covers the case where Launcher fell back to Core due to empty Eye pipe (Eye crash/kill).
        if (!RunningInEye)
        {
            var coreExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
            if (!string.IsNullOrEmpty(coreExePath))
                ThreadPool.QueueUserWorkItem(_ => { try { TryRenameSwap(coreExePath, "CORE:STARTUP"); } catch { } });
        }

        // Kill ghost zoom overlays from previous invocations (keeps exe unlocked for publish)
        try { InputZoomHost.CloseAllGhosts(); } catch { }

        // Auto-create busybox symlinks (a11y.exe, wka11y.exe → wkappbot.exe) if missing.
        // Skip for grap/grep fast-exit: symlink file ops on W:/ (SMB) leave pending kernel I/O
        // that blocks TerminateProcess for ~27s (SMB cancel timeout).
        bool _isGrapFastPath = args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep" && args.Length > 1;
        if (!_isGrapFastPath)
        {
            ThreadPool.QueueUserWorkItem(_ => { try { EnsureBusyboxAliases(); } catch { } });
            prof("EnsureBusyboxAliases queued (background)");
        }

        // Orphan guard: if parent dies, exit too (prevents ghost processes like stuck win-click).
        // Eye mode is intentionally long-running/detached → skip.
        bool isEyeCmd = args.Length > 0 && args[0].Equals("eye", StringComparison.OrdinalIgnoreCase);
        if (!RunningInEye && !isEyeCmd)
        {
            int parentPid = GetParentPid(Environment.ProcessId);
            if (parentPid > 0)
            {
                var monitor = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            using var ph = System.Diagnostics.Process.GetProcessById(parentPid);
                            if (ph.HasExited) { AppBotExit(0, runCleanup: true); Environment.Exit(0); return; }
                        }
                        catch (ArgumentException) { AppBotExit(0, runCleanup: true); Environment.Exit(0); return; } // parent gone
                        catch { } // access denied etc. — keep watching
                    }
                }) { IsBackground = true, Name = "OrphanGuard" };
                monitor.Start();
            }
        }

        // Screen reader mode: once enabled for Chromium/Electron, stays ON permanently.
        // No restore on exit — next run starts instantly (no broadcast delay).

        prof("OrphanGuard started");

        // Auto-log: tee all console output to file
        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var exeName = Path.GetFileName(exePath);
        var logDir = Path.Combine(DataDir, "logs");
        // Delay CreateDirectory until we know this isn't a fast-exit path (grap/grep).
        // Directory.CreateDirectory on W:/ (SMB) can leave pending kernel I/O that delays
        // TerminateProcess exit by ~27s (SMB cancel timeout). Logcat can create it if needed.
        var pid = Environment.ProcessId;
        bool _fastExitAfterCommand = false; // set when grap/grep alias → FastExit after logcat to skip DLL-detach 26s hang

        // ── Alias rewrite (runs BEFORE cmdTag/logFile so all downstream sees canonical command) ──
        // Step 1: busybox exe-name → prepend implicit command (symlink-friendly)
        //   grap.exe error *.log  → args = ["grap", "error", "*.log"]
        //   a11y.exe find "*foo*" → args = ["a11y", "find", "*foo*"]
        // Use argv[0] (not ProcessPath) — ProcessPath resolves symlinks, argv[0] preserves link name.
        {
            var argv0 = Environment.GetCommandLineArgs().FirstOrDefault() ?? exePath;
            var exeBaseName = Path.GetFileNameWithoutExtension(argv0).ToLowerInvariant();
            var implicitCmd = DetectCommandFromExeName(exeBaseName);
            if (implicitCmd != null && (args.Length == 0 || args[0].ToLowerInvariant() != implicitCmd))
                args = new[] { implicitCmd }.Concat(args).ToArray();
        }
        // Step 2: command alias → canonical command + arg translation
        //   grep/grap: logcat alias, grep-compat arg order (pattern first → fileFilter first)
        if (args.Length > 0 && args[0].ToLowerInvariant() is "grep" or "grap")
        {
            var alias = args[0].ToLowerInvariant();
            // --help or no-args: show alias-specific help (before rewrite)
            if (args.Length == 1 || args.Any(a => a is "--help" or "-h"))
            {
                prof("PrintGrapHelp");
                PrintGrapHelp(alias);
                prof("return 0");
                Console.Out.Flush();
                FastExit(0); // TerminateProcess — skips loader-lock deadlock (~26s) from background symlink thread
                return 0; // unreachable if FastExit works
            }
            args = new[] { "logcat" }.Concat(GrapArgsToLogcat(args.Skip(1).ToArray())).ToArray();
            _fastExitAfterCommand = true; // skip DLL-detach 26s hang after logcat completes
            GrapMode = true;             // grep-compatible one-shot behavior
        }

        // CreateDirectory + RotateOldLogs: skip for grap/grep fast-exit path — they call
        // TerminateProcess after logcat, and any file I/O on W:/ (SMB network drive) leaves
        // pending kernel I/O that blocks TerminateProcess for ~27s (SMB cancel timeout).
        if (!_fastExitAfterCommand)
        {
            Directory.CreateDirectory(logDir);
            ThreadPool.QueueUserWorkItem(_ => { try { RotateOldLogs(logDir, staleHours: 24); } catch { } });
            prof("RotateOldLogs queued (background)");
        }

        // Include command name in log filename for easy identification via ls
        // e.g. "wkappbot.exe.out-20260221_211427.eye.pid=36944.txt"
        var (cmdTag, oldSubDir) = ComputeCmdTagAndSubDir(args);
        var logFile = Path.Combine(logDir, $"{exeName}.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={pid}.log");
        // Track current command log path for auto-heal diagnostics (non-Eye mode only; Eye sets it in RunInEye)
        if (!RunningInEye) _currentLogPath = logFile;

        // Pipe mode: stdout is redirected (pipe/file) — suppress diagnostic output for ALL commands.
        // Must be captured before TeeWriter replaces Console.Out.
        IsPipeMode = Console.IsOutputRedirected;

        // --sudo: re-launch with UAC elevation if not already admin.
        // Strip flag before dispatch so the actual command runs normally once elevated.
        // Eye + --수두: gracefully stop current non-admin Eye first so admin Eye can grab the mutex.
        if (args.Contains("--sudo"))
        {
            args = args.Where(a => a != "--sudo").ToArray();
            if (!IsElevated())
            {
                bool isEyeRelaunch = args.Length > 0 && args[0].Equals("eye", StringComparison.OrdinalIgnoreCase);

                if (isEyeRelaunch)
                {
                    // eye --sudo: stop current Eye so admin Eye can grab the mutex, then launch via runas.
                    // Admin Eye = full daemon, not just proxy — no choice but runas here.
                    Console.WriteLine("[SUDO] Stopping current Eye before admin re-launch...");
                    foreach (var proc in System.Diagnostics.Process.GetProcessesByName("wkappbot-core"))
                    {
                        try { if (proc.Id != System.Environment.ProcessId) proc.Kill(entireProcessTree: false); }
                        catch { }
                    }
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("[SUDO] Eye stopped — launching admin Eye...");

                    var quotedArgs2 = string.Join(" ", args.Select(a => a.Contains(' ') ? $"\"{a}\"" : a));
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = Environment.ProcessPath ?? exePath,
                            Arguments = quotedArgs2,
                            Verb = "runas",
                            UseShellExecute = true,
                            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        });
                        return 0; // Eye는 데몬 — 기다리지 않음
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[SUDO] UAC cancelled or failed: {ex.Message}");
                        return 1;
                    }
                }

                // Non-eye --sudo: route via admin Eye proxy (no new window!).
                // Ensure admin Eye is running first, then delegate via pipe.
                Console.WriteLine("[SUDO] Routing via admin Eye proxy (no new window)...");
                if (!ElevatedEyeClient.IsAvailable())
                {
                    if (!ElevationHelper.LaunchElevatedEye())
                    {
                        Console.Error.WriteLine("[SUDO] Failed to launch admin Eye proxy");
                        return 1;
                    }
                }

                var exit = ElevatedEyeClient.ExecuteViaProxy(args[0], args.Skip(1).ToArray());
                if (exit == -1)
                {
                    // Retry once — pipe may have appeared but server wasn't listening yet
                    Console.Error.WriteLine("[SUDO] Proxy communication failed — retrying in 500ms...");
                    Thread.Sleep(500);
                    exit = ElevatedEyeClient.ExecuteViaProxy(args[0], args.Skip(1).ToArray());
                }
                if (exit == -1)
                {
                    Console.Error.WriteLine("[SUDO] Admin Eye proxy communication failed");
                    return 1;
                }
                return exit;
            }
            // Already elevated — proceed normally (flag already stripped)
        }

        // --read-only: global flag for UIA read-only mode (strip from args before dispatch)
        if (args.Contains("--read-only"))
        {
            ReadOnlyMode = true;
            args = args.Where(a => a != "--read-only").ToArray();
        }

        // Grep-mode: logcat family (logcat/grap/grep) with stdout redirected (no watch flags).
        // result=$(grap pattern *.txt) must work like grep: matches to stdout, diagnostics to stderr.
        // Detected here (before TeeWriter) so TeeWriter can echo to stderr instead of stdout.
        GrepModeActive = Console.IsOutputRedirected
            && args.Length > 0 && args[0].ToLowerInvariant() == "logcat"
            && !args.Any(a => a is "--past" or "--follow" or "-f" or "--timeout")
            && !args.Any(a => a is "--help" or "-h");  // help output must reach stdout

        // Preserve original stdout for match output in grep-mode (TeeWriter replaces Console.Out below).
        OriginalStdout = Console.Out;

        // RunningInEye: skip Console.SetOut — log tee is handled by EyeCmdPipeServer (per-command, parallel-safe)
        // Grep-mode: echo diagnostics to stderr so stdout contains only match lines (grep-compat).
        // Skip TeeWriter for grap/grep fast-exit: writing log file on W:/ (SMB) leaves kernel-level
        // pending I/O that blocks TerminateProcess (STILL_ACTIVE) for ~27s (SMB cancel timeout).
        TeeTextWriter? tee = (RunningInEye || _fastExitAfterCommand) ? null : new TeeTextWriter(GrepModeActive ? Console.Error : Console.Out, logFile, oldSubDir: oldSubDir);
        _activeTee = tee; // expose for ProcessExit hook
        // Wrap tee in ThreadRoutingWriter so EyeCmdPipeServer.Route() can redirect per-command output.
        // Without this, Console.WriteLine always goes to the global Eye tee, bypassing AsyncLocal routing.
        if (tee != null) Console.SetOut(new ThreadRoutingWriter(tee));
        // Print log path early — so if the caller times out, they know where to tail the live log.
        if (tee != null && !GrepModeActive && !QuietFindOutput)
            Console.Error.WriteLine($"[LOG] {logFile}");
        // No TeeWriter yet: either RunningInEye (Eye-internal route OR spawned worker) or fast-exit.
        // Rule: if wkappbot.exe Launcher is not in the parent process chain, always create a stderr log.
        // Eye-internal routes (EyeCmdPipeServer.RunInEye in-process): same PID as Eye → Launcher IS
        //   ancestor of Eye → skip. All other fresh processes without Launcher ancestor → create log.
        if (tee == null && !_fastExitAfterCommand && !IsLauncherInParentChain())
        {
            try
            {
                var workerLog = new System.IO.StreamWriter(logFile, append: false, System.Text.Encoding.UTF8) { AutoFlush = true };
                Console.SetError(new WorkerStderrTeeWriter(Console.Error, workerLog));
                Console.Error.WriteLine($"[LOG] {logFile}");
            }
            catch { /* non-fatal — logging best-effort */ }
        }
        // LAUNCH identity: callerCwd + callerHwnd → logged by TeeWriter for post-mortem analysis
        {
            var launchCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var launchHwnd = EyeCmdPipeServer.CallerHwnd.Value;
            var launchCmd = string.Join(" ", args);
            if (!QuietFindOutput)
                Console.Error.WriteLine($"[LAUNCH:CORE] cwd={launchCwd} hwnd=0x{launchHwnd:X} cmd={launchCmd}");
        }
        // For grap/grep fast-exit: Launcher writes output path to WKAPPBOT_RELAY_FILE.
        // Core redirects Console.Out to that file. FastExit signals WKAPPBOT_RELAY_EVENT (EventWaitHandle)
        // after flushing — Launcher reads the file while Core is still alive (no 27s AV/SMB delay),
        // then signals WKAPPBOT_RELAY_READ_EVENT so Core can proceed to TerminateProcess.
        RelayFilePath = _fastExitAfterCommand ? Environment.GetEnvironmentVariable("WKAPPBOT_RELAY_FILE") : null;
        if (RelayFilePath != null)
        {
            try
            {
                var relayStream = new System.IO.FileStream(RelayFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Read);
                var _relayWriter = new System.IO.StreamWriter(relayStream, Console.OutputEncoding, bufferSize: 4096, leaveOpen: false) { AutoFlush = false };
                Console.SetOut(_relayWriter);
                OriginalStdout = _relayWriter;
            }
            catch { RelayFilePath = null; /* fallback to normal stdout */ }
        }
        if (_fastExitAfterCommand) SetupSyncStdout();
        prof("TeeWriter ready");

        // ── Crash handler: dump stack trace to log, DON'T move to old/ (crash evidence) ──
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            try
            {
                var ex = e.ExceptionObject as Exception;
                var crashMsg = $"\n[CRASH] Unhandled exception — process terminating\n" +
                               $"[CRASH] {ex?.GetType().FullName}: {ex?.Message}\n" +
                               $"[CRASH] {ex?.StackTrace}\n";
                if (ex?.InnerException != null)
                    crashMsg += $"[CRASH] Inner: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}\n" +
                                $"[CRASH] Inner stack: {ex.InnerException.StackTrace}\n";
                // Write to tee (goes to both console + file)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(crashMsg);
                Console.ResetColor();
                // Bug suggest: Slack + suggestions.jsonl via shared suggest command
                try
                {
                    var stack = ex?.StackTrace?.Length > 300 ? ex.StackTrace[..300] + "..." : ex?.StackTrace;
                    var logRef = tee?.LogPath != null ? $"\nlog: {Path.GetFileName(tee.LogPath)}" : "";
                    RunSuggestCommand($"[CRASH] {ex?.GetType().Name}: {ex?.Message}{logRef}\n{stack}");
                }
                catch { }

                // Flush and close file WITHOUT moving to old/ — crash log stays in logs/
                if (tee != null) Console.SetOut(tee.OriginalConsole);
                tee?.ForceCloseWithoutMove();
                Console.Error.WriteLine($"[CRASH] Log saved (not moved to old/): {tee?.LogPath}");
            }
            catch { /* last resort — at least don't mask the original crash */ }
        };

        int exitCode = 1;
        bool _skipTick = true; // default skip; set to false once command is known
        System.Threading.Timer? timeoutTimer = null;
        try {
            // Policy broadcast only on Eye spawn (not every CLI command)
            // This prevents stdout pollution for ask/web/other commands.
            
            if (args.Length == 0)
            {
                prof("PrintUsage (no-args)");
                PrintUsage();
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();

            // [BUG-AUTO] hooks are wired below; AutoBugReport() is a class-level static method.

            // [FL] Chrome focus theft -> focusless warning overlay + auto bug report
            WKAppBot.WebBot.ChromeLauncher.OnFocusTheft ??= (chromeHwnd, prevFgHwnd) =>
            {
                FocuslessWarningOverlay.Show(chromeHwnd, "Chrome focus theft detected; restoring focusless warning overlay.", "chrome");
                AutoBugReport($"Chrome focus theft: chrome=0x{chromeHwnd:X} prevFg=0x{prevFgHwnd:X}");
            };

            // [FOCUSSTEALER] UIA action stole focus -> stamp prop + auto-record knowhow + auto bug report
            ActionApi.OnFocusStealer ??= (rootHwnd, action) =>
            {
                AppendFocusStealerKnowhow(rootHwnd, action);
                FocuslessWarningOverlay.Show(rootHwnd, $"UIA {action} stole focus; yielding and auto-recording knowhow.", null);
                AutoBugReport($"UIA focus steal: action={action} hwnd=0x{rootHwnd:X}");
            };

            // [CDP-FALLBACK] Auto-suggest when the CDP caller has no fallback (Eye pipe -> zero spawn)
            WKAppBot.WebBot.CdpClient.OnFallbackSuggest ??= (text) =>
            {
                EyeCmdPipeServer.DispatchBg(["suggest", "[CDP-FALLBACK] " + text]);
            };

            // Global option: disable zoom overlay -> intentionally obnoxious name to discourage use
            if (restArgs.Any(a => a == "--i-dont-want-to-see-the-zoom-magnifier-overlay"))
            {
                ActionApi.ZoomEnabled = false;
                restArgs = restArgs.Where(a => a != "--i-dont-want-to-see-the-zoom-magnifier-overlay").ToArray();
            }

            // Global option: --timeout <duration>.
            // Supported forms: 30=30s, 1.5s=1.5s, 2m=2min, 500ms=500ms, 1h=1h.
            // The timeout only requests a graceful stop. Long-running commands should finish
            // the current step and then exit cleanly.
            for (int ti = 0; ti < restArgs.Length; ti++)
            {
                if (restArgs[ti] == "--timeout" && ti + 1 < restArgs.Length)
                {
                    var dur = ParseDuration(restArgs[ti + 1]);
                    restArgs = restArgs.Take(ti).Concat(restArgs.Skip(ti + 2)).ToArray();
                    if (dur > TimeSpan.Zero)
                    {
                        var timeoutMs = (long)dur.TotalMilliseconds;
                        timeoutTimer = new System.Threading.Timer(_ =>
                        {
                            GlobalTimeoutRequested = true;
                            Console.Error.WriteLine($"[TIMEOUT] {dur} elapsed; requesting graceful shutdown");
                            try { Console.Out.Flush(); Console.Error.Flush(); } catch { }
                        }, null, timeoutMs, Timeout.Infinite);
                    }
                    break;
                }
            }

            prof($"command={command}");

            // Global Eye tick for multi-parent monitoring.
            // Skip for grap/grep because EmitEyeTick can leave pending I/O and slow exit.
            // Skip for fast-exit aliases and Eye pipe commands.
            bool isFileCommand = command == "file" || command.StartsWith("file-", StringComparison.Ordinal);
            _skipTick = _fastExitAfterCommand || RunningInEye || isFileCommand;
            if (!_skipTick && !QuietFindOutput)
            {
                try { EmitEyeTick(command, cmdTag, "start"); } catch { }
                if (!GrepModeActive && !IsPipeMode) Console.Error.WriteLine(string.Join(" ", Environment.GetCommandLineArgs()));
                try { EmitEyeTick(command, cmdTag, "step:1/3:prepare"); } catch { }
                try
                {
                    var promptPreview = BuildPromptPreview(command, restArgs);
                    if (!string.IsNullOrWhiteSpace(promptPreview))
                        EmitEyeTick(command, cmdTag, $"prompt:{promptPreview}");
                }
                catch { }
            }

            // Auto-launch AppBotEye for all commands except help and eye commands.
            // The eye command itself is excluded so the auto-launch path does not cascade.
            // ThreadPool dispatch keeps the launch fire-and-forget and avoids blocking startup.
            var isEyeCommand = command == "eye";
            // _fastExitAfterCommand (grap/grep alias): skip Eye spawn so the spawned process
            // does not inherit the stdout pipe write end and block the Launcher relay task.
            var isMcpCommand = command == "mcp";
            // kill/close subcommands may target Eye itself, so do not auto-launch Eye first.
            var subCmd = restArgs.Length > 0 ? restArgs[0].ToLowerInvariant() : "";
            bool isKillOrClose = command == "a11y" && subCmd is "kill" or "close";
            bool isHackWorker = command == "a11y" && subCmd.StartsWith("hack-", StringComparison.OrdinalIgnoreCase);
            // Exclusions: eye, kill/close, mcp (stdio), grap/grep (fast exit), hack-* workers.
            var isExcluded = isEyeCommand || isMcpCommand || isKillOrClose || isHackWorker || _fastExitAfterCommand;
            if (!isExcluded && !RunningInEye)
            {
                ThreadPool.QueueUserWorkItem(_ => { try { LaunchAppBotEyeIfNeeded(); } catch { } });
            }

            if (TryPrintCommandHelp(command, restArgs)) { exitCode = 0; return 0; }
            if (TryRunRegression(command, restArgs)) { exitCode = 0; return 0; }

            if (!_fastExitAfterCommand) try { EmitEyeTick(command, cmdTag, "step:2/3:dispatch"); } catch { }
            if (!QuietFindOutput && !GrepModeActive && !GrapMode && !IsPipeMode) try { Console.Error.WriteLine("[ACT] cmd=" + command + " args='" + string.Join(" ", restArgs) + "'"); } catch { }
            prof("dispatch");

            // ErrorScope captures stderr with timestamps by default.
            // --stderr bypasses ErrorScope and shows stderr in real time for debugging.
            // Success: clean output. Error: timestamped error log via AppBotExit.
            // Errors are always logged to the TeeWriter log file.
            // ErrorScope is only suppressed when stderr is already redirected to the console.
            // Piped processes (MCP, Eye, pipe) pass stderr through immediately.
            // Redirected stderr stays real-time, similar to --stderr.
            bool isConsoleDirect = !IsPipeMode && !RunningInEye && !IsMcpMode && !Console.IsErrorRedirected;
            // eye/agent/tick: diagnostic-heavy commands that legitimately write to stderr, so skip ErrorScope.
            bool isErrScopeExcluded = command is "eye" or "agent" or "tick";
            using var _errScope = (isConsoleDirect && !isErrScopeExcluded) ? ErrorScope.Begin() : null;

            exitCode = command switch
            {
                // Primary: a11y universal interface (busybox: a11y.exe symlink)
                "a11y" => A11yCommand(restArgs),
                // Core
                "run" => RunCommand(restArgs),
                "find" => FindCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "windows" => WindowsCommand(restArgs),
                "ocr" => OcrCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "ask" => AskCommand(restArgs),
                "agent" => AgentCommand(restArgs),
                "model" => ModelCommand(restArgs),
                "logcat" => LogcatCommand(restArgs),
                "eye" => AppBotEyeCommand(restArgs),
                "slack" => SlackCommand(restArgs),
                "web" => WebCommand(restArgs),
                "file" => FileCommand(restArgs),
                "code" => EditorAliasCommand("code", restArgs),
                "vscode" => EditorAliasCommand("vscode", restArgs),
                // file-* hyphenated aliases: "file-edit old new f.cs" → FileCommand(["edit","old","new","f.cs"])
                "file-edit" => FileCommand(new[] { "edit" }.Concat(restArgs).ToArray()),
                "file-open" => FileCommand(new[] { "open" }.Concat(restArgs).ToArray()),
                "file-read" => FileCommand(new[] { "read" }.Concat(restArgs).ToArray()),
                "file-grep" => FileCommand(new[] { "grep" }.Concat(restArgs).ToArray()),
                "file-glob" => FileCommand(new[] { "glob" }.Concat(restArgs).ToArray()),
                // json-grep removed — use "grap ... --json" instead
                "mcp" => McpCommand(restArgs), // fallback (normally caught in Main early path)
                // Web: use "web <subcommand>" or unified a11y for web views
                // Automation
                "do" => DoCommand(restArgs),
                "input" => InputCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "dismiss" => DismissCommand(restArgs),
                "win-click" => WinClickCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "tab-select" => TabSelectCommand(restArgs),
                "cond-add" => CondAddCommand(restArgs),
                // Inspection
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "snapshot" => SnapshotCommand(restArgs),
                "readiness" => ReadinessCommand(restArgs),
                "uia-test" => UiaTestCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "toolbar-ocr" => ToolbarOcrCommand(restArgs),
                "titlebar" => TitlebarCommand(restArgs),
                // Analysis & Testing
                "validate" => ValidateCommand(restArgs),
                "chart-analyze" => ChartAnalyzeCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "tooltip-probe" => TooltipProbeCommand(restArgs),
                // Utility
                "speak" => SpeakCommand(restArgs),
                "whisper" => WhisperCommand(restArgs),
                "newchat" => NewChatCommand(restArgs),
                "analyze-hack" => AnalyzeHackCommand(restArgs),
                "screensaver" => ScreenSaverStandaloneCommand(),
                "whisper-ring" => WhisperRingStandaloneCommand(restArgs),
                "prompt"  => PromptCommand(restArgs),
                "schedule" => ScheduleCommand(restArgs),
                "dashboard" => DashboardCommand(restArgs),
                "knowhow" => KnowhowCommand(restArgs),
                "skill" => SkillCommand(restArgs),
                "win-move" => WindowMoveCommand(restArgs),
                "screen" => ScreenCommand(restArgs),
                "clipboard" => ClipboardCommand(restArgs),
                "claude-usage" => A11yClaudeUsage(),
                "claude-proxy" => ClaudeProxyCommand(restArgs),
                "enc-test" => EncTestCommand(),
                "suggest" => SuggestCommand(restArgs),
                "gc" => GcCommand(restArgs),
                "hotswap" => HotSwapCommand(restArgs),
                "zoom-demo" => ZoomDemoCommand(restArgs),
                "tick" => TickCommand(restArgs),
                // External
                "kiwoom" => KiwoomCommand(restArgs),
                "com" => ComCommand(restArgs),
                "telegram" => TelegramCommand(restArgs),
                "stock-scan" => StockScanCommand(restArgs),
                "tree-select" => TreeSelectCommand(restArgs),
                "prompt-test" => PromptTestCommand(restArgs),
                "prompt-probe" => PromptProbeCommand(restArgs),
                "claude-detect" => ClaudeDetectCommand(restArgs),
                "find-prompts" => FindPromptsCommand(restArgs),
                "msaa-probe" => MsaaProbeCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                "--version" or "version" => PrintVersion(),
                _ when command.StartsWith("kro-trial-", StringComparison.OrdinalIgnoreCase) => KroTrialSpecialCommand(command, restArgs),
                _ => Error($"Unknown command: {command}")
            };

            try
            {
                if (!GrapMode && !IsPipeMode)
                {
                    if (exitCode == 0) Console.Error.WriteLine($"[ACT] result=ok cmd={command}");
                    else Console.Error.WriteLine($"[FALLBACK] result=fail code={exitCode} cmd={command}");
                }
            }
            catch { }

            // Auto snapshot + profile-based experience DB recording on A11Y action failure
            try
            {
                if (exitCode != 0)
                {
                    var a11yFailCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                    {
                        "input", "click", "do", "inspect", "dialog-click"
                    };
                    if (a11yFailCommands.Contains(command) && restArgs.Length > 0)
                    {
                        var winTitle = restArgs[0];
                        if (!string.IsNullOrWhiteSpace(winTitle))
                        {
                            Console.Error.WriteLine($"[FALLBACK] auto snapshot+experience trigger (cmd={command})");
                            Console.WriteLine($"[A11Y] unavailable (cmd={command}, reason=action-failed, text=(none), role=(none), action=(none))");
                            var cidArg = "";
                            for (int i = 0; i < restArgs.Length - 1; i++)
                            {
                                if (string.Equals(restArgs[i], "--cid", StringComparison.OrdinalIgnoreCase))
                                {
                                    cidArg = restArgs[i + 1];
                                    break;
                                }
                            }

                            // 1. Snapshot (UIA tree + screenshot + OCR + per-control learning)
                            if (!string.IsNullOrWhiteSpace(cidArg))
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2", "--cid", cidArg });
                            else
                                SnapshotCommand(new[] { winTitle, "--tag", $"a11y_fail_{command}", "--depth", "2" });

                            // 2. Profile-based action log (screenshot ring buffer + JSONL in control/form folder)
                            var stdAction = ResolveStandardAction(command, restArgs);
                            WriteA11yFailToProfileDb(winTitle, command, stdAction, cidArg, "action-failed");

                            // 3. Legacy flat file record (backward compat, will be phased out)
                            WriteA11yFailExperienceRecord(winTitle, command, stdAction, cidArg, "action-failed", "(none)", "(none)", "(none)");
                        }
                    }
                }
            }
            catch { }

            // AppBotExit: ErrorScope flush + stdout-blank guard + DoRequiredExitCleanup via finally.
            exitCode = AppBotExit(exitCode);
            _exitCode = exitCode;
            return exitCode;
        }
        catch (Exception ex)
        {
            var errorMsg = $"Error: {ex.GetType().Name}: {ex.Message}";
            try { Console.ForegroundColor = ConsoleColor.Red; } catch { }
            // Console.Error broken in Git Bash (mintty pipes) — write to stdout too
            try { Console.Error.WriteLine(errorMsg); } catch { }
            try { Console.WriteLine(errorMsg); } catch { }
            try { Console.ResetColor(); } catch { }
            return 1;
        }
        finally
        {
            if (!_skipTick)
            try
            {
                var cmd = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
                if (exitCode == 0) EmitEyeTick(cmd, cmdTag, "done:ok");
                else EmitEyeTick(cmd, cmdTag, "step:3/3:error");
                EmitEyeTick(cmd, cmdTag, $"end:{exitCode}");
            }
            catch { }
            // DoRequiredExitCleanup: errors.jsonl write + tee.Dispose (log move) + WriteExitFile.
            // Idempotent — ProcessExit hook calls this too as fallback for abnormal exits.
            // fastExitAfterCommand (grap/grep): skip signalLauncher here; FastExit handles it.
            if (!_fastExitAfterCommand)
                DoRequiredExitCleanup(exitCode);
            else
            {
                // grap/grep: still need errors.jsonl write; tee is null here (skipped for fast-exit).
                // FastExit below handles WriteExitFile + TerminateProcess.
                try { if (tee != null) { tee.ExitCode = exitCode; tee.WriteErrorRecordNow(); } } catch { }
            }

            var logPath = tee?.LogPath;
            if (logPath != null) Console.Error.WriteLine($"Log saved: {logPath}  [{_mainStarted.Elapsed:m\\:ss\\.fff}  {DateTime.Now:HH:mm:ss}]");
            timeoutTimer?.Dispose();
            // grap/grep path still uses FastExit (relay file + TerminateProcess).
            if (_fastExitAfterCommand)
                FastExit(exitCode);
        }
    }

    /// <summary>
    /// Compute (cmdTag, oldSubDir) for log filename and old-dir routing.
    /// cmdTag  → embedded in the log filename  (e.g. "web-fetch-github.com")
    /// oldSubDir → used as the old-{subDir}/ folder name (same value, sans redundant prefix)
    /// Rules:
    ///   a11y &lt;action&gt;       → tag=action,          dir=action         (a11y is a namespace)
    ///   web fetch/read &lt;url&gt; → tag=web-fetch-{host}, dir=web-{host}
    ///   web search           → tag=web-search,       dir=web-search
    ///   ask/agent &lt;ai&gt;       → tag=ask-gpt etc,      dir=ask-gpt
    ///   slack/file/…        → tag=cmd-sub,           dir=cmd(-sub)
    ///   others              → tag=cmd,               dir=cmd
    /// </summary>
    static (string cmdTag, string oldSubDir) ComputeCmdTagAndSubDir(string[] args)
    {
        var dir = ComputeOldSubDir(args);
        return (dir.Replace(" ", "-"), dir);
    }

    /// <summary>Public accessor for EyeCmdPipeServer — computes "old {cmd} {sub}" directory name.</summary>
    internal static string ComputeOldSubDirPublic(string[] args) => ComputeOldSubDir(args);

    static string ComputeOldSubDir(string[] args)
    {
        if (args.Length == 0) return "noargs";
        var cmd = args[0].ToLowerInvariant();
        var sub = args.Length > 1 ? args[1].ToLowerInvariant() : "";

        switch (cmd)
        {
            case "a11y":
            {
                var action = sub.Length > 0 ? sub : "a11y";
                var grap   = args.Length > 2 ? args[2] : "";
                var win    = ExtractMainWindowFromGrap(grap);
                return win != null ? $"a11y {action} {win}" : $"a11y {action}";
            }

            case "web":
            {
                if (sub is "fetch" or "read")
                {
                    var url  = args.Length > 2 ? args[2] : "";
                    var host = ExtractUrlHost(url);
                    return host != null ? $"web {sub} {host}" : $"web {sub}";
                }
                return sub.Length > 0 ? $"web {sub}" : "web";
            }

            case "ask":
            case "agent":
            case "slack":
            case "file":
            case "schedule":
            case "knowhow":
            case "skill":
                return sub.Length > 0 ? $"{cmd} {sub}" : cmd;

            default:
            {
                // Known commands → use as-is. Unknown/typo → "unknown" to prevent folder explosion.
                var known = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "eye", "run", "find", "inspect", "windows", "ocr", "input", "click", "dismiss",
                    "win-click", "dialog-click", "tab-select", "cond-add", "focus", "watch", "snapshot",
                    "readiness", "uia-test", "form-dump", "toolbar-ocr", "titlebar", "validate",
                    "chart-analyze", "hts-stress", "tooltip-probe", "speak", "whisper", "newchat",
                    "analyze-hack", "screensaver", "whisper-ring", "prompt", "dashboard", "win-move",
                    "screen", "clipboard", "claude-usage", "claude-proxy", "enc-test", "suggest", "gc", "hotswap", "tick",
                    "kiwoom", "com", "telegram", "stock-scan", "logcat", "grep", "grap",
                    "help", "--help", "-h", "mcp", "claude-detect", "noise", "capture",
                    "elevate", "webbot", "code", "vscode", "version", "--version"
                };
                return known.Contains(cmd) ? cmd : "unknown";
            }
        }
    }

    /// <summary>Extract main window name from grap pattern for log folder naming.
    /// "영웅문#실시간계좌"          → "영웅문"
    /// "*메모장*"                   → "메모장"
    /// "adb://Fold5/*heromts*#..."  → "adb Fold5 heromts"
    /// "regex:..."                  → null</summary>
    static string? ExtractMainWindowFromGrap(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) return null;
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase)) return null;

        string main;
        if (pattern.StartsWith("adb://", StringComparison.OrdinalIgnoreCase))
        {
            // adb://{device}/{winname}#scope → "adb {device} {winname}"
            var rest = pattern["adb://".Length..];
            var parts = rest.Split('/');
            var win = parts.Length > 1 ? parts[1].Split('#')[0].Split(';')[0].Trim() : "";
            win = win.Replace("*", "").Replace("?", "").Trim();
            main = win.Length > 0 ? $"adb {win}" : "adb";
        }
        else
        {
            // Cut at first #/;/ — take main window token only
            main = pattern.Split('#', '/', ';')[0].Trim();
            // Strip wildcards/spaces/hyphens → compact keyword ("*영웅문*" → "영웅문", "*Visual Studio*" → "VisualStudio")
            main = Regex.Replace(main, @"[*?\s\-]+", "");
        }

        // Strip filesystem-unsafe chars
        main = string.Concat(main.Where(c => !Path.GetInvalidFileNameChars().Contains(c))).Trim();
        return main.Length >= 2 ? main : null;
    }

    static string? ExtractUrlHost(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        try
        {
            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                url = "https://" + url;
            var host = new Uri(url).Host.ToLowerInvariant();
            return string.IsNullOrEmpty(host) ? null : host;
        }
        catch { return null; }
    }

    static void RotateOldLogs(string logDir, int staleHours = 24)
    {
        try
        {
            var _rsw = System.Diagnostics.Stopwatch.StartNew();
            bool _rprof = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
            void rprof(string s) { if (_rprof) Console.Error.WriteLine($"[ROTATE] {_rsw.ElapsedMilliseconds}ms {s}"); }

            var now = DateTime.UtcNow;

            // ── Phase 1: move stale live logs → old-{subkey}/ ──────────────────────
            rprof("GetFiles start");
            var files = Directory
                .GetFiles(logDir, "*.out-*.log", SearchOption.TopDirectoryOnly)
                .Select(p => new FileInfo(p))
                .OrderBy(f => f.CreationTimeUtc)
                .ToList();
            rprof($"GetFiles done count={files.Count}");

            foreach (var f in files)
            {
                if ((now - f.CreationTimeUtc).TotalHours < staleHours)
                    continue;
                if (!TryGetPidFromLogName(f.Name, out var pid))
                    continue;
                rprof($"IsProcessAlive pid={pid} file={f.Name}");
                if (IsProcessAlive(pid))
                    continue;
                rprof($"IsProcessAlive done — moving {f.Name}");

                try
                {
                    var destDir = Path.Combine(logDir, $"old {OldSubDirFromCmdTag(f.Name)}");
                    Directory.CreateDirectory(destDir);
                    var dest = Path.Combine(destDir, f.Name);
                    if (File.Exists(dest))
                    {
                        var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        dest = Path.Combine(destDir, $"{Path.GetFileNameWithoutExtension(f.Name)}.{stamp}{f.Extension}");
                    }
                    File.Move(f.FullName, dest);
                    rprof($"moved → {dest}");
                }
                catch { }
            }

            rprof("done");
        }
        catch
        {
            // Best-effort log housekeeping only.
        }
    }

    // Extract old-dir subkey from log filename: exe.out-YYYYMMDD_HHMMSS.{cmdTag}.pid=N.txt
    static string OldSubDirFromCmdTag(string fileName)
    {
        var pidIdx = fileName.LastIndexOf(".pid=", StringComparison.OrdinalIgnoreCase);
        var outIdx = fileName.IndexOf(".out-", StringComparison.OrdinalIgnoreCase);
        if (pidIdx < 0 || outIdx < 0) return "misc";
        var afterTs = outIdx + ".out-".Length + 16; // "YYYYMMDD_HHMMSS."
        if (afterTs >= pidIdx) return "misc";
        var tag = fileName[afterTs..pidIdx].ToLowerInvariant();
        if (tag.StartsWith("slack-"))     return "slack " + tag["slack-".Length..];
        if (tag.StartsWith("file-"))      return "file " + tag["file-".Length..];
        if (tag.StartsWith("web-fetch-")) return "web fetch " + tag["web-fetch-".Length..];
        if (tag.StartsWith("web-read-"))  return "web read " + tag["web-read-".Length..];
        if (tag.StartsWith("web-"))       return "web " + tag["web-".Length..];
        if (tag.StartsWith("ask-"))       return "ask " + tag["ask-".Length..];
        if (tag.StartsWith("agent-"))     return "agent " + tag["agent-".Length..];
        return string.IsNullOrEmpty(tag) ? "misc" : tag;
    }

    static bool TryGetPidFromLogName(string fileName, out int pid)
    {
        pid = 0;
        const string key = ".pid=";
        var idx = fileName.LastIndexOf(key, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return false;
        idx += key.Length;

        var end = idx;
        while (end < fileName.Length && char.IsDigit(fileName[end])) end++;
        if (end <= idx) return false;

        return int.TryParse(fileName[idx..end], out pid);
    }

    static bool IsProcessAlive(int pid)
    {
        try
        {
            var p = System.Diagnostics.Process.GetProcessById(pid);
            return !p.HasExited;
        }
        catch
        {
            return false;
        }
    }

    static string BuildPromptPreview(string command, string[] restArgs)
    {
        if (restArgs == null || restArgs.Length == 0)
            return string.Empty;

        // Only surface raw prompt-like commands to reduce noise.
        var c = (command ?? "").ToLowerInvariant();
        if (c is not ("ask" or "agent" or "web" or "kiwoom" or "com" or "telegram" or "schedule" or "knowhow"))
            return string.Empty;

        var raw = string.Join(" ", restArgs).Trim();
        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;

        var first = raw.Split(new[] { '\r', '\n', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault()?.Trim() ?? raw;

        if (first.Length > 80)
            first = first[..80] + "...";

        return first;
    }

    sealed class EyeTick
    {
        public string Ts { get; set; } = DateTime.UtcNow.ToString("O");
        public int Pid { get; set; }
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public int HostPid { get; set; }
        public string HostName { get; set; } = "";
        public string HostTitle { get; set; } = "";
        public string Command { get; set; } = "";
        public string Tag { get; set; } = "";
        public string Status { get; set; } = "";
        public string Cwd { get; set; } = "";
        public string PromptHwnd { get; set; } = ""; // caller prompt window handle (session key)
        public string Args { get; set; } = "";  // command-line args (skip argv[0] exe path)
    }

    internal static string EyeTicksPath => Path.Combine(DataDir, "runtime", "eye_ticks.jsonl");

    static void EmitEyeTick(string command, string tag, string status)
    {
        try
        {
            if (QuietFindOutput) return;
            var runtimeDir = Path.Combine(DataDir, "runtime");
            Directory.CreateDirectory(runtimeDir);

            var pid = Environment.ProcessId;
            var ppid = GetParentPid(pid);
            var parentName = "unknown";
            if (ppid > 0)
            {
                try { parentName = System.Diagnostics.Process.GetProcessById(ppid).ProcessName; } catch { }
            }

            var (hostPid, hostName, hostTitle) = FindLogicalHost(pid, ppid);

            // Include args only on "start" to avoid repeating in every step/end entry
            var args = status == "start"
                ? string.Join(" ", Environment.GetCommandLineArgs().Skip(1))
                : "";

            var tick = new EyeTick
            {
                Pid = pid,
                ParentPid = ppid,
                ParentName = parentName,
                HostPid = hostPid,
                HostName = hostName,
                HostTitle = hostTitle,
                Command = command,
                Tag = tag,
                Status = status,
                Cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory,
                PromptHwnd = EyeCmdPipeServer.CallerHwnd.Value is IntPtr h && h != IntPtr.Zero
                    ? $"0x{h.ToInt64():X}" : "",
                Args = args,
            };

            File.AppendAllText(EyeTicksPath, JsonSerializer.Serialize(tick) + Environment.NewLine);
        }
        catch
        {
            // best effort
        }
    }

    /// <summary>
    /// Emit eye tick with auto-detected host from eye_ticks.jsonl.
    /// For use in PostPublish where process tree doesn't reach Claude Desktop.
    /// Scans recent ticks for most recent Claude/Code host and adopts that host PID.
    /// </summary>
    static void EmitEyeTickWithHost(string command, string tag, string status)
    {
        try
        {
            var runtimeDir = Path.Combine(DataDir, "runtime");
            Directory.CreateDirectory(runtimeDir);

            // Find most recent host from eye_ticks.jsonl (tail 8KB = ~last 30 ticks)
            int hostPid = 0;
            string hostName = "", hostTitle = "";
            try
            {
                if (File.Exists(EyeTicksPath))
                {
                    var lines = ReadTailBytes(EyeTicksPath, 8192);
                    // Scan in reverse — most recent first
                    for (int i = lines.Length - 1; i >= 0; i--)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;
                        var t = System.Text.Json.JsonSerializer.Deserialize<EyeTick>(lines[i]);
                        if (t?.HostPid > 0 && !string.IsNullOrWhiteSpace(t.HostTitle))
                        {
                            // Found a tick with a real host (e.g. claude, Code) — adopt it
                            hostPid = t.HostPid;
                            hostName = t.HostName ?? "";
                            hostTitle = t.HostTitle ?? "";
                            break;
                        }
                    }
                }
            }
            catch { }

            // Fallback to normal host detection if no host found in ticks
            if (hostPid <= 0)
            {
                var pid = Environment.ProcessId;
                var ppid = GetParentPid(pid);
                (hostPid, hostName, hostTitle) = FindLogicalHost(pid, ppid);
            }

            var tick = new EyeTick
            {
                Pid = Environment.ProcessId,
                ParentPid = GetParentPid(Environment.ProcessId),
                ParentName = "build",
                HostPid = hostPid,
                HostName = hostName,
                HostTitle = hostTitle,
                Command = command,
                Tag = tag,
                Status = status,
                Cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory,
            };

            File.AppendAllText(EyeTicksPath, JsonSerializer.Serialize(tick) + Environment.NewLine);
        }
        catch { }
    }

    /// <summary>Read tail bytes from a file and split into lines (shared read, no lock)</summary>
    static string[] ReadTailBytes(string path, int byteCount)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            if (fs.Length > byteCount) fs.Seek(-byteCount, SeekOrigin.End);
            using var sr = new StreamReader(fs);
            var text = sr.ReadToEnd();
            return text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }
        catch { return Array.Empty<string>(); }
    }

    /// <summary>
    /// Safe wrapper for Process.MainWindowTitle — times out after <paramref name="timeoutMs"/> ms.
    /// Process.MainWindowTitle calls GetWindowText (Win32) which sends WM_GETTEXT via SendMessage.
    /// For hung/not-responding windows this blocks for up to 30 seconds. This wrapper avoids that.
    /// </summary>
    static string GetMainWindowTitleSafe(System.Diagnostics.Process p, int timeoutMs = 150)
    {
        string? title = null;
        var t = new Thread(() => { try { title = p.MainWindowTitle; } catch { } }) { IsBackground = true };
        t.Start();
        t.Join(timeoutMs);
        return title ?? "";
    }

    static (int hostPid, string hostName, string hostTitle) FindLogicalHost(int selfPid, int directParentPid)
    {
        static bool IsShell(string n)
        {
            var x = (n ?? "").ToLowerInvariant();
            // Include build tools (dotnet, msbuild) so PostPublish ticks walk past them to Claude
            return x is "powershell" or "pwsh" or "cmd" or "conhost" or "node" or "wkappbot"
                or "dotnet" or "msbuild";
        }

        int cur = directParentPid;
        int depth = 0;
        while (cur > 0 && depth < 12)
        {
            depth++;
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(cur);
                var name = p.ProcessName ?? "unknown";
                var title = GetMainWindowTitleSafe(p);

                if (!IsShell(name) && !string.IsNullOrWhiteSpace(title))
                    return (cur, name, title);

                var next = GetParentPid(cur);
                if (next <= 0 || next == cur)
                    break;
                cur = next;
            }
            catch { break; }
        }

        if (directParentPid > 0)
        {
            try
            {
                var p = System.Diagnostics.Process.GetProcessById(directParentPid);
                return (directParentPid, p.ProcessName ?? "unknown", GetMainWindowTitleSafe(p));
            }
            catch { }
        }

        return (selfPid, "wkappbot", "");
    }

    // ── run ────────────────────────────────────────────────────

    static string ResolveStandardAction(string command, string[] restArgs)
    {
        return (command ?? "").ToLowerInvariant() switch
        {
            "input" => "SetValue",
            "click" => "Invoke",
            "do" => "Invoke",
            "dialog-click" => "Invoke",
            "inspect" => "Inspect",
            "snapshot" => "Snapshot",
            "scan" => "Scan",
            _ => "General"
        };
    }

    /// <summary>
    /// Write A11Y failure to profile-based ExperienceDb.
    /// Screenshot → ring buffer (log_0..log_8.png) + metadata → action_log.jsonl
    /// Stored under profiles/{name}_exp/form_{id}/logs/ or form_{id}/tree/.../logs/
    /// </summary>
    static void WriteA11yFailToProfileDb(string windowTitle, string command, string stdAction, string cidArg, string reason)
    {
        try
        {
            var windows = WKAppBot.Win32.Window.WindowFinder.FindWindows(windowTitle);
            if (windows.Count == 0) return;
            var win = windows[0];

            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
            string procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            // Find matching profile
            var profileStore = new WKAppBot.Win32.Window.ProfileStore();
            var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                ?? (!string.IsNullOrEmpty(procName) ? profileStore.FindByMatch("", procName) : null);

            if (profileMatch == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[A11Y] 프로파일 없음 — 경험DB 기록 건너뜀 (wkappbot scan --save 필요)");
                Console.ResetColor();
                return;
            }

            var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
            var expDb = new WKAppBot.Win32.Window.ExperienceDb(expDir);

            // Capture window screenshot for ring buffer
            System.Drawing.Bitmap? screenshot = null;
            try
            {
                screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
                if (screenshot != null && WKAppBot.Win32.Input.ScreenCapture.IsBlankBitmap(screenshot))
                {
                    screenshot.Dispose();
                    screenshot = null;
                }
            }
            catch { }

            // Build metadata JSON
            int? cid = int.TryParse(cidArg, out var c) ? c : null;
            var metadata = JsonSerializer.Serialize(new
            {
                ts = DateTime.Now.ToString("O"),
                cmd = command,
                action = stdAction,
                reason,
                window = win.Title,
                @class = win.ClassName,
                pid = (int)pid,
                cid,
                profile = profileMatch.Value.name
            });

            // Scan for active forms to find the right form to log under
            var scanResult = WKAppBot.Win32.Window.AppScanner.Scan(win.Handle);
            bool saved = false;

            if (cid.HasValue)
            {
                // Try to find which form contains this control
                foreach (var form in scanResult.Forms.Where(f => f.FormId != null && f.IsVisible))
                {
                    // Save to form-level log under action-name subfolder
                    expDb.SaveFormActionLog(form.FormId!, screenshot, metadata, actionName: stdAction);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[A11Y] 프로파일 경험DB 기록: profile={profileMatch.Value.name}, form={form.FormId}, action={stdAction}, cid={cid}");
                    Console.ResetColor();
                    saved = true;
                    break;
                }
            }

            if (!saved)
            {
                // Save to the first visible form, or a generic "unknown" form
                var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId != null && f.IsVisible);
                var formId = targetForm?.FormId ?? "unknown";
                expDb.SaveFormActionLog(formId, screenshot, metadata, actionName: stdAction);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[A11Y] 프로파일 경험DB 기록: profile={profileMatch.Value.name}, form={formId}, action={stdAction}");
                Console.ResetColor();
            }

            screenshot?.Dispose();

            // Also run QuickTouchControls to accumulate per-control experience
            try
            {
                var (forms, controls, screenshots2) = WKAppBot.Win32.Window.AppScanner.QuickTouchControls(scanResult, expDb);
                if (controls > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[A11Y] 추가 경험 학습: {forms} forms, {controls} controls, {screenshots2} new screenshots");
                    Console.ResetColor();
                }
            }
            catch { }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[A11Y] 프로파일 경험DB 기록 실패: {ex.Message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Show past failure history and knowhow hints for a form.
    /// Called when accessing a form that has previous action log entries in ExperienceDb.
    /// Shared across do/input/click commands.
    /// </summary>
    /// <summary>
    /// Show form-level experience hints.
    /// actionName이 있으면 knowhow-{action}.md 우선 방송, 없으면 knowhow.md 방송.
    /// </summary>
    static void ShowFormExperienceHints(ExperienceDb expDb, string formId, string? actionName = null)
    {
        try
        {
            var summary = expDb.GetFormActionLogSummary(formId);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] total logs {summary.TotalEntries}");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", screenshots {summary.ScreenshotCount}");
                Console.ResetColor();
            }

            // 액션 노하우 우선, 없으면 일반 노하우 방송
            string? knowhowPath = null;
            if (!string.IsNullOrEmpty(actionName))
                knowhowPath = expDb.GetFormActionKnowhowPath(formId, actionName);
            knowhowPath ??= expDb.GetFormKnowhowPath(formId);

            if (knowhowPath != null)
                ShowKnowhowBroadcast(knowhowPath);
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Show control-level experience hints.
    /// actionName이 있으면 knowhow-{action}.md 우선 방송, 없으면 knowhow.md 방송.
    /// 노하우 없으면 "여기에 노하우를 남겨주세요" 절대경로 안내.
    /// </summary>
    static void ShowControlExperienceHints(ExperienceDb expDb, string formId, int cid,
        string? actionName = null)
    {
        try
        {
            // Control-level log history
            var summary = expDb.GetControlActionLogSummary(formId, cid);
            if (summary != null && summary.TotalEntries > 0)
            {
                var breakdown = summary.ActionBreakdown.Count > 0
                    ? string.Join(", ", summary.ActionBreakdown.Select(kv => $"{kv.Key}:{kv.Value}"))
                    : "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  [EXP] cid={cid}: total logs {summary.TotalEntries}");
                if (!string.IsNullOrEmpty(breakdown))
                    Console.Write($" ({breakdown})");
                Console.WriteLine($", screenshots {summary.ScreenshotCount}");
                Console.ResetColor();
            }

            // Try to resolve knowhow from form/action first, then fall back to form-level knowhow.
            bool found = false;
            if (!string.IsNullOrEmpty(actionName))
            {
                var (actFormPath, actCtrlPath) = expDb.GetActionKnowhowPaths(formId, cid, actionName);
                if (actCtrlPath != null) { ShowKnowhowBroadcast(actCtrlPath); found = true; }
                else if (actFormPath != null) { ShowKnowhowBroadcast(actFormPath); found = true; }
            }
            if (!found)
            {
                var (formPath, ctrlPath) = expDb.GetKnowhowPaths(formId, cid);
                if (ctrlPath != null) { ShowKnowhowBroadcast(ctrlPath); found = true; }
                else if (formPath != null) { ShowKnowhowBroadcast(formPath); found = true; }
            }

            // 노하우 없을 때: 노하우 파일 경로 안내 (미래 클롣이 발견하면 여기에 기록)
            if (!found && summary != null && summary.TotalEntries > 0)
            {
                var ctrlDir = expDb.GetControlDir(formId, cid, create: false);
                var suggestedFile = !string.IsNullOrEmpty(actionName)
                    ? $"knowhow-{actionName.ToLowerInvariant()}.md"
                    : "knowhow.md";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Error.WriteLine($"  [KNOWHOW] 노하우 발견 시 기록 → {Path.Combine(ctrlDir, suggestedFile)}");
                Console.ResetColor();
            }
        }
        catch { /* best-effort */ }
    }

    /// <summary>
    /// Knowhow 방송: 파일의 첫 문단(개요/목차)을 출력.
    /// 첫 문단 = 파일 시작부터 첫 번째 빈 줄(\n\n) 전까지의 비어있지 않은 줄들.
    /// ## 헤더가 있으면 첫 헤더를 타이틀로, 그 아래 첫 문단을 내용으로 표시.
    /// 섹션 수도 함께 표시 → 미래 클롣이 "더 있다"는 것을 인지.
    /// </summary>
    static void ShowKnowhowBroadcast(string knowhowPath, string tag = "KNOWHOW")
    {
        try
        {
            var allLines = File.ReadAllLines(knowhowPath, System.Text.Encoding.UTF8);
            if (allLines.Length == 0) return;

            // ## 헤더 수 카운트 (섹션 수 표시용)
            int sectionCount = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].TrimStart().StartsWith("## "))
                    sectionCount++;
            }

            // 첫 헤더 찾기 (타이틀)
            string title = "";
            int contentStart = 0;
            for (int i = 0; i < allLines.Length; i++)
            {
                var trimmed = allLines[i].TrimStart();
                if (trimmed.StartsWith("## "))
                {
                    title = trimmed.TrimStart('#').Trim();
                    contentStart = i + 1;
                    break;
                }
                // 헤더 없이 바로 내용 시작하는 경우
                if (!string.IsNullOrWhiteSpace(allLines[i]))
                {
                    contentStart = i;
                    break;
                }
            }

            // 첫 문단 추출: contentStart부터 빈 줄(\n\n) 또는 다음 ## 헤더까지
            var paragraph = new List<string>();
            bool started = false;
            for (int i = contentStart; i < allLines.Length; i++)
            {
                var line = allLines[i].TrimEnd();
                // 다음 ## 섹션이면 끝
                if (line.TrimStart().StartsWith("## "))
                    break;
                // 빈 줄이면 문단 끝 (내용 시작 후에만)
                if (started && string.IsNullOrWhiteSpace(line))
                    break;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    started = true;
                    paragraph.Add(line);
                }
            }

            if (paragraph.Count == 0 && string.IsNullOrEmpty(title)) return;

            // 출력: [KNOWHOW] (N sections) [title] paragraph...
            Console.ForegroundColor = ConsoleColor.Magenta;
            var countInfo = sectionCount > 1 ? $" ({sectionCount}癲?" : "";
            Console.Error.Write($"  [{tag}]{countInfo} ");
            Console.ResetColor();

            if (!string.IsNullOrEmpty(title))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Error.Write($"{title}: ");
                Console.ResetColor();
            }

            if (paragraph.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                // 줄 합치 + md 주석/마커 트림 + 연속 공백 정리 (토큰 절약)
                var text = string.Join(" ", paragraph.Select(l =>
                    l.TrimStart('-', '*', ' ', '\t', '`')));
                text = System.Text.RegularExpressions.Regex.Replace(text, @"\s{2,}", " ").Trim();
                if (text.Length > 200) text = text[..197] + "...";
                Console.Error.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                Console.Error.WriteLine();
            }
        }
        catch { /* best-effort */ }
    }

    static void WriteA11yFailExperienceRecord(string windowTitle, string command, string stdAction, string cidArg, string reason, string text, string role, string action)
    {
        try
        {
            var windows = WKAppBot.Win32.Window.WindowFinder.FindWindows(windowTitle);
            if (windows.Count == 0) return;
            var win = windows[0];

            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
            var procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

            var expDir = Path.Combine(DataDir, "experience", SanitizePathTokenForExp(procName), SanitizePathTokenForExp(win.ClassName));
            Directory.CreateDirectory(expDir);
            var actionToken = SanitizePathTokenForExp(string.IsNullOrWhiteSpace(stdAction) ? "General" : stdAction);
            var logPath = Path.Combine(expDir, $"a11y_fail_{actionToken}.jsonl");
            var knowhowPath = Path.Combine(expDir, "knowhow.md");

            int? cid = int.TryParse(cidArg, out var c) ? c : null;
            var rec = new
            {
                ts = DateTime.Now.ToString("O"),
                cmd = command,
                window = win.Title,
                @class = win.ClassName,
                pid,
                cid,
                reason,
                text,
                role,
                action
            };

            File.AppendAllText(logPath, JsonSerializer.Serialize(rec) + Environment.NewLine, Encoding.UTF8);
            Console.WriteLine($"[A11Y] 경험 기록 저장: {logPath}");
            Console.WriteLine($"[KNOWHOW] hint: {knowhowPath}");
        }
        catch { }
    }

    /// <summary>
    /// Compact window grap for # TARGET / hack-hover lines — copy-paste ready, noise-free.
    /// Rules: no hwnd/pid; browser→domain or file:'name'; non-browser→{proc,cls}; no title/url.
    /// </summary>
    internal static string BuildCompactWinGrap(IntPtr hwnd) => _BuildCompactWinGrapCore(hwnd);

    /// <summary>
    /// Inject hwnd:0x... into any compact grap string — shared helper for # FOCUS / # TARGET.
    /// Always produces a JSON5 object with hwnd as last field.
    /// </summary>
    internal static string InjectHwnd(string grap, IntPtr hwnd)
    {
        var hwndStr = $"hwnd:0x{hwnd.ToInt64():X8}";
        if (grap.StartsWith('{'))
            return grap.TrimEnd('}') + $",{hwndStr}}}";
        // bare form (file:'name', domain, etc.) → wrap as JSON5
        return $"{{{grap},{hwndStr}}}";
    }

    /// <summary>
    /// Build a fully-resolved target grap string for display output.
    /// Takes hwnd (+ optional pid to avoid extra lookup) and produces:
    ///   compact portable form (domain/proc/file) + hwnd injected.
    /// Use instead of BuildTargetJson5 wherever target window info is displayed.
    /// For search/verify, use BuildCompactWinGrap (no hwnd).
    /// </summary>
    internal static string BuildTargetGrap(IntPtr hwnd, uint pid = 0)
    {
        if (pid == 0) WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out pid);
        return InjectHwnd(BuildCompactWinGrap(hwnd), hwnd);
    }

    /// <summary>
    /// Build a target grap that includes the currently focused leaf's absolute UIA path when available.
    /// Falls back to the plain target grap if the focused leaf cannot be resolved.
    /// </summary>
    internal static string BuildTargetGrapWithFocusPath(IntPtr hwnd)
        => AppendFocusPath(BuildTargetGrap(hwnd), hwnd);

    /// <summary>
    /// Build a target grap from a verified window hit and append the focused leaf path.
    /// Preserves MatchedVia / MatchedSnippet on the base grap.
    /// </summary>
    internal static string BuildTargetGrapWithFocusPath(WKAppBot.Win32.Window.WindowInfo hit)
        => AppendFocusPath(BuildTargetGrap(hit), hit.Handle);

    /// <summary>Quote a grap expression so the whole token can be pasted into a shell as one argument.</summary>
    internal static string QuoteGrapExpression(string grap)
        => "\"" + grap.Replace("\"", "\\\"") + "\"";

    private static string AppendFocusPath(string target, IntPtr hwnd)
    {
        try
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return target;

            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, hwnd);
            if (focused == null) return target;

            var path = WKAppBot.Win32.Accessibility.GrapHelper.BuildAbsoluteTagPath(
                focused, uia.TreeWalkerFactory.GetRawViewWalker(), 15);
            if (string.IsNullOrWhiteSpace(path) || path == "?") return target;

            return $"{target}#{path}";
        }
        catch
        {
            return target;
        }
    }

    // Get the focused leaf element's XML tag for TARGET block display.
    // Returns empty string if leaf not found or UIA unavailable.
    internal static string GetFocusedLeafTag(IntPtr hwnd)
    {
        try
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return "";
            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, hwnd);
            if (focused == null) return "";
            return FormatLeafTag(focused);
        }
        catch { return ""; }
    }

    /// <summary>
    /// BuildTargetGrap overload accepting WindowInfo with MatchedVia/MatchedSnippet.
    /// Injects the matched field into the grap when not already visible in compact form,
    /// so the reason for the match is always clear in the output.
    /// </summary>
    internal static string BuildTargetGrap(WKAppBot.Win32.Window.WindowInfo hit)
    {
        // child-cmd: host window found via child process cmdline match.
        // proc: must be the child process (it owns the cmdline), hwnd: is the terminal shell window.
        // MatchedSnippet = "childProcName:matchedToken"
        if (hit.MatchedVia == "child-cmd" && !string.IsNullOrEmpty(hit.MatchedSnippet))
        {
            var sep = hit.MatchedSnippet.IndexOf(':');
            if (sep > 0)
            {
                var childProc = hit.MatchedSnippet[..sep].Replace("'", "\\'");
                var token     = hit.MatchedSnippet[(sep + 1)..].Replace("'", "\\'");
                if (token.Length > 60) token = token[..60];
                var childCompact = $"{{proc:'{childProc}',cmd:'{token}'}}";
                return InjectHwnd(childCompact, hit.Handle);
            }
        }

        var compact = BuildCompactWinGrap(hit.Handle);

        // For browser/web-app windows: always inject domain: or file: if not already in compact
        if (!compact.Contains("domain:") && !compact.Contains("file:"))
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hit.Handle, out uint hitPid);
            try
            {
                var rawUrl = WKAppBot.Win32.Window.WindowFinder.GetBrowserUrl(hit.Handle, hitPid);
                if (TryBuildDisplayBrowserInject(rawUrl, out var inject) && !string.IsNullOrEmpty(inject))
                    compact = compact.StartsWith('{')
                        ? compact.TrimEnd('}') + $",{inject}}}"
                        : $"{{{inject}}}";
            }
            catch { }
        }

        // For legacy/non-browser apps: ensure cls: is present (needed for Win32 targeting)
        if (!compact.Contains("domain:") && !compact.Contains("file:") && !compact.Contains("cls:"))
        {
            var cls = WKAppBot.Win32.Window.WindowFinder.GetClassName(hit.Handle);
            if (!string.IsNullOrEmpty(cls))
            {
                cls = cls.Replace("'", "\\'");
                compact = compact.StartsWith('{')
                    ? compact.TrimEnd('}') + $",cls:'{cls}'}}"
                    : $"{{cls:'{cls}'}}";
            }
        }

        // Inject matched field if not already visible in compact form
        if (!string.IsNullOrEmpty(hit.MatchedVia) && !string.IsNullOrEmpty(hit.MatchedSnippet))
        {
            bool alreadyVisible = hit.MatchedVia switch
            {
                "domain" => compact.Contains("domain:"),
                "url"    => compact.Contains("domain:") || compact.Contains("file:"),
                "proc"   => compact.Contains("proc:"),
                "cmd"    => compact.Contains("cmd:") || compact.Contains("file:") || compact.Contains("domain:"),
                "uia"    => compact.Contains("uia:"),
                _        => true
            };
            if (!alreadyVisible)
            {
                var snippet = hit.MatchedSnippet.Replace("'", "\\'");
                if (snippet.Length > 60) snippet = snippet[..60];
                var inject = $"{hit.MatchedVia}:'{snippet}'";
                compact = compact.StartsWith('{')
                    ? compact.TrimEnd('}') + $",{inject}}}"
                    : $"{{{inject}}}";
            }
        }
        return InjectHwnd(compact, hit.Handle);
    }

    private static bool TryBuildDisplayBrowserInject(string? rawUrl, out string inject)
    {
        inject = "";
        if (string.IsNullOrWhiteSpace(rawUrl)) return false;

        var primary = rawUrl.Split(' ')[0];
        if (primary.StartsWith("vscode-file://", StringComparison.OrdinalIgnoreCase) ||
            primary.StartsWith("chrome-extension://", StringComparison.OrdinalIgnoreCase) ||
            primary.StartsWith("devtools://", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (primary.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
        {
            var fn = System.IO.Path.GetFileName(Uri.UnescapeDataString(primary));
            if (string.IsNullOrEmpty(fn)) return false;
            inject = $"file:'{fn.Replace("'", "\\'")}'";
            return true;
        }

        try
        {
            var host = new Uri(primary).Host;
            if (string.IsNullOrEmpty(host) ||
                host.Equals("vscode-app", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            inject = $"domain:'{host.Replace("'", "\\'")}'";
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string _BuildCompactWinGrapCore(IntPtr hwnd)
    {
        var g = WindowFinder.BuildTargetJson5(hwnd);
        // Strip machine-specific noisy fields
        g = System.Text.RegularExpressions.Regex.Replace(g, @",?hwnd:0x[0-9A-Fa-f]+,?", ",");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",?pid:\d+,?", ",");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",title:'[^']*'", "");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",url:'[^']*'", "");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",cmd:'[^']*'", "");
        if (g.Contains("domain:"))
        {
            // Browser with web domain: return just the domain
            g = System.Text.RegularExpressions.Regex.Replace(g, @",?proc:'[^']*',?", ",");
            g = System.Text.RegularExpressions.Regex.Replace(g, @",?cls:'[^']*',?", ",");
            var m = System.Text.RegularExpressions.Regex.Match(g, @"domain:'([^']*)'");
            if (m.Success) return $"{{domain:'{m.Groups[1].Value}'}}";
        }
        else
        {
            // No domain — check for stripped internal Electron/browser URLs
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid2);
            try
            {
                var rawUrl = WindowFinder.GetBrowserUrl(hwnd, pid2);
                if (!string.IsNullOrEmpty(rawUrl))
                {
                    if (rawUrl.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
                    {
                        var fileName = System.IO.Path.GetFileName(Uri.UnescapeDataString(rawUrl));
                        if (!string.IsNullOrEmpty(fileName)) return $"file:'{fileName}'";
                    }
                    // vscode-file:// and other Electron internal URLs → fall through to {proc,cls}
                }
            }
            catch { }
        }
        // Strip generic Chromium window classes — add no disambiguation value
        g = System.Text.RegularExpressions.Regex.Replace(g, @",?cls:'Chrome_WidgetWin_\d+',?", ",");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",?cls:'Chrome_RenderWidgetHostHWND',?", ",");
        // Clean up stray commas from removals
        g = System.Text.RegularExpressions.Regex.Replace(g, @"\{,+", "{");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",+\}", "}");
        g = System.Text.RegularExpressions.Regex.Replace(g, @",{2,}", ",");
        // If all fields were stripped (e.g. process name unavailable + Chrome class removed),
        // fall back to a minimal {proc,cls} grap directly from Win32 to avoid useless "{}"
        if (g == "{}" || g == "{ }")
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint fallbackPid);
            var fallbackProc = WKAppBot.Win32.Native.NativeMethods.TryGetProcessNameFast(fallbackPid) ?? "";
            var fallbackCls  = WindowFinder.GetClassName(hwnd) ?? "";
            fallbackProc = fallbackProc.Replace("'", "\\'");
            fallbackCls  = fallbackCls.Replace("'", "\\'");
            if (!string.IsNullOrEmpty(fallbackProc) && !string.IsNullOrEmpty(fallbackCls))
                return $"{{proc:'{fallbackProc}',cls:'{fallbackCls}'}}";
            if (!string.IsNullOrEmpty(fallbackProc))
                return $"{{proc:'{fallbackProc}'}}";
            if (!string.IsNullOrEmpty(fallbackCls))
                return $"{{cls:'{fallbackCls}'}}";
        }
        return g;
    }

    static string SanitizePathTokenForExp(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "unknown";
        var invalid = Path.GetInvalidFileNameChars();
        var chars = s.Select(ch => invalid.Contains(ch) ? '_' : ch).ToArray();
        return new string(chars);
    }

    static int RunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot run <scenario.yaml> [-v|--verbose] [--no-watch] [--report <dir>]");

        string scenarioPath = args[0];
        bool verbose = args.Contains("-v") || args.Contains("--verbose");
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 200;
        string? reportDir = GetArgValue(args, "--report");

        // Parse scenario
        var doc = ScenarioParser.Load(scenarioPath);
        Console.WriteLine($"Loaded: {doc.Scenario.Name} ({doc.Steps.Count} steps)");

        // Run with passive background watcher (default: on)
        var runner = new ScenarioRunner(verbose, watch: !noWatch, watchIntervalMs: watchMs);

        // [ZOOM] Wire up zoom overlay factory for per-step visual feedback
        runner.ZoomFactory = (screenRect, formHandle, action, label) =>
        {
            var helper = ClickZoomHelper.BeginFromRect(screenRect, formHandle, action, label);
            return helper != null ? new ClickZoomAdapter(helper) : null;
        };

        // [READINESS] Wire up InputReadiness for pre-action blocker/minimize check
        runner.ReadinessInstance = CreateInputReadiness();

        // [VISION-ASK] Wire up Gemini fast-path for blob identification (returns OcrSegment with coords)
        runner.VisionAskFn = (bmp, desc) => AskGeminiForVisionAsync(bmp, desc);

        var result = runner.Run(doc);

        // TODO: Generate HTML report if reportDir specified

        return result.IsSuccess ? 0 : 1;
    }

    // ── validate ───────────────────────────────────────────────

    static int ValidateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot validate <scenario.yaml>");

        string path = args[0];
        if (ScenarioParser.TryValidate(path, out string? error))
        {
            var doc = ScenarioParser.Load(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"VALID: {doc.Scenario.Name}");
            Console.ResetColor();
            Console.WriteLine($"  Steps: {doc.Steps.Count}");
            Console.WriteLine($"  App: {doc.App.Launch}");
            if (doc.Teardown != null)
                Console.WriteLine($"  Teardown: {doc.Teardown.Count} steps");
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"INVALID: {error}");
            Console.ResetColor();
            return 1;
        }
    }

    // TerminateProcess: bypass ExitProcess / DLL detach / managed finalizers → immediate exit.
    // ExitProcess (used by Environment.Exit) sends DLL_PROCESS_DETACH to all DLLs and waits for
    // them — if a background thread holds a loader lock (e.g. inside CreateHardLink / CreateSymbolicLink),
    // ExitProcess deadlocks for ~26s until the OS timeout. TerminateProcess skips all that.
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern IntPtr GetCurrentProcess();

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern uint GetLastError();

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    // Named event for Core→Launcher instant exit signaling
    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr OpenEventW(uint dwDesiredAccess, bool bInheritHandle, string lpName);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetEvent(IntPtr hEvent);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool FlushFileBuffers(IntPtr hFile);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateFileW(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    /// <summary>
    /// For grap/grep fast-exit: replace Console.Out with a synchronous (non-IOCP) pipe writer.
    /// .NET 8's default Console.Out uses IOCP (async I/O) — Flush() only SCHEDULES the write;
    /// actual delivery to the pipe happens during DLL_PROCESS_DETACH (~28s due to loader lock
    /// deadlock). A synchronous FileStream bypasses IOCP: writes reach the pipe immediately.
    /// Call this BEFORE command dispatch and update OriginalStdout so LogcatCommand also uses it.
    /// </summary>
    internal static void SetupSyncStdout()
    {
        // No-op: sync FileStream on overlapped pipe handles throws ArgumentException.
        // FastExit() uses FlushFileBuffers + TerminateProcess instead — see FastExit comments.
    }

    /// <summary>
    /// Required exit cleanup: log move (tee.Dispose) + WriteExitFile.
    /// Idempotent — safe to call from both normal Main exit and ProcessExit hook.
    /// The ProcessExit hook calls this with signalLauncher=false because the named-event
    /// signal races with TerminateProcess; we let the hook's own UIT + TerminateProcess handle that.
    /// FastExit (TerminateProcess) bypasses CLR shutdown entirely, so it calls WriteExitFile directly.
    /// </summary>
    static void DoRequiredExitCleanup(int code, bool signalLauncher = true)
    {
        if (System.Threading.Interlocked.Exchange(ref _exitCleanupDone, 1) != 0) return;

        var tee = _activeTee;
        if (tee != null)
        {
            try { tee.ExitCode = code; } catch { }
            try { tee.WriteErrorRecordNow(); } catch { }
            try { Console.SetOut(tee.OriginalConsole); } catch { }
            try { tee.Dispose(); } catch { } // moves log to logs/old
        }

        if (signalLauncher)
            WriteExitFile(code);
    }

    /// <summary>
    /// Write exit-file sentinel for Launcher's IOCP poll. Uses Win32 API directly.
    /// Does NOT call TerminateProcess — caller should return normally from Main.
    /// </summary>
    static void WriteExitFile(int code)
    {
        try { Console.Out.Flush(); } catch { }
        try
        {
            var hOut = GetStdHandle(-11);
            if (hOut != IntPtr.Zero && hOut != new IntPtr(-1))
                FlushFileBuffers(hOut);
        }
        catch { }
        try
        {
            var exitFile = _exitFilePath ?? Environment.GetEnvironmentVariable("WKAPPBOT_EXIT_FILE");
            if (!string.IsNullOrEmpty(exitFile))
            {
                // Use Win32 API directly — File.WriteAllText may leave pending I/O
                // that GetFileAttributesW on the parent side doesn't see for ~30s.
                var hFile = CreateFileW(exitFile, 0x40000000 /*GENERIC_WRITE*/, 0, IntPtr.Zero,
                    2 /*CREATE_ALWAYS*/, 0x80 /*FILE_ATTRIBUTE_NORMAL*/, IntPtr.Zero);
                if (hFile != IntPtr.Zero && hFile != new IntPtr(-1))
                {
                    var ecBytes = System.Text.Encoding.UTF8.GetBytes(code.ToString());
                    WriteFile(hFile, ecBytes, (uint)ecBytes.Length, out _, IntPtr.Zero);
                    FlushFileBuffers(hFile);
                    CloseHandle(hFile);
                }
            }
        }
        catch { }

        // Signal named event (Launcher waits on this — instant, no FS visibility delay)
        if (!string.IsNullOrEmpty(_exitEventName))
        {
            try
            {
                const uint EVENT_MODIFY_STATE = 0x0002;
                var hEvt = OpenEventW(EVENT_MODIFY_STATE, false, _exitEventName);
                if (hEvt != IntPtr.Zero)
                {
                    SetEvent(hEvt);
                    CloseHandle(hEvt);
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// FastExit for grap/grep: flush stdout pipe to kernel buffer, then TerminateProcess.
    ///
    /// Why not Environment.Exit: ExitProcess → DLL_PROCESS_DETACH → loader lock deadlock for ~28s.
    ///   If stdout is a pipe, .NET's IOCP async writes may not have committed to the kernel pipe
    ///   buffer yet → TerminateProcess cancels them → data loss (relay sees bytes=0).
    ///
    /// Why not CloseHandle(STDOUT): Launcher uses overlapped (async) pipe handles. CloseHandle on
    ///   the write end while IOCP writes are in-flight cancels those writes → data loss.
    ///
    /// Correct approach:
    ///   1. Console.Out.Flush() — flushes StreamWriter char buffer to underlying FileStream
    ///   2. FlushFileBuffers(hOut) — forces kernel to commit all pending pipe writes; blocks until
    ///      all data is in the OS pipe buffer and visible to the reader.
    ///   3. TerminateProcess(self) — kills immediately; all handles closed atomically.
    ///      No DLL_PROCESS_DETACH → no loader lock deadlock. Relay reads committed data, then gets EOF.
    /// </summary>
    static void FastExit(int code = 0)
    {
        void Dbg(string s) { try { System.IO.File.AppendAllText(@"C:\Temp\fastexit_dbg.txt", $"{System.Diagnostics.Stopwatch.GetTimestamp()} {s}\n"); } catch { } }
        Dbg($"FastExit enter code={code} relay={RelayFilePath ?? "(null)"}");
        try
        {
            // Flush all buffered output to relay file (or stdout if no relay)
            Dbg("before Flush");
            Console.Out.Flush();
            Dbg("after Flush");

            if (RelayFilePath == null)
            {
                // Direct stdout path (no relay): FlushFileBuffers commits pending IOCP pipe writes
                // to the kernel buffer BEFORE TerminateProcess. Without this, .NET 8 async writes
                // may still be in-flight when TerminateProcess kills them → bash $() capture gets empty.
                var hOut = GetStdHandle(-11); // STD_OUTPUT_HANDLE
                if (hOut != IntPtr.Zero && hOut != new IntPtr(-1))
                {
                    Dbg("FlushFileBuffers(stdout)");
                    FlushFileBuffers(hOut);
                    Dbg("FlushFileBuffers done");
                }
                // File-based exit sentinel: create via Win32 API directly (not .NET File I/O).
                // .NET File.WriteAllText leaves pending I/O that TerminateProcess doesn't flush for ~30s.
                // Win32 CreateFileW + WriteFile + FlushFileBuffers + CloseHandle is immediate.
                Dbg("writing exit-file sentinel (Win32)");
                try
                {
                    var exitDir = System.IO.Directory.Exists(@"C:\Temp") ? @"C:\Temp" : System.IO.Path.GetTempPath();
                    var exitFile = System.IO.Path.Combine(exitDir, $"wkappbot-exit-{Environment.ProcessId}");
                    var hFile = CreateFileW(exitFile, 0x40000000 /*GENERIC_WRITE*/, 0, IntPtr.Zero,
                        2 /*CREATE_ALWAYS*/, 0x80 /*FILE_ATTRIBUTE_NORMAL*/, IntPtr.Zero);
                    if (hFile != IntPtr.Zero && hFile != new IntPtr(-1))
                    {
                        var ecBytes = System.Text.Encoding.UTF8.GetBytes(code.ToString());
                        WriteFile(hFile, ecBytes, (uint)ecBytes.Length, out _, IntPtr.Zero);
                        FlushFileBuffers(hFile);
                        CloseHandle(hFile);
                        Dbg($"exit-file written (Win32): {exitFile}");
                    }
                    else
                    {
                        Dbg($"CreateFileW failed err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()}");
                    }
                }
                catch (Exception ex) { Dbg($"exit-file FAILED: {ex.Message}"); }
                // Verify the file is visible to other processes BEFORE TerminateProcess.
                // NTFS directory entry may not be committed yet after CloseHandle.
                // FlushFileBuffers on the parent directory forces metadata commit.
                try
                {
                    var exitDir2 = System.IO.Directory.Exists(@"C:\Temp") ? @"C:\Temp" : System.IO.Path.GetTempPath();
                    var hDir = CreateFileW(exitDir2, 0x40000000 /*GENERIC_WRITE*/, 7 /*RW|DELETE*/,
                        IntPtr.Zero, 3 /*OPEN_EXISTING*/, 0x02000000 /*FILE_FLAG_BACKUP_SEMANTICS*/, IntPtr.Zero);
                    if (hDir != IntPtr.Zero && hDir != new IntPtr(-1))
                    {
                        FlushFileBuffers(hDir);
                        CloseHandle(hDir);
                        Dbg("directory flushed");
                    }
                }
                catch { }
            }

            if (RelayFilePath != null)
            {
                // Close relay file BEFORE signaling .ready so Launcher can read it without sharing conflicts.
                Dbg("closing Console.Out (relay file)");
                try { Console.Out.Close(); } catch { }
                Console.SetOut(System.IO.TextWriter.Null); // prevent further writes

                // File-based handshake: create .ready WHILE STILL ALIVE so Launcher can see it immediately.
                // (Files created before TerminateProcess are visible to other processes without 27s delay.)
                // Launcher reads relay file, then creates .ack. We wait for .ack (max 500ms) then terminate.
                var _readyPath    = RelayFilePath + ".ready";
                var _ackPath      = RelayFilePath + ".ack";
                var _exitCodePath = RelayFilePath + ".exitcode";
                Dbg($"writing .exitcode={code} .ready to {_readyPath}");
                try { System.IO.File.WriteAllText(_exitCodePath, code.ToString()); } catch { }
                try { System.IO.File.WriteAllText(_readyPath, "1"); Dbg(".ready written"); } catch (Exception ex) { Dbg($".ready FAILED: {ex.Message}"); }
                // Wait for Launcher to signal .ack (relay file is closed — no sharing issue)
                var _deadline = System.Diagnostics.Stopwatch.GetTimestamp()
                              + (long)(500 * System.Diagnostics.Stopwatch.Frequency / 1000.0);
                while (System.Diagnostics.Stopwatch.GetTimestamp() < _deadline)
                {
                    if (System.IO.File.Exists(_ackPath)) { Dbg(".ack found"); break; }
                    System.Threading.Thread.Sleep(5);
                }
                Dbg(".ack wait done");
            }
        }
        catch (Exception ex) { Dbg($"FastExit outer catch: {ex.Message}"); }
        Dbg("calling TerminateProcess");
        TerminateProcess(GetCurrentProcess(), (uint)code);
        System.Threading.Thread.Sleep(500);
        Environment.Exit(code);
    }

    /// <summary>
    /// Returns true if wkappbot.exe (the Launcher) is in this process's ancestor chain.
    /// Used to detect whether Launcher log infra is active — if not, we self-create a log.
    /// </summary>
    static bool IsLauncherInParentChain()
    {
        try
        {
            var visited = new HashSet<int>();
            int pid = Environment.ProcessId;
            for (int depth = 0; depth < 10; depth++)
            {
                var query = new System.Management.ManagementObjectSearcher(
                    $"SELECT ParentProcessId,Name FROM Win32_Process WHERE ProcessId={pid}");
                using var results = query.Get();
                int parentPid = 0;
                string? parentName = null;
                foreach (System.Management.ManagementObject obj in results)
                {
                    parentPid = Convert.ToInt32(obj["ParentProcessId"]);
                    parentName = obj["Name"]?.ToString();
                }
                if (parentPid == 0 || !visited.Add(parentPid)) break;
                if (parentName?.StartsWith("wkappbot", StringComparison.OrdinalIgnoreCase) == true
                    && !parentName.Contains("core", StringComparison.OrdinalIgnoreCase))
                    return true;
                pid = parentPid;
            }
        }
        catch { /* WMI unavailable — assume no Launcher */ }
        return false;
    }

    /// <summary>Tees stderr to a log file for Core processes spawned without Launcher log infra.</summary>
    sealed class WorkerStderrTeeWriter(TextWriter original, System.IO.StreamWriter logFile) : TextWriter
    {
        public override System.Text.Encoding Encoding => original.Encoding;
        public override void Write(char value) { original.Write(value); logFile.Write(value); }
        public override void Write(string? value) { original.Write(value); logFile.Write(value); }
        public override void WriteLine(string? value) { original.WriteLine(value); logFile.WriteLine(value); }
        public override void Flush() { original.Flush(); logFile.Flush(); }
        protected override void Dispose(bool disposing) { if (disposing) logFile.Dispose(); base.Dispose(disposing); }
    }
}

