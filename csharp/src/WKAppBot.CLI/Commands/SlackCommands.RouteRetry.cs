using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// Route retry queue — persists failed route attempts to disk.
// Resilience layers:
//   1. Eye loop (~10s polling)
//   2. Windows Task Scheduler one-shot at exact retryAt time (Eye-independent)
//   3. Enqueue() EnsureEyeRunning — respawns Eye if dead
// Infinite retry: no max retryCount ("사용자의 요청은 한개도 허투로 듣지 않게").
internal static class RouteRetryQueue
{
    const string TaskName = "WKAppBot Route Retry";

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
    /// Registers a Windows Task Scheduler one-shot at the exact retryAt time.
    /// Also ensures Eye is running as a fallback.
    /// </summary>
    public static void Enqueue(JsonNode msgNode, int retryCount)
    {
        try
        {
            var retryAt = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds();

            // Clone to avoid mutating caller's node
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

            var fireAt = DateTimeOffset.FromUnixTimeSeconds(retryAt);
            Console.WriteLine($"[RETRY] Queued retry #{retryCount} at {fireAt.LocalDateTime:HH:mm:ss}");

            // Register one-shot task at exact retryAt (or earlier if another item is sooner)
            ScheduleNextTask();

            // Fallback: ensure Eye is also running
            EnsureEyeRunning();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] Enqueue error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns all due items (retryAt &lt;= now) and removes them from the queue file.
    /// After returning due items, re-schedules the next one-shot for any remaining items.
    /// Thread-safe. Returns empty list if queue file doesn't exist.
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
                    if (retryAt <= nowEpoch)
                        due.Add(line);
                    else
                        pending.Add(line);
                }

                // Rewrite file with only non-due items
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
    /// Reads the queue and registers a Windows Task Scheduler one-shot at the soonest retryAt.
    /// Called by Eye startup and after each flush — keeps the task in sync with the actual queue.
    /// If queue is empty, deletes the task (no-op if already gone).
    /// </summary>
    public static void ScheduleNextTask()
    {
        try
        {
            var soonest = GetSoonestRetryAt();
            if (soonest == null)
            {
                // Queue empty — remove task if it exists
                DeleteScheduledTask();
                return;
            }

            // Add a small buffer so eye tick runs after retryAt, not before
            var fireAt = soonest.Value.AddSeconds(5);
            if (fireAt < DateTimeOffset.Now.AddSeconds(10))
                fireAt = DateTimeOffset.Now.AddSeconds(10); // minimum 10s from now

            RegisterOneshotTask(fireAt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] ScheduleNextTask error: {ex.Message}");
        }
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

    /// <summary>
    /// Registers a one-shot Windows Task Scheduler task at the given time.
    /// Uses -Force to overwrite if already exists. Task auto-deletes 30s after firing.
    /// </summary>
    static void RegisterOneshotTask(DateTimeOffset fireAt)
    {
        var exePath = Environment.ProcessPath;
        if (string.IsNullOrEmpty(exePath)) return;

        // Format for PowerShell: local time string
        var fireLocal = fireAt.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        var ps = $"""
$action   = New-ScheduledTaskAction -Execute '{exePath}' -Argument 'eye tick'
$trigger  = New-ScheduledTaskTrigger -Once -At '{fireLocal}'
$settings = New-ScheduledTaskSettingsSet `
    -ExecutionTimeLimit (New-TimeSpan -Minutes 2) `
    -MultipleInstances IgnoreNew `
    -Hidden `
    -StartWhenAvailable $true `
    -DeleteExpiredTaskAfter (New-TimeSpan -Seconds 30)
Register-ScheduledTask -TaskName '{TaskName}' -Action $action -Trigger $trigger -Settings $settings -Force | Out-Null
""";

        RunPowerShell(ps, out var exitCode);
        Console.WriteLine(exitCode == 0
            ? $"[RETRY] Scheduled one-shot: {fireLocal} ({TaskName})"
            : $"[RETRY] Task register exit={exitCode} (non-fatal)");
    }

    static void DeleteScheduledTask()
    {
        var ps = $"Unregister-ScheduledTask -TaskName '{TaskName}' -Confirm:$false -ErrorAction SilentlyContinue";
        RunPowerShell(ps, out _);
    }

    static void RunPowerShell(string script, out int exitCode)
    {
        exitCode = -1;
        try
        {
            // Escape double-quotes inside the -Command argument
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
    static void EnsureEyeRunning()
    {
        if (Program.RunningInEye) return; // we ARE Eye — no need to spawn

        try
        {
            using var pipe = new NamedPipeClientStream(".", EyeCmdPipeServer.PipeName,
                PipeDirection.InOut, PipeOptions.None);
            pipe.Connect(150);
            return; // Eye is alive
        }
        catch { } // timeout → Eye is not running

        // Eye is down — spawn it
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
