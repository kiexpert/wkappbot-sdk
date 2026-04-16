using System.Diagnostics;
using System.Text;
using STARTUPINFOW = AppBotPipe.STARTUPINFOW;
using PROCESS_INFORMATION = AppBotPipe.PROCESS_INFORMATION;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// MCP stdio proxy loop.
    /// Launcher holds the stdio pipe to Claude Code permanently.
    /// Core runs behind the proxy — if it exits with code 42, it is restarted automatically.
    /// stdin broadcaster routes bytes to whichever Core instance is current.
    /// </summary>
    static int RunMcpProxy(string[] args)
    {
        var dir  = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = ResolveCoreExe();

        if (!File.Exists(core))
        {
            Console.Error.WriteLine($"[LAUNCHER:MCP] wkappbot-core.exe not found at: {core}");
            return 1;
        }

        int timeoutSec  = 0;
        int timeoutExit = 2;
        bool showWt     = args.Any(a => a == "--wt");
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--timeout"      && int.TryParse(args[i + 1], out var t) && t > 0) timeoutSec  = t;
            if (args[i] == "--timeout-exit" && int.TryParse(args[i + 1], out var e))           timeoutExit = e;
        }

        // --wt: redirect Core stderr to a temp log and open a Windows Terminal tab tailing it
        string? wtLogFile = null;
        if (showWt)
        {
            wtLogFile = Path.Combine(Path.GetTempPath(), $"wkappbot-mcp-{Environment.ProcessId}.log");
            File.WriteAllText(wtLogFile, ""); // create empty log
            var wtTitle = $"WKAppBot MCP ({Environment.ProcessId})";
            var psCmd = $"Get-Content -Wait -Path '{wtLogFile}'";
            try
            {
                // [FOCUS-GUARD] Capture foreground before wt.exe launch; restore if stolen.
                var fgBeforeWt = FocusGuard.GetForegroundWindow();
                Process.Start(new ProcessStartInfo
                {
                    FileName        = "wt.exe",
                    Arguments       = $"--window 0 new-tab --title \"{wtTitle}\" -- powershell -NoExit -Command \"{psCmd}\"",
                    UseShellExecute = true,
                });
                Console.Error.WriteLine($"[LAUNCHER:MCP] WT monitor tab opened → {wtLogFile}");
                Thread.Sleep(600);
                if (fgBeforeWt != IntPtr.Zero && FocusGuard.GetForegroundWindow() != fgBeforeWt)
                {
                    Console.Error.WriteLine("[LAUNCHER:MCP] wt.exe stole focus — restoring");
                    FocusGuard.SetForegroundWindow(fgBeforeWt);
                }
            }
            catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER:MCP] wt.exe failed: {ex.Message}"); wtLogFile = null; }
        }

        var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");

        // MCP mode: pipe-based relay (same pattern as Eye↔Core).
        // ConPTY doesn't pass stdin to grandchild, so we pipe explicitly:
        //   Claude Code → ConPTY → Launcher (ReadFile stdin → WriteFile pipe) → Core
        //   Core → pipe → Launcher (ReadFile pipe → WriteFile stdout) → ConPTY → Claude Code

        // Create stdin pipe: Launcher writes → Core reads
        if (!CreatePipe(out var hCoreStdinRead, out var hCoreStdinWrite, IntPtr.Zero, 0))
        { Console.Error.WriteLine("[LAUNCHER:MCP] CreatePipe(stdin) failed"); return 1; }
        SetHandleInformation(hCoreStdinRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

        // Create stdout pipe: Core writes → Launcher reads
        if (!CreatePipe(out var hCoreStdoutRead, out var hCoreStdoutWrite, IntPtr.Zero, 0))
        { CloseHandle(hCoreStdinRead); CloseHandle(hCoreStdinWrite); Console.Error.WriteLine("[LAUNCHER:MCP] CreatePipe(stdout) failed"); return 1; }
        SetHandleInformation(hCoreStdoutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

        var cmdSb = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmdSb.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        var cmdArr = (cmdSb.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW
        {
            cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>(),
            dwFlags = STARTF_USESTDHANDLES,
            hStdInput  = hCoreStdinRead,   // Core reads from this pipe
            hStdOutput = hCoreStdoutWrite,  // Core writes to this pipe
            hStdError  = GetStdHandle(-12), // stderr inherited directly
        };
        if (!CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, true,
            CREATE_UNICODE_ENVIRONMENT, IntPtr.Zero, Environment.CurrentDirectory, ref si, out var pi))
        {
            Console.Error.WriteLine($"[LAUNCHER:MCP] CreateProcess failed (err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()})");
            CloseHandle(hCoreStdinRead); CloseHandle(hCoreStdinWrite);
            CloseHandle(hCoreStdoutRead); CloseHandle(hCoreStdoutWrite);
            return 1;
        }
        // Close child-side handles in parent
        CloseHandle(hCoreStdinRead);
        CloseHandle(hCoreStdoutWrite);
        CloseHandle(pi.hThread);
        Console.Error.WriteLine($"[LAUNCHER:MCP] core started via pipe (pid={pi.dwProcessId})");

        // ── State machine for MCP relay ──
        // Tracks JSON-RPC requests by id for routing during elevation handoff AND hot-swap
        var _inflight = new Dictionary<string, string>(); // id → raw request line
        var _gate = new object();
        var _outLock = new object();
        string? _initializeLine = null; // cached for admin/new Core synthetic init
        string? _toolsListLine = null;  // cached tools/list for new Core replay
        IntPtr _activeStdinWrite = hCoreStdinWrite; // current target for new requests
        System.IO.TextWriter? _activeStdinWriter = null; // current line writer after admin handoff
        IntPtr _adminStdinWrite = IntPtr.Zero;
        IntPtr _adminStdoutRead = IntPtr.Zero;
        IntPtr _adminProc = IntPtr.Zero;
        string? _elevationRequestLine = null; // saved for re-send to admin Core
        bool _adminMode = false;

        // stdout handle (needed early for hot-swap FSW closure)
        var hOut = GetStdHandle(-11);

        // ── Hot-swap: watch the original core path only; Eye owns .new.exe staging/rename ──
        IntPtr _oldCoreStdinWrite = IntPtr.Zero; // drain pipe for old Core during swap
        IntPtr _oldCoreStdoutRead = IntPtr.Zero;
        var _oldInflight = new Dictionary<string, string>(); // requests still in old Core
        bool _swapInProgress = false;
        bool _pendingSwapWhileAdmin = false;
        var _activeCoreStamp = System.IO.File.Exists(core) ? System.IO.File.GetLastWriteTimeUtc(core).Ticks.ToString() : string.Empty;
        string? _lastFailedSwapStamp = null;
        var _swapDrainEvent = new ManualResetEventSlim(false); // signaled when old Core drain completes

        var exePath = core; // path to wkappbot-core.exe
        var exeDir = System.IO.Path.GetDirectoryName(exePath) ?? ".";
        var exeName = System.IO.Path.GetFileName(exePath);
        var newExePath = System.IO.Path.Combine(exeDir, System.IO.Path.GetFileNameWithoutExtension(exeName) + ".new.exe");

        // FSW for core-path replacement/change detection (500ms debounce, same as Eye)
        System.Threading.Timer? _fswDebounce = null;
        var fsw = new FileSystemWatcher(exeDir)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.LastWrite,
            Filter = "*core*.exe",
            EnableRaisingEvents = true
        };
        // Trigger on: core exe replaced at original path (Eye rename-swap or external overwrite)
        fsw.Created += OnFswEvent;
        fsw.Changed += OnFswEvent;
        void OnFswEvent(object? _, FileSystemEventArgs e)
        {
            var fn = e.Name ?? "";
            // Detect: core exe was replaced at original path (Eye did rename-swap, or external tool replaced it)
            bool isCoreReplaced = string.Equals(System.IO.Path.GetFullPath(e.FullPath), System.IO.Path.GetFullPath(core), StringComparison.OrdinalIgnoreCase);
            if (!isCoreReplaced) return;
            // Debounce: 500ms after last event
            _fswDebounce?.Dispose();
            _fswDebounce = new System.Threading.Timer(_ =>
            {
                lock (_gate)
                {
                    if (_adminMode || _activeStdinWriter != null)
                    {
                        _pendingSwapWhileAdmin = true;
                        Console.Error.WriteLine("[HOT-SWAP] admin endpoint active — deferring normal core swap");
                        return;
                    }
                }
                var currentSwapStamp = System.IO.File.Exists(core) ? System.IO.File.GetLastWriteTimeUtc(core).Ticks.ToString() : string.Empty;
                if (currentSwapStamp.Length == 0)
                {
                    Console.Error.WriteLine("[HOT-SWAP] core path changed but stamp is unreadable — ignoring");
                    return;
                }
                if (currentSwapStamp == _activeCoreStamp)
                {
                    Console.Error.WriteLine($"[HOT-SWAP] same active stamp ({currentSwapStamp}) — ignoring");
                    return;
                }
                if (currentSwapStamp == _lastFailedSwapStamp)
                {
                    Console.Error.WriteLine($"[HOT-SWAP] previously failed stamp ({currentSwapStamp}) — waiting for newer core");
                    return;
                }
                if (_swapInProgress) { Console.Error.WriteLine("[HOT-SWAP] Already in progress — ignoring"); return; }
                _swapInProgress = true;
                Console.Error.WriteLine($"[HOT-SWAP] core exe changed — starting graceful swap");

                try
                {
                    // External tool (Eye or publish) already did the rename-swap.
                    // We just need to drain old Core and spawn new one from the updated path.

                    // 2. Move current Core to old pipe (drain mode)
                    lock (_gate)
                    {
                        _oldCoreStdinWrite = _activeStdinWrite;
                        _activeStdinWrite = IntPtr.Zero;
                        _activeStdinWriter = null;
                        // Move inflight to old
                        foreach (var kv in _inflight)
                            _oldInflight[kv.Key] = kv.Value;
                        _inflight.Clear();
                    }
                    Console.Error.WriteLine($"[HOT-SWAP] old Core draining ({_oldInflight.Count} inflight)");

                    // 3. Spawn new Core with fresh pipes
                    if (!CreatePipe(out var hNewStdinRead, out var hNewStdinWrite, IntPtr.Zero, 0))
                    { Console.Error.WriteLine("[HOT-SWAP] CreatePipe(stdin) failed"); _lastFailedSwapStamp = currentSwapStamp; _swapInProgress = false; return; }
                    SetHandleInformation(hNewStdinRead, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

                    if (!CreatePipe(out var hNewStdoutRead, out var hNewStdoutWrite, IntPtr.Zero, 0))
                    { CloseHandle(hNewStdinRead); CloseHandle(hNewStdinWrite); Console.Error.WriteLine("[HOT-SWAP] CreatePipe(stdout) failed"); _lastFailedSwapStamp = currentSwapStamp; _swapInProgress = false; return; }
                    SetHandleInformation(hNewStdoutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

                    var newSi = new STARTUPINFOW
                    {
                        cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>(),
                        dwFlags = STARTF_USESTDHANDLES,
                        hStdInput = hNewStdinRead,
                        hStdOutput = hNewStdoutWrite,
                        hStdError = GetStdHandle(-12),
                    };
                    var newCmdSb = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
                    foreach (var a in args) newCmdSb.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
                    var newCmdArr = (newCmdSb.ToString() + "\0").ToCharArray();

                    if (!CreateProcessW(null, newCmdArr, IntPtr.Zero, IntPtr.Zero, true,
                        CREATE_UNICODE_ENVIRONMENT, IntPtr.Zero, Environment.CurrentDirectory, ref newSi, out var newPi))
                    {
                        Console.Error.WriteLine($"[HOT-SWAP] CreateProcess failed — restoring old Core");
                        _lastFailedSwapStamp = currentSwapStamp;
                        lock (_gate) { _activeStdinWrite = _oldCoreStdinWrite; _oldCoreStdinWrite = IntPtr.Zero; foreach (var kv in _oldInflight) _inflight[kv.Key] = kv.Value; _oldInflight.Clear(); }
                        _swapInProgress = false; return;
                    }
                    CloseHandle(hNewStdinRead);
                    CloseHandle(hNewStdoutWrite);
                    CloseHandle(newPi.hThread);
                    Console.Error.WriteLine($"[HOT-SWAP] new Core started (pid={newPi.dwProcessId})");

                    // 4. Replay initialize + tools/list to new Core
                    lock (_gate)
                    {
                        _activeStdinWrite = hNewStdinWrite;
                        if (_initializeLine != null)
                        {
                            var initBytes = System.Text.Encoding.UTF8.GetBytes(_initializeLine + "\n");
                            WriteFile(hNewStdinWrite, initBytes, (uint)initBytes.Length, out uint _ign1, IntPtr.Zero);
                            FlushFileBuffers(hNewStdinWrite);
                            Console.Error.WriteLine("[HOT-SWAP] replayed initialize");
                        }
                        if (_toolsListLine != null)
                        {
                            var tlBytes = System.Text.Encoding.UTF8.GetBytes(_toolsListLine + "\n");
                            WriteFile(hNewStdinWrite, tlBytes, (uint)tlBytes.Length, out uint _ign2, IntPtr.Zero);
                            FlushFileBuffers(hNewStdinWrite);
                            Console.Error.WriteLine("[HOT-SWAP] replayed tools/list");
                        }
                    }

                    // 5. Start new Core stdout relay thread
                    new Thread(() => RelayStdout(hNewStdoutRead, "new-core", detectElevation: true))
                    { IsBackground = true, Name = "mcp-newcore-stdout" }.Start();

                    // 6. Wait for old Core drain (60s timeout)
                    var drainSw = System.Diagnostics.Stopwatch.StartNew();
                    while (drainSw.Elapsed.TotalSeconds < 60)
                    {
                        lock (_gate) { if (_oldInflight.Count == 0) break; }
                        Thread.Sleep(500);
                    }

                    lock (_gate)
                    {
                        if (_oldInflight.Count > 0)
                        {
                            Console.Error.WriteLine($"[HOT-SWAP] drain timeout — {_oldInflight.Count} requests orphaned, re-routing to new Core");
                            foreach (var kv in _oldInflight)
                            {
                                var reBytes = System.Text.Encoding.UTF8.GetBytes(kv.Value + "\n");
                                WriteFile(_activeStdinWrite, reBytes, (uint)reBytes.Length, out uint _ign3, IntPtr.Zero);
                                _inflight[kv.Key] = kv.Value;
                            }
                            FlushFileBuffers(_activeStdinWrite);
                        }
                        _oldInflight.Clear();
                    }

                    // 7. Close old Core stdin → graceful exit
                    if (_oldCoreStdinWrite != IntPtr.Zero)
                    { CloseHandle(_oldCoreStdinWrite); _oldCoreStdinWrite = IntPtr.Zero; }
                    Console.Error.WriteLine("[HOT-SWAP] old Core stdin closed — graceful exit");

                    _activeCoreStamp = currentSwapStamp;
                    _lastFailedSwapStamp = null;
                    _swapInProgress = false;
                    Console.Error.WriteLine("[HOT-SWAP] swap complete!");
                }
                catch (Exception ex)
                {
                    _lastFailedSwapStamp = currentSwapStamp;
                    Console.Error.WriteLine($"[HOT-SWAP] error: {ex.Message}");
                    _swapInProgress = false;
                }
            }, null, 500, Timeout.Infinite);
        };

        // Stdin router: line-buffered, tracks JSON-RPC id, routes to active Core
        var stdinRelay = new Thread(() =>
        {
            var hIn = GetStdHandle(-10);
            Console.Error.WriteLine($"[LAUNCHER:MCP] stdin relay started (hIn=0x{hIn:X})");
            var buf = new byte[4096];
            var lineBuf = new System.Text.StringBuilder();

            while (ReadFile(hIn, buf, (uint)buf.Length, out uint n, IntPtr.Zero) && n > 0)
            {
                lineBuf.Append(System.Text.Encoding.UTF8.GetString(buf, 0, (int)n));
                var acc = lineBuf.ToString();
                int nlIdx;
                while ((nlIdx = acc.IndexOf('\n')) >= 0)
                {
                    var line = acc[..nlIdx].TrimEnd('\r');
                    acc = acc[(nlIdx + 1)..];

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Extract JSON-RPC id
                    var id = ExtractJsonRpcId(line);

                    lock (_gate)
                    {
                        // Cache initialize + tools/list for hot-swap/admin Core replay
                        if (line.Contains("\"method\":\"initialize\""))
                            _initializeLine = line;
                        else if (line.Contains("\"method\":\"tools/list\""))
                            _toolsListLine = line;

                        // Pre-execution gate: --sudo detected + admin Core not yet running
                        // → trigger admin spawn (same machinery as reactive elevation intercept).
                        // Queue this request as _elevationRequestLine; SpawnAdminCore replays it on success.
                        // If spawn fails, existing SpawnAdminCore error path handles the response.
                        if (id != null && line.Contains("\"--sudo\"") && _adminProc == IntPtr.Zero && !_adminMode)
                        {
                            Console.Error.WriteLine($"[LAUNCHER:MCP] --sudo requested (id={id}) — preemptive admin Core spawn");
                            _inflight[id] = line;
                            _elevationRequestLine = line;
                            new Thread(() => SpawnAdminCore(core, args, _initializeLine, _elevationRequestLine,
                                ref _adminStdinWrite, ref _adminStdoutRead, ref _adminProc, ref _activeStdinWrite,
                                ref _activeStdinWriter, ref _adminMode, ref _pendingSwapWhileAdmin, hCoreStdinWrite, hOut, _gate, _outLock, _inflight))
                            { IsBackground = true, Name = "mcp-sudo-spawn" }.Start();
                            continue; // don't forward to normal Core — admin will handle after spawn
                        }

                        // Track in-flight requests
                        if (id != null)
                            _inflight[id] = line;

                        // Route to active Core
                        if (_activeStdinWriter != null)
                        {
                            _activeStdinWriter.WriteLine(line);
                            _activeStdinWriter.Flush();
                        }
                        else
                        {
                            var target = _activeStdinWrite;
                            if (target != IntPtr.Zero)
                            {
                                var lineBytes = System.Text.Encoding.UTF8.GetBytes(line + "\n");
                                WriteFile(target, lineBytes, (uint)lineBytes.Length, out _, IntPtr.Zero);
                                FlushFileBuffers(target);
                                Console.Error.WriteLine($"[LAUNCHER:MCP] stdin→core: {line[..System.Math.Min(60, line.Length)]}");
                            }
                            else
                                Console.Error.WriteLine("[LAUNCHER:MCP] stdin relay: target handle is ZERO!");
                        }
                    }
                }
                lineBuf.Clear();
                if (acc.Length > 0) lineBuf.Append(acc);
            }

            Console.Error.WriteLine($"[LAUNCHER:MCP] stdin relay: ReadFile loop ended (EOF or error)");
            // ConPTY stdin EOF → close all Core stdins
            lock (_gate)
            {
                if (_activeStdinWrite != IntPtr.Zero) { CloseHandle(_activeStdinWrite); _activeStdinWrite = IntPtr.Zero; }
                if (_activeStdinWriter is IDisposable d) { try { d.Dispose(); } catch { } _activeStdinWriter = null; }
                if (_adminStdinWrite != IntPtr.Zero) { CloseHandle(_adminStdinWrite); _adminStdinWrite = IntPtr.Zero; }
            }
        }) { IsBackground = true, Name = "mcp-stdin-router" };
        stdinRelay.Start();

        // ── Normal Core stdout relay (main thread) ──

        // Shared helper: relay one Core's stdout to hOut, with elevation detection
        void RelayStdout(IntPtr hRead, string label, bool detectElevation)
        {
            var outBuf = new byte[4096];
            var outLineBuf = new System.Text.StringBuilder();
            while (ReadFile(hRead, outBuf, (uint)outBuf.Length, out uint outN, IntPtr.Zero) && outN > 0)
            {
                outLineBuf.Append(System.Text.Encoding.UTF8.GetString(outBuf, 0, (int)outN));
                var acc = outLineBuf.ToString();
                int nlIdx;
                while ((nlIdx = acc.IndexOf('\n')) >= 0)
                {
                    var line = acc[..nlIdx].TrimEnd('\r');
                    acc = acc[(nlIdx + 1)..];

                    // Elevation intercept
                    if (detectElevation && line.Contains("\"_elevationRequired\":true"))
                    {
                        var failedId = ExtractJsonRpcId(line);
                        Console.Error.WriteLine($"[LAUNCHER:MCP] Elevation required (id={failedId}) — spawning admin Core");

                        // Save the original request for re-send
                        string? savedRequest = null;
                        lock (_gate)
                        {
                            if (failedId != null && _inflight.TryGetValue(failedId, out var req))
                                savedRequest = req;
                        }
                        _elevationRequestLine = savedRequest;

                        // Spawn admin Core on background thread (runas blocks on UAC)
                        new Thread(() => SpawnAdminCore(core, args, _initializeLine, _elevationRequestLine,
                            ref _adminStdinWrite, ref _adminStdoutRead, ref _adminProc, ref _activeStdinWrite,
                            ref _activeStdinWriter, ref _adminMode, ref _pendingSwapWhileAdmin, hCoreStdinWrite, hOut, _gate, _outLock, _inflight))
                        { IsBackground = true, Name = "mcp-admin-spawn" }.Start();

                        continue; // suppress elevation error from reaching client
                    }

                    // Remove from inflight (both current and old-drain)
                    var id = ExtractJsonRpcId(line);
                    if (id != null)
                        lock (_gate) { _inflight.Remove(id); _oldInflight.Remove(id); }

                    // Forward to client
                    lock (_outLock)
                    {
                        var lineBytes = System.Text.Encoding.UTF8.GetBytes(line + "\n");
                        WriteFile(hOut, lineBytes, (uint)lineBytes.Length, out _, IntPtr.Zero);
                        FlushFileBuffers(hOut);
                    }
                }
                outLineBuf.Clear();
                if (acc.Length > 0) outLineBuf.Append(acc);
            }
            // Flush remaining
            if (outLineBuf.Length > 0)
            {
                lock (_outLock)
                {
                    var rem = System.Text.Encoding.UTF8.GetBytes(outLineBuf.ToString());
                    WriteFile(hOut, rem, (uint)rem.Length, out _, IntPtr.Zero);
                    FlushFileBuffers(hOut);
                }
            }
            CloseHandle(hRead);
            Console.Error.WriteLine($"[LAUNCHER:MCP] {label} stdout EOF");
        }

        // Run normal Core relay on main thread
        RelayStdout(hCoreStdoutRead, "normal-core", detectElevation: true);

        // If admin Core was spawned, wait for it too
        if (_adminProc != IntPtr.Zero)
        {
            WaitForSingleObject(_adminProc, 0xFFFFFFFF);
            GetExitCodeProcess(_adminProc, out uint adminExit);
            CloseHandle(_adminProc);
            Console.Error.WriteLine($"[LAUNCHER:MCP] admin core exited (code={adminExit})");
        }

        fsw.EnableRaisingEvents = false;
        fsw.Dispose();
        _fswDebounce?.Dispose();

        WaitForSingleObject(pi.hProcess, 5000);
        GetExitCodeProcess(pi.hProcess, out uint exitCode2);
        CloseHandle(pi.hProcess);
        Console.Error.WriteLine($"[LAUNCHER:MCP] core exited (code={exitCode2})");
        return (int)exitCode2;
    }

    /// <summary>Extract JSON-RPC "id" from a JSON line (naive string search, sufficient for MCP).</summary>
    static string? ExtractJsonRpcId(string line)
    {
        // Find "id": followed by value (number or string)
        var idx = line.IndexOf("\"id\"", StringComparison.Ordinal);
        if (idx < 0) return null;
        var colonIdx = line.IndexOf(':', idx + 4);
        if (colonIdx < 0) return null;
        var rest = line[(colonIdx + 1)..].TrimStart();
        if (rest.Length == 0) return null;
        if (rest[0] == '"')
        {
            var endQuote = rest.IndexOf('"', 1);
            return endQuote > 0 ? rest[1..endQuote] : null;
        }
        // Numeric id
        int end = 0;
        while (end < rest.Length && (char.IsDigit(rest[end]) || rest[end] == '-')) end++;
        return end > 0 ? rest[..end] : null;
    }

    [System.Runtime.InteropServices.DllImport("shell32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern bool ShellExecuteExW(ref SHELLEXECUTEINFOW lpExecInfo);

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    struct SHELLEXECUTEINFOW
    {
        public uint cbSize;
        public uint fMask;
        public IntPtr hwnd;
        public string lpVerb;
        public string lpFile;
        public string lpParameters;
        public string? lpDirectory;
        public int nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        public string? lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon; // union with hMonitor
        public IntPtr hProcess;
    }

    const uint SEE_MASK_NOCLOSEPROCESS = 0x00000040;

    /// <summary>Spawn elevated admin Core with named pipes for MCP relay.</summary>
    static void SpawnAdminCore(string corePath, string[] mcpArgs,
        string? initLine, string? elevationRequestLine,
        ref IntPtr adminStdinWrite, ref IntPtr adminStdoutRead, ref IntPtr adminProc,
        ref IntPtr activeStdinWrite, ref System.IO.TextWriter? activeStdinWriter, ref bool adminMode, ref bool pendingSwapWhileAdmin,
        IntPtr oldCoreStdinWrite, IntPtr hOut, object gate, object outLock,
        Dictionary<string, string> inflight)
    {
        System.IO.StreamWriter? stdinWriter = null;
        System.IO.StreamReader? stdoutReader = null;
        var pipeName = $"wkappbot-mcp-admin-{Environment.ProcessId}";
        var stdinPipeName = $"{pipeName}-stdin";
        var stdoutPipeName = $"{pipeName}-stdout";

        // Launch admin Core via ShellExecuteExW runas
        var paramStr = $"mcp --admin-pipes {stdinPipeName} {stdoutPipeName}";
        var sei = new SHELLEXECUTEINFOW
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<SHELLEXECUTEINFOW>(),
            fMask = SEE_MASK_NOCLOSEPROCESS,
            lpVerb = "runas",
            lpFile = corePath,
            lpParameters = paramStr,
            nShow = 0, // SW_HIDE
        };

        Console.Error.WriteLine($"[LAUNCHER:MCP] Requesting admin elevation (runas)...");
        if (!ShellExecuteExW(ref sei))
        {
            var err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            Console.Error.WriteLine($"[LAUNCHER:MCP] UAC denied or failed (err={err})");
            // Send error for the elevation-failed request
            if (elevationRequestLine != null)
            {
                var failId = ExtractJsonRpcId(elevationRequestLine);
                var errJson = $"{{\"jsonrpc\":\"2.0\",\"id\":{failId},\"error\":{{\"code\":-32001,\"message\":\"Elevation denied by user\"}}}}\n";
                lock (outLock)
                {
                    var errBytes = System.Text.Encoding.UTF8.GetBytes(errJson);
                    WriteFile(hOut, errBytes, (uint)errBytes.Length, out _, IntPtr.Zero);
                    FlushFileBuffers(hOut);
                }
                lock (gate) { if (failId != null) inflight.Remove(failId); }
            }
            return;
        }

        Console.Error.WriteLine($"[LAUNCHER:MCP] Admin Core launched (pid=?) — waiting for pipe connection (60s)...");
        adminProc = sei.hProcess;

        // Wait for admin Core to connect to named pipes (it uses NamedPipeClientStream)
        // We create server-side named pipes that admin Core connects to
        try
        {
            using var pipeStdinServer = new System.IO.Pipes.NamedPipeServerStream(stdinPipeName,
                System.IO.Pipes.PipeDirection.Out, 1, System.IO.Pipes.PipeTransmissionMode.Byte, System.IO.Pipes.PipeOptions.None);
            using var pipeStdoutServer = new System.IO.Pipes.NamedPipeServerStream(stdoutPipeName,
                System.IO.Pipes.PipeDirection.In, 1, System.IO.Pipes.PipeTransmissionMode.Byte, System.IO.Pipes.PipeOptions.None);

            // Wait for connections (60s timeout for UAC)
            var cts = new System.Threading.CancellationTokenSource(60_000);
            pipeStdinServer.WaitForConnectionAsync(cts.Token).GetAwaiter().GetResult();
            pipeStdoutServer.WaitForConnectionAsync(cts.Token).GetAwaiter().GetResult();
            Console.Error.WriteLine("[LAUNCHER:MCP] Admin Core connected to pipes!");

            // Transition: close old Core stdin (drain), switch to admin
            lock (gate)
            {
                CloseHandle(oldCoreStdinWrite); // EOF to old Core → drain remaining work
                activeStdinWrite = IntPtr.Zero; // temporarily null until we set up admin pipe handle
            }

            // Send initialize + elevation request to admin Core
            // NOTE: NOT using 'using' — lifetime managed by finally block below.
            // 'using' would dispose at method exit while _activeStdinWriter still references it (race).
            stdinWriter = new System.IO.StreamWriter(pipeStdinServer, new System.Text.UTF8Encoding(false)) { AutoFlush = true };
            if (initLine != null)
            {
                lock (gate)
                {
                    stdinWriter.WriteLine(initLine);
                    stdinWriter.Flush();
                }
            }
            // Wait briefly for initialize response (read and forward)
            stdoutReader = new System.IO.StreamReader(pipeStdoutServer, System.Text.Encoding.UTF8);
            var initResp = stdoutReader.ReadLine(); // initialize response
            if (initResp != null)
            {
                // Don't forward init response (client already initialized)
                Console.Error.WriteLine($"[LAUNCHER:MCP] Admin init OK: {initResp[..Math.Min(80, initResp.Length)]}...");
            }

            lock (gate)
            {
                activeStdinWriter = stdinWriter;
                adminMode = true;
            }

            // Re-send the elevation-failed request
            if (elevationRequestLine != null)
            {
                lock (gate)
                {
                    stdinWriter.WriteLine(elevationRequestLine);
                    stdinWriter.Flush();
                }
                Console.Error.WriteLine($"[LAUNCHER:MCP] Re-sent elevation request to admin Core");
            }

            // Relay admin Core stdout on this thread
            string? adminLine;
            while ((adminLine = stdoutReader.ReadLine()) != null)
            {
                var id = ExtractJsonRpcId(adminLine);
                if (id != null)
                    lock (gate) { inflight.Remove(id); }

                lock (outLock)
                {
                    var lineBytes = System.Text.Encoding.UTF8.GetBytes(adminLine + "\n");
                    WriteFile(hOut, lineBytes, (uint)lineBytes.Length, out _, IntPtr.Zero);
                    FlushFileBuffers(hOut);
                }
            }
            Console.Error.WriteLine("[LAUNCHER:MCP] Admin Core stdout EOF");
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("[LAUNCHER:MCP] Admin Core connection timeout (60s) — elevation failed");
            try { TerminateProcess(adminProc, 1); } catch { }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER:MCP] Admin Core error: {ex.Message}");
        }
        finally
        {
            lock (gate)
            {
                activeStdinWriter = null; // prevent other threads from writing
                if (pendingSwapWhileAdmin)
                {
                    Console.Error.WriteLine("[LAUNCHER:MCP] deferred core swap is still pending after admin endpoint closed");
                    pendingSwapWhileAdmin = false;
                }
            }
            // Dispose after nulling reference — no race
            try { stdinWriter?.Dispose(); } catch { }
            try { stdoutReader?.Dispose(); } catch { }
        }
    }
}
