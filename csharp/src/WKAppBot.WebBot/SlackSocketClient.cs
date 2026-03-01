using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Slack Socket Mode client — receives events via WebSocket, sends messages via HTTP API.
/// Zero external dependencies — same pattern as CdpClient.
///
/// Usage:
///   var slack = new SlackSocketClient();
///   await slack.ConnectAsync(appToken, botToken);
///   slack.OnMessage += (channel, user, text) => { /* handle */ };
///   slack.OnMention += (channel, user, text) => { /* handle */ };
///   await slack.SendAsync(channel, "Hello from WKAppBot!");
///   await slack.DisconnectAsync();
///
/// Protocol:
///   1. POST apps.connections.open (app token) → WebSocket URL
///   2. Connect WebSocket → receive hello
///   3. Events arrive as JSON: { envelope_id, type, payload }
///   4. Acknowledge each event: { envelope_id }
///   5. Send messages via POST chat.postMessage (bot token)
/// </summary>
public sealed class SlackSocketClient : IAsyncDisposable, IDisposable
{
    private ClientWebSocket? _ws;
    private CancellationTokenSource? _receiveCts;
    private Task? _receiveTask;
    private readonly HttpClient _http = new();

    private string? _botToken;
    private string? _appToken;
    private string? _botUserId;  // our bot's user ID (to ignore own messages)
    private int _messageCount;   // total WebSocket messages received (for diagnostics)
    private volatile bool _autoReconnect;  // enables auto-reconnect on disconnect/error
    private DateTime _lastConnectedUtc = DateTime.MinValue;  // for periodic health check
    private int _reconnectCount;  // total reconnections (for diagnostics)

    public bool IsConnected => _ws?.State == WebSocketState.Open;
    public int MessageCount => _messageCount;
    public int ReconnectCount => _reconnectCount;
    public DateTime LastConnectedUtc => _lastConnectedUtc;

    /// <summary>Fired when a message is posted in a subscribed channel.</summary>
    public event Action<SlackMessage>? OnMessage;

    /// <summary>Fired when the bot is @mentioned.</summary>
    public event Action<SlackMessage>? OnMention;

    /// <summary>Fired on any event (raw JSON for extensibility).</summary>
    public event Action<string, JsonNode?>? OnEvent;

    /// <summary>Fired when a Block Kit interactive button is clicked (action_id, user, value, envelope for response_url).</summary>
    public event Action<SlackBlockAction>? OnBlockAction;

    /// <summary>Fired when the bot's own message appears in a thread (for ack cleanup etc.).</summary>
    public event Action<SlackMessage>? OnSelfMessage;

    /// <summary>
    /// Connect to Slack via Socket Mode.
    /// </summary>
    /// <param name="appToken">App-Level Token (xapp-...)</param>
    /// <param name="botToken">Bot User OAuth Token (xoxb-...)</param>
    public async Task ConnectAsync(string appToken, string botToken)
    {
        _appToken = appToken;
        _botToken = botToken;

        // Resolve our bot's user ID (to filter out own messages)
        _botUserId = await GetBotUserIdAsync();
        Console.WriteLine($"[SLACK] Bot user ID: {_botUserId}");

        // Step 1: Get WebSocket URL from apps.connections.open
        var wsUrl = await OpenConnectionAsync();

        // Step 2: Connect WebSocket
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
        Console.WriteLine($"[SLACK] WebSocket state: {_ws.State}");

        // Step 3: Start receive loop
        _receiveCts = new CancellationTokenSource();
        _receiveTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
        _autoReconnect = true;
        _lastConnectedUtc = DateTime.UtcNow;

        Console.WriteLine($"[SLACK] Connected (Socket Mode)");
    }

    /// <summary>Send a text message to a channel.</summary>
    public async Task<bool> SendAsync(string channel, string text)
    {
        var payload = new
        {
            channel,
            text,
            unfurl_links = false,
            unfurl_media = false
        };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _botToken);
        req.Content = content;

        var resp = await _http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonNode>(body);
        var ok = result?["ok"]?.GetValue<bool>() ?? false;

        if (!ok)
            Console.WriteLine($"[SLACK] SendAsync failed: {result?["error"]}");

        return ok;
    }

    /// <summary>Send a rich block message to a channel.</summary>
    public async Task<bool> SendBlocksAsync(string channel, string text, JsonArray blocks)
    {
        var payload = new JsonObject
        {
            ["channel"] = channel,
            ["text"] = text,
            ["blocks"] = blocks
        };
        var json = payload.ToJsonString();
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _botToken);
        req.Content = content;

        var resp = await _http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<JsonNode>(body);
        return result?["ok"]?.GetValue<bool>() ?? false;
    }

    /// <summary>POST apps.connections.open to get WebSocket URL.</summary>
    private async Task<string> OpenConnectionAsync()
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/apps.connections.open");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _appToken);
        req.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

        var resp = await _http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);

        var ok = json?["ok"]?.GetValue<bool>() ?? false;
        if (!ok)
            throw new InvalidOperationException($"apps.connections.open failed: {json?["error"]}");

        var url = json?["url"]?.GetValue<string>()
            ?? throw new InvalidOperationException("No WebSocket URL in response");

        Console.WriteLine($"[SLACK] WebSocket URL obtained (len={url.Length})");
        return url;
    }

    /// <summary>Get our bot's user ID via auth.test.</summary>
    private async Task<string?> GetBotUserIdAsync()
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/auth.test");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _botToken);
        req.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

        var resp = await _http.SendAsync(req);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonNode>(body);
        return json?["user_id"]?.GetValue<string>();
    }

    /// <summary>Background WebSocket receive loop.</summary>
    private async Task ReceiveLoopAsync(CancellationToken ct)
    {
        var buffer = new byte[64 * 1024];  // 64KB buffer
        var messageBuffer = new StringBuilder();
        Console.WriteLine($"[SLACK] Receive loop started (ws.State={_ws?.State})");

        while (!ct.IsCancellationRequested && _ws?.State == WebSocketState.Open)
        {
            try
            {
                var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), ct);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine("[SLACK] WebSocket closed by server");
                    break;
                }

                messageBuffer.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

                if (result.EndOfMessage)
                {
                    var message = messageBuffer.ToString();
                    messageBuffer.Clear();
                    Interlocked.Increment(ref _messageCount);
                    ProcessMessage(message);
                }
            }
            catch (OperationCanceledException) { break; }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"[SLACK] WebSocket error: {ex.Message}");
                break;
            }
        }

        Console.WriteLine($"[SLACK] Receive loop ended (ws.State={_ws?.State}, msgCount={_messageCount})");

        // ── Auto-reconnect: if loop ended unexpectedly, try to reconnect ──
        if (_autoReconnect && !ct.IsCancellationRequested)
        {
            _ = Task.Run(async () =>
            {
                for (int attempt = 1; attempt <= 5; attempt++)
                {
                    var delay = Math.Min(attempt * 3, 15);  // 3s, 6s, 9s, 12s, 15s
                    Console.WriteLine($"[SLACK] Auto-reconnect attempt {attempt}/5 in {delay}s...");
                    await Task.Delay(delay * 1000);
                    if (!_autoReconnect) break;
                    try
                    {
                        await ReconnectAsync();
                        Interlocked.Increment(ref _reconnectCount);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[SLACK] Auto-reconnected OK (attempt {attempt}, total reconnects={_reconnectCount})");
                        Console.ResetColor();
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[SLACK] Auto-reconnect attempt {attempt} failed: {ex.Message}");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[SLACK] Auto-reconnect exhausted — Slack offline until periodic health check");
                Console.ResetColor();
            });
        }
    }

    /// <summary>Process a received WebSocket message.</summary>
    private void ProcessMessage(string rawJson)
    {
        try
        {
            var json = JsonSerializer.Deserialize<JsonNode>(rawJson);
            if (json == null) return;

            var type = json["type"]?.GetValue<string>();

            // Diagnostic: log every received message type (except hello which is already logged)
            if (type != "hello")
            {
                var preview = rawJson.Length > 120 ? rawJson[..120] + "..." : rawJson;
                Console.WriteLine($"[SLACK] WS recv #{_messageCount} type={type}: {preview}");
            }

            switch (type)
            {
                case "hello":
                    Console.WriteLine("[SLACK] Received hello — connection established");
                    break;

                case "disconnect":
                    Console.WriteLine($"[SLACK] Disconnect requested: {json["reason"]} — will auto-reconnect");
                    // Slack sends "disconnect" with reason "refresh_requested" periodically
                    // Must reconnect to maintain event delivery
                    if (_autoReconnect)
                    {
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                await Task.Delay(1000);  // brief delay before reconnect
                                await ReconnectAsync();
                                Interlocked.Increment(ref _reconnectCount);
                                Console.WriteLine($"[SLACK] Reconnected after disconnect (total={_reconnectCount})");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[SLACK] Reconnect after disconnect failed: {ex.Message}");
                                // auto-reconnect in ReceiveLoopAsync will retry
                            }
                        });
                    }
                    break;

                case "events_api":
                    HandleEventsApi(json);
                    break;

                case "interactive":
                    HandleInteractive(json);
                    break;

                default:
                    // Unknown type — acknowledge anyway
                    AcknowledgeEnvelope(json);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SLACK] Error processing message: {ex.Message}");
        }
    }

    /// <summary>Handle events_api envelope.</summary>
    private void HandleEventsApi(JsonNode envelope)
    {
        // Must acknowledge within 3 seconds
        AcknowledgeEnvelope(envelope);

        var payload = envelope["payload"];
        var eventNode = payload?["event"];
        if (eventNode == null) return;

        var eventType = eventNode["type"]?.GetValue<string>();
        var channel = eventNode["channel"]?.GetValue<string>();
        var user = eventNode["user"]?.GetValue<string>();
        var text = eventNode["text"]?.GetValue<string>();
        var ts = eventNode["ts"]?.GetValue<string>();
        var subtype = eventNode["subtype"]?.GetValue<string>();
        var threadTs = eventNode["thread_ts"]?.GetValue<string>();
        var botId = eventNode["bot_id"]?.GetValue<string>();

        // Bot's own messages → fire OnSelfMessage for ack cleanup, then skip normal processing
        if (user == _botUserId || (subtype == "bot_message" && botId != null))
        {
            if (threadTs != null && OnSelfMessage != null)
            {
                OnSelfMessage.Invoke(new SlackMessage
                {
                    Channel = channel ?? "",
                    User = user ?? "",
                    Text = text ?? "",
                    Timestamp = ts ?? "",
                    ThreadTs = threadTs
                });
            }
            return;
        }

        // Skip message subtypes (channel_join, etc.)
        if (subtype != null) return;

        // Fire raw event
        OnEvent?.Invoke(eventType ?? "", eventNode);

        var msg = new SlackMessage
        {
            Channel = channel ?? "",
            User = user ?? "",
            Text = text ?? "",
            Timestamp = ts ?? "",
            EventType = eventType ?? "",
            ThreadTs = threadTs,
            BotId = botId
        };

        switch (eventType)
        {
            case "app_mention":
                Console.WriteLine($"[SLACK] @mention from {user} in {channel}: {text}");
                OnMention?.Invoke(msg);
                break;

            case "message":
                Console.WriteLine($"[SLACK] Message from {user} in {channel}: {text}");
                OnMessage?.Invoke(msg);
                break;
        }
    }

    /// <summary>Handle interactive envelope (Block Kit button clicks, etc.).</summary>
    private void HandleInteractive(JsonNode envelope)
    {
        // Must acknowledge within 3 seconds
        AcknowledgeEnvelope(envelope);

        var payload = envelope["payload"];
        if (payload == null) return;

        var payloadType = payload["type"]?.GetValue<string>();
        if (payloadType != "block_actions") return;

        var user = payload["user"]?["id"]?.GetValue<string>() ?? "";
        var userName = payload["user"]?["username"]?.GetValue<string>() ?? user;
        var channel = payload["channel"]?["id"]?.GetValue<string>() ?? "";
        var messageTs = payload["message"]?["ts"]?.GetValue<string>();
        var responseUrl = payload["response_url"]?.GetValue<string>();

        var actions = payload["actions"]?.AsArray();
        if (actions == null || actions.Count == 0) return;

        foreach (var action in actions)
        {
            if (action == null) continue;
            var actionId = action["action_id"]?.GetValue<string>() ?? "";
            var value = action["value"]?.GetValue<string>() ?? "";

            Console.WriteLine($"[SLACK] Block action: {actionId}={value} from {userName} in {channel}");

            OnBlockAction?.Invoke(new SlackBlockAction
            {
                ActionId = actionId,
                Value = value,
                UserId = user,
                UserName = userName,
                Channel = channel,
                MessageTs = messageTs,
                ResponseUrl = responseUrl
            });
        }
    }

    /// <summary>Acknowledge an envelope (required within 3s).</summary>
    private void AcknowledgeEnvelope(JsonNode envelope)
    {
        var envelopeId = envelope["envelope_id"]?.GetValue<string>();
        if (envelopeId == null) return;

        var ack = JsonSerializer.Serialize(new { envelope_id = envelopeId });
        var bytes = Encoding.UTF8.GetBytes(ack);

        // Fire and forget — must be fast
        _ = _ws?.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    /// <summary>Reconnect by closing current WebSocket and opening a new one.
    /// Preserves event handlers. Use when connection goes stale.</summary>
    public async Task ReconnectAsync()
    {
        Console.WriteLine("[SLACK] Reconnecting...");
        _receiveCts?.Cancel();
        if (_ws?.State == WebSocketState.Open)
        {
            try { await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "reconnect", CancellationToken.None); }
            catch { }
        }
        if (_receiveTask != null)
        {
            try { await _receiveTask; } catch { }
        }
        _ws?.Dispose();

        // Open new connection
        var wsUrl = await OpenConnectionAsync();
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
        _lastConnectedUtc = DateTime.UtcNow;
        Console.WriteLine($"[SLACK] Reconnected (ws.State={_ws.State})");

        // Restart receive loop
        _receiveCts = new CancellationTokenSource();
        _messageCount = 0;
        _receiveTask = Task.Run(() => ReceiveLoopAsync(_receiveCts.Token));
    }

    /// <summary>Disconnect and clean up.</summary>
    public async Task DisconnectAsync()
    {
        _autoReconnect = false;  // prevent auto-reconnect on intentional disconnect
        _receiveCts?.Cancel();

        if (_ws?.State == WebSocketState.Open)
        {
            try
            {
                await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
            }
            catch { }
        }

        if (_receiveTask != null)
        {
            try { await _receiveTask; } catch { }
        }

        _ws?.Dispose();
        _ws = null;
        Console.WriteLine("[SLACK] Disconnected");
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
        _http.Dispose();
    }

    public void Dispose()
    {
        _receiveCts?.Cancel();
        _ws?.Dispose();
        _http.Dispose();
    }
}

/// <summary>Slack message event data.</summary>
public record SlackMessage
{
    public string Channel { get; init; } = "";
    public string User { get; init; } = "";
    public string Text { get; init; } = "";
    public string Timestamp { get; init; } = "";
    public string EventType { get; init; } = "";
    /// <summary>Thread parent timestamp. Present when this message is a thread reply.</summary>
    public string? ThreadTs { get; init; }
    /// <summary>Bot ID. Non-null when the message was sent by a bot (used to filter own messages).</summary>
    public string? BotId { get; init; }
}

/// <summary>Slack Block Kit interactive action (button click, etc.).</summary>
public record SlackBlockAction
{
    public string ActionId { get; init; } = "";
    public string Value { get; init; } = "";
    public string UserId { get; init; } = "";
    public string UserName { get; init; } = "";
    public string Channel { get; init; } = "";
    /// <summary>Timestamp of the message containing the button.</summary>
    public string? MessageTs { get; init; }
    /// <summary>URL for responding to the interaction (can update the original message).</summary>
    public string? ResponseUrl { get; init; }
}
