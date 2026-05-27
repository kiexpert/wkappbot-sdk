using System.Diagnostics;

namespace WKAppBot.Launcher;

/// <summary>
/// OOM watchdog for ask-gpt subprocesses: kills them if WorkingSet64 > 2GB
/// or if the process hasn't completed within timeout+60s.
/// Prevents memory leak cascades (4-5GB per zombie ask-gpt process).
/// </summary>
partial class Program
{
    /// <summary>
    /// Start monitoring an ask-gpt subprocess for OOM.
    /// Kills process if WorkingSet64 > 2GB or after timeout+60s.
    /// </summary>
    static void StartAskGptWatchdog(int processId, int timeoutSeconds)
    {
        Task.Run(async () =>
        {
            try
            {
                await MonitorMemoryLeakAsync(processId, timeoutSeconds);
            }
            catch (Exception ex)
            {
                // Silently ignore watchdog errors - don't break the main command
                if (_prof)
                    try { Console.Error.WriteLine($"[ASK-GPT-WATCHDOG] Error: {ex.Message}"); } catch { }
            }
        });
    }

    /// <summary>
    /// Monitor ask-gpt subprocess every 500ms.
    /// Kill if WorkingSet64 > 2147483648 bytes (2GB) or timeout+60s elapsed.
    /// </summary>
    static async Task MonitorMemoryLeakAsync(int processId, int timeoutSeconds)
    {
        const long OOM_THRESHOLD = 2_147_483_648; // 2GB in bytes
        var timeoutMs = timeoutSeconds * 1000;
        var killAfterMs = timeoutMs + 60_000; // timeout + 60s = force kill
        var startTime = DateTime.UtcNow;
        var checkIntervalMs = 500;

        while (true)
        {
            await Task.Delay(checkIntervalMs);

            var elapsed = (DateTime.UtcNow - startTime).TotalMilliseconds;

            // Check if process is still running
            Process? proc = null;
            try
            {
                proc = Process.GetProcessById(processId);
            }
            catch (ArgumentException)
            {
                // Process already exited - watchdog done
                return;
            }

            // Check memory (WorkingSet64 = physical RAM used)
            var memoryBytes = proc?.WorkingSet64 ?? 0;
            if (memoryBytes > OOM_THRESHOLD)
            {
                Prof($"[ASK-GPT-WATCHDOG] PID {processId} OOM detected: {memoryBytes / 1024 / 1024}MB > 2GB threshold");
                KillProcessByPid(processId);
                return;
            }

            // Check timeout+60s deadline
            if (elapsed > killAfterMs)
            {
                Prof($"[ASK-GPT-WATCHDOG] PID {processId} timeout+60s exceeded ({elapsed}ms)");
                KillProcessByPid(processId);
                return;
            }
        }
    }

    /// <summary>
    /// Kill a process by PID using taskkill --force (safe routing per CLAUDE.md).
    /// </summary>
    static void KillProcessByPid(int processId)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "taskkill.exe",
                Arguments = $"/F /PID {processId}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            using var proc = Process.Start(psi);
            if (proc != null)
            {
                proc.WaitForExit(5000); // Wait up to 5s for taskkill to complete
                Prof($"[ASK-GPT-WATCHDOG] taskkill /F /PID {processId} exit code: {proc.ExitCode}");
            }
        }
        catch (Exception ex)
        {
            if (_prof)
                try { Console.Error.WriteLine($"[ASK-GPT-WATCHDOG] taskkill failed: {ex.Message}"); } catch { }
        }
    }
}
