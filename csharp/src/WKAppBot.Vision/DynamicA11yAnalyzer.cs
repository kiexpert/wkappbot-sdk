using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WKAppBot.Vision;

/// <summary>
/// Dynamic Accessibility Analyzer: uses Vision AI (Gemini) to infer
/// control type, state, possible actions, and label for UI elements
/// that UIA cannot describe (owner-drawn MFC, custom controls).
///
/// Results cached in experience DB per control + layout hash.
///
/// Prompt engineering: ask Gemini to act as a UI accessibility expert
/// analyzing screenshots of Win32/MFC controls from Korean financial apps.
/// </summary>
public sealed class DynamicA11yAnalyzer
{
    private const string CacheFileName = "dyn_a11y.json";

    /// <summary>Analysis result for a single UI element.</summary>
    public sealed class A11yInfo
    {
        public string ControlType { get; set; } = "Unknown";    // Button, ComboBox, ScrollBar, Tab, CheckBox, Label, TextBox, Grid, etc.
        public string State { get; set; } = "Unknown";          // Enabled, Disabled, Checked, Unchecked, Selected, Expanded, Collapsed, Pressed, etc.
        public List<string> Actions { get; set; } = new();       // click, toggle, select, scroll, type, expand, collapse, invoke, etc.
        public string Label { get; set; } = "";                  // visible text/label
        public string Description { get; set; } = "";            // brief description of what this control does
        public double Confidence { get; set; }                   // 0.0-1.0, from cross-verification
        public string DynId { get; set; } = "";                  // generated ID: dyn_{type}_r{row}c{col}_{label}
        public DateTime AnalyzedAtUtc { get; set; }
        public string LayoutHash { get; set; } = "";
    }

    /// <summary>
    /// Generate a stable dynamic AutomationId from physical properties only.
    /// No AI-inferred data (type, label) -- those can change between queries.
    /// Primary:  dyn_r{row}c{col}_{width}x{height}  (absolute pixels)
    /// Fallback: dyn_r{row}c{col}_p{w%}x{h%}        (% of parent, for ratio-based layouts)
    /// </summary>
    public static string GenerateDynId(int row, int col, int width, int height)
    {
        return $"dyn_r{row}c{col}_{width}x{height}";
    }

    /// <summary>
    /// Generate ratio-based fallback ID: percentage of parent container size.
    /// Used when container resizes but internal layout maintains proportions.
    /// </summary>
    public static string GenerateDynIdRatio(int row, int col, int width, int height, int parentW, int parentH)
    {
        if (parentW <= 0 || parentH <= 0) return GenerateDynId(row, col, width, height);
        int pw = width * 100 / parentW;
        int ph = height * 100 / parentH;
        return $"dyn_r{row}c{col}_p{pw}x{ph}";
    }

    /// <summary>
    /// Try matching a dynId against a region. Tries absolute first, then ratio fallback.
    /// Returns true if either matches within tolerance.
    /// </summary>
    public static bool MatchDynId(string dynId, int row, int col,
        int width, int height, int parentW, int parentH, int sizeTolerance = 4, int ratioTolerance = 3)
    {
        // Parse dynId: dyn_r{R}c{C}_{W}x{H} or dyn_r{R}c{C}_p{W%}x{H%}
        var match = System.Text.RegularExpressions.Regex.Match(dynId,
            @"^dyn_r(\d+)c(\d+)_(p?)(\d+)x(\d+)$");
        if (!match.Success) return false;

        int idRow = int.Parse(match.Groups[1].Value);
        int idCol = int.Parse(match.Groups[2].Value);
        if (idRow != row || idCol != col) return false;

        bool isRatio = match.Groups[3].Value == "p";
        int idW = int.Parse(match.Groups[4].Value);
        int idH = int.Parse(match.Groups[5].Value);

        if (!isRatio)
        {
            // Absolute match: pixel size within tolerance
            return Math.Abs(idW - width) <= sizeTolerance
                && Math.Abs(idH - height) <= sizeTolerance;
        }
        else
        {
            // Ratio match: parent percentage within tolerance
            if (parentW <= 0 || parentH <= 0) return false;
            int pw = width * 100 / parentW;
            int ph = height * 100 / parentH;
            return Math.Abs(idW - pw) <= ratioTolerance
                && Math.Abs(idH - ph) <= ratioTolerance;
        }
    }

    /// <summary>
    /// Assign row/col positions to regions by spatial sorting (top->bottom, left->right).
    /// Groups regions into rows by Y-overlap, then sorts columns within each row.
    /// </summary>
    public static List<(int row, int col, T region)> AssignGridPositions<T>(
        List<T> regions, Func<T, System.Drawing.Rectangle> getRect, int rowGap = 5)
    {
        var sorted = regions.Select((r, i) => (rect: getRect(r), idx: i, region: r))
            .OrderBy(x => x.rect.Y).ThenBy(x => x.rect.X).ToList();

        var result = new List<(int, int, T)>();
        int lastRowBottom = int.MinValue;

        var rowGroups = new List<List<(System.Drawing.Rectangle rect, int idx, T region)>>();
        var currentGroup = new List<(System.Drawing.Rectangle, int, T)>();

        foreach (var item in sorted)
        {
            if (currentGroup.Count > 0 && item.rect.Top > lastRowBottom + rowGap)
            {
                rowGroups.Add(currentGroup);
                currentGroup = new();
            }
            currentGroup.Add(item);
            lastRowBottom = Math.Max(lastRowBottom, item.rect.Bottom);
        }
        if (currentGroup.Count > 0) rowGroups.Add(currentGroup);

        for (int r = 0; r < rowGroups.Count; r++)
        {
            var row = rowGroups[r].OrderBy(x => x.rect.Left).ToList();
            for (int c = 0; c < row.Count; c++)
                result.Add((r + 1, c + 1, row[c].region));
        }
        return result;
    }


    /// <summary>
    /// Build the Vision API prompt for analyzing UI elements.
    /// Designed for Korean financial HTS (MFC/Win32) controls.
    /// </summary>
    public static string BuildAnalysisPrompt(int regionCount)
    {
        return $@"You are a UI accessibility expert analyzing screenshots of Windows desktop application controls.
These are from Korean financial trading software (HTS) built with Win32/MFC.
Many controls are owner-drawn with NO accessibility information -- you must infer everything visually.

The attached image contains {regionCount} numbered UI region(s).

For EACH numbered region, analyze and respond in this EXACT JSON format.
Wrap the entire JSON in a markdown code block (```json ... ```) for readability:

```json
[
  {{
    ""region"": 1,
    ""controlType"": ""Button"",
    ""state"": ""Enabled"",
    ""actions"": [""click"", ""invoke""],
    ""label"": ""매매시작"",
    ""description"": ""Order execution button""
  }}
]
```

Guidelines:
- **controlType**: Use standard UIA control types: Button, CheckBox, ComboBox, Edit (text input),
  Label (static text), Tab, ScrollBar, Slider, Grid, DataGrid, ListItem, MenuItem, TreeItem,
  StatusBar, ProgressBar, Image, Separator, Spinner, ToolTip, Custom
- **state**: Enabled, Disabled, Checked, Unchecked, Selected, Unselected, Expanded, Collapsed,
  Pressed, Focused, ReadOnly, Normal. Combine if needed: ""Enabled, Checked""
- **actions**: What a user or automation tool could do with this control:
  click, double_click, right_click, toggle, select, type, scroll, expand, collapse,
  invoke, set_value, set_range, drag, focus, read
- **label**: The visible text. Use Korean if the text is Korean. Empty string if no text.
- **description**: Brief English description of the control's purpose/function.

IMPORTANT:
- Disabled/grayed-out controls: state=""Disabled"", actions should be empty []
- Icons without text: controlType=""Image"", label="""", describe the icon in description
- If a region contains multiple sub-controls, describe the PRIMARY one
- Korean text: preserve exact characters (do NOT romanize)
- Financial context: 매수=Buy, 매도=Sell, 체결=Execution, 잔고=Balance, 종목=Stock";
    }

    /// <summary>
    /// Parse Gemini's JSON response into A11yInfo list.
    /// Tolerant: handles markdown code fences, partial JSON, etc.
    /// </summary>
    public static List<(int region, A11yInfo info)> ParseResponse(string response)
    {
        var results = new List<(int, A11yInfo)>();

        // Strip markdown code fences if present
        var jsonText = response;
        var fenceMatch = Regex.Match(response, @"```(?:json)?\s*\n?([\s\S]*?)\n?```", RegexOptions.Multiline);
        if (fenceMatch.Success)
            jsonText = fenceMatch.Groups[1].Value;

        // Try parse as JSON array
        try
        {
            using var doc = JsonDocument.Parse(jsonText);
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var elem in doc.RootElement.EnumerateArray())
                {
                    int region = elem.TryGetProperty("region", out var rp) ? rp.GetInt32() : 0;
                    var info = new A11yInfo
                    {
                        ControlType = elem.TryGetProperty("controlType", out var ct) ? ct.GetString() ?? "Unknown" : "Unknown",
                        State = elem.TryGetProperty("state", out var st) ? st.GetString() ?? "Unknown" : "Unknown",
                        Label = elem.TryGetProperty("label", out var lb) ? lb.GetString() ?? "" : "",
                        Description = elem.TryGetProperty("description", out var desc) ? desc.GetString() ?? "" : "",
                    };
                    if (elem.TryGetProperty("actions", out var acts) && acts.ValueKind == JsonValueKind.Array)
                        info.Actions = acts.EnumerateArray().Select(a => a.GetString() ?? "").Where(s => s != "").ToList();
                    results.Add((region, info));
                }
            }
        }
        catch
        {
            // Fallback: regex-based extraction for each region block
            var blockPattern = new Regex(@"""region""\s*:\s*(\d+).*?""controlType""\s*:\s*""([^""]*)""\s*.*?""state""\s*:\s*""([^""]*)""\s*.*?""actions""\s*:\s*\[(.*?)\].*?""label""\s*:\s*""([^""]*)""\s*.*?""description""\s*:\s*""([^""]*)""",
                RegexOptions.Singleline);
            foreach (Match m in blockPattern.Matches(response))
            {
                var info = new A11yInfo
                {
                    ControlType = m.Groups[2].Value,
                    State = m.Groups[3].Value,
                    Label = m.Groups[5].Value,
                    Description = m.Groups[6].Value,
                };
                var actionStr = m.Groups[4].Value;
                info.Actions = Regex.Matches(actionStr, @"""([^""]+)""")
                    .Cast<Match>().Select(am => am.Groups[1].Value).ToList();
                if (int.TryParse(m.Groups[1].Value, out int region))
                    results.Add((region, info));
            }
        }

        return results;
    }

    /// <summary>
    /// Format A11yInfo for inspect display output.
    /// </summary>
    public static string FormatForInspect(A11yInfo info)
    {
        var actions = info.Actions.Count > 0 ? string.Join(", ", info.Actions) : "(none)";
        var state = info.State != "Unknown" ? $" [{info.State}]" : "";
        var label = !string.IsNullOrEmpty(info.Label) ? $" \"{info.Label}\"" : "";
        var desc = !string.IsNullOrEmpty(info.Description) ? $" -- {info.Description}" : "";
        var conf = info.Confidence > 0 ? $" ({info.Confidence:P0})" : "";
        var dynId = !string.IsNullOrEmpty(info.DynId) ? $" aid=\"{info.DynId}\"" : "";
        return $"[DYN-A11Y] {info.ControlType}{label}{state}{dynId}{desc}{conf}\n"
             + $"  [ACTIONS: {actions}]";
    }

    // -- Cache: experience DB per control + layout hash ----------------------

    /// <summary>Load cached analysis from experience DB.</summary>
    public static A11yInfo? LoadFromCache(string controlDir, string layoutHash)
    {
        var path = Path.Combine(controlDir, CacheFileName);
        if (!File.Exists(path)) return null;
        try
        {
            var json = File.ReadAllText(path);
            var cache = JsonSerializer.Deserialize<Dictionary<string, A11yInfo>>(json);
            return cache != null && cache.TryGetValue(layoutHash, out var info) ? info : null;
        }
        catch { return null; }
    }

    /// <summary>Save analysis result to experience DB cache.</summary>
    public static void SaveToCache(string controlDir, string layoutHash, A11yInfo info)
    {
        var path = Path.Combine(controlDir, CacheFileName);
        Directory.CreateDirectory(controlDir);
        try
        {
            Dictionary<string, A11yInfo> cache;
            if (File.Exists(path))
            {
                var existing = File.ReadAllText(path);
                cache = JsonSerializer.Deserialize<Dictionary<string, A11yInfo>>(existing) ?? new();
            }
            else cache = new();

            info.AnalyzedAtUtc = DateTime.UtcNow;
            info.LayoutHash = layoutHash;
            cache[layoutHash] = info;

            // Keep max 10 layout variants per control (LRU: remove oldest)
            while (cache.Count > 10)
            {
                var oldest = cache.OrderBy(kv => kv.Value.AnalyzedAtUtc).First().Key;
                cache.Remove(oldest);
            }

            var json = JsonSerializer.Serialize(cache, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        catch { /* best effort */ }
    }
}
