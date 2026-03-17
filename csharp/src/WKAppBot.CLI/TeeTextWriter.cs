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
    }

    public string LogPath => _logPath;

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

    public override void Write(char value)
    {
        ConsoleWrite(value);
        if (!_fileClosed) try { _file.Write(value); } catch { }
    }

    public override void Write(string? value)
    {
        ConsoleWrite(value);
        if (!_fileClosed) try { _file.Write(value); } catch { }
        PeriodicFlush();
    }

    public override void Write(char[] buffer, int index, int count)
    {
        ConsoleWrite(buffer, index, count);
        if (!_fileClosed) try { _file.Write(buffer, index, count); } catch { }
        PeriodicFlush();
    }

    public override void WriteLine()
    {
        ConsoleWriteLine();
        if (!_fileClosed) try { _file.WriteLine(); } catch { }
    }

    public override void WriteLine(string? value)
    {
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

    protected override void Dispose(bool disposing)
    {
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
                        var subDirName = string.IsNullOrWhiteSpace(_oldSubDir) ? "old" : $"old-{_oldSubDir}";
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
            }
        }
        base.Dispose(disposing);
    }
}
