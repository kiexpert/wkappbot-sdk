// AppBotEyeHoverAnalyzer.cs — Mouse CCA Live Analysis (화면 MD 변환기)
// Background worker in Eye: 1s interval → mouse pos → UIA + CCA → Slack thread reply.

using System.Text;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Mouse CCA live analysis (1s interval, change-based Slack thread reply) ──
    static string? _mouseCcaReplyTs;            // Slack thread ts for CCA result
    static string _lastMouseCcaResult = "";      // change detection

    /// <summary>
    /// Start mouse CCA live analysis worker.
    /// Every 1s: mouse pos → UIA element → parent → CCA → Slack thread reply (change-based).
    /// </summary>
    internal static void StartMouseCcaWorker(CancellationToken ct) =>
        Task.Run(() => MouseCcaLoop(ct), ct);

    static async Task MouseCcaLoop(CancellationToken ct)
    {
        Console.WriteLine("[MOUSE-CCA] Live analysis worker started (1s interval)");
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, ct);

                NativeMethods.GetCursorPos(out var pt);
                var hwnd = NativeMethods.WindowFromPoint(pt);
                if (hwnd == IntPtr.Zero) continue;

                // Skip our own Eye/ScreenSaver windows
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                if ((int)pid == Environment.ProcessId) continue;

                // ── Analysis ──
                string ccaResult;
                try
                {
                    var titleBuf = new StringBuilder(128);
                    NativeMethods.GetWindowTextW(hwnd, titleBuf, titleBuf.Capacity);
                    var winTitle = titleBuf.ToString();
                    // Chrome_RenderWidgetHostHWND has no title → walk up to parent with title
                    if (string.IsNullOrEmpty(winTitle) || winTitle == "Chrome Legacy Window")
                    {
                        var parentHwnd = NativeMethods.GetParent(hwnd);
                        if (parentHwnd == IntPtr.Zero) parentHwnd = NativeMethods.GetAncestor(hwnd, 2); // GA_ROOT
                        if (parentHwnd != IntPtr.Zero)
                        {
                            var buf2 = new StringBuilder(256);
                            NativeMethods.GetWindowTextW(parentHwnd, buf2, buf2.Capacity);
                            if (buf2.Length > 0) winTitle = buf2.ToString();
                        }
                    }
                    var winTitleShort = winTitle.Length > 40 ? winTitle[..40] + "…" : winTitle;

                    // Step 1: UIA — kept alive until Step 4 (parent needs live COM)
                    string elName = "", elType = "?", elAid = "";
                    using var uiaSession = new UiaLocator();
                    FlaUI.Core.AutomationElements.AutomationElement? parentEl = null;
                    try
                    {
                        var (element, info) = uiaSession.GetElementAndInfoAtPoint(pt.X, pt.Y);
                        elName = info?.Name ?? "";
                        elType = info?.ControlType ?? "?";
                        elAid = info?.AutomationId ?? "";
                        if (element != null)
                        {
                            try { parentEl = element.Parent; } catch { }
                            parentEl ??= element;
                        }
                    }
                    catch { }
                    if (elName.Length > 30) elName = elName[..30] + "…";

                    // Step 2: Screenshot — parent rect → window rect fallback → mouse-centered crop
                    int x = 0, y = 0, w = 0, h = 0;
                    bool gotRect = false;
                    if (parentEl != null)
                    {
                        try
                        {
                            var pr = parentEl.BoundingRectangle;
                            if (pr.Width > 10 && pr.Height > 10)
                            { x = pr.X; y = pr.Y; w = pr.Width; h = pr.Height; gotRect = true; }
                        }
                        catch { }
                    }
                    if (!gotRect)
                    {
                        // Fallback: window rect + mouse-centered crop
                        NativeMethods.GetWindowRect(hwnd, out var wr2);
                        x = wr2.Left; y = wr2.Top; w = wr2.Right - wr2.Left; h = wr2.Bottom - wr2.Top;
                        if (w > 800 || h > 600)
                        {
                            int cx = pt.X - 300, cy = pt.Y - 200;
                            if (cx < x) cx = x; if (cy < y) cy = y;
                            w = Math.Min(600, wr2.Right - cx); h = Math.Min(400, wr2.Bottom - cy);
                            x = cx; y = cy;
                        }
                    }
                    if (w > 1200) w = 1200;
                    if (h > 800) h = 800;
                    if (x < 0) { w += x; x = 0; }
                    if (y < 0) { h += y; y = 0; }
                    if (w < 10 || h < 10) continue;
                    Console.Error.WriteLine($"[MOUSE-CCA] capture: ({x},{y} {w}x{h}) parent={gotRect} el=[{elType}] \"{elName}\"");

                    using var bmp = new System.Drawing.Bitmap(w, h);
                    using (var g = System.Drawing.Graphics.FromImage(bmp))
                        g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(w, h));

                    // Step 3: CCA analysis + dyn_id from mouse-position container
                    string tableInfo = "", dynId = "";
                    int textCnt = 0, iconCnt = 0, sepCnt = 0, contCnt = 0, totalSeg = 0;
                    try
                    {
                        var cca = new WKAppBot.Vision.ConnectedComponentAnalyzer();
                        var regions = cca.Analyze(bmp);
                        totalSeg = regions.Count;
                        textCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Text);
                        iconCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Icon);
                        sepCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Separator);
                        contCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Container);
                        var table = cca.DetectTable(regions, w, h);
                        tableInfo = table != null ? $" tbl={table.Rows}x{table.Cols}" : "";

                        // Find container region at mouse position (relative to capture area)
                        int mx = pt.X - x, my = pt.Y - y;
                        var containers = regions
                            .Where(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Container)
                            .Where(r => r.Bounds.Contains(mx, my))
                            .OrderBy(r => r.Bounds.Width * r.Bounds.Height) // smallest containing
                            .ToList();
                        if (containers.Count > 0)
                        {
                            var c = containers[0];
                            // Assign row/col based on sorted position among all containers
                            var allConts = regions
                                .Where(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.Container)
                                .OrderBy(r => r.Bounds.Y).ThenBy(r => r.Bounds.X).ToList();
                            int idx = allConts.IndexOf(c);
                            int row = 0, col = 0;
                            if (idx >= 0)
                            {
                                // Simple grid: group by Y proximity
                                int curRow = 0;
                                for (int ci = 0; ci <= idx; ci++)
                                {
                                    if (ci > 0 && Math.Abs(allConts[ci].Bounds.Y - allConts[ci - 1].Bounds.Y) > 8)
                                    { curRow++; col = 0; }
                                    else if (ci > 0) col++;
                                    row = curRow;
                                }
                            }
                            dynId = WKAppBot.Vision.DynamicA11yAnalyzer.GenerateDynId(row, col, c.Bounds.Width, c.Bounds.Height);
                        }
                    }
                    catch { }

                    // Step 3.5: Fused Matcher (CCA↔UIA spatial matching)
                    string fusedSummary = "";
                    try
                    {
                        if (parentEl != null && totalSeg > 0)
                        {
                            // Collect UIA leaf elements with bounds (relative to capture area)
                            var uiaInfos = new List<WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo>();
                            try
                            {
                                var leaves = new List<(string text, int x, int y, int w2, int h2, int depth)>();
                                UiaLocator.CollectTextLeaves(parentEl, leaves, 0, 8);
                                foreach (var leaf in leaves)
                                {
                                    uiaInfos.Add(new WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo
                                    {
                                        Name = leaf.text,
                                        Bounds = new System.Drawing.Rectangle(leaf.x - x, leaf.y - y, leaf.w2, leaf.h2)
                                    });
                                }
                            }
                            catch { }

                            if (uiaInfos.Count > 0)
                            {
                                var cca2 = new WKAppBot.Vision.ConnectedComponentAnalyzer();
                                var regions2 = cca2.Analyze(bmp);
                                var matchResults = WKAppBot.Vision.CcaUiaFusedMatcher.Match(regions2, uiaInfos);
                                fusedSummary = WKAppBot.Vision.CcaUiaFusedMatcher.Summarize(matchResults);

                                // Save to Experience DB
                                NativeMethods.GetWindowThreadProcessId(hwnd, out uint procId);
                                string procName2;
                                try { procName2 = System.Diagnostics.Process.GetProcessById((int)procId).ProcessName; } catch { procName2 = "unknown"; }
                                var clsBuf = new StringBuilder(64);
                                NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
                                WKAppBot.Vision.CcaUiaFusedMatcher.SaveToExperienceDb(
                                    Path.Combine(DataDir, "experience"), procName2, clsBuf.ToString(), matchResults);
                            }
                        }
                    }
                    catch { }

                    // Step 4: Visual markdown render (parent scope — walk up if empty)
                    string visualMd = "";
                    try
                    {
                        var scanEl = parentEl;
                        // Try parent, then grandparent, up to 3 levels — stop when we get content
                        for (int up = 0; up < 3 && scanEl != null; up++)
                        {
                            visualMd = UiaLocator.RenderVisualMarkdown(scanEl, maxDepth: 8);
                            if (!string.IsNullOrEmpty(visualMd) && visualMd != "_(no text)_") break;
                            try { scanEl = scanEl.Parent; } catch { break; }
                        }
                    }
                    catch { }

                    // Build grap path — prefer dynId > aid > name > controlType
                    var grapScope = !string.IsNullOrEmpty(dynId) ? dynId
                        : !string.IsNullOrEmpty(elAid) ? elAid
                        : !string.IsNullOrEmpty(elName) ? $"*{(elName.Length > 20 ? elName[..20] : elName)}*"
                        : elType != "?" ? $"[{elType}]" : "";
                    var grapWin = !string.IsNullOrEmpty(winTitle) && winTitle.Length > 20
                        ? $"*{winTitle[..20]}*" : !string.IsNullOrEmpty(winTitle) ? $"*{winTitle}*" : "*unknown*";
                    var grapPath = !string.IsNullOrEmpty(grapScope) ? $"{grapWin}#{grapScope}" : grapWin;

                    // Header (for Slack message edit)
                    var header = new StringBuilder();
                    header.AppendLine($"🔍 **grap**: `{grapPath}`");
                    header.AppendLine($"**pos**: ({pt.X},{pt.Y}) | **win**: _{winTitleShort}_ ({w}x{h})");
                    header.AppendLine($"**UIA**: `[{elType}]` \"{elName}\"");
                    header.AppendLine($"**CCA**: {totalSeg} seg — T={textCnt} I={iconCnt} S={sepCnt} C={contCnt}{tableInfo}");
                    if (!string.IsNullOrEmpty(fusedSummary))
                        header.AppendLine($"**match**: {fusedSummary}");

                    // Build snippet filename from UIA node path (not grap)
                    var snippetFileName = $"{winTitleShort}_{elType}_{elName}";
                    if (!string.IsNullOrEmpty(dynId)) snippetFileName += $"_{dynId}";

                    // Combine: header for message, visualMd for snippet, snippetFileName for .md name
                    ccaResult = $"__GRAP__{snippetFileName}__GRAP__\n__TREE__{visualMd ?? ""}__TREE__\n{header.ToString().TrimEnd()}";
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[MOUSE-CCA] {ex.GetType().Name}: {ex.Message}");
                    continue;
                }

                // Change detection: only post if result differs
                // Strip grap tag for comparison (grap changes with mouse pos but content may be same)
                var compareResult = ccaResult.Contains("__GRAP__")
                    ? ccaResult[(ccaResult.LastIndexOf("__GRAP__") + 8)..] : ccaResult;
                if (compareResult == _lastMouseCcaResult) continue;
                _lastMouseCcaResult = compareResult;

                // Extract snippet name from grap tag, then remove tag from content
                var snippetName = "mouse-cca";
                if (ccaResult.StartsWith("__GRAP__"))
                {
                    var end = ccaResult.IndexOf("__GRAP__", 8);
                    if (end > 8) snippetName = ccaResult[8..end];
                    ccaResult = ccaResult[(end + 9)..]; // skip tag + newline
                }

                await UpdateMouseCcaSlack(ccaResult, snippetName);

                Console.WriteLine($"[MOUSE-CCA] {ccaResult.Split('\n')[0]}");
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[MOUSE-CCA] Error: {ex.Message}"); }
        }
        Console.WriteLine("[MOUSE-CCA] Worker stopped");
    }

    // ── Keyboard Focus Chain analysis (separate Slack thread reply) ──
    static string? _focusChainReplyTs;
    static string _lastFocusChainResult = "";

    /// <summary>
    /// Start keyboard focus chain analysis worker.
    /// Every 1s: UIA focused element → parent chain → root window → Slack thread reply.
    /// </summary>
    internal static void StartFocusChainWorker(CancellationToken ct) =>
        Task.Run(() => FocusChainLoop(ct), ct);

    static async Task FocusChainLoop(CancellationToken ct)
    {
        Console.WriteLine("[FOCUS-CHAIN] Keyboard focus analysis worker started (1s interval)");
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(1000, ct);

                string chainResult;
                try
                {
                    using var uia = new UiaLocator();
                    var focused = uia.GetFocusedElementInfo();
                    if (focused == null) continue;

                    // Skip our own process
                    if (focused.ProcessId == Environment.ProcessId) continue;

                    // Build grap path for focused element
                    var winTitle = focused.WindowTitle ?? "";
                    var winShort = winTitle.Length > 30 ? winTitle[..30] + "…" : winTitle;
                    var grapWin = winTitle.Length > 20 ? $"*{winTitle[..20]}*" : $"*{winTitle}*";
                    var grapScope = !string.IsNullOrEmpty(focused.AutomationId)
                        ? focused.AutomationId
                        : !string.IsNullOrEmpty(focused.Name)
                            ? $"*{(focused.Name.Length > 20 ? focused.Name[..20] : focused.Name)}*"
                            : "";
                    var grapPath = !string.IsNullOrEmpty(grapScope)
                        ? $"{grapWin}#{grapScope}" : grapWin;

                    // Build chain display (focused → parents → root)
                    var sb = new StringBuilder();
                    sb.AppendLine($"⌨️ **grap**: `{grapPath}`");
                    sb.AppendLine($"**focus**: `[{focused.ControlType}]` \"{focused.Name}\"");
                    if (!string.IsNullOrEmpty(focused.Value))
                        sb.AppendLine($"**value**: \"{(focused.Value.Length > 50 ? focused.Value[..50] + "…" : focused.Value)}\"");
                    sb.AppendLine($"**win**: _{winShort}_");

                    // Parent chain
                    if (focused.ParentChain?.Count > 0)
                    {
                        sb.AppendLine("**chain**:");
                        foreach (var p in focused.ParentChain)
                            sb.AppendLine($"  └ `[{p.type}]` {p.name}");
                    }

                    // Patterns
                    if (focused.Patterns?.Count > 0)
                        sb.AppendLine($"**patterns**: {string.Join(", ", focused.Patterns)}");

                    chainResult = sb.ToString().TrimEnd();
                }
                catch { continue; }

                // Change detection
                if (chainResult == _lastFocusChainResult) continue;
                _lastFocusChainResult = chainResult;

                // Post/edit as simple message (no file upload)
                if (_eyeStatusTs != null && !string.IsNullOrEmpty(_eyeBotToken) && !string.IsNullOrEmpty(_eyeChannel))
                {
                    try
                    {
                        if (_focusChainReplyTs != null)
                            await SlackUpdateMessageAsync(_eyeBotToken, _eyeChannel, _focusChainReplyTs, chainResult);
                        else
                        {
                            var (ok2, ts2) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, chainResult,
                                threadTs: _eyeStatusTs, username: "앱봇아이");
                            if (ok2 && ts2 != null) _focusChainReplyTs = ts2;
                        }
                    }
                    catch { }
                }

                Console.WriteLine($"[FOCUS-CHAIN] {chainResult.Split('\n')[0]}");
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex) { Console.WriteLine($"[FOCUS-CHAIN] Error: {ex.Message}"); }
        }
        Console.WriteLine("[FOCUS-CHAIN] Worker stopped");
    }

    // Track uploaded snippet message ts for delete→reupload cycle
    static string? _mouseCcaSnippetMsgTs;
    static string? _focusChainSnippetMsgTs;

    /// <summary>Post/edit mouse CCA result as single message: header + code block tree.</summary>
    static async Task UpdateMouseCcaSlack(string text, string snippetName = "mouse-cca")
    {
        if (_eyeStatusTs == null || string.IsNullOrEmpty(_eyeBotToken) || string.IsNullOrEmpty(_eyeChannel))
            return;

        // Extract tree from __TREE__ tags → append as code block
        string treeContent = "";
        var msgText = text;
        if (text.Contains("__TREE__"))
        {
            var start = text.IndexOf("__TREE__") + 8;
            var end = text.IndexOf("__TREE__", start);
            if (end > start) treeContent = text[start..end];
            msgText = text[(end + 8)..].TrimStart('\n');
        }

        // Combine: header + code block (tree content)
        if (!string.IsNullOrEmpty(treeContent) && treeContent.Trim().Length > 0)
            msgText += $"\n```\n{treeContent}\n```";
        else
            msgText += "\n_(no tree)_";

        // Truncate to Slack limit (4000 chars)
        if (msgText.Length > 3900) msgText = msgText[..3900] + "\n…";

        try
        {
            if (_mouseCcaReplyTs != null)
                await SlackUpdateMessageAsync(_eyeBotToken, _eyeChannel, _mouseCcaReplyTs, msgText);
            else
            {
                var (ok, ts) = await SlackSendViaApi(_eyeBotToken, _eyeChannel, msgText,
                    threadTs: _eyeStatusTs, username: "앱봇아이");
                if (ok && ts != null) _mouseCcaReplyTs = ts;
            }
        }
        catch { /* best effort */ }
    }
}
