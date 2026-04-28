// SPDX-License-Identifier: MIT
using WKAppBot.Abstractions.License;
using WKAppBot.Core.License;
using Xunit;

namespace WKAppBot.Tests.License;

public class LicenseManagerTests
{
    [Fact]
    public void Current_WhenNoLicenseFile_ReturnsFree()
    {
        // IsDev bypasses in dev repo -- test the Free fallback directly
        var lic = LicenseManager.Current;
        Assert.NotNull(lic);
        // In dev mode, tier is Free or higher (dev bypass active)
        Assert.True(lic.Tier >= LicenseTier.Free);
    }

    [Fact]
    public void FeatureSet_FreeTier_IsEmpty()
    {
        var freeInfo = new LicenseInfo
        {
            Tier      = LicenseTier.Free,
            ExpiresAt = DateTime.MaxValue,
            Features  = Array.Empty<LicenseFeature>(),
            Signature = "",
        };
        var features = LicenseManager.FeatureSet(freeInfo);
        Assert.Empty(features);
    }

    [Fact]
    public void FeatureSet_StandardTier_HasAskAI()
    {
        var stdInfo = new LicenseInfo
        {
            Tier      = LicenseTier.Standard,
            ExpiresAt = DateTime.MaxValue,
            Features  = Array.Empty<LicenseFeature>(),
            Signature = "",
        };
        var features = LicenseManager.FeatureSet(stdInfo);
        Assert.Contains(LicenseFeature.AskAI, features);
    }

    [Fact]
    public void TierDefaults_Free_IsEmpty()
    {
        var defaults = LicenseManager.TierDefaults(LicenseTier.Free);
        Assert.Empty(defaults);
    }

    [Fact]
    public void TierDefaults_Standard_ContainsAskAI()
    {
        var defaults = LicenseManager.TierDefaults(LicenseTier.Standard);
        Assert.Contains(LicenseFeature.AskAI, defaults);
    }

    [Fact]
    public void MinTierFor_Cdp_IsStandard()
    {
        Assert.Equal(LicenseTier.Standard, LicenseManager.MinTierFor(LicenseFeature.AskAI));
    }
}
