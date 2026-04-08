using FlaUI.Core.AutomationElements;
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
            Console.Error.WriteLine("[A11Y] highlight ??no BoundingRect available");
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
            // Fallback: just print position info without overlay
            Console.Error.WriteLine($"[A11Y] highlight ??overlay failed, element at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");
            return true;
        }

        var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : (aid != "" ? $"aid={aid}" : "(unnamed)");
        zoom.UpdateStatus($"[{type}] {label}");
        Console.Error.WriteLine($"[A11Y] highlight ??[{type}] {label} at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");

        // Show for duration then fade out
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
        var f = new System.Text.StringBuilder("{");
        f.Append($"hwnd:0x{hwnd.ToInt64():X}");
        f.Append($",pid:{pid}");
        f.Append($",proc:'{procName.Replace("'", "\\'")}'");

        // domain or file for web/browser windows
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
            // legacy app — no a11y tree, cls is essential for targeting
            f.Append($",cls:'{clsMatch.Groups[1].Value.Replace("'", "\\'")}'");
        }

        // matched search field — always include if not already covered above
        if (hit != null && !string.IsNullOrWhiteSpace(hit.MatchedVia) && !string.IsNullOrWhiteSpace(hit.MatchedSnippet))
        {
            bool alreadyCovered = hit.MatchedVia switch
            {
                "domain"  => hasDomainOrFile,
                "url"     => hasDomainOrFile,
                "file"    => hasDomainOrFile,
                "proc"    => true, // already in proc field
                "cls"     => clsMatch.Success,
                "context" => true, // internal heuristic (foreground host) — not a real grap field
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

    static bool A11yFind(AutomationElement root, IntPtr hwnd, int depth, bool printFocus = true, string[]? extraArgs = null,
        WKAppBot.Win32.Window.WindowInfo? originalHit = null, string? multiHeader = null)
    {
        FocusedElementInfo? focInfo = null;
        try
        {
            using var loc = new UiaLocator();
            focInfo = loc.GetFocusedElementInfo();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[DIAG:find] error: {ex.Message}");
        }

        var compactGrap = BuildCompactWinGrap(hwnd);
        var fullGrap = BuildTargetGrapWithFocusPath(hwnd);

        // ── FOCUS section ──────────────────────────────────────
        if (printFocus && focInfo != null)
        {
            var gti2 = new NativeMethods.GUITHREADINFO
                { cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.GUITHREADINFO>() };
            NativeMethods.GetGUIThreadInfo(0, ref gti2);
            var focRootHwnd = gti2.hwndFocus != IntPtr.Zero
                ? NativeMethods.GetAncestor(gti2.hwndFocus, NativeMethods.GA_ROOT)
                : IntPtr.Zero;
            if (focRootHwnd == IntPtr.Zero) focRootHwnd = gti2.hwndFocus;

            var focGrap = focRootHwnd != IntPtr.Zero
                ? BuildTargetGrapWithFocusPath(focRootHwnd)
                : BuildTargetGrapWithFocusPath(hwnd);
            uint focPid = 0;
            string focProc = "";
            if (focRootHwnd != IntPtr.Zero)
            {
                NativeMethods.GetWindowThreadProcessId(focRootHwnd, out focPid);
                try { using var fp = System.Diagnostics.Process.GetProcessById((int)focPid); focProc = fp.ProcessName; } catch { }
            }
            var focScope = focGrap.Contains('#') ? focGrap[focGrap.IndexOf('#')..] : "";
            var focTitle = focRootHwnd != IntPtr.Zero ? NativeMethods.GetWindowTextW(focRootHwnd) : "";
            var focCompact = BuildCompactWinGrap(focRootHwnd);
            var focGrapJson = BuildFindGrap(focRootHwnd, focPid, focProc, focCompact, null);
            var focPaste = QuoteGrapExpression($"{focGrapJson}{focScope}");
            var focSw = System.Diagnostics.Stopwatch.StartNew();
            string focVerify = "?";
            try
            {
                var focHits = WindowFinder.FindWindows(focCompact, false);
                focVerify = focHits.Any(v => v.Handle == focRootHwnd) ? "OK" : "MISS";
            }
            catch { }
            focSw.Stop();
            PrintFocusBlock(focPaste, focVerify, focSw.ElapsedMilliseconds);
            if (!string.IsNullOrWhiteSpace(focTitle))
                Console.Error.WriteLine(focTitle);
        }

        // Multi-match header: printed once before first TARGET (after FOCUS if present)
        if (!string.IsNullOrEmpty(multiHeader))
            Console.WriteLine(Ansi.Dim(multiHeader));

        // ── TARGET section ─────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        string verifyMark = "?";
        var verifyHits = new List<WKAppBot.Win32.Window.WindowInfo>();
        try
        {
            verifyHits = WindowFinder.FindWindows(compactGrap, false);
            verifyMark = verifyHits.Any(v => v.Handle == hwnd) ? "OK" : "MISS";
        }
        catch { }
        sw.Stop();

        NativeMethods.GetWindowThreadProcessId(hwnd, out uint resolvedPid);
        string procName = "";
        try { using var proc = System.Diagnostics.Process.GetProcessById((int)resolvedPid); procName = proc.ProcessName; } catch { }

        var primaryHit = verifyHits.FirstOrDefault(v => v.Handle == hwnd);
        var scope = fullGrap.Contains('#') ? fullGrap[fullGrap.IndexOf('#')..] : "";
        var title = NativeMethods.GetWindowTextW(hwnd);
        if (title.Length > 90) title = title[..87] + "...";

        // Prefer originalHit for matched-field injection: verify re-searches by compact grap,
        // losing the original matched field (e.g. cmdLine:'chatgpt.com' for a VS Code window).
        var hitForGrap = originalHit ?? primaryHit;
        var grapJson = BuildFindGrap(hwnd, resolvedPid, procName, compactGrap, hitForGrap);
        var paste = QuoteGrapExpression($"{grapJson}{scope}");

        var titleHeading = !string.IsNullOrWhiteSpace(title) ? title : "TARGET";
        var matchNote = originalHit?.MatchedVia switch
        {
            null or "" or "context" or "proc" or "domain" or "url" or "file" or "cls" or "title" => "",
            "uia"       => $"  ← uia: {originalHit.MatchedSnippet}",
            "child-cmd" => "",  // proc/cmd already in grap — no extra annotation needed
            _ => $"  ← {originalHit.MatchedVia}: {originalHit.MatchedSnippet}"
        } ?? "";

        var leafTag = GetFocusedLeafTag(hwnd);
        PrintTargetBlock(titleHeading, paste, "find", extraArgs, verifyMark, sw.ElapsedMilliseconds, matchNote, leafTag);

        return true;
    }

    /// <summary>
    /// Recursively dump UIA element tree to stderr (diagnostic use only).
    /// </summary>
    static void DumpUiaElement(AutomationElement el, int level, int maxDepth)
    {
        if (level > maxDepth) return;
        var indent = new string(' ', level * 2);
        try
        {
            var patterns = GetSupportedPatternNames(el);
            var tag = GrapHelper.FormatNodeLabel(el, includeRect: false);
            var nhStr = "";
            var nh = GetElementHwnd(el);
            if (nh != IntPtr.Zero) nhStr = $" hwnd={nh:X8}";
            var patStr = patterns.Count > 0 ? $" ({string.Join(",", patterns)})" : "";
            Console.Error.WriteLine($"[DIAG:uia] {indent}{tag}{nhStr}{patStr}");
            if (level < maxDepth)
                try { foreach (var child in el.FindAllChildren()) DumpUiaElement(child, level + 1, maxDepth); } catch { }
        }
        catch { }
    }

    /// <summary>Get compact list of supported UIA pattern names.</summary>
    static List<string> GetSupportedPatternNames(AutomationElement el)
    {
        var names = new List<string>();
        try { if (el.Patterns.Invoke.IsSupported) names.Add("Invoke"); } catch { }
        try { if (el.Patterns.Value.IsSupported) names.Add("Value"); } catch { }
        try { if (el.Patterns.Toggle.IsSupported) names.Add("Toggle"); } catch { }
        try { if (el.Patterns.SelectionItem.IsSupported) names.Add("Select"); } catch { }
        try { if (el.Patterns.ExpandCollapse.IsSupported) names.Add("Expand"); } catch { }
        try { if (el.Patterns.Scroll.IsSupported) names.Add("Scroll"); } catch { }
        try { if (el.Patterns.RangeValue.IsSupported) names.Add("Range"); } catch { }
        try { if (el.Patterns.Window.IsSupported) names.Add("Window"); } catch { }
        try { if (el.Patterns.Transform.IsSupported) names.Add("Transform"); } catch { }
        try { if (el.Patterns.Grid.IsSupported) names.Add("Grid"); } catch { }
        try { if (el.Patterns.LegacyIAccessible.IsSupported) names.Add("LegacyIA"); } catch { }
        return names;
    }

    // -- Read: dump element's accessible state (TTS friendly) --
    static bool A11yRead(AutomationElement el)
    {
        var lines = new List<string>();

        var name = el.Properties.Name.ValueOrDefault ?? "";
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
        string controlType = "?";
        try { controlType = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        System.Drawing.Rectangle? rect = null;
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0) rect = new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
        catch { }
        lines.Add($"Tag: {GrapHelper.FormatNodeLabel(controlType, aid, name, rect: rect)}");
        if (!string.IsNullOrEmpty(name))
            lines.Add($"Name: {name}");
        lines.Add($"Type: {controlType}");
        if (!string.IsNullOrEmpty(aid))
            lines.Add($"AutomationId: {aid}");

        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported)
            {
                var val = vp.Pattern.Value.ValueOrDefault ?? "";
                var ro = vp.Pattern.IsReadOnly.ValueOrDefault;
                lines.Add($"Value: \"{val}\"{(ro ? " (readonly)" : "")}");
            }
        }
        catch { }

        var toggle = UiaLocator.GetToggleState(el);
        if (toggle != null)
            lines.Add($"ToggleState: {toggle}");

        var sel = UiaLocator.IsSelected(el);
        if (sel != null)
            lines.Add($"IsSelected: {sel}");

        var ec = UiaLocator.GetExpandCollapseState(el);
        if (ec != null)
            lines.Add($"ExpandState: {ec}");

        var range = UiaLocator.GetRangeValueInfo(el);
        if (range != null)
            lines.Add($"Range: {range.Value} ({range.Minimum}..{range.Maximum}, step={range.SmallChange}{(range.IsReadOnly ? ", readonly" : "")})");

        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0)
                lines.Add($"Rect: ({(int)r.X},{(int)r.Y}) {(int)r.Width}x{(int)r.Height}");
        }
        catch { }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var desc = legacy.Pattern.Description.ValueOrDefault;
                if (!string.IsNullOrEmpty(desc))
                    lines.Add($"Description: {desc}");
                var help = legacy.Pattern.Help.ValueOrDefault;
                if (!string.IsNullOrEmpty(help))
                    lines.Add($"Help: {help}");
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                    lines.Add($"DefaultAction: {defAction}");
            }
        }
        catch { }

        try
        {
            var children = el.FindAllChildren();
            if (children.Length > 0)
            {
                lines.Add($"Children: {children.Length}");
                foreach (var child in children.Take(10))
                {
                    try
                    {
                        var cn = child.Properties.Name.ValueOrDefault ?? "";
                        var cct = "?";
                        try { cct = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var caid = child.Properties.AutomationId.ValueOrDefault ?? "";
                        lines.Add($"  {GrapHelper.FormatNodeLabel(cct, caid, cn)}");
                    }
                    catch { }
                }
                if (children.Length > 10)
                    lines.Add($"  ... +{children.Length - 10} more");
            }
        }
        catch { }

        if (lines.Count == 0)
        {
            Console.Error.WriteLine("[A11Y] read ??no accessible information available");
            return false;
        }

        foreach (var line in lines)
            Console.Error.WriteLine($"[A11Y] {line}");
        return true;
    }
}

