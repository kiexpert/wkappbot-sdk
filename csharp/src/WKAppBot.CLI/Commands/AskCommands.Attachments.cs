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

    /// <summary>
    /// Attach a non-image file via CDP DOM.setFileInputFiles on hidden file input.
    /// Works for txt, cs, log, pdf, etc. Fully focusless ??no clipboard involved.
    /// Tier 1: DOM.setFileInputFiles + change event
    /// Tier 2: Synthetic drop event with DataTransfer (for React apps that ignore file input)
    /// </summary>
    static async Task<bool> AttachFileViaFileInput(CdpClient cdp, string filePath, IntPtr originalUserFg = default)
    {
        var fileName = Path.GetFileName(filePath);
        var absPath = Path.GetFullPath(filePath);
        var fileSize = new FileInfo(absPath).Length;
        Console.WriteLine($"[ASK] Attaching file: {fileName} ({fileSize / 1024}KB)");

        try
        {
            // ???? Tier 1: DOM.setFileInputFiles on pre-existing hidden input (no click needed) ????
            // Some pages have input[type=file] in DOM already ??try before triggering any click.
            Console.WriteLine("[ASK] Tier 1: trying pre-existing hidden file input...");
            if (await TrySetFileInputFiles(cdp, absPath, fileName))
                return true;

            // ???? Tier 0.5: CDP File Chooser Interception + click (fully focusless) ????
            // IMPORTANT: Enable interception FIRST, then click ??otherwise OS dialog opens before we can intercept.
            Console.WriteLine("[ASK] Tier 0.5: CDP chooser intercept (intercept ON ??click ??intercept file)...");
            var chooserOk = await cdp.SetFileViaChooserAsync(absPath, timeoutMs: 6000);
            if (chooserOk)
            {
                Console.WriteLine($"[ASK] File attached via chooser: {fileName}");
                await Task.Delay(1500);
                return true;
            }

            // ???? Tier 1.5: Re-check file inputs after menu click (dynamic input created by Gemini/React) ????
            // Tier 0.5 clicked the upload menu item ??the page may have dynamically created input[type=file].
            Console.WriteLine("[ASK] Tier 1.5: re-check dynamic file inputs after menu click...");
            await Task.Delay(800);
            if (await TrySetFileInputFiles(cdp, absPath, fileName))
                return true;

            // ???? Tier 0: Native OS file dialog via UIA (STEALS FOCUS ??last resort) ????
            await cdp.DisableFileChooserInterception();
            Console.WriteLine("[ASK] All focusless tiers failed ??falling back to native file dialog (will steal focus)...");
            var uiaOk = await TryAttachViaFileDialog(cdp, absPath, originalUserFg);
            if (uiaOk)
            {
                Console.WriteLine($"[ASK] File attached via UIA dialog: {fileName}");
                await Task.Delay(2000);
                return true;
            }

            // ???? Tier 2: Synthetic drop event with DataTransfer ????
            Console.WriteLine("[ASK] All focusless tiers failed, trying drag-drop...");
            var bytes = File.ReadAllBytes(absPath);
            var base64 = Convert.ToBase64String(bytes);
            var ext = Path.GetExtension(absPath).ToLowerInvariant();
            var mimeType = ext switch
            {
                ".txt" or ".log" or ".md" => "text/plain",
                ".cs" or ".js" or ".ts" or ".py" or ".java" => "text/plain",
                ".json" => "application/json",
                ".yaml" or ".yml" => "text/yaml",
                ".xml" => "application/xml",
                ".html" or ".htm" => "text/html",
                ".csv" => "text/csv",
                ".pdf" => "application/pdf",
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                ".ogg" => "audio/ogg",
                ".m4a" => "audio/mp4",
                ".mp4" => "video/mp4",
                _ => "application/octet-stream"
            };

            var dropResult = await cdp.EvalAsync($$"""
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
                        // Try drop on editor area
                        var target = document.querySelector('#prompt-textarea')
                                  || document.querySelector('[contenteditable="true"]')
                                  || document.querySelector('[role="textbox"]');
                        if (!target) return 'NO_TARGET';
                        target.dispatchEvent(new DragEvent('drop', {dataTransfer: dt, bubbles: true, cancelable: true}));
                        return 'DROPPED';
                    } catch(e) { return 'ERR:' + e.message; }
                })()
                """);
            Console.WriteLine($"[ASK] Drop event: {dropResult}");

            if (dropResult == "DROPPED")
            {
                await Task.Delay(1500);
                if (await CheckFileAttached(cdp))
                {
                    Console.WriteLine($"[ASK] File attached via drop: {fileName}");
                    return true;
                }
            }

            // ???? Tier 3: If text file, just append content inline as code block ????
            if (fileSize < 50_000 && IsTextFile(ext))
            {
                Console.WriteLine($"[ASK] Falling back to inline text for: {fileName}");
                return false; // caller will handle ??file content in question text would be too big
            }

            Console.WriteLine($"[ASK] All attachment methods failed for: {fileName}");
            return false;
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "File attach error", ex);
            return false;
        }
    }

    /// <summary>Check if any file attachment indicator is visible in the page.</summary>
    // Tier 1 helper: set file on any pre-existing input[type=file] without triggering a click
    static async Task<bool> TrySetFileInputFiles(CdpClient cdp, string absPath, string fileName)
    {
        try
        {
            var docResult = await cdp.SendAsync("DOM.getDocument", new JsonObject());
            var rootNodeId = docResult?["root"]?["nodeId"]?.GetValue<int>() ?? 0;
            if (rootNodeId == 0) return false;

            var queryResult = await cdp.SendAsync("DOM.querySelectorAll", new JsonObject
            {
                ["nodeId"] = rootNodeId,
                ["selector"] = "input[type=\"file\"]"
            });
            var nodeIds = queryResult?["nodeIds"]?.AsArray();
            if (nodeIds == null || nodeIds.Count == 0) return false;

            Console.WriteLine($"[ASK] Tier 1: {nodeIds.Count} hidden file input(s) found");
            foreach (var nodeIdVal in nodeIds)
            {
                var nodeId = nodeIdVal?.GetValue<int>() ?? 0;
                if (nodeId == 0) continue;
                try
                {
                    await cdp.SendAsync("DOM.setFileInputFiles", new JsonObject
                    {
                        ["nodeId"] = nodeId,
                        ["files"] = new JsonArray { absPath.Replace('\\', '/') }
                    });
                    // Fire React/Angular change events
                    await cdp.EvalAsync("""
                        (() => {
                            var inputs = document.querySelectorAll('input[type="file"]');
                            for (var inp of inputs) {
                                if (inp.files && inp.files.length > 0) {
                                    inp.dispatchEvent(new Event('change', {bubbles:true}));
                                    inp.dispatchEvent(new Event('input', {bubbles:true}));
                                    return 'FIRED:' + inp.files[0].name;
                                }
                            }
                        })()
                        """);
                    await Task.Delay(2000);
                    if (await CheckFileAttached(cdp))
                    {
                        Console.WriteLine($"[ASK] Tier 1: file attached via hidden input: {fileName}");
                        return true;
                    }
                }
                catch { /* try next */ }
            }
        }
        catch { }
        return false;
    }

    static async Task<bool> CheckFileAttached(CdpClient cdp)
    {
        var attached = await cdp.CheckFileAttachedExtendedAsync();
        if (attached == "DUPLICATE")
        {
            Console.WriteLine("[ASK] Duplicate upload notice detected -> treating as attached");
            await DismissDuplicateUploadNotice(cdp);
        }
        return attached != "NO";
    }

    static async Task DismissDuplicateUploadNotice(CdpClient cdp)
    {
        try
        {
            for (int i = 0; i < 4; i++)
            {
                var result = await cdp.EvalAsync("""
                    (() => {
                        var bodyText = (document.body && (document.body.innerText || document.body.textContent) || '');
                        var dup = /(이미\s*이\s*파일|already\s*uploaded)/i.test(bodyText);
                        if (!dup) return 'NO_DUP';

                        var clicked = 0;
                        var closeText = /(닫기|확인|close|ok|x)/i;
                        var nodes = Array.from(document.querySelectorAll('button,[role="button"],[aria-label]'));
                        for (var n of nodes) {
                            var label = (n.getAttribute('aria-label') || '') + ' ' + (n.textContent || '');
                            if (!closeText.test(label)) continue;
                            var cs = window.getComputedStyle(n);
                            if (cs.display === 'none' || cs.visibility === 'hidden') continue;
                            try { n.click(); clicked++; } catch {}
                        }

                        // If text still visible and no obvious close button, return marker for ESC fallback.
                        return clicked > 0 ? ('CLICKED:' + clicked) : 'NEED_ESC';
                    })()
                    """) ?? "NO_DUP";

                if (result == "NO_DUP")
                    return;

                if (result.StartsWith("CLICKED:", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"[ASK] Duplicate notice dismissed ({result})");
                    await Task.Delay(200);
                    continue;
                }

                // ESC fallback for modal/dialog toasts that don't expose text close buttons.
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyDown", ["key"] = "Escape", ["code"] = "Escape",
                    ["windowsVirtualKeyCode"] = 27, ["nativeVirtualKeyCode"] = 27
                });
                await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                {
                    ["type"] = "keyUp", ["key"] = "Escape", ["code"] = "Escape",
                    ["windowsVirtualKeyCode"] = 27, ["nativeVirtualKeyCode"] = 27
                });
                Console.WriteLine("[ASK] Duplicate notice dismiss fallback: ESC");
                await Task.Delay(250);
            }
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "Duplicate notice dismiss failed", ex);
        }
    }

    /// <summary>
    /// Open Gemini upload menu ??click "???뵬 ??낆쨮?? ??OS file dialog appears ??UIA type path + click Open.
    /// Returns true if file was successfully attached via the native dialog.
    /// originalUserFg: the user's foreground window BEFORE the ask command started ??restored after dialog closes.
    /// </summary>
    static async Task<bool> TryAttachViaFileDialog(CdpClient cdp, string absPath, IntPtr originalUserFg = default)
    {
        try
        {
            // Check if file open dialog is already open
            IntPtr existingDialog = FindFileOpenDialog();
            if (existingDialog == IntPtr.Zero)
            {
                // Open the upload menu with a TRUSTED click (Input.dispatchMouseEvent)
                // Chrome requires trusted user gestures to open <input type="file"> dialogs.
                // CDP Runtime.evaluate clicks are NOT trusted ??dialog won't open.
                var btnRect = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label*="???뵬 ??낆쨮??]')
                               || document.querySelector('button[aria-label*="Upload"]')
                               || document.querySelector('button[aria-label*="Attach"]');
                        if (!btn) return 'NO_BTN';
                        var r = btn.getBoundingClientRect();
                        return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2);
                    })()
                """);
                if (btnRect == "NO_BTN") return false;

                var parts = btnRect!.Split(',');
                if (parts.Length != 2) return false;
                var bx = int.Parse(parts[0]);
                var by = int.Parse(parts[1]);

                // [STEP 1] Trusted click on upload button ??capture prevFg just before CDP call
                var prevFg1 = NativeMethods.GetForegroundWindow();
                Console.WriteLine($"[ASK:FOCUS] pre-upload-btn-click fg={prevFg1:X8}");
                await CdpTrustedClick(cdp, bx, by);
                Console.WriteLine($"[ASK] UIA dialog: upload btn trusted click at ({bx},{by})");
                await Task.Delay(1500); // give menu time to animate open
                LogRestoreFocus(prevFg1, "trusted-click-upload-btn");

                // Now find and click the "???뵬 ??낆쨮?? menu item with trusted gesture
                var menuRect = await cdp.EvalAsync("""
                    (() => {
                        var items = document.querySelectorAll('[role=menuitem]');
                        for (var item of items) {
                            var t = (item.textContent || '').trim();
                            if (t === '???뵬 ??낆쨮?? || t === 'Upload file' || t === 'Upload') {
                                var r = item.getBoundingClientRect();
                                return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + t;
                            }
                        }
                        return 'NO_ITEM';
                    })()
                """);
                Console.WriteLine($"[ASK] UIA dialog: menu={menuRect}");
                if (menuRect == "NO_ITEM")
                {
                    // Menu didn't appear ??maybe OS dialog already opened from previous tier's menu click
                    var existingD = FindFileOpenDialog();
                    if (existingD != IntPtr.Zero)
                    {
                        Console.WriteLine($"[ASK] UIA dialog: OS dialog already open hwnd={existingD:X} (from prev tier)");
                        existingDialog = existingD; // skip menu click, go straight to STEP 3
                    }
                    else return false;
                }
                else
                {
                    var menuParts = menuRect!.Split(':');
                    var coords = menuParts[0].Split(',');
                    var mx = int.Parse(coords[0]);
                    var my = int.Parse(coords[1]);

                    // [STEP 2] Trusted click on menu item ??capture prevFg just before CDP call
                    var prevFg2 = NativeMethods.GetForegroundWindow();
                    Console.WriteLine($"[ASK:FOCUS] pre-menu-item-click fg={prevFg2:X8}");
                    await CdpTrustedClick(cdp, mx, my);
                    Console.WriteLine($"[ASK] UIA dialog: menu item trusted click at ({mx},{my})");
                    await Task.Delay(200);
                    LogRestoreFocus(prevFg2, "trusted-click-menu-item");
                }
            }
            else
            {
                Console.WriteLine($"[ASK] UIA dialog: reusing existing dialog hwnd={existingDialog:X}");
            }

            // [STEP 3] Capture prevFg before dialog wait ??OS file dialog may steal focus when it appears
            var prevFg3 = NativeMethods.GetForegroundWindow();
            Console.WriteLine($"[ASK:FOCUS] pre-dialog-wait fg={prevFg3:X8}");

            // Wait for OS file dialog to appear (#32770 with title "??용┛"/"Open" or ComboBoxEx32 child)
            IntPtr dialogHwnd = IntPtr.Zero;
            for (int i = 0; i < 20; i++) // 4 seconds max
            {
                await Task.Delay(200);
                dialogHwnd = FindFileOpenDialog();
                if (dialogHwnd != IntPtr.Zero) break;
            }

            if (dialogHwnd == IntPtr.Zero)
            {
                Console.WriteLine("[ASK] UIA dialog: no file dialog found");
                return false;
            }
            Console.WriteLine($"[ASK] UIA dialog: found hwnd={dialogHwnd:X}");
            LogRestoreFocus(prevFg3, "file-dialog-appeared");

            // Step 4: Find the filename edit via Win32 (ComboBoxEx32 ??ComboBox ??Edit chain)
            // Standard Windows file dialog structure: Dialog ??ComboBoxEx32(cid=1148) ??ComboBox ??Edit
            var comboEx = NativeMethods.FindWindowExW(dialogHwnd, IntPtr.Zero, "ComboBoxEx32", null);
            IntPtr editHwnd = IntPtr.Zero;
            if (comboEx != IntPtr.Zero)
            {
                var combo = NativeMethods.FindWindowExW(comboEx, IntPtr.Zero, "ComboBox", null);
                if (combo != IntPtr.Zero)
                    editHwnd = NativeMethods.FindWindowExW(combo, IntPtr.Zero, "Edit", null);
                if (editHwnd == IntPtr.Zero)
                    editHwnd = NativeMethods.FindWindowExW(comboEx, IntPtr.Zero, "Edit", null);
            }
            // Fallback: search for any Edit with control id 1148
            if (editHwnd == IntPtr.Zero)
            {
                NativeMethods.EnumChildWindows(dialogHwnd, (h, _) =>
                {
                    var cidBuf = new StringBuilder(32);
                    NativeMethods.GetClassNameW(h, cidBuf, cidBuf.Capacity);
                    if (cidBuf.ToString() == "Edit")
                    {
                        editHwnd = h;
                        return false;
                    }
                    return true;
                }, IntPtr.Zero);
            }

            if (editHwnd == IntPtr.Zero)
            {
                Console.WriteLine("[ASK] UIA dialog: filename edit not found");
                NativeMethods.SendMessageW(dialogHwnd, 0x0010 /*WM_CLOSE*/, IntPtr.Zero, IntPtr.Zero);
                return false;
            }

            // Set the filename via WM_SETTEXT (Win32 ??most reliable for file dialogs)
            NativeMethods.SendMessageW(editHwnd, 0x000C /*WM_SETTEXT*/, IntPtr.Zero, absPath);
            Console.WriteLine($"[ASK] UIA dialog: path set via WM_SETTEXT");

            await Task.Delay(300);

            // Step 5: Click "??용┛" button ??find by control ID 1 (standard Open button)
            // GetDlgItem equivalent: find the button with control ID 1
            // [STEP 4] Before clicking Open button ??capture prevFg fresh
            var prevFg4 = NativeMethods.GetForegroundWindow();
            Console.WriteLine($"[ASK:FOCUS] pre-open-btn-click fg={prevFg4:X8}");
            var openBtnHwnd = NativeMethods.FindWindowExW(dialogHwnd, IntPtr.Zero, "Button", null);
            // The first Button child is usually "??용┛(O)" / "Open". Click via BM_CLICK.
            if (openBtnHwnd != IntPtr.Zero)
            {
                NativeMethods.SendMessageW(openBtnHwnd, 0x00F5 /*BM_CLICK*/, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("[ASK] UIA dialog: Open BM_CLICK sent");
            }
            else
            {
                // Fallback: press Enter on dialog
                Console.WriteLine("[ASK] UIA dialog: Open button not found, posting Enter");
                NativeMethods.PostMessageW(dialogHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D, IntPtr.Zero);
            }
            LogRestoreFocus(prevFg4, "open-btn-click");

            // Step 6: Wait for dialog to close and file to appear in Gemini
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(300);
                if (!NativeMethods.IsWindow(dialogHwnd))
                {
                    Console.WriteLine("[ASK] UIA dialog: dialog closed OK");
                    await Task.Delay(500);
                    // [STEP 5] Restore to the ORIGINAL user foreground (before entire ask command)
                    // ??not prevFg4 (which was the file dialog hwnd, now dead)
                    var restoreFg = originalUserFg != IntPtr.Zero ? originalUserFg : prevFg4;
                    Console.WriteLine($"[ASK:FOCUS] post-dialog: restoring to original fg={restoreFg:X8}");
                    NativeMethods.SmartSetForegroundWindow(restoreFg);
                    await Task.Delay(1000);
                    return true;
                }
            }

            Console.WriteLine("[ASK] UIA dialog: dialog didn't close");
            return false;
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "UIA dialog error", ex);
            return false;
        }
    }
}
