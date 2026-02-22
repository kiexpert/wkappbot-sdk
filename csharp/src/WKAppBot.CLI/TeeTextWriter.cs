using System.IO;
using System.Text;

namespace WKAppBot.CLI;

/// <summary>
/// TextWriter that writes to both the original Console.Out and a log file.
/// Install via Console.SetOut() at program start — all Console.Write/WriteLine
/// automatically goes to both destinations.
/// </summary>
public sealed class TeeTextWriter : TextWriter
{
    private readonly TextWriter _console;
    private readonly StreamWriter _file;
    private readonly bool _moveToOldOnDispose;
    private string _logPath;

    public TextWriter OriginalConsole => _console;
    public override Encoding Encoding => Encoding.UTF8;

    public TeeTextWriter(TextWriter console, string logPath, bool moveToOldOnDispose = true)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
        _moveToOldOnDispose = moveToOldOnDispose;
        _logPath = logPath;

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

    // ── Core overrides ──────────────────────────────────────────

    public override void Write(char value)
    {
        _console.Write(value);
        _file.Write(value);
    }

    public override void Write(string? value)
    {
        _console.Write(value);
        _file.Write(value);
    }

    public override void Write(char[] buffer, int index, int count)
    {
        _console.Write(buffer, index, count);
        _file.Write(buffer, index, count);
    }

    public override void WriteLine()
    {
        _console.WriteLine();
        _file.WriteLine();
    }

    public override void WriteLine(string? value)
    {
        _console.WriteLine(value);
        _file.WriteLine(value);
    }

    public override void Flush()
    {
        _console.Flush();
        _file.Flush();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
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
