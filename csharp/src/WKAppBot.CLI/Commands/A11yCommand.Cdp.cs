using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ═══ WebView CDP Fallback ═══

    /// <summary>
    /// Try to perform an a11y action on a web view element via CDP.
    /// Returns true/false on success/failure, or null if CDP is unavailable.
    /// </summary>
    static bool? TryWebViewCdpAction(IntPtr hwnd, string action, string cssSelector,
        string? text, double? rangeValue, string scrollDir, string scrollAmount, int findDepth, bool speak = false, bool hotkey = false, string? evalJs = null,
        int timeoutMs = 10000, int intervalMs = 500)
    {
        try
        {
            // Detect CDP port from the owning process — browsers only.
            // Non-browser Electron apps (VS Code…) also expose CDP but must NOT be treated as browsers.
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            if (!WKAppBot.WebBot.CdpClient.IsBrowserProcess((int)pid))
                return null; // Not a browser — skip CDP entirely
            var port = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)pid);
            if (port <= 0)
            {
                Console.WriteLine($"[A11Y] No CDP port found for PID {pid}");
                return null; // CDP not available
            }

            Console.WriteLine($"[A11Y] Telepathy! CDP port={port}, selector=\"{cssSelector}\"");

            using var cdp = new WKAppBot.WebBot.CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();

            // Find the right tab — match by URL hint first (scope), then window title
            var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();
            bool tabFound = false;

            // Read-only actions don't need focus — skip browser minimize during tab switch.
            bool readOnly = action is "read" or "find" or "inspect" or "highlight" or "ocr" or "screenshot";

            // Priority 1: if scope looks like URL (contains localhost, http, .com, :port) → match by URL
            bool scopeIsUrl = cssSelector.Contains("localhost", StringComparison.OrdinalIgnoreCase)
                || cssSelector.Contains("http", StringComparison.OrdinalIgnoreCase)
                || cssSelector.Contains(":", StringComparison.Ordinal) && char.IsDigit(cssSelector[^1]);
            if (scopeIsUrl)
            {
                foreach (var tab in tabs)
                {
                    if (tab.Url.Contains(cssSelector, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[A11Y] Tab matched by URL hint: \"{cssSelector}\" → {tab.Url[..Math.Min(60, tab.Url.Length)]}");
                        if (tab.Id != cdp.TargetId)
                            cdp.SwitchToTargetAsync(tab.Id, port, skipMinimize: readOnly).GetAwaiter().GetResult();
                        tabFound = true;
                        break;
                    }
                }
            }

            // Priority 2: fallback to window title match
            if (!tabFound)
            {
                var windowTitle = WindowFinder.GetWindowText(hwnd);
                if (!string.IsNullOrEmpty(windowTitle))
                {
                    foreach (var tab in tabs)
                    {
                        if (tab.Title.Contains(windowTitle, StringComparison.OrdinalIgnoreCase) ||
                            windowTitle.Contains(tab.Title, StringComparison.OrdinalIgnoreCase))
                        {
                            if (tab.Id != cdp.TargetId)
                                cdp.SwitchToTargetAsync(tab.Id, port, skipMinimize: readOnly).GetAwaiter().GetResult();
                            break;
                        }
                    }
                }
            }

            // ── Inject shared JS helpers (defA11yRead, defA11yClick, etc.) ──
            if (!string.IsNullOrEmpty(evalJs))
                InjectA11yJsHelpers(cdp);

            // ── Pre-action JS: --eval-js runs before AAR (can remove disabled, scroll, etc.) ──
            // For read/find: --eval-js IS the action (CdpEvalAsRead), so skip pre-hook to avoid double execution
            if (!string.IsNullOrEmpty(evalJs) && action is not ("read" or "find"))
            {
                try
                {
                    var jsResult = cdp.EvalAsync(evalJs).GetAwaiter().GetResult();
                    Console.WriteLine($"[A11Y] --eval-js result: {jsResult}");
                }
                catch (Exception jsEx)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Error.WriteLine($"[A11Y] --eval-js FAILED: {jsEx.Message}");
                    Console.Error.WriteLine($"[A11Y] → fallback: proceeding with '{action}' on \"{cssSelector}\" without pre-hook JS");
                    Console.ResetColor();
                }
            }

            // ── AAR: Action-Aware Readiness for CDP ──
            if (action is not ("read" or "find" or "inspect" or "highlight" or "screenshot" or "ocr" or "eval"))
            {
                var cdpAar = new WKAppBot.WebBot.CdpActionReadiness();
                var (offX, offY) = WKAppBot.WebBot.CdpActionTarget.GetWindowContentOffset(cdp);
                var cdpTarget = WKAppBot.WebBot.CdpActionTarget.Query(cdp, cssSelector, offX, offY);
                if (cdpTarget != null)
                {
                    var cdpCtx = new WKAppBot.Abstractions.ReadinessContext { Hwnd = hwnd };
                    var aarResult = cdpAar.Ensure(action, cdpTarget, cdpCtx);
                    if (aarResult == null)
                    {
                        Console.Error.WriteLine($"[A11Y] AAR blocked CDP {action} on \"{cssSelector}\"");
                        return false;
                    }
                }
                // cdpTarget == null → element not found; let the action itself handle NOT_FOUND
            }

            bool result = action switch
            {
                "click" or "invoke" => CdpClick(cdp, cssSelector),
                "double-click" or "double_click" => CdpDoubleClick(cdp, cssSelector),
                "right-click" or "right_click" => CdpRightClick(cdp, cssSelector),
                "type" when hotkey  => CdpHotkey(cdp, text ?? ""),
                "type" => CdpType(cdp, cssSelector, text ?? ""),
                "set-value" => CdpSetValue(cdp, cssSelector, text ?? ""),
                "read" or "find" when !string.IsNullOrEmpty(evalJs) => CdpEvalAsRead(cdp, evalJs, timeoutMs, intervalMs),
                "read" or "find" => CdpReadElement(cdp, cssSelector),
                "toggle" => CdpToggle(cdp, cssSelector),
                "select" => CdpSelect(cdp, cssSelector, text ?? ""),
                "scroll" => CdpScroll(cdp, cssSelector, scrollDir, scrollAmount),
                "expand" => CdpExpandCollapse(cdp, cssSelector, expand: true),
                "collapse" => CdpExpandCollapse(cdp, cssSelector, expand: false),
                _ => CdpEvalAction(cdp, cssSelector, action)
            };

            Console.WriteLine($"[A11Y] CDP {action} → {(result ? "OK" : "FAIL")}");

            // --speak: TTS for CDP results too
            if (speak && result && (action == "read" || action == "find"))
            {
                // Re-fetch text content for speak
                try
                {
                    var esc2 = cssSelector.Replace("\\", "\\\\").Replace("'", "\\'");
                    var txt = cdp.EvalAsync($"document.querySelector('{esc2}')?.textContent?.substring(0,200)||''").GetAwaiter().GetResult();
                    if (!string.IsNullOrWhiteSpace(txt) && txt != "''")
                    {
                        AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "wkappbot",
                            Arguments = $"speak \"{txt.Replace("\"", "'")}\" --bg",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }, Environment.CurrentDirectory, "A11Y-CDP");
                    }
                }
                catch { /* best effort */ }
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] CDP fallback error: {ex.Message}");
            return null; // Fall through to UIA
        }
    }

    static bool CdpClick(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        cdp.ClickAsync(selector).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpDoubleClick(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        cdp.DoubleClickAsync(selector).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpRightClick(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        cdp.RightClickAsync(selector).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpScroll(WKAppBot.WebBot.CdpClient cdp, string selector, string direction, string amount)
    {
        cdp.ScrollAsync(selector, direction, amount).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpExpandCollapse(WKAppBot.WebBot.CdpClient cdp, string selector, bool expand)
    {
        cdp.ExpandCollapseAsync(selector, expand).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpType(WKAppBot.WebBot.CdpClient cdp, string selector, string text)
    {
        // Auto-detect CJK text → use Input.insertText (bypasses IME composition issues)
        if (text.Any(c => c >= 0x1100 && c <= 0xD7AF   // Korean (Hangul)
                       || c >= 0x3000 && c <= 0x9FFF    // CJK Unified + Japanese Kana
                       || c >= 0xF900 && c <= 0xFAFF))  // CJK Compatibility
        {
            Console.WriteLine("[A11Y] CJK text detected → using CDP Input.insertText");
            cdp.TypeInsertTextAsync(selector, text).GetAwaiter().GetResult();
        }
        else
        {
            cdp.TypeAsync(selector, text).GetAwaiter().GetResult();
        }
        return true;
    }

    /// <summary>
    /// --hotkey 모드 CDP 발동:
    /// 1. text에 '/' 포함 → aria-keyshortcuts / accesskey 직접 디스패치 (예: "Alt+S", "s")
    /// 2. 그 외 → DOM 스캔(accesskey + aria-keyshortcuts)에서 grap 패턴으로 레이블 매칭 → 커버리지 최고 선택
    /// </summary>
    static bool CdpHotkey(WKAppBot.WebBot.CdpClient cdp, string text)
    {
        // ① 명시적 단축키 문자열 (예: "Ctrl+S", "Alt+F4", "s")
        if (text.Contains('+') || (text.Length == 1 && char.IsLetterOrDigit(text[0])))
        {
            bool isAccessKey = text.Length == 1;
            return cdp.DispatchShortcutAsync(text, isAccessKey).GetAwaiter().GetResult();
        }

        // ② DOM 핫키 맵 스캔 → grap 패턴 레이블 매칭
        var hotkeyMap = cdp.GetHotkeyMapAsync().GetAwaiter().GetResult();
        if (hotkeyMap.Count == 0)
        {
            Console.Error.WriteLine("[A11Y] type --hotkey CDP — no accesskey/aria-keyshortcuts found in DOM");
            return false;
        }

        var matcher = PatternMatcher.Create(text);
        var matches = hotkeyMap
            .Where(e => !string.IsNullOrEmpty(e.Label) && matcher.IsMatch(e.Label))
            .OrderBy(e => e.Label.Length) // 커버리지 높은 것 우선
            .ToList();

        if (matches.Count == 0)
        {
            Console.Error.WriteLine($"[A11Y] type --hotkey CDP — no label match for '{text}'");
            return false;
        }

        var best = matches[0];
        if (matches.Count > 1)
            Console.WriteLine($"[A11Y] type --hotkey CDP — {matches.Count} matches, best: '{best.Label}'");

        // accesskey 우선, 없으면 aria-keyshortcuts 첫 번째
        if (!string.IsNullOrEmpty(best.Accesskey))
            return cdp.DispatchShortcutAsync(best.Accesskey, isAccessKey: true).GetAwaiter().GetResult();

        if (!string.IsNullOrEmpty(best.Keyshortcuts))
        {
            var first = best.Keyshortcuts.Split(' ')[0]; // 공백 구분 다중 단축키 중 첫 것
            return cdp.DispatchShortcutAsync(first).GetAwaiter().GetResult();
        }

        Console.Error.WriteLine($"[A11Y] type --hotkey CDP — '{best.Label}' has no dispatchable shortcut");
        return false;
    }

    static bool CdpSetValue(WKAppBot.WebBot.CdpClient cdp, string selector, string text)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var textEscaped = text.Replace("\\", "\\\\").Replace("'", "\\'");
        cdp.EvalAsync($"(() => {{ var el = document.querySelector('{escaped}'); if(!el) return 'NOT_FOUND'; el.value = '{textEscaped}'; el.dispatchEvent(new Event('input', {{bubbles:true}})); return 'OK'; }})()").GetAwaiter().GetResult();
        return true;
    }

    /// <summary>List all CDP tabs for a browser target (when no #scope specified).</summary>
    static bool CdpListTabs(int port, string tag)
    {
        try
        {
            using var cdp = new WKAppBot.WebBot.CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();
            var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();

            Console.WriteLine($"[A11Y] {tag} — {tabs.Count} tab(s):");
            for (int i = 0; i < tabs.Count; i++)
            {
                var t = tabs[i];
                var active = t.Id == cdp.TargetId ? " ★" : "";
                Console.WriteLine($"  [{i + 1}] {t.Title[..Math.Min(t.Title.Length, 60)]}{active}");
                Console.WriteLine($"      {t.Url[..Math.Min(t.Url.Length, 80)]}");
            }
            Console.WriteLine();
            Console.WriteLine("[A11Y] Use #tab-hint to target a specific tab:");
            Console.WriteLine($"  a11y read \"{tag}#chatgpt.com\" --eval-js \"document.title\"");
            return tabs.Count > 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] CDP tab list error: {ex.Message}");
            return false;
        }
    }

    /// <summary>Inject shared a11y JS helpers into the page (idempotent).</summary>
    static void InjectA11yJsHelpers(WKAppBot.WebBot.CdpClient cdp)
    {
        try
        {
            cdp.EvalAsync("""
                if (!window.__a11y) {
                    window.__a11y = {
                        _editorSel: ['[contenteditable="true"]','textarea:not([readonly])','input[type="text"]:not([readonly])','[role="textbox"]','.ql-editor'],
                        _btnSel: ['button[type="submit"]','button[aria-label*="Send"]','button[aria-label*="send"]','button[data-testid*="send"]','button.send-button','form button:last-of-type','button[aria-label*="확인"]']
                    };
                    window.defA11yRead = () => JSON.stringify({title:document.title,url:location.href,ready:document.readyState,hidden:document.hidden,text:(document.body?.innerText||'').substring(0,500)});
                    window.defA11yClick = () => { for(const s of __a11y._btnSel){const e=document.querySelector(s);if(e&&!e.disabled&&e.offsetParent!==null){e.click();return 'clicked:'+s}} return 'NOT_FOUND' };
                    window.defA11yFocus = () => { for(const s of __a11y._editorSel){const e=document.querySelector(s);if(e&&e.offsetParent!==null){e.focus();return 'focused:'+s}} return 'NOT_FOUND' };
                    window.defA11yEditor = () => { for(const s of __a11y._editorSel){const e=document.querySelector(s);if(e&&e.offsetParent!==null)return e.textContent?.trim()||''} return '' };
                    window.defA11yHidden = () => document.hidden;
                    window.defA11yReady = () => document.readyState;
                }
                """).GetAwaiter().GetResult();
        }
        catch { /* best effort — helpers unavailable won't break eval-js */ }
    }

    /// <summary>
    /// read + --eval-js: JS expression result becomes the read output.
    /// Waits for readyState=complete, then polls until result stabilizes (streaming-safe).
    /// </summary>
    static bool CdpEvalAsRead(WKAppBot.WebBot.CdpClient cdp, string jsExpression, int timeoutMs = 10000, int intervalMs = 500)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Phase 1: Wait for page ready (readyState = complete)
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var state = cdp.EvalAsync("document.readyState").GetAwaiter().GetResult();
            if (state is "complete" or "interactive") break;
            Console.WriteLine($"[A11Y] read --eval-js: waiting for page load (readyState={state})...");
            Thread.Sleep(Math.Min(intervalMs, 300));
        }

        // Phase 2: Eval + stabilize (detect streaming completion)
        string? lastResult = null;
        int stableCount = 0;
        const int stableThreshold = 2; // N consecutive identical results = stable

        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            string? result;
            try { result = cdp.EvalAsync(jsExpression).GetAwaiter().GetResult(); }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[A11Y] read --eval-js error: {ex.Message}");
                Thread.Sleep(intervalMs);
                continue;
            }

            // ── null = streaming done signal: return previous result ──
            if (result == null && lastResult != null)
            {
                Console.WriteLine($"[A11Y] read --eval-js: null signal → streaming done");
                Console.WriteLine($"[A11Y] read --eval-js: {lastResult}");
                return true;
            }

            if (result == lastResult)
            {
                stableCount++;
                if (stableCount >= stableThreshold)
                {
                    // Result stabilized — done
                    Console.WriteLine($"[A11Y] read --eval-js: {result}");
                    return result != null;
                }
            }
            else
            {
                // First eval or result changed (streaming in progress)
                if (lastResult == null)
                {
                    // First eval — if not waiting for stability, return immediately
                    // (when interval is default 500ms, just return first result for speed)
                    if (intervalMs >= 500 && timeoutMs <= 10000)
                    {
                        Console.WriteLine($"[A11Y] read --eval-js: {result}");
                        return result != null;
                    }
                }
                else
                {
                    Console.WriteLine($"[A11Y] read --eval-js: streaming... ({result?.Length ?? 0} chars)");
                }
                lastResult = result;
                stableCount = 1;
            }
            Thread.Sleep(intervalMs);
        }

        // Timeout — return last result
        if (lastResult != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[A11Y] read --eval-js: timeout {timeoutMs}ms, returning last result");
            Console.ResetColor();
        }
        Console.WriteLine($"[A11Y] read --eval-js: {lastResult}");
        return lastResult != null;
    }

    static bool CdpReadElement(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var result = cdp.EvalAsync(
            $"(() => {{ var el = document.querySelector('{escaped}'); if(!el) return 'NOT_FOUND'; " +
            "return JSON.stringify({ tag: el.tagName, id: el.id, class: el.className, " +
            "text: (el.textContent||'').substring(0,200), value: el.value||'', " +
            "type: el.type||'', href: el.href||'', aria: el.getAttribute('aria-label')||'' }); }})()").GetAwaiter().GetResult();

        if (result == "NOT_FOUND")
        {
            // Fallback: selector is domain hint, not CSS — read page summary instead
            Console.WriteLine($"[A11Y] CDP element not found: {selector} — falling back to page read");
            var page = cdp.ReadPageAsync().GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(page))
            {
                Console.WriteLine($"[A11Y] CDP page: {page}");
                return true;
            }
            return false;
        }
        Console.WriteLine($"[A11Y] CDP read: {result}");
        return true;
    }

    static bool CdpToggle(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        cdp.JsClickAsync(selector).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpSelect(WKAppBot.WebBot.CdpClient cdp, string selector, string value)
    {
        cdp.SelectAsync(selector, value).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpEvalAction(WKAppBot.WebBot.CdpClient cdp, string selector, string action)
    {
        Console.Error.WriteLine($"[A11Y] CDP action '{action}' not directly supported, trying JS click");
        cdp.JsClickAsync(selector).GetAwaiter().GetResult();
        return true;
    }

    // ── file-read / file-write: encoding-aware file I/O for CP949 Korean sources ──
    static int FileReadWrite(string action, string filePath, string? text, string? encodingArg, string[] args)
    {
        // Resolve encoding (default: UTF-8)
        System.Text.Encoding enc;
        if (encodingArg == null || encodingArg.Equals("utf-8", StringComparison.OrdinalIgnoreCase) || encodingArg == "65001")
        {
            enc = System.Text.Encoding.UTF8;
        }
        else if (encodingArg.Equals("utf-16", StringComparison.OrdinalIgnoreCase))
        {
            enc = System.Text.Encoding.Unicode;
        }
        else if (int.TryParse(encodingArg, out var cp))
        {
            try { enc = System.Text.Encoding.GetEncoding(cp); }
            catch { return Error($"Unknown encoding: {encodingArg}"); }
        }
        else
        {
            try { enc = System.Text.Encoding.GetEncoding(encodingArg); }
            catch { return Error($"Unknown encoding: {encodingArg}"); }
        }

        // ── file-read ──────────────────────────────────────────────────────────
        if (action == "file-read")
        {
            if (!File.Exists(filePath))
                return Error($"File not found: {filePath}");
            var bytes = File.ReadAllBytes(filePath);
            var unicode = enc.GetString(bytes);
            Console.WriteLine($"[FILE-READ] {filePath} ({enc.WebName}, {bytes.Length} bytes → {unicode.Length} chars)");
            Console.WriteLine(unicode);
            return 0;
        }

        // ── file-write ─────────────────────────────────────────────────────────
        if (text == null && args.Length >= 3 && !args[2].StartsWith("--"))
            text = args[2];
        if (text == null)
            return Error("file-write requires --text \"content\" or --text \"@source.txt\"");

        var writeArgs = new List<string> { "write", filePath, "--text", text };
        if (encodingArg != null) { writeArgs.Add("--encoding"); writeArgs.Add(encodingArg); }
        if (args.Any(a => a.Equals("--append", StringComparison.OrdinalIgnoreCase))) writeArgs.Add("--append");
        if (args.Any(a => a.Equals("--stdin", StringComparison.OrdinalIgnoreCase))) writeArgs.Add("--stdin");
        if (args.Any(a => a.Equals("--i-really-want-no-backup", StringComparison.OrdinalIgnoreCase))) writeArgs.Add("--i-really-want-no-backup");
        if (args.Any(a => a.Equals("--dry-run", StringComparison.OrdinalIgnoreCase))) writeArgs.Add("--dry-run");
        return FileCommand(writeArgs.ToArray());
    }

    /// <summary>Lookup hack cache in experience DB by pixel hash. Returns description or null.</summary>
    static string? LookupHackCache(IntPtr hwnd, string pixHash)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var proc = System.Diagnostics.Process.GetProcessById((int)pid);
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
            var expDir = Path.Combine(DataDir, "experience", proc.ProcessName, sb.ToString());
            if (!Directory.Exists(expDir)) return null;
            // Search all files matching *'{hash}=*.png
            var pattern = $"*'{pixHash}=*.png";
            var files = Directory.GetFiles(expDir, pattern, SearchOption.AllDirectories);
            if (files.Length == 0) return null;
            var name = Path.GetFileNameWithoutExtension(files[0]);
            var eqIdx = name.IndexOf('=');
            return eqIdx >= 0 ? name[(eqIdx + 1)..] : null;
        }
        catch { return null; }
    }
}
