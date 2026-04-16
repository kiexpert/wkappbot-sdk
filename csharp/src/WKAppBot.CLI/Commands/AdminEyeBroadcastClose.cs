using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WKAppBot.CLI;

/// <summary>
/// Broadcast-close coordination for admin Eye hand-over.
///
/// New admin Eye: calls <see cref="FireBroadcast"/> BEFORE claiming any resources
///   → signals all existing admin Eye(s) via a named manual-reset event
///   → sleeps briefly to give old eyes time to drain + exit
///   → polls for the admin pipe to disappear (confirms clean gap)
///
/// Old admin Eye: calls <see cref="StartListener"/> once during startup
///   → background task watches the event
///   → on signal: cancels the supplied CancellationTokenSource → graceful self-exit
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
    /// Non-fatal on any failure (best effort).
    /// </summary>
    public static void FireBroadcast(int postSignalWaitMs = 500, int maxGapWaitMs = 2000)
    {
        try
        {
            using var ev = new EventWaitHandle(false, EventResetMode.ManualReset, EventName);
            ev.Set();
            Console.Error.WriteLine("[EYE:ADMIN-CLOSE] broadcast close fired — waiting for old admin Eye(s) to drain");
            Thread.Sleep(postSignalWaitMs);
            ev.Reset();

            // Poll for admin pipe file to vanish — confirms old admin Eye released pipe server
            var deadline = Environment.TickCount + maxGapWaitMs;
            while (Environment.TickCount < deadline)
            {
                if (!File.Exists(AdminPipePath)) break;
                Thread.Sleep(50);
            }
            Console.Error.WriteLine(File.Exists(AdminPipePath)
                ? "[EYE:ADMIN-CLOSE] gap timeout — pipe still present; new admin will contend"
                : "[EYE:ADMIN-CLOSE] gap confirmed — new admin Eye taking over");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN-CLOSE] broadcast skipped: {ex.Message}");
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
                        Console.Error.WriteLine("[EYE:ADMIN-CLOSE] broadcast received — initiating graceful exit");
                        cts.Cancel();
                        break;
                    }
                }
            }
            catch { /* event handle invalidated — stop watching silently */ }
        });
    }
}
