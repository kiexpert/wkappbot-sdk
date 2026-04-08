using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int FocusCommand(string[] args)
    {
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 6;
        int delay = int.TryParse(GetArgValue(args, "--delay"), out var dl) ? dl : 3;
        bool showWin32 = args.Contains("--win32");
        bool interactive = args.Contains("--buttons") || args.Contains("-b");
        string? titleFilter = GetArgValue(args, "--title");

        IntPtr hWnd;

        if (titleFilter != null)
        {
            // Direct title search — no delay needed
            var windows = WindowFinder.FindWindows(titleFilter);
            if (windows.Count == 0)
                return Error($"No window found matching: \"{titleFilter}\"");
            hWnd = windows[0].Handle;
        }
        else
        {
            // Countdown to let user focus the target window
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = delay; i > 0; i--)
            {
                Console.Write($"\r  Focus target window... {i}s ");
                Thread.Sleep(1000);
            }
            Console.ResetColor();
            Console.WriteLine("\r" + new string(' ', 40));

            // Get foreground window
            hWnd = NativeMethods.GetForegroundWindow();
        }

        if (hWnd == IntPtr.Zero)
            return Error("No foreground window found");

        var win = WindowInfo.FromHwnd(hWnd);

        // Process info
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
        string procName = "(unknown)";
        try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }

        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  WKAppBot Focus — Active Window Inspector                 ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("  ■ Window Info");
        Console.ResetColor();
        Console.WriteLine($"    Title     : \"{win.Title}\"");
        Console.WriteLine($"    Class     : {win.ClassName}");
        Console.WriteLine($"    HWND      : 0x{hWnd:X8}");
        Console.WriteLine($"    PID       : {pid} ({procName})");
        Console.WriteLine($"    Rect      : ({win.Rect.Left},{win.Rect.Top}) {win.Rect.Width}x{win.Rect.Height}");

        // Check for MDI
        var hMdi = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi != IntPtr.Zero)
        {
            int mdiCount = WindowFinder.CountMDIChildren(hWnd);
            var hActive = WindowFinder.GetActiveMDIChild(hWnd);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"    MDI       : Yes ({mdiCount} children, active=0x{hActive:X8})");
            Console.ResetColor();
        }

        Console.WriteLine();

        // Win32 direct children summary
        var children = WindowFinder.GetChildren(hWnd);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  ■ Win32 Children: {children.Count}");
        Console.ResetColor();

        if (showWin32 && children.Count > 0)
        {
            foreach (var child in children.Take(50))
            {
                var vis = NativeMethods.IsWindowVisible(child.Handle) ? "" : " [hidden]";
                Console.WriteLine($"    0x{child.Handle:X8} \"{child.Title}\" ({child.ClassName}) cid={child.ControlId}{vis}");
            }
            if (children.Count > 50)
                Console.WriteLine($"    ... +{children.Count - 50} more");
        }

        Console.WriteLine();

        // UIA tree
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  ■ UI Automation Tree (depth={depth})");
        Console.ResetColor();

        using var uia = new UiaLocator();

        // Hot focus chain (always shown, depth-independent)
        var focusChain = uia.GetFocusChain(hWnd);
        if (!string.IsNullOrEmpty(focusChain))
            Console.Write(focusChain);

        var tree = uia.DumpTree(hWnd, depth);

        // Count interactive elements for summary
        int buttonCount = 0, editCount = 0, textCount = 0, listCount = 0, otherCount = 0;
        var interactiveElements = new List<string>();

        foreach (var line in tree.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.Contains("[Button]"))
            {
                buttonCount++;
                // Extract interesting buttons (with aid or short name)
                var aidStart = line.IndexOf("aid=\"") + 5;
                var aidEnd = line.IndexOf("\"", aidStart);
                var aid = aidStart > 4 && aidEnd > aidStart ? line.Substring(aidStart, aidEnd - aidStart) : "";

                var nameStart = line.IndexOf('"') + 1;
                var nameEnd = line.IndexOf('"', nameStart);
                var name = nameStart > 0 && nameEnd > nameStart ? line.Substring(nameStart, nameEnd - nameStart) : "";

                if (!string.IsNullOrEmpty(aid))
                    interactiveElements.Add($"    🔘 \"{name}\" aid=\"{aid}\"");
                else if (!string.IsNullOrEmpty(name) && name != "(err)" && name != "(null)")
                    interactiveElements.Add($"    🔘 \"{name}\"");
            }
            else if (line.Contains("[Edit]")) editCount++;
            else if (line.Contains("[Text]")) textCount++;
            else if (line.Contains("[List]") || line.Contains("[DataGrid]") || line.Contains("[Table]")) listCount++;
            else otherCount++;
        }

        // Summary of interactive elements
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  ■ Element Summary");
        Console.ResetColor();
        Console.WriteLine($"    Buttons: {buttonCount}  |  Edits: {editCount}  |  Texts: {textCount}  |  Lists: {listCount}  |  Other: {otherCount}");

        if (interactive && interactiveElements.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ■ Clickable Buttons ({interactiveElements.Count}):");
            Console.ResetColor();
            foreach (var el in interactiveElements.Take(40))
                Console.WriteLine(el);
            if (interactiveElements.Count > 40)
                Console.WriteLine($"    ... +{interactiveElements.Count - 40} more");
        }

        // Full tree output (optional, always print)
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"\n  ── Full UIA Tree ──");
        Console.ResetColor();
        Console.Write(tree);

        // Dynamic a11y: auto Gemini fallback when UIA tree is sparse
        TryDynamicA11yFallback(hWnd, tree, args);

        Console.WriteLine();
        return 0;
    }

    // ── watch ──────────────────────────────────────────────────

    static int WatchCommand(string[] args)
    {
        int intervalMs = int.TryParse(GetArgValue(args, "--interval"), out var iv) ? iv : 150;
        int durationSec = int.TryParse(GetArgValue(args, "--duration"), out var dur) ? dur : 0;
        bool showWin32 = args.Contains("--win32");
        bool liveMode = args.Contains("--live");  // single-line overwrite mode
        bool hoverAnalyze = args.Contains("--hover-analyze");
        string? saveFile = GetArgValue(args, "--save");

        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  WKAppBot Watch — Real-time Element Tracker               ║");
        if (durationSec > 0)
            Console.WriteLine($"║  Tracking for {durationSec}s. Move mouse over UI elements.    ║");
        else
            Console.WriteLine("║  Move mouse over UI elements. Press Ctrl+C to stop.     ║");
        if (hoverAnalyze)
            Console.WriteLine("║  [HOVER] 1s still → auto UIA tree dump                  ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        using var uia = new UiaLocator();
        string lastElemKey = "";       // element identity (type+aid+name) — detect real element change
        string lastFocusKey = "";      // keyboard focus identity — detect focus change
        var logEntries = new List<string>();
        var stopwatch = Stopwatch.StartNew();
        bool running = true;
        int changeCount = 0;
        var seenElements = new HashSet<string>();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;  // don't kill immediately — let cleanup run
            running = false;
        };

        // Also monitor stdin EOF (Ctrl+Z on Windows) in a background thread
        // Only when stdin is a real console (not redirected/piped)
        bool stdinIsConsole;
        try { stdinIsConsole = !Console.IsInputRedirected; } catch { stdinIsConsole = false; }

        if (stdinIsConsole)
        {
            var eofThread = new Thread(() =>
            {
                try
                {
                    while (running)
                    {
                        // Console.In.Peek() returns -1 at EOF (Ctrl+Z + Enter)
                        if (Console.In.Peek() == -1) { running = false; break; }
                        Thread.Sleep(200);
                    }
                }
                catch { /* stdin not available */ }
            }) { IsBackground = true, Name = "EofWatcher" };
            eofThread.Start();
        }

        // Print column header for scroll mode
        if (!liveMode)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Time         Pos          Element");
            Console.WriteLine("  ──────────── ──────────── ─────────────────────────────────────────");
            Console.ResetColor();
        }

        int lastPtX = -1, lastPtY = -1;
        bool lineHasContent = false;  // tracks if current \r line has content
        DateTime lastMoveTime = DateTime.Now;
        IntPtr lastAnalyzedHwnd = IntPtr.Zero;

        while (running)
        {
            // Check duration limit
            if (durationSec > 0 && stopwatch.Elapsed.TotalSeconds >= durationSec)
                break;

            try
            {
                NativeMethods.GetCursorPos(out var pt);
                var hWndTop = NativeMethods.WindowFromPoint(pt);
                var topClass = WindowFinder.GetClassName(hWndTop);

                // Skip transparent overlays (Chrome extensions, screen capture tools)
                var hWndUnder = NativeMethods.FindRealWindowFromPoint(pt, hWndTop, topClass);
                var win32Class = (hWndUnder == hWndTop) ? topClass : WindowFinder.GetClassName(hWndUnder);
                int ctrlId = NativeMethods.GetDlgCtrlID(hWndUnder);

                // Get UIA element at point
                ElementAtPointInfo? elemInfo;
                bool overlayDetected = (hWndUnder != hWndTop);

                if (overlayDetected)
                {
                    // Win32 Z-order detected overlay → use real window's UIA tree
                    elemInfo = uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndUnder);
                }
                else
                {
                    // Normal UIA FromPoint first
                    elemInfo = uia.GetElementAtPoint(pt.X, pt.Y);

                    // Check if UIA returned a known overlay element (e.g. Chrome extension BTN_BKGRND)
                    // Even if Win32 didn't detect it (non-transparent overlay)
                    if (elemInfo != null && IsOverlayElement(elemInfo.AutomationId, win32Class))
                    {
                        overlayDetected = true;
                        // Find real window behind this one
                        var realHwnd = NativeMethods.GetWindow(hWndUnder, NativeMethods.GW_HWNDNEXT);
                        int skip = 0;
                        while (realHwnd != IntPtr.Zero && skip < 30)
                        {
                            skip++;
                            if (NativeMethods.IsWindowVisible(realHwnd))
                            {
                                NativeMethods.GetWindowRect(realHwnd, out var rc);
                                if (NativeMethods.PtInRect(ref rc, pt))
                                {
                                    hWndUnder = realHwnd;
                                    win32Class = WindowFinder.GetClassName(realHwnd);
                                    ctrlId = NativeMethods.GetDlgCtrlID(realHwnd);
                                    elemInfo = uia.GetElementAtPointInWindow(pt.X, pt.Y, realHwnd);
                                    break;
                                }
                            }
                            realHwnd = NativeMethods.GetWindow(realHwnd, NativeMethods.GW_HWNDNEXT);
                        }
                    }
                }

                // Build element identity key (detect element change, not just pixel change)
                string elemKey;
                if (elemInfo != null)
                    elemKey = $"{elemInfo.ControlType}|{elemInfo.AutomationId}|{elemInfo.Name}|0x{hWndUnder:X8}";
                else
                    elemKey = $"?|0x{hWndUnder:X8}";

                bool elementChanged = (elemKey != lastElemKey);
                bool posChanged = (pt.X != lastPtX || pt.Y != lastPtY);

                if (elementChanged || posChanged)
                {
                    lastPtX = pt.X;
                    lastPtY = pt.Y;
                    var ts = DateTime.Now.ToString("HH:mm:ss.fff");

                    if (elementChanged)
                    {
                        // Element changed → finalize current line, start new one
                        lastElemKey = elemKey;
                        changeCount++;
                        seenElements.Add(elemKey);

                        // Build combined hierarchy path
                        string? hierarchyPath = null;
                        try
                        {
                            hierarchyPath = uia.GetHierarchyPath(pt.X, pt.Y, hWndUnder, useWindowTree: overlayDetected);
                        }
                        catch { }

                        // Keyboard focus node (snapshot at same moment)
                        string? focusLine = null;
                        try
                        {
                            var focused = uia.Automation.FocusedElement();
                            if (focused != null)
                            {
                                var fName = focused.Properties.Name.ValueOrDefault ?? "";
                                var fType = focused.Properties.ControlType.ValueOrDefault.ToString().Replace("ControlType.", "") ?? "";
                                var fAid  = focused.Properties.AutomationId.ValueOrDefault ?? "";
                                var focusKey = $"{fType}|{fAid}|{fName}";
                                if (focusKey != elemKey) // only show if different from cursor element
                                {
                                    focusLine = $"[KB] {fType} \"{fName}\"{(fAid.Length > 0 ? $" #{fAid}" : "")}";
                                    lastFocusKey = focusKey;
                                }
                            }
                        }
                        catch { }

                        // Collect log entry
                        var logLine = BuildPlainLine(ts, pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                        if (hierarchyPath != null) logLine = $"{hierarchyPath}  " + logLine;
                        if (overlayDetected)
                            logLine += $"  !! overlay: 0x{hWndTop:X8} {topClass}";
                        if (focusLine != null) logLine += $"  {focusLine}";
                        logEntries.Add(logLine);

                        if (liveMode)
                        {
                            ClearCurrentLine();
                            WritePosAndElement(pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                            if (overlayDetected) WriteOverlayTag(hWndTop, topClass);
                            if (focusLine != null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write($"  {focusLine}");
                            }
                        }
                        else
                        {
                            if (lineHasContent)
                                Console.WriteLine();

                            // Line 1: combined [ClassPath] NamePath
                            if (!string.IsNullOrEmpty(hierarchyPath))
                            {
                                Console.Write("  ");
                                var bracketEnd = hierarchyPath.IndexOf("] ");
                                if (bracketEnd >= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write(hierarchyPath[..(bracketEnd + 1)]);
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    Console.Write(hierarchyPath[(bracketEnd + 1)..]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    Console.Write(hierarchyPath);
                                }
                                if (overlayDetected) WriteOverlayTag(hWndTop, topClass);
                                Console.WriteLine();
                            }

                            // Line 2: timestamp + coords + element detail
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"  {ts}  ");

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"({pt.X,5},{pt.Y,5})  ");

                            WritePosAndElement(null, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);

                            // Line 3: keyboard focus (if different from cursor element)
                            if (focusLine != null)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write($"  {focusLine}");
                            }
                            lineHasContent = true;
                        }
                    }
                    else if (posChanged && !liveMode && lineHasContent)
                    {
                        // Same element, mouse moved → overwrite position on current line with \r
                        // We re-render the whole line at the same position
                        ClearCurrentLine();

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"  {ts}  ");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"({pt.X,5},{pt.Y,5})  ");

                        WritePosAndElement(null, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                    }
                    else if (posChanged && liveMode)
                    {
                        ClearCurrentLine();
                        WritePosAndElement(pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                    }
                }

                // ── Hover-analyze: dump UIA tree after 1s of no movement ──
                if (hoverAnalyze)
                {
                    if (posChanged)
                    {
                        lastMoveTime = DateTime.Now;
                        lastAnalyzedHwnd = IntPtr.Zero;  // reset so new window gets analyzed
                    }
                    else if (hWndUnder != IntPtr.Zero
                          && hWndUnder != lastAnalyzedHwnd
                          && (DateTime.Now - lastMoveTime).TotalMilliseconds >= 1000)
                    {
                        lastAnalyzedHwnd = hWndUnder;
                        if (lineHasContent) { Console.WriteLine(); lineHasContent = false; }

                        var titleBuf = new System.Text.StringBuilder(256);
                        NativeMethods.GetWindowTextW(hWndUnder, titleBuf, titleBuf.Capacity);
                        var winTitle = titleBuf.ToString();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n  [HOVER] Auto-analyzing: \"{winTitle}\" (0x{hWndUnder:X8})");
                        Console.ResetColor();
                        try
                        {
                            var tree = uia.DumpTree(hWndUnder, maxDepth: 6);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            foreach (var line in tree.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                                Console.WriteLine($"    {line}");
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"  [HOVER] Tree dump failed: {ex.Message}");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                }

                Thread.Sleep(intervalMs);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(intervalMs);
            }
        }

        // Finalize last line
        if (lineHasContent) Console.WriteLine();

        // ── Summary ──
        stopwatch.Stop();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  ────────────────────────────────────────────────");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  Duration    : {stopwatch.Elapsed:mm\\:ss\\.f}");
        Console.WriteLine($"  Changes     : {changeCount}");
        Console.WriteLine($"  Unique elems: {seenElements.Count}");
        Console.ResetColor();

        // Save log
        if (logEntries.Count > 0)
        {
            var logFile = saveFile ?? $"watch_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            try
            {
                File.WriteAllLines(logFile, logEntries);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  Saved       : {logFile} ({logEntries.Count} entries)");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  Save failed : {ex.Message}");
                Console.ResetColor();
            }
        }

        Console.WriteLine();
        return 0;
    }

    /// <summary>
    /// Write colorized element info. If pos is provided, also prints coordinates.
    /// </summary>
    private static void WritePosAndElement(
        POINT? pos,
        ElementAtPointInfo? elemInfo,
        IntPtr hWnd, string win32Class, int ctrlId, bool showWin32)
    {
        if (pos.HasValue)
        {
            Console.Write($"  ({pos.Value.X,5},{pos.Value.Y,5}) ");
        }

        if (elemInfo != null)
        {
            var rect = new System.Drawing.Rectangle(elemInfo.BoundsX, elemInfo.BoundsY, elemInfo.BoundsW, elemInfo.BoundsH);
            var tag = GrapHelper.FormatNodeLabel(elemInfo.ControlType, elemInfo.AutomationId, elemInfo.Name, rect: rect);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(tag);
            if (!string.IsNullOrEmpty(elemInfo.Value))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" val=\"{Truncate(elemInfo.Value, 20)}\"");
            }
            if (elemInfo.Patterns.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($" ({string.Join(",", elemInfo.Patterns)})");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("<Unknown>");
        }

        if (showWin32)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  | 0x{hWnd:X8} {win32Class}");
            if (ctrlId != 0) Console.Write($" cid={ctrlId}");
        }

        Console.ResetColor();
    }

    /// <summary>
    /// Build a plain-text log line (no colors) for file saving.
    /// </summary>
    private static string BuildPlainLine(
        string ts, POINT pt,
        ElementAtPointInfo? elemInfo,
        IntPtr hWnd, string win32Class, int ctrlId, bool showWin32)
    {
        var sb = new StringBuilder();
        sb.Append($"[{ts}] ({pt.X,5},{pt.Y,5}) ");

        if (elemInfo != null)
        {
            sb.Append(GrapHelper.FormatNodeLabel(elemInfo.ControlType, elemInfo.AutomationId, elemInfo.Name,
                rect: new System.Drawing.Rectangle(elemInfo.BoundsX, elemInfo.BoundsY, elemInfo.BoundsW, elemInfo.BoundsH)));
            if (!string.IsNullOrEmpty(elemInfo.Value))
                sb.Append($" val=\"{elemInfo.Value}\"");
            if (elemInfo.Patterns.Count > 0)
                sb.Append($" ({string.Join(",", elemInfo.Patterns)})");
        }
        else
        {
            sb.Append("<Unknown>");
        }

        if (showWin32)
        {
            sb.Append($"  | 0x{hWnd:X8} {win32Class}");
            if (ctrlId != 0) sb.Append($" cid={ctrlId}");
        }

        return sb.ToString();
    }

    private static void WriteControlType(string ct)
    {
        Console.ForegroundColor = ct switch
        {
            "Button" => ConsoleColor.Yellow,
            "Edit" => ConsoleColor.Cyan,
            "Text" => ConsoleColor.Gray,
            "List" or "ListItem" or "DataGrid" or "DataItem" => ConsoleColor.Magenta,
            "ComboBox" => ConsoleColor.Blue,
            "CheckBox" or "RadioButton" => ConsoleColor.DarkYellow,
            "Tab" or "TabItem" => ConsoleColor.DarkCyan,
            "Menu" or "MenuItem" or "MenuBar" => ConsoleColor.DarkGreen,
            "Window" or "Pane" => ConsoleColor.DarkGray,
            _ => ConsoleColor.White
        };
        Console.Write($"[{ct}]");
        Console.ResetColor();
    }

    /// <summary>
    /// Check if a UIA element is a known overlay (Chrome extension background, etc.)
    /// </summary>
    private static bool IsOverlayElement(string? automationId, string className)
    {
        // Chrome extension overlays
        if (className.StartsWith("Chrome_WidgetWin") &&
            (automationId == "BTN_BKGRND" || automationId == ""))
            return true;
        return false;
    }

    private static void WriteOverlayTag(IntPtr overlayHwnd, string overlayClass)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"  !! {overlayClass}");
        Console.ResetColor();
    }

    private static void ClearCurrentLine()
    {
        int w;
        try { w = Console.WindowWidth; } catch { w = 120; }
        Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");
    }

    private static string Truncate(string s, int max) =>
        s.Length <= max ? s : s[..max] + "...";

    // ── capture ────────────────────────────────────────────────

}
