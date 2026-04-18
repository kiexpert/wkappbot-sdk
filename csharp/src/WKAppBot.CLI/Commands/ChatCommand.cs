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
    // Supported shell targets. AI entries run as REPL via NonBlockingLineReader;
    // OS shells (cmd/powershell/bash) are stdio-inherited subprocesses.
    static readonly HashSet<string> _shellAis =
        new(StringComparer.OrdinalIgnoreCase) { "gpt", "gemini", "triad" };
    static readonly HashSet<string> _shellOsNames =
        new(StringComparer.OrdinalIgnoreCase) { "cmd", "powershell", "pwsh", "bash" };

    /// <summary>Path of persisted default-shell pointer. One line, shell name.</summary>
    static string ChatDefaultShellFile => Path.Combine(DataDir, "chat-default.txt");

    static string GetDefaultShell()
    {
        try
        {
            if (File.Exists(ChatDefaultShellFile))
            {
                var v = File.ReadAllText(ChatDefaultShellFile).Trim().ToLowerInvariant();
                if (!string.IsNullOrEmpty(v)) return v;
            }
        }
        catch { }
        return "claude"; // bootstrap default
    }

    static int SetDefaultShell(string shell)
    {
        shell = shell.Trim().ToLowerInvariant();
        if (shell != "claude" && !_shellAis.Contains(shell) && !_shellOsNames.Contains(shell))
        {
            Console.Error.WriteLine($"[CHAT] --set-default: unknown shell '{shell}'. Valid: claude | cmd | powershell | bash | gemini | gpt | triad");
            return 2;
        }
        try
        {
            Directory.CreateDirectory(DataDir);
            File.WriteAllText(ChatDefaultShellFile, shell + "\n");
            Console.WriteLine($"[CHAT] default shell set to '{shell}' -> {ChatDefaultShellFile}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] --set-default failed: {ex.Message}");
            return 1;
        }
    }

    static int ChatCommand(string[] args)
    {
        bool printMode = false;
        bool noFallback = false;
        string? explicitShell = null;
        var qParts = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a is "-p" or "--print") printMode = true;
            else if (a is "--no-fallback") noFallback = true;
            else if (a is "--help" or "-h") { PrintChatHelp(); return 0; }
            else if (a == "--shell" && i + 1 < args.Length)
                explicitShell = args[++i].ToLowerInvariant();
            else if (a == "--set-default" && i + 1 < args.Length)
                return SetDefaultShell(args[++i]);
            else if (a == "--ai" && i + 1 < args.Length)
            {
                var picked = args[++i].ToLowerInvariant();
                if (picked is "gpt" or "gemini" or "claude" or "triad")
                    _chatFallbackAi = picked;
                else
                {
                    Console.Error.WriteLine($"[CHAT] --ai {picked} invalid. Use gpt|gemini|claude|triad.");
                    return 2;
                }
            }
            // First positional arg as shell-name shortcut: "wkappbot chat cmd" / "chat gemini".
            // Only when it exactly matches a known shell AND no question-like args preceded it.
            else if (explicitShell == null && qParts.Count == 0 &&
                     (_shellAis.Contains(a) || _shellOsNames.Contains(a) || a.Equals("claude", StringComparison.OrdinalIgnoreCase)))
                explicitShell = a.ToLowerInvariant();
            else qParts.Add(a);
        }

        // Resolve target shell: explicit flag > positional > persisted default.
        var shell = explicitShell ?? GetDefaultShell();

        // OS-shell route: stdio-inherited subprocess. No question mode, no fallback.
        if (_shellOsNames.Contains(shell))
            return ExecOsShell(shell);

        // AI-REPL route: reuse the existing ask-* REPL.
        if (_shellAis.Contains(shell))
        {
            _chatFallbackAi = shell;
            // Accept a one-shot question through the AI route too.
            var aq = string.Join(' ', qParts).Trim();
            if (!string.IsNullOrEmpty(aq)) return AskSingleAiFallback(aq, shell);
            Console.Error.WriteLine($"[CHAT] shell=ask-{shell} -- REPL mode. /quit or Ctrl+D to exit.");
            return RunAskTriadRepl();
        }

        // Default: claude shell (rate-limit-aware passthrough, existing behavior).
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
                Console.Error.WriteLine($"[CHAT] `claude` CLI not found on PATH -- entering ask-{_chatFallbackAi} REPL fallback.");
                Console.Error.WriteLine("[CHAT] Type a question and press Enter. /quit or Ctrl+D to exit. /help for tips.");
                return RunAskTriadRepl();
            }
            Console.Error.WriteLine($"[CHAT] `claude` CLI not installed -- routing to ask {_chatFallbackAi}");
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
        Console.WriteLine("chat [<shell>|<question>] [-p] [--no-fallback] [--shell S] [--ai AI] [--set-default S]");
        Console.WriteLine("  Unified shell router: pick a shell target to pipe into.");
        Console.WriteLine();
        Console.WriteLine("Shell targets:");
        Console.WriteLine("  claude           Claude Code CLI (rate-limit-aware, fallback on limit).");
        Console.WriteLine("  cmd              Windows cmd.exe (stdio inherited).");
        Console.WriteLine("  powershell|pwsh  Windows PowerShell.");
        Console.WriteLine("  bash             Git Bash or WSL bash.");
        Console.WriteLine("  gemini|gpt|triad AI REPL via NonBlockingLineReader (type-ahead works).");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot chat                   Use persisted default shell (init=claude).");
        Console.WriteLine("  wkappbot chat cmd               Launch cmd.exe.");
        Console.WriteLine("  wkappbot chat gemini            Gemini REPL loop.");
        Console.WriteLine("  wkappbot chat \"<q>\"             One-shot: claude -p <q>, fallback on limit.");
        Console.WriteLine("  wkappbot chat gemini \"<q>\"      One-shot via Gemini.");
        Console.WriteLine("  wkappbot chat --shell bash      Force bash shell (disambiguates arg vs shell).");
        Console.WriteLine("  wkappbot chat --set-default cmd Persist cmd as default shell.");
        Console.WriteLine("  wkappbot chat --ai gpt          Claude fallback AI override (default gemini).");
        Console.WriteLine("  wkappbot chat --no-fallback     Disable auto-fallback on claude rate-limit.");
        Console.WriteLine();
        Console.WriteLine("Rate-limit fallback (claude shell only):");
        Console.WriteLine("  Triggered by: 'usage limit', 'rate limit', '5-hour limit',");
        Console.WriteLine("                'session exhausted', 'HTTP 429'");
        Console.WriteLine("  Routes to: ask <--ai>  (default gemini; cheap single-AI, not triad).");
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

    /// <summary>
    /// Launch an OS-shell subprocess with inherited stdio. No question mode, no
    /// rate-limit fallback -- the shell manages its own lifecycle. Exit code
    /// passthrough. Used for "wkappbot chat cmd / powershell / bash".
    /// </summary>
    static int ExecOsShell(string shellName)
    {
        var exe = shellName switch
        {
            "cmd"        => "cmd.exe",
            "powershell" => "powershell.exe",
            "pwsh"       => "pwsh.exe",
            "bash"       => ResolveBashExe() ?? "bash.exe",
            _            => shellName, // caller already validated
        };

        try
        {
            Console.Error.WriteLine($"[CHAT] launching shell: {exe}  (Ctrl+D / exit to return)");
            var psi = new ProcessStartInfo
            {
                FileName = exe,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
            };
            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "CHAT-SHELL")
                          ?? AppBotPipe.Start(psi);
            if (proc == null) { Console.Error.WriteLine("[CHAT] failed to start shell"); return 1; }
            proc.WaitForExit();
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] shell '{shellName}' error: {ex.Message}");
            return 1;
        }
    }

    static string? ResolveBashExe()
    {
        foreach (var p in new[]
        {
            @"C:\Program Files\Git\bin\bash.exe",
            @"C:\Program Files\Git\usr\bin\bash.exe",
            @"C:\Windows\System32\bash.exe", // WSL wrapper
        })
            if (File.Exists(p)) return p;
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

    // Default fallback AI. Triad (GPT+Gemini+Claude parallel debate) is overkill for
    // a casual chat and has known bugs + long runtimes -- bad UX for a first-response
    // path. Gemini is the current default: fastest to reply, single stream to read,
    // simplest to debug when something goes wrong. Override with --ai gpt|claude|triad.
    static string _chatFallbackAi = "gemini";

    static int AskSingleAiFallback(string question, string ai)
    {
        var preview = question.Length > 80 ? question[..77] + "..." : question;
        Console.Error.WriteLine($"[CHAT:FALLBACK] ask {ai} \"{preview}\"");
        if (ai == "triad")
        {
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
        // Delegate to `wkappbot ask <ai> <question>` which is the same code path an
        // operator would hit typing the ask command by hand -- keeps the fallback in
        // sync with any future improvements to single-AI ask flows.
        return AskCommand(new[] { ai, question });
    }

    static int AskTriadFallback(string question) => AskSingleAiFallback(question, _chatFallbackAi);

    /// <summary>
    /// REPL loop for interactive-mode fallback when `claude` CLI is not installed.
    /// Uses the shared <see cref="WKAppBot.Shared.NonBlockingLineReader"/> so the
    /// user can type the NEXT question while the AI is still answering the current
    /// one -- their keystrokes land in a thread-safe queue and are consumed on the
    /// next iteration with no visible lag.
    ///
    /// Terminal-native line editing (Backspace, arrow keys, shell history) is
    /// preserved because the reader calls Console.ReadLine() under the hood.
    /// Real-time character streaming / bottom-line-pinned prompts are a future
    /// enhancement; this layer covers the common 80% cleanly.
    ///
    /// Exits on /quit, /exit, :q, EOF (Ctrl+D / Ctrl+Z+Enter), or stdin close.
    /// </summary>
    static int RunAskTriadRepl()
    {
        using var reader = new WKAppBot.Shared.NonBlockingLineReader();

        int cycles = 0, pending = 0;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            var depthTag = reader.PendingCount > 0 ? $" (buffered={reader.PendingCount})" : "";
            Console.Write($"{_chatFallbackAi}>{depthTag} ");
            Console.ResetColor();

            var line = reader.Take();
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
                Console.WriteLine("  <any question>       -- routed to ask " + _chatFallbackAi);
                Console.WriteLine("  Ctrl+Z then Enter    -- EOF (Windows)");
                Console.WriteLine();
                Console.WriteLine("  Tip: you can keep typing while the AI answers --");
                Console.WriteLine("  lines are queued and the prompt shows (buffered=N).");
                continue;
            }

            try
            {
                pending = reader.PendingCount;
                var rc = AskSingleAiFallback(q, _chatFallbackAi);
                cycles++;
                var picked = reader.PendingCount;
                var lagNote = picked > pending
                    ? $" +{picked - pending} queued during this run"
                    : "";
                Console.Error.WriteLine($"[CHAT] cycle #{cycles} done (rc={rc}){lagNote}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CHAT] {_chatFallbackAi} error: {ex.Message} -- continuing");
            }
        }
    }
}
