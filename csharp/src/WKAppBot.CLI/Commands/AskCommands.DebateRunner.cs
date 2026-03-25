using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Runs the 정반합 debate loop on top of AskTriadParallel.
/// Called after R1 (initial parallel answers) to orchestrate R2 (critique) and R3 (synthesis).
/// </summary>
internal partial class Program
{
    // 🦉 Moderator assigns animal emoji by R0 finish order (dictator style!)
    static readonly string[] _speedEmojis = ["🦊", "🐬", "🐙"]; // 1st, 2nd, 3rd
    static readonly ConcurrentDictionary<string, string> _aiEmoji = new();
    static int _emojiIndex;

    static void AssignEmojiOnFinish(string ai)
    {
        var idx = Interlocked.Increment(ref _emojiIndex) - 1;
        if (idx < _speedEmojis.Length)
            _aiEmoji[ai] = _speedEmojis[idx];
    }

    static void ResetEmojis() { _aiEmoji.Clear(); Interlocked.Exchange(ref _emojiIndex, 0); }

    static string AiDisplayName(string ai)
    {
        var emoji = _aiEmoji.GetValueOrDefault(ai, "🤖");
        return ai switch
        {
            "gpt" => $"{emoji} GPT(SKEPTIC)",
            "gemini" => $"{emoji} Gemini(EXPLORER)",
            "claude" => $"{emoji} Claude(AUDITOR)",
            _ => $"{emoji} {ai}",
        };
    }
    /// <summary>
    /// Execute a 정반합 debate round: send prompt to all AIs in parallel, collect structured responses.
    /// Each AI gets a prompt via `ask {ai} "prompt"` and the response is parsed for [CLAIM] markers.
    /// </summary>
    static List<TriadDebateLoop.RoundResult> RunDebateRound(
        string roundName,
        Dictionary<string, string> prompts, // ai → prompt
        int timeoutSec,
        TriadSharedContext ctx)
    {
        var results = new ConcurrentBag<TriadDebateLoop.RoundResult>();

        Console.WriteLine($"[DEBATE:{roundName}] Starting parallel round...");
        // Post moderator's instruction to Slack (first prompt's first 200 chars)
        var samplePrompt = prompts.Values.FirstOrDefault() ?? "";
        var promptPreview = samplePrompt.Length > 200 ? samplePrompt[..200] + "..." : samplePrompt;
        SlackPostToThread($"🎙️ *[Moderator → {roundName}]*: {promptPreview}", "🦉 Moderator");

        var tasks = prompts.Select(kv => Task.Run(async () =>
        {
            var ai = kv.Key;
            var prompt = kv.Value;
            var linePrefix = $"[{ai}:{roundName}] ";

            try
            {
                using var pfx = ApplyOutputPrefix(linePrefix);

                // Direct editor injection — no persona re-injection, moderator's words only
                var hasCdp = ctx._cdpClients.ContainsKey(ai);
                Console.WriteLine($"[DEBATE:{roundName}:{ai}] path={( hasCdp ? "CDP-inject" : "AskAndCapture-fallback" )}");
                var response = hasCdp
                    ? await InjectAndPollAsync(ctx, ai, prompt, timeoutSec)
                    : AskAndCapture(ai, prompt, timeoutSec); // fallback if no CDP registered
                Console.WriteLine($"[DEBATE:{roundName}:{ai}] response={( response != null ? $"{response.Length} chars" : "NULL" )}");

                if (response != null)
                {
                    // Format enforcement: retry once if required elements missing
                    var claims = TriadDebateLoop.ParseClaims(response);
                    bool needsRetry = false;
                    string? retryReason = null;

                    if (claims.Count == 0)
                    {
                        needsRetry = true;
                        retryReason = "no [CLAIM] blocks found";
                    }
                    else if (roundName.StartsWith("R2") && !response.Contains("[DISPUTE]"))
                    {
                        needsRetry = true;
                        retryReason = "no [DISPUTE] in critique round";
                    }
                    else if (roundName.StartsWith("R3"))
                    {
                        if (!response.Contains("[CONCLUSION_KR]"))
                        {
                            needsRetry = true;
                            retryReason = "no [CONCLUSION_KR] in synthesis round";
                        }
                        else
                        {
                            // Check [합의] content quality: must be substantial (≥100 Korean chars)
                            var krStart = response.IndexOf("[CONCLUSION_KR]");
                            var krEnd = response.IndexOf("[/CONCLUSION_KR]");
                            if (krStart >= 0 && krEnd > krStart)
                            {
                                var krBlock = response[(krStart + "[CONCLUSION_KR]".Length)..krEnd];
                                var haIdx = krBlock.IndexOf("[합의]");
                                var miIdx = krBlock.IndexOf("[미합의]");
                                if (haIdx >= 0 && miIdx > haIdx)
                                {
                                    var consensus = krBlock[(haIdx + "[합의]".Length)..miIdx].Trim();
                                    // Count words (whitespace split, filter empties)
                                    var wordCount = consensus.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
                                    if (wordCount < 100)
                                    {
                                        needsRetry = true;
                                        retryReason = $"[합의] too brief ({wordCount} words, need ≥100). Explain WHAT was agreed and WHY, not just '동의했습니다'";
                                    }

                                    // Check [개인의견] quality: ≥20 words
                                    if (!needsRetry)
                                    {
                                        var opIdx = krBlock.IndexOf("[개인의견]");
                                        if (opIdx >= 0)
                                        {
                                            var opinion = krBlock[(opIdx + "[개인의견]".Length)..].Trim();
                                            var opWords = opinion.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
                                            if (opWords < 20)
                                            {
                                                needsRetry = true;
                                                retryReason = $"[개인의견] too brief ({opWords} words, need ≥20). Share your honest personal view";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (needsRetry && hasCdp)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[DEBATE:{roundName}:{ai}] Format violation: {retryReason} → requesting revision");
                        Console.ResetColor();
                        SlackPostToThread($"🔄 *[Moderator→{ai}]* Format violation: {retryReason}. Requesting revision.", "🦉 Moderator");

                        var retryPrompt = $"[MODERATOR] ⚠️ Your response is missing required format: {retryReason}.\n" +
                            (roundName.StartsWith("R2")
                                ? "You MUST include:\n• At least 2 [CLAIM] blocks\n• At least 1 [DISPUTE] block\n• [STANCE N=? R=? C=? E=? D=?] with D >= 1\nPlease revise your answer now."
                                : "You MUST include:\n• At least 2 [CLAIM] blocks\n• [CONCLUSION_KR] with [합의]/[미합의]/[개인의견]\n• [합의] must be at least 100 words IN KOREAN (한국어 100단어 이상!)\n• [STANCE N=? R=? C=? E=? D=?]\n한국어로 충분히 상세하게 다시 응답해주세요.");
                        var retry = await InjectAndPollAsync(ctx, ai, retryPrompt, Math.Min(timeoutSec, 60));
                        if (retry != null)
                        {
                            response = retry;
                            claims = TriadDebateLoop.ParseClaims(response);
                        }
                    }

                    var summary = TriadDebateLoop.CompressSummary(response);
                    var result = new TriadDebateLoop.RoundResult(ai, summary, claims);

                    ctx.LogStep(ai, $"[{roundName}] {claims.Count} claims: {string.Join("; ", claims.Select(c => $"{c.Text[..Math.Min(50, c.Text.Length)]}({c.Confidence:F1})"))}");
                    results.Add(result);

                    Console.WriteLine($"[DEBATE:{roundName}:{ai}] {claims.Count} claims extracted{(needsRetry ? " (after retry)" : "")}");
                    SlackPostToThread($"📋 *[{roundName}:{AiDisplayName(ai)}]* {claims.Count} claims{(needsRetry ? " 🔄(revised)" : "")}", AiDisplayName(ai));

                    // D=0 warning: critique round requires dissent — warn if AI didn't challenge anything
                    if (roundName.StartsWith("R2", StringComparison.OrdinalIgnoreCase))
                    {
                        var stance = TriadDebateLoop.ParseStance(response);
                        int dissentScore = 0;
                        if (stance != null)
                        {
                            dissentScore = stance.Dissent;
                        }
                        else
                        {
                            // Fallback: parse from DEBATE_JSON
                            var debateJson = TriadDebateLoop.ParseDebateJson(response);
                            dissentScore = debateJson?["stance"]?["D"]?.GetValue<int>() ?? -1;
                        }

                        if (dissentScore == 0)
                        {
                            var disputes = TriadDebateLoop.ParseDisputes(response);
                            if (disputes.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"[DEBATE:{roundName}:{ai}] WARNING: D=0 with no disputes — critique round requires dissent!");
                                Console.ResetColor();
                                SlackPostToThread($"⚠️ *[Moderator→{ai}]* D=0 + no [DISPUTE] in critique round! You MUST challenge at least one peer assumption. Revise your STANCE.", "🦉 Moderator");
                                // DM the AI to fix their response
                                if (ctx._cdpClients.ContainsKey(ai))
                                {
                                    var dmPrompt = "[MODERATOR DM] Your STANCE has D=0 (no dissent) and you filed no [DISPUTE] blocks. " +
                                        "This is the CRITIQUE round — your job is to find weaknesses in peer arguments. " +
                                        "Please add at least one [DISPUTE]{\"target_assumption\":\"...\",\"reason\":\"...\"}[/DISPUTE] " +
                                        "and set D >= 1 in your STANCE. Revise now.";
                                    ctx.InjectToSingle(ai, dmPrompt);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] No response");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] Error: {ex.Message}");
            }
        })).ToArray();

        // WhenAny loop: nudge remaining AIs as each one finishes (R1 pattern)
        var aiKeys = prompts.Keys.ToArray();
        var pending = tasks.ToList();
        while (pending.Count > 0)
        {
            var done = Task.WhenAny(pending).GetAwaiter().GetResult();
            var idx = Array.IndexOf(tasks, done);
            pending.Remove(done);

            if (pending.Count > 0 && idx >= 0 && idx < aiKeys.Length)
            {
                var doneAi = aiKeys[idx];
                Console.WriteLine($"[DEBATE:{roundName}] {doneAi} done → nudging {pending.Count} remaining");
                SlackPostToThread($"⏰ *{doneAi}* finished {roundName}! Moderator: wrap up, {pending.Count} AI(s) remaining.", "🦉 Moderator");

                // Inject nudge directly into still-running AIs' editors
                foreach (var p in pending)
                {
                    var pIdx = Array.IndexOf(tasks, p);
                    if (pIdx >= 0 && pIdx < aiKeys.Length)
                    {
                        var peerAi = aiKeys[pIdx];
                        var nudge = $"[MODERATOR]: ⏰ {doneAi} has finished {roundName}. You're still writing — please wrap up promptly. Other participants are waiting.";
                        if (ctx._cdpClients.ContainsKey(peerAi))
                            ctx.InjectToSingle(peerAi, nudge);
                        else
                            ctx.UpdateChunk("moderator", nudge); // fallback: broadcast
                    }
                }
            }
        }

        var roundResults = results.ToList();
        Console.WriteLine($"[DEBATE:{roundName}] Complete: {roundResults.Count}/{prompts.Count} AIs responded");
        Console.Out.Flush();
        return roundResults;
    }

    /// <summary>
    /// Ask an AI and capture the full response text (one-shot, no loop).
    /// Returns the AI's response text, or null on failure.
    /// </summary>
    static string? AskAndCapture(string ai, string prompt, int timeoutSec)
    {
        var originalOut = Console.Out;
        var capture = new StringWriter();
        var tee = new System.IO.TextWriter[] { originalOut, capture };

        // Write prompt to temp file (may be too long for command line)
        var tmpFile = Path.Combine(Path.GetTempPath(), $"debate_{ai}_{DateTime.Now:HHmmss}.txt");
        File.WriteAllText(tmpFile, prompt);

        try
        {
            // Stream to Slack in real-time (don't suppress — user wants live updates)

            var askArgs = new List<string> { ai };
            foreach (var line in prompt.Split('\n'))
            {
                var trimmed = line.TrimEnd('\r');
                if (trimmed.Length > 0) askArgs.Add(trimmed);
            }
            askArgs.Add("--timeout");
            askArgs.Add(timeoutSec.ToString());
            // No --new-session: debate continues in existing session (game within session)
            askArgs.Add("--no-dry-run"); // debate system operation, not user tool call

            // Capture output
            var outputCapture = new StringWriter();
            var teeWriter = new DebateTeeWriter(originalOut, outputCapture);
            Console.SetOut(teeWriter);

            try { AskCommand(askArgs.ToArray()); }
            finally { Console.SetOut(originalOut); }

            var output = outputCapture.ToString();

            // Extract answer from markers
            var beginIdx = output.IndexOf("[ASK_FULL_ANSWER_BEGIN]");
            var endIdx = output.IndexOf("[ASK_FULL_ANSWER_END]");
            if (beginIdx >= 0 && endIdx > beginIdx)
                return output[(beginIdx + "[ASK_FULL_ANSWER_BEGIN]".Length)..endIdx].Trim();

            // Fallback: return last substantial block (keep debate format lines like [CLAIM], [CONCLUSION_KR])
            var skipPrefixes = new[] { "[ASK]", "[ASK_", "[CDP", "[SANDBOX", "[ZOOM", "[AAR", "[POLL", "[SEND", "[SERVER", "[EDITOR", "[PERSONA", "[FLUSH", "[RUNNING", "[NAME]", "[CMD]", "[SLACK]", "[LOG]" };
            var lines = output.Split('\n').Where(l => l.Length > 20 && !skipPrefixes.Any(p => l.StartsWith(p))).ToArray();
            return lines.Length > 0 ? string.Join("\n", lines.TakeLast(50)) : null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[DEBATE] AskAndCapture({ai}): {ex.Message}");
            return null;
        }
        finally
        {
            try { File.Delete(tmpFile); } catch { }
        }
    }

    /// <summary>Simple tee writer that writes to two TextWriters simultaneously (debate-specific).</summary>
    sealed class DebateTeeWriter : TextWriter
    {
        private readonly TextWriter _a, _b;
        public DebateTeeWriter(TextWriter a, TextWriter b) { _a = a; _b = b; }
        public override Encoding Encoding => _a.Encoding;
        public override void Write(char value) { _a.Write(value); _b.Write(value); }
        public override void Write(string? value) { _a.Write(value); _b.Write(value); }
        public override void WriteLine(string? value) { _a.WriteLine(value); _b.WriteLine(value); }
        public override void Flush() { _a.Flush(); _b.Flush(); }
    }

    /// <summary>
    /// Full 정반합 debate: R1 → R2 → R3 with convergence checking.
    /// Called after initial triad parallel run (R1 data collected from TriadSharedContext).
    /// </summary>
    static void RunDebateLoop(string question, int timeoutSec, TriadSharedContext ctx)
    {
        // Debate rounds enforce dry-run: AI tool calls are read-only (inspect/read OK, click/type/edit blocked)
        _dryRunMode.Value = true;

        var ais = new[] { "gemini", "gpt", "claude" };

        Console.WriteLine("\n[DEBATE] ═══ 사회자: R2/R3 시작 (R1 이미 완료) ═══");
        SlackPostToThread("═══ *Moderator: R2(Critique) → R3(Synthesis)* ═══\nR1 already completed. Proceeding to cross-critique.", "🦉 Moderator");

        // Inject debate rules to all AIs (game announcement, not persona)
        var rules = "[MODERATOR] 🎮 정반합 게임 시작!\n" +
            "RULES:\n" +
            "1. Use [CLAIM]{\"claim\":\"...\",\"confidence\":0.9,\"key_assumptions\":[\"...\"]}[/CLAIM] for each point\n" +
            "2. Use [DISPUTE]{\"target_assumption\":\"...\",\"reason\":\"...\"}[/DISPUTE] to challenge peers\n" +
            "3. Include [STANCE N=? R=? C=? E=? D=?] (sum=9) to show your position\n" +
            "4. Korean conclusion: [CONCLUSION_KR] with [합의]/[미합의]/[개인의견]\n" +
            "5. [합의] must be ≥100 words in Korean. [개인의견] must be ≥20 words.\n" +
            "6. ⚠️ CRITICAL: If another AI puts something in [합의] that you disagree with, you MUST list it in YOUR [미합의]. Do NOT silently accept.\n" +
            "7. Read-only tools available (file read, grep, web search). No writes.\n" +
            "Respond to the moderator's prompts. Be intellectually honest.";
        foreach (var ai in ais)
        {
            if (ctx._cdpClients.ContainsKey(ai))
                ctx.InjectToSingle(ai, rules);
        }
        SlackPostToThread($"📋 *Debate Rules injected to all AIs*", "🦉 Moderator");

        // ── R1 SKIP: already completed by AskTriadParallel/AgentCommand ──
        // Build R1 results from shared context (chunks collected during R1 streaming)
        var r1Results = ais.Select(ai =>
        {
            var latestChunk = ctx.GetLatestChunk(ai) ?? "";
            var claims = TriadDebateLoop.ParseClaims(latestChunk);
            return new TriadDebateLoop.RoundResult(ai, latestChunk, claims);
        }).Where(r => r.Summary.Length > 50).ToList();

        Console.WriteLine($"[DEBATE] R1 results from context: {r1Results.Count} AIs with content");

        if (r1Results.Count < 1)
        {
            Console.Error.WriteLine("[DEBATE] No R1 context found — cannot proceed.");
            SlackPostToThread("❌ No R1 context available for R2/R3.", "🦉 Moderator");
            return;
        }

        // ── Streaming cross-prompt: AIs react to each other in real-time ──
        RunCrossPromptLoop(ais, Math.Min(timeoutSec, 90), ctx);

        // Check convergence after cross-prompting
        var r1Convergence = TriadDebateLoop.CalculateConvergence(r1Results);
        Console.WriteLine($"[DEBATE:R1] Convergence: {r1Convergence:F2} (threshold: 0.60)");
        if (r1Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r1Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Early convergence in R1! Skipping R2/R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R1 Early Convergence* (Jaccard={r1Convergence:F2}) — debate skipped, consensus reached", "🦉 Moderator");
            PostDebateSummary(r1Results, 1);
            return;
        }

        // ── R2: Critique (adversarial, anonymous labels) ──
        var r2Prompts = ais.Where(ai => r1Results.Any(r => r.Ai.Equals(ai, StringComparison.OrdinalIgnoreCase)))
            .ToDictionary(ai => ai, ai => TriadDebateLoop.BuildR2Prompt(question, r1Results, ai));
        var r2Results = RunDebateRound("R2", r2Prompts, timeoutSec, ctx);

        // Proceed with whatever we have (even 1 response)
        var bestResults = r2Results.Count > 0 ? r2Results : r1Results;

        var r2Convergence = TriadDebateLoop.CalculateConvergence(r2Results);
        Console.WriteLine($"[DEBATE:R2] Convergence: {r2Convergence:F2}");
        if (r2Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r2Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Convergence in R2! Skipping R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R2 Convergence* (Jaccard={r2Convergence:F2}) — R3 skipped", "🦉 Moderator");
            PostDebateSummary(r2Results, 2);
            return;
        }

        // ── R3: Consensus loop — repeat until [미합의] is empty ──
        SlackPostToThread("═══ *R3: Consensus Loop* ═══", "🦉 Moderator");
        var currentResults = bestResults;
        List<TriadDebateLoop.RoundResult> r3Results = new();
        const int maxR3Loops = 3;

        for (int r3i = 0; r3i < maxR3Loops; r3i++)
        {
            Console.WriteLine($"[DEBATE:R3-{r3i + 1}] Consensus attempt {r3i + 1}/{maxR3Loops}");
            SlackPostToThread($"🔄 *R3-{r3i + 1}* Consensus attempt", "🦉 Moderator");

            // Cascading consensus: AI-A first → AI-B sees A's items → AI-C sees A+B
            var priorConsensusItems = new List<string>(); // accumulates [합의] from earlier AIs
            r3Results = new();
            for (int aiIdx = 0; aiIdx < ais.Length; aiIdx++)
            {
                var ai = ais[aiIdx];
                var prompt = TriadDebateLoop.BuildR3Prompt(question, currentResults, priorConsensusItems, ai);
                Console.WriteLine($"[DEBATE:R3-{r3i + 1}:{ai}] Cascading — {priorConsensusItems.Count} prior items");
                SlackPostToThread($"🔗 *{ai}* — 이전 {priorConsensusItems.Count}개 합의 항목 포함하여 작성 중...", ai);
                var singlePrompt = new Dictionary<string, string> { [ai] = prompt };
                var singleResult = RunDebateRound($"R3-{r3i + 1}", singlePrompt, timeoutSec, ctx);
                r3Results.AddRange(singleResult);
                // Extract [합의] items from this AI's response for next AI
                foreach (var r in singleResult)
                {
                    var haStart = r.Summary.IndexOf("[합의]");
                    if (haStart < 0) continue;
                    var haEnd = r.Summary.IndexOf("[미합의]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.IndexOf("[개인의견]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.Length;
                    var haSection = r.Summary[(haStart + "[합의]".Length)..haEnd].Trim();
                    foreach (var line in haSection.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var trimmed = line.Trim().TrimStart("0123456789.•-  ".ToCharArray());
                        if (trimmed.Length > 5) priorConsensusItems.Add($"{ai}: {trimmed}");
                    }
                }
            }
            if (r3Results.Count == 0) break;

            // Parse [합의] and [미합의] from all responses
            var unresolved = new List<string>();
            bool hasConsensus = false;
            foreach (var r in r3Results)
            {
                // Check [합의] exists and non-empty
                var haStart = r.Summary.IndexOf("[합의]");
                if (haStart >= 0)
                {
                    var haEnd = r.Summary.IndexOf("[미합의]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.Length;
                    var haSection = r.Summary[(haStart + "[합의]".Length)..haEnd].Trim();
                    var haWords = haSection.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
                    if (haWords >= 100) hasConsensus = true;
                    else if (haWords > 0)
                        SlackPostToThread($"⚠️ *[Moderator→{r.Ai}]* [합의] too brief ({haWords} words, need ≥100). 한국어 100단어 이상으로 다시!", "🦉 Moderator");
                }

                // Check [미합의]
                var miStart = r.Summary.IndexOf("[미합의]");
                var miEnd = r.Summary.IndexOf("[개인의견]");
                if (miEnd < 0) miEnd = r.Summary.IndexOf("[MY_OPINION");
                if (miEnd < 0) miEnd = r.Summary.Length;
                if (miStart >= 0)
                {
                    var section = r.Summary[(miStart + "[미합의]".Length)..Math.Min(miEnd, r.Summary.Length)].Trim();
                    // "없음", "None", empty → consensus. Otherwise 2+ words = real disagreement → loop
                    var miWords = section.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                    bool hasDisagreement = miWords.Length >= 2
                        && !section.Equals("없음", StringComparison.OrdinalIgnoreCase)
                        && !section.Equals("None", StringComparison.OrdinalIgnoreCase);
                    if (hasDisagreement)
                        unresolved.Add($"[{r.Ai}] {section[..Math.Min(100, section.Length)]}");
                }
            }

            // Warn if no consensus content
            if (!hasConsensus && r3Results.Count > 0)
            {
                SlackPostToThread("⚠️ *[Moderator]* No [합의] content found! AIs must state consensus.", "🦉 Moderator");
                // Treat as unresolved
                if (unresolved.Count == 0) unresolved.Add("No consensus stated");
            }

            if (unresolved.Count == 0)
            {
                // Cross-check: extract [합의] items per AI and compare content
                var aiItems = new Dictionary<string, List<string>>(); // ai → item texts
                foreach (var r in r3Results)
                {
                    var cStart = r.Summary.IndexOf("[합의]");
                    var cEnd = r.Summary.IndexOf("[미합의]", Math.Max(0, cStart));
                    if (cEnd < 0) cEnd = r.Summary.IndexOf("[개인의견]", Math.Max(0, cStart));
                    if (cEnd < 0) cEnd = r.Summary.Length;
                    if (cStart < 0) continue;

                    var section = r.Summary[(cStart + "[합의]".Length)..cEnd];
                    var items = section.Split('\n')
                        .Select(l => l.TrimStart('-', '•', ' ', '\t'))
                        .Select(l => System.Text.RegularExpressions.Regex.Replace(l, @"^\d+[.)]\s*", "").Trim())
                        .Where(l => l.Length > 10)
                        .ToList();
                    if (items.Count == 0) // fallback: sentence split
                        items = section.Split('.', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim()).Where(s => s.Length > 10).ToList();
                    aiItems[r.Ai] = items;
                }

                if (aiItems.Count >= 2)
                {
                    // For each item from each AI, check if peers have a similar item (keyword overlap >= 3)
                    var confirmed = new List<string>();   // items found in all AIs
                    var unverified = new List<(string ai, string item, List<string> missingFrom)>(); // items only in some AIs

                    var allAis = aiItems.Keys.ToList();
                    var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // dedup

                    foreach (var (srcAi, items) in aiItems)
                    {
                        foreach (var item in items)
                        {
                            var itemKey = TriadDebateLoop.Tokenize(item);
                            if (itemKey.Count < 2) continue;
                            var keyStr = string.Join("|", itemKey.OrderBy(k => k));
                            if (!seen.Add(keyStr)) continue; // already checked similar item

                            var missing = new List<string>();
                            foreach (var peerAi in allAis)
                            {
                                if (peerAi == srcAi) continue;
                                bool found = aiItems[peerAi].Any(peerItem =>
                                {
                                    var peerKey = TriadDebateLoop.Tokenize(peerItem);
                                    return itemKey.Intersect(peerKey).Count() >= Math.Min(3, itemKey.Count);
                                });
                                if (!found) missing.Add(peerAi);
                            }

                            if (missing.Count == 0)
                                confirmed.Add(item);
                            else
                                unverified.Add((srcAi, item, missing));
                        }
                    }

                    if (unverified.Count > 0)
                    {
                        // Build structured report
                        var reportSb = new StringBuilder();
                        reportSb.AppendLine($"🔍 *[Moderator] 합의 항목 교차검증* — 확인={confirmed.Count}, 미확인={unverified.Count}");
                        if (confirmed.Count > 0)
                        {
                            reportSb.AppendLine("\n*✅ 확인된 합의:*");
                            foreach (var c in confirmed.Take(10))
                                reportSb.AppendLine($"  • {c[..Math.Min(80, c.Length)]}");
                        }
                        reportSb.AppendLine("\n*❓ 미확인 항목 (일부 AI만 언급):*");
                        foreach (var (ai, item, missing) in unverified.Take(10))
                        {
                            var snippet = item.Length > 60 ? item[..60] + "…" : item;
                            reportSb.AppendLine($"  • [{ai}] \"{snippet}\" — missing from: {string.Join(", ", missing)}");
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[DEBATE:R3] {unverified.Count} unverified consensus items");
                        Console.ResetColor();
                        SlackPostToThread(reportSb.ToString(), "🦉 Moderator");

                        // ── Step 1: Broadcast full report to ALL AIs (공개 양식) ──
                        var broadcastSb = new StringBuilder();
                        broadcastSb.AppendLine("[MODERATOR] 합의 항목 교차검증 결과입니다. 모든 AI에 공개합니다.");
                        if (confirmed.Count > 0)
                        {
                            broadcastSb.AppendLine($"\n[확인된 합의] ({confirmed.Count}건):");
                            for (int ci = 0; ci < Math.Min(confirmed.Count, 10); ci++)
                                broadcastSb.AppendLine($"  {ci + 1}. {confirmed[ci][..Math.Min(80, confirmed[ci].Length)]}");
                        }
                        broadcastSb.AppendLine($"\n[미확인 항목] ({unverified.Count}건 — 일부 AI만 언급):");
                        for (int ui = 0; ui < Math.Min(unverified.Count, 10); ui++)
                        {
                            var (srcAi2, item2, missing2) = unverified[ui];
                            broadcastSb.AppendLine($"  {ui + 1}. [{srcAi2}] \"{item2[..Math.Min(60, item2.Length)]}\" — missing from: {string.Join(", ", missing2)}");
                        }
                        broadcastSb.AppendLine("\n각 AI는 미확인 항목에 대해 동의/거부를 밝혀주세요.");
                        broadcastSb.AppendLine("전체 [합의]/[미합의]/[개인의견] 양식을 다시 출력해주세요.");

                        // Broadcast to all AIs
                        foreach (var ai in ais)
                        {
                            if (ctx._cdpClients.ContainsKey(ai))
                                ctx.InjectToSingle(ai, broadcastSb.ToString());
                        }

                        // ── Step 2: DM objection prompts to specific AIs (이의제기 타겟) ──
                        var perAiMissing = new Dictionary<string, List<string>>();
                        foreach (var (srcAi, item, missing) in unverified)
                        {
                            foreach (var m in missing)
                            {
                                if (!perAiMissing.ContainsKey(m)) perAiMissing[m] = new();
                                perAiMissing[m].Add($"[{srcAi}]: \"{item[..Math.Min(80, item.Length)]}\"");
                            }
                        }

                        foreach (var (targetAi, missingItems) in perAiMissing)
                        {
                            var dmSb = new StringBuilder();
                            dmSb.AppendLine($"[MODERATOR DM → {targetAi} only] 아래 항목들이 당신의 [합의]에 빠져있습니다:");
                            foreach (var mi in missingItems.Take(5))
                                dmSb.AppendLine($"  ⚡ {mi}");
                            dmSb.AppendLine("각 항목에 대해: 동의하면 [합의]에 추가, 반대하면 [미합의]에 이유와 함께 넣어주세요.");

                            if (ctx._cdpClients.ContainsKey(targetAi))
                                ctx.InjectToSingle(targetAi, dmSb.ToString());

                            // Slack에도 DM 내용 공개 (유저 모니터링용)
                            SlackPostToThread($"📩 *[DM→{targetAi}]* {missingItems.Count}건 이의제기:\n{string.Join("\n", missingItems.Take(3).Select(m => $"  {m}"))}", "🦉 Moderator");
                        }

                        unresolved.Add($"Cross-check: {unverified.Count} unverified consensus items");
                    }
                }
            }

            if (unresolved.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[DEBATE:R3] Full consensus reached! (attempt {r3i + 1})");
                Console.ResetColor();
                SlackPostToThread($"✅ *Full consensus!* (R3-{r3i + 1})", "🦉 Moderator");
                break;
            }

            Console.WriteLine($"[DEBATE:R3] {unresolved.Count} unresolved items");
            SlackPostToThread($"⚠️ *{unresolved.Count} 미합의 남음* → 다음 라운드", "🦉 Moderator");

            // Inject unresolved items back for next round
            currentResults = r3Results;
        }

        var finalResults = r3Results.Count > 0 ? r3Results : bestResults;
        var r3Convergence = TriadDebateLoop.CalculateConvergence(finalResults);
        Console.WriteLine($"[DEBATE:R3] Final convergence: {r3Convergence:F2}");

        // ── Cross-verify: extract consensus from intersection of all AI claims ──
        var allClaims = finalResults.SelectMany(r => r.Claims.Select(c => (r.Ai, c))).ToList();
        var consensusClaims = new List<string>();
        var dissentClaims = new List<(string ai, string claim)>();

        foreach (var (ai, claim) in allClaims)
        {
            // Check if similar claim exists in other AIs (simple keyword overlap)
            bool shared = allClaims.Any(other =>
                other.Ai != ai &&
                TriadDebateLoop.Tokenize(claim.Text).Intersect(TriadDebateLoop.Tokenize(other.c.Text)).Count() >= 3);
            if (shared)
                consensusClaims.Add(claim.Text);
            else
                dissentClaims.Add((ai, claim.Text));
        }

        // Post final consensus to Slack
        var sb = new StringBuilder();
        sb.AppendLine("═══ *정반합 최종 결과* ═══\n");
        sb.AppendLine($"📊 Convergence: {r3Convergence:F2}\n");

        if (consensusClaims.Count > 0)
        {
            sb.AppendLine("*[합의]*");
            foreach (var c in consensusClaims.Distinct().Take(5))
                sb.AppendLine($"  • {c[..Math.Min(100, c.Length)]}");
        }
        if (dissentClaims.Count > 0)
        {
            sb.AppendLine("\n*[미합의]*");
            foreach (var (ai, c) in dissentClaims.Take(5))
                sb.AppendLine($"  • [{ai}] {c[..Math.Min(100, c.Length)]}");
        }

        // Extract personal opinions [개인의견] or [CONCLUSION_KR]
        sb.AppendLine("\n*[개인의견]*");
        foreach (var r in finalResults)
        {
            string? opinion = null;
            // Try [개인의견] first
            var opStart = r.Summary.IndexOf("[개인의견]");
            var opEnd = r.Summary.IndexOf("[/CONCLUSION_KR]");
            if (opEnd < 0) opEnd = r.Summary.Length;
            if (opStart >= 0)
                opinion = r.Summary[(opStart + "[개인의견]".Length)..Math.Min(opEnd, r.Summary.Length)].Trim();
            // Fallback: [CONCLUSION_KR]
            if (string.IsNullOrEmpty(opinion))
            {
                var krStart = r.Summary.IndexOf("[CONCLUSION_KR]");
                var krEnd = r.Summary.IndexOf("[/CONCLUSION_KR]");
                if (krStart >= 0 && krEnd > krStart)
                    opinion = r.Summary[(krStart + "[CONCLUSION_KR]".Length)..krEnd].Trim();
            }

            if (!string.IsNullOrEmpty(opinion) && opinion.Length >= 100)
            {
                sb.AppendLine($"  🗣️ *{r.Ai}*: {opinion[..Math.Min(300, opinion.Length)]}");
            }
            else if (!string.IsNullOrEmpty(opinion))
            {
                sb.AppendLine($"  🗣️ *{r.Ai}*: {opinion} ⚠️(too brief)");
                SlackPostToThread($"⚠️ *[Moderator→{r.Ai}]* Personal opinion too brief ({opinion.Length} chars). Express yourself fully!", "🦉 Moderator");
            }
            else
            {
                // Fallback: first 100 chars of summary
                sb.AppendLine($"  🗣️ *{r.Ai}*: {r.Summary[..Math.Min(100, r.Summary.Length)]}...");
            }
        }

        SlackPostToThread(sb.ToString(), "🦉 Moderator");
        Console.WriteLine("[DEBATE] ═══ 정반합 토론 완료 ═══");
        SlackPostToThread("═══ *정반합 토론 완료* ═══", "🦉 Moderator");
    }

    /// <summary>
    /// Streaming cross-prompt loop: inject peer chunks into each AI after their R1 response.
    /// Called from the main triad flow — runs concurrently with R1 polling.
    /// Each AI that finishes early picks up peer chunks and auto-reacts.
    /// Max 3 cross-prompt rounds per AI to prevent infinite loop.
    /// </summary>
    static void RunCrossPromptLoop(string[] ais, int timeoutSec, TriadSharedContext ctx)
    {
        const int maxCrossRounds = 3;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Console.WriteLine("[CROSS] ═══ Real-time cross-prompting started ═══");
        SlackPostToThread("🔄 *Real-time cross-prompting started!", "🦉 Moderator");

        var crossTasks = ais.Select(ai => Task.Run(async () =>
        {
            var peerSeen = new ConcurrentDictionary<string, int>();

            for (int round = 0; round < maxCrossRounds; round++)
            {
                if (sw.Elapsed.TotalSeconds > timeoutSec) break;

                // Wait for peer chunks to accumulate
                await Task.Delay(5000);

                var peers = ctx.GetPeerChunks(ai, peerSeen);
                if (peers.Count == 0) continue;

                // Build cross-prompt message
                var crossMsg = new StringBuilder();
                crossMsg.AppendLine($"[CROSS-PROMPT round {round + 1}] Other AIs are arguing:");
                foreach (var (fromAi, text) in peers)
                    crossMsg.AppendLine($"  [{fromAi}]: {text}");
                crossMsg.AppendLine("React briefly — agree, disagree, or refine. Use [CLAIM] format.");

                Console.WriteLine($"[CROSS:{ai}] Round {round + 1}: {peers.Count} peer chunk(s) → injecting");
                SlackPostToThread($"🔀 *[CROSS:{ai} R{round + 1}]* {peers.Count} peer chunk(s) injected", ai);

                // Inject as follow-up ask (reuses existing session)
                var response = AskAndCapture(ai, crossMsg.ToString(), Math.Min(timeoutSec, 60));
                if (response != null)
                {
                    var claims = TriadDebateLoop.ParseClaims(response);
                    ctx.LogStep(ai, $"[CROSS-R{round + 1}] {claims.Count} claims after peer review");

                    // Share back via chunk update
                    ctx.UpdateChunk(ai, response);

                    SlackPostToThread(
                        $"💬 *[{ai} CROSS-R{round + 1}]* {claims.Count} claims" +
                        (claims.Count > 0 ? $": {claims[0].Text[..Math.Min(60, claims[0].Text.Length)]}..." : ""),
                        ai);
                }

                // Check convergence after each cross-round
                var allResults = ais.Select(a =>
                {
                    var chunks = ctx.GetPeerChunks("__none__", new ConcurrentDictionary<string, int>());
                    // Build rough results from latest chunks
                    var latestChunk = "";
                    if (ctx.GetPeerChunks(a, new ConcurrentDictionary<string, int>()).Count > 0)
                        latestChunk = ctx.GetPeerChunks(a, new ConcurrentDictionary<string, int>()).Last().chunk;
                    return new TriadDebateLoop.RoundResult(a, latestChunk, TriadDebateLoop.ParseClaims(latestChunk));
                }).Where(r => r.Claims.Count >= 2).ToList(); // min 2 claims per AI

                if (allResults.Count >= 2) // min 2 AIs with substantive claims
                {
                    var conv = TriadDebateLoop.CalculateConvergence(allResults);
                    Console.WriteLine($"[CROSS:{ai}] Convergence: {conv:F2} ({allResults.Count} AIs with 2+ claims)");
                    SlackPostToThread($"📊 *[Convergence]* {conv:F2} ({allResults.Count} AIs)", "🦉 Moderator");
                    if (conv >= 0.6 && conv < 1.0 && !TriadDebateLoop.HasContradictions(allResults)) // 1.0 = suspicious
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[CROSS] Convergence reached! ({conv:F2})");
                        Console.ResetColor();
                        SlackPostToThread($"✅ *Convergence reached!* Jaccard={conv:F2} — debate complete", "🦉 Moderator");
                        return;
                    }
                }
            }
        })).ToArray();

        Task.WaitAll(crossTasks);
        Console.WriteLine("[CROSS] ═══ Cross-prompting complete ═══");
    }

    /// <summary>
    /// After AI response completes, check if cross-prompt text was pre-typed in editor.
    /// If so, send it immediately (Enter key). Returns true if cross-prompt was sent.
    /// </summary>
    internal static async Task<bool> SendPendingCrossPromptAsync(WKAppBot.WebBot.CdpClient cdp, string ai, string editorSel)
    {
        try
        {
            var editorLen = await cdp.GetTextLengthAsync(editorSel);
            if (editorLen > 10) // cross-prompt text was pre-typed
            {
                Console.WriteLine($"[CROSS:{ai}] Found pre-typed cross-prompt ({editorLen} chars) → sending");
                await cdp.SendPromptAsync(editorSel);
                SlackPostToThread($"🔀 *[{ai}]* Cross-prompt sent ({editorLen} chars)", ai);
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>Inject moderator prompt directly into AI editor + poll response. No persona re-injection.</summary>
    static async Task<string?> InjectAndPollAsync(TriadSharedContext ctx, string ai, string prompt, int timeoutSec)
    {
        try
        {
            var editorSel = ai.ToLowerInvariant() switch { "gemini" => ".ql-editor", "gpt" => "#prompt-textarea", "claude" => "div.tiptap.ProseMirror", _ => null };
            if (editorSel == null || !ctx._cdpClients.TryGetValue(ai, out var cdp))
            {
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: no CDP client or editor selector");
                return null;
            }

            // Verify CDP is still connected — try reconnect before fallback
            if (!cdp.IsConnected)
            {
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: CDP disconnected — attempting reconnect...");
                try
                {
                    // Reconnect using same tab ID (the tab is still open, just WebSocket dropped)
                    await cdp.ReconnectAsync();
                    if (cdp.IsConnected)
                        Console.WriteLine($"[DEBATE:INJECT] {ai}: CDP reconnected!");
                    else
                    {
                        Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: reconnect failed — falling back to AskAndCapture");
                        return AskAndCapture(ai, prompt, timeoutSec);
                    }
                }
                catch (Exception rex)
                {
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: reconnect error: {rex.Message} — falling back");
                    return AskAndCapture(ai, prompt, timeoutSec);
                }
            }

            // Capture baseline response count + text BEFORE injection
            int baselineCount = 0;
            var baseline = "";
            try
            {
                // Count existing response blocks (model-response / [role="article"])
                var countStr = await cdp.EvalAsync(
                    "(()=>{var r=document.querySelectorAll('model-response');if(!r.length)r=document.querySelectorAll('[role=\"article\"]');return r.length})()");
                int.TryParse(countStr, out baselineCount);
                baseline = await cdp.GetLastResponseTextAsync() ?? "";
            }
            catch { }
            Console.WriteLine($"[DEBATE:INJECT] {ai}: baseline={baseline.Length} chars, turns={baselineCount}, connected={cdp.IsConnected}");

            // Inject prompt text
            try
            {
                await cdp.InsertContentEditableAsync(editorSel, prompt);
                await Task.Delay(1000); // wait for full text insertion
                // Send (Enter)
                await cdp.SendPromptAsync(editorSel);
                Console.WriteLine($"[DEBATE:INJECT] {ai}: sent moderator prompt ({prompt.Length} chars)");
            }
            catch (Exception insertEx)
            {
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: insert/send FAILED: {insertEx.Message}");
                // Fallback: try InjectToSingle (types via ctx) + SendPromptAsync
                try
                {
                    ctx.InjectToSingle(ai, prompt);
                    await Task.Delay(1500);
                    await cdp.SendPromptAsync(editorSel);
                    Console.WriteLine($"[DEBATE:INJECT] {ai}: sent via InjectToSingle fallback ({prompt.Length} chars)");
                }
                catch (Exception fb) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: fallback also failed: {fb.Message}"); return null; }
            }

            // Poll response with baseline comparison + 9s stall detection
            var sw = System.Diagnostics.Stopwatch.StartNew();
            string lastText = "";
            int stable = 0;
            var lastChunkTime = System.Diagnostics.Stopwatch.StartNew();
            int nudgeCount = 0;
            while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 90))
            {
                await Task.Delay(2000);
                string text;
                try { text = await cdp.GetLastResponseTextAsync(baselineCount) ?? ""; }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: poll error: {ex.Message}");
                    continue;
                }

                // 9s since last new chunk → nudge (max 2 times)
                if (lastChunkTime.Elapsed.TotalSeconds >= 9 && nudgeCount < 2)
                {
                    nudgeCount++;
                    var stallMsg = text.Length < 20
                        ? "You haven't started responding. Please begin your answer now."
                        : "Your response appears stalled. Please continue or wrap up.";
                    Console.WriteLine($"[DEBATE:INJECT] {ai}: stall detected ({lastChunkTime.Elapsed.TotalSeconds:F0}s) → nudge #{nudgeCount}");
                    SlackPostToThread($"⏰ *[Moderator→{ai}]* {lastChunkTime.Elapsed.TotalSeconds:F0}초 무응답 — 재촉 #{nudgeCount}", "🦉 Moderator");
                    await cdp.InsertContentEditableAsync(editorSel, $"[MODERATOR] ⏰ {stallMsg}");
                    await cdp.SendPromptAsync(editorSel);
                    lastChunkTime.Restart();
                }

                if (text.Length < 20) continue;
                if (text != lastText) { stable = 0; lastText = text; lastChunkTime.Restart(); }
                else { stable++; if (stable >= 3) return text; }
                try { var provider = ai == "gpt" ? "chatgpt" : ai;
                    if (!await cdp.IsStreamingAsync(provider)) { await Task.Delay(1000); return lastText; } } catch { }
            }
            return string.IsNullOrEmpty(lastText) ? null : lastText;
        }
        catch (Exception ex) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai} error: {ex.Message}"); }
        return null;
    }

    /// <summary>Post debate summary to Slack thread.</summary>
    static void PostDebateSummary(List<TriadDebateLoop.RoundResult> results, int roundsCompleted)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"═══ *Debate Results* (R{roundsCompleted} complete) ═══");

        foreach (var r in results)
        {
            sb.AppendLine($"\n*{r.Ai}* ({r.Claims.Count} claims):");
            foreach (var c in r.Claims.OrderByDescending(c => c.Confidence).Take(3))
                sb.AppendLine($"  • [{c.Confidence:F2}] {c.Text[..Math.Min(80, c.Text.Length)]}");
        }

        var convergence = TriadDebateLoop.CalculateConvergence(results);
        var hasContradictions = TriadDebateLoop.HasContradictions(results);
        sb.AppendLine($"\n📊 Convergence: {convergence:F2} | Contradictions: {(hasContradictions ? "⚠ YES" : "✅ No")}");

        SlackPostToThread(sb.ToString(), "🦉 Moderator");
    }
}
