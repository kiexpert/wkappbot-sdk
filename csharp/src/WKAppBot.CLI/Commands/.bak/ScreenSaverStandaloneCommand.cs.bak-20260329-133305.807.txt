// ScreenSaverStandaloneCommand.cs — Standalone screensaver process
// Usage: wkappbot screensaver
// Self-contained: own idle detection, own WPF, own process.
// Eye spawns this → WPF memory isolated from Eye.

namespace WKAppBot.CLI;

internal partial class Program
{
    static int ScreenSaverStandaloneCommand()
    {
        int parentPid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out parentPid);
        Console.WriteLine($"[SCREENSAVER] Standalone process (parent PID={parentPid})");
        try
        {
            var ss = new ScreenSaverOverlay();
            ss.Start();
            Console.WriteLine("[SCREENSAVER] Overlay ready (idle ≥10s)");

            int parentCheck = 0;
            while (!ss.ShouldExit)
            {
                ss.Tick();
                Thread.Sleep(100);
                if (parentPid > 0 && ++parentCheck % 50 == 0)
                {
                    try { System.Diagnostics.Process.GetProcessById(parentPid); }
                    catch { Console.WriteLine("[SCREENSAVER] Parent gone — exiting"); break; }
                }
            }
            if (ss.ShouldExit) Console.WriteLine("[SCREENSAVER] User input — process exiting (memory freed)");
            ss.Dispose();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSAVER] Error: {ex.Message}");
            return 1;
        }
        return 0;
    }
}
