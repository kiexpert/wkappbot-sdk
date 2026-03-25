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
        ("wkedit", "file"),   // wkedit.exe → file edit (busybox-style)
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
        // backward-compat local alias so existing prof("...") calls in Main still work
        Action<string> prof = Prof;
        prof("Main() entered");

        // ── Encoding recovery: GetCommandLineA() → system codepage raw bytes ──
        // bash (Git Bash/MSYS2) corrupts Korean args at UTF-8↔CP949 boundary.
        // GetCommandLineA() returns raw system-codepage bytes — decode with system encoding.
        // If decoded args differ from Unicode args, the A version is the correct one.
        args = TryRecoverEncodingFromAnsiCommandLine(args);
        prof("encoding-recovery");

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
            // wkedit: prepend "file" + "edit" (two args, not one)
            var prependCmd = (exeBase == "grap" || exeBase == "grep") ? exeBase : implicitCmd;
            if (exeBase == "wkedit")
                args = new[] { "file", "edit" }.Concat(args).ToArray();
            else if (args.Length == 0 || args[0].ToLowerInvariant() != prependCmd)
                args = new[] { prependCmd }.Concat(args).ToArray();
            prof($"busybox-prepend={prependCmd}");
        }

        // ── FAST EXITS ────────────────────────────────────────────────────────────────────────────
        // Encoding is set by app.manifest activeCodePage=UTF-8 (OS load, no runtime API call needed).
        // No Console.OutputEncoding/InputEncoding assignments in Launcher — avoids SetConsoleCP(65001)
        // which initializes SMB-backed console kernel objects → ~27-35s TerminateProcess delay.

        // grap/grep with no args (or --help/-h): print help directly in Launcher — no Core needed.
        if (args.Length > 0 && args[0].ToLowerInvariant() is "grap" or "grep"
            && (args.Length == 1 || args.Any(a => a is "--help" or "-h")))
        {
            PrintGrapHelp(args[0].ToLowerInvariant());
            Console.Out.Flush();
            TerminateSelf(0);
            return 0; // unreachable
        }

        // wkappbot no-args: print usage directly in Launcher (same pattern as grap help path).
        // No Core spawn — avoids ConPTY handle issues entirely. TerminateSelf before encoding setup → fast.
        if (args.Length == 0)
        {
            prof("no-args → PrintUsage + TerminateSelf");
            PrintUsage();
            Console.Out.Flush();
            TerminateSelf(1);
            return 1; // unreachable
        }

        // Encoding: app.manifest activeCodePage=UTF-8 sets CP65001 at OS load (no runtime SetConsoleOutputCP → no SMB init → fast exit)

        // --args-file <path>: read args from UTF-8 text file (one arg per line) to bypass
        // bash→PowerShell CP949/UTF-8 mismatch that corrupts Korean command-line args.
        // Scan after busybox prepend so implicit command is already present if needed.
        // File format: one arg per line, empty lines ignored. No quoting needed.
        // Usage: printf '%s\n' a11y type "hello" > /tmp/a.txt && wkappbot --args-file /tmp/a.txt
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

        // Auto-create missing busybox symlinks (runs only when argv0 == wkappbot)
        if (exeBase == "wkappbot")
        {
            prof("EnsureBusyboxAliases start");
            EnsureBusyboxAliases();
            prof("EnsureBusyboxAliases done");
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

        // eye (no subcommand / --elevated): IS the daemon, must run core directly
        // eye tick: one-shot status query — can go through Eye pipe (fast-path if Eye running, falls to Core if not)
        // file read-pdf/ocr: may take several seconds — use --only-core for long PDF/OCR jobs
        // file edit/read/grep/glob: fast operations — route through Eye pipe for zero cold-start
        // help/no-args: fast path — skip Eye pipe, run Core directly (Core is ~22ms for help)
        // logcat/grep/grap: streaming log monitor — needs direct stdout, TeeConsole, full error handling
        var isSlowFileCmd = cmd == "file" && args.Length > 1
            && args[1].ToLowerInvariant() is "read-pdf";
        var isEyeDaemon = cmd == "eye"
            && !(forwardArgs.Length > 1 && forwardArgs[1].ToLowerInvariant() == "tick");
        if (!onlyCore && !isEyeDaemon && !isSlowFileCmd && cmd != "logcat" && cmd != "grep" && cmd != "grap"
            && cmd != "help" && cmd != "--help" && cmd != "-h")
        {
            // Parse --timeout / --timeout-exit for Eye pipe enforcement
            int eyeTimeoutMs = 0, eyeTimeoutExit = 2;
            for (int i = 0; i < forwardArgs.Length - 1; i++)
            {
                if (forwardArgs[i] == "--timeout" && int.TryParse(forwardArgs[i + 1], out var t) && t > 0) eyeTimeoutMs = t * 1000;
                if (forwardArgs[i] == "--timeout-exit" && int.TryParse(forwardArgs[i + 1], out var e)) eyeTimeoutExit = e;
            }

            prof($"Eye pipe attempt cmd={cmd}");
            if (EyeCmdPipeClient.TryDelegate(forwardArgs, out int code, eyeTimeoutMs, eyeTimeoutExit))
            {
                prof("Eye pipe: delegated");
                // TerminateSelf: avoid 27s CLR/ConPTY handle cleanup delay that keeps bash waiting.
                // All output is already flushed by TryDelegate; hard-kill Launcher immediately.
                Console.Out.Flush();
                Console.Error.Flush();
                TerminateSelf((uint)code);
                return code; // unreachable
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

        int finalCode = RunCoreDetachedNormal(forwardArgs);
        Console.Out.Flush(); Console.Error.Flush();
        TerminateSelf((uint)finalCode);
        return finalCode; // unreachable
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
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern IntPtr GetStdHandle(int nStdHandle);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool SetConsoleOutputCP(uint wCodePageID);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool FreeConsole();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    static extern IntPtr GetCommandLineA();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint GetConsoleOutputCP();
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern uint GetConsoleCP();

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
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CreatePipe(out IntPtr hRead, out IntPtr hWrite, IntPtr lpPipeAttributes, uint size);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetHandleInformation(IntPtr h, uint mask, uint flags);
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = false)]
    static extern bool ReadFile(IntPtr h, byte[] buf, uint toRead, out uint read, IntPtr ov);
    const uint HANDLE_FLAG_INHERIT = 0x1;
    const uint STARTF_USESTDHANDLES = 0x100;
    static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);
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
    /// Spawn Core with DETACHED_PROCESS + pipe handles for stdout and stderr.
    /// DETACHED_PROCESS prevents console LPC init (avoids bash/ConPTY deadlock).
    /// Returns Core's process handle and pipe read handles. Caller must CloseHandle all.
    /// Returns false on failure (caller should fall back to RunCore).
    /// </summary>
    static bool SpawnDetachedCoreWithPipes(string core, string[] args, IntPtr envBlock,
        out IntPtr hProc, out IntPtr hStdoutRead, out IntPtr hStderrRead)
    {
        hProc = hStdoutRead = hStderrRead = IntPtr.Zero;
        // Create pipes with no SA (default: non-inheritable). Then make write ends inheritable.
        if (!CreatePipe(out hStdoutRead, out var hStdoutWrite, IntPtr.Zero, 0)) return false;
        if (!CreatePipe(out hStderrRead, out var hStderrWrite, IntPtr.Zero, 0)) { CloseHandle(hStdoutRead); CloseHandle(hStdoutWrite); return false; }
        // Make write ends inheritable (child needs them); read ends stay non-inheritable.
        SetHandleInformation(hStdoutWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);
        SetHandleInformation(hStderrWrite, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT);

        var cmd = new StringBuilder($"\"{core.Replace("\"", "\\\"")}\"");
        foreach (var a in args) cmd.Append(" \"").Append(a.Replace("\"", "\\\"")).Append('"');
        var cmdArr = (cmd.ToString() + "\0").ToCharArray();
        var si = new STARTUPINFOW
        {
            cb = System.Runtime.InteropServices.Marshal.SizeOf<STARTUPINFOW>(),
            dwFlags = STARTF_USESTDHANDLES,
            hStdInput  = INVALID_HANDLE, // no stdin (DETACHED_PROCESS has no console)
            hStdOutput = hStdoutWrite,
            hStdError  = hStderrWrite,
        };
        bool ok = CreateProcessW(null, cmdArr, IntPtr.Zero, IntPtr.Zero, true, // bInheritHandles=true for pipe handles
            DETACHED_PROCESS | CREATE_BREAKAWAY_FROM_JOB | CREATE_UNICODE_ENVIRONMENT,
            envBlock, null, ref si, out var pi);
        // Close write ends in parent — child holds them; closing here causes EOF when child exits
        CloseHandle(hStdoutWrite);
        CloseHandle(hStderrWrite);
        if (!ok) { CloseHandle(hStdoutRead); CloseHandle(hStderrRead); hStdoutRead = hStderrRead = IntPtr.Zero; return false; }
        CloseHandle(pi.hThread);
        hProc = pi.hProcess;
        return true;
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
                { int fb = RunCore(args); Console.Out.Flush(); Console.Error.Flush(); TerminateSelf((uint)fb); return fb; } // fallback (was: slow 27s)
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
    /// Normal (non-fast-exit, non-relay) Core spawn via DETACHED_PROCESS + pipes.
    /// DETACHED_PROCESS prevents .NET 8 AppHost console LPC deadlock in bash/ConPTY context.
    /// Relays stdout in real-time (with "\0UIT" sentinel support) and stderr in background.
    /// Falls back to RunCore() if detached spawn fails (e.g., non-bash context).
    /// </summary>
    static int RunCoreDetachedNormal(string[] args)
    {
        var dir  = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core = Path.Combine(dir, "wkappbot-core.exe");
        if (!File.Exists(core)) return RunCore(args); // fallback

        // Parse --timeout/--timeout-exit (same as RunCore)
        int timeoutSec = 0, timeoutExit = 2;
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--timeout"      && int.TryParse(args[i + 1], out var t) && t > 0) timeoutSec  = t;
            if (args[i] == "--timeout-exit" && int.TryParse(args[i + 1], out var e))           timeoutExit = e;
        }

        // Build env: current process env minus MSYS2/PTY vars (no relay file for normal path)
        var strip = new System.Collections.Generic.HashSet<string>(System.StringComparer.OrdinalIgnoreCase)
        {
            "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL", "ConEmuANSI",
            "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST", "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE",
        };
        var envSb = new StringBuilder();
        foreach (System.Collections.DictionaryEntry kv in Environment.GetEnvironmentVariables())
        {
            var k = kv.Key?.ToString() ?? "";
            if (strip.Contains(k)) continue;
            envSb.Append(k).Append('=').Append(kv.Value?.ToString() ?? "").Append('\0');
        }
        envSb.Append('\0');
        var envBytes = Encoding.Unicode.GetBytes(envSb.ToString());
        var envPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(envBytes.Length);
        System.Runtime.InteropServices.Marshal.Copy(envBytes, 0, envPtr, envBytes.Length);

        try
        {
            if (!SpawnDetachedCoreWithPipes(core, args, envPtr, out var hProc, out var hOut, out var hErr))
            {
                Prof("detached-normal spawn failed → RunCore fallback");
                return RunCore(args); // fallback
            }
            Prof($"detached-normal spawned, relaying stdout");

            // Relay stderr in background
            var _stderr = Console.OpenStandardError();
            _ = System.Threading.Tasks.Task.Run(() =>
            {
                var b = new byte[4096];
                while (ReadFile(hErr, b, (uint)b.Length, out uint n, IntPtr.Zero) && n > 0)
                    { _stderr.Write(b, 0, (int)n); _stderr.Flush(); }
            });

            // Relay stdout in real-time with sentinel detection
            var _stdout = Console.OpenStandardOutput();
            int effectiveTimeoutMs = timeoutSec > 0 ? timeoutSec * 1000 : 0;
            var sentinel = new byte[] { 0, (byte)'U', (byte)'I', (byte)'T' };
            var relayBuf = new byte[65536];
            uint exitCode = 0;

            while (ReadFile(hOut, relayBuf, (uint)relayBuf.Length, out uint n, IntPtr.Zero) && n > 0)
            {
                // Exact 4-byte sentinel → TerminateSelf immediately
                if (n == 4 && relayBuf[0] == sentinel[0] && relayBuf[1] == sentinel[1]
                           && relayBuf[2] == sentinel[2] && relayBuf[3] == sentinel[3])
                {
                    Prof("detached-normal: \\0UIT sentinel → TerminateSelf");
                    try { _stdout.Flush(); CloseHandle(GetStdHandle(-11)); } catch { }
                    WaitForSingleObject(hProc, 500);
                    GetExitCodeProcess(hProc, out exitCode);
                    CloseHandle(hOut); CloseHandle(hErr); CloseHandle(hProc);
                    TerminateSelf(exitCode);
                    return (int)exitCode; // unreachable
                }

                // Timeout check: Launcher exits, Core continues running (DETACHED_PROCESS)
                if (effectiveTimeoutMs > 0 && _sw.ElapsedMilliseconds > effectiveTimeoutMs)
                {
                    Console.Error.WriteLine($"[LAUNCHER] timeout {timeoutSec}s — Launcher exiting, Core continues in background");
                    try { _stdout.Flush(); } catch { }
                    // Do NOT kill Core — it's detached and may still be working (e.g. agent triad)
                    CloseHandle(hOut); CloseHandle(hErr); CloseHandle(hProc);
                    TerminateSelf((uint)timeoutExit);
                    return timeoutExit; // unreachable
                }

                _stdout.Write(relayBuf, 0, (int)n);
                _stdout.Flush();
            }

            // EOF — Core exited normally
            try { _stdout.Flush(); } catch { }
            WaitForSingleObject(hProc, 500);
            GetExitCodeProcess(hProc, out exitCode);
            CloseHandle(hOut); CloseHandle(hErr); CloseHandle(hProc);
            return exitCode == 259 ? 0 : (int)exitCode; // 259 = STILL_ACTIVE (shouldn't happen)
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(envPtr);
        }
    }

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

        // stdin broadcaster — single reader, routes bytes to whichever Core is current
        Process? _current = null;
        var coreReady = new ManualResetEventSlim(false);
        var stdinThread = new Thread(() =>
        {
            var stdin = Console.OpenStandardInput();
            var buf   = new byte[4096];
            Console.Error.WriteLine("[LAUNCHER:MCP] stdin relay thread started");
            while (true)
            {
                int n;
                try   { n = stdin.Read(buf, 0, buf.Length); }
                catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER:MCP] stdin read error: {ex.Message}"); break; }
                if (n == 0) { Console.Error.WriteLine("[LAUNCHER:MCP] stdin EOF"); break; }

                Console.Error.WriteLine($"[LAUNCHER:MCP] stdin read {n} bytes, waiting for core...");
                // Wait for Core to be ready (prevents dropping first JSON-RPC message)
                coreReady.Wait();
                var p = Volatile.Read(ref _current);
                if (p == null) { Console.Error.WriteLine("[LAUNCHER:MCP] stdin: core is null, dropping"); continue; }
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
                    RedirectStandardInput  = true,     // relay: Launcher reads caller stdin → writes to Core
                    RedirectStandardOutput = true,     // relay: Launcher reads Core stdout → writes to caller
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
            coreReady.Set(); // unblock stdin relay
            Console.Error.WriteLine($"[LAUNCHER:MCP] core started (pid={proc.Id})");

            // Relay Core stdout → our stdout (JSON-RPC passthrough)
            var stdoutRelay = new Thread(() =>
            {
                try
                {
                    var src = proc.StandardOutput.BaseStream;
                    var dst = Console.OpenStandardOutput();
                    var buf = new byte[4096];
                    int n;
                    while ((n = src.Read(buf, 0, buf.Length)) > 0)
                    {
                        dst.Write(buf, 0, n);
                        dst.Flush();
                    }
                }
                catch { }
            }) { IsBackground = true, Name = "mcp-stdout-relay" };
            stdoutRelay.Start();

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
    /// <summary>
    /// Spawn Core with CreateNoWindow=true and stdin piped (no ConPTY inheritance).
    /// stdout/stderr are NOT redirected — Core writes directly to Launcher's terminal handles.
    /// This avoids both: the ~27s ConPTY bash wait AND the unreliable pipe relay mechanism.
    /// </summary>
    static int RunCoreNoConPty(string[] args)
    {
        try
        {
            var dir = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
            var core = Path.Combine(dir, "wkappbot-core.exe");
            if (!File.Exists(core)) { Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found at: {core}"); return 1; }

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = core,
                    UseShellExecute       = false,
                    CreateNoWindow        = true,  // no ConPTY handle → bash won't wait 27s
                    RedirectStandardInput = true,  // pipe stdin → Core holds no ConPTY stdin
                    RedirectStandardOutput = false, // Core writes directly to Launcher's stdout handle
                    RedirectStandardError  = false, // Core writes directly to Launcher's stderr handle
                }
            };
            // Strip MSYS2/Cygwin PTY env vars — AppHost deadlock prevention (same as fastExit path)
            foreach (var v in new[] { "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL",
                                      "ConEmuANSI", "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST",
                                      "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE" })
                proc.StartInfo.EnvironmentVariables.Remove(v);
            foreach (var a in args) proc.StartInfo.ArgumentList.Add(a);

            proc.Start();
            try { proc.StandardInput.Close(); } catch { } // EOF stdin immediately
            // Use WaitForExit(timeout) instead of blocking WaitForExit() — avoids potential
            // .NET internal async-stream-completion wait that can block 27-35s in AOT builds.
            if (!proc.WaitForExit(5000)) proc.Kill(entireProcessTree: false);
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] RunCoreNoConPty error: {ex.Message}");
            return 1;
        }
    }

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
                    // ALL 3 streams redirected + CreateNoWindow=true for ALL spawns.
                    // Root cause of bash/MSYS2 LPC deadlock: ConPTY slave handles inherited via
                    // stdin/stderr trigger .NET 8 AppHost console init (SetConsoleMode LPC → csrss.exe).
                    // Fix: redirect stdin+stderr so Core receives pipes (not ConPTY handles).
                    // Core detects piped stdin → Console.IsInputRedirected=true → skips interactive init.
                    // "\0UIT" (4 bytes, exact flush) = Core signaling Launcher to TerminateSelf immediately.
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    RedirectStandardInput  = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding  = System.Text.Encoding.UTF8,
                    // CreateNoWindow=true: prevents console window creation for ALL Core spawns.
                    // Together with all-streams-redirected, Core has zero ConPTY handles → no LPC init.
                    CreateNoWindow         = true,
                }
            };
            // File-based handshake: Core creates .ready BEFORE TerminateProcess (file visible immediately).
            // Launcher polls for .ready, reads relay file while Core is alive, then creates .ack.
            // Core waits for .ack (max 500ms), then calls TerminateProcess.
            // No named kernel objects — avoids ConPTY/session namespace issues.

            // Pass relay file path to Core via env var (one-shot only)
            if (relayFilePath != null)
                proc.StartInfo.EnvironmentVariables["WKAPPBOT_RELAY_FILE"] = relayFilePath;

            // Strip MSYS2/Cygwin PTY env vars for ALL Core spawns (not just fast-exit).
            // .NET 8 single-file AppHost deadlocks when TERM/MSYSTEM/MSYS/etc. are set,
            // regardless of CreateNoWindow mode. These vars tell the AppHost to reconcile
            // PTY handles at startup, causing an LPC deadlock before Main() is reached.
            foreach (var v in new[] { "TERM", "MSYSTEM", "MSYS", "MSYS2_ARG_CONV_EXCL",
                                      "ConEmuANSI", "CYGWIN", "MINGW_PREFIX", "MINGW_CHOST",
                                      "MINGW_PACKAGE_PREFIX", "MSYS2_PATH_TYPE" })
                proc.StartInfo.EnvironmentVariables.Remove(v);

            // Forward all args as-is
            foreach (var a in args)
                proc.StartInfo.ArgumentList.Add(a);


            _lDiagStep = "proc-starting";
            Prof("proc.Start()");
            // Detach Launcher from ConPTY console BEFORE spawning Core.
            // CREATE_NO_WINDOW still attaches Core to parent's ConPTY console session → LPC deadlock
            // in .NET 8 AppHost during console init (SetConsoleMode → csrss.exe LPC → never returns).
            // FreeConsole(): Launcher releases its console → child inherits nothing → no LPC deadlock.
            // Stdout/stderr file handles (ConPTY slave) remain valid for WriteFile after FreeConsole().
            FreeConsole();
            proc.Start();
            // Close stdin immediately — Core doesn't read stdin interactively.
            // Stdin is always redirected (pipe) so Core never inherits a ConPTY stdin handle.
            try { proc.StandardInput.Close(); } catch { }
            _lDiagStep = $"proc-waitforexit(pid={proc.Id})";
            Prof($"proc.WaitForExit pid={proc.Id}");

            if (useFastExit)
            {
                // stdin already closed above.
                // Relay Core's stdout and stderr to Launcher's stdout/stderr in background.
                // DO NOT set Console.OutputEncoding here — that calls SetConsoleOutputCP(65001),
                // initializing SMB-backed console kernel objects. TerminateSelf after that = ~27s.
                // Instead, write bytes directly to the underlying stdout stream (bypasses encoding).
                var stdoutStream = Console.OpenStandardOutput();
                var stderrStream = Console.OpenStandardError();
                var stdoutRelay = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new byte[4096];
                        int n;
                        while ((n = proc.StandardOutput.BaseStream.Read(buf, 0, buf.Length)) > 0)
                        {
                            stdoutStream.Write(buf, 0, n);
                            stdoutStream.Flush();
                        }
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
                        {
                            stderrStream.Write(buf, 0, n);
                            stderrStream.Flush();
                        }
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
                stdoutStream.Flush();
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
                    // Follow mode: relay stdout+stderr in background, wait for Ctrl+C / natural exit.
                    Prof("relay-follow: Core running (Ctrl+C to stop)");
                    var fStdout = Console.OpenStandardOutput();
                    var fStderr = Console.OpenStandardError();
                    var fOut = System.Threading.Tasks.Task.Run(() => {
                        try { var b = new byte[4096]; int n;
                              while ((n = proc.StandardOutput.BaseStream.Read(b,0,b.Length)) > 0) { fStdout.Write(b,0,n); fStdout.Flush(); } } catch { }
                    });
                    var fErr = System.Threading.Tasks.Task.Run(() => {
                        try { var b = new byte[4096]; int n;
                              while ((n = proc.StandardError.BaseStream.Read(b,0,b.Length)) > 0) { fStderr.Write(b,0,n); fStderr.Flush(); } } catch { }
                    });
                    proc.WaitForExit();
                    System.Threading.Tasks.Task.WhenAll(fOut, fErr).Wait(500);
                    Prof($"relay-follow: Core exited code={proc.ExitCode}");
                    return proc.ExitCode;
                }
            }

            // Normal path: relay Core's stdout to Launcher's stdout in real-time.
            // Detect "\0UIT" sentinel (exactly 4 bytes, Core-flushed) → TerminateSelf immediately.
            // Core's ~30s SMB console cleanup runs in background; bash gets control back right away.
            // Relay stderr in background — stderr is now always redirected (avoids ConPTY inheritance).
            var _stderr = Console.OpenStandardError();
            _ = System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var buf = new byte[4096];
                    int n;
                    while ((n = proc.StandardError.BaseStream.Read(buf, 0, buf.Length)) > 0)
                    { _stderr.Write(buf, 0, n); _stderr.Flush(); }
                }
                catch { }
            });
            var _stdout = Console.OpenStandardOutput();
            int effectiveTimeoutMs = timeoutSec > 0 ? timeoutSec * 1000 : 0;
            var relayBuf = new byte[65536];
            var sentinel = new byte[] { 0, (byte)'U', (byte)'I', (byte)'T' };
            int coreExitFromRelay = -1;
            while (true)
            {
                int n;
                try { n = proc.StandardOutput.BaseStream.Read(relayBuf, 0, relayBuf.Length); }
                catch { break; }
                if (n == 0) break; // EOF — Core exited normally

                // Exact 4-byte sentinel check (Core flushes exactly these 4 bytes as a signal)
                if (n == 4 && relayBuf[0] == sentinel[0] && relayBuf[1] == sentinel[1]
                           && relayBuf[2] == sentinel[2] && relayBuf[3] == sentinel[3])
                {
                    Prof($"relay: \\0UIT sentinel received — TerminateSelf immediately (Core cleanup in bg)");
                    try { _stdout.Flush(); } catch { }
                    // Close stdout handle → pipe gets EOF immediately so bash pipeline completes right away.
                    // Core's 30s SMB cleanup continues in background; bash doesn't need to wait.
                    try { CloseHandle(GetStdHandle(-11)); } catch { }
                    // Try to get Core's exit code (Core calls TerminateProcess right after sentinel)
                    coreExitFromRelay = proc.WaitForExit(500) ? proc.ExitCode : 0;
                    TerminateSelf((uint)coreExitFromRelay);
                    return coreExitFromRelay; // unreachable
                }

                // Timeout check (--timeout N flag)
                if (effectiveTimeoutMs > 0 && _sw.ElapsedMilliseconds > effectiveTimeoutMs)
                {
                    Console.Error.WriteLine($"[LAUNCHER] timeout {timeoutSec}s exceeded — killing core (pid={proc.Id})");
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    try { _stdout.Flush(); } catch { }
                    TerminateSelf((uint)timeoutExit);
                    return timeoutExit; // unreachable
                }

                try { _stdout.Write(relayBuf, 0, n); _stdout.Flush(); } catch { break; }
            }
            try { _stdout.Flush(); } catch { }
            if (!proc.HasExited) proc.WaitForExit();

            _lDiagStep = $"proc-exited(code={proc.ExitCode})";
            Prof($"proc exited code={proc.ExitCode}");
            int coreExitCode = proc.ExitCode;
            Console.Out.Flush();
            Console.Error.Flush();
            // TerminateSelf: avoid 27s CLR/ConPTY handle cleanup delay after Core exits.
            // Core's output is already written (inherited handles); hard-kill Launcher immediately.
            TerminateSelf((uint)coreExitCode);
            return coreExitCode; // unreachable
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] Failed to run core: {ex.Message}");
            return 1;
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
    static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

    // ── Encoding recovery ────────────────────────────────────────────────────
    /// <summary>
    /// Recover Korean (and other multibyte) args corrupted by bash/MSYS2.
    /// GetCommandLineA() → raw system-codepage bytes → decode with system encoding.
    /// Compare each arg: if Unicode version has replacement chars (U+FFFD) or
    /// differs from ANSI-decoded version, use the ANSI version.
    /// </summary>
    static string[] TryRecoverEncodingFromAnsiCommandLine(string[] args)
    {
        try
        {
            var ansiPtr = GetCommandLineA();
            if (ansiPtr == IntPtr.Zero) return args;

            // Read raw ANSI command line as bytes
            var ansiStr = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ansiPtr);
            if (string.IsNullOrEmpty(ansiStr)) return args;

            // Parse ANSI command line into args (same logic as Windows CommandLineToArgvW but for ANSI)
            var ansiArgs = ParseCommandLineArgs(ansiStr);
            if (ansiArgs.Length == 0) return args;

            // Skip argv[0] (exe path) — compare only actual args
            var unicodeAll = Environment.GetCommandLineArgs();
            if (unicodeAll.Length <= 1) return args;

            var unicodeArgs = unicodeAll[1..]; // skip exe
            if (ansiArgs.Length <= 0) return args;

            // Also decode using system codepage for comparison
            // Marshal.PtrToStringAnsi uses system ANSI codepage (CP949 on Korean Windows)
            bool anyRecovered = false;
            var result = new string[args.Length];
            Array.Copy(args, result, args.Length);

            for (int i = 0; i < result.Length && i < ansiArgs.Length; i++)
            {
                var uArg = result[i];
                var aArg = ansiArgs[i];

                // Check if Unicode version has corruption indicators
                bool hasReplacementChar = uArg.Contains('\uFFFD');
                bool hasMojibake = false;

                // Detect common mojibake: high bytes in Latin range that shouldn't be there
                // e.g. "í\u0085\u008c스í\u008a\u00b8" instead of "테스트"
                if (!hasReplacementChar && uArg.Length > 0)
                {
                    int suspiciousChars = 0;
                    foreach (var c in uArg)
                    {
                        if (c >= 0x80 && c <= 0xFF) suspiciousChars++;
                    }
                    // If >30% of chars are in the suspicious range, likely mojibake
                    hasMojibake = suspiciousChars > 0 && (double)suspiciousChars / uArg.Length > 0.3;
                }

                if ((hasReplacementChar || hasMojibake) && aArg != uArg && aArg.Length > 0)
                {
                    result[i] = aArg;
                    anyRecovered = true;
                    Console.Error.WriteLine($"[ENCODING] Recovered arg[{i}]: \"{uArg}\" → \"{aArg}\" (system codepage)");
                }
            }

            if (anyRecovered)
                Console.Error.WriteLine($"[ENCODING] {result.Length} arg(s) checked, recovery applied (codepage={GetConsoleCP()})");

            return result;
        }
        catch
        {
            return args; // any failure → use original args
        }
    }

    /// <summary>Simple command line parser (handles quoted args).</summary>
    static string[] ParseCommandLineArgs(string cmdLine)
    {
        var args = new System.Collections.Generic.List<string>();
        int i = 0;

        // Skip executable path (first arg)
        if (i < cmdLine.Length)
        {
            if (cmdLine[i] == '"')
            {
                i++; while (i < cmdLine.Length && cmdLine[i] != '"') i++; i++; // skip closing quote
            }
            else
            {
                while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
            }
            while (i < cmdLine.Length && cmdLine[i] == ' ') i++; // skip spaces
        }

        // Parse remaining args
        while (i < cmdLine.Length)
        {
            if (cmdLine[i] == '"')
            {
                i++; // skip opening quote
                int start = i;
                while (i < cmdLine.Length && cmdLine[i] != '"') i++;
                args.Add(cmdLine[start..i]);
                if (i < cmdLine.Length) i++; // skip closing quote
            }
            else
            {
                int start = i;
                while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
                args.Add(cmdLine[start..i]);
            }
            while (i < cmdLine.Length && cmdLine[i] == ' ') i++;
        }

        return args.ToArray();
    }

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
    /// Prints wkappbot usage directly from the Launcher — no Core spawn required.
    /// Kept in sync with UsageCommand.GetUsageText in WKAppBot.CLI.
    /// Called from no-args fast path (before Console.OutputEncoding setup) → TerminateSelf is fast.
    /// </summary>
    static void PrintUsage()
    {
        var ver = typeof(Program).Assembly.GetName().Version;
        var verStr = ver != null ? $" v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
        Console.Write($@"
WKAppBot{verStr} - Windows App Automation Test Framework
Building the Eyes of Claude to realize WilKim's vision of autonomous secretarial ops.
All AI agents welcome — Claude, GPT, Gemini, Copilot, and beyond.
Your testing, coding, and ideas are appreciated. Let's build together.

Usage:
  wkappbot <command> [options]

═══ Public Commands ═══════════════════════════════════════════

  a11y <action> <grap>[#uia-scope] [options]        (alias: a11y.exe / wka11y.exe)
      Universal accessibility interface — 20 standard actions for ANY window.
      3-tier fallback: UIA → Win32 → SendInput. Busybox: symlink `a11y.exe` works.

      Auto-pipeline per action: blocker dismiss → minimize restore → tab activate
        → zoom/magnifier → execute (3-tier) → result feedback (green/amber) → fade

      Window (7):  close  minimize  maximize  restore  focus  move  resize
      Element (13): read  find  highlight  invoke  click  toggle
                    expand  collapse  select  scroll  type  set-value  set-range
      Utility:     clipboard  clipboard-read  clipboard-write (text/files/mixed)

      Target:  --nth 3 | 3~ | ~3 | 2~4    --all    (default: first match)
      Options: --depth N  --force  --value N  --direction  --amount
      Grap:    ';' OR  #scope  Ex: ""*Notepad*#*File*"" → Notepad's File menu

      a11y find ""*app*"" --depth 5       # MUD: look (Win32 + UIA children)
      a11y highlight ""*app*#*button*""   # visualize target with zoom overlay
      a11y invoke ""*app*#*button*""      # click (UIA Invoke → BM_CLICK → SendInput)
      a11y close ""*Chrome*"" --nth 2~    # close 2nd window onwards
      a11y type ""*app*#*edit*"" ""hello""

  find <keyword> [--deep] [--limit N] [--process <name>]
      Unified search: window titles + UIA accessibility elements.
      --deep: Thorough search (depth 12, slower but finds more).
  run <scenario.yaml> [-v] [--no-watch]
      Run a YAML test scenario with background element tracking.
  do <window-title> <form-id> <button-text> [--confirm]
      Full automation: combo select + button click + dialog handling.
  scan <window-title> [--save] [--ocr] [--detail]
      Scan app structure, learn controls, build Experience DB.
  ocr <window-title|image.png> [--save] [-o file]
      Extract text from window/image using Windows.Media.Ocr.
  capture <window-title> [-o output.png] [--form <id>]
      Capture a screenshot of a window or MDI child form.
  dismiss <window-title> [keywords...]
      Auto-dismiss notice/popup windows (OCR importance check).
  input <window-title> <text>
      Type text into a window (focusless PostMessage preferred).
  eye [--interval N] [--size WxH] [--pos X,Y]
      WK AppBot Eye — live overlay + Slack daemon (always on).
      ctx=N% in tick output, auto-deletes stale idle messages on restart.
  newchat ""prompt"" [--file prompt.txt]
      Open new Claude Desktop chat + submit prompt (all focusless UIA).
      Use for session handoff when context reaches 90%+.
  slack send|reply|upload|screenshot|listen|catch-up
      Slack messaging (Socket Mode, always-on prompt forwarding).
  mcp
      Start MCP stdio server (wkappbot_cli tool for JSON-RPC clients).
  web fetch|search|read|html
      Chrome DevTools Protocol web automation.
  file read|grep|glob
      File reading, search, and pattern matching.
  ask gpt|gemini|claude|triad ""question"" [file.png]
      Ask AI via CDP (focusless). ask triad = parallel 3-way.
  logcat [regex] [files] [--past N] [-f] [--timeout N] [--hq]
      Stream/search logs. grep/grap = aliases with grep-compat arg order.

General Options:
  -v, --verbose         Verbose output
  --timeout <duration>  Hard kill after N time (exit 124). e.g. 30, 2m, 500ms
  -h, --help            Show this help message

Data Directory:
  Runtime data (profiles, logs, handlers, output) stored in:
  {{exe_dir}}/wkappbot.hq/

Run 'wkappbot --help' for full command reference.
");
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

/// <summary>
/// Minimal focus-restore helper for Launcher (no Win32 project reference available).
/// Only use for restore patterns — not for acquiring focus.
/// </summary>
static class FocusGuard
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
}
