namespace WKAppBot.Win32.Window;

/// <summary>
/// Maps Win32 class names and control IDs to semantic roles.
/// For apps where Accessibility (UIA) is missing or incomplete (e.g., MFC/GDI apps).
/// Builds a learning cache from inspection sessions.
/// </summary>
public sealed class ControlMap
{
    private readonly Dictionary<string, ControlMapEntry> _byKey = new();

    /// <summary>
    /// Register a known control mapping.
    /// Key format: "ClassName:ControlId" or "ClassName" (for all of that class).
    /// </summary>
    public void Register(string className, int? controlId, string semanticRole, string? description = null)
    {
        var key = MakeKey(className, controlId);
        _byKey[key] = new ControlMapEntry
        {
            ClassName = className,
            ControlId = controlId,
            SemanticRole = semanticRole,
            Description = description
        };
    }

    /// <summary>
    /// Look up the semantic role of a control.
    /// Tries exact match (className:ctrlId) first, then class-only match.
    /// </summary>
    public ControlMapEntry? Lookup(string className, int controlId)
    {
        // Try exact match
        var exactKey = MakeKey(className, controlId);
        if (_byKey.TryGetValue(exactKey, out var exact)) return exact;

        // Try class-only match
        var classKey = MakeKey(className, null);
        if (_byKey.TryGetValue(classKey, out var classMatch)) return classMatch;

        return null;
    }

    /// <summary>
    /// Get all registered entries.
    /// </summary>
    public IReadOnlyCollection<ControlMapEntry> GetAll() => _byKey.Values;

    /// <summary>
    /// Create a default map for HTS (LS Securities) application.
    /// Based on inspection data from previous sessions.
    /// </summary>
    public static ControlMap CreateHtsDefault()
    {
        var map = new ControlMap();

        // MDI structure
        map.Register("MDIClient", null, "mdi_container", "MDI child container");
        map.Register("ETK_CHILDFRAME_WINDOW", null, "mdi_child", "HTS form (MDI child frame)");

        // Afx custom controls (MFC)
        map.Register("AfxWnd140", 3780, "stock_code_input", "Stock code input combo (e.g., 005930)");
        map.Register("AfxWnd140", 3785, "query_button", "Query/Search button");
        map.Register("AfxWnd140", 3950, "filter_all", "Full filter control");
        map.Register("AfxWnd140", 3955, "chart_window", "Chart display area");

        // Standard controls
        map.Register("Button", null, "button", "Standard Win32 button");
        map.Register("Static", null, "label", "Static text label");
        map.Register("Edit", null, "text_input", "Standard text input");
        map.Register("ComboBox", null, "combo_box", "Standard combo box");
        map.Register("ListBox", null, "list_box", "Standard list box");

        // GridChe (custom grid control)
        map.Register("AfxWnd140", null, "custom_control", "MFC custom control (owner-draw)");

        return map;
    }

    private static string MakeKey(string className, int? controlId)
        => controlId.HasValue ? $"{className}:{controlId}" : className;
}

public sealed class ControlMapEntry
{
    public string ClassName { get; init; } = "";
    public int? ControlId { get; init; }
    public string SemanticRole { get; init; } = "";
    public string? Description { get; init; }

    public override string ToString() =>
        $"[{SemanticRole}] {ClassName}" +
        (ControlId.HasValue ? $":{ControlId}" : "") +
        (Description != null ? $" - {Description}" : "");
}
