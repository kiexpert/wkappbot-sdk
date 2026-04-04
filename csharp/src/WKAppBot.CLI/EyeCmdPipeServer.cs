using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Eye command pipe server — executes delegated CLI commands via MCP subprocess or in-process.
/// Protocol: client sends JSON string[] args line → server streams output lines → sends "\x00END {code}".
/// Parallel execution: AsyncLocal routing isolates each command's output. No global serialization.
/// TeeTextWriter wraps pipeWriter per-command → real-time streaming to pipe + per-command log file.
/// CWD: Launcher prepends "__cwd:{path}" as first arg → extracted here, stored in AsyncLocal
///      so GetSessionTag() and tab-key builders use caller CWD (not Eye's own CWD).
/// Background: "__bg" anywhere in args → immediate EndMarker response + fire-and-forget task.
///      For ask commands, "--slack" is auto-added so result reaches the user via Slack.
///
/// MCP routing (v4.9): a11y/inspect/windows/capture/ocr/ask/prompt/scan/focus commands are routed
/// to EyeMcpClient (persistent subprocess) to avoid loading UIA/Win32 assemblies into Eye's memory.
/// Only Eye-internal commands (slack, eye, schedule, speak, claude-usage) run in-process.
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

    /// <summary>Active command count — screensaver checks eye_busy file to suppress during automation.</summary>
    static int _activeCommandCount;
    static readonly string EyeBusyFile = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq", "runtime", "eye_busy");
    internal static void BeginCommand() { if (Interlocked.Increment(ref _activeCommandCount) == 1) try { File.WriteAllText(EyeBusyFile, DateTime.UtcNow.ToString("O")); } catch { } }
    internal static void EndCommand() { if (Interlocked.Decrement(ref _activeCommandCount) <= 0) try { File.Delete(EyeBusyFile); } catch { } }

    private static readonly CancellationTokenSource _acceptCts = new();
    private static int _activeConnections = 0;

    public static void StartServer() => Task.Run(ServerLoop);

    /// <summary>
    /// Stop accepting NEW pipe connections immediately (hot-swap retiring).
    /// Call this as soon as _slackRetiring = true — new Eye takes over new connections instantly.
    /// In-flight connections continue to be served until StopAcceptingAndWaitForDrain() is called.
    /// </summary>
    public static void StopAccepting() => _acceptCts.Cancel();

    /// <summary>
    /// Wait for all in-progress commands to finish.
    /// Call StopAccepting() first (or this will do it too).
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
    /// Determines whether a command should be routed to the MCP subprocess (UIA/Win32 isolation)
    /// or executed in-process (Eye-internal, no UIA dependency).
    /// </summary>
    internal static bool ShouldRouteToMcp(string[] args)
    {
        if (args.Length == 0) return false;
        if (!EyeMcpClient.IsRunning) return false; // fallback to in-process if MCP not available

        var cmd = args[0].ToLowerInvariant();

        // "a11y kill" must NOT go through MCP — it kills processes including the MCP worker itself
        if (cmd == "a11y" && args.Length > 1 && args[1].Equals("kill", StringComparison.OrdinalIgnoreCase))
            return false;

        // Eye-internal commands — run in-process (no UIA dependency)
        return cmd switch
        {
            "slack" => false,     // pure HTTP
            "eye" => false,       // Eye-internal status
            "schedule" => false,  // timer management
            "speak" => false,     // TTS
            "claude-usage" => false, // file reads
            "gc" => false,        // file cleanup
            "screen" => false,    // overlay management
            "validate" => false,  // config validation
            "suggest" => false,   // Slack API + file I/O (no UIA)
            "file" => false,      // file read/edit/grep/glob (no UIA)
            // json-grep removed — use "grap ... --json" instead
            "logcat" => false,    // log search (no UIA)
            "web" => false,       // CDP web operations (no UIA)
            "help" or "--help" or "-h" => false,
            "mcp" => false,       // MCP server itself (avoid recursion)
            "newchat" => false,   // focusless clipboard-based
            "dashboard" => false, // long-running HTTP server (no UIA)
            "ask" => false,       // long-running CDP + Slack streaming (MCP 60s timeout too short)
            "agent" => false,     // long-running loop (MCP 60s timeout too short)
            _ => true             // a11y, inspect, windows, prompt, capture, ocr, scan, etc.
        };
    }

    /// <summary>
    /// Fire-and-forget: run args in background via MCP subprocess or in-process routing.
    /// Output is isolated to a per-invocation log file (not mixed into Eye console).
    /// callerHwnd: explicitly set CallerHwnd (use a fixed sentinel for non-pipe callers such as HoverAnalyzer
    ///             so tab sandbox keys are deterministic — pass null to clear it).
    /// </summary>
    internal static void DispatchBg(string[] args, IntPtr? callerHwnd = null)
    {
        if (ShouldRouteToMcp(args))
        {
            // Route through MCP subprocess — fire-and-forget
            Console.Error.WriteLine($"[DISPATCHBG-MCP] {string.Join(" ", args)}");
            EyeMcpClient.CallFireAndForget(args);
            return;
        }

        // In-process path for Eye-internal commands
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
            // ReadOnlyMode no longer set here — Eye routes all a11y through MCP subprocess
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

        // ── MCP routing: a11y commands go to subprocess, Eye-internal stay in-process ──
        if (ShouldRouteToMcp(args))
        {
            // Set caller context for MCP routing (used by GetSendReplyUsername in MCP worker)
            CallerCwd.Value = callerCwd;
            CallerHwnd.Value = callerHwnd;
            CallerArgs.Value = args;
            CurrentCommandGlobal = args;

            var delegName = Program.GetSendReplyUsername();
            var cmdLine = string.Join(" ", args);
            Console.Error.WriteLine($"[CMD-MCP] name={delegName ?? "?"} cmd={cmdLine} cwd={callerCwd ?? "(none)"} hwnd=0x{callerHwnd?.ToInt64():X}");

            // CWD mismatch: caller has different CWD than Eye's own CWD → MCP worker may use wrong base path.
            // Register as bug for path-sensitive commands (a11y, file, logcat, inspect).
            var eyeCwd = Program.EyeCallerCwd;
            if (!string.IsNullOrEmpty(callerCwd) && !string.IsNullOrEmpty(eyeCwd)
                && !string.Equals(callerCwd.TrimEnd('\\', '/'), eyeCwd.TrimEnd('\\', '/'), StringComparison.OrdinalIgnoreCase)
                && (args.Length > 0 && args[0].ToLowerInvariant() is "a11y" or "file" or "logcat" or "inspect"))
            {
                Console.Error.WriteLine($"[CMD-CWD-MISMATCH] caller={callerCwd} eye={eyeCwd}");
                Program.AutoRegisterBug(
                    $"[BUG-AUTO] CWD mismatch on `{cmdLine}`\n" +
                    $"Caller: {callerCwd}\nEye: {eyeCwd}\n" +
                    $"MCP worker runs with Eye CWD — relative paths in caller may resolve differently.",
                    args, callerCwd);
            }

            EyeMcpClient.CurrentCallerCwd = callerCwd;
            EyeMcpClient.CurrentCallerHwnd = callerHwnd?.ToInt64().ToString("X");
            BeginCommand();
            try
            {
                var _mcpSw = System.Diagnostics.Stopwatch.StartNew();
                var (output, exitCode) = EyeMcpClient.CallAsync(args).GetAwaiter().GetResult();
                Console.Error.WriteLine($"[MCP-PERF] cmd={cmdLine} mcp={_mcpSw.ElapsedMilliseconds}ms exit={exitCode} len={output?.Length ?? 0}");
                pipeWriter.Write(output);
                if (output != null && !output.EndsWith('\n')) pipeWriter.WriteLine();
                return exitCode;
            }
            finally { EndCommand(); }
        }

        // ── Long-running commands (ask/agent): dispatch as background task ──
        // Launcher gets immediate response; Core continues working with Slack streaming.
        var cmd0 = args.Length > 0 ? args[0].ToLowerInvariant() : "";
        if (cmd0 is "ask" or "agent")
        {
            pipeWriter.WriteLine($"[CMD] {string.Join(" ", args)} → dispatched to background");
            pipeWriter.WriteLine($"Log: {logFile}");
            // Pass callerCwd so Slack bot-name and thread work correctly in background
            var bgCwd = callerCwd;
            var bgHwnd = callerHwnd;
            var bgLogDir = Path.Combine(Program.DataDir, "logs");
            Directory.CreateDirectory(bgLogDir);
            var bgCmdTag = args.Length > 1 ? $"{args[0]}-{args[1]}" : (args.Length > 0 ? args[0] : "bg");
            var bgLogFile = Path.Combine(bgLogDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{bgCmdTag}.pid={Environment.ProcessId}.log");
            var bgTee = new TeeTextWriter(TextWriter.Null, bgLogFile);
            _ = Task.Run(() =>
            {
                CallerCwd.Value = bgCwd;
                CallerHwnd.Value = bgHwnd;
                CallerArgs.Value = args;
                CurrentCommandGlobal = args;
                Program.RunningInEye = true;
                using (ThreadRoutingWriter.Route(bgTee))
                {
                    try { Program.Main(args); }
                    catch (Exception ex) { bgTee.WriteLine($"[BG] error: {ex.Message}"); }
                }
                bgTee.Dispose();
            });
            return 0; // immediate return to launcher
        }

        // ── In-process path for Eye-internal commands ──
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
