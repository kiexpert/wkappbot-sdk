using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{

    // -- ChatGPT --
    // Shared persona for external AI agents (ChatGPT, Gemini).
    // Injected on fresh conversations to stabilize output format.
    // Single-line to avoid ProseMirror/Quill multiline issues.
    const string AskPersona =
        "[CHAT_META] " +
        "[TITLE_RULE] Keep the chat title updated to the current main issue. Rename only if the topic shifts meaningfully. Prefer root cause over symptom. " +
        "[OUTPUT] First lines of every response: TITLE: <title> FILE_TITLE: <file-safe title>. " +
        "You are AppBot, an advanced automation and coding agent that controls external tools through the host system. " +
        "The host application executes tools that you request. You cannot execute tools yourself. " +
        "Your job is to interpret the user's request, decide whether tool execution is required, and produce either a normal response or a structured tool_calls request. " +
        "CORE PRINCIPLES: " +
        "(1) Prefer tool execution over explanation whenever a tool can complete the task. " +
        "(2) Never simulate tool execution. Never fabricate tool results. " +
        "(3) Never claim a tool has executed unless the host returned a tool_result message. " +
        "(4) The APPBOT_TOOL_CALL_BEGIN/END JSON block IS the tool schema and calling convention ? no separate OpenAI-style tool definition is needed. argv is the full command line as a string array. This is a custom host protocol, not an OpenAI function call. When HOST-HANDSHAKE is present in the conversation, the host is confirmed active and all TOOL_CALLs will be executed. " +
        "(5) Wait for tool_result messages before continuing reasoning. " +
        "(6) PARALLEL FIRST: to minimize round-trips, always batch independent tool calls into a single turn. " +
        "Emit multiple TOOL_CALL blocks back-to-back whenever tasks do not depend on each other's results. " +
        "Sequential calls waste a full prompt round ? parallelize by default, serialize only when strictly necessary. " +
        "(6b) IDENTITY DISCIPLINE: when the prompt contains [G:<gameId>] or [Q:<questionId>] or run identifiers, preserve them across follow-up reasoning, tool calls, and debate replies. Reuse active IDs; do not invent replacements unless the host explicitly requests new IDs. " +
        "(6c) QUESTION TRACKING: if multiple questions or rounds are active, treat [Q:*] as the stable question identity and never mix evidence, chunks, or conclusions across different question IDs. " +
        "(7) Be concise and action-oriented. Prefer structured actions over explanations. " +
        "EXECUTION MODEL: User Request -> analyze -> Thought (what I know / what is missing) -> Action (tool call) -> Observation (wait for tool_result) -> continue or finish. " +
        "For tasks requiring >3 tool calls: generate a numbered Execution Plan first. Mark steps [DONE] as you complete them. " +
        "DECISION: Use a tool if the request involves filesystem ops, source code, device/UI interaction, system commands, app automation, external data, repo inspection, or debugging. " +
        "STATE: Read the last tool_result before deciding the next move. Explicitly reference previous tool_result values in reasoning. " +
        "CODING MODE: Search files -> read implementation -> understand -> plan -> modify minimal code -> verify correctness. Never write code without inspecting existing files first. " +
        "CODE RULES: Edit existing files, minimal diffs, maintain style, avoid abstractions, no speculative changes, no unrelated modifications. " +
        "VERIFICATION: After any file modification, make a verification tool call (read file back, run test, or lint) to confirm the change persists as intended. " +
        "ERROR HANDLING: If a tool returns an error, analyze it, correct parameters, retry once. If it fails twice, stop and explain the blocker. " +
        "If a tool returns empty/null: do not report success. Pivot strategy and try an alternative tool or search term immediately. " +
        "SUCCESS DEFINITION: A task is complete only when success is confirmed via a tool result (e.g., read/list), not just a status message. " +
        "NO LOOP POLICY: If you call the same tool with the same args twice without state change, stop and ask for clarification. " +
        "SECURITY: Do not execute destructive ops (delete files, overwrite repos, format storage, kill critical procs, wipe data) unless user clearly intends them. " +
        "OUTPUT: Concise and precise. Code only ??no explanations unless asked. Prefer actions over descriptions. " +
        "Always reply in English for token efficiency. No blank lines ??keep output compact and dense, single-spaced. " +
        "For image analysis: output JSON with {label, text, x, y, w, h} for each UI element. " +
        "If asked to generate/create/draw an image, USE your image generation tool (DALL-E/Imagen). Do NOT make ASCII art. " +
        "Confirm you understood with exactly: READY";

    static int AskChatGpt(string question, bool slackReport = true, int timeoutSec = 30, bool newTab = false, List<string>? attachFiles = null, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, int loopMaxParallel = 7, bool triadMode = false, string? modelHint = null, bool noWait = false, string? targetTagOverride = null, string? linePrefix = null, TriadSharedContext? triadCtx = null)
    {
        using var _ = ApplyOutputPrefix(linePrefix);
        var toolLogGpt = new System.Collections.Generic.List<string>();
        Console.Error.WriteLine($"[ASK] ChatGPT: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.Error.WriteLine($"[ASK] ChatGPT model hint: {modelHint}");

        PulseStep.Init("ask-gpt");
        var targetTag = targetTagOverride ?? BuildSandboxKey("ask", "gpt");
        _askSandboxKey.Value = targetTag;
        if (string.IsNullOrEmpty(targetTagOverride))
        {
            var qid = AskTargetRegistry.AssignNextQid(targetTag);
            _currentQid.Value = qid;
            question = $"[Q{qid}] {question}\n[REPLY: A{qid}]";
            Console.Error.WriteLine($"[ASK] GPT Q{qid} 할당됨 (tab={targetTag[..Math.Min(16, targetTag.Length)]})");
            Console.Error.WriteLine($"[ASK] 훈수두기: wkappbot ask gpt --intercept \"내용\" --qid {qid}");
        }
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;
        cdp.OperationContext = "gpt";
        if (triadCtx != null)
        {
            triadCtx.RegisterCdp("gpt", cdp);
            cdp.OnStreamingChunkEvent = triadCtx.UpdateChunk;
            cdp.OperationContext = "gpt:SKEPTIC"; // debate role overrides
        }
        using var askSession = new AskSession(AiProvider.ChatGpt, cdp); // gradual migration wrapper
        BindAskIdentity(askSession, question, "gpt");
        PulseStep.Mark("cdp-connected");

        // No tab activation ? CDP works on background tabs via targetId. Truly focusless.

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        EnsureSlackThread("ChatGPT", question);

        var task = Task.Run(async () =>
        {
            try
            {
                // --?Phase 1: Navigate (iconified OK) --?                PulseStep.Mark("phase1-navigate");
                var currentUrl = await cdp.GetUrlAsync() ?? "";
                Console.Error.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("chatgpt.com"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session ??navigating to fresh ChatGPT..." : "[ASK] Navigating to ChatGPT...");
                    await cdp.NavigateAsync("https://chatgpt.com");
                    await Task.Delay(3000);
                }

                // NOTE: BringToFront removed ??steals OS focus. CDP works on background tabs.

                // Wait for ProseMirror editor
                var editorSel = await WaitForChatGptEditorA11y(cdp);
                if (editorSel == null)
                    return (false, (string?)null);
                _currentAskCdp.Value = cdp;
                _currentAskHost.Value = "chatgpt";
                _currentAskEditorSel.Value = editorSel;
                PulseStep.Mark("editor-found");
                Console.Error.WriteLine($"[ASK] Editor found: {editorSel}");
                askSession.BindStreamingContext(editorSel);

                // Check if this is a fresh conversation ??try multiple selectors
                // TODO: migrate to askSession.GetUserMessageCountAsync() when multi-selector counting is unified
                var turnCountStr = await cdp.EvalAsync("""
                    (() => {
                        var c = document.querySelectorAll('[data-message-author-role="assistant"]').length;
                        if (c > 0) return '' + c;
                        c = document.querySelectorAll('article').length;
                        if (c > 0) return '' + Math.floor(c / 2);
                        c = document.querySelectorAll('[class*="agent-turn"]').length;
                        if (c > 0) return '' + c;
                        c = document.querySelectorAll('[data-testid*="conversation-turn"]').length;
                        if (c > 0) return '' + Math.floor(c / 2);
                        return '0';
                    })()
                    """) ?? "0";
                int existingTurns = int.TryParse(turnCountStr, out var etc) ? etc : 0;

                triadCtx?.BindStreamContext("gpt", cdp, editorSel, Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID"));
                // Reuse existing session ??only inject persona on fresh (0 turns) conversations
                if (existingTurns > 0)
                    Console.Error.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                var hasLoopPersonaState = await HasLoopPersonaStateAsync(cdp, "gpt");
                var effectiveLoopPersona = !_suppressLoopPersona.Value && (loopMode || hasLoopPersonaState);
                Console.Error.WriteLine($"[ASK] Loop persona state: {(hasLoopPersonaState ? "present" : "missing")}");
                if (!loopMode && hasLoopPersonaState)
                    Console.WriteLine("[ASK] Loop marker found; MCP guidance will be included for fresh session persona.");

                if (existingTurns == 0 || (effectiveLoopPersona && !hasLoopPersonaState))
                {
                    // Fresh conversation -- inject persona prompt first
                    Console.WriteLine(existingTurns == 0
                        ? "[ASK] Fresh conversation -- injecting persona..."
                        : "[ASK] Loop persona missing on this tab -- re-injecting persona...");
                    var personaTextGpt = BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint);
                    if (!_suppressLoopPersona.Value && Interlocked.CompareExchange(ref _slackPersonaPostedFlag, 1, 0) == 0)
                        SlackPostToThread($"⚡ *[persona]* steps={loopMaxSteps} retry={loopRetry}\n```\n{(personaTextGpt.Length > 800 ? personaTextGpt[..800] + "..." : personaTextGpt)}\n```", "System");
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, personaTextGpt, timeoutSec: 20, askSession: askSession);
                    if (!personaOk)
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                    else
                    {
                        bool ready = (personaResp ?? "").Contains("READY", StringComparison.OrdinalIgnoreCase);
                        Console.Error.WriteLine($"[ASK] Persona: {(ready ? "READY" : personaResp?.Substring(0, Math.Min(50, personaResp.Length)))}");
                        if (effectiveLoopPersona)
                            await SetLoopPersonaStateAsync(cdp, "gpt");
                    }
                }

                // Re-wait for editor readiness after persona exchange
                if (!await WaitForChatGptEditor(cdp))
                    return (false, (string?)null);
                await Task.Delay(200);


                // Prepend host handshake proof for loop sessions so GPT trusts the host is live
                if (effectiveLoopPersona)
                    question = BuildHostHandshake() + question;

                // Send the actual question (with 9s-delay retry on timeout)
                PulseStep.Mark("question-send");
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, attachFiles, returnAfterSend: noWait);
                if (!ok && string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("[ASK] ChatGPT timeout ??retrying in 9 seconds...");
                    await TryRecoverChatGptTabAsync(cdp, "timeout before retry");
                    await Task.Delay(9000);
                    (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, returnAfterSend: noWait, askSession: askSession);
                }
                {
                    EnsureSlackThread("ChatGPT", question);
                    if (ok && !string.IsNullOrWhiteSpace(answer))
                    {
                        var gptSlack = NormalizeBlankLines(answer);
                        SlackPostToThread(gptSlack.Length > 2000 ? gptSlack[..2000] + "..." : gptSlack, SlackAiName("gpt", "ChatGPT"));
                        SlackPostAnswerBlocks(gptSlack, "ChatGPT");
                    }
                    else
                         SlackPostToThread("[timeout or image failed]", SlackAiName("gpt", "ChatGPT"));
                }

                // Log initial answer to shared triad context (for recovery by other AIs if needed)
                if (ok && !string.IsNullOrWhiteSpace(answer))
                    triadCtx?.LogStep("ChatGPT", answer);

                Action<string, string?> onStepReport = (msg, uname) =>
                {
                    SlackPostToThread(msg, uname ?? SlackAiName("gpt", "ChatGPT"));
                    triadCtx?.LogStep("ChatGPT", msg);
                };
                if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
                    (ok, answer) = await RunChatGptLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry, loopMaxParallel, onStepReport, triadCtx, toolLogGpt);
                return (ok, answer);
            }
            catch (Exception ex)
            {
                LogError("ASK", ex);
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Parseable output format with markers
            Console.WriteLine("[ASK_ANSWER_BEGIN]");
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);
            Console.WriteLine("[ASK_ANSWER_END]");

            // Full answer marker (for programmatic capture by whisper study etc.)
            Console.WriteLine("[ASK_FULL_ANSWER_BEGIN]");
            Console.WriteLine(answer);
            Console.WriteLine("[ASK_FULL_ANSWER_END]");
            PulseStep.Done();

            // Slack already handled above (initial answer) + loop onStepReport
        }

        UnregisterWaitingTab("chatgpt");
        if (triadCtx != null && ok)
            SendPendingCrossPromptAsync(cdp, "gpt", "#prompt-textarea").GetAwaiter().GetResult();
        // Write ask result to .wkappbot/ask/ MD file
        if (ok && !string.IsNullOrEmpty(answer) && triadCtx == null)
            WriteAskMd("gpt", question, answer, toolLogGpt.Count > 0 ? toolLogGpt : null);
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // A11y-first selector chain for ChatGPT editor (most stable -> least stable)
    static readonly string[] ChatGptEditorSelectors =
    [
        "#prompt-textarea",                              // Stable ID
        "[data-testid='prompt-textarea']",               // Test hook (recent ChatGPT)
        "[role='textbox'][contenteditable='true']",      // ARIA role
        ".ProseMirror[contenteditable='true']",          // ProseMirror class
        "textarea[placeholder*='Message']",              // Plain textarea fallback
        "div[contenteditable='true']",                   // Generic fallback
    ];

    /// <summary>Close all about:blank tabs via CDP /json API.</summary>
    static async Task CloseBlankTabs(int port)
    {
        try
        {
            using var http = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var json = await http.GetStringAsync($"http://127.0.0.1:{port}/json");
            var targets = System.Text.Json.Nodes.JsonNode.Parse(json)?.AsArray();
            if (targets == null) return;
            int closed = 0;
            foreach (var t in targets)
            {
                var url = t?["url"]?.GetValue<string>() ?? "";
                var id = t?["id"]?.GetValue<string>() ?? "";
                if (url == "about:blank" && !string.IsNullOrEmpty(id))
                {
                    await http.GetAsync($"http://127.0.0.1:{port}/json/close/{id}");
                    closed++;
                }
            }
            if (closed > 0)
                Console.Error.WriteLine($"[ASK] Closed {closed} about:blank tab(s)");
        }
        catch { }
    }

    /// <summary>
    /// Close stale duplicate tabs for the same AI host (gemini/chatgpt/claude).
    /// Keeps only the saved (registry) tab, or if none saved, the most recently created one.
    /// Silently ignores errors.
    /// </summary>
    static async Task CloseStaleDuplicateTabs(int port, string host, string? keepTargetId)
    {
        try
        {
            using var http = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var json = await http.GetStringAsync($"http://127.0.0.1:{port}/json");
            var targets = System.Text.Json.Nodes.JsonNode.Parse(json)?.AsArray();
            if (targets == null) return;

            // Collect all tabs matching this host
            var matching = targets
                .Where(t => (t?["url"]?.GetValue<string>() ?? "").Contains(host, StringComparison.OrdinalIgnoreCase)
                         && (t?["type"]?.GetValue<string>() ?? "") == "page")
                .ToList();

             if (matching.Count <= 1) return; // no issue

            // registry에서 매칭된 탭을 키로드하여 열기 (이전 세션 보호)
            if (string.IsNullOrEmpty(keepTargetId)) return;

            var keepId = keepTargetId;

            int closed = 0;
            foreach (var t in matching)
            {
                var id = t?["id"]?.GetValue<string>() ?? "";
                if (id == keepId || string.IsNullOrEmpty(id)) continue;
                var url = t?["url"]?.GetValue<string>() ?? "";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Error.WriteLine($"[ASK] Closing stale {host} tab: {url[..Math.Min(60, url.Length)]}");
                Console.ResetColor();
                await http.GetAsync($"http://127.0.0.1:{port}/json/close/{id}");
                closed++;
            }
            if (closed > 0)
                Console.Error.WriteLine($"[ASK] Closed {closed} stale {host} tab(s) ??keeping active session");
        }
        catch { }
    }

    /// <summary>Count assistant turns ??multi-selector for ChatGPT DOM changes.
    /// TODO: migrate to askSession.GetResponseCountAsync() when multi-selector fallback counting is unified</summary>
    static async Task<int> CountChatGptTurns(CdpClient cdp)
    {
        var result = await cdp.EvalAsync("""
            (() => {
                var c = document.querySelectorAll('[data-message-author-role="assistant"]').length;
                if (c > 0) return '' + c;
                c = document.querySelectorAll('[data-testid*="conversation-turn"]').length;
                if (c > 0) return '' + Math.floor(c / 2);
                c = document.querySelectorAll('article').length;
                if (c > 0) return '' + Math.floor(c / 2);
                return '0';
            })()
            """) ?? "0";
        return int.TryParse(result, out var v) ? v : 0;
    }

    /// <summary>Wait for ChatGPT editor to be ready. Returns the working CSS selector.</summary>
    static async Task<string?> WaitForChatGptEditorA11y(CdpClient cdp)
    {
        // Restore Chrome if minimized ??V8 throttles JS when iconic, causing eval timeouts
        cdp.EnsureChromeNotIconic();
        Console.Write("[EDITOR-WAIT]");
        var sw = Stopwatch.StartNew();
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ChatGptEditorSelectors)
            {
                if (await cdp.QueryExistsAsync(sel)) { Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s -> {sel}"); return sel; }
            }
            if (attempt % 2 == 0) { Console.Write("."); Console.Out.Flush(); }
            await Task.Delay(500);
        }
        Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
        Console.WriteLine("[ASK] Editor not found (a11y selector chain exhausted)");
        // Diagnostic dump: URL + title + visible editables -- actionable clue for suggest queue
        try
        {
            var diag = await cdp.EvalAsync("""
                (() => {
                    var u = location.href;
                    var t = document.title;
                    var ce = document.querySelectorAll('[contenteditable=\"true\"]').length;
                    var ta = document.querySelectorAll('textarea').length;
                    var tb = document.querySelectorAll('[role=\"textbox\"]').length;
                    var login = /login|auth/i.test(location.pathname) ? 'login-page' : 'normal';
                    return JSON.stringify({url:u, title:t, contentEditable:ce, textarea:ta, textbox:tb, nav:login});
                })()
                """);
            Console.WriteLine($"[ASK] EDITOR-DIAG: {diag}");
        }
        catch { }

        // Last-resort rescue: ChatGPT sometimes renames the stable selector
        // faster than we can update the chain. If *any* visible editable
        // element exists, tag it with a unique data-attribute and return a
        // selector targeting that tag. Beats returning null and killing the
        // whole ask flow over a selector churn.
        try
        {
            var rescue = await cdp.EvalAsync("""
                (() => {
                    var tag = 'wkappbot-editor-rescue';
                    var cand = document.querySelector('[contenteditable="true"]')
                            || document.querySelector('[role="textbox"]')
                            || document.querySelector('textarea');
                    if (!cand) return '';
                    var r = cand.getBoundingClientRect();
                    if (r.width < 50 || r.height < 10) return '';
                    cand.setAttribute('data-' + tag, '1');
                    return '[data-' + tag + '="1"]';
                })()
                """);
            if (!string.IsNullOrEmpty(rescue) && rescue != "\"\"")
            {
                var sel = rescue.Trim('"');
                Console.WriteLine($"[ASK] EDITOR-RESCUE: tagged first visible editable -> {sel}");
                return sel;
            }
        }
        catch { }
        return null;
    }

    // Kept for backward compat (returns bool)
    static async Task<bool> WaitForChatGptEditor(CdpClient cdp)
        => await WaitForChatGptEditorA11y(cdp) != null;

    /// <summary>Generic a11y-first editor wait with custom selector chain.</summary>
    static async Task<string?> WaitForEditorA11y(CdpClient cdp, params string[] selectors)
    {
        // Restore Chrome if minimized ??V8 throttles JS when iconic, causing eval timeouts
        cdp.EnsureChromeNotIconic();
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in selectors)
            {
                // Use double-quote JS string so selectors with single quotes (e.g. [role='textbox'])
                // don't cause "SyntaxError: missing ) after argument list"
                if (await cdp.QueryExistsAsync(sel)) return sel;
            }
            await Task.Delay(500);
        }
        return null;
    }

}


