// StepProfiler.cs — zero-friction step profiler with Init/Mark/Done lifecycle
//
// Usage (no instance needed):
//   PulseStep.Init("eye-launch");          // start session (always prints, resets timer)
//   PulseStep.Mark("mutex-checked");       // checkpoint (prints only when delta ≥ threshold)
//   PulseStep.Done("confirmed");           // end session (prints total, removes session)
//
// Output (stderr):
//   [PULSE:eye-launch] ── init @11:22:33.444 ──
//   [PULSE:eye-launch] LaunchEye:142 "mutex-checked" +234ms (total=234ms @11:22:33.444)
//     callchain: LaunchEye:142 → Main:55
//   [PULSE:eye-launch] LaunchEye:158 "confirmed" +500ms (total=734ms @11:22:33.444)
//   [PULSE:eye-launch] ── total 734ms ──

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace WKAppBot.CLI;

// PulseStep output goes to stderr only when profiling is explicitly enabled.
// Default: suppressed (ErrorScope buffers it, and MCP pipe would leak it to user).

// ── Static zero-instance entry point ────────────────────────────────────────
// One implicit session per caller method. Lifecycle: Init → Mark* → Done.
internal static class PulseStep
{
    // One implicit session per caller method name
    private static readonly Dictionary<string, StepProfiler> _sessions = new();

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
    /// always=true: bypass threshold AND WKAPPBOT_PROFILE check → always prints to stdout.
    /// </summary>
    public static void Mark(
        string label,
        bool always = false,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (!_sessions.TryGetValue(caller, out var prof))
            _sessions[caller] = prof = new StepProfiler(caller);
        prof.Step(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: always);
    }

    /// <summary>
    /// End the session: print final step + total elapsed, then remove session.
    /// always=true: bypass threshold AND WKAPPBOT_PROFILE check → always prints to stdout.
    /// </summary>
    public static void Done(
        string label = "done",
        bool always = false,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
    {
        if (_sessions.TryGetValue(caller, out var prof))
        {
            prof.Done(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: always);
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
    // Default: silent — prevents stderr noise leaking through MCP/Eye/Launcher pipes.
    // force=true: bypass _profEnabled + threshold → always print to stdout (e.g. route logging).
    private static readonly bool _profEnabled = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
    private void Emit(string msg, bool force = false)
    {
        // always=true: stderr (captured by DebugStringWriter → logcat --dbg, also Eye log relay)
        // normal: stderr only when WKAPPBOT_PROFILE=1
        if (force || _profEnabled) { try { Console.Error.WriteLine(msg); } catch { } }
    }

    /// <summary>Print init line unconditionally (no threshold). Resets last-step timer.</summary>
    public void Init(string label, string callerHint = "")
    {
        _lastMs = _sw.ElapsedMilliseconds;
        _initMemMB = GetWsMB();
        _lastMemMB = _initMemMB;
        Emit($"[PULSE:{_name}] ── init \"{label}\" @{_startedAt} mem={_initMemMB}MB ──");
    }

    public void Step(
        string label,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Step(label, skipFrames: 1, callerHint: $"{caller}:{line}");

    /// <summary>force=true: always emit to stderr (bypass threshold + WKAPPBOT_PROFILE).</summary>
    public void Step(
        string label,
        bool force,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Step(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: force);

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
        Step(label, skipFrames + 1, callerHint, force);
        var totalMs = _sw.ElapsedMilliseconds;
        var finalMem = GetWsMB();
        var totalMemDelta = finalMem - _initMemMB;
        var memStr = totalMemDelta != 0 ? $" mem={finalMem}MB({(totalMemDelta >= 0 ? "+" : "")}{totalMemDelta})" : "";
        Emit($"[PULSE:{_name}] ── total {totalMs}ms{memStr} ──", force);
    }

    /// <summary>force=true: always emit to stderr (bypass threshold + WKAPPBOT_PROFILE).</summary>
    public void Done(
        string label,
        bool force,
        [CallerMemberName] string caller = "",
        [CallerLineNumber] int line = 0)
        => Done(label, skipFrames: 1, callerHint: $"{caller}:{line}", force: force);

    /// <summary>
    /// Walk StackTrace, skip StepProfiler/PulseStep frames, return call chain as
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
            if (skipping && (declType == nameof(StepProfiler) || declType == nameof(PulseStep)))
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
