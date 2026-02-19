using System.Security.Cryptography;
using System.Text;

namespace WKAppBot.WebBot;

/// <summary>
/// Per-site and per-element knowhow storage for web automation.
/// File-based, append-only, best-effort. No caching — reads from disk each time.
///
/// Directory layout:
///   {webProfilesDir}/_global/knowhow.md                              — cross-site patterns
///   {webProfilesDir}/{domain}/knowhow.md                             — site-level quirks
///   {webProfilesDir}/{domain}/elements/{label}_{hash}/knowhow.md     — element-level quirks
/// </summary>
public static class WebKnowhow
{
    /// <summary>Append site-level knowhow for a domain.</summary>
    public static bool AppendSiteKnowhow(string webProfilesDir, string domain,
        string category, string lesson)
    {
        try
        {
            var dir = Path.Combine(webProfilesDir, SanitizeDomain(domain));
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, "knowhow.md");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            File.AppendAllText(path, $"\n## [{timestamp}] {category}\n{lesson}\n");
            return true;
        }
        catch { return false; }
    }

    /// <summary>Append element-level knowhow for a specific CSS selector.</summary>
    public static bool AppendElementKnowhow(string webProfilesDir, string domain,
        string cssSelector, string category, string lesson)
    {
        try
        {
            var hash = ComputeSelectorHash(cssSelector);
            var label = ExtractSelectorLabel(cssSelector);
            var dir = Path.Combine(webProfilesDir, SanitizeDomain(domain),
                "elements", $"{label}_{hash}");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "knowhow.md");

            // Write selector as header on first creation
            if (!File.Exists(path))
                File.WriteAllText(path, $"# Element: `{cssSelector}`\n");

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            File.AppendAllText(path, $"\n## [{timestamp}] {category}\n{lesson}\n");
            return true;
        }
        catch { return false; }
    }

    /// <summary>Append global (cross-site) knowhow.</summary>
    public static bool AppendGlobalKnowhow(string webProfilesDir,
        string category, string lesson)
    {
        try
        {
            var dir = Path.Combine(webProfilesDir, "_global");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, "knowhow.md");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            File.AppendAllText(path, $"\n## [{timestamp}] {category}\n{lesson}\n");
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Read all knowhow for a domain. Merges global + site + optionally element.
    /// Returns null if no knowhow exists.
    /// </summary>
    public static string? ReadKnowhow(string webProfilesDir, string domain,
        string? cssSelector = null)
    {
        var parts = new List<string>();

        // Global knowhow
        var globalPath = Path.Combine(webProfilesDir, "_global", "knowhow.md");
        if (File.Exists(globalPath))
            parts.Add($"# Global Web Knowhow\n{File.ReadAllText(globalPath)}");

        // Site-level knowhow
        var sitePath = Path.Combine(webProfilesDir, SanitizeDomain(domain), "knowhow.md");
        if (File.Exists(sitePath))
            parts.Add($"# Site: {domain}\n{File.ReadAllText(sitePath)}");

        // Element-level knowhow
        if (cssSelector != null)
        {
            var hash = ComputeSelectorHash(cssSelector);
            var label = ExtractSelectorLabel(cssSelector);
            var elemPath = Path.Combine(webProfilesDir, SanitizeDomain(domain),
                "elements", $"{label}_{hash}", "knowhow.md");
            if (File.Exists(elemPath))
                parts.Add(File.ReadAllText(elemPath));
        }

        return parts.Count > 0 ? string.Join("\n\n---\n\n", parts) : null;
    }

    /// <summary>List all domains with knowhow.</summary>
    public static IReadOnlyList<string> ListDomains(string webProfilesDir)
    {
        if (!Directory.Exists(webProfilesDir)) return Array.Empty<string>();
        return Directory.GetDirectories(webProfilesDir)
            .Select(Path.GetFileName)
            .Where(n => n != null && n != "_global")
            .OrderBy(n => n)
            .ToList()!;
    }

    internal static string SanitizeDomain(string domain)
        => string.Join("_", domain.Split(Path.GetInvalidFileNameChars()))
           .Replace(":", "_").Replace("/", "_").Replace("\\", "_");

    internal static string ComputeSelectorHash(string selector)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(selector));
        return Convert.ToHexString(bytes)[..8].ToLowerInvariant();
    }

    internal static string ExtractSelectorLabel(string selector)
    {
        // ".ReactVirtualized__Grid" -> "ReactVirtualized__Grid"
        // "#btn-login" -> "btn-login"
        var clean = selector.TrimStart('.', '#', ' ');
        if (clean.Length > 30) clean = clean[..30];
        return string.Join("_", clean.Split(Path.GetInvalidFileNameChars()));
    }
}
