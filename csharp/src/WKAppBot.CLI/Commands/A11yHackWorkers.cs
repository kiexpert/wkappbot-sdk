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
            var line = $"[{DateTime.Now:HH:mm:ss.fff}] [HACK-HOVER] {msg}";
            Console.WriteLine(line);
            try { File.AppendAllText(logPath, line + Environment.NewLine, Encoding.UTF8); } catch { }
        }

        // UTF-8 output: Launcher already sets SetConsoleOutputCP(65001) + transcoding fallback
        // Core direct run: ensure UTF-8 just in case
        try { Console.OutputEncoding = new System.Text.UTF8Encoding(false); } catch { }

        Log($"Worker started (PID={Environment.ProcessId}, parent={parentPid})");

        // Ensure screen reader mode for Chromium/Electron UIA tree access
        WKAppBot.Win32.Native.ScreenReaderMode.Enable();
        Log($"ScreenReader: enabled={WKAppBot.Win32.Native.ScreenReaderMode.IsEnabled}");

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
        string lastGrap = "";
        int loopCount = 0;
        int lastMouseX = -1, lastMouseY = -1;
        using var uia = new FlaUI.UIA3.UIA3Automation(); // reuse — avoid 4s init per loop

        Log("Loop started — monitoring mouse/focus");

        while (!cts.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(100);
                loopCount++;

                // Get mouse position + window
                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;

                // Mouse moved? → reset debounce + cancel running analysis
                if (pt.X != lastMouseX || pt.Y != lastMouseY)
                {
                    lastMouseX = pt.X; lastMouseY = pt.Y;
                    _hackHoverDebounceSeq++;
                    _hackHoverAnalyzeCts?.Cancel();
                }

                // Direct UIA: find element at mouse position
                var sw = System.Diagnostics.Stopwatch.StartNew();
                string elType = "?", elName = "", elAid = "", elPatterns = "";
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                string proc; try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { proc = "?"; }
                try
                {
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
                }
                catch { }

                if (elName.Length > 30) elName = elName[..30];
                var elLabel = !string.IsNullOrEmpty(elAid) ? elAid : elName;
                var json5 = WKAppBot.Win32.Window.WindowFinder.BuildTargetJson5(hwnd);
                var fullGrap = !string.IsNullOrEmpty(elLabel) ? $"{json5}#*{elLabel}*" : json5;

                // Verify: can we actually target this?
                var verifyHits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(json5, true);
                bool verified = verifyHits.Any(v => v.Handle == hwnd);
                var mark = verified ? "OK" : "MISS";

                var result = $"{fullGrap} [{elType}] {mark}";

                // Console: compact for non-browser, full JSON5 for browser only
                var json5Short = json5;
                // Non-browser: strip domain/url (already filtered in BuildTargetJson5)
                // Further compact: just hwnd + proc for readability
                if (!json5.Contains("domain:"))
                {
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint cpid);
                    string cproc; try { cproc = System.Diagnostics.Process.GetProcessById((int)cpid).ProcessName; } catch { cproc = "?"; }
                    json5Short = $"{{hwnd:0x{hwnd.ToInt64():X8},proc:'{cproc}'}}";
                }
                var compactGrap = !string.IsNullOrEmpty(elLabel) ? $"{json5Short}#*{elLabel}*" : json5Short;

                // Always output (0.1s interval), include mouse coords
                var elMs = sw.ElapsedMilliseconds;
                var richGrap = $"{{hwnd:0x{hwnd.ToInt64():X8},pid:{pid},proc:'{proc}'}}";
                if (!string.IsNullOrEmpty(elLabel))
                    richGrap += $"#*{elLabel}*";

                bool changed = result != lastResult;
                if (changed) lastResult = result;
                bool grapChanged = richGrap != lastGrap;
                lastGrap = richGrap;
                if (RunningInEye && !changed) continue; // Eye: change-only
                if (grapChanged) Console.WriteLine(); // different target pattern → new line
                Console.Write($"{richGrap} [{mark}] {elMs}ms ({pt.X},{pt.Y})          \r");
                Console.Out.Flush();

                // Detailed log (file only, on change)
                if (changed)
                {
                    var ts2 = DateTime.Now.ToString("HH:mm:ss.fff");
                    var logLine = $"[{ts2}] [{mark}] [{elType}] \"{elLabel}\" ({pt.X},{pt.Y}) {elPatterns} -> {compactGrap}";
                    try { File.AppendAllText(logPath, logLine + Environment.NewLine, Encoding.UTF8); } catch { }
                }

                // Debounce before starting heavy analysis
                // Blind window (no UIA label, no patterns) → 1s fast-hack
                // Normal → 9s idle debounce
                bool isBlind = string.IsNullOrEmpty(elLabel) && string.IsNullOrEmpty(elPatterns);
                int debounceMs = isBlind ? 1000 : 9000;
                if (changed)
                {
                    // Cancel any running analysis
                    _hackHoverAnalyzeCts?.Cancel();
                    var hackGrap = isBlind ? json5Short : compactGrap; // blind → window-level hack
                    var debounceStamp = ++_hackHoverDebounceSeq;
                    var analyzeCts = new CancellationTokenSource();
                    _hackHoverAnalyzeCts = analyzeCts;
                    _ = Task.Run(async () =>
                    {
                        try { await Task.Delay(debounceMs, analyzeCts.Token); }
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
