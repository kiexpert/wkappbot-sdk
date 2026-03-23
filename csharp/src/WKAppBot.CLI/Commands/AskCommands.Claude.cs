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

    // ── Claude.ai ──

    // Claude.ai uses ProseMirror editor — innerHTML/execCommand fail, must use ClipboardEvent paste
    static readonly string[] ClaudeEditorSelectors =
    [
        "div.tiptap.ProseMirror",                          // Claude.ai ProseMirror (no attr filter — most reliable)
        "div.tiptap.ProseMirror[contenteditable='true']",  // With attr (single-quote in CSS — safe in JS double-quoted eval)
        ".ProseMirror[contenteditable='true']",            // ProseMirror generic
        "[contenteditable='true']",                        // Generic contenteditable
        "[contenteditable]",                               // contenteditable without value check
        "[data-testid='composer-input']",                  // Claude.ai composer testid
        "[data-testid='user-input']",                      // alt testid
        "textarea",                                        // fallback if Claude.ai switched to textarea
    ];

    static async Task<string?> WaitForClaudeEditorA11y(CdpClient cdp)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ClaudeEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector(\"{sel}\") ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        // Diagnostic: log what input-like elements exist on the page
        var diag = await cdp.EvalAsync("""
            (() => {
                var ce = document.querySelectorAll('[contenteditable]').length;
                var pm = document.querySelectorAll('.ProseMirror').length;
                var ta = document.querySelectorAll('textarea').length;
                var rb = document.querySelectorAll('[role="textbox"]').length;
                return 'ce=' + ce + ' pm=' + pm + ' ta=' + ta + ' textbox=' + rb;
            })()
            """) ?? "unknown";
        Console.WriteLine($"[ASK] Claude editor not found (selector chain exhausted) | dom: {diag}");
        return null;
    }

    /// <summary>Insert text into Claude.ai editor. Supports ProseMirror (ClipboardEvent paste) and textarea (value setter).</summary>
    static async Task<bool> InsertTextClaudeProseMirror(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");

        // Detect element type first so we can choose the right injection strategy
        var tagName = await cdp.EvalAsync(
            $"document.querySelector(\"{selector}\")?.tagName?.toLowerCase() ?? 'unknown'") ?? "unknown";

        if (tagName == "textarea")
        {
            // Textarea: use native value setter + input/change events (React-compatible)
            var taResult = await cdp.EvalAsync($$"""
                (() => {
                    var el = document.querySelector("{{selector}}");
                    if (!el) return 'NOT_FOUND';
                    el.focus();
                    var setter = Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype, 'value')?.set;
                    if (setter) setter.call(el, '{{escaped}}');
                    else el.value = '{{escaped}}';
                    el.dispatchEvent(new Event('input', {bubbles: true}));
                    el.dispatchEvent(new Event('change', {bubbles: true}));
                    return el.value.length > 0 ? 'OK' : 'EMPTY';
                })()
                """);
            if (taResult == "OK") return true;
            Console.WriteLine($"[ASK] Textarea native setter result: {taResult}, trying Input.insertText...");
            await cdp.FocusAsync(selector);
            await Task.Delay(100);
            await cdp.SendAsync("Input.insertText", new JsonObject { ["text"] = text });
            await Task.Delay(200);
            var taVerify = await cdp.EvalAsync(
                $"(document.querySelector(\"{selector}\")?.value?.length ?? 0).toString()") ?? "0";
            return taVerify != "0";
        }

        // ProseMirror / contenteditable: ClipboardEvent paste
        var result = await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector("{{selector}}");
                if (!el) return 'NOT_FOUND';
                el.focus();
                var dt = new DataTransfer();
                dt.setData('text/plain', '{{escaped}}');
                var pe = new ClipboardEvent('paste', {clipboardData: dt, bubbles: true, cancelable: true});
                el.dispatchEvent(pe);
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """);
        if (result == "OK") return true;

        // Fallback: CDP Input.insertText
        Console.WriteLine($"[ASK] ClipboardEvent paste result: {result}, trying Input.insertText...");
        await cdp.FocusAsync(selector);
        await Task.Delay(100);
        await cdp.SendAsync("Input.insertText", new JsonObject { ["text"] = text });
        await Task.Delay(200);
        var verify = await cdp.EvalAsync(
            $"(document.querySelector(\"{selector}\")?.value?.length ?? document.querySelector(\"{selector}\")?.textContent?.length ?? 0).toString()") ?? "0";
        return verify != "0";
    }

    /// <summary>Count Claude.ai assistant turns.</summary>
    static async Task<int> CountClaudeTurns(CdpClient cdp)
    {
        var result = await cdp.EvalAsync("""
            (() => {
                var c = document.querySelectorAll('[data-testid="user-message"]').length;
                if (c > 0) return '' + c;
                c = document.querySelectorAll('[data-is-streaming]').length;
                if (c > 0) return '1';
                return '0';
            })()
            """) ?? "0";
        return int.TryParse(result, out var v) ? v : 0;
    }

    static int AskClaude(string question, bool slackReport = true, int timeoutSec = 30, bool newTab = false, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, int loopMaxParallel = 7, bool triadMode = false, string? modelHint = null, bool noWait = false, string? targetTagOverride = null, string? linePrefix = null, TriadSharedContext? triadCtx = null)
    {
        using var _ = ApplyOutputPrefix(linePrefix);
        Console.WriteLine($"[ASK] Claude: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Claude model hint: {modelHint}");

        PulseStep.Init("ask-claude");
        var targetTag = targetTagOverride ?? BuildSandboxKey("ask", "claude");
        var cdp = EnsureCdpConnection(preferredHost: "claude.ai", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;
        PulseStep.Mark("cdp-connected");

        // No tab activation — CDP works on background tabs via targetId. Truly focusless.

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        EnsureSlackThread("Claude", question);

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate ──
                PulseStep.Mark("phase1-navigate");
                var currentUrl = await cdp.GetUrlAsync() ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("claude.ai"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session — navigating to fresh Claude..." : "[ASK] Navigating to Claude...");
                    await cdp.NavigateAsync("https://claude.ai/new");
                    await Task.Delay(3000);
                }
                else
                {
                    Console.WriteLine($"[ASK] Reusing Claude session");
                }

                // NOTE: BringToFront removed — steals OS focus. CDP works on background tabs.
                await Task.Delay(1000);

                // ── Phase 2: Find editor ──
                var editorSel = await WaitForClaudeEditorA11y(cdp);
                if (editorSel == null)
                    return (false, (string?)null);
                PulseStep.Mark("editor-found");
                Console.WriteLine($"[ASK] Editor found: {editorSel}");

                // ── Phase 3: Check existing turns ──
                int existingTurns = await CountClaudeTurns(cdp);
                if (existingTurns > 0)
                    Console.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                var hasLoopPersonaState = await HasLoopPersonaStateAsync(cdp, "claude");
                var effectiveLoopPersona = loopMode || hasLoopPersonaState;
                Console.WriteLine($"[ASK] Loop persona state: {(hasLoopPersonaState ? "present" : "missing")}");
                if (!loopMode && hasLoopPersonaState)
                    Console.WriteLine("[ASK] Loop marker found; MCP guidance will be included for fresh session persona.");

                if (existingTurns == 0 || (effectiveLoopPersona && !hasLoopPersonaState))
                {
                    Console.WriteLine(existingTurns == 0
                        ? "[ASK] Fresh Claude -- injecting persona..."
                        : "[ASK] Loop persona missing on this tab -- re-injecting persona...");
                    var personaTextClaude = BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint);
                    if (Interlocked.CompareExchange(ref _slackPersonaPostedFlag, 1, 0) == 0)
                        SlackPostToThread($"📋 *[persona]* steps={loopMaxSteps} retry={loopRetry}\n```\n{(personaTextClaude.Length > 800 ? personaTextClaude[..800] + "…" : personaTextClaude)}\n```", "System");
                    var (personaOk, personaResp) = await ClaudeSendAndWaitAsync(
                        cdp,
                        personaTextClaude,
                        timeoutSec: 20);
                    if (!personaOk)
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                    else
                    {
                        bool ready = (personaResp ?? "").Contains("READY", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : personaResp?.Substring(0, Math.Min(50, personaResp.Length)))}");
                        if (IsClaudeLimitResponse(personaResp))
                        {
                            Console.WriteLine("[ASK] Claude limit detected during persona. Fast-fail.");
                            return (false, personaResp);
                        }
                        if (effectiveLoopPersona)
                            await SetLoopPersonaStateAsync(cdp, "claude");
                        // Persona continuation may already contain a tool call — skip question send
                        // Verify there is at least one parseable tool call (not just marker text in explanation)
                        if (effectiveLoopPersona && (personaResp ?? "").Contains("[APPBOT_TOOL_CALL_BEGIN]")
                            && ParseAllLoopToolCalls(personaResp!).Count > 0)
                        {
                            Console.WriteLine($"[ASK] Persona continuation has tool call ({personaResp!.Length} chars) — skipping question send");
                            return (true, personaResp);
                        }
                    }

                    // Re-acquire editor after persona exchange.
                    editorSel = await WaitForClaudeEditorA11y(cdp);
                    if (editorSel == null)
                        return (false, (string?)null);

                    // Prepend host handshake proof for fresh loop sessions so Claude trusts the host is live
                    if (effectiveLoopPersona)
                        question = BuildHostHandshake() + question;
                }

                // ── Phase 4: Insert text + send ──
                using var chatLock = ChromeTabLock.Acquire("Claude");
                if (chatLock == null) return (false, (string?)null);

                // ── CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ──
                var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "Claude");

                await ClearContentEditable(cdp, editorSel);
                PulseStep.Mark("question-send");
                var inserted = await InsertTextClaudeProseMirror(cdp, editorSel, question);
                var editorContent = await cdp.EvalAsync(
                    $"document.querySelector(\"{editorSel}\")?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
                Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
                if (!inserted)
                {
                    zoom?.ShowFail("insert failed");
                    zoom?.Dispose();
                    Console.WriteLine("[ASK] Failed to insert text into Claude editor");
                    return (false, (string?)null);
                }

                // ── Send ──
                // Wait for any active response to finish (stop button = Claude is generating/tool-running).
                // Clicking the send button during tool execution would interrupt it — Enter key is safer
                // because Claude queues it in the editor without firing until generation completes.
                if (!await WaitWhileStopButtonVisible(cdp, maxWaitMs: 60000))
                    return (false, (string?)null);

                await Task.Delay(500);
                int preSendTurns = await CountClaudeTurns(cdp);
                // Capture existing streaming element count so Phase 5/6 only reacts to NEW elements.
                // Bug: after persona injection, stale [data-is-streaming="false"] from "READY" response
                // would immediately satisfy the DONE check, returning the persona response as the answer.
                int preStreamingCount = await cdp.QueryCountAsync("[data-is-streaming]");
                // Fallback: also track assistant-message count (Claude.ai alternate DOM structure)
                int preAssistantCount = await cdp.QueryCountAsync("[data-testid=\"assistant-message\"]");
                var sendResult = "PENDING";

                // Tier 1: CDP Enter key — queues safely, does NOT interrupt tool execution
                await cdp.FocusAsync(editorSel);
                await Task.Delay(100);
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                    ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                });
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                    ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                });
                await Task.Delay(1000);
                var postTurnsEnter = await CountClaudeTurns(cdp);
                if (postTurnsEnter > preSendTurns)
                    sendResult = "CDP_ENTER";
                else
                {
                    var remaining0 = await cdp.GetTextLengthAsync(editorSel);
                    if (remaining0 == 0) sendResult = "CDP_ENTER";
                }

                // Tier 2: JS click on send button (fallback — only when Enter had no effect)
                if (sendResult == "PENDING")
                {
                    Console.WriteLine("[ASK] Enter key didn't send, trying JS button click...");
                    var jsClick = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('[data-testid="chat-input-grid-area"] button[type="submit"]')
                                   || document.querySelector('[data-testid="chat-input"] button[type="submit"]')
                                   || document.querySelector('button[aria-label="메시지 보내기"]')
                                   || document.querySelector('button[aria-label="Send Message"]')
                                   || document.querySelector('button[aria-label*="Send"]');
                            if (!btn || btn.disabled) return 'NO_BTN';
                            btn.click();
                            return 'CLICKED';
                        })()
                        """) ?? "NO_BTN";

                    if (jsClick == "CLICKED")
                    {
                        await Task.Delay(1000);
                        var postTurns = await CountClaudeTurns(cdp);
                        if (postTurns > preSendTurns)
                            sendResult = "JS_CLICK";
                        else
                        {
                            var remaining = await cdp.GetTextLengthAsync(editorSel);
                            sendResult = remaining == 0 ? "JS_CLICK" : "CLICK_NOOP";
                        }
                    }
                }

                // Zoom feedback + focus guard
                zoom?.ShowPass($"sent ({sendResult})");
                zoom?.Dispose();
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");
                LogRestoreFocus(prevFg, "after-send-Claude");

                var afterSend = await cdp.EvalAsync(
                    $"document.querySelector(\"{editorSel}\")?.textContent?.length ?? -1") ?? "-1";
                Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={preSendTurns})");
                if (noWait)
                {
                    chatLock.Release("queued-no-wait");
                    return (true, BuildNoWaitQueuedMessage("Claude"));
                }

                // ── Phase 5: Wait for response ──
                var sw = Stopwatch.StartNew();
                bool responseStarted = false;
                while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 30))
                {
                    await Task.Delay(1000);

                    var limitText = await cdp.EvalAsync("""
                        (() => {
                            // Only check the LAST AI message to avoid false positives from prior conversation
                            var msgs = document.querySelectorAll('[data-is-streaming]');
                            var t = msgs.length > 0 ? (msgs[msgs.length - 1].innerText || '') : '';
                            // Fallback: check for standalone limit UI banners (not inside chat turns)
                            if (!t) {
                                var banners = document.querySelectorAll('[class*="limit"],[class*="usage"],[class*="quota"]');
                                t = Array.from(banners).map(b => b.innerText).join('\n').substring(0, 800);
                            }
                            var keys = ['usage limit', 'rate limit', 'too many requests', '요청이 너무 많', '사용량 한도'];
                            var tl = t.toLowerCase();
                            for (var i = 0; i < keys.length; i++) {
                                if (tl.includes(keys[i])) {
                                    return t.substring(0, 800);
                                }
                            }
                            return '';
                        })()
                        """) ?? "";
                    if (!string.IsNullOrWhiteSpace(limitText))
                    {
                        Console.WriteLine("[ASK] Claude limit detected");
                        return (false, FormatClaudeLimitResponse(limitText));
                    }

                    var detectResult = await cdp.EvalAsync($$"""
                        (() => {
                            var msgs = document.querySelectorAll('[data-is-streaming]');
                            if (msgs.length > {{preStreamingCount}}) {
                                var last = msgs[msgs.length - 1];
                                return last.getAttribute('data-is-streaming') === 'true' ? 'STREAMING' : 'DONE';
                            }
                            // Fallback: assistant-message count increase (alternate DOM structure)
                            var asstMsgs = document.querySelectorAll('[data-testid="assistant-message"]');
                            if (asstMsgs.length > {{preAssistantCount}}) return 'DONE';
                            // Fallback: thinking/generating indicator
                            var thinking = document.querySelector('[data-testid*="thinking"],[aria-label*="generating"],[aria-label*="loading"],[data-testid="streaming-indicator"]');
                            if (thinking) return 'STREAMING';
                            var userMsgs = document.querySelectorAll('[data-testid="user-message"]');
                            return 'WAITING_' + userMsgs.length;
                        })()
                        """) ?? "WAITING_0";

                    if (detectResult == "STREAMING" || detectResult == "DONE")
                    {
                        responseStarted = true;
                        Console.WriteLine($"[ASK] Response detected: {detectResult}");
                        chatLock.Release("first-byte");
                        break;
                    }

                    if (sw.Elapsed.TotalSeconds > 3)
                        Console.WriteLine($"[ASK] Waiting for response... ({detectResult}, {sw.Elapsed.TotalSeconds:F0}s)");
                }
                if (!responseStarted)
                {
                    Console.WriteLine("[ASK] No response detected");
                    return (false, (string?)null);
                }

                // ── Phase 6: Poll for completion ──
                int lastFlushedLen = 0;
                bool liveHeaderPrinted = false;
                var lastFlushTime = DateTime.UtcNow;
                sw.Restart();
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(1500);

                    // Get streaming state + latest response text
                    var pollResult = await cdp.EvalAsync($$"""
                        (() => {
                            var msgs = document.querySelectorAll('[data-is-streaming]');
                            var last = msgs.length > {{preStreamingCount}} ? msgs[msgs.length - 1] : null;
                            // Fallback: assistant-message alternate structure
                            if (!last) {
                                var asstMsgs = document.querySelectorAll('[data-testid="assistant-message"]');
                                if (asstMsgs.length > {{preAssistantCount}}) last = asstMsgs[asstMsgs.length - 1];
                            }
                            if (!last) return JSON.stringify({state: 'WAITING', len: 0, text: ''});
                            var text = last.textContent;
                            var streaming = last.getAttribute('data-is-streaming');
                            var state = streaming === 'true' ? 'STREAMING' : 'DONE';
                            return JSON.stringify({state: state, len: text.length, text: text.substring(0, 3000)});
                        })()
                        """) ?? "{}";

                    try
                    {
                        var poll = JsonSerializer.Deserialize<JsonElement>(pollResult);
                        var state = poll.GetProperty("state").GetString() ?? "UNKNOWN";
                        var text = poll.GetProperty("text").GetString() ?? "";
                        var len = poll.GetProperty("len").GetInt32();

                        // Live flush
                        if (len > lastFlushedLen && text.Length > 0)
                        {
                            if (!liveHeaderPrinted)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("── Claude streaming ──");
                                Console.ResetColor();
                                liveHeaderPrinted = true;
                            }
                            var newText = text.Length > lastFlushedLen ? text[lastFlushedLen..] : "";
                            if (newText.Length > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write(newText);
                                Console.ResetColor();
                            }
                            int flushDelta = len - lastFlushedLen;
                            lastFlushedLen = len;
                            lastFlushTime = DateTime.UtcNow;
                            Console.Write($" [FLUSH+{flushDelta}]"); Console.Out.Flush();
                        }
                        else if (state == "STREAMING" && lastFlushedLen > 0)
                        {
                            Console.Write($" [RUNNING {sw.Elapsed.TotalSeconds:F0}s]"); Console.Out.Flush();
                        }

                        // Early-exit: flush idle 1s → don't wait for DONE attribute
                        if (lastFlushedLen > 50 && state == "STREAMING"
                            && (DateTime.UtcNow - lastFlushTime).TotalSeconds >= 1.0)
                        {
                            if (liveHeaderPrinted) Console.WriteLine();
                            Console.WriteLine($"[ASK] Flush idle 1s → early done ({sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }

                        if (state == "DONE")
                        {
                            if (liveHeaderPrinted) Console.WriteLine();
                            Console.WriteLine($"[ASK] Response complete ({len} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"[ASK] Poll parse error: {pollResult[..Math.Min(80, pollResult.Length)]}");
                    }
                }

                // Timeout — return what we have
                var finalText = await cdp.EvalAsync($$"""
                    (() => {
                        var msgs = document.querySelectorAll('[data-is-streaming]');
                        var last = msgs.length > {{preStreamingCount}} ? msgs[msgs.length - 1] : null;
                        return last ? last.textContent.substring(0, 3000) : '';
                    })()
                    """) ?? "";
                if (liveHeaderPrinted) Console.WriteLine();
                Console.WriteLine($"[ASK] Timeout ({timeoutSec}s) — partial response ({finalText.Length} chars)");
                return (finalText.Length > 0, finalText.Length > 0 ? finalText : null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        // Open Slack thread early (before loop) so every step lands in same thread
        if (ok && !string.IsNullOrWhiteSpace(answer))
        {
            EnsureSlackThread("Claude", question);
            SlackPostToThread(answer.Length > 2000 ? answer[..2000] + "…" : answer, "Claude");
        }

        // Log initial answer to shared triad context (for recovery by other AIs if needed)
        if (ok && !string.IsNullOrWhiteSpace(answer))
            triadCtx?.LogStep("Claude", answer);

        Action<string, string?> onStepReport = (msg, uname) =>
        {
            SlackPostToThread(msg, uname ?? "Claude");
            triadCtx?.LogStep("Claude", msg);
        };
        if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
            (ok, answer) = Task.Run(() => RunClaudeLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry, loopMaxParallel, onStepReport)).GetAwaiter().GetResult();
        bool isLimit = IsClaudeLimitResponse(answer);
        if (isLimit) ok = false;

        if (isLimit)
        {
            EnsureSlackThread("Claude", question);
            SlackPostToThread("❌ _Claude 메시지 한도 초과_ — claude.ai 사용량 확인 필요", "Claude");
        }
        else if (!ok)
        {
            EnsureSlackThread("Claude", question);
            SlackPostToThread("❌ _Claude 응답 실패_", "Claude");
        }

        if (answer != null)
        {
            Console.WriteLine("[ASK_ANSWER_BEGIN]");
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);
            Console.WriteLine("[ASK_ANSWER_END]");

            // Slack already handled above (initial answer) + loop onStepReport
        }

        PulseStep.Done();
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── Slack Report ──

    // AsyncLocal: triad parent can pre-create a shared thread ts before spawning Task.Run children.
    // Each child Task inherits the value and posts to the same thread (no duplicate headers).
    internal static readonly System.Threading.AsyncLocal<string?> _slackSessionThreadTs = new();

    // Suppress Slack for internal sub-calls (e.g. whisper study → AskAiForStudy).
    // Set to true before calling AskCommand internally to prevent channel noise.
    internal static readonly System.Threading.AsyncLocal<bool> _suppressSlackSession = new();

    // Persona posted flag — triad posts persona once (first AI wins), others skip.
    static int _slackPersonaPostedFlag = 0; // Interlocked: 0=not posted, 1=posted

    // Per-session last-post tracker: sessionThreadTs → (msgTs, username, text).
    // Used to detect "latest comment is mine" and append via chat.update instead of new post.
    // Tool ⏳ markers update this too, so AI answers won't falsely append over tool messages.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string msgTs, string user, string text)>
        _slackThreadLastPost = new();
    const int SlackMaxAppendLength = 3800; // Slack message text limit (conservative)

    /// Post to thread and return the message ts (for later in-place updates). Null if no thread or error.
    /// Also updates _slackThreadLastPost so subsequent calls know the current "last poster".
    internal static string? SlackPostToThreadAndGetTs(string msg, string? username = null)
    {
        var threadTs = _slackSessionThreadTs.Value;
        if (threadTs == null) return null;
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;
            var uname = username ?? BotUsername;
            var (ok, ts) = SlackSendViaApi(botToken, channel, msg,
                threadTs: threadTs, username: uname).GetAwaiter().GetResult();
            if (ok && ts != null)
                _slackThreadLastPost[threadTs] = (ts, uname, msg);
            return ok ? ts : null;
        }
        catch { return null; }
    }

    /// Update a specific Slack message in-place by its ts. Noop if config unavailable.
    internal static void SlackUpdateThreadMessage(string msgTs, string text)
    {
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, msgTs, text).GetAwaiter().GetResult();
            if (!ok) Console.WriteLine($"[SLACK] SlackUpdateThreadMessage failed for ts={msgTs}");
        }
        catch { }
    }

    /// Post text to the current session's Slack thread. Noop if no active thread.
    /// If the latest thread message was posted by the same username, appends via chat.update
    /// instead of creating a new message (per user request: "최신 댓글이 자기꺼인 경우만 편집").
    internal static void SlackPostToThread(string msg, string? username = null)
    {
        var sessionTs = _slackSessionThreadTs.Value;
        if (sessionTs == null) return;
        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
            var uname = username ?? BotUsername;

            // Append via chat.update if the latest thread comment is from the same AI
            if (_slackThreadLastPost.TryGetValue(sessionTs, out var last) && last.user == uname)
            {
                var combined = last.text + "\n\n" + msg;
                if (combined.Length <= SlackMaxAppendLength)
                {
                    var (ok, _, _) = SlackUpdateMessageAsync(botToken, channel, last.msgTs, combined).GetAwaiter().GetResult();
                    if (ok)
                    {
                        _slackThreadLastPost[sessionTs] = (last.msgTs, uname, combined);
                        return;
                    }
                    // If update fails, fall through to post new
                }
                // Combined too long — fall through to new post
            }

            // Post new message (chunked for long content)
            const int chunkSize = 3000;
            string? newMsgTs = null;
            for (int pos = 0; pos < msg.Length; pos += chunkSize)
            {
                var chunk = msg[pos..Math.Min(pos + chunkSize, msg.Length)];
                var (ok, ts) = SlackSendViaApi(botToken, channel, chunk,
                    threadTs: sessionTs, username: uname).GetAwaiter().GetResult();
                if (ok && ts != null && pos == 0) newMsgTs = ts;
            }
            if (newMsgTs != null)
                _slackThreadLastPost[sessionTs] = (newMsgTs, uname, msg.Length > SlackMaxAppendLength ? msg[..SlackMaxAppendLength] : msg);
        }
        catch { }
    }

    /// <summary>
    /// Open a Slack thread for the current session if not already open.
    /// Returns the thread ts. Safe to call multiple times (idempotent per AsyncLocal context).
    /// </summary>
    internal static string? EnsureSlackThread(string label, string question)
    {
        if (_suppressSlackSession.Value) return null; // internal sub-call (e.g. whisper study)
        if (_slackSessionThreadTs.Value != null)
            return _slackSessionThreadTs.Value;   // already opened by triad parent

        try
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return null;

            var qTrunc = question.Length > 200 ? question[..200] + "..." : question;
            var (ok, ts) = SlackSendViaApi(botToken, channel, $"*[{label}]* {qTrunc}", username: BotUsername)
                               .GetAwaiter().GetResult();
            if (ok) _slackSessionThreadTs.Value = ts;
            return ok ? ts : null;
        }
        catch { return null; }
    }

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel  = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            // Open (or reuse) the session thread — triad shares one thread, single AI gets its own
            var ts = EnsureSlackThread(aiName, question);
            if (ts == null) { Console.WriteLine("[ASK] Slack report: no thread ts"); return; }

            // Thread: answer in 3000-char chunks (no truncation — split as needed)
            const int chunkSize = 3000;
            int pos = 0, part = 1;
            while (pos < answer.Length)
            {
                var chunk = answer[pos..Math.Min(pos + chunkSize, answer.Length)];
                SlackSendViaApi(botToken, channel, chunk, threadTs: ts, username: BotUsername).GetAwaiter().GetResult();
                pos += chunkSize;
                part++;
            }
            Console.WriteLine($"[ASK] Reported to Slack (thread {ts}, {part - 1} part(s))");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Slack report failed: {ex.Message}");
        }
    }
}
