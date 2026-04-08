// AnalyzeHackCommand.cs — One-shot CCA+UIA analysis (separate process for memory isolation)
// Usage: wkappbot analyze-hack <hwnd-hex> <x> <y> [w] [h]
// Output: JSON to stdout with fusedSummary, visualMd, dynId, grapPath
// Process exits after analysis → memory freed instantly.

using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int AnalyzeHackCommand(string[] args)
    {
        // Server mode: stdin JSON loop, idle 5min auto-exit
        if (args.Length >= 1 && args[0] == "--server")
            return AnalyzeHackServer();

        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: wkappbot analyze-hack <hwnd-hex> <x> <y> [w] [h]\n       wkappbot analyze-hack --server");
            return 1;
        }

        IntPtr hwnd;
        try { hwnd = new IntPtr(Convert.ToInt64(args[0], 16)); }
        catch { Console.Error.WriteLine($"Invalid hwnd: {args[0]}"); return 1; }

        if (!int.TryParse(args[1], out int px) || !int.TryParse(args[2], out int py))
        { Console.Error.WriteLine("Invalid x/y"); return 1; }

        hwnd = ResolveFrontmostWindowForPoint(hwnd, px, py);
        if (hwnd == IntPtr.Zero)
        {
            Console.Error.WriteLine("No frontmost window at point");
            return 1;
        }

        NativeMethods.GetWindowRect(hwnd, out var wr);
        int cx = wr.Left, cy = wr.Top;
        int cw = wr.Right - wr.Left, ch = wr.Bottom - wr.Top;

        var result = new JsonObject();

        // UIA element at cursor → parent = "family" scope for capture
        string elName = "", elType = "?", elAid = "";
        FlaUI.Core.AutomationElements.AutomationElement? parentEl = null;
        try
        {
            using var uia = new UiaLocator();
            var (element, info) = uia.GetElementAndInfoContainingRectInWindow(px, py, 1, 1, hwnd);
            elName = info?.Name ?? "";
            elType = info?.ControlType ?? "?";
            elAid = info?.AutomationId ?? "";
            if (element != null)
            {
                try { parentEl = element.Parent; } catch { }
                parentEl ??= element;
            }

            // Use parent's BoundingRect as capture area (family-scoped analysis)
            // If parent is too small (< 50px either axis, likely single child), walk up
            if (parentEl != null)
            {
                try
                {
                    var scanScope = parentEl;
                    for (int up = 0; up < 3; up++)
                    {
                        var pb = scanScope.Properties.BoundingRectangle.ValueOrDefault;
                        if (pb.Width >= 50 && pb.Height >= 30)
                        {
                            cx = pb.X; cy = pb.Y; cw = pb.Width; ch = pb.Height;
                            if (cx < wr.Left) { cw -= (wr.Left - cx); cx = wr.Left; }
                            if (cy < wr.Top) { ch -= (wr.Top - cy); cy = wr.Top; }
                            if (cx + cw > wr.Right) cw = wr.Right - cx;
                            if (cy + ch > wr.Bottom) ch = wr.Bottom - cy;
                            break;
                        }
                        // Too small — go up one level
                        try { scanScope = scanScope.Parent; } catch { break; }
                        if (scanScope == null) break;
                    }
                }
                catch { /* fallback to window rect */ }
            }

            // Override with explicit w/h if provided
            if (args.Length >= 5 && int.TryParse(args[3], out int aw) && int.TryParse(args[4], out int ah))
            { cx = wr.Left; cy = wr.Top; cw = aw; ch = ah; }

            // Fallback: if still too large (no UIA parent found), crop around cursor
            if (cw > 800 || ch > 600)
            {
                cx = px - 300; cy = py - 200;
                if (cx < wr.Left) cx = wr.Left;
                if (cy < wr.Top) cy = wr.Top;
                cw = Math.Min(600, wr.Right - cx);
                ch = Math.Min(400, wr.Bottom - cy);
            }
        if (cw < 10 || ch < 10) { Console.Error.WriteLine("Area too small"); return 1; }

            // Visual MD render (parent scope)
            string visualMd = "";
            var scanEl = parentEl;
            for (int up = 0; up < 3 && scanEl != null; up++)
            {
                visualMd = UiaLocator.RenderVisualMarkdown(scanEl, maxDepth: 8);
                if (!string.IsNullOrEmpty(visualMd) && visualMd != "_(no text)_") break;
                try { scanEl = scanEl.Parent; } catch { break; }
            }
            result["visualMd"] = visualMd;

            // Collect UIA leaves for fused matching
            var uiaInfos = new List<WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo>();
            if (parentEl != null)
            {
                var leaves = new List<(string text, int lx, int ly, int lw, int lh, int depth)>();
                UiaLocator.CollectTextLeaves(parentEl, leaves, 0, 8);
                foreach (var leaf in leaves)
                    uiaInfos.Add(new WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo
                    {
                        Name = leaf.text,
                        Bounds = new Rectangle(leaf.lx - cx, leaf.ly - cy, leaf.lw, leaf.lh)
                    });
            }
            result["uiaLeaves"] = uiaInfos.Count;

            // Screenshot + CCA
            using var bmp = new Bitmap(cw, ch);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(cx, cy, 0, 0, new Size(cw, ch));

            var cca = new WKAppBot.Vision.ConnectedComponentAnalyzer();
            var regions = cca.Analyze(bmp);

            int textCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DyText);
            int iconCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DyIcon);
            int sepCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DySeparator);
            int contCnt = regions.Count(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DyContainer);
            var table = cca.DetectTable(regions, cw, ch);

            result["cca"] = new JsonObject
            {
                ["total"] = regions.Count,
                ["text"] = textCnt, ["icon"] = iconCnt,
                ["separator"] = sepCnt, ["container"] = contCnt,
                ["table"] = table != null ? $"{table.Rows}x{table.Cols}" : null
            };

            // Fused match
            if (uiaInfos.Count > 0)
            {
                var matchResults = WKAppBot.Vision.CcaUiaFusedMatcher.Match(regions, uiaInfos);
                result["fused"] = WKAppBot.Vision.CcaUiaFusedMatcher.Summarize(matchResults);

                // Save to Experience DB
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                string procName;
                try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { procName = "unknown"; }
                var clsBuf = new StringBuilder(64);
                NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
                WKAppBot.Vision.CcaUiaFusedMatcher.SaveToExperienceDb(
                    DataDir + "/experience", procName, clsBuf.ToString(), matchResults);
            }

            // DynId from mouse container
            string dynId = "";
            int mx = px - cx, my = py - cy;
            var containers = regions
                .Where(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DyContainer)
                .Where(r => r.Bounds.Contains(mx, my))
                .OrderBy(r => r.Bounds.Width * r.Bounds.Height).ToList();
            if (containers.Count > 0)
            {
                var c = containers[0];
                var allConts = regions
                    .Where(r => r.Type == WKAppBot.Vision.ConnectedComponentAnalyzer.RegionType.DyContainer)
                    .OrderBy(r => r.Bounds.Y).ThenBy(r => r.Bounds.X).ToList();
                int idx = allConts.IndexOf(c);
                int row = 0, col = 0;
                if (idx >= 0)
                {
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

            // Build grap path
            var titleBuf = new StringBuilder(128);
            NativeMethods.GetWindowTextW(hwnd, titleBuf, titleBuf.Capacity);
            var winTitle = titleBuf.ToString();
            if (string.IsNullOrEmpty(winTitle) || winTitle == "Chrome Legacy Window")
            {
                var ph = NativeMethods.GetParent(hwnd);
                if (ph == IntPtr.Zero) ph = NativeMethods.GetAncestor(hwnd, 2);
                if (ph != IntPtr.Zero)
                {
                    var b2 = new StringBuilder(256);
                    NativeMethods.GetWindowTextW(ph, b2, b2.Capacity);
                    if (b2.Length > 0) winTitle = b2.ToString();
                }
            }

            var grapScope = !string.IsNullOrEmpty(dynId) ? dynId
                : !string.IsNullOrEmpty(elAid) ? elAid
                : !string.IsNullOrEmpty(elName) ? $"*{elName}*" : "";
            var grapWin = winTitle.Length > 20 ? $"*{winTitle[..20]}*" : $"*{winTitle}*";

            result["elName"] = elName;
            result["elType"] = elType;
            if (info != null)
            {
                result["elBounds"] = $"{info.BoundsX},{info.BoundsY} {info.BoundsW}x{info.BoundsH}";
                if (!string.IsNullOrEmpty(info.Value))
                    result["elValue"] = info.Value;
                if (info.Patterns?.Count > 0)
                    result["elPatterns"] = string.Join(", ", info.Patterns);
            }
            result["dynId"] = dynId;
            result["grapPath"] = !string.IsNullOrEmpty(grapScope) ? $"{grapWin}#{grapScope}" : grapWin;
            result["winTitle"] = winTitle;
            result["captureRect"] = $"{cx},{cy} {cw}x{ch}";
        }
        catch (Exception ex)
        {
            result["error"] = ex.Message;
        }

        // Output JSON to stdout
        Console.Write(result.ToJsonString());
        return 0;
    }

    static IntPtr ResolveFrontmostWindowForPoint(IntPtr fallbackHwnd, int px, int py)
    {
        try
        {
            var pt = new POINT { X = px, Y = py };
            var topLeaf = NativeMethods.WindowFromPoint(pt);
            var candidate = topLeaf != IntPtr.Zero ? topLeaf : fallbackHwnd;
            if (candidate == IntPtr.Zero) return IntPtr.Zero;

            var cls = new StringBuilder(128);
            try { NativeMethods.GetClassNameW(candidate, cls, cls.Capacity); } catch { }
            candidate = NativeMethods.FindRealWindowFromPoint(pt, candidate, cls.ToString());

            var root = NativeMethods.GetAncestor(candidate, NativeMethods.GA_ROOT);
            if (root != IntPtr.Zero) candidate = root;

            IntPtr zTop = IntPtr.Zero;
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowRect(hWnd, out var r);
                if (px >= r.Left && px < r.Right && py >= r.Top && py < r.Bottom)
                {
                    // Skip LG overlay processes (fullscreen topmost screen-cover)
                    try
                    {
                        NativeMethods.GetWindowThreadProcessId(hWnd, out uint zpid);
                        var zProc = System.Diagnostics.Process.GetProcessById((int)zpid).ProcessName;
                        if (_knownLgOverlayProcesses.Contains(zProc))
                            return true; // continue enumeration, skip this window
                    }
                    catch { }

                    zTop = hWnd;
                    return false;
                }
                return true;
            }, IntPtr.Zero);

            if (zTop != IntPtr.Zero)
            {
                var zRoot = NativeMethods.GetAncestor(zTop, NativeMethods.GA_ROOT);
                if (zRoot != IntPtr.Zero) zTop = zRoot;
                candidate = zTop;
            }

            return candidate;
        }
        catch
        {
            return fallbackHwnd;
        }
    }

    /// <summary>
    /// Server mode: read JSON requests from stdin, write JSON responses to stdout.
    /// Each line = one request: {"hwnd":"hex","x":N,"y":N}
    /// Idle 5 minutes → auto-exit (memory freed).
    /// </summary>
    static int AnalyzeHackServer()
    {
        Console.Error.WriteLine("[ANALYZE-HACK] Server mode started (stdin JSON, idle 5min auto-exit)");
        var idleTimeout = TimeSpan.FromMinutes(5);
        var lastActivity = DateTime.UtcNow;

        // Non-blocking stdin read with idle check
        while (true)
        {
            if (DateTime.UtcNow - lastActivity > idleTimeout)
            {
                Console.Error.WriteLine("[ANALYZE-HACK] Idle 5min → auto-exit");
                break;
            }

            // Check stdin available (non-blocking)
            if (!Console.In.Peek().Equals(-1) || Console.In.Peek() >= 0)
            {
                string? line;
                try { line = Console.ReadLine(); }
                catch { break; } // stdin closed
                if (line == null) break; // EOF

                lastActivity = DateTime.UtcNow;
                line = line.Trim();
                if (line.Length == 0) continue;
                if (line == "exit" || line == "quit") break;

                try
                {
                    var req = JsonSerializer.Deserialize<JsonNode>(line);
                    var reqType = req?["type"]?.GetValue<string>() ?? "mouse";

                    if (reqType == "focus")
                    {
                        // Focus chain analysis
                        var result = AnalyzeFocusChain();
                        Console.WriteLine(result.ToJsonString());
                        Console.Out.Flush();
                    }
                    else
                    {
                        // Mouse CCA analysis (default)
                        var hwndStr = req?["hwnd"]?.GetValue<string>() ?? "0";
                        var xVal = req?["x"]?.GetValue<int>() ?? 0;
                        var yVal = req?["y"]?.GetValue<int>() ?? 0;
                        var hackArgs = new[] { hwndStr, xVal.ToString(), yVal.ToString() };
                        var oldOut = Console.Out;
                        var sw = new System.IO.StringWriter();
                        Console.SetOut(sw);
                        AnalyzeHackCommand(hackArgs);
                        Console.SetOut(oldOut);
                        Console.WriteLine(sw.ToString());
                        Console.Out.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(new JsonObject { ["error"] = ex.Message }.ToJsonString());
                    Console.Out.Flush();
                }
            }
            else
            {
                Thread.Sleep(100); // poll interval
            }
        }

        Console.Error.WriteLine("[ANALYZE-HACK] Server exiting");
        return 0;
    }

    /// <summary>Analyze keyboard focus chain — focus node → parent chain → root.</summary>
    static JsonObject AnalyzeFocusChain()
    {
        var result = new JsonObject();
        try
        {
            using var uia = new UiaLocator();
            var focused = uia.GetFocusedElementInfo();
            if (focused == null) { result["error"] = "no focus"; return result; }
            var inspectText = uia.GetFocusedInspectText();

            result["name"] = focused.Name;
            result["type"] = focused.ControlType;
            result["aid"] = focused.AutomationId;
            result["value"] = focused.Value;
            result["winTitle"] = focused.WindowTitle;
            result["pid"] = focused.ProcessId;
            result["bounds"] = $"{focused.Bounds.X},{focused.Bounds.Y} {focused.Bounds.Width}x{focused.Bounds.Height}";
            if (!string.IsNullOrWhiteSpace(inspectText))
                result["inspectText"] = inspectText;

            if (focused.Patterns?.Count > 0)
                result["patterns"] = string.Join(", ", focused.Patterns);

            if (focused.SelectionRects.Count > 0)
            {
                var sels = new JsonArray();
                foreach (var r in focused.SelectionRects)
                    sels.Add($"{r.X},{r.Y} {r.Width}x{r.Height}");
                result["selectionRects"] = sels;
            }

            var chain = new JsonArray();
            if (focused.ParentChain != null)
                foreach (var (type, name) in focused.ParentChain)
                    chain.Add(new JsonObject { ["type"] = type, ["name"] = name });
            result["chain"] = chain;

            // Build grap
            var winTitle = focused.WindowTitle ?? "";
            var grapWin = winTitle.Length > 20 ? $"*{winTitle[..20]}*" : $"*{winTitle}*";
            var grapScope = !string.IsNullOrEmpty(focused.AutomationId) ? focused.AutomationId
                : !string.IsNullOrEmpty(focused.Name) ? $"*{(focused.Name.Length > 20 ? focused.Name[..20] : focused.Name)}*"
                : "";
            result["grapPath"] = !string.IsNullOrEmpty(grapScope) ? $"{grapWin}#{grapScope}" : grapWin;
        }
        catch (Exception ex) { result["error"] = ex.Message; }
        return result;
    }
}
