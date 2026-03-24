using System.Collections.Concurrent;
using System.Text;

namespace WKAppBot.CLI;

/// <summary>
/// Pure debate mode: ask triad --debate "question"
/// No tool loop, no APSP persona — just dialectical debate.
/// Short persona = less AI confusion, faster responses.
/// </summary>
internal partial class Program
{
    static int AskTriadDebate(string question, int timeoutSec, string? modelHint)
    {
        Console.WriteLine("[DEBATE] Pure dialectical debate mode (no tool loop)");

        // Unified Slack thread
        EnsureSlackThread("정반합", question);

        var sessionDir = Path.Combine(DataDir, "triad", $"debate_{DateTime.UtcNow:yyyyMMdd_HHmmss}");
        var ctx = new TriadSharedContext(question, sessionDir);

        // Pure debate persona — NO tool/APSP instructions
        var debatePersona = BuildDebateOnlyPersona();

        var ais = new[] { "gemini", "gpt", "claude" };
        var roles = new Dictionary<string, string>
        {
            ["gemini"] = "EXPLORER",
            ["gpt"] = "SKEPTIC",
            ["claude"] = "AUDITOR"
        };

        // R1: Independent answers with debate persona
        Console.WriteLine("[DEBATE:R1] Independent answers starting...");
        SlackPostToThread("═══ *Debate R1: Independent Positions* ═══", "Moderator");

        var r1Tasks = ais.Select(ai => Task.Run(() =>
        {
            var prompt = $"{debatePersona}\n\nYour role: {roles[ai]}\n\nQuestion: {question}";
            var response = AskAndCapture(ai, prompt, Math.Min(timeoutSec, 90));
            if (response != null)
            {
                ctx.UpdateChunk(ai, response);
                ctx.LogStep(ai, $"[R1] {response[..Math.Min(200, response.Length)]}");

                // Parse and report
                var debateJson = TriadDebateLoop.ParseDebateJson(response);
                var stance = TriadDebateLoop.ParseStance(response);
                var claims = TriadDebateLoop.ParseClaims(response);

                if (stance != null)
                    SlackPostToThread($"📊 *[{ai} {roles[ai]}]* STANCE: {stance}", ai);
                if (claims.Count > 0)
                    SlackPostToThread($"💬 *[{ai}]* {claims.Count} claims: {claims[0].Text[..Math.Min(80, claims[0].Text.Length)]}...", ai);
                else
                    SlackPostToThread($"💬 *[{ai}]* {response[..Math.Min(200, response.Length)]}...", ai);
            }
            return response;
        })).ToArray();

        Task.WaitAll(r1Tasks);
        var r1Results = r1Tasks.Select(t => t.Result).ToArray();
        var r1Count = r1Results.Count(r => r != null);
        Console.WriteLine($"[DEBATE:R1] Done: {r1Count}/3 responded");

        if (r1Count < 2)
        {
            SlackPostToThread("❌ Debate aborted: less than 2 AIs responded.", "Moderator");
            return 1;
        }

        // R2: Cross-critique — share R1 answers and ask for reactions
        Console.WriteLine("[DEBATE:R2] Cross-critique starting...");
        SlackPostToThread("═══ *Debate R2: Cross-Critique* ═══", "Moderator");

        var r2Tasks = ais.Select(ai => Task.Run(() =>
        {
            var otherResponses = new StringBuilder();
            for (int i = 0; i < ais.Length; i++)
            {
                if (ais[i] == ai || r1Results[i] == null) continue;
                var snippet = r1Results[i]!.Length > 500 ? r1Results[i]![..500] + "..." : r1Results[i]!;
                otherResponses.AppendLine($"[{ais[i]} says]: {snippet}");
            }

            var prompt = $"{debatePersona}\n\nYour role: {roles[ai]}\n\n" +
                $"You answered: {(r1Results[Array.IndexOf(ais, ai)] ?? "(no response)")[..Math.Min(200, (r1Results[Array.IndexOf(ais, ai)] ?? "").Length)]}\n\n" +
                $"Other AIs responded:\n{otherResponses}\n\n" +
                "React from your role's perspective. Update your STANCE and claims. Use [DISPUTE] if you challenge an assumption.";

            var response = AskAndCapture(ai, prompt, Math.Min(timeoutSec, 90));
            if (response != null)
            {
                ctx.UpdateChunk(ai, response);
                var stance = TriadDebateLoop.ParseStance(response);
                var claims = TriadDebateLoop.ParseClaims(response);
                if (stance != null)
                    SlackPostToThread($"📊 *[{ai} R2]* STANCE: {stance}", ai);
                if (claims.Count > 0)
                    SlackPostToThread($"🔀 *[{ai} R2]* {claims.Count} claims (revised)", ai);
            }
            return response;
        })).ToArray();

        Task.WaitAll(r2Tasks);
        Console.WriteLine("[DEBATE:R2] Done");

        // R3: Synthesis — ask for final conclusion in Korean
        Console.WriteLine("[DEBATE:R3] Synthesis starting...");
        SlackPostToThread("═══ *Debate R3: Final Synthesis* ═══", "Moderator");

        var allAnswers = new StringBuilder();
        for (int i = 0; i < ais.Length; i++)
        {
            var r2 = r2Tasks[i].Result ?? r1Results[i] ?? "(no response)";
            allAnswers.AppendLine($"[{ais[i]} final position]: {r2[..Math.Min(300, r2.Length)]}");
        }

        var synthPrompt = $"{debatePersona}\n\nAll positions after critique:\n{allAnswers}\n\n" +
            "Produce a FINAL SYNTHESIS:\n" +
            "1. Points of agreement across all AIs\n" +
            "2. Remaining disagreements with reasoning\n" +
            "3. Your final [DEBATE_JSON] with updated confidence\n" +
            "4. Write [CONCLUSION_KR] with your conclusion in Korean 한국어 [/CONCLUSION_KR]\n\n" +
            "[SYNTHESIS_COMPLETE] when done.";

        // Use first successful AI for synthesis
        var synthAi = r1Count >= 3 ? "gemini" : (r1Results[0] != null ? "gemini" : "gpt");
        var synthesis = AskAndCapture(synthAi, synthPrompt, Math.Min(timeoutSec, 90));
        if (synthesis != null)
        {
            SlackPostToThread($"📋 *[Final Synthesis by {synthAi}]*\n{synthesis[..Math.Min(500, synthesis.Length)]}", "Moderator");

            // Extract Korean conclusion
            var krStart = synthesis.IndexOf("[CONCLUSION_KR]");
            var krEnd = synthesis.IndexOf("[/CONCLUSION_KR]");
            if (krStart >= 0 && krEnd > krStart)
            {
                var krConclusion = synthesis[(krStart + "[CONCLUSION_KR]".Length)..krEnd].Trim();
                SlackPostToThread($"🇰🇷 *한국어 결론*\n{krConclusion}", "Moderator");
            }
        }

        Console.WriteLine("[DEBATE] ═══ Debate complete ═══");
        SlackPostToThread("═══ *Debate Complete* ═══", "Moderator");
        return 0;
    }

    /// <summary>Pure debate persona — no tool/APSP instructions.</summary>
    static string BuildDebateOnlyPersona()
    {
        return """
            You are participating in a structured dialectical debate with other AIs.

            RESPONSE FORMAT: Your response MUST contain [DEBATE_JSON]...[/DEBATE_JSON]:
            [DEBATE_JSON]{"stance":{"N":2,"R":3,"C":1,"E":2,"D":1},"role":"YOUR_ROLE","position":"one sentence","claims":[{"claim":"...","confidence":0.85,"key_assumptions":["..."]}],"rebuttals":["..."],"disputes":[{"target_assumption":"...","reason":"..."}]}[/DEBATE_JSON]

            RULES:
            - stance values N+R+C+E+D MUST sum to 9
            - N=Novelty R=Rigor C=Consensus E=Evidence D=Dissent
            - 2-5 claims per response, with honest confidence scores
            - ENGLISH ONLY for the debate (Korean only in [CONCLUSION_KR] tags)
            - Max 300 words. Claims over filler.
            - When critiquing peers, use disputes[] with specific target_assumption
            - Update confidence with prev_confidence when revising after critique

            You may add free text AFTER the JSON block for elaboration.
            """;
    }
}
