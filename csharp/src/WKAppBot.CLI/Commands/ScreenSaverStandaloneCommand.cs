// ScreenSaverStandaloneCommand.cs — Standalone screensaver process
// Usage: wkappbot screensaver
// Self-contained: own idle detection, own WPF, own process.
// Eye spawns this → WPF memory isolated from Eye.

namespace WKAppBot.CLI;

internal partial class Program
{
    static int ScreenSaverStandaloneCommand()
    {
        Console.WriteLine("[SCREENSAVER] Standalone process starting...");
        try
        {
            var ss = new ScreenSaverOverlay();
            ss.Start();
            Console.WriteLine("[SCREENSAVER] Overlay ready (idle ≥10s)");

            // Self-contained tick loop
            while (true)
            {
                ss.Tick();
                Thread.Sleep(100);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCREENSAVER] Error: {ex.Message}");
            return 1;
        }
    }
}
