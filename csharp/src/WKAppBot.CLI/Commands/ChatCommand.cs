using System.Diagnostics;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Claude Code CLI passthrough with rate-limit fallback to ask triad.
    ///
    ///   wkappbot chat                -> exec 'claude' (interactive, stdio inherited)
    ///   wkappbot chat "<q>"          -> 'claude -p <q>' capture + fallback on limit
    ///   wkappbot chat -p "<q>"       -> same as above
    ///   wkappbot chat --no-fallback  -> disable auto-fallback to ask triad
    ///
    /// Fallback triggers:
    ///   1. `claude` binary not found on PATH
    ///   2. stdout/stderr contains rate-limit markers
    ///   3. claude exits non-zero with a known limit signature
    /// </summary>
    static int ChatCommand(string[] args)
    {
        bool printMode = false;
        bool noFallback = false;
        var qParts = new List<string>();
        foreach (var a in args)
        {
            if (a is "-p" or "--print") printMode = true;
            else if (a is "--no-fallback") noFallback = true;
            else if (a is "--help" or "-h") { PrintChatHelp(); return 0; }
            else qParts.Add(a);
        }
        var question = string.Join(' ', qParts).Trim();
        bool interactive = string.IsNullOrEmpty(question);

        var claudeExe = ResolveClaudeExe();
        if (claudeExe == null)
        {
            if (noFallback)
            {
                Console.Error.WriteLine("[CHAT] `claude` CLI not found on PATH and --no-fallback set. Install Claude Code or drop --no-fallback.");
                return 127;
            }
            if (interactive)
            {
                Console.Error.WriteLine("[CHAT] `claude` CLI not found on PATH -- entering ask-triad REPL fallback.");
                Console.Error.WriteLine("[CHAT] Type a question and press Enter. /quit or Ctrl+D to exit. /help for tips.");
                return RunAskTriadRepl();
            }
            Console.Error.WriteLine("[CHAT] `claude` CLI not installed -- routing to ask triad");
            return AskTriadFallback(question);
        }

        if (interactive)
        {
            // Interactive: replace stdio, inherit TTY. No output capture (cannot detect limit mid-stream).
            return ExecClaudeInteractive(claudeExe);
        }

        // Print mode: capture output, scan for limit markers
        var (exit, stdout, stderr) = RunClaudePrint(claudeExe, question, printMode);
        var combined = (stdout ?? "") + "\n" + (stderr ?? "");
        bool limitHit = DetectRateLimitMarker(combined) || (exit != 0 && DetectLimitExitSignature(combined));

        if (!limitHit)
        {
            if (!string.IsNullOrEmpty(stdout)) Console.Write(stdout);
            if (!string.IsNullOrEmpty(stderr)) Console.Error.Write(stderr);
            return exit;
        }

        if (noFallback)
        {
            Console.Error.WriteLine("[CHAT] Rate-limit detected but --no-fallback set -- returning claude's output");
            if (!string.IsNullOrEmpty(stdout)) Console.Write(stdout);
            if (!string.IsNullOrEmpty(stderr)) Console.Error.Write(stderr);
            return exit == 0 ? 1 : exit;
        }

        Console.Error.WriteLine("[CHAT] Rate-limit marker detected in claude output -> routing to ask triad");
        return AskTriadFallback(question);
    }

    static void PrintChatHelp()
    {
        Console.WriteLine("chat [<question>] [-p] [--no-fallback]");
        Console.WriteLine("  Claude Code CLI passthrough with auto-fallback to ask triad on rate-limit.");
        Console.WriteLine();
        Console.WriteLine("  wkappbot chat                Interactive Claude Code session (inherits stdio).");
        Console.WriteLine("  wkappbot chat \"<q>\"          Non-interactive: claude -p <q>, fallback on limit.");
        Console.WriteLine("  wkappbot chat -p \"<q>\"       Same as above (explicit print mode).");
        Console.WriteLine("  wkappbot chat --no-fallback  Disable auto-fallback to ask triad.");
        Console.WriteLine();
        Console.WriteLine("Fallback triggers:");
        Console.WriteLine("  1) `claude` binary not on PATH -> route to ask triad");
        Console.WriteLine("  2) stdout/stderr contains: usage limit, rate limit, 5-hour limit,");
        Console.WriteLine("     session exhausted, HTTP 429, Claude is temporarily unavailable");
        Console.WriteLine("  3) exit != 0 + one of the above signatures in output");
    }

    static string? ResolveClaudeExe()
    {
        var exts = new[] { ".cmd", ".exe", ".bat", "" };

        // 1) Normal PATH walk (covers most installs when PATH was refreshed after install)
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in pathEnv.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(dir)) continue;
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p)) return p;
            }
        }

        // 2) Well-known npm global install locations on Windows. Eye often starts
        // BEFORE the user installs @anthropic-ai/claude-code via npm, so its cached
        // PATH doesn't yet include %APPDATA%\npm. Explicit probe avoids needing an
        // Eye restart.
        var fallbackDirs = new List<string>();
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!string.IsNullOrEmpty(appData))
            fallbackDirs.Add(Path.Combine(appData, "npm"));
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!string.IsNullOrEmpty(localAppData))
        {
            fallbackDirs.Add(Path.Combine(localAppData, "npm"));
            fallbackDirs.Add(Path.Combine(localAppData, "Programs", "claude")); // official installer target
        }
        fallbackDirs.Add(@"C:\Program Files\nodejs");
        fallbackDirs.Add(@"C:\Program Files (x86)\nodejs");

        foreach (var dir in fallbackDirs)
        {
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p))
                {
                    Console.Error.WriteLine($"[CHAT] claude resolved via fallback probe: {p}");
                    Console.Error.WriteLine($"[CHAT] (PATH did not include {dir} -- Eye started before install; restart Eye or add to system PATH)");
                    return p;
                }
            }
        }

        return null;
    }

    static int ExecClaudeInteractive(string claudeExe)
    {
        try
        {
            // Route through AppBotPipe.StartTracked so the focus-launch guard + CWD enforcement
            // + CreateProcessW hooks apply uniformly. Admin token inheritance is automatic
            // (StartTracked uses CreateProcessW which inherits the parent's primary token).
            var psi = new ProcessStartInfo
            {
                FileName = claudeExe,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
            };
            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "CHAT-INTERACTIVE")
                          ?? AppBotPipe.Start(psi);
            if (proc == null) { Console.Error.WriteLine("[CHAT] Failed to start claude"); return 1; }
            proc.WaitForExit();
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] interactive error: {ex.Message}");
            return 1;
        }
    }

    static (int exit, string stdout, string stderr) RunClaudePrint(string claudeExe, string question, bool printMode)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = claudeExe,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };
            psi.ArgumentList.Add("-p");
            psi.ArgumentList.Add(question);
            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "CHAT-PRINT")
                          ?? AppBotPipe.Start(psi);
            if (proc == null) return (1, "", "[CHAT] Failed to start claude");
            var outSb = new StringBuilder();
            var errSb = new StringBuilder();
            proc.OutputDataReceived += (_, e) => { if (e.Data != null) outSb.AppendLine(e.Data); };
            proc.ErrorDataReceived += (_, e) => { if (e.Data != null) errSb.AppendLine(e.Data); };
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            return (proc.ExitCode, outSb.ToString(), errSb.ToString());
        }
        catch (Exception ex)
        {
            return (1, "", $"[CHAT] print-mode error: {ex.Message}");
        }
    }

    static readonly string[] RateLimitMarkers =
    [
        "usage limit",
        "rate limit",
        "5-hour limit",
        "session exhausted",
        "Claude is temporarily unavailable",
        "HTTP 429",
        "too many requests",
        "quota exceeded",
    ];

    static bool DetectRateLimitMarker(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        foreach (var m in RateLimitMarkers)
            if (text.Contains(m, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    static bool DetectLimitExitSignature(string text)
    {
        // Broader check for non-zero exits -- e.g. "limit" appearing without the exact marker phrase
        return !string.IsNullOrEmpty(text)
            && (text.Contains("limit", StringComparison.OrdinalIgnoreCase)
                || text.Contains("429", StringComparison.Ordinal));
    }

    static int AskTriadFallback(string question)
    {
        Console.Error.WriteLine($"[CHAT:FALLBACK] ask triad \"{(question.Length > 80 ? question[..77] + "..." : question)}\"");
        return AskTriadParallel(
            question,
            timeoutSec: 180,
            attachFiles: null,
            newSession: false,
            loopMode: false,
            loopMaxSteps: 3,
            loopRetry: 1,
            loopMaxParallel: 7,
            modelHint: null,
            noWait: false,
            debateMode: false);
    }

    /// <summary>
    /// REPL loop for interactive-mode fallback when `claude` CLI is not installed.
    /// Reads a question per line, routes each to ask-triad, prints results. Exits on
    /// /quit, /exit, EOF (Ctrl+D on Unix, Ctrl+Z then Enter on Windows), or empty-line+EOF.
    /// One full cycle = user-line -> triad call -> triad reply printed -> prompt again.
    /// </summary>
    static int RunAskTriadRepl()
    {
        int cycles = 0;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("triad> ");
            Console.ResetColor();

            string? line;
            try { line = Console.ReadLine(); }
            catch (IOException) { line = null; } // stdin closed
            if (line == null)
            {
                Console.Error.WriteLine($"[CHAT] EOF -- leaving REPL after {cycles} cycle(s)");
                return 0;
            }

            var q = line.Trim();
            if (q.Length == 0) continue;

            if (q is "/quit" or "/exit" or ":q")
            {
                Console.Error.WriteLine($"[CHAT] /quit -- leaving REPL after {cycles} cycle(s)");
                return 0;
            }
            if (q is "/help" or "?")
            {
                Console.WriteLine("  /quit | /exit | :q   -- leave REPL");
                Console.WriteLine("  /help | ?            -- this help");
                Console.WriteLine("  <any question>       -- routed to ask-triad (GPT + Gemini + Claude web)");
                Console.WriteLine("  Ctrl+Z then Enter    -- EOF (Windows)");
                continue;
            }

            try
            {
                var rc = AskTriadFallback(q);
                cycles++;
                Console.Error.WriteLine($"[CHAT] cycle #{cycles} done (rc={rc})");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CHAT] triad error: {ex.Message} -- continuing");
            }
        }
    }
}
