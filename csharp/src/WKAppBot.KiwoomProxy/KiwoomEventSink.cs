// KiwoomEventSink.cs -- IDispatch-based event sink for KHOpenAPI events.
// Uses IConnectionPoint to subscribe to _DKHOpenAPIEvents (9 events).
// The IReflect approach allows us to receive COM events without a type library reference.

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WKAppBot.KiwoomProxy;

/// <summary>
/// Event sink for KHOpenAPI COM events via IConnectionPoint.
/// Implements IReflect to intercept IDispatch::Invoke calls from COM.
/// </summary>
[ComVisible(true)]
public class KiwoomEventSink : IReflect, IDisposable
{
    // KHOpenAPI event interface IID (from TypeLib analysis)
    private static readonly Guid EventsIid = new("7335F12D-8973-4BD5-B7F0-12DF03D175B7");

    private IConnectionPoint? _connectionPoint;
    private int _adviseCookie;
    private bool _disposed;

    /// <summary>Fired when any COM event is received.</summary>
    public event Action<string, object?[]>? OnEvent;

    // Known DISPID -> event name mapping (from ComInspect analysis)
    private static readonly Dictionary<int, string> DispIdToName = new()
    {
        [1] = "OnReceiveTrData",
        [2] = "OnReceiveRealData",
        [3] = "OnReceiveMsg",
        [4] = "OnReceiveChejanData",
        [5] = "OnEventConnect",
        [6] = "OnReceiveRealCondition",
        [7] = "OnReceiveTrCondition",
        [8] = "OnReceiveConditionVer",
        [9] = "OnReceiveInvestRealData",
    };

    /// <summary>
    /// Connect to the COM object's event source via IConnectionPoint.
    /// Must be called from STA thread.
    /// </summary>
    public void Advise(object comObject)
    {
        if (comObject is not IConnectionPointContainer container)
            throw new InvalidOperationException("COM object does not support IConnectionPointContainer");

        container.FindConnectionPoint(ref Unsafe.AsRef(EventsIid), out _connectionPoint);
        if (_connectionPoint == null)
            throw new InvalidOperationException($"ConnectionPoint not found for IID {EventsIid}");

        _connectionPoint.Advise(this, out _adviseCookie);
        Console.WriteLine($"[KIWOOM] Event sink connected (cookie={_adviseCookie})");
    }

    /// <summary>Disconnect from events.</summary>
    public void Unadvise()
    {
        if (_connectionPoint != null && _adviseCookie != 0)
        {
            try { _connectionPoint.Unadvise(_adviseCookie); }
            catch { }
            _adviseCookie = 0;
            Console.WriteLine("[KIWOOM] Event sink disconnected");
        }
    }

    // -- IReflect implementation -- COM calls IDispatch::Invoke which routes through IReflect --

    // This is the key method: COM's IDispatch::Invoke ends up here
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr) => null;
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr, Binder? binder, Type[] types, ParameterModifier[]? modifiers) => null;
    public MethodInfo[] GetMethods(BindingFlags bindingAttr) => Array.Empty<MethodInfo>();

    public FieldInfo? GetField(string name, BindingFlags bindingAttr) => null;
    public FieldInfo[] GetFields(BindingFlags bindingAttr) => Array.Empty<FieldInfo>();

    public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr) => null;
    public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[] types, ParameterModifier[]? modifiers) => null;
    public PropertyInfo[] GetProperties(BindingFlags bindingAttr) => Array.Empty<PropertyInfo>();

    public MemberInfo[] GetMember(string name, BindingFlags bindingAttr) => Array.Empty<MemberInfo>();
    public MemberInfo[] GetMembers(BindingFlags bindingAttr) => Array.Empty<MemberInfo>();

    public Type UnderlyingSystemType => typeof(KiwoomEventSink);

    /// <summary>
    /// Called by COM when dispatching an event. The DISPID is in the name parameter.
    /// This is where we intercept all 9 KHOpenAPI events.
    /// </summary>
    public object? InvokeMember(string name, BindingFlags invokeAttr, Binder? binder,
        object? target, object?[]? args, ParameterModifier[]? modifiers,
        System.Globalization.CultureInfo? culture, string[]? namedParameters)
    {
        // name is the CYCLEDISPID as string (e.g. "1", "5")
        if (int.TryParse(name, out var dispId))
        {
            var eventName = DispIdToName.GetValueOrDefault(dispId, $"UnknownEvent_{dispId}");
            var eventArgs = args ?? Array.Empty<object?>();

            try
            {
                OnEvent?.Invoke(eventName, eventArgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KIWOOM] Event handler error ({eventName}): {ex.Message}");
            }
        }

        return null;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        Unadvise();
    }
}

// Helper for ref Guid in FindConnectionPoint
file static class Unsafe
{
    public static ref T AsRef<T>(in T source) => ref System.Runtime.CompilerServices.Unsafe.AsRef(in source);
}
