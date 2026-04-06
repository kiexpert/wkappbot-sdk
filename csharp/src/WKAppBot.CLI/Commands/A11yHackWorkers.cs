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
        IntPtr lastRootHwnd = IntPtr.Zero;
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

                // Root window: for UIA tree access and proc name resolution
                var rootHwndEarly = NativeMethods.GetAncestor(hwnd, 2 /* GA_ROOT */);
                if (rootHwndEarly == IntPtr.Zero) rootHwndEarly = hwnd;

                // Recursively drill to deepest child at cursor (RealChildWindowFromPoint is NOT recursive)
                hwnd = DrillToDeepestChild(rootHwndEarly, pt);

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

                // Direct UIA: find element at mouse position via shared BuildElementAtPointInfo
                var sw = System.Diagnostics.Stopwatch.StartNew();
                string elType = "?", elName = "", elAid = "", elPatterns = "";
                System.Drawing.Rectangle elBounds = default;
                NativeMethods.GetWindowThreadProcessId(rootHwndEarly, out uint pid);
                string proc; try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { proc = "?"; }
                FlaUI.Core.AutomationElements.AutomationElement? curEl = null;
                try
                {
                    curEl = uia.FromPoint(new System.Drawing.Point(pt.X, pt.Y));
                    if (curEl != null)
                    {
                        var info = WKAppBot.Win32.Accessibility.UiaLocator.BuildElementAtPointInfo(curEl);
                        elType = info.ControlType;
                        elName = info.Name == "(null)" || info.Name == "(err)" ? "" : info.Name;
                        elAid  = info.AutomationId;
                        elBounds = new System.Drawing.Rectangle(info.BoundsX, info.BoundsY, info.BoundsW, info.BoundsH);
                        elPatterns = string.Join(",", info.Patterns);
                    }
                }
                catch { }

                // Best child hwnd: UIA NativeWindowHandle > WindowFromPoint child > root
                // UIA element knows its exact backing Win32 window (WPF/Electron host, not generic parent)
                var bestHwnd = hwnd;
                if (curEl != null)
                {
                    try
                    {
                        var nwh = new IntPtr(curEl.Properties.NativeWindowHandle.ValueOrDefault);
                        if (nwh != IntPtr.Zero && nwh != rootHwndEarly)
                            bestHwnd = nwh; // most specific: the actual Win32 host of this UIA element
                    }
                    catch { }
                }

                if (elName.Length > 30) elName = elName[..30];
                var elLabel = !string.IsNullOrEmpty(elAid) ? elAid : elName;
                // Window grap: title from bestHwnd (has title), fallback to root
                var winTitle = NativeMethods.GetWindowTextW(bestHwnd);
                if (string.IsNullOrEmpty(winTitle)) winTitle = NativeMethods.GetWindowTextW(rootHwndEarly);
                if (winTitle.Length > 20) winTitle = winTitle[..20];
                var winGrap = $"\"*{winTitle}*\"";
                // Full grap with #scope for targeting
                var fullGrap = !string.IsNullOrEmpty(elLabel) ? $"{winGrap}#*{elLabel}*" : winGrap;

                // Verify: bestHwnd first, fallback root
                var json5 = WKAppBot.Win32.Window.WindowFinder.BuildTargetJson5(bestHwnd);
                var verifyHits = WKAppBot.Win32.Window.WindowFinder.FindByTitle(json5, true);
                bool verified = verifyHits.Any(v => v.Handle == bestHwnd || v.Handle == rootHwndEarly);
                var mark = verified ? "OK" : "MISS";

                // Build tag path: walk parent chain → "Pane/Group_1th/Edit_1052"
                // Absolute tag path via shared helper
                var tagPath = curEl != null
                    ? WKAppBot.Win32.Accessibility.GrapHelper.BuildAbsoluteTagPath(curEl, uia.TreeWalkerFactory.GetRawViewWalker())
                    : WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(elType, elAid);

                // '?' tagPath = UIA completely blind → look up experience DB first, then coord fallback
                if (tagPath == "?")
                {
                    var expTag = TryLookupExpDbTag(rootHwndEarly, proc, pt, elBounds);
                    if (expTag != null)
                        tagPath = expTag;
                    else
                    {
                        int nx = elBounds.Width > 0 ? elBounds.X + elBounds.Width / 2 : pt.X;
                        int ny = elBounds.Height > 0 ? elBounds.Y + elBounds.Height / 2 : pt.Y;
                        tagPath = WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(nx, ny);
                    }
                }

                var clsBuf2 = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(bestHwnd, clsBuf2, clsBuf2.Capacity);
                var winCls2 = clsBuf2.ToString();
                // WPF: "HwndWrapper[DefaultDomain;;GUID]" → "HwndWrapper[WPF]" (GUID changes every run)
                if (winCls2.StartsWith("HwndWrapper[", StringComparison.Ordinal))
                    winCls2 = "HwndWrapper[WPF]";
                var shortWin = $"{{hwnd:0x{bestHwnd.ToInt64():X},proc:'{proc}',cls:'{winCls2}'}}";


                var result = $"{shortWin}#{tagPath} {mark}";

                var elMs = sw.ElapsedMilliseconds;

                bool changed = result != lastResult;
                if (changed) lastResult = result;
                // grapChanged = root window switched (different app) — bestHwnd alone stays same inside Electron/WPF
                bool grapChanged = rootHwndEarly != lastRootHwnd;
                lastRootHwnd = rootHwndEarly;
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
                        // Root window rect (reuse rootHwndEarly computed above)
                        var rootHwnd = rootHwndEarly;
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
                                                    NativeMethods.GetWindowRect(capturedRootHwnd, out var wr2);
                                                    for (int ci = 0; ci < ch.Length; ci++)
                                                    {
                                                        if (ctrls.Count >= 500) return;
                                                        try
                                                        {
                                                            var cr = ch[ci].BoundingRectangle;
                                                            if (cr.Width < 5 || cr.Height < 5) continue;
                                                            string ct2 = "Node", aid2 = ""; // "Node" → DyNode if type unreadable
                                                            try { ct2 = ch[ci].ControlType.ToString(); } catch { }
                                                            try { aid2 = ch[ci].AutomationId ?? ""; } catch { }
                                                            ctrls.Add(new WKAppBot.Win32.Window.ControlExperience
                                                            {
                                                                ClassName = ct2,
                                                                Role = aid2,        // AutomationId (AID takes priority over sibling index in tag)
                                                                ControlId = ci + 1, // sibling index (1-based, fallback when no AID)
                                                                Width = (int)cr.Width,
                                                                Height = (int)cr.Height,
                                                                RelativeX = capturedRootW > 0 ? (cr.X - wr2.Left) / (double)capturedRootW : 0,
                                                                RelativeY = capturedRootH > 0 ? (cr.Y - wr2.Top) / (double)capturedRootH : 0,
                                                            });
                                                        }
                                                        catch { }
                                                        ScanForExp(ch[ci]);
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
                                                            ctrl.ClassName ?? "Node", ctrl.Role, ctrl.ControlId, isDynamic: true);
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
                                // Hybrid: Cached(경험DB) + Known(UIA) — UIA가 커버하는 영역은 Cached 억제
                                // UIA-blind 영역만 Cached로 채움 (DyGroup_3th 등)
                                if (_hoverExpBoxes.Count > 0)
                                {
                                    var targetBox = boxes.FirstOrDefault(b => b.Role == HackBoxRole.Target);
                                    if (targetBox != null)
                                    {
                                        // ±9 filter: Y-sorted, closest to target
                                        var sorted = _hoverExpBoxes
                                            .OrderBy(b => b.Bounds.Y).ThenBy(b => b.Bounds.X).ToList();
                                        var tcx = targetBox.Bounds.X + targetBox.Bounds.Width / 2;
                                        var tcy = targetBox.Bounds.Y + targetBox.Bounds.Height / 2;
                                        int bestIdx = 0; double bestDist = double.MaxValue;
                                        for (int i = 0; i < sorted.Count; i++)
                                        {
                                            var b = sorted[i];
                                            var d = Math.Abs(b.Bounds.Y + b.Bounds.Height / 2 - tcy)
                                                  + Math.Abs(b.Bounds.X + b.Bounds.Width / 2 - tcx) * 0.1;
                                            if (d < bestDist) { bestDist = d; bestIdx = i; }
                                        }
                                        // Known 박스 목록 (UIA 커버 영역 판정용)
                                        var knownBoxes = boxes.Where(b =>
                                            b.Role is HackBoxRole.Known or HackBoxRole.Target or HackBoxRole.Scope).ToList();
                                        const int ExpSpan = 9;
                                        for (int i = Math.Max(0, bestIdx - ExpSpan);
                                             i <= Math.Min(sorted.Count - 1, bestIdx + ExpSpan); i++)
                                        {
                                            var cb = sorted[i];
                                            // UIA가 이미 커버하면 억제 — Cached 중심점이 Known 박스 안에 있으면 스킵
                                            var ccx = cb.Bounds.X + cb.Bounds.Width / 2;
                                            var ccy = cb.Bounds.Y + cb.Bounds.Height / 2;
                                            bool covered = knownBoxes.Any(k =>
                                                ccx >= k.Bounds.X && ccx <= k.Bounds.X + k.Bounds.Width &&
                                                ccy >= k.Bounds.Y && ccy <= k.Bounds.Y + k.Bounds.Height);
                                            if (!covered) boxes.Add(cb);
                                        }
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
                // tagPath containing '@' = was '?' (coord-temp name) = fully UIA-blind
                bool uiaBlind = (string.IsNullOrEmpty(elLabel) && string.IsNullOrEmpty(elPatterns))
                    || tagPath.StartsWith("FindNodeXY(");
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

    /// <summary>
    /// Look up experience DB (form_uia_{winClass}.json) for the node closest to cursor/bounds.
    /// Returns DyTag string ("DyEdit_active", "DyGroup_3th", ...) or null if no match.
    /// </summary>
    /// <summary>
    /// Recursively drill from parent down to the deepest visible child window containing the screen point.
    /// RealChildWindowFromPoint is NOT recursive — this calls it repeatedly until no deeper child exists.
    /// </summary>
    static IntPtr DrillToDeepestChild(IntPtr parent, POINT screenPt)
    {
        var current = parent;
        for (int depth = 0; depth < 16; depth++)
        {
            var clientPt = screenPt;
            NativeMethods.ScreenToClient(current, ref clientPt);
            var child = NativeMethods.RealChildWindowFromPoint(current, clientPt);
            if (child == IntPtr.Zero || child == current) break;
            current = child;
        }
        return current;
    }

    static string? TryLookupExpDbTag(IntPtr rootHwnd, string proc, POINT pt, System.Drawing.Rectangle elBounds)
    {
        try
        {
            var clsBuf = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(rootHwnd, clsBuf, clsBuf.Capacity);
            var winCls = clsBuf.ToString();
            if (string.IsNullOrEmpty(winCls)) return null;

            var formPath = Path.Combine(DataDir, "profiles", $"{proc.ToLowerInvariant()}_exp",
                $"form_uia_{winCls.Replace(" ", "_")}.json");
            if (!File.Exists(formPath)) return null;

            var form = System.Text.Json.JsonSerializer.Deserialize<WKAppBot.Win32.Window.FormExperience>(
                File.ReadAllText(formPath));
            if (form == null || form.Controls.Count == 0) return null;

            NativeMethods.GetWindowRect(rootHwnd, out var wr);
            int winW = wr.Right - wr.Left, winH = wr.Bottom - wr.Top;
            if (winW <= 0 || winH <= 0) return null;

            // Target center: prefer UIA bounds, fall back to cursor
            int tx = elBounds.Width > 0 ? elBounds.X + elBounds.Width / 2 : pt.X;
            int ty = elBounds.Height > 0 ? elBounds.Y + elBounds.Height / 2 : pt.Y;

            WKAppBot.Win32.Window.ControlExperience? best = null;
            double bestDist = double.MaxValue;
            foreach (var ctrl in form.Controls)
            {
                int cx = wr.Left + (int)(ctrl.RelativeX * winW) + ctrl.Width / 2;
                int cy = wr.Top  + (int)(ctrl.RelativeY * winH) + ctrl.Height / 2;
                double dist = Math.Sqrt((cx - tx) * (cx - tx) + (cy - ty) * (cy - ty));
                if (dist < bestDist) { bestDist = dist; best = ctrl; }
            }

            // Accept match within 40px
            if (best == null || bestDist > 40) return null;

            return WKAppBot.Win32.Accessibility.GrapHelper.FormatNodeTag(
                best.ClassName ?? "Node", best.Role, best.ControlId, isDynamic: true);
        }
        catch { return null; }
    }
}
