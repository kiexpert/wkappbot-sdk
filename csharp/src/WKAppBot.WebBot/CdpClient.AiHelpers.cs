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
    /// Click the send button for a specific AI provider.
    /// Returns: "CLICKED", "DISABLED", "NOT_FOUND"
    /// </summary>
    public async Task<string> ClickSendButtonAsync(string provider)
    {
        var selectors = provider.ToLowerInvariant() switch
        {
            "claude" => """
                'button[aria-label="Send Message"]',
                'button[data-testid="chat-input-grid-area"] button[type="submit"]',
                'button[data-testid="chat-input"] button[type="submit"]',
                'button[aria-label*="Send"]'
                """,
            "gemini" => """
                'button[aria-label="Send message"]',
                'button[aria-label*="Send"]',
                '.send-button',
                'button.send-button'
                """,
            "gpt" or "chatgpt" => """
                'button[data-testid="send-button"]',
                'button[aria-label="Send prompt"]',
                'button[aria-label*="Send"]',
                'form button[type="submit"]:not([disabled])'
                """,
            _ => "'button[type=\"submit\"]','button[aria-label*=\"Send\"]'"
        };

        return await EvalAsync(
            "(()=>{var sels=[" + selectors + "];" +
            "for(var s of sels){var b=document.querySelector(s);" +
            "if(b){if(b.disabled)return 'DISABLED';" +
            "b.click();return 'CLICKED:'+s}}" +
            "return 'NOT_FOUND'})()") ?? "NOT_FOUND";
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
