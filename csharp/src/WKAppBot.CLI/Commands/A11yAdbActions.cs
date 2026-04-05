using WKAppBot.Abstractions;
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
    private static readonly Lazy<AdbExperienceDb> _adbExpDb = new(() => new AdbExperienceDb(DataDir!));
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

        // ── AAR: device-level readiness ──────────────────
        var adbAar = new AdbActionReadiness(adb);
        // Quick global check: device awake? (skip for discovery/window actions)
        if (action is not ("windows" or "inspect" or "find" or "screenshot" or "ocr"
            or "read" or "highlight"))
        {
            // Use a dummy target for global-only check — Stage 0 only needs serial
            var globalCtx = new ReadinessContext { Serial = serial };
            // Create a minimal target from current activity for global check
            var globalResult = adbAar.Ensure(action,
                new AdbDummyTarget(), globalCtx);
            if (globalResult == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ADB] AAR: device not ready (screen off?)");
                Console.ResetColor();
                return 1;
            }
        }

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
            "move"       => AdbMove(adb, serial, grap, args),
            "resize"     => AdbResize(adb, serial, grap, args),
            // Element
            "click"      => AdbClick(adb, serial, grap),
            "invoke"     => AdbClick(adb, serial, grap), // invoke = click alias
            "read"       => AdbRead(adb, serial, grap, args),
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

        // Hot focus chain (depth-irrelevant — orthogonal axis)
        var focusChain = AndroidA11yTree.GetFocusChain(target);
        if (!string.IsNullOrEmpty(focusChain))
            Console.Write(focusChain);

        // Dump tree (hot focus nodes rendered regardless of depth limit)
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

}
