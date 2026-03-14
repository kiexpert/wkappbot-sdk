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
    internal const string CwdPrefix = "__cwd:";
    internal const string BgFlag = "__bg";

    /// <summary>Per-command caller CWD (set by Launcher, read by GetSessionTag)</summary>
    internal static readonly AsyncLocal<string?> CallerCwd = new();

    public static void StartServer() => Task.Run(ServerLoop);

    /// <summary>
    /// Fire-and-forget: run args in background via the same in-process routing as __bg pipe commands.
    /// Output is isolated to a per-invocation log file (not mixed into Eye console).
    /// </summary>
    internal static void DispatchBg(string[] args)
    {
        var logDir = Path.Combine(Program.DataDir, "logs");
        Directory.CreateDirectory(logDir);
        var cmdTag = args.Length > 0 ? args[0] : "bg";
        if (args.Length > 1 && cmdTag is "slack" or "ask") cmdTag += $"-{args[1]}";
        var logFile = Path.Combine(logDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={Environment.ProcessId}.txt");
        var tee = new TeeTextWriter(TextWriter.Null, logFile);
        _ = Task.Run(() =>
        {
            CallerCwd.Value = null;
            Program.RunningInEye = true;
            using (ThreadRoutingWriter.Route(tee))
            {
                try { Program.Main(args); }
                catch (Exception ex) { tee.WriteLine($"[DISPATCHBG] error: {ex.Message}"); }
            }
            tee.Dispose();
        });
    }

    static async Task ServerLoop()
    {
        Console.WriteLine("[EYE] CmdPipe server started — delegated commands run in-process (parallel)");
        while (true)
        {
            try
            {
                var pipe = new NamedPipeServerStream(
                    PipeName, PipeDirection.InOut,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                await pipe.WaitForConnectionAsync();
                _ = Task.Run(() => HandleClientAsync(pipe));
            }
            catch { await Task.Delay(1000); }
        }
    }

    static async Task HandleClientAsync(NamedPipeServerStream pipe)
    {
        using (pipe)
        {
            StreamWriter? pw = null;
            try
            {
                var line = await new StreamReader(pipe, leaveOpen: true).ReadLineAsync();
                var args = JsonSerializer.Deserialize<string[]>(line ?? "[]") ?? [];
                pw = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };

                // Extract CWD prefix (prepended by Launcher, backward-compatible)
                string? callerCwd = null;
                if (args.Length > 0 && args[0].StartsWith(CwdPrefix, StringComparison.Ordinal))
                {
                    callerCwd = args[0].Substring(CwdPrefix.Length);
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
                    var bgArgs = args; var bgCwd = callerCwd;
                    _ = Task.Run(() => RunInEye(bgArgs, TextWriter.Null, bgCwd));
                }
                else
                {
                    int code = RunInEye(args, pw, callerCwd);
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

    static int RunInEye(string[] args, TextWriter pipeWriter, string? callerCwd)
    {
        // Build per-command log file path (same convention as Program.cs non-Eye mode)
        var logDir = Path.Combine(Program.DataDir, "logs");
        Directory.CreateDirectory(logDir);
        var cmdTag = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
        if (args.Length > 1 && cmdTag is "slack" or "web" or "schedule" or "knowhow" or "ask")
            cmdTag += $"-{args[1].ToLowerInvariant()}";
        var logFile = Path.Combine(logDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={Environment.ProcessId}.txt");

        // TeeTextWriter wraps pipeWriter: output goes to pipe (real-time) AND log file.
        // AsyncLocal routing isolates this command's output from concurrent commands.
        var tee = new TeeTextWriter(pipeWriter, logFile);
        int code;
        // CallerCwd stored in AsyncLocal — propagates to all async continuations of this command
        CallerCwd.Value = callerCwd;
        // RunningInEye=true prevents Program.cs from creating a second TeeTextWriter (duplicate log)
        Program.RunningInEye = true;
        using (ThreadRoutingWriter.Route(tee))
        {
            try { code = Program.Main(args); }
            catch (Exception ex) { tee.WriteLine($"[EYECMD] error: {ex.Message}"); code = 1; }
        }
        tee.Dispose(); // moves log to old/, updates tee.LogPath
        // "Log saved:" goes through pipeWriter directly (tee already disposed)
        pipeWriter.WriteLine($"Log saved: {tee.LogPath}");
        return code;
    }
}
