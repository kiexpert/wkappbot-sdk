using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// Grap pattern helper: unified '/' and '#' separator for hierarchical scope narrowing.
///
/// Both '/' and '#' are equivalent separators that split grap into segments:
///   "영웅문/잔고확인"    → find window → narrow to "잔고확인" (Win32 child or UIA scope)
///   "영웅문#잔고확인"    → same as above — '#' and '/' are interchangeable
///   "영웅문/실현손익/당일" → window → child → UIA scope
///
/// Resolution order per segment (after the first = window title):
///   1. Try Win32 child window match (MDIClient → direct children)
///   2. If no Win32 child found → switch to UIA scope (container-first search)
///   3. Once in UIA mode, remaining segments stay in UIA mode
///
/// Each segment supports PatternMatcher syntax: substring (default), wildcards (*/?), regex: prefix.
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
    /// Find UIA element by name path within a root element.
    /// Supports multi-level paths with '/' or '#' separator:
    ///   "잔고확인"         → find container descendant with Name matching "잔고확인"
    ///   "잔고확인/Tab"     → find "잔고확인", then "Tab" within it
    /// Each segment uses PatternMatcher (wildcards, regex:, literal).
    /// Container-first: Window/Pane/Group preferred over TabItem/Button.
    /// </summary>
    public static AutomationElement? FindUiaScope(
        AutomationElement root, string uiaPath, int maxDepth = 15)
    {
        if (string.IsNullOrEmpty(uiaPath))
            return root;

        // Split by '/' or '#' for multi-level path
        var segments = uiaPath.Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);

        var current = root;
        foreach (var segment in segments)
        {
            // Create() does substring matching — no wrapping needed
            var matcher = PatternMatcher.Create(segment);
            var found = WalkFindContainerFirst(current, matcher, maxDepth);
            if (found == null)
                return null;
            current = found;
        }

        return current;
    }

    /// <summary>
    /// Parse full grap pattern and resolve to (hwnd, uiaRoot).
    /// '/' and '#' are equivalent separators — segments resolve as:
    ///   1st segment → FindByTitle (main window)
    ///   subsequent  → Win32 child first, UIA scope fallback (auto-switch)
    /// Once switched to UIA mode, remaining segments stay in UIA.
    /// Examples:
    ///   "영웅문"                → main window only
    ///   "영웅문/잔고확인"       → window → Win32 child (MDI)
    ///   "영웅문#실시간계좌"     → window → (no child) → UIA scope
    ///   "영웅문/실현손익/당일"  → window → child → UIA scope "당일"
    /// </summary>
    public static (IntPtr hwnd, AutomationElement root, string? error)? ResolveFullGrap(
        string grap, UIA3Automation automation)
    {
        if (string.IsNullOrEmpty(grap))
            return (IntPtr.Zero, null!, "Empty grap pattern");

        // Split by / or # — both are equivalent separators
        var segments = grap.Split(new[] { '/', '#' }, StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0)
            return (IntPtr.Zero, null!, "Empty grap pattern");

        // First segment = main window title
        var windows = Window.WindowFinder.FindByTitle(segments[0]);
        if (windows.Count == 0)
            return (IntPtr.Zero, null!, $"Window not found: \"{segments[0]}\"");

        var targetHwnd = windows[0].Handle;
        AutomationElement? root = null;
        bool inUiaMode = false;

        // Subsequent segments: Win32 child → UIA scope (auto-switch)
        for (int i = 1; i < segments.Length; i++)
        {
            var seg = segments[i];

            // Once in UIA mode, stay in UIA mode
            if (!inUiaMode)
            {
                var child = Window.WindowFinder.FindChildByPattern(targetHwnd, seg);
                if (child != null)
                {
                    targetHwnd = child.Handle;
                    continue; // Stay in Win32 mode
                }
                // No Win32 child found — switch to UIA mode
                inUiaMode = true;
            }

            // UIA scope narrowing
            if (root == null)
                root = automation.FromHandle(targetHwnd);

            var scoped = FindUiaScope(root, seg);
            if (scoped == null)
                return (targetHwnd, null!, $"Scope not found: \"{seg}\"");
            root = scoped;
        }

        // Ensure root is initialized
        if (root == null)
            root = automation.FromHandle(targetHwnd);

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
    /// Recursive walk to find first element with matching Name.
    /// Uses FindAllChildren() recursion (not FindAllDescendants) to reach
    /// deep Electron/MFC subtrees that FlaUI's flat search misses.
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
                    var name = child.Properties.Name.ValueOrDefault ?? "";
                    if (!string.IsNullOrEmpty(name) && nameMatcher.IsMatch(name))
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
