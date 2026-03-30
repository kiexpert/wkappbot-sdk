using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// TextWriter wrapper that mirrors all stderr writes to OutputDebugString.
/// Enables real-time debugging via "wkappbot logcat --dbg [pid]" without polluting stdout pipes.
/// Especially useful when running as MCP server (stdout = protocol channel).
/// </summary>
internal sealed class DebugStringWriter : TextWriter
{
    private readonly TextWriter _inner;
    private readonly string _prefix = $"wkappbot({Environment.ProcessId})>";

    public DebugStringWriter(TextWriter inner) => _inner = inner;

    public override Encoding Encoding => _inner.Encoding;

    public override void Write(char value) => _inner.Write(value);

    public override void Write(string? value)
    {
        _inner.Write(value);
        if (!string.IsNullOrEmpty(value))
            EmitDbg(value);
    }

    public override void WriteLine(string? value)
    {
        // Flush stdout first so pending output appears before this stderr line (clear ordering)
        try { Console.Out.Flush(); } catch { }
        _inner.WriteLine(value);
        EmitDbg(value ?? "");
    }

    public override void WriteLine()
    {
        _inner.WriteLine();
    }

    public override void Flush()
    {
        _inner.Flush();
        // When stderr is flushed, also flush stdout for synchronized output
        try { Console.Out.Flush(); } catch { }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _inner.Flush();
        base.Dispose(disposing);
    }

    private void EmitDbg(string msg)
    {
        try
        {
            // Strip ANSI escape codes for cleaner DebugView output
            var clean = System.Text.RegularExpressions.Regex.Replace(msg, @"\x1b\[[0-9;]*m", "").TrimEnd();
            if (!string.IsNullOrEmpty(clean))
                NativeMethods.OutputDebugStringW($"{_prefix} {clean}");
        }
        catch { }
    }
}
