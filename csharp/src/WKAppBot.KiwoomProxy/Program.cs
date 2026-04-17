// Program.cs -- KiwoomProxy 32-bit main entry point.
// [STAThread] is required for COM apartment threading.
// WinForms message pump (Application.DoEvents) is needed for ActiveX event delivery.
// Named Pipe server runs on a background thread, marshaling requests to STA.

using System.Text.Json;

namespace WKAppBot.KiwoomProxy;

internal static class Program
{
    private static KiwoomComHost? _comHost;
    private static KiwoomEventSink? _eventSink;
    private static PipeServer? _pipeServer;
    private static readonly CancellationTokenSource _cts = new();

    // Experience DB base path -- one level up from proxy dir (= wkappbot.hq/kiwoom_exp/)
    // This way both the proxy and the CLI share the same experience DB location.
    private static string ExpDbPath => Path.Combine(
        Path.GetDirectoryName(Path.GetDirectoryName(Environment.ProcessPath) ?? ".") ?? ".",
        "kiwoom_exp");

    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine($"[KIWOOM] KiwoomProxy starting (PID={Environment.ProcessId}, x86={!Environment.Is64BitProcess})");

        if (Environment.Is64BitProcess)
        {
            Console.Error.WriteLine("[KIWOOM] ERROR: Must run as 32-bit process! KHOpenAPI.ocx is 32-bit only.");
            Environment.Exit(1);
        }

        // Handle Ctrl+C gracefully
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            _cts.Cancel();
        };

        try
        {
            // 1. Create COM object
            _comHost = new KiwoomComHost();
            _comHost.OnMethodCall += OnMethodCallRecorded;
            _comHost.Create();

            // 2. Connect event sink
            var comObj = _comHost.GetComObject();
            if (comObj != null)
            {
                _eventSink = new KiwoomEventSink();
                _eventSink.OnEvent += OnComEvent;
                try
                {
                    _eventSink.Advise(comObj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[KIWOOM] Event sink connection failed: {ex.Message}");
                    Console.WriteLine("[KIWOOM] Events will not be received (continuing without)");
                }
            }

            // 3. Start Named Pipe server on background thread
            _pipeServer = new PipeServer();
            _pipeServer.OnRequest += HandlePipeRequest;
            var pipeTask = Task.Run(() => _pipeServer.ListenAsync(_cts.Token));

            Console.WriteLine("[KIWOOM] Ready -- waiting for pipe commands...");

            // 4. STA message pump -- required for COM event delivery
            // Application.DoEvents() processes Windows messages (WM_*) that COM needs
            while (!_cts.Token.IsCancellationRequested)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(10); // ~100 Hz message pump
            }

            Console.WriteLine("[KIWOOM] Shutting down...");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[KIWOOM] Fatal error: {ex}");
            Environment.Exit(2);
        }
        finally
        {
            _pipeServer?.Dispose();
            _eventSink?.Dispose();
            _comHost?.Dispose();
        }

        Console.WriteLine("[KIWOOM] Stopped");
    }

    /// <summary>Handle a JSON-RPC request from the CLI via Named Pipe.</summary>
    private static PipeResponse HandlePipeRequest(PipeRequest request)
    {
        if (_comHost == null || !_comHost.IsCreated)
            return new PipeResponse { Id = request.Id, Error = "COM not initialized" };

        Console.WriteLine($"[KIWOOM] << {request.Method}({FormatParams(request.Params)})");

        try
        {
            // Special methods
            if (request.Method == "Ping")
                return new PipeResponse { Id = request.Id, Result = "Pong" };

            if (request.Method == "GetStatus")
            {
                return new PipeResponse
                {
                    Id = request.Id,
                    Result = new
                    {
                        IsCreated = _comHost.IsCreated,
                        Is32Bit = !Environment.Is64BitProcess,
                        ProcessId = Environment.ProcessId
                    }
                };
            }

            // Generic COM method invocation
            var args = request.Params?.Select(p =>
            {
                // JSON elements need conversion to primitive types for COM
                if (p is JsonElement je)
                {
                    return je.ValueKind switch
                    {
                        JsonValueKind.String => (object?)je.GetString(),
                        JsonValueKind.Number => je.TryGetInt32(out var i) ? i : (object)je.GetDouble(),
                        JsonValueKind.True => true,
                        JsonValueKind.False => false,
                        JsonValueKind.Null => null,
                        _ => je.GetRawText()
                    };
                }
                return p;
            }).ToArray() ?? Array.Empty<object?>();

            var result = _comHost.Invoke(request.Method, args!);
            Console.WriteLine($"[KIWOOM] >> {request.Method} = {result} ({_lastCallMs}ms)");

            return new PipeResponse { Id = request.Id, Result = result };
        }
        catch (KiwoomComException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[KIWOOM] !! {request.Method} COM error: {ex.Message}");
            Console.ResetColor();
            return new PipeResponse { Id = request.Id, Error = ex.Message };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[KIWOOM] !! {request.Method} error: {ex.Message}");
            Console.ResetColor();
            return new PipeResponse { Id = request.Id, Error = ex.Message };
        }
    }

    private static long _lastCallMs;

    /// <summary>Handle COM events and push to pipe client.</summary>
    private static void OnComEvent(string eventName, object?[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[KIWOOM] EVENT: {eventName}({FormatParams(args)})");
        Console.ResetColor();

        // Push event to pipe client
        if (_pipeServer?.IsClientConnected == true)
        {
            var evt = new PipeEvent { EventName = eventName, Params = args };
            _ = _pipeServer.PushEventAsync(evt);
        }

        // Record event for experience DB
        RecordEvent(eventName, args);
    }

    /// <summary>Record method call metrics for experience DB.</summary>
    private static void OnMethodCallRecorded(MethodCallRecord record)
    {
        _lastCallMs = record.ResponseMs;
        SaveMethodExperience(record);
    }

    // -- Experience DB persistence (JSON files) --

    private static void SaveMethodExperience(MethodCallRecord record)
    {
        try
        {
            var methodDir = Path.Combine(ExpDbPath, "methods");
            Directory.CreateDirectory(methodDir);

            var filePath = Path.Combine(methodDir, $"{record.MethodName}.json");
            MethodExperience exp;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                exp = JsonSerializer.Deserialize<MethodExperience>(json) ?? new() { MethodName = record.MethodName };
            }
            else
            {
                exp = new() { MethodName = record.MethodName, FirstCall = record.CallTime };
            }

            exp.CallCount++;
            if (record.Success) exp.SuccessCount++;
            else exp.FailCount++;
            exp.LastCall = record.CallTime;

            // Running average response time
            if (exp.CallCount > 1)
                exp.AvgResponseMs = (exp.AvgResponseMs * (exp.CallCount - 1) + record.ResponseMs) / exp.CallCount;
            else
                exp.AvgResponseMs = record.ResponseMs;

            if (record.ResponseMs > exp.MaxResponseMs)
                exp.MaxResponseMs = record.ResponseMs;

            // Track errors
            if (!record.Success && record.ErrorMessage != null)
            {
                exp.CommonErrors ??= new List<string>();
                if (!exp.CommonErrors.Contains(record.ErrorMessage))
                    exp.CommonErrors.Add(record.ErrorMessage);
                if (exp.CommonErrors.Count > 20)
                    exp.CommonErrors.RemoveAt(0); // Keep last 20
            }

            var outJson = JsonSerializer.Serialize(exp, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, outJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[KIWOOM] ExpDB save error: {ex.Message}");
        }
    }

    private static void RecordEvent(string eventName, object?[] args)
    {
        try
        {
            var eventDir = Path.Combine(ExpDbPath, "events");
            Directory.CreateDirectory(eventDir);

            var filePath = Path.Combine(eventDir, $"{eventName}.json");
            EventExperience exp;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                exp = JsonSerializer.Deserialize<EventExperience>(json) ?? new() { EventName = eventName };
            }
            else
            {
                exp = new() { EventName = eventName, FirstReceived = DateTime.UtcNow };
            }

            exp.ReceiveCount++;
            exp.LastReceived = DateTime.UtcNow;

            var outJson = JsonSerializer.Serialize(exp, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, outJson);
        }
        catch { }
    }

    private static string FormatParams(object?[]? args)
    {
        if (args == null || args.Length == 0) return "";
        return string.Join(", ", args.Select(a => a switch
        {
            string s => s.Length > 30 ? $"\"{s[..30]}...\"" : $"\"{s}\"",
            null => "null",
            _ => a.ToString() ?? "?"
        }));
    }
}

// -- Experience DB models --

public class MethodExperience
{
    public string MethodName { get; set; } = "";
    public int CallCount { get; set; }
    public int SuccessCount { get; set; }
    public int FailCount { get; set; }
    public long AvgResponseMs { get; set; }
    public long MaxResponseMs { get; set; }
    public DateTime FirstCall { get; set; }
    public DateTime LastCall { get; set; }
    public List<string>? CommonErrors { get; set; }
    public string? Knowhow { get; set; }
}

public class EventExperience
{
    public string EventName { get; set; } = "";
    public int ReceiveCount { get; set; }
    public DateTime FirstReceived { get; set; }
    public DateTime LastReceived { get; set; }
    public string? Knowhow { get; set; }
}
