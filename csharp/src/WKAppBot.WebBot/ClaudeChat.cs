using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Lightweight Claude API chat client -- text only, no vision.
/// Zero external dependencies. Uses ANTHROPIC_API_KEY env var.
///
/// Usage:
///   var chat = new ClaudeChat();
///   var reply = await chat.AskAsync("What is 2+2?");
///   // reply = "2 + 2 = 4"
///
/// Conversation history is maintained per instance for multi-turn chat.
/// </summary>
public sealed class ClaudeChat : IDisposable
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly string _systemPrompt;
    private readonly HttpClient _http;
    private readonly List<ChatMessage> _history = new();
    private readonly int _maxHistoryTurns;

    private const string ApiUrl = "https://api.anthropic.com/v1/messages";

    public ClaudeChat(
        string? apiKey = null,
        string model = "claude-haiku-4-20250514",
        string? systemPrompt = null,
        int maxHistoryTurns = 10)
    {
        _apiKey = apiKey ?? Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
            ?? throw new InvalidOperationException(
                "ANTHROPIC_API_KEY not set. Provide via constructor or environment variable.");
        _model = model;
        _maxHistoryTurns = maxHistoryTurns;
        _systemPrompt = systemPrompt ?? @"You are WKAppBot, a helpful AI assistant running on a Windows desktop.
You respond concisely in the same language as the user's message.
Keep responses under 300 characters for Slack readability.
If asked about your capabilities, mention: Windows app automation, HTS trading, chart analysis, OCR, and Slack messaging.";

        _http = new HttpClient();
        _http.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
        _http.Timeout = TimeSpan.FromSeconds(30);
    }

    /// <summary>Send a message and get Claude's response.</summary>
    public async Task<string> AskAsync(string userMessage, CancellationToken ct = default)
    {
        // Add user message to history
        _history.Add(new ChatMessage("user", userMessage));

        // Trim history if too long (keep recent turns)
        while (_history.Count > _maxHistoryTurns * 2)
            _history.RemoveAt(0);

        // Build messages array
        var messages = _history.Select(m => new { role = m.Role, content = m.Content }).ToArray();

        var requestBody = new JsonObject
        {
            ["model"] = _model,
            ["max_tokens"] = 512,
            ["system"] = _systemPrompt,
            ["messages"] = JsonSerializer.SerializeToNode(messages)
        };

        var json = requestBody.ToJsonString();
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var resp = await _http.PostAsync(ApiUrl, content, ct);
            var body = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                Console.WriteLine($"[AI] API error {resp.StatusCode}: {body}");
                _history.RemoveAt(_history.Count - 1); // rollback user msg
                return $"(API error: {resp.StatusCode})";
            }

            var result = JsonSerializer.Deserialize<JsonNode>(body);
            var reply = result?["content"]?[0]?["text"]?.GetValue<string>() ?? "(empty response)";

            // Add assistant response to history
            _history.Add(new ChatMessage("assistant", reply));

            return reply;
        }
        catch (TaskCanceledException)
        {
            _history.RemoveAt(_history.Count - 1);
            return "(timeout)";
        }
        catch (Exception ex)
        {
            _history.RemoveAt(_history.Count - 1);
            Console.WriteLine($"[AI] Error: {ex.Message}");
            return $"(error: {ex.Message})";
        }
    }

    /// <summary>Clear conversation history.</summary>
    public void ClearHistory() => _history.Clear();

    /// <summary>Number of messages in history.</summary>
    public int HistoryCount => _history.Count;

    public void Dispose() => _http.Dispose();

    private record ChatMessage(string Role, string Content);
}
