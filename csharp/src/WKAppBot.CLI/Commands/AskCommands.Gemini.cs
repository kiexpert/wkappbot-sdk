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

    // ?? Gemini ??
    static readonly string[] GeminiStopNoticeKeywords =
    [
        "응답이 중지되었습니다",
        "응답이 중지",
        "대답이 중지되었습니다",
        "중지되었습니다",
        "response was stopped",
        "response stopped",
        "stopped response"
    ];

    static bool IsGeminiStoppedNotice(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        return GeminiStopNoticeKeywords.Any(k => text.Contains(k, StringComparison.OrdinalIgnoreCase));
    }

    static async Task<bool> WaitWhileGeminiStopVisibleAsync(CdpClient cdp, int maxWaitMs = 12000)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            var stopVisible = await cdp.EvalAsync("""
                (() => {
                    var stop = document.querySelector('button[aria-label*="Stop"]')
                            || document.querySelector('button[aria-label*="중지"]')
                            || document.querySelector('mat-icon[fonticon="stop_circle"]');
                    return stop ? '1' : '0';
                })()
                """) ?? "0";
            if (stopVisible != "1")
                return true;

            Console.WriteLine($"[ASK] Gemini stop visible; waiting before send... ({sw.ElapsedMilliseconds}ms)");
            await Task.Delay(700);
        }

        Console.WriteLine("[ASK] Gemini stop still visible after wait.");
        return false;
    }

    static async Task<(bool ok, string? text)> RetryGeminiAfterStopAsync(CdpClient cdp, string editorSel, string question)
    {
        await WaitWhileGeminiStopVisibleAsync(cdp, 6000);
        await ClearContentEditable(cdp, editorSel);
        await InsertTextContentEditable(cdp, editorSel, question);
        await Task.Delay(300);
        await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[aria-label="Send message"]')
                       || document.querySelector('button[aria-label*="Send"]')
                       || document.querySelector('button.send-button');
                if (btn && !btn.disabled) btn.click();
            })()
            """);

        var sw = Stopwatch.StartNew();
        string? retryText = null;
        while (sw.Elapsed.TotalSeconds < 30)
        {
            await Task.Delay(2000);
            var text = await cdp.EvalAsync(
                "(() => {" +
                "var r = document.querySelectorAll('model-response');" +
                "if (r.length === 0) { var a = document.querySelectorAll('[role=\"article\"]'); r = a.length > 0 ? a : r; }" +
                "if (r.length === 0) return '';" +
                "return r[r.length-1].textContent || '';" +
                "})()") ?? "";
            if (string.IsNullOrWhiteSpace(text)) continue;
            if (text == retryText)
            {
                if (IsGeminiStoppedNotice(text))
                    return (false, text);
                return (true, text);
            }
            retryText = text;
        }

        if (!string.IsNullOrWhiteSpace(retryText) && !IsGeminiStoppedNotice(retryText))
            return (true, retryText);
        return (false, retryText);
    }

    static int AskGemini(string question, bool slackReport, int timeoutSec, bool newTab, List<string>? attachFiles = null, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, bool triadMode = false, string? modelHint = null, bool noWait = false)
    {
        Console.WriteLine($"[ASK] Gemini: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Gemini model hint: {modelHint}");

        var targetTag = BuildAskTargetTag("gemini");
        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        // CDP tab activation (focusless) ??replaces UIA EnsureTabViaGrap
        // Target.activateTarget does Chrome-internal tab switch without OS SetForegroundWindow
        var prevFgGemini = NativeMethods.GetForegroundWindow();
        Console.WriteLine($"[ASK:FOCUS] pre-activate fg={prevFgGemini:X8}");
        if (!newTab) cdp.ActivateTabAsync().GetAwaiter().GetResult();
        LogRestoreFocus(prevFgGemini, "ActivateTab");

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ?? Phase 1: Navigate (iconified OK ??CDP works without rendering) ??
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("gemini.google.com"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session ??navigating to fresh Gemini..." : "[ASK] Navigating to Gemini...");
                    await cdp.NavigateAsync("https://gemini.google.com/app");
                    await Task.Delay(3000);
                }
                else
                {
                    Console.WriteLine($"[ASK] Reusing Gemini session");
                }

                // NOTE: BringToFront removed ??steals OS focus.
                // CDP insertText/eval/setFileInputFiles all work on background tabs.

                // Diagnose tab state before editor search
                var hiddenState = await cdp.EvalAsync("document.hidden + '|' + document.title + '|' + document.querySelectorAll('*').length");
                Console.WriteLine($"[ASK] Tab state: {hiddenState}");

                // If tab is hidden (background), dispatch visibility events to trigger deferred rendering
                // (Gemini lazily initializes editor on hidden tabs ??fake visibility = focusless fix)
                if (hiddenState?.StartsWith("true|") == true)
                {
                    Console.WriteLine("[ASK] Tab hidden ??dispatching visibility events (focusless)...");
                    await cdp.EvalAsync(
                        "try{Object.defineProperty(document,'hidden',{get:()=>false,configurable:true});" +
                        "Object.defineProperty(document,'visibilityState',{get:()=>'visible',configurable:true});" +
                        "document.dispatchEvent(new Event('visibilitychange'));" +
                        "window.dispatchEvent(new Event('focus'));}catch(e){}");
                    await Task.Delay(500);
                }

                // A11y-first: find editor via selector chain
                var editorSel = await WaitForEditorA11y(cdp,
                    ".ql-editor",                                   // Quill class
                    "[role='textbox'][contenteditable='true']",      // ARIA role
                    "div[contenteditable='true']",                   // Generic
                    "div.ql-editor",                                 // Quill with tag
                    "rich-textarea [contenteditable]",               // Gemini new UI
                    ".input-area [contenteditable]"                  // Gemini alt
                );
                if (editorSel == null)
                {
                    // Last resort: dump available contenteditable elements
                    var ceDebug = await cdp.EvalAsync("""
                        (() => {
                            var ce = document.querySelectorAll('[contenteditable]');
                            return Array.from(ce).map(e => e.tagName + '.' + (e.className||'').substring(0,40) + '[' + e.getAttribute('contenteditable') + ']').join(' | ');
                        })()
                        """);
                    Console.WriteLine($"[ASK] contenteditable elements: {ceDebug}");
                    Console.WriteLine("[ASK] Editor not found");
                    return (false, (string?)null);
                }

                // ?? Persona injection on fresh Gemini conversation ??
                var geminiTurnCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                var hasLoopPersonaState = await HasLoopPersonaStateAsync(cdp, "gemini");
                var effectiveLoopPersona = loopMode || hasLoopPersonaState;
                Console.WriteLine($"[ASK] Loop persona state: {(hasLoopPersonaState ? "present" : "missing")}");
                if (!loopMode && hasLoopPersonaState)
                    Console.WriteLine("[ASK] Loop marker found; MCP guidance will be included for fresh session persona.");
                if (geminiTurnCount == "0" || (effectiveLoopPersona && !hasLoopPersonaState))
                {
                    // ?? Browser exclusive: persona input ??send complete ??
                    using var personaLock = ChromeTabLock.Acquire("Gemini/persona");
                    if (personaLock == null) return (false, (string?)null);

                    Console.WriteLine(geminiTurnCount == "0"
                        ? "[ASK] Fresh Gemini -- injecting persona..."
                        : "[ASK] Loop persona missing on this tab -- re-injecting persona...");
                    await ClearContentEditable(cdp, editorSel);
                    await InsertTextContentEditable(cdp, editorSel, BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint));
                    await Task.Delay(300);

                    // Send persona (button click ??Enter fallback)
                    var personaSent = false;
                    for (int ps = 0; ps < 5; ps++)
                    {
                        var rem = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                        if (rem == "0" && ps > 0) { personaSent = true; break; }
                        await cdp.EvalAsync("document.querySelector('button[aria-label=\"Send message\"], button[aria-label*=\"Send\"], .send-button, button.send-button')?.click()");
                        await Task.Delay(500);
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
                        await Task.Delay(500);
                    }

                    if (personaSent)
                    {
                        if (effectiveLoopPersona)
                            await SetLoopPersonaStateAsync(cdp, "gemini");
                        // Wait for Gemini to respond to persona (no lock needed ??just polling)
                        var psw = Stopwatch.StartNew();
                        while (psw.Elapsed.TotalSeconds < 15)
                        {
                            await Task.Delay(1500);
                            var resp = await cdp.EvalAsync("""
                                (() => {
                                    var r = document.querySelectorAll('model-response');
                                    if (r.length === 0) r = document.querySelectorAll('[role="article"]');
                                    if (r.length === 0) return '';
                                    return (r[r.length-1].innerText || '').substring(0, 50);
                                })()
                                """) ?? "";
                            if (resp.Length > 0)
                            {
                                bool ready = resp.Contains("READY", StringComparison.OrdinalIgnoreCase);
                                Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : resp)}");
                                break;
                            }
                        }
                        // Wait for Gemini to finish streaming (stop button gone + text stable)
                        for (int stab = 0; stab < 10; stab++)
                        {
                            await Task.Delay(500);
                            var streaming = await cdp.EvalAsync("""
                                (() => {
                                    var stop = document.querySelector('button[aria-label="?묐떟 以묒?"], button[aria-label="Stop response"], button[aria-label="Stop"]');
                                    if (stop) return 'STREAMING';
                                    var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                                    if (mat) return 'STREAMING';
                                    return 'IDLE';
                                })()
                                """) ?? "IDLE";
                            if (streaming == "IDLE")
                            {
                                Console.WriteLine($"[ASK] Persona streaming done (stable after {(stab+1)*500}ms)");
                                break;
                            }
                            if (stab == 9) Console.WriteLine("[ASK] Persona streaming timeout, proceeding anyway");
                        }
                        // Re-find editor after persona exchange
                        editorSel = await WaitForEditorA11y(cdp,
                            ".ql-editor", "[role='textbox'][contenteditable='true']",
                            "div[contenteditable='true']", "div.ql-editor",
                            "rich-textarea [contenteditable]", ".input-area [contenteditable]");
                        if (editorSel == null)
                        {
                            Console.WriteLine("[ASK] Editor lost after persona");
                            return (false, (string?)null);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                }

                // ?? Browser exclusive: question input ??send complete ??
                using var questionLock = ChromeTabLock.Acquire("Gemini");
                if (questionLock == null) return (false, (string?)null);

                // ?? CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ??
                var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "Gemini");

                // ?? File attachments (before text) ??
                // Pass prevFgGemini so native file dialog tier can restore original user focus after close
                if (attachFiles?.Count > 0)
                    await AttachFilesViaCdp(cdp, attachFiles, editorSel, prevFgGemini);

                // Tier 1: focusless insert (a11y-first)
                await ClearContentEditable(cdp, editorSel);
                var inserted = await InsertTextContentEditable(cdp, editorSel, question);
                if (!inserted)
                {
                    // ?? Tier 2: CDP Input.insertText (needs focus) ??
                    Console.WriteLine("[ASK] Focusless insert failed, trying CDP Input.insertText...");
                    await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
                    await Task.Delay(100);
                    await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                    {
                        ["type"] = "keyDown", ["key"] = "a", ["code"] = "KeyA",
                        ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
                    });
                    await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                    {
                        ["type"] = "keyUp", ["key"] = "a", ["code"] = "KeyA",
                        ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
                    });
                    await Task.Delay(50);
                    await cdp.SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject
                    {
                        ["text"] = question
                    });
                    await Task.Delay(200);

                    var verify = await cdp.EvalAsync(
                        $"document.querySelector('{editorSel}')?.textContent?.length ?? 0") ?? "0";
                    if (verify == "0")
                    {
                        zoom?.ShowFail("insert failed");
                        zoom?.Dispose();
                        Console.WriteLine("[ASK] Failed to insert text");
                        return (false, (string?)null);
                    }
                }

                // ?? Focus theft detection: restore if Chrome stole focus ??
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");

                // Send: a11y-first (CDP real click on button) ??focusless Enter fallback
                // Keep trying until editor is empty (= message sent)
                await Task.Delay(300);
                var sendResult = "PENDING";
                // Count model-responses before sending ??detect response start as send confirmation
                var preResponseCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";

                for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
                {
                    if (!await WaitWhileGeminiStopVisibleAsync(cdp))
                    {
                        var curResponses = await cdp.EvalAsync(
                            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                        if (curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_IN_PROGRESS(attempt={sendAttempt})";
                            break;
                        }
                    }

                    // Check if editor cleared OR response started (= already sent, don't re-send!)
                    var remaining = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                    if (remaining == "0" && sendAttempt > 0)
                    {
                        sendResult = $"SENT(attempt={sendAttempt})";
                        break;
                    }
                    // Response started = message was sent, stop clicking (avoids hitting stop button)
                    if (sendAttempt > 0)
                    {
                        var curResponses = await cdp.EvalAsync(
                            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                        if (curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_STARTED(attempt={sendAttempt})";
                            break;
                        }
                    }

                    // Re-insert text if editor is empty (text didn't stick)
                    if (remaining == "0" && sendAttempt == 0)
                    {
                        await InsertTextContentEditable(cdp, editorSel, question);
                        await Task.Delay(200);
                    }

                    // JS click() ??works even when Chrome is minimized (no viewport needed)
                    var clickResult = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('button[aria-label="硫붿떆吏 蹂대궡湲?]')
                                   || document.querySelector('button[aria-label="Send message"]')
                                   || document.querySelector('button.send-button');
                            if (!btn || btn.disabled) return 'NO_BUTTON';
                            btn.click();
                            return 'CLICKED';
                        })()
                        """) ?? "NO_BUTTON";

                    if (clickResult != "CLICKED")
                    {
                        var stopVisibleNow = await cdp.EvalAsync("""
                            (() => {
                                var stop = document.querySelector('button[aria-label*="Stop"]')
                                        || document.querySelector('button[aria-label*="중지"]')
                                        || document.querySelector('mat-icon[fonticon="stop_circle"]');
                                return stop ? '1' : '0';
                            })()
                            """) ?? "0";
                        var curResponses = await cdp.EvalAsync(
                            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                        if (stopVisibleNow == "1" || curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_STARTED(stop-visible attempt={sendAttempt + 1})";
                            break;
                        }

                        // Fallback: Enter key via CDP
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
                    }
                    await Task.Delay(500);
                }
                if (sendResult == "PENDING") sendResult = "FORCED(5x)";

                // Zoom feedback: sent successfully
                zoom?.ShowPass($"sent ({sendResult})");
                zoom?.Dispose();
                LogRestoreFocus(prevFg, "after-send-Gemini");

                Console.WriteLine($"[ASK] Sent! Waiting for response... (send={sendResult})");
                questionLock.Release("sent");
                if (noWait)
                    return (true, BuildNoWaitQueuedMessage("Gemini"));

                // Count existing responses before polling (skip persona's READY etc.)
                var preCountStr = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                int baseResponseCount = int.TryParse(preCountStr, out var brc) ? brc : 0;
                bool responseAlreadyStarted = sendResult.StartsWith("RESPONSE_", StringComparison.OrdinalIgnoreCase);

                // Register for tab handoff + activate peer's tab (we'll poll with textContent)
                RegisterWaitingTab("gemini", cdp);
                await HandoffTabToPeer("gemini");

                // Wait for response ??poll until text stabilizes
                // Live flush: print new text as it arrives during streaming
                string? lastText = null;
                int stableCount = 0;
                int lastTextLen = 0;
                int lastFlushedLen = 0;
                bool liveHeaderPrinted = false;
                var geminiKnownImages = new HashSet<string>();
                var geminiSavedImages = new List<string>();
                var sw = Stopwatch.StartNew();

                int geminiBlankCount = 0;
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    // A11y-first: model-response ??[role='article'] ??generic text
                    // Only read NEW responses (skip persona exchange)
                    var text = await cdp.EvalAsync(
                        "(() => {" +
                        "if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100) return '\\x01BLANK';" +
                        "var responses = document.querySelectorAll('model-response');" +
                        "if (responses.length === 0) { var articles = document.querySelectorAll('[role=\"article\"]'); responses = articles.length > 0 ? articles : responses; }" +
                        (!responseAlreadyStarted ? $"if (responses.length <= {baseResponseCount}) return '';" : "") +
                        "var last = responses[responses.length - 1];" +
                        "return last.textContent || '';" +
                        "})()");

                    // Blank/broken page detection
                    if (text == "\x01BLANK" || text == null)
                    {
                        geminiBlankCount++;
                        Console.WriteLine($"[ASK] Page blank/broken ({geminiBlankCount}/3)");
                        if (geminiBlankCount >= 3)
                        {
                            Console.WriteLine("[ASK] Page unresponsive ??aborting poll");
                            break;
                        }
                        continue;
                    }
                    geminiBlankCount = 0;

                    if (string.IsNullOrEmpty(text))
                        continue;

                    // Live flush: print new text delta
                    if (text.Length > lastFlushedLen)
                    {
                        if (!liveHeaderPrinted)
                        {
                            Console.WriteLine();
                            Console.WriteLine("?? Gemini (streaming) ??");
                            liveHeaderPrinted = true;
                        }
                        Console.Write(text.Substring(lastFlushedLen));
                        Console.Out.Flush();
                        lastFlushedLen = text.Length;
                    }

                    // Detect generated images in response
                    var gemNewImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                    geminiSavedImages.AddRange(gemNewImages);

                    // Streaming handoff: text growing ??this tab is alive, give active tab to peer
                    if (text.Length > lastTextLen && lastTextLen > 0)
                        await HandoffTabToPeer("gemini");
                    lastTextLen = text.Length;

                    // Check if response is still generating
                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 2) // stable for 4+ seconds
                        {
                            // Final image check
                            var gemFinalImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                            geminiSavedImages.AddRange(gemFinalImages);
                            if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                            if (IsGeminiStoppedNotice(text))
                            {
                                Console.WriteLine("[ASK] Gemini stopped response notice detected; retrying once...");
                                var retryResult = await RetryGeminiAfterStopAsync(cdp, editorSel, question);
                                if (retryResult.ok && !string.IsNullOrWhiteSpace(retryResult.text))
                                {
                                    Console.WriteLine($"[ASK] Retry recovered ({retryResult.text.Length} chars)");
                                    return (true, retryResult.text);
                                }
                                Console.WriteLine("[ASK] Retry did not recover from Gemini stop notice; fast-fail");
                                return (false, retryResult.text ?? text);
                            }
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            if (geminiSavedImages.Count > 0)
                                Console.WriteLine($"[ASK] Downloaded {geminiSavedImages.Count} generated image(s)");
                            return (true, text);
                        }
                    }
                    else
                    {
                        stableCount = 0;
                        lastText = text;
                    }
                }

                // Done ??hand off tab to peer if still waiting
                await HandoffTabToPeer("gemini");

                // Timeout ??return whatever we have
                if (!string.IsNullOrEmpty(lastText))
                {
                    if (IsGeminiStoppedNotice(lastText))
                    {
                        Console.WriteLine("[ASK] Timeout with Gemini stop notice; fast-fail");
                        return (false, lastText);
                    }
                    Console.WriteLine($"[ASK] Timeout ??partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout ??no response, retrying once...");

                // Retry: same page, re-insert and send (no reload, keeps session)
                await Task.Delay(1000);
                await ClearContentEditable(cdp, editorSel);
                await InsertTextContentEditable(cdp, editorSel, question);
                await Task.Delay(300);
                await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label="硫붿떆吏 蹂대궡湲?]')
                               || document.querySelector('button[aria-label="Send message"]')
                               || document.querySelector('button.send-button');
                        if (btn && !btn.disabled) btn.click();
                    })()
                    """);
                Console.WriteLine("[ASK] Retry: sent question");

                // Poll retry (shorter timeout)
                var retrySw = Stopwatch.StartNew();
                string? retryText = null;
                while (retrySw.Elapsed.TotalSeconds < 30)
                {
                    await Task.Delay(2000);
                    var text = await cdp.EvalAsync(
                        "(() => {" +
                        "var r = document.querySelectorAll('model-response');" +
                        "if (r.length === 0) { var a = document.querySelectorAll('[role=\"article\"]'); r = a.length > 0 ? a : r; }" +
                        "if (r.length === 0) return '';" +
                        "return r[r.length-1].textContent || '';" +
                        "})()") ?? "";
                    if (string.IsNullOrEmpty(text)) continue;
                    if (text == retryText)
                    {
                        if (IsGeminiStoppedNotice(text))
                        {
                            Console.WriteLine("[ASK] Retry: stop notice detected; fast-fail");
                            return (false, text);
                        }
                        Console.WriteLine($"[ASK] Retry: response ({text.Length} chars)");
                        return (true, text);
                    }
                    retryText = text;
                }
                if (!string.IsNullOrEmpty(retryText))
                {
                    if (IsGeminiStoppedNotice(retryText))
                    {
                        Console.WriteLine("[ASK] Retry: partial stop notice; fast-fail");
                        return (false, retryText);
                    }
                    Console.WriteLine($"[ASK] Retry: partial ({retryText.Length} chars)");
                    return (true, retryText);
                }
                Console.WriteLine("[ASK] Retry: also failed");
                return (false, (string?)null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();
        if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
            (ok, answer) = Task.Run(() => RunGeminiLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry)).GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Print answer (truncate for console)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("?? Gemini ?듬? ??");
            Console.ResetColor();
            // Remove "Gemini???묐떟" prefix if present
            var cleaned = answer.StartsWith("Gemini???묐떟") ? answer["Gemini???묐떟".Length..] : answer;
            Console.WriteLine(cleaned.Length > 2000 ? cleaned[..2000] + "\n... (truncated)" : cleaned);

            // Full answer marker (for programmatic capture by whisper study etc.)
            Console.WriteLine("[ASK_FULL_ANSWER_BEGIN]");
            Console.WriteLine(cleaned);
            Console.WriteLine("[ASK_FULL_ANSWER_END]");

            if (slackReport)
                ReportToSlack("Gemini", question, cleaned);
        }

        UnregisterWaitingTab("gemini");
        // Preserve Chrome's original state ??don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }
}

