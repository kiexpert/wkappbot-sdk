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
        "(4) Only call tools listed in the provided tool schema. Use exact argument names. Do not invent parameters. Before calling, validate all required args match the JSON schema. If a required arg is missing, ask ??do not guess. " +
        "(5) Wait for tool_result messages before continuing reasoning. One tool call per turn unless tools are explicitly designed for parallel use. " +
        "(6) Be concise and action-oriented. Prefer structured actions over explanations. " +
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

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab, List<string>? attachFiles = null, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, bool triadMode = false, string? modelHint = null, bool noWait = false)
    {
        Console.WriteLine($"[ASK] ChatGPT: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] ChatGPT model hint: {modelHint}");

        var targetTag = BuildAskTargetTag("gpt");
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        { var prevFgGpt = NativeMethods.GetForegroundWindow(); Console.WriteLine($"[ASK:FOCUS] pre-activate fg={prevFgGpt:X8}"); if (!newTab) cdp.ActivateTabAsync().GetAwaiter().GetResult(); LogRestoreFocus(prevFgGpt, "ActivateTab-GPT"); }

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

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
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint), timeoutSec: 20);
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
                await Task.Delay(1000); // Let ChatGPT UI settle

                // Send the actual question (with 9s-delay retry on timeout)
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, attachFiles, returnAfterSend: noWait);
                if (!ok && string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("[ASK] ChatGPT timeout ??retrying in 9 seconds...");
                    await Task.Delay(9000);
                    (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, returnAfterSend: noWait);
                }
                if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
                    (ok, answer) = await RunChatGptLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry);
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

            if (slackReport)
                ReportToSlack("ChatGPT", question, answer);
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
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ChatGptEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
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
        int prevTurns = await CountChatGptTurns(cdp);

        // NOTE: BringToFront removed ??steals OS focus. CDP works on background tabs.

        // ?? A11y-first: find editor via selector chain ??
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // ?? Browser exclusive zone: prompt input ??first response turn ??
        using var chatLock = ChromeTabLock.Acquire("ChatGPT");
        if (chatLock == null) return (false, null);

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
        while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 30))
        {
            await Task.Delay(1000);

            // URL change detection (new conversation resets turn count)
            var newUrl = await cdp.EvalAsync("location.href") ?? "";
            if (newUrl != currentUrl && newUrl.Contains("/c/"))
            {
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

            if (detectResult.StartsWith("TURN_") || detectResult == "STREAMING" || detectResult == "THINKING")
            {
                responseStarted = true;
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
                        || !!(lastAssist.textContent||'').match(/(?:?대?吏|image|?ъ쭊|洹몃┝|?앹꽦|creating|generating)/i)
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

            // Blank/broken page detection ??bail out early
            if (state == "BLANK" || string.IsNullOrEmpty(state))
            {
                blankPageCount++;
                Console.WriteLine($"[ASK] Page blank/broken ({blankPageCount}/3), {sw.Elapsed.TotalSeconds:F0}s");
                if (blankPageCount >= 3)
                {
                    Console.WriteLine("[ASK] Page unresponsive ??aborting poll");
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

            // Only print poll status when NOT live-flushing (avoid noise)
            if (!liveHeaderPrinted)
                Console.WriteLine($"[ASK] Poll: {(isThinking ? "thinking" : "streaming")}{(hasResponseText ? "+" : "")}, {sw.Elapsed.TotalSeconds:F0}s");

            // Extend timeout while actively streaming/thinking
            // Thinking mode (o1/o3/o4): unlimited extensions ??can think for minutes
            // Streaming: max 1 extension (2x original timeout)
            int maxExtensions = isThinking ? 99 : 1;
            if (sw.Elapsed.TotalSeconds > timeoutSec * 0.8 && streamExtensions < maxExtensions)
            {
                streamExtensions++;
                Console.WriteLine($"[ASK] Still {(isThinking ? "thinking" : "streaming")}, extending timeout... (ext #{streamExtensions})");
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
        if (chromeHwnd == IntPtr.Zero || !NativeMethods.IsIconic(chromeHwnd))
            return; // not minimized ??already visible

        var title = WKAppBot.Win32.Window.WindowFinder.GetWindowText(chromeHwnd);
        Console.WriteLine($"[ASK] Showing answer ??focusless restore \"{title}\"");

        // Focusless restore: SW_SHOWNOACTIVATE won't steal focus
        NativeMethods.ShowWindow(chromeHwnd, 4); // SW_SHOWNOACTIVATE
    }

    // ?? UIA Send Button ??
    // Tier 2 fallback: find and invoke the send button via UIA (completely focusless).
    // Searches Chrome/ChatGPT windows for buttons matching send-related names.
    static readonly string[] SendButtonNames = ["Send message", "Send", "Submit"];
    static readonly string[] StopButtonKeywords = ["Stop", "중지", "스트리밍 중지"];

    static async Task<bool> WaitWhileStopButtonVisible(CdpClient cdp, int maxWaitMs = 12000)
    {
        var sw = Stopwatch.StartNew();
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
                return true;

            Console.WriteLine($"[ASK] Stop button visible; waiting before send... ({sw.ElapsedMilliseconds}ms)");
            await Task.Delay(700);
        }

        Console.WriteLine("[ASK] Stop button still visible after wait; aborting fallback send.");
        return false;
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
    // Cross-process mutex: only one ask command can own the browser at a time.
    // Acquired before prompt input, auto-released after 9s or when first byte arrives.
    // IDisposable ??always safe, no manual release needed.

    // Semaphore (not Mutex) ??async/await can resume on different threads,
    // SemaphoreSlim: in-process, thread-agnostic release (timer callback on any thread is fine),
    // and always starts fresh when Eye restarts ??no named OS resource that can get stuck
    // after a crash (unlike named Semaphore which has no AbandonedMutexException).
    static readonly SemaphoreSlim ChromeTabSemaphore = new(1, 1);

    sealed class ChromeTabLock : IDisposable
    {
        readonly string _aiName;
        readonly Timer _autoRelease;
        int _released;

        ChromeTabLock(string aiName)
        {
            _aiName = aiName;
            // Auto-release after 9 seconds (safety net ??prevents deadlock)
            _autoRelease = new Timer(_ => Release("auto-9s"), null, 9000, Timeout.Infinite);
        }

        public static ChromeTabLock? Acquire(string aiName, int timeoutMs = 90000)
        {
            Console.WriteLine($"[ASK] Waiting for browser lock ({aiName})...");
            var sw = Stopwatch.StartNew();
            const int sliceMs = 1000;
            bool dotLineOpen = false;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (ChromeTabSemaphore.Wait(sliceMs))
                {
                    if (dotLineOpen) Console.WriteLine();
                    if (sw.ElapsedMilliseconds > 1500)
                        Console.WriteLine($"[ASK] Browser lock acquired after queue wait ({aiName}, {sw.ElapsedMilliseconds}ms)");
                    else
                        Console.WriteLine($"[ASK] Browser lock acquired ({aiName})");
                    return new ChromeTabLock(aiName);
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
            try { ChromeTabSemaphore.Release(); } catch { }
            Console.WriteLine($"[ASK] Browser lock released ({_aiName}, {reason})");
        }

        public void Dispose() => Release("dispose");
    }
}

