using System.Text.Json;

namespace WKAppBot.WebBot;

/// <summary>
/// High-level AI chat helpers for CdpClient.
/// Encapsulates complex multi-step JS operations for AI web interfaces
/// (ChatGPT, Gemini, Claude.ai).
/// </summary>
public sealed partial class CdpClient
{
    // ══ Editor Operations ══

    /// <summary>
    /// Insert text into a contenteditable element (3-tier: innerHTML → execCommand → Input.insertText).
    /// Returns true if text was inserted successfully.
    /// </summary>
    public async Task<bool> InsertContentEditableAsync(string selector, string text)
    {
        var jsStr = JsonSerializer.Serialize(text);

        // Tier 1: Focusless innerHTML/textContent
        var result = await EvalAsync(
            $"(()=>{{var el=document.querySelector('{Esc(selector)}');if(!el)return 'NOT_FOUND';" +
            $"var t={jsStr};var p=el.querySelector('p');" +
            "if(p)p.textContent=t;else el.innerHTML='<p>'+t.replace(/</g,'&lt;')+'</p>';" +
            "el.dispatchEvent(new InputEvent('input',{bubbles:true,inputType:'insertText',data:t}));" +
            "return el.textContent.length>0?'OK':'EMPTY'}})()");
        if (result == "OK") return true;

        // Tier 2: execCommand (respects selection model)
        result = await EvalAsync(
            $"(()=>{{var el=document.querySelector('{Esc(selector)}');if(!el)return 'NOT_FOUND';" +
            "el.focus();var sel=window.getSelection();var r=document.createRange();" +
            "r.selectNodeContents(el);r.collapse(false);sel.removeAllRanges();sel.addRange(r);" +
            $"document.execCommand('insertText',false,{jsStr});" +
            "return el.textContent.length>0?'OK':'EMPTY'}})()");
        if (result == "OK") return true;

        // Tier 3: Input.insertText (CDP protocol level)
        await FocusAsync(selector);
        await Task.Delay(50);
        await SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject { ["text"] = text });
        var len = await GetTextLengthAsync(selector);
        return len > 0;
    }

    /// <summary>Clear a contenteditable editor (selectAll + delete).</summary>
    public async Task ClearEditorAsync(string selector)
    {
        await EvalAsync(
            $"(()=>{{var el=document.querySelector('{Esc(selector)}');" +
            "if(!el)return;el.focus();" +
            "document.execCommand('selectAll');document.execCommand('delete')}})()");
    }

    // ══ Send Button ══

    /// <summary>
    /// Send message via Enter key (modern AI chats use Enter=send, button=stop).
    /// Focuses the editor first, then dispatches Enter key.
    /// Returns: "SENT", "NO_EDITOR", "ERROR"
    /// </summary>
    public async Task<string> SendPromptAsync(string editorSelector)
    {
        // Focus editor first
        await FocusAsync(editorSelector);
        await Task.Delay(50);

        // Enter key via CDP Input.dispatchKeyEvent
        await SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
            ["windowsVirtualKeyCode"] = 13
        });
        await SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
            ["windowsVirtualKeyCode"] = 13
        });
        return "SENT";
    }

    /// <summary>
    /// Click the stop/cancel button (visible during AI response generation).
    /// ⚠ WARNING: In modern AI chats, the "send" button becomes "stop" during streaming!
    /// Use SendPromptAsync() for sending, this for stopping.
    /// </summary>
    public async Task<string> ClickStopButtonAsync()
    {
        return await EvalAsync("""
            (() => {
                var sels = [
                    'button[aria-label*="Stop"]', 'button[aria-label*="중지"]',
                    'button[data-testid="stop-button"]',
                    'button[aria-label="Cancel"]'
                ];
                var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                if (mat) { var b = mat.closest('button'); if (b) { b.click(); return 'STOPPED:mat-icon'; } }
                for (var s of sels) {
                    var b = document.querySelector(s);
                    if (b && b.offsetParent !== null) { b.click(); return 'STOPPED:' + s; }
                }
                return 'NOT_FOUND';
            })()
            """) ?? "NOT_FOUND";
    }

    // ══ Streaming Detection ══

    /// <summary>
    /// Check if AI is currently streaming a response.
    /// Returns true if streaming indicators are visible.
    /// </summary>
    public async Task<bool> IsStreamingAsync(string provider)
    {
        var js = provider.ToLowerInvariant() switch
        {
            "claude" => "document.querySelector('[data-is-streaming]') ? '1' : '0'",
            "gemini" => "(document.querySelector('mat-icon[fonticon=\"stop_circle\"]') || " +
                       "document.querySelector('button[aria-label*=\"Stop\"]')) ? '1' : '0'",
            "gpt" or "chatgpt" => "(document.querySelector('button[aria-label=\"Stop generating\"]') || " +
                                 "document.querySelector('[data-testid=\"stop-button\"]')) ? '1' : '0'",
            _ => "'0'"
        };
        return await EvalAsync(js) == "1";
    }

    /// <summary>
    /// Get editor content text for a specific AI provider.
    /// </summary>
    public async Task<string> GetEditorContentAsync(string selector)
    {
        var escaped = Esc(selector);
        return await EvalAsync(
            $"(()=>{{var el=document.querySelector('{escaped}');return el?(el.textContent||el.value||'').trim():''}})()") ?? "";
    }

    /// <summary>
    /// Detect rate limit / usage cap messages.
    /// Returns the limit text if found, null otherwise.
    /// </summary>
    public async Task<string?> DetectRateLimitAsync()
    {
        return await EvalAsync("""
            (() => {
                var limitSels = [
                    '[class*="rate-limit"]', '[class*="usage-cap"]',
                    '[class*="limit-reached"]', '[data-testid*="limit"]',
                    '.error-message', '[role="alert"]'
                ];
                for (var s of limitSels) {
                    var el = document.querySelector(s);
                    if (el && el.offsetParent !== null)
                        return el.textContent.trim().substring(0, 200);
                }
                return null;
            })()
            """);
    }

    // ══ Common Ask Flow ══

    /// <summary>
    /// Wait for AI response to complete (streaming done).
    /// Polls until: stop button gone + response count increased + text stabilized.
    /// Returns (success, responseText).
    /// </summary>
    public async Task<(bool ok, string text)> WaitForResponseAsync(
        string provider, int baseResponseCount = 0, int timeoutSec = 30, int pollMs = 1000)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string lastText = "";
        int stableCount = 0;

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(pollMs);

            // Check response count increased
            var count = await GetResponseCountAsync();
            if (count <= baseResponseCount) continue;

            // Read latest response
            var text = await GetLastResponseTextAsync() ?? "";
            if (string.IsNullOrWhiteSpace(text)) continue;

            // Check streaming done (stop button gone)
            var streaming = await IsStreamingAsync(provider);
            if (!streaming)
            {
                // Final read after streaming done
                await Task.Delay(300);
                text = await GetLastResponseTextAsync() ?? "";
                return (true, text.Trim());
            }

            // Stability check (text unchanged for 2 polls = possibly done)
            if (text == lastText) { stableCount++; if (stableCount >= 3) return (true, text.Trim()); }
            else { lastText = text; stableCount = 0; }
        }

        // Timeout — return last result
        if (!string.IsNullOrEmpty(lastText))
            return (true, lastText.Trim());
        return (false, "");
    }

    /// <summary>Wait for editor element to appear and become ready.</summary>
    public async Task<string?> WaitForEditorAsync(string[] selectors, int timeoutSec = 15)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            foreach (var sel in selectors)
            {
                if (await QueryExistsAsync(sel))
                    return sel;
            }
            await Task.Delay(500);
        }
        return null;
    }

    /// <summary>Auto-dismiss modal dialogs (copyright, terms, warnings). Returns dismiss result.</summary>
    public async Task<string> DismissDialogAsync()
    {
        return await EvalAsync("""
            (() => {
                const dlg = document.querySelector('mat-dialog-container')
                         || document.querySelector('[role="dialog"]')
                         || document.querySelector('[role="alertdialog"]');
                if (!dlg) return 'NONE';
                const btns = dlg.querySelectorAll('button, [role="button"]');
                for (const btn of btns) {
                    const txt = (btn.textContent || '').trim().toLowerCase();
                    if (txt.includes('ok') || txt.includes('got it') || txt.includes('i understand')
                        || txt.includes('confirm') || txt.includes('agree') || txt.includes('continue')
                        || txt.includes('확인') || txt.includes('동의') || txt.includes('계속')
                        || btn.classList.contains('primary') || btn.classList.contains('mat-primary')) {
                        btn.click(); return 'DISMISSED:' + txt;
                    }
                }
                if (btns.length > 0) { btns[btns.length - 1].click(); return 'DISMISSED_LAST'; }
                return 'NO_BUTTON';
            })()
            """) ?? "NONE";
    }

    // ── Internal ──
    private static string Esc(string s) => s.Replace("\\", "\\\\").Replace("'", "\\'");
}
