using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot eye [--interval N] [--size WxH] [--pos X,Y]
/// Opens "WK AppBot Eye" overlay — GlobalMode text-based summary with Slack integration.
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

    // AllocConsole BANNED — 콘솔 창 절대 생성 금지. 호출 시 즉시 에러 + 종료.
    /// <summary>DO NOT USE. Throws + exits if called. Use log files instead.</summary>
    [Obsolete("AllocConsole BANNED — use log files. Calling this will crash.", true)]
    static bool AllocConsole() => throw new InvalidOperationException(
        "AllocConsole is BANNED. 콘솔 창 생성 금지! 로그파일로 출력하세요. " +
        "Call site: " + new System.Diagnostics.StackTrace(true).ToString());

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandlerDelegate? handler, bool add);
    delegate bool ConsoleCtrlHandlerDelegate(int ctrlType);
    // Keep a static reference — GC must not collect it while Eye is running
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
    /// Write a log line to stderr — Eye's stderr is captured and logged by the host (Launcher relay).
    /// Safe to call from any thread; swallows all exceptions.
    /// </summary>
    internal static void EyeLog(string message)
    {
        try { Console.Error.WriteLine($"[EYE] {message}"); } catch { }
    }

    /// <summary>Safe console color — no-op when no console (DETACHED_PROCESS).</summary>
    internal static void EyeColor(ConsoleColor c) { try { Console.ForegroundColor = c; } catch { } }
    internal static void EyeResetColor() { try { Console.ResetColor(); } catch { } }

    // ── Eye Watchdog Task (Task Scheduler) ──────────────────────────────
    // Eye 시작마다 자동 재등록 → 클롣이 까먹어도 자가 치유.
    // 10분마다 `eye tick --timeout 15` 실행 → Eye 죽으면 재spawn + retry queue flush.
    private const string EyeWatchdogTaskName = "WKAppBot Eye Watchdog";

    /// <summary>
    /// Schedule eye tick 2 minutes from now via schtasks.exe (no PowerShell = no focus steal).
    /// Eye loop calls this every minute — keeps pushing 2 min forward while alive.
    /// If Eye dies, last scheduled time fires 2 min later → tick checks + respawns Eye.
    /// </summary>
    internal static void EnsureEyeWatchdogTask()
    {
        var corePath = Environment.ProcessPath ?? "";
        var dir = Path.GetDirectoryName(corePath) ?? "";
        var launcherPath = Path.Combine(dir, "wkappbot.exe");
        if (!File.Exists(launcherPath)) launcherPath = corePath;

        // Use wscript.exe + VBS wrapper to suppress console window entirely.
        // VBScript ws.Run "...", 0 = hidden window (more reliable than conhost --headless).
        // ws.CurrentDirectory sets CWD before launch — prevents system32 default.
        // VBS lives under {watchdogCwd}/.wkappbot/ — visible in project, per-workspace.
        // Only update VBS when running in full Eye loop (EyeCallerCwd is set).
        // During eye tick, skip VBS rewrite — existing VBS on disk already has correct CWD.
        string? watchdogCwd = EyeCallerCwd.Length > 0 ? EyeCallerCwd : null;
        var vbsDir = watchdogCwd != null ? Path.Combine(watchdogCwd, ".wkappbot") : dir;
        var vbsPath = Path.Combine(vbsDir, "wkappbot-silent.vbs");
        var vbsLog = Path.Combine(vbsDir, "watchdog.log");
        // Watchdog fires = Eye has been dead ≥2 min (abnormal). Log + kill zombie Eye + respawn.
        // WMI query kills any wkappbot-core.exe whose cmdline contains " eye" (eye / eye tick modes).
        // "On Error Resume Next" protects against WMI failures on locked/exiting processes.
        var cwdLine = watchdogCwd != null ? $"ws.CurrentDirectory = \"{watchdogCwd}\"\n" : "";
        var vbsContent =
            "On Error Resume Next\n" +
            $"Set fso = CreateObject(\"Scripting.FileSystemObject\")\n" +
            $"Set f = fso.OpenTextFile(\"{vbsLog}\", 8, True)\n" +
            "f.WriteLine \"[WATCHDOG] \" & Now() & \" Eye dead — killing zombie + respawn\"\n" +
            "f.Close\n" +
            "Set fso = Nothing\n" +
            "Set oWMI = GetObject(\"winmgmts:\")\n" +
            "Set oProcs = oWMI.ExecQuery(\"SELECT ProcessId FROM Win32_Process WHERE Name='wkappbot-core.exe' AND CommandLine LIKE '% eye%'\")\n" +
            "For Each oProc In oProcs\n" +
            "  oProc.Terminate()\n" +
            "Next\n" +
            "On Error GoTo 0\n" +
            "WScript.Sleep 1000\n" +
            "Set ws = CreateObject(\"WScript.Shell\")\n" +
            cwdLine +
            $"ws.Run \"\"\"{launcherPath}\"\" eye tick --timeout 15\", 0, False\n";
        try { Directory.CreateDirectory(vbsDir); File.WriteAllText(vbsPath, vbsContent); } catch { }
        var tr = File.Exists(vbsPath) ? $"wscript.exe \\\"{vbsPath}\\\"" : $"\\\"{launcherPath}\\\" eye tick --timeout 15";
        // /SC MINUTE /MO 2: repeating every 2 min — survives even if Eye's 60s refresh fails.
        // Eye's loop refreshes this every 60s (rolling defer), but if Eye dies the task keeps firing.
        var fireAt = DateTime.Now.AddMinutes(2).ToString("HH:mm");
        var fireDate = DateTime.Now.AddMinutes(2).ToString("yyyy/MM/dd");
        var args = $"/Create /TN \"{EyeWatchdogTaskName}\" /TR \"{tr}\" /SC MINUTE /MO 2 /ST {fireAt} /SD {fireDate} /F /RL LIMITED";
        // Fire-and-forget — schtasks.exe can take 0.5-3s; don't block Eye startup
        Console.WriteLine($"[EYE] Watchdog: registering /SC MINUTE /MO 2 (first={fireAt})...");
        _ = Task.Run(() =>
        {
            try
            {
                using var spawn = AppBotPipe.Spawn("schtasks.exe", args,
                    cwd: Environment.SystemDirectory, caller: "EYE-SCHED");
                spawn?.WaitForExit(5000);
                var ok = spawn != null && spawn.ExitCode == 0;
                Console.WriteLine(ok
                    ? $"[EYE] Watchdog: ✓ registered (/SC MINUTE /MO 2 → first={fireAt})"
                    : $"[EYE] Watchdog: schtasks exit={spawn?.ExitCode} (non-fatal)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE] Watchdog error: {ex.Message} (non-fatal)");
            }
        });
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
            EyeLog($"[EYE] Console event: {name} — Eye is a daemon, blocking termination");
            return true; // returning true blocks default action (kill process)
        };
        SetConsoleCtrlHandler(_eyeCtrlHandler, true);
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
    // Callers use WaitOne(0) — if it fails, Eye is alive; if it succeeds, Eye is dead.
    internal const string EyeAliveMutexName = "Global\\WKAppBotEyeAlive";
    // Static reference so GC never collects the mutex while Eye is running.
    private static System.Threading.Mutex? _eyeAliveMutex;
    // Spawn mutex: prevents concurrent spawn attempts
    private const string EyeSpawnMutexName = "Global\\WKAppBotEyeSpawn";

    /// <summary>
    /// Eye auto-launch — called from Program.Main for every CLI command.
    ///
    /// ┌─────────────────────────────────────────────────────────────────┐
    /// │  Eye Launch Flow (single-instance guarantee)                   │
    /// │                                                                │
    /// │  Step 1: Guard checks (RunningInEye, LOOP_CALLER)             │
    /// │  Step 2: Alive mutex fast check — is Eye already running?      │
    /// │  Step 3: Spawn mutex acquire — serialize concurrent spawners   │
    /// │          ⚠ If timeout (3s) → another spawner active → bail    │
    /// │  Step 4: Alive mutex re-check — Eye may have started while    │
    /// │          we waited for spawn mutex                             │
    /// │  Step 5: Process.Start("wkappbot-core.exe eye")               │
    /// │          ⚠ CreateProcess hooks / AV can add 1-5s delay here   │
    /// │  Step 6: Poll alive mutex (max 5s) — wait for Eye to signal   │
    /// │          "I'm alive" before releasing spawn mutex              │
    /// │  Step 7: Release spawn mutex → other callers can proceed      │
    /// └─────────────────────────────────────────────────────────────────┘
    ///
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
        // Loop subprocess mode: Eye inherits stdout pipe handle → blocks parent's ReadToEndAsync.
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WKAPPBOT_LOOP_CALLER"))) return;

        PulseStep.Init("eye-launch");

        // ── Step 2: Alive mutex fast check ──
        // WaitOne(0) fails → Eye holds it → alive → skip.
        // WaitOne(0) succeeds → Eye dead → release + proceed to spawn.
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
        catch (System.Threading.AbandonedMutexException) { } // Eye crashed — fall through

        PulseStep.Mark("alive-check-passed");
        EyeLog("Eye not running — attempting spawn");

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
            EyeLog("Spawn mutex timeout — another spawner active, skipping");
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
                    EyeLog("Eye appeared during spawn-mutex wait — skipping");
                    PulseStep.Done("eye-appeared-during-wait");
                    return;
                }
                aliveMutex2.ReleaseMutex();
            }
            catch (System.Threading.AbandonedMutexException) { } // Eye crashed — spawn

            // ── Step 5: Resolve core exe path and spawn ──
            var launcherPath = Environment.ProcessPath ?? "";
            var dir  = Path.GetDirectoryName(launcherPath) ?? "";
            var exeName = Path.GetFileNameWithoutExtension(launcherPath); // e.g. "wkappbot" or "a11y"
            var corePath = Path.Combine(dir, exeName + "-core.exe");
            if (!File.Exists(corePath)) corePath = launcherPath;
            if (string.IsNullOrEmpty(corePath)) return;

            // CRITICAL: Do NOT redirect stdin/stdout/stderr!
            // .NET Process.Start with UseShellExecute=false + no redirects →
            //   CreateProcess(bInheritHandles=FALSE) → child gets NO parent handles.
            // This prevents Eye from inheriting bash's PTY handles (which caused
            // `eye tick` to hang forever — bash waits for all PTY handle holders to exit).
            // Eye creates its own console via AllocConsole() — no stdio from parent needed.
            // Previously used UseShellExecute=true, but ShellExecuteEx fails silently
            // in DETACHED_PROCESS context (no shell/console to work with).
            // CWD chain: user's project dir → Core → Eye → screensaver/whisper-ring/MCP
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
                for (int wait = 0; wait < 2000; wait += 200)
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

        // Parse arguments (GlobalMode only — no app/web/legacy modes)
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

        // Eye is a background daemon — console setup depends on context:
        // DETACHED_PROCESS (spawned by Core): no console → redirect to log file.
        //   AllocConsole is NOT called — it creates a visible window flash.
        //   Console.Out/Error → log file so output is preserved (not lost to void).
        //   ForegroundColor/ResetColor → safe no-op (no console handle).
        // Manual run (user typed `wkappbot eye`): console exists → hide + install ctrl handler.
        if (GetConsoleWindow() == IntPtr.Zero)
        {
            // DETACHED context: no console, no AllocConsole (flash 방지).
            // Eye는 Core가 직접 spawn (Launcher 파이프 안 거침) → stdout/stderr 로그 파일로 보존.
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
            }
            catch
            {
                // log 파일 못 열면 → Null (출력 유실이지만 Eye 동작엔 지장 없음)
                Console.SetOut(TextWriter.Null);
                Console.SetError(TextWriter.Null);
            }
        }
        else
        {
            // Manual context (or scheduler-spawned): UTF-8 for Korean console output
            try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }
            try { Console.InputEncoding = System.Text.Encoding.UTF8; } catch { }
            try { WKAppBot.Win32.Native.NativeMethods.SetConsoleOutputCP(65001); } catch { }
            try { WKAppBot.Win32.Native.NativeMethods.SetConsoleCP(65001); } catch { }
            TryHideConsoleWindow();
            InstallEyeCtrlHandler();
        }

        // Acquire Eye-alive mutex for this process's lifetime — signals to callers that Eye is running.
        // Elevated Eye skips mutex — runs alongside normal Eye (admin proxy only, not full Eye).
        bool isElevatedArg = args.Any(a => a == "--elevated");
        if (!isElevatedArg)
        {
            _eyeAliveMutex = new System.Threading.Mutex(true, EyeAliveMutexName, out bool createdNew);
            if (!createdNew)
            {
                // Another Eye holds the mutex — wait briefly (hot-swap handoff window)
                EyeLog("[EYE] Another Eye detected — waiting 5s for handoff...");
                bool acquired = _eyeAliveMutex.WaitOne(5000);
                if (!acquired)
                {
                    Console.Error.WriteLine($"[EYE:WARN] ⚠ SELF-EXIT: Another Eye holds mutex after 5s — PID={Environment.ProcessId} exiting as duplicate");
                    _eyeAliveMutex.Dispose();
                    _eyeAliveMutex = null;
                    return 0;
                }
                EyeLog("[EYE] Previous Eye released mutex — taking over");
            }
        }
        else
        {
            EyeLog("[EYE] Elevated mode — skipping mutex (coexists with normal Eye)");
        }
        // _eyeAliveMutex is static — held for process lifetime, GC will never collect it

        try { Console.Title = "AppBotEye"; } catch { } // for a11y close targeting (safe: no console in DETACHED)
        WKAppBot.Win32.Input.ProcessLaunchGuard.IsEyeProcess = true; // Eye daemon — skip focus guard

        // Elevated proxy mode: if running as admin, start Named Pipe server alongside Eye
        bool elevated = args.Any(a => a == "--elevated") || ElevationHelper.IsElevated();
        if (elevated)
        {
            // Hide console window — attach to parent's console if exists, otherwise no window
            var hConsole = GetConsoleWindow();
            if (hConsole != IntPtr.Zero)
                ShowWindow(hConsole, 0); // SW_HIDE

            EyeColor(ConsoleColor.Yellow);
            Console.WriteLine("[EYE] Running as ADMIN — elevated proxy pipe will be available");
            EyeResetColor();
        }

        // Hot-swap blue-green: --replace-pid <pid> → close old Eye after first render
        int replacePid = 0;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--replace-pid" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out replacePid);

        Console.WriteLine("[EYE] Starting WK AppBot Eye (GlobalMode)");
        // Clean up orphan sandbox registry entries from previous Eye session (dead HWNDs)
        AskTargetRegistry.PurgeDeadHwnds();
        // Purge stale card cache from previous Eye session — fresh start, no zombie cards
        CardCachePurgeAll();
        return EyeGlobalPollingLoop(width, height, posX, posY, intervalMs, Environment.CurrentDirectory, elevated, replacePid);
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
