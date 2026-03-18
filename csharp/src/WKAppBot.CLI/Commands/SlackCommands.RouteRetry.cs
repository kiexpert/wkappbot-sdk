using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// Route retry queue — persists failed route attempts to disk.
// Dual Task Scheduler structure:
//   [WKAppBot Eye Watchdog]  permanent 10-min repeat — keeps Eye alive regardless
//   [WKAppBot Route Retry]   precise one-shot at retryAt — fast retry when item queued
// Plus Eye loop (~10s) and EnsureEyeRunning() as additional layers.
// Infinite retry: no max retryCount ("사용자의 요청은 한개도 허투로 듣지 않게").
internal static class RouteRetryQueue
{
    const string WatchdogTaskName = "WKAppBot Eye Watchdog";
    const string RetryTaskName    = "WKAppBot Route Retry";

    static string QueuePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "route_retry.jsonl");
        }
    }

    static readonly object _lock = new();

    // ── Public API ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Persists a failed route attempt for retry after 1 minute.
    /// Registers a precise one-shot Task Scheduler at retryAt.
    /// Also ensures Eye is running as an immediate fallback.
    /// </summary>
    public static void Enqueue(JsonNode msgNode, int retryCount)
    {
        try
        {
            var retryAt = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds();

            var clone = JsonNode.Parse(msgNode.ToJsonString())!;
            clone["retryCount"] = retryCount;
            clone["retryAt"]    = retryAt;

            var line = clone.ToJsonString();
            lock (_lock)
            {
                var dir = Path.GetDirectoryName(QueuePath)!;
                Directory.CreateDirectory(dir);
                File.AppendAllText(QueuePath, line + "\n");
            }

            Console.WriteLine($"[RETRY] Queued retry #{retryCount} at {DateTimeOffset.FromUnixTimeSeconds(retryAt).LocalDateTime:HH:mm:ss}");

            // Schedule precise one-shot at soonest retryAt
            ScheduleRetryTask();

            // Immediate fallback: spawn Eye if it's dead
            EnsureEyeRunning();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] Enqueue error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns all due items (retryAt &lt;= now) and removes them from the queue file.
    /// After returning, re-schedules the retry task for remaining items (or removes it).
    /// </summary>
    public static List<string> GetDueItems()
    {
        if (!File.Exists(QueuePath)) return [];

        lock (_lock)
        {
            try
            {
                var allLines = File.ReadAllLines(QueuePath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .ToList();

                if (allLines.Count == 0) return [];

                var nowEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var due      = new List<string>();
                var pending  = new List<string>();

                foreach (var line in allLines)
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var retryAt = node["retryAt"]?.GetValue<long>() ?? 0;
                    if (retryAt <= nowEpoch) due.Add(line);
                    else pending.Add(line);
                }

                if (due.Count > 0)
                    File.WriteAllLines(QueuePath, pending);

                return due;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RETRY] GetDueItems error: {ex.Message}");
                return [];
            }
        }
    }

    /// <summary>
    /// Reads the queue and schedules a precise one-shot at the soonest retryAt.
    /// If queue is empty, removes the retry task (watchdog still runs every 10 min).
    /// Called after flush and on Eye startup to keep the task in sync.
    /// </summary>
    public static void ScheduleRetryTask()
    {
        try
        {
            var soonest = GetSoonestRetryAt();
            if (soonest == null)
            {
                // Queue empty — remove one-shot retry task (watchdog continues independently)
                RunPowerShell($"Unregister-ScheduledTask -TaskName '{RetryTaskName}' -Confirm:$false -ErrorAction SilentlyContinue", out _);
                return;
            }

            // Fire slightly after retryAt to ensure item is actually due
            var fireAt = soonest.Value.AddSeconds(5);
            if (fireAt < DateTimeOffset.Now.AddSeconds(10))
                fireAt = DateTimeOffset.Now.AddSeconds(10);

            var fireLocal = fireAt.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            var exePath = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exePath)) return;

            var ps = $"""
$action   = New-ScheduledTaskAction -Execute '{exePath}' -Argument 'eye tick'
$trigger  = New-ScheduledTaskTrigger -Once -At '{fireLocal}'
$settings = New-ScheduledTaskSettingsSet `
    -ExecutionTimeLimit (New-TimeSpan -Minutes 2) `
    -MultipleInstances IgnoreNew `
    -Hidden `
    -StartWhenAvailable $true `
    -DeleteExpiredTaskAfter (New-TimeSpan -Seconds 30)
Register-ScheduledTask -TaskName '{RetryTaskName}' -Action $action -Trigger $trigger -Settings $settings -Force | Out-Null
""";
            RunPowerShell(ps, out var exitCode);
            Console.WriteLine(exitCode == 0
                ? $"[RETRY] One-shot task at {fireLocal}"
                : $"[RETRY] One-shot task register exit={exitCode} (non-fatal)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] ScheduleRetryTask error: {ex.Message}");
        }
    }

    /// <summary>
    /// Registers (or refreshes) the permanent Eye watchdog task.
    /// Runs 'eye tick' every 10 min — respawns Eye if dead, flushes retry queue.
    /// Called on Eye startup. Never deleted — permanent watchdog.
    /// </summary>
    public static void EnsureWatchdogTask()
    {
        var exePath = Environment.ProcessPath;
        if (string.IsNullOrEmpty(exePath)) return;

        var ps = $"""
$action   = New-ScheduledTaskAction -Execute '{exePath}' -Argument 'eye tick'
$trigger  = New-ScheduledTaskTrigger -Once -At (Get-Date).AddMinutes(1) -RepetitionInterval (New-TimeSpan -Minutes 10)
$settings = New-ScheduledTaskSettingsSet `
    -ExecutionTimeLimit (New-TimeSpan -Minutes 2) `
    -MultipleInstances IgnoreNew `
    -Hidden `
    -StartWhenAvailable $true
Register-ScheduledTask -TaskName '{WatchdogTaskName}' -Action $action -Trigger $trigger -Settings $settings -Force | Out-Null
""";
        RunPowerShell(ps, out var exitCode);
        Console.WriteLine(exitCode == 0
            ? $"[EYE] Watchdog task registered: '{WatchdogTaskName}' (10-min repeat)"
            : $"[EYE] Watchdog task register exit={exitCode} (non-fatal)");
    }

    // ── Internals ───────────────────────────────────────────────────────────────

    static DateTimeOffset? GetSoonestRetryAt()
    {
        if (!File.Exists(QueuePath)) return null;
        try
        {
            long? soonest = null;
            foreach (var line in File.ReadAllLines(QueuePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var node = JsonNode.Parse(line);
                var retryAt = node?["retryAt"]?.GetValue<long>();
                if (retryAt.HasValue && (soonest == null || retryAt < soonest))
                    soonest = retryAt;
            }
            return soonest.HasValue ? DateTimeOffset.FromUnixTimeSeconds(soonest.Value) : null;
        }
        catch { return null; }
    }

    static void RunPowerShell(string script, out int exitCode)
    {
        exitCode = -1;
        try
        {
            var escaped = script.Replace("\"", "\\\"");
            var psi = new ProcessStartInfo
            {
                FileName               = "powershell.exe",
                Arguments              = $"-NoProfile -NonInteractive -Command \"{escaped}\"",
                UseShellExecute        = false,
                CreateNoWindow         = true,
                RedirectStandardOutput = false,
                RedirectStandardError  = false,
            };
            var proc = Process.Start(psi);
            proc?.WaitForExit(5000);
            exitCode = proc?.ExitCode ?? -1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] PowerShell error: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks if Eye is alive via CmdPipe. If not, spawns it as a detached background process.
    /// </summary>
    public static void EnsureEyeRunning()
    {
        if (Program.RunningInEye) return;

        try
        {
            using var pipe = new NamedPipeClientStream(".", EyeCmdPipeServer.PipeName,
                PipeDirection.InOut, PipeOptions.None);
            pipe.Connect(150);
            return; // alive
        }
        catch { }

        var exePath = Environment.ProcessPath;
        if (string.IsNullOrEmpty(exePath)) return;

        try
        {
            var proc = Process.Start(new ProcessStartInfo
            {
                FileName        = exePath,
                Arguments       = "eye",
                UseShellExecute = false,
                CreateNoWindow  = true,
            });
            Console.WriteLine(proc != null
                ? $"[RETRY] Eye was down — respawned (PID={proc.Id})"
                : "[RETRY] Eye spawn returned null");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] Eye spawn error: {ex.Message}");
        }
    }
}
