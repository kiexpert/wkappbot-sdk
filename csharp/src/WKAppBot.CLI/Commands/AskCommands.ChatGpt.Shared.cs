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
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// 최종 답변 표시: Chrome의 아이콘화를 리스토어하여 사용자에게 답변 페이지를 보여준다.
    /// ask 프로세스 자체는 아이콘화 상태로 진행 (CDP는 미니마이즈에서도 정상 작동).
    /// 최종 답변 추출 후에만 호출하여 결과를 시각적으로 확인할 수 있게 한다.
    /// </summary>
    static void ShowChromeAnswer(CdpClient cdp)
    {
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero) return;

        bool isIconic  = NativeMethods.IsIconic(chromeHwnd);
        bool isVisible = NativeMethods.IsWindowVisible(chromeHwnd);
        if (!isIconic && isVisible) return; // already normal-visible

        var title = WKAppBot.Win32.Window.WindowFinder.GetWindowText(chromeHwnd);
        Console.WriteLine($"[ASK] Restoring Chrome window (iconic={isIconic}, visible={isVisible}): \"{title}\"");
        // SW_RESTORE(9): restores minimized or hidden window, may steal focus (recovery use)
        NativeMethods.ShowWindow(chromeHwnd, 9); // SW_RESTORE
    }

    static async Task TryRecoverChatGptTabAsync(CdpClient cdp, string reason)
    {
        Console.WriteLine($"[ASK] Recovery: {reason} -> SW_SHOWNOACTIVATE (focusless restore)");
        ShowChromeAnswer(cdp);
        cdp.RestoreChromeNoActivate();
        await cdp.EmulateActiveTabAsync(); // unthrottle renderer
        await Task.Delay(200);
    }

    // ★★ UIA Send Button ★★
    // Tier 2 fallback: find and invoke the send button via UIA (completely focusless).
    // Searches Chrome/ChatGPT windows for buttons matching send-related names.
    static readonly string[] SendButtonNames = ["Send message", "Send", "Submit"];
    static readonly string[] StopButtonKeywords = ["Stop", "중지", "스트리밍 중지"];

    static async Task<bool> WaitWhileStopButtonVisible(CdpClient cdp, int maxWaitMs = 12000)
    {
        // Legacy overload — wrap in a generic AskSession for provider-aware stop detection.
        // Uses ChatGpt provider as default since this was originally GPT-only.
        using var session = new AskSession(AiProvider.ChatGpt, cdp);
        return await WaitWhileStopButtonVisible(session, maxWaitMs);
    }

    static async Task<bool> WaitWhileStopButtonVisible(AskSession session, int maxWaitMs = 12000)
    {
        var sw = Stopwatch.StartNew();
        bool headerPrinted = false;
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            if (!await session.IsStopVisibleAsync())
            {
                if (headerPrinted) Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
                return true;
            }

            if (!headerPrinted) { Console.Write("[STOP-BTN-WAIT]"); headerPrinted = true; }
            Console.Write("."); Console.Out.Flush();
            await Task.Delay(700);
        }

        Console.WriteLine($" {sw.Elapsed.TotalSeconds:F1}s");
        // Timed out — click stop to cancel ongoing generation, then wait briefly and proceed
        Console.WriteLine($"[ASK] {session.Provider.Name} stop still visible — clicking stop to cancel generation...");
        await session.ClickStopAsync();
        await Task.Delay(1500);
        return true;
    }

    static bool TryUiaInvokeSendButton()
    {
        try
        {
            using var automation = new UIA3Automation();
            // Try ChatGPT PWA first, then Chrome
            foreach (var grap in new[] { "*ChatGPT*", "*Gemini*Chrome*", "*chrome*" })
            {
                var resolved = GrapHelper.ResolveFullGrap(grap, automation);
                if (resolved == null || resolved.Value.error != null) continue;
                var (_, root, _) = resolved.Value;

                foreach (var name in SendButtonNames)
                {
                    var btn = GrapHelper.FindByNameOrAid(root, name);
                    if (btn == null) continue;

                    // Must be a Button with Invoke pattern
                    if (btn.ControlType != FlaUI.Core.Definitions.ControlType.Button) continue;
                    if (StopButtonKeywords.Any(k =>
                        !string.IsNullOrWhiteSpace(btn.Name) &&
                        btn.Name.Contains(k, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"[ASK] UIA: skip stop-like button \"{btn.Name}\"");
                        continue;
                    }
                    var invoke = btn.Patterns.Invoke.PatternOrDefault;
                    if (invoke == null) continue;

                    Console.WriteLine($"[ASK] UIA: found \"{btn.Name}\" ({btn.ControlType})");
                    invoke.Invoke();
                    Console.WriteLine("[ASK] UIA: Invoked!");
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "UIA send failed", ex);
        }
        return false;
    }

    // ★★ Tab Handoff: disabled ★★
    // BringToFront actually steals OS focus (not just Chrome-internal).
    // CDP eval/insertText work fine on background tabs, so handoff is unnecessary.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, CdpClient> _waitingTabs = new();

    /// <summary>
    /// Register this AI's tab as "waiting for response" — eligible for tab handoff.
    /// </summary>
    static void RegisterWaitingTab(string aiName, CdpClient cdp)
    {
        _waitingTabs[aiName] = cdp;
    }

    /// <summary>
    /// Unregister on completion.
    /// </summary>
    static void UnregisterWaitingTab(string aiName)
    {
        _waitingTabs.TryRemove(aiName, out _);
    }

    /// <summary>
    /// Tab handoff disabled — BringToFront steals OS focus.
    /// CDP works fine on background tabs, no handoff needed.
    /// </summary>
    static Task HandoffTabToPeer(string myName) => Task.CompletedTask;

    // ★★ Chrome Tab Lock ★★
    // Per-AI tab lock: each AI provider (gemini, gpt, claude) gets its own semaphore.
    // This allows Gemini, GPT, and Claude to send/receive in parallel → different tabs,
    // different CDP connections → no shared browser resource that needs serialization.
    // Only operations on the SAME AI tab are serialized (send vs concurrent send).
    // Acquired before prompt input, auto-released after 9s or when first byte arrives.
    static readonly System.Collections.Concurrent.ConcurrentDictionary<string, SemaphoreSlim>
        _tabSemaphores = new(StringComparer.OrdinalIgnoreCase);

    static SemaphoreSlim GetTabSemaphore(string aiName)
    {
        // Normalize key: "Gemini/persona" → "gemini", "Claude" → "claude", "ChatGPT" → "chatgpt"
        var key = aiName.Split('/')[0].ToLowerInvariant();
        return _tabSemaphores.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
    }

    sealed class ChromeTabLock : IDisposable
    {
        readonly string _aiName;
        readonly SemaphoreSlim _sem;
        readonly Timer _autoRelease;
        int _released;

        ChromeTabLock(string aiName, SemaphoreSlim sem)
        {
            _aiName = aiName;
            _sem = sem;
            // Auto-release after 60 seconds (safety net → prevents deadlock)
            // NOTE: must be > approval popup timeout (30s) + input time
            _autoRelease = new Timer(_ => Release("auto-60s"), null, 60000, Timeout.Infinite);
        }

        public static ChromeTabLock? Acquire(string aiName, int timeoutMs = 90000)
        {
            var sem = GetTabSemaphore(aiName);
            Console.WriteLine($"[ASK] Waiting for browser lock ({aiName})...");
            var sw = Stopwatch.StartNew();
            const int sliceMs = 1000;
            bool dotLineOpen = false;
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (sem.Wait(sliceMs))
                {
                    if (dotLineOpen) Console.WriteLine();
                    if (sw.ElapsedMilliseconds > 1500)
                        Console.WriteLine($"[ASK] Browser lock acquired after queue wait ({aiName}, {sw.ElapsedMilliseconds}ms)");
                    else
                        Console.WriteLine($"[ASK] Browser lock acquired ({aiName})");
                    return new ChromeTabLock(aiName, sem);
                }

                if (!dotLineOpen)
                {
                    Console.Write($"[ASK] Browser lock queued ({aiName}) ");
                    dotLineOpen = true;
                }
                Console.Write(".");
                Console.Out.Flush();

                if (sw.ElapsedMilliseconds % 5000 < sliceMs)
                {
                    Console.WriteLine($" {sw.ElapsedMilliseconds}ms");
                    dotLineOpen = false;
                }
            }

            if (dotLineOpen) Console.WriteLine();
            Console.WriteLine($"[ASK] Browser lock timeout ({aiName})");
            return null;
        }

        public void Release(string reason = "done")
        {
            if (Interlocked.CompareExchange(ref _released, 1, 0) != 0) return;
            _autoRelease.Dispose();
            try { _sem.Release(); } catch { }
            Console.WriteLine($"[ASK] Browser lock released ({_aiName}, {reason})");
        }

        public void Dispose() => Release("dispose");
    }
}
