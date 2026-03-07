using System.Text.Json;

namespace WKAppBot.Android;

/// <summary>
/// Device discovery and alias management.
/// Alias file: wkappbot.hq/profiles/adb_devices.json
/// Format: {"Fold5": "R3CW70TFJ7A", "Fold5:outer": "R3CW70TFJ7A:4630946481096930692"}
/// </summary>
public class AdbDeviceRegistry
{
    private readonly AdbClient _adb;
    private readonly string _aliasPath;
    private Dictionary<string, string>? _aliases;

    public AdbDeviceRegistry(AdbClient adb, string? hqPath = null)
    {
        _adb = adb;
        var hq = hqPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "SDK", "bin", "wkappbot.hq");
        _aliasPath = Path.Combine(hq, "profiles", "adb_devices.json");
    }

    // ── Device resolution ─────────────────────────────────

    /// <summary>
    /// Resolve device identifier to serial + optional display ID.
    /// Matching order: alias → model → serial (wildcard supported)
    /// Returns (serial, displayId?) or null if not found.
    /// </summary>
    public (string serial, string? displayId)? ResolveDevice(string? deviceName)
    {
        var devices = _adb.ListDevices().Where(d => d.IsOnline).ToList();
        if (devices.Count == 0) return null;

        // No device specified → auto-detect single device
        if (string.IsNullOrEmpty(deviceName))
        {
            if (devices.Count == 1) return (devices[0].Serial, null);
            return null; // Ambiguous — multiple devices
        }

        // Split display suffix: "Fold5:outer" → device="Fold5", display hint
        string? displayHint = null;
        var colonIdx = deviceName.IndexOf(':');
        if (colonIdx > 0)
        {
            displayHint = deviceName[(colonIdx + 1)..];
            deviceName = deviceName[..colonIdx];
        }

        // 1. Check aliases
        var aliases = LoadAliases();
        if (aliases.TryGetValue(deviceName, out var aliasValue))
        {
            // Alias value can be "SERIAL" or "SERIAL:DISPLAY_ID"
            var parts = aliasValue.Split(':', 2);
            var serial = parts[0];
            var displayId = parts.Length > 1 ? parts[1] : null;
            if (displayHint != null) displayId = ResolveDisplayHint(displayHint, serial);
            return (serial, displayId);
        }

        // 2. Match by model name (case-insensitive, wildcard)
        var byModel = MatchDevice(devices, deviceName, d => d.Model);
        if (byModel != null)
        {
            var displayId = displayHint != null ? ResolveDisplayHint(displayHint, byModel.Serial) : null;
            return (byModel.Serial, displayId);
        }

        // 3. Match by serial (wildcard)
        var bySerial = MatchDevice(devices, deviceName, d => d.Serial);
        if (bySerial != null)
        {
            var displayId = displayHint != null ? ResolveDisplayHint(displayHint, bySerial.Serial) : null;
            return (bySerial.Serial, displayId);
        }

        // 4. Match by product or device name
        var byProduct = MatchDevice(devices, deviceName, d => d.Product)
                     ?? MatchDevice(devices, deviceName, d => d.DeviceName);
        if (byProduct != null)
        {
            var displayId = displayHint != null ? ResolveDisplayHint(displayHint, byProduct.Serial) : null;
            return (byProduct.Serial, displayId);
        }

        return null;
    }

    /// <summary>List all connected devices with alias info</summary>
    public List<DeviceInfo> ListAll()
    {
        var devices = _adb.ListDevices();
        var aliases = LoadAliases();
        var reverseAlias = aliases.ToDictionary(kv => kv.Value.Split(':')[0], kv => kv.Key,
            StringComparer.OrdinalIgnoreCase);

        return devices.Select(d => new DeviceInfo
        {
            Device = d,
            Alias = reverseAlias.GetValueOrDefault(d.Serial),
            Displays = d.IsOnline ? GetDisplays(d.Serial) : []
        }).ToList();
    }

    /// <summary>Auto-register a device with a friendly alias</summary>
    public void RegisterAlias(string alias, string serial, string? displayId = null)
    {
        var aliases = LoadAliases();
        aliases[alias] = displayId != null ? $"{serial}:{displayId}" : serial;
        SaveAliases(aliases);
    }

    // ── Display handling (foldables) ──────────────────────

    public List<DisplayInfo> GetDisplays(string serial)
    {
        var r = _adb.Shell("dumpsys SurfaceFlinger --display-id", serial);
        if (!r.IsOk) return [];

        var displays = new List<DisplayInfo>();
        foreach (var line in r.StdOut.Split('\n'))
        {
            // Display 4630947175568309891 (HWC display 0): port=131 ...
            var match = System.Text.RegularExpressions.Regex.Match(line,
                @"Display (\d+) \(HWC display (\d+)\)");
            if (match.Success)
            {
                displays.Add(new DisplayInfo
                {
                    DisplayId = match.Groups[1].Value,
                    HwcIndex = int.Parse(match.Groups[2].Value),
                    IsInner = match.Groups[2].Value == "0" // HWC 0 = primary (inner for foldable)
                });
            }
        }
        return displays;
    }

    private string? ResolveDisplayHint(string hint, string serial)
    {
        var displays = GetDisplays(serial);
        if (displays.Count == 0) return null;

        var hintLower = hint.ToLowerInvariant();
        if (hintLower is "inner" or "main" or "0")
            return displays.FirstOrDefault(d => d.IsInner)?.DisplayId;
        if (hintLower is "outer" or "cover" or "external")
            return displays.FirstOrDefault(d => !d.IsInner)?.DisplayId;

        // Try matching by display ID directly
        return displays.FirstOrDefault(d => d.DisplayId == hint)?.DisplayId;
    }

    // ── Pattern matching ──────────────────────────────────

    private static AdbDevice? MatchDevice(List<AdbDevice> devices, string pattern, Func<AdbDevice, string?> selector)
    {
        // Exact match first
        var exact = devices.FirstOrDefault(d => string.Equals(selector(d), pattern, StringComparison.OrdinalIgnoreCase));
        if (exact != null) return exact;

        // Wildcard match
        if (pattern.Contains('*') || pattern.Contains('?'))
        {
            var regex = "^" + System.Text.RegularExpressions.Regex.Escape(pattern)
                .Replace("\\*", ".*").Replace("\\?", ".") + "$";
            return devices.FirstOrDefault(d =>
            {
                var val = selector(d);
                return val != null && System.Text.RegularExpressions.Regex.IsMatch(val, regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            });
        }

        // Substring match (convenience)
        return devices.FirstOrDefault(d =>
        {
            var val = selector(d);
            return val != null && val.Contains(pattern, StringComparison.OrdinalIgnoreCase);
        });
    }

    // ── Alias persistence ─────────────────────────────────

    private Dictionary<string, string> LoadAliases()
    {
        if (_aliases != null) return _aliases;
        if (!File.Exists(_aliasPath)) return _aliases = new();
        try
        {
            var json = File.ReadAllText(_aliasPath);
            _aliases = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
        }
        catch { _aliases = new(); }
        return _aliases;
    }

    private void SaveAliases(Dictionary<string, string> aliases)
    {
        _aliases = aliases;
        var dir = Path.GetDirectoryName(_aliasPath)!;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(_aliasPath, JsonSerializer.Serialize(aliases, new JsonSerializerOptions { WriteIndented = true }));
    }
}

// ── Models ────────────────────────────────────────────────

public class DeviceInfo
{
    public required AdbDevice Device { get; init; }
    public string? Alias { get; init; }
    public List<DisplayInfo> Displays { get; init; } = [];

    public override string ToString()
    {
        var alias = Alias != null ? $" (alias: {Alias})" : "";
        var displays = Displays.Count > 0
            ? $" [{string.Join(", ", Displays.Select(d => d.IsInner ? "inner" : "outer"))}]"
            : "";
        return $"{Device}{alias}{displays}";
    }
}

public class DisplayInfo
{
    public required string DisplayId { get; init; }
    public int HwcIndex { get; init; }
    public bool IsInner { get; init; }
}
