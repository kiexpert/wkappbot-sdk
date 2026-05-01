using System.Net.Http.Headers;
using System.Text.Json;

namespace WKAppBot.Launcher;

static class LicenseStatus
{
    const string Repo = "kiexpert/wkappbot-sdk";

    internal static async Task<int> PrintAsync()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token))
        {
            Console.Error.WriteLine("[license] No GitHub token found. Set GH_TOKEN or log in with: gh auth login");
            return 1;
        }

        using var http = MakeClient(token);

        // Get current user
        var user = await GetCurrentUserAsync(http);
        if (user == null)
        {
            Console.Error.WriteLine("[license] Failed to get GitHub user identity.");
            return 1;
        }

        // Check collaborator permission
        var (perm, isPending) = await GetPermissionAsync(http, user);

        // Determine tier
        var tier = perm switch
        {
            "admin" => "Dev",
            "write" => "Sudo",
            "read"  => "CDP",
            _       => isPending ? "CDP (pending invite)" : "Free",
        };

        var cdpEnabled   = tier != "Free";
        var sudoEnabled  = perm is "write" or "admin";

        var (cdpExpiry, sudoExpiry) = await GetExpiryAsync(http, user);

        Console.WriteLine($"  User    : @{user}");
        Console.WriteLine($"  Repo    : {Repo}");
        Console.WriteLine($"  Tier    : {tier}");
        Console.WriteLine($"  CDP     : {(cdpEnabled  ? "✓ enabled" : "✗ not licensed")}{FormatExpiryInline(cdpExpiry)}");
        Console.WriteLine($"  Sudo    : {(sudoEnabled  ? "✓ enabled" : "✗ not licensed")}{FormatExpiryInline(sudoExpiry)}");
        if (isPending)
            Console.WriteLine("  Note    : Invitation pending — CDP active immediately, accept to confirm.");
        if (tier == "Free")
            Console.WriteLine("  Info    : See https://kiexpert.github.io/wkappbot-sdk/guide/pricing for upgrade.");
        return 0;
    }

    // ── helpers ────────────────────────────────────────────────────────────────

    static string? GetToken() =>
        Environment.GetEnvironmentVariable("GH_TOKEN")
        ?? Environment.GetEnvironmentVariable("GITHUB_TOKEN")
        ?? TryReadGhCliToken();

    static string? TryReadGhCliToken()
    {
        try
        {
            var hostsFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "GitHub CLI", "hosts.yml");
            if (!File.Exists(hostsFile)) return null;
            foreach (var line in File.ReadAllLines(hostsFile))
            {
                var t = line.Trim();
                if (t.StartsWith("oauth_token:"))
                    return t["oauth_token:".Length..].Trim();
            }
        }
        catch { }
        return null;
    }

    static HttpClient MakeClient(string token)
    {
        var c = new HttpClient();
        c.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("WKAppBot-Launcher", "1.0"));
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        c.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        return c;
    }

    static async Task<string?> GetCurrentUserAsync(HttpClient http)
    {
        try
        {
            var json = await http.GetStringAsync("https://api.github.com/user");
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("login").GetString();
        }
        catch { return null; }
    }

    static async Task<(DateTime? cdp, DateTime? sudo)> GetExpiryAsync(HttpClient http, string user)
    {
        try
        {
            var json = await http.GetStringAsync(
                $"https://api.github.com/repos/{Repo}/contents/.github/licenses/{user}.json");
            using var doc = JsonDocument.Parse(json);
            var encoded = doc.RootElement.GetProperty("content").GetString() ?? "";
            var content = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encoded.Replace("\n", "")));
            using var inner = JsonDocument.Parse(content);
            var root = inner.RootElement;

            // new per-tier schema: {"cdp": "...", "sudo": "..."}
            DateTime? cdp = null, sudo = null;
            if (root.TryGetProperty("cdp", out var cdpEl))
                cdp = DateTime.Parse(cdpEl.GetString()!, null, System.Globalization.DateTimeStyles.RoundtripKind);
            if (root.TryGetProperty("sudo", out var sudoEl))
                sudo = DateTime.Parse(sudoEl.GetString()!, null, System.Globalization.DateTimeStyles.RoundtripKind);
            // fallback: old flat schema {"expires": "..."} treated as cdp
            if (cdp == null && root.TryGetProperty("expires", out var expEl))
                cdp = DateTime.Parse(expEl.GetString()!, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return (cdp, sudo);
        }
        catch { }
        return (null, null);
    }

    static string FormatExpiryInline(DateTime? expiry)
    {
        if (!expiry.HasValue) return "";
        var remaining = expiry.Value - DateTime.UtcNow;
        return remaining.TotalSeconds > 0
            ? $"  (expires {expiry.Value:yyyy-MM-dd}, {FormatRemaining(remaining)} remaining)"
            : $"  (EXPIRED {expiry.Value:yyyy-MM-dd})";
    }

    static string FormatRemaining(TimeSpan t)
    {
        if (t.TotalDays >= 1) return $"{(int)t.TotalDays}일 {t.Hours}시간";
        if (t.TotalHours >= 1) return $"{(int)t.TotalHours}시간 {t.Minutes}분";
        return $"{(int)t.TotalMinutes}분";
    }

    static async Task<(string perm, bool isPending)> GetPermissionAsync(HttpClient http, string user)
    {
        try
        {
            var resp = await http.GetAsync($"https://api.github.com/repos/{Repo}/collaborators/{user}/permission");
            if (resp.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
                var p = doc.RootElement.GetProperty("permission").GetString() ?? "none";
                return (p, false);
            }
        }
        catch { }

        // Check pending invitation
        try
        {
            var json = await http.GetStringAsync($"https://api.github.com/repos/{Repo}/invitations");
            using var doc = JsonDocument.Parse(json);
            foreach (var inv in doc.RootElement.EnumerateArray())
            {
                var login = inv.GetProperty("invitee").GetProperty("login").GetString();
                if (string.Equals(login, user, StringComparison.OrdinalIgnoreCase))
                    return ("read", true);
            }
        }
        catch { }

        return ("none", false);
    }
}
