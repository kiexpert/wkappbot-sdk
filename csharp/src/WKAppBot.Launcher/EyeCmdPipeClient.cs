using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;
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
        // Eye pipe sends UTF-8 text. We decode it to Unicode strings via StreamReader,
        // then re-encode to terminal CP via WideCharToMultiByte (NativeAOT-safe, no managed code pages).
        // Encoding.GetEncoding(949) is NOT used — NativeAOT trims code page support; silent UTF-8
        // fallback caused garbled Korean in CMD. Win32 WideCharToMultiByte has no such limitation.
        var rawStdout = Console.OpenStandardOutput();
        var cp = Program._consoleCodePage;
        var lineEnd = "\r\n"u8.ToArray(); // Windows line ending as bytes
        Action<string> writeLine = cp > 0 && cp != 65001
            ? line => WriteLineW(rawStdout, line, cp, lineEnd)
            : line => { var b = Encoding.UTF8.GetBytes(line + "\r\n"); rawStdout.Write(b, 0, b.Length); rawStdout.Flush(); };
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

            bool gotEndMarker = false;
            int outputLines = 0;
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
                        gotEndMarker = true;
                        break;
                    }
                    writeLine(line);
                    outputLines++;
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

            // Empty pipe guard: pipe closed without EndMarker and without any output
            // → silent failure (process hard-killed or crashed before writing anything)
            if (!timedOut && !gotEndMarker && outputLines == 0 && peekedLine == null)
            {
                Console.Error.WriteLine("[PIPE] empty closed — process exited without output or end marker (possible crash/kill)");
                exitCode = -1;
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

    // CharSet.Unicode: prevents char[] from being marshaled as ANSI bytes.
    // Without it, Korean chars become '?' before Win32 sees them.
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WideCharToMultiByte")]
    static extern int WideCharToMultiByte(int codePage, uint dwFlags,
        char[] lpWideCharStr, int cchWideChar,
        byte[] lpMultiByteStr, int cbMultiByte,
        nint lpDefaultChar, nint lpUsedDefaultChar);

    static readonly byte[] _wcBuf = new byte[65536];
    static readonly char[] _charBuf = new char[32768];

    static void WriteLineW(Stream stdout, string line, int codePage, byte[] lineEnd)
    {
        // Encode string to target code page via Win32 WideCharToMultiByte.
        // Works in NativeAOT — no managed Encoding stack required.
        int len = line.Length;
        if (len > _charBuf.Length) len = _charBuf.Length;
        line.CopyTo(0, _charBuf, 0, len);
        int n = WideCharToMultiByte(codePage, 0, _charBuf, len, _wcBuf, _wcBuf.Length, 0, 0);
        if (n > 0) stdout.Write(_wcBuf, 0, n);
        stdout.Write(lineEnd, 0, lineEnd.Length);
        stdout.Flush();
    }
}
