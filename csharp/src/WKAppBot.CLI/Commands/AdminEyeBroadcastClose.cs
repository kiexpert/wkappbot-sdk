using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WKAppBot.CLI;

/// <summary>
/// Broadcast-close coordination for admin Eye hand-over.
///
/// New admin Eye: calls <see cref="FireBroadcast"/> BEFORE claiming any resources
///   -> signals all existing admin Eye(s) via a named manual-reset event
///   -> sleeps briefly to give old eyes time to drain + exit
///   -> polls for the admin pipe to disappear (confirms clean gap)
///
/// Old admin Eye: calls <see cref="StartListener"/> once during startup
///   -> background task watches the event
///   -> on signal: cancels the supplied CancellationTokenSource -> graceful self-exit
///
/// Pattern ensures a brief window where NO admin Eye exists before the new one takes over.
/// Same admin pipe name is reused, so without this gap the pipe would be contested.
/// </summary>
internal static class AdminEyeBroadcastClose
{
    public const string EventName = "Global\\WKAppBotAdminEyeBroadcastClose";
    public static readonly string AdminPipePath = $@"\\.\pipe\wkappbot_elevated_{ProjectRoot.Hash8()}";

    /// <summary>
    /// Fire a broadcast-close event, wait for old admin Eye(s) to release the pipe, return.
    /// Two-phase strong close:
    ///   Phase 1 (graceful): signal named event -- old admin Eyes with listener exit cleanly
    ///   Phase 2 (forced):  if pipe still held after grace period, kill other admin Eye processes
    /// The caller of this method is itself an admin Eye (elevated), so Process.Kill is authorized.
    /// </summary>
    public static void FireBroadcast(int postSignalWaitMs = 500, int maxGapWaitMs = 2000)
    {
        // -- Phase 1: graceful signal --
        try
        {
            using var ev = new EventWaitHandle(false, EventResetMode.ManualReset, EventName);
            ev.Set();
            Console.Error.WriteLine("[EYE:ADMIN-CLOSE] broadcast close fired -- waiting for old admin Eye(s) to drain");
            Thread.Sleep(postSignalWaitMs);
            ev.Reset();

            // Poll for admin pipe file to vanish -- confirms old admin Eye released pipe server
            var deadline = Environment.TickCount + maxGapWaitMs;
            while (Environment.TickCount < deadline)
            {
                if (!File.Exists(AdminPipePath)) break;
                Thread.Sleep(50);
            }
            if (!File.Exists(AdminPipePath))
            {
                Console.Error.WriteLine("[EYE:ADMIN-CLOSE] gap confirmed -- new admin Eye taking over");
                return;
            }
            Console.Error.WriteLine("[EYE:ADMIN-CLOSE] graceful close did not free pipe -- escalating to force kill");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] graceful signal skipped: {ex.Message}");
        }

        // -- Phase 2: force kill OTHER ADMIN EYE processes (not random wkappbot-core) --
        // Bootstrap path -- when old admin Eye was built WITHOUT the broadcast listener
        // (e.g. first upgrade), Phase 1 has no effect. We are running as admin so we can
        // terminate other admin processes. Strictly filter by cmdline `eye --elevated`
        // so user Eye, MCP worker, whisper-ring, screensaver, analyze-hack etc. are NOT
        // touched -- the previous "collateral on user Eye is acceptable" policy killed
        // the entire user-session tree whenever a pipe zombie extended Phase 1.
        try
        {
            int me = Environment.ProcessId;
            int killed = 0;
            int skipped = 0;
            foreach (var p in System.Diagnostics.Process.GetProcessesByName("wkappbot-core"))
            {
                try
                {
                    if (p.Id == me) continue;
                    string cmd = "";
                    try { cmd = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(p.Id) ?? ""; } catch { }
                    bool isAdminEye = cmd.Contains(" eye --elevated", StringComparison.OrdinalIgnoreCase)
                                   || cmd.EndsWith(" eye --elevated", StringComparison.OrdinalIgnoreCase);
                    if (!isAdminEye)
                    {
                        skipped++;
                        continue;
                    }
                    p.Kill(entireProcessTree: false);
                    killed++;
                }
                catch { /* ignore -- may be protected or already gone */ }
            }
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] force-killed {killed} admin Eye process(es), spared {skipped} other wkappbot-core (user Eye/MCP/ring)");

            // Re-poll pipe disappearance after kill
            var deadline2 = Environment.TickCount + 1500;
            while (Environment.TickCount < deadline2)
            {
                if (!File.Exists(AdminPipePath)) break;
                Thread.Sleep(50);
            }
            Console.Error.WriteLine(File.Exists(AdminPipePath)
                ? "[EYE:ADMIN-CLOSE] pipe STILL present after kill -- kernel may still be GC-ing a handleless entry; new ListenAsync will supersede"
                : "[EYE:ADMIN-CLOSE] pipe cleared after force kill -- clean gap");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] force kill failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Background listener: when the broadcast event fires, checks if the incoming Eye is also
    /// admin-elevated. If not, performs a succession hot-swap -- spawns a new admin Eye from
    /// the latest binary (no UAC needed: already elevated) before exiting.
    /// </summary>
    public static void StartListener(CancellationTokenSource cts)
    {
        _ = Task.Run(() =>
        {
            try
            {
                using var ev = new EventWaitHandle(false, EventResetMode.ManualReset, EventName);
                while (!cts.IsCancellationRequested)
                {
                    if (ev.WaitOne(500))
                    {
                        Console.Error.WriteLine("[EYE:ADMIN-CLOSE] broadcast received -- checking successor elevation");
                        TrySuccessionHotSwap();
                        cts.Cancel();
                        break;
                    }
                }
            }
            catch { /* event handle invalidated -- stop watching silently */ }
        });
    }

    /// <summary>
    /// After receiving a close signal, wait for the successor Eye to stabilize, then check
    /// whether it is admin-elevated. If not, spawn a new admin Eye from the latest binary
    /// (elevation is inherited -- no UAC prompt since we are already running as admin).
    /// </summary>
    static void TrySuccessionHotSwap(int waitMs = 3000)
    {
        Thread.Sleep(waitMs); // let successor Eye establish itself

        // Check if any running Eye is elevated
        bool adminSuccessorFound = false;
        int myPid = Environment.ProcessId;
        foreach (var p in System.Diagnostics.Process.GetProcessesByName("wkappbot-core"))
        {
            if (p.Id == myPid) { p.Dispose(); continue; }
            try
            {
                var cmd = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(p.Id) ?? "";
                if (cmd.Contains(" eye --elevated", StringComparison.OrdinalIgnoreCase)
                    || cmd.EndsWith(" eye --elevated", StringComparison.OrdinalIgnoreCase))
                {
                    adminSuccessorFound = true;
                    p.Dispose();
                    break;
                }
            }
            catch { }
            finally { try { p.Dispose(); } catch { } }
        }

        if (adminSuccessorFound)
        {
            Console.Error.WriteLine("[EYE:ADMIN-CLOSE] admin successor confirmed -- clean handoff");
            return;
        }

        Console.Error.WriteLine("[EYE:ADMIN-CLOSE] successor is not admin -- attempting succession hot-swap");
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot-core.exe";
            var exeDir  = Path.GetDirectoryName(exePath) ?? ".";

            // Promote .new.exe if pending (same logic as LaunchElevatedEye pre-spawn swap)
            var newExe = Path.Combine(exeDir, "wkappbot-core.new.exe");
            if (File.Exists(newExe))
            {
                var backup = Path.Combine(exeDir, $"wkappbot-core.old-succession-{DateTime.Now:yyyyMMdd-HHmmss}.exe");
                try { File.Move(exePath, backup); } catch { }
                try { File.Move(newExe, exePath); Console.Error.WriteLine("[EYE:ADMIN-CLOSE] promoted .new.exe for succession"); } catch { }
            }

            // Spawn elevated without UAC -- inherits elevation token from this process
            var psi = new System.Diagnostics.ProcessStartInfo(exePath, "eye --elevated")
            {
                UseShellExecute = false,
            };
            System.Diagnostics.Process.Start(psi);
            Console.Error.WriteLine("[EYE:ADMIN-CLOSE] succession admin Eye spawned -- elevation preserved");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] succession spawn failed: {ex.Message}");
        }
    }
}
