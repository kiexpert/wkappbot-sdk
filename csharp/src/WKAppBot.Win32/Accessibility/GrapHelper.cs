using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// Grap pattern helper: '/' for Win32 hierarchy, '#' switches to UIA scope.
///
/// ═══ Separator semantics ═══
///   Before first '#':  '/' = Win32 child window separator
///   After first '#':   '/' and '#' are both UIA scope separators (equivalent)
///
/// ═══ Synthesized full-path examples ═══
///   "영웅문/실시간계좌"                → Win32: 영웅문 → child "실시간계좌"
///   "영웅문#실시간계좌"                → UIA scope "실시간계좌" within 영웅문
///   "영웅문/실시간계좌#aid1000"        → Win32 child → UIA scope "aid1000"
///   "영웅문/child#uia1/uia2"          → Win32 child → UIA "uia1" → "uia2"
///
/// ═══ Browser tab portal (v2.1) ═══
/// When a '#' segment matches a TabItem (browser tab), the grap engine:
///   1. Calls SelectionItem.Select() to switch to that tab (focusless!)
///   2. Waits for web content to load
///   3. Jumps to the sibling Document[RootWebArea] — the tab's web content
///   4. Continues matching remaining segments inside the web page
///
/// This enables unified Win32 → browser tab → web element paths:
///   "Chrome#ChatGPT#모델"     → Chrome window → switch to ChatGPT tab → find "모델" button
///   "Edge#YouTube#재생"       → Edge window → switch to YouTube tab → find "재생" button
///   "Chrome#Gemini#새 채팅"   → Chrome → Gemini tab → "새 채팅" button in web page
///
/// The same path works for Electron apps (Claude, VS Code) where UIA exposes
/// web content directly under Document[RootWebArea] without needing CDP.
///
/// ═══ Pattern matching ═══
/// Each segment supports PatternMatcher syntax:
///   plain text = substring Contains match (no wildcards needed!)
///   * / ? = glob wildcards
///   regex: prefix = regular expression
/// Container-first: prefers Window/Pane/Group elements over leaf elements (TabItem/Button).
///
/// ═══ Public API (v2.1) ═══
/// All core search/portal functions are public for reuse by other commands:
///   FindUiaScope()         — multi-segment UIA path resolution with tab portal
///   FindByNameOrAid()      — single element search by Name or AutomationId
///   FindRootWebArea()      — locate Document[RootWebArea] for web content access
///   SwitchToTab()          — focusless tab switch via SelectionItem/Invoke
///   IsTabItem()            — check if element is a browser tab
///   WalkTree()             — generic predicate-based tree walk
///   SplitGrap()            — parse grap into Win32 + UIA segments
///   ResolveFullGrap()      — end-to-end grap → (hwnd, element) resolution
/// </summary>
public static class GrapHelper
{
    // ── 공용 a11y 노드 포맷터 ──────────────────────────────────────────────

    /// <summary>
    /// Format a11y node label: "ControlType(AutomationId)" or "ControlType("Name")".
    /// Used everywhere a11y nodes are displayed: windows, inspect, find, read, etc.
    /// </summary>
    public static string FormatNodeLabel(AutomationElement el)
    {
        var ct = el.Properties.ControlType.ValueOrDefault;
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
        var name = el.Properties.Name.ValueOrDefault ?? "";
        var label = ct.ToString();
        if (aid.Length > 0)
            return $"{label}({aid})";
        if (name.Length > 0)
            return $"{label}(\"{(name.Length > 25 ? name[..22] + "..." : name)}\")";
        return label;
    }

    /// <summary>
    /// Build full a11y path from leaf to root: "Window/Group/Edit(inputId)".
    /// Walk up parent chain. Returns grap-ready path (/ separated).
    /// </summary>
    public static string BuildA11yPath(AutomationElement leaf)
    {
        var parts = new List<string>();
        var el = leaf;
        while (el != null)
        {
            parts.Add(FormatNodeLabel(el));
            try { el = el.Parent; } catch { break; }
        }
        parts.Reverse();
        return string.Join("/", parts);
    }

    /// <summary>
    /// Build full grap expression: "windowTitle#a11yPath".
    /// Ready to use as wkappbot a11y target.
    /// </summary>
    public static string BuildGrapExpression(IntPtr hwnd, AutomationElement leaf)
    {
        var title = WKAppBot.Win32.Native.NativeMethods.GetWindowTextW(hwnd);
        if (title.Length > 30) title = title[..27] + "*";
        return $"\"{title}\"#{BuildA11yPath(leaf)}";
    }

    /// <summary>
    /// Find the keyboard-focused LEAF element under a root (works for background windows).
    /// Uses TreeWalker + HasKeyboardFocus instead of FocusedElement (foreground-only).
    /// </summary>
    public static AutomationElement? FindFocusedLeaf(FlaUI.UIA3.UIA3Automation uia, AutomationElement root)
    {
        if (root.Properties.HasKeyboardFocus.ValueOrDefault) return root;
        try
        {
            var walker = uia.TreeWalkerFactory.GetControlViewWalker();
            return WalkForFocus(walker, root);
        }
        catch { return null; }
    }

    private static AutomationElement? WalkForFocus(dynamic walker, AutomationElement el)
    {
        var child = walker.GetFirstChild(el);
        while (child != null)
        {
            if (child.Properties.HasKeyboardFocus.ValueOrDefault) return child;
            var found = WalkForFocus(walker, child);
            if (found != null) return found;
            child = walker.GetNextSibling(child);
        }
        return null;
    }

    // Container types: these are preferred when searching for scope roots
    private static readonly HashSet<ControlType> ContainerTypes = new()
    {
        ControlType.Window, ControlType.Pane, ControlType.Group,
        ControlType.Tab, ControlType.Document, ControlType.Custom
    };

    // ═══════════════════════════════════════════════════════════════════════
    // CSS vs UIA pattern detection
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Determine if a '#' scope pattern looks like a CSS selector rather than a UIA Name.
    /// CSS indicators: starts with '.', contains '[', '>', '~', '+', ':', or is a tag name.
    /// UIA indicators: contains '*' wildcard, starts with letter and has spaces (natural name).
    /// </summary>
    public static bool LooksLikeCssSelector(string? pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) return false;

        // First segment only (before any / or # separator)
        var firstSeg = pattern.Split(['/', '#'], 2)[0].Trim();
        if (string.IsNullOrEmpty(firstSeg)) return false;

        // Explicit CSS: starts with . or [ or contains CSS combinators
        if (firstSeg.StartsWith('.') || firstSeg.StartsWith('['))
            return true;
        if (firstSeg.Contains('[') || firstSeg.Contains('>') || firstSeg.Contains('~') || firstSeg.Contains('+'))
            return true;
        // :pseudo selectors (but not regex: prefix)
        if (firstSeg.Contains(':') && !firstSeg.StartsWith("regex:"))
            return true;

        // Explicit UIA: wildcard patterns are UIA names
        if (firstSeg.Contains('*') || firstSeg.Contains('?'))
            return false;

        // Tag-like selectors: bare word with no spaces (div, button, input, a, span, etc.)
        // But only common HTML tags — otherwise default to UIA Name
        var lower = firstSeg.ToLowerInvariant();
        var htmlTags = new HashSet<string> {
            "div", "span", "button", "input", "textarea", "select", "a", "form",
            "table", "tr", "td", "th", "ul", "ol", "li", "img", "p", "h1", "h2",
            "h3", "h4", "h5", "h6", "nav", "header", "footer", "section", "article",
            "label", "fieldset", "legend", "option", "iframe", "body", "html"
        };
        if (htmlTags.Contains(lower))
            return true;

        // Default: treat as UIA Name (backwards compatible)
        return false;
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Public API: Grap parsing
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Split grap into Win32 path (before first '#') and UIA path (after first '#').
    /// Before '#': '/' is Win32 child separator.
    /// After '#':  '/' and '#' are both UIA scope separators (equivalent).
    /// Returns: (win32Segments, uiaPath) where uiaPath is null if no '#' present.
    /// </summary>
    public static (string[] win32Segments, string? uiaPath) SplitGrap(string grap)
    {
        if (string.IsNullOrEmpty(grap))
            return (Array.Empty<string>(), null);

        var hashIdx = grap.IndexOf('#');
        string win32Part = hashIdx >= 0 ? grap[..hashIdx] : grap;
        string? uiaPart = hashIdx >= 0 ? grap[(hashIdx + 1)..] : null;

        var win32Segments = win32Part.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (string.IsNullOrWhiteSpace(uiaPart))
            uiaPart = null;

        return (win32Segments, uiaPart);
    }

    /// <summary>Overload without matchCount for backwards compat.</summary>
    public static (IntPtr hwnd, AutomationElement root, string? error)? ResolveFullGrap(
        string grap, UIA3Automation automation)
        => ResolveFullGrap(grap, automation, 0, out _);

    /// <summary>
    /// Parse full grap and resolve to (hwnd, uiaRoot).
    /// windowIndex: 0-based index into matching windows (default 0 = first match).
    /// matchCount: out parameter — total matching windows found.
    /// </summary>
    public static (IntPtr hwnd, AutomationElement root, string? error)? ResolveFullGrap(
        string grap, UIA3Automation automation, int windowIndex, out int matchCount)
    {
        matchCount = 0;
        var (win32Segments, uiaPath) = SplitGrap(grap);
        if (win32Segments.Length == 0 && uiaPath == null)
            return (IntPtr.Zero, null!, "Empty grap pattern");

        if (win32Segments.Length == 0)
            return (IntPtr.Zero, null!, "No window title before '#'");

        // First segment = main window title — may match multiple windows
        var windows = Window.WindowFinder.FindByTitle(win32Segments[0]);
        matchCount = windows.Count;
        if (windows.Count == 0)
            return (IntPtr.Zero, null!, $"Window not found: \"{win32Segments[0]}\"");

        if (windowIndex >= windows.Count)
            return (IntPtr.Zero, null!, $"--nth {windowIndex + 1} but only {windows.Count} match(es) for \"{win32Segments[0]}\"");

        var targetHwnd = windows[windowIndex].Handle;

        // Trailing '/' (no UIA path) or trailing '#' (empty UIA scope) = drill to focused child + UIA leaf
        // e.g. "*메모장*/"  → focused child hwnd + focused UIA element
        //      "*메모장*#"  → same (# with empty scope = focus drill)
        bool drillFocused = (grap.TrimEnd().EndsWith('/') && uiaPath == null)
                         || (uiaPath != null && uiaPath.Length == 0);

        // Walk Win32 children (segments[1..] before '#')
        for (int i = 1; i < win32Segments.Length; i++)
        {
            var child = Window.WindowFinder.FindChildByPattern(targetHwnd, win32Segments[i]);
            if (child == null)
                return (targetHwnd, null!, $"Win32 child not found: \"{win32Segments[i]}\"");
            targetHwnd = child.Handle;
        }

        // Drill to focused child hwnd + focused UIA leaf
        if (drillFocused)
        {
            uint tid = NativeMethods.GetWindowThreadProcessId(targetHwnd, out _);
            var gti = new NativeMethods.GUITHREADINFO { cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            NativeMethods.GetGUIThreadInfo(tid, ref gti);
            if (gti.hwndFocus != IntPtr.Zero)
                targetHwnd = gti.hwndFocus;
            Console.WriteLine($"[GRAP] focus-drill → hwnd=0x{targetHwnd:X8}");

            try
            {
                var focusedEl = automation.FocusedElement();
                if (focusedEl != null)
                {
                    var fname = focusedEl.Properties.Name.ValueOrDefault ?? "";
                    var ftype = "?";
                    try { ftype = focusedEl.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                    Console.WriteLine($"[GRAP] focus-drill → UIA [{ftype}] \"{fname}\"");
                    return (targetHwnd, focusedEl, null);
                }
            }
            catch { }

            return (targetHwnd, automation.FromHandle(targetHwnd), null);
        }

        // UIA scope (after '#')
        AutomationElement root = automation.FromHandle(targetHwnd);
        if (!string.IsNullOrEmpty(uiaPath))
        {
            var scoped = FindUiaScope(root, uiaPath);
            if (scoped == null)
                return (targetHwnd, null!, $"UIA scope not found: \"{uiaPath}\"");
            root = scoped;
        }

        return (targetHwnd, root, null);
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Public API: UIA scope resolution (with tab portal)
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Find UIA element by name path within a root element.
    /// Supports multi-level paths with '/' or '#' separator (both equivalent).
    ///
    /// ═══ Browser Tab Portal ═══
    /// When a segment matches a TabItem (browser tab), the engine:
    ///   1. SelectionItem.Select() → switch tab (focusless)
    ///   2. Navigate back to the window root
    ///   3. Find Document[RootWebArea] → the newly-active tab's web content
    ///   4. Continue matching remaining segments inside that Document
    ///
    /// Why? In Chrome/Edge UIA tree, TabItem and RootWebArea are siblings,
    /// not parent-child. TabItem lives under [Tab], web content under [Pane]:
    ///   Pane (browser frame)
    ///   ├── Pane → Document "Google Gemini" [RootWebArea]  ← web content
    ///   ├── Tab
    ///   │   ├── TabItem "ChatGPT"    ← tab UI (only has Close button)
    ///   │   └── TabItem "Gemini"
    ///   └── ToolBar (address bar)
    ///
    /// So "Chrome#ChatGPT#Send" means:
    ///   find TabItem "ChatGPT" → select it → jump to RootWebArea → find "Send"
    /// </summary>
    public static AutomationElement? FindUiaScope(
        AutomationElement root, string uiaPath, int maxDepth = 25)
    {
        if (string.IsNullOrEmpty(uiaPath))
            return root;

        var segments = uiaPath.Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);

        // Keep reference to the window root for tab-portal jump-back
        var windowRoot = root;
        var current = root;
        foreach (var segment in segments)
        {
            var found = FindByNameOrAid(current, segment, maxDepth);
            if (found == null)
                return null;

            // ═══ Tab Portal: TabItem → select tab → jump to RootWebArea ═══
            if (IsTabItem(found))
            {
                var webRoot = SwitchToTabAndGetWebRoot(found, windowRoot);
                if (webRoot == null) return null;
                current = webRoot;
                continue; // next segment searches inside the web content
            }

            current = found;
        }

        return current;
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Public API: Element search
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Find first UIA element matching a name/aid pattern string.
    /// Container-first: prefers Window/Pane/Group/Tab/Document over leaf elements.
    /// Matches against Name first, then AutomationId as fallback.
    /// Pattern supports: plain text (substring), wildcards (*/?), regex: prefix.
    ///
    /// Usage:
    ///   var btn = GrapHelper.FindByNameOrAid(root, "새 채팅");     // Name substring
    ///   var edit = GrapHelper.FindByNameOrAid(root, "email");      // aid fallback
    ///   var item = GrapHelper.FindByNameOrAid(root, "regex:btn_\\d+"); // regex
    /// </summary>
    public static AutomationElement? FindByNameOrAid(
        AutomationElement root, string pattern, int maxDepth = 25)
    {
        var matcher = PatternMatcher.Create(pattern);

        // Pass 1: containers only (Window/Pane/Group/Tab/Document/Custom)
        var container = WalkFindByMatcher(root, matcher, maxDepth, depth: 0, containersOnly: true);
        if (container != null) return container;

        // Pass 2: any element (fallback for leaf elements like Button/Edit/TabItem)
        return WalkFindByMatcher(root, matcher, maxDepth, depth: 0, containersOnly: false);
    }

    /// <summary>
    /// Find ALL UIA elements matching a name/aid pattern string, sorted by coverage descending.
    /// Coverage = key length / matched text length — shorter/exact name = higher coverage.
    /// Used for --nth element selection: --nth 1 = best coverage (most specific match).
    /// </summary>
    public static List<(AutomationElement el, double coverage, string matchedText)> FindAllByNameOrAid(
        AutomationElement root, string pattern, int maxDepth = 25)
    {
        var matcher = PatternMatcher.Create(pattern);
        // Strip wildcards/prefix to get the "key" length for coverage calculation
        var key = pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase)
            ? pattern[6..] : pattern.Replace("*", "").Replace("?", "");
        var results = new List<(AutomationElement, double, string)>();
        WalkCollectByMatcher(root, matcher, key, maxDepth, 0, results);
        // Sort: higher coverage first (shorter name relative to pattern = more specific)
        results.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        return results;
    }

    /// <summary>
    /// Like FindUiaScope but returns ALL matches for the last segment, coverage-sorted.
    /// Preceding segments are resolved normally (first match). The last segment collects all.
    /// </summary>
    public static List<(AutomationElement el, double coverage, string matchedText)> FindUiaScopeAll(
        AutomationElement root, string uiaPath, int maxDepth = 25)
    {
        if (string.IsNullOrEmpty(uiaPath))
            return new List<(AutomationElement, double, string)> { (root, 1.0, "") };

        var segments = uiaPath.Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);
        var windowRoot = root;
        var current = root;

        // Resolve all but the last segment (same as FindUiaScope)
        for (int i = 0; i < segments.Length - 1; i++)
        {
            var found = FindByNameOrAid(current, segments[i], maxDepth);
            if (found == null) return new List<(AutomationElement, double, string)>();
            if (IsTabItem(found))
            {
                var webRoot = SwitchToTabAndGetWebRoot(found, windowRoot);
                if (webRoot == null) return new List<(AutomationElement, double, string)>();
                current = webRoot;
                continue;
            }
            current = found;
        }

        // Last segment: collect ALL matches, coverage-sorted
        return FindAllByNameOrAid(current, segments[^1], maxDepth);
    }

    private static void WalkCollectByMatcher(
        AutomationElement parent, PatternMatcher matcher, string key,
        int maxDepth, int depth, List<(AutomationElement, double, string)> results)
    {
        if (depth > maxDepth) return;
        try
        {
            foreach (var child in parent.FindAllChildren())
            {
                try
                {
                    var name = child.Properties.Name.ValueOrDefault ?? "";
                    var aid = child.Properties.AutomationId.ValueOrDefault ?? "";
                    bool nameMatch = !string.IsNullOrEmpty(name) && matcher.IsMatch(name);
                    bool aidMatch = !nameMatch && !string.IsNullOrEmpty(aid) && matcher.IsMatch(aid);
                    if (nameMatch || aidMatch)
                    {
                        var matched = nameMatch ? name : aid;
                        double coverage = matched.Length > 0
                            ? (double)Math.Max(key.Length, 1) / Math.Max(matched.Length, key.Length)
                            : 0;
                        results.Add((child, coverage, matched));
                    }
                }
                catch { }
                WalkCollectByMatcher(child, matcher, key, maxDepth, depth + 1, results);
            }
        }
        catch { }
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Public API: Browser tab portal
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Check if element is a browser TabItem (Chrome/Edge tab strip).
    /// </summary>
    public static bool IsTabItem(AutomationElement el)
    {
        try { return el.Properties.ControlType.ValueOrDefault == ControlType.TabItem; }
        catch { return false; }
    }

    /// <summary>
    /// Switch to a browser tab (focusless) via SelectionItem.Select() or Invoke fallback.
    /// Returns true on success.
    /// </summary>
    public static bool SwitchToTab(AutomationElement tabItem)
    {
        try
        {
            var si = tabItem.Patterns.SelectionItem.PatternOrDefault;
            if (si != null)
            {
                si.Select();
                Console.WriteLine($"[GRAP] Tab portal: selected \"{tabItem.Name}\"");
                Thread.Sleep(600);
                return true;
            }

            // Fallback: invoke if SelectionItem not available (e.g. Edge favorites)
            var inv = tabItem.Patterns.Invoke.PatternOrDefault;
            if (inv != null)
            {
                inv.Invoke();
                Console.WriteLine($"[GRAP] Tab portal: invoked \"{tabItem.Name}\" (no SelectionItem)");
                Thread.Sleep(600);
                return true;
            }

            Console.Error.WriteLine($"[GRAP] Tab switch: no SelectionItem or Invoke on \"{tabItem.Name}\"");
            return false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[GRAP] Tab switch failed: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Switch to a browser tab and return the web content root (Document[RootWebArea]).
    /// Combines SwitchToTab() + FindRootWebArea() in one call.
    ///
    /// Usage:
    ///   var tabItem = GrapHelper.FindByNameOrAid(chromeRoot, "ChatGPT");
    ///   var webRoot = GrapHelper.SwitchToTabAndGetWebRoot(tabItem, chromeRoot);
    ///   var sendBtn = GrapHelper.FindByNameOrAid(webRoot, "Send");
    /// </summary>
    public static AutomationElement? SwitchToTabAndGetWebRoot(
        AutomationElement tabItem, AutomationElement windowRoot)
    {
        if (!SwitchToTab(tabItem)) return null;

        var webRoot = FindRootWebArea(windowRoot);
        if (webRoot == null)
        {
            Console.Error.WriteLine("[GRAP] Tab portal: RootWebArea not found after tab switch");
            return null;
        }

        Console.WriteLine($"[GRAP] Tab portal: jumped to Document \"{webRoot.Name}\"");
        return webRoot;
    }

    /// <summary>
    /// Find the active tab's web content: Document element with aid="RootWebArea".
    /// This is the entry point to all web-page UIA elements (buttons, links, edits, etc).
    /// Works for Chrome, Edge, and Electron apps (Claude, VS Code, Slack).
    ///
    /// Usage:
    ///   var webRoot = GrapHelper.FindRootWebArea(chromeRoot);
    ///   // Now search within: GrapHelper.FindByNameOrAid(webRoot, "Send")
    /// </summary>
    public static AutomationElement? FindRootWebArea(AutomationElement root, int maxDepth = 10)
    {
        return WalkTree(root, maxDepth, el =>
        {
            try
            {
                var ct = el.Properties.ControlType.ValueOrDefault;
                if (ct != ControlType.Document) return false;
                var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
                // Primary: explicit RootWebArea aid (Chrome/Electron)
                if (aid == "RootWebArea") return true;
                // Fallback: any Document with a non-empty name (active page title)
                var name = el.Properties.Name.ValueOrDefault ?? "";
                return !string.IsNullOrEmpty(name) && aid != "active-frame";
            }
            catch { return false; }
        });
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Public API: Generic tree walk
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generic predicate-based UIA tree walk. Recursively searches children
    /// using FindAllChildren() to reach deep Electron/MFC subtrees.
    ///
    /// Usage:
    ///   // Find any Edit element
    ///   var edit = GrapHelper.WalkTree(root, 10, el =>
    ///       el.Properties.ControlType.ValueOrDefault == ControlType.Edit);
    ///
    ///   // Find element with specific ClassName
    ///   var mfc = GrapHelper.WalkTree(root, 8, el =>
    ///       (el.Properties.ClassName.ValueOrDefault ?? "").Contains("MaskEdit"));
    /// </summary>
    public static AutomationElement? WalkTree(
        AutomationElement root, int maxDepth, Func<AutomationElement, bool> predicate)
    {
        return WalkTreeInternal(root, maxDepth, depth: 0, predicate);
    }

    // ═══════════════════════════════════════════════════════════════════════
    // Internal: tree walk implementations
    // ═══════════════════════════════════════════════════════════════════════

    private static AutomationElement? WalkTreeInternal(
        AutomationElement parent, int maxDepth, int depth,
        Func<AutomationElement, bool> predicate)
    {
        if (depth > maxDepth) return null;
        try
        {
            foreach (var child in parent.FindAllChildren())
            {
                try { if (predicate(child)) return child; } catch { }
                var found = WalkTreeInternal(child, maxDepth, depth + 1, predicate);
                if (found != null) return found;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Recursive walk to find first element with matching Name or AutomationId.
    /// Matching order: Name first, then AutomationId as fallback.
    /// This allows "#email" to match aid="email" even when Name is empty.
    /// </summary>
    private static AutomationElement? WalkFindByMatcher(
        AutomationElement parent, PatternMatcher matcher, int maxDepth, int depth,
        bool containersOnly)
    {
        if (depth > maxDepth) return null;

        try
        {
            foreach (var child in parent.FindAllChildren())
            {
                try
                {
                    // Match against Name first, then AutomationId as fallback
                    var name = child.Properties.Name.ValueOrDefault ?? "";
                    var aid = child.Properties.AutomationId.ValueOrDefault ?? "";
                    bool nameMatch = !string.IsNullOrEmpty(name) && matcher.IsMatch(name);
                    bool aidMatch = !nameMatch && !string.IsNullOrEmpty(aid) && matcher.IsMatch(aid);

                    if (nameMatch || aidMatch)
                    {
                        if (!containersOnly)
                            return child;

                        try
                        {
                            var ct = child.Properties.ControlType.ValueOrDefault;
                            if (ContainerTypes.Contains(ct))
                                return child;
                        }
                        catch { /* ControlType access failed, skip */ }
                    }
                }
                catch { /* COM exceptions on some MFC elements */ }

                var found = WalkFindByMatcher(child, matcher, maxDepth, depth + 1, containersOnly);
                if (found != null) return found;
            }
        }
        catch { /* COM or timeout */ }

        return null;
    }
}
