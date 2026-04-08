using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WKAppBot.WebBot;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

// partial class: File attachment via file input / native OS dialog
internal partial class Program
{
    /// <summary>
    /// Attach a non-image file via CDP DOM.setFileInputFiles on hidden file input.
    /// Works for txt, cs, log, pdf, etc. Fully focusless — no clipboard involved.
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
            // Tier 1: DOM.setFileInputFiles on pre-existing hidden input (no click needed)
            Console.WriteLine("[ASK] Tier 1: trying pre-existing hidden file input...");
            if (await TrySetFileInputFiles(cdp, absPath, fileName))
                return true;

            // Tier 0.5: CDP File Chooser Interception + click (fully focusless)
            Console.WriteLine("[ASK] Tier 0.5: CDP chooser intercept (intercept ON → click → intercept file)...");
            var chooserOk = await cdp.SetFileViaChooserAsync(absPath, timeoutMs: 6000);
            if (chooserOk)
            {
                Console.WriteLine($"[ASK] File attached via chooser: {fileName}");
                await Task.Delay(1500);
                return true;
            }

            // Tier 1.5: Re-check file inputs after menu click (dynamic input created by Gemini/React)
            Console.WriteLine("[ASK] Tier 1.5: re-check dynamic file inputs after menu click...");
            await Task.Delay(800);
            if (await TrySetFileInputFiles(cdp, absPath, fileName))
                return true;

            // Tier 0: Native OS file dialog via UIA (STEALS FOCUS — last resort)
            // Temporarily disable focus-theft monitoring: the HWND change here is expected and intentional.
            await cdp.DisableFileChooserInterception();
            Console.WriteLine("[ASK] All focusless tiers failed → falling back to native file dialog (will steal focus)...");
            var prevFocusMonitor = cdp.EnableFocusTheftMonitoring;
            cdp.EnableFocusTheftMonitoring = false;
            bool uiaOk;
            try { uiaOk = await TryAttachViaFileDialog(cdp, absPath, originalUserFg); }
            finally { cdp.EnableFocusTheftMonitoring = prevFocusMonitor; }
            if (uiaOk)
            {
                Console.WriteLine($"[ASK] File attached via UIA dialog: {fileName}");
                await Task.Delay(2000);
                return true;
            }

            // Tier 2: Synthetic drop event with DataTransfer
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

            // Tier 3: If text file, just append content inline as code block
            if (fileSize < 50_000 && IsTextFile(ext))
            {
                Console.WriteLine($"[ASK] Falling back to inline text for: {fileName}");
                return false; // caller will handle
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
    /// Open Gemini upload menu → click "파일 업로드" → OS file dialog appears → UIA type path + click Open.
    /// Returns true if file was successfully attached via the native dialog.
    /// originalUserFg: the user's foreground window BEFORE the ask command started — restored after dialog closes.
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
                var btnRect = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label*="파일 업로드"]')
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

                // [STEP 1] Trusted click on upload button
                var prevFg1 = NativeMethods.GetForegroundWindow();
                Console.WriteLine($"[ASK:FOCUS] pre-upload-btn-click fg={prevFg1:X8}");
                await CdpTrustedClick(cdp, bx, by);
                Console.WriteLine($"[ASK] UIA dialog: upload btn trusted click at ({bx},{by})");
                await Task.Delay(1500);
                LogRestoreFocus(prevFg1, "trusted-click-upload-btn", cdp);

                // Now find and click the "파일 업로드" menu item with trusted gesture
                var menuRect = await cdp.EvalAsync("""
                    (() => {
                        var items = document.querySelectorAll('[role=menuitem]');
                        for (var item of items) {
                            var t = (item.textContent || '').trim();
                            if (t === '파일 업로드' || t === 'Upload file' || t === 'Upload') {
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
                    var existingD = FindFileOpenDialog();
                    if (existingD != IntPtr.Zero)
                    {
                        Console.WriteLine($"[ASK] UIA dialog: OS dialog already open hwnd={existingD:X} (from prev tier)");
                        existingDialog = existingD;
                    }
                    else return false;
                }
                else
                {
                    var menuParts = menuRect!.Split(':');
                    var coords = menuParts[0].Split(',');
                    var mx = int.Parse(coords[0]);
                    var my = int.Parse(coords[1]);

                    // [STEP 2] Trusted click on menu item
                    var prevFg2 = NativeMethods.GetForegroundWindow();
                    Console.WriteLine($"[ASK:FOCUS] pre-menu-item-click fg={prevFg2:X8}");
                    await CdpTrustedClick(cdp, mx, my);
                    Console.WriteLine($"[ASK] UIA dialog: menu item trusted click at ({mx},{my})");
                    await Task.Delay(200);
                    LogRestoreFocus(prevFg2, "trusted-click-menu-item", cdp);
                }
            }
            else
            {
                Console.WriteLine($"[ASK] UIA dialog: reusing existing dialog hwnd={existingDialog:X}");
            }

            // [STEP 3] Capture prevFg before dialog wait
            var prevFg3 = NativeMethods.GetForegroundWindow();
            Console.WriteLine($"[ASK:FOCUS] pre-dialog-wait fg={prevFg3:X8}");

            // Wait for OS file dialog to appear
            IntPtr dialogHwnd = IntPtr.Zero;
            for (int i = 0; i < 20; i++)
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
            LogRestoreFocus(prevFg3, "file-dialog-appeared", cdp);

            // Step 4: Find the filename edit via Win32
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

            NativeMethods.SendMessageW(editHwnd, 0x000C /*WM_SETTEXT*/, IntPtr.Zero, absPath);
            Console.WriteLine($"[ASK] UIA dialog: path set via WM_SETTEXT");
            await Task.Delay(300);

            // Step 5: Click "열기" button
            var prevFg4 = NativeMethods.GetForegroundWindow();
            Console.WriteLine($"[ASK:FOCUS] pre-open-btn-click fg={prevFg4:X8}");
            var openBtnHwnd = NativeMethods.FindWindowExW(dialogHwnd, IntPtr.Zero, "Button", null);
            if (openBtnHwnd != IntPtr.Zero)
            {
                NativeMethods.SendMessageW(openBtnHwnd, 0x00F5 /*BM_CLICK*/, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("[ASK] UIA dialog: Open BM_CLICK sent");
            }
            else
            {
                Console.WriteLine("[ASK] UIA dialog: Open button not found, posting Enter");
                NativeMethods.PostMessageW(dialogHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D, IntPtr.Zero);
            }
            LogRestoreFocus(prevFg4, "open-btn-click", cdp);

            // Step 6: Wait for dialog to close
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(300);
                if (!NativeMethods.IsWindow(dialogHwnd))
                {
                    Console.WriteLine("[ASK] UIA dialog: dialog closed OK");
                    await Task.Delay(500);
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
