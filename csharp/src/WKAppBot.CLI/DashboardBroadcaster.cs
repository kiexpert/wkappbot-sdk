using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WKAppBot.CLI;

/// <summary>
/// Singleton broadcaster for the local web dashboard.
/// Ring buffer (500 msgs) + WebSocket fan-out to all connected clients.
/// Dual sink: Slack + Dashboard -- add DashboardBroadcaster.Emit() alongside Slack calls.
/// </summary>
internal static class DashboardBroadcaster
{
    const int MaxHistory = 500;

    public record DashMsg(DateTime Ts, string Type, string Text, string? Username, string? ThreadId);

    static readonly ConcurrentQueue<DashMsg> _ring = new();
    static readonly ConcurrentDictionary<Guid, (WebSocket ws, SemaphoreSlim sem)> _clients = new();
    static int _ringCount;

    /// <summary>Broadcast a message to all connected dashboard clients + enqueue in ring buffer.</summary>
    public static void Emit(string type, string text, string? username = null, string? threadId = null)
    {
        var msg = new DashMsg(DateTime.UtcNow, type, text, username, threadId);

        // Ring buffer: cap at MaxHistory
        _ring.Enqueue(msg);
        if (Interlocked.Increment(ref _ringCount) > MaxHistory)
        {
            _ring.TryDequeue(out _);
            Interlocked.Decrement(ref _ringCount);
        }

        // Fan-out to all WebSocket clients
        var json = SerializeMsg(msg);
        var bytes = Encoding.UTF8.GetBytes(json);
        var segment = new ArraySegment<byte>(bytes);

        foreach (var (id, (ws, sem)) in _clients)
        {
            if (ws.State != WebSocketState.Open)
            {
                _clients.TryRemove(id, out _);
                continue;
            }
            // Fire-and-forget per client (serialized per-connection via SemaphoreSlim)
            _ = Task.Run(async () =>
            {
                try
                {
                    await sem.WaitAsync();
                    try { await ws.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None); }
                    finally { sem.Release(); }
                }
                catch { _clients.TryRemove(id, out _); }
            });
        }
    }

    /// <summary>Register a new WebSocket client. Sends history then subscribes to live stream.</summary>
    public static async Task HandleClientAsync(WebSocket ws, CancellationToken ct)
    {
        var id = Guid.NewGuid();
        var sem = new SemaphoreSlim(1, 1);
        _clients[id] = (ws, sem);

        try
        {
            // Send ring buffer history
            foreach (var msg in _ring.ToArray())
            {
                if (ws.State != WebSocketState.Open) break;
                var json = SerializeMsg(msg);
                var bytes = Encoding.UTF8.GetBytes(json);
                await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, ct);
            }

            // Listen for incoming commands from client
            var buf = new byte[4096];
            while (ws.State == WebSocketState.Open && !ct.IsCancellationRequested)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buf), ct);
                if (result.MessageType == WebSocketMessageType.Close) break;
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var cmd = Encoding.UTF8.GetString(buf, 0, result.Count);
                    Emit("cmd", cmd, "dashboard-user");
                    // TODO: dispatch command to CLI
                }
            }
        }
        catch (WebSocketException) { }
        catch (OperationCanceledException) { }
        finally
        {
            _clients.TryRemove(id, out _);
            if (ws.State == WebSocketState.Open)
                try { await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None); } catch { }
        }
    }

    public static int ClientCount => _clients.Count;
    public static int HistoryCount => _ringCount;

    static string SerializeMsg(DashMsg msg) => JsonSerializer.Serialize(new
    {
        ts = msg.Ts.ToString("o"),
        type = msg.Type,
        text = msg.Text,
        user = msg.Username,
        thread = msg.ThreadId,
    });
}
