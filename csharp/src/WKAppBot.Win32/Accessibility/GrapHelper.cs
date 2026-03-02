using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// Grap pattern helper: parses "windowGrap#uiaPath" syntax for UIA scope narrowing.
///
/// The '#' separator splits window finding (Win32) from UIA element scoping:
///   *영웅문*#*잔고확인*        → find window → narrow UIA root to "잔고확인"
///   *영웅문*#*잔고확인*/*Tab*  → find window → narrow to "잔고확인" → then "Tab" inside it
///
/// After '#', both '/' and '#' separate UIA hierarchy levels (multi-level path).
/// Each segment supports PatternMatcher syntax: wildcards (*/?), regex: prefix, or literal.
///
/// Container-first: prefers Window/Pane/Group elements over leaf elements (TabItem/Button)
/// when matching by name. This ensures #scope finds forms/containers, not their leaf children.
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
    /// Split "windowGrap#uiaPath" into components.
    /// Returns (windowGrap, null) if no '#' present (backward compatible).
    /// </summary>
    public static (string windowGrap, string? uiaPath) SplitHash(string grap)
    {
        if (string.IsNullOrEmpty(grap))
            return (grap, null);

        var hashIdx = grap.IndexOf('#');
        if (hashIdx < 0)
            return (grap, null);

        var windowPart = grap[..hashIdx];
        var uiaPart = hashIdx + 1 < grap.Length ? grap[(hashIdx + 1)..] : null;

        // Empty window part → use original (shouldn't happen, but safety)
        if (string.IsNullOrEmpty(windowPart))
            return (grap, null);

        return (windowPart, string.IsNullOrEmpty(uiaPart) ? null : uiaPart);
    }

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
            // Smart substring matching: ensure *...* for # scope
            var pattern = PatternMatcher.EnsureSubstring(segment);
            var matcher = PatternMatcher.Create(pattern);
            var found = WalkFindContainerFirst(current, matcher, maxDepth);
            if (found == null)
                return null;
            current = found;
        }

        return current;
    }

    /// <summary>
    /// Convenience: resolve full grap with optional '#' scope.
    /// Returns narrowed UIA root element, or original root if no '#'.
    /// </summary>
    public static AutomationElement? ResolveScope(
        UIA3Automation automation, IntPtr windowHwnd, string? uiaPath)
    {
        var root = automation.FromHandle(windowHwnd);
        if (string.IsNullOrEmpty(uiaPath))
            return root;

        return FindUiaScope(root, uiaPath);
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
