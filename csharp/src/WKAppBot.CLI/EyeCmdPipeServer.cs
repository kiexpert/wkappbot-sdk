using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Eye command pipe server — executes delegated CLI commands in-process (zero cold-start).
/// Protocol: client sends JSON string[] args line → server streams output lines → sends "\x00END {code}".
/// Parallel execution: AsyncLocal routing isolates each command's output. No global serialization.
/// TeeTextWriter wraps pipeWriter per-command → real-time streaming to pipe + per-command log file.
/// CWD: Launcher prepends "__cwd:{path}" as first arg → extracted here, stored in AsyncLocal
///      so GetSessionTag() and tab-key builders use caller CWD (not Eye's own CWD).
/// Background: "__bg" anywhere in args → immediate EndMarker response + fire-and-forget task.
///      For ask commands, "--slack" is auto-added so result reaches the user via Slack.
/// </summary>
internal static class EyeCmdPipeServer
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";
    internal const string CwdPrefix  = "__cwd:";
    internal const string HwndPrefix = "__hwnd:";
    internal const string BgFlag = "__bg";

    /// <summary>Per-command caller CWD (set by Launcher, read by GetSessionTag)</summary>
    internal static readonly AsyncLocal<string?> CallerCwd = new();
    /// <summary>Per-command caller foreground HWND hint (set by Launcher for direct prompt window lookup)</summary>
    internal static readonly AsyncLocal<IntPtr?> CallerHwnd = new();
    /// <summary>Per-command actual args passed to Program.Main (e.g. ["ask","gemini","..."]) — for approval popup display</summary>
    internal static readonly AsyncLocal<string[]?> CallerArgs = new();

    /// <summary>
    /// Global (non-task-local) snapshot of the most recently dispatched command args.
    /// Updated whenever CallerArgs is set. Safe for read from any thread/context.
    /// Use for diagnostics, focus guards, and overlay decisions where async context is unavailable.
    /// Note: concurrent commands may overwrite each other — this reflects the LATEST dispatch, not per-task.
    /// For per-task isolation, use CallerArgs.Value.
    /// </summary>
    public static volatile string[]? CurrentCommandGlobal;

    private static readonly CancellationTokenSource _acceptCts = new();
    private static int _activeConnections = 0;

    public static void StartServer() => Task.Run(ServerLoop);

    /// <summary>
    /// Stop accepting new pipe connections and wait for all in-progress commands to finish.
    /// Called during hot-swap after new Eye is confirmed responsive.
    /// Logs a warning every 9s; hard-caps at 5 minutes.
    /// </summary>
    public static void StopAcceptingAndWaitForDrain()
    {
        _acceptCts.Cancel(); // unblocks WaitForConnectionAsync in ServerLoop
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var warnAt = 9.0;
        while (Volatile.Read(ref _activeConnections) > 0)
        {
            if (sw.Elapsed.TotalSeconds >= warnAt)
            {
                int n = Volatile.Read(ref _activeConnections);
                Console.WriteLine($"[EYE:HOT-SWAP] waiting for {n} active pipe service(s)... ({warnAt:F0}s)");
                warnAt += 9.0;
            }
            if (sw.Elapsed.TotalMinutes >= 5) break; // 5 min hard cap
            Thread.Sleep(200);
        }
    }

    /// <summary>
    /// Fire-and-forget: run args in background via the same in-process routing as __bg pipe commands.
    /// Output is isolated to a per-invocation log file (not mixed into Eye console).
    /// callerHwnd: explicitly set CallerHwnd (use a fixed sentinel for non-pipe callers such as HoverAnalyzer
    ///             so tab sandbox keys are deterministic — pass null to clear it).
    /// </summary>
    internal static void DispatchBg(string[] args, IntPtr? callerHwnd = null)
    {
        var logDir = Path.Combine(Program.DataDir, "logs");
        Directory.CreateDirectory(logDir);
        var cmdTag = args.Length > 0 ? args[0] : "bg";
        if (args.Length > 1 && cmdTag is "slack" or "ask") cmdTag += $"-{args[1]}";
        var logFile = Path.Combine(logDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={Environment.ProcessId}.log");
        var tee = new TeeTextWriter(TextWriter.Null, logFile);
        _ = Task.Run(() =>
        {
            CallerCwd.Value = null;
            CallerHwnd.Value = callerHwnd; // explicit — never inherit parent AsyncLocal value
            CallerArgs.Value = args;
            CurrentCommandGlobal = args;
            Program.RunningInEye = true;
            var memBefore = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
            using (ThreadRoutingWriter.Route(tee))
            {
                try { Program.Main(args); }
                catch (Exception ex) { tee.WriteLine($"[DISPATCHBG] error: {ex.Message}"); }
            }
            var memAfter = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
            var delta = memAfter - memBefore;
            if (Math.Abs(delta) >= 5) // log only significant changes (±5MB)
                Console.Error.WriteLine($"[PIPE-MEM] {cmdTag}: {memBefore}→{memAfter}MB ({(delta >= 0 ? "+" : "")}{delta})");
            tee.Dispose();
        });
    }

    static async Task ServerLoop()
    {
        Console.WriteLine("[EYE] CmdPipe server started — delegated commands run in-process (parallel)");
        var ct = _acceptCts.Token;
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var pipe = new NamedPipeServerStream(
                    PipeName, PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                await pipe.WaitForConnectionAsync(ct);
                _ = Task.Run(() => HandleClientAsync(pipe));
            }
            catch (OperationCanceledException) { break; }
            catch { await Task.Delay(1000); }
        }
        Console.WriteLine("[EYE] CmdPipe server stopped accepting new connections");
    }

    static async Task HandleClientAsync(NamedPipeServerStream pipe)
    {
        Interlocked.Increment(ref _activeConnections);
        try
        {
        using (pipe)
        {
            StreamWriter? pw = null;
            try
            {
                var line = await new StreamReader(pipe, leaveOpen: true).ReadLineAsync();
                var args = JsonSerializer.Deserialize<string[]>(line ?? "[]") ?? [];
                pw = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };

                // Extract CWD + HWND prefixes (prepended by Launcher, backward-compatible)
                string? callerCwd = null;
                IntPtr? callerHwnd = null;
                while (args.Length > 0 && (args[0].StartsWith(CwdPrefix, StringComparison.Ordinal)
                                        || args[0].StartsWith(HwndPrefix, StringComparison.Ordinal)))
                {
                    if (args[0].StartsWith(CwdPrefix, StringComparison.Ordinal))
                        callerCwd = args[0].Substring(CwdPrefix.Length);
                    else if (args[0].StartsWith(HwndPrefix, StringComparison.Ordinal))
                    {
                        var hwndStr = args[0].Substring(HwndPrefix.Length);
                        if (long.TryParse(hwndStr.TrimStart('0', 'x').TrimStart('0', 'X'),
                                System.Globalization.NumberStyles.HexNumber, null, out var hwndVal))
                            callerHwnd = new IntPtr(hwndVal);
                    }
                    args = args[1..];
                }

                // Check __bg flag — fire-and-forget: return immediately, run in background
                bool isBg = args.Contains(BgFlag, StringComparer.Ordinal);
                if (isBg)
                {
                    args = args.Where(a => a != BgFlag).ToArray();
                    // Auto-add --slack for ask commands so the result reaches the user
                    if (args.Length > 0 && args[0] == "ask" && !args.Contains("--slack"))
                        args = [..args, "--slack"];
                    await pw.WriteLineAsync("[BG] Running in background (result → Slack)");
                    await pw.WriteLineAsync($"{EndMarker} 0");
                    var bgArgs = args; var bgCwd = callerCwd; var bgHwnd = callerHwnd;
                    _ = Task.Run(() => RunInEye(bgArgs, TextWriter.Null, bgCwd, bgHwnd));
                }
                else
                {
                    int code = RunInEye(args, pw, callerCwd, callerHwnd);
                    await pw.WriteLineAsync($"{EndMarker} {code}");
                }
            }
            catch (Exception ex)
            {
                try { await (pw ?? new StreamWriter(pipe)).WriteLineAsync($"{EndMarker} 1"); } catch { }
                Console.WriteLine($"[EYE:PIPE] error: {ex.Message}");
            }
        }
        }
        finally { Interlocked.Decrement(ref _activeConnections); }
    }

    static int RunInEye(string[] args, TextWriter pipeWriter, string? callerCwd, IntPtr? callerHwnd = null)
    {
        // Build per-command log file path (same convention as Program.cs non-Eye mode)
        var logDir = Path.Combine(Program.DataDir, "logs");
        Directory.CreateDirectory(logDir);
        var cmdTag = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
        if (args.Length > 1 && cmdTag is "slack" or "web" or "schedule" or "knowhow" or "ask")
            cmdTag += $"-{args[1].ToLowerInvariant()}";
        var logFile = Path.Combine(logDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={Environment.ProcessId}.log");
        Program._currentLogPath = logFile; // track for auto-heal diagnostics

        // TeeTextWriter wraps pipeWriter: output goes to pipe (real-time) AND log file.
        // AsyncLocal routing isolates this command's output from concurrent commands.
        var tee = new TeeTextWriter(pipeWriter, logFile);
        int code;
        // CallerCwd + CallerHwnd stored in AsyncLocal — propagates to all async continuations of this command
        CallerCwd.Value = callerCwd;
        CallerHwnd.Value = callerHwnd;
        CallerArgs.Value = args;
        CurrentCommandGlobal = args;
        // RunningInEye=true prevents Program.cs from creating a second TeeTextWriter (duplicate log)
        Program.RunningInEye = true;
        using (ThreadRoutingWriter.Route(tee))
        {
            // Print delegation header: name=... cmd=... cwd=...  (goes to pipe+log, easy to grep)
            var delegName = Program.GetSendReplyUsername();
            var cmdLine = string.Join(" ", args);
            Console.WriteLine($"[CMD] name={delegName ?? "?"} cmd={cmdLine} cwd={callerCwd ?? "(none)"}");
            var memBefore2 = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
            try { code = Program.Main(args); }
            catch (Exception ex) { tee.WriteLine($"[EYECMD] error: {ex.Message}"); code = 1; }
            var memAfter2 = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
            var delta2 = memAfter2 - memBefore2;
            if (Math.Abs(delta2) >= 5)
                Console.Error.WriteLine($"[PIPE-MEM] {cmdTag}: {memBefore2}→{memAfter2}MB ({(delta2 >= 0 ? "+" : "")}{delta2})");
        }
        tee.Dispose(); // moves log to old/, updates tee.LogPath
        // "Log saved:" goes through pipeWriter directly (tee already disposed)
        pipeWriter.WriteLine($"Log saved: {tee.LogPath}");
        return code;
    }
}
