using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// CLI command: wkappbot eye [--interval N] [--size WxH] [--pos X,Y]
/// Opens "WK AppBot Eye" overlay — GlobalMode text-based summary with Slack integration.
/// Auto-positions at the rightmost monitor top-right corner.
///
/// Entry point + P/Invoke declarations.
/// GlobalMode loop: AppBotEyeGlobalMode.cs
/// Claude Desktop detection: AppBotEyeClaudeDetector.cs
/// Shared Slack handlers: AppBotEyeSlackHandlers.cs
/// </summary>
internal partial class Program
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;

    static void TryHideConsoleWindow()
    {
        try
        {
            var h = GetConsoleWindow();
            if (h != IntPtr.Zero)
                ShowWindow(h, SW_HIDE);
        }
        catch { }
    }

    static int AppBotEyeCommand(string[] args)
    {
        // one-shot diagnostic tick (must not enter global loop)
        if (args.Length > 0 && string.Equals(args[0], "tick", StringComparison.OrdinalIgnoreCase))
            return EyeTickCommand(args.Skip(1).ToArray());

        // Parse arguments (GlobalMode only — no app/web/legacy modes)
        int intervalMs = 100;
        int width = 320, height = 220;
        int posX = -1, posY = -1;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--interval" when i + 1 < args.Length:
                    int.TryParse(args[++i], out intervalMs);
                    break;
                case "--size" when i + 1 < args.Length:
                    var sizeParts = args[++i].Split('x', 'X');
                    if (sizeParts.Length == 2)
                    {
                        int.TryParse(sizeParts[0], out width);
                        int.TryParse(sizeParts[1], out height);
                    }
                    break;
                case "--pos" when i + 1 < args.Length:
                    var posParts = args[++i].Split(',');
                    if (posParts.Length == 2)
                    {
                        int.TryParse(posParts[0], out posX);
                        int.TryParse(posParts[1], out posY);
                    }
                    break;
            }
        }

        TryHideConsoleWindow();
        Console.WriteLine("[EYE] Starting WK AppBot Eye (GlobalMode)");
        return EyeGlobalPollingLoop(width, height, posX, posY, intervalMs);
    }

    // ── P/Invoke declarations (shared across all AppBotEye partial class files) ──

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_BASIC_INFORMATION
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    // P/Invoke
    private delegate bool EnumWindowsProcMV(IntPtr hWnd, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProcMV lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern int GetWindowLongW(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out MV_RECT lpRect);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("ntdll.dll")]
    private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass,
        ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out MV_POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct MV_POINT { public int X, Y; }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
}
