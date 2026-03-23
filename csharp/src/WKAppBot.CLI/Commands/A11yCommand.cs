using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Abstractions;
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
        // ═══ ADB early dispatch: adb:// prefix → Android pipeline ═══
        if (args.Length >= 2 && Android.AdbGrapRouter.IsAdbGrap(args[1]))
            return AdbA11yDispatch(args);
        // Also check if grap is first arg (discovery actions)
        if (args.Length >= 1 && Android.AdbGrapRouter.IsAdbGrap(args[0]))
            return AdbA11yDispatch(new[] { "inspect" }.Concat(args).ToArray());

        // ═══ Delegate actions (may not require grap) ═══
        if (args.Length >= 1)
        {
            var maybeAction = args[0].ToLowerInvariant();
            if (maybeAction is "inspect" or "windows" or "screenshot" or "ocr" or "hack")
            {
                var delegateArgs = args.Skip(1).ToArray();
                // ADB check: if first delegate arg is adb:// → route to Android pipeline
                if (delegateArgs.Length > 0 && Android.AdbGrapRouter.IsAdbGrap(delegateArgs[0]))
                    return AdbA11yDispatch(args);
                return maybeAction switch
                {
                    "inspect"    => InspectCommand(delegateArgs),
                    "windows"    => WindowsCommand(delegateArgs),
                    "screenshot" => CaptureCommand(delegateArgs),
                    "ocr"        => OcrCommand(delegateArgs),
                    "hack"       => A11yHackCommand(delegateArgs),
                    _ => 1
                };
            }
            // ═══ Ask AI agents (삼두협의체) ═══
            if (maybeAction is "ask-gpt" or "ask-gemini" or "ask-claude" or "ask")
            {
                var aiArgs = new List<string>();
                string aiName;
                if (maybeAction == "ask-gpt") aiName = "gpt";
                else if (maybeAction == "ask-gemini") aiName = "gemini";
                else if (maybeAction == "ask-claude") aiName = "claude";
                else if (args.Length >= 2 && args[1].ToLowerInvariant() is "gpt" or "gemini" or "claude")
                {
                    aiName = args[1].ToLowerInvariant();
                    aiArgs.Add(aiName);
                    aiArgs.AddRange(args.Skip(2));
                    return AskCommand(aiArgs.ToArray());
                }
                else
                {
                    Console.WriteLine("Usage: a11y ask-gpt \"question\" [file.png] | a11y ask-gemini \"question\" | a11y ask-claude \"question\" | a11y ask gpt|gemini|claude \"question\"");
                    return 1;
                }
                aiArgs.Add(aiName);
                // Rebuild args: extract --text as question, grap as file, pass rest through
                var restArgs = args.Skip(1).ToList();
                for (int ri = 0; ri < restArgs.Count; ri++)
                {
                    if (restArgs[ri] == "--text" && ri + 1 < restArgs.Count)
                    {
                        aiArgs.Add(restArgs[ri + 1]); // question text
                        ri++;
                    }
                    else if (!restArgs[ri].StartsWith("--"))
                    {
                        aiArgs.Add(restArgs[ri]); // file path or question
                    }
                    else
                    {
                        aiArgs.Add(restArgs[ri]); // pass flags through
                    }
                }
                return AskCommand(aiArgs.ToArray());
            }
        }

        if (args.Length == 1 && args[0] is "file-read" or "file-write")
            return Error($"{args[0]} requires a file path: a11y {args[0]} \"path/to/file.cpp\" [--encoding 949]");

        if (args.Length == 1 && args[0] is "clipboard-read" or "clipboard")
            return ClipboardRead();

        if (args.Length < 2)
        {
            var ver = typeof(Program).Assembly.GetName().Version;
            var verStr = ver != null ? $"v{ver.Major}.{ver.Minor}.{ver.Build}" : "";
            Console.WriteLine();
            Console.WriteLine($"  WKAppBot a11y {verStr}");
            Console.WriteLine($"  A MUD-style portal for AI to see and touch the real world.");
            Console.WriteLine($"  find = look, read = examine, invoke = open the door.");
            Console.WriteLine($"  26 actions · 3-tier fallback · focusless · zoom overlay");
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
            Console.WriteLine("  kill        Kill processes by name pattern (no window needed)");
            Console.WriteLine("              WM_CLOSE first if window exists, else force kill");
            Console.WriteLine("              parent/child chain, ** wildcard, #exeFilter");
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
            Console.WriteLine("  type        Type text \"...\" — focusless WM_CHAR / SendKeys");
            Console.WriteLine("  set-value   Set value directly \"...\"");
            Console.WriteLine("  set-range   Set numeric range (--value N)");
            Console.WriteLine();
            Console.WriteLine("═══ Async Actions (2) ══════════════════════════════════");
            Console.WriteLine("  wait        Poll until window/element appears (--timeout --interval)");
            Console.WriteLine("  eval        Execute JavaScript via CDP \"js expr\"");
            Console.WriteLine();
            Console.WriteLine("═══ Utility ═══════════════════════════════════════════════");
            Console.WriteLine("  clipboard        Read clipboard text (Unicode + CP949 auto-detect)");
            Console.WriteLine("  clipboard-write  Write text/files to clipboard");
            Console.WriteLine("                   \"line1\" \"line2\" → text, file paths → CF_HDROP");
            Console.WriteLine("  file-read <path> [--encoding 949]   Read file as Unicode (CP949/UTF-8/...)");
            Console.WriteLine("  file-write <path> --text \"...\" [--encoding 949]   Write with re-encoding");
            Console.WriteLine("                   Use --text \"@tmp.txt\" to reference a temp file (large content)");
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
            Console.WriteLine("  --allow-ancestors  ⚠ Bypass ancestor process guard (all interactive actions)");
            Console.WriteLine("                     Always prints warning. --force-close-ancestors is a deprecated alias.");
            Console.WriteLine("  --repeat    Repeat action on chain dialogs (max 10, 1s interval)");
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
            Console.WriteLine("  a11y close Notepad                       Close Notepad");
            Console.WriteLine("  a11y find Notepad --depth 5              Explore element tree");
            Console.WriteLine("  a11y invoke \"Notepad#File\"              Click 'File' menu");
            Console.WriteLine("  a11y type \"HTS#StockCode\" \"005930\"");
            Console.WriteLine();
            Console.WriteLine("═══ Synthesized Full-Path (v3.0) — Unified Web+Native ═══");
            Console.WriteLine("  a11y find  \"Chrome#ChatGPT\"             Tab portal → web tree");
            Console.WriteLine("  a11y invoke \"Chrome#Gemini#Default Menu\"  Tab → web button click");
            Console.WriteLine("  a11y click \"Chrome#button.submit\"       CSS → CDP auto-fallback!");
            Console.WriteLine("  a11y type  \"Chrome#input[name=q]\" \"hello\"  CDP type");
            Console.WriteLine("  a11y read  \"Claude#.editor\"             Electron + CDP read");
            Console.WriteLine("  --eval-js: run JS before any CDP action (pre-hook)");
            Console.WriteLine("  a11y click \"Chrome#button[type=submit]\" --eval-js \"document.querySelector('button').disabled=false\"");
            Console.WriteLine("  a11y read \"Chrome#chatgpt.com\" --eval-js \"document.title\"   JS + read combo");
            Console.WriteLine();
            Console.WriteLine("  grap = Win32#a11y — unified native + web path");
            Console.WriteLine("    Notepad                plain text = UIA Name match");
            Console.WriteLine("    Chrome#ChatGPT#Send    UIA scope / tab portal");
            Console.WriteLine("    Chrome#button.submit   CSS selector → CDP auto-detect!");
            Console.WriteLine("    Chrome#[aria-label=X]  CSS attr → CDP");
            Console.WriteLine("    HTS/0338#RealTimeAcct  / = Win32 child, # = UIA scope");
            Console.WriteLine("    */? wildcards · regex: · `;` OR · aid fallback");
            Console.WriteLine();
            Console.WriteLine("─── © 2026 WilKim · github.com/kiexpert/WKAppBot ──────────");
            return 1;
        }

        var action = args[0].ToLowerInvariant();
        var grap = args[1];
        bool all = args.Any(a => a == "--all");
        bool force = args.Any(a => a == "--force");
        // --allow-ancestors: bypass ancestor process guard for ALL interactive actions.
        // ⚠ ALWAYS prints warning — so junior AIs learn this is dangerous.
        // --force-close-ancestors kept as deprecated alias.
        bool allowAncestors = args.Any(a => a == "--allow-ancestors" || a == "--force-close-ancestors");
        if (allowAncestors)
            Console.WriteLine("[⚠ ALLOW-ANCESTORS] Ancestor process guard disabled — you may interact with your own parent process tree. Be careful not to interfere with other active sessions!");
        bool speak = args.Any(a => a == "--speak");
        bool repeat = args.Any(a => a == "--repeat");
        bool hotkey = args.Any(a => a == "--hotkey"); // type: Win32 label/menu dispatch instead of text input
        int countN = 1;
        { var ci = Array.IndexOf(args, "--count"); if (ci >= 0 && ci + 1 < args.Length && int.TryParse(args[ci + 1], out var cv)) countN = Math.Max(1, cv); }

        // Parse action-specific params
        int? mx = null, my = null, mw = null, mh = null;
        string? text = null;
        double? rangeValue = null;
        string scrollDir = "down";
        string scrollAmount = "small";
        string? nthRaw = null;
        string? evalJs = null;
        int findDepth = 3;
        int timeoutMs = 10000;
        int intervalMs = 500;
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
            else if (a == "--timeout" && i + 1 < args.Length && int.TryParse(args[i + 1], out var tv)) { timeoutMs = tv; i++; }
            else if (a == "--interval" && i + 1 < args.Length && int.TryParse(args[i + 1], out var iv)) { intervalMs = iv; i++; }
            else if (a == "--eval-js" && i + 1 < args.Length) { evalJs = args[++i]; }
        }

        // Parse encoding param (for file-read/file-write)
        string? encodingArg = null;
        for (int i = 0; i < args.Length - 1; i++)
            if (args[i].ToLowerInvariant() is "--encoding" or "--enc") { encodingArg = args[i + 1]; break; }

        // Validate
        if (action == "move" && (mx == null || my == null))
            return Error("move requires --x N --y N");
        if (action == "resize" && (mw == null || mh == null))
            return Error("resize requires --w N --h N");
        // keystroke/hotkey → type alias (통합)
        if (action is "hotkey" or "keystroke") action = "type";
        // type/set-value/eval: --text 없으면 잔여 위치 인수(index 2+, -- 미시작) 공백 합치기
        // a11y type "*App*" hello world  →  text = "hello world"
        // a11y type "*App*" 파일/저장 --hotkey  →  text = "파일/저장"
        if ((action is "type" or "set-value" or "eval") && text == null && args.Length >= 3)
        {
            // -- 옵션의 값으로 소비된 인수 인덱스 제외
            var valueOpts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                { "--text", "--nth", "--n", "--depth", "--timeout", "--interval",
                  "--value", "--x", "--y", "--w", "--h", "--encoding", "--count", "--eval-js" };
            var consumedIdx = new HashSet<int>();
            for (int i = 2; i < args.Length - 1; i++)
                if (args[i].StartsWith("--") && valueOpts.Contains(args[i]))
                    consumedIdx.Add(i + 1);
            var parts = args.Skip(2)
                .Select((a, i) => (a, idx: i + 2))
                .Where(t => !t.a.StartsWith("--") && !consumedIdx.Contains(t.idx))
                .Select(t => t.a)
                .ToArray();
            if (parts.Length > 0) text = string.Join(" ", parts);
        }
        if ((action is "type" or "set-value" or "eval") && text == null)
            return Error($"{action} requires text argument (e.g., a11y {action} \"target\" \"value\")");

        if (action == "set-range" && rangeValue == null)
            return Error("set-range requires --value N");

        // ═══ Special: clipboard (no window needed) ═══
        if (action == "clipboard" && args.Length <= 1)
        {
            Console.WriteLine("═══ Clipboard ═════════════════════════════════════════════");
            Console.WriteLine("  a11y clipboard                         Show this help");
            Console.WriteLine("  a11y clipboard-read                    Read text (Unicode + CP949)");
            Console.WriteLine("  a11y clipboard-write \"text\" ...        Write text (CF_UNICODETEXT)");
            Console.WriteLine("  a11y clipboard-write file.png ...      Copy files (CF_HDROP)");
            Console.WriteLine("  a11y clipboard-write \"msg\" img.png     Mixed: text→.txt + files");
            Console.WriteLine();
            Console.WriteLine("  Mixed mode uses [file:name] markers inline, like `ask` command.");
            return 0;
        }
        if (action is "clipboard-read" or "clipboard")
            return ClipboardRead();
        if (action is "clipboard-write" or "clipboard-copy")
        {
            // args[1..] = payload (slack send / ask style: text+file mixed)
            var payload = args.Skip(1).Where(a => !a.StartsWith("--")).ToArray();
            if (payload.Length == 0)
                return Error("clipboard-write requires text: a11y clipboard-write \"line1\" \"line2\"");

            // Separate files vs text with [file:] markers (shared ParseTextAndFilesWithMarkers)
            var (textParts, files) = ParseTextAndFilesWithMarkers(payload);

            // Mixed text+files → text as .txt (with [file:] markers), all as CF_HDROP
            if (files.Count > 0 && textParts.Any(p => !p.StartsWith("[file:")))
            {
                var textContent = string.Join(Environment.NewLine, textParts);
                var firstText = textParts.FirstOrDefault(p => !p.StartsWith("[file:")) ?? "clipboard";
                var preview = firstText.Length > 20 ? firstText[..20] : firstText;
                var safeName = string.Concat(preview.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c)).Trim();
                if (string.IsNullOrEmpty(safeName)) safeName = "clipboard";
                var tmpFile = Path.Combine(Path.GetTempPath(), $"{safeName}.txt");
                File.WriteAllText(tmpFile, textContent, System.Text.Encoding.UTF8);
                files.Insert(0, tmpFile);
                Console.WriteLine($"[CLIPBOARD] text → {tmpFile}");
                Console.WriteLine($"  preview: {textContent.Replace(Environment.NewLine, " | ")}");
                return ClipboardWriteFiles(files.ToArray());
            }

            // Files only → CF_HDROP
            if (files.Count > 0)
                return ClipboardWriteFiles(files.ToArray());

            // Text only → CF_UNICODETEXT
            return ClipboardWrite(string.Join(Environment.NewLine, textParts));
        }

        // ═══ Special: file-read / file-write (encoding-aware, no window needed) ═══
        if (action is "file-read" or "file-write")
        {
            if (string.IsNullOrEmpty(grap))
                return Error($"{action} requires a file path: a11y {action} \"path/to/file.cpp\" [--encoding 949]");
            return FileReadWrite(action, grap, text, encodingArg, args);
        }

        // ═══ Special: file-edit (search-replace with backup, encoding-aware) ═══
        if (action == "file-edit")
        {
            if (string.IsNullOrEmpty(grap))
                return Error("file-edit requires a file path: a11y file-edit \"path.cs\" --old-string \"foo\" --text \"bar\"");
            string? oldStr = null;
            for (int i = 2; i < args.Length - 1; i++)
                if (args[i].Equals("--old-string", StringComparison.OrdinalIgnoreCase)) { oldStr = args[i + 1]; break; }
            if (oldStr == null)
                return Error("file-edit requires --old-string <search_text>");
            if (text == null)
                return Error("file-edit requires --text <replacement_text>");
            var editArgs = new List<string> { "edit", oldStr, text, grap };
            if (args.Any(a => a.Equals("--replace-all", StringComparison.OrdinalIgnoreCase))) editArgs.Add("--replace-all");
            if (args.Any(a => a.Equals("--regex", StringComparison.OrdinalIgnoreCase)))       editArgs.Add("--regex");
            if (args.Any(a => a.Equals("--i-really-want-lossy-encoding", StringComparison.OrdinalIgnoreCase))) editArgs.Add("--i-really-want-lossy-encoding");
            if (args.Any(a => a.Equals("--i-really-want-no-backup", StringComparison.OrdinalIgnoreCase)))   editArgs.Add("--i-really-want-no-backup");
            if (encodingArg != null) { editArgs.Add("--encoding"); editArgs.Add(encodingArg); }
            for (int i = 2; i < args.Length - 1; i++)
                if (args[i].Equals("--context", StringComparison.OrdinalIgnoreCase)) { editArgs.Add("--context"); editArgs.Add(args[i + 1]); break; }
            return FileCommand(editArgs.ToArray());
        }

        // ═══ Special: wait action (polls for window/element, early return) ═══
        if (action == "wait")
        {
            // Parse wait-specific options: --condition "text" --not
            _waitCondition = null;
            _waitNot = false;
            for (int wi = 0; wi < args.Length; wi++)
            {
                if (args[wi] == "--condition" && wi + 1 < args.Length) _waitCondition = args[wi + 1];
                if (args[wi] == "--not") _waitNot = true;
            }
            return A11yWaitAction(grap, timeoutMs, intervalMs);
        }

        // ═══ Special: kill (process kill by name/cmdline pattern, no window needed) ═══
        if (action == "kill")
            return A11yKillByPattern(grap, allowAncestors);

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

        // ── Ancestor process guard: skip windows owned by own process tree ──
        // Applied to ALL interactive actions (not just close) unless --allow-ancestors is set.
        // Read-only actions (inspect/find/windows/screenshot/ocr/read/highlight/wait) are exempt.
        bool isInteractiveAction = action is
            "close" or "invoke" or "click" or "toggle" or "expand" or "collapse" or
            "select" or "scroll" or "type" or "set-value" or "set-range" or
            "minimize" or "maximize" or "restore" or "focus" or "move" or "resize";
        if (isInteractiveAction && !allowAncestors)
        {
            // All interactive actions: window-hierarchy ancestors only
            // (kill has its own handler using process ancestors instead)
            var windowAncs = GetWindowHierarchyAncestors();
            allWindows.RemoveAll(w =>
            {
                if (windowAncs.Contains(w.Handle))
                {
                    Console.WriteLine($"[GUARD] window-ancestor-protected (hwnd=0x{w.Handle:X}): \"{w.Title}\" — use --allow-ancestors to override");
                    return true;
                }
                return false;
            });
        }

        if (allWindows.Count == 0)
            return Error($"No window found: \"{win32Segments[0]}\"");

        // ── User idle time (input readiness hint) ──
        if (isInteractiveAction)
        {
            var idleMs = NativeMethods.GetUserIdleMs();
            var idleStr = idleMs >= 60000 ? $"{idleMs / 60000}m {idleMs / 1000 % 60}s"
                        : idleMs >= 1000  ? $"{idleMs / 1000.0:F1}s"
                        :                   $"{idleMs}ms";
            Console.WriteLine($"[IDLE] user input {idleStr} ago");
        }

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

        // ═══ STEP 4.5: Hot focus chain (show on all actions) ═══
        try
        {
            using var uiaFocus = new UiaLocator();
            var focusChain = uiaFocus.GetFocusChain(targets[0].Handle);
            if (!string.IsNullOrEmpty(focusChain))
                Console.Error.Write(focusChain);
        }
        catch { /* best effort */ }

        // ═══ STEP 4.9: Elevation auto-detect → Elevated Eye Proxy delegation ═══
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            targets[0].Handle, "a11y", args);
        if (delegated) return delegateExit;

        // ═══ STEP 4.95: close on browser window ═══
        // Rule: browser windows (CDP port detected) REQUIRE a #tabHint to close a tab.
        //       Without #tabHint → error (prevents accidental whole-browser close).
        //       Use --force to override and close the whole window anyway.
        if (action == "close" && string.IsNullOrEmpty(uiaPath) && !force)
        {
            // Check if any matched window is a browser (has CDP port) → show tab list
            var portsChecked2 = new HashSet<int>();
            foreach (var w in allWindows)
            {
                NativeMethods.GetWindowThreadProcessId(w.Handle, out uint wPid2);
                var cdpPort2 = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)wPid2);
                if (cdpPort2 <= 0 || !portsChecked2.Add(cdpPort2)) continue;
                try
                {
                    var http2 = new System.Net.Http.HttpClient();
                    var json2 = http2.GetStringAsync($"http://127.0.0.1:{cdpPort2}/json").GetAwaiter().GetResult();
                    var doc2 = System.Text.Json.JsonDocument.Parse(json2);
                    var pages = doc2.RootElement.EnumerateArray()
                        .Where(t => !t.TryGetProperty("type", out var tp2) || tp2.GetString() == "page")
                        .ToList();
                    if (pages.Count == 0) continue;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[A11Y] Browser has {pages.Count} tab(s) — specify which to close with #hint:");
                    Console.ResetColor();
                    foreach (var tab in pages)
                    {
                        var tabUrl = tab.TryGetProperty("url", out var u2) ? u2.GetString() ?? "" : "";
                        var tabTitle = tab.TryGetProperty("title", out var tt2) ? tt2.GetString() ?? "" : "";
                        // Strip scheme, keep host+path up to 40 chars (e.g. "chatgpt.com/c/69b0f4")
                        var hint = tabUrl.StartsWith("https://") ? tabUrl[8..Math.Min(tabUrl.Length, 48)]
                                 : tabUrl.StartsWith("http://")  ? tabUrl[7..Math.Min(tabUrl.Length, 47)]
                                 : tabUrl[..Math.Min(tabUrl.Length, 40)];
                        Console.WriteLine($"  \"{tabTitle}\"");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"    → a11y close \"{grap}#{hint}\"");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  ⚠ Browser main window cannot be closed without #tab-hint (safety guard).");
                    Console.WriteLine($"  ⚠ Use --force to kill the whole browser (WM_CLOSE → Process.Kill).");
                    Console.ResetColor();
                    return 1;
                }
                catch { }
            }
        }

        if (action == "close" && !string.IsNullOrEmpty(uiaPath))
        {
            var tabHint = uiaPath;
            var http = new System.Net.Http.HttpClient();
            int totalClosed = 0;
            var portsChecked = new HashSet<int>();
            // Search ALL matched windows (not just first) for a browser with CDP
            foreach (var w in allWindows)
            {
                NativeMethods.GetWindowThreadProcessId(w.Handle, out uint wPid);
                var cdpPort = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)wPid);
                if (cdpPort <= 0 || !portsChecked.Add(cdpPort)) continue;
                try
                {
                    var json = http.GetStringAsync($"http://127.0.0.1:{cdpPort}/json").GetAwaiter().GetResult();
                    var doc = System.Text.Json.JsonDocument.Parse(json);
                    foreach (var tab in doc.RootElement.EnumerateArray())
                    {
                        if (tab.TryGetProperty("type", out var tp) && tp.GetString() != "page") continue;
                        var tabUrl = tab.TryGetProperty("url", out var u) ? u.GetString() ?? "" : "";
                        var tabTitle = tab.TryGetProperty("title", out var tt) ? tt.GetString() ?? "" : "";
                        if (!tabUrl.Contains(tabHint, StringComparison.OrdinalIgnoreCase) &&
                            !tabTitle.Contains(tabHint, StringComparison.OrdinalIgnoreCase)) continue;
                        var tabId = tab.TryGetProperty("id", out var id) ? id.GetString() : null;
                        if (tabId == null) continue;
                        http.GetStringAsync($"http://127.0.0.1:{cdpPort}/json/close/{tabId}").GetAwaiter().GetResult();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[A11Y] tab closed [{cdpPort}]: \"{tabTitle}\" ({tabUrl[..Math.Min(70, tabUrl.Length)]})");
                        Console.ResetColor();
                        totalClosed++;
                    }
                }
                catch { /* port not responsive, skip */ }
            }
            if (totalClosed > 0)
            {
                Console.WriteLine($"[A11Y] Done: {totalClosed} tab(s) closed");
                return 0;
            }
            // No CDP tab found — error, do NOT fall through to window close!
            return Error($"No browser tab matching \"{tabHint}\" found.\n  Tip: use a11y close \"*App*\" (no #tabHint) to close the whole window.");
        }

        // ═══ STEP 5: Execute on each target ═══
        int ok = 0, fail = 0;
        using var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);
        var readiness = CreateInputReadiness();
        var aar = CreateActionReadiness(readiness);
        var gapCollector = new OcrGapCollector();

        foreach (var win in targets)
        {
            var hwnd = win.Handle;
            var title = win.Title;
            var tag = $"0x{hwnd.ToInt64():X} \"{title}\"";

            // ── Diagnostic: foreground window + UIA focused element ──
            {
                var fgHwnd = NativeMethods.GetForegroundWindow();
                var fgTitle = WindowFinder.GetWindowText(fgHwnd);
                if (fgTitle.Length > 50) fgTitle = fgTitle[..50] + "...";
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint tPid);
                NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fgPid);
                var relation = fgHwnd == hwnd ? "SAME" : fgPid == tPid ? "SAME-PROC" : "DIFFERENT";
                var idleMs = NativeMethods.GetUserIdleMs();
                Console.Error.WriteLine($"[FOCUS] fg=0x{fgHwnd.ToInt64():X} \"{fgTitle}\" | target={tag} | {relation} | idle={idleMs}ms");

                // UIA focused element chain
                try
                {
                    using var uiaAuto = new UIA3Automation();
                    var focused = uiaAuto.FocusedElement();
                    if (focused != null)
                    {
                        var fType = "?"; try { fType = focused.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var fName = focused.Properties.Name.ValueOrDefault ?? "";
                        if (fName.Length > 40) fName = fName[..40] + "...";
                        var fAid = focused.Properties.AutomationId.ValueOrDefault ?? "";
                        var fRect = "";
                        try { var r = focused.Properties.BoundingRectangle.ValueOrDefault; fRect = $" rect=({r.X},{r.Y} {r.Width}x{r.Height})"; } catch { }
                        NativeMethods.GetWindowThreadProcessId(focused.Properties.NativeWindowHandle.ValueOrDefault, out uint focPid);
                        var focRelation = focPid == tPid ? "TARGET-PROC" : focPid == fgPid ? "FG-PROC" : "OTHER";
                        Console.Error.WriteLine($"[FOCUS] uia-focused: {fType} \"{fName}\" (aid=\"{fAid}\"){fRect} | {focRelation}");
                    }
                }
                catch { /* best effort */ }
            }

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

            // ── AAR: 윈도우 레벨 입력위치확보 ──
            // Window-level readiness: iconic restore + blocker dismiss
            // Element-level readiness runs later after UIA scope resolution
            if (action != "close" && action != "minimize")
                EnsureWindowReady(hwnd, $"a11y-{action}", title, readiness);

            // ── Readiness Probe: 돋보기 + 방해꾼 리타겟 + 포커스 양보 ──
            // All actions: magnifier + blocker detection
            // Interaction actions: + yield popup if user is active
            {
                var probeReport = readiness.Probe(new InputReadinessRequest
                {
                    TargetHwnd = hwnd,
                    IntendedAction = action,
                });
                if (probeReport.UserYieldConfirmed)
                    Console.WriteLine($"[A11Y] yield confirmed for {action}");

                // ── Blocker retarget: 팝업이 타겟을 가리면 팝업으로 리타겟 ──
                if (probeReport.ActiveBlocker != null)
                {
                    var blocker = probeReport.ActiveBlocker;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[A11Y] ⚠ RETARGET: [{blocker.ClassName}] \"{blocker.Title}\" blocking {tag}");
                    Console.ResetColor();
                    hwnd = blocker.Handle;
                    title = blocker.Title;
                    tag = $"0x{hwnd.ToInt64():X} \"{title}\"";
                }
            }

            bool success = false;

            if (isElementAction)
            {
                // ── CDP Tab List: browser target + no #scope + read/inspect → show tabs ──
                if (string.IsNullOrEmpty(uiaPath) && action is "read" or "find" or "inspect")
                {
                    NativeMethods.GetWindowThreadProcessId(hwnd, out uint tabPid);
                    var tabPort = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)tabPid);
                    if (tabPort > 0)
                    {
                        success = CdpListTabs(tabPort, tag);
                        if (success) ok++; else fail++;
                        continue;
                    }
                }

                // ── CDP Telepathy: if CSS pattern, try CDP first on ANY browser ──
                bool isCssPattern = !string.IsNullOrEmpty(uiaPath) && GrapHelper.LooksLikeCssSelector(uiaPath);

                if (isCssPattern)
                {
                    // Telepathy: try CDP on any process that has a debugging port
                    var cdpResult = TryWebViewCdpAction(hwnd, action, uiaPath!, text, rangeValue, scrollDir, scrollAmount, findDepth, speak, hotkey, evalJs, timeoutMs, intervalMs);
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
                    AutomationElement? scoped = null;

                    // --nth on element level: collect all matches, sort by coverage, pick nth
                    if (nthRaw != null)
                    {
                        var allMatches = GrapHelper.FindUiaScopeAll(root, uiaPath);
                        if (allMatches.Count > 0)
                        {
                            if (allMatches.Count > 1)
                            {
                                for (int mi = 0; mi < allMatches.Count; mi++)
                                {
                                    var (mel, mcov, mtxt) = allMatches[mi];
                                    string melType = "?"; try { melType = mel.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                                    Console.WriteLine($"[A11Y]   #{mi + 1} {melType} \"{mtxt}\" cov={mcov:P0}");
                                }
                            }
                            int elNth = 1;
                            if (!int.TryParse(nthRaw, out elNth) || elNth < 1 || elNth > allMatches.Count)
                            {
                                Console.Error.WriteLine($"[A11Y] --nth {nthRaw}: out of bounds ({allMatches.Count} element match(es))");
                                fail++; continue;
                            }
                            scoped = allMatches[elNth - 1].el;
                        }
                    }
                    else
                    {
                        // Fire parallel CCA analysis while UIA searches (non-blocking)
                        Task? parallelCca = null;
                        if (_autoHackSemaphore.Wait(0))
                        {
                            parallelCca = Task.Run(() =>
                            {
                                try
                                {
                                    NativeMethods.GetWindowRect(hwnd, out var wr4);
                                    int pw = wr4.Right - wr4.Left, ph = wr4.Bottom - wr4.Top;
                                    if (pw > 10 && ph > 10)
                                    {
                                        if (pw > 1200) pw = 1200; if (ph > 800) ph = 800;
                                        using var bmp2 = new System.Drawing.Bitmap(pw, ph);
                                        using (var g2 = System.Drawing.Graphics.FromImage(bmp2))
                                            g2.CopyFromScreen(wr4.Left, wr4.Top, 0, 0, new System.Drawing.Size(pw, ph));
                                        var cca3 = new WKAppBot.Vision.ConnectedComponentAnalyzer();
                                        var regions3 = cca3.Analyze(bmp2);
                                        NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid3);
                                        string proc3; try { proc3 = System.Diagnostics.Process.GetProcessById((int)pid3).ProcessName; } catch { proc3 = "unknown"; }
                                        var cls3 = WKAppBot.Win32.Window.WindowFinder.GetClassName(hwnd);
                                        // Collect UIA for fused match
                                        var uias3 = new List<WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo>();
                                        try
                                        {
                                            using var uia3 = new UiaLocator();
                                            var leaves3 = new List<(string text, int lx, int ly, int lw, int lh, int d)>();
                                            UiaLocator.CollectTextLeaves(root, leaves3, 0, 6);
                                            foreach (var l in leaves3)
                                                uias3.Add(new WKAppBot.Vision.CcaUiaFusedMatcher.UiaInfo
                                                { Name = l.text, Bounds = new System.Drawing.Rectangle(l.lx - wr4.Left, l.ly - wr4.Top, l.lw, l.lh) });
                                        } catch { }
                                        var mr3 = WKAppBot.Vision.CcaUiaFusedMatcher.Match(regions3, uias3);
                                        WKAppBot.Vision.CcaUiaFusedMatcher.SaveToExperienceDb(
                                            DataDir + "/experience", proc3, cls3, mr3);
                                        Console.WriteLine($"[A11Y] Parallel CCA: {WKAppBot.Vision.CcaUiaFusedMatcher.Summarize(mr3)}");
                                    }
                                }
                                catch { }
                                finally { _autoHackSemaphore.Release(); }
                            });
                        }

                        scoped = GrapHelper.FindUiaScope(root, uiaPath);
                    }

                    if (scoped == null)
                    {
                        // UIA failed → try CDP telepathy as fallback (any browser with CDP port)
                        if (!isCssPattern && !string.IsNullOrEmpty(uiaPath))
                        {
                            Console.WriteLine($"[A11Y] UIA scope failed, trying CDP telepathy with \"{uiaPath}\"");
                            var cdpResult = TryWebViewCdpAction(hwnd, action, uiaPath!, text, rangeValue, scrollDir, scrollAmount, findDepth, speak, hotkey, evalJs, timeoutMs, intervalMs);
                            if (cdpResult != null)
                            {
                                success = cdpResult.Value;
                                if (success) ok++; else fail++;
                                continue;
                            }
                        }
                        // Experience DB fallback: check fused_match.jsonl for cached coordinate
                        if (!string.IsNullOrEmpty(uiaPath))
                        {
                            try
                            {
                                NativeMethods.GetWindowThreadProcessId(hwnd, out uint expPid);
                                string expProc;
                                try { expProc = System.Diagnostics.Process.GetProcessById((int)expPid).ProcessName; } catch { expProc = ""; }
                                var expCls = WKAppBot.Win32.Window.WindowFinder.GetClassName(hwnd);
                                var (expBounds, expScore) = WKAppBot.Vision.CcaUiaFusedMatcher.FindByNameInExperience(
                                    DataDir + "/experience", expProc, expCls, uiaPath);
                                if (expBounds.Width > 0 && expScore > 0.3)
                                {
                                    Console.WriteLine($"[A11Y] Experience DB hit: \"{uiaPath}\" → ({expBounds.X},{expBounds.Y} {expBounds.Width}x{expBounds.Height}) score={expScore:F2}");
                                    // Use experience bounds for click/invoke actions
                                    if (action is "click" or "invoke")
                                    {
                                        NativeMethods.GetWindowRect(hwnd, out var wr3);
                                        int cx = wr3.Left + expBounds.X + expBounds.Width / 2;
                                        int cy = wr3.Top + expBounds.Y + expBounds.Height / 2;
                                        Console.WriteLine($"[A11Y] Experience click at ({cx},{cy})");
                                        WKAppBot.Win32.Input.MouseInput.Click(cx, cy);
                                        ok++;
                                        continue;
                                    }
                                }
                            }
                            catch { }
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
                var elRectStr = "";
                System.Drawing.Rectangle? elBounds = null;
                try { var r = root.Properties.BoundingRectangle.ValueOrDefault; elBounds = new(r.X, r.Y, r.Width, r.Height); elRectStr = $" rect=({r.X},{r.Y} {r.Width}x{r.Height})"; } catch { }

                Console.WriteLine($"[A11Y] element: {elType} \"{elName}\" (aid=\"{elAid}\"){elRectStr} in {tag}");

                // ── OCR gap analysis: detect missing text in element rect ──
                // Applies to all elements wider than one character (~14px)
                bool needsOcrScan = elBounds is { Width: > 0, Height: > 0 };
                bool ocrGapDetected = false;
                ClickZoomHelper? dynZoom = null; // magnifier for DYN-A11Y analysis
                if (needsOcrScan)
                {
                    var capturedBounds = elBounds!.Value;
                    try
                    {
                        using var bmp = new System.Drawing.Bitmap(capturedBounds.Width, capturedBounds.Height);
                        using (var g = System.Drawing.Graphics.FromImage(bmp))
                            g.CopyFromScreen(capturedBounds.X, capturedBounds.Y, 0, 0, capturedBounds.Size);
                        using var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                        var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();

                        if (result.Words.Count > 0)
                        {
                            // Build coverage map: which X-pixels are covered by OCR words
                            int totalW = capturedBounds.Width;
                            var covered = new bool[totalW];
                            foreach (var w in result.Words)
                            {
                                int x0 = Math.Max(0, w.X);
                                int x1 = Math.Min(totalW, w.X + w.Width);
                                for (int px = x0; px < x1; px++) covered[px] = true;
                            }

                            // Find gaps wider than one char (~14px)
                            const int charWidth = 14;
                            var gaps = new List<(int start, int width)>();
                            int gapStart = -1;
                            for (int px = 0; px < totalW; px++)
                            {
                                if (!covered[px])
                                {
                                    if (gapStart < 0) gapStart = px;
                                }
                                else
                                {
                                    if (gapStart >= 0)
                                    {
                                        int gw = px - gapStart;
                                        if (gw >= charWidth) gaps.Add((gapStart, gw));
                                        gapStart = -1;
                                    }
                                }
                            }
                            if (gapStart >= 0 && (totalW - gapStart) >= charWidth)
                                gaps.Add((gapStart, totalW - gapStart));

                            // Build display: words sorted by X + [?] for gaps
                            var sortedWords = result.Words.OrderBy(w => w.X).ToList();
                            int expectedChars = totalW / charWidth;
                            int actualChars = sortedWords.Sum(w => w.Text.Length);
                            bool partial = gaps.Count > 0 || actualChars < expectedChars / 2;

                            if (partial)
                            {
                                ocrGapDetected = true;
                                // Interleave words and [?] gaps
                                var parts = new List<string>();
                                int cursor = 0;
                                foreach (var w in sortedWords)
                                {
                                    if (w.X - cursor >= charWidth)
                                        parts.Add("[?]");
                                    parts.Add(w.Text);
                                    cursor = w.X + w.Width;
                                }
                                if (totalW - cursor >= charWidth)
                                    parts.Add("[?]");

                                var displayText = string.Join("", parts);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.ResetColor();
                                if (gapCollector.Add(capturedBounds, displayText, elAid, out var cached1))
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    Console.WriteLine($"[A11Y] Acquiring target context... \"{displayText}\"");
                                    Console.ResetColor();
                                }
                                else if (cached1 != null)
                                {
                                    elName = cached1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[A11Y] Cache hit → \"{cached1}\"");
                                    Console.ResetColor();
                                }
                            }
                            else if (string.IsNullOrWhiteSpace(elName))
                            {
                                // Full OCR success on blind node
                                elName = string.Join(" ", sortedWords.Select(w => w.Text));
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"[A11Y] OCR → \"{elName}\"");
                                Console.ResetColor();
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(elName) && string.IsNullOrWhiteSpace(elAid))
                        {
                            // No OCR words at all on blind node → definitely needs Vision
                            ocrGapDetected = true;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.ResetColor();
                            if (gapCollector.Add(capturedBounds, null, elAid, out var cached2))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine($"[A11Y] Acquiring target context... (no text detected)");
                                Console.ResetColor();
                            }
                            else if (cached2 != null)
                            {
                                elName = cached2;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"[A11Y] Cache hit → \"{cached2}\"");
                                Console.ResetColor();
                            }
                        }

                        // ── Magnifier: 1 gap → zoom on segment, 2+ gaps → zoom on parent node ──
                        if (gapCollector.HasGaps)
                        {
                            var zoomHwnd = GetElementHwnd(root);
                            if (zoomHwnd == IntPtr.Zero) zoomHwnd = hwnd;
                            int gapCount = gapCollector.Count;
                            if (gapCount == 1)
                            {
                                // Single gap: zoom on the specific segment
                                dynZoom = ClickZoomHelper.Begin(zoomHwnd, hwnd, "DYN-A11Y", "Analyzing 1 segment...");
                            }
                            else
                            {
                                // Multiple gaps: zoom on parent node (show full context)
                                dynZoom = ClickZoomHelper.Begin(hwnd, hwnd, "DYN-A11Y", $"Analyzing {gapCount} segments...");
                            }
                        }

                        // Auto-hack: if blind nodes found, trigger lightweight CCA+cache lookup
                        if (gapCollector.HasGaps && string.IsNullOrWhiteSpace(elName) && string.IsNullOrWhiteSpace(elAid))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"[A11Y] Auto-hack: {gapCollector.Count} blind segment(s) — checking cache...");
                            Console.ResetColor();
                            // Cache lookup by pixel hash (instant — no Vision call)
                            if (elBounds.HasValue)
                            {
                                try
                                {
                                    using var cropBmp = new System.Drawing.Bitmap(capturedBounds.Width, capturedBounds.Height);
                                    using (var g = System.Drawing.Graphics.FromImage(cropBmp))
                                        g.CopyFromScreen(capturedBounds.X, capturedBounds.Y, 0, 0, capturedBounds.Size);
                                    var pixHash = OcrGapCollector.ComputePixelHash(cropBmp);
                                    var cacheHit = LookupHackCache(hwnd, pixHash);
                                    if (cacheHit != null)
                                    {
                                        elName = cacheHit;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"[A11Y] Hack cache hit → \"{elName}\"");
                                        Console.ResetColor();
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch { /* OCR is best-effort */ }
                    finally { dynZoom?.Dispose(); }
                }
                var _nodeBefore = default(NodeState); // 입력위치확보 진입 시 캡처 (elHwnd 계산 후)

                // Tab activation: if target is inside an unselected tab, activate it first
                EnsureTabActive(root);

                // ── Blocker info for read-only actions (find/read/highlight) ──
                if (action is "read" or "find" or "highlight")
                {
                    var blockerInfo = readiness.DetectBlocker(hwnd);
                    if (blockerInfo != null)
                        Console.WriteLine($"[BLOCK] Blocker detected: \"{blockerInfo.Title}\" (class={blockerInfo.ClassName}, hwnd=0x{blockerInfo.Handle.ToInt64():X8})");
                }

                // ── AAR: element-level readiness check ──
                if (action is not ("read" or "find" or "highlight"))
                {
                    var aarTarget = new UiaActionTarget(root);
                    var aarCtx = new ReadinessContext { Hwnd = hwnd };
                    var aarResult = aar.Ensure(action, aarTarget, aarCtx);
                    if (aarResult == null)
                    {
                        Console.Error.WriteLine($"[A11Y] AAR blocked: {action} on \"{elName}\"");
                        fail++;
                        continue;
                    }
                    if (aarResult != aarTarget)
                    {
                        // Retarget: AAR found a popup/modal — switch root
                        Console.WriteLine($"[A11Y] AAR retarget: \"{aarResult.DisplayName}\"");
                        if (aarResult is UiaActionTarget retargetUia)
                            root = retargetUia.Element;
                    }
                }

                // Zoom: show magnifier/highlight on target element
                var elRect = GetBoundingRect(root);
                var elHwnd = GetElementHwnd(root);
                // Pre-measure WM_NULL baseline — cached for invoke hollow detection (TTL=3s)
                if (elHwnd != IntPtr.Zero) PreheatWindow(elHwnd);

                // ── 입력위치확보 진입: 대상 노드 풀 덤프 (BEFORE) ──
                if (countN == 1)
                    _nodeBefore = PrintNodeBefore(root, elHwnd, action);

                // 중간체크 abort 시 UIA 포커스 노드 덤프 콜백 등록
                // 클로저로 root + elHwnd 캡처 → abort 시 UIA 재스캔 없이 액션 타겟 재현
                var _abortRoot = root;
                var _abortElHwnd = elHwnd;
                Win32.Input.KeyboardInput.OnMidInputAbort = (reason, ctx, intended) =>
                {
                    PrintMidInputAbortNode(reason, ctx, intended, automation, _abortRoot, _abortElHwnd);

                    // Auto bug report: capture callstack + last log line → suggest
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            var stack = Environment.StackTrace;
                            var fgHwnd = NativeMethods.GetForegroundWindow();
                            var fgTitle = WindowFinder.GetWindowText(fgHwnd);
                            var report = $"[BUG-AUTO] MidInputAbort reason={reason} fg=0x{fgHwnd.ToInt64():X} \"{fgTitle}\"\n" +
                                $"intended=0x{intended.ToInt64():X} ctx={ctx}\n" +
                                $"stack: {stack[..Math.Min(500, stack.Length)]}";
                            SuggestCommand(new[] { report });
                        }
                        catch { }
                    });

                    // FOCUS-DRIFT: show yield popup immediately and wait for user approval
                    // before the action fails and caller retries (so next Probe() auto-approves)
                    if (reason == "FOCUS-DRIFT" && readiness?.UserInputWait != null && intended != IntPtr.Zero)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [READINESS] FOCUS-DRIFT mid-input → showing yield popup (blocking until approved)");
                        Console.ResetColor();
                        Console.Out.Flush();
                        readiness.UserInputWait.WaitForUserYield(intended, 0u, timeoutSeconds: 30, positionHwnd: intended);
                    }
                };

                // Zoom: fire in parallel so it doesn't block the action (WPF creation ~330ms)
                // Skip zoom for stress test (--count N > 1) — avoid 334ms+800ms overhead per iteration
                Task<ClickZoomHelper?>? zoomTask = null;
                if (action != "highlight" && countN == 1)
                {
                    var capturedHwnd = elHwnd; var capturedRect = elRect;
                    var capturedLabel = $"{elType} \"{elName}\"";
                    zoomTask = Task.Run<ClickZoomHelper?>(() => capturedHwnd != IntPtr.Zero
                        ? ClickZoomHelper.Begin(capturedHwnd, hwnd, $"a11y-{action}", capturedLabel)
                        : capturedRect != null ? ClickZoomHelper.BeginFromRect(capturedRect.Value, hwnd, $"a11y-{action}", capturedLabel)
                        : null);
                }

                // --count N: stress-test loop, reports per-iteration timing
                var swTotal = System.Diagnostics.Stopwatch.StartNew();
                for (int ci = 0; ci < countN; ci++)
                {
                    // Reset auto-heal tier log before each click/invoke attempt
                    if (action is "click" or "invoke")
                        _autoHealTiers = new List<string>();

                    var swIter = System.Diagnostics.Stopwatch.StartNew();
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
                        "type" => A11yType(root, hwnd, text!, hotkey),
                        "set-value" => A11ySetValue(root, hwnd, text!),
                        "set-range" => A11ySetRange(root, rangeValue!.Value),
                        "focus" => A11yFocusElement(root, hwnd),
                        _ => A11yNotYet(action)
                    };
                    swIter.Stop();
                    if (countN > 1)
                        Console.WriteLine($"[A11Y] [{ci+1}/{countN}] {(success?"ok":"FAIL")} {swIter.ElapsedMilliseconds}ms");
                }
                swTotal.Stop();
                if (countN > 1)
                    Console.WriteLine($"[A11Y] stress: {countN} iters, total={swTotal.ElapsedMilliseconds}ms, avg={swTotal.ElapsedMilliseconds/countN}ms, rate={countN*1000.0/swTotal.ElapsedMilliseconds:F1}/s");

                // ── 입력위치확보 해제: 상태 diff 출력 (AFTER) + callback 해제 ──
                Win32.Input.KeyboardInput.OnMidInputAbort = null;
                if (countN == 1)
                    PrintNodeAfter(root, _nodeBefore, action, success, swTotal.ElapsedMilliseconds);

                // Zoom: fire-and-forget ShowPass/Dispose — don't block action result on WPF fade
                var capturedSuccess = success; var capturedAction = action;
                if (zoomTask != null)
                    _ = zoomTask.ContinueWith(t =>
                    {
                        var zoom = t.Result;
                        if (zoom == null) return;
                        if (capturedSuccess) zoom.ShowPass($"{capturedAction} OK");
                        else zoom.ShowFail($"{capturedAction} FAIL");
                        Thread.Sleep(800);
                        zoom.Dispose();
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);

                // --speak: TTS 카라오케 (a11y 공통 옵션)
                if (speak && success)
                {
                    var speakText = root.Properties.Name.ValueOrDefault;
                    try { if (string.IsNullOrEmpty(speakText)) speakText = root.Patterns.Value.PatternOrDefault?.Value?.ValueOrDefault?.ToString(); } catch { }
                    if (!string.IsNullOrWhiteSpace(speakText))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "wkappbot",
                                Arguments = $"speak \"{speakText.Replace("\"", "'")}\" --bg",
                                UseShellExecute = false,
                                CreateNoWindow = true
                            });
                        }
                        catch { /* best effort */ }
                    }
                }

                // Zoom result feedback handled above via ContinueWith (fire-and-forget)

                // Auto-heal: click/invoke 전 티어 실패 시 삼두에 해결방법 자동 문의
                // 조건: CWD=WKAppBot 프로젝트 OR --auto-heal 플래그
                if (!success && action is "click" or "invoke" && ShouldAutoHeal(args))
                    FireAutoHeal(action, root, hwnd, title);
            }
            else
            {
                // ── AAR: window-level readiness check ──
                if (action is not ("close" or "minimize"))
                {
                    var aarTarget = new UiaActionTarget(automation.FromHandle(hwnd));
                    var aarCtx = new ReadinessContext { Hwnd = hwnd };
                    var aarResult = aar.Ensure(action, aarTarget, aarCtx);
                    if (aarResult == null)
                    {
                        Console.Error.WriteLine($"[A11Y] AAR blocked: {action} on \"{title}\"");
                        fail++;
                        continue;
                    }
                }
                else if (action == "close")
                {
                    // close: AAR retarget → dismiss child popup first
                    var aarTarget = new UiaActionTarget(automation.FromHandle(hwnd));
                    var aarCtx = new ReadinessContext { Hwnd = hwnd };
                    var aarResult = aar.Ensure(action, aarTarget, aarCtx);
                    if (aarResult != null && aarResult != aarTarget && aarResult is UiaActionTarget retUia)
                    {
                        var retHwnd = retUia.NativeHandle is IntPtr h ? h : IntPtr.Zero;
                        if (retHwnd != IntPtr.Zero)
                        {
                            Console.WriteLine($"[A11Y] AAR: closing child popup \"{aarResult.DisplayName}\" first");
                            NativeMethods.SendMessageTimeoutW(retHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
                            Thread.Sleep(500);
                        }
                    }
                }

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
                    "eval" => DeprecatedEval(hwnd, tag, text!, uiaPath),
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

            // ── Knowhow broadcast: 선배 클롣의 경험을 후배 클롣에게 전달 ──
            BroadcastActionKnowhow(hwnd, action, success);

            if (success) ok++; else fail++;

            // ── --repeat: 연쇄 다이얼로그 자동 dismiss 루프 ──
            // After successful action, check for new blocker from same process → re-execute
            if (repeat && success)
            {
                var origHwnd = win.Handle;
                const int maxRepeat = 10;
                for (int rep = 0; rep < maxRepeat; rep++)
                {
                    Thread.Sleep(1000);
                    var nextBlocker = readiness.DetectBlocker(origHwnd);
                    if (nextBlocker == null) break;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[A11Y] ⚠ REPEAT {rep + 1}: [{nextBlocker.ClassName}] \"{nextBlocker.Title}\"");
                    Console.ResetColor();

                    // Re-execute same action on the new blocker
                    var repHwnd = nextBlocker.Handle;
                    var repTag = $"0x{repHwnd.ToInt64():X} \"{nextBlocker.Title}\"";
                    bool repOk;
                    if (isElementAction)
                    {
                        try
                        {
                            var repRoot = automation.FromHandle(repHwnd);
                            if (!string.IsNullOrEmpty(uiaPath))
                            {
                                var scoped = GrapHelper.FindUiaScope(repRoot, uiaPath);
                                if (scoped != null) repRoot = scoped;
                            }
                            repOk = action switch
                            {
                                "click" => A11yClick(repRoot, repHwnd),
                                "invoke" => A11yInvoke(repRoot, repHwnd),
                                _ => A11yClick(repRoot, repHwnd), // default to click
                            };
                        }
                        catch { repOk = false; }
                    }
                    else
                    {
                        // Window-level: send WM_CLOSE to blocker
                        NativeMethods.SendMessageTimeoutW(repHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
                        repOk = true;
                    }

                    if (repOk) ok++;
                    else { fail++; break; }
                }
            }
        }

        // ── Batch Vision fallback: composite gap screenshots → Gemini ──
        if (gapCollector.HasGaps)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[A11Y] {gapCollector.Count} gap region(s) collected — building composite for Vision API...");
            Console.ResetColor();
            try
            {
                var (images, prompt) = gapCollector.BuildTripleComposite();
                if (images.Count > 0)
                {
                    var gapDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "WKAppBot", "gap_screenshots");
                    Directory.CreateDirectory(gapDir);
                    var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    var labels = new[] { "A_forward", "B_reverse", "C_shuffle" };
                    int totalKB = 0;
                    for (int gi = 0; gi < images.Count; gi++)
                    {
                        var path = Path.Combine(gapDir, $"gap_{ts}_{labels[gi]}.png");
                        File.WriteAllBytes(path, images[gi]);
                        totalKB += images[gi].Length / 1024;
                    }
                    var promptPath = Path.Combine(gapDir, $"gap_{ts}.prompt.txt");
                    File.WriteAllText(promptPath, prompt);
                    Console.WriteLine($"[A11Y] Gap triple composite saved: {gapDir} ({totalKB}KB total, {gapCollector.Count} regions x3 variants)");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[A11Y] Gap composite failed: {ex.Message}");
            }
        }
        gapCollector.Dispose();

        Console.WriteLine($"[A11Y] Done: {ok} ok, {fail} fail (total {targets.Count})");
        return fail > 0 ? 1 : 0;
    }

    // ── 선배 클롣 경험 노하우 자동 방송 ──
    // 1. knowhow.md (앱 특성 개요) — 있으면 방송, 없으면 경로 안내
    // 2. knowhow-{action}.md (액션별) — 있으면 방송, 없으면 경로 안내
    // 3. knowhow-failed-actions.md — 실패 시 방송 or 안내
    static void BroadcastActionKnowhow(IntPtr hwnd, string action, bool success)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = "unknown";
            var className = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            try
            {
                var buf = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(hwnd, buf, 256);
                className = buf.ToString();
            }
            catch { }

            var safeProc = SanitizePathTokenForExp(procName);
            var safeClass = SanitizePathTokenForExp(className);
            var expDir = Path.Combine(DataDir, "experience", safeProc, safeClass);

            // 1. knowhow.md (앱 일반 특성)
            var generalPath = Path.Combine(DataDir, "experience", safeProc, "knowhow.md");
            if (File.Exists(generalPath))
                ShowKnowhowBroadcast(generalPath, "KNOWHOW:OS");
            else
                ShowKnowhowHint(generalPath, "Record app-wide automation traits (MFC/WPF/Electron quirks etc.)");

            // 2. knowhow-{action}.md (per action)
            var actionPath = Path.Combine(expDir, $"knowhow-{action}.md");
            if (File.Exists(actionPath))
                ShowKnowhowBroadcast(actionPath, "KNOWHOW:OS");
            else
                ShowKnowhowHint(actionPath, $"Record knowhow for {action}: success patterns, pitfalls, failures");

            // 3. On failure: knowhow-failed-actions.md
            if (!success)
            {
                var failedPath = Path.Combine(expDir, "knowhow-failed-actions.md");
                if (File.Exists(failedPath))
                    ShowKnowhowBroadcast(failedPath, "KNOWHOW:OS");
                else
                    ShowKnowhowHint(failedPath, "Record this failure: root cause and workaround");
            }
        }
        catch { /* best effort */ }
    }

    static void ShowKnowhowHint(string path, string hint)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"  [KNOWHOW:OS] 💌 No knowhow yet — {hint}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("     → Your expertise here saves future Claudes hours of trial-and-error. 🙏");
        Console.WriteLine("     → Please write what you just learned about this app/action:");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"        a11y file-write \"{path}\" --text \"## Knowhow\\n- your note here\"");
        Console.ResetColor();
    }

    // ── FocusStealer Knowhow Auto-Record ──
    // Called via ActionApi.OnFocusStealer when a nominally-focusless UIA action steals focus.
    // Appends a dated entry to knowhow-{action}.md for both:
    //   (window) experience/{proc}/{class}/knowhow-{action}.md
    //   (app)    experience/{proc}/knowhow.md   (if not yet mentioned)
    internal static void AppendFocusStealerKnowhow(IntPtr rootHwnd, string action)
    {
        try
        {
            // Resolve process name + class name from the target hwnd
            NativeMethods.GetWindowThreadProcessId(rootHwnd, out uint pid);
            var procName = "unknown";
            var className = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            try
            {
                var buf = new System.Text.StringBuilder(256);
                NativeMethods.GetClassNameW(rootHwnd, buf, 256);
                className = buf.ToString();
            }
            catch { }

            var safeProc  = SanitizePathTokenForExp(procName);
            var safeClass = SanitizePathTokenForExp(className);
            var expDir    = Path.Combine(DataDir, "experience", safeProc, safeClass);
            Directory.CreateDirectory(expDir);

            var date    = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var note    = $"\n## FocusStealer @ {date}\n" +
                          $"- Action `{action}` on [{className}] ({procName}, hwnd=0x{rootHwnd:X}) stole focus despite nominally being focusless.\n" +
                          $"- Next invocation will force yield popup (Win32 prop `WKAppBot_FocusStealer-{action}` stamped).\n" +
                          $"- If this is always the case, prefer wrapping with `--yield` or switching to SendInput approach.\n";

            // Window-class node: knowhow-{action}.md
            var actionKnowhowPath = Path.Combine(expDir, $"knowhow-{action}.md");
            File.AppendAllText(actionKnowhowPath, note);

            // App-level node: experience/{proc}/knowhow.md — add section only once per run
            var appKnowhowPath = Path.Combine(DataDir, "experience", safeProc, "knowhow.md");
            var appNote = $"\n## FocusStealer ({action}) @ {date}\n" +
                          $"- [{className}] `{action}` steals focus. See: {actionKnowhowPath}\n";
            File.AppendAllText(appKnowhowPath, appNote);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"  [FOCUSSTEALER] Knowhow recorded → {actionKnowhowPath}");
            Console.ResetColor();
        }
        catch { /* best effort */ }
    }

    static bool A11yNotYet(string action)
    {
        Console.Error.WriteLine($"[A11Y] ERROR: '{action}' is not a valid action");
        Console.Error.WriteLine("[A11Y] Window: close, minimize, maximize, restore, move, resize, focus");
        Console.Error.WriteLine("[A11Y] Element: read, find, highlight, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range");
        Console.Error.WriteLine("[A11Y] Async: wait, eval");
        return false;
    }

    // ═══ Window-Level Actions (see A11yElementActions.cs for element-level) ═══

    static bool A11yFocus(IntPtr hwnd, string tag)
    {
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9);
        NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
        NativeMethods.BringWindowToTop(hwnd);
        Console.WriteLine($"[A11Y] focus {tag} — SetForegroundWindow");
        return true;
    }

    static bool A11yHotkey(IntPtr hwnd, string tag, string keyCombo)
    {
        // Ensure target window has focus (SendInput requires foreground)
        if (NativeMethods.IsIconic(hwnd))
            NativeMethods.ShowWindow(hwnd, 9); // SW_RESTORE
        NativeMethods.SmartSetForegroundWindow(hwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
        NativeMethods.BringWindowToTop(hwnd);
        Thread.Sleep(100); // let focus settle

        try
        {
            // Windows menu label: "새 탭\tCtrl+T" or "Save\tCtrl+S" → strip prefix before \t
            var tab = keyCombo.LastIndexOf('\t');
            if (tab >= 0) keyCombo = keyCombo[(tab + 1)..].Trim();

            // +/- notation → SendKeys (e.g., "+Shift h e l l o -Shift")
            // Otherwise legacy Ctrl+S notation
            if (keyCombo.Contains(" +") || keyCombo.Contains(" -") || keyCombo.StartsWith("+") || keyCombo.StartsWith("-"))
            {
                // Pass hwnd for per-token mid-input focus check+restore
                WKAppBot.Win32.Input.KeyboardInput.SendKeys(keyCombo, hwnd);
            }
            else
            {
                var keys = keyCombo.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (keys.Length == 1)
                    WKAppBot.Win32.Input.KeyboardInput.PressKey(keys[0]);
                else
                    WKAppBot.Win32.Input.KeyboardInput.Hotkey(keys);
            }

            Console.WriteLine($"[A11Y] hotkey {tag} — \"{keyCombo}\"");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] hotkey failed: {ex.Message}");
            return false;
        }
    }

    static bool A11yClose(UIA3Automation automation, IntPtr hwnd, string tag, bool force, InputReadiness readiness)
    {
        try
        {
            var el = automation.FromHandle(hwnd);
            if (el.Patterns.Window.IsSupported)
            {
                el.Patterns.Window.Pattern.Close();
                Thread.Sleep(500);
                if (!NativeMethods.IsWindow(hwnd))
                {
                    Console.WriteLine($"[A11Y] close {tag} — UIA WindowPattern");
                    return true;
                }
                // Window still alive — save dialog may have appeared (WinUI internal modal)
                Console.WriteLine($"[A11Y] close {tag} — UIA Close sent but window still alive, checking internal modal...");
                if (HasUiaInternalModal(automation, hwnd, out var modalButtonName))
                {
                    if (force)
                    {
                        DismissUiaInternalModal(automation, hwnd, tag, modalButtonName);
                        Thread.Sleep(500);
                        if (!NativeMethods.IsWindow(hwnd))
                        {
                            Console.WriteLine($"[A11Y] close {tag} — closed after UIA modal dismiss");
                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[A11Y] close {tag} — save dialog detected! use --force to dismiss without saving");
                        return false;
                    }
                }
                // Fall through to Win32 WM_CLOSE tier
            }
            Console.WriteLine($"[A11Y] close {tag} — UIA not sufficient, trying Win32");
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
            // UIA internal modal detection (WinUI/WPF save dialogs — no separate Win32 popup)
            if (HasUiaInternalModal(automation, hwnd, out var wmModalBtn))
            {
                if (force)
                {
                    DismissUiaInternalModal(automation, hwnd, tag, wmModalBtn);
                    Thread.Sleep(500);
                    if (!NativeMethods.IsWindow(hwnd))
                    {
                        Console.WriteLine($"[A11Y] close {tag} — closed after UIA modal dismiss");
                        return true;
                    }
                    // Retry WM_CLOSE after dismiss
                    NativeMethods.SendMessageTimeoutW(hwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 3000, out _);
                    Thread.Sleep(500);
                    if (!NativeMethods.IsWindow(hwnd))
                    {
                        Console.WriteLine($"[A11Y] close {tag} — closed after retry WM_CLOSE");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"[A11Y] close {tag} — save dialog detected! use --force to dismiss without saving");
                    return false;
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

    /// <summary>Known dismiss button names for save dialogs (priority-ordered: don't-save first).</summary>
    static readonly string[] SaveDialogDismissNames = [
        "저장하지 않음", "Don't Save", "Don\u2019t Save",
        "아니오", "No",
        "취소", "Cancel",
        "닫기", "Close",
    ];

    /// <summary>
    /// Detect UIA internal modal dialog (WinUI/WPF save dialogs — no separate Win32 popup).
    /// Returns true if a dismiss-able button was found, with the button name in <paramref name="buttonName"/>.
    /// Does NOT click the button — caller decides based on --force flag.
    /// </summary>
    static bool HasUiaInternalModal(UIA3Automation automation, IntPtr hwnd, out string buttonName)
    {
        buttonName = "";
        try
        {
            var root = automation.FromHandle(hwnd);
            if (root == null) return false;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = root.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null || buttons.Length == 0) return false;

            foreach (var dismissName in SaveDialogDismissNames)
            {
                foreach (var btn in buttons)
                {
                    try
                    {
                        var name = btn.Name;
                        if (!string.IsNullOrEmpty(name) && name.Equals(dismissName, StringComparison.OrdinalIgnoreCase))
                        {
                            buttonName = name;
                            return true;
                        }
                    }
                    catch { }
                }
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Click the dismiss button in a UIA internal modal dialog.
    /// Called only when --force is specified.
    /// </summary>
    static void DismissUiaInternalModal(UIA3Automation automation, IntPtr hwnd, string tag, string buttonName)
    {
        try
        {
            var root = automation.FromHandle(hwnd);
            if (root == null) return;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = root.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null) return;

            foreach (var btn in buttons)
            {
                try
                {
                    var name = btn.Name;
                    if (!string.IsNullOrEmpty(name) && name.Equals(buttonName, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"[A11Y] close {tag} — UIA internal modal: clicking \"{name}\" (--force)");
                        if (btn.Patterns.Invoke.IsSupported)
                            btn.Patterns.Invoke.Pattern.Invoke();
                        else
                            btn.Patterns.LegacyIAccessible.Pattern.DoDefaultAction();
                        return;
                    }
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] close {tag} — UIA modal dismiss failed: {ex.Message}");
        }
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

    // Window hierarchy ancestors: root windows that own/parent our process's windows.
    // Catches hosts like VS Code that aren't in the process tree but contain our window.
    static readonly HashSet<IntPtr> _cachedWindowAncestors = BuildWindowHierarchyAncestors();

    static HashSet<IntPtr> BuildWindowHierarchyAncestors()
    {
        var ancestors = new HashSet<IntPtr>();
        try
        {
            uint myPid = (uint)Environment.ProcessId;
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != myPid) return true;
                // Walk owner + parent chain from our window up to root
                var cur = hWnd;
                for (int i = 0; i < 20 && cur != IntPtr.Zero; i++)
                {
                    var owner = NativeMethods.GetWindow(cur, 4u); // GW_OWNER = 4
                    var parent = NativeMethods.GetParent(cur);
                    var next = owner != IntPtr.Zero ? owner : parent;
                    if (next == IntPtr.Zero) break;
                    NativeMethods.GetWindowThreadProcessId(next, out uint nextPid);
                    if (nextPid != myPid) ancestors.Add(NativeMethods.GetAncestor(next, NativeMethods.GA_ROOT));
                    cur = next;
                }
                return true;
            }, IntPtr.Zero);
        }
        catch { }
        return ancestors;
    }

    static HashSet<IntPtr> GetWindowHierarchyAncestors() => _cachedWindowAncestors;

    // Cached at startup — parent processes may exit mid-run, breaking GetParentPid chain.
    // Capture the full chain NOW while all ancestors are still alive.
    static readonly HashSet<int> _cachedAncestorPids = BuildAncestorPids();

    static HashSet<int> BuildAncestorPids()
    {
        var pids = new HashSet<int>();
        try
        {
            var proc = System.Diagnostics.Process.GetCurrentProcess();
            pids.Add(proc.Id);
            int pid = proc.Id;
            for (int i = 0; i < 20 && pid > 0; i++) // 20 levels: covers Code.exe → shell → wkappbot
            {
                int ppid = GetParentPid(pid);
                if (ppid <= 0 || !pids.Add(ppid)) break;
                pid = ppid;
            }
        }
        catch { }
        return pids;
    }

    static HashSet<int> GetSelfAndAncestorPids() => _cachedAncestorPids;

    // ═══ Kill-by-Pattern: close --kill ═══
    // Kills processes matching grap pattern. No window needed.
    // Search key per process: "[pid]processName.exe" — substring match by default (no * needed).
    // Supports:
    //   "wkappbot-core"            → any process named wkappbot-core (substring)
    //   "node/wkappbot-core"       → wkappbot-core whose direct parent is node
    //   "flutter/node/wkappbot-core" → chain: flutter→node→wkappbot-core
    //   "**/wkappbot-core"         → wkappbot-core with any ancestor (any depth)
    //   "wkappbot-core#regex:lucy" → '#' splits name#exePathFilter
    // If process has a window → WM_CLOSE first then Kill; else → Kill directly.
    static int A11yKillByPattern(string grap, bool allowAncestors)
    {
        // Split '#' for optional exe-path sub-filter
        string namePattern = grap;
        string? exeFilter = null;
        var hashIdx = grap.IndexOf('#');
        if (hashIdx >= 0)
        {
            namePattern = grap[..hashIdx].Trim();
            exeFilter = grap[(hashIdx + 1)..].Trim();
            if (string.IsNullOrEmpty(exeFilter)) exeFilter = null;
        }

        // Split '/' for parent/child chain: last segment = target, rest = ancestors (outermost first)
        var segments = namePattern.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string targetPattern = segments.Length > 0 ? segments[^1] : namePattern;
        string[] ancestorPatterns = segments.Length > 1 ? segments[..^1] : Array.Empty<string>();

        var targetMatcher = PatternMatcher.Create(targetPattern);
        var exeMatcher = exeFilter != null ? PatternMatcher.Create(exeFilter) : null;

        var allProcs = System.Diagnostics.Process.GetProcesses();
        // PID→processName lookup for ancestor chain resolution
        var pidToName = allProcs.GroupBy(p => p.Id)
            .ToDictionary(g => g.Key, g => g.First().ProcessName);

        var selfPids = allowAncestors ? new HashSet<int>() : GetSelfAndAncestorPids();
        var killed = new List<string>();
        var skipped = new List<string>();

        foreach (var p in allProcs)
        {
            try
            {
                var procName = p.ProcessName;
                string exePath = "";
                try { exePath = p.MainModule?.FileName ?? ""; } catch { }

                // Search key: "[pid]processName.exe" — substring by default
                var nodeKey = $"[{p.Id}]{procName}.exe";

                if (!targetMatcher.IsMatch(nodeKey) && !targetMatcher.IsMatch(procName))
                    continue;

                if (exeMatcher != null && !exeMatcher.IsMatch(string.IsNullOrEmpty(exePath) ? nodeKey : exePath))
                    continue;

                if (ancestorPatterns.Length > 0 && !KillMatchesAncestorChain(p.Id, ancestorPatterns, pidToName))
                    continue;

                if (selfPids.Contains(p.Id))
                {
                    skipped.Add($"{nodeKey} (ancestor-protected)");
                    continue;
                }

                // Protect MCP launcher processes (long-lived stdio relay, survives hot-swap)
                if (IsMcpLauncherProcess(p.Id))
                {
                    skipped.Add($"{nodeKey} (mcp-launcher-protected)");
                    continue;
                }

                var mainHwnd = p.MainWindowHandle;
                bool hasWindow = mainHwnd != IntPtr.Zero && NativeMethods.IsWindow(mainHwnd);
                if (hasWindow)
                {
                    Console.WriteLine($"[KILL] {nodeKey} — window found, sending WM_CLOSE");
                    NativeMethods.SendMessageTimeoutW(mainHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero, 0x0002, 2000, out _);
                    Thread.Sleep(800);
                    try { p.Refresh(); } catch { }
                    if (p.HasExited)
                    {
                        Console.WriteLine($"[KILL] {nodeKey} — exited gracefully");
                        killed.Add(nodeKey);
                        continue;
                    }
                    Console.WriteLine($"[KILL] {nodeKey} — still alive, force kill");
                }
                else
                {
                    Console.WriteLine($"[KILL] {nodeKey} — no window, force kill");
                }
                p.Kill(entireProcessTree: false);
                killed.Add(nodeKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KILL] pid={p.Id} failed: {ex.Message}");
            }
        }

        if (skipped.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var s in skipped)
                Console.WriteLine($"[GUARD] skipped {s} — use --allow-ancestors to override");
            Console.ResetColor();
        }

        if (killed.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[KILL] no matching processes for \"{grap}\"");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[KILL] killed {killed.Count}: {string.Join(", ", killed)}");
        Console.ResetColor();
        return 0;
    }

    // Returns true if the process is an MCP launcher (wkappbot.exe mcp --launcher).
    // These are long-lived stdio relays that must survive hot-swap.
    static bool IsMcpLauncherProcess(int pid)
    {
        try
        {
            var cmdLine = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(pid);
            if (string.IsNullOrEmpty(cmdLine)) return false;
            return cmdLine.Contains(" mcp ", StringComparison.OrdinalIgnoreCase) ||
                   cmdLine.EndsWith(" mcp", StringComparison.OrdinalIgnoreCase) ||
                   cmdLine.Contains("--launcher", StringComparison.OrdinalIgnoreCase);
        }
        catch { return false; }
    }

    // Ancestor chain matching for --kill.
    // ancestorPatterns: outermost first, e.g. ["flutter","node"] for flutter/node/target
    // Builds actual ancestor list innermost-first and matches reversed patterns.
    // "**" in patterns = skip any number of ancestor levels (multi-level wildcard).
    static bool KillMatchesAncestorChain(int childPid, string[] ancestorPatterns, Dictionary<int, string> pidToName)
    {
        var ancestors = new List<string>();
        int pid = childPid;
        for (int i = 0; i < 20; i++)
        {
            int ppid = GetParentPid(pid);
            if (ppid <= 0 || ppid == pid) break;
            if (!pidToName.TryGetValue(ppid, out var name)) break;
            ancestors.Add(name);
            pid = ppid;
        }
        // Reverse patterns so innermost = first to match
        return KillMatchChainRec(ancestorPatterns.Reverse().ToArray(), 0, ancestors, 0);
    }

    static bool KillMatchChainRec(string[] patterns, int pi, List<string> ancestors, int ai)
    {
        if (pi >= patterns.Length) return true;
        if (ai > ancestors.Count) return false;
        var pat = patterns[pi];
        if (pat == "**")
        {
            // Try matching remaining patterns at every ancestor depth
            for (int skip = 0; skip + ai <= ancestors.Count; skip++)
                if (KillMatchChainRec(patterns, pi + 1, ancestors, ai + skip)) return true;
            return false;
        }
        if (ai >= ancestors.Count) return false;
        var m = PatternMatcher.Create(pat);
        if (!m.IsMatch(ancestors[ai]) && !m.IsMatch($"[0]{ancestors[ai]}.exe")) return false;
        return KillMatchChainRec(patterns, pi + 1, ancestors, ai + 1);
    }

    // ═══ Wait Action: poll for window/element appearance ═══

    static int A11yWaitAction(string grap, int timeoutMs, int intervalMs)
    {
        // Parse extra wait options from grap args context
        // These are set by A11yCommand before calling: _waitCondition, _waitNot, _waitCdp
        var condition = _waitCondition;
        var waitNot = _waitNot;

        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grap);
        if (win32Segments.Length == 0)
            return Error("No window title in grap pattern");

        var firstSegPatterns = win32Segments[0]
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        bool needsUia = !string.IsNullOrEmpty(uiaPath);
        var modeStr = waitNot ? "disappear" : "appear";
        var condStr = condition != null ? $", condition=\"{condition}\"" : "";
        Console.WriteLine($"[A11Y] wait — polling for \"{grap}\" to {modeStr} (timeout={timeoutMs}ms, interval={intervalMs}ms{condStr})");

        // CDP web wait: if grap contains CSS selector patterns
        bool isCdpWait = grap.Contains("#") && firstSegPatterns.Any(p =>
            p.Contains("*Chrome*", StringComparison.OrdinalIgnoreCase) ||
            p.Contains("*Edge*", StringComparison.OrdinalIgnoreCase));

        using var automation = needsUia && !isCdpWait ? new UIA3Automation() : null;
        if (automation != null)
        {
            automation.ConnectionTimeout = TimeSpan.FromSeconds(2);
            automation.TransactionTimeout = TimeSpan.FromSeconds(2);
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();
        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            bool found = false;
            string foundInfo = "";

            foreach (var pat in firstSegPatterns)
            {
                var wins = WindowFinder.FindByTitle(pat);
                if (wins.Count == 0) continue;

                var hwnd = wins[0].Handle;

                // Walk Win32 children
                bool childOk = true;
                for (int seg = 1; seg < win32Segments.Length; seg++)
                {
                    var child = WindowFinder.FindChildByPattern(hwnd, win32Segments[seg]);
                    if (child == null) { childOk = false; break; }
                    hwnd = child.Handle;
                }
                if (!childOk) continue;

                // Window-only wait (no UIA scope)
                if (!needsUia)
                {
                    found = true;
                    foundInfo = $"\"{wins[0].Title}\" (hwnd={hwnd:X8})";
                    break;
                }

                // UIA scope wait
                try
                {
                    var root = automation!.FromHandle(hwnd);
                    var scoped = GrapHelper.FindUiaScope(root, uiaPath!);
                    if (scoped != null)
                    {
                        var name = scoped.Properties.Name.ValueOrDefault ?? "";
                        var type = "?";
                        try { type = scoped.Properties.ControlType.ValueOrDefault.ToString(); } catch { }

                        // Condition check: text/name must contain condition string
                        if (condition != null)
                        {
                            var text = name;
                            try { text = scoped.Patterns.Value.PatternOrDefault?.Value?.ToString() ?? name; } catch { }
                            if (!text.Contains(condition, StringComparison.OrdinalIgnoreCase))
                                continue; // condition not met, keep polling
                        }

                        found = true;
                        foundInfo = $"[{type}] \"{name}\"";
                        break;
                    }
                }
                catch { /* UIA timeout on stale elements, keep polling */ }
            }

            // --not mode: wait until element DISAPPEARS
            if (waitNot)
            {
                if (!found)
                {
                    Console.WriteLine($"[A11Y] wait --not — element gone after {sw.ElapsedMilliseconds}ms");
                    return 0;
                }
            }
            else
            {
                if (found)
                {
                    Console.WriteLine($"[A11Y] wait — found after {sw.ElapsedMilliseconds}ms: {foundInfo}");
                    return 0;
                }
            }

            Thread.Sleep(intervalMs);
        }

        Console.Error.WriteLine($"[A11Y] wait — timeout after {timeoutMs}ms: \"{grap}\" ({modeStr})");
        return 1;
    }

    // Wait options (set by A11yCommand before calling A11yWaitAction)
    [ThreadStatic] static string? _waitCondition;
    [ThreadStatic] static bool _waitNot;

    // ═══ Eval Action: execute JavaScript via CDP ═══

    /// <summary>Deprecated wrapper — warns about eval removal, suggests --eval-js.</summary>
    static bool DeprecatedEval(IntPtr hwnd, string tag, string jsExpression, string? tabHint)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Error.WriteLine("[DEPRECATED] 'a11y eval' will be removed — target is ambiguous.");
        Console.Error.WriteLine("  Use --eval-js with any a11y action:");
        Console.Error.WriteLine("    a11y read  \"*Chrome*#chatgpt.com\" --eval-js \"document.title\"");
        Console.Error.WriteLine("    a11y click \"*Chrome*#gemini.google.com/button.send\" --eval-js \"el.disabled=false\"");
        Console.Error.WriteLine("  Available CDP helpers (via CdpClient):");
        Console.Error.WriteLine("    GetUrlAsync, GetTitleAsync, FocusAsync, GetTextLengthAsync,");
        Console.Error.WriteLine("    IsHiddenAsync, GetTabStateAsync, QueryCountAsync, JsClickAsync");
        Console.ResetColor();
        return A11yEvalJs(hwnd, tag, jsExpression, tabHint);
    }

    static bool A11yEvalJs(IntPtr hwnd, string tag, string jsExpression, string? tabHint)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var port = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)pid);
            if (port <= 0)
            {
                Console.Error.WriteLine($"[A11Y] eval — no CDP port found for {tag}");
                return false;
            }

            using var cdp = new WKAppBot.WebBot.CdpClient();
            cdp.ConnectAsync(port).GetAwaiter().GetResult();

            // Tab matching: prefer #scope hint, fallback to window title
            var windowTitle = WindowFinder.GetWindowText(hwnd);
            var tabMatch = !string.IsNullOrEmpty(tabHint) ? tabHint : windowTitle;
            if (!string.IsNullOrEmpty(tabMatch))
            {
                var tabs = cdp.ListTabsAsync(port).GetAwaiter().GetResult();
                foreach (var tab in tabs)
                {
                    if (tab.Title.Contains(tabMatch, StringComparison.OrdinalIgnoreCase) ||
                        tabMatch.Contains(tab.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        if (tab.Id != cdp.TargetId)
                            cdp.SwitchToTargetAsync(tab.Id, port).GetAwaiter().GetResult();
                        break;
                    }
                }
            }

            var result = cdp.EvalAsync(jsExpression).GetAwaiter().GetResult();
            Console.WriteLine($"[A11Y] eval result: {result}");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] eval — error: {ex.Message}");
            return false;
        }
    }

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
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "wkappbot",
                            Arguments = $"speak \"{txt.Replace("\"", "'")}\" --bg",
                            UseShellExecute = false,
                            CreateNoWindow = true
                        });
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
    // Solves the "byte offset splice nightmare" when editing CP949 files via MCP.
    // file-read: reads file as target encoding → outputs Unicode to stdout
    // file-write: receives Unicode text (inline or from @file) → saves as target encoding
    //
    // Usage:
    //   a11y file-read "path/to/file.cpp" --encoding 949
    //   a11y file-write "path/to/file.cpp" --text "@tmp_edit.txt" --encoding 949
    //   a11y file-write "path/to/file.cpp" --text "inline content" --encoding 949
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
        // text: inline content OR "@filename" (curl-style reference)
        // Also allow positional: a11y file-write "target.cpp" "@tmp.txt" --encoding 949
        if (text == null && args.Length >= 3 && !args[2].StartsWith("--"))
            text = args[2];
        if (text == null)
            return Error("file-write requires --text \"content\" or --text \"@source.txt\"");

        string content;
        if (text.StartsWith("@"))
        {
            // @filename: read UTF-8 source (Claude writes temp file in UTF-8)
            var srcPath = text[1..];
            if (!File.Exists(srcPath))
                return Error($"Source file not found: {srcPath}");
            content = File.ReadAllText(srcPath, System.Text.Encoding.UTF8);
            Console.WriteLine($"[FILE-WRITE] source: {srcPath} ({content.Length} chars)");
        }
        else
        {
            content = text;
        }

        var targetDir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);

        AgentFileTracker.Track(filePath); // capture original before overwrite
        var encoded = enc.GetBytes(content);
        File.WriteAllBytes(filePath, encoded);
        Console.WriteLine($"[FILE-WRITE] {filePath} ({enc.WebName}, {content.Length} chars → {encoded.Length} bytes) ✓");
        return 0;
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
