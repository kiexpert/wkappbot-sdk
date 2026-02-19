using WKAppBot.WebBot;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: wkappbot knowhow <subcommand> — per-control/web knowhow management
internal partial class Program
{
    static string WebProfilesDir => Path.Combine(DataDir, "web_profiles");

    static int KnowhowCommand(string[] args)
    {
        if (args.Length == 0)
            return KnowhowUsage();

        var sub = args[0].ToLowerInvariant();
        return sub switch
        {
            "write" => KnowhowWriteCommand(args.Skip(1).ToArray()),
            "read" => KnowhowReadCommand(args.Skip(1).ToArray()),
            "web" => KnowhowWebWriteCommand(args.Skip(1).ToArray()),
            "web-read" => KnowhowWebReadCommand(args.Skip(1).ToArray()),
            "web-list" => KnowhowWebListCommand(),
            "--help" or "-h" or "help" => KnowhowUsage(),
            _ => Error($"Unknown knowhow sub-command: {sub}")
        };
    }

    /// <summary>
    /// wkappbot knowhow write &lt;form-id&gt; "lesson" [--cid N] [--category "..."]
    /// </summary>
    static int KnowhowWriteCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot knowhow write <form-id> \"lesson\" [--cid N] [--category \"...\"]");
            return 1;
        }

        var formId = args[0];
        string? lesson = null;
        int? cid = null;
        string category = "Manual Note";

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--cid" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out int c)) cid = c;
            }
            else if (args[i] == "--category" && i + 1 < args.Length)
            {
                category = args[++i];
            }
            else if (lesson == null)
            {
                lesson = args[i];
            }
        }

        if (string.IsNullOrEmpty(lesson))
        {
            Console.WriteLine("Error: lesson text is required");
            return 1;
        }

        // Find profile directory
        var profileDir = FindProfileExpDir();
        if (profileDir == null)
        {
            // No profile — use a default location
            profileDir = Path.Combine(DataDir, "profiles", "_default_exp");
            Directory.CreateDirectory(profileDir);
        }

        var expDb = new ExperienceDb(profileDir);

        bool ok;
        if (cid.HasValue)
        {
            ok = expDb.AppendKnowhow(formId, cid.Value, category, lesson);
            Console.WriteLine(ok
                ? $"[KNOWHOW] Appended to form={formId} cid={cid}: {category}"
                : "[KNOWHOW] Failed to write knowhow");
        }
        else
        {
            ok = expDb.AppendFormKnowhow(formId, category, lesson);
            Console.WriteLine(ok
                ? $"[KNOWHOW] Appended to form={formId} (form-level): {category}"
                : "[KNOWHOW] Failed to write knowhow");
        }

        return ok ? 0 : 1;
    }

    /// <summary>
    /// wkappbot knowhow read &lt;form-id&gt; [--cid N]
    /// </summary>
    static int KnowhowReadCommand(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: wkappbot knowhow read <form-id> [--cid N]");
            return 1;
        }

        var formId = args[0];
        int? cid = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--cid" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out int c)) cid = c;
            }
        }

        // Find all profile directories and search
        var profilesDir = Path.Combine(DataDir, "profiles");
        if (!Directory.Exists(profilesDir))
        {
            Console.WriteLine("[KNOWHOW] No profiles directory found");
            return 1;
        }

        foreach (var expDir in Directory.GetDirectories(profilesDir, "*_exp"))
        {
            var expDb = new ExperienceDb(expDir);
            string? knowhow;

            if (cid.HasValue)
                knowhow = expDb.ReadKnowhow(formId, cid.Value);
            else
                knowhow = expDb.ReadFormKnowhow(formId);

            if (knowhow != null)
            {
                var profileName = Path.GetFileName(expDir).Replace("_exp", "");
                Console.WriteLine($"[KNOWHOW] Profile: {profileName}");
                Console.WriteLine(knowhow);
                return 0;
            }
        }

        Console.WriteLine($"[KNOWHOW] No knowhow found for form={formId}" +
            (cid.HasValue ? $" cid={cid}" : ""));
        return 0;
    }

    /// <summary>
    /// wkappbot knowhow web &lt;domain&gt; "lesson" [--selector "..."] [--category "..."]
    /// </summary>
    static int KnowhowWebWriteCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot knowhow web <domain> \"lesson\" [--selector \"...\"] [--category \"...\"]");
            return 1;
        }

        var domain = args[0];
        string? lesson = null;
        string? selector = null;
        string category = "Manual Note";

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--selector" && i + 1 < args.Length)
                selector = args[++i];
            else if (args[i] == "--category" && i + 1 < args.Length)
                category = args[++i];
            else if (lesson == null)
                lesson = args[i];
        }

        if (string.IsNullOrEmpty(lesson))
        {
            Console.WriteLine("Error: lesson text is required");
            return 1;
        }

        bool ok;
        if (selector != null)
        {
            ok = WebKnowhow.AppendElementKnowhow(WebProfilesDir, domain, selector, category, lesson);
            Console.WriteLine(ok
                ? $"[KNOWHOW] Web: {domain} selector={selector}: {category}"
                : "[KNOWHOW] Failed to write web knowhow");
        }
        else
        {
            ok = WebKnowhow.AppendSiteKnowhow(WebProfilesDir, domain, category, lesson);
            Console.WriteLine(ok
                ? $"[KNOWHOW] Web: {domain} (site-level): {category}"
                : "[KNOWHOW] Failed to write web knowhow");
        }

        return ok ? 0 : 1;
    }

    /// <summary>
    /// wkappbot knowhow web-read &lt;domain&gt; [--selector "..."]
    /// </summary>
    static int KnowhowWebReadCommand(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: wkappbot knowhow web-read <domain> [--selector \"...\"]");
            return 1;
        }

        var domain = args[0];
        string? selector = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--selector" && i + 1 < args.Length)
                selector = args[++i];
        }

        var knowhow = WebKnowhow.ReadKnowhow(WebProfilesDir, domain, selector);
        if (knowhow != null)
            Console.WriteLine(knowhow);
        else
            Console.WriteLine($"[KNOWHOW] No web knowhow found for {domain}" +
                (selector != null ? $" selector={selector}" : ""));

        return 0;
    }

    /// <summary>
    /// wkappbot knowhow web-list — list domains with knowhow
    /// </summary>
    static int KnowhowWebListCommand()
    {
        var domains = WebKnowhow.ListDomains(WebProfilesDir);
        if (domains.Count == 0)
        {
            Console.WriteLine("[KNOWHOW] No web profiles found");

            // Check for global knowhow
            var globalPath = Path.Combine(WebProfilesDir, "_global", "knowhow.md");
            if (File.Exists(globalPath))
                Console.WriteLine("  (global knowhow exists)");

            return 0;
        }

        Console.WriteLine($"[KNOWHOW] {domains.Count} domain(s) with web knowhow:");
        foreach (var d in domains)
            Console.WriteLine($"  {d}");

        return 0;
    }

    /// <summary>
    /// Find the first matching profile _exp directory.
    /// Returns null if no profiles exist.
    /// </summary>
    static string? FindProfileExpDir()
    {
        var profilesDir = Path.Combine(DataDir, "profiles");
        if (!Directory.Exists(profilesDir)) return null;

        var expDirs = Directory.GetDirectories(profilesDir, "*_exp");
        return expDirs.Length > 0 ? expDirs[0] : null;
    }

    static int KnowhowUsage()
    {
        Console.WriteLine("Usage: wkappbot knowhow <command>");
        Console.WriteLine();
        Console.WriteLine("Win32 control knowhow:");
        Console.WriteLine("  write <form-id> \"lesson\" [--cid N] [--category \"...\"]");
        Console.WriteLine("  read  <form-id> [--cid N]");
        Console.WriteLine();
        Console.WriteLine("Web control knowhow:");
        Console.WriteLine("  web      <domain> \"lesson\" [--selector \"...\"] [--category \"...\"]");
        Console.WriteLine("  web-read <domain> [--selector \"...\"]");
        Console.WriteLine("  web-list");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot knowhow write 1301 \"BM_CLICK fails on this button\" --cid 3785");
        Console.WriteLine("  wkappbot knowhow web api.slack.com \"ReactVirtualized needs scrollTop\"");
        Console.WriteLine("  wkappbot knowhow read 1301 --cid 3785");
        Console.WriteLine("  wkappbot knowhow web-read api.slack.com");
        return 0;
    }
}
