using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Focusless Tab control navigation: list tabs, select TabItem.
    /// Uses UIA SelectionItem pattern -- NO focus stealing!
    ///
    /// Usage:
    ///   wkappbot tab-select "영웅문4" --aid 3019 --select "조건식"
    ///   wkappbot tab-select "영웅문4" --aid 3019 --list
    ///   wkappbot tab-select "영웅문4" --aid 3019 --index 0
    ///   wkappbot tab-select "*영웅문*#*잔고확인*" --aid 1000 --list   (# = UIA scope)
    /// </summary>
    static int TabSelectCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot tab-select <window-title> --aid <tab-aid> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --aid <id>        Tab control AutomationId (required)");
            Console.WriteLine("  --list            List all TabItems");
            Console.WriteLine("  --select <text>   Select TabItem matching text (substring)");
            Console.WriteLine("  --index <N>       Select TabItem by index (0-based)");
            return 1;
        }

        var tabAid = GetArgValue(args, "--aid") ?? "";
        var selectTarget = GetArgValue(args, "--select") ?? "";
        var doList = args.Contains("--list");
        var indexStr = GetArgValue(args, "--index");

        if (string.IsNullOrEmpty(tabAid))
            return Error("--aid is required. Use inspect to find the Tab control's AutomationId.");

        // Resolve full grap: "window/child#uiaScope"
        UIA3Automation automation;
        AutomationElement root;
        IntPtr mainHwnd;
        try
        {
            automation = new UIA3Automation();
            automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
            automation.TransactionTimeout = TimeSpan.FromSeconds(5);

            var resolved = GrapHelper.ResolveFullGrap(args[0], automation);
            if (resolved == null)
                return Error("Failed to resolve grap pattern.");
            if (resolved.Value.error != null)
                return Error(resolved.Value.error);

            mainHwnd = resolved.Value.hwnd;
            root = resolved.Value.root;

            // Print resolved window info
            var winTitle = WindowFinder.GetWindowText(mainHwnd);
            Console.WriteLine($"Window: [{mainHwnd:X8}] \"{winTitle}\"");
        }
        catch (Exception ex)
        {
            return Error($"UIA init failed: {ex.Message}");
        }

        // Knowhow broadcast
        var cls = WindowFinder.GetClassName(mainHwnd);
        BroadcastInspectKnowhow(mainHwnd, cls, null, WindowFinder.GetWindowText(mainHwnd));

        // Find Tab control -- search for Tab and TabItem control types
        AutomationElement? tab = null;
        try
        {
            // Try ControlType.Tab first
            var allTabs = root.FindAllDescendants(x => x.ByControlType(ControlType.Tab));
            foreach (var t in allTabs)
            {
                if (t.Properties.AutomationId.ValueOrDefault == tabAid)
                {
                    tab = t;
                    break;
                }
            }

            // If not found as Tab, try as generic element by aid (MFC sometimes misreports type)
            if (tab == null)
            {
                var allElements = root.FindAllDescendants();
                foreach (var e in allElements)
                {
                    if (e.Properties.AutomationId.ValueOrDefault == tabAid)
                    {
                        tab = e;
                        Console.WriteLine($"  (found as ControlType={e.Properties.ControlType.ValueOrDefault})");
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return Error($"Tab search failed: {ex.Message}");
        }

        if (tab == null)
            return Error($"Tab with aid=\"{tabAid}\" not found.");

        var tabRect = tab.BoundingRectangle;
        Console.WriteLine($"Tab: aid=\"{tabAid}\" ({tabRect.Left},{tabRect.Top} {tabRect.Width}x{tabRect.Height})");

        // Find TabItems (children)
        AutomationElement[] tabItems;
        try
        {
            // Try TabItem type first
            tabItems = tab.FindAllChildren(x => x.ByControlType(ControlType.TabItem));

            // Fallback: if no TabItems, check all children
            if (tabItems.Length == 0)
            {
                var allChildren = tab.FindAllChildren();
                Console.WriteLine($"  No TabItem children, found {allChildren.Length} children of other types:");
                foreach (var c in allChildren)
                {
                    var cType = c.Properties.ControlType.ValueOrDefault;
                    var cName = c.Name ?? "(null)";
                    var cAid = c.Properties.AutomationId.ValueOrDefault ?? "";
                    var patterns = UiaLocator.GetSupportedPatterns(c);
                    Console.WriteLine($"    [{cType}] \"{cName}\" aid=\"{cAid}\" ({string.Join(",", patterns)})");
                }
                // Use all children as potential tab items
                tabItems = allChildren;
            }
        }
        catch (Exception ex)
        {
            return Error($"TabItem search failed: {ex.Message}");
        }

        Console.WriteLine($"TabItems: {tabItems.Length}");

        // --list: show all tabs
        if (doList)
        {
            for (int i = 0; i < tabItems.Length; i++)
            {
                var item = tabItems[i];
                var name = item.Name ?? "(no name)";
                var aid = item.Properties.AutomationId.ValueOrDefault ?? "";
                var selected = UiaLocator.IsSelected(item);
                var rect = item.BoundingRectangle;
                var patterns = UiaLocator.GetSupportedPatterns(item);
                var selMark = selected == true ? " ★" : "";
                Console.WriteLine($"  [{i}] \"{name}\" aid=\"{aid}\"{selMark} ({rect.Left},{rect.Top} {rect.Width}x{rect.Height}) ({string.Join(",", patterns)})");
            }
            return 0;
        }

        // --index N: select by index
        if (indexStr != null && int.TryParse(indexStr, out var idx))
        {
            if (idx < 0 || idx >= tabItems.Length)
                return Error($"Index {idx} out of range (0..{tabItems.Length - 1})");
            return DoTabSelect(tabItems[idx], idx, mainHwnd, tabRect);
        }

        // --select <text>: select by name
        if (!string.IsNullOrEmpty(selectTarget))
        {
            for (int i = 0; i < tabItems.Length; i++)
            {
                var name = tabItems[i].Name ?? "";
                if (name.Contains(selectTarget, StringComparison.OrdinalIgnoreCase))
                {
                    return DoTabSelect(tabItems[i], i, mainHwnd, tabRect);
                }
            }
            return Error($"No TabItem matching \"{selectTarget}\" found.");
        }

        Console.WriteLine("No action specified. Use --list, --select, or --index.");
        return 1;
    }

    static int DoTabSelect(AutomationElement tabItem, int index,
        IntPtr mainHwnd, System.Drawing.Rectangle tabCtrlRect)
    {
        var name = tabItem.Name ?? "(no name)";
        Console.WriteLine($"\nSelecting tab [{index}] \"{name}\"...");

        // Check current state
        var wasSel = UiaLocator.IsSelected(tabItem);
        Console.WriteLine($"  Before: IsSelected={wasSel}");

        // Try SelectionItem.Select() -- focusless! (with auto-zoom via ActionApi)
        var selected = ActionApi.Select(tabItem, mainHwnd, $"[{index}] {name}");
        Console.WriteLine($"  SelectionItem.Select(): {selected} (focusless!)");

        if (!selected)
        {
            // Fallback: try Invoke if available (also with zoom)
            var invoked = ActionApi.Invoke(tabItem, mainHwnd, $"[{index}] {name}");
            Console.WriteLine($"  Invoke fallback: {invoked}");
        }

        // Verify
        Thread.Sleep(200);
        var nowSel = UiaLocator.IsSelected(tabItem);
        Console.WriteLine($"  After: IsSelected={nowSel}");

        if (nowSel == true)
        {
            Console.WriteLine($"  v Tab \"{name}\" selected successfully!");
            return 0;
        }
        else
        {
            Console.WriteLine($"  X Tab selection may have failed (IsSelected={nowSel})");
            return selected ? 0 : 1; // return success if Select() succeeded even if IsSelected is unclear
        }
    }
}
