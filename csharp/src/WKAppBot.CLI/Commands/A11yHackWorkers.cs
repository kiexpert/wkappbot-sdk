// A11yHackWorkers.cs — Standalone hack-hover worker
// Usage: wkappbot a11y hack-hover [--parent-pid N]
// Monitors mouse/focus → analyze-hack server via pipe → console output + overlay
// Runs until: Ctrl+C, parent dies, or server pipe breaks.

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static volatile string _hackHoverDebounceGrap = "";

    static int A11yHackHoverWorker(string[] args)
    {
        int parentPid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out parentPid);
        int timeoutSec = 0; // 0 = infinite
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--parent-pid" && int.TryParse(args[i + 1], out var pp)) parentPid = pp;
            if (args[i] == "--timeout" && int.TryParse(args[i + 1], out var ts)) timeoutSec = ts;
        }

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

        // Start analyze-hack server
        EnsureHackServer();
        if (_hackServerProcess is not { HasExited: false } || _hackServerStdin == null)
        {
            Log("Server failed to start — exiting");
            return 1;
        }
        Log($"Server ready (PID={_hackServerProcess.Pid})");

        using var cts = new CancellationTokenSource();

        // Parent PID watch
        if (parentPid > 0)
        {
            Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(5000);
                    try { System.Diagnostics.Process.GetProcessById(parentPid); }
                    catch { Log($"Parent gone — exiting"); cts.Cancel(); break; }
                }
            });
        }
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        if (timeoutSec > 0)
            Task.Run(async () => { await Task.Delay(timeoutSec * 1000); Log($"Timeout {timeoutSec}s — exiting"); cts.Cancel(); });

        // Main loop: mouse/focus → server pipe → console output
        string lastResult = "";
        string lastNodeKey = "";
        int loopCount = 0;

        Log("Loop started — monitoring mouse/focus");

        while (!cts.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(loopCount == 0 ? 1000 : 200);
                loopCount++;

                // Check server alive
                if (_hackServerProcess is not { HasExited: false })
                {
                    Log("Server pipe broken — restarting");
                    EnsureHackServer();
                    if (_hackServerProcess is not { HasExited: false }) { Thread.Sleep(2000); continue; }
                }

                // Get mouse position + window
                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;

                // Change detection
                var nodeKey = $"{hwnd:X8}:{pt.X / 20},{pt.Y / 20}"; // 20px grid dedup
                var idleMs = NativeMethods.GetUserIdleMs();
                if (idleMs < 500) continue; // user actively moving
                if (nodeKey == lastNodeKey && loopCount > 1) continue;
                lastNodeKey = nodeKey;

                // Send request to server
                var request = $"{{\"hwnd\":\"{hwnd:X8}\",\"x\":{pt.X},\"y\":{pt.Y}}}";
                _hackServerStdin!.WriteLine(request);
                _hackServerStdin.Flush();

                // Read response
                var response = _hackServerProcess!.StdOut!.ReadLine();
                if (string.IsNullOrEmpty(response)) continue;

                // Parse
                var resp = JsonSerializer.Deserialize<JsonNode>(response);
                var grapPath = resp?["grapPath"]?.GetValue<string>() ?? "";
                var elName = resp?["elName"]?.GetValue<string>() ?? "";
                var elType = resp?["elType"]?.GetValue<string>() ?? "?";
                var elPatterns = resp?["elPatterns"]?.GetValue<string>() ?? "";
                var winTitle = resp?["winTitle"]?.GetValue<string>() ?? "";
                var ccaNode = resp?["cca"];
                int totalSeg = ccaNode?["total"]?.GetValue<int>() ?? 0;
                int textCnt = ccaNode?["text"]?.GetValue<int>() ?? 0;
                int iconCnt = ccaNode?["icon"]?.GetValue<int>() ?? 0;

                // Build result
                if (winTitle.Length > 40) winTitle = winTitle[..40];
                if (elName.Length > 30) elName = elName[..30];
                var result = $"{grapPath} [{elType}] \"{elName}\" CCA:{totalSeg}";

                // Change detection on result
                if (result == lastResult) continue;
                lastResult = result;

                // Console output — \r overwrite for live status
                var statusLine = $"[{elType}] \"{elName}\" ({pt.X},{pt.Y}) CCA:{totalSeg}";
                if (statusLine.Length > 100) statusLine = statusLine[..100];
                Console.Write($"\r{statusLine.PadRight(110)}\r");

                // Detailed log (file only, not console spam)
                var ts = DateTime.Now.ToString("HH:mm:ss.fff");
                var logLine = $"[{ts}] [HOVER] {grapPath}\n" +
                    $"[{ts}]   Window: \"{winTitle}\"\n" +
                    $"[{ts}]   UIA: [{elType}] \"{elName}\"{(!string.IsNullOrEmpty(elPatterns) ? $" ({elPatterns})" : "")}\n" +
                    $"[{ts}]   CCA: {totalSeg} seg (T={textCnt} I={iconCnt})\n" +
                    $"[{ts}]   Mouse: ({pt.X},{pt.Y})\n" +
                    $"[{ts}]   GRAP: a11y read \"{grapPath}\"";
                try { File.AppendAllText(logPath, logLine + Environment.NewLine, Encoding.UTF8); } catch { }

                // Debounce: wait 1s stable before starting analysis
                var hackGrap = grapPath;
                _hackHoverDebounceGrap = hackGrap;
                _ = Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    if (_hackHoverDebounceGrap != hackGrap) return; // changed during wait
                    Console.WriteLine(); // newline after \r status
                    Log($"Analyzing: {hackGrap}");
                    try { A11yHackCommand(new[] { hackGrap }); }
                    catch (Exception hex) { Log($"Hack error: {hex.Message}"); }
                });
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                Thread.Sleep(1000);
            }
        }

        Log("Worker stopped");
        return 0;
    }
}
