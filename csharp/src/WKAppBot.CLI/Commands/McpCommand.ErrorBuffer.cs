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
        public bool HasErrors => _capture.Entries.Count > 0;
        /// <summary>True if an error-like message was detected (passthrough mode was triggered).</summary>
        public bool ErrorDetected => _capture.ErrorDetected;

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

        /// <summary>
        /// Captures stderr with timestamps. On first error entry, immediately switches to
        /// passthrough mode: flushes buffered entries + forwards all subsequent writes directly.
        /// </summary>
        sealed class ErrorCapture : TextWriter
        {
            readonly TextWriter _inner;
            readonly long _baseT = Environment.TickCount64;
            readonly StringBuilder _line = new();
            public readonly List<(long relMs, string msg)> Entries = new();
            bool _passthrough = false;
            public bool ErrorDetected => _passthrough;

            public ErrorCapture(TextWriter inner) => _inner = inner;
            public override Encoding Encoding => _inner.Encoding;

            static readonly System.Text.RegularExpressions.Regex _errorPattern =
                new(@"error|fail|exception|오류|에러",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                    System.Text.RegularExpressions.RegexOptions.Compiled);

            static bool IsErrorLike(string msg) => _errorPattern.IsMatch(msg);

            void EmitEntry(string msg)
            {
                var relMs = Environment.TickCount64 - _baseT;
                Entries.Add((relMs, msg));
                if (!_passthrough && IsErrorLike(msg))
                {
                    // Error-like message detected — switch to passthrough: flush all buffered entries
                    _passthrough = true;
                    foreach (var (t, m) in Entries)
                        _inner.WriteLine($"[+{t / 1000.0:F1}s] {m}");
                    _inner.Flush();
                }
                else if (_passthrough)
                {
                    _inner.WriteLine($"[+{relMs / 1000.0:F1}s] {msg}");
                    _inner.Flush();
                }
            }

            public override void WriteLine(string? value)
            {
                if (!string.IsNullOrEmpty(value)) EmitEntry(value);
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
                EmitEntry(_line.ToString());
                _line.Clear();
            }

            public override void Flush() => _inner.Flush();
        }
    }
}
