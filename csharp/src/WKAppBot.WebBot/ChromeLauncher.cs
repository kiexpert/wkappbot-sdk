using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace WKAppBot.WebBot;

/// <summary>
/// Launches Chrome with --remote-debugging-port and waits until the CDP endpoint is ready.
/// Searches common install paths on Windows, or uses PATH.
/// </summary>
public static class ChromeLauncher
{
    /// <summary>
    /// Focus theft callback: (stolenByHwnd, prevFgHwnd) — called when Chrome steals focus during restore.
    /// CLI layer hooks this to show FocuslessWarningOverlay.
    /// </summary>
    public static Action<IntPtr, IntPtr>? OnFocusTheft { get; set; }

    private static readonly string[] WindowsChromePaths =
    [
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Chrome", "Application", "chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google", "Chrome", "Application", "chrome.exe"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "Application", "chrome.exe"),
    ];

    /// <summary>Find chrome.exe on disk.</summary>
    public static string? FindChrome()
    {
        // Check common Windows paths
        foreach (var path in WindowsChromePaths)
        {
            if (File.Exists(path))
                return path;
        }

        // Try PATH
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "where",
                Arguments = "chrome.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);
            if (proc != null)
            {
                var output = proc.StandardOutput.ReadLine();
                proc.WaitForExit(3000);
                if (!string.IsNullOrEmpty(output) && File.Exists(output))
                    return output;
            }
        }
        catch { }

        return null;
    }

    /// <summary>
    /// Launch Chrome with remote debugging enabled.
    /// If Chrome is already running with the specified port, returns null (reuse existing).
    /// </summary>
    /// <param name="port">CDP port (default: 9222)</param>
    /// <param name="url">Initial URL to open (default: about:blank)</param>
    /// <param name="headless">Run headless (no visible window)</param>
    /// <param name="userDataDir">Custom user data dir (null = temp dir for isolation)</param>
    /// <param name="appMode">Launch in app mode (no address bar/tabs — clean WebView-like window).
    /// ★ MUST be true by default to avoid mixing with user's normal browser sessions!</param>
    /// <returns>The launched Process, or null if Chrome was already running on that port.</returns>
    public static async Task<Process?> LaunchAsync(
        int port = 9222,
        string? url = null,
        bool headless = false,
        string? userDataDir = null,
        bool appMode = false)
    {
        // Check if Chrome is already listening on this port
        if (await IsPortActiveAsync(port))
        {
            // Existing Chrome — don't touch its windows!
            // Caller uses CdpClient.EnsureCorrectWindowAsync() to handle positioning.
            return null;
        }

        var chromePath = FindChrome()
            ?? throw new FileNotFoundException("Chrome not found. Install Chrome or add it to PATH.");

        // Use temp dir for isolation if not specified
        userDataDir ??= Path.Combine(Path.GetTempPath(), $"wkappbot_chrome_{port}");

        // Kill any stale Chrome processes using our user-data-dir that aren't serving CDP
        // This prevents "profile lock" issues where a zombie Chrome holds the profile
        await KillStaleChromesAsync(userDataDir);

        // Re-check: KillStaleChromesAsync may have freed the port for an existing healthy Chrome
        if (await IsPortActiveAsync(port))
            return null;

        var arguments = $"--remote-debugging-port={port} --user-data-dir=\"{userDataDir}\"";
        // Isolation & performance flags
        arguments += " --no-first-run --no-default-browser-check";
        arguments += " --disable-background-networking --disable-backgrounding-occluded-windows";
        arguments += " --disable-sync --disable-translate --disable-extensions";
        arguments += " --disable-component-extensions-with-background-pages";
        arguments += " --disable-gpu --disable-software-rasterizer";  // Prevent GPU CPU spikes
        arguments += " --disable-dev-shm-usage";  // Prevent shared memory issues
        arguments += " --renderer-process-limit=2";  // Limit renderer processes
        arguments += " --disable-features=TranslateUI,BlinkGenPropertyTrees";
        arguments += " --disable-hang-monitor --disable-popup-blocking";
        arguments += " --disable-session-crashed-bubble --disable-infobars --hide-crash-restore-bubble";
        // Force accessibility tree for web content — enables UIA to read page headings, text, links
        arguments += " --force-renderer-accessibility";
        // Position at expected bounds from start. Focus guard handles focus theft.
        var bounds = CdpClient.ExpectedBounds;
        arguments += $" --window-size={bounds.W},{bounds.H} --window-position={bounds.X},{bounds.Y}";
        if (headless)
            arguments += " --headless=new";

        var targetUrl = url ?? "about:blank";
        if (appMode)
        {
            // App mode: no address bar, no tab bar, no bookmarks bar — clean WebView-like window
            arguments += $" --app=\"{targetUrl}\"";
        }
        else
        {
            arguments += $" \"{targetUrl}\"";
        }

        var psi = new ProcessStartInfo
        {
            FileName = chromePath,
            Arguments = arguments,
            UseShellExecute = false,
            WindowStyle = ProcessWindowStyle.Minimized, // launch iconic → no focus theft
        };

        var process = Process.Start(psi)
            ?? throw new InvalidOperationException("Failed to start Chrome");

        // Wait for CDP endpoint to become available
        using var http = new HttpClient();
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < 10000)
        {
            try
            {
                var json = await http.GetStringAsync($"http://localhost:{port}/json/version");
                if (!string.IsNullOrEmpty(json))
                {
                    // Position Chrome window at expected bounds.
                    // Focus guard in caller handles any focus theft.
                    try
                    {
                        var mainHwnd = FindChromeMainWindow(process.Id);
                        if (mainHwnd != IntPtr.Zero)
                        {
                            // Restore from minimized → focusless (SW_SHOWNOACTIVATE=4)
                            ShowWindow(mainHwnd, 4);
                            // SWP_NOACTIVATE(0x10)|SWP_NOZORDER(0x4)|SWP_NOOWNERZORDER(0x200)
                            SetWindowPos(mainHwnd, IntPtr.Zero, bounds.X, bounds.Y, bounds.W, bounds.H, 0x0214);
                            // Tag window as WKWebBot — SetPropW survives title changes from tab switches
                            SetPropW(mainHwnd, "wkwebbot", new IntPtr(1));
                        }
                    }
                    catch { }
                    return process;
                }
            }
            catch { }
            await Task.Delay(200);
        }

        throw new TimeoutException($"Chrome started but CDP endpoint not ready after 10s (port {port})");
    }

    /// <summary>Check if CDP is already active on a port (retries once on failure).</summary>
    public static async Task<bool> IsPortActiveAsync(int port)
    {
        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
        for (int attempt = 0; attempt < 2; attempt++)
        {
            try
            {
                var json = await http.GetStringAsync($"http://localhost:{port}/json/version");
                if (!string.IsNullOrEmpty(json)) return true;
            }
            catch { }
            if (attempt == 0) await Task.Delay(300);
        }
        return false;
    }

    /// <summary>
    /// Kill Chrome processes that use our user-data-dir but aren't serving CDP.
    /// This handles zombie/stale Chrome processes that hold the profile lock.
    /// </summary>
    private static async Task KillStaleChromesAsync(string userDataDir)
    {
        try
        {
            // Normalize path for comparison
            var normalizedDir = Path.GetFullPath(userDataDir).TrimEnd('\\', '/').ToLowerInvariant();

            foreach (var proc in Process.GetProcessesByName("chrome"))
            {
                try
                {
                    // Check if this Chrome uses our user-data-dir by examining command line
                    var cmdLine = GetCommandLine(proc.Id);
                    if (cmdLine == null) continue;

                    if (cmdLine.ToLowerInvariant().Contains(normalizedDir))
                    {
                        proc.Kill(entireProcessTree: true);
                        proc.WaitForExit(3000);
                    }
                }
                catch { } // Access denied etc — skip
            }

            // Wait a moment for file locks to release
            await Task.Delay(500);
        }
        catch { }
    }

    /// <summary>Get command line of a process via WMI.</summary>
    private static string? GetCommandLine(int pid)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = $"process where ProcessId={pid} get CommandLine /format:list",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using var proc = Process.Start(psi);
            if (proc == null) return null;
            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit(2000);
            // Format: CommandLine=...
            var idx = output.IndexOf("CommandLine=", StringComparison.OrdinalIgnoreCase);
            return idx >= 0 ? output.Substring(idx + 12).Trim() : null;
        }
        catch { return null; }
    }

    // P/Invoke
    [DllImport("user32.dll")]
    private static extern bool SetPropW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpString, IntPtr hData);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [StructLayout(LayoutKind.Sequential)]
    private struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public int ptMinPosition_x, ptMinPosition_y;
        public int ptMaxPosition_x, ptMaxPosition_y;
        public int rcNormalPosition_left, rcNormalPosition_top, rcNormalPosition_right, rcNormalPosition_bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    private static IntPtr FindChromeMainWindow(int pid)
    {
        IntPtr found = IntPtr.Zero;
        var sb = new System.Text.StringBuilder(256);
        EnumWindows((hwnd, _) =>
        {
            GetClassNameW(hwnd, sb, sb.Capacity);
            if (sb.ToString() != "Chrome_WidgetWin_1") return true;
            GetWindowThreadProcessId(hwnd, out int wndPid);
            if (wndPid == pid)
            {
                found = hwnd;
                return false;
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }
}
