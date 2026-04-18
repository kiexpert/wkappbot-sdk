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
    /// Parse and act on a single line. Returns via out parameter when the line
    /// requested loop exit (/quit etc); otherwise null and the caller continues.
    /// Kept as a separate method so the main loop and the interrupt drain share
    /// the exact same dispatch semantics.
    /// </summary>
    static void HandleLine(string rawLine, ref string currentShell, ref int cycles, out int? exitCode)
    {
        exitCode = null;
        var line = rawLine.Trim();
        if (line.Length == 0) return;

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

        // -- plain dispatch to current shell ---------------------------------
        try
        {
            var rc = AskSingleAiFallback(line, currentShell);
            cycles++;
            Console.Error.WriteLine($"[CHAT:LOOP] cycle #{cycles} done (rc={rc})");
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
        Console.WriteLine("  <any text>             dispatch to the current shell");
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
