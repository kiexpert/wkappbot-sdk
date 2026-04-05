// A11yHackWorkers.cs — Standalone hack worker processes
// Usage: wkappbot a11y hack-hover [--parent-pid N]
//        wkappbot a11y hack-input [--parent-pid N]
// Independent processes: Eye spawns them, they watch parent PID and exit when Eye dies.
// Logs go to eye-hack.log (consolidated).

using System.Text;
using System.Text.Json;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Standalone hover analysis worker.
    /// Monitors mouse position → UIA + CCA analysis on hovered window.
    /// </summary>
    static int A11yHackHoverWorker(string[] args)
    {
        int parentPid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out parentPid);
        // Also accept --parent-pid N from command line
        for (int i = 0; i < args.Length - 1; i++)
            if (args[i] == "--parent-pid" && int.TryParse(args[i + 1], out var pp)) parentPid = pp;

        var logPath = Path.Combine(
            Path.GetDirectoryName(Environment.ProcessPath ?? AppContext.BaseDirectory) ?? "",
            "wkappbot.hq", "logs", "eye-hack.log");
        Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);

        void Log(string msg)
        {
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] [HACK-HOVER] {msg}";
            Console.WriteLine(line);
            try { File.AppendAllText(logPath, line + Environment.NewLine, Encoding.UTF8); } catch { }
        }

        Log($"Worker started (PID={Environment.ProcessId}, parent={parentPid})");

        // Start the analyze-hack server subprocess
        EnsureHackServer();
        Log("Server ready");

        // Start unified analysis loop on this thread
        using var cts = new CancellationTokenSource();

        // Parent PID watch thread
        if (parentPid > 0)
        {
            Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(5000);
                    try { System.Diagnostics.Process.GetProcessById(parentPid); }
                    catch
                    {
                        Log($"Parent (PID={parentPid}) gone — exiting");
                        cts.Cancel();
                        break;
                    }
                }
            });
        }

        // Console Ctrl+C handler
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        try
        {
            // Run the existing unified analysis loop (mouse + focus tracking)
            UnifiedAnalysisLoop(cts.Token).GetAwaiter().GetResult();
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Log($"Error: {ex.Message}"); }

        Log("Worker stopped");
        return 0;
    }

    // TODO: A11yHackInputWorker — keyboard focus analysis (future)
}
