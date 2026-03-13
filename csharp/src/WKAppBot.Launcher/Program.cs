using System.Diagnostics;
using System.Text;

namespace WKAppBot.Launcher;

/// <summary>
/// Ultra-thin relay launcher for WKAppBot.
/// Happy path: delegates to Eye via named pipe (~200ms total, no cold-start).
/// Fallback: spawns wkappbot-core.exe directly (~3s, rare — Eye not running).
///
/// Routing control flags (parsed here, stripped before forwarding):
///   --only-eye   Eye pipe required — fail with exit 3 if Eye unavailable (no Core fallback)
///   --only-core  Skip Eye pipe — run Core directly regardless of Eye state
///
/// MCP mode (wkappbot mcp):
///   Launcher owns the stdio pipe to Claude Code and manages Core lifecycle.
///   Core can be hot-swapped without disconnecting Claude Code.
///   wkappbot mcp --reload  →  signal running MCP proxy to restart Core
///   Core exit code 42      →  self-requested reload (e.g. after self-update)
/// </summary>
class Program
{
    [STAThread]
    static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

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

        // mcp: Launcher owns stdio + Core lifecycle (hot-reload support)
        if (cmd == "mcp")
        {
            if (forwardArgs.Any(a => a == "--reload"))
            {
                // Touch reload signal file → running MCP proxy restarts Core
                var d = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
                try { File.WriteAllText(Path.Combine(d, "wkappbot.mcp-reload"), ""); }
                catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER] reload signal failed: {ex.Message}"); return 1; }
                Console.WriteLine("[LAUNCHER:MCP] reload signal sent");
                return 0;
            }
            return RunMcpProxy(forwardArgs);
        }

        // eye: IS the daemon, must run core directly
        if (!onlyCore && cmd != "eye")
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
    /// Core is (re)started behind the proxy and can be hot-swapped at any time.
    /// Reload triggers:
    ///   1. wkappbot.mcp-reload file appears in SDK/bin/
    ///   2. Core exits with code 42 (self-requested reload)
    /// On reload: if wkappbot-core.new.exe exists, it is renamed to wkappbot-core.exe first.
    /// </summary>
    static int RunMcpProxy(string[] args)
    {
        var dir    = Path.GetDirectoryName(Environment.ProcessPath) ?? ".";
        var core   = Path.Combine(dir, "wkappbot-core.exe");
        var signal = Path.Combine(dir, "wkappbot.mcp-reload");
        var staged = Path.Combine(dir, "wkappbot-core.new.exe");

        if (!File.Exists(core))
        {
            Console.Error.WriteLine($"[LAUNCHER:MCP] wkappbot-core.exe not found at: {core}");
            return 1;
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
            // Hot-swap binary if staged
            if (File.Exists(staged))
            {
                try { File.Move(staged, core, overwrite: true); Console.Error.WriteLine("[LAUNCHER:MCP] binary hot-swapped"); }
                catch (Exception ex) { Console.Error.WriteLine($"[LAUNCHER:MCP] hot-swap failed: {ex.Message}"); }
            }

            using var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName        = core,
                    UseShellExecute = false,
                    RedirectStandardInput  = true,  // fed by stdin broadcaster
                    RedirectStandardOutput = false,  // inherited → flows to Claude Code
                    RedirectStandardError  = false,  // inherited → flows to Claude Code
                    CreateNoWindow  = false,
                }
            };
            foreach (var a in args) proc.StartInfo.ArgumentList.Add(a);
            if (!string.IsNullOrEmpty(dotnetRoot))
                proc.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = dotnetRoot;

            proc.Start();
            Volatile.Write(ref _current, proc);
            Console.Error.WriteLine($"[LAUNCHER:MCP] core started (pid={proc.Id})");

            // Poll: wait for exit or reload signal (200ms tick)
            bool reload = false;
            while (!proc.WaitForExit(200))
            {
                if (!File.Exists(signal)) continue;
                try { File.Delete(signal); } catch { }
                Console.Error.WriteLine("[LAUNCHER:MCP] reload signal — restarting core...");
                Volatile.Write(ref _current, null);
                try { proc.Kill(entireProcessTree: true); } catch { }
                proc.WaitForExit();
                reload = true;
                break;
            }

            if (reload) { Thread.Sleep(50); continue; }

            // Core exited on its own
            var exit = proc.ExitCode;
            Console.Error.WriteLine($"[LAUNCHER:MCP] core exited (code={exit})");
            if (exit == 42) continue; // Core self-requested reload
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
            int timeoutSec = 0;
            for (int i = 0; i < args.Length - 1; i++)
                if (args[i] == "--timeout" && int.TryParse(args[i + 1], out var t) && t > 0)
                    { timeoutSec = t; break; }

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
                    return 2; // distinct exit code: timeout
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
}
