using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int WindowsCommand(string[] args)
    {
        // First positional arg = title filter (like inspect <title>)
        string? filterTitle = args.Length > 0 && !args[0].StartsWith("--") ? args[0] : GetArgValue(args, "--filter");
        string? filterProcess = GetArgValue(args, "--process");
        string? filterClass = GetArgValue(args, "--class");
        bool showAll = args.Contains("--all"); // include zero-size/invisible (also bypasses wkappbot self-filter)
        bool deep = args.Contains("--deep");   // include child windows (EnumChildWindows)
        bool uiaDeep = args.Contains("--uia-deep"); // deep UIA search (find --deep)
        bool uiaSearch = uiaDeep || args.Contains("--uia"); // also search UIA elements
        bool showCmd = args.Contains("--cmd"); // show process path + command line args
        int limit = int.TryParse(GetArgValue(args, "--limit"), out var lim) ? lim : 0; // 0=unlimited
        bool hasFilter = filterTitle != null || filterProcess != null || filterClass != null;

        // Snapshot focus state once (standard API)
        var focus = WindowFinder.FocusSnapshot.CaptureNow();

        // Process cache to avoid repeated Process.GetProcessById
        var processCache = new Dictionary<uint, string>();
        string GetProcessName(uint pid)
        {
            if (processCache.TryGetValue(pid, out var cached)) return cached;
            string name = "";
            try { name = Process.GetProcessById((int)pid).ProcessName; } catch { }
            processCache[pid] = name;
            return name;
        }

        // Command line cache (WMI, lazy — only used when --cmd)
        var cmdLineCache = new Dictionary<uint, string>();
        string GetCommandLine(uint pid)
        {
            if (cmdLineCache.TryGetValue(pid, out var cached)) return cached;
            string cmd = "";
            try
            {
                using var searcher = new System.Management.ManagementObjectSearcher(
                    $"SELECT CommandLine FROM Win32_Process WHERE ProcessId={pid}");
                foreach (System.Management.ManagementObject mo in searcher.Get())
                    cmd = mo["CommandLine"]?.ToString() ?? "";
            }
            catch { }
            cmdLineCache[pid] = cmd;
            return cmd;
        }

        // Pre-create matcher ONCE for all filter checks (avoid per-window regex compile)
        var ownerCandidateMatcher = filterTitle != null ? PatternMatcher.Create(filterTitle) : null;

        // Get window info, apply filters. Returns null if filtered out or noise.
        (string title, string className, string process, uint pid, int w, int h, bool visible)?
            GetWindowInfo(IntPtr hWnd)
        {
            // Use timeout-safe version to avoid blocking on hung windows (50ms timeout)
            string title = NativeMethods.GetWindowTextSafe(hWnd, 50);

            var classBuf = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            string className = classBuf.ToString();

            NativeMethods.GetWindowRect(hWnd, out var rect);
            int w = rect.Right - rect.Left;
            int h = rect.Bottom - rect.Top;
            bool visible = NativeMethods.IsWindowVisible(hWnd);

            // Skip noise unless --all (cheap checks first — no process lookup needed)
            if (!showAll && string.IsNullOrEmpty(title) && !visible) return null;
            if (!showAll && w == 0 && h == 0) return null;

            // Fast title-only pre-filter: if title doesn't match AND className doesn't match,
            // skip expensive GetProcessName. Most windows fail here → huge speedup.
            if (ownerCandidateMatcher != null && !showAll
                && !ownerCandidateMatcher.IsMatch(title)
                && !ownerCandidateMatcher.IsMatch(className))
            {
                // Last chance: check process name (needed for "*wsl*" matching "wslhost")
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint fpid);
                var fpn = GetProcessName(fpid);
                if (!ownerCandidateMatcher.IsMatch(fpn)) return null;
            }

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string processName = GetProcessName(pid);

            // Skip wkappbot windows
            if (!showAll && processName.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)) return null;

            // Apply filters — full search key check for remaining candidates
            if (ownerCandidateMatcher != null)
            {
                var searchKey = WindowFinder.BuildSearchKey(hWnd, className, title, processName, w, h, focus);
                if (!ownerCandidateMatcher.IsMatch(title) && !ownerCandidateMatcher.IsMatch(searchKey)) return null;
            }
            if (filterProcess != null)
            {
                bool match = PatternMatcher.IsPattern(filterProcess)
                    ? PatternMatcher.Create(filterProcess).IsMatch(processName)
                    : processName.Contains(filterProcess, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }
            if (filterClass != null)
            {
                bool match = PatternMatcher.IsPattern(filterClass)
                    ? PatternMatcher.Create(filterClass).IsMatch(className)
                    : className.Contains(filterClass, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }

            return (title, className, processName, pid, w, h, visible);
        }

        // Broken pipe detection — TeeTextWriter handles IOException globally,
        // but we check IsPipeBroken to stop expensive EnumWindows/EnumChildWindows early
        bool IsPipeBroken() => Console.Out is TeeTextWriter tee && tee.IsPipeBroken;

        void PrintWindow(IntPtr hWnd, string title, string className, string process,
            uint pid, int w, int h, bool visible, bool isChild, bool isForeground)
        {
            string vis = visible ? "" : " [hidden]";
            string prefix = isChild ? "  └ " : "";
            string displayTitle = title.Length > 60 ? title[..57] + "..." : title;
            if (string.IsNullOrEmpty(displayTitle)) displayTitle = "(no title)";

            // --cmd: resolve process exe path + command line args
            string? exePath = null;
            string? cmdArgs = null;
            if (showCmd)
            {
                try { exePath = Process.GetProcessById((int)pid).MainModule?.FileName; } catch { }
                var fullCmd = GetCommandLine(pid);
                if (!string.IsNullOrEmpty(fullCmd))
                {
                    // Strip leading exe path (quoted or unquoted) to show only the args
                    if (fullCmd.StartsWith('"'))
                    {
                        int end = fullCmd.IndexOf('"', 1);
                        cmdArgs = end > 0 ? fullCmd[(end + 1)..].TrimStart() : fullCmd;
                    }
                    else
                    {
                        int sp = fullCmd.IndexOf(' ');
                        cmdArgs = sp > 0 ? fullCmd[(sp + 1)..] : "";
                    }
                }
            }

            // Collect style tags for automation-relevant properties
            int style = NativeMethods.GetWindowLongW(hWnd, NativeMethods.GWL_STYLE);
            int exStyle = NativeMethods.GetWindowLongW(hWnd, NativeMethods.GWL_EXSTYLE);
            var tags = new List<string>(4);
            if (NativeMethods.IsIconic(hWnd)) tags.Add("min");
            else if ((style & 0x01000000 /*WS_MAXIMIZE*/) != 0) tags.Add("max");
            if ((style & NativeMethods.WS_DISABLED) != 0) tags.Add("disabled");
            if ((exStyle & NativeMethods.WS_EX_TOPMOST) != 0) tags.Add("topmost");
            if ((exStyle & NativeMethods.WS_EX_NOACTIVATE) != 0) tags.Add("noact");
            if ((exStyle & NativeMethods.WS_EX_LAYERED) != 0) tags.Add("layered");
            if ((exStyle & NativeMethods.WS_EX_TOOLWINDOW) != 0) tags.Add("tool");
            var ownerHwnd = NativeMethods.GetWindow(hWnd, 4 /*GW_OWNER*/);

            // ── Compact columnar output ──
            // [hwnd] title(40)  process(12)  WxH  flags
            var trimTitle = displayTitle.Length > 45 ? displayTitle[..42] + "..." : displayTitle;
            var trimProc = process.Length > 12 ? process[..12] : process;
            var sizeStr = $"{w}x{h}";
            // Flags: single-char compact (H=hidden L=layered T=tool M=min X=max D=disabled ↑=topmost)
            var flagChars = new StringBuilder();
            if (!visible) flagChars.Append('H');
            if (NativeMethods.IsIconic(hWnd)) flagChars.Append('M');
            if ((exStyle & NativeMethods.WS_EX_LAYERED) != 0) flagChars.Append('L');
            if ((exStyle & NativeMethods.WS_EX_TOOLWINDOW) != 0) flagChars.Append('T');
            if ((exStyle & NativeMethods.WS_EX_TOPMOST) != 0) flagChars.Append('↑');
            if ((exStyle & NativeMethods.WS_EX_NOACTIVATE) != 0) flagChars.Append('N');
            if ((style & NativeMethods.WS_DISABLED) != 0) flagChars.Append('D');
            var flagStr = flagChars.Length > 0 ? $" [{flagChars}]" : "";

            Console.ForegroundColor = isForeground ? ConsoleColor.Green
                : (visible && !string.IsNullOrEmpty(title)) ? ConsoleColor.White
                : ConsoleColor.DarkGray;
            Console.Write($"  {prefix}[{hWnd:X8}] ");
            Console.ForegroundColor = isForeground ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.Write($"{trimTitle,-45}");
            Console.ResetColor();
            Console.Write($" {trimProc,-12} {sizeStr,9}");
            if (isForeground) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(" ★"); Console.ResetColor(); }
            if (flagStr.Length > 0) { Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write(flagStr); Console.ResetColor(); }
            if (ownerHwnd != IntPtr.Zero) { Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write($" ◇{ownerHwnd:X}"); Console.ResetColor(); }
            Console.WriteLine();

            // --cmd: print process exe path + args
            if (showCmd && (exePath != null || cmdArgs != null))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (exePath != null)
                    Console.WriteLine($"    📂 {exePath}");
                if (!string.IsNullOrEmpty(cmdArgs))
                    Console.WriteLine($"    ▶  {cmdArgs}");
                Console.ResetColor();
            }

            // Lightweight focus path for ALL windows (Win32 only, <1ms, generalized)
            if (!isChild)
            {
                var focusPath = GrapHelper.GetFocusPath(hWnd);
                if (focusPath != null)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    ⌨ ");
                    Console.ResetColor();
                    Console.WriteLine(focusPath);
                }
            }
            // Full UIA focus path — DISABLED for speed. Use GetFocusPath (Win32) above.
            // To re-enable: if (!isChild && (isForeground || hasFilter)) PrintFocusAndPopup(hWnd);
        }

        void PrintFocusAndPopup(IntPtr hWnd)
        {
            // 1. Keyboard focus child via GetGUIThreadInfo for this window's thread
            var threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out _);
            if (threadId != 0)
            {
                var gti = new NativeMethods.GUITHREADINFO
                    { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
                bool gtiOk = NativeMethods.GetGUIThreadInfo(threadId, ref gti);
                if (gtiOk && gti.hwndFocus == IntPtr.Zero)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("    ⌨ focus: (none — no stored keyboard focus for this thread)");
                    Console.ResetColor();
                }
                // Show focus child (including focus == self for a11y path)
                if (gtiOk && gti.hwndFocus != IntPtr.Zero)
                {
                    var focusBuf = new StringBuilder(256);
                    NativeMethods.GetWindowTextW(gti.hwndFocus, focusBuf, focusBuf.Capacity);
                    var focusClassBuf = new StringBuilder(128);
                    NativeMethods.GetClassNameW(gti.hwndFocus, focusClassBuf, focusClassBuf.Capacity);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    ⌨ focus: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    var focusTitle = focusBuf.ToString();
                    Console.WriteLine($"[{gti.hwndFocus:X8}] \"{(focusTitle.Length > 40 ? focusTitle[..37] + "..." : focusTitle)}\" ({focusClassBuf})");
                    Console.ResetColor();

                    // UIA focused LEAF element — TreeWalker + HasKeyboardFocus (works for background windows)
                    try
                    {
                        using var uia = new FlaUI.UIA3.UIA3Automation();
                        FlaUI.Core.AutomationElements.AutomationElement? focusEl = null;
                        // Get UIA root from hwndFocus, then walk to leaf with HasKeyboardFocus
                        try
                        {
                            var hwndRoot = uia.FromHandle(gti.hwndFocus);
                            focusEl = GrapHelper.FindFocusedLeaf(uia, hwndRoot, hWnd) ?? hwndRoot;
                        }
                        catch { }
                        if (focusEl == null) try { focusEl = uia.FocusedElement(); } catch { } // global fallback
                        if (focusEl == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"    ⚠ a11y focus: unavailable (hwndFocus=0x{gti.hwndFocus:X}, thread={threadId})");
                            Console.ResetColor();
                        }
                        else
                        {
                            // Full grap expression: "windowTitle#a11y/full/path"
                            var grapExpr = WKAppBot.Win32.Accessibility.GrapHelper.BuildGrapExpression(hWnd, focusEl);
                            var leafRect = focusEl.Properties.BoundingRectangle.ValueOrDefault;
                            var vp = focusEl.Patterns.Value.PatternOrDefault;
                            var editable = vp != null && !vp.IsReadOnly.ValueOrDefault;

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("    ↳ ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(grapExpr);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($" ({leafRect.X},{leafRect.Y} {leafRect.Width}x{leafRect.Height})");
                            if (editable) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(" [editable]"); }
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }
                    catch { }
                }

                // CDP active tab URL (best-effort)
                try
                {
                    NativeMethods.GetWindowThreadProcessId(hWnd, out uint cdpPid);
                    var cdpPort = WKAppBot.WebBot.CdpClient.DetectCdpPort((int)cdpPid);
                    if (cdpPort > 0)
                    {
                        using var cdpProbe = new WKAppBot.WebBot.CdpClient();
                        cdpProbe.ConnectAsync(cdpPort).GetAwaiter().GetResult();
                        var tabUrl = cdpProbe.EvalAsync("location.href").GetAwaiter().GetResult() ?? "";
                        if (tabUrl.Length > 0 && tabUrl != "about:blank")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write("    ↳ url: ");
                            Console.ResetColor();
                            Console.WriteLine(tabUrl.Length > 80 ? tabUrl[..77] + "..." : tabUrl);
                        }
                    }
                }
                catch { }
            }

            // 2. Owned popup/dialog windows (other top-levels whose owner = hWnd)
            var popups = new List<IntPtr>();
            PulseStep.Mark("popup-enum-start");
            NativeMethods.EnumWindows((h, _) =>
            {
                if (h == hWnd) return true;
                if (!NativeMethods.IsWindowVisible(h)) return true;
                var owner = NativeMethods.GetWindow(h, 4 /*GW_OWNER*/);
                if (owner == hWnd)
                    popups.Add(h);
                return true;
            }, IntPtr.Zero);
            PulseStep.Mark("popup-enum-done");

            foreach (var popup in popups)
            {
                var pTitleBuf = new StringBuilder(256);
                NativeMethods.GetWindowTextW(popup, pTitleBuf, pTitleBuf.Capacity);
                var pClassBuf = new StringBuilder(128);
                NativeMethods.GetClassNameW(popup, pClassBuf, pClassBuf.Capacity);
                NativeMethods.GetWindowRect(popup, out var pRect);
                var pTitle = pTitleBuf.ToString();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("    ◇ popup: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[{popup:X8}] \"{(pTitle.Length > 40 ? pTitle[..37] + "..." : pTitle)}\" ({pClassBuf}) {pRect.Right - pRect.Left}x{pRect.Bottom - pRect.Top}");
                Console.ResetColor();
            }
        }

        // ── Hot Focus Line helper ──
        static void PrintHotFocusLine(IntPtr fgWnd, WindowFinder.FocusSnapshot focus, Func<uint, string> getProcessName)
        {
            if (fgWnd == IntPtr.Zero) return;

            // Get keyboard focus hwnd
            var kbFocus = focus.KeyboardHwnd;
            if (kbFocus == IntPtr.Zero) kbFocus = fgWnd;

            // Build chain: focus → parent → parent → ... → top-level
            var chain = new List<IntPtr>();
            var seen = new HashSet<IntPtr>();
            var cur = kbFocus;
            while (cur != IntPtr.Zero && seen.Add(cur))
            {
                chain.Add(cur);
                cur = NativeMethods.GetParent(cur);
            }
            // Ensure foreground window is in chain (it might be root already)
            if (!seen.Contains(fgWnd))
                chain.Add(fgWnd);

            Console.WriteLine("── hot focus ──");
            var titleBuf = new StringBuilder(256);
            var classBuf = new StringBuilder(128);

            for (int i = 0; i < chain.Count; i++)
            {
                var h = chain[i];
                titleBuf.Clear(); classBuf.Clear();
                NativeMethods.GetWindowTextW(h, titleBuf, titleBuf.Capacity);
                NativeMethods.GetClassNameW(h, classBuf, classBuf.Capacity);
                NativeMethods.GetWindowThreadProcessId(h, out uint pid);
                NativeMethods.GetWindowRect(h, out var rect);
                int w = rect.Right - rect.Left, ht = rect.Bottom - rect.Top;

                string indent = new string(' ', i * 2);
                string arrow = i == 0 ? "⌨ " : "└ ";
                string title = titleBuf.ToString();
                string displayTitle = title.Length > 50 ? title[..47] + "..." : title;
                if (string.IsNullOrEmpty(displayTitle)) displayTitle = "(no title)";
                string proc = getProcessName(pid);

                // Style tags
                int style = NativeMethods.GetWindowLongW(h, NativeMethods.GWL_STYLE);
                int exStyle = NativeMethods.GetWindowLongW(h, NativeMethods.GWL_EXSTYLE);
                var tags = new List<string>(3);
                if (NativeMethods.IsIconic(h)) tags.Add("min");
                else if ((style & 0x01000000) != 0) tags.Add("max");
                if ((style & NativeMethods.WS_DISABLED) != 0) tags.Add("disabled");
                if ((exStyle & NativeMethods.WS_EX_TOPMOST) != 0) tags.Add("topmost");

                Console.ForegroundColor = i == 0 ? ConsoleColor.Magenta : ConsoleColor.DarkGray;
                Console.Write($"  {indent}{arrow}");
                Console.ForegroundColor = i == 0 ? ConsoleColor.White : ConsoleColor.Gray;
                Console.Write($"[{h:X8}] \"{displayTitle}\" ({classBuf}) {w}x{ht}");
                if (!string.IsNullOrEmpty(proc))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"  [{proc}]");
                }
                if (tags.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($" [{string.Join(",", tags)}]");
                }
                if (h == fgWnd) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(" ★"); }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        IntPtr fgWnd = NativeMethods.GetForegroundWindow();
        int totalCount = 0;
        int visibleResultCount = 0;
        var matchedHiddenHwnds = new List<IntPtr>();
        int uiaMatchWindows = 0;

        PulseStep.Mark("focus-snapshot");
        // ── Hot Focus Line: focused child → parent → ... → top-level ──
        PrintHotFocusLine(fgWnd, focus, GetProcessName);
        PulseStep.Mark("hot-focus-line-done");

        // EnumWindows enumerates in Z-order (front to back) — no re-sort needed!
        bool hasPathSearch = filterTitle != null && (filterTitle.Contains('/') || filterTitle.Contains("**"));
        string mode = hasPathSearch ? "find path (Z-order ★=foreground)"
            : uiaDeep ? "find --deep (Z-order ★=foreground)"
            : uiaSearch ? "find (Z-order ★=foreground)"
            : deep ? "windows (deep, Z-order ★=foreground)"
            : "windows (Z-order ★=foreground)";
        Console.WriteLine($"── {mode} ──");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  [hwnd___] title_____________________________________  process______ ____WxH  flags");
        Console.ResetColor();

        // Helper: get raw window info WITHOUT title filter (for --uia mode)
        (string title, string className, string process, uint pid, int w, int h, bool visible)?
            GetRawWindowInfo(IntPtr hWnd)
        {
            var titleBuf = new StringBuilder(512);
            NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
            string title = titleBuf.ToString();

            var classBuf = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
            string className = classBuf.ToString();

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string processName = GetProcessName(pid);

            NativeMethods.GetWindowRect(hWnd, out var rect);
            int w = rect.Right - rect.Left;
            int h = rect.Bottom - rect.Top;
            bool visible = NativeMethods.IsWindowVisible(hWnd);

            if (!showAll && string.IsNullOrEmpty(title) && !visible) return null;
            if (!showAll && w == 0 && h == 0) return null;

            // Skip wkappbot windows (Eye/Zoom overlays) — don't pollute results.
            // --all overrides: useful to identify mcp/eye/a11y processes by args (--cmd).
            if (!showAll && processName.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)) return null;

            // Process/class filters still apply (not title!)
            if (filterProcess != null)
            {
                bool match = PatternMatcher.IsPattern(filterProcess)
                    ? PatternMatcher.Create(filterProcess).IsMatch(processName)
                    : processName.Contains(filterProcess, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }
            if (filterClass != null)
            {
                bool match = PatternMatcher.IsPattern(filterClass)
                    ? PatternMatcher.Create(filterClass).IsMatch(className)
                    : className.Contains(filterClass, StringComparison.OrdinalIgnoreCase);
                if (!match) return null;
            }

            return (title, className, processName, pid, w, h, visible);
        }

        // Helper: print UIA matches inline under a window
        void PrintUiaMatches(List<UiaQuickMatch> matches)
        {
            foreach (var m in matches)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"    → [{m.ControlType}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{m.Name}\"");
                if (!string.IsNullOrEmpty(m.AutomationId))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" aid=\"{m.AutomationId}\"");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Helper: print UIA matches with name path (for path search mode)
        void PrintUiaMatchesWithPath(List<UiaQuickMatch> matches)
        {
            foreach (var m in matches)
            {
                var tag = GrapHelper.FormatNodeLabel(m.ControlType, m.AutomationId, m.Name);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"    → {tag}");
                if (!string.IsNullOrEmpty(m.NamePath))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"  path={CollapsePath(m.NamePath)}");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Collapse consecutive same-name segments for display (VS Code style)
        // Pane/Pane/Pane/Pane → Pane/...   (3+ repeats → keep first + ...)
        string CollapsePath(string path)
        {
            var segs = path.Split('/');
            var result = new List<string>();
            int i = 0;
            while (i < segs.Length)
            {
                result.Add(segs[i]);
                int run = 1;
                while (i + run < segs.Length && segs[i + run] == segs[i]) run++;
                if (run >= 3) result.Add("...");
                i += run;
            }
            return string.Join("/", result);
        }

        // Build regex from path pattern for partial matching against full UIA name paths.
        // Path pattern → regex (partial match, IgnoreCase)
        // ** → .*             (any chars including / — crosses levels)
        // *  → [^/]*          (any chars excluding / — within one level)
        // /  → [^/]*/[^/]*   (one level boundary)
        // ?  → [^/]           (single char excluding /)
        // else → Regex.Escape (literal — . $ + etc. safely escaped)
        // "투혼/현재가"    → 투혼[^/]*/[^/]*현재가
        // "투혼/**/현재가" → 투혼[^/]*/[^/]*.*[^/]*/[^/]*현재가
        Regex BuildPathRegex(string pattern)
        {
            // Tokenize with regex: ** first (greedy), then single special chars, then literal runs
            var rxStr = Regex.Replace(pattern, @"\*\*|[*/?]|[^*/?]+", m => m.Value switch
            {
                "**" => ".*",
                "*"  => "[^/]*",
                "/"  => "[^/]*/[^/]*",
                "?"  => "[^/]",
                _    => Regex.Escape(m.Value),  // literal chunk (.→\. $→\$ etc.)
            });
            return new Regex(rxStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        PulseStep.Mark("enum-windows-start");
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (IsPipeBroken()) return false;
            bool isFg = hWnd == fgWnd;
            bool parentPrinted = false;

            if (uiaSearch && filterTitle != null)
            {
                // ── Unified search: title OR UIA elements ──
                // Path search ("/") → build full name-path, match with PathGlob
                // Regular search → same keyword for title and UIA (OR logic)
                bool isPathSearch = filterTitle.Contains('/') || filterTitle.Contains("**");

                var raw = GetRawWindowInfo(hWnd);
                if (raw == null) return true; // noise or process/class mismatch

                var r = raw.Value;
                if (!r.visible || r.w < 50 || r.h < 50) return true; // skip tiny/hidden

                if (isPathSearch)
                {
                    // ── Path search: "투혼/현재가" → regex partial match on full name path ──
                    var pathRx = BuildPathRegex(filterTitle);

                    // Search all UIA elements with name paths (maxResults high — filtering is done post-hoc by regex)
                    var allElements = uiaDeep
                        ? UiaLocator.QuickSearch(hWnd, "", maxDepth: 12, maxResults: 5000, maxVisited: 3000, timeoutMs: 10000)
                        : UiaLocator.QuickSearch(hWnd, "", maxDepth: 4, maxResults: 2000, maxVisited: 1000, timeoutMs: 5000);

                    // Match full path: windowTitle/[Type]Name/... (partial match!)
                    // Skip [Text] elements — conversation content noise (self-referencing echo)
                    var uiaMatches = new List<UiaQuickMatch>();
                    foreach (var el in allElements)
                    {
                        if (el.ControlType == "Text") continue;
                        string fullPath = string.IsNullOrEmpty(el.NamePath)
                            ? r.title : $"{r.title}/{el.NamePath}";
                        if (pathRx.IsMatch(fullPath))
                            uiaMatches.Add(el);
                    }

                    // Also check window-only match
                    bool windowOnlyMatch = pathRx.IsMatch(r.title);
                    if (uiaMatches.Count == 0 && !windowOnlyMatch) return true;

                    PrintWindow(hWnd, r.title, r.className, r.process, r.pid, r.w, r.h, r.visible, false, isFg);
                    if (uiaMatches.Count > 0)
                    {
                        PrintUiaMatchesWithPath(uiaMatches);
                        uiaMatchWindows++;
                    }
                    PrintCachedDynA11y(hWnd);
                }
                else
                {
                    // ── Regular search: keyword matches title OR UIA elements (OR logic) ──
                    var titleMatch = ownerCandidateMatcher!.IsMatch(r.title);

                    var uiaMatches = uiaDeep
                        ? UiaLocator.QuickSearch(hWnd, filterTitle, maxDepth: 12, maxResults: 10, maxVisited: 1500, timeoutMs: 8000)
                        : UiaLocator.QuickSearch(hWnd, filterTitle);

                    bool hasCached = _dynA11yCache.ContainsKey(hWnd);
                    if (!titleMatch && uiaMatches.Count == 0 && !hasCached) return true;

                    // Print window (Cyan if UIA-only match, normal if title match)
                    if (!titleMatch)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        string dt = r.title.Length > 60 ? r.title[..57] + "..." : r.title;
                        if (string.IsNullOrEmpty(dt)) dt = "(no title)";
                        Console.Write($"  [{hWnd:X8}] \"{dt}\"  ({r.className})  [{r.process}]");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"  ({uiaMatches.Count} UIA match)");
                        Console.ResetColor();
                    }
                    else
                    {
                        PrintWindow(hWnd, r.title, r.className, r.process, r.pid, r.w, r.h, r.visible, false, isFg);
                    }

                    if (uiaMatches.Count > 0)
                    {
                        PrintUiaMatches(uiaMatches);
                        uiaMatchWindows++;
                    }
                    PrintCachedDynA11y(hWnd);
                }

                totalCount++;
                parentPrinted = true;
                Console.Out.Flush();
                if (limit > 0 && totalCount >= limit) return false;
            }
            else
            {
                // ── Original mode: title/process/class filter only ──
                var info = GetWindowInfo(hWnd);

                if (info != null)
                {
                    var v = info.Value;
                    PrintWindow(hWnd, v.title, v.className, v.process, v.pid, v.w, v.h, v.visible, false, isFg);
                    totalCount++;
                    if (v.visible && v.w >= 50 && v.h >= 50) visibleResultCount++;
                    else matchedHiddenHwnds.Add(hWnd);
                    parentPrinted = true;
                    if (limit > 0 && totalCount >= limit) { Console.Out.Flush(); return false; }
                }
                else if (ownerCandidateMatcher != null)
                {
                    // ── Owner chain candidate: even 0x0/hidden windows that match pattern ──
                    // e.g. PseudoConsoleWindow(wsl.exe, 0x0) with owner=CASCADIA
                    var owner = NativeMethods.GetWindow(hWnd, 4 /* GW_OWNER */);
                    if (owner != IntPtr.Zero && owner != hWnd)
                    {
                        NativeMethods.GetWindowThreadProcessId(hWnd, out uint fpid);
                        var fpn = GetProcessName(fpid);
                        if (ownerCandidateMatcher.IsMatch(fpn))
                            matchedHiddenHwnds.Add(hWnd);
                    }
                }

                // Child windows (if --deep)
                if (deep)
                {
                    NativeMethods.EnumChildWindows(hWnd, (childHwnd, _) =>
                    {
                        if (IsPipeBroken() || (limit > 0 && totalCount >= limit)) return false;
                        var childInfo = GetWindowInfo(childHwnd);
                        if (childInfo != null)
                        {
                            if (!parentPrinted)
                            {
                                var titleBuf = new StringBuilder(512);
                                NativeMethods.GetWindowTextW(hWnd, titleBuf, titleBuf.Capacity);
                                var classBuf = new StringBuilder(256);
                                NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
                                NativeMethods.GetWindowThreadProcessId(hWnd, out uint ppid);
                                NativeMethods.GetWindowRect(hWnd, out var prect);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine($"  [{hWnd:X8}] \"{titleBuf}\"  ({classBuf}) {prect.Right - prect.Left}x{prect.Bottom - prect.Top}  [{GetProcessName(ppid)} pid={ppid}]  (parent)");
                                Console.ResetColor();
                                parentPrinted = true;
                            }
                            var c = childInfo.Value;
                            PrintWindow(childHwnd, c.title, c.className, c.process, c.pid, c.w, c.h, c.visible, true, false);
                            totalCount++;
                        }
                        return true;
                    }, IntPtr.Zero);
                }

                if (parentPrinted) Console.Out.Flush();
            }

            return !(limit > 0 && totalCount >= limit);
        }, IntPtr.Zero);
        PulseStep.Done("enum-windows-done");

        // ── Parent process fallback: find host windows for hidden child processes ──
        // If filter matched only hidden/tiny windows (wslhost → WindowsTerminal, chrome renderer → Chrome),
        // show their host windows too via WindowFinder parent chain.
        // If only hidden/tiny windows matched, follow owner-window chain to find visible host.
        // e.g. wslhost PseudoConsoleWindow(owner=CASCADIA) → WindowsTerminal CASCADIA tab.
        // O(1) per matched window × max 3 hops — no re-enumeration needed.
        if (filterTitle != null && visibleResultCount == 0 && matchedHiddenHwnds.Count > 0)
        {
            var hostHwnds = new HashSet<IntPtr>();
            var progressSw = System.Diagnostics.Stopwatch.StartNew();
            long lastCommitMs = 0;
            uint lastReportedPid = 0;

            void ReportProgress(IntPtr hwnd, uint pid, string tag)
            {
                if (pid == lastReportedPid) return; // same process → skip output
                lastReportedPid = pid;
                var t = new StringBuilder(256);
                NativeMethods.GetWindowTextW(hwnd, t, t.Capacity);
                var p = GetProcessName(pid);
                var preview = t.Length > 40 ? t.ToString(0, 37) + "..." : t.ToString();
                var line = $"[HOST:{tag}] {hwnd:X8} \"{preview}\" [{p}]";
                var elapsed = progressSw.ElapsedMilliseconds;
                if (elapsed - lastCommitMs >= 1000)
                {
                    Console.Error.WriteLine(line);
                    lastCommitMs = elapsed;
                }
                else
                    Console.Error.Write($"\r{line,-100}");
            }

            // ── Strategy 1: Owner chain (O(1) per hidden window, max 3 hops) ──
            foreach (var hiddenHwnd in matchedHiddenHwnds)
            {
                var cur = hiddenHwnd;
                for (int hop = 0; hop < 3; hop++)
                {
                    var owner = NativeMethods.GetWindow(cur, 4 /* GW_OWNER */);
                    if (owner == IntPtr.Zero || owner == cur) break;
                    NativeMethods.GetWindowThreadProcessId(owner, out uint opid);
                    ReportProgress(owner, opid, "owner");
                    if (NativeMethods.IsWindowVisible(owner))
                    {
                        NativeMethods.GetWindowRect(owner, out var or);
                        if (or.Right - or.Left >= 50 && or.Bottom - or.Top >= 50)
                            hostHwnds.Add(owner);
                        break;
                    }
                    cur = owner;
                }
            }

            // ── Strategy 2: Top-down child process scan (only if owner chain found nothing) ──
            // For each visible top-level window, check if ANY child belongs to a matching process.
            // ClassName pre-filter: only scan known host classes (삼두 optimization #2).
            // Fast skip: no children=skip, same-PID children=skip.
            if (hostHwnds.Count == 0)
            {
                // Known host window classes that may host cross-process children
                var hostClasses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "CASCADIA_HOSTING_WINDOW_CLASS",  // Windows Terminal (WSL, PowerShell)
                    "Chrome_WidgetWin_1",             // Chrome/Edge/Electron (renderer processes)
                    "ConsoleWindowClass",             // Classic conhost
                    "ApplicationFrameWindow",         // UWP apps
                    "Windows.UI.Core.CoreWindow",     // WinUI
                };

                var fmatch = PatternMatcher.Create(filterTitle);
                NativeMethods.EnumWindows((topWnd, _) =>
                {
                    if (!NativeMethods.IsWindowVisible(topWnd)) return true;
                    NativeMethods.GetWindowRect(topWnd, out var tr);
                    if (tr.Right - tr.Left < 50 || tr.Bottom - tr.Top < 50) return true;

                    // ClassName pre-filter — skip windows that never host cross-process children
                    var clsBuf = new StringBuilder(256);
                    NativeMethods.GetClassNameW(topWnd, clsBuf, clsBuf.Capacity);
                    if (!hostClasses.Contains(clsBuf.ToString())) return true;

                    NativeMethods.GetWindowThreadProcessId(topWnd, out uint topPid);

                    // Quick skip: check first child's PID — if same as parent, no cross-process children likely
                    bool hasCrossProcess = false;
                    bool foundMatch = false;
                    NativeMethods.EnumChildWindows(topWnd, (childWnd, _) =>
                    {
                        NativeMethods.GetWindowThreadProcessId(childWnd, out uint childPid);
                        if (childPid == topPid) return true; // same process → keep scanning
                        hasCrossProcess = true;
                        // Different process child! Check if it matches the search pattern
                        var cn = GetProcessName(childPid);
                        if (fmatch.IsMatch(cn))
                        {
                            foundMatch = true;
                            return false; // early exit — found it!
                        }
                        return true;
                    }, IntPtr.Zero);

                    if (foundMatch && hostHwnds.Add(topWnd))
                    {
                        ReportProgress(topWnd, topPid, "child");
                    }
                    return true;
                }, IntPtr.Zero);
            }

            // Final summary
            Console.Error.WriteLine($"\r{"[HOST] " + matchedHiddenHwnds.Count + " hidden → " + hostHwnds.Count + " host(s)",-100}");

            if (hostHwnds.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n── host windows (owner/child) ──");
                Console.ResetColor();
                foreach (var oh in hostHwnds)
                {
                    var ht = WindowFinder.GetWindowText(oh);
                    var hc = WindowFinder.GetClassName(oh);
                    NativeMethods.GetWindowThreadProcessId(oh, out uint hpid);
                    NativeMethods.GetWindowRect(oh, out var hrect);
                    PrintWindow(oh, ht, hc, GetProcessName(hpid), hpid,
                        hrect.Right - hrect.Left, hrect.Bottom - hrect.Top, true, false, oh == fgWnd);
                    totalCount++;
                }
            }
        }

        Console.WriteLine();
        string uiaNote = uiaSearch ? $", UIA matched in {uiaMatchWindows} window(s)" : "";
        string limitNote = limit > 0 ? $", --limit {limit}" : "";
        string hint = uiaSearch ? "(--deep: thorough search)" : "(--uia: accessibility search, --deep: child windows)";
        Console.WriteLine($"Total: {totalCount}{uiaNote} {hint}{limitNote}");
        return 0;
    }
}
