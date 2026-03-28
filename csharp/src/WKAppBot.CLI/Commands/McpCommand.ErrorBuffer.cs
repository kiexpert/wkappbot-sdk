using System.Diagnostics;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Universal exit point — flushes timestamped error log on failure.
    /// Usage: return AppBotExit(0);          // success, discard errors
    ///        return AppBotExit(1);          // error, auto-flush error log
    ///        return AppBotExit(1, true);    // error, force flush even if code==0
    /// </summary>
    static int AppBotExit(int code, bool forceErrorLog = false)
    {
        var scope = ErrorScope.Current;
        if (scope != null)
        {
            // ErrorScope active → stderr was buffered. Flush on error.
            bool isError = code != 0 || forceErrorLog;
            var log = scope.Finalize(isError);
            if (isError && log.Length > 0)
                Console.Error.Write(log);
        }
        // No ErrorScope (--stderr mode) → stderr already shown in real-time, nothing to flush.
        return code;
    }

    /// <summary>
    /// MCP tool error scope — auto-captures Console.Error during tool execution.
    /// Usage:
    ///   using var _ = ErrorScope.Begin();    // redirects Console.Error
    ///   ... tool execution ...                   // Console.Error.WriteLine auto-buffered
    ///   return AppBotExit(exitCode);             // auto-flush on error
    /// </summary>
    sealed class ErrorScope : IDisposable
    {
        readonly TextWriter _originalStderr;
        readonly ErrorCapture _capture;

        [ThreadStatic] static ErrorScope? _current;
        public static ErrorScope? Current => _current;

        ErrorScope()
        {
            _originalStderr = Console.Error;
            _capture = new ErrorCapture(_originalStderr);
            Console.SetError(_capture);
            _current = this;
        }

        public static ErrorScope Begin() => new();

        /// <summary>
        /// Finalize: restore stderr. Returns timestamped error log if isError, empty if success.
        /// </summary>
        public string Finalize(bool isError)
        {
            Console.SetError(_originalStderr);
            _current = null;
            if (!isError || _capture.Entries.Count == 0) return "";

            var sb = new StringBuilder();
            sb.AppendLine("\n--- Error Log ---");
            foreach (var (relMs, msg) in _capture.Entries)
                sb.AppendLine($"[+{relMs / 1000.0:F1}s] {msg}");
            return sb.ToString();
        }

        public void Dispose()
        {
            Console.SetError(_originalStderr);
            _current = null;
        }

        /// <summary>Buffer-only writer: captures stderr with timestamps, NO passthrough to console.</summary>
        sealed class ErrorCapture : TextWriter
        {
            readonly TextWriter _inner; // kept for Flush/Encoding only
            readonly long _baseT = Environment.TickCount64;
            readonly StringBuilder _line = new();
            public readonly List<(long relMs, string msg)> Entries = new();

            public ErrorCapture(TextWriter inner) => _inner = inner;
            public override Encoding Encoding => _inner.Encoding;

            public override void WriteLine(string? value)
            {
                // Buffer only — no passthrough (stderr hidden by default)
                if (!string.IsNullOrEmpty(value))
                    Entries.Add((Environment.TickCount64 - _baseT, value));
            }

            public override void Write(char value)
            {
                if (value == '\n') { FlushLine(); return; }
                if (value != '\r') _line.Append(value);
            }

            public override void Write(string? value)
            {
                if (string.IsNullOrEmpty(value)) return;
                foreach (var c in value)
                {
                    if (c == '\n') { FlushLine(); continue; }
                    if (c != '\r') _line.Append(c);
                }
            }

            void FlushLine()
            {
                if (_line.Length == 0) return;
                Entries.Add((Environment.TickCount64 - _baseT, _line.ToString()));
                _line.Clear();
            }

            public override void Flush() => _inner.Flush();
        }
    }
}
