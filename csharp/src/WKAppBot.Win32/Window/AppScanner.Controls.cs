using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public static partial class AppScanner
{
    /// <summary>
    /// A control with its Win32 class hierarchy tree path.
    /// TreePath mirrors the actual hWnd parent-child tree as filesystem folders.
    /// Example: "#32770_59648/AfxWnd140_999/Button_3785"
    /// </summary>
    private record struct ControlWithPath(WindowInfo Info, string TreePath);

    /// <summary>
    /// Collect leaf-level controls (buttons, labels, edits, etc.) from a form.
    /// Traverses the Win32 child tree and returns controls with no children or known leaf types.
    /// </summary>
    private static List<WindowInfo> CollectLeafControls(IntPtr hForm, RECT formRect, int maxDepth)
    {
        var leafResult = new List<ControlWithPath>();
        var parentResult = new List<ControlWithPath>();
        CollectControlsRecursive(hForm, formRect, "", leafResult, parentResult, 0, maxDepth);
        return leafResult.Select(c => c.Info).ToList();
    }

    /// <summary>
    /// Collect both leaf and parent controls with tree paths.
    /// Parent controls = controls that have children and are not leaf-like (grids, tables, panels).
    /// These get a screenshot with their children included.
    /// </summary>
    private static (List<ControlWithPath> leafControls, List<ControlWithPath> parentControls) CollectAllControls(
        IntPtr hForm, RECT formRect, int maxDepth)
    {
        var leafResult = new List<ControlWithPath>();
        var parentResult = new List<ControlWithPath>();
        CollectControlsRecursive(hForm, formRect, "", leafResult, parentResult, 0, maxDepth);
        return (leafResult, parentResult);
    }

    private static void CollectControlsRecursive(
        IntPtr hParent, RECT formRect, string parentPath,
        List<ControlWithPath> leafResult, List<ControlWithPath> parentResult,
        int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            // Skip invisible/zero-size
            if (!child.IsVisible || child.Rect.Width <= 0 || child.Rect.Height <= 0) continue;

            // Skip if outside form bounds (sometimes children have weird coords)
            if (child.Rect.Left > formRect.Right || child.Rect.Top > formRect.Bottom) continue;

            // Build tree path segment for this control
            var segment = ExperienceDb.BuildTreeSegment(child.ClassName, child.ControlId);
            var currentPath = string.IsNullOrEmpty(parentPath) ? segment : $"{parentPath}/{segment}";

            var grandChildren = WindowFinder.GetChildrenZOrder(child.Handle);

            if (grandChildren.Count == 0)
            {
                // Leaf node -- this is a single control (button, label, edit, etc.)
                leafResult.Add(new ControlWithPath(child, currentPath));
            }
            else
            {
                // Has children -- check if it's a known leaf-like class, otherwise recurse
                if (IsLeafLikeClass(child.ClassName))
                {
                    leafResult.Add(new ControlWithPath(child, currentPath));
                }
                else
                {
                    // Parent control: collect for screenshot (children included)
                    // Only significant-size parents (not tiny wrapper panels)
                    if (child.Rect.Width >= 30 && child.Rect.Height >= 30)
                    {
                        parentResult.Add(new ControlWithPath(child, currentPath));
                    }
                    CollectControlsRecursive(child.Handle, formRect, currentPath, leafResult, parentResult, depth + 1, maxDepth);
                }
            }
        }
    }

    /// <summary>Is this a class that should be treated as a leaf even if it has children?</summary>
    private static bool IsLeafLikeClass(string className)
    {
        // ComboBox has a child Edit + ListBox, but treat it as one control
        // SysTabControl32 has tab pages but treat the tab itself as leaf
        return className is "ComboBox" or "SysTabControl32" or "SysListView32"
            or "SysTreeView32" or "RichEdit20A" or "RichEdit20W";
    }

    /// <summary>Is this control worth OCR-scanning?</summary>
    private static bool IsOcrCandidate(WindowInfo ctrl)
    {
        var cls = ctrl.ClassName;

        // Definitely OCR: buttons, labels (Static doesn't expose text via GetWindowText sometimes)
        if (cls is "Button" or "Static") return true;

        // AfxWnd (MFC custom controls) -- might be owner-drawn buttons/labels
        if (cls.StartsWith("Afx", StringComparison.Ordinal)) return true;

        // Small controls that might be buttons (unknown MFC classes)
        if (ctrl.Rect.Width > 10 && ctrl.Rect.Height > 10 &&
            ctrl.Rect.Width < 300 && ctrl.Rect.Height < 100)
            return true;

        return false;
    }

    /// <summary>
    /// Find OCR-recognized text that spatially overlaps with a control's rectangle.
    /// Converts control's screen coords to form-relative coords for matching.
    /// </summary>
    private static string? FindOverlappingOcrText(
        WindowInfo ctrl, IReadOnlyList<OcrWordInfo> ocrWords, RECT formRect)
    {
        // Control rect in form-relative coordinates (OCR coords are form-relative)
        int ctrlLeft = ctrl.Rect.Left - formRect.Left;
        int ctrlTop = ctrl.Rect.Top - formRect.Top;
        int ctrlRight = ctrlLeft + ctrl.Rect.Width;
        int ctrlBottom = ctrlTop + ctrl.Rect.Height;

        // Expand search area by a small margin (OCR bounding boxes aren't always pixel-perfect)
        int margin = 4;
        ctrlLeft -= margin;
        ctrlTop -= margin;
        ctrlRight += margin;
        ctrlBottom += margin;

        var matchedWords = new List<string>();

        foreach (var word in ocrWords)
        {
            int wordCX = word.X + word.Width / 2;
            int wordCY = word.Y + word.Height / 2;

            // Check if word center falls within control rect
            if (wordCX >= ctrlLeft && wordCX <= ctrlRight &&
                wordCY >= ctrlTop && wordCY <= ctrlBottom)
            {
                matchedWords.Add(word.Text);
            }
        }

        return matchedWords.Count > 0 ? string.Join(" ", matchedWords) : null;
    }

    /// <summary>Infer semantic role from class name and OCR text.</summary>
    private static string InferRole(string className, string ocrText)
    {
        if (className == "Button") return "button";
        if (className == "Static") return "label";
        if (className == "Edit") return "input";
        if (className == "ComboBox") return "combo";
        if (className == "SysTabControl32") return "tab";

        // Check if OCR text suggests a specific role
        var lower = ocrText.ToLowerInvariant();
        if (lower.Contains("매수") || lower.Contains("buy")) return "buy_button";
        if (lower.Contains("매도") || lower.Contains("sell")) return "sell_button";
        if (lower.Contains("조회") || lower.Contains("search") || lower.Contains("검색")) return "query_button";
        if (lower.Contains("취소") || lower.Contains("cancel")) return "cancel_button";
        if (lower.Contains("확인") || lower.Contains("ok")) return "confirm_button";
        if (lower.Contains("정정") || lower.Contains("수정")) return "modify_button";

        return "control";
    }

    // -- Form-level Text Snapshot (WM_GETTEXT) ----------------─

    /// <summary>
    /// Collect all visible text (WM_GETTEXT) from a form's child controls, sorted by Y-coordinate.
    /// Returns text lines for puppet pattern building via ExperienceDb.AddTextSnapshot().
    ///
    /// Text collection is NEVER skipped even for DB-known controls -- needed for puppet pattern diff.
    /// </summary>
    /// <param name="hForm">Form window handle</param>
    /// <param name="formRect">Form bounding rectangle (for relative Y sorting)</param>
    /// <param name="maxDepth">Max recursion depth for child traversal</param>
    public static List<string> CollectFormTextSnapshot(IntPtr hForm, RECT formRect, int maxDepth = 4)
    {
        var textItems = new List<(int y, string text)>();
        CollectTextRecursive(hForm, formRect, textItems, 0, maxDepth);

        // Sort by Y-coordinate -> deduplicate adjacent identical lines
        return textItems
            .OrderBy(item => item.y)
            .Select(item => item.text)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .ToList();
    }

    private static void CollectTextRecursive(
        IntPtr hParent, RECT formRect, List<(int y, string text)> textItems,
        int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            if (!child.IsVisible || child.Rect.Width <= 0 || child.Rect.Height <= 0) continue;

            // Collect WM_GETTEXT from this control
            var text = GetWindowText(child.Handle);
            if (!string.IsNullOrWhiteSpace(text))
            {
                int relativeY = child.Rect.Top - formRect.Top;
                textItems.Add((relativeY, text));
            }

            // Recurse into children (unless leaf-like class)
            if (!IsLeafLikeClass(child.ClassName))
            {
                CollectTextRecursive(child.Handle, formRect, textItems, depth + 1, maxDepth);
            }
        }
    }
}
