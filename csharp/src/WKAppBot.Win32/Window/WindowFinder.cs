using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using FlaUI.UIA3;
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
    // -- Grap search cache: pattern -> (hwnds, timestamp) --
    // Lazy cache: only caches what was searched. hwnd validity checked on hit.
    static readonly Dictionary<string, (List<IntPtr> hwnds, DateTime cachedAt)> _grapCache = new();
    static readonly TimeSpan _grapCacheTtl = TimeSpan.FromSeconds(5);

    // -- Chrome URL caches (shared across FindWindows calls) --
    // PID -> CDP tab URLs string (all tabs joined by space), 5s TTL
    static readonly Dictionary<uint, (string urls, DateTime cachedAt)> _cdpPidUrlCache = new();
    // HWND -> UIA omnibox URL (active tab), 5s TTL
    static readonly Dictionary<IntPtr, (string url, DateTime cachedAt)> _uiaOmniboxCache = new();
    static readonly TimeSpan _urlCacheTtl = TimeSpan.FromSeconds(5);
    static UIA3Automation? _lazyUia;
    static bool _lazyUiaFailed;

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
    ///   Examples: "#32770" -> finds [#32770] dialogs with empty titles
    ///             "nkrunlite" -> finds all windows from that process
    ///             "영웅문" -> finds [_NKHeroMainClass] 영웅문4
    ///             "hwnd:0054188E" -> direct handle lookup
    /// </summary>
    public static List<WindowInfo> FindWindows(string titlePattern, bool stopOnFirstMatch = false)
    {
        // -- Cache check: return cached if all hwnds still alive and within TTL --
        if (_grapCache.TryGetValue(titlePattern, out var cached)
            && DateTime.UtcNow - cached.cachedAt < _grapCacheTtl
            && cached.hwnds.All(NativeMethods.IsWindow))
        {
            return cached.hwnds.Select(WindowInfo.FromHwnd).ToList();
        }
        // -- Multi-field JSON-like pattern: {hwnd:0xAABB,pid:1234,title:"...",domain:"..."} --
        // All specified fields must match (AND logic). Supported fields:
        //   hwnd    hex HWND (with or without 0x) -- direct lookup if sole field
        //   pid     decimal or hex process ID
        //   title   window title substring (PatternMatcher: glob/regex supported)
        //   cls     window class name substring
        //   proc    process name substring
        //   domain  browser domain (via GetBrowserUrl) substring
        //   url     browser full URL substring
        if (titlePattern.StartsWith('{') && titlePattern.EndsWith('}'))
        {
            var mfFields = ParseMultiFieldPattern(titlePattern);
            if (mfFields != null)
            {
                var mfResults = FindByMultiField(mfFields, stopOnFirstMatch);
                _grapCache[titlePattern] = (mfResults.Select(r => r.Handle).ToList(), DateTime.UtcNow);
                return mfResults;
            }
        }

        // ★ Direct HWND lookup -- no enumeration, works for any HWND (root or child)
        // Supported formats (all equivalent):
        //   [001A2B3C]   <- windows/inspect output format (canonical)
        //   hwnd:001A2B3C
        //   hwnd:0x001A2B3C
        string? hwndHex = null;
        if (titlePattern.Length == 10 && titlePattern[0] == '[' && titlePattern[9] == ']')
            hwndHex = titlePattern[1..9];  // [001A2B3C]
        else if (titlePattern.StartsWith("hwnd:", StringComparison.OrdinalIgnoreCase))
            hwndHex = titlePattern[5..].Trim().TrimStart('0', 'x', 'X');
        if (hwndHex != null)
        {
            if (IntPtr.TryParse(hwndHex, System.Globalization.NumberStyles.HexNumber, null, out var hwndVal)
                && hwndVal != IntPtr.Zero && NativeMethods.IsWindow(hwndVal))
                return new List<WindowInfo> { WindowInfo.FromHwnd(hwndVal) };
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
                procName = NativeMethods.TryGetProcessNameFast(pid) ?? $"pid={pid}";
                procNameCache[pid] = procName;
            }

            // Get window size
            NativeMethods.GetWindowRect(hWnd, out var rect);
            var w = rect.Right - rect.Left;
            var h = rect.Bottom - rect.Top;

            // ★ Standard search key with focus flags
            var searchKey = BuildSearchKey(hWnd, cls, title, procName, w, h, focus);

            // Token-AND match: plain multi-word patterns check each token in title+searchKey (order-independent)
            string matchedVia = "title";
            // "매칭된 만큼만" -- the emitted title field should show only the
            // pattern's literal core, not the whole window title. Titles
            // mutate (tab switches, dirty-doc '*', etc.) so full-title
            // capture invites miss-match on the next copy-paste call.
            string matchedSnippet = ExtractPatternLiteral(titlePattern, title);
            bool matched = PatternMatcher.TokenMatchAny(titlePattern, title, searchKey);
            if (matched && !PatternMatcher.TokenMatchAny(titlePattern, title) && PatternMatcher.TokenMatchAny(titlePattern, procName))
            {
                matchedVia = "proc"; matchedSnippet = procName;
            }

            // URL fallback: any browser exposing URL via UIA a11y tree or CDP
            if (!matched)
            {
                var url = GetBrowserUrl(hWnd, pid);
                if (!string.IsNullOrEmpty(url) && PatternMatcher.TokenMatchAny(titlePattern, url))
                {
                    matched = true;
                    try { matchedVia = "domain"; matchedSnippet = new Uri(url.Split(' ')[0]).Host; } catch { matchedVia = "url"; matchedSnippet = url.Length > 60 ? url[..60] : url; }
                }
            }

            // cmd: fallback -- match against process command line args
            // Skip wkappbot* and shell processes: their cmdline carries the current search query.
            if (!matched && !IsSearcherProcess(procName))
            {
                try
                {
                    var cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
                    if (!string.IsNullOrEmpty(cmdLine) && PatternMatcher.TokenMatchAny(titlePattern, cmdLine))
                    {
                        matched = true;
                        matchedVia = "cmd";
                        // Extract just the matching token, not the full cmdLine
                        var tokens = cmdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var matchToken = tokens.FirstOrDefault(tk => PatternMatcher.TokenMatchAny(titlePattern, tk))
                                         ?? (cmdLine.Length > 60 ? cmdLine[..60] : cmdLine);
                        matchedSnippet = matchToken.Trim('"', '\'');
                    }
                }
                catch { }
            }

            if (!matched && (procName.Equals("Code", StringComparison.OrdinalIgnoreCase)
                             || procName.Equals("claude", StringComparison.OrdinalIgnoreCase)
                             || procName.Equals("codex", StringComparison.OrdinalIgnoreCase)
                             || cls.StartsWith("Chrome_WidgetWin_", StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    var uiaHits = UiaLocator.QuickSearch(hWnd, titlePattern, maxDepth: 5, maxResults: 3, maxVisited: 250, timeoutMs: 1500);
                    if (uiaHits.Count > 0)
                    {
                        matched = true;
                        matchedVia = "uia";
                        matchedSnippet = uiaHits[0].NamePath.Length > 0
                            ? uiaHits[0].NamePath
                            : (!string.IsNullOrEmpty(uiaHits[0].AutomationId) ? uiaHits[0].AutomationId : uiaHits[0].Name);
                    }
                }
                catch { }
            }

            if (matched)
            {
                var info = WindowInfo.FromHwnd(hWnd);
                var patLen = titlePattern.Replace("*", "").Replace("?", "").Length;
                var denomLen = (matchedSnippet ?? title).Length;
                info.Coverage = patLen > 0 && denomLen > 0 ? (double)patLen / denomLen : 0;
                info.MatchedVia = matchedVia;
                info.MatchedSnippet = matchedSnippet;
                // Expose all discoverable fields so a11y find can suggest better grap alternatives
                var simpleFields = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
                    { [matchedVia ?? "title"] = new List<string> { titlePattern } };
                info.FieldScores = ComputeFieldScores(hWnd, simpleFields, procNameCache: null);
                results.Add(info);
                if (stopOnFirstMatch) return false;
            }

            return true;
        }, IntPtr.Zero);

        // -- Hidden top-level fallback: if the visible scan found nothing, re-scan
        // invisible top-level windows so process-name / cmdline searches can still
        // surface admin-only frames, hidden launchers, and windowless shells.
        if (results.Count == 0)
        {
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (NativeMethods.IsWindowVisible(hWnd)) return true;

                var title = GetWindowText(hWnd);
                var cls = GetClassName(hWnd);

                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (!procNameCache.TryGetValue(pid, out var procName))
                {
                    procName = NativeMethods.TryGetProcessNameFast(pid) ?? $"pid={pid}";
                    procNameCache[pid] = procName;
                }

                NativeMethods.GetWindowRect(hWnd, out var rect);
                var w = rect.Right - rect.Left;
                var h = rect.Bottom - rect.Top;
                var searchKey = BuildSearchKey(hWnd, cls, title, procName, w, h, focus);

                string matchedVia = "hidden";
                string matchedSnippet = title;
                bool matched = PatternMatcher.TokenMatchAny(titlePattern, title, searchKey);
                if (matched && !PatternMatcher.TokenMatchAny(titlePattern, title) && PatternMatcher.TokenMatchAny(titlePattern, procName))
                {
                    matchedVia = "proc"; matchedSnippet = procName;
                }

                if (!matched)
                {
                    var url = GetBrowserUrl(hWnd, pid);
                    if (!string.IsNullOrEmpty(url) && PatternMatcher.TokenMatchAny(titlePattern, url))
                    {
                        matched = true;
                        try { matchedVia = "domain"; matchedSnippet = new Uri(url.Split(' ')[0]).Host; } catch { matchedVia = "url"; matchedSnippet = url.Length > 60 ? url[..60] : url; }
                    }
                }

                if (!matched && !IsSearcherProcess(procName))
                {
                    try
                    {
                        var cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
                        if (!string.IsNullOrEmpty(cmdLine) && PatternMatcher.TokenMatchAny(titlePattern, cmdLine))
                        {
                            matched = true;
                            matchedVia = "cmd";
                            var tokens = cmdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            var matchToken = tokens.FirstOrDefault(tk => PatternMatcher.TokenMatchAny(titlePattern, tk))
                                             ?? (cmdLine.Length > 60 ? cmdLine[..60] : cmdLine);
                            matchedSnippet = matchToken.Trim('"', '\'');
                        }
                    }
                    catch { }
                }

                if (!matched)
                {
                    if ((procName.Equals("Code", StringComparison.OrdinalIgnoreCase)
                         || procName.Equals("claude", StringComparison.OrdinalIgnoreCase)
                         || procName.Equals("codex", StringComparison.OrdinalIgnoreCase)
                         || cls.StartsWith("Chrome_WidgetWin_", StringComparison.OrdinalIgnoreCase)))
                    {
                        try
                        {
                            var uiaHits = UiaLocator.QuickSearch(hWnd, titlePattern, maxDepth: 5, maxResults: 3, maxVisited: 250, timeoutMs: 1500);
                            if (uiaHits.Count > 0)
                            {
                                matched = true;
                                matchedVia = "uia";
                                matchedSnippet = uiaHits[0].NamePath.Length > 0
                                    ? uiaHits[0].NamePath
                                    : (!string.IsNullOrEmpty(uiaHits[0].AutomationId) ? uiaHits[0].AutomationId : uiaHits[0].Name);
                            }
                        }
                        catch { }
                    }
                }

                if (matched)
                {
                    var info = WindowInfo.FromHwnd(hWnd);
                    // Same search-term coverage metric as the visible-scan path:
                    // denominator = matched field length, not always title length.
                    var patLen = titlePattern.Replace("*", "").Replace("?", "").Length;
                    var denomLen = (matchedSnippet ?? title).Length;
                    info.Coverage = patLen > 0 && denomLen > 0 ? (double)patLen / denomLen : 0;
                    info.MatchedVia = matchedVia;
                    info.MatchedSnippet = matchedSnippet;
                    results.Add(info);
                    if (stopOnFirstMatch) return false;
                }

                return true;
            }, IntPtr.Zero);
        }

        // -- MDI child title scan fallback --
        // EnumWindows only enumerates top-level windows. MDI document children
        // (e.g. Heroes4 "[0150] 조건검색" under MDIClient) are invisible to a
        // plain title-pattern search. When the main scan found nothing (or only
        // shell-noise matches), scan MDI children of all visible top-level windows.
        // Run BEFORE the slow windowless cmdLine scan so we can skip that scan
        // when MDI children already answer the query (30+s -> <1s on hit).
        bool noRealResults = results.Count == 0
            || results.All(r => r.Coverage == 0
                             || r.MatchedVia is "context" or "child-cmd");
        if (noRealResults)
        {
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                var hMdi = NativeMethods.FindWindowExW(hWnd, IntPtr.Zero, "MDIClient", null);
                if (hMdi == IntPtr.Zero) return true;
                foreach (var mc in GetChildrenZOrder(hMdi))
                {
                    if (!NativeMethods.IsWindowVisible(mc.Handle)) continue;
                    if (string.IsNullOrEmpty(mc.Title)) continue;
                    if (!PatternMatcher.TokenMatchAny(titlePattern, mc.Title)) continue;
                    if (results.Any(r => r.Handle == mc.Handle)) continue;
                    var patLen = titlePattern.Replace("*", "").Replace("?", "").Length;
                    mc.Coverage = patLen > 0 && mc.Title.Length > 0 ? (double)patLen / mc.Title.Length : 0;
                    mc.MatchedVia = "title";
                    mc.MatchedSnippet = mc.Title;
                    mc.FieldScores = ComputeFieldScores(mc.Handle, null, null);
                    results.Add(mc);
                }
                return true;
            }, IntPtr.Zero);
        }

        // -- Windowless process cmdLine scan: find processes with no visible window --
        // Walk PPID chain to find host window (e.g. wkappbot running in VS Code terminal).
        // Only run when no results found yet -- GetProcessCommandLine across all
        // processes takes 30+s, so skip it entirely when main scan + MDI fallback
        // already produced matches.
        if (results.Count == 0)
        {
            // Build pid->hwnd map from all visible windows
            var pidToHwnd = new Dictionary<uint, IntPtr>();
            var alreadyMatchedPids = new HashSet<uint>(results.Select(r => {
                NativeMethods.GetWindowThreadProcessId(r.Handle, out uint p); return p;
            }));
            NativeMethods.EnumWindows((hWnd, _) => {
                if (NativeMethods.IsWindowVisible(hWnd)) {
                    NativeMethods.GetWindowThreadProcessId(hWnd, out uint wPid);
                    if (!pidToHwnd.ContainsKey(wPid)) pidToHwnd[wPid] = hWnd;
                }
                return true;
            }, IntPtr.Zero);

            foreach (var proc in System.Diagnostics.Process.GetProcesses())
            {
                try
                {
                    var pid = (uint)proc.Id;
                    if (pid <= 4) continue;
                    if (pidToHwnd.ContainsKey(pid)) continue; // has own window -- already checked
                    if (IsSearcherProcess(proc.ProcessName)) continue;
                    var cmdLine = NativeMethods.GetProcessCommandLine((int)pid);
                    if (string.IsNullOrEmpty(cmdLine)) continue;
                    if (!PatternMatcher.TokenMatchAny(titlePattern, cmdLine)) continue;

                    // Extract matching token
                    var tokens = cmdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var matchToken = tokens.FirstOrDefault(tk => PatternMatcher.TokenMatchAny(titlePattern, tk))
                                     ?? (cmdLine.Length > 60 ? cmdLine[..60] : cmdLine);
                    matchToken = matchToken.Trim('"', '\'');

                    // Walk PPID chain to find host window
                    IntPtr hostHwnd = IntPtr.Zero;
                    int cur = (int)pid;
                    for (int depth = 0; depth < 8 && hostHwnd == IntPtr.Zero; depth++)
                    {
                        int parent = NativeMethods.GetParentProcessId(cur);
                        if (parent <= 0 || parent == cur) break;
                        if (pidToHwnd.TryGetValue((uint)parent, out var ph)) { hostHwnd = ph; break; }
                        cur = parent;
                    }
                    if (hostHwnd == IntPtr.Zero) continue;
                    if (results.Any(r => r.Handle == hostHwnd)) continue;

                    var hostInfo = WindowInfo.FromHwnd(hostHwnd);
                    // "child-cmd": host window found via child process cmdline match.
                    // NOT injected as cmd: field in grap (host proc != matched proc).
                    // MatchedSnippet = "childProc:token" -> annotation "<- wkappbot: chatgpt.com"
                    hostInfo.MatchedVia = "child-cmd";
                    hostInfo.MatchedSnippet = $"{proc.ProcessName}:{matchToken}";
                    // Real search-term coverage against the matched cmd token, not a hardcoded 0.5.
                    var patLen2 = titlePattern.Replace("*", "").Replace("?", "").Length;
                    hostInfo.Coverage = patLen2 > 0 && matchToken.Length > 0 ? (double)patLen2 / matchToken.Length : 0;
                    results.Add(hostInfo);
                }
                catch { }
                finally { try { proc.Dispose(); } catch { } }
            }
        }

        // Sort by coverage descending (most specific match first), then focus priority
        if (results.Count > 1)
            results.Sort((a, b) =>
            {
                var covCmp = b.Coverage.CompareTo(a.Coverage);
                return covCmp != 0 ? covCmp : focus.GetPriority(b.Handle).CompareTo(focus.GetPriority(a.Handle));
            });

        bool queryLooksLikeDomain = titlePattern.Contains('.') || titlePattern.Contains("://") || titlePattern.StartsWith("www.", StringComparison.OrdinalIgnoreCase);
        if (queryLooksLikeDomain && focus.ForegroundHwnd != IntPtr.Zero && NativeMethods.IsWindowVisible(focus.ForegroundHwnd))
        {
            if (!results.Any(r => r.Handle == focus.ForegroundHwnd))
            {
                NativeMethods.GetWindowThreadProcessId(focus.ForegroundHwnd, out uint fgPid);
                var fgProc = NativeMethods.TryGetProcessNameFast(fgPid) ?? $"pid={fgPid}";
                var fgCls = GetClassName(focus.ForegroundHwnd);
                if (fgProc.Equals("Code", StringComparison.OrdinalIgnoreCase)
                    || fgProc.Equals("claude", StringComparison.OrdinalIgnoreCase)
                    || fgProc.Equals("codex", StringComparison.OrdinalIgnoreCase)
                    || fgCls.StartsWith("Chrome_WidgetWin_", StringComparison.OrdinalIgnoreCase))
                {
                    var host = WindowInfo.FromHwnd(focus.ForegroundHwnd);
                    // No real search-term match -- this is a foreground-host
                    // context add. Coverage=0 keeps it strictly below real
                    // matches; focus priority is the natural tiebreaker.
                    host.Coverage = 0;
                    host.MatchedVia = "context";
                    host.MatchedSnippet = "foreground host";
                    results.Add(host);
                }
            }
        }

        // -- Fallback: owner window chain for hidden child processes --
        // When grap matches only hidden/tiny windows (e.g. wsl PseudoConsoleWindow),
        // follow GW_OWNER chain to find the visible host window (e.g. WindowsTerminal CASCADIA).
        // This works because ConPTY/hosted windows set the owner to their host's tab window.
        if (results.Count > 0 && results.All(r =>
        {
            NativeMethods.GetWindowRect(r.Handle, out var rr);
            return !NativeMethods.IsWindowVisible(r.Handle)
                || (rr.Right - rr.Left < 50 && rr.Bottom - rr.Top < 50);
        }))
        {
            var hostFallback = FindHostByOwnerChain(results);
            if (hostFallback.Count > 0)
                results.AddRange(hostFallback);
        }

        // -- Cache results for repeat searches --
        _grapCache[titlePattern] = (results.Select(r => r.Handle).ToList(), DateTime.UtcNow);

        return results;
    }

    // -- Owner chain cache: hidden HWND -> host HWND (2s TTL) --
    static readonly Dictionary<IntPtr, (IntPtr host, DateTime cachedAt)> _ownerHostCache = new();
    static readonly TimeSpan _ownerCacheTtl = TimeSpan.FromSeconds(2);

    /// <summary>
    /// Follow GW_OWNER chain from hidden windows to find visible host windows.
    /// E.g. wsl PseudoConsoleWindow(owner=CASCADIA) -> WindowsTerminal CASCADIA tab.
    /// O(1) per window with 2s TTL cache. Max 3 hops on cache miss.
    /// </summary>
    static List<WindowInfo> FindHostByOwnerChain(List<WindowInfo> hiddenResults)
    {
        var hosts = new List<WindowInfo>();
        var seen = new HashSet<IntPtr>();
        var now = DateTime.UtcNow;

        foreach (var hidden in hiddenResults)
        {
            // Cache hit?
            if (_ownerHostCache.TryGetValue(hidden.Handle, out var cached)
                && now - cached.cachedAt < _ownerCacheTtl
                && NativeMethods.IsWindow(cached.host))
            {
                if (cached.host != IntPtr.Zero && seen.Add(cached.host))
                {
                    var info = WindowInfo.FromHwnd(cached.host);
                    // Owner-host fallback -- no direct text match, so zero coverage.
                    info.Coverage = 0;
                    hosts.Add(info);
                }
                continue;
            }

            // Cache miss -- walk owner chain
            var current = hidden.Handle;
            IntPtr foundHost = IntPtr.Zero;
            for (int hop = 0; hop < 3; hop++)
            {
                var owner = NativeMethods.GetWindow(current, 4 /* GW_OWNER */);
                if (owner == IntPtr.Zero || owner == current) break;

                if (NativeMethods.IsWindowVisible(owner))
                {
                    NativeMethods.GetWindowRect(owner, out var rect);
                    int w = rect.Right - rect.Left, h = rect.Bottom - rect.Top;
                    if (w >= 50 && h >= 50)
                    {
                        foundHost = owner;
                        if (seen.Add(owner))
                        {
                            var info = WindowInfo.FromHwnd(owner);
                            info.Coverage = 0.1;
                            hosts.Add(info);
                        }
                    }
                    break;
                }
                current = owner;
            }

            // Cache result (even if not found -- prevents re-walk)
            _ownerHostCache[hidden.Handle] = (foundHost, now);
        }

        return hosts;
    }

    // ===============================================================
    //  Focus state snapshot + search key builder (standard for all callers)
    // ===============================================================

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

            // Pre-compute root ancestors once -- avoids per-window GetAncestor calls
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
    /// Used by FindWindows, WindowsCommand, and any grap-based search.
    /// </summary>
    // Slice the pattern's literal down to what actually matches inside text.
    // Strips leading/trailing '*'/'?' wildcards and the "regex:" prefix, then
    // returns the core. If the core is empty (e.g. pattern was just "*"), we
    // fall back to the first 30 chars of text. Cap at 30 chars so the grap
    // never blows out from long window titles.
    public static string ExtractPatternLiteral(string pattern, string text)
    {
        if (string.IsNullOrEmpty(pattern)) return TrimForGrap(text);
        var p = pattern;
        if (p.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
            p = p["regex:".Length..];
        // Take longest literal run between wildcards/regex-anchors.
        var parts = p.Split(new[] { '*', '?', '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
        var core = parts.OrderByDescending(s => s.Length).FirstOrDefault() ?? "";
        if (core.Length == 0) return TrimForGrap(text);
        return TrimForGrap(core);
    }

    static string TrimForGrap(string s)
    {
        var t = s.Trim();
        return t.Length > 30 ? t[..30] : t;
    }

    public static string BuildSearchKey(IntPtr hWnd, string cls, string title,
        string procName, int w, int h, FocusSnapshot? focus = null)
    {
        var flags = focus?.GetFlags(hWnd) ?? "";
        // wkappbot.* props -> "wkappbot" 딱지 + 상세 props
        var wkArgs = "";
        var isWebbot = NativeMethods.GetPropW(hWnd, "wkappbot.webbot") != IntPtr.Zero;
        var cdpPort = NativeMethods.GetPropW(hWnd, "wkappbot.cdp").ToInt32();
        if (isWebbot || cdpPort > 0)
        {
            var parts = new List<string>(3) { "wkappbot" }; // 딱지 항상 포함
            if (isWebbot) parts.Add("webbot");
            if (cdpPort > 0) parts.Add($"cdp={cdpPort}");
            wkArgs = $" {string.Join(" ", parts)}"; // IntPtr props -- auto-freed on window destroy
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
    /// Find a child window matching a pattern. Resolution order:
    ///   1. MDI children (direct children of MDIClient under hParent)
    ///   2. Direct children of hParent
    ///   3. Deep descendants (EnumChildWindows fallback for MFC grandchildren)
    /// Step 3 is critical for HTS-style MFC apps where UIA tree is empty below
    /// the top frame and controls live 3-4 levels deep under custom container
    /// classes (Afx:* / CView / custom group boxes). Uses PatternMatcher
    /// substring match against title + enriched searchKey.
    /// Returns null if no match found at any depth.
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
                if (PatternMatcher.TokenMatchAny(pattern, mc.Title, SearchKey(mc)))
                    return mc;
            }
        }

        // Direct children
        foreach (var ch in topChildren)
        {
            if (PatternMatcher.TokenMatchAny(pattern, ch.Title, SearchKey(ch)))
                return ch;
        }

        // Deep-descendant fallback -- for HTS/MFC apps with empty UIA tree,
        // controls live 3-4 levels deep under custom MFC containers. Walk the
        // full EnumChildWindows tree (depth capped implicitly by the Win32
        // enum itself) and return the first pattern match. Visited set is
        // unnecessary because EnumChildWindows walks a tree, not a graph.
        WindowInfo? found = null;
        NativeMethods.EnumChildWindows(hParent, (hWnd, _) =>
        {
            var wi = WindowInfo.FromHwnd(hWnd);
            if (PatternMatcher.TokenMatchAny(pattern, wi.Title, SearchKey(wi)))
            {
                found = wi;
                return false; // stop enumeration
            }
            return true;
        }, IntPtr.Zero);
        return found;
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

    // -- Win32 Recursive Tree Dump (Z-order, front->back) --------

    /// <summary>
    /// Dump Win32 child window tree recursively in Z-order (front to back).
    /// Windows fully occluded by siblings in front are marked [occluded].
    /// MFC/Win32 native apps have no UIA tree -- this is the only way to inspect them.
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

    // -- Helpers --------------------------------------------------

    public static string GetWindowText(IntPtr hWnd)
        => NativeMethods.GetWindowTextSafe(hWnd, 50);

    public static string GetClassName(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        NativeMethods.GetClassNameW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    /// <summary>
    /// Returns class name ancestry path: "ownClass/parentClass" (up to maxDepth).
    /// Stops at desktop (#32769), zero hwnd, or repeated classes.
    /// WPF HwndWrapper[proc;name;UUID]: extracts semantic name part only (e.g. "WhisperRing-Main-STA").
    /// Empty name segment → uses proc name (e.g. "wkappbot-core").
    /// Example (MFC):  "Afx:00DB0000/_NKHeroMainClass"
    /// Example (WPF):  "WhisperRing-Main-STA/wkappbot-core"
    /// </summary>
    public static string GetClassPath(IntPtr hWnd, int maxDepth = 3)
    {
        var parts = new List<string>();
        var cur = hWnd;
        for (int i = 0; i < maxDepth && cur != IntPtr.Zero; i++)
        {
            var cls = GetClassName(cur);
            if (string.IsNullOrEmpty(cls) || cls == "#32769") break; // desktop
            // WPF: HwndWrapper[proc;name;UUID] → extract semantic name or proc
            var wpf = System.Text.RegularExpressions.Regex.Match(cls,
                @"HwndWrapper\[([^;]+);([^;]*);[0-9a-f\-]{0,36}\]",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (wpf.Success)
            {
                var name = wpf.Groups[2].Value.Trim();
                cls = name.Length > 0 ? name : wpf.Groups[1].Value.Trim();
            }
            if (parts.Contains(cls)) break; // cycle guard
            parts.Add(cls);
            cur = NativeMethods.GetParent(cur);
        }
        return string.Join("/", parts);
    }

    // Shell/system processes that "launch" everything -- stop proc path walk here.
    // Including them makes proc:'**/explorer' match all apps, which is useless noise.
    static readonly HashSet<string> _procPathStopList = new(StringComparer.OrdinalIgnoreCase)
    {
        "explorer", "svchost", "services", "lsass", "winlogon", "csrss", "smss",
        "wininit", "System", "Registry", "dwm",
    };

    /// <summary>
    /// Returns process ancestry path: "ownProc/parentProc" (up to maxDepth).
    /// Stops at pid 0/1, shell/system processes (explorer, svchost, etc.), or repeated names.
    /// Example: "nkrunlite/wkappbot-core" (stops before bash/explorer)
    /// </summary>
    public static string GetProcPath(uint pid, int maxDepth = 3)
    {
        var parts = new List<string>();
        var cur = (int)pid;
        for (int i = 0; i < maxDepth && cur > 1; i++)
        {
            var name = NativeMethods.TryGetProcessNameFast((uint)cur) ?? "";
            if (string.IsNullOrEmpty(name)) break;
            if (_procPathStopList.Contains(name)) break; // shell/system stop
            if (parts.Contains(name)) break; // cycle guard
            parts.Add(name);
            cur = NativeMethods.GetParentProcessId(cur);
        }
        return string.Join("/", parts);
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
    /// Returns (success, method) -- method describes how focus was obtained.
    ///
    /// Phase 0: Already focused? -> return immediately (0ms)
    /// Phase 1: Alert + wait (alertDelaySec) -> beep + flash, give user time to finish
    /// Phase 2: Smart recovery (remaining timeout) -> AttachThreadInput trick
    /// Phase 3: Timeout -> fail
    ///
    /// Design: "알림 후 3초 대기 -> 기회 오거나 응답 없으면 즉시 입력 수행"
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

        // -- Phase 1: Alert + graceful wait --------------------─
        // Beep + flash to warn user, then wait alertDelaySec for user to switch back
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Focus lost -- alerting user...");
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

        // Wait alertDelaySec -- user might click back voluntarily
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
                    Console.WriteLine($"Focus restored by user <- {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "user_restored");
            }
        }

        // -- Phase 2: Smart recovery (force) --------------------
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
                    Console.WriteLine($"Focus recovered <- {alertSw.ElapsedMilliseconds}ms");
                    Console.ResetColor();
                }
                return (true, "smart_recovery");
            }

            // Beep every 2 seconds during recovery
            if ((int)recoverySw.Elapsed.TotalSeconds % 2 == 0)
                NativeMethods.MessageBeep(NativeMethods.MB_ICONEXCLAMATION);
        }

        // -- Phase 3: Timeout ----------------------------------─
        StopFlash(hWnd);
        lock (consoleLock)
        {
            ClearLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[FOCUS] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"FOCUS RECOVERY FAILED -- timeout after {timeoutSec:F1}s");
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

    // -- Browser URL fetch -- 4-tier fallback, any browser --
    // Tier 0: Fast give-up for non-browser windows
    // Tier 1: CDP HTTP /json          -- Chromium + --remote-debugging-port (all tabs)
    // Tier 2: UIA known address bar IDs -- omnibox_view (Chrome/Edge/Brave), urlbar-input (Firefox)
    // Tier 3: UIA ControlType.Edit in ToolBar + URL heuristic -- standard UIA, any compliant browser
    // Tier 4: UIA ControlType.Document -> LegacyIAccessible.Value -- W3C a11y standard
    // Results cached per PID (Tier 1) / HWND (Tier 2-4) with 5s TTL.
    public static string GetBrowserUrl(IntPtr hWnd, uint pid)
    {
        var now = DateTime.UtcNow;

        if (hWnd != IntPtr.Zero && _uiaOmniboxCache.TryGetValue(hWnd, out var uiaEntry) && now - uiaEntry.cachedAt < _urlCacheTtl)
            return uiaEntry.url;

        var cls = hWnd != IntPtr.Zero ? GetClassName(hWnd) : "";
        var proc = GetProcessNameCached(pid);
        bool browserLike = IsLikelyBrowserWindow(hWnd, cls, proc);

        if (!browserLike)
        {
            if (hWnd != IntPtr.Zero)
                _uiaOmniboxCache[hWnd] = ("", now);
            return "";
        }

        // Tier 0: cmdline URL extraction -- works for Chromium PWAs AND Electron apps (e.g. VS Code)
        try
        {
            var cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
            // --app=https://... (Chromium PWA / Web App only)
            if (IsChromiumProcess(proc))
            {
                var mApp = Regex.Match(cmdLine, @"--app=(https?://\S+)", RegexOptions.IgnoreCase);
                if (mApp.Success)
                {
                    var appUrl = mApp.Groups[1].Value.TrimEnd('"', '\'');
                    if (!string.IsNullOrEmpty(appUrl))
                    {
                        _uiaOmniboxCache[hWnd] = (appUrl, now);
                        return appUrl;
                    }
                }
            }
            // --folder-uri=file:// (VS Code / any Electron workspace -- not Chromium-specific)
            var mFolder = Regex.Match(cmdLine, @"--folder-uri=(file://[^\s""']+)", RegexOptions.IgnoreCase);
            if (mFolder.Success)
            {
                var folderUrl = mFolder.Groups[1].Value.TrimEnd('"', '\'');
                if (!string.IsNullOrEmpty(folderUrl))
                {
                    _uiaOmniboxCache[hWnd] = (folderUrl, now);
                    return folderUrl;
                }
            }
        }
        catch { }

        // Tier 1: CDP HTTP -- Chromium only, requires --remote-debugging-port
        if (!_cdpPidUrlCache.TryGetValue(pid, out var cdpEntry) || now - cdpEntry.cachedAt >= _urlCacheTtl)
        {
            string cdpUrls = "";
            try
            {
                int port = NativeMethods.GetPropW(hWnd, "wkappbot.cdp").ToInt32();
                if (port <= 0 && IsChromiumProcess(proc))
                {
                    var cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
                    var m = Regex.Match(cmdLine, @"--remote-debugging-port=(\d+)");
                    if (m.Success)
                        int.TryParse(m.Groups[1].Value, out port);
                }

                // Sanity: port must be in valid user-range. port 9 (discard), <1024 (reserved),
                // or >65535 are never legit CDP endpoints. GetPropW occasionally returns stale/
                // corrupted values -> probing 127.0.0.1:9 throws HttpRequestException that escaped
                // to RunMain top-level catch in the 2026-04-19 Slack bug report.
                if (port >= 1024 && port <= 65535)
                {
                    using var http = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromMilliseconds(200) };
                    var json = http.GetStringAsync($"http://localhost:{port}/json").GetAwaiter().GetResult();
                    var urlMatches = Regex.Matches(json, "\"url\":\\s*\"([^\"]+)\"");
                    cdpUrls = string.Join(" ", urlMatches.Select(u => u.Groups[1].Value));
                }
            }
            catch (System.Net.Http.HttpRequestException) { /* probe failed -- cache empty */ }
            catch (TaskCanceledException) { /* probe timed out */ }
            catch { }
            _cdpPidUrlCache[pid] = (cdpUrls, now);
            cdpEntry = (cdpUrls, now);
        }
        if (!string.IsNullOrEmpty(cdpEntry.urls))
        {
            if (hWnd != IntPtr.Zero)
                _uiaOmniboxCache[hWnd] = (cdpEntry.urls, now);
            return cdpEntry.urls;
        }

        if (hWnd == IntPtr.Zero) return "";

        string url = "";
        if (!_lazyUiaFailed)
        {
            try
            {
                _lazyUia ??= new UIA3Automation();
                var root = _lazyUia.FromHandle(hWnd);
                if (root != null)
                {
                    // Tier 2: known address bar AutomationIds (Chrome/Edge/Brave = omnibox_view, Firefox = urlbar-input)
                    var addrBar = root.FindFirstDescendant(cf =>
                        cf.ByAutomationId("omnibox_view").Or(cf.ByAutomationId("urlbar-input")));
                    url = addrBar?.Patterns.Value.PatternOrDefault?.Value.ValueOrDefault ?? "";

                    // Tier 3: ControlType.Edit inside a ToolBar whose Value looks like a URL
                    if (string.IsNullOrEmpty(url))
                    {
                        var toolbars = root.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ToolBar));
                        foreach (var toolbar in toolbars)
                        {
                            var edits = toolbar.FindAllDescendants(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
                            foreach (var edit in edits)
                            {
                                var val = edit.Patterns.Value.PatternOrDefault?.Value.ValueOrDefault ?? "";
                                if (val.Contains("://") || val.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
                                { url = val; break; }
                            }
                            if (!string.IsNullOrEmpty(url)) break;
                        }
                    }

                    // Tier 4: ControlType.Document -> LegacyIAccessible.Value (W3C a11y standard path)
                    if (string.IsNullOrEmpty(url))
                    {
                        var doc = root.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Document));
                        var legacyVal = doc?.Patterns.LegacyIAccessible.PatternOrDefault?.Value.ValueOrDefault ?? "";
                        if (legacyVal.Contains("://")) url = legacyVal;
                    }
                }
            }
            catch { _lazyUiaFailed = true; }
        }

        _uiaOmniboxCache[hWnd] = (url, now);
        return url;
    }

    static bool IsLikelyBrowserWindow(IntPtr hWnd, string cls, string proc)
    {
        if (hWnd != IntPtr.Zero)
        {
            if (NativeMethods.GetPropW(hWnd, "wkappbot.webbot") != IntPtr.Zero) return true;
            if (NativeMethods.GetPropW(hWnd, "wkappbot.cdp") != IntPtr.Zero) return true;
        }

        if (string.IsNullOrEmpty(cls) && string.IsNullOrEmpty(proc)) return false;
        if (cls.Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase)) return true;
        if (cls.Equals("Chrome_WidgetWin_0", StringComparison.OrdinalIgnoreCase)) return true;
        if (cls.Equals("MozillaWindowClass", StringComparison.OrdinalIgnoreCase)) return true;
        if (cls.Equals("ApplicationFrameWindow", StringComparison.OrdinalIgnoreCase) && proc.Contains("msedge", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    static bool IsChromiumProcess(string proc)
    {
        if (string.IsNullOrEmpty(proc)) return false;
        return proc.Contains("chrome", StringComparison.OrdinalIgnoreCase)
            || proc.Contains("msedge", StringComparison.OrdinalIgnoreCase)
            || proc.Contains("brave", StringComparison.OrdinalIgnoreCase)
            || proc.Contains("vivaldi", StringComparison.OrdinalIgnoreCase)
            || proc.Contains("opera", StringComparison.OrdinalIgnoreCase);
    }

    static string GetProcessNameCached(uint pid)
    {
        if (pid == 0) return "";
        return NativeMethods.TryGetProcessNameFast(pid) ?? "";
    }

    static string GetPrimaryUrlToken(string urls)
    {
        if (string.IsNullOrWhiteSpace(urls)) return "";
        foreach (var token in urls.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            if (token.Contains("://", StringComparison.OrdinalIgnoreCase) || token.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
                return token;
        return urls;
    }

    // ==============================================================================
    // JSON5 target pattern builder -- usable as grap in any command
    // ==============================================================================

    /// <summary>
    /// Build a JSON5 target pattern string for <paramref name="hWnd"/>.
    /// Format: {hwnd:0xXXXXXXXX,pid:NNN,title:'...',cls:'...',proc:'...',cid:NNN,domain:'...',url:'...'}
    /// domain/url are omitted if the window is not a browser or URL is unavailable.
    /// The returned string can be used as a grap pattern in any command.
    /// </summary>
    public static string BuildTargetJson5(IntPtr hWnd, string? overrideTitle = null)
    {
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
        var cls = GetClassName(hWnd);
        var proc = "";
        int cid = 0;
        proc = NativeMethods.TryGetProcessNameFast(pid) ?? "";
        try { cid = NativeMethods.GetDlgCtrlID(hWnd); } catch { }

        var title = overrideTitle ?? GetWindowText(hWnd);
        // Trim " - Chrome / Chromium / Microsoft Edge" suffix
        title = title.Split(new[] { " - Chrome", " - Chromium", " - Microsoft Edge" },
                            StringSplitOptions.None)[0].Trim();
        if (title.Length > 30) title = title[..30];
        title = title.Replace("'", "\\'");
        cls = (cls ?? "").Replace("'", "\\'");
        proc = (proc ?? "").Replace("'", "\\'");

        var domain = "";
        var url = "";
        try
        {
            // Skip Electron apps (VS Code, Codex, etc.) -- their GetBrowserUrl triggers
            // deep UIA tree traversal (30+s). Only actual browser processes need this.
            bool isElectronSkip = IsElectronWindow(cls, proc);
            url = isElectronSkip ? "" : GetPrimaryUrlToken(GetBrowserUrl(hWnd, pid));
            // Skip Electron internal URLs (not real web -- noise in grap pattern)
            if (!string.IsNullOrEmpty(url) &&
                (url.StartsWith("vscode-file://", StringComparison.OrdinalIgnoreCase) ||
                 url.StartsWith("chrome-extension://", StringComparison.OrdinalIgnoreCase) ||
                 url.StartsWith("devtools://", StringComparison.OrdinalIgnoreCase) ||
                 url.StartsWith("file:///", StringComparison.OrdinalIgnoreCase)))
            {
                url = "";
            }
            if (!string.IsNullOrEmpty(url))
            {
                var host = new Uri(url).Host;
                // Internal Electron hosts like vscode-app are not useful as a display domain.
                if (!string.IsNullOrEmpty(host) && !host.Equals("vscode-app", StringComparison.OrdinalIgnoreCase))
                    domain = host;
            }
        }
        catch { }

        // cmd: truncated command line (key args only, not exe path itself)
        string cmdField = "";
        try
        {
            var rawCmd = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
            if (!string.IsNullOrEmpty(rawCmd))
            {
                // Strip leading exe path token (up to first space after closing quote or first space)
                var stripped = rawCmd.TrimStart();
                if (stripped.StartsWith('"'))
                {
                    var end = stripped.IndexOf('"', 1);
                    if (end >= 0) stripped = stripped[(end + 1)..].TrimStart();
                }
                else
                {
                    var sp = stripped.IndexOf(' ');
                    if (sp >= 0) stripped = stripped[(sp + 1)..].TrimStart();
                    else stripped = "";
                }
                if (stripped.Length > 120) stripped = stripped[..120];
                cmdField = stripped.Replace("'", "\\'");
            }
        }
        catch { }

        var sb = new System.Text.StringBuilder();
        sb.Append($"{{hwnd:0x{hWnd.ToInt64():X8},pid:{pid}");
        if (!string.IsNullOrEmpty(proc)) sb.Append($",proc:'{proc}'");
        if (!string.IsNullOrEmpty(domain)) sb.Append($",domain:'{domain}'");
        // title intentionally omitted: titles mutate (tab switches, doc dirty
        // '*', browser tab flips), so including it in a copy-paste grap would
        // invite miss-match on the next invocation. hwnd+pid+proc+domain is
        // the stable identity set.
        _ = title; // keep the variable referenced so the compute path upstream
                   // (used by inspect / logs elsewhere) doesn't warn about unused.
        if (!string.IsNullOrEmpty(cls)) sb.Append($",cls:'{cls}'");
        if (cid != 0) sb.Append($",cid:{cid}");
        if (!string.IsNullOrEmpty(url)) sb.Append($",url:'{url.Replace("'", "\\'")}'");
        if (!string.IsNullOrEmpty(cmdField)) sb.Append($",cmd:'{cmdField}'");
        sb.Append('}');
        return sb.ToString();
    }

    // ==============================================================================
    // Multi-field JSON-like grap pattern  {key:value, key:"quoted", key:/regex/, key:[or1,or2]}
    // ==============================================================================

    /// <summary>
    /// Parse {key:value,...} multi-field pattern into field->value(s) dictionary.
    /// Values may be:
    ///   unquoted   ->  single token (no spaces / commas)
    ///   "quoted"   ->  substring match value (may contain spaces)
    ///   /regex/    ->  regex match value (inside {} only)
    ///   [a,b,"c"]  ->  OR list of values
    /// Returns null if the input doesn't look like a valid multi-field pattern.
    /// </summary>
    /// <summary>
    /// Public entry for JSON5-like multi-field grap parsing. Accepts either
    /// a brace-wrapped pattern ({proc:'x',pid:123}) or a pre-stripped inner.
    /// Returns null if the pattern is not parseable as multi-field.
    /// Exposed so non-window consumers (e.g. a11y kill) can reuse grap syntax
    /// without re-implementing the parser.
    /// </summary>
    public static Dictionary<string, List<string>>? TryParseMultiFieldPattern(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) return null;
        if (pattern.Length >= 2 && pattern[0] == '{' && pattern[^1] == '}')
            return ParseMultiFieldPattern(pattern);
        return null;
    }

    static Dictionary<string, List<string>>? ParseMultiFieldPattern(string pattern)
    {
        var inner = pattern[1..^1].Trim();
        if (string.IsNullOrEmpty(inner)) return null;

        var fields = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        int i = 0;

        while (i < inner.Length)
        {
            // Skip whitespace and commas between pairs
            while (i < inner.Length && (char.IsWhiteSpace(inner[i]) || inner[i] == ',')) i++;
            if (i >= inner.Length) break;

            // Read key -- supports quoted keys ("hwnd", 'hwnd') and bare keys (hwnd)
            string key;
            if (inner[i] == '"' || inner[i] == '\'')
            {
                key = ReadPatternValue(inner, ref i);   // reuse quoted-value reader
                // skip whitespace then ':'
                while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;
                if (i >= inner.Length || inner[i] != ':') return null;
                i++; // skip ':'
            }
            else
            {
                int keyStart = i;
                while (i < inner.Length && inner[i] != ':' && inner[i] != '}') i++;
                if (i >= inner.Length || inner[i] == '}') return null; // malformed
                key = inner[keyStart..i].Trim();
                i++; // skip ':'
            }
            if (string.IsNullOrEmpty(key)) return null;

            // Skip whitespace
            while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;
            if (i >= inner.Length) return null;

            // Read value(s)
            var values = new List<string>();

            if (inner[i] == '[')
            {
                // OR array: [val1, "val 2", val3]
                i++; // skip '['
                while (i < inner.Length && inner[i] != ']')
                {
                    while (i < inner.Length && (char.IsWhiteSpace(inner[i]) || inner[i] == ',')) i++;
                    if (i >= inner.Length || inner[i] == ']') break;
                    values.Add(ReadPatternValue(inner, ref i));
                }
                if (i < inner.Length && inner[i] == ']') i++; // skip ']'
            }
            else
            {
                // Single value (quoted or unquoted)
                values.Add(ReadPatternValue(inner, ref i));
            }

            if (values.Count > 0) fields[key] = values;
        }

        return fields.Count > 0 ? fields : null;
    }

    /// <summary>Read one value token (quoted or unquoted) from a pattern string at position i.</summary>
    static string ReadPatternValue(string s, ref int i)
    {
        if (i >= s.Length) return "";
        if (s[i] == '/')
        {
            int start = i++;
            bool escaped = false;
            while (i < s.Length)
            {
                var ch = s[i++];
                if (escaped) { escaped = false; continue; }
                if (ch == '\\') { escaped = true; continue; }
                if (ch == '/') return s[start..i];
            }
            return s[start..i];
        }
        // Quoted value -- supports both " and ' as delimiters
        if (s[i] == '"' || s[i] == '\'')
        {
            char quote = s[i++];
            int start = i;
            while (i < s.Length && s[i] != quote) i++;
            var val = s[start..i];
            if (i < s.Length) i++; // skip closing quote
            return val;
        }
        else
        {
            int start = i;
            while (i < s.Length && s[i] != ',' && s[i] != ']' && s[i] != '}') i++;
            return s[start..i].Trim();
        }
    }

    /// <summary>
    /// Electron-based apps (VS Code, Codex, Claude desktop, etc.) use Chrome_WidgetWin_1
    /// but their UIA trees are managed by the Chromium renderer, not classic Win32/UIA.
    /// Traversal can take 30+s -- skip UIA path building for these windows.
    /// </summary>
    public static bool IsElectronWindow(string cls, string proc)
    {
        if (!cls.Equals("Chrome_WidgetWin_1", StringComparison.OrdinalIgnoreCase)
            && !cls.Equals("Chrome_WidgetWin_0", StringComparison.OrdinalIgnoreCase))
            return false;
        // True browser processes are NOT Electron; skip them (GetBrowserUrl is fast via CDP).
        return !IsChromiumProcess(proc);
    }

    /// <summary>Public fast process-name lookup for use outside WindowFinder.</summary>
    public static string GetProcessNameCached2(uint pid)
        => NativeMethods.TryGetProcessNameFast(pid) ?? "";

    /// <summary>
    /// Returns true for processes that act as search-runners and always carry the current
    /// search query in their cmdline, causing false positives in cmd: fallback matching.
    /// Shells (bash/cmd/powershell) and wkappbot* workers are the main offenders.
    /// </summary>
    static bool IsSearcherProcess(string procName) =>
        procName.StartsWith("wkappbot", StringComparison.OrdinalIgnoreCase)
        || procName.Equals("bash",       StringComparison.OrdinalIgnoreCase)
        || procName.Equals("sh",         StringComparison.OrdinalIgnoreCase)
        || procName.Equals("cmd",        StringComparison.OrdinalIgnoreCase)
        || procName.Equals("powershell", StringComparison.OrdinalIgnoreCase)
        || procName.Equals("pwsh",       StringComparison.OrdinalIgnoreCase);

    static string NormalizeFieldPattern(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        if (value.Length >= 2 && value[0] == '/' && value[^1] == '/')
            return "regex:" + value[1..^1];
        return value;
    }

    /// <summary>
    /// Compute per-field match scores for any grap match (JSON5 or simple pattern).
    /// Discovers ALL available fields for the window (not just the ones specified in the pattern)
    /// so users can see which fields offer the most efficient grap alternatives.
    ///
    /// Score = pattern_literal_chars / actual_field_value_chars (capped at 1.0).
    ///   - Specified fields: coverage of the pattern against the real value.
    ///   - Un-specified fields: 1.0 (full exact match possible, candidate to add to grap).
    /// Sorted by score descending; tiebreak by value_length ascending (shorter = easier grap).
    /// </summary>
    static List<FieldScore> ComputeFieldScores(
        IntPtr hWnd, Dictionary<string, List<string>>? fields, Dictionary<uint, string>? procNameCache)
    {
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
        var cache = procNameCache ?? new Dictionary<uint, string>();

        // Collect actual values for all text fields
        // proc: "ownProc/parentProc" path (depth=2) for process ancestry context.
        string? ownProc = cache.TryGetValue(pid, out var pn) ? pn : null;
        if (ownProc == null)
        {
            ownProc = NativeMethods.TryGetProcessNameFast(pid) ?? "";
            cache[pid] = ownProc;
        }
        var proc = GetProcPath(pid, maxDepth: 2);
        var title  = GetWindowText(hWnd);
        // cls: "ownClass/parentClass" path (depth=2) for all windows including WPF.
        var cls = GetClassPath(hWnd, maxDepth: 2);
        string domain = "", url = "", cmd = "";
        // Only query browser URL for actual browser processes (Chrome, Edge, Firefox, Brave...).
        // IsLikelyBrowserWindow also matches Chrome_WidgetWin_1 for VS Code / Codex / Claude
        // (Electron apps) -- calling GetBrowserUrl on those triggers deep UIA tree traversal
        // that can take 30+s. IsChromiumProcess guards against that.
        bool needBrowserUrl = IsLikelyBrowserWindow(hWnd, cls, proc) && IsChromiumProcess(proc);
        if (needBrowserUrl)
        {
            try { url = GetBrowserUrl(hWnd, pid) ?? ""; } catch { }
            try { domain = ExtractDomainToken(url); } catch { }
        }
        // cmd: only fetch when explicitly requested in the pattern (expensive WMI/NTAPI call)
        bool needCmd = fields != null && fields.ContainsKey("cmd");
        if (needCmd)
            try { cmd = NativeMethods.GetProcessCommandLine((int)pid) ?? ""; } catch { }

        // All candidate fields in preferred display order.
        // cmd/url are expensive and less commonly needed -- only include if requested in fields dict.
        bool wantCmd = fields != null && fields.ContainsKey("cmd");
        bool wantUrl = fields != null && fields.ContainsKey("url");
        var candidates = new List<(string Field, string Actual)>
        {
            ("domain", domain),
            ("proc",   proc),
            ("cls",    cls),
            ("title",  title),
        };
        if (wantUrl || !string.IsNullOrEmpty(url))   candidates.Add(("url",  url));
        if (wantCmd && !string.IsNullOrEmpty(cmd))   candidates.Add(("cmd",  cmd));

        var scores = new List<FieldScore>();
        foreach (var (field, actual) in candidates)
        {
            if (string.IsNullOrEmpty(actual)) continue;

            double score;
            string pattern;

            if (fields != null && fields.TryGetValue(field, out var values))
            {
                // Specified field: coverage score = Σ(matched chars) / field_length.
                // For path patterns (cls:'A/B'), score against the matching path segment only.
                // OR array [a,b,c] matches MORE windows → less discriminating → score / n.
                pattern = values[0];
                score = 0;
                foreach (var v in values)
                {
                    double s;
                    if (v.Contains('/') || v.Contains("**"))
                    {
                        // Path pattern: score each segment of actual against corresponding segment of pattern.
                        // Use best-matching segment pair's score.
                        var pathSegs = actual.Split('/');
                        var patSegs  = v.Split('/').Where(p => p != "**").ToArray();
                        s = patSegs.Length == 0 ? 0.0
                            : pathSegs.Max(ps => patSegs.Max(pp => GlobCoverageScore(pp, ps)));
                    }
                    else
                    {
                        // Simple pattern: match against own class (first path segment)
                        var ownSeg = actual.Split('/')[0];
                        s = GlobCoverageScore(v, ownSeg);
                        if (s == 0) s = GlobCoverageScore(v, actual); // fallback: full path
                    }
                    if (s > score) { score = s; pattern = v; }
                }
                if (values.Count > 1) score /= values.Count;
            }
            else
            {
                // Un-specified field: full exact match is possible → score 1.0 (candidate)
                pattern = actual.Length > 30 ? actual[..30] : actual;
                score = 1.0;
            }

            var display = GetMatchContext(actual, pattern, contextChars: 12);
            scores.Add(new FieldScore(field, pattern, display, score));
        }

        // Sort: score descending, tiebreak by value length ascending (shorter = easier grap)
        scores.Sort((a, b) =>
        {
            var cmp = b.Score.CompareTo(a.Score);
            return cmp != 0 ? cmp : a.Matched.Length.CompareTo(b.Matched.Length);
        });
        return scores;
    }

    /// <summary>
    /// Coverage score: Σ(segment_len × occurrence_count) / field_len, capped at 1.0.
    /// Regex forms: "re:pat" (grap CLI), "/pat/" or "~pat~" (skill search / bash-safe).
    /// Regex scored by literal-char-count (symbols penalized); exact match → score 1.0.
    /// Glob: split on '*'/'?' → each segment counted independently; multi-hit boosts score.
    /// </summary>
    public static double GlobCoverageScore(string pattern, string field)
    {
        if (field.Length == 0) return 0.0;

        // Detect regex form and extract inner pattern string.
        string? rxPat = null;
        if (pattern.Length > 3 && pattern.StartsWith("re:", StringComparison.OrdinalIgnoreCase))
            rxPat = pattern[3..];
        else if (pattern.Length > 2 && pattern[0] == '/' && pattern[^1] == '/')
            rxPat = pattern[1..^1];
        else if (pattern.Length > 2 && pattern[0] == '~' && pattern[^1] == '~')
            rxPat = pattern[1..^1];

        if (rxPat != null)
        {
            // Score = literal-char-count / field_len (symbols are "free" — naturally penalized).
            int litCount = 0;
            bool escaped = false;
            foreach (var ch in rxPat)
            {
                if (escaped) { litCount++; escaped = false; continue; }
                if (ch == '\\') { escaped = true; continue; }
                if (".*+?^${}()|[]".IndexOf(ch) < 0) litCount++;
            }
            try
            {
                var rx = new System.Text.RegularExpressions.Regex(rxPat,
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                    System.Text.RegularExpressions.RegexOptions.CultureInvariant);
                if (!rx.IsMatch(field)) return 0.0;
            }
            catch { return 0.0; }
            return Math.Min(1.0, (double)litCount / field.Length);
        }

        // Glob: split on '*' and '?' → count each literal segment independently.
        var segments = pattern.Split('*', '?');
        int total = 0;
        foreach (var seg in segments)
        {
            if (seg.Length == 0) continue;
            total += CountOccurrencesCI(field, seg) * seg.Length;
        }
        return total == 0 ? 0.0 : Math.Min(1.0, (double)total / field.Length);
    }

    /// <summary>Non-overlapping case-insensitive substring count.</summary>
    public static int CountOccurrencesCI(string haystack, string needle)
    {
        if (needle.Length == 0) return 0;
        int count = 0, idx = 0;
        while ((idx = haystack.IndexOf(needle, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
        {
            count++;
            idx += needle.Length;
        }
        return count;
    }

    /// <summary>
    /// Returns a display snippet showing where pattern matched in actual.
    /// Path fields (containing '/'): shows the matched segment + adjacent segments.
    /// Other fields: shows contextChars chars before/after the match position.
    /// Falls back to truncated actual if no literal part found.
    /// </summary>
    static string GetMatchContext(string actual, string pattern, int contextChars = 12)
    {
        if (actual.Length == 0) return actual;

        // Extract literal part from glob pattern (strip * ? and re: prefix)
        string lit = pattern;
        if (lit.StartsWith("re:", StringComparison.OrdinalIgnoreCase)) lit = lit[3..];
        else if ((lit.StartsWith("/") && lit.EndsWith("/")) || (lit.StartsWith("~") && lit.EndsWith("~")))
            lit = lit[1..^1];
        lit = System.Text.RegularExpressions.Regex.Replace(lit, @"[\*\?\[\]]", "");
        if (lit.Length == 0) return actual.Length > 30 ? actual[..30] + "…" : actual;

        // Path field: find which segment contains the match, show segment + neighbors
        if (actual.Contains('/'))
        {
            var segs = actual.Split('/');
            for (int i = 0; i < segs.Length; i++)
            {
                if (segs[i].IndexOf(lit, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Show: prev…matched/next (or just matched if neighbors are long)
                    var prev = i > 0 ? "…/" : "";
                    var next = i < segs.Length - 1 ? "/" + segs[i + 1] : "";
                    var result = prev + segs[i] + next;
                    return result.Length > 40 ? result[..40] + "…" : result;
                }
            }
        }

        // Plain string: find match position, show context chars around it
        int idx = actual.IndexOf(lit, StringComparison.OrdinalIgnoreCase);
        if (idx < 0) return actual.Length > 30 ? actual[..30] + "…" : actual;

        int start = Math.Max(0, idx - contextChars);
        int end   = Math.Min(actual.Length, idx + lit.Length + contextChars);
        var snippet = (start > 0 ? "…" : "") + actual[start..end] + (end < actual.Length ? "…" : "");
        return snippet;
    }

    /// <summary>
    /// Glob-path match for cls ancestor chains. Each segment is auto-wrapped as substring.
    /// Supports GitHub-style ** (zero or more segments).
    /// Pattern "나와" matches any path containing a segment with "나와" (substring).
    /// Pattern "_NKHero**" matches main class and all descendants.
    /// Pattern "**/_NKHeroDialog*" matches any dialog regardless of depth.
    /// Pattern "Afx*/_NKHeroMain*" matches Afx child of _NKHeroMain parent.
    /// </summary>
    static bool MatchClassPath(string pattern, string path)
    {
        // Normalize each segment to substring (auto-wrap with *...*), except ** which stays as-is.
        var patSegs = pattern.Split('/');
        var normalized = string.Join("/", patSegs.Select(seg =>
            seg == "**" ? "**" : PatternMatcher.EnsureSubstring(seg)));
        // Use CreatePathGlob for proper anchored path matching
        var m = PatternMatcher.CreatePathGlob(normalized);
        return m.IsMatch(path);
    }

    static string ExtractDomainToken(string? urls)
    {
        if (string.IsNullOrEmpty(urls)) return "";
        foreach (var tok in urls.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            try { return new Uri(tok).Host; } catch { }
        }
        return urls.Length > 30 ? urls[..30] : urls;
    }

    /// <summary>
    /// Find windows matching all fields in a parsed multi-field pattern (AND between fields, OR within each field's value list).
    /// </summary>
    static List<WindowInfo> FindByMultiField(Dictionary<string, List<string>> fields, bool stopOnFirstMatch = false)
    {
        // -- hwnd-only: direct lookup --
        if (fields.Count == 1 && fields.TryGetValue("hwnd", out var hwndOnlyVals))
        {
            var h = ParseHwndField(hwndOnlyVals[0]);
            if (h != IntPtr.Zero && NativeMethods.IsWindow(h))
                return [WindowInfo.FromHwnd(h)];
            return [];
        }

        // -- Parse static filters --
        IntPtr hwndFilter = IntPtr.Zero;
        if (fields.TryGetValue("hwnd", out var hwndVals))
            hwndFilter = ParseHwndField(hwndVals[0]);

        uint pidFilter = 0;
        if (fields.TryGetValue("pid", out var pidVals))
            pidFilter = (uint)ParseNumericField(pidVals[0]);

        int? cidFilter = null;
        if (fields.TryGetValue("cid", out var cidVals))
            cidFilter = (int)ParseNumericField(cidVals[0]);

        var titleMatchers  = fields.TryGetValue("title",  out var tv) ? tv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;
        var clsMatchers    = fields.TryGetValue("cls",    out var cv) ? cv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;
        var procMatchers   = fields.TryGetValue("proc",   out var pv) ? pv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;
        var domainMatchers = fields.TryGetValue("domain", out var dv) ? dv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;
        var urlMatchers    = fields.TryGetValue("url",    out var uv) ? uv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;
        var cmdMatchers    = fields.TryGetValue("cmd",    out var kv) ? kv.Select(v => PatternMatcher.Create(NormalizeFieldPattern(v))).ToList() : null;

        var results = new List<WindowInfo>();
        var procNameCache = new Dictionary<uint, string>();
        var focus = FocusSnapshot.CaptureNow();

        // -- If hwnd specified: hwnd is AUTHORITATIVE --
        // Explicit hwnd in the grap dict means "I already resolved this window
        // and want exactly this one." Do NOT re-validate the other fields
        // (proc/title/cls/domain) because they were captured at probe time and
        // can drift (dialog title rewrites, proc case, domain empty on new tab
        // navigation). Prior behavior: any drifted field dropped the result to
        // empty, and callers fell back to closest-match enumeration and
        // returned a different sibling popup -- the exact bug reported for
        // Hero4/HeroGlobal with 4+ #32770 popups open.
        if (hwndFilter != IntPtr.Zero)
        {
            if (NativeMethods.IsWindow(hwndFilter))
                results.Add(WindowInfo.FromHwnd(hwndFilter));
            return results;
        }

        // -- Enumerate all visible windows --
        // Build pid->hwnd map in the same pass (used by secondary windowless-proc scan below).
        var pidToHwnd = new Dictionary<uint, IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint p);
            if (!pidToHwnd.ContainsKey(p)) pidToHwnd[p] = hWnd;

            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            if (MatchesMultiField(hWnd, pidFilter, cidFilter, titleMatchers, clsMatchers, procMatchers,
                    domainMatchers, urlMatchers, procNameCache, cmdMatchers))
            {
                var info = WindowInfo.FromHwnd(hWnd);
                if (fields.Count > 1)
                    info.FieldScores = ComputeFieldScores(hWnd, fields, procNameCache);
                results.Add(info);
                if (stopOnFirstMatch) return false;
            }
            return true;
        }, IntPtr.Zero);

        // -- MDI child scan --
        // EnumWindows above misses MDI document children (e.g. Heroes4 nkrunlite.exe
        // "[0156]..." panels under MDIClient). When caller scoped to a specific process
        // (proc: or pid:), walk the MDIClient of each matching frame and apply
        // title/cls matchers to its children.
        if ((!stopOnFirstMatch || results.Count == 0) &&
            (pidFilter != 0 || procMatchers != null))
        {
            var framePids = new HashSet<uint>();
            foreach (var kv2 in pidToHwnd)
            {
                var framePid = kv2.Key;
                if (pidFilter != 0 && framePid != pidFilter) continue;
                if (procMatchers != null)
                {
                    if (!procNameCache.TryGetValue(framePid, out var pn2))
                    {
                        pn2 = NativeMethods.TryGetProcessNameFast(framePid) ?? "";
                        procNameCache[framePid] = pn2;
                    }
                    if (string.IsNullOrEmpty(pn2)) continue;
                    if (!procMatchers.Any(m => m.IsMatch(pn2))) continue;
                }
                framePids.Add(framePid);
            }

            foreach (var fpid in framePids)
            {
                var frame = FindMDIMainFrame(fpid);
                if (frame == IntPtr.Zero) continue;
                var hMdiClient = NativeMethods.FindWindowExW(frame, IntPtr.Zero, "MDIClient", null);
                if (hMdiClient == IntPtr.Zero) continue;

                foreach (var mc in GetChildrenZOrder(hMdiClient))
                {
                    var mcHwnd = mc.Handle;
                    if (!NativeMethods.IsWindowVisible(mcHwnd)) continue;
                    if (results.Any(r => r.Handle == mcHwnd)) continue;
                    if (cidFilter.HasValue && mc.ControlId != cidFilter.Value) continue;
                    if (titleMatchers != null && !titleMatchers.Any(m => m.IsMatch(mc.Title ?? ""))) continue;
                    if (clsMatchers   != null && !clsMatchers.Any(m => m.IsMatch(mc.ClassName ?? ""))) continue;
                    mc.FieldScores = ComputeFieldScores(mcHwnd, fields, procNameCache);
                    results.Add(mc);
                    if (stopOnFirstMatch) break;
                }
                if (stopOnFirstMatch && results.Count > 0) break;
            }
        }

        // -- Secondary scan: windowless processes matching proc:/cmd: --
        // Handles processes with no own top-level window (e.g. terminal-hosted CLI tools).
        // For each matching process, walk parent PID chain to find effective host window.
        // proc-name scan: always run when procMatchers present (fast -- no cmdline read here).
        // cmd-line scan: only run when no results yet (GetProcessCommandLine is 30+s across all procs).
        if (procMatchers != null || (cmdMatchers != null && results.Count == 0))
        {
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    var pid = (uint)proc.Id;
                    if (pidToHwnd.ContainsKey(pid)) continue; // has own window -- already handled above

                    var procName = proc.ProcessName;
                    if (procMatchers != null && !procMatchers.Any(m => m.IsMatch(procName))) continue;

                    string cmdLine = "";
                    string matchedToken = "";
                    if (cmdMatchers != null)
                    {
                        cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
                        if (!cmdMatchers.Any(m => m.IsMatch(cmdLine))) continue;
                        var tokens = cmdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        matchedToken = tokens.FirstOrDefault(t => cmdMatchers.Any(m => m.IsMatch(t)))
                                       ?? (cmdLine.Length > 60 ? cmdLine[..60] : cmdLine);
                        matchedToken = matchedToken.Trim('"', '\'');
                        if (matchedToken.Length > 60) matchedToken = matchedToken[..60];
                    }

                    // Always include as process-only result with cmdline -- proc: search shows every
                    // matching process regardless of window ownership.
                    if (results.Any(r => r.IsProcessOnly && r.ProcessId == pid)) continue;
                    if (string.IsNullOrEmpty(cmdLine))
                        cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? "";
                    results.Add(WindowInfo.FromProcess(pid, procName, cmdLine));
                }
                catch { }
                finally { try { proc.Dispose(); } catch { } }
            }
        }

        // Sort: focus priority
        if (results.Count > 1)
            results.Sort((a, b) => focus.GetPriority(b.Handle).CompareTo(focus.GetPriority(a.Handle)));

        return results;
    }

    /// <summary>
    /// Walk parent PID chain to find the nearest ancestor that owns a top-level window.
    /// Returns IntPtr.Zero if no windowed ancestor found within 8 hops.
    /// </summary>
    static IntPtr ResolveEffectiveHwnd(uint pid, Dictionary<uint, IntPtr> pidToHwnd)
    {
        int cur = (int)pid;
        for (int depth = 0; depth < 8; depth++)
        {
            int parent = NativeMethods.GetParentProcessId(cur);
            if (parent <= 0 || parent == cur) break;
            if (pidToHwnd.TryGetValue((uint)parent, out var ph)) return ph;
            cur = parent;
        }
        return IntPtr.Zero;
    }

    // Parse hwnd or pid value: auto-detects decimal vs hex.
    // Rules: "0x..." prefix -> hex; contains a-f chars -> hex; digits-only -> decimal.
    static IntPtr ParseHwndField(string s) => new IntPtr((long)ParseNumericField(s));

    static ulong ParseNumericField(string s)
    {
        s = s.Trim();
        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            ulong.TryParse(s[2..], System.Globalization.NumberStyles.HexNumber, null, out var v);
            return v;
        }
        // No 0x prefix -> always decimal
        ulong.TryParse(s, out var dv);
        return dv;
    }

    static bool MatchesMultiField(
        IntPtr hWnd, uint pidFilter, int? cidFilter,
        List<PatternMatcher>? titleMatchers, List<PatternMatcher>? clsMatchers, List<PatternMatcher>? procMatchers,
        List<PatternMatcher>? domainMatchers, List<PatternMatcher>? urlMatchers,
        Dictionary<uint, string> procNameCache, List<PatternMatcher>? cmdMatchers = null)
    {
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);

        // pid
        if (pidFilter != 0 && pid != pidFilter) return false;

        // cid
        if (cidFilter.HasValue)
        {
            int cid = 0;
            try { cid = NativeMethods.GetDlgCtrlID(hWnd); } catch { }
            if (cid != cidFilter.Value) return false;
        }

        // title (OR within list)
        if (titleMatchers != null)
        {
            var title = GetWindowText(hWnd);
            if (!titleMatchers.Any(m => m.IsMatch(title))) return false;
        }

        // cls: glob path matching.
        // Simple "ClassName"  → own class + ancestor segments (finds main class AND all descendants).
        // Path "A/B" or "A/**/B" → full glob-path match against ancestor chain.
        if (clsMatchers != null)
        {
            var cls = GetClassName(hWnd);
            bool clsMatch = clsMatchers.Any(m =>
            {
                if (m.IsMatch(cls)) return true; // own class -- fast path
                var path = GetClassPath(hWnd, maxDepth: 5);
                // Path pattern (contains '/' or '**'): use glob-path segment matching
                var rawPat = m.RawPattern;
                if (!string.IsNullOrEmpty(rawPat) && (rawPat.Contains('/') || rawPat.Contains("**")))
                    return MatchClassPath(rawPat, path);
                // Simple pattern: match any ancestor segment (e.g. cls:'_NKHeroMainClass' finds all children)
                return path.Split('/').Skip(1).Any(seg => m.IsMatch(seg));
            });
            if (!clsMatch) return false;
        }

        // proc: own process name + parent process chain (path patterns supported).
        // Simple proc:'X' → own proc name (fast, current behavior).
        // Path proc:'child/parent' or proc:'**/parent' → ancestor process chain.
        if (procMatchers != null)
        {
            if (!procNameCache.TryGetValue(pid, out var proc))
            {
                try { proc = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { proc = ""; }
                procNameCache[pid] = proc;
            }
            string cmdLine = "";
            try { cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? ""; } catch { }
            bool procMatch = procMatchers.Any(m =>
            {
                if (m.MatchAny(proc, cmdLine)) return true; // own proc -- fast path
                var rawPat = m.RawPattern;
                if (string.IsNullOrEmpty(rawPat) || (!rawPat.Contains('/') && !rawPat.Contains("**")))
                    return false; // simple pattern -- only own proc
                // Path pattern: walk parent process chain
                var procPath = GetProcPath(pid, maxDepth: 4);
                return MatchClassPath(rawPat, procPath); // reuse same glob-path logic
            });
            if (!procMatch) return false;
        }

        // cmd: match against process command line args
        if (cmdMatchers != null)
        {
            string cmdLine = "";
            try { cmdLine = NativeMethods.GetProcessCommandLine((int)pid) ?? ""; } catch { }
            if (!cmdMatchers.Any(m => m.IsMatch(cmdLine))) return false;
        }

        // domain / url (lazy fetch)
        if (domainMatchers != null || urlMatchers != null)
        {
            var url = GetBrowserUrl(hWnd, pid);
            if (domainMatchers != null)
            {
                // CDP Tier 1 returns space-separated multi-URL (all open tabs) -- check ALL, not just first
                bool domainMatched = false;
                foreach (var tok in (url ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        var dom = new Uri(tok).Host;
                        if (domainMatchers.Any(m => m.IsMatch(dom))) { domainMatched = true; break; }
                    }
                    catch { }
                }
                if (!domainMatched) return false;
            }
            if (urlMatchers != null && !urlMatchers.Any(m => m.IsMatch(url ?? "")))
                return false;
        }

        return true;
    }
}

/// <summary>
/// Snapshot of window properties at query time.
/// </summary>
/// <summary>Per-field match score for JSON5 grap patterns. Field=field name, Score=literal_chars/field_len (higher=more discriminating).</summary>
public record FieldScore(string Field, string Pattern, string Matched, double Score);

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
    /// <summary>Which field caused this window to match (title/proc/domain/url/cmd/json5).</summary>
    public string? MatchedVia { get; set; }
    /// <summary>Snippet of the matched value (truncated), for injecting into display grap.</summary>
    public string? MatchedSnippet { get; set; }
    /// <summary>Per-field scores for JSON5 multi-field patterns; sorted by Score descending.</summary>
    public List<FieldScore>? FieldScores { get; set; }
    /// <summary>True when this result represents a windowless process (no HWND). Handle is Zero.</summary>
    public bool IsProcessOnly { get; set; }
    /// <summary>PID for process-only results where Handle is Zero.</summary>
    public uint ProcessId { get; set; }

    public static WindowInfo FromProcess(uint pid, string procName, string cmdLine) => new WindowInfo
    {
        Handle = IntPtr.Zero,
        IsProcessOnly = true,
        ProcessId = pid,
        Title = cmdLine.Length > 0 ? cmdLine : procName,
        ClassName = procName,
        IsVisible = false,
        MatchedVia = "proc",
        MatchedSnippet = procName,
        Coverage = 1.0,
    };

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
