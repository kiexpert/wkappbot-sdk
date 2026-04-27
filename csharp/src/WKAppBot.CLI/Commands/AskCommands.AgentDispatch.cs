namespace WKAppBot.CLI;

// Canonical agent options + per-tier dispatch.
//
// Design:
//   `wkappbot agent "task" - <tier> [canonical-opts]` must work for ALL AI tiers.
//   Claude CLI's argument format is the canonical standard; other AIs translate
//   from it (their native signatures stay, we just remap the fields).
//
// Canonical options (Claude-standard):
//   --model <id>        Override model within the tier
//   --max-turns N       Max agentic turns (default 20) -> loopMaxSteps for browser AIs
//   --system "..."      System prompt override (carried for future use)
//   --no-agent          One-shot (no loop)
//   --fresh             New session
//   --attach <file>     Attach file (repeatable)
//   --timeout N         Timeout seconds (default 90)
//   --no-wait           Fire and don't wait for response
//
// Supported tiers:
//   codex-mini | read-only | haiku | sonnet | opus | gpt | gemini | triad

internal partial class Program
{
    // --------------------------------------------------------------------
    // Canonical options record
    // --------------------------------------------------------------------
    sealed record CanonicalAgentOpts(
        string Task,
        string Tier,
        string? ModelOverride  = null,
        int    MaxTurns        = 20,
        string? SystemPrompt   = null,
        bool   AgentMode       = true,
        bool   NewSession      = false,
        List<string>? Attach   = null,
        int    TimeoutSec      = 90,
        bool   NoWait          = false
    );

    // --------------------------------------------------------------------
    // Parser: tierHint = "<tier> [--flag ...]"
    // --------------------------------------------------------------------
    static (string tier, CanonicalAgentOpts opts) ParseTierAndCanonicalOpts(string task, string tierHint)
    {
        var tokens = (tierHint ?? "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var tier = tokens.Length > 0 ? tokens[0].ToLowerInvariant() : "codex-mini";

        string? model = null;
        int     maxTurns   = 20;
        string? systemPrompt = null;
        bool    agentMode  = true;
        bool    newSession = false;
        int     timeoutSec = 90;
        bool    noWait     = false;
        var     attach     = new List<string>();

        for (int i = 1; i < tokens.Length; i++)
        {
            var t = tokens[i];
            switch (t)
            {
                case "--model" when i + 1 < tokens.Length:
                    model = tokens[++i];
                    break;
                case "--max-turns" when i + 1 < tokens.Length:
                    if (int.TryParse(tokens[++i], out var mt)) maxTurns = Math.Max(1, mt);
                    break;
                case "--system" when i + 1 < tokens.Length:
                    systemPrompt = tokens[++i];
                    break;
                case "--no-agent":
                    agentMode = false;
                    break;
                case "--fresh":
                case "--new-session":
                    newSession = true;
                    break;
                case "--attach" when i + 1 < tokens.Length:
                    attach.Add(tokens[++i]);
                    break;
                case "--timeout" when i + 1 < tokens.Length:
                    if (int.TryParse(tokens[++i], out var ts)) timeoutSec = Math.Max(1, ts);
                    break;
                case "--no-wait":
                    noWait = true;
                    break;
                default:
                    // Unknown tokens are ignored; parser is forgiving so user
                    // typos don't stop the dispatch.
                    break;
            }
        }

        var opts = new CanonicalAgentOpts(
            Task: task,
            Tier: tier,
            ModelOverride: model,
            MaxTurns: maxTurns,
            SystemPrompt: systemPrompt,
            AgentMode: agentMode,
            NewSession: newSession,
            Attach: attach.Count > 0 ? attach : null,
            TimeoutSec: timeoutSec,
            NoWait: noWait
        );
        return (tier, opts);
    }

    // --------------------------------------------------------------------
    // Top-level dispatch
    // --------------------------------------------------------------------
    static int DispatchAgent(CanonicalAgentOpts opts)
    {
        return opts.Tier switch
        {
            "codex-mini" => DispatchCodex(opts, readOnly: false),
            "read-only"  => DispatchCodex(opts, readOnly: true),
            "haiku"      => DispatchClaude(opts, "claude-haiku-4-5-20251001"),
            "sonnet"     => DispatchClaude(opts, "claude-sonnet-4-6"),
            "opus"       => DispatchClaude(opts, "claude-opus-4-7"),
            "gpt"        => DispatchGpt(opts),
            "chatgpt"    => DispatchGpt(opts),
            "gemini"     => DispatchGemini(opts),
            "triad"      => DispatchTriad(opts),
            _            => Error($"Unknown agent tier: {opts.Tier} (use codex-mini, read-only, haiku, sonnet, opus, gpt, gemini, or triad)")
        };
    }

    // --------------------------------------------------------------------
    // Per-tier translators (canonical -> native signature)
    // --------------------------------------------------------------------

    // Claude CLI: the canonical format. Thin passthrough.
    static int DispatchClaude(CanonicalAgentOpts opts, string defaultModel)
    {
        var model = opts.ModelOverride ?? defaultModel;
        // AskClaudeCli doesn't expose max-turns/timeout/attach/system-prompt;
        // those are handled by claude.exe internally. We only forward what the
        // existing signature accepts.
        return AskClaudeCli(opts.Task, opts.NewSession, model, opts.AgentMode, out _);
    }

    // Codex CLI: re-encode canonical opts into the flag-prefix string that
    // AskCodexCli already knows how to peel.
    static int DispatchCodex(CanonicalAgentOpts opts, bool readOnly)
    {
        var flags = new List<string>();
        if (opts.AgentMode) flags.Add("--agent");
        if (readOnly)
        {
            flags.Add("-s");
            flags.Add("read-only");
        }
        if (!string.IsNullOrWhiteSpace(opts.ModelOverride))
        {
            flags.Add("-m");
            flags.Add(opts.ModelOverride);
        }
        if (opts.NewSession) flags.Add("--fresh");
        // Note: --max-turns / --timeout / --system / --attach are not native
        // to codex exec flag peel. They are honored at the harness layer when
        // codex supports them in the future; for now we pass them inline so a
        // smart agent can read them from the task text.
        var prefix = string.Join(" ", flags);
        var q = prefix.Length > 0 ? $"{prefix} {opts.Task}" : opts.Task;
        return AskCodexCli(q, opts.NewSession);
    }

    // GPT (ChatGPT browser bot)
    static int DispatchGpt(CanonicalAgentOpts opts)
        => AskChatGpt(
            question:        opts.Task,
            slackReport:     true,
            timeoutSec:      opts.TimeoutSec,
            newTab:          false,
            attachFiles:     opts.Attach,
            newSession:      opts.NewSession,
            loopMode:        opts.AgentMode,
            loopMaxSteps:    opts.MaxTurns,
            loopRetry:       2,
            loopMaxParallel: 4,
            triadMode:       false,
            modelHint:       opts.ModelOverride,
            noWait:          opts.NoWait);

    // Gemini (browser bot)
    static int DispatchGemini(CanonicalAgentOpts opts)
        => AskGemini(
            question:        opts.Task,
            slackReport:     true,
            timeoutSec:      opts.TimeoutSec,
            newTab:          false,
            attachFiles:     opts.Attach,
            newSession:      opts.NewSession,
            loopMode:        opts.AgentMode,
            loopMaxSteps:    opts.MaxTurns,
            loopRetry:       2,
            loopMaxParallel: 4,
            triadMode:       false,
            modelHint:       opts.ModelOverride,
            noWait:          opts.NoWait);

    // Triad: GPT + Gemini + Claude in parallel
    static int DispatchTriad(CanonicalAgentOpts opts)
        => AskTriadParallel(
            question:        opts.Task,
            timeoutSec:      opts.TimeoutSec,
            attachFiles:     opts.Attach,
            newSession:      opts.NewSession,
            loopMode:        opts.AgentMode,
            loopMaxSteps:    opts.MaxTurns,
            loopRetry:       2,
            loopMaxParallel: 4,
            modelHint:       opts.ModelOverride,
            noWait:          opts.NoWait);
}
