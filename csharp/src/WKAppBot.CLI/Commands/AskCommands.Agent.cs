namespace WKAppBot.CLI;

// wkappbot agent <ai> "task" — autonomous sub-agent execution
// Wraps ask --loop with agent-optimized defaults and a separate tab namespace.
//
// Tab namespace: {ai}-agent-{sessionHash}  (separate from ask: {ai}-{sessionHash})
// Defaults (triad-agreed): timeout=90s, max-steps=20, retry=2, parallel=4, loop=on
// Extra options: --agent-id <name>  --fresh  --triad  --slack  --verify-delay <ms>
//
// ⚠ Do NOT call 'wkappbot agent' from inside an agent loop — infinite recursion!

internal partial class Program
{
    static int AgentCommand(string[] args)
    {
        // wkappbot agent checkpoint [--label "text"]
        if (args.Length > 0 && args[0] == "checkpoint")
        {
            var label = args.Length > 2 && args[1] == "--label" ? args[2]
                      : args.Length > 1 && !args[1].StartsWith("--") ? args[1]
                      : "";
            var id = AgentFileTracker.Checkpoint(label);
            if (id == 0) Console.WriteLine("[AGENT] Checkpoint skipped (no git workspace).");
            else Console.WriteLine($"[AGENT] Checkpoint {id} saved.");
            return 0;
        }

        // wkappbot agent dump-patch [--out file.patch] [--apply]
        if (args.Length > 0 && args[0] == "dump-patch")
        {
            string? outPath = null;
            bool    doApply = false;
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--out" && i + 1 < args.Length) outPath = args[++i];
                else if (args[i] == "--apply") doApply = true;
            }

            var patch = AgentFileTracker.DumpPatch(outPath: outPath);
            if (patch == null) return 0;

            if (doApply)
            {
                Console.WriteLine($"[AGENT] Applying patch: {patch}");
                var gitRoot = Path.GetDirectoryName(patch) ?? Directory.GetCurrentDirectory();
                var psi2 = new System.Diagnostics.ProcessStartInfo("git", $"apply \"{patch}\"")
                    { UseShellExecute = false, WorkingDirectory = gitRoot };
                using var p2 = System.Diagnostics.Process.Start(psi2)!;
                p2.WaitForExit();
                Console.WriteLine(p2.ExitCode == 0 ? "[AGENT] Patch applied." : $"[AGENT] git apply failed (exit {p2.ExitCode})");
            }
            return 0;
        }

        // wkappbot agent session-status
        if (args.Length > 0 && args[0] == "session-status")
        {
            AgentFileTracker.SessionStatus();
            return 0;
        }

        // wkappbot agent session-clear
        if (args.Length > 0 && args[0] == "session-clear")
        {
            AgentFileTracker.SessionClear();
            return 0;
        }

        if (args.Length < 2)
            return AgentUsage();

        var ai = args[0].ToLowerInvariant();
        if (ai is not ("gemini" or "gpt" or "chatgpt" or "claude" or "triad" or "all"))
            return Error($"Unknown AI: {ai} (use gemini, gpt, claude, or triad)");

        // Agent defaults (triad-agreed)
        int timeoutSec    = 90;
        int loopMaxSteps  = 20;
        int loopRetry     = 2;
        int loopMaxParallel = 4;
        int verifyDelayMs = 400;
        bool newSession   = false;    // --fresh
        bool triadMode    = false;
        string? modelHint = null;
        string? agentId   = null;     // --agent-id <name>

        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--fresh" || args[i] == "--new-session")
                newSession = true;
            else if (args[i] == "--triad")
                triadMode = true;
            else if (args[i] == "--agent-id" && i + 1 < args.Length)
                agentId = args[++i];
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else if (args[i] == "--max-steps" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var n)) loopMaxSteps = Math.Max(1, n);
            }
            else if (args[i] == "--retry" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var n)) loopRetry = Math.Max(0, n);
            }
            else if (args[i] == "--parallel" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var n)) loopMaxParallel = Math.Max(1, n);
            }
            else if (args[i] == "--verify-delay" && i + 1 < args.Length)
                int.TryParse(args[++i], out verifyDelayMs);
            else if (args[i] == "--model" && i + 1 < args.Length)
                modelHint = args[++i];
            else
                remaining.Add(args[i]);
        }

        var (questionParts, attachFiles) = ParseTextAndFilesWithMarkers(remaining.ToArray());
        var question = InlineTextFiles(questionParts, attachFiles);
        if (string.IsNullOrWhiteSpace(question))
            return AgentUsage();

        foreach (var f in attachFiles)
        {
            if (!File.Exists(f))
                return Error($"File not found: {f}");
        }

        // Build dedicated tab tag: {ai}-agent-{id|hash}
        var tabSuffix = !string.IsNullOrWhiteSpace(agentId)
            ? agentId.ToLowerInvariant().Replace(' ', '-')
            : (GetSessionTag() ?? "default");
        var targetTag = $"{(ai == "chatgpt" ? "gpt" : ai)}-agent-{tabSuffix}";

        // --fresh: clear file tracker session too
        if (newSession) AgentFileTracker.SessionClear();

        Console.WriteLine($"[AGENT] {ai.ToUpperInvariant()} | tag={targetTag} | steps={loopMaxSteps} | timeout={timeoutSec}s | retry={loopRetry} | parallel={loopMaxParallel}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[AGENT] Model hint: {modelHint}");
        if (verifyDelayMs > 0)
            Console.WriteLine($"[AGENT] Verify delay: {verifyDelayMs}ms");
        if (attachFiles.Count > 0)
            Console.WriteLine($"[AGENT] Attaching {attachFiles.Count} file(s): {string.Join(", ", attachFiles.Select(Path.GetFileName))}");

        // triad agent: run all three in parallel with prefixes
        if (ai is "triad" or "all")
        {
            Console.WriteLine("[AGENT] Triad mode — launching Gemini + GPT + Claude agents in parallel...");

            // Pre-open a shared Slack thread so all 3 AI answers land in one thread
            EnsureSlackThread("Triad", question);

            var tasks = new[]
            {
                Task.Run(() => AskGemini(question, true, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"gemini-agent-{tabSuffix}", linePrefix: "[gemini] ")),
                Task.Run(() => AskChatGpt(question, true, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"gpt-agent-{tabSuffix}",    linePrefix: "[gpt] ")),
                Task.Run(() => AskClaude(question, true, timeoutSec, newTab: false, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"claude-agent-{tabSuffix}", linePrefix: "[claude] ")),
            };
            Task.WaitAll(tasks);
            Console.WriteLine($"[AGENT] Triad done — gemini={tasks[0].Result} gpt={tasks[1].Result} claude={tasks[2].Result}");
            return tasks.Select(t => t.Result).Any(r => r == 0) ? 0 : 1;
        }

        return ai switch
        {
            "gemini"            => AskGemini(question, true, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[gemini] "),
            "gpt" or "chatgpt"  => AskChatGpt(question, true, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[gpt] "),
            "claude"            => AskClaude(question, true, timeoutSec, newTab: false, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[claude] "),
            _                   => Error($"Unknown AI: {ai}")
        };
    }

    static int AgentUsage()
    {
        Console.WriteLine(@"
wkappbot agent — autonomous sub-agent (multi-step reasoning loop with tools)
  vs ask:  ask = one-shot Q&A    agent = autonomous loop: plans → uses tools → verifies → repeats

Usage:
  wkappbot agent gemini|gpt|claude|triad ""task"" [files...] [options]

  triad / all  — run Gemini + GPT + Claude in parallel, each prefixed [gemini]/[gpt]/[claude]

Defaults (triad-agreed):
  --timeout      90     seconds total hard kill
  --max-steps    20     max reasoning loop iterations
  --retry         2     retries per tool call on failure
  --parallel      4     max concurrent tool calls per step
  --verify-delay 400    ms to wait after UI actions before verifying

Options:
  --agent-id <name>   Named persistent tab — reuses context across calls, survives restarts
                      Tab: '{ai}-agent-{name}'  (ask uses '{ai}-{session}')
  --fresh             Force new conversation + clear AgentFileTracker session
  --triad             Inject triad-planning hints into loop persona (multi-AI planning)
  --slack             Report final result to Slack channel after completion
  --model <hint>      Model/version hint (e.g. 2.0-flash, opus, 4.5)
  --timeout N         Override total timeout (seconds)
  --max-steps N       Override max loop iterations
  --retry N           Override per-tool retry count
  --parallel N        Override parallel tool call limit

Sub-commands (session/checkpoint management):
  agent checkpoint [--label ""text""]   Save git snapshot (restore point on crash/undo)
  agent dump-patch [--out f.patch]     Export uncommitted changes as patch file
              [--apply]                Apply a previously dumped patch via git apply
  agent session-status                 Show current AgentFileTracker session state
  agent session-clear                  Reset session (same as --fresh without re-running)

WARNING: Do NOT call 'wkappbot agent' from inside an agent loop!
  Agents calling agents = infinite recursion. For nested tasks use 'wkappbot ask --loop'.

APSP v1 (WKAppBot MCP extension — beyond standard MCP spec):
  MCP progress notifications carry extra fields: mem_mb, cpu_pct, threads, handles
  stdin/IO wait auto-detected by watchdog: status=wait_input|wait_lock|wait_ipc|wait_io|sleeping
  data field format: ""N> output line""  (N=progress counter, correlates console with MCP stream)

Examples:
  wkappbot agent gemini ""analyze all *.cs files and find N+1 query patterns""
  wkappbot agent gpt ""fix UI bug shown in screenshot"" screen.png --slack
  wkappbot agent claude ""investigate why build fails"" --max-steps 30
  wkappbot agent triad ""test MCP APSP v1 profiling + parallel tool calls""
  wkappbot agent gemini ""task"" --agent-id myjob           # persistent named session
  wkappbot agent gemini ""continue"" --agent-id myjob       # resume same named session
  wkappbot agent claude ""plan"" --triad --fresh            # new session + triad hints
");
        return 1;
    }
}
