using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed record CdpPageTarget(string Id, string Url, string Title, string WebSocketDebuggerUrl);

    // AsyncLocal prefix: each async execution context (Task.Run lambda) has its own prefix.
    // A single AsyncPrefixWriter is installed globally on first use and reads this per-context value.
    static readonly System.Threading.AsyncLocal<string?> _asyncLinePrefix = new();
    static volatile bool _asyncPrefixWriterInstalled;
    static readonly object _prefixInstallLock = new();

    /// <summary>
    /// Set the output line prefix for the current async execution context (e.g. "[gpt] ").
    /// Installs a single AsyncPrefixWriter on Console.Out the first time it is called.
    /// Each parallel Task.Run context gets its own AsyncLocal value → correct per-AI prefix
    /// even when multiple AIs stream output concurrently.
    /// </summary>
    static IDisposable? ApplyOutputPrefix(string? prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return null;
        if (!_asyncPrefixWriterInstalled)
        {
            lock (_prefixInstallLock)
            {
                if (!_asyncPrefixWriterInstalled)
                {
                    Console.SetOut(new AsyncPrefixWriter(Console.Out));
                    _asyncPrefixWriterInstalled = true;
                }
            }
        }
        var prev = _asyncLinePrefix.Value;
        _asyncLinePrefix.Value = prefix;
        return new RestoreAsyncPrefix(prev);
    }

    sealed class RestoreAsyncPrefix(string? prev) : IDisposable
    {
        public void Dispose() => _asyncLinePrefix.Value = prev;
    }

    /// <summary>
    /// Single global Console.Out replacement. Reads AsyncLocal prefix per async execution context.
    /// Buffers per-context line, flushes with prefix atomically when '\n' is seen.
    /// Parallel Task.Run tasks each have their own AsyncLocal → correct AI label per output line.
    /// </summary>
    sealed class AsyncPrefixWriter(TextWriter sink) : TextWriter
    {
        static readonly object _writeLock = new();
        // Per-async-context line buffer (AsyncLocal isolates per Task.Run execution context)
        static readonly System.Threading.AsyncLocal<System.Text.StringBuilder?> _buf = new();
        static System.Text.StringBuilder Buf => _buf.Value ??= new();

        public override System.Text.Encoding Encoding => sink.Encoding;
        public override void Write(char value) { Buf.Append(value); if (value == '\n') FlushLine(); }
        public override void Write(string? value) { if (value != null) foreach (var c in value) Write(c); }
        public override void WriteLine(string? value) { Write(value ?? ""); Write('\n'); }
        public override void WriteLine() => Write('\n');
        public override void Flush() { if (Buf.Length > 0) FlushLine(); sink.Flush(); }

        void FlushLine()
        {
            var line = Buf.ToString();
            Buf.Clear();
            var prefix = _asyncLinePrefix.Value;
            lock (_writeLock)
            {
                if (!string.IsNullOrEmpty(prefix)) sink.Write(prefix);
                sink.Write(line);
                sink.Flush();
            }
        }
        protected override void Dispose(bool disposing) { if (disposing) Flush(); }
    }

    static string BuildAskTargetTag(string provider)
    {
        var hash = GetSessionTag() ?? "default";
        return $"{provider}-{hash}";
    }

    /// <summary>
    /// Build a sandbox key: "{command}+{subcommand}+{hwnd:X8}".
    /// HWND-based: each prompt window → isolated Chrome tab, guaranteed no cross-contamination.
    /// Falls back to cwdHash if HWND not available (direct CLI mode, no Eye).
    /// </summary>
    static string BuildSandboxKey(string command, string subcommand)
    {
        var hwnd = EyeCmdPipeServer.CallerHwnd.Value ?? IntPtr.Zero;
        var hwndStr = hwnd != IntPtr.Zero
            ? hwnd.ToInt64().ToString("X8")
            : (GetSessionTag() ?? "00000000");
        return $"{command}+{subcommand}+{hwndStr}";
    }

    /// <summary>
    /// GetOrCreateSandboxedTab — core sandboxing algorithm:
    ///   ① Registry hit → validate URL against expectedHost
    ///       match  → return existing tab (reconnect)
    ///       mismatch → fast-fail: invalidate registry + create new tab (leave polluted tab for debugging)
    ///   ② Registry miss → EnsureCorrectWindowAsync (existing positioning logic) → register
    /// </summary>
    static async Task<string?> GetOrCreateSandboxedTabAsync(CdpClient cdp, int port, string key, string expectedHost)
    {
        static async Task<System.Text.Json.Nodes.JsonArray?> TryGetTargetsBeforeCreateAsync(int probePort)
        {
            try
            {
                using var http = new HttpClient();
                var json = await http.GetStringAsync($"http://localhost:{probePort}/json");
                return JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonArray>(json);
            }
            catch
            {
                return null;
            }
        }

        var entry = AskTargetRegistry.GetEntry(key);
        if (entry != null)
        {
            // Registry hit — validate URL
            var targets = await GetPageTargetsAsync(port);
            var tab = targets.FirstOrDefault(t => t.Id == entry.TargetId);
            if (tab != null)
            {
                if (tab.Url.Contains(expectedHost, StringComparison.OrdinalIgnoreCase))
                {
                    // ✓ URL OK — reconnect to this tab
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[SANDBOX] ✓ Hit: {key} → {entry.TargetId[..Math.Min(8,entry.TargetId.Length)]}");
                    Console.ResetColor();
                    if (cdp.TargetId != entry.TargetId)
                        await cdp.SwitchToTargetAsync(entry.TargetId, port);
                    return entry.TargetId;
                }
                else
                {
                    // ✗ URL mismatch — fast-fail: invalidate + new tab
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[SANDBOX] ✗ Mismatch: {key}");
                    Console.WriteLine($"[SANDBOX]   expected host: {expectedHost}");
                    Console.WriteLine($"[SANDBOX]   actual url:    {tab.Url[..Math.Min(80, tab.Url.Length)]}");
                    Console.WriteLine($"[SANDBOX]   → invalidating registry, creating clean tab");
                    Console.ResetColor();

                    AskTargetRegistry.RemoveEntry(key);
                    // Leave polluted tab alive (debugging) — create a new clean tab
                    try
                    {
                        var beforeTargets = await TryGetTargetsBeforeCreateAsync(port);
                        var result = await cdp.SendAsync("Target.createTarget",
                            new System.Text.Json.Nodes.JsonObject { ["url"] = $"https://{expectedHost}" });
                        var newId = result?["targetId"]?.GetValue<string>();
                        if (newId != null)
                        {
                            await cdp.DumpTabGrowthAsync(port, "sandbox-mismatch-create", beforeTargets, key, expectedHost, newId);
                            await cdp.SwitchToTargetAsync(newId, port);
                            AskTargetRegistry.SetEntry(key, newId, expectedHost);
                            await cdp.TryCloseTabByIdAsync(port, entry.TargetId, "sandbox-mismatch");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[SANDBOX] ✓ New tab after mismatch: {newId[..Math.Min(8,newId.Length)]}");
                            Console.ResetColor();
                            return newId;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWarning("SANDBOX", "Create failed after mismatch", ex);
                    }
                    return null;
                }
            }
            else
            {
                // Tab gone — remove stale entry, fall through to create
                Console.WriteLine($"[SANDBOX] Stale entry {key} (tab no longer exists) — removing");
                AskTargetRegistry.RemoveEntry(key);
            }
        }

        // Registry miss — first check if a matching-host tab already exists (e.g. Chrome launched with URL)
        // This prevents duplicate tabs when ChromeLauncher already opened the target URL.
        {
            var existingTargets = await GetPageTargetsAsync(port);
            var hostMatch = existingTargets.FirstOrDefault(t =>
                t.Url.Contains(expectedHost, StringComparison.OrdinalIgnoreCase)
                && !t.Url.Contains("#wkbot-", StringComparison.Ordinal));
            if (hostMatch != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[SANDBOX] Miss but host-match found: {key} -> {hostMatch.Id[..Math.Min(8,hostMatch.Id.Length)]} ({hostMatch.Url[..Math.Min(50,hostMatch.Url.Length)]})");
                Console.ResetColor();
                if (cdp.TargetId != hostMatch.Id)
                    await cdp.SwitchToTargetAsync(hostMatch.Id, port);
                AskTargetRegistry.SetEntry(key, hostMatch.Id, expectedHost);
                return hostMatch.Id;
            }
        }

        // No existing tab — create a fresh isolated tab.
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[SANDBOX] Miss: {key} — creating fresh tab for {expectedHost}");
        Console.ResetColor();
        try
        {
            var beforeTargets = await TryGetTargetsBeforeCreateAsync(port);
            var result = await cdp.SendAsync("Target.createTarget",
                new System.Text.Json.Nodes.JsonObject { ["url"] = $"https://{expectedHost}" });
            var newId = result?["targetId"]?.GetValue<string>();
            if (newId != null)
            {
                await cdp.DumpTabGrowthAsync(port, "sandbox-miss-create", beforeTargets, key, expectedHost, newId);
                await cdp.SwitchToTargetAsync(newId, port);
                AskTargetRegistry.SetEntry(key, newId, expectedHost);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[SANDBOX] ✓ Fresh tab: {newId[..Math.Min(8, newId.Length)]}");
                Console.ResetColor();
                return newId;
            }
        }
        catch (Exception ex)
        {
            LogWarning("SANDBOX", "Create failed", ex);
        }
        return null;
    }

    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false, string? targetTag = null)
    {
        var task = Task.Run(async () =>
        {
            try
            {
                // ── Multi-browser port scan ──
                // When a preferred host is set, scan ports 9222-9230 to find which Chrome
                // instance already has that host open — avoids hardcoding port 9222.
                if (!string.IsNullOrWhiteSpace(preferredHost))
                {
                    var hostPort = await ChromeLauncher.FindBestPortForHostAsync(preferredHost);
                    if (hostPort > 0)
                    {
                        if (hostPort != port)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"[ASK] Multi-browser: {preferredHost} found on port {hostPort}");
                            Console.ResetColor();
                        }
                        port = hostPort;
                    }
                    else
                    {
                        // Host not open anywhere — find a free port for new Chrome launch
                        var freePort = await ChromeLauncher.FindFirstFreePortAsync();
                        if (freePort != port)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"[ASK] Port {port} taken, using port {freePort} for new Chrome");
                            Console.ResetColor();
                            port = freePort;
                        }
                    }
                }

                var active = await ChromeLauncher.IsPortActiveAsync(port);
                if (!active)
                {
                    var launchUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    Console.WriteLine($"[ASK] Launching Chrome on port {port}…{launchUrl ?? "about:blank"}...");
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl);
                    await Task.Delay(2500);
                }

                var cdp = new CdpClient();
                try
                {
                    await cdp.ConnectAsync(port, timeoutMs: 5000, preferredTargetTag: preferredHost);
                }
                catch (Exception firstEx)
                {
                    // If Chrome is running but CDP timed out (Runtime.enable) → broken state → restart
                    bool chromeBroken = firstEx is TimeoutException
                        && await ChromeLauncher.IsPortActiveAsync(port);
                    if (chromeBroken)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[ASK] Chrome stuck (Runtime.enable timeout) — restarting Chrome…");
                        Console.ResetColor();
                        await ChromeLauncher.KillChromeOnPortAsync(port);
                        AskTargetRegistry.ClearAll();
                    }
                    else
                    {
                        Console.WriteLine("[ASK] CDP connect failed — launching Chrome...");
                    }

                    var launchUrl2 = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl2);
                    await Task.Delay(3000);
                    await cdp.ConnectAsync(port, timeoutMs: 10000, preferredTargetTag: preferredHost);
                }

                // ── Pre-minimize Chrome BEFORE any tab action (prevent focus steal) ──
                var preMinHwnd = cdp.GetChromeWindowHandle();
                if (preMinHwnd != IntPtr.Zero && !NativeMethods.IsIconic(preMinHwnd))
                {
                    NativeMethods.ShowWindow(preMinHwnd, 6); // SW_MINIMIZE
                    // Wait until actually iconic (up to 500ms) — tab action must not start before minimize completes
                    for (int wi = 0; wi < 50 && !NativeMethods.IsIconic(preMinHwnd); wi++)
                        await Task.Delay(10);
                    Console.WriteLine("[ASK] Chrome minimized before tab action (focus-steal prevention)");
                }

                // Sandbox: Registry hit → URL validate → fast-fail on mismatch
                // Registry miss → EnsureCorrectWindowAsync (positioning + creation)
                string? resolvedId;
                if (!string.IsNullOrWhiteSpace(preferredHost) && !string.IsNullOrWhiteSpace(targetTag))
                {
                    resolvedId = await GetOrCreateSandboxedTabAsync(cdp, port, targetTag, preferredHost);

                    // Sandbox path bypasses EnsureCorrectWindowAsync — manually position window to ExpectedBounds.
                    // Without this, Chrome opens wherever it defaults (wrong position / wrong monitor).
                    if (resolvedId != null)
                    {
                        var (expX, expY, expW, expH) = CdpClient.ExpectedBounds;
                        var wb = await cdp.GetWindowForTargetAsync(resolvedId);
                        if (wb != null)
                        {
                            await cdp.SetWindowBoundsAsync(wb.Value.windowId, expX, expY, expW, expH);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"[ASK] Window positioned: ({expX},{expY}) {expW}×{expH}");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    // No host constraint — fall back to old EnsureCorrectWindowAsync path
                    var savedTargetId = !string.IsNullOrWhiteSpace(targetTag) ? AskTargetRegistry.GetTargetId(targetTag) : null;
                    await CloseBlankTabs(port);
                    var navigateUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, navigateUrl, savedTargetId: savedTargetId);
                    if (resolvedId != null && !string.IsNullOrWhiteSpace(targetTag))
                    {
                        AskTargetRegistry.SetTargetId(targetTag, resolvedId);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"[ASK] Target: {targetTag} → {resolvedId[..Math.Min(8, resolvedId.Length)]}");
                        Console.ResetColor();
                    }
                }

                var chromeHwnd = cdp.GetChromeWindowHandle();
                if (chromeHwnd != IntPtr.Zero)
                {
                    var readiness = new WKAppBot.Win32.Input.InputReadiness();
                    var blocker = readiness.DetectBlocker(chromeHwnd);
                    if (blocker != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[AAR:CDP] Blocker: {blocker.Title} (hwnd={blocker.Handle:X})");
                        Console.ResetColor();
                    }
                    if (NativeMethods.IsIconic(chromeHwnd))
                    {
                        Console.WriteLine($"[AAR:CDP] Chrome minimized …focusless restore");
                        NativeMethods.ShowWindow(chromeHwnd, 4);
                        await Task.Delay(300);
                    }
                }

                cdp.ChromeWindowHandle = (nint)cdp.GetChromeWindowHandle();
                cdp.EnableFocusTheftMonitoring = true;

                // Magnifier: show on Chrome window for ALL CDP operations (non-foreground target)
                cdp.OnZoomRequired = (hwnd) =>
                {
                    try
                    {
                        NativeMethods.GetWindowRect(hwnd, out var wr);
                        if (wr.Width > 0 && wr.Height > 0)
                            return ClickZoomHelper.BeginFromRect(
                                new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Width, wr.Height),
                                hwnd, "cdp-session", cdp.OperationContext ?? "CDP");
                    }
                    catch { }
                    return null;
                };

                cdp.OnFocusTheft = (method, prevFg, curFg) =>
                {
                    TeeTextWriter._focusTheftDetected = true;
                    // Capture real call stack for diagnostics (not just "no stack")
                    var stack = new System.Diagnostics.StackTrace(true).ToString();
                    var focusEx = new InvalidOperationException(
                        $"Focus stolen @ CDP:{method}: was={prevFg:X8} now={curFg:X8}");
                    // Inject real stack via helper field
                    _lastFocusTheftStack = stack;
                    LogError("BUG-AUTO", focusEx);
                    _lastFocusTheftStack = null;
                    // Immediately restore user focus (fire-and-forget retry)
                    _ = Task.Run(() => RestoreFocusWithRetryAsync((IntPtr)prevFg, $"cdp-theft:{method}", cdp));
                };

                // JS errors → Slack thread (빠른 버그 추적)
                cdp.OnJsError = (dump) => SlackPostToThread($"🔴 {dump}", "🦉 Moderator");

                return (CdpClient?)cdp;
            }
            catch (Exception ex)
            {
                LogError("ASK", ex);
                return (CdpClient?)null;
            }
        });
        return task.GetAwaiter().GetResult();
    }

    static async Task<List<CdpPageTarget>> GetPageTargetsAsync(int port)
    {
        var result = new List<CdpPageTarget>();
        try
        {
            using var http = new HttpClient();
            var json = await http.GetStringAsync($"http://localhost:{port}/json");
            var arr = JsonSerializer.Deserialize<JsonArray>(json);
            if (arr == null) return result;

            foreach (var node in arr)
            {
                var type = node?["type"]?.GetValue<string>() ?? "";
                if (!string.Equals(type, "page", StringComparison.OrdinalIgnoreCase))
                    continue;

                var ws = node?["webSocketDebuggerUrl"]?.GetValue<string>() ?? "";
                if (string.IsNullOrWhiteSpace(ws))
                    continue;

                var url = node?["url"]?.GetValue<string>() ?? "";
                var title = node?["title"]?.GetValue<string>() ?? "";
                var id = node?["id"]?.GetValue<string>() ?? string.Empty;
                result.Add(new CdpPageTarget(id, url, title, ws));
            }
        }
        catch { }

        return result;
    }
}
