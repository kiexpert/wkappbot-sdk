using System.Diagnostics;
using System.Text;

namespace WKAppBot.Launcher;

/// <summary>
/// Ultra-thin relay launcher for WKAppBot.
/// Happy path: delegates to Eye via named pipe (~200ms total, no cold-start).
/// Fallback: spawns wkappbot-core.exe directly (~3s, rare — Eye not running).
///
/// Routing control flags (parsed here, stripped before forwarding to Eye/Core):
///   --only-eye       Eye pipe required — fail with exit 3 if Eye unavailable (no Core fallback)
///   --only-core      Skip Eye pipe — run Core directly regardless of Eye state
///   --timeout N      Kill Core after N seconds (Launcher-level watchdog, exit 2 on timeout)
///   --timeout-exit N Override timeout exit code (default: 2); applies to both normal and mcp mode
///
/// Fixed routing (flag-independent):
///   mcp  → Launcher owns stdio pipe permanently; Core runs behind proxy (restartable)
///   eye  → Core directly (eye IS the daemon)
/// </summary>
class Program
{
    // Busybox aliases: symlink names that map to an implicit first argument
    static readonly (string name, string cmd)[] BusyboxAliases =
    {
        ("a11y",   "a11y"),
        ("wka11y", "a11y"),
        ("inspect","inspect"),
        ("ocr",    "ocr"),
        ("logcat", "logcat"),
        ("grep",   "logcat"), // grep → logcat alias (busybox-style)
        ("grap",   "logcat"), // grap → logcat alias (GRab Accessible Pattern)
        ("scan",   "scan"),
    };

    // Aliases that bypass Launcher and point directly to Core (no relay needed)
    static readonly HashSet<string> CoreDirectAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        "grap", "grep", "logcat",
    };

    // WKAPPBOT_PROFILE=1 support — static so RunCore() can log too
    static readonly System.Diagnostics.Stopwatch _sw = System.Diagnostics.Stopwatch.StartNew();
    static readonly bool _prof = Environment.GetEnvironmentVariable("WKAPPBOT_PROFILE") == "1";
    static void Prof(string label) { if (_prof) try { Console.Error.WriteLine($"[LAUNCHER] {_sw.ElapsedMilliseconds}ms {label}"); } catch { } }

    [STAThread]
    static int Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false); // no BOM — BOM is noise in pipes/relay
        Console.InputEncoding = Encoding.UTF8;

        // backward-compat local alias so existing prof("...") calls in Main still work
        Action<string> prof = Prof;
        prof("Main() entered");

        // Busybox-style: if invoked as a11y.exe / wka11y.exe / etc., prepend implicit command.
        // Must be done in Launcher because Core is spawned as a new process — argv[0] loses the symlink name.
        // Also auto-create missing symlinks when running as wkappbot.exe.
        var argv0      = Environment.GetCommandLineArgs().FirstOrDefault() ?? "";
        var exeBase    = Path.GetFileNameWithoutExtension(argv0).ToLowerInvariant();
        prof($"exeBase={exeBase}");
        var implicitCmd = BusyboxAliases
            .Where(x => exeBase == x.name || exeBase.Contains(x.name))
            .Select(x => x.cmd)
            .FirstOrDefault();
        if (implicitCmd != null)
        {
            // grap/grep: pass alias name to Core (not "logcat") — Core handles arg-order translation + help.
            // This allows Core's help fast path to show grap-specific help for "grap" with no args.
            var prependCmd = (exeBase == "grap" || exeBase == "grep") ? exeBase : implicitCmd;
            if (args.Length == 0 || args[0].ToLowerInvariant() != prependCmd)
                args = new[] { prependCmd }.Concat(args).ToArray();
            prof($"busybox-prepend={prependCmd}");
        }

        // --args-file <path>: read args from UTF-8 text file (one arg per line) to bypass
        // bash→PowerShell CP949/UTF-8 mismatch that corrupts Korean command-line args.
        // Scan after busybox prepend so implicit command is already present if needed.
        // File format: one arg per line, empty lines ignored. No quoting needed.
        // Usage: printf '%s\n' a11y type "한글입력" > /tmp/a.txt && wkappbot --args-file /tmp/a.txt
        {
            var argsFileIdx = Array.FindIndex(args, a => a == "--args-file");
            if (argsFileIdx >= 0 && argsFileIdx + 1 < args.Length)
            {
                var argsFilePath = args[argsFileIdx + 1];
                if (!File.Exists(argsFilePath))
                {
                    Console.Error.WriteLine($"[LAUNCHER] --args-file: not found: {argsFilePath}");
                    return 1;
                }
                var fileArgs = File.ReadAllLines(argsFilePath, Encoding.UTF8)
                    .Where(l => l.Length > 0).ToArray();
                args = args[..argsFileIdx].Concat(fileArgs).Concat(args[(argsFileIdx + 2)..]).ToArray();
                prof($"--args-file: {fileArgs.Length} args loaded");
            }
        }

        // grap/grep with no args (or --help/-h): print help directly in Launcher — no Core needed.
        // Must run BEFORE EnsureBusyboxAliases to avoid ~1.7s busybox symlink scan.
        // This avoids the bash/MSYS2→Launcher→Core startup hang that occurs when stdin/stdout/stderr
        // are MSYS2 pty handles: .NET 8 single-file AppHost gets stuck before reaching Main().
        if (args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep"
            && (args.Length == 1 || args.Any(a => a is "--help" or "-h")))
        {
            PrintGrapHelp(args[0].ToLowerInvariant());
            Console.Out.Flush();
            TerminateSelf(0);
            return 0; // unreachable
        }

        // Auto-create missing busybox symlinks (runs only when argv0 == wkappbot)
        if (exeBase == "wkappbot")
        {
            prof("EnsureBusyboxAliases start");
            EnsureBusyboxAliases();
            prof("EnsureBusyboxAliases done");
        }

        if (args.Length == 0)
        {
            prof("no-args → RunCore");
            return RunCore(args);
        }

        var cmd = args[0].ToLowerInvariant();
        prof($"cmd={cmd}");

        // Routing control flags — parsed by Launcher, stripped before forwarding to Eye/Core
        bool onlyEye  = args.Any(a => a == "--only-eye");
        bool onlyCore = args.Any(a => a == "--only-core");

        if (onlyEye && onlyCore)
        {
            Console.Error.WriteLine("[LAUNCHER] --only-eye and --only-core are mutually exclusive");
            return 1;
        }

        // Strip routing flags from args forwarded downstream
        var forwardArgs = onlyEye || onlyCore
            ? args.Where(a => a != "--only-eye" && a != "--only-core").ToArray()
            : args;

        // mcp: Launcher holds the stdio pipe to Claude Code and manages Core lifecycle
        if (cmd == "mcp")
        {
            prof("mcp → RunMcpProxy");
            return RunMcpProxy(forwardArgs);
        }

        // eye: IS the daemon, must run core directly
        // file: read-only utility (PDF/OCR may take several seconds) — skip Eye pipe to avoid timeout
        // help/no-args: fast path — skip Eye pipe, run Core directly (Core is ~22ms for help)
        // logcat/grep/grap: streaming log monitor — needs direct stdout, TeeConsole, full error handling
        if (!onlyCore && cmd != "eye" && cmd != "file" && cmd != "logcat" && cmd != "grep" && cmd != "grap"
            && cmd != "help" && cmd != "--help" && cmd != "-h")
        {
            prof($"Eye pipe attempt cmd={cmd}");
            if (EyeCmdPipeClient.TryDelegate(forwardArgs, out int code))
            {
                prof("Eye pipe: delegated");
                return code;
            }
            prof("Eye pipe: unavailable, falling back to Core");

            if (onlyEye)
            {
                Console.Error.WriteLine("[LAUNCHER] --only-eye: Eye pipe unavailable — refusing Core fallback");
                return 3; // distinct exit: Eye required but not running
            }
        }

        prof($"→ RunCore cmd={cmd}");

        // grap/grep with args: spawn Core via UseShellExecute=true (ShellExecuteEx) so bash
        // doesn't wait 27s. ShellExecuteEx detaches from bash's ConPTY/job tracking → bash
        // exits as soon as Launcher (wkappbot.exe) exits. Core runs in background.
        // Output relay: Core writes to WKAPPBOT_RELAY_FILE, signals .ready; Launcher reads+relays.
        // Follow mode (-f/--follow) runs normally (streaming, Ctrl+C to stop).
        if (cmd is "grap" or "grep")
        {
            bool isFollowMode = forwardArgs.Any(a => a is "-f" or "--follow");
            if (!isFollowMode)
            {
                var _dir2 = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
                var _core2 = Path.Combine(_dir2, "wkappbot-core.exe");
                var _relayBase = Directory.Exists(@"C:\Temp") ? @"C:\Temp" : Path.GetTempPath();
                var _relayFile    = Path.Combine(_relayBase, $"wkappbot-relay-{Environment.ProcessId}.txt");
                var _readyPath    = _relayFile + ".ready";
                var _ackPath      = _relayFile + ".ack";
                var _exitCodePath = _relayFile + ".exitcode";
                foreach (var p in new[] { _relayFile, _readyPath, _ackPath, _exitCodePath }) try { File.Delete(p); } catch { }

                // Set relay env var on our own process — inherited by UseShellExecute spawn
                Environment.SetEnvironmentVariable("WKAPPBOT_RELAY_FILE", _relayFile);

                var _tp = new System.Diagnostics.Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = _core2,
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                    }
                };
                foreach (var a in forwardArgs) _tp.StartInfo.ArgumentList.Add(a);
                _tp.Start();
                var _lDbg = Path.Combine(@"C:\Temp", "launcher_relay_dbg.txt");
                void LDbg(string s) { try { File.AppendAllText(_lDbg, $"{_sw.ElapsedMilliseconds}ms {s}\n"); } catch { } }
                LDbg($"spawn done pid={_tp.Id}");
                prof("UseShellExecute spawn done, waiting for relay");

                // Poll for .ready (Core signals after flushing relay file, while still alive)
                var _relaySw = System.Diagnostics.Stopwatch.StartNew();
                bool _relayFound = false;
                while (_relaySw.ElapsedMilliseconds < 300_000) // 5min max
                {
                    if (GetFileAttributesW(_readyPath) != 0xFFFFFFFF) { _relayFound = true; break; }
                    System.Threading.Thread.Sleep(10);
                }
                LDbg($"poll done relayFound={_relayFound} elapsed={_relaySw.ElapsedMilliseconds}ms");

                if (_relayFound)
                {
                    LDbg("reading relay file");
                    try
                    {
                        var _content = File.ReadAllText(_relayFile, Encoding.UTF8);
                        LDbg($"relay read bytes={_content.Length}, writing stdout");
                        Console.Out.Write(_content);
                        LDbg("stdout write done, flushing");
                        Console.Out.Flush();
                        LDbg("flush done");
                    }
                    catch (Exception ex) { LDbg($"relay read/write FAILED: {ex.Message}"); }
                    LDbg("writing .ack");
                    try { File.WriteAllText(_ackPath, "1"); LDbg(".ack written"); } catch (Exception ex) { LDbg($".ack FAILED: {ex.Message}"); }
                }

                int _relayExitCode = 0;
                try { if (File.Exists(_exitCodePath)) _relayExitCode = int.Parse(File.ReadAllText(_exitCodePath).Trim()); } catch { }
                LDbg($"cleanup + TerminateSelf exitCode={_relayExitCode}");
                foreach (var p in new[] { _relayFile, _readyPath, _ackPath, _exitCodePath }) try { File.Delete(p); } catch { }
                prof("relay done, TerminateSelf");
                TerminateSelf((uint)_relayExitCode);
                return _relayExitCode; // unreachable
            }
            // follow mode: run normally (streaming, no timeout)
        }

        // Fast-exit commands (help): watchdog reports exact step if process hangs >3s.
        // _lDiagStep is a static field so RunCore() can update it directly.
        bool isFastExit = cmd is "help" or "--help" or "-h";
        if (isFastExit)
        {
            _lDiagStep = "before-RunCore";
            var _lSw = System.Diagnostics.Stopwatch.StartNew();
            AppDomain.CurrentDomain.ProcessExit += (_, _) =>
                Console.Error.WriteLine($"[HANG-DIAG-L] ProcessExit: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
            var _lWatchdog = new Thread(() =>
            {
                Thread.Sleep(3_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 3s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
                Thread.Sleep(5_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 8s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
                Thread.Sleep(18_000);
                Console.Error.WriteLine($"[HANG-DIAG-L] 26s: step={_lDiagStep} elapsed={_lSw.ElapsedMilliseconds}ms");
            }) { IsBackground = true, Name = "LauncherHangDiag" };
            _lWatchdog.Start();

            // Core calls FastExit (TerminateProcess) after flushing help output.
            // proc.WaitForExit detects termination immediately — no named-event needed.
            var code = RunCore(forwardArgs, fastExitTimeoutMs: 2000);
            _lDiagStep = "post-RunCore";
            TerminateSelf((uint)code);
            return code; // unreachable
        }

        return RunCore(forwardArgs);
    }

    // Shared step name for fast-exit watchdog — updated in both Main() and RunCore().
    static volatile string _lDiagStep = "";

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern uint GetFileAttributesW(string lpFileName);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern IntPtr GetCurrentProcess();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    // CreateProcessW with DETACHED_PROCESS: spawns Core outside bash's ConPTY session.
    // bash tracks processes via the ConPTY session (PTY slave handles). DETACHED_PROCESS prevents
    // Core from inheriting or attaching to any console, so bash doesn't wait for Core's 27s SMB
    // image-section cleanup. Launcher exits quickly; Core's cleanup runs in background.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct STARTUPINFOW
    {
        public int cb, _res0; public IntPtr lpReserved, lpDesktop, lpTitle;
        public uint dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute;
        public uint dwFlags; public ushort wShowWindow, cbReserved2; public IntPtr lpReserved2;
        public IntPtr hStdInput, hStdOutput, hStdError;
    }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    struct PROCESS_INFORMATION { public IntPtr hProcess, hThread; public uint dwProcessId, dwThreadId; }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern bool CreateProcessW(string? app, char[] cmd, IntPtr pa, IntPtr ta,
        bool inh, uint flags, IntPtr env, string? cwd, ref STARTUPINFOW si, out PROCESS_INFORMATION pi);

    const uint DETACHED_PROCESS          = 0x00000008;
    const uint CREATE_BREAKAWAY_FROM_JOB = 0x01000000;
    const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;

    /// <summary>
    /// Build a Unicode environment block for Core: current env + WKAPPBOT_RELAY_FILE, minus PTY vars.
    /// The block is double-null-terminated (Win32 CreateProcess format).
    /// Caller must free the returned pointer with Marshal.FreeHGlobal.
    /// </summary>
    static IntPtr BuildDetachedEnvBlock(string relayFilePath)
    {
        var strip = new System.Collections.Generic.HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
        {
            "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL", "ConEmuANSI",
            "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST", "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE",
            "WKAPPBOT_RELAY_FILE",
        };
        var sb = new StringBuilder();
        foreach (System.Collections.DictionaryEntry kv in Environment.GetEnvironmentVariables())
        {
            var k = kv.Key?.ToString() ?? "";
            if (strip.Contains(k)) continue;
            sb.Append(k).Append('=').Append(kv.Value?.ToString() ?? "").Append('\0');
        }
        sb.Append("WKAPPBOT_RELAY_FILE=").Append(relayFilePath).Append('\0');
        sb.Append('\0'); // double-null terminator
        var bytes = Encoding.Unicode.GetBytes(sb.ToString());
        var ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(bytes.Length);
        System.Runtime.InteropServices.Marshal.Copy(bytes, 0, ptr, bytes.Length);
        return ptr;
    }

    /// <summary>
    /// Spawn Core with DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB.
    /// Core runs outside bash's ConPTY session and job object → bash doesn't wait for Core.
    /// Returns Core's process handle (caller must CloseHandle) or IntPtr.Zero on failure.
    /// </summary>
    static IntPtr SpawnDetachedCore(string core, string[] args, IntPtr envBlock)
    {
        var cmd = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmd.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        var cmdArr = (cmd.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW { cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>() };
        bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, false,
            DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB | CREATE_UNICODE_ENVIRONMENT,
            envBlock, null, ref si, out var pi);
        if (!ok) return IntPtr.Zero;
        CloseHandle(pi.hThread);
        return pi.hProcess;
    }

    /// <summary>
    /// grap/grep one-shot: spawn Core detached so bash doesn't wait for Core's 27s SMB cleanup.
    /// Core writes output to relay file (WKAPPBOT_RELAY_FILE). Launcher polls for .ready sentinel
    /// (created by Core while still alive → immediately visible). Reads relay, exits fast.
    /// </summary>
    static int RunGrapDetached(string[] args, int timeoutMs)
    {
        var dir  = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = Path.Combine(dir, "wkappbot-core.exe");
        if (!File.Exists(core))
        {
            Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found: {core}");
            return 1;
        }

        var relayBase = Directory.Exists(@"C:\Temp") ? @"C:\Temp" : Path.GetTempPath();
        var relayFile = Path.Combine(relayBase, $"wkappbot-relay-{Environment.ProcessId}.txt");
        var readyPath = relayFile + ".ready";
        var ackPath   = relayFile + ".ack";
        foreach (var p in new[] { relayFile, readyPath, ackPath }) try { File.Delete(p); } catch { }

        var envPtr = BuildDetachedEnvBlock(relayFile);
        try
        {
            var hProc = SpawnDetachedCore(core, args, envPtr);
            if (hProc == IntPtr.Zero)
            {
                Prof($"detached spawn failed (err={System.Runtime.InteropServices.Marshal.GetLastWin32Error()}), fallback");
                return RunCore(args); // fallback to normal (slow 27s) path
            }
            Prof($"detached core spawned pid=?, polling .ready");

            // Poll for .ready (Core creates it while alive → visible on local FS immediately)
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool found = false;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (GetFileAttributesW(readyPath) != 0xFFFFFFFF) { found = true; break; }
                Thread.Sleep(5);
            }
            Prof(found ? $"detached: .ready found t={sw.ElapsedMilliseconds}ms"
                       : $"detached: .ready timeout t={sw.ElapsedMilliseconds}ms");

            if (found)
            {
                try
                {
                    Console.OutputEncoding = new UTF8Encoding(false); // no BOM
                    var content = File.ReadAllText(relayFile, Encoding.UTF8);
                    Console.Out.Write(content);
                    Console.Out.Flush();
                    Prof($"detached: {content.Length} chars written");
                }
                catch (Exception ex) { Prof($"detached: relay read error: {ex.Message}"); }
                try { File.WriteAllText(ackPath, "1"); } catch { } // signal Core: file read
            }
            else
            {
                // Core never signaled — kill it (abnormal)
                try { TerminateProcess(hProc, 1); } catch { }
            }
            CloseHandle(hProc);
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(envPtr);
            foreach (var p in new[] { relayFile, readyPath, ackPath }) try { File.Delete(p); } catch { }
        }
        TerminateSelf(0);
        return 0; // unreachable
    }

    static void TerminateSelf(uint code) => TerminateProcess(GetCurrentProcess(), code);

    /// <summary>
    /// MCP stdio proxy loop.
    /// Launcher holds the stdio pipe to Claude Code permanently.
    /// Core runs behind the proxy — if it exits with code 42, it is restarted automatically.
    /// stdin broadcaster routes bytes to whichever Core instance is current.
    /// </summary>
    static int RunMcpProxy(string[] args)
    {
        var dir  = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = Path.Combine(dir, "wkappbot-core.exe");

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
                Process.Start(new ProcessStartInfo
                {
                    FileName        = "wt.exe",
                    Arguments       = $"--window 0 new-tab --title \"{wtTitle}\" -- powershell -NoExit -Command \"{psCmd}\"",
                    UseShellExecute = true,
                }) ;
                Console.Error.WriteLine($"[LAUNCHER:MCP] WT monitor tab opened → {wtLogFile}");
            }
            catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER:MCP] wt.exe failed: {ex.Message}"); wtLogFile = null; }
        }

        var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");

        // stdin broadcaster — single reader, routes bytes to whichever Core is current
        Process? _current = null;
        var stdinThread = new Thread(() =>
        {
            var stdin = Console.OpenStandardInput();
            var buf   = new byte[4096];
            while (true)
            {
                int n;
                try   { n = stdin.Read(buf, 0, buf.Length); }
                catch { break; }
                if (n == 0) break; // EOF from Claude Code → done

                var p = Volatile.Read(ref _current);
                if (p == null) continue;
                try
                {
                    p.StandardInput.BaseStream.Write(buf, 0, n);
                    p.StandardInput.BaseStream.Flush();
                }
                catch { } // Core died mid-write — next iteration gets new Core
            }
        }) { IsBackground = true, Name = "mcp-stdin" };
        stdinThread.Start();

        while (true)
        {
            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName        = core,
                    UseShellExecute = false,
                    RedirectStandardInput  = true,    // fed by stdin broadcaster
                    RedirectStandardOutput = false,    // inherited → flows to Claude Code
                    RedirectStandardError  = wtLogFile != null, // --wt: tee to log; else inherited
                    CreateNoWindow  = false,
                }
            };
            foreach (var a in args) proc.StartInfo.ArgumentList.Add(a);
            if (!string.IsNullOrEmpty(dotnetRoot))
                proc.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = dotnetRoot;

            // --wt: tee stderr to log file so WT tab can tail it
            if (wtLogFile != null)
            {
                proc.ErrorDataReceived += (_, e) =>
                {
                    if (e.Data == null) return;
                    Console.Error.WriteLine(e.Data);           // still flows to MCP caller
                    try { File.AppendAllText(wtLogFile, e.Data + "\n"); } catch { }
                };
            }

            proc.Start();
            if (wtLogFile != null) proc.BeginErrorReadLine();
            Volatile.Write(ref _current, proc);
            Console.Error.WriteLine($"[LAUNCHER:MCP] core started (pid={proc.Id})");

            if (timeoutSec > 0)
            {
                if (!proc.WaitForExit(timeoutSec * 1000))
                {
                    Console.Error.WriteLine($"[LAUNCHER:MCP] timeout {timeoutSec}s exceeded — killing core (pid={proc.Id})");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    return timeoutExit;
                }
            }
            else
            {
                proc.WaitForExit();
            }

            var exit = proc.ExitCode;
            Console.Error.WriteLine($"[LAUNCHER:MCP] core exited (code={exit})");
            if (exit == 42) { Thread.Sleep(50); continue; } // Core self-requested reload
            return exit;
        }
    }

    /// <param name="fastExitTimeoutMs">If >0, kills Core if it doesn't exit within this time (ms) and returns exit 0.
    /// Used for fast-exit commands (help) where Core may hang 26s due to RotateOldLogs on W: drive.</param>
    /// <param name="stdoutRelayOnly">If true, redirect only stdout (not stdin/stderr) and relay it.
    /// Used for grap/grep with args — prevents MSYS2 PTY drain (~31s) from large stdout output.
    /// Core inherits stdin/stderr from Launcher (PTY) so interactive features still work.</param>
    static int RunCore(string[] args, int fastExitTimeoutMs = 0, bool stdoutRelayOnly = false)
    {
        try
        {
            // wkappbot-core.exe lives next to wkappbot.exe in SDK/bin/
            var dir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
            var core = Path.Combine(dir, "wkappbot-core.exe");

            if (!File.Exists(core))
            {
                Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found at: {core}");
                Console.Error.WriteLine("[LAUNCHER] Run: dotnet publish WKAppBot.CLI to deploy core.");
                return 1;
            }

            // Parse --timeout N (seconds) — Launcher-level watchdog.
            // Works for ALL commands, including ones that don't implement their own timeout.
            // stdout/stderr inherited (no piping overhead); Launcher simply kills Core if exceeded.
            int timeoutSec  = 0;
            int timeoutExit = 2;
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "--timeout"      && int.TryParse(args[i + 1], out var t) && t > 0) timeoutSec  = t;
                if (args[i] == "--timeout-exit" && int.TryParse(args[i + 1], out var e))           timeoutExit = e;
            }

            bool useFastExit   = fastExitTimeoutMs > 0;
            bool useStdoutRelay = stdoutRelayOnly && !useFastExit;
            // For grap/grep one-shot: temp file relay avoids all pipe/ConPTY handle issues.
            // ConPTY (bash → Launcher → Core) swaps stdout/stderr handles in Core when UseShellExecute=false
            // redirects both streams; and CreateNoWindow=true prevents .NET 8 from initializing Console.
            // Solution: Core writes to a local temp file (WKAPPBOT_RELAY_FILE env var), Launcher reads after.
            // For --follow mode: run Core normally (no temp file, output goes to terminal, Ctrl+C to stop).
            bool followMode = useStdoutRelay && args.Any(a => a is "-f" or "--follow");
            string? relayFilePath = null;
            if (useStdoutRelay && !followMode)
            {
                // Use C:\Temp\ instead of GetTempPath() — AppData\Local\Temp has 28s visibility delay
                // when Core's SMB handles are being cleaned up after TerminateProcess.
                relayFilePath = System.IO.Directory.Exists(@"C:\Temp")
                    ? $@"C:\Temp\wkappbot-relay-{Environment.ProcessId}.txt"
                    : System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"wkappbot-relay-{Environment.ProcessId}.txt");
                // File will be created by Core; delete any stale file from previous run
                try { System.IO.File.Delete(relayFilePath); } catch { }
            }

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = core,
                    UseShellExecute = false,
                    // fast-exit: redirect all 3 streams + CreateNoWindow — bypasses MSYS2/Cygwin pty.
                    // stdout-relay (one-shot): NO stream redirect — Core writes to WKAPPBOT_RELAY_FILE temp file.
                    //   Avoids ConPTY handle swapping and CreateNoWindow stdio init issues.
                    // stdout-relay (follow): NO stream redirect — Core output goes to terminal, Ctrl+C stops it.
                    // normal: inherit all handles directly.
                    RedirectStandardOutput = useFastExit,
                    RedirectStandardError  = useFastExit,
                    // fast-exit: redirect stdin too — Core must NOT inherit ConPTY stdin handle.
                    // If Core holds a ConPTY handle, bash waits 27s after Core's TerminateProcess
                    // (SMB image section lock delays all handle cleanup, including ConPTY slave).
                    // With stdin redirected: Core gets a pipe (EOF), no ConPTY handle → bash completes
                    // as soon as Launcher exits (~100ms), while Core's 27s cleanup runs in the background.
                    RedirectStandardInput  = useFastExit,
                    StandardOutputEncoding = useFastExit ? System.Text.Encoding.UTF8 : null,
                    StandardErrorEncoding  = useFastExit ? System.Text.Encoding.UTF8 : null,
                    // fast-exit: CreateNoWindow=true detaches Core from parent ConPTY session.
                    // Without this, Core inherits the ConPTY console handle and bash waits 27s
                    // (SMB image section cleanup delay) even after Launcher has already exited.
                    // With CreateNoWindow=true: Core has no console handle → ConPTY not affected.
                    CreateNoWindow         = useFastExit,
                }
            };
            // File-based handshake: Core creates .ready BEFORE TerminateProcess (file visible immediately).
            // Launcher polls for .ready, reads relay file while Core is alive, then creates .ack.
            // Core waits for .ack (max 500ms), then calls TerminateProcess.
            // No named kernel objects — avoids ConPTY/session namespace issues.

            // Pass relay file path to Core via env var (one-shot only)
            if (relayFilePath != null)
                proc.StartInfo.EnvironmentVariables["WKAPPBOT_RELAY_FILE"] = relayFilePath;

            // fast-exit path: strip MSYS2/Cygwin PTY env vars so .NET 8 AppHost doesn't deadlock.
            // When bash launches Launcher, it injects TERM/MSYSTEM/MSYS/etc. into the env.
            // .NET 8 single-file AppHost detects PTY via these vars and tries to reconcile with
            // the redirected pipe handles → deadlock before Main() is reached.
            // Stripping them lets AppHost start normally with the pipe handles.
            if (useFastExit)
            {
                foreach (var v in new[] { "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL",
                                          "ConEmuANSI", "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST",
                                          "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE" })
                    proc.StartInfo.EnvironmentVariables.Remove(v);
            }

            // Forward all args as-is
            foreach (var a in args)
                proc.StartInfo.ArgumentList.Add(a);


            _lDiagStep = "proc-starting";
            Prof("proc.Start()");
            proc.Start();
            _lDiagStep = $"proc-waitforexit(pid={proc.Id})";
            Prof($"proc.WaitForExit pid={proc.Id}");

            if (useFastExit)
            {
                // Close stdin pipe immediately — Core gets EOF (doesn't read stdin for help/grap).
                // This ensures Core holds NO ConPTY handles → bash won't wait 27s after Core dies.
                try { proc.StandardInput.Close(); } catch { }
                // Relay Core's stdout and stderr to Launcher's stdout/stderr in background.
                Console.OutputEncoding = new System.Text.UTF8Encoding(false); // no BOM
                var stdoutRelay = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new byte[4096];
                        int n;
                        while ((n = proc.StandardOutput.BaseStream.Read(buf, 0, buf.Length)) > 0)
                            Console.Out.Write(System.Text.Encoding.UTF8.GetString(buf, 0, n));
                        Console.Out.Flush();
                    }
                    catch { }
                });
                var stderrRelay = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new byte[4096];
                        int n;
                        while ((n = proc.StandardError.BaseStream.Read(buf, 0, buf.Length)) > 0)
                            Console.Error.Write(System.Text.Encoding.UTF8.GetString(buf, 0, n));
                        Console.Error.Flush();
                    }
                    catch { }
                });
                // Core calls FastExit (TerminateProcess) after flushing help output.
                // If it doesn't exit within timeout, kill it (safety net for hangs).
                bool exited = proc.WaitForExit(fastExitTimeoutMs);
                _lDiagStep = exited ? "fast-exit-ok" : "fast-exit-timeout-kill";
                Prof(exited
                    ? $"fast-exit: core exited naturally pid={proc.Id}"
                    : $"fast-exit: timeout {fastExitTimeoutMs}ms — kill core pid={proc.Id}");
                if (!exited)
                    try { proc.Kill(entireProcessTree: true); } catch { }
                // Wait for relay to flush all output before Launcher exits
                System.Threading.Tasks.Task.WhenAll(stdoutRelay, stderrRelay).Wait(500);
                Console.Out.Flush();
                _lDiagStep = "TerminateSelf";
                TerminateSelf(0);
                return 0; // unreachable
            }

            // Stdout-relay path (grap/grep with args):
            // Fast path: named pipe (if set up above) — Core closes pipe BEFORE TerminateProcess.
            //   Launcher gets EOF at ~20ms, outputs content, exits fast. 27s cleanup in background.
            // Fallback: relay file (WKAPPBOT_RELAY_FILE) — visible after ~27s (proc object signaled).
            // Follow mode: Core output goes directly to the terminal (Ctrl+C to stop).
            if (useStdoutRelay)
            {
                if (relayFilePath != null)
                {
                    // File-based fast path: poll for .ready (Core creates it BEFORE TerminateProcess).
                    // File created by live process → visible to GetFileAttributesW immediately (no 27s delay).
                    var readyPath = relayFilePath + ".ready";
                    var ackPath   = relayFilePath + ".ack";
                    Prof($"relay: polling .ready path={readyPath}");
                    var readyDeadline = _sw.ElapsedMilliseconds + 5000;
                    bool readyFound = false;
                    while (_sw.ElapsedMilliseconds < readyDeadline)
                    {
                        if (GetFileAttributesW(readyPath) != 0xFFFFFFFF) { readyFound = true; break; }
                        System.Threading.Thread.Sleep(5);
                    }
                    Prof(readyFound ? $"relay: .ready found pid={proc.Id}" : $"relay: .ready timeout pid={proc.Id}");
                    if (readyFound)
                    {
                        try
                        {
                            Console.OutputEncoding = new System.Text.UTF8Encoding(false); // no BOM
                            var content = System.IO.File.ReadAllText(relayFilePath, System.Text.Encoding.UTF8);
                            Console.Out.Write(content);
                            Console.Out.Flush();
                            Prof($"relay: {content.Length} chars written");
                        }
                        catch (Exception ex) { Prof($"relay: read error: {ex.Message}"); }
                        finally
                        {
                            try { System.IO.File.WriteAllText(ackPath, "1"); } catch { } // signal Core: done
                            try { System.IO.File.Delete(relayFilePath); } catch { }
                            try { System.IO.File.Delete(readyPath); } catch { }
                            try { System.IO.File.Delete(ackPath); } catch { }
                        }
                        TerminateSelf(0);
                        return 0; // unreachable
                    }
                    Prof("relay: .ready timeout → killing Core, no output");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    try { System.IO.File.Delete(relayFilePath); } catch { }
                    try { System.IO.File.Delete(readyPath); } catch { }
                    Prof($"relay: done (timeout, {_sw.ElapsedMilliseconds}ms)");
                    return 0;
                }
                else
                {
                    // Follow mode: Core output goes to terminal directly. Wait for Ctrl+C / natural exit.
                    Prof("relay-follow: Core running directly (Ctrl+C to stop)");
                    proc.WaitForExit();
                    Prof($"relay-follow: Core exited code={proc.ExitCode}");
                    return proc.ExitCode;
                }
            }

            // Normal (non-fast-exit) path
            int effectiveTimeoutMs = timeoutSec > 0 ? timeoutSec * 1000 : 0;
            if (effectiveTimeoutMs > 0)
            {
                if (!proc.WaitForExit(effectiveTimeoutMs))
                {
                    Console.Error.WriteLine($"[LAUNCHER] timeout {timeoutSec}s exceeded — killing core (pid={proc.Id})");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    return timeoutExit;
                }
            }
            else
            {
                proc.WaitForExit();
            }

            _lDiagStep = $"proc-exited(code={proc.ExitCode})";
            Prof($"proc exited code={proc.ExitCode}");
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] Failed to run core: {ex.Message}");
            return 1;
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    /// <summary>
    /// Auto-create busybox symlinks (a11y.exe, wka11y.exe → wkappbot.exe) if missing.
    /// Symlink preferred; falls back to hardlink if no permission.
    /// Stale hardlinks (size mismatch after hot-swap) are deleted and recreated.
    /// </summary>
    static void EnsureBusyboxAliases()
    {
        try
        {
            var exePath = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exePath)) return;
            var dir = Path.GetDirectoryName(exePath);
            if (string.IsNullOrEmpty(dir)) return;

            var launcherName = Path.GetFileName(exePath); // wkappbot.exe
            var coreName     = Path.GetFileNameWithoutExtension(exePath) + "-core.exe"; // wkappbot-core.exe
            var corePath     = Path.Combine(dir, coreName);
            long launcherSize = new FileInfo(exePath).Length;

            var aliasNames = BusyboxAliases.Select(x => x.name).Distinct();
            foreach (var alias in aliasNames)
            {
                var linkPath   = Path.Combine(dir, alias + ".exe");
                // grap/grep/logcat → Core directly (no Launcher relay needed, ~0.2s startup)
                var targetName = CoreDirectAliases.Contains(alias) && File.Exists(corePath)
                    ? coreName : launcherName;
                var targetSize = targetName == coreName
                    ? new FileInfo(corePath).Length : launcherSize;

                if (File.Exists(linkPath))
                {
                    bool isSymlink = File.ResolveLinkTarget(linkPath, returnFinalTarget: false) != null;
                    if (!isSymlink)
                    {
                        // Stale hardlink check
                        if (new FileInfo(linkPath).Length != targetSize)
                            try { File.Delete(linkPath); } catch { continue; }
                        else continue;
                    }
                    else continue; // symlink exists → keep as-is
                }
                else
                {
                    try { if (File.GetAttributes(linkPath) != 0) continue; } catch { }
                }

                try
                {
                    File.CreateSymbolicLink(linkPath, targetName);
                }
                catch (UnauthorizedAccessException)
                {
                    var targetFull = Path.Combine(dir, targetName);
                    try { CreateHardLink(linkPath, targetFull, IntPtr.Zero); } catch { }
                }
                catch { }
            }
        }
        catch { }
    }

    /// <summary>
    /// grap/grep one-shot: named pipe relay.
    /// Creates a named pipe server, spawns Core with WKAPPBOT_NAMED_PIPE env var (no stream redirect),
    /// reads output until Core closes the pipe (before TerminateProcess), outputs to Console, exits.
    /// Core's 27s SMB cleanup continues in the background — Launcher is already gone by then.
    /// </summary>
    static int RunGrapWithNamedPipe(string[] args)
    {
        var core = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? ".", "wkappbot-core.exe");
        if (!File.Exists(core)) { Console.Error.WriteLine($"[ERROR] wkappbot-core.exe not found at: {core}"); return 1; }

        Console.OutputEncoding = new System.Text.UTF8Encoding(false); // no BOM

        // DIAGNOSTIC: start Core with NO pipe, just env var, and see if it starts
        using var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = core,
                UseShellExecute = false,
            }
        };
        // Minimal env var to trigger startup log
        proc.StartInfo.EnvironmentVariables["WKAPPBOT_NAMED_PIPE"] = "test-no-pipe";
        foreach (var a in args)
            proc.StartInfo.ArgumentList.Add(a);

        Prof("RunGrapWithNamedPipe: proc.Start()");
        proc.Start();
        Prof($"RunGrapWithNamedPipe: waiting pid={proc.Id}");

        // Just wait 5s and see if Core reaches Main()
        System.Threading.Thread.Sleep(5000);
        Prof("RunGrapWithNamedPipe: done waiting");
        TerminateSelf(0);
        return 0; // unreachable
    }

    /// <summary>
    /// Prints grap/grep help directly from the Launcher — no Core spawn required.
    /// Kept in sync with UsageCommand.PrintGrapHelp in WKAppBot.CLI.
    /// </summary>
    static void PrintGrapHelp(string alias)
    {
        var isgrap = alias == "grap";
        Console.WriteLine($"""
            {alias} — WKAppBot log search ({(isgrap ? "grab accessible pattern, official WKAppBot name" : "legacy grep alias")})
            Internally: {alias} <pattern> [files...] → logcat <files> <pattern>

            Usage:
              {alias} <pattern> [files...] [options]

            Arguments:
              <pattern>     Regex pattern to search (case-insensitive by default)
              [files...]    File glob(s) to search, ';' OR  e.g. "*.log;*.txt"
                            Default: *.txt in current directory

            Options:
              -r, --recursive       Recursive (unlimited depth)
              -r=N                  Recursive up to depth N
              --basedir <dir>       Search root directory (default: CWD)
              --hq                  Include wkappbot.hq + openclaw log dirs
              --past <duration>     Scan files modified within duration (e.g. 1h, 30m, 2d)
                                    Without --follow: scan and exit (grep-style, one-shot)
              -f, --follow          Follow new log entries after --past scan (tail -f style)
              --timeout <duration>  Watch mode: follow for duration then exit
                                    e.g. --timeout 30s, --timeout 5m  (implies --follow)

              -i, --case-sensitive  Case-sensitive match (default: insensitive)
              -v, --invert-match    Invert match
              -l, --files-with-matches  Print filenames only
              -c, --count           Count matches per file
              -m N, --max-count N   Stop after N matches per file
              -A N                  N lines after match
              -B N                  N lines before match
              -C N                  N lines context (before + after)

            Examples:
              {alias} error                          # search *.txt in CWD
              {alias} error *.log                    # search *.log files
              {alias} "NullRef" *.log -C3            # 3 lines context
              {alias} error *.log --past 1h          # last 1h, then exit
              {alias} error *.log --past 1h -f       # last 1h, then follow
              {alias} error *.log --timeout 30s      # watch for 30 seconds
              {alias} error --hq -r                  # recursive in HQ logs
            """);
    }
}
