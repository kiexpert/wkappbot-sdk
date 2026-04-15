using FlaUI.Core.AutomationElements;

namespace WKAppBot.Win32.Accessibility;

public sealed partial class UiaLocator
{
    private static List<System.Drawing.Rectangle> TryGetTextSelectionRects(AutomationElement element)
    {
        var result = new List<System.Drawing.Rectangle>();
        try
        {
            if (!element.Patterns.Text.IsSupported) return result;
            var ranges = element.Patterns.Text.Pattern.GetSelection();
            if (ranges == null) return result;
            foreach (var range in ranges)
            {
                try
                {
                    var rects = range.GetBoundingRectangles();
                    if (rects == null) continue;
                    foreach (var r in rects)
                    {
                        if (r.Width <= 0 || r.Height <= 0) continue;
                        result.Add(new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height));
                    }
                }
                catch { }
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Render descendants as visual markdown — text only (no node types).
    /// Groups elements by Y coordinate into rows; multi-column rows → MD table.
    /// </summary>
    public string RenderVisualMarkdown(IntPtr hWnd, int maxDepth = 10)
        => RenderVisualMarkdown(_automation.FromHandle(hWnd), maxDepth);

    public static string RenderVisualMarkdown(AutomationElement element, int maxDepth = 10)
    {
        // Collect all text-bearing leaf nodes with their screen coordinates
        var leaves = new List<(string text, int x, int y, int w, int h, int depth)>();
        CollectTextLeaves(element, leaves, 0, maxDepth);
        if (leaves.Count == 0) return "_(no text)_";

        // Shorten file paths: "D:\foo\bar\baz\file.txt" → "D:\...\file.txt"
        for (int i = 0; i < leaves.Count; i++)
        {
            var t = leaves[i].text;
            if (t.Length > 30 && (t.Contains(":\\") || t.Contains(":/")))
            {
                var sep = t.Contains(":\\") ? '\\' : '/';
                var lastSep = t.LastIndexOf(sep);
                if (lastSep > 3)
                {
                    var drive = t[..t.IndexOf(sep)];
                    var file = t[(lastSep + 1)..];
                    var l = leaves[i];
                    leaves[i] = ($"{drive}{sep}...{sep}{file}", l.x, l.y, l.w, l.h, l.depth);
                }
            }
        }

        // Group by Y coordinate (tolerance: elements within 8px are same row)
        leaves.Sort((a, b) => a.y != b.y ? a.y.CompareTo(b.y) : a.x.CompareTo(b.x));
        var rows = new List<List<(string text, int x, int w)>>();
        List<(string text, int x, int w)>? currentRow = null;
        int currentRowY = int.MinValue;

        foreach (var leaf in leaves)
        {
            if (currentRow == null || Math.Abs(leaf.y - currentRowY) > 8)
            {
                currentRow = new List<(string, int, int)>();
                rows.Add(currentRow);
                currentRowY = leaf.y;
            }
            currentRow.Add((leaf.text, leaf.x, leaf.w));
        }

        // Detect dominant column count for table rendering
        var sb = new System.Text.StringBuilder();
        // Find max columns in any row (for table detection)
        int maxCols = rows.Max(r => r.Count);

        // Calculate indent level from x coordinate (relative to leftmost element)
        int minX = leaves.Min(l => l.x);
        int indentUnit = 40; // ~40px per indent level

        if (maxCols >= 2 && rows.Count(r => r.Count >= 2) >= 2)
        {
            // Table mode: code block for multi-column rows only
            int tableCols = rows.Where(r => r.Count >= 2).Select(r => r.Count).GroupBy(c => c)
                .OrderByDescending(g => g.Count()).First().Key;

            // Calculate column widths
            var colWidths = new int[tableCols];
            foreach (var row in rows.Where(r => r.Count >= 2))
                for (int i = 0; i < Math.Min(row.Count, tableCols); i++)
                    colWidths[i] = Math.Max(colWidths[i], row[i].text.Length);
            for (int i = 0; i < tableCols; i++)
                colWidths[i] = Math.Min(colWidths[i], 30);

            bool inCodeBlock = false;
            foreach (var row in rows)
            {
                if (row.Count == 1)
                {
                    // Single col → close code block if open, emit as text with indent
                    if (inCodeBlock) { sb.AppendLine("```"); inCodeBlock = false; }
                    int indent = (row[0].x - minX) / indentUnit;
                    var prefix = indent > 0 ? new string(' ', indent) + "- " : "";
                    sb.AppendLine($"{prefix}{row[0].text}");
                }
                else
                {
                    // Multi col → open code block if needed
                    if (!inCodeBlock) { sb.AppendLine("```"); inCodeBlock = true; }
                    var parts = new List<string>();
                    for (int i = 0; i < tableCols; i++)
                    {
                        var cell = i < row.Count ? row[i].text : "";
                        if (cell.Length > colWidths[i]) cell = cell[..colWidths[i]];
                        parts.Add(cell.PadRight(colWidths[i]));
                    }
                    sb.AppendLine(string.Join("  ", parts));
                }
            }
            if (inCodeBlock) sb.AppendLine("```");
        }
        else
        {
            // Text mode: x-based indentation with Slack - list
            foreach (var row in rows)
            {
                var firstX = row[0].x;
                int indent = (firstX - minX) / indentUnit;
                var prefix = indent > 0 ? new string(' ', indent) + "- " : "";
                var text = string.Join("  ", row.Select(c => c.text));
                sb.AppendLine($"{prefix}{text}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>Collect all text-bearing leaf/named elements with their bounding rects.</summary>
    public static void CollectTextLeaves(AutomationElement element,
        List<(string text, int x, int y, int w, int h, int depth)> leaves, int depth, int maxDepth)
    {
        if (depth > maxDepth || leaves.Count > 300) return;
        string name;
        try { name = element.Name ?? ""; } catch { name = ""; }

        // Build tag prefix: <Type_aid> or <Type_name> for AI readability
        string tagPrefix = "";
        try
        {
            string ct = "?", aid = "", nm = "";
            try { ct = element.ControlType.ToString(); } catch { }
            try { aid = element.AutomationId ?? ""; } catch { }
            nm = name; // reuse already-read Name
            tagPrefix = $"<{GrapHelper.FormatNodeTag(ct, aid)}> ";
        }
        catch { }

        bool hasChildren = false;
        try
        {
            var children = element.FindAllChildren();
            if (children.Length > 0)
            {
                hasChildren = true;
                foreach (var child in children)
                    CollectTextLeaves(child, leaves, depth + 1, maxDepth);
            }
        }
        catch { }

        // Skip icon font glyphs: single char in PUA/CJK/symbol ranges (Codicon, Segoe MDL2, etc.)
        if (name.Length <= 2 && name.Length > 0)
        {
            var c = name[0];
            if (c >= 0xE000 && c <= 0xF8FF) name = "";     // Private Use Area (icon fonts)
            else if (c >= 0x2500 && c <= 0x27FF) name = ""; // Box drawing, misc symbols
            else if (c >= 0xF000 && c <= 0xFFFF) name = ""; // Supplementary PUA
            else if (name.Length == 1 && c >= 0x4E00 && c <= 0x9FFF) name = ""; // Single CJK char (likely icon)
        }

        // Leaf node with text, or named node with no text children
        if (!string.IsNullOrWhiteSpace(name) && (!hasChildren || !leaves.Any(l => l.depth > depth)))
        {
            try
            {
                var rect = element.BoundingRectangle;
                if (rect.Width <= 0 || rect.Height <= 0) { /* skip */ }
                else if (name.Contains('\n'))
                {
                    // Multi-line: try to split into per-line leaves using child rects
                    var lines = name.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    bool matched = false;
                    if (hasChildren && lines.Length >= 2)
                    {
                        try
                        {
                            var children = element.FindAllChildren();
                            if (children.Length > 0)
                            {
                                // Match up to min(lines, children) using child rects
                                int matchCount = Math.Min(lines.Length, children.Length);
                                for (int li = 0; li < matchCount; li++)
                                {
                                    var lineText = (li == 0 ? tagPrefix : "") + lines[li].Trim();
                                    try
                                    {
                                        var cr = children[li].BoundingRectangle;
                                        if (cr.Width > 0 && cr.Height > 0)
                                            leaves.Add((lineText, cr.X, cr.Y, cr.Width, cr.Height, depth));
                                    }
                                    catch { leaves.Add((lineText, rect.X, rect.Y + li * 16, rect.Width, 16, depth)); }
                                }
                                // Remaining lines (no child rect) → append with estimated Y
                                if (lines.Length != children.Length)
                                    Console.Error.WriteLine($"[VISUAL-MD] Line/child mismatch: {lines.Length} lines vs {children.Length} children — partial match {matchCount}");
                                for (int li = matchCount; li < lines.Length; li++)
                                    leaves.Add((lines[li].Trim(), rect.X, rect.Y + li * 16, rect.Width, 16, depth));
                                matched = true;
                            }
                        }
                        catch { }
                    }
                    if (!matched)
                    {
                        // No children at all: first line + ⋯
                        leaves.Add((tagPrefix + lines[0].TrimEnd() + "⋯", rect.X, rect.Y, rect.Width, rect.Height, depth));
                    }
                }
                else
                {
                    leaves.Add((tagPrefix + name, rect.X, rect.Y, rect.Width, rect.Height, depth));
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// Get the UIA focus chain: focused element → parent → ... → root.
    /// Returns lines like: "⌨ [Button] "OK" aid="1" (100,200 80x26)"
    /// Each line is indented by depth. First line = deepest focused element.
    /// Returns empty string if no focused element or focus is outside the given window.
    /// </summary>
    public string GetFocusedInspectText(int maxDepth = 12)
    {
        try
        {
            var focused = _automation.FocusedElement();
            if (focused == null) return "";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("── hot focus (UIA) ──");

            void AppendElementLine(AutomationElement el, string prefix)
            {
                string name = "", aid = "", ct = "?";
                System.Drawing.Rectangle? rect = null;
                try { name = el.Name ?? ""; } catch { }
                try { aid = el.AutomationId ?? ""; } catch { }
                try { ct = el.ControlType.ToString(); } catch { }
                try
                {
                    var r = el.BoundingRectangle;
                    rect = new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
                }
                catch { }

                var line = $"{prefix}{GrapHelper.FormatNodeLabel(ct, aid, name, rect: rect)}";
                sb.AppendLine(line);
            }

            AppendElementLine(focused, "⌨ ");

            var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
            var cur = focused;
            for (int level = 0; level < maxDepth; level++)
            {
                try { cur = walker.GetParent(cur); } catch { break; }
                if (cur == null) break;
                AppendElementLine(cur, $"{new string(' ', level * 2 + 2)}└ ");
                try
                {
                    if (cur.ControlType == FlaUI.Core.Definitions.ControlType.Window)
                        break;
                }
                catch { }
            }

            return sb.ToString().TrimEnd();
        }
        catch
        {
            return "";
        }
    }

    /// <summary>Get focused element info with parent chain for keyboard focus analysis.</summary>
    public FocusedElementInfo? GetFocusedElementInfo()
    {
        try
        {
            var focused = _automation.FocusedElement();
            if (focused == null) return null;

            string name, aid, ctStr, value = "";
            try { name = focused.Name ?? ""; } catch { name = ""; }
            try { aid = focused.AutomationId ?? ""; } catch { aid = ""; }
            try { ctStr = focused.ControlType.ToString(); } catch { ctStr = "?"; }
            try { if (focused.Patterns.Value.IsSupported) value = focused.Patterns.Value.Pattern.Value.Value ?? ""; } catch { }
            System.Drawing.Rectangle bounds = System.Drawing.Rectangle.Empty;
            try
            {
                var br = focused.BoundingRectangle;
                bounds = new System.Drawing.Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
            }
            catch { }
            var selectionRects = TryGetTextSelectionRects(focused);

            // Patterns
            var patterns = new List<string>();
            try { if (focused.Patterns.Invoke.IsSupported) patterns.Add("Invoke"); } catch { }
            try { if (focused.Patterns.Value.IsSupported) patterns.Add("Value"); } catch { }
            try { if (focused.Patterns.Toggle.IsSupported) patterns.Add("Toggle"); } catch { }
            try { if (focused.Patterns.Text.IsSupported) patterns.Add("Text"); } catch { }

            // Parent chain → root
            var chain = new List<(string type, string name, string aid)>();
            string? winTitle = null;
            int pid = 0;
            var cur = focused;
            var walker = _automation.TreeWalkerFactory.GetRawViewWalker();
            int safety = 30;
            while (safety-- > 0)
            {
                try { cur = walker.GetParent(cur); } catch { break; }
                if (cur == null) break;
                string pName, pCt, pAid;
                try { pName = cur.Name ?? ""; } catch { pName = ""; }
                try { pCt = cur.ControlType.ToString(); } catch { pCt = "?"; }
                try { pAid = cur.AutomationId ?? ""; } catch { pAid = ""; }
                if (pName.Length > 40) pName = pName[..40] + "…";
                chain.Add((pCt, pName, pAid));
                // Capture window title and pid from first Window-type parent
                if (winTitle == null && pCt == "Window" && !string.IsNullOrEmpty(pName))
                {
                    winTitle = pName;
                    try { pid = cur.Properties.ProcessId.Value; } catch { }
                }
            }

            return new FocusedElementInfo
            {
                Name = name, AutomationId = aid, ControlType = ctStr, Value = value,
                Patterns = patterns,
                ParentChain = chain,
                WindowTitle = winTitle,
                ProcessId = pid,
                Bounds = bounds,
                SelectionRects = selectionRects
            };
        }
        catch { return null; }
    }
}

/// <summary>Keyboard focus element info with parent chain.</summary>
public sealed class FocusedElementInfo
{
    public string Name { get; init; } = "";
    public string AutomationId { get; init; } = "";
    public string ControlType { get; init; } = "";
    public string Value { get; init; } = "";
    public List<string> Patterns { get; init; } = new();
    public List<(string type, string name, string aid)> ParentChain { get; init; } = new();
    public string? WindowTitle { get; init; }
    public int ProcessId { get; init; }
    public System.Drawing.Rectangle Bounds { get; init; } = System.Drawing.Rectangle.Empty;
    public List<System.Drawing.Rectangle> SelectionRects { get; init; } = new();
}
