using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// Unified CDP tab session manager — encapsulates tab sandbox policy.
///
/// Two modes:
///   1. Hwnd-scoped (ask gemini/gpt/claude): each Claude prompt hwnd gets its own tab
///      - Key: "{command}+{subcommand}+{promptHwnd:X8}"
///      - Different Claude sessions → different tabs, even for same domain
///
///   2. Non-scoped (web eval, a11y read --eval-js): target any matching tab
///      - No per-session isolation — pattern/title matching
///
/// Usage:
///   using var session = CdpTabManager.CreateScoped("ask", "gemini", "gemini.google.com");
///   var title = await session.Cdp.GetTitleAsync();
///   await session.Cdp.FocusAsync(editorSel);
///
///   // Non-scoped:
///   using var session = CdpTabManager.CreateShared("chatgpt.com");
/// </summary>
internal sealed class CdpTabSession : IDisposable
{
    /// <summary>Connected CDP client (already on the correct tab).</summary>
    public CdpClient Cdp { get; }

    /// <summary>Sandbox key (e.g. "ask+gemini+001A2B3C"). Null for non-scoped sessions.</summary>
    public string? SandboxKey { get; }

    /// <summary>Expected host domain (e.g. "gemini.google.com").</summary>
    public string ExpectedHost { get; }

    /// <summary>CDP port.</summary>
    public int Port { get; }

    /// <summary>Whether this session owns a hwnd-scoped tab.</summary>
    public bool IsScoped => SandboxKey != null;

    internal CdpTabSession(CdpClient cdp, int port, string expectedHost, string? sandboxKey)
    {
        Cdp = cdp;
        Port = port;
        ExpectedHost = expectedHost;
        SandboxKey = sandboxKey;
    }

    /// <summary>Verify the tab is still on the expected domain. Returns false if drifted.</summary>
    public async Task<bool> ValidateAsync()
    {
        var url = await Cdp.GetUrlAsync() ?? "";
        return url.Contains(ExpectedHost, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Navigate back to the expected domain if the tab drifted.</summary>
    public async Task<bool> RecoverIfDriftedAsync()
    {
        if (await ValidateAsync()) return true;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[TAB] URL drift detected — recovering to {ExpectedHost}");
        Console.ResetColor();
        await Cdp.NavigateAsync($"https://{ExpectedHost}");
        await Task.Delay(1000);
        return await ValidateAsync();
    }

    /// <summary>Wait for page readyState = complete/interactive.</summary>
    public async Task WaitForReadyAsync(int timeoutMs = 10000)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var state = await Cdp.EvalAsync("document.readyState");
            if (state is "complete" or "interactive") return;
            await Task.Delay(300);
        }
    }

    public void Dispose() => Cdp.Dispose();
}

internal static class CdpTabManager
{
    /// <summary>
    /// Create a hwnd-scoped tab session (ask gemini/gpt/claude).
    /// Each Claude prompt hwnd gets its own isolated tab.
    /// </summary>
    /// <param name="command">Command name (e.g. "ask")</param>
    /// <param name="subcommand">Subcommand (e.g. "gemini")</param>
    /// <param name="expectedHost">Domain to navigate to (e.g. "gemini.google.com")</param>
    /// <param name="port">CDP port (default 9222)</param>
    /// <param name="promptHwnd">Override hwnd (default: from CallerHwnd)</param>
    /// <returns>Session with CDP connected to the correct tab, or null on failure.</returns>
    public static CdpTabSession? CreateScoped(string command, string subcommand, string expectedHost, int port = 9222, IntPtr? promptHwnd = null)
    {
        var hwnd = promptHwnd ?? EyeCmdPipeServer.CallerHwnd.Value ?? IntPtr.Zero;
        var hwndStr = hwnd != IntPtr.Zero
            ? hwnd.ToInt64().ToString("X8")
            : (GetSessionTag() ?? "00000000");
        var key = $"{command}+{subcommand}+{hwndStr}";

        var cdp = ConnectAndResolveTab(port, key, expectedHost);
        if (cdp == null) return null;

        return new CdpTabSession(cdp, port, expectedHost, key);
    }

    /// <summary>
    /// Create a non-scoped (shared) tab session.
    /// Targets any existing tab matching the domain — no per-hwnd isolation.
    /// </summary>
    public static CdpTabSession? CreateShared(string expectedHost, int port = 9222)
    {
        var cdp = new CdpClient();
        try
        {
            var detectedPort = CdpClient.DetectCdpPort(port);
            if (detectedPort <= 0) detectedPort = port;
            cdp.ConnectAsync(detectedPort).GetAwaiter().GetResult();

            // Find tab by host match
            var tabs = cdp.ListTabsAsync(detectedPort).GetAwaiter().GetResult();
            var match = tabs.FirstOrDefault(t => t.Url.Contains(expectedHost, StringComparison.OrdinalIgnoreCase));
            if (match != null && match.Id != cdp.TargetId)
                cdp.SwitchToTargetAsync(match.Id, detectedPort).GetAwaiter().GetResult();

            return new CdpTabSession(cdp, detectedPort, expectedHost, sandboxKey: null);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[TAB] Shared session failed: {ex.Message}");
            cdp.Dispose();
            return null;
        }
    }

    // ── Internal: connect + sandbox tab resolution ──

    private static CdpClient? ConnectAndResolveTab(int port, string key, string expectedHost)
    {
        var cdp = new CdpClient();
        try
        {
            var detectedPort = CdpClient.DetectCdpPort(port);
            if (detectedPort <= 0) detectedPort = port;
            cdp.ConnectAsync(detectedPort).GetAwaiter().GetResult();

            // Delegate to existing sandbox logic
            var entry = AskTargetRegistry.GetEntry(key);
            if (entry != null)
            {
                // Registry hit — validate
                var tabs = cdp.ListTabsAsync(detectedPort).GetAwaiter().GetResult();
                var tab = tabs.FirstOrDefault(t => t.Id == entry.TargetId);
                if (tab != null)
                {
                    if (tab.Url.Contains(expectedHost, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"[TAB] ✓ Scoped hit: {key}");
                        Console.ResetColor();
                        if (cdp.TargetId != entry.TargetId)
                            cdp.SwitchToTargetAsync(entry.TargetId, detectedPort).GetAwaiter().GetResult();
                        return cdp;
                    }
                    else
                    {
                        // URL mismatch — invalidate + create new
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[TAB] ✗ Drift: {key} (expected {expectedHost}, got {tab.Url[..Math.Min(60, tab.Url.Length)]})");
                        Console.ResetColor();
                        AskTargetRegistry.RemoveEntry(key);
                    }
                }
                else
                {
                    // Tab gone
                    Console.WriteLine($"[TAB] Stale: {key} — tab gone");
                    AskTargetRegistry.RemoveEntry(key);
                }
            }

            // Registry miss or invalidated — create fresh tab
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[TAB] Creating fresh tab: {key} → {expectedHost}");
            Console.ResetColor();
            var result = cdp.SendAsync("Target.createTarget",
                new System.Text.Json.Nodes.JsonObject { ["url"] = $"https://{expectedHost}" })
                .GetAwaiter().GetResult();
            var newId = result?["targetId"]?.GetValue<string>();
            if (newId != null)
            {
                cdp.SwitchToTargetAsync(newId, detectedPort).GetAwaiter().GetResult();
                AskTargetRegistry.SetEntry(key, newId, expectedHost);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[TAB] ✓ Fresh: {newId[..Math.Min(8, newId.Length)]}");
                Console.ResetColor();
                return cdp;
            }

            Console.Error.WriteLine("[TAB] Failed to create tab");
            cdp.Dispose();
            return null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[TAB] Session error: {ex.Message}");
            cdp.Dispose();
            return null;
        }
    }

    private static string? GetSessionTag()
    {
        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        return cwd.GetHashCode().ToString("X8");
    }
}
