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
    /// <summary>Evaluate JavaScript and return the result as string. Retries with backoff on timeout/cancellation.
    /// <param name="timeoutMs">Per-attempt timeout in ms. 0 = use SendAsync default (10s). Use higher values for heavy JS (e.g. ArmPromptPump).</param>
    /// </summary>
    public async Task<string?> EvalAsync(string expression, bool awaitPromise = false, int timeoutMs = 0)
    {
        // 2 retries with 500ms/1000ms backoff, then 1 final attempt (3 total)
        for (int attempt = 0; attempt < 2; attempt++)
        {
            try { return await EvalAsyncCore(expression, awaitPromise, timeoutMs); }
            catch (Exception ex) when (ex is TimeoutException or TaskCanceledException)
            {
                var delayMs = (attempt + 1) * 500; // 500ms, 1000ms
                Console.Error.WriteLine($"[CDP:EVAL] {ex.GetType().Name} attempt {attempt} -- retry in {delayMs}ms ({expression[..Math.Min(60, expression.Length)]})");
                await Task.Delay(delayMs);
            }
        }
        return await EvalAsyncCore(expression, awaitPromise, timeoutMs); // final attempt, let exception propagate
    }

    private async Task<string?> EvalAsyncCore(string expression, bool awaitPromise, int timeoutMs = 0)
    {
        var parameters = new JsonObject
        {
            ["expression"] = expression,
            ["returnByValue"] = true,
        };
        if (awaitPromise)
            parameters["awaitPromise"] = true;

        JsonNode? result;
        var sendTimeoutMs = timeoutMs > 0 ? timeoutMs : 10000; // default 10s
        if (_currentContextId.HasValue)
        {
            parameters["contextId"] = _currentContextId.Value;
            result = await SendAsync("Runtime.evaluate", parameters, sendTimeoutMs);
        }
        else
        {
            result = await SendAsync("Runtime.evaluate", parameters, sendTimeoutMs);
        }

        // Log JS exceptions -- critical for debugging CDP automation bugs
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
    /// Type text using CDP Input.insertText -- bypasses IME composition issues.
    /// Best for CJK (Korean/Japanese/Chinese) input in contentEditable editors (Notion, Slack, etc.).
    /// </summary>
    public async Task TypeInsertTextAsync(string selector, string text)
    {
        var escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'");

        // Focus the element first
        var focusResult = await EvalAsync($"(() => {{ const el = document.querySelector('{escapedSelector}'); if (!el) return 'NOT_FOUND'; el.focus(); return 'OK'; }})()");
        if (focusResult == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");

        // Use CDP Input.insertText -- injects text directly without key events
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
}
