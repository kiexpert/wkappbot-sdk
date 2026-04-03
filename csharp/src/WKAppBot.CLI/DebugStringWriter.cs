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
    private readonly string _prefix;

    /// <param name="inner">Underlying writer (original stderr).</param>
    /// <param name="cmd">CLI command name (e.g. "web"). null = unknown.</param>
    /// <param name="subcmd">CLI subcommand (e.g. "open"). null = omit.</param>
    public DebugStringWriter(TextWriter inner, string? cmd = null, string? subcmd = null)
    {
        _inner = inner;
        var pid = Environment.ProcessId;
        var exeName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "") is { Length: > 0 } n ? n : "wkappbot";
        var cmdPart = cmd != null ? $"-{cmd}" : "";
        var subcmdPart = subcmd != null ? $"-{subcmd}" : "";
        _prefix = $"{exeName}:{pid}{cmdPart}{subcmdPart}>";
    }

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
