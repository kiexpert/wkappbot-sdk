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
        // Minimize Chrome before switching tab — prevents OS focus fight during tab switch.
        // Chrome processes tab changes internally fine while minimized.
        MinimizeChrome();
        await Task.Delay(80);

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

        // Proactive focusless measures — best-effort, non-fatal
        await EmulateActiveTabAsync();

        // Restore Chrome to visible (non-minimized) after tab switch is complete
        // Delay to ensure Chrome has fully settled the internal tab change
        await Task.Delay(200);
        RestoreChromeNoActivate();

        return true;
    }

    /// <summary>
    /// Emulate active/focused tab without OS SetForegroundWindow.
    /// Applies three layers:
    ///   1. Emulation.setFocusEmulationEnabled — Chrome accepts input events as if focused
    ///   2. Page.setWebLifecycleState("active") — unthrottle timers/RAF in background
    ///   3. JS visibility fake — visibilityState="visible" so page JS doesn't pause streaming
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

        // Step 2b: Exact URL match — reuse existing tab without navigating (prevents tab duplication)
        if (!string.IsNullOrWhiteSpace(navigateUrl))
        {
            foreach (var target in allTargets)
            {
                if (target?["type"]?.GetValue<string>() != "page") continue;
                var tid = target?["id"]?.GetValue<string>();
                var url = target?["url"]?.GetValue<string>() ?? "";
                if (tid == null || url.Contains("#wkbot-", StringComparison.Ordinal)) continue;
                if (string.Equals(url, navigateUrl, StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith(navigateUrl.TrimEnd('/'), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"[WEB] Reusing exact-URL tab {tid} for {navigateUrl}");
                    if (tid != TargetId) await SwitchToTargetAsync(tid, port);
                    if (minimizeAfter) await MinimizeWindowAsync(tid);
                    return tid;
                }
            }
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
                    // Minimize Chrome BEFORE creating tab — Chrome may activate window on tab create.
                    // SwitchToTargetAsync also minimizes; pre-minimize here closes the gap window.
                    MinimizeChrome();
                    await Task.Delay(100);
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

            // Minimize new window immediately after creation to return focus to user
            await Task.Delay(300);
            await MinimizeWindowAsync(newTargetId);

            // Position new window at expected bounds (while minimized — takes effect on restore)
            var newWb = await GetWindowForTargetAsync(newTargetId);
            if (newWb != null)
                await SetWindowBoundsAsync(newWb.Value.windowId, expX, expY, expW, expH);
        }

        await Task.Delay(200);
        await SwitchToTargetAsync(newTargetId, port);

        return newTargetId;
    }

    private static bool IsAtExpectedBounds(
        (int windowId, int left, int top, int width, int height) wb,
        int expX, int expY, int expW, int expH) =>
        Math.Abs(wb.left - expX) < BoundsTolerance &&
        Math.Abs(wb.top - expY) < BoundsTolerance &&
        Math.Abs(wb.width - expW) < BoundsTolerance &&
        Math.Abs(wb.height - expH) < BoundsTolerance;
}
