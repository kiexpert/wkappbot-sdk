using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using STARTUPINFOW = AppBotPipe.STARTUPINFOW;
using PROCESS_INFORMATION = AppBotPipe.PROCESS_INFORMATION;

namespace WKAppBot.CLI;

/// <summary>
/// Eye MCP Client -- routes a11y operations to a persistent wkappbot-core.exe mcp subprocess.
/// All UIA/Win32 memory stays in the subprocess; Eye stays lean.
///
/// Protocol: JSON-RPC 2.0 over stdin/stdout pipes (same as McpCommand server).
/// Uses "wkappbot_cli" tool with argv array for maximum flexibility.
///
/// Subprocess is spawned with DETACHED_PROCESS flag (CreateProcessW) to avoid
/// ConPTY/LPC deadlock in UIA calls -- same pattern as Launcher.RunCoreDetachedNormal().
///
/// Thread-safe: multiple Eye tasks can call concurrently.
/// Auto-restart: subprocess crash -> restart (max 5 within 5min).
/// </summary>
internal static class EyeMcpClient
{
    private static Process? _process;
    private static StreamWriter? _stdin;
    private static StreamReader? _stdout;
    private static readonly object _writeLock = new();
    private static readonly ConcurrentDictionary<long, TaskCompletionSource<JsonNode?>> _pending = new();
    private static long _nextId;
    private static int _restartCount;
    private static DateTime _restartWindowStart = DateTime.UtcNow;
    private static readonly SemaphoreSlim _startLock = new(1, 1);
    private static CancellationTokenSource? _readerCts;
    private static Task? _readerTask;
    private static volatile bool _stopping;

    /// <summary>True when MCP subprocess is running and ready.</summary>
    internal static bool IsRunning => _process is { HasExited: false };

    /// <summary>Spawn MCP subprocess and send initialize handshake.</summary>
    internal static void Start()
    {
        _stopping = false;
        _restartCount = 0;
        _restartWindowStart = DateTime.UtcNow;
        EnsureStarted();
    }

    /// <summary>Kill subprocess and cancel all pending calls.</summary>
    internal static void Stop()
    {
        _stopping = true;
        _readerCts?.Cancel();

        try { _process?.Kill(); } catch { }
        try { _process?.Dispose(); } catch { }
        _process = null;
        _stdin = null;
        _stdout = null;

        // Complete all pending with cancellation
        foreach (var kv in _pending)
        {
            kv.Value.TrySetCanceled();
            _pending.TryRemove(kv.Key, out _);
        }

        Console.Error.WriteLine("[EYE-MCP] Stopped");
    }

    /// <summary>
    /// Call a CLI command through MCP and wait for the result.
    /// argv: e.g. ["a11y","invoke","*Button*"] or ["prompt","send","name","text"]
    /// </summary>
    /// <summary>Per-call caller context -- set by EyeCmdPipeServer before CallAsync.</summary>
    internal static string? CurrentCallerCwd;
    internal static string? CurrentCallerHwnd;

    internal static async Task<(string output, int exitCode)> CallAsync(string[] argv, int timeoutMs = 60_000)
    {
        EnsureStarted();
        if (_stdin == null)
            return ("[EYE-MCP] Not connected", 1);

        var id = Interlocked.Increment(ref _nextId);
        var tcs = new TaskCompletionSource<JsonNode?>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pending[id] = tcs;

        try
        {
            // Per-call caller context in _meta (CWD/HWND vary per call)
            var meta = new JsonObject();
            if (CurrentCallerCwd != null) meta["callerCwd"] = CurrentCallerCwd;
            if (CurrentCallerHwnd != null) meta["callerHwnd"] = CurrentCallerHwnd;

            var request = new JsonObject
            {
                ["jsonrpc"] = "2.0",
                ["id"] = id,
                ["method"] = "tools/call",
                ["params"] = new JsonObject
                {
                    ["name"] = "wkappbot_cli",
                    ["arguments"] = new JsonObject
                    {
                        ["argv"] = new JsonArray(argv.Select(a => (JsonNode)JsonValue.Create(a)!).ToArray())
                    },
                    ["_meta"] = meta
                }
            };

            var json = request.ToJsonString();
            lock (_writeLock)
            {
                _stdin?.WriteLine(json);
            }

            using var cts = new CancellationTokenSource(timeoutMs);
            cts.Token.Register(() => tcs.TrySetCanceled());

            var result = await tcs.Task;
            return ParseResult(result);
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine($"[TIP] MCP timeout -- command may have succeeded. Rerun or check target to confirm.");
            return ($"[EYE-MCP] Timeout after {timeoutMs}ms: {string.Join(" ", argv)}", 1);
        }
        catch (Exception ex)
        {
            // Auto-restart MCP worker on pipe errors (hot-swap, process crash)
            if (ex is IOException or ObjectDisposedException)
            {
                Console.Error.WriteLine($"[EYE-MCP] Pipe broken ({ex.GetType().Name}) -- restarting MCP worker");
                try { _process?.Kill(); } catch { }
                _process = null; _stdin = null;
                EnsureStarted(); // respawn
            }
            return ($"[EYE-MCP] Error: {ex.Message}", 1);
        }
        finally
        {
            _pending.TryRemove(id, out _);
        }
    }

    /// <summary>Send a CLI command and don't wait for the result.</summary>
    internal static void CallFireAndForget(string[] argv)
    {
        EnsureStarted();
        if (_stdin == null) return;

        var id = Interlocked.Increment(ref _nextId);
        _pending[id] = new TaskCompletionSource<JsonNode?>(TaskCreationOptions.RunContinuationsAsynchronously);

        var request = new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = id,
            ["method"] = "tools/call",
            ["params"] = new JsonObject
            {
                ["name"] = "wkappbot_cli",
                ["arguments"] = new JsonObject
                {
                    ["argv"] = new JsonArray(argv.Select(a => (JsonNode)JsonValue.Create(a)!).ToArray())
                }
            }
        };

        var json = request.ToJsonString();
        lock (_writeLock)
        {
            try { _stdin?.WriteLine(json); } catch { }
        }

        _ = Task.Delay(300_000).ContinueWith(_ =>
        {
            if (_pending.TryRemove(id, out var tcs))
                tcs.TrySetCanceled();
        });
    }

    // -- P/Invoke for DETACHED_PROCESS spawn ----------------─
    // Structs + guard: AppBotPipe.cs (shared between Launcher and Core)

    const uint DETACHED_PROCESS = AppBotPipe.DETACHED_PROCESS;
    const uint CREATE_UNICODE_ENVIRONMENT = AppBotPipe.CREATE_UNICODE_ENVIRONMENT;
    const uint STARTF_USESTDHANDLES = AppBotPipe.STARTF_USESTDHANDLES;
    const uint HANDLE_FLAG_INHERIT = AppBotPipe.HANDLE_FLAG_INHERIT;

    [StructLayout(LayoutKind.Sequential)]
    struct SECURITY_ATTRIBUTES { public int nLength; public IntPtr lpSecurityDescriptor; public bool bInheritHandle; }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, ref SECURITY_ATTRIBUTES lpPipeAttributes, uint nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, IntPtr lpPipeAttributes, uint nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetHandleInformation(IntPtr hObject, uint dwMask, uint dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    // -- Private ----------------------------------------------

    private static void EnsureStarted()
    {
        if (IsRunning) return;
        if (_stopping) return;

        _startLock.Wait();
        try
        {
            if (IsRunning) return;
            SpawnSubprocess();
        }
        finally
        {
            _startLock.Release();
        }
    }

    private static void SpawnSubprocess()
    {
        if ((DateTime.UtcNow - _restartWindowStart).TotalMinutes > 5)
        {
            _restartCount = 0;
            _restartWindowStart = DateTime.UtcNow;
        }
        if (_restartCount >= 5)
        {
            Console.Error.WriteLine("[EYE-MCP] Max restarts (5/5min) exceeded -- MCP disabled");
            return;
        }

        var exe = Environment.ProcessPath;
        if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
        {
            Console.Error.WriteLine($"[EYE-MCP] Cannot find executable: {exe}");
            return;
        }

        try
        {
            var envBlock = BuildDetachedEnvBlock();
            var cwd = Program.EyeCallerCwd.Length > 0 ? Program.EyeCallerCwd : Environment.CurrentDirectory;
            bool ok = AppBotPipe.SpawnMcp(exe, cwd, envBlock,
                out int mcpPid, out var stdinStream, out var stdoutStream, out var stderrStream);
            if (envBlock != IntPtr.Zero) Marshal.FreeHGlobal(envBlock);

            if (!ok)
            {
                Console.Error.WriteLine($"[EYE-MCP] SpawnMcp failed: exe={exe} cwd={cwd}");
                return;
            }

            _process = Process.GetProcessById(mcpPid);
            _stdin = new StreamWriter(stdinStream!, new UTF8Encoding(false)) { AutoFlush = true };
            _stdout = new StreamReader(stdoutStream!, Encoding.UTF8);

            // Pump stderr (prefixed) -- write raw UTF-8 bytes to preserve Korean and ANSI codes.
            // Console.Error.WriteLine() may transcode through CP949 on Windows, garbling UTF-8 output.
            var stderrReader = new StreamReader(stderrStream!, Encoding.UTF8);
            var stderrRawOut = Console.OpenStandardError();
            var stderrUtf8 = new UTF8Encoding(false);
            var stderrPrefix = stderrUtf8.GetBytes("[MCP-W] ");
            var stderrNewline = stderrUtf8.GetBytes(Environment.NewLine);
            _ = Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        var line = await stderrReader.ReadLineAsync();
                        if (line == null) break;
                        // Write as raw UTF-8 bytes to preserve ANSI escape codes and Korean chars
                        stderrRawOut.Write(stderrPrefix);
                        stderrRawOut.Write(stderrUtf8.GetBytes(line));
                        stderrRawOut.Write(stderrNewline);
                        stderrRawOut.Flush();
                    }
                }
                catch { }
            });

            // Send initialize handshake
            var initRequest = new JsonObject
            {
                ["jsonrpc"] = "2.0",
                ["id"] = Interlocked.Increment(ref _nextId),
                ["method"] = "initialize",
                ["params"] = new JsonObject
                {
                    ["protocolVersion"] = "2024-11-05",
                    ["capabilities"] = new JsonObject(),
                    ["clientInfo"] = new JsonObject
                    {
                        ["name"] = "EyeMcpClient",
                        ["version"] = "1.0"
                    }
                }
            };

            lock (_writeLock)
            {
                _stdin.WriteLine(initRequest.ToJsonString());
            }

            // Start reader loop
            _readerCts?.Cancel();
            _readerCts = new CancellationTokenSource();
            _readerTask = Task.Run(() => ReaderLoop(_readerCts.Token));

            _restartCount++;
            Console.Error.WriteLine($"[EYE-MCP] Started PID={mcpPid} (DETACHED_PROCESS, restart #{_restartCount})");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE-MCP] Spawn error: {ex.Message}");
        }
    }

    /// <summary>Build Unicode environment block with PTY vars removed.</summary>
    private static IntPtr BuildDetachedEnvBlock()
    {
        var removeVars = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL", "ConEmuANSI",
            "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST", "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE"
        };

        var sb = new StringBuilder();
        foreach (System.Collections.DictionaryEntry entry in Environment.GetEnvironmentVariables())
        {
            var key = entry.Key?.ToString() ?? "";
            if (removeVars.Contains(key)) continue;
            sb.Append(key).Append('=').Append(entry.Value?.ToString() ?? "").Append('\0');
        }
        // Set default caller name for MCP worker (process-level, used as fallback)
        var defaultName = Program.GetSendReplyUsername();
        if (!string.IsNullOrEmpty(defaultName))
            sb.Append("WKAPPBOT_CALLER_NAME=").Append(defaultName).Append('\0');
        sb.Append('\0'); // double-null terminator

        var bytes = Encoding.Unicode.GetBytes(sb.ToString());
        var ptr = Marshal.AllocHGlobal(bytes.Length);
        Marshal.Copy(bytes, 0, ptr, bytes.Length);
        return ptr;
    }

    private static async Task ReaderLoop(CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested && _stdout != null)
            {
                var line = await _stdout.ReadLineAsync(ct);
                if (line == null) break;

                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var response = JsonNode.Parse(line);
                    if (response == null) continue;

                    var idNode = response["id"];
                    if (idNode != null)
                    {
                        var id = idNode.GetValue<long>();
                        if (_pending.TryRemove(id, out var tcs))
                        {
                            var result = response["result"];
                            var error = response["error"];
                            if (error != null)
                                tcs.TrySetException(new Exception(error["message"]?.GetValue<string>() ?? "MCP error"));
                            else
                                tcs.TrySetResult(result);
                        }
                    }
                }
                catch (JsonException) { }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE-MCP] Reader error: {ex.Message}");
        }

        if (!_stopping)
        {
            Console.Error.WriteLine("[EYE-MCP] Subprocess exited -- completing pending calls with error");
            foreach (var kv in _pending)
            {
                kv.Value.TrySetException(new Exception("MCP subprocess exited"));
                _pending.TryRemove(kv.Key, out _);
            }

            if (!_stopping)
            {
                await Task.Delay(2000);
                if (!_stopping)
                {
                    Console.Error.WriteLine("[EYE-MCP] Auto-restarting...");
                    EnsureStarted();
                }
            }
        }
    }

    private static (string output, int exitCode) ParseResult(JsonNode? result)
    {
        if (result == null)
            return ("", 0);

        var isError = result["isError"]?.GetValue<bool>() == true;
        var content = result["content"] as JsonArray;
        if (content == null)
            return (result.ToJsonString(), isError ? 1 : 0);

        var sb = new StringBuilder();
        foreach (var item in content)
        {
            if (item?["type"]?.GetValue<string>() == "text")
            {
                if (sb.Length > 0) sb.AppendLine();
                sb.Append(item["text"]?.GetValue<string>() ?? "");
            }
        }

        return (sb.ToString(), isError ? 1 : 0);
    }
}
