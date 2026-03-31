using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot dashboard [--port 4443] [--no-browser]
    /// Local HTTPS web dashboard — mini Slack for WKAppBot.
    /// HttpListener + WebSocket, single embedded HTML page.
    /// </summary>
    static int DashboardCommand(string[] args)
    {
        int port = 4443;
        bool openBrowser = !args.Contains("--no-browser");

        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--port" && i + 1 < args.Length && int.TryParse(args[i + 1], out var p))
                port = p;

        // ── Resolve URLs ──
        // Use http for now (no cert setup needed). Upgrade to https with mkcert later.
        var prefix = $"http://+:{port}/";
        var localUrl = $"http://localhost:{port}/";

        Console.WriteLine($"[DASH] Starting dashboard on port {port}...");

        HttpListener? listener = null;
        try
        {
            listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
        }
        catch (HttpListenerException ex) when (ex.ErrorCode == 5) // Access Denied
        {
            // http://+:port requires netsh urlacl. Fall back to http://*:port (same but different ACL)
            // If that also fails, try localhost-only.
            Console.Error.WriteLine($"[DASH] Access denied for {prefix} — trying http://*:{port}/");
            listener?.Close();
            listener = new HttpListener();
            prefix = $"http://*:{port}/";
            try { listener.Prefixes.Add(prefix); listener.Start(); }
            catch
            {
                Console.Error.WriteLine($"[DASH] Also denied — falling back to 127.0.0.1");
                listener?.Close();
                listener = new HttpListener();
                prefix = $"http://127.0.0.1:{port}/";
                listener.Prefixes.Add(prefix);
                listener.Start();
            }
            listener.Prefixes.Add(prefix);
            listener.Start();
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[DASH] Dashboard ready: {localUrl}");
        Console.ResetColor();

        // Show LAN IPs for phone access
        try
        {
            foreach (var iface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (iface.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up) continue;
                foreach (var addr in iface.GetIPProperties().UnicastAddresses)
                {
                    if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                        && !IPAddress.IsLoopback(addr.Address))
                        Console.WriteLine($"[DASH] Phone: http://{addr.Address}:{port}/");
                }
            }
        }
        catch { }

        if (openBrowser)
        {
            try { AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo(localUrl) { UseShellExecute = true }, Environment.CurrentDirectory, "DASHBOARD"); }
            catch { }
        }

        // ── Ctrl+C graceful shutdown ──
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // ── Accept loop ──
        Console.WriteLine("[DASH] Ctrl+C to stop. Waiting for connections...");
        _ = AcceptLoop(listener, cts.Token);

        // ── Keep alive ──
        try { Task.Delay(-1, cts.Token).Wait(); }
        catch (AggregateException) { }

        listener.Stop();
        Console.WriteLine("[DASH] Dashboard stopped.");
        return 0;
    }

    static async Task AcceptLoop(HttpListener listener, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            HttpListenerContext ctx;
            try { ctx = await listener.GetContextAsync(); }
            catch { break; }

            _ = Task.Run(async () =>
            {
                try { await HandleDashRequest(ctx, ct); }
                catch (Exception ex) { Console.Error.WriteLine($"[DASH] Error: {ex.Message}"); }
            }, ct);
        }
    }

    static async Task HandleDashRequest(HttpListenerContext ctx, CancellationToken ct)
    {
        var req = ctx.Request;
        var resp = ctx.Response;

        // ── WebSocket upgrade ──
        if (req.IsWebSocketRequest)
        {
            var wsCtx = await ctx.AcceptWebSocketAsync(null);
            Console.Error.WriteLine($"[DASH] WS connected ({DashboardBroadcaster.ClientCount + 1} clients)");
            await DashboardBroadcaster.HandleClientAsync(wsCtx.WebSocket, ct);
            Console.Error.WriteLine($"[DASH] WS disconnected ({DashboardBroadcaster.ClientCount} clients)");
            return;
        }

        // ── API: /api/status ──
        if (req.Url?.AbsolutePath == "/api/status")
        {
            resp.ContentType = "application/json";
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                clients = DashboardBroadcaster.ClientCount,
                history = DashboardBroadcaster.HistoryCount,
                uptime = Environment.TickCount64 / 1000,
            });
            var bytes = Encoding.UTF8.GetBytes(json);
            resp.ContentLength64 = bytes.Length;
            await resp.OutputStream.WriteAsync(bytes, ct);
            resp.Close();
            return;
        }

        // ── Serve HTML page ──
        resp.ContentType = "text/html; charset=utf-8";
        var html = GetDashboardHtml();
        var htmlBytes = Encoding.UTF8.GetBytes(html);
        resp.ContentLength64 = htmlBytes.Length;
        await resp.OutputStream.WriteAsync(htmlBytes, ct);
        resp.Close();
    }

    static string? _cachedDashHtml;
    static string GetDashboardHtml()
    {
        if (_cachedDashHtml != null) return _cachedDashHtml;

        // Try embedded resource first
        var asm = Assembly.GetExecutingAssembly();
        var resName = asm.GetManifestResourceNames().FirstOrDefault(n => n.Contains("DashboardPage"));
        if (resName != null)
        {
            using var stream = asm.GetManifestResourceStream(resName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8);
                _cachedDashHtml = reader.ReadToEnd();
                return _cachedDashHtml;
            }
        }

        // Fallback: try file on disk
        var filePath = Path.Combine(AppContext.BaseDirectory, "Commands", "DashboardPage.html");
        if (!File.Exists(filePath))
            filePath = Path.Combine(Path.GetDirectoryName(asm.Location) ?? ".", "Commands", "DashboardPage.html");
        if (File.Exists(filePath))
        {
            _cachedDashHtml = File.ReadAllText(filePath, Encoding.UTF8);
            return _cachedDashHtml;
        }

        _cachedDashHtml = "<html><body><h1>WKAppBot Dashboard</h1><p>DashboardPage.html not found</p></body></html>";
        return _cachedDashHtml;
    }
}
