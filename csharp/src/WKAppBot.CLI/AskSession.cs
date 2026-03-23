using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// Declarative AI provider configuration — selectors, host, quirks.
/// No JS here — just data that drives the common AskSession flow.
/// </summary>
internal record AiProvider(
    string Name,                // "gemini", "claude", "gpt"
    string Host,                // "gemini.google.com"
    string[] EditorSelectors,   // contenteditable / textarea variants
    string ResponseSelector,    // last-response element
    string[] StopSelectors      // stop/cancel button variants
)
{
    public static readonly AiProvider Gemini = new(
        Name: "gemini", Host: "gemini.google.com",
        EditorSelectors: [
            ".ql-editor",
            "[role='textbox'][contenteditable='true']",
            "div[contenteditable='true']",
            "div.ql-editor",
            "rich-textarea [contenteditable]",
            ".input-area [contenteditable]"
        ],
        ResponseSelector: "model-response",
        StopSelectors: ["button[aria-label*='Stop']", "mat-icon[fonticon='stop_circle']"]
    );

    public static readonly AiProvider Claude = new(
        Name: "claude", Host: "claude.ai",
        EditorSelectors: [
            ".ProseMirror[contenteditable='true']",
            "[contenteditable='true']",
            "div[contenteditable='plaintext-only']"
        ],
        ResponseSelector: "[data-is-streaming]",
        StopSelectors: ["button[aria-label*='Stop']"]
    );

    public static readonly AiProvider ChatGpt = new(
        Name: "gpt", Host: "chatgpt.com",
        EditorSelectors: [
            "#prompt-textarea",
            "[contenteditable='true'][data-placeholder]",
            "[role='textbox']"
        ],
        ResponseSelector: "[data-message-author-role='assistant']",
        StopSelectors: [
            "button[aria-label='Stop generating']",
            "button[data-testid='stop-button']"
        ]
    );

    public static AiProvider? FromName(string name) => name.ToLowerInvariant() switch
    {
        "gemini" => Gemini,
        "claude" => Claude,
        "gpt" or "chatgpt" => ChatGpt,
        _ => null
    };
}

/// <summary>
/// Shared AI ask session — orchestrates the common flow across all providers.
/// Eliminates per-provider reimplementation of connect → type → send → poll.
///
/// Usage:
///   using var session = new AskSession(AiProvider.Gemini);
///   if (!await session.ConnectAsync()) return 1;
///   await session.NavigateAsync(newSession);
///   var editor = await session.FindEditorAsync();
///   await session.TypeAsync(editor, question);
///   await session.SendAsync(editor);
///   var (ok, text) = await session.WaitForResponseAsync(30);
/// </summary>
internal sealed class AskSession : IDisposable
{
    public AiProvider Provider { get; }
    public CdpTabSession? Tab { get; private set; }
    public CdpClient? Cdp => _legacyCdp ?? Tab?.Cdp;
    public int Port { get; private set; } = 9222;

    public AskSession(AiProvider provider, int port = 9222)
    {
        Provider = provider;
        Port = port;
    }

    /// <summary>Wrap an existing CdpClient (for gradual migration from legacy EnsureCdpConnection).</summary>
    public AskSession(AiProvider provider, CdpClient existingCdp)
    {
        Provider = provider;
        _legacyCdp = existingCdp;
    }
    private CdpClient? _legacyCdp;

    // ══ Step 1: Connect ══

    public bool Connect(IntPtr? promptHwnd = null)
    {
        Tab = CdpTabManager.CreateScoped("ask", Provider.Name, Provider.Host, Port, promptHwnd);
        if (Tab == null) { Console.Error.WriteLine($"[ASK] Failed to create tab session for {Provider.Name}"); return false; }
        Console.WriteLine($"[ASK] Session: {Provider.Name} → {Provider.Host} (tab={Tab.IsScoped})");
        return true;
    }

    // ══ Step 2: Navigate ══

    public async Task<bool> NavigateAsync(bool newSession = false)
    {
        if (Cdp == null) return false;
        await Tab!.WaitForReadyAsync();
        if (await Cdp.IsHiddenAsync()) await Cdp.DispatchVisibilityAsync();

        if (newSession || !await Tab.ValidateAsync())
        {
            await Cdp.NavigateAsync($"https://{Provider.Host}");
            await Tab.WaitForReadyAsync(15000);
        }
        return true;
    }

    // ══ Step 3: Find Editor ══

    public async Task<string?> FindEditorAsync(int timeoutSec = 15)
    {
        if (Cdp == null) return null;
        return await Cdp.WaitForEditorAsync(Provider.EditorSelectors, timeoutSec);
    }

    // ══ Step 4: Type ══

    public async Task<bool> TypeAsync(string editorSelector, string text)
    {
        if (Cdp == null) return false;
        await Cdp.ClearEditorAsync(editorSelector);
        return await Cdp.InsertContentEditableAsync(editorSelector, text);
    }

    // ══ Step 5: Send ══

    public async Task<string> SendAsync(string editorSelector)
    {
        if (Cdp == null) return "NO_CDP";
        return await Cdp.SendPromptAsync(editorSelector);
    }

    // ══ Step 6: Wait for Response ══

    public async Task<(bool ok, string text)> WaitForResponseAsync(int timeoutSec = 30)
    {
        if (Cdp == null) return (false, "");
        int baseCount = await Cdp.GetResponseCountAsync();
        return await Cdp.PollStreamingResponseAsync(Provider.Name, baseCount, timeoutSec);
    }

    // ══ Convenience: Full Ask (Type + Send + Wait) ══

    public async Task<(bool ok, string text)> AskAsync(string editorSelector, string question, int timeoutSec = 30)
    {
        if (!await TypeAsync(editorSelector, question)) return (false, "TYPE_FAILED");
        var verify = await Cdp!.GetEditorContentAsync(editorSelector);
        Console.WriteLine($"[ASK] Editor: [{verify[..Math.Min(verify.Length, 60)]}]");

        await SendAsync(editorSelector);
        return await WaitForResponseAsync(timeoutSec);
    }

    // ══ Utilities ══

    public async Task<bool> IsStreamingAsync()
        => Cdp != null && await Cdp.IsStreamingAsync(Provider.Name);

    public async Task<string?> GetLastResponseAsync()
        => Cdp == null ? null : await Cdp.GetLastResponseTextAsync();

    public async Task<int> GetResponseCountAsync()
        => Cdp == null ? 0 : await Cdp.GetResponseCountAsync();

    public void Dispose() => Tab?.Dispose();
}
