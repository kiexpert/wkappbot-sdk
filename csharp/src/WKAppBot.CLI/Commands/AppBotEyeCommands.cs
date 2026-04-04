using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot eye [--interval N] [--size WxH] [--pos X,Y]
/// Opens "WK AppBot Eye" overlay ??GlobalMode text-based summary with Slack integration.
/// Auto-positions at the rightmost monitor top-right corner.
///
/// Entry point + P/Invoke declarations.
/// GlobalMode loop: AppBotEyeGlobalMode.cs
/// Claude Desktop detection: AppBotEyeClaudeDetector.cs
/// Shared Slack handlers: AppBotEyeSlackHandlers.cs
/// Eye auto-launch: LaunchAppBotEyeIfNeeded / LaunchAppBotEyeIfNeededCore (this file)
/// </summary>
internal partial class Program
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SendMessageTimeout(
        IntPtr hWnd,
        uint Msg,
        IntPtr wParam,
        IntPtr lParam,
        uint fuFlags,
        uint uTimeout,
        out IntPtr lpdwResult);

    // AllocConsole BANNED -- do not create console windows; use log files instead. Throws + exits if called.
    /// <summary>DO NOT USE. Throws + exits if called. Use log files instead.</summary>
    [Obsolete("AllocConsole BANNED ??use log files. Calling this will crash.", true)]
    static bool AllocConsole() => throw new InvalidOperationException(
        "AllocConsole is BANNED. Do not create console windows; use log files! " +
        "Call site: " + new System.Diagnostics.StackTrace(true).ToString());

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandlerDelegate? handler, bool add);
    delegate bool ConsoleCtrlHandlerDelegate(int ctrlType);
    // Keep a static reference ??GC must not collect it while Eye is running
    static ConsoleCtrlHandlerDelegate? _eyeCtrlHandler;

    private const int SW_HIDE = 0;

    static void TryHideConsoleWindow()
    {
        try
        {
            var h = GetConsoleWindow();
            if (h != IntPtr.Zero)
                ShowWindow(h, SW_HIDE);
        }
        catch { }
    }

    /// <summary>
    /// Write a log line to stderr ??Eye's stderr is captured and logged by the host (Launcher relay).
    /// Safe to call from any thread; swallows all exceptions.
    /// </summary>
    internal static void EyeLog(string message)
    {
        try { Console.Error.WriteLine($"[EYE] {message}"); } catch { }
    }

    /// <summary>Safe console color ??no-op when no console (DETACHED_PROCESS).</summary>
    internal static void EyeColor(ConsoleColor c) { try { Console.ForegroundColor = c; } catch { } }
    internal static void EyeResetColor() { try { Console.ResetColor(); } catch { } }

    // ── Eye Watchdog Task (Task Scheduler) ──────────────────────────────
    // Eye watchdog keeps ticking while alive; if Eye dies, watchdog restarts it.
    // Every 10 ticks: run `eye tick --timeout 15` -- if Eye is dead, re-spawn + retry queue flush.
    internal const string EyeWatchdogStartupRunKeyName = "WKAppBot Eye Startup Recovery";

    /// <summary>
    /// Register watchdog task 2 minutes from now. Delete+Create ensures the timer is always reset
    /// (schtasks /Create /F alone doesn't reset an existing repeating task's next fire time).
    /// Eye loop calls this every 60s ??keeps pushing 2 min forward while alive.
    /// If Eye dies, last registration fires 2 min later ??tick checks + respawns Eye.
    /// </summary>
    internal static void EnsureEyeWatchdogTask()
    {
        var corePath = Environment.ProcessPath ?? "";
        var dir = Path.GetDirectoryName(corePath) ?? "";
        var launcherPath = Path.Combine(dir, "wkappbot.exe");
        if (!File.Exists(launcherPath)) launcherPath = corePath;

        string? watchdogCwd = EyeCallerCwd.Length > 0 ? EyeCallerCwd : null;
        var vbsDir = watchdogCwd != null ? Path.Combine(watchdogCwd, ".wkappbot") : dir;
        var vbsPath = Path.Combine(vbsDir, "wkappbot-silent.vbs");
        var vbsLog = Path.Combine(vbsDir, "watchdog.log");
        var guardianArgs = "eye guardian --respawn-delay 10 --poll-ms 10000 --tick-timeout-ms 5000";

        var cwdLine = watchdogCwd != null ? $"ws.CurrentDirectory = \"{watchdogCwd}\"\n" : "";
        var vbsContent =
            "On Error Resume Next\n" +
            $"Set fso = CreateObject(\"Scripting.FileSystemObject\")\n" +
            $"Set f = fso.OpenTextFile(\"{vbsLog}\", 8, True)\n" +
            "f.WriteLine \"[WATCHDOG] \" & Now() & \" launch guardian\"\n" +
            "f.Close\n" +
            "Set fso = Nothing\n" +
            "Set ws = CreateObject(\"WScript.Shell\")\n" +
            cwdLine +
            $"ws.Run \"\"\"{corePath}\"\" {guardianArgs}\", 0, False\n";
        try { Directory.CreateDirectory(vbsDir); File.WriteAllText(vbsPath, vbsContent); } catch { }
        var tr = File.Exists(vbsPath) ? $"wscript.exe //B \\\"{vbsPath}\\\"" : $"\\\"{corePath}\\\" {guardianArgs}";
        bool runKeyArmed = EnsureEyeStartupRunKey(tr, out var runKeyReason);
        Console.WriteLine(runKeyArmed
            ? $"[EYE] Guardian startup run-key: armed ({runKeyReason})"
            : $"[EYE] Guardian startup run-key: not armed ({runKeyReason})");
        Console.WriteLine("[EYE] Schedulerless guardian mode active");
        return;
    }

    internal static bool TryLaunchEyeGuardianBootstrap(string vbsPath, string corePath, string guardianArgs, out string reason)
    {
        try
        {
            if (File.Exists(vbsPath))
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "wscript.exe",
                    Arguments = $"//B \"{vbsPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(vbsPath) ?? Path.GetDirectoryName(corePath) ?? Environment.CurrentDirectory,
                };
                using var proc = Process.Start(psi);
                reason = proc != null ? "vbs" : "vbs-start-null";
                return proc != null;
            }

            var fallbackPsi = new ProcessStartInfo
            {
                FileName = corePath,
                Arguments = guardianArgs,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(corePath) ?? Environment.CurrentDirectory,
            };
            using var fallbackProc = Process.Start(fallbackPsi);
            reason = fallbackProc != null ? "core-fallback" : "core-start-null";
            return fallbackProc != null;
        }
        catch (Exception ex)
        {
            reason = ex.GetType().Name + ": " + ex.Message;
            return false;
        }
    }

    internal static void EnsureEyeGuardianForWindow(IntPtr hwnd)
    {
        try
        {
            if (hwnd == IntPtr.Zero) return;
            var corePath = Environment.ProcessPath ?? "";
            if (string.IsNullOrWhiteSpace(corePath)) return;
            var dir = Path.GetDirectoryName(corePath) ?? "";
            string? watchdogCwd = EyeCallerCwd.Length > 0 ? EyeCallerCwd : null;
            var vbsDir = watchdogCwd != null ? Path.Combine(watchdogCwd, ".wkappbot") : dir;
            var vbsPath = Path.Combine(vbsDir, "wkappbot-silent.vbs");
            var guardianArgs = $"eye guardian --respawn-delay 10 --poll-ms 10000 --tick-timeout-ms 5000 --eye-hwnd 0x{hwnd.ToInt64():X}";
            bool launched = TryLaunchEyeGuardianBootstrap(vbsPath, corePath, guardianArgs, out var launchReason);
            Console.WriteLine(launched
                ? $"[EYE] Guardian bootstrap launched ({launchReason}, hwnd=0x{hwnd.ToInt64():X})"
                : $"[EYE] Guardian bootstrap launch skipped ({launchReason}, hwnd=0x{hwnd.ToInt64():X})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] Guardian bootstrap error: {ex.Message}");
        }
    }

    private static bool EnsureEyeStartupRunKey(string command, out string reason)
    {
        try
        {
            using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", writable: true);
            if (key == null)
            {
                reason = "open-key-failed";
                return false;
            }

            var current = key.GetValue(EyeWatchdogStartupRunKeyName) as string;
            if (string.Equals(current, command, StringComparison.Ordinal))
            {
                reason = "already-set";
                return true;
            }

            key.SetValue(EyeWatchdogStartupRunKeyName, command, RegistryValueKind.String);
            var verify = key.GetValue(EyeWatchdogStartupRunKeyName) as string;
            if (string.Equals(verify, command, StringComparison.Ordinal))
            {
                reason = "set";
                return true;
            }

            reason = "verify-mismatch";
            return false;
        }
        catch (Exception ex)
        {
            reason = ex.GetType().Name + ": " + ex.Message;
            return false;
        }
    }


    /// <summary>
    /// Install a Ctrl handler that blocks console-close events.
    /// Logs the event type to console + eye.log so we can diagnose unexpected termination attempts.
    /// </summary>
    static void InstallEyeCtrlHandler()
    {
        _eyeCtrlHandler = ctrlType =>
        {
            // 0=C, 1=BREAK, 2=CLOSE, 5=LOGOFF, 6=SHUTDOWN
            var name = ctrlType switch { 0 => "CTRL_C", 1 => "CTRL_BREAK", 2 => "CTRL_CLOSE",
                5 => "CTRL_LOGOFF", 6 => "CTRL_SHUTDOWN", _ => $"CTRL_{ctrlType}" };
            EyeLog($"[EYE] Console event: {name} ??Eye is a daemon, blocking termination");
            return true; // returning true blocks default action (kill process)
        };
        SetConsoleCtrlHandler(_eyeCtrlHandler, true);
    }

    // ── Crash detection helpers ──────────────────────────────────────────

    static string GetEyeVbsDir()
    {
        var dir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? "";
        return EyeCallerCwd.Length > 0 ? Path.Combine(EyeCallerCwd, ".wkappbot") : dir;
    }

    static string GetEyeLogDir()
    {
        var dir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? "";
        return Path.Combine(dir, "wkappbot.hq", "logs");
    }

    /// <summary>
    /// Called at Eye startup: if no clean-exit marker from previous session, move that log to logs/_crashed/.
    /// Covers both exception crashes and hard kills (taskkill/watchdog).
    /// </summary>
    internal static void CheckPreviousCrash()
    {
        try
        {
            var cleanExitFile = Path.Combine(GetEyeVbsDir(), "eye-clean-exit");
            if (File.Exists(cleanExitFile)) { File.Delete(cleanExitFile); return; }
            var logDir = GetEyeLogDir();
            var myLog = $"eye.pid={Environment.ProcessId}.log";
            var prevLogs = Directory.Exists(logDir)
                ? Directory.GetFiles(logDir, "eye.pid=*.log")
                    .Where(f => !Path.GetFileName(f).Equals(myLog, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(File.GetLastWriteTimeUtc)
                    .ToArray()
                : Array.Empty<string>();
            if (prevLogs.Length == 0) return;
            var crashedDir = Path.Combine(logDir, "_crashed");
            Directory.CreateDirectory(crashedDir);
            var latest = prevLogs[0];
            var dest = Path.Combine(crashedDir, Path.GetFileName(latest));
            File.Move(latest, dest, overwrite: true);
            Console.WriteLine($"[EYE] Previous session was not clean ??moved {Path.GetFileName(latest)} ??_crashed/");
        }
        catch { }
    }

    /// <summary>Write clean-exit marker so next Eye startup skips _crashed/ move.</summary>
    internal static void WriteEyeCleanExit()
    {
        try { File.WriteAllText(Path.Combine(GetEyeVbsDir(), "eye-clean-exit"), "1"); } catch { }
    }

    // ── Eye auto-launch (called from Program.cs for every command) ──

    /// <summary>
    /// Auto-launch AppBotEye in unified mode (ActionState IPC) if not already running.
    /// Called fire-and-forget from Program.Main for every command.
    /// </summary>
    internal static void LaunchAppBotEyeIfNeeded() => LaunchAppBotEyeIfNeededCore("");

    /// <summary>
    /// Auto-launch AppBotEye with --port (web mode). Called from web commands.
    /// </summary>
    internal static void LaunchAppBotEyeIfNeeded(int port) => LaunchAppBotEyeIfNeededCore($"--port {port}");

    // "Eye alive" mutex: Eye holds this for its entire lifetime.
    // Callers use WaitOne(0) ??if it fails, Eye is alive; if it succeeds, Eye is dead.
    internal const string EyeAliveMutexName = "Global\\WKAppBotEyeAlive";
    // Static reference so GC never collects the mutex while Eye is running.
    private static System.Threading.Mutex? _eyeAliveMutex;
    static int EyeGuardianCommand(string[] args)
    {
        int pollMs = 10000;
        int respawnDelaySec = 10;
        int launchTimeoutMs = 15000;
        int tickTimeoutMs = 5000;
        IntPtr eyeHwnd = IntPtr.Zero;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--poll-ms" && i + 1 < args.Length) int.TryParse(args[i + 1], out pollMs);
            else if (args[i] == "--respawn-delay" && i + 1 < args.Length) int.TryParse(args[i + 1], out respawnDelaySec);
            else if (args[i] == "--launch-timeout-ms" && i + 1 < args.Length) int.TryParse(args[i + 1], out launchTimeoutMs);
            else if (args[i] == "--tick-timeout-ms" && i + 1 < args.Length) int.TryParse(args[i + 1], out tickTimeoutMs);
            else if (args[i] == "--eye-hwnd" && i + 1 < args.Length)
            {
                var raw = args[i + 1];
                if (raw.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) raw = raw[2..];
                if (long.TryParse(raw, System.Globalization.NumberStyles.HexNumber, null, out var value))
                    eyeHwnd = new IntPtr(value);
            }
        }

        if (pollMs < 500) pollMs = 500;
        if (respawnDelaySec < 5) respawnDelaySec = 5;
        if (launchTimeoutMs < 3000) launchTimeoutMs = 3000;
        if (tickTimeoutMs < 100) tickTimeoutMs = 100;

        TryHideConsoleWindow();
        var guardianLogPath = Path.Combine(GetEyeLogDir(), "eye-guardian.log");
        void GuardianLog(string message)
        {
            try
            {
                Directory.CreateDirectory(GetEyeLogDir());
                File.AppendAllText(guardianLogPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}{Environment.NewLine}");
            }
            catch { }
            try { Console.Error.WriteLine(message); } catch { }
        }
        using var guardianMutex = new System.Threading.Mutex(false, "Global\\WKAppBotEyeGuardian");
        bool gmAcquired = false;
        try { gmAcquired = guardianMutex.WaitOne(3000); }
        catch (System.Threading.AbandonedMutexException) { gmAcquired = true; }
        if (!gmAcquired)
        {
            GuardianLog("[EYE_GUARDIAN] already running (mutex not acquired in 3s)");
            return 0;
        }

        bool IsEyeAlive()
        {
            try
            {
                using var aliveMutex = new System.Threading.Mutex(false, EyeAliveMutexName);
                if (!aliveMutex.WaitOne(0))
                    return true;
                aliveMutex.ReleaseMutex();
                return false;
            }
            catch (System.Threading.AbandonedMutexException) { return false; }
            catch { return false; }
        }

        bool IsEyeHealthy()
        {
            var pingOk = PingEyeWindow();
            try
            {
                var tick = EyeIpcClient.QueryTickAsync(timeoutMs: tickTimeoutMs).GetAwaiter().GetResult();
                return tick != null || pingOk || IsEyeLogFresh();
            }
            catch
            {
                return pingOk || IsEyeLogFresh();
            }
        }

        bool IsEyeLogFresh()
        {
            try
            {
                var logDir = GetEyeLogDir();
                if (!Directory.Exists(logDir)) return false;
                var latest = Directory.EnumerateFiles(logDir, "eye.pid=*.log", SearchOption.TopDirectoryOnly)
                    .Select(path =>
                    {
                        try { return new FileInfo(path); }
                        catch { return null; }
                    })
                    .Where(fi => fi != null)
                    .OrderByDescending(fi => fi!.LastWriteTimeUtc)
                    .FirstOrDefault();
                if (latest == null) return false;
                var freshWindowMs = Math.Max(15000, pollMs * 2);
                return (DateTime.UtcNow - latest.LastWriteTimeUtc).TotalMilliseconds <= freshWindowMs;
            }
            catch
            {
                return false;
            }
        }

        bool PingEyeWindow()
        {
            try
            {
                if (eyeHwnd == IntPtr.Zero) return false;
                if (!WKAppBot.Win32.Native.NativeMethods.IsWindow(eyeHwnd)) return false;
                _ = SendMessageTimeout(
                    eyeHwnd,
                    0x0000,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    0x0002,
                    (uint)Math.Max(100, tickTimeoutMs),
                    out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        GuardianLog($"[EYE_GUARDIAN] started pollMs={pollMs} respawnDelaySec={respawnDelaySec} launchTimeoutMs={launchTimeoutMs} tickTimeoutMs={tickTimeoutMs} eyeHwnd=0x{eyeHwnd.ToInt64():X}");
        DateTime? deadSinceUtc = null;
        int consecutiveHealthFailures = 0;
        int consecutiveHealthSuccesses = 0;
        const int ExitThreshold = 7;
        while (true)
        {
            var alive = IsEyeAlive();
            if (alive)
            {
                if (IsEyeHealthy())
                {
                    consecutiveHealthSuccesses++;
                    GuardianLog($"[EYE_GUARDIAN] tick ok streak={consecutiveHealthSuccesses}/{ExitThreshold}");
                    deadSinceUtc = null;
                    consecutiveHealthFailures = 0;
                    if (consecutiveHealthSuccesses >= ExitThreshold)
                    {
                        GuardianLog($"[EYE_GUARDIAN] {ExitThreshold} consecutive healthy ticks — spawning successor guardian");

                        // Release mutex so successor can acquire it
                        try { guardianMutex.ReleaseMutex(); } catch { }

                        // Spawn successor with same parameters
                        var corePath2 = Environment.ProcessPath ?? "wkappbot";
                        var succArgs = $"eye guardian --respawn-delay {respawnDelaySec} --poll-ms {pollMs}" +
                                       $" --tick-timeout-ms {tickTimeoutMs} --launch-timeout-ms {launchTimeoutMs}" +
                                       (eyeHwnd != IntPtr.Zero ? $" --eye-hwnd 0x{eyeHwnd.ToInt64():X}" : "");
                        AppBotPipe.Spawn(corePath2, succArgs,
                            EyeCallerCwd.Length > 0 ? EyeCallerCwd : Environment.CurrentDirectory,
                            caller: "EYE-GUARDIAN-SUCCESSOR");

                        // Wait up to 5s for successor to acquire guardian mutex
                        bool successorConfirmed = false;
                        for (int si = 0; si < 20; si++)
                        {
                            Thread.Sleep(250);
                            try
                            {
                                using var probe = new System.Threading.Mutex(false, "Global\\WKAppBotEyeGuardian");
                                if (!probe.WaitOne(0)) { successorConfirmed = true; break; }
                                probe.ReleaseMutex();
                            }
                            catch { }
                        }

                        GuardianLog(successorConfirmed
                            ? "[EYE_GUARDIAN] successor confirmed — exiting"
                            : "[EYE_GUARDIAN] successor not confirmed in 5s — exiting anyway");
                        return 0;
                    }
                    Thread.Sleep(1000); // 1s interval for exit threshold check
                    continue;
                }

                consecutiveHealthFailures++;
                consecutiveHealthSuccesses = 0;
                GuardianLog($"[EYE_GUARDIAN] tick failed/timeout streak={consecutiveHealthFailures}");
                if (consecutiveHealthFailures < 2)
                {
                    Thread.Sleep(pollMs);
                    continue;
                }

                GuardianLog("[EYE_GUARDIAN] tick failed twice -> eye tick (kill skipped)");
                try
                {
                    Environment.CurrentDirectory = EyeCallerCwd.Length > 0 ? EyeCallerCwd : Environment.CurrentDirectory;
                }
                catch { }
                try { EyeTickCommand(new[] { "--timeout", "15" }); } catch { }
                deadSinceUtc = DateTime.UtcNow;
                consecutiveHealthFailures = 0;
                Thread.Sleep(pollMs);
                continue;
            }

            consecutiveHealthFailures = 0;
            deadSinceUtc ??= DateTime.UtcNow;
            if ((DateTime.UtcNow - deadSinceUtc.Value).TotalSeconds < respawnDelaySec)
            {
                Thread.Sleep(pollMs);
                continue;
            }

            GuardianLog("[EYE_GUARDIAN] eye missing -> launch attempt");
            LaunchAppBotEyeIfNeededCore("");
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < launchTimeoutMs)
            {
                if (IsEyeAlive())
                {
                    GuardianLog("[EYE_GUARDIAN] eye restored — will exit after healthy streak");
                    consecutiveHealthSuccesses = 0;
                    break;
                }
                Thread.Sleep(250);
            }

            if (deadSinceUtc != null)
            {
                GuardianLog("[EYE_GUARDIAN] launch timeout -> eye tick fallback");
                try
                {
                    Environment.CurrentDirectory = EyeCallerCwd.Length > 0 ? EyeCallerCwd : Environment.CurrentDirectory;
                }
                catch { }
                try { EyeTickCommand(new[] { "--timeout", "15" }); } catch { }
                // If still not alive after tick fallback, exit so a fresh guardian can be started
                if (!IsEyeAlive())
                {
                    GuardianLog("[EYE_GUARDIAN] eye unrecoverable — guardian exiting");
                    return 1;
                }
                GuardianLog("[EYE_GUARDIAN] eye restored via tick — exiting guardian (new Eye will spawn its own)");
                return 0;
            }

            Thread.Sleep(pollMs);
        }
    }
    // Spawn mutex: prevents concurrent spawn attempts
    private const string EyeSpawnMutexName = "Global\\WKAppBotEyeSpawn";

    /// <summary>
    /// Eye auto-launch ??called from Program.Main for every CLI command.
    ///
    /// ─────────────────────────────────────────────────────────────────??    /// ?? Eye Launch Flow (single-instance guarantee)                   ??    /// ??                                                               ??    /// ?? Step 1: Guard checks (RunningInEye, LOOP_CALLER)             ??    /// ?? Step 2: Alive mutex fast check ??is Eye already running?      ??    /// ?? Step 3: Spawn mutex acquire ??serialize concurrent spawners   ??    /// ??         ??If timeout (3s) ??another spawner active ??bail    ??    /// ?? Step 4: Alive mutex re-check ??Eye may have started while    ??    /// ??         we waited for spawn mutex                             ??    /// ?? Step 5: Process.Start("wkappbot-core.exe eye")               ??    /// ??         ??CreateProcess hooks / AV can add 1-5s delay here   ??    /// ?? Step 6: Poll alive mutex (max 5s) ??wait for Eye to signal   ??    /// ??         "I'm alive" before releasing spawn mutex              ??    /// ?? Step 7: Release spawn mutex ??other callers can proceed      ??    /// ─────────────────────────────────────────────────────────────────??    ///
    /// Why spawn mutex is held during Step 6:
    ///   Without this, caller B sees "alive mutex free" during Eye's init
    ///   window (between Process.Start and Eye acquiring alive mutex) and
    ///   spawns a duplicate. CreateProcess hooks widen this gap.
    /// </summary>
    internal static void LaunchAppBotEyeIfNeededCore(string extraArgs)
    {
        // ── Step 1: Guard checks ──
        if (RunningInEye) return;
        if (IsMcpMode) return; // MCP server must NEVER spawn Eye/console windows
        // Loop subprocess mode: Eye inherits stdout pipe handle ??blocks parent's ReadToEndAsync.
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WKAPPBOT_LOOP_CALLER"))) return;

        PulseStep.Init("eye-launch");

        // ── Step 2: Alive mutex fast check ──
        // WaitOne(0) fails ??Eye holds it ??alive ??skip.
        // WaitOne(0) succeeds ??Eye dead ??release + proceed to spawn.
        try
        {
            using var aliveMutex = new System.Threading.Mutex(false, EyeAliveMutexName);
            if (!aliveMutex.WaitOne(0))
            {
                PulseStep.Done("eye-already-alive");
                return;
            }
            aliveMutex.ReleaseMutex();
        }
        catch (System.Threading.AbandonedMutexException) { } // Eye crashed ??fall through

        PulseStep.Mark("alive-check-passed");
        EyeLog("Eye not running ??attempting spawn");

        // ── Step 3: Spawn mutex acquire ──
        // Serializes concurrent spawn attempts across all wkappbot processes.
        // 3s timeout: if another process is already spawning, trust it and bail.
        using var spawnMutex = new System.Threading.Mutex(false, EyeSpawnMutexName);
        bool acquired = false;
        try
        {
            acquired = spawnMutex.WaitOne(3000);
        }
        catch (System.Threading.AbandonedMutexException) { acquired = true; }

        if (!acquired)
        {
            EyeLog("Spawn mutex timeout ??another spawner active, skipping");
            PulseStep.Done("spawn-mutex-timeout");
            return;
        }

        PulseStep.Mark("spawn-mutex-acquired");

        try
        {
            // ── Step 4: Alive mutex re-check ──
            // Eye may have started while we waited for the spawn mutex.
            try
            {
                using var aliveMutex2 = new System.Threading.Mutex(false, EyeAliveMutexName);
                if (!aliveMutex2.WaitOne(0))
                {
                    EyeLog("Eye appeared during spawn-mutex wait ??skipping");
                    PulseStep.Done("eye-appeared-during-wait");
                    return;
                }
                aliveMutex2.ReleaseMutex();
            }
            catch (System.Threading.AbandonedMutexException) { } // Eye crashed ??spawn

            // ── Step 5: Resolve core exe path and spawn ──
            var launcherPath = Environment.ProcessPath ?? "";
            var dir  = Path.GetDirectoryName(launcherPath) ?? "";
            var exeName = Path.GetFileNameWithoutExtension(launcherPath); // e.g. "wkappbot" or "a11y"
            var corePath = Path.Combine(dir, exeName + "-core.exe");
            if (!File.Exists(corePath)) corePath = launcherPath;
            if (string.IsNullOrEmpty(corePath)) return;

            // CRITICAL: Do NOT redirect stdin/stdout/stderr!
            // .NET Process.Start with UseShellExecute=false + no redirects ??            //   CreateProcess(bInheritHandles=FALSE) ??child gets NO parent handles.
            // This prevents Eye from inheriting bash's PTY handles (which caused
            // `eye tick` to hang forever ??bash waits for all PTY handle holders to exit).
            // Eye creates its own console via AllocConsole() ??no stdio from parent needed.
            // Previously used UseShellExecute=true, but ShellExecuteEx fails silently
            // in DETACHED_PROCESS context (no shell/console to work with).
            // CWD chain: user's project dir ??Core ??Eye ??screensaver/whisper-ring/MCP
            var callerCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
            var eyeArgs = "eye" + (extraArgs.Length > 0 ? " " + extraArgs : "");
            EyeLog($"Spawning: {corePath} {eyeArgs} cwd={callerCwd}");
            using var proc2 = AppBotPipe.Spawn(corePath, eyeArgs, callerCwd, caller: "EYE-LAUNCH");
            if (proc2 != null)
            {
                Console.WriteLine($"[EYE] Launched (PID={proc2.Pid})");
                PulseStep.Mark("process-started");

                // Policy broadcast when Eye first spawns (agent reads stdout)
                AgentPolicy.StartPolicyBroadcast();

                // ── Step 6: Wait for Eye to acquire alive mutex ──
                // CRITICAL: Hold spawn mutex during this wait!
                // Without this, another caller sees "alive mutex free" during Eye's
                // init window and spawns a duplicate. CreateProcess hooks / AV scans
                // can widen this gap to several seconds.
                for (int wait = 0; wait < 1000; wait += 200)
                {
                    System.Threading.Thread.Sleep(200);
                    try
                    {
                        using var probe = new System.Threading.Mutex(false, EyeAliveMutexName);
                        if (!probe.WaitOne(0))
                        {
                            EyeLog($"Eye alive mutex confirmed after {wait + 200}ms");
                            break;
                        }
                        probe.ReleaseMutex();
                    }
                    catch (System.Threading.AbandonedMutexException) { break; }

                    if (proc2.HasExited)
                    {
                        EyeLog($"Eye process exited during init (exit={proc2.ExitCode})");
                        break;
                    }
                }

                PulseStep.Done("eye-launch-complete");
            }
        }
        catch (Exception ex)
        {
            EyeLog($"Spawn failed: {ex.Message}");
        }
        finally
        {
            // ── Step 7: Release spawn mutex ──
            if (acquired) try { spawnMutex.ReleaseMutex(); } catch { }
        }
    }

    static int AppBotEyeCommand(string[] args)
    {
        // one-shot diagnostic tick (must not enter global loop)
        if (args.Length > 0 && string.Equals(args[0], "tick", StringComparison.OrdinalIgnoreCase))
            return EyeTickCommand(args.Skip(1).ToArray());
        if (args.Length > 0 && string.Equals(args[0], "guardian", StringComparison.OrdinalIgnoreCase))
            return EyeGuardianCommand(args.Skip(1).ToArray());

        PulseStep.Init("eye-cli");

        // Parse arguments (GlobalMode only ??no app/web/legacy modes)
        int intervalMs = 100;
        int width = 320, height = 220;
        int posX = -1, posY = -1;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--interval" when i + 1 < args.Length:
                    int.TryParse(args[++i], out intervalMs);
                    break;
                case "--size" when i + 1 < args.Length:
                    var sizeParts = args[++i].Split('x', 'X');
                    if (sizeParts.Length == 2)
                    {
                        int.TryParse(sizeParts[0], out width);
                        int.TryParse(sizeParts[1], out height);
                    }
                    break;
                case "--pos" when i + 1 < args.Length:
                    var posParts = args[++i].Split(',');
                    if (posParts.Length == 2)
                    {
                        int.TryParse(posParts[0], out posX);
                        int.TryParse(posParts[1], out posY);
                    }
                    break;
            }
        }

        // Eye is a background daemon ??console setup depends on context:
        // DETACHED_PROCESS (spawned by Core): no console ??redirect to log file.
        //   AllocConsole is NOT called ??it creates a visible window flash.
        //   Console.Out/Error ??log file so output is preserved (not lost to void).
        //   ForegroundColor/ResetColor ??safe no-op (no console handle).
        // Manual run (user typed `wkappbot eye`): console exists ??hide + install ctrl handler.
        if (GetConsoleWindow() == IntPtr.Zero)
        {
            PulseStep.Mark("detached-console");
            // DETACHED context: no console, no AllocConsole (flash 諛⑹?).
            // Eye is spawned directly by Core (not via Launcher relay) -- redirect stdout/stderr to log file.
            var logDir = Path.Combine(
                Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".",
                "wkappbot.hq", "logs");
            try { Directory.CreateDirectory(logDir); } catch { }
            try
            {
                var logPath = Path.Combine(logDir, $"eye.pid={Environment.ProcessId}.log");
                var logWriter = new StreamWriter(logPath, append: true, new System.Text.UTF8Encoding(false)) { AutoFlush = true };
                Console.SetOut(logWriter);
                Console.SetError(logWriter);
                PulseStep.Mark("detached-log-wired");
            }
            catch
            {
                // Log file creation failed -- fall back to Null (output lost, but Eye can still run)
                Console.SetOut(TextWriter.Null);
                Console.SetError(TextWriter.Null);
            }
        }
        else
        {
            PulseStep.Mark("console-attached");
            // Manual context (or scheduler-spawned): UTF-8 for Korean console output
            try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }
            try { Console.InputEncoding = System.Text.Encoding.UTF8; } catch { }
            try { WKAppBot.Win32.Native.NativeMethods.SetConsoleOutputCP(65001); } catch { }
            try { WKAppBot.Win32.Native.NativeMethods.SetConsoleCP(65001); } catch { }
            TryHideConsoleWindow();
            InstallEyeCtrlHandler();
            PulseStep.Mark("console-hidden");
        }

        // Acquire Eye-alive mutex for this process's lifetime ??signals to callers that Eye is running.
        // Elevated Eye skips mutex ??runs alongside normal Eye (admin proxy only, not full Eye).
        bool isElevatedArg = args.Any(a => a == "--elevated");
        PulseStep.Mark("mutex-check");
        if (!isElevatedArg)
        {
            _eyeAliveMutex = new System.Threading.Mutex(true, EyeAliveMutexName, out bool createdNew);
            if (!createdNew)
            {
                // Another Eye holds the mutex ??wait briefly (hot-swap handoff window)
                EyeLog("[EYE] Another Eye detected ??waiting 5s for handoff...");
                bool acquired = _eyeAliveMutex.WaitOne(5000);
                if (!acquired)
                {
                    Console.Error.WriteLine($"[EYE:WARN] ??SELF-EXIT: Another Eye holds mutex after 5s ??PID={Environment.ProcessId} exiting as duplicate");
                    _eyeAliveMutex.Dispose();
                    _eyeAliveMutex = null;
                    return 0;
                }
                EyeLog("[EYE] Previous Eye released mutex ??taking over");
            }
            PulseStep.Mark("mutex-acquired");
        }
        else
        {
            EyeLog("[EYE] Elevated mode ??skipping mutex (coexists with normal Eye)");
            PulseStep.Mark("mutex-skipped-elevated");
        }
        // _eyeAliveMutex is static ??held for process lifetime, GC will never collect it

        try { Console.Title = "AppBotEye"; } catch { } // for a11y close targeting (safe: no console in DETACHED)
        WKAppBot.Win32.Input.ProcessLaunchGuard.IsEyeProcess = true; // Eye daemon ??skip focus guard

        // Elevated proxy mode: if running as admin, start Named Pipe server alongside Eye
        bool elevated = args.Any(a => a == "--elevated") || ElevationHelper.IsElevated();
        PulseStep.Mark(elevated ? "elevation-admin" : "elevation-normal");
        if (elevated)
        {
            // Hide console window ??attach to parent's console if exists, otherwise no window
            var hConsole = GetConsoleWindow();
            if (hConsole != IntPtr.Zero)
                ShowWindow(hConsole, 0); // SW_HIDE

            EyeColor(ConsoleColor.Yellow);
            Console.WriteLine("[EYE] Running as ADMIN ??elevated proxy pipe will be available");
            EyeResetColor();
        }

        // Hot-swap blue-green: --replace-pid <pid> ??close old Eye after first render
        int replacePid = 0;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--replace-pid" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out replacePid);
        PulseStep.Mark(replacePid > 0 ? $"replace-pid:{replacePid}" : "replace-pid:none");

        Console.WriteLine("[EYE] Starting WK AppBot Eye (GlobalMode)");
        // Clean up orphan sandbox registry entries from previous Eye session (dead HWNDs)
        AskTargetRegistry.PurgeDeadHwnds();
        // Purge stale card cache from previous Eye session ??fresh start, no zombie cards
        CardCachePurgeAll();
        PulseStep.Mark("startup-cleaned");
        // Walk up to git/.wkappbot root so watchdog VBS is always at project root,
        // not wherever the shell happened to be when Eye was spawned (e.g. during publish).
        var eyeCwd = Environment.CurrentDirectory;
        try
        {
            var d = eyeCwd.Replace('/', '\\');
            while (!string.IsNullOrEmpty(d))
            {
                if (Directory.Exists(Path.Combine(d, ".git"))
                    || Directory.Exists(Path.Combine(d, ".svn"))
                    || Directory.Exists(Path.Combine(d, ".wkappbot"))
                    || File.Exists(Path.Combine(d, ".wkappbot")))
                { eyeCwd = d; break; }
                var p = Path.GetDirectoryName(d);
                if (p == d || p == null) break;
                d = p;
            }
        }
        catch { }
        PulseStep.Mark("global-loop-call");
        return EyeGlobalPollingLoop(width, height, posX, posY, intervalMs, eyeCwd, elevated, replacePid);
    }

    // ── P/Invoke declarations (shared across all AppBotEye partial class files) ──

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_BASIC_INFORMATION
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    // P/Invoke
    private delegate bool EnumWindowsProcMV(IntPtr hWnd, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProcMV lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern int GetWindowLongW(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out MV_RECT lpRect);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("ntdll.dll")]
    private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass,
        ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out MV_POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_POINT { public int X, Y; }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
}
