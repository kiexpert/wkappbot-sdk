using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;

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
/// ═══ Browser tab portal (v2.0) ═══
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
/// </summary>
public static class GrapHelper
{
    // Container types: these are preferred when searching for scope roots
    private static readonly HashSet<ControlType> ContainerTypes = new()
    {
        ControlType.Window, ControlType.Pane, ControlType.Group,
        ControlType.Tab, ControlType.Document, ControlType.Custom
    };

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

        // Trim empty UIA part
        if (string.IsNullOrWhiteSpace(uiaPart))
            uiaPart = null;

        return (win32Segments, uiaPart);
    }

    /// <summary>
    /// Find UIA element by name path within a root element.
    /// Supports multi-level paths with '/' or '#' separator (both equivalent in UIA context):
    ///   "잔고확인"         → find container descendant with Name matching "잔고확인"
    ///   "잔고확인/Tab"     → find "잔고확인", then "Tab" within it
    /// Each segment uses PatternMatcher (wildcards, regex:, literal).
    /// Container-first: Window/Pane/Group preferred over TabItem/Button.
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
        AutomationElement root, string uiaPath, int maxDepth = 15)
    {
        if (string.IsNullOrEmpty(uiaPath))
            return root;

        // In UIA context, '/' and '#' are equivalent separators
        var segments = uiaPath.Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);

        // Keep reference to the window root for tab-portal jump-back
        var windowRoot = root;
        var current = root;
        foreach (var segment in segments)
        {
            var matcher = PatternMatcher.Create(segment);
            var found = WalkFindContainerFirst(current, matcher, maxDepth);
            if (found == null)
                return null;

            // ═══ Tab Portal: TabItem → select tab → jump to RootWebArea ═══
            // When we hit a browser TabItem, the remaining path segments refer to
            // web content inside that tab, not UIA children of the TabItem itself.
            // TabItem children are just tab UI (close button), not web content.
            if (IsTabItem(found))
            {
                // Step 1: Switch to this tab (focusless via SelectionItem pattern)
                try
                {
                    var si = found.Patterns.SelectionItem.PatternOrDefault;
                    if (si != null)
                    {
                        si.Select();
                        Console.WriteLine($"[GRAP] Tab portal: selected \"{found.Name}\"");
                        Thread.Sleep(600); // wait for web content to render after tab switch
                    }
                    else
                    {
                        // Fallback: invoke if SelectionItem not available (e.g. Edge favorites)
                        var inv = found.Patterns.Invoke.PatternOrDefault;
                        if (inv != null) inv.Invoke();
                        Console.WriteLine($"[GRAP] Tab portal: invoked \"{found.Name}\" (no SelectionItem)");
                        Thread.Sleep(600);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[GRAP] Tab switch failed: {ex.Message}");
                    return null;
                }

                // Step 2: Jump to the freshly-loaded Document[RootWebArea]
                // After tab switch, the active tab's web content appears as a
                // Document element with aid="RootWebArea" somewhere under windowRoot.
                var webRoot = FindRootWebArea(windowRoot, maxDepth: 10);
                if (webRoot == null)
                {
                    Console.Error.WriteLine("[GRAP] Tab portal: RootWebArea not found after tab switch");
                    return null;
                }

                Console.WriteLine($"[GRAP] Tab portal: jumped to Document \"{webRoot.Name}\"");
                current = webRoot;
                continue; // next segment searches inside the web content
            }

            current = found;
        }

        return current;
    }

    /// <summary>
    /// Check if element is a browser TabItem (Chrome/Edge tab strip).
    /// </summary>
    private static bool IsTabItem(AutomationElement el)
    {
        try
        {
            return el.Properties.ControlType.ValueOrDefault == ControlType.TabItem;
        }
        catch { return false; }
    }

    /// <summary>
    /// Find the active tab's web content: Document element with aid="RootWebArea".
    /// This is the entry point to all web-page UIA elements (buttons, links, edits, etc).
    /// Works for Chrome, Edge, and Electron apps (Claude, VS Code, Slack).
    /// </summary>
    private static AutomationElement? FindRootWebArea(AutomationElement root, int maxDepth)
    {
        return WalkFindFirst(root, maxDepth, depth: 0,
            predicate: el =>
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

    /// <summary>
    /// Generic predicate-based tree walk. Used by FindRootWebArea.
    /// </summary>
    private static AutomationElement? WalkFindFirst(
        AutomationElement parent, int maxDepth, int depth,
        Func<AutomationElement, bool> predicate)
    {
        if (depth > maxDepth) return null;
        try
        {
            foreach (var child in parent.FindAllChildren())
            {
                try { if (predicate(child)) return child; } catch { }
                var found = WalkFindFirst(child, maxDepth, depth + 1, predicate);
                if (found != null) return found;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Parse full grap pattern and resolve to (hwnd, uiaRoot).
    /// First '#' switches from Win32 mode to UIA mode:
    ///   Before '#': '/' separates Win32 child windows
    ///   After '#':  '/' and '#' are UIA scope separators
    /// Examples:
    ///   "영웅문"                     → main window only
    ///   "영웅문/실시간계좌"          → window → Win32 child (MDI)
    ///   "영웅문#실시간계좌"          → window → UIA scope
    ///   "영웅문/실시간계좌#Tab/탭"   → window → Win32 child → UIA "Tab" → "탭"
    /// </summary>
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

        // Walk Win32 children (segments[1..] before '#')
        for (int i = 1; i < win32Segments.Length; i++)
        {
            var child = Window.WindowFinder.FindChildByPattern(targetHwnd, win32Segments[i]);
            if (child == null)
                return (targetHwnd, null!, $"Win32 child not found: \"{win32Segments[i]}\"");
            targetHwnd = child.Handle;
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

    /// <summary>
    /// Container-first search: finds element by name, preferring containers over leaves.
    /// Two-pass approach:
    ///   Pass 1: Walk tree looking for container types (Window/Pane/Group/Tab/Custom) with matching name
    ///   Pass 2: If no container found, fall back to any element with matching name
    /// This ensures "잔고확인" matches the Window form, not the TabItem tab.
    /// </summary>
    private static AutomationElement? WalkFindContainerFirst(
        AutomationElement root, PatternMatcher nameMatcher, int maxDepth)
    {
        // Pass 1: containers only
        var container = WalkFindFirst(root, nameMatcher, maxDepth, depth: 0, containersOnly: true);
        if (container != null) return container;

        // Pass 2: any element (fallback for when scope target is a non-container)
        return WalkFindFirst(root, nameMatcher, maxDepth, depth: 0, containersOnly: false);
    }

    /// <summary>
    /// Recursive walk to find first element with matching Name or AutomationId.
    /// Uses FindAllChildren() recursion (not FindAllDescendants) to reach
    /// deep Electron/MFC subtrees that FlaUI's flat search misses.
    ///
    /// Matching order per element:
    ///   1. Name (display text, e.g. "새 채팅", "Google로 계속하기")
    ///   2. AutomationId (developer id, e.g. "email", "RootWebArea", "MenuBar")
    /// This allows "#email" to match aid="email" even when Name is empty.
    /// </summary>
    private static AutomationElement? WalkFindFirst(
        AutomationElement parent, PatternMatcher nameMatcher, int maxDepth, int depth,
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
                    bool nameMatch = !string.IsNullOrEmpty(name) && nameMatcher.IsMatch(name);
                    bool aidMatch = !nameMatch && !string.IsNullOrEmpty(aid) && nameMatcher.IsMatch(aid);

                    if (nameMatch || aidMatch)
                    {
                        if (!containersOnly)
                            return child;

                        // Container check: is this a container type?
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

                // Recurse into children
                var found = WalkFindFirst(child, nameMatcher, maxDepth, depth + 1, containersOnly);
                if (found != null) return found;
            }
        }
        catch { /* COM or timeout */ }

        return null;
    }
}
