// KiwoomCommands.cs -- CLI commands for Kiwoom OpenAPI+ proxy bot.
// 64-bit CLI communicates with 32-bit KiwoomProxy via Named Pipe (JSON-RPC).
// Experience DB (method/event/TR stats) is accumulated in kiwoom_exp/ folder.

using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Constants --

    const string KiwoomPipeName = "wkappbot_kiwoom";
    static string KiwoomProxyDir => Path.Combine(DataDir, "kiwoom_proxy");
    static string KiwoomExpDir => Path.Combine(DataDir, "kiwoom_exp");
    static string KiwoomProxyExe => Path.Combine(KiwoomProxyDir, "WKAppBot.KiwoomProxy.exe");
    static string KiwoomProxyPidFile => Path.Combine(DataDir, "kiwoom_proxy.pid");

    // -- Dispatcher --

    static int KiwoomCommand(string[] args)
    {
        if (args.Length == 0)
            return KiwoomUsage();

        var sub = args[0].ToLowerInvariant();
        var rest = args.Skip(1).ToArray();

        return sub switch
        {
            "start" => KiwoomStartCommand(rest),
            "stop" => KiwoomStopCommand(),
            "status" => KiwoomStatusCommand(),
            "login" => KiwoomLoginCommand(rest),
            "bootstrap" => KiwoomBootstrapCommand(rest),
            "call" => KiwoomCallCommand(rest),
            "query" => KiwoomQueryCommand(rest),
            "realtime" => KiwoomRealtimeCommand(rest),
            "knowhow" => KiwoomKnowhowCommand(rest),
            "exp" => KiwoomExpCommand(rest),
            _ => KiwoomUsage()
        };
    }

    static int KiwoomUsage()
    {
        Console.WriteLine(@"Usage: wkappbot kiwoom <subcommand>

Subcommands:
  start              Start KiwoomProxy (32-bit COM host)
  stop               Stop KiwoomProxy
  status             Show proxy + connection status
  login              CommConnect (auto-starts proxy if needed)
  bootstrap          Start proxy -> login wait -> print account info
                     Options: --timeout N(sec, default 120), --show-account-window, --setup
  call <method> [p]  Invoke any COM method with params
  query <tr> --input key=val [--screen N]
                     SetInputValue + CommRqData
  realtime <codes> --fids N,N [--screen N]
                     SetRealReg for real-time data
  knowhow ""lesson""   Record COM knowhow manually
  exp [method|event] Show accumulated experience DB");
        return 1;
    }

    // -- Start / Stop / Status --

    static int KiwoomStartCommand(string[] rest)
    {
        // Check if already running
        var existingPid = GetKiwoomProxyPid();
        if (existingPid > 0)
        {
            try
            {
                var proc = Process.GetProcessById(existingPid);
                Console.Error.WriteLine($"[KIWOOM] Proxy already running (PID={existingPid}, name={proc.ProcessName})");
                return 0;
            }
            catch { /* process gone, stale PID file */ }
        }

        // Check proxy EXE exists
        if (!File.Exists(KiwoomProxyExe))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[KIWOOM] Proxy EXE not found: {KiwoomProxyExe}");
            Console.ResetColor();
            Console.WriteLine("[KIWOOM] Build with: dotnet publish WKAppBot.KiwoomProxy.csproj -c Release -r win-x86 --self-contained");
            return 1;
        }

        // Launch proxy process (hidden console)
        Console.Error.WriteLine($"[KIWOOM] Starting proxy: {KiwoomProxyExe}");
        var psi = new ProcessStartInfo
        {
            FileName = KiwoomProxyExe,
            UseShellExecute = false,
            CreateNoWindow = false, // Needs WinForms message pump, must have window
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        var process = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "KIWOOM");
        if (process == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[KIWOOM] Failed to start proxy process");
            Console.ResetColor();
            return 1;
        }

        // Save PID for later stop/status
        Directory.CreateDirectory(Path.GetDirectoryName(KiwoomProxyPidFile)!);
        File.WriteAllText(KiwoomProxyPidFile, process.Id.ToString());

        // Forward output on background threads (non-blocking)
        _ = Task.Run(() => ForwardOutput(process.StandardOutput, "[KIWOOM-PROXY]"));
        _ = Task.Run(() => ForwardOutput(process.StandardError, "[KIWOOM-PROXY-ERR]"));

        // Wait briefly for pipe to become available
        Console.Error.WriteLine($"[KIWOOM] Proxy started (PID={process.Id}), waiting for pipe...");
        for (int i = 0; i < 30; i++) // up to 3 seconds
        {
            Thread.Sleep(100);
            if (IsPipeAvailable())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[KIWOOM] Proxy ready -- pipe connected");
                Console.ResetColor();
                return 0;
            }
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[KIWOOM] Proxy started but pipe not yet available (may need more time for COM init)");
        Console.ResetColor();
        return 0;
    }

    static int KiwoomStopCommand()
    {
        var pid = GetKiwoomProxyPid();
        if (pid <= 0)
        {
            Console.WriteLine("[KIWOOM] No proxy PID file found");
            return 1;
        }

        try
        {
            var proc = Process.GetProcessById(pid);
            Console.Error.WriteLine($"[KIWOOM] Stopping proxy (PID={pid})...");
            proc.Kill();
            proc.WaitForExit(5000);
            Console.WriteLine("[KIWOOM] Proxy stopped");
        }
        catch (ArgumentException)
        {
            Console.Error.WriteLine($"[KIWOOM] Process {pid} not found (already stopped?)");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[KIWOOM] Stop error: {ex.Message}");
        }

        // Clean up PID file
        try { File.Delete(KiwoomProxyPidFile); } catch { }
        return 0;
    }

    static int KiwoomStatusCommand()
    {
        Console.WriteLine("--─ Kiwoom Proxy Status --─");

        // 1. Check PID file
        var pid = GetKiwoomProxyPid();
        bool processAlive = false;
        if (pid > 0)
        {
            try
            {
                var proc = Process.GetProcessById(pid);
                processAlive = !proc.HasExited;
                Console.WriteLine($"  Process: PID={pid} ({proc.ProcessName}) -- {(processAlive ? "ALIVE" : "EXITED")}");
            }
            catch
            {
                Console.WriteLine($"  Process: PID={pid} -- NOT FOUND (stale)");
            }
        }
        else
        {
            Console.WriteLine("  Process: not started");
        }

        // 2. Check pipe
        bool pipeReady = IsPipeAvailable();
        Console.WriteLine($"  Pipe: {(pipeReady ? "AVAILABLE" : "not available")} (wkappbot_kiwoom)");

        // 3. Try GetStatus via pipe
        if (pipeReady)
        {
            try
            {
                var resp = SendPipeRequest("GetStatus");
                if (resp.Error != null)
                    Console.WriteLine($"  COM: ERROR -- {resp.Error}");
                else
                    Console.WriteLine($"  COM: {JsonSerializer.Serialize(resp.Result)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  COM: pipe call failed -- {ex.Message}");
            }
        }

        // 4. Experience DB stats
        ShowExpDbSummary();

        return 0;
    }

    // -- Login --

    static int KiwoomLoginCommand(string[] rest)
    {
        // Auto-start proxy if needed
        if (!IsPipeAvailable())
        {
            Console.WriteLine("[KIWOOM] Proxy not running, starting...");
            var startResult = KiwoomStartCommand(Array.Empty<string>());
            if (startResult != 0) return startResult;

            // Extra wait for COM init
            if (!IsPipeAvailable())
            {
                Console.WriteLine("[KIWOOM] Waiting for proxy to be ready...");
                for (int i = 0; i < 50; i++) // up to 5 seconds
                {
                    Thread.Sleep(100);
                    if (IsPipeAvailable()) break;
                }
            }
        }

        if (!IsPipeAvailable())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[KIWOOM] Proxy pipe not available after start");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine("[KIWOOM] Calling CommConnect()...");
        var sw = Stopwatch.StartNew();
        var resp = SendPipeRequest("CommConnect");
        sw.Stop();

        if (resp.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[KIWOOM] CommConnect failed: {resp.Error}");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KIWOOM] CommConnect() -> {resp.Result} ({sw.ElapsedMilliseconds}ms)");
        Console.ResetColor();
        Console.WriteLine("[KIWOOM] Waiting for OnEventConnect event... (login dialog will appear)");
        Console.WriteLine("[KIWOOM] Use 'wkappbot kiwoom status' to check connection state after login");
        return 0;
    }

    static int KiwoomBootstrapCommand(string[] rest)
    {
        int timeoutSec = 120;
        bool setupMode = rest.Contains("--setup");
        bool showAccountWindow = rest.Contains("--show-account-window") || setupMode;

        for (int i = 0; i < rest.Length; i++)
        {
            if (rest[i] == "--timeout" && i + 1 < rest.Length)
            {
                if (int.TryParse(rest[++i], out var t) && t > 0)
                    timeoutSec = t;
            }
        }

        Console.Error.WriteLine($"[KIWOOM] Bootstrap start (timeout={timeoutSec}s)...");
        if (setupMode)
            PrintKiwoomLoginSetupGuide();

        // State machine: EnsureProxy -> Connecting -> WaitingConnect -> Connected/Failed
        if (!EnsureProxy())
            return 1;

        var stateResp = SendPipeRequest("GetConnectState");
        var connected = ParseConnectState(stateResp.Result) == 1;
        if (connected)
            Console.WriteLine("[KIWOOM][STATE] Connected (already)");

        var deadline = DateTime.UtcNow.AddSeconds(timeoutSec);
        var attempts = 0;
        string? lastError = null;

        while (!connected && attempts < 3 && DateTime.UtcNow < deadline)
        {
            attempts++;
            Console.Error.WriteLine($"[KIWOOM][STATE] Connecting attempt {attempts}/3...");

            var loginResp = SendPipeRequest("CommConnect");
            if (loginResp.Error != null)
            {
                lastError = loginResp.Error;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine($"[KIWOOM] CommConnect attempt {attempts} failed: {loginResp.Error}");
                Console.ResetColor();

                // Backoff before retry (1s, 2s, 4s)
                var backoffSec = (int)Math.Pow(2, attempts - 1);
                Thread.Sleep(TimeSpan.FromSeconds(backoffSec));
                continue;
            }

            Console.WriteLine("[KIWOOM][STATE] Waiting for connection event...");
            while (DateTime.UtcNow < deadline)
            {
                Thread.Sleep(1000);
                stateResp = SendPipeRequest("GetConnectState");
                if (stateResp.Error != null)
                {
                    lastError = stateResp.Error;
                    continue;
                }

                if (ParseConnectState(stateResp.Result) == 1)
                {
                    connected = true;
                    break;
                }
            }

            if (!connected)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine($"[KIWOOM] Still disconnected after attempt {attempts}");
                Console.ResetColor();
            }
        }

        if (!connected)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[KIWOOM][STATE] Failed (disconnected)");
            Console.ResetColor();
            if (!string.IsNullOrWhiteSpace(lastError))
                Console.Error.WriteLine($"[KIWOOM] Last error: {lastError}");
            Console.WriteLine("[KIWOOM] Hint: check login window/update popup and retry.");
            PrintKiwoomLoginSetupGuide();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[KIWOOM][STATE] Connected");
        Console.ResetColor();

        // Print account list
        var accResp = SendPipeRequest("GetLoginInfo", "ACCNO");
        if (accResp.Error != null)
            Console.Error.WriteLine($"[KIWOOM] GetLoginInfo(ACCNO) failed: {accResp.Error}");
        else
            Console.Error.WriteLine($"[KIWOOM] ACCNO: {FormatResult(accResp.Result)}");

        var userResp = SendPipeRequest("GetLoginInfo", "USER_ID");
        if (userResp.Error == null)
            Console.Error.WriteLine($"[KIWOOM] USER_ID: {FormatResult(userResp.Result)}");

        var cntResp = SendPipeRequest("GetLoginInfo", "ACCOUNT_CNT");
        if (cntResp.Error == null)
            Console.Error.WriteLine($"[KIWOOM] ACCOUNT_CNT: {FormatResult(cntResp.Result)}");

        if (showAccountWindow)
        {
            var showResp = SendPipeRequest("KOA_Functions", "ShowAccountWindow", "");
            if (showResp.Error != null)
            {
                Console.Error.WriteLine($"[KIWOOM] ShowAccountWindow failed: {showResp.Error}");
                Console.WriteLine("[KIWOOM] Hint: if update/login popup is open, finish that first then retry --setup.");
            }
            else
            {
                Console.WriteLine("[KIWOOM] ShowAccountWindow requested (set account password + AUTO login if needed)");
            }
        }

        if (setupMode)
        {
            Console.WriteLine("[KIWOOM] Setup checklist done?");
            Console.WriteLine("  1) Logged in successfully");
            Console.WriteLine("  2) Opened account password window (ShowAccountWindow)");
            Console.WriteLine("  3) Saved account password and optional AUTO login");
        }

        return 0;
    }

    static void PrintKiwoomLoginSetupGuide()
    {
        Console.WriteLine("[KIWOOM][SETUP] Login setup guide:");
        Console.WriteLine("  1) OpenAPI login window appears after CommConnect()");
        Console.WriteLine("  2) Complete version/update popup first if shown");
        Console.WriteLine("  3) Login with account/password + cert password");
        Console.WriteLine("  4) Open account password window via KOA_Functions(ShowAccountWindow)");
        Console.WriteLine("  5) Save account password (and AUTO login if desired)");
        Console.WriteLine("  6) Retry: wkappbot kiwoom bootstrap --setup");
    }

    static int ParseConnectState(object? result)
    {
        if (result == null) return 0;

        if (result is JsonElement je)
        {
            if (je.ValueKind == JsonValueKind.Number && je.TryGetInt32(out var n))
                return n;
            if (je.ValueKind == JsonValueKind.String && int.TryParse(je.GetString(), out n))
                return n;
        }

        return int.TryParse(result.ToString(), out var parsed) ? parsed : 0;
    }

    // -- Generic COM Method Call --

    static int KiwoomCallCommand(string[] rest)
    {
        if (rest.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot kiwoom call <method> [param1] [param2] ...");
            Console.WriteLine("Example: wkappbot kiwoom call GetMasterCodeName 005930");
            Console.WriteLine("Example: wkappbot kiwoom call GetConnectState");
            return 1;
        }

        var method = rest[0];
        var paramStrings = rest.Skip(1).ToArray();

        // Convert params: try int, then leave as string
        var paramObjects = paramStrings.Select(p =>
        {
            if (int.TryParse(p, out var i)) return (object)i;
            return p;
        }).ToArray();

        if (!EnsureProxy()) return 1;

        Console.Error.WriteLine($"[KIWOOM] Calling {method}({string.Join(", ", paramStrings)})...");
        var sw = Stopwatch.StartNew();
        var resp = SendPipeRequest(method, paramObjects);
        sw.Stop();

        if (resp.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[KIWOOM] {method} failed: {resp.Error} ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KIWOOM] {method} -> {FormatResult(resp.Result)} ({sw.ElapsedMilliseconds}ms)");
        Console.ResetColor();
        return 0;
    }

    // -- TR Query (SetInputValue + CommRqData) --

    static int KiwoomQueryCommand(string[] rest)
    {
        if (rest.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot kiwoom query <tr_code> --input key=val [--input key=val] [--screen N] [--name N]");
            Console.WriteLine("Example: wkappbot kiwoom query opt10001 --input 종목코드=005930");
            return 1;
        }

        var trCode = rest[0];
        var inputs = new List<(string key, string val)>();
        string screenNo = "0101";
        string? rqName = null;

        for (int i = 1; i < rest.Length; i++)
        {
            if (rest[i] == "--input" && i + 1 < rest.Length)
            {
                var kv = rest[++i].Split('=', 2);
                if (kv.Length == 2)
                    inputs.Add((kv[0], kv[1]));
            }
            else if (rest[i] == "--screen" && i + 1 < rest.Length)
                screenNo = rest[++i];
            else if (rest[i] == "--name" && i + 1 < rest.Length)
                rqName = rest[++i];
        }

        rqName ??= trCode;

        if (!EnsureProxy()) return 1;

        // 1. SetInputValue for each input
        foreach (var (key, val) in inputs)
        {
            Console.Error.WriteLine($"[KIWOOM] SetInputValue(\"{key}\", \"{val}\")");
            var setResp = SendPipeRequest("SetInputValue", key, val);
            if (setResp.Error != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"[KIWOOM] SetInputValue failed: {setResp.Error}");
                Console.ResetColor();
                return 1;
            }
        }

        // 2. CommRqData
        Console.Error.WriteLine($"[KIWOOM] CommRqData(\"{rqName}\", \"{trCode}\", 0, \"{screenNo}\")");
        var sw = Stopwatch.StartNew();
        var resp = SendPipeRequest("CommRqData", rqName, trCode, 0, screenNo);
        sw.Stop();

        if (resp.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[KIWOOM] CommRqData failed: {resp.Error} ({sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KIWOOM] CommRqData -> {resp.Result} ({sw.ElapsedMilliseconds}ms)");
        Console.ResetColor();
        Console.WriteLine("[KIWOOM] TR response will arrive via OnReceiveTrData event");

        // Record TR query experience
        RecordTrQueryExperience(trCode, inputs.Select(kv => kv.key).ToArray());

        return 0;
    }

    // -- Realtime Registration --

    static int KiwoomRealtimeCommand(string[] rest)
    {
        if (rest.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot kiwoom realtime <codes> --fids N,N,N [--screen N] [--opt 0|1]");
            Console.WriteLine("Example: wkappbot kiwoom realtime 005930;000660 --fids 10,11,12,15 --screen 1001");
            return 1;
        }

        var codes = rest[0];
        string fids = "";
        string screenNo = "1001";
        int opt = 0; // 0=add, 1=replace

        for (int i = 1; i < rest.Length; i++)
        {
            if (rest[i] == "--fids" && i + 1 < rest.Length) fids = rest[++i];
            else if (rest[i] == "--screen" && i + 1 < rest.Length) screenNo = rest[++i];
            else if (rest[i] == "--opt" && i + 1 < rest.Length) opt = int.TryParse(rest[++i], out var o) ? o : 0;
        }

        if (string.IsNullOrEmpty(fids))
        {
            Console.WriteLine("[KIWOOM] --fids required (e.g., --fids 10,11,12,15)");
            return 1;
        }

        if (!EnsureProxy()) return 1;

        Console.Error.WriteLine($"[KIWOOM] SetRealReg(\"{screenNo}\", \"{codes}\", \"{fids}\", {opt})");
        var resp = SendPipeRequest("SetRealReg", screenNo, codes, fids, opt);

        if (resp.Error != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[KIWOOM] SetRealReg failed: {resp.Error}");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KIWOOM] SetRealReg -> {resp.Result}");
        Console.ResetColor();
        Console.WriteLine("[KIWOOM] Real-time data will arrive via OnReceiveRealData event");
        return 0;
    }

    // -- Knowhow (Manual COM Lesson Recording) --

    static int KiwoomKnowhowCommand(string[] rest)
    {
        if (rest.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot kiwoom knowhow \"lesson text\"");
            Console.WriteLine("Records a COM/API knowhow lesson to kiwoom_exp/knowhow.md");
            return 1;
        }

        var lesson = string.Join(" ", rest);
        var knowhowPath = Path.Combine(KiwoomExpDir, "knowhow.md");

        Directory.CreateDirectory(KiwoomExpDir);

        // Append with timestamp
        var entry = $"\n## {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n{lesson}\n";
        File.AppendAllText(knowhowPath, entry);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Error.WriteLine($"[KIWOOM] Knowhow recorded -> {knowhowPath}");
        Console.ResetColor();
        return 0;
    }

    // -- Experience DB Viewer --

    static int KiwoomExpCommand(string[] rest)
    {
        var filter = rest.Length > 0 ? rest[0] : null;

        Console.WriteLine("--─ Kiwoom Experience DB --─");

        // Methods
        var methodsDir = Path.Combine(KiwoomExpDir, "methods");
        if (Directory.Exists(methodsDir))
        {
            var files = Directory.GetFiles(methodsDir, "*.json")
                .Where(f => filter == null || Path.GetFileNameWithoutExtension(f).Contains(filter, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f);

            Console.WriteLine("\n  Methods:");
            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var exp = JsonSerializer.Deserialize<KiwoomMethodExp>(json);
                    if (exp == null) continue;
                    var successRate = exp.CallCount > 0 ? (double)exp.SuccessCount / exp.CallCount * 100 : 0;
                    Console.Write($"    {exp.MethodName,-30}");
                    Console.Write($" calls={exp.CallCount,4}");
                    Console.Write($" ok={exp.SuccessCount,4}");
                    Console.Write($" fail={exp.FailCount,3}");
                    Console.Write($" rate={successRate:F0}%");
                    Console.Write($" avg={exp.AvgResponseMs}ms");
                    Console.Write($" max={exp.MaxResponseMs}ms");
                    Console.WriteLine();
                }
                catch { }
            }
        }

        // Events
        var eventsDir = Path.Combine(KiwoomExpDir, "events");
        if (Directory.Exists(eventsDir))
        {
            var files = Directory.GetFiles(eventsDir, "*.json")
                .Where(f => filter == null || Path.GetFileNameWithoutExtension(f).Contains(filter, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f);

            Console.WriteLine("\n  Events:");
            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var exp = JsonSerializer.Deserialize<KiwoomEventExp>(json);
                    if (exp == null) continue;
                    Console.Write($"    {exp.EventName,-30}");
                    Console.Write($" count={exp.ReceiveCount,5}");
                    Console.Write($" first={exp.FirstReceived:yyyy-MM-dd HH:mm}");
                    Console.Write($" last={exp.LastReceived:yyyy-MM-dd HH:mm}");
                    Console.WriteLine();
                }
                catch { }
            }
        }

        // TR codes
        var trDir = Path.Combine(KiwoomExpDir, "tr_codes");
        if (Directory.Exists(trDir))
        {
            var files = Directory.GetFiles(trDir, "*.json")
                .Where(f => filter == null || Path.GetFileNameWithoutExtension(f).Contains(filter, StringComparison.OrdinalIgnoreCase))
                .OrderBy(f => f);

            Console.WriteLine("\n  TR Codes:");
            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var exp = JsonSerializer.Deserialize<KiwoomTrExp>(json);
                    if (exp == null) continue;
                    Console.Write($"    {exp.TrCode,-12}");
                    Console.Write($" queries={exp.QueryCount,4}");
                    Console.Write($" inputs=[{string.Join(",", exp.InputFields ?? Array.Empty<string>())}]");
                    Console.WriteLine();
                }
                catch { }
            }
        }

        // Knowhow
        var knowhowPath = Path.Combine(KiwoomExpDir, "knowhow.md");
        if (File.Exists(knowhowPath))
        {
            Console.WriteLine($"\n  Knowhow: {knowhowPath} ({new FileInfo(knowhowPath).Length} bytes)");
        }

        return 0;
    }

    // -- Pipe Client Helpers --

    /// <summary>Ensure proxy is running and pipe is available.</summary>
    static bool EnsureProxy()
    {
        if (IsPipeAvailable()) return true;

        Console.WriteLine("[KIWOOM] Proxy not available, starting...");
        var result = KiwoomStartCommand(Array.Empty<string>());
        if (result != 0) return false;

        // Wait for pipe
        for (int i = 0; i < 50; i++)
        {
            Thread.Sleep(100);
            if (IsPipeAvailable()) return true;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[KIWOOM] Proxy pipe not available after start");
        Console.ResetColor();
        return false;
    }

    /// <summary>Check if Named Pipe is available (proxy is listening).</summary>
    static bool IsPipeAvailable()
    {
        try
        {
            using var client = new NamedPipeClientStream(".", KiwoomPipeName, PipeDirection.InOut, PipeOptions.None);
            client.Connect(100); // 100ms timeout
            // Quick disconnect -- just checking if pipe exists
            return true;
        }
        catch
        {
            return false;
        }
    }

    static int _pipeRequestId;

    /// <summary>Send a JSON-RPC request to KiwoomProxy via Named Pipe and get the response.</summary>
    static KiwoomPipeResponse SendPipeRequest(string method, params object[] paramValues)
    {
        using var client = new NamedPipeClientStream(".", KiwoomPipeName, PipeDirection.InOut, PipeOptions.None);
        client.Connect(5000); // 5s timeout for connection

        var request = new KiwoomPipeRequest
        {
            Id = Interlocked.Increment(ref _pipeRequestId),
            Method = method,
            Params = paramValues.Length > 0 ? paramValues : null
        };

        // Write request (length-prefixed JSON)
        var json = JsonSerializer.Serialize(request, _kiwoomJsonOpts);
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var lenBytes = BitConverter.GetBytes(jsonBytes.Length);
        client.Write(lenBytes, 0, 4);
        client.Write(jsonBytes, 0, jsonBytes.Length);
        client.Flush();

        // Read response (length-prefixed JSON)
        var lenBuf = new byte[4];
        int bytesRead = ReadExact(client, lenBuf, 4);
        if (bytesRead < 4)
            return new KiwoomPipeResponse { Id = request.Id, Error = "Pipe read error (no response header)" };

        int respLen = BitConverter.ToInt32(lenBuf, 0);
        if (respLen <= 0 || respLen > 10 * 1024 * 1024)
            return new KiwoomPipeResponse { Id = request.Id, Error = $"Invalid response length: {respLen}" };

        var respBuf = new byte[respLen];
        bytesRead = ReadExact(client, respBuf, respLen);
        if (bytesRead < respLen)
            return new KiwoomPipeResponse { Id = request.Id, Error = "Pipe read error (incomplete response)" };

        var respJson = Encoding.UTF8.GetString(respBuf);
        return JsonSerializer.Deserialize<KiwoomPipeResponse>(respJson, _kiwoomJsonOpts)
            ?? new KiwoomPipeResponse { Id = request.Id, Error = "Failed to deserialize response" };
    }

    /// <summary>Read exactly N bytes from a stream (blocking).</summary>
    static int ReadExact(Stream stream, byte[] buffer, int count)
    {
        int offset = 0;
        while (offset < count)
        {
            int n = stream.Read(buffer, offset, count - offset);
            if (n == 0) return offset; // EOF
            offset += n;
        }
        return offset;
    }

    /// <summary>Get proxy PID from PID file, or -1 if not found.</summary>
    static int GetKiwoomProxyPid()
    {
        try
        {
            if (File.Exists(KiwoomProxyPidFile))
                return int.Parse(File.ReadAllText(KiwoomProxyPidFile).Trim());
        }
        catch { }
        return -1;
    }

    /// <summary>Forward a process output stream to console with a prefix tag.</summary>
    static void ForwardOutput(System.IO.StreamReader reader, string tag)
    {
        try
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                    Console.WriteLine($"{tag} {line}");
            }
        }
        catch { }
    }

    static string FormatResult(object? result)
    {
        if (result == null) return "null";
        if (result is JsonElement je)
        {
            return je.ValueKind switch
            {
                JsonValueKind.String => $"\"{je.GetString()}\"",
                JsonValueKind.Number => je.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => je.GetRawText()
            };
        }
        return result.ToString() ?? "?";
    }

    // -- Experience DB Helpers --

    static void ShowExpDbSummary()
    {
        var methodsDir = Path.Combine(KiwoomExpDir, "methods");
        var eventsDir = Path.Combine(KiwoomExpDir, "events");
        var trDir = Path.Combine(KiwoomExpDir, "tr_codes");

        int methodCount = Directory.Exists(methodsDir) ? Directory.GetFiles(methodsDir, "*.json").Length : 0;
        int eventCount = Directory.Exists(eventsDir) ? Directory.GetFiles(eventsDir, "*.json").Length : 0;
        int trCount = Directory.Exists(trDir) ? Directory.GetFiles(trDir, "*.json").Length : 0;

        Console.WriteLine($"  ExpDB: {methodCount} methods, {eventCount} events, {trCount} TR codes ({KiwoomExpDir})");
    }

    /// <summary>Record TR query experience (input fields learned).</summary>
    static void RecordTrQueryExperience(string trCode, string[] inputFieldNames)
    {
        try
        {
            var trDir = Path.Combine(KiwoomExpDir, "tr_codes");
            Directory.CreateDirectory(trDir);

            var filePath = Path.Combine(trDir, $"{trCode}.json");
            KiwoomTrExp exp;

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                exp = JsonSerializer.Deserialize<KiwoomTrExp>(json) ?? new() { TrCode = trCode };
            }
            else
            {
                exp = new() { TrCode = trCode, FirstQuery = DateTime.UtcNow };
            }

            exp.QueryCount++;
            exp.LastQuery = DateTime.UtcNow;

            // Learn input fields (merge new fields)
            exp.InputFields ??= Array.Empty<string>();
            var merged = new HashSet<string>(exp.InputFields);
            foreach (var f in inputFieldNames)
                merged.Add(f);
            exp.InputFields = merged.ToArray();

            var outJson = JsonSerializer.Serialize(exp, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, outJson);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[KIWOOM] TR ExpDB save error: {ex.Message}");
        }
    }

    // -- JSON-RPC Models (CLI-side, independent of KiwoomProxy project) --

    static readonly JsonSerializerOptions _kiwoomJsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    class KiwoomPipeRequest
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("method")] public string Method { get; set; } = "";
        [JsonPropertyName("params")] public object?[]? Params { get; set; }
    }

    class KiwoomPipeResponse
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("result")] public object? Result { get; set; }
        [JsonPropertyName("error")] public string? Error { get; set; }
    }

    // -- Experience DB Models (CLI-side readers) --

    class KiwoomMethodExp
    {
        [JsonPropertyName("MethodName")] public string MethodName { get; set; } = "";
        [JsonPropertyName("CallCount")] public int CallCount { get; set; }
        [JsonPropertyName("SuccessCount")] public int SuccessCount { get; set; }
        [JsonPropertyName("FailCount")] public int FailCount { get; set; }
        [JsonPropertyName("AvgResponseMs")] public long AvgResponseMs { get; set; }
        [JsonPropertyName("MaxResponseMs")] public long MaxResponseMs { get; set; }
        [JsonPropertyName("FirstCall")] public DateTime FirstCall { get; set; }
        [JsonPropertyName("LastCall")] public DateTime LastCall { get; set; }
        [JsonPropertyName("CommonErrors")] public List<string>? CommonErrors { get; set; }
        [JsonPropertyName("Knowhow")] public string? Knowhow { get; set; }
    }

    class KiwoomEventExp
    {
        [JsonPropertyName("EventName")] public string EventName { get; set; } = "";
        [JsonPropertyName("ReceiveCount")] public int ReceiveCount { get; set; }
        [JsonPropertyName("FirstReceived")] public DateTime FirstReceived { get; set; }
        [JsonPropertyName("LastReceived")] public DateTime LastReceived { get; set; }
        [JsonPropertyName("Knowhow")] public string? Knowhow { get; set; }
    }

    class KiwoomTrExp
    {
        [JsonPropertyName("TrCode")] public string TrCode { get; set; } = "";
        [JsonPropertyName("QueryCount")] public int QueryCount { get; set; }
        [JsonPropertyName("FirstQuery")] public DateTime FirstQuery { get; set; }
        [JsonPropertyName("LastQuery")] public DateTime LastQuery { get; set; }
        [JsonPropertyName("InputFields")] public string[]? InputFields { get; set; }
        [JsonPropertyName("OutputFields")] public string[]? OutputFields { get; set; }
        [JsonPropertyName("HasNext")] public bool HasNext { get; set; }
        [JsonPropertyName("Knowhow")] public string? Knowhow { get; set; }
    }
}
