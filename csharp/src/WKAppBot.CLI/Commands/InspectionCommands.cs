using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: inspect, focus, watch, capture commands + watch helpers
internal partial class Program
{
    // ── find (unified search — absorbs windows --uia + inspect --filter) ──

    static int FindCommand(string[] args)
    {
        if (args.Length == 0 || (args.Length == 1 && args[0].StartsWith("--")))
            return Error("Usage: wkappbot find <keyword> [--deep] [--limit N] [--process <name>]\n" +
                         "  Unified search: window titles + UIA accessibility elements.\n" +
                         "  Path search: find \"윈도우/UIA요소\" — / separates hierarchy levels.\n" +
                         "    * = any chars within one level, ** = any number of levels.\n" +
                         "    Each segment is implicitly *segment* (contains match).\n" +
                         "  --deep: Deeper UIA tree search (depth 12, slower but thorough).\n" +
                         "  Examples:\n" +
                         "    find \"Claude\"              Search everywhere for 'Claude'\n" +
                         "    find \"투혼/현재가\"         투혼 windows → ... → 현재가 element\n" +
                         "    find \"투혼/**/현재가\"      투혼 → any depth → 현재가\n" +
                         "    find \"Claude/**/Radio*\"    Claude → any depth → Radio* elements");

        // Preprocess: inject --uia (or --uia-deep for --deep) and forward to WindowsCommand
        var forwarded = new List<string>(args);
        bool hasDeep = forwarded.Remove("--deep");

        // Path search: "/" present → auto deep (hierarchy needs thorough search)
        string keyword = forwarded.FirstOrDefault(a => !a.StartsWith("--")) ?? "";
        if (keyword.Contains('/') || keyword.Contains("**"))
            hasDeep = true;

        forwarded.Add(hasDeep ? "--uia-deep" : "--uia");
        return WindowsCommand(forwarded.ToArray());
    }

    // ── inspect ────────────────────────────────────────────────

    static int InspectCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot inspect <window-title> [--depth N] [--win32] [--filter <pattern>]\n  --filter: Search entire UIA tree for matching elements (Name/AutomationId/ControlType)\n            Supports wildcards (*/?), regex: prefix, or plain substring");

        string title = args[0];
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 5;
        bool win32Mode = args.Contains("--win32");
        string? filter = GetArgValue(args, "--filter");

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.WriteLine($"No window found matching: \"{title}\"");
            return 1;
        }

        var win = windows[0];
        Console.WriteLine($"Window: {win}");
        Console.WriteLine();

        if (win32Mode)
        {
            // Win32 recursive child window tree (for MFC/native apps where UIA is empty)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── Win32 Window Tree (depth={depth}) ──");
            Console.ResetColor();
            var win32Tree = WindowFinder.DumpWin32Tree(win.Handle, depth);
            Console.Write(win32Tree);
        }
        else if (filter != null)
        {
            // Filtered UIA tree — search entire tree, dump matching subtrees
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── UIA Tree Filtered: \"{filter}\" (subtree depth={depth}) ──");
            Console.ResetColor();
            using var uia = new UiaLocator();
            var tree = uia.DumpTreeFiltered(win.Handle, filter, depth);
            Console.Write(tree);
        }
        else
        {
            // UIA tree (default — works for UWP/WPF/WinForms)
            using var uia = new UiaLocator();
            var tree = uia.DumpTree(win.Handle, depth);
            Console.Write(tree);

            // Also show Win32 child count hint
            var children = WindowFinder.GetChildren(win.Handle);
            Console.WriteLine($"\n--- Win32 children: {children.Count} ---");
            if (children.Count > 0 && string.IsNullOrWhiteSpace(tree.Replace($"[Window] \"{win.Title}\"", "").Trim()))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Hint: UIA tree is empty. Try --win32 for Win32 native child windows.");
                Console.ResetColor();
            }
        }

        return 0;
    }

    // ── focus ─────────────────────────────────────────────────

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
            var windows = WindowFinder.FindByTitle(titleFilter);
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
        string? saveFile = GetArgValue(args, "--save");

        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  WKAppBot Watch — Real-time Element Tracker               ║");
        if (durationSec > 0)
            Console.WriteLine($"║  Tracking for {durationSec}s. Move mouse over UI elements.    ║");
        else
            Console.WriteLine("║  Move mouse over UI elements. Press Ctrl+C to stop.     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        using var uia = new UiaLocator();
        string lastElemKey = "";       // element identity (type+aid+name) — detect real element change
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

                        // Collect log entry
                        var logLine = BuildPlainLine(ts, pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                        if (hierarchyPath != null) logLine = $"{hierarchyPath}  " + logLine;
                        if (overlayDetected)
                            logLine += $"  !! overlay: 0x{hWndTop:X8} {topClass}";
                        logEntries.Add(logLine);

                        if (liveMode)
                        {
                            ClearCurrentLine();
                            WritePosAndElement(pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                            if (overlayDetected) WriteOverlayTag(hWndTop, topClass);
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
            WriteControlType(elemInfo.ControlType);

            if (!string.IsNullOrEmpty(elemInfo.Name))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" \"{Truncate(elemInfo.Name, 30)}\"");
            }
            if (!string.IsNullOrEmpty(elemInfo.AutomationId))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($" aid=\"{elemInfo.AutomationId}\"");
            }
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
            Console.Write("[?]");
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
            sb.Append($"[{elemInfo.ControlType}]");
            if (!string.IsNullOrEmpty(elemInfo.Name))
                sb.Append($" \"{elemInfo.Name}\"");
            if (!string.IsNullOrEmpty(elemInfo.AutomationId))
                sb.Append($" aid=\"{elemInfo.AutomationId}\"");
            if (!string.IsNullOrEmpty(elemInfo.Value))
                sb.Append($" val=\"{elemInfo.Value}\"");
            if (elemInfo.Patterns.Count > 0)
                sb.Append($" ({string.Join(",", elemInfo.Patterns)})");
        }
        else
        {
            sb.Append("[?]");
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

    static int CaptureCommand(string[] args)
    {
        if (args.Length == 0)
            return Error(@"Usage: appbot capture <window-title> [-o output.png] [--form <form-id>]
  --form <id>: Capture a specific MDI child form (brings it to front first).");

        string title = args[0];
        string output = GetArgValue(args, "-o") ?? $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        string? formId = GetArgValue(args, "--form");

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.WriteLine($"No window found matching: \"{title}\"");
            return 1;
        }

        var win = windows[0];

        if (formId != null)
        {
            // Find MDI child form and bring to front before capture
            var scanResult = AppScanner.Scan(win.Handle);
            var form = scanResult.Forms.FirstOrDefault(f =>
                f.FormId != null && f.FormId.Contains(formId, StringComparison.OrdinalIgnoreCase));
            if (form == null)
            {
                Console.WriteLine($"Form [{formId}] not found in \"{win.Title}\"");
                Console.WriteLine($"Available forms:");
                foreach (var f in scanResult.Forms)
                    Console.WriteLine($"  [{f.FormId}] {f.FormName}");
                return 1;
            }

            Console.WriteLine($"Capturing form: [{form.FormId}] {form.FormName}");

            // Bring MDI child to front using BringWindowToTop + SetWindowPos(TOPMOST)
            NativeMethods.BringWindowToTop(form.Handle);
            // Also use SetWindowPos SWP_NOACTIVATE to avoid stealing focus
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOP,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
            Thread.Sleep(200); // let repaint happen

            // Capture the MDI child window directly
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(form.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {output} ({bmp.Width}x{bmp.Height})");
        }
        else
        {
            Console.WriteLine($"Capturing: {win}");
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {output} ({bmp.Width}x{bmp.Height})");
        }

        return 0;
    }

    // ── ocr ────────────────────────────────────────────────────

    static int OcrCommand(string[] args)
    {
        if (args.Length == 0)
            return Error(@"Usage: wkappbot ocr <window-title|image.png> [--save] [-o output.txt]
  Runs OCR on a window screenshot or image file and prints all recognized text with coordinates.
  --save: Save annotated screenshot with OCR bounding boxes");

        string target = args[0];
        bool save = args.Contains("--save");
        string? outputFile = GetArgValue(args, "-o");

        System.Drawing.Bitmap screenshot;
        string sourceDesc;

        // Check if target is an image file
        if (File.Exists(target) && (target.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)))
        {
            screenshot = new System.Drawing.Bitmap(target);
            sourceDesc = Path.GetFileName(target);
        }
        else
        {
            // Treat as window title
            var windows = WindowFinder.FindByTitle(target);
            if (windows.Count == 0)
                return Error($"Window not found: \"{target}\"");

            var win = windows[0];
            Console.WriteLine($"Capturing: {win}");
            screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            sourceDesc = win.Title;
        }

        using (screenshot)
        {
            Console.WriteLine($"Image: {screenshot.Width}x{screenshot.Height} — {sourceDesc}");
            Console.WriteLine();

            // Run OCR
            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
            var result = ocr.RecognizeAll(screenshot).GetAwaiter().GetResult();

            Console.WriteLine($"── OCR Results ({result.Words.Count} words) ──────────────────────");
            Console.WriteLine();

            // Print full text first
            if (!string.IsNullOrWhiteSpace(result.FullText))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[Full Text]");
                Console.ResetColor();
                // Limit to first 2000 chars
                var text = result.FullText.Length > 2000
                    ? result.FullText[..2000] + "..."
                    : result.FullText;
                Console.WriteLine(text);
                Console.WriteLine();
            }

            // Print words with coordinates (grouped by Y position → lines)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Words with Coordinates]");
            Console.ResetColor();

            // Group words into lines by Y proximity (within 8px)
            var sortedWords = result.Words.OrderBy(w => w.Y).ThenBy(w => w.X).ToList();
            int lastLineY = -100;
            var lineWords = new List<WKAppBot.Vision.OcrWord>();

            foreach (var word in sortedWords)
            {
                if (Math.Abs(word.Y - lastLineY) > 8 && lineWords.Count > 0)
                {
                    // Print previous line
                    PrintOcrLine(lineWords);
                    lineWords.Clear();
                }
                lineWords.Add(word);
                lastLineY = word.Y;
            }
            if (lineWords.Count > 0) PrintOcrLine(lineWords);

            Console.WriteLine();
            Console.WriteLine($"Total: {result.Words.Count} words");

            // Save output
            if (outputFile != null)
            {
                var dir = Path.GetDirectoryName(outputFile);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(outputFile, result.FullText ?? "");
                Console.WriteLine($"Text saved: {outputFile}");
            }

            if (save)
            {
                var savePath = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                WKAppBot.Win32.Input.ScreenCapture.SaveToFile(screenshot, savePath);
                Console.WriteLine($"Screenshot saved: {savePath}");
            }
        }

        return 0;
    }

    static void PrintOcrLine(List<WKAppBot.Vision.OcrWord> words)
    {
        var lineText = string.Join(" ", words.Select(w => w.Text));
        var firstWord = words[0];
        Console.Write($"  Y={firstWord.Y,4} | ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(lineText);
        Console.ResetColor();
        Console.WriteLine($"  ({words.Count} words, x={firstWord.X}..{words.Last().X + words.Last().Width})");
    }

    // ── windows — list all top-level windows ────────────────────
    // Usage: wkappbot windows [--filter <pattern>] [--process <name>] [--class <name>]

    static int WindowsCommand(string[] args)
    {
        // First positional arg = title filter (like inspect <title>)
        string? filterTitle = args.Length > 0 && !args[0].StartsWith("--") ? args[0] : GetArgValue(args, "--filter");
        string? filterProcess = GetArgValue(args, "--process");
        string? filterClass = GetArgValue(args, "--class");
        bool showAll = args.Contains("--all"); // include zero-size/invisible
        bool deep = args.Contains("--deep");   // include child windows (EnumChildWindows)
        bool uiaDeep = args.Contains("--uia-deep"); // deep UIA search (find --deep)
        bool uiaSearch = uiaDeep || args.Contains("--uia"); // also search UIA elements
        int limit = int.TryParse(GetArgValue(args, "--limit"), out var lim) ? lim : 0; // 0=unlimited
        bool hasFilter = filterTitle != null || filterProcess != null || filterClass != null;

        // Process cache to avoid repeated Process.GetProcessById
        var processCache = new Dictionary<uint, string>();
        string GetProcessName(uint pid)
        {
            if (processCache.TryGetValue(pid, out var cached)) return cached;
            string name = "";
            try { name = Process.GetProcessById((int)pid).ProcessName; } catch { }
            processCache[pid] = name;
            return name;
        }

        // Get window info, apply filters. Returns null if filtered out or noise.
        (string title, string className, string process, uint pid, int w, int h, bool visible)?
            GetWindowInfo(IntPtr hWnd)
        {
            var titleBuf = new StringBuilder(512);
            NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
            string title = titleBuf.ToString();

            var classBuf = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            string className = classBuf.ToString();

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string processName = GetProcessName(pid);

            NativeMethods.GetWindowRect(hWnd, out var rect);
            int w = rect.Right - rect.Left;
            int h = rect.Bottom - rect.Top;
            bool visible = NativeMethods.IsWindowVisible(hWnd);

            // Skip noise unless --all
            if (!showAll && string.IsNullOrEmpty(title) && !visible) return null;
            if (!showAll && w == 0 && h == 0) return null;

            // Skip wkappbot windows (Eye/Zoom overlays) — don't search ourselves
            if (processName.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)) return null;

            // Apply filters
            if (filterTitle != null)
            {
                bool match = PatternMatcher.IsPattern(filterTitle)
                    ? PatternMatcher.Create(filterTitle).IsMatch(title)
                    : title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }
            if (filterProcess != null)
            {
                bool match = PatternMatcher.IsPattern(filterProcess)
                    ? PatternMatcher.Create(filterProcess).IsMatch(processName)
                    : processName.Contains(filterProcess, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }
            if (filterClass != null)
            {
                bool match = PatternMatcher.IsPattern(filterClass)
                    ? PatternMatcher.Create(filterClass).IsMatch(className)
                    : className.Contains(filterClass, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }

            return (title, className, processName, pid, w, h, visible);
        }

        // Broken pipe detection — TeeTextWriter handles IOException globally,
        // but we check IsPipeBroken to stop expensive EnumWindows/EnumChildWindows early
        bool IsPipeBroken() => Console.Out is TeeTextWriter tee && tee.IsPipeBroken;

        void PrintWindow(IntPtr hWnd, string title, string className, string process,
            uint pid, int w, int h, bool visible, bool isChild, bool isForeground)
        {
            string vis = visible ? "" : " [hidden]";
            string prefix = isChild ? "  └ " : "";
            string displayTitle = title.Length > 60 ? title[..57] + "..." : title;
            if (string.IsNullOrEmpty(displayTitle)) displayTitle = "(no title)";

            Console.ForegroundColor = isForeground ? ConsoleColor.Green
                : (visible && !string.IsNullOrEmpty(title)) ? ConsoleColor.White
                : ConsoleColor.DarkGray;
            Console.Write($"  {prefix}[{hWnd:X8}] ");
            Console.ForegroundColor = isForeground ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.Write($"\"{displayTitle}\"");
            Console.ResetColor();
            Console.Write($"  ({className}) {w}x{h}{vis}  [{process} pid={pid}]");
            if (isForeground) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(" ★"); Console.ResetColor(); }
            Console.WriteLine();
        }

        IntPtr fgWnd = NativeMethods.GetForegroundWindow();
        int totalCount = 0;
        int uiaMatchWindows = 0;

        // EnumWindows enumerates in Z-order (front to back) — no re-sort needed!
        bool hasPathSearch = filterTitle != null && (filterTitle.Contains('/') || filterTitle.Contains("**"));
        string mode = hasPathSearch ? "find path (Z-order ★=foreground)"
            : uiaDeep ? "find --deep (Z-order ★=foreground)"
            : uiaSearch ? "find (Z-order ★=foreground)"
            : deep ? "windows (deep, Z-order ★=foreground)"
            : "windows (Z-order ★=foreground)";
        Console.WriteLine($"── {mode} ──");
        Console.WriteLine();

        // Helper: get raw window info WITHOUT title filter (for --uia mode)
        (string title, string className, string process, uint pid, int w, int h, bool visible)?
            GetRawWindowInfo(IntPtr hWnd)
        {
            var titleBuf = new StringBuilder(512);
            NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
            string title = titleBuf.ToString();

            var classBuf = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            string className = classBuf.ToString();

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string processName = GetProcessName(pid);

            NativeMethods.GetWindowRect(hWnd, out var rect);
            int w = rect.Right - rect.Left;
            int h = rect.Bottom - rect.Top;
            bool visible = NativeMethods.IsWindowVisible(hWnd);

            if (!showAll && string.IsNullOrEmpty(title) && !visible) return null;
            if (!showAll && w == 0 && h == 0) return null;

            // Skip wkappbot windows (Eye/Zoom overlays) — don't search ourselves
            if (processName.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)) return null;

            // Process/class filters still apply (not title!)
            if (filterProcess != null)
            {
                bool match = PatternMatcher.IsPattern(filterProcess)
                    ? PatternMatcher.Create(filterProcess).IsMatch(processName)
                    : processName.Contains(filterProcess, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }
            if (filterClass != null)
            {
                bool match = PatternMatcher.IsPattern(filterClass)
                    ? PatternMatcher.Create(filterClass).IsMatch(className)
                    : className.Contains(filterClass, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }

            return (title, className, processName, pid, w, h, visible);
        }

        // Helper: print UIA matches inline under a window
        void PrintUiaMatches(List<UiaQuickMatch> matches)
        {
            foreach (var m in matches)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"    → [{m.ControlType}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{m.Name}\"");
                if (!string.IsNullOrEmpty(m.AutomationId))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" aid=\"{m.AutomationId}\"");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Helper: print UIA matches with name path (for path search mode)
        void PrintUiaMatchesWithPath(List<UiaQuickMatch> matches)
        {
            foreach (var m in matches)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"    → [{m.ControlType}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{m.Name}\"");
                if (!string.IsNullOrEmpty(m.AutomationId))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" aid=\"{m.AutomationId}\"");
                }
                if (!string.IsNullOrEmpty(m.NamePath))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"  path={m.NamePath}");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Build regex from path pattern for partial matching against full UIA name paths.
        // Path pattern → regex (partial match, IgnoreCase)
        // ** → .*             (any chars including / — crosses levels)
        // *  → [^/]*          (any chars excluding / — within one level)
        // /  → [^/]*/[^/]*   (one level boundary)
        // ?  → [^/]           (single char excluding /)
        // else → Regex.Escape (literal — . $ + etc. safely escaped)
        // "투혼/현재가"    → 투혼[^/]*/[^/]*현재가
        // "투혼/**/현재가" → 투혼[^/]*/[^/]*.*[^/]*/[^/]*현재가
        Regex BuildPathRegex(string pattern)
        {
            // Tokenize with regex: ** first (greedy), then single special chars, then literal runs
            var rxStr = Regex.Replace(pattern, @"\*\*|[*/?]|[^*/?]+", m => m.Value switch
            {
                "**" => ".*",
                "*"  => "[^/]*",
                "/"  => "[^/]*/[^/]*",
                "?"  => "[^/]",
                _    => Regex.Escape(m.Value),  // literal chunk (.→\. $→\$ etc.)
            });
            return new Regex(rxStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (IsPipeBroken()) return false;
            bool isFg = hWnd == fgWnd;
            bool parentPrinted = false;

            if (uiaSearch && filterTitle != null)
            {
                // ── Unified search: title OR UIA elements ──
                // Path search ("/") → build full name-path, match with PathGlob
                // Regular search → same keyword for title and UIA (OR logic)
                bool isPathSearch = filterTitle.Contains('/') || filterTitle.Contains("**");

                var raw = GetRawWindowInfo(hWnd);
                if (raw == null) return true; // noise or process/class mismatch

                var r = raw.Value;
                if (!r.visible || r.w < 50 || r.h < 50) return true; // skip tiny/hidden

                if (isPathSearch)
                {
                    // ── Path search: "투혼/현재가" → regex partial match on full name path ──
                    var pathRx = BuildPathRegex(filterTitle);

                    // Search all UIA elements with name paths (maxResults high — filtering is done post-hoc by regex)
                    var allElements = uiaDeep
                        ? UiaLocator.QuickSearch(hWnd, "", maxDepth: 12, maxResults: 5000, maxVisited: 3000, timeoutMs: 10000)
                        : UiaLocator.QuickSearch(hWnd, "", maxDepth: 4, maxResults: 2000, maxVisited: 1000, timeoutMs: 5000);

                    // Match full path: windowTitle/element/name/path (partial match!)
                    var uiaMatches = new List<UiaQuickMatch>();
                    foreach (var el in allElements)
                    {
                        string fullPath = string.IsNullOrEmpty(el.NamePath)
                            ? r.title : $"{r.title}/{el.NamePath}";
                        if (pathRx.IsMatch(fullPath))
                            uiaMatches.Add(el);
                    }

                    // Also check window-only match
                    bool windowOnlyMatch = pathRx.IsMatch(r.title);
                    if (uiaMatches.Count == 0 && !windowOnlyMatch) return true;

                    PrintWindow(hWnd, r.title, r.className, r.process, r.pid, r.w, r.h, r.visible, false, isFg);
                    if (uiaMatches.Count > 0)
                    {
                        PrintUiaMatchesWithPath(uiaMatches);
                        uiaMatchWindows++;
                    }
                }
                else
                {
                    // ── Regular search: keyword matches title OR UIA elements (OR logic) ──
                    bool titleMatch;
                    if (PatternMatcher.IsPattern(filterTitle))
                        titleMatch = PatternMatcher.Create(filterTitle).IsMatch(r.title);
                    else
                        titleMatch = r.title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase);

                    var uiaMatches = uiaDeep
                        ? UiaLocator.QuickSearch(hWnd, filterTitle, maxDepth: 12, maxResults: 10, maxVisited: 1500, timeoutMs: 8000)
                        : UiaLocator.QuickSearch(hWnd, filterTitle);

                    if (!titleMatch && uiaMatches.Count == 0) return true;

                    // Print window (Cyan if UIA-only match, normal if title match)
                    if (!titleMatch)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string dt = r.title.Length > 60 ? r.title[..57] + "..." : r.title;
                        if (string.IsNullOrEmpty(dt)) dt = "(no title)";
                        Console.Write($"  [{hWnd:X8}] \"{dt}\"  ({r.className})  [{r.process}]");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"  ({uiaMatches.Count} UIA match)");
                        Console.ResetColor();
                    }
                    else
                    {
                        PrintWindow(hWnd, r.title, r.className, r.process, r.pid, r.w, r.h, r.visible, false, isFg);
                    }

                    if (uiaMatches.Count > 0)
                    {
                        PrintUiaMatches(uiaMatches);
                        uiaMatchWindows++;
                    }
                }

                totalCount++;
                parentPrinted = true;
                Console.Out.Flush();
                if (limit > 0 && totalCount >= limit) return false;
            }
            else
            {
                // ── Original mode: title/process/class filter only ──
                var info = GetWindowInfo(hWnd);

                if (info != null)
                {
                    var v = info.Value;
                    PrintWindow(hWnd, v.title, v.className, v.process, v.pid, v.w, v.h, v.visible, false, isFg);
                    totalCount++;
                    parentPrinted = true;
                    if (limit > 0 && totalCount >= limit) { Console.Out.Flush(); return false; }
                }

                // Child windows (if --deep)
                if (deep)
                {
                    NativeMethods.EnumChildWindows(hWnd, (childHwnd, _) =>
                    {
                        if (IsPipeBroken() || (limit > 0 && totalCount >= limit)) return false;
                        var childInfo = GetWindowInfo(childHwnd);
                        if (childInfo != null)
                        {
                            if (!parentPrinted)
                            {
                                var titleBuf = new StringBuilder(512);
                                NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
                                var classBuf = new StringBuilder(256);
                                NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
                                NativeMethods.GetWindowThreadProcessId(hWnd, out uint ppid);
                                NativeMethods.GetWindowRect(hWnd, out var prect);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine($"  [{hWnd:X8}] \"{titleBuf}\"  ({classBuf}) {prect.Right - prect.Left}x{prect.Bottom - prect.Top}  [{GetProcessName(ppid)} pid={ppid}]  (parent)");
                                Console.ResetColor();
                                parentPrinted = true;
                            }
                            var c = childInfo.Value;
                            PrintWindow(childHwnd, c.title, c.className, c.process, c.pid, c.w, c.h, c.visible, true, false);
                            totalCount++;
                        }
                        return true;
                    }, IntPtr.Zero);
                }

                if (parentPrinted) Console.Out.Flush();
            }

            return !(limit > 0 && totalCount >= limit);
        }, IntPtr.Zero);

        Console.WriteLine();
        string uiaNote = uiaSearch ? $", UIA matched in {uiaMatchWindows} window(s)" : "";
        string limitNote = limit > 0 ? $", --limit {limit}" : "";
        string hint = uiaSearch ? "(--deep: thorough search)" : "(--uia: accessibility search, --deep: child windows)";
        Console.WriteLine($"Total: {totalCount}{uiaNote} {hint}{limitNote}");
        return 0;
    }
}
