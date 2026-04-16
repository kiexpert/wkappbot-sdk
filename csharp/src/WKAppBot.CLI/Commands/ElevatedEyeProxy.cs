// ElevatedEyeProxy.cs — Elevated Eye Named Pipe proxy.
// When Eye runs as admin, it listens on a Named Pipe for CLI commands.
// CLI (normal user) sends commands to Eye (admin) for transparent execution.
// Same pattern as KiwoomProxy (64-bit↔32-bit Named Pipe).

using System.Diagnostics;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// ── Protocol ──

/// <summary>Request from CLI → elevated Eye.</summary>
sealed class EyeProxyRequest
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("command")] public string Command { get; set; } = "";
    [JsonPropertyName("args")] public string[] Args { get; set; } = [];
}

/// <summary>Response from elevated Eye → CLI.</summary>
sealed class EyeProxyResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("exitCode")] public int ExitCode { get; set; }
    [JsonPropertyName("stdout")] public string Stdout { get; set; } = "";
    [JsonPropertyName("stderr")] public string Stderr { get; set; } = "";
    [JsonPropertyName("error")] public string? Error { get; set; }
}

// ── Wire: length-prefixed JSON (reuse KiwoomProxy pattern) ──

static class EyeProxyWire
{
    static readonly JsonSerializerOptions Opts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    public static async Task WriteAsync(Stream s, object msg, CancellationToken ct = default)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(msg, Opts);
        var len = BitConverter.GetBytes(json.Length);
        await s.WriteAsync(len, 0, 4, ct);
        await s.WriteAsync(json, 0, json.Length, ct);
        await s.FlushAsync(ct);
    }

    public static async Task<T?> ReadAsync<T>(Stream s, CancellationToken ct = default)
    {
        var lenBuf = new byte[4];
        if (await ReadExact(s, lenBuf, 4, ct) < 4) return default;
        int len = BitConverter.ToInt32(lenBuf, 0);
        if (len <= 0 || len > 10 * 1024 * 1024) return default;
        var buf = new byte[len];
        if (await ReadExact(s, buf, len, ct) < len) return default;
        return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(buf), Opts);
    }

    static async Task<int> ReadExact(Stream s, byte[] buf, int count, CancellationToken ct)
    {
        int off = 0;
        while (off < count)
        {
            int n = await s.ReadAsync(buf, off, count - off, ct);
            if (n == 0) return off;
            off += n;
        }
        return off;
    }
}

// ── Server (runs inside elevated Eye) ──

/// <summary>
/// Named Pipe server that runs inside elevated Eye process.
/// Executes wkappbot commands as admin and returns results.
/// Pipe name: wkappbot_elevated
/// </summary>
static class ElevatedEyeServer
{
    public const string PipeName = "wkappbot_elevated";

    /// <summary>True while at least one admin command is being executed — hot-swap must defer.</summary>
    public static bool IsBusy => _activeClients > 0;
    static volatile int _activeClients;

    public static async Task ListenAsync(CancellationToken ct)
    {
        Console.Error.WriteLine($"[EYE:PIPE] Eye pipe listening on \\\\.\\pipe\\{PipeName}");

        while (!ct.IsCancellationRequested)
        {
            NamedPipeServerStream? pipe = null;
            try
            {
                // Allow non-admin clients to connect (UIPI pipe security)
                var pipeSec = new PipeSecurity();
                pipeSec.AddAccessRule(new PipeAccessRule(
                    new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null),
                    PipeAccessRights.ReadWrite, AccessControlType.Allow));
                pipeSec.AddAccessRule(new PipeAccessRule(
                    new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null),
                    PipeAccessRights.FullControl, AccessControlType.Allow));

                pipe = NamedPipeServerStreamAcl.Create(
                    PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte, PipeOptions.Asynchronous,
                    inBufferSize: 0, outBufferSize: 0, pipeSecurity: pipeSec);

                await pipe.WaitForConnectionAsync(ct);
                var connectedPipe = pipe;
                pipe = null; // ownership transferred
                Interlocked.Increment(ref _activeClients);
                _ = Task.Run(async () =>
                {
                    try { await HandleClient(connectedPipe, ct); }
                    finally { Interlocked.Decrement(ref _activeClients); }
                }, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE:PIPE] Pipe error: {ex.Message}");
                // Backoff to avoid log-spam tight loop when another process holds the pipe name
                try { await Task.Delay(TimeSpan.FromSeconds(5), ct); }
                catch (OperationCanceledException) { break; }
            }
            finally
            {
                pipe?.Dispose();
            }

            // No delay on happy-path connections — next WaitForConnectionAsync starts immediately
        }

        Console.WriteLine("[EYE:PIPE] Eye pipe stopped");
    }

    static async Task HandleClient(NamedPipeServerStream pipe, CancellationToken ct)
    {
        while (pipe.IsConnected && !ct.IsCancellationRequested)
        {
            try
            {
                var req = await EyeProxyWire.ReadAsync<EyeProxyRequest>(pipe, ct);
                if (req == null) break;

                Console.Error.WriteLine($"[EYE:ADMIN] Exec: {req.Command} {string.Join(" ", req.Args)}");

                var resp = await ExecuteCommand(req);
                await EyeProxyWire.WriteAsync(pipe, resp, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (IOException) { break; }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE:ADMIN] Handler error: {ex.Message}");
            }
        }

        Console.WriteLine("[EYE:PIPE] Client disconnected");
    }

    static async Task<EyeProxyResponse> ExecuteCommand(EyeProxyRequest req)
    {
        // Special: __eye_tick__ returns cached Eye loop state without spawning a subprocess
        if (req.Command == "__eye_tick__")
        {
            var tickResp = Program.BuildIpcTickResponse();
            var tickJson = System.Text.Json.JsonSerializer.Serialize(tickResp,
                new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });
            return new EyeProxyResponse { Id = req.Id, ExitCode = 0, Stdout = tickJson };
        }

        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";
            // Build args: command + original args
            var allArgs = new List<string> { req.Command };
            allArgs.AddRange(req.Args);
            var argsStr = string.Join(" ", allArgs.Select(a =>
                a.Contains(' ') || a.Contains('"') ? $"\"{a.Replace("\"", "\\\"")}\"" : a));

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = argsStr,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "EYE-PROXY");
            if (proc == null)
                return new EyeProxyResponse { Id = req.Id, ExitCode = -1, Error = "Failed to start process" };

            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            return new EyeProxyResponse
            {
                Id = req.Id,
                ExitCode = proc.ExitCode,
                Stdout = await stdoutTask,
                Stderr = await stderrTask,
            };
        }
        catch (Exception ex)
        {
            return new EyeProxyResponse { Id = req.Id, ExitCode = -1, Error = ex.Message };
        }
    }
}

// ── Client (used by normal CLI to send commands to elevated Eye) ──

/// <summary>
/// Named Pipe client for sending commands to elevated Eye.
/// Used when CLI detects admin-required target and elevated Eye is running.
/// </summary>
static class ElevatedEyeClient
{
    static int _nextId;

    /// <summary>Check if elevated Eye proxy pipe exists (without consuming a connection).</summary>
    public static bool IsAvailable()
        => File.Exists($@"\\.\pipe\{ElevatedEyeServer.PipeName}");

    /// <summary>
    /// Liveness probe: attempt to connect to admin Eye pipe within connectMs.
    /// Stronger than IsAvailable (which only checks pipe file existence).
    /// Confirms a server is actually accepting connections.
    /// Returns true only if connect succeeded within the timeout.
    /// Caller should use a hard ceiling (e.g. 200ms) — if admin Eye is in bad state,
    /// fall through to LaunchElevatedEye (sudo protection path) for recovery.
    /// </summary>
    public static bool Ping(int connectMs = 200)
    {
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            using var cts = new CancellationTokenSource(connectMs);
            pipe.ConnectAsync(connectMs, cts.Token).GetAwaiter().GetResult();
            return pipe.IsConnected;
        }
        catch { return false; }
    }

    /// <summary>Send a command to elevated Eye and get the result.</summary>
    public static async Task<EyeProxyResponse?> SendCommandAsync(
        string command, string[] args, int timeoutMs = 5000)
    {
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            using var cts = new CancellationTokenSource(timeoutMs);
            await pipe.ConnectAsync(timeoutMs, cts.Token); // use caller's timeoutMs, not hardcoded 3000

            var req = new EyeProxyRequest
            {
                Id = Interlocked.Increment(ref _nextId),
                Command = command,
                Args = args,
            };

            await EyeProxyWire.WriteAsync(pipe, req, cts.Token);
            return await EyeProxyWire.ReadAsync<EyeProxyResponse>(pipe, cts.Token);
        }
        catch (TimeoutException)
        {
            Console.WriteLine("[ELEVATION] Elevated Eye proxy timeout");
            return null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ELEVATION] Proxy communication error ({ex.GetType().Name}): {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Execute a command via elevated Eye proxy, printing stdout/stderr transparently.
    /// Returns exit code, or -1 if proxy unavailable.
    /// </summary>
    public static int ExecuteViaProxy(string command, string[] args)
    {
        var resp = SendCommandAsync(command, args).GetAwaiter().GetResult();
        if (resp == null) return -1;

        if (!string.IsNullOrEmpty(resp.Stdout))
            Console.Write(resp.Stdout);
        if (!string.IsNullOrEmpty(resp.Stderr))
            Console.Error.Write(resp.Stderr);
        if (!string.IsNullOrEmpty(resp.Error))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[ELEVATION] Proxy error: {resp.Error}");
            Console.ResetColor();
        }

        return resp.ExitCode;
    }
}

// ── Helpers ──

static class ElevationHelper
{
    /// <summary>Check if current process is running as administrator.</summary>
    public static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// Check if target window is elevated and delegate command via proxy if needed.
    /// Returns (shouldDelegate, exitCode). If shouldDelegate=false, caller proceeds normally.
    /// </summary>
    public static (bool delegated, int exitCode) TryDelegateIfElevated(
        IntPtr targetHwnd, string command, string[] args)
    {
        if (NativeMethods.IsCurrentProcessElevated())
            return (false, 0); // we're already admin

        // MCP mode: never launch new processes (no console window, no Eye spawn)
        // Try existing elevated Eye proxy. If unavailable, signal Launcher to re-route via admin Core.
        if (Program.IsMcpMode || Program.RunningInEye)
        {
            NativeMethods.GetWindowThreadProcessId(targetHwnd, out uint mcpPid);
            var mcpTargetElev = NativeMethods.IsProcessElevated(mcpPid);
            Console.Error.WriteLine($"[ELEVATION] MCP/Eye mode: target pid={mcpPid} elevated={mcpTargetElev} IsMcp={Program.IsMcpMode} InEye={Program.RunningInEye}");
            if (mcpTargetElev == true)
            {
                if (ElevatedEyeClient.IsAvailable())
                {
                    Console.Error.WriteLine("[ELEVATION] Delegating via existing elevated Eye proxy pipe");
                    var exit = ElevatedEyeClient.ExecuteViaProxy(command, args);
                    Console.Error.WriteLine($"[ELEVATION] Proxy result: exit={exit}");
                    if (exit != -1) return (true, exit);
                    Console.Error.WriteLine("[ELEVATION] Proxy failed — falling through");
                }
                else
                {
                    Console.Error.WriteLine("[ELEVATION] No elevated Eye proxy available");
                }
                if (Program.IsMcpMode)
                {
                    Console.Error.WriteLine("[ELEVATION:MCP] Signaling Launcher for admin Core re-route");
                    Program.McpElevationRequired = true;
                }
                else
                {
                    Console.Error.WriteLine("[ELEVATION:EYE] Cannot elevate in Eye worker — limited access");
                }
            }
            return (false, 0);
        }

        NativeMethods.GetWindowThreadProcessId(targetHwnd, out uint pid);
        var targetElev = NativeMethods.IsProcessElevated(pid);
        if (targetElev != true)
            return (false, 0); // target is not admin

        // Avoid circular reference: don't delegate if target IS another wkappbot instance
        // (e.g., trying to close the Elevated Eye itself)
        // Use window class name — Process.GetProcessById fails on admin processes from non-admin
        var className = WindowFinder.GetClassName(targetHwnd);
        if (className.Contains("wkappbot", StringComparison.OrdinalIgnoreCase))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[ELEVATION] Target is wkappbot process (class={className}) — skipping proxy delegation");
            Console.ResetColor();
            return (false, 0);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine($"[ELEVATION] Target (pid={pid}) is elevated — requires admin proxy");
        Console.ResetColor();

        // Strategy 1: try existing Elevated Eye proxy
        if (ElevatedEyeClient.IsAvailable())
        {
            Console.WriteLine("[ELEVATION] Delegating via Elevated Eye proxy...");
            var exit1 = ElevatedEyeClient.ExecuteViaProxy(command, args);
            if (exit1 != -1) return (true, exit1);
            // Proxy connection failed → fall through to Strategy 2
            Console.WriteLine("[ELEVATION] Existing proxy unreachable, launching new one...");
        }

        // Strategy 2: launch Elevated Eye, then delegate
        if (LaunchElevatedEye())
        {
            var exit2 = ElevatedEyeClient.ExecuteViaProxy(command, args);
            if (exit2 != -1) return (true, exit2);
        }

        // Strategy 3: fall through — let caller proceed with limited access
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("[ELEVATION] Proxy unavailable — continuing with limited access");
        Console.ResetColor();
        return (false, 0);
    }

    /// <summary>
    /// Unified entry: wait for admin server availability.
    /// MCP mode: signal Launcher via McpElevationRequired, return false (Launcher handles).
    /// Eye mode: try existing proxy pipe, return availability.
    /// Normal mode: try proxy, then LaunchElevatedEye.
    /// </summary>
    public static bool WaitForAdminServer()
    {
        // Already available?
        if (ElevatedEyeClient.IsAvailable()) return true;

        if (Program.IsMcpMode)
        {
            // Signal Launcher to spawn admin Core
            Console.Error.WriteLine("[ELEVATION] WaitForAdminServer: MCP mode — signaling Launcher");
            Program.McpElevationRequired = true;
            return false; // Launcher will re-route
        }

        if (Program.RunningInEye)
        {
            Console.Error.WriteLine("[ELEVATION] WaitForAdminServer: Eye mode — proxy unavailable");
            return false;
        }

        // Normal mode: launch elevated Eye
        return LaunchElevatedEye();
    }

    /// <summary>
    /// Launch Eye as admin with --elevated flag.
    /// Returns true if UAC was approved and Eye started.
    /// </summary>
    /// <summary>Alert user before elevation request — wkappbot speak (fire-and-forget).</summary>
    static void PlayElevationMelody()
    {
        _ = Task.Run(() =>
        {
            try
            {
                AppBotPipe.Spawn(
                    Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? "", "wkappbot.exe"),
                    "speak 관리자 권한이 필요합니다",
                    Environment.CurrentDirectory, caller: "ELEVATED-SPEAK");
            }
            catch { }
        });
    }

    public static bool LaunchElevatedEye(string reason = "Eye proxy")
    {
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";

            // ── Pre-spawn hot-swap: promote wkappbot-core.new.exe if pending ──
            // Admin Eye starts from exePath; rename-swap latest before UAC so admin runs newest code.
            // Rename of running exe is permitted on Windows; delete/overwrite is not.
            try
            {
                var exeDir = System.IO.Path.GetDirectoryName(exePath);
                if (!string.IsNullOrEmpty(exeDir))
                {
                    var newExePath = System.IO.Path.Combine(exeDir, "wkappbot-core.new.exe");
                    if (System.IO.File.Exists(newExePath))
                    {
                        var backupPath = System.IO.Path.Combine(exeDir, $"wkappbot-core.old-sudo-{DateTime.Now:yyyyMMdd-HHmmss}.exe");
                        Console.Error.WriteLine("[ELEVATION:HOT-SWAP] pending .new.exe detected — promoting before admin spawn");
                        try { System.IO.File.Move(exePath, backupPath); } catch { /* already renamed */ }
                        System.IO.File.Move(newExePath, exePath);
                        Console.Error.WriteLine("[ELEVATION:HOT-SWAP] promoted — admin Eye will run latest core");
                    }
                }
            }
            catch (Exception hsEx)
            {
                Console.Error.WriteLine($"[ELEVATION:HOT-SWAP] skipped: {hsEx.Message} — proceeding with current exe");
            }

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = "eye --elevated",
                UseShellExecute = true,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            PlayElevationMelody(); // 🎵 before UAC

            // Print reason + recent pulse trail BEFORE UAC so user knows why elevation was requested
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[ELEVATION] ▼ Requesting admin rights: {reason}");
            var trail = WKAppBot.CLI.PulseStep.GetRecentTrail(5);
            if (trail.Count > 0)
            {
                Console.Error.WriteLine("[ELEVATION] ── recent step trail ──");
                foreach (var line in trail) Console.Error.WriteLine($"[ELEVATION]   {line}");
                Console.Error.WriteLine("[ELEVATION] ──────────────────────");
            }
            Console.ResetColor();

            var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "EYE-ELEVATED");
            if (proc == null) return false;

            // Wait for pipe file to appear (proxy ready) — up to 10s
            for (int i = 0; i < 40; i++)
            {
                Thread.Sleep(250);
                if (ElevatedEyeClient.IsAvailable())
                {
                    Thread.Sleep(200); // settle — give server time to enter WaitForConnectionAsync
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[ELEVATION] Elevated Eye proxy is ready!");
                    Console.ResetColor();
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ELEVATION] Elevated Eye did not start in time");
            Console.ResetColor();
            return false;
        }
        catch (System.ComponentModel.Win32Exception)
        {
            // UAC cancelled by user
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[ELEVATION] UAC cancelled — continuing without admin rights");
            Console.ResetColor();
            return false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ELEVATION] Failed to launch elevated Eye: {ex.Message}");
            return false;
        }
    }
}

// ── Eye IPC: eye tick queries running Eye loop for cached state ──
// Pipe name: wkappbot_eye_ipc  Request: {"cmd":"tick"}  Response: EyeIpcTickResponse JSON

/// <summary>Response from running Eye loop → one-shot eye tick client.</summary>
sealed class EyeIpcTickResponse
{
    [JsonPropertyName("summary")]          public string Summary { get; set; } = "";
    [JsonPropertyName("cardCount")]        public int CardCount { get; set; }
    [JsonPropertyName("contextPct")]       public int ContextPct { get; set; }
    [JsonPropertyName("plans")]            public string[] Plans { get; set; } = [];
    [JsonPropertyName("promptSource")]     public string PromptSource { get; set; } = "";
    [JsonPropertyName("prompt")]           public string Prompt { get; set; } = "";
    [JsonPropertyName("guardArmed")]       public bool GuardArmed { get; set; }
    [JsonPropertyName("execIdleSec")]      public double ExecIdleSec { get; set; }
    [JsonPropertyName("aiIdleSec")]        public double AiIdleSec { get; set; }
    [JsonPropertyName("cooldownSec")]      public double CooldownSec { get; set; }
    [JsonPropertyName("keepAwakeAgeSec")]  public double KeepAwakeAgeSec { get; set; }
    [JsonPropertyName("latestTickAgeSec")] public double LatestTickAgeSec { get; set; }
    [JsonPropertyName("promptLineHint")]   public int PromptLineHint { get; set; }
    [JsonPropertyName("tickLineHint")]     public int TickLineHint { get; set; }
    [JsonPropertyName("cachedAgeMs")]      public long CachedAgeMs { get; set; }
}

/// <summary>Client used by one-shot eye tick to query the running Eye loop's cached state via ElevatedEyeServer.</summary>
static class EyeIpcClient
{
    public static async Task<EyeIpcTickResponse?> QueryTickAsync(int timeoutMs = 2000)
    {
        var resp = await ElevatedEyeClient.SendCommandAsync("__eye_tick__", [], timeoutMs);
        if (resp == null || resp.ExitCode != 0 || string.IsNullOrEmpty(resp.Stdout)) return null;
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<EyeIpcTickResponse>(resp.Stdout,
                new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });
        }
        catch { return null; }
    }
}
