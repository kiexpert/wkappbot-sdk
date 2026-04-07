using System.IO;
using System.Text;

namespace WKAppBot.CLI;

/// <summary>
/// TextWriter that writes to both the original Console.Out and a log file.
/// Install via Console.SetOut() at program start — all Console.Write/WriteLine
/// automatically goes to both destinations.
///
/// Features:
/// - Broken pipe safe: stdout IOException → suppress, file logging continues
/// - Crash safe: UnhandledException → ForceCloseWithoutMove() flushes + keeps in logs/
/// - Periodic flush: auto-flushes console every ~1 second (reduces output lag)
/// - File always AutoFlush=true (real-time log)
/// </summary>
public sealed class TeeTextWriter : TextWriter
{
    private readonly TextWriter _console;
    private readonly StreamWriter _file;
    private bool _moveToOldOnDispose;
    private string _logPath;
    private bool _pipeBroken;    // stdout broken → skip console writes, file continues
    private bool _fileClosed;    // ForceCloseWithoutMove called → skip all file writes
    private long _lastFlushTicks;  // for periodic console flush (~1s)
    private const long FlushIntervalTicks = TimeSpan.TicksPerSecond; // 1 second

    // Stderr spinner: shows braille-dot animation while stdout is silent >100ms
    private static readonly char[] SpinnerChars = { '⣾', '⣽', '⣻', '⢿', '⡿', '⣟', '⣯', '⣷' };
    private long _lastWriteMs; // accessed via Interlocked
    private volatile bool _spinnerShowing;
    private int _spinnerIdx;
    private Thread? _spinnerThread;
    private volatile bool _spinnerStopped;

    public TextWriter OriginalConsole => _console;
    public override Encoding Encoding => Encoding.UTF8;
    public bool IsPipeBroken => _pipeBroken;

    private readonly string _oldSubDir; // e.g. "newchat", "inspect", "web-github.com"

    public TeeTextWriter(TextWriter console, string logPath, bool moveToOldOnDispose = true, string oldSubDir = "")
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _moveToOldOnDispose = moveToOldOnDispose;
        _oldSubDir = oldSubDir;
        _logPath = logPath;
        _lastFlushTicks = Environment.TickCount64 * TimeSpan.TicksPerMillisecond;

        var dir = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        // Use FileShare.ReadWrite to allow other processes to read the log concurrently
        var stream = new FileStream(logPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        _file = new StreamWriter(stream, encoding: Encoding.UTF8)
        {
            AutoFlush = true
        };

        // Write header
        _file.WriteLine($"# WKAppBot Console Log — {DateTime.Now:yyyy-MM-dd HH:mm:ss} (PID={Environment.ProcessId})");
        _file.WriteLine($"# Command: {Environment.CommandLine}");
        _file.WriteLine();

        // Start spinner only when both stdout and stderr are a live TTY
        Interlocked.Exchange(ref _lastWriteMs, Environment.TickCount64);
        if (!Console.IsOutputRedirected && !Console.IsErrorRedirected)
        {
            _spinnerThread = new Thread(SpinnerLoop) { IsBackground = true, Name = "tee-spinner" };
            _spinnerThread.Start();
        }
    }

    public string LogPath => _logPath;

    /// <summary>Set before Dispose() — if non-zero, a one-line error summary is appended to errors.jsonl.</summary>
    public int ExitCode { get; set; } = 0;

    // ── Broken-pipe-safe console write helpers ──────────────────

    private void ConsoleWrite(char value)
    {
        if (_pipeBroken) return;
        try { _console.Write(value); }
        catch (IOException) { _pipeBroken = true; }
    }

    private void ConsoleWrite(string? value)
    {
        if (_pipeBroken) return;
        try { _console.Write(value); }
        catch (IOException) { _pipeBroken = true; }
    }

    private void ConsoleWrite(char[] buffer, int index, int count)
    {
        if (_pipeBroken) return;
        try { _console.Write(buffer, index, count); }
        catch (IOException) { _pipeBroken = true; }
    }

    private void ConsoleWriteLine()
    {
        if (_pipeBroken) return;
        try { _console.WriteLine(); }
        catch (IOException) { _pipeBroken = true; }
    }

    private void ConsoleWriteLine(string? value)
    {
        if (_pipeBroken) return;
        try { _console.WriteLine(value); }
        catch (IOException) { _pipeBroken = true; }
    }

    /// <summary>Flush console if ~1 second elapsed since last flush (reduces output lag for piped consumers)</summary>
    private void PeriodicFlush()
    {
        if (_pipeBroken) return;
        long now = Environment.TickCount64 * TimeSpan.TicksPerMillisecond;
        if (now - _lastFlushTicks >= FlushIntervalTicks)
        {
            try { _console.Flush(); }
            catch (IOException) { _pipeBroken = true; }
            _lastFlushTicks = now;
        }
    }

    // ── Core overrides ──────────────────────────────────────────

    /// <summary>Update last-write timestamp and erase spinner if showing.</summary>
    private void TouchWriteTime()
    {
        Interlocked.Exchange(ref _lastWriteMs, Environment.TickCount64);
        if (_spinnerShowing)
        {
            _spinnerShowing = false;
            try { Console.Error.Write("\b \b\x1b[?25h"); } catch { }
        }
    }

    /// <summary>Background thread: write braille spinner to stderr when stdout is silent >100ms.</summary>
    private void SpinnerLoop()
    {
        while (!_spinnerStopped)
        {
            Thread.Sleep(100);
            if (_spinnerStopped || _pipeBroken) break;
            var silentMs = Environment.TickCount64 - Interlocked.Read(ref _lastWriteMs);
            if (silentMs >= 100 && !_spinnerShowing)
            {
                try
                {
                    Console.Error.Write("\x1b[?25l"); // hide cursor
                    Console.Error.Write(SpinnerChars[_spinnerIdx % SpinnerChars.Length]);
                    _spinnerIdx++;
                    _spinnerShowing = true;
                }
                catch { _spinnerStopped = true; }
            }
            else if (_spinnerShowing && silentMs >= 100)
            {
                // Advance frame while still silent
                try
                {
                    Console.Error.Write('\b');
                    Console.Error.Write(SpinnerChars[_spinnerIdx % SpinnerChars.Length]);
                    _spinnerIdx++;
                }
                catch { _spinnerStopped = true; }
            }
        }
    }

    public override void Write(char value)
    {
        TouchWriteTime();
        ConsoleWrite(value);
        if (!_fileClosed) try { _file.Write(value); } catch { }
    }

    public override void Write(string? value)
    {
        TouchWriteTime();
        ConsoleWrite(value);
        if (!_fileClosed) try { _file.Write(value); } catch { }
        PeriodicFlush();
    }

    public override void Write(char[] buffer, int index, int count)
    {
        TouchWriteTime();
        ConsoleWrite(buffer, index, count);
        if (!_fileClosed) try { _file.Write(buffer, index, count); } catch { }
        PeriodicFlush();
    }

    public override void WriteLine()
    {
        TouchWriteTime();
        ConsoleWriteLine();
        if (!_fileClosed) try { _file.WriteLine(); } catch { }
    }

    public override void WriteLine(string? value)
    {
        TouchWriteTime();
        ConsoleWriteLine(value);
        if (!_fileClosed) try { _file.WriteLine(value); } catch { }
        PeriodicFlush();
    }

    public override void Flush()
    {
        if (!_pipeBroken)
        {
            try { _console.Flush(); }
            catch (IOException) { _pipeBroken = true; }
        }
        if (!_fileClosed) try { _file.Flush(); } catch { }
        _lastFlushTicks = Environment.TickCount64 * TimeSpan.TicksPerMillisecond;
    }

    /// <summary>Flush and close the log file WITHOUT moving to old/ — for crash dumps.
    /// Crash log stays in logs/ as evidence (not buried in old/).
    /// Safe to call from UnhandledException handler (abort imminent).</summary>
    public void ForceCloseWithoutMove()
    {
        _spinnerStopped = true;
        if (_spinnerShowing) { _spinnerShowing = false; try { Console.Error.Write("\b \b\x1b[?25h"); } catch { } }
        if (_fileClosed) return;
        try
        {
            if (_pipeBroken)
                _file.WriteLine($"\n# [PIPE] stdout broken — file logging continued normally");
            _file.WriteLine($"\n# [CRASH] Process terminated abnormally at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _file.Flush();
            _file.Dispose();
        }
        catch { }
        _fileClosed = true;
        _moveToOldOnDispose = false; // prevent Dispose from moving
    }

    // Noise tags to skip when building per-field last-value map
    private static readonly System.Collections.Generic.HashSet<string> _noiseTags =
        new(System.StringComparer.OrdinalIgnoreCase)
        {
            "LAUNCHER", "IOCP", "LOG", "CMD", "LAUNCH:CORE", "LAUNCH",
            "FOCUS", "EYE", "SUGGEST:WARN", "LOG:CORE", "+0.0s", "+",
        };

    /// <summary>
    /// Scan the log file and collect the last output line per [TAG],
    /// then append a JSON record { ts, exit, cmd, log, fields:{TAG:lastLine} } to errors.jsonl.
    /// </summary>
    private static void TryAppendErrorRecord(string logPath, int exitCode)
    {
        try
        {
            // Collect last value per [TAG] — forward scan, later lines overwrite earlier ones
            var fields = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);
            if (File.Exists(logPath))
            {
                foreach (var raw in File.ReadLines(logPath))
                {
                    var line = raw.Trim();
                    if (line.Length < 3 || line[0] != '[') continue;
                    var close = line.IndexOf(']');
                    if (close < 2 || close > 30) continue;
                    var tag = line[1..close];
                    if (_noiseTags.Contains(tag)) continue;
                    if (tag.StartsWith('+')) continue; // timestamp prefix lines
                    var msg = line[(close + 1)..].TrimStart();
                    if (string.IsNullOrEmpty(msg)) continue;
                    fields[tag] = msg.Length > 200 ? msg[..200] : msg;
                }
            }

            if (fields.Count == 0) return; // nothing interesting — skip

            var logsDir = Path.GetDirectoryName(logPath);
            // Step up from "old xxx/" subdirs to the parent logs/ dir
            while (logsDir != null && Path.GetFileName(logsDir).StartsWith("old", StringComparison.OrdinalIgnoreCase))
                logsDir = Path.GetDirectoryName(logsDir);
            if (string.IsNullOrEmpty(logsDir)) return;

            var errorsFile = Path.Combine(logsDir, "errors.jsonl");
            var cmd = Environment.CommandLine;
            if (cmd.Length > 300) cmd = cmd[..300];

            var record = System.Text.Json.JsonSerializer.Serialize(new
            {
                ts     = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                exit   = exitCode,
                cmd,
                log    = Path.GetFileName(logPath),
                fields,
            });
            File.AppendAllText(errorsFile, record + Environment.NewLine, Encoding.UTF8);
        }
        catch { /* best-effort */ }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _spinnerStopped = true;
            if (_spinnerShowing)
            {
                _spinnerShowing = false;
                try { Console.Error.Write("\b \b\x1b[?25h"); } catch { }
            }
        }
        if (disposing && !_fileClosed)
        {
            try
            {
                // Log broken pipe event if it happened
                if (_pipeBroken)
                    _file.WriteLine($"\n# [PIPE] stdout broken — file logging continued normally");

                _file.Flush();
                _file.Dispose();
            }
            catch { }
            _fileClosed = true;

            if (_moveToOldOnDispose)
            {
                try
                {
                    var dir = Path.GetDirectoryName(_logPath);
                    if (!string.IsNullOrWhiteSpace(dir))
                    {
                        var subDirName = string.IsNullOrWhiteSpace(_oldSubDir) ? "old" : $"old {_oldSubDir}";
                        var oldDir = Path.Combine(dir, subDirName);
                        Directory.CreateDirectory(oldDir);

                        var fileName = Path.GetFileName(_logPath);
                        var dest = Path.Combine(oldDir, fileName);
                        if (File.Exists(dest))
                        {
                            var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            dest = Path.Combine(oldDir, $"{Path.GetFileNameWithoutExtension(fileName)}.{stamp}{Path.GetExtension(fileName)}");
                        }

                        File.Move(_logPath, dest);
                        _logPath = dest;
                    }
                }
                catch
                {
                    // Best-effort on normal exit only.
                }

                // Append one-line error summary to errors.jsonl (exit≠0 only)
                if (ExitCode != 0)
                    TryAppendErrorRecord(_logPath, ExitCode);
            }
        }
        base.Dispose(disposing);
    }
}
