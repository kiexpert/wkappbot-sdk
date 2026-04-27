using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    public async Task<string> FlushPromptPumpNowAsync(string selector)
    {
        var esc = Esc(selector);
        return await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var root=window.__wkAskPump;if(!root||!root.trySend)return 'NO_PUMP';" +
            "var st=root.states?root.states[sel]:null;" +
            "if(st)st.ticks=0;" + // reset so interval doesn't re-fire
            "return root.trySend(sel);" +
            "})()") ?? "NO_PUMP";
    }

    /// <summary>
    /// Check if JS pump set CDP_SIGNAL (ProseMirror/editors that ignore JS KeyboardEvent).
    /// Returns true and clears the flag -- caller should fire CDP Enter immediately.
    /// </summary>
    public async Task<bool> CheckPumpReadyAsync(string selector)
    {
        var esc = Esc(selector);
        var result = await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var root=window.__wkAskPump;if(!root||!root.states)return '0';" +
            "var st=root.states[sel];if(!st||!st.readyToSend)return '0';" +
            "st.readyToSend=false;st.isSending=false;" + // clear both flags -- C# has taken over
            "return '1';" +
            "})()");
        return result == "1";
    }

    /// <summary>
    /// Reset the JS pump's isSending/readyToSend flags so the interval can retry trySend.
    /// Call before watchdog recovery re-send to unblock the stuck pump.
    /// </summary>
    public async Task ResetPumpSendingAsync(string selector)
    {
        var esc = Esc(selector);
        await EvalAsync(
            "(()=>{var root=window.__wkAskPump;if(!root||!root.states)return;" +
            "var st=root.states['" + esc + "'];if(st){st.isSending=false;st.readyToSend=false;}})()");
    }

    public async Task<string> CancelPromptPumpAsync(string selector, bool clearEditor = true)
    {
        UntrackPromptPump(selector); // stop self-heal re-arm for this selector
        var esc = Esc(selector);
        var jsClear = JsonSerializer.Serialize(clearEditor);
        return await EvalAsync(
            "(()=>{" +
            "var sel='" + esc + "';" +
            "var clearEditor=" + jsClear + ";" +
            "var root=(window.__wkAskPump||(window.__wkAskPump={}));" +
            "if(!root.states)root.states={};" +
            "var st=root.states[sel]||(root.states[sel]={interval:0,lastResult:'',locked:false,ticks:0});" +
            "if(st.interval)try{clearInterval(st.interval);}catch(_e){}" +
            "st.interval=0;st.ticks=0;st.locked=false;st.lastResult='CANCELLED';st.responseStatus='CANCELLED';" +
            "var el=document.querySelector(sel);" +
            "if(clearEditor&&el){" +
                "try{" +
                    "if(el.tagName==='TEXTAREA'||el.tagName==='INPUT'){" +
                        "el.value='';" +
                        "el.dispatchEvent(new Event('input',{bubbles:true}));" +
                        "el.dispatchEvent(new Event('change',{bubbles:true}));" +
                    "}else{" +
                        "el.textContent='';" +
                        "el.dispatchEvent(new InputEvent('input',{bubbles:true,inputType:'deleteContentBackward',data:null}));" +
                        "el.dispatchEvent(new Event('change',{bubbles:true}));" +
                    "}" +
                "}catch(_e){}" +
            "}" +
            "return clearEditor?'CANCELLED:CLEARED':'CANCELLED';" +
            "})()") ?? "NO_PUMP";
    }

    /// <summary>Clear a contenteditable editor (selectAll + delete).</summary>
    public async Task ClearEditorAsync(string selector)
    {
        var esc = Esc(selector);
        await EvalAsync(
            "(()=>{var el=document.querySelector('" + esc + "');" +
            "if(!el)return;el.focus();" +
            "document.execCommand('selectAll');document.execCommand('delete')})()");
    }

    // ─ Send Button ─

    /// <summary>
    /// Send message via Enter key (modern AI chats use Enter=send, button=stop).
    /// Focuses the editor first, then dispatches Enter key.
    /// Returns: "SENT", "NO_EDITOR", "ERROR"
    /// </summary>
    public async Task<string> SendPromptAsync(string editorSelector)
    {
        TickBotOverlay(); // pulse on send
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
    /// ??WARNING: In modern AI chats, the "send" button becomes "stop" during streaming!
    /// Use SendPromptAsync() for sending, this for stopping.
    /// </summary>
    public async Task<string> ClickStopButtonAsync()
    {
        return await EvalAsync("""
            (() => {
                var sels = [
                    'button[aria-label*="Stop"]', 'button[aria-label*="\uc911\uc9c0"]',
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

    // ─ Streaming Detection ─

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

    // ─ Common Ask Flow ─

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

        // Timeout ??return last result
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

    /// <summary>Get detailed stop button info (for diagnostic logging). Returns "BTN:label", "MAT:label", or "NONE".</summary>
    public async Task<string> GetStopButtonDetailAsync()
    {
        return await EvalAsync("""
            (() => {
                var s1 = document.querySelector('button[aria-label*="Stop"]');
                var s2 = document.querySelector('button[aria-label*="\uc911\uc9c0"]');
                if (s1) return 'BTN:' + (s1.getAttribute('aria-label') || '?');
                if (s2) return 'BTN:' + (s2.getAttribute('aria-label') || '?');
                var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                if (mat) { var b = mat.closest('button'); if (b) { var l = (b.getAttribute('aria-label')||b.title||'').toLowerCase(); if (l.includes('stop')||l.includes('\uc911\uc9c0')) return 'MAT:'+l; } }
                return 'NONE';
            })()
            """) ?? "NONE";
    }

    /// <summary>Auto-dismiss modal dialogs (copyright, terms, warnings) AND
    /// non-dialog promo popovers (Claude "CoWork"-style feature banners).
    /// Strategy: scan dialog roots first for a primary-action button (OK/계속/...);
    /// fall back to close-icon buttons (aria-label/title=close/dismiss/×, or SVG
    /// hint) anywhere in the viewport. Returns dismiss result string.</summary>
    public async Task<string> DismissDialogAsync()
    {
        return await EvalAsync("""
            (() => {
                const primaryWords = ['ok','got it','i understand','confirm','agree','continue',
                    '\uc911\uc9c0','\uc815\uc9c0','계속','\ub3d9\uc758'];
                const closeWords = ['close','dismiss','not now','no thanks','maybe later',
                    '\ub2eb\uae30','\ucde8\uc18c','\ub098\uc911\uc5d0','\ub3c4\uc6c0\ub9d0\uae30','\uac70\uc808'];

                function clickPrimary(root, tag) {
                    const btns = root.querySelectorAll('button, [role="button"]');
                    for (const btn of btns) {
                        const txt = (btn.textContent || '').trim().toLowerCase();
                        if (primaryWords.some(w => txt.includes(w))
                            || btn.classList.contains('primary')
                            || btn.classList.contains('mat-primary')) {
                            btn.click(); return 'DISMISSED:' + tag + ':' + txt;
                        }
                    }
                    return null;
                }

                // (1) Classic modal dialogs -- primary-action button
                const dlg = document.querySelector('mat-dialog-container')
                         || document.querySelector('[role="dialog"]')
                         || document.querySelector('[role="alertdialog"]');
                if (dlg) {
                    const r = clickPrimary(dlg, 'dialog');
                    if (r) return r;
                    const btns = dlg.querySelectorAll('button, [role="button"]');
                    if (btns.length > 0) { btns[btns.length - 1].click(); return 'DISMISSED_LAST:dialog'; }
                }

                // (2) Promo popovers / announcement banners -- X-icon close buttons.
                // Claude's "CoWork" / new-feature banners are not role=dialog; they
                // sit in [role="region"] / aside / absolute-positioned div layers
                // with a close button labelled via aria-label / title / SVG only.
                const isVisible = (el) => {
                    if (!el || el.offsetParent === null) return false;
                    const r = el.getBoundingClientRect();
                    return r.width > 0 && r.height > 0;
                };
                const candidates = document.querySelectorAll(
                    'button[aria-label], [role="button"][aria-label], button[title]');
                for (const btn of candidates) {
                    if (!isVisible(btn)) continue;
                    const label = ((btn.getAttribute('aria-label') || '')
                        + ' ' + (btn.getAttribute('title') || '')
                        + ' ' + (btn.textContent || '')).toLowerCase();
                    if (closeWords.some(w => label.includes(w)) || label.trim() === '\u00d7' || label.trim() === 'x') {
                        // Narrow down: prefer close buttons that live inside a
                        // "feature-y" surface -- aside, role=region, or a parent
                        // whose text mentions Cowork / new / feature / try.
                        let surface = btn.closest('aside,[role="region"],[role="complementary"],[class*="banner" i],[class*="announcement" i],[class*="promo" i],[class*="cowork" i],[class*="feature" i],[class*="popover" i]');
                        if (!surface) {
                            // fallback: any ancestor with promo-like text within 4 levels
                            let p = btn.parentElement; let depth = 0;
                            while (p && depth++ < 4) {
                                const t = (p.innerText || '').slice(0, 200).toLowerCase();
                                if (t.includes('cowork') || t.includes('new feature') || t.includes('try')
                                    || t.includes('\uc0c8') /*새*/) { surface = p; break; }
                                p = p.parentElement;
                            }
                        }
                        if (surface) { btn.click(); return 'DISMISSED:promo:' + label.trim().slice(0, 40); }
                    }
                }

                return 'NONE';
            })()
            """) ?? "NONE";
    }

    // ─ File Attachment ─

}
