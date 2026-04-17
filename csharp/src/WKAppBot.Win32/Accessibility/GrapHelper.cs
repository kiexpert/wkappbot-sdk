using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// Grap pattern helper: '/' for Win32 hierarchy, '#' switches to UIA scope.
///
/// === Separator semantics ===
///   Before first '#':  '/' = Win32 child window separator
///   After first '#':   '/' and '#' are both UIA scope separators (equivalent)
///
/// === Synthesized full-path examples ===
///   "영웅문/실시간계좌"                -> Win32: 영웅문 -> child "실시간계좌"
///   "영웅문#실시간계좌"                -> UIA scope "실시간계좌" within 영웅문
///   "영웅문/실시간계좌#aid1000"        -> Win32 child -> UIA scope "aid1000"
///   "영웅문/child#uia1/uia2"          -> Win32 child -> UIA "uia1" -> "uia2"
///
/// === Browser tab portal (v2.1) ===
/// When a '#' segment matches a TabItem (browser tab), the grap engine:
///   1. Calls SelectionItem.Select() to switch to that tab (focusless!)
///   2. Waits for web content to load
///   3. Jumps to the sibling Document[RootWebArea] -- the tab's web content
///   4. Continues matching remaining segments inside the web page
///
/// This enables unified Win32 -> browser tab -> web element paths:
///   "Chrome#ChatGPT#모델"     -> Chrome window -> switch to ChatGPT tab -> find "모델" button
///   "Edge#YouTube#재생"       -> Edge window -> switch to YouTube tab -> find "재생" button
///   "Chrome#Gemini#새 채팅"   -> Chrome -> Gemini tab -> "새 채팅" button in web page
///
/// The same path works for Electron apps (Claude, VS Code) where UIA exposes
/// web content directly under Document[RootWebArea] without needing CDP.
///
/// === Pattern matching ===
/// Each segment supports PatternMatcher syntax:
///   plain text = substring Contains match (no wildcards needed!)
///   * / ? = glob wildcards
///   regex: prefix = regular expression
/// Container-first: prefers Window/Pane/Group elements over leaf elements (TabItem/Button).
///
/// === Public API (v2.1) ===
/// All core search/portal functions are public for reuse by other commands:
///   FindUiaScope()         -- multi-segment UIA path resolution with tab portal
///   FindByNameOrAid()      -- single element search by Name or AutomationId
///   FindRootWebArea()      -- locate Document[RootWebArea] for web content access
///   SwitchToTab()          -- focusless tab switch via SelectionItem/Invoke
///   IsTabItem()            -- check if element is a browser tab
///   WalkTree()             -- generic predicate-based tree walk
///   SplitGrap()            -- parse grap into Win32 + UIA segments
///   ResolveFullGrap()      -- end-to-end grap -> (hwnd, element) resolution
/// </summary>
public static class GrapHelper
{
    // -- 공용 a11y 노드 포맷터 ----------------------------------------------

    /// <summary>
    /// Short tag for overlay/console labels: "Type_aid" / "Type_N" / "Type".
    /// No text/Name -- only AutomationId or sibling index as fallback.
    /// isDynamic=true -> prefix "Dy" (experience DB only, no system UIA).
    /// </summary>
    public static string FormatNodeTag(string controlType, string? automationId, int siblingIndex = 0, bool isDynamic = false)
    {
        var prefix = isDynamic ? "Dy" : "";
        var aid = automationId;
        // Truncate long hyphenated IDs to first segment: "active-frame" -> "active", UUID -> first 8 chars
        if (!string.IsNullOrWhiteSpace(aid))
        {
            var dashIdx = aid.IndexOf('-');
            if (dashIdx > 0) aid = aid[..dashIdx];
        }
        var id = !string.IsNullOrWhiteSpace(aid) ? aid
               : siblingIndex > 0 ? $"{siblingIndex}th"
               : "";
        return string.IsNullOrEmpty(id) ? $"{prefix}{controlType}" : $"{prefix}{controlType}_{id}";
    }

    /// <summary>
    /// Coordinate-based temp tag for UIA-blind elements: "NodeXY(cx,cy)".
    /// Used when ControlType is completely inaccessible -- center coords identify the element.
    /// </summary>
    public static string FormatNodeTag(int cx, int cy) => $"FindNodeXY({cx},{cy})";

    /// <summary>
    /// Build absolute UIA tag path from element to window root.
    /// E.g. "Pane_1th/Document_2th/Edit_1052"
    /// Walks parent chain via TreeWalker, finds sibling index at each level by RuntimeId match.
    /// </summary>
    public static string BuildAbsoluteTagPath(AutomationElement element, FlaUI.Core.ITreeWalker walker, int maxDepth = 10)
    {
        var parts = new List<string>();
        var node = element;
        for (int d = 0; d < maxDepth; d++)
        {
            string ct = "?", aid = "";
            try { ct = node.ControlType.ToString(); } catch { }
            try { aid = node.AutomationId ?? ""; } catch { }
            if (ct == "Window") break;
            // Abbreviate type to 3 chars -- resolution uses AID after '_', type prefix is cosmetic
            if (ct.Length > 3) ct = ct[..3];

            int sibIdx = 0;
            try
            {
                var parent = walker.GetParent(node);
                if (parent != null)
                {
                    var rid = node.Properties.RuntimeId.ValueOrDefault;
                    if (rid != null)
                    {
                        var siblings = parent.FindAllChildren();
                        for (int si = 0; si < siblings.Length; si++)
                        {
                            try
                            {
                                var sRid = siblings[si].Properties.RuntimeId.ValueOrDefault;
                                if (sRid != null && rid.SequenceEqual(sRid)) { sibIdx = si + 1; break; }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }

            parts.Add(FormatNodeTag(ct, aid, sibIdx));
            try { node = walker.GetParent(node); } catch { break; }
            if (node == null) break;
        }
        if (parts.Count == 0) return FormatNodeTag("?", null);
        parts.Reverse();
        // Compress consecutive identical bare types: Gro/Gro/Gro/Gro -> Gro**/
        // ** means "any depth of same type" -- reuses existing ** glob wildcard in grap path search.
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < parts.Count; i++)
        {
            if (i > 0) sb.Append('/');
            sb.Append(parts[i]);
            // Skip consecutive identical bare-type tags (no aid, no sibIdx suffix)
            // "Gro" == "Gro" but "Gro_1th" != "Gro"
            var baseType = parts[i].Contains('_') ? null : parts[i];
            if (baseType != null)
            {
                int skip = 0;
                while (i + 1 + skip < parts.Count && parts[i + 1 + skip] == baseType)
                    skip++;
                if (skip > 0) { sb.Append("**"); i += skip; }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Format a11y node label: "ControlType(AutomationId)" or "ControlType("Name")".
    /// Used everywhere a11y nodes are displayed: windows, inspect, find, read, etc.
    /// </summary>
    /// <summary>String-based overload for pre-extracted info (ElementAtPointInfo etc.)</summary>
    /// <summary>
    /// Canonical tag format: &lt;TypeIdOrName attrs&gt;
    ///   automationId present -> &lt;ButtonOK&gt;
    ///   name only            -> &lt;Button'확인'&gt;
    ///   neither              -> &lt;Button2&gt; (siblingIndex)
    ///   attrs: ltwh=x,y,w,h  actions="Invoke,..."  text="..."
    /// </summary>
    public static string FormatNodeLabel(string controlType, string? automationId, string? name,
        int siblingIndex = 0, System.Drawing.Rectangle? rect = null, List<string>? actions = null, string? text = null)
    {
        var idx = siblingIndex > 0 ? siblingIndex.ToString() : "";
        // "Type name" format -- no XML angle brackets; name quoted, aid unquoted, number when nameless
        string label;
        if (!string.IsNullOrEmpty(automationId))
            label = $"{controlType} {automationId}";
        else if (!string.IsNullOrEmpty(name))
        {
            var n = name.Length > 40 ? name[..37] + "..." : name;
            label = $"{controlType} \"{n}\"";
        }
        else
            label = $"{controlType}{idx}";
        var parts = new List<string>(4) { label };
        if (rect.HasValue) parts.Add($"ltwh={rect.Value.X},{rect.Value.Y},{rect.Value.Width},{rect.Value.Height}");
        if (actions != null && actions.Count > 0) parts.Add($"actions=\"{string.Join(",", actions)}\"");
        if (text != null)
        {
            var escaped = text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
            if (escaped.Length > 50) escaped = escaped[..47] + "...";
            parts.Add($"text=\"{escaped}\"");
        }
        return string.Join(" ", parts);
    }

    /// <summary>AutomationElement overload -- delegates to canonical string overload.</summary>
    public static string FormatNodeLabel(AutomationElement el, int siblingIndex = 0, bool includeRect = false, bool includeText = false)
    {
        var ct  = el.Properties.ControlType.ValueOrDefault.ToString();
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
        var name = el.Properties.Name.ValueOrDefault ?? "";
        System.Drawing.Rectangle? rect = null;
        if (includeRect)
            try { var r = el.Properties.BoundingRectangle.ValueOrDefault; rect = new((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height); } catch { }
        string? text = null;
        if (includeText)
        {
            text = "";
            try { text = el.Patterns.Value.PatternOrDefault?.Value.ValueOrDefault ?? ""; } catch { }
            if (string.IsNullOrEmpty(text)) text = name;
            if (string.IsNullOrEmpty(text)) text = null;
        }
        return FormatNodeLabel(ct, aid, name, siblingIndex, rect, null, text);
    }

    /// <summary>
    /// Get Win32 focus leaf absolute path for any window (fast, no UIA, &lt;1ms).
    /// Returns: "TopLevel(title)/Child(class)/FocusLeaf(class)" or null if no focus.
    /// </summary>
    public static string? GetFocusPath(IntPtr hwnd)
    {
        var tid = Native.NativeMethods.GetWindowThreadProcessId(hwnd, out _);
        if (tid == 0) return null;
        var gti = new Native.NativeMethods.GUITHREADINFO
            { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<Native.NativeMethods.GUITHREADINFO>() };
        if (!Native.NativeMethods.GetGUIThreadInfo(tid, ref gti) || gti.hwndFocus == IntPtr.Zero)
            return null;

        var parts = new List<string>();
        var cur = gti.hwndFocus;
        int safety = 20;
        while (cur != IntPtr.Zero && safety-- > 0)
        {
            var cls = new System.Text.StringBuilder(128);
            Native.NativeMethods.GetClassNameW(cur, cls, 128);
            var title = Native.NativeMethods.GetWindowTextW(cur);
            var label = cls.ToString();
            if (title.Length > 0) label += $"(\"{(title.Length > 20 ? title[..17] + "..." : title)}\")";
            parts.Add(label);
            if (cur == hwnd) break;
            cur = Native.NativeMethods.GetParent(cur);
            if (cur == IntPtr.Zero) break;
        }
        parts.Reverse();
        return parts.Count > 0 ? string.Join("/", parts) : null;
    }

    /// <summary>Close tag: &lt;/Type{N}id&gt;</summary>
    public static string FormatCloseTag(AutomationElement el, int siblingIndex = 0)
    {
        var ct = el.Properties.ControlType.ValueOrDefault;
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
        var name = el.Properties.Name.ValueOrDefault ?? "";
        var idx = siblingIndex > 0 ? siblingIndex.ToString() : "";
        var id = aid.Length > 0 ? aid : (name.Length > 20 ? name[..17] + "..." : name);
        return $"</{ct}{idx}{id}>";
    }

    public static string FormatCloseTag(string controlType, string? automationId, string? name, int siblingIndex = 0)
    {
        var idx = siblingIndex > 0 ? siblingIndex.ToString() : "";
        var id = !string.IsNullOrEmpty(automationId) ? automationId
               : !string.IsNullOrEmpty(name) ? (name.Length > 20 ? name[..17] + "..." : name)
               : "";
        return $"</{controlType}{idx}{id}>";
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
            // Count sibling index (same ControlType under same parent)
            int sibIdx = 1;
            try
            {
                var parent = el.Parent;
                if (parent != null)
                {
                    var ct = el.Properties.ControlType.ValueOrDefault;
                    var siblings = parent.FindAllChildren();
                    int sameTypeCount = 0;
                    foreach (var sib in siblings)
                    {
                        if (sib.Properties.ControlType.ValueOrDefault == ct)
                        {
                            sameTypeCount++;
                            if (sib.Equals(el)) { sibIdx = sameTypeCount; break; }
                        }
                    }
                    // Only number if >1 same type siblings
                    if (sameTypeCount <= 1 && siblings.Length > 0)
                    {
                        int total = siblings.Count(s => s.Properties.ControlType.ValueOrDefault == ct);
                        if (total <= 1) sibIdx = 0; // unique -> no number
                    }
                }
            }
            catch { sibIdx = 0; }
            parts.Add(FormatNodeLabel(el, sibIdx));
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
    /// Find the keyboard-focused LEAF element under a root.
    /// Strategy: (1) Foreground -> FocusedElement (accurate). (2) Background -> TreeWalker HasKeyboardFocus.
    /// </summary>
    public static AutomationElement? FindFocusedLeaf(FlaUI.UIA3.UIA3Automation uia, AutomationElement root, IntPtr hwnd = default)
    {
        // Strategy 1: If this is the foreground window, FocusedElement is accurate
        try
        {
            var fg = WKAppBot.Win32.Native.NativeMethods.GetForegroundWindow();
            if (hwnd != default && hwnd == fg)
            {
                var focused = uia.FocusedElement();
                if (focused != null) return focused;
            }
        }
        catch { }

        // Strategy 2: TreeWalker -- walk children for HasKeyboardFocus
        try
        {
            if (root.Properties.HasKeyboardFocus.ValueOrDefault) return root;
            var walker = uia.TreeWalkerFactory.GetControlViewWalker();
            var found = WalkForFocusIterative(walker, root);
            if (found != null) return found;
        }
        catch { }

        // Strategy 3: FindAll descendants with HasKeyboardFocus (broader, slower)
        try
        {
            var cond = new FlaUI.Core.Conditions.PropertyCondition(
                uia.PropertyLibrary.Element.HasKeyboardFocus, true);
            var results = root.FindAll(FlaUI.Core.Definitions.TreeScope.Descendants, cond);
            if (results.Length > 0) return results[0];
        }
        catch { }

        return root; // fallback: return root itself
    }

    private static AutomationElement? WalkForFocusIterative(dynamic walker, AutomationElement root)
    {
        var stack = new Stack<AutomationElement>();
        stack.Push(root);
        int safety = 200;
        while (stack.Count > 0 && safety-- > 0)
        {
            var el = stack.Pop();
            try { if (el.Properties.HasKeyboardFocus.ValueOrDefault) return el; } catch { continue; }
            try
            {
                var child = walker.GetFirstChild(el);
                while (child != null)
                {
                    stack.Push(child);
                    child = walker.GetNextSibling(child);
                }
            }
            catch { }
        }
        return null;
    }

    // Container types: these are preferred when searching for scope roots
    private static readonly HashSet<ControlType> ContainerTypes = new()
    {
        ControlType.Window, ControlType.Pane, ControlType.Group,
        ControlType.Tab, ControlType.Document, ControlType.Custom
    };

    // =======================================================================
    // CSS vs UIA pattern detection
    // =======================================================================

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
        // But only common HTML tags -- otherwise default to UIA Name
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

    // =======================================================================
    // Public API: Grap parsing
    // =======================================================================

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

        // Find '#' that separates window grap from UIA scope.
        // JSON5 {..} block: skip quoted strings so '#' inside values doesn't split early.
        var hashIdx = -1;
        if (grap.StartsWith('{'))
        {
            int depth = 0; bool inQ = false; char qc = '\0';
            for (int ci = 0; ci < grap.Length; ci++)
            {
                var ch = grap[ci];
                if (inQ) { if (ch == qc) inQ = false; continue; }
                if (ch == '\'' || ch == '"') { inQ = true; qc = ch; continue; }
                if (ch == '{') depth++;
                else if (ch == '}' && --depth == 0) { hashIdx = grap.IndexOf('#', ci + 1); break; }
            }
        }
        if (hashIdx < 0) hashIdx = grap.IndexOf('#');
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
    /// matchCount: out parameter -- total matching windows found.
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

        // First segment = main window title -- may match multiple windows
        var windows = Window.WindowFinder.FindWindows(win32Segments[0]);
        matchCount = windows.Count;
        if (windows.Count == 0)
            return (IntPtr.Zero, null!, $"Window not found: \"{win32Segments[0]}\"");

        if (windowIndex >= windows.Count)
            return (IntPtr.Zero, null!, $"--nth {windowIndex + 1} but only {windows.Count} match(es) for \"{win32Segments[0]}\"");

        var targetHwnd = windows[windowIndex].Handle;

        // Trailing '/' (no UIA path) or trailing '#' (empty UIA scope) = drill to focused child + UIA leaf
        // e.g. "*메모장*/"  -> focused child hwnd + focused UIA element
        //      "*메모장*#"  -> same (# with empty scope = focus drill)
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
            Console.WriteLine($"[GRAP] focus-drill -> hwnd=0x{targetHwnd:X8}");

            try
            {
                var focusedEl = automation.FocusedElement();
                if (focusedEl != null)
                {
                    var fname = focusedEl.Properties.Name.ValueOrDefault ?? "";
                    var ftype = "?";
                    try { ftype = focusedEl.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                    Console.WriteLine($"[GRAP] focus-drill -> UIA [{ftype}] \"{fname}\"");
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

    // =======================================================================
    // Public API: UIA scope resolution (with tab portal)
    // =======================================================================

    /// <summary>
    /// Find UIA element by name path within a root element.
    /// Supports multi-level paths with '/' or '#' separator (both equivalent).
    ///
    /// === Browser Tab Portal ===
    /// When a segment matches a TabItem (browser tab), the engine:
    ///   1. SelectionItem.Select() -> switch tab (focusless)
    ///   2. Navigate back to the window root
    ///   3. Find Document[RootWebArea] -> the newly-active tab's web content
    ///   4. Continue matching remaining segments inside that Document
    ///
    /// Why? In Chrome/Edge UIA tree, TabItem and RootWebArea are siblings,
    /// not parent-child. TabItem lives under [Tab], web content under [Pane]:
    ///   Pane (browser frame)
    ///   +-- Pane -> Document "Google Gemini" [RootWebArea]  <- web content
    ///   +-- Tab
    ///   |   +-- TabItem "ChatGPT"    <- tab UI (only has Close button)
    ///   |   +-- TabItem "Gemini"
    ///   +-- ToolBar (address bar)
    ///
    /// So "Chrome#ChatGPT#Send" means:
    ///   find TabItem "ChatGPT" -> select it -> jump to RootWebArea -> find "Send"
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
            // "?" = unknown segment from hack-hover path capture -> skip narrowing, keep current scope
            if (segment == "?") continue;

            var found = FindByNameOrAid(current, segment, maxDepth);
            if (found == null)
                return null;

            // === Tab Portal: TabItem -> select tab -> jump to RootWebArea ===
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

    // =======================================================================
    // Public API: Element search
    // =======================================================================

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

        // Fast path: plain literal (no wildcards/regex) -> native UIA FindFirst, O(log n) vs BFS O(n)
        if (!PatternMatcher.IsPattern(pattern))
        {
            var fast = TryNativeFastFind(root, pattern);
            if (fast != null) return fast;
        }

        // Pass 1: containers only (Window/Pane/Group/Tab/Document/Custom)
        var container = WalkFindByMatcher(root, matcher, maxDepth, depth: 0, containersOnly: true);
        if (container != null) return container;

        // Pass 2: any element (fallback for leaf elements like Button/Edit/TabItem)
        return WalkFindByMatcher(root, matcher, maxDepth, depth: 0, containersOnly: false);
    }

    // Ordinal suffix pattern: "1st","2nd","3rd","4th"..."10th" etc.
    private static readonly System.Text.RegularExpressions.Regex _ordinalRx =
        new(@"^\d+(st|nd|rd|th)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase |
            System.Text.RegularExpressions.RegexOptions.Compiled);

    /// <summary>
    /// Native UIA FindFirst fast-path for plain literal patterns.
    /// Tries: exact Name -> AID portion of "Type_aid" tag -> exact AID.
    /// Falls through returning null if nothing found (caller does BFS).
    /// </summary>
    private static AutomationElement? TryNativeFastFind(AutomationElement root, string pattern)
    {
        try
        {
            // Exact Name match
            var byName = root.FindFirstDescendant(cf => cf.ByName(pattern));
            if (byName != null) return byName;

            // FormatNodeTag format: "Type_aid" -> extract AID portion after first '_'
            var uidx = pattern.IndexOf('_');
            if (uidx > 0)
            {
                var aidPart = pattern[(uidx + 1)..];
                // Skip ordinal suffixes like "1st", "2nd", "10th" (sibling-index only, no AID)
                if (aidPart.Length > 3 && !_ordinalRx.IsMatch(aidPart))
                {
                    var byAidPart = root.FindFirstDescendant(cf => cf.ByAutomationId(aidPart));
                    if (byAidPart != null) return byAidPart;
                }
            }

            // Exact AID match with full pattern (e.g., bare AutomationId search)
            var byAid = root.FindFirstDescendant(cf => cf.ByAutomationId(pattern));
            if (byAid != null) return byAid;
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Find ALL UIA elements matching a name/aid pattern string, sorted by coverage descending.
    /// Coverage = key length / matched text length -- shorter/exact name = higher coverage.
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
                    bool nameMatch = !string.IsNullOrEmpty(name) && matcher.MatchAny(name, aid);
                    bool aidMatch = !nameMatch && !string.IsNullOrEmpty(aid) && matcher.MatchAny(aid, name);
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

    // =======================================================================
    // Public API: Browser tab portal
    // =======================================================================

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

    // =======================================================================
    // Public API: Generic tree walk
    // =======================================================================

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

    // =======================================================================
    // Internal: tree walk implementations
    // =======================================================================

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
                    bool nameMatch = !string.IsNullOrEmpty(name) && matcher.MatchAny(name, aid);
                    bool aidMatch = !nameMatch && !string.IsNullOrEmpty(aid) && matcher.MatchAny(aid, name);

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

    // -- FindNodeXY: screen coordinate -> deepest UIA element --

    /// <summary>
    /// Result of FindNodeXY: the deepest UIA element at screen (x, y) + parent chain info.
    /// </summary>
    public sealed class NodeXYResult
    {
        public AutomationElement Element { get; init; } = null!;
        public string Tag { get; init; } = "";
        public string AbsolutePath { get; init; } = "";
        public System.Drawing.Rectangle Bounds { get; init; }
        /// <summary>Caret rect in screen coords (from GetGUIThreadInfo). Empty if no caret.</summary>
        public System.Drawing.Rectangle CaretRect { get; init; }
        /// <summary>Selection bounding rects from UIA TextPattern (screen coords). Empty if no selection.</summary>
        public System.Drawing.Rectangle[] SelectionRects { get; init; } = Array.Empty<System.Drawing.Rectangle>();
        /// <summary>Union of all selection rects (screen coords). Empty if no selection.</summary>
        public System.Drawing.Rectangle SelectionBounds { get; init; }
        public IntPtr FocusHwnd { get; init; }
    }

    /// <summary>
    /// Find the deepest UIA element containing screen point (x, y).
    /// Uses UIA FromPoint for initial hit, then drills into children by bounds containment.
    /// Also retrieves caret info via GetGUIThreadInfo.
    /// </summary>
    /// <param name="screenX">Screen X of the point (or left edge of rect to contain).</param>
    /// <param name="screenY">Screen Y of the point (or top edge of rect to contain).</param>
    /// <param name="containWidth">If > 0, find the deepest node that fully contains this rect width.</param>
    /// <param name="containHeight">If > 0, find the deepest node that fully contains this rect height.</param>
    public static NodeXYResult? FindNodeXY(int screenX, int screenY, UIA3Automation? uia = null,
        int containWidth = 0, int containHeight = 0)
    {
        bool ownUia = uia == null;
        try
        {
            uia ??= new UIA3Automation();
            try
            {
                var hit = uia.FromPoint(new System.Drawing.Point(screenX, screenY));
                if (hit == null) return null;

                // Rect to contain (point or caret rect)
                int rRight = screenX + Math.Max(0, containWidth);
                int rBottom = screenY + Math.Max(0, containHeight);

                // Drill down: find the smallest (deepest) child fully containing the rect
                var best = hit;
                for (int depth = 0; depth < 15; depth++)
                {
                    AutomationElement? deeper = null;
                    double bestArea = double.MaxValue;
                    try
                    {
                        var children = best.FindAllChildren();
                        foreach (var c in children)
                        {
                            try
                            {
                                var cr = c.BoundingRectangle;
                                if (cr.Width < 2 || cr.Height < 2) continue;
                                // Must fully contain the rect
                                if (screenX >= cr.X && rRight <= cr.X + cr.Width &&
                                    screenY >= cr.Y && rBottom <= cr.Y + cr.Height)
                                {
                                    double area = cr.Width * cr.Height;
                                    if (area < bestArea)
                                    {
                                        bestArea = area;
                                        deeper = c;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    catch { break; }
                    if (deeper == null) break;
                    best = deeper;
                }

                // Build tag + absolute path
                string ct = "?", aid = "";
                try { ct = best.ControlType.ToString(); } catch { }
                try { aid = best.AutomationId ?? ""; } catch { }
                var tag = FormatNodeTag(ct, aid);
                string absPath;
                try { absPath = BuildAbsoluteTagPath(best, uia.TreeWalkerFactory.GetRawViewWalker()); } catch { absPath = tag; }
                var bounds = best.BoundingRectangle;

                // Caret info from GetGUIThreadInfo
                var caretRect = System.Drawing.Rectangle.Empty;
                IntPtr focusHwnd = IntPtr.Zero;
                try
                {
                    var gti = new NativeMethods.GUITHREADINFO
                    { cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
                    if (NativeMethods.GetGUIThreadInfo(0, ref gti))
                    {
                        focusHwnd = gti.hwndFocus;
                        if (gti.rcCaret.Right > gti.rcCaret.Left && gti.rcCaret.Bottom > gti.rcCaret.Top)
                        {
                            // rcCaret is client coords of hwndFocus -> convert to screen
                            var caretPt = new POINT { X = gti.rcCaret.Left, Y = gti.rcCaret.Top };
                            NativeMethods.ClientToScreen(gti.hwndFocus, ref caretPt);
                            caretRect = new System.Drawing.Rectangle(
                                caretPt.X, caretPt.Y,
                                gti.rcCaret.Right - gti.rcCaret.Left,
                                gti.rcCaret.Bottom - gti.rcCaret.Top);
                        }
                    }
                }
                catch { }

                return new NodeXYResult
                {
                    Element = best,
                    Tag = tag,
                    AbsolutePath = absPath,
                    Bounds = bounds,
                    CaretRect = caretRect,
                    FocusHwnd = focusHwnd,
                };
            }
            finally { if (ownUia) uia.Dispose(); }
        }
        catch { return null; }
    }

    /// <summary>
    /// Find the UIA node at the current keyboard caret position.
    /// Combines GetGUIThreadInfo (caret coords) + FindNodeXY (UIA drill-down).
    /// Returns null if no caret is active.
    /// </summary>
    public static NodeXYResult? FindNodeAtCaret(UIA3Automation? uia = null)
    {
        bool ownUia = uia == null;
        try
        {
            uia ??= new UIA3Automation();
            var gti = new NativeMethods.GUITHREADINFO
            { cbSize = Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            if (!NativeMethods.GetGUIThreadInfo(0, ref gti)) return null;
            if (gti.hwndFocus == IntPtr.Zero) return null;

            // rcCaret is client coords -> convert to screen
            var pt = new POINT { X = gti.rcCaret.Left, Y = gti.rcCaret.Top };
            NativeMethods.ClientToScreen(gti.hwndFocus, ref pt);
            int caretW = gti.rcCaret.Right - gti.rcCaret.Left;
            int caretH = gti.rcCaret.Bottom - gti.rcCaret.Top;

            // Find node containing the caret
            var result = FindNodeXY(pt.X, pt.Y, uia, containWidth: caretW, containHeight: caretH);
            if (result == null) return null;

            // Query TextPattern selection rects -> union rect -> re-find if selection is larger
            var selRects = Array.Empty<System.Drawing.Rectangle>();
            var selBounds = System.Drawing.Rectangle.Empty;
            try
            {
                if (result.Element.Patterns.Text.IsSupported)
                {
                    var ranges = result.Element.Patterns.Text.Pattern.GetSelection();
                    if (ranges is { Length: > 0 })
                    {
                        var brs = ranges[0].GetBoundingRectangles();
                        if (brs is { Length: > 0 })
                        {
                            selRects = brs.Select(r => new System.Drawing.Rectangle(
                                (int)r.X, (int)r.Y, (int)r.Width, (int)r.Height)).ToArray();
                            // Union of all rects
                            int minX = selRects.Min(r => r.X), minY = selRects.Min(r => r.Y);
                            int maxX = selRects.Max(r => r.Right), maxY = selRects.Max(r => r.Bottom);
                            selBounds = new System.Drawing.Rectangle(minX, minY, maxX - minX, maxY - minY);

                            // Selection info is captured for callers but node search
                            // always uses caret rect -- most reliable anchor point.
                        }
                    }
                }
            }
            catch { }

            return new NodeXYResult
            {
                Element = result.Element,
                Tag = result.Tag,
                AbsolutePath = result.AbsolutePath,
                Bounds = result.Bounds,
                CaretRect = result.CaretRect,
                SelectionRects = selRects,
                SelectionBounds = selBounds,
                FocusHwnd = result.FocusHwnd,
            };
        }
        catch { return null; }
        finally { if (ownUia && uia != null) uia.Dispose(); }
    }
}
