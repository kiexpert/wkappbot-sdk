namespace WKAppBot.Abstractions.License;

/// <summary>
/// Subscription tier. Free tier needs no license file at all.
/// Standard / Pro require a signed license.json from %USERPROFILE%\.wkappbot\.
/// </summary>
public enum LicenseTier
{
    Free = 0,
    Standard = 1,
    Pro = 2,
}

/// <summary>
/// Discrete capability gates. A LicenseInfo carries an explicit Features
/// array so an issued license can override the tier defaults if needed
/// (e.g. early-access promo grants AndroidAdb on a Standard tier).
/// </summary>
public enum LicenseFeature
{
    AskAI = 0,
    Schedule = 1,
    AndroidAdb = 2,
    VisionApi = 3,
    UnlimitedScenarios = 4,
}
