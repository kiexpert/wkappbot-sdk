namespace WKAppBot.CLI;

// wkappbot agent <ai> "task" — autonomous sub-agent execution
// Wraps ask --loop with agent-optimized defaults and a separate tab namespace.
//
// Tab namespace: {ai}-agent-{sessionHash}  (separate from ask: {ai}-{sessionHash})
// Defaults (triad-agreed): timeout=90s, max-steps=20, retry=2, parallel=3, loop=on
// Extra options: --agent-id <name>  --fresh  --verify-delay <ms>

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
            Console.WriteLine($"[AGENT] Checkpoint {id} saved ({AgentFileTracker.TrackedCount} files tracked)");
            return 0;
        }

        // wkappbot agent dump-patch [--out file.patch] [--apply]
        if (args.Length > 0 && args[0] == "dump-patch")
        {
            string? outPath  = null;
            bool    doApply  = false;
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--out"   && i + 1 < args.Length) outPath = args[++i];
                else if (args[i] == "--apply") doApply = true;
            }
            var workspace = DataDir; // use HQ dir as fallback; ideally workspace root
            // Try to find git root
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo("git", "rev-parse --show-toplevel")
                    { RedirectStandardOutput = true, UseShellExecute = false };
                using var p = System.Diagnostics.Process.Start(psi)!;
                var root = p.StandardOutput.ReadToEnd().Trim();
                p.WaitForExit();
                if (p.ExitCode == 0 && Directory.Exists(root)) workspace = root;
            }
            catch { }

            var patch = AgentFileTracker.DumpPatch(workspace, outPath);
            if (patch == null)
            {
                Console.WriteLine("[AGENT] No files tracked — nothing to dump.");
                return 0;
            }
            if (doApply)
            {
                Console.WriteLine($"[AGENT] Applying patch: {patch}");
                var psi2 = new System.Diagnostics.ProcessStartInfo("git", $"apply \"{patch}\"")
                    { UseShellExecute = false, WorkingDirectory = workspace };
                using var p2 = System.Diagnostics.Process.Start(psi2)!;
                p2.WaitForExit();
                Console.WriteLine(p2.ExitCode == 0 ? "[AGENT] Patch applied." : $"[AGENT] git apply failed (exit {p2.ExitCode})");
            }
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
        bool slackReport  = false;
        bool newSession   = false;    // --fresh
        bool triadMode    = false;
        string? modelHint = null;
        string? agentId   = null;     // --agent-id <name>

        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--slack")
                slackReport = true;
            else if (args[i] == "--fresh" || args[i] == "--new-session")
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
            var tasks = new[]
            {
                Task.Run(() => AskGemini(question, slackReport, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"gemini-agent-{tabSuffix}", linePrefix: "[gemini] ")),
                Task.Run(() => AskChatGpt(question, slackReport, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"gpt-agent-{tabSuffix}",    linePrefix: "[gpt] ")),
                Task.Run(() => AskClaude(question, slackReport, timeoutSec, newTab: false, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode: true, modelHint, noWait: false, $"claude-agent-{tabSuffix}", linePrefix: "[claude] ")),
            };
            Task.WaitAll(tasks);
            Console.WriteLine($"[AGENT] Triad done — gemini={tasks[0].Result} gpt={tasks[1].Result} claude={tasks[2].Result}");
            return tasks.Select(t => t.Result).Any(r => r == 0) ? 0 : 1;
        }

        return ai switch
        {
            "gemini"            => AskGemini(question, slackReport, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[gemini] "),
            "gpt" or "chatgpt"  => AskChatGpt(question, slackReport, timeoutSec, newTab: false, attachFiles, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[gpt] "),
            "claude"            => AskClaude(question, slackReport, timeoutSec, newTab: false, newSession, loopMode: true, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait: false, targetTag, linePrefix: $"[claude] "),
            _                   => Error($"Unknown AI: {ai}")
        };
    }

    static int AgentUsage()
    {
        Console.WriteLine(@"
wkappbot agent — autonomous sub-agent execution (ask --loop with agent-optimized defaults)

Usage:
  wkappbot agent gemini|gpt|claude ""task"" [files...] [options]

Defaults (triad-agreed):
  --timeout     90      seconds per step
  --max-steps   20      max loop iterations
  --retry       2       retries per tool call
  --parallel    3       max concurrent tool calls
  --verify-delay 400    ms to wait after UI actions

Options:
  --agent-id <name>   Named persistent tab (e.g. gemini-agent-myjob) — survives session restarts
  --fresh             Force new conversation (clear previous context)
  --triad             Add triad-planning hints to loop persona
  --slack             Report result to Slack channel
  --model <hint>      Model/version hint (e.g. 4.1, opus)
  --timeout N         Override step timeout
  --max-steps N       Override max steps
  --retry N           Override retry count
  --parallel N        Override parallel tool calls

Tab namespace:
  agent uses '{ai}-agent-{session}' tabs — separate from 'ask' tabs ('{ai}-{session}')
  --agent-id pins to '{ai}-agent-{name}' for persistent named sessions

Examples:
  wkappbot agent gemini ""refactor all test files to use xUnit""
  wkappbot agent gpt ""analyze this screenshot and fix the UI bug"" screen.png
  wkappbot agent claude ""investigate why build fails"" --slack
  wkappbot agent gemini ""myjob task"" --agent-id myjob   # persistent named session
  wkappbot agent claude ""task"" --triad --max-steps 30   # triad hints, more steps
");
        return 1;
    }
}
