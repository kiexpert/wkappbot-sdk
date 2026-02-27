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
/// - Periodic flush: auto-flushes console every ~1 second (reduces output lag)
/// - File always AutoFlush=true (real-time log)
/// </summary>
public sealed class TeeTextWriter : TextWriter
{
    private readonly TextWriter _console;
    private readonly StreamWriter _file;
    private readonly bool _moveToOldOnDispose;
    private string _logPath;
    private bool _pipeBroken;  // stdout broken → skip console writes, file continues
    private long _lastFlushTicks;  // for periodic console flush (~1s)
    private const long FlushIntervalTicks = TimeSpan.TicksPerSecond; // 1 second

    public TextWriter OriginalConsole => _console;
    public override Encoding Encoding => Encoding.UTF8;
    public bool IsPipeBroken => _pipeBroken;

    public TeeTextWriter(TextWriter console, string logPath, bool moveToOldOnDispose = true)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _moveToOldOnDispose = moveToOldOnDispose;
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
        _file.Write(value);
    }

    public override void Write(string? value)
    {
        ConsoleWrite(value);
        _file.Write(value);
        PeriodicFlush();
    }

    public override void Write(char[] buffer, int index, int count)
    {
        ConsoleWrite(buffer, index, count);
        _file.Write(buffer, index, count);
        PeriodicFlush();
    }

    public override void WriteLine()
    {
        ConsoleWriteLine();
        _file.WriteLine();
    }

    public override void WriteLine(string? value)
    {
        ConsoleWriteLine(value);
        _file.WriteLine(value);
        PeriodicFlush();
    }

    public override void Flush()
    {
        if (!_pipeBroken)
        {
            try { _console.Flush(); }
            catch (IOException) { _pipeBroken = true; }
        }
        _file.Flush();
        _lastFlushTicks = Environment.TickCount64 * TimeSpan.TicksPerMillisecond;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Log broken pipe event if it happened
            if (_pipeBroken)
                _file.WriteLine($"\n# [PIPE] stdout broken — file logging continued normally");

            _file.Flush();
            _file.Dispose();

            if (_moveToOldOnDispose)
            {
                try
                {
                    var dir = Path.GetDirectoryName(_logPath);
                    if (!string.IsNullOrWhiteSpace(dir))
                    {
                        var oldDir = Path.Combine(dir, "old");
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
