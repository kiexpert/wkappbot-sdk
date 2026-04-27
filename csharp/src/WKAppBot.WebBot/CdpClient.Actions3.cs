using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.WebBot;

public sealed partial class CdpClient
{
    /// <summary>
    /// Activate this tab in Chrome (bring to front).
    /// Focusless-safe: ensures Chrome is iconic before Page.bringToFront so
    /// tab activation happens internally without stealing OS focus.
    /// </summary>
    public async Task BringToFrontAsync()
    {
        // Check if our target is already the active tab in Chrome (via CDP)
        // If so, Page.bringToFront is a no-op -- skip the minimize dance
        bool alreadyActive = false;
        try
        {
            var visible = await EvalAsync("document.visibilityState") ?? "";
            alreadyActive = visible == "visible";
        }
        catch { }

        if (!alreadyActive)
        {
            // Tab is not active -> minimize Chrome before activation to prevent OS focus theft
            var hwnd = GetChromeWindowHandle();
            bool didMinimize = false;
            if (hwnd != IntPtr.Zero && !IsIconic(hwnd))
            {
                Console.Error.WriteLine($"[CDP] Chrome minimized for tab switch (hwnd={hwnd:X8})");
                ShowWindowNative(hwnd, 8); // SW_SHOWMINNOACTIVE: minimize without activating next window
                ScheduleMinimizeDump("bring-to-front-tab-switch", hwnd);
                didMinimize = true;
                // Poll until minimized (replaces fixed 50ms delay -- Chrome may not be iconic yet)
                var preWb = await GetWindowForTargetAsync();
                if (preWb != null)
                    await WaitForWindowStateAsync(preWb.Value.windowId, "minimized", timeoutMs: 1000);
            }
            await SendAsync("Page.bringToFront");
            // Restore after tab switch -- poll until normal (replaces fixed 200ms delay)
            if (didMinimize)
            {
                RestoreChromeNoActivate();
                var postWb = await GetWindowForTargetAsync();
                if (postWb != null)
                    await WaitForWindowStateAsync(postWb.Value.windowId, "normal", timeoutMs: 1000);
            }
        }
    }

    /// <summary>
    /// Bring this tab to the OS foreground (recovery use only -- steals focus).
    /// Uses Page.bringToFront which activates the tab AND brings Chrome to front.
    /// </summary>
    /// <summary>
    /// Make Chrome window visible WITHOUT stealing OS focus (SW_SHOWNOACTIVATE).
    /// Renderer becomes active + compositor runs, but foreground stays with caller.
    /// Use before CDP input that needs a visible (non-minimized) renderer.
    /// Call MinimizeChromeAsync() when done to restore minimized state.
    /// </summary>
    public void RestoreChromeNoActivate()
    {
        var hwnd = GetChromeWindowHandle();
        if (hwnd == IntPtr.Zero) return;
        // SW_SHOWNOACTIVATE=4: visible, not minimized, does NOT steal focus
        ShowWindowNative(hwnd, 4);
        CancelMinimizeDump("restore-no-activate");
        Console.WriteLine("[CDP] Chrome restored (SW_SHOWNOACTIVATE -- no focus steal)");
    }

    public void MinimizeChrome()
    {
        var hwnd = GetChromeWindowHandle();
        if (hwnd == IntPtr.Zero) { Console.WriteLine("[CDP] MinimizeChrome: hwnd=zero (Chrome not found)"); return; }
        // SW_SHOWMINNOACTIVE=8: minimize without activating next window (no focus steal)
        ShowWindowNative(hwnd, 8);
        ScheduleMinimizeDump("explicit-minimize", hwnd);
        var stack = new System.Diagnostics.StackTrace(1, true).ToString();
        var caller = stack.Length > 200 ? stack[..200] : stack;
        Console.Error.WriteLine($"[CDP:MINIMIZE] Chrome minimized (hwnd={hwnd:X8})\n  callstack: {caller.Replace("\n", "\n  ")}");
    }

    /// <summary>Legacy recovery -- replaced by RestoreChromeNoActivate. Kept as no-op.</summary>
    public Task BringTabToFrontAsync() => Task.CompletedTask;

    /// <summary>
    /// Activate this tab in Chrome.
    /// WARNING: Target.activateTarget DOES steal OS foreground window when Chrome is in background --
    /// Chrome calls SetForegroundWindow internally as part of tab activation.
    /// Unlike Page.bringToFront (even more aggressive), but still NOT truly focusless.
    ///
    /// Truly focusless alternative: skip this call entirely.
    /// CDP Runtime.evaluate / DOM commands work on background tabs via targetId WebSocket.
    /// Only call this when the tab MUST be visible (e.g. rendering-dependent operations).
    /// </summary>
    /// <summary>No-op -- tab activation not needed; CDP operates on background tabs via targetId.</summary>
    public Task ActivateTabAsync() => Task.CompletedTask;

    /// <summary>
    /// Intercept file chooser dialog and provide files programmatically (fully focusless).
    /// Uses Input.dispatchMouseEvent (trusted gesture) so Chrome opens the file chooser,
    /// which is intercepted by Page.setInterceptFileChooserDialog BEFORE the native OS dialog
    /// appears -> no focus stealing at all.
    /// Steps: 1) Enable interception 2) Trusted-click upload button 3) Wait for fileChooserOpened
    ///         4) If menu appeared, trusted-click menu item + wait again 5) handleFileChooser
    /// </summary>
    public async Task<bool> SetFileViaChooserAsync(string absolutePath, int timeoutMs = 5000)
    {
        try
        {
            // Enable file chooser interception BEFORE the click -- intercepts before OS dialog opens
            await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = true });
            _fileChooserTcs = new TaskCompletionSource<JsonNode?>();

            // Get upload button coords for trusted gesture
            var btnInfo = await EvalAsync("""
                (() => {
                    var btn = document.querySelector('button[aria-label*="파일 업로드"]')
                           || document.querySelector('button[aria-label*="Upload"]')
                           || document.querySelector('button[aria-label*="Attach"]')
                           || document.querySelector('button[aria-label*="첨부"]')
                           || document.querySelector('button[aria-label*="Add file"]');
                    if (!btn) return 'NO_BTN';
                    var r = btn.getBoundingClientRect();
                    return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + (btn.getAttribute('aria-label') || '');
                })()
            """);
            Console.WriteLine($"[CDP] FileChooser btn: {btnInfo}");
            if (btnInfo == "NO_BTN") return false;

            // Trusted gesture click -- Chrome treats this as real user input for file chooser
            var btnCoords = btnInfo!.Split(':')[0].Split(',');
            await TrustedClickAsync(int.Parse(btnCoords[0]), int.Parse(btnCoords[1]));

            // Wait for file chooser event (direct open -- no menu)
            using var cts = new CancellationTokenSource(timeoutMs);
            cts.Token.Register(() => _fileChooserTcs.TrySetCanceled());

            try
            {
                await _fileChooserTcs.Task;
            }
            catch (TaskCanceledException)
            {
                // Menu opened instead of direct file chooser -- find and trusted-click menu item
                var menuInfo = await EvalAsync("""
                    (() => {
                        var items = document.querySelectorAll('[role=menuitem], [role=option]');
                        for (var item of items) {
                            var t = (item.textContent || '').trim();
                            if (t === '파일 업로드' || t === 'Upload file' || t === 'Upload'
                                || t.includes('컴퓨터') || t.includes('Computer') || t.includes('내 컴퓨터')) {
                                var r = item.getBoundingClientRect();
                                return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + t;
                            }
                        }
                        // Broader fallback
                        var all = document.querySelectorAll('[role=menuitem], [role=option], li, button');
                        for (var item of all) {
                            var t = (item.textContent || '').trim();
                            if (t && (t.includes('업로드') || t.includes('Upload'))) {
                                var r = item.getBoundingClientRect();
                                return Math.round(r.x + r.width/2) + ',' + Math.round(r.y + r.height/2) + ':' + t;
                            }
                        }
                        return 'NO_MENU_ITEM';
                    })()
                """);
                Console.WriteLine($"[CDP] FileChooser menu: {menuInfo}");
                if (menuInfo == "NO_MENU_ITEM") return false;

                // Re-enable interception + reset TCS before trusted-click
                await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = true });
                _fileChooserTcs = new TaskCompletionSource<JsonNode?>();

                var menuCoords = menuInfo!.Split(':')[0].Split(',');
                await TrustedClickAsync(int.Parse(menuCoords[0]), int.Parse(menuCoords[1]));

                using var cts2 = new CancellationTokenSource(12000); // 12s -- React menu click may take time
                cts2.Token.Register(() => _fileChooserTcs.TrySetCanceled());
                try { await _fileChooserTcs.Task; }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("[CDP] FileChooser: no event after menu trusted-click -- trying speculative handleFileChooser...");
                    // Chrome may still be holding the pending chooser even after our TCS timed out.
                    // Speculatively send handleFileChooser -- if Chrome accepts it, the file is set.
                    try
                    {
                        var fp2 = absolutePath.Replace('\\', '/');
                        await SendAsync("Page.handleFileChooser", new JsonObject
                        {
                            ["action"] = "accept",
                            ["files"] = new JsonArray { fp2 },
                        });
                        Console.WriteLine("[CDP] FileChooser: speculative accept sent -- file likely set");
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine($"[CDP] FileChooser: speculative accept failed: {ex2.Message}");
                    }
                    return false;
                }
            }

            // Accept -- Chrome provides file to the page without opening native OS dialog
            var filePath = absolutePath.Replace('\\', '/');
            await SendAsync("Page.handleFileChooser", new JsonObject
            {
                ["action"] = "accept",
                ["files"] = new JsonArray { filePath },
            });
            Console.WriteLine($"[CDP] FileChooser: accepted (focusless)");
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CDP] FileChooser error: {ex.Message}");
            return false;
        }
        finally
        {
            _fileChooserTcs = null;
            try { await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = false }); }
            catch { }
        }
    }

    /// <summary>Send a trusted mouse click via CDP Input.dispatchMouseEvent (page coords).</summary>
    public async Task TrustedClickAtAsync(int x, int y) => await TrustedClickAsync(x, y);

    async Task TrustedClickAsync(int x, int y)
    {
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
        {
            ["type"] = "mousePressed", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
        await Task.Delay(50);
        await SendAsync("Input.dispatchMouseEvent", new JsonObject
        {
            ["type"] = "mouseReleased", ["x"] = x, ["y"] = y,
            ["button"] = "left", ["clickCount"] = 1
        });
    }

    /// <summary>
    /// Disable CDP file chooser interception so the native OS file dialog can open.
    /// </summary>
    public async Task DisableFileChooserInterception()
    {
        try { await SendAsync("Page.setInterceptFileChooserDialog", new JsonObject { ["enabled"] = false }); }
        catch { }
    }

    // -- Hotkey / Keyboard dispatch --------------------------------

    /// <summary>
    /// CDP Input.dispatchKeyEvent -- 포커스리스 키 이벤트 발송 (탭 내 포커스 기준).
    /// modifiers: 0=none, 1=Alt, 2=Ctrl, 4=Meta, 8=Shift (OR 조합)
    /// </summary>
    public async Task DispatchKeyAsync(string key, int modifiers = 0, string code = "")
    {
        var obj = new JsonObject
        {
            ["type"] = "keyDown", ["key"] = key,
            ["modifiers"] = modifiers,
        };
        if (!string.IsNullOrEmpty(code)) obj["code"] = code;
        await SendAsync("Input.dispatchKeyEvent", obj);
        await Task.Delay(20);
        obj["type"] = "keyUp";
        await SendAsync("Input.dispatchKeyEvent", obj);
    }

    /// <summary>
    /// DOM 전체에서 accesskey + aria-keyshortcuts 속성을 스캔하여 핫키 맵 반환.
    /// 반환: List of (label, accessKey, keyshortcuts, selector)
    /// label = innerText / aria-label / title 순으로 추출.
    /// </summary>
    public async Task<List<CdpHotkeyEntry>> GetHotkeyMapAsync()
    {
        const string js = """
            (function() {
              var all = document.querySelectorAll('[accesskey],[aria-keyshortcuts]');
              var result = [];
              all.forEach(function(el) {
                var label = (el.innerText || el.getAttribute('aria-label') ||
                             el.getAttribute('title') || el.getAttribute('value') || '').trim()
                              .replace(/\s+/g,' ').substring(0, 80);
                var ak  = el.getAttribute('accesskey') || '';
                var aks = el.getAttribute('aria-keyshortcuts') || '';
                var sel = '';
                if (el.id) sel = '#' + el.id;
                else if (el.className) sel = el.tagName.toLowerCase() + '.' + el.className.trim().split(/\s+/)[0];
                else sel = el.tagName.toLowerCase();
                result.push({ label: label, accesskey: ak, keyshortcuts: aks, selector: sel });
              });
              return JSON.stringify(result);
            })()
            """;
        var json = await EvalAsync(js);
        if (string.IsNullOrWhiteSpace(json) || json == "null") return [];
        json = json.Trim('"').Replace("\\\"", "\"").Replace("\\\\", "\\");
        try
        {
            var arr = System.Text.Json.JsonSerializer.Deserialize<List<CdpHotkeyEntry>>(json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return arr ?? [];
        }
        catch { return []; }
    }

    /// <summary>
    /// aria-keyshortcuts / accesskey 문자열을 파싱하여 CDP DispatchKeyAsync 호출.
    /// "Alt+S", "Ctrl+Shift+S", "s" (accesskey -> Alt+key) 등 지원.
    /// </summary>
    public async Task<bool> DispatchShortcutAsync(string shortcut, bool isAccessKey = false)
    {
        // accesskey는 브라우저가 Alt(+Shift)로 발동 -- 여기선 Alt 조합으로 처리
        if (isAccessKey && shortcut.Length == 1)
        {
            await DispatchKeyAsync(shortcut.ToUpperInvariant(), modifiers: 1); // Alt=1
            Console.WriteLine($"  [CDP-HOTKEY] accesskey '{shortcut}' -> Alt+{shortcut.ToUpperInvariant()}");
            return true;
        }

        // "Alt+S", "Ctrl+Shift+F5" 파싱
        var parts = shortcut.Split('+');
        var key = parts[^1].Trim();
        int mods = 0;
        foreach (var p in parts[..^1])
        {
            mods |= p.Trim().ToLowerInvariant() switch
            {
                "alt"   => 1,
                "ctrl" or "control" => 2,
                "meta" or "cmd"     => 4,
                "shift" => 8,
                _ => 0
            };
        }
        await DispatchKeyAsync(key, mods);
        Console.WriteLine($"  [CDP-HOTKEY] '{shortcut}' -> key={key} mods={mods}");
        return true;
    }

}
