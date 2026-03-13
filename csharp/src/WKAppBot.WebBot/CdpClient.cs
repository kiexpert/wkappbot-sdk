using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Minimal Chrome DevTools Protocol client using System.Net.WebSockets.
/// Zero external dependencies -- talks to Chrome via WebSocket JSON-RPC.
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
    private TaskCompletionSource<JsonNode?>? _fileChooserTcs;
    private readonly HttpClient _http = new();

    public bool IsConnected => _ws?.State == WebSocketState.Open;
    public string? WebSocketUrl { get; private set; }
    public string? TargetId { get; private set; }
    private int? _currentContextId;

    /// <summary>Chrome browser process ID (resolved from CDP port).</summary>
    public int ChromePid { get; private set; }

    /// <summary>
    /// Focus theft monitoring for every CDP SendAsync call.
    /// When enabled, detects if Chrome steals OS foreground after each CDP command.
    /// OnFocusTheft(method, prevFg, curFg) is called when focus stolen — caller can log/restore.
    /// </summary>
    public bool EnableFocusTheftMonitoring { get; set; }
    public Action<string, nint, nint>? OnFocusTheft { get; set; }

    /// <summary>
    /// Connect to Chrome's DevTools WebSocket.
    /// Chrome must be running with --remote-debugging-port=PORT.
    /// </summary>
    public async Task ConnectAsync(int port = 9222, int tabIndex = 0, int timeoutMs = 10_000, string? preferredTargetTag = null)
    {
        using var cts = new CancellationTokenSource(timeoutMs);
        var ct = cts.Token;

        // Get available targets from Chrome's JSON API
        var json = await _http.GetStringAsync($"http://localhost:{port}/json", ct);
        var targets = JsonSerializer.Deserialize<JsonArray>(json);
        if (targets == null || targets.Count == 0)
            throw new InvalidOperationException("No Chrome targets found");

        string? wsUrl = null;
        string? resolvedTargetId = null;
        string? resolvedTargetUrl = null;
        foreach (var target in targets)
        {
            var type = target?["type"]?.GetValue<string>();
            if (type != "page") continue;

            var url = target?["url"]?.GetValue<string>();
            var id = target?["id"]?.GetValue<string>();
            if (!string.IsNullOrWhiteSpace(preferredTargetTag) && url != null && url.Contains(preferredTargetTag, StringComparison.OrdinalIgnoreCase))
            {
                wsUrl = target?["webSocketDebuggerUrl"]?.GetValue<string>();
                resolvedTargetId = id;
                resolvedTargetUrl = url;
                break;
            }

            if (tabIndex-- <= 0)
            {
                wsUrl = target?["webSocketDebuggerUrl"]?.GetValue<string>();
                resolvedTargetId = id;
                resolvedTargetUrl = url;
                break;
            }
        }


        if (wsUrl == null)
            throw new InvalidOperationException("No page target with WebSocket URL found");

        TargetId = resolvedTargetId;
        WebSocketUrl = wsUrl;
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), ct);

        // Resolve Chrome browser PID from the CDP port
        ChromePid = ResolvePidFromPort(port);

        // Start background receive loop BEFORE sending commands (otherwise responses are never read)
        _receiveCts = new CancellationTokenSource();
        _receiveTask = ReceiveLoopAsync(_receiveCts.Token);

        // Re-enable main-world contexts on every run
        await SendAsync("Runtime.enable");
        await SendAsync("Page.enable");
        await SendAsync("DOM.enable");
        await SendAsync("Page.getFrameTree");

        // Refresh execution context
        try
        {
            var contextInfo = await SendAsync("Runtime.getExecutionContexts");
            var contexts = contextInfo?["result"] as JsonArray;
            var mainContext = contexts?.FirstOrDefault();
            _currentContextId = mainContext?["id"]?.GetValue<int?>();
        }
        catch { }
    }

    /// <summary>
    /// Find the PID of the process listening on a TCP port using netstat.
    /// Fallback: returns 0 if unable to determine.
    /// </summary>
    private static int ResolvePidFromPort(int port)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using var proc = Process.Start(psi);
            if (proc == null) return 0;

            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit(3000);

            // Parse netstat output: look for LISTENING on our port
            // Format: "  TCP    127.0.0.1:9222    0.0.0.0:0    LISTENING    12345"
            var needle = $":{port}";
            foreach (var rawLine in output.Split('\n'))
            {
                var line = rawLine.Trim();
                if (!line.Contains(needle) || !line.Contains("LISTENING")) continue;

                // Verify the port matches exactly (not :92220)
                var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) continue;
                var local = parts[1]; // e.g. "127.0.0.1:9222"
                if (!local.EndsWith(needle)) continue;

                if (int.TryParse(parts[^1], out var pid))
                    return pid;
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// Send a CDP command and wait for the response.
    /// </summary>
    public async Task<JsonNode?> SendAsync(string method, JsonObject? parameters = null, int timeoutMs = 10000)
    {
        if (_ws == null || _ws.State != WebSocketState.Open)
            throw new InvalidOperationException("Not connected");

        // Focus theft monitoring: capture prevFg just before send
        nint prevFg = 0;
        if (EnableFocusTheftMonitoring && OnFocusTheft != null)
            prevFg = (nint)GetForegroundWindow();

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
        JsonNode? result;
        try
        {
            cts.Token.Register(() => tcs.TrySetCanceled());
            result = await tcs.Task;
        }
        catch (TaskCanceledException)
        {
            _pending.TryRemove(id, out _);
            throw new TimeoutException($"CDP command '{method}' timed out after {timeoutMs}ms");
        }

        // Focus theft check after response received
        if (prevFg != 0 && OnFocusTheft != null)
        {
            var curFg = (nint)GetForegroundWindow();
            if (curFg != prevFg)
            {
                OnFocusTheft(method, prevFg, curFg);
                SetForegroundWindow((IntPtr)prevFg);
            }
        }

        return result;
    }

    /// <summary>
    /// When true, injects a WebBot URL bar at the top of each page after navigation,
    /// and updates document.title to "{PageTitle} - WKWebBot v0.1".
    /// </summary>
    public bool InjectWebBotBar { get; set; }

    /// <summary>Navigate to a URL. Focusless: does NOT steal focus from user's active window.</summary>
    public async Task NavigateAsync(string url)
    {
        // Save current foreground window BEFORE navigation (focusless principle!)
        var prevForeground = GetForegroundWindow();

        await SendAsync("Page.navigate", new JsonObject { ["url"] = url });
        // Wait for page load
        await SendAsync("Page.enable");
        // Small delay for page to settle
        await Task.Delay(500);

        // Inject WebBot bar and update title
        if (InjectWebBotBar)
            await InjectBarAsync();

        // Force Chrome window title via Win32 (Chrome ignores document.title in some profiles)
        if (InjectWebBotBar)
            await SetWindowTitleAsync();

        // Restore focus to user's previous window (don't steal focus!)
        RestoreFocus(prevForeground);
    }

    /// <summary>Restore focus to a previously active window (best-effort, non-blocking).</summary>
    private static void RestoreFocus(IntPtr prevHwnd)
    {
        if (prevHwnd == IntPtr.Zero) return;
        try
        {
            var currentFg = GetForegroundWindow();
            if (currentFg != prevHwnd && IsWindow(prevHwnd))
            {
                SetForegroundWindow(prevHwnd);
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>
    /// Inject top URL bar + bottom status bar, and update document.title.
    /// Top bar: WebBot brand + URL + connection dot
    /// Bottom bar: last action + elapsed time + step counter
    /// Auto-updates URL on SPA navigation (pushState/popstate).
    /// </summary>
    public async Task InjectBarAsync()
    {
        try
        {
            await EvalAsync("""
                (() => {
                    // Remove existing bars if re-injected
                    document.getElementById('__wkappbot_bar')?.remove();
                    document.getElementById('__wkappbot_status')?.remove();

                    // -- Shared style constants --
                    const DARK = '#1a1a2e';
                    const FONT = "12px/24px 'Consolas', 'Courier New', monospace";
                    const CYAN = '#4fc3f7';
                    const GREEN = '#4caf50';

                    // -- Top bar: brand + URL + dot --
                    const bar = document.createElement('div');
                    bar.id = '__wkappbot_bar';
                    bar.style.cssText = `
                        position: fixed; top: 0; left: 0; right: 0; z-index: 2147483647;
                        height: 26px; background: ${DARK}; color: #e0e0e0;
                        font: ${FONT}; display: flex; align-items: center;
                        padding: 0 10px; box-shadow: 0 1px 4px rgba(0,0,0,0.3);
                        user-select: text; -webkit-user-select: text;
                    `;

                    const brand = document.createElement('span');
                    brand.textContent = 'WKWebBot';
                    brand.style.cssText = `color:${CYAN};font-weight:bold;margin-right:8px;font-size:13px;letter-spacing:.5px;`;

                    const sep = document.createElement('span');
                    sep.textContent = '|';
                    sep.style.cssText = 'color:#555;margin:0 8px;';

                    const urlEl = document.createElement('span');
                    urlEl.id = '__wkappbot_url';
                    urlEl.style.cssText = 'color:#aaa;flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;';
                    urlEl.textContent = location.href;

                    const dot = document.createElement('span');
                    dot.id = '__wkappbot_dot';
                    dot.style.cssText = `width:8px;height:8px;border-radius:50%;background:${GREEN};margin-left:8px;flex-shrink:0;`;
                    dot.title = 'CDP Connected';

                    bar.append(brand, sep, urlEl, dot);

                    // -- Bottom status bar: action + time + counter --
                    const status = document.createElement('div');
                    status.id = '__wkappbot_status';
                    status.style.cssText = `
                        position: fixed; bottom: 0; left: 0; right: 0; z-index: 2147483647;
                        height: 28px; background: #2d2d4e; color: #aaa;
                        font: 12px/28px 'Consolas', 'Courier New', monospace;
                        display: flex; align-items: center; padding: 0 10px;
                        border-top: 2px solid ${CYAN};
                        box-shadow: 0 -2px 8px rgba(0,0,0,0.4);
                    `;

                    // Status icon (idle/running indicator)
                    const icon = document.createElement('span');
                    icon.id = '__wkappbot_icon';
                    icon.textContent = '\u25CF';
                    icon.style.cssText = `color:${GREEN};margin-right:6px;font-size:12px;`;
                    icon.title = 'Idle';

                    // Last action display
                    const action = document.createElement('span');
                    action.id = '__wkappbot_action';
                    action.style.cssText = 'color:#aaa;flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;';
                    action.textContent = 'Ready';

                    // Step counter
                    const counter = document.createElement('span');
                    counter.id = '__wkappbot_counter';
                    counter.style.cssText = 'color:#666;margin-left:10px;';
                    counter.textContent = '';

                    // Elapsed time
                    const elapsed = document.createElement('span');
                    elapsed.id = '__wkappbot_elapsed';
                    elapsed.style.cssText = 'color:#666;margin-left:10px;min-width:60px;text-align:right;';
                    elapsed.textContent = '';

                    status.append(icon, action, counter, elapsed);

                    // Push page content to avoid overlap (top + bottom)
                    document.body.style.marginTop = '30px';
                    document.body.style.marginBottom = '26px';
                    document.body.insertBefore(bar, document.body.firstChild);
                    // Append status bar to <html> instead of <body>
                    // Some sites (Naver Finance etc) use overflow/scroll on body that clips fixed children
                    document.documentElement.appendChild(status);

                    // Update title (short -- no URL)
                    // Even pages with no title get at least "WKWebBot v0.1" in the window title bar
                    const origTitle = document.title || location.hostname || '';
                    document.title = origTitle ? origTitle + ' - WKWebBot v0.1' : 'WKWebBot v0.1';

                    // Auto-update URL on SPA navigation
                    const updateUrl = () => {
                        const u = document.getElementById('__wkappbot_url');
                        if (u) u.textContent = location.href;
                    };
                    window.addEventListener('popstate', updateUrl);
                    const origPush = history.pushState;
                    history.pushState = function() { origPush.apply(this, arguments); updateUrl(); };
                    const origReplace = history.replaceState;
                    history.replaceState = function() { origReplace.apply(this, arguments); updateUrl(); };

                    // Watch for title changes (MutationObserver on <title> element)
                    // Stores pending title so C# can sync Win32 window title
                    const titleEl = document.querySelector('head > title') || (() => {
                        const t = document.createElement('title');
                        document.head.appendChild(t);
                        return t;
                    })();
                    const titleObs = new MutationObserver(() => {
                        const curr = document.title;
                        if (window.__wkappbot) {
                            // Only flag if title doesn't already end with our suffix
                            if (!curr.endsWith('- WKWebBot v0.1')) {
                                document.title = curr ? curr + ' - WKWebBot v0.1' : 'WKWebBot v0.1';
                            }
                            window.__wkappbot._pendingTitle = document.title;
                        }
                    });
                    titleObs.observe(titleEl, { childList: true, characterData: true, subtree: true });

                    // Global helper for status updates from CDP
                    window.__wkappbot = {
                        _pendingTitle: null,
                        stepCount: 0,
                        setAction(text, isError) {
                            const a = document.getElementById('__wkappbot_action');
                            const i = document.getElementById('__wkappbot_icon');
                            if (a) { a.textContent = text; a.style.color = isError ? '#f44336' : '#aaa'; }
                            if (i) { i.style.color = isError ? '#f44336' : '#4caf50'; i.title = isError ? 'Error' : 'OK'; }
                        },
                        setRunning(text) {
                            const a = document.getElementById('__wkappbot_action');
                            const i = document.getElementById('__wkappbot_icon');
                            if (a) { a.textContent = text; a.style.color = '#ffb74d'; }
                            if (i) { i.style.color = '#ffb74d'; i.title = 'Running'; }
                        },
                        setElapsed(ms) {
                            const e = document.getElementById('__wkappbot_elapsed');
                            if (e) e.textContent = ms >= 1000 ? (ms/1000).toFixed(1)+'s' : ms+'ms';
                        },
                        setCounter(n, total) {
                            this.stepCount = n;
                            const c = document.getElementById('__wkappbot_counter');
                            if (c) c.textContent = total ? `[${n}/${total}]` : `#${n}`;
                        },
                    };

                    return 'BARS_INJECTED';
                })()
            """);
        }
        catch { /* best effort */ }
    }

    /// <summary>Update the bottom status bar with current action info.</summary>
    public async Task UpdateStatusAsync(string action, int? stepNum = null, int? totalSteps = null, int? elapsedMs = null, bool isError = false)
    {
        if (!InjectWebBotBar) return;
        try
        {
            var escapedAction = action.Replace("\\", "\\\\").Replace("'", "\\'");
            var js = $"window.__wkappbot?.setAction('{escapedAction}', {(isError ? "true" : "false")});";
            if (stepNum.HasValue)
                js += $"window.__wkappbot?.setCounter({stepNum.Value},{totalSteps ?? 0});";
            if (elapsedMs.HasValue)
                js += $"window.__wkappbot?.setElapsed({elapsedMs.Value});";
            await EvalAsync(js);
        }
        catch { /* best effort */ }

        // Auto-sync Win32 window title if page changed document.title
        await SyncTitleIfChangedAsync();
    }

    /// <summary>
    /// Check if document.title changed (via MutationObserver flag) and sync Win32 window title.
    /// Called automatically from UpdateStatusAsync/SetStatusRunningAsync.
    /// </summary>
    private async Task SyncTitleIfChangedAsync()
    {
        if (!InjectWebBotBar) return;
        try
        {
            var pending = await EvalAsync("(() => { const t = window.__wkappbot?._pendingTitle; if (t) { window.__wkappbot._pendingTitle = null; return t; } return ''; })()");
            if (!string.IsNullOrEmpty(pending))
            {
                await SetWindowTitleAsync(pending);
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>Set status bar to "running" state (orange).</summary>
    public async Task SetStatusRunningAsync(string action)
    {
        if (!InjectWebBotBar) return;
        try
        {
            var escaped = action.Replace("\\", "\\\\").Replace("'", "\\'");
            await EvalAsync($"window.__wkappbot?.setRunning('{escaped}');");
        }
        catch { /* best effort */ }
    }

    /// <summary>Evaluate JavaScript and return the result as string.</summary>
    public async Task<string?> EvalAsync(string expression, bool awaitPromise = false)
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

        // Log JS exceptions to console (previously silent — returned null with no trace)
        var exDetails = result?["exceptionDetails"];
        if (exDetails != null)
        {
            var msg = exDetails["exception"]?["description"]?.GetValue<string>()
                   ?? exDetails["text"]?.GetValue<string>()
                   ?? "unknown JS error";
            var line = exDetails["lineNumber"]?.GetValue<int>() ?? -1;
            Console.WriteLine($"[CDP:JS-ERR] {msg}{(line >= 0 ? $" (line {line})" : "")}");
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

    /// <summary>
    /// Activate this tab in Chrome (bring to front).
    /// Makes Chrome's window title bar show this tab's title.
    /// WARNING: This steals focus! Only call when user explicitly wants to see the window.
    /// </summary>
    public async Task BringToFrontAsync()
    {
        await SendAsync("Page.bringToFront");
    }

    /// <summary>
    /// Activate this tab in Chrome WITHOUT stealing OS foreground window.
    /// Uses Target.activateTarget which makes the tab active in Chrome internally
    /// (Chrome-level tab switch) without triggering an OS SetForegroundWindow call.
    /// Unlike Page.bringToFront which explicitly brings Chrome to front.
    /// </summary>
    public async Task ActivateTabAsync()
    {
        try
        {
            var tid = TargetId;
            if (!string.IsNullOrEmpty(tid))
            {
                await SendAsync("Target.activateTarget", new JsonObject { ["targetId"] = tid });
                Console.WriteLine($"[CDP] Tab activated (focusless): {tid[..8]}…");
            }
        }
        catch { }
    }

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

    /// <summary>
    /// Minimize Chrome window (focusless -- does not steal focus from user's active window).
    /// CDP still works perfectly when Chrome is minimized!
    /// </summary>
    public void MinimizeChromeWindow()
    {
        var hwnd = FindChromeMainWindow();
        if (hwnd != IntPtr.Zero)
            ShowWindow(hwnd, SW_SHOWMINNOACTIVE);
    }

    /// <summary>
    /// Restore Chrome window without stealing focus.
    /// Uses SetWindowPlacement(SW_SHOWNOACTIVATE) which actually restores from
    /// minimized state, unlike ShowWindow(SW_SHOWNOACTIVATE) which is a no-op
    /// when the window is iconic.
    /// </summary>
    public void RestoreChromeWindow()
    {
        var hwnd = FindChromeMainWindow();
        if (hwnd == IntPtr.Zero) return;

        var iconic = IsIconic(hwnd);

        if (iconic)
        {
            // ShowWindow(SW_RESTORE=9) is the only reliable way to un-minimize.
            // It briefly activates, so restore user's foreground immediately after.
            var prevFg = GetForegroundWindow();
            ShowWindow(hwnd, 9); // SW_RESTORE
            if (prevFg != IntPtr.Zero && prevFg != hwnd)
                SetForegroundWindow(prevFg);
        }
        else
        {
            ShowWindow(hwnd, SW_SHOWNOACTIVATE);
        }
    }

    /// <summary>
    /// Set the Chrome window title bar directly via Win32 SetWindowText.
    /// Chrome ignores document.title for the window title in some profiles,
    /// so we force it via Win32 P/Invoke on the Chrome main window.
    /// </summary>
    public async Task SetWindowTitleAsync(string? customTitle = null)
    {
        try
        {
            // Get page title from CDP
            var pageTitle = customTitle ?? await EvalAsync("document.title") ?? "";
            if (string.IsNullOrEmpty(pageTitle))
                pageTitle = "WKWebBot v0.1";

            // Find Chrome process from CDP port (localhost:{port})
            var chromeHwnd = FindChromeMainWindow();
            if (chromeHwnd != IntPtr.Zero)
            {
                SetWindowTextW(chromeHwnd, pageTitle);
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>
    /// Find the main Chrome window handle by enumerating top-level windows.
    /// Uses ChromePid to find the exact Chrome instance connected via CDP.
    /// Falls back to first visible Chrome_WidgetWin_1 if PID not available.
    /// </summary>
    private IntPtr FindChromeMainWindow()
    {
        IntPtr found = IntPtr.Zero;
        var sb = new StringBuilder(512);
        var targetPid = ChromePid;

        EnumWindows((hwnd, _) =>
        {
            GetClassNameW(hwnd, sb, sb.Capacity);
            var cls = sb.ToString();
            if (cls != "Chrome_WidgetWin_1") return true; // continue

            // Check if visible main window (not popup/child)
            if (!IsWindowVisible(hwnd)) return true;
            var style = GetWindowLong(hwnd, -16); // GWL_STYLE
            if ((style & 0x00C00000) == 0) return true; // WS_CAPTION required

            // Match by PID if known
            if (targetPid > 0)
            {
                GetWindowThreadProcessId(hwnd, out var pid);
                if (pid != targetPid) return true; // wrong Chrome instance
            }

            found = hwnd;
            return false; // stop
        }, IntPtr.Zero);

        return found;
    }

    // Win32 P/Invoke for window title management
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SetWindowTextW(IntPtr hWnd, string lpString);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [StructLayout(LayoutKind.Sequential)]
    private struct WINDOWPLACEMENT
    {
        public int length, flags, showCmd;
        public System.Drawing.Point ptMinPosition, ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }

    private const int SW_MINIMIZE = 6;
    private const int SW_SHOWNOACTIVATE = 4;   // Show at previous size/position without activating
    private const int SW_SHOWMINNOACTIVE = 7;  // Show minimized without activating

    /// <summary>
    /// Set files on a file input element (e.g., for file upload).
    /// Uses CDP DOM.setFileInputFiles to programmatically set files without opening a file dialog.
    /// </summary>
    public async Task<bool> SetFileInputAsync(string selector, string filePath)
    {
        // Enable DOM domain (required for DOM operations)
        await SendAsync("DOM.enable");

        // Get document root first
        await SendAsync("DOM.getDocument");

        // Get the element's remote object via Runtime.evaluate
        var evalResult = await SendAsync("Runtime.evaluate", new JsonObject
        {
            ["expression"] = $"document.querySelector('{selector}')",
            ["returnByValue"] = false
        });

        var exDetailsFile = evalResult?["exceptionDetails"];
        if (exDetailsFile != null)
        {
            var msg = exDetailsFile["exception"]?["description"]?.GetValue<string>()
                   ?? exDetailsFile["text"]?.GetValue<string>() ?? "unknown JS error";
            Console.WriteLine($"[CDP:JS-ERR] SetFileInput: {msg}");
        }
        var objectId = evalResult?["result"]?["objectId"]?.GetValue<string>();
        if (string.IsNullOrEmpty(objectId))
            return false;

        // Request DOM node for the JS object
        var reqResult = await SendAsync("DOM.requestNode", new JsonObject
        {
            ["objectId"] = objectId
        });

        var nodeId = reqResult?["nodeId"]?.GetValue<int>() ?? 0;
        if (nodeId == 0)
            return false;

        // Set the file using DOM.setFileInputFiles with nodeId
        await SendAsync("DOM.setFileInputFiles", new JsonObject
        {
            ["files"] = new JsonArray { filePath },
            ["nodeId"] = nodeId
        });

        return true;
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

    // ── Window management (CDP Browser domain + Target domain) ──

    /// <summary>
    /// Expected WebBot window bounds (right side of virtual screen, near top).
    /// Position: rightmost monitor upper area, 800x600.
    /// Uses GetSystemMetrics (physical pixels) — same coordinate space as SetWindowPos/CDP.
    /// For WPF Eye alignment, CLI computes separately using SystemParameters (logical px).
    /// </summary>
    public static (int X, int Y, int W, int H) ExpectedBounds => ComputeExpectedBounds();
    private const int BoundsTolerance = 50;

    private static (int X, int Y, int W, int H) ComputeExpectedBounds()
    {
        const int W = 800, H = 600;
        const int Corner = 20; // offset from top-right corner of rightmost monitor
        try
        {
            // Virtual screen = union of all monitors
            int vx = GetSystemMetrics(76);  // SM_XVIRTUALSCREEN
            int vw = GetSystemMetrics(78);  // SM_CXVIRTUALSCREEN
            int rightEdge = vx + vw; // rightmost pixel of virtual screen

            // Find the rightmost monitor's top-Y using MonitorFromPoint + GetMonitorInfo
            // Point at top-right corner of virtual screen → belongs to rightmost monitor
            int monitorTop = 0;
            var pt = new POINT { x = rightEdge - 1, y = 0 };
            var hMon = MonitorFromPoint(pt, 2 /* MONITOR_DEFAULTTONEAREST */);
            if (hMon != IntPtr.Zero)
            {
                var mi = new MONITORINFO { cbSize = 40 }; // sizeof(MONITORINFO) = 40
                if (GetMonitorInfo(hMon, ref mi))
                    monitorTop = mi.rcMonitor_top;
            }

            int x = rightEdge - W - Corner;
            int y = monitorTop + Corner;
            return (x, y, W, H);
        }
        catch
        {
            return (100, Corner, W, H);
        }
    }

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT { public int x, y; }

    [StructLayout(LayoutKind.Sequential)]
    private struct MONITORINFO
    {
        public int cbSize;
        public int rcMonitor_left, rcMonitor_top, rcMonitor_right, rcMonitor_bottom;
        public int rcWork_left, rcWork_top, rcWork_right, rcWork_bottom;
        public int dwFlags;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Get the Chrome window bounds for a given target via CDP Browser.getWindowForTarget.
    /// Returns (windowId, left, top, width, height) or null if unavailable.
    /// </summary>
    public async Task<(int windowId, int left, int top, int width, int height)?> GetWindowForTargetAsync(string? targetId = null)
    {
        try
        {
            var param = new JsonObject();
            if (targetId != null) param["targetId"] = targetId;
            var result = await SendAsync("Browser.getWindowForTarget", param, timeoutMs: 3000);
            if (result == null) return null;

            var windowId = result["windowId"]?.GetValue<int>() ?? 0;
            var bounds = result["bounds"];
            if (bounds == null) return null;

            return (windowId,
                bounds["left"]?.GetValue<int>() ?? 0,
                bounds["top"]?.GetValue<int>() ?? 0,
                bounds["width"]?.GetValue<int>() ?? 0,
                bounds["height"]?.GetValue<int>() ?? 0);
        }
        catch { return null; }
    }

    /// <summary>Set Chrome window bounds via CDP Browser.setWindowBounds.</summary>
    public async Task<bool> SetWindowBoundsAsync(int windowId, int left, int top, int width, int height)
    {
        try
        {
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = windowId,
                ["bounds"] = new JsonObject
                {
                    ["left"] = left,
                    ["top"] = top,
                    ["width"] = width,
                    ["height"] = height,
                    ["windowState"] = "normal"
                }
            });
            return true;
        }
        catch { return false; }
    }

    /// <summary>Minimize Chrome window via CDP Browser.setWindowBounds. Focusless — no OS activation.</summary>
    public async Task<bool> MinimizeWindowAsync(string? targetId = null)
    {
        try
        {
            var wb = await GetWindowForTargetAsync(targetId);
            if (wb == null) return false;
            await SendAsync("Browser.setWindowBounds", new JsonObject
            {
                ["windowId"] = wb.Value.windowId,
                ["bounds"] = new JsonObject { ["windowState"] = "minimized" }
            });
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Create a new Chrome window with a blank tab via CDP Target.createTarget.
    /// Returns the new target's ID, or null on failure.
    /// </summary>
    public async Task<string?> CreateTargetInNewWindowAsync(string url = "about:blank")
    {
        try
        {
            var result = await SendAsync("Target.createTarget", new JsonObject
            {
                ["url"] = url,
                ["newWindow"] = true
            });
            return result?["targetId"]?.GetValue<string>();
        }
        catch { return null; }
    }

    // ── Static: CDP port detection from process ─────────────────

    /// <summary>
    /// Detect CDP debugging port from a process's command line arguments.
    /// Parses --remote-debugging-port=NNNN from the process command line via WMI.
    /// Returns 0 if not found or not a Chromium-based process.
    /// </summary>
    /// <summary>Known CDP ports to probe (common defaults).</summary>
    private static readonly int[] KnownCdpPorts = [9222, 9223, 9224, 9225, 9229];

    public static int DetectCdpPort(int processId)
    {
        // Strategy: check known ports via netstat → match to target PID
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using var proc = Process.Start(psi);
            if (proc == null) return 0;
            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit(3000);

            // Get all PIDs in the process tree (chrome spawns many child processes)
            var targetPids = GetProcessTreePids(processId);

            foreach (var port in KnownCdpPorts)
            {
                var needle = $":{port}";
                foreach (var rawLine in output.Split('\n'))
                {
                    var line = rawLine.Trim();
                    if (!line.Contains(needle) || !line.Contains("LISTENING")) continue;
                    var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 5) continue;
                    if (!parts[1].EndsWith(needle)) continue;
                    if (int.TryParse(parts[^1], out var listenPid) && targetPids.Contains(listenPid))
                        return port;
                }
            }

            // Broader scan: any LISTENING port owned by this process tree in high range
            foreach (var rawLine in output.Split('\n'))
            {
                var line = rawLine.Trim();
                if (!line.Contains("LISTENING")) continue;
                var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) continue;
                if (!int.TryParse(parts[^1], out var listenPid) || !targetPids.Contains(listenPid)) continue;
                var local = parts[1]; // e.g. "127.0.0.1:9222"
                var colonIdx = local.LastIndexOf(':');
                if (colonIdx < 0) continue;
                if (int.TryParse(local[(colonIdx + 1)..], out var foundPort) && foundPort >= 9222 && foundPort <= 9999)
                {
                    // Verify it's actually a CDP endpoint
                    try
                    {
                        using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                        var json = http.GetStringAsync($"http://localhost:{foundPort}/json/version").GetAwaiter().GetResult();
                        if (json.Contains("Browser") || json.Contains("webSocketDebuggerUrl"))
                            return foundPort;
                    }
                    catch { }
                }
            }
        }
        catch { }
        return 0;
    }

    private static HashSet<int> GetProcessTreePids(int rootPid)
    {
        var pids = new HashSet<int> { rootPid };
        try
        {
            // Simple: get all processes with same name as root
            var rootProc = Process.GetProcessById(rootPid);
            var name = rootProc.ProcessName;
            rootProc.Dispose();
            foreach (var p in Process.GetProcessesByName(name))
            {
                pids.Add(p.Id);
                p.Dispose();
            }
        }
        catch { }
        return pids;
    }

    /// <summary>
    /// Detect CDP port from a process name (e.g. "chrome", "msedge", "code").
    /// Scans all processes with that name and returns the first found port.
    /// </summary>
    public static int DetectCdpPortByName(string processName)
    {
        try
        {
            foreach (var p in Process.GetProcessesByName(processName))
            {
                try
                {
                    var port = DetectCdpPort(p.Id);
                    if (port > 0) return port;
                }
                finally { p.Dispose(); }
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// Check if a window class name indicates a Chromium-based web view.
    /// </summary>
    public static bool IsWebViewClass(string className)
    {
        return className is "Chrome_WidgetWin_1" or "Chrome_WidgetWin_0"
            or "Chromium_WidgetWin_1" or "Chromium_WidgetWin_0";
    }

    /// <summary>Tab info from CDP /json endpoint.</summary>
    public record TabInfo(string Id, string Title, string Url, string? WsUrl);

    /// <summary>List all page tabs via CDP /json.</summary>
    public async Task<List<TabInfo>> ListTabsAsync(int port = 9222)
    {
        var result = new List<TabInfo>();
        try
        {
            var json = await _http.GetStringAsync($"http://localhost:{port}/json");
            var targets = JsonSerializer.Deserialize<JsonArray>(json);
            if (targets == null) return result;
            foreach (var t in targets)
            {
                if (t?["type"]?.GetValue<string>() != "page") continue;
                result.Add(new TabInfo(
                    t?["id"]?.GetValue<string>() ?? "",
                    t?["title"]?.GetValue<string>() ?? "",
                    t?["url"]?.GetValue<string>() ?? "",
                    t?["webSocketDebuggerUrl"]?.GetValue<string>()));
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Find a tab by URL/title pattern (wildcard * supported).
    /// Returns null if no match found — never opens a new tab.
    /// </summary>
    public async Task<TabInfo?> FindTabByPatternAsync(int port, string pattern)
    {
        var tabs = await ListTabsAsync(port);
        // If no wildcards, auto-wrap with * for substring match
        var matchPattern = pattern.Contains('*') ? pattern : $"*{pattern}*";
        foreach (var tab in tabs)
        {
            if (GlobMatch(tab.Title, matchPattern) || GlobMatch(tab.Url, matchPattern))
                return tab;
        }
        return null;
    }

    /// <summary>
    /// Connect to a specific tab by pattern match (URL or title).
    /// Returns true if found and connected, false if no match.
    /// </summary>
    public async Task<bool> ConnectToTabAsync(int port, string pattern)
    {
        var tab = await FindTabByPatternAsync(port, pattern);
        if (tab == null) return false;
        if (tab.Id == TargetId) return true; // already connected
        return await SwitchToTargetAsync(tab.Id, port);
    }

    static bool GlobMatch(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        // Simple wildcard: * matches any sequence
        var parts = pattern.Split('*');
        int idx = 0;
        for (int i = 0; i < parts.Length; i++)
        {
            if (string.IsNullOrEmpty(parts[i])) continue;
            var found = text.IndexOf(parts[i], idx, StringComparison.OrdinalIgnoreCase);
            if (found < 0) return false;
            if (i == 0 && !pattern.StartsWith("*") && found != 0) return false;
            idx = found + parts[i].Length;
        }
        if (!pattern.EndsWith("*") && idx != text.Length && parts.Length > 0 && !string.IsNullOrEmpty(parts[^1]))
            return false;
        return true;
    }

    /// <summary>
    /// Switch this CdpClient to a different page target (disconnect + reconnect).
    /// The port is needed to look up the new target's WebSocket URL from /json.
    /// </summary>
    public async Task<bool> SwitchToTargetAsync(string targetId, int port)
    {
        // Find new target's WebSocket URL
        string? wsUrl = null;
        try
        {
            var json = await _http.GetStringAsync($"http://localhost:{port}/json");
            var targets = JsonSerializer.Deserialize<JsonArray>(json);
            if (targets != null)
            {
                foreach (var t in targets)
                {
                    if (t?["id"]?.GetValue<string>() == targetId)
                    {
                        wsUrl = t?["webSocketDebuggerUrl"]?.GetValue<string>();
                        break;
                    }
                }
            }
        }
        catch { return false; }

        if (wsUrl == null) return false;

        // Disconnect current
        _receiveCts?.Cancel();
        if (_ws?.State == WebSocketState.Open)
        {
            try { await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None); }
            catch { }
        }
        if (_receiveTask != null)
        {
            try { await _receiveTask; } catch { }
        }
        _ws?.Dispose();
        _receiveCts?.Dispose();
        _pending.Clear();

        // Connect to new target
        TargetId = targetId;
        WebSocketUrl = wsUrl;
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);

        _receiveCts = new CancellationTokenSource();
        _receiveTask = ReceiveLoopAsync(_receiveCts.Token);

        await SendAsync("Runtime.enable");
        await SendAsync("Page.enable");
        await SendAsync("DOM.enable");

        try
        {
            var contextInfo = await SendAsync("Runtime.getExecutionContexts");
            var contexts = contextInfo?["result"] as JsonArray;
            var mainContext = contexts?.FirstOrDefault();
            _currentContextId = mainContext?["id"]?.GetValue<int?>();
        }
        catch { }

        return true;
    }

    /// <summary>
    /// Ensure CDP is connected to a tab identified by targetName.
    /// Primary lookup: savedTargetId parameter (from AskTargetRegistry or caller).
    /// Fallback: URL fragment scan, then create new tab.
    /// </summary>
    /// <param name="port">CDP port.</param>
    /// <param name="targetName">Unique tab identifier (e.g. "gemini-a1b2c3d4").</param>
    /// <param name="navigateUrl">URL to navigate after creating a new tab.</param>
    /// <param name="savedTargetId">Previously saved target ID for reuse (from AskTargetRegistry).</param>
    /// <param name="minimizeAfter">If true, minimize Chrome window after setup (for focusless CDP input).</param>
    public async Task<string?> EnsureCorrectWindowAsync(int port, string? targetName = null, string? navigateUrl = null,
        string? savedTargetId = null, bool minimizeAfter = false)
    {
        var (expX, expY, expW, expH) = ExpectedBounds;

        // Step 0: Get all page targets + immediately close any about:blank tabs (waste of resources)
        JsonArray? allTargets = null;
        try
        {
            var json = await _http.GetStringAsync($"http://localhost:{port}/json");
            allTargets = JsonSerializer.Deserialize<JsonArray>(json);
        }
        catch { return TargetId; }
        if (allTargets == null) return TargetId;

        // Sweep: close all about:blank tabs on sight (they serve no purpose and waste tokens)
        int blanksClosed = 0;
        for (int i = allTargets.Count - 1; i >= 0; i--)
        {
            var t = allTargets[i];
            var tUrl = t?["url"]?.GetValue<string>() ?? "";
            var tId = t?["id"]?.GetValue<string>() ?? "";
            if (tUrl == "about:blank" && !string.IsNullOrEmpty(tId))
            {
                try { await _http.GetAsync($"http://localhost:{port}/json/close/{tId}"); } catch { }
                allTargets.RemoveAt(i);
                blanksClosed++;
            }
        }
        if (blanksClosed > 0)
            Console.WriteLine($"[WEB] Closed {blanksClosed} about:blank tab(s) on sight");

        // Step 1: Try saved target ID (from AskTargetRegistry — survives across CLI invocations)
        if (!string.IsNullOrWhiteSpace(savedTargetId))
        {
            foreach (var target in allTargets)
            {
                if (target?["type"]?.GetValue<string>() != "page") continue;
                var tid = target?["id"]?.GetValue<string>();
                if (tid == savedTargetId)
                {
                    // Saved target still alive — check browser window position + tab health
                    if (tid != TargetId)
                        await SwitchToTargetAsync(tid, port);

                    // Position check: log only — saved target tab is always reused regardless of position
                    // (window may have been moved by user, but we still want to reuse this tab)
                    var wb = await GetWindowForTargetAsync(tid);
                    if (wb != null)
                        Console.WriteLine($"[ASK] Window at ({wb.Value.left},{wb.Value.top},{wb.Value.width},{wb.Value.height})");
                    else
                        Console.WriteLine("[ASK] Cannot get window bounds (OK — reusing tab anyway)");

                    if (minimizeAfter)
                        await MinimizeWindowAsync(tid);
                    return tid;
                }
            }
            // Saved target no longer alive or window moved — fall through
        }

        // Step 2: Scan by URL fragment (legacy / backup)
        var fragment = $"#wkbot-{targetName ?? "default"}";
        foreach (var target in allTargets)
        {
            var type = target?["type"]?.GetValue<string>();
            if (type != "page") continue;
            var tid = target?["id"]?.GetValue<string>();
            var url = target?["url"]?.GetValue<string>() ?? "";
            if (tid == null || !url.Contains(fragment, StringComparison.Ordinal)) continue;

            if (tid != TargetId)
                await SwitchToTargetAsync(tid, port);
            if (minimizeAfter)
                await MinimizeWindowAsync(tid);
            return tid;
        }

        // Step 3: Claim untagged tab in correctly-positioned window
        foreach (var target in allTargets)
        {
            var type = target?["type"]?.GetValue<string>();
            if (type != "page") continue;
            var tid = target?["id"]?.GetValue<string>();
            var url = target?["url"]?.GetValue<string>() ?? "";
            if (tid == null) continue;
            if (url.Contains("#wkbot-", StringComparison.Ordinal)) continue;

            // Only claim blank or matching-host tabs
            var isBlank = url.StartsWith("about:", StringComparison.OrdinalIgnoreCase)
                       || url.StartsWith("chrome:", StringComparison.OrdinalIgnoreCase);
            var matchesHost = !string.IsNullOrWhiteSpace(navigateUrl)
                           && url.Contains(new Uri(navigateUrl).Host, StringComparison.OrdinalIgnoreCase);
            if (!isBlank && !matchesHost) continue;

            if (tid != TargetId)
                await SwitchToTargetAsync(tid, port);

            // matchesHost tabs: reuse regardless of window position (already on right site)
            // blank tabs: check window position (only claim blank tabs in expected window)
            if (isBlank && !matchesHost)
            {
                var twb = await GetWindowForTargetAsync(tid);
                if (twb != null && !IsAtExpectedBounds(twb.Value, expX, expY, expW, expH))
                    continue; // blank tab in wrong window — skip

                // Navigate blank to requested URL
                if (!string.IsNullOrWhiteSpace(navigateUrl))
                {
                    try { await NavigateAsync(navigateUrl); }
                    catch { }
                }
            }
            // matchesHost: already on correct site — no need to navigate

            if (minimizeAfter)
                await MinimizeWindowAsync(tid);
            return tid;
        }

        // Step 4: Create new tab — prefer existing correctly-positioned window, else new window
        // IMPORTANT: Never create about:blank tabs — they persist and waste resources.
        // If no navigateUrl, just return whatever tab we're currently on.
        if (string.IsNullOrWhiteSpace(navigateUrl))
        {
            Console.WriteLine("[WEB] No navigateUrl — reusing current tab (avoiding about:blank)");
            return TargetId;
        }

        string? newTargetId = null;
        var createUrl = navigateUrl;

        // First: check if any existing tab lives in a correctly-positioned window
        // If so, create new tab there (reuse window, avoid stacking)
        foreach (var target in allTargets)
        {
            if (target?["type"]?.GetValue<string>() != "page") continue;
            var existingTid = target?["id"]?.GetValue<string>();
            if (existingTid == null) continue;
            try
            {
                // Temporarily switch to check its window bounds
                if (existingTid != TargetId)
                    await SwitchToTargetAsync(existingTid, port);
                var existingWb = await GetWindowForTargetAsync(existingTid);
                if (existingWb != null && IsAtExpectedBounds(existingWb.Value, expX, expY, expW, expH))
                {
                    // Found a correctly-positioned window — create tab here
                    Console.WriteLine($"[ASK] Reusing window at ({existingWb.Value.left},{existingWb.Value.top}) for new tab");
                    var result = await SendAsync("Target.createTarget", new JsonObject { ["url"] = createUrl });
                    newTargetId = result?["targetId"]?.GetValue<string>();
                    break;
                }
            }
            catch { }
        }

        // No correctly-positioned window found — create new window
        if (newTargetId == null)
        {
            try { newTargetId = await CreateTargetInNewWindowAsync(createUrl); }
            catch { }

            if (newTargetId == null)
            {
                try
                {
                    var result = await SendAsync("Target.createTarget", new JsonObject { ["url"] = createUrl });
                    newTargetId = result?["targetId"]?.GetValue<string>();
                }
                catch { }
                if (newTargetId == null) return TargetId;
            }

            // Position new window at expected bounds
            await Task.Delay(300);
            var newWb = await GetWindowForTargetAsync(newTargetId);
            if (newWb != null)
                await SetWindowBoundsAsync(newWb.Value.windowId, expX, expY, expW, expH);
        }

        await Task.Delay(200);
        await SwitchToTargetAsync(newTargetId, port);

        if (minimizeAfter)
            await MinimizeWindowAsync(newTargetId);
        return newTargetId;
    }

    private static bool IsAtExpectedBounds(
        (int windowId, int left, int top, int width, int height) wb,
        int expX, int expY, int expW, int expH) =>
        Math.Abs(wb.left - expX) < BoundsTolerance &&
        Math.Abs(wb.top - expY) < BoundsTolerance &&
        Math.Abs(wb.width - expW) < BoundsTolerance &&
        Math.Abs(wb.height - expH) < BoundsTolerance;

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

    // -- Background receive loop --

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
                    var error = msg.AsObject().ContainsKey("error") ? msg["error"] : null;
                    if (error != null)
                        tcs.TrySetException(new InvalidOperationException(
                            $"CDP error: {error["message"]?.GetValue<string>()}"));
                    else
                        tcs.TrySetResult(msg.AsObject().ContainsKey("result") ? msg["result"] : null);
                }
                else
                {
                    var method = msg["method"]?.GetValue<string>();
                    if (method == "Runtime.executionContextDestroyed")
                        _currentContextId = null;
                    else if (method == "Page.fileChooserOpened")
                        _fileChooserTcs?.TrySetResult(msg["params"]);
                }
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

    public async Task ApplyTargetTagAsync(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag) || _ws == null || _ws.State != WebSocketState.Open)
            return;
        try
        {
            await EvalAsync($"window.__wk_ask_tag = '{tag.Replace("'", "\'")}';");
        }
        catch
        {
            // best effort
        }
    }

}
