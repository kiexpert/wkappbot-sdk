using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Accessibility;

/// <summary>
/// UI Automation element locator using FlaUI.
/// Primary locator in the Chain of Responsibility (before Vision and Coordinate).
/// </summary>
public sealed class UiaLocator : IDisposable
{
    private readonly UIA3Automation _automation;

    /// <summary>Expose automation instance for GrapHelper scope resolution.</summary>
    public UIA3Automation Automation => _automation;

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

        // Fallback: LegacyIAccessible DoDefaultAction — needed for Electron Hyperlinks
        // where Patterns.Invoke.IsSupported throws COM exception.
        try
        {
            var legacy = element.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                {
                    legacy.Pattern.DoDefaultAction();
                    return true;
                }
            }
        }
        catch { /* LegacyIA also not available */ }
        return false;
    }

    // ── Toggle Pattern (Checkbox/ToggleButton — focusless!) ──────

    /// <summary>
    /// Toggle a checkbox or toggle button. Returns true if toggled.
    /// Focusless — no mouse or focus needed!
    /// </summary>
    public static bool TryToggle(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Toggle;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Toggle();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Set a toggle to a specific state (on/off).
    /// Toggles until desired state is reached (max 3 attempts).
    /// Returns true if final state matches desired state.
    /// </summary>
    public static bool TrySetToggle(AutomationElement element, bool desiredOn)
    {
        try
        {
            var pattern = element.Patterns.Toggle;
            if (!pattern.IsSupported) return false;

            for (int i = 0; i < 3; i++)
            {
                var state = pattern.Pattern.ToggleState.Value;
                bool isOn = state == FlaUI.Core.Definitions.ToggleState.On;
                if (isOn == desiredOn) return true;
                pattern.Pattern.Toggle();
                Thread.Sleep(50); // brief settle
            }
            // Final check
            var finalState = pattern.Pattern.ToggleState.Value;
            return (finalState == FlaUI.Core.Definitions.ToggleState.On) == desiredOn;
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Get toggle state: On, Off, or Indeterminate. Null if not supported.
    /// </summary>
    public static FlaUI.Core.Definitions.ToggleState? GetToggleState(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Toggle;
            if (pattern.IsSupported)
                return pattern.Pattern.ToggleState.Value;
        }
        catch { }
        return null;
    }

    // ── ExpandCollapse Pattern (ComboBox/TreeNode — focusless!) ──

    /// <summary>
    /// Expand a tree node, combo box, or group. Focusless!
    /// </summary>
    public static bool TryExpand(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.ExpandCollapse;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Expand();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Collapse a tree node, combo box, or group. Focusless!
    /// </summary>
    public static bool TryCollapse(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.ExpandCollapse;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Collapse();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Get expand/collapse state. Null if not supported.
    /// </summary>
    public static FlaUI.Core.Definitions.ExpandCollapseState? GetExpandCollapseState(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.ExpandCollapse;
            if (pattern.IsSupported)
                return pattern.Pattern.ExpandCollapseState.Value;
        }
        catch { }
        return null;
    }

    // ── SelectionItem Pattern (ListItem/TabItem/ComboItem — focusless!) ──

    /// <summary>
    /// Select an item in a list, tab, or combo box. Focusless!
    /// </summary>
    public static bool TrySelect(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.SelectionItem;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Select();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Check if an item is currently selected.
    /// </summary>
    public static bool? IsSelected(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.SelectionItem;
            if (pattern.IsSupported)
                return pattern.Pattern.IsSelected.Value;
        }
        catch { }
        return null;
    }

    // ── Scroll Pattern (ScrollableContainer — focusless!) ──

    /// <summary>
    /// Scroll a container in a direction. Focusless!
    /// Direction: horizontal (true) or vertical (false).
    /// Amount: SmallIncrement, LargeIncrement, SmallDecrement, LargeDecrement.
    /// </summary>
    public static bool TryScroll(AutomationElement element,
        FlaUI.Core.Definitions.ScrollAmount horizontalAmount,
        FlaUI.Core.Definitions.ScrollAmount verticalAmount)
    {
        try
        {
            var pattern = element.Patterns.Scroll;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Scroll(horizontalAmount, verticalAmount);
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Set scroll position to a percentage (0-100). Focusless!
    /// </summary>
    public static bool TrySetScrollPercent(AutomationElement element,
        double horizontalPercent, double verticalPercent)
    {
        try
        {
            var pattern = element.Patterns.Scroll;
            if (pattern.IsSupported)
            {
                pattern.Pattern.SetScrollPercent(horizontalPercent, verticalPercent);
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Get scroll info (horizontal/vertical percentages, scrollability).
    /// Returns null if not supported.
    /// </summary>
    public static ScrollInfo? GetScrollInfo(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Scroll;
            if (pattern.IsSupported)
            {
                return new ScrollInfo
                {
                    HorizontalPercent = pattern.Pattern.HorizontalScrollPercent.Value,
                    VerticalPercent = pattern.Pattern.VerticalScrollPercent.Value,
                    HorizontallyScrollable = pattern.Pattern.HorizontallyScrollable.Value,
                    VerticallyScrollable = pattern.Pattern.VerticallyScrollable.Value,
                    HorizontalViewSize = pattern.Pattern.HorizontalViewSize.Value,
                    VerticalViewSize = pattern.Pattern.VerticalViewSize.Value,
                };
            }
        }
        catch { }
        return null;
    }

    // ── ScrollItem Pattern (scroll element into view — focusless!) ──

    /// <summary>
    /// Scroll a TreeItem/ListItem into the visible area. Focusless!
    /// Works with virtualized containers (TreeView, ListView).
    /// </summary>
    public static bool TryScrollIntoView(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.ScrollItem;
            if (pattern.IsSupported)
            {
                pattern.Pattern.ScrollIntoView();
                return true;
            }
        }
        catch { }
        return false;
    }

    // ── Window Pattern (Window close/minimize/maximize — focusless!) ──

    /// <summary>
    /// Close a window via UIA Window pattern. Focusless!
    /// </summary>
    public static bool TryWindowClose(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Window;
            if (pattern.IsSupported)
            {
                pattern.Pattern.Close();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Set window visual state (Normal, Minimized, Maximized). Focusless!
    /// </summary>
    public static bool TrySetWindowState(AutomationElement element,
        FlaUI.Core.Definitions.WindowVisualState state)
    {
        try
        {
            var pattern = element.Patterns.Window;
            if (pattern.IsSupported)
            {
                pattern.Pattern.SetWindowVisualState(state);
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Get window interaction state (Ready, NotResponding, etc.).
    /// </summary>
    public static FlaUI.Core.Definitions.WindowInteractionState? GetWindowInteractionState(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.Window;
            if (pattern.IsSupported)
                return pattern.Pattern.WindowInteractionState.Value;
        }
        catch { }
        return null;
    }

    // ── RangeValue Pattern (Slider/ProgressBar — focusless!) ──

    /// <summary>
    /// Set a range value (slider, progress bar). Focusless!
    /// </summary>
    public static bool TrySetRangeValue(AutomationElement element, double value)
    {
        try
        {
            var pattern = element.Patterns.RangeValue;
            if (pattern.IsSupported)
            {
                var min = pattern.Pattern.Minimum.Value;
                var max = pattern.Pattern.Maximum.Value;
                if (value < min || value > max) return false;
                pattern.Pattern.SetValue(value);
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Get range value info (value, min, max, step). Null if not supported.
    /// </summary>
    public static RangeValueInfo? GetRangeValueInfo(AutomationElement element)
    {
        try
        {
            var pattern = element.Patterns.RangeValue;
            if (pattern.IsSupported)
            {
                return new RangeValueInfo
                {
                    Value = pattern.Pattern.Value.Value,
                    Minimum = pattern.Pattern.Minimum.Value,
                    Maximum = pattern.Pattern.Maximum.Value,
                    SmallChange = pattern.Pattern.SmallChange.Value,
                    LargeChange = pattern.Pattern.LargeChange.Value,
                    IsReadOnly = pattern.Pattern.IsReadOnly.Value,
                };
            }
        }
        catch { }
        return null;
    }

    // ── Pattern Enumeration (for diagnostics) ──────────────────

    /// <summary>
    /// Get all supported pattern names for an element.
    /// Also includes Window pattern which was missing from the original 8.
    /// </summary>
    public static List<string> GetSupportedPatterns(AutomationElement element)
    {
        var patterns = new List<string>();
        try { if (element.Patterns.Invoke.IsSupported) patterns.Add("Invoke"); } catch { }
        try { if (element.Patterns.Value.IsSupported) patterns.Add("Value"); } catch { }
        try { if (element.Patterns.Toggle.IsSupported) patterns.Add("Toggle"); } catch { }
        try { if (element.Patterns.SelectionItem.IsSupported) patterns.Add("SelectionItem"); } catch { }
        try { if (element.Patterns.ExpandCollapse.IsSupported) patterns.Add("ExpandCollapse"); } catch { }
        try { if (element.Patterns.Scroll.IsSupported) patterns.Add("Scroll"); } catch { }
        try { if (element.Patterns.Text.IsSupported) patterns.Add("Text"); } catch { }
        try { if (element.Patterns.RangeValue.IsSupported) patterns.Add("RangeValue"); } catch { }
        try { if (element.Patterns.Window.IsSupported) patterns.Add("Window"); } catch { }
        try { if (element.Patterns.Selection.IsSupported) patterns.Add("Selection"); } catch { }
        try { if (element.Patterns.Grid.IsSupported) patterns.Add("Grid"); } catch { }
        try { if (element.Patterns.Table.IsSupported) patterns.Add("Table"); } catch { }
        try { if (element.Patterns.Transform.IsSupported) patterns.Add("Transform"); } catch { }
        try { if (element.Patterns.ScrollItem.IsSupported) patterns.Add("ScrollItem"); } catch { }
        try { if (element.Patterns.ItemContainer.IsSupported) patterns.Add("ItemContainer"); } catch { }
        try { if (element.Patterns.VirtualizedItem.IsSupported) patterns.Add("VirtualizedItem"); } catch { }
        try { if (element.Patterns.LegacyIAccessible.IsSupported) patterns.Add("LegacyIA"); } catch { }
        return patterns;
    }

    /// <summary>
    /// Get the text value of an element (Name or Value pattern).
    /// For web elements: if Name and Value are empty, collects text from child Text elements.
    /// This handles Chrome's StatusBar/Group containers where text lives in child nodes.
    /// </summary>
    public static string? GetText(AutomationElement element)
    {
        // Try Value pattern first
        try
        {
            var vp = element.Patterns.Value;
            if (vp.IsSupported)
            {
                var val = vp.Pattern.Value.Value;
                if (!string.IsNullOrEmpty(val))
                    return val;
            }
        }
        catch { }

        // Try Name
        var name = element.Name;

        // For web containers (StatusBar, Group, etc.), the actual text content
        // lives in child Text nodes, while Name may be just an aria-label.
        // Collect child text and prefer it if different from the element Name.
        try
        {
            var children = element.FindAllChildren();
            if (children.Length > 0)
            {
                var texts = new List<string>();
                foreach (var child in children)
                {
                    try
                    {
                        var childName = child.Name;
                        if (!string.IsNullOrEmpty(childName))
                            texts.Add(childName);
                    }
                    catch { }
                }
                if (texts.Count > 0)
                {
                    var childText = string.Join(" ", texts);
                    // Prefer child text if it differs from element Name
                    // (Name is likely an aria-label, child text is actual content)
                    if (childText != name)
                        return childText;
                }
            }
        }
        catch { }

        return name; // may be null/empty
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
        if (depth > 15) return parent; // safety limit (Chrome/Electron web content can be 12+ deep)

        try
        {
            var children = parent.FindAllChildren();
            if (children == null || children.Length == 0) return parent;

            // Explore ALL matching children (not just first) — critical for Electron/Chrome
            // where multiple Pane siblings share the same BoundingRectangle.
            // Pick the result with the smallest area (most specific element).
            FlaUI.Core.AutomationElements.AutomationElement? bestResult = null;
            double bestArea = double.MaxValue;
            int bestDepth = depth;

            foreach (var child in children)
            {
                try
                {
                    var rect = child.BoundingRectangle;
                    if (!rect.Contains(pt)) continue;

                    var candidate = FindDeepestAtPoint(child, pt, depth + 1);
                    if (candidate == null) continue;

                    // Score: prefer smallest area (most specific), then deepest
                    var cRect = candidate.BoundingRectangle;
                    double cArea = (double)cRect.Width * cRect.Height;
                    if (bestResult == null || cArea < bestArea)
                    {
                        bestResult = candidate;
                        bestArea = cArea;
                    }
                }
                catch { /* skip inaccessible children */ }
            }

            if (bestResult != null) return bestResult;
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
    /// Focusless click at a screen point within a window's UIA tree.
    /// Finds the deepest element → tries Invoke/Toggle/Select/ExpandCollapse patterns.
    /// Returns (success, elementInfo) — no foreground or physical click needed!
    /// </summary>
    public (bool ok, string detail) TryFocuslessClickAtPoint(int screenX, int screenY, IntPtr hWnd)
    {
        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return (false, "UIA root not found");

            var pt = new System.Drawing.Point(screenX, screenY);
            var found = FindDeepestAtPoint(root, pt);
            if (found == null) return (false, "no element at point");

            string name = "", aid = "", ct = "";
            try { name = found.Name ?? ""; } catch { }
            try { aid = found.AutomationId ?? ""; } catch { }
            try { ct = found.ControlType.ToString(); } catch { ct = "?"; }

            string desc = $"[{ct}]";
            if (!string.IsNullOrEmpty(name)) desc += $" \"{(name.Length > 30 ? name[..30] : name)}\"";
            if (!string.IsNullOrEmpty(aid)) desc += $" aid={aid}";

            // Try focusless patterns in priority order
            if (TryInvoke(found))
                return (true, $"{desc} (Invoke, focusless)");
            if (TryToggle(found))
                return (true, $"{desc} (Toggle, focusless)");
            if (TrySelect(found))
                return (true, $"{desc} (Select, focusless)");

            // Try ExpandCollapse
            try
            {
                var ec = found.Patterns.ExpandCollapse;
                if (ec.IsSupported)
                {
                    var state = ec.Pattern.ExpandCollapseState.Value;
                    if (state == FlaUI.Core.Definitions.ExpandCollapseState.Collapsed)
                        ec.Pattern.Expand();
                    else
                        ec.Pattern.Collapse();
                    return (true, $"{desc} (ExpandCollapse, focusless)");
                }
            }
            catch { }

            var patterns = GetSupportedPatterns(found);
            string patStr = patterns.Count > 0 ? string.Join(",", patterns) : "none";
            return (false, $"{desc} patterns=[{patStr}] — no focusless action available");
        }
        catch (Exception ex)
        {
            return (false, $"UIA error: {ex.Message}");
        }
    }

    /// <summary>
    /// 미니마이즈 윈도우용: 자식 트리에서 Invoke/Toggle 가능 요소 수집.
    /// rcNormalPosition 기준 상대좌표로 거리 계산하여 가까운 순으로 정렬.
    /// BoundingRectangle이 비어있는 요소도 포함 (미니마이즈 상태에서 rect=0,0,0,0).
    /// 미니마이즈에서 BoundingRect이 없으면 트리 순서(DFS)로 수집 → 첫 번째 = 가장 위/왼쪽.
    /// </summary>
    public List<InvocableCandidate> FindInvocableDescendants(
        IntPtr hWnd, int targetX, int targetY, RECT normalRect)
    {
        var result = new List<InvocableCandidate>();
        try
        {
            var root = _automation.FromHandle(hWnd);
            if (root == null) return result;

            CollectInvocable(root, targetX, targetY, normalRect, result, 0);

            // 거리순 정렬 (BoundingRect이 있는 요소 우선, 없으면 트리 순서)
            result.Sort((a, b) =>
            {
                // BoundingRect 있는 쪽 우선
                if (a.HasBounds && !b.HasBounds) return -1;
                if (!a.HasBounds && b.HasBounds) return 1;
                // 둘 다 있으면 거리순
                if (a.HasBounds && b.HasBounds) return a.Distance.CompareTo(b.Distance);
                // 둘 다 없으면 트리 순서 유지
                return a.TreeOrder.CompareTo(b.TreeOrder);
            });
        }
        catch { }
        return result;
    }

    /// <summary>재귀적으로 Invoke/Toggle 가능 요소 수집.</summary>
    private void CollectInvocable(AutomationElement parent, int targetX, int targetY,
        RECT normalRect, List<InvocableCandidate> result, int depth)
    {
        if (depth > 8) return; // 미니마이즈 탐색은 깊이 제한 (MFC는 보통 3-4단)

        try
        {
            var children = parent.FindAllChildren();
            if (children == null) return;

            foreach (var child in children)
            {
                try
                {
                    // Invoke 또는 Toggle 지원?
                    bool hasInvoke = false, hasToggle = false;
                    try { hasInvoke = child.Patterns.Invoke.IsSupported; } catch { }
                    try { hasToggle = child.Patterns.Toggle.IsSupported; } catch { }

                    if (hasInvoke || hasToggle)
                    {
                        string name = "", aid = "", ct = "";
                        try { name = child.Name ?? ""; } catch { }
                        try { aid = child.AutomationId ?? ""; } catch { }
                        try { ct = child.ControlType.ToString(); } catch { ct = "?"; }

                        // BoundingRectangle 거리 계산 (미니마이즈면 0,0,0,0 → HasBounds=false)
                        var rect = System.Drawing.Rectangle.Empty;
                        bool hasBounds = false;
                        double dist = double.MaxValue;
                        try
                        {
                            rect = child.BoundingRectangle;
                            hasBounds = !rect.IsEmpty && rect.Width > 0 && rect.Height > 0;
                            if (hasBounds)
                            {
                                // 요소 중심까지 거리
                                double cx = rect.Left + rect.Width / 2.0;
                                double cy = rect.Top + rect.Height / 2.0;
                                dist = Math.Sqrt((cx - targetX) * (cx - targetX) + (cy - targetY) * (cy - targetY));
                            }
                        }
                        catch { }

                        result.Add(new InvocableCandidate(
                            Element: child,
                            ControlType: ct,
                            Name: name,
                            AutomationId: aid,
                            HasBounds: hasBounds,
                            Distance: dist,
                            TreeOrder: result.Count
                        ));
                    }

                    // 재귀: 자식의 자식도 탐색
                    CollectInvocable(child, targetX, targetY, normalRect, result, depth + 1);
                }
                catch { }
            }
        }
        catch { }
    }

    /// <summary>요소에 Invoke 실행. Toggle은 Invoke 없을 때 폴백.</summary>
    public bool TryInvokeElement(AutomationElement element)
    {
        try { element.Patterns.Invoke.Pattern.Invoke(); return true; } catch { }
        try { element.Patterns.Toggle.Pattern.Toggle(); return true; } catch { }
        return false;
    }

    /// <summary>미니마이즈 UIA 탐색 후보.</summary>
    public record InvocableCandidate(
        AutomationElement Element,
        string ControlType,
        string Name,
        string AutomationId,
        bool HasBounds,
        double Distance,
        int TreeOrder
    );

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

    /// <summary>DumpTree from a pre-resolved UIA element (for # scope narrowing).</summary>
    public string DumpTree(AutomationElement root, int maxDepth = 5)
    {
        var sb = new System.Text.StringBuilder();
        DumpElement(root, sb, 0, maxDepth);
        return sb.ToString();
    }

    /// <summary>
    /// Dump the UI tree filtered by a pattern.
    /// Searches the ENTIRE tree (ignoring depth limit for the search phase).
    /// When a matching element is found, dumps its ancestry path + subtree up to subtreeDepth.
    /// Pattern matches against Name, AutomationId, or ControlType (case-insensitive).
    /// Supports wildcards (*/?), regex: prefix, and plain substring matching.
    /// </summary>
    public string DumpTreeFiltered(IntPtr hWnd, string filterPattern, int subtreeDepth = 5)
        => DumpTreeFiltered(_automation.FromHandle(hWnd), filterPattern, subtreeDepth);

    /// <summary>DumpTreeFiltered from a pre-resolved UIA element (for # scope narrowing).</summary>
    public string DumpTreeFiltered(AutomationElement root, string filterPattern, int subtreeDepth = 5)
    {
        var sb = new System.Text.StringBuilder();
        var matches = new List<(AutomationElement element, List<string> ancestorLines)>();

        // Phase 1: Search entire tree for matching elements (max 20 depth, 15s timeout)
        var matcher = PatternMatcher.IsPattern(filterPattern)
            ? PatternMatcher.Create(filterPattern)
            : null; // plain substring

        var searchTimeout = DateTime.UtcNow.AddSeconds(15);
        SearchForMatches(root, filterPattern, matcher, new List<string>(), matches, 0, 20, searchTimeout);

        if (matches.Count == 0)
        {
            sb.AppendLine($"(no elements matching \"{filterPattern}\")");
            return sb.ToString();
        }

        sb.AppendLine($"── {matches.Count} match(es) for \"{filterPattern}\" ──");
        sb.AppendLine();

        // Phase 2: For each match, show path one-liner + subtree dump
        for (int i = 0; i < matches.Count; i++)
        {
            var (elem, ancestors) = matches[i];

            // Build path one-liner: /Window:Claude/Pane/Document/Group/Button:Menu
            string elemName, elemAid, elemCt;
            try { elemName = elem.Name ?? ""; } catch { elemName = ""; }
            try { elemAid = elem.AutomationId ?? ""; } catch { elemAid = ""; }
            try { elemCt = elem.ControlType.ToString(); } catch { elemCt = "?"; }
            var pathParts = ancestors.Select(a =>
            {
                var typeStart = a.IndexOf('[');
                var typeEnd = a.IndexOf(']', typeStart + 1);
                var nameStart = a.IndexOf('"', typeEnd) + 1;
                var nameEnd = a.IndexOf('"', nameStart);
                var type = typeStart >= 0 && typeEnd > typeStart ? a[(typeStart + 1)..typeEnd] : "?";
                var name = nameStart > 0 && nameEnd > nameStart ? a[nameStart..nameEnd] : "";
                return string.IsNullOrEmpty(name) || name == "(null)" ? type : $"{type}:{name}";
            }).ToList();
            // Element itself: include aid if available
            string elemPart = string.IsNullOrEmpty(elemName) ? elemCt : $"{elemCt}:{elemName}";
            if (!string.IsNullOrEmpty(elemAid)) elemPart += $"(aid={elemAid})";
            pathParts.Add(elemPart);

            // Path line with index — A11Y: prefix to distinguish from Win32 tree paths
            sb.AppendLine($"[{i + 1}] A11Y:/{string.Join("/", pathParts)}");

            // Subtree dump starting at depth 0 (flat, easy to read)
            DumpElement(elem, sb, 1, 1 + subtreeDepth);
            sb.AppendLine();

            if (i >= 49) // safety limit
            {
                sb.AppendLine($"... +{matches.Count - 50} more matches (truncated)");
                break;
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Recursively search the UIA tree for elements matching the filter.
    /// Collects matching elements with their ancestry path for context.
    /// </summary>
    private void SearchForMatches(
        AutomationElement element, string filterText, PatternMatcher? matcher,
        List<string> ancestorLines, List<(AutomationElement, List<string>)> matches,
        int depth, int maxSearchDepth, DateTime timeout)
    {
        if (depth > maxSearchDepth || matches.Count >= 50) return;
        if (DateTime.UtcNow > timeout) return; // safety timeout

        // Extract properties safely
        string name, aid, ctStr, rectStr;
        try { name = element.Name ?? ""; } catch { name = ""; }
        try { aid = element.AutomationId ?? ""; } catch { aid = ""; }
        try { ctStr = element.ControlType.ToString(); } catch { ctStr = "?"; }
        try
        {
            var rect = element.BoundingRectangle;
            rectStr = $"({rect.X},{rect.Y} {rect.Width}x{rect.Height})";
        }
        catch { rectStr = "(?)"; }

        // Check if this element matches the filter
        bool isMatch;
        if (matcher != null)
        {
            isMatch = matcher.IsMatch(name) || matcher.IsMatch(aid) || matcher.IsMatch(ctStr);
        }
        else
        {
            // Plain substring search (case-insensitive)
            isMatch = name.Contains(filterText, StringComparison.OrdinalIgnoreCase)
                || aid.Contains(filterText, StringComparison.OrdinalIgnoreCase)
                || ctStr.Contains(filterText, StringComparison.OrdinalIgnoreCase);
        }

        string lineForAncestry = $"[{ctStr}] \"{(name.Length > 40 ? name[..40] + "..." : name)}\" aid=\"{aid}\" {rectStr}";

        if (isMatch)
        {
            matches.Add((element, new List<string>(ancestorLines)));
        }
        else
        {
            // Not a match — recurse into children
            try
            {
                var children = element.FindAllChildren();
                var newAncestors = new List<string>(ancestorLines) { lineForAncestry };
                foreach (var child in children)
                {
                    SearchForMatches(child, filterText, matcher, newAncestors, matches, depth + 1, maxSearchDepth, timeout);
                    if (matches.Count >= 50 || DateTime.UtcNow > timeout) break;
                }
            }
            catch { }
        }
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

        // Gather supported patterns for this element
        var patternStr = "";
        try
        {
            var patterns = GetSupportedPatterns(element);
            if (patterns.Count > 0)
                patternStr = $" ({string.Join(",", patterns)})";
        }
        catch { }

        sb.AppendLine($"{indent}[{ctStr}] \"{name}\" aid=\"{aid}\" {rectStr}{patternStr}");

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

    // ── Quick UIA search (static, self-contained) ──────────────────

    /// <summary>
    /// Lightweight BFS search of a window's UIA tree for keyword matches.
    /// Self-contained: creates and disposes its own UIA3Automation.
    /// Used by "windows --uia" for unified title+element search.
    /// </summary>
    /// <param name="hWnd">Target window handle</param>
    /// <param name="keyword">Search text (substring, case-insensitive)</param>
    /// <param name="maxDepth">BFS depth limit (default: 4, keeps it fast)</param>
    /// <param name="maxResults">Stop after N matches</param>
    /// <param name="maxVisited">Safety limit on total elements visited</param>
    /// <param name="timeoutMs">Abort if search takes longer than this</param>
    /// <returns>List of matching elements (empty on error/timeout)</returns>
    public static List<UiaQuickMatch> QuickSearch(
        IntPtr hWnd, string keyword,
        int maxDepth = 4, int maxResults = 5, int maxVisited = 300, int timeoutMs = 3000)
    {
        var results = new List<UiaQuickMatch>();
        try
        {
            // Auto-enable screen reader mode for Chromium/Electron apps
            // (their A11Y tree is empty unless a screen reader is announced)
            Native.ScreenReaderMode.EnableForWindow(hWnd);

            using var automation = new UIA3Automation();
            var root = automation.FromHandle(hWnd);
            if (root == null) return results;

            return QuickSearchCore(root, keyword, maxDepth, maxResults, maxVisited, timeoutMs);
        }
        catch { }
        return results;
    }

    /// <summary>
    /// QuickSearch from an existing AutomationElement root (for #scope narrowing).
    /// </summary>
    public static List<UiaQuickMatch> QuickSearch(
        AutomationElement root, string keyword,
        int maxDepth = 4, int maxResults = 5, int maxVisited = 300, int timeoutMs = 3000)
    {
        return QuickSearchCore(root, keyword, maxDepth, maxResults, maxVisited, timeoutMs);
    }

    private static List<UiaQuickMatch> QuickSearchCore(
        AutomationElement root, string keyword,
        int maxDepth, int maxResults, int maxVisited, int timeoutMs)
    {
        var results = new List<UiaQuickMatch>();
        try
        {

            // Pattern support: glob (*/?) and regex: prefix via PatternMatcher
            bool isPattern = PatternMatcher.IsPattern(keyword);
            PatternMatcher? matcher = isPattern ? PatternMatcher.Create(keyword) : null;

            var deadline = Environment.TickCount64 + timeoutMs;
            int visited = 0;
            var queue = new Queue<(AutomationElement el, int depth, string path)>();
            queue.Enqueue((root, 0, ""));

            while (queue.Count > 0 && results.Count < maxResults)
            {
                if (++visited > maxVisited || Environment.TickCount64 > deadline) break;

                var (el, depth, parentPath) = queue.Dequeue();

                string name, aid, ct;
                try { name = el.Properties.Name.ValueOrDefault ?? ""; } catch { continue; }
                try { aid = el.Properties.AutomationId.ValueOrDefault ?? ""; } catch { aid = ""; }
                try { ct = el.Properties.ControlType.ValueOrDefault.ToString(); }
                catch { ct = "?"; }

                // Build name path segment: [Type:aid]Name — type+id+name as one "folder"
                // [Button:radix-_r_42_]사이드바, [Pane]Claude, [Pane] (unnamed, no aid)
                string segName = !string.IsNullOrEmpty(aid) ? $"[{ct}:{aid}]{name}" : $"[{ct}]{name}";
                string currentPath = depth == 0 ? "" : string.IsNullOrEmpty(parentPath) ? segName : $"{parentPath}/{segName}";

                // Match on Name or AutomationId (skip root element)
                // Empty keyword → return all elements (for path search mode)
                // Supports: substring (default), glob (*/?) and regex: prefix
                if (depth > 0)
                {
                    if (string.IsNullOrEmpty(keyword))
                    {
                        // Path search mode: collect all elements with their paths
                        results.Add(new UiaQuickMatch(ct, name, aid, currentPath));
                    }
                    else
                    {
                        bool nameMatch, aidMatch;
                        if (matcher != null)
                        {
                            nameMatch = !string.IsNullOrEmpty(name) && matcher.IsMatch(name);
                            aidMatch = !string.IsNullOrEmpty(aid) && matcher.IsMatch(aid);
                        }
                        else
                        {
                            nameMatch = !string.IsNullOrEmpty(name) && name.Contains(keyword, StringComparison.OrdinalIgnoreCase);
                            aidMatch = !string.IsNullOrEmpty(aid) && aid.Contains(keyword, StringComparison.OrdinalIgnoreCase);
                        }
                        if (nameMatch || aidMatch)
                            results.Add(new UiaQuickMatch(ct, name, aid, currentPath));
                    }
                }

                // Enqueue children (BFS)
                if (depth < maxDepth)
                {
                    try
                    {
                        var children = el.FindAllChildren();
                        foreach (var child in children)
                            queue.Enqueue((child, depth + 1, currentPath));
                    }
                    catch { }
                }
            }
        }
        catch { }
        return results;
    }
}

/// <summary>
/// Lightweight search result from QuickSearch (no COM references, safe to hold).
/// </summary>
public record UiaQuickMatch(string ControlType, string Name, string AutomationId, string NamePath = "");

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
    /// All modes do SUBSTRING matching by default (no anchoring).
    /// Use <see cref="CreatePathGlob"/> for anchored path-segment matching.
    /// </summary>
    public static PatternMatcher Create(string pattern)
    {
        // regex: prefix → explicit regex (user controls anchoring)
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
        {
            var regexStr = pattern[6..];
            return new PatternMatcher(new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        // Contains wildcard chars → convert glob to regex (NO anchoring = substring)
        if (pattern.Contains('*') || pattern.Contains('?'))
        {
            var regexStr = Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".");
            return new PatternMatcher(new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        // Plain literal → substring match (Contains)
        return new PatternMatcher(pattern);
    }

    /// <summary>
    /// Create a path-aware glob matcher (GitHub-style).
    /// <c>*</c>  = matches anything except <c>/</c> (single path segment).
    /// <c>**</c> = matches anything including <c>/</c> (any depth / multiple segments).
    /// <c>?</c>  = matches single character except <c>/</c>.
    /// Also supports <c>regex:</c> prefix for explicit regex.
    /// </summary>
    /// <example>
    /// "*/#32770"          → any single parent + #32770
    /// "**/#32770"         → #32770 at any depth
    /// "E*Trade*/#32770"   → parent starts with E*Trade + #32770
    /// "*_59648/**/Button_*" → CID 59648 parent, Button at any depth below
    /// </example>
    public static PatternMatcher CreatePathGlob(string pattern)
    {
        // regex: prefix → pass through
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
        {
            var regexStr = pattern[6..];
            return new PatternMatcher(new Regex(regexStr, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        // Path-aware glob → regex conversion
        // Process order: **/ first (special: zero-or-more segments), then standalone **, then *, then ?
        var escaped = Regex.Escape(pattern);
        escaped = escaped
            .Replace(@"\*\*/", "(.*/)?")   // **/ → zero or more path segments (GitHub-style: matches empty too)
            .Replace(@"\*\*", ".*")        // **  → match anything including / (at end or standalone)
            .Replace(@"\*", "[^/]*")       // *   → match within single segment (no /)
            .Replace(@"\?", "[^/]");       // ?   → single char within segment (no /)

        var rx = "^" + escaped + "$";
        return new PatternMatcher(new Regex(rx, RegexOptions.IgnoreCase | RegexOptions.Compiled));
    }

    /// <summary>
    /// Does the given pattern string use pattern syntax (wildcard/regex)?
    /// If false, it's a plain literal and can use fast UIA PropertyCondition.
    /// </summary>
    public static bool IsPattern(string text) =>
        text.StartsWith("regex:", StringComparison.OrdinalIgnoreCase) ||
        text.Contains('*') || text.Contains('?');

    /// <summary>
    /// Ensure a pattern does substring matching by wrapping with <c>*...*</c>.
    /// Handles all cases gracefully:
    ///   "실현손익"  → "*실현손익*"  (plain literal)
    ///   "*실현손익" → "*실현손익*"  (missing trailing *)
    ///   "실현손익*" → "*실현손익*"  (missing leading *)
    ///   "*실현*"    → "*실현*"     (already wrapped)
    ///   "regex:..."  → "regex:..."   (explicit regex, unchanged)
    /// Use this for contexts where substring matching is the expected default
    /// (e.g., window finding, UIA scope narrowing, child window matching).
    /// </summary>
    public static string EnsureSubstring(string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return pattern;
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase)) return pattern;
        if (!pattern.StartsWith('*')) pattern = "*" + pattern;
        if (!pattern.EndsWith('*')) pattern += "*";
        return pattern;
    }

    /// <summary>
    /// Does the pattern use path-aware glob syntax?
    /// True if it contains <c>/</c> or <c>**</c> — should match against full classPath hierarchy.
    /// False means match against leaf className only (backward compatible).
    /// </summary>
    public static bool IsPathGlob(string pattern) =>
        pattern.Contains('/') || pattern.Contains("**");

    /// <summary>
    /// Test if a value matches this pattern.
    /// </summary>
    public bool IsMatch(string value)
    {
        if (_regex != null)
            return _regex.IsMatch(value);
        // Literal → substring match (Contains), not exact match
        return value.Contains(_literal!, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() =>
        _regex != null ? $"pattern({_regex})" : $"literal({_literal})";
}

/// <summary>
/// Scroll position and capability info for Scroll pattern.
/// </summary>
public sealed class ScrollInfo
{
    public double HorizontalPercent { get; init; }
    public double VerticalPercent { get; init; }
    public bool HorizontallyScrollable { get; init; }
    public bool VerticallyScrollable { get; init; }
    public double HorizontalViewSize { get; init; }
    public double VerticalViewSize { get; init; }

    public override string ToString() =>
        $"H={HorizontalPercent:F0}%({(HorizontallyScrollable ? "scrollable" : "fixed")}) " +
        $"V={VerticalPercent:F0}%({(VerticallyScrollable ? "scrollable" : "fixed")})";
}

/// <summary>
/// Range value info for Slider/ProgressBar.
/// </summary>
public sealed class RangeValueInfo
{
    public double Value { get; init; }
    public double Minimum { get; init; }
    public double Maximum { get; init; }
    public double SmallChange { get; init; }
    public double LargeChange { get; init; }
    public bool IsReadOnly { get; init; }

    public override string ToString() =>
        $"{Value} [{Minimum}~{Maximum}] step={SmallChange}/{LargeChange}{(IsReadOnly ? " RO" : "")}";
}
