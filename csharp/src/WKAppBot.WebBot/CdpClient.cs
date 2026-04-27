using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

/// <summary>
/// Minimal Chrome DevTools Protocol client using System.Net.WebSockets.
/// Zero external dependencies -- talks to Chrome via WebSocket JSON-RPC.
///
/// Usage:
///   var cdp = new CdpClient();
///   await cdp.ConnectAsync(9222);                        // connect to Chrome debugging port
///   await cdp.NavigateAsync("https://naver.com");        // navigate
///   var title = await cdp.EvalAsync("document.title");   // eval JS
///   await cdp.ClickAsync("#btn-login");                  // click by CSS selector
///   await cdp.TypeAsync("#search", "hello");             // type into input
///   var png = await cdp.ScreenshotAsync();               // capture page
///   await cdp.DisconnectAsync();
/// </summary>
public sealed partial class CdpClient : IAsyncDisposable, IDisposable
{
    private ClientWebSocket? _ws;
    private int _messageId;
    private readonly ConcurrentDictionary<int, TaskCompletionSource<JsonNode?>> _pending = new();
    private CancellationTokenSource? _receiveCts;
    private Task? _receiveTask;
    private TaskCompletionSource<JsonNode?>? _fileChooserTcs;
    private IDisposable? _activeZoom; // auto-magnifier for non-foreground Chrome
    private readonly HttpClient _http = new();

    public bool IsConnected => _ws?.State == WebSocketState.Open;
    public string? WebSocketUrl { get; private set; }
    public string? TargetId { get; private set; }
    private long _streamChunkSeq;

    /// <summary>Reconnect to the same tab using saved WebSocketUrl (tab still open, WS dropped).</summary>
    public async Task ReconnectAsync(int timeoutMs = 5000)
    {
        if (string.IsNullOrEmpty(WebSocketUrl))
            throw new InvalidOperationException("No WebSocketUrl saved -- cannot reconnect");

        // Dispose old WebSocket
        try { _receiveCts?.Cancel(); } catch { }
        try { _ws?.Dispose(); } catch { }

        using var cts = new CancellationTokenSource(timeoutMs);
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(WebSocketUrl), cts.Token);

        _receiveCts = new CancellationTokenSource();
        _receiveTask = ReceiveLoopAsync(_receiveCts.Token);

        await EnableRuntimeWithRetry();
        Console.WriteLine($"[CDP] Reconnected to {TargetId}");
    }
    private int? _currentContextId;

    /// <summary>Chrome browser process ID (resolved from CDP port).</summary>
    public int ChromePid { get; private set; }

    /// <summary>
    /// Focus theft monitoring for every CDP SendAsync call.
    /// When enabled, detects if Chrome steals OS foreground after each CDP command.
    /// OnFocusTheft(method, prevFg, curFg) fires ONLY when Chrome itself stole focus
    /// (not when the user naturally switches windows).
    /// </summary>
    public bool EnableFocusTheftMonitoring { get; set; }
    public Action<string, nint, nint>? OnFocusTheft { get; set; }

    /// <summary>Chrome main window handle -- set by caller for accurate focus theft detection.</summary>
    public nint ChromeWindowHandle { get; set; }

    /// <summary>Current high-level operation context (e.g. "ask-gemini:wait-response") for focus theft diagnostics.</summary>
    public string? OperationContext { get; set; }

    /// <summary>Callback for JS errors -- caller can route to Slack. Args: (errorMsg, expression, pageUrl, contextDump).</summary>
    public Action<string>? OnJsError { get; set; }

    /// <summary>
    /// Called once when CDP session starts active operations on a non-foreground Chrome window.
    /// Caller should show magnifier on the target. Returns IDisposable to dismiss when done.
    /// Signature: (chromeHwnd) -> IDisposable? zoom
    /// </summary>
    public Func<IntPtr, IDisposable?>? OnZoomRequired { get; set; }

    /// <summary>
    /// Connect to Chrome's DevTools WebSocket.
    /// Chrome must be running with --remote-debugging-port=PORT.
    /// </summary>
    /// <summary>CDP reliability notice -- printed once per process.</summary>
    private static int _cdpWarningShown;

    /// <summary>
    /// CDP fallback training: when a caller catches a CDP exception but has no real fallback
    /// (empty catch / catch-and-rethrow / catch-and-return-null), inject random failures
    /// 1-in-9 to force fallback implementation. Callers that DO implement fallback are unaffected.
    /// Set to false to disable (e.g. in tests).
    /// </summary>
    public static bool FallbackTrainingEnabled { get; set; } = true;
    private static readonly Random _fallbackRng = new();
    private static readonly HashSet<string> _suggestedCallSites = new();

    /// <summary>Hook for auto-suggest: set by CLI to route CDP fallback warnings to wkappbot suggest.</summary>
    public static Action<string>? OnFallbackSuggest { get; set; }

    /// <summary>
    /// Call this after a CDP operation succeeds. If the caller has no try-catch with real fallback,
    /// occasionally throws to train the caller to implement proper error handling.
    /// shouldProbe: set false if caller has known fallback (e.g. whisper 3-tier cascade).
    /// </summary>
    public static void ProbeFallbackReadiness(bool hasFallback = false)
    {
        if (!FallbackTrainingEnabled || hasFallback) return;

        // Collect caller info
        var stack = new System.Diagnostics.StackTrace(1, true);
        var caller = stack.GetFrame(0);
        var callSite = $"{caller?.GetMethod()?.DeclaringType?.Name}.{caller?.GetMethod()?.Name}";
        var fileName = caller?.GetFileName();
        var lineNum = caller?.GetFileLineNumber();

        // Auto-suggest once per call site
        if (!string.IsNullOrEmpty(callSite) && _suggestedCallSites.Add(callSite))
        {
            var suggestText = $"CDP fallback missing at {callSite} ({Path.GetFileName(fileName)}:{lineNum}). " +
                "Implement try/catch with retry or alternative action.";
            Console.Error.WriteLine($"[CDP-FALLBACK] {suggestText}");
            try { OnFallbackSuggest?.Invoke(suggestText); } catch { }
        }

        // 1-in-9 random failure injection
        if (_fallbackRng.Next(9) == 0)
        {
            Console.Error.WriteLine($"[CDP-TRAINING] Injected failure at {callSite} -- implement fallback! (1-in-9 probe)");
            throw new InvalidOperationException($"[CDP-TRAINING] Simulated CDP failure at {callSite}. Implement retry+fallback to handle real failures.");
        }
    }

    public async Task ConnectAsync(int port = 9222, int tabIndex = 0, int timeoutMs = 10_000, string? preferredTargetTag = null)
    {
        // One-time warning: CDP operations are inherently fragile
        if (Interlocked.CompareExchange(ref _cdpWarningShown, 1, 0) == 0)
            Console.Error.WriteLine("[CDP] CDP is fragile. Implement retry+fallback for production.");

        int origTabIndex = tabIndex; // preserve for self-heal replay (tabIndex is decremented in the loop below)
        using var cts = new CancellationTokenSource(timeoutMs);
        var ct = cts.Token;

        // Get available targets from Chrome's JSON API
        var json = await _http.GetStringAsync($"http://localhost:{port}/json", ct);
        var targets = JsonSerializer.Deserialize<JsonArray>(json);
        if (targets == null || targets.Count == 0)
            throw new InvalidOperationException("No Chrome targets found");

        string? wsUrl = null;
        string? resolvedTargetId = null;
        string? resolvedTargetUrl = null;
        foreach (var target in targets)
        {
            var type = target?["type"]?.GetValue<string>();
            if (type != "page") continue;

            var url = target?["url"]?.GetValue<string>();
            var id = target?["id"]?.GetValue<string>();
            if (!string.IsNullOrWhiteSpace(preferredTargetTag) && url != null && url.Contains(preferredTargetTag, StringComparison.OrdinalIgnoreCase))
            {
                wsUrl = target?["webSocketDebuggerUrl"]?.GetValue<string>();
                resolvedTargetId = id;
                resolvedTargetUrl = url;
                break;
            }

            if (tabIndex-- <= 0)
            {
                wsUrl = target?["webSocketDebuggerUrl"]?.GetValue<string>();
                resolvedTargetId = id;
                resolvedTargetUrl = url;
                break;
            }
        }


        if (wsUrl == null)
            throw new InvalidOperationException("No page target with WebSocket URL found");

        TargetId = resolvedTargetId;
        WebSocketUrl = wsUrl;
        RememberConnectTarget(port, preferredTargetTag, origTabIndex); // record for EnsureConnectedAsync self-heal
        Console.Error.WriteLine($"[CDP:CONNECT] target={resolvedTargetId} url={resolvedTargetUrl ?? "?"} port={port}{(preferredTargetTag != null ? $" tag={preferredTargetTag}" : "")}");
        _ws = new ClientWebSocket();
        await _ws.ConnectAsync(new Uri(wsUrl), ct);

        // Resolve Chrome browser PID from the CDP port
        ChromePid = ResolvePidFromPort(port);

        // Start background receive loop BEFORE sending commands (otherwise responses are never read)
        _receiveCts = new CancellationTokenSource();
        _receiveTask = ReceiveLoopAsync(_receiveCts.Token);

        // Re-enable main-world contexts on every run (with retry for heavy tab loads)
        await EnableRuntimeWithRetry();
        await SendAsync("Page.getFrameTree");

        // Refresh execution context
        try
        {
            var contextInfo = await SendAsync("Runtime.getExecutionContexts");
            var contexts = contextInfo?["result"] as JsonArray;
            var mainContext = contexts?.FirstOrDefault();
            _currentContextId = mainContext?["id"]?.GetValue<int?>();
        }
        catch { }
    }

    /// <summary>
    /// Find the PID of the process listening on a TCP port using netstat.
    /// Fallback: returns 0 if unable to determine.
    /// </summary>
    private static int ResolvePidFromPort(int port)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = "-ano",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using var proc = AppBotPipe.Start(psi);
            if (proc == null) return 0;

            var output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit(3000);

            // Parse netstat output: look for LISTENING on our port
            // Format: "  TCP    127.0.0.1:9222    0.0.0.0:0    LISTENING    12345"
            var needle = $":{port}";
            foreach (var rawLine in output.Split('\n'))
            {
                var line = rawLine.Trim();
                if (!line.Contains(needle) || !line.Contains("LISTENING")) continue;

                // Verify the port matches exactly (not :92220)
                var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 5) continue;
                var local = parts[1]; // e.g. "127.0.0.1:9222"
                if (!local.EndsWith(needle)) continue;

                if (int.TryParse(parts[^1], out var pid))
                    return pid;
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// Send Runtime.enable + Page.enable + DOM.enable with exponential backoff retry.
    /// Retries up to maxRetries times with increasing delays before giving up.
    /// Called from ConnectAsync/ReconnectAsync -- prevents premature Chrome restart.
    /// </summary>
    internal async Task EnableRuntimeWithRetry(int maxRetries = 3, int baseDelayMs = 500)
    {
        string[] enableCmds = ["Runtime.enable", "Page.enable", "DOM.enable"];
        for (int attempt = 0; attempt <= maxRetries; attempt++)
        {
            try
            {
                foreach (var cmd in enableCmds)
                    await SendAsync(cmd, timeoutMs: attempt == 0 ? 10000 : 15000 + attempt * 5000);
                if (attempt > 0)
                    Console.Error.WriteLine($"[CDP] Runtime.enable succeeded on retry {attempt}");
                return;
            }
            catch (TimeoutException) when (attempt < maxRetries)
            {
                var delay = baseDelayMs * (1 << attempt); // 500, 1000, 2000ms
                Console.Error.WriteLine($"[CDP] Runtime.enable timeout (attempt {attempt + 1}/{maxRetries + 1}) -- retry in {delay}ms");
                await Task.Delay(delay);
            }
        }
        // Final attempt failed -- throw so caller can handle (e.g., restart Chrome)
        throw new TimeoutException($"CDP Runtime.enable failed after {maxRetries + 1} attempts");
    }
}
