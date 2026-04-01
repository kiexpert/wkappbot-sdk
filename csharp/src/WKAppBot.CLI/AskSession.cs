using WKAppBot.WebBot;

namespace WKAppBot.CLI;

/// <summary>
/// Declarative AI provider configuration: selectors, host, and quirks.
/// No JS here, only data that drives the common AskSession flow.
/// </summary>
internal record AiProvider(
    string Name,                // "gemini", "claude", "gpt"
    string Host,                // "gemini.google.com"
    string[] EditorSelectors,   // contenteditable / textarea variants
    string ResponseSelector,    // last-response element (for :last-child text)
    string[] StopSelectors,     // stop/cancel button variants
    string[] SendSelectors,     // send/submit button variants (fallback after Enter)
    string UserMessageSelector, // user turn counter selector
    string StreamingIndicator,  // streaming-in-progress indicator (attr or element)
    string? LimitSelector       // rate limit / quota banner (null = no detection)
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
        StopSelectors: ["button[aria-label*='Stop']", "mat-icon[fonticon='stop_circle']"],
        SendSelectors: ["button[aria-label*='Send']", "button.send-button"],
        UserMessageSelector: ".query-content",
        StreamingIndicator: "mat-icon[fonticon='stop_circle']",
        LimitSelector: null
    );

    public static readonly AiProvider Claude = new(
        Name: "claude", Host: "claude.ai",
        EditorSelectors: [
            ".ProseMirror[contenteditable='true']",
            "[contenteditable='true']",
            "div[contenteditable='plaintext-only']"
        ],
        ResponseSelector: "[data-is-streaming]",
        StopSelectors: ["button[aria-label*='Stop']"],
        SendSelectors: ["button[aria-label*='Send']", "button[type='submit']"],
        UserMessageSelector: "[data-testid='user-message']",
        StreamingIndicator: "[data-is-streaming='true']",
        LimitSelector: ".limit-banner, [class*='quota'], [class*='limit']"
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
        ],
        SendSelectors: ["button[data-testid='send-button']", "button[aria-label*='Send']"],
        UserMessageSelector: "[data-message-author-role='user']",
        StreamingIndicator: "[data-testid='stop-button']",
        LimitSelector: ".rate-limit-banner, [class*='rate-limit']"
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
/// Shared AI ask session for the common provider flow.
/// Eliminates per-provider reimplementation of connect -> type -> send -> poll.
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
    public sealed class AskQuestionState
    {
        public string Key { get; init; } = "";
        public string Provider { get; init; } = "";
        public string? ProviderTag { get; init; }
        public string? GameId { get; init; }
        public string? QuestionId { get; init; }
        public string? RunId { get; init; }
        public string? PageKey { get; init; }
        public string? EditorSelector { get; init; }
        public string Status { get; set; } = "CREATED";
        public DateTime CreatedUtc { get; init; } = DateTime.UtcNow;
        public DateTime LastUpdateUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LastChunkUtc { get; set; }
        public int ChunkCount { get; set; }
        public long LastChunkSeq { get; set; }
        public bool IsFinal { get; set; }
        public string? LastSendResult { get; set; }
        public string? SendMethod { get; set; }
        public string? SendKeyChord { get; set; }
        public int? SendAttempt { get; set; }
        public DateTime? QueuedAtUtc { get; set; }
        public string LastDeltaText { get; set; } = "";
        public string LastFullText { get; set; } = "";
    }

    public sealed class AskIdentity
    {
        public string? GameId { get; init; }
        public string? QuestionId { get; init; }
        public string? RunId { get; init; }
        public string? ProviderTag { get; init; }
    }

    public AiProvider Provider { get; }
    public CdpTabSession? Tab { get; private set; }
    public CdpClient? Cdp => _legacyCdp ?? Tab?.Cdp;
    public int Port { get; private set; } = 9222;
    public AskIdentity Identity { get; private set; } = new();
    public Action<AskQuestionState>? OnQuestionStateChanged { get; set; }
    public IReadOnlyDictionary<string, AskQuestionState> Questions => _questions;

    readonly Dictionary<string, AskQuestionState> _questions = new(StringComparer.OrdinalIgnoreCase);
    string? _currentQuestionKey;
    Action<CdpClient.PromptStreamEvent>? _priorStreamingChunkHandler;
    bool _streamBridgeAttached;

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
        AttachStreamingBridge();
    }
    private CdpClient? _legacyCdp;

    public void SetIdentity(string? gameId = null, string? questionId = null, string? runId = null, string? providerTag = null)
    {
        Identity = new AskIdentity
        {
            GameId = gameId,
            QuestionId = questionId,
            RunId = runId,
            ProviderTag = providerTag ?? Provider.Name,
        };
    }

    public void BindStreamingContext(string editorSelector, string? pageKey = null)
    {
        var resolvedPageKey = pageKey ?? $"{Identity.ProviderTag ?? Provider.Name}:{editorSelector}";
        RegisterQuestion(editorSelector, resolvedPageKey);
        Cdp?.SetStreamingChunkContext(
            provider: Identity.ProviderTag ?? Provider.Name,
            gameId: Identity.GameId,
            questionId: Identity.QuestionId,
            runId: Identity.RunId,
            editorSelector: editorSelector,
            pageKey: resolvedPageKey);
    }

    public void AttachStreamingBridge()
    {
        if (_streamBridgeAttached || Cdp == null)
            return;

        _priorStreamingChunkHandler = Cdp.OnStreamingChunkEvent;
        Cdp.OnStreamingChunkEvent = evt =>
        {
            _priorStreamingChunkHandler?.Invoke(evt);
            TrackChunkEvent(evt);
        };
        _streamBridgeAttached = true;
    }

    public AskQuestionState RegisterQuestion(string editorSelector, string? pageKey = null)
    {
        var normalizedPageKey = pageKey ?? $"{Identity.ProviderTag ?? Provider.Name}:{editorSelector}";
        var key = BuildQuestionKey(normalizedPageKey);
        if (!_questions.TryGetValue(key, out var state))
        {
            state = new AskQuestionState
            {
                Key = key,
                Provider = Provider.Name,
                ProviderTag = Identity.ProviderTag ?? Provider.Name,
                GameId = Identity.GameId,
                QuestionId = Identity.QuestionId,
                RunId = Identity.RunId,
                PageKey = normalizedPageKey,
                EditorSelector = editorSelector,
            };
            _questions[key] = state;
        }
        else
        {
            state.LastUpdateUtc = DateTime.UtcNow;
            state.Status = "BOUND";
        }

        _currentQuestionKey = key;
        EmitQuestionState(state);
        return state;
    }

    public void MarkQueued(string? sendResult = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = "QUEUED";
        state.LastSendResult = sendResult ?? state.LastSendResult;
        var parsed = ParseDispatchResult(sendResult);
        state.SendMethod = parsed.method ?? state.SendMethod;
        state.SendKeyChord = parsed.keyChord ?? state.SendKeyChord;
        state.SendAttempt = parsed.attempt ?? state.SendAttempt;
        state.QueuedAtUtc = DateTime.UtcNow;
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkRunning()
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = "RUNNING";
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkDone(string? finalText = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = "DONE";
        state.IsFinal = true;
        if (!string.IsNullOrEmpty(finalText))
            state.LastFullText = finalText;
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkTimedOut(string? partialText = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = "TIMED_OUT";
        if (!string.IsNullOrEmpty(partialText))
            state.LastFullText = partialText;
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkRetrying(string? reason = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = string.IsNullOrWhiteSpace(reason) ? "RETRYING" : $"RETRYING:{reason}";
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkCancelled(string? reason = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = string.IsNullOrWhiteSpace(reason) ? "CANCELLED" : $"CANCELLED:{reason}";
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public void MarkFailed(string? reason = null, string? detailText = null)
    {
        if (!TryGetCurrentQuestion(out var state))
            return;
        state.Status = string.IsNullOrWhiteSpace(reason) ? "FAILED" : $"FAILED:{reason}";
        if (!string.IsNullOrEmpty(detailText))
            state.LastFullText = detailText;
        state.LastUpdateUtc = DateTime.UtcNow;
        EmitQuestionState(state);
    }

    public bool TryGetQuestion(string key, out AskQuestionState? state)
    {
        if (_questions.TryGetValue(key, out var found))
        {
            state = found;
            return true;
        }
        state = null;
        return false;
    }

    public bool SelectQuestion(string key)
    {
        if (!_questions.ContainsKey(key))
            return false;
        _currentQuestionKey = key;
        return true;
    }

    public string ResolveQuestionKey(
        string? pageKey = null,
        string? questionId = null,
        string? runId = null)
        => BuildQuestionKey(pageKey, questionId, runId);

    public void TrackChunkEvent(CdpClient.PromptStreamEvent evt)
    {
        var key = BuildQuestionKey(evt.PageKey, evt.QuestionId, evt.RunId);
        if (!_questions.TryGetValue(key, out var state))
        {
            state = new AskQuestionState
            {
                Key = key,
                Provider = Provider.Name,
                ProviderTag = evt.Provider,
                GameId = evt.GameId,
                QuestionId = evt.QuestionId,
                RunId = evt.RunId,
                PageKey = evt.PageKey,
                EditorSelector = evt.EditorSelector,
            };
            _questions[key] = state;
        }

        _currentQuestionKey = key;
        state.Status = evt.IsFinal ? "DONE" : "RUNNING";
        state.LastUpdateUtc = DateTime.UtcNow;
        state.LastChunkUtc = DateTime.UtcNow;
        state.ChunkCount++;
        state.LastChunkSeq = evt.ChunkSeq;
        state.IsFinal = evt.IsFinal;
        state.LastDeltaText = evt.DeltaText ?? evt.ChunkText ?? "";
        state.LastFullText = evt.FullTextSoFar ?? state.LastFullText;
        EmitQuestionState(state);
    }

    string BuildQuestionKey(string? pageKey = null, string? questionId = null, string? runId = null)
    {
        var q = questionId ?? Identity.QuestionId ?? "q0";
        var r = runId ?? Identity.RunId ?? "run0";
        var p = pageKey ?? _currentQuestionKey ?? $"{Identity.ProviderTag ?? Provider.Name}:page";
        return $"{Identity.ProviderTag ?? Provider.Name}|{q}|{r}|{p}";
    }

    bool TryGetCurrentQuestion(out AskQuestionState state)
    {
        if (_currentQuestionKey != null && _questions.TryGetValue(_currentQuestionKey, out state!))
            return true;

        if (_questions.Count > 0)
        {
            state = _questions.Values.Last();
            _currentQuestionKey = state.Key;
            return true;
        }

        state = null!;
        return false;
    }

    void EmitQuestionState(AskQuestionState state)
    {
        OnQuestionStateChanged?.Invoke(state);
    }

    static (string? method, string? keyChord, int? attempt) ParseDispatchResult(string? sendResult)
    {
        if (string.IsNullOrWhiteSpace(sendResult))
            return (null, null, null);

        string? method = null;
        string? keyChord = null;
        int? attempt = null;
        var value = sendResult.Trim();

        if (value.StartsWith("JS_CLICK", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("BTN_CLICK", StringComparison.OrdinalIgnoreCase))
            method = "button-click";
        else if (value.StartsWith("UIA_INVOKE", StringComparison.OrdinalIgnoreCase))
            method = "uia-invoke";
        else if (value.StartsWith("FORM_SUBMIT", StringComparison.OrdinalIgnoreCase) ||
                 value.StartsWith("REQUEST_SUBMIT", StringComparison.OrdinalIgnoreCase))
            method = "form-submit";
        else if (value.StartsWith("CDP_ENTER", StringComparison.OrdinalIgnoreCase) ||
                 value.StartsWith("KEY_ENTER", StringComparison.OrdinalIgnoreCase))
        {
            method = "key-dispatch";
            keyChord = "Enter";
        }
        else if (value.StartsWith("SENT(", StringComparison.OrdinalIgnoreCase) ||
                 value.StartsWith("RESPONSE_STARTED(", StringComparison.OrdinalIgnoreCase) ||
                 value.StartsWith("RESPONSE_IN_PROGRESS(", StringComparison.OrdinalIgnoreCase))
        {
            method = "provider-default";
        }
        else if (value.StartsWith("FORCED(", StringComparison.OrdinalIgnoreCase))
            method = "forced-retry";

        var attemptMarker = "attempt=";
        var idx = value.IndexOf(attemptMarker, StringComparison.OrdinalIgnoreCase);
        if (idx >= 0)
        {
            idx += attemptMarker.Length;
            var end = idx;
            while (end < value.Length && char.IsDigit(value[end]))
                end++;
            if (end > idx && int.TryParse(value[idx..end], out var parsedAttempt))
                attempt = parsedAttempt;
        }

        return (method, keyChord, attempt);
    }

    // Step 1: Connect

    public bool Connect(IntPtr? promptHwnd = null)
    {
        Tab = CdpTabManager.CreateScoped("ask", Provider.Name, Provider.Host, Port, promptHwnd);
        if (Tab == null) { Console.Error.WriteLine($"[ASK] Failed to create tab session for {Provider.Name}"); return false; }
        Console.WriteLine($"[ASK] Session: {Provider.Name} ??{Provider.Host} (tab={Tab.IsScoped})");
        return true;
    }

    // Step 2: Navigate

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

    // Step 3: Find Editor

    public async Task<string?> FindEditorAsync(int timeoutSec = 15)
    {
        if (Cdp == null) return null;
        return await Cdp.WaitForEditorAsync(Provider.EditorSelectors, timeoutSec);
    }

    // Step 4: Type

    public async Task<bool> TypeAsync(string editorSelector, string text)
    {
        if (Cdp == null) return false;
        await Cdp.ClearEditorAsync(editorSelector);
        return await Cdp.InsertContentEditableAsync(editorSelector, text);
    }

    // Step 5: Send

    public async Task<string> SendAsync(string editorSelector)
    {
        if (Cdp == null) return "NO_CDP";
        RegisterQuestion(editorSelector);
        var result = await Cdp.SendPromptAsync(editorSelector);
        MarkQueued(result);
        return result;
    }

    // Step 6: Wait for Response

    public async Task<(bool ok, string text)> WaitForResponseAsync(int timeoutSec = 30)
    {
        if (Cdp == null) return (false, "");
        int baseCount = await Cdp.GetResponseCountAsync();
        return await Cdp.PollStreamingResponseAsync(Provider.Name, baseCount, timeoutSec);
    }

    // Convenience: Full Ask (Type + Send + Wait)

    public async Task<(bool ok, string text)> AskAsync(string editorSelector, string question, int timeoutSec = 30)
    {
        // Pre-type hook (persona injection etc.)
        if (OnBeforeType != null)
            try { await OnBeforeType(this, editorSelector); } catch (Exception ex) { Console.WriteLine($"[ASK] OnBeforeType hook error: {ex.Message}"); }

        if (!await TypeAsync(editorSelector, question)) return (false, "TYPE_FAILED");
        var verify = await Cdp!.GetEditorContentAsync(editorSelector);
        Console.WriteLine($"[ASK] Editor: [{verify[..Math.Min(verify.Length, 60)]}]");

        await SendAsync(editorSelector);

        // Post-send hook
        if (OnAfterSend != null)
            try { await OnAfterSend(this); } catch (Exception ex) { Console.WriteLine($"[ASK] OnAfterSend hook error: {ex.Message}"); }

        var (ok, text) = await WaitForResponseAsync(timeoutSec);

        // Response done hook (post-processing)
        if (ok && OnResponseDone != null)
            try { text = await OnResponseDone(this, text); } catch (Exception ex) { Console.WriteLine($"[ASK] OnResponseDone hook error: {ex.Message}"); }

        return (ok, text);
    }

    // Provider hooks set by the caller for provider-specific behavior.

    /// <summary>Called before typing question ??e.g. persona injection.</summary>
    public Func<AskSession, string, Task>? OnBeforeType { get; set; }

    /// <summary>Called after send ??e.g. send verification.</summary>
    public Func<AskSession, Task>? OnAfterSend { get; set; }

    /// <summary>Called when streaming completes ??e.g. response post-processing.</summary>
    public Func<AskSession, string, Task<string>>? OnResponseDone { get; set; }

    // Provider-aware utilities.

    public async Task<bool> IsStreamingAsync()
        => Cdp != null && await Cdp.QueryExistsAsync(Provider.StreamingIndicator);

    public async Task<string?> GetLastResponseAsync()
        => Cdp == null ? null : await Cdp.EvalAsync(
            $"document.querySelector('{Provider.ResponseSelector}:last-child')?.textContent?.trim() ?? ''");

    public async Task<int> GetResponseCountAsync()
        => Cdp == null ? 0 : await Cdp.QueryCountAsync(Provider.ResponseSelector);

    public async Task<int> GetUserMessageCountAsync()
        => Cdp == null ? 0 : await Cdp.QueryCountAsync(Provider.UserMessageSelector);

    public async Task<bool> IsStopVisibleAsync()
    {
        if (Cdp == null) return false;
        // Check visibility (not just DOM existence) ??hidden/disabled buttons should not block
        var js = string.Join(",", Provider.StopSelectors.Select(s => "'" + s.Replace("'", "\\'") + "'"));
        var result = await Cdp.EvalAsync(
            "(()=>{var sels=[" + js + "];" +
            "for(var s of sels){var el=document.querySelector(s);" +
            "if(el&&el.offsetParent!==null&&!el.disabled&&el.offsetWidth>0)return 'yes'}" +
            "return 'no'})()");
        return result == "yes";
    }

    public async Task<bool> ClickStopAsync()
    {
        if (Cdp == null) return false;
        foreach (var sel in Provider.StopSelectors)
            if (await Cdp.QueryExistsAsync(sel)) { await Cdp.JsClickAsync(sel); return true; }
        return false;
    }

    public async Task<bool> CancelCurrentQuestionAsync(string reason = "USER_STOP")
    {
        if (!TryGetCurrentQuestion(out _))
            return false;
        var clicked = await ClickStopAsync();
        if (clicked)
            MarkCancelled(reason);
        return clicked;
    }

    public async Task<bool> CancelQuestionAsync(string key, string reason = "USER_STOP")
    {
        var previous = _currentQuestionKey;
        if (!SelectQuestion(key))
            return false;
        try
        {
            return await CancelCurrentQuestionAsync(reason);
        }
        finally
        {
            _currentQuestionKey = previous;
        }
    }

    public async Task<bool> CancelQuestionAsync(
        string? pageKey,
        string? questionId,
        string? runId,
        string reason = "USER_STOP")
    {
        var key = ResolveQuestionKey(pageKey, questionId, runId);
        return await CancelQuestionAsync(key, reason);
    }

    public async Task<bool> ClickSendAsync()
    {
        if (Cdp == null) return false;
        foreach (var sel in Provider.SendSelectors)
            if (await Cdp.QueryExistsAsync(sel)) { await Cdp.JsClickAsync(sel); return true; }
        return false;
    }

    public async Task<bool> IsRateLimitedAsync()
    {
        if (Cdp == null || Provider.LimitSelector == null) return false;
        return await Cdp.QueryExistsAsync(Provider.LimitSelector);
    }

    public void Dispose() => Tab?.Dispose();
}

