using System.Text;
using System.Runtime.InteropServices;

namespace WKAppBot.CLI;

/// <summary>
/// TextWriter wrapper that outputs in dim/dark color using ANSI escape codes.
/// Used for stderr to visually distinguish diagnostic output from command results.
/// Only applies dim when writing to a real console (not piped).
/// </summary>
internal sealed class DimTextWriter : TextWriter
{
    private readonly TextWriter _inner;
    private const string DimOn = "\x1b[2m";   // ANSI dim
    private const string Reset = "\x1b[0m";   // ANSI reset

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    public DimTextWriter(TextWriter inner)
    {
        _inner = inner;
        // Enable VT processing on stderr so ANSI codes work on Windows console
        try
        {
            var hErr = GetStdHandle(-12); // STD_ERROR_HANDLE
            if (GetConsoleMode(hErr, out uint mode))
                SetConsoleMode(hErr, mode | 0x0004); // ENABLE_VIRTUAL_TERMINAL_PROCESSING
        }
        catch { }
    }

    public override Encoding Encoding => _inner.Encoding;

    public override void Write(char value) => _inner.Write(value);

    public override void Write(string? value)
    {
        if (value == null) return;
        _inner.Write(DimOn);
        _inner.Write(value);
        _inner.Write(Reset);
    }

    public override void WriteLine(string? value)
    {
        _inner.Write(DimOn);
        _inner.WriteLine(value);
        _inner.Write(Reset);
    }

    public override void WriteLine() => _inner.WriteLine();

    public override void Flush() => _inner.Flush();

    protected override void Dispose(bool disposing)
    {
        if (disposing) _inner.Flush();
        base.Dispose(disposing);
    }
}
