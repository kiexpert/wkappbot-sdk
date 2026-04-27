using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>Select an option in a &lt;select&gt; by value or text.</summary>
    public async Task SelectAsync(string selector, string valueOrText)
    {
        var escaped = valueOrText.Replace("'", "\\'");
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                for (const opt of el.options) {
                    if (opt.value === '{{escaped}}' || opt.textContent.trim() === '{{escaped}}') {
                        el.value = opt.value;
                        el.dispatchEvent(new Event('change', { bubbles: true }));
                        return 'OK';
                    }
                }
                return 'NOT_FOUND_OPTION';
            })()
            """;
        var result = await EvalAsync(js);
        if (result?.Contains("NOT_FOUND") == true)
            throw new InvalidOperationException($"Element or option not found: {selector} = {valueOrText}");
    }

    /// <summary>Take a screenshot and return PNG bytes.</summary>
    public async Task<byte[]> ScreenshotAsync()
    {
        var result = await SendAsync("Page.captureScreenshot", new JsonObject
        {
            ["format"] = "png",
        });
        var base64 = result?["data"]?.GetValue<string>();
        return base64 != null ? Convert.FromBase64String(base64) : [];
    }

    /// <summary>Get the Chrome window handle for external Win32 operations (capture, etc).</summary>
    public IntPtr GetChromeWindowHandle() => FindChromeMainWindow();

    /// <summary>Get the current page URL.</summary>
    public async Task<string?> GetUrlAsync()
    {
        return await EvalAsync("window.location.href");
    }

    /// <summary>Get the page title.</summary>
    public async Task<string?> GetTitleAsync()
    {
        return await EvalAsync("document.title");
    }

    /// <summary>Default page read -- returns structured page summary (title, url, readyState, text snippet).</summary>
    public async Task<string?> ReadPageAsync()
    {
        return await EvalAsync("""
            (() => {
                // Fallback selector chain for SPA content (Notion, etc.)
                const selectors = [
                    '.notion-page-content',       // Notion regular pages
                    '[data-block-id]',             // Notion database views
                    '.notion-body',                // Notion fallback
                    'article', 'main', '[role="main"]',  // standard semantic
                    '#__next', '#app', '#root',    // SPA root containers
                    'document.body'                // final fallback
                ];
                let text = '';
                for (const sel of selectors) {
                    const el = sel === 'document.body' ? document.body : document.querySelector(sel);
                    if (el && el.innerText && el.innerText.trim().length > 10) {
                        text = el.innerText.substring(0, 500);
                        break;
                    }
                }
                if (!text) text = (document.body?.innerText || '').substring(0, 500);
                return JSON.stringify({
                    title: document.title,
                    url: location.href,
                    readyState: document.readyState,
                    hidden: document.hidden,
                    text: text
                });
            })()
            """);
    }

    /// <summary>Default click -- finds the most likely interactive button (send/submit/confirm).</summary>
    public async Task<string?> DefaultClickAsync()
    {
        return await EvalAsync("""
            (() => {
                const selectors = [
                    'button[type="submit"]',
                    'button[aria-label*="Send"]', 'button[aria-label*="send"]',
                    'button[aria-label*="Submit"]', 'button[data-testid*="send"]',
                    'button.send-button', 'button.submit',
                    'form button:last-of-type',
                    'button[aria-label*="확인"]', 'button[aria-label*="허용"]'
                ];
                for (const s of selectors) {
                    const el = document.querySelector(s);
                    if (el && !el.disabled && el.offsetParent !== null) {
                        el.click();
                        return 'clicked:' + s + '=' + (el.textContent||'').trim().substring(0,30);
                    }
                }
                return 'NOT_FOUND';
            })()
            """);
    }

    /// <summary>Default type -- finds editor, types text, sends with Enter (full prompt flow).</summary>
    public async Task<string?> DefaultTypeAsync(string text, bool autoSend = true)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        var js = "(() => {" +
            "const selectors = ['[contenteditable=\"true\"]','textarea:not([readonly])'," +
            "'input[type=\"text\"]:not([readonly])','input:not([type]):not([readonly])'," +
            "'[role=\"textbox\"]','.editor','.ql-editor'];" +
            "for (const s of selectors) {" +
            "const el = document.querySelector(s);" +
            "if (el && el.offsetParent !== null) {" +
            "el.focus();" +
            "if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {" +
            $"el.value = '{escaped}'; el.dispatchEvent(new Event('input', {{bubbles:true}}));" +
            "} else {" +
            $"el.textContent = '{escaped}'; el.dispatchEvent(new Event('input', {{bubbles:true}}));" +
            "}" +
            "return 'typed:' + s;" +
            "}}" +
            "return 'NOT_FOUND';})()";
        var result = await EvalAsync(js);

        // Auto-send with Enter key (AI chat flow: type -> Enter -> send)
        if (autoSend && result != null && result.StartsWith("typed:"))
        {
            await Task.Delay(100);
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
            result += "+sent";
        }
        return result;
    }

    /// <summary>Default focus -- finds the most likely editor/input and focuses it.</summary>
    public async Task<string?> DefaultFocusAsync()
    {
        return await EvalAsync("""
            (() => {
                const selectors = [
                    '[contenteditable="true"]', 'textarea:not([readonly])',
                    'input[type="text"]:not([readonly])', '[role="textbox"]'
                ];
                for (const s of selectors) {
                    const el = document.querySelector(s);
                    if (el && el.offsetParent !== null) { el.focus(); return 'focused:' + s; }
                }
                return 'NOT_FOUND';
            })()
            """);
    }

    /// <summary>Focus a DOM element by CSS selector.</summary>
    public async Task FocusAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        await EvalAsync($"document.querySelector('{escaped}')?.focus()");
    }

    /// <summary>Check if the tab is hidden (iconified/backgrounded).</summary>
    public async Task<bool> IsHiddenAsync()
    {
        var result = await EvalAsync("document.hidden.toString()");
        return result == "true";
    }

    /// <summary>Get tab state snapshot: hidden, title, DOM element count.</summary>
    public async Task<(bool hidden, string title, int elementCount)> GetTabStateAsync()
    {
        var result = await EvalAsync("document.hidden + '|' + document.title + '|' + document.querySelectorAll('*').length") ?? "true||0";
        var parts = result.Split('|', 3);
        return (
            parts[0] == "true",
            parts.Length > 1 ? parts[1] : "",
            parts.Length > 2 && int.TryParse(parts[2], out var c) ? c : 0
        );
    }

    /// <summary>Get element bounding rect as "left,top,width,height" string. Returns null if not found.</summary>
    public async Task<string?> GetElementRectAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        var result = await EvalAsync(
            $"(()=>{{var el=document.querySelector('{escaped}');if(!el)return null;" +
            "var r=el.getBoundingClientRect();" +
            "return Math.round(r.left)+','+Math.round(r.top)+','+Math.round(r.width)+','+Math.round(r.height)})()");
        return result;
    }

    /// <summary>Get the tag name of a DOM element (lowercase, e.g. "div", "textarea").</summary>
    public async Task<string> GetTagNameAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        return await EvalAsync($"document.querySelector('{escaped}')?.tagName?.toLowerCase() ?? 'unknown'") ?? "unknown";
    }

    /// <summary>Check if a DOM element exists matching a CSS selector.</summary>
    public async Task<bool> QueryExistsAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        var result = await EvalAsync($"document.querySelector('{escaped}') ? 'yes' : 'no'");
        return result == "yes";
    }

    /// <summary>Count DOM elements matching a CSS selector.</summary>
    public async Task<int> QueryCountAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        var result = await EvalAsync($"document.querySelectorAll('{escaped}').length.toString()") ?? "0";
        return int.TryParse(result, out var n) ? n : 0;
    }

    /// <summary>Count AI response turns (Gemini model-response OR article role elements).</summary>
    public async Task<int> GetResponseCountAsync()
    {
        var result = await EvalAsync(
            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
        return int.TryParse(result, out var n) ? n : 0;
    }

    /// <summary>Check if a file attachment indicator is visible (ChatGPT/Gemini/Claude).</summary>
    public async Task<string?> CheckAttachmentAsync()
    {
        return await EvalAsync("""
            (() => {
                var gpt = document.querySelector('[data-testid="file-thumbnail"]')
                        || document.querySelector('[class*="attachment"]')
                        || document.querySelector('[class*="file-upload"]')
                        || document.querySelector('img[src*="blob:"]');
                if (gpt) return 'GPT_ATTACHED';
                var gem = document.querySelector('.input-area img')
                        || document.querySelector('[class*="uploaded"]')
                        || document.querySelector('img[src*="blob:"]');
                if (gem) return 'GEM_ATTACHED';
                return 'NONE';
            })()
            """) ?? "NONE";
    }

    /// <summary>Check if file is uploading (progress indicators visible).</summary>
    public async Task<bool> IsUploadingAsync()
    {
        var result = await EvalAsync("""
            (() => {
                return document.querySelector('[class*="progress"]')
                    || document.querySelector('[class*="uploading"]')
                    || document.querySelector('[class*="loading"]')
                    ? 'YES' : 'NO';
            })()
            """) ?? "NO";
        return result == "YES";
    }

    /// <summary>Check if AI stop/cancel button is visible (generation in progress).</summary>
    public async Task<bool> IsStopButtonVisibleAsync()
    {
        var result = await EvalAsync("""
            (() => {
                if (document.querySelector('button[aria-label*="Stop"]') || document.querySelector('button[aria-label*="중지"]')) return '1';
                var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                if (mat) { var b=mat.closest('button'); if(b&&(b.getAttribute('aria-label')||b.title||'').toLowerCase().includes('stop')) return '1'; }
                return '0';
            })()
            """) ?? "0";
        return result == "1";
    }

    /// <summary>Fake visibility events for hidden/iconified tabs (forces deferred rendering).</summary>
    public async Task DispatchVisibilityAsync()
    {
        await EvalAsync(
            "try{Object.defineProperty(document,'hidden',{get:()=>false,configurable:true});" +
            "Object.defineProperty(document,'visibilityState',{get:()=>'visible',configurable:true});" +
            "document.dispatchEvent(new Event('visibilitychange'));" +
            "window.dispatchEvent(new Event('focus'));}catch(e){}");
    }

    /// <summary>Get the last AI response text (Gemini model-response / article role).</summary>
    public async Task<string?> GetLastResponseTextAsync()
    {
        return await EvalAsync(
            "(()=>{var r=document.querySelectorAll('model-response');" +
            "if(!r.length)r=document.querySelectorAll('[data-message-author-role=\"assistant\"]');" +
            "if(!r.length)r=document.querySelectorAll('[role=\"article\"]');" +
            "return r.length>0?(r[r.length-1].textContent||'').trim():''})()");
    }

    /// <summary>Get last response text with min-count guard + blank page detection.</summary>
    /// <param name="minCount">Only return text if response count > minCount (skip old responses).</param>
    /// <param name="blankDetect">If true, return "\x01BLANK" when page body is empty/broken.</param>
    public async Task<string?> GetLastResponseTextAsync(int minCount, bool blankDetect = false)
    {
        var blankCheck = blankDetect
            ? "if(!document.body||!document.body.innerHTML||document.body.innerHTML.length<100)return'\\x01BLANK';"
            : "";
        return await EvalAsync(
            "(()=>{" + blankCheck +
            "var r=document.querySelectorAll('model-response');" +
            "if(!r.length)r=document.querySelectorAll('[data-message-author-role=\"assistant\"]');" +
            "if(!r.length)r=document.querySelectorAll('[role=\"article\"]');" +
            $"if(r.length<={minCount})return '';" +
            "return(r[r.length-1].textContent||'').trim()})()");
    }

    /// <summary>Click an element via JS (no mouse events -- works when iconified).</summary>
    public async Task JsClickAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        await EvalAsync($"document.querySelector('{escaped}')?.click()");
    }

}
