using System.Diagnostics;
using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: dismiss, dialog-click, toolbar-ocr commands + TryHandleBlocker + ExecuteClickButton
internal partial class Program
{
    /// <summary>
    /// Check if a blocker dialog appeared and try to handle it automatically.
    /// Uses DialogHandlerManager to match and execute handlers from handlers/*.yaml.
    /// If no handler matches, generates a sample YAML template for the user to edit.
    /// Returns: (handled, shouldRetry) -- handled=true if blocker was processed, shouldRetry from handler params.
    /// </summary>
    private static (bool handled, bool shouldRetry) TryHandleBlocker(
        IntPtr expectedFgWindow, DialogHandlerManager? handlerMgr)
    {
        // Strategy 1: Check foreground window
        var fg = NativeMethods.GetForegroundWindow();
        IntPtr blockerHwnd = IntPtr.Zero;

        if (fg != expectedFgWindow && fg != IntPtr.Zero)
        {
            NativeMethods.GetWindowThreadProcessId(fg, out uint fgPid);
            NativeMethods.GetWindowThreadProcessId(expectedFgWindow, out uint targetPid1);
            uint myPid = (uint)Environment.ProcessId;

            // Only consider foreground if it's from the target process (not us, not other apps)
            if (fgPid != myPid && fgPid == targetPid1)
                blockerHwnd = fg;
        }

        // Strategy 2: EnumWindows scan -- find popup/dialog owned by same process
        // This catches modal dialogs that overlay the main window but aren't foreground
        if (blockerHwnd == IntPtr.Zero)
        {
            NativeMethods.GetWindowThreadProcessId(expectedFgWindow, out uint targetPid2);
            var candidates = new List<IntPtr>();

            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (hWnd == expectedFgWindow) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != targetPid2) return true;

                // Check if it's a dialog-like window (#32770 or small popup)
                var clsSb = new StringBuilder(256);
                NativeMethods.GetClassNameW(hWnd, clsSb, 256);
                var cls = clsSb.ToString();

                NativeMethods.GetWindowRect(hWnd, out var r);
                bool isDialog = cls == "#32770";
                bool isPopup = r.Width > 50 && r.Height > 30 && r.Width < 800 && r.Height < 600;

                if (isDialog || isPopup)
                    candidates.Add(hWnd);

                return true;
            }, IntPtr.Zero);

            // Pick the topmost (first in Z-order from EnumWindows) dialog
            if (candidates.Count > 0)
                blockerHwnd = candidates[0];
        }

        if (blockerHwnd == IntPtr.Zero)
            return (false, false);

        // Get blocker window info
        var titleSb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(blockerHwnd, titleSb, 256);
        var windowTitle = titleSb.ToString();

        var classSb = new StringBuilder(256);
        NativeMethods.GetClassNameW(blockerHwnd, classSb, 256);
        var className = classSb.ToString();

        // Build class hierarchy path: walk GetParent chain up to desktop
        var classPath = BuildClassPath(blockerHwnd);

        NativeMethods.GetWindowThreadProcessId(blockerHwnd, out uint blockerPid);
        string processName = "";
        try { processName = Process.GetProcessById((int)blockerPid).ProcessName; } catch { }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n[BLOCK] Blocker detected: [{classPath}] \"{windowTitle}\" (process: {processName})");
        Console.ResetColor();

        // Read dialog contents for context and handler matching
        var messageText = GetDialogMessageText(blockerHwnd);
        ReadDialogContents(blockerHwnd);

        // Try to find a matching handler
        // Resolve handlers directory
        var resolvedHandlersDir = handlerMgr?.HandlersDir
            ?? Path.Combine(DataDir, "handlers");

        if (handlerMgr == null || handlerMgr.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[BLOCK] No handlers loaded.");
            Console.ResetColor();

            // Interactive learning: ask user to hover over desired button
            return InteractiveButtonLearn(
                blockerHwnd, windowTitle, classPath, className, processName, resolvedHandlersDir);
        }

        var handler = handlerMgr.FindHandler(windowTitle, classPath, className, processName, messageText);
        if (handler == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[BLOCK] No matching handler for [{classPath}] \"{windowTitle}\".");
            Console.ResetColor();

            // Interactive learning: ask user to hover over desired button
            return InteractiveButtonLearn(
                blockerHwnd, windowTitle, classPath, className, processName, resolvedHandlersDir);
        }

        // Execute handler action
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[BLOCK] Handler matched -> action: {handler.Action}");
        Console.ResetColor();

        bool success = false;
        switch (handler.Action.ToLowerInvariant())
        {
            case "click_button":
                success = ExecuteClickButton(blockerHwnd, handler.Params);
                break;

            case "dismiss":
                // Send ESC to close the dialog
                WKAppBot.Win32.Input.InputReadiness.ReadinessCalled = true; // blocker dialog handler -- automation-invoked
                NativeMethods.SmartSetForegroundWindow(blockerHwnd); // [FOCUS-GUARD] CheckActiveGuard 적용
                Thread.Sleep(100);
                KeyboardInput.PressKey("escape");
                Thread.Sleep(300);
                success = !NativeMethods.IsWindow(blockerHwnd) || !NativeMethods.IsWindowVisible(blockerHwnd);
                if (!success)
                {
                    // Fallback: Alt+F4
                    KeyboardInput.Hotkey(new[] { "alt", "f4" });
                    Thread.Sleep(300);
                    success = !NativeMethods.IsWindow(blockerHwnd) || !NativeMethods.IsWindowVisible(blockerHwnd);
                }
                Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(success ? " v dismissed" : " X dismiss failed");
                Console.ResetColor();
                break;

            case "report":
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" (report only -- no auto-action)");
                Console.ResetColor();
                return (false, false);
        }

        if (success && handler.Params != null)
        {
            // Active patrol loop: handle blockers while waiting for condition
            // Replaces passive Thread.Sleep -- "핸들러 개입환영" (welcomes handler intervention)
            var waitUntil = handler.Params.WaitUntil;
            int patrolMs = handler.Params.WaitAfter > 0
                ? handler.Params.WaitAfter
                : (waitUntil != null ? waitUntil.Timeout * 1000 : 0);

            if (waitUntil != null)
                patrolMs = Math.Max(patrolMs, waitUntil.Timeout * 1000);

            if (patrolMs > 0)
            {
                PatrolWaitLoop(
                    expectedFgWindow, handlerMgr,
                    blockerHwnd, waitUntil, patrolMs);
            }

            return (true, handler.Params.Retry);
        }

        return (success, false);
    }

    /// <summary>
    /// Active patrol loop: checks condition + handles blockers while waiting.
    /// "핸들러 개입환영" -- instead of sleeping, we patrol for more blockers.
    /// Level 0: Thread.Sleep (멍) -> Level 1: Patrol (순찰) -> Level 2: Patrol+StableHash (눈뜨고감시)
    /// </summary>
    private static void PatrolWaitLoop(
        IntPtr expectedFgWindow, DialogHandlerManager? handlerMgr,
        IntPtr handledDialogHwnd, WaitUntilCondition? condition, int totalMs)
    {
        int pollInterval = condition?.StableFrames > 0
            ? Math.Max(condition.StableInterval, 200)
            : 500;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Determine what we're waiting for
        bool hasWindowCondition = condition?.WindowExists != null;
        bool hasDialogGoneCondition = condition?.DialogGone == true
            || (condition != null && !hasWindowCondition && condition.DialogGone != false);
        bool hasStableCondition = condition?.StableFrames > 0;
        bool hasResponsiveCondition = condition?.Responsive == true;
        int stableTarget = condition?.StableFrames ?? 0;

        // Build description
        var parts = new List<string>();
        if (hasWindowCondition) parts.Add($"window \"{condition!.WindowExists}\"");
        if (hasDialogGoneCondition) parts.Add("dialog gone");
        if (hasStableCondition) parts.Add($"stable×{stableTarget}");
        if (hasResponsiveCondition) parts.Add("responsive");
        if (parts.Count == 0) parts.Add($"{totalMs}ms patrol");
        string condDesc = string.Join(" + ", parts);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Error.WriteLine($"[BLOCK] Patrol wait: {condDesc} (max {totalMs / 1000.0:F1}s)");
        Console.ResetColor();

        int blockerCount = 0;
        long lastProgressSec = -1; // track last progress output second
        bool windowFound = false;
        bool responsiveConfirmed = false; // SendMessageTimeout responsive check

        // Frame stability tracking (Level 2)
        string? lastFrameHash = null;
        int consecutiveStable = 0;
        IntPtr stableTargetHwnd = IntPtr.Zero; // window to capture for stability check

        while (sw.ElapsedMilliseconds < totalMs)
        {
            Thread.Sleep(pollInterval);

            // -- Per-second progress output (overwrite same line) --
            long currentSec = sw.ElapsedMilliseconds / 1000;
            if (currentSec > lastProgressSec)
            {
                lastProgressSec = currentSec;
                var sb = new System.Text.StringBuilder();
                sb.Append($"\r[BLOCK] Patrol {currentSec}s/{totalMs / 1000}s");
                if (hasWindowCondition)
                    sb.Append(windowFound ? " window=v" : " window=...");
                if (hasStableCondition)
                    sb.Append($" stable={consecutiveStable}/{stableTarget}");
                if (hasResponsiveCondition)
                    sb.Append(responsiveConfirmed ? " responsive=v" : " responsive=...");
                if (blockerCount > 0)
                    sb.Append($" blockers={blockerCount}");
                sb.Append("   "); // clear trailing chars
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(sb.ToString());
                Console.ResetColor();
            }

            // -- Check condition: window exists? --
            if (hasWindowCondition)
            {
                var found = WindowFinder.FindWindows(condition!.WindowExists!);
                if (found.Count > 0)
                {
                    windowFound = true;
                    stableTargetHwnd = found[0].Handle; // use this for stability + responsive check

                    if (!hasStableCondition && !hasResponsiveCondition)
                    {
                        Console.WriteLine(); // newline after \r progress
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Error.WriteLine($"[BLOCK] Patrol done: window found \"{found[0].Title}\" ({sw.ElapsedMilliseconds}ms, {blockerCount} blockers handled)");
                        Console.ResetColor();
                        return;
                    }
                    // If stable/responsive check is also required, continue below
                }
            }

            // -- Check condition: original dialog gone? --
            if (hasDialogGoneCondition && handledDialogHwnd != IntPtr.Zero)
            {
                bool gone = !NativeMethods.IsWindow(handledDialogHwnd)
                    || !NativeMethods.IsWindowVisible(handledDialogHwnd);
                if (gone && !hasWindowCondition && !hasStableCondition)
                {
                    Console.WriteLine(); // newline after \r progress
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Error.WriteLine($"[BLOCK] Patrol done: dialog gone ({sw.ElapsedMilliseconds}ms, {blockerCount} blockers handled)");
                    Console.ResetColor();
                    return;
                }
            }

            // -- Check condition: frame stability (Level 2) --
            if (hasStableCondition && stableTargetHwnd != IntPtr.Zero)
            {
                string? frameHash = CaptureFrameHash(stableTargetHwnd);
                if (frameHash != null)
                {
                    if (frameHash == lastFrameHash)
                    {
                        consecutiveStable++;
                        if (consecutiveStable >= stableTarget)
                        {
                            Console.WriteLine(); // newline after \r progress
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Error.WriteLine($"[BLOCK] Patrol done: stable ×{consecutiveStable} ({sw.ElapsedMilliseconds}ms, {blockerCount} blockers handled)");
                            Console.ResetColor();
                            return;
                        }
                    }
                    else
                    {
                        consecutiveStable = 1; // new frame = reset, but count this one
                        lastFrameHash = frameHash;
                    }
                }
            }
            else if (hasStableCondition && stableTargetHwnd == IntPtr.Zero)
            {
                // No target window yet for stability -- use expectedFgWindow as fallback
                if (expectedFgWindow != IntPtr.Zero && NativeMethods.IsWindow(expectedFgWindow))
                    stableTargetHwnd = expectedFgWindow;
            }

            // -- Check condition: responsive (SendMessageTimeout WM_NULL) --
            // "마지막 변화후 센드메시지 보내서 리턴될때까지 기다려봐도 되겠다"
            // SMTO_ABORTIFHUNG: returns 0 if the target message loop is hung (not pumping messages).
            // WM_NULL: no-op message -- zero side effects, pure liveness check.
            // This detects when HTS finishes heavy initialization and starts processing messages again.
            if (hasResponsiveCondition && !responsiveConfirmed)
            {
                IntPtr checkHwnd = stableTargetHwnd != IntPtr.Zero ? stableTargetHwnd : expectedFgWindow;
                if (checkHwnd != IntPtr.Zero && NativeMethods.IsWindow(checkHwnd))
                {
                    IntPtr result;
                    IntPtr sent = NativeMethods.SendMessageTimeoutW(
                        checkHwnd, NativeMethods.WM_NULL, IntPtr.Zero, IntPtr.Zero,
                        NativeMethods.SMTO_ABORTIFHUNG, 3000, out result);

                    if (sent != IntPtr.Zero)
                    {
                        // Message loop responded within timeout -- app is alive!
                        responsiveConfirmed = true;

                        // Check if this is the final remaining condition
                        bool stableDone = !hasStableCondition || consecutiveStable >= stableTarget;
                        bool windowDone = !hasWindowCondition || windowFound;
                        bool dialogDone = !hasDialogGoneCondition || (handledDialogHwnd != IntPtr.Zero &&
                            (!NativeMethods.IsWindow(handledDialogHwnd) || !NativeMethods.IsWindowVisible(handledDialogHwnd)));

                        if (windowDone && stableDone && dialogDone)
                        {
                            Console.WriteLine(); // newline after \r progress
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Error.WriteLine($"[BLOCK] Patrol done: responsive v ({sw.ElapsedMilliseconds}ms, {blockerCount} blockers handled)");
                            Console.ResetColor();
                            return;
                        }
                    }
                    // sent == 0 means hung/timeout -- keep waiting
                }
            }

            // -- Patrol: check for new blockers and handle them --
            var (handled, _) = TryHandleBlocker(expectedFgWindow, handlerMgr);
            if (handled)
            {
                blockerCount++;
                // Blocker handled = screen changed -> reset stability + responsive counters
                consecutiveStable = 0;
                lastFrameHash = null;
                responsiveConfirmed = false; // app may hang again after popup dismiss
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Error.WriteLine($"[BLOCK] Patrol: handled blocker #{blockerCount} (stable reset)");
                Console.ResetColor();
            }
        }

        Console.WriteLine(); // newline after \r progress
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        var extras = new List<string>();
        if (hasStableCondition) extras.Add($"stable={consecutiveStable}/{stableTarget}");
        if (hasResponsiveCondition) extras.Add($"responsive={responsiveConfirmed}");
        string extra = extras.Count > 0 ? ", " + string.Join(", ", extras) : "";
        Console.Error.WriteLine($"[BLOCK] Patrol timeout: {totalMs / 1000.0:F1}s elapsed, {blockerCount} blockers handled{extra}");
        Console.ResetColor();
    }

    /// <summary>
    /// Helper for dismiss Step 0: run patrol wait + write ActionState after handler success.
    /// </summary>
    private static void RunStep0Patrol(
        IntPtr dialogHwnd, string dialogTitle,
        DialogActionParams? prms, DialogHandlerManager? handlerMgr)
    {
        // Patrol wait loop (if configured)
        var waitUntil = prms?.WaitUntil;
        int patrolMs = prms?.WaitAfter > 0 ? prms.WaitAfter
            : (waitUntil != null ? waitUntil.Timeout * 1000 : 0);
        if (waitUntil != null)
            patrolMs = Math.Max(patrolMs, waitUntil.Timeout * 1000);

        if (patrolMs > 0)
        {
            // Use the process's main window as expectedFgWindow for blocker detection
            NativeMethods.GetWindowThreadProcessId(dialogHwnd, out uint pid);
            IntPtr expectedFg = IntPtr.Zero;
            try { expectedFg = Process.GetProcessById((int)pid).MainWindowHandle; } catch { }
            if (expectedFg == IntPtr.Zero) expectedFg = dialogHwnd;

            PatrolWaitLoop(expectedFg, handlerMgr, dialogHwnd, waitUntil, patrolMs);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  1 window(s) dismissed (handler: {prms?.ButtonText ?? "action"}).");
        Console.ResetColor();

        try
        {
            ActionState.Write(new ActionState
            {
                Source = "dismiss",
                WindowTitle = dialogTitle,
                ActionName = "dismiss",
                ActionDetail = $"Handler: click_button on \"{dialogTitle}\"" +
                    (patrolMs > 0 ? " + patrol" : ""),
                Status = "pass",
            });
        }
        catch { }
    }

    /// <summary>
    /// Capture a window via PrintWindow and return SHA256 hash of the pixel data.
    /// Used for frame stability detection -- consecutive identical hashes = stable content.
    /// Returns null on capture failure (best-effort, non-blocking).
    /// </summary>
    private static string? CaptureFrameHash(IntPtr hWnd)
    {
        try
        {
            using var bmp = ScreenCapture.CaptureWindow(hWnd, new WKAppBot.Win32.Input.CaptureOptions
            {
                RejectBlank = true,
                StepLogger = s => Console.Error.WriteLine(s),
            });
            if (bmp == null) return null;

            // Fast hash: lock bits and compute SHA256 over raw pixel data
            var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            try
            {
                int byteCount = Math.Abs(data.Stride) * data.Height;
                var pixels = new byte[byteCount];
                System.Runtime.InteropServices.Marshal.Copy(data.Scan0, pixels, 0, byteCount);
                var hash = System.Security.Cryptography.SHA256.HashData(pixels);
                return Convert.ToHexString(hash, 0, 8); // first 8 bytes = 16 hex chars (enough)
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }
        catch
        {
            return null; // best-effort
        }
    }

    /// <summary>
    /// Execute click_button action: find and click a button in the dialog.
    /// Supports button_index (positional) and button_text (text match).
    /// </summary>
    private static bool ExecuteClickButton(IntPtr hDialog, DialogActionParams? prms)
    {
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(hDialog, allChildren, 0, 3);

        // Button detection: standard Button class + small clickable MFC controls
        var buttons = allChildren.Where(c => c.IsVisible && c.Rect.Width > 10 && IsButtonLike(c))
            .OrderBy(b => b.Rect.Left).ToList();

        // Build text map: GetWindowText -> WM_GETTEXT -> OCR fallback for owner-drawn buttons
        var buttonTexts = new Dictionary<IntPtr, string>();
        bool needOcr = false;
        foreach (var btn in buttons)
        {
            var text = btn.Title;
            if (string.IsNullOrEmpty(text))
                text = GetWmGetText(btn.Handle);
            if (string.IsNullOrEmpty(text))
                needOcr = true;
            buttonTexts[btn.Handle] = text ?? "";
        }

        // OCR fallback: capture button regions and read text via SimpleOcr
        if (needOcr)
        {
            try
            {
                var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.3f);

                foreach (var btn in buttons)
                {
                    if (!string.IsNullOrEmpty(buttonTexts[btn.Handle])) continue;
                    try
                    {
                        // Capture button screen region
                        using var bmp = ScreenCapture.CaptureScreenRegion(
                            btn.Rect.Left, btn.Rect.Top, btn.Rect.Width, btn.Rect.Height);
                        var result = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                        var ocrText = string.Join("", result.Words.Select(w => w.Text)).Trim();
                        if (!string.IsNullOrEmpty(ocrText))
                            buttonTexts[btn.Handle] = ocrText;
                    }
                    catch { /* OCR per-button is best-effort */ }
                }
            }
            catch { /* OCR init failure is non-critical */ }
        }

        // Debug: show buttons found
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(" ->");
        if (buttons.Count == 0)
            Console.Write(" (no buttons)");
        else
            foreach (var b in buttons)
            {
                var t = buttonTexts[b.Handle];
                var display = !string.IsNullOrEmpty(t) ? $"\"{t}\"" : "(owner-drawn)";
                Console.Write($" [{b.ClassName}]{display}");
            }
        Console.WriteLine();
        Console.ResetColor();

        if (buttons.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" X no buttons found in dialog");
            Console.ResetColor();
            return false;
        }

        WindowInfo targetBtn;

        if (prms?.ButtonText != null)
        {
            // Match by text (GetWindowText + WM_GETTEXT)
            var textMatch = buttons.FirstOrDefault(b =>
                buttonTexts[b.Handle].Contains(prms.ButtonText, StringComparison.OrdinalIgnoreCase));

            if (textMatch == null && buttons.Count == 1)
            {
                // Single button fallback: if only 1 button exists, assume it's the one
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($" (single button fallback)");
                Console.ResetColor();
                textMatch = buttons[0];
            }

            if (textMatch == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" X button \"{prms.ButtonText}\" not found ({buttons.Count} buttons available)");
                Console.ResetColor();
                return false;
            }
            targetBtn = textMatch;
        }
        else
        {
            // Match by index (default: 0 = first)
            int idx = prms?.ButtonIndex ?? 0;
            if (idx >= buttons.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" X button index {idx} out of range ({buttons.Count} buttons)");
                Console.ResetColor();
                return false;
            }
            targetBtn = buttons[idx];
        }

        Console.ForegroundColor = ConsoleColor.Green;
        var targetBtnText = buttonTexts.GetValueOrDefault(targetBtn.Handle, targetBtn.Title ?? "");
        Console.Write($" -> clicking \"{targetBtnText}\"");
        Console.ResetColor();

        bool clicked = SmartClickButton(targetBtn.Handle, hDialog);
        if (!clicked)
        {
            // SmartClickButton logs its own failure
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.ResetColor();
        return true;
    }

    /// <summary>
    /// <summary>
    /// Dismiss MDI child windows matching keywords in their title.
    /// <summary>
    /// OCR-scan toolbar panes, show recognized text regions, and optionally click one.
    /// MFC apps expose toolbars as Pane elements with custom-drawn buttons (no UIA Button children).
    /// Strategy: screenshot entire toolbar pane -> OCR -> find text regions.
    /// Usage: appbot toolbar-ocr "window-title" [--click "text"] [--save]
    static int ToolbarOcrCommand(string[] args)
    {
        if (args.Length < 1)
            return Error(@"Usage: appbot toolbar-ocr <window-title> [--click ""text""] [--save]
  OCR-scans toolbar panes and shows recognized text regions.
  --click ""text""  Click the region whose OCR text matches.
  --save           Save toolbar screenshots to output/toolbar/");

        string title = args[0];
        string? clickText = GetArgValue(args, "--click");
        bool save = args.Contains("--save");

        var windows = WindowFinder.FindWindows(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Find toolbar panes via UIA -- MFC toolbars are Pane elements with "툴바" in name
        var uia = new FlaUI.UIA3.UIA3Automation();
        var root = uia.FromHandle(win.Handle);

        // Look for panes named "*툴바*" (메뉴툴바, 화면툴바, 쾌속주문툴바)
        var allPanes = root.FindAllChildren(cf =>
            cf.ByControlType(FlaUI.Core.Definitions.ControlType.Pane));

        var toolbarPanes = new List<(FlaUI.Core.AutomationElements.AutomationElement elem, string name)>();
        foreach (var pane in allPanes)
        {
            string paneName;
            try { paneName = pane.Name ?? ""; } catch { paneName = ""; }
            if (paneName.Contains("툴바"))
                toolbarPanes.Add((pane, paneName));
        }

        if (toolbarPanes.Count == 0)
            return Error("No toolbar panes found (looking for panes with '툴바' in name).");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"Found {toolbarPanes.Count} toolbar pane(s).");
        Console.ResetColor();

        // Init OCR
        var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
        var lang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
        using var ocr = new SimpleOcrAnalyzer(primaryLanguage: lang, confidenceThreshold: 0.3);

        if (save)
            Directory.CreateDirectory(Path.Combine(DataDir, "output", "toolbar"));

        OcrWord? clickWord = null;
        System.Drawing.RectangleF clickPaneRect = default;
        int totalWords = 0;

        foreach (var (tbElem, tbName) in toolbarPanes)
        {
            var tbRect = tbElem.BoundingRectangle;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n-- {tbName} -- ({(int)tbRect.X},{(int)tbRect.Y}) {(int)tbRect.Width}x{(int)tbRect.Height}");
            Console.ResetColor();

            // Screenshot the entire toolbar pane
            try
            {
                using var bmp = ScreenCapture.CaptureScreenRegion(
                    (int)tbRect.X, (int)tbRect.Y, (int)tbRect.Width, (int)tbRect.Height);

                if (save)
                {
                    var safeName = string.Join("", tbName.Split(Path.GetInvalidFileNameChars()));
                    var outPath = Path.Combine(DataDir, "output", "toolbar", $"toolbar_{safeName}.png");
                    bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  Saved: {outPath}");
                    Console.ResetColor();
                }

                // OCR entire toolbar
                var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();

                if (ocrResult.Words.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  (no text recognized)");
                    Console.ResetColor();
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  {ocrResult.Words.Count} words recognized (upscale: x{ocrResult.UpscaleFactor})");
                Console.ResetColor();

                // Group nearby words into logical button labels
                // Words within ~5px Y-gap and ~15px X-gap belong together
                var groups = GroupWordsIntoLabels(ocrResult.Words);

                int idx = 0;
                foreach (var group in groups)
                {
                    // Build label: sort words top-to-bottom then left-to-right
                    var orderedWords = group.OrderBy(w => w.Y).ThenBy(w => w.X).ToList();
                    string labelText = string.Join("", orderedWords.Select(w => w.Text));
                    int gx = group.Min(w => w.X);
                    int gy = group.Min(w => w.Y);
                    int gx2 = group.Max(w => w.X + w.Width);
                    int gy2 = group.Max(w => w.Y + w.Height);
                    int gw = gx2 - gx;
                    int gh = gy2 - gy;

                    // Screen coordinates: pane origin + word offset
                    int screenX = (int)tbRect.X + (gx + gw / 2);
                    int screenY = (int)tbRect.Y + (gy + gh / 2);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  [{idx:D2}] \"{labelText}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"  rel=({gx},{gy} {gw}x{gh})");
                    Console.Write($"  scr=({screenX},{screenY})");
                    Console.WriteLine();
                    Console.ResetColor();

                    // Check if this is the click target
                    if (clickText != null &&
                        labelText.Contains(clickText, StringComparison.OrdinalIgnoreCase))
                    {
                        // Create a synthetic OcrWord for the group center
                        clickWord = new OcrWord
                        {
                            Text = labelText,
                            X = gx + gw / 2,
                            Y = gy + gh / 2,
                            Width = gw,
                            Height = gh
                        };
                        clickPaneRect = tbRect;
                    }

                    idx++;
                    totalWords++;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Summary
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  Total: {totalWords} text regions across {toolbarPanes.Count} toolbar(s)");
        Console.ResetColor();

        // Click target if found
        if (clickText != null)
        {
            if (clickWord != null)
            {
                int cx = (int)clickPaneRect.X + clickWord.X;
                int cy = (int)clickPaneRect.Y + clickWord.Y;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n  Clicking \"{clickWord.Text}\" at ({cx},{cy})...");
                Console.ResetColor();

                // Use physical mouse click (need foreground)
                WKAppBot.Win32.Input.InputReadiness.ReadinessCalled = true; // blocker dialog handler -- automation-invoked
                NativeMethods.SmartSetForegroundWindow(win.Handle); // [FOCUS-GUARD] CheckActiveGuard 적용
                Thread.Sleep(200);
                MouseInput.Click(cx, cy);
                Thread.Sleep(500);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" done.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  Text \"{clickText}\" not found in toolbar OCR results.");
                Console.ResetColor();
                return 1;
            }
        }

        return 0;
    }

    /// <summary>
    /// Group OCR words into logical toolbar button labels.
    /// MFC toolbars use 2-line layout: top line + bottom line per button.
    /// Words that overlap horizontally (within ~10px) are grouped as one button.
    /// Then sorted by X position of group center.
    /// </summary>
    private static List<List<OcrWord>> GroupWordsIntoLabels(List<OcrWord> words)
    {
        if (words.Count == 0) return new();

        // Strategy: group by X-overlap.
        // Two words belong to the same button if their X ranges overlap significantly.
        // This handles the 2-line toolbar layout where "캐치" (top) + "실전" (bottom) share X range.

        var sorted = words.OrderBy(w => w.X).ToList();
        var used = new bool[sorted.Count];
        var groups = new List<List<OcrWord>>();

        for (int i = 0; i < sorted.Count; i++)
        {
            if (used[i]) continue;

            var group = new List<OcrWord> { sorted[i] };
            used[i] = true;

            // Find all other words that overlap with this group's X range
            int groupLeft = sorted[i].X;
            int groupRight = sorted[i].X + sorted[i].Width;

            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int j = i + 1; j < sorted.Count; j++)
                {
                    if (used[j]) continue;
                    var w = sorted[j];
                    int wLeft = w.X;
                    int wRight = w.X + w.Width;

                    // Check X-overlap: word starts before group ends + some margin
                    // and word ends after group starts - some margin
                    int margin = 10;
                    bool overlaps = wLeft < groupRight + margin && wRight > groupLeft - margin;

                    if (overlaps)
                    {
                        group.Add(w);
                        used[j] = true;
                        // Expand group range
                        if (wLeft < groupLeft) groupLeft = wLeft;
                        if (wRight > groupRight) groupRight = wRight;
                        changed = true;
                    }
                }
            }

            groups.Add(group);
        }

        // Sort groups by their center X
        groups.Sort((a, b) =>
        {
            double ax = a.Average(w => w.X + w.Width / 2.0);
            double bx = b.Average(w => w.X + w.Width / 2.0);
            return ax.CompareTo(bx);
        });

        return groups;
    }

    /// Usage: appbot dismiss "영웅문" [keyword1] [keyword2] ...
    /// If no keywords given, closes common notice windows (공지, 인사, 안내, 알림).
    /// Sends WM_CLOSE to each matching MDI child.
    /// </summary>
    static int DismissCommand(string[] args)
    {
        if (args.Length < 1)
            return Error(@"Usage: appbot dismiss <window-title> [--force] [keyword1] [keyword2] ...
  Closes MDI child windows matching keywords in their title.
  If no keywords given, closes common notices (공지, 인사, 안내, 알림, POP-UP).
  --force: Skip critical importance check and close regardless.");

        // Parse --force flag
        bool force = args.Any(a => a == "--force");
        var filteredArgs = args.Where(a => a != "--force").ToArray();

        string title = filteredArgs[0];
        var keywords = filteredArgs.Length > 1
            ? filteredArgs.Skip(1).Where(k => !string.IsNullOrWhiteSpace(k) && k.Length >= 2).ToArray()
            : new[] { "공지", "인사", "안내", "알림", "POP-UP", "POPUP", "notice", "popup" };

        // Safety: empty keywords match EVERYTHING (String.Contains("") == true)
        if (keywords.Length == 0)
            return Error("No valid keywords provided. Keywords must be at least 2 characters. " +
                "Empty/whitespace keywords would match all windows -- this is a safety check to prevent accidental mass-close.");

        var windows = WindowFinder.FindWindows(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Load dialog handlers
        var handlersDir = Path.Combine(DataDir, "handlers");
        var handlerMgr = Directory.Exists(handlersDir) ? new DialogHandlerManager(handlersDir) : null;

        // Step 0: If target window itself IS a #32770 dialog, try handler matching directly
        // This handles cases like login dialogs where the dialog IS the top-level match
        {
            var targetClsSb = new StringBuilder(256);
            NativeMethods.GetClassNameW(win.Handle, targetClsSb, 256);
            var targetCls = targetClsSb.ToString();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [Step0] class=\"{targetCls}\" handlers={handlerMgr?.Count ?? -1}");
            Console.ResetColor();

            if (targetCls == "#32770" && handlerMgr != null && handlerMgr.Count > 0)
            {
                var classPath = BuildClassPath(win.Handle);
                NativeMethods.GetWindowThreadProcessId(win.Handle, out uint tPid);
                string tProcName = "";
                try { tProcName = Process.GetProcessById((int)tPid).ProcessName; } catch { }
                var msgText = GetDialogMessageText(win.Handle);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [Step0] classPath=\"{classPath}\" proc=\"{tProcName}\" msg=\"{(msgText?.Replace("\n"," ") ?? "").Substring(0, Math.Min(msgText?.Length ?? 0, 60))}\"");
                Console.ResetColor();

                var handler = handlerMgr.FindHandler(win.Title, classPath, targetCls, tProcName, msgText);
                if (handler != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  [DISMISS] Target is #32770 dialog -- handler matched: {handler.Action}");
                    Console.ResetColor();

                    if (handler.Action == "click_button")
                    {
                        bool ok = ExecuteClickButton(win.Handle, handler.Params);
                        if (ok)
                        {
                            // Run patrol wait loop if configured
                            RunStep0Patrol(win.Handle, win.Title, handler.Params, handlerMgr);
                            return 0;
                        }
                    }
                    else if (handler.Action == "dismiss")
                    {
                        NativeMethods.SendMessageW(win.Handle, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        Thread.Sleep(300);
                        bool gone = !NativeMethods.IsWindow(win.Handle) || !NativeMethods.IsWindowVisible(win.Handle);
                        Console.ForegroundColor = gone ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine(gone ? "  <- closed" : "  <- still visible");
                        Console.ResetColor();
                        if (gone)
                        {
                            // Run patrol wait loop if configured
                            RunStep0Patrol(win.Handle, win.Title, handler.Params, handlerMgr);
                            return 0;
                        }
                    }
                }
            }
        }

        // Step 1: Scan MDI children for matching titles
        var scanResult = AppScanner.Scan(win.Handle);
        int closedCount = 0;

        foreach (var form in scanResult.Forms)
        {
            var formTitle = form.FormName ?? "";
            bool matches = keywords.Any(kw =>
                formTitle.Contains(kw, StringComparison.OrdinalIgnoreCase));

            if (matches)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [DISMISS] [{form.FormId}] \"{formTitle}\"");
                Console.ResetColor();

                // Read notice content via OCR before closing
                var noticeText = ReadNoticeContent(form.Handle, formTitle);
                if (!string.IsNullOrWhiteSpace(noticeText))
                {
                    // Check importance (skip if --force)
                    var importance = ClassifyNoticeImportance(noticeText);
                    if (importance == NoticeImportance.Critical && !force)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"  *** IMPORTANT NOTICE -- NOT closing ***");
                        Console.ForegroundColor = ConsoleColor.White;
                        PrintNoticeText(noticeText);
                        Console.ResetColor();
                        continue; // skip closing
                    }

                    // Normal notice (or forced) -- print summary and close
                    Console.ForegroundColor = importance == NoticeImportance.Critical
                        ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
                    if (force && importance == NoticeImportance.Critical)
                        Console.WriteLine($"  (--force: ignoring critical keywords)");
                    PrintNoticeText(noticeText);
                    Console.ResetColor();
                }

                NativeMethods.SendMessageW(form.Handle, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(300);

                bool gone = !NativeMethods.IsWindow(form.Handle) || !NativeMethods.IsWindowVisible(form.Handle);
                Console.ForegroundColor = gone ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(gone ? "  <- closed" : "  <- still visible");
                Console.ResetColor();
                if (gone) closedCount++;
            }
        }

        // Step 2: Also scan for #32770 popup dialogs from same process (like before)
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        var popupCandidates = new List<IntPtr>();

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (hWnd == win.Handle) return true;
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != targetPid) return true;

            var clsSb = new StringBuilder(256);
            NativeMethods.GetClassNameW(hWnd, clsSb, 256);
            if (clsSb.ToString() == "#32770")
                popupCandidates.Add(hWnd);

            return true;
        }, IntPtr.Zero);

        foreach (var hPopup in popupCandidates)
        {
            var popTitle = new StringBuilder(256);
            NativeMethods.GetWindowTextW(hPopup, popTitle, 256);
            var classPath = BuildClassPath(hPopup);

            var clsSb2 = new StringBuilder(256);
            NativeMethods.GetClassNameW(hPopup, clsSb2, 256);
            var cls = clsSb2.ToString();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  [DISMISS] [{classPath}] \"{popTitle}\"");
            Console.ResetColor();

            // Try handler first
            NativeMethods.GetWindowThreadProcessId(hPopup, out uint popPid);
            string procName = "";
            try { procName = Process.GetProcessById((int)popPid).ProcessName; } catch { }
            var msgText = GetDialogMessageText(hPopup);

            var handler = handlerMgr?.FindHandler(popTitle.ToString(), classPath, cls, procName, msgText);
            if (handler != null && handler.Action == "click_button")
            {
                bool ok = ExecuteClickButton(hPopup, handler.Params);
                if (ok) closedCount++;
            }
            else
            {
                // Default: WM_CLOSE
                NativeMethods.SendMessageW(hPopup, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(300);
                bool gone = !NativeMethods.IsWindow(hPopup) || !NativeMethods.IsWindowVisible(hPopup);
                Console.ForegroundColor = gone ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(gone ? " <- closed" : " <- still visible");
                Console.ResetColor();
                if (gone) closedCount++;
            }
        }

        Console.ForegroundColor = closedCount > 0 ? ConsoleColor.Green : ConsoleColor.DarkGray;
        Console.WriteLine($"\n  {closedCount} window(s) dismissed.");
        Console.ResetColor();

        // -- ActionState IPC: share dismiss info with AppBotEye --
        try
        {
            ActionState.Write(new ActionState
            {
                Source = "dismiss",
                WindowTitle = win.Title,
                ActionName = "dismiss",
                ActionDetail = $"Dismiss: {closedCount} window(s) closed ({string.Join(",", keywords)})",
                Status = closedCount > 0 ? "pass" : "fail",
            });
        }
        catch { /* best-effort */ }

        return 0;
    }

    /// <summary>
    /// Click a button in a top-level dialog by title match.
    /// Usage: appbot dialog-click "dialog title" [button-index]
    /// Uses physical mouse click (works with owner-drawn buttons).
    /// </summary>
    static int DialogClickCommand(string[] args)
    {
        if (args.Length < 1)
            return Error(@"Usage: appbot dialog-click <dialog-title> [button-index]
  Finds a dialog by title and clicks a button using physical mouse.
  button-index: 0=first (default), 1=second, etc.");

        string title = args[0];
        int btnIndex = args.Length >= 2 && int.TryParse(args[1], out var bi) ? bi : 0;

        // Find dialog
        var windows = WindowFinder.FindWindows(title);
        if (windows.Count == 0) return Error($"Dialog not found: \"{title}\"");
        var dlg = windows[0];
        Console.WriteLine($"Dialog: [{dlg.Handle:X8}] \"{dlg.Title}\" ({dlg.Rect.Width}x{dlg.Rect.Height})");

        // Find all buttons recursively (up to 2 levels)
        var buttons = new List<WindowInfo>();
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(dlg.Handle, allChildren, 0, 3);
        buttons = allChildren.Where(c => c.ClassName == "Button" && c.IsVisible && c.Rect.Width > 10)
            .OrderBy(b => b.Rect.Left).ToList(); // left-to-right order (확인 first)

        if (buttons.Count == 0) return Error("No buttons found in dialog.");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        for (int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            var txt = !string.IsNullOrEmpty(b.Title) ? $"\"{b.Title}\"" : "(owner-drawn)";
            var marker = i == btnIndex ? " ◀" : "";
            Console.WriteLine($"  [{i}] {txt} {b.Rect.Width}x{b.Rect.Height} @({b.Rect.Left},{b.Rect.Top}){marker}");
        }
        Console.ResetColor();

        if (btnIndex >= buttons.Count)
            return Error($"Button index {btnIndex} out of range (have {buttons.Count})");

        var target = buttons[btnIndex];
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  Clicking [{btnIndex}]: ");
        Console.ResetColor();
        SmartClickButton(target.Handle, dlg.Handle);

        // Check if dialog closed
        if (!NativeMethods.IsWindowVisible(dlg.Handle))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Dialog closed v");
            Console.ResetColor();
        }

        return 0;
    }

}
