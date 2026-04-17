// PipeProtocol.cs -- JSON-RPC models for Named Pipe communication.
// Length-prefixed (4-byte LE) + UTF-8 JSON messages.
// Shared between KiwoomProxy (32-bit server) and WKAppBot.CLI (64-bit client).

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.KiwoomProxy;

/// <summary>JSON-RPC request from CLI -> Proxy.</summary>
public class PipeRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; } = "";

    [JsonPropertyName("params")]
    public object?[]? Params { get; set; }
}

/// <summary>JSON-RPC response from Proxy -> CLI.</summary>
public class PipeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("result")]
    public object? Result { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

/// <summary>JSON-RPC event from Proxy -> CLI (no id, push notification).</summary>
public class PipeEvent
{
    [JsonPropertyName("event")]
    public string EventName { get; set; } = "";

    [JsonPropertyName("params")]
    public object?[]? Params { get; set; }
}

/// <summary>Wire protocol: length-prefixed JSON over NamedPipe.</summary>
public static class PipeWire
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    /// <summary>Write a length-prefixed JSON message to a stream.</summary>
    public static async Task WriteMessageAsync(Stream stream, object message, CancellationToken ct = default)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(message, JsonOpts);
        var lenBytes = BitConverter.GetBytes(json.Length); // 4-byte LE
        await stream.WriteAsync(lenBytes, 0, 4, ct);
        await stream.WriteAsync(json, 0, json.Length, ct);
        await stream.FlushAsync(ct);
    }

    /// <summary>Read a length-prefixed JSON message from a stream. Returns null on EOF.</summary>
    public static async Task<string?> ReadMessageAsync(Stream stream, CancellationToken ct = default)
    {
        var lenBuf = new byte[4];
        int read = await ReadExactAsync(stream, lenBuf, 4, ct);
        if (read < 4) return null; // EOF or disconnected

        int len = BitConverter.ToInt32(lenBuf, 0);
        if (len <= 0 || len > 10 * 1024 * 1024) return null; // sanity: max 10MB

        var msgBuf = new byte[len];
        read = await ReadExactAsync(stream, msgBuf, len, ct);
        if (read < len) return null;

        return Encoding.UTF8.GetString(msgBuf);
    }

    /// <summary>Read exactly N bytes from stream. Returns actual bytes read.</summary>
    private static async Task<int> ReadExactAsync(Stream stream, byte[] buffer, int count, CancellationToken ct)
    {
        int offset = 0;
        while (offset < count)
        {
            int n = await stream.ReadAsync(buffer, offset, count - offset, ct);
            if (n == 0) return offset; // EOF
            offset += n;
        }
        return offset;
    }

    /// <summary>Deserialize a JSON string to a typed object.</summary>
    public static T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, JsonOpts);

    /// <summary>Serialize an object to JSON string.</summary>
    public static string Serialize(object obj) => JsonSerializer.Serialize(obj, JsonOpts);
}
