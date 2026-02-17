using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Vision;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

/// <summary>
/// CLI entry point: appbot run|validate|inspect|capture
/// </summary>
internal class Program
{
    static int Main(string[] args)
    {
        // Force UTF-8 output (Windows 949 codepage breaks Korean in non-Korean terminals)
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Enable DPI awareness
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }

        // Auto-log: tee all console output to file
        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var exeName = Path.GetFileName(exePath);
        var logDir = Path.Combine(Path.GetDirectoryName(exePath) ?? ".", "logs");
        var logFile = Path.Combine(logDir, $"{exeName}.out-{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        using var tee = new TeeTextWriter(Console.Out, logFile);
        Console.SetOut(tee);

        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();

            return command switch
            {
                "run" => RunCommand(restArgs),
                "validate" => ValidateCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "do" => DoCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                _ => Error($"Unknown command: {command}")
            };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
        finally
        {
            Console.SetOut(tee.OriginalConsole);
            Console.WriteLine($"Log saved: {tee.LogPath}");
        }
    }

    // ── run ────────────────────────────────────────────────────

    static int RunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot run <scenario.yaml> [-v|--verbose] [--no-watch] [--report <dir>]");

        string scenarioPath = args[0];
        bool verbose = args.Contains("-v") || args.Contains("--verbose");
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 200;
        string? reportDir = GetArgValue(args, "--report");

        // Parse scenario
        var doc = ScenarioParser.Load(scenarioPath);
        Console.WriteLine($"Loaded: {doc.Scenario.Name} ({doc.Steps.Count} steps)");

        // Run with passive background watcher (default: on)
        var runner = new ScenarioRunner(verbose, watch: !noWatch, watchIntervalMs: watchMs);
        var result = runner.Run(doc);

        // TODO: Generate HTML report if reportDir specified

        return result.IsSuccess ? 0 : 1;
    }

    // ── validate ───────────────────────────────────────────────

    static int ValidateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot validate <scenario.yaml>");

        string path = args[0];
        if (ScenarioParser.TryValidate(path, out string? error))
        {
            var doc = ScenarioParser.Load(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"VALID: {doc.Scenario.Name}");
            Console.ResetColor();
            Console.WriteLine($"  Steps: {doc.Steps.Count}");
            Console.WriteLine($"  App: {doc.App.Launch}");
            if (doc.Teardown != null)
                Console.WriteLine($"  Teardown: {doc.Teardown.Count} steps");
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"INVALID: {error}");
            Console.ResetColor();
            return 1;
        }
    }

    // ── inspect ────────────────────────────────────────────────

    static int InspectCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot inspect <window-title> [--depth N] [--win32]");

        string title = args[0];
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 5;
        bool win32Mode = args.Contains("--win32");

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
        var sb = new System.Text.StringBuilder();
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
            return Error("Usage: appbot capture <window-title> [-o output.png]");

        string title = args[0];
        string output = GetArgValue(args, "-o") ?? $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.WriteLine($"No window found matching: \"{title}\"");
            return 1;
        }

        var win = windows[0];
        Console.WriteLine($"Capturing: {win}");

        using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
        WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
        Console.WriteLine($"Saved: {output} ({bmp.Width}x{bmp.Height})");

        return 0;
    }

    // ── hts-stress ─────────────────────────────────────────────

    static int HtsStressCommand(string[] args)
    {
        // Parse args
        string? formPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (!args[i].StartsWith("-") && formPath == null)
            { formPath = args[i]; continue; }
        }

        string pattern = GetArgValue(args, "--pattern") ?? "repeat";
        int iterations = int.TryParse(GetArgValue(args, "-n"), out var n) ? n : 20;
        int delayMs = int.TryParse(GetArgValue(args, "--delay"), out var dl) ? dl : 800;
        int openMs = int.TryParse(GetArgValue(args, "--open-ms"), out var o) ? o : 1000;
        int closeMs = int.TryParse(GetArgValue(args, "--close-ms"), out var c) ? c : 1000;
        int openCount = int.TryParse(GetArgValue(args, "--open"), out var oc) ? oc : 3;
        int closeCount = int.TryParse(GetArgValue(args, "--close"), out var cc) ? cc : 1;
        int batchSize = int.TryParse(GetArgValue(args, "--batch"), out var bs) ? bs : 1;
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 500;
        string processName = GetArgValue(args, "--process") ?? "HTSRun";

        if (string.IsNullOrEmpty(formPath))
            return Error(@"Usage: wkappbot hts-stress <form.xmf> [options]

Patterns (--pattern):
  repeat    Simple open/close N times (default)
  memory    Open M / close K per cycle + memory table
  ctx-only  Anchor form + repeated open/close (V8 Context isolation)

Options:
  -n N              Iterations (default: 20)
  --pattern <name>  Test pattern: repeat|memory|ctx-only
  --delay N         Delay between actions in ms (default: 800)
  --open-ms N       Open delay for repeat pattern (default: 1000)
  --close-ms N      Close delay for repeat pattern (default: 1000)
  --open N          Forms to open per cycle for memory pattern (default: 3)
  --close N         Forms to close per cycle for memory pattern (default: 1)
  --batch N         Batch size for ctx-only pattern (default: 1)
  --process <name>  Target process name (default: HTSRun)
  --no-watch        Disable background [WATCH] element tracker
  --watch-interval  Watcher polling ms (default: 500)");

        // Find target process + MDI frame
        var proc = Process.GetProcessesByName(processName).FirstOrDefault();
        if (proc == null)
            return Error($"{processName}.exe not found! Is the app running?");

        var hMain = WindowFinder.FindMDIMainFrame((uint)proc.Id);
        if (hMain == IntPtr.Zero)
            return Error($"No MDI main frame found in {processName} (PID: {proc.Id})");

        // Validate form path
        if (!File.Exists(formPath))
        {
            // Try relative to VIGSOne
            var alt = $@"W:\VIGSOne\BinD\Win32\MultiByte\HTSRun\screen\{formPath}";
            if (File.Exists(alt)) formPath = alt;
            else return Error($"Form file not found: {formPath}");
        }

        // Verify COPYDATASTRUCT SendMessage support
        var hMdi = NativeMethods.FindWindowExW(hMain, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero)
            return Error("MDIClient child not found — is this an MDI application?");

        var baseMdi = WindowFinder.CountMDIChildren(hMain);
        var baseMem = HtsInterop.TakeMemorySnapshot(proc);

        // ── Banner ──
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  WKAppBot HTS Stress — {pattern.ToUpper(),-10}                          ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"  Target   : {processName} (PID:{proc.Id}) MDI 0x{hMain:X8}");
        Console.WriteLine($"  Form     : {Path.GetFileName(formPath)}");
        Console.WriteLine($"  Pattern  : {pattern} x {iterations}");
        Console.WriteLine($"  Baseline : WS={baseMem.WorkingSetMB}MB  Priv={baseMem.PrivateMemMB}MB  MDI={baseMdi}");
        Console.WriteLine();

        // ── Initialize Simple OCR (free, offline) ──
        SimpleOcrAnalyzer? simpleOcr = null;
        try
        {
            var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
            var primaryLang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
            simpleOcr = new SimpleOcrAnalyzer(primaryLanguage: primaryLang, confidenceThreshold: 0.7f);
        }
        catch { /* OCR not available — continue without it */ }

        // ── Start background watcher (optional) ──
        BackgroundWatcher? watcher = null;
        var ctx = new RuntimeContext { MainWindowHandle = hMain, AppTitle = processName };
        object consoleLock = new();

        if (!noWatch)
        {
            watcher = new BackgroundWatcher(watchMs, ctx: ctx, ocr: simpleOcr);
            consoleLock = watcher.ConsoleLock;
            ctx.ConsoleLock = consoleLock;
            watcher.Start();

            var ocrTag = simpleOcr != null ? " + OCR" : "";
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[WATCH] ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Started — tracking MDI form changes{ocrTag}");
                Console.ResetColor();
            }
        }

        // ── Memory table header ──
        lock (consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  {"#",-6} {"WS(MB)",-9} {"Priv(MB)",-10} {"WS_Chg",-9} {"Priv_Chg",-10} {"MDI",-5} Phase");
            Console.WriteLine($"  {"─────",-6} {"───────",-9} {"────────",-10} {"──────",-9} {"────────",-10} {"───",-5} ─────");
            Console.ResetColor();
        }

        // ── Cycle event handler (all 3 patterns share this) ──
        int lastNudgeCycle = -1;
        void OnCycle(StressCycleEvent evt)
        {
            // Nudge watcher on cycle boundaries (not every open/close)
            if (watcher != null && evt.Cycle != lastNudgeCycle && evt.Phase != "OPEN")
            {
                watcher.Nudge();
                lastNudgeCycle = evt.Cycle;
            }

            // Only print summary rows (not individual OPEN/CLOSE for repeat)
            bool printRow = pattern switch
            {
                "repeat" => evt.Phase == "CLOSE",   // 1 row per cycle (after close)
                "memory" => evt.Phase == "CYCLE",    // 1 row per cycle
                "ctx-only" => true,                  // all phases
                _ => true
            };

            if (!printRow) return;

            var mem = evt.Memory;
            var wsDelta = mem.WsDeltaMB(evt.Baseline);
            var privDelta = mem.PrivDeltaMB(evt.Baseline);
            var wsSign = wsDelta >= 0 ? "+" : "";
            var privSign = privDelta >= 0 ? "+" : "";

            // Color: red>20MB, yellow>5MB, green otherwise
            var color = privDelta > 20 ? ConsoleColor.Red
                      : privDelta > 5 ? ConsoleColor.Yellow
                      : ConsoleColor.Green;

            lock (consoleLock)
            {
                // Clear any [WATCH] remnant
                ClearCurrentLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[STRESS] ");
                Console.ForegroundColor = color;
                Console.Write($"{evt.Cycle,-5} ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"{mem.WorkingSetMB,-9} {mem.PrivateMemMB,-10} ");
                Console.ForegroundColor = color;
                Console.Write($"{wsSign}{wsDelta,-8} {privSign}{privDelta,-9} ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{(evt.MdiAfter >= 0 ? evt.MdiAfter.ToString() : "?"),-5} ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(evt.Phase);

                if (!evt.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" FAIL");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // ── Run the selected pattern ──
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        StressResult result;
        try
        {
            result = pattern.ToLower() switch
            {
                "repeat" => HtsInterop.RunRepeat(
                    hMain, formPath, proc, iterations, openMs, closeMs, OnCycle, cts.Token),
                "memory" => HtsInterop.RunMemory(
                    hMain, formPath, proc, iterations, openCount, closeCount, delayMs, OnCycle, cts.Token),
                "ctx-only" or "ctx_only" or "ctxonly" => HtsInterop.RunCtxOnly(
                    hMain, formPath, proc, iterations, batchSize, delayMs, OnCycle, cts.Token),
                _ => throw new ArgumentException($"Unknown pattern: {pattern}. Use: repeat|memory|ctx-only")
            };
        }
        catch (OperationCanceledException)
        {
            lock (consoleLock)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[STRESS] Cancelled by user (Ctrl+C)");
                Console.ResetColor();
            }

            watcher?.Stop(printSummary: true);
            watcher?.Dispose();
            simpleOcr?.Dispose();
            return 0;
        }

        // ── Stop watcher ──
        watcher?.Stop(printSummary: true);
        watcher?.Dispose();
        simpleOcr?.Dispose();

        // ── Results summary ──
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  Results ({result.Elapsed:hh\\:mm\\:ss})                                       ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"  Pattern    : {result.Pattern}");
        Console.WriteLine($"  Iterations : {result.Iterations}");
        Console.WriteLine($"  Baseline   : WS={result.Baseline.WorkingSetMB}MB  Priv={result.Baseline.PrivateMemMB}MB");
        Console.WriteLine($"  Final      : WS={result.Final.WorkingSetMB}MB  Priv={result.Final.PrivateMemMB}MB  MDI={result.RemainingMdi}");

        var growthColor = result.PrivGrowthMB > 20 ? ConsoleColor.Red
                        : result.PrivGrowthMB > 5 ? ConsoleColor.Yellow
                        : ConsoleColor.Green;
        Console.ForegroundColor = growthColor;
        Console.WriteLine($"  Growth     : WS +{result.WsGrowthMB}MB  Priv +{result.PrivGrowthMB}MB");
        Console.ResetColor();
        Console.WriteLine($"  Actions    : {result.OpenCount} opens ({result.OpenOk} ok), {result.CloseCount} closes ({result.CloseOk} ok)");

        if (result.PerCycleKB != 0)
        {
            var perCycleColor = result.PerCycleKB > 100 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = perCycleColor;
            Console.WriteLine($"  Per cycle  : ~{result.PerCycleKB} KB/cycle");
            Console.ResetColor();
        }
        if (result.TotalContexts > 0 && result.PerContextKB != 0)
        {
            var perCtxColor = result.PerContextKB > 50 ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = perCtxColor;
            Console.WriteLine($"  Per context: ~{result.PerContextKB} KB ({result.TotalContexts} contexts)");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("══════════════════════════════════════════════════════════");
        Console.ResetColor();

        return 0;
    }

    // ── scan ──────────────────────────────────────────────────

    static int ScanCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot scan <window-title> [--save] [--ocr] [--detail] [--depth N]");

        string title = args[0];
        bool save = args.Contains("--save");
        bool useOcr = args.Contains("--ocr");
        bool captureDetails = args.Contains("--detail");
        int depth = 1; // default: form level only
        var depthStr = GetArgValue(args, "--depth");
        if (depthStr != null) int.TryParse(depthStr, out depth);

        // Find target window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Window not found: \"{title}\"");
            Console.ResetColor();
            return 1;
        }

        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\" ({win.ClassName}) {win.Rect.Width}x{win.Rect.Height}");
        Console.WriteLine();

        // Check for existing profile
        var profileStore = new ProfileStore();
        var match = profileStore.FindByMatch(win.ClassName, "");
        string? profileName = null;
        AppProfile? existingProfile = null;

        // Get process name for profile matching
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
        string processName = "";
        try { processName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

        if (match == null && !string.IsNullOrEmpty(processName))
            match = profileStore.FindByMatch("", processName);

        if (match != null)
        {
            profileName = match.Value.name;
            existingProfile = match.Value.profile;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Profile: {profileName} (matched, scan #{existingProfile.ScanCount + 1})");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Profile: (new — use --save to create)");
            Console.ResetColor();
        }
        Console.WriteLine();

        // Run scan
        var scanResult = AppScanner.Scan(win.Handle);

        // Print summary
        var summary = AppScanner.FormatSummary(scanResult, profileName);
        Console.Write(summary);

        // Determine profile name early (needed for OCR + exp DB)
        if (profileName == null && (save || useOcr))
            profileName = ProfileStore.GenerateProfileName(scanResult);

        // Experience DB
        var expDir = profileName != null
            ? Path.Combine(profileStore.ProfileDir, $"{profileName}_exp")
            : null;
        ExperienceDb? expDb = expDir != null ? new ExperienceDb(expDir) : null;

        // ── OCR learning pass (--ocr) ──
        if (useOcr && expDb != null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── OCR Learning ────────────────────────");
            Console.ResetColor();

            // Initialize Simple OCR
            SimpleOcrAnalyzer? simpleOcr = null;
            try
            {
                var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                var primaryLang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                simpleOcr = new SimpleOcrAnalyzer(primaryLanguage: primaryLang, confidenceThreshold: 0.5f);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  OCR init failed: {ex.Message}");
                Console.ResetColor();
            }

            if (simpleOcr != null)
            {
                try
                {
                    // Bridge: SimpleOcrAnalyzer.RecognizeAll → OcrWordInfo[]
                    // (avoids WKAppBot.Vision dependency in Win32 project)
                    async Task<IReadOnlyList<OcrWordInfo>> OcrBridge(System.Drawing.Bitmap bmp)
                    {
                        var ocrFull = await simpleOcr.RecognizeAll(bmp);
                        return ocrFull.Words.Select(w => new OcrWordInfo
                        {
                            Text = w.Text,
                            X = w.X,
                            Y = w.Y,
                            Width = w.Width,
                            Height = w.Height,
                        }).ToArray();
                    }

                    var ocrResult = AppScanner.LearnFormsWithOcr(
                        scanResult, expDb, OcrBridge,
                        onProgress: (formId, count) =>
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"\r  Scanning [{formId}]... {count} controls learned");
                            Console.ResetColor();
                        },
                        captureDetails: captureDetails
                    ).GetAwaiter().GetResult();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  {ocrResult}");
                    Console.ResetColor();

                    foreach (var err in ocrResult.Errors)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Warning: {err}");
                        Console.ResetColor();
                    }

                    // ── Control Detail Cache (--detail) ──
                    if (captureDetails && ocrResult.DetailScreenshots > 0)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("── Control Detail Cache ────────────────");
                        Console.ResetColor();

                        // Per-form stats
                        foreach (var formGroup2 in scanResult.Forms
                            .Where(f => f.FormId != null && f.IsVisible)
                            .GroupBy(f => f.FormId!))
                        {
                            var fid = formGroup2.Key;
                            var fExp = expDb.GetForm(fid);
                            if (fExp == null) continue;
                            var ctrlDir = Path.Combine(expDb.ExpDir, $"form_{fid}", "controls");
                            if (Directory.Exists(ctrlDir))
                            {
                                var cidDirs = Directory.GetDirectories(ctrlDir);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"  [{fid}] {fExp.Controls.Count} controls → {cidDirs.Length} screenshots saved");
                                Console.ResetColor();
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  Total: {ocrResult.DetailScreenshots} screenshots, {ocrResult.DetailTextChanges} text history entries");
                        Console.ResetColor();
                    }

                    // ── Text Snapshot Collection (Puppet Pattern) ──
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("── Text Snapshot (Puppet Pattern) ─────");
                    Console.ResetColor();

                    int snapshotCount = 0;
                    var formGroupsForSnapshot = scanResult.Forms
                        .Where(f => f.FormId != null && f.IsVisible && f.Rect.Width > 50 && f.Rect.Height > 50)
                        .GroupBy(f => f.FormId!)
                        .ToList();

                    foreach (var snapGroup in formGroupsForSnapshot)
                    {
                        var snapFormId = snapGroup.Key;
                        var snapForm = snapGroup.First();

                        try
                        {
                            var textLines = AppScanner.CollectFormTextSnapshot(
                                snapForm.Handle, snapForm.Rect, maxDepth: 4);

                            if (textLines.Count > 0)
                            {
                                expDb.AddTextSnapshot(snapFormId, textLines);
                                snapshotCount++;

                                var formExp = expDb.GetForm(snapFormId);
                                if (formExp?.PuppetPattern != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"  [{snapFormId}] Puppet pattern built ({textLines.Count} lines, scan #{formExp.PuppetScanCount})");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine($"  [{snapFormId}] {textLines.Count} text lines collected (need 1 more scan for pattern)");
                                    Console.ResetColor();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  [{snapFormId}] Snapshot failed: {ex.Message}");
                            Console.ResetColor();
                        }
                    }

                    if (snapshotCount > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  {snapshotCount} text snapshots collected");
                        Console.ResetColor();
                    }

                    // Save experience DB (includes text snapshots + puppet patterns)
                    expDb.SaveAll();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  Experience saved: {expDir}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  OCR error: {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    simpleOcr.Dispose();
                }
            }
        }

        // Print experience DB stats (with fingerprint + keywords)
        if (expDb != null)
        {
            Console.WriteLine();
            Console.WriteLine(expDb.GetStatsString());

            // Show fingerprint + keywords for each form type
            foreach (var (formId, formExp) in expDb.GetAllForms())
            {
                var ctrlCount = formExp.Controls.Count;
                var fpStr = formExp.FingerprintHash != null ? $"fp={formExp.FingerprintHash}" : "fp=(none)";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"  [{formId}] {formExp.FormName}: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{ctrlCount} controls, ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(fpStr);

                if (formExp.OcrKeywords != null && formExp.OcrKeywords.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    var kwList = string.Join(", ", formExp.OcrKeywords.Take(15));
                    var more = formExp.OcrKeywords.Count > 15 ? $" +{formExp.OcrKeywords.Count - 15} more" : "";
                    Console.WriteLine($"         keywords: [{kwList}]{more}");
                }
                if (formExp.PuppetPattern != null)
                {
                    var pLines = formExp.PuppetPattern.Split('\n');
                    int totalLines = pLines.Length;
                    int dynamicLines = pLines.Count(l => l.Trim() == "{*}");
                    int fixedLines = totalLines - dynamicLines;
                    int wildcards = pLines.Sum(l => l.Split("{*}").Length - 1);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"         puppet: {fixedLines} fixed + {dynamicLines} dynamic lines, {wildcards} fields (scan #{formExp.PuppetScanCount})");
                }
                Console.ResetColor();
            }
        }

        // ── Save profile ──
        if (save)
        {
            AppProfile profile;
            if (existingProfile != null)
            {
                // Update existing profile
                profile = existingProfile;
                profile.ScanCount++;
                profile.UpdatedAt = DateTime.UtcNow;

                // Merge new form types
                var newFormGroups = scanResult.Forms
                    .Where(f => f.FormId != null)
                    .GroupBy(f => f.FormId!);

                foreach (var g in newFormGroups)
                {
                    if (!profile.FormTypes.ContainsKey(g.Key))
                    {
                        var first = g.First();
                        profile.FormTypes[g.Key] = new FormTypeDefinition
                        {
                            Name = first.FormName ?? g.Key,
                            FrameClass = first.ClassName,
                            ScanCount = 1,
                        };
                    }
                    else
                    {
                        profile.FormTypes[g.Key].ScanCount++;
                    }

                    // Copy fingerprint hash from experience DB
                    var formExp = expDb?.GetForm(g.Key);
                    if (formExp?.FingerprintHash != null)
                        profile.FormTypes[g.Key].FingerprintHash = formExp.FingerprintHash;
                }
            }
            else
            {
                // Create new profile from scan (with fingerprint hashes from experience DB)
                profile = ProfileStore.FromScanResult(scanResult, expDb);
            }

            profileStore.Save(profileName!, profile);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Profile saved: {Path.Combine(profileStore.ProfileDir, profileName + ".json")}");
            Console.ResetColor();
        }

        return 0;
    }

    // ── click ──────────────────────────────────────────────────

    static int ClickCommand(string[] args)
    {
        if (args.Length < 2)
            return Error(@"Usage: appbot click <window-title> <form-id> [button-text]
  Finds a specific MDI form and clicks a button inside it.
  If button-text is omitted, lists all buttons in the form.

Examples:
  appbot click ""영웅문"" 4051              # list all buttons in [4051] 캐치 실전매매
  appbot click ""영웅문"" 4051 ""매매시작""    # click the 매매시작 button
  appbot click ""투혼"" 1101 --cid 3785    # click button by control ID
  appbot click ""영웅문"" 4051 --list-all   # list ALL controls (buttons, combos, edits)
  appbot click ""영웅문"" 4051 --combo 1 0  # select item 0 in combo #1, then click
  appbot click ""영웅문"" 4051 ""매매시작"" --combo 1 0 --combo 2 0  # combos then click");

        string title = args[0];
        string targetFormId = args[1];
        string? buttonText = args.Length >= 3 && !args[2].StartsWith("--") ? args[2] : null;
        int? targetCid = int.TryParse(GetArgValue(args, "--cid"), out var cid) ? cid : null;
        bool dryRun = args.Contains("--dry");
        bool listAll = args.Contains("--list-all");
        int maxDepth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 4;

        // Parse --combo N INDEX pairs (e.g., --combo 1 0 --combo 2 0)
        var comboSelections = new List<(int comboIndex, int itemIndex)>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--combo" && i + 2 < args.Length)
            {
                if (int.TryParse(args[i + 1], out var ci) && int.TryParse(args[i + 2], out var ii))
                    comboSelections.Add((ci, ii));
            }
        }

        // Find target window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Check elevation: target app elevated but we're not?
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        var targetElevated = NativeMethods.IsProcessElevated(targetPid);
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();

        if ((targetElevated == true || targetElevated == null) && !weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠ Target process (pid={targetPid}) is elevated (admin).");
            Console.WriteLine($"  ⚠ This process is NOT elevated → SendInput/SetCursorPos will be blocked by UIPI.");
            Console.ResetColor();

            // Auto-relaunch as admin
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  → Re-launching as administrator...");
            Console.ResetColor();
            Console.Out.Flush();

            try
            {
                // Find the .exe (not .dll) — dotnet publish creates both
                var exePath = Environment.ProcessPath ?? "wkappbot.exe";
                if (exePath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    exePath = Path.ChangeExtension(exePath, ".exe");

                var exeDir = Path.GetDirectoryName(exePath) ?? ".";
                var escapedArgs = string.Join(" ", args.Select(a => a.Contains(' ') ? $"\"{a}\"" : a));

                // Use cmd /c to set DOTNET_ROOT and run, so output goes to a temp file we can read
                var resultFile = Path.Combine(exeDir, "logs", "_elevated_result.txt");
                var cmdLine = $"/c set \"DOTNET_ROOT={Environment.GetEnvironmentVariable("DOTNET_ROOT")}\" && " +
                              $"\"{exePath}\" click {escapedArgs} > \"{resultFile}\" 2>&1";

                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmdLine,
                    WorkingDirectory = exeDir,
                    UseShellExecute = true,
                    Verb = "runas",  // triggers UAC
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                var proc = Process.Start(psi);
                proc?.WaitForExit();

                // Show the elevated process's output
                if (File.Exists(resultFile))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  ── Elevated process output ──");
                    Console.ResetColor();
                    Console.Write(File.ReadAllText(resultFile));
                }

                return proc?.ExitCode ?? 1;
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // user cancelled UAC
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ UAC cancelled. Cannot click elevated app without admin privileges.");
                Console.ResetColor();
                return 1;
            }
        }

        if (weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  ✓ Running elevated");
            Console.ResetColor();
            Console.WriteLine(" — physical mouse input enabled");
        }

        // Scan to find MDI forms
        var scanResult = AppScanner.Scan(win.Handle);

        // Find the specific form by form_id
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found. Available forms:");
            Console.ResetColor();
            foreach (var f in scanResult.Forms.Where(f => f.FormId != null).GroupBy(f => f.FormId!))
            {
                var first = f.First();
                Console.WriteLine($"  [{first.FormId}] {first.FormName}");
            }
            return 1;
        }

        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName} ({targetForm.Rect.Width}x{targetForm.Rect.Height})");
        Console.WriteLine();

        // Recursively find all controls in this form
        var allControls = new List<(WindowInfo Info, int Depth, string Path)>();
        FindControlsRecursive(targetForm.Handle, "", 0, maxDepth, allControls);

        // Separate by type
        var allButtons = allControls.Where(c => c.Info.ClassName == "Button").ToList();
        var allCombos = allControls.Where(c => c.Info.ClassName == "ComboBox").ToList();

        // --list-all: show every control
        if (listAll)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── All Controls in [{targetFormId}] ({allControls.Count}) ────────────────────");
            Console.ResetColor();
            foreach (var (ctrl, depth, path) in allControls)
            {
                var txt = !string.IsNullOrEmpty(ctrl.Title) ? $" \"{ctrl.Title}\"" : "";
                var vis = ctrl.IsVisible ? "" : " [hidden]";
                var color = ctrl.ClassName switch
                {
                    "Button" => ConsoleColor.Yellow,
                    "ComboBox" => ConsoleColor.Blue,
                    "Edit" => ConsoleColor.Green,
                    "Static" => ConsoleColor.DarkGray,
                    _ => ConsoleColor.Gray
                };
                Console.ForegroundColor = color;
                Console.Write($"    [{ctrl.ClassName}]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" cid={ctrl.ControlId,-6}{txt} {ctrl.Rect.Width}x{ctrl.Rect.Height} @({ctrl.Rect.Left},{ctrl.Rect.Top}){vis}");
                if (!string.IsNullOrEmpty(path)) Console.Write($" [{path}]");
                Console.WriteLine();
            }
            Console.ResetColor();

            // Show combo box details
            if (allCombos.Count > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"── ComboBoxes ({allCombos.Count}) ────────────────────");
                Console.ResetColor();
                for (int ci = 0; ci < allCombos.Count; ci++)
                {
                    var combo = allCombos[ci].Info;
                    int count = (int)NativeMethods.SendMessageW(combo.Handle, 0x0146 /*CB_GETCOUNT*/, IntPtr.Zero, IntPtr.Zero);
                    int curSel = (int)NativeMethods.SendMessageW(combo.Handle, 0x0147 /*CB_GETCURSEL*/, IntPtr.Zero, IntPtr.Zero);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"  Combo #{ci + 1}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" cid={combo.ControlId} @({combo.Rect.Left},{combo.Rect.Top}) {combo.Rect.Width}x{combo.Rect.Height}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" — {count} items (selected: {(curSel >= 0 ? curSel.ToString() : "none")})");
                    Console.ResetColor();

                    for (int j = 0; j < Math.Min(count, 30); j++)
                    {
                        int len = (int)NativeMethods.SendMessageW(combo.Handle, 0x0149 /*CB_GETLBTEXTLEN*/, (IntPtr)j, IntPtr.Zero);
                        if (len > 0 && len < 1024)
                        {
                            var sb = new StringBuilder(len + 2);
                            NativeMethods.SendMessageW(combo.Handle, 0x0148 /*CB_GETLBTEXT*/, (IntPtr)j, sb);
                            var marker = j == curSel ? " ◀" : "";
                            Console.ForegroundColor = j == curSel ? ConsoleColor.Yellow : ConsoleColor.Gray;
                            Console.WriteLine($"    [{j}] {sb}{marker}");
                            Console.ResetColor();
                        }
                    }
                }
            }

            return 0;
        }

        if (allButtons.Count == 0 && comboSelections.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  No buttons found in this form.");
            Console.ResetColor();

            // Also try OCR to show what's visible
            Console.WriteLine("\n  Visible child controls:");
            var children = WindowFinder.GetChildrenZOrder(targetForm.Handle);
            foreach (var c in children.Take(30))
            {
                var vis = c.IsVisible ? "" : " [hidden]";
                var txt = !string.IsNullOrEmpty(c.Title) ? $" \"{c.Title}\"" : "";
                Console.WriteLine($"    [{c.ClassName}] cid={c.ControlId} {c.Rect.Width}x{c.Rect.Height}{txt}{vis}");
            }
            return 1;
        }

        // Display all found buttons
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"── Buttons in [{targetFormId}] ({allButtons.Count}) ────────────────────");
        Console.ResetColor();

        WindowInfo? matchedButton = null;

        foreach (var (btn, depth, path) in allButtons)
        {
            var txt = !string.IsNullOrEmpty(btn.Title) ? btn.Title : "(no text)";
            var size = $"{btn.Rect.Width}x{btn.Rect.Height}";
            var vis = btn.IsVisible ? "" : " [hidden]";

            // Check if this button matches the target
            bool isMatch = false;
            if (targetCid.HasValue && btn.ControlId == targetCid.Value)
                isMatch = true;
            else if (buttonText != null && !string.IsNullOrEmpty(btn.Title) && btn.Title.Contains(buttonText))
                isMatch = true;

            if (isMatch)
            {
                matchedButton = btn;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("  ▶ ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("    ");
            }

            Console.ForegroundColor = isMatch ? ConsoleColor.White : ConsoleColor.Gray;
            Console.Write($"cid={btn.ControlId,-6}");
            Console.ForegroundColor = isMatch ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
            Console.Write($" \"{txt}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {size} @({btn.Rect.Left},{btn.Rect.Top}){vis}");
            if (!string.IsNullOrEmpty(path))
                Console.Write($" [{path}]");
            Console.WriteLine();
            Console.ResetColor();
        }

        // ── Process combo selections BEFORE button click ──
        if (comboSelections.Count > 0 && allCombos.Count > 0)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── ComboBox Selections ────────────────────");
            Console.ResetColor();

            foreach (var (comboIdx, itemIdx) in comboSelections)
            {
                if (comboIdx < 1 || comboIdx > allCombos.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✗ Combo #{comboIdx} not found (have {allCombos.Count} combos)");
                    Console.ResetColor();
                    continue;
                }
                var combo = allCombos[comboIdx - 1].Info;
                int count = (int)NativeMethods.SendMessageW(combo.Handle, 0x0146 /*CB_GETCOUNT*/, IntPtr.Zero, IntPtr.Zero);
                if (itemIdx < 0 || itemIdx >= count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✗ Combo #{comboIdx}: item [{itemIdx}] out of range (have {count} items)");
                    Console.ResetColor();
                    continue;
                }

                // Get item text for display
                int len = (int)NativeMethods.SendMessageW(combo.Handle, 0x0149 /*CB_GETLBTEXTLEN*/, (IntPtr)itemIdx, IntPtr.Zero);
                var itemText = "(?)";
                if (len > 0 && len < 1024)
                {
                    var sb = new StringBuilder(len + 2);
                    NativeMethods.SendMessageW(combo.Handle, 0x0148 /*CB_GETLBTEXT*/, (IntPtr)itemIdx, sb);
                    itemText = sb.ToString();
                }

                // Select the item
                NativeMethods.SendMessageW(combo.Handle, 0x014E /*CB_SETCURSEL*/, (IntPtr)itemIdx, IntPtr.Zero);
                Thread.Sleep(100);

                // Notify parent (CBN_SELCHANGE = 1)
                int notifyCode = 1; // CBN_SELCHANGE
                int comboControlId = NativeMethods.GetDlgCtrlID(combo.Handle);
                IntPtr wParam = (IntPtr)((notifyCode << 16) | (comboControlId & 0xFFFF));
                var parent = NativeMethods.GetParent(combo.Handle);
                NativeMethods.SendMessageW(parent, 0x0111 /*WM_COMMAND*/, wParam, combo.Handle);
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"  ✓ Combo #{comboIdx}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" → [{itemIdx}] \"{itemText}\"");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" (cid={comboControlId})");
                Console.ResetColor();
            }
        }

        // If no specific button requested, just list them
        if (buttonText == null && !targetCid.HasValue)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Tip: appbot click \"<title>\" <form-id> \"<button-text>\" to click a button");
            Console.WriteLine("  Tip: appbot click \"<title>\" <form-id> --list-all   to see ALL controls");
            Console.ResetColor();
            return 0;
        }

        // Click the matched button
        if (matchedButton == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var searchDesc = targetCid.HasValue ? $"cid={targetCid}" : $"\"{buttonText}\"";
            Console.WriteLine($"\n  Button {searchDesc} not found in [{targetFormId}]");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine();
        if (dryRun)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [DRY RUN] Would click: \"{matchedButton.Title}\" cid={matchedButton.ControlId}");
            Console.WriteLine($"  Screen pos: ({matchedButton.Rect.Left + matchedButton.Rect.Width/2}, {matchedButton.Rect.Top + matchedButton.Rect.Height/2})");
            Console.ResetColor();
        }
        else
        {
            int cx = matchedButton.Rect.Left + matchedButton.Rect.Width / 2;
            int cy = matchedButton.Rect.Top + matchedButton.Rect.Height / 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  Clicking: \"{matchedButton.Title}\" cid={matchedButton.ControlId}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" @({cx},{cy})");
            Console.ResetColor();
            Console.Write(" ... ");

            // Snapshot: child windows of main + form BEFORE click
            var preMainChildren = new HashSet<IntPtr>(
                WindowFinder.GetChildrenZOrder(win.Handle).Select(c => c.Handle));
            var preFormChildren = new HashSet<IntPtr>(
                WindowFinder.GetChildrenZOrder(targetForm.Handle).Select(c => c.Handle));
            var preFgHwnd = NativeMethods.GetForegroundWindow();
            var preButtonText = matchedButton.Title;

            // Bring MAIN window to front (MDI child can't be foreground independently)
            NativeMethods.SmartSetForegroundWindow(win.Handle);
            Thread.Sleep(300);

            // Re-read button rect (in case window moved during focus switch)
            NativeMethods.GetWindowRect(matchedButton.Handle, out var btnRect);
            cx = (btnRect.Left + btnRect.Right) / 2;
            cy = (btnRect.Top + btnRect.Bottom) / 2;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"(rect: {btnRect.Left},{btnRect.Top}-{btnRect.Right},{btnRect.Bottom}) ");
            Console.ResetColor();

            // ── Click Strategy ──
            // Try message-based click first (no cursor movement).
            // If no reaction detected, fall back to physical mouse click.
            bool usePhysicalMouse = args.Contains("--mouse");
            string clickMethod;

            if (!usePhysicalMouse)
            {
                // Strategy 1: BM_CLICK (0x00F5) — standard button click message
                // Works for standard Win32 Button class, even owner-drawn
                NativeMethods.PostMessageW(matchedButton.Handle, 0x00F5, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(200);

                // Strategy 2: WM_LBUTTONDOWN/UP with local coords
                // Works for custom controls that don't respond to BM_CLICK
                int localX = cx - btnRect.Left;
                int localY = cy - btnRect.Top;
                IntPtr lParam = (IntPtr)((localY << 16) | (localX & 0xFFFF));
                NativeMethods.PostMessageW(matchedButton.Handle, 0x0201, (IntPtr)0x0001, lParam);
                Thread.Sleep(50);
                NativeMethods.PostMessageW(matchedButton.Handle, 0x0202, IntPtr.Zero, lParam);

                clickMethod = "message";
            }
            else
            {
                // Strategy 3: Physical mouse (SendInput) — guaranteed but moves cursor
                MouseInput.Click(cx, cy);
                clickMethod = "mouse";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"clicked!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" [{clickMethod}]");
            Console.ResetColor();

            // Wait for reaction
            Thread.Sleep(500);

            // ── Detect reaction ──
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n── Reaction Check ────────────────────");
            Console.ResetColor();

            bool anyReaction = false;

            // 1. Check for new popup/dialog windows (system-wide top-level)
            var postFgHwnd = NativeMethods.GetForegroundWindow();
            if (postFgHwnd != preFgHwnd && postFgHwnd != targetForm.Handle && postFgHwnd != win.Handle)
            {
                var popupInfo = WindowInfo.FromHwnd(postFgHwnd);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  📢 New foreground window: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[{popupInfo.ClassName}] \"{popupInfo.Title}\" ({popupInfo.Rect.Width}x{popupInfo.Rect.Height})");
                Console.ResetColor();
                ReadDialogContents(postFgHwnd);
                anyReaction = true;
            }

            // 2. Check for new child windows under main window (HTS alerts are often children)
            var postMainChildren = WindowFinder.GetChildrenZOrder(win.Handle);
            foreach (var child in postMainChildren)
            {
                if (!preMainChildren.Contains(child.Handle) && child.IsVisible)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 New child window: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{child.ClassName}] \"{child.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({child.Rect.Width}x{child.Rect.Height})");
                    Console.ResetColor();
                    anyReaction = true;
                }
            }

            // 3. Check for new children under the form (in-form popups)
            var postFormChildren = WindowFinder.GetChildrenZOrder(targetForm.Handle);
            foreach (var child in postFormChildren)
            {
                if (!preFormChildren.Contains(child.Handle) && child.IsVisible)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 New form child: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{child.ClassName}] \"{child.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({child.Rect.Width}x{child.Rect.Height})");
                    Console.ResetColor();
                    anyReaction = true;
                }
            }

            // 4. Check if button text changed (toggle buttons like 매매시작→매매중지)
            var postButtonText = WindowFinder.GetWindowText(matchedButton.Handle);
            if (postButtonText != preButtonText)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  🔄 Button text changed: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\"{preButtonText}\" → \"{postButtonText}\"");
                Console.ResetColor();
                anyReaction = true;
            }

            // 5. Check if any MessageBox-like popup appeared (same process, any window)
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint clickTargetPid);
            var topWindows = WindowFinder.FindByTitle(""); // all visible windows
            foreach (var tw in topWindows)
            {
                NativeMethods.GetWindowThreadProcessId(tw.Handle, out uint twPid);
                if (twPid == clickTargetPid && !preMainChildren.Contains(tw.Handle)
                    && tw.Handle != win.Handle && tw.Handle != postFgHwnd
                    && tw.IsVisible && tw.Rect.Width > 50 && tw.Rect.Height > 30)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 Popup: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{tw.ClassName}] \"{tw.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({tw.Rect.Width}x{tw.Rect.Height})");
                    Console.ResetColor();
                    ReadDialogContents(tw.Handle);
                    anyReaction = true;
                }
            }

            if (!anyReaction)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  (no visible reaction detected — button may require specific state)");
                Console.ResetColor();
            }
        }

        return 0;
    }

    /// <summary>
    /// Dump all Win32 children of a specific MDI form as a tree.
    /// Shows EVERY control with class, CID, text, rect — for debugging custom MFC controls.
    /// </summary>
    /// <summary>
    /// Multi-step action: click MFC custom combos (AfxWnd+Edit) to open dropdown,
    /// select first item, then click a button. Designed for HTS trading forms.
    /// Usage: appbot do &lt;window-title&gt; &lt;form-id&gt; &lt;button-text&gt; [--delay N]
    /// </summary>
    static int DoCommand(string[] args)
    {
        if (args.Length < 3)
            return Error(@"Usage: appbot do <window-title> <form-id> <button-text> [--delay N]
  Selects first item in all MFC custom combos, then clicks a button.

Examples:
  appbot do ""영웅문"" 4051 ""매매시작""          # select combos + click 매매시작
  appbot do ""영웅문"" 4051 ""매매시작"" --delay 500  # custom delay between steps");

        string title = args[0];
        string targetFormId = args[1];
        string buttonText = args[2];
        int stepDelay = int.TryParse(GetArgValue(args, "--delay"), out var sd) ? sd : 300;

        // Find window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Elevation check
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();
        if (!weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ✗ Not elevated — physical mouse click requires admin. Re-run as admin.");
            Console.ResetColor();
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("  ✓ Elevated");
        Console.ResetColor();
        Console.WriteLine(" — physical mouse enabled");

        // Load ExperienceDb from matching profile (best-effort)
        ExperienceDb? expDb = null;
        try
        {
            var profileStore = new ProfileStore();
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid2);
            string procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid2).ProcessName; } catch { }
            var match = profileStore.FindByMatch(win.ClassName, "")
                ?? (!string.IsNullOrEmpty(procName) ? profileStore.FindByMatch("", procName) : null);
            if (match != null)
            {
                var expDir = Path.Combine(profileStore.ProfileDir, $"{match.Value.name}_exp");
                expDb = new ExperienceDb(expDir);
            }
        }
        catch { /* best-effort — proceed without experience DB */ }

        // Find form
        var scanResult = AppScanner.Scan(win.Handle);
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found.");
            Console.ResetColor();
            return 1;
        }
        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName}");
        Console.WriteLine();

        // Find the target button first (매매시작)
        var allControls = new List<(WindowInfo Info, int Depth, string Path)>();
        FindControlsRecursive(targetForm.Handle, "", 0, 6, allControls);
        var allButtons = allControls.Where(c =>
            c.Info.ClassName == "Button" && !string.IsNullOrEmpty(c.Info.Title) && c.Info.Title.Contains(buttonText)).ToList();

        if (allButtons.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ Button \"{buttonText}\" not found in form [{targetFormId}]");
            Console.ResetColor();
            return 1;
        }
        var targetButton = allButtons[0].Info;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  Found button: \"{targetButton.Title}\" @({targetButton.Rect.Left},{targetButton.Rect.Top})");
        Console.ResetColor();

        // ── Step 1: Find MFC custom combos ──
        // Pattern: AfxWnd parent containing an enabled Edit child, small size, above the button
        // Direct Win32 tree walk (not UIA) to find these MFC custom controls
        var mfcCombos = new List<WindowInfo>();
        FindMfcCombos(targetForm.Handle, targetButton.Rect, mfcCombos, 0, 6);

        // Sort by X position (left to right)
        mfcCombos.Sort((a, b) => a.Rect.Left.CompareTo(b.Rect.Left));

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"── Action Sequence ({mfcCombos.Count} combos + 1 button) ────────────────────");
        Console.ResetColor();

        if (mfcCombos.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  No MFC custom combos found above button. Skipping to button click.");
            Console.ResetColor();
        }

        // Bring window to front for physical mouse
        NativeMethods.SmartSetForegroundWindow(win.Handle);
        Thread.Sleep(stepDelay);

        // ── Process each combo ──
        // Click the EDIT inside the AfxWnd (more precise than clicking container)
        int comboNum = 0;
        foreach (var combo in mfcCombos)
        {
            comboNum++;

            // Find the Edit child inside this combo container
            var comboChildren = WindowFinder.GetChildrenZOrder(combo.Handle);
            var editChild = comboChildren.FirstOrDefault(c => c.ClassName == "Edit");
            var clickTarget = editChild ?? combo; // fallback to container

            // Re-read rect (window might have moved)
            NativeMethods.GetWindowRect(clickTarget.Handle, out var editRect);
            int cx = (editRect.Left + editRect.Right) / 2;
            int cy = (editRect.Top + editRect.Bottom) / 2;

            // Read current value
            var valSb = new StringBuilder(256);
            NativeMethods.SendMessageW(clickTarget.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, valSb);
            var currentVal = valSb.ToString();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"  [{comboNum}/{mfcCombos.Count}] Combo");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" @({cx},{cy}) {clickTarget.Rect.Width}x{clickTarget.Rect.Height}");
            if (!string.IsNullOrEmpty(currentVal))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" (\"{currentVal}\")");
            }
            Console.ResetColor();
            Console.Write(" → click ... ");

            // Snapshot windows before click
            var preWindows = new HashSet<IntPtr>();
            NativeMethods.EnumWindows((hWnd, _) => { preWindows.Add(hWnd); return true; }, IntPtr.Zero);

            // Click the Edit to open dropdown
            MouseInput.Click(cx, cy);
            Thread.Sleep(stepDelay + 300); // wait for dropdown

            // Capture screenshot to see what happened
            try
            {
                using var bmp = ScreenCapture.CaptureWindow(win.Handle);
                var ssPath = Path.Combine(
                    Path.GetDirectoryName(Environment.ProcessPath) ?? ".",
                    "logs", $"combo{comboNum}_opened.png");
                bmp.Save(ssPath);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[screenshot: {Path.GetFileName(ssPath)}] ");
                Console.ResetColor();
            }
            catch { /* screenshot not critical */ }

            // Detect dropdown: look for NEW top-level windows from same process
            var dropdownHwnd = IntPtr.Zero;
            var newWindows = new List<(IntPtr Handle, string ClassName, RECT Rect)>();

            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (preWindows.Contains(hWnd)) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != targetPid) return true;
                var cls = WindowFinder.GetClassName(hWnd);
                NativeMethods.GetWindowRect(hWnd, out var r);
                if (r.Width > 10 && r.Height > 10)
                    newWindows.Add((hWnd, cls, r));
                return true;
            }, IntPtr.Zero);

            // Also check for new children under combo's grandparent
            var grandParent = NativeMethods.GetParent(NativeMethods.GetParent(combo.Handle));
            if (grandParent != IntPtr.Zero)
            {
                var allDescendants = new List<WindowInfo>();
                FindAllChildrenFlat(grandParent, allDescendants, 0, 4);
                foreach (var desc in allDescendants)
                {
                    if (desc.ClassName == "ListBox" && desc.IsVisible && desc.Rect.Height > 20)
                    {
                        dropdownHwnd = desc.Handle;
                        break;
                    }
                }
            }

            // Pick the best dropdown candidate from new windows
            if (dropdownHwnd == IntPtr.Zero && newWindows.Count > 0)
            {
                // Prefer ListBox, then anything near the combo
                var listBox = newWindows.FirstOrDefault(w => w.ClassName.Contains("ListBox"));
                if (listBox.Handle != IntPtr.Zero)
                    dropdownHwnd = listBox.Handle;
                else
                {
                    // Pick the one closest to the combo
                    var nearest = newWindows.OrderBy(w => Math.Abs(w.Rect.Top - editRect.Bottom)).First();
                    dropdownHwnd = nearest.Handle;
                }
            }

            if (dropdownHwnd != IntPtr.Zero)
            {
                var ddCls = WindowFinder.GetClassName(dropdownHwnd);
                NativeMethods.GetWindowRect(dropdownHwnd, out var ddRect);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"dropdown [{ddCls}] {ddRect.Width}x{ddRect.Height} → ");
                Console.ResetColor();

                // Click first item (top of dropdown + offset)
                int itemX = (ddRect.Left + ddRect.Right) / 2;
                int itemY = ddRect.Top + 12;
                MouseInput.Click(itemX, itemY);
                Thread.Sleep(stepDelay);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("selected first item ✓");
                Console.ResetColor();
            }
            else
            {
                // No popup window → in-place dropdown (MFC custom)
                // The list appears BELOW the edit. Click first item by mouse.
                NativeMethods.GetWindowRect(clickTarget.Handle, out var editRect2);
                int firstItemX = (editRect2.Left + editRect2.Right) / 2;
                int firstItemY = editRect2.Bottom + 14; // first visible item

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"in-place dropdown → click @({firstItemX},{firstItemY}) ... ");
                Console.ResetColor();

                MouseInput.Click(firstItemX, firstItemY);
                Thread.Sleep(stepDelay + 200);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓");
                Console.ResetColor();
            }

            // Small delay to let UI settle before next step
            Thread.Sleep(300);

            // Read new value after selection
            var newValSb = new StringBuilder(256);
            NativeMethods.SendMessageW(clickTarget.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, newValSb);
            var newVal = newValSb.ToString();
            if (newVal != currentVal)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("         value: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\"{currentVal}\" → \"{newVal}\"");
                Console.ResetColor();
            }

            Thread.Sleep(stepDelay);
        }

        // ── Final: Click the button (SmartClickButton with experience DB) ──
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  ▶ Clicking: \"{targetButton.Title}\" ");
        Console.ResetColor();

        SmartClickButton(targetButton.Handle, targetForm.Handle,
            expDb, targetFormId, targetButton.ControlId);

        // Wait and check reaction
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n── Reaction Check ────────────────────");
        Console.ResetColor();

        bool anyReaction = false;
        bool autoConfirm = args.Contains("--confirm");

        var postFgHwnd = NativeMethods.GetForegroundWindow();
        if (postFgHwnd != win.Handle)
        {
            var popupInfo = WindowInfo.FromHwnd(postFgHwnd);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  📢 New foreground: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{popupInfo.ClassName}] \"{popupInfo.Title}\" ({popupInfo.Rect.Width}x{popupInfo.Rect.Height})");
            Console.ResetColor();
            ReadDialogContents(postFgHwnd);
            anyReaction = true;

            // Auto-confirm: click the first Button in the dialog
            if (autoConfirm && popupInfo.ClassName == "#32770")
            {
                ClickFirstButtonInDialog(postFgHwnd, "confirm");
            }
        }

        // Check button text change (매매시작 → 매매중지?)
        var postButtonText = WindowFinder.GetWindowText(targetButton.Handle);
        if (postButtonText != targetButton.Title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  🔄 Button changed: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\"{targetButton.Title}\" → \"{postButtonText}\"");
            Console.ResetColor();
            anyReaction = true;
        }

        // Check for popups from same process
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != targetPid || hWnd == win.Handle || hWnd == postFgHwnd) return true;
            NativeMethods.GetWindowRect(hWnd, out var r);
            if (r.Width < 50 || r.Height < 30) return true;
            var info = WindowInfo.FromHwnd(hWnd);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  📢 Popup: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[{info.ClassName}] \"{info.Title}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({info.Rect.Width}x{info.Rect.Height})");
            Console.ResetColor();
            ReadDialogContents(hWnd);
            anyReaction = true;
            return true;
        }, IntPtr.Zero);

        if (!anyReaction)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  (no visible reaction)");
            Console.ResetColor();
        }

        return 0;
    }

    /// <summary>
    /// Find and click the first Button in a dialog window.
    /// Searches direct children and one level of nested panels.
    /// </summary>
    private static void ClickFirstButtonInDialog(IntPtr hDialog, string label)
    {
        // Find first button: direct children, then one level deeper
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(hDialog, allChildren, 0, 3);
        var buttons = allChildren.Where(c => c.ClassName == "Button" && c.IsVisible && c.Rect.Width > 10)
            .OrderBy(b => b.Rect.Left).ToList();

        if (buttons.Count == 0) return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  ✓ Auto-{label}: ");
        Console.ResetColor();

        SmartClickButton(buttons[0].Handle, hDialog);
    }

    /// <summary>
    /// Smart click: tries strategies in experience-optimized order, checking for window change after each.
    /// Default priority: no cursor movement first → cursor movement last.
    /// With ExperienceDb: reorders by historical success rate.
    /// Detection: checks if dialog closed OR a new modal appeared.
    /// </summary>
    private static bool SmartClickButton(
        IntPtr hButton, IntPtr hDialogOrParent,
        ExperienceDb? expDb = null, string? formId = null, int? controlId = null)
    {
        NativeMethods.GetWindowRect(hButton, out var btnRect);
        int cx = (btnRect.Left + btnRect.Right) / 2;
        int cy = (btnRect.Top + btnRect.Bottom) / 2;
        int localX = (btnRect.Right - btnRect.Left) / 2;
        int localY = (btnRect.Bottom - btnRect.Top) / 2;

        // Strategy implementations keyed by name
        var strategyActions = new Dictionary<string, Action>
        {
            ["bm_click"] = () =>
            {
                NativeMethods.PostMessageW(hButton, 0x00F5 /*BM_CLICK*/, IntPtr.Zero, IntPtr.Zero);
            },
            ["wm_lbutton"] = () =>
            {
                IntPtr lParam = (IntPtr)((localY << 16) | (localX & 0xFFFF));
                NativeMethods.SendMessageW(hButton, 0x0201 /*WM_LBUTTONDOWN*/, (IntPtr)0x0001, lParam);
                Thread.Sleep(80);
                NativeMethods.SendMessageW(hButton, 0x0202 /*WM_LBUTTONUP*/, IntPtr.Zero, lParam);
            },
            ["send_input"] = () =>
            {
                NativeMethods.SetForegroundWindow(hDialogOrParent);
                Thread.Sleep(100);
                MouseInput.MoveTo(cx, cy);
                Thread.Sleep(150);
                var downInput = new INPUT[1];
                downInput[0].type = INPUT.INPUT_MOUSE;
                downInput[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
                downInput[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                NativeMethods.SendInput(1, downInput, Marshal.SizeOf<INPUT>());
                Thread.Sleep(80);
                var upInput = new INPUT[1];
                upInput[0].type = INPUT.INPUT_MOUSE;
                upInput[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
                upInput[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                NativeMethods.SendInput(1, upInput, Marshal.SizeOf<INPUT>());
            },
        };

        // Get optimal order from experience DB (or default)
        bool hasExpData = expDb != null && formId != null && controlId != null;
        var order = hasExpData
            ? expDb!.GetBestClickOrder(formId!, controlId!.Value)
            : (IReadOnlyList<string>)new[] { "bm_click", "wm_lbutton", "send_input" };

        // Show experience-based order if it differs from default
        if (hasExpData)
        {
            var ctrl = expDb!.GetControl(formId!, controlId!.Value);
            if (ctrl?.ClickStrategies != null && ctrl.ClickStrategies.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"@({cx},{cy}) [EXP] ");
                var parts = order.Select(s =>
                {
                    if (ctrl.ClickStrategies.TryGetValue(s, out var st))
                        return $"{s}({st.SuccessRate:P0})";
                    return $"{s}(new)";
                });
                Console.Write(string.Join(" → ", parts));
                Console.ResetColor();
                Console.Write("  ");
            }
        }

        // Snapshot: foreground window before clicking
        var fgBefore = NativeMethods.GetForegroundWindow();
        bool isFirst = true;

        foreach (var strategyName in order)
        {
            if (!strategyActions.TryGetValue(strategyName, out var action))
                continue;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(isFirst ? $"@({cx},{cy}) {strategyName}" : $" → {strategyName}");
            Console.ResetColor();
            isFirst = false;

            action();
            bool success = CheckDialogGone(hDialogOrParent, fgBefore, strategyName);

            // Record result in experience DB
            if (hasExpData)
                expDb!.RecordClickStrategy(formId!, controlId!.Value, strategyName, success);

            if (success)
            {
                if (hasExpData) expDb!.SaveAll();
                return true;
            }
        }

        // Record overall failure
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" ✗ (all strategies failed)");
        Console.ResetColor();
        if (hasExpData) expDb!.SaveAll();
        return false;
    }

    /// <summary>
    /// Robust check: did the dialog close or did a new modal appear?
    /// Waits, then triple-checks: IsWindow + IsWindowVisible + foreground change + new modal.
    /// </summary>
    private static bool CheckDialogGone(IntPtr hDialog, IntPtr fgBefore, string strategyName)
    {
        Thread.Sleep(400);

        // Check 1: Is the window handle still valid?
        bool hwndValid = NativeMethods.IsWindow(hDialog);
        bool hwndVisible = hwndValid && NativeMethods.IsWindowVisible(hDialog);

        // Check 2: Did the foreground window change? (new modal may have appeared)
        var fgNow = NativeMethods.GetForegroundWindow();
        bool fgChanged = fgNow != fgBefore && fgNow != hDialog;

        // Check 3: Re-verify after another short wait (avoid transient states)
        if (!hwndValid || !hwndVisible)
        {
            Thread.Sleep(200);
            hwndValid = NativeMethods.IsWindow(hDialog);
            hwndVisible = hwndValid && NativeMethods.IsWindowVisible(hDialog);
        }

        bool dialogGone = !hwndValid || !hwndVisible;
        bool newModalAppeared = fgChanged && fgNow != IntPtr.Zero;

        if (dialogGone)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" ✓ [{strategyName}: dialog closed]");
            Console.ResetColor();
            return true;
        }

        if (newModalAppeared)
        {
            // A new window appeared on top — the click probably worked and triggered something
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.GetWindowTextW(fgNow, sb, 256);
            string newTitle = sb.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" ✓ [{strategyName}: new modal] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\"{newTitle}\"");
            Console.ResetColor();
            return true;
        }

        return false; // no change detected, try next strategy
    }

    /// <summary>
    /// Click a button in a top-level dialog by title match.
    /// Usage: appbot dialog-click "dialog title" [button-index]
    /// Uses physical mouse click (works with owner-drawn buttons).
    /// </summary>
    static int DialogClickCommand(string[] args)
    {
        if (args.Length < 1)
            return Error(@"Usage: appbot dialog-click <dialog-title> [button-index]
  Finds a dialog by title and clicks a button using physical mouse.
  button-index: 0=first (default), 1=second, etc.");

        string title = args[0];
        int btnIndex = args.Length >= 2 && int.TryParse(args[1], out var bi) ? bi : 0;

        // Find dialog
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Dialog not found: \"{title}\"");
        var dlg = windows[0];
        Console.WriteLine($"Dialog: [{dlg.Handle:X8}] \"{dlg.Title}\" ({dlg.Rect.Width}x{dlg.Rect.Height})");

        // Find all buttons recursively (up to 2 levels)
        var buttons = new List<WindowInfo>();
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(dlg.Handle, allChildren, 0, 3);
        buttons = allChildren.Where(c => c.ClassName == "Button" && c.IsVisible && c.Rect.Width > 10)
            .OrderBy(b => b.Rect.Left).ToList(); // left-to-right order (확인 first)

        if (buttons.Count == 0) return Error("No buttons found in dialog.");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            var txt = !string.IsNullOrEmpty(b.Title) ? $"\"{b.Title}\"" : "(owner-drawn)";
            var marker = i == btnIndex ? " ◀" : "";
            Console.WriteLine($"  [{i}] {txt} {b.Rect.Width}x{b.Rect.Height} @({b.Rect.Left},{b.Rect.Top}){marker}");
        }
        Console.ResetColor();

        if (btnIndex >= buttons.Count)
            return Error($"Button index {btnIndex} out of range (have {buttons.Count})");

        var target = buttons[btnIndex];
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  Clicking [{btnIndex}]: ");
        Console.ResetColor();
        SmartClickButton(target.Handle, dlg.Handle);

        // Check if dialog closed
        if (!NativeMethods.IsWindowVisible(dlg.Handle))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Dialog closed ✓");
            Console.ResetColor();
        }

        return 0;
    }

    static int FormDumpCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: appbot form-dump <window-title> <form-id> [--depth N]");

        string title = args[0];
        string targetFormId = args[1];
        int maxDepth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 6;

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];

        var scanResult = AppScanner.Scan(win.Handle);
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found.");
            Console.ResetColor();
            foreach (var f in scanResult.Forms.Where(f => f.FormId != null).GroupBy(f => f.FormId!))
                Console.WriteLine($"  [{f.First().FormId}] {f.First().FormName}");
            return 1;
        }

        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName} ({targetForm.Rect.Width}x{targetForm.Rect.Height})");
        Console.WriteLine();

        DumpFormTree(targetForm.Handle, 0, maxDepth);
        return 0;
    }

    /// <summary>
    /// Find MFC custom combo-like controls: AfxWnd parent with an enabled Edit child,
    /// small size, positioned above the target button. Direct Win32 tree walk.
    /// </summary>
    /// <summary>Flat enumeration of all child windows (for finding dropdown ListBox etc).</summary>
    private static void FindAllChildrenFlat(IntPtr hParent, List<WindowInfo> results, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            results.Add(child);
            FindAllChildrenFlat(child.Handle, results, depth + 1, maxDepth);
        }
    }

    private static void FindMfcCombos(IntPtr hParent, RECT buttonRect, List<WindowInfo> results, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            if (!child.IsVisible) continue;

            // Check if this AfxWnd looks like a combo container
            if (child.ClassName.StartsWith("Afx") &&
                child.Rect.Height >= 20 && child.Rect.Height <= 35 &&
                child.Rect.Width >= 50 && child.Rect.Width <= 200 &&
                child.Rect.Top >= buttonRect.Top - 80 &&
                child.Rect.Top <= buttonRect.Top)
            {
                // Check for Edit child that is enabled
                var grandChildren = WindowFinder.GetChildrenZOrder(child.Handle);
                var edit = grandChildren.FirstOrDefault(c => c.ClassName == "Edit" && NativeMethods.IsWindowEnabled(c.Handle));
                if (edit != null)
                {
                    // Read current text
                    var sb = new StringBuilder(256);
                    NativeMethods.SendMessageW(edit.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, sb);
                    var currentText = sb.ToString();

                    // Skip account number (has dash like "5229-9737") and stock code (cid=32760)
                    if (currentText.Contains("-") && currentText.Length >= 8) goto recurse;
                    if (edit.ControlId == 32760) goto recurse;

                    // Skip disabled containers
                    if (!NativeMethods.IsWindowEnabled(child.Handle)) goto recurse;

                    results.Add(child);
                    continue; // don't recurse into combos
                }
            }

            recurse:
            FindMfcCombos(child.Handle, buttonRect, results, depth + 1, maxDepth);
        }
    }

    private static void DumpFormTree(IntPtr hParent, int level, int maxDepth)
    {
        if (level > maxDepth) return;
        var children = WindowFinder.GetChildrenZOrder(hParent);
        var indent = new string(' ', level * 2);

        foreach (var child in children)
        {
            // Read text via WM_GETTEXT
            var sb = new StringBuilder(512);
            NativeMethods.SendMessageW(child.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)512, sb);
            var text = sb.ToString();
            var textDisplay = string.IsNullOrEmpty(text) ? "" : $" \"{text}\"";

            var vis = child.IsVisible ? "" : " [H]";
            var en = NativeMethods.IsWindowEnabled(child.Handle) ? "" : " [disabled]";

            // Color by class
            var color = child.ClassName switch
            {
                "Button" => ConsoleColor.Yellow,
                "ComboBox" => ConsoleColor.Blue,
                "Edit" => ConsoleColor.Green,
                "Static" => ConsoleColor.DarkGray,
                "ListBox" => ConsoleColor.Cyan,
                _ when child.ClassName.StartsWith("Afx") => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };

            Console.ForegroundColor = color;
            Console.Write($"{indent}[{child.ClassName}]");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" cid={child.ControlId}");
            if (!string.IsNullOrEmpty(textDisplay))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(textDisplay);
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {child.Rect.Width}x{child.Rect.Height} @({child.Rect.Left},{child.Rect.Top}){vis}{en}");
            Console.WriteLine();
            Console.ResetColor();

            DumpFormTree(child.Handle, level + 1, maxDepth);
        }
    }

    /// <summary>
    /// Recursively find all controls inside a window (buttons, combos, edits, etc).
    /// Buttons are found at leaf level; ComboBox is NOT recursed into (it has internal Edit+ListBox).
    /// </summary>
    private static void FindControlsRecursive(
        IntPtr hParent, string path, int depth, int maxDepth,
        List<(WindowInfo Info, int Depth, string Path)> results)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            var childPath = string.IsNullOrEmpty(path) ? child.ClassName : $"{path}>{child.ClassName}";

            // Record interesting control types
            if (child.ClassName is "Button" or "ComboBox" or "Edit" or "Static" or "ListBox"
                or "SysListView32" or "SysTreeView32")
            {
                results.Add((child, depth, childPath));
            }

            // Also record MFC owner-drawn buttons (AfxWnd with small size that look like buttons)
            if (child.ClassName.StartsWith("Afx") && child.Rect.Width > 10 && child.Rect.Height > 10
                && child.Rect.Width < 200 && child.Rect.Height < 60 && child.IsVisible)
            {
                // Check if it has button-like text (from GetWindowText)
                if (!string.IsNullOrEmpty(child.Title))
                    results.Add((child, depth, childPath));
            }

            // Don't recurse into ComboBox (has internal Edit+ListBox children)
            if (child.ClassName != "ComboBox")
            {
                FindControlsRecursive(child.Handle, childPath, depth + 1, maxDepth, results);
            }
        }
    }

    /// <summary>
    /// Read and display contents of a dialog/popup window.
    /// Uses Win32 GetWindowText first, falls back to OCR for owner-drawn content.
    /// </summary>
    private static void ReadDialogContents(IntPtr hDialog)
    {
        var children = WindowFinder.GetChildrenZOrder(hDialog);
        bool foundText = false;

        // Pass 1: Try Win32 GetWindowText on children
        foreach (var child in children)
        {
            var text = WindowFinder.GetWindowText(child.Handle);
            if (string.IsNullOrWhiteSpace(text)) continue;

            if (child.ClassName == "Static")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("         message: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\"{text}\"");
                Console.ResetColor();
                foundText = true;
            }
            else if (child.ClassName == "Button")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"         button: [{text}]");
                Console.ResetColor();
            }
        }

        // Recurse one level for nested dialogs
        foreach (var child in children)
        {
            if (child.ClassName == "#32770" || child.ClassName.StartsWith("Afx"))
            {
                var subChildren = WindowFinder.GetChildrenZOrder(child.Handle);
                foreach (var sc in subChildren)
                {
                    var text = WindowFinder.GetWindowText(sc.Handle);
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    if (sc.ClassName == "Static")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("         message: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\"{text}\"");
                        Console.ResetColor();
                        foundText = true;
                    }
                    else if (sc.ClassName == "Button")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"         button: [{text}]");
                        Console.ResetColor();
                    }
                }
            }
        }

        // Pass 2: If no Static text found, try OCR on the dialog window
        if (!foundText)
        {
            try
            {
                using var bmp = ScreenCapture.CaptureWindow(hDialog);
                if (bmp.Width > 20 && bmp.Height > 20)
                {
                    var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                    var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                    using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.5f);

                    var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                    if (result.Words.Count > 0)
                    {
                        // Filter out button text, keep dialog message text only
                        var buttonTexts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (var child in children)
                        {
                            var bt = WindowFinder.GetWindowText(child.Handle);
                            if (!string.IsNullOrWhiteSpace(bt)) buttonTexts.Add(bt.Trim());
                        }

                        var messageWords = result.Words
                            .Where(w => !buttonTexts.Contains(w.Text.Trim()))
                            .Select(w => w.Text)
                            .ToList();

                        if (messageWords.Count > 0)
                        {
                            var ocrMsg = string.Join(" ", messageWords);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("         OCR: ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\"{ocrMsg}\"");
                            Console.ResetColor();
                        }
                    }
                }
            }
            catch { /* OCR not critical */ }
        }
    }

    // ── helpers ─────────────────────────────────────────────────

    static int PrintUsage()
    {
        Console.WriteLine(@"
WKAppBot - Windows App Automation Test Framework

Usage:
  appbot run <scenario.yaml> [-v] [--no-watch] [--watch-interval N] [--report <dir>]
  appbot validate <scenario.yaml>
  appbot inspect <window-title> [--depth N]
  appbot focus [--title <text>] [--delay N] [--depth N] [--win32] [-b|--buttons]
  appbot watch [--duration N] [--live] [--win32] [--interval N] [--save file]
  appbot capture <window-title> [-o output.png]
  appbot hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only]
  appbot scan <window-title> [--save] [--ocr] [--detail] [--depth N]

Commands:
  run        Run a test scenario (with passive [WATCH] background tracker)
  validate   Validate a YAML scenario file
  inspect    Dump UI Automation tree of a window (by title)
  focus      Inspect the currently focused window (countdown + dump)
  watch      Real-time element tracking under mouse cursor
  capture    Capture a screenshot of a window
  hts-stress HTS MDI stress test (3 patterns: repeat/memory/ctx-only)
  scan       Scan window structure (auto-classify zones + MDI forms + experience DB)

Run options:
  --no-watch          Disable passive background element watcher
  --watch-interval N  Watcher polling interval in ms (default: 200)

Focus options:
  --title <text>  Find window by title (skip countdown)
  --delay N       Seconds to wait before capturing focus (default: 3)
  --depth N       UIA tree depth (default: 6)
  --win32         Show Win32 child windows list
  -b, --buttons   Show all clickable buttons with AutomationId

Watch options:
  --duration N    Stop after N seconds (default: unlimited, Ctrl+C or Ctrl+Z to stop)
  --live          Single-line overwrite mode (instead of scroll history)
  --win32         Also show Win32 hwnd/class under cursor
  --interval N    Polling interval in ms (default: 150)
  --save <file>   Save log to specific file (default: watch_YYYYMMDD_HHmmss.log)

General options:
  -v, --verbose   Verbose output
  --report <dir>  Generate HTML report in directory
");
        return 0;
    }

    static int Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(msg);
        Console.ResetColor();
        return 1;
    }

    static string? GetArgValue(string[] args, string flag)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == flag) return args[i + 1];
        }
        return null;
    }
}
