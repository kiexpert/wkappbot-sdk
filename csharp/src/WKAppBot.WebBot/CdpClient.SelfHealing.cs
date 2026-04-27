using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Self-healing CDP reconnect: survives Chrome restarts by re-discovering the target
/// and re-arming the per-page prompt-pump singletons without losing pending chunks.
///
/// Usage:
///   await cdp.ConnectAsync(9222, preferredTargetTag: "chatgpt");
///   await cdp.ArmPromptPumpAsync("#prompt-textarea"); // tracked automatically
///   // ... Chrome restarts, WebSocket drops ...
///   await cdp.EnsureConnectedAsync(); // reconnect + re-attach + re-arm + re-feed chunks
///
/// Design notes:
///   - Prompt-pump state lives in the browser page (window.__wkAskPump). When the page dies,
///     we cannot preserve it server-side. Instead we track (selector, idleMs, pending chunks)
///     on the C# side and replay Arm + chunk append after a successful reconnect.
///   - Retries use exponential backoff capped at 5 attempts to bound recovery latency.
///   - Target re-discovery falls back in order: saved WebSocketUrl -> saved TargetId ->
///     preferredTargetTag URL match -> first available page target.
/// </summary>
public sealed partial class CdpClient
{
    // -- Reconnect bookkeeping --
    private int _lastPort = 9222;
    private string? _lastPreferredTargetTag;
    private int _lastTabIndex;
    private bool _hasConnectHistory;

    // -- Tracked pumps for self-healing re-arm --
    private sealed class TrackedPump
    {
        public string Selector { get; set; } = "";
        public int IdleMs { get; set; } = 1000;
        public readonly ConcurrentQueue<string> PendingChunks = new();
    }

    private readonly ConcurrentDictionary<string, TrackedPump> _trackedPumps = new();

    /// <summary>
    /// Record connect parameters so EnsureConnectedAsync can recover without caller input.
    /// Called from ConnectAsync right after a successful dial.
    /// </summary>
    internal void RememberConnectTarget(int port, string? preferredTargetTag, int tabIndex)
    {
        _lastPort = port;
        _lastPreferredTargetTag = preferredTargetTag;
        _lastTabIndex = Math.Max(0, tabIndex);
        _hasConnectHistory = true;
    }

    /// <summary>
    /// Register an armed prompt pump for self-healing re-arm.
    /// Call this right after a successful ArmPromptPumpAsync -- already wired there.
    /// </summary>
    public void TrackPromptPump(string selector, int idleMs)
    {
        if (string.IsNullOrEmpty(selector)) return;
        _trackedPumps.AddOrUpdate(
            selector,
            _ => new TrackedPump { Selector = selector, IdleMs = idleMs },
            (_, existing) => { existing.IdleMs = idleMs; return existing; });
    }

    /// <summary>Stop tracking a pump (e.g. after explicit CancelPromptPumpAsync).</summary>
    public void UntrackPromptPump(string selector)
    {
        if (string.IsNullOrEmpty(selector)) return;
        _trackedPumps.TryRemove(selector, out _);
    }

    /// <summary>
    /// Record a pending chunk so it can be replayed if the page is lost before the pump
    /// sends. Callers that stream chunks should enqueue here AND append to the editor.
    /// After a successful send the queue is drained via <see cref="MarkPumpChunksFlushed"/>.
    /// </summary>
    public void RecordPendingChunk(string selector, string chunk)
    {
        if (string.IsNullOrEmpty(selector) || string.IsNullOrEmpty(chunk)) return;
        if (_trackedPumps.TryGetValue(selector, out var pump))
            pump.PendingChunks.Enqueue(chunk);
    }

    /// <summary>Clear queued chunks -- call after the pump successfully dispatched.</summary>
    public void MarkPumpChunksFlushed(string selector)
    {
        if (_trackedPumps.TryGetValue(selector, out var pump))
            while (pump.PendingChunks.TryDequeue(out _)) { }
    }

    /// <summary>
    /// Health probe: returns false when the socket is closed/aborted, or when a lightweight
    /// Runtime.evaluate round-trip fails. Cheap enough to call before sensitive operations.
    /// </summary>
    public async Task<bool> IsHealthyAsync(int timeoutMs = 2000)
    {
        if (_ws == null || _ws.State != WebSocketState.Open) return false;
        try
        {
            await SendAsync("Runtime.evaluate",
                new JsonObject { ["expression"] = "1", ["returnByValue"] = true },
                timeoutMs);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Reconnect the CDP websocket to Chrome with retry+backoff, re-discovering the page
    /// target when the saved WebSocketUrl is stale (typical after Chrome restart).
    /// Re-arms every tracked prompt pump and replays pending chunks so the singleton state
    /// appears intact to the caller.
    ///
    /// Returns true on success, false after maxRetries exhausted. Never throws.
    /// </summary>
    public async Task<bool> EnsureConnectedAsync(int maxRetries = 5, int baseDelayMs = 250)
    {
        if (!_hasConnectHistory)
        {
            Console.Error.WriteLine("[CDP:HEAL] No connect history -- cannot self-heal. Call ConnectAsync first.");
            return false;
        }

        if (await IsHealthyAsync()) return true;

        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            var delay = baseDelayMs * (1 << attempt); // 250, 500, 1000, 2000, 4000 ms
            try
            {
                Console.Error.WriteLine($"[CDP:HEAL] attempt {attempt + 1}/{maxRetries} port={_lastPort} tag={_lastPreferredTargetTag ?? "?"}");

                // Strategy 1: try saved WebSocketUrl if we have one.
                bool reconnected = false;
                if (!string.IsNullOrEmpty(WebSocketUrl))
                {
                    try
                    {
                        await ReconnectAsync(timeoutMs: 5000);
                        reconnected = true;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[CDP:HEAL] saved WebSocketUrl failed: {ex.Message}");
                    }
                }

                // Strategy 2: full re-discovery via ConnectAsync (new target IDs after Chrome restart).
                if (!reconnected)
                {
                    await DisposeWebSocketQuietAsync();
                    await ConnectAsync(_lastPort, _lastTabIndex, timeoutMs: 10_000, _lastPreferredTargetTag);
                    reconnected = true;
                }

                if (reconnected && await IsHealthyAsync())
                {
                    await RestoreTrackedPumpsAsync();
                    Console.Error.WriteLine($"[CDP:HEAL] recovered on attempt {attempt + 1}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CDP:HEAL] attempt {attempt + 1} failed: {ex.Message}");
            }

            if (attempt < maxRetries - 1)
                await Task.Delay(delay);
        }

        Console.Error.WriteLine($"[CDP:HEAL] giving up after {maxRetries} attempts");
        return false;
    }

    /// <summary>Close and dispose the current WebSocket without throwing.</summary>
    private async Task DisposeWebSocketQuietAsync()
    {
        try { _receiveCts?.Cancel(); } catch { }
        try
        {
            if (_ws?.State == WebSocketState.Open)
            {
                using var cts = new CancellationTokenSource(1000);
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "heal", cts.Token);
            }
        }
        catch { }
        try { _ws?.Dispose(); } catch { }
        _ws = null;
        if (_receiveTask != null)
        {
            try { await _receiveTask.WaitAsync(TimeSpan.FromSeconds(2)); } catch { }
        }
        // Fail any in-flight commands -- caller should retry after reconnect.
        foreach (var kv in _pending)
        {
            kv.Value.TrySetException(new InvalidOperationException("CDP disconnected during self-heal"));
        }
        _pending.Clear();
    }

    /// <summary>
    /// Re-arm every tracked pump and replay pending chunks into the editor.
    /// Errors per-pump are swallowed -- other pumps should still recover.
    /// </summary>
    private async Task RestoreTrackedPumpsAsync()
    {
        foreach (var kv in _trackedPumps)
        {
            var pump = kv.Value;
            try
            {
                var armed = await ArmPromptPumpAsync(pump.Selector, pump.IdleMs);
                if (!armed)
                {
                    Console.Error.WriteLine($"[CDP:HEAL] re-arm returned false for selector={pump.Selector}");
                    continue;
                }

                // Replay any chunks that were queued while disconnected.
                var replayed = 0;
                while (pump.PendingChunks.TryDequeue(out var chunk))
                {
                    try
                    {
                        await InsertContentEditableAsync(pump.Selector, chunk);
                        replayed++;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[CDP:HEAL] replay chunk failed ({ex.Message}) -- requeuing");
                        pump.PendingChunks.Enqueue(chunk);
                        break;
                    }
                }
                if (replayed > 0)
                    Console.Error.WriteLine($"[CDP:HEAL] re-armed {pump.Selector}, replayed {replayed} chunk(s)");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CDP:HEAL] restore pump {pump.Selector} failed: {ex.Message}");
            }
        }
    }
}
