using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;

namespace WKAppBot.CLI;

internal partial class Program
{
    static async Task<(bool ok, string? text)> ClaudeSendAndWaitAsync(CdpClient cdp, string message, int timeoutSec)
    {
        using var askSession = new AskSession(AiProvider.Claude, cdp);
        askSession.SetIdentity(
            runId: Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID"),
            providerTag: "claude-loop");
        // Guard: verify tab is still on claude.ai (web search subprocess might have navigated away)
        var currentUrl = await cdp.GetUrlAsync() ?? "";
        if (!currentUrl.Contains("claude.ai", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[ASK:LOOP] Claude tab drifted to {currentUrl} ??navigating back to claude.ai");
            await cdp.NavigateAsync("https://claude.ai/new");
            await Task.Delay(3000);
        }

        var editorSel = await WaitForClaudeEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);
        askSession.BindStreamingContext(editorSel);

        int baseCount = await cdp.QueryCountAsync("[data-is-streaming]");

        using var loopLock = ChromeTabLock.Acquire("Claude/loop");
        if (loopLock == null)
            return (false, null);

        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextClaudeProseMirror(cdp, editorSel, message);
        if (!inserted)
            return (false, null);

        await Task.Delay(350);
        var sendClicked = await askSession.ClickSendAsync();

        if (!sendClicked)
        {
            await cdp.FocusAsync(editorSel);
            await Task.Delay(80);
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
        }

        loopLock.Release("sent");

        var sw = Stopwatch.StartNew();
        string? last = null;
        int stable = 0;
        while (sw.Elapsed.TotalSeconds < Math.Max(20, timeoutSec))
        {
            await Task.Delay(1500);
            if (await TryHandleAskControlAsync(askSession))
                return (false, "CANCELLED");
            // TODO: migrate to AskSession when provider-specific limit detection is unified
            var limitText = await cdp.EvalAsync("""
                (() => {
                    var msgs = document.querySelectorAll('[data-is-streaming]');
                    var t = msgs.length > 0 ? (msgs[msgs.length - 1].innerText || '') : '';
                    if (!t) {
                        var banners = document.querySelectorAll('[class*="limit"],[class*="usage"],[class*="quota"]');
                        t = Array.from(banners).map(b => b.innerText).join('\n').substring(0, 800);
                    }
                    var keys = ['usage limit', 'rate limit', 'too many requests', 'request limit', 'usage cap'];
                    var tl = t.toLowerCase();
                    for (var i = 0; i < keys.length; i++) {
                        if (tl.includes(keys[i])) return t.substring(0, 800);
                    }
                    return '';
                })()
                """) ?? "";
            if (!string.IsNullOrWhiteSpace(limitText))
                return (true, FormatClaudeLimitResponse(limitText));

            // TODO: migrate to AskSession when provider-specific polling is unified
            var poll = await cdp.EvalAsync(
                "(() => {" +
                "var msgs = document.querySelectorAll('[data-is-streaming]');" +
                $"if (msgs.length <= {baseCount}) return JSON.stringify({{state:'WAIT',text:''}});" +
                "var last = msgs[msgs.length - 1];" +
                "var state = last.getAttribute('data-is-streaming') === 'true' ? 'STREAMING' : 'DONE';" +
                "var text = (last.textContent || '').trim();" +
                "return JSON.stringify({state:state,text:text});" +
                "})()") ?? "{}";
            try
            {
                using var doc = JsonDocument.Parse(poll);
                var state = doc.RootElement.TryGetProperty("state", out var s) ? s.GetString() ?? "WAIT" : "WAIT";
                var text = doc.RootElement.TryGetProperty("text", out var t) ? t.GetString() ?? "" : "";
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                if (text == last)
                {
                    stable++;
                    if (stable >= 1 && state != "STREAMING")
                        return (true, text);
                }
                else
                {
                    stable = 0;
                    last = text;
                }
            }
            catch
            {
                // ignore transient parse/DOM errors during polling
            }
        }

        return (!string.IsNullOrWhiteSpace(last), last);
    }

    static async Task<(bool ok, string? text)> GeminiSendAndWaitAsync(CdpClient cdp, string message, int timeoutSec)
    {
        using var askSession = new AskSession(AiProvider.Gemini, cdp);
        askSession.SetIdentity(
            runId: Environment.GetEnvironmentVariable("WKAPPBOT_RUN_ID"),
            providerTag: "gemini-loop");
        // Guard: verify tab is still on gemini.google.com
        var currentUrl = await cdp.GetUrlAsync() ?? "";
        if (!currentUrl.Contains("gemini.google.com", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"[ASK:LOOP] Gemini tab drifted to {currentUrl} ??navigating back");
            await cdp.NavigateAsync("https://gemini.google.com/app");
            await Task.Delay(3000);
        }

        var editorSel = await WaitForEditorA11y(cdp,
            ".ql-editor",
            "[role='textbox'][contenteditable='true']",
            "div[contenteditable='true']",
            "div.ql-editor",
            "rich-textarea [contenteditable]",
            ".input-area [contenteditable]");
        if (editorSel == null)
            return (false, null);
        askSession.BindStreamingContext(editorSel);

        using var loopLock = ChromeTabLock.Acquire("Gemini/loop");
        if (loopLock == null)
            return (false, null);

        var baseCountStr = (await cdp.GetResponseCountAsync()).ToString();
        int baseCount = int.TryParse(baseCountStr, out var b) ? b : 0;

        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        if (!inserted)
            return (false, null);
        await Task.Delay(300);

        var sendResult = "PENDING";
        for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
        {
            var remaining = (await cdp.GetTextLengthAsync(editorSel)).ToString();
            if (remaining == "0" && sendAttempt > 0)
            {
                sendResult = $"SENT(attempt={sendAttempt})";
                break;
            }

            if (sendAttempt > 0)
            {
                var curResponses = (await cdp.GetResponseCountAsync()).ToString();
                if (curResponses != baseCountStr)
                {
                    sendResult = $"RESPONSE_STARTED(attempt={sendAttempt})";
                    break;
                }
            }

            var sendClicked = await askSession.ClickSendAsync();

            if (!sendClicked)
            {
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
            }

            await Task.Delay(500);
        }
        if (sendResult == "PENDING")
            sendResult = "FORCED(5x)";
        Console.WriteLine($"[ASK:LOOP] Gemini send={sendResult}");
        loopLock.Release("sent");

        var sw = Stopwatch.StartNew();
        string? last = null;
        int blankCount = 0;
        int stable = 0;
        while (sw.Elapsed.TotalSeconds < Math.Max(20, timeoutSec))
        {
            await Task.Delay(2000);
            if (await TryHandleAskControlAsync(askSession))
                return (false, "CANCELLED");
            var text = await cdp.GetLastResponseTextAsync(baseCount, blankDetect: true) ?? "";
            if (text == "\x01BLANK")
            {
                blankCount++;
                if (blankCount >= 3)
                    break;
                continue;
            }
            blankCount = 0;
            if (string.IsNullOrWhiteSpace(text))
                continue;

            var isStreaming = await askSession.IsStopVisibleAsync();

            if (text == last)
            {
                stable++;
                if (stable >= 1 && !isStreaming)
                    return (true, text);
            }
            else
            {
                stable = 0;
                last = text;
            }
        }

        return (!string.IsNullOrWhiteSpace(last), last);
    }
}


