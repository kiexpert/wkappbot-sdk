using System.Text.RegularExpressions;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Auto-classifies top-level Win32 child windows into semantic zones.
/// Heuristic engine — uses class name, size, visibility, title, and cid
/// to determine if a child is a toolbar, MDI container, status bar, form, etc.
///
/// Designed to work with ANY MFC-based HTS app:
///   - LS Securities Tuhon (AfxWnd140, ETK_CHILDFRAME_WINDOW)
///   - Kiwoom HeroMun4 (AfxWnd110, _NKHeroMainClass)
///   - NH NaMu, etc.
/// </summary>
public static class ZoneClassifier
{
    /// <summary>
    /// Classify a child window's semantic role based on heuristics.
    /// </summary>
    /// <param name="child">The child window to classify</param>
    /// <param name="parentRect">Parent window rect (for relative size checks)</param>
    public static ZoneInfo Classify(WindowInfo child, Native.RECT parentRect)
    {
        var cls = child.ClassName;
        var title = child.Title;
        var r = child.Rect;
        int parentW = parentRect.Width;

        // Rule 1: MDIClient — universal MDI container (same cid=59648 across all HTS)
        if (cls == "MDIClient")
            return new ZoneInfo(ZoneType.MdiContainer, "MDI 폼 컨테이너");

        // Rule 2: Known form frame classes (HTS-specific MDI child frames)
        if (cls == "ETK_CHILDFRAME_WINDOW")
            return new ZoneInfo(ZoneType.Form, TryExtractFormDesc(title));

        // Rule 3: Form-like — title matches [NNNN] pattern (Kiwoom etc.)
        if (!string.IsNullOrEmpty(title) && FormIdRegex.IsMatch(title))
            return new ZoneInfo(ZoneType.Form, TryExtractFormDesc(title));

        // Rule 4: Chrome/Edge embedded webview
        if (cls.StartsWith("Chrome_WidgetWin", StringComparison.Ordinal))
            return new ZoneInfo(ZoneType.WebView, title ?? "WebView");

        // Rule 5: Zero-size or invisible → background service
        if ((r.Width <= 0 && r.Height <= 0) || !child.IsVisible)
        {
            // Check if it has a meaningful service name
            if (!string.IsNullOrEmpty(title) &&
                (title.Contains("알람") || title.Contains("관리자") || title.Contains("COMM") ||
                 title.Contains("GAMSI") || title.Contains("감시")))
                return new ZoneInfo(ZoneType.Service, title);

            return new ZoneInfo(ZoneType.Service, string.IsNullOrEmpty(title) ? $"cid={child.ControlId}" : title);
        }

        // Rule 6: Edit control → input field
        if (cls == "Edit")
            return new ZoneInfo(ZoneType.Input, "텍스트 입력");

        // Rule 7: ReBarWindow32 — Windows standard rebar (Kiwoom uses this for toolbars)
        if (cls == "ReBarWindow32")
            return new ZoneInfo(ZoneType.Toolbar, title ?? "Rebar 툴바");

        // Rule 8: ToolbarWindow32 — standard toolbar
        if (cls == "ToolbarWindow32")
            return new ZoneInfo(ZoneType.Toolbar, title ?? "툴바");

        // Rule 9: AfxControlBar — MFC control bar (toolbar/status)
        if (cls.StartsWith("AfxControlBar", StringComparison.Ordinal))
        {
            if (!string.IsNullOrEmpty(title))
                return new ZoneInfo(ZoneType.Toolbar, title);
            return new ZoneInfo(ZoneType.Toolbar, "컨트롤 바");
        }

        // Rule 10: Full-width thin strip → bar (toolbar/menu/status)
        if (parentW > 0 && r.Width >= parentW * 0.7 && r.Height > 0 && r.Height < 80)
        {
            // Heuristic: top area = toolbar/menu, bottom area = status bar
            return new ZoneInfo(ZoneType.Bar, string.IsNullOrEmpty(title) ? $"bar ({r.Width}x{r.Height})" : title);
        }

        // Rule 11: Small button
        if (cls == "Button" && r.Width > 0 && r.Height > 0 && r.Width * r.Height < 5000)
            return new ZoneInfo(ZoneType.QuickButton, title ?? "버튼");

        // Default
        return new ZoneInfo(ZoneType.Unknown,
            string.IsNullOrEmpty(title) ? $"[{cls}] cid={child.ControlId}" : title);
    }

    // ── Form title parsing ─────────────────────────────────────

    private static readonly Regex FormIdRegex = new(@"^\[(\d+)\]\s*(.+)$", RegexOptions.Compiled);

    /// <summary>
    /// Extract form ID and name from title like "[1101] 현재가" or "[0606] 자동일지차트(매매내역)"
    /// </summary>
    public static (string? formId, string? formName) ParseFormTitle(string? title)
    {
        if (string.IsNullOrEmpty(title)) return (null, null);
        var m = FormIdRegex.Match(title);
        if (!m.Success) return (null, null);
        return (m.Groups[1].Value, m.Groups[2].Value.Trim());
    }

    private static string TryExtractFormDesc(string? title)
    {
        var (formId, formName) = ParseFormTitle(title);
        if (formId != null && formName != null)
            return $"[{formId}] {formName}";
        return title ?? "form";
    }
}

/// <summary>Semantic zone types for window classification.</summary>
public enum ZoneType
{
    /// <summary>Toolbar or menu bar area</summary>
    Toolbar,
    /// <summary>MDI child container (MDIClient)</summary>
    MdiContainer,
    /// <summary>Horizontal bar (menu, status, etc.)</summary>
    Bar,
    /// <summary>MDI child form window</summary>
    Form,
    /// <summary>Text input control</summary>
    Input,
    /// <summary>Embedded web browser (Chrome/Edge)</summary>
    WebView,
    /// <summary>Background service window (invisible/zero-size)</summary>
    Service,
    /// <summary>Small standalone button</summary>
    QuickButton,
    /// <summary>Unclassified</summary>
    Unknown
}

/// <summary>Classification result for a window zone.</summary>
public sealed record ZoneInfo(ZoneType Type, string Description)
{
    /// <summary>Short tag for display (e.g., "[mdi]", "[toolbar]", "[form]")</summary>
    public string Tag => Type switch
    {
        ZoneType.Toolbar => "toolbar",
        ZoneType.MdiContainer => "mdi",
        ZoneType.Bar => "bar",
        ZoneType.Form => "form",
        ZoneType.Input => "input",
        ZoneType.WebView => "webview",
        ZoneType.Service => "service",
        ZoneType.QuickButton => "button",
        ZoneType.Unknown => "???",
        _ => "???"
    };
}
