using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Android;

/// <summary>
/// Android Experience DB — per-package knowledge from ADB inspection + actions.
/// Mirrors Windows ExperienceDb pattern with A11Y (profile) + OS (package) dual paths.
///
/// A11Y path: profiles/{shortPkg}_exp/         — platform-agnostic knowledge
///   knowhow.md, screen_{name}/ subtrees
/// OS path:   experience/android/{fullPkg}/    — OS-specific logs, screenshots
///   actions.jsonl, device_info.json, screenshots/
/// </summary>
public sealed class AdbExperienceDb
{
    private readonly string _hqPath;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public AdbExperienceDb(string hqPath) => _hqPath = hqPath;

    // ── Path helpers ────────────────────────────────────

    /// <summary>A11Y profile path: profiles/{shortPkg}_exp/</summary>
    public string GetA11yDir(string package)
    {
        var shortPkg = ShortPackage(package);
        return Path.Combine(_hqPath, "profiles", $"{shortPkg}_exp");
    }

    /// <summary>OS experience path: experience/android/{fullPkg}/</summary>
    public string GetOsDir(string package)
        => Path.Combine(_hqPath, "experience", "android", package);

    /// <summary>Screen-specific A11Y dir: profiles/{shortPkg}_exp/screen_{name}/</summary>
    public string GetScreenDir(string package, string screenName)
        => Path.Combine(GetA11yDir(package), $"screen_{SanitizeName(screenName)}");

    // ── Tree snapshot (ring buffer 0~9) ─────────────────

    private int _treeRingIndex;

    /// <summary>Save a11y tree dump to ring buffer file (tree_0.txt ~ tree_9.txt)</summary>
    public string SaveTreeSnapshot(string package, string treeDump, string? screenName = null)
    {
        var dir = screenName != null
            ? GetScreenDir(package, screenName)
            : Path.Combine(GetA11yDir(package), "android_tree");
        Directory.CreateDirectory(dir);

        var idx = _treeRingIndex++ % 10;
        var path = Path.Combine(dir, $"tree_{idx}.txt");
        File.WriteAllText(path, $"# {DateTime.Now:yyyy-MM-dd HH:mm:ss} package={package}\n{treeDump}");
        return path;
    }

    // ── Action log (append-only JSONL) ──────────────────

    /// <summary>Log an action result to actions.jsonl</summary>
    public void LogAction(string package, AdbActionLog entry)
    {
        var dir = GetOsDir(package);
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, "actions.jsonl");
        var json = JsonSerializer.Serialize(entry, JsonOpts);
        // Single-line for JSONL
        var line = json.Replace("\r\n", " ").Replace("\n", " ");
        File.AppendAllText(path, line + "\n");
    }

    // ── Screenshot save (ring buffer 0~9) ───────────────

    public string SaveScreenshot(string package, string sourcePath)
    {
        var dir = Path.Combine(GetOsDir(package), "screenshots");
        Directory.CreateDirectory(dir);

        // Find next ring index from existing files
        var existing = Directory.GetFiles(dir, "screen_*.png").Length;
        var idx = existing % 10;
        var dest = Path.Combine(dir, $"screen_{idx}.png");
        File.Copy(sourcePath, dest, overwrite: true);
        return dest;
    }

    // ── Device info ─────────────────────────────────────

    public void SaveDeviceInfo(string package, string serial, string? model, string? displayInfo)
    {
        var dir = GetOsDir(package);
        Directory.CreateDirectory(dir);
        var info = new { Serial = serial, Model = model, Display = displayInfo, UpdatedAt = DateTime.Now };
        File.WriteAllText(Path.Combine(dir, "device_info.json"), JsonSerializer.Serialize(info, JsonOpts));
    }

    // ── Knowhow broadcast ───────────────────────────────

    /// <summary>Get knowhow files for broadcast (both A11Y and OS paths)</summary>
    public List<(string Path, string Tag)> GetKnowhowFiles(string package, string? screenName = null)
    {
        var results = new List<(string, string)>();

        // A11Y knowhow
        var a11yDir = GetA11yDir(package);
        var a11yKh = Path.Combine(a11yDir, "knowhow.md");
        if (File.Exists(a11yKh)) results.Add((a11yKh, "KNOWHOW:A11Y"));

        // Screen-level knowhow
        if (screenName != null)
        {
            var screenDir = GetScreenDir(package, screenName);
            var screenKh = Path.Combine(screenDir, "knowhow.md");
            if (File.Exists(screenKh)) results.Add((screenKh, "KNOWHOW:A11Y"));

            // Any extra knowhow-*.md files
            if (Directory.Exists(screenDir))
                foreach (var kh in Directory.GetFiles(screenDir, "knowhow-*.md").Take(5))
                    results.Add((kh, "KNOWHOW:A11Y"));
        }

        // OS knowhow
        var osDir = GetOsDir(package);
        var osKh = Path.Combine(osDir, "knowhow.md");
        if (File.Exists(osKh)) results.Add((osKh, "KNOWHOW:OS"));

        return results;
    }

    // ── Utility ─────────────────────────────────────────

    private static string ShortPackage(string package)
    {
        var dot = package.LastIndexOf('.');
        return dot >= 0 ? package[(dot + 1)..] : package;
    }

    private static string SanitizeName(string name)
        => name.Replace(' ', '_').Replace('/', '_').Replace('\\', '_')
               .Replace(':', '_').Replace('#', '_').Replace('?', '_');
}

// ── Action log entry ────────────────────────────────────

public class AdbActionLog
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Action { get; set; } = "";
    public string? Target { get; set; }
    public string? Package { get; set; }
    public string? Device { get; set; }
    public bool Success { get; set; }
    public string? Detail { get; set; }
    public int? TapX { get; set; }
    public int? TapY { get; set; }
}
