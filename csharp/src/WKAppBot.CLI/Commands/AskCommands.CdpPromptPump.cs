using System.Collections.Concurrent;

namespace WKAppBot.CLI;

internal sealed class CdpPromptPump : IDisposable
{
    private sealed class PageState
    {
        public required string Scope { get; init; }
        public required string PageKey { get; init; }
        public required string EditorSelector { get; init; }
        public required WKAppBot.WebBot.CdpClient Cdp { get; init; }
        public bool Locked { get; set; }
        public List<string> QueuedChunks { get; } = new();
        public object SyncRoot { get; } = new();
        public int WatchdogGen { get; set; }
    }

    private static readonly ConcurrentDictionary<string, PageState> _pages = new(StringComparer.OrdinalIgnoreCase);

    public CdpPromptPump(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static string BuildPageKey(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector)
    {
        var targetId = string.IsNullOrWhiteSpace(cdp.TargetId) ? "no-target" : cdp.TargetId;
        return $"{scope}::{targetId}::{editorSelector}";
    }

    private PageState GetOrCreateState(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector)
    {
        var pageKey = BuildPageKey(scope, cdp, editorSelector);
        return _pages.GetOrAdd(pageKey, _ => new PageState
        {
            Scope = scope,
            PageKey = pageKey,
            EditorSelector = editorSelector,
            Cdp = cdp,
        });
    }

    public async Task<bool> AppendAndQueueAsync(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector, string text, string separator = "\n")
    {
        var state = GetOrCreateState(scope, cdp, editorSelector);
        lock (state.SyncRoot)
        {
            if (state.Locked)
            {
                state.QueuedChunks.Add(text);
                Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: queued-in-lock ({text.Length} chars)");
                return true;
            }
        }

        var ok = await cdp.AppendContentEditableAsync(editorSelector, text, separator);
        if (!ok) return false;

        // Snapshot editor state for submit watchdog
        var dumpPreview = text.Length > 120 ? text[..120] + "..." : text;
        var dumpTarget = cdp.TargetId ?? "no-target";
        var dumpAt = DateTime.UtcNow;

        var armed = await cdp.ArmPromptPumpAsync(editorSelector, idleMs: 1000);
        Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: {(armed ? "armed" : "arm-failed")}");

        // CDP_SIGNAL poller: JS pump sets readyToSend when JS KeyboardEvent won't work (ProseMirror).
        // Poll every 300ms for up to 8s (was 4s); timeout/exception -> log and continue (not silent).
        _ = Task.Run(async () =>
        {
            try
            {
                for (int i = 0; i < 26; i++) // 26 × 300ms = ~8s (doubled from 4s to survive Chrome throttle)
                {
                    await Task.Delay(300);
                    bool ready;
                    try { ready = await cdp.CheckPumpReadyAsync(editorSelector); }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: CDP_SIGNAL poll timeout ({ex.GetType().Name}) -- continuing");
                        continue; // retry next tick instead of aborting
                    }
                    if (!ready) continue;
                    Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: CDP_SIGNAL -> firing CDP Enter");
                    await cdp.SendPromptAsync(editorSelector);
                    return;
                }
            }
            catch { }
        });

        // Submit watchdog: check at 7s and 5s after LAST append.
        // Delay increased 3->7s to give CDP_SIGNAL poller (now 8s window) room to recover from throttle.
        int myWatchdog;
        lock (state.SyncRoot) { myWatchdog = ++state.WatchdogGen; }
        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(7000); // increased from 3s -- pump poller window is now 8s
                lock (state.SyncRoot) { if (state.WatchdogGen != myWatchdog) return; }
                var first = await cdp.GetTextLengthAsync(editorSelector);
                if (first <= 0) return; // already submitted

                // Recovery attempt: the usual stuck cause is a promo popover
                // (Claude CoWork banner, Gemini copyright, etc.) eating the
                // submit click. Dismiss any such overlay and re-fire the
                // send before giving up. Cheap when nothing's open -- the
                // helper returns 'NONE'.
                // Also reset JS pump isSending flag so the interval can retry if CDP Enter misses.
                string? recoveryDismissed = null;
                try { recoveryDismissed = await cdp.DismissDialogAsync(); } catch { }
                if (recoveryDismissed != null && recoveryDismissed != "NONE")
                    Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: watchdog recovery -- dismissed {recoveryDismissed}");
                try { await cdp.ResetPumpSendingAsync(editorSelector); } catch { } // unblock JS pump interval
                try { await cdp.SendPromptAsync(editorSelector); } catch { }

                await Task.Delay(5000); // increased from 3s -- give extra room for slow AI tabs
                lock (state.SyncRoot) { if (state.WatchdogGen != myWatchdog) return; }
                var second = await cdp.GetTextLengthAsync(editorSelector);
                if (second <= 0) return; // submitted between checks (recovery worked)
                if (second < first) return; // content decreasing = submission in progress

                // Second recovery: explicit button click + re-send after another JS unblock
                Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: watchdog 2nd recovery -- first={first} second={second}");
                try { await cdp.DismissDialogAsync(); } catch { }
                try { await cdp.ResetPumpSendingAsync(editorSelector); } catch { }
                try { await cdp.SendPromptAsync(editorSelector); } catch { }

                await Task.Delay(5000);
                lock (state.SyncRoot) { if (state.WatchdogGen != myWatchdog) return; }
                var third = await cdp.GetTextLengthAsync(editorSelector);
                if (third <= 0) return;
                if (third < second) return;
                var msg = $"[PUMP] Submit watchdog: editor still has {third} chars after 17s (2x recovery failed)! scope={scope} target={dumpTarget} text=[{dumpPreview}]";
                Console.Error.WriteLine(msg);
                Program.AutoRegisterBug(msg);
            }
            catch { }
        });

        return armed;
    }

    public async Task<bool> BeginAttachmentLockAsync(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector)
    {
        var state = GetOrCreateState(scope, cdp, editorSelector);
        lock (state.SyncRoot)
        {
            state.Locked = true;
        }

        var ok = await cdp.SetPromptPumpLockAsync(editorSelector, locked: true);
        Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: {(ok ? "lock-on" : "lock-on-failed")}");
        return ok;
    }

    public async Task<bool> CompleteAttachmentLockAsync(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector, bool immediateFlush = true, string separator = "\n\n")
    {
        var state = GetOrCreateState(scope, cdp, editorSelector);
        string? merged = null;
        lock (state.SyncRoot)
        {
            if (state.QueuedChunks.Count > 0)
            {
                merged = string.Join(separator, state.QueuedChunks);
                state.QueuedChunks.Clear();
            }
        }

        if (!string.IsNullOrWhiteSpace(merged))
        {
            var appended = await cdp.AppendContentEditableAsync(editorSelector, merged, separator);
            if (!appended)
            {
                Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: append-after-lock failed");
                return false;
            }
        }

        lock (state.SyncRoot)
        {
            state.Locked = false;
        }

        var unlockOk = await cdp.SetPromptPumpLockAsync(editorSelector, locked: false);
        if (!unlockOk) return false;

        if (immediateFlush)
        {
            var flush = await cdp.FlushPromptPumpNowAsync(editorSelector);
            Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: flush-after-lock={flush}");
            return flush != "NO_EDITOR";
        }

        var armed = await cdp.ArmPromptPumpAsync(editorSelector, idleMs: 1000);
        Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: re-armed={armed}");
        return armed;
    }

    public async Task<bool> AbortAttachmentLockAsync(string scope, WKAppBot.WebBot.CdpClient cdp, string editorSelector, bool keepQueued = true)
    {
        var state = GetOrCreateState(scope, cdp, editorSelector);
        lock (state.SyncRoot)
        {
            state.Locked = false;
            if (!keepQueued) state.QueuedChunks.Clear();
        }

        var ok = await cdp.SetPromptPumpLockAsync(editorSelector, locked: false);
        Console.Error.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: abort-lock={(ok ? "ok" : "failed")}");
        return ok;
    }

    public static async Task<bool> DropPendingForPageAsync(string? pageKey, WKAppBot.WebBot.CdpClient cdp, string editorSelector, bool clearEditor = true)
    {
        if (!string.IsNullOrWhiteSpace(pageKey) && _pages.TryGetValue(pageKey, out var state))
        {
            lock (state.SyncRoot)
            {
                state.Locked = false;
                state.QueuedChunks.Clear();
            }
        }

        var result = await cdp.CancelPromptPumpAsync(editorSelector, clearEditor);
        return result.StartsWith("CANCELLED", StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        // No background resources. The page-scoped singleton pump lives inside the target page.
    }
}
