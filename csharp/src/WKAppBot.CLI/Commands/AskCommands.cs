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
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

// partial class: wkappbot ask <ai> "question" — one-command AI Q&A via WebBot
internal partial class Program
{
    static int AskCommand(string[] args)
    {
        if (args.Length < 2)
            return AskUsage();

        var ai = args[0].ToLowerInvariant();
        // Collect question (everything after AI name, excluding flags)
        var questionParts = new List<string>();
        bool slackReport = false;
        bool newTab = false;
        int timeoutSec = 30;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--slack")
                slackReport = true;
            else if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else
                questionParts.Add(args[i]);
        }
        var question = string.Join(" ", questionParts);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        return ai switch
        {
            "gemini" => AskGemini(question, slackReport, timeoutSec, newTab),
            "gpt" or "chatgpt" => AskChatGpt(question, slackReport, timeoutSec, newTab),
            _ => Error($"Unknown AI: {ai} (use gemini or gpt)")
        };
    }

    static int AskUsage()
    {
        Console.WriteLine(@"
WKAppBot Ask — one-command AI Q&A via WebBot

Usage:
  wkappbot ask gemini ""question""  [--slack] [--timeout 30] [--new-tab]
  wkappbot ask gpt ""question""     [--slack] [--timeout 30] [--new-tab]

Options:
  --slack       Report answer to Slack channel
  --timeout N   Max seconds to wait for response (default: 30)
  --new-tab     Open in a new tab (default: reuse existing tab)

Examples:
  wkappbot ask gemini ""오늘 코스피 특징주 알려줘""
  wkappbot ask gpt ""이 패턴 분석해줘"" --slack
  wkappbot ask gpt ""새 탭으로 테스트"" --new-tab
");
        return 1;
    }

    // ── GrapHelper tab routing: find & switch browser tab via UIA before CDP ──

    /// <summary>
    /// Find a browser tab by title substring and switch to it via UIA (focusless).
    /// Returns true if the tab was found and selected.
    /// This runs BEFORE CDP connection — ensures the right tab is active so CDP
    /// connects to the correct page without complex URL-matching logic.
    /// </summary>
    static bool EnsureTabViaGrap(string browserGrap, string tabPattern)
    {
        try
        {
            using var automation = new UIA3Automation();
            var resolved = GrapHelper.ResolveFullGrap(browserGrap, automation);
            if (resolved == null || resolved.Value.error != null)
            {
                Console.WriteLine($"[ASK] GrapHelper: browser not found ({browserGrap})");
                return false;
            }

            var (_, root, _) = resolved.Value;
            var tab = GrapHelper.FindByNameOrAid(root, tabPattern);
            if (tab == null)
            {
                Console.WriteLine($"[ASK] GrapHelper: tab \"{tabPattern}\" not found");
                return false;
            }

            if (!GrapHelper.IsTabItem(tab))
            {
                // Found something but it's not a TabItem — might be web content already visible
                Console.WriteLine($"[ASK] GrapHelper: \"{tabPattern}\" matched but not a TabItem (already active?)");
                return true;
            }

            return GrapHelper.SwitchToTab(tab);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] GrapHelper tab routing failed: {ex.Message}");
            return false;
        }
    }

    sealed record CdpPageTarget(string Id, string Url, string Title, string WebSocketDebuggerUrl);

    // Stable tag per session+provider — reuses the same tab across CLI invocations within a session
    // Format: {provider}-{sessionHash} (e.g. "gemini-a1b2c3d4")
    static string BuildAskTargetTag(string provider)
    {
        var hash = GetSessionTag() ?? "default";
        return $"{provider}-{hash}";
    }

    /// <summary>
    /// Connect to CDP, launching Chrome if needed.
    /// Uses AskTargetRegistry + EnsureCorrectWindowAsync for tab reuse.
    /// Focus guard thread prevents Chrome from stealing keyboard focus.
    /// After setup, minimizes Chrome window — CDP works perfectly minimized,
    /// and this prevents all focus theft issues.
    /// </summary>
    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false, string? targetTag = null)
    {
        var task = Task.Run(async () =>
        {
            try
            {
                var active = await ChromeLauncher.IsPortActiveAsync(port);
                if (!active)
                {
                    var launchUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    Console.WriteLine($"[ASK] Launching Chrome → {launchUrl ?? "about:blank"}...");
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl);
                    await Task.Delay(2500);
                }

                var cdp = new CdpClient();
                await cdp.ConnectAsync(port, timeoutMs: 5000);

                // ── Cleanup: close all about:blank tabs ──
                await CloseBlankTabs(port);

                // Look up saved target from registry (survives across CLI invocations)
                var savedTargetId = !string.IsNullOrWhiteSpace(targetTag) ? AskTargetRegistry.GetTargetId(targetTag) : null;
                if (savedTargetId != null)
                    Console.WriteLine($"[ASK] Registry hit: {targetTag} → {savedTargetId[..Math.Min(8, savedTargetId.Length)]}");

                var navigateUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                var resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, navigateUrl,
                    savedTargetId: savedTargetId);

                // ── Tab URL validation: reject about:blank, verify correct host ──
                if (!string.IsNullOrWhiteSpace(preferredHost))
                {
                    var tabUrl = await cdp.EvalAsync("location.href") ?? "";
                    if (tabUrl == "about:blank" || !tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[ASK] Wrong tab: {tabUrl} (expected {preferredHost})");

                        if (tabUrl == "about:blank")
                        {
                            // about:blank is useless — close it and invalidate registry
                            Console.WriteLine("[ASK] Closing about:blank...");
                            try { await cdp.EvalAsync("window.close()"); } catch { }
                            await Task.Delay(500);
                        }

                        // Invalidate saved target
                        if (!string.IsNullOrWhiteSpace(targetTag))
                            AskTargetRegistry.SetTargetId(targetTag, null!);

                        // Reconnect — find or create correct tab
                        Console.WriteLine($"[ASK] Reconnecting to {preferredHost}...");
                        await cdp.ConnectAsync(port, timeoutMs: 5000);
                        resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag,
                            $"https://{preferredHost}", savedTargetId: null);
                        await Task.Delay(2000);

                        tabUrl = await cdp.EvalAsync("location.href") ?? "";
                        if (!tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                        {
                            await cdp.NavigateAsync($"https://{preferredHost}");
                            await Task.Delay(3000);
                        }
                        Console.WriteLine($"[ASK] Now on: {await cdp.EvalAsync("location.href")}");
                    }
                }

                // Save resolved target to registry for next invocation
                if (resolvedId != null && !string.IsNullOrWhiteSpace(targetTag))
                {
                    AskTargetRegistry.SetTargetId(targetTag, resolvedId);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[ASK] Target: {targetTag} → {resolvedId[..Math.Min(8, resolvedId.Length)]}");
                    Console.ResetColor();
                }

                // Don't minimize here — CDP Input.dispatch* needs a visible viewport.
                // CdpFocusGuard handles focus theft. Minimize after interaction completes.

                return (CdpClient?)cdp;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Failed to connect: {ex.Message}");
                return (CdpClient?)null;
            }
        });
        return task.GetAwaiter().GetResult();
    }

    static async Task<List<CdpPageTarget>> GetPageTargetsAsync(int port)
    {
        var result = new List<CdpPageTarget>();
        try
        {
            using var http = new HttpClient();
            var json = await http.GetStringAsync($"http://localhost:{port}/json");
            var arr = JsonSerializer.Deserialize<JsonArray>(json);
            if (arr == null) return result;

            foreach (var node in arr)
            {
                var type = node?["type"]?.GetValue<string>() ?? "";
                if (!string.Equals(type, "page", StringComparison.OrdinalIgnoreCase))
                    continue;

                var ws = node?["webSocketDebuggerUrl"]?.GetValue<string>() ?? "";
                if (string.IsNullOrWhiteSpace(ws))
                    continue;

                var url = node?["url"]?.GetValue<string>() ?? "";
                var title = node?["title"]?.GetValue<string>() ?? "";
                var id = node?["id"]?.GetValue<string>() ?? string.Empty;
                result.Add(new CdpPageTarget(id, url, title, ws));
            }
        }
        catch { }
        return result;
    }

    /// <summary>Insert text into contentEditable editor (Quill/ProseMirror universal pattern).</summary>
    static async Task<bool> InsertTextContentEditable(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");

        // Tier 1: focusless — set innerHTML + dispatch InputEvent (React/ProseMirror picks it up)
        var focusless = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                var p = el.querySelector('p');
                if (p) { p.textContent = '{{escaped}}'; }
                else { el.innerHTML = '<p>{{escaped}}</p>'; }
                el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:'{{escaped}}'}));
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        var result = await cdp.EvalAsync(focusless);
        if (result == "OK") return true;

        // Tier 2: focus + execCommand (classic approach)
        var js = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                var sel = window.getSelection();
                var range = document.createRange();
                range.selectNodeContents(el);
                range.collapse(false);
                sel.removeAllRanges();
                sel.addRange(range);
                document.execCommand('insertText', false, '{{escaped}}');
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        result = await cdp.EvalAsync(js);
        return result == "OK";
    }

    /// <summary>Clear contentEditable editor.</summary>
    static async Task ClearContentEditable(CdpClient cdp, string selector)
    {
        await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return;
                el.focus();
                document.execCommand('selectAll');
                document.execCommand('delete');
            })()
            """);
    }

    // ── Gemini ──

    static int AskGemini(string question, bool slackReport, int timeoutSec, bool newTab)
    {
        Console.WriteLine($"[ASK] Gemini: {question}");
        using var focusGuard = new CdpFocusGuard();

        // UIA tab routing: switch to Gemini tab before CDP connects (focusless)
        if (!newTab) EnsureTabViaGrap("Chrome", "Gemini");

        var targetTag = BuildAskTargetTag("gemini");
        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate (iconified OK — CDP works without rendering) ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (!currentUrl.Contains("gemini.google.com"))
                {
                    Console.WriteLine("[ASK] Navigating to Gemini...");
                    await cdp.NavigateAsync("https://gemini.google.com/app");
                    await Task.Delay(3000);
                }
                else
                {
                    Console.WriteLine($"[ASK] Reusing Gemini session");
                }

                // Activate tab (no lock needed — just internal tab switch)
                try
                {
                    var prevFg = NativeMethods.GetForegroundWindow();
                    await cdp.BringToFrontAsync();
                    if (prevFg != IntPtr.Zero) NativeMethods.SetForegroundWindow(prevFg);
                    Console.WriteLine("[ASK] Tab activated (BringToFront OK)");
                }
                catch (Exception btfEx)
                {
                    Console.WriteLine($"[ASK] BringToFront failed: {btfEx.Message}");
                }

                // Diagnose tab state before editor search
                var hiddenState = await cdp.EvalAsync("document.hidden + '|' + document.title + '|' + document.querySelectorAll('*').length");
                Console.WriteLine($"[ASK] Tab state: {hiddenState}");

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

                // ── Persona injection on fresh Gemini conversation ──
                var geminiTurnCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                if (geminiTurnCount == "0")
                {
                    // ── Browser exclusive: persona input → send complete ──
                    using var personaLock = ChromeTabLock.Acquire("Gemini/persona");
                    if (personaLock == null) return (false, (string?)null);

                    Console.WriteLine("[ASK] Fresh Gemini — injecting persona...");
                    await ClearContentEditable(cdp, editorSel);
                    await InsertTextContentEditable(cdp, editorSel, AskPersona);
                    await Task.Delay(300);

                    // Send persona (button click → Enter fallback)
                    var personaSent = false;
                    for (int ps = 0; ps < 5; ps++)
                    {
                        var rem = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                        if (rem == "0" && ps > 0) { personaSent = true; break; }
                        await cdp.EvalAsync("document.querySelector('button[aria-label=\"Send message\"], button[aria-label=\"메시지 보내기\"], .send-button, button.send-button')?.click()");
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
                        // Wait for Gemini to respond to persona (no lock needed — just polling)
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
                                    var stop = document.querySelector('button[aria-label="응답 중지"], button[aria-label="Stop response"], button[aria-label="Stop"]');
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

                // ── Browser exclusive: question input → send complete ──
                using var questionLock = ChromeTabLock.Acquire("Gemini");
                if (questionLock == null) return (false, (string?)null);

                // Tier 1: focusless insert (a11y-first)
                await ClearContentEditable(cdp, editorSel);
                var inserted = await InsertTextContentEditable(cdp, editorSel, question);
                if (!inserted)
                {
                    // ── Tier 2: CDP Input.insertText (needs focus) ──
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
                        Console.WriteLine("[ASK] Failed to insert text");
                        return (false, (string?)null);
                    }
                }

                // Send: a11y-first (CDP real click on button) → focusless Enter fallback
                // Keep trying until editor is empty (= message sent)
                await Task.Delay(300);
                var sendResult = "PENDING";
                // Count model-responses before sending — detect response start as send confirmation
                var preResponseCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";

                for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
                {
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

                    // JS click() — works even when Chrome is minimized (no viewport needed)
                    var clickResult = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('button[aria-label="메시지 보내기"]')
                                   || document.querySelector('button[aria-label="Send message"]')
                                   || document.querySelector('button.send-button');
                            if (!btn || btn.disabled) return 'NO_BUTTON';
                            btn.click();
                            return 'CLICKED';
                        })()
                        """) ?? "NO_BUTTON";

                    if (clickResult != "CLICKED")
                    {
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

                Console.WriteLine($"[ASK] Sent! Waiting for response... (send={sendResult})");
                questionLock.Release("sent");

                // Count existing responses before polling (skip persona's READY etc.)
                var preCountStr = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                int baseResponseCount = int.TryParse(preCountStr, out var brc) ? brc : 0;

                // Register for tab handoff + activate peer's tab (we'll poll with textContent)
                RegisterWaitingTab("gemini", cdp);
                await HandoffTabToPeer("gemini");

                // Wait for response — poll until text stabilizes
                string? lastText = null;
                int stableCount = 0;
                int lastTextLen = 0;
                var sw = Stopwatch.StartNew();

                int geminiBlankCount = 0;
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    // A11y-first: model-response → [role='article'] → generic text
                    // Only read NEW responses (skip persona exchange)
                    var text = await cdp.EvalAsync(
                        "(() => {" +
                        "if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100) return '\\x01BLANK';" +
                        "var responses = document.querySelectorAll('model-response');" +
                        "if (responses.length === 0) { var articles = document.querySelectorAll('[role=\"article\"]'); responses = articles.length > 0 ? articles : responses; }" +
                        $"if (responses.length <= {baseResponseCount}) return '';" +
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
                            Console.WriteLine("[ASK] Page unresponsive — aborting poll");
                            break;
                        }
                        continue;
                    }
                    geminiBlankCount = 0;

                    if (string.IsNullOrEmpty(text))
                        continue;

                    // Streaming handoff: text growing → this tab is alive, give active tab to peer
                    if (text.Length > lastTextLen && lastTextLen > 0)
                        await HandoffTabToPeer("gemini");
                    lastTextLen = text.Length;

                    // Check if response is still generating
                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 2) // stable for 4+ seconds
                        {
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }
                    }
                    else
                    {
                        stableCount = 0;
                        lastText = text;
                    }
                }

                // Done — hand off tab to peer if still waiting
                await HandoffTabToPeer("gemini");

                // Timeout — return whatever we have
                if (!string.IsNullOrEmpty(lastText))
                {
                    Console.WriteLine($"[ASK] Timeout — partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout — no response, retrying once...");

                // Retry: same page, re-insert and send (no reload, keeps session)
                await Task.Delay(1000);
                await ClearContentEditable(cdp, editorSel);
                await InsertTextContentEditable(cdp, editorSel, question);
                await Task.Delay(300);
                await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label="메시지 보내기"]')
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
                    if (text == retryText) { Console.WriteLine($"[ASK] Retry: response ({text.Length} chars)"); return (true, text); }
                    retryText = text;
                }
                if (!string.IsNullOrEmpty(retryText))
                {
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

        if (ok && answer != null)
        {
            // Print answer (truncate for console)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── Gemini 답변 ──");
            Console.ResetColor();
            // Remove "Gemini의 응답" prefix if present
            var cleaned = answer.StartsWith("Gemini의 응답") ? answer["Gemini의 응답".Length..] : answer;
            Console.WriteLine(cleaned.Length > 2000 ? cleaned[..2000] + "\n... (truncated)" : cleaned);

            if (slackReport)
                ReportToSlack("Gemini", question, cleaned);
        }

        UnregisterWaitingTab("gemini");
        // Preserve Chrome's original state — don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── ChatGPT ──

    // Shared persona for external AI agents (ChatGPT, Gemini).
    // Injected on fresh conversations to stabilize output format.
    // Single-line to avoid ProseMirror/Quill multiline issues.
    const string AskPersona =
        "You are a senior dev consultant called by another AI agent (Claude) via CLI automation. " +
        "I will ask you planning, debugging, and architecture questions. " +
        "Rules: (1) Always reply in English for token efficiency (the caller translates if needed). " +
        "(2) Answer as if YOU were doing the task — give concrete steps, actual commands, and real code. " +
        "(3) No disclaimers, no filler, no follow-up questions. " +
        "(4) For planning: numbered steps with specific commands/tools. " +
        "(5) For code: ONLY the code, no explanation unless asked. " +
        "(6) Keep answers under 150 words unless the question demands more. " +
        "(7) Confirm you understood with exactly: READY";

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab)
    {
        Console.WriteLine($"[ASK] ChatGPT: {question}");
        using var focusGuard = new CdpFocusGuard();

        // UIA tab routing: switch to ChatGPT tab before CDP connects (focusless)
        if (!newTab) EnsureTabViaGrap("Chrome", "ChatGPT");

        var targetTag = BuildAskTargetTag("gpt");
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate (iconified OK) ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (!currentUrl.Contains("chatgpt.com"))
                {
                    Console.WriteLine("[ASK] Navigating to ChatGPT...");
                    await cdp.NavigateAsync("https://chatgpt.com");
                    await Task.Delay(3000);
                }

                // Activate tab (no lock needed — just internal tab switch)
                var prevFg = NativeMethods.GetForegroundWindow();
                await cdp.BringToFrontAsync();
                if (prevFg != IntPtr.Zero) NativeMethods.SetForegroundWindow(prevFg);

                // Wait for ProseMirror editor
                var editorSel = await WaitForChatGptEditorA11y(cdp);
                if (editorSel == null)
                    return (false, (string?)null);
                Console.WriteLine($"[ASK] Editor found: {editorSel}");

                // Check if this is a fresh conversation — try multiple selectors
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

                // Reuse existing session — only inject persona on fresh (0 turns) conversations
                if (existingTurns > 0)
                    Console.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                if (existingTurns == 0)
                {
                    // Fresh conversation -- inject persona prompt first
                    Console.WriteLine("[ASK] Fresh conversation -- injecting persona...");
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, AskPersona.Trim(), timeoutSec: 20);
                    if (!personaOk)
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                    else
                    {
                        bool ready = (personaResp ?? "").Contains("READY", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : personaResp?.Substring(0, Math.Min(50, personaResp.Length)))}");
                    }
                }

                // Re-wait for editor readiness after persona exchange
                if (!await WaitForChatGptEditor(cdp))
                    return (false, (string?)null);
                await Task.Delay(1000); // Let ChatGPT UI settle

                // Send the actual question (with 9s-delay retry on timeout)
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec);
                if (!ok && string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("[ASK] ChatGPT timeout — retrying in 9 seconds...");
                    await Task.Delay(9000);
                    (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec);
                }
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
        // Preserve Chrome's original state — don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // A11y-first selector chain for ChatGPT editor (most stable → least stable)
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

    /// <summary>Count assistant turns — multi-selector for ChatGPT DOM changes.</summary>
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
        CdpClient cdp, string message, int timeoutSec)
    {
        // ── Phase 0: URL check + turn count (iconified OK) ──
        var currentUrl = await cdp.EvalAsync("location.href") ?? "";
        int prevTurns = await CountChatGptTurns(cdp);

        // Activate tab (no lock needed)
        var prevFg = NativeMethods.GetForegroundWindow();
        await cdp.BringToFrontAsync();
        if (prevFg != IntPtr.Zero) NativeMethods.SetForegroundWindow(prevFg);

        // ── A11y-first: find editor via selector chain ──
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // ── Browser exclusive zone: prompt input → first response turn ──
        using var chatLock = ChromeTabLock.Acquire("ChatGPT");
        if (chatLock == null) return (false, null);

        // Tier 1: focusless insert (a11y-first)
        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        // Verify what's actually in the editor
        var editorContent = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
        Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
        if (!inserted)
        {
            // ── Tier 2: CDP Input.insertText (needs focus) ──
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

        // ── Send: JS click → verify → CDP Enter fallback ──
        await Task.Delay(500);
        var sendResult = "PENDING";

        // Tier 1: JS click (works minimized, but React may ignore .click())
        var jsClick = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="send-button"]')
                       || document.querySelector('button[aria-label*="보내기"]')
                       || document.querySelector('button[aria-label*="Send"]');
                if (!btn || btn.disabled) return 'NO_BTN';
                btn.click();
                return 'CLICKED';
            })()
            """) ?? "NO_BTN";

        if (jsClick == "CLICKED")
        {
            await Task.Delay(500);
            var remaining = await cdp.EvalAsync(
                $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
            sendResult = remaining == "0" ? "JS_CLICK" : "CLICK_NOOP";
        }

        // Tier 2: UIA Invoke on send button (focusless, works minimized)
        if (sendResult != "JS_CLICK")
        {
            Console.WriteLine("[ASK] JS click didn't send, trying UIA invoke...");
            if (TryUiaInvokeSendButton())
            {
                await Task.Delay(500);
                var remaining = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                sendResult = remaining == "0" ? "UIA_INVOKE" : "UIA_NOOP";
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

        // Check editor after send — should be empty if sent successfully
        var afterSend = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
        Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={prevTurns})");

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

        // ── Poll Phase 1: wait for streaming/thinking to finish (iconified — no rendering needed) ──
        int streamExtensions = 0;
        int blankPageCount = 0;
        sw.Restart();

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(1500);

            // Lightweight check: is streaming/thinking still active?
            // Also validates page health and checks if any response text has appeared.
            var stateJson = await cdp.EvalAsync("""
                (() => {
                    if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100)
                        return 'BLANK';
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label="Stop streaming"]')
                            || document.querySelector('button[aria-label="스트리밍 중지"]');
                    var thinking = !!document.querySelector('.result-thinking');
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    // textContent (not innerText) — works in background tabs without layout/rendering
                    var hasText = msgs.length > 0 && (msgs[msgs.length-1].textContent||'').trim().length > 0;
                    if (!stop && !thinking) return hasText ? 'DONE' : 'DONE_EMPTY';
                    if (thinking) return hasText ? 'THINKING_HAS_TEXT' : 'THINKING';
                    return hasText ? 'STREAMING_HAS_TEXT' : 'STREAMING';
                })()
                """) ?? "";

            if (stateJson == "DONE" || stateJson == "DONE_EMPTY")
            {
                Console.WriteLine($"[ASK] Streaming complete ({sw.Elapsed.TotalSeconds:F0}s)");
                break;
            }

            // Blank/broken page detection — bail out early
            if (stateJson == "BLANK" || string.IsNullOrEmpty(stateJson))
            {
                blankPageCount++;
                Console.WriteLine($"[ASK] Page blank/broken ({blankPageCount}/3), {sw.Elapsed.TotalSeconds:F0}s");
                if (blankPageCount >= 3)
                {
                    Console.WriteLine("[ASK] Page unresponsive — aborting poll");
                    break;
                }
                continue;
            }

            blankPageCount = 0; // reset on valid response

            // First-byte timeout: 20s of streaming with no text → likely stuck
            // Exempt: THINKING state (o3/o4 models can think for 30s+ before first text)
            bool isThinking = stateJson == "THINKING" || stateJson == "THINKING_HAS_TEXT";
            bool hasResponseText = stateJson == "STREAMING_HAS_TEXT" || stateJson == "THINKING_HAS_TEXT";

            // Streaming handoff: response text appearing → give active tab to peer
            if (hasResponseText)
                await HandoffTabToPeer("chatgpt");

            if (!hasResponseText && !isThinking && sw.Elapsed.TotalSeconds >= 20)
            {
                Console.WriteLine("[ASK] No response text after 20s — aborting (first-byte timeout)");
                break;
            }

            Console.WriteLine($"[ASK] Poll: {(isThinking ? "thinking" : "streaming")}{(hasResponseText ? "+" : "")}, {sw.Elapsed.TotalSeconds:F0}s");

            // Extend timeout while actively streaming/thinking (max 1 extension = 2x original timeout)
            if (sw.Elapsed.TotalSeconds > timeoutSec * 0.8 && streamExtensions < 1)
            {
                streamExtensions++;
                Console.WriteLine("[ASK] Still streaming/thinking, extending timeout...");
                sw.Restart();
            }
        }

        // ── Poll Phase 2: text extraction ──
        // BringToFront so innerText (which needs layout) works properly
        try
        {
            var prevFg2 = NativeMethods.GetForegroundWindow();
            await cdp.BringToFrontAsync();
            if (prevFg2 != IntPtr.Zero) NativeMethods.SetForegroundWindow(prevFg2);
        }
        catch { }
        await Task.Delay(300); // let React hydrate after tab activation

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

                    // Fallback: innerHTML → strip tags
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

            // Done — hand off tab to peer if still waiting
            await HandoffTabToPeer("chatgpt");

            // ── Final answer ready: focusless restore to show answer to user ──
            ShowChromeAnswer(cdp);

            return (true, finalText);
        }
        Console.WriteLine("[ASK] Timeout -- no response");
        return (false, null);
    }

    /// <summary>
    /// 최종 답변 표시: Chrome을 포커스리스 리스토어하여 사용자에게 답변 페이지를 보여준다.
    /// ask 플로우 전체는 아이콘화 상태로 진행 (CDP는 미니마이즈에서 정상 동작).
    /// 최종 답변 추출 후에만 호출하여 결과를 시각적으로 확인할 수 있게 한다.
    /// </summary>
    static void ShowChromeAnswer(CdpClient cdp)
    {
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero || !NativeMethods.IsIconic(chromeHwnd))
            return; // not minimized — already visible

        var title = WKAppBot.Win32.Window.WindowFinder.GetWindowText(chromeHwnd);
        Console.WriteLine($"[ASK] Showing answer — focusless restore \"{title}\"");

        // Focusless restore: SW_RESTORE + immediately give focus back to previous window
        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.ShowWindow(chromeHwnd, 9); // SW_RESTORE
        if (prevFg != IntPtr.Zero && prevFg != chromeHwnd)
            NativeMethods.SetForegroundWindow(prevFg);
    }

    // ── UIA Send Button ──
    // Tier 2 fallback: find and invoke the send button via UIA (completely focusless).
    // Searches Chrome/ChatGPT windows for buttons matching send-related names.
    static readonly string[] SendButtonNames = ["제출", "보내기", "Send message", "Send", "메시지 보내기"];

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

    // ── Focus Guard ──
    // Polls foreground window every 50ms and restores if Chrome steals it.
    // Wraps the entire ask flow so the user's keyboard focus is never lost.

    sealed class CdpFocusGuard : IDisposable
    {
        readonly IntPtr _protectedHwnd;
        readonly CancellationTokenSource _cts = new();
        readonly Task _task;
        int _theftCount;

        public CdpFocusGuard()
        {
            _protectedHwnd = NativeMethods.GetForegroundWindow();
            _task = Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        var fg = NativeMethods.GetForegroundWindow();
                        if (_protectedHwnd != IntPtr.Zero && fg != _protectedHwnd && fg != IntPtr.Zero)
                        {
                            NativeMethods.SetForegroundWindow(_protectedHwnd);
                            Interlocked.Increment(ref _theftCount);
                        }
                    }
                    catch { }
                    await Task.Delay(50);
                }
            });
        }

        public void Dispose()
        {
            _cts.Cancel();
            try { _task.Wait(500); } catch { }
            _cts.Dispose();
            var count = _theftCount;
            if (count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[FOCUS] Guard blocked {count} focus theft(s)");
                Console.ResetColor();
            }
        }
    }

    // ── Tab Handoff: async 상부상조 ──
    // When one AI's response starts, activate the other AI's tab via BringToFrontAsync.
    // BringToFront = Chrome-internal tab switch only (no OS focus change = fully focusless).
    // The streaming AI polls with textContent (works in background), so it doesn't need active tab.
    // The waiting AI gets active tab → React renders at full speed → faster first-byte.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, CdpClient> _waitingTabs = new();

    /// <summary>
    /// Register this AI's tab as "waiting for response" — eligible for tab handoff.
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
    /// Activate another waiting AI's tab (focusless — Chrome internal tab switch only).
    /// Called when this AI's response starts streaming (= no longer needs active tab).
    /// </summary>
    static async Task HandoffTabToPeer(string myName)
    {
        foreach (var (name, cdp) in _waitingTabs)
        {
            if (string.Equals(name, myName, StringComparison.OrdinalIgnoreCase)) continue;
            try
            {
                await cdp.BringToFrontAsync();
                Console.WriteLine($"[ASK] Tab handoff: {myName} → {name} (focusless)");
                return;
            }
            catch { }
        }
    }

    // ── Chrome Tab Lock ──
    // Cross-process mutex: only one ask command can own the browser at a time.
    // Acquired before prompt input, auto-released after 9s or when first byte arrives.
    // IDisposable — always safe, no manual release needed.

    // Semaphore (not Mutex) — async/await can resume on different threads,
    // and Mutex.ReleaseMutex() requires the same thread. Semaphore is thread-agnostic.
    static readonly Semaphore ChromeTabSemaphore = new(1, 1, @"Global\WKAppBot_ChromeTabLock");

    sealed class ChromeTabLock : IDisposable
    {
        readonly string _aiName;
        readonly Timer _autoRelease;
        int _released;

        ChromeTabLock(string aiName)
        {
            _aiName = aiName;
            // Auto-release after 9 seconds (safety net — prevents deadlock)
            _autoRelease = new Timer(_ => Release("auto-9s"), null, 9000, Timeout.Infinite);
        }

        public static ChromeTabLock? Acquire(string aiName, int timeoutMs = 15000)
        {
            Console.WriteLine($"[ASK] Waiting for browser lock ({aiName})...");
            try
            {
                if (ChromeTabSemaphore.WaitOne(timeoutMs))
                {
                    Console.WriteLine($"[ASK] Browser lock acquired ({aiName})");
                    return new ChromeTabLock(aiName);
                }
            }
            catch (AbandonedMutexException)
            {
                Console.WriteLine($"[ASK] Browser lock recovered ({aiName})");
                return new ChromeTabLock(aiName);
            }
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
