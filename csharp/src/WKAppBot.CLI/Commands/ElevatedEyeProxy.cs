// ElevatedEyeProxy.cs -- Elevated Eye Named Pipe proxy.
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

// -- Protocol --

/// <summary>Request from CLI -> elevated Eye.</summary>
sealed class EyeProxyRequest
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("command")] public string Command { get; set; } = "";
    [JsonPropertyName("args")] public string[] Args { get; set; } = [];
    [JsonPropertyName("workingDir")] public string? WorkingDir { get; set; }
    // Streaming output: if set, admin Eye connects to these pipes and CopyToAsync
    // subprocess stdout/stderr directly. No buffering in admin Eye.
    [JsonPropertyName("streamOutPipe")] public string? StreamOutPipe { get; set; }
    [JsonPropertyName("streamErrPipe")] public string? StreamErrPipe { get; set; }
}

/// <summary>Response from elevated Eye -> CLI.</summary>
sealed class EyeProxyResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("exitCode")] public int ExitCode { get; set; }
    [JsonPropertyName("stdout")] public string Stdout { get; set; } = "";
    [JsonPropertyName("stderr")] public string Stderr { get; set; } = "";
    [JsonPropertyName("error")] public string? Error { get; set; }
}

// -- Wire: length-prefixed JSON (reuse KiwoomProxy pattern) --

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

// -- Server (runs inside elevated Eye) --

/// <summary>
/// Named Pipe server that runs inside elevated Eye process.
/// Executes wkappbot commands as admin and returns results.
/// Pipe name: wkappbot_elevated
/// </summary>
static class ElevatedEyeServer
{
    public static readonly string PipeName = $"wkappbot_elevated_{ProjectRoot.Hash8()}";

    /// <summary>True while at least one admin command is being executed -- hot-swap must defer.</summary>
    public static bool IsBusy => _activeClients > 0;
    static volatile int _activeClients;

    // Number of concurrent accept-loop workers. Must be >= expected parallel --sudo
    // burst size; fewer listeners mean a window where all instances are busy servicing
    // and new ConnectAsync stalls until one loops back to WaitForConnectionAsync.
    // 26 simultaneous Launcher probes (bug-auto merged count) -> 32 workers gives
    // comfortable headroom without exhausting the OS max (MaxAllowedServerInstances).
    const int AcceptWorkers = 32;

    public static async Task ListenAsync(CancellationToken ct)
    {
        Console.Error.WriteLine($"[EYE:PIPE] Eye pipe listening on \\\\.\\pipe\\{PipeName} workers={AcceptWorkers}");

        // Run N concurrent acceptor loops. Each maintains its own pipe server
        // instance and loops WaitForConnectionAsync -> handoff -> create-next.
        // At any moment ~AcceptWorkers instances are in WaitForConnectionAsync,
        // so a burst of simultaneous --sudo ConnectAsync calls all get accepted
        // without serializing on the single-listener accept gap.
        var workers = new Task[AcceptWorkers];
        for (int i = 0; i < AcceptWorkers; i++)
        {
            int workerId = i;
            workers[i] = Task.Run(() => AcceptLoop(workerId, ct), ct);
        }

        try { await Task.WhenAll(workers); }
        catch (OperationCanceledException) { /* shutdown */ }

        Console.WriteLine("[EYE:PIPE] Eye pipe stopped");
    }

    static async Task AcceptLoop(int workerId, CancellationToken ct)
    {
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
                pipe = null; // ownership transferred to handler
                Interlocked.Increment(ref _activeClients);
                _ = Task.Run(async () =>
                {
                    try { await HandleClient(connectedPipe, ct); }
                    finally
                    {
                        Interlocked.Decrement(ref _activeClients);
                        try { connectedPipe.Dispose(); } catch { }
                    }
                }, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE:PIPE] worker#{workerId} error: {ex.Message}");
                // Backoff to avoid log-spam tight loop when another process holds the pipe name
                try { await Task.Delay(TimeSpan.FromSeconds(5), ct); }
                catch (OperationCanceledException) { break; }
            }
            finally
            {
                pipe?.Dispose();
            }

            // No delay on happy-path connections -- this worker's next
            // WaitForConnectionAsync starts immediately, and meanwhile the
            // other AcceptWorkers-1 listeners are still armed.
        }
    }

    static async Task HandleClient(NamedPipeServerStream pipe, CancellationToken ct)
    {
        while (pipe.IsConnected && !ct.IsCancellationRequested)
        {
            try
            {
                var req = await EyeProxyWire.ReadAsync<EyeProxyRequest>(pipe, ct);
                if (req == null) break;

                if (req.Command != "__eye_tick__" && string.IsNullOrWhiteSpace(req.WorkingDir))
                {
                    Console.Error.WriteLine($"[EYE:ADMIN] REJECTED id={req.Id} cmd='{req.Command}': WorkingDir not set");
                    await EyeProxyWire.WriteAsync(pipe, new EyeProxyResponse
                    {
                        Id = req.Id, ExitCode = 1,
                        Stderr = $"[SUDO-PROXY] WorkingDir not set in request -- upgrade caller (cmd='{req.Command}')",
                    }, ct);
                    continue;
                }

                Console.Error.WriteLine($"[EYE:ADMIN] Exec: {req.Command} {string.Join(" ", req.Args)} cwd={req.WorkingDir}");

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

        var reqTag = $"id={req.Id} cmd='{req.Command}'";
        try
        {
            // WorkingDir is required -- callers must always set it (via Environment.CurrentDirectory).
            // Silently falling back to admin Eye's own CWD causes wrong-project execution.
            if (string.IsNullOrWhiteSpace(req.WorkingDir) || !Directory.Exists(req.WorkingDir))
                return new EyeProxyResponse
                {
                    Id = req.Id, ExitCode = 1,
                    Stderr = $"[SUDO-PROXY] WorkingDir missing or invalid: '{req.WorkingDir}' -- request rejected",
                };

            var exePath = Environment.ProcessPath ?? "wkappbot.exe";
            var allArgs = new List<string> { req.Command };
            allArgs.AddRange(req.Args);
            var argsStr = string.Join(" ", allArgs.Select(a =>
                a.Contains(' ') || a.Contains('"') ? $"\"{a.Replace("\"", "\\\"")}\"" : a));

            // Streaming path: caller provided named pipes -- CopyToAsync directly, no buffer in admin Eye.
            if (!string.IsNullOrEmpty(req.StreamOutPipe))
            {
                var psiS = new ProcessStartInfo
                {
                    FileName = exePath, Arguments = argsStr, UseShellExecute = false,
                    RedirectStandardOutput = true, RedirectStandardError = true,
                    CreateNoWindow = true, WorkingDirectory = req.WorkingDir,
                    StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8,
                };
                psiS.EnvironmentVariables["WKAPPBOT_LOOP_CALLER"] = "eye-proxy";
                psiS.EnvironmentVariables["WKAPPBOT_SUDO_ACTIVE"] = "1";
                psiS.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";

                using var sProc = AppBotPipe.StartTracked(psiS, Environment.CurrentDirectory, "EYE-PROXY-STREAM");
                if (sProc == null)
                    return new EyeProxyResponse { Id = req.Id, ExitCode = -1, Error = "Failed to start process (streaming)" };

                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} STREAM pid={sProc.Id}");

                // Connect to caller's streaming pipes and copy subprocess output directly.
                var outPipe = new NamedPipeClientStream(".", req.StreamOutPipe, PipeDirection.Out, PipeOptions.Asynchronous);
                var errPipe = !string.IsNullOrEmpty(req.StreamErrPipe)
                    ? new NamedPipeClientStream(".", req.StreamErrPipe, PipeDirection.Out, PipeOptions.Asynchronous)
                    : null;
                await outPipe.ConnectAsync(3000);
                if (errPipe != null) await errPipe.ConnectAsync(3000);

                var outCopy = sProc.StandardOutput.BaseStream.CopyToAsync(outPipe);
                var errCopy = errPipe != null
                    ? sProc.StandardError.BaseStream.CopyToAsync(errPipe)
                    : sProc.StandardError.ReadToEndAsync().ContinueWith(_ => { });

                await sProc.WaitForExitAsync();
                await Task.WhenAll(outCopy, errCopy);
                await outPipe.FlushAsync(); outPipe.Dispose();
                if (errPipe != null) { await errPipe.FlushAsync(); errPipe.Dispose(); }

                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} STREAM done exit={sProc.ExitCode}");
                return new EyeProxyResponse { Id = req.Id, ExitCode = sProc.ExitCode };
            }

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = argsStr,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = req.WorkingDir,
                // Must match subprocess's Console.OutputEncoding (UTF-8 set at startup).
                // Without this, StreamReader defaults to system codepage (CP949 on Korean Windows)
                // and misinterprets UTF-8 Korean bytes as mojibake.
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };
            // Subprocess-tag: skip Eye auto-spawn in child. When elevated parent already holds
            // stdout pipe, a grandchild Eye would inherit it and block ReadToEndAsync forever.
            psi.EnvironmentVariables["WKAPPBOT_LOOP_CALLER"] = "eye-proxy";
            psi.EnvironmentVariables["WKAPPBOT_SUDO_ACTIVE"] = "1";
            // RunningInEye=true: skip card registration, OrphanGuard, TeeWriter console init,
            // and other interactive-session startup paths that hang under elevated stdout redirection.
            psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";

            Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} StartTracked exe={Path.GetFileName(exePath)} args=\"{argsStr}\"");
            var swStart = System.Diagnostics.Stopwatch.StartNew();
            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "EYE-PROXY");
            if (proc == null)
            {
                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} StartTracked returned null");
                return new EyeProxyResponse { Id = req.Id, ExitCode = -1, Error = "Failed to start process" };
            }
            Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} child pid={proc.Id} started in {swStart.ElapsedMilliseconds}ms");

            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();

            // Hard timeout: admin Eye must respond promptly or client will abandon.
            // 30s gives slow commands room.
            //
            // Note on .NET 8 Process.WaitForExitAsync: when stdout/stderr are
            // redirected, WaitForExitAsync waits for BOTH process exit AND
            // stream EOF. If the child exits cleanly but a grandchild inherits
            // stdout (common: child launches background daemons that outlive
            // it), WaitForExitAsync stalls on the stream half even though the
            // process is long dead -- 30s fires with stdoutDone=False and the
            // old code killed the (already-dead) tree and returned error.
            // Fix: on timeout, check proc.HasExited first. If the process has
            // exited, the child finished its work cleanly and we should return
            // the drained output as success, not a spurious error.
            using var subprocCts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            bool timedOut = false;
            try
            {
                await proc.WaitForExitAsync(subprocCts.Token);
                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} child exited code={proc.ExitCode} in {swStart.ElapsedMilliseconds}ms");
            }
            catch (OperationCanceledException)
            {
                timedOut = true;
            }

            if (timedOut)
            {
                // Give HasExited a moment to catch up with reality -- WaitForExitAsync
                // cancellation can fire milliseconds before .HasExited flips.
                try { proc.Refresh(); } catch { }
                bool alreadyExited = false;
                try { alreadyExited = proc.HasExited; } catch { }

                if (alreadyExited)
                {
                    // Process already exited; WaitForExitAsync was blocked on
                    // stdout/stderr drain because a grandchild inherited the
                    // pipe. Grant a short grace window for any in-flight reader
                    // bytes, then return whatever drained. This is success --
                    // the subprocess completed its work.
                    Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} child exited but readers pending (grandchild holds pipe); draining {proc.ExitCode}");
                    try { await Task.WhenAll(stdoutTask, stderrTask).WaitAsync(TimeSpan.FromSeconds(2)); } catch { }
                    return new EyeProxyResponse
                    {
                        Id = req.Id,
                        ExitCode = proc.ExitCode,
                        Stdout = stdoutTask.IsCompletedSuccessfully ? stdoutTask.Result : "",
                        Stderr = stderrTask.IsCompletedSuccessfully ? stderrTask.Result : "",
                    };
                }

                // True hang: process still running after 30s. Kill and report.
                bool stdoutDone = stdoutTask.IsCompleted;
                bool stderrDone = stderrTask.IsCompleted;
                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} TIMEOUT (30s) -- stdoutDone={stdoutDone} stderrDone={stderrDone} -- killing tree");
                try { proc.Kill(entireProcessTree: true); } catch { }
                try { await Task.WhenAll(stdoutTask, stderrTask).WaitAsync(TimeSpan.FromSeconds(2)); } catch { }
                return new EyeProxyResponse
                {
                    Id = req.Id,
                    ExitCode = -1,
                    Error = $"Subprocess timeout (30s) -- stdoutDone={stdoutDone} stderrDone={stderrDone}",
                    Stdout = stdoutTask.IsCompletedSuccessfully ? stdoutTask.Result : "",
                    Stderr = stderrTask.IsCompletedSuccessfully ? stderrTask.Result : "",
                };
            }

            // Child has exited cleanly. In .NET 8 WaitForExitAsync with redirected
            // streams already waited for reader EOF, so these should be complete.
            // Still cap with a grace timeout in case the runtime semantics ever
            // diverge or a grandchild is holding the pipe on a fast exit.
            var readDeadline = Task.Delay(TimeSpan.FromSeconds(3));
            var readDone = Task.WhenAll(stdoutTask, stderrTask);
            if (await Task.WhenAny(readDone, readDeadline) == readDeadline)
            {
                Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} stdout/stderr drain TIMEOUT (3s after child exit) -- grandchild may still hold pipe");
                // Grandchild holds pipe; return drained-so-far as success since
                // the main child exited cleanly. Do NOT tag this as error --
                // callers (auto-retry in ExecuteViaProxy) treat Error/ExitCode=-1
                // as "retry me" and that amplifies the original timeout.
                return new EyeProxyResponse
                {
                    Id = req.Id,
                    ExitCode = proc.ExitCode,
                    Stdout = stdoutTask.IsCompletedSuccessfully ? stdoutTask.Result : "",
                    Stderr = stderrTask.IsCompletedSuccessfully ? stderrTask.Result : "",
                };
            }

            return new EyeProxyResponse
            {
                Id = req.Id,
                ExitCode = proc.ExitCode,
                Stdout = stdoutTask.Result,
                Stderr = stderrTask.Result,
            };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE:ADMIN:PULSE] {reqTag} EXCEPTION {ex.GetType().Name}: {ex.Message}");
            return new EyeProxyResponse { Id = req.Id, ExitCode = -1, Error = ex.Message };
        }
    }
}

// -- Client (used by normal CLI to send commands to elevated Eye) --

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
    /// Caller should use a hard ceiling (e.g. 200ms) -- if admin Eye is in bad state,
    /// fall through to LaunchElevatedEye (sudo protection path) for recovery.
    /// </summary>
    public static bool Ping(int connectMs = 200)
    {
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            if (connectMs <= 0)
            {
                pipe.Connect();
                return pipe.IsConnected;
            }
            using var cts = new CancellationTokenSource(connectMs);
            pipe.ConnectAsync(connectMs, cts.Token).GetAwaiter().GetResult();
            return pipe.IsConnected;
        }
        catch { return false; }
    }

    /// <summary>Send a pre-built request to elevated Eye and get the result.</summary>
    public static async Task<EyeProxyResponse?> SendCommandAsync(
        EyeProxyRequest req, int timeoutMs = 5000)
    {
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", ElevatedEyeServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            CancellationToken ct;
            CancellationTokenSource? cts = null;
            if (timeoutMs > 0)
            {
                cts = new CancellationTokenSource(timeoutMs);
                ct = cts.Token;
                await pipe.ConnectAsync(timeoutMs, ct);
            }
            else
            {
                ct = CancellationToken.None;
                await Task.Run(() => pipe.Connect());
            }

            await EyeProxyWire.WriteAsync(pipe, req, ct);
            return await EyeProxyWire.ReadAsync<EyeProxyResponse>(pipe, ct);
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

    /// <summary>Send a command to elevated Eye and get the result.</summary>
    public static Task<EyeProxyResponse?> SendCommandAsync(
        string command, string[] args, int timeoutMs = 5000)
        => SendCommandAsync(new EyeProxyRequest
        {
            Id = Interlocked.Increment(ref _nextId),
            Command = command,
            Args = args,
            WorkingDir = Environment.CurrentDirectory,
        }, timeoutMs);

    /// <summary>
    /// Execute a command via elevated Eye proxy, printing stdout/stderr transparently.
    /// Returns exit code, or -1 if proxy unavailable / timed out.
    /// timeoutMs governs total pipe-round-trip budget (default 5000).
    /// Use short timeout (e.g. 1500ms) for fast-fail detection of hung admin Eye --
    /// caller then falls through to LaunchElevatedEye for recovery.
    /// </summary>
    public static int ExecuteViaProxy(string command, string[] args, int timeoutMs = 5000)
    {
        var id = Guid.NewGuid().ToString("N")[..12];
        var outPipeName = $"wkappbot_sudo_out_{id}";
        var errPipeName = $"wkappbot_sudo_err_{id}";

        using var outServer = new NamedPipeServerStream(outPipeName, PipeDirection.In, 1,
            PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
        using var errServer = new NamedPipeServerStream(errPipeName, PipeDirection.In, 1,
            PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

        // Relay tasks: pipe server → Console.Out / Console.Error (streaming, no buffer)
        var outRelay = Task.Run(async () =>
        {
            await outServer.WaitForConnectionAsync();
            await outServer.CopyToAsync(Console.OpenStandardOutput());
        });
        var errRelay = Task.Run(async () =>
        {
            await errServer.WaitForConnectionAsync();
            await errServer.CopyToAsync(Console.OpenStandardError());
        });

        // Response only carries exit code; output already streamed above.
        // Use long timeout: subprocess may run for tens of seconds.
        var streamReq = new EyeProxyRequest
        {
            Id = Interlocked.Increment(ref _nextId),
            Command = command,
            Args = args,
            WorkingDir = Environment.CurrentDirectory,
            StreamOutPipe = outPipeName,
            StreamErrPipe = errPipeName,
        };
        var resp = SendCommandAsync(streamReq, Math.Max(timeoutMs, 300_000)).GetAwaiter().GetResult();

        // Drain relay tasks (EOF received when admin Eye closes its pipe ends after subprocess exits).
        Task.WhenAll(outRelay, errRelay).Wait(TimeSpan.FromSeconds(5));

        // Auto-retry once on subprocess timeout. Admin Eye reports a busy-path
        // 50pct-ish timeout rate under load (the reported merged bug); one
        // extra round trip fixes most of them without bothering the user.
        // Protocol-level failures (resp == null) still fall through to the
        // caller's launch-new-proxy strategy.
        if (resp != null && resp.ExitCode == -1 && !string.IsNullOrEmpty(resp.Error)
            && resp.Error.Contains("Subprocess timeout", StringComparison.Ordinal))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[ELEVATION] Subprocess timeout -- auto-retry 1/1");
            Console.ResetColor();
            var retry = SendCommandAsync(command, args, timeoutMs).GetAwaiter().GetResult();
            if (retry != null) resp = retry;
        }

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

// -- Helpers --

static class ElevationHelper
{
    // UAC-storm guard: once any UAC attempt in this process fails (timeout,
    // STATUS_DLL_INIT_FAILED, pipe-zombie handshake miss, cancel), every
    // subsequent UAC attempt short-circuits to false. Heroes HTS and similar
    // elevated targets trigger the Readiness path which historically chained
    // SudoHandler -> LaunchElevatedEye -> runas-relaunch = 2-3 UAC dialogs
    // popping in a row for the same failure mode. One dialog + one clear
    // error is a much better experience than three.
    static int _uacFailedCount;

    public static bool UacAlreadyFailedThisProcess => Volatile.Read(ref _uacFailedCount) > 0;

    public static void MarkUacFailure(string caller, string reason)
    {
        var count = Interlocked.Increment(ref _uacFailedCount);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Error.WriteLine($"[ELEVATION:GUARD] UAC failure #{count} by {caller}: {reason}");
        Console.Error.WriteLine($"[ELEVATION:GUARD] further UAC attempts this process will be skipped");
        Console.ResetColor();
    }

    public static bool ShortCircuitIfUacFailed(string attempt)
    {
        if (!UacAlreadyFailedThisProcess) return false;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Error.WriteLine($"[ELEVATION:GUARD] skipping {attempt} -- UAC already failed once this process");
        Console.ResetColor();
        return true;
    }

    /// <summary>
    /// Sanitize TEMP/TMP/DOTNET_BUNDLE_EXTRACT_BASE_DIR before spawning an
    /// elevated child via ShellExecute(runas). ShellExecute inherits the
    /// caller's environment block, so Git Bash / MSYS2 "/tmp" leaks into
    /// the admin-child and breaks .NET 8 single-file bundle extract with
    /// STATUS_DLL_INIT_FAILED (0xC0000142) -- admin Eye dies before it
    /// can even reach Main(), pipe never comes up, caller hangs or falls
    /// back to more UAC prompts. Call this right before runas.
    /// </summary>
    public static void SanitizeEnvForElevatedSpawn()
    {
        try
        {
            var winDir = Environment.GetEnvironmentVariable("SystemRoot") ?? @"C:\Windows";
            var fallbackTemp = Path.Combine(winDir, "Temp");
            Directory.CreateDirectory(fallbackTemp);

            foreach (var name in new[] { "TEMP", "TMP" })
            {
                var cur = Environment.GetEnvironmentVariable(name);
                bool invalid = string.IsNullOrWhiteSpace(cur)
                    || cur!.StartsWith('/')
                    || !Directory.Exists(cur);
                if (invalid)
                {
                    Environment.SetEnvironmentVariable(name, fallbackTemp);
                    Console.Error.WriteLine($"[ELEVATION:ENV] {name}={cur ?? "(null)"} invalid -> {fallbackTemp}");
                }
            }

            // .NET 8 single-file bundle extract dir -- explicit override bypasses
            // any lingering resolution issues on the elevated child.
            var bundleDir = Path.Combine(fallbackTemp, "wkappbot-bundle");
            Directory.CreateDirectory(bundleDir);
            var curBundle = Environment.GetEnvironmentVariable("DOTNET_BUNDLE_EXTRACT_BASE_DIR");
            if (string.IsNullOrEmpty(curBundle) || !Directory.Exists(curBundle))
            {
                Environment.SetEnvironmentVariable("DOTNET_BUNDLE_EXTRACT_BASE_DIR", bundleDir);
                Console.Error.WriteLine($"[ELEVATION:ENV] DOTNET_BUNDLE_EXTRACT_BASE_DIR -> {bundleDir}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ELEVATION:ENV] sanitize failed: {ex.Message}");
        }
    }

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
                // Use Ping (actual connect) not IsAvailable (file exists) -- pipe path can
                // outlive a crashed admin Eye briefly, giving false "alive" on dead daemon.
                if (ElevatedEyeClient.Ping(100))
                {
                    Console.Error.WriteLine("[ELEVATION] Delegating via existing elevated Eye proxy pipe");
                    var exit = ElevatedEyeClient.ExecuteViaProxy(command, args);
                    Console.Error.WriteLine($"[ELEVATION] Proxy result: exit={exit}");
                    if (exit != -1) return (true, exit);
                    Console.Error.WriteLine("[ELEVATION] Proxy failed -- falling through");
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
                    Console.Error.WriteLine("[ELEVATION:EYE] Cannot elevate in Eye worker -- limited access");
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
        // Use window class name -- Process.GetProcessById fails on admin processes from non-admin
        var className = WindowFinder.GetClassName(targetHwnd);
        if (className.Contains("wkappbot", StringComparison.OrdinalIgnoreCase))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Error.WriteLine($"[ELEVATION] Target is wkappbot process (class={className}) -- skipping proxy delegation");
            Console.ResetColor();
            return (false, 0);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine($"[ELEVATION] Target (pid={pid}) is elevated -- requires admin proxy");
        Console.ResetColor();

        // Strategy 1: try existing Elevated Eye proxy
        // Ping (actual connect) avoids false-positive when pipe file lingers after admin crash.
        if (ElevatedEyeClient.Ping(100))
        {
            Console.WriteLine("[ELEVATION] Delegating via Elevated Eye proxy...");
            var exit1 = ElevatedEyeClient.ExecuteViaProxy(command, args);
            if (exit1 != -1) return (true, exit1);
            // Proxy connection failed -> fall through to Strategy 2
            Console.WriteLine("[ELEVATION] Existing proxy unreachable, launching new one...");
        }

        // Strategy 2: launch Elevated Eye, then delegate
        if (LaunchElevatedEye())
        {
            var exit2 = ElevatedEyeClient.ExecuteViaProxy(command, args);
            if (exit2 != -1) return (true, exit2);
        }

        // Strategy 3: fall through -- let caller proceed with limited access
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("[ELEVATION] Proxy unavailable -- continuing with limited access");
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
        // Already available? Ping is the authoritative check -- IsAvailable only tests
        // for the pipe file in the namespace, which can persist briefly after crash.
        if (ElevatedEyeClient.Ping(100)) return true;

        if (Program.IsMcpMode)
        {
            // Signal Launcher to spawn admin Core
            Console.Error.WriteLine("[ELEVATION] WaitForAdminServer: MCP mode -- signaling Launcher");
            Program.McpElevationRequired = true;
            return false; // Launcher will re-route
        }

        if (Program.RunningInEye)
        {
            Console.Error.WriteLine("[ELEVATION] WaitForAdminServer: Eye mode -- proxy unavailable");
            return false;
        }

        // Normal mode: launch elevated Eye
        return LaunchElevatedEye();
    }

    /// <summary>
    /// Launch Eye as admin with --elevated flag.
    /// Returns true if UAC was approved and Eye started.
    /// </summary>
    /// <summary>Alert user before elevation request -- wkappbot speak (fire-and-forget).</summary>
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
        if (ShortCircuitIfUacFailed($"LaunchElevatedEye({reason})")) return false;
        // Fix TMP/TEMP before runas -- see SanitizeEnvForElevatedSpawn comment.
        SanitizeEnvForElevatedSpawn();
        try
        {
            var exePath = Environment.ProcessPath ?? "wkappbot.exe";

            // -- Pre-spawn hot-swap: promote wkappbot-core.new.exe if pending --
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
                        Console.Error.WriteLine("[ELEVATION:HOT-SWAP] pending .new.exe detected -- promoting before admin spawn");
                        try { System.IO.File.Move(exePath, backupPath); } catch { /* already renamed */ }
                        System.IO.File.Move(newExePath, exePath);
                        Console.Error.WriteLine("[ELEVATION:HOT-SWAP] promoted -- admin Eye will run latest core");
                    }
                }
            }
            catch (Exception hsEx)
            {
                Console.Error.WriteLine($"[ELEVATION:HOT-SWAP] skipped: {hsEx.Message} -- proceeding with current exe");
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
                Console.Error.WriteLine("[ELEVATION] -- recent step trail --");
                foreach (var line in trail) Console.Error.WriteLine($"[ELEVATION]   {line}");
                Console.Error.WriteLine("[ELEVATION] ----------------------");
            }
            Console.ResetColor();

            System.Diagnostics.Process? proc = null;
            bool uacApproved = false;
            System.ComponentModel.Win32Exception? uacEx = null;
            try
            {
                proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "EYE-ELEVATED");
                uacApproved = proc != null;
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                // UAC cancelled or runas denied. Don't exit yet -- the UAC dialog time may
                // have overlapped with a pre-existing admin Eye still booting, so we still
                // owe the caller one handshake chance below.
                uacEx = ex;
            }

            // -- Post-UAC handshake (100ms) --
            // Admin Eye boot time and UAC dialog time tend to overlap. A pre-existing Eye
            // that started booting just before this call may be ready NOW. Regardless of
            // UAC outcome, give it exactly one 100ms Ping chance before we commit to either
            // our own spawned instance or a hard failure.
            if (ElevatedEyeClient.Ping(100))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine("[ELEVATION] admin Eye responsive after UAC wait -- reusing existing instance");
                Console.ResetColor();
                return true;
            }

            if (!uacApproved)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Error.WriteLine($"[ELEVATION] UAC cancelled ({uacEx?.NativeErrorCode}) -- no admin Eye available");
                Console.ResetColor();
                MarkUacFailure("LaunchElevatedEye", $"UAC cancelled err={uacEx?.NativeErrorCode}");
                return false;
            }

            // UAC approved + our admin Eye spawning. Poll for pipe readiness -- up to 10s.
            // Use Ping (actual connect) over IsAvailable (file exists) for authoritative liveness.
            for (int i = 0; i < 40; i++)
            {
                Thread.Sleep(250);
                if (ElevatedEyeClient.Ping(100))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[ELEVATION] Elevated Eye proxy is ready!");
                    Console.ResetColor();
                    return true;
                }
                // Bail early if the spawned admin Eye died (STATUS_DLL_INIT_FAILED etc.)
                // so we don't burn 10s of polling for a ghost. Mirrors SudoHandler fix d03182fc.
                try
                {
                    if (proc != null && proc.HasExited)
                    {
                        var hex = unchecked((uint)proc.ExitCode).ToString("X8");
                        var label = unchecked((uint)proc.ExitCode) == 0xC0000142u
                            ? "STATUS_DLL_INIT_FAILED (admin .NET init crash)"
                            : $"exit=0x{hex}";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine($"[ELEVATION] admin Eye died before pipe came up -- {label}");
                        Console.ResetColor();
                        MarkUacFailure("LaunchElevatedEye", $"admin Eye died {label}");
                        return false;
                    }
                }
                catch { /* handle race -- keep polling */ }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ELEVATION] Elevated Eye did not start in time (10s)");
            Console.ResetColor();
            MarkUacFailure("LaunchElevatedEye", "pipe didn't come up within 10s");
            return false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ELEVATION] Failed to launch elevated Eye: {ex.Message}");
            return false;
        }
    }
}

// -- Eye IPC: eye tick queries running Eye loop for cached state --
// Pipe name: wkappbot_eye_ipc  Request: {"cmd":"tick"}  Response: EyeIpcTickResponse JSON

/// <summary>Response from running Eye loop -> one-shot eye tick client.</summary>
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

/// <summary>
/// Simple tick-only IPC server that runs inside normal (non-elevated) Eye process.
/// Handles only __eye_tick__ queries on wkappbot_eye_ipc.
/// Real command proxy is handled exclusively by admin Eye on wkappbot_elevated.
/// Separation prevents normal Eye from intercepting elevated command connections.
/// </summary>
static class EyeIpcServer
{
    public static readonly string PipeName = $"wkappbot_eye_ipc_{ProjectRoot.Hash8()}";
    static readonly JsonSerializerOptions _opts = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task ListenAsync(CancellationToken ct)
    {
        Console.Error.WriteLine($"[EYE:IPC] Eye IPC server listening on \\\\.\\pipe\\{PipeName}");
        while (!ct.IsCancellationRequested)
        {
            NamedPipeServerStream? pipe = null;
            try
            {
                pipe = new NamedPipeServerStream(
                    PipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                await pipe.WaitForConnectionAsync(ct);
                var connectedPipe = pipe;
                pipe = null;
                _ = Task.Run(async () =>
                {
                    try { await HandleTickClient(connectedPipe, ct); }
                    finally { connectedPipe.Dispose(); }
                }, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE:IPC] Error: {ex.Message}");
                try { await Task.Delay(TimeSpan.FromSeconds(5), ct); }
                catch (OperationCanceledException) { break; }
            }
            finally { pipe?.Dispose(); }
        }
        Console.Error.WriteLine("[EYE:IPC] Eye IPC server stopped");
    }

    static async Task HandleTickClient(NamedPipeServerStream pipe, CancellationToken ct)
    {
        try
        {
            var req = await EyeProxyWire.ReadAsync<EyeProxyRequest>(pipe, ct);
            if (req == null) return;
            if (req.Command == "__eye_tick__")
            {
                Program.EnsureEyeVisible(); // restore opacity/topmost/position on every tick query
                var tickResp = Program.BuildIpcTickResponse();
                var tickJson = JsonSerializer.Serialize(tickResp, _opts);
                var resp = new EyeProxyResponse { Id = req.Id, ExitCode = 0, Stdout = tickJson };
                await EyeProxyWire.WriteAsync(pipe, resp, ct);
            }
        }
        catch { }
    }
}

/// <summary>Client used by one-shot eye tick to query the running Eye loop's cached state.</summary>
static class EyeIpcClient
{
    static readonly JsonSerializerOptions _opts = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task<EyeIpcTickResponse?> QueryTickAsync(int timeoutMs = 2000)
    {
        // Connect to normal Eye's dedicated IPC pipe, separate from the elevated command proxy.
        try
        {
            using var pipe = new NamedPipeClientStream(
                ".", EyeIpcServer.PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            using var cts = new CancellationTokenSource(timeoutMs);
            await pipe.ConnectAsync(timeoutMs, cts.Token);
            var req = new EyeProxyRequest { Id = 1, Command = "__eye_tick__", Args = [] };
            await EyeProxyWire.WriteAsync(pipe, req, cts.Token);
            var resp = await EyeProxyWire.ReadAsync<EyeProxyResponse>(pipe, cts.Token);
            if (resp == null || resp.ExitCode != 0 || string.IsNullOrEmpty(resp.Stdout)) return null;
            return JsonSerializer.Deserialize<EyeIpcTickResponse>(resp.Stdout, _opts);
        }
        catch { return null; }
    }
}
