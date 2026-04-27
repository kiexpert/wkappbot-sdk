// CdpClient.Core.cs -- SendAsync, focus-steal guard, navigation, status bar injection
// BUG-AUTO CDP series addressed: CDP error Inspected target navigated during ask,
// CDP Not connected during ask-gemini, WebSocket closed race condition reconnect,
// InvalidOperationException CDP batch A/B, CDP mass disconnection concurrent ask.
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>
    /// Send a CDP command and wait for the response.
    /// </summary>
    public async Task<JsonNode?> SendAsync(string method, JsonObject? parameters = null, int timeoutMs = 10000)
    {
        // Auto-reconnect: if socket is closed/aborted, attempt one self-heal before throwing.
        // Covers the concurrent-tab-switch race where _ws is nulled between check and send.
        if (_ws == null || _ws.State != WebSocketState.Open)
        {
            Console.Error.WriteLine($"[CDP] Not connected for '{method}' — auto-reconnect attempt");
            if (!_hasConnectHistory || !await EnsureConnectedAsync(maxRetries: 2, baseDelayMs: 200))
                throw new InvalidOperationException("Not connected");
        }

        // Auto-magnifier: if Chrome is not foreground and no zoom is active, show one
        if (OnZoomRequired != null && ChromeWindowHandle != 0 && _activeZoom == null)
        {
            var fg = GetForegroundWindow();
            GetWindowThreadProcessId(fg, out int fgPid);
            GetWindowThreadProcessId((IntPtr)ChromeWindowHandle, out int chPid);
            if (fgPid != chPid)
                _activeZoom = OnZoomRequired((IntPtr)ChromeWindowHandle);
        }

        // Focus theft monitoring: capture prevFg just before send
        nint prevFg = 0;
        if (EnableFocusTheftMonitoring && OnFocusTheft != null)
            prevFg = (nint)GetForegroundWindow();

        await MaybeLogFocusRiskBeforeAsync(method, parameters, prevFg);

        // CDP Input.* is delivered via DevTools protocol directly to the renderer --
        // does NOT require Chrome to be foreground or visible. No minimize needed.
        // Runtime.evaluate always guarded: ANY eval can activate Chrome tab and steal fg.
        bool isFocusStealing = IsFocusStealingMethod(method);
        bool weMinimized = false;
        if (isFocusStealing && ChromeWindowHandle != 0
            && !IsIconic((IntPtr)ChromeWindowHandle))
        {
            Console.Error.WriteLine($"[CDP] Pre-minimize for focus-stealing method: {method}");
            ShowWindowNative((IntPtr)ChromeWindowHandle, 8); // SW_SHOWMINNOACTIVE: minimize without activating next window
            ScheduleMinimizeDump($"focus-steal-guard:{method}", (IntPtr)ChromeWindowHandle);
            weMinimized = true;
            // Wait up to 500ms for minimize to complete before sending CDP (prevents RACE)
            for (int wi = 0; wi < 50 && !IsIconic((IntPtr)ChromeWindowHandle); wi++)
                await Task.Delay(10);
        }

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
        try
        {
            await _ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception wsEx) when (wsEx is WebSocketException or ObjectDisposedException or InvalidOperationException)
        {
            // Socket was closed between check and send (concurrent tab-switch race).
            // Clean up pending entry and attempt one reconnect before re-throwing.
            _pending.TryRemove(id, out _);
            Console.Error.WriteLine($"[CDP] WebSocket closed during send '{method}' ({wsEx.GetType().Name}) — reconnect");
            if (_hasConnectHistory && await EnsureConnectedAsync(maxRetries: 2, baseDelayMs: 200))
            {
                // Reconnected — retry the send once
                id = Interlocked.Increment(ref _messageId);
                tcs = new TaskCompletionSource<JsonNode?>();
                _pending[id] = tcs;
                msg["id"] = id;
                bytes = Encoding.UTF8.GetBytes(msg.ToJsonString());
                await _ws!.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            else
            {
                throw new InvalidOperationException($"Not connected: WebSocket closed during '{method}'", wsEx);
            }
        }

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
        catch (InvalidOperationException ex) when (ex.Message.Contains("Inspected target"))
        {
            // Tab navigated or closed while command was in-flight.
            // Attempt transparent reconnect and replay once before surfacing the error.
            _pending.TryRemove(id, out _);
            Console.Error.WriteLine($"[CDP] '{method}' — Inspected target gone, attempting reconnect+replay");
            if (_hasConnectHistory && await EnsureConnectedAsync(maxRetries: 3, baseDelayMs: 300))
            {
                // Re-send the command on the new session
                var id2 = Interlocked.Increment(ref _messageId);
                var tcs2 = new TaskCompletionSource<JsonNode?>();
                _pending[id2] = tcs2;
                var msg2 = new System.Text.Json.Nodes.JsonObject { ["id"] = id2, ["method"] = method };
                if (parameters != null) msg2["params"] = parameters;
                var bytes2 = System.Text.Encoding.UTF8.GetBytes(msg2.ToJsonString());
                await _ws.SendAsync(bytes2, System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                using var cts2 = new CancellationTokenSource(timeoutMs);
                cts2.Token.Register(() => tcs2.TrySetCanceled());
                try { result = await tcs2.Task; }
                catch { _pending.TryRemove(id2, out _); throw; }
            }
            else throw;
        }

        // Focus theft check: only fire when CHROME stole focus (became foreground unexpectedly).
        // User switching to other apps is normal behavior -- never yank focus back.
        if (prevFg != 0 && OnFocusTheft != null)
        {
            var curFg = (nint)GetForegroundWindow();
            if (curFg != prevFg && ChromeWindowHandle != 0)
            {
                // Get Chrome's process ID for comparison
                GetWindowThreadProcessId((IntPtr)curFg, out int curPid);
                GetWindowThreadProcessId((IntPtr)ChromeWindowHandle, out int chromePid);
                if (curPid == chromePid)
                {
                    // Chrome process stole focus -> report + restore user's window
                    await LogFocusRiskAsync("focus-theft", method, parameters, prevFg, curFg, "chrome-became-foreground");
                    OnFocusTheft(method + (OperationContext != null ? $"[{OperationContext}]" : ""), prevFg, curFg);
                    // ForceForegroundWindow: AttachThreadInput trick bypasses Windows foreground lock
                    ForceForegroundWindow((IntPtr)prevFg);
                }
                // else: user switched windows naturally -> do nothing
            }
        }

        // Post-restore: ONLY restore if WE minimized Chrome (not if it was already minimized).
        // Using weMinimized prevents spurious restores when Chrome was already iconic before the call.
        if (weMinimized && ChromeWindowHandle != 0
            && IsIconic((IntPtr)ChromeWindowHandle))
        {
            ShowWindowNative((IntPtr)ChromeWindowHandle, 4); // SW_SHOWNOACTIVATE
            CancelMinimizeDump($"post-restore:{method}");
        }

        return result;
    }

    /// <summary>
    /// Methods that trigger Chrome OS-level focus theft (window activation/restoration).
    /// Input.* is NOT in this list -- CDP sends Input events directly to the renderer.
    /// </summary>
    private static bool IsFocusStealingMethod(string method) => method is
        "Page.bringToFront" or "Target.activateTarget" or "Browser.setWindowBounds" or "Runtime.evaluate";

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
        await SendAsync("Page.enable");
        // Wait for document.readyState === 'complete' (up to 10s)
        await WaitForLoadAsync(10_000);

        // SPA title stabilization: wait for document.title to change from default/stale
        // (SPA sites like Notion update title async after navigation)
        var initialTitle = await EvalAsync("document.title") ?? "";
        if (string.IsNullOrEmpty(initialTitle) || initialTitle.Contains("WKWebBot"))
        {
            for (int i = 0; i < 10; i++) // up to 5s
            {
                await Task.Delay(500);
                var curTitle = await EvalAsync("document.title") ?? "";
                if (!string.IsNullOrEmpty(curTitle) && curTitle != initialTitle && !curTitle.Contains("WKWebBot"))
                    break;
            }
        }

        // Inject WebBot bar and update title
        if (InjectWebBotBar)
            await InjectBarAsync();

        // Force Chrome window title via Win32 (Chrome ignores document.title in some profiles)
        if (InjectWebBotBar)
            await SetWindowTitleAsync();

        // Restore focus to user's previous window (don't steal focus!)
        RestoreFocus(prevForeground);
    }

    /// <summary>Wait for document.readyState === 'complete' (polls every 200ms).</summary>
    public async Task WaitForLoadAsync(int timeoutMs = 10_000)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            try
            {
                var state = await EvalAsync("document.readyState") ?? "";
                if (state.Contains("complete")) return;
            }
            catch { }
            await Task.Delay(200);
        }
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
                    icon.textContent = '●';
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
}
