using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Highlight: show zoom/highlight overlay on target element --
    static bool A11yHighlight(AutomationElement el, IntPtr hwnd, int durationMs = 3000)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] highlight: no BoundingRect available");
            return false;
        }

        var name = el.Properties.Name.ValueOrDefault ?? "";
        var type = "?";
        try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";

        // Try element hwnd first (better capture), fall back to screen rect
        var elHwnd = GetElementHwnd(el);
        ClickZoomHelper? zoom;
        if (elHwnd != IntPtr.Zero)
            zoom = ClickZoomHelper.Begin(elHwnd, hwnd, "a11y-highlight", $"{type} \"{name}\"");
        else
            zoom = ClickZoomHelper.BeginFromRect(rect.Value, hwnd, "a11y-highlight", $"{type} \"{name}\"");

        if (zoom == null)
        {
            Console.Error.WriteLine($"[A11Y] highlight: overlay failed, element at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");
            return true;
        }

        var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : (aid != "" ? $"aid={aid}" : "(unnamed)");
        zoom.UpdateStatus($"[{type}] {label}");
        Console.Error.WriteLine($"[A11Y] highlight: [{type}] {label} at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");

        zoom.ShowPass($"{type} {label}");
        Thread.Sleep(durationMs);
        zoom.Dispose();
        return true;
    }

    // -- Find: CURSOR -> TARGET -> VERDICT structured output --
    // stdout: clean result sections only
    // stderr: only hard errors, not the full analysis dump
    /// <summary>
    /// Build the paste-ready JSON5 grap for find output.
    /// Required fields: hwnd, pid (after hwnd for collision safety), proc,
    /// domain/file (web/browser), cls (legacy no-a11y), matched search field.
    /// </summary>
    static string BuildFindGrap(IntPtr hwnd, uint pid, string procName,
        string compactGrap, WKAppBot.Win32.Window.WindowInfo? hit)
    {
        if (hit != null && hit.MatchedVia == "child-cmd" && !string.IsNullOrEmpty(hit.MatchedSnippet))
        {
            var sep = hit.MatchedSnippet.IndexOf(':');
            if (sep > 0)
            {
                var childProc = hit.MatchedSnippet[..sep].Replace("'", "\\'");
                var token     = hit.MatchedSnippet[(sep + 1)..].Replace("'", "\\'");
                if (token.Length > 60) token = token[..60];
                return $"{{hwnd:0x{hwnd.ToInt64():X},proc:'{childProc}',cmd:'{token}'}}";
            }
        }

        var f = new System.Text.StringBuilder("{");
        f.Append($"hwnd:0x{hwnd.ToInt64():X}");
        f.Append($",pid:{pid}");
        f.Append($",proc:'{procName.Replace("'", "\\'")}'");

        var domainMatch = System.Text.RegularExpressions.Regex.Match(compactGrap, @"domain:'([^']*)'");
        var fileMatch   = System.Text.RegularExpressions.Regex.Match(compactGrap, @"file:'([^']*)'");
        var clsMatch    = System.Text.RegularExpressions.Regex.Match(compactGrap, @"cls:'([^']*)'");

        bool hasDomainOrFile = false;
        if (domainMatch.Success)
        {
            f.Append($",domain:'{domainMatch.Groups[1].Value}'");
            hasDomainOrFile = true;
        }
        else if (fileMatch.Success)
        {
            f.Append($",file:'{fileMatch.Groups[1].Value}'");
            hasDomainOrFile = true;
        }
        else if (clsMatch.Success)
        {
            f.Append($",cls:'{clsMatch.Groups[1].Value.Replace("'", "\\'")}'");
        }

        if (hit != null && !string.IsNullOrWhiteSpace(hit.MatchedVia) && !string.IsNullOrWhiteSpace(hit.MatchedSnippet))
        {
            bool alreadyCovered = hit.MatchedVia switch
            {
                "domain"  => hasDomainOrFile,
                "url"     => hasDomainOrFile,
                "file"    => hasDomainOrFile,
                "proc"    => true,
                "cls"     => clsMatch.Success,
                "context" => true,
                _         => false
            };
            if (!alreadyCovered)
            {
                var snip = hit.MatchedSnippet.Replace("'", "\\'");
                if (snip.Length > 60) snip = snip[..60];
                f.Append($",{hit.MatchedVia}:'{snip}'");
            }
        }

        f.Append('}');
        return f.ToString();
    }

    /// <summary>
    /// Build the most concise stable grap that uniquely resolves to this window.
    /// </summary>
    static string BuildStableGrap(IntPtr hwnd, string procName)
    {
        var compact  = BuildCompactWinGrap(hwnd);
        var safeProc = procName.Replace("'", "\\'");
        var procOnly = $"{{proc:'{safeProc}'}}";

        bool isBrowser = compact.Contains("domain:") || compact.Contains("file:");
        var candidates = (isBrowser || compact == procOnly)
            ? new List<string> { compact }
            : new List<string> { procOnly, compact };

        if (isBrowser && compact.Contains("domain:"))
        {
            try
            {
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint urlPid);
                var rawUrl = WKAppBot.Win32.Window.WindowFinder.GetBrowserUrl(hwnd, urlPid);
                if (!string.IsNullOrEmpty(rawUrl))
                {
                    var uri = new Uri(rawUrl.Split(' ')[0]);
                    var path = uri.AbsolutePath.TrimEnd('/');
                    if (!string.IsNullOrEmpty(path) && path != "/")
                    {
                        var safeUrl = path.Replace("'", "\\'");
                        if (safeUrl.Length > 60) safeUrl = safeUrl[..60];
                        var domainMatch = System.Text.RegularExpressions.Regex.Match(compact, @"domain:'([^']*)'");
                        if (domainMatch.Success)
                            candidates.Add($"{{domain:'{domainMatch.Groups[1].Value}',url:'{safeUrl}'}}");
                    }
                }
            }
            catch { }
        }

        foreach (var candidate in candidates)
        {
            try
            {
                var hits = WKAppBot.Win32.Window.WindowFinder.FindWindows(candidate);
                if (hits.Count == 1 && hits[0].Handle == hwnd)
                    return candidate;
            }
            catch { }
        }

        NativeMethods.GetWindowThreadProcessId(hwnd, out uint fbPid);
        return $"{{hwnd:0x{hwnd.ToInt64():X},pid:{fbPid},proc:'{safeProc}'}}";
    }

    // -- KIS dataAlert dismiss --------------------------------------------------
    // KIS web dataAlert popup (class contains "Gro_dataAlert") fires on every
    // navigation and steals UIA focus -- a11y read targets the modal button
    // instead of page content. This pre-read step detects the popup, clicks its
    // close button, and lets the actual read proceed against the right element.
    //
    // Detection strategies (any one hits -> dismiss):
    //   A. Walk UIA parent chain of `el` looking for ClassName/Name containing
    //      "Gro_dataAlert" (covers case where the read target IS inside the modal).
    //   B. Enumerate top-level windows in the same process searching for class
    //      "Gro_dataAlert*" (covers case where the modal is a sibling window
    //      that's intercepting focus / blocking the page).
    //
    // Returns true if a popup was found AND dismissed (caller may want to re-resolve
    // the UIA scope before reading); false on no-op.
    static bool TryDismissKisDataAlert(AutomationElement el, IntPtr hostHwnd)
    {
        bool dismissed = false;

        // -- Strategy A: UIA parent chain --------------------------------------
        try
        {
            var current = el;
            for (int i = 0; i < 25 && current != null; i++)
            {
                string cls = "";
                string name = "";
                try { cls  = current.Properties.ClassName.ValueOrDefault ?? ""; } catch { }
                try { name = current.Properties.Name.ValueOrDefault ?? ""; } catch { }

                if (cls.Contains("Gro_dataAlert", StringComparison.OrdinalIgnoreCase)
                    || name.Contains("Gro_dataAlert", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Error.WriteLine($"[A11Y] KIS dataAlert ancestor detected (cls=\"{cls}\" name=\"{name}\")");
                    if (DismissKisDataAlertElement(current))
                    {
                        dismissed = true;
                        Thread.Sleep(150); // let modal teardown settle
                    }
                    break;
                }

                AutomationElement? parent = null;
                try { parent = current.Parent; } catch { }
                if (parent == null) break;
                current = parent;
            }
        }
        catch { }

        // -- Strategy B: top-level window scan in same process -----------------
        if (!dismissed && hostHwnd != IntPtr.Zero)
        {
            try
            {
                NativeMethods.GetWindowThreadProcessId(hostHwnd, out uint hostPid);
                if (hostPid != 0)
                {
                    var found = new List<IntPtr>();
                    NativeMethods.EnumWindows((hwnd, _) =>
                    {
                        if (!NativeMethods.IsWindowVisible(hwnd)) return true;
                        NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                        if (pid != hostPid) return true;
                        var sb = new System.Text.StringBuilder(256);
                        NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
                        var cls = sb.ToString();
                        if (cls.Contains("Gro_dataAlert", StringComparison.OrdinalIgnoreCase))
                            found.Add(hwnd);
                        return true;
                    }, IntPtr.Zero);

                    foreach (var alertHwnd in found)
                    {
                        Console.Error.WriteLine($"[A11Y] KIS dataAlert window detected (hwnd=0x{alertHwnd.ToInt64():X})");
                        if (DismissKisDataAlertWindow(alertHwnd))
                        {
                            dismissed = true;
                            Thread.Sleep(150);
                        }
                    }
                }
            }
            catch { }
        }

        if (dismissed)
            Console.Error.WriteLine("[A11Y] KIS dataAlert dismissed -- proceeding with read");
        return dismissed;
    }

    /// <summary>Click the close/OK button inside a Gro_dataAlert UIA element. Falls back to WM_CLOSE on host hwnd.</summary>
    static bool DismissKisDataAlertElement(AutomationElement modal)
    {
        try
        {
            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            // Try buttons -- prefer "Close"/"OK"/"확인"/"닫기" but invoke first hit if no name match.
            var buttons = modal.FindAllDescendants(cf.ByControlType(ControlType.Button));
            AutomationElement? target = null;
            foreach (var b in buttons)
            {
                var nm = b.Properties.Name.ValueOrDefault ?? "";
                if (nm.Contains("close", StringComparison.OrdinalIgnoreCase)
                    || nm.Contains("OK",    StringComparison.OrdinalIgnoreCase)
                    || nm.Contains("확인")
                    || nm.Contains("닫기")
                    || nm == "X" || nm == "x")
                {
                    target = b;
                    break;
                }
            }
            target ??= buttons.FirstOrDefault();
            if (target != null)
            {
                try
                {
                    var inv = target.Patterns.Invoke;
                    if (inv.IsSupported) { inv.Pattern.Invoke(); return true; }
                }
                catch { }
                // Fallback: SendInput click via WindowFinder coordinates
                var rect = GetBoundingRect(target);
                if (rect != null)
                {
                    int cx = rect.Value.Left + rect.Value.Width / 2;
                    int cy = rect.Value.Top + rect.Value.Height / 2;
                    MouseInput.Click(cx, cy);
                    return true;
                }
            }
        }
        catch { }
        return false;
    }

    /// <summary>Dismiss a Gro_dataAlert window by hwnd: try BM_CLICK on close button, else WM_CLOSE.</summary>
    static bool DismissKisDataAlertWindow(IntPtr alertHwnd)
    {
        // Try Win32 child enumeration for a Button/X control first.
        IntPtr btn = IntPtr.Zero;
        NativeMethods.EnumChildWindows(alertHwnd, (child, _) =>
        {
            var sb = new System.Text.StringBuilder(64);
            NativeMethods.GetClassNameW(child, sb, sb.Capacity);
            var cls = sb.ToString();
            if (cls.Equals("Button", StringComparison.OrdinalIgnoreCase))
            {
                btn = child;
                return false; // first button = good enough for OK-style alerts
            }
            return true;
        }, IntPtr.Zero);

        if (btn != IntPtr.Zero)
        {
            // BM_CLICK = 0x00F5 -- focusless button click
            try { NativeMethods.SendMessageW(btn, 0x00F5, IntPtr.Zero, IntPtr.Zero); return true; } catch { }
        }
        // Last resort: WM_CLOSE the modal directly
        try { NativeMethods.PostMessageW(alertHwnd, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero); return true; } catch { }
        return false;
    }
}
