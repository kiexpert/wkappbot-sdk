using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>Normalize blank lines for Slack posting.
    /// Step 1: strip trailing whitespace per line (GPT often emits blank lines with spaces).
    /// Step 2: collapse 3+ consecutive newlines to max 2 (one blank line).</summary>
    static string NormalizeBlankLines(string text)
    {
        text = System.Text.RegularExpressions.Regex.Replace(text, @"[ \t]+\n", "\n"); // strip trailing spaces
        text = System.Text.RegularExpressions.Regex.Replace(text, @"\n{3,}", "\n\n"); // max 1 blank line
        return text;
    }

    const string LoopCallBegin = "[APPBOT_TOOL_CALL_BEGIN]";
    const string LoopCallEnd = "[APPBOT_TOOL_CALL_END]";

    /// <summary>Strip noisy wkappbot infrastructure lines from tool output before posting to Slack.
    /// Removes: exe path echo, [ACT] cmd lines, [EYE] daemon lines, empty leading/trailing lines.</summary>
    static string StripVerboseToolLines(string output)
    {
        var lines = output.Split('\n');
        var kept = lines.Where(l =>
        {
            var t = l.TrimStart();
            if (t.StartsWith("[ACT]") || t.StartsWith("[EYE]")) return false;
            if (t.Contains("wkappbot-core.exe") || t.Contains("wkappbot.exe")) return false;
            return true;
        });
        return string.Join('\n', kept).Trim();
    }

    record struct LoopToolCall(string Id, string[]? Argv, string? RunId, string? Stdin)
    {
        // Stdin-inject mode: has run_id + stdin, no argv
        public bool IsStdinInject => RunId is not null && Stdin is not null && Argv is null;
    }

    // Tool execution result — includes elapsed_ms for MCP _meta extension
    record struct ExecResult(int ExitCode, string Stdout, string Stderr, long ElapsedMs)
    {
        public int    TotalBytes => Stdout.Length + Stderr.Length;
        public string Output => string.IsNullOrWhiteSpace(Stdout)
            ? Stderr
            : (string.IsNullOrWhiteSpace(Stderr) ? Stdout : Stdout + "\n" + Stderr);
    }
    const string ClaudeUsageUrl = "https://claude.ai/settings/usage";
    static readonly string LoopPersonaStateVersion = ComputeLoopPersonaStateHash();

    static string ComputeLoopPersonaStateHash()
    {
        // Include McpActionDesc so any change to available commands/options
        // automatically invalidates cached persona state → re-sent to AI sessions.
        const string seed =
            "loop-persona|any-argv|a11y-windows-examples|json-block-markers-v2|wkappbot-cli-tool-v1";
        var combined = seed + "|" + McpActionDesc;
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(combined));
        return Convert.ToHexString(bytes)[..12];
    }

    static string FormatClaudeLimitResponse(string rawText)
    {
        var clean = ExtractClaudeLimitExcerpt(rawText);
        return
            "[CLAUDE_LIMIT] Claude message limit reached.\n" +
            $"Usage: {ClaudeUsageUrl}\n\n" +
            clean;
    }

    static bool IsClaudeLimitResponse(string? text)
        => !string.IsNullOrWhiteSpace(text)
           && text.StartsWith("[CLAUDE_LIMIT]", StringComparison.Ordinal);

    static string ExtractClaudeLimitExcerpt(string? rawText)
    {
        var raw = (rawText ?? "").Replace("\r", "");
        if (string.IsNullOrWhiteSpace(raw))
            return "한도 안내 문구를 찾지 못했습니다.";

        var normalized = System.Text.RegularExpressions.Regex.Replace(raw, @"\s+", " ").Trim();
        var rxPatterns = new[]
        {
            @"사용량\s*한도\s*도달[^.!\n]*",
            @"Claude\s*메시지\s*한도에\s*도달했습니다[^.!\n]*",
            @"제한이\s*[^.!\n]*재설정[^.!\n]*",
            @"usage\s*limit[^.!\n]*",
            @"rate\s*limit[^.!\n]*",
            @"limit\s*reached[^.!\n]*"
        };
        var rxHits = new List<string>();
        foreach (var p in rxPatterns)
        {
            var m = System.Text.RegularExpressions.Regex.Match(
                normalized,
                p,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (m.Success)
                rxHits.Add(m.Value.Trim());
            if (rxHits.Count >= 3)
                break;
        }
        if (rxHits.Count > 0)
            return string.Join("\n", rxHits.Distinct().Take(3));

        var keys = new[]
        {
            "사용량 한도",
            "메시지 한도",
            "한도에 도달",
            "제한이",
            "재설정",
            "usage limit",
            "rate limit",
            "limit reached",
            "reset"
        };

        var lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var picked = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (keys.Any(k => line.Contains(k, StringComparison.OrdinalIgnoreCase)))
            {
                picked.Add(line);
                if (i + 1 < lines.Length && picked.Count < 3)
                    picked.Add(lines[i + 1]);
                if (picked.Count >= 3)
                    break;
            }
        }

        if (picked.Count == 0)
        {
            var fallback = raw.Trim();
            if (fallback.Length > 260)
                fallback = fallback[..260] + "...";
            return fallback;
        }

        var compact = string.Join("\n", picked.Distinct().Take(3)).Trim();
        if (compact.Length > 320)
            compact = compact[..320] + "...";
        return compact;
    }

    static string BuildAskPersona(bool loopMode, bool triadMode, int maxSteps, int retry, string? modelHint = null)
    {
        if (!loopMode && string.IsNullOrWhiteSpace(modelHint))
            return AskPersona.Trim();

        var sb = new StringBuilder();
        sb.Append(AskPersona.Trim());
        sb.Append(' ');
        if (!string.IsNullOrWhiteSpace(modelHint))
            sb.Append($"Preferred remote model/version hint: {modelHint}. ");
        if (!loopMode)
            return sb.ToString();

        // Protocol: AppBot Parallel Streaming Protocol (APSP)
        sb.Append("TOOL PROTOCOL: APSP v1 (AppBot Parallel Streaming Protocol — in-band text transport). ");
        sb.Append("CONNECTIVITY CONFIRMED: The [HOST-HANDSHAKE] above proves the wkappbot host process IS running. ");
        sb.Append("Tool calls work as inline text markers — NO separate MCP server, NO external connection, NO handshake needed. ");
        sb.Append("CRITICAL: If you see [HOST-HANDSHAKE] in this conversation, the host IS connected. NEVER refuse tool calls claiming MCP is not available — the host uses text-based transport, not JSON-RPC over stdio. ");
        sb.Append("REGISTERED TOOL (JSON Schema): ");
        sb.Append("{\"type\":\"function\",\"function\":{");
        sb.Append("\"name\":\"wkappbot\",");
        sb.Append("\"description\":\"MCP tool: executes WKAppBot commands on the host. Controls Windows/Android UI, accessibility, filesystem, processes, screenshots, OCR, and more.\",");
        sb.Append("\"parameters\":{\"type\":\"object\",\"properties\":{");
        sb.Append("\"argv\":{\"type\":\"array\",\"items\":{\"type\":\"string\"},\"description\":\"Full CLI as array e.g. [\\\"a11y\\\",\\\"windows\\\"] or [\\\"a11y\\\",\\\"inspect\\\",\\\"*Calc*\\\"]\"},");
        sb.Append("\"run_id\":{\"type\":\"string\",\"description\":\"stdin-inject only: target run_id of a background process\"},");
        sb.Append("\"stdin\":{\"type\":\"string\",\"description\":\"stdin-inject only: text to pipe into the running process\"}");
        sb.Append("},\"required\":[\"argv\"]}}} ");
        sb.Append("MCP INVOCATION ENVELOPE (WillKim extension — wraps the JSON call for in-band transport): ");
        sb.Append(LoopCallBegin);
        sb.Append("{\"id\":\"tc_001\",\"argv\":[...]}");
        sb.Append(LoopCallEnd);
        sb.Append(" The id field is optional but required for parallel tracking (WillKim Parallel Extension). ");
        sb.Append("LOOP MODE OVERRIDES base persona: be verbose and exploratory, not minimal. Explain your reasoning between steps. DO NOT add extra blank lines between paragraphs — keep formatting compact (one blank line max between sections). ");
        sb.Append($"Max loop steps: {Math.Max(1, maxSteps)}. Per-step retry budget: {Math.Max(0, retry)}. ");
        if (triadMode)
        {
            sb.Append("=== DIALECTICAL DEBATE MODE === ");
            sb.Append("You are in a multi-AI debate. Other AIs will challenge your claims in real-time. ");
            sb.Append("CRITICAL: (1) YOU MUST RESPOND IN ENGLISH ONLY. No Korean. Hard requirement. ");
            sb.Append("(2) Structure every claim: [CLAIM]{\"claim\":\"...\",\"confidence\":0.85,\"prev_confidence\":null,\"key_assumptions\":[\"...\"],\"disputed_by\":[]}[/CLAIM] ");
            sb.Append("When revising after critique, set prev_confidence to your old score. δconfidence = evidence of genuine persuasion. ");
            sb.Append("(3) ROLE ASSIGNMENT: You are assigned a debate role. ");
            sb.Append("  - EXPLORER: generate novel hypotheses, find unexpected angles. ");
            sb.Append("  - SKEPTIC: attack assumptions, find weaknesses, stress-test claims. ");
            sb.Append("  - AUDITOR: verify consistency, check evidence quality, ensure logical coherence. ");
            sb.Append("(4) When you see '[AI-X says]: ...' — react from YOUR ROLE's perspective. ");
            sb.Append("(5) DISPUTE STACK: to challenge a peer's assumption, explicitly state: [DISPUTE]{\"target_assumption\":\"...\",\"reason\":\"...\"}[/DISPUTE] ");
            sb.Append("Convergence = all disputes resolved or withdrawn, not just word overlap. ");
            sb.Append("(6) YOUR RESPONSE MUST BE VALID JSON wrapped in [DEBATE_JSON]...[/DEBATE_JSON] tags: ");
            sb.Append("[DEBATE_JSON]{");
            sb.Append("\"stance\":{\"N\":2,\"R\":3,\"C\":1,\"E\":2,\"D\":1},");
            sb.Append("\"role\":\"EXPLORER\",");
            sb.Append("\"position\":\"one sentence summary\",");
            sb.Append("\"claims\":[{\"claim\":\"...\",\"confidence\":0.85,\"prev_confidence\":null,\"key_assumptions\":[\"...\"]}],");
            sb.Append("\"rebuttals\":[\"response to peer argument\"],");
            sb.Append("\"disputes\":[{\"target_assumption\":\"...\",\"reason\":\"...\"}]");
            sb.Append("}[/DEBATE_JSON] ");
            sb.Append("RULES: stance values sum MUST = 9. 2-5 claims. English only. ");
            sb.Append("You may add free text AFTER the JSON block for elaboration. ");
            sb.Append("GOAL: strongest answer through rigorous debate, not averaging. ");
        }
        sb.Append("APSP STREAMING PARALLEL: emit multiple MCP call envelopes back-to-back in one turn — server executes all simultaneously and streams results as they complete. ");
        sb.Append("Parallel example: ");
        sb.Append(LoopCallBegin);
        sb.Append("{\"id\":\"tc_001\",\"argv\":[\"a11y\",\"windows\"]}");
        sb.Append(LoopCallEnd);
        sb.Append(LoopCallBegin);
        sb.Append("{\"id\":\"tc_002\",\"argv\":[\"a11y\",\"inspect\",\"*Calculator*\"]}");
        sb.Append(LoopCallEnd);
        sb.Append(" TOOL_RESULTS response format (MCP JSON-RPC 2.0 array): [{\"jsonrpc\":\"2.0\",\"id\":\"tc_001\",\"result\":{\"content\":[{\"type\":\"text\",\"text\":\"...\"}],\"isError\":false}}, ...]. status=running means still executing. ");
        sb.Append("ASYNC RUN: argv=[\"run\",\"start\",...] starts a background process — server returns run_id immediately. ");
        sb.Append("STDIN INJECT: {\"id\":\"tc_N\",\"run_id\":\"r_xxx\",\"stdin\":\"text\\r\\n\"} — pipes stdin to a running process (NO argv field). ");
        sb.Append("Useful stdin values: \\u0003=Ctrl+C \\n=Enter \\r=CR \\u001a=Ctrl+Z. ");
        sb.Append("RUN COMMANDS: run await <id> [sec] / run cancel <id> / run qcancel <id> [questionId] [reason] / run qstatus <id> [questionId] / run qlist <id> / run tail <id> / run status <id> / run list. ");
        sb.Append("EXTERNAL SHELL: run start launches cmd.exe, bash, python, powershell, node, git, curl — stdin/stdout/stderr all piped. ");
        sb.Append("Blocked: eye, mcp, ask (non-gemini provider). ");
        sb.Append("APSP argv maps DIRECTLY to wkappbot CLI — argv[0] is the wkappbot command name, NO tool-name prefix. ");
        sb.Append("Examples: argv=[\"windows\"], argv=[\"windows\",\"--deep\"], argv=[\"prompt-probe\",\"--all\"], argv=[\"slack\",\"send\",\"hi\"]. ");
        sb.Append("⚠ Do NOT use 'wkappbot_cli' as argv[0] — that is only an MCP tool name for JSON-RPC clients, not a CLI command. ");
        sb.Append("AVAILABLE ACTIONS (argv=[\"a11y\",\"<action>\",...] for a11y, or argv=[\"<cmd>\",...] for other commands): ");
        sb.Append(McpActionDesc.Replace("\n", " | "));
        sb.Append(" | ⚠ prompt-probe: omit --all for fast scan; --all includes hidden windows but UIA tree traversal can timeout on heavy Electron apps. ");
        sb.Append("FILESYSTEM TOOLS (read-only — use for code exploration): ");
        sb.Append("argv=[\"file\",\"read\",\"<path>\"] — read file with line numbers (--offset N, --limit N). ");
        sb.Append("argv=[\"file\",\"grep\",\"<regex>\",\"--path\",\"<dir>\",\"--type\",\"<ext>\"] — regex search in files (-i ignore-case, -C N context lines, --max N). ");
        sb.Append("argv=[\"file\",\"glob\",\"<pattern>\",\"--path\",\"<dir>\"] — find files with ** glob. ");
        sb.Append("⚠ GLOB RULES: ALWAYS use **/ prefix for recursive search (e.g. \"**/*.cs\", \"**/*WebFetch*\", \"**/*Commands.cs\"). ");
        sb.Append("NEVER use paths without **/ (e.g. \"Commands/File.cs\" finds nothing). NEVER use leading slash. ");
        sb.Append("WEB TOOLS: ");
        sb.Append("argv=[\"web\",\"fetch\",\"<url>\"] — HTTP GET, returns response body (--max-chars N). ");
        sb.Append("argv=[\"web\",\"search\",\"<query>\"] — Google search via WebBot Chrome CDP, no API key needed (--limit N). ");
        sb.Append("argv=[\"web\",\"read\",\"<url>\"] — navigate URL and read rendered text content (--max-chars N). ");
        sb.Append("After tool_result, emit next TOOL_CALL(s) or give final answer. ");
        sb.Append("PROACTIVE BEHAVIOR: Do not wait to be asked for each step. ");
        sb.Append("After completing a task: (a) summarize key findings in 2-4 bullet points, ");
        sb.Append("(b) if the result suggests follow-up exploration, do it immediately with another TOOL_CALL, ");
        sb.Append("(c) end with a short 'NEXT STEPS' list of 2-3 actionable suggestions. ");
        sb.Append("If a tool_result is empty or minimal, investigate deeper automatically — do not just report empty. ");
        sb.Append("Prefer chaining tool calls (inspect → click → verify) over asking the user what to do next.");
        return sb.ToString();
    }

    static async Task<bool> HasLoopPersonaStateAsync(CdpClient cdp, string provider)
        => await cdp.HasSessionStateAsync($"wkappbot.loop.persona.{provider}", LoopPersonaStateVersion);

    static async Task SetLoopPersonaStateAsync(CdpClient cdp, string provider)
        => await cdp.SetSessionStateAsync($"wkappbot.loop.persona.{provider}", LoopPersonaStateVersion);

    static string LoopProviderLabel(string provider) => provider switch
    {
        "gpt" => "ChatGPT", "gemini" => "Gemini", "claude" => "Claude", _ => provider
    };
}
