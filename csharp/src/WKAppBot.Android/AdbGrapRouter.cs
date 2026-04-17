namespace WKAppBot.Android;

/// <summary>
/// Parses adb:// URI scheme and routes to Android automation.
/// Format: adb://device/package#scope1#scope2
/// Examples:
///   adb://Fold5/*heromts*#해외잔고
///   adb://SM-F946N/*heromts*#계좌#조건검색
///   adb://*heromts*#해외잔고          (auto-detect device)
///   adb://Fold5:outer/*heromts*       (outer display)
/// </summary>
public static class AdbGrapRouter
{
    private const string Scheme = "adb://";

    public static bool IsAdbGrap(string? grap)
        => grap != null && grap.StartsWith(Scheme, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Parse adb://device/package#scope into components.
    /// </summary>
    public static AdbGrapInfo Parse(string grap)
    {
        if (!IsAdbGrap(grap))
            return new AdbGrapInfo { Raw = grap };

        var rest = grap[Scheme.Length..]; // "Fold5/*heromts*#해외잔고"

        // Split hash scopes: same as Windows grap #scope1#scope2
        string? scopePath = null;
        var hashIdx = rest.IndexOf('#');
        if (hashIdx >= 0)
        {
            scopePath = rest[(hashIdx + 1)..];
            rest = rest[..hashIdx];
        }

        // Split device/package by first /
        string? device = null;
        string? package = null;
        var slashIdx = rest.IndexOf('/');
        if (slashIdx >= 0)
        {
            device = rest[..slashIdx];
            package = rest[(slashIdx + 1)..];
        }
        else
        {
            // No slash -- could be just device or just package
            // If it contains * or looks like a package, treat as package (auto-detect device)
            if (rest.Contains('.') || rest.Contains('*'))
                package = rest;
            else
                device = rest;
        }

        // Normalize empty strings to null
        if (string.IsNullOrEmpty(device)) device = null;
        if (string.IsNullOrEmpty(package)) package = null;
        if (string.IsNullOrEmpty(scopePath)) scopePath = null;

        // Parse scope segments (# separated, like UIA multi-level scope)
        var scopes = scopePath?.Split('#', StringSplitOptions.RemoveEmptyEntries);

        return new AdbGrapInfo
        {
            Raw = grap,
            IsAdb = true,
            Device = device,
            Package = package,
            ScopePath = scopePath,
            Scopes = scopes ?? [],
        };
    }
}

public class AdbGrapInfo
{
    public string Raw { get; init; } = "";
    public bool IsAdb { get; init; }
    public string? Device { get; init; }
    public string? Package { get; init; }
    public string? ScopePath { get; init; }
    public string[] Scopes { get; init; } = [];

    public override string ToString()
    {
        var parts = new List<string> { $"adb://" };
        if (Device != null) parts.Add(Device);
        parts.Add("/");
        if (Package != null) parts.Add(Package);
        if (ScopePath != null) parts.Add($"#{ScopePath}");
        return string.Join("", parts);
    }
}
