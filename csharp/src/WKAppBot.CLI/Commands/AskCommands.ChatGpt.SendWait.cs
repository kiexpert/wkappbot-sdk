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
    /// <summary>
    /// Send a message in ChatGPT and wait for the response.
    /// A11y-first: finds editor via ARIA selector chain, inserts text via CDP Input.insertText.
    /// </summary>
    static async Task<(bool ok, string? text)> ChatGptSendAndWait(
        CdpClient cdp, string message, int timeoutSec, List<string>? attachFiles = null, bool returnAfterSend = false, AskSession? askSession = null)
    {
        // Phase 0: URL check + turn count (iconified OK)
        var currentUrl = await cdp.GetUrlAsync() ?? "";
        if (!currentUrl.Contains("chatgpt.com", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[ASK:LOOP] GPT tab drifted to {currentUrl[..Math.Min(80, currentUrl.Length)]} -> navigating back to chatgpt.com");
            await cdp.NavigateAsync("https://chatgpt.com/");
            await Task.Delay(3000);
            currentUrl = await cdp.GetUrlAsync() ?? "";
        }
        // prevTurns captured just before send (after lock) to avoid lazy-DOM false positives.
        int prevTurns = 0;
        string prevLastFingerprint = string.Empty;

        // NOTE: BringToFront removed because it steals OS focus. CDP works on background tabs.

        // A11y-first: find editor via selector chain.
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // Browser exclusive zone: prompt input through first response turn.
        using var chatLock = ChromeTabLock.Acquire("ChatGPT");
        if (chatLock == null) return (false, null);

        // Capture stable baseline after lock so the DOM is fully settled.
        // Fixes: WKAppBot reading an old unread GPT reply as the new response (off-by-1 DOM timing).
        prevTurns = await CountChatGptTurns(cdp);
        // TODO: migrate to AskSession when provider-specific fingerprinting is unified
        prevLastFingerprint = await cdp.EvalAsync(
            "(() => { var els = document.querySelectorAll('[data-message-author-role=\"assistant\"]');" +
            " return els.length > 0 ? (els[els.length-1].textContent?.substring(0,80) ?? '') : ''; })()") ?? string.Empty;
        Console.WriteLine($"[ASK] Pre-send baseline: turns={prevTurns} tail=[{prevLastFingerprint[..Math.Min(50, prevLastFingerprint.Length)]}]");

        // TODO-10: Capture prevFg just before input (after lock, after any tab-switch ops).
        var prevFgGpt = NativeMethods.GetForegroundWindow();

        // CDP input readiness: blocker check, restore, zoom, and focus guard.
        var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "ChatGPT", prevFgHint: prevFgGpt);
        using var focusWatchdog = StartFocusWatchdog(prevFg, cdp, "gpt-send");

        // File attachments happen before text send.
        if (attachFiles?.Count > 0)
            await AttachFilesViaCdp(cdp, attachFiles, editorSel, promptPump: AskAttachmentPump, pumpScope: "gpt");

        // Tier 1: focusless insert (a11y-first)
        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        // Verify what's actually in the editor
        var editorRaw = await cdp.GetEditorContentAsync(editorSel);
        var editorContent = editorRaw.Length > 80 ? editorRaw[..80] : editorRaw;
        Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
        if (!inserted)
        {
            // Tier 2: DOM.focus (Chrome-internal, no OS focus) + execCommand/DOM mutation
            Console.WriteLine("[ASK] Tier 1 failed, trying DOM.focus tier 2...");
            inserted = await InsertTextViaDomFocus(cdp, editorSel, message);
            if (!inserted)
            {
                Console.WriteLine("[ASK] Failed to insert text");
                return (false, null);
            }

        }
        // Send path: JS click, verify, then CDP Enter fallback.
        // With file attachments, wait for send button to become enabled
        // TODO: migrate to AskSession when provider-specific send button state polling is unified
        if (attachFiles?.Count > 0)
        {
            for (int bw = 0; bw < 10; bw++)
            {
                var btnState = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[data-testid="send-button"]')
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

        // If a stop button is visible, the previous response is still streaming.
        var shouldWaitForPriorStop = prevTurns > 0 || currentUrl.Contains("/c/", StringComparison.OrdinalIgnoreCase);
        if (shouldWaitForPriorStop)
        {
            if (!await WaitWhileStopButtonVisible(cdp, maxWaitMs: 12000))
                return (false, null);
        }

        var sendResult = "PENDING";

        // Use turn count for send verification when image is attached
        // (textContent check is unreliable with image attachments)
        int preSendTurns = await CountChatGptTurns(cdp);

        // Tier 1: JS click (works minimized, but React may ignore .click())
        // TODO: migrate to AskSession.ClickSendAsync() when turn-count verification flow is unified
        var jsClick = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="send-button"]')
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
                var remaining = await cdp.GetTextLengthAsync(editorSel);
                sendResult = remaining == 0 ? "JS_CLICK" : "CLICK_NOOP";
            }
        }

        // Tier 2: UIA Invoke on send button (focusless, works minimized)
        if (sendResult != "JS_CLICK")
        {
            if (shouldWaitForPriorStop)
            {
                if (!await WaitWhileStopButtonVisible(cdp))
                    return (false, null);
            }
            Console.WriteLine("[ASK] JS click didn't send, trying UIA invoke...");
            if (TryUiaInvokeSendButton())
            {
                await Task.Delay(1000);
                var postTurns = await CountChatGptTurns(cdp);
                if (postTurns > preSendTurns)
                    sendResult = "UIA_INVOKE";
                else
                {
                    var remaining = await cdp.GetTextLengthAsync(editorSel);
                    sendResult = remaining == 0 ? "UIA_INVOKE" : "UIA_NOOP";
                }
            }
        }

        // Tier 3: CDP Enter key (needs visible viewport + editor focus)
        if (sendResult != "JS_CLICK" && sendResult != "UIA_INVOKE")
        {
            await cdp.FocusAsync(editorSel);
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
        LogRestoreFocus(prevFg, "after-send-GPT", cdp);

        // Check editor after send; it should be empty on success.
        var afterSend = (await cdp.GetTextLengthAsync(editorSel)).ToString();
        Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={prevTurns})");
        await cdp.MarkPromptDispatchAsync(editorSel, "chatgpt", sendResult);
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
            if (askSession != null && await TryHandleAskControlAsync(askSession))
            {
                chatLock.Release("cancelled");
                return (false, "CANCELLED");
            }

            // URL change detection (new conversation resets turn count)
            var newUrl = await cdp.GetUrlAsync() ?? "";
            if (newUrl != currentUrl && newUrl.Contains("/c/"))
            {
                Console.WriteLine();
                Console.WriteLine("[ASK] New conversation detected");
                prevTurns = 0;
                currentUrl = newUrl;
            }

            // Multi-signal detection: turn count OR stop button OR thinking marker
            // TODO: migrate to AskSession when provider-specific response detection is unified
            var probe = await cdp.ProbePromptResponseStateAsync(editorSel, "chatgpt", prevTurns);
            var detectResult = probe.Status switch
            {
                "RUNNING" => probe.ResponseCount > prevTurns ? $"TURN_{probe.ResponseCount}" : "STREAMING",
                "DONE" => $"TURN_{probe.ResponseCount}",
                "QUEUED" => $"QUEUED_{probe.ResponseCount}",
                "LOCKED" => "LOCKED",
                "NO_EDITOR" => "NO_EDITOR",
                _ => $"WAITING_{probe.ResponseCount}"
            };

            // Show meaningful state instead of plain dots
            var waitTag = detectResult.StartsWith("TURN_") ? "TURN" :
                          detectResult == "STREAMING" ? "RUNNING" :
                          detectResult == "THINKING"  ? "THINKING" : ".";
            Console.Write($" {waitTag}"); Console.Out.Flush();

            if (detectResult.StartsWith("TURN_") || detectResult == "STREAMING" || detectResult == "THINKING")
            {
                responseStarted = true;
                askSession?.MarkRunning();
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
            askSession?.MarkTimedOut();
            Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
            Console.WriteLine("[ASK] No response detected");
            return (false, null);
        }

        // Poll phase 1: wait for streaming or thinking to finish.
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
            if (askSession != null && await TryHandleAskControlAsync(askSession))
                return (false, "CANCELLED");

            // Combined check: state + streaming text length + delta text for live flush
            // TODO: migrate to AskSession when provider-specific polling is unified
            var stateJson = await cdp.EvalAsync($$"""
                (() => {
                    if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100)
                        return JSON.stringify({s:'BLANK',len:0,delta:''});
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label="Stop streaming"]')
                            || document.querySelector('button[aria-label="Stop generating"]');
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
                     Console.WriteLine("[ChatGPT] streaming...");
                    liveHeaderPrinted = true;
                }
                Console.Write(delta);
                Console.Out.Flush();
                lastFlushedLen = textLen;
                lastFlushTime = DateTime.UtcNow;
            }

            // Early exit: if flush stopped for 1s and text exists, treat it as done.
            bool isThinkingState = state == "THINKING" || state == "THINKING_HAS_TEXT";
            if (lastFlushedLen > 50 && state != "DONE" && state != "IMG_GEN"
                && !isThinkingState && (DateTime.UtcNow - lastFlushTime).TotalSeconds >= 1.0)
            {
                if (liveHeaderPrinted) Console.WriteLine();
                Console.WriteLine($"[ASK] Flush idle 1s -- early done ({sw.Elapsed.TotalSeconds:F0}s)");
                break;
            }

            // Detect generated images in response (inline marker output)
            var newImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
            savedImages.AddRange(newImages);

            // Image generation in progress; keep waiting (up to 90s for DALL-E).
            if (state == "IMG_GEN")
            {
                if (!liveHeaderPrinted)
                {
                    Console.WriteLine();
                     Console.WriteLine("[ChatGPT] streaming...");
                    liveHeaderPrinted = true;
                }
                Console.Write(".");
                Console.Out.Flush();
                continue;
            }

            if (state == "DONE" || state == "DONE_EMPTY")
            {
                // If very little text and early in stream, wait a bit more
                // ChatGPT can briefly idle before the actual response appears.
                if (textLen < 50 && sw.Elapsed.TotalSeconds < 60)
                {
                    if (textLen > 0) Console.Write(".");
                    Console.Out.Flush();
                    continue; // Keep polling; likely just a transient idle gap.
                }
                // Auto-select first response if GPT shows A/B comparison mode
                try
                {
                    var abChoice = await cdp.EvalAsync(
                        "(() => {" +
                        "var btns = document.querySelectorAll('button');" +
                        // Detect A/B comparison header: "피드백" or "Which response" or "어떤 응답"
                        "var abHeader = document.body.innerText.indexOf('\\uD53C\\uB4DC\\uBC31') >= 0" + // 피드백
                        "  || document.body.innerText.indexOf('Which response') >= 0" +
                        "  || document.body.innerText.indexOf('\\uC5B4\\uB5A4 \\uC751\\uB2F5') >= 0;" + // 어떤 응답
                        "if (!abHeader) return 'NO_AB';" +
                        // Find first response button (contains "1" as standalone or "Response 1" or "응답 1")
                        "for (var b of btns) {" +
                        "  var txt = (b.textContent || '').trim();" +
                        "  if (/Response\\s*1/i.test(txt) || /\\uC751\\uB2F5\\s*1/.test(txt) || txt === '1') {" +
                        "    b.click(); return 'PICKED_1:' + txt;" +
                        "  }" +
                        "}" +
                        // Fallback: click first of any prefer/thumbs buttons
                        "var prefs = document.querySelectorAll('[data-testid*=\"prefer\"], [data-testid*=\"thumbs-up\"]');" +
                        "if (prefs.length > 0) { prefs[0].click(); return 'PREFER_FIRST'; }" +
                        "return 'NO_AB';" +
                        "})()") ?? "NO_AB";
                    if (abChoice != "NO_AB")
                        Console.WriteLine($"[ASK] GPT A/B comparison detected -> auto-selected first ({abChoice})");
                }
                catch { }

                // Final image check before completion
                var finalImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
                savedImages.AddRange(finalImages);
                if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                Console.WriteLine($"[ASK] Streaming complete ({sw.Elapsed.TotalSeconds:F0}s)");
                if (savedImages.Count > 0)
                    Console.WriteLine($"[ASK] Downloaded {savedImages.Count} generated image(s)");
                break;
            }

            // Blank page detection: ChatGPT often navigates to /c/UUID after first message,
            // causing brief BLANK during page load. Tolerate more blanks early on (navigation window).
            if (state == "BLANK" || string.IsNullOrEmpty(state))
            {
                blankPageCount++;
                int blankLimit = sw.Elapsed.TotalSeconds < 30 ? 20 : 3; // GPT stays blank while navigating to /c/UUID
                Console.WriteLine($"[ASK] Page blank/navigating ({blankPageCount}/{blankLimit}), {sw.Elapsed.TotalSeconds:F0}s");
                if (blankPageCount >= blankLimit)
                {
                    Console.WriteLine("[ASK] Page unresponsive -> aborting poll");
                    break;
                }
                continue;
            }

            blankPageCount = 0; // reset on valid response

            // First-byte timeout: 20s of streaming with no text likely means the page is stuck.
            // Exempt: THINKING state (o3/o4 models can think for 30s+ before first text)
            bool isThinking = state == "THINKING" || state == "THINKING_HAS_TEXT";
            bool hasResponseText = state == "STREAMING_HAS_TEXT" || state == "THINKING_HAS_TEXT";

            // Streaming handoff: once response text appears, give the active tab to a peer.
            if (hasResponseText)
                await HandoffTabToPeer("chatgpt");

            if (!hasResponseText && !isThinking && lastFlushedLen == 0 && sw.Elapsed.TotalSeconds >= 20)
            {
                Console.WriteLine("[ASK] No response text after 20s -> aborting (first-byte timeout)");
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
            // Thinking mode (o1/o3/o4) can run for minutes, so allow unlimited extensions.
            // Streaming: max 1 extension (2x original timeout)
            int maxExtensions = isThinking ? 99 : 1;
            if (sw.Elapsed.TotalSeconds > timeoutSec * 0.8 && streamExtensions < maxExtensions)
            {
                streamExtensions++;
                Console.WriteLine($"[ASK] Still {(isThinking ? "thinking" : "streaming")}, extending timeout... (ext #{streamExtensions})");
                // ext #1: the window may be hidden or minimized, so try recovery.
                if (streamExtensions == 1)
                    await TryRecoverChatGptTabAsync(cdp, $"thinking ext#{streamExtensions}");
                sw.Restart();
            }
        }

        // Poll phase 2: text extraction.
        // NOTE: BringToFront removed because innerText works on background tabs.
        await Task.Delay(300);

        // Scroll into view + hydrate + extract
        // TODO: migrate to AskSession when provider-specific text extraction is unified
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

                    // Fallback: innerHTML with tags stripped.
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

            // Done; hand off the tab to a peer if still waiting.
            await HandoffTabToPeer("chatgpt");

            // Final answer ready: focusless restore to show the answer to the user.
            ShowChromeAnswer(cdp);

            askSession?.MarkDone(finalText);
            return (true, finalText);
        }
        Console.WriteLine("[ASK] Timeout -- no response");
        askSession?.MarkTimedOut();
        return (false, null);
    }
}
