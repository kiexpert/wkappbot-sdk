using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: `find <keyword> --tree`
// Prints one line per reachable grap path for every matched top-level window.
// Each line combines a Win32 child hierarchy (grap path with '/') with an
// optional UIA suffix (tag path after '#'). Copy-pasteable into any other
// a11y command that accepts a grap pattern.
internal partial class Program
{
    // Tunables for tree output. Small/cheap by design -- MFC frames like
    // 영웅문 explode into hundreds of Win32 children and UIA under each is
    // usually sparse, so we cap aggressively.
    private const int TreeWin32Depth     = 4;   // max Win32 child recursion depth
    private const int TreeWin32PerLevel  = 40;  // max siblings printed per level
    private const int TreeUiaDepth       = 3;   // max UIA sub-depth per node (default MFC/Win32)
    private const int TreeUiaDepthDeep   = 8;   // max UIA depth for browser/Electron (Web content is 10+ deep)
    private const int TreeUiaMaxElements = 12;  // max UIA paths printed per node
    private const int TreeUiaMaxElementsDeep = 30; // max UIA paths for browser/Electron (more actionable nodes)
    private const int TreeUiaTimeoutMs   = 500; // per-node UIA timeout (default)
    private const int TreeUiaTimeoutMsDeep = 1500; // per-node UIA timeout for browsers (deeper walk)

    // Class names that carry no actionable info -- fall back to title/hwnd.
    // #32770 = generic Dialog class, Static = label, msctls_statusbar32 = statusbar, etc.
    private static readonly HashSet<string> GenericClassNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "#32770", "Static", "msctls_statusbar32", "Button", "Edit", "ComboBox",
        "ListBox", "ScrollBar", "SysListView32", "SysTreeView32", "SysTabControl32",
        "ToolbarWindow32", "tooltips_class32", "ReBarWindow32",
    };

    // Browser/Electron process names that benefit from deeper UIA walk
    private static readonly HashSet<string> BrowserProcessNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "chrome", "msedge", "brave", "opera", "vivaldi", "chromium",
        "electron", "Code", "Cursor", "Slack", "Discord", "Notion", "ClaudeCode",
    };

    // UIA ControlTypes that are considered "meaningful" (actionable / content-bearing).
    // Used to filter anonymous paths -- at least one segment must be meaningful.
    private static readonly HashSet<string> MeaningfulControlTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "Button", "Edit", "ComboBox", "ListItem", "TreeItem", "TabItem",
        "Hyperlink", "CheckBox", "RadioButton", "MenuItem", "Slider",
        "Spinner", "Text", "Image", "DataItem", "Thumb", "SplitButton",
        "Table", "Header", "HeaderItem",
    };

    static int FindTreeCommand(string[] args)
    {
        var positional = args.Where(a => !a.StartsWith("--")).ToList();
        if (positional.Count == 0)
            return Error("find --tree: keyword required");

        string keyword = positional[0];
        int limit = int.TryParse(GetArgValue(args, "--limit"), out var lim) ? lim : 20;

        var windows = WindowFinder.FindWindows(keyword);
        if (windows.Count == 0)
            return Error($"No windows match: \"{keyword}\"");

        int printedWindows = 0;
        foreach (var win in windows)
        {
            if (printedWindows++ >= limit) break;
            PrintWindowTree(win);
        }

        if (windows.Count > limit)
            Console.WriteLine($"\n... +{windows.Count - limit} more (raise with --limit N)");

        return 0;
    }

    static void PrintWindowTree(WindowInfo root)
    {
        var title = string.IsNullOrEmpty(root.Title) ? "(untitled)" : root.Title;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"## {title}  [hwnd:0x{root.Handle:X8}] ({root.ClassName})");
        Console.ResetColor();

        var rootSeg = BuildTreeSegment(root, preferTitle: true);
        var segments = new List<string> { rootSeg };

        // Root line + its UIA suffix lines
        EmitTreeLine(segments, null);
        foreach (var uia in DumpUiaSuffixes(root.Handle))
            EmitTreeLine(segments, uia);

        // Recurse into Win32 children
        WalkWin32(root.Handle, segments, depth: 1);
    }

    static void WalkWin32(IntPtr hParent, List<string> segments, int depth)
    {
        if (depth > TreeWin32Depth) return;

        List<WindowInfo> children;
        try { children = WindowFinder.GetChildrenZOrder(hParent); }
        catch { return; }

        int emitted = 0;
        foreach (var child in children)
        {
            if (emitted >= TreeWin32PerLevel)
            {
                EmitTreeLine(segments, $"... +{children.Count - emitted} more Win32 children");
                break;
            }
            // Skip invisible / zero-sized noise -- IME windows, hidden helpers
            if (!child.IsVisible) continue;
            if (child.Rect.Width <= 0 || child.Rect.Height <= 0) continue;
            if (child.ClassName == "IME" || child.ClassName == "MSCTFIME UI") continue;

            var seg = BuildTreeSegment(child, preferTitle: false);
            if (seg.Length == 0) continue; // no usable identifier -- skip
            segments.Add(seg);
            try
            {
                EmitTreeLine(segments, null);
                foreach (var uia in DumpUiaSuffixes(child.Handle))
                    EmitTreeLine(segments, uia);
                WalkWin32(child.Handle, segments, depth + 1);
            }
            finally
            {
                segments.RemoveAt(segments.Count - 1);
            }
            emitted++;
        }
    }

    /// <summary>
    /// Generate grap candidates for a Win32 window, score by intuitiveness, return the best.
    /// Candidates: human-readable title, stable class name, class+title combo, class alone.
    /// hwnd is never emitted -- it changes every session and breaks saved scripts.
    /// </summary>
    static string BuildTreeSegment(WindowInfo wi, bool preferTitle)
    {
        var title = (wi.Title ?? "").Trim();
        var cls   = (wi.ClassName ?? "").Trim();

        var candidates = new List<(string expr, int score)>();

        // Title candidate: short human-readable title scores highest
        if (title.Length > 0)
        {
            string expr = QuotedGlob(title);
            int score = TitleIntuitionScore(title);
            candidates.Add((expr, score));
        }

        // Class candidate: stable Win32 class (not auto-generated)
        if (!string.IsNullOrEmpty(cls) && !IsGenericClassName(cls))
        {
            string expr = BareIdent(cls);
            int score = ClassIntuitionScore(cls);
            candidates.Add((expr, score));
        }

        // If both exist and title is long/ugly, class is better -- already handled by scores.
        // If only generic class + no title: show class anyway (better than nothing)
        if (candidates.Count == 0 && !string.IsNullOrEmpty(cls))
            candidates.Add((BareIdent(cls), 10));

        // No candidates at all -- skip this node (caller handles empty)
        if (candidates.Count == 0)
            return "";

        // Pick best by score, then verify it actually matches this window.
        // Titles with glob-special chars like '[', ']' may silently mis-match.
        foreach (var (expr, _) in candidates.OrderByDescending(c => c.score))
        {
            if (GrapMatches(expr, wi.Title ?? "", wi.ClassName ?? ""))
                return expr;
        }

        // All intuitive candidates failed verification -- emit session-stable fallback.
        NativeMethods.GetWindowThreadProcessId(wi.Handle, out uint fbPid);
        var fbProc = NativeMethods.TryGetProcessNameFast(fbPid) ?? "";
        return string.IsNullOrEmpty(fbProc)
            ? $"hwnd:0x{wi.Handle:X8}"
            : $"{{proc:'{fbProc}',hwnd:0x{wi.Handle:X8}}}";
    }

    // Verify the grap expression round-trips back to this window's fields.
    // Uses plain string matching (not PatternMatcher) to avoid glob mis-interpretation
    // of chars like '[', ']' that appear in 영웅문 form titles (e.g. "[2000] 해외주식").
    static bool GrapMatches(string expr, string title, string cls)
    {
        if (string.IsNullOrEmpty(expr)) return false;
        // JSON5 / hwnd: always assume valid (content-verified by construction)
        if (expr.StartsWith('{') || expr.StartsWith("hwnd:")) return true;
        // Quoted title: "text" -> strip quotes, check substring
        if (expr.StartsWith('"') && expr.EndsWith('"') && expr.Length >= 2)
        {
            var inner = expr[1..^1];
            return title.Contains(inner, StringComparison.OrdinalIgnoreCase)
                || cls.Contains(inner, StringComparison.OrdinalIgnoreCase);
        }
        // Bare ident (class name): exact or contains match
        return string.Equals(cls, expr, StringComparison.OrdinalIgnoreCase)
            || cls.Contains(expr, StringComparison.OrdinalIgnoreCase)
            || title.Contains(expr, StringComparison.OrdinalIgnoreCase);
    }

    // Score a title by intuitiveness (higher = more intuitive).
    // Rewards: short, contains Korean/letters, has meaningful words.
    // Penalizes: pure hex/numbers, very long, path-like strings.
    static int TitleIntuitionScore(string title)
    {
        int score = 100;
        // Length penalty: longer = harder to read
        score -= Math.Min(title.Length, 40);
        // Bonus: contains Korean (highly descriptive for this domain)
        if (title.Any(c => c >= 0xAC00 && c <= 0xD7A3)) score += 40;
        // Bonus: contains letters (meaningful word)
        if (title.Any(char.IsLetter)) score += 10;
        // Penalty: looks like a path or hex dump
        if (title.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) score -= 50;
        if (title.Contains('\\') || title.Contains('/')) score -= 20;
        return score;
    }

    // Score a class name by intuitiveness (higher = more intuitive).
    // Rewards: short, PascalCase descriptive names.
    // Penalizes: all-caps, contains digits/colons (auto-generated).
    static int ClassIntuitionScore(string cls)
    {
        int score = 60;
        score -= Math.Min(cls.Length, 30);
        // PascalCase is human-authored (e.g. AfxFrameOrView110, MDIClient)
        if (char.IsUpper(cls[0]) && cls.Any(char.IsLower)) score += 20;
        // Contains digits (version suffix like 110) -- minor penalty
        if (cls.Any(char.IsDigit)) score -= 5;
        // Contains colons/special chars -- auto-generated
        if (cls.Contains(':') || cls.Contains(';')) score -= 40;
        return score;
    }

    static bool IsGenericClassName(string cls)
    {
        if (string.IsNullOrWhiteSpace(cls)) return true;
        if (GenericClassNames.Contains(cls)) return true;
        // MFC auto-generated MDI child class: Afx:XXXXXXXX:b:... -- not stable/human-readable
        if (System.Text.RegularExpressions.Regex.IsMatch(cls, @"^Afx:[0-9A-Fa-f]", System.Text.RegularExpressions.RegexOptions.None)) return true;
        return false;
    }

    /// <summary>
    /// Emit an identifier as bare text when it's safe (alphanumeric/underscore/dash/dot),
    /// else wrap in double quotes. Keeps grap paths copy-paste clean.
    /// </summary>
    static string BareIdent(string raw)
    {
        var t = raw.Replace("\"", "'");
        if (t.Length > 48) t = t[..48];
        bool safe = t.Length > 0;
        foreach (var c in t)
        {
            if (!(char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '.'))
            {
                safe = false;
                break;
            }
        }
        return safe ? t : $"\"{t}\"";
    }

    static string QuotedGlob(string raw)
    {
        // Grap does contains-match by default, so plain "title" == "*title*"
        var t = raw.Replace("\"", "'");
        if (t.Length > 30) t = t[..30];
        return $"\"{t}\"";
    }

    static void EmitTreeLine(IReadOnlyList<string> segments, string? uiaSuffix)
    {
        Console.Write("  ");
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < segments.Count; i++)
        {
            if (i > 0) Console.Write("/");
            Console.Write(segments[i]);
        }
        Console.ResetColor();
        if (!string.IsNullOrEmpty(uiaSuffix))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(uiaSuffix);
            Console.ResetColor();
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Collect UIA tag-path suffixes (starting with '#') reachable under the
    /// given Win32 window. Executes DumpPaths inside a Task guard --
    /// MFC frames with no UIA provider can otherwise hang for seconds.
    /// Browser/Electron windows get a deeper budget (UIA tree can be 20+ levels).
    /// </summary>
    static List<string> DumpUiaSuffixes(IntPtr hwnd)
    {
        var result = new List<string>();
        bool isBrowser = IsBrowserOrElectronWindow(hwnd);
        int depthBudget = isBrowser ? TreeUiaDepthDeep : TreeUiaDepth;
        int elementBudget = isBrowser ? TreeUiaMaxElementsDeep : TreeUiaMaxElements;
        int timeoutMs = isBrowser ? TreeUiaTimeoutMsDeep : TreeUiaTimeoutMs;

        var cts = new System.Threading.CancellationTokenSource();
        var task = System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                using var uia = new UiaLocator();
                var root = uia.Automation.FromHandle(hwnd);
                if (root == null) return;
                using var sw = new System.IO.StringWriter();
                uia.DumpPaths(root, maxDepth: depthBudget, maxElements: elementBudget, writer: sw);
                var lines = sw.ToString()
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.TrimEnd('\r'))
                    .Where(l => l.Length > 0);
                foreach (var line in lines)
                {
                    if (cts.IsCancellationRequested) break;
                    // DumpPaths emits "#/Type_Aid/Type_Aid" -- strip the leading
                    // "#/" so callers can paste it directly after our Win32
                    // segments as a single grap, which already ends at a '/'
                    // or uses '#' as the Win32->UIA boundary.
                    var trimmed = line.StartsWith("#/") ? "#" + line[2..] : line;
                    if (!trimmed.StartsWith("#")) trimmed = "#" + trimmed;
                    // Filter paths that are all anonymous Pane/Group/Document
                    // (e.g. "#Pan/Pan/Pan") -- keep only paths with at least one
                    // named node (aid/index) OR a meaningful ControlType.
                    if (!HasMeaningfulSegment(trimmed)) continue;
                    result.Add(trimmed);
                }
            }
            catch { /* best effort -- missing UIA provider is normal for MFC */ }
        }, cts.Token);

        if (!task.Wait(timeoutMs))
        {
            cts.Cancel();
            // don't await -- let it finish in background and be GC'd
        }
        return result;
    }

    /// <summary>
    /// True if the window's owning process is a browser / Electron shell
    /// (Chrome, Edge, VSCode, Slack, etc.). Used to widen UIA depth budget
    /// since Web content routinely nests 10+ levels deep.
    /// </summary>
    static bool IsBrowserOrElectronWindow(IntPtr hwnd)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = NativeMethods.TryGetProcessNameFast(pid);
            if (string.IsNullOrEmpty(procName)) return false;
            return BrowserProcessNames.Contains(procName);
        }
        catch { return false; }
    }

    /// <summary>
    /// A UIA path is "meaningful" if at least one segment carries either:
    ///   - a name/aid (segment contains '_' from Type_Aid formatting, or an index '**' marker)
    ///   - a meaningful ControlType (Button, Edit, ListItem, ...) -- not just Pane/Group/Document.
    /// Drops "#Pan/Pan/Pan" and "#Group/Document/Pan" noise typical of Chrome Web hosts.
    /// </summary>
    static bool HasMeaningfulSegment(string uiaPath)
    {
        var segs = uiaPath.TrimStart('#').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segs.Length == 0) return false;
        foreach (var s in segs)
        {
            // Aid/index suffix: "Type_Aid" or "Type**2" -> carries identity, keep.
            if (s.Contains('_') || s.Contains("**")) return true;
            // Bare type -- check if it's a meaningful control type.
            // Type names are normalized by FormatNodeTag (e.g. "Button", "Pan" for Pane).
            // Strip any trailing digits/index and compare against the meaningful set.
            var baseType = s;
            if (MeaningfulControlTypes.Contains(baseType)) return true;
            // Also accept abbreviated forms FormatNodeTag might emit for common meaningful types.
            if (baseType.Equals("Btn", StringComparison.OrdinalIgnoreCase)) return true;
            if (baseType.Equals("Edit", StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }
}
