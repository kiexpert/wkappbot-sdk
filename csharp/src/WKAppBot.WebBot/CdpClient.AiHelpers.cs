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

    // ── Internal ──
    private static string Esc(string s) => s.Replace("\\", "\\\\").Replace("'", "\\'");
}
