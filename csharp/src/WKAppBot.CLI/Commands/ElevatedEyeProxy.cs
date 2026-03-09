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

    public static async Task ListenAsync(CancellationToken ct)
    {
        Console.WriteLine($"[EYE:ADMIN] Elevated proxy listening on \\\\.\\pipe\\{PipeName}");

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
                    PipeName, PipeDirection.InOut, 1,
                    PipeTransmissionMode.Byte, PipeOptions.Asynchronous,
                    inBufferSize: 0, outBufferSize: 0, pipeSecurity: pipeSec);

                await pipe.WaitForConnectionAsync(ct);
                Console.WriteLine("[EYE:ADMIN] Client connected");

                await HandleClient(pipe, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE:ADMIN] Pipe error: {ex.Message}");
            }
            finally
            {
                pipe?.Dispose();
            }

            if (!ct.IsCancellationRequested)
                await Task.Delay(100, ct).ConfigureAwait(false);
        }

        Console.WriteLine("[EYE:ADMIN] Elevated proxy stopped");
    }

    static async Task HandleClient(NamedPipeServerStream pipe, CancellationToken ct)
    {
        while (pipe.IsConnected && !ct.IsCancellationRequested)
        {
            try
            {
                var req = await EyeProxyWire.ReadAsync<EyeProxyRequest>(pipe, ct);
                if (req == null) break;

                Console.WriteLine($"[EYE:ADMIN] Exec: {req.Command} {string.Join(" ", req.Args)}");

                var resp = await ExecuteCommand(req);
                await EyeProxyWire.WriteAsync(pipe, resp, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (IOException) { break; }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE:ADMIN] Handler error: {ex.Message}");
            }
        }

        Console.WriteLine("[EYE:ADMIN] Client disconnected");
    }

    static async Task<EyeProxyResponse> ExecuteCommand(EyeProxyRequest req)
    {
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

            using var proc = Process.Start(psi);
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

    /// <summary>Check if elevated Eye proxy is running (try connecting).</summary>
    public static bool IsAvailable()
    {
        try
        {
            // File.Exists on named pipe is unreliable — try actual connection
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.None);
            pipe.Connect(500); // 500ms timeout
            return true;
        }
        catch { return false; }
    }

    /// <summary>Send a command to elevated Eye and get the result.</summary>
    public static async Task<EyeProxyResponse?> SendCommandAsync(
        string command, string[] args, int timeoutMs = 30000)
    {
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            using var cts = new CancellationTokenSource(timeoutMs);
            await pipe.ConnectAsync(3000, cts.Token);

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
            Console.WriteLine($"[ELEVATION] Proxy communication error ({ex.GetType().Name}): {ex.Message}");
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
            Console.WriteLine($"[ELEVATION] Proxy error: {resp.Error}");
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
            Console.WriteLine($"[ELEVATION] Target is wkappbot process (class={className}) — skipping proxy delegation");
            Console.ResetColor();
            return (false, 0);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ELEVATION] Target (pid={pid}) is elevated — requires admin proxy");
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
    /// Launch Eye as admin with --elevated flag.
    /// Returns true if UAC was approved and Eye started.
    /// </summary>
    public static bool LaunchElevatedEye()
    {
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = "eye --elevated",
                UseShellExecute = true,
                Verb = "runas",
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ELEVATION] Requesting admin rights for Eye proxy...");
            Console.ResetColor();

            var proc = Process.Start(psi);
            if (proc == null) return false;

            // Wait briefly for pipe to become available
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(500);
                if (ElevatedEyeClient.IsAvailable())
                {
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
            Console.WriteLine($"[ELEVATION] Failed to launch elevated Eye: {ex.Message}");
            return false;
        }
    }
}
