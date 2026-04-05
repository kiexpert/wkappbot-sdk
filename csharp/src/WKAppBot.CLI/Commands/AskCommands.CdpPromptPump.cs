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
                Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: queued-in-lock ({text.Length} chars)");
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
        Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{dumpTarget}]: {(armed ? "armed" : "arm-failed")}");

        // Submit watchdog: 5s after LAST append, check if editor still has content
        // Two-phase: check at 3s and 5s — if content decreased between checks, submission is in progress
        int myWatchdog;
        lock (state.SyncRoot) { myWatchdog = ++state.WatchdogGen; }
        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(3000);
                lock (state.SyncRoot) { if (state.WatchdogGen != myWatchdog) return; }
                var first = await cdp.GetTextLengthAsync(editorSelector);
                if (first <= 0) return; // already submitted
                await Task.Delay(2000);
                lock (state.SyncRoot) { if (state.WatchdogGen != myWatchdog) return; }
                var second = await cdp.GetTextLengthAsync(editorSelector);
                if (second <= 0) return; // submitted between checks
                if (second < first) return; // content decreasing = submission in progress
                var msg = $"[PUMP] Submit watchdog: editor still has {second} chars after 5s! scope={scope} target={dumpTarget} text=[{dumpPreview}]";
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
        Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: {(ok ? "lock-on" : "lock-on-failed")}");
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
                Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: append-after-lock failed");
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
            Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: flush-after-lock={flush}");
            return flush != "NO_EDITOR";
        }

        var armed = await cdp.ArmPromptPumpAsync(editorSelector, idleMs: 1000);
        Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: re-armed={armed}");
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
        Console.WriteLine($"[ASK:PUMP:{Name}] {scope} [{cdp.TargetId ?? "no-target"}]: abort-lock={(ok ? "ok" : "failed")}");
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
