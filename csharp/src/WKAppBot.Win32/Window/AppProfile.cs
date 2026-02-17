using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Win32.Window;

/// <summary>
/// App Profile — persistent knowledge about a target application's window structure.
/// Saved as JSON in {exe_dir}/profiles/{profile_name}.json
///
/// The profile stores:
///   - App identification (process name, window class, title pattern)
///   - Top-level zone map (which cid is toolbar, MDI container, etc.)
///   - Form type definitions (form_id → known controls with semantic roles)
///
/// On subsequent scans, the scanner only traverses the tree until it hits
/// a known DB item (form, grid, chart, etc.) — no need to go deeper.
/// </summary>
public sealed class AppProfile
{
    [JsonPropertyName("app")]
    public AppIdentity App { get; set; } = new();

    [JsonPropertyName("zones")]
    public List<ZoneDefinition> Zones { get; set; } = new();

    [JsonPropertyName("form_types")]
    public Dictionary<string, FormTypeDefinition> FormTypes { get; set; } = new();

    [JsonPropertyName("known_stock_code_cids")]
    public List<int> KnownStockCodeCids { get; set; } = new() { 3780 };

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("scan_count")]
    public int ScanCount { get; set; }
}

public sealed class AppIdentity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("process")]
    public string Process { get; set; } = "";

    [JsonPropertyName("window_class")]
    public string WindowClass { get; set; } = "";

    [JsonPropertyName("title_match")]
    public string TitleMatch { get; set; } = "";
}

public sealed class ZoneDefinition
{
    [JsonPropertyName("cid")]
    public int ControlId { get; set; }

    [JsonPropertyName("class")]
    public string? ClassName { get; set; }

    [JsonPropertyName("zone")]
    public string Zone { get; set; } = "";

    [JsonPropertyName("desc")]
    public string? Description { get; set; }

    [JsonPropertyName("children")]
    public List<ControlDefinition>? Children { get; set; }
}

public sealed class FormTypeDefinition
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("frame_class")]
    public string? FrameClass { get; set; }

    [JsonPropertyName("controls")]
    public List<ControlDefinition> Controls { get; set; } = new();

    [JsonPropertyName("scan_count")]
    public int ScanCount { get; set; }

    /// <summary>
    /// Structural fingerprint hash from Experience DB (copied on --save).
    /// Enables fingerprint-based form matching without loading full experience data.
    /// </summary>
    [JsonPropertyName("fingerprint_hash")]
    public string? FingerprintHash { get; set; }
}

public sealed class ControlDefinition
{
    [JsonPropertyName("cid")]
    public int ControlId { get; set; }

    [JsonPropertyName("class")]
    public string? ClassName { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; } = "";

    [JsonPropertyName("desc")]
    public string? Description { get; set; }

    [JsonPropertyName("ocr_text")]
    public string? OcrText { get; set; }

    [JsonPropertyName("ocr_confidence")]
    public double? OcrConfidence { get; set; }
}

// ── Profile Store (Load/Save/Match) ──────────────────────

/// <summary>
/// Manages app profile files on disk.
/// Profiles stored as JSON in {exe_dir}/profiles/
/// </summary>
public sealed class ProfileStore
{
    private readonly string _profileDir;
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public ProfileStore(string? profileDir = null)
    {
        _profileDir = profileDir
            ?? Path.Combine(Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "profiles");

        if (!Directory.Exists(_profileDir))
            Directory.CreateDirectory(_profileDir);
    }

    public string ProfileDir => _profileDir;

    /// <summary>
    /// Try to find a matching profile by window class or process name.
    /// </summary>
    public (string name, AppProfile profile)? FindByMatch(string windowClass, string processName)
    {
        if (!Directory.Exists(_profileDir)) return null;

        foreach (var file in Directory.GetFiles(_profileDir, "*.json"))
        {
            try
            {
                var json = File.ReadAllText(file);
                var profile = JsonSerializer.Deserialize<AppProfile>(json, JsonOpts);
                if (profile == null) continue;

                // Match by window class (exact)
                if (!string.IsNullOrEmpty(profile.App.WindowClass) &&
                    string.Equals(profile.App.WindowClass, windowClass, StringComparison.OrdinalIgnoreCase))
                {
                    return (Path.GetFileNameWithoutExtension(file), profile);
                }

                // Match by process name
                if (!string.IsNullOrEmpty(profile.App.Process) &&
                    string.Equals(profile.App.Process, processName, StringComparison.OrdinalIgnoreCase))
                {
                    return (Path.GetFileNameWithoutExtension(file), profile);
                }
            }
            catch { /* skip corrupt profiles */ }
        }

        return null;
    }

    /// <summary>
    /// Save a profile to disk.
    /// </summary>
    public void Save(string name, AppProfile profile)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        var path = Path.Combine(_profileDir, $"{name}.json");
        var json = JsonSerializer.Serialize(profile, JsonOpts);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Load a profile by name.
    /// </summary>
    public AppProfile? Load(string name)
    {
        var path = Path.Combine(_profileDir, $"{name}.json");
        if (!File.Exists(path)) return null;
        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<AppProfile>(json, JsonOpts);
        }
        catch { return null; }
    }

    /// <summary>
    /// List all saved profiles.
    /// </summary>
    public IEnumerable<string> ListProfiles()
    {
        if (!Directory.Exists(_profileDir)) yield break;
        foreach (var file in Directory.GetFiles(_profileDir, "*.json"))
            yield return Path.GetFileNameWithoutExtension(file);
    }

    /// <summary>
    /// Generate a profile name from scan result.
    /// Sanitizes to filesystem-safe characters.
    /// </summary>
    public static string GenerateProfileName(AppScanResult scan)
    {
        // Prefer process name, fallback to sanitized title
        var name = !string.IsNullOrEmpty(scan.ProcessName)
            ? scan.ProcessName
            : scan.WindowTitle;

        // Sanitize: lowercase, replace spaces/special chars with underscore
        name = name.ToLowerInvariant();
        name = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-z0-9가-힣]", "_");
        name = System.Text.RegularExpressions.Regex.Replace(name, @"_+", "_").Trim('_');

        return string.IsNullOrEmpty(name) ? "unknown_app" : name;
    }

    /// <summary>
    /// Create an AppProfile from a scan result.
    /// Optionally copies fingerprint hashes from Experience DB.
    /// </summary>
    public static AppProfile FromScanResult(AppScanResult scan, ExperienceDb? expDb = null)
    {
        var profile = new AppProfile
        {
            App = new AppIdentity
            {
                Name = scan.WindowTitle,
                Process = scan.ProcessName,
                WindowClass = scan.WindowClass,
                TitleMatch = ExtractTitleKeyword(scan.WindowTitle),
            },
            ScanCount = 1,
        };

        // Zones
        foreach (var z in scan.Zones)
        {
            if (z.Zone.Type == ZoneType.Form) continue; // forms go in FormTypes

            var zoneDef = new ZoneDefinition
            {
                ControlId = z.ControlId,
                ClassName = z.ClassName,
                Zone = z.Zone.Tag,
                Description = z.Zone.Description,
            };

            // Add notable children
            if (z.NotableChildren.Count > 0)
            {
                zoneDef.Children = z.NotableChildren.Select(nc => new ControlDefinition
                {
                    ControlId = nc.ControlId,
                    ClassName = nc.ClassName,
                    Role = nc.Zone.Tag,
                    Description = nc.Zone.Description,
                }).ToList();
            }

            profile.Zones.Add(zoneDef);
        }

        // Form types (grouped by form_id)
        var formGroups = scan.Forms
            .Where(f => f.FormId != null)
            .GroupBy(f => f.FormId!);

        foreach (var g in formGroups)
        {
            var first = g.First();
            var formDef = new FormTypeDefinition
            {
                Name = first.FormName ?? g.Key,
                FrameClass = first.ClassName,
                ScanCount = 1,
            };

            // Copy fingerprint hash from Experience DB if available
            var formExp = expDb?.GetForm(g.Key);
            if (formExp != null)
            {
                formDef.FingerprintHash = formExp.FingerprintHash;
            }

            profile.FormTypes[g.Key] = formDef;
        }

        // Detect stock code CID pattern
        // (default 3780 already in KnownStockCodeCids)

        return profile;
    }

    private static string ExtractTitleKeyword(string title)
    {
        // Try to find a short, meaningful keyword
        // "LS증권 투혼" → "투혼", "영웅문4" → "영웅문"
        var parts = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2) return parts[^1]; // last word
        return title;
    }
}
