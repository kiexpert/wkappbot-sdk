using System.Text.RegularExpressions;
using System.Xml.Linq;
using WKAppBot.Abstractions;

namespace WKAppBot.Android;

/// <summary>
/// Parses Android uiautomator dump XML into a searchable node tree.
/// Produces Windows-compatible search keys for grap pattern matching.
/// </summary>
public class AndroidA11yTree
{
    private AndroidNode? _root;
    private DateTime _cacheTime;
    private string? _cacheSerial;
    private readonly AdbClient _adb;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromMilliseconds(500);

    public AndroidA11yTree(AdbClient adb) => _adb = adb;

    // ── Tree access ───────────────────────────────────────

    /// <summary>Get (cached) root node. Force refresh if stale or on demand.</summary>
    public AndroidNode? GetRoot(string? serial = null, bool forceRefresh = false)
    {
        if (!forceRefresh && _root != null && _cacheSerial == serial
            && DateTime.UtcNow - _cacheTime < CacheTtl)
            return _root;

        var xml = _adb.DumpUiTree(serial);
        if (xml == null) return null;

        _root = ParseXml(xml);
        _cacheTime = DateTime.UtcNow;
        _cacheSerial = serial;
        return _root;
    }

    // ── Search ────────────────────────────────────────────

    /// <summary>Find nodes matching a package pattern (top-level app matching)</summary>
    public List<AndroidNode> FindByPackage(AndroidNode root, string packagePattern)
    {
        var matcher = GrapMatcher.Create(packagePattern);
        var results = new List<AndroidNode>();
        CollectByPackage(root, matcher, results);
        return results;
    }

    /// <summary>
    /// Resolve #scope path: content-desc → text → resource-id (same as UIA Name → AutomationId).
    /// Multi-level: #scope1#scope2 narrows progressively.
    /// </summary>
    public AndroidNode? ResolveScope(AndroidNode root, string[] scopes)
    {
        var current = root;
        foreach (var scope in scopes)
        {
            var found = FindScopeChild(current, scope);
            if (found == null) return null;
            current = found;
        }
        return current;
    }

    /// <summary>Find all nodes matching a scope pattern within subtree</summary>
    public List<AndroidNode> FindAllInScope(AndroidNode root, string pattern)
    {
        var matcher = GrapMatcher.Create(pattern);
        var results = new List<AndroidNode>();
        CollectByScope(root, matcher, results);
        return results;
    }

    // ── Tree dump (inspect output) ────────────────────────

    /// <summary>Produce inspect-style tree output matching Windows format</summary>
    public static string DumpTree(AndroidNode node, int maxDepth = 5, int indent = 0)
    {
        var hotNodes = GetFocusChainNodes(node);
        var sb = new System.Text.StringBuilder();
        DumpNode(sb, node, maxDepth, indent, 0, hotNodes);
        return sb.ToString();
    }

    private static void DumpNode(System.Text.StringBuilder sb, AndroidNode node, int maxDepth,
        int baseIndent, int depth, HashSet<AndroidNode> hotNodes)
    {
        bool isHot = hotNodes.Contains(node);
        if (depth > maxDepth && !isHot) return;

        var prefix = new string(' ', (baseIndent + depth) * 2);
        var flags = "";
        if (node.Clickable) flags += "*";
        if (node.Scrollable) flags += "~";
        if (node.Checkable) flags += "?";
        if (node.Checked) flags += "!";
        if (node.Focused) flags += "⌨";

        var label = node.DisplayName;
        var rid = node.ShortResourceId;

        sb.Append($"{prefix}[{node.ClassName}]{flags}");
        if (!string.IsNullOrEmpty(label)) sb.Append($" {label}");
        if (!string.IsNullOrEmpty(rid)) sb.Append($" ({rid})");
        sb.AppendLine($" {node.BoundsString}");

        foreach (var child in node.Children)
            DumpNode(sb, child, maxDepth, baseIndent, depth + 1, hotNodes);
    }

    // ── XML parsing ───────────────────────────────────────

    private static AndroidNode ParseXml(string xml)
    {
        var doc = XDocument.Parse(xml);
        var hierarchy = doc.Root!;
        var topNode = hierarchy.Element("node");
        return topNode != null ? ParseNode(topNode) : new AndroidNode { ClassName = "hierarchy" };
    }

    private static AndroidNode ParseNode(XElement el)
    {
        var node = new AndroidNode
        {
            Index = int.TryParse(el.Attribute("index")?.Value, out var idx) ? idx : 0,
            Text = el.Attribute("text")?.Value ?? "",
            ResourceId = el.Attribute("resource-id")?.Value ?? "",
            ClassName = SimplifyClassName(el.Attribute("class")?.Value ?? ""),
            Package = el.Attribute("package")?.Value ?? "",
            ContentDesc = el.Attribute("content-desc")?.Value ?? "",
            Checkable = el.Attribute("checkable")?.Value == "true",
            Checked = el.Attribute("checked")?.Value == "true",
            Clickable = el.Attribute("clickable")?.Value == "true",
            Enabled = el.Attribute("enabled")?.Value == "true",
            Focusable = el.Attribute("focusable")?.Value == "true",
            Focused = el.Attribute("focused")?.Value == "true",
            Scrollable = el.Attribute("scrollable")?.Value == "true",
            LongClickable = el.Attribute("long-clickable")?.Value == "true",
            Selected = el.Attribute("selected")?.Value == "true",
        };

        // Parse bounds: [left,top][right,bottom]
        var boundsStr = el.Attribute("bounds")?.Value ?? "";
        var bm = Regex.Match(boundsStr, @"\[(\d+),(\d+)\]\[(\d+),(\d+)\]");
        if (bm.Success)
        {
            node.BoundsLeft = int.Parse(bm.Groups[1].Value);
            node.BoundsTop = int.Parse(bm.Groups[2].Value);
            node.BoundsRight = int.Parse(bm.Groups[3].Value);
            node.BoundsBottom = int.Parse(bm.Groups[4].Value);
        }

        // Parse children
        foreach (var childEl in el.Elements("node"))
        {
            var child = ParseNode(childEl);
            child.Parent = node;
            node.Children.Add(child);
        }

        return node;
    }

    private static string SimplifyClassName(string cls)
    {
        // "android.widget.FrameLayout" → "FrameLayout"
        var lastDot = cls.LastIndexOf('.');
        return lastDot >= 0 ? cls[(lastDot + 1)..] : cls;
    }

    // ── Search internals ──────────────────────────────────

    private static void CollectByPackage(AndroidNode node, GrapMatcher matcher, List<AndroidNode> results)
    {
        if (!string.IsNullOrEmpty(node.Package) && matcher.IsMatch(node.Package))
            results.Add(node);
        foreach (var child in node.Children)
            CollectByPackage(child, matcher, results);
    }

    private static AndroidNode? FindScopeChild(AndroidNode parent, string scopePattern)
    {
        var matcher = GrapMatcher.Create(scopePattern);

        // Pass 1: content-desc (= UIA Name)
        var found = FindInTree(parent, n => matcher.IsMatch(n.ContentDesc));
        if (found != null) return found;

        // Pass 2: text
        found = FindInTree(parent, n => matcher.IsMatch(n.Text));
        if (found != null) return found;

        // Pass 3: resource-id short name (= UIA AutomationId)
        found = FindInTree(parent, n => matcher.IsMatch(n.ShortResourceId));
        return found;
    }

    private static void CollectByScope(AndroidNode node, GrapMatcher matcher, List<AndroidNode> results)
    {
        if (matcher.IsMatch(node.ContentDesc) || matcher.IsMatch(node.Text) || matcher.IsMatch(node.ShortResourceId))
            results.Add(node);
        foreach (var child in node.Children)
            CollectByScope(child, matcher, results);
    }

    private static AndroidNode? FindInTree(AndroidNode node, Func<AndroidNode, bool> predicate)
    {
        if (predicate(node)) return node;
        foreach (var child in node.Children)
        {
            var found = FindInTree(child, predicate);
            if (found != null) return found;
        }
        return null;
    }

    // ── Hot Focus Chain ────────────────────────────────────

    /// <summary>
    /// Find the focused node and build parent chain (focused → ... → root).
    /// Returns formatted string or empty if no focused node found.
    /// </summary>
    public static string GetFocusChain(AndroidNode root)
    {
        var focused = FindInTree(root, n => n.Focused);
        if (focused == null) return "";

        // Build chain: focused → parent → ... → root
        var chain = new List<AndroidNode>();
        var cur = focused;
        while (cur != null)
        {
            chain.Add(cur);
            cur = cur.Parent;
        }

        if (chain.Count <= 1) return ""; // focus is on root itself

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("── hot focus (Android) ──");
        for (int i = 0; i < chain.Count; i++)
        {
            var node = chain[i];
            var indent = new string(' ', i * 2);
            var arrow = i == 0 ? "⌨ " : "└ ";
            var label = node.DisplayName;
            var labelStr = !string.IsNullOrEmpty(label)
                ? (label.Length > 40 ? $" \"{label[..37]}...\"" : $" \"{label}\"")
                : "";
            var rid = node.ShortResourceId;
            var ridStr = !string.IsNullOrEmpty(rid) ? $" ({rid})" : "";
            var flags = "";
            if (node.Clickable) flags += "*";
            if (node.Scrollable) flags += "~";
            if (node.Checked) flags += "!";

            sb.AppendLine($"  {indent}{arrow}[{node.ClassName}]{flags}{labelStr}{ridStr} {node.BoundsString}");
        }

        // ── Siblings of focused element ──
        var focusedNode = chain[0];
        var parentNode = focusedNode.Parent;
        if (parentNode != null && parentNode.Children.Count > 1)
        {
            sb.AppendLine("── siblings ──");
            foreach (var sib in parentNode.Children)
            {
                var isSelf = ReferenceEquals(sib, focusedNode);
                var sLabel = sib.DisplayName;
                var sDisp = string.IsNullOrEmpty(sLabel) ? "" : (sLabel.Length > 50 ? $"\"{sLabel[..47]}...\"" : $"\"{sLabel}\"");
                if (isSelf && !string.IsNullOrEmpty(sDisp))
                    sDisp = $"\"{(sLabel.Length > 50 ? sLabel[..47] + "..." : sLabel)}(포커스드)\"";
                else if (isSelf)
                    sDisp = "\"(포커스드)\"";
                var sRid = sib.ShortResourceId;
                var sRidStr = !string.IsNullOrEmpty(sRid) ? $" ({sRid})" : "";
                sb.AppendLine($"  [{sib.ClassName}] {sDisp}{sRidStr}");
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Collect the set of nodes on the hot focus chain (for depth-independent rendering).
    /// </summary>
    public static HashSet<AndroidNode> GetFocusChainNodes(AndroidNode root)
    {
        var set = new HashSet<AndroidNode>();
        var focused = FindInTree(root, n => n.Focused);
        if (focused == null) return set;

        var cur = focused;
        while (cur != null)
        {
            set.Add(cur);
            cur = cur.Parent;
        }
        return set;
    }
}

// ── Android Node ──────────────────────────────────────────

public class AndroidNode : IActionTarget
{
    public int Index { get; set; }
    public string Text { get; set; } = "";
    public string ResourceId { get; set; } = "";
    public string ClassName { get; set; } = "";
    public string Package { get; set; } = "";
    public string ContentDesc { get; set; } = "";
    public bool Checkable { get; set; }
    public bool Checked { get; set; }
    public bool Clickable { get; set; }
    public bool Enabled { get; set; }
    public bool Focusable { get; set; }
    public bool Focused { get; set; }
    public bool Scrollable { get; set; }
    public bool LongClickable { get; set; }
    public bool Selected { get; set; }

    public int BoundsLeft { get; set; }
    public int BoundsTop { get; set; }
    public int BoundsRight { get; set; }
    public int BoundsBottom { get; set; }

    public AndroidNode? Parent { get; set; }
    public List<AndroidNode> Children { get; } = [];

    // ── Computed properties ───────────────────────────────

    /// <summary>Display name: content-desc first, then text</summary>
    public string DisplayName => !string.IsNullOrEmpty(ContentDesc) ? ContentDesc
                               : !string.IsNullOrEmpty(Text) ? Text : "";

    /// <summary>Short resource-id: "com.kiwoom.heromts:id/rootFrame" → "rootFrame"</summary>
    public string ShortResourceId
    {
        get
        {
            if (string.IsNullOrEmpty(ResourceId)) return "";
            var slashIdx = ResourceId.LastIndexOf('/');
            return slashIdx >= 0 ? ResourceId[(slashIdx + 1)..] : ResourceId;
        }
    }

    /// <summary>Short package name: "com.kiwoom.heromts" → "heromts"</summary>
    public string ShortPackage
    {
        get
        {
            if (string.IsNullOrEmpty(Package)) return "";
            var dotIdx = Package.LastIndexOf('.');
            return dotIdx >= 0 ? Package[(dotIdx + 1)..] : Package;
        }
    }

    public int CenterX => (BoundsLeft + BoundsRight) / 2;
    public int CenterY => (BoundsTop + BoundsBottom) / 2;
    public int Width => BoundsRight - BoundsLeft;
    public int Height => BoundsBottom - BoundsTop;
    public string BoundsString => $"[{BoundsLeft},{BoundsTop}][{BoundsRight},{BoundsBottom}]";

    // ── IActionTarget explicit implementation ─────────────────
    // DisplayName, ClassName, Enabled, Focused already match by name.

    string? IActionTarget.Identifier => ResourceId;
    string IActionTarget.BackendType => "ADB";
    object? IActionTarget.NativeHandle => this;
    (int Left, int Top, int Right, int Bottom) IActionTarget.BoundingRect
        => (BoundsLeft, BoundsTop, BoundsRight, BoundsBottom);
    bool IActionTarget.Visible => Enabled && Width > 0 && Height > 0;
    bool IActionTarget.IsOffscreen => false;
    bool IActionTarget.IsWindow => false;
    string? IActionTarget.WindowState => null;
    IActionTarget? IActionTarget.Parent => Parent;
    IReadOnlyList<IActionTarget> IActionTarget.Children => Children;

    /// <summary>
    /// Windows-compatible search key for grap pattern matching.
    /// Format: [ClassName] DisplayName (shortPackage bounds=[l,t][r,b])
    /// </summary>
    public string SearchKey
    {
        get
        {
            var name = DisplayName;
            var nameStr = !string.IsNullOrEmpty(name) ? $" {name}" : "";
            var ridStr = !string.IsNullOrEmpty(ShortResourceId) ? $" aid={ShortResourceId}" : "";
            return $"[{ClassName}]{nameStr} ({ShortPackage}{ridStr} {BoundsString})";
        }
    }
}

// ── Grap-compatible pattern matcher (lightweight, no Win32 dependency) ──

/// <summary>
/// Simple pattern matcher: wildcard (*/?), regex:, or literal substring.
/// Same logic as Win32 PatternMatcher but self-contained for Android project.
/// </summary>
public sealed class GrapMatcher
{
    private readonly Regex? _regex;
    private readonly string? _literal;

    private GrapMatcher(Regex regex) => _regex = regex;
    private GrapMatcher(string literal) => _literal = literal;

    public static GrapMatcher Create(string pattern)
    {
        if (pattern.StartsWith("regex:", StringComparison.OrdinalIgnoreCase))
            return new GrapMatcher(new Regex(pattern[6..], RegexOptions.IgnoreCase | RegexOptions.Compiled));

        if (pattern.Contains('*') || pattern.Contains('?'))
        {
            var rx = Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".");
            return new GrapMatcher(new Regex(rx, RegexOptions.IgnoreCase | RegexOptions.Compiled));
        }

        return new GrapMatcher(pattern);
    }

    public bool IsMatch(string? text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        return _regex != null ? _regex.IsMatch(text) : text.Contains(_literal!, StringComparison.OrdinalIgnoreCase);
    }
}
