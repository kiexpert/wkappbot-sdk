using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: inspect, focus, watch, capture commands + watch helpers
internal partial class Program
{
    // ── find (unified search — absorbs windows --uia + inspect --filter) ──

    static int FindCommand(string[] args)
    {
        if (args.Length == 0 || (args.Length == 1 && args[0].StartsWith("--")))
            return Error("Usage: wkappbot find <keyword> [--deep] [--limit N] [--process <name>]\n" +
                         "  Unified search: window titles + UIA accessibility elements.\n" +
                         "  Path search: find \"윈도우/UIA요소\" — / separates hierarchy levels.\n" +
                         "    * = any chars within one level, ** = any number of levels.\n" +
                         "    Each segment is implicitly *segment* (contains match).\n" +
                         "  Scoped search: find \"윈도우#UIA스코프\" — # narrows UIA search root.\n" +
                         "    Combine: find \"영웅문/*실현*#*잔고*\" + keyword after #\n" +
                         "  --deep: Deeper UIA tree search (depth 12, slower but thorough).\n" +
                         "  Examples:\n" +
                         "    find \"Claude\"                       Search everywhere for 'Claude'\n" +
                         "    find \"투혼/현재가\"                  투혼 windows → ... → 현재가 element\n" +
                         "    find \"투혼/**/현재가\"               투혼 → any depth → 현재가\n" +
                         "    find \"*영웅문*#*잔고확인*\"          영웅문 → UIA scope 잔고확인 → list elements\n" +
                         "    find \"*영웅문*/*실현*#*잔고*\" 예수금  영웅문/실현* child → #잔고 scope → 예수금 검색");

        // Check for scope separators — '#' = UIA scope, '/' with 2+ segments = Win32 child hierarchy
        string firstArg = args.FirstOrDefault(a => !a.StartsWith("--")) ?? "";
        var (w32segs, uiaP) = GrapHelper.SplitGrap(firstArg);
        if (uiaP != null || w32segs.Length >= 2)
            return FindScopedCommand(args);

        // Preprocess: inject --uia (or --uia-deep for --deep) and forward to WindowsCommand
        var forwarded = new List<string>(args);
        bool hasDeep = forwarded.Remove("--deep");

        // Path search: "**" present → auto deep (hierarchy needs thorough search)
        string keyword = forwarded.FirstOrDefault(a => !a.StartsWith("--")) ?? "";
        if (keyword.Contains("**"))
            hasDeep = true;

        forwarded.Add(hasDeep ? "--uia-deep" : "--uia");
        return WindowsCommand(forwarded.ToArray());
    }

    /// <summary>
    /// Scoped find: "window#uiaScope" [keyword] — narrow UIA root, then search within.
    /// Without keyword: lists all elements under scope (like inspect).
    /// With keyword: searches for matching elements within scope.
    /// </summary>
    static int FindScopedCommand(string[] args)
    {
        bool hasDeep = args.Contains("--deep");
        int limit = int.TryParse(GetArgValue(args, "--limit"), out var lim) ? lim : 50;

        // First positional arg = grap#scope, second = keyword (optional)
        var positional = args.Where(a => !a.StartsWith("--")).ToList();
        string grapArg = positional[0];
        string? searchKeyword = positional.Count > 1 ? positional[1] : null;

        // Split at first '#': before = Win32 path (/), after = UIA scope (/ and # equivalent)
        var (win32Segments, uiaPath) = GrapHelper.SplitGrap(grapArg);
        if (win32Segments.Length == 0)
            return Error("find requires window title: find \"window/child\" or find \"window#scope\"");

        var windows = WindowFinder.FindByTitle(win32Segments[0]);
        if (windows.Count == 0) return Error($"Window not found: \"{win32Segments[0]}\"");
        var mainWin = windows[0];
        Console.WriteLine($"Window: [{mainWin.Handle:X8}] \"{mainWin.Title}\"");

        // Elevation auto-detect: delegate to admin proxy if target is elevated
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            mainWin.Handle, "a11y", new[] { "find" }.Concat(args).ToArray());
        if (delegated) return delegateExit;

        // Blocker detection (bug report: find should show blocker info)
        try
        {
            var readiness = CreateInputReadiness();
            var blockerInfo = readiness.DetectBlocker(mainWin.Handle);
            if (blockerInfo != null)
                Console.WriteLine($"[BLOCK] Blocker detected: \"{blockerInfo.Title}\" (class={blockerInfo.ClassName}, hwnd=0x{blockerInfo.Handle.ToInt64():X8})");
        }
        catch { /* best effort */ }

        // Walk Win32 children (segments before '#')
        IntPtr targetHwnd = mainWin.Handle;
        for (int si = 1; si < win32Segments.Length; si++)
        {
            var childMatch = FindChildWindowByPattern(mainWin.Handle, win32Segments[si]);
            if (childMatch != null)
            {
                targetHwnd = childMatch.Value.handle;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"  └ Child: ");
                Console.ResetColor();
                Console.WriteLine($"\"{childMatch.Value.title}\" (hWnd=0x{targetHwnd:X})");
            }
            else
            {
                return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            }
        }

        // UIA scope (after '#')
        using var uia = new FlaUI.UIA3.UIA3Automation();
        uia.ConnectionTimeout = TimeSpan.FromSeconds(5);
        uia.TransactionTimeout = TimeSpan.FromSeconds(5);

        AutomationElement? scoped = uia.FromHandle(targetHwnd);
        if (scoped is null) return Error("UIA root not available.");

        if (!string.IsNullOrEmpty(uiaPath))
        {
            scoped = GrapHelper.FindUiaScope(scoped!, uiaPath);
            if (scoped is null)
                return Error($"UIA scope not found: \"{uiaPath}\"");
        }

        var scopeName = scoped!.Properties.Name.ValueOrDefault ?? "(unnamed)";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"  # Scope: ");
        Console.ResetColor();
        Console.WriteLine($"\"{scopeName}\"");

        // Search within scope
        int maxDepth = hasDeep ? 12 : 6;
        int maxResults = limit;
        int maxVisited = hasDeep ? 3000 : 1000;
        int timeoutMs = hasDeep ? 10000 : 5000;

        var matches = UiaLocator.QuickSearch(scoped!, searchKeyword ?? "",
            maxDepth: maxDepth, maxResults: maxResults, maxVisited: maxVisited, timeoutMs: timeoutMs);

        Console.WriteLine();
        if (matches.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(searchKeyword != null
                ? $"  No elements matching \"{searchKeyword}\" within scope \"{scopeName}\""
                : $"  No elements found within scope \"{scopeName}\"");
            Console.ResetColor();
            return 0;
        }

        string mode = searchKeyword != null ? $"find \"{searchKeyword}\"" : "list all";
        Console.WriteLine($"── {mode} under #{scopeName} ({matches.Count} results) ──");
        foreach (var m in matches)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  [{m.ControlType}] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\"{m.Name}\"");
            if (!string.IsNullOrEmpty(m.AutomationId))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" aid=\"{m.AutomationId}\"");
            }
            if (!string.IsNullOrEmpty(m.NamePath))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"  path={m.NamePath}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine($"Total: {matches.Count} (scope=\"{scopeName}\", depth={maxDepth})");
        return 0;
    }

    // ── inspect ────────────────────────────────────────────────

    static int InspectCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot inspect <window-title>[/<child-pattern>][#<uia-scope>] [--depth N] [--win32] [--filter <pattern>]\n" +
                "  <child-pattern>: MDI/자식 윈도우 glob 매칭 (예: 투혼/**현재가, 투혼/[0600]*)\n" +
                "  #<uia-scope>: UIA 요소 Name 매칭으로 검색 루트 축소 (예: *영웅문*#*잔고확인*)\n" +
                "  --filter: Search entire UIA tree for matching elements (Name/AutomationId/ControlType)\n" +
                "            Supports wildcards (*/?), regex: prefix, or plain substring");

        string rawTitle = args[0];
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 5;
        bool win32Mode = args.Contains("--win32");
        string? filter = GetArgValue(args, "--filter");

        // Split at first '#': before = Win32 path (/), after = UIA scope (/ and # equivalent)
        var (win32Segments, uiaScopePath) = GrapHelper.SplitGrap(rawTitle);
        if (win32Segments.Length == 0) return Error("Empty grap pattern");

        var windows = WindowFinder.FindByTitle(win32Segments[0]);
        if (windows.Count == 0)
        {
            Console.WriteLine($"No window found matching: \"{win32Segments[0]}\"");
            return 1;
        }

        var mainWin = windows[0];

        // Elevation auto-detect: delegate to admin proxy if target is elevated
        var (delegated, delegateExit) = ElevationHelper.TryDelegateIfElevated(
            mainWin.Handle, "inspect", args);
        if (delegated) return delegateExit;

        // Walk Win32 children (segments before '#')
        IntPtr inspectHandle = mainWin.Handle;
        string? matchedFormId = null;
        string? matchedFormTitle = null;

        for (int si = 1; si < win32Segments.Length; si++)
        {
            var childMatch = FindChildWindowByPattern(mainWin.Handle, win32Segments[si]);
            if (childMatch != null)
            {
                inspectHandle = childMatch.Value.handle;
                matchedFormId = childMatch.Value.formId;
                matchedFormTitle = childMatch.Value.title;
            }
            else
            {
                return Error($"Win32 child not found: \"{win32Segments[si]}\"");
            }
        }

        // Display window info
        if (matchedFormTitle != null)
        {
            Console.WriteLine($"Window: {mainWin}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  └ Child: ");
            Console.ResetColor();
            Console.WriteLine($"\"{matchedFormTitle}\" (hWnd=0x{inspectHandle:X})");
        }
        else
        {
            Console.WriteLine($"Window: {mainWin}");
        }

        // 노하우 방송: 프로파일 매칭 → 해당 폼 폴더의 knowhow.md
        BroadcastInspectKnowhow(mainWin.Handle, mainWin.ClassName, matchedFormId, matchedFormTitle);

        // Blocker detection (bug report: inspect should show blocker info)
        try
        {
            var readiness = CreateInputReadiness();
            var blockerInfo = readiness.DetectBlocker(inspectHandle);
            if (blockerInfo != null)
                Console.WriteLine($"[BLOCK] Blocker detected: \"{blockerInfo.Title}\" (class={blockerInfo.ClassName}, hwnd=0x{blockerInfo.Handle.ToInt64():X8})");
        }
        catch { /* best effort */ }

        Console.WriteLine();

        if (win32Mode)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── Win32 Window Tree (depth={depth}) ──");
            Console.ResetColor();
            var win32Tree = WindowFinder.DumpWin32Tree(inspectHandle, depth);
            Console.Write(win32Tree);
        }
        else if (filter != null)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── UIA Tree Filtered: \"{filter}\" (subtree depth={depth}) ──");
            Console.ResetColor();
            using var uia = new UiaLocator();
            if (!string.IsNullOrEmpty(uiaScopePath))
            {
                var uiaRoot = uia.Automation.FromHandle(inspectHandle);
                var scopedRoot = GrapHelper.FindUiaScope(uiaRoot, uiaScopePath);
                if (scopedRoot == null) return Error($"UIA scope not found: \"{uiaScopePath}\"");
                Console.WriteLine($"  UIA scope: \"{scopedRoot.Properties.Name.ValueOrDefault}\"");
                var tree = uia.DumpTreeFiltered(scopedRoot, filter, depth);
                Console.Write(tree);
            }
            else
            {
                var tree = uia.DumpTreeFiltered(inspectHandle, filter, depth);
                Console.Write(tree);
            }
        }
        else
        {
            using var uia = new UiaLocator();
            // Hot focus chain (always shown, depth-independent)
            var focusChain = uia.GetFocusChain(inspectHandle);
            if (!string.IsNullOrEmpty(focusChain))
                Console.Write(focusChain);

            string tree;
            if (!string.IsNullOrEmpty(uiaScopePath))
            {
                var uiaRoot = uia.Automation.FromHandle(inspectHandle);
                var scopedRoot = GrapHelper.FindUiaScope(uiaRoot, uiaScopePath);
                if (scopedRoot == null) return Error($"UIA scope not found: \"{uiaScopePath}\"");
                Console.WriteLine($"  UIA scope: \"{scopedRoot.Properties.Name.ValueOrDefault}\"");
                tree = uia.DumpTree(scopedRoot, depth);
            }
            else
            {
                tree = uia.DumpTree(inspectHandle, depth);
            }
            Console.Write(tree);

            var children = WindowFinder.GetChildren(inspectHandle);
            Console.WriteLine($"\n--- Win32 children: {children.Count} ---");
            if (children.Count > 0 && string.IsNullOrWhiteSpace(tree.Replace($"[Window] \"{matchedFormTitle ?? mainWin.Title}\"", "").Trim()))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Hint: UIA tree is empty. Try --win32 for Win32 native child windows.");
                Console.ResetColor();
            }
        }

        return 0;
    }

    /// <summary>
    /// Find a child window (MDI children + direct children) matching a grap pattern.
    /// ★ Search key: "[ClassName] Title (cid=N hwnd=XX WxH)" — class name 절대 우선.
    ///   Empty-title children도 클래스명/cid로 매칭 가능.
    ///   Examples: "#32770" → class match, "cid=4015" → control ID match, "*현재가*" → title match.
    /// </summary>
    static (IntPtr handle, string? formId, string title)? FindChildWindowByPattern(IntPtr hParent, string pattern)
    {
        // MDI 자식 먼저 탐색 (MDIClient 찾기)
        var topChildren = WindowFinder.GetChildrenZOrder(hParent);
        IntPtr hMdiClient = IntPtr.Zero;
        foreach (var ch in topChildren)
        {
            if (ch.ClassName == "MDIClient")
            {
                hMdiClient = ch.Handle;
                break;
            }
        }

        // MDI 자식 검색
        if (hMdiClient != IntPtr.Zero)
        {
            var mdiChildren = WindowFinder.GetChildrenZOrder(hMdiClient);
            foreach (var mc in mdiChildren)
            {
                var searchKey = BuildChildSearchKey(mc);
                // ★ Try title first (backward compat), then full searchKey
                if (GlobMatch(mc.Title, pattern) || GlobMatch(searchKey, pattern))
                {
                    var (formId, _) = ZoneClassifier.ParseFormTitle(mc.Title);
                    return (mc.Handle, formId, mc.Title);
                }
            }
        }

        // 직접 자식도 검색 (non-MDI 앱)
        foreach (var ch in topChildren)
        {
            var searchKey = BuildChildSearchKey(ch);
            if (GlobMatch(ch.Title, pattern) || GlobMatch(searchKey, pattern))
            {
                var (formId, _) = ZoneClassifier.ParseFormTitle(ch.Title);
                return (ch.Handle, formId, ch.Title);
            }
        }

        return null;
    }

    /// <summary>
    /// Build enriched search key for child window: "[ClassName] Title (cid=N hwnd=XX WxH)"
    /// </summary>
    static string BuildChildSearchKey(WindowInfo wi)
    {
        var r = wi.Rect;
        return $"[{wi.ClassName}] {wi.Title} (cid={wi.ControlId} hwnd={wi.Handle:X8} {r.Width}x{r.Height})";
    }

    /// <summary>
    /// Simple glob match: * = any chars, ? = one char, ** = same as * for title matching.
    /// Pattern can be substring if no wildcards.
    /// </summary>
    static bool GlobMatch(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern)) return false;
        // PatternMatcher.Create does substring matching for all modes
        return PatternMatcher.Create(pattern).IsMatch(text);
    }

    /// <summary>
    /// Show available child windows when pattern match fails.
    /// </summary>
    static void ShowAvailableChildren(IntPtr hParent)
    {
        var topChildren = WindowFinder.GetChildrenZOrder(hParent);
        IntPtr hMdiClient = IntPtr.Zero;
        foreach (var ch in topChildren)
        {
            if (ch.ClassName == "MDIClient") { hMdiClient = ch.Handle; break; }
        }

        var candidates = new List<string>();
        if (hMdiClient != IntPtr.Zero)
        {
            var mdiChildren = WindowFinder.GetChildrenZOrder(hMdiClient);
            foreach (var mc in mdiChildren)
            {
                // ★ Show enriched search key (empty-title children visible too)
                candidates.Add(BuildChildSearchKey(mc));
            }
        }
        else
        {
            // Non-MDI: show direct children
            foreach (var ch in topChildren)
            {
                if (ch.ClassName == "IME" || ch.Rect.Width <= 0) continue;
                candidates.Add(BuildChildSearchKey(ch));
            }
        }
        if (candidates.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n사용 가능한 자식 ({candidates.Count}개):");
            foreach (var c in candidates.Take(20))
                Console.WriteLine($"  · {c}");
            if (candidates.Count > 20)
                Console.WriteLine($"  ... +{candidates.Count - 20}개");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Broadcast knowhow for the inspect target.
    /// matchedFormId != null → 해당 폼 폴더의 knowhow.md만 방송
    /// matchedFormId == null → 프로필 레벨 knowhow.md만 방송 (form 노하우는 생략)
    /// 엑빌경로(A11Y) = profiles/{name}_exp/ — 플랫폼 무관, 프로필 기반
    /// 운영경로(OS)  = experience/{process}/{class}/ — OS 종속 (Win32 class)
    /// </summary>
    static void BroadcastInspectKnowhow(IntPtr mainHandle, string mainClassName, string? matchedFormId, string? matchedFormTitle = null)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(mainHandle, out uint inspPid);
            var procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)inspPid).ProcessName; } catch { }

            // ── 경로 해석 ──
            var profileStore = new ProfileStore();
            var profileMatch = profileStore.FindByMatch(mainClassName, procName);

            // A11Y 경로 (프로필 기반)
            string? a11yDir = null;
            if (profileMatch != null)
            {
                a11yDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                if (!Directory.Exists(a11yDir)) a11yDir = null;
            }

            // OS 경로 (process/class 기반)
            string? osDir = null;
            if (!string.IsNullOrEmpty(procName))
            {
                var safeProc = SanitizePathToken(procName);
                var safeClass = SanitizePathToken(mainClassName);
                osDir = Path.Combine(DataDir, "experience", safeProc, safeClass);
                if (!Directory.Exists(osDir)) osDir = null;
            }

            // 경로 표시
            if (a11yDir != null || osDir != null)
            {
                if (a11yDir != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("  [A11Y] ");
                    Console.ResetColor();
                    Console.WriteLine(Path.GetRelativePath(Path.GetDirectoryName(profileStore.ProfileDir)!, a11yDir));
                }
                if (osDir != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("  [OS]   ");
                    Console.ResetColor();
                    Console.WriteLine(Path.GetRelativePath(DataDir, osDir));

                    // Recent captures (latest 3)
                    var captures = Directory.GetFiles(osDir, "capture_*.png")
                        .OrderByDescending(File.GetLastWriteTime).Take(3).ToArray();
                    if (captures.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("  [CAP]  ");
                        Console.ResetColor();
                        Console.WriteLine(string.Join("  ", captures.Select(Path.GetFileName)));
                    }

                    // Recent info files: non-PNG, latest 5 by mtime (filename only)
                    var infoFiles = Directory.GetFiles(osDir)
                        .Where(f => !Path.GetExtension(f).Equals(".png", StringComparison.OrdinalIgnoreCase))
                        .OrderByDescending(File.GetLastWriteTime).Take(5).ToArray();
                    if (infoFiles.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("  [INFO] ");
                        Console.ResetColor();
                        Console.WriteLine(string.Join("  ", infoFiles.Select(Path.GetFileName)));
                    }
                }
            }

            if (a11yDir == null && osDir == null) return;

            // ── A11Y 노하우 방송 ──
            if (a11yDir != null)
            {
                // Profile-level knowhow
                var profileKh = Path.Combine(a11yDir, "knowhow.md");
                if (File.Exists(profileKh)) ShowKnowhowBroadcast(profileKh, "KNOWHOW:A11Y");

                if (!string.IsNullOrEmpty(matchedFormId))
                {
                    var formDir = Path.Combine(a11yDir, $"form_{matchedFormId}");
                    if (Directory.Exists(formDir))
                    {
                        var kh = Path.Combine(formDir, "knowhow.md");
                        if (File.Exists(kh))
                        {
                            ShowKnowhowBroadcast(kh, "KNOWHOW:A11Y");
                        }
                        else
                        {
                            TryGenerateKnowhowTemplate(formDir, matchedFormId, a11yDir, matchedFormTitle);
                            if (File.Exists(kh)) ShowKnowhowBroadcast(kh, "KNOWHOW:A11Y");
                        }

                        // Form-level action knowhow files (knowhow-*.md) — filename only
                        foreach (var khExtra in Directory.GetFiles(formDir, "knowhow-*.md").Take(5))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"  [KNOWHOW:A11Y] + {Path.GetFileName(khExtra)}");
                            Console.ResetColor();
                        }

                        var treeDir = Path.Combine(formDir, "tree");
                        if (Directory.Exists(treeDir))
                        {
                            foreach (var khFile in Directory.GetFiles(treeDir, "knowhow*.md", SearchOption.AllDirectories).Take(5))
                                ShowKnowhowBroadcast(khFile, "KNOWHOW:A11Y");
                        }
                    }
                }
                else
                {
                    // matchedFormId == null → 프로필 레벨 knowhow.md는 이미 위에서 방송됨
                    // form_* 폴더 무차별 방송은 노이즈 유발 → 생략
                    // (특정 폼 작업 시 inspect/cond-add 등에서 formId 명시하면 해당 폼만 방송)
                }
            }

            // ── OS 노하우 방송 ──
            if (osDir != null)
            {
                var osKh = Path.Combine(osDir, "knowhow.md");
                if (File.Exists(osKh)) ShowKnowhowBroadcast(osKh, "KNOWHOW:OS");
            }
        }
        catch { /* best-effort */ }
    }

    // SanitizePathToken is defined in SnapshotCommand.cs (same partial class)

    /// <summary>
    /// 노하우 없는 폼 폴더에 기본 템플릿 자동 생성.
    /// 첫 문단: 개요 (폼 이름 + form.json 요약)
    /// 둘째 문단: UIA 접근 가능/불가 컨트롤 분류
    /// ExperienceDb form_{id}.json에서 컨트롤 목록 추출.
    /// </summary>
    static void TryGenerateKnowhowTemplate(string formDir, string formId, string expDir, string? mdiTitle = null)
    {
        try
        {
            var khPath = Path.Combine(formDir, "knowhow.md");
            if (File.Exists(khPath)) return; // already exists

            // formTitle: MDI child 타이틀에서 추출 (예: "[0600] 키움종합차트" → "키움종합차트")
            var formJson = Path.Combine(expDir, $"form_{formId}.json");
            string formTitle = "";
            if (!string.IsNullOrEmpty(mdiTitle))
            {
                // "[0600] 키움종합차트" → "키움종합차트", "[0150] 조건검색" → "조건검색"
                formTitle = System.Text.RegularExpressions.Regex.Replace(mdiTitle, @"^\[\d+\]\s*", "").Trim();
                // 뒤에 부가정보 제거: "키움현재가 (통합)" → 그대로 유지 (정보성)
            }
            var uiaAccessible = new List<string>();
            var ownerDrawn = new List<string>();

            if (File.Exists(formJson))
            {
                try
                {
                    var json = File.ReadAllText(formJson);
                    var doc = System.Text.Json.JsonDocument.Parse(json);

                    // FormTitle 추출 (mdiTitle이 없을 때만)
                    if (string.IsNullOrEmpty(formTitle) && doc.RootElement.TryGetProperty("FormTitle", out var titleEl))
                    {
                        var t = titleEl.GetString();
                        if (!string.IsNullOrEmpty(t))
                            formTitle = System.Text.RegularExpressions.Regex.Replace(t, @"^\[\d+\]\s*", "").Trim();
                    }

                    // Controls 맵에서 UIA 패턴 분류
                    if (doc.RootElement.TryGetProperty("Controls", out var controls))
                    {
                        foreach (var ctrl in controls.EnumerateObject())
                        {
                            var cid = ctrl.Name;
                            var c = ctrl.Value;
                            var name = "";
                            var role = "";
                            var patterns = "";

                            if (c.TryGetProperty("Name", out var nameEl)) name = nameEl.GetString() ?? "";
                            if (c.TryGetProperty("Role", out var roleEl)) role = roleEl.GetString() ?? "";
                            if (c.TryGetProperty("Patterns", out var patEl)) patterns = patEl.GetString() ?? "";

                            // Invoke/Value/Toggle/SelectionItem = UIA 접근 가능
                            bool hasUiaAction = patterns.Contains("Invoke") || patterns.Contains("Value")
                                || patterns.Contains("Toggle") || patterns.Contains("SelectionItem")
                                || patterns.Contains("ExpandCollapse");

                            var desc = !string.IsNullOrEmpty(name) ? $"{role} \"{name}\" (cid={cid})" : $"{role} (cid={cid})";
                            if (!string.IsNullOrEmpty(patterns)) desc += $" [{patterns}]";

                            if (hasUiaAction)
                                uiaAccessible.Add($"- {desc}");
                            else if (role == "Pane" || role == "Custom" || role == "List")
                                ownerDrawn.Add($"- {desc}");
                        }
                    }
                }
                catch { /* parse error, still generate minimal template */ }
            }

            // 템플릿 생성 — 첫 문단은 ShowKnowhowBroadcast에서 방송됨!
            var sb = new System.Text.StringBuilder();
            var headerTitle = !string.IsNullOrEmpty(formTitle) ? $"{formTitle} [{formId}]" : $"[{formId}]";
            sb.AppendLine($"## {headerTitle} 자동화 노하우");
            sb.AppendLine($"이 폼의 자동화 노하우를 여기에 기록해주세요! 첫 문단(이 줄)이 inspect 시 [KNOWHOW]로 방송됩니다. 폼의 핵심 특성, Focusless 가능 여부, 주의사항 등을 한 줄 개요로 정리하면 미래의 클롣이 감사합니다.");
            sb.AppendLine();

            if (uiaAccessible.Count > 0 || ownerDrawn.Count > 0)
            {
                if (uiaAccessible.Count > 0)
                {
                    sb.AppendLine($"### UIA 접근 가능 (Focusless) — {uiaAccessible.Count}개");
                    foreach (var item in uiaAccessible.Take(15))
                        sb.AppendLine(item);
                    if (uiaAccessible.Count > 15)
                        sb.AppendLine($"- ... +{uiaAccessible.Count - 15}개");
                    sb.AppendLine();
                }

                if (ownerDrawn.Count > 0)
                {
                    sb.AppendLine($"### UIA 접근 불가 (Owner-drawn) — {ownerDrawn.Count}개");
                    foreach (var item in ownerDrawn.Take(10))
                        sb.AppendLine(item);
                    if (ownerDrawn.Count > 10)
                        sb.AppendLine($"- ... +{ownerDrawn.Count - 10}개");
                    sb.AppendLine();
                }
            }

            File.WriteAllText(khPath, sb.ToString(), System.Text.Encoding.UTF8);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"  [KNOWHOW] 기본 템플릿 자동 생성: {khPath}");
            Console.ResetColor();
        }
        catch { /* best-effort */ }
    }

    // ── focus ─────────────────────────────────────────────────

    static int FocusCommand(string[] args)
    {
        int depth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 6;
        int delay = int.TryParse(GetArgValue(args, "--delay"), out var dl) ? dl : 3;
        bool showWin32 = args.Contains("--win32");
        bool interactive = args.Contains("--buttons") || args.Contains("-b");
        string? titleFilter = GetArgValue(args, "--title");

        IntPtr hWnd;

        if (titleFilter != null)
        {
            // Direct title search — no delay needed
            var windows = WindowFinder.FindByTitle(titleFilter);
            if (windows.Count == 0)
                return Error($"No window found matching: \"{titleFilter}\"");
            hWnd = windows[0].Handle;
        }
        else
        {
            // Countdown to let user focus the target window
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = delay; i > 0; i--)
            {
                Console.Write($"\r  Focus target window... {i}s ");
                Thread.Sleep(1000);
            }
            Console.ResetColor();
            Console.WriteLine("\r" + new string(' ', 40));

            // Get foreground window
            hWnd = NativeMethods.GetForegroundWindow();
        }

        if (hWnd == IntPtr.Zero)
            return Error("No foreground window found");

        var win = WindowInfo.FromHwnd(hWnd);

        // Process info
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
        string procName = "(unknown)";
        try { procName = Process.GetProcessById((int)pid).ProcessName; } catch { }

        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  WKAppBot Focus — Active Window Inspector                 ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("  ■ Window Info");
        Console.ResetColor();
        Console.WriteLine($"    Title     : \"{win.Title}\"");
        Console.WriteLine($"    Class     : {win.ClassName}");
        Console.WriteLine($"    HWND      : 0x{hWnd:X8}");
        Console.WriteLine($"    PID       : {pid} ({procName})");
        Console.WriteLine($"    Rect      : ({win.Rect.Left},{win.Rect.Top}) {win.Rect.Width}x{win.Rect.Height}");

        // Check for MDI
        var hMdi = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi != IntPtr.Zero)
        {
            int mdiCount = WindowFinder.CountMDIChildren(hWnd);
            var hActive = WindowFinder.GetActiveMDIChild(hWnd);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"    MDI       : Yes ({mdiCount} children, active=0x{hActive:X8})");
            Console.ResetColor();
        }

        Console.WriteLine();

        // Win32 direct children summary
        var children = WindowFinder.GetChildren(hWnd);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  ■ Win32 Children: {children.Count}");
        Console.ResetColor();

        if (showWin32 && children.Count > 0)
        {
            foreach (var child in children.Take(50))
            {
                var vis = NativeMethods.IsWindowVisible(child.Handle) ? "" : " [hidden]";
                Console.WriteLine($"    0x{child.Handle:X8} \"{child.Title}\" ({child.ClassName}) cid={child.ControlId}{vis}");
            }
            if (children.Count > 50)
                Console.WriteLine($"    ... +{children.Count - 50} more");
        }

        Console.WriteLine();

        // UIA tree
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  ■ UI Automation Tree (depth={depth})");
        Console.ResetColor();

        using var uia = new UiaLocator();

        // Hot focus chain (always shown, depth-independent)
        var focusChain = uia.GetFocusChain(hWnd);
        if (!string.IsNullOrEmpty(focusChain))
            Console.Write(focusChain);

        var tree = uia.DumpTree(hWnd, depth);

        // Count interactive elements for summary
        int buttonCount = 0, editCount = 0, textCount = 0, listCount = 0, otherCount = 0;
        var interactiveElements = new List<string>();

        foreach (var line in tree.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            if (line.Contains("[Button]"))
            {
                buttonCount++;
                // Extract interesting buttons (with aid or short name)
                var aidStart = line.IndexOf("aid=\"") + 5;
                var aidEnd = line.IndexOf("\"", aidStart);
                var aid = aidStart > 4 && aidEnd > aidStart ? line.Substring(aidStart, aidEnd - aidStart) : "";

                var nameStart = line.IndexOf('"') + 1;
                var nameEnd = line.IndexOf('"', nameStart);
                var name = nameStart > 0 && nameEnd > nameStart ? line.Substring(nameStart, nameEnd - nameStart) : "";

                if (!string.IsNullOrEmpty(aid))
                    interactiveElements.Add($"    🔘 \"{name}\" aid=\"{aid}\"");
                else if (!string.IsNullOrEmpty(name) && name != "(err)" && name != "(null)")
                    interactiveElements.Add($"    🔘 \"{name}\"");
            }
            else if (line.Contains("[Edit]")) editCount++;
            else if (line.Contains("[Text]")) textCount++;
            else if (line.Contains("[List]") || line.Contains("[DataGrid]") || line.Contains("[Table]")) listCount++;
            else otherCount++;
        }

        // Summary of interactive elements
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  ■ Element Summary");
        Console.ResetColor();
        Console.WriteLine($"    Buttons: {buttonCount}  |  Edits: {editCount}  |  Texts: {textCount}  |  Lists: {listCount}  |  Other: {otherCount}");

        if (interactive && interactiveElements.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ■ Clickable Buttons ({interactiveElements.Count}):");
            Console.ResetColor();
            foreach (var el in interactiveElements.Take(40))
                Console.WriteLine(el);
            if (interactiveElements.Count > 40)
                Console.WriteLine($"    ... +{interactiveElements.Count - 40} more");
        }

        // Full tree output (optional, always print)
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"\n  ── Full UIA Tree ──");
        Console.ResetColor();
        Console.Write(tree);

        Console.WriteLine();
        return 0;
    }

    // ── watch ──────────────────────────────────────────────────

    static int WatchCommand(string[] args)
    {
        int intervalMs = int.TryParse(GetArgValue(args, "--interval"), out var iv) ? iv : 150;
        int durationSec = int.TryParse(GetArgValue(args, "--duration"), out var dur) ? dur : 0;
        bool showWin32 = args.Contains("--win32");
        bool liveMode = args.Contains("--live");  // single-line overwrite mode
        string? saveFile = GetArgValue(args, "--save");

        // Header
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  WKAppBot Watch — Real-time Element Tracker               ║");
        if (durationSec > 0)
            Console.WriteLine($"║  Tracking for {durationSec}s. Move mouse over UI elements.    ║");
        else
            Console.WriteLine("║  Move mouse over UI elements. Press Ctrl+C to stop.     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        using var uia = new UiaLocator();
        string lastElemKey = "";       // element identity (type+aid+name) — detect real element change
        var logEntries = new List<string>();
        var stopwatch = Stopwatch.StartNew();
        bool running = true;
        int changeCount = 0;
        var seenElements = new HashSet<string>();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;  // don't kill immediately — let cleanup run
            running = false;
        };

        // Also monitor stdin EOF (Ctrl+Z on Windows) in a background thread
        // Only when stdin is a real console (not redirected/piped)
        bool stdinIsConsole;
        try { stdinIsConsole = !Console.IsInputRedirected; } catch { stdinIsConsole = false; }

        if (stdinIsConsole)
        {
            var eofThread = new Thread(() =>
            {
                try
                {
                    while (running)
                    {
                        // Console.In.Peek() returns -1 at EOF (Ctrl+Z + Enter)
                        if (Console.In.Peek() == -1) { running = false; break; }
                        Thread.Sleep(200);
                    }
                }
                catch { /* stdin not available */ }
            }) { IsBackground = true, Name = "EofWatcher" };
            eofThread.Start();
        }

        // Print column header for scroll mode
        if (!liveMode)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Time         Pos          Element");
            Console.WriteLine("  ──────────── ──────────── ─────────────────────────────────────────");
            Console.ResetColor();
        }

        int lastPtX = -1, lastPtY = -1;
        bool lineHasContent = false;  // tracks if current \r line has content

        while (running)
        {
            // Check duration limit
            if (durationSec > 0 && stopwatch.Elapsed.TotalSeconds >= durationSec)
                break;

            try
            {
                NativeMethods.GetCursorPos(out var pt);
                var hWndTop = NativeMethods.WindowFromPoint(pt);
                var topClass = WindowFinder.GetClassName(hWndTop);

                // Skip transparent overlays (Chrome extensions, screen capture tools)
                var hWndUnder = NativeMethods.FindRealWindowFromPoint(pt, hWndTop, topClass);
                var win32Class = (hWndUnder == hWndTop) ? topClass : WindowFinder.GetClassName(hWndUnder);
                int ctrlId = NativeMethods.GetDlgCtrlID(hWndUnder);

                // Get UIA element at point
                ElementAtPointInfo? elemInfo;
                bool overlayDetected = (hWndUnder != hWndTop);

                if (overlayDetected)
                {
                    // Win32 Z-order detected overlay → use real window's UIA tree
                    elemInfo = uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndUnder);
                }
                else
                {
                    // Normal UIA FromPoint first
                    elemInfo = uia.GetElementAtPoint(pt.X, pt.Y);

                    // Check if UIA returned a known overlay element (e.g. Chrome extension BTN_BKGRND)
                    // Even if Win32 didn't detect it (non-transparent overlay)
                    if (elemInfo != null && IsOverlayElement(elemInfo.AutomationId, win32Class))
                    {
                        overlayDetected = true;
                        // Find real window behind this one
                        var realHwnd = NativeMethods.GetWindow(hWndUnder, NativeMethods.GW_HWNDNEXT);
                        int skip = 0;
                        while (realHwnd != IntPtr.Zero && skip < 30)
                        {
                            skip++;
                            if (NativeMethods.IsWindowVisible(realHwnd))
                            {
                                NativeMethods.GetWindowRect(realHwnd, out var rc);
                                if (NativeMethods.PtInRect(ref rc, pt))
                                {
                                    hWndUnder = realHwnd;
                                    win32Class = WindowFinder.GetClassName(realHwnd);
                                    ctrlId = NativeMethods.GetDlgCtrlID(realHwnd);
                                    elemInfo = uia.GetElementAtPointInWindow(pt.X, pt.Y, realHwnd);
                                    break;
                                }
                            }
                            realHwnd = NativeMethods.GetWindow(realHwnd, NativeMethods.GW_HWNDNEXT);
                        }
                    }
                }

                // Build element identity key (detect element change, not just pixel change)
                string elemKey;
                if (elemInfo != null)
                    elemKey = $"{elemInfo.ControlType}|{elemInfo.AutomationId}|{elemInfo.Name}|0x{hWndUnder:X8}";
                else
                    elemKey = $"?|0x{hWndUnder:X8}";

                bool elementChanged = (elemKey != lastElemKey);
                bool posChanged = (pt.X != lastPtX || pt.Y != lastPtY);

                if (elementChanged || posChanged)
                {
                    lastPtX = pt.X;
                    lastPtY = pt.Y;
                    var ts = DateTime.Now.ToString("HH:mm:ss.fff");

                    if (elementChanged)
                    {
                        // Element changed → finalize current line, start new one
                        lastElemKey = elemKey;
                        changeCount++;
                        seenElements.Add(elemKey);

                        // Build combined hierarchy path
                        string? hierarchyPath = null;
                        try
                        {
                            hierarchyPath = uia.GetHierarchyPath(pt.X, pt.Y, hWndUnder, useWindowTree: overlayDetected);
                        }
                        catch { }

                        // Collect log entry
                        var logLine = BuildPlainLine(ts, pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                        if (hierarchyPath != null) logLine = $"{hierarchyPath}  " + logLine;
                        if (overlayDetected)
                            logLine += $"  !! overlay: 0x{hWndTop:X8} {topClass}";
                        logEntries.Add(logLine);

                        if (liveMode)
                        {
                            ClearCurrentLine();
                            WritePosAndElement(pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                            if (overlayDetected) WriteOverlayTag(hWndTop, topClass);
                        }
                        else
                        {
                            if (lineHasContent)
                                Console.WriteLine();

                            // Line 1: combined [ClassPath] NamePath
                            if (!string.IsNullOrEmpty(hierarchyPath))
                            {
                                Console.Write("  ");
                                var bracketEnd = hierarchyPath.IndexOf("] ");
                                if (bracketEnd >= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write(hierarchyPath[..(bracketEnd + 1)]);
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    Console.Write(hierarchyPath[(bracketEnd + 1)..]);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    Console.Write(hierarchyPath);
                                }
                                if (overlayDetected) WriteOverlayTag(hWndTop, topClass);
                                Console.WriteLine();
                            }

                            // Line 2: timestamp + coords + element detail
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"  {ts}  ");

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write($"({pt.X,5},{pt.Y,5})  ");

                            WritePosAndElement(null, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                            lineHasContent = true;
                        }
                    }
                    else if (posChanged && !liveMode && lineHasContent)
                    {
                        // Same element, mouse moved → overwrite position on current line with \r
                        // We re-render the whole line at the same position
                        ClearCurrentLine();

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"  {ts}  ");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"({pt.X,5},{pt.Y,5})  ");

                        WritePosAndElement(null, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                    }
                    else if (posChanged && liveMode)
                    {
                        ClearCurrentLine();
                        WritePosAndElement(pt, elemInfo, hWndUnder, win32Class, ctrlId, showWin32);
                    }
                }

                Thread.Sleep(intervalMs);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Error: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(intervalMs);
            }
        }

        // Finalize last line
        if (lineHasContent) Console.WriteLine();

        // ── Summary ──
        stopwatch.Stop();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  ────────────────────────────────────────────────");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"  Duration    : {stopwatch.Elapsed:mm\\:ss\\.f}");
        Console.WriteLine($"  Changes     : {changeCount}");
        Console.WriteLine($"  Unique elems: {seenElements.Count}");
        Console.ResetColor();

        // Save log
        if (logEntries.Count > 0)
        {
            var logFile = saveFile ?? $"watch_{DateTime.Now:yyyyMMdd_HHmmss}.log";
            try
            {
                File.WriteAllLines(logFile, logEntries);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  Saved       : {logFile} ({logEntries.Count} entries)");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"  Save failed : {ex.Message}");
                Console.ResetColor();
            }
        }

        Console.WriteLine();
        return 0;
    }

    /// <summary>
    /// Write colorized element info. If pos is provided, also prints coordinates.
    /// </summary>
    private static void WritePosAndElement(
        POINT? pos,
        ElementAtPointInfo? elemInfo,
        IntPtr hWnd, string win32Class, int ctrlId, bool showWin32)
    {
        if (pos.HasValue)
        {
            Console.Write($"  ({pos.Value.X,5},{pos.Value.Y,5}) ");
        }

        if (elemInfo != null)
        {
            WriteControlType(elemInfo.ControlType);

            if (!string.IsNullOrEmpty(elemInfo.Name))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" \"{Truncate(elemInfo.Name, 30)}\"");
            }
            if (!string.IsNullOrEmpty(elemInfo.AutomationId))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($" aid=\"{elemInfo.AutomationId}\"");
            }
            if (!string.IsNullOrEmpty(elemInfo.Value))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" val=\"{Truncate(elemInfo.Value, 20)}\"");
            }
            if (elemInfo.Patterns.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($" ({string.Join(",", elemInfo.Patterns)})");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[?]");
        }

        if (showWin32)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  | 0x{hWnd:X8} {win32Class}");
            if (ctrlId != 0) Console.Write($" cid={ctrlId}");
        }

        Console.ResetColor();
    }

    /// <summary>
    /// Build a plain-text log line (no colors) for file saving.
    /// </summary>
    private static string BuildPlainLine(
        string ts, POINT pt,
        ElementAtPointInfo? elemInfo,
        IntPtr hWnd, string win32Class, int ctrlId, bool showWin32)
    {
        var sb = new StringBuilder();
        sb.Append($"[{ts}] ({pt.X,5},{pt.Y,5}) ");

        if (elemInfo != null)
        {
            sb.Append($"[{elemInfo.ControlType}]");
            if (!string.IsNullOrEmpty(elemInfo.Name))
                sb.Append($" \"{elemInfo.Name}\"");
            if (!string.IsNullOrEmpty(elemInfo.AutomationId))
                sb.Append($" aid=\"{elemInfo.AutomationId}\"");
            if (!string.IsNullOrEmpty(elemInfo.Value))
                sb.Append($" val=\"{elemInfo.Value}\"");
            if (elemInfo.Patterns.Count > 0)
                sb.Append($" ({string.Join(",", elemInfo.Patterns)})");
        }
        else
        {
            sb.Append("[?]");
        }

        if (showWin32)
        {
            sb.Append($"  | 0x{hWnd:X8} {win32Class}");
            if (ctrlId != 0) sb.Append($" cid={ctrlId}");
        }

        return sb.ToString();
    }

    private static void WriteControlType(string ct)
    {
        Console.ForegroundColor = ct switch
        {
            "Button" => ConsoleColor.Yellow,
            "Edit" => ConsoleColor.Cyan,
            "Text" => ConsoleColor.Gray,
            "List" or "ListItem" or "DataGrid" or "DataItem" => ConsoleColor.Magenta,
            "ComboBox" => ConsoleColor.Blue,
            "CheckBox" or "RadioButton" => ConsoleColor.DarkYellow,
            "Tab" or "TabItem" => ConsoleColor.DarkCyan,
            "Menu" or "MenuItem" or "MenuBar" => ConsoleColor.DarkGreen,
            "Window" or "Pane" => ConsoleColor.DarkGray,
            _ => ConsoleColor.White
        };
        Console.Write($"[{ct}]");
        Console.ResetColor();
    }

    /// <summary>
    /// Check if a UIA element is a known overlay (Chrome extension background, etc.)
    /// </summary>
    private static bool IsOverlayElement(string? automationId, string className)
    {
        // Chrome extension overlays
        if (className.StartsWith("Chrome_WidgetWin") &&
            (automationId == "BTN_BKGRND" || automationId == ""))
            return true;
        return false;
    }

    private static void WriteOverlayTag(IntPtr overlayHwnd, string overlayClass)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"  !! {overlayClass}");
        Console.ResetColor();
    }

    private static void ClearCurrentLine()
    {
        int w;
        try { w = Console.WindowWidth; } catch { w = 120; }
        Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");
    }

    private static string Truncate(string s, int max) =>
        s.Length <= max ? s : s[..max] + "...";

    // ── capture ────────────────────────────────────────────────

    static int CaptureCommand(string[] args)
    {
        if (args.Length == 0)
            return Error(@"Usage: appbot capture <window-title> [-o output.png] [--form <form-id>] [--no-learn]
  --form <id>: Capture a specific MDI child form (brings it to front first).
  --no-learn:  Skip per-control experience DB learning.");

        string title = args[0];
        string? output = GetArgValue(args, "-o");
        string? formId = GetArgValue(args, "--form");
        bool skipLearn = args.Any(a => a.Equals("--no-learn", StringComparison.OrdinalIgnoreCase));

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.WriteLine($"No window found matching: \"{title}\"");
            return 1;
        }

        var win = windows[0];
        AppScanResult? scanResult = null;

        // Default save location: experience/{proc}/{class}/ (same dir as inspect/scan data)
        if (output == null)
        {
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint capPid);
            string? capProc = null;
            try { capProc = System.Diagnostics.Process.GetProcessById((int)capPid).ProcessName; } catch { }
            var safeProc = SanitizePathToken(capProc ?? "unknown");
            var safeClass = SanitizePathToken(win.ClassName);
            var expDir = Path.Combine(DataDir, "experience", safeProc, safeClass);
            Directory.CreateDirectory(expDir);
            output = Path.Combine(expDir, $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        }

        if (formId != null)
        {
            // Find MDI child form and bring to front before capture
            scanResult = AppScanner.Scan(win.Handle);
            var form = scanResult.Forms.FirstOrDefault(f =>
                f.FormId != null && f.FormId.Contains(formId, StringComparison.OrdinalIgnoreCase));
            if (form == null)
            {
                Console.WriteLine($"Form [{formId}] not found in \"{win.Title}\"");
                Console.WriteLine($"Available forms:");
                foreach (var f in scanResult.Forms)
                    Console.WriteLine($"  [{f.FormId}] {f.FormName}");
                return 1;
            }

            Console.WriteLine($"Capturing form: [{form.FormId}] {form.FormName}");

            // Bring MDI child to front using BringWindowToTop + SetWindowPos(TOPMOST)
            NativeMethods.BringWindowToTop(form.Handle);
            // Also use SetWindowPos SWP_NOACTIVATE to avoid stealing focus
            NativeMethods.SetWindowPos(form.Handle, NativeMethods.HWND_TOP,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
            Thread.Sleep(200); // let repaint happen

            // Capture the MDI child window directly
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(form.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {Path.GetFullPath(output)} ({bmp.Width}x{bmp.Height})");
        }
        else
        {
            Console.WriteLine($"Capturing: {win}");
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            WKAppBot.Win32.Input.ScreenCapture.SaveToFile(bmp, output);
            Console.WriteLine($"Saved: {Path.GetFullPath(output)} ({bmp.Width}x{bmp.Height})");
        }

        // Per-control experience learning (auto when profile exists)
        if (!skipLearn)
        {
            try
            {
                NativeMethods.GetWindowThreadProcessId(win.Handle, out uint capPid);
                string? capProcName = null;
                try { capProcName = System.Diagnostics.Process.GetProcessById((int)capPid).ProcessName; } catch { }

                var profileStore = new ProfileStore();
                var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                    ?? (!string.IsNullOrEmpty(capProcName) ? profileStore.FindByMatch("", capProcName) : null);

                if (profileMatch != null)
                {
                    var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                    var expDb = new ExperienceDb(expDir);
                    scanResult ??= AppScanner.Scan(win.Handle);
                    var (forms, controls, screenshots) = AppScanner.QuickTouchControls(scanResult, expDb);

                    if (controls > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[CAPTURE] 경험DB 학습: {forms} forms, {controls} controls, {screenshots} new screenshots (profile={profileMatch.Value.name})");
                        Console.ResetColor();
                    }
                }
            }
            catch { /* best-effort */ }
        }

        return 0;
    }

    // ── ocr ────────────────────────────────────────────────────

    static int OcrCommand(string[] args)
    {
        if (args.Length == 0)
            return Error(@"Usage: wkappbot ocr <window-title|image.png> [--save] [-o output.txt]
  Runs OCR on a window screenshot or image file and prints all recognized text with coordinates.
  --save: Save annotated screenshot with OCR bounding boxes");

        string target = args[0];
        bool save = args.Contains("--save");
        string? outputFile = GetArgValue(args, "-o");

        System.Drawing.Bitmap screenshot;
        string sourceDesc;

        // Check if target is an image file
        if (File.Exists(target) && (target.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            || target.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)))
        {
            screenshot = new System.Drawing.Bitmap(target);
            sourceDesc = Path.GetFileName(target);
        }
        else
        {
            // Treat as window title
            var windows = WindowFinder.FindByTitle(target);
            if (windows.Count == 0)
                return Error($"Window not found: \"{target}\"");

            var win = windows[0];
            Console.WriteLine($"Capturing: {win}");
            screenshot = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(win.Handle);
            sourceDesc = win.Title;
        }

        using (screenshot)
        {
            Console.WriteLine($"Image: {screenshot.Width}x{screenshot.Height} — {sourceDesc}");
            Console.WriteLine();

            // Run OCR
            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
            var result = ocr.RecognizeAll(screenshot).GetAwaiter().GetResult();

            Console.WriteLine($"── OCR Results ({result.Words.Count} words) ──────────────────────");
            Console.WriteLine();

            // Print full text first
            if (!string.IsNullOrWhiteSpace(result.FullText))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[Full Text]");
                Console.ResetColor();
                // Limit to first 2000 chars
                var text = result.FullText.Length > 2000
                    ? result.FullText[..2000] + "..."
                    : result.FullText;
                Console.WriteLine(text);
                Console.WriteLine();
            }

            // Print words with coordinates (grouped by Y position → lines)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[Words with Coordinates]");
            Console.ResetColor();

            // Group words into lines by Y proximity (within 8px)
            var sortedWords = result.Words.OrderBy(w => w.Y).ThenBy(w => w.X).ToList();
            int lastLineY = -100;
            var lineWords = new List<WKAppBot.Vision.OcrWord>();

            foreach (var word in sortedWords)
            {
                if (Math.Abs(word.Y - lastLineY) > 8 && lineWords.Count > 0)
                {
                    // Print previous line
                    PrintOcrLine(lineWords);
                    lineWords.Clear();
                }
                lineWords.Add(word);
                lastLineY = word.Y;
            }
            if (lineWords.Count > 0) PrintOcrLine(lineWords);

            Console.WriteLine();
            Console.WriteLine($"Total: {result.Words.Count} words");

            // Save output
            if (outputFile != null)
            {
                var dir = Path.GetDirectoryName(outputFile);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(outputFile, result.FullText ?? "");
                Console.WriteLine($"Text saved: {outputFile}");
            }

            if (save)
            {
                var savePath = $"ocr_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                WKAppBot.Win32.Input.ScreenCapture.SaveToFile(screenshot, savePath);
                Console.WriteLine($"Screenshot saved: {savePath}");
            }
        }

        return 0;
    }

    static void PrintOcrLine(List<WKAppBot.Vision.OcrWord> words)
    {
        var lineText = string.Join(" ", words.Select(w => w.Text));
        var firstWord = words[0];
        Console.Write($"  Y={firstWord.Y,4} | ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(lineText);
        Console.ResetColor();
        Console.WriteLine($"  ({words.Count} words, x={firstWord.X}..{words.Last().X + words.Last().Width})");
    }

    // ── windows — list all top-level windows ────────────────────
    // Usage: wkappbot windows [--filter <pattern>] [--process <name>] [--class <name>]

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

        // Get window info, apply filters. Returns null if filtered out or noise.
        (string title, string className, string process, uint pid, int w, int h, bool visible)?
            GetWindowInfo(IntPtr hWnd)
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

            // Skip noise unless --all
            if (!showAll && string.IsNullOrEmpty(title) && !visible) return null;
            if (!showAll && w == 0 && h == 0) return null;

            // Skip wkappbot windows (Eye/Zoom overlays) — don't pollute results.
            // --all overrides: useful to identify mcp/eye/a11y processes by args (--cmd).
            if (!showAll && processName.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)) return null;

            // Apply filters — ★ standard search key with focus flags
            if (filterTitle != null)
            {
                var searchKey = WindowFinder.BuildSearchKey(hWnd, className, title, processName, w, h, focus);
                var m = PatternMatcher.Create(filterTitle);
                if (!m.IsMatch(title) && !m.IsMatch(searchKey)) return null;
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

            Console.ForegroundColor = isForeground ? ConsoleColor.Green
                : (visible && !string.IsNullOrEmpty(title)) ? ConsoleColor.White
                : ConsoleColor.DarkGray;
            Console.Write($"  {prefix}[{hWnd:X8}] ");
            Console.ForegroundColor = isForeground ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.Write($"\"{displayTitle}\"");
            Console.ResetColor();
            Console.Write($"  ({className}) {w}x{h}{vis}  [{process} pid={pid}]");
            if (isForeground) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(" ★"); Console.ResetColor(); }
            if (tags.Count > 0 || ownerHwnd != IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (tags.Count > 0) Console.Write($" [{string.Join(",", tags)}]");
                if (ownerHwnd != IntPtr.Zero) Console.Write($" owner={ownerHwnd:X}");
                Console.ResetColor();
            }
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

            // Show focus child + owned popup (foreground or filtered matches)
            if (!isChild && (isForeground || hasFilter))
                PrintFocusAndPopup(hWnd);
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
                // Show focus child (skip if focus == self — no extra info)
                if (gtiOk && gti.hwndFocus != IntPtr.Zero && gti.hwndFocus != hWnd)
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
                }
            }

            // 2. Owned popup/dialog windows (other top-levels whose owner = hWnd)
            var popups = new List<IntPtr>();
            NativeMethods.EnumWindows((h, _) =>
            {
                if (h == hWnd) return true;
                if (!NativeMethods.IsWindowVisible(h)) return true;
                var owner = NativeMethods.GetWindow(h, 4 /*GW_OWNER*/);
                if (owner == hWnd)
                    popups.Add(h);
                return true;
            }, IntPtr.Zero);

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
        int uiaMatchWindows = 0;

        // ── Hot Focus Line: focused child → parent → ... → top-level ──
        PrintHotFocusLine(fgWnd, focus, GetProcessName);

        // EnumWindows enumerates in Z-order (front to back) — no re-sort needed!
        bool hasPathSearch = filterTitle != null && (filterTitle.Contains('/') || filterTitle.Contains("**"));
        string mode = hasPathSearch ? "find path (Z-order ★=foreground)"
            : uiaDeep ? "find --deep (Z-order ★=foreground)"
            : uiaSearch ? "find (Z-order ★=foreground)"
            : deep ? "windows (deep, Z-order ★=foreground)"
            : "windows (Z-order ★=foreground)";
        Console.WriteLine($"── {mode} ──");
        Console.WriteLine();

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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"    → [{m.ControlType}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\"{m.Name}\"");
                if (!string.IsNullOrEmpty(m.AutomationId))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" aid=\"{m.AutomationId}\"");
                }
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
                }
                else
                {
                    // ── Regular search: keyword matches title OR UIA elements (OR logic) ──
                    var titleMatch = PatternMatcher.Create(filterTitle).IsMatch(r.title);

                    var uiaMatches = uiaDeep
                        ? UiaLocator.QuickSearch(hWnd, filterTitle, maxDepth: 12, maxResults: 10, maxVisited: 1500, timeoutMs: 8000)
                        : UiaLocator.QuickSearch(hWnd, filterTitle);

                    if (!titleMatch && uiaMatches.Count == 0) return true;

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
                    parentPrinted = true;
                    if (limit > 0 && totalCount >= limit) { Console.Out.Flush(); return false; }
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

        Console.WriteLine();
        string uiaNote = uiaSearch ? $", UIA matched in {uiaMatchWindows} window(s)" : "";
        string limitNote = limit > 0 ? $", --limit {limit}" : "";
        string hint = uiaSearch ? "(--deep: thorough search)" : "(--uia: accessibility search, --deep: child windows)";
        Console.WriteLine($"Total: {totalCount}{uiaNote} {hint}{limitNote}");
        return 0;
    }
}
