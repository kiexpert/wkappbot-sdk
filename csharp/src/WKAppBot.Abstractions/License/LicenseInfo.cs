using System;

namespace WKAppBot.Abstractions.License;

/// <summary>
/// On-disk license payload. The ECDSA signature in <see cref="Signature"/>
/// covers the canonical JSON of every other field (sorted keys, no whitespace).
///
/// Persisted at <c>%USERPROFILE%\.wkappbot\license.json</c>. Free-tier users
/// have no file -- the absence is treated as Free.
/// </summary>
public sealed record LicenseInfo
{
    /// <summary>Stable customer id (uuid recommended).</summary>
    public string UserId { get; init; } = "";

    /// <summary>Email address the license was issued to (display only).</summary>
    public string UserEmail { get; init; } = "";

    /// <summary>Subscription tier.</summary>
    public LicenseTier Tier { get; init; } = LicenseTier.Free;

    /// <summary>Hard expiry, UTC. After this instant the CLI refuses to run
    /// gated features. Always serialized as ISO-8601 with trailing "Z".</summary>
    public DateTime ExpiresAt { get; init; } = DateTime.UtcNow;

    /// <summary>Explicit feature grants. Allows per-license overrides on top
    /// of the default tier feature set.</summary>
    public LicenseFeature[] Features { get; init; } = Array.Empty<LicenseFeature>();

    /// <summary>base64url ECDSA P-256 signature over the canonical JSON of
    /// every other field. Excluded from the signed payload itself.</summary>
    public string Signature { get; init; } = "";
}
