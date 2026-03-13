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
        "div.tiptap.ProseMirror[contenteditable='true']",  // With attr
        ".ProseMirror[contenteditable='true']",            // ProseMirror generic
        "[contenteditable='true']",                        // Generic fallback
    ];

    static async Task<string?> WaitForClaudeEditorA11y(CdpClient cdp)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ClaudeEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        Console.WriteLine("[ASK] Claude editor not found (selector chain exhausted)");
        return null;
    }

    /// <summary>Insert text into Claude.ai ProseMirror via ClipboardEvent paste (only reliable method).</summary>
    static async Task<bool> InsertTextClaudeProseMirror(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");

        var result = await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
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
        await cdp.EvalAsync($"document.querySelector('{selector}')?.focus()");
        await Task.Delay(100);
        await cdp.SendAsync("Input.insertText", new JsonObject { ["text"] = text });
        await Task.Delay(200);
        var verify = await cdp.EvalAsync(
            $"document.querySelector('{selector}')?.textContent?.length ?? 0") ?? "0";
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

    static int AskClaude(string question, bool slackReport, int timeoutSec, bool newTab, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, int loopMaxParallel = 7, bool triadMode = false, string? modelHint = null, bool noWait = false)
    {
        Console.WriteLine($"[ASK] Claude: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Claude model hint: {modelHint}");

        var targetTag = BuildAskTargetTag("claude");
        var cdp = EnsureCdpConnection(preferredHost: "claude.ai", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        { var prevFgClaude = NativeMethods.GetForegroundWindow(); Console.WriteLine($"[ASK:FOCUS] pre-activate fg={prevFgClaude:X8}"); if (!newTab) cdp.ActivateTabAsync().GetAwaiter().GetResult(); LogRestoreFocus(prevFgClaude, "ActivateTab-Claude"); }

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
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
                    var (personaOk, personaResp) = await ClaudeSendAndWaitAsync(
                        cdp,
                        BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint),
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
                var inserted = await InsertTextClaudeProseMirror(cdp, editorSel, question);
                var editorContent = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
                Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
                if (!inserted)
                {
                    zoom?.ShowFail("insert failed");
                    zoom?.Dispose();
                    Console.WriteLine("[ASK] Failed to insert text into Claude editor");
                    return (false, (string?)null);
                }

                // ── Send: click button ──
                await Task.Delay(500);
                int preSendTurns = await CountClaudeTurns(cdp);
                var sendResult = "PENDING";

                // Tier 1: JS click on send button (multi-selector fallback for DOM changes)
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
                        var remaining = await cdp.EvalAsync(
                            $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                        sendResult = remaining == "0" ? "JS_CLICK" : "CLICK_NOOP";
                    }
                }

                // Tier 2: CDP Enter key
                if (sendResult != "JS_CLICK")
                {
                    Console.WriteLine("[ASK] JS click didn't send, trying Enter key...");
                    await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
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
                    sendResult = "CDP_ENTER";
                }

                // Zoom feedback + focus guard
                zoom?.ShowPass($"sent ({sendResult})");
                zoom?.Dispose();
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");
                LogRestoreFocus(prevFg, "after-send-Claude");

                var afterSend = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
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
                            var t = (document.body?.innerText || '');
                            var keys = ['usage limit', 'rate limit', 'too many requests', '요청이 너무 많', '사용량 한도'];
                            for (var i = 0; i < keys.length; i++) {
                                if (t.toLowerCase().includes(keys[i])) {
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

                    var detectResult = await cdp.EvalAsync("""
                        (() => {
                            if (document.querySelector('[data-is-streaming="true"]')) return 'STREAMING';
                            if (document.querySelector('[data-is-streaming="false"]')) return 'DONE';
                            var msgs = document.querySelectorAll('[data-testid="user-message"]');
                            return 'WAITING_' + msgs.length;
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
                sw.Restart();
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(1500);

                    // Get streaming state + latest response text
                    var pollResult = await cdp.EvalAsync("""
                        (() => {
                            var streaming = document.querySelector('[data-is-streaming="true"]');
                            var msgs = document.querySelectorAll('[data-is-streaming]');
                            var last = msgs.length > 0 ? msgs[msgs.length - 1] : null;
                            var text = last ? last.textContent : '';
                            var state = streaming ? 'STREAMING' : 'DONE';
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
                            lastFlushedLen = len;
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
                var finalText = await cdp.EvalAsync("""
                    (() => {
                        var msgs = document.querySelectorAll('[data-is-streaming]');
                        var last = msgs.length > 0 ? msgs[msgs.length - 1] : null;
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
        if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
            (ok, answer) = Task.Run(() => RunClaudeLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry, loopMaxParallel)).GetAwaiter().GetResult();
        if (IsClaudeLimitResponse(answer))
            ok = false;

        if (answer != null)
        {
            Console.WriteLine("[ASK_ANSWER_BEGIN]");
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);
            Console.WriteLine("[ASK_ANSWER_END]");

            if (slackReport)
                ReportToSlack("Claude", question, answer);
        }

        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── Slack Report ──

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            var truncated = answer.Length > 1500 ? answer[..1500] + "\n... (truncated)" : answer;
            var msg = $"*[{aiName} 답변]*\n> Q: {question}\n\n{truncated}";
            var (ok, _) = SlackSendViaApi(botToken, channel, msg, username: BotUsername).GetAwaiter().GetResult();
            if (ok)
                Console.WriteLine($"[ASK] Reported to Slack");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Slack report failed: {ex.Message}");
        }
    }
}
