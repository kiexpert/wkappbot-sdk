using System.Diagnostics;
using System.Text;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Find windows by title, class name, process, or HWND.
/// All Unicode (W) functions for Korean text.
/// Supports glob patterns: * (single segment), ** (any depth), ? (single char).
/// </summary>
public static class WindowFinder
{
    // ── Grap search cache: pattern → (hwnds, timestamp) ──
    // Lazy cache: only caches what was searched. hwnd validity checked on hit.
    static readonly Dictionary<string, (List<IntPtr> hwnds, DateTime cachedAt)> _grapCache = new();
    static readonly TimeSpan _grapCacheTtl = TimeSpan.FromSeconds(5);

    /// <summary>Invalidate cache entries containing this hwnd (call after a11y action).</summary>
    public static void InvalidateCache(IntPtr hwnd)
    {
        var keysToRemove = _grapCache
            .Where(kv => kv.Value.hwnds.Contains(hwnd))
            .Select(kv => kv.Key).ToList();
        foreach (var key in keysToRemove) _grapCache.Remove(key);
    }

    /// <summary>Clear entire grap search cache.</summary>
    public static void ClearCache() => _grapCache.Clear();

    /// <summary>
    /// Find all visible top-level windows matching a grap pattern.
    /// Supports: literal substring, glob (* ? **), regex: prefix, hwnd: prefix.
    /// ★ Search key: "[ClassName] Title (processName hwnd=XXXXXXXX WxH)"
    ///   Class name 절대 우선! 제목, 프로세스명, hwnd, 크기 전부 포함.
    ///   Examples: "#32770" → finds [#32770] dialogs with empty titles
    ///             "nkrunlite" → finds all windows from that process
    ///             "영웅문" → finds [_NKHeroMainClass] 영웅문4
    ///             "hwnd:0054188E" → direct handle lookup
    /// </summary>
    public static List<WindowInfo> FindByTitle(string titlePattern)
    {
        // ── Cache check: return cached if all hwnds still alive and within TTL ──
        if (_grapCache.TryGetValue(titlePattern, out var cached)
            && DateTime.UtcNow - cached.cachedAt < _grapCacheTtl
            && cached.hwnds.All(NativeMethods.IsWindow))
        {
            return cached.hwnds.Select(WindowInfo.FromHwnd).ToList();
        }
        // ★ hwnd: prefix — direct handle lookup (no enumeration needed)
        if (titlePattern.StartsWith("hwnd:", StringComparison.OrdinalIgnoreCase))
        {
            var hexStr = titlePattern.Substring(5).Trim().TrimStart('0', 'x', 'X');
            if (IntPtr.TryParse(hexStr, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal)
                && hwndVal != IntPtr.Zero && NativeMethods.IsWindow(hwndVal))
            {
                return new List<WindowInfo> { WindowInfo.FromHwnd(hwndVal) };
            }
            return new List<WindowInfo>();
        }

        // Create() does substring matching for all modes (literal/glob/regex)
        var matcher = PatternMatcher.Create(titlePattern);

        // Cache process names by PID (avoid repeated lookups)
        var procNameCache = new Dictionary<uint, string>();

        // Snapshot focus state once for flags + priority sorting
        var focus = FocusSnapshot.CaptureNow();

        var results = new List<WindowInfo>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var title = GetWindowText(hWnd);
            var cls = GetClassName(hWnd);

            // Get process name (cached)
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (!procNameCache.TryGetValue(pid, out var procName))
            {
                try { procName = Process.GetProcessById((int)pid).ProcessName; }
                catch { procName = $"pid={pid}"; }
                procNameCache[pid] = procName;
            }

            // Get window size
            NativeMethods.GetWindowRect(hWnd, out var rect);
            var w = rect.Right - rect.Left;
            var h = rect.Bottom - rect.Top;

            // ★ Standard search key with focus flags
            var searchKey = BuildSearchKey(hWnd, cls, title, procName, w, h, focus);

            // Substring match: try title first, then full searchKey — track coverage
            if (matcher.IsMatch(title) || matcher.IsMatch(searchKey))
            {
                var info = WindowInfo.FromHwnd(hWnd);
                // Coverage = pattern literal length / matched text length (higher = more specific match)
                var patLen = titlePattern.Replace("*", "").Replace("?", "").Length;
                info.Coverage = patLen > 0 && title.Length > 0 ? (double)patLen / title.Length : 0;
                results.Add(info);
            }

            return true;
        }, IntPtr.Zero);

        // Sort by coverage descending (most specific match first), then focus priority
        if (results.Count > 1)
            results.Sort((a, b) =>
            {
                var covCmp = b.Coverage.CompareTo(a.Coverage);
                return covCmp != 0 ? covCmp : focus.GetPriority(b.Handle).CompareTo(focus.GetPriority(a.Handle));
            });

        // ── Cache results for repeat searches ──
        _grapCache[titlePattern] = (results.Select(r => r.Handle).ToList(), DateTime.UtcNow);

        return results;
    }

    // ═══════════════════════════════════════════════════════════════
    //  Focus state snapshot + search key builder (standard for all callers)
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Snapshot of current focus state for search key enrichment and priority sorting.
    /// Create once per search operation via CaptureNow().
    /// </summary>
    public class FocusSnapshot
    {
        public IntPtr ForegroundHwnd { get; init; }
        public IntPtr KeyboardHwnd { get; init; }
        public IntPtr KeyboardRootHwnd { get; init; } // pre-computed GA_ROOT
        public IntPtr MouseHwnd { get; init; }
        public IntPtr MouseRootHwnd { get; init; }    // pre-computed GA_ROOT

        public static FocusSnapshot CaptureNow()
        {
            var fg = NativeMethods.GetForegroundWindow();
            var kb = IntPtr.Zero;
            try
            {
                var gti = new NativeMethods.GUITHREADINFO
                { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
                if (NativeMethods.GetGUIThreadInfo(0, ref gti))
                    kb = gti.hwndFocus;
            }
            catch { }
            var mouse = IntPtr.Zero;
            try
            {
                NativeMethods.GetCursorPos(out var pt);
                mouse = NativeMethods.WindowFromPoint(pt);
            }
            catch { }

            // Pre-compute root ancestors once — avoids per-window GetAncestor calls
            var kbRoot = kb != IntPtr.Zero ? NativeMethods.GetAncestor(kb, NativeMethods.GA_ROOT) : IntPtr.Zero;
            var mouseRoot = mouse != IntPtr.Zero ? NativeMethods.GetAncestor(mouse, NativeMethods.GA_ROOT) : IntPtr.Zero;

            return new FocusSnapshot
            {
                ForegroundHwnd = fg,
                KeyboardHwnd = kb, KeyboardRootHwnd = kbRoot,
                MouseHwnd = mouse, MouseRootHwnd = mouseRoot
            };
        }

        /// <summary>Focus flags string for search key (e.g., " ★foreground ★keyboard").</summary>
        public string GetFlags(IntPtr hWnd)
        {
            var flags = "";
            if (hWnd == ForegroundHwnd) flags += " ★foreground";
            if (hWnd == KeyboardHwnd || hWnd == KeyboardRootHwnd) flags += " ★keyboard";
            if (hWnd == MouseHwnd || hWnd == MouseRootHwnd) flags += " ★mouse";
            return flags;
        }

        /// <summary>Priority score for sorting: keyboard=3, mouse=2, foreground=1, other=0.</summary>
        public int GetPriority(IntPtr hWnd) =>
            (hWnd == KeyboardHwnd || hWnd == KeyboardRootHwnd) ? 3 :
            (hWnd == MouseHwnd || hWnd == MouseRootHwnd) ? 2 :
            (hWnd == ForegroundHwnd) ? 1 : 0;
    }

    /// <summary>
    /// Build the standard enriched search key for a window.
    /// Format: "[ClassName] Title (processName hwnd=XXXXXXXX WxH ★flags)"
    /// Used by FindByTitle, WindowsCommand, and any grap-based search.
    /// </summary>
    public static string BuildSearchKey(IntPtr hWnd, string cls, string title,
        string procName, int w, int h, FocusSnapshot? focus = null)
    {
        var flags = focus?.GetFlags(hWnd) ?? "";
        // wkappbot.* props → "wkappbot" 딱지 + 상세 props
        var wkArgs = "";
        var isWebbot = NativeMethods.GetPropW(hWnd, "wkappbot.webbot") != IntPtr.Zero;
        var cdpPort = NativeMethods.GetPropW(hWnd, "wkappbot.cdp").ToInt32();
        if (isWebbot || cdpPort > 0)
        {
            var parts = new List<string>(3) { "wkappbot" }; // 딱지 항상 포함
            if (isWebbot) parts.Add("webbot");
            if (cdpPort > 0) parts.Add($"cdp={cdpPort}");
            wkArgs = $" {string.Join(" ", parts)}"; // IntPtr props — auto-freed on window destroy
        }
        return $"[{cls}] {title} ({procName} hwnd={hWnd:X8} {w}x{h}{flags}{wkArgs})";
    }

    /// <summary>Check if childHwnd is a descendant of a top-level parentHwnd.</summary>
    public static bool IsChildOfTopLevel(IntPtr childHwnd, IntPtr parentHwnd)
    {
        if (childHwnd == IntPtr.Zero || parentHwnd == IntPtr.Zero) return false;
        var root = NativeMethods.GetAncestor(childHwnd, NativeMethods.GA_ROOT);
        return root == parentHwnd;
    }

    /// <summary>
    /// Find top-level window by class name.
    /// </summary>
    public static List<WindowInfo> FindByClassName(string className)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var cls = GetClassName(hWnd);
            if (cls.Equals(className, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(WindowInfo.FromHwnd(hWnd));
            }
            return true;
        }, IntPtr.Zero);
        return results;
    }

    /// <summary>
    /// Find the MDI main frame of a process (has MDIClient child).
    /// Pattern from leak_test_repeat.ps1.
    /// </summary>
    public static IntPtr FindMDIMainFrame(uint processId)
    {
        IntPtr found = IntPtr.Zero;
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != processId) return true;
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            var hMdi = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "MDIClient", null);
            if (hMdi != IntPtr.Zero)
            {
                found = hWnd;
                return false; // stop enumeration
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }

    /// <summary>
    /// Find the MDI main frame by process name (e.g., "HTSRun").
    /// </summary>
    public static IntPtr FindMDIMainFrameByProcessName(string processName)
    {
        var proc = Process.GetProcessesByName(processName).FirstOrDefault();
        if (proc == null) return IntPtr.Zero;
        return FindMDIMainFrame((uint)proc.Id);
    }

    /// <summary>
    /// Enumerate child windows of a parent.
    /// </summary>
    public static List<WindowInfo> GetChildren(IntPtr hWndParent)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumChildWindows(hWndParent, (hWnd, _) =>
        {
            if (NativeMethods.GetParent(hWnd) == hWndParent)
            {
                results.Add(WindowInfo.FromHwnd(hWnd));
            }
            return true;
        }, IntPtr.Zero);
        return results;
    }

    /// <summary>
    /// Find a child window (MDI children first, then direct children) matching a pattern.
    /// Uses PatternMatcher.Create (substring matching) against title and enriched searchKey.
    /// Returns null if no match found.
    /// </summary>
    public static WindowInfo? FindChildByPattern(IntPtr hParent, string pattern)
    {
        var matcher = PatternMatcher.Create(pattern);

        // Build search key for matching
        static string SearchKey(WindowInfo wi) =>
            $"[{wi.ClassName}] {wi.Title} (cid={wi.ControlId} hwnd={wi.Handle:X8} {wi.Rect.Width}x{wi.Rect.Height})";

        // MDI children first
        var topChildren = GetChildrenZOrder(hParent);
        IntPtr hMdiClient = IntPtr.Zero;
        foreach (var ch in topChildren)
            if (ch.ClassName == "MDIClient") { hMdiClient = ch.Handle; break; }

        if (hMdiClient != IntPtr.Zero)
        {
            foreach (var mc in GetChildrenZOrder(hMdiClient))
            {
                if (matcher.IsMatch(mc.Title) || matcher.IsMatch(SearchKey(mc)))
                    return mc;
            }
        }

        // Direct children fallback
        foreach (var ch in topChildren)
        {
            if (matcher.IsMatch(ch.Title) || matcher.IsMatch(SearchKey(ch)))
                return ch;
        }

        return null;
    }

    /// <summary>
    /// Count MDI children (direct children of MDIClient).
    /// </summary>
    public static int CountMDIChildren(IntPtr hMainWnd)
    {
        var hMdi = NativeMethods.FindWindowExW(hMainWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero) return -1;

        int count = 0;
        NativeMethods.EnumChildWindows(hMdi, (hWnd, _) =>
        {
            if (NativeMethods.GetParent(hWnd) == hMdi) count++;
            return true;
        }, IntPtr.Zero);
        return count;
    }

    /// <summary>
    /// Get the active MDI child window.
    /// </summary>
    public static IntPtr GetActiveMDIChild(IntPtr hMainWnd)
    {
        var hMdi = NativeMethods.FindWindowExW(hMainWnd, IntPtr.Zero, "MDIClient", null);
        if (hMdi == IntPtr.Zero) return IntPtr.Zero;
        return NativeMethods.SendMessageW(hMdi, NativeMethods.WM_MDIGETACTIVE, IntPtr.Zero, IntPtr.Zero);
    }

    /// <summary>
    /// Build Win32 window hierarchy path from a child hwnd up to the desktop.
    /// Returns class name path like: "ApplicationFrameWindow/Windows.UI.Core.CoreWindow/..."
    /// </summary>
    public static string GetWindowHierarchyPath(IntPtr hWnd, int maxDepth = 8)
    {
        var parts = new List<string>();
        var current = hWnd;
        int depth = 0;

        while (current != IntPtr.Zero && depth < maxDepth)
        {
            var cls = GetClassName(current);
            if (string.IsNullOrEmpty(cls)) break;

            // Stop at desktop window classes
            if (cls == "Progman" || cls == "#32769") break;

            parts.Add(cls);
            current = NativeMethods.GetParent(current);
            depth++;
        }

        parts.Reverse();
        return string.Join("/", parts);
    }

    /// <summary>
    /// Build Win32 window class hierarchy by drilling DOWN from hWnd to the deepest
    /// child window at the given screen coordinate.
    /// Returns top-down path like: "ApplicationFrameWindow/Windows.UI.Core.CoreWindow/..."
    /// </summary>
    public static string GetWindowHierarchyPathAtPoint(IntPtr hWnd, int screenX, int screenY, int maxDepth = 8)
    {
        var parts = new List<string>();
        var current = hWnd;

        for (int depth = 0; depth < maxDepth; depth++)
        {
            var cls = GetClassName(current);
            if (string.IsNullOrEmpty(cls)) break;
            if (cls == "Progman" || cls == "#32769") break;

            parts.Add(cls);

            // Convert screen coords to client coords for this window
            var clientPt = new POINT { X = screenX, Y = screenY };
            NativeMethods.ScreenToClient(current, ref clientPt);

            // Find child window at that point
            var child = NativeMethods.ChildWindowFromPointEx(current, clientPt, NativeMethods.CWP_ALL);

            // No child, or child is self = we've reached the deepest
            if (child == IntPtr.Zero || child == current) break;

            current = child;
        }

        return string.Join("/", parts);
    }

    // ── Win32 Recursive Tree Dump (Z-order, front→back) ────────

    /// <summary>
    /// Dump Win32 child window tree recursively in Z-order (front to back).
    /// Windows fully occluded by siblings in front are marked [occluded].
    /// MFC/Win32 native apps have no UIA tree — this is the only way to inspect them.
    /// </summary>
    public static string DumpWin32Tree(IntPtr hWnd, int maxDepth = 6)
    {
        var sb = new StringBuilder(8192);
        var info = WindowInfo.FromHwnd(hWnd);
        var vis = info.IsVisible ? "" : " [hidden]";
        var title = string.IsNullOrEmpty(info.Title) ? "" : $" \"{info.Title}\"";
        var r = info.Rect;
        sb.AppendLine($"[{info.ClassName}]{title} hwnd={hWnd:X8} cid={info.ControlId} ({r.Left},{r.Top} {r.Width}x{r.Height}){vis}");
        DumpWin32Children(hWnd, sb, maxDepth, 1);
        return sb.ToString();
    }

    /// <summary>
    /// Get direct child windows in Z-order (front to back) using GetWindow chain.
    /// </summary>
    public static List<WindowInfo> GetChildrenZOrder(IntPtr hParent)
    {
        var results = new List<WindowInfo>();
        var child = NativeMethods.GetWindow(hParent, NativeMethods.GW_CHILD);
        while (child != IntPtr.Zero)
        {
            results.Add(WindowInfo.FromHwnd(child));
            child = NativeMethods.GetWindow(child, NativeMethods.GW_HWNDNEXT);
        }
        return results;
    }

    /// <summary>
    /// Check if targetRect is fully covered by a set of rects in front of it.
    /// Simple check: any single front rect fully contains the target.
    /// </summary>
    private static bool IsFullyOccluded(RECT target, List<RECT> frontRects)
    {
        if (target.Width <= 0 || target.Height <= 0) return false;
        foreach (var fr in frontRects)
        {
            if (fr.Left <= target.Left && fr.Top <= target.Top &&
                fr.Right >= target.Right && fr.Bottom >= target.Bottom)
                return true;
        }
        return false;
    }

    private static void DumpWin32Children(IntPtr hParent, StringBuilder sb, int maxDepth, int level)
    {
        if (level > maxDepth) return;

        var children = GetChildrenZOrder(hParent);
        var indent = new string(' ', level * 2);

        // Track visible rects of siblings in front (for occlusion check)
        var frontRects = new List<RECT>();

        foreach (var child in children)
        {
            var vis = "";
            if (!child.IsVisible)
            {
                vis = " [hidden]";
            }
            else if (IsFullyOccluded(child.Rect, frontRects))
            {
                vis = " [occluded]";
            }

            // Add this visible window's rect to front list for next siblings
            if (child.IsVisible && child.Rect.Width > 0 && child.Rect.Height > 0)
                frontRects.Add(child.Rect);

            var title = string.IsNullOrEmpty(child.Title) ? "" : $" \"{child.Title}\"";
            var r = child.Rect;
            sb.AppendLine($"{indent}[{child.ClassName}]{title} hwnd={child.Handle:X8} cid={child.ControlId} ({r.Left},{r.Top} {r.Width}x{r.Height}){vis}");
            DumpWin32Children(child.Handle, sb, maxDepth, level + 1);
        }
    }

    // ── Helpers ──────────────────────────────────────────────────

    public static string GetWindowText(IntPtr hWnd)
    {
        var sb = new StringBuilder(512);
        NativeMethods.GetWindowTextW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    public static string GetClassName(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        NativeMethods.GetClassNameW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    public static RECT GetWindowRect(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        return rect;
    }

    /// <summary>
    /// Bring window to foreground and set focus (simple version).
    /// </summary>
    public static void BringToFront(IntPtr hWnd)
    {
        NativeMethods.ShowWindow(hWnd, 9); // SW_RESTORE
        NativeMethods.SmartSetForegroundWindow(hWnd); // [FOCUS-GUARD] CheckActiveGuard 적용
    }

    /// <summary>
    /// Smart focus acquisition with multi-phase recovery.
    /// Returns (success, method) — method describes how focus was obtained.
    ///
    /// Phase 0: Already focused? → return immediately (0ms)
    /// Phase 1: Alert + wait (alertDelaySec) → beep + flash, give user time to finish
    /// Phase 2: Smart recovery (remaining timeout) → AttachThreadInput trick
    /// Phase 3: Timeout → fail
    ///
    /// Design: "알림 후 3초 대기 → 기회 오거나 응답 없으면 즉시 입력 수행"
    /// </summary>
    /// <param name="hWnd">Target window handle</param>
    /// <param name="timeoutSec">Total timeout in seconds</param>
    /// <param name="retryDelaySec">Delay between retries</param>
    /// <param name="alertDelaySec">How long to wait after beep before forcing focus. 0 = force immediately.</param>
    /// <param name="consoleLock">Shared console lock for [FOCUS] output</param>
    /// <returns>(success, method description)</returns>
    public static (bool success, string method) SmartBringToFront(
        IntPtr hWnd,
        double timeoutSec = 5.0,
        double retryDelaySec = 0.3,
        double alertDelaySec = 3.0,
        object? consoleLock = null)
    {
        consoleLock ??= new object();

        // Phase 0: Already focused?
        if (NativeMethods.IsWindowForeground(hWnd))
            return (true, "already_focused");

        // ── Phase 1: Alert + graceful wait ─────────────────────
        // Beep + flash to warn user, then wait alertDelaySec for user to switch back
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Focus lost — alerting user...");
            Console.ResetColor();
        }

        NativeMethods.MessageBeep(NativeMethods.MB_ICONEXCLAMATION);

        // Flash the target window's taskbar button
        var flashInfo = new FLASHWINFO
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<FLASHWINFO>(),
            hwnd = hWnd,
            dwFlags = FLASHWINFO.FLASHW_ALL | FLASHWINFO.FLASHW_TIMERNOFG,
            uCount = 5,
            dwTimeout = 0
        };
        NativeMethods.FlashWindowEx(ref flashInfo);

        // Wait alertDelaySec — user might click back voluntarily
        var alertSw = System.Diagnostics.Stopwatch.StartNew();
        while (alertSw.Elapsed.TotalSeconds < alertDelaySec)
        {
            Thread.Sleep((int)(retryDelaySec * 1000));

            if (NativeMethods.IsWindowForeground(hWnd))
            {
                StopFlash(hWnd);
                lock (consoleLock)
                {
                    ClearLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[FOCUS] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Focus restored by user ← {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "user_restored");
            }
        }

        // ── Phase 2: Smart recovery (force) ────────────────────
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Forcing focus recovery...");
            Console.ResetColor();
        }

        var remainingTimeout = timeoutSec - alertDelaySec;
        var recoverySw = System.Diagnostics.Stopwatch.StartNew();

        while (recoverySw.Elapsed.TotalSeconds < Math.Max(remainingTimeout, 1.0))
        {
            // Restore + SmartSetForegroundWindow (AttachThreadInput trick)
            NativeMethods.ShowWindow(hWnd, 9); // SW_RESTORE
            NativeMethods.SmartSetForegroundWindow(hWnd);

            Thread.Sleep((int)(retryDelaySec * 1000));

            if (NativeMethods.IsWindowForeground(hWnd))
            {
                StopFlash(hWnd);
                lock (consoleLock)
                {
                    ClearLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[FOCUS] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Focus recovered ← {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "smart_recovery");
            }

            // Beep every 2 seconds during recovery
            if ((int)recoverySw.Elapsed.TotalSeconds % 2 == 0)
                NativeMethods.MessageBeep(NativeMethods.MB_ICONEXCLAMATION);
        }

        // ── Phase 3: Timeout ───────────────────────────────────
        StopFlash(hWnd);
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"FOCUS RECOVERY FAILED — timeout after {timeoutSec:F1}s");
            Console.ResetColor();
        }

        return (false, "timeout");
    }

    private static void StopFlash(IntPtr hWnd)
    {
        var stop = new FLASHWINFO
        {
            cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<FLASHWINFO>(),
            hwnd = hWnd,
            dwFlags = FLASHWINFO.FLASHW_STOP,
            uCount = 0,
            dwTimeout = 0
        };
        NativeMethods.FlashWindowEx(ref stop);
    }

    private static void ClearLine()
    {
        int w;
        try { w = Console.WindowWidth; } catch { w = 120; }
        Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");
    }
}

/// <summary>
/// Snapshot of window properties at query time.
/// </summary>
public sealed class WindowInfo
{
    public IntPtr Handle { get; init; }
    public string Title { get; init; } = "";
    public string ClassName { get; init; } = "";
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }
    /// <summary>WS_* style bits from GetWindowLongW(GWL_STYLE)</summary>
    public int Style { get; init; }
    /// <summary>WS_EX_* extended style bits from GetWindowLongW(GWL_EXSTYLE)</summary>
    public int ExStyle { get; init; }
    /// <summary>Grap pattern coverage score (0-1). Higher = more specific match.</summary>
    public double Coverage { get; set; }

    public static WindowInfo FromHwnd(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        return new WindowInfo
        {
            Handle = hWnd,
            Title = WindowFinder.GetWindowText(hWnd),
            ClassName = WindowFinder.GetClassName(hWnd),
            ControlId = NativeMethods.GetDlgCtrlID(hWnd),
            Rect = rect,
            IsVisible = NativeMethods.IsWindowVisible(hWnd),
            Style = NativeMethods.GetWindowLongW(hWnd, NativeMethods.GWL_STYLE),
            ExStyle = NativeMethods.GetWindowLongW(hWnd, NativeMethods.GWL_EXSTYLE),
        };
    }

    public override string ToString() =>
        $"[{Handle:X8}] \"{Title}\" ({ClassName}) cid={ControlId} ({Rect.Width}x{Rect.Height})";
}
