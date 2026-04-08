// A11yAdbActions.Helpers.cs -- ADB target resolution, input, diagnostics, experience DB
// Split from A11yAdbActions.cs for maintainability

using WKAppBot.Abstractions;
using WKAppBot.Android;

namespace WKAppBot.CLI;

internal partial class Program
{
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ADB] Scope '{grap.ScopePath}' not found in UIA tree — trying OCR fallback...");
                Console.ResetColor();

                // Show available targets for debugging
                var candidates = tree.FindAllInScope(target, grap.Scopes[0]);
                if (candidates.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[ADB] Similar UIA matches:");
                    foreach (var c in candidates.Take(5))
                        Console.WriteLine($"  {c.SearchKey}");
                    Console.ResetColor();
                }

                // ── OCR Fallback: screenshot → OCR → find text match → synthetic node ──
                var ocrNode = TryOcrFallback(adb, serial, grap.Scopes[^1]);
                if (ocrNode != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[ADB] OCR fallback hit: \"{ocrNode.DisplayName}\" at ({ocrNode.CenterX},{ocrNode.CenterY})");
                    Console.ResetColor();
                    return ocrNode;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ADB] OCR fallback also failed for '{grap.ScopePath}'");
                Console.ResetColor();

                // Auto bug report
                AutoBugReport($"ADB element not found: {grap.ScopePath} (UIA + OCR both failed)");
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
        DumpFailureDiagnostics(adb, serial);
        LogAdbAction(actionName, node, grap, serial, false, r.StdErr);
        return 1;
    }

    // ── Pre-input focus verification ──────────────────

    /// <summary>
    /// Verify the target node has keyboard focus using the already-dumped tree (no re-dump).
    /// Walks Parent chain to root, then searches for focused node.
    /// If focused node ≠ target → dump hot focus chain + IME as warning, return false.
    /// </summary>
    static bool VerifyInputFocus(AdbClient adb, string serial, AndroidNode targetNode)
    {
        try
        {
            // Walk up to root from target (tree already dumped by ResolveAdbTarget)
            var root = targetNode;
            while (root.Parent != null) root = root.Parent;

            var focused = FindFocusedNode(root);
            if (focused == null) return true; // no focus info → proceed

            // Match by resource-id or content-desc or text
            bool matches = false;
            if (!string.IsNullOrEmpty(targetNode.ResourceId) && targetNode.ResourceId == focused.ResourceId)
                matches = true;
            else if (!string.IsNullOrEmpty(targetNode.ContentDesc) && targetNode.ContentDesc == focused.ContentDesc)
                matches = true;
            else if (!string.IsNullOrEmpty(targetNode.Text) && targetNode.Text == focused.Text)
                matches = true;
            else if (targetNode.ClassName == focused.ClassName
                     && targetNode.BoundsLeft == focused.BoundsLeft && targetNode.BoundsTop == focused.BoundsTop)
                matches = true; // same class + same position

            if (!matches)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var targetLabel = !string.IsNullOrEmpty(targetNode.ShortResourceId) ? targetNode.ShortResourceId : targetNode.DisplayName;
                var focusLabel = !string.IsNullOrEmpty(focused.ShortResourceId) ? focused.ShortResourceId : focused.DisplayName;
                var msg = $"focus mismatch — target: {targetLabel}, focused: {focusLabel}";
                Console.WriteLine($"[ADB] ⚠ {msg}");
                Console.ResetColor();
                DumpFailureDiagnostics(adb, serial);

                // Device notification + TTS speak (best effort, background)
                try
                {
                    adb.Shell($"cmd notification post -t WKAppBot FOCUS_WARN '⚠ {targetLabel} ≠ {focusLabel}'", serial);
                    AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "wkappbot",
                        Arguments = $"speak \"포커스 불일치: {targetLabel}\" --bg",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }, Environment.CurrentDirectory, "ADB");
                }
                catch { /* best effort */ }

                return false;
            }
        }
        catch { /* best effort */ }
        return true;
    }

    private static AndroidNode? FindFocusedNode(AndroidNode node)
    {
        if (node.Focused) return node;
        foreach (var child in node.Children)
        {
            var found = FindFocusedNode(child);
            if (found != null) return found;
        }
        return null;
    }

    // ── Failure diagnostics (hot focus + IME) ─────────

    /// <summary>
    /// On action failure, dump hot focus chain + IME status for instant diagnosis.
    /// Shows: focused element chain + IME visibility (HoneyBoard, SwiftKey, etc.)
    /// </summary>
    static void DumpFailureDiagnostics(AdbClient adb, string serial)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[ADB] ── failure diagnostics ──");
        Console.ResetColor();

        // 1. Hot focus chain from fresh tree dump
        try
        {
            var tree = new AndroidA11yTree(adb);
            var root = tree.GetRoot(serial, forceRefresh: true);
            if (root != null)
            {
                var focusChain = AndroidA11yTree.GetFocusChain(root);
                if (!string.IsNullOrEmpty(focusChain))
                    Console.Write(focusChain);
                else
                    Console.WriteLine("  (no focused element detected)");
            }
        }
        catch { /* best effort */ }

        // 2. IME status
        try
        {
            var r = adb.Shell("dumpsys input_method | grep -E 'mCurId|mInputShown|mWindowVisible'", serial, 3000);
            if (r.IsOk)
            {
                var lines = r.StdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                string? imeId = null;
                bool imeShown = false;
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (trimmed.StartsWith("mCurId="))
                    {
                        // mCurId=com.samsung.android.honeyboard/.service.HoneyBoardService → "honeyboard"
                        var val = trimmed["mCurId=".Length..];
                        var slash = val.IndexOf('/');
                        var pkg = slash >= 0 ? val[..slash] : val;
                        var dot = pkg.LastIndexOf('.');
                        imeId = dot >= 0 ? pkg[(dot + 1)..] : pkg;
                    }
                    else if (trimmed.Contains("mInputShown=true"))
                        imeShown = true;
                }

                if (imeId != null)
                {
                    var icon = imeShown ? "⚡" : "○";
                    var state = imeShown ? "visible" : "hidden";
                    Console.ForegroundColor = imeShown ? ConsoleColor.Magenta : ConsoleColor.DarkGray;
                    Console.WriteLine($"  {icon} [IME] {imeId} ({state})");
                    Console.ResetColor();
                }
            }
        }
        catch { /* best effort */ }
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

    /// <summary>Minimal IActionTarget for device-level AAR global check (no real element).</summary>
    private sealed class AdbDummyTarget : IActionTarget
    {
        public string DisplayName => "(device)";
        public string? Identifier => null;
        public string? ClassName => null;
        public string BackendType => "ADB";
        public object? NativeHandle => null;
        public (int Left, int Top, int Right, int Bottom) BoundingRect => (0, 0, 1, 1);
        public bool Focused => false;
        public bool Enabled => true;
        public bool Visible => true;
        public bool IsOffscreen => false;
        public bool IsWindow => false;
        public string? WindowState => null;
        public IActionTarget? Parent => null;
        public IReadOnlyList<IActionTarget> Children => [];
    }

    // ── ADB OCR Fallback ──────────────────────────────────────────────────
    /// <summary>
    /// OCR fallback for ADB element not found: screenshot → Windows OCR → text match → synthetic node.
    /// Returns an AndroidNode with approximate bounds, or null if text not found.
    /// </summary>
    static AndroidNode? TryOcrFallback(AdbClient adb, string serial, string searchText)
    {
        try
        {
            // 1. Capture screenshot via ADB
            var tmpPng = Path.Combine(Path.GetTempPath(), $"adb_ocr_{DateTime.Now:HHmmss}.png");
            adb.Shell($"screencap -p /sdcard/_wk_ocr.png", serial, timeoutMs: 5000);
            adb.Run($"pull /sdcard/_wk_ocr.png \"{tmpPng}\"", serial, timeoutMs: 5000);
            adb.Shell("rm /sdcard/_wk_ocr.png", serial);

            if (!File.Exists(tmpPng) || new FileInfo(tmpPng).Length < 1000)
                return null;

            // 2. Run wkappbot OCR on the screenshot (reuses existing OcrCommand)
            var ocrOutput = "";
            try
            {
                var prevOut = Console.Out;
                var capture = new StringWriter();
                Console.SetOut(capture);
                OcrCommand([tmpPng]);
                Console.SetOut(prevOut);
                ocrOutput = capture.ToString();
            }
            catch { }
            try { File.Delete(tmpPng); } catch { }

            if (string.IsNullOrWhiteSpace(ocrOutput))
                return null;

            // 3. Find text match in OCR output lines
            var pattern = searchText.Replace("*", "").Trim();
            var lines = ocrOutput.Split('\n');
            foreach (var line in lines)
            {
                if (!line.Contains(pattern, StringComparison.OrdinalIgnoreCase)) continue;

                // Try to extract bounds from OCR output: "text @X,Y WxH" or similar
                // Fallback: use screen center if no bounds
                // For now, search uiautomator dump for matching content-desc/text
                var dumpResult = adb.Shell("uiautomator dump /dev/tty", serial, timeoutMs: 5000);
                if (dumpResult.IsOk)
                {
                    var dump = dumpResult.StdOut;
                    var patIdx = dump.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
                    if (patIdx >= 0)
                    {
                        // Find bounds near the match
                        var boundsIdx = dump.IndexOf("bounds=\"[", patIdx);
                        if (boundsIdx < 0) boundsIdx = dump.LastIndexOf("bounds=\"[", patIdx);
                        if (boundsIdx >= 0)
                        {
                            var bStart = boundsIdx + "bounds=\"".Length;
                            var bEnd = dump.IndexOf('"', bStart);
                            if (bEnd > bStart)
                            {
                                var boundsStr = dump[bStart..bEnd]; // [x1,y1][x2,y2]
                                var nums = System.Text.RegularExpressions.Regex.Matches(boundsStr, @"\d+");
                                if (nums.Count >= 4)
                                {
                                    int x1 = int.Parse(nums[0].Value), y1 = int.Parse(nums[1].Value);
                                    int x2 = int.Parse(nums[2].Value), y2 = int.Parse(nums[3].Value);
                                    int cx = (x1 + x2) / 2, cy = (y1 + y2) / 2;

                                    Console.WriteLine($"[ADB-OCR] Found \"{pattern}\" at ({cx},{cy}) via OCR+dump hybrid");
                                    return new AndroidNode
                                    {
                                        ClassName = "ocr-fallback", Text = pattern,
                                        ContentDesc = $"[OCR] {pattern}", Package = "ocr",
                                        BoundsLeft = x1, BoundsTop = y1, BoundsRight = x2, BoundsBottom = y2,
                                        Clickable = true, Enabled = true,
                                    };
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"[ADB-OCR] OCR matched \"{pattern}\" but couldn't resolve bounds");
                return null;
            }

            Console.WriteLine($"[ADB-OCR] Text \"{pattern}\" not found in OCR output");
            return null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ADB-OCR] Fallback error: {ex.Message}");
            return null;
        }
    }
}
