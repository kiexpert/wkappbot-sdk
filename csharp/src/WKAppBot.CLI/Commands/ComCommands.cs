using System.Text.Json;

namespace WKAppBot.CLI;

// partial class: generic COM command facade (v1: kiwoom adapter)
internal partial class Program
{
    sealed class ComSession
    {
        public string Profile { get; set; } = "kiwoom";
    }

    static readonly string[] ComProfiles = ["kiwoom"];

    static string ComSessionDir => Path.Combine(Environment.CurrentDirectory, ".wkcom");
    static string ComSessionPath => Path.Combine(ComSessionDir, "session.json");

    static int ComCommand(string[] args)
    {
        if (args.Length == 0)
            return ComUsage();

        var sub = args[0].ToLowerInvariant();
        var rest = args.Skip(1).ToArray();

        return sub switch
        {
            "ls" => ComLs(),
            "use" => ComUse(rest),
            "current" => ComCurrent(),
            "methods" => ComMethods(rest),
            "call" => ComCall(rest),
            _ => ComUsage()
        };
    }

    static int ComUsage()
    {
        Console.WriteLine(@"Usage: wkappbot com <subcommand>

Subcommands:
  ls                  List available COM profiles/adapters
  use <name>          Select profile for current folder (.wkcom/session.json)
  current             Show selected profile for current folder
  methods             Show callable methods for selected profile
  call <method> [p]   Invoke method via selected profile adapter

Examples:
  wkappbot com ls
  wkappbot com use kiwoom
  wkappbot com methods
  wkappbot com call GetConnectState
  wkappbot com call GetLoginInfo ACCNO
");
        return 1;
    }

    static int ComLs()
    {
        Console.WriteLine("Available COM profiles:");
        foreach (var p in ComProfiles)
            Console.WriteLine($"  - {p}");
        return 0;
    }

    static int ComUse(string[] rest)
    {
        if (rest.Length == 0)
            return Error("Usage: wkappbot com use <name>");

        var profile = rest[0].ToLowerInvariant();
        if (!ComProfiles.Contains(profile))
            return Error($"Unknown profile: {profile}");

        Directory.CreateDirectory(ComSessionDir);
        var session = new ComSession { Profile = profile };
        File.WriteAllText(ComSessionPath, JsonSerializer.Serialize(session, new JsonSerializerOptions { WriteIndented = true }));

        Console.WriteLine($"[COM] Selected profile: {profile}");
        Console.WriteLine($"[COM] Session file: {ComSessionPath}");
        return 0;
    }

    static int ComCurrent()
    {
        var session = LoadComSession();
        Console.WriteLine($"[COM] Current profile: {session.Profile}");
        Console.WriteLine($"[COM] Session file: {ComSessionPath}");
        return 0;
    }

    static int ComMethods(string[] rest)
    {
        var session = LoadComSession();
        if (session.Profile == "kiwoom")
        {
            Console.WriteLine("[COM] kiwoom methods (common):");
            Console.WriteLine("  - CommConnect");
            Console.WriteLine("  - GetConnectState");
            Console.WriteLine("  - GetLoginInfo <TAG>");
            Console.WriteLine("  - KOA_Functions <name> <param>");
            Console.WriteLine("  - SetInputValue <key> <val>");
            Console.WriteLine("  - CommRqData <rqName> <trCode> <prevNext> <screenNo>");
            Console.WriteLine("  - SetRealReg <screenNo> <codeList> <fidList> <opt>");
            return 0;
        }

        return Error($"Profile not supported yet: {session.Profile}");
    }

    static int ComCall(string[] rest)
    {
        if (rest.Length == 0)
            return Error("Usage: wkappbot com call <method> [param1] [param2] ...");

        var session = LoadComSession();
        var method = rest[0];
        var paramStrings = rest.Skip(1).ToArray();

        if (session.Profile == "kiwoom")
        {
            if (!EnsureProxy())
                return 1;

            var paramObjects = paramStrings.Select(p =>
            {
                if (int.TryParse(p, out var n)) return (object)n;
                return p;
            }).ToArray();

            Console.WriteLine($"[COM:{session.Profile}] Calling {method}({string.Join(", ", paramStrings)})...");
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var resp = SendPipeRequest(method, paramObjects);
            sw.Stop();

            if (resp.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[COM:{session.Profile}] FAIL: {resp.Error} ({sw.ElapsedMilliseconds}ms)");
                Console.ResetColor();
                return 1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[COM:{session.Profile}] OK: {FormatResult(resp.Result)} ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            return 0;
        }

        return Error($"Profile not supported yet: {session.Profile}");
    }

    static ComSession LoadComSession()
    {
        try
        {
            if (File.Exists(ComSessionPath))
            {
                var s = JsonSerializer.Deserialize<ComSession>(File.ReadAllText(ComSessionPath));
                if (s != null && !string.IsNullOrWhiteSpace(s.Profile))
                    return s;
            }
        }
        catch { }

        return new ComSession(); // default: kiwoom
    }
}
