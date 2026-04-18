using System.Diagnostics;
using System.Text;
using WKAppBot.Shared;

namespace WKAppBot.CLI;

/// <summary>
/// Core-side interactive chat loop with minimal router. Copied from the
/// RunAskTriadRepl pattern and split into its own file so it can evolve
/// independently of the one-shot chat flow.
///
/// MVP scope (kept small on purpose; "안전하게"):
///   /quit, /exit, :q       -> leave loop
///   /help, ?               -> show this help
///   /use &lt;shell&gt;           -> switch current shell (gpt | gemini | claude | triad)
///   /status                -> show current shell + pending interrupt count
///   (anything else)        -> dispatched to the current AI shell one-shot
///
/// OS shells (cmd / powershell / bash) are intentionally NOT wired here:
/// line-by-line dispatch to a long-running interactive shell requires
/// bidirectional stdio plumbing that belongs in a future expansion, not in
/// the MVP. For those shells the user should run `wkappbot chat cmd`
/// directly which hands stdio straight to the shell via RunCoreInheritedStdio.
///
/// InterruptChannel integration: the Launcher's forwarder pushes lines the
/// user types mid-response into %TEMP%\wkappbot-interrupt-&lt;corePid&gt;.txt.
/// Each loop iteration drains that channel BEFORE blocking on the local
/// reader so interrupts are never lost even under heavy AI output load.
/// </summary>
internal partial class Program
{
    // Recent shell executions accumulated during the loop. Prepended to the
    // next AI dispatch as MCP-style tool_use/tool_result blocks so the fallback
    // model sees the user's shell interactions in the same shape its own
    // tool-use protocol would deliver. Cleared after each AI dispatch.
    static readonly List<ShellOutputRecord> _chatToolUseBuffer = new();

    static int ChatLoopCommand(string[] args)
    {
        var currentShell = _chatFallbackAi;
        int cycles = 0;

        using var reader = new NonBlockingLineReader();
        Console.Error.WriteLine($"[CHAT:LOOP] shell={currentShell}  /help for commands, /quit to exit");

        while (true)
        {
            // Drain the interrupt channel before asking for local input -- lines the
            // user typed while the previous AI call was in flight land here as
            // timestamped entries and execute next.
            foreach (var pending in InterruptChannel.Drain())
            {
                // Strip "[yyyy-MM-ddTHH:mm:ss.fff] " prefix so the loop sees the raw user text.
                var raw = StripInterruptTimestamp(pending);
                if (string.IsNullOrWhiteSpace(raw)) continue;
                Console.Error.WriteLine($"[CHAT:LOOP] interrupt in: {raw}");
                HandleLine(raw, ref currentShell, ref cycles, out var interruptExit);
                if (interruptExit.HasValue) return interruptExit.Value;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            var depthTag = reader.PendingCount > 0 ? $" (buffered={reader.PendingCount})" : "";
            Console.Write($"{currentShell}>{depthTag} ");
            Console.ResetColor();

            var line = reader.Take();
            if (line == null)
            {
                Console.Error.WriteLine($"[CHAT:LOOP] EOF -- leaving loop after {cycles} cycle(s)");
                return 0;
            }

            HandleLine(line, ref currentShell, ref cycles, out var exitCode);
            if (exitCode.HasValue) return exitCode.Value;
        }
    }

    /// <summary>
    /// Auto-detect whether a line is a shell command or an AI chat message.
    /// - Any non-ASCII character (Korean, Japanese, ...) -> chat. Users typing
    ///   in their native language are definitely not invoking a binary.
    /// - Starts with "/" or "@" -> router command / future tool dispatcher.
    /// - First token is a path that exists, or an executable resolvable on
    ///   PATH -> shell. Catches `npm build`, `D:/scripts/run.ps1`, `git status`.
    /// - Otherwise -> chat. Users asking "how do I fix X" don't match a
    ///   binary name, so they fall through to the fallback AI.
    /// </summary>
    static bool LooksLikeShellCommand(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return false;

        // Non-ASCII (e.g. Korean hangul/jamo) -> chat, never a shell command.
        foreach (var ch in line)
            if (ch > 0x7F) return false;

        var firstToken = line.Split(' ', 2)[0];
        if (firstToken.Length == 0) return false;
        if (firstToken.StartsWith("/")) return false;  // slash router commands
        if (firstToken.StartsWith("@")) return false;  // reserved tool dispatcher

        // Path-form tokens: contains slash/backslash, or ends with an executable extension.
        if (firstToken.Contains('\\') || firstToken.Contains('/'))
        {
            try { return File.Exists(firstToken) || Directory.Exists(firstToken); }
            catch { return false; }
        }

        // Walk PATH for a matching executable.
        try
        {
            var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
            var exts = new[] { ".exe", ".cmd", ".bat", ".ps1", "" };
            foreach (var dir in pathEnv.Split(Path.PathSeparator))
            {
                if (string.IsNullOrWhiteSpace(dir)) continue;
                foreach (var ext in exts)
                    if (File.Exists(Path.Combine(dir, firstToken + ext))) return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Run <paramref name="command"/> through cmd.exe /c, mirror output to the
    /// terminal verbatim AND capture it for later tool_use encoding. Stores
    /// the record in ShellOutputStore (numeric id) and pushes it onto the
    /// chat-loop buffer so the next AI dispatch sees it as MCP tool-use
    /// context. Safe to call with empty command (no-op).
    /// </summary>
    static void RunShellCommandWithCapture(string command)
    {
        if (string.IsNullOrWhiteSpace(command)) return;
        var cwd = Environment.CurrentDirectory;
        var sb = new StringBuilder();
        int exit = -1;

        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c " + command,
                WorkingDirectory = cwd,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };

            using var proc = Process.Start(psi);
            if (proc == null)
            {
                Console.Error.WriteLine($"[CHAT:LOOP] shell: failed to start -- {command}");
                return;
            }

            // Mirror each line to the user's terminal while accumulating for capture.
            proc.OutputDataReceived += (_, e) =>
            {
                if (e.Data == null) return;
                Console.WriteLine(e.Data);
                sb.AppendLine(e.Data);
            };
            proc.ErrorDataReceived += (_, e) =>
            {
                if (e.Data == null) return;
                Console.Error.WriteLine(e.Data);
                sb.AppendLine(e.Data);
            };
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            if (!proc.WaitForExit(60_000))
            {
                try { proc.Kill(entireProcessTree: true); } catch { }
                Console.Error.WriteLine("[CHAT:LOOP] shell: 60s timeout -- killed");
                exit = 124;
            }
            else exit = proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT:LOOP] shell error: {ex.Message}");
            sb.AppendLine($"(shell error: {ex.Message})");
        }

        var rec = ShellOutputStore.Store(command, cwd, exit, sb.ToString());
        _chatToolUseBuffer.Add(rec);
        Console.Error.WriteLine(
            $"[CHAT:LOOP] stored as output={rec.Id} exit={rec.ExitCode} lines={rec.LineCount} -- will include in next AI prompt");
    }

    /// <summary>
    /// Parse and act on a single line. Returns via out parameter when the line
    /// requested loop exit (/quit etc); otherwise null and the caller continues.
    /// Kept as a separate method so the main loop and the interrupt drain share
    /// the exact same dispatch semantics.
    /// </summary>
    static void HandleLine(string rawLine, ref string currentShell, ref int cycles, out int? exitCode)
    {
        exitCode = null;
        // A single leading space is an explicit "force chat" hint from the user --
        // useful when they want to ask the AI about a command name that also
        // happens to resolve on PATH (e.g. " git 뭐가 좋아?"). Treat the rest
        // verbatim and skip the shell-detection heuristic entirely.
        bool forceChat = rawLine.Length > 0 && rawLine[0] == ' ';
        var line = rawLine.Trim();
        if (line.Length == 0) return;

        // Auto-detect: shell command (first token is path/executable on PATH, ASCII-only)
        // vs AI chat. Users type naturally; we don't require a prefix. The shell run is
        // captured + attached to the next AI dispatch as MCP tool_use/tool_result.
        if (!forceChat && LooksLikeShellCommand(line))
        {
            RunShellCommandWithCapture(line);
            return;
        }

        // -- router ----------------------------------------------------------
        if (line is "/quit" or "/exit" or ":q")
        {
            Console.Error.WriteLine($"[CHAT:LOOP] {line} -- leaving after {cycles} cycle(s)");
            exitCode = 0;
            return;
        }
        if (line is "/help" or "?")
        {
            PrintChatLoopHelp(currentShell);
            return;
        }
        if (line == "/status")
        {
            Console.WriteLine($"shell={currentShell}  cycles={cycles}  interrupts={InterruptChannel.HasPending()}");
            return;
        }
        // /out <id> -- show a captured shell output by id. Useful when AI mentions
        // "see output 2501" -- operator can bring up the full body without leaving
        // the loop. Successful commands only ship preview + id in the tool_use
        // broadcast, so this is how you inspect the actual content.
        if (line.StartsWith("/out ", StringComparison.OrdinalIgnoreCase))
        {
            var idStr = line[5..].Trim();
            if (!int.TryParse(idStr, out var id))
            {
                Console.Error.WriteLine($"[CHAT:LOOP] /out: '{idStr}' is not a valid id");
                return;
            }
            var body = ShellOutputStore.ReadById(id);
            if (body == null)
            {
                Console.Error.WriteLine($"[CHAT:LOOP] /out: id={id} not found");
                return;
            }
            Console.WriteLine($"--- output id={id} ---");
            Console.Write(body);
            if (!body.EndsWith('\n')) Console.WriteLine();
            Console.WriteLine($"--- end id={id} ---");
            return;
        }

        if (line.StartsWith("/use ", StringComparison.OrdinalIgnoreCase))
        {
            var name = line[5..].Trim().ToLowerInvariant();
            if (name is "gpt" or "gemini" or "claude" or "triad")
            {
                currentShell = name;
                Console.WriteLine($"[CHAT:LOOP] shell -> {name}");
            }
            else
            {
                Console.Error.WriteLine($"[CHAT:LOOP] /use: unknown shell '{name}'. Valid: gpt | gemini | claude | triad");
            }
            return;
        }
        if (line.StartsWith("/", StringComparison.Ordinal))
        {
            Console.Error.WriteLine($"[CHAT:LOOP] unknown command: {line.Split(' ')[0]}  (/help for list)");
            return;
        }

        // -- plain dispatch to current AI shell ------------------------------
        // Prepend accumulated shell tool_use/tool_result blocks so the AI sees
        // the user's recent shell activity as conversation context (MCP style).
        try
        {
            var toolUseCtx = ShellToolUseEncoder.Encode(_chatToolUseBuffer);
            var question = string.IsNullOrEmpty(toolUseCtx)
                ? line
                : toolUseCtx + "\n\nUser: " + line;
            var rc = AskSingleAiFallback(question, currentShell);
            cycles++;
            var attachedTag = _chatToolUseBuffer.Count > 0
                ? $" (attached {_chatToolUseBuffer.Count} shell tool_use block(s))"
                : "";
            Console.Error.WriteLine($"[CHAT:LOOP] cycle #{cycles} done (rc={rc}){attachedTag}");
            // Clear buffer AFTER successful dispatch -- if the call threw, keep the
            // records so the next attempt still sees them.
            _chatToolUseBuffer.Clear();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT:LOOP] {currentShell} error: {ex.Message} -- continuing");
        }
    }

    static void PrintChatLoopHelp(string currentShell)
    {
        Console.WriteLine($"  Current shell: {currentShell}");
        Console.WriteLine();
        Console.WriteLine("  /quit | /exit | :q     leave the loop");
        Console.WriteLine("  /help | ?              show this help");
        Console.WriteLine("  /use gpt|gemini|claude|triad   switch current AI shell");
        Console.WriteLine("  /status                show shell + interrupt state");
        Console.WriteLine("  /out <id>              display captured shell output by id");
        Console.WriteLine("  <shell command>        auto-detected: first token = path/exe -> run");
        Console.WriteLine("  \" <text>\" (leading space)   force chat mode (skip shell detection)");
        Console.WriteLine("  <non-ASCII text>       always chat (Korean / CJK never run as shell)");
        Console.WriteLine("  <any other text>       dispatch to the current AI shell");
        Console.WriteLine();
        Console.WriteLine("  Tip: you can keep typing while the shell answers -- lines are");
        Console.WriteLine("  queued locally AND via the Launcher interrupt channel, so nothing");
        Console.WriteLine("  is lost even when the AI is mid-response.");
    }

    static string StripInterruptTimestamp(string line)
    {
        // Expected format: "[yyyy-MM-ddTHH:mm:ss.fff] <body>"
        if (line.Length > 26 && line[0] == '[' && line[24] == ']')
            return line[25..].TrimStart();
        return line;
    }
}
