// ClaudeProxyCommand.cs — Local Anthropic API proxy for Claude Code
// Usage: wkappbot claude-proxy [--port 7070] [--inject-context] [--verbose]
//   Set ANTHROPIC_BASE_URL=http://localhost:7070 in Claude Code settings or env.
//   Proxies all requests to https://api.anthropic.com with optional injection.
//
// Injection features:
//   - Adds WKAppBot system context (session summary, pending tasks) to each request
//   - On context-limit error response: injects handoff note into error message
//   - Streams SSE responses transparently

using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    const string AnthropicApiBase = "https://api.anthropic.com";
    const int ProxyDefaultPort = 7788; // lucky 77 + will's 88

    static int ClaudeProxyCommand(string[] args)
    {
        if (args.Contains("-h") || args.Contains("--help"))
        {
            Console.WriteLine("Usage: wkappbot claude-proxy [--port 7070] [--inject-context] [--verbose]");
            Console.WriteLine();
            Console.WriteLine("  Starts a local Anthropic API proxy on localhost.");
            Console.WriteLine("  Configure Claude Code to use it:");
            Console.WriteLine("    ANTHROPIC_BASE_URL=http://localhost:7070");
            Console.WriteLine("    (set in ~/.claude/settings.json or env)");
            Console.WriteLine();
            Console.WriteLine("  Options:");
            Console.WriteLine("    --port N          Listen port (default: 7070)");
            Console.WriteLine("    --inject-context  Inject WKAppBot session context into system messages");
            Console.WriteLine("    --verbose         Log all requests/responses");
            return 0;
        }

        int port = ProxyDefaultPort;
        bool injectContext = args.Contains("--inject-context");
        bool verbose = args.Contains("--verbose");

        for (int i = 0; i < args.Length - 1; i++)
            if (args[i] == "--port" && int.TryParse(args[i + 1], out var p))
                port = p;

        Console.Error.WriteLine($"[PROXY] Forwarding to: {AnthropicApiBase}");
        Console.Error.WriteLine($"[PROXY] Context injection: {(injectContext ? "ON" : "OFF")}");

        string prefix = "";
        HttpListener? listener = null;
        for (int tryPort = port; tryPort < port + 10; tryPort++)
        {
            prefix = $"http://localhost:{tryPort}/";
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add(prefix);
                listener.Start();
                port = tryPort;
                break;
            }
            catch (Exception ex)
            {
                listener?.Close();
                listener = null;
                var reason = ex.Message.Contains("conflict") || ex.Message.Contains("use") ? "in use" : ex.Message;
                if (tryPort < port + 9)
                    Console.Error.WriteLine($"[PROXY] Port {tryPort} {reason}, trying {tryPort + 1}...");
            }
        }
        if (listener == null)
        {
            Console.Error.WriteLine($"[PROXY] ERROR: no available port in range {port}-{port + 9}. Try --port <other>.");
            return 1;
        }

        Console.Error.WriteLine($"[PROXY] Listening on {prefix}");
        Console.Error.WriteLine($"[PROXY] Set: ANTHROPIC_BASE_URL={prefix}");
        Console.WriteLine(prefix.TrimEnd('/'));

        using var http = new HttpClient();
        http.Timeout = TimeSpan.FromMinutes(10);

        Console.Error.WriteLine($"[PROXY] Ready. Waiting for requests...");

        bool cleanExit = false;
        while (true)
        {
            HttpListenerContext ctx;
            try { ctx = listener.GetContext(); }
            catch (HttpListenerException ex) { Console.Error.WriteLine($"[PROXY] ERROR: listener stopped: {ex.Message}"); break; }
            catch (ObjectDisposedException) { cleanExit = true; break; } // intentional Close()
            catch (Exception ex) { Console.Error.WriteLine($"[PROXY] ERROR: unexpected {ex.GetType().Name}: {ex.Message}"); break; }

            _ = Task.Run(() => HandleProxyRequest(ctx, http, injectContext, verbose));
        }

        listener?.Close();
        Console.Error.WriteLine("[PROXY] Stopped.");
        return cleanExit ? 0 : 1; // non-clean exit → AppBotExit(1) flushes error log
    }

    static async Task HandleProxyRequest(HttpListenerContext ctx, HttpClient http, bool injectContext, bool verbose)
    {
        var req = ctx.Request;
        var resp = ctx.Response;

        try
        {
            var path = req.Url?.PathAndQuery ?? "/";
            var targetUrl = AnthropicApiBase + path;

            if (verbose)
                Console.Error.WriteLine($"[PROXY] {req.HttpMethod} {path}");

            // Read request body
            string? requestBody = null;
            if (req.HasEntityBody)
            {
                using var reader = new StreamReader(req.InputStream, req.ContentEncoding ?? Encoding.UTF8);
                requestBody = await reader.ReadToEndAsync();
            }

            // Inject WKAppBot context into /v1/messages POST
            if (injectContext && req.HttpMethod == "POST" && path.StartsWith("/v1/messages") && requestBody != null)
                requestBody = InjectWKAppBotContext(requestBody, verbose);

            // Build upstream request
            var upstreamReq = new HttpRequestMessage(new HttpMethod(req.HttpMethod), targetUrl);

            // Forward headers (except Host)
            foreach (string? key in req.Headers)
            {
                if (key == null || key.Equals("Host", StringComparison.OrdinalIgnoreCase)
                    || key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)
                    || key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
                    continue;
                try { upstreamReq.Headers.TryAddWithoutValidation(key, req.Headers[key]); } catch { }
            }

            if (requestBody != null)
            {
                var content = new StringContent(requestBody, Encoding.UTF8, req.ContentType ?? "application/json");
                upstreamReq.Content = content;
            }

            // Send to Anthropic
            var upstreamResp = await http.SendAsync(upstreamReq, HttpCompletionOption.ResponseHeadersRead);

            // Copy response status + headers
            resp.StatusCode = (int)upstreamResp.StatusCode;
            foreach (var (key, values) in upstreamResp.Headers)
            {
                if (key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase)) continue;
                try { resp.Headers[key] = string.Join(", ", values); } catch { }
            }
            foreach (var (key, values) in upstreamResp.Content.Headers)
            {
                if (key.Equals("Content-Length", StringComparison.OrdinalIgnoreCase)) continue;
                try { resp.Headers[key] = string.Join(", ", values); } catch { }
            }

            var contentType = upstreamResp.Content.Headers.ContentType?.MediaType ?? "";
            bool isSSE = contentType.Contains("text/event-stream");
            bool isError = (int)upstreamResp.StatusCode >= 400;

            bool isStreaming = requestBody != null && requestBody.Contains("\"stream\":true");

            if (isSSE)
            {
                // Stream SSE — buffer to detect context limit before forwarding
                resp.ContentType = "text/event-stream";
                resp.Headers["Cache-Control"] = "no-cache";

                var upstreamStream = await upstreamResp.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(upstreamStream, Encoding.UTF8);
                using var writer = new StreamWriter(resp.OutputStream, Encoding.UTF8, leaveOpen: true);

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.StartsWith("data:") && (line.Contains("\"context_window_exceeded\"") || line.Contains("\"context_length_exceeded\"")))
                    {
                        if (verbose) Console.Error.WriteLine("[PROXY] Context limit in SSE — generating summary...");
                        var apiKey = req.Headers["x-api-key"] ?? req.Headers["Authorization"]?.Replace("Bearer ", "");
                        var model = ExtractModel(requestBody);
                        var summary = await GenerateHandoffSummaryAsync(http, apiKey, model, requestBody, verbose);
                        await WriteFakeSseResponseAsync(writer, summary);
                        return; // don't forward the error
                    }
                    await writer.WriteLineAsync(line);
                    await writer.FlushAsync();
                }
            }
            else if (isError)
            {
                // Read error body, check for context limit
                var errorBody = await upstreamResp.Content.ReadAsStringAsync();

                if (errorBody.Contains("context_window_exceeded") || errorBody.Contains("context_length_exceeded"))
                {
                    if (verbose) Console.Error.WriteLine("[PROXY] Context limit error — generating summary...");
                    var apiKey = req.Headers["x-api-key"] ?? req.Headers["Authorization"]?.Replace("Bearer ", "");
                    var model = ExtractModel(requestBody);
                    var summary = await GenerateHandoffSummaryAsync(http, apiKey, model, requestBody, verbose);

                    // Return as normal 200 response (SSE or JSON depending on request)
                    resp.StatusCode = 200;
                    if (isStreaming)
                    {
                        resp.ContentType = "text/event-stream";
                        resp.Headers["Cache-Control"] = "no-cache";
                        using var writer = new StreamWriter(resp.OutputStream, Encoding.UTF8, leaveOpen: true);
                        await WriteFakeSseResponseAsync(writer, summary);
                    }
                    else
                    {
                        var fakeJson = BuildFakeJsonResponse(summary, model ?? "claude-proxy");
                        var fakeBytes = Encoding.UTF8.GetBytes(fakeJson);
                        resp.ContentType = "application/json";
                        resp.ContentLength64 = fakeBytes.Length;
                        await resp.OutputStream.WriteAsync(fakeBytes);
                    }
                    return;
                }

                var errorBytes = Encoding.UTF8.GetBytes(errorBody);
                resp.ContentLength64 = errorBytes.Length;
                resp.ContentType = "application/json";
                await resp.OutputStream.WriteAsync(errorBytes);
            }
            else
            {
                // Pass through response body directly
                var bodyStream = await upstreamResp.Content.ReadAsStreamAsync();
                await bodyStream.CopyToAsync(resp.OutputStream);
            }
        }
        catch (Exception ex)
        {
            if (verbose) Console.Error.WriteLine($"[PROXY] Error: {ex.Message}");
            try
            {
                resp.StatusCode = 500;
                var errBytes = Encoding.UTF8.GetBytes($"{{\"error\":\"proxy error: {ex.Message}\"}}");
                resp.ContentLength64 = errBytes.Length;
                await resp.OutputStream.WriteAsync(errBytes);
            }
            catch { }
        }
        finally
        {
            try { resp.OutputStream.Close(); } catch { }
        }
    }

    /// <summary>Inject WKAppBot session context as a system message prefix.</summary>
    static string InjectWKAppBotContext(string requestBody, bool verbose)
    {
        try
        {
            var node = JsonSerializer.Deserialize<JsonNode>(requestBody);
            if (node == null) return requestBody;

            var contextLines = BuildProxyInjectionContext();
            if (string.IsNullOrEmpty(contextLines)) return requestBody;

            var system = node["system"];
            if (system is JsonArray sysArr)
            {
                // Array-style system — prepend a text block
                var newBlock = JsonNode.Parse($"{{\"type\":\"text\",\"text\":{JsonSerializer.Serialize(contextLines)}}}");
                sysArr.Insert(0, newBlock);
            }
            else if (system is JsonValue sysVal && sysVal.GetValueKind() == System.Text.Json.JsonValueKind.String)
            {
                node["system"] = contextLines + "\n\n" + sysVal.GetValue<string>();
            }
            else if (system == null)
            {
                node["system"] = contextLines;
            }

            if (verbose) Console.Error.WriteLine($"[PROXY] Injected {contextLines.Length}ch context");
            return node.ToJsonString();
        }
        catch
        {
            return requestBody;
        }
    }

    /// <summary>Build WKAppBot context string to inject into system messages.</summary>
    static string BuildProxyInjectionContext()
    {
        var sb = new StringBuilder();
        sb.AppendLine("<!-- WKAppBot Proxy Context -->");

        // Session JSONL size / context%
        try
        {
            var jsonlFiles = Directory.GetFiles(
                Path.Combine(DataDir, "sessions"),
                "*.jsonl"
            ).OrderByDescending(File.GetLastWriteTime).Take(1).ToArray();

            if (jsonlFiles.Length > 0)
            {
                var size = new FileInfo(jsonlFiles[0]).Length;
                var sizeMb = size / 1024.0 / 1024.0;
                var pct = sizeMb / 20.0 * 100;
                sb.AppendLine($"[SESSION] ctx={sizeMb:F1}MB ({pct:F0}%) -- handoff at 8MB, urgent at 10MB");
            }
        }
        catch { }

        // Pending suggests count
        try
        {
            var suggestPath = Path.Combine(DataDir, "suggestions.jsonl");
            if (File.Exists(suggestPath))
            {
                int pending = File.ReadLines(suggestPath)
                    .Count(l => l.Contains("\"status\":\"pending\"") || !l.Contains("\"status\""));
                if (pending > 0)
                    sb.AppendLine($"[SUGGEST] {pending} pending item(s) -- run 'wkappbot suggest list'");
            }
        }
        catch { }

        var result = sb.ToString().Trim();
        return result.Length > 50 ? result : ""; // only inject if meaningful
    }

    /// <summary>Inject handoff note into SSE context_window_exceeded data line.</summary>
    static string InjectHandoffIntoSSEData(string sseLine)
    {
        try
        {
            var dataIdx = sseLine.IndexOf('{');
            if (dataIdx < 0) return sseLine;
            var json = sseLine[dataIdx..];
            var node = JsonSerializer.Deserialize<JsonNode>(json);
            if (node == null) return sseLine;

            var errNode = node["error"];
            if (errNode != null)
            {
                var msg = errNode["message"]?.GetValue<string>() ?? "";
                errNode["message"] = msg + "\n\n" + BuildHandoffNote();
                return "data: " + node.ToJsonString();
            }
        }
        catch { }
        return sseLine;
    }

    /// <summary>Inject handoff note into JSON error body.</summary>
    static string InjectHandoffIntoErrorBody(string body)
    {
        try
        {
            var node = JsonSerializer.Deserialize<JsonNode>(body);
            if (node == null) return body;
            var errNode = node["error"];
            if (errNode != null)
            {
                var msg = errNode["message"]?.GetValue<string>() ?? "";
                errNode["message"] = msg + "\n\n" + BuildHandoffNote();
                return node.ToJsonString();
            }
        }
        catch { }
        return body;
    }

    static string BuildHandoffNote()
    {
        var sb = new StringBuilder();
        sb.AppendLine("── WKAppBot Context Limit Handoff ──");
        sb.AppendLine("Run: wkappbot newchat \"<brief summary of current work>\"");

        try
        {
            var suggestPath = Path.Combine(DataDir, "suggestions.jsonl");
            if (File.Exists(suggestPath))
            {
                int pending = File.ReadLines(suggestPath)
                    .Count(l => l.Contains("\"status\":\"pending\"") || !l.Contains("\"status\""));
                if (pending > 0)
                    sb.AppendLine($"Pending suggests: {pending} item(s)");
            }
        }
        catch { }

        return sb.ToString().Trim();
    }

    static string? ExtractModel(string? requestBody)
    {
        if (requestBody == null) return null;
        try
        {
            var node = JsonSerializer.Deserialize<JsonNode>(requestBody);
            return node?["model"]?.GetValue<string>();
        }
        catch { return null; }
    }

    /// <summary>
    /// Call Claude API to summarize the conversation, return as handoff message.
    /// Used when context limit is hit — instead of forwarding the error, return a summary.
    /// </summary>
    static async Task<string> GenerateHandoffSummaryAsync(HttpClient http, string? apiKey, string? model, string? requestBody, bool verbose)
    {
        var fallback = BuildHandoffNote() + "\n\n(Auto-summary unavailable — run wkappbot newchat manually)";
        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(requestBody)) return fallback;

        try
        {
            // Extract conversation history from the original request
            var origNode = JsonSerializer.Deserialize<JsonNode>(requestBody);
            var messages = origNode?["messages"]?.AsArray();
            if (messages == null || messages.Count == 0) return fallback;

            // Build a concise conversation transcript (last 20 turns max to fit summary model)
            var transcript = new StringBuilder();
            var turns = messages.TakeLast(20).ToArray();
            foreach (var msg in turns)
            {
                var role = msg?["role"]?.GetValue<string>() ?? "?";
                var contentNode = msg?["content"];
                string text = "";
                if (contentNode is JsonValue cv) text = cv.GetValue<string>();
                else if (contentNode is JsonArray ca)
                    text = string.Join(" ", ca.Select(b => b?["text"]?.GetValue<string>() ?? "").Where(t => !string.IsNullOrEmpty(t)));
                if (!string.IsNullOrEmpty(text))
                    transcript.AppendLine($"{role.ToUpperInvariant()}: {text[..Math.Min(text.Length, 500)]}");
            }

            var summaryPrompt = $"""
                The following is a conversation that hit the context window limit.
                Please provide a concise handoff summary (3-5 bullet points) covering:
                - What was being worked on
                - Key decisions made
                - Current status / what was just completed
                - What to do next

                Then add: "Run: wkappbot newchat \"<your summary here>\""

                Conversation (last {turns.Length} turns):
                {transcript}
                """;

            var summaryRequest = new
            {
                model = model ?? "claude-haiku-4-5-20251001",
                max_tokens = 1024,
                messages = new[] { new { role = "user", content = summaryPrompt } }
            };

            var summaryJson = JsonSerializer.Serialize(summaryRequest);
            var summaryReq = new HttpRequestMessage(HttpMethod.Post, AnthropicApiBase + "/v1/messages");
            summaryReq.Headers.TryAddWithoutValidation("x-api-key", apiKey);
            summaryReq.Headers.TryAddWithoutValidation("anthropic-version", "2023-06-01");
            summaryReq.Content = new StringContent(summaryJson, Encoding.UTF8, "application/json");

            var summaryResp = await http.SendAsync(summaryReq);
            var summaryBody = await summaryResp.Content.ReadAsStringAsync();

            var summaryNode = JsonSerializer.Deserialize<JsonNode>(summaryBody);
            var summaryText = summaryNode?["content"]?[0]?["text"]?.GetValue<string>();

            if (!string.IsNullOrEmpty(summaryText))
            {
                if (verbose) Console.Error.WriteLine($"[PROXY] Summary generated ({summaryText.Length} chars)");
                return "## Context Limit Reached — Auto-generated Handoff\n\n" + summaryText;
            }
        }
        catch (Exception ex)
        {
            if (verbose) Console.Error.WriteLine($"[PROXY] Summary failed: {ex.Message}");
        }

        return fallback;
    }

    /// <summary>Write a fake SSE response stream with the given text content.</summary>
    static async Task WriteFakeSseResponseAsync(StreamWriter writer, string text)
    {
        var msgId = $"msg_proxy_{DateTime.UtcNow:yyyyMMddHHmmss}";
        // message_start
        await writer.WriteLineAsync($"data: {{\"type\":\"message_start\",\"message\":{{\"id\":\"{msgId}\",\"type\":\"message\",\"role\":\"assistant\",\"content\":[],\"model\":\"claude-proxy\",\"stop_reason\":null,\"usage\":{{\"input_tokens\":0,\"output_tokens\":0}}}}}}");
        await writer.WriteLineAsync();
        // content_block_start
        await writer.WriteLineAsync("data: {\"type\":\"content_block_start\",\"index\":0,\"content_block\":{\"type\":\"text\",\"text\":\"\"}}");
        await writer.WriteLineAsync();
        // content_block_delta (text)
        var escaped = JsonSerializer.Serialize(text);
        await writer.WriteLineAsync($"data: {{\"type\":\"content_block_delta\",\"index\":0,\"delta\":{{\"type\":\"text_delta\",\"text\":{escaped}}}}}");
        await writer.WriteLineAsync();
        // content_block_stop
        await writer.WriteLineAsync("data: {\"type\":\"content_block_stop\",\"index\":0}");
        await writer.WriteLineAsync();
        // message_delta
        await writer.WriteLineAsync("data: {\"type\":\"message_delta\",\"delta\":{\"stop_reason\":\"end_turn\",\"stop_sequence\":null},\"usage\":{\"output_tokens\":100}}");
        await writer.WriteLineAsync();
        // message_stop
        await writer.WriteLineAsync("data: {\"type\":\"message_stop\"}");
        await writer.WriteLineAsync();
        await writer.FlushAsync();
    }

    /// <summary>Build a fake non-streaming JSON response.</summary>
    static string BuildFakeJsonResponse(string text, string model)
    {
        var escaped = JsonSerializer.Serialize(text);
        return $"{{\"id\":\"msg_proxy\",\"type\":\"message\",\"role\":\"assistant\",\"content\":[{{\"type\":\"text\",\"text\":{escaped}}}],\"model\":\"{model}\",\"stop_reason\":\"end_turn\",\"usage\":{{\"input_tokens\":0,\"output_tokens\":0}}}}";
    }
}
