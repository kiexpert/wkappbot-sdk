using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

/// <summary>Result of InspectTargetA11yNode: element bounds + tail tip + DPI scale.</summary>
internal record A11yNodeInfo(
    System.Drawing.Rectangle ElementRect,  // full BoundingRectangle of the a11y element
    System.Drawing.Rectangle TipRect,      // caret / text-end / left+20 (for callout tail tip)
    float DpiScale);

// partial class: inspect, focus, watch, capture commands + watch helpers
internal partial class Program
{
    /// <summary>
    /// Prints a prominent skill-search reminder to stdout.
    /// Call this before listing/inspecting results when no specific keyword was supplied,
    /// so any Claude session (even without hook configuration) sees it immediately.
    /// </summary>
    internal static void PrintSkillSearchHint(string? context = null)
    {
        var ctx = context != null ? $" [{context}]" : "";
        Console.WriteLine($"# SKILL SEARCH FIRST{ctx}: wkappbot skill search <keyword>");
        Console.WriteLine("#   Skills cover: Eye/CDP/a11y/grap/guardian/suggest/build -- check before file search.");
    }

    // -- InspectTargetA11yNode: find Edit/target node, print rect/center/DPI to stdout --

    /// <summary>
    /// Finds the target a11y node (Edit by name) in the given window, prints its rect/center/DPI,
    /// and returns the BoundingRectangle. Returns default if not found.
    /// Call this before showing the callout so the tail tip uses the correct coordinates.
    /// </summary>
    internal static A11yNodeInfo? InspectTargetA11yNode(IntPtr hwnd, string elementName = "Message input",
        FlaUI.Core.Definitions.ControlType controlType = FlaUI.Core.Definitions.ControlType.Edit)
    {
        try
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return default;

            var el = root.FindFirst(FlaUI.Core.Definitions.TreeScope.Descendants,
                new FlaUI.Core.Conditions.AndCondition(
                    uia.ConditionFactory.ByControlType(controlType),
                    uia.ConditionFactory.ByName(elementName)));

            if (el == null)
            {
                Console.WriteLine($"[INSPECT] {controlType}(\"{elementName}\") not found in hwnd=0x{hwnd:X}");
                return null;
            }

            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            int cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;

            // DPI scale via user32 GetDpiForWindow (Win10+)
            float dpiScale = 1f;
            try
            {
                var elHwnd = (IntPtr)el.Properties.NativeWindowHandle.ValueOrDefault;
                if (elHwnd == IntPtr.Zero) elHwnd = hwnd;
                uint dpi = NativeMethods.GetDpiForWindow(elHwnd);
                if (dpi > 0) dpiScale = dpi / 96f;
            }
            catch { }

            Console.WriteLine($"[INSPECT] {controlType}(\"{elementName}\") hwnd=0x{hwnd:X}");
            Console.WriteLine($"[INSPECT]   rect   ltwh=({r.X},{r.Y},{r.Width},{r.Height})");
            Console.WriteLine($"[INSPECT]   center px=({cx},{cy})  dip=({cx/dpiScale:F1},{cy/dpiScale:F1})  dpiScale={dpiScale:F2}");

            // Caret rect: if element has TextPattern2, get caret position for tail tip
            System.Drawing.Rectangle caretRect = default;
            try
            {
                var tp2 = el.Patterns.Text2;
                if (tp2.IsSupported)
                {
                    var getCaretRange = tp2.Pattern.GetType().GetMethod("GetCaretRange",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (getCaretRange != null)
                    {
                        var args2 = new object[] { false };
                        var caretRange = getCaretRange.Invoke(tp2.Pattern, args2);
                        if (caretRange != null)
                        {
                            var getBounds = caretRange.GetType().GetMethod("GetBoundingRectangles");
                            var rects = getBounds?.Invoke(caretRange, null) as System.Windows.Rect[];
                            if (rects != null && rects.Length > 0)
                            {
                                var cr = rects[0];
                                caretRect = new System.Drawing.Rectangle((int)cr.X, (int)cr.Y, Math.Max(1, (int)cr.Width), (int)cr.Height);
                                int ccx = caretRect.X + caretRect.Width / 2, ccy = caretRect.Y + caretRect.Height / 2;
                                Console.WriteLine($"[INSPECT]   caret  ltwh=({caretRect.X},{caretRect.Y},{caretRect.Width},{caretRect.Height}) center=({ccx},{ccy})");
                            }
                        }
                    }
                }
            }
            catch { }

            var elemRect = new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height);

            // Tail tip priority:
            // 1. Caret rect (if cursor is active -- most precise)
            // 2. Text end position (if element has text content)
            // 3. Element left+20px (just inside left edge of the node)
            if (caretRect.Height > 0)
            {
                Console.WriteLine($"[INSPECT]   tail=caret");
                return new A11yNodeInfo(elemRect, caretRect, dpiScale);
            }

            // Try text-end position via TextPattern.DocumentRange
            try
            {
                var tp = el.Patterns.Text;
                if (tp.IsSupported)
                {
                    var docRange = tp.Pattern.DocumentRange;
                    var textRects = docRange.GetBoundingRectangles();
                    if (textRects != null && textRects.Length > 0)
                    {
                        var lastRect = textRects[textRects.Length - 1];
                        int tx = (int)(lastRect.X + lastRect.Width), ty = (int)(lastRect.Y + lastRect.Height / 2);
                        Console.WriteLine($"[INSPECT]   tail=text-end px=({tx},{ty})");
                        var tipRect = new System.Drawing.Rectangle(tx, ty - r.Height / 2, 1, r.Height);
                        return new A11yNodeInfo(elemRect, tipRect, dpiScale);
                    }
                }
            }
            catch { }

            // Fallback: 20px inside left edge of element
            Console.WriteLine($"[INSPECT]   tail=left+20");
            var fallbackTip = new System.Drawing.Rectangle(r.X + 20, r.Y, 1, r.Height);
            return new A11yNodeInfo(elemRect, fallbackTip, dpiScale);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[INSPECT] error: {ex.Message}");
            return null;
        }
    }

    // -- find (unified search -- absorbs windows --uia + inspect --filter) --

    static int FindCommand(string[] args)
    {
        if (args.Length == 0 || (args.Length == 1 && args[0].StartsWith("--")))
            return Error("Usage: wkappbot find <keyword> [--deep] [--limit N] [--process <name>]\n" +
                         "  Default: tree output -- prints reachable grap paths (Win32 children + UIA)\n" +
                         "           for each matched window. Copy-paste any path into another a11y command.\n" +
                         "  Path search: find \"윈도우/UIA요소\" -- / separates hierarchy levels.\n" +
                         "    * = any chars within one level, ** = any number of levels.\n" +
                         "    Each segment is implicitly *segment* (contains match).\n" +
                         "  Scoped search: find \"윈도우#UIA스코프\" -- # narrows UIA search root.\n" +
                         "    Combine: find \"영웅문/*실현*#*잔고*\" + keyword after #\n" +
                         "  --deep: Deeper UIA tree search (depth 12, slower but thorough).\n" +
                         "  Examples:\n" +
                         "    find \"Claude\"                       Search everywhere for 'Claude'\n" +
                         "    find \"영웅문*\"                      영웅문 windows -> Win32+UIA grap tree\n" +
                         "    find \"투혼/현재가\"                  투혼 windows -> ... -> 현재가 element\n" +
                         "    find \"투혼/**/현재가\"               투혼 -> any depth -> 현재가\n" +
                         "    find \"*영웅문*#*잔고확인*\"          영웅문 -> UIA scope 잔고확인 -> list elements\n" +
                         "    find \"*영웅문*/*실현*#*잔고*\" 예수금  영웅문/실현* child -> #잔고 scope -> 예수금 검색");

        // Check for scope separators -- '#' = UIA scope, '/' with 2+ segments = Win32 child hierarchy
        string firstArg = args.FirstOrDefault(a => !a.StartsWith("--")) ?? "";
        var (w32segs, uiaP) = GrapHelper.SplitGrap(firstArg);
        if (uiaP != null || w32segs.Length >= 2)
            return FindScopedCommand(args);

        // Default: tree output (Win32 children + UIA path suffixes per matched window).
        // --tree flag remains accepted for backward compatibility but is now implicit.
        return FindTreeCommand(args);
    }

    /// <summary>
    /// Scoped find: "window#uiaScope" [keyword] -- narrow UIA root, then search within.
    /// Without keyword: lists all elements under scope (like inspect).
    /// With keyword: searches for matching elements within scope.
    /// </summary>
    static int FindScopedCommand(string[] args)
    {
        bool hasDeep = args.Contains("--deep");
        int limit = int.TryParse(GetArgValue(args, "--limit"), out var lim) ? lim : 50;

        // First positional arg = grap#scope, second = keyword (optional)
        var positional = args.Where(a => !a.StartsWith("--")).ToList();
        string grapArg = positional[0];
        string? searchKeyword = positional.Count > 1 ? positional[1] : null;

        // Split at first '#': before = Win32 path (/), after = UIA scope (/ and # equivalent)
        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grapArg);
        if (win32Segments.Length == 0)
            return Error("find requires window title: find \"window/child\" or find \"window#scope\"");

        var windows = WindowFinder.FindWindows(win32Segments[0]);
        if (windows.Count == 0) return Error($"Window not found: \"{win32Segments[0]}\"");
        var mainWin = windows[0];
        Console.WriteLine($"Window: [{mainWin.Handle:X8}] \"{mainWin.Title}\"");

        // Elevation auto-detect: delegate to admin proxy if target is elevated
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            mainWin.Handle, "a11y", new[] { "find" }.Concat(args).ToArray());
        if (delegated) return delegateExit;

        // Blocker detection (bug report: find should show blocker info)
        try
        {
            var readiness = CreateInputReadiness();
            var blockerInfo = readiness.DetectBlocker(mainWin.Handle);
            if (blockerInfo != null)
                Console.Error.WriteLine($"[BLOCK] Blocker detected: \"{blockerInfo.Title}\" (class={blockerInfo.ClassName}, hwnd=0x{blockerInfo.Handle.ToInt64():X8})");
        }
        catch { /* best effort */ }

        // Walk Win32 children (segments before '#')
        IntPtr targetHwnd = mainWin.Handle;
        for (int si = 1; si < win32Segments.Length; si++)
        {
            var childMatch = FindChildWindowByPattern(mainWin.Handle, win32Segments[si]);
            if (childMatch != null)
            {
                targetHwnd = childMatch.Value.handle;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"  `- Child: ");
                Console.ResetColor();
                Console.WriteLine($"\"{childMatch.Value.title}\" (hWnd=0x{targetHwnd:X})");
            }
            else
            {
                return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            }
        }

        // UIA scope (after '#')
        using var uia = new FlaUI.UIA3.UIA3Automation();
        uia.ConnectionTimeout = TimeSpan.FromSeconds(5);
        uia.TransactionTimeout = TimeSpan.FromSeconds(5);

        AutomationElement? scoped = uia.FromHandle(targetHwnd);
        if (scoped is null) return Error("UIA root not available.");

        if (!string.IsNullOrEmpty(uiaPath))
        {
            scoped = GrapHelper.FindUiaScope(scoped!, uiaPath);
            if (scoped is null)
                return Error($"UIA scope not found: \"{uiaPath}\"");
        }

        var scopeName = scoped!.Properties.Name.ValueOrDefault ?? "(unnamed)";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"  # Scope: ");
        Console.ResetColor();
        Console.WriteLine($"\"{scopeName}\"");

        // Search within scope
        int maxDepth = hasDeep ? 12 : 6;
        int maxResults = limit;
        int maxVisited = hasDeep ? 3000 : 1000;
        int timeoutMs = hasDeep ? 10000 : 5000;

        var matches = UiaLocator.QuickSearch(scoped!, searchKeyword ?? "",
            maxDepth: maxDepth, maxResults: maxResults, maxVisited: maxVisited, timeoutMs: timeoutMs);

        Console.WriteLine();
        if (matches.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(searchKeyword != null
                ? $"  No elements matching \"{searchKeyword}\" within scope \"{scopeName}\""
                : $"  No elements found within scope \"{scopeName}\"");
            Console.ResetColor();
            return 0;
        }

        string mode = searchKeyword != null ? $"find \"{searchKeyword}\"" : "list all";
        Console.WriteLine($"-- {mode} under #{scopeName} ({matches.Count} results) --");
        foreach (var m in matches)
        {
            var tag = GrapHelper.FormatNodeLabel(m.ControlType, m.AutomationId, m.Name);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  {tag}");
            if (!string.IsNullOrEmpty(m.NamePath))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"  path={m.NamePath}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine($"Total: {matches.Count} (scope=\"{scopeName}\", depth={maxDepth})");
        return 0;
    }

    // -- inspect ------------------------------------------------

    static int InspectCommand(string[] args)
    {
        // End-of-command focus-steal sentinel (restore + auto bug report if stolen).
        using var focusSentinel = new FocusStealSentinel("a11y-inspect");
        if (args.Length == 0)
            return Error("Usage: appbot inspect <window-title>[/<child-pattern>][#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]\n" +
                "  <child-pattern>: MDI/자식 윈도우 glob 매칭 (예: 투혼/**현재가, 투혼/[0600]*)\n" +
                "  #<uia-scope>: UIA 요소 Name 매칭으로 검색 루트 축소 (예: *영웅문*#*잔고확인*)\n" +
                "  --filter: Search entire UIA tree for matching elements (Name/AutomationId/ControlType)\n" +
                "            Supports wildcards (*/?), regex: prefix, or plain substring");

        // Yield to active user: heavy UIA FromHandle + tree dump on Chromium/Electron
        // targets steals foreground. Bail out early if user is mid-typing.
        if (FocusSafe.ShouldYieldToActiveUser(out var idleMs))
            return Error($"[A11Y:INSPECT] user active ({idleMs}ms idle) -- skipping UIA scan to avoid focus steal");

        string rawTitle = args[0];
        if (rawTitle == "*" || rawTitle == "**")
            PrintSkillSearchHint("inspect");
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 5;
        bool win32Mode = args.Contains("--win32");
        bool pathsMode = args.Contains("--paths");
        string? filter = GetArgValue(args, "--filter");

        // Split at first '#': before = Win32 path (/), after = UIA scope (/ and # equivalent)
        var (win32Segments, uiaScopePath) = GrapHelper.SplitGrap(rawTitle);
        if (win32Segments.Length == 0) return Error("Empty grap pattern");

        // Ambiguity-guard + readiness Probe (magnifier) on the root target.
        // Single-target contract: multiple matches -> find-style list + error exit.
        var mainWin = ResolveA11yTarget(win32Segments[0], "inspect");
        if (mainWin == null) return 1;

        // Elevation auto-detect: delegate to admin proxy if target is elevated
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            mainWin.Handle, "inspect", args);
        if (delegated) return delegateExit;

        // Walk Win32 children (segments before '#')
        IntPtr inspectHandle = mainWin.Handle;
        string? matchedFormId = null;
        string? matchedFormTitle = null;

        for (int si = 1; si < win32Segments.Length; si++)
        {
            var childMatch = FindChildWindowByPattern(mainWin.Handle, win32Segments[si]);
            if (childMatch != null)
            {
                inspectHandle = childMatch.Value.handle;
                matchedFormId = childMatch.Value.formId;
                matchedFormTitle = childMatch.Value.title;
            }
            else
            {
                return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            }
        }

        // Display window info
        if (matchedFormTitle != null)
        {
            Console.WriteLine($"Window: {mainWin}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  `- Child: ");
            Console.ResetColor();
            Console.WriteLine($"\"{matchedFormTitle}\" (hWnd=0x{inspectHandle:X})");
        }
        else
        {
            Console.WriteLine($"Window: {mainWin}");
        }

        // [PROCESS] section: Priv/WS memory, handles, threads, GDI/USER objects
        try
        {
            NativeMethods.GetWindowThreadProcessId(inspectHandle, out uint procPid);
            var proc = System.Diagnostics.Process.GetProcessById((int)procPid);
            var privMB  = proc.PrivateMemorySize64 / (1024.0 * 1024.0);
            var wsMB    = proc.WorkingSet64 / (1024.0 * 1024.0);
            var handles = proc.HandleCount;
            var threads = proc.Threads.Count;
            const uint GR_GDIOBJECTS  = 0;
            const uint GR_USEROBJECTS = 1;
            const uint PROCESS_QUERY_INFORMATION = 0x0400;
            var hProc = NativeMethods.OpenProcess(PROCESS_QUERY_INFORMATION, false, procPid);
            uint gdi = 0, user = 0;
            if (hProc != IntPtr.Zero)
            {
                gdi  = NativeMethods.GetGuiResources(hProc, GR_GDIOBJECTS);
                user = NativeMethods.GetGuiResources(hProc, GR_USEROBJECTS);
                NativeMethods.CloseHandle(hProc);
            }
            Console.Error.WriteLine($"[PROCESS] pid={procPid} {proc.ProcessName}  Priv={privMB:F1}MB  WS={wsMB:F1}MB  handles={handles}  threads={threads}  GDI={gdi}  USER={user}");
        }
        catch { /* best effort -- skip if access denied */ }

        // [URL] grap field: domain + full url (browser windows only)
        try
        {
            NativeMethods.GetWindowThreadProcessId(inspectHandle, out uint urlPid);
            var browserUrl = WindowFinder.GetBrowserUrl(inspectHandle, urlPid);
            if (!string.IsNullOrEmpty(browserUrl) && Uri.TryCreate(browserUrl, UriKind.Absolute, out var uri))
            {
                Console.Error.WriteLine($"[URL]     domain={uri.Host}");
                Console.Error.WriteLine($"          url={browserUrl}");
            }
        }
        catch { }

        // [CMD] grap field: process command line (WMI)
        try
        {
            NativeMethods.GetWindowThreadProcessId(inspectHandle, out uint cmdPid);
            using var searcher = new ManagementObjectSearcher(
                $"SELECT CommandLine FROM Win32_Process WHERE ProcessId={cmdPid}");
            foreach (ManagementObject mo in searcher.Get())
            {
                var cmdLine = mo["CommandLine"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(cmdLine))
                    Console.Error.WriteLine($"[CMD]     {cmdLine}");
            }
        }
        catch { }

        // 노하우 방송: 프로파일 매칭 -> 해당 폼 폴더의 knowhow.md
        BroadcastInspectKnowhow(mainWin.Handle, mainWin.ClassName, matchedFormId, matchedFormTitle);

        // Blocker detection (bug report: inspect should show blocker info)
        try
        {
            var readiness = CreateInputReadiness();
            var blockerInfo = readiness.DetectBlocker(inspectHandle);
            if (blockerInfo != null)
                Console.Error.WriteLine($"[BLOCK] Blocker detected: \"{blockerInfo.Title}\" (class={blockerInfo.ClassName}, hwnd=0x{blockerInfo.Handle.ToInt64():X8})");
        }
        catch { /* best effort */ }

        Console.WriteLine();

        if (pathsMode)
        {
            // --paths: flat list of absolute UIA paths -- each line is a grap-ready #Node1/Node2/Button4
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"-- UIA Paths (depth={depth}) --");
            Console.ResetColor();
            using var uia = new UiaLocator();
            AutomationElement pathRoot;
            if (!string.IsNullOrEmpty(uiaScopePath))
            {
                var uiaRoot = uia.Automation.FromHandle(inspectHandle);
                var scopedRoot = GrapHelper.FindUiaScope(uiaRoot, uiaScopePath);
                if (scopedRoot == null) return Error($"UIA scope not found: \"{uiaScopePath}\"");
                pathRoot = scopedRoot;
            }
            else
            {
                pathRoot = uia.Automation.FromHandle(inspectHandle);
            }
            uia.DumpPaths(pathRoot, maxDepth: depth, writer: Console.Out);
        }
        else if (win32Mode)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"-- Win32 Window Tree (depth={depth}) --");
            Console.ResetColor();
            var win32Tree = WindowFinder.DumpWin32Tree(inspectHandle, depth);
            Console.Write(win32Tree);
        }
        else if (filter != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"-- UIA Tree Filtered: \"{filter}\" (subtree depth={depth}) --");
            Console.ResetColor();
            using var uia = new UiaLocator();
            if (!string.IsNullOrEmpty(uiaScopePath))
            {
                var uiaRoot = uia.Automation.FromHandle(inspectHandle);
                var scopedRoot = GrapHelper.FindUiaScope(uiaRoot, uiaScopePath);
                if (scopedRoot == null) return Error($"UIA scope not found: \"{uiaScopePath}\"");
                Console.WriteLine($"  UIA scope: \"{scopedRoot.Properties.Name.ValueOrDefault}\"");
                var tree = uia.DumpTreeFiltered(scopedRoot, filter, depth);
                Console.Write(tree);
            }
            else
            {
                var tree = uia.DumpTreeFiltered(inspectHandle, filter, depth);
                Console.Write(tree);
            }
        }
        else
        {
            using var uia = new UiaLocator();
            // Hot focus chain (always shown, depth-independent)
            var focusChain = uia.GetFocusChain(inspectHandle);
            if (!string.IsNullOrEmpty(focusChain))
                Console.Write(focusChain);

            string tree;
            if (!string.IsNullOrEmpty(uiaScopePath))
            {
                var uiaRoot = uia.Automation.FromHandle(inspectHandle);
                var scopedRoot = GrapHelper.FindUiaScope(uiaRoot, uiaScopePath);
                if (scopedRoot == null) return Error($"UIA scope not found: \"{uiaScopePath}\"");
                Console.WriteLine($"  UIA scope: \"{scopedRoot.Properties.Name.ValueOrDefault}\"");
                tree = uia.DumpTree(scopedRoot, depth);
            }
            else
            {
                tree = uia.DumpTree(inspectHandle, depth);
            }
            Console.Write(tree);

            // Binary garbage detection: UIA names from MFC owner-drawn windows (e.g. Kiwoom Heroes
            // _NKHeroMainClass MDI child) may contain non-printable bytes. Auto-trigger visual
            // capture as fallback so caller gets a screenshot instead of unreadable text.
            if (IsBinaryOutput(tree))
                TryCaptureOnBinaryTree(inspectHandle, args);

            var children = WindowFinder.GetChildren(inspectHandle);
            Console.WriteLine($"\n--- Win32 children: {children.Count} ---");
            if (children.Count > 0 && string.IsNullOrWhiteSpace(tree.Replace($"[Window] \"{matchedFormTitle ?? mainWin.Title}\"", "").Trim()))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Hint: UIA tree is empty. Try --win32 for Win32 native child windows.");
                Console.ResetColor();
            }

            // Dynamic a11y: auto-trigger OCR+Gemini when UIA tree is sparse
            TryDynamicA11yFallback(inspectHandle, tree, args);
        }

        Console.WriteLine($"# END hwnd:0x{inspectHandle.ToInt64():X8}");
        return 0;
    }

    /// <summary>
    /// Evaluates UIA tree quality via composite score.
    /// Triggers OCR+Gemini fallback automatically when tree is sparse.
    /// Skipped when --no-vision flag is set or output is redirected.
    /// </summary>
    // In-process cache: hwnd -> Gemini segments (survives across commands in the same session)
    private static readonly Dictionary<IntPtr, List<WKAppBot.Vision.OcrSegment>> _dynA11yCache = new();

    /// <summary>Print cached DYN-A11Y segments for a window (from a previous scan in this session).</summary>
    static void PrintCachedDynA11y(IntPtr hWnd)
    {
        if (!_dynA11yCache.TryGetValue(hWnd, out var segments)) return;
        if (!NativeMethods.IsWindow(hWnd)) { _dynA11yCache.Remove(hWnd); return; }

        NativeMethods.GetWindowRect(hWnd, out var wr);
        var winRect = new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Right - wr.Left, wr.Bottom - wr.Top);

        // Hot focus zone priority (same logic as TryDynamicA11yFallback)
        System.Drawing.RectangleF focusRect = System.Drawing.RectangleF.Empty;
        try
        {
            using var uiaFocus = new WKAppBot.Win32.Accessibility.UiaLocator();
            var focused = uiaFocus.Automation.FocusedElement();
            if (focused != null)
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint targetPid);
                if (focused.Properties.ProcessId.ValueOrDefault == (int)targetPid)
                {
                    var fr = focused.BoundingRectangle;
                    focusRect = new System.Drawing.RectangleF(
                        (fr.X - winRect.Left) / (float)winRect.Width,
                        (fr.Y - winRect.Top)  / (float)winRect.Height,
                        fr.Width  / (float)winRect.Width,
                        fr.Height / (float)winRect.Height);
                }
            }
        }
        catch { }

        bool IsFocusHit(WKAppBot.Vision.OcrSegment s)
        {
            if (focusRect.IsEmpty) return false;
            var expanded = System.Drawing.RectangleF.Inflate(focusRect, focusRect.Width * 0.2f, focusRect.Height * 0.2f);
            return expanded.Contains((float)s.RelX, (float)s.RelY);
        }

        var hotSegs  = segments.Where(IsFocusHit).OrderBy(s => s.RelY).ThenBy(s => s.RelX).ToList();
        var restSegs = segments.Where(s => !IsFocusHit(s)).OrderBy(s => s.RelY).ThenBy(s => s.RelX).ToList();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"  [DYN-A11Y] {segments.Count} cached Gemini element(s):");
        Console.ResetColor();

        foreach (var seg in hotSegs)
        {
            var cx = (int)(winRect.Left + seg.RelX * winRect.Width);
            var cy = (int)(winRect.Top  + seg.RelY * winRect.Height);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"    ★ [{(seg.ControlType ?? "?").PadRight(12)}] \"{seg.Text}\"  @({cx},{cy})");
            Console.ResetColor();
        }
        foreach (var seg in restSegs)
        {
            var cx = (int)(winRect.Left + seg.RelX * winRect.Width);
            var cy = (int)(winRect.Top  + seg.RelY * winRect.Height);
            Console.WriteLine($"    [{(seg.ControlType ?? "?").PadRight(12)}] \"{seg.Text}\"  @({cx},{cy})");
        }
    }

    /// <summary>
    /// Returns true when the UIA tree output contains a high ratio of non-printable bytes
    /// (binary garbage from elevated MFC owner-drawn MDI children such as Kiwoom Heroes
    /// _NKHeroMainClass / Afx:* sub-windows where UIA cannot read text across the elevated
    /// process boundary).
    ///
    /// Threshold: >30% non-readable chars. A char is "readable" when it is:
    ///   - ASCII printable (0x20-0x7E)
    ///   - Normal whitespace (\n \r \t)
    ///   - Hangul (AC00-D7A3 syllables, 1100-11FF Jamo, 3130-318F Compat Jamo)
    ///   - CJK Unified Ideographs (4E00-9FFF) -- common in Korean trading apps for ticker names
    ///   - Other Letter/Number/Punctuation/Symbol categories (covers JP/CN, math, currency)
    /// Anything else (control chars outside the whitespace allow-list, surrogate halves,
    /// PUA garbage) counts as binary noise.
    /// </summary>
    static bool IsBinaryOutput(string text)
    {
        if (string.IsNullOrEmpty(text) || text.Length < 20) return false;
        int binary = 0;
        foreach (var c in text)
        {
            if (c == '\n' || c == '\r' || c == '\t') continue;
            if (c >= 0x20 && c <= 0x7E) continue;                       // ASCII printable
            if (c >= 0xAC00 && c <= 0xD7A3) continue;                   // Hangul syllables
            if (c >= 0x1100 && c <= 0x11FF) continue;                   // Hangul Jamo
            if (c >= 0x3130 && c <= 0x318F) continue;                   // Hangul Compat Jamo
            if (c >= 0x4E00 && c <= 0x9FFF) continue;                   // CJK Unified
            var cat = char.GetUnicodeCategory(c);
            if (cat == System.Globalization.UnicodeCategory.UppercaseLetter
                || cat == System.Globalization.UnicodeCategory.LowercaseLetter
                || cat == System.Globalization.UnicodeCategory.TitlecaseLetter
                || cat == System.Globalization.UnicodeCategory.OtherLetter
                || cat == System.Globalization.UnicodeCategory.DecimalDigitNumber
                || cat == System.Globalization.UnicodeCategory.LetterNumber
                || cat == System.Globalization.UnicodeCategory.OtherNumber
                || cat == System.Globalization.UnicodeCategory.ConnectorPunctuation
                || cat == System.Globalization.UnicodeCategory.DashPunctuation
                || cat == System.Globalization.UnicodeCategory.OpenPunctuation
                || cat == System.Globalization.UnicodeCategory.ClosePunctuation
                || cat == System.Globalization.UnicodeCategory.InitialQuotePunctuation
                || cat == System.Globalization.UnicodeCategory.FinalQuotePunctuation
                || cat == System.Globalization.UnicodeCategory.OtherPunctuation
                || cat == System.Globalization.UnicodeCategory.MathSymbol
                || cat == System.Globalization.UnicodeCategory.CurrencySymbol
                || cat == System.Globalization.UnicodeCategory.ModifierSymbol
                || cat == System.Globalization.UnicodeCategory.OtherSymbol)
                continue;
            binary++;
        }
        return binary * 100 / text.Length > 30;
    }

    /// <summary>
    /// Auto-capture the target window when inspect returns binary garbage from an elevated
    /// MFC/MDI child where UIA text extraction is unreadable.
    ///
    /// Fallback chain (per suggest 2026-04-26 -- elevated MDI capture):
    ///   1. ScreenCapture.TryPrintWindowOnly(hwnd) -- focusless, no Z-order disturbance
    ///   2. ScreenCapture.CaptureScreenRegion(GetWindowRect) -- screen-coord fallback if
    ///      PrintWindow returns null (window may be partially covered but visible)
    ///
    /// GetWindowRect returns screen coordinates even for child/MDI windows on Win10+,
    /// so no ClientToScreen translation is required for top-level capture geometry.
    /// Output: prints "# SCREENSHOT path=..." marker line so downstream tooling (Claude
    /// session, Eye renderer) can pick up the visual context instead of binary text.
    /// </summary>
    static void TryCaptureOnBinaryTree(IntPtr hWnd, string[] args)
    {
        if (args.Contains("--no-vision")) return;
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine("[INSPECT] Binary garbage detected (>30% non-readable) -- elevated MDI fallback to PrintWindow...");
            Console.ResetColor();

            System.Drawing.Bitmap? bmp = null;
            string source = "";

            // 1) PrintWindow: focusless, works against most owner-drawn HDC paths.
            try
            {
                bmp = WKAppBot.Win32.Input.ScreenCapture.TryPrintWindowOnly(hWnd);
                if (bmp != null) source = "PrintWindow";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[INSPECT] PrintWindow threw: {ex.Message}");
            }

            // 2) Screen-region fallback via GetWindowRect (returns screen coords on Win10+).
            if (bmp == null)
            {
                try
                {
                    if (NativeMethods.GetWindowRect(hWnd, out var wr))
                    {
                        int w = wr.Right - wr.Left;
                        int h = wr.Bottom - wr.Top;
                        if (w > 0 && h > 0)
                        {
                            bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(wr.Left, wr.Top, w, h);
                            if (bmp != null) source = "ScreenRegion";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[INSPECT] CaptureScreenRegion threw: {ex.Message}");
                }
            }

            if (bmp == null)
            {
                Console.Error.WriteLine("[INSPECT] Both PrintWindow and ScreenRegion fallbacks returned null -- window may be minimized or off-screen.");
                return;
            }

            try
            {
                var dir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq", "experience", "captures");
                Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, $"inspect_binary_{DateTime.Now:yyyyMMdd_HHmmss}_0x{hWnd:X}.png");
                WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, path);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"# SCREENSHOT path={path} source={source}");
                Console.ResetColor();
            }
            finally
            {
                bmp.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[INSPECT] Capture failed: {ex.Message}");
        }
    }

    static void TryDynamicA11yFallback(IntPtr hWnd, string uiaTree, string[] args)
    {
        if (args.Contains("--no-vision")) return;
        if (Console.IsOutputRedirected) return;

        // Composite quality score from tree string
        var lines = uiaTree.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int totalElements = lines.Length;

        int maxDepth = 0;
        int leafTextCount = 0;
        foreach (var line in lines)
        {
            int indent = line.Length - line.TrimStart().Length;
            int depth = indent / 2;
            if (depth > maxDepth) maxDepth = depth;
            // Leaf with visible text: line contains a non-empty quoted string
            var m = System.Text.RegularExpressions.Regex.Match(line, "\"([^\"]+)\"");
            if (m.Success && m.Groups[1].Value.Trim().Length > 0)
                leafTextCount++;
        }

        // Fire if hollow OR sparse:
        // - Hollow: tree has elements (≥3) but no meaningful text labels -- Flutter [Group] blobs, custom renderers
        // - Sparse: truly tiny tree -- almost nothing loaded yet
        bool isHollow = totalElements >= 3 && leafTextCount < 2;
        bool isSparse = totalElements < 5 && maxDepth < 5 && leafTextCount < 2;
        bool isPoor = isHollow || isSparse;
        if (!isPoor) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n[DYN-A11Y] Sparse UIA tree (elements={totalElements}, depth={maxDepth}, text={leafTextCount}) -- auto-triggering OCR+Gemini...");
        Console.ResetColor();

        try
        {
            var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hWnd, new WKAppBot.Win32.Input.CaptureOptions
            {
                RejectBlank = true,
                StepLogger = s => Console.Error.WriteLine(s),
            });
            if (bmp == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[DYN-A11Y] Capture returned blank -- skipping Gemini scan.");
                Console.ResetColor();
                return;
            }
            var segments = AskGeminiForFormScanAsync(bmp).GetAwaiter().GetResult();
            if (segments == null || segments.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[DYN-A11Y] Gemini returned no elements.");
                Console.ResetColor();
                return;
            }

            NativeMethods.GetWindowRect(hWnd, out var wr);
            var winRect = new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Right - wr.Left, wr.Bottom - wr.Top);

            // Store in session cache for windows --uia to pick up
            _dynA11yCache[hWnd] = segments;

            // Hot focus chain: get focused element rect for priority sorting
            System.Drawing.RectangleF focusRect = System.Drawing.RectangleF.Empty;
            try
            {
                using var uiaFocus = new WKAppBot.Win32.Accessibility.UiaLocator();
                var focused = uiaFocus.Automation.FocusedElement();
                if (focused != null)
                {
                    NativeMethods.GetWindowThreadProcessId(hWnd, out uint targetPid);
                    if (focused.Properties.ProcessId.ValueOrDefault == (int)targetPid)
                    {
                        var fr = focused.BoundingRectangle;
                        // Convert to relative coords within window
                        focusRect = new System.Drawing.RectangleF(
                            (fr.X - winRect.Left) / (float)winRect.Width,
                            (fr.Y - winRect.Top)  / (float)winRect.Height,
                            fr.Width  / (float)winRect.Width,
                            fr.Height / (float)winRect.Height);
                    }
                }
            }
            catch { }

            // Classify segments: focused-area hits first, then rest by Y/X
            bool IsFocusHit(WKAppBot.Vision.OcrSegment s)
            {
                if (focusRect.IsEmpty) return false;
                // Segment center overlaps focused element rect (with 20% expansion tolerance)
                var expanded = System.Drawing.RectangleF.Inflate(focusRect, focusRect.Width * 0.2f, focusRect.Height * 0.2f);
                return expanded.Contains((float)s.RelX, (float)s.RelY);
            }

            var hotSegs  = segments.Where(IsFocusHit).OrderBy(s => s.RelY).ThenBy(s => s.RelX).ToList();
            var restSegs = segments.Where(s => !IsFocusHit(s)).OrderBy(s => s.RelY).ThenBy(s => s.RelX).ToList();

            // Print as virtual a11y tree -- hot nodes first
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"-- [DYN-A11Y] Gemini Vision Tree ({segments.Count} elements) --");
            Console.ResetColor();

            if (hotSegs.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  ★ Focus-zone ({hotSegs.Count}):");
                Console.ResetColor();
                foreach (var seg in hotSegs)
                {
                    var cx = (int)(winRect.Left + seg.RelX * winRect.Width);
                    var cy = (int)(winRect.Top  + seg.RelY * winRect.Height);
                    var type = (seg.ControlType ?? "?").PadRight(12);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"  ★ [{type}] \"{seg.Text}\"  @({cx},{cy})");
                    Console.ResetColor();
                }
            }

            foreach (var seg in restSegs)
            {
                var cx = (int)(winRect.Left + seg.RelX * winRect.Width);
                var cy = (int)(winRect.Top  + seg.RelY * winRect.Height);
                var type = (seg.ControlType ?? "?").PadRight(12);
                Console.WriteLine($"  [{type}] \"{seg.Text}\"  @({cx},{cy})");
            }
            Console.Error.WriteLine($"[DYN-A11Y] Cached {segments.Count} element(s) for hwnd=0x{hWnd:X}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Error.WriteLine($"[DYN-A11Y] Failed: {ex.Message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Find a child window (MDI children + direct children) matching a grap pattern.
    /// ★ Search key: "[ClassName] Title (cid=N hwnd=XX WxH)" -- class name 절대 우선.
    ///   Empty-title children도 클래스명/cid로 매칭 가능.
    ///   Examples: "#32770" -> class match, "cid=4015" -> control ID match, "*현재가*" -> title match.
    /// </summary>
    static (IntPtr handle, string? formId, string title)? FindChildWindowByPattern(IntPtr hParent, string pattern)
    {
        // MDI 자식 먼저 탐색 (MDIClient 찾기)
        var topChildren = WindowFinder.GetChildrenZOrder(hParent);
        IntPtr hMdiClient = IntPtr.Zero;
        foreach (var ch in topChildren)
        {
            if (ch.ClassName == "MDIClient")
            {
                hMdiClient = ch.Handle;
                break;
            }
        }

        // MDI 자식 검색
        if (hMdiClient != IntPtr.Zero)
        {
            var mdiChildren = WindowFinder.GetChildrenZOrder(hMdiClient);
            foreach (var mc in mdiChildren)
            {
                var searchKey = BuildChildSearchKey(mc);
                // ★ Try title first (backward compat), then full searchKey
                if (GlobMatch(mc.Title, pattern) || GlobMatch(searchKey, pattern))
                {
                    var (formId, _) = ZoneClassifier.ParseFormTitle(mc.Title);
                    return (mc.Handle, formId, mc.Title);
                }
            }
        }

        // 직접 자식도 검색 (non-MDI 앱)
        foreach (var ch in topChildren)
        {
            var searchKey = BuildChildSearchKey(ch);
            if (GlobMatch(ch.Title, pattern) || GlobMatch(searchKey, pattern))
            {
                var (formId, _) = ZoneClassifier.ParseFormTitle(ch.Title);
                return (ch.Handle, formId, ch.Title);
            }
        }

        return null;
    }

    /// <summary>
    /// Build enriched search key for child window: "[ClassName] Title (cid=N hwnd=XX WxH)"
    /// </summary>
    static string BuildChildSearchKey(WindowInfo wi)
    {
        var r = wi.Rect;
        return $"[{wi.ClassName}] {wi.Title} (cid={wi.ControlId} hwnd={wi.Handle:X8} {r.Width}x{r.Height})";
    }

    /// <summary>
    /// Simple glob match: * = any chars, ? = one char, ** = same as * for title matching.
    /// Pattern can be substring if no wildcards.
    /// </summary>
    static bool GlobMatch(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        // PatternMatcher.Create does substring matching for all modes
        return PatternMatcher.Create(pattern).IsMatch(text);
    }

    /// <summary>
    /// Show available child windows when pattern match fails.
    /// </summary>
    static void ShowAvailableChildren(IntPtr hParent)
    {
        var topChildren = WindowFinder.GetChildrenZOrder(hParent);
        IntPtr hMdiClient = IntPtr.Zero;
        foreach (var ch in topChildren)
        {
            if (ch.ClassName == "MDIClient") { hMdiClient = ch.Handle; break; }
        }

        var candidates = new List<string>();
        if (hMdiClient != IntPtr.Zero)
        {
            var mdiChildren = WindowFinder.GetChildrenZOrder(hMdiClient);
            foreach (var mc in mdiChildren)
            {
                // ★ Show enriched search key (empty-title children visible too)
                candidates.Add(BuildChildSearchKey(mc));
            }
        }
        else
        {
            // Non-MDI: show direct children
            foreach (var ch in topChildren)
            {
                if (ch.ClassName == "IME" || ch.Rect.Width <= 0) continue;
                candidates.Add(BuildChildSearchKey(ch));
            }
        }
        if (candidates.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n사용 가능한 자식 ({candidates.Count}개):");
            foreach (var c in candidates.Take(20))
                Console.WriteLine($"  · {c}");
            if (candidates.Count > 20)
                Console.WriteLine($"  ... +{candidates.Count - 20}개");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Broadcast knowhow for the inspect target.
    /// matchedFormId != null -> 해당 폼 폴더의 knowhow.md만 방송
    /// matchedFormId == null -> 프로필 레벨 knowhow.md만 방송 (form 노하우는 생략)
    /// 엑빌경로(A11Y) = profiles/{name}_exp/ -- 플랫폼 무관, 프로필 기반
    /// 운영경로(OS)  = experience/{process}/{class}/ -- OS 종속 (Win32 class)
    /// </summary>
    static void BroadcastInspectKnowhow(IntPtr mainHandle, string mainClassName, string? matchedFormId, string? matchedFormTitle = null)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(mainHandle, out uint inspPid);
            var procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)inspPid).ProcessName; } catch { }

            // -- 경로 해석 --
            var profileStore = new ProfileStore();
            var profileMatch = profileStore.FindByMatch(mainClassName, procName);

            // A11Y 경로 (프로필 기반)
            string? a11yDir = null;
            if (profileMatch != null)
            {
                a11yDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                if (!Directory.Exists(a11yDir)) a11yDir = null;
            }

            // OS 경로 (process/class 기반)
            string? osDir = null;
            if (!string.IsNullOrEmpty(procName))
            {
                var safeProc = SanitizePathToken(procName);
                var safeClass = SanitizePathToken(mainClassName);
                osDir = Path.Combine(DataDir, "experience", safeProc, safeClass);
                if (!Directory.Exists(osDir)) osDir = null;
            }

            // 경로 표시
            if (a11yDir != null || osDir != null)
            {
                if (a11yDir != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Error.Write("  [A11Y] ");
                    Console.ResetColor();
                    Console.WriteLine(Path.GetRelativePath(Path.GetDirectoryName(profileStore.ProfileDir)!, a11yDir));
                }
                if (osDir != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Error.Write("  [OS]   ");
                    Console.ResetColor();
                    Console.Error.WriteLine(Path.GetRelativePath(DataDir, osDir));

                    // Recent captures (latest 3)
                    var captures = Directory.GetFiles(osDir, "capture_*.png")
                        .OrderByDescending(File.GetLastWriteTime).Take(3).ToArray();
                    if (captures.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Error.Write("  [CAP]  ");
                        Console.ResetColor();
                        Console.Error.WriteLine(string.Join("  ", captures.Select(Path.GetFileName)));
                    }

                    // Recent info files: non-PNG, latest 5 by mtime (filename only)
                    var infoFiles = Directory.GetFiles(osDir)
                        .Where(f => !Path.GetExtension(f).Equals(".png", StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(File.GetLastWriteTime).Take(5).ToArray();
                    if (infoFiles.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Error.Write("  [INFO] ");
                        Console.ResetColor();
                        Console.Error.WriteLine(string.Join("  ", infoFiles.Select(Path.GetFileName)));
                    }
                }
            }

            if (a11yDir == null && osDir == null) return;

            // -- A11Y 노하우 방송 --
            if (a11yDir != null)
            {
                // Profile-level knowhow
                var profileKh = Path.Combine(a11yDir, "knowhow.md");
                if (File.Exists(profileKh)) ShowKnowhowBroadcast(profileKh, "KNOWHOW:A11Y");

                if (!string.IsNullOrEmpty(matchedFormId))
                {
                    var formDir = Path.Combine(a11yDir, $"form_{matchedFormId}");
                    if (Directory.Exists(formDir))
                    {
                        var kh = Path.Combine(formDir, "knowhow.md");
                        if (File.Exists(kh))
                        {
                            ShowKnowhowBroadcast(kh, "KNOWHOW:A11Y");
                        }
                        else
                        {
                            TryGenerateKnowhowTemplate(formDir, matchedFormId, a11yDir, matchedFormTitle);
                            if (File.Exists(kh)) ShowKnowhowBroadcast(kh, "KNOWHOW:A11Y");
                        }

                        // Form-level action knowhow files (knowhow-*.md) -- filename only
                        foreach (var khExtra in Directory.GetFiles(formDir, "knowhow-*.md").Take(5))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"  [KNOWHOW:A11Y] + {Path.GetFileName(khExtra)}");
                            Console.ResetColor();
                        }

                        var treeDir = Path.Combine(formDir, "tree");
                        if (Directory.Exists(treeDir))
                        {
                            foreach (var khFile in Directory.GetFiles(treeDir, "knowhow*.md", SearchOption.AllDirectories).Take(5))
                                ShowKnowhowBroadcast(khFile, "KNOWHOW:A11Y");
                        }
                    }
                }
                else
                {
                    // matchedFormId == null -> 프로필 레벨 knowhow.md는 이미 위에서 방송됨
                    // form_* 폴더 무차별 방송은 노이즈 유발 -> 생략
                    // (특정 폼 작업 시 inspect/cond-add 등에서 formId 명시하면 해당 폼만 방송)
                }
            }

            // -- OS 노하우 방송 --
            if (osDir != null)
            {
                var osKh = Path.Combine(osDir, "knowhow.md");
                if (File.Exists(osKh)) ShowKnowhowBroadcast(osKh, "KNOWHOW:OS");
            }
        }
        catch { /* best-effort */ }
    }

    // SanitizePathToken is defined in SnapshotCommand.cs (same partial class)

    /// <summary>
    /// 노하우 없는 폼 폴더에 기본 템플릿 자동 생성.
    /// 첫 문단: 개요 (폼 이름 + form.json 요약)
    /// 둘째 문단: UIA 접근 가능/불가 컨트롤 분류
    /// ExperienceDb form_{id}.json에서 컨트롤 목록 추출.
    /// </summary>
    static void TryGenerateKnowhowTemplate(string formDir, string formId, string expDir, string? mdiTitle = null)
    {
        try
        {
            var khPath = Path.Combine(formDir, "knowhow.md");
            if (File.Exists(khPath)) return; // already exists

            // formTitle: MDI child 타이틀에서 추출 (예: "[0600] 키움종합차트" -> "키움종합차트")
            var formJson = Path.Combine(expDir, $"form_{formId}.json");
            string formTitle = "";
            if (!string.IsNullOrEmpty(mdiTitle))
            {
                // "[0600] 키움종합차트" -> "키움종합차트", "[0150] 조건검색" -> "조건검색"
                formTitle = System.Text.RegularExpressions.Regex.Replace(mdiTitle, @"^\[\d+\]\s*", "").Trim();
                // 뒤에 부가정보 제거: "키움현재가 (통합)" -> 그대로 유지 (정보성)
            }
            var uiaAccessible = new List<string>();
            var ownerDrawn = new List<string>();

            if (File.Exists(formJson))
            {
                try
                {
                    var json = File.ReadAllText(formJson);
                    var doc = System.Text.Json.JsonDocument.Parse(json);

                    // FormTitle 추출 (mdiTitle이 없을 때만)
                    if (string.IsNullOrEmpty(formTitle) && doc.RootElement.TryGetProperty("FormTitle", out var titleEl))
                    {
                        var t = titleEl.GetString();
                        if (!string.IsNullOrEmpty(t))
                            formTitle = System.Text.RegularExpressions.Regex.Replace(t, @"^\[\d+\]\s*", "").Trim();
                    }

                    // Controls 맵에서 UIA 패턴 분류
                    if (doc.RootElement.TryGetProperty("Controls", out var controls))
                    {
                        foreach (var ctrl in controls.EnumerateObject())
                        {
                            var cid = ctrl.Name;
                            var c = ctrl.Value;
                            var name = "";
                            var role = "";
                            var patterns = "";

                            if (c.TryGetProperty("Name", out var nameEl)) name = nameEl.GetString() ?? "";
                            if (c.TryGetProperty("Role", out var roleEl)) role = roleEl.GetString() ?? "";
                            if (c.TryGetProperty("Patterns", out var patEl)) patterns = patEl.GetString() ?? "";

                            // Invoke/Value/Toggle/SelectionItem = UIA 접근 가능
                            bool hasUiaAction = patterns.Contains("Invoke") || patterns.Contains("Value")
                                || patterns.Contains("Toggle") || patterns.Contains("SelectionItem")
                                || patterns.Contains("ExpandCollapse");

                            var desc = !string.IsNullOrEmpty(name) ? $"{role} \"{name}\" (cid={cid})" : $"{role} (cid={cid})";
                            if (!string.IsNullOrEmpty(patterns)) desc += $" [{patterns}]";

                            if (hasUiaAction)
                                uiaAccessible.Add($"- {desc}");
                            else if (role == "Pane" || role == "Custom" || role == "List")
                                ownerDrawn.Add($"- {desc}");
                        }
                    }
                }
                catch { /* parse error, still generate minimal template */ }
            }

            // 템플릿 생성 -- 첫 문단은 ShowKnowhowBroadcast에서 방송됨!
            var sb = new System.Text.StringBuilder();
            var headerTitle = !string.IsNullOrEmpty(formTitle) ? $"{formTitle} [{formId}]" : $"[{formId}]";
            sb.AppendLine($"## {headerTitle} 자동화 노하우");
            sb.AppendLine($"이 폼의 자동화 노하우를 여기에 기록해주세요! 첫 문단(이 줄)이 inspect 시 [KNOWHOW]로 방송됩니다. 폼의 핵심 특성, Focusless 가능 여부, 주의사항 등을 한 줄 개요로 정리하면 미래의 클롣이 감사합니다.");
            sb.AppendLine();

            if (uiaAccessible.Count > 0 || ownerDrawn.Count > 0)
            {
                if (uiaAccessible.Count > 0)
                {
                    sb.AppendLine($"### UIA 접근 가능 (Focusless) -- {uiaAccessible.Count}개");
                    foreach (var item in uiaAccessible.Take(15))
                        sb.AppendLine(item);
                    if (uiaAccessible.Count > 15)
                        sb.AppendLine($"- ... +{uiaAccessible.Count - 15}개");
                    sb.AppendLine();
                }

                if (ownerDrawn.Count > 0)
                {
                    sb.AppendLine($"### UIA 접근 불가 (Owner-drawn) -- {ownerDrawn.Count}개");
                    foreach (var item in ownerDrawn.Take(10))
                        sb.AppendLine(item);
                    if (ownerDrawn.Count > 10)
                        sb.AppendLine($"- ... +{ownerDrawn.Count - 10}개");
                    sb.AppendLine();
                }
            }

            File.WriteAllText(khPath, sb.ToString(), System.Text.Encoding.UTF8);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"  [KNOWHOW] 기본 템플릿 자동 생성: {khPath}");
            Console.ResetColor();
        }
        catch { /* best-effort */ }
    }

    // -- focus ------------------------------------------------─

    // windows was removed (v6.1) -- redirect to a11y find
    // Strip --uia/--uia-deep injected by FindCommand to avoid mutual recursion.
    static int WindowsCommand(string[] args)
    {
        var cleanArgs = args.Where(a => a != "--uia" && a != "--uia-deep").ToArray();
        if (cleanArgs.Length == 0 || (cleanArgs.Length == 1 && cleanArgs[0] == "--help"))
            return A11yCommand(["find", "wkappbot*"]);
        return A11yCommand(["find", ..cleanArgs]);
    }

}
