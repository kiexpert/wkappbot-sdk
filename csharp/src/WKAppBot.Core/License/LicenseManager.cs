// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WKAppBot.Abstractions.License;

namespace WKAppBot.Core.License;

/// <summary>
/// Subscription license enforcement (offline-first).
///
/// <list type="bullet">
///   <item>Loads <c>%USERPROFILE%\.wkappbot\license.json</c> on first access.</item>
///   <item>Verifies ECDSA P-256 signature against the embedded public key.</item>
///   <item>Auto-bypasses validation when running from the dev repo (csharp/ sibling exists).</item>
///   <item>Free tier needs no license file -- absence => Free.</item>
/// </list>
/// </summary>
public static class LicenseManager
{
    public const string UpgradeUrl = "https://wkappbot.com/license";
    public const int ExpiryWarnDays = 7;

    // -- Embedded public key (P-256, SubjectPublicKeyInfo, base64) --
    // Pair these with the matching private key only when issuing licenses.
    // Re-keying requires bumping this constant + reissuing every license.
    private const string PublicKeySpkiBase64 =
        "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEvbJxBkioGjalBf6RSnE+nxyxtF8ZRjJzxO5heyrhDrD+yD1+0fl2ZH2TuQeXl/ocqoClsMUs1xybSRqCuFC1hw==";

    private static readonly object _lock = new();
    private static LicenseInfo? _current;
    private static bool _loaded;
    private static string? _loadError;

    /// <summary>
    /// Current license. Triggers a load on first access. Returns a synthetic
    /// Free license when no file exists, the file is invalid, or the signature
    /// fails to verify (with the failure reason captured in <see cref="LoadError"/>).
    /// </summary>
    public static LicenseInfo Current
    {
        get
        {
            EnsureLoaded();
            return _current ?? FreeLicense();
        }
    }

    /// <summary>Last load failure reason, or null on success / Free fallback.</summary>
    public static string? LoadError
    {
        get { EnsureLoaded(); return _loadError; }
    }

    /// <summary>True when running from the dev repo (sibling <c>csharp/</c> directory).
    /// All license checks are bypassed in dev mode so contributors aren't blocked
    /// by an expired or missing license while developing.</summary>
    public static bool IsDev
    {
        get
        {
            try
            {
                var baseDir = AppContext.BaseDirectory;
                // bin/Debug/net8.0-...   -> ../../../csharp
                // wkappbot-sdk/<bin>     -> ../csharp
                // Walk up a few levels and check for a sibling csharp/ folder
                // that has at least one *.csproj inside (avoid false positives).
                var probe = new DirectoryInfo(baseDir);
                for (int depth = 0; depth < 6 && probe != null; depth++)
                {
                    var sibling = Path.Combine(probe.FullName, "csharp");
                    if (Directory.Exists(sibling))
                    {
                        try
                        {
                            if (Directory.EnumerateFiles(sibling, "*.csproj", SearchOption.AllDirectories).Any())
                                return true;
                        }
                        catch { /* permission denied -- treat as not-dev */ }
                    }
                    probe = probe.Parent;
                }
                return false;
            }
            catch { return false; }
        }
    }

    /// <summary>Path to the on-disk license file (always returns a value, even
    /// when the file does not yet exist).</summary>
    public static string LicensePath
    {
        get
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ".wkappbot", "license.json");
        }
    }

    /// <summary>Force a (re)load from disk. Useful right after <c>license activate</c>.</summary>
    public static void Load()
    {
        lock (_lock)
        {
            _loaded = false;
            _current = null;
            _loadError = null;
            EnsureLoadedLocked();
        }
    }

    private static void EnsureLoaded()
    {
        if (_loaded) return;
        lock (_lock) { EnsureLoadedLocked(); }
    }

    private static void EnsureLoadedLocked()
    {
        if (_loaded) return;
        _loaded = true;
        try
        {
            var path = LicensePath;
            if (!File.Exists(path))
            {
                _current = FreeLicense();
                return;
            }
            var raw = File.ReadAllText(path, Encoding.UTF8);
            var info = JsonSerializer.Deserialize<LicenseInfo>(raw, JsonOpts.Read);
            if (info == null) { _loadError = "license.json is empty"; _current = FreeLicense(); return; }
            if (string.IsNullOrEmpty(info.Signature)) { _loadError = "license.json missing signature"; _current = FreeLicense(); return; }
            if (!VerifySignature(info)) { _loadError = "license.json signature invalid"; _current = FreeLicense(); return; }
            _current = info;
        }
        catch (Exception ex)
        {
            _loadError = ex.Message;
            _current = FreeLicense();
        }
    }

    /// <summary>
    /// Gate a feature at command entry. No-op when:
    /// (a) running from dev repo, (b) license is valid and grants the feature.
    ///
    /// Otherwise prints a friendly upgrade message to stderr and exits the process
    /// with code 2 (license refused). Throws are deliberately avoided here so
    /// the caller doesn't need a try/catch around every command dispatch.
    /// </summary>
    public static void Require(LicenseFeature feature)
    {
        if (IsDev) return;
        var lic = Current;
        // Hard expiry: gated features are blocked even on Standard/Pro after expiry.
        if (lic.Tier != LicenseTier.Free && lic.ExpiresAt < DateTime.UtcNow)
        {
            FailExit(
                $"License expired on {lic.ExpiresAt:yyyy-MM-dd} UTC.",
                "Renew at " + UpgradeUrl);
        }
        if (FeatureSet(lic).Contains(feature)) return;
        var needed = MinTierFor(feature);
        FailExit(
            $"Feature '{feature}' requires the {needed} tier (current: {lic.Tier}).",
            "Upgrade at " + UpgradeUrl);
    }

    /// <summary>Returns a warning string when the license expires within
    /// <see cref="ExpiryWarnDays"/>, otherwise null. Free tier never warns.</summary>
    public static string? ExpiryWarning()
    {
        if (IsDev) return null;
        var lic = Current;
        if (lic.Tier == LicenseTier.Free) return null;
        var remain = lic.ExpiresAt - DateTime.UtcNow;
        if (remain <= TimeSpan.Zero) return $"[LICENSE] EXPIRED on {lic.ExpiresAt:yyyy-MM-dd} UTC -- renew at {UpgradeUrl}";
        if (remain.TotalDays <= ExpiryWarnDays)
        {
            int days = Math.Max(1, (int)Math.Ceiling(remain.TotalDays));
            return $"[LICENSE] expires in {days} day(s) ({lic.ExpiresAt:yyyy-MM-dd} UTC) -- renew at {UpgradeUrl}";
        }
        return null;
    }

    /// <summary>Activate a license by copying it into the user profile location.
    /// Verifies the signature before writing -- bad files never overwrite a good one.</summary>
    public static void Activate(string sourcePath)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException("License file not found", sourcePath);
        var raw = File.ReadAllText(sourcePath, Encoding.UTF8);
        var info = JsonSerializer.Deserialize<LicenseInfo>(raw, JsonOpts.Read)
            ?? throw new InvalidDataException("License JSON is empty");
        if (!VerifySignature(info))
            throw new InvalidDataException("License signature invalid -- not activated");
        var dest = LicensePath;
        Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
        File.WriteAllText(dest, raw, new UTF8Encoding(false));
        Load();
    }

    /// <summary>Effective feature set for a license: tier defaults UNION explicit grants.</summary>
    public static IReadOnlySet<LicenseFeature> FeatureSet(LicenseInfo lic)
    {
        var s = new HashSet<LicenseFeature>(TierDefaults(lic.Tier));
        foreach (var f in lic.Features ?? Array.Empty<LicenseFeature>()) s.Add(f);
        return s;
    }

    /// <summary>Default features for a tier. Free has none of the gated features.</summary>
    public static IEnumerable<LicenseFeature> TierDefaults(LicenseTier tier) => tier switch
    {
        LicenseTier.Free => Array.Empty<LicenseFeature>(),
        LicenseTier.Standard => new[] { LicenseFeature.AskAI, LicenseFeature.Schedule },
        LicenseTier.Pro => new[]
        {
            LicenseFeature.AskAI, LicenseFeature.Schedule,
            LicenseFeature.AndroidAdb, LicenseFeature.VisionApi,
            LicenseFeature.UnlimitedScenarios,
        },
        _ => Array.Empty<LicenseFeature>(),
    };

    /// <summary>Minimum tier that includes the given feature by default
    /// (used for "upgrade to X" hints).</summary>
    public static LicenseTier MinTierFor(LicenseFeature f) => f switch
    {
        LicenseFeature.AskAI or LicenseFeature.Schedule => LicenseTier.Standard,
        _ => LicenseTier.Pro,
    };

    // -- Signing helpers ---------------------------------------------------

    /// <summary>Build the canonical JSON bytes that the signature covers.
    /// Sorted keys, no whitespace, ISO-8601 UTC timestamps. The Signature
    /// field itself is excluded.</summary>
    public static byte[] CanonicalPayload(LicenseInfo lic)
    {
        // Hand-roll canonical JSON so we don't depend on a particular
        // serializer's key-ordering behavior across versions.
        var sb = new StringBuilder();
        sb.Append('{');
        AppendKv(sb, "expiresAt", JsonString(lic.ExpiresAt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"))); sb.Append(',');
        sb.Append("\"features\":[");
        var feats = (lic.Features ?? Array.Empty<LicenseFeature>())
            .Select(f => f.ToString())
            .OrderBy(s => s, StringComparer.Ordinal)
            .ToArray();
        for (int i = 0; i < feats.Length; i++)
        {
            if (i > 0) sb.Append(',');
            sb.Append(JsonString(feats[i]));
        }
        sb.Append("],");
        AppendKv(sb, "tier", JsonString(lic.Tier.ToString())); sb.Append(',');
        AppendKv(sb, "userEmail", JsonString(lic.UserEmail ?? "")); sb.Append(',');
        AppendKv(sb, "userId", JsonString(lic.UserId ?? ""));
        sb.Append('}');
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private static void AppendKv(StringBuilder sb, string key, string jsonValue)
    {
        sb.Append(JsonString(key));
        sb.Append(':');
        sb.Append(jsonValue);
    }

    private static string JsonString(string s)
    {
        var sb = new StringBuilder(s.Length + 2);
        sb.Append('"');
        foreach (var c in s)
        {
            switch (c)
            {
                case '"': sb.Append("\\\""); break;
                case '\\': sb.Append("\\\\"); break;
                case '\b': sb.Append("\\b"); break;
                case '\f': sb.Append("\\f"); break;
                case '\n': sb.Append("\\n"); break;
                case '\r': sb.Append("\\r"); break;
                case '\t': sb.Append("\\t"); break;
                default:
                    if (c < 0x20) sb.Append($"\\u{(int)c:x4}");
                    else sb.Append(c);
                    break;
            }
        }
        sb.Append('"');
        return sb.ToString();
    }

    /// <summary>Verify the signature against the embedded public key.</summary>
    public static bool VerifySignature(LicenseInfo lic)
    {
        try
        {
            var sig = Base64Url.Decode(lic.Signature);
            using var ec = ECDsa.Create();
            ec.ImportSubjectPublicKeyInfo(Convert.FromBase64String(PublicKeySpkiBase64), out _);
            return ec.VerifyData(CanonicalPayload(lic), sig, HashAlgorithmName.SHA256);
        }
        catch { return false; }
    }

    /// <summary>Sign a license payload with a P-256 private key (PKCS#8, base64).
    /// Returns the LicenseInfo with its Signature field populated.
    /// Used by the offline license-generator tool.</summary>
    public static LicenseInfo Sign(LicenseInfo unsigned, string privateKeyPkcs8Base64)
    {
        using var ec = ECDsa.Create();
        ec.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKeyPkcs8Base64), out _);
        var sig = ec.SignData(CanonicalPayload(unsigned), HashAlgorithmName.SHA256);
        return unsigned with { Signature = Base64Url.Encode(sig) };
    }

    private static LicenseInfo FreeLicense() => new()
    {
        UserId = "",
        UserEmail = "",
        Tier = LicenseTier.Free,
        // Free has no expiry concept -- pin to MaxValue so any "is expired"
        // check on a Free license is always false.
        ExpiresAt = DateTime.MaxValue,
        Features = Array.Empty<LicenseFeature>(),
        Signature = "",
    };

    private static void FailExit(string headline, string hint)
    {
        try { Console.Error.WriteLine($"[LICENSE] {headline}"); } catch { }
        try { Console.Error.WriteLine($"[LICENSE] {hint}"); } catch { }
        Environment.Exit(2);
    }

    internal static class JsonOpts
    {
        public static readonly JsonSerializerOptions Read = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
        };
        public static readonly JsonSerializerOptions Write = new()
        {
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
        };
    }

    /// <summary>Minimal base64url codec (RFC 4648 §5, no padding).</summary>
    public static class Base64Url
    {
        public static string Encode(byte[] data)
        {
            var s = Convert.ToBase64String(data);
            return s.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        public static byte[] Decode(string s)
        {
            var n = s.Replace('-', '+').Replace('_', '/');
            switch (n.Length % 4) { case 2: n += "=="; break; case 3: n += "="; break; }
            return Convert.FromBase64String(n);
        }
    }
}
