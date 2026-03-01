using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: do command — MFC combo selection + button click
internal partial class Program
{
    /// <summary>
    /// Multi-step action: activate MDI child, optionally click MFC custom combos + button.
    /// Usage: appbot do &lt;window-title&gt; &lt;form-id&gt; [button-text] [--delay N]
    /// If button-text is omitted, just brings MDI child to front (focusless MDI activate).
    /// </summary>
    static int DoCommand(string[] args)
    {
        if (args.Length < 2)
            return Error(@"Usage: appbot do <window-title> <form-id> [button-text] [--delay N]
  Activates MDI child form (focusless). If button-text given, also clicks it.

Examples:
  appbot do ""영웅문Global"" 2220                    # just bring MDI child to front (focusless!)
  appbot do ""영웅문"" 4051 ""매매시작""          # select combos + click 매매시작
  appbot do ""영웅문"" 4051 ""매매시작"" --delay 500  # custom delay between steps");

        string title = args[0];
        string targetFormId = args[1];
        // buttonText is now optional — if missing, just MDI-activate
        string? buttonText = args.Length >= 3 && !args[2].StartsWith("--") ? args[2] : null;
        int stepDelay = int.TryParse(GetArgValue(args, "--delay"), out var sd) ? sd : 300;

        // Load dialog handlers from handlers/ directory
        var handlersDir = Path.Combine(DataDir, "handlers");
        var handlerMgr = new DialogHandlerManager(handlersDir);
        if (handlerMgr.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[BLOCK] {handlerMgr}");
            Console.ResetColor();
        }

        // Find window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Elevation check (only needed when button click is required)
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        if (buttonText != null)
        {
            bool weAreElevated = NativeMethods.IsCurrentProcessElevated();
            if (!weAreElevated)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ Not elevated — physical mouse click requires admin. Re-run as admin.");
                Console.ResetColor();
                return 1;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  ✓ Elevated");
            Console.ResetColor();
            Console.WriteLine(" — physical mouse enabled");
        }

        // Load ExperienceDb from matching profile (best-effort)
        ExperienceDb? expDb = null;
        try
        {
            var profileStore = new ProfileStore();
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid2);
            string procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid2).ProcessName; } catch { }
            var match = profileStore.FindByMatch(win.ClassName, "")
                ?? (!string.IsNullOrEmpty(procName) ? profileStore.FindByMatch("", procName) : null);
            if (match != null)
            {
                var expDir = Path.Combine(profileStore.ProfileDir, $"{match.Value.name}_exp");
                expDb = new ExperienceDb(expDir);
            }
        }
        catch { /* best-effort — proceed without experience DB */ }

        // Find form
        var scanResult = AppScanner.Scan(win.Handle);

        // Auto-dismiss notice/popup MDI children before proceeding
        var noticeKeywords = new[] { "공지", "인사", "안내", "알림", "POP-UP", "POPUP" };
        foreach (var form in scanResult.Forms)
        {
            var fn = form.FormName ?? "";
            if (noticeKeywords.Any(kw => fn.Contains(kw, StringComparison.OrdinalIgnoreCase)))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [DISMISS] [{form.FormId}] \"{fn}\"");
                Console.ResetColor();

                // Read notice content before closing
                var noticeText = ReadNoticeContent(form.Handle, fn);
                if (!string.IsNullOrWhiteSpace(noticeText))
                {
                    var importance = ClassifyNoticeImportance(noticeText);
                    if (importance == NoticeImportance.Critical)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"  *** IMPORTANT NOTICE — NOT closing ***");
                        Console.ForegroundColor = ConsoleColor.White;
                        PrintNoticeText(noticeText);
                        Console.ResetColor();
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrintNoticeText(noticeText);
                    Console.ResetColor();
                }

                NativeMethods.SendMessageW(form.Handle, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(200);
                bool gone = !NativeMethods.IsWindow(form.Handle) || !NativeMethods.IsWindowVisible(form.Handle);
                Console.ForegroundColor = gone ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(gone ? "  ← closed" : "  ← still visible");
                Console.ResetColor();
            }
        }

        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found.");
            Console.ResetColor();
            return 1;
        }
        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName}");
        Console.WriteLine($"[A11Y] form={targetForm.FormId} name='{targetForm.FormName}' role=MDIForm");

        // Show past failure history + knowhow for this form (if any)
        if (expDb != null)
        {
            ShowFormExperienceHints(expDb, targetFormId, actionName: "click");
        }

        // ── Focusless MDI Activate: bring child to front without stealing focus ──
        if (scanResult.MdiHandle != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(scanResult.MdiHandle, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [MDI] Activated form [{targetFormId}] (focusless WM_MDIACTIVATE)");
            Console.ResetColor();
            Thread.Sleep(100);
        }

        // If no button text specified, just MDI-activate and return
        if (buttonText == null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✓ MDI child [{targetFormId}] brought to front (focusless)");
            Console.ResetColor();

            // ActionState IPC
            try
            {
                ActionState.Write(new ActionState
                {
                    Source = "do",
                    WindowTitle = win.Title,
                    ElementName = targetForm.FormName ?? targetFormId,
                    ActionName = "mdi_activate",
                    ActionDetail = $"MDI activate [{targetFormId}] \"{targetForm.FormName}\" (focusless)",
                    Status = "pass",
                });
            }
            catch { /* best-effort */ }
            return 0;
        }
        Console.WriteLine();

        // Find the target button (with blocker retry loop)
        WindowInfo? targetButton = null;
        int maxRetries = 3;
        for (int attempt = 0; attempt <= maxRetries; attempt++)
        {
            var allControls = new List<(WindowInfo Info, int Depth, string Path)>();
            FindControlsRecursive(targetForm.Handle, "", 0, 6, allControls);
            var allButtons = allControls.Where(c =>
                c.Info.ClassName == "Button" && !string.IsNullOrEmpty(c.Info.Title) && c.Info.Title.Contains(buttonText)).ToList();

            if (allButtons.Count > 0)
            {
                targetButton = allButtons[0].Info;
                break;
            }

            // Button not found — could be a blocker dialog hiding/disabling UI
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  Button \"{buttonText}\" not found (attempt {attempt + 1}/{maxRetries + 1})");
            Console.ResetColor();

            if (attempt < maxRetries)
            {
                var (handled, shouldRetry) = TryHandleBlocker(win.Handle, handlerMgr);
                if (handled && shouldRetry)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[BLOCK] Blocker handled. Re-scanning for button...");
                    Console.ResetColor();
                    continue;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ Button \"{buttonText}\" not found in form [{targetFormId}]");
            Console.ResetColor();
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  Found button: \"{targetButton!.Title}\" @({targetButton.Rect.Left},{targetButton.Rect.Top})");
        Console.ResetColor();

        // ── Step 1: Find MFC custom combos ──
        // Pattern: AfxWnd parent containing an enabled Edit child, small size, above the button
        // Direct Win32 tree walk (not UIA) to find these MFC custom controls
        var mfcCombos = new List<WindowInfo>();
        FindMfcCombos(targetForm.Handle, targetButton.Rect, mfcCombos, 0, 6);

        // Sort by X position (left to right)
        mfcCombos.Sort((a, b) => a.Rect.Left.CompareTo(b.Rect.Left));

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"── Action Sequence ({mfcCombos.Count} combos + 1 button) ────────────────────");
        Console.ResetColor();

        if (mfcCombos.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  No MFC custom combos found above button. Skipping to button click.");
            Console.ResetColor();
        }

        // Check for blockers before starting (e.g. disconnection dialog already showing)
        var (preBlocked, _) = TryHandleBlocker(win.Handle, handlerMgr);
        if (preBlocked)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[BLOCK] Pre-action blocker handled. Continuing...");
            Console.ResetColor();
        }

        // Bring window to front for physical mouse
        NativeMethods.SmartSetForegroundWindow(win.Handle);
        Thread.Sleep(stepDelay);

        // ── Process each combo ──
        // Click the EDIT inside the AfxWnd (more precise than clicking container)
        int comboNum = 0;
        foreach (var combo in mfcCombos)
        {
            comboNum++;

            // Find the Edit child inside this combo container
            var comboChildren = WindowFinder.GetChildrenZOrder(combo.Handle);
            var editChild = comboChildren.FirstOrDefault(c => c.ClassName == "Edit");
            var clickTarget = editChild ?? combo; // fallback to container

            // Re-read rect (window might have moved)
            NativeMethods.GetWindowRect(clickTarget.Handle, out var editRect);
            int cx = (editRect.Left + editRect.Right) / 2;
            int cy = (editRect.Top + editRect.Bottom) / 2;

            // Read current value
            var valSb = new StringBuilder(256);
            NativeMethods.SendMessageW(clickTarget.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, valSb);
            var currentVal = valSb.ToString();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"  [{comboNum}/{mfcCombos.Count}] Combo");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" @({cx},{cy}) {clickTarget.Rect.Width}x{clickTarget.Rect.Height}");
            if (!string.IsNullOrEmpty(currentVal))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" (\"{currentVal}\")");
            }
            Console.ResetColor();
            Console.Write(" → click ... ");

            // Snapshot windows before click
            var preWindows = new HashSet<IntPtr>();
            NativeMethods.EnumWindows((hWnd, _) => { preWindows.Add(hWnd); return true; }, IntPtr.Zero);

            // Click the Edit to open dropdown
            MouseInput.Click(cx, cy);
            Thread.Sleep(stepDelay + 300); // wait for dropdown

            // Capture screenshot to see what happened
            try
            {
                using var bmp = ScreenCapture.CaptureWindow(win.Handle);
                var ssPath = Path.Combine(DataDir, "logs", $"combo{comboNum}_opened.png");
                bmp.Save(ssPath);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[screenshot: {Path.GetFileName(ssPath)}] ");
                Console.ResetColor();
            }
            catch { /* screenshot not critical */ }

            // Detect dropdown: look for NEW top-level windows from same process
            var dropdownHwnd = IntPtr.Zero;
            var newWindows = new List<(IntPtr Handle, string ClassName, RECT Rect)>();

            NativeMethods.EnumWindows((hWnd, _) =>
            {
                if (preWindows.Contains(hWnd)) return true;
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid != targetPid) return true;
                var cls = WindowFinder.GetClassName(hWnd);
                NativeMethods.GetWindowRect(hWnd, out var r);
                if (r.Width > 10 && r.Height > 10)
                    newWindows.Add((hWnd, cls, r));
                return true;
            }, IntPtr.Zero);

            // Also check for new children under combo's grandparent
            var grandParent = NativeMethods.GetParent(NativeMethods.GetParent(combo.Handle));
            if (grandParent != IntPtr.Zero)
            {
                var allDescendants = new List<WindowInfo>();
                FindAllChildrenFlat(grandParent, allDescendants, 0, 4);
                foreach (var desc in allDescendants)
                {
                    if (desc.ClassName == "ListBox" && desc.IsVisible && desc.Rect.Height > 20)
                    {
                        dropdownHwnd = desc.Handle;
                        break;
                    }
                }
            }

            // Pick the best dropdown candidate from new windows
            if (dropdownHwnd == IntPtr.Zero && newWindows.Count > 0)
            {
                // Prefer ListBox, then anything near the combo
                var listBox = newWindows.FirstOrDefault(w => w.ClassName.Contains("ListBox"));
                if (listBox.Handle != IntPtr.Zero)
                    dropdownHwnd = listBox.Handle;
                else
                {
                    // Pick the one closest to the combo
                    var nearest = newWindows.OrderBy(w => Math.Abs(w.Rect.Top - editRect.Bottom)).First();
                    dropdownHwnd = nearest.Handle;
                }
            }

            if (dropdownHwnd != IntPtr.Zero)
            {
                var ddCls = WindowFinder.GetClassName(dropdownHwnd);
                NativeMethods.GetWindowRect(dropdownHwnd, out var ddRect);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"dropdown [{ddCls}] {ddRect.Width}x{ddRect.Height} → ");
                Console.ResetColor();

                // Click first item (top of dropdown + offset)
                int itemX = (ddRect.Left + ddRect.Right) / 2;
                int itemY = ddRect.Top + 12;
                MouseInput.Click(itemX, itemY);
                Thread.Sleep(stepDelay);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("selected first item ✓");
                Console.ResetColor();
            }
            else
            {
                // No popup window → in-place dropdown (MFC custom)
                // The list appears BELOW the edit. Click first item by mouse.
                NativeMethods.GetWindowRect(clickTarget.Handle, out var editRect2);
                int firstItemX = (editRect2.Left + editRect2.Right) / 2;
                int firstItemY = editRect2.Bottom + 14; // first visible item

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"in-place dropdown → click @({firstItemX},{firstItemY}) ... ");
                Console.ResetColor();

                MouseInput.Click(firstItemX, firstItemY);
                Thread.Sleep(stepDelay + 200);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓");
                Console.ResetColor();
            }

            // Small delay to let UI settle before next step
            Thread.Sleep(300);

            // Read new value after selection
            var newValSb = new StringBuilder(256);
            NativeMethods.SendMessageW(clickTarget.Handle, 0x000D /*WM_GETTEXT*/, (IntPtr)256, newValSb);
            var newVal = newValSb.ToString();
            if (newVal != currentVal)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("         value: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\"{currentVal}\" → \"{newVal}\"");
                Console.ResetColor();
            }

            Thread.Sleep(stepDelay);
        }

        // ── Final: Click the button (SmartClickButton with experience DB) ──
        Console.WriteLine();

        // Show control-level experience hints for the button being clicked
        if (expDb != null)
        {
            ShowControlExperienceHints(expDb, targetFormId, targetButton.ControlId, actionName: "click");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  ▶ Clicking: \"{targetButton.Title}\" ");
        Console.ResetColor();

        SmartClickButton(targetButton.Handle, targetForm.Handle,
            expDb, targetFormId, targetButton.ControlId);

        // ── ActionState IPC: share do-command info with AppBotEye ──
        try
        {
            ActionState.Write(new ActionState
            {
                Source = "do",
                WindowTitle = win.Title,
                ElementName = targetButton.Title,
                ActionName = "button_click",
                ActionDetail = $"Do: click \"{buttonText}\" in [{targetFormId}]",
                Status = "pass",
            });
        }
        catch { /* best-effort */ }

        // Wait and check reaction
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n── Reaction Check ────────────────────");
        Console.ResetColor();

        bool anyReaction = false;
        bool autoConfirm = args.Contains("--confirm");

        var postFgHwnd = NativeMethods.GetForegroundWindow();
        if (postFgHwnd != IntPtr.Zero)
        {
            var fgInfo = WindowInfo.FromHwnd(postFgHwnd);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("  Foreground: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{fgInfo.ClassName}] \"{fgInfo.Title}\" ({fgInfo.Rect.Width}x{fgInfo.Rect.Height})");
            Console.ResetColor();

            if (postFgHwnd != win.Handle)
            {
                ReadDialogContents(postFgHwnd);
                anyReaction = true;

                // Try handler-based auto-handling first, then --confirm fallback
                var (handled, shouldRetry) = TryHandleBlocker(win.Handle, handlerMgr);
                if (!handled && autoConfirm && fgInfo.ClassName == "#32770")
                {
                    // Legacy --confirm fallback: click first button in #32770 dialogs
                    ClickFirstButtonInDialog(postFgHwnd, "confirm");
                }
            }
        }

        // Check button text change (매매시작 → 매매중지?)
        var postButtonText = WindowFinder.GetWindowText(targetButton.Handle);
        if (postButtonText != targetButton.Title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Button changed: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\"{targetButton.Title}\" → \"{postButtonText}\"");
            Console.ResetColor();
            anyReaction = true;
        }

        // Check for popups from same process
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid != targetPid || hWnd == win.Handle || hWnd == postFgHwnd) return true;
            NativeMethods.GetWindowRect(hWnd, out var r);
            if (r.Width < 50 || r.Height < 30) return true;
            var info = WindowInfo.FromHwnd(hWnd);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Popup: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[{info.ClassName}] \"{info.Title}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({info.Rect.Width}x{info.Rect.Height})");
            Console.ResetColor();
            ReadDialogContents(hWnd);
            anyReaction = true;
            return true;
        }, IntPtr.Zero);

        if (!anyReaction)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  (no visible reaction)");
            Console.ResetColor();
        }

        return 0;
    }

}
