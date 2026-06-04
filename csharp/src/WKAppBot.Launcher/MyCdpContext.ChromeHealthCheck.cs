namespace WKAppBot.Launcher;

partial class Program
{
    internal static void DiagnoseExcessiveChromeProcesses(string cmd)
    {
        try
        {
            var allChromeProcs = System.Diagnostics.Process.GetProcessesByName("chrome");
            var mainBrowserProcs = allChromeProcs.Where(p => {
                try { return p.MainWindowHandle != IntPtr.Zero; }
                catch { return false; }
            }).ToArray();
            if (mainBrowserProcs.Length > 3)
            {
                Console.Error.WriteLine($"[CHROME-HEALTH] WARNING: {mainBrowserProcs.Length} Chrome browser instances detected (cmd={cmd}) [{allChromeProcs.Length} total procs]");
                Console.Error.WriteLine($"[CHROME-HEALTH] This may indicate Chrome multiplication bug (Core FindRunningChromePortAny guard)");
                Console.Error.WriteLine($"[CHROME-HEALTH] Expected: 1-2 browser instances max.");
                CleanExcessiveChromeProcesses(mainBrowserProcs);
            }
        }
        catch { }
    }

    internal static void CleanExcessiveChromeProcesses(System.Diagnostics.Process[] mainBrowserProcs)
    {
        try
        {
            if (mainBrowserProcs.Length <= 3) return;
            var toKill = mainBrowserProcs.OrderBy(p => p.StartTime).Skip(2).ToArray();
            foreach (var proc in toKill)
            {
                try { proc.Kill(); Console.Error.WriteLine($"[CHROME-HEALTH] Auto-cleaned duplicate chrome browser PID {proc.Id}"); }
                catch { }
            }
            Console.Error.WriteLine($"[CHROME-HEALTH] Auto-cleaned {toKill.Length} duplicate Chrome browser instances ({mainBrowserProcs.Length}-->2)");
        }
        catch { }
    }
}