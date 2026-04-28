// SPDX-License-Identifier: MIT
using System.IO;

namespace WKAppBot.Shared;

/// <summary>
/// File-based side channel for pushing user input from the Launcher to the Core
/// process during MCP / detached-stdio runs. Core's stdin is typically closed in
/// those modes (RunCoreDetachedIocp redirects to a pipe Launcher owns), so the
/// user has no direct way to interrupt or redirect a long-running loop. This
/// channel offers a minimal path:
///
///   Launcher side (reads its own stdin via NonBlockingLineReader):
///       InterruptChannel.Write(corePid, line);
///
///   Core side (inside the loop or command that supports interrupts):
///       foreach (var pending in InterruptChannel.Drain()) ...
///
/// Why a file:
///   - Launcher and Core are separate processes with no direct pipe in detached
///     mode; an on-disk staging file needs no OS-level handshake.
///   - Drain() is an atomic "read all + delete" so the same line is never
///     observed twice even if multiple threads poll.
///   - Overhead is negligible (a couple ms, polled between steps).
///
/// Limitations:
///   - Lines are not delivered instantly; polling cadence is up to the consumer
///     (typical: once per loop iteration or before each long-running step).
///   - Single-file per PID -- if Core spawns grandchildren that also want to
///     receive, they would need their own channel keyed on their PID.
/// </summary>
public static class InterruptChannel
{
    /// <summary>
    /// Path for the interrupt staging file of the given process. Placed in
    /// %TEMP% so it survives across wkappbot restarts if needed but is still
    /// subject to the normal temp cleanup policies.
    /// </summary>
    public static string FilePath(int pid) =>
        Path.Combine(Path.GetTempPath(), $"wkappbot-interrupt-{pid}.txt");

    /// <summary>
    /// Append a single line to the given process's interrupt channel. Each line
    /// is prefixed with an ISO-ish timestamp so Drain consumers can see how old
    /// the interrupt is and decide whether to honor it.
    /// </summary>
    public static void Write(int corePid, string line)
    {
        if (corePid <= 0) return;
        if (string.IsNullOrEmpty(line)) return;
        try
        {
            var ts = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            File.AppendAllText(FilePath(corePid), $"[{ts}] {line}\n");
        }
        catch { /* best-effort: never throw from a background forwarder */ }
    }

    /// <summary>
    /// Atomically read and remove all pending interrupt lines for the CURRENT
    /// process. Returns an empty array if the channel file is missing or empty.
    /// Each returned string includes the timestamp prefix: "[yyyy-MM-ddTHH:mm:ss.fff] &lt;line&gt;".
    /// </summary>
    public static string[] Drain()
    {
        var path = FilePath(Environment.ProcessId);
        try
        {
            if (!File.Exists(path)) return Array.Empty<string>();
            // Move aside first so a concurrent Write doesn't lose data.
            var snap = path + ".draining";
            try { File.Delete(snap); } catch { }
            File.Move(path, snap);
            try
            {
                var content = File.ReadAllText(snap);
                return content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            }
            finally
            {
                try { File.Delete(snap); } catch { }
            }
        }
        catch { return Array.Empty<string>(); }
    }

    /// <summary>
    /// Quick check without consuming -- returns true if the channel file exists
    /// and is non-empty. Useful for spinning fast checks in hot paths.
    /// </summary>
    public static bool HasPending()
    {
        var path = FilePath(Environment.ProcessId);
        try
        {
            var fi = new FileInfo(path);
            return fi.Exists && fi.Length > 0;
        }
        catch { return false; }
    }

    /// <summary>
    /// Remove the channel file for the given process. Call on channel-consumer
    /// shutdown to avoid leaving stale entries behind when Core exits mid-write.
    /// </summary>
    public static void Cleanup(int pid)
    {
        try { File.Delete(FilePath(pid)); } catch { }
    }
}