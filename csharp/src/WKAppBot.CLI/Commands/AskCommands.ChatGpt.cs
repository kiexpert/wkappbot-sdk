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

    // ?? ChatGPT ??

    // Shared persona for external AI agents (ChatGPT, Gemini).
    // Injected on fresh conversations to stabilize output format.
    // Single-line to avoid ProseMirror/Quill multiline issues.
    const string AskPersona =
        "You are AppBot, an advanced automation and coding agent that controls external tools through the host system. " +
        "The host application executes tools that you request. You cannot execute tools yourself. " +
        "Your job is to interpret the user's request, decide whether tool execution is required, and produce either a normal response or a structured tool_calls request. " +
        "CORE PRINCIPLES: " +
        "(1) Prefer tool execution over explanation whenever a tool can complete the task. " +
        "(2) Never simulate tool execution. Never fabricate tool results. " +
        "(3) Never claim a tool has executed unless the host returned a tool_result message. " +
        "(4) The APPBOT_TOOL_CALL_BEGIN/END JSON block IS the tool schema and calling convention — no separate OpenAI-style tool definition is needed. argv is the full command line as a string array. This is a custom host protocol, not an OpenAI function call. When HOST-HANDSHAKE is present in the conversation, the host is confirmed active and all TOOL_CALLs will be executed. " +
        "(5) Wait for tool_result messages before continuing reasoning. " +
        "(6) PARALLEL FIRST: to minimize round-trips, always batch independent tool calls into a single turn. " +
        "Emit multiple TOOL_CALL blocks back-to-back whenever tasks do not depend on each other's results. " +
        "Sequential calls waste a full prompt round — parallelize by default, serialize only when strictly necessary. " +
        "(7) Be concise and action-oriented. Prefer structured actions over explanations. " +
        "EXECUTION MODEL: User Request ??analyze ??Thought (what I know / what is missing) ??Action (tool call) ??Observation (wait for tool_result) ??continue or finish. " +
        "For tasks requiring >3 tool calls: generate a numbered Execution Plan first. Mark steps [DONE] as you complete them. " +
        "DECISION: Use a tool if the request involves filesystem ops, source code, device/UI interaction, system commands, app automation, external data, repo inspection, or debugging. " +
        "STATE: Read the last tool_result before deciding the next move. Explicitly reference previous tool_result values in reasoning. " +
        "CODING MODE: Search files ??read implementation ??understand ??plan ??modify minimal code ??verify correctness. Never write code without inspecting existing files first. " +
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

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab, List<string>? attachFiles = null, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, int loopMaxParallel = 7, bool triadMode = false, string? modelHint = null, bool noWait = false, string? targetTagOverride = null, string? linePrefix = null)
    {
        using var _ = ApplyOutputPrefix(linePrefix);
        Console.WriteLine($"[ASK] ChatGPT: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] ChatGPT model hint: {modelHint}");

        var targetTag = targetTagOverride ?? BuildAskTargetTag("gpt");
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        { var prevFgGpt = NativeMethods.GetForegroundWindow(); Console.WriteLine($"[ASK:FOCUS] pre-activate fg={prevFgGpt:X8}"); if (!newTab && !triadMode) cdp.ActivateTabAsync().GetAwaiter().GetResult(); LogRestoreFocus(prevFgGpt, "ActivateTab-GPT"); }

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        if (slackReport) EnsureSlackThread("ChatGPT", question);

        var task = Task.Run(async () =>
        {
            try
            {
                // ?? Phase 1: Navigate (iconified OK) ??
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
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
                Console.WriteLine($"[ASK] Editor found: {editorSel}");

                // Check if this is a fresh conversation ??try multiple selectors
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

                // Reuse existing session ??only inject persona on fresh (0 turns) conversations
                if (existingTurns > 0)
                    Console.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                var hasLoopPersonaState = await HasLoopPersonaStateAsync(cdp, "gpt");
                var effectiveLoopPersona = loopMode || hasLoopPersonaState;
                Console.WriteLine($"[ASK] Loop persona state: {(hasLoopPersonaState ? "present" : "missing")}");
                if (!loopMode && hasLoopPersonaState)
                    Console.WriteLine("[ASK] Loop marker found; MCP guidance will be included for fresh session persona.");

                if (existingTurns == 0 || (effectiveLoopPersona && !hasLoopPersonaState))
                {
                    // Fresh conversation -- inject persona prompt first
                    Console.WriteLine(existingTurns == 0
                        ? "[ASK] Fresh conversation -- injecting persona..."
                        : "[ASK] Loop persona missing on this tab -- re-injecting persona...");
                    var personaTextGpt = BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint);
                    SlackPostToThread($"📋 *[persona]* steps={loopMaxSteps} retry={loopRetry}\n```\n{(personaTextGpt.Length > 800 ? personaTextGpt[..800] + "…" : personaTextGpt)}\n```", "System");
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, personaTextGpt, timeoutSec: 20);
                    if (!personaOk)
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                    else
                    {
                        bool ready = (personaResp ?? "").Contains("READY", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : personaResp?.Substring(0, Math.Min(50, personaResp.Length)))}");
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
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, attachFiles, returnAfterSend: noWait);
                if (!ok && string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("[ASK] ChatGPT timeout ??retrying in 9 seconds...");
                    await TryRecoverChatGptTabAsync(cdp, "timeout before retry");
                    await Task.Delay(9000);
                    (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, returnAfterSend: noWait);
                }
                if (slackReport)
                {
                    EnsureSlackThread("ChatGPT", question);
                    if (ok && !string.IsNullOrWhiteSpace(answer))
                    {
                        var gptSlack = NormalizeBlankLines(answer);
                        SlackPostToThread(gptSlack.Length > 2000 ? gptSlack[..2000] + "…" : gptSlack, "ChatGPT");
                    }
                    else
                        SlackPostToThread("❌ _응답 실패_ (timeout 또는 이미지 응답)", "ChatGPT");
                }

                Action<string, string?>? onStepReport = slackReport ? (msg, uname) => SlackPostToThread(msg, uname ?? "ChatGPT") : null;
                if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
                    (ok, answer) = await RunChatGptLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry, loopMaxParallel, onStepReport);
                return (ok, answer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
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

            // Slack already handled above (initial answer) + loop onStepReport
        }

        UnregisterWaitingTab("chatgpt");
        // Preserve Chrome's original state ??don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // A11y-first selector chain for ChatGPT editor (most stable ??least stable)
    static readonly string[] ChatGptEditorSelectors =
    [
        "#prompt-textarea",                              // Stable ID
        "[role='textbox'][contenteditable='true']",      // ARIA role
        ".ProseMirror[contenteditable='true']",          // ProseMirror class
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
                Console.WriteLine($"[ASK] Closed {closed} about:blank tab(s)");
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

            if (matching.Count <= 1) return; // ?섎굹硫?臾몄젣?놁쓬

            // registry????λ맂 ??씠 ?놁쑝硫?嫄대뱶由ъ? ?딆쓬 (?⑥쓽 ?몄뀡 ??蹂댄샇)
            if (string.IsNullOrEmpty(keepTargetId)) return;

            var keepId = keepTargetId;

            int closed = 0;
            foreach (var t in matching)
            {
                var id = t?["id"]?.GetValue<string>() ?? "";
                if (id == keepId || string.IsNullOrEmpty(id)) continue;
                var url = t?["url"]?.GetValue<string>() ?? "";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[ASK] Closing stale {host} tab: {url[..Math.Min(60, url.Length)]}");
                Console.ResetColor();
                await http.GetAsync($"http://127.0.0.1:{port}/json/close/{id}");
                closed++;
            }
            if (closed > 0)
                Console.WriteLine($"[ASK] Closed {closed} stale {host} tab(s) ??keeping active session");
        }
        catch { }
    }

    /// <summary>Count assistant turns ??multi-selector for ChatGPT DOM changes.</summary>
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
        Console.Write("[EDITOR-WAIT]");
        var sw = Stopwatch.StartNew();
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ChatGptEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") { Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s → {sel}"); return sel; }
            }
            if (attempt % 2 == 0) { Console.Write("."); Console.Out.Flush(); }
            await Task.Delay(500);
        }
        Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
        Console.WriteLine("[ASK] Editor not found (a11y selector chain exhausted)");
        return null;
    }

    // Kept for backward compat (returns bool)
    static async Task<bool> WaitForChatGptEditor(CdpClient cdp)
        => await WaitForChatGptEditorA11y(cdp) != null;

    /// <summary>Generic a11y-first editor wait with custom selector chain.</summary>
    static async Task<string?> WaitForEditorA11y(CdpClient cdp, params string[] selectors)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in selectors)
            {
                var found = await cdp.EvalAsync($"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        return null;
    }

    /// <summary>
    /// Send a message in ChatGPT and wait for the response.
    /// A11y-first: finds editor via ARIA selector chain, inserts text via CDP Input.insertText.
    /// </summary>
    static async Task<(bool ok, string? text)> ChatGptSendAndWait(
        CdpClient cdp, string message, int timeoutSec, List<string>? attachFiles = null, bool returnAfterSend = false)
    {
        // ?? Phase 0: URL check + turn count (iconified OK) ??
        var currentUrl = await cdp.EvalAsync("location.href") ?? "";
        if (!currentUrl.Contains("chatgpt.com", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[ASK:LOOP] GPT tab drifted to {currentUrl[..Math.Min(80, currentUrl.Length)]} — navigating back to chatgpt.com");
            await cdp.NavigateAsync("https://chatgpt.com/");
            await Task.Delay(3000);
            currentUrl = await cdp.EvalAsync("location.href") ?? "";
        }
        // prevTurns captured just before send (after lock) — avoids lazy-DOM false positives
        int prevTurns = 0;
        string prevLastFingerprint = string.Empty;

        // NOTE: BringToFront removed ??steals OS focus. CDP works on background tabs.

        // ?? A11y-first: find editor via selector chain ??
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // ?? Browser exclusive zone: prompt input ??first response turn ??
        using var chatLock = ChromeTabLock.Acquire("ChatGPT");
        if (chatLock == null) return (false, null);

        // Capture stable baseline AFTER lock — DOM fully settled, no stale prior-session answer.
        // Fixes: WKAppBot reading an old unread GPT reply as the new response (off-by-1 DOM timing).
        prevTurns = await CountChatGptTurns(cdp);
        prevLastFingerprint = await cdp.EvalAsync(
            "(() => { var els = document.querySelectorAll('[data-message-author-role=\"assistant\"]');" +
            " return els.length > 0 ? (els[els.length-1].textContent?.substring(0,80) ?? '') : ''; })()") ?? string.Empty;
        Console.WriteLine($"[ASK] Pre-send baseline: turns={prevTurns} tail=[{prevLastFingerprint[..Math.Min(50, prevLastFingerprint.Length)]}]");

        // ?? CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ??
        var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "ChatGPT");

        // ?? File attachments (before text) ??
        if (attachFiles?.Count > 0)
            await AttachFilesViaCdp(cdp, attachFiles, editorSel);

        // Tier 1: focusless insert (a11y-first)
        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        // Verify what's actually in the editor
        var editorContent = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
        Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
        if (!inserted)
        {
            // ?? Tier 2: CDP Input.insertText (needs focus) ??
            Console.WriteLine("[ASK] Focusless insert failed, trying CDP Input.insertText...");
            await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
            await Task.Delay(100);
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyDown", ["key"] = "a", ["code"] = "KeyA",
                ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2 // Ctrl
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyUp", ["key"] = "a", ["code"] = "KeyA",
                ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
            });
            await Task.Delay(50);
            await cdp.SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject
            {
                ["text"] = message
            });
            await Task.Delay(200);

            var verify = await cdp.EvalAsync(
                $"document.querySelector('{editorSel}')?.textContent?.length ?? 0") ?? "0";
            if (verify == "0")
            {
                Console.WriteLine("[ASK] Failed to insert text");
                return (false, null);
            }
        }

        // ?? Send: JS click ??verify ??CDP Enter fallback ??
        // With file attachments, wait for send button to become enabled
        if (attachFiles?.Count > 0)
        {
            for (int bw = 0; bw < 10; bw++)
            {
                var btnState = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[data-testid="send-button"]')
                               || document.querySelector('button[aria-label*="蹂대궡湲?]')
                               || document.querySelector('button[aria-label*="Send"]');
                        return btn ? (btn.disabled ? 'DISABLED' : 'ENABLED') : 'NOT_FOUND';
                    })()
                    """) ?? "NOT_FOUND";
                if (btnState == "ENABLED") break;
                Console.WriteLine($"[ASK] Send button: {btnState}, waiting...");
                await Task.Delay(1000);
            }
        }
        await Task.Delay(500);

        // Stop button visible = previous response still streaming → wait before sending
        if (!await WaitWhileStopButtonVisible(cdp, maxWaitMs: 60000))
            return (false, null);

        var sendResult = "PENDING";

        // Use turn count for send verification when image is attached
        // (textContent check is unreliable with image attachments)
        int preSendTurns = await CountChatGptTurns(cdp);

        // Tier 1: JS click (works minimized, but React may ignore .click())
        var jsClick = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="send-button"]')
                       || document.querySelector('button[aria-label*="蹂대궡湲?]')
                       || document.querySelector('button[aria-label*="Send"]');
                if (!btn || btn.disabled) return 'NO_BTN';
                btn.click();
                return 'CLICKED';
            })()
            """) ?? "NO_BTN";

        if (jsClick == "CLICKED")
        {
            await Task.Delay(1000);
            // Verify send: turn count increase OR editor emptied
            var postTurns = await CountChatGptTurns(cdp);
            if (postTurns > preSendTurns)
            {
                sendResult = "JS_CLICK";
            }
            else
            {
                var remaining = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                sendResult = remaining == "0" ? "JS_CLICK" : "CLICK_NOOP";
            }
        }

        // Tier 2: UIA Invoke on send button (focusless, works minimized)
        if (sendResult != "JS_CLICK")
        {
            if (!await WaitWhileStopButtonVisible(cdp))
                return (false, null);
            Console.WriteLine("[ASK] JS click didn't send, trying UIA invoke...");
            if (TryUiaInvokeSendButton())
            {
                await Task.Delay(1000);
                var postTurns = await CountChatGptTurns(cdp);
                if (postTurns > preSendTurns)
                    sendResult = "UIA_INVOKE";
                else
                {
                    var remaining = await cdp.EvalAsync(
                        $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                    sendResult = remaining == "0" ? "UIA_INVOKE" : "UIA_NOOP";
                }
            }
        }

        // Tier 3: CDP Enter key (needs visible viewport + editor focus)
        if (sendResult != "JS_CLICK" && sendResult != "UIA_INVOKE")
        {
            await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
            await Task.Delay(100);
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
            sendResult = "CDP_ENTER";
        }

        // Zoom feedback + focus guard
        zoom?.ShowPass($"sent ({sendResult})");
        zoom?.Dispose();
        GuardCdpFocusTheft(cdp, prevFg, "input-cdp");
        LogRestoreFocus(prevFg, "after-send-GPT");

        // Check editor after send ??should be empty if sent successfully
        var afterSend = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
        Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={prevTurns})");
        if (returnAfterSend)
        {
            chatLock.Release("queued-no-wait");
            return (true, BuildNoWaitQueuedMessage("ChatGPT"));
        }

        // Wait for response start: turn count increase OR streaming/thinking indicators
        // Uses querySelectorAll + textContent (works in background tabs without layout)
        var sw = Stopwatch.StartNew();
        bool responseStarted = false;
        Console.Write("[SERVER-WAIT]");
        while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 30))
        {
            await Task.Delay(1000);

            // URL change detection (new conversation resets turn count)
            var newUrl = await cdp.EvalAsync("location.href") ?? "";
            if (newUrl != currentUrl && newUrl.Contains("/c/"))
            {
                Console.WriteLine();
                Console.WriteLine("[ASK] New conversation detected");
                prevTurns = 0;
                currentUrl = newUrl;
            }

            // Multi-signal detection: turn count OR stop button OR thinking marker
            var detectResult = await cdp.EvalAsync(
                "(() => {" +
                "var turns = document.querySelectorAll('[data-message-author-role=\"assistant\"]').length;" +
                "if (turns === 0) turns = Math.floor((document.querySelectorAll('[data-testid*=\"conversation-turn\"]').length || document.querySelectorAll('article').length) / 2);" +
                $"if (turns > {prevTurns}) return 'TURN_' + turns;" +
                "var stop = document.querySelector('button[data-testid=\"stop-button\"]')" +
                "|| document.querySelector('button[aria-label=\"Stop streaming\"]')" +
                "|| document.querySelector('button[aria-label=\"\\uc2a4\\ud2b8\\ub9ac\\ubc0d \\uc911\\uc9c0\"]');" +
                "if (stop) return 'STREAMING';" +
                "if (document.querySelector('.result-thinking')) return 'THINKING';" +
                "return 'WAITING_' + turns;" +
                "})()") ?? "WAITING_0";

            // Show meaningful state instead of plain dots
            var waitTag = detectResult.StartsWith("TURN_") ? "TURN" :
                          detectResult == "STREAMING" ? "RUNNING" :
                          detectResult == "THINKING"  ? "THINKING" : ".";
            Console.Write($" {waitTag}"); Console.Out.Flush();

            if (detectResult.StartsWith("TURN_") || detectResult == "STREAMING" || detectResult == "THINKING")
            {
                responseStarted = true;
                Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
                Console.WriteLine($"[ASK] Response detected: {detectResult}");
                chatLock.Release("first-byte");
                RegisterWaitingTab("chatgpt", cdp);
                await HandoffTabToPeer("chatgpt");
                break;
            }

            if (sw.Elapsed.TotalSeconds > 3)
                Console.WriteLine($"[ASK] Waiting for response... ({detectResult}, {sw.Elapsed.TotalSeconds:F0}s)");
        }
        if (!responseStarted)
        {
            Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
            Console.WriteLine("[ASK] No response detected");
            return (false, null);
        }

        // ?? Poll Phase 1: wait for streaming/thinking to finish (iconified ??no rendering needed) ??
        // Live flush: print new text as it arrives during streaming
        int streamExtensions = 0;
        int blankPageCount = 0;
        int lastFlushedLen = 0;
        bool liveHeaderPrinted = false;
        var knownImageUrls = new HashSet<string>();
        var savedImages = new List<string>();
        var lastFlushTime = DateTime.UtcNow;
        sw.Restart();

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(1500);

            // Combined check: state + streaming text length + delta text for live flush
            var stateJson = await cdp.EvalAsync($$"""
                (() => {
                    if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100)
                        return JSON.stringify({s:'BLANK',len:0,delta:''});
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label="Stop streaming"]')
                            || document.querySelector('button[aria-label="?ㅽ듃由щ컢 以묒?"]');
                    var thinking = !!document.querySelector('.result-thinking');
                    // Detect DALL-E image generation in progress (spinner/progress within assistant message)
                    var lastAssist = document.querySelector('[data-message-author-role="assistant"]:last-of-type')
                                  || document.querySelector('article:last-of-type');
                    var imgGen = lastAssist && (
                        !!lastAssist.querySelector('[role="progressbar"]')
                        || !!lastAssist.querySelector('.animate-spin')
                        || !!lastAssist.querySelector('svg.animate-spin')
                        || !!lastAssist.querySelector('[data-testid="image-gen-progress"]')
                        || !!(lastAssist.textContent||'').match(/(?:image|creating|generating)/i)
                    );
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    var txt = msgs.length > 0 ? (msgs[msgs.length-1].textContent||'').trim() : '';
                    var hasText = txt.length > 0;
                    var state;
                    if (!stop && !thinking && !imgGen) state = hasText ? 'DONE' : 'DONE_EMPTY';
                    else if (imgGen) state = 'IMG_GEN';
                    else if (thinking) state = hasText ? 'THINKING_HAS_TEXT' : 'THINKING';
                    else state = hasText ? 'STREAMING_HAS_TEXT' : 'STREAMING';
                    var delta = txt.length > {{lastFlushedLen}} ? txt.substring({{lastFlushedLen}}) : '';
                    return JSON.stringify({s:state,len:txt.length,delta:delta});
                })()
                """) ?? "";

            // Parse combined response
            string state = "";
            int textLen = 0;
            string delta = "";
            try
            {
                var jo = JsonDocument.Parse(stateJson).RootElement;
                state = jo.GetProperty("s").GetString() ?? "";
                textLen = jo.GetProperty("len").GetInt32();
                delta = jo.GetProperty("delta").GetString() ?? "";
            }
            catch
            {
                state = stateJson; // fallback: treat as plain state string
            }

            // Live flush: print new text delta
            if (delta.Length > 0)
            {
                if (!liveHeaderPrinted)
                {
                    Console.WriteLine();
                    Console.WriteLine("?? ChatGPT (streaming) ??");
                    liveHeaderPrinted = true;
                }
                Console.Write(delta);
                Console.Out.Flush();
                lastFlushedLen = textLen;
                lastFlushTime = DateTime.UtcNow;
            }

            // Early-exit: flush stopped for 1s and we have text → treat as DONE (don't wait for stop button)
            bool isThinkingState = state == "THINKING" || state == "THINKING_HAS_TEXT";
            if (lastFlushedLen > 50 && state != "DONE" && state != "IMG_GEN"
                && !isThinkingState && (DateTime.UtcNow - lastFlushTime).TotalSeconds >= 1.0)
            {
                if (liveHeaderPrinted) Console.WriteLine();
                Console.WriteLine($"[ASK] Flush idle 1s → early done ({sw.Elapsed.TotalSeconds:F0}s)");
                break;
            }

            // Detect generated images in response (inline marker output)
            var newImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
            savedImages.AddRange(newImages);

            // Image generation in progress ??keep waiting (up to 90s for DALL-E)
            if (state == "IMG_GEN")
            {
                if (!liveHeaderPrinted)
                {
                    Console.WriteLine();
                    Console.WriteLine("?? ChatGPT (streaming) ??");
                    liveHeaderPrinted = true;
                }
                Console.Write(".");
                Console.Out.Flush();
                continue;
            }

            if (state == "DONE" || state == "DONE_EMPTY")
            {
                // If very little text and early in stream, wait a bit more
                // (ChatGPT shows "?대?吏 遺꾩꽍 以? then goes idle briefly before actual response)
                if (textLen < 50 && sw.Elapsed.TotalSeconds < 60)
                {
                    if (textLen > 0) Console.Write(".");
                    Console.Out.Flush();
                    continue; // keep polling ??likely just a transient idle gap
                }
                // Final image check before completion
                var finalImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
                savedImages.AddRange(finalImages);
                if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                Console.WriteLine($"[ASK] Streaming complete ({sw.Elapsed.TotalSeconds:F0}s)");
                if (savedImages.Count > 0)
                    Console.WriteLine($"[ASK] Downloaded {savedImages.Count} generated image(s)");
                break;
            }

            // Blank/broken page detection — ChatGPT navigates to /c/UUID after first message,
            // causing brief BLANK during page load. Tolerate more blanks early on (navigation window).
            if (state == "BLANK" || string.IsNullOrEmpty(state))
            {
                blankPageCount++;
                int blankLimit = sw.Elapsed.TotalSeconds < 30 ? 20 : 3; // GPT stays blank while navigating to /c/UUID
                Console.WriteLine($"[ASK] Page blank/navigating ({blankPageCount}/{blankLimit}), {sw.Elapsed.TotalSeconds:F0}s");
                if (blankPageCount >= blankLimit)
                {
                    Console.WriteLine("[ASK] Page unresponsive — aborting poll");
                    break;
                }
                continue;
            }

            blankPageCount = 0; // reset on valid response

            // First-byte timeout: 20s of streaming with no text ??likely stuck
            // Exempt: THINKING state (o3/o4 models can think for 30s+ before first text)
            bool isThinking = state == "THINKING" || state == "THINKING_HAS_TEXT";
            bool hasResponseText = state == "STREAMING_HAS_TEXT" || state == "THINKING_HAS_TEXT";

            // Streaming handoff: response text appearing ??give active tab to peer
            if (hasResponseText)
                await HandoffTabToPeer("chatgpt");

            if (!hasResponseText && !isThinking && lastFlushedLen == 0 && sw.Elapsed.TotalSeconds >= 20)
            {
                Console.WriteLine("[ASK] No response text after 20s ??aborting (first-byte timeout)");
                break;
            }

            // Poll status: FLUSH when delta arrived, RUNNING when streaming without delta, THINKING for o3+
            if (delta.Length > 0)
                Console.Write($" [FLUSH+{delta.Length}]");
            else if (isThinking)
                Console.Write($" [THINKING {sw.Elapsed.TotalSeconds:F0}s]");
            else if (!liveHeaderPrinted)
                Console.Write($" [RUNNING {sw.Elapsed.TotalSeconds:F0}s]");
            Console.Out.Flush();

            // Extend timeout while actively streaming/thinking
            // Thinking mode (o1/o3/o4): unlimited extensions ??can think for minutes
            // Streaming: max 1 extension (2x original timeout)
            int maxExtensions = isThinking ? 99 : 1;
            if (sw.Elapsed.TotalSeconds > timeoutSec * 0.8 && streamExtensions < maxExtensions)
            {
                streamExtensions++;
                Console.WriteLine($"[ASK] Still {(isThinking ? "thinking" : "streaming")}, extending timeout... (ext #{streamExtensions})");
                // ext #1: window may be hidden/minimized → restore + bring to front
                if (streamExtensions == 1)
                    await TryRecoverChatGptTabAsync(cdp, $"thinking ext#{streamExtensions}");
                sw.Restart();
            }
        }

        // ?? Poll Phase 2: text extraction ??
        // NOTE: BringToFront removed ??innerText works on background tabs too.
        await Task.Delay(300);

        // Scroll into view + hydrate + extract
        string? finalText = null;
        for (int extractAttempt = 0; extractAttempt < 3; extractAttempt++)
        {
            await cdp.EvalAsync("""
                (() => {
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    if (msgs.length > 0) msgs[msgs.length-1].scrollIntoView({block:'end',behavior:'instant'});
                })()
                """);
            await Task.Delay(300); // let React hydrate after scroll

            var extractJson = await cdp.EvalAsync("""
                (() => {
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    if (msgs.length === 0) return '';
                    var last = msgs[msgs.length - 1];

                    var md = last.querySelector('.markdown:not(.result-thinking):not(.result-thinking-content)')
                          || last.querySelector('.result-streaming:not(.result-thinking)')
                          || last.querySelector('.markdown:not(.result-thinking):not(.result-thinking-content)');

                    var txt = md ? (md.innerText || md.textContent || '') : '';
                    if (!txt && !last.querySelector('.result-thinking')) txt = last.innerText || last.textContent || '';

                    // Fallback: innerHTML ??strip tags
                    if (!txt && md && md.innerHTML) {
                        var tmp = document.createElement('div');
                        tmp.innerHTML = md.innerHTML;
                        txt = tmp.innerText || tmp.textContent || '';
                    }
                    // Fallback: Selection API
                    if (!txt && md) {
                        try { var r = document.createRange(); r.selectNodeContents(md); txt = r.toString(); r.detach(); } catch(e) {}
                    }
                    return txt?.trim() || '';
                })()
                """) ?? "";

            if (!string.IsNullOrWhiteSpace(extractJson))
            {
                finalText = extractJson;
                break;
            }
            // Hydration retry
            if (extractAttempt < 2)
                await Task.Delay(500);
        }

        if (!string.IsNullOrEmpty(finalText))
        {
            Console.WriteLine($"[ASK] Response ({finalText.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");

            // Done ??hand off tab to peer if still waiting
            await HandoffTabToPeer("chatgpt");

            // ?? Final answer ready: focusless restore to show answer to user ??
            ShowChromeAnswer(cdp);

            return (true, finalText);
        }
        Console.WriteLine("[ASK] Timeout -- no response");
        return (false, null);
    }

    /// <summary>
    /// 理쒖쥌 ?듬? ?쒖떆: Chrome???ъ빱?ㅻ━??由ъ뒪?좎뼱?섏뿬 ?ъ슜?먯뿉寃??듬? ?섏씠吏瑜?蹂댁뿬以??
    /// ask ?뚮줈???꾩껜???꾩씠肄섑솕 ?곹깭濡?吏꾪뻾 (CDP??誘몃땲留덉씠利덉뿉???뺤긽 ?숈옉).
    /// 理쒖쥌 ?듬? 異붿텧 ?꾩뿉留??몄텧?섏뿬 寃곌낵瑜??쒓컖?곸쑝濡??뺤씤?????덇쾶 ?쒕떎.
    /// </summary>
    static void ShowChromeAnswer(CdpClient cdp)
    {
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero) return;

        bool isIconic  = NativeMethods.IsIconic(chromeHwnd);
        bool isVisible = NativeMethods.IsWindowVisible(chromeHwnd);
        if (!isIconic && isVisible) return; // already normal-visible

        var title = WKAppBot.Win32.Window.WindowFinder.GetWindowText(chromeHwnd);
        Console.WriteLine($"[ASK] Restoring Chrome window (iconic={isIconic}, visible={isVisible}): \"{title}\"");
        // SW_RESTORE(9): restores minimized or hidden window, may steal focus (recovery use)
        NativeMethods.ShowWindow(chromeHwnd, 9); // SW_RESTORE
    }

    static async Task TryRecoverChatGptTabAsync(CdpClient cdp, string reason)
    {
        Console.WriteLine($"[ASK] Recovery: {reason} → restore Chrome + bring tab to front");
        ShowChromeAnswer(cdp);
        await cdp.BringTabToFrontAsync();
    }

    // ?? UIA Send Button ??
    // Tier 2 fallback: find and invoke the send button via UIA (completely focusless).
    // Searches Chrome/ChatGPT windows for buttons matching send-related names.
    static readonly string[] SendButtonNames = ["Send message", "Send", "Submit"];
    static readonly string[] StopButtonKeywords = ["Stop", "중지", "스트리밍 중지"];

    static async Task<bool> WaitWhileStopButtonVisible(CdpClient cdp, int maxWaitMs = 12000)
    {
        var sw = Stopwatch.StartNew();
        bool headerPrinted = false;
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            var stopVisible = await cdp.EvalAsync("""
                (() => {
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label*="Stop"]')
                            || document.querySelector('button[aria-label*="중지"]');
                    return stop ? '1' : '0';
                })()
                """) ?? "0";
            if (stopVisible != "1")
            {
                if (headerPrinted) Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
                return true;
            }

            if (!headerPrinted) { Console.Write("[STOP-BTN-WAIT]"); headerPrinted = true; }
            Console.Write("."); Console.Out.Flush();
            await Task.Delay(700);
        }

        Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
        // Timed out — click stop to cancel ongoing generation, then wait briefly and proceed
        Console.WriteLine("[ASK] GPT stop still visible — clicking stop to cancel generation...");
        await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="stop-button"]')
                       || document.querySelector('button[aria-label*="Stop"]')
                       || document.querySelector('button[aria-label*="중지"]');
                if (btn) btn.click();
            })()
            """);
        await Task.Delay(1500);
        return true;
    }

    static bool TryUiaInvokeSendButton()
    {
        try
        {
            using var automation = new UIA3Automation();
            // Try ChatGPT PWA first, then Chrome
            foreach (var grap in new[] { "*ChatGPT*", "*Gemini*Chrome*", "*chrome*" })
            {
                var resolved = GrapHelper.ResolveFullGrap(grap, automation);
                if (resolved == null || resolved.Value.error != null) continue;
                var (_, root, _) = resolved.Value;

                foreach (var name in SendButtonNames)
                {
                    var btn = GrapHelper.FindByNameOrAid(root, name);
                    if (btn == null) continue;

                    // Must be a Button with Invoke pattern
                    if (btn.ControlType != FlaUI.Core.Definitions.ControlType.Button) continue;
                    if (StopButtonKeywords.Any(k =>
                        !string.IsNullOrWhiteSpace(btn.Name) &&
                        btn.Name.Contains(k, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"[ASK] UIA: skip stop-like button \"{btn.Name}\"");
                        continue;
                    }
                    var invoke = btn.Patterns.Invoke.PatternOrDefault;
                    if (invoke == null) continue;

                    Console.WriteLine($"[ASK] UIA: found \"{btn.Name}\" ({btn.ControlType})");
                    invoke.Invoke();
                    Console.WriteLine("[ASK] UIA: Invoked!");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] UIA send failed: {ex.Message}");
        }
        return false;
    }

    // ?? Tab Handoff: disabled ??
    // BringToFront actually steals OS focus (not just Chrome-internal).
    // CDP eval/insertText work fine on background tabs, so handoff is unnecessary.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, CdpClient> _waitingTabs = new();

    /// <summary>
    /// Register this AI's tab as "waiting for response" ??eligible for tab handoff.
    /// </summary>
    static void RegisterWaitingTab(string aiName, CdpClient cdp)
    {
        _waitingTabs[aiName] = cdp;
    }

    /// <summary>
    /// Unregister on completion.
    /// </summary>
    static void UnregisterWaitingTab(string aiName)
    {
        _waitingTabs.TryRemove(aiName, out _);
    }

    /// <summary>
    /// Tab handoff disabled ??BringToFront steals OS focus.
    /// CDP works fine on background tabs, no handoff needed.
    /// </summary>
    static Task HandoffTabToPeer(string myName) => Task.CompletedTask;

    // ?? Chrome Tab Lock ??
    // Per-AI tab lock: each AI provider (gemini, gpt, claude) gets its own semaphore.
    // This allows Gemini, GPT, and Claude to send/receive in parallel — different tabs,
    // different CDP connections — no shared browser resource that needs serialization.
    // Only operations on the SAME AI tab are serialized (send vs concurrent send).
    // Acquired before prompt input, auto-released after 9s or when first byte arrives.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, SemaphoreSlim>
        _tabSemaphores = new(StringComparer.OrdinalIgnoreCase);

    static SemaphoreSlim GetTabSemaphore(string aiName)
    {
        // Normalize key: "Gemini/persona" → "gemini", "Claude" → "claude", "ChatGPT" → "chatgpt"
        var key = aiName.Split('/')[0].ToLowerInvariant();
        return _tabSemaphores.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
    }

    sealed class ChromeTabLock : IDisposable
    {
        readonly string _aiName;
        readonly SemaphoreSlim _sem;
        readonly Timer _autoRelease;
        int _released;

        ChromeTabLock(string aiName, SemaphoreSlim sem)
        {
            _aiName = aiName;
            _sem = sem;
            // Auto-release after 9 seconds (safety net — prevents deadlock)
            _autoRelease = new Timer(_ => Release("auto-9s"), null, 9000, Timeout.Infinite);
        }

        public static ChromeTabLock? Acquire(string aiName, int timeoutMs = 90000)
        {
            var sem = GetTabSemaphore(aiName);
            Console.WriteLine($"[ASK] Waiting for browser lock ({aiName})...");
            var sw = Stopwatch.StartNew();
            const int sliceMs = 1000;
            bool dotLineOpen = false;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (sem.Wait(sliceMs))
                {
                    if (dotLineOpen) Console.WriteLine();
                    if (sw.ElapsedMilliseconds > 1500)
                        Console.WriteLine($"[ASK] Browser lock acquired after queue wait ({aiName}, {sw.ElapsedMilliseconds}ms)");
                    else
                        Console.WriteLine($"[ASK] Browser lock acquired ({aiName})");
                    return new ChromeTabLock(aiName, sem);
                }

                if (!dotLineOpen)
                {
                    Console.Write($"[ASK] Browser lock queued ({aiName}) ");
                    dotLineOpen = true;
                }
                Console.Write(".");
                Console.Out.Flush();

                if (sw.ElapsedMilliseconds % 5000 < sliceMs)
                {
                    Console.WriteLine($" {sw.ElapsedMilliseconds}ms");
                    dotLineOpen = false;
                }
            }

            if (dotLineOpen) Console.WriteLine();
            Console.WriteLine($"[ASK] Browser lock timeout ({aiName})");
            return null;
        }

        public void Release(string reason = "done")
        {
            if (Interlocked.CompareExchange(ref _released, 1, 0) != 0) return;
            _autoRelease.Dispose();
            try { _sem.Release(); } catch { }
            Console.WriteLine($"[ASK] Browser lock released ({_aiName}, {reason})");
        }

        public void Dispose() => Release("dispose");
    }
}

