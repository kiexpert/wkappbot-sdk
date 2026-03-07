using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot a11y <action> <grap>[#uia-scope] [options]
    // Unified pattern: find all -> select (--nth/--all) -> dispatch targets
    static int A11yCommand(string[] args)
    {
        // ═══ Delegate actions (may not require grap) ═══
        if (args.Length >= 1)
        {
            var maybeAction = args[0].ToLowerInvariant();
            if (maybeAction is "inspect" or "windows" or "screenshot" or "ocr")
            {
                var delegateArgs = args.Skip(1).ToArray();
                return maybeAction switch
                {
                    "inspect"    => InspectCommand(delegateArgs),
                    "windows"    => WindowsCommand(delegateArgs),
                    "screenshot" => CaptureCommand(delegateArgs),
                    "ocr"        => OcrCommand(delegateArgs),
                    _ => 1
                };
            }
        }

        if (args.Length < 2)
        {
            var ver = typeof(Program).Assembly.GetName().Version;
            var verStr = ver != null ? $"v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
            Console.WriteLine();
            Console.WriteLine($"  WKAppBot a11y {verStr}");
            Console.WriteLine($"  A MUD-style portal for AI to see and touch the real world.");
            Console.WriteLine($"  find = look, read = examine, invoke = open the door.");
            Console.WriteLine($"  24 actions · 3-tier fallback · focusless · zoom overlay");
            Console.WriteLine($"  UIA → Win32 → SendInput + CDP web fallback — just works.");
            Console.WriteLine();
            Console.WriteLine("  Usage: a11y <action> <grap>[#uia-scope] [options]");
            Console.WriteLine();
            Console.WriteLine("═══ Discovery Actions (4) ═════════════════════════════════");
            Console.WriteLine("  inspect     UIA element tree (delegates to inspect command)");
            Console.WriteLine("  windows     List visible windows (delegates to windows command)");
            Console.WriteLine("  screenshot  Capture window screenshot (delegates to capture)");
            Console.WriteLine("  ocr         OCR text extraction (delegates to ocr command)");
            Console.WriteLine();
            Console.WriteLine("═══ Window Actions (7) ════════════════════════════════════");
            Console.WriteLine("  close       Close window (UIA → WM_CLOSE → Process.Kill)");
            Console.WriteLine("  minimize    Minimize window");
            Console.WriteLine("  maximize    Maximize window");
            Console.WriteLine("  restore     Restore minimized/maximized window");
            Console.WriteLine("  focus       Set foreground focus");
            Console.WriteLine("  move        Move window (--x N --y N)");
            Console.WriteLine("  resize      Resize window (--w N --h N)");
            Console.WriteLine();
            Console.WriteLine("═══ Element Actions (13) — use #scope to target ════════");
            Console.WriteLine("  find        Win32 children + UIA tree dump (--depth N)");
            Console.WriteLine("  read        Read all accessible state (name, value, patterns)");
            Console.WriteLine("  highlight   Show zoom/magnifier overlay on element");
            Console.WriteLine("  invoke      Invoke (UIA Invoke → BM_CLICK → WM_LBUTTON → SendInput)");
            Console.WriteLine("  click       Physical click at element center");
            Console.WriteLine("  toggle      Toggle checkbox/switch on↔off");
            Console.WriteLine("  expand      Expand tree node / combo dropdown");
            Console.WriteLine("  collapse    Collapse tree node / combo dropdown");
            Console.WriteLine("  select      Select list item / tab");
            Console.WriteLine("  scroll      Scroll (--direction up|down|left|right --amount small|large)");
            Console.WriteLine("  type        Type text (--text \"...\") — focusless WM_CHAR");
            Console.WriteLine("  set-value   Set value directly (--text \"...\")");
            Console.WriteLine("  set-range   Set numeric range (--value N)");
            Console.WriteLine();
            Console.WriteLine("═══ Target Selection ══════════════════════════════════════");
            Console.WriteLine("  (default)   First match");
            Console.WriteLine("  --nth 3     3rd match only");
            Console.WriteLine("  --nth 2~4   Range: 2nd to 4th");
            Console.WriteLine("  --nth 3~    From 3rd to end");
            Console.WriteLine("  --nth ~3    First to 3rd");
            Console.WriteLine("  --all       All matches");
            Console.WriteLine();
            Console.WriteLine("═══ Options ═══════════════════════════════════════════════");
            Console.WriteLine("  --depth N   Tree depth for find (default: 3)");
            Console.WriteLine("  --force     close: escalate to Process.Kill");
            Console.WriteLine("  --force-close-ancestors  Include own process tree in targets");
            Console.WriteLine();
            Console.WriteLine("═══ Auto Pipeline (every action) ══════════════════════════");
            Console.WriteLine("  1. Window find        Pattern match with `;` OR support");
            Console.WriteLine("  2. Ancestor protect   Self process tree auto-excluded");
            Console.WriteLine("  3. Blocker dismiss    Popup detection + auto close (~5ms)");
            Console.WriteLine("  4. Minimize restore   Focusless SW_SHOWNOACTIVATE");
            Console.WriteLine("  5. Win32 child walk   grap `/` segments");
            Console.WriteLine("  6. UIA scope          grap `#` segments (container-first)");
            Console.WriteLine("  7. Tab activation     Auto-select unselected parent TabItem");
            Console.WriteLine("  8. Zoom overlay       Magnifier/highlight on target");
            Console.WriteLine("  9. Action execute     3-tier: UIA → Win32 → SendInput");
            Console.WriteLine(" 10. Result feedback    ✓ green OK / ✗ amber FAIL + fade");
            Console.WriteLine();
            Console.WriteLine("═══ Examples ══════════════════════════════════════════════");
            Console.WriteLine("  a11y close 메모장                        Close Notepad");
            Console.WriteLine("  a11y find 메모장 --depth 5               Explore element tree");
            Console.WriteLine("  a11y invoke \"메모장#파일\"               Click '파일' menu");
            Console.WriteLine("  a11y type \"영웅문#종목코드\" --text \"005930\"");
            Console.WriteLine();
            Console.WriteLine("═══ Synthesized Full-Path (v3.0) — Unified Web+Native ═══");
            Console.WriteLine("  a11y find  \"Chrome#ChatGPT\"             Tab portal → web tree");
            Console.WriteLine("  a11y invoke \"Chrome#Gemini#기본 메뉴\"    Tab → web button click");
            Console.WriteLine("  a11y click \"Chrome#button.submit\"       CSS → CDP auto-fallback!");
            Console.WriteLine("  a11y type  \"Chrome#input[name=q]\" --text \"hello\"  CDP type");
            Console.WriteLine("  a11y read  \"Claude#.editor\"             Electron + CDP read");
            Console.WriteLine();
            Console.WriteLine("  grap = Win32#a11y — unified native + web path");
            Console.WriteLine("    메모장                 plain text = UIA Name match");
            Console.WriteLine("    Chrome#ChatGPT#Send    UIA scope / tab portal");
            Console.WriteLine("    Chrome#button.submit   CSS selector → CDP auto-detect!");
            Console.WriteLine("    Chrome#[aria-label=X]  CSS attr → CDP");
            Console.WriteLine("    영웅문/0338#실시간계좌  / = Win32 child, # = UIA scope");
            Console.WriteLine("    */? wildcards · regex: · `;` OR · aid fallback");
            Console.WriteLine();
            Console.WriteLine("─── © 2026 WilKim · github.com/kiexpert/WKAppBot ──────────");
            return 1;
        }

        var action = args[0].ToLowerInvariant();
        var grap = args[1];
        bool all = args.Any(a => a == "--all");
        bool force = args.Any(a => a == "--force");
        bool forceCloseAncestors = args.Any(a => a == "--force-close-ancestors");

        // Parse action-specific params
        int? mx = null, my = null, mw = null, mh = null;
        string? text = null;
        double? rangeValue = null;
        string scrollDir = "down";
        string scrollAmount = "small";
        string? nthRaw = null;
        int findDepth = 3;
        for (int i = 2; i < args.Length; i++)
        {
            var a = args[i].ToLowerInvariant();
            if (a == "--x" && i + 1 < args.Length && int.TryParse(args[i + 1], out var xv)) { mx = xv; i++; }
            else if (a == "--y" && i + 1 < args.Length && int.TryParse(args[i + 1], out var yv)) { my = yv; i++; }
            else if (a == "--w" && i + 1 < args.Length && int.TryParse(args[i + 1], out var wv)) { mw = wv; i++; }
            else if (a == "--h" && i + 1 < args.Length && int.TryParse(args[i + 1], out var hv)) { mh = hv; i++; }
            else if (a == "--text" && i + 1 < args.Length) { text = args[++i]; }
            else if (a == "--value" && i + 1 < args.Length && double.TryParse(args[i + 1], out var rv)) { rangeValue = rv; i++; }
            else if (a == "--direction" && i + 1 < args.Length) { scrollDir = args[++i].ToLowerInvariant(); }
            else if (a == "--amount" && i + 1 < args.Length) { scrollAmount = args[++i].ToLowerInvariant(); }
            else if (a == "--nth" && i + 1 < args.Length) { nthRaw = args[++i]; }
            else if (a == "--depth" && i + 1 < args.Length && int.TryParse(args[i + 1], out var dv)) { findDepth = dv; i++; }
        }

        // Validate
        if (action == "move" && (mx == null || my == null))
            return Error("move requires --x N --y N");
        if (action == "resize" && (mw == null || mh == null))
            return Error("resize requires --w N --h N");
        if ((action == "type" || action == "set-value") && text == null)
            return Error($"{action} requires --text \"...\"");
        if (action == "set-range" && rangeValue == null)
            return Error("set-range requires --value N");

        var elementActions = new HashSet<string> {
            "invoke", "click", "toggle", "expand", "collapse", "select",
            "scroll", "type", "set-value", "set-range", "read",
            "highlight", "find"
        };
        bool isElementAction = elementActions.Contains(action);

        // ═══ STEP 1: Split grap ═══
        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grap);
        if (win32Segments.Length == 0)
            return Error("No window title in grap pattern");

        // ═══ STEP 2: Find all matching windows ═══
        var firstSegPatterns = win32Segments[0]
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var allWindows = new List<WindowInfo>();
        var seen = new HashSet<IntPtr>();
        foreach (var pat in firstSegPatterns)
            foreach (var w in WindowFinder.FindByTitle(pat))
                if (seen.Add(w.Handle))
                    allWindows.Add(w);

        if (action == "close" && !forceCloseAncestors)
        {
            var selfPids = GetSelfAndAncestorPids();
            allWindows.RemoveAll(w =>
            {
                NativeMethods.GetWindowThreadProcessId(w.Handle, out var pid);
                if (selfPids.Contains((int)pid))
                {
                    Console.WriteLine($"[A11Y] skip (ancestor pid={pid}): \"{w.Title}\" (hwnd={w.Handle:X8})");
                    return true;
                }
                return false;
            });
        }

        if (allWindows.Count == 0)
            return Error($"No window found: \"{win32Segments[0]}\"");

        // ═══ STEP 3: Select targets ═══
        List<WindowInfo> targets;
        if (all)
            targets = allWindows;
        else if (nthRaw != null)
        {
            var parsed = ParseNthTargets(allWindows, nthRaw);
            if (parsed == null)
                return Error($"--nth {nthRaw}: invalid range or out of bounds ({allWindows.Count} match(es))");
            targets = parsed;
        }
        else
            targets = new List<WindowInfo> { allWindows[0] };

        // ═══ STEP 4: Display matches ═══
        // search key = grap pattern matching target string
        static string SearchKey(WindowInfo wi)
        {
            var r = wi.Rect;
            string proc;
            try
            {
                NativeMethods.GetWindowThreadProcessId(wi.Handle, out uint pid);
                proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName;
            }
            catch { proc = "?"; }
            return $"[{wi.ClassName}] {wi.Title} ({proc} hwnd={wi.Handle:X8} {r.Right - r.Left}x{r.Bottom - r.Top})";
        }

        if (allWindows.Count > 1)
        {
            for (int idx = 0; idx < allWindows.Count; idx++)
            {
                var w = allWindows[idx];
                var marker = targets.Contains(w) ? ">" : " ";
                Console.WriteLine($"[A11Y] {marker} #{idx + 1} {SearchKey(w)}");
            }
        }
        else
        {
            Console.WriteLine($"[A11Y] matched: {SearchKey(targets[0])}");
        }

        // ═══ STEP 5: Execute on each target ═══
        int ok = 0, fail = 0;
        using var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);
        var readiness = CreateInputReadiness();

        foreach (var win in targets)
        {
            var hwnd = win.Handle;
            var title = win.Title;
            var tag = $"0x{hwnd.ToInt64():X} \"{title}\"";

            // Walk Win32 children (segments[1..])
            bool childError = false;
            for (int i = 1; i < win32Segments.Length; i++)
            {
                var child = WindowFinder.FindChildByPattern(hwnd, win32Segments[i]);
                if (child == null)
                {
                    Console.Error.WriteLine($"[A11Y] Win32 child not found: \"{win32Segments[i]}\" in {tag}");
                    childError = true;
                    break;
                }
                hwnd = child.Handle;
                tag = $"0x{hwnd.ToInt64():X} \"{WindowFinder.GetWindowText(hwnd)}\"";
            }
            if (childError) { fail++; continue; }

            // ── 입력위치확보 (EnsureInputReady) ──
            if (action != "close" && action != "minimize")
                EnsureWindowReady(hwnd, $"a11y-{action}", title, readiness);

            bool success;

            if (isElementAction)
            {
                // ── CDP Telepathy: if CSS pattern, try CDP first on ANY browser ──
                bool isCssPattern = !string.IsNullOrEmpty(uiaPath) && GrapHelper.LooksLikeCssSelector(uiaPath);

                if (isCssPattern)
                {
                    // Telepathy: try CDP on any process that has a debugging port
                    var cdpResult = TryWebViewCdpAction(hwnd, action, uiaPath!, text, rangeValue, scrollDir, scrollAmount, findDepth);
                    if (cdpResult != null)
                    {
                        success = cdpResult.Value;
                        if (success) ok++; else fail++;
                        continue;
                    }
                    // CDP not available → fall through to UIA (knock on the door)
                    Console.WriteLine($"[A11Y] No CDP — falling through to UIA");
                }

                AutomationElement root;
                try { root = automation.FromHandle(hwnd); }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[A11Y] UIA FromHandle failed for {tag}: {ex.Message}");
                    fail++;
                    continue;
                }

                if (!string.IsNullOrEmpty(uiaPath))
                {
                    var scoped = GrapHelper.FindUiaScope(root, uiaPath);
                    if (scoped == null)
                    {
                        // UIA failed → try CDP telepathy as fallback (any browser with CDP port)
                        if (!isCssPattern && !string.IsNullOrEmpty(uiaPath))
                        {
                            Console.WriteLine($"[A11Y] UIA scope failed, trying CDP telepathy with \"{uiaPath}\"");
                            var cdpResult = TryWebViewCdpAction(hwnd, action, uiaPath!, text, rangeValue, scrollDir, scrollAmount, findDepth);
                            if (cdpResult != null)
                            {
                                success = cdpResult.Value;
                                if (success) ok++; else fail++;
                                continue;
                            }
                        }
                        Console.Error.WriteLine($"[A11Y] UIA scope not found: \"{uiaPath}\" in {tag}");
                        fail++;
                        continue;
                    }
                    root = scoped;
                }

                var elName = root.Properties.Name.ValueOrDefault ?? "";
                var elType = "?";
                try { elType = root.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                var elAid = root.Properties.AutomationId.ValueOrDefault ?? "";
                Console.WriteLine($"[A11Y] element: {elType} \"{elName}\" (aid=\"{elAid}\") in {tag}");

                // Tab activation: if target is inside an unselected tab, activate it first
                EnsureTabActive(root);

                // Zoom: show magnifier/highlight on target element
                var elRect = GetBoundingRect(root);
                var elHwnd = GetElementHwnd(root);
                ClickZoomHelper? zoom = null;
                if (action != "highlight") // highlight manages its own zoom
                {
                    if (elHwnd != IntPtr.Zero)
                        zoom = ClickZoomHelper.Begin(elHwnd, hwnd, $"a11y-{action}", $"{elType} \"{elName}\"");
                    else if (elRect != null)
                        zoom = ClickZoomHelper.BeginFromRect(elRect.Value, hwnd, $"a11y-{action}", $"{elType} \"{elName}\"");
                }

                success = action switch
                {
                    "highlight" => A11yHighlight(root, hwnd),
                    "find" => A11yFind(root, hwnd, findDepth),
                    "read" => A11yRead(root),
                    "invoke" => A11yInvoke(root, hwnd),
                    "click" => A11yClick(root, hwnd),
                    "toggle" => A11yToggle(root, hwnd),
                    "expand" => A11yExpand(root),
                    "collapse" => A11yCollapse(root),
                    "select" => A11ySelectItem(root),
                    "scroll" => A11yScrollAction(root, hwnd, scrollDir, scrollAmount),
                    "type" => A11yType(root, hwnd, text!),
                    "set-value" => A11ySetValue(root, hwnd, text!),
                    "set-range" => A11ySetRange(root, rangeValue!.Value),
                    _ => A11yNotYet(action)
                };

                // Zoom result feedback + fade
                if (zoom != null)
                {
                    if (success) zoom.ShowPass($"{action} OK");
                    else zoom.ShowFail($"{action} FAIL");
                    Thread.Sleep(800);
                    zoom.Dispose();
                }
            }
            else
            {
                // Zoom: show magnifier/highlight on target window
                ClickZoomHelper? zoom = null;
                zoom = ClickZoomHelper.Begin(hwnd, hwnd, $"a11y-{action}", $"\"{title}\"");

                success = action switch
                {
                    "close" => A11yClose(automation, hwnd, tag, force, readiness),
                    "minimize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Minimized),
                    "maximize" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Maximized),
                    "restore" => A11ySetVisualState(automation, hwnd, tag, WindowVisualState.Normal),
                    "move" => A11yMove(automation, hwnd, tag, mx!.Value, my!.Value),
                    "resize" => A11yResize(automation, hwnd, tag, mw!.Value, mh!.Value),
                    "focus" => A11yFocus(hwnd, tag),
                    _ => A11yNotYet(action)
                };

                // Zoom result feedback + fade
                if (zoom != null)
                {
                    if (success) zoom.ShowPass($"{action} OK");
                    else zoom.ShowFail($"{action} FAIL");
                    Thread.Sleep(800);
                    zoom.Dispose();
                }
            }

            if (success) ok++; else fail++;
        }

        Console.WriteLine($"[A11Y] Done: {ok} ok, {fail} fail (total {targets.Count})");
        return fail > 0 ? 1 : 0;
    }

    static bool A11yNotYet(string action)
    {
        Console.Error.WriteLine($"[A11Y] ERROR: '{action}' is not a valid action");
        Console.Error.WriteLine("[A11Y] Window: close, minimize, maximize, restore, move, resize, focus");
        Console.Error.WriteLine("[A11Y] Element: read, find, highlight, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range");
        return false;
    }

    // ═══ Window-Level Actions (see A11yElementActions.cs for element-level) ═══

    static bool A11yFocus(IntPtr hwnd, string tag)
    {
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9);
        NativeMethods.SetForegroundWindow(hwnd);
        NativeMethods.BringWindowToTop(hwnd);
        Console.WriteLine($"[A11Y] focus {tag} — SetForegroundWindow");
        return true;
    }

    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag, bool force, InputReadiness readiness)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.Close();
                Console.WriteLine($"[A11Y] close {tag} — UIA WindowPattern");
                return true;
            }
            Console.WriteLine($"[A11Y] close {tag} — UIA not supported, trying Win32");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] close {tag} — UIA failed ({ex.Message}), trying Win32");
        }

        try
        {
            NativeMethods.SendMessageTimeoutW(hwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
            Thread.Sleep(1000);
            if (!NativeMethods.IsWindow(hwnd))
            {
                Console.WriteLine($"[A11Y] close {tag} — Win32 WM_CLOSE");
                return true;
            }
            var blocker = readiness.DetectBlocker(hwnd);
            if (blocker != null)
            {
                Console.WriteLine($"[A11Y] close {tag} — blocker: {blocker.ClassName} \"{blocker.Title}\" — dismissing");
                readiness.BlockerHandler?.TryHandle(hwnd, blocker);
                Thread.Sleep(500);
                if (!NativeMethods.IsWindow(hwnd))
                {
                    Console.WriteLine($"[A11Y] close {tag} — closed after dismiss");
                    return true;
                }
            }
            if (!force)
            {
                Console.WriteLine($"[A11Y] close {tag} — still alive (use --force to kill)");
                return false;
            }
        }
        catch (Exception ex)
        {
            if (!force) { Console.Error.WriteLine($"[A11Y] close {tag} — WM_CLOSE failed: {ex.Message}"); return false; }
        }

        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            if (pid != 0)
            {
                System.Diagnostics.Process.GetProcessById((int)pid).Kill();
                Console.WriteLine($"[A11Y] close {tag} — Process.Kill (pid={pid})");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} — all tiers FAIL: {ex.Message}");
        }
        return false;
    }

    static bool A11ySetVisualState(UIA3Automation automation, IntPtr hwnd, string tag, WindowVisualState state)
    {
        var name = state.ToString().ToLower();
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.SetWindowVisualState(state);
                Console.WriteLine($"[A11Y] {name} {tag} — UIA WindowPattern");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] {name} {tag} — UIA failed ({ex.Message}), trying Win32");
        }

        int cmd = state switch
        {
            WindowVisualState.Minimized => 6,
            WindowVisualState.Maximized => 3,
            _ => 9
        };
        try
        {
            NativeMethods.ShowWindow(hwnd, cmd);
            Console.WriteLine($"[A11Y] {name} {tag} — Win32 ShowWindow({cmd})");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] {name} {tag} — FAIL: {ex.Message}");
            return false;
        }
    }

    static bool A11yMove(UIA3Automation automation, IntPtr hwnd, string tag, int x, int y)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanMove)
            {
                el.Patterns.Transform.Pattern.Move(x, y);
                Console.WriteLine($"[A11Y] move {tag} -> ({x},{y}) — UIA Transform");
                return true;
            }
        }
        catch { }
        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 0x0015);
            Console.WriteLine($"[A11Y] move {tag} -> ({x},{y}) — Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] move {tag} — FAIL: {ex.Message}");
            return false;
        }
    }

    static bool A11yResize(UIA3Automation automation, IntPtr hwnd, string tag, int w, int h)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Transform.IsSupported && el.Patterns.Transform.Pattern.CanResize)
            {
                el.Patterns.Transform.Pattern.Resize(w, h);
                Console.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) — UIA Transform");
                return true;
            }
        }
        catch { }
        try
        {
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 0x0016);
            Console.WriteLine($"[A11Y] resize {tag} -> ({w}x{h}) — Win32 SetWindowPos");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] resize {tag} — FAIL: {ex.Message}");
            return false;
        }
    }

    // ═══ Target Selection Helpers ═══

    static List<WindowInfo>? ParseNthTargets(List<WindowInfo> windows, string nthRaw)
    {
        if (nthRaw.StartsWith('~'))
        {
            if (!int.TryParse(nthRaw[1..], out var to) || to < 1) return null;
            return windows.Take(Math.Min(to, windows.Count)).ToList();
        }
        if (nthRaw.EndsWith('~'))
        {
            if (!int.TryParse(nthRaw[..^1], out var from) || from < 1 || from > windows.Count) return null;
            return windows.Skip(from - 1).ToList();
        }
        if (nthRaw.Contains('~'))
        {
            var parts = nthRaw.Split('~');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var from) || !int.TryParse(parts[1], out var to)
                || from < 1 || to < from || from > windows.Count) return null;
            return windows.Skip(from - 1).Take(Math.Min(to, windows.Count) - from + 1).ToList();
        }
        if (int.TryParse(nthRaw, out var idx) && idx >= 1 && idx <= windows.Count)
            return new List<WindowInfo> { windows[idx - 1] };
        return null;
    }

    static HashSet<int> GetSelfAndAncestorPids()
    {
        var pids = new HashSet<int>();
        try
        {
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            pids.Add(proc.Id);
            int pid = proc.Id;
            for (int i = 0; i < 10 && pid > 0; i++)
            {
                int ppid = GetParentPid(pid);
                if (ppid <= 0 || !pids.Add(ppid)) break;
                pid = ppid;
            }
        }
        catch { }
        return pids;
    }

    // ═══ WebView CDP Fallback ═══

    /// <summary>
    /// Try to perform an a11y action on a web view element via CDP.
    /// Returns true/false on success/failure, or null if CDP is unavailable.
    /// </summary>
    static bool? TryWebViewCdpAction(IntPtr hwnd, string action, string cssSelector,
        string? text, double? rangeValue, string scrollDir, string scrollAmount, int findDepth)
    {
        try
        {
            // Detect CDP port from the owning process
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var port = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)pid);
            if (port <= 0)
            {
                Console.WriteLine($"[A11Y] No CDP port found for PID {pid}");
                return null; // CDP not available
            }

            Console.WriteLine($"[A11Y] Telepathy! CDP port={port}, selector=\"{cssSelector}\"");

            using var cdp = new WKAppBot.WebBot.CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();

            // Find the right tab — match by window title
            var windowTitle = WKAppBot.Win32.Window.WindowFinder.GetWindowText(hwnd);
            if (!string.IsNullOrEmpty(windowTitle))
            {
                var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();
                foreach (var tab in tabs)
                {
                    if (tab.Title.Contains(windowTitle, StringComparison.OrdinalIgnoreCase) ||
                        windowTitle.Contains(tab.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (tab.Id != cdp.TargetId)
                            cdp.SwitchToTargetAsync(tab.Id, port).GetAwaiter().GetResult();
                        break;
                    }
                }
            }

            bool result = action switch
            {
                "click" or "invoke" => CdpClick(cdp, cssSelector),
                "type" => CdpType(cdp, cssSelector, text ?? ""),
                "set-value" => CdpSetValue(cdp, cssSelector, text ?? ""),
                "read" or "find" => CdpReadElement(cdp, cssSelector),
                "toggle" => CdpToggle(cdp, cssSelector),
                "select" => CdpSelect(cdp, cssSelector, text ?? ""),
                _ => CdpEvalAction(cdp, cssSelector, action)
            };

            Console.WriteLine($"[A11Y] CDP {action} → {(result ? "OK" : "FAIL")}");
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

    static bool CdpType(WKAppBot.WebBot.CdpClient cdp, string selector, string text)
    {
        cdp.TypeAsync(selector, text).GetAwaiter().GetResult();
        return true;
    }

    static bool CdpSetValue(WKAppBot.WebBot.CdpClient cdp, string selector, string text)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        var textEscaped = text.Replace("\\", "\\\\").Replace("'", "\\'");
        cdp.EvalAsync($"(() => {{ var el = document.querySelector('{escaped}'); if(!el) return 'NOT_FOUND'; el.value = '{textEscaped}'; el.dispatchEvent(new Event('input', {{bubbles:true}})); return 'OK'; }})()").GetAwaiter().GetResult();
        return true;
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
            Console.Error.WriteLine($"[A11Y] CDP element not found: {selector}");
            return false;
        }
        Console.WriteLine($"[A11Y] CDP read: {result}");
        return true;
    }

    static bool CdpToggle(WKAppBot.WebBot.CdpClient cdp, string selector)
    {
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        cdp.EvalAsync($"(() => {{ var el = document.querySelector('{escaped}'); if(!el) return; el.click(); }})()").GetAwaiter().GetResult();
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
        var escaped = selector.Replace("\\", "\\\\").Replace("'", "\\'");
        cdp.EvalAsync($"document.querySelector('{escaped}')?.click()").GetAwaiter().GetResult();
        return true;
    }
}
