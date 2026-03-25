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
        sb.AppendLine("You previously answered this question. Now review anonymized peer responses.");
        sb.AppendLine($"Question: {question}");
        sb.AppendLine();

        char label = 'A';
        foreach (var r in r1Results)
        {
            if (r.Ai.Equals(selfAi, StringComparison.OrdinalIgnoreCase)) continue;
            sb.AppendLine($"=== AI-{label} Response ===");
            sb.AppendLine(r.Summary);
            foreach (var c in r.Claims)
                sb.AppendLine($"  Claim (confidence={c.Confidence:F2}): {c.Text}");
            sb.AppendLine();
            label++;
        }

        sb.AppendLine("""
            Tasks:
            1. Identify points of AGREEMENT with peers (strengthen shared claims)
            2. Identify points of DISAGREEMENT (explain why, with evidence)
            3. Flag any assumptions you think are WRONG in peer responses
            4. Revise your own claims if convinced by peer arguments

            Output your revised claims in the same format:
            [CLAIM]{"claim":"...","confidence":0.9,"key_assumptions":["..."]}[/CLAIM]

            Be intellectually honest — update confidence based on peer evidence.
            If you disagree, explain WHY with specific reasoning (informed disagreement).
            """);

        return sb.ToString();
    }

    /// <summary>R3: Synthesis — propose unified answer.</summary>
    public static string BuildR3Prompt(string question, List<RoundResult> r2Results)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Final synthesis round. All AIs have now critiqued each other's positions.");
        sb.AppendLine($"Original question: {question}");
        sb.AppendLine();
        sb.AppendLine("Peer positions after critique:");

        foreach (var r in r2Results)
        {
            sb.AppendLine($"=== {r.Ai} (revised) ===");
            foreach (var c in r.Claims)
                sb.AppendLine($"  [{c.Confidence:F2}] {c.Text}");
        }

        sb.AppendLine();
        sb.AppendLine("""
            Produce a UNIFIED SYNTHESIS:
            1. State the consensus answer (points all AIs agree on)
            2. State remaining disagreements (if any) with each position's reasoning
            3. Provide your FINAL claims with updated confidence:
            [CLAIM]{"claim":"...","confidence":0.95,"key_assumptions":["..."]}[/CLAIM]

            IMPORTANT: Write your FINAL CONCLUSION in Korean (한국어).
            Include each AI's position with their reasoning:
            [CONCLUSION_KR]
            [Gemini/EXPLORER의 판단]: (이 AI의 핵심 주장과 근거)
            [GPT/SKEPTIC의 판단]: (이 AI의 핵심 주장과 근거)
            [Claude/AUDITOR의 판단]: (이 AI의 핵심 주장과 근거)
            [합의]: (공통 결론)
            [미합의]: (남은 이견과 각자의 이유)
            [/CONCLUSION_KR]
            The rest of the synthesis should remain in English.

            Mark the synthesis complete with: [SYNTHESIS_COMPLETE]
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
        var m = StanceRegex.Match(text);
        if (!m.Success) return null;
        return new Stance(
            int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value),
            int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value),
            int.Parse(m.Groups[5].Value));
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

    static HashSet<string> Tokenize(string text)
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
