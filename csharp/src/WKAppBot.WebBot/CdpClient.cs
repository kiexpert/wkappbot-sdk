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
public sealed partial class CdpClient : IAsyncDisposable, IDisposable
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
