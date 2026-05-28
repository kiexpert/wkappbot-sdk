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

                // SDK-side guard: auto-clean excessive Chrome to prevent cascading failures
                CleanExcessiveChromeProcesses(chromeProcs);
            }
        }
        catch { /* best-effort diagnostic */ }
    }

    /// <summary>
    /// Auto-remediation: kill duplicate Chrome processes when count exceeds 5.
    /// Preserves the 2 oldest (active) Chrome processes, kills the rest.
    /// Prevents Chrome multiplication from cascading into Eye IPC lag.
    /// </summary>
    internal static void CleanExcessiveChromeProcesses(System.Diagnostics.Process[] chromeProcs)
    {
        try
        {
            if (chromeProcs.Length <= 5)
                return;

            var sorted = chromeProcs.OrderBy(p => p.StartTime).ToArray();
            var toKeep = 2;
            var toKill = sorted.Skip(toKeep).ToArray();

            foreach (var proc in toKill)
            {
                try
                {
                    proc.Kill(entireProcessTree: true);
                    Console.Error.WriteLine($"[CHROME-HEALTH] Auto-cleaned duplicate chrome PID {proc.Id}");
                }
                catch { /* silent fail on individual process */ }
            }

            Console.Error.WriteLine($"[CHROME-HEALTH] Auto-cleaned {toKill.Length} duplicate Chrome processes ({chromeProcs.Length}→{toKeep})");
        }
        catch { /* best-effort cleanup */ }
    }
}
