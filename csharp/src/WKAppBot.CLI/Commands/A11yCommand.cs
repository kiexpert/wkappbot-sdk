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
        // --help/--regression interceptor: MCP worker calls A11yCommand directly (bypasses Program.cs)
        if (args.Any(a => a is "--help" or "-h")) { TryPrintCommandHelp("a11y", args); return 0; }
        if (args.Any(a => a is "--regression")) { TryRunRegression("a11y", args); return 0; }

        // ═══ Focus Hot-Chain (lightweight — Win32 only, no FlaUI/UIA DLL loading) ═══
        try
        {
            var fg = NativeMethods.GetForegroundWindow();
            var fgTitle = NativeMethods.GetWindowTextW(fg);
            var gti = new NativeMethods.GUITHREADINFO
                { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            if (NativeMethods.GetGUIThreadInfo(0, ref gti) && gti.hwndFocus != IntPtr.Zero)
            {
                var focusTitle = NativeMethods.GetWindowTextW(gti.hwndFocus);
                var focusClass = new System.Text.StringBuilder(128);
                NativeMethods.GetClassNameW(gti.hwndFocus, focusClass, 128);
                var winShort = fgTitle.Length > 30 ? fgTitle[..27] + "*" : fgTitle;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[FOCUS] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{winShort}\"");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"#0x{gti.hwndFocus:X}({focusClass})");
                if (focusTitle.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($" \"{(focusTitle.Length > 30 ? focusTitle[..27] + "..." : focusTitle)}\"");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        catch { }

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
            Console.WriteLine("  --nth 1,3~  Union: #1 plus #3 onward");
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

            // Text only → CF_UNICODETEXT (+ CF_HTML if --html)
            bool asHtml = args.Contains("--html");
            return ClipboardWrite(string.Join(Environment.NewLine, textParts), asHtml);
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
        {
            bool dryRun = args.Any(a => a == "--dry-run");
            string? argFilter = GetArgValue(args, "--arg"); // cmdline substring filter
            return A11yKillByPattern(grap, allowAncestors, dryRun, argFilter);
        }

        var elementActions = new HashSet<string> {
            "invoke", "click", "toggle", "expand", "collapse", "select",
            "scroll", "type", "set-value", "set-range", "read",
            "highlight", "find"
        };
        bool isElementAction = elementActions.Contains(action);

        // Read-only actions (inspect/find/windows/screenshot/ocr/read/highlight/wait) are exempt.
        bool isInteractiveAction = action is
            "close" or "invoke" or "click" or "toggle" or "expand" or "collapse" or
            "select" or "scroll" or "type" or "set-value" or "set-range" or
            "minimize" or "maximize" or "restore" or "focus" or "move" or "resize";

        // ═══ STEP 1: Split grap ═══
        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grap);
        if (win32Segments.Length == 0)
            return Error("No window title in grap pattern");

        // ═══ STEP 1.5: Detect vague pattern (no hwnd/pid/automationId) ═══
        // Specific patterns: {hwnd:...}, {pid:...}, [XXXXXXXX], hwnd:0x..., hwnd=...
        // Vague patterns: "*Chrome*", "계산기", "*메모장*" — need full scan for ambiguity check
        bool isSpecificPattern = grap.Contains("hwnd", StringComparison.OrdinalIgnoreCase)
            || grap.Contains("pid:", StringComparison.OrdinalIgnoreCase)
            || grap.Contains("pid=", StringComparison.OrdinalIgnoreCase)
            || (grap.StartsWith("[") && grap.EndsWith("]") && grap.Length <= 12)
            || (grap.StartsWith("{") && grap.Contains(":"));

        // ═══ STEP 2: Find all matching windows ═══
        var firstSegPatterns = win32Segments[0]
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var allWindows = new List<WindowInfo>();
        var seen = new HashSet<IntPtr>();
        // Vague patterns: always full scan to detect ambiguity
        bool stopOnFirstWindowMatch = isSpecificPattern && isInteractiveAction && !all && nthRaw == null;
        foreach (var pat in firstSegPatterns)
        {
            foreach (var w in WindowFinder.FindByTitle(pat, stopOnFirstWindowMatch))
                if (seen.Add(w.Handle))
                    allWindows.Add(w);
            if (stopOnFirstWindowMatch && allWindows.Count > 0)
                break;
        }

        // ── Ancestor process guard: skip windows owned by own process tree ──
        // Applied to ALL interactive actions (not just close) unless --allow-ancestors is set.
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

        // ═══ STEP 2.5: Vague pattern + multiple matches → auto-find ═══
        // When pattern has no specific target (hwnd/pid) and matches multiple windows,
        // show all matches with precise JSON5 grap patterns instead of guessing.
        if (!isSpecificPattern && allWindows.Count > 1 && !all && nthRaw == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[A11Y] \"{grap}\" -> {allWindows.Count}개 매칭 -> find 자동 전환. 정확한 타겟을 지정하세요:");
            Console.ResetColor();

            // Get focused UIA element info (applies to foreground window only)
            FocusedElementInfo? focusInfo = null;
            try { using var uiaLoc = new UiaLocator(); focusInfo = uiaLoc.GetFocusedElementInfo(); }
            catch { }

            for (int idx = 0; idx < allWindows.Count; idx++)
            {
                var w = allWindows[idx];
                var r = w.Rect;
                NativeMethods.GetWindowThreadProcessId(w.Handle, out uint wpid);
                string proc;
                try { proc = System.Diagnostics.Process.GetProcessById((int)wpid).ProcessName; }
                catch { proc = "?"; }

                // Truncate title for display (no ellipsis — keeps pattern-matchable)
                var title = w.Title.Length > 50 ? w.Title[..50] : w.Title;

                // Summary line: index, proc, class, title, size
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  {idx + 1}. ");
                Console.ResetColor();
                Console.Write($"{proc} ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[{w.ClassName}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{title}\"");
                Console.ResetColor();
                Console.WriteLine($" ({r.Right - r.Left}x{r.Bottom - r.Top})");

                // Focused UIA node (only for the window that owns the focus)
                string? focusScope = null;
                if (focusInfo != null && focusInfo.ProcessId == (int)wpid)
                {
                    var fId = focusInfo.AutomationId != "" ? focusInfo.AutomationId : focusInfo.Name;
                    if (fId.Length > 40) fId = fId[..40];
                    focusScope = fId;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"     [FOCUS] {focusInfo.ControlType}(\"{fId}\"){(focusInfo.Patterns.Count > 0 ? " " + string.Join(",", focusInfo.Patterns) : "")}");
                    Console.ResetColor();
                }

                // Precise grap command (append #focusScope for combined target)
                var json5 = WindowFinder.BuildTargetJson5(w.Handle);
                var fullTarget = focusScope != null ? $"{json5}#*{focusScope}*" : json5;

                // Verify: re-search with the generated pattern to confirm it actually works
                var verifyHits = WindowFinder.FindByTitle(json5, true);
                var verified = verifyHits.Any(v => v.Handle == w.Handle);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"     a11y {action} \"{fullTarget}\"");
                Console.ForegroundColor = verified ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(verified ? " [OK]" : " [MISS]");
                Console.ResetColor();
                if (!verified)
                {
                    // Auto-heal: generate a simpler hwnd-only fallback pattern
                    var healPattern = $"*hwnd={w.Handle:X8}*";
                    var healHits = WindowFinder.FindByTitle(healPattern, true);
                    var healed = healHits.Any(v => v.Handle == w.Handle);
                    if (healed)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"     [HEAL] hwnd fallback: a11y {action} \"{healPattern}\"");
                        Console.ResetColor();
                    }
                    Program.AutoRegisterBug(
                        $"[BUG-AUTO] auto-find grap verify MISS: pattern={json5} hwnd=0x{w.Handle:X8} title=\"{title}\" healed={healed}",
                        args: ["a11y", "find", json5]);
                }
            }
            Console.WriteLine($"[A11Y] Tip: a11y find \"<target>\" 으로 상세 탐색 / --all 또는 --nth 1 으로 강제 실행");
            return 1;
        }

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
            if (isInteractiveAction && targets.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[A11Y] WARN: --nth {nthRaw} matched {targets.Count} candidates; acting on highest-priority one only");
                Console.ResetColor();
                targets = [targets[0]];
            }
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

        // ── JSON5 TARGET: usable as grap pattern in any command ──
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (var t in targets)
            Console.WriteLine($"[A11Y] TARGET: {WindowFinder.BuildTargetJson5(t.Handle)}");
        Console.ResetColor();

        // ═══ STEP 4.5: Focused leaf node → target suggestion (all actions) ═══
        // Check for modal dialog blocking the target window first
        if (!NativeMethods.IsWindowEnabled(targets[0].Handle))
        {
            // Window is disabled → likely a modal dialog on top
            var ownedPopup = NativeMethods.GetWindow(targets[0].Handle, 6 /* GW_ENABLEDPOPUP */);
            if (ownedPopup != IntPtr.Zero && ownedPopup != targets[0].Handle)
            {
                var popTitle = WindowFinder.GetWindowText(ownedPopup);
                var popJson5 = WindowFinder.BuildTargetJson5(ownedPopup);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[A11Y] ALERT: 모달 다이얼로그가 타겟을 차단 중 → \"{popTitle}\"");
                Console.ResetColor();
                // Auto-find inside the alert dialog
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[A11Y] ALERT -> find 자동 전환. 알러트 내부 요소:");
                Console.ResetColor();
                try
                {
                    using var alertAuto = new UIA3Automation();
                    var alertRoot = alertAuto.FromHandle(ownedPopup);
                    var alertChildren = alertRoot.FindAllDescendants();
                    int shown = 0;
                    foreach (var ac in alertChildren)
                    {
                        if (shown >= 15) { Console.WriteLine($"     ... (+{alertChildren.Length - shown}개 더)"); break; }
                        var acType = "?"; try { acType = ac.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var acName = ac.Properties.Name.ValueOrDefault ?? "";
                        var acAid = ac.Properties.AutomationId.ValueOrDefault ?? "";
                        if (acName.Length > 40) acName = acName[..40];
                        var acLabel = !string.IsNullOrEmpty(acAid) ? acAid : acName;
                        if (string.IsNullOrEmpty(acLabel)) { shown++; continue; }
                        // Highlight actionable elements (Button, Hyperlink, CheckBox)
                        bool actionable = acType is "Button" or "Hyperlink" or "CheckBox" or "RadioButton" or "MenuItem";
                        Console.ForegroundColor = actionable ? ConsoleColor.Cyan : ConsoleColor.DarkGreen;
                        Console.Write(actionable ? "  >> " : "     ");
                        Console.Write($"{acType}(\"{acLabel}\")");
                        if (actionable)
                            Console.Write($" -> a11y invoke \"{popJson5}#*{acLabel}*\"");
                        Console.WriteLine();
                        Console.ResetColor();
                        shown++;
                    }
                }
                catch { }
                // Also show Win32 children (MFC dialogs often have no UIA)
                var alertKids = WindowFinder.GetChildrenZOrder(ownedPopup);
                if (alertKids.Count > 0)
                {
                    int btnCount = 0;
                    foreach (var ak in alertKids)
                    {
                        var akCls = WindowFinder.GetClassName(ak.Handle);
                        var akTitle = ak.Title;
                        if (akCls != "Button" || string.IsNullOrEmpty(akTitle)) continue;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"  >> [Button] \"{akTitle}\" -> a11y click \"{popJson5}#*{akTitle}*\"");
                        Console.ResetColor();
                        btnCount++;
                    }
                }
            }
        }
        // Find focused element WITHIN the target window (not OS-global focus)
        try
        {
            using var uiaFocAuto = new UIA3Automation();
            var targetRoot = uiaFocAuto.FromHandle(targets[0].Handle);
            // Strategy 1: OS focused element if it belongs to this window's process
            FocusedElementInfo? focLeaf = null;
            using var uiaFocLoc = new UiaLocator();
            var osFocus = uiaFocLoc.GetFocusedElementInfo();
            NativeMethods.GetWindowThreadProcessId(targets[0].Handle, out uint focWinPid);
            if (osFocus != null && osFocus.ProcessId == (int)focWinPid)
                focLeaf = osFocus;

            // Strategy 2: Walk target window's UIA tree to find deepest leaf with keyboard focus
            if (focLeaf == null)
            {
                try
                {
                    var walker = uiaFocAuto.TreeWalkerFactory.GetRawViewWalker();
                    AutomationElement? deepest = null;
                    string dType = "", dName = "", dAid = "";
                    var patterns = new List<string>();
                    void WalkForFocus(AutomationElement el, int depth)
                    {
                        if (depth > 15) return;
                        try
                        {
                            var child = walker.GetFirstChild(el);
                            while (child != null)
                            {
                                var name = child.Properties.Name.ValueOrDefault ?? "";
                                var aid = child.Properties.AutomationId.ValueOrDefault ?? "";
                                if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(aid))
                                {
                                    deepest = child;
                                    dType = child.Properties.ControlType.ValueOrDefault.ToString();
                                    dName = name; dAid = aid;
                                    patterns.Clear();
                                    try { if (child.Patterns.Invoke.IsSupported) patterns.Add("Invoke"); } catch { }
                                    try { if (child.Patterns.Value.IsSupported) patterns.Add("Value"); } catch { }
                                    try { if (child.Patterns.Toggle.IsSupported) patterns.Add("Toggle"); } catch { }
                                }
                                bool hasFocus = false;
                                try { hasFocus = child.Patterns.SelectionItem.IsSupported && child.Patterns.SelectionItem.Pattern.IsSelected; } catch { }
                                if (hasFocus || !string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(aid))
                                    WalkForFocus(child, depth + 1);
                                child = walker.GetNextSibling(child);
                            }
                        }
                        catch { }
                    }
                    WalkForFocus(targetRoot, 0);
                    if (deepest != null)
                    {
                        var label = !string.IsNullOrEmpty(dAid) ? dAid : dName;
                        if (label.Length > 40) label = label[..40];
                        var focJson5 = WindowFinder.BuildTargetJson5(targets[0].Handle);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"[A11Y] LEAF: {dType}(\"{label}\")");
                        if (patterns.Count > 0) Console.Write($" [{string.Join(",", patterns)}]");
                        Console.WriteLine();
                        Console.ResetColor();
                        if (!string.IsNullOrEmpty(label))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"[A11Y] LEAF TARGET: a11y {action} \"{focJson5}#*{label}*\"");
                            Console.ResetColor();
                        }
                    }
                }
                catch { }
            }
            else
            {
                // OS focus is in our target window — show leaf + parent chain
                var fLabel = !string.IsNullOrEmpty(focLeaf.AutomationId) ? focLeaf.AutomationId : focLeaf.Name;
                if (fLabel.Length > 40) fLabel = fLabel[..40];
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"[A11Y] FOCUSED: {focLeaf.ControlType}(\"{fLabel}\")");
                if (focLeaf.Patterns.Count > 0) Console.Write($" [{string.Join(",", focLeaf.Patterns)}]");
                Console.WriteLine();
                foreach (var (pType, pName) in focLeaf.ParentChain)
                {
                    if (string.IsNullOrEmpty(pName)) continue;
                    Console.Write($"         <- {pType}(\"{pName}\")");
                    Console.WriteLine();
                }
                Console.ResetColor();
                if (!string.IsNullOrEmpty(fLabel))
                {
                    var focJson5 = WindowFinder.BuildTargetJson5(targets[0].Handle);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[A11Y] FOCUSED TARGET: a11y {action} \"{focJson5}#*{fLabel}*\"");
                    Console.ResetColor();
                }
            }
        }
        catch { /* best effort */ }

        // ═══ STEP 4.9: Elevation auto-detect → Elevated Eye Proxy delegation ═══
        // Pre-check: if target is elevated AND we have #scope, delegate BEFORE UIA traversal
        // (UIPI blocks UIA access to admin windows → 60s timeout without this check)
        NativeMethods.GetWindowThreadProcessId(targets[0].Handle, out uint _targetPid);
        var _targetElevated = NativeMethods.IsProcessElevated(_targetPid);
        if (_targetElevated == true && !NativeMethods.IsCurrentProcessElevated())
        {
            Console.Error.WriteLine($"[A11Y] Target (pid={_targetPid}) is elevated — delegating full command before UIA traversal");
        }
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            targets[0].Handle, "a11y", args);
        if (delegated) return delegateExit;
        // If elevation was needed but delegation failed (MCP/Eye mode), and we have UIA scope,
        // skip UIA traversal entirely (would hang 60s on UIPI block)
        if (_targetElevated == true && !NativeMethods.IsCurrentProcessElevated() && !string.IsNullOrEmpty(uiaPath))
        {
            Console.Error.WriteLine($"[A11Y] UIPI block: cannot traverse UIA on elevated target without admin rights");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[A11Y] Target is admin (pid={_targetPid}) — elevation required for #scope access");
            Console.ResetColor();
            return 1;
        }

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
        PulseStep.Mark("uia-init");
        using var automation = new UIA3Automation();
        PulseStep.Mark("uia-timeout-set");
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);
        var readiness = CreateInputReadiness();
        var aar = CreateActionReadiness(readiness);
        var gapCollector = new OcrGapCollector();

        PulseStep.Mark("uia-ready");

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
                    // Auto-find: list Win32 child windows for MFC/HTS navigation
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[A11Y] \"{win32Segments[i]}\" 자식창 없음 -> find 자동 전환. Win32 자식 윈도우:");
                    Console.ResetColor();
                    var siblings = WindowFinder.GetChildrenZOrder(hwnd);
                    int shown = 0;
                    foreach (var sib in siblings)
                    {
                        if (shown >= 20) { Console.WriteLine($"     ... (+{siblings.Count - shown}개 더)"); break; }
                        var sibCls = WindowFinder.GetClassName(sib.Handle);
                        var sibTitle = sib.Title;
                        if (sibTitle.Length > 40) sibTitle = sibTitle[..40];
                        var sibR = sib.Rect;
                        var sibW = sibR.Right - sibR.Left;
                        var sibH = sibR.Bottom - sibR.Top;
                        var sibLabel = !string.IsNullOrEmpty(sibTitle) ? sibTitle : sibCls;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"     [{sibCls}] ");
                        Console.ResetColor();
                        Console.Write($"\"{sibTitle}\" ({sibW}x{sibH})");
                        if (!string.IsNullOrEmpty(sibLabel))
                        {
                            // Build grap: parent/child pattern
                            var parentPat = string.Join("/", win32Segments.Take(i));
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write($" -> a11y {action} \"{grap.Split('#')[0]}/{(sibTitle != "" ? "*" + sibTitle + "*" : sibCls)}");
                            if (!string.IsNullOrEmpty(uiaPath)) Console.Write($"#{uiaPath}");
                            Console.Write("\"");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                        shown++;
                    }
                    if (siblings.Count == 0)
                        Console.WriteLine("     (자식 윈도우 없음 — UIA 탐색 필요)");
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
                PulseStep.Mark("uia-from-handle");
                try { root = automation.FromHandle(hwnd); }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[A11Y] UIA FromHandle failed for {tag}: {ex.Message}");
                    fail++;
                    continue;
                }
                PulseStep.Mark("uia-from-handle-done");

                // ── Element action without #scope → auto-find: list available UIA nodes ──
                if (string.IsNullOrEmpty(uiaPath) && isInteractiveAction && action is not ("close" or "minimize" or "maximize" or "restore" or "focus" or "move" or "resize"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[A11Y] #scope 없음 -> find 자동 전환. \"{title}\" 의 UIA 요소:");
                    Console.ResetColor();
                    try
                    {
                        var winJson5 = WindowFinder.BuildTargetJson5(hwnd);

                        // Show focused element first (leaf → parent chain)
                        try
                        {
                            using var focLoc = new UiaLocator();
                            var focInfo = focLoc.GetFocusedElementInfo();
                            NativeMethods.GetWindowThreadProcessId(hwnd, out uint winPid2);
                            if (focInfo != null && focInfo.ProcessId == (int)winPid2)
                            {
                                var fLabel = !string.IsNullOrEmpty(focInfo.AutomationId) ? focInfo.AutomationId : focInfo.Name;
                                if (fLabel.Length > 40) fLabel = fLabel[..40];
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write($"  >> [FOCUS] {focInfo.ControlType}(\"{fLabel}\")");
                                if (focInfo.Patterns.Count > 0) Console.Write($" {string.Join(",", focInfo.Patterns)}");
                                Console.WriteLine();
                                // Parent chain (leaf → root)
                                foreach (var (pType, pName) in focInfo.ParentChain)
                                {
                                    if (string.IsNullOrEmpty(pName)) continue;
                                    Console.Write($"       <- {pType}(\"{pName}\")");
                                    Console.WriteLine();
                                }
                                Console.ResetColor();
                                if (!string.IsNullOrEmpty(fLabel))
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine($"     -> a11y {action} \"{winJson5}#*{fLabel}*\"");
                                    Console.ResetColor();
                                }
                            }
                        }
                        catch { }

                        var children = root.FindAllChildren();
                        int shown = 0;
                        foreach (var child in children)
                        {
                            if (shown >= 20) { Console.WriteLine($"     ... (+{children.Length - shown}개 더 — a11y find로 전체 탐색)"); break; }
                            var cType = "?"; try { cType = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                            var cName = child.Properties.Name.ValueOrDefault ?? "";
                            var cAid = child.Properties.AutomationId.ValueOrDefault ?? "";
                            if (cName.Length > 40) cName = cName[..40];
                            var cLabel = !string.IsNullOrEmpty(cAid) ? cAid : cName;
                            if (string.IsNullOrEmpty(cLabel)) { shown++; continue; }
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine($"     {cType}(\"{cLabel}\") -> a11y {action} \"{winJson5}#*{cLabel}*\"");
                            Console.ResetColor();
                            shown++;
                        }
                    }
                    catch { }
                    Console.WriteLine($"[A11Y] Tip: a11y find \"{WindowFinder.BuildTargetJson5(hwnd)}\" --depth 5 로 전체 탐색");
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
                        // Auto-find: show available children so user can pick the right element
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[A11Y] \"{uiaPath}\" 요소 없음 -> find 자동 전환. 사용 가능한 요소:");
                        Console.ResetColor();
                        try
                        {
                            var children = root.FindAllChildren();
                            int shown = 0;
                            foreach (var child in children)
                            {
                                if (shown >= 15) { Console.WriteLine($"     ... (+{children.Length - shown}개)"); break; }
                                var cType = "?"; try { cType = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                                var cName = child.Properties.Name.ValueOrDefault ?? "";
                                var cAid = child.Properties.AutomationId.ValueOrDefault ?? "";
                                if (cName.Length > 40) cName = cName[..40];
                                var cLabel = !string.IsNullOrEmpty(cAid) ? cAid : cName;
                                if (string.IsNullOrEmpty(cLabel)) { shown++; continue; }
                                var winJson5 = WindowFinder.BuildTargetJson5(hwnd);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine($"     {cType}(\"{cLabel}\") -> a11y {action} \"{winJson5}#*{cLabel}*\"");
                                Console.ResetColor();
                                shown++;
                            }
                        }
                        catch { }
                        Console.WriteLine($"[A11Y] Tip: a11y find \"{WindowFinder.BuildTargetJson5(hwnd)}\" --depth 5 로 전체 탐색");
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

                    // ── Dry-run gate: block write actions, allow read/discovery ──
                    if (_dryRunMode.Value && action is not ("highlight" or "find" or "read" or "wait" or "eval" or "clipboard-read"))
                    {
                        var targetDesc = root?.Name ?? grap ?? "?";
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"[DRY-RUN] Would execute: a11y {action} {targetDesc}");
                        Console.ResetColor();
                        success = true; // report success without side effects
                        break;
                    }

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
                    PrintNodeAfter(root!, _nodeBefore, action, success, swTotal.ElapsedMilliseconds);

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
                    var speakText = root!.Properties.Name.ValueOrDefault;
                    try { if (string.IsNullOrEmpty(speakText)) speakText = root.Patterns.Value.PatternOrDefault?.Value?.ValueOrDefault?.ToString(); } catch { }
                    if (!string.IsNullOrWhiteSpace(speakText))
                    {
                        try
                        {
                            AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "wkappbot",
                                Arguments = $"speak \"{speakText.Replace("\"", "'")}\" --bg",
                                UseShellExecute = false,
                                CreateNoWindow = true
                            }, Environment.CurrentDirectory, "A11Y");
                        }
                        catch { /* best effort */ }
                    }
                }

                // Zoom result feedback handled above via ContinueWith (fire-and-forget)

                // Auto-heal: click/invoke 전 티어 실패 시 삼두에 해결방법 자동 문의
                // 조건: CWD=WKAppBot 프로젝트 OR --auto-heal 플래그
                if (!success && action is "click" or "invoke" && ShouldAutoHeal(args))
                    FireAutoHeal(action, root!, hwnd, title);
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

                // Dry-run gate for window actions (focus and eval are read-safe)
                if (_dryRunMode.Value && action is not ("focus" or "eval"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[DRY-RUN] Would execute: a11y {action} \"{title}\"");
                    Console.ResetColor();
                    success = true;
                }
                else
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
}
