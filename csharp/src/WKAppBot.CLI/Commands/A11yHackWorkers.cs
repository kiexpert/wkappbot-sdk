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
    static volatile int _hackHoverDebounceSeq;
    static volatile bool _hackHoverAnalyzing;
    static CancellationTokenSource? _hackHoverAnalyzeCts;

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

        // Main loop: mouse/focus → UIA → console output
        string lastResult = "";
        int loopCount = 0;
        int lastMouseX = -1, lastMouseY = -1;

        Log("Loop started — monitoring mouse/focus");

        while (!cts.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(100); // 0.1초마다 계산
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

                // Mouse moved? → reset debounce + cancel running analysis
                if (pt.X != lastMouseX || pt.Y != lastMouseY)
                {
                    lastMouseX = pt.X; lastMouseY = pt.Y;
                    _hackHoverDebounceSeq++;
                    _hackHoverAnalyzeCts?.Cancel(); // cancel pending/running analysis
                }

                // Direct UIA: find element at mouse position
                var sw = System.Diagnostics.Stopwatch.StartNew();
                string elType = "?", elName = "", elAid = "", elPatterns = "", winTitle = "";
                string grapPath = "";
                try
                {
                    using var uia = new FlaUI.UIA3.UIA3Automation();
                    var el = uia.FromPoint(new System.Drawing.Point(pt.X, pt.Y));
                    if (el != null)
                    {
                        try { elType = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        try { elName = el.Properties.Name.ValueOrDefault ?? ""; } catch { }
                        try { elAid = el.Properties.AutomationId.ValueOrDefault ?? ""; } catch { }
                        var pats = new List<string>();
                        try { if (el.Patterns.Invoke.IsSupported) pats.Add("Invoke"); } catch { }
                        try { if (el.Patterns.Value.IsSupported) pats.Add("Value"); } catch { }
                        try { if (el.Patterns.Toggle.IsSupported) pats.Add("Toggle"); } catch { }
                        elPatterns = string.Join(",", pats);
                    }
                    // Window title
                    var winEl = uia.FromHandle(hwnd);
                    if (winEl != null)
                        try { winTitle = winEl.Properties.Name.ValueOrDefault ?? ""; } catch { }
                }
                catch { }

                if (elName.Length > 30) elName = elName[..30];
                if (winTitle.Length > 40) winTitle = winTitle[..40];
                var elLabel = !string.IsNullOrEmpty(elAid) ? elAid : elName;
                // Build targetable grap using JSON5 (hwnd-based = always works)
                var json5 = WKAppBot.Win32.Window.WindowFinder.BuildTargetJson5(hwnd);
                grapPath = !string.IsNullOrEmpty(elLabel) ? $"{json5}#*{elLabel}*" : json5;

                // Verify: can we actually target this?
                var verifyHits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(json5, true);
                bool verified = verifyHits.Any(v => v.Handle == hwnd);
                var mark = verified ? "OK" : "MISS";

                var result = $"{grapPath} [{elType}] {mark}";

                // Log only on change (file), but always \r output (console)
                bool changed = result != lastResult;
                if (changed) lastResult = result;

                // Console output — grap pattern + verify mark, \r continuous
                var elMs = sw.ElapsedMilliseconds;
                var line = $"[{mark}] {elMs}ms {grapPath}";
                if (line.Length > 150) line = line[..150];
                Console.Write($"\r{line.PadRight(155)}\r");

                // Detailed log (file only, on change)
                if (changed)
                {
                    var ts2 = DateTime.Now.ToString("HH:mm:ss.fff");
                    var logLine = $"[{ts2}] [{mark}] [{elType}] \"{elLabel}\" ({pt.X},{pt.Y}) {elPatterns} -> {grapPath}";
                    try { File.AppendAllText(logPath, logLine + Environment.NewLine, Encoding.UTF8); } catch { }
                }

                // Debounce: 9s mouse idle before starting heavy analysis
                if (changed)
                {
                    // Cancel any running analysis
                    _hackHoverAnalyzeCts?.Cancel();
                    var hackGrap = grapPath;
                    var debounceStamp = ++_hackHoverDebounceSeq;
                    var analyzeCts = new CancellationTokenSource();
                    _hackHoverAnalyzeCts = analyzeCts;
                    _ = Task.Run(async () =>
                    {
                        try { await Task.Delay(9000, analyzeCts.Token); }
                        catch (OperationCanceledException) { return; }
                        if (_hackHoverDebounceSeq != debounceStamp) return;
                        Console.WriteLine($"\n[ANALYZING] {hackGrap}");
                        _hackHoverAnalyzing = true;
                        try { A11yHackCommand(new[] { hackGrap }); }
                        catch (Exception hex) { Log($"Hack error: {hex.Message}"); }
                        finally { _hackHoverAnalyzing = false; }
                    });
                }
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
