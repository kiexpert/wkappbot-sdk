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
    /// <summary>
    /// Switch this CdpClient to a different page target (disconnect + reconnect).
    /// The port is needed to look up the new target's WebSocket URL from /json.
    /// skipMinimize: true for read-only actions (read/find/inspect) -- avoids minimizing the browser window.
    /// </summary>
    public async Task<bool> SwitchToTargetAsync(string targetId, int port, bool skipMinimize = false)
    {
        // Minimize Chrome before switching tab -- prevents OS focus fight during tab switch.
        // Chrome processes tab changes internally fine while minimized.
        // Poll windowState=minimized instead of fixed 80ms delay.
        // Skip for read-only operations (no focus steal risk).
        if (!skipMinimize)
        {
            var preWb = await GetWindowForTargetAsync();
            MinimizeChrome();
            if (preWb != null)
                await WaitForWindowStateAsync(preWb.Value.windowId, "minimized", timeoutMs: 1000);
        }

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

        await EnableRuntimeWithRetry();

        try
        {
            var contextInfo = await SendAsync("Runtime.getExecutionContexts");
            var contexts = contextInfo?["result"] as JsonArray;
            var mainContext = contexts?.FirstOrDefault();
            _currentContextId = mainContext?["id"]?.GetValue<int?>();
        }
        catch { }

        // Proactive focusless measures -- best-effort, non-fatal
        await EmulateActiveTabAsync();

        // Restore Chrome to visible after tab switch -- poll windowState=normal instead of fixed 200ms
        RestoreChromeNoActivate();
        var postWb = await GetWindowForTargetAsync(targetId);
        if (postWb != null)
            await WaitForWindowStateAsync(postWb.Value.windowId, "normal", timeoutMs: 1000);

        return true;
    }

    /// <summary>
    /// Emulate active/focused tab without OS SetForegroundWindow.
    /// Applies three layers:
    ///   1. Emulation.setFocusEmulationEnabled -- Chrome accepts input events as if focused
    ///   2. Page.setWebLifecycleState("active") -- unthrottle timers/RAF in background
    ///   3. JS visibility fake -- visibilityState="visible" so page JS doesn't pause streaming
    /// All best-effort; failures are silently ignored.
    /// </summary>
    public async Task EmulateActiveTabAsync()
    {
        try { await SendAsync("Emulation.setFocusEmulationEnabled", new JsonObject { ["enabled"] = true }); }
        catch { }

        try { await SendAsync("Page.setWebLifecycleState", new JsonObject { ["state"] = "active" }); }
        catch { }

        try
        {
            await SendAsync("Runtime.evaluate", new JsonObject
            {
                ["expression"] = """
                    (() => {
                        try {
                            Object.defineProperty(document, 'visibilityState',
                                { value: 'visible', configurable: true, writable: false });
                            Object.defineProperty(document, 'hidden',
                                { value: false, configurable: true, writable: false });
                            document.dispatchEvent(new Event('visibilitychange'));
                        } catch {}
                    })()
                    """,
                ["silent"] = true
            });
        }
        catch { }
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
}
