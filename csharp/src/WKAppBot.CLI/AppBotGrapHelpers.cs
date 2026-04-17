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

    private static string AppendFocusPath(string target, IntPtr hwnd)
    {
        try
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return target;

            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, hwnd);
            if (focused == null) return target;

            var path = WKAppBot.Win32.Accessibility.GrapHelper.BuildAbsoluteTagPath(
                focused, uia.TreeWalkerFactory.GetRawViewWalker(), 15);
            if (string.IsNullOrWhiteSpace(path) || path == "?") return target;

            return $"{target}#{path}";
        }
        catch
        {
            return target;
        }
    }

    // Get the focused leaf element's XML tag for TARGET block display.
    internal static string GetFocusedLeafTag(IntPtr hwnd)
    {
        try
        {
            using var uia = new FlaUI.UIA3.UIA3Automation();
            var root = uia.FromHandle(hwnd);
            if (root == null) return "";
            var focused = WKAppBot.Win32.Accessibility.GrapHelper.FindFocusedLeaf(uia, root, hwnd);
            if (focused == null) return "";
            return FormatLeafTag(focused);
        }
        catch { return ""; }
    }

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
