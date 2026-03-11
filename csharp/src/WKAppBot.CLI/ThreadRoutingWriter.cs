using System.Text;

namespace WKAppBot.CLI;

/// <summary>
/// Thread-local Console router for Eye command pipe.
/// Eye threads write to the global (TeeWriter → file+console).
/// Command threads call Route(sw) to redirect their output to a StringWriter for piping.
/// This avoids the global Console.SetOut race where Eye tick output leaks into command output.
/// </summary>
internal sealed class ThreadRoutingWriter : TextWriter
{
    [ThreadStatic] static TextWriter? _local;

    /// <summary>
    /// Redirect the CURRENT thread's console output to <paramref name="local"/>.
    /// Dispose the returned token to restore the previous route.
    /// </summary>
    public static IDisposable Route(TextWriter local) => new Scope(local);

    readonly TextWriter _global;

    public ThreadRoutingWriter(TextWriter global) => _global = global;

    public override Encoding Encoding => _global.Encoding;

    TextWriter Active => _local ?? _global;

    public override void Write(char value)          => Active.Write(value);
    public override void Write(string? value)       => Active.Write(value);
    public override void WriteLine(string? value)   => Active.WriteLine(value);
    public override void WriteLine()                => Active.WriteLine();
    public override void Write(bool value)          => Active.Write(value);
    public override void Write(int value)           => Active.Write(value);
    public override void Write(long value)          => Active.Write(value);
    public override void Write(object? value)       => Active.Write(value);
    public override void Write(ReadOnlySpan<char> buffer) => Active.Write(buffer);
    public override void WriteLine(bool value)      => Active.WriteLine(value);
    public override void WriteLine(int value)       => Active.WriteLine(value);
    public override void WriteLine(object? value)   => Active.WriteLine(value);
    public override void WriteLine(ReadOnlySpan<char> buffer) => Active.WriteLine(buffer);
    public override void Flush()                    => Active.Flush();
    public override Task FlushAsync()               => Active.FlushAsync();

    sealed class Scope : IDisposable
    {
        readonly TextWriter? _prev;
        public Scope(TextWriter w) { _prev = _local; _local = w; }
        public void Dispose() => _local = _prev;
    }
}
