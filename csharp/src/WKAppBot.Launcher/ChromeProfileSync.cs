using System.Diagnostics;

namespace WKAppBot.Launcher;

/// <summary>
/// Chrome profile sync coordination -- waits for pending Chrome profile sync operations
/// to complete on Launcher exit before the OS closes file handles.
///
/// Synchronous blocking call (no async): called from AppBotExit which is the final
/// universal exit point in Launcher. Must complete before TerminateSelf.
/// </summary>
internal static class ChromeProfileSync
{
    /// <summary>
    /// Wait for pending Chrome profile sync operations to complete (timeout 2s).
    /// Runs synchronously in AppBotExit -- blocks until Chrome finishes sync or timeout.
    ///
    /// Chrome's profile sync (preferences, history, extensions) may have pending writes
    /// when the user closes the tab or terminal. Without waiting, the OS closes handles
    /// before sync completes, losing user state.
    /// </summary>
    internal static void WaitForPendingSync()
    {
        try
        {
            // Poll for any Chrome processes with pending profile sync.
            // Give them up to 2 seconds to complete.
            const int timeoutMs = 2000;
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                // Check if there are any Chrome processes still holding profile locks.
                // If found, sleep briefly and retry. If all released or timeout, continue exit.
                var chromeProcs = Process.GetProcessesByName("chrome");
                bool hasProfileSync = false;

                foreach (var proc in chromeProcs)
                {
                    try
                    {
                        // Try to open Chrome's profile directory.
                        // If it's locked by a sync operation, the open will fail.
                        // This is a heuristic check -- not foolproof but sufficient.
                        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        var profilePath = Path.Combine(appDataPath, "Google", "Chrome", "User Data", "Default");
                        if (Directory.Exists(profilePath))
                        {
                            // Try to access a sync-related file to detect if Chrome is still writing.
                            var prefsPath = Path.Combine(profilePath, "Preferences");
                            if (File.Exists(prefsPath))
                            {
                                try
                                {
                                    using (var fs = File.Open(prefsPath, FileMode.Open, FileAccess.Read, FileShare.None))
                                    {
                                        fs.Close();
                                    }
                                }
                                catch (IOException)
                                {
                                    // File is locked by Chrome -- sync still in progress
                                    hasProfileSync = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch { /* ignore errors during check */ }
                }

                if (!hasProfileSync) break; // All profiles synced or no Chrome processes
                System.Threading.Thread.Sleep(50); // Brief sleep before retry
            }
        }
        catch { /* best-effort -- don't block exit on any error */ }
    }
}
