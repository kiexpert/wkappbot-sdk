using System.Collections.Concurrent;
using System.Text;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// Pure debate mode: ask triad --debate "question"
/// COMPLETELY SEPARATE from tool loop — uses CDP directly.
/// No AskCommand, no AskGemini, no loop persona, no APSP.
/// </summary>
internal partial class Program
{
    /// <summary>When true, skip loop/APSP persona injection (debate mode).</summary>
    internal static readonly System.Threading.AsyncLocal<bool> _suppressLoopPersona = new();

    static int AskTriadDebate(string question, int timeoutSec, string? modelHint)
    {
        Console.WriteLine("[정반합] Pure debate mode (no tool loop, CDP direct)");

        EnsureSlackThread("정반합", question);

        var debatePersona = BuildDebateOnlyPersona();
        SlackPostToThread($"📋 *[Debate Persona]*\n```\n{debatePersona[..Math.Min(500, debatePersona.Length)]}\n```", "Moderator");

        var ais = new[] { "gemini", "gpt", "claude" };
        var roles = new Dictionary<string, string>
        {
            ["gemini"] = "EXPLORER", ["gpt"] = "SKEPTIC", ["claude"] = "AUDITOR"
        };
        var hosts = new Dictionary<string, string>
        {
            ["gemini"] = "gemini.google.com", ["gpt"] = "chatgpt.com", ["claude"] = "claude.ai"
        };
        var editorSels = new Dictionary<string, string[]>
        {
            ["gemini"] = new[] { ".ql-editor", "[role='textbox'][contenteditable='true']", "div[contenteditable='true']" },
            ["gpt"] = new[] { "#prompt-textarea", "[contenteditable='true']" },
            ["claude"] = new[] { "div.tiptap.ProseMirror", "[contenteditable='true']" }
        };

        // ═══ R1: Independent positions (CDP direct) ═══
        Console.WriteLine("[정반합:R1] Independent positions...");
        SlackPostToThread("═══ *R1: Independent Positions* ═══", "Moderator");

        var r1Results = new ConcurrentDictionary<string, string>();
        var r1Tasks = ais.Select(ai => Task.Run(async () =>
        {
            try
            {
                var cdp = EnsureCdpConnection(preferredHost: hosts[ai], newTab: false,
                    targetTag: $"debate-{ai}-{DateTime.UtcNow:HHmmss}");
                if (cdp == null) { Console.WriteLine($"[정반합:{ai}] CDP connection failed"); return; }

                var prompt = $"{debatePersona}\n\nYour role: {roles[ai]}\n\nQuestion: {question}";
                var response = await DebateSendAndPoll(cdp, ai, prompt, editorSels[ai], timeoutSec);

                if (response != null)
                {
                    r1Results[ai] = response;
                    var snippet = response.Length > 200 ? response[..200] + "..." : response;
                    SlackPostToThread($"💬 *[{ai} {roles[ai]}]*:\n{snippet}", ai);

                    var stance = TriadDebateLoop.ParseStance(response);
                    if (stance != null)
                        SlackPostToThread($"📊 *[{ai}]* STANCE: {stance} (sum={stance.Sum})", ai);
                }
                else
                {
                    SlackPostToThread($"❌ *{ai}* R1 응답 실패", ai);
                }
                cdp.Dispose();
            }
            catch (Exception ex) { Console.Error.WriteLine($"[정반합:{ai}] R1 error: {ex.Message}"); }
        })).ToArray();

        // Wait for 2+ responses
        WaitForNTasks(r1Tasks, 2, timeoutSec, "R1");

        if (r1Results.Count < 1)
        {
            SlackPostToThread("❌ Debate aborted: no AIs responded.", "Moderator");
            return 1;
        }

        // ═══ R2: Cross-critique ═══
        Console.WriteLine("[정반합:R2] Cross-critique...");
        SlackPostToThread("═══ *R2: Cross-Critique* ═══", "Moderator");

        var r2Results = new ConcurrentDictionary<string, string>();
        var r2Tasks = ais.Where(ai => r1Results.ContainsKey(ai)).Select(ai => Task.Run(async () =>
        {
            try
            {
                var peers = new StringBuilder();
                foreach (var kv in r1Results)
                {
                    if (kv.Key == ai) continue;
                    var s = kv.Value.Length > 400 ? kv.Value[..400] + "..." : kv.Value;
                    peers.AppendLine($"[{kv.Key} says]: {s}");
                }

                var myR1 = r1Results.GetValueOrDefault(ai, "(no response)");
                var mySnippet = myR1.Length > 200 ? myR1[..200] + "..." : myR1;

                var prompt = $"{debatePersona}\n\nYour role: {roles[ai]}\n\n" +
                    $"Your R1 position: {mySnippet}\n\nPeer responses:\n{peers}\n\n" +
                    "React from your role. Update STANCE and claims. Use disputes[] to challenge peers.";

                var cdp = EnsureCdpConnection(preferredHost: hosts[ai], newTab: false,
                    targetTag: $"debate-{ai}-{DateTime.UtcNow:HHmmss}");
                if (cdp == null) return;

                var response = await DebateSendAndPoll(cdp, ai, prompt, editorSels[ai], timeoutSec);
                if (response != null)
                {
                    r2Results[ai] = response;
                    var snippet = response.Length > 200 ? response[..200] + "..." : response;
                    SlackPostToThread($"🔀 *[{ai} R2]*:\n{snippet}", ai);
                }
                cdp.Dispose();
            }
            catch (Exception ex) { Console.Error.WriteLine($"[정반합:{ai}] R2 error: {ex.Message}"); }
        })).ToArray();

        WaitForNTasks(r2Tasks, Math.Min(r1Results.Count, 2), timeoutSec, "R2");

        // ═══ R3: Synthesis + Korean conclusion ═══
        Console.WriteLine("[정반합:R3] Synthesis...");
        SlackPostToThread("═══ *R3: Final Synthesis* ═══", "Moderator");

        var allPositions = new StringBuilder();
        foreach (var ai in ais)
        {
            var best = r2Results.GetValueOrDefault(ai, r1Results.GetValueOrDefault(ai, ""));
            if (best.Length > 0)
            {
                var s = best.Length > 300 ? best[..300] + "..." : best;
                allPositions.AppendLine($"[{ai} final]: {s}");
            }
        }

        var synthAi = r1Results.ContainsKey("gemini") ? "gemini" : r1Results.Keys.First();
        var synthPrompt = $"{debatePersona}\n\nAll positions:\n{allPositions}\n\n" +
            "FINAL SYNTHESIS: (1) Consensus points (2) Remaining disagreements (3) Updated [DEBATE_JSON]\n" +
            "(4) Write your conclusion in Korean: [CONCLUSION_KR] 한국어 결론 [/CONCLUSION_KR]\n[SYNTHESIS_COMPLETE]";

        var synthTask = Task.Run(async () =>
        {
            try
            {
                var cdp = EnsureCdpConnection(preferredHost: hosts[synthAi], newTab: false,
                    targetTag: $"debate-{synthAi}-synth");
                if (cdp == null) return (string?)null;
                var result = await DebateSendAndPoll(cdp, synthAi, synthPrompt, editorSels[synthAi], timeoutSec);
                cdp.Dispose();
                return result;
            }
            catch { return (string?)null; }
        });

        synthTask.Wait(TimeSpan.FromSeconds(timeoutSec));
        var synthesis = synthTask.IsCompleted ? synthTask.Result : null;

        if (synthesis != null)
        {
            SlackPostToThread($"📋 *[Synthesis by {synthAi}]*\n{synthesis[..Math.Min(500, synthesis.Length)]}", "Moderator");

            var krStart = synthesis.IndexOf("[CONCLUSION_KR]");
            var krEnd = synthesis.IndexOf("[/CONCLUSION_KR]");
            if (krStart >= 0 && krEnd > krStart)
            {
                var kr = synthesis[(krStart + "[CONCLUSION_KR]".Length)..krEnd].Trim();
                SlackPostToThread($"🇰🇷 *한국어 결론*\n{kr}", "Moderator");
            }
        }

        Console.WriteLine("[정반합] ═══ Complete ═══");
        SlackPostToThread("═══ *정반합 토론 완료* ═══", "Moderator");
        return 0;
    }

    /// <summary>CDP direct: insert text + send + poll response. No AskCommand involvement.</summary>
    static async Task<string?> DebateSendAndPoll(CdpClient cdp, string ai, string prompt, string[] editorSels, int timeoutSec)
    {
        // Find editor
        string? editorSel = null;
        foreach (var sel in editorSels)
        {
            var len = await cdp.GetTextLengthAsync(sel);
            if (len >= 0) { editorSel = sel; break; }
        }
        if (editorSel == null)
        {
            // Wait and retry
            await Task.Delay(3000);
            foreach (var sel in editorSels)
            {
                try { var r = await cdp.EvalAsync($"document.querySelector('{CdpClient.Esc(sel)}') ? 'OK' : 'NO'");
                    if (r == "OK") { editorSel = sel; break; } } catch { }
            }
        }
        if (editorSel == null)
        {
            Console.Error.WriteLine($"[정반합:{ai}] Editor not found");
            return null;
        }

        // Snapshot response count before sending
        int preCount = 0;
        try { preCount = await cdp.GetResponseCountAsync(); } catch { }

        // Clear + insert + send
        await cdp.ClearEditorAsync(editorSel);
        await Task.Delay(200);
        await cdp.InsertContentEditableAsync(editorSel, prompt);
        await Task.Delay(300);
        await cdp.SendPromptAsync(editorSel);
        Console.WriteLine($"[정반합:{ai}] Sent ({prompt.Length} chars, preCount={preCount})");

        // Poll for response (use preCount as baseline to find NEW response only)
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string lastText = "";
        int stableCount = 0;
        var provider = ai == "gpt" ? "chatgpt" : ai;

        while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 90))
        {
            await Task.Delay(2000);
            // Try provider-specific response detection first
            string text = "";
            try { text = await cdp.GetLastResponseTextAsync(preCount) ?? ""; } catch { }
            // Fallback: generic body text change detection
            if (string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    var bodyText = await cdp.EvalAsync("(document.querySelector('[data-testid=\"assistant-message\"]') || document.querySelector('.response-container') || document.querySelector('.markdown') || {}).innerText || ''") ?? "";
                    if (bodyText.Length > 20) text = bodyText;
                }
                catch { }
            }
            if (string.IsNullOrWhiteSpace(text)) { Console.Write("."); continue; }

            if (text == lastText)
            {
                stableCount++;
                if (stableCount >= 3)
                {
                    Console.WriteLine($"[정반합:{ai}] Response stable ({text.Length} chars)");
                    return text;
                }
            }
            else
            {
                stableCount = 0;
                lastText = text;
                Console.Write($"[정반합:{ai} {sw.Elapsed.TotalSeconds:F0}s {text.Length}ch] ");
            }

            // Check if done streaming
            try
            {
                if (!await cdp.IsStreamingAsync(provider))
                {
                    await Task.Delay(1000);
                    var final = await cdp.GetLastResponseTextAsync() ?? text;
                    Console.WriteLine($"[정반합:{ai}] Done ({final.Length} chars)");
                    return final;
                }
            }
            catch { }
        }

        return string.IsNullOrEmpty(lastText) ? null : lastText;
    }

    /// <summary>Wait until N tasks complete or timeout.</summary>
    static void WaitForNTasks(Task[] tasks, int minDone, int timeoutSec, string label)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 120))
        {
            var done = tasks.Count(t => t.IsCompleted);
            if (done >= minDone) break;
            Thread.Sleep(2000);
            if (sw.Elapsed.TotalSeconds > 30 && done < minDone)
                SlackPostToThread($"⏳ [{label}] Waiting... ({done}/{tasks.Length})", "Moderator");
        }
    }

    /// <summary>Pure debate persona — no tool/APSP/loop instructions.</summary>
    static string BuildDebateOnlyPersona()
    {
        return """
You are in a structured dialectical debate. Respond with [DEBATE_JSON]...[/DEBATE_JSON]:

[DEBATE_JSON]{"stance":{"N":2,"R":3,"C":1,"E":2,"D":1},"role":"YOUR_ROLE","position":"one sentence","claims":[{"claim":"...","confidence":0.85,"key_assumptions":["..."]}],"rebuttals":["..."],"disputes":[{"target_assumption":"...","reason":"..."}]}[/DEBATE_JSON]

RULES: stance N+R+C+E+D must sum to 9. 2-5 claims. ENGLISH ONLY. Max 300 words.
Free text allowed AFTER the JSON block.
""";
    }
}
