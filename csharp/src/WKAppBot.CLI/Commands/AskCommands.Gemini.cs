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

    // ?�?�?Gemini ?�?�?
    static readonly string[] GeminiStopNoticeKeywords =
    [
        "������ �����Ǿ����ϴ�",
        "������ ����",
        "�����?�����Ǿ����ϴ�",
        "�����Ǿ����ϴ�",
        "response was stopped",
        "response stopped",
        "stopped response"
    ];

    static bool IsGeminiStoppedNotice(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        return GeminiStopNoticeKeywords.Any(k => text.Contains(k, StringComparison.OrdinalIgnoreCase));
    }

    /// Strip "Gemini�� ����" UI label prepended by Gemini web UI.
    /// Applied at the source so it doesn't appear in loop context, Slack, or console.
    static string StripGeminiUiPrefix(string text)
    {
        if (!text.StartsWith("Gemini")) return text;
        var eo = text.IndexOf('\uC751'); // ��
        if (eo > 0 && eo < 15 && eo + 1 < text.Length && text[eo + 1] == '\uB2F5') // ��
            return text[(eo + 2)..].TrimStart();
        return text;
    }

    // Wait for stop to clear naturally ? never clicks stop (preserves ongoing generation)
    static async Task<bool> WaitWhileGeminiStopVisibleNoClickAsync(CdpClient cdp, int maxWaitMs = 30000)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            if (!await cdp.IsStopButtonVisibleAsync()) return true;
            Console.WriteLine($"[ASK] Gemini generating... waiting ({sw.ElapsedMilliseconds}ms)");
            await Task.Delay(1000);
        }
        Console.WriteLine("[ASK] Gemini still generating after wait ? proceeding anyway");
        return false;
    }

    static async Task<string> GetGeminiLastResponseAsync(CdpClient cdp)
    {
        return await cdp.GetLastResponseTextAsync() ?? "";
    }

    static async Task<bool> WaitWhileGeminiStopVisibleAsync(CdpClient cdp, int maxWaitMs = 12000)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            var stopVisible = await cdp.EvalAsync("""
                (() => {
                    var s1 = document.querySelector('button[aria-label*="Stop"]');
                    var s2 = document.querySelector('button[aria-label*="����"]');
                    if (s1) return 'BTN:' + (s1.getAttribute('aria-label') || '?');
                    if (s2) return 'BTN:' + (s2.getAttribute('aria-label') || '?');
                    // mat-icon stop_circle: only count if parent button has stop-related aria-label
                    var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                    if (mat) {
                        var btn = mat.closest('button');
                        if (btn) {
                            var lbl = (btn.getAttribute('aria-label') || btn.title || '').toLowerCase();
                            if (lbl.includes('stop') || lbl.includes('����') || lbl.includes('halt'))
                                return 'MAT:' + lbl;
                        }
                    }
                    return '0';
                })()
                """) ?? "0";
            if (stopVisible == "0")
                return true;

            Console.WriteLine($"[ASK] Gemini stop visible [{stopVisible}]; waiting before send... ({sw.ElapsedMilliseconds}ms)");
            await Task.Delay(700);
        }

        // Timed out ? try clicking the stop button to cancel ongoing generation, then wait briefly
        Console.WriteLine("[ASK] Gemini stop still visible ? clicking stop to cancel generation...");
        await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[aria-label*="Stop"]')
                       || document.querySelector('button[aria-label*="����"]');
                if (!btn) {
                    var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                    if (mat) btn = mat.closest('button');
                }
                if (btn) btn.click();
            })()
            """);
        await Task.Delay(1500);
        var stillVisible = await cdp.IsStopButtonVisibleAsync();
        Console.WriteLine($"[ASK] Gemini stop after click: {(stillVisible ? "still visible" : "cleared")}");
        return !stillVisible;
    }

    /// <summary>
    /// Gemini vision ask ? identifies a specific UI element, returns structured OcrSegment.
    /// Optimized for speed: dedicated tab, 500ms poll, 15s timeout, no Slack/streaming.
    ///
    /// Use as ActionExecutor.AskVisionFn delegate:
    ///   executor.AskVisionFn = (bmp, desc) => AskGeminiForVisionAsync(bmp, desc);
    ///
    /// Prompt is verbose (Gemini tokens are free) ? asks for JSON a11y object with x/y/w/h coords.
    /// Returns: OcrSegment with position from Gemini JSON, or null on failure.
    /// </summary>
    static async Task<WKAppBot.Vision.OcrSegment?> AskGeminiForVisionAsync(
        System.Drawing.Bitmap screenshot, string description, int timeoutMs = 20000)
    {
        var raw = await AskGeminiVisionRawAsync(screenshot, BuildVisionElementPrompt(description), timeoutMs);
        if (raw == null) return null;

        var segs = WKAppBot.Vision.OcrSegmentCache.ParseA11yJson(raw);
        if (segs.Count == 0) return null;

        // Best match by description, or first if only one result
        return segs.Count == 1
            ? segs[0]
            : WKAppBot.Vision.OcrSegmentCache.BestMatch(segs, description) ?? segs[0];
    }

    /// <summary>
    /// Full-form Gemini a11y scan ? returns ALL visible UI elements as OcrSegment list.
    /// Called once per form to build OcrSegmentCache entries.
    /// Returns null on failure, empty list if no elements found.
    /// </summary>
    static async Task<List<WKAppBot.Vision.OcrSegment>?> AskGeminiForFormScanAsync(
        System.Drawing.Bitmap screenshot, int timeoutMs = 30000)
    {
        var raw = await AskGeminiVisionRawAsync(screenshot, BuildVisionFormScanPrompt(), timeoutMs);
        if (raw == null) return null;
        return WKAppBot.Vision.OcrSegmentCache.ParseA11yJson(raw);
    }

    // ���� Prompt builders (verbose ? Gemini tokens are free) ������������������������������

    static string BuildVisionElementPrompt(string description) => $$"""
        You are an accessibility inspector analyzing a Windows application screenshot.

        TARGET: Find the UI element that matches this description: "{{description}}"

        Examine the screenshot carefully. Look for:
        - Visible text labels, button captions, field labels
        - Icons, shapes, symbols that represent the target
        - Input fields, dropdowns, checkboxes, radio buttons
        - Scroll controls, sliders, progress bars
        - Any clickable or interactive element matching the description

        Return a SINGLE JSON object for the best matching element:
        {
          "type": "<ControlType>",
          "label": "<visible text or icon description>",
          "x": <center X, 0.0-1.0 relative to image width>,
          "y": <center Y, 0.0-1.0 relative to image height>,
          "w": <width, 0.0-1.0>,
          "h": <height, 0.0-1.0>,
          "state": "<enabled|disabled|checked|unchecked>"
        }

        ControlType must be one of:
        Button, Edit, Text, CheckBox, RadioButton, ComboBox, List, ListItem,
        Tab, TabItem, Image, DataGrid, DataItem, Group, Pane, ScrollBar,
        ProgressBar, Slider, Spinner, MenuItem, ToolBar, StatusBar, Unknown

        If the target element is not found, return:
        {"type":"unknown","label":"","x":0,"y":0,"w":0,"h":0}

        Return ONLY the JSON object, no explanation, no markdown fences.
        """;

    static string BuildVisionFormScanPrompt() => """
        You are an accessibility inspector analyzing a Windows application screenshot.

        Enumerate ALL visible UI elements in this form/window. Be thorough and detailed.

        For each element include:
        - Readable text paragraphs and labels (type: Text)
        - All buttons and clickable controls (type: Button)
        - Input fields and text boxes (type: Edit)
        - Checkboxes with their checked/unchecked state
        - Radio buttons with their checked/unchecked state
        - Dropdown/combo controls (type: ComboBox)
        - List items and data rows (type: ListItem / DataItem)
        - Tab headers (type: TabItem)
        - Icons and image shapes that are meaningful (type: Image)
        - Scroll bars if visible (type: ScrollBar)
        - Sliders, spinners, progress bars
        - Group boxes and panel labels (type: Group / Pane)

        Return a JSON ARRAY of all elements:
        [
          {
            "type": "<ControlType>",
            "label": "<visible text or description>",
            "x": <center X, 0.0-1.0>,
            "y": <center Y, 0.0-1.0>,
            "w": <width, 0.0-1.0>,
            "h": <height, 0.0-1.0>,
            "state": "<enabled|disabled|checked|unchecked>"
          },
          ...
        ]

        ControlType must be one of:
        Button, Edit, Text, CheckBox, RadioButton, ComboBox, List, ListItem,
        Tab, TabItem, Image, DataGrid, DataItem, Group, Pane, ScrollBar,
        ProgressBar, Slider, Spinner, MenuItem, ToolBar, StatusBar, Unknown

        Return ONLY the JSON array, no explanation, no markdown fences.
        Include every element you can see ? the more detail the better.
        """;

    // ���� Shared CDP vision transport ������������������������������������������������������������������������������

    static async Task<string?> AskGeminiVisionRawAsync(
        System.Drawing.Bitmap screenshot, string prompt, int timeoutMs = 20000)
    {
        var tmpPath = Path.Combine(Path.GetTempPath(), $"wkappbot_vision_{Guid.NewGuid():N}.png");
        try
        {
            screenshot.Save(tmpPath, System.Drawing.Imaging.ImageFormat.Png);

            // Dedicated vision tab ? never contaminates user's AI chat sessions
            var cdp = EnsureCdpConnection(
                preferredHost: "gemini.google.com", newTab: false,
                targetTag: "vision-ask");
            if (cdp == null) return null;

            // Navigate if drifted
            var currentUrl = await cdp.GetUrlAsync() ?? "";
            if (!currentUrl.Contains("gemini.google.com", StringComparison.OrdinalIgnoreCase))
            {
                await cdp.NavigateAsync("https://gemini.google.com/app");
                await Task.Delay(2500);
            }

            // Wait for editor (shorter timeout than regular ask)
            var editorSel = await WaitForEditorA11y(cdp,
                ".ql-editor", "[role='textbox'][contenteditable='true']",
                "div[contenteditable='true']", "rich-textarea [contenteditable]");
            if (editorSel == null) return null;

            await WaitWhileGeminiStopVisibleNoClickAsync(cdp, 5000);

            using var tabLock = ChromeTabLock.Acquire("Gemini/vision-ask");
            if (tabLock == null) return null;

            // Attach screenshot + prompt
            await AttachFilesViaCdp(cdp, new List<string> { tmpPath }, editorSel);
            await cdp.ClearEditorAsync(editorSel);
            await cdp.InsertContentEditableAsync(editorSel, prompt);
            await Task.Delay(200);

            // Send
            await cdp.EvalAsync("""
                (() => {
                    var btn = document.querySelector('button[aria-label*="Send"]')
                           || document.querySelector('button[aria-label="Send message"]')
                           || document.querySelector('button.send-button');
                    if (btn && !btn.disabled) btn.click();
                })()
                """);

            // Poll 500ms intervals until response stabilizes
            var sw = Stopwatch.StartNew();
            string? lastText = null;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                await Task.Delay(500);
                var text = await cdp.GetLastResponseTextAsync() ?? "";
                text = StripGeminiUiPrefix(text).Trim();
                if (string.IsNullOrWhiteSpace(text)) continue;

                if (text == lastText)
                {
                    return IsGeminiStoppedNotice(text) ? null : text;
                }
                lastText = text;
            }
            return null;  // timeout
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[VISION-ASK] Gemini error: {ex.Message}");
            return null;
        }
        finally
        {
            try { File.Delete(tmpPath); } catch { }
        }
    }

    static async Task<(bool ok, string? text)> RetryGeminiAfterStopAsync(CdpClient cdp, string editorSel, string question)
    {
        await WaitWhileGeminiStopVisibleAsync(cdp, 6000);
        await cdp.ClearEditorAsync(editorSel);
        await cdp.InsertContentEditableAsync(editorSel, question);
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
            await Task.Delay(1000);
            var text = await cdp.GetLastResponseTextAsync() ?? "";
            if (string.IsNullOrWhiteSpace(text)) continue;
            if (text == retryText)
            {
                if (IsGeminiStoppedNotice(text))
                    return (false, text);
                return (true, StripGeminiUiPrefix(text));
            }
            retryText = text;
        }

        if (!string.IsNullOrWhiteSpace(retryText) && !IsGeminiStoppedNotice(retryText))
            return (true, retryText);
        return (false, retryText);
    }

    static int AskGemini(string question, bool slackReport = true, int timeoutSec = 30, bool newTab = false, List<string>? attachFiles = null, bool newSession = false, bool loopMode = false, int loopMaxSteps = 3, int loopRetry = 1, int loopMaxParallel = 7, bool triadMode = false, string? modelHint = null, bool noWait = false, string? targetTagOverride = null, string? linePrefix = null, TriadSharedContext? triadCtx = null)
    {
        using var _ = ApplyOutputPrefix(linePrefix);
        Console.WriteLine($"[ASK] Gemini: {question}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Gemini model hint: {modelHint}");

        PulseStep.Init("ask-gemini");
        var targetTag = targetTagOverride ?? BuildSandboxKey("ask", "gemini");
        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;
        PulseStep.Mark("cdp-connected");
        using var askSession = new AskSession(AiProvider.Gemini, cdp); // gradual migration wrapper

        // No tab activation ? CDP works on background tabs via targetId. Truly focusless.
        var prevFgGemini = NativeMethods.GetForegroundWindow();

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        // Pre-open Slack thread before task so inside-task code (persona injection etc.) can post to it
        EnsureSlackThread("Gemini", question);

        var task = Task.Run(async () =>
        {
            try
            {
                // ?�?�?Phase 1: Navigate (iconified OK ??CDP works without rendering) ?�?�?
                // ── P1: Navigate via AskSession ──
                PulseStep.Mark("phase1-navigate");
                await askSession.NavigateAsync(newSession);
                var currentUrl = await cdp.GetUrlAsync() ?? ""; // kept for downstream logging
                if (false) // legacy navigate — replaced by askSession.NavigateAsync
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

                // Tab state + visibility dispatch handled by askSession.NavigateAsync above
                if (false) // legacy — replaced by AskSession
                {
                    Console.WriteLine("[ASK] Tab hidden ??dispatching visibility events (focusless)...");
                    await cdp.DispatchVisibilityAsync();
                    await Task.Delay(500);
                }

                // ── P2: Find editor via AskSession (uses AiProvider.Gemini selectors) ──
                PulseStep.Mark("find-editor");
                var editorSel = await askSession.FindEditorAsync(15);
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

                // ?�?�?Persona injection on fresh Gemini conversation ?�?�?
                // If persona continuation already contains a tool call, skip question send entirely
                string? personaEarlyToolCall = null;
                var geminiTurnCount = (await cdp.GetResponseCountAsync()).ToString();
                var hasLoopPersonaState = await HasLoopPersonaStateAsync(cdp, "gemini");
                var effectiveLoopPersona = loopMode || hasLoopPersonaState;
                Console.WriteLine($"[ASK] Loop persona state: {(hasLoopPersonaState ? "present" : "missing")}");
                if (!loopMode && hasLoopPersonaState)
                    Console.WriteLine("[ASK] Loop marker found; MCP guidance will be included for fresh session persona.");
                if (geminiTurnCount == "0" || (effectiveLoopPersona && !hasLoopPersonaState))
                {
                    // ?�?�?Browser exclusive: persona input ??send complete ?�?�?
                    using var personaLock = ChromeTabLock.Acquire("Gemini/persona");
                    if (personaLock == null) return (false, (string?)null);

                    Console.WriteLine(geminiTurnCount == "0"
                        ? "[ASK] Fresh Gemini -- injecting persona..."
                        : "[ASK] Loop persona missing on this tab -- re-injecting persona...");
                    await cdp.ClearEditorAsync(editorSel);
                    var personaText = BuildAskPersona(effectiveLoopPersona, triadMode, loopMaxSteps, loopRetry, modelHint);
                    if (Interlocked.CompareExchange(ref _slackPersonaPostedFlag, 1, 0) == 0)
                        SlackPostToThread($"📋 *[persona]* steps={loopMaxSteps} retry={loopRetry}\n```\n{(personaText.Length > 800 ? personaText[..800] + "..." : personaText)}\n```", "System");
                    await cdp.InsertContentEditableAsync(editorSel, personaText);
                    await Task.Delay(300);

                    // Send persona (button click ??Enter fallback)
                    var personaSent = false;
                    for (int ps = 0; ps < 5; ps++)
                    {
                        var rem = (await cdp.GetTextLengthAsync(editorSel)).ToString();
                        if (rem == "0" && ps > 0) { personaSent = true; break; }
                        // Send: Enter key (modern AI: Enter=send, button=stop)
                        await askSession.SendAsync(editorSel);
                        await Task.Delay(500);
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
                        // Wait for persona response to fully stabilize (text-based, no stop-click interference)
                        // This handles the case where Gemini immediately continues generating a tool call
                        // after "READY". We wait patiently for the full response without clicking stop.
                        Console.WriteLine("[PERSONA-WAIT] stabilizing...");
                        string? stablePersonaResp = null;
                        {
                            string? prevText = null;
                            int pStabCount = 0;
                            var pStabSw = Stopwatch.StartNew();
                            while (pStabSw.Elapsed.TotalSeconds < 45)
                            {
                                await Task.Delay(1500);
                                var curText = await cdp.EvalAsync("""
                                    (() => {
                                        var r = document.querySelectorAll('model-response');
                                        if (r.length === 0) r = document.querySelectorAll('[role="article"]');
                                        if (r.length === 0) return '';
                                        return (r[r.length-1].textContent || '').trim();
                                    })()
                                    """) ?? "";
                                if (curText.Length == 0) continue;
                                // Also check stop button ? if still visible, Gemini is still generating (not truly stable)
                                var stopNowPersona = await cdp.EvalAsync("""
                                    (() => {
                                        if (document.querySelector('button[aria-label*="Stop"]') ||
                                            document.querySelector('button[aria-label*="����"]')) return '1';
                                        var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                                        if (mat) { var b=mat.closest('button'); if(b&&(b.getAttribute('aria-label')||'').toLowerCase().includes('stop')) return '1'; }
                                        return '0';
                                    })()
                                    """) ?? "0";
                                if (stopNowPersona == "1") { pStabCount = 0; prevText = curText; continue; } // still generating
                                if (curText == prevText)
                                {
                                    pStabCount++;
                                    if (pStabCount >= 4) // stable for 6s (4x1.5s) with stop gone
                                    {
                                        stablePersonaResp = curText;
                                        break;
                                    }
                                }
                                else
                                {
                                    pStabCount = 0;
                                    prevText = curText;
                                }
                            }
                        }
                        if (stablePersonaResp != null)
                        {
                            bool ready = stablePersonaResp.Contains("READY", StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"[ASK] Persona stable ({stablePersonaResp.Length}): {(ready ? "READY" : stablePersonaResp.Substring(0, Math.Min(80, stablePersonaResp.Length)).Replace('\n', ' '))}");
                            if (effectiveLoopPersona && stablePersonaResp.Contains("[APPBOT_TOOL_CALL_BEGIN]")
                                && ParseAllLoopToolCalls(stablePersonaResp).Count > 0)
                            {
                                Console.WriteLine($"[ASK] Persona continuation has tool call ({stablePersonaResp.Length} chars) ? skipping question send");
                                personaEarlyToolCall = stablePersonaResp;
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ASK] Persona stability timeout ? proceeding anyway");
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

                // Persona continuation already had tool call ? skip question send, go directly to loop
                if (personaEarlyToolCall != null)
                    return (true, personaEarlyToolCall);

                // Post-persona: if Gemini is still generating (tool call burst after READY),
                // wait WITHOUT clicking stop ? stop-click generates poisonous stop-notice responses.
                if (effectiveLoopPersona)
                {
                    Console.WriteLine("[POST-PERSONA] waiting for generation to finish...");
                    var postStopVisible = await WaitWhileGeminiStopVisibleNoClickAsync(cdp, maxWaitMs: 30000);
                    // Capture last response ? may contain tool call that Gemini generated post-READY
                    var latePersonaResp = await GetGeminiLastResponseAsync(cdp);
                    if (latePersonaResp.Length > 0)
                        Console.WriteLine($"[ASK] Post-persona resp ({latePersonaResp.Length}): {latePersonaResp.Replace('\n', ' ').Substring(0, Math.Min(80, latePersonaResp.Length))}");
                    if (latePersonaResp.Contains("[APPBOT_TOOL_CALL_BEGIN]")
                        && ParseAllLoopToolCalls(latePersonaResp).Count > 0)
                    {
                        Console.WriteLine("[ASK] Post-persona tool call captured -- skipping question send");
                        return (true, latePersonaResp);
                    }
                }
                // ?�?�?Browser exclusive: question input ??send complete ?�?�?
                // Prepend host handshake proof for loop sessions so Gemini trusts the host is live
                if (effectiveLoopPersona)
                    question = BuildHostHandshake() + question;

                PulseStep.Mark("question-prep");
                using var questionLock = ChromeTabLock.Acquire("Gemini");
                if (questionLock == null) return (false, (string?)null);

                // ?�?�?CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ?�?�?
                var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "Gemini");

                // ?�?�?File attachments (before text) ?�?�?
                // Pass prevFgGemini so native file dialog tier can restore original user focus after close
                if (attachFiles?.Count > 0)
                {
                    await AttachFilesViaCdp(cdp, attachFiles, editorSel, prevFgGemini);
                    PulseStep.Mark("files-attached");
                }

                // Tier 1: focusless insert (a11y-first)
                await cdp.ClearEditorAsync(editorSel);
                var inserted = await cdp.InsertContentEditableAsync(editorSel, question);
                if (!inserted)
                {
                    // Tier 2: DOM.focus (Chrome-internal, no OS focus) + execCommand/DOM mutation
                    Console.WriteLine("[ASK] Tier 1 failed, trying DOM.focus tier 2...");
                    inserted = await InsertTextViaDomFocus(cdp, editorSel, question);
                    if (!inserted)
                    {
                        zoom?.ShowFail("insert failed");
                        zoom?.Dispose();
                        Console.WriteLine("[ASK] Failed to insert text");
                        return (false, (string?)null);
                    }

                }
                // ?�?�?Focus theft detection: restore if Chrome stole focus ?�?�?
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");

                // Send: a11y-first (CDP real click on button) ??focusless Enter fallback
                // Keep trying until editor is empty (= message sent)
                await Task.Delay(300);
                // Pre-send: if stop visible (persona continuation), wait WITHOUT clicking stop.
                // Clicking stop generates a stop-notice response that poisons subsequent reads.
                {
                    var preStopped = await cdp.EvalAsync("""
                        (() => {
                            if (document.querySelector('button[aria-label*="Stop"]') || document.querySelector('button[aria-label*="����"]')) return '1';
                            var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                            return (mat && mat.closest('button')) ? '1' : '0';
                        })()
                        """) ?? "0";
                    if (preStopped == "1")
                    {
                        Console.WriteLine("[ASK] Gemini still generating pre-send ? waiting without interrupt...");
                        await WaitWhileGeminiStopVisibleNoClickAsync(cdp, maxWaitMs: 30000);
                        // After Gemini finishes, check if it generated a tool call (loop mode)
                        if (effectiveLoopPersona && personaEarlyToolCall == null)
                        {
                            var lateResp = await cdp.EvalAsync("""
                                (() => {
                                    var r = document.querySelectorAll('model-response');
                                    if (r.length === 0) r = document.querySelectorAll('[role="article"]');
                                    return r.length > 0 ? (r[r.length-1].textContent || '') : '';
                                })()
                                """) ?? "";
                            Console.WriteLine($"[ASK] Post-wait resp ({lateResp.Length}): {lateResp.Substring(0, Math.Min(80, lateResp.Length)).Replace('\n', ' ')}");
                            if (lateResp.Contains("[APPBOT_TOOL_CALL_BEGIN]")
                                && ParseAllLoopToolCalls(lateResp).Count > 0)
                            {
                                Console.WriteLine("[ASK] Late persona tool call captured ? skipping question send");
                                zoom?.ShowPass("tool call");
                                zoom?.Dispose();
                                questionLock.Release("late-toolcall");
                                LogRestoreFocus(prevFg, "late-toolcall");
                                return (true, lateResp);
                            }
                        }
                    }
                }
                var sendResult = "PENDING";
                // Count model-responses before sending ??detect response start as send confirmation
                var preResponseCount = (await cdp.GetResponseCountAsync()).ToString();

                for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
                {
                    // Fast-fail if Gemini is still generating ? do NOT click stop (poisons response)
                    var stopAtSend = await cdp.EvalAsync("""
                        (() => {
                            if (document.querySelector('button[aria-label*="Stop"]') || document.querySelector('button[aria-label*="����"]')) return '1';
                            var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                            if (mat) { var b=mat.closest('button'); if(b) return '1'; }
                            return '0';
                        })()
                        """) ?? "0";
                    if (stopAtSend == "1")
                    {
                        // If editor is already empty, message was sent ? Gemini is responding (stop = normal)
                        if (sendAttempt > 0)
                        {
                            var editorLen = (await cdp.GetTextLengthAsync(editorSel)).ToString();
                            if (editorLen == "0")
                            {
                                sendResult = $"SENT(attempt={sendAttempt})";
                                break;
                            }
                        }
                        var curResponses = (await cdp.GetResponseCountAsync()).ToString();
                        if (curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_IN_PROGRESS(attempt={sendAttempt})";
                            break;
                        }
                        // Gemini generating before send ? fast-fail (attempt=0 only, or if editor not cleared)
                        Console.WriteLine($"[ASK] Gemini still generating at send time (attempt={sendAttempt}) ? fast-fail");
                        zoom?.ShowFail("still generating");
                        zoom?.Dispose();
                        questionLock.Release("fast-fail-gen");
                        LogRestoreFocus(prevFg, "fast-fail-gen");
                        return (false, null);
                    }

                    // Check if editor cleared OR response started (= already sent, don't re-send!)
                    var remaining = (await cdp.GetTextLengthAsync(editorSel)).ToString();
                    if (remaining == "0" && sendAttempt > 0)
                    {
                        sendResult = $"SENT(attempt={sendAttempt})";
                        break;
                    }
                    // Response started = message was sent, stop clicking (avoids hitting stop button)
                    if (sendAttempt > 0)
                    {
                        var curResponses = (await cdp.GetResponseCountAsync()).ToString();
                        if (curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_STARTED(attempt={sendAttempt})";
                            break;
                        }
                    }

                    // Re-insert text if editor is empty (text didn't stick)
                    if (remaining == "0" && sendAttempt == 0)
                    {
                        await cdp.InsertContentEditableAsync(editorSel, question);
                        await Task.Delay(200);
                    }

                    // JS click() ??works even when Chrome is minimized (no viewport needed)
                    var clickResult = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('button[aria-label="메시지 보내�?]')
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
                                if (document.querySelector('button[aria-label*="Stop"]') || document.querySelector('button[aria-label*="����"]')) return '1';
                                var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                                if (mat) { var b=mat.closest('button'); if (b&&((b.getAttribute('aria-label')||b.title||'').toLowerCase().includes('stop')||(b.getAttribute('aria-label')||b.title||'').toLowerCase().includes('����'))) return '1'; }
                                return '0';
                            })()
                            """) ?? "0";
                        var curResponses = (await cdp.GetResponseCountAsync()).ToString();
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

                Console.WriteLine($"[SEND-DONE] send={sendResult}");
                questionLock.Release("sent");
                PulseStep.Mark("question-sent");
                if (noWait)
                    return (true, BuildNoWaitQueuedMessage("Gemini"));

                // Count existing responses before polling (skip persona's READY etc.)
                // Use pre-send count as baseline (preResponseCount already measured before send loop)
                // If we measured post-send, Gemini may have already added the new response �� skipped!
                bool responseAlreadyStarted = sendResult.StartsWith("RESPONSE_", StringComparison.OrdinalIgnoreCase);
                int baseResponseCount = int.TryParse(preResponseCount, out var brc) ? brc : 0;
                Console.WriteLine($"[POLL-WAIT] start (base={baseResponseCount}, timeout={timeoutSec}s)...");

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
                var lastFlushTime = DateTime.UtcNow;
                var sw = Stopwatch.StartNew();

                int geminiBlankCount = 0;
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(1000);
                    // A11y-first: model-response ??[role='article'] ??generic text
                    // Only read NEW responses (skip persona exchange)
                    var text = responseAlreadyStarted
                        ? await cdp.GetLastResponseTextAsync(0, blankDetect: true)
                        : await cdp.GetLastResponseTextAsync(baseResponseCount, blankDetect: true);

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
                    {
                        // Diagnostic: log if response count changed but text is empty (baseline filter issue)
                        var diagCount = await cdp.EvalAsync(
                            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                        if (diagCount != preResponseCount && sw.Elapsed.TotalSeconds < 10)
                            Console.WriteLine($"[ASK] Poll: respCount={diagCount} base={baseResponseCount} ? text empty (filter skipping? check baseline)");
                        continue;
                    }

                    // Live flush: print new text delta
                    if (text.Length > lastFlushedLen)
                    {
                        if (!liveHeaderPrinted)
                        {
                            Console.WriteLine();
                            Console.WriteLine("[Gemini] streaming...");
                            liveHeaderPrinted = true;
                        }
                        Console.Write(text.Substring(lastFlushedLen));
                        Console.Out.Flush();
                        lastFlushedLen = text.Length;
                        lastFlushTime = DateTime.UtcNow;

                        // Stream-time tool call detection: complete block visible �� fire immediately
                        // No need to wait for 4s stability ? [TOOL_CALL_END] = call is ready now
                        if (effectiveLoopPersona
                            && text.Contains("[APPBOT_TOOL_CALL_BEGIN]")
                            && text.Contains("[APPBOT_TOOL_CALL_END]"))
                        {
                            Console.WriteLine("\n[STREAM-TOOLCALL] complete -- firing immediately");
                            return (true, StripGeminiUiPrefix(text));
                        }
                    }

                    // Detect generated images in response
                    var gemNewImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                    geminiSavedImages.AddRange(gemNewImages);

                    // Poll status indicator
                    int delta = text.Length - lastFlushedLen;
                    if (delta > 0)
                    { Console.Write($" [FLUSH+{delta}]"); Console.Out.Flush(); }
                    else if (text.Length > 0)
                    { Console.Write($" [RUNNING {sw.Elapsed.TotalSeconds:F0}s]"); Console.Out.Flush(); }

                    // Streaming handoff: text growing �� this tab is alive, give active tab to peer
                    if (text.Length > lastTextLen && lastTextLen > 0)
                        await HandoffTabToPeer("gemini");
                    lastTextLen = text.Length;

                    // Check if response is still generating
                    // Early-exit: flush idle 1s and enough text �� don't wait for full stability
                    if (lastFlushedLen > 50 && (DateTime.UtcNow - lastFlushTime).TotalSeconds >= 1.0)
                    {
                        var gemEarlyImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                        geminiSavedImages.AddRange(gemEarlyImages);
                        if (liveHeaderPrinted) Console.WriteLine();
                        Console.WriteLine($"[ASK] Flush idle 1s -- early done ({sw.Elapsed.TotalSeconds:F0}s)");
                        return (true, text!);
                    }

                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 3) // stable for 3+ seconds (fallback if no flush)
                        {
                            // Final image check
                            var gemFinalImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                            geminiSavedImages.AddRange(gemFinalImages);
                            if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                            // Tool call takes priority over stop notice ? Gemini appends stop notice
                            // even when the response is a valid tool call (DOM artifact)
                            bool hasToolCall = text.Contains("[APPBOT_TOOL_CALL_BEGIN]");
                            if (!hasToolCall && IsGeminiStoppedNotice(text))
                            {
                                // Check if meaningful content exists before the stop notice
                                var preStop = text;
                                foreach (var kw in GeminiStopNoticeKeywords)
                                {
                                    var ki = preStop.IndexOf(kw, StringComparison.OrdinalIgnoreCase);
                                    if (ki >= 0) { preStop = preStop[..ki].TrimEnd(); break; }
                                }
                                preStop = StripGeminiUiPrefix(preStop);
                                if (!string.IsNullOrWhiteSpace(preStop) && preStop.Length >= 3)
                                {
                                    Console.WriteLine($"[ASK] Stop notice but answer found before it ({preStop.Length} chars) ? accepting");
                                    return (true, preStop);
                                }
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
                            if (hasToolCall && IsGeminiStoppedNotice(text))
                                Console.WriteLine("[ASK] Stop notice present but tool call found ? ignoring stop notice");
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            if (geminiSavedImages.Count > 0)
                                Console.WriteLine($"[ASK] Downloaded {geminiSavedImages.Count} generated image(s)");
                            return (true, StripGeminiUiPrefix(text));
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
                    return (true, StripGeminiUiPrefix(lastText));
                }
                Console.WriteLine("[ASK] Timeout ??no response, retrying once...");

                // Retry: same page, re-insert and send (no reload, keeps session)
                await Task.Delay(1000);
                await cdp.ClearEditorAsync(editorSel);
                await cdp.InsertContentEditableAsync(editorSel, question);
                await Task.Delay(300);
                await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label="메시지 보내�?]')
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
                    await Task.Delay(1000);
                    var text = await cdp.GetLastResponseTextAsync() ?? "";
                    if (string.IsNullOrEmpty(text)) continue;
                    if (text == retryText)
                    {
                        if (IsGeminiStoppedNotice(text))
                        {
                            Console.WriteLine("[ASK] Retry: stop notice detected; fast-fail");
                            return (false, text);
                        }
                        Console.WriteLine($"[ASK] Retry: response ({text.Length} chars)");
                        return (true, StripGeminiUiPrefix(text));
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
        if (answer != null) answer = StripGeminiUiPrefix(answer); // safety net: catches any missed return path

        if (!string.IsNullOrWhiteSpace(answer))
        {
            EnsureSlackThread("Gemini", question);
            var forSlack = answer; // already stripped of "Gemini�� ����" prefix by StripGeminiUiPrefix at source
            // Strip stop notice suffix (e.g. "�����?�����Ǿ����ϴ�") before posting to Slack
            foreach (var kw in GeminiStopNoticeKeywords)
            {
                var ki = forSlack.IndexOf(kw, StringComparison.OrdinalIgnoreCase);
                if (ki >= 0) { forSlack = forSlack[..ki].TrimEnd(); break; }
            }
            if (!string.IsNullOrWhiteSpace(forSlack))
            {
                var suffix = ok ? "" : "\n[send failed]";
                var post = forSlack.Length > 2000 ? forSlack[..2000] + "..." : forSlack;
                SlackPostToThread(post + suffix, "Gemini");
            }
        }

        // Log initial answer to shared triad context (for recovery by other AIs if needed)
        if (ok && !string.IsNullOrWhiteSpace(answer))
            triadCtx?.LogStep("Gemini", answer);

        Action<string, string?> onStepReport = (msg, uname) =>
        {
            SlackPostToThread(msg, uname ?? "Gemini");
            triadCtx?.LogStep("Gemini", msg);
        };
        if (loopMode && ok && !string.IsNullOrWhiteSpace(answer))
            (ok, answer) = Task.Run(() => RunGeminiLoopAsync(cdp, answer!, timeoutSec, loopMaxSteps, loopRetry, loopMaxParallel, onStepReport)).GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Print answer (truncate for console)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Gemini] Answer:");
            Console.ResetColor();
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);

            // Full answer marker (for programmatic capture by whisper study etc.)
            Console.WriteLine("[ASK_FULL_ANSWER_BEGIN]");
            Console.WriteLine(answer);
            Console.WriteLine("[ASK_FULL_ANSWER_END]");
            PulseStep.Done();

            // Slack already handled above (initial answer) + loop onStepReport
        }

        UnregisterWaitingTab("gemini");
        // Preserve Chrome's original state ??don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }
}

