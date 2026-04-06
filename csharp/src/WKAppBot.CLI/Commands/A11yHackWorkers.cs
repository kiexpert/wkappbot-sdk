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
        bool renderAll = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--render-all") renderAll = true;
            if (i + 1 < args.Length && args[i] == "--parent-pid" && int.TryParse(args[i + 1], out var pp)) parentPid = pp;
            if (i + 1 < args.Length && args[i] == "--timeout" && int.TryParse(args[i + 1], out var ts)) timeoutSec = ts;
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

                // Build tag path: walk parent chain → "Pane/Group_1th/Edit_1052"
                // Absolute tag path via shared helper
                var tagPath = curEl != null
                    ? WKAppBot.Win32.Accessibility.GrapHelper.BuildAbsoluteTagPath(curEl, uia.TreeWalkerFactory.GetRawViewWalker())
                    : WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(elType, elAid);

                var shortWin = $"{{hwnd:0x{hwnd.ToInt64():X},proc:'{proc}'}}";
                var result = $"{shortWin}#{tagPath} {mark}";

                var elMs = sw.ElapsedMilliseconds;

                bool changed = result != lastResult;
                if (changed) lastResult = result;
                bool grapChanged = shortWin != lastGrap;
                lastGrap = shortWin;
                if (RunningInEye && !changed) continue; // Eye: change-only
                if (grapChanged) Console.WriteLine(); // different target pattern → new line
                Console.Write($"{shortWin}#{tagPath} [{mark}] {elMs}ms          \r");
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
                                            // Helper: add one element as Known box
                                            void AddUiaBox(FlaUI.Core.AutomationElements.AutomationElement el, int sibIdx)
                                            {
                                                if (_hoverUiaBoxes.Count >= 500) return;
                                                try
                                                {
                                                    var cr = el.BoundingRectangle;
                                                    if (cr.Width < 5 || cr.Height < 5) return;
                                                    // Skip elements entirely outside root window
                                                    if (cr.X + cr.Width < rootWr.Left || cr.Y + cr.Height < rootWr.Top ||
                                                        cr.X > rootWr.Right + 10 || cr.Y > rootWr.Bottom + 10) return;
                                                    // Skip near-fullsize elements (>90% of root) — wrapper layers
                                                    var rootW2 = rootWr.Right - rootWr.Left;
                                                    var rootH2 = rootWr.Bottom - rootWr.Top;
                                                    if (rootW2 > 0 && rootH2 > 0 && cr.Width > rootW2 * 0.9 && cr.Height > rootH2 * 0.9) return;
                                                    // Clamp to root bounds (elements partially outside → show visible part)
                                                    double ox = cr.X - rootWr.Left, oy = cr.Y - rootWr.Top;
                                                    double ow = cr.Width, oh = cr.Height;
                                                    if (ox < 0) { ow += ox; ox = 0; }
                                                    if (oy < 0) { oh += oy; oy = 0; }
                                                    if (ox + ow > rootW2) ow = rootW2 - ox;
                                                    if (oy + oh > rootH2) oh = rootH2 - oy;
                                                    if (ow < 5 || oh < 5) return;
                                                    string ct = "?", aid = "";
                                                    try { ct = el.ControlType.ToString(); } catch { }
                                                    try { aid = el.AutomationId ?? ""; } catch { }
                                                    var tag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(ct, aid, sibIdx);
                                                    _hoverUiaBoxes.Add(new A11yHackOverlayBox(
                                                        new System.Windows.Rect(ox, oy, ow, oh),
                                                        $"{tag} {(int)cr.Width}x{(int)cr.Height}", null, HackBoxRole.Known));
                                                }
                                                catch { }
                                            }

                                            if (renderAll)
                                            {
                                                // Full recursive scan
                                                void ScanAll(FlaUI.Core.AutomationElements.AutomationElement parent)
                                                {
                                                    if (_hoverUiaBoxes.Count >= 500) return;
                                                    FlaUI.Core.AutomationElements.AutomationElement[] children;
                                                    try { children = parent.FindAllChildren(); } catch { return; }
                                                    for (int ci = 0; ci < children.Length; ci++)
                                                    {
                                                        AddUiaBox(children[ci], ci + 1);
                                                        ScanAll(children[ci]);
                                                    }
                                                }
                                                ScanAll(rootEl);
                                            }
                                            else if (curEl != null)
                                            {
                                                // Default: target chain + prev/next siblings at each level
                                                var walker = uia.TreeWalkerFactory.GetRawViewWalker();
                                                var chain = curEl;
                                                for (int depth = 0; depth < 10 && chain != null; depth++)
                                                {
                                                    var parent = walker.GetParent(chain);
                                                    if (parent == null) break;
                                                    string pCt = "?";
                                                    try { pCt = parent.ControlType.ToString(); } catch { }
                                                    if (pCt == "Window" && depth > 0) break;

                                                    try
                                                    {
                                                        var siblings = parent.FindAllChildren();
                                                        var rid = chain.Properties.RuntimeId.ValueOrDefault;
                                                        int selfIdx = -1;
                                                        if (rid != null)
                                                            for (int si = 0; si < siblings.Length; si++)
                                                            {
                                                                try
                                                                {
                                                                    var sRid = siblings[si].Properties.RuntimeId.ValueOrDefault;
                                                                    if (sRid != null && rid.SequenceEqual(sRid)) { selfIdx = si; break; }
                                                                }
                                                                catch { }
                                                            }
                                                        // Target level: ±10 siblings, upper chain: ±1
                                                        int span = depth == 0 ? 9 : 1;
                                                        for (int si = Math.Max(0, selfIdx - span); si <= Math.Min(siblings.Length - 1, selfIdx + span); si++)
                                                            AddUiaBox(siblings[si], si + 1);
                                                    }
                                                    catch { }
                                                    chain = parent;
                                                }
                                            }
                                        }
                                    }
                                    catch { }

                                    // Save full UIA tree as experience DB (async, non-blocking)
                                    var expRootEl = grapChanged ? uia.FromHandle(rootHwnd) : null;
                                    if (grapChanged && expRootEl != null)
                                    {
                                        var capturedRootEl = expRootEl;
                                        var capturedProc = proc.ToLowerInvariant();
                                        var capturedRootHwnd = rootHwnd;
                                        var capturedRootW = rootW;
                                        var capturedRootH = rootH;
                                        var capturedShortWin = shortWin;
                                        _ = Task.Run(() =>
                                        {
                                            try
                                            {
                                                var profDir = Path.Combine(DataDir, "profiles", $"{capturedProc}_exp");
                                                Directory.CreateDirectory(profDir);
                                                var clsBuf2 = new System.Text.StringBuilder(256);
                                                NativeMethods.GetClassNameW(capturedRootHwnd, clsBuf2, clsBuf2.Capacity);
                                                var winCls = clsBuf2.ToString();
                                                if (string.IsNullOrEmpty(winCls)) winCls = "unknown";
                                                var formPath = Path.Combine(profDir, $"form_uia_{winCls.Replace(" ", "_")}.json");
                                                var ctrls = new List<WKAppBot.Win32.Window.ControlExperience>();
                                                // Full descendant scan (up to 500 elements)
                                                void ScanForExp(FlaUI.Core.AutomationElements.AutomationElement parent)
                                                {
                                                    if (ctrls.Count >= 500) return;
                                                    FlaUI.Core.AutomationElements.AutomationElement[] ch;
                                                    try { ch = parent.FindAllChildren(); } catch { return; }
                                                    foreach (var c in ch)
                                                    {
                                                        if (ctrls.Count >= 500) return;
                                                        try
                                                        {
                                                            var cr = c.BoundingRectangle;
                                                            if (cr.Width < 5 || cr.Height < 5) continue;
                                                            string ct2 = "?", aid2 = "";
                                                            try { ct2 = c.ControlType.ToString(); } catch { }
                                                            try { aid2 = c.AutomationId ?? ""; } catch { }
                                                            var tag2 = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(ct2, aid2);
                                                            NativeMethods.GetWindowRect(capturedRootHwnd, out var wr2);
                                                            ctrls.Add(new WKAppBot.Win32.Window.ControlExperience
                                                            {
                                                                ClassName = tag2,
                                                                Width = (int)cr.Width,
                                                                Height = (int)cr.Height,
                                                                RelativeX = capturedRootW > 0 ? (cr.X - wr2.Left) / (double)capturedRootW : 0,
                                                                RelativeY = capturedRootH > 0 ? (cr.Y - wr2.Top) / (double)capturedRootH : 0,
                                                            });
                                                        }
                                                        catch { }
                                                        ScanForExp(c);
                                                    }
                                                }
                                                ScanForExp(capturedRootEl);
                                                if (ctrls.Count == 0) return;
                                                var form = new WKAppBot.Win32.Window.FormExperience
                                                {
                                                    FormId = $"uia_{winCls}",
                                                    FormName = capturedShortWin,
                                                    UpdatedAt = DateTime.UtcNow,
                                                    ScanCount = 1,
                                                    Controls = ctrls,
                                                };
                                                if (File.Exists(formPath))
                                                {
                                                    try
                                                    {
                                                        var prev = System.Text.Json.JsonSerializer.Deserialize<WKAppBot.Win32.Window.FormExperience>(
                                                            File.ReadAllText(formPath));
                                                        if (prev != null)
                                                        {
                                                            form.LearnedAt = prev.LearnedAt;
                                                            form.ScanCount = prev.ScanCount + 1;
                                                        }
                                                    }
                                                    catch { }
                                                }
                                                File.WriteAllText(formPath,
                                                    System.Text.Json.JsonSerializer.Serialize(form,
                                                        new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
                                            }
                                            catch { }
                                        });
                                    }
                                }
                                boxes.AddRange(_hoverUiaBoxes);

                                // ── Keyboard focus chain overlay ──
                                try
                                {
                                    var focusEl = uia.FocusedElement();
                                    if (focusEl != null)
                                    {
                                        int focusBoxCount = 0;
                                        var walker2 = uia.TreeWalkerFactory.GetRawViewWalker();
                                        var fNode = focusEl;
                                        for (int fd = 0; fd < 10 && fNode != null; fd++)
                                        {
                                            try
                                            {
                                                var fr = fNode.BoundingRectangle;
                                                if (fr.Width >= 5 && fr.Height >= 5)
                                                {
                                                    var rootW3 = rootWr.Right - rootWr.Left;
                                                    var rootH3 = rootWr.Bottom - rootWr.Top;
                                                    if (!(rootW3 > 0 && rootH3 > 0 && fr.Width > rootW3 * 0.9 && fr.Height > rootH3 * 0.9))
                                                    {
                                                        double fx2 = fr.X - rootWr.Left, fy2 = fr.Y - rootWr.Top;
                                                        double fw2 = fr.Width, fh2 = fr.Height;
                                                        if (fx2 < 0) { fw2 += fx2; fx2 = 0; }
                                                        if (fy2 < 0) { fh2 += fy2; fy2 = 0; }
                                                        if (fx2 + fw2 > rootW3) fw2 = rootW3 - fx2;
                                                        if (fy2 + fh2 > rootH3) fh2 = rootH3 - fy2;
                                                        if (fw2 >= 5 && fh2 >= 5)
                                                        {
                                                            string fCt = "?", fAid = "";
                                                            try { fCt = fNode.ControlType.ToString(); } catch { }
                                                            try { fAid = fNode.AutomationId ?? ""; } catch { }
                                                            var fTag = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(fCt, fAid, fd);
                                                            var role = fd == 0 ? HackBoxRole.Focus : HackBoxRole.FocusChain;
                                                            boxes.Add(new A11yHackOverlayBox(
                                                                new System.Windows.Rect(fx2, fy2, fw2, fh2),
                                                                fd == 0 ? $"KB:{fTag}" : fTag, null, role));
                                                            focusBoxCount++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch { }
                                            try { fNode = walker2.GetParent(fNode); } catch { fNode = null; }
                                            if (fNode != null)
                                            {
                                                string pCt2 = "?";
                                                try { pCt2 = fNode.ControlType.ToString(); } catch { }
                                                if (pCt2 == "Window") break;
                                            }
                                        }
                                        if (focusBoxCount == 0 && !RunningInEye)
                                        {
                                            string fDbg = "?";
                                            try { fDbg = $"{focusEl.ControlType}|{focusEl.Name}|{focusEl.BoundingRectangle}"; } catch { }
                                            Log($"[FOCUS-DBG] 0 boxes — el={fDbg} rootWr={rootWr.Left},{rootWr.Top},{rootWr.Right},{rootWr.Bottom}");
                                        }
                                    }
                                }
                                catch (Exception fex) { if (!RunningInEye) Log($"[FOCUS-DBG] ex={fex.Message}"); }

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
                                // Filter: ±9 Cached entries closest to target box (Y-sorted index)
                                if (_hoverExpBoxes.Count > 0)
                                {
                                    var targetBox = boxes.FirstOrDefault(b => b.Role == HackBoxRole.Target);
                                    if (targetBox != null)
                                    {
                                        var sorted = _hoverExpBoxes
                                            .OrderBy(b => b.Bounds.Y).ThenBy(b => b.Bounds.X).ToList();
                                        var tcx = targetBox.Bounds.X + targetBox.Bounds.Width / 2;
                                        var tcy = targetBox.Bounds.Y + targetBox.Bounds.Height / 2;
                                        int bestIdx = 0; double bestDist = double.MaxValue;
                                        for (int i = 0; i < sorted.Count; i++)
                                        {
                                            var b = sorted[i];
                                            // Y-weighted distance: vertical proximity matters most
                                            var d = Math.Abs(b.Bounds.Y + b.Bounds.Height / 2 - tcy)
                                                  + Math.Abs(b.Bounds.X + b.Bounds.Width / 2 - tcx) * 0.1;
                                            if (d < bestDist) { bestDist = d; bestIdx = i; }
                                        }
                                        const int ExpSpan = 9;
                                        for (int i = Math.Max(0, bestIdx - ExpSpan);
                                             i <= Math.Min(sorted.Count - 1, bestIdx + ExpSpan); i++)
                                            boxes.Add(sorted[i]);
                                    }
                                }

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
                    var logLine = $"[{ts2}] {shortWin}#{tagPath} [{mark}] {elMs}ms";
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
