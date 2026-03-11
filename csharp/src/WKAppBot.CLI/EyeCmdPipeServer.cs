using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Eye command pipe server — executes delegated CLI commands in-process (zero cold-start).
/// Protocol: client sends JSON string[] args line → server streams output lines → sends "\x00END {code}".
/// Parallel execution: AsyncLocal routing isolates each command's output. No global serialization.
/// TeeTextWriter wraps pipeWriter per-command → real-time streaming to pipe + per-command log file.
/// </summary>
internal static class EyeCmdPipeServer
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";

    public static void StartServer() => Task.Run(ServerLoop);

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
                int code = await RunInEyeAsync(args, pw);
                await pw.WriteLineAsync($"{EndMarker} {code}");
            }
            catch (Exception ex)
            {
                try { await (pw ?? new StreamWriter(pipe)).WriteLineAsync($"{EndMarker} 1"); } catch { }
                Console.WriteLine($"[EYE:PIPE] error: {ex.Message}");
            }
        }
    }

    static Task<int> RunInEyeAsync(string[] args, TextWriter pipeWriter)
    {
        // Build per-command log file path (same convention as Program.cs non-Eye mode)
        var logDir = Path.Combine(Program.DataDir, "logs");
        Directory.CreateDirectory(logDir);
        var cmdTag = args.Length > 0 ? args[0].ToLowerInvariant() : "noargs";
        if (args.Length > 1 && cmdTag is "slack" or "web" or "schedule" or "knowhow")
            cmdTag += $"-{args[1].ToLowerInvariant()}";
        var logFile = Path.Combine(logDir, $"wkappbot-core.exe.out-{DateTime.Now:yyyyMMdd_HHmmss}.{cmdTag}.pid={Environment.ProcessId}.txt");

        // TeeTextWriter wraps pipeWriter: output goes to pipe (real-time) AND log file.
        // AsyncLocal routing isolates this command's output from concurrent commands.
        var tee = new TeeTextWriter(pipeWriter, logFile);
        int code;
        using (ThreadRoutingWriter.Route(tee))
        {
            try { code = Program.Main(args); }
            catch (Exception ex) { tee.WriteLine($"[EYECMD] error: {ex.Message}"); code = 1; }
        }
        tee.Dispose(); // moves log to old/, updates tee.LogPath
        // "Log saved:" goes through pipeWriter directly (tee already disposed)
        pipeWriter.WriteLine($"Log saved: {tee.LogPath}");
        return Task.FromResult(code);
    }
}
