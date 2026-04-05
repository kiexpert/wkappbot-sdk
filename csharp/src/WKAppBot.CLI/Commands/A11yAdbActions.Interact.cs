// A11yAdbActions.Interact.cs -- ADB element interaction actions
// Split from A11yAdbActions.cs for maintainability

using WKAppBot.Android;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Click ─────────────────────────────────────────────

    static int AdbClick(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;
        VerifyInputFocus(adb, serial, node); // warn only, proceed anyway

        Console.Write($"[ADB] Tap ({node.CenterX},{node.CenterY}) {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        if (!ok) DumpFailureDiagnostics(adb, serial);

        LogAdbAction("click", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Read ──────────────────────────────────────────────

    static int AdbRead(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
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

        // --speak: TTS 카라오케로 텍스트 읽어주기
        if (args.Contains("--speak"))
        {
            var speakText = !string.IsNullOrEmpty(node.Text) ? node.Text
                : !string.IsNullOrEmpty(node.ContentDesc) ? node.ContentDesc
                : node.DisplayName;
            if (!string.IsNullOrWhiteSpace(speakText))
            {
                try
                {
                    AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "wkappbot",
                        Arguments = $"speak \"{speakText.Replace("\"", "'")}\" --bg",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }, Environment.CurrentDirectory, "ADB");
                }
                catch { /* best effort */ }
            }
        }

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
        VerifyInputFocus(adb, serial, node);

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
        VerifyInputFocus(adb, serial, node);

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
        VerifyInputFocus(adb, serial, node);

        Console.Write($"[ADB] Select {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        if (!ok) DumpFailureDiagnostics(adb, serial);

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
        VerifyInputFocus(adb, serial, node);

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
        if (!ok) DumpFailureDiagnostics(adb, serial);

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
        // Positional fallback: first non-flag arg
        if (text == null)
            for (int i = 0; i < args.Length; i++)
                if (!args[i].StartsWith("--")) { text = args[i]; break; }

        if (text == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] text required for type action (e.g., a11y type \"adb://*#target\" \"hello\")");
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

        // Verify focus landed on target
        if (!VerifyInputFocus(adb, serial, node)) return 1;

        return AdbInputText(adb, serial, text, node, grap, "type");
    }

    // ── Set-Value (clear existing + type new text) ────────

    static int AdbSetValue(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        string? text = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--text" && i + 1 < args.Length)
                text = args[i + 1];
        // Positional fallback
        if (text == null)
            for (int i = 0; i < args.Length; i++)
                if (!args[i].StartsWith("--")) { text = args[i]; break; }

        if (text == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] text required for set-value action (e.g., a11y set-value \"adb://*#input\" \"value\")");
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

        // Verify focus landed on target
        if (!VerifyInputFocus(adb, serial, node)) return 1;

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
        VerifyInputFocus(adb, serial, node);

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
        if (!ok) DumpFailureDiagnostics(adb, serial);

        LogAdbAction("set-range", node, grap, serial, ok, $"ratio={ratio:F2}");
        return ok ? 0 : 1;
    }

    // ── Focus (tap element center) ────────────────────────

    static int AdbFocus(AdbClient adb, string serial, AdbGrapInfo grap)
    {
        var node = ResolveAdbTarget(adb, serial, grap);
        if (node == null) return 1;
        VerifyInputFocus(adb, serial, node);

        Console.Write($"[ADB] Focus tap ({node.CenterX},{node.CenterY}) {node.DisplayName}... ");
        var r = adb.Tap(node.CenterX, node.CenterY, serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        if (!ok) DumpFailureDiagnostics(adb, serial);

        LogAdbAction("focus", node, grap, serial, ok, ok ? null : r.StdErr);
        return ok ? 0 : 1;
    }

    // ── Wait (poll until element appears) ─────────────────

    static int AdbWait(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        var timeoutMs = 10000;
        var intervalMs = 1000;
        string? condition = null;
        bool waitNot = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out timeoutMs);
            if (args[i] == "--interval" && i + 1 < args.Length)
                int.TryParse(args[i + 1], out intervalMs);
            if (args[i] == "--condition" && i + 1 < args.Length)
                condition = args[i + 1];
            if (args[i] == "--not")
                waitNot = true;
        }

        if (grap.Scopes.Length == 0 && grap.Package == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] wait requires a target (package and/or #scope)");
            Console.ResetColor();
            return 1;
        }

        var modeStr = waitNot ? "disappear" : "appear";
        var condStr = condition != null ? $", condition=\"{condition}\"" : "";
        Console.Write($"[ADB] Waiting for '{grap.ScopePath ?? grap.Package}' to {modeStr} (timeout={timeoutMs}ms{condStr})... ");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var tree = new AndroidA11yTree(adb);

        while (sw.ElapsedMilliseconds < timeoutMs)
        {
            bool found = false;
            string foundInfo = "";

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
                        // Condition check: text/content-desc must contain condition string
                        if (condition != null)
                        {
                            var text = !string.IsNullOrEmpty(scoped.Text) ? scoped.Text : scoped.ContentDesc;
                            if (!text.Contains(condition, StringComparison.OrdinalIgnoreCase))
                            { Thread.Sleep(intervalMs); continue; }
                        }
                        found = true;
                        foundInfo = scoped.SearchKey;
                    }
                }
                else
                {
                    found = true;
                    foundInfo = target.Package;
                }
            }

            if (waitNot)
            {
                if (!found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"GONE ({sw.ElapsedMilliseconds}ms)");
                    Console.ResetColor();
                    return 0;
                }
            }
            else
            {
                if (found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"FOUND ({sw.ElapsedMilliseconds}ms) {foundInfo}");
                    Console.ResetColor();
                    return 0;
                }
            }

            Thread.Sleep(intervalMs);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"TIMEOUT ({timeoutMs}ms) ({modeStr})");
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
        // Positional fallback
        if (cmd == null)
            for (int i = 0; i < args.Length; i++)
                if (!args[i].StartsWith("--")) { cmd = args[i]; break; }

        if (cmd == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] text required for eval action (e.g., a11y eval \"adb://\" \"getprop ro.product.model\")");
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
        VerifyInputFocus(adb, serial, node); // warn only — coordinate action

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
        if (!ok) DumpFailureDiagnostics(adb, serial);

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
        if (!ok) DumpFailureDiagnostics(adb, serial);

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
        if (!ok) DumpFailureDiagnostics(adb, serial);
        return ok ? 0 : 1;
    }

    // ── Move / Resize (DeX freeform via am task resize) ──

    /// <summary>Find task ID for package via dumpsys activity recents</summary>
    static int? FindTaskId(AdbClient adb, string serial, string packagePattern)
    {
        var r = adb.Shell("dumpsys activity recents", serial, 5000);
        if (!r.IsOk) return null;

        var matcher = GrapMatcher.Create(packagePattern);
        foreach (var line in r.StdOut.Split('\n'))
        {
            // * Recent #0: Task{xxx #24927 type=standard A=10254:com.android.chrome}
            var trimmed = line.Trim();
            if (!trimmed.StartsWith("* Recent") && !trimmed.StartsWith("Recent")) continue;

            // Task ID is the second # in the line: "Recent #0: Task{xxx #24925 type=..."
            var firstHash = trimmed.IndexOf('#');
            if (firstHash < 0) continue;
            var secondHash = trimmed.IndexOf('#', firstHash + 1);
            if (secondHash < 0) continue;
            var spaceAfter = trimmed.IndexOf(' ', secondHash);
            if (spaceAfter < 0) continue;
            if (!int.TryParse(trimmed[(secondHash + 1)..spaceAfter], out var taskId)) continue;

            // Check if package matches (after A= or in the line)
            var aIdx = trimmed.IndexOf("A=");
            if (aIdx >= 0)
            {
                var pkg = trimmed[(aIdx + 2)..].TrimEnd('}').Trim();
                // A=10254:com.android.chrome → extract package after ':'
                var colonIdx = pkg.IndexOf(':');
                if (colonIdx >= 0) pkg = pkg[(colonIdx + 1)..];
                if (matcher.IsMatch(pkg)) return taskId;
            }
        }
        return null;
    }

    /// <summary>Get current window bounds from dumpsys window (fast, no uiautomator dump)</summary>
    static (int left, int top, int right, int bottom)? GetCurrentBounds(AdbClient adb, string serial, string? packagePattern)
    {
        if (string.IsNullOrEmpty(packagePattern)) return null;

        var matcher = GrapMatcher.Create(packagePattern);
        var r = adb.Shell("dumpsys window windows", serial, 5000);
        if (!r.IsOk) return null;

        bool inMatchingWindow = false;
        foreach (var line in r.StdOut.Split('\n'))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("Window #") && trimmed.Contains("Window{"))
            {
                inMatchingWindow = false;
                var pkgStart = trimmed.IndexOf("u0 ");
                if (pkgStart >= 0)
                {
                    var pkgStr = trimmed[(pkgStart + 3)..].TrimEnd(':', '}');
                    var slashIdx = pkgStr.IndexOf('/');
                    if (slashIdx >= 0) pkgStr = pkgStr[..slashIdx];
                    if (matcher.IsMatch(pkgStr)) inMatchingWindow = true;
                }
            }
            if (inMatchingWindow && trimmed.Contains("mBounds=Rect("))
            {
                var match = System.Text.RegularExpressions.Regex.Match(trimmed,
                    @"mBounds=Rect\((\d+),\s*(\d+)\s*-\s*(\d+),\s*(\d+)\)");
                if (match.Success)
                    return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                            int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            }
        }
        return null;
    }

    static int AdbMove(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        int? x = null, y = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--x" && i + 1 < args.Length && int.TryParse(args[i + 1], out var vx)) x = vx;
            if (args[i] == "--y" && i + 1 < args.Length && int.TryParse(args[i + 1], out var vy)) y = vy;
        }

        if (x == null && y == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --x and/or --y required for move action");
            Console.ResetColor();
            return 1;
        }

        var pkg = grap.Package ?? "*";
        var taskId = FindTaskId(adb, serial, pkg);
        if (taskId == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ADB] Task not found for package '{pkg}'");
            Console.ResetColor();
            return 1;
        }

        var bounds = GetCurrentBounds(adb, serial, grap.Package);
        if (bounds == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] Cannot determine current window bounds");
            Console.ResetColor();
            return 1;
        }

        var (cl, ct, cr, cb) = bounds.Value;
        var w = cr - cl;
        var h = cb - ct;
        var newL = x ?? cl;
        var newT = y ?? ct;

        Console.Write($"[ADB] Move task {taskId} → ({newL},{newT}) {w}x{h}... ");
        var r = adb.Shell($"am task resize {taskId} {newL} {newT} {newL + w} {newT + h}", serial);
        var ok = r.IsOk;
        Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(ok ? "OK" : $"FAILED: {r.StdErr}");
        Console.ResetColor();
        return ok ? 0 : 1;
    }

    static int AdbResize(AdbClient adb, string serial, AdbGrapInfo grap, string[] args)
    {
        int? w = null, h = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--w" && i + 1 < args.Length && int.TryParse(args[i + 1], out var vw)) w = vw;
            if (args[i] == "--h" && i + 1 < args.Length && int.TryParse(args[i + 1], out var vh)) h = vh;
        }

        if (w == null && h == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] --w and/or --h required for resize action");
            Console.ResetColor();
            return 1;
        }

        var pkg = grap.Package ?? "*";
        var taskId = FindTaskId(adb, serial, pkg);
        if (taskId == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ADB] Task not found for package '{pkg}'");
            Console.ResetColor();
            return 1;
        }

        var bounds = GetCurrentBounds(adb, serial, grap.Package);
        if (bounds == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ADB] Cannot determine current window bounds");
            Console.ResetColor();
            return 1;
        }

        var (cl, ct, cr, cb) = bounds.Value;
        var newW = w ?? (cr - cl);
        var newH = h ?? (cb - ct);

        Console.Write($"[ADB] Resize task {taskId} → {newW}x{newH} at ({cl},{ct})... ");
        var r = adb.Shell($"am task resize {taskId} {cl} {ct} {cl + newW} {ct + newH}", serial);
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
}
