using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using FlaUI.UIA3;
using FlaUI.Core.Definitions;
using WKAppBot.Core.Runner;
using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: click command + SmartClickButton + CheckDialogGone (shared helpers)
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
        var windows = WindowFinder.FindWindows(title);
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
            // MCP/Eye mode: no process launch, signal only
            if (Program.IsMcpMode || Program.RunningInEye)
            {
                Console.Error.WriteLine($"[ELEVATION] Click: target pid={targetPid} elevated, MCP/Eye mode -- using WaitForAdminServer");
                if (ElevationHelper.WaitForAdminServer())
                    return ElevatedEyeClient.ExecuteViaProxy("click", args) == 0 ? 0 : 1;
                return 1; // elevation unavailable
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ! Target process (pid={targetPid}) is elevated (admin).");
            Console.WriteLine($"  ! This process is NOT elevated -> SendInput/SetCursorPos will be blocked by UIPI.");
            Console.ResetColor();

            // Auto-relaunch as admin
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  -> Re-launching as administrator...");
            Console.ResetColor();
            Console.Out.Flush();

            try
            {
                // Find the .exe (not .dll) -- dotnet publish creates both
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
                var proc = AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "AUTOMATION");
                proc?.WaitForExit();

                // Show the elevated process's output
                if (File.Exists(resultFile))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  -- Elevated process output --");
                    Console.ResetColor();
                    Console.Write(File.ReadAllText(resultFile));
                }

                return proc?.ExitCode ?? 1;
            }
            catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 1223) // user cancelled UAC
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  X UAC cancelled. Cannot click elevated app without admin privileges.");
                Console.ResetColor();
                return 1;
            }
        }

        if (weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  v Running elevated");
            Console.ResetColor();
            Console.WriteLine(" -- physical mouse input enabled");
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
            Console.WriteLine($"-- All Controls in [{targetFormId}] ({allControls.Count}) --------------------");
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
                Console.WriteLine($"-- ComboBoxes ({allCombos.Count}) --------------------");
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
                    Console.WriteLine($" -- {count} items (selected: {(curSel >= 0 ? curSel.ToString() : "none")})");
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
        Console.WriteLine($"-- Buttons in [{targetFormId}] ({allButtons.Count}) --------------------");
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

        // -- Process combo selections BEFORE button click --
        if (comboSelections.Count > 0 && allCombos.Count > 0)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-- ComboBox Selections --------------------");
            Console.ResetColor();

            foreach (var (comboIdx, itemIdx) in comboSelections)
            {
                if (comboIdx < 1 || comboIdx > allCombos.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  X Combo #{comboIdx} not found (have {allCombos.Count} combos)");
                    Console.ResetColor();
                    continue;
                }
                var combo = allCombos[comboIdx - 1].Info;
                int count = (int)NativeMethods.SendMessageW(combo.Handle, 0x0146 /*CB_GETCOUNT*/, IntPtr.Zero, IntPtr.Zero);
                if (itemIdx < 0 || itemIdx >= count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  X Combo #{comboIdx}: item [{itemIdx}] out of range (have {count} items)");
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
                Console.Write($"  v Combo #{comboIdx}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" -> [{itemIdx}] \"{itemText}\"");
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

        // Click the matched button -- if no button found, try UIA TabItem fallback
        if (matchedButton == null && buttonText != null)
        {
            // UIA TabItem fallback: search ALL matching forms for tab items
            var matchingForms = scanResult.Forms.Where(f => f.FormId == targetFormId).ToList();
            int tabsSelected = 0;

            try
            {
                using var automation = new UIA3Automation();
                var mainElement = automation.FromHandle(win.Handle);
                // Find ALL TabItem elements matching the text
                var allTabItems = mainElement.FindAllDescendants(cf =>
                    cf.ByControlType(ControlType.TabItem));

                foreach (var ti in allTabItems)
                {
                    if (ti.Name == null || !ti.Name.Contains(buttonText))
                        continue;

                    // Check if already selected
                    var selPattern = ti.Patterns.SelectionItem;
                    if (selPattern != null && selPattern.IsSupported && selPattern.Pattern.IsSelected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"  [TAB] \"{ti.Name}\" already selected -- skipping");
                        Console.ResetColor();
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  [TAB] Selecting \"{ti.Name}\"");
                    Console.ResetColor();

                    bool selected = false;
                    // Try UIA SelectionItem.Select first (focusless!)
                    if (selPattern != null && selPattern.IsSupported)
                    {
                        try
                        {
                            selPattern.Pattern.Select();
                            selected = true;
                        }
                        catch { /* UIA Select failed -- try click fallback */ }
                    }

                    // Fallback: PostMessage click on tab item coordinates
                    if (!selected)
                    {
                        try
                        {
                            var rect = ti.BoundingRectangle;
                            int tcx = (int)(rect.X + rect.Width / 2);
                            int tcy = (int)(rect.Y + rect.Height / 2);
                            // Find the parent Tab control handle
                            var parentTab = ti.Parent;
                            if (parentTab != null)
                            {
                                var ph = parentTab.Properties.NativeWindowHandle.ValueOrDefault;
                                if (ph != IntPtr.Zero)
                                {
                                    var parentRect = parentTab.BoundingRectangle;
                                    int localX = tcx - (int)parentRect.X;
                                    int localY = tcy - (int)parentRect.Y;
                                    IntPtr lParam = (IntPtr)((localY << 16) | (localX & 0xFFFF));
                                    NativeMethods.PostMessageW(ph, 0x0201 /*WM_LBUTTONDOWN*/, (IntPtr)0x0001, lParam);
                                    Thread.Sleep(50);
                                    NativeMethods.PostMessageW(ph, 0x0202 /*WM_LBUTTONUP*/, IntPtr.Zero, lParam);
                                    selected = true;
                                }
                            }
                        }
                        catch { /* click fallback also failed */ }
                    }

                    if (selected)
                    {
                        tabsSelected++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($" v (focusless!)");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" X FAIL");
                    }
                    Console.ResetColor();
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [TAB] UIA error: {ex.Message}");
                Console.ResetColor();
            }

            if (tabsSelected > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  v {tabsSelected} tab(s) \"{buttonText}\" selected across all forms");
                Console.ResetColor();
                return 0;
            }

            // No button AND no tab found
            Console.ForegroundColor = ConsoleColor.Red;
            var searchDesc = targetCid.HasValue ? $"cid={targetCid}" : $"\"{buttonText}\"";
            Console.WriteLine($"\n  Button/TabItem {searchDesc} not found in [{targetFormId}]");
            Console.ResetColor();
            return 1;
        }

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

            // -- Click Strategy --
            // Try message-based click first (no cursor movement).
            // If no reaction detected, fall back to physical mouse click.
            bool usePhysicalMouse = args.Contains("--mouse");
            string clickMethod;

            if (!usePhysicalMouse)
            {
                // Strategy 1: BM_CLICK (0x00F5) -- standard button click message
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
                // Strategy 3: Physical mouse (SendInput) -- guaranteed but moves cursor
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

            // -- Detect reaction --
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n-- Reaction Check --------------------");
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
                Console.Error.WriteLine($"[{popupInfo.ClassName}] \"{popupInfo.Title}\" ({popupInfo.Rect.Width}x{popupInfo.Rect.Height})");
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

            // 4. Check if button text changed (toggle buttons like 매매시작->매매중지)
            var postButtonText = WindowFinder.GetWindowText(matchedButton.Handle);
            if (postButtonText != preButtonText)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  🔄 Button text changed: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\"{preButtonText}\" -> \"{postButtonText}\"");
                Console.ResetColor();
                anyReaction = true;
            }

            // 5. Check if any MessageBox-like popup appeared (same process, any window)
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint clickTargetPid);
            var topWindows = WindowFinder.FindWindows(""); // all visible windows
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
                Console.WriteLine("  (no visible reaction detected -- button may require specific state)");
                Console.ResetColor();
            }
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
        Console.Write($"  v Auto-{label}: ");
        Console.ResetColor();

        SmartClickButton(buttons[0].Handle, hDialog);
    }

    /// <summary>
    /// Smart click: tries strategies in experience-optimized order, checking for window change after each.
    /// Default priority: no cursor movement first -> cursor movement last.
    /// With ExperienceDb: reorders by historical success rate.
    /// Detection: checks if dialog closed OR a new modal appeared.
    /// </summary>
    private static bool SmartClickButton(
        IntPtr hButton, IntPtr hDialogOrParent,
        ExperienceDb? expDb = null, string? formId = null, int? controlId = null,
        bool showZoom = true)
    {
        NativeMethods.GetWindowRect(hButton, out var btnRect);
        int cx = (btnRect.Left + btnRect.Right) / 2;
        int cy = (btnRect.Top + btnRect.Bottom) / 2;
        int localX = (btnRect.Right - btnRect.Left) / 2;
        int localY = (btnRect.Bottom - btnRect.Top) / 2;

        // [ZOOM] Start adaptive overlay for the button
        var buttonLabel = WindowFinder.GetWindowText(hButton);
        ClickZoomHelper? zoom = null;
        if (showZoom)
        {
            zoom = ClickZoomHelper.Begin(hButton, hDialogOrParent,
                "click", string.IsNullOrEmpty(buttonLabel) ? $"cid={controlId}" : buttonLabel);
        }

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
                WKAppBot.Win32.Input.InputReadiness.ReadinessCalled = true; // automation command -- user-invoked
                NativeMethods.SmartSetForegroundWindow(hDialogOrParent); // [FOCUS-GUARD] CheckActiveGuard 적용
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
                Console.Write(string.Join(" -> ", parts));
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

        // Show existing knowhow before clicking (오답노트 펴보기)
        if (hasExpData)
        {
            var knowhow = expDb!.ReadKnowhow(formId!, controlId!.Value);
            if (knowhow != null)
            {
                // Count entries (## headers)
                var entryCount = knowhow.Split('\n')
                    .Count(l => l.StartsWith("## ["));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[KNOWHOW] ");
                Console.ResetColor();
                Console.WriteLine($"cid={controlId}: {entryCount} knowhow entries found");
                // Show compact summary (first line of each entry)
                foreach (var line in knowhow.Split('\n')
                    .Where(l => l.StartsWith("- **")))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"  {line}");
                    Console.ResetColor();
                }
            }
        }

        // Snapshot: foreground window before clicking
        var fgBefore = NativeMethods.GetForegroundWindow();
        bool isFirst = true;
        var failedStrategies = new List<string>();
        var buttonText = WindowFinder.GetWindowText(hButton);

        zoom?.UpdateStatus($"Clicking: \"{buttonText}\"");

        foreach (var strategyName in order)
        {
            if (!strategyActions.TryGetValue(strategyName, out var action))
                continue;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(isFirst ? $"@({cx},{cy}) {strategyName}" : $" -> {strategyName}");
            Console.ResetColor();
            isFirst = false;

            zoom?.UpdateStatus($"Try: {strategyName}");

            action();
            bool success = CheckDialogGone(hDialogOrParent, fgBefore, strategyName);

            // Record result in experience DB
            if (hasExpData)
                expDb!.RecordClickStrategy(formId!, controlId!.Value, strategyName, success);

            if (success)
            {
                // [ZOOM] Show success
                zoom?.ShowPass($"{strategyName}: dialog closed");

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

        // [ZOOM] Show failure
        zoom?.ShowFail("all strategies failed");

        // Record overall failure + knowhow
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" X (all strategies failed)");
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
            Console.WriteLine($" v [{strategyName}: dialog closed]");
            Console.ResetColor();
            return true;
        }

        if (newModalAppeared)
        {
            // A new window appeared on top -- the click probably worked and triggered something
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.GetWindowTextW(fgNow, sb, 256);
            string newTitle = sb.ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" v [{strategyName}: new modal] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\"{newTitle}\"");
            Console.ResetColor();
            return true;
        }

        return false; // no change detected, try next strategy
    }
}
