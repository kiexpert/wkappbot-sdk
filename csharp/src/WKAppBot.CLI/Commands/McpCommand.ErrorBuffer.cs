using System.Diagnostics;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int AppBotExit(int code, bool forceErrorLog = false)
    {
        var scope = ErrorScope.Current;
        if (scope != null)
        {
            bool isError = code != 0 || forceErrorLog;
            var log = scope.Finalize(isError);
            if (isError && log.Length > 0)
                Console.Error.Write(log);
        }
        return code;
    }

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
        public bool HasErrors => _capture.Entries.Count > 0;
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

            // Structured error markers only -- the old "any substring match"
            // fired on prose like "[ASK] Persona injection failed, continuing"
            // or "[gemini] failure modes detected", producing BUG-AUTO exit=0
            // noise. Gates now require either a line-anchored error keyword
            // with a colon (ERROR:, Exception:, 오류:) or a .NET/stack-trace
            // signature.
            static readonly System.Text.RegularExpressions.Regex _errorPattern =
                new(@"(?im)^\s*(error|fatal|err|exception|unhandled|traceback|오류|에러)\s*[:：]"
                    + @"|^\s*at\s+\S+\.\S+\("                                // stack frame
                    + @"|^\s*System\.\S*Exception\b"                          // .NET exception typename
                    + @"|^\s*\w+Exception:\s",                                // bare "FooException: msg"
                    System.Text.RegularExpressions.RegexOptions.Compiled);
            // Progress/status lines like [ASK], [WEB], [gemini], etc. are
            // never treated as errors -- they're our own diagnostic channels
            // and frequently relay AI prose containing "fail"/"error" as
            // natural-language content.
            static readonly System.Text.RegularExpressions.Regex _progressTagPattern =
                new(@"^\s*\[[^\]]+\]",
                    System.Text.RegularExpressions.RegexOptions.Compiled);
            static readonly System.Text.RegularExpressions.Regex _warningPattern =
                new(@"(?im)^\s*(warn|warning|경고)\s*[:：]",
                    System.Text.RegularExpressions.RegexOptions.Compiled);

            static bool IsErrorLike(string msg)
            {
                if (string.IsNullOrWhiteSpace(msg)) return false;
                if (_progressTagPattern.IsMatch(msg)) return false;
                if (_warningPattern.IsMatch(msg)) return false;
                return _errorPattern.IsMatch(msg);
            }

            void EmitEntry(string msg)
            {
                var relMs = Environment.TickCount64 - _baseT;
                Entries.Add((relMs, msg));
                if (!_passthrough && IsErrorLike(msg))
                {
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
