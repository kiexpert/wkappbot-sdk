// A11yHackInputWorker.cs — Standalone hack-input worker (keyboard focus analysis)
// Usage: wkappbot a11y hack-input [--parent-pid N] [--timeout Ns]
// Tracks keyboard focus element, shows input capabilities + parent chain.
// Runs until: Ctrl+C, parent dies, or timeout.

using System.Text;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using System.Diagnostics;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int A11yHackInputWorker(string[] args)
    {
        int parentPid = 0;
        int.TryParse(Environment.GetEnvironmentVariable("WKAPPBOT_PARENT_PID"), out parentPid);
        int timeoutSec = 0;
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
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] [HACK-INPUT] {msg}";
            Console.WriteLine(line);
            try { File.AppendAllText(logPath, line + Environment.NewLine, Encoding.UTF8); } catch { }
        }

        try { Console.OutputEncoding = new UTF8Encoding(false); } catch { }
        Log($"Worker started (PID={Environment.ProcessId}, parent={parentPid})");

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
                    catch { Log("Parent gone — exiting"); cts.Cancel(); break; }
                }
            });
        }
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        if (timeoutSec > 0)
            Task.Run(async () => { await Task.Delay(timeoutSec * 1000); Log($"Timeout {timeoutSec}s — exiting"); cts.Cancel(); });

        using var locator = new UiaLocator();

        string lastKey = "";
        Log("Loop started — monitoring keyboard focus");

        while (!cts.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(200); // focus changes less frequently than mouse

                var sw = System.Diagnostics.Stopwatch.StartNew();
                var info = locator.GetFocusedElementInfo();
                var elMs = sw.ElapsedMilliseconds;

                if (info == null)
                {
                    if (lastKey != "(none)")
                    {
                        Console.WriteLine("[FOCUS] (none)");
                        lastKey = "(none)";
                    }
                    continue;
                }

                // Build unique key for change detection
                var label = !string.IsNullOrEmpty(info.AutomationId) ? info.AutomationId : info.Name;
                if (label.Length > 40) label = label[..40];
                var key = $"{info.ProcessId}:{info.ControlType}:{label}:{info.Bounds}";
                if (key == lastKey) continue;
                lastKey = key;

                // Process info
                string proc;
                try { proc = System.Diagnostics.Process.GetProcessById(info.ProcessId).ProcessName; } catch { proc = "?"; }

                // Window grap
                var fgHwnd = NativeMethods.GetForegroundWindow();
                var json5 = WindowFinder.BuildTargetJson5(fgHwnd);

                // Build output
                var pats = info.Patterns.Count > 0 ? string.Join(",", info.Patterns) : "none";
                var valuePreview = info.Value.Length > 0 ? $" val=\"{(info.Value.Length > 30 ? info.Value[..30] + "…" : info.Value)}\"" : "";

                // Input capability assessment
                var inputMethods = new List<string>();
                if (info.Patterns.Contains("Value")) inputMethods.Add("SetValue");
                if (info.Patterns.Contains("Text")) inputMethods.Add("TextPattern");
                if (info.Patterns.Contains("Invoke")) inputMethods.Add("Invoke");
                if (info.Patterns.Contains("Toggle")) inputMethods.Add("Toggle");
                // Always available as fallback
                inputMethods.Add("SendKeys");
                var inputStr = string.Join("→", inputMethods);

                // grap pattern for this element
                var grap = !string.IsNullOrEmpty(label) ? $"{json5}#*{label}*" : json5;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[FOCUS] ");
                Console.ResetColor();
                Console.WriteLine($"{info.ControlType} \"{label}\" [{pats}] {elMs}ms");
                Console.WriteLine($"  grap: {grap}");
                Console.WriteLine($"  input: {inputStr}{valuePreview}");
                Console.WriteLine($"  proc: {proc} (pid={info.ProcessId}) bounds=({info.Bounds.X},{info.Bounds.Y} {info.Bounds.Width}x{info.Bounds.Height})");

                // Parent chain (compact)
                if (info.ParentChain.Count > 0)
                {
                    var chain = string.Join(" → ", info.ParentChain.Take(5).Select(p =>
                        !string.IsNullOrEmpty(p.name) ? $"{p.type}:\"{p.name}\"" : p.type));
                    Console.WriteLine($"  chain: {chain}");
                }

                // Log to file
                var ts2 = DateTime.Now.ToString("HH:mm:ss.fff");
                var logLine = $"[{ts2}] [FOCUS] {info.ControlType} \"{label}\" [{pats}] input={inputStr} proc={proc} pid={info.ProcessId}";
                try { File.AppendAllText(logPath, logLine + Environment.NewLine, Encoding.UTF8); } catch { }
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
