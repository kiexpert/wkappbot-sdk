using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Minimal Chrome DevTools Protocol client using System.Net.WebSockets.
/// Zero external dependencies — talks to Chrome via WebSocket JSON-RPC.
///
/// Usage:
///   var cdp = new CdpClient();
///   await cdp.ConnectAsync(9222);                        // connect to Chrome debugging port
///   await cdp.NavigateAsync("https://naver.com");        // navigate
///   var title = await cdp.EvalAsync("document.title");   // eval JS
///   await cdp.ClickAsync("#btn-login");                  // click by CSS selector
///   await cdp.TypeAsync("#search", "hello");             // type into input
///   var png = await cdp.ScreenshotAsync();               // capture page
///   await cdp.DisconnectAsync();
/// </summary>
public sealed class CdpClient : IAsyncDisposable, IDisposable
{
    private ClientWebSocket? _ws;
    private int _messageId;
    private readonly ConcurrentDictionary<int, TaskCompletionSource<JsonNode?>> _pending = new();
    private CancellationTokenSource? _receiveCts;
    private Task? _receiveTask;
    private readonly HttpClient _http = new();

    public bool IsConnected => _ws?.State == WebSocketState.Open;
    public string? WebSocketUrl { get; private set; }

    /// <summary>
    /// Connect to Chrome's DevTools WebSocket.
    /// Chrome must be running with --remote-debugging-port=PORT.
    /// </summary>
    public async Task ConnectAsync(int port = 9222, int tabIndex = 0)
    {
        // Get available targets from Chrome's JSON API
        var json = await _http.GetStringAsync($"http://localhost:{port}/json");
        var targets = JsonSerializer.Deserialize<JsonArray>(json);
        if (targets == null || targets.Count == 0)
            throw new InvalidOperationException("No Chrome targets found");

        // Find the first 'page' type target
        string? wsUrl = null;
        foreach (var target in targets)
        {
            var type = target?["type"]?.GetValue<string>();
            if (type == "page")
            {
                wsUrl = target?["webSocketDebuggerUrl"]?.GetValue<string>();
                if (tabIndex-- <= 0 && wsUrl != null)
                    break;
            }
        }

        if (wsUrl == null)
            throw new InvalidOperationException("No page target with WebSocket URL found");

        WebSocketUrl = wsUrl;
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);

        // Start background receive loop
        _receiveCts = new CancellationTokenSource();
        _receiveTask = ReceiveLoopAsync(_receiveCts.Token);
    }

    /// <summary>
    /// Send a CDP command and wait for the response.
    /// </summary>
    public async Task<JsonNode?> SendAsync(string method, JsonObject? parameters = null, int timeoutMs = 10000)
    {
        if (_ws == null || _ws.State != WebSocketState.Open)
            throw new InvalidOperationException("Not connected");

        var id = Interlocked.Increment(ref _messageId);
        var tcs = new TaskCompletionSource<JsonNode?>();
        _pending[id] = tcs;

        var msg = new JsonObject
        {
            ["id"] = id,
            ["method"] = method,
        };
        if (parameters != null)
            msg["params"] = parameters;

        var bytes = Encoding.UTF8.GetBytes(msg.ToJsonString());
        await _ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

        // Wait for response with timeout
        using var cts = new CancellationTokenSource(timeoutMs);
        try
        {
            cts.Token.Register(() => tcs.TrySetCanceled());
            return await tcs.Task;
        }
        catch (TaskCanceledException)
        {
            _pending.TryRemove(id, out _);
            throw new TimeoutException($"CDP command '{method}' timed out after {timeoutMs}ms");
        }
    }

    /// <summary>
    /// When true, injects a WebBot URL bar at the top of each page after navigation,
    /// and updates document.title to "{PageTitle} - WebBot v0.1".
    /// </summary>
    public bool InjectWebBotBar { get; set; }

    /// <summary>Navigate to a URL.</summary>
    public async Task NavigateAsync(string url)
    {
        await SendAsync("Page.navigate", new JsonObject { ["url"] = url });
        // Wait for page load
        await SendAsync("Page.enable");
        // Small delay for page to settle
        await Task.Delay(500);

        // Inject WebBot bar and update title
        if (InjectWebBotBar)
            await InjectBarAsync();
    }

    /// <summary>
    /// Inject a fixed URL bar at the top of the page showing current URL,
    /// and update document.title to "{PageTitle} - WebBot v0.1".
    /// The bar auto-updates on pushState/replaceState/popstate via MutationObserver.
    /// </summary>
    private async Task InjectBarAsync()
    {
        try
        {
            await EvalAsync("""
                (() => {
                    // Remove existing bar if re-injected
                    const existing = document.getElementById('__wkappbot_bar');
                    if (existing) existing.remove();

                    // Create the bar
                    const bar = document.createElement('div');
                    bar.id = '__wkappbot_bar';
                    bar.style.cssText = `
                        position: fixed; top: 0; left: 0; right: 0; z-index: 2147483647;
                        height: 28px; background: #1a1a2e; color: #e0e0e0;
                        font: 12px/28px 'Consolas', 'Courier New', monospace;
                        display: flex; align-items: center; padding: 0 10px;
                        box-shadow: 0 1px 4px rgba(0,0,0,0.3);
                        user-select: text; -webkit-user-select: text;
                    `;

                    // Logo/brand
                    const brand = document.createElement('span');
                    brand.textContent = 'WebBot';
                    brand.style.cssText = `
                        color: #4fc3f7; font-weight: bold; margin-right: 8px;
                        font-size: 13px; letter-spacing: 0.5px;
                    `;

                    // Separator
                    const sep = document.createElement('span');
                    sep.textContent = '|';
                    sep.style.cssText = 'color: #555; margin: 0 8px;';

                    // URL display
                    const urlEl = document.createElement('span');
                    urlEl.id = '__wkappbot_url';
                    urlEl.style.cssText = `
                        color: #aaa; flex: 1; overflow: hidden;
                        text-overflow: ellipsis; white-space: nowrap;
                    `;
                    urlEl.textContent = location.href;

                    // Status dot (green = connected)
                    const dot = document.createElement('span');
                    dot.id = '__wkappbot_dot';
                    dot.style.cssText = `
                        width: 8px; height: 8px; border-radius: 50%;
                        background: #4caf50; margin-left: 8px; flex-shrink: 0;
                    `;
                    dot.title = 'CDP Connected';

                    bar.appendChild(brand);
                    bar.appendChild(sep);
                    bar.appendChild(urlEl);
                    bar.appendChild(dot);

                    // Push page content down so bar doesn't overlap
                    document.body.style.marginTop = '32px';
                    document.body.insertBefore(bar, document.body.firstChild);

                    // Update title
                    const origTitle = document.title || location.hostname || 'Untitled';
                    document.title = origTitle + ' - WebBot v0.1';

                    // Auto-update URL on SPA navigation (pushState/popstate)
                    const updateUrl = () => {
                        const u = document.getElementById('__wkappbot_url');
                        if (u) u.textContent = location.href;
                    };
                    window.addEventListener('popstate', updateUrl);
                    const origPush = history.pushState;
                    history.pushState = function() {
                        origPush.apply(this, arguments);
                        updateUrl();
                    };
                    const origReplace = history.replaceState;
                    history.replaceState = function() {
                        origReplace.apply(this, arguments);
                        updateUrl();
                    };

                    return 'BAR_INJECTED';
                })()
            """);
        }
        catch { /* best effort */ }
    }

    /// <summary>Evaluate JavaScript and return the result as string.</summary>
    public async Task<string?> EvalAsync(string expression)
    {
        var result = await SendAsync("Runtime.evaluate", new JsonObject
        {
            ["expression"] = expression,
            ["returnByValue"] = true,
        });
        return result?["result"]?["value"]?.ToJsonString()?.Trim('"');
    }

    /// <summary>Click an element by CSS selector.</summary>
    public async Task ClickAsync(string selector)
    {
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.click();
                return 'OK';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
    }

    /// <summary>Type text into an element by CSS selector (sets value + dispatches input event).</summary>
    public async Task TypeAsync(string selector, string text)
    {
        var escapedText = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        var js = $$"""
            (() => {
                const el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                el.value = '{{escapedText}}';
                el.dispatchEvent(new Event('input', { bubbles: true }));
                el.dispatchEvent(new Event('change', { bubbles: true }));
                return 'OK';
            })()
            """;
        var result = await EvalAsync(js);
        if (result == "NOT_FOUND")
            throw new InvalidOperationException($"Element not found: {selector}");
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

    /// <summary>Wait for an element to appear (polling).</summary>
    public async Task<bool> WaitForElementAsync(string selector, int timeoutMs = 5000)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var js = $"document.querySelector('{selector}') !== null";
            var result = await EvalAsync(js);
            if (result == "true") return true;
            await Task.Delay(200);
        }
        return false;
    }

    /// <summary>Get page HTML source.</summary>
    public async Task<string?> GetHtmlAsync()
    {
        return await EvalAsync("document.documentElement.outerHTML");
    }

    /// <summary>Disconnect from Chrome.</summary>
    public async Task DisconnectAsync()
    {
        _receiveCts?.Cancel();
        if (_ws?.State == WebSocketState.Open)
        {
            try
            {
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            catch { /* best effort */ }
        }
        if (_receiveTask != null)
        {
            try { await _receiveTask; } catch { }
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
        _ws?.Dispose();
        _receiveCts?.Dispose();
        _http.Dispose();
    }

    public void Dispose()
    {
        DisconnectAsync().GetAwaiter().GetResult();
        _ws?.Dispose();
        _receiveCts?.Dispose();
        _http.Dispose();
    }

    // ── Background receive loop ──

    private async Task ReceiveLoopAsync(CancellationToken ct)
    {
        var buffer = new byte[1024 * 64]; // 64KB buffer
        var sb = new StringBuilder();

        while (!ct.IsCancellationRequested && _ws?.State == WebSocketState.Open)
        {
            try
            {
                sb.Clear();
                WebSocketReceiveResult result;
                do
                {
                    result = await _ws.ReceiveAsync(buffer, ct);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var msg = JsonNode.Parse(sb.ToString());
                if (msg == null) continue;

                // Response to a command (has "id")
                var id = msg["id"]?.GetValue<int>();
                if (id.HasValue && _pending.TryRemove(id.Value, out var tcs))
                {
                    var error = msg["error"];
                    if (error != null)
                        tcs.TrySetException(new InvalidOperationException(
                            $"CDP error: {error["message"]?.GetValue<string>()}"));
                    else
                        tcs.TrySetResult(msg["result"]);
                }
                // Events (has "method") — ignore for now, can be extended later
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (WebSocketException)
            {
                break;
            }
        }
    }
}
