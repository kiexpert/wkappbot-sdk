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

// partial class: wkappbot ask <ai> "question" ??one-command AI Q&A via WebBot
internal partial class Program
{
    // ???? Image Paste ????

    /// <summary>
    /// Paste image into chat editor via CDP.
    /// Tier 1: Synthetic ClipboardEvent with File blob (fully focusless, no real clipboard).
    /// Tier 2: Win32 clipboard + CDP Ctrl+V (needs focus + visible viewport).
    /// Returns true if image was pasted (upload indicator detected).
    /// </summary>
    static async Task<bool> PasteImageViaCdp(CdpClient cdp, string imagePath, string editorSelector)
    {
        var bytes = File.ReadAllBytes(imagePath);
        var base64 = Convert.ToBase64String(bytes);
        var ext = Path.GetExtension(imagePath).ToLowerInvariant();
        var mimeType = ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".bmp" => "image/bmp",
            _ => "image/png"
        };
        var fileName = Path.GetFileName(imagePath);
        Console.WriteLine($"[ASK] Pasting image: {fileName} ({bytes.Length / 1024}KB, {mimeType})");

        // Tier 1: Synthetic paste event with File blob (focusless ??no real clipboard needed)
        var pasteJs = $$"""
            (async () => {
                try {
                    var b64 = '{{base64}}';
                    var bin = atob(b64);
                    var arr = new Uint8Array(bin.length);
                    for (var i = 0; i < bin.length; i++) arr[i] = bin.charCodeAt(i);
                    var blob = new Blob([arr], {type: '{{mimeType}}'});
                    var file = new File([blob], '{{fileName}}', {type: '{{mimeType}}'});
                    var dt = new DataTransfer();
                    dt.items.add(file);
                    var el = document.querySelector('{{editorSelector}}');
                    if (!el) return 'NO_EDITOR';
                    el.focus();
                    var evt = new ClipboardEvent('paste', {clipboardData: dt, bubbles: true, cancelable: true});
                    el.dispatchEvent(evt);
                    return 'PASTED';
                } catch(e) { return 'ERR:' + e.message; }
            })()
            """;
        var result = await cdp.EvalAsync(pasteJs);
        Console.WriteLine($"[ASK] Synthetic paste: {result}");

        if (result == "PASTED")
        {
            // Wait for upload indicator (ChatGPT/Gemini show a thumbnail or progress)
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(500);
                var indicator = await cdp.CheckAttachmentAsync();

                if (indicator != "NONE")
                {
                    Console.WriteLine($"[ASK] Image attached: {indicator} ({(i + 1) * 500}ms)");
                    return true;
                }
            }
            // Even if no indicator detected, the paste event was dispatched ??proceed optimistically
            Console.WriteLine("[ASK] No upload indicator, but paste dispatched ??proceeding");
            return true;
        }

        // Tier 2: Win32 clipboard + CDP Ctrl+V
        Console.WriteLine("[ASK] Synthetic paste failed, trying clipboard + Ctrl+V...");
        System.Windows.Forms.IDataObject? clipBackup = null;
        try
        {
            // Set image to clipboard on STA thread (backup + restore)
            var clipboardSet = false;
            var staThread = new Thread(() =>
            {
                try
                {
                    // Backup current clipboard contents
                    try { clipBackup = System.Windows.Forms.Clipboard.GetDataObject(); } catch { }
                    using var img = System.Drawing.Image.FromFile(imagePath);
                    System.Windows.Forms.Clipboard.SetImage(img);
                    clipboardSet = true;
                }
                catch (Exception ex)
                {
                    LogWarning("ASK", "Clipboard.SetImage failed", ex);
                }
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join(3000);

            if (!clipboardSet)
                return false;

            // Focus editor
            await cdp.FocusAsync(editorSelector);
            await Task.Delay(200);

            // Ctrl+V via CDP
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyDown", ["key"] = "v", ["code"] = "KeyV",
                ["windowsVirtualKeyCode"] = 86, ["modifiers"] = 2 // Ctrl
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyUp", ["key"] = "v", ["code"] = "KeyV",
                ["windowsVirtualKeyCode"] = 86, ["modifiers"] = 2
            });
            await Task.Delay(1000);

            // Check for attachment
            var attached = await cdp.CheckAttachmentAsync();
            Console.WriteLine($"[ASK] Clipboard paste: {attached}");

            // Restore original clipboard contents
            RestoreClipboard(clipBackup);

            return attached != "NONE";
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "Clipboard paste failed", ex);
            RestoreClipboard(clipBackup);
            return false;
        }
    }

    /// <summary>Restore clipboard contents from backup (best effort, STA thread).</summary>
    static void RestoreClipboard(System.Windows.Forms.IDataObject? backup)
    {
        if (backup == null) return;
        try
        {
            var t = new Thread(() =>
            {
                try
                {
                    if (backup.GetDataPresent(System.Windows.Forms.DataFormats.Text))
                        System.Windows.Forms.Clipboard.SetText(
                            backup.GetData(System.Windows.Forms.DataFormats.Text) as string ?? "");
                    else if (backup.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                        System.Windows.Forms.Clipboard.SetDataObject(backup, true);
                    Console.WriteLine("[ASK] Clipboard restored");
                }
                catch { Console.WriteLine("[ASK] Clipboard restore failed (non-critical)"); }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join(2000);
        }
        catch { }
    }

    /// <summary>Wait for image upload to complete (progress indicator gone).</summary>
    static async Task WaitForImageUpload(CdpClient cdp, int maxWaitMs = 15000)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            await Task.Delay(500);
            // Auto-dismiss copyright/terms dialogs (Gemini image upload warning)
            await DismissDialogIfPresent(cdp);
            if (!await cdp.IsUploadingAsync())
            {
                // Final check for delayed dialogs
                await DismissDialogIfPresent(cdp);
                Console.WriteLine($"[ASK] Image upload complete ({sw.ElapsedMilliseconds}ms)");
                return;
            }
        }
        Console.WriteLine("[ASK] Image upload wait timeout — proceeding");
    }

    /// <summary>
    /// Auto-dismiss copyright/terms/warning dialogs that appear after image upload.
    /// Targets Gemini's mat-dialog and generic [role="dialog"] modals.
    /// Clicks the primary/confirm/OK button to dismiss.
    /// </summary>
    static async Task DismissDialogIfPresent(CdpClient cdp)
    {
        try
        {
            var dismissed = await cdp.DismissDialogAsync();
            if (dismissed.StartsWith("DISMISSED"))
                Console.WriteLine($"[ASK] Auto-dismissed dialog: {dismissed}");
        }
        catch { /* best effort */ }
    }

    static readonly HashSet<string> ImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        { ".png", ".jpg", ".jpeg", ".gif", ".webp", ".bmp" };

    /// <summary>Attach multiple files: images via clipboard paste, other files via hidden file input.</summary>
    static async Task AttachFilesViaCdp(CdpClient cdp, List<string> files, string editorSelector, IntPtr originalUserFg = default)
    {
        foreach (var filePath in files)
        {
            var ext = Path.GetExtension(filePath);
            if (ImageExtensions.Contains(ext))
            {
                // Image: clipboard paste (Tier 1 synthetic ??Tier 2 Win32 clipboard + Ctrl+V)
                var imgOk = await PasteImageViaCdp(cdp, filePath, editorSelector);
                if (imgOk) await WaitForImageUpload(cdp);
                else Console.WriteLine($"[ASK] Image paste failed: {Path.GetFileName(filePath)}");
            }
            else
            {
                // Non-image file: use hidden file input element
                var fileOk = await AttachFileViaFileInput(cdp, filePath, originalUserFg);
                if (fileOk) await WaitForImageUpload(cdp); // reuse upload wait
                else Console.WriteLine($"[ASK] File attach failed: {Path.GetFileName(filePath)}");
            }
            await Task.Delay(500); // settle between attachments
            await DismissDialogIfPresent(cdp); // catch late-appearing dialogs
        }
    }
}
