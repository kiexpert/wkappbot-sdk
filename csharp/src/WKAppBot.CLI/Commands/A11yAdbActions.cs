using WKAppBot.Android;

namespace WKAppBot.CLI;

/// <summary>
/// Android ADB dispatch for a11y commands.
/// Handles adb:// grap patterns — routes to ADB instead of Win32/UIA.
/// </summary>
internal partial class Program
{
    private static readonly string HqPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        "SDK", "bin", "wkappbot.hq");

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
        var registry = new AdbDeviceRegistry(adb, HqPath);

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
            "windows"    => AdbWindows(adb, registry),
            "inspect"    => AdbInspect(adb, serial, grap, args),
            "find"       => AdbInspect(adb, serial, grap, args), // find = inspect for Android
            "screenshot" => AdbScreenshot(adb, serial, displayId, args),
            "click"      => AdbClick(adb, serial, grap),
            "read"       => AdbRead(adb, serial, grap),
            "scroll"     => AdbScroll(adb, serial, grap, args),
            "type"       => AdbType(adb, serial, grap, args),
            "close"      => AdbClose(adb, serial, grap),
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
        Console.Write(AndroidA11yTree.DumpTree(target, depth));
        return 0;
    }

    // ── Screenshot ────────────────────────────────────────

    static int AdbScreenshot(AdbClient adb, string serial, string? displayId, string[] args)
    {
        var output = Path.Combine(HqPath, "output", "android_screen.png");
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

    // ── Click ─────────────────────────────────────────────

    static int AdbClick(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;

        Console.Write($"[ADB] Tap ({node.CenterX},{node.CenterY}) {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        if (r.IsOk)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK");
            Console.ResetColor();
            return 0;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"FAILED: {r.StdErr}");
        Console.ResetColor();
        return 1;
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
        Console.ForegroundColor = r.IsOk ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(r.IsOk ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        return r.IsOk ? 0 : 1;
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

        // Try ADB Keyboard IME broadcast first (supports Unicode/Korean)
        Console.Write($"[ADB] Input text \"{text}\"... ");
        var r = adb.BroadcastText(text, serial);
        if (r.IsOk && r.StdOut.Contains("result=0"))
        {
            // Broadcast succeeded — ADB Keyboard IME is active
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK (ADB Keyboard)");
            Console.ResetColor();
            return 0;
        }

        // Fallback: clipboard paste
        Console.Write("(ADB Keyboard not available, trying clipboard paste)... ");
        r = adb.ClipboardPaste(text, serial);
        if (r.IsOk)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK (clipboard paste)");
            Console.ResetColor();
            return 0;
        }

        // Last resort: ASCII-only input text
        if (text.All(c => c < 128))
        {
            r = adb.InputText(text, serial);
            if (r.IsOk)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK (ASCII input)");
                Console.ResetColor();
                return 0;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"FAILED: {r.StdErr}");
        Console.ResetColor();
        return 1;
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
        Console.ForegroundColor = r.IsOk ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(r.IsOk ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        return r.IsOk ? 0 : 1;
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

    static int AdbUnsupported(string action)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ADB] Action '{action}' not yet supported for Android");
        Console.WriteLine("[ADB] Supported: inspect, find, windows, screenshot, click, read, scroll, type, close");
        Console.ResetColor();
        return 1;
    }
}
