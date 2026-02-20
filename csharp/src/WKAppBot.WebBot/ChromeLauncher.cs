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
    /// <param name="appMode">Launch in app mode (no address bar/tabs — clean WebView-like window)</param>
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
            return null;

        var chromePath = FindChrome()
            ?? throw new FileNotFoundException("Chrome not found. Install Chrome or add it to PATH.");

        // Use temp dir for isolation if not specified
        userDataDir ??= Path.Combine(Path.GetTempPath(), $"wkappbot_chrome_{port}");

        // Kill any stale Chrome processes using our user-data-dir that aren't serving CDP
        // This prevents "profile lock" issues where a zombie Chrome holds the profile
        await KillStaleChromesAsync(userDataDir);

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
        // Small window (will be minimized anyway, but keeps it unobtrusive if restored)
        arguments += " --window-size=400,300 --window-position=0,0";
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
                    return process;
            }
            catch { }
            await Task.Delay(200);
        }

        throw new TimeoutException($"Chrome started but CDP endpoint not ready after 10s (port {port})");
    }

    /// <summary>Check if CDP is already active on a port.</summary>
    public static async Task<bool> IsPortActiveAsync(int port)
    {
        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
        try
        {
            var json = await http.GetStringAsync($"http://localhost:{port}/json/version");
            return !string.IsNullOrEmpty(json);
        }
        catch
        {
            return false;
        }
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
}
