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
    // Emoji + Messages: see DebateRunner.Emoji.cs and DebateRunner.Messages.cs

    /// <summary>
    /// Execute a 정반합 debate round: send prompt to all AIs in parallel, collect structured responses.
    /// Each AI gets a prompt via `ask {ai} "prompt"` and the response is parsed for [CLAIM] markers.
    /// </summary>
    static List<TriadDebateLoop.RoundResult> RunDebateRound(
        string roundName,
        Dictionary<string, string> prompts, // ai -> prompt
        int timeoutSec,
        TriadSharedContext ctx)
    {
        var results = new ConcurrentBag<TriadDebateLoop.RoundResult>();

        Console.Error.WriteLine($"[DEBATE:{roundName}] Starting parallel round...");

        // Post moderator's full instruction to Slack (no truncation)
        var samplePrompt = prompts.Values.FirstOrDefault() ?? "";
        SlackPostToThread($"🎙️ *[Moderator -> {roundName}]*: {samplePrompt}", "🦉 Moderator");

        var tasks = prompts.Select(kv => Task.Run(async () =>
        {
            var ai = kv.Key;
            var prompt = kv.Value;
            var linePrefix = $"[{ai}:{roundName}] ";

            try
            {
                using var pfx = ApplyOutputPrefix(linePrefix);

                // Direct editor injection -- no persona re-injection, moderator's words only
                var hasCdp = ctx._cdpClients.ContainsKey(ai);
                Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] path={( hasCdp ? "CDP-inject" : "AskAndCapture-fallback" )}");
                var response = hasCdp
                    ? await InjectAndPollAsync(ctx, ai, prompt, timeoutSec)
                    : AskAndCapture(ai, prompt, timeoutSec); // fallback if no CDP registered
                Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] response={( response != null ? $"{response.Length} chars" : "NULL" )}");

                if (response != null)
                {
                    // Moderator redirect: detect off-topic responses (tool exploration, CLI jargon, etc.)
                    if (IsOffTopicResponse(response))
                    {
                        var originalQ = ctx.OriginalQuestion;
                        var redirectMsg = string.IsNullOrEmpty(originalQ)
                            ? "[MODERATOR DM] !️ OFF-TOPIC DETECTED: Your response is about tools/CLI commands, not the debate question. " +
                              "STOP exploring tools. Answer the original question directly using [CLAIM] + [STANCE] format."
                            : $"[MODERATOR DM] !️ OFF-TOPIC DETECTED: Your response ignores the question. " +
                              $"Original question: {originalQ}\n" +
                              "Answer THIS question directly with [CLAIM] + [STANCE] format. NO tool exploration.";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine($"[MOD] {ai}: off-topic response detected -> redirecting");
                        Console.ResetColor();
                        SlackPostToThread($"!️ *[Moderator->{ai}]* 질문 무시 감지! 오리지날 질문으로 리다이렉트", "🦉 Moderator");
                        if (ctx._cdpClients.ContainsKey(ai)) ctx.InjectToSingle(ai, redirectMsg);
                        response = hasCdp
                            ? await InjectAndPollAsync(ctx, ai, redirectMsg, Math.Min(timeoutSec, 60))
                            : AskAndCapture(ai, redirectMsg, Math.Min(timeoutSec, 60));
                    }

                    // Format enforcement: retry once if required elements missing
                    var claims = TriadDebateLoop.ParseClaims(response ?? "");
                    bool needsRetry = false;
                    string? retryReason = null;

                    // Word limit: R2 = total ≤99 words, R3 = [합의]+[미합의] items only (삼두 합의: Option B)
                    if (roundName.StartsWith("R2"))
                    {
                        var totalWords = (response ?? "").Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
                        if (totalWords > 99)
                        {
                            SlackPostToThread($"!️ *[Moderator->{ai}]* R2 답변 {totalWords}단어 -- 99단어 초과!", "🦉 Moderator");
                            needsRetry = true;
                            retryReason = $"R2 too verbose ({totalWords} words, max 99).";
                        }
                    }
                    else if (claims.Count == 0 && !roundName.StartsWith("R3"))
                    {
                        // [CLAIM] required for R2, optional for R3 (R3 main output is [CONCLUSION_KR])
                        needsRetry = true;
                        retryReason = "no [CLAIM] blocks found";
                    }
                    else if (roundName.StartsWith("R2") && response?.Contains("[DISPUTE]") != true)
                    {
                        needsRetry = true;
                        retryReason = "no [DISPUTE] in critique round";
                    }
                    else if (roundName.StartsWith("R2") && (response?.Contains("[합의]") == true || response?.Contains("[CONCLUSION_KR]") == true))
                    {
                        // R2 round scope violation: using R3 format prematurely
                        var warnMsg = "[MODERATOR DM] !️ ROUND SCOPE VIOLATION: You used R3 format ([합의]/[CONCLUSION_KR]) in R2. " +
                            "R2 is CRITIQUE ONLY. Use [DISPUTE] + [CLAIM] + [STANCE]. Remove [합의]/[CONCLUSION_KR] and focus on critiquing peers.";
                        if (ctx._cdpClients.ContainsKey(ai))
                            ctx.InjectToSingle(ai, warnMsg);
                        SlackPostToThread($"!️ *[Moderator->{ai}]* 라운드 범위 위반! R2에서 R3 포맷([합의]/[CONCLUSION_KR]) 사용 -> DM 경고 발송", "🦉 Moderator");
                        needsRetry = true;
                        retryReason = "R2 round scope violation -- used R3 format ([합의]/[CONCLUSION_KR]). R2 = critique only!";
                    }
                    else if (roundName.StartsWith("R3"))
                    {
                        if (response?.Contains("[CONCLUSION_KR]") != true)
                        {
                            needsRetry = true;
                            retryReason = "no [CONCLUSION_KR] in synthesis round";
                        }
                        else
                        {
                            // Check [합의] content quality
                            var krStart = response.IndexOf("[CONCLUSION_KR]");
                            var krEnd = response.IndexOf("[/CONCLUSION_KR]");
                            if (krStart >= 0 && krEnd > krStart)
                            {
                                var krBlock = response[(krStart + "[CONCLUSION_KR]".Length)..krEnd];
                                var haIdx = krBlock.IndexOf("[합의]");
                                var miIdx = krBlock.IndexOf("[미합의]");
                                if (haIdx >= 0 && miIdx > haIdx)
                                {
                                    // Verbosity guard: CONCLUSION_KR should be concise (≤2000 chars)
                                    if (krBlock.Length > 2000)
                                    {
                                        SlackPostToThread($"!️ *[Moderator->{ai}]* [CONCLUSION_KR] 너무 장황! ({krBlock.Length}자, 2000자 이하로 줄이세요)", "🦉 Moderator");
                                        if (ctx._cdpClients.ContainsKey(ai))
                                            ctx.InjectToSingle(ai, $"[MODERATOR DM] !️ Your [CONCLUSION_KR] is too verbose ({krBlock.Length} chars). Keep it under 2000 chars. Be concise -- atomic items, not essays.");
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
                                    // Check [셀프힐링] presence (R3 required)
                                    if (!needsRetry && !krBlock.Contains("[셀프힐링]"))
                                    {
                                        needsRetry = true;
                                        retryReason = "missing [셀프힐링] section -- you MUST admit what you revised or got wrong from prior rounds";
                                    }
                                }
                            }
                        }
                    }

                    if (needsRetry && hasCdp)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] Format violation: {retryReason} -> requesting revision");
                        Console.ResetColor();
                        SlackPostToThread($"🔄 *[Moderator->{ai}]* Format violation: {retryReason}. Requesting revision.", "🦉 Moderator");

                        var retryPrompt = $"[MODERATOR] !️ Your response is missing required format: {retryReason}.\n" +
                            (roundName.StartsWith("R2")
                                ? "You MUST include:\n• At least 2 [CLAIM] blocks\n• At least 1 [DISPUTE] block\n• [STANCE N=? R=? C=? E=? D=?] with D >= 1\nPlease revise your answer now."
                                : "You MUST include:\n• At least 2 [CLAIM] blocks\n• [CONCLUSION_KR] with [합의]/[미합의]/[개인의견]\n• [합의] must be at least 100 words IN KOREAN (한국어 100단어 이상!)\n• [STANCE N=? R=? C=? E=? D=?]\n한국어로 충분히 상세하게 다시 응답해주세요.");
                        SlackPostToThread($"📩 *[DM->{ai}]* {retryPrompt}", "🦉 Moderator");
                        var retry = await InjectAndPollAsync(ctx, ai, retryPrompt, Math.Min(timeoutSec, 60));
                        if (retry != null)
                        {
                            response = retry;
                            claims = TriadDebateLoop.ParseClaims(response);
                        }
                    }

                    // -- EEP: Evidence Escalation Protocol -- detect restatements --
                    int restatements = 0;
                    foreach (var c in claims)
                    {
                        if (ctx.IsRestatement(ai, c.Text))
                        {
                            restatements++;
                            var count = ctx.IncrementRestatement(ai);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Error.WriteLine($"[EEP:{ai}] RESTATEMENT #{count}: {c.Text[..Math.Min(60, c.Text.Length)]}");
                            Console.ResetColor();
                            if (count >= 2)
                            {
                                SlackPostToThread($"!️ *[EEP->{AiDisplayName(ai)}]* RESTATEMENT #{count} -- 새 근거 없이 반복! 해당 항목 자동 양보 처리", "🦉 Moderator");
                                if (ctx._cdpClients.ContainsKey(ai))
                                    ctx.InjectToSingle(ai, $"[MODERATOR] !️ EEP: You restated the same claim {count} times without new evidence. This item is auto-conceded. STANCE -1.");
                            }
                            else
                                SlackPostToThread($"🔁 *[EEP:{AiDisplayName(ai)}]* 반복 감지 -- 새 근거 제시 필요!", "🦉 Moderator");
                        }
                    }
                    // Store claims for next round comparison
                    ctx.StoreClaims(ai, claims);

                    var summary = TriadDebateLoop.CompressSummary(response ?? "");
                    var result = new TriadDebateLoop.RoundResult(ai, summary, claims);

                    ctx.LogStep(ai, $"[{roundName}] {claims.Count} claims ({restatements} restated): {string.Join("; ", claims.Select(c => $"{c.Text[..Math.Min(50, c.Text.Length)]}({c.Confidence:F1})"))}");
                    results.Add(result);

                    Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] {claims.Count} claims ({restatements} restated){(needsRetry ? " (after retry)" : "")}");
                    SlackPostToThread($"📋 *[{roundName}:{AiDisplayName(ai)}]* {claims.Count} claims{(restatements > 0 ? $" 🔁{restatements} restated" : "")}{(needsRetry ? " 🔄(revised)" : "")}", AiDisplayName(ai));

                    // D=0 warning: critique round requires dissent -- warn if AI didn't challenge anything
                    if (roundName.StartsWith("R2", StringComparison.OrdinalIgnoreCase))
                    {
                        var stance = TriadDebateLoop.ParseStance(response ?? "");
                        int dissentScore = 0;
                        if (stance != null)
                        {
                            dissentScore = stance.Dissent;
                        }
                        else
                        {
                            // Fallback: parse from DEBATE_JSON
                            var debateJson = TriadDebateLoop.ParseDebateJson(response ?? "");
                            dissentScore = debateJson?["stance"]?["D"]?.GetValue<int>() ?? -1;
                        }

                        if (dissentScore == 0)
                        {
                            var disputes = TriadDebateLoop.ParseDisputes(response ?? "");
                            if (disputes.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Error.WriteLine($"[DEBATE:{roundName}:{ai}] WARNING: D=0 with no disputes -- critique round requires dissent!");
                                Console.ResetColor();
                                SlackPostToThread($"!️ *[Moderator->{ai}]* D=0 + no [DISPUTE] in critique round! You MUST challenge at least one peer assumption. Revise your STANCE.", "🦉 Moderator");
                                // DM the AI to fix their response
                                if (ctx._cdpClients.ContainsKey(ai))
                                {
                                    var dmPrompt = "[MODERATOR DM] Your STANCE has D=0 (no dissent) and you filed no [DISPUTE] blocks. " +
                                        "This is the CRITIQUE round -- your job is to find weaknesses in peer arguments. " +
                                        "Please add at least one [DISPUTE]{\"target_assumption\":\"...\",\"reason\":\"...\"}[/DISPUTE] " +
                                        "and set D >= 1 in your STANCE. Revise now.";
                                    ctx.InjectToSingle(ai, dmPrompt);
                                    SlackPostToThread($"📩 *[DM->{ai}]* {dmPrompt}", "🦉 Moderator");
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

            if (idx >= 0 && idx < aiKeys.Length)
            {
                var doneAi = aiKeys[idx];
                var hasResponse = results.Any(r => r.Ai.Equals(doneAi, StringComparison.OrdinalIgnoreCase));

                if (pending.Count > 0 && hasResponse)
                {
                    Console.Error.WriteLine($"[DEBATE:{roundName}] {AiDisplayName(doneAi)} done -> nudging {pending.Count} remaining");
                    SlackPostToThread($"⏰ *{AiDisplayName(doneAi)}* finished {roundName}! Moderator: wrap up, {pending.Count} AI(s) remaining.", "🦉 Moderator");

                    // Inject nudge directly into still-running AIs' editors
                    foreach (var p in pending)
                    {
                        var pIdx = Array.IndexOf(tasks, p);
                        if (pIdx >= 0 && pIdx < aiKeys.Length)
                        {
                            var peerAi = aiKeys[pIdx];
                            var nudge = $"[MODERATOR]: ⏰ {AiDisplayName(doneAi)} has finished {roundName}. You're still writing -- please wrap up promptly. Other participants are waiting.";
                            if (ctx._cdpClients.ContainsKey(peerAi))
                                ctx.InjectToSingle(peerAi, nudge);
                            else
                                ctx.UpdateChunk("moderator", nudge); // fallback: broadcast
                            SlackPostToThread($"📩 *[DM->{peerAi}]* {nudge}", "🦉 Moderator");
                        }
                    }
                }
                else if (!hasResponse)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Error.WriteLine($"[DEBATE:{roundName}] {AiDisplayName(doneAi)} returned NULL -- no response");
                    Console.ResetColor();
                    if (pending.Count > 0)
                        SlackPostToThread($"!️ *{AiDisplayName(doneAi)}* returned no response for {roundName}. {pending.Count} AI(s) remaining.", "🦉 Moderator");
                }
            }
        }

        var roundResults = results.ToList();
        Console.Error.WriteLine($"[DEBATE:{roundName}] Complete: {roundResults.Count}/{prompts.Count} AIs responded");
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
            // Stream to Slack in real-time (don't suppress -- user wants live updates)

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
    /// Full 정반합 debate: R1 -> R2 -> R3 with convergence checking.
    /// Called after initial triad parallel run (R1 data collected from TriadSharedContext).
    /// </summary>
    static CancellationTokenSource? _debateCts;

    static void RunDebateLoop(string question, int timeoutSec, TriadSharedContext ctx)
    {
        // Debate rounds enforce dry-run: AI tool calls are read-only (inspect/read OK, click/type/edit blocked)
        _dryRunMode.Value = true;

        // Ctrl+C graceful abort
        _debateCts = new CancellationTokenSource();
        var prevHandler = default(ConsoleCancelEventHandler);
        prevHandler = (_, e) =>
        {
            e.Cancel = true;
            _debateCts.Cancel();
            Console.WriteLine("\n[DEBATE] ⛔ Ctrl+C -- 긴급 중단!");
            SlackPostToThread("⛔ *[Moderator]* 정반합 긴급 중단 (Ctrl+C by user)", "⚖️ Moderator");
        };
        Console.CancelKeyPress += prevHandler;

        try { RunDebateLoopCore(question, timeoutSec, ctx); }
        finally
        {
            Console.CancelKeyPress -= prevHandler;
            _debateCts.Dispose();
            _debateCts = null;
        }
    }

    static void RunDebateLoopCore(string question, int timeoutSec, TriadSharedContext ctx)
    {
        var ais = new[] { "gemini", "gpt", "claude" };

        Console.WriteLine("\n[DEBATE] === 사회자: R2/R3 시작 (R1 이미 완료) ===");
        SlackPostToThread(DebateMsg.R2R3Start, "🦉 Moderator");

        // Inject debate rules to all AIs (game announcement, not persona)
        var rules = DebateMsg.GetRulesForRound("R2"); // token-optimized: R2 rules only at start
        foreach (var ai in ais)
        {
            if (ctx._cdpClients.ContainsKey(ai))
                ctx.InjectToSingle(ai, rules);
        }
        SlackPostToThread(DebateMsg.SlackRulesInjected(rules), "🦉 Moderator");

        // -- R1 SKIP: already completed by AskTriadParallel/AgentCommand --
        // Build R1 results from shared context (chunks collected during R1 streaming)
        var r1Results = ais.Select(ai =>
        {
            var latestChunk = ctx.GetLatestChunk(ai) ?? "";
            var claims = TriadDebateLoop.ParseClaims(latestChunk);
            return new TriadDebateLoop.RoundResult(ai, latestChunk, claims);
        }).Where(r => r.Summary.Length > 50).ToList();

        Console.Error.WriteLine($"[DEBATE] R1 results from context: {r1Results.Count} AIs with content");

        if (r1Results.Count < 1)
        {
            Console.Error.WriteLine("[DEBATE] No R1 context found -- cannot proceed.");
            SlackPostToThread("❌ No R1 context available for R2/R3.", "🦉 Moderator");
            return;
        }

        if (_debateCts?.IsCancellationRequested == true) return;

        // -- Streaming cross-prompt: AIs react to each other in real-time --
        RunCrossPromptLoop(ais, Math.Min(timeoutSec, 90), ctx);
        if (_debateCts?.IsCancellationRequested == true) return;

        // Check convergence after cross-prompting
        var r1Convergence = TriadDebateLoop.CalculateConvergence(r1Results);
        Console.Error.WriteLine($"[DEBATE:R1] Convergence: {r1Convergence:F2} (threshold: 0.60)");
        if (r1Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r1Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Early convergence in R1! Skipping R2/R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R1 Early Convergence* (Jaccard={r1Convergence:F2}) -- debate skipped, consensus reached", "🦉 Moderator");
            PostDebateSummary(r1Results, 1);
            return;
        }

        // -- Devil's Advocate: random dissenter assignment --
        var activeAis = ais.Where(ai => ctx._cdpClients.ContainsKey(ai)).ToList();
        var dissenter = activeAis[Random.Shared.Next(activeAis.Count)];
        Console.Error.WriteLine($"[DEBATE] ⚔️ DISSENTER assigned: {AiDisplayName(dissenter)}");
        SlackPostToThread($"⚔️ *[DISSENTER]* {AiDisplayName(dissenter)} -- 3개 이상 P-항목 도전 필수! 첫 동의 = STANCE -2점", "🦉 Moderator");

        // -- R2: Critique -- ALL AIs participate --
        var r2Prompts = activeAis
            .ToDictionary(ai => ai, ai =>
            {
                var basePrompt = TriadDebateLoop.BuildR2Prompt(question, r1Results, ai);
                if (ai.Equals(dissenter, StringComparison.OrdinalIgnoreCase))
                    return basePrompt + "\n\n⚔️ YOU ARE [DISSENTER]: You MUST challenge ≥3 P-items before accepting ANY. First agreement costs 2 STANCE points. Be adversarial!";
                else
                    return basePrompt + $"\n\n!️ {AiDisplayName(dissenter)} is [DISSENTER] this round. Expect strong pushback from them.";
            });
        var r2Results = RunDebateRound("R2", r2Prompts, timeoutSec, ctx);
        if (_debateCts?.IsCancellationRequested == true) return;

        // Proceed with whatever we have (even 1 response)
        var bestResults = r2Results.Count > 0 ? r2Results : r1Results;

        var r2Convergence = TriadDebateLoop.CalculateConvergence(r2Results);
        Console.Error.WriteLine($"[DEBATE:R2] Convergence: {r2Convergence:F2}");
        if (r2Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r2Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Convergence in R2! Skipping R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R2 Convergence* (Jaccard={r2Convergence:F2}) -- R3 skipped", "🦉 Moderator");
            PostDebateSummary(r2Results, 2);
            return;
        }

        // -- R3: Consensus loop -- repeat until [미합의] is empty --
        SlackPostToThread("=== *R3: Consensus Loop* ===", "🦉 Moderator");
        var currentResults = bestResults;
        List<TriadDebateLoop.RoundResult> r3Results = new();
        const int maxR3Loops = 3;

        for (int r3i = 0; r3i < maxR3Loops; r3i++)
        {
            if (_debateCts?.IsCancellationRequested == true) break;
            Console.Error.WriteLine($"[DEBATE:R3-{r3i + 1}] Consensus attempt {r3i + 1}/{maxR3Loops}");
            SlackPostToThread($"🔄 *R3-{r3i + 1}* Consensus attempt", "🦉 Moderator");

            // Cascading consensus: AI-A first -> AI-B sees A's items -> AI-C sees A+B
            var priorConsensusItems = new List<string>(); // accumulates [합의] from earlier AIs
            r3Results = new();
            for (int aiIdx = 0; aiIdx < ais.Length; aiIdx++)
            {
                var ai = ais[aiIdx];
                var prompt = TriadDebateLoop.BuildR3Prompt(question, currentResults, priorConsensusItems, ai);
                Console.Error.WriteLine($"[DEBATE:R3-{r3i + 1}:{ai}] Cascading -- {priorConsensusItems.Count} atomic items");
                var cascadePreview = priorConsensusItems.Count > 0
                    ? $"\n{string.Join("\n", priorConsensusItems.Select((p, i) => $"  P{i + 1}. {p}"))}"
                    : " (첫 번째 AI -- 선행 항목 없음)";
                SlackPostToThread($"🔗 *{AiDisplayName(ai)}* -- {priorConsensusItems.Count}개 원자적 합의 항목 수용/거부 판정 중...{cascadePreview}", AiDisplayName(ai));
                var singlePrompt = new Dictionary<string, string> { [ai] = prompt };
                var singleResult = RunDebateRound($"R3-{r3i + 1}", singlePrompt, timeoutSec, ctx);
                r3Results.AddRange(singleResult);
                // Extract [합의] items as atomic propositions for next AI (with score if present)
                foreach (var r in singleResult)
                {
                    var haStart = r.Summary.IndexOf("[합의]");
                    if (haStart < 0) continue;
                    var haEnd = r.Summary.IndexOf("[미합의]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.IndexOf("[셀프힐링]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.IndexOf("[개인의견]", haStart);
                    if (haEnd < 0) haEnd = r.Summary.Length;
                    var haSection = r.Summary[(haStart + "[합의]".Length)..haEnd].Trim();
                    foreach (var line in haSection.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    {
                        // Strip leading numbering: "1.", "P1.", "•", "-"
                        var trimmed = line.Trim().TrimStart("0123456789.•-P ".ToCharArray());
                        if (trimmed.Length > 5) priorConsensusItems.Add($"[{AiDisplayName(ai)}] {trimmed}");
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
                    // Check item count (not word count) -- at least 1 meaningful line
                    var haLines = haSection.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Where(l => l.Trim().Length > 5).ToList();
                    if (haLines.Count > 0) hasConsensus = true;
                }

                // Check [미합의]
                var miStart = r.Summary.IndexOf("[미합의]");
                var miEnd = r.Summary.IndexOf("[개인의견]");
                if (miEnd < 0) miEnd = r.Summary.IndexOf("[MY_OPINION");
                if (miEnd < 0) miEnd = r.Summary.Length;
                if (miStart >= 0)
                {
                    var section = r.Summary[(miStart + "[미합의]".Length)..Math.Min(miEnd, r.Summary.Length)].Trim();
                    // "없음", "None", empty -> consensus. Otherwise 2+ words = real disagreement -> loop
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
                SlackPostToThread("!️ *[Moderator]* No [합의] content found! AIs must state consensus.", "🦉 Moderator");
                // Treat as unresolved
                if (unresolved.Count == 0) unresolved.Add("No consensus stated");
            }

            if (unresolved.Count == 0)
            {
                // Cross-check: extract [합의] items per AI and compare content
                var aiItems = new Dictionary<string, List<string>>(); // ai -> item texts
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
                        reportSb.AppendLine($"🔍 *[Moderator] 합의 항목 교차검증* -- 확인={confirmed.Count}, 미확인={unverified.Count}");
                        if (confirmed.Count > 0)
                        {
                            reportSb.AppendLine("\n*✅ 확인된 합의:*");
                            foreach (var c in confirmed)
                                reportSb.AppendLine($"  • {c}");
                        }
                        reportSb.AppendLine("\n*❓ 미확인 항목 (일부 AI만 언급):*");
                        foreach (var (ai, item, missing) in unverified)
                            reportSb.AppendLine($"  • [{ai}] \"{item}\" -- missing from: {string.Join(", ", missing)}");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Error.WriteLine($"[DEBATE:R3] {unverified.Count} unverified consensus items");
                        Console.ResetColor();
                        SlackPostToThread(reportSb.ToString(), "🦉 Moderator");

                        // -- Step 1: Broadcast full report to ALL AIs (공개 양식) --
                        var broadcastSb = new StringBuilder();
                        broadcastSb.AppendLine("[MODERATOR] 합의 항목 교차검증 결과입니다. 모든 AI에 공개합니다.");
                        if (confirmed.Count > 0)
                        {
                            broadcastSb.AppendLine($"\n[확인된 합의] ({confirmed.Count}건):");
                            for (int ci = 0; ci < confirmed.Count; ci++)
                                broadcastSb.AppendLine($"  {ci + 1}. {confirmed[ci]}");
                        }
                        broadcastSb.AppendLine($"\n[미확인 항목] ({unverified.Count}건 -- 일부 AI만 언급):");
                        for (int ui = 0; ui < unverified.Count; ui++)
                        {
                            var (srcAi2, item2, missing2) = unverified[ui];
                            broadcastSb.AppendLine($"  {ui + 1}. [{srcAi2}] \"{item2}\" -- missing from: {string.Join(", ", missing2)}");
                        }
                        broadcastSb.AppendLine("\n각 AI는 미확인 항목에 대해 동의/거부를 밝혀주세요.");
                        broadcastSb.AppendLine("전체 [합의]/[미합의]/[개인의견] 양식을 다시 출력해주세요.");

                        // Broadcast to all AIs + Slack
                        var broadcastText = broadcastSb.ToString();
                        foreach (var ai in ais)
                        {
                            if (ctx._cdpClients.ContainsKey(ai))
                                ctx.InjectToSingle(ai, broadcastText);
                        }
                        SlackPostToThread($"📢 *[Moderator->ALL]* Cross-check 방송:\n{broadcastText}", "🦉 Moderator");

                        // -- Step 2: DM objection prompts to specific AIs (이의제기 타겟) --
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
                            dmSb.AppendLine($"[MODERATOR DM -> {targetAi} only] 아래 항목들이 당신의 [합의]에 빠져있습니다:");
                            foreach (var mi in missingItems) // no truncation
                                dmSb.AppendLine($"  ⚡ {mi}");
                            dmSb.AppendLine("각 항목에 대해: 동의하면 [합의]에 추가, 반대하면 [미합의]에 이유와 함께 넣어주세요.");

                            if (ctx._cdpClients.ContainsKey(targetAi))
                                ctx.InjectToSingle(targetAi, dmSb.ToString());

                            // Slack에도 DM 전체 내용 공개 (no truncation)
                            SlackPostToThread($"📩 *[DM->{targetAi}]* {dmSb}", "🦉 Moderator");
                        }

                        unresolved.Add($"Cross-check: {unverified.Count} unverified consensus items");
                    }
                }
            }

            // Count total consensus items across all AIs
            int totalConsensusItems = 0;
            foreach (var r in r3Results)
            {
                var ha = r.Summary.IndexOf("[합의]");
                var haEnd = r.Summary.IndexOf("[미합의]", Math.Max(0, ha));
                if (haEnd < 0) haEnd = r.Summary.IndexOf("[셀프힐링]", Math.Max(0, ha));
                if (haEnd < 0) haEnd = r.Summary.Length;
                if (ha >= 0)
                    totalConsensusItems += r.Summary[ha..haEnd].Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Count(l => l.Trim().Length > 5);
            }

            // Target consensus check (--debate N)
            var target = _debateTargetConsensus;
            if (target > 0 && totalConsensusItems < target)
            {
                SlackPostToThread($"🎯 *합의 {totalConsensusItems}/{target}개* -- 목표 미달! 더 합의하세요!", "🦉 Moderator");
                foreach (var ai in ais)
                    if (ctx._cdpClients.ContainsKey(ai))
                        ctx.InjectToSingle(ai, $"[MODERATOR] 🎯 합의 목표 {target}개 중 {totalConsensusItems}개 달성. {target - totalConsensusItems}개 더 필요! 미합의 항목을 설득하거나 새 합의를 추가하세요.");
                // Don't break -- continue R3 loop
            }
            else if (unresolved.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Error.WriteLine($"[DEBATE:R3] Full consensus reached! ({totalConsensusItems} items, attempt {r3i + 1})");
                Console.ResetColor();
                SlackPostToThread($"✅ *Full consensus!* ({totalConsensusItems}개, R3-{r3i + 1})", "🦉 Moderator");
                break;
            }

            Console.Error.WriteLine($"[DEBATE:R3] {unresolved.Count} unresolved items -> tiered persuasion");

            // -- Tiered Disagreement Classification --
            // T1(표현): 2/3 agree -> rewrite to unify
            // T2(판단): 1/3 agree -> demand evidence
            // T3(근본): 0/3 agree -> [합의불가] graceful exit
            var t1Items = new List<string>(); // expression difference
            var t2Items = new List<string>(); // judgment difference
            var t3Items = new List<string>(); // fundamental gap
            foreach (var item in unresolved)
            {
                // Count how many AIs have this item in [합의] (rough keyword match)
                var itemKey = TriadDebateLoop.Tokenize(item);
                int agreeCount = r3Results.Count(r =>
                {
                    var ha = r.Summary.IndexOf("[합의]");
                    if (ha < 0) return false;
                    var haEnd = r.Summary.IndexOf("[미합의]", ha);
                    if (haEnd < 0) haEnd = r.Summary.Length;
                    var haSection = r.Summary[ha..haEnd];
                    return itemKey.Any(k => haSection.Contains(k, StringComparison.OrdinalIgnoreCase));
                });
                if (agreeCount >= 2) t1Items.Add(item);
                else if (agreeCount == 1) t2Items.Add(item);
                else t3Items.Add(item);
            }

            SlackPostToThread($"📊 *미합의 분류:* T1(표현)={t1Items.Count} T2(판단)={t2Items.Count} T3(근본)={t3Items.Count}", "🦉 Moderator");

            var persuasionSb = new StringBuilder();
            persuasionSb.AppendLine("[MODERATOR] 🎯 미합의 항목 -- 등급별 처리:");
            if (t1Items.Count > 0)
            {
                persuasionSb.AppendLine("\n📝 *T1(표현 차이)* -- 사회자가 통합 표현 제안:");
                foreach (var item in t1Items)
                    persuasionSb.AppendLine($"  -> {item}");
            }
            if (t2Items.Count > 0)
            {
                persuasionSb.AppendLine("\n🔍 *T2(판단 차이)* -- 새 근거/데이터 제시 필수:");
                foreach (var item in t2Items)
                    persuasionSb.AppendLine($"  🔴 {item}");
            }
            if (t3Items.Count > 0)
            {
                persuasionSb.AppendLine("\n🚫 *T3(근본 차이)* -- [합의불가] 선언, 양측 입장 보존:");
                foreach (var item in t3Items)
                    persuasionSb.AppendLine($"  ⛔ {item}");
            }
            persuasionSb.AppendLine("\nT1: 사회자 표현 수용/수정. T2: 새 근거 제시 OR 수용. T3: 합의불가 인정.");

            var persuasionText = persuasionSb.ToString();
            foreach (var ai in ais)
            {
                if (ctx._cdpClients.ContainsKey(ai))
                    ctx.InjectToSingle(ai, persuasionText);
            }
            SlackPostToThread($"🎯 *[Moderator->ALL] 미합의 설득 유도:*\n{persuasionText}", "🦉 Moderator");

            currentResults = r3Results;
        }

        var finalResults = r3Results.Count > 0 ? r3Results : bestResults;
        var r3Convergence = TriadDebateLoop.CalculateConvergence(finalResults);
        Console.Error.WriteLine($"[DEBATE:R3] Final convergence: {r3Convergence:F2}");

        // -- Cross-verify: extract consensus from intersection of all AI claims --
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
        sb.AppendLine("=== *정반합 최종 결과* ===\n");
        sb.AppendLine($"📊 Convergence: {r3Convergence:F2}\n");

        if (consensusClaims.Count > 0)
        {
            sb.AppendLine("*[합의]*");
            foreach (var c in consensusClaims.Distinct())
                sb.AppendLine($"  • {c}");
        }
        if (dissentClaims.Count > 0)
        {
            sb.AppendLine("\n*[미합의]*");
            foreach (var (ai, c) in dissentClaims)
                sb.AppendLine($"  • [{ai}] {c}");
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
                sb.AppendLine($"  🗣️ *{r.Ai}*: {opinion}");
            }
            else if (!string.IsNullOrEmpty(opinion))
            {
                sb.AppendLine($"  🗣️ *{r.Ai}*: {opinion} !️(too brief)");
                SlackPostToThread($"!️ *[Moderator->{r.Ai}]* Personal opinion too brief ({opinion.Length} chars). Express yourself fully!", "🦉 Moderator");
            }
            else
            {
                sb.AppendLine($"  🗣️ *{r.Ai}*: {r.Summary}");
            }
        }

        SlackPostToThread(sb.ToString(), "🦉 Moderator");
        Console.WriteLine("[DEBATE] === 정반합 토론 완료 ===");
        SlackPostToThread("=== *정반합 토론 완료* ===", "🦉 Moderator");
    }

    /// <summary>
    /// Streaming cross-prompt loop: inject peer chunks into each AI after their R1 response.
    /// Called from the main triad flow -- runs concurrently with R1 polling.
    /// Each AI that finishes early picks up peer chunks and auto-reacts.
    /// Max 3 cross-prompt rounds per AI to prevent infinite loop.
    /// </summary>
    static void RunCrossPromptLoop(string[] ais, int timeoutSec, TriadSharedContext ctx)
    {
        const int maxCrossRounds = 3;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Console.WriteLine("[CROSS] === Real-time cross-prompting started ===");
        SlackPostToThread("🔄 *Real-time cross-prompting started!", "🦉 Moderator");

        var crossTasks = ais.Select(ai => Task.Run(async () =>
        {
            var peerSeen = new ConcurrentDictionary<string, int>();

            for (int round = 0; round < maxCrossRounds; round++)
            {
                if (sw.Elapsed.TotalSeconds > timeoutSec || _debateCts?.IsCancellationRequested == true) break;

                // Wait for peer chunks to accumulate
                await Task.Delay(5000);

                var peers = ctx.GetPeerChunks(ai, peerSeen);
                if (peers.Count == 0) continue;

                // Build cross-prompt message
                var crossMsg = new StringBuilder();
                crossMsg.AppendLine($"[CROSS-PROMPT round {round + 1}] Other AIs are arguing:");
                foreach (var (fromAi, text) in peers)
                    crossMsg.AppendLine($"  [{fromAi}]: {text}");
                crossMsg.AppendLine("React briefly -- agree, disagree, or refine. Use [CLAIM] format.");

                Console.Error.WriteLine($"[CROSS:{ai}] Round {round + 1}: {peers.Count} peer chunk(s) -> injecting");
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
                    Console.Error.WriteLine($"[CROSS:{ai}] Convergence: {conv:F2} ({allResults.Count} AIs with 2+ claims)");
                    SlackPostToThread($"📊 *[Convergence]* {conv:F2} ({allResults.Count} AIs)", "🦉 Moderator");
                    if (conv >= 0.6 && conv < 1.0 && !TriadDebateLoop.HasContradictions(allResults)) // 1.0 = suspicious
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Error.WriteLine($"[CROSS] Convergence reached! ({conv:F2})");
                        Console.ResetColor();
                        SlackPostToThread($"✅ *Convergence reached!* Jaccard={conv:F2} -- debate complete", "🦉 Moderator");
                        return;
                    }
                }
            }
        })).ToArray();

        Task.WaitAll(crossTasks);
        Console.WriteLine("[CROSS] === Cross-prompting complete ===");
    }

    /// <summary>
    /// After AI response completes, check if cross-prompt text was pre-typed in editor.
    /// If so, send it immediately (Enter key). Returns true if cross-prompt was sent.
    /// </summary>
    internal static async Task<bool> SendPendingCrossPromptAsync(WKAppBot.WebBot.CdpClient cdp, string ai, string editorSel,
        TriadSharedContext? ctx = null)
    {
        // Drain tool discoveries first (큐 기반 -- idle 시점에만 주입)
        if (ctx != null)
        {
            var discoveries = ctx.DrainDiscoveries(ai);
            if (discoveries.Count > 0)
            {
                var combined = string.Join("\n", discoveries);
                try
                {
                    await cdp.InsertContentEditableAsync(editorSel, combined);
                    await cdp.SendPromptAsync(editorSel);
                    Console.Error.WriteLine($"[CROSS:{ai}] Injected {discoveries.Count} tool discoveries ({combined.Length} chars)");
                    SlackPostToThread($"🔧 *[->{ai}]* {discoveries.Count}개 도구 발견 주입", "🦉 Moderator");
                    return true;
                }
                catch (Exception ex) { Console.Error.WriteLine($"[CROSS:{ai}] Discovery inject failed: {ex.Message}"); }
            }
        }

        try
        {
            var editorLen = await cdp.GetTextLengthAsync(editorSel);
            if (editorLen > 10) // cross-prompt text was pre-typed
            {
                Console.Error.WriteLine($"[CROSS:{ai}] Found pre-typed cross-prompt ({editorLen} chars) -> sending");
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

            // Verify CDP is still connected -- try reconnect before fallback
            if (!cdp.IsConnected)
            {
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: CDP disconnected -- attempting reconnect...");
                try
                {
                    // Reconnect using same tab ID (the tab is still open, just WebSocket dropped)
                    await cdp.ReconnectAsync();
                    if (cdp.IsConnected)
                        Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: CDP reconnected!");
                    else
                    {
                        Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: reconnect failed -- falling back to AskAndCapture");
                        return AskAndCapture(ai, prompt, timeoutSec);
                    }
                }
                catch (Exception rex)
                {
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: reconnect error: {rex.Message} -- falling back");
                    return AskAndCapture(ai, prompt, timeoutSec);
                }
            }

            // Capture baseline: full page text length (DOM-agnostic snapshot)
            // Claude triad fix: don't rely on turn-count selectors -- they break across AI sites
            int baselineCount = 0;
            int baselinePageLen = 0;
            var baseline = "";
            try
            {
                var countStr = await cdp.EvalAsync(
                    "(()=>{var r=document.querySelectorAll('model-response');if(!r.length)r=document.querySelectorAll('[role=\"article\"]');return r.length})()");
                int.TryParse(countStr, out baselineCount);
                baseline = await cdp.GetLastResponseTextAsync() ?? "";
                // DOM-agnostic: snapshot full page text length
                var pageLenStr = await cdp.EvalAsync("document.body?.innerText?.length??0") ?? "0";
                int.TryParse(pageLenStr, out baselinePageLen);
            }
            catch { }
            Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: baseline={baseline.Length}chars, pageLen={baselinePageLen}, turns={baselineCount}, connected={cdp.IsConnected}");

            // Verify target page URL matches expected AI site
            try
            {
                var pageUrl = await cdp.EvalAsync("location.href") ?? "(unknown)";
                var expectedHost = ai switch { "gemini" => "gemini.google", "gpt" => "chatgpt.com", "claude" => "claude.ai", _ => "" };
                if (!string.IsNullOrEmpty(expectedHost) && !pageUrl.Contains(expectedHost, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: ❌ WRONG TARGET! Expected {expectedHost}, got {pageUrl}");
                    Console.ResetColor();
                    SlackPostToThread($"❌ *[{ai}] Wrong target!* Expected {expectedHost}, got: {pageUrl}", "🦉 Moderator");
                    return AskAndCapture(ai, prompt, timeoutSec);
                }
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: target verified -> {pageUrl[..Math.Min(60, pageUrl.Length)]}");
            }
            catch (Exception urlEx) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: URL check failed: {urlEx.Message}"); }

            // Wait for previous response to finish streaming before injecting new prompt
            try
            {
                var provider = ai == "gpt" ? "chatgpt" : ai;
                for (int waitGen = 0; waitGen < 15; waitGen++) // max 30s
                {
                    if (!await cdp.IsStreamingAsync(provider)) break;
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: waiting for previous response to finish ({waitGen * 2}s)...");
                    await Task.Delay(2000);
                }
            }
            catch { }

            // Dump editor + response node paths BEFORE inject
            try
            {
                var editorPath = await cdp.EvalAsync($@"(()=>{{
                    var el=document.querySelector('{editorSel}');
                    if(!el) return '(editor not found: {editorSel})';
                    var path=[];
                    while(el){{path.unshift(el.tagName+(el.id?'#'+el.id:'')+(el.className?' .'+el.className.split(' ').slice(0,2).join('.'):'')); el=el.parentElement;}}
                    return path.join(' > ');
                }})()") ?? "(eval fail)";
                // Use CSS selectors without quotes conflict -- backtick template in JS
                var respSel = ai switch { "gemini" => "model-response", "gpt" => "[data-message-author-role=assistant]", "claude" => "[role=article]", _ => "model-response" };
                var respPath = await cdp.EvalAsync($@"(()=>{{
                    var els=document.querySelectorAll('{respSel}');
                    if(!els.length) return '(no response nodes: {respSel})';
                    var last=els[els.length-1];
                    var path=[];
                    while(last){{path.unshift(last.tagName+(last.id?'#'+last.id:'')+(last.className?' .'+last.className.split(' ').slice(0,2).join('.'):'')); last=last.parentElement;}}
                    return els.length+'nodes, last='+path.join(' > ');
                }})()") ?? "(eval fail)";
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: editor-path: {editorPath}");
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: response-path: {respPath}");
            }
            catch (Exception pathEx) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: path dump failed: {pathEx.Message}"); }

            // Inject prompt text
            try
            {
                await cdp.InsertContentEditableAsync(editorSel, prompt);
                await Task.Delay(1000); // wait for full text insertion
                // Send (Enter)
                await cdp.SendPromptAsync(editorSel);
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: sent moderator prompt ({prompt.Length} chars)");
                // Post-inject verification: editor content + send button state
                try
                {
                    var editorContent = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.innerText?.substring(0,100)??'(empty)'") ?? "(null)";
                    var editorLen = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.innerText?.length??0") ?? "0";
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: post-inject editor={editorLen}chars [{editorContent}]");
                }
                catch { }
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
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: sent via InjectToSingle fallback ({prompt.Length} chars)");
                }
                catch (Exception fb) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: fallback also failed: {fb.Message}"); return null; }
            }

            // Poll response: DOM-agnostic page text length comparison (삼두 합의 방식)
            // Initial wait 5s (AI needs time to start responding after Enter)
            await Task.Delay(5000);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            string lastText = "";
            int stable = 0;
            var lastChunkTime = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 90))
            {
                await Task.Delay(2000);

                // Primary: page text length growth detection (DOM-agnostic)
                int curPageLen = 0;
                string text = "";
                try
                {
                    var pageLenStr = await cdp.EvalAsync("document.body?.innerText?.length??0") ?? "0";
                    int.TryParse(pageLenStr, out curPageLen);
                    // Also try selector-based (best-effort)
                    text = await cdp.GetLastResponseTextAsync(baselineCount) ?? "";
                    // If selector failed but page grew -> extract last chunk
                    if (text.Length < 20 && curPageLen > baselinePageLen + 50)
                    {
                        text = await cdp.EvalAsync(
                            $"document.body?.innerText?.substring({baselinePageLen})?.trim()?.substring(0,3000)??''") ?? "";
                        if (text.Length > 20 && stable == 0) // log only once (first detection)
                            Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: selector miss -> pageLen fallback +{curPageLen - baselinePageLen}chars, extracted {text.Length}chars");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: poll error: {ex.Message}");
                    continue;
                }

                // Stall detection: 15s with no growth -> log (no nudge -- nudge poisons context)
                if (lastChunkTime.Elapsed.TotalSeconds >= 15 && text.Length < 20)
                {
                    Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: stall {lastChunkTime.Elapsed.TotalSeconds:F0}s, pageLen={curPageLen}(was {baselinePageLen}), text={text.Length}chars");
                    SlackPostToThread($"⏰ *[{ai}]* {lastChunkTime.Elapsed.TotalSeconds:F0}초 무응답 -- pageLen={curPageLen}(was {baselinePageLen})", "🦉 Moderator");
                    lastChunkTime.Restart();
                }

                if (text.Length < 20) continue;

                // TOOL_CALL detection: if AI emitted [APPBOT_TOOL_CALL_BEGIN]...[END], execute and inject result
                if (text.Contains("[APPBOT_TOOL_CALL_BEGIN]") && text.Contains("[APPBOT_TOOL_CALL_END]"))
                {
                    var tcStart = text.IndexOf("[APPBOT_TOOL_CALL_BEGIN]") + "[APPBOT_TOOL_CALL_BEGIN]".Length;
                    var tcEnd = text.IndexOf("[APPBOT_TOOL_CALL_END]", tcStart);
                    if (tcEnd > tcStart)
                    {
                        var toolCmd = text[tcStart..tcEnd].Trim();
                        Console.Error.WriteLine($"[DEBATE:TOOL] {ai}: detected TOOL_CALL: {toolCmd[..Math.Min(100, toolCmd.Length)]}");
                        SlackPostToThread($"🔧 *[{AiDisplayName(ai)} TOOL_CALL]* `{toolCmd[..Math.Min(80, toolCmd.Length)]}`", AiDisplayName(ai));
                        // Parse: CLI string "wkappbot cmd args" OR JSON {"argv":["cmd","args"]}
                        string[] cmdParts;
                        if (toolCmd.TrimStart().StartsWith("{"))
                        {
                            try
                            {
                                var json = System.Text.Json.JsonDocument.Parse(toolCmd);
                                var argv = json.RootElement.GetProperty("argv");
                                cmdParts = new string[argv.GetArrayLength()];
                                for (int ci = 0; ci < cmdParts.Length; ci++)
                                    cmdParts[ci] = argv[ci].GetString() ?? "";
                            }
                            catch { cmdParts = toolCmd.Split(' ', StringSplitOptions.RemoveEmptyEntries); }
                        }
                        else
                            cmdParts = toolCmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        // Strip leading "wkappbot" if present
                        if (cmdParts.Length >= 1 && cmdParts[0].Equals("wkappbot", StringComparison.OrdinalIgnoreCase))
                            cmdParts = cmdParts[1..];
                        if (cmdParts.Length >= 1)
                        {
                            var (output, exitCode) = RunCliCaptureWithCode(cmdParts[0], cmdParts.Length > 1 ? cmdParts[1..] : Array.Empty<string>(), null);
                            var resultSnippet = output.Length > 500 ? output[..500] + "..." : output;
                            var toolResult = $"TOOL_RESULT [exit={exitCode}]\n{resultSnippet}";
                            Console.Error.WriteLine($"[DEBATE:TOOL] {ai}: result={output.Length}chars exit={exitCode}");
                            SlackPostToThread($"🔧 *[{ai} TOOL_RESULT]* exit={exitCode}, {output.Length}chars", "🦉 Moderator");
                            // Inject result back to AI
                            try { await cdp.InsertContentEditableAsync(editorSel, toolResult); await cdp.SendPromptAsync(editorSel); }
                            catch { }
                            // Share with peers
                            ctx.PushDiscovery(ai, $"{toolCmd} -> {resultSnippet}");
                            baselinePageLen = curPageLen + toolResult.Length; // reset baseline
                            lastChunkTime.Restart();
                            continue; // poll for AI's next response after tool result
                        }
                    }
                }

                if (text != lastText) { stable = 0; lastText = text; lastChunkTime.Restart(); }
                else { stable++; if (stable >= 3) return text; }
                try { var provider = ai == "gpt" ? "chatgpt" : ai;
                    if (!await cdp.IsStreamingAsync(provider)) { await Task.Delay(1000); return lastText; } } catch { }
            }
            // Poll timeout -- dump debug info for diagnosis
            if (string.IsNullOrEmpty(lastText))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: ❌ POLL TIMEOUT -- no response detected after {sw.Elapsed.TotalSeconds:F0}s");
                Console.ResetColor();

                // Debug dump: DOM state + editor state + screenshot
                var dumpSb = new StringBuilder();
                dumpSb.AppendLine($"baseline: {baseline.Length}chars, turns={baselineCount}");
                try
                {
                    var curCount = await cdp.EvalAsync(
                        "(()=>{var r=document.querySelectorAll('model-response');if(!r.length)r=document.querySelectorAll('[role=\"article\"]');return r.length})()");
                    var curText = await cdp.GetLastResponseTextAsync() ?? "(null)";
                    var editorText = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.innerText?.substring(0,200)??'(no editor)'") ?? "(eval fail)";
                    var pageTitle = await cdp.EvalAsync("document.title") ?? "(no title)";
                    var isStreaming = false;
                    try { isStreaming = await cdp.IsStreamingAsync(ai == "gpt" ? "chatgpt" : ai); } catch { }
                    dumpSb.AppendLine($"current: turns={curCount}(was {baselineCount}), streaming={isStreaming}");
                    dumpSb.AppendLine($"lastResponse: {curText.Length}chars [{curText[..Math.Min(120, curText.Length)]}]");
                    dumpSb.AppendLine($"editor: [{editorText}]");
                    dumpSb.AppendLine($"title: {pageTitle}");
                    dumpSb.AppendLine($"connected={cdp.IsConnected}, wsUrl={cdp.WebSocketUrl?[..Math.Min(60, cdp.WebSocketUrl?.Length ?? 0)]}");
                    // Full a11y node path: Chrome hwnd + tab info
                    var chromeHwnd = cdp.GetChromeWindowHandle();
                    dumpSb.AppendLine($"chromeHwnd=0x{chromeHwnd:X}, ChromeWindowHandle=0x{cdp.ChromeWindowHandle:X}");
                    var tabUrl = await cdp.EvalAsync("location.href") ?? "(unknown)";
                    var tabTitle = await cdp.EvalAsync("document.title") ?? "(unknown)";
                    dumpSb.AppendLine($"tab: {tabTitle}");
                    dumpSb.AppendLine($"url: {tabUrl}");
                    // Response selectors check
                    var modelResp = await cdp.EvalAsync("document.querySelectorAll('model-response').length") ?? "0";
                    var roleArticle = await cdp.EvalAsync("document.querySelectorAll('[role=\"article\"]').length") ?? "0";
                    var turnGroups = await cdp.EvalAsync("document.querySelectorAll('[data-testid*=\"turn\"]').length") ?? "0";
                    dumpSb.AppendLine($"DOM: model-response={modelResp}, [role=article]={roleArticle}, turn-groups={turnGroups}");
                }
                catch (Exception dex) { dumpSb.AppendLine($"dump error: {dex.Message}"); }

                // Screenshot
                string? screenshotPath = null;
                try
                {
                    var ssDir = Path.Combine(Path.GetTempPath(), "wkappbot_debate_debug");
                    Directory.CreateDirectory(ssDir);
                    screenshotPath = Path.Combine(ssDir, $"{ai}_{DateTime.Now:HHmmss}_poll_fail.png");
                    var ssData = await cdp.EvalAsync("(async()=>{var r=await new Promise(ok=>chrome.debugger?ok('n/a'):ok('n/a'));return r})()");
                    // CDP screenshot
                    var ssResult = await cdp.SendAsync("Page.captureScreenshot", new System.Text.Json.Nodes.JsonObject { ["format"] = "png" });
                    var b64 = ssResult?["data"]?.GetValue<string>();
                    if (b64 != null) File.WriteAllBytes(screenshotPath, Convert.FromBase64String(b64));
                    dumpSb.AppendLine($"screenshot: {screenshotPath}");
                }
                catch (Exception ssEx) { dumpSb.AppendLine($"screenshot failed: {ssEx.Message}"); screenshotPath = null; }

                var dumpText = dumpSb.ToString();
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: DEBUG DUMP:\n{dumpText}");
                // Upload dump as text file attachment (avoids msg_too_long)
                SlackPostToThread($"❌ *[{ai}] Poll Timeout* -- 디버그 덤프 첨부 v", "🦉 Moderator");
                try
                {
                    var dumpDir = Path.Combine(Path.GetTempPath(), "wkappbot_debate_debug");
                    Directory.CreateDirectory(dumpDir);
                    var dumpFile = Path.Combine(dumpDir, $"{ai}_{DateTime.Now:HHmmss}_poll_dump.txt");
                    File.WriteAllText(dumpFile, dumpText);
                    var threadTs = _slackSessionThreadTs.Value;
                    if (threadTs != null)
                    {
                        var config = LoadSlackConfig();
                        var botToken = config?["bot_token"]?.GetValue<string>();
                        var channel = config?["channel"]?.GetValue<string>();
                        if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
                            SlackUploadFileAsync(botToken, channel, dumpFile, $"{ai} poll timeout dump", threadTs).GetAwaiter().GetResult();
                    }
                }
                catch { }
                // Upload screenshot to Slack if available
                if (screenshotPath != null && File.Exists(screenshotPath))
                {
                    try
                    {
                        var threadTs = _slackSessionThreadTs.Value;
                        if (threadTs != null)
                        {
                            var config = LoadSlackConfig();
                            var botToken = config?["bot_token"]?.GetValue<string>();
                            var channel = config?["channel"]?.GetValue<string>();
                            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
                                SlackUploadFileAsync(botToken, channel, screenshotPath, $"{ai} poll timeout", threadTs).GetAwaiter().GetResult();
                        }
                    }
                    catch { }
                }

                // RETRY: fall back to AskAndCapture (new tab approach)
                Console.Error.WriteLine($"[DEBATE:INJECT] {ai}: retrying via AskAndCapture fallback...");
                SlackPostToThread($"🔄 *[Moderator->{ai}]* CDP poll 실패 -> AskAndCapture 폴백 재시도", "🦉 Moderator");
                return AskAndCapture(ai, prompt, timeoutSec);
            }
            return lastText;
        }
        catch (Exception ex) { Console.Error.WriteLine($"[DEBATE:INJECT] {ai} error: {ex.Message}"); }
        return null;
    }

    // -- Off-topic detection: moderator redirect --

    /// <summary>
    /// Detect if an AI response is off-topic (e.g., tool exploration, CLI jargon, wrong language).
    /// Returns true if the moderator should redirect the debater to the original question.
    /// </summary>
    static bool IsOffTopicResponse(string? response)
    {
        if (string.IsNullOrWhiteSpace(response) || response.Length < 50) return false;

        // CLI tool exploration patterns: --help, wkappbot commands, DEBATE_JSON
        var offTopicPatterns = new[]
        {
            "DEBATE_JSON", "DEBATE_MODE", "--help", "--debug", "--verbose",
            "wkappbot inspect", "wkappbot a11y", "wkappbot windows",
            "[APPBOT_TOOL_CALL", "TOOL_CALL_BEGIN", "Usage: wkappbot",
            "Available commands:", "Available options:", "Usage: "
        };
        var lower = response;
        int matches = offTopicPatterns.Count(p => lower.Contains(p, StringComparison.OrdinalIgnoreCase));
        if (matches >= 2) return true; // 2+ CLI patterns = definitely off-topic

        // Single strong CLI pattern = also off-topic
        if (matches >= 1 && response.Contains("--", StringComparison.Ordinal) &&
            response.Split("--", StringSplitOptions.None).Length > 3)
            return true;

        return false;
    }

    // -- NLI: AI-as-judge semantic comparison --
    enum NliResult { Entail, Neutral, Contradict }

    static async Task<NliResult> SemanticCompareAsync(TriadSharedContext ctx, string claim1, string claim2)
    {
        // Pick fastest available AI (prefer gemini for speed)
        var ai = ctx._cdpClients.ContainsKey("gemini") ? "gemini"
               : ctx._cdpClients.Keys.FirstOrDefault() ?? "gemini";
        var prompt = $"Are these two claims semantically equivalent?\nP1: {claim1}\nP2: {claim2}\nReply ONLY: ENTAIL / NEUTRAL / CONTRADICT";
        try
        {
            var resp = await InjectAndPollAsync(ctx, ai, prompt, 15);
            var r = resp?.Trim().ToUpper();
            if (r?.Contains("ENTAIL") == true) return NliResult.Entail;
            if (r?.Contains("CONTRADICT") == true) return NliResult.Contradict;
        }
        catch (Exception ex) { Console.Error.WriteLine($"[NLI] SemanticCompare error: {ex.Message}"); }
        return NliResult.Neutral;
    }

    /// <summary>Post debate summary to Slack thread.</summary>
    static void PostDebateSummary(List<TriadDebateLoop.RoundResult> results, int roundsCompleted)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== *Debate Results* (R{roundsCompleted} complete) ===");

        foreach (var r in results)
        {
            sb.AppendLine($"\n*{r.Ai}* ({r.Claims.Count} claims):");
            foreach (var c in r.Claims.OrderByDescending(c => c.Confidence).Take(3))
                sb.AppendLine($"  • [{c.Confidence:F2}] {c.Text[..Math.Min(80, c.Text.Length)]}");
        }

        var convergence = TriadDebateLoop.CalculateConvergence(results);
        var hasContradictions = TriadDebateLoop.HasContradictions(results);
        sb.AppendLine($"\n📊 Convergence: {convergence:F2} | Contradictions: {(hasContradictions ? "! YES" : "✅ No")}");

        SlackPostToThread(sb.ToString(), "🦉 Moderator");
    }
}
