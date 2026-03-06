using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;
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

        var targetTag = BuildAskTargetTag("gemini");
        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // Navigate to Gemini if not already there
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                if (!currentUrl.Contains("gemini.google.com"))
                {
                    Console.WriteLine("[ASK] Navigating to Gemini...");
                    await cdp.NavigateAsync("https://gemini.google.com/app");
                    await Task.Delay(3000);
                }

                // A11y-first: find editor via selector chain
                var editorSel = await WaitForEditorA11y(cdp,
                    ".ql-editor",                                   // Quill class
                    "[role='textbox'][contenteditable='true']",      // ARIA role
                    "div[contenteditable='true']"                    // Generic
                );
                if (editorSel == null)
                {
                    Console.WriteLine("[ASK] Editor not found");
                    return (false, (string?)null);
                }

                // ── Tier 1: focusless insert (a11y-first) ──
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
                for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
                {
                    // Check if editor still has text
                    var remaining = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                    if (remaining == "0" && sendAttempt > 0)
                    {
                        sendResult = $"SENT(attempt={sendAttempt})";
                        break; // editor cleared = message sent!
                    }

                    // Re-insert text if editor is empty (text didn't stick)
                    if (remaining == "0" && sendAttempt == 0)
                    {
                        await InsertTextContentEditable(cdp, editorSel, question);
                        await Task.Delay(200);
                    }

                    // A11y-first: find send button by aria-label → get bounding rect → CDP mouse click
                    var clickResult = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('button[aria-label="메시지 보내기"]')
                                   || document.querySelector('button[aria-label="Send message"]')
                                   || document.querySelector('button.send-button');
                            if (!btn || btn.disabled) return 'NO_BUTTON';
                            var r = btn.getBoundingClientRect();
                            if (r.width === 0 || r.height === 0) return 'INVISIBLE';
                            return JSON.stringify({x: Math.round(r.x + r.width/2), y: Math.round(r.y + r.height/2)});
                        })()
                        """) ?? "NO_BUTTON";

                    if (clickResult != "NO_BUTTON" && clickResult != "INVISIBLE" && clickResult.StartsWith("{"))
                    {
                        // A11y: CDP real mouse click at button center (focusless, no keyboard focus needed)
                        try
                        {
                            var coords = System.Text.Json.Nodes.JsonNode.Parse(clickResult);
                            var bx = coords?["x"]?.GetValue<int>() ?? 0;
                            var by = coords?["y"]?.GetValue<int>() ?? 0;
                            await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
                            {
                                ["type"] = "mousePressed", ["x"] = bx, ["y"] = by, ["button"] = "left", ["clickCount"] = 1
                            });
                            await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
                            {
                                ["type"] = "mouseReleased", ["x"] = bx, ["y"] = by, ["button"] = "left", ["clickCount"] = 1
                            });
                        }
                        catch { }
                    }
                    else
                    {
                        // Fallback: focusless Enter key via CDP
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

                // Wait for response — poll until text stabilizes
                string? lastText = null;
                int stableCount = 0;
                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    // A11y-first: model-response → [role='article'] → generic text
                    var text = await cdp.EvalAsync("""
                        (() => {
                            var responses = document.querySelectorAll('model-response');
                            if (responses.length === 0) {
                                var articles = document.querySelectorAll('[role="article"]');
                                responses = articles.length > 0 ? articles : responses;
                            }
                            if (responses.length === 0) return '';
                            var last = responses[responses.length - 1];
                            return last.innerText || last.textContent || '';
                        })()
                        """);

                    if (string.IsNullOrEmpty(text))
                        continue;

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

                // Timeout — return whatever we have
                if (!string.IsNullOrEmpty(lastText))
                {
                    Console.WriteLine($"[ASK] Timeout — partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout — no response");
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

        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── ChatGPT ──

    // Persona prompt injected on fresh conversation to stabilize output format.
    // Single-line to avoid ProseMirror multiline issues.
    const string ChatGptPersona =
        "You are a concise dev assistant. Reply in the same language as the question. Keep answers under 120 words. No disclaimers or filler. Confirm with: READY";

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab)
    {
        Console.WriteLine($"[ASK] ChatGPT: {question}");
        using var focusGuard = new CdpFocusGuard();

        var targetTag = BuildAskTargetTag("gpt");
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (!currentUrl.Contains("chatgpt.com"))
                {
                    Console.WriteLine("[ASK] Navigating to ChatGPT...");
                    await cdp.NavigateAsync("https://chatgpt.com");
                    await Task.Delay(3000);
                }

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

                if (existingTurns == 0)
                {
                    // Fresh conversation -- inject persona prompt first
                    Console.WriteLine("[ASK] Fresh conversation -- injecting persona...");
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, ChatGptPersona.Trim(), timeoutSec: 20);
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

                // Send the actual question
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec);
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
        var currentUrl = await cdp.EvalAsync("location.href") ?? "";

        // Count existing turns (multi-selector: ChatGPT DOM changes frequently)
        int prevTurns = await CountChatGptTurns(cdp);

        // ── A11y-first: find editor via selector chain ──
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // ── Tier 1: focusless insert (a11y-first) ──
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

        // ── A11y-first: find send button → CDP real mouse click → Enter fallback ──
        await Task.Delay(500);
        var sendResult = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="send-button"]')
                       || document.querySelector('button[aria-label*="보내기"]')
                       || document.querySelector('button[aria-label*="Send"]');
                if (!btn || btn.disabled) return 'NO_BTN';
                var r = btn.getBoundingClientRect();
                if (r.width === 0 || r.height === 0) return 'INVISIBLE';
                return JSON.stringify({x: Math.round(r.x + r.width/2), y: Math.round(r.y + r.height/2)});
            })()
            """) ?? "NO_BTN";

        if (sendResult != "NO_BTN" && sendResult != "INVISIBLE" && sendResult.StartsWith("{"))
        {
            // A11y: CDP real mouse click at button center
            try
            {
                var coords = System.Text.Json.Nodes.JsonNode.Parse(sendResult);
                var bx = coords?["x"]?.GetValue<int>() ?? 0;
                var by = coords?["y"]?.GetValue<int>() ?? 0;
                await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
                {
                    ["type"] = "mousePressed", ["x"] = bx, ["y"] = by, ["button"] = "left", ["clickCount"] = 1
                });
                await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
                {
                    ["type"] = "mouseReleased", ["x"] = bx, ["y"] = by, ["button"] = "left", ["clickCount"] = 1
                });
                sendResult = "CLICKED";
            }
            catch { sendResult = "CLICK_FAIL"; }
        }

        if (sendResult != "CLICKED")
        {
            // Fallback: focusless Enter key
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
            sendResult = "ENTER";
        }

        // Check editor after send — should be empty if sent successfully
        var afterSend = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
        Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={prevTurns})");

        // Wait for new assistant turn
        var sw = Stopwatch.StartNew();
        bool newTurnAppeared = false;
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

            var c = await CountChatGptTurns(cdp);
            if (c > prevTurns) { newTurnAppeared = true; break; }
            var cur = c.ToString();

            if (sw.Elapsed.TotalSeconds > 3)
                Console.WriteLine($"[ASK] Waiting for turn... (now={cur}, prev={prevTurns}, {sw.Elapsed.TotalSeconds:F0}s)");
        }
        if (!newTurnAppeared)
        {
            Console.WriteLine("[ASK] No new turn");
            return (false, null);
        }

        // Poll until streaming finishes + text stabilizes
        string? lastText = null;
        int stableCount = 0;
        sw.Restart();

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(1500);
            var stateJson = await cdp.EvalAsync("""
                (() => {
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label="Stop streaming"]')
                            || document.querySelector('button[aria-label="스트리밍 중지"]');
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('[data-testid*="conversation-turn"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    if (msgs.length === 0) return JSON.stringify({s:!!stop,t:''});
                    var last = msgs[msgs.length - 1];
                    // A11y-first text extraction: skip result-thinking, prefer result-streaming or plain .markdown
                    var md = last.querySelector('.markdown:not(.result-thinking)')
                          || last.querySelector('.result-streaming')
                          || last.querySelector('.markdown');
                    var txt = md ? (md.innerText || md.textContent) : '';
                    if (!txt) txt = last.innerText || last.textContent || '';
                    if (!txt) { var lbl = last.getAttribute('aria-label'); if (lbl) txt = lbl; }
                    // If only result-thinking exists, treat as streaming (still generating)
                    if (!txt && last.querySelector('.result-thinking')) stop = true;
                    return JSON.stringify({s:!!stop,t:txt||''});
                })()
                """);

            if (string.IsNullOrEmpty(stateJson)) continue;

            string text = "";
            bool streaming = false;
            try
            {
                var node = System.Text.Json.Nodes.JsonNode.Parse(stateJson);
                streaming = node?["s"]?.GetValue<bool>() ?? false;
                text = node?["t"]?.GetValue<string>() ?? "";
            }
            catch { continue; }

            if (streaming) { lastText = text; stableCount = 0; continue; }

            if (!string.IsNullOrWhiteSpace(text))
            {
                if (text == lastText)
                {
                    stableCount++;
                    if (stableCount >= 2)
                    {
                        Console.WriteLine($"[ASK] Response ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                        return (true, text);
                    }
                }
                else { stableCount = 0; lastText = text; }
            }
            // Silent wait — DOM debug logging removed
        }

        if (!string.IsNullOrEmpty(lastText))
        {
            Console.WriteLine($"[ASK] Timeout -- partial ({lastText.Length} chars)");
            return (true, lastText);
        }
        Console.WriteLine("[ASK] Timeout -- no response");
        return (false, null);
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
