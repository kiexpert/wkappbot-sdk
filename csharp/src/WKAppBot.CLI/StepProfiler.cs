// StepProfiler.cs — zero-friction step profiler with full callstack tree
//
// Usage (no instance needed):
//   WkStep.Call("loop-start");
//   ... work ...
//   WkStep.Call("loop-done");
//   WkStep.Done();
//
// Output (stderr, only when step > 100ms):
//   [PROF] WindowsCommand → PrintFocusAndPopup:2022 "enum-popup" +1234ms (total=1300ms @11:22:33.444)
//          └ Program.WindowsCommand:2022
//          └ Program.PrintFocusAndPopup:2028
//          └ NativeMethods.EnumWindows

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WKAppBot.CLI;

// ── Static zero-instance entry point ────────────────────────────────────────
internal static class WkStep
{
    // One implicit session per caller method name
    private static readonly Dictionary<string, StepProfiler> _sessions = new();

    /// <summary>
    /// Record a step in the implicit session for the calling method.
    /// Prints callstack + elapsed to stderr only when delta ≥ threshold.
    /// </summary>
    public static void Call(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (!_sessions.TryGetValue(caller, out var prof))
            _sessions[caller] = prof = new StepProfiler(caller);
        prof.Step(label, skipFrames: 1, callerHint: $"{caller}:{line}");
    }

    /// <summary>Flush + remove the implicit session for the caller method.</summary>
    public static void Done(
        string label = "done",
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (_sessions.TryGetValue(caller, out var prof))
        {
            prof.Done(label, skipFrames: 1, callerHint: $"{caller}:{line}");
            _sessions.Remove(caller);
        }
    }
}

// ── Explicit instance (multiple sessions in same method) ────────────────────
internal sealed class StepProfiler
{
    private readonly string _name;
    private readonly Stopwatch _sw = Stopwatch.StartNew();
    private readonly string _startedAt = DateTime.Now.ToString("HH:mm:ss.fff");
    private long _lastMs;
    private readonly long _thresholdMs;

    public StepProfiler(string name, long thresholdMs = 100)
    {
        _name = name;
        _thresholdMs = thresholdMs;
    }

    public void Step(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Step(label, skipFrames: 1, callerHint: $"{caller}:{line}");

    internal void Step(string label, int skipFrames, string callerHint)
    {
        var nowMs = _sw.ElapsedMilliseconds;
        var delta = nowMs - _lastMs;
        _lastMs = nowMs;
        if (delta < _thresholdMs) return;

        var chain = BuildCallChain(skipFrames + 1);
        Console.Error.WriteLine(
            $"[PROF:{_name}] {callerHint} \"{label}\" +{delta}ms (total={nowMs}ms @{_startedAt})");
        Console.Error.WriteLine($"  callchain: {chain}");
    }

    public void Done(
        string label = "done",
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Done(label, skipFrames: 1, callerHint: $"{caller}:{line}");

    internal void Done(string label, int skipFrames, string callerHint)
    {
        Step(label, skipFrames + 1, callerHint);
        var totalMs = _sw.ElapsedMilliseconds;
        if (totalMs >= _thresholdMs)
            Console.Error.WriteLine($"[PROF:{_name}] ── total {totalMs}ms ──");
    }

    /// <summary>
    /// Walk StackTrace, skip StepProfiler/WkStep frames, return call chain as
    ///   main → WindowsCommand:2121 → PrintFocusAndPopup:2028
    /// </summary>
    private static string BuildCallChain(int extraSkip)
    {
        var frames = new StackTrace(fNeedFileInfo: true).GetFrames();
        var sb = new StringBuilder();
        bool skipping = true;

        foreach (var frame in frames.Skip(extraSkip))
        {
            var method = frame.GetMethod();
            if (method == null) continue;
            var declType = method.DeclaringType?.Name ?? "";

            // Skip our own profiler frames
            if (skipping && (declType == nameof(StepProfiler) || declType == nameof(WkStep)))
                continue;
            skipping = false;

            // Stop at framework/runtime internals
            var ns = method.DeclaringType?.Namespace ?? "";
            if (ns.StartsWith("System.") || ns.StartsWith("Microsoft.")) break;

            if (sb.Length > 0) sb.Append(" → ");
            sb.Append(method.Name);
            int ln = frame.GetFileLineNumber();
            if (ln > 0) sb.Append($":{ln}");
        }

        return sb.Length > 0 ? sb.ToString() : "(no frames)";
    }
}
