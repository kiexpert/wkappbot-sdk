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
    public const string AdminPipePath = @"\\.\pipe\wkappbot_elevated";

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

        // -- Phase 2: force kill other admin Eye processes --
        // Bootstrap path -- when old admin Eye was built WITHOUT the broadcast listener
        // (e.g. first upgrade), Phase 1 has no effect. We are running as admin so
        // we can terminate other elevated wkappbot-core processes.
        try
        {
            int me = Environment.ProcessId;
            int killed = 0;
            foreach (var p in System.Diagnostics.Process.GetProcessesByName("wkappbot-core"))
            {
                try
                {
                    if (p.Id == me) continue;
                    // Best-effort: kill any other wkappbot-core; caller uses this only when new admin
                    // Eye is actively taking over, so collateral on user Eye is acceptable (user Eye will
                    // be respawned by the guardian with --sudo marker inherited).
                    p.Kill(entireProcessTree: false);
                    killed++;
                }
                catch { /* ignore -- may be protected or already gone */ }
            }
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] force-killed {killed} other wkappbot-core process(es)");

            // Re-poll pipe disappearance after kill
            var deadline2 = Environment.TickCount + 1500;
            while (Environment.TickCount < deadline2)
            {
                if (!File.Exists(AdminPipePath)) break;
                Thread.Sleep(50);
            }
            Console.Error.WriteLine(File.Exists(AdminPipePath)
                ? "[EYE:ADMIN-CLOSE] pipe STILL present after kill -- something odd"
                : "[EYE:ADMIN-CLOSE] pipe cleared after force kill -- clean gap");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] force kill failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Background listener: when the broadcast event fires, cancels the given CTS.
    /// Old admin Eye uses this to self-terminate gracefully when a newer admin takes over.
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
                        Console.Error.WriteLine("[EYE:ADMIN-CLOSE] broadcast received -- initiating graceful exit");
                        cts.Cancel();
                        break;
                    }
                }
            }
            catch { /* event handle invalidated -- stop watching silently */ }
        });
    }
}
