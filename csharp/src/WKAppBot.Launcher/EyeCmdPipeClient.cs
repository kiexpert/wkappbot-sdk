using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Launcher;

[JsonSerializable(typeof(string[]))]
internal partial class LauncherJsonContext : JsonSerializerContext { }

/// <summary>Connects to Eye's command pipe and delegates execution (zero cold-start).</summary>
internal static class EyeCmdPipeClient
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";

    /// <summary>
    /// Try to delegate args to the running Eye process.
    /// Returns true + sets exitCode if delegation succeeded.
    /// Returns false only if Eye is not running/busy (caller should fall through to RunCore).
    ///
    /// timeoutMs: if >0, close the pipe after this many ms (enforces Launcher-level timeout
    /// even for Eye in-process commands that don't implement their own timeout).
    /// firstOutputTimeoutMs: if >0, fall back to Core if Eye produces no output within this time.
    ///   Use for fast commands (file edit/read/grep) where Eye stall → Core is safer than waiting.
    ///   Returns false so caller runs Core; pipe is closed (Eye gets BrokenPipe and skips command).
    /// </summary>
    public static bool TryDelegate(string[] args, out int exitCode, int timeoutMs = 0, int timeoutExitCode = 2, int firstOutputTimeoutMs = 0)
    {
        exitCode = 0;
        bool connected = false;
        var pipe = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
        // Write pipe output to raw stdout as UTF-8 — same as Core subprocess (StandardOutputEncoding=UTF8).
        // Console.WriteLine uses Console.OutputEncoding which may differ; raw stream is reliable.
        var rawOut = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.UTF8) { AutoFlush = true };
        try
        {
            pipe.Connect(300); // 300ms: Eye either answers instantly or isn't running
            connected = true;

            var w = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };
            var r = new StreamReader(pipe, leaveOpen: true);

            // Prepend CWD + foreground HWND hint so Eye can resolve caller window directly
            var fgHwnd = GetForegroundWindow();
            var hwndPrefix = fgHwnd != IntPtr.Zero ? $"__hwnd:0x{fgHwnd.ToInt64():X8}" : null;
            var prefixes = new[] { $"__cwd:{Environment.CurrentDirectory}" }
                .Concat(hwndPrefix != null ? new[] { hwndPrefix } : Array.Empty<string>());
            var payload = prefixes.Concat(args).ToArray();
            w.WriteLine(JsonSerializer.Serialize(payload, LauncherJsonContext.Default.StringArray));

            // First-output timeout: for fast commands, if Eye doesn't respond within N ms,
            // close the pipe and return false → caller falls back to Core directly.
            string? peekedLine = null;
            if (firstOutputTimeoutMs > 0)
            {
                var firstReadTask = Task.Run(() => r.ReadLine());
                if (!firstReadTask.Wait(firstOutputTimeoutMs))
                {
                    // Eye is stalled — fall back to Core. Eye will get BrokenPipe and skip.
                    try { pipe.Close(); } catch { }
                    return false;
                }
                peekedLine = firstReadTask.Result;
            }

            // Timeout: fire timer closes pipe → unblocks ReadLine with IOException.
            // Eye continues executing in background; Launcher returns timeout exit code.
            bool timedOut = false;
            Timer? timeoutTimer = null;
            if (timeoutMs > 0)
            {
                timeoutTimer = new Timer(_ =>
                {
                    timedOut = true;
                    try { pipe.Close(); } catch { }
                }, null, timeoutMs, Timeout.Infinite);
            }

            try
            {
                // Drain: process peeked line first (if first-output timeout was used), then loop.
                IEnumerable<string?> Lines()
                {
                    if (peekedLine != null) yield return peekedLine;
                    string? l;
                    while ((l = r.ReadLine()) != null) yield return l;
                }

                foreach (var line in Lines())
                {
                    if (line == null) break;
                    if (line.StartsWith(EndMarker))
                    {
                        int.TryParse(line.AsSpan(EndMarker.Length).Trim(), out exitCode);
                        break;
                    }
                    rawOut.WriteLine(line);
                }
                if (timedOut) exitCode = timeoutExitCode;
            }
            catch (Exception) when (timedOut)
            {
                exitCode = timeoutExitCode;
            }
            finally
            {
                timeoutTimer?.Dispose();
            }

            return true;
        }
        catch
        {
            return connected; // false = Eye not running; true = connected but read failed
        }
        finally
        {
            try { pipe.Dispose(); } catch { }
        }
    }

    [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
}
