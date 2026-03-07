using WKAppBot.Android;

namespace WKAppBot.CLI;

/// <summary>
/// Android ADB dispatch for a11y commands.
/// Handles adb:// grap patterns — routes to ADB instead of Win32/UIA.
/// Experience DB integration: inspect/actions save to A11Y + OS paths.
///
/// Supported actions (26):
///   Discovery: inspect, find, windows, screenshot, ocr
///   Window: close, minimize, maximize, restore, focus, move, resize
///   Element: read, highlight, invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range
///   Async: wait, eval
///   Android-specific: back, home, recent, long-press
/// </summary>
internal partial class Program
{
    private static readonly Lazy<AdbExperienceDb> _adbExpDb = new(() => new AdbExperienceDb(DataDir));
    private static AdbExperienceDb AdbExpDb => _adbExpDb.Value;

    // ── Entry point ───────────────────────────────────────

    /// <summary>
    /// Dispatch a11y command to Android ADB pipeline.
    /// args[0] = action, args[1] = adb://... grap, args[2..] = options
    /// </summary>
    static int AdbA11yDispatch(string[] args)
    {
        var action = args[0].ToLowerInvariant();
        var grapStr = args.Length > 1 ? args[1] : "adb://";
        var grap = AdbGrapRouter.Parse(grapStr);

        var adb = new AdbClient();
        var registry = new AdbDeviceRegistry(adb, DataDir);

        // ── Resolve device ────────────────────────────────
        var resolved = registry.ResolveDevice(grap.Device);
        if (resolved == null)
        {
            var devices = adb.ListDevices();
            if (devices.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ADB] No Android devices connected");
                Console.ResetColor();
                return 1;
            }
            if (grap.Device == null && devices.Count > 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ADB] Multiple devices connected — specify device in grap:");
                Console.ResetColor();
                foreach (var d in devices)
                    Console.WriteLine($"  adb://{d.Model ?? d.Serial}/...");
                return 1;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ADB] Device '{grap.Device}' not found");
            Console.ResetColor();
            return 1;
        }

        var (serial, displayId) = resolved.Value;

        // ── Dispatch by action ────────────────────────────
        return action switch
        {
            // Discovery
            "windows"    => AdbWindows(adb, registry),
            "inspect"    => AdbInspect(adb, serial, grap, args),
            "find"       => AdbInspect(adb, serial, grap, args),
            "screenshot" => AdbScreenshot(adb, serial, displayId, args),
            "ocr"        => AdbOcr(adb, serial, displayId, args),
            // Window
            "close"      => AdbClose(adb, serial, grap),
            "minimize"   => AdbKeyAction(adb, serial, "HOME", "minimize", () => adb.Home(serial)),
            "maximize"   => AdbWindowStub("maximize"),
            "restore"    => AdbKeyAction(adb, serial, "RECENT→select", "restore", () => adb.RecentApps(serial)),
            "move"       => AdbWindowStub("move"),
            "resize"     => AdbWindowStub("resize"),
            // Element
            "click"      => AdbClick(adb, serial, grap),
            "invoke"     => AdbClick(adb, serial, grap), // invoke = click alias
            "read"       => AdbRead(adb, serial, grap),
            "highlight"  => AdbHighlight(adb, serial, grap),
            "toggle"     => AdbToggle(adb, serial, grap),
            "expand"     => AdbExpandCollapse(adb, serial, grap, expand: true),
            "collapse"   => AdbExpandCollapse(adb, serial, grap, expand: false),
            "select"     => AdbSelect(adb, serial, grap, args),
            "scroll"     => AdbScroll(adb, serial, grap, args),
            "type"       => AdbType(adb, serial, grap, args),
            "set-value"  => AdbSetValue(adb, serial, grap, args),
            "set-range"  => AdbSetRange(adb, serial, grap, args),
            "focus"      => AdbFocus(adb, serial, grap),
            // Async
            "wait"       => AdbWait(adb, serial, grap, args),
            "eval"       => AdbEval(adb, serial, args),
            // Android-specific
            "back"       => AdbKeyAction(adb, serial, "BACK", "back", () => adb.Back(serial)),
            "home"       => AdbKeyAction(adb, serial, "HOME", "home", () => adb.Home(serial)),
            "recent"     => AdbKeyAction(adb, serial, "RECENT", "recent", () => adb.RecentApps(serial)),
            "long-press" => AdbLongPress(adb, serial, grap, args),
            _ => AdbUnsupported(action)
        };
    }

    // ── Windows (list devices + running apps) ─────────────

    static int AdbWindows(AdbClient adb, AdbDeviceRegistry registry)
    {
        var all = registry.ListAll();
        Console.WriteLine($"[ADB] {all.Count} device(s):");
        foreach (var info in all)
        {
            var alias = info.Alias != null ? $" [{info.Alias}]" : "";
            var state = info.Device.IsOnline ? "online" : info.Device.State;
            Console.ForegroundColor = info.Device.IsOnline ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.Write($"  {info.Device.DisplayName}");
            Console.ResetColor();
            Console.Write($" ({info.Device.Serial}){alias} [{state}]");
            if (info.Displays.Count > 0)
                Console.Write($" displays:[{string.Join(",", info.Displays.Select(d => d.IsInner ? "inner" : "outer"))}]");
            Console.WriteLine();

            // Show current activity
            if (info.Device.IsOnline)
            {
                var activity = adb.GetCurrentActivity(info.Device.Serial);
                if (activity != null)
                    Console.WriteLine($"    → {activity}");
            }
        }
        return 0;
    }

    // ── Inspect (dump a11y tree) ──────────────────────────

    static int AdbInspect(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var depth = 5;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--depth" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out depth);

        Console.Write("[ADB] Dumping UI tree... ");
        var tree = new AndroidA11yTree(adb);
        var root = tree.GetRoot(serial, forceRefresh: true);
        if (root == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAILED (uiautomator dump failed)");
            Console.ResetColor();
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        // Find target subtree
        var target = root;
        if (grap.Package != null)
        {
            var pkgMatches = tree.FindByPackage(root, grap.Package);
            if (pkgMatches.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ADB] No package matching '{grap.Package}'");
                Console.ResetColor();
                // Still dump from root
            }
            else
            {
                target = pkgMatches[0]; // Use first package match as root
                Console.WriteLine($"[ADB] Package: {target.Package}");
            }
        }

        // Resolve #scope
        if (grap.Scopes.Length > 0)
        {
            var scoped = tree.ResolveScope(target, grap.Scopes);
            if (scoped != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[ADB] Scope: {scoped.SearchKey}");
                Console.ResetColor();
                target = scoped;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ADB] Scope '{grap.ScopePath}' not found — showing from package root");
                Console.ResetColor();
            }
        }

        // Dump tree
        Console.WriteLine();
        var treeDump = AndroidA11yTree.DumpTree(target, depth);
        Console.Write(treeDump);

        // ── Experience: save tree snapshot + broadcast paths/knowhow ──
        var pkg = target.Package;
        if (string.IsNullOrEmpty(pkg) && grap.Package != null)
        {
            var pkgs = tree.FindByPackage(root, grap.Package);
            if (pkgs.Count > 0) pkg = pkgs[0].Package;
        }
        if (!string.IsNullOrEmpty(pkg))
        {
            var screenName = grap.Scopes.Length > 0 ? grap.Scopes[0] : null;
            var saved = AdbExpDb.SaveTreeSnapshot(pkg, treeDump, screenName);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n[EXP] tree → {Path.GetRelativePath(DataDir, saved)}");
            Console.ResetColor();

            BroadcastAdbPaths(pkg, screenName);
        }

        return 0;
    }

    // ── Screenshot ────────────────────────────────────────

    static int AdbScreenshot(AdbClient adb, string serial, string? displayId, string[] args)
    {
        var output = Path.Combine(DataDir, "output", "android_screen.png");
        for (int i = 0; i < args.Length; i++)
            if ((args[i] == "-o" || args[i] == "--output") && i + 1 < args.Length)
                output = args[i + 1];

        Console.Write("[ADB] Capturing screenshot... ");
        if (adb.Screencap(output, serial, displayId))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"OK → {output}");
            Console.ResetColor();
            return 0;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("FAILED");
        Console.ResetColor();
        return 1;
    }

    // ── OCR (screencap + local SimpleOcr) ─────────────────

    static int AdbOcr(AdbClient adb, string serial, string? displayId, string[] args)
    {
        var imgPath = Path.Combine(DataDir, "output", "android_ocr_temp.png");
        Console.Write("[ADB] Capturing for OCR... ");
        if (!adb.Screencap(imgPath, serial, displayId))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAILED (screencap)");
            Console.ResetColor();
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("OK");
        Console.ResetColor();

        // Delegate to existing OCR command with the captured image
        var ocrArgs = new List<string> { imgPath };
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--save" || args[i] == "-o")
                ocrArgs.Add(args[i]);
            if ((args[i] == "-o") && i + 1 < args.Length)
                ocrArgs.Add(args[++i]);
        }
        return OcrCommand(ocrArgs.ToArray());
    }

    // ── Click ─────────────────────────────────────────────

    static int AdbClick(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.Write($"[ADB] Tap ({node.CenterX},{node.CenterY}) {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        LogAdbAction("click", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Read ──────────────────────────────────────────────

    static int AdbRead(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.WriteLine($"[ADB] Node: {node.SearchKey}");
        Console.WriteLine($"  class:        {node.ClassName}");
        Console.WriteLine($"  text:         {node.Text}");
        Console.WriteLine($"  content-desc: {node.ContentDesc}");
        Console.WriteLine($"  resource-id:  {node.ResourceId}");
        Console.WriteLine($"  package:      {node.Package}");
        Console.WriteLine($"  bounds:       {node.BoundsString} ({node.Width}x{node.Height})");
        Console.WriteLine($"  center:       ({node.CenterX},{node.CenterY})");
        Console.WriteLine($"  clickable:    {node.Clickable}  scrollable: {node.Scrollable}");
        Console.WriteLine($"  checkable:    {node.Checkable}  checked: {node.Checked}");
        Console.WriteLine($"  focusable:    {node.Focusable}  focused: {node.Focused}");
        Console.WriteLine($"  enabled:      {node.Enabled}  selected: {node.Selected}");
        Console.WriteLine($"  children:     {node.Children.Count}");
        return 0;
    }

    // ── Highlight (print bounds info for AI/MCP) ──────────

    static int AdbHighlight(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ADB] Highlight: {node.SearchKey}");
        Console.ResetColor();
        Console.WriteLine($"  bounds: {node.BoundsString}");
        Console.WriteLine($"  center: ({node.CenterX},{node.CenterY})");
        Console.WriteLine($"  size:   {node.Width}x{node.Height}");

        // Also show siblings for context
        if (node.Parent != null)
        {
            var siblings = node.Parent.Children.Where(c => c != node).Take(5).ToList();
            if (siblings.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  siblings ({node.Parent.Children.Count - 1}):");
                foreach (var s in siblings)
                    Console.WriteLine($"    [{s.ClassName}] {s.DisplayName} {s.BoundsString}");
                Console.ResetColor();
            }
        }
        return 0;
    }

    // ── Toggle (tap + verify checked state) ───────────────

    static int AdbToggle(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        var beforeChecked = node.Checked;
        Console.Write($"[ADB] Toggle ({(beforeChecked ? "ON→OFF" : "OFF→ON")}) {node.DisplayName}... ");

        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        if (!r.IsOk)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"FAILED: {r.StdErr}");
            Console.ResetColor();
            LogAdbAction("toggle", node, grap, serial, false, r.StdErr);
            return 1;
        }

        // Re-dump and verify state change
        Thread.Sleep(500);
        var tree = new AndroidA11yTree(adb);
        var root = tree.GetRoot(serial, forceRefresh: true);
        var afterNode = root != null ? ReResolveNode(tree, root, grap) : null;

        if (afterNode != null && afterNode.Checked != beforeChecked)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"OK (checked={afterNode.Checked})");
            Console.ResetColor();
            LogAdbAction("toggle", node, grap, serial, true, $"checked: {beforeChecked}→{afterNode.Checked}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("OK (tap sent, state unverified)");
            Console.ResetColor();
            LogAdbAction("toggle", node, grap, serial, true, "state unverified");
        }
        return 0;
    }

    // ── Expand / Collapse (tap + verify subtree change) ───

    static int AdbExpandCollapse(AdbClient adb, string serial, AdbGrapInfo grap, bool expand)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        var actionName = expand ? "expand" : "collapse";
        var beforeChildren = node.Children.Count;
        Console.Write($"[ADB] {(expand ? "Expand" : "Collapse")} {node.DisplayName} (children={beforeChildren})... ");

        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        if (!r.IsOk)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"FAILED: {r.StdErr}");
            Console.ResetColor();
            LogAdbAction(actionName, node, grap, serial, false, r.StdErr);
            return 1;
        }

        // Re-dump and check subtree delta
        Thread.Sleep(500);
        var tree = new AndroidA11yTree(adb);
        var root = tree.GetRoot(serial, forceRefresh: true);
        var afterNode = root != null ? ReResolveNode(tree, root, grap) : null;

        if (afterNode != null)
        {
            var afterChildren = afterNode.Children.Count;
            var delta = afterChildren - beforeChildren;
            var verified = expand ? delta > 0 : delta < 0;
            Console.ForegroundColor = verified ? ConsoleColor.Green : ConsoleColor.Yellow;
            Console.WriteLine(verified
                ? $"OK (children: {beforeChildren}→{afterChildren})"
                : $"OK (tap sent, children: {beforeChildren}→{afterChildren})");
            Console.ResetColor();
            LogAdbAction(actionName, node, grap, serial, true, $"children: {beforeChildren}→{afterChildren}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("OK (tap sent, state unverified)");
            Console.ResetColor();
            LogAdbAction(actionName, node, grap, serial, true, "state unverified");
        }
        return 0;
    }

    // ── Select (tap target item) ──────────────────────────

    static int AdbSelect(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.Write($"[ADB] Select {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        // Verify selected state
        if (ok)
        {
            Thread.Sleep(300);
            var tree = new AndroidA11yTree(adb);
            var root = tree.GetRoot(serial, forceRefresh: true);
            var afterNode = root != null ? ReResolveNode(tree, root, grap) : null;
            if (afterNode != null && afterNode.Selected)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[ADB] Verified: selected=true");
                Console.ResetColor();
            }
        }

        LogAdbAction("select", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Scroll ────────────────────────────────────────────

    static int AdbScroll(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        var direction = "down";
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--direction" && i + 1 < args.Length)
                direction = args[i + 1].ToLowerInvariant();

        int x1 = node.CenterX, y1 = node.CenterY, x2 = x1, y2 = y1;
        var dist = node.Height / 3;
        switch (direction)
        {
            case "up":    y1 += dist; y2 -= dist; break;
            case "down":  y1 -= dist; y2 += dist; break;
            case "left":  x1 += dist; x2 -= dist; break;
            case "right": x1 -= dist; x2 += dist; break;
        }

        Console.Write($"[ADB] Swipe {direction}... ");
        var r = adb.Swipe(x1, y1, x2, y2, 300, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        LogAdbAction($"scroll-{direction}", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Type (text input) ─────────────────────────────────

    static int AdbType(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        string? text = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--text" && i + 1 < args.Length)
                text = args[i + 1];

        if (text == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --text required for type action");
            Console.ResetColor();
            return 1;
        }

        // Click target first to focus
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.Write($"[ADB] Tap to focus ({node.CenterX},{node.CenterY})... ");
        adb.Tap(node.CenterX, node.CenterY, serial);
        Console.WriteLine("OK");
        Thread.Sleep(200);

        return AdbInputText(adb, serial, text, node, grap, "type");
    }

    // ── Set-Value (clear existing + type new text) ────────

    static int AdbSetValue(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        string? text = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--text" && i + 1 < args.Length)
                text = args[i + 1];

        if (text == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --text required for set-value action");
            Console.ResetColor();
            return 1;
        }

        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        // Tap to focus
        Console.Write($"[ADB] Tap to focus ({node.CenterX},{node.CenterY})... ");
        adb.Tap(node.CenterX, node.CenterY, serial);
        Console.WriteLine("OK");
        Thread.Sleep(200);

        // Select all + delete existing text
        Console.Write("[ADB] Clear existing text... ");
        adb.KeyEvent("KEYCODE_MOVE_HOME", serial);
        adb.Shell("input keyevent --longpress KEYCODE_SHIFT_LEFT KEYCODE_MOVE_END", serial);
        Thread.Sleep(100);
        adb.KeyEvent("KEYCODE_DEL", serial);
        Thread.Sleep(100);
        Console.WriteLine("OK");

        return AdbInputText(adb, serial, text, node, grap, "set-value");
    }

    // ── Set-Range (seekbar: calculate position + tap) ─────

    static int AdbSetRange(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        double? value = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--value" && i + 1 < args.Length)
            {
                if (double.TryParse(args[i + 1], out var v))
                    value = v;
            }
        }

        if (value == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --value (0.0~1.0) required for set-range action");
            Console.ResetColor();
            return 1;
        }

        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        // Clamp 0~1, map to X position within bounds
        var ratio = Math.Clamp(value.Value, 0.0, 1.0);
        var targetX = node.BoundsLeft + (int)(node.Width * ratio);
        var targetY = node.CenterY;

        Console.Write($"[ADB] Set range {ratio:P0} → tap ({targetX},{targetY})... ");
        var r = adb.Tap(targetX, targetY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        LogAdbAction("set-range", node, grap, serial, ok, $"ratio={ratio:F2}");
        return ok ? 0 : 1;
    }

    // ── Focus (tap element center) ────────────────────────

    static int AdbFocus(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.Write($"[ADB] Focus tap ({node.CenterX},{node.CenterY}) {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        LogAdbAction("focus", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Wait (poll until element appears) ─────────────────

    static int AdbWait(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var timeoutMs = 10000;
        var intervalMs = 1000;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out timeoutMs);
            if (args[i] == "--interval" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out intervalMs);
        }

        if (grap.Scopes.Length == 0 && grap.Package == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] wait requires a target (package and/or #scope)");
            Console.ResetColor();
            return 1;
        }

        Console.Write($"[ADB] Waiting for '{grap.ScopePath ?? grap.Package}' (timeout={timeoutMs}ms)... ");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var tree = new AndroidA11yTree(adb);

        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            var root = tree.GetRoot(serial, forceRefresh: true);
            if (root != null)
            {
                var target = root;
                if (grap.Package != null)
                {
                    var pkgs = tree.FindByPackage(root, grap.Package);
                    if (pkgs.Count > 0) target = pkgs[0];
                    else { Thread.Sleep(intervalMs); continue; }
                }
                if (grap.Scopes.Length > 0)
                {
                    var scoped = tree.ResolveScope(target, grap.Scopes);
                    if (scoped != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"FOUND ({sw.ElapsedMilliseconds}ms) {scoped.SearchKey}");
                        Console.ResetColor();
                        return 0;
                    }
                }
                else
                {
                    // Package-only wait — package found
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"FOUND ({sw.ElapsedMilliseconds}ms) {target.Package}");
                    Console.ResetColor();
                    return 0;
                }
            }
            Thread.Sleep(intervalMs);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"TIMEOUT ({timeoutMs}ms)");
        Console.ResetColor();
        return 1;
    }

    // ── Eval (adb shell command execution) ────────────────

    static int AdbEval(AdbClient adb, string serial, string[] args)
    {
        string? cmd = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--text" && i + 1 < args.Length)
                cmd = args[i + 1];

        if (cmd == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --text \"shell command\" required for eval action");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[ADB] eval: {cmd}");
        Console.ResetColor();

        var r = adb.ShellRaw(cmd, serial, 30000);
        if (!string.IsNullOrWhiteSpace(r.StdOut))
            Console.Write(r.StdOut);
        if (!string.IsNullOrWhiteSpace(r.StdErr))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(r.StdErr);
            Console.ResetColor();
        }

        return r.ExitCode;
    }

    // ── Long-press ────────────────────────────────────────

    static int AdbLongPress(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        var durationMs = 1000;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--duration" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out durationMs);

        Console.Write($"[ADB] Long-press ({node.CenterX},{node.CenterY}) {durationMs}ms {node.DisplayName}... ");
        var r = adb.LongPress(node.CenterX, node.CenterY, durationMs, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        LogAdbAction("long-press", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Close (force-stop app) ────────────────────────────

    static int AdbClose(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        if (grap.Package == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] Package required for close action");
            Console.ResetColor();
            return 1;
        }

        // Resolve full package name from tree
        var tree = new AndroidA11yTree(adb);
        var root = tree.GetRoot(serial, forceRefresh: true);
        var fullPkg = grap.Package;
        if (root != null)
        {
            var pkgs = tree.FindByPackage(root, grap.Package);
            if (pkgs.Count > 0) fullPkg = pkgs[0].Package;
        }

        Console.Write($"[ADB] Force-stop {fullPkg}... ");
        var r = adb.ForceStop(fullPkg, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();

        if (!string.IsNullOrEmpty(fullPkg))
            AdbExpDb.LogAction(fullPkg, new AdbActionLog
            {
                Action = "close", Package = fullPkg, Device = serial,
                Success = ok, Detail = ok ? null : r.StdErr,
            });
        return ok ? 0 : 1;
    }

    // ── Key action helper (back/home/recent/minimize) ─────

    static int AdbKeyAction(AdbClient adb, string serial, string keyName, string actionName, Func<AdbResult> action)
    {
        Console.Write($"[ADB] {actionName} (keyevent {keyName})... ");
        var r = action();
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        return ok ? 0 : 1;
    }

    // ── Window stubs (not meaningful on Android) ──────────

    static int AdbWindowStub(string action)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ADB] '{action}' is not supported on Android (apps are fullscreen)");
        Console.ResetColor();
        return 0; // Not an error — just no-op
    }

    // ── Helpers ───────────────────────────────────────────

    static AndroidNode? ResolveAdbTarget(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var tree = new AndroidA11yTree(adb);
        var root = tree.GetRoot(serial, forceRefresh: true);
        if (root == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] Failed to dump UI tree");
            Console.ResetColor();
            return null;
        }

        var target = root;

        // Package matching
        if (grap.Package != null)
        {
            var pkgs = tree.FindByPackage(root, grap.Package);
            if (pkgs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ADB] No package matching '{grap.Package}'");
                Console.ResetColor();
                return null;
            }
            target = pkgs[0];
        }

        // Scope resolution
        if (grap.Scopes.Length > 0)
        {
            var scoped = tree.ResolveScope(target, grap.Scopes);
            if (scoped == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ADB] Scope '{grap.ScopePath}' not found");
                Console.ResetColor();

                // Show available targets for debugging
                Console.ForegroundColor = ConsoleColor.Yellow;
                var candidates = tree.FindAllInScope(target, grap.Scopes[0]);
                if (candidates.Count > 0)
                {
                    Console.WriteLine("[ADB] Similar matches:");
                    foreach (var c in candidates.Take(5))
                        Console.WriteLine($"  {c.SearchKey}");
                }
                Console.ResetColor();
                return null;
            }
            target = scoped;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[ADB] Target: {target.SearchKey}");
        Console.ResetColor();
        return target;
    }

    /// <summary>Re-resolve the same target after tree refresh (for state verification)</summary>
    static AndroidNode? ReResolveNode(AndroidA11yTree tree, AndroidNode root, AdbGrapInfo grap)
    {
        var target = root;
        if (grap.Package != null)
        {
            var pkgs = tree.FindByPackage(root, grap.Package);
            if (pkgs.Count == 0) return null;
            target = pkgs[0];
        }
        if (grap.Scopes.Length > 0)
            return tree.ResolveScope(target, grap.Scopes);
        return target;
    }

    /// <summary>Shared text input logic: ADB Keyboard → clipboard paste → ASCII input</summary>
    static int AdbInputText(AdbClient adb, string serial, string text, AndroidNode node, AdbGrapInfo grap, string actionName)
    {
        Console.Write($"[ADB] Input text \"{text}\"... ");
        var r = adb.BroadcastText(text, serial);
        if (r.IsOk && r.StdOut.Contains("result=0"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK (ADB Keyboard)");
            Console.ResetColor();
            LogAdbAction(actionName, node, grap, serial, true, $"ADB Keyboard, text={text}");
            return 0;
        }

        Console.Write("(ADB Keyboard not available, trying clipboard paste)... ");
        r = adb.ClipboardPaste(text, serial);
        if (r.IsOk)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK (clipboard paste)");
            Console.ResetColor();
            LogAdbAction(actionName, node, grap, serial, true, $"clipboard paste, text={text}");
            return 0;
        }

        if (text.All(c => c < 128))
        {
            r = adb.InputText(text, serial);
            if (r.IsOk)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK (ASCII input)");
                Console.ResetColor();
                LogAdbAction(actionName, node, grap, serial, true, $"ASCII input, text={text}");
                return 0;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"FAILED: {r.StdErr}");
        Console.ResetColor();
        LogAdbAction(actionName, node, grap, serial, false, r.StdErr);
        return 1;
    }

    static int AdbUnsupported(string action)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ADB] Action '{action}' not supported for Android");
        Console.WriteLine("[ADB] Supported: inspect find windows screenshot ocr | close minimize maximize restore | "
            + "click invoke read highlight toggle expand collapse select scroll type set-value set-range focus | "
            + "wait eval | back home recent long-press");
        Console.ResetColor();
        return 1;
    }

    // ── Experience DB helpers ────────────────────────────

    /// <summary>Log action result to experience DB (actions.jsonl)</summary>
    static void LogAdbAction(string action, AndroidNode? node, AdbGrapInfo grap, string serial, bool success, string? detail)
    {
        var pkg = node?.Package ?? grap.Package;
        if (string.IsNullOrEmpty(pkg)) return;

        AdbExpDb.LogAction(pkg, new AdbActionLog
        {
            Action = action,
            Target = node?.SearchKey,
            Package = pkg,
            Device = serial,
            Success = success,
            Detail = detail,
            TapX = node?.CenterX,
            TapY = node?.CenterY,
        });
    }

    /// <summary>Broadcast A11Y/OS paths and knowhow files (mirrors Windows inspect pattern)</summary>
    static void BroadcastAdbPaths(string package, string? screenName)
    {
        var a11yDir = AdbExpDb.GetA11yDir(package);
        var osDir = AdbExpDb.GetOsDir(package);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("  [A11Y] ");
        Console.ResetColor();
        Console.WriteLine(Path.GetRelativePath(DataDir, a11yDir));

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("  [OS]   ");
        Console.ResetColor();
        Console.WriteLine(Path.GetRelativePath(DataDir, osDir));

        // Broadcast knowhow files — reuse Windows ShowKnowhowBroadcast (title + first paragraph)
        var knowhows = AdbExpDb.GetKnowhowFiles(package, screenName);
        foreach (var (path, tag) in knowhows)
            ShowKnowhowBroadcast(path, tag);
    }
}
