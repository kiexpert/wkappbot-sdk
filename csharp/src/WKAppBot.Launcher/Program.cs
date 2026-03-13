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

        // mcp: stdout is JSON-RPC, can't proxy through pipe
        // eye: IS the daemon, must run core directly
        bool skipPipe = cmd is "mcp" or "eye";

        if (!skipPipe && !onlyCore)
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
