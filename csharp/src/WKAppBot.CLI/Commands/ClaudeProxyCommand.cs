// ClaudeProxyCommand.cs -- Local Anthropic API proxy for Claude Code
// Usage: wkappbot claude-proxy [--port 7788] [--inject-context] [--verbose]
//   Set ANTHROPIC_BASE_URL=http://localhost:7788 in Claude Code settings or env.
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
            Console.WriteLine("Usage: wkappbot claude-proxy [--port 7788] [--inject-context] [--verbose]");
            Console.WriteLine();
            Console.WriteLine("  Starts a local Anthropic API proxy on localhost.");
            Console.WriteLine("  Configure Claude Code to use it:");
            Console.WriteLine("    ANTHROPIC_BASE_URL=http://localhost:7788");
            Console.WriteLine("    (set in ~/.claude/settings.json or env)");
            Console.WriteLine();
            Console.WriteLine("  Options:");
            Console.WriteLine("    --port N          Listen port (default: 7788)");
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
        return cleanExit ? 0 : 1; // non-clean exit -> AppBotExit(1) flushes error log
    }

    static async Task HandleProxyRequest(HttpListenerContext ctx, HttpClient http, bool injectContext, bool verbose)
    {
        var req = ctx.Request;
        var resp = ctx.Response;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            var path = req.Url?.PathAndQuery ?? "/";
            var targetUrl = AnthropicApiBase + path;

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
                // Stream SSE -- buffer to detect context limit before forwarding
                resp.ContentType = "text/event-stream";
                resp.Headers["Cache-Control"] = "no-cache";
                Console.Error.WriteLine($"[PROXY] <- 200 SSE stream started ({sw.ElapsedMilliseconds}ms)");

                var upstreamStream = await upstreamResp.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(upstreamStream, Encoding.UTF8);
                using var writer = new StreamWriter(resp.OutputStream, Encoding.UTF8, leaveOpen: true);

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.StartsWith("data:") && IsAnthropicLimitError(line))
                    {
                        Console.Error.WriteLine("[PROXY] Limit hit in SSE -- handing off to Gemini agent...");
                        var handoffMsg = await SpawnGeminiAgentHandoffAsync(requestBody, verbose);
                        await WriteFakeSseResponseAsync(writer, handoffMsg);
                        return;
                    }
                    await writer.WriteLineAsync(line);
                    await writer.FlushAsync();
                }
                Console.Error.WriteLine($"[PROXY] <- SSE done ({sw.ElapsedMilliseconds}ms)");
            }
            else if (isError)
            {
                var errorBody = await upstreamResp.Content.ReadAsStringAsync();

                if (IsAnthropicLimitError(errorBody))
                {
                    Console.Error.WriteLine($"[PROXY] Limit hit ({(int)upstreamResp.StatusCode}) -- handing off to Gemini agent...");
                    var handoffMsg = await SpawnGeminiAgentHandoffAsync(requestBody, verbose);

                    resp.StatusCode = 200;
                    if (isStreaming)
                    {
                        resp.ContentType = "text/event-stream";
                        resp.Headers["Cache-Control"] = "no-cache";
                        using var writer = new StreamWriter(resp.OutputStream, Encoding.UTF8, leaveOpen: true);
                        await WriteFakeSseResponseAsync(writer, handoffMsg);
                    }
                    else
                    {
                        var fakeJson = BuildFakeJsonResponse(handoffMsg, ExtractModel(requestBody) ?? "claude-proxy");
                        var fakeBytes = Encoding.UTF8.GetBytes(fakeJson);
                        resp.ContentType = "application/json";
                        resp.ContentLength64 = fakeBytes.Length;
                        await resp.OutputStream.WriteAsync(fakeBytes);
                    }
                    return;
                }

                Console.Error.WriteLine($"[PROXY] <- {(int)upstreamResp.StatusCode} error ({sw.ElapsedMilliseconds}ms)");
                if (verbose) Console.Error.WriteLine($"[PROXY] error body: {errorBody[..Math.Min(200, errorBody.Length)]}");
                var errorBytes = Encoding.UTF8.GetBytes(errorBody);
                resp.ContentLength64 = errorBytes.Length;
                resp.ContentType = "application/json";
                await resp.OutputStream.WriteAsync(errorBytes);
            }
            else
            {
                Console.Error.WriteLine($"[PROXY] <- {(int)upstreamResp.StatusCode} ({sw.ElapsedMilliseconds}ms)");
                // Pass through response body directly
                var bodyStream = await upstreamResp.Content.ReadAsStreamAsync();
                await bodyStream.CopyToAsync(resp.OutputStream);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PROXY] ERROR: {ex.Message}");
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
                // Array-style system -- prepend a text block
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
        sb.AppendLine("-- WKAppBot Context Limit Handoff --");
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

    static bool IsAnthropicLimitError(string text) =>
        text.Contains("context_window_exceeded") || text.Contains("context_length_exceeded") ||
        text.Contains("overloaded_error") || text.Contains("rate_limit_exceeded") ||
        text.Contains("\"529\"") || text.Contains("\"529 \"");

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
    /// Spawn wkappbot agent gemini --new-session with CLAUDE.md + MEMORY.md + last user prompt.
    /// No Anthropic API calls -- works even when weekly limit is hit.
    /// Returns a status message to show in Claude Code while Gemini agent starts.
    /// </summary>
    static async Task<string> SpawnGeminiAgentHandoffAsync(string? requestBody, bool verbose)
    {
        try
        {
            // Extract last user prompt from the failed request
            var lastPrompt = "";
            try
            {
                var node = JsonSerializer.Deserialize<JsonNode>(requestBody ?? "{}");
                var messages = node?["messages"]?.AsArray();
                if (messages != null)
                {
                    var lastUser = messages.LastOrDefault(m => m?["role"]?.GetValue<string>() == "user");
                    var content = lastUser?["content"];
                    if (content is JsonValue cv) lastPrompt = cv.GetValue<string>();
                    else if (content is JsonArray ca)
                        lastPrompt = string.Join("\n", ca
                            .Select(b => b?["text"]?.GetValue<string>() ?? "")
                            .Where(t => !string.IsNullOrEmpty(t)));
                }
            }
            catch { }

            // Build context: CLAUDE.md + MEMORY.md + last prompt -> temp file -> agent gemini --file
            var claudeMdPath = Path.Combine(AppContext.BaseDirectory, "../../../../../../CLAUDE.md");  // project CLAUDE.md
            var globalClaudeMd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "CLAUDE.md");
            var memoryMd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects", "w--GitHub-WKAppBot", "memory", "MEMORY.md");

            // Write combined context to temp file
            var tempCtx = Path.Combine(Path.GetTempPath(), $"wkappbot-proxy-handoff-{DateTime.Now:yyyyMMddHHmmss}.md");
            var sb = new StringBuilder();
            sb.AppendLine("# WKAppBot Proxy Handoff Context");
            sb.AppendLine($"*Handed off from Claude Code at {DateTime.Now:yyyy-MM-dd HH:mm}*");
            sb.AppendLine();
            foreach (var f in new[] { globalClaudeMd, claudeMdPath, memoryMd })
            {
                var resolved = Path.GetFullPath(f);
                if (File.Exists(resolved))
                {
                    sb.AppendLine($"## {Path.GetFileName(resolved)}");
                    sb.AppendLine(File.ReadAllText(resolved));
                    sb.AppendLine();
                }
            }
            sb.AppendLine("## Original Prompt");
            sb.AppendLine(lastPrompt);
            File.WriteAllText(tempCtx, sb.ToString(), Encoding.UTF8);

            // Spawn agent gemini --new-session in background (non-blocking)
            var wkappbot = Path.Combine(AppContext.BaseDirectory, "wkappbot.exe");
            if (!File.Exists(wkappbot)) wkappbot = "wkappbot";
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = wkappbot,
                Arguments = $"agent gemini --new-session --file \"{tempCtx}\" \"{lastPrompt.Replace("\"", "'")}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            System.Diagnostics.Process.Start(psi);

            if (verbose) Console.Error.WriteLine($"[PROXY] Gemini agent spawned with {sb.Length}ch context");

            return $"""
                ## Anthropic Limit Reached -- Handed off to Gemini Agent

                Your session has been forwarded to `wkappbot agent gemini --new-session`.
                Context included: CLAUDE.md + MEMORY.md + your prompt.

                Gemini is starting up -- check the Gemini browser tab or Slack for the response.
                Subsequent messages here will be relayed to the Gemini session via `--interrupt`.

                *(Prompt: {lastPrompt[..Math.Min(lastPrompt.Length, 100)]}...)*
                """;
        }
        catch (Exception ex)
        {
            return $"## Anthropic Limit Reached\n\nAuto-handoff failed: {ex.Message}\n\nManual: `wkappbot agent gemini --new-session`";
        }
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
