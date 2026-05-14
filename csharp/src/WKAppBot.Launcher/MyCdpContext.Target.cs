using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace WKAppBot.Launcher;

partial class Program
{
    static string? FindMyCdpTarget(string cmd, string[] forwardArgs, bool hasEvalJs)
    {
        if (forwardArgs.Length == 0) return null;

        if (cmd.Equals("a11y", StringComparison.OrdinalIgnoreCase))
            return FindA11yTarget(forwardArgs, hasEvalJs);

        if (cmd.Equals("web", StringComparison.OrdinalIgnoreCase)
            || cmd.Equals("cdp", StringComparison.OrdinalIgnoreCase))
            return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 1);

        if (cmd.Equals("ask", StringComparison.OrdinalIgnoreCase))
            return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 1);

        return FindFirstPositionalArgBeforeEvalJs(forwardArgs, startIndex: 0);
    }

    static string? FindA11yTarget(string[] args, bool hasEvalJs)
    {
        var positional = CollectPositionalArgs(args, startIndex: 1, stopAtEvalJs: false);
        if (hasEvalJs && positional.Count < 2)
            return null;
        return positional.Count >= 2 ? positional[1] : positional.FirstOrDefault();
    }

    static string? FindFirstPositionalArgBeforeEvalJs(string[] args, int startIndex)
    {
        var positional = CollectPositionalArgs(args, startIndex, stopAtEvalJs: true);
        return positional.FirstOrDefault();
    }

    static List<string> CollectPositionalArgs(string[] args, int startIndex, bool stopAtEvalJs)
    {
        var positional = new List<string>();
        for (var i = startIndex; i < args.Length; i++)
        {
            var arg = args[i];
            if (string.IsNullOrWhiteSpace(arg)) continue;
            if (stopAtEvalJs && arg.Equals("--eval-js", StringComparison.OrdinalIgnoreCase))
                break;
            if (arg.StartsWith("-", StringComparison.Ordinal)) continue;
            positional.Add(arg);
        }
        return positional;
    }

    static bool HasCdpField(string? grap)
        => !string.IsNullOrWhiteSpace(grap)
           && (grap.Contains("cdp:", StringComparison.OrdinalIgnoreCase)
               || grap.Contains("cdp=", StringComparison.OrdinalIgnoreCase));

    static int? GetExpectedCdpPort()
    {
        try
        {
            var root = WKAppBot.CLI.ProjectRoot.Find();
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(root.ToLowerInvariant()));
            var n = BinaryPrimitives.ReadUInt32BigEndian(bytes);
            return 9300 + (int)((n % 174) * 4);
        }
        catch
        {
            return null;
        }
    }

    static int? GetCdpPort(string? grap)
    {
        if (string.IsNullOrWhiteSpace(grap)) return null;
        var idx = grap.IndexOf("cdp:", StringComparison.OrdinalIgnoreCase);
        var sep = 4;
        if (idx < 0)
        {
            idx = grap.IndexOf("cdp=", StringComparison.OrdinalIgnoreCase);
            sep = 4;
        }
        if (idx < 0) return null;

        var start = idx + sep;
        var end = start;
        while (end < grap.Length && char.IsDigit(grap[end])) end++;
        if (end == start) return null;
        return int.TryParse(grap[start..end], out var port) ? port : null;
    }
}
