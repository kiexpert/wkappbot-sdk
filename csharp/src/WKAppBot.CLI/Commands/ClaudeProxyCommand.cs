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
    const int ProxyDefaultPort = 7070;

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
            catch
            {
                listener?.Close();
                listener = null;
                if (tryPort < port + 9)
                    Console.Error.WriteLine($"[PROXY] Port {tryPort} in use, trying {tryPort + 1}...");
            }
        }
        if (listener == null)
        {
            Console.Error.WriteLine($"[PROXY] No available port in range {port}-{port + 9}. Try --port <other>.");
            return 1;
        }

        Console.Error.WriteLine($"[PROXY] Listening on {prefix}");
        Console.Error.WriteLine($"[PROXY] Set: ANTHROPIC_BASE_URL={prefix}");
        Console.WriteLine(prefix.TrimEnd('/'));

        using var http = new HttpClient();
        http.Timeout = TimeSpan.FromMinutes(10);

        Console.Error.WriteLine($"[PROXY] Ready. Waiting for requests...");

        while (true)
        {
            HttpListenerContext ctx;
            try { ctx = listener.GetContext(); }
            catch (HttpListenerException) { break; }
            catch (ObjectDisposedException) { break; }

            _ = Task.Run(() => HandleProxyRequest(ctx, http, injectContext, verbose));
        }

        listener?.Close();
        return 0;
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

            if (isSSE)
            {
                // Stream SSE — check for context limit events inline
                resp.ContentType = "text/event-stream";
                resp.Headers["Cache-Control"] = "no-cache";

                var upstreamStream = await upstreamResp.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(upstreamStream, Encoding.UTF8);
                using var writer = new StreamWriter(resp.OutputStream, Encoding.UTF8, leaveOpen: true);

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    // Detect context limit in SSE data
                    if (line.StartsWith("data:") && line.Contains("\"context_window_exceeded\""))
                    {
                        var injected = InjectHandoffIntoSSEData(line);
                        await writer.WriteLineAsync(injected);
                        if (verbose) Console.Error.WriteLine("[PROXY] Context limit detected — handoff injected");
                    }
                    else
                    {
                        await writer.WriteLineAsync(line);
                    }
                    await writer.FlushAsync();
                }
            }
            else if (isError)
            {
                // Read error body, check for context limit
                var errorBody = await upstreamResp.Content.ReadAsStringAsync();

                if (errorBody.Contains("context_window_exceeded") || errorBody.Contains("context_length_exceeded"))
                {
                    errorBody = InjectHandoffIntoErrorBody(errorBody);
                    if (verbose) Console.Error.WriteLine("[PROXY] Context limit error — handoff injected");
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

        // Pending suggests
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
}
