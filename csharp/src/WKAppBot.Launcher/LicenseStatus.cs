using System.Net.Http.Headers;
using System.Text.Json;

namespace WKAppBot.Launcher;

static class LicenseStatus
{
    const string Owner = "kiexpert";
    const string RepoName = "wkappbot-sdk";
    const string Repo = $"{Owner}/{RepoName}";

    internal static async Task<int> PrintAsync()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token))
        {
            Console.Error.WriteLine("[license] GitHub 인증이 필요해요.");
            Console.Error.WriteLine("          → gh auth login");
            Console.Error.WriteLine("          또는 GH_TOKEN=<token> wkappbot license status");
            return 1;
        }

        using var http = MakeClient(token);

        // Call 1: GraphQL — viewer.login + viewerPermission in one shot
        var (user, perm) = await GetIdentityAndPermissionAsync(http);
        if (user == null)
        {
            Console.Error.WriteLine("[license] GitHub 인증 실패. 토큰을 확인해주세요.");
            return 1;
        }

        // Call 2: latest trusted commit → tier + expiry from commit message
        var (tierStr, cdpExpiry, sudoExpiry) = await GetLicenseAsync(http, user);

        var now         = DateTime.UtcNow;
        var sudoActive  = tierStr?.Contains("sudo") == true
                          && (!sudoExpiry.HasValue || sudoExpiry.Value > now);
        var cdpEnabled  = tierStr != null || perm is "read" or "write" or "admin";
        var sudoEnabled = perm is "write" or "admin";

        var tier = perm switch
        {
            "admin" => "Dev",
            "write" => tierStr?.Contains("sudo") == true ? "Sudo" : "CDP",
            "read"  => "CDP",
            _       => tierStr != null ? "CDP (invite pending)" : "Free",
        };

        Console.WriteLine($"  User    : @{user}");
        Console.WriteLine($"  Tier    : {tier}");
        Console.WriteLine($"  CDP     : {CdpIcon(cdpEnabled, cdpExpiry, sudoActive)}{CdpExpiry(cdpExpiry, sudoActive)}");
        Console.WriteLine($"  Sudo    : {Icon(sudoEnabled, sudoExpiry)}{ExpiryInline(sudoExpiry)}");
        if (tier == "CDP (invite pending)")
            Console.WriteLine("  Note    : 초대장 수락 후 기능이 활성화돼요 → github.com/notifications");
        if (tier == "Free")
            Console.WriteLine($"  Info    : https://github.com/{Repo}/blob/main/PRICING.md");
        return 0;
    }

    // ── API calls ──────────────────────────────────────────────────────────────

    static async Task<(string? user, string? perm)> GetIdentityAndPermissionAsync(HttpClient http)
    {
        const string query = """
            {
              viewer { login }
              repository(owner: "kiexpert", name: "wkappbot-sdk") {
                viewerPermission
              }
            }
            """;
        try
        {
            var body = JsonSerializer.Serialize(new { query });
            var resp = await http.PostAsync("https://api.github.com/graphql",
                new StringContent(body, System.Text.Encoding.UTF8, "application/json"));
            using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync());
            var data  = doc.RootElement.GetProperty("data");
            var login = data.GetProperty("viewer").GetProperty("login").GetString();
            var vp    = data.GetProperty("repository").TryGetProperty("viewerPermission", out var vpEl)
                        ? vpEl.GetString()?.ToLowerInvariant() : null;
            // GraphQL returns ADMIN/WRITE/READ/TRIAGE/MAINTAIN/null
            var perm  = vp switch { "admin" => "admin", "write" => "write", "read" => "read", _ => "none" };
            return (login, perm);
        }
        catch { return (null, null); }
    }

    static readonly HashSet<string> TrustedAuthors = ["kiexpert", "github-actions[bot]"];
    static readonly System.Text.RegularExpressions.Regex ExpRe  =
        new(@"(cdp|sudo)=(\S+)", System.Text.RegularExpressions.RegexOptions.Compiled);
    static readonly System.Text.RegularExpressions.Regex TierRe =
        new(@"tier=([\w+]+)",    System.Text.RegularExpressions.RegexOptions.Compiled);

    static async Task<(string? tier, DateTime? cdp, DateTime? sudo)> GetLicenseAsync(HttpClient http, string user)
    {
        try
        {
            var json = await http.GetStringAsync(
                $"https://api.github.com/repos/{Repo}/commits?path=.github/licenses/{user}&sha=main&per_page=1");
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.GetArrayLength() == 0) return (null, null, null);

            var c     = doc.RootElement[0];
            var login = c.TryGetProperty("author", out var a) && a.ValueKind != JsonValueKind.Null
                        ? (a.TryGetProperty("login", out var l) ? l.GetString() ?? "" : "") : "";
            var name  = c.GetProperty("commit").GetProperty("author").GetProperty("name").GetString() ?? "";
            if (!TrustedAuthors.Contains(login) && !TrustedAuthors.Contains(name))
                return (null, null, null);

            var msg     = c.GetProperty("commit").GetProperty("message").GetString() ?? "";
            var tierStr = TierRe.Match(msg) is { Success: true } tm ? tm.Groups[1].Value : null;
            DateTime? cdp = null, sudo = null;
            foreach (System.Text.RegularExpressions.Match m in ExpRe.Matches(msg))
            {
                var dt = DateTime.Parse(m.Groups[2].Value, null, System.Globalization.DateTimeStyles.RoundtripKind);
                if (m.Groups[1].Value == "cdp")  cdp  = dt;
                if (m.Groups[1].Value == "sudo") sudo = dt;
            }
            return (tierStr, cdp, sudo);
        }
        catch { return (null, null, null); }
    }

    // ── display helpers ────────────────────────────────────────────────────────

    static string Icon(bool on, DateTime? exp)
    {
        if (!on) return "✗ not licensed";
        if (exp.HasValue && exp.Value <= DateTime.UtcNow) return "✗ expired";
        return "✓ enabled";
    }

    static string CdpIcon(bool on, DateTime? cdpExp, bool sudoActive)
    {
        if (!on) return "✗ not licensed";
        if (sudoActive) return "✓ enabled";
        if (cdpExp.HasValue && cdpExp.Value <= DateTime.UtcNow) return "✗ expired";
        return "✓ enabled";
    }

    static string CdpExpiry(DateTime? cdpExp, bool sudoActive) =>
        sudoActive ? "  (included with Sudo)" : ExpiryInline(cdpExp);

    static string ExpiryInline(DateTime? exp)
    {
        if (!exp.HasValue) return "";
        var r = exp.Value - DateTime.UtcNow;
        return r.TotalSeconds > 0
            ? $"  (expires {exp.Value:yyyy-MM-dd}, {FmtRemaining(r)} remaining)"
            : $"  (EXPIRED {exp.Value:yyyy-MM-dd})";
    }

    static string FmtRemaining(TimeSpan t)
    {
        if (t.TotalDays >= 1)  return $"{(int)t.TotalDays}일 {t.Hours}시간";
        if (t.TotalHours >= 1) return $"{(int)t.TotalHours}시간 {t.Minutes}분";
        return $"{(int)t.TotalMinutes}분";
    }

    // ── auth ───────────────────────────────────────────────────────────────────

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
}
