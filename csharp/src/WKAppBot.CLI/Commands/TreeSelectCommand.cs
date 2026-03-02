using System.Drawing;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Focusless TreeView navigation: expand, select, list TreeItems.
    /// Uses UIA patterns (ExpandCollapse, SelectionItem, ScrollItem) — NO focus stealing!
    ///
    /// Usage:
    ///   wkappbot tree-select "영웅문4" --aid 10003 --select "상7하7돈7순위"
    ///   wkappbot tree-select "영웅문4" --aid 10003 --expand "종목"
    ///   wkappbot tree-select "영웅문4" --aid 10003 --list
    ///   wkappbot tree-select "영웅문4" --aid 10003 --expand-all
    /// </summary>
    static int TreeSelectCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot tree-select <window-title> --aid <tree-aid> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --aid <id>        Tree control AutomationId (required)");
            Console.WriteLine("  --list            List all TreeItems (expanded)");
            Console.WriteLine("  --select <text>   Select TreeItem matching text (substring)");
            Console.WriteLine("  --expand <text>   Expand TreeItem matching text");
            Console.WriteLine("  --collapse <text> Collapse TreeItem matching text");
            Console.WriteLine("  --expand-all      Expand all root-level items");
            Console.WriteLine("  --depth <N>       Max depth for --list (default: 3)");
            return 1;
        }

        var (title, uiaScope) = GrapHelper.SplitHash(args[0]);
        var treeAid = GetArgValue(args, "--aid") ?? "";
        var selectTarget = GetArgValue(args, "--select") ?? "";
        var expandTarget = GetArgValue(args, "--expand") ?? "";
        var collapseTarget = GetArgValue(args, "--collapse") ?? "";
        var doList = args.Contains("--list");
        var doExpandAll = args.Contains("--expand-all");
        var maxDepth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 3;

        if (string.IsNullOrEmpty(treeAid))
            return Error("--aid is required. Use inspect to find the Tree control's AutomationId.");

        // Find window
        var matches = WindowFinder.FindByTitle(title);
        if (matches.Count == 0) return Error($"Window not found: {title}");
        var mainHwnd = matches[0].Handle;
        Console.WriteLine($"Window: [{mainHwnd:X8}] \"{matches[0].Title}\"");

        // Initialize UIA
        UIA3Automation automation;
        AutomationElement root;
        try
        {
            automation = new UIA3Automation();
            automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
            automation.TransactionTimeout = TimeSpan.FromSeconds(5);
            root = automation.FromHandle(mainHwnd);

            // '#' scope narrowing
            if (!string.IsNullOrEmpty(uiaScope))
            {
                var scoped = GrapHelper.FindUiaScope(root, uiaScope);
                if (scoped == null) return Error($"UIA scope not found: \"{uiaScope}\"");
                root = scoped;
                Console.WriteLine($"  UIA scope: \"{scoped.Properties.Name.ValueOrDefault}\"");
            }
        }
        catch (Exception ex)
        {
            return Error($"UIA init failed: {ex.Message}");
        }

        // Find Tree control by AutomationId
        AutomationElement? tree = null;
        try
        {
            var allTrees = root.FindAllDescendants(x => x.ByControlType(ControlType.Tree));
            foreach (var t in allTrees)
            {
                if (t.Properties.AutomationId.ValueOrDefault == treeAid)
                {
                    tree = t;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            return Error($"Tree search failed: {ex.Message}");
        }

        if (tree == null)
            return Error($"Tree with aid=\"{treeAid}\" not found.");

        var treeRect = tree.BoundingRectangle;
        Console.WriteLine($"Tree: aid=\"{treeAid}\" ({treeRect.Left},{treeRect.Top} {treeRect.Width}x{treeRect.Height})");

        // --expand-all: expand all root-level TreeItems
        if (doExpandAll)
        {
            return ExpandAllRoots(tree);
        }

        // --expand <text>: expand matching TreeItem
        if (!string.IsNullOrEmpty(expandTarget))
        {
            return ExpandMatchingItem(tree, expandTarget, mainHwnd);
        }

        // --collapse <text>: collapse matching TreeItem
        if (!string.IsNullOrEmpty(collapseTarget))
        {
            return CollapseMatchingItem(tree, collapseTarget, mainHwnd);
        }

        // --select <text>: select matching TreeItem
        if (!string.IsNullOrEmpty(selectTarget))
        {
            return SelectMatchingItem(tree, selectTarget, mainHwnd);
        }

        // --list: list all TreeItems
        if (doList)
        {
            return ListTreeItems(tree, maxDepth);
        }

        Console.WriteLine("No action specified. Use --list, --select, --expand, --collapse, or --expand-all.");
        return 1;
    }

    static int ExpandAllRoots(AutomationElement tree)
    {
        var children = tree.FindAllChildren(x => x.ByControlType(ControlType.TreeItem));
        Console.WriteLine($"\nExpanding {children.Length} root items...");
        int ok = 0;
        foreach (var child in children)
        {
            var name = child.Name ?? "(no name)";
            var state = UiaLocator.GetExpandCollapseState(child);
            if (state == ExpandCollapseState.Expanded)
            {
                Console.WriteLine($"  [{name}] already expanded");
                ok++;
                continue;
            }
            var expanded = UiaLocator.TryExpand(child);
            Console.WriteLine($"  [{name}] expand={expanded}");
            if (expanded) ok++;
        }
        Console.WriteLine($"\nDone: {ok}/{children.Length} expanded");
        return 0;
    }

    static int ExpandMatchingItem(AutomationElement tree, string target, IntPtr mainHwnd)
    {
        var items = tree.FindAllDescendants(x => x.ByControlType(ControlType.TreeItem));
        foreach (var item in items)
        {
            var name = item.Name ?? "";
            if (name.Contains(target, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\nFound: \"{name}\"");

                var br = item.BoundingRectangle;
                var zoomRect = new Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
                using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, mainHwnd, "tree_expand", name);

                var scrolled = UiaLocator.TryScrollIntoView(item);
                Console.WriteLine($"  ScrollIntoView: {scrolled}");
                var state = UiaLocator.GetExpandCollapseState(item);
                Console.WriteLine($"  State: {state}");
                var expanded = UiaLocator.TryExpand(item);
                Console.WriteLine($"  Expand: {expanded} (focusless!)");

                // List children after expand
                Thread.Sleep(200); // let tree update
                var children = item.FindAllChildren(x => x.ByControlType(ControlType.TreeItem));
                if (children.Length > 0)
                {
                    Console.WriteLine($"  Children ({children.Length}):");
                    foreach (var c in children)
                        Console.WriteLine($"    [{c.Name}]");
                }

                if (expanded) zoom?.ShowPass($"Expand \"{name}\" OK");
                else zoom?.ShowFail($"Expand \"{name}\" failed");
                return 0;
            }
        }
        return Error($"No TreeItem matching \"{target}\" found.");
    }

    static int CollapseMatchingItem(AutomationElement tree, string target, IntPtr mainHwnd)
    {
        var items = tree.FindAllDescendants(x => x.ByControlType(ControlType.TreeItem));
        foreach (var item in items)
        {
            var name = item.Name ?? "";
            if (name.Contains(target, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\nFound: \"{name}\"");

                var br = item.BoundingRectangle;
                var zoomRect = new Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
                using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, mainHwnd, "tree_collapse", name);

                var collapsed = UiaLocator.TryCollapse(item);
                Console.WriteLine($"  Collapse: {collapsed} (focusless!)");

                if (collapsed) zoom?.ShowPass($"Collapse \"{name}\" OK");
                else zoom?.ShowFail($"Collapse \"{name}\" failed");
                return 0;
            }
        }
        return Error($"No TreeItem matching \"{target}\" found.");
    }

    static int SelectMatchingItem(AutomationElement tree, string target, IntPtr mainHwnd)
    {
        var items = tree.FindAllDescendants(x => x.ByControlType(ControlType.TreeItem));
        Console.WriteLine($"\nSearching {items.Length} items for \"{target}\"...");

        foreach (var item in items)
        {
            var name = item.Name ?? "";
            if (name.Contains(target, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  Match: \"{name}\"");

                var br = item.BoundingRectangle;
                var zoomRect = new Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
                using var zoom = ClickZoomHelper.BeginFromRect(zoomRect, mainHwnd, "tree_select", name);

                // Scroll into view first
                var scrolled = UiaLocator.TryScrollIntoView(item);
                if (scrolled)
                    Console.WriteLine($"  ScrollIntoView: OK");

                // Select
                var selected = UiaLocator.TrySelect(item);
                Console.WriteLine($"  Select: {selected} (focusless!)");

                // Verify
                Thread.Sleep(100);
                var isSelected = UiaLocator.IsSelected(item);
                var rect = item.BoundingRectangle;
                Console.WriteLine($"  IsSelected: {isSelected}");
                Console.WriteLine($"  Rect: ({rect.Left},{rect.Top} {rect.Width}x{rect.Height})");

                if (selected) zoom?.ShowPass($"Select \"{name}\" OK");
                else zoom?.ShowFail($"Select \"{name}\" failed");

                return selected ? 0 : 1;
            }
        }
        return Error($"No TreeItem matching \"{target}\" found.");
    }

    static int ListTreeItems(AutomationElement tree, int maxDepth)
    {
        Console.WriteLine($"\n── TreeItems (depth={maxDepth}) ──\n");
        ListTreeItemsRecursive(tree, 0, maxDepth);
        return 0;
    }

    static void ListTreeItemsRecursive(AutomationElement parent, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        AutomationElement[] children;
        try
        {
            children = parent.FindAllChildren(x => x.ByControlType(ControlType.TreeItem));
        }
        catch
        {
            return;
        }

        foreach (var child in children)
        {
            var name = child.Name ?? "(no name)";
            var state = UiaLocator.GetExpandCollapseState(child);
            var selected = UiaLocator.IsSelected(child);
            var rect = child.BoundingRectangle;

            var prefix = new string(' ', depth * 2);
            var stateStr = state switch
            {
                ExpandCollapseState.Expanded => "[−]",
                ExpandCollapseState.Collapsed => "[+]",
                ExpandCollapseState.LeafNode => "   ",
                _ => "[ ]"
            };
            var selStr = selected == true ? " ★" : "";
            var posStr = rect.Width > 0 ? $" ({rect.Left},{rect.Top})" : "";

            Console.WriteLine($"{prefix}{stateStr} {name}{selStr}{posStr}");

            if (state == ExpandCollapseState.Expanded && depth < maxDepth)
            {
                ListTreeItemsRecursive(child, depth + 1, maxDepth);
            }
        }
    }
}
