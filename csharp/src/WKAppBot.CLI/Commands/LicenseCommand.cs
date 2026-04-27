using System;
using System.IO;
using System.Linq;
using WKAppBot.Abstractions.License;
using WKAppBot.Core.License;

namespace WKAppBot.CLI.Commands;

/// <summary>
/// <c>wkappbot license status | activate &lt;path&gt;</c>
/// </summary>
internal static class LicenseCommandImpl
{
    public static int Run(string[] args)
    {
        var sub = (args.Length > 0 ? args[0] : "status").ToLowerInvariant();
        return sub switch
        {
            "status" or "info" or "show" => Status(),
            "activate" or "install" => Activate(args.Skip(1).ToArray()),
            "path" => PrintPath(),
            "--help" or "-h" or "help" => PrintHelp(),
            _ => Unknown(sub),
        };
    }

    private static int Status()
    {
        var lic = LicenseManager.Current;
        var dev = LicenseManager.IsDev;
        Console.WriteLine($"Tier       : {lic.Tier}{(dev ? " (DEV BYPASS active)" : "")}");
        Console.WriteLine($"User       : {(string.IsNullOrEmpty(lic.UserEmail) ? "(none)" : lic.UserEmail)}");
        if (!string.IsNullOrEmpty(lic.UserId))
            Console.WriteLine($"UserId     : {lic.UserId}");
        if (lic.Tier != LicenseTier.Free)
        {
            Console.WriteLine($"Expires    : {lic.ExpiresAt:yyyy-MM-dd HH:mm} UTC");
            var remain = lic.ExpiresAt - DateTime.UtcNow;
            if (remain <= TimeSpan.Zero) Console.WriteLine("Remaining  : EXPIRED");
            else Console.WriteLine($"Remaining  : {(int)Math.Ceiling(remain.TotalDays)} day(s)");
        }
        Console.WriteLine($"License at : {LicenseManager.LicensePath}");
        Console.WriteLine($"File found : {File.Exists(LicenseManager.LicensePath)}");
        var feats = LicenseManager.FeatureSet(lic);
        Console.WriteLine($"Features   : {(feats.Count == 0 ? "(none -- Free tier)" : string.Join(", ", feats.OrderBy(f => f.ToString())))}");
        var err = LicenseManager.LoadError;
        if (!string.IsNullOrEmpty(err))
            Console.WriteLine($"LoadError  : {err}");
        var warn = LicenseManager.ExpiryWarning();
        if (warn != null) Console.WriteLine(warn);
        Console.WriteLine();
        Console.WriteLine($"Manage     : {LicenseManager.UpgradeUrl}");
        return 0;
    }

    private static int Activate(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: wkappbot license activate <path-to-license.json>");
            return 1;
        }
        var src = args[0];
        try
        {
            LicenseManager.Activate(src);
            var lic = LicenseManager.Current;
            Console.WriteLine($"[LICENSE] activated -- tier={lic.Tier} user={lic.UserEmail} expires={lic.ExpiresAt:yyyy-MM-dd}");
            Console.WriteLine($"[LICENSE] stored at {LicenseManager.LicensePath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LICENSE] activation failed: {ex.Message}");
            Console.Error.WriteLine($"[LICENSE] obtain a fresh license at {LicenseManager.UpgradeUrl}");
            return 1;
        }
    }

    private static int PrintPath()
    {
        Console.WriteLine(LicenseManager.LicensePath);
        return 0;
    }

    private static int PrintHelp()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  wkappbot license status                 -- show current tier / expiry / features");
        Console.WriteLine("  wkappbot license activate <file.json>   -- install license to %USERPROFILE%\\.wkappbot\\");
        Console.WriteLine("  wkappbot license path                   -- print on-disk license file path");
        Console.WriteLine();
        Console.WriteLine($"Manage your subscription: {LicenseManager.UpgradeUrl}");
        return 0;
    }

    private static int Unknown(string sub)
    {
        Console.Error.WriteLine($"Unknown subcommand: license {sub}");
        return PrintHelp() == 0 ? 1 : 1;
    }
}
