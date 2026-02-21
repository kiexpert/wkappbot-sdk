// KiwoomComHost.cs — COM host for KHOpenAPI.ocx (32-bit ActiveX).
// Creates COM object via Type.GetTypeFromProgID + Activator.CreateInstance.
// All method calls use late binding (Type.InvokeMember) since we can't reference the OCX directly.
// Each call is automatically recorded for the COM Experience DB.

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WKAppBot.KiwoomProxy;

/// <summary>
/// Hosts the KHOpenAPI COM object and provides late-bound method invocation.
/// Must be used from an STA thread with a message pump (Application.DoEvents).
/// </summary>
public class KiwoomComHost : IDisposable
{
    private object? _comObject;
    private Type? _comType;
    private bool _disposed;

    /// <summary>COM ProgID for the Kiwoom OpenAPI ActiveX control.</summary>
    public const string ProgId = "KHOPENAPI.KHOpenAPICtrl.1";

    /// <summary>Whether the COM object has been created.</summary>
    public bool IsCreated => _comObject != null;

    /// <summary>Event fired when a COM method is called (for experience DB recording).</summary>
    public event Action<MethodCallRecord>? OnMethodCall;

    /// <summary>
    /// Create the COM object. Must be called from STA thread.
    /// Throws COMException if OCX is not registered or 64-bit mismatch.
    /// </summary>
    public void Create()
    {
        if (_comObject != null) return;

        _comType = Type.GetTypeFromProgID(ProgId, throwOnError: true);
        if (_comType == null)
            throw new InvalidOperationException($"COM type not found: {ProgId}");

        _comObject = Activator.CreateInstance(_comType)
            ?? throw new InvalidOperationException($"Failed to create COM instance: {ProgId}");

        Log($"COM created: {ProgId} (type={_comType.FullName})");
    }

    /// <summary>
    /// Invoke a COM method by name with positional parameters.
    /// Returns the method result, or null for void methods.
    /// Automatically records call metrics for experience DB.
    /// </summary>
    public object? Invoke(string methodName, params object?[] args)
    {
        if (_comObject == null || _comType == null)
            throw new InvalidOperationException("COM object not created. Call Create() first.");

        var record = new MethodCallRecord
        {
            MethodName = methodName,
            ParamCount = args.Length,
            CallTime = DateTime.UtcNow
        };

        var sw = Stopwatch.StartNew();
        try
        {
            var result = _comType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                null,
                _comObject,
                args);

            sw.Stop();
            record.ResponseMs = sw.ElapsedMilliseconds;
            record.Success = true;
            record.Result = result;

            OnMethodCall?.Invoke(record);
            return result;
        }
        catch (TargetInvocationException ex) when (ex.InnerException is COMException comEx)
        {
            sw.Stop();
            record.ResponseMs = sw.ElapsedMilliseconds;
            record.Success = false;
            record.ErrorCode = comEx.HResult;
            record.ErrorMessage = comEx.Message;

            OnMethodCall?.Invoke(record);
            throw new KiwoomComException(methodName, comEx.HResult, comEx.Message, comEx);
        }
        catch (Exception ex)
        {
            sw.Stop();
            record.ResponseMs = sw.ElapsedMilliseconds;
            record.Success = false;
            record.ErrorMessage = ex.Message;

            OnMethodCall?.Invoke(record);
            throw;
        }
    }

    /// <summary>
    /// Get the underlying COM object for event sink connection.
    /// </summary>
    public object? GetComObject() => _comObject;

    private static void Log(string msg)
    {
        Console.WriteLine($"[KIWOOM] {msg}");
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        if (_comObject != null)
        {
            try { Marshal.ReleaseComObject(_comObject); }
            catch { }
            _comObject = null;
        }
        _comType = null;
    }
}

/// <summary>Record of a single COM method call for experience DB.</summary>
public class MethodCallRecord
{
    public string MethodName { get; set; } = "";
    public int ParamCount { get; set; }
    public DateTime CallTime { get; set; }
    public long ResponseMs { get; set; }
    public bool Success { get; set; }
    public object? Result { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>Exception wrapper for COM errors with HRESULT.</summary>
public class KiwoomComException : Exception
{
    public string MethodName { get; }
    public new int HResult { get; }

    public KiwoomComException(string methodName, int hresult, string message, Exception? inner = null)
        : base($"COM error in {methodName}: 0x{hresult:X8} — {message}", inner)
    {
        MethodName = methodName;
        HResult = hresult;
    }
}
