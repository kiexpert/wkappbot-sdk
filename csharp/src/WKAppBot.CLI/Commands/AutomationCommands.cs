using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: click, do commands + SmartClickButton + CheckDialogGone
internal partial class Program
{
    static int ClickCommand(string[] args)
    {
        if (args.Length < 2)
            return Error(@"Usage: appbot click <window-title> <form-id> [button-text]
  Finds a specific MDI form and clicks a button inside it.
  If button-text is omitted, lists all buttons in the form.

Examples:
  appbot click ""영웅문"" 4051              # list all buttons in [4051] 캐치 실전매매
  appbot click ""영웅문"" 4051 ""매매시작""    # click the 매매시작 button
  appbot click ""투혼"" 1101 --cid 3785    # click button by control ID
  appbot click ""영웅문"" 4051 --list-all   # list ALL controls (buttons, combos, edits)
  appbot click ""영웅문"" 4051 --combo 1 0  # select item 0 in combo #1, then click
  appbot click ""영웅문"" 4051 ""매매시작"" --combo 1 0 --combo 2 0  # combos then click");

        string title = args[0];
        string targetFormId = args[1];
        string? buttonText = args.Length >= 3 && !args[2].StartsWith("--") ? args[2] : null;
        int? targetCid = int.TryParse(GetArgValue(args, "--cid"), out var cid) ? cid : null;
        bool dryRun = args.Contains("--dry");
        bool listAll = args.Contains("--list-all");
        int maxDepth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 4;

        // Parse --combo N INDEX pairs (e.g., --combo 1 0 --combo 2 0)
        var comboSelections = new List<(int comboIndex, int itemIndex)>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--combo" && i + 2 < args.Length)
            {
                if (int.TryParse(args[i + 1], out var ci) && int.TryParse(args[i + 2], out var ii))
                    comboSelections.Add((ci, ii));
            }
        }

        // Find target window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Check elevation: target app elevated but we're not?
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        var targetElevated = NativeMethods.IsProcessElevated(targetPid);
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();

        if ((targetElevated == true || targetElevated == null) && !weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠ Target process (pid={targetPid}) is elevated (admin).");
            Console.WriteLine($"  ⚠ This process is NOT elevated → SendInput/SetCursorPos will be blocked by UIPI.");
            Console.ResetColor();

            // Auto-relaunch as admin
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  → Re-launching as administrator...");
            Console.ResetColor();
            Console.Out.Flush();

            try
            {
                // Find the .exe (not .dll) — dotnet publish creates both
                var exePath = Environment.ProcessPath ?? "wkappbot.exe";
                if (exePath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                    exePath = Path.ChangeExtension(exePath, ".exe");

                var exeDir = Path.GetDirectoryName(exePath) ?? ".";
                var escapedArgs = string.Join(" ", args.Select(a => a.Contains(' ') ? $"\"{a}\"" : a));

                // Use cmd /c to set DOTNET_ROOT and run, so output goes to a temp file we can read
                var resultFile = Path.Combine(DataDir, "logs", "_elevated_result.txt");
                var cmdLine = $"/c set \"DOTNET_ROOT={Environment.GetEnvironmentVariable("DOTNET_ROOT")}\" && " +
                              $"\"{exePath}\" click {escapedArgs} > \"{resultFile}\" 2>&1";

                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmdLine,
                    WorkingDirectory = exeDir,
                    UseShellExecute = true,
                    Verb = "runas",  // triggers UAC
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                var proc = Process.Start(psi);
                proc?.WaitForExit();

                // Show the elevated process's output
                if (File.Exists(resultFile))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  ── Elevated process output ──");
                    Console.ResetColor();
                    Console.Write(File.ReadAllText(resultFile));
                }

                return proc?.ExitCode ?? 1;
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // user cancelled UAC
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✗ UAC cancelled. Cannot click elevated app without admin privileges.");
                Console.ResetColor();
                return 1;
            }
        }

        if (weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  ✓ Running elevated");
            Console.ResetColor();
            Console.WriteLine(" — physical mouse input enabled");
        }

        // Scan to find MDI forms
        var scanResult = AppScanner.Scan(win.Handle);

        // Find the specific form by form_id
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found. Available forms:");
            Console.ResetColor();
            foreach (var f in scanResult.Forms.Where(f => f.FormId != null).GroupBy(f => f.FormId!))
            {
                var first = f.First();
                Console.WriteLine($"  [{first.FormId}] {first.FormName}");
            }
            return 1;
        }

        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName} ({targetForm.Rect.Width}x{targetForm.Rect.Height})");
        Console.WriteLine();

        // Recursively find all controls in this form
        var allControls = new List<(WindowInfo Info, int Depth, string Path)>();
        FindControlsRecursive(targetForm.Handle, "", 0, maxDepth, allControls);

        // Separate by type
        var allButtons = allControls.Where(c => c.Info.ClassName == "Button").ToList();
        var allCombos = allControls.Where(c => c.Info.ClassName == "ComboBox").ToList();

        // --list-all: show every control
        if (listAll)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── All Controls in [{targetFormId}] ({allControls.Count}) ────────────────────");
            Console.ResetColor();
            foreach (var (ctrl, depth, path) in allControls)
            {
                var txt = !string.IsNullOrEmpty(ctrl.Title) ? $" \"{ctrl.Title}\"" : "";
                var vis = ctrl.IsVisible ? "" : " [hidden]";
                var color = ctrl.ClassName switch
                {
                    "Button" => ConsoleColor.Yellow,
                    "ComboBox" => ConsoleColor.Blue,
                    "Edit" => ConsoleColor.Green,
                    "Static" => ConsoleColor.DarkGray,
                    _ => ConsoleColor.Gray
                };
                Console.ForegroundColor = color;
                Console.Write($"    [{ctrl.ClassName}]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" cid={ctrl.ControlId,-6}{txt} {ctrl.Rect.Width}x{ctrl.Rect.Height} @({ctrl.Rect.Left},{ctrl.Rect.Top}){vis}");
                if (!string.IsNullOrEmpty(path)) Console.Write($" [{path}]");
                Console.WriteLine();
            }
            Console.ResetColor();

            // Show combo box details
            if (allCombos.Count > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"── ComboBoxes ({allCombos.Count}) ────────────────────");
                Console.ResetColor();
                for (int ci = 0; ci < allCombos.Count; ci++)
                {
                    var combo = allCombos[ci].Info;
                    int count = (int)NativeMethods.SendMessageW(combo.Handle, 0x0146 /*CB_GETCOUNT*/, IntPtr.Zero, IntPtr.Zero);
                    int curSel = (int)NativeMethods.SendMessageW(combo.Handle, 0x0147 /*CB_GETCURSEL*/, IntPtr.Zero, IntPtr.Zero);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"  Combo #{ci + 1}");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" cid={combo.ControlId} @({combo.Rect.Left},{combo.Rect.Top}) {combo.Rect.Width}x{combo.Rect.Height}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" — {count} items (selected: {(curSel >= 0 ? curSel.ToString() : "none")})");
                    Console.ResetColor();

                    for (int j = 0; j < Math.Min(count, 30); j++)
                    {
                        int len = (int)NativeMethods.SendMessageW(combo.Handle, 0x0149 /*CB_GETLBTEXTLEN*/, (IntPtr)j, IntPtr.Zero);
                        if (len > 0 && len < 1024)
                        {
                            var sb = new StringBuilder(len + 2);
                            NativeMethods.SendMessageW(combo.Handle, 0x0148 /*CB_GETLBTEXT*/, (IntPtr)j, sb);
                            var marker = j == curSel ? " ◀" : "";
                            Console.ForegroundColor = j == curSel ? ConsoleColor.Yellow : ConsoleColor.Gray;
                            Console.WriteLine($"    [{j}] {sb}{marker}");
                            Console.ResetColor();
                        }
                    }
                }
            }

            return 0;
        }

        if (allButtons.Count == 0 && comboSelections.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  No buttons found in this form.");
            Console.ResetColor();

            // Also try OCR to show what's visible
            Console.WriteLine("\n  Visible child controls:");
            var children = WindowFinder.GetChildrenZOrder(targetForm.Handle);
            foreach (var c in children.Take(30))
            {
                var vis = c.IsVisible ? "" : " [hidden]";
                var txt = !string.IsNullOrEmpty(c.Title) ? $" \"{c.Title}\"" : "";
                Console.WriteLine($"    [{c.ClassName}] cid={c.ControlId} {c.Rect.Width}x{c.Rect.Height}{txt}{vis}");
            }
            return 1;
        }

        // Display all found buttons
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"── Buttons in [{targetFormId}] ({allButtons.Count}) ────────────────────");
        Console.ResetColor();

        WindowInfo? matchedButton = null;

        foreach (var (btn, depth, path) in allButtons)
        {
            var txt = !string.IsNullOrEmpty(btn.Title) ? btn.Title : "(no text)";
            var size = $"{btn.Rect.Width}x{btn.Rect.Height}";
            var vis = btn.IsVisible ? "" : " [hidden]";

            // Check if this button matches the target
            bool isMatch = false;
            if (targetCid.HasValue && btn.ControlId == targetCid.Value)
                isMatch = true;
            else if (buttonText != null && !string.IsNullOrEmpty(btn.Title) && btn.Title.Contains(buttonText))
                isMatch = true;

            if (isMatch)
            {
                matchedButton = btn;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("  ▶ ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("    ");
            }

            Console.ForegroundColor = isMatch ? ConsoleColor.White : ConsoleColor.Gray;
            Console.Write($"cid={btn.ControlId,-6}");
            Console.ForegroundColor = isMatch ? ConsoleColor.Yellow : ConsoleColor.DarkYellow;
            Console.Write($" \"{txt}\"");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" {size} @({btn.Rect.Left},{btn.Rect.Top}){vis}");
            if (!string.IsNullOrEmpty(path))
                Console.Write($" [{path}]");
            Console.WriteLine();
            Console.ResetColor();
        }

        // ── Process combo selections BEFORE button click ──
        if (comboSelections.Count > 0 && allCombos.Count > 0)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── ComboBox Selections ────────────────────");
            Console.ResetColor();

            foreach (var (comboIdx, itemIdx) in comboSelections)
            {
                if (comboIdx < 1 || comboIdx > allCombos.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✗ Combo #{comboIdx} not found (have {allCombos.Count} combos)");
                    Console.ResetColor();
                    continue;
                }
                var combo = allCombos[comboIdx - 1].Info;
                int count = (int)NativeMethods.SendMessageW(combo.Handle, 0x0146 /*CB_GETCOUNT*/, IntPtr.Zero, IntPtr.Zero);
                if (itemIdx < 0 || itemIdx >= count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✗ Combo #{comboIdx}: item [{itemIdx}] out of range (have {count} items)");
                    Console.ResetColor();
                    continue;
                }

                // Get item text for display
                int len = (int)NativeMethods.SendMessageW(combo.Handle, 0x0149 /*CB_GETLBTEXTLEN*/, (IntPtr)itemIdx, IntPtr.Zero);
                var itemText = "(?)";
                if (len > 0 && len < 1024)
                {
                    var sb = new StringBuilder(len + 2);
                    NativeMethods.SendMessageW(combo.Handle, 0x0148 /*CB_GETLBTEXT*/, (IntPtr)itemIdx, sb);
                    itemText = sb.ToString();
                }

                // Select the item
                NativeMethods.SendMessageW(combo.Handle, 0x014E /*CB_SETCURSEL*/, (IntPtr)itemIdx, IntPtr.Zero);
                Thread.Sleep(100);

                // Notify parent (CBN_SELCHANGE = 1)
                int notifyCode = 1; // CBN_SELCHANGE
                int comboControlId = NativeMethods.GetDlgCtrlID(combo.Handle);
                IntPtr wParam = (IntPtr)((notifyCode << 16) | (comboControlId & 0xFFFF));
                var parent = NativeMethods.GetParent(combo.Handle);
                NativeMethods.SendMessageW(parent, 0x0111 /*WM_COMMAND*/, wParam, combo.Handle);
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"  ✓ Combo #{comboIdx}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" → [{itemIdx}] \"{itemText}\"");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($" (cid={comboControlId})");
                Console.ResetColor();
            }
        }

        // If no specific button requested, just list them
        if (buttonText == null && !targetCid.HasValue)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Tip: appbot click \"<title>\" <form-id> \"<button-text>\" to click a button");
            Console.WriteLine("  Tip: appbot click \"<title>\" <form-id> --list-all   to see ALL controls");
            Console.ResetColor();
            return 0;
        }

        // Click the matched button
        if (matchedButton == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var searchDesc = targetCid.HasValue ? $"cid={targetCid}" : $"\"{buttonText}\"";
            Console.WriteLine($"\n  Button {searchDesc} not found in [{targetFormId}]");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine();
        if (dryRun)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [DRY RUN] Would click: \"{matchedButton.Title}\" cid={matchedButton.ControlId}");
            Console.WriteLine($"  Screen pos: ({matchedButton.Rect.Left + matchedButton.Rect.Width/2}, {matchedButton.Rect.Top + matchedButton.Rect.Height/2})");
            Console.ResetColor();
        }
        else
        {
            int cx = matchedButton.Rect.Left + matchedButton.Rect.Width / 2;
            int cy = matchedButton.Rect.Top + matchedButton.Rect.Height / 2;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"  Clicking: \"{matchedButton.Title}\" cid={matchedButton.ControlId}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($" @({cx},{cy})");
            Console.ResetColor();
            Console.Write(" ... ");

            // Snapshot: child windows of main + form BEFORE click
            var preMainChildren = new HashSet<IntPtr>(
                WindowFinder.GetChildrenZOrder(win.Handle).Select(c => c.Handle));
            var preFormChildren = new HashSet<IntPtr>(
                WindowFinder.GetChildrenZOrder(targetForm.Handle).Select(c => c.Handle));
            var preFgHwnd = NativeMethods.GetForegroundWindow();
            var preButtonText = matchedButton.Title;

            // Bring MAIN window to front (MDI child can't be foreground independently)
            NativeMethods.SmartSetForegroundWindow(win.Handle);
            Thread.Sleep(300);

            // Re-read button rect (in case window moved during focus switch)
            NativeMethods.GetWindowRect(matchedButton.Handle, out var btnRect);
            cx = (btnRect.Left + btnRect.Right) / 2;
            cy = (btnRect.Top + btnRect.Bottom) / 2;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"(rect: {btnRect.Left},{btnRect.Top}-{btnRect.Right},{btnRect.Bottom}) ");
            Console.ResetColor();

            // ── Click Strategy ──
            // Try message-based click first (no cursor movement).
            // If no reaction detected, fall back to physical mouse click.
            bool usePhysicalMouse = args.Contains("--mouse");
            string clickMethod;

            if (!usePhysicalMouse)
            {
                // Strategy 1: BM_CLICK (0x00F5) — standard button click message
                // Works for standard Win32 Button class, even owner-drawn
                NativeMethods.PostMessageW(matchedButton.Handle, 0x00F5, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(200);

                // Strategy 2: WM_LBUTTONDOWN/UP with local coords
                // Works for custom controls that don't respond to BM_CLICK
                int localX = cx - btnRect.Left;
                int localY = cy - btnRect.Top;
                IntPtr lParam = (IntPtr)((localY << 16) | (localX & 0xFFFF));
                NativeMethods.PostMessageW(matchedButton.Handle, 0x0201, (IntPtr)0x0001, lParam);
                Thread.Sleep(50);
                NativeMethods.PostMessageW(matchedButton.Handle, 0x0202, IntPtr.Zero, lParam);

                clickMethod = "message";
            }
            else
            {
                // Strategy 3: Physical mouse (SendInput) — guaranteed but moves cursor
                MouseInput.Click(cx, cy);
                clickMethod = "mouse";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"clicked!");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" [{clickMethod}]");
            Console.ResetColor();

            // Wait for reaction
            Thread.Sleep(500);

            // ── Detect reaction ──
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n── Reaction Check ────────────────────");
            Console.ResetColor();

            bool anyReaction = false;

            // 1. Check for new popup/dialog windows (system-wide top-level)
            var postFgHwnd = NativeMethods.GetForegroundWindow();
            if (postFgHwnd != preFgHwnd && postFgHwnd != targetForm.Handle && postFgHwnd != win.Handle)
            {
                var popupInfo = WindowInfo.FromHwnd(postFgHwnd);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  📢 New foreground window: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[{popupInfo.ClassName}] \"{popupInfo.Title}\" ({popupInfo.Rect.Width}x{popupInfo.Rect.Height})");
                Console.ResetColor();
                ReadDialogContents(postFgHwnd);
                anyReaction = true;
            }

            // 2. Check for new child windows under main window (HTS alerts are often children)
            var postMainChildren = WindowFinder.GetChildrenZOrder(win.Handle);
            foreach (var child in postMainChildren)
            {
                if (!preMainChildren.Contains(child.Handle) && child.IsVisible)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 New child window: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{child.ClassName}] \"{child.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({child.Rect.Width}x{child.Rect.Height})");
                    Console.ResetColor();
                    anyReaction = true;
                }
            }

            // 3. Check for new children under the form (in-form popups)
            var postFormChildren = WindowFinder.GetChildrenZOrder(targetForm.Handle);
            foreach (var child in postFormChildren)
            {
                if (!preFormChildren.Contains(child.Handle) && child.IsVisible)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 New form child: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{child.ClassName}] \"{child.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({child.Rect.Width}x{child.Rect.Height})");
                    Console.ResetColor();
                    anyReaction = true;
                }
            }

            // 4. Check if button text changed (toggle buttons like 매매시작→매매중지)
            var postButtonText = WindowFinder.GetWindowText(matchedButton.Handle);
            if (postButtonText != preButtonText)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  🔄 Button text changed: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\"{preButtonText}\" → \"{postButtonText}\"");
                Console.ResetColor();
                anyReaction = true;
            }

            // 5. Check if any MessageBox-like popup appeared (same process, any window)
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint clickTargetPid);
            var topWindows = WindowFinder.FindByTitle(""); // all visible windows
            foreach (var tw in topWindows)
            {
                NativeMethods.GetWindowThreadProcessId(tw.Handle, out uint twPid);
                if (twPid == clickTargetPid && !preMainChildren.Contains(tw.Handle)
                    && tw.Handle != win.Handle && tw.Handle != postFgHwnd
                    && tw.IsVisible && tw.Rect.Width > 50 && tw.Rect.Height > 30)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  📢 Popup: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{tw.ClassName}] \"{tw.Title}\"");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" ({tw.Rect.Width}x{tw.Rect.Height})");
                    Console.ResetColor();
                    ReadDialogContents(tw.Handle);
                    anyReaction = true;
                }
            }

            if (!anyReaction)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  (no visible reaction detected — button may require specific state)");
                Console.ResetColor();
            }
        }

        return 0;
    }

    /// <summary>
    /// Dump all Win32 children of a specific MDI form as a tree.
    /// Shows EVERY control with class, CID, text, rect — for debugging custom MFC controls.
    /// </summary>
    /// <summary>
    /// Multi-step action: click MFC custom combos (AfxWnd+Edit) to open dropdown,
    /// select first item, then click a button. Designed for HTS trading forms.
    /// Usage: appbot do &lt;window-title&gt; &lt;form-id&gt; &lt;button-text&gt; [--delay N]
    /// </summary>
    static int DoCommand(string[] args)
    {
        if (args.Length < 3)
            return Error(@"Usage: appbot do <window-title> <form-id> <button-text> [--delay N]
  Selects first item in all MFC custom combos, then clicks a button.

Examples:
  appbot do ""영웅문"" 4051 ""매매시작""          # select combos + click 매매시작
  appbot do ""영웅문"" 4051 ""매매시작"" --delay 500  # custom delay between steps");

        string title = args[0];
        string targetFormId = args[1];
        string buttonText = args[2];
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

        // Elevation check
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  ▶ Clicking: \"{targetButton.Title}\" ");
        Console.ResetColor();

        SmartClickButton(targetButton.Handle, targetForm.Handle,
            expDb, targetFormId, targetButton.ControlId);

        // Wait and check reaction
        Thread.Sleep(500);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n── Reaction Check ────────────────────");
        Console.ResetColor();

        bool anyReaction = false;
        bool autoConfirm = args.Contains("--confirm");

        var postFgHwnd = NativeMethods.GetForegroundWindow();
        if (postFgHwnd != win.Handle && postFgHwnd != IntPtr.Zero)
        {
            var popupInfo = WindowInfo.FromHwnd(postFgHwnd);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  New foreground: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{popupInfo.ClassName}] \"{popupInfo.Title}\" ({popupInfo.Rect.Width}x{popupInfo.Rect.Height})");
            Console.ResetColor();
            ReadDialogContents(postFgHwnd);
            anyReaction = true;

            // Try handler-based auto-handling first, then --confirm fallback
            var (handled, shouldRetry) = TryHandleBlocker(win.Handle, handlerMgr);
            if (!handled && autoConfirm && popupInfo.ClassName == "#32770")
            {
                // Legacy --confirm fallback: click first button in #32770 dialogs
                ClickFirstButtonInDialog(postFgHwnd, "confirm");
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

    /// <summary>
    /// Find and click the first Button in a dialog window.
    /// Searches direct children and one level of nested panels.
    /// </summary>
    private static void ClickFirstButtonInDialog(IntPtr hDialog, string label)
    {
        // Find first button: direct children, then one level deeper
        var allChildren = new List<WindowInfo>();
        FindAllChildrenFlat(hDialog, allChildren, 0, 3);
        var buttons = allChildren.Where(c => c.ClassName == "Button" && c.IsVisible && c.Rect.Width > 10)
            .OrderBy(b => b.Rect.Left).ToList();

        if (buttons.Count == 0) return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"  ✓ Auto-{label}: ");
        Console.ResetColor();

        SmartClickButton(buttons[0].Handle, hDialog);
    }

    /// <summary>
    /// Smart click: tries strategies in experience-optimized order, checking for window change after each.
    /// Default priority: no cursor movement first → cursor movement last.
    /// With ExperienceDb: reorders by historical success rate.
    /// Detection: checks if dialog closed OR a new modal appeared.
    /// </summary>
    private static bool SmartClickButton(
        IntPtr hButton, IntPtr hDialogOrParent,
        ExperienceDb? expDb = null, string? formId = null, int? controlId = null)
    {
        NativeMethods.GetWindowRect(hButton, out var btnRect);
        int cx = (btnRect.Left + btnRect.Right) / 2;
        int cy = (btnRect.Top + btnRect.Bottom) / 2;
        int localX = (btnRect.Right - btnRect.Left) / 2;
        int localY = (btnRect.Bottom - btnRect.Top) / 2;

        // Strategy implementations keyed by name
        var strategyActions = new Dictionary<string, Action>
        {
            ["bm_click"] = () =>
            {
                NativeMethods.PostMessageW(hButton, 0x00F5 /*BM_CLICK*/, IntPtr.Zero, IntPtr.Zero);
            },
            ["wm_lbutton"] = () =>
            {
                IntPtr lParam = (IntPtr)((localY << 16) | (localX & 0xFFFF));
                NativeMethods.SendMessageW(hButton, 0x0201 /*WM_LBUTTONDOWN*/, (IntPtr)0x0001, lParam);
                Thread.Sleep(80);
                NativeMethods.SendMessageW(hButton, 0x0202 /*WM_LBUTTONUP*/, IntPtr.Zero, lParam);
            },
            ["send_input"] = () =>
            {
                NativeMethods.SetForegroundWindow(hDialogOrParent);
                Thread.Sleep(100);
                MouseInput.MoveTo(cx, cy);
                Thread.Sleep(150);
                var downInput = new INPUT[1];
                downInput[0].type = INPUT.INPUT_MOUSE;
                downInput[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
                downInput[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                NativeMethods.SendInput(1, downInput, Marshal.SizeOf<INPUT>());
                Thread.Sleep(80);
                var upInput = new INPUT[1];
                upInput[0].type = INPUT.INPUT_MOUSE;
                upInput[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
                upInput[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                NativeMethods.SendInput(1, upInput, Marshal.SizeOf<INPUT>());
            },
        };

        // Get optimal order from experience DB (or default)
        bool hasExpData = expDb != null && formId != null && controlId != null;
        var order = hasExpData
            ? expDb!.GetBestClickOrder(formId!, controlId!.Value)
            : (IReadOnlyList<string>)new[] { "bm_click", "wm_lbutton", "send_input" };

        // Show experience-based order if it differs from default
        if (hasExpData)
        {
            var ctrl = expDb!.GetControl(formId!, controlId!.Value);
            if (ctrl?.ClickStrategies != null && ctrl.ClickStrategies.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"@({cx},{cy}) [EXP] ");
                var parts = order.Select(s =>
                {
                    if (ctrl.ClickStrategies.TryGetValue(s, out var st))
                        return $"{s}({st.SuccessRate:P0})";
                    return $"{s}(new)";
                });
                Console.Write(string.Join(" → ", parts));
                Console.ResetColor();
                Console.Write("  ");
            }
        }

        // Auto-learn: screenshot + text on first encounter
        if (hasExpData)
        {
            var drawRect = new System.Drawing.Rectangle(
                btnRect.Left, btnRect.Top,
                btnRect.Right - btnRect.Left, btnRect.Bottom - btnRect.Top);
            expDb!.TouchControl(formId!, controlId!.Value, drawRect,
                wmText: WindowFinder.GetWindowText(hButton));
        }

        // Snapshot: foreground window before clicking
        var fgBefore = NativeMethods.GetForegroundWindow();
        bool isFirst = true;
        var failedStrategies = new List<string>();
        var buttonText = WindowFinder.GetWindowText(hButton);

        foreach (var strategyName in order)
        {
            if (!strategyActions.TryGetValue(strategyName, out var action))
                continue;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(isFirst ? $"@({cx},{cy}) {strategyName}" : $" → {strategyName}");
            Console.ResetColor();
            isFirst = false;

            action();
            bool success = CheckDialogGone(hDialogOrParent, fgBefore, strategyName);

            // Record result in experience DB
            if (hasExpData)
                expDb!.RecordClickStrategy(formId!, controlId!.Value, strategyName, success);

            if (success)
            {
                // Auto-record knowhow when earlier strategies failed (fallback occurred)
                if (hasExpData && failedStrategies.Count > 0)
                {
                    expDb!.AppendKnowhow(formId!, controlId!.Value,
                        "Click Strategy Fallback",
                        $"- **Control**: cid={controlId} ({buttonText})\n" +
                        $"- **Failed**: {string.Join(", ", failedStrategies)}\n" +
                        $"- **Succeeded**: {strategyName}\n" +
                        $"- **Lesson**: Use {strategyName} first for this control type\n");
                }
                if (hasExpData) expDb!.SaveAll();
                return true;
            }

            failedStrategies.Add(strategyName);
        }

        // Record overall failure + knowhow
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" ✗ (all strategies failed)");
        Console.ResetColor();
        if (hasExpData)
        {
            expDb!.AppendKnowhow(formId!, controlId!.Value,
                "Click Strategy Total Failure",
                $"- **Control**: cid={controlId} ({buttonText})\n" +
                $"- **All strategies failed**: {string.Join(", ", failedStrategies)}\n" +
                $"- **Possible cause**: window state, elevation, or control type mismatch\n");
            expDb!.SaveAll();
        }
        return false;
    }

    /// <summary>
    /// Robust check: did the dialog close or did a new modal appear?
    /// Waits, then triple-checks: IsWindow + IsWindowVisible + foreground change + new modal.
    /// </summary>
    private static bool CheckDialogGone(IntPtr hDialog, IntPtr fgBefore, string strategyName)
    {
        Thread.Sleep(400);

        // Check 1: Is the window handle still valid?
        bool hwndValid = NativeMethods.IsWindow(hDialog);
        bool hwndVisible = hwndValid && NativeMethods.IsWindowVisible(hDialog);

        // Check 2: Did the foreground window change? (new modal may have appeared)
        var fgNow = NativeMethods.GetForegroundWindow();
        bool fgChanged = fgNow != fgBefore && fgNow != hDialog;

        // Check 3: Re-verify after another short wait (avoid transient states)
        if (!hwndValid || !hwndVisible)
        {
            Thread.Sleep(200);
            hwndValid = NativeMethods.IsWindow(hDialog);
            hwndVisible = hwndValid && NativeMethods.IsWindowVisible(hDialog);
        }

        bool dialogGone = !hwndValid || !hwndVisible;
        bool newModalAppeared = fgChanged && fgNow != IntPtr.Zero;

        if (dialogGone)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" ✓ [{strategyName}: dialog closed]");
            Console.ResetColor();
            return true;
        }

        if (newModalAppeared)
        {
            // A new window appeared on top — the click probably worked and triggered something
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.GetWindowTextW(fgNow, sb, 256);
            string newTitle = sb.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" ✓ [{strategyName}: new modal] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\"{newTitle}\"");
            Console.ResetColor();
            return true;
        }

        return false; // no change detected, try next strategy
    }
}
