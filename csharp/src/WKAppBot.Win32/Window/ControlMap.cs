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

    /// <summary>
    /// Create a ControlMap from an AppProfile + ExperienceDb.
    /// Combines profile's form_type definitions with experience DB's OCR-learned controls.
    ///
    /// Priority: Experience DB (OCR-learned, cid-specific) > Profile (static definitions) > default
    /// </summary>
    /// <param name="profile">App profile with zone/form structure</param>
    /// <param name="expDb">Optional experience DB with OCR-learned controls</param>
    /// <returns>ControlMap populated from profile + experience knowledge</returns>
    public static ControlMap FromProfile(AppProfile profile, ExperienceDb? expDb = null)
    {
        var map = new ControlMap();

        // 1. Register MDI structure (universal)
        map.Register("MDIClient", null, "mdi_container", "MDI child container");

        // 2. Register zone-level controls from profile
        foreach (var zone in profile.Zones)
        {
            if (!string.IsNullOrEmpty(zone.ClassName))
            {
                map.Register(zone.ClassName, zone.ControlId > 0 ? zone.ControlId : null,
                    zone.Zone, zone.Description);
            }

            // Zone children (e.g., Edit cid=2000 inside a bar)
            if (zone.Children != null)
            {
                foreach (var child in zone.Children)
                {
                    if (!string.IsNullOrEmpty(child.ClassName))
                    {
                        map.Register(child.ClassName, child.ControlId > 0 ? child.ControlId : null,
                            child.Role, child.Description);
                    }
                }
            }
        }

        // 3. Register form-type controls from profile (static definitions)
        foreach (var (formId, formDef) in profile.FormTypes)
        {
            // Register the form frame class itself
            if (!string.IsNullOrEmpty(formDef.FrameClass))
            {
                map.Register(formDef.FrameClass, null, "form_frame",
                    $"[{formId}] {formDef.Name}");
            }

            // Register form-level control definitions
            foreach (var ctrl in formDef.Controls)
            {
                if (!string.IsNullOrEmpty(ctrl.ClassName))
                {
                    var desc = ctrl.Description ?? ctrl.OcrText ?? $"[{formId}] cid={ctrl.ControlId}";
                    map.Register(ctrl.ClassName, ctrl.ControlId > 0 ? ctrl.ControlId : null,
                        ctrl.Role, desc);
                }
            }
        }

        // 4. Register experience DB controls (OCR-learned — highest priority)
        if (expDb != null)
        {
            foreach (var (formId, formExp) in expDb.GetAllForms())
            {
                foreach (var ctrl in formExp.Controls)
                {
                    if (string.IsNullOrEmpty(ctrl.ClassName)) continue;

                    var role = ctrl.Role ?? "control";
                    var desc = ctrl.OcrText ?? $"[{formId}] cid={ctrl.ControlId}";

                    // Experience DB entries are cid-specific → always register with cid
                    map.Register(ctrl.ClassName, ctrl.ControlId, role, desc);
                }
            }
        }

        // 5. Register common standard controls (fallback)
        // Only register if not already covered by profile/experience
        var standardDefaults = new Dictionary<string, string>
        {
            ["Button"] = "button",
            ["Static"] = "label",
            ["Edit"] = "text_input",
            ["ComboBox"] = "combo_box",
            ["ListBox"] = "list_box",
            ["SysTabControl32"] = "tab_control",
            ["SysListView32"] = "list_view",
            ["SysTreeView32"] = "tree_view",
            ["RichEdit20A"] = "rich_edit",
            ["RichEdit20W"] = "rich_edit",
        };

        foreach (var (cls, role) in standardDefaults)
        {
            if (map.Lookup(cls, 0) == null)
                map.Register(cls, null, role, $"Standard Win32 {cls}");
        }

        // 6. Register stock code CID pattern from profile
        foreach (var cid in profile.KnownStockCodeCids)
        {
            // Register for common Afx classes (different MFC versions)
            foreach (var afxPrefix in new[] { "AfxWnd110", "AfxWnd140" })
            {
                if (map.Lookup(afxPrefix, cid) == null)
                    map.Register(afxPrefix, cid, "stock_code_input", $"Stock code input (cid={cid})");
            }
        }

        return map;
    }

    /// <summary>
    /// Get total number of registered entries.
    /// </summary>
    public int Count => _byKey.Count;

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
