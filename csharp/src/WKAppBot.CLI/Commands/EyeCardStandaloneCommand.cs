// EyeCardStandaloneCommand.cs -- Standalone per-workspace Eye status overlay process.
// Usage: wkappbot eye-card [--hwnd 0x1234AB] [--cwd D:/path]
// Omit --hwnd to auto-detect VS Code window for --cwd (or current dir).
// Env:   WKAPPBOT_PARENT_PID -- watchdog target.

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace WKAppBot.CLI.Commands;

internal static class EyeCardStandaloneCommand
{
    [DllImport("user32.dll")] static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll", CharSet = CharSet.Auto)] static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder sb, int nMaxCount);
    [DllImport("user32.dll", CharSet = CharSet.Auto)] static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder sb, int nMaxCount);
    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public static int Run(string[] args)
    {
        IntPtr vsHwnd = IntPtr.Zero;
        string cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--hwnd" && i + 1 < args.Length)
            {
                var raw = args[++i];
                try
                {
                    if (raw.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                        raw = raw.Substring(2);
                    vsHwnd = new IntPtr(Convert.ToInt64(raw, 16));
                }
                catch { return Fail($"invalid --hwnd value: {raw}"); }
            }
            else if (args[i] == "--cwd" && i + 1 < args.Length)
            {
                cwd = args[++i];
            }
        }

        // Auto-detect: walk own PID's parent chain for first ancestor with a visible window.
        // Works for any launcher -- VS Code terminal, CMD, Claude CLI, etc.
        if (vsHwnd == IntPtr.Zero)
        {
            vsHwnd = FindAncestorWindowHwnd();
            if (vsHwnd == IntPtr.Zero)
                return Fail("No ancestor window found. Pass --hwnd explicitly.");
        }

        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out int ppid);
        var statusFilePath = Path.Combine(Path.GetTempPath(),
            $"wkappbot_card_{CwdHash(cwd)}.json");

        Console.Error.WriteLine($"[EYE-CARD] Standalone (parent={ppid}, hwnd=0x{vsHwnd.ToInt64():X}, status={statusFilePath})");

        var thread = new Thread(() => RunSta(vsHwnd, statusFilePath, ppid));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Name = "EyeCard-Main-STA";
        thread.Start();
        thread.Join();

        Console.Error.WriteLine("[EYE-CARD] Exiting");
        return 0;
    }

    private static void RunSta(IntPtr vsHwnd, string statusFilePath, int ppid)
    {
        try
        {
            var window = new EyeCardWindow(vsHwnd, statusFilePath);
            var dispatcher = Dispatcher.CurrentDispatcher;

            if (ppid > 0)
            {
                var watchdog = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                watchdog.Tick += (_, _) =>
                {
                    try { Process.GetProcessById(ppid); }
                    catch
                    {
                        Console.Error.WriteLine("[EYE-CARD] Parent gone -- shutting down");
                        try { window.Close(); } catch { }
                        dispatcher.InvokeShutdown();
                    }
                };
                watchdog.Start();
            }

            window.Closed += (_, _) => dispatcher.InvokeShutdown();
            window.Show();
            Dispatcher.Run();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE-CARD] Error: {ex.Message}");
        }
    }

    internal static string CwdHash(string cwd)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(cwd.ToLowerInvariant()));
        var sb = new StringBuilder(8);
        for (int i = 0; i < 4; i++) sb.Append(bytes[i].ToString("x2"));
        return sb.ToString();
    }

    // Walk own PID's parent chain; return the first ancestor that owns a visible window.
    // Works for any launcher: VS Code terminal, CMD, Claude CLI, etc.
    private static IntPtr FindAncestorWindowHwnd()
    {
        int checkPid = Environment.ProcessId;
        var titleBuf = new StringBuilder(512);
        for (int depth = 0; depth < 15 && checkPid > 0; depth++)
        {
            try
            {
                int parentPid = 0;
                using var searcher = new System.Management.ManagementObjectSearcher(
                    $"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId={checkPid}");
                foreach (System.Management.ManagementObject mo in searcher.Get())
                { parentPid = Convert.ToInt32(mo["ParentProcessId"]); break; }
                if (parentPid <= 0) break;

                IntPtr found = IntPtr.Zero;
                EnumWindows((hWnd, _) =>
                {
                    if (!IsWindowVisible(hWnd)) return true;
                    GetWindowThreadProcessId(hWnd, out uint wpid);
                    if ((int)wpid != parentPid) return true;
                    titleBuf.Clear(); GetWindowText(hWnd, titleBuf, titleBuf.Capacity);
                    if (titleBuf.Length == 0) return true; // skip untitled helper windows
                    found = hWnd; return false;
                }, IntPtr.Zero);

                if (found != IntPtr.Zero)
                {
                    Console.Error.WriteLine($"[EYE-CARD] Ancestor window pid={parentPid} hwnd=0x{found.ToInt64():X} depth={depth}");
                    return found;
                }
                checkPid = parentPid;
            }
            catch { break; }
        }
        return IntPtr.Zero;
    }

    private static int Fail(string msg)
    {
        Console.Error.WriteLine($"[EYE-CARD] {msg}");
        return 2;
    }
}
