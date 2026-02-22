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
    static string ComExpRoot => Path.Combine(DataDir, "com_exp");

    sealed class ComCallRecord
    {
        public string Ts { get; set; } = DateTime.UtcNow.ToString("O");
        public string Profile { get; set; } = "";
        public string Method { get; set; } = "";
        public string[] Params { get; set; } = Array.Empty<string>();
        public bool Ok { get; set; }
        public string? Result { get; set; }
        public string? Error { get; set; }
        public long ElapsedMs { get; set; }
    }

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

        EnsureComProfileDocs(profile);

        Console.WriteLine($"[COM] Selected profile: {profile}");
        Console.WriteLine($"[COM] Session file: {ComSessionPath}");
        Console.WriteLine($"[COM] Exp folder: {Path.Combine(ComExpRoot, profile)}");
        return 0;
    }

    static int ComCurrent()
    {
        var session = LoadComSession();
        EnsureComProfileDocs(session.Profile);
        Console.WriteLine($"[COM] Current profile: {session.Profile}");
        Console.WriteLine($"[COM] Session file: {ComSessionPath}");
        Console.WriteLine($"[COM] Exp folder: {Path.Combine(ComExpRoot, session.Profile)}");
        return 0;
    }

    static int ComMethods(string[] rest)
    {
        var session = LoadComSession();
        if (session.Profile == "kiwoom")
        {
            EnsureComProfileDocs(session.Profile);
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

            EnsureComProfileDocs(session.Profile);

            Console.WriteLine($"[COM:{session.Profile}] Calling {method}({string.Join(", ", paramStrings)})...");
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var resp = SendPipeRequest(method, paramObjects);
            sw.Stop();

            var ok = resp.Error == null;
            var resultText = ok ? FormatResult(resp.Result) : null;
            var rec = new ComCallRecord
            {
                Profile = session.Profile,
                Method = method,
                Params = paramStrings,
                Ok = ok,
                Result = resultText,
                Error = resp.Error,
                ElapsedMs = sw.ElapsedMilliseconds
            };
            var callLogPath = AppendComCallRecord(session.Profile, rec);

            if (!ok)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[COM:{session.Profile}] FAIL: {resp.Error} ({sw.ElapsedMilliseconds}ms)");
                Console.ResetColor();
                Console.WriteLine($"[COM:{session.Profile}] Saved call record: {callLogPath}");
                return 1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[COM:{session.Profile}] OK: {resultText} ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            Console.WriteLine($"[COM:{session.Profile}] Saved call record: {callLogPath}");
            return 0;
        }

        return Error($"Profile not supported yet: {session.Profile}");
    }

    static void EnsureComProfileDocs(string profile)
    {
        try
        {
            var dir = Path.Combine(ComExpRoot, profile);
            Directory.CreateDirectory(dir);

            var interfacePath = Path.Combine(dir, "interface.json");
            if (!File.Exists(interfacePath))
            {
                object doc = profile switch
                {
                    "kiwoom" => new
                    {
                        profile,
                        progId = "KHOPENAPI.KHOpenAPICtrl.1",
                        pipe = "wkappbot_kiwoom",
                        source = "C:/OpenAPI/khopenapi_typelib.md",
                        notes = "Generated baseline for COM session usage"
                    },
                    _ => new { profile }
                };
                File.WriteAllText(interfacePath, JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
            }

            var knowhowPath = Path.Combine(dir, "knowhow.md");
            if (!File.Exists(knowhowPath))
            {
                File.WriteAllText(knowhowPath,
$"# {profile} knowhow\n\n- Keep interface/version notes here.\n- Updated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
            }

            var methodsPath = Path.Combine(dir, "methods.json");
            if (!File.Exists(methodsPath) && profile == "kiwoom")
            {
                var methods = new[]
                {
                    "CommConnect","GetConnectState","GetLoginInfo","KOA_Functions",
                    "SetInputValue","CommRqData","SetRealReg"
                };
                File.WriteAllText(methodsPath, JsonSerializer.Serialize(methods, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
        catch { }
    }

    static string AppendComCallRecord(string profile, ComCallRecord rec)
    {
        var dir = Path.Combine(ComExpRoot, profile);
        Directory.CreateDirectory(dir);

        var path = Path.Combine(dir, $"calls-{DateTime.Now:yyyyMMdd}.jsonl");
        var line = JsonSerializer.Serialize(rec);
        File.AppendAllText(path, line + Environment.NewLine);
        return path;
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
