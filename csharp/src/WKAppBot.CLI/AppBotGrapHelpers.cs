using System.Text.RegularExpressions;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// Grap building helpers: compact window address strings for # TARGET / hack-hover output.
internal partial class Program
{
    /// <summary>
    /// Compact window grap for # TARGET / hack-hover lines -- copy-paste ready, noise-free.
    /// Rules: no hwnd/pid; browser->domain or file:'name'; non-browser->{proc,cls}; no title/url.
    /// </summary>
    internal static string BuildCompactWinGrap(IntPtr hwnd) => _BuildCompactWinGrapCore(hwnd);

    /// <summary>
    /// Inject hwnd:0x... into any compact grap string -- shared helper for # FOCUS / # TARGET.
    /// Always produces a JSON5 object with hwnd as last field.
    /// </summary>
    internal static string InjectHwnd(string grap, IntPtr hwnd)
    {
        var hwndStr = $"hwnd:0x{hwnd.ToInt64():X8}";
        if (grap.StartsWith('{'))
            return grap.TrimEnd('}') + $",{hwndStr}}}";
        return $"{{{grap},{hwndStr}}}";
    }

    /// <summary>
    /// Build a fully-resolved target grap string for display output.
    /// compact portable form (domain/proc/file) + hwnd injected.
    /// </summary>
    internal static string BuildTargetGrap(IntPtr hwnd, uint pid = 0)
    {
        if (pid == 0) WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out pid);
        return InjectHwnd(BuildCompactWinGrap(hwnd), hwnd);
    }

    /// <summary>
    /// Build a target grap that includes the currently focused leaf's absolute UIA path when available.
    /// Falls back to the plain target grap if the focused leaf cannot be resolved.
    /// </summary>
    internal static string BuildTargetGrapWithFocusPath(IntPtr hwnd)
        => AppendFocusPath(BuildTargetGrap(hwnd), hwnd);

    /// <summary>
    /// Build a target grap from a verified window hit and append the focused leaf path.
    /// </summary>
    internal static string BuildTargetGrapWithFocusPath(WKAppBot.Win32.Window.WindowInfo hit)
        => AppendFocusPath(BuildTargetGrap(hit), hit.Handle);

    /// <summary>Quote a grap expression so the whole token can be pasted into a shell as one argument.</summary>
    internal static string QuoteGrapExpression(string grap)
        => "\"" + grap.Replace("\"", "\\\"") + "\"";

    /// <summary>
    /// Walk Win32 parent chain from focusHwnd up to (exclusive) rootHwnd.
    /// Returns intermediate child HWNDs in root-first order, or empty if
    /// focusHwnd is not a descendant of rootHwnd.
    /// </summary>
    internal static List<IntPtr> GetFocusedChildChain(IntPtr rootHwnd)
    {
        var gti = new WKAppBot.Win32.Native.NativeMethods.GUITHREADINFO
            { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<WKAppBot.Win32.Native.NativeMethods.GUITHREADINFO>() };
        if (!WKAppBot.Win32.Native.NativeMethods.GetGUIThreadInfo(0, ref gti)) return [];
        var focusHwnd = gti.hwndFocus;
        if (focusHwnd == IntPtr.Zero || focusHwnd == rootHwnd) return [];

        var chain = new List<IntPtr>();
        var current = focusHwnd;
        while (current != IntPtr.Zero && current != rootHwnd)
        {
            chain.Insert(0, current);
            current = WKAppBot.Win32.Native.NativeMethods.GetParent(current);
        }
        return current == rootHwnd ? chain : [];
    }

    // Max ms to spend building the UIA focus path. VS Code / Electron apps have
    // deeply nested trees that can take 30+s without a guard. Fall back to the
    // plain JSON5 grap (no #scope) when the timeout fires.
    private const int FocusPathTimeoutMs = 1500;

    /// <summary>
    /// Run UIA work on a dedicated STA thread with a hard timeout.
    /// IsBackground=true means the thread doesn't block process exit even if stuck.
    /// STA is required for FlaUI / UIAutomation COM objects.
    /// </summary>
    internal static T RunWithUiaTimeout<T>(Func<T> work, T fallback, int timeoutMs = FocusPathTimeoutMs)
    {
        T result = fallback;
        var thread = new System.Threading.Thread(() =>
        {
            try { result = work(); } catch { }
        });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true; // won't block process exit if we abandon it
        thread.Start();
        if (!thread.Join(timeoutMs))
        {
            // Thread is still running; we abandon it (IsBackground ensures exit isn't blocked).
            // Log to stderr so the user knows why the path was omitted.
            Console.Error.WriteLine($"[GRAP] UIA path timed out ({timeoutMs}ms) -- falling back to JSON5 only");
        }
        return result;
    }

    private static string AppendFocusPath(string target, IntPtr hwnd)
        => RunWithUiaTimeout(() => AppendFocusPathCore(target, hwnd), target);

    /// <summary>
    /// Run multiple UIA operations on a SINGLE STA thread to avoid COM serialization.
    /// 5 separate RunWithUiaTimeout() calls each spawn a new STA thread; COM serializes
    /// them → 5×7s = 35s delay. Use this batch version to run them all in one thread.
    /// </summary>
    internal static void RunAllUiaInOneBatch(Action uiaWork, int timeoutMs = 2000)
    {
        var thread = new System.Threading.Thread(() => { try { uiaWork(); } catch { } });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();
        if (!thread.Join(timeoutMs))
            Console.Error.WriteLine($"[GRAP] UIA batch timed out ({timeoutMs}ms)");
    }

    // Public alias for batch UIA thread use
    internal static string AppendFocusPathCore_Public(string target, IntPtr hwnd)
        => AppendFocusPathCore(target, hwnd);

    private static string AppendFocusPathCore(string target, IntPtr hwnd)
    {
        try
        {
            // For MDI/legacy multi-window apps: if keyboard focus is in a Win32 child HWND,
            // build the /childGrap chain before the #uiaPath scope.
            var childChain = GetFocusedChildChain(hwnd);
            IntPtr uiaRoot = hwnd;
            string winPath = target;
            if (childChain.Count > 0)
            {
                WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint rootPid);
                string rootProc = "";
                try { rootProc = System.Diagnostics.Process.GetProcessById((int)rootPid).ProcessName.Replace("'", "\\'"); } catch { }
                var sb = new System.Text.StringBuilder(target);
                foreach (var childHwnd in childChain)
                {
                    var cls = WKAppBot.Win32.Window.WindowFinder.GetClassName(childHwnd);
                    if (!string.IsNullOrEmpty(cls))
                        sb.Append($"/{{proc:'{rootProc}',cls:'{cls.Replace("'", "\\'")}'}}");;
                }
                winPath = sb.ToString();
                uiaRoot = childChain[^1]; // UIA path from the innermost child hwnd
            }

            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(uiaRoot);
            if (root == null) return winPath;

            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, uiaRoot);
            if (focused == null) return winPath;

            var path = WKAppBot.Win32.Accessibility.GrapHelper.BuildAbsoluteTagPath(
                focused, uia.TreeWalkerFactory.GetRawViewWalker(), 15);
            if (string.IsNullOrWhiteSpace(path) || path == "?") return winPath;

            var full = $"{winPath}#{path}";
            if (full.Length <= 255) return full;

            // Over atom limit: compact to first/**/last-3-segments
            // e.g. Pan_1th/Gro_uuid/.../Gro_root/Gro**/Edi -> Pan_1th/**/Gro_root/Gro**/Edi
            var segs = path.Split('/');
            if (segs.Length > 4)
            {
                var compact = $"{segs[0]}/**/{string.Join("/", segs[^3..])}";
                var compactFull = $"{winPath}#{compact}";
                if (compactFull.Length <= 255) return compactFull;
            }
            // Fallback: name wildcard only
            var elName = ""; try { elName = focused.Properties.Name.ValueOrDefault ?? ""; } catch { }
            if (!string.IsNullOrEmpty(elName))
                return $"{winPath}#*{elName}*";
            return winPath;
        }
        catch
        {
            return target;
        }
    }

    // Get the focused leaf element's XML tag for TARGET block display.
    internal static string GetFocusedLeafTag(IntPtr hwnd)
        => RunWithUiaTimeout(() =>
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return "";
            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, hwnd);
            if (focused == null) return "";
            return FormatLeafTag(focused);
        }, fallback: "", timeoutMs: FocusPathTimeoutMs);

    /// <summary>
    /// BuildTargetGrap overload accepting WindowInfo with MatchedVia/MatchedSnippet.
    /// Injects the matched field into the grap when not already visible in compact form.
    /// </summary>
    internal static string BuildTargetGrap(WKAppBot.Win32.Window.WindowInfo hit)
    {
        if (hit.MatchedVia == "child-cmd" && !string.IsNullOrEmpty(hit.MatchedSnippet))
        {
            var sep = hit.MatchedSnippet.IndexOf(':');
            if (sep > 0)
            {
                var childProc = hit.MatchedSnippet[..sep].Replace("'", "\\'");
                var token     = hit.MatchedSnippet[(sep + 1)..].Replace("'", "\\'");
                if (token.Length > 60) token = token[..60];
                var childCompact = $"{{proc:'{childProc}',cmd:'{token}'}}";
                return InjectHwnd(childCompact, hit.Handle);
            }
        }

        var compact = BuildCompactWinGrap(hit.Handle);

        if (!compact.Contains("domain:") && !compact.Contains("file:"))
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hit.Handle, out uint hitPid);
            try
            {
                var rawUrl = WKAppBot.Win32.Window.WindowFinder.GetBrowserUrl(hit.Handle, hitPid);
                if (TryBuildDisplayBrowserInject(rawUrl, out var inject) && !string.IsNullOrEmpty(inject))
                    compact = compact.StartsWith('{')
                        ? compact.TrimEnd('}') + $",{inject}}}"
                        : $"{{{inject}}}";
            }
            catch { }
        }

        if (!compact.Contains("domain:") && !compact.Contains("file:") && !compact.Contains("cls:"))
        {
            var cls = WKAppBot.Win32.Window.WindowFinder.GetClassName(hit.Handle);
            if (!string.IsNullOrEmpty(cls))
            {
                cls = cls.Replace("'", "\\'");
                compact = compact.StartsWith('{')
                    ? compact.TrimEnd('}') + $",cls:'{cls}'}}"
                    : $"{{cls:'{cls}'}}";
            }
        }

        if (!string.IsNullOrEmpty(hit.MatchedVia) && !string.IsNullOrEmpty(hit.MatchedSnippet))
        {
            bool alreadyVisible = hit.MatchedVia switch
            {
                "domain" => compact.Contains("domain:"),
                "url"    => compact.Contains("domain:") || compact.Contains("file:"),
                "proc"   => compact.Contains("proc:"),
                "cmd"    => compact.Contains("cmd:") || compact.Contains("file:") || compact.Contains("domain:"),
                "uia"    => compact.Contains("uia:"),
                _        => true
            };
            if (!alreadyVisible)
            {
                var snippet = hit.MatchedSnippet.Replace("'", "\\'");
                if (snippet.Length > 60) snippet = snippet[..60];
                var inject = $"{hit.MatchedVia}:'{snippet}'";
                compact = compact.StartsWith('{')
                    ? compact.TrimEnd('}') + $",{inject}}}"
                    : $"{{{inject}}}";
            }
        }
        return InjectHwnd(compact, hit.Handle);
    }

    private static bool TryBuildDisplayBrowserInject(string? rawUrl, out string inject)
    {
        inject = "";
        if (string.IsNullOrWhiteSpace(rawUrl)) return false;

        var primary = rawUrl.Split(' ')[0];
        if (primary.StartsWith("vscode-file://", StringComparison.OrdinalIgnoreCase) ||
            primary.StartsWith("chrome-extension://", StringComparison.OrdinalIgnoreCase) ||
            primary.StartsWith("devtools://", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (primary.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
        {
            var fn = System.IO.Path.GetFileName(Uri.UnescapeDataString(primary));
            if (string.IsNullOrEmpty(fn)) return false;
            inject = $"file:'{fn.Replace("'", "\\'")}'";
            return true;
        }

        try
        {
            var host = new Uri(primary).Host;
            if (string.IsNullOrEmpty(host) ||
                host.Equals("vscode-app", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            inject = $"domain:'{host.Replace("'", "\\'")}'";
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string _BuildCompactWinGrapCore(IntPtr hwnd)
    {
        var g = WindowFinder.BuildTargetJson5(hwnd);
        // Hot-swap leaves the previous core running under `<name>.old-<stamp>`
        // until it exits. Portable patterns must stay stable across that
        // rename, otherwise the auto-find verify pass reports MISS every time
        // the user hits a stale hwnd belonging to the old core. Normalize
        // here before other regex passes see the proc value.
        g = Regex.Replace(g, @"(proc:'[^']*?)\.old-\d{8}-\d{4,6}(')", "$1$2");
        g = Regex.Replace(g, @",?hwnd:0x[0-9A-Fa-f]+,?", ",");
        g = Regex.Replace(g, @",?pid:\d+,?", ",");
        g = Regex.Replace(g, @",title:'[^']*'", "");
        g = Regex.Replace(g, @",url:'[^']*'", "");
        g = Regex.Replace(g, @",cmd:'[^']*'", "");
        if (g.Contains("domain:"))
        {
            g = Regex.Replace(g, @",?proc:'[^']*',?", ",");
            g = Regex.Replace(g, @",?cls:'[^']*',?", ",");
            var m = Regex.Match(g, @"domain:'([^']*)'");
            if (m.Success) return $"{{domain:'{m.Groups[1].Value}'}}";
        }
        else
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid2);
            try
            {
                // Skip GetBrowserUrl for Electron apps (VS Code, Codex, Claude desktop):
                // their UIA trees are too deep and GetBrowserUrl via UIA takes 30+s.
                // Only use CDP-exposed URL (wkappbot.cdp property) for file:/// detection.
                var cls2 = WindowFinder.GetClassName(hwnd);
                var proc2 = WKAppBot.Win32.Native.NativeMethods.TryGetProcessNameFast(pid2) ?? "";
                bool skipBrowserUrl = WindowFinder.IsElectronWindow(cls2, proc2);
                if (!skipBrowserUrl)
                {
                    var rawUrl = WindowFinder.GetBrowserUrl(hwnd, pid2);
                    if (!string.IsNullOrEmpty(rawUrl))
                    {
                        if (rawUrl.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
                        {
                            var fileName = System.IO.Path.GetFileName(Uri.UnescapeDataString(rawUrl));
                            if (!string.IsNullOrEmpty(fileName)) return $"file:'{fileName}'";
                        }
                    }
                }
            }
            catch { }
        }
        g = Regex.Replace(g, @",?cls:'Chrome_WidgetWin_\d+',?", ",");
        g = Regex.Replace(g, @",?cls:'Chrome_RenderWidgetHostHWND',?", ",");
        g = Regex.Replace(g, @"\{,+", "{");
        g = Regex.Replace(g, @",+\}", "}");
        g = Regex.Replace(g, @",{2,}", ",");
        if (g == "{}" || g == "{ }")
        {
            WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hwnd, out uint fallbackPid);
            var fallbackProc = WKAppBot.Win32.Native.NativeMethods.TryGetProcessNameFast(fallbackPid) ?? "";
            // Strip hot-swap .old-<timestamp> suffix (see normalize above).
            fallbackProc = Regex.Replace(fallbackProc, @"\.old-\d{8}-\d{4,6}$", "");
            var fallbackCls  = WindowFinder.GetClassName(hwnd) ?? "";
            fallbackProc = fallbackProc.Replace("'", "\\'");
            fallbackCls  = fallbackCls.Replace("'", "\\'");
            if (!string.IsNullOrEmpty(fallbackProc) && !string.IsNullOrEmpty(fallbackCls))
                return $"{{proc:'{fallbackProc}',cls:'{fallbackCls}'}}";
            if (!string.IsNullOrEmpty(fallbackProc))
                return $"{{proc:'{fallbackProc}'}}";
            if (!string.IsNullOrEmpty(fallbackCls))
                return $"{{cls:'{fallbackCls}'}}";
        }
        return g;
    }
}
