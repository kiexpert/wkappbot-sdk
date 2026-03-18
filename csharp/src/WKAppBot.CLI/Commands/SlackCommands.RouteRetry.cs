using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// Route retry queue — persists failed route attempts to disk, replayed by Eye loop every ~10s.
// Infinite retry: no max retryCount limit ("사용자의 요청은 한개도 허투로 듣지 않게").
// Eye-resilient: Enqueue() ensures Eye is running so retry won't sit on disk indefinitely.
internal static class RouteRetryQueue
{
    static string QueuePath
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime", "route_retry.jsonl");
        }
    }

    static readonly object _lock = new();

    /// <summary>
    /// Persists a failed route attempt for retry after 1 minute.
    /// Also ensures Eye is running so the retry file won't sit forever if Eye was killed.
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

            Console.WriteLine($"[RETRY] Queued retry #{retryCount} at {DateTimeOffset.FromUnixTimeSeconds(retryAt):HH:mm:ss}");

            // Ensure Eye is running to process this retry.
            // No-op if already alive; re-spawns if Claude killed it and forgot.
            EnsureEyeRunning();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] Enqueue error: {ex.Message}");
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
            // Try to connect to Eye's named pipe (150ms timeout)
            using var pipe = new NamedPipeClientStream(".", EyeCmdPipeServer.PipeName,
                PipeDirection.InOut, PipeOptions.None);
            pipe.Connect(150);
            return; // Eye is alive
        }
        catch { } // timeout → Eye is not running

        // Eye is down — spawn it so it can process the retry queue
        var exePath = Environment.ProcessPath;
        if (string.IsNullOrEmpty(exePath)) return;

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName        = exePath,
                Arguments       = "eye",
                UseShellExecute = false,
                CreateNoWindow  = true,
            };
            var proc = Process.Start(psi);
            Console.WriteLine(proc != null
                ? $"[RETRY] Eye was down — respawned (PID={proc.Id})"
                : "[RETRY] Eye spawn returned null");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RETRY] Eye spawn error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns all due items (retryAt &lt;= now) and removes them from the queue file.
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
}
