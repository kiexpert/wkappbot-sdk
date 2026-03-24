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
        SlackPostToThread($"🔄 *[{roundName}]* 라운드 시작", "앱봇아이");

        var tasks = prompts.Select(kv => Task.Run(() =>
        {
            var ai = kv.Key;
            var prompt = kv.Value;
            var linePrefix = $"[{ai}:{roundName}] ";

            try
            {
                using var pfx = ApplyOutputPrefix(linePrefix);

                // Reuse existing ask infrastructure — one-shot ask (no loop mode)
                var response = AskAndCapture(ai, prompt, timeoutSec);

                if (response != null)
                {
                    var claims = TriadDebateLoop.ParseClaims(response);
                    var summary = TriadDebateLoop.CompressSummary(response);
                    var result = new TriadDebateLoop.RoundResult(ai, summary, claims);

                    ctx.LogStep(ai, $"[{roundName}] {claims.Count} claims: {string.Join("; ", claims.Select(c => $"{c.Text[..Math.Min(50, c.Text.Length)]}({c.Confidence:F1})"))}");
                    results.Add(result);

                    Console.WriteLine($"[DEBATE:{roundName}:{ai}] {claims.Count} claims extracted");
                    SlackPostToThread($"📋 *[{roundName}:{ai}]* {claims.Count} claims (avg conf={claims.Average(c => c.Confidence):F2})", ai);
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

        Task.WaitAll(tasks);

        var roundResults = results.ToList();
        Console.WriteLine($"[DEBATE:{roundName}] Complete: {roundResults.Count}/{prompts.Count} AIs responded");
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
            askArgs.Add("--new-session");

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

            // Fallback: return last substantial block
            var lines = output.Split('\n').Where(l => l.Length > 20 && !l.StartsWith("[")).ToArray();
            return lines.Length > 0 ? string.Join("\n", lines.TakeLast(20)) : null;
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
        var ais = new[] { "gemini", "gpt", "claude" };

        Console.WriteLine("\n[DEBATE] ═══ 정반합 토론 루프 시작 ═══");
        SlackPostToThread("═══ *정반합 토론 시작* ═══\nR1(다양성) → R2(비판) → R3(합성)", "앱봇아이");

        // ── R1: Diversity (independent answers with structured claims) ──
        var r1Prompts = ais.ToDictionary(ai => ai, _ => TriadDebateLoop.BuildR1Prompt(question));
        var r1Results = RunDebateRound("R1", r1Prompts, timeoutSec, ctx);

        if (r1Results.Count < 2)
        {
            Console.Error.WriteLine("[DEBATE] R1: insufficient responses — need at least 2 AIs. Aborting.");
            SlackPostToThread("❌ R1 실패: 응답 2개 미만. 토론 중단.", "앱봇아이");
            return;
        }

        // Check early convergence (unlikely in R1 but possible for simple questions)
        var r1Convergence = TriadDebateLoop.CalculateConvergence(r1Results);
        Console.WriteLine($"[DEBATE:R1] Convergence: {r1Convergence:F2} (threshold: 0.60)");
        if (r1Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r1Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Early convergence in R1! Skipping R2/R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R1 조기수렴* (Jaccard={r1Convergence:F2}) — 토론 생략, 합의 완료", "앱봇아이");
            PostDebateSummary(r1Results, 1);
            return;
        }

        // ── R2: Critique (adversarial, anonymous labels) ──
        var r2Prompts = ais.Where(ai => r1Results.Any(r => r.Ai.Equals(ai, StringComparison.OrdinalIgnoreCase)))
            .ToDictionary(ai => ai, ai => TriadDebateLoop.BuildR2Prompt(question, r1Results, ai));
        var r2Results = RunDebateRound("R2", r2Prompts, timeoutSec, ctx);

        if (r2Results.Count < 2)
        {
            Console.Error.WriteLine("[DEBATE] R2: insufficient responses. Using R1 results.");
            PostDebateSummary(r1Results, 2);
            return;
        }

        var r2Convergence = TriadDebateLoop.CalculateConvergence(r2Results);
        Console.WriteLine($"[DEBATE:R2] Convergence: {r2Convergence:F2}");
        if (r2Convergence >= 0.6 && !TriadDebateLoop.HasContradictions(r2Results))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[DEBATE] Convergence in R2! Skipping R3.");
            Console.ResetColor();
            SlackPostToThread($"✅ *R2 수렴* (Jaccard={r2Convergence:F2}) — R3 생략", "앱봇아이");
            PostDebateSummary(r2Results, 2);
            return;
        }

        // ── R3: Synthesis (unified answer) ──
        var r3Prompts = ais.Where(ai => r2Results.Any(r => r.Ai.Equals(ai, StringComparison.OrdinalIgnoreCase)))
            .ToDictionary(ai => ai, _ => TriadDebateLoop.BuildR3Prompt(question, r2Results));
        var r3Results = RunDebateRound("R3", r3Prompts, timeoutSec, ctx);

        var r3Convergence = TriadDebateLoop.CalculateConvergence(r3Results.Count > 0 ? r3Results : r2Results);
        Console.WriteLine($"[DEBATE:R3] Final convergence: {r3Convergence:F2}");
        PostDebateSummary(r3Results.Count > 0 ? r3Results : r2Results, 3);

        Console.WriteLine("[DEBATE] ═══ 정반합 토론 완료 ═══");
    }

    /// <summary>Post debate summary to Slack thread.</summary>
    static void PostDebateSummary(List<TriadDebateLoop.RoundResult> results, int roundsCompleted)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"═══ *정반합 결과* (R{roundsCompleted} 완료) ═══");

        foreach (var r in results)
        {
            sb.AppendLine($"\n*{r.Ai}* ({r.Claims.Count} claims):");
            foreach (var c in r.Claims.OrderByDescending(c => c.Confidence).Take(3))
                sb.AppendLine($"  • [{c.Confidence:F2}] {c.Text[..Math.Min(80, c.Text.Length)]}");
        }

        var convergence = TriadDebateLoop.CalculateConvergence(results);
        var hasContradictions = TriadDebateLoop.HasContradictions(results);
        sb.AppendLine($"\n📊 Convergence: {convergence:F2} | Contradictions: {(hasContradictions ? "⚠ YES" : "✅ No")}");

        SlackPostToThread(sb.ToString(), "앱봇아이");
    }
}
