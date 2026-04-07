using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int KroTrialSpecialCommand(string command, string[] args)
    {
        var m = Regex.Match(command ?? string.Empty, @"^kro-trial-(\d{8})$", RegexOptions.IgnoreCase);
        if (!m.Success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[KRO-TRIAL] Invalid command format.");
            Console.ResetColor();
            Console.WriteLine("Usage: wkappbot kro-trial-YYYYMMDD [window-title form-id text --cid N --enter --method N]");
            Console.WriteLine("Example: wkappbot kro-trial-20260225");
            Console.WriteLine("Example: wkappbot kro-trial-20260225 \"투혼\" 1101 \"005930\" --cid 3780 --enter");
            return 1;
        }

        var yyyymmdd = m.Groups[1].Value;
        var baseDir = Directory.GetCurrentDirectory();
        var probeDir = FindInputProbeDir(baseDir) ?? Path.Combine(baseDir, "tools", "InputProbe");
        var bakPath = Path.Combine(probeDir, $"Program.success.{yyyymmdd}.cs.bak");

        // historical breadcrumb only (non-blocking)
        if (File.Exists(bakPath))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[KRO-TRIAL] history found: {bakPath}");
            Console.ResetColor();
        }

        // Real AppBot command path (not InputProbe)
        // default target: 투혼 1101 005930 --cid 3780 --enter
        string[] inputArgs;
        if (args.Length >= 3)
        {
            inputArgs = args;
        }
        else
        {
            inputArgs = new[] { "투혼", "1101", "005930", "--cid", "3780", "--enter" };
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[KRO-TRIAL] using default real input scenario: 투혼 1101 005930 --cid 3780 --enter");
            Console.ResetColor();
        }

        Console.Error.WriteLine($"[KRO-TRIAL] delegating to real input command (date={yyyymmdd})");
        return InputCommand(inputArgs);
    }

    static string? FindInputProbeDir(string startDir)
    {
        try
        {
            var di = new DirectoryInfo(startDir);
            while (di is not null)
            {
                var candidate = Path.Combine(di.FullName, "tools", "InputProbe");
                if (Directory.Exists(candidate))
                    return candidate;
                di = di.Parent;
            }
        }
        catch { }

        return null;
    }
}
