// ScreenSaverStandaloneCommand.cs — Standalone screensaver process
// Usage: wkappbot screensaver
// Self-contained: own idle detection, own WPF, own process.
// Eye spawns this → WPF memory isolated from Eye.

namespace WKAppBot.CLI;

internal partial class Program
{
    static int ScreenSaverStandaloneCommand()
    {
        // Parent PID passed via environment variable by Eye
        int parentPid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out parentPid);
        Console.WriteLine($"[SCREENSAVER] Standalone process (parent PID={parentPid})");
        try
        {
            var ss = new ScreenSaverOverlay();
            ss.Start();
            Console.WriteLine("[SCREENSAVER] Overlay ready (idle ≥10s)");

            // Self-contained tick loop — exit when parent dies
            int parentCheck = 0;
            while (true)
            {
                ss.Tick();
                Thread.Sleep(100);
                if (++parentCheck % 50 == 0) // every 5s
                {
                    try { System.Diagnostics.Process.GetProcessById(parentPid); }
                    catch { Console.WriteLine("[SCREENSAVER] Parent gone — exiting"); break; }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSAVER] Error: {ex.Message}");
            return 1;
        }
        return 0;
    }
}
