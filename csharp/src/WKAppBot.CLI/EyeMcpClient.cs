using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

/// <summary>
/// Eye MCP Client — routes a11y operations to a persistent wkappbot-core.exe mcp subprocess.
/// All UIA/Win32 memory stays in the subprocess; Eye stays lean.
///
/// Protocol: JSON-RPC 2.0 over stdin/stdout pipes (same as McpCommand server).
/// Uses "wkappbot_cli" tool with argv array for maximum flexibility.
///
/// Subprocess is spawned with DETACHED_PROCESS flag (CreateProcessW) to avoid
/// ConPTY/LPC deadlock in UIA calls — same pattern as Launcher.RunCoreDetachedNormal().
///
/// Thread-safe: multiple Eye tasks can call concurrently.
/// Auto-restart: subprocess crash → restart (max 5 within 5min).
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
    /// <summary>Per-call caller context — set by EyeCmdPipeServer before CallAsync.</summary>
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
            Console.Error.WriteLine($"[TIP] MCP timeout — command may have succeeded. Rerun or check target to confirm.");
            return ($"[EYE-MCP] Timeout after {timeoutMs}ms: {string.Join(" ", argv)}", 1);
        }
        catch (Exception ex)
        {
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

    // ── P/Invoke for DETACHED_PROCESS spawn ─────────────────

    const uint DETACHED_PROCESS = 0x00000008;
    const uint CREATE_BREAKAWAY_FROM_JOB = 0x01000000;
    const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;
    const uint STARTF_USESTDHANDLES = 0x00000100;
    const uint HANDLE_FLAG_INHERIT = 0x00000001;

    [StructLayout(LayoutKind.Sequential)]
    struct SECURITY_ATTRIBUTES { public int nLength; public IntPtr lpSecurityDescriptor; public bool bInheritHandle; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct STARTUPINFOW
    {
        public int cb; public IntPtr lpReserved, lpDesktop, lpTitle;
        public int dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute;
        public uint dwFlags; public short wShowWindow; public short cbReserved2;
        public IntPtr lpReserved2, hStdInput, hStdOutput, hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct PROCESS_INFORMATION { public IntPtr hProcess, hThread; public uint dwProcessId, dwThreadId; }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, ref SECURITY_ATTRIBUTES lpPipeAttributes, uint nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, IntPtr lpPipeAttributes, uint nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetHandleInformation(IntPtr hObject, uint dwMask, uint dwFlags);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CreateProcessW(
        string? lpApplicationName, char[] lpCommandLine,
        IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
        [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment, string? lpCurrentDirectory,
        ref STARTUPINFOW lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    // ── Private ──────────────────────────────────────────────

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
            Console.Error.WriteLine("[EYE-MCP] Max restarts (5/5min) exceeded — MCP disabled");
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
            // Create pipes with inheritable handles for the MCP-side
            var sa = new SECURITY_ATTRIBUTES { nLength = Marshal.SizeOf<SECURITY_ATTRIBUTES>(), bInheritHandle = true };

            CreatePipe(out var hStdinRead, out var hStdinWrite, ref sa, 0);
            CreatePipe(out var hStdoutRead, out var hStdoutWrite, ref sa, 0);
            CreatePipe(out var hStderrRead, out var hStderrWrite, ref sa, 0);

            // Eye-side handles must NOT be inherited (prevent handle leak)
            SetHandleInformation(hStdinWrite, HANDLE_FLAG_INHERIT, 0);
            SetHandleInformation(hStdoutRead, HANDLE_FLAG_INHERIT, 0);
            SetHandleInformation(hStderrRead, HANDLE_FLAG_INHERIT, 0);

            // Build environment block (Unicode, remove PTY vars)
            var envBlock = BuildDetachedEnvBlock();

            var si = new STARTUPINFOW();
            si.cb = Marshal.SizeOf<STARTUPINFOW>();
            si.dwFlags = STARTF_USESTDHANDLES;
            si.hStdInput = hStdinRead;
            si.hStdOutput = hStdoutWrite;
            si.hStdError = hStderrWrite;

            var cmdLine = ($"\"{exe}\" mcp" + '\0').ToCharArray();
            var cwd = Environment.CurrentDirectory;
            Console.Error.WriteLine($"[EYE-MCP] CreateProcessW: exe={exe} cwd={cwd} si.cb={si.cb} flags=0x{(DETACHED_PROCESS | CREATE_UNICODE_ENVIRONMENT):X}");
            bool ok = CreateProcessW(
                null, cmdLine,
                IntPtr.Zero, IntPtr.Zero,
                true, // bInheritHandles
                DETACHED_PROCESS | CREATE_UNICODE_ENVIRONMENT, // no CREATE_BREAKAWAY_FROM_JOB (ACCESS_DENIED in some job contexts)
                envBlock, cwd,
                ref si, out var pi);
            Console.Error.WriteLine($"[EYE-MCP] CreateProcessW result: ok={ok} pid={pi.dwProcessId} err={Marshal.GetLastWin32Error()}");

            // Free environment block
            if (envBlock != IntPtr.Zero) Marshal.FreeHGlobal(envBlock);

            // Close MCP-side pipe handles (we keep Eye-side only)
            CloseHandle(hStdinRead);
            CloseHandle(hStdoutWrite);
            CloseHandle(hStderrWrite);

            if (!ok)
            {
                var err = Marshal.GetLastWin32Error();
                Console.Error.WriteLine($"[EYE-MCP] CreateProcessW failed: win32err={err} exe={exe} si.cb={si.cb} sizeof={Marshal.SizeOf<STARTUPINFOW>()}");
                CloseHandle(hStdinWrite);
                CloseHandle(hStdoutRead);
                CloseHandle(hStderrRead);
                return;
            }

            CloseHandle(pi.hThread);

            // Wrap Win32 handles as .NET streams
            var stdinStream = new FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdinWrite, true),
                FileAccess.Write);
            var stdoutStream = new FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStdoutRead, true),
                FileAccess.Read);
            var stderrStream = new FileStream(new Microsoft.Win32.SafeHandles.SafeFileHandle(hStderrRead, true),
                FileAccess.Read);

            _process = Process.GetProcessById((int)pi.dwProcessId);
            _stdin = new StreamWriter(stdinStream, new UTF8Encoding(false)) { AutoFlush = true };
            _stdout = new StreamReader(stdoutStream, Encoding.UTF8);

            // Pump stderr (prefixed)
            var stderrReader = new StreamReader(stderrStream, Encoding.UTF8);
            _ = Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        var line = await stderrReader.ReadLineAsync();
                        if (line == null) break;
                        Console.Error.WriteLine($"[MCP-W] {line}");
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
            Console.Error.WriteLine($"[EYE-MCP] Started PID={(int)pi.dwProcessId} (DETACHED_PROCESS, restart #{_restartCount})");

            // Close process handle (Process object tracks it)
            CloseHandle(pi.hProcess);
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
            Console.Error.WriteLine("[EYE-MCP] Subprocess exited — completing pending calls with error");
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
