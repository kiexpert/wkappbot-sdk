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
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

// partial class: wkappbot ask <ai> "question" — one-command AI Q&A via WebBot
internal partial class Program
{
    static int AskCommand(string[] args)
    {
        if (args.Length < 2)
            return AskUsage();

        var ai = args[0].ToLowerInvariant();
        // Collect question (everything after AI name, excluding flags)
        // Auto-detect file paths: if arg is an existing file, attach it + insert [file:name] marker
        var questionParts = new List<string>();
        var attachFiles = new List<string>();
        bool slackReport = false;
        bool newTab = false;
        bool newSession = false;
        int timeoutSec = 30;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--slack")
                slackReport = true;
            else if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--new-session")
                newSession = true;
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else if (args[i] == "--image" && i + 1 < args.Length)
            {
                var imgArg = args[++i];
                attachFiles.Add(imgArg);
                questionParts.Add($"[file:{Path.GetFileName(imgArg)}]");
            }
            else if (!args[i].StartsWith("--") && File.Exists(args[i]))
            {
                attachFiles.Add(args[i]);
                questionParts.Add($"[file:{Path.GetFileName(args[i])}]"); // inline marker at arg position
            }
            else
                questionParts.Add(args[i]);
        }
        // Each argument gets its own line for readability
        var question = string.Join("\n", questionParts);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        // Validate file paths
        foreach (var f in attachFiles)
        {
            if (!File.Exists(f))
                return Error($"File not found: {f}");
        }
        if (attachFiles.Count > 0)
            Console.WriteLine($"[ASK] Attaching {attachFiles.Count} file(s): {string.Join(", ", attachFiles.Select(Path.GetFileName))}");

        return ai switch
        {
            "gemini" => AskGemini(question, slackReport, timeoutSec, newTab, attachFiles, newSession),
            "gpt" or "chatgpt" => AskChatGpt(question, slackReport, timeoutSec, newTab, attachFiles, newSession),
            "claude" => AskClaude(question, slackReport, timeoutSec, newTab, newSession),
            _ => Error($"Unknown AI: {ai} (use gemini, gpt, or claude)")
        };
    }

    static int AskUsage()
    {
        Console.WriteLine(@"
WKAppBot Ask — one-command AI Q&A via WebBot

Usage:
  wkappbot ask gemini ""question"" [files...] [--slack] [--timeout 30] [--new-tab] [--new-session]
  wkappbot ask gpt ""question"" [files...]   [--slack] [--timeout 30] [--new-tab] [--new-session]
  wkappbot ask claude ""question""            [--slack] [--timeout 30] [--new-tab] [--new-session]

Options:
  --slack         Report answer to Slack channel
  --timeout N     Max seconds to wait for response (default: 30)
  --new-tab       Open in a new tab (default: reuse existing tab)
  --new-session   Start fresh conversation in existing tab (navigate to new chat URL)

File attachment:
  Any argument that matches an existing file path is auto-attached.
  Images (png/jpg/gif/webp/bmp) → clipboard paste, other files → file input.
  Multiple files supported — attached in order before sending the question.

Examples:
  wkappbot ask gemini ""오늘 코스피 특징주 알려줘""
  wkappbot ask gpt ""이 패턴 분석해줘"" --slack
  wkappbot ask gpt ""이 UI 분석해줘"" screenshot.png
  wkappbot ask gpt ""코드 리뷰해줘"" main.cs test.log
  wkappbot ask gemini ""새 세션으로 질문"" --new-session
");
        return 1;
    }

    // ── GrapHelper tab routing: find & switch browser tab via UIA before CDP ──

    /// <summary>
    /// Find a browser tab by title substring and switch to it via UIA (focusless).
    /// Returns true if the tab was found and selected.
    /// This runs BEFORE CDP connection — ensures the right tab is active so CDP
    /// connects to the correct page without complex URL-matching logic.
    /// </summary>
    static bool EnsureTabViaGrap(string browserGrap, string tabPattern)
    {
        try
        {
            using var automation = new UIA3Automation();
            var resolved = GrapHelper.ResolveFullGrap(browserGrap, automation);
            if (resolved == null || resolved.Value.error != null)
            {
                Console.WriteLine($"[ASK] GrapHelper: browser not found ({browserGrap})");
                return false;
            }

            var (_, root, _) = resolved.Value;
            var tab = GrapHelper.FindByNameOrAid(root, tabPattern);
            if (tab == null)
            {
                Console.WriteLine($"[ASK] GrapHelper: tab \"{tabPattern}\" not found");
                return false;
            }

            if (!GrapHelper.IsTabItem(tab))
            {
                // Found something but it's not a TabItem — might be web content already visible
                Console.WriteLine($"[ASK] GrapHelper: \"{tabPattern}\" matched but not a TabItem (already active?)");
                return true;
            }

            // Zoom on tab before switching (focusless visual feedback)
            ClickZoomHelper? tabZoom = null;
            try
            {
                var tabRect = tab.BoundingRectangle;
                if (tabRect.Width > 0 && tabRect.Height > 0)
                {
                    var hwnd = resolved.Value.hwnd;
                    var screenRect = new System.Drawing.Rectangle(
                        (int)tabRect.X, (int)tabRect.Y, (int)tabRect.Width, (int)tabRect.Height);
                    tabZoom = ClickZoomHelper.BeginFromRect(screenRect, hwnd, "tab-select", tabPattern);
                }
            }
            catch { }

            var ok = GrapHelper.SwitchToTab(tab);
            if (ok)
                tabZoom?.ShowPass("tab activated");
            else
                tabZoom?.ShowFail("tab switch failed");
            tabZoom?.Dispose();
            return ok;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] GrapHelper tab routing failed: {ex.Message}");
            return false;
        }
    }

    sealed record CdpPageTarget(string Id, string Url, string Title, string WebSocketDebuggerUrl);

    // Stable tag per session+provider — reuses the same tab across CLI invocations within a session
    // Format: {provider}-{sessionHash} (e.g. "gemini-a1b2c3d4")
    static string BuildAskTargetTag(string provider)
    {
        var hash = GetSessionTag() ?? "default";
        return $"{provider}-{hash}";
    }

    /// <summary>
    /// Connect to CDP, launching Chrome if needed.
    /// Uses AskTargetRegistry + EnsureCorrectWindowAsync for tab reuse.
    /// Focus guard thread prevents Chrome from stealing keyboard focus.
    /// After setup, minimizes Chrome window — CDP works perfectly minimized,
    /// and this prevents all focus theft issues.
    /// </summary>
    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false, string? targetTag = null)
    {
        var task = Task.Run(async () =>
        {
            try
            {
                var active = await ChromeLauncher.IsPortActiveAsync(port);
                if (!active)
                {
                    var launchUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    Console.WriteLine($"[ASK] Launching Chrome → {launchUrl ?? "about:blank"}...");
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl);
                    await Task.Delay(2500);
                }

                var cdp = new CdpClient();
                await cdp.ConnectAsync(port, timeoutMs: 5000);

                // ── Cleanup: close all about:blank tabs ──
                await CloseBlankTabs(port);

                // Look up saved target from registry (survives across CLI invocations)
                var savedTargetId = !string.IsNullOrWhiteSpace(targetTag) ? AskTargetRegistry.GetTargetId(targetTag) : null;
                if (savedTargetId != null)
                    Console.WriteLine($"[ASK] Registry hit: {targetTag} → {savedTargetId[..Math.Min(8, savedTargetId.Length)]}");

                var navigateUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                var resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, navigateUrl,
                    savedTargetId: savedTargetId);

                // ── Tab URL validation: reject about:blank, verify correct host ──
                if (!string.IsNullOrWhiteSpace(preferredHost))
                {
                    var tabUrl = await cdp.EvalAsync("location.href") ?? "";
                    if (tabUrl == "about:blank" || !tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[ASK] Wrong tab: {tabUrl} (expected {preferredHost})");

                        if (tabUrl == "about:blank")
                        {
                            // about:blank is useless — close it and invalidate registry
                            Console.WriteLine("[ASK] Closing about:blank...");
                            try { await cdp.EvalAsync("window.close()"); } catch { }
                            await Task.Delay(500);
                        }

                        // Invalidate saved target
                        if (!string.IsNullOrWhiteSpace(targetTag))
                            AskTargetRegistry.SetTargetId(targetTag, null!);

                        // Reconnect — find or create correct tab
                        Console.WriteLine($"[ASK] Reconnecting to {preferredHost}...");
                        await cdp.ConnectAsync(port, timeoutMs: 5000);
                        resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag,
                            $"https://{preferredHost}", savedTargetId: null);
                        await Task.Delay(2000);

                        tabUrl = await cdp.EvalAsync("location.href") ?? "";
                        if (!tabUrl.Contains(preferredHost, StringComparison.OrdinalIgnoreCase))
                        {
                            await cdp.NavigateAsync($"https://{preferredHost}");
                            await Task.Delay(3000);
                        }
                        Console.WriteLine($"[ASK] Now on: {await cdp.EvalAsync("location.href")}");
                    }
                }

                // Save resolved target to registry for next invocation
                if (resolvedId != null && !string.IsNullOrWhiteSpace(targetTag))
                {
                    AskTargetRegistry.SetTargetId(targetTag, resolvedId);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[ASK] Target: {targetTag} → {resolvedId[..Math.Min(8, resolvedId.Length)]}");
                    Console.ResetColor();
                }

                // ── InputReadiness: blocker check + focusless minimize restore ──
                var chromeHwnd = cdp.GetChromeWindowHandle();
                if (chromeHwnd != IntPtr.Zero)
                {
                    var readiness = new WKAppBot.Win32.Input.InputReadiness();
                    var blocker = readiness.DetectBlocker(chromeHwnd);
                    if (blocker != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[AAR:CDP] Blocker: {blocker.Title} (hwnd={blocker.Handle:X})");
                        Console.ResetColor();
                    }
                    if (NativeMethods.IsIconic(chromeHwnd))
                    {
                        Console.WriteLine($"[AAR:CDP] Chrome minimized — focusless restore");
                        NativeMethods.ShowWindow(chromeHwnd, 4); // SW_SHOWNOACTIVATE
                        await Task.Delay(300);
                    }
                }

                return (CdpClient?)cdp;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Failed to connect: {ex.Message}");
                return (CdpClient?)null;
            }
        });
        return task.GetAwaiter().GetResult();
    }

    static async Task<List<CdpPageTarget>> GetPageTargetsAsync(int port)
    {
        var result = new List<CdpPageTarget>();
        try
        {
            using var http = new HttpClient();
            var json = await http.GetStringAsync($"http://localhost:{port}/json");
            var arr = JsonSerializer.Deserialize<JsonArray>(json);
            if (arr == null) return result;

            foreach (var node in arr)
            {
                var type = node?["type"]?.GetValue<string>() ?? "";
                if (!string.Equals(type, "page", StringComparison.OrdinalIgnoreCase))
                    continue;

                var ws = node?["webSocketDebuggerUrl"]?.GetValue<string>() ?? "";
                if (string.IsNullOrWhiteSpace(ws))
                    continue;

                var url = node?["url"]?.GetValue<string>() ?? "";
                var title = node?["title"]?.GetValue<string>() ?? "";
                var id = node?["id"]?.GetValue<string>() ?? string.Empty;
                result.Add(new CdpPageTarget(id, url, title, ws));
            }
        }
        catch { }
        return result;
    }

    /// <summary>Insert text into contentEditable editor (Quill/ProseMirror universal pattern).</summary>
    static async Task<bool> InsertTextContentEditable(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");

        // Tier 1: focusless — set innerHTML + dispatch InputEvent (React/ProseMirror picks it up)
        var focusless = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                var p = el.querySelector('p');
                if (p) { p.textContent = '{{escaped}}'; }
                else { el.innerHTML = '<p>{{escaped}}</p>'; }
                el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:'{{escaped}}'}));
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        var result = await cdp.EvalAsync(focusless);
        if (result == "OK") return true;

        // Tier 2: focus + execCommand (classic approach)
        var js = $$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                var sel = window.getSelection();
                var range = document.createRange();
                range.selectNodeContents(el);
                range.collapse(false);
                sel.removeAllRanges();
                sel.addRange(range);
                document.execCommand('insertText', false, '{{escaped}}');
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """;
        result = await cdp.EvalAsync(js);
        return result == "OK";
    }

    /// <summary>Clear contentEditable editor.</summary>
    static async Task ClearContentEditable(CdpClient cdp, string selector)
    {
        await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return;
                el.focus();
                document.execCommand('selectAll');
                document.execCommand('delete');
            })()
            """);
    }

    // ── Image Paste ──

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

        // Tier 1: Synthetic paste event with File blob (focusless — no real clipboard needed)
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
                var indicator = await cdp.EvalAsync("""
                    (() => {
                        // ChatGPT: file attachment area appears
                        var gpt = document.querySelector('[data-testid="file-thumbnail"]')
                                || document.querySelector('[class*="attachment"]')
                                || document.querySelector('[class*="file-upload"]')
                                || document.querySelector('img[src*="blob:"]');
                        if (gpt) return 'GPT_ATTACHED';
                        // Gemini: image preview in input area
                        var gem = document.querySelector('.input-area img')
                                || document.querySelector('[class*="uploaded"]')
                                || document.querySelector('[class*="attachment"]')
                                || document.querySelector('img[src*="blob:"]');
                        if (gem) return 'GEM_ATTACHED';
                        return 'NONE';
                    })()
                    """) ?? "NONE";

                if (indicator != "NONE")
                {
                    Console.WriteLine($"[ASK] Image attached: {indicator} ({(i + 1) * 500}ms)");
                    return true;
                }
            }
            // Even if no indicator detected, the paste event was dispatched — proceed optimistically
            Console.WriteLine("[ASK] No upload indicator, but paste dispatched — proceeding");
            return true;
        }

        // Tier 2: Win32 clipboard + CDP Ctrl+V
        Console.WriteLine("[ASK] Synthetic paste failed, trying clipboard + Ctrl+V...");
        try
        {
            // Set image to clipboard on STA thread
            var clipboardSet = false;
            var staThread = new Thread(() =>
            {
                try
                {
                    using var img = System.Drawing.Image.FromFile(imagePath);
                    System.Windows.Forms.Clipboard.SetImage(img);
                    clipboardSet = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ASK] Clipboard.SetImage failed: {ex.Message}");
                }
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join(3000);

            if (!clipboardSet)
                return false;

            // Focus editor
            await cdp.EvalAsync($"document.querySelector('{editorSelector}')?.focus()");
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
            var attached = await cdp.EvalAsync("""
                (() => {
                    return document.querySelector('[data-testid="file-thumbnail"]')
                        || document.querySelector('[class*="attachment"]')
                        || document.querySelector('img[src*="blob:"]')
                        ? 'YES' : 'NO';
                })()
                """) ?? "NO";

            Console.WriteLine($"[ASK] Clipboard paste: {attached}");
            return attached == "YES";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Clipboard paste failed: {ex.Message}");
            return false;
        }
    }

    /// <summary>Wait for image upload to complete (progress indicator gone).</summary>
    static async Task WaitForImageUpload(CdpClient cdp, int maxWaitMs = 15000)
    {
        var sw = Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            await Task.Delay(500);
            var uploading = await cdp.EvalAsync("""
                (() => {
                    var progress = document.querySelector('[class*="uploading"]')
                                || document.querySelector('[class*="progress"]')
                                || document.querySelector('[role="progressbar"]');
                    return progress ? 'UPLOADING' : 'DONE';
                })()
                """) ?? "DONE";
            if (uploading == "DONE")
            {
                Console.WriteLine($"[ASK] Image upload complete ({sw.ElapsedMilliseconds}ms)");
                return;
            }
        }
        Console.WriteLine("[ASK] Image upload wait timeout — proceeding");
    }

    static readonly HashSet<string> ImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        { ".png", ".jpg", ".jpeg", ".gif", ".webp", ".bmp" };

    /// <summary>Attach multiple files: images via clipboard paste, other files via hidden file input.</summary>
    static async Task AttachFilesViaCdp(CdpClient cdp, List<string> files, string editorSelector)
    {
        foreach (var filePath in files)
        {
            var ext = Path.GetExtension(filePath);
            if (ImageExtensions.Contains(ext))
            {
                // Image: clipboard paste (Tier 1 synthetic → Tier 2 Win32 clipboard + Ctrl+V)
                var imgOk = await PasteImageViaCdp(cdp, filePath, editorSelector);
                if (imgOk) await WaitForImageUpload(cdp);
                else Console.WriteLine($"[ASK] Image paste failed: {Path.GetFileName(filePath)}");
            }
            else
            {
                // Non-image file: use hidden file input element
                var fileOk = await AttachFileViaFileInput(cdp, filePath);
                if (fileOk) await WaitForImageUpload(cdp); // reuse upload wait
                else Console.WriteLine($"[ASK] File attach failed: {Path.GetFileName(filePath)}");
            }
            await Task.Delay(500); // settle between attachments
        }
    }

    /// <summary>
    /// Attach a non-image file via CDP DOM.setFileInputFiles on hidden file input.
    /// Works for txt, cs, log, pdf, etc. Fully focusless — no clipboard involved.
    /// Tier 1: DOM.setFileInputFiles + change event
    /// Tier 2: Synthetic drop event with DataTransfer (for React apps that ignore file input)
    /// </summary>
    static async Task<bool> AttachFileViaFileInput(CdpClient cdp, string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var absPath = Path.GetFullPath(filePath);
        var fileSize = new FileInfo(absPath).Length;
        Console.WriteLine($"[ASK] Attaching file: {fileName} ({fileSize / 1024}KB)");

        try
        {
            // ── Tier 1 FIRST: DOM.setFileInputFiles (fully focusless!) ──
            // Try CDP DOM approach before native dialog — no focus theft at all.
            Console.WriteLine("[ASK] Trying focusless DOM file attach...");
            // Click upload button via JS eval (focusless) → input[type=file] appears
            await cdp.EvalAsync("""
                (() => {
                    var btn = document.querySelector('button[aria-label*="Attach"]')
                           || document.querySelector('button[aria-label*="첨부"]')
                           || document.querySelector('button[aria-label*="Upload"]')
                           || document.querySelector('button[aria-label*="파일 업로드"]')
                           || document.querySelector('button[data-testid*="attach"]')
                           || document.querySelector('button[data-testid*="upload"]');
                    if (btn) btn.click();
                })()
                """);
            await Task.Delay(800);

            // If a menu opened, click the "파일 업로드" / "Upload file" menu item
            await cdp.EvalAsync("""
                (() => {
                    var items = document.querySelectorAll('[role=menuitem], [role=option]');
                    for (var item of items) {
                        var t = (item.textContent || '').trim();
                        if (t === '파일 업로드' || t === 'Upload file' || t === 'Upload'
                            || t.includes('컴퓨터') || t.includes('Computer')) {
                            item.click();
                            return 'CLICKED:' + t;
                        }
                    }
                    return 'NO_MENU';
                })()
                """);
            await Task.Delay(800);

            // Get all file inputs (some pages have multiple)
            var docResult = await cdp.SendAsync("DOM.getDocument", new JsonObject());
            var rootNodeId = docResult?["root"]?["nodeId"]?.GetValue<int>() ?? 0;

            if (rootNodeId > 0)
            {
                // Try querySelectorAll for multiple file inputs
                var queryResult = await cdp.SendAsync("DOM.querySelectorAll", new JsonObject
                {
                    ["nodeId"] = rootNodeId,
                    ["selector"] = "input[type=\"file\"]"
                });

                var nodeIds = queryResult?["nodeIds"]?.AsArray();
                Console.WriteLine($"[ASK] Found {nodeIds?.Count ?? 0} file input(s)");
                if (nodeIds != null && nodeIds.Count > 0)
                {
                    // Try each file input until one works
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
                            Console.WriteLine($"[ASK] File input set (nodeId={nodeId})");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ASK] File input set failed (nodeId={nodeId}): {ex.Message}");
                            continue;
                        }

                        // Fire change event (Angular/React needs this to detect the file)
                        var fireResult = await cdp.EvalAsync("""
                            (() => {
                                var inputs = document.querySelectorAll('input[type="file"]');
                                for (var inp of inputs) {
                                    if (inp.files && inp.files.length > 0) {
                                        inp.dispatchEvent(new Event('change', {bubbles: true}));
                                        inp.dispatchEvent(new Event('input', {bubbles: true}));
                                        return 'FIRED:' + inp.files[0].name;
                                    }
                                }
                                return 'NO_FILES';
                            })()
                            """);
                        Console.WriteLine($"[ASK] Change event: {fireResult}");
                        await Task.Delay(2000);

                        // Check if attachment appeared
                        if (await CheckFileAttached(cdp))
                        {
                            Console.WriteLine($"[ASK] File attached via input: {fileName}");
                            return true;
                        }
                        Console.WriteLine("[ASK] File input: attachment not detected in UI");
                    }
                }
            }

            // ── Tier 0.5: CDP File Chooser Interception (focusless) ──
            Console.WriteLine("[ASK] DOM failed, trying CDP file chooser...");
            var chooserOk = await cdp.SetFileViaChooserAsync(absPath, timeoutMs: 5000);
            if (chooserOk)
            {
                Console.WriteLine($"[ASK] File attached via chooser: {fileName}");
                await Task.Delay(1500);
                return true;
            }

            // ── Tier 0: Native OS file dialog via UIA (STEALS FOCUS — last resort) ──
            // Only try this if focusless tiers all failed.
            await cdp.DisableFileChooserInterception();
            Console.WriteLine("[ASK] Trying native file dialog (UIA) — may steal focus...");
            var uiaOk = await TryAttachViaFileDialog(cdp, absPath);
            if (uiaOk)
            {
                Console.WriteLine($"[ASK] File attached via UIA dialog: {fileName}");
                await Task.Delay(2000);
                return true;
            }

            // ── Tier 2: Synthetic drop event with DataTransfer ──
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

            // ── Tier 3: If text file, just append content inline as code block ──
            if (fileSize < 50_000 && IsTextFile(ext))
            {
                Console.WriteLine($"[ASK] Falling back to inline text for: {fileName}");
                return false; // caller will handle — file content in question text would be too big
            }

            Console.WriteLine($"[ASK] All attachment methods failed for: {fileName}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] File attach error: {ex.Message}");
            return false;
        }
    }

    /// <summary>Check if any file attachment indicator is visible in the page.</summary>
    static async Task<bool> CheckFileAttached(CdpClient cdp)
    {
        var attached = await cdp.EvalAsync("""
            (() => {
                // ChatGPT indicators
                if (document.querySelector('[data-testid="file-thumbnail"]')) return 'YES';
                if (document.querySelector('[data-testid*="attachment"]')) return 'YES';
                // Generic indicators (both ChatGPT and Gemini)
                if (document.querySelector('[class*="attachment"]')) return 'YES';
                if (document.querySelector('[class*="file-upload"]')) return 'YES';
                if (document.querySelector('[class*="uploaded-file"]')) return 'YES';
                if (document.querySelector('img[src*="blob:"]')) return 'YES';
                // Gemini file chip / upload indicator
                if (document.querySelector('[class*="file-chip"]')) return 'YES';
                if (document.querySelector('[class*="upload-chip"]')) return 'YES';
                if (document.querySelector('[class*="file-container"]')) return 'YES';
                if (document.querySelector('[class*="upload-container"]')) return 'YES';
                if (document.querySelector('[class*="inline-file"]')) return 'YES';
                if (document.querySelector('[class*="file-preview"]')) return 'YES';
                if (document.querySelector('uploader-thumbnail')) return 'YES';
                if (document.querySelector('mat-chip')) return 'YES';
                // Count file inputs with files set
                var inputs = document.querySelectorAll('input[type="file"]');
                for (var inp of inputs) { if (inp.files && inp.files.length > 0) return 'INPUT_HAS_FILES'; }
                return 'NO';
            })()
            """) ?? "NO";
        return attached != "NO";
    }

    /// <summary>
    /// Open Gemini upload menu → click "파일 업로드" → OS file dialog appears → UIA type path + click Open.
    /// Returns true if file was successfully attached via the native dialog.
    /// </summary>
    static async Task<bool> TryAttachViaFileDialog(CdpClient cdp, string absPath)
    {
        try
        {
            // Check if file open dialog is already open
            IntPtr existingDialog = FindFileOpenDialog();
            if (existingDialog == IntPtr.Zero)
            {
                // Open the upload menu with a TRUSTED click (Input.dispatchMouseEvent)
                // Chrome requires trusted user gestures to open <input type="file"> dialogs.
                // CDP Runtime.evaluate clicks are NOT trusted → dialog won't open.
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

                // Trusted click on upload button
                await CdpTrustedClick(cdp, bx, by);
                Console.WriteLine($"[ASK] UIA dialog: upload btn trusted click at ({bx},{by})");
                await Task.Delay(600);

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
                if (menuRect == "NO_ITEM") return false;

                var menuParts = menuRect!.Split(':');
                var coords = menuParts[0].Split(',');
                var mx = int.Parse(coords[0]);
                var my = int.Parse(coords[1]);

                // Trusted click on menu item → triggers <input type="file">.click() with user gesture
                await CdpTrustedClick(cdp, mx, my);
                Console.WriteLine($"[ASK] UIA dialog: menu item trusted click at ({mx},{my})");
            }
            else
            {
                Console.WriteLine($"[ASK] UIA dialog: reusing existing dialog hwnd={existingDialog:X}");
            }

            // Wait for OS file dialog to appear (#32770 with title "열기"/"Open" or ComboBoxEx32 child)
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

            // Step 4: Find the filename edit via Win32 (ComboBoxEx32 → ComboBox → Edit chain)
            // Standard Windows file dialog structure: Dialog → ComboBoxEx32(cid=1148) → ComboBox → Edit
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

            // Set the filename via WM_SETTEXT (Win32 — most reliable for file dialogs)
            NativeMethods.SendMessageW(editHwnd, 0x000C /*WM_SETTEXT*/, IntPtr.Zero, absPath);
            Console.WriteLine($"[ASK] UIA dialog: path set via WM_SETTEXT");

            await Task.Delay(300);

            // Step 5: Click "열기" button — find by control ID 1 (standard Open button)
            // GetDlgItem equivalent: find the button with control ID 1
            var openBtnHwnd = NativeMethods.FindWindowExW(dialogHwnd, IntPtr.Zero, "Button", null);
            // The first Button child is usually "열기(O)" / "Open". Click via BM_CLICK.
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

            // Step 6: Wait for dialog to close and file to appear in Gemini
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(300);
                if (!NativeMethods.IsWindow(dialogHwnd))
                {
                    Console.WriteLine("[ASK] UIA dialog: dialog closed OK");
                    await Task.Delay(1500);
                    return true;
                }
            }

            Console.WriteLine("[ASK] UIA dialog: dialog didn't close");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] UIA dialog error: {ex.Message}");
            return false;
        }
    }

    // ── CDP InputReadiness: ensure Chrome window is ready for CDP actions ──
    // Detects blockers, restores from minimized (focusless), guards focus theft.

    /// <summary>
    /// Ensure Chrome window is ready for a CDP action. Focusless approach:
    /// 1. DetectBlocker (~5ms) — check for blocking popups
    /// 2. Minimized restore (SW_SHOWNOACTIVATE — no focus steal)
    /// 3. Zoom overlay on target element (visual feedback)
    /// 4. Focus theft guard: snapshot + restore after action
    /// Returns (ready, prevForeground, zoom) — caller disposes zoom after action.
    /// </summary>
    static async Task<(bool ready, IntPtr prevFg, ClickZoomHelper? zoom)> EnsureCdpReadyAsync(
        CdpClient cdp, string action, string? cssSelector = null, string? label = null)
    {
        var prevFg = NativeMethods.GetForegroundWindow();
        ClickZoomHelper? zoom = null;
        try
        {
            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd == IntPtr.Zero)
            {
                Console.WriteLine($"[AAR:CDP] Chrome window not found for {action}");
                return (false, prevFg, null);
            }

            // Blocker/minimize already handled in EnsureCdpConnection.
            // Here: zoom overlay — show WHICH TAB is being targeted.
            // Background tab = user can't see the editor, so zoom on the tab strip item.

            // Strategy 1: Find the tab via UIA (shows tab title = user knows which AI)
            try
            {
                var pageTitle = await cdp.EvalAsync("document.title") ?? "";
                if (!string.IsNullOrEmpty(pageTitle))
                {
                    using var automation = new UIA3Automation();
                    var chromeEl = automation.FromHandle(chromeHwnd);
                    // Walk tab strip for matching title (substring match)
                    var tabs = chromeEl.FindAllDescendants(cf =>
                        cf.ByControlType(FlaUI.Core.Definitions.ControlType.TabItem));
                    foreach (var tab in tabs)
                    {
                        var tabName = tab.Name ?? "";
                        if (tabName.Contains(label ?? "", StringComparison.OrdinalIgnoreCase)
                            || tabName.Contains(pageTitle[..Math.Min(20, pageTitle.Length)], StringComparison.OrdinalIgnoreCase))
                        {
                            var tabRect = tab.BoundingRectangle;
                            if (tabRect.Width > 0 && tabRect.Height > 0)
                            {
                                var screenRect = new System.Drawing.Rectangle(
                                    (int)tabRect.X, (int)tabRect.Y, (int)tabRect.Width, (int)tabRect.Height);
                                Console.Write($"[ZOOM:TAB \"{tabName[..Math.Min(20, tabName.Length)]}\"] ");
                                zoom = ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label ?? action);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception tex)
            {
                Console.Write($"[ZOOM:TAB:ERR {tex.GetType().Name}] ");
            }

            // Fallback: zoom on Chrome window itself
            if (zoom == null)
            {
                try
                {
                    NativeMethods.GetWindowRect(chromeHwnd, out var winRect);
                    if (winRect.Width > 0 && winRect.Height > 0)
                    {
                        var screenRect = new System.Drawing.Rectangle(
                            winRect.Left, winRect.Top, winRect.Width, winRect.Height);
                        zoom = ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label ?? action);
                    }
                }
                catch { }
            }

            Console.WriteLine($"[AAR:CDP] Ready: {action}");
            return (true, prevFg, zoom);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AAR:CDP] Error: {ex.Message}");
            return (true, prevFg, zoom);
        }
    }

    /// <summary>
    /// Check if Chrome stole focus and restore if so.
    /// </summary>
    static void GuardCdpFocusTheft(CdpClient cdp, IntPtr prevFg, string action)
    {
        if (prevFg == IntPtr.Zero) return;
        var curFg = NativeMethods.GetForegroundWindow();
        if (curFg == prevFg) return;

        NativeMethods.GetWindowThreadProcessId(curFg, out uint curPid);
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero) return;
        NativeMethods.GetWindowThreadProcessId(chromeHwnd, out uint chromePid);

        if (curPid == chromePid)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[AAR:CDP:FOCUS] Chrome stole focus during {action}! Restoring...");
            Console.ResetColor();
            NativeMethods.SetForegroundWindow(prevFg);
        }
    }

    // ── CDP Zoom: show magnifier on CDP target element ──
    // Gets element's bounding rect (viewport coords) + Chrome window offset → screen coords → ClickZoomHelper.

    /// <summary>
    /// Begin zoom overlay on a CDP element identified by CSS selector.
    /// Returns null on failure (non-critical — zoom is informational only).
    /// </summary>
    static ClickZoomHelper? BeginCdpZoom(CdpClient cdp, string cssSelector, string action, string label)
    {
        try
        {
            var chromeHwnd = cdp.GetChromeWindowHandle();
            if (chromeHwnd == IntPtr.Zero) return null;

            // Get element bounding rect in viewport coords
            // Task.Run avoids sync-over-async deadlock when caller is already on async context
            var rectStr = Task.Run(() => cdp.EvalAsync($@"
                (() => {{
                    var el = document.querySelector('{cssSelector.Replace("'", "\\'")}');
                    if (!el) return 'NO_EL';
                    var r = el.getBoundingClientRect();
                    return Math.round(r.left) + ',' + Math.round(r.top) + ',' + Math.round(r.width) + ',' + Math.round(r.height);
                }})()
            ")).WaitAsync(TimeSpan.FromSeconds(3)).GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(rectStr) || rectStr == "NO_EL") return null;

            var parts = rectStr.Split(',');
            if (parts.Length != 4) return null;
            int vx = int.Parse(parts[0]), vy = int.Parse(parts[1]);
            int vw = int.Parse(parts[2]), vh = int.Parse(parts[3]);
            if (vw <= 0 || vh <= 0) return null;

            // Chrome window client area offset → screen coords
            var pt = new WKAppBot.Win32.Native.POINT(0, 0);
            NativeMethods.ClientToScreen(chromeHwnd, ref pt);

            var screenRect = new System.Drawing.Rectangle(pt.X + vx, pt.Y + vy, vw, vh);
            Console.Write($"[ZOOM:CDP {action}] ");
            return ClickZoomHelper.BeginFromRect(screenRect, chromeHwnd, $"cdp-{action}", label);
        }
        catch (Exception ex)
        {
            Console.Write($"[ZOOM:CDP:ERR {ex.GetType().Name}] ");
            return null;
        }
    }

    /// <summary>
    /// Send a trusted mouse click via CDP Input.dispatchMouseEvent.
    /// This creates a real user gesture that Chrome trusts for file dialog opening.
    /// </summary>
    static async Task CdpTrustedClick(CdpClient cdp, int x, int y)
    {
        await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "mousePressed", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
        await Task.Delay(50);
        await cdp.SendAsync("Input.dispatchMouseEvent", new System.Text.Json.Nodes.JsonObject
        {
            ["type"] = "mouseReleased", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
    }

    /// <summary>
    /// Enumerate all top-level #32770 dialogs and find one that looks like a file-open dialog.
    /// Strategy: title match ("열기"/"Open") first, then structural match (ComboBoxEx32/DUIViewWndClassName child).
    /// </summary>
    static IntPtr FindFileOpenDialog()
    {
        IntPtr result = IntPtr.Zero;
        var classBuf = new StringBuilder(256);
        var titleBuf = new StringBuilder(256);

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            classBuf.Clear();
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            if (classBuf.ToString() != "#32770") return true;

            titleBuf.Clear();
            NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
            var title = titleBuf.ToString();

            // Title match — most reliable
            if (title is "열기" or "Open" or "파일 열기" or "Open File")
            {
                result = hWnd;
                Console.WriteLine($"[ASK] FindFileOpenDialog: title match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            // Structural match — file dialog has ComboBoxEx32 (filename field) + DUIViewWndClassName (file list)
            var combo = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "ComboBoxEx32", null);
            var dui = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "DUIViewWndClassName", null);
            if (combo != IntPtr.Zero && dui != IntPtr.Zero)
            {
                result = hWnd;
                Console.WriteLine($"[ASK] FindFileOpenDialog: structural match '{title}' hwnd={hWnd:X}");
                return false; // stop
            }

            return true; // continue
        }, IntPtr.Zero);

        return result;
    }

    static bool IsTextFile(string ext) => ext is ".txt" or ".log" or ".md" or ".cs" or ".js"
        or ".ts" or ".py" or ".java" or ".json" or ".yaml" or ".yml" or ".xml" or ".html"
        or ".htm" or ".csv" or ".css" or ".sql" or ".sh" or ".bat" or ".cfg" or ".ini";

    // ── Response Image Detection & Download ──

    /// <summary>
    /// Detect new images in AI response and download them.
    /// Returns list of saved file paths for inline markers.
    /// Tracks already-downloaded images via knownImageUrls set.
    /// </summary>
    static async Task<List<string>> DetectAndDownloadImages(
        CdpClient cdp, HashSet<string> knownImageUrls, string aiName, string? responseSelector = null)
    {
        var saved = new List<string>();
        try
        {
            // Find all images in the latest response that we haven't seen yet
            var selectorBase = responseSelector ?? (aiName == "gemini"
                ? "model-response:last-of-type img, [role='article']:last-of-type img"
                : "[data-message-author-role='assistant']:last-of-type img, article:last-of-type img");

            var imgJson = await cdp.EvalAsync($$"""
                (() => {
                    var imgs = document.querySelectorAll("{{selectorBase}}");
                    var result = [];
                    var allImgs = document.querySelectorAll('img');
                    for (var img of imgs) {
                        var src = img.src || img.getAttribute('src') || '';
                        if (!src || src.startsWith('data:image/svg')) continue;
                        // Skip tiny icons/avatars (< 50px)
                        if (img.naturalWidth > 0 && img.naturalWidth < 50) continue;
                        // Find global index for reliable re-lookup
                        var idx = -1;
                        for (var j = 0; j < allImgs.length; j++) { if (allImgs[j] === img) { idx = j; break; } }
                        result.push({src: src, w: img.naturalWidth || 0, h: img.naturalHeight || 0, idx: idx});
                    }
                    return JSON.stringify(result);
                })()
                """) ?? "[]";

            var imgs = JsonDocument.Parse(imgJson).RootElement;
            foreach (var img in imgs.EnumerateArray())
            {
                var src = img.GetProperty("src").GetString() ?? "";
                var imgIdx = img.TryGetProperty("idx", out var idxEl) ? idxEl.GetInt32() : -1;
                if (string.IsNullOrEmpty(src) || knownImageUrls.Contains(src)) continue;
                knownImageUrls.Add(src);

                // Download image
                var outputDir = Path.Combine(
                    Environment.GetEnvironmentVariable("WKAPPBOT_HQ") ?? @"W:\SDK\bin\wkappbot.hq",
                    "output");
                Directory.CreateDirectory(outputDir);
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var idx = saved.Count + 1;
                var w = img.GetProperty("w").GetInt32();
                var h = img.GetProperty("h").GetInt32();
                var fileName = $"{aiName}_gen_{timestamp}_{idx}.png";
                var filePath = Path.Combine(outputDir, fileName);

                try
                {
                    byte[]? bytes = null;
                    // Use global index for reliable re-lookup (avoids URL matching issues)
                    string findImgJs;
                    if (imgIdx >= 0)
                    {
                        findImgJs = $"document.querySelectorAll('img')[{imgIdx}]";
                    }
                    else
                    {
                        var srcSnippet = src.Replace("'", "\\'");
                        if (srcSnippet.Length > 60) srcSnippet = srcSnippet.Substring(0, 60);
                        findImgJs = $"Array.from(document.querySelectorAll('img')).find(i => i.src.includes('{srcSnippet}'))";
                    }

                    // ── Tier 1: Canvas rendering — reload with crossOrigin to avoid taint ──
                    var canvasB64 = await cdp.EvalAsync($$"""
                        (async () => {
                            try {
                                var origImg = {{findImgJs}};
                                if (!origImg) return 'ERR:no_img';
                                // Try direct canvas first (same-origin images)
                                try {
                                    if (origImg.complete && origImg.naturalWidth > 0) {
                                        var c = document.createElement('canvas');
                                        c.width = origImg.naturalWidth; c.height = origImg.naturalHeight;
                                        c.getContext('2d').drawImage(origImg, 0, 0);
                                        var d = c.toDataURL('image/png');
                                        return d.substring(d.indexOf(',') + 1);
                                    }
                                } catch(e) { /* tainted — fall through to crossOrigin reload */ }
                                // Reload with crossOrigin='anonymous' to avoid taint
                                var src = origImg.src;
                                if (!src.startsWith('data:') && !src.startsWith('blob:')) {
                                    var newImg = new Image();
                                    newImg.crossOrigin = 'anonymous';
                                    var loaded = await new Promise((resolve) => {
                                        newImg.onload = () => resolve(true);
                                        newImg.onerror = () => resolve(false);
                                        setTimeout(() => resolve(false), 5000);
                                        newImg.src = src;
                                    });
                                    if (loaded && newImg.naturalWidth > 0) {
                                        var c = document.createElement('canvas');
                                        c.width = newImg.naturalWidth; c.height = newImg.naturalHeight;
                                        c.getContext('2d').drawImage(newImg, 0, 0);
                                        var d = c.toDataURL('image/png');
                                        return d.substring(d.indexOf(',') + 1);
                                    }
                                }
                                // data: or blob: direct canvas with wait
                                if (!origImg.complete) await new Promise(r => { origImg.onload = r; setTimeout(r, 3000); });
                                if (!origImg.naturalWidth) return 'ERR:no_natural';
                                var c2 = document.createElement('canvas');
                                c2.width = origImg.naturalWidth; c2.height = origImg.naturalHeight;
                                c2.getContext('2d').drawImage(origImg, 0, 0);
                                var d2 = c2.toDataURL('image/png');
                                return d2.substring(d2.indexOf(',') + 1);
                            } catch(e) { return 'ERR:' + e.message; }
                        })()
                        """, awaitPromise: true) ?? "ERR:null";
                    if (!canvasB64.StartsWith("ERR:") && canvasB64.Length > 100)
                    {
                        bytes = Convert.FromBase64String(canvasB64);
                        Console.WriteLine($"[ASK] Image captured via canvas ({w}x{h})");
                    }
                    else if (canvasB64.StartsWith("ERR:"))
                    {
                        Console.WriteLine($"[ASK] Canvas failed: {canvasB64}");
                    }

                    // ── Tier 2: data: URL direct extract ──
                    if (bytes == null && src.StartsWith("data:"))
                    {
                        var commaIdx = src.IndexOf(',');
                        if (commaIdx > 0)
                            bytes = Convert.FromBase64String(src.Substring(commaIdx + 1));
                    }

                    // ── Tier 3: fetch with credentials (https: URLs) ──
                    if (bytes == null && src.StartsWith("http"))
                    {
                        var fetchSrcEsc = src.Replace("\\", "\\\\").Replace("'", "\\'");
                        var fetchB64 = await cdp.EvalAsync($$"""
                            (async () => {
                                try {
                                    var resp = await fetch('{{fetchSrcEsc}}', {credentials: 'include'});
                                    if (!resp.ok) return 'ERR:' + resp.status;
                                    var blob = await resp.blob();
                                    if (blob.size < 100) return 'ERR:empty';
                                    return await new Promise((resolve) => {
                                        var reader = new FileReader();
                                        reader.onloadend = () => {
                                            var r = reader.result || '';
                                            var i = r.indexOf(',');
                                            resolve(i > 0 ? r.substring(i + 1) : '');
                                        };
                                        reader.readAsDataURL(blob);
                                    });
                                } catch(e) { return 'ERR:' + e.message; }
                            })()
                            """, awaitPromise: true) ?? "ERR:null";
                        if (!fetchB64.StartsWith("ERR:") && fetchB64.Length > 100)
                            bytes = Convert.FromBase64String(fetchB64);
                    }

                    // ── Tier 4: CDP Page.captureScreenshot with proper timing ──
                    if (bytes == null)
                    {
                        try
                        {
                            // Bring tab to front for accurate screenshot (ignore errors)
                            try { await cdp.SendAsync("Page.bringToFront"); } catch { }

                            // Scroll image into view, wait 2 frames for reflow, measure rect
                            var rectJson = await cdp.EvalAsync($$"""
                                (async () => {
                                    var img = {{findImgJs}};
                                    if (!img) return '';
                                    img.scrollIntoView({block:'center', behavior:'instant'});
                                    // Wait 2 animation frames for scroll + reflow
                                    await new Promise(r => requestAnimationFrame(() => requestAnimationFrame(r)));
                                    var r = img.getBoundingClientRect();
                                    var dpr = window.devicePixelRatio || 1;
                                    if (r.width < 10 || r.height < 10) return '';
                                    // Clamp to viewport
                                    var x = Math.max(0, r.x), y = Math.max(0, r.y);
                                    var w = Math.min(r.width, window.innerWidth - x);
                                    var h = Math.min(r.height, window.innerHeight - y);
                                    if (w < 10 || h < 10) return '';
                                    return JSON.stringify({x:x, y:y, w:w, h:h, dpr:dpr});
                                })()
                                """, awaitPromise: true) ?? "";
                            if (!string.IsNullOrEmpty(rectJson))
                            {
                                var rect = JsonDocument.Parse(rectJson).RootElement;
                                var rx = rect.GetProperty("x").GetDouble();
                                var ry = rect.GetProperty("y").GetDouble();
                                var rw = rect.GetProperty("w").GetDouble();
                                var rh = rect.GetProperty("h").GetDouble();
                                var dpr = rect.TryGetProperty("dpr", out var dprEl) ? dprEl.GetDouble() : 1.0;

                                Console.WriteLine($"[ASK] Screenshot clip: x={rx:F0} y={ry:F0} w={rw:F0} h={rh:F0} dpr={dpr}");
                                var ssResult = await cdp.SendAsync("Page.captureScreenshot", new JsonObject
                                {
                                    ["format"] = "png",
                                    ["clip"] = new JsonObject
                                    {
                                        ["x"] = rx, ["y"] = ry,
                                        ["width"] = rw, ["height"] = rh,
                                        ["scale"] = 1
                                    }
                                });
                                var ssB64 = (ssResult as System.Text.Json.Nodes.JsonObject)?["data"]?.GetValue<string>();
                                if (!string.IsNullOrEmpty(ssB64))
                                {
                                    bytes = Convert.FromBase64String(ssB64);
                                    w = (int)(rw * dpr); h = (int)(rh * dpr);
                                    Console.WriteLine($"[ASK] Image captured via screenshot ({w}x{h}, dpr={dpr:F1})");
                                }
                            }
                        }
                        catch (Exception ssEx)
                        {
                            Console.WriteLine($"[ASK] Screenshot fallback failed: {ssEx.GetType().Name}: {ssEx.Message}");
                            Console.WriteLine($"[ASK] Screenshot stack: {ssEx.StackTrace?.Split('\n').FirstOrDefault()?.Trim()}");
                        }
                    }

                    if (bytes != null && bytes.Length > 100)
                    {
                        // Detect actual format from magic bytes for extension
                        var ext = ".png";
                        if (bytes.Length > 3 && bytes[0] == 0xFF && bytes[1] == 0xD8) ext = ".jpg";
                        else if (bytes.Length > 4 && bytes[0] == 0x52 && bytes[1] == 0x49) ext = ".webp";
                        fileName = $"{aiName}_gen_{timestamp}_{idx}{ext}";
                        filePath = Path.Combine(outputDir, fileName);

                        File.WriteAllBytes(filePath, bytes);
                        saved.Add(filePath);
                        Console.WriteLine();
                        Console.WriteLine($"[image:{fileName} ({w}x{h}, {bytes.Length / 1024}KB)]");
                        Console.Out.Flush();
                    }
                    else
                    {
                        Console.WriteLine($"[ASK] Image download empty/small for: {src.Substring(0, Math.Min(80, src.Length))}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ASK] Image save failed: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Non-fatal — image detection is best-effort
            if (!ex.Message.Contains("JSON")) // suppress parse noise
                Console.WriteLine($"[ASK] Image detect error: {ex.Message}");
        }
        return saved;
    }

    // ── Gemini ──

    static int AskGemini(string question, bool slackReport, int timeoutSec, bool newTab, List<string>? attachFiles = null, bool newSession = false)
    {
        Console.WriteLine($"[ASK] Gemini: {question}");

        // UIA tab routing: switch to Gemini tab before CDP connects (focusless)
        if (!newTab) EnsureTabViaGrap("Chrome", "Gemini");

        var targetTag = BuildAskTargetTag("gemini");
        var cdp = EnsureCdpConnection(preferredHost: "gemini.google.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate (iconified OK — CDP works without rendering) ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("gemini.google.com"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session — navigating to fresh Gemini..." : "[ASK] Navigating to Gemini...");
                    await cdp.NavigateAsync("https://gemini.google.com/app");
                    await Task.Delay(3000);
                }
                else
                {
                    Console.WriteLine($"[ASK] Reusing Gemini session");
                }

                // NOTE: BringToFront removed — steals OS focus.
                // CDP insertText/eval/setFileInputFiles all work on background tabs.

                // Diagnose tab state before editor search
                var hiddenState = await cdp.EvalAsync("document.hidden + '|' + document.title + '|' + document.querySelectorAll('*').length");
                Console.WriteLine($"[ASK] Tab state: {hiddenState}");

                // A11y-first: find editor via selector chain
                var editorSel = await WaitForEditorA11y(cdp,
                    ".ql-editor",                                   // Quill class
                    "[role='textbox'][contenteditable='true']",      // ARIA role
                    "div[contenteditable='true']",                   // Generic
                    "div.ql-editor",                                 // Quill with tag
                    "rich-textarea [contenteditable]",               // Gemini new UI
                    ".input-area [contenteditable]"                  // Gemini alt
                );
                if (editorSel == null)
                {
                    // Last resort: dump available contenteditable elements
                    var ceDebug = await cdp.EvalAsync("""
                        (() => {
                            var ce = document.querySelectorAll('[contenteditable]');
                            return Array.from(ce).map(e => e.tagName + '.' + (e.className||'').substring(0,40) + '[' + e.getAttribute('contenteditable') + ']').join(' | ');
                        })()
                        """);
                    Console.WriteLine($"[ASK] contenteditable elements: {ceDebug}");
                    Console.WriteLine("[ASK] Editor not found");
                    return (false, (string?)null);
                }

                // ── Persona injection on fresh Gemini conversation ──
                var geminiTurnCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                if (geminiTurnCount == "0")
                {
                    // ── Browser exclusive: persona input → send complete ──
                    using var personaLock = ChromeTabLock.Acquire("Gemini/persona");
                    if (personaLock == null) return (false, (string?)null);

                    Console.WriteLine("[ASK] Fresh Gemini — injecting persona...");
                    await ClearContentEditable(cdp, editorSel);
                    await InsertTextContentEditable(cdp, editorSel, AskPersona);
                    await Task.Delay(300);

                    // Send persona (button click → Enter fallback)
                    var personaSent = false;
                    for (int ps = 0; ps < 5; ps++)
                    {
                        var rem = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                        if (rem == "0" && ps > 0) { personaSent = true; break; }
                        await cdp.EvalAsync("document.querySelector('button[aria-label=\"Send message\"], button[aria-label=\"메시지 보내기\"], .send-button, button.send-button')?.click()");
                        await Task.Delay(500);
                        await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                        {
                            ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                            ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                        });
                        await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                        {
                            ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                            ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                        });
                        await Task.Delay(500);
                    }

                    if (personaSent)
                    {
                        // Wait for Gemini to respond to persona (no lock needed — just polling)
                        var psw = Stopwatch.StartNew();
                        while (psw.Elapsed.TotalSeconds < 15)
                        {
                            await Task.Delay(1500);
                            var resp = await cdp.EvalAsync("""
                                (() => {
                                    var r = document.querySelectorAll('model-response');
                                    if (r.length === 0) r = document.querySelectorAll('[role="article"]');
                                    if (r.length === 0) return '';
                                    return (r[r.length-1].innerText || '').substring(0, 50);
                                })()
                                """) ?? "";
                            if (resp.Length > 0)
                            {
                                bool ready = resp.Contains("READY", StringComparison.OrdinalIgnoreCase);
                                Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : resp)}");
                                break;
                            }
                        }
                        // Wait for Gemini to finish streaming (stop button gone + text stable)
                        for (int stab = 0; stab < 10; stab++)
                        {
                            await Task.Delay(500);
                            var streaming = await cdp.EvalAsync("""
                                (() => {
                                    var stop = document.querySelector('button[aria-label="응답 중지"], button[aria-label="Stop response"], button[aria-label="Stop"]');
                                    if (stop) return 'STREAMING';
                                    var mat = document.querySelector('mat-icon[fonticon="stop_circle"]');
                                    if (mat) return 'STREAMING';
                                    return 'IDLE';
                                })()
                                """) ?? "IDLE";
                            if (streaming == "IDLE")
                            {
                                Console.WriteLine($"[ASK] Persona streaming done (stable after {(stab+1)*500}ms)");
                                break;
                            }
                            if (stab == 9) Console.WriteLine("[ASK] Persona streaming timeout, proceeding anyway");
                        }
                        // Re-find editor after persona exchange
                        editorSel = await WaitForEditorA11y(cdp,
                            ".ql-editor", "[role='textbox'][contenteditable='true']",
                            "div[contenteditable='true']", "div.ql-editor",
                            "rich-textarea [contenteditable]", ".input-area [contenteditable]");
                        if (editorSel == null)
                        {
                            Console.WriteLine("[ASK] Editor lost after persona");
                            return (false, (string?)null);
                        }
                    }
                    else
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                }

                // ── Browser exclusive: question input → send complete ──
                using var questionLock = ChromeTabLock.Acquire("Gemini");
                if (questionLock == null) return (false, (string?)null);

                // ── CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ──
                var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "Gemini");

                // ── File attachments (before text) ──
                if (attachFiles?.Count > 0)
                    await AttachFilesViaCdp(cdp, attachFiles, editorSel);

                // Tier 1: focusless insert (a11y-first)
                await ClearContentEditable(cdp, editorSel);
                var inserted = await InsertTextContentEditable(cdp, editorSel, question);
                if (!inserted)
                {
                    // ── Tier 2: CDP Input.insertText (needs focus) ──
                    Console.WriteLine("[ASK] Focusless insert failed, trying CDP Input.insertText...");
                    await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
                    await Task.Delay(100);
                    await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                    {
                        ["type"] = "keyDown", ["key"] = "a", ["code"] = "KeyA",
                        ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
                    });
                    await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                    {
                        ["type"] = "keyUp", ["key"] = "a", ["code"] = "KeyA",
                        ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
                    });
                    await Task.Delay(50);
                    await cdp.SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject
                    {
                        ["text"] = question
                    });
                    await Task.Delay(200);

                    var verify = await cdp.EvalAsync(
                        $"document.querySelector('{editorSel}')?.textContent?.length ?? 0") ?? "0";
                    if (verify == "0")
                    {
                        zoom?.ShowFail("insert failed");
                        zoom?.Dispose();
                        Console.WriteLine("[ASK] Failed to insert text");
                        return (false, (string?)null);
                    }
                }

                // ── Focus theft detection: restore if Chrome stole focus ──
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");

                // Send: a11y-first (CDP real click on button) → focusless Enter fallback
                // Keep trying until editor is empty (= message sent)
                await Task.Delay(300);
                var sendResult = "PENDING";
                // Count model-responses before sending — detect response start as send confirmation
                var preResponseCount = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";

                for (int sendAttempt = 0; sendAttempt < 5; sendAttempt++)
                {
                    // Check if editor cleared OR response started (= already sent, don't re-send!)
                    var remaining = await cdp.EvalAsync($"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 0") ?? "0";
                    if (remaining == "0" && sendAttempt > 0)
                    {
                        sendResult = $"SENT(attempt={sendAttempt})";
                        break;
                    }
                    // Response started = message was sent, stop clicking (avoids hitting stop button)
                    if (sendAttempt > 0)
                    {
                        var curResponses = await cdp.EvalAsync(
                            "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                        if (curResponses != preResponseCount)
                        {
                            sendResult = $"RESPONSE_STARTED(attempt={sendAttempt})";
                            break;
                        }
                    }

                    // Re-insert text if editor is empty (text didn't stick)
                    if (remaining == "0" && sendAttempt == 0)
                    {
                        await InsertTextContentEditable(cdp, editorSel, question);
                        await Task.Delay(200);
                    }

                    // JS click() — works even when Chrome is minimized (no viewport needed)
                    var clickResult = await cdp.EvalAsync("""
                        (() => {
                            var btn = document.querySelector('button[aria-label="메시지 보내기"]')
                                   || document.querySelector('button[aria-label="Send message"]')
                                   || document.querySelector('button.send-button');
                            if (!btn || btn.disabled) return 'NO_BUTTON';
                            btn.click();
                            return 'CLICKED';
                        })()
                        """) ?? "NO_BUTTON";

                    if (clickResult != "CLICKED")
                    {
                        // Fallback: Enter key via CDP
                        await Task.Delay(100);
                        await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                        {
                            ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                            ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                        });
                        await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
                        {
                            ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                            ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                        });
                    }
                    await Task.Delay(500);
                }
                if (sendResult == "PENDING") sendResult = "FORCED(5x)";

                // Zoom feedback: sent successfully
                zoom?.ShowPass($"sent ({sendResult})");
                zoom?.Dispose();

                Console.WriteLine($"[ASK] Sent! Waiting for response... (send={sendResult})");
                questionLock.Release("sent");

                // Count existing responses before polling (skip persona's READY etc.)
                var preCountStr = await cdp.EvalAsync(
                    "(document.querySelectorAll('model-response').length || document.querySelectorAll('[role=\"article\"]').length || 0).toString()") ?? "0";
                int baseResponseCount = int.TryParse(preCountStr, out var brc) ? brc : 0;

                // Register for tab handoff + activate peer's tab (we'll poll with textContent)
                RegisterWaitingTab("gemini", cdp);
                await HandoffTabToPeer("gemini");

                // Wait for response — poll until text stabilizes
                // Live flush: print new text as it arrives during streaming
                string? lastText = null;
                int stableCount = 0;
                int lastTextLen = 0;
                int lastFlushedLen = 0;
                bool liveHeaderPrinted = false;
                var geminiKnownImages = new HashSet<string>();
                var geminiSavedImages = new List<string>();
                var sw = Stopwatch.StartNew();

                int geminiBlankCount = 0;
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(2000);
                    // A11y-first: model-response → [role='article'] → generic text
                    // Only read NEW responses (skip persona exchange)
                    var text = await cdp.EvalAsync(
                        "(() => {" +
                        "if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100) return '\\x01BLANK';" +
                        "var responses = document.querySelectorAll('model-response');" +
                        "if (responses.length === 0) { var articles = document.querySelectorAll('[role=\"article\"]'); responses = articles.length > 0 ? articles : responses; }" +
                        $"if (responses.length <= {baseResponseCount}) return '';" +
                        "var last = responses[responses.length - 1];" +
                        "return last.textContent || '';" +
                        "})()");

                    // Blank/broken page detection
                    if (text == "\x01BLANK" || text == null)
                    {
                        geminiBlankCount++;
                        Console.WriteLine($"[ASK] Page blank/broken ({geminiBlankCount}/3)");
                        if (geminiBlankCount >= 3)
                        {
                            Console.WriteLine("[ASK] Page unresponsive — aborting poll");
                            break;
                        }
                        continue;
                    }
                    geminiBlankCount = 0;

                    if (string.IsNullOrEmpty(text))
                        continue;

                    // Live flush: print new text delta
                    if (text.Length > lastFlushedLen)
                    {
                        if (!liveHeaderPrinted)
                        {
                            Console.WriteLine();
                            Console.WriteLine("── Gemini (streaming) ──");
                            liveHeaderPrinted = true;
                        }
                        Console.Write(text.Substring(lastFlushedLen));
                        Console.Out.Flush();
                        lastFlushedLen = text.Length;
                    }

                    // Detect generated images in response
                    var gemNewImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                    geminiSavedImages.AddRange(gemNewImages);

                    // Streaming handoff: text growing → this tab is alive, give active tab to peer
                    if (text.Length > lastTextLen && lastTextLen > 0)
                        await HandoffTabToPeer("gemini");
                    lastTextLen = text.Length;

                    // Check if response is still generating
                    if (text == lastText)
                    {
                        stableCount++;
                        if (stableCount >= 2) // stable for 4+ seconds
                        {
                            // Final image check
                            var gemFinalImages = await DetectAndDownloadImages(cdp, geminiKnownImages, "gemini");
                            geminiSavedImages.AddRange(gemFinalImages);
                            if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                            Console.WriteLine($"[ASK] Response received ({text.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            if (geminiSavedImages.Count > 0)
                                Console.WriteLine($"[ASK] Downloaded {geminiSavedImages.Count} generated image(s)");
                            return (true, text);
                        }
                    }
                    else
                    {
                        stableCount = 0;
                        lastText = text;
                    }
                }

                // Done — hand off tab to peer if still waiting
                await HandoffTabToPeer("gemini");

                // Timeout — return whatever we have
                if (!string.IsNullOrEmpty(lastText))
                {
                    Console.WriteLine($"[ASK] Timeout — partial response ({lastText.Length} chars)");
                    return (true, lastText);
                }
                Console.WriteLine("[ASK] Timeout — no response, retrying once...");

                // Retry: same page, re-insert and send (no reload, keeps session)
                await Task.Delay(1000);
                await ClearContentEditable(cdp, editorSel);
                await InsertTextContentEditable(cdp, editorSel, question);
                await Task.Delay(300);
                await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[aria-label="메시지 보내기"]')
                               || document.querySelector('button[aria-label="Send message"]')
                               || document.querySelector('button.send-button');
                        if (btn && !btn.disabled) btn.click();
                    })()
                    """);
                Console.WriteLine("[ASK] Retry: sent question");

                // Poll retry (shorter timeout)
                var retrySw = Stopwatch.StartNew();
                string? retryText = null;
                while (retrySw.Elapsed.TotalSeconds < 30)
                {
                    await Task.Delay(2000);
                    var text = await cdp.EvalAsync(
                        "(() => {" +
                        "var r = document.querySelectorAll('model-response');" +
                        "if (r.length === 0) { var a = document.querySelectorAll('[role=\"article\"]'); r = a.length > 0 ? a : r; }" +
                        "if (r.length === 0) return '';" +
                        "return r[r.length-1].textContent || '';" +
                        "})()") ?? "";
                    if (string.IsNullOrEmpty(text)) continue;
                    if (text == retryText) { Console.WriteLine($"[ASK] Retry: response ({text.Length} chars)"); return (true, text); }
                    retryText = text;
                }
                if (!string.IsNullOrEmpty(retryText))
                {
                    Console.WriteLine($"[ASK] Retry: partial ({retryText.Length} chars)");
                    return (true, retryText);
                }
                Console.WriteLine("[ASK] Retry: also failed");
                return (false, (string?)null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Print answer (truncate for console)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── Gemini 답변 ──");
            Console.ResetColor();
            // Remove "Gemini의 응답" prefix if present
            var cleaned = answer.StartsWith("Gemini의 응답") ? answer["Gemini의 응답".Length..] : answer;
            Console.WriteLine(cleaned.Length > 2000 ? cleaned[..2000] + "\n... (truncated)" : cleaned);

            // Full answer marker (for programmatic capture by whisper study etc.)
            Console.WriteLine("[ASK_FULL_ANSWER_BEGIN]");
            Console.WriteLine(cleaned);
            Console.WriteLine("[ASK_FULL_ANSWER_END]");

            if (slackReport)
                ReportToSlack("Gemini", question, cleaned);
        }

        UnregisterWaitingTab("gemini");
        // Preserve Chrome's original state — don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── ChatGPT ──

    // Shared persona for external AI agents (ChatGPT, Gemini).
    // Injected on fresh conversations to stabilize output format.
    // Single-line to avoid ProseMirror/Quill multiline issues.
    const string AskPersona =
        "You are a senior dev consultant called by another AI agent (Claude) via CLI automation. " +
        "I will ask you planning, debugging, and architecture questions. " +
        "Rules: (1) Always reply in English for token efficiency (the caller translates if needed). " +
        "(2) Answer as if YOU were doing the task — give concrete steps, actual commands, and real code. " +
        "(3) No disclaimers, no filler, no follow-up questions. " +
        "(4) For planning: numbered steps with specific commands/tools. " +
        "(5) For code: ONLY the code, no explanation unless asked. " +
        "(6) Keep answers under 150 words unless the question demands more. " +
        "(7) No blank lines between paragraphs — keep output compact and dense, single-spaced. " +
        "(8) For image analysis: output JSON with {label, text, x, y, w, h} for each UI element. " +
        "(9) If asked to generate/create/draw an image, USE your image generation tool (DALL-E/Imagen). Do NOT make ASCII art. " +
        "(10) Confirm you understood with exactly: READY";

    static int AskChatGpt(string question, bool slackReport, int timeoutSec, bool newTab, List<string>? attachFiles = null, bool newSession = false)
    {
        Console.WriteLine($"[ASK] ChatGPT: {question}");

        // UIA tab routing: switch to ChatGPT tab before CDP connects (focusless)
        if (!newTab) EnsureTabViaGrap("Chrome", "ChatGPT");

        var targetTag = BuildAskTargetTag("gpt");
        var cdp = EnsureCdpConnection(preferredHost: "chatgpt.com", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate (iconified OK) ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("chatgpt.com"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session — navigating to fresh ChatGPT..." : "[ASK] Navigating to ChatGPT...");
                    await cdp.NavigateAsync("https://chatgpt.com");
                    await Task.Delay(3000);
                }

                // NOTE: BringToFront removed — steals OS focus. CDP works on background tabs.

                // Wait for ProseMirror editor
                var editorSel = await WaitForChatGptEditorA11y(cdp);
                if (editorSel == null)
                    return (false, (string?)null);
                Console.WriteLine($"[ASK] Editor found: {editorSel}");

                // Check if this is a fresh conversation — try multiple selectors
                var turnCountStr = await cdp.EvalAsync("""
                    (() => {
                        var c = document.querySelectorAll('[data-message-author-role="assistant"]').length;
                        if (c > 0) return '' + c;
                        c = document.querySelectorAll('article').length;
                        if (c > 0) return '' + Math.floor(c / 2);
                        c = document.querySelectorAll('[class*="agent-turn"]').length;
                        if (c > 0) return '' + c;
                        c = document.querySelectorAll('[data-testid*="conversation-turn"]').length;
                        if (c > 0) return '' + Math.floor(c / 2);
                        return '0';
                    })()
                    """) ?? "0";
                int existingTurns = int.TryParse(turnCountStr, out var etc) ? etc : 0;

                // Reuse existing session — only inject persona on fresh (0 turns) conversations
                if (existingTurns > 0)
                    Console.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                if (existingTurns == 0)
                {
                    // Fresh conversation -- inject persona prompt first
                    Console.WriteLine("[ASK] Fresh conversation -- injecting persona...");
                    var (personaOk, personaResp) = await ChatGptSendAndWait(
                        cdp, AskPersona.Trim(), timeoutSec: 20);
                    if (!personaOk)
                    {
                        Console.WriteLine("[ASK] Persona injection failed, continuing anyway");
                    }
                    else
                    {
                        bool ready = (personaResp ?? "").Contains("READY", StringComparison.OrdinalIgnoreCase);
                        Console.WriteLine($"[ASK] Persona: {(ready ? "READY" : personaResp?.Substring(0, Math.Min(50, personaResp.Length)))}");
                    }
                }

                // Re-wait for editor readiness after persona exchange
                if (!await WaitForChatGptEditor(cdp))
                    return (false, (string?)null);
                await Task.Delay(1000); // Let ChatGPT UI settle

                // Send the actual question (with 9s-delay retry on timeout)
                var (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec, attachFiles);
                if (!ok && string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("[ASK] ChatGPT timeout — retrying in 9 seconds...");
                    await Task.Delay(9000);
                    (ok, answer) = await ChatGptSendAndWait(cdp, question, timeoutSec);
                }
                return (ok, answer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            // Parseable output format with markers
            Console.WriteLine("[ASK_ANSWER_BEGIN]");
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);
            Console.WriteLine("[ASK_ANSWER_END]");

            if (slackReport)
                ReportToSlack("ChatGPT", question, answer);
        }

        UnregisterWaitingTab("chatgpt");
        // Preserve Chrome's original state — don't force minimize
        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // A11y-first selector chain for ChatGPT editor (most stable → least stable)
    static readonly string[] ChatGptEditorSelectors =
    [
        "#prompt-textarea",                              // Stable ID
        "[role='textbox'][contenteditable='true']",      // ARIA role
        ".ProseMirror[contenteditable='true']",          // ProseMirror class
        "div[contenteditable='true']",                   // Generic fallback
    ];

    /// <summary>Close all about:blank tabs via CDP /json API.</summary>
    static async Task CloseBlankTabs(int port)
    {
        try
        {
            using var http = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var json = await http.GetStringAsync($"http://127.0.0.1:{port}/json");
            var targets = System.Text.Json.Nodes.JsonNode.Parse(json)?.AsArray();
            if (targets == null) return;
            int closed = 0;
            foreach (var t in targets)
            {
                var url = t?["url"]?.GetValue<string>() ?? "";
                var id = t?["id"]?.GetValue<string>() ?? "";
                if (url == "about:blank" && !string.IsNullOrEmpty(id))
                {
                    await http.GetAsync($"http://127.0.0.1:{port}/json/close/{id}");
                    closed++;
                }
            }
            if (closed > 0)
                Console.WriteLine($"[ASK] Closed {closed} about:blank tab(s)");
        }
        catch { }
    }

    /// <summary>Count assistant turns — multi-selector for ChatGPT DOM changes.</summary>
    static async Task<int> CountChatGptTurns(CdpClient cdp)
    {
        var result = await cdp.EvalAsync("""
            (() => {
                var c = document.querySelectorAll('[data-message-author-role="assistant"]').length;
                if (c > 0) return '' + c;
                c = document.querySelectorAll('[data-testid*="conversation-turn"]').length;
                if (c > 0) return '' + Math.floor(c / 2);
                c = document.querySelectorAll('article').length;
                if (c > 0) return '' + Math.floor(c / 2);
                return '0';
            })()
            """) ?? "0";
        return int.TryParse(result, out var v) ? v : 0;
    }

    /// <summary>Wait for ChatGPT editor to be ready. Returns the working CSS selector.</summary>
    static async Task<string?> WaitForChatGptEditorA11y(CdpClient cdp)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ChatGptEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        Console.WriteLine("[ASK] Editor not found (a11y selector chain exhausted)");
        return null;
    }

    // Kept for backward compat (returns bool)
    static async Task<bool> WaitForChatGptEditor(CdpClient cdp)
        => await WaitForChatGptEditorA11y(cdp) != null;

    /// <summary>Generic a11y-first editor wait with custom selector chain.</summary>
    static async Task<string?> WaitForEditorA11y(CdpClient cdp, params string[] selectors)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in selectors)
            {
                var found = await cdp.EvalAsync($"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        return null;
    }

    /// <summary>
    /// Send a message in ChatGPT and wait for the response.
    /// A11y-first: finds editor via ARIA selector chain, inserts text via CDP Input.insertText.
    /// </summary>
    static async Task<(bool ok, string? text)> ChatGptSendAndWait(
        CdpClient cdp, string message, int timeoutSec, List<string>? attachFiles = null)
    {
        // ── Phase 0: URL check + turn count (iconified OK) ──
        var currentUrl = await cdp.EvalAsync("location.href") ?? "";
        int prevTurns = await CountChatGptTurns(cdp);

        // NOTE: BringToFront removed — steals OS focus. CDP works on background tabs.

        // ── A11y-first: find editor via selector chain ──
        var editorSel = await WaitForChatGptEditorA11y(cdp);
        if (editorSel == null)
            return (false, null);

        // ── Browser exclusive zone: prompt input → first response turn ──
        using var chatLock = ChromeTabLock.Acquire("ChatGPT");
        if (chatLock == null) return (false, null);

        // ── CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ──
        var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "ChatGPT");

        // ── File attachments (before text) ──
        if (attachFiles?.Count > 0)
            await AttachFilesViaCdp(cdp, attachFiles, editorSel);

        // Tier 1: focusless insert (a11y-first)
        await ClearContentEditable(cdp, editorSel);
        var inserted = await InsertTextContentEditable(cdp, editorSel, message);
        // Verify what's actually in the editor
        var editorContent = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
        Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
        if (!inserted)
        {
            // ── Tier 2: CDP Input.insertText (needs focus) ──
            Console.WriteLine("[ASK] Focusless insert failed, trying CDP Input.insertText...");
            await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
            await Task.Delay(100);
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyDown", ["key"] = "a", ["code"] = "KeyA",
                ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2 // Ctrl
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyUp", ["key"] = "a", ["code"] = "KeyA",
                ["windowsVirtualKeyCode"] = 65, ["modifiers"] = 2
            });
            await Task.Delay(50);
            await cdp.SendAsync("Input.insertText", new System.Text.Json.Nodes.JsonObject
            {
                ["text"] = message
            });
            await Task.Delay(200);

            var verify = await cdp.EvalAsync(
                $"document.querySelector('{editorSel}')?.textContent?.length ?? 0") ?? "0";
            if (verify == "0")
            {
                Console.WriteLine("[ASK] Failed to insert text");
                return (false, null);
            }
        }

        // ── Send: JS click → verify → CDP Enter fallback ──
        // With file attachments, wait for send button to become enabled
        if (attachFiles?.Count > 0)
        {
            for (int bw = 0; bw < 10; bw++)
            {
                var btnState = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('button[data-testid="send-button"]')
                               || document.querySelector('button[aria-label*="보내기"]')
                               || document.querySelector('button[aria-label*="Send"]');
                        return btn ? (btn.disabled ? 'DISABLED' : 'ENABLED') : 'NOT_FOUND';
                    })()
                    """) ?? "NOT_FOUND";
                if (btnState == "ENABLED") break;
                Console.WriteLine($"[ASK] Send button: {btnState}, waiting...");
                await Task.Delay(1000);
            }
        }
        await Task.Delay(500);
        var sendResult = "PENDING";

        // Use turn count for send verification when image is attached
        // (textContent check is unreliable with image attachments)
        int preSendTurns = await CountChatGptTurns(cdp);

        // Tier 1: JS click (works minimized, but React may ignore .click())
        var jsClick = await cdp.EvalAsync("""
            (() => {
                var btn = document.querySelector('button[data-testid="send-button"]')
                       || document.querySelector('button[aria-label*="보내기"]')
                       || document.querySelector('button[aria-label*="Send"]');
                if (!btn || btn.disabled) return 'NO_BTN';
                btn.click();
                return 'CLICKED';
            })()
            """) ?? "NO_BTN";

        if (jsClick == "CLICKED")
        {
            await Task.Delay(1000);
            // Verify send: turn count increase OR editor emptied
            var postTurns = await CountChatGptTurns(cdp);
            if (postTurns > preSendTurns)
            {
                sendResult = "JS_CLICK";
            }
            else
            {
                var remaining = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                sendResult = remaining == "0" ? "JS_CLICK" : "CLICK_NOOP";
            }
        }

        // Tier 2: UIA Invoke on send button (focusless, works minimized)
        if (sendResult != "JS_CLICK")
        {
            Console.WriteLine("[ASK] JS click didn't send, trying UIA invoke...");
            if (TryUiaInvokeSendButton())
            {
                await Task.Delay(1000);
                var postTurns = await CountChatGptTurns(cdp);
                if (postTurns > preSendTurns)
                    sendResult = "UIA_INVOKE";
                else
                {
                    var remaining = await cdp.EvalAsync(
                        $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                    sendResult = remaining == "0" ? "UIA_INVOKE" : "UIA_NOOP";
                }
            }
        }

        // Tier 3: CDP Enter key (needs visible viewport + editor focus)
        if (sendResult != "JS_CLICK" && sendResult != "UIA_INVOKE")
        {
            await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
            await Task.Delay(100);
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
            await cdp.SendAsync("Input.dispatchKeyEvent", new System.Text.Json.Nodes.JsonObject
            {
                ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
            });
            sendResult = "CDP_ENTER";
        }

        // Zoom feedback + focus guard
        zoom?.ShowPass($"sent ({sendResult})");
        zoom?.Dispose();
        GuardCdpFocusTheft(cdp, prevFg, "input-cdp");

        // Check editor after send — should be empty if sent successfully
        var afterSend = await cdp.EvalAsync(
            $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
        Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={prevTurns})");

        // Wait for response start: turn count increase OR streaming/thinking indicators
        // Uses querySelectorAll + textContent (works in background tabs without layout)
        var sw = Stopwatch.StartNew();
        bool responseStarted = false;
        while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 30))
        {
            await Task.Delay(1000);

            // URL change detection (new conversation resets turn count)
            var newUrl = await cdp.EvalAsync("location.href") ?? "";
            if (newUrl != currentUrl && newUrl.Contains("/c/"))
            {
                Console.WriteLine("[ASK] New conversation detected");
                prevTurns = 0;
                currentUrl = newUrl;
            }

            // Multi-signal detection: turn count OR stop button OR thinking marker
            var detectResult = await cdp.EvalAsync(
                "(() => {" +
                "var turns = document.querySelectorAll('[data-message-author-role=\"assistant\"]').length;" +
                "if (turns === 0) turns = Math.floor((document.querySelectorAll('[data-testid*=\"conversation-turn\"]').length || document.querySelectorAll('article').length) / 2);" +
                $"if (turns > {prevTurns}) return 'TURN_' + turns;" +
                "var stop = document.querySelector('button[data-testid=\"stop-button\"]')" +
                "|| document.querySelector('button[aria-label=\"Stop streaming\"]')" +
                "|| document.querySelector('button[aria-label=\"\\uc2a4\\ud2b8\\ub9ac\\ubc0d \\uc911\\uc9c0\"]');" +
                "if (stop) return 'STREAMING';" +
                "if (document.querySelector('.result-thinking')) return 'THINKING';" +
                "return 'WAITING_' + turns;" +
                "})()") ?? "WAITING_0";

            if (detectResult.StartsWith("TURN_") || detectResult == "STREAMING" || detectResult == "THINKING")
            {
                responseStarted = true;
                Console.WriteLine($"[ASK] Response detected: {detectResult}");
                chatLock.Release("first-byte");
                RegisterWaitingTab("chatgpt", cdp);
                await HandoffTabToPeer("chatgpt");
                break;
            }

            if (sw.Elapsed.TotalSeconds > 3)
                Console.WriteLine($"[ASK] Waiting for response... ({detectResult}, {sw.Elapsed.TotalSeconds:F0}s)");
        }
        if (!responseStarted)
        {
            Console.WriteLine("[ASK] No response detected");
            return (false, null);
        }

        // ── Poll Phase 1: wait for streaming/thinking to finish (iconified — no rendering needed) ──
        // Live flush: print new text as it arrives during streaming
        int streamExtensions = 0;
        int blankPageCount = 0;
        int lastFlushedLen = 0;
        bool liveHeaderPrinted = false;
        var knownImageUrls = new HashSet<string>();
        var savedImages = new List<string>();
        sw.Restart();

        while (sw.Elapsed.TotalSeconds < timeoutSec)
        {
            await Task.Delay(1500);

            // Combined check: state + streaming text length + delta text for live flush
            var stateJson = await cdp.EvalAsync($$"""
                (() => {
                    if (!document.body || !document.body.innerHTML || document.body.innerHTML.length < 100)
                        return JSON.stringify({s:'BLANK',len:0,delta:''});
                    var stop = document.querySelector('button[data-testid="stop-button"]')
                            || document.querySelector('button[aria-label="Stop streaming"]')
                            || document.querySelector('button[aria-label="스트리밍 중지"]');
                    var thinking = !!document.querySelector('.result-thinking');
                    // Detect DALL-E image generation in progress (spinner/progress within assistant message)
                    var lastAssist = document.querySelector('[data-message-author-role="assistant"]:last-of-type')
                                  || document.querySelector('article:last-of-type');
                    var imgGen = lastAssist && (
                        !!lastAssist.querySelector('[role="progressbar"]')
                        || !!lastAssist.querySelector('.animate-spin')
                        || !!lastAssist.querySelector('svg.animate-spin')
                        || !!lastAssist.querySelector('[data-testid="image-gen-progress"]')
                        || !!(lastAssist.textContent||'').match(/(?:이미지|image|사진|그림|생성|creating|generating)/i)
                    );
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    var txt = msgs.length > 0 ? (msgs[msgs.length-1].textContent||'').trim() : '';
                    var hasText = txt.length > 0;
                    var state;
                    if (!stop && !thinking && !imgGen) state = hasText ? 'DONE' : 'DONE_EMPTY';
                    else if (imgGen) state = 'IMG_GEN';
                    else if (thinking) state = hasText ? 'THINKING_HAS_TEXT' : 'THINKING';
                    else state = hasText ? 'STREAMING_HAS_TEXT' : 'STREAMING';
                    var delta = txt.length > {{lastFlushedLen}} ? txt.substring({{lastFlushedLen}}) : '';
                    return JSON.stringify({s:state,len:txt.length,delta:delta});
                })()
                """) ?? "";

            // Parse combined response
            string state = "";
            int textLen = 0;
            string delta = "";
            try
            {
                var jo = JsonDocument.Parse(stateJson).RootElement;
                state = jo.GetProperty("s").GetString() ?? "";
                textLen = jo.GetProperty("len").GetInt32();
                delta = jo.GetProperty("delta").GetString() ?? "";
            }
            catch
            {
                state = stateJson; // fallback: treat as plain state string
            }

            // Live flush: print new text delta
            if (delta.Length > 0)
            {
                if (!liveHeaderPrinted)
                {
                    Console.WriteLine();
                    Console.WriteLine("── ChatGPT (streaming) ──");
                    liveHeaderPrinted = true;
                }
                Console.Write(delta);
                Console.Out.Flush();
                lastFlushedLen = textLen;
            }

            // Detect generated images in response (inline marker output)
            var newImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
            savedImages.AddRange(newImages);

            // Image generation in progress — keep waiting (up to 90s for DALL-E)
            if (state == "IMG_GEN")
            {
                if (!liveHeaderPrinted)
                {
                    Console.WriteLine();
                    Console.WriteLine("── ChatGPT (streaming) ──");
                    liveHeaderPrinted = true;
                }
                Console.Write(".");
                Console.Out.Flush();
                continue;
            }

            if (state == "DONE" || state == "DONE_EMPTY")
            {
                // If very little text and early in stream, wait a bit more
                // (ChatGPT shows "이미지 분석 중" then goes idle briefly before actual response)
                if (textLen < 50 && sw.Elapsed.TotalSeconds < 60)
                {
                    if (textLen > 0) Console.Write(".");
                    Console.Out.Flush();
                    continue; // keep polling — likely just a transient idle gap
                }
                // Final image check before completion
                var finalImages = await DetectAndDownloadImages(cdp, knownImageUrls, "gpt");
                savedImages.AddRange(finalImages);
                if (liveHeaderPrinted) Console.WriteLine(); // newline after streamed text
                Console.WriteLine($"[ASK] Streaming complete ({sw.Elapsed.TotalSeconds:F0}s)");
                if (savedImages.Count > 0)
                    Console.WriteLine($"[ASK] Downloaded {savedImages.Count} generated image(s)");
                break;
            }

            // Blank/broken page detection — bail out early
            if (state == "BLANK" || string.IsNullOrEmpty(state))
            {
                blankPageCount++;
                Console.WriteLine($"[ASK] Page blank/broken ({blankPageCount}/3), {sw.Elapsed.TotalSeconds:F0}s");
                if (blankPageCount >= 3)
                {
                    Console.WriteLine("[ASK] Page unresponsive — aborting poll");
                    break;
                }
                continue;
            }

            blankPageCount = 0; // reset on valid response

            // First-byte timeout: 20s of streaming with no text → likely stuck
            // Exempt: THINKING state (o3/o4 models can think for 30s+ before first text)
            bool isThinking = state == "THINKING" || state == "THINKING_HAS_TEXT";
            bool hasResponseText = state == "STREAMING_HAS_TEXT" || state == "THINKING_HAS_TEXT";

            // Streaming handoff: response text appearing → give active tab to peer
            if (hasResponseText)
                await HandoffTabToPeer("chatgpt");

            if (!hasResponseText && !isThinking && lastFlushedLen == 0 && sw.Elapsed.TotalSeconds >= 20)
            {
                Console.WriteLine("[ASK] No response text after 20s — aborting (first-byte timeout)");
                break;
            }

            // Only print poll status when NOT live-flushing (avoid noise)
            if (!liveHeaderPrinted)
                Console.WriteLine($"[ASK] Poll: {(isThinking ? "thinking" : "streaming")}{(hasResponseText ? "+" : "")}, {sw.Elapsed.TotalSeconds:F0}s");

            // Extend timeout while actively streaming/thinking (max 1 extension = 2x original timeout)
            if (sw.Elapsed.TotalSeconds > timeoutSec * 0.8 && streamExtensions < 1)
            {
                streamExtensions++;
                Console.WriteLine("[ASK] Still streaming/thinking, extending timeout...");
                sw.Restart();
            }
        }

        // ── Poll Phase 2: text extraction ──
        // NOTE: BringToFront removed — innerText works on background tabs too.
        await Task.Delay(300);

        // Scroll into view + hydrate + extract
        string? finalText = null;
        for (int extractAttempt = 0; extractAttempt < 3; extractAttempt++)
        {
            await cdp.EvalAsync("""
                (() => {
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    if (msgs.length > 0) msgs[msgs.length-1].scrollIntoView({block:'end',behavior:'instant'});
                })()
                """);
            await Task.Delay(300); // let React hydrate after scroll

            var extractJson = await cdp.EvalAsync("""
                (() => {
                    var msgs = document.querySelectorAll('[data-message-author-role="assistant"]');
                    if (msgs.length === 0) msgs = document.querySelectorAll('article');
                    if (msgs.length === 0) return '';
                    var last = msgs[msgs.length - 1];

                    var md = last.querySelector('.markdown:not(.result-thinking):not(.result-thinking-content)')
                          || last.querySelector('.result-streaming:not(.result-thinking)')
                          || last.querySelector('.markdown:not(.result-thinking):not(.result-thinking-content)');

                    var txt = md ? (md.innerText || md.textContent || '') : '';
                    if (!txt && !last.querySelector('.result-thinking')) txt = last.innerText || last.textContent || '';

                    // Fallback: innerHTML → strip tags
                    if (!txt && md && md.innerHTML) {
                        var tmp = document.createElement('div');
                        tmp.innerHTML = md.innerHTML;
                        txt = tmp.innerText || tmp.textContent || '';
                    }
                    // Fallback: Selection API
                    if (!txt && md) {
                        try { var r = document.createRange(); r.selectNodeContents(md); txt = r.toString(); r.detach(); } catch(e) {}
                    }
                    return txt?.trim() || '';
                })()
                """) ?? "";

            if (!string.IsNullOrWhiteSpace(extractJson))
            {
                finalText = extractJson;
                break;
            }
            // Hydration retry
            if (extractAttempt < 2)
                await Task.Delay(500);
        }

        if (!string.IsNullOrEmpty(finalText))
        {
            Console.WriteLine($"[ASK] Response ({finalText.Length} chars, {sw.Elapsed.TotalSeconds:F0}s)");

            // Done — hand off tab to peer if still waiting
            await HandoffTabToPeer("chatgpt");

            // ── Final answer ready: focusless restore to show answer to user ──
            ShowChromeAnswer(cdp);

            return (true, finalText);
        }
        Console.WriteLine("[ASK] Timeout -- no response");
        return (false, null);
    }

    /// <summary>
    /// 최종 답변 표시: Chrome을 포커스리스 리스토어하여 사용자에게 답변 페이지를 보여준다.
    /// ask 플로우 전체는 아이콘화 상태로 진행 (CDP는 미니마이즈에서 정상 동작).
    /// 최종 답변 추출 후에만 호출하여 결과를 시각적으로 확인할 수 있게 한다.
    /// </summary>
    static void ShowChromeAnswer(CdpClient cdp)
    {
        var chromeHwnd = cdp.GetChromeWindowHandle();
        if (chromeHwnd == IntPtr.Zero || !NativeMethods.IsIconic(chromeHwnd))
            return; // not minimized — already visible

        var title = WKAppBot.Win32.Window.WindowFinder.GetWindowText(chromeHwnd);
        Console.WriteLine($"[ASK] Showing answer — focusless restore \"{title}\"");

        // Focusless restore: SW_SHOWNOACTIVATE won't steal focus
        NativeMethods.ShowWindow(chromeHwnd, 4); // SW_SHOWNOACTIVATE
    }

    // ── UIA Send Button ──
    // Tier 2 fallback: find and invoke the send button via UIA (completely focusless).
    // Searches Chrome/ChatGPT windows for buttons matching send-related names.
    static readonly string[] SendButtonNames = ["제출", "보내기", "Send message", "Send", "메시지 보내기"];

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
            Console.WriteLine($"[ASK] UIA send failed: {ex.Message}");
        }
        return false;
    }

    // ── Tab Handoff: disabled ──
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

    // ── Chrome Tab Lock ──
    // Cross-process mutex: only one ask command can own the browser at a time.
    // Acquired before prompt input, auto-released after 9s or when first byte arrives.
    // IDisposable — always safe, no manual release needed.

    // Semaphore (not Mutex) — async/await can resume on different threads,
    // and Mutex.ReleaseMutex() requires the same thread. Semaphore is thread-agnostic.
    static readonly Semaphore ChromeTabSemaphore = new(1, 1, @"Global\WKAppBot_ChromeTabLock");

    sealed class ChromeTabLock : IDisposable
    {
        readonly string _aiName;
        readonly Timer _autoRelease;
        int _released;

        ChromeTabLock(string aiName)
        {
            _aiName = aiName;
            // Auto-release after 9 seconds (safety net — prevents deadlock)
            _autoRelease = new Timer(_ => Release("auto-9s"), null, 9000, Timeout.Infinite);
        }

        public static ChromeTabLock? Acquire(string aiName, int timeoutMs = 15000)
        {
            Console.WriteLine($"[ASK] Waiting for browser lock ({aiName})...");
            try
            {
                if (ChromeTabSemaphore.WaitOne(timeoutMs))
                {
                    Console.WriteLine($"[ASK] Browser lock acquired ({aiName})");
                    return new ChromeTabLock(aiName);
                }
            }
            catch (AbandonedMutexException)
            {
                Console.WriteLine($"[ASK] Browser lock recovered ({aiName})");
                return new ChromeTabLock(aiName);
            }
            Console.WriteLine($"[ASK] Browser lock timeout ({aiName})");
            return null;
        }

        public void Release(string reason = "done")
        {
            if (Interlocked.CompareExchange(ref _released, 1, 0) != 0) return;
            _autoRelease.Dispose();
            try { ChromeTabSemaphore.Release(); } catch { }
            Console.WriteLine($"[ASK] Browser lock released ({_aiName}, {reason})");
        }

        public void Dispose() => Release("dispose");
    }

    // ── Claude.ai ──

    // Claude.ai uses ProseMirror editor — innerHTML/execCommand fail, must use ClipboardEvent paste
    static readonly string[] ClaudeEditorSelectors =
    [
        "div.tiptap.ProseMirror",                          // Claude.ai ProseMirror (no attr filter — most reliable)
        "div.tiptap.ProseMirror[contenteditable='true']",  // With attr
        ".ProseMirror[contenteditable='true']",            // ProseMirror generic
        "[contenteditable='true']",                        // Generic fallback
    ];

    static async Task<string?> WaitForClaudeEditorA11y(CdpClient cdp)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            foreach (var sel in ClaudeEditorSelectors)
            {
                var found = await cdp.EvalAsync(
                    $"document.querySelector('{sel}') ? 'yes' : 'no'");
                if (found == "yes") return sel;
            }
            await Task.Delay(500);
        }
        Console.WriteLine("[ASK] Claude editor not found (selector chain exhausted)");
        return null;
    }

    /// <summary>Insert text into Claude.ai ProseMirror via ClipboardEvent paste (only reliable method).</summary>
    static async Task<bool> InsertTextClaudeProseMirror(CdpClient cdp, string selector, string text)
    {
        var escaped = text.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");

        var result = await cdp.EvalAsync($$"""
            (() => {
                var el = document.querySelector('{{selector}}');
                if (!el) return 'NOT_FOUND';
                el.focus();
                var dt = new DataTransfer();
                dt.setData('text/plain', '{{escaped}}');
                var pe = new ClipboardEvent('paste', {clipboardData: dt, bubbles: true, cancelable: true});
                el.dispatchEvent(pe);
                return el.textContent.length > 0 ? 'OK' : 'EMPTY';
            })()
            """);
        if (result == "OK") return true;

        // Fallback: CDP Input.insertText
        Console.WriteLine($"[ASK] ClipboardEvent paste result: {result}, trying Input.insertText...");
        await cdp.EvalAsync($"document.querySelector('{selector}')?.focus()");
        await Task.Delay(100);
        await cdp.SendAsync("Input.insertText", new JsonObject { ["text"] = text });
        await Task.Delay(200);
        var verify = await cdp.EvalAsync(
            $"document.querySelector('{selector}')?.textContent?.length ?? 0") ?? "0";
        return verify != "0";
    }

    /// <summary>Count Claude.ai assistant turns.</summary>
    static async Task<int> CountClaudeTurns(CdpClient cdp)
    {
        var result = await cdp.EvalAsync("""
            (() => {
                var c = document.querySelectorAll('[data-testid="user-message"]').length;
                if (c > 0) return '' + c;
                c = document.querySelectorAll('[data-is-streaming]').length;
                if (c > 0) return '1';
                return '0';
            })()
            """) ?? "0";
        return int.TryParse(result, out var v) ? v : 0;
    }

    static int AskClaude(string question, bool slackReport, int timeoutSec, bool newTab, bool newSession = false)
    {
        Console.WriteLine($"[ASK] Claude: {question}");

        // UIA tab routing: switch to Claude tab before CDP connects (focusless)
        if (!newTab) EnsureTabViaGrap("Chrome", "Claude");

        var targetTag = BuildAskTargetTag("claude");
        var cdp = EnsureCdpConnection(preferredHost: "claude.ai", newTab: newTab, targetTag: targetTag);
        if (cdp == null) return 1;

        LaunchAppBotEyeIfNeeded(9222);
        cdp.ApplyTargetTagAsync(targetTag).GetAwaiter().GetResult();

        var task = Task.Run(async () =>
        {
            try
            {
                // ── Phase 1: Navigate ──
                var currentUrl = await cdp.EvalAsync("location.href") ?? "";
                Console.WriteLine($"[ASK] Tab URL: {currentUrl}");
                if (newSession || !currentUrl.Contains("claude.ai"))
                {
                    Console.WriteLine(newSession ? "[ASK] New session — navigating to fresh Claude..." : "[ASK] Navigating to Claude...");
                    await cdp.NavigateAsync("https://claude.ai/new");
                    await Task.Delay(3000);
                }
                else
                {
                    Console.WriteLine($"[ASK] Reusing Claude session");
                }

                // NOTE: BringToFront removed — steals OS focus. CDP works on background tabs.
                await Task.Delay(1000);

                // ── Phase 2: Find editor ──
                var editorSel = await WaitForClaudeEditorA11y(cdp);
                if (editorSel == null)
                    return (false, (string?)null);
                Console.WriteLine($"[ASK] Editor found: {editorSel}");

                // ── Phase 3: Check existing turns ──
                int existingTurns = await CountClaudeTurns(cdp);
                if (existingTurns > 0)
                    Console.WriteLine($"[ASK] Reusing session ({existingTurns} turns)");

                // ── Phase 4: Insert text + send ──
                using var chatLock = ChromeTabLock.Acquire("Claude");
                if (chatLock == null) return (false, (string?)null);

                // ── CDP InputReadiness: blocker check + minimize restore + zoom + focus guard ──
                var (cdpReady, prevFg, zoom) = await EnsureCdpReadyAsync(cdp, "input-cdp", editorSel, "Claude");

                var inserted = await InsertTextClaudeProseMirror(cdp, editorSel, question);
                var editorContent = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.substring(0,80) || 'EMPTY'") ?? "EMPTY";
                Console.WriteLine($"[ASK] After insert: {(inserted ? "OK" : "FAIL")}, editor=[{editorContent}]");
                if (!inserted)
                {
                    zoom?.ShowFail("insert failed");
                    zoom?.Dispose();
                    Console.WriteLine("[ASK] Failed to insert text into Claude editor");
                    return (false, (string?)null);
                }

                // ── Send: click button ──
                await Task.Delay(500);
                int preSendTurns = await CountClaudeTurns(cdp);
                var sendResult = "PENDING";

                // Tier 1: JS click on send button (multi-selector fallback for DOM changes)
                var jsClick = await cdp.EvalAsync("""
                    (() => {
                        var btn = document.querySelector('[data-testid="chat-input-grid-area"] button[type="submit"]')
                               || document.querySelector('[data-testid="chat-input"] button[type="submit"]')
                               || document.querySelector('button[aria-label="메시지 보내기"]')
                               || document.querySelector('button[aria-label="Send Message"]')
                               || document.querySelector('button[aria-label*="Send"]');
                        if (!btn || btn.disabled) return 'NO_BTN';
                        btn.click();
                        return 'CLICKED';
                    })()
                    """) ?? "NO_BTN";

                if (jsClick == "CLICKED")
                {
                    await Task.Delay(1000);
                    var postTurns = await CountClaudeTurns(cdp);
                    if (postTurns > preSendTurns)
                        sendResult = "JS_CLICK";
                    else
                    {
                        var remaining = await cdp.EvalAsync(
                            $"document.querySelector('{editorSel}')?.textContent?.trim()?.length ?? 99") ?? "99";
                        sendResult = remaining == "0" ? "JS_CLICK" : "CLICK_NOOP";
                    }
                }

                // Tier 2: CDP Enter key
                if (sendResult != "JS_CLICK")
                {
                    Console.WriteLine("[ASK] JS click didn't send, trying Enter key...");
                    await cdp.EvalAsync($"document.querySelector('{editorSel}')?.focus()");
                    await Task.Delay(100);
                    await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                    {
                        ["type"] = "keyDown", ["key"] = "Enter", ["code"] = "Enter",
                        ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                    });
                    await cdp.SendAsync("Input.dispatchKeyEvent", new JsonObject
                    {
                        ["type"] = "keyUp", ["key"] = "Enter", ["code"] = "Enter",
                        ["windowsVirtualKeyCode"] = 13, ["nativeVirtualKeyCode"] = 13
                    });
                    sendResult = "CDP_ENTER";
                }

                // Zoom feedback + focus guard
                zoom?.ShowPass($"sent ({sendResult})");
                zoom?.Dispose();
                GuardCdpFocusTheft(cdp, prevFg, "input-cdp");

                var afterSend = await cdp.EvalAsync(
                    $"document.querySelector('{editorSel}')?.textContent?.length ?? -1") ?? "-1";
                Console.WriteLine($"[ASK] Sent! (send={sendResult}, editorLen={afterSend}, prevTurns={preSendTurns})");

                // ── Phase 5: Wait for response ──
                var sw = Stopwatch.StartNew();
                bool responseStarted = false;
                while (sw.Elapsed.TotalSeconds < Math.Min(timeoutSec, 30))
                {
                    await Task.Delay(1000);

                    var detectResult = await cdp.EvalAsync("""
                        (() => {
                            if (document.querySelector('[data-is-streaming="true"]')) return 'STREAMING';
                            if (document.querySelector('[data-is-streaming="false"]')) return 'DONE';
                            var msgs = document.querySelectorAll('[data-testid="user-message"]');
                            return 'WAITING_' + msgs.length;
                        })()
                        """) ?? "WAITING_0";

                    if (detectResult == "STREAMING" || detectResult == "DONE")
                    {
                        responseStarted = true;
                        Console.WriteLine($"[ASK] Response detected: {detectResult}");
                        chatLock.Release("first-byte");
                        break;
                    }

                    if (sw.Elapsed.TotalSeconds > 3)
                        Console.WriteLine($"[ASK] Waiting for response... ({detectResult}, {sw.Elapsed.TotalSeconds:F0}s)");
                }
                if (!responseStarted)
                {
                    Console.WriteLine("[ASK] No response detected");
                    return (false, (string?)null);
                }

                // ── Phase 6: Poll for completion ──
                int lastFlushedLen = 0;
                bool liveHeaderPrinted = false;
                sw.Restart();
                while (sw.Elapsed.TotalSeconds < timeoutSec)
                {
                    await Task.Delay(1500);

                    // Get streaming state + latest response text
                    var pollResult = await cdp.EvalAsync("""
                        (() => {
                            var streaming = document.querySelector('[data-is-streaming="true"]');
                            var msgs = document.querySelectorAll('[data-is-streaming]');
                            var last = msgs.length > 0 ? msgs[msgs.length - 1] : null;
                            var text = last ? last.textContent : '';
                            var state = streaming ? 'STREAMING' : 'DONE';
                            return JSON.stringify({state: state, len: text.length, text: text.substring(0, 3000)});
                        })()
                        """) ?? "{}";

                    try
                    {
                        var poll = JsonSerializer.Deserialize<JsonElement>(pollResult);
                        var state = poll.GetProperty("state").GetString() ?? "UNKNOWN";
                        var text = poll.GetProperty("text").GetString() ?? "";
                        var len = poll.GetProperty("len").GetInt32();

                        // Live flush
                        if (len > lastFlushedLen && text.Length > 0)
                        {
                            if (!liveHeaderPrinted)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("── Claude streaming ──");
                                Console.ResetColor();
                                liveHeaderPrinted = true;
                            }
                            var newText = text.Length > lastFlushedLen ? text[lastFlushedLen..] : "";
                            if (newText.Length > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write(newText);
                                Console.ResetColor();
                            }
                            lastFlushedLen = len;
                        }

                        if (state == "DONE")
                        {
                            if (liveHeaderPrinted) Console.WriteLine();
                            Console.WriteLine($"[ASK] Response complete ({len} chars, {sw.Elapsed.TotalSeconds:F0}s)");
                            return (true, text);
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"[ASK] Poll parse error: {pollResult[..Math.Min(80, pollResult.Length)]}");
                    }
                }

                // Timeout — return what we have
                var finalText = await cdp.EvalAsync("""
                    (() => {
                        var msgs = document.querySelectorAll('[data-is-streaming]');
                        var last = msgs.length > 0 ? msgs[msgs.length - 1] : null;
                        return last ? last.textContent.substring(0, 3000) : '';
                    })()
                    """) ?? "";
                if (liveHeaderPrinted) Console.WriteLine();
                Console.WriteLine($"[ASK] Timeout ({timeoutSec}s) — partial response ({finalText.Length} chars)");
                return (finalText.Length > 0, finalText.Length > 0 ? finalText : null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ASK] Error: {ex.Message}");
                return (false, (string?)null);
            }
        });

        var (ok, answer) = task.GetAwaiter().GetResult();

        if (ok && answer != null)
        {
            Console.WriteLine("[ASK_ANSWER_BEGIN]");
            Console.WriteLine(answer.Length > 2000 ? answer[..2000] + "\n... (truncated)" : answer);
            Console.WriteLine("[ASK_ANSWER_END]");

            if (slackReport)
                ReportToSlack("Claude", question, answer);
        }

        cdp.Dispose();
        return ok ? 0 : 1;
    }

    // ── Slack Report ──

    static void ReportToSlack(string aiName, string question, string answer)
    {
        try
        {
            var config = LoadSlackConfig();
            if (config == null) return;

            var botToken = config["bot_token"]?.GetValue<string>();
            var channel = config["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;

            var truncated = answer.Length > 1500 ? answer[..1500] + "\n... (truncated)" : answer;
            var msg = $"*[{aiName} 답변]*\n> Q: {question}\n\n{truncated}";
            var (ok, _) = SlackSendViaApi(botToken, channel, msg, username: BotUsername).GetAwaiter().GetResult();
            if (ok)
                Console.WriteLine($"[ASK] Reported to Slack");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ASK] Slack report failed: {ex.Message}");
        }
    }
}
