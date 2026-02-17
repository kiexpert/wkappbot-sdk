using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// UI Automation element locator using FlaUI.
/// Primary locator in the Chain of Responsibility (before Vision and Coordinate).
/// </summary>
public sealed class UiaLocator : IDisposable
{
    private readonly UIA3Automation _automation;

    public UiaLocator()
    {
        _automation = new UIA3Automation();
    }

    /// <summary>
    /// Find element by AutomationId within a window.
    /// </summary>
    public AutomationElement? FindByAutomationId(IntPtr hWnd, string automationId)
    {
        var element = _automation.FromHandle(hWnd);
        return element.FindFirstDescendant(
            new PropertyCondition(
                _automation.PropertyLibrary.Element.AutomationId,
                automationId));
    }

    /// <summary>
    /// Find element by Name within a window.
    /// </summary>
    public AutomationElement? FindByName(IntPtr hWnd, string name)
    {
        var element = _automation.FromHandle(hWnd);
        return element.FindFirstDescendant(
            new PropertyCondition(
                _automation.PropertyLibrary.Element.Name,
                name));
    }

    /// <summary>
    /// Find element by control type name within a window.
    /// </summary>
    public AutomationElement? FindByControlType(IntPtr hWnd, string controlTypeName, string? name = null)
    {
        var element = _automation.FromHandle(hWnd);
        var cf = _automation.ConditionFactory;

        var conditions = new List<PropertyCondition>();
        // FlaUI ControlType matching by name
        if (Enum.TryParse<FlaUI.Core.Definitions.ControlType>(controlTypeName, true, out var ct))
        {
            conditions.Add(new PropertyCondition(
                _automation.PropertyLibrary.Element.ControlType, (int)ct));
        }

        if (name != null)
        {
            conditions.Add(new PropertyCondition(
                _automation.PropertyLibrary.Element.Name, name));
        }

        if (conditions.Count == 0) return null;

        var combined = conditions.Count == 1
            ? (ConditionBase)conditions[0]
            : new AndCondition(conditions.ToArray());

        return element.FindFirstDescendant(combined);
    }

    /// <summary>
    /// Find element by Name pattern (wildcard or regex) within a window.
    /// Walks all descendants and matches against pattern.
    ///
    /// Pattern syntax:
    ///   "exact text"      — exact match (handled by FindByName, not this)
    ///   "*partial*"       — wildcard (* = 0+ chars, ? = 1 char)
    ///   "regex:pattern"   — regular expression
    /// </summary>
    public AutomationElement? FindByNamePattern(IntPtr hWnd, string pattern)
    {
        var matcher = PatternMatcher.Create(pattern);
        var root = _automation.FromHandle(hWnd);
        return FindFirstDescendantMatching(root, el =>
        {
            try { return matcher.IsMatch(el.Name ?? ""); } catch { return false; }
        });
    }

    /// <summary>
    /// Find element by AutomationId pattern (wildcard or regex).
    /// </summary>
    public AutomationElement? FindByAutomationIdPattern(IntPtr hWnd, string pattern)
    {
        var matcher = PatternMatcher.Create(pattern);
        var root = _automation.FromHandle(hWnd);
        return FindFirstDescendantMatching(root, el =>
        {
            try { return matcher.IsMatch(el.AutomationId ?? ""); } catch { return false; }
        });
    }

    /// <summary>
    /// Walk UIA tree and return first element matching a predicate.
    /// BFS order for predictable results (closest to root first).
    /// </summary>
    private static AutomationElement? FindFirstDescendantMatching(
        AutomationElement root, Func<AutomationElement, bool> predicate, int maxDepth = 6)
    {
        var queue = new Queue<(AutomationElement el, int depth)>();
        queue.Enqueue((root, 0));

        while (queue.Count > 0)
        {
            var (el, depth) = queue.Dequeue();

            if (depth > 0 && predicate(el))
                return el;

            if (depth >= maxDepth) continue;

            try
            {
                var children = el.FindAllChildren();
                if (children != null)
                    foreach (var child in children)
                        queue.Enqueue((child, depth + 1));
            }
            catch { /* skip inaccessible subtrees */ }
        }
        return null;
    }

    /// <summary>
    /// Combined locator: try AutomationId first, then Name, then ControlType.
    /// Supports pattern matching: wildcard (*,?) and regex (prefix "regex:").
    /// Returns (element, method_used) or (null, null).
    /// </summary>
    public (AutomationElement? element, string? method) FindElement(
        IntPtr hWnd,
        string? automationId,
        string? name,
        string? controlType)
    {
        // ── AutomationId: exact first, pattern fallback ──
        if (automationId != null)
        {
            if (PatternMatcher.IsPattern(automationId))
            {
                var el = FindByAutomationIdPattern(hWnd, automationId);
                if (el != null) return (el, "automation_id_pattern");
            }
            else
            {
                var el = FindByAutomationId(hWnd, automationId);
                if (el != null) return (el, "automation_id");
            }
        }

        // ── Name: exact first, pattern fallback ──
        if (name != null)
        {
            if (PatternMatcher.IsPattern(name))
            {
                var el = FindByNamePattern(hWnd, name);
                if (el != null) return (el, "name_pattern");
            }
            else
            {
                var el = FindByName(hWnd, name);
                if (el != null) return (el, "name");
            }
        }

        // ── ControlType ──
        if (controlType != null)
        {
            var el = FindByControlType(hWnd, controlType, name);
            if (el != null) return (el, "control_type");
        }

        return (null, null);
    }

    /// <summary>
    /// Get the bounding rectangle of an automation element in screen coordinates.
    /// </summary>
    public static (int x, int y, int width, int height)? GetBoundingRect(AutomationElement element)
    {
        var rect = element.BoundingRectangle;
        if (rect.IsEmpty) return null;
        return ((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
    }

    /// <summary>
    /// Get the center point of an element in screen coordinates.
    /// </summary>
    public static (int x, int y)? GetCenter(AutomationElement element)
    {
        var bounds = GetBoundingRect(element);
        if (bounds == null) return null;
        var (x, y, w, h) = bounds.Value;
        return (x + w / 2, y + h / 2);
    }

    /// <summary>
    /// Click an automation element using its Invoke pattern (no mouse needed).
    /// Returns true if invoked, false if no invoke pattern.
    /// </summary>
    public static bool TryInvoke(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Invoke;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Invoke();
                return true;
            }
        }
        catch { /* not supported */ }
        return false;
    }

    /// <summary>
    /// Get the text value of an element (Name or Value pattern).
    /// </summary>
    public static string? GetText(AutomationElement element)
    {
        // Try Value pattern first
        try
        {
            var vp = element.Patterns.Value;
            if (vp.IsSupported)
                return vp.Pattern.Value.Value;
        }
        catch { }

        // Fall back to Name
        return element.Name;
    }

    /// <summary>
    /// Get the UI Automation element at a screen point.
    /// Returns element info or null if nothing found.
    /// </summary>
    public ElementAtPointInfo? GetElementAtPoint(int screenX, int screenY)
    {
        try
        {
            var pt = new System.Drawing.Point(screenX, screenY);
            var element = _automation.FromPoint(pt);
            if (element == null) return null;

            string name, aid, ctStr;
            try { name = element.Name ?? ""; } catch { name = ""; }
            try { aid = element.AutomationId ?? ""; } catch { aid = ""; }
            try { ctStr = element.ControlType.ToString(); } catch { ctStr = "?"; }

            string? value = null;
            try
            {
                var vp = element.Patterns.Value;
                if (vp.IsSupported)
                    value = vp.Pattern.Value.Value;
            }
            catch { }

            // Gather supported patterns
            var patterns = new List<string>();
            try { if (element.Patterns.Invoke.IsSupported) patterns.Add("Invoke"); } catch { }
            try { if (element.Patterns.Value.IsSupported) patterns.Add("Value"); } catch { }
            try { if (element.Patterns.Toggle.IsSupported) patterns.Add("Toggle"); } catch { }
            try { if (element.Patterns.SelectionItem.IsSupported) patterns.Add("SelectionItem"); } catch { }
            try { if (element.Patterns.ExpandCollapse.IsSupported) patterns.Add("ExpandCollapse"); } catch { }
            try { if (element.Patterns.Scroll.IsSupported) patterns.Add("Scroll"); } catch { }
            try { if (element.Patterns.Text.IsSupported) patterns.Add("Text"); } catch { }
            try { if (element.Patterns.RangeValue.IsSupported) patterns.Add("RangeValue"); } catch { }

            System.Drawing.Rectangle rect;
            try { rect = element.BoundingRectangle; } catch { rect = System.Drawing.Rectangle.Empty; }

            return new ElementAtPointInfo
            {
                Name = name,
                AutomationId = aid,
                ControlType = ctStr,
                Value = value,
                Patterns = patterns,
                BoundsX = rect.X, BoundsY = rect.Y,
                BoundsW = rect.Width, BoundsH = rect.Height,
            };
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get UI element at a screen point, but within a specific window's UIA tree.
    /// Used when the default FromPoint returns an overlay element.
    /// Walks the UIA tree of the given window to find the deepest element containing the point.
    /// </summary>
    public ElementAtPointInfo? GetElementAtPointInWindow(int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return null;

            var pt = new System.Drawing.Point(screenX, screenY);
            var found = FindDeepestAtPoint(root, pt);
            if (found == null) return null;

            return ExtractElementInfo(found);
        }
        catch { return null; }
    }

    /// <summary>
    /// Recursively find the deepest UIA element containing the given screen point.
    /// </summary>
    private FlaUI.Core.AutomationElements.AutomationElement? FindDeepestAtPoint(
        FlaUI.Core.AutomationElements.AutomationElement parent,
        System.Drawing.Point pt, int depth = 0)
    {
        if (depth > 8) return parent; // safety limit

        try
        {
            var children = parent.FindAllChildren();
            if (children == null || children.Length == 0) return parent;

            foreach (var child in children)
            {
                try
                {
                    var rect = child.BoundingRectangle;
                    if (rect.Contains(pt))
                    {
                        // Go deeper
                        return FindDeepestAtPoint(child, pt, depth + 1);
                    }
                }
                catch { /* skip inaccessible children */ }
            }
        }
        catch { }

        return parent;
    }

    /// <summary>
    /// Extract info from a UIA element into ElementAtPointInfo.
    /// </summary>
    private ElementAtPointInfo ExtractElementInfo(FlaUI.Core.AutomationElements.AutomationElement element)
    {
        string name, aid, ctStr;
        try { name = element.Name ?? ""; } catch { name = ""; }
        try { aid = element.AutomationId ?? ""; } catch { aid = ""; }
        try { ctStr = element.ControlType.ToString(); } catch { ctStr = "?"; }

        string? value = null;
        try
        {
            var vp = element.Patterns.Value;
            if (vp.IsSupported)
                value = vp.Pattern.Value.Value;
        }
        catch { }

        var patterns = new List<string>();
        try { if (element.Patterns.Invoke.IsSupported) patterns.Add("Invoke"); } catch { }
        try { if (element.Patterns.Value.IsSupported) patterns.Add("Value"); } catch { }
        try { if (element.Patterns.Toggle.IsSupported) patterns.Add("Toggle"); } catch { }
        try { if (element.Patterns.SelectionItem.IsSupported) patterns.Add("SelectionItem"); } catch { }
        try { if (element.Patterns.ExpandCollapse.IsSupported) patterns.Add("ExpandCollapse"); } catch { }
        try { if (element.Patterns.Scroll.IsSupported) patterns.Add("Scroll"); } catch { }
        try { if (element.Patterns.Text.IsSupported) patterns.Add("Text"); } catch { }
        try { if (element.Patterns.RangeValue.IsSupported) patterns.Add("RangeValue"); } catch { }

        System.Drawing.Rectangle rect;
        try { rect = element.BoundingRectangle; } catch { rect = System.Drawing.Rectangle.Empty; }

        return new ElementAtPointInfo
        {
            Name = name, AutomationId = aid, ControlType = ctStr,
            Value = value, Patterns = patterns,
            BoundsX = rect.X, BoundsY = rect.Y,
            BoundsW = rect.Width, BoundsH = rect.Height,
        };
    }

    /// <summary>
    /// Get combined hierarchy path for an element at a point.
    /// Format: "[ClassPath/.../ClassName] 앱이름/부모이름/.../현재요소이름"
    /// Win32 class path in brackets, UIA name path after it.
    /// </summary>
    public string? GetHierarchyPath(int screenX, int screenY, IntPtr hWnd, bool useWindowTree = false)
    {
        try
        {
            AutomationElement? element;
            if (useWindowTree)
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) return null;
                element = FindDeepestAtPoint(root, new System.Drawing.Point(screenX, screenY));
            }
            else
            {
                element = _automation.FromPoint(new System.Drawing.Point(screenX, screenY));
            }
            if (element == null) return null;

            // Build UIA name chain (bottom-up)
            var names = new List<string>();
            var current = element;
            int depth = 0;

            while (current != null && depth < 8)
            {
                try
                {
                    string ct;
                    try { ct = current.ControlType.ToString(); } catch { ct = "?"; }

                    // Stop at Desktop
                    if (ct == "?" && depth > 0) break;
                    if (ct == "Pane")
                    {
                        string pName = "";
                        try { pName = current.Name ?? ""; } catch { }
                        if (pName.Contains("Desktop") || pName.Contains("\ub370\uc2a4\ud06c\ud1b1")) break;
                    }

                    // Get display label: prefer Name, fallback AutomationId
                    string label = "";
                    try
                    {
                        var name = current.Name;
                        if (!string.IsNullOrEmpty(name))
                            label = name.Length > 20 ? name[..20] + ".." : name;
                    }
                    catch { }

                    if (string.IsNullOrEmpty(label))
                    {
                        try
                        {
                            var aid = current.AutomationId;
                            if (!string.IsNullOrEmpty(aid))
                                label = aid;
                        }
                        catch { }
                    }

                    // Use ControlType as last resort
                    if (string.IsNullOrEmpty(label))
                        label = ct;

                    names.Add(label);

                    var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
                    current = walker.GetParent(current);
                    depth++;
                }
                catch { break; }
            }

            if (names.Count == 0) return null;

            names.Reverse();  // top-down now

            // Trim generic root entries (Pane, Window with no name at start)
            while (names.Count > 1 && (names[0] == "Pane" || names[0] == "Window"))
                names.RemoveAt(0);

            // Build: [Win32ClassPath] UIA이름/경로/현재
            var sb = new System.Text.StringBuilder();

            // Win32 class path: drill down from hWnd through child windows at point
            var classPath = WKAppBot.Win32.Window.WindowFinder.GetWindowHierarchyPathAtPoint(hWnd, screenX, screenY);
            if (!string.IsNullOrEmpty(classPath))
            {
                sb.Append('[');
                sb.Append(classPath);
                sb.Append("] ");
            }

            // UIA name path
            sb.Append(string.Join("/", names));

            return sb.ToString();
        }
        catch { return null; }
    }

    /// <summary>
    /// Dump the UI tree of a window for inspection.
    /// </summary>
    public string DumpTree(IntPtr hWnd, int maxDepth = 5)
    {
        var root = _automation.FromHandle(hWnd);
        var sb = new System.Text.StringBuilder();
        DumpElement(root, sb, 0, maxDepth);
        return sb.ToString();
    }

    private void DumpElement(AutomationElement element, System.Text.StringBuilder sb, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var indent = new string(' ', depth * 2);

        // Some UWP elements don't support all properties — access safely
        string name, aid, ctStr, rectStr;
        try { name = element.Name ?? "(null)"; } catch { name = "(err)"; }
        try { aid = element.AutomationId ?? ""; } catch { aid = ""; }
        try { ctStr = element.ControlType.ToString(); } catch { ctStr = "?"; }
        try
        {
            var rect = element.BoundingRectangle;
            rectStr = $"({rect.X},{rect.Y} {rect.Width}x{rect.Height})";
        }
        catch { rectStr = "(?)"; }

        sb.AppendLine($"{indent}[{ctStr}] \"{name}\" aid=\"{aid}\" {rectStr}");

        try
        {
            var children = element.FindAllChildren();
            foreach (var child in children)
            {
                DumpElement(child, sb, depth + 1, maxDepth);
            }
        }
        catch { /* some elements throw on FindAllChildren */ }
    }

    public void Dispose()
    {
        _automation.Dispose();
    }
}

/// <summary>
/// Info about a UI element found at a screen point.
/// </summary>
public sealed class ElementAtPointInfo
{
    public string Name { get; init; } = "";
    public string AutomationId { get; init; } = "";
    public string ControlType { get; init; } = "";
    public string? Value { get; init; }
    public List<string> Patterns { get; init; } = new();
    public int BoundsX { get; init; }
    public int BoundsY { get; init; }
    public int BoundsW { get; init; }
    public int BoundsH { get; init; }

    /// <summary>
    /// One-line summary for console display.
    /// </summary>
    public string ToSummary()
    {
        var parts = new List<string>();
        parts.Add($"[{ControlType}]");
        if (!string.IsNullOrEmpty(Name)) parts.Add($"\"{Name}\"");
        if (!string.IsNullOrEmpty(AutomationId)) parts.Add($"aid=\"{AutomationId}\"");
        if (!string.IsNullOrEmpty(Value)) parts.Add($"val=\"{Truncate(Value, 30)}\"");
        if (Patterns.Count > 0) parts.Add($"({string.Join(",", Patterns)})");
        parts.Add($"[{BoundsX},{BoundsY} {BoundsW}x{BoundsH}]");
        return string.Join(" ", parts);
    }

    private static string Truncate(string s, int max) =>
        s.Length <= max ? s : s[..max] + "...";
}

/// <summary>
/// Unified pattern matcher for UI element name/id matching.
///
/// Syntax:
///   "exact text"       — literal exact match (default, backward-compatible)
///   "*partial*"        — wildcard: * = 0+ chars, ? = 1 char (glob-style)
///   "regex:pattern"    — regular expression (case-insensitive)
///
/// Examples:
///   "num5Button"       → exact match for automation id
///   "*Button*"         → any name containing "Button"
///   "결과*"            → names starting with "결과"
///   "regex:btn_\\d+"   → regex matching btn_ followed by digits
///   "?u?"              → 3-char names with 'u' in middle
/// </summary>
public sealed class PatternMatcher
{
    private readonly Regex? _regex;
    private readonly string? _literal;

    private PatternMatcher(Regex regex) { _regex = regex; }
    private PatternMatcher(string literal) { _literal = literal; }

    /// <summary>
    /// Create a matcher from a pattern string.
    /// Auto-detects pattern type: regex: prefix, wildcard (*/?), or literal.
    /// </summary>
    public static PatternMatcher Create(string pattern)
    {
        // regex: prefix → explicit regex
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
        {
            var regexStr = pattern[6..];
            return new PatternMatcher(new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        // Contains wildcard chars → convert glob to regex
        if (pattern.Contains('*') || pattern.Contains('?'))
        {
            var regexStr = "^" + Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";
            return new PatternMatcher(new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        // Plain literal → exact match
        return new PatternMatcher(pattern);
    }

    /// <summary>
    /// Does the given pattern string use pattern syntax (wildcard/regex)?
    /// If false, it's a plain literal and can use fast UIA PropertyCondition.
    /// </summary>
    public static bool IsPattern(string text) =>
        text.StartsWith("regex:", StringComparison.OrdinalIgnoreCase) ||
        text.Contains('*') || text.Contains('?');

    /// <summary>
    /// Test if a value matches this pattern.
    /// </summary>
    public bool IsMatch(string value)
    {
        if (_regex != null)
            return _regex.IsMatch(value);
        return string.Equals(_literal, value, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() =>
        _regex != null ? $"pattern({_regex})" : $"literal({_literal})";
}
