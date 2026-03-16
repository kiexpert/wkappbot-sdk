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
        ("grap",   "logcat"), // grap → logcat alias (WKAppBot official pattern name ㅋ)
        ("scan",   "scan"),
    };

    [STAThread]
    static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Busybox-style: if invoked as a11y.exe / wka11y.exe / etc., prepend implicit command.
        // Must be done in Launcher because Core is spawned as a new process — argv[0] loses the symlink name.
        // Also auto-create missing symlinks when running as wkappbot.exe.
        var argv0      = Environment.GetCommandLineArgs().FirstOrDefault() ?? "";
        var exeBase    = Path.GetFileNameWithoutExtension(argv0).ToLowerInvariant();
        var implicitCmd = BusyboxAliases
            .Where(x => exeBase == x.name || exeBase.Contains(x.name))
            .Select(x => x.cmd)
            .FirstOrDefault();
        if (implicitCmd != null && (args.Length == 0 || args[0].ToLowerInvariant() != implicitCmd))
            args = new[] { implicitCmd }.Concat(args).ToArray();

        // Auto-create missing busybox symlinks (runs only when argv0 == wkappbot)
        if (exeBase == "wkappbot")
            EnsureBusyboxAliases();

        if (args.Length == 0)
        {
            // No args → show usage via core
            return RunCore(args);
        }

        var cmd = args[0].ToLowerInvariant();

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
            return RunMcpProxy(forwardArgs);

        // eye: IS the daemon, must run core directly
        // file: read-only utility (PDF/OCR may take several seconds) — skip Eye pipe to avoid timeout
        // logcat/grep/grap: streaming log monitor — needs direct stdout, TeeConsole, full error handling
        if (!onlyCore && cmd != "eye" && cmd != "file" && cmd != "logcat" && cmd != "grep" && cmd != "grap")
        {
            if (EyeCmdPipeClient.TryDelegate(forwardArgs, out int code))
                return code;

            if (onlyEye)
            {
                Console.Error.WriteLine("[LAUNCHER] --only-eye: Eye pipe unavailable — refusing Core fallback");
                return 3; // distinct exit: Eye required but not running
            }
        }

        return RunCore(forwardArgs);
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

    static int RunCore(string[] args)
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

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = core,
                    UseShellExecute = false,
                    RedirectStandardOutput = false, // inherit — stdout flows directly to terminal
                    RedirectStandardError = false,
                    CreateNoWindow = false,
                }
            };

            // Forward all args as-is
            foreach (var a in args)
                proc.StartInfo.ArgumentList.Add(a);

            // Propagate DOTNET_ROOT so core finds its runtime
            var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
            if (!string.IsNullOrEmpty(dotnetRoot))
                proc.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = dotnetRoot;

            proc.Start();

            if (timeoutSec > 0)
            {
                if (!proc.WaitForExit(timeoutSec * 1000))
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

            var targetName = Path.GetFileName(exePath); // wkappbot.exe
            long targetSize = new FileInfo(exePath).Length;

            var aliasNames = BusyboxAliases.Select(x => x.name).Distinct();
            foreach (var alias in aliasNames)
            {
                var linkPath = Path.Combine(dir, alias + ".exe");

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
                    else continue;
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
                    try { CreateHardLink(linkPath, exePath, IntPtr.Zero); } catch { }
                }
                catch { }
            }
        }
        catch { }
    }
}
