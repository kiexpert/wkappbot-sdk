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
    /// <summary>Evaluate JavaScript and return the result as string. Retries with backoff on timeout.</summary>
    public async Task<string?> EvalAsync(string expression, bool awaitPromise = false)
    {
        for (int attempt = 0; attempt < 3; attempt++)
        {
            try { return await EvalAsyncCore(expression, awaitPromise); }
            catch (TimeoutException) when (attempt < 2)
            {
                var delayMs = (attempt + 1) * 500; // 500ms, 1000ms
                Console.Error.WriteLine($"[CDP:EVAL] Timeout attempt {attempt} — retry in {delayMs}ms ({expression[..Math.Min(60, expression.Length)]})");
                await Task.Delay(delayMs);
            }
        }
        return await EvalAsyncCore(expression, awaitPromise); // final attempt, let exception propagate
    }

    private async Task<string?> EvalAsyncCore(string expression, bool awaitPromise)
    {
        var parameters = new JsonObject
        {
            ["expression"] = expression,
            ["returnByValue"] = true,
        };
        if (awaitPromise)
            parameters["awaitPromise"] = true;

        JsonNode? result;
        if (_currentContextId.HasValue)
        {
            parameters["contextId"] = _currentContextId.Value;
            result = await SendAsync("Runtime.evaluate", parameters);
        }
        else
        {
            result = await SendAsync("Runtime.evaluate", parameters);
        }

        // Log JS exceptions — critical for debugging CDP automation bugs
        var exDetails = result?["exceptionDetails"];
        if (exDetails != null)
        {
            var msg = exDetails["exception"]?["description"]?.GetValue<string>()
                   ?? exDetails["text"]?.GetValue<string>()
                   ?? "unknown JS error";
            var line = exDetails["lineNumber"]?.GetValue<int>() ?? -1;
            var exprPreview = expression.Length > 120 ? expression[..120] + "..." : expression;
            exprPreview = exprPreview.Replace('\n', ' ').Replace('\r', ' ');
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[CDP:JS-ERR] {msg}{(line >= 0 ? $" (line {line})" : "")}");
            Console.WriteLine($"[CDP:JS-ERR] expr: {exprPreview}");
            // Debug context dump on every JS error
            try
            {
                Console.WriteLine($"[CDP:JS-ERR] context: wsUrl={WebSocketUrl?[..Math.Min(60, WebSocketUrl?.Length ?? 0)]}, connected={IsConnected}, chromeHwnd=0x{ChromeWindowHandle:X}");
                var urlResult = await SendAsync("Runtime.evaluate", new JsonObject { ["expression"] = "location.href", ["returnByValue"] = true });
                var pageUrl = urlResult?["result"]?["value"]?.GetValue<string>() ?? "(unknown)";
                Console.WriteLine($"[CDP:JS-ERR] page: {pageUrl}");
                // Notify caller (Slack routing etc.)
                OnJsError?.Invoke($"[CDP:JS-ERR] {msg}\nexpr: {exprPreview}\npage: {pageUrl}\nconnected={IsConnected} hwnd=0x{ChromeWindowHandle:X}");
            }
            catch { }
            Console.ResetColor();
        }

        var valueNode = result?["result"]?["value"];
        if (valueNode == null) return null;

        // Prefer GetValue<string> for string results (avoids double-escaping JSON.stringify output)
        try { return valueNode.GetValue<string>(); }
        catch
        {
            // Non-string values (numbers, bools, objects): serialize to JSON
            var json = valueNode.ToJsonString();
            return json?.Trim('"');
        }
    }

    /// <summary>
    /// Click an element by CSS selector.
    /// Primary path uses CDP mouse dispatch (trusted-like user gesture),
    /// then falls back to DOM click for compatibility.
    /// </summary>
    public async Task ClickAsync(string selector)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        // Resolve a visible clickable point first.
        var pointJson = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escapedSelector}}');
                if (!el) return JSON.stringify({ ok:false, reason:'NOT_FOUND' });

                el.scrollIntoView({ block:'center', inline:'center' });
                const r = el.getBoundingClientRect();
                if (!r || r.width <= 0 || r.height <= 0)
                    return JSON.stringify({ ok:false, reason:'NO_RECT' });

                const x = r.left + Math.min(r.width / 2, Math.max(1, r.width - 1));
                const y = r.top + Math.min(r.height / 2, Math.max(1, r.height - 1));
                return JSON.stringify({ ok:true, x, y });
            })()
            """);

        if (string.IsNullOrWhiteSpace(pointJson))
            throw new InvalidOperationException($"Failed to resolve click point: {selector}");

        JsonNode? point;
        try { point = JsonNode.Parse(pointJson); }
        catch { point = null; }

        var ok = point?["ok"]?.GetValue<bool>() ?? false;
        var reason = point? ["reason"]?.GetValue<string>();
        if (!ok)
        {
            if (reason == "NOT_FOUND")
                throw new InvalidOperationException($"Element not found: {selector}");
            throw new InvalidOperationException($"Element not clickable: {selector} ({reason ?? "unknown"})");
        }

        var x = point?["x"]?.GetValue<double>() ?? 0;
        var y = point?["y"]?.GetValue<double>() ?? 0;

        // Trusted-like path via CDP input events.
        try
        {
            await SendAsync("Input.dispatchMouseEvent", new JsonObject
            {
                ["type"] = "mouseMoved",
                ["x"] = x,
                ["y"] = y,
                ["button"] = "none"
            });

            await SendAsync("Input.dispatchMouseEvent", new JsonObject
            {
                ["type"] = "mousePressed",
                ["x"] = x,
                ["y"] = y,
                ["button"] = "left",
                ["clickCount"] = 1
            });

            await SendAsync("Input.dispatchMouseEvent", new JsonObject
            {
                ["type"] = "mouseReleased",
                ["x"] = x,
                ["y"] = y,
                ["button"] = "left",
                ["clickCount"] = 1
            });
            return;
        }
        catch
        {
            // Fallback for pages where CDP click sequence fails.
            var js = $$"""
                (() => {
                    const el = document.querySelector('{{escapedSelector}}');
                    if (!el) return 'NOT_FOUND';
                    el.click();
                    return 'OK';
                })()
                """;
            var result = await EvalAsync(js);
            if (result == "NOT_FOUND")
                throw new InvalidOperationException($"Element not found: {selector}");
        }
    }

    /// <summary>
    /// Double-click an element by CSS selector. Selects the word under cursor in text.
    /// Uses CDP Input.dispatchMouseEvent with clickCount=2.
    /// </summary>
    public async Task DoubleClickAsync(string selector)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        var pointJson = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escapedSelector}}');
                if (!el) return JSON.stringify({ ok:false, reason:'NOT_FOUND' });
                el.scrollIntoView({ block:'center', inline:'center' });
                const r = el.getBoundingClientRect();
                if (!r || r.width <= 0 || r.height <= 0)
                    return JSON.stringify({ ok:false, reason:'NO_RECT' });
                const x = r.left + Math.min(r.width / 2, Math.max(1, r.width - 1));
                const y = r.top + Math.min(r.height / 2, Math.max(1, r.height - 1));
                return JSON.stringify({ ok:true, x, y });
            })()
            """);

        if (string.IsNullOrWhiteSpace(pointJson))
            throw new InvalidOperationException($"Failed to resolve click point: {selector}");

        var point = JsonNode.Parse(pointJson);
        var ok = point?["ok"]?.GetValue<bool>() ?? false;
        if (!ok)
        {
            var reason = point?["reason"]?.GetValue<string>();
            throw new InvalidOperationException(reason == "NOT_FOUND"
                ? $"Element not found: {selector}"
                : $"Element not clickable: {selector} ({reason})");
        }

        var x = point?["x"]?.GetValue<double>() ?? 0;
        var y = point?["y"]?.GetValue<double>() ?? 0;

        // Double-click = two rapid click sequences with clickCount=2
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseMoved", ["x"] = x, ["y"] = y, ["button"] = "none" });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mousePressed", ["x"] = x, ["y"] = y, ["button"] = "left", ["clickCount"] = 1 });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseReleased", ["x"] = x, ["y"] = y, ["button"] = "left", ["clickCount"] = 1 });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mousePressed", ["x"] = x, ["y"] = y, ["button"] = "left", ["clickCount"] = 2 });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseReleased", ["x"] = x, ["y"] = y, ["button"] = "left", ["clickCount"] = 2 });
    }

    /// <summary>
    /// Type text into an element by CSS selector.
    /// Supports both input/textarea and contentEditable editors (e.g., Quill).
    /// </summary>
    public async Task TypeAsync(string selector, string text)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var escapedText = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        var js = $$"""
            (() => {
                const el = document.querySelector('{{escapedSelector}}');
                if (!el) return 'NOT_FOUND';

                const text = '{{escapedText}}';
                const tag = (el.tagName || '').toLowerCase();
                const isInputLike = tag === 'input' || tag === 'textarea';
                const isContentEditable = !!el.isContentEditable;

                el.focus();

                if (isInputLike) {
                    el.value = text;
                    if (typeof el.setSelectionRange === 'function') {
                        const n = text.length;
                        el.setSelectionRange(n, n);
                    }
                    try {
                        el.dispatchEvent(new InputEvent('input', { bubbles: true, data: text, inputType: 'insertText' }));
                    } catch {
                        el.dispatchEvent(new Event('input', { bubbles: true }));
                    }
                    el.dispatchEvent(new Event('change', { bubbles: true }));
                    return 'OK_INPUT';
                }

                if (isContentEditable) {
                    const doc = el.ownerDocument || document;
                    const win = doc.defaultView || window;

                    // Place caret inside editor and replace existing content.
                    const sel = win.getSelection();
                    const range = doc.createRange();
                    range.selectNodeContents(el);
                    sel.removeAllRanges();
                    sel.addRange(range);

                    // Framework-friendly path first.
                    try { doc.execCommand('selectAll', false, null); } catch {}
                    let inserted = false;
                    try { inserted = doc.execCommand('insertText', false, text); } catch {}

                    // Fallback for editors that block execCommand.
                    if (!inserted) {
                        el.textContent = text;
                    }

                    try {
                        el.dispatchEvent(new InputEvent('input', { bubbles: true, data: text, inputType: 'insertText' }));
                    } catch {
                        el.dispatchEvent(new Event('input', { bubbles: true }));
                    }
                    el.dispatchEvent(new Event('change', { bubbles: true }));
                    return 'OK_CONTENTEDITABLE';
                }

                return 'UNSUPPORTED';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
        if (result == "UNSUPPORTED")
            throw new InvalidOperationException($"Element is neither input/textarea nor contentEditable: {selector}");
    }

    /// <summary>
    /// Type text using CDP Input.insertText — bypasses IME composition issues.
    /// Best for CJK (Korean/Japanese/Chinese) input in contentEditable editors (Notion, Slack, etc.).
    /// </summary>
    public async Task TypeInsertTextAsync(string selector, string text)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        // Focus the element first
        var focusResult = await EvalAsync($"(() => {{ const el = document.querySelector('{escapedSelector}'); if (!el) return 'NOT_FOUND'; el.focus(); return 'OK'; }})()");
        if (focusResult == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");

        // Use CDP Input.insertText — injects text directly without key events
        await SendAsync("Input.insertText", new JsonObject { ["text"] = text });
    }

    /// <summary>Get text content of an element by CSS selector.</summary>
    public async Task<string?> GetTextAsync(string selector)
    {
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                return el ? el.textContent : null;
            })()
            """;
        return await EvalAsync(js);
    }

    /// <summary>Get trimmed text length of an element (0 if not found).</summary>
    public async Task<int> GetTextLengthAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        var result = await EvalAsync($"document.querySelector('{escaped}')?.textContent?.trim()?.length ?? 0") ?? "0";
        return int.TryParse(result, out var len) ? len : 0;
    }

    /// <summary>Get value of a form element by CSS selector.</summary>
    public async Task<string?> GetValueAsync(string selector)
    {
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                return el ? el.value : null;
            })()
            """;
        return await EvalAsync(js);
    }

    /// <summary>Check/uncheck a checkbox by CSS selector.</summary>
    public async Task SetCheckedAsync(string selector, bool @checked)
    {
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                if (el.checked !== {{(@checked ? "true" : "false")}}) {
                    el.click();
                }
                return 'OK';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
    }

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

    /// <summary>Default page read — returns structured page summary (title, url, readyState, text snippet).</summary>
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

    /// <summary>Default click — finds the most likely interactive button (send/submit/confirm).</summary>
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

    /// <summary>Default type — finds editor, types text, sends with Enter (full prompt flow).</summary>
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

        // Auto-send with Enter key (AI chat flow: type → Enter → send)
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

    /// <summary>Default focus — finds the most likely editor/input and focuses it.</summary>
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

    /// <summary>Click an element via JS (no mouse events — works when iconified).</summary>
    public async Task JsClickAsync(string selector)
    {
        var escaped = selector.Replace("'", "\\'");
        await EvalAsync($"document.querySelector('{escaped}')?.click()");
    }

    /// <summary>
    /// Activate this tab in Chrome (bring to front).
    /// Focusless-safe: ensures Chrome is iconic before Page.bringToFront so
    /// tab activation happens internally without stealing OS focus.
    /// </summary>
    public async Task BringToFrontAsync()
    {
        // Check if our target is already the active tab in Chrome (via CDP)
        // If so, Page.bringToFront is a no-op — skip the minimize dance
        bool alreadyActive = false;
        try
        {
            var visible = await EvalAsync("document.visibilityState") ?? "";
            alreadyActive = visible == "visible";
        }
        catch { }

        if (!alreadyActive)
        {
            // Tab is not active → minimize Chrome before activation to prevent OS focus theft
            var hwnd = GetChromeWindowHandle();
            bool didMinimize = false;
            if (hwnd != IntPtr.Zero && !IsIconic(hwnd))
            {
                Console.Error.WriteLine($"[CDP] Chrome minimized for tab switch (hwnd={hwnd:X8})");
                ShowWindowNative(hwnd, 6); // SW_MINIMIZE
                ScheduleMinimizeDump("bring-to-front-tab-switch", hwnd);
                didMinimize = true;
                await Task.Delay(50);
            }
            await SendAsync("Page.bringToFront");
            // Restore after tab switch settles
            if (didMinimize)
            {
                await Task.Delay(200);
                RestoreChromeNoActivate();
            }
        }
    }

    /// <summary>
    /// Bring this tab to the OS foreground (recovery use only — steals focus).
    /// Uses Page.bringToFront which activates the tab AND brings Chrome to front.
    /// </summary>
    /// <summary>
    /// Make Chrome window visible WITHOUT stealing OS focus (SW_SHOWNOACTIVATE).
    /// Renderer becomes active + compositor runs, but foreground stays with caller.
    /// Use before CDP input that needs a visible (non-minimized) renderer.
    /// Call MinimizeChromeAsync() when done to restore minimized state.
    /// </summary>
    public void RestoreChromeNoActivate()
    {
        var hwnd = GetChromeWindowHandle();
        if (hwnd == IntPtr.Zero) return;
        // SW_SHOWNOACTIVATE=4: visible, not minimized, does NOT steal focus
        ShowWindowNative(hwnd, 4);
        CancelMinimizeDump("restore-no-activate");
        Console.WriteLine("[CDP] Chrome restored (SW_SHOWNOACTIVATE — no focus steal)");
    }

    public void MinimizeChrome()
    {
        var hwnd = GetChromeWindowHandle();
        if (hwnd == IntPtr.Zero) { Console.WriteLine("[CDP] MinimizeChrome: hwnd=zero (Chrome not found)"); return; }
        // SW_MINIMIZE=6
        ShowWindowNative(hwnd, 6);
        ScheduleMinimizeDump("explicit-minimize", hwnd);
        var stack = new System.Diagnostics.StackTrace(1, true).ToString();
        var caller = stack.Length > 200 ? stack[..200] : stack;
        Console.Error.WriteLine($"[CDP:MINIMIZE] Chrome minimized (hwnd={hwnd:X8})\n  callstack: {caller.Replace("\n", "\n  ")}");
    }

    /// <summary>Legacy recovery — replaced by RestoreChromeNoActivate. Kept as no-op.</summary>
    public Task BringTabToFrontAsync() => Task.CompletedTask;

    /// <summary>
    /// Activate this tab in Chrome.
    /// WARNING: Target.activateTarget DOES steal OS foreground window when Chrome is in background —
    /// Chrome calls SetForegroundWindow internally as part of tab activation.
    /// Unlike Page.bringToFront (even more aggressive), but still NOT truly focusless.
    ///
    /// Truly focusless alternative: skip this call entirely.
    /// CDP Runtime.evaluate / DOM commands work on background tabs via targetId WebSocket.
    /// Only call this when the tab MUST be visible (e.g. rendering-dependent operations).
    /// </summary>
    /// <summary>No-op — tab activation not needed; CDP operates on background tabs via targetId.</summary>
    public Task ActivateTabAsync() => Task.CompletedTask;

    /// <summary>
    /// Intercept file chooser dialog and provide files programmatically (fully focusless).
    /// Uses Input.dispatchMouseEvent (trusted gesture) so Chrome opens the file chooser,
    /// which is intercepted by Page.setInterceptFileChooserDialog BEFORE the native OS dialog
    /// appears → no focus stealing at all.
    /// Steps: 1) Enable interception 2) Trusted-click upload button 3) Wait for fileChooserOpened
    ///         4) If menu appeared, trusted-click menu item + wait again 5) handleFileChooser
    /// </summary>
    public async Task<bool> SetFileViaChooserAsync(string absolutePath, int timeoutMs = 5000)
    {
        try
        {
            // Enable file chooser interception BEFORE the click — intercepts before OS dialog opens
            await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = true });
            _fileChooserTcs = new TaskCompletionSource<JsonNode?>();

            // Get upload button coords for trusted gesture
            var btnInfo = await EvalAsync("""
                (() => {
                    var btn = document.querySelector('button[aria-label*="파일 업로드"]')
                           || document.querySelector('button[aria-label*="Upload"]')
                           || document.querySelector('button[aria-label*="Attach"]')
                           || document.querySelector('button[aria-label*="첨부"]')
                           || document.querySelector('button[aria-label*="Add file"]');
                    if (!btn) return 'NO_BTN';
                    var r = btn.getBoundingClientRect();
                    return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + (btn.getAttribute('aria-label') || '');
                })()
            """);
            Console.WriteLine($"[CDP] FileChooser btn: {btnInfo}");
            if (btnInfo == "NO_BTN") return false;

            // Trusted gesture click — Chrome treats this as real user input for file chooser
            var btnCoords = btnInfo!.Split(':')[0].Split(',');
            await TrustedClickAsync(int.Parse(btnCoords[0]), int.Parse(btnCoords[1]));

            // Wait for file chooser event (direct open — no menu)
            using var cts = new CancellationTokenSource(timeoutMs);
            cts.Token.Register(() => _fileChooserTcs.TrySetCanceled());

            try
            {
                await _fileChooserTcs.Task;
            }
            catch (TaskCanceledException)
            {
                // Menu opened instead of direct file chooser — find and trusted-click menu item
                var menuInfo = await EvalAsync("""
                    (() => {
                        var items = document.querySelectorAll('[role=menuitem], [role=option]');
                        for (var item of items) {
                            var t = (item.textContent || '').trim();
                            if (t === '파일 업로드' || t === 'Upload file' || t === 'Upload'
                                || t.includes('컴퓨터') || t.includes('Computer') || t.includes('내 컴퓨터')) {
                                var r = item.getBoundingClientRect();
                                return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + t;
                            }
                        }
                        // Broader fallback
                        var all = document.querySelectorAll('[role=menuitem], [role=option], li, button');
                        for (var item of all) {
                            var t = (item.textContent || '').trim();
                            if (t && (t.includes('업로드') || t.includes('Upload'))) {
                                var r = item.getBoundingClientRect();
                                return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + t;
                            }
                        }
                        return 'NO_MENU_ITEM';
                    })()
                """);
                Console.WriteLine($"[CDP] FileChooser menu: {menuInfo}");
                if (menuInfo == "NO_MENU_ITEM") return false;

                // Re-enable interception + reset TCS before trusted-click
                await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = true });
                _fileChooserTcs = new TaskCompletionSource<JsonNode?>();

                var menuCoords = menuInfo!.Split(':')[0].Split(',');
                await TrustedClickAsync(int.Parse(menuCoords[0]), int.Parse(menuCoords[1]));

                using var cts2 = new CancellationTokenSource(12000); // 12s — React menu click may take time
                cts2.Token.Register(() => _fileChooserTcs.TrySetCanceled());
                try { await _fileChooserTcs.Task; }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("[CDP] FileChooser: no event after menu trusted-click — trying speculative handleFileChooser...");
                    // Chrome may still be holding the pending chooser even after our TCS timed out.
                    // Speculatively send handleFileChooser — if Chrome accepts it, the file is set.
                    try
                    {
                        var fp2 = absolutePath.Replace('\\', '/');
                        await SendAsync("Page.handleFileChooser", new JsonObject
                        {
                            ["action"] = "accept",
                            ["files"] = new JsonArray { fp2 },
                        });
                        Console.WriteLine("[CDP] FileChooser: speculative accept sent — file likely set");
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine($"[CDP] FileChooser: speculative accept failed: {ex2.Message}");
                    }
                    return false;
                }
            }

            // Accept — Chrome provides file to the page without opening native OS dialog
            var filePath = absolutePath.Replace('\\', '/');
            await SendAsync("Page.handleFileChooser", new JsonObject
            {
                ["action"] = "accept",
                ["files"] = new JsonArray { filePath },
            });
            Console.WriteLine($"[CDP] FileChooser: accepted (focusless)");
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CDP] FileChooser error: {ex.Message}");
            return false;
        }
        finally
        {
            _fileChooserTcs = null;
            try { await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = false }); }
            catch { }
        }
    }

    /// <summary>Send a trusted mouse click via CDP Input.dispatchMouseEvent (page coords).</summary>
    async Task TrustedClickAsync(int x, int y)
    {
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
        {
            ["type"] = "mousePressed", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
        await Task.Delay(50);
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
        {
            ["type"] = "mouseReleased", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
    }

    /// <summary>
    /// Disable CDP file chooser interception so the native OS file dialog can open.
    /// </summary>
    public async Task DisableFileChooserInterception()
    {
        try { await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = false }); }
        catch { }
    }

    // ── Hotkey / Keyboard dispatch ────────────────────────────────

    /// <summary>
    /// CDP Input.dispatchKeyEvent — 포커스리스 키 이벤트 발송 (탭 내 포커스 기준).
    /// modifiers: 0=none, 1=Alt, 2=Ctrl, 4=Meta, 8=Shift (OR 조합)
    /// </summary>
    public async Task DispatchKeyAsync(string key, int modifiers = 0, string code = "")
    {
        var obj = new JsonObject
        {
            ["type"] = "keyDown", ["key"] = key,
            ["modifiers"] = modifiers,
        };
        if (!string.IsNullOrEmpty(code)) obj["code"] = code;
        await SendAsync("Input.dispatchKeyEvent", obj);
        await Task.Delay(20);
        obj["type"] = "keyUp";
        await SendAsync("Input.dispatchKeyEvent", obj);
    }

    /// <summary>
    /// DOM 전체에서 accesskey + aria-keyshortcuts 속성을 스캔하여 핫키 맵 반환.
    /// 반환: List of (label, accessKey, keyshortcuts, selector)
    /// label = innerText / aria-label / title 순으로 추출.
    /// </summary>
    public async Task<List<CdpHotkeyEntry>> GetHotkeyMapAsync()
    {
        const string js = """
            (function() {
              var all = document.querySelectorAll('[accesskey],[aria-keyshortcuts]');
              var result = [];
              all.forEach(function(el) {
                var label = (el.innerText || el.getAttribute('aria-label') ||
                             el.getAttribute('title') || el.getAttribute('value') || '').trim()
                              .replace(/\s+/g,' ').substring(0, 80);
                var ak  = el.getAttribute('accesskey') || '';
                var aks = el.getAttribute('aria-keyshortcuts') || '';
                var sel = '';
                if (el.id) sel = '#' + el.id;
                else if (el.className) sel = el.tagName.toLowerCase() + '.' + el.className.trim().split(/\s+/)[0];
                else sel = el.tagName.toLowerCase();
                result.push({ label: label, accesskey: ak, keyshortcuts: aks, selector: sel });
              });
              return JSON.stringify(result);
            })()
            """;
        var json = await EvalAsync(js);
        if (string.IsNullOrWhiteSpace(json) || json == "null") return [];
        json = json.Trim('"').Replace("\\\"", "\"").Replace("\\\\", "\\");
        try
        {
            var arr = System.Text.Json.JsonSerializer.Deserialize<List<CdpHotkeyEntry>>(json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return arr ?? [];
        }
        catch { return []; }
    }

    /// <summary>
    /// aria-keyshortcuts / accesskey 문자열을 파싱하여 CDP DispatchKeyAsync 호출.
    /// "Alt+S", "Ctrl+Shift+S", "s" (accesskey → Alt+key) 등 지원.
    /// </summary>
    public async Task<bool> DispatchShortcutAsync(string shortcut, bool isAccessKey = false)
    {
        // accesskey는 브라우저가 Alt(+Shift)로 발동 — 여기선 Alt 조합으로 처리
        if (isAccessKey && shortcut.Length == 1)
        {
            await DispatchKeyAsync(shortcut.ToUpperInvariant(), modifiers: 1); // Alt=1
            Console.WriteLine($"  [CDP-HOTKEY] accesskey '{shortcut}' → Alt+{shortcut.ToUpperInvariant()}");
            return true;
        }

        // "Alt+S", "Ctrl+Shift+F5" 파싱
        var parts = shortcut.Split('+');
        var key = parts[^1].Trim();
        int mods = 0;
        foreach (var p in parts[..^1])
        {
            mods |= p.Trim().ToLowerInvariant() switch
            {
                "alt"   => 1,
                "ctrl" or "control" => 2,
                "meta" or "cmd"     => 4,
                "shift" => 8,
                _ => 0
            };
        }
        await DispatchKeyAsync(key, mods);
        Console.WriteLine($"  [CDP-HOTKEY] '{shortcut}' → key={key} mods={mods}");
        return true;
    }

    /// <summary>
    /// Right-click an element by CSS selector (context menu).
    /// Uses CDP Input.dispatchMouseEvent with button="right".
    /// </summary>
    public async Task RightClickAsync(string selector)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        var pointJson = await EvalAsync($$"""
            (() => {
                const el = document.querySelector('{{escapedSelector}}');
                if (!el) return JSON.stringify({ ok:false, reason:'NOT_FOUND' });
                el.scrollIntoView({ block:'center', inline:'center' });
                const r = el.getBoundingClientRect();
                if (!r || r.width <= 0 || r.height <= 0)
                    return JSON.stringify({ ok:false, reason:'NO_RECT' });
                const x = r.left + Math.min(r.width / 2, Math.max(1, r.width - 1));
                const y = r.top + Math.min(r.height / 2, Math.max(1, r.height - 1));
                return JSON.stringify({ ok:true, x, y });
            })()
            """);

        if (string.IsNullOrWhiteSpace(pointJson))
            throw new InvalidOperationException($"Failed to resolve click point: {selector}");

        var point = JsonNode.Parse(pointJson);
        var ok = point?["ok"]?.GetValue<bool>() ?? false;
        if (!ok)
        {
            var reason = point?["reason"]?.GetValue<string>();
            throw new InvalidOperationException(reason == "NOT_FOUND"
                ? $"Element not found: {selector}"
                : $"Element not clickable: {selector} ({reason})");
        }

        var x = point?["x"]?.GetValue<double>() ?? 0;
        var y = point?["y"]?.GetValue<double>() ?? 0;

        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseMoved", ["x"] = x, ["y"] = y, ["button"] = "none" });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mousePressed", ["x"] = x, ["y"] = y, ["button"] = "right", ["clickCount"] = 1 });
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
            { ["type"] = "mouseReleased", ["x"] = x, ["y"] = y, ["button"] = "right", ["clickCount"] = 1 });
    }

    /// <summary>
    /// Scroll an element or the page by CSS selector.
    /// Uses Element.scrollBy for element scroll, window.scrollBy for page scroll.
    /// </summary>
    public async Task ScrollAsync(string? selector, string direction, string amount)
    {
        // Parse amount: "page" = viewport height, number = pixels, default = 300px
        var amountJs = amount?.ToLowerInvariant() switch
        {
            "page" => direction is "up" or "down"
                ? "(window.innerHeight * 0.85)"
                : "(window.innerWidth * 0.85)",
            null or "" => "300",
            _ => int.TryParse(amount, out var px) ? px.ToString() : "300"
        };

        var (dx, dy) = direction?.ToLowerInvariant() switch
        {
            "up" => ("0", $"-{amountJs}"),
            "down" => ("0", amountJs),
            "left" => ($"-{amountJs}", "0"),
            "right" => (amountJs, "0"),
            _ => ("0", amountJs) // default = down
        };

        if (string.IsNullOrEmpty(selector) || selector == "window" || selector == "page")
        {
            await EvalAsync($"window.scrollBy({dx}, {dy})");
        }
        else
        {
            var esc = selector.Replace("\\", "\\\\").Replace("'", "\\'");
            var js = $"(() => {{ const el = document.querySelector('{esc}'); if (!el) return 'NOT_FOUND'; el.scrollBy({dx}, {dy}); return 'OK'; }})()";
            var result = await EvalAsync(js);
            if (result == "NOT_FOUND")
                throw new InvalidOperationException($"Element not found: {selector}");
        }
    }

    /// <summary>
    /// Expand or collapse a details/disclosure element, or toggle aria-expanded.
    /// </summary>
    public async Task ExpandCollapseAsync(string selector, bool expand)
    {
        var esc = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var js = $$"""
            (() => {
                const el = document.querySelector('{{esc}}');
                if (!el) return 'NOT_FOUND';
                // <details> element
                if (el.tagName === 'DETAILS') {
                    el.open = {{(expand ? "true" : "false")}};
                    return 'OK';
                }
                // aria-expanded toggle
                const cur = el.getAttribute('aria-expanded');
                if (cur !== null) {
                    el.setAttribute('aria-expanded', '{{(expand ? "true" : "false")}}');
                    el.click();
                    return 'OK';
                }
                // Fallback: just click (many accordions toggle on click)
                el.click();
                return 'OK_CLICK';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "ShowWindow")]
    private static extern bool ShowWindowNative(IntPtr hWnd, int nCmdShow);
}

/// <summary>DOM 핫키 스캔 결과 엔트리.</summary>
public record CdpHotkeyEntry(
    string Label,
    string Accesskey,
    string Keyshortcuts,
    string Selector);
