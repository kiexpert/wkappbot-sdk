using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

/// <summary>
/// 정반합 (正反合) Debate Loop — structured multi-AI dialectical reasoning.
///
/// R1 (Diversity):  Each AI answers independently with structured claims.
/// R2 (Critique):   AIs see anonymized peer answers, critique assumptions.
/// R3 (Synthesis):  AIs propose unified answer, convergence checked.
///
/// Convergence: Jaccard overlap of claim keywords >= 0.6 + no contradictions.
/// Max 3 rounds. 300-token summary compression per round.
/// </summary>
internal sealed class TriadDebateLoop
{
    public record Claim(string Text, double Confidence, string[] KeyAssumptions);
    public record RoundResult(string Ai, string Summary, List<Claim> Claims);

    private readonly string _question;
    private readonly TriadSharedContext _ctx;
    private readonly List<List<RoundResult>> _rounds = new(); // _rounds[roundIdx][aiIdx]

    public TriadDebateLoop(string question, TriadSharedContext ctx)
    {
        _question = question;
        _ctx = ctx;
    }

    // ── Prompt Templates ──

    /// <summary>R1: Independent answer with structured claims.</summary>
    public static string BuildR1Prompt(string question)
    {
        return $$"""
            You are participating in a 정반합 (dialectical) debate with other AIs.
            Answer in ENGLISH. Be concise (max 300 words).

            For each key point, use this exact format:
            [CLAIM]{"claim":"your specific claim","confidence":0.85,"key_assumptions":["assumption1","assumption2"]}[/CLAIM]

            Guidelines:
            - Provide 2-5 claims covering different aspects
            - Confidence: 0.0 (uncertain) to 1.0 (certain) — be honest
            - List key assumptions that could be challenged
            - Other AIs WILL see and critique your claims — be defensible

            Question: {{question}}
            """;
    }

    /// <summary>R2: Critique peer answers (anonymized as AI-A, AI-B).</summary>
    public static string BuildR2Prompt(string question, List<RoundResult> r1Results, string selfAi)
    {
        var sb = new StringBuilder();
        sb.AppendLine("[MODERATOR — Round 2: Cross-Critique]");
        sb.AppendLine();
        sb.AppendLine("You answered this question in Round 1. Now it's time to critique your peers.");
        sb.AppendLine("Below are anonymized peer responses. Read them carefully, then respond.");
        sb.AppendLine($"\nOriginal question: {question}");
        sb.AppendLine();

        char label = 'A';
        foreach (var r in r1Results)
        {
            if (r.Ai.Equals(selfAi, StringComparison.OrdinalIgnoreCase)) continue;
            sb.AppendLine($"═══ Peer AI-{label} ═══");
            sb.AppendLine(r.Summary);
            if (r.Claims.Count > 0)
            {
                sb.AppendLine($"  Their claims ({r.Claims.Count}):");
                foreach (var c in r.Claims)
                    sb.AppendLine($"    • [{c.Confidence:F2}] {c.Text}");
            }
            sb.AppendLine();
            label++;
        }

        sb.AppendLine("""
            ━━ What you must do ━━
            1. AGREE: Which peer claims do you support? Strengthen them with your reasoning.
            2. DISAGREE: Which claims are wrong or weak? Explain WHY with evidence.
               Use: [DISPUTE]{"target_assumption":"what you challenge","reason":"why it's wrong"}[/DISPUTE]
            3. REVISE: Update your own claims based on what you learned from peers.
               If a peer convinced you, raise your confidence. If not, explain why.

            ━━ Required output format ━━
            • For each of your points (2-5 total):
              [CLAIM]{"claim":"your position","confidence":0.85,"key_assumptions":["assumption1"]}[/CLAIM]
            • At least ONE [DISPUTE] tag (this is a critique round — you MUST challenge something)
            • End with: [STANCE N=? R=? C=? E=? D=?] (sum must equal 9, D must be >= 1)

            Be direct. Be honest. Don't just agree to be nice — real disagreement makes better answers.
            """);

        return sb.ToString();
    }

    /// <summary>R3: Synthesis — propose unified answer.</summary>
    public static string BuildR3Prompt(string question, List<RoundResult> r2Results,
        List<string>? priorConsensusItems = null, string? currentAi = null)
    {
        var sb = new StringBuilder();
        sb.AppendLine("[MODERATOR — Round 3: Final Synthesis]");
        sb.AppendLine();
        sb.AppendLine("All AIs have critiqued each other. Now it's time to find common ground.");
        sb.AppendLine($"\nOriginal question: {question}");

        // Cascading: show prior AIs' consensus items
        if (priorConsensusItems?.Count > 0)
        {
            sb.AppendLine();
            sb.AppendLine("═══ PRIOR AIs' CONSENSUS ITEMS (you MUST address each one!) ═══");
            sb.AppendLine("For each item below: INCLUDE it in your [합의] if you agree, or move it to [미합의] with your reason.");
            sb.AppendLine("You may also ADD new items not listed here.");
            sb.AppendLine();
            for (int i = 0; i < priorConsensusItems.Count; i++)
                sb.AppendLine($"  {i + 1}. {priorConsensusItems[i]}");
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.AppendLine("Here's where each AI stands after the critique round:");

        foreach (var r in r2Results)
        {
            sb.AppendLine($"\n═══ {r.Ai} (revised position) ═══");
            if (r.Claims.Count > 0)
                foreach (var c in r.Claims)
                    sb.AppendLine($"  • [{c.Confidence:F2}] {c.Text}");
            else
                sb.AppendLine($"  (no structured claims extracted)");
        }

        sb.AppendLine();
        sb.AppendLine("""
            ━━ What you must do ━━
            This is the FINAL round. Produce a unified synthesis that captures the best of all positions.

            Step 1: English synthesis (brief)
            • What do all AIs agree on? (consensus)
            • What remains disputed? (with each side's reasoning)
            • Your final claims (2-4):
              [CLAIM]{"claim":"...","confidence":0.95,"key_assumptions":["..."]}[/CLAIM]

            Step 2: Korean conclusion (REQUIRED — this is the main output!)
            Write a thorough conclusion IN KOREAN (max 2000 chars total — fits one Slack message).
            Use this exact format:

            [CONCLUSION_KR]
            [Gemini/EXPLORER의 판단]: (이 AI의 핵심 주장 2-3줄 + 근거)
            [GPT/SKEPTIC의 판단]: (이 AI의 핵심 주장 2-3줄 + 근거)
            [Claude/AUDITOR의 판단]: (이 AI의 핵심 주장 2-3줄 + 근거)
            [합의]: Each item must be ATOMIC (one clear proposition per line) with your agreement score (0-9).
            Score 7+ = genuine agreement. Below 7 = move to [미합의].
            Format: "1. 구체적 합의 내용 (8)" — 한국어, 항목당 한 문장, 총 100단어 이상!
            Example:
              1. Jaccard 키워드 겹침을 NLI 의미론적 함의로 대체한다 (9)
              2. 원자적 명제 분해가 의미 분석보다 선행한다 (6) ← 7미만이면 [미합의]로!
            [미합의]: (남은 이견 + 점수 7 미만 항목. ⚠️ 다른 AI가 [합의]에 넣었지만 동의 안 하면 반드시 여기에! 없으면 "없음")
            [개인의견]: (당신의 솔직한 본심 — 합의와 다를 수 있음, 20단어 이상)
            [/CONCLUSION_KR]

            Step 3: End with [STANCE N=? R=? C=? E=? D=?] (sum=9)

            ⚠ The Korean conclusion IS the deliverable. Don't skip it or make it brief.
            Mark complete with: [SYNTHESIS_COMPLETE]
            """);

        return sb.ToString();
    }

    // ── DEBATE_JSON parsing ──

    private static readonly Regex DebateJsonRegex = new(@"\[DEBATE_JSON\](.*?)\[/DEBATE_JSON\]", RegexOptions.Singleline);

    public static JsonNode? ParseDebateJson(string text)
    {
        var m = DebateJsonRegex.Match(text);
        if (!m.Success) return null;
        try { return JsonNode.Parse(m.Groups[1].Value); }
        catch { return null; }
    }

    // ── Stance Points (N=Novelty R=Rigor C=Consensus E=Evidence D=Dissent, sum=9) ──

    public record Stance(int Novelty, int Rigor, int Consensus, int Evidence, int Dissent)
    {
        public int Sum => Novelty + Rigor + Consensus + Evidence + Dissent;
        public override string ToString() => $"N={Novelty} R={Rigor} C={Consensus} E={Evidence} D={Dissent}";
    }

    private static readonly Regex StanceRegex = new(@"\[STANCE\s+N=(\d+)\s+R=(\d+)\s+C=(\d+)\s+E=(\d+)\s+D=(\d+)\]");

    public static Stance? ParseStance(string text)
    {
        // Tier 1: [STANCE N=X R=X C=X E=X D=X] inline tag
        var m = StanceRegex.Match(text);
        if (m.Success)
            return new Stance(
                int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value),
                int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value),
                int.Parse(m.Groups[5].Value));

        // Tier 2: DEBATE_JSON → "stance":{"N":2,"R":3,"C":1,"E":2,"D":1}
        var json = ParseDebateJson(text);
        var stanceNode = json?["stance"];
        if (stanceNode != null)
        {
            try
            {
                return new Stance(
                    stanceNode["N"]?.GetValue<int>() ?? 0,
                    stanceNode["R"]?.GetValue<int>() ?? 0,
                    stanceNode["C"]?.GetValue<int>() ?? 0,
                    stanceNode["E"]?.GetValue<int>() ?? 0,
                    stanceNode["D"]?.GetValue<int>() ?? 0);
            }
            catch { }
        }

        return null;
    }

    /// <summary>Stance convergence: all AIs have C >= 3 (consensus priority) and D <= 1 (low dissent).</summary>
    public static bool StancesConverged(List<(string ai, Stance stance)> stances)
    {
        if (stances.Count < 2) return false;
        return stances.All(s => s.stance.Consensus >= 3 && s.stance.Dissent <= 1);
    }

    // ── Dispute tracking ──

    public record Dispute(string TargetAssumption, string Reason);

    private static readonly Regex DisputeRegex = new(@"\[DISPUTE\](.*?)\[/DISPUTE\]", RegexOptions.Singleline);

    public static List<Dispute> ParseDisputes(string text)
    {
        var disputes = new List<Dispute>();
        foreach (Match m in DisputeRegex.Matches(text))
        {
            try
            {
                var json = JsonNode.Parse(m.Groups[1].Value);
                if (json == null) continue;
                var target = json["target_assumption"]?.GetValue<string>() ?? "";
                var reason = json["reason"]?.GetValue<string>() ?? "";
                if (!string.IsNullOrEmpty(target))
                    disputes.Add(new Dispute(target, reason));
            }
            catch { }
        }
        return disputes;
    }

    /// <summary>Check if all disputes are resolved (referenced in later claims' disputed_by).</summary>
    public static bool AllDisputesResolved(List<RoundResult> results)
    {
        var allDisputes = new List<string>();
        var allResolutions = new List<string>();
        foreach (var r in results)
        {
            foreach (var c in r.Claims)
                foreach (var a in c.KeyAssumptions)
                    allResolutions.Add(a.ToLower());
        }
        // A dispute is "resolved" if its target_assumption appears in some claim's key_assumptions
        // (meaning someone addressed it). Simple heuristic.
        return true; // TODO: refine with actual dispute tracking
    }

    // ── Claim Parsing ──

    private static readonly Regex ClaimRegex = new(@"\[CLAIM\](.*?)\[/CLAIM\]", RegexOptions.Singleline);

    public static List<Claim> ParseClaims(string text)
    {
        var claims = new List<Claim>();
        // Tier 1: [CLAIM]...[/CLAIM] inline tags
        foreach (Match m in ClaimRegex.Matches(text))
        {
            try
            {
                var json = JsonNode.Parse(m.Groups[1].Value);
                if (json == null) continue;
                var claim = json["claim"]?.GetValue<string>() ?? "";
                var conf = json["confidence"]?.GetValue<double>() ?? 0.5;
                var assumptions = json["key_assumptions"]?.AsArray()
                    .Select(a => a?.GetValue<string>() ?? "").ToArray() ?? [];
                if (!string.IsNullOrEmpty(claim))
                    claims.Add(new Claim(claim, Math.Clamp(conf, 0, 1), assumptions));
            }
            catch { }
        }
        // Tier 2: DEBATE_JSON → "claims" array (Gemini often puts claims inside DEBATE_JSON)
        if (claims.Count == 0)
        {
            var debateJson = ParseDebateJson(text);
            var claimsArr = debateJson?["claims"]?.AsArray();
            if (claimsArr != null)
            {
                foreach (var node in claimsArr)
                {
                    try
                    {
                        var claim = node?["claim"]?.GetValue<string>() ?? "";
                        var conf = node?["confidence"]?.GetValue<double>() ?? 0.5;
                        var assumptions = node?["key_assumptions"]?.AsArray()
                            ?.Select(a => a?.GetValue<string>() ?? "").ToArray() ?? [];
                        if (!string.IsNullOrEmpty(claim))
                            claims.Add(new Claim(claim, Math.Clamp(conf, 0, 1), assumptions));
                    }
                    catch { }
                }
            }
        }
        return claims;
    }

    // ── Convergence Detection ──

    /// <summary>
    /// Jaccard overlap of claim keywords across all AIs.
    /// Returns overlap ratio (0.0 - 1.0). >= 0.6 = converged.
    /// </summary>
    public static double CalculateConvergence(List<RoundResult> results)
    {
        if (results.Count < 2) return 0;

        var allKeywordSets = results.Select(r =>
        {
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in r.Claims)
            {
                foreach (var w in Tokenize(c.Text))
                    words.Add(w);
            }
            return words;
        }).ToList();

        // Pairwise Jaccard, then average
        double totalJaccard = 0;
        int pairs = 0;
        for (int i = 0; i < allKeywordSets.Count; i++)
        {
            for (int j = i + 1; j < allKeywordSets.Count; j++)
            {
                var intersection = allKeywordSets[i].Intersect(allKeywordSets[j]).Count();
                var union = allKeywordSets[i].Union(allKeywordSets[j]).Count();
                totalJaccard += union > 0 ? (double)intersection / union : 0;
                pairs++;
            }
        }
        return pairs > 0 ? totalJaccard / pairs : 0;
    }

    /// <summary>Check if any claims directly contradict each other.</summary>
    public static bool HasContradictions(List<RoundResult> results)
    {
        // Simple heuristic: if two high-confidence claims contain negation words about the same topic
        var allClaims = results.SelectMany(r => r.Claims).Where(c => c.Confidence >= 0.7).ToList();
        for (int i = 0; i < allClaims.Count; i++)
        {
            for (int j = i + 1; j < allClaims.Count; j++)
            {
                var a = allClaims[i].Text.ToLower();
                var b = allClaims[j].Text.ToLower();
                // If one says "should" and other says "should not" about similar topic
                var aWords = new HashSet<string>(Tokenize(a));
                var bWords = new HashSet<string>(Tokenize(b));
                var overlap = aWords.Intersect(bWords).Count();
                bool aHasNot = a.Contains("not ") || a.Contains("don't") || a.Contains("shouldn't") || a.Contains("incorrect");
                bool bHasNot = b.Contains("not ") || b.Contains("don't") || b.Contains("shouldn't") || b.Contains("incorrect");
                if (overlap >= 3 && aHasNot != bHasNot)
                    return true; // likely contradiction
            }
        }
        return false;
    }

    // ── Summary Compression ──

    /// <summary>Compress response to ~300 tokens (rough: 1 token ≈ 4 chars).</summary>
    public static string CompressSummary(string text, int maxChars = 1200)
    {
        if (text.Length <= maxChars) return text;
        // Keep first paragraph + claims
        var lines = text.Split('\n');
        var sb = new StringBuilder();
        bool inClaim = false;
        foreach (var line in lines)
        {
            if (line.Contains("[CLAIM]")) { sb.AppendLine(line); inClaim = true; continue; }
            if (inClaim && line.Contains("[/CLAIM]")) { sb.AppendLine(line); inClaim = false; continue; }
            if (sb.Length < maxChars / 2) sb.AppendLine(line);
        }
        var result = sb.ToString();
        return result.Length > maxChars ? result[..maxChars] + "..." : result;
    }

    // ── Tokenizer ──

    private static readonly Regex WordRegex = new(@"\b\w{3,}\b");
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "the", "and", "for", "are", "but", "not", "you", "all", "can", "her",
        "was", "one", "our", "out", "has", "have", "with", "this", "that", "from",
        "they", "been", "said", "each", "which", "their", "will", "other", "about",
        "should", "would", "could", "these", "than", "into", "some", "when", "there"
    };

    public static HashSet<string> Tokenize(string text)
    {
        var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match m in WordRegex.Matches(text))
        {
            var w = m.Value.ToLower();
            if (!StopWords.Contains(w))
                words.Add(w);
        }
        return words;
    }
}
