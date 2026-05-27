namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Diagnostic check: if excessive Chrome processes are detected before launch,
    /// emit warning suggesting the Chrome multiplication bug (Core FindRunningChromePortAny guard).
    /// Triggered by recurring bug where FindRunningChromePortAny skips reuse when port file
    /// expires (registered=0 condition). Fix in Core 8819ed449; verify deployment.
    /// </summary>
    internal static void DiagnoseExcessiveChromeProcesses(string cmd)
    {
        try
        {
            var chromeProcs = System.Diagnostics.Process.GetProcessesByName("chrome");
            if (chromeProcs.Length > 5)
            {
                Console.Error.WriteLine($"[CHROME-HEALTH] WARNING: {chromeProcs.Length} Chrome processes detected before launch (cmd={cmd})");
                Console.Error.WriteLine($"[CHROME-HEALTH] This may indicate Chrome multiplication bug (Core FindRunningChromePortAny guard)");
                Console.Error.WriteLine($"[CHROME-HEALTH] Expected: 1-2 Chrome processes max. Verify Core binary has commit 8819ed449 fix deployed.");
            }
        }
        catch { /* best-effort diagnostic */ }
    }
}
