// StepProfiler.cs -- zero-friction step profiler with Init/Mark/Done lifecycle
//
// Usage (no instance needed):
//   PulseStep.Init("eye-launch");          // start session (always prints, resets timer)
//   PulseStep.Mark("mutex-checked");       // checkpoint (prints only when delta ≥ threshold)
//   PulseStep.Done("confirmed");           // end session (prints total, removes session)
//
// Output (stderr):
//   [PULSE:eye-launch] -- init @11:22:33.444 --
//   [PULSE:eye-launch] LaunchEye:142 "mutex-checked" +234ms (total=234ms @11:22:33.444)
//     callchain: LaunchEye:142 -> Main:55
//   [PULSE:eye-launch] LaunchEye:158 "confirmed" +500ms (total=734ms @11:22:33.444)
//   [PULSE:eye-launch] -- total 734ms --

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WKAppBot.CLI;

// PulseStep output goes to stderr only when profiling is explicitly enabled.
// Default: suppressed (ErrorScope buffers it, and MCP pipe would leak it to user).

// -- Static zero-instance entry point ----------------------------------------
// One implicit session per caller method. Lifecycle: Init -> Mark* -> Done.
internal static class PulseStep
{
    // One implicit session per caller method name
    private static readonly Dictionary<string, StepProfiler> _sessions = new();

    // Ring buffer of recent emitted pulse lines (for elevation prompt context)
    private const int _trailSize = 20;
    private static readonly LinkedList<string> _trail = new();
    private static readonly object _trailLock = new();

    internal static void RecordTrail(string line)
    {
        lock (_trailLock)
        {
            _trail.AddLast(line);
            while (_trail.Count > _trailSize) _trail.RemoveFirst();
        }
    }

    /// <summary>Get the last N emitted pulse lines (newest last). Useful for UAC/elevation prompts.</summary>
    public static IReadOnlyList<string> GetRecentTrail(int n = 5)
    {
        lock (_trailLock)
        {
            var take = System.Math.Min(n, _trail.Count);
            var arr = new string[take];
            int i = take - 1;
            var node = _trail.Last;
            while (node != null && i >= 0) { arr[i--] = node.Value; node = node.Previous; }
            return arr;
        }
    }

    /// <summary>
    /// Start a new profiling session. Always prints the init line (no threshold).
    /// Resets the session if one already exists for this caller.
    /// </summary>
    public static void Init(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        var prof = new StepProfiler(caller);
        _sessions[caller] = prof;
        prof.Init(label, callerHint: $"{caller}:{line}");
    }

    /// <summary>
    /// Record a checkpoint in the current session.
    /// Prints callstack + elapsed to stderr only when delta ≥ threshold (100ms).
    /// </summary>
    public static void Mark(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (!_sessions.TryGetValue(caller, out var prof))
            _sessions[caller] = prof = new StepProfiler(caller);
        prof.Step(label, skipFrames: 1, callerHint: $"{caller}:{line}");
    }

    /// <summary>Always emit to stderr -- bypasses threshold + WKAPPBOT_PROFILE.</summary>
    public static void Line(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (!_sessions.TryGetValue(caller, out var prof))
            _sessions[caller] = prof = new StepProfiler(caller);
        prof.Step(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: true);
    }

    /// <summary>End the session: print final step + total elapsed, then remove session.</summary>
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

    /// <summary>Always emit Done to stderr -- bypasses threshold + WKAPPBOT_PROFILE.</summary>
    public static void Finish(
        string label = "done",
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (_sessions.TryGetValue(caller, out var prof))
        {
            prof.Done(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: true);
            _sessions.Remove(caller);
        }
    }
}

// -- Explicit instance (multiple sessions in same method) --------------------
internal sealed class StepProfiler
{
    private readonly string _name;
    private readonly Stopwatch _sw = Stopwatch.StartNew();
    private readonly string _startedAt = DateTime.Now.ToString("HH:mm:ss.fff");
    private long _lastMs;
    private long _initMemMB;  // WS at Init time
    private long _lastMemMB;  // WS at last step
    private readonly long _thresholdMs;

    public StepProfiler(string name, long thresholdMs = 100)
    {
        _name = name;
        _thresholdMs = thresholdMs;
    }

    private static long GetWsMB() => Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);

    // PULSE output: only when WKAPPBOT_PROFILE=1 (explicit profiling).
    // Default: silent -- prevents stderr noise leaking through MCP/Eye/Launcher pipes.
    // force=true: bypass _profEnabled + threshold -> always print to stdout (e.g. route logging).
    private static readonly bool _profEnabled = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
    // Nesting depth -- AsyncLocal so each async context (Task, thread) has independent depth
    private static readonly AsyncLocal<int> _nestDepth = new();
    private int _myDepth = -1;       // depth captured at Init time; -1 = Init never called
    private bool _finished;          // true once Done/Finish is called
    private string _initCaller = "";  // callerHint at Init -- for mismatch detection

    private void Emit(string msg, bool force = false)
    {
        // always=true: stderr (captured by DebugStringWriter -> logcat --dbg, also Eye log relay)
        // normal: stderr only when WKAPPBOT_PROFILE=1
        if (force || _profEnabled)
        {
            var indent = _myDepth > 0 ? new string(' ', _myDepth * 2) : "";
            var full = $"{indent}{msg}";
            try { Console.Error.WriteLine(full); } catch { }
            PulseStep.RecordTrail(full);
        }
    }

    /// <summary>Print init line unconditionally (no threshold). Resets last-step timer.</summary>
    public void Init(string label, string callerHint = "")
    {
        _initCaller = callerHint;
        _myDepth = _nestDepth.Value;
        _nestDepth.Value++;
        _lastMs = _sw.ElapsedMilliseconds;
        _initMemMB = GetWsMB();
        _lastMemMB = _initMemMB;
        Emit($"[PULSE:{_name}] -- init \"{label}\" @{_startedAt} mem={_initMemMB}MB --");
    }

    ~StepProfiler()
    {
        if (_myDepth >= 0 && !_finished)
            try { Console.Error.WriteLine($"[PULSE:!] {_name} leaked -- Done/Finish never called (init at {_initCaller})"); } catch { }
    }

    public void Step(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Step(label, skipFrames: 1, callerHint: $"{caller}:{line}");

    /// <summary>Always emit to stderr -- bypasses threshold + WKAPPBOT_PROFILE. Compact single-line, no callchain.</summary>
    public void Line(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Step(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: true);

    internal void Step(string label, int skipFrames, string callerHint, bool force = false)
    {
        var nowMs = _sw.ElapsedMilliseconds;
        var delta = nowMs - _lastMs;
        _lastMs = nowMs;
        var curMem = GetWsMB();
        var memDelta = curMem - _lastMemMB;
        _lastMemMB = curMem;
        if (!force && delta < _thresholdMs && memDelta < 10) return; // also trigger on +10MB

        var memStr = memDelta != 0 ? $" mem={curMem}MB({(memDelta >= 0 ? "+" : "")}{memDelta})" : "";
        if (force)
        {
            // always mode: compact single-line, no callchain (stdout)
            Emit($"[PULSE:{_name}] \"{label}\" +{delta}ms (total={nowMs}ms){memStr}", force: true);
        }
        else
        {
            var chain = BuildCallChain(skipFrames + 1);
            Emit($"[PULSE:{_name}] {callerHint} \"{label}\" +{delta}ms (total={nowMs}ms @{_startedAt}){memStr}");
            Emit($"  callchain: {chain}");
        }
    }

    public void Done(
        string label = "done",
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Done(label, skipFrames: 1, callerHint: $"{caller}:{line}");

    internal void Done(string label, int skipFrames, string callerHint, bool force = false)
    {
        // Callstack mismatch: Init and Done called from different methods
        var initMethod = _initCaller.Split(':')[0];
        var doneMethod = callerHint.Split(':')[0];
        if (!string.IsNullOrEmpty(initMethod) && !string.IsNullOrEmpty(doneMethod) && initMethod != doneMethod)
            try { Console.Error.WriteLine($"[PULSE:!] {_name} caller mismatch -- Init:{_initCaller} Done:{callerHint}"); } catch { }

        Step(label, skipFrames + 1, callerHint, force);
        var totalMs = _sw.ElapsedMilliseconds;
        var finalMem = GetWsMB();
        var totalMemDelta = finalMem - _initMemMB;
        var memStr = totalMemDelta != 0 ? $" mem={finalMem}MB({(totalMemDelta >= 0 ? "+" : "")}{totalMemDelta})" : "";
        Emit($"[PULSE:{_name}] -- total {totalMs}ms{memStr} --", force);

        _finished = true;
        _nestDepth.Value--;
    }

    /// <summary>Always emit Done to stderr -- bypasses threshold + WKAPPBOT_PROFILE.</summary>
    public void Finish(
        string label = "done",
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Done(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: true);

    /// <summary>
    /// Walk StackTrace, skip StepProfiler/PulseStep frames, return call chain as
    ///   main -> WindowsCommand:2121 -> PrintFocusAndPopup:2028
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
            if (skipping && (declType == nameof(StepProfiler) || declType == nameof(PulseStep)))
                continue;
            skipping = false;

            // Stop at framework/runtime internals
            var ns = method.DeclaringType?.Namespace ?? "";
            if (ns.StartsWith("System.") || ns.StartsWith("Microsoft.")) break;

            if (sb.Length > 0) sb.Append(" -> ");
            sb.Append(method.Name);
            int ln = frame.GetFileLineNumber();
            if (ln > 0) sb.Append($":{ln}");
        }

        return sb.Length > 0 ? sb.ToString() : "(no frames)";
    }
}
