using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>
    /// Synthetic paste of an image file via clipboard DataTransfer event.
    /// Returns: "PASTED", "ERR:message", or null.
    /// </summary>
    public async Task<string?> PasteImageAsync(string editorSelector, string mimeType = "image/png")
    {
        var escaped = Esc(editorSelector);
        return await EvalAsync(
            $"(()=>{{try{{var el=document.querySelector('{escaped}');if(!el)return 'NO_EDITOR';" +
            $"var dt=new DataTransfer();var file=new File([''],'{mimeType.Split('/').Last()}',{{type:'{mimeType}'}});" +
            "dt.items.add(file);var ev=new ClipboardEvent('paste',{clipboardData:dt,bubbles:true,cancelable:true});" +
            "el.dispatchEvent(ev);return 'PASTED'}catch(e){return 'ERR:'+e.message}})()");
    }

    /// <summary>
    /// Comprehensive file attachment check (GPT/Gemini/Claude + duplicate detection).
    /// Returns: "YES", "DUPLICATE", "INPUT_HAS_FILES", "NO"
    /// </summary>
    public async Task<string> CheckFileAttachedExtendedAsync()
    {
        return await EvalAsync("""
            (() => {
                var bt = (document.body?.innerText || '');
                if (/already\s*uploaded/i.test(bt) || /duplicate/i.test(bt)) return 'DUPLICATE';
                var sels = ['[data-testid="file-thumbnail"]','[data-testid*="attachment"]',
                    '[class*="attachment"]','[class*="file-upload"]','[class*="uploaded-file"]',
                    'img[src*="blob:"]','[class*="file-chip"]','[class*="upload-chip"]',
                    '[class*="file-container"]','[class*="upload-container"]','[class*="inline-file"]',
                    '[class*="file-preview"]','uploader-thumbnail','mat-chip'];
                for (var s of sels) { if (document.querySelector(s)) return 'YES'; }
                var inputs = document.querySelectorAll('input[type="file"]');
                for (var inp of inputs) { if (inp.files?.length > 0) return 'INPUT_HAS_FILES'; }
                return 'NO';
            })()
            """) ?? "NO";
    }

    /// <summary>
    /// Set session state in sessionStorage + localStorage + window (triple persist).
    /// Used for persona/loop state tracking across page reloads.
    /// </summary>
    public async Task SetSessionStateAsync(string key, string value)
    {
        var k = Esc(key); var v = Esc(value);
        await EvalAsync(
            $"(()=>{{try{{sessionStorage.setItem('{k}','{v}');localStorage.setItem('{k}','{v}');window['{k}']='{v}'}}catch(e){{}}return 'OK'}})()");
    }

    /// <summary>Check session state from window ??sessionStorage ??localStorage (triple check).</summary>
    public async Task<bool> HasSessionStateAsync(string key, string expectedValue)
    {
        var k = Esc(key); var v = Esc(expectedValue);
        var result = await EvalAsync(
            $"(()=>{{try{{var k='{k}';if(window[k]==='{v}')return '1';" +
            $"if(sessionStorage.getItem(k)==='{v}')return '1';" +
            $"if(localStorage.getItem(k)==='{v}')return '1'}}catch(e){{}}return '0'}})()") ?? "0";
        return result == "1";
    }

    /// <summary>
    /// Poll streaming response with comprehensive checks.
    /// Returns (success, finalText) ??integrates response count, stop button, stability, rate limit.
    /// </summary>
    /// <summary>Called during streaming with latest response text. For cross-prompting.</summary>
    public Action<string>? OnStreamingChunk { get; set; }

    public async Task<(bool ok, string text)> PollStreamingResponseAsync(
        string provider, int baseResponseCount, int timeoutSec = 30, bool detectBlank = true)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string lastText = "";
        int stableCount = 0;
        int blankCount = 0;

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(1000);

            // Read response with blank detection
            var text = await GetLastResponseTextAsync(baseResponseCount, blankDetect: detectBlank) ?? "";

            // Blank page detection
            if (text == "\x01BLANK") { blankCount++; if (blankCount >= 3) return (false, "BLANK_PAGE"); continue; }
            if (string.IsNullOrWhiteSpace(text)) continue;

            // Auto-dismiss dialogs (Gemini copyright warning etc.)
            await DismissDialogAsync();

            // Notify chunk callback (cross-prompting)
            if (text.Length > lastText.Length)
            {
                var deltaText = text.Length > lastText.Length ? text[lastText.Length..] : text;
                OnStreamingChunk?.Invoke(text);
                var ctx = StreamingChunkContext;
                var seq = Interlocked.Increment(ref _streamChunkSeq);
                OnStreamingChunkEvent?.Invoke(new PromptStreamEvent
                {
                    Provider = string.IsNullOrWhiteSpace(ctx?.Provider) ? provider : ctx!.Provider,
                    GameId = ctx?.GameId,
                    QuestionId = ctx?.QuestionId,
                    RunId = ctx?.RunId,
                    TargetId = ctx?.TargetId ?? TargetId,
                    PageKey = ctx?.PageKey,
                    EditorSelector = ctx?.EditorSelector,
                    Status = "RUNNING",
                    ChunkSeq = seq,
                    ChunkText = text,
                    FullTextSoFar = text,
                    DeltaText = deltaText,
                    OperationContext = OperationContext,
                    IsFinal = false,
                });
            }

            // Check streaming done
            if (!await IsStreamingAsync(provider) && !await IsStopButtonVisibleAsync())
            {
                await Task.Delay(300);
                var final = await GetLastResponseTextAsync(baseResponseCount) ?? text;
                return (true, final.Trim());
            }

            // Stability check
            if (text == lastText) { stableCount++; if (stableCount >= 3) return (true, text.Trim()); }
            else { lastText = text; stableCount = 0; }
        }

        return string.IsNullOrEmpty(lastText) ? (false, "") : (true, lastText.Trim());
    }

    // -- Internal --
    /// <summary>
    /// Ensure Chrome window is not minimized before DOM-dependent operations.
    /// ProseMirror, contenteditable, and some React components need rendering.
    /// Uses SW_SHOWNOACTIVATE (4) ??no focus steal.
    /// Also emulates active tab via CDP for background tab rendering.
    /// </summary>
    public void EnsureChromeNotIconic()
    {
        var hwnd = GetChromeWindowHandle();
        if (hwnd != IntPtr.Zero && IsIconic(hwnd))
        {
            ShowWindowNative(hwnd, 4); // SW_SHOWNOACTIVATE ??visible, no focus steal
            Thread.Sleep(100);
        }
        // CDP: emulate active tab (triggers rendering even for background tabs)
        try { EmulateActiveTabAsync().GetAwaiter().GetResult(); } catch { }
    }

    public static string Esc(string s) => s.Replace("\\", "\\\\").Replace("'", "\\'");
}
