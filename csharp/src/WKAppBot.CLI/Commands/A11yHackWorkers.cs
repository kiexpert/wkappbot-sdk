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
    static volatile bool _hackHoverBlindMode; // blind hack in progress — don't cancel on mouse move
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
            Console.Error.WriteLine(line);
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
        var _hoverUiaBoxes = new List<A11yHackOverlayBox>();
        var _hoverExpBoxes = new List<A11yHackOverlayBox>();
        string _lastExpProc = "";
        string _lastOverlayHash = "";
        int loopCount = 0;
        int lastMouseX = -1, lastMouseY = -1;
        using var uia = new FlaUI.UIA3.UIA3Automation(); // reuse — avoid 4s init per loop

        // Hot-swap: detect .new.exe via shared TryRenameSwap
        var corePath = Environment.ProcessPath ?? "";
        int swapCheckCounter = 0;

        // Experience DB FSW: file changes → overlay refresh
        var expDir = Path.Combine(DataDir, "experience");
        Directory.CreateDirectory(expDir);
        int _expDirty = 0;
        var expWatcher = new FileSystemWatcher(expDir)
        {
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true,
        };
        expWatcher.Created += (_, _) => Interlocked.Exchange(ref _expDirty, 1);
        expWatcher.Changed += (_, _) => Interlocked.Exchange(ref _expDirty, 1);
        Log($"ExpDB watcher: {expDir}");

        Log("Loop started — monitoring mouse/focus");

        while (!cts.IsCancellationRequested)
        {
            try
            {
                Thread.Sleep(100);
                loopCount++;

                // Hot-swap check every 5s via shared TryRenameSwap
                if (++swapCheckCounter >= 50)
                {
                    swapCheckCounter = 0;
                    try
                    {
                        var swapResult = TryRenameSwap(corePath, "HOVER:HOTSWAP");
                        if (swapResult == HotSwapResult.Swapped)
                        {
                            Log("[HOTSWAP] Binary swapped — exit 99 → Launcher will re-spawn Core");
                            Console.Error.WriteLine($"\n[HOTSWAP] Restarting with new version...");
                            return 99; // Signal Launcher to re-spawn Core (same terminal, no new window)
                        }
                    }
                    catch { }
                }

                // Get mouse position + window
                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;

                // Mouse moved? → reset debounce + cancel running analysis (but not blind hack)
                if (pt.X != lastMouseX || pt.Y != lastMouseY)
                {
                    lastMouseX = pt.X; lastMouseY = pt.Y;
                    if (!_hackHoverBlindMode) // blind hack runs regardless of mouse movement
                    {
                        _hackHoverDebounceSeq++;
                        _hackHoverAnalyzeCts?.Cancel();
                    }
                }

                // Direct UIA: find element at mouse position
                var sw = System.Diagnostics.Stopwatch.StartNew();
                string elType = "?", elName = "", elAid = "", elPatterns = "";
                System.Drawing.Rectangle elBounds = default;
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                string proc; try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { proc = "?"; }
                FlaUI.Core.AutomationElements.AutomationElement? curEl = null;
                try
                {
                    curEl = uia.FromPoint(new System.Drawing.Point(pt.X, pt.Y));
                    if (curEl != null)
                    {
                        try { elType = curEl.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        try { elName = curEl.Properties.Name.ValueOrDefault ?? ""; } catch { }
                        try { elAid = curEl.Properties.AutomationId.ValueOrDefault ?? ""; } catch { }
                        try { var br = curEl.BoundingRectangle; elBounds = new System.Drawing.Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height); } catch { }
                        var pats = new List<string>();
                        try { if (curEl.Patterns.Invoke.IsSupported) pats.Add("Invoke"); } catch { }
                        try { if (curEl.Patterns.Value.IsSupported) pats.Add("Value"); } catch { }
                        try { if (curEl.Patterns.Toggle.IsSupported) pats.Add("Toggle"); } catch { }
                        elPatterns = string.Join(",", pats);
                    }
                }
                catch { }

                if (elName.Length > 30) elName = elName[..30];
                var elLabel = !string.IsNullOrEmpty(elAid) ? elAid : elName;
                // Window grap: short pattern for readability
                var winTitle = NativeMethods.GetWindowTextW(hwnd);
                if (winTitle.Length > 20) winTitle = winTitle[..20];
                var winGrap = $"\"*{winTitle}*\"";
                // Full grap with #scope for targeting
                var fullGrap = !string.IsNullOrEmpty(elLabel) ? $"{winGrap}#*{elLabel}*" : winGrap;

                // Verify
                var json5 = WKAppBot.Win32.Window.WindowFinder.BuildTargetJson5(hwnd);
                var verifyHits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(json5, true);
                bool verified = verifyHits.Any(v => v.Handle == hwnd);
                var mark = verified ? "OK" : "MISS";

                // XML tag with attributes
                var elNodeTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(elType, elAid);
                var attrs = new List<string>();
                if (elBounds.Width > 0) attrs.Add($"ltwh={elBounds.X},{elBounds.Y},{elBounds.Width},{elBounds.Height}");
                if (!string.IsNullOrEmpty(elName)) attrs.Add($"name=\"{elName}\"");
                if (!string.IsNullOrEmpty(elPatterns)) attrs.Add($"pat=\"{elPatterns}\"");
                var xmlTag = attrs.Count > 0 ? $"<{elNodeTag} {string.Join(" ", attrs)}>" : $"<{elNodeTag}>";
                var result = $"{fullGrap} {xmlTag} {mark}";

                var elMs = sw.ElapsedMilliseconds;

                bool changed = result != lastResult;
                if (changed) lastResult = result;
                bool grapChanged = fullGrap != lastGrap;
                lastGrap = fullGrap;
                if (RunningInEye && !changed) continue; // Eye: change-only
                if (grapChanged) Console.WriteLine(); // different target pattern → new line
                Console.Write($"{fullGrap} // {xmlTag} [{mark}] {elMs}ms          \r");
                Console.Out.Flush();

                // ── Live overlay: root window size, known nodes as dashed boxes ──
                if (!_hackHoverAnalyzing) // don't interfere with hack analysis overlay
                {
                    try
                    {
                        // Root window rect
                        var rootHwnd = NativeMethods.GetAncestor(hwnd, 2 /* GA_ROOT */);
                        if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;
                        NativeMethods.GetWindowRect(rootHwnd, out var rootWr);
                        int rootW = rootWr.Right - rootWr.Left, rootH = rootWr.Bottom - rootWr.Top;
                        if (rootW > 0 && rootH > 0)
                        {
                            var overlay = A11yHackOverlayHost.GetOrCreate(
                                OverlaySlot.Input, rootWr.Left, rootWr.Top, rootW, rootH);
                            if (overlay != null)
                            {
                                var boxes = new List<A11yHackOverlayBox>();
                                // Current hovered element + parent chain
                                if (elBounds.Width > 0 && elBounds.Height > 0)
                                {
                                    var elTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(elType, elAid);
                                    boxes.Add(new A11yHackOverlayBox(
                                        new System.Windows.Rect(
                                            elBounds.X - rootWr.Left, elBounds.Y - rootWr.Top,
                                            elBounds.Width, elBounds.Height),
                                        $"{elTag} {elBounds.Width}x{elBounds.Height}", null, HackBoxRole.Target));
                                }
                                // Parent chain → root (Scope = 2x thick dashed)
                                if (curEl != null)
                                {
                                    try
                                    {
                                        var walker = uia.TreeWalkerFactory.GetRawViewWalker();
                                        var parent = curEl;
                                        for (int depth = 0; depth < 20; depth++)
                                        {
                                            try { parent = walker.GetParent(parent); } catch { break; }
                                            if (parent == null) break;
                                            try
                                            {
                                                var pr = parent.BoundingRectangle;
                                                if (pr.Width < 5 || pr.Height < 5) continue;
                                                string pCt = "?", pAid = "", pNm = "";
                                                try { pCt = parent.ControlType.ToString(); } catch { }
                                                try { pAid = parent.AutomationId ?? ""; } catch { }
                                                try { pNm = parent.Name ?? ""; } catch { }
                                                var pTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(pCt, pAid, depth);
                                                boxes.Add(new A11yHackOverlayBox(
                                                    new System.Windows.Rect(
                                                        pr.X - rootWr.Left, pr.Y - rootWr.Top,
                                                        pr.Width, pr.Height),
                                                    $"{pTag} {(int)pr.Width}x{(int)pr.Height}", null, HackBoxRole.Scope));
                                            }
                                            catch { }
                                        }
                                    }
                                    catch { }
                                }
                                // Quick UIA children of root (depth 1 — lightweight)
                                // Refresh on: window change OR experience DB file change
                                bool expRefresh = Interlocked.Exchange(ref _expDirty, 0) != 0;
                                if (grapChanged || expRefresh)
                                {
                                    _hoverUiaBoxes.Clear();
                                    try
                                    {
                                        var rootEl = uia.FromHandle(rootHwnd);
                                        if (rootEl != null)
                                        {
                                            // Recursive scan with sibling index tracking
                                            void ScanChildren(FlaUI.Core.AutomationElements.AutomationElement parent)
                                            {
                                                if (_hoverUiaBoxes.Count >= 500) return;
                                                FlaUI.Core.AutomationElements.AutomationElement[] children;
                                                try { children = parent.FindAllChildren(); } catch { return; }
                                                for (int ci = 0; ci < children.Length; ci++)
                                                {
                                                    if (_hoverUiaBoxes.Count >= 500) break;
                                                    var ch = children[ci];
                                                    try
                                                    {
                                                        var cr = ch.BoundingRectangle;
                                                        if (cr.Width < 5 || cr.Height < 5) { ScanChildren(ch); continue; }
                                                        if (cr.X < rootWr.Left - 10 || cr.Y < rootWr.Top - 10) { ScanChildren(ch); continue; }
                                                        string cCt = "?", cAid = "";
                                                        try { cCt = ch.ControlType.ToString(); } catch { }
                                                        try { cAid = ch.AutomationId ?? ""; } catch { }
                                                        var cTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(cCt, cAid, ci + 1);
                                                        _hoverUiaBoxes.Add(new A11yHackOverlayBox(
                                                            new System.Windows.Rect(
                                                                cr.X - rootWr.Left, cr.Y - rootWr.Top,
                                                                cr.Width, cr.Height),
                                                            $"{cTag} {(int)cr.Width}x{(int)cr.Height}", null, HackBoxRole.Known));
                                                    }
                                                    catch { }
                                                    ScanChildren(ch);
                                                }
                                            }
                                            ScanChildren(rootEl);
                                        }
                                    }
                                    catch { }
                                }
                                boxes.AddRange(_hoverUiaBoxes);

                                // ── Experience DB overlay: Cached boxes from form_*.json ──
                                if (grapChanged || expRefresh || proc != _lastExpProc)
                                {
                                    _hoverExpBoxes.Clear();
                                    _lastExpProc = proc;
                                    try
                                    {
                                        var profDir = Path.Combine(DataDir, "profiles", $"{proc.ToLowerInvariant()}_exp");
                                        if (Directory.Exists(profDir))
                                        {
                                            foreach (var formFile in Directory.GetFiles(profDir, "form_*.json"))
                                            {
                                                try
                                                {
                                                    var form = System.Text.Json.JsonSerializer.Deserialize<WKAppBot.Win32.Window.FormExperience>(
                                                        File.ReadAllText(formFile));
                                                    if (form?.Controls == null) continue;
                                                    foreach (var ctrl in form.Controls)
                                                    {
                                                        if (ctrl.Width < 5 || ctrl.Height < 5) continue;
                                                        if (ctrl.RelativeX <= 0 && ctrl.RelativeY <= 0) continue;
                                                        var cx = ctrl.RelativeX * rootW;
                                                        var cy = ctrl.RelativeY * rootH;
                                                        var tag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(
                                                            ctrl.ClassName ?? "?", null, ctrl.ControlId, isDynamic: true);
                                                        _hoverExpBoxes.Add(new A11yHackOverlayBox(
                                                            new System.Windows.Rect(cx, cy, ctrl.Width, ctrl.Height),
                                                            $"{tag} {ctrl.Width}x{ctrl.Height}", null, HackBoxRole.Cached));
                                                    }
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                boxes.AddRange(_hoverExpBoxes);

                                // Skip render if boxes unchanged (avoid flicker on FSW noise)
                                var hash = string.Join("|", boxes.Select(b => $"{b.Bounds}{b.Label}{b.Role}"));
                                if (hash != _lastOverlayHash)
                                {
                                    _lastOverlayHash = hash;
                                    overlay.Update(new A11yHackOverlayModel(boxes));
                                }
                            }
                        }
                    }
                    catch { }
                }

                // Detailed log (file only, on change)
                if (changed)
                {
                    var ts2 = DateTime.Now.ToString("HH:mm:ss.fff");
                    var logLine = $"[{ts2}] {fullGrap} // {xmlTag} [{mark}] {elMs}ms";
                    try { File.AppendAllText(logPath, logLine + Environment.NewLine, Encoding.UTF8); } catch { }
                }

                // Blind = no UIA info OR no experience DB layout for this window
                // → immediate hack (experience DB build, uncancellable)
                bool uiaBlind = string.IsNullOrEmpty(elLabel) && string.IsNullOrEmpty(elPatterns);
                bool expMissing = false;
                if (!uiaBlind) // UIA has info, but check experience DB for window layout
                {
                    try
                    {
                        var clsBuf = new System.Text.StringBuilder(256);
                        NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
                        var winExpDir = Path.Combine(DataDir, "experience", proc, clsBuf.ToString());
                        expMissing = !Directory.Exists(winExpDir) || Directory.GetFiles(winExpDir, "*.png").Length == 0;
                    }
                    catch { }
                }
                bool isBlind = uiaBlind || expMissing;
                if (changed)
                {
                    if (!isBlind) _hackHoverAnalyzeCts?.Cancel(); // don't cancel blind hack
                    var hackGrap = isBlind ? json5 : fullGrap;
                    var debounceStamp = ++_hackHoverDebounceSeq;
                    var analyzeCts = new CancellationTokenSource();
                    _hackHoverAnalyzeCts = analyzeCts;
                    _ = Task.Run(async () =>
                    {
                        if (!isBlind)
                        {
                            try { await Task.Delay(9000, analyzeCts.Token); }
                            catch (OperationCanceledException) { return; }
                            if (_hackHoverDebounceSeq != debounceStamp) return;
                        }
                        _hackHoverBlindMode = isBlind;
                        Console.Error.WriteLine($"\n[ANALYZING{(isBlind ? ":BLIND" : "")}] {hackGrap}");
                        _hackHoverAnalyzing = true;
                        try { A11yHackCommand(new[] { hackGrap }); }
                        catch (Exception hex) { Log($"Hack error: {hex.Message}"); }
                        finally { _hackHoverAnalyzing = false; _hackHoverBlindMode = false; }
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
