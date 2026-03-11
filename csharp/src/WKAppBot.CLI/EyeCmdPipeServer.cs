using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Eye command pipe server — executes delegated CLI commands in-process (zero cold-start).
/// Protocol: client sends JSON string[] args line → server streams output lines → sends "\x00END {code}".
/// Only one command at a time (SemaphoreSlim). Console.SetOut is redirected per-command via StringWriter.
/// </summary>
internal static class EyeCmdPipeServer
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";

    static readonly SemaphoreSlim _sem = new(1, 1);

    public static void StartServer() => Task.Run(ServerLoop);

    static async Task ServerLoop()
    {
        Console.WriteLine("[EYE] CmdPipe server started — delegated commands run in-process");
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

    static async Task<int> RunInEyeAsync(string[] args, TextWriter pipeWriter)
    {
        // Serialize for RunningInEye flag (static state safety)
        await _sem.WaitAsync();
        var sw = new StringWriter();
        int code;
        try
        {
            Program.RunningInEye = true;
            // Thread-local route: only THIS thread writes to sw, Eye tick/slack → global
            using (ThreadRoutingWriter.Route(sw))
            {
                try { code = Program.Main(args); }
                catch (Exception ex) { sw.WriteLine($"[EYECMD] error: {ex.Message}"); code = 1; }
            }
        }
        finally
        {
            Program.RunningInEye = false;
            _sem.Release();
        }

        await pipeWriter.WriteAsync(sw.ToString());
        return code;
    }
}
