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

        // Step 1: Try saved target ID (from AskTargetRegistry -- survives across CLI invocations)
        if (!string.IsNullOrWhiteSpace(savedTargetId))
        {
            foreach (var target in allTargets)
            {
                if (target?["type"]?.GetValue<string>() != "page") continue;
                var tid = target?["id"]?.GetValue<string>();
                if (tid == savedTargetId)
                {
                    // Saved target still alive -- check browser window position + tab health
                    if (tid != TargetId)
                        await SwitchToTargetAsync(tid, port);

                    // Position check: log only -- saved target tab is always reused regardless of position
                    // (window may have been moved by user, but we still want to reuse this tab)
                    var wb = await GetWindowForTargetAsync(tid);
                    if (wb != null)
                        Console.WriteLine($"[ASK] Window at ({wb.Value.left},{wb.Value.top},{wb.Value.width},{wb.Value.height})");
                    else
                        Console.WriteLine("[ASK] Cannot get window bounds (OK -- reusing tab anyway)");

                    Console.WriteLine($"[CDP:TARGET] reused saved target={tid} name={targetName}");
                    if (minimizeAfter)
                        await MinimizeWindowAsync(tid);
                    return tid;
                }
            }
            Console.WriteLine($"[CDP:TARGET] saved target {savedTargetId} not found -- scanning");
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

            Console.WriteLine($"[CDP:TARGET] fragment match target={tid} name={targetName}");
            if (tid != TargetId)
                await SwitchToTargetAsync(tid, port);
            if (minimizeAfter)
                await MinimizeWindowAsync(tid);
            return tid;
        }

        // Step 2b: Exact URL match -- reuse existing tab without navigating (prevents tab duplication)
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

        // Step 2c: Path-similarity match -- same host, >50% path prefix segments match → reuse + navigate
        if (!string.IsNullOrWhiteSpace(navigateUrl) && Uri.TryCreate(navigateUrl, UriKind.Absolute, out var navUri2c))
        {
            foreach (var target in allTargets)
            {
                if (target?["type"]?.GetValue<string>() != "page") continue;
                var tid = target?["id"]?.GetValue<string>();
                var url = target?["url"]?.GetValue<string>() ?? "";
                if (tid == null || url.Contains("#wkbot-", StringComparison.Ordinal)) continue;
                if (!Uri.TryCreate(url, UriKind.Absolute, out var tabUri2c)) continue;
                if (!string.Equals(tabUri2c.Host, navUri2c.Host, StringComparison.OrdinalIgnoreCase)) continue;
                var tabSegs = tabUri2c.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                var navSegs = navUri2c.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                int maxSegs = Math.Max(tabSegs.Length, navSegs.Length);
                if (maxSegs == 0) continue;
                int matching = 0;
                for (int i = 0; i < Math.Min(tabSegs.Length, navSegs.Length); i++)
                {
                    if (string.Equals(tabSegs[i], navSegs[i], StringComparison.OrdinalIgnoreCase)) matching++;
                    else break;
                }
                if ((double)matching / maxSegs > 0.5)
                {
                    Console.WriteLine($"[WEB] Path-similarity reuse tab {tid} ({tabUri2c.AbsolutePath} ~> {navUri2c.AbsolutePath}, {matching}/{maxSegs} segs)");
                    if (tid != TargetId) await SwitchToTargetAsync(tid, port);
                    try { await NavigateAsync(navigateUrl); } catch { }
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
                    continue; // blank tab in wrong window -- skip

                // Navigate blank to requested URL
                if (!string.IsNullOrWhiteSpace(navigateUrl))
                {
                    try { await NavigateAsync(navigateUrl); }
                    catch { }
                }
            }
            // matchesHost: already on correct site -- no need to navigate

            if (minimizeAfter)
                await MinimizeWindowAsync(tid);
            return tid;
        }

        // Step 4: Create new tab -- prefer existing correctly-positioned window, else new window
        // IMPORTANT: Never create about:blank tabs -- they persist and waste resources.
        // If no navigateUrl, just return whatever tab we're currently on.
        if (string.IsNullOrWhiteSpace(navigateUrl))
        {
            Console.WriteLine("[WEB] No navigateUrl -- reusing current tab (avoiding about:blank)");
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
                    // Minimize Chrome BEFORE creating tab -- Chrome may activate window on tab create.
                    // Poll windowState=minimized instead of fixed 100ms delay.
                    MinimizeChrome();
                    await WaitForWindowStateAsync(existingWb.Value.windowId, "minimized", timeoutMs: 1000);
                    Console.WriteLine($"[ASK] Reusing window at ({existingWb.Value.left},{existingWb.Value.top}) for new tab");
                    var beforeTargets = CloneTargets(allTargets);
                    var result = await SendAsync("Target.createTarget", new JsonObject { ["url"] = createUrl });
                    newTargetId = result?["targetId"]?.GetValue<string>();
                    if (!string.IsNullOrWhiteSpace(newTargetId))
                        await DumpTabGrowthAsync(port, "ensure-correct-window-reuse-create", beforeTargets, targetName, navigateUrl, newTargetId);
                    break;
                }
            }
            catch { }
        }

        // No correctly-positioned window found -- create new window
        if (newTargetId == null)
        {
            // Ensure minimized BEFORE tab creation (prevent focus theft)
            await EnsureMinimizedAsync();

            try { newTargetId = await CreateTargetInNewWindowAsync(createUrl); }
            catch { }

            if (newTargetId == null)
            {
                try
                {
                    var beforeTargets = CloneTargets(allTargets);
                    var result = await SendAsync("Target.createTarget", new JsonObject { ["url"] = createUrl });
                    newTargetId = result?["targetId"]?.GetValue<string>();
                    if (!string.IsNullOrWhiteSpace(newTargetId))
                        await DumpTabGrowthAsync(port, "ensure-correct-window-fallback-create", beforeTargets, targetName, navigateUrl, newTargetId);
                }
                catch { }
                if (newTargetId == null) return TargetId;
            }

            // Position new window at expected bounds (restore -> resize -> re-minimize)
            var newWb = await GetWindowForTargetAsync(newTargetId);
            if (newWb != null)
            {
                await SetWindowBoundsAsync(newWb.Value.windowId, expX, expY, expW, expH);
                // Re-minimize after bounds applied (prevent focus theft)
                await MinimizeWindowAsync(newTargetId);
            }
        }

        // Poll /json until new target appears (replaces fixed 200ms delay)
        var targetSw = Stopwatch.StartNew();
        bool targetReady = false;
        while (targetSw.ElapsedMilliseconds < 2000)
        {
            try
            {
                var checkJson = await _http.GetStringAsync($"http://localhost:{port}/json");
                var checkTargets = JsonSerializer.Deserialize<JsonArray>(checkJson);
                if (checkTargets?.Any(t => t?["id"]?.GetValue<string>() == newTargetId) == true)
                {
                    targetReady = true;
                    break;
                }
            }
            catch { }
            await Task.Delay(50);
        }
        if (!targetReady)
            Console.Error.WriteLine($"[CDP:TARGET] warning: new target {newTargetId} not visible in /json after 2s");
        await SwitchToTargetAsync(newTargetId, port);
        Console.WriteLine($"[CDP:TARGET] created new target={newTargetId} name={targetName} url={navigateUrl}");

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
