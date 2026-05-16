// CDP WebSocket helpers used by Stage 2 page-load wait.
//
// Split out of MyCdpContext.Stage23.cs (2026-05-16). The Stage 2 high-level
// flow lives in Stage23.cs; the low-level CDP transport details live here.
//
// Public surface (within partial class Program):
//   ResolveActivePageWsUrl(cdpPort, ct)
//       -> HTTP GET http://127.0.0.1:<port>/json/list, return the
//          webSocketDebuggerUrl of the first page-type tab (or null).
//   WaitForPageLoadEventFired(wsUrl, ct)
//       -> Open the given CDP WS, enable the Page domain, then poll for
//          Page.loadEventFired. Returns a short trigger code string used
//          by Stage 2 to classify the outcome.

using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// HTTP GET http://127.0.0.1:&lt;port&gt;/json/list and return the
    /// webSocketDebuggerUrl of the first page (type=="page") tab. Returns
    /// null on any error so the caller can fall back to the timeout path.
    /// </summary>
    static async Task<string?> ResolveActivePageWsUrl(int cdpPort, CancellationToken ct)
    {
        try
        {
            using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var url = $"http://127.0.0.1:{cdpPort}/json/list";
            var resp = await http.GetAsync(url, ct).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP /json/list returned {(int)resp.StatusCode}");
                return null;
            }
            var body = await resp.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            using var doc = JsonDocument.Parse(body);
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                if (el.TryGetProperty("type", out var typeProp)
                    && typeProp.GetString() == "page"
                    && el.TryGetProperty("webSocketDebuggerUrl", out var wsProp))
                {
                    return wsProp.GetString();
                }
            }
            Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP /json/list returned no page tab");
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("[PLACEMENT:STAGE2] CDP /json/list timeout");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP /json/list error: {ex.GetType().Name}: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Open the given CDP WebSocket, enable Page domain, then wait for the
    /// next Page.loadEventFired event. Returns a short trigger code:
    ///   "page_load_event" -- the event fired
    ///   "ws_closed"       -- socket closed before any event
    ///   "timeout"         -- ct fired first (caller-imposed budget)
    ///   "ws_open_failed"  -- could not connect
    /// </summary>
    static async Task<string> WaitForPageLoadEventFired(string wsUrl, CancellationToken ct)
    {
        ClientWebSocket? ws = null;
        try
        {
            ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri(wsUrl), ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP ws connect failed: {ex.GetType().Name}: {ex.Message}");
                return "ws_open_failed";
            }

            // Enable Page domain so Page.* events get delivered. Some Chrome
            // versions don't deliver events without explicit enable.
            var enableMsg = "{\"id\":1,\"method\":\"Page.enable\"}";
            var enableBytes = Encoding.UTF8.GetBytes(enableMsg);
            try
            {
                await ws.SendAsync(enableBytes, WebSocketMessageType.Text, true, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP Page.enable send failed: {ex.GetType().Name}: {ex.Message}");
                return "cdp_enable_failed";
            }

            // Read frames until Page.loadEventFired, ws close, or cancel.
            var buf = new byte[8192];
            var sb = new StringBuilder();
            var frameCount = 0;
            while (!ct.IsCancellationRequested && ws.State == WebSocketState.Open)
            {
                sb.Clear();
                WebSocketReceiveResult res;
                try
                {
                    res = await ws.ReceiveAsync(buf, ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Cancellation token fired (Stage 2 budget exhausted) -- this is the
                    // expected benign termination, NOT a real WebSocket failure. Report
                    // as timeout so silent-yield gate suppresses BUG-AUTO and downstream
                    // log readers see the correct cause.
                    return "timeout";
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP frame receive failed: {ex.GetType().Name}: {ex.Message}");
                    return "ws_receive_error";
                }

                if (res.MessageType == WebSocketMessageType.Close)
                {
                    Console.Error.WriteLine("[PLACEMENT:STAGE2] CDP ws closed by server");
                    return "ws_closed";
                }
                sb.Append(Encoding.UTF8.GetString(buf, 0, res.Count));
                if (!res.EndOfMessage) continue;

                var msg = sb.ToString();
                frameCount++;

                // EARLY FAIL: if we receive error messages about missing selectors
                // or DOM query failures, bail immediately instead of waiting for timeout
                if (msg.Contains("\"errorDetails\"") || msg.Contains("\"error\":{"))
                {
                    Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP error frame detected at frame {frameCount}: {msg.Substring(0, Math.Min(100, msg.Length))}");
                    return "cdp_error_frame";
                }

                // Cheap substring probe -- CDP frames are JSON and the method
                // string is uniquely identifiable. Avoids parsing every frame
                // (Chrome can be chatty with Page.frameNavigated, etc.).
                if (msg.Contains("\"Page.loadEventFired\""))
                    return "page_load_event";
            }
        }
        catch (OperationCanceledException)
        {
            return "timeout";
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PLACEMENT:STAGE2] CDP ws error: {ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            try { ws?.Dispose(); } catch { }
        }
        return ct.IsCancellationRequested ? "timeout" : "ws_closed";
    }
}