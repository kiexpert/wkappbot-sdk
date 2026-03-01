using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Condition search automation for [0150] form.
    /// Focusless workflow: tab-switch → search → double-click → add indicator.
    ///
    /// Usage:
    ///   wkappbot cond-add "영웅문4" "시가총액"
    ///   wkappbot cond-add "영웅문4" "시가총액" --dry-run
    ///   wkappbot cond-add "영웅문4" --new "오늘의 주도주"
    ///   wkappbot cond-add "영웅문4" --formula "(A and B and C)"
    ///   wkappbot cond-add "영웅문4" --search               (search and show results)
    ///   wkappbot cond-add "영웅문4" --run                   (click 검색 button)
    ///   wkappbot cond-add "영웅문4" --save "오늘의 주도주"  (save condition with name)
    /// </summary>
    static int CondAddCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot cond-add <window-title> <indicator-keyword>");
            Console.WriteLine("       wkappbot cond-add <window-title> --new \"name\"");
            Console.WriteLine("       wkappbot cond-add <window-title> --formula \"(A and B)\"");
            Console.WriteLine("       wkappbot cond-add <window-title> --run");
            Console.WriteLine("       wkappbot cond-add <window-title> --save \"name\"");
            Console.WriteLine("       wkappbot cond-add <window-title> --select A|B|C");
            Console.WriteLine("       wkappbot cond-add <window-title> --modify");
            Console.WriteLine("       wkappbot cond-add <window-title> --list");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  <keyword>         Search and add indicator to condition");
            Console.WriteLine("  --new <name>      Create new condition with name");
            Console.WriteLine("  --formula <expr>  Set condition formula");
            Console.WriteLine("  --run             Execute search (click 검색 button)");
            Console.WriteLine("  --save <name>     Save condition with name");
            Console.WriteLine("  --select <A-Z>    Select condition row (PostMessage click)");
            Console.WriteLine("  --modify          Click 수정 button (UIA Invoke, focusless)");
            Console.WriteLine("  --delete          Delete selected condition row (X button, focusless)");
            Console.WriteLine("  --list            Show current condition items via OCR");
            Console.WriteLine("  --dry-run         Just search, don't double-click");
            Console.WriteLine("  --force-focusless Block all focus-stealing ops (error if needed)");
            return 1;
        }

        var title = args[0];
        var newName = GetArgValue(args, "--new");
        var formula = GetArgValue(args, "--formula");
        var saveName = GetArgValue(args, "--save");
        var selectRow = GetArgValue(args, "--select");
        var dryRun = args.Contains("--dry-run");
        var doRun = args.Contains("--run");
        var doModify = args.Contains("--modify");
        var doDelete = args.Contains("--delete");
        var doList = args.Contains("--list");
        var forceFocusless = args.Contains("--force-focusless");

        // --force-focusless: globally block all focus-stealing operations
        if (forceFocusless)
        {
            FocuslessGuard.Enabled = true;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[FOCUSLESS] Guard enabled — SetCursorPos/SendInput/SetForeground blocked");
            Console.ResetColor();
        }

        // Find window
        var matches = WindowFinder.FindByTitle(title);
        if (matches.Count == 0) return Error($"Window not found: {title}");
        var mainHwnd = matches[0].Handle;
        Console.WriteLine($"Window: [{mainHwnd:X8}] \"{matches[0].Title}\"");

        // Initialize UIA
        var automation = new UIA3Automation();
        automation.ConnectionTimeout = TimeSpan.FromSeconds(5);
        automation.TransactionTimeout = TimeSpan.FromSeconds(5);
        var root = automation.FromHandle(mainHwnd);

        // Find [0150] form
        AutomationElement? form = null;
        foreach (var w in root.FindAllDescendants(x => x.ByControlType(ControlType.Window)))
        {
            var name = w.Name ?? "";
            if (name.Contains("[0150]"))
            {
                form = w;
                break;
            }
        }
        if (form == null) return Error("[0150] 조건검색 form not found. Open it first.");
        Console.WriteLine($"Form: \"{form.Name}\"");

        // Knowhow broadcast: show knowhow for [0150] form
        BroadcastInspectKnowhow(mainHwnd, matches[0].ClassName, "0150", form.Name);

        // --new: create new condition
        if (newName != null)
            return CondNew(form, automation, newName);

        // --formula: set formula
        if (formula != null)
            return CondSetFormula(form, formula);

        // --run: click search button
        if (doRun)
            return CondRunSearch(form);

        // --select: click condition row (A/B/C...)
        if (selectRow != null)
            return CondSelectRow(form, selectRow.ToUpper());

        // --modify: click 수정 button
        if (doModify)
            return CondClickModify(form);

        // --delete: click X button to delete selected condition
        if (doDelete)
            return CondClickDelete(form);

        // --list: show current conditions via OCR
        if (doList)
            return CondListItems(form);

        // --save: save condition
        if (saveName != null)
            return CondSave(form, saveName);

        // Default: add indicator by keyword
        var keyword = args.Length > 1 && !args[1].StartsWith("--") ? args[1] : null;
        if (keyword == null)
            return Error("Provide indicator keyword or use --new/--formula/--run/--save");

        return CondAddIndicator(form, automation, mainHwnd, keyword, dryRun);
    }

    /// <summary>Create new blank condition + set name</summary>
    static int CondNew(AutomationElement form, UIA3Automation automation, string name)
    {
        // Click "조건식 새로작성" (aid=4026)
        var btn = FindByAid(form, "4026");
        if (btn == null) return Error("Button 조건식 새로작성 (aid=4026) not found");

        Console.WriteLine("Clicking '조건식 새로작성'...");
        var invoked = UiaLocator.TryInvoke(btn);
        Console.WriteLine($"  Invoke: {invoked} (focusless!)");
        if (!invoked) return Error("Failed to invoke 조건식 새로작성");
        Thread.Sleep(500);

        // Set name in the condition name field (aid=4025 → Pane, need to find its child or use WM_SETTEXT)
        // aid=4029 has a Document(aid=1) child for name editing
        var namePane = FindByAid(form, "4029");
        if (namePane != null)
        {
            var nameDoc = namePane.FindFirstChild(x => x.ByControlType(ControlType.Document));
            if (nameDoc != null)
            {
                Console.WriteLine($"Setting condition name: \"{name}\"");
                try
                {
                    var valPat = nameDoc.Patterns.Value;
                    if (valPat.IsSupported)
                    {
                        valPat.Pattern.SetValue(name);
                        Console.WriteLine("  Value.SetValue: OK (focusless!)");
                    }
                    else
                    {
                        Console.WriteLine("  Value pattern not supported, trying WM_SETTEXT...");
                        SetTextViaWin32(nameDoc, name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  SetValue failed: {ex.Message}");
                    SetTextViaWin32(nameDoc, name);
                }
            }
        }

        return 0;
    }

    /// <summary>Set the condition formula (aid=10009)</summary>
    static int CondSetFormula(AutomationElement form, string formula)
    {
        var formulaDoc = FindByAid(form, "10009");
        if (formulaDoc == null) return Error("Formula editor (aid=10009) not found");

        Console.WriteLine($"Setting formula: \"{formula}\"");
        try
        {
            var valPat = formulaDoc.Patterns.Value;
            if (valPat.IsSupported)
            {
                valPat.Pattern.SetValue(formula);
                Console.WriteLine("  Value.SetValue: OK (focusless!)");

                // Read back
                Thread.Sleep(200);
                var readBack = valPat.Pattern.Value.Value;
                Console.WriteLine($"  ReadBack: \"{readBack}\"");
                return 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  SetValue failed: {ex.Message}");
        }

        // Fallback: WM_SETTEXT
        SetTextViaWin32(formulaDoc, formula);
        return 0;
    }

    /// <summary>Click 검색 button (aid=4035)</summary>
    static int CondRunSearch(AutomationElement form)
    {
        var btn = FindByAid(form, "4035");
        if (btn == null) return Error("Button 검색 (aid=4035) not found");

        Console.WriteLine("Clicking '검색'...");
        var invoked = UiaLocator.TryInvoke(btn);
        Console.WriteLine($"  Invoke: {invoked} (focusless!)");
        return invoked ? 0 : 1;
    }

    /// <summary>Save condition: set name + click 내조건식 저장</summary>
    static int CondSave(AutomationElement form, string name)
    {
        // First set the name in the name edit (aid=4029)
        var namePane = FindByAid(form, "4029");
        if (namePane != null)
        {
            var nameDoc = namePane.FindFirstChild(x => x.ByControlType(ControlType.Document));
            if (nameDoc != null)
            {
                Console.WriteLine($"Setting condition name: \"{name}\"");
                try
                {
                    nameDoc.Patterns.Value.Pattern.SetValue(name);
                    Console.WriteLine("  Value.SetValue: OK (focusless!)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  SetValue failed: {ex.Message}, trying WM_SETTEXT");
                    SetTextViaWin32(nameDoc, name);
                }
            }
        }

        Thread.Sleep(300);

        // Click "내조건식 저장" (aid=4030)
        var btn = FindByAid(form, "4030");
        if (btn == null) return Error("Button 내조건식 저장 (aid=4030) not found");

        Console.WriteLine("Clicking '내조건식 저장'...");
        var invoked = UiaLocator.TryInvoke(btn);
        Console.WriteLine($"  Invoke: {invoked} (focusless!)");
        return invoked ? 0 : 1;
    }

    /// <summary>
    /// Add indicator by keyword:
    /// 1. Switch to 조건식 tab
    /// 2. Type keyword in search edit
    /// 3. Click search button
    /// 4. Wait for tree to jump
    /// 5. Double-click matching item
    /// </summary>
    static int CondAddIndicator(AutomationElement form, UIA3Automation automation, IntPtr mainHwnd,
        string keyword, bool dryRun)
    {
        Console.WriteLine($"\n=== Adding indicator: \"{keyword}\" ===\n");

        // Step 1: Switch to 조건식 tab (Tab1, index 0)
        Console.Write("[1/5] Switching to 조건식 tab... ");
        var tab = FindByAid(form, "3019");
        if (tab == null) return Error("Tab control (aid=3019) not found");

        var tabItems = tab.FindAllChildren(x => x.ByControlType(ControlType.TabItem));
        if (tabItems.Length == 0) return Error("No TabItems found");

        // Tab1 = index 0 = 조건식
        var condTab = tabItems[0];
        var isSelected = UiaLocator.IsSelected(condTab);
        if (isSelected != true)
        {
            UiaLocator.TrySelect(condTab);
            Thread.Sleep(300);
        }
        Console.WriteLine("OK (focusless!)");

        // Step 2: Type keyword in search edit
        Console.Write($"[2/5] Typing \"{keyword}\" in search edit... ");
        var searchPane = FindByAid(form, "3000");
        if (searchPane == null) return Error("Search pane (aid=3000) not found");

        // Find the Edit/Document child
        AutomationElement? searchEdit = null;
        // Try Document first
        searchEdit = searchPane.FindFirstChild(x => x.ByControlType(ControlType.Document));
        if (searchEdit == null)
            searchEdit = searchPane.FindFirstChild(); // any child

        if (searchEdit == null) return Error("Search edit not found under aid=3000");

        // Set text via Value pattern (focusless!)
        bool textSet = false;
        try
        {
            var valPat = searchEdit.Patterns.Value;
            if (valPat.IsSupported)
            {
                valPat.Pattern.SetValue(keyword);
                textSet = true;
                Console.WriteLine("OK (Value.SetValue, focusless!)");
            }
        }
        catch { }

        if (!textSet)
        {
            // Fallback: WM_SETTEXT
            Console.Write("(WM_SETTEXT fallback) ");
            SetTextViaWin32(searchEdit, keyword);
            Console.WriteLine("OK");
        }

        // Step 3: Press Enter in search edit to trigger search
        Console.Write("[3/5] Pressing Enter to search... ");
        IntPtr editHwnd = IntPtr.Zero;
        try
        {
            var hwndVal = searchEdit.Properties.NativeWindowHandle.ValueOrDefault;
            if (hwndVal != 0)
                editHwnd = new IntPtr((long)hwndVal);
        }
        catch { }

        if (editHwnd == IntPtr.Zero)
        {
            // Fallback: click search button (aid=3001)
            Console.Write("(no hWnd, clicking button) ");
            var searchBtn = FindByAid(form, "3001");
            if (searchBtn != null) UiaLocator.TryInvoke(searchBtn);
            Console.WriteLine("OK (button fallback)");
        }
        else
        {
            // Send VK_RETURN via PostMessage to the Edit (focusless!)
            const int VK_RETURN = 0x0D;
            uint scanCode = NativeMethods.MapVirtualKeyW((uint)VK_RETURN, 0);
            uint lParamDown = 1u | (scanCode << 16);
            uint lParamUp = 1u | (scanCode << 16) | (3u << 30);

            NativeMethods.PostMessageW(editHwnd, NativeMethods.WM_KEYDOWN, (IntPtr)VK_RETURN, (IntPtr)lParamDown);
            Thread.Sleep(30);
            NativeMethods.PostMessageW(editHwnd, NativeMethods.WM_KEYUP, (IntPtr)VK_RETURN, (IntPtr)lParamUp);
            Console.WriteLine($"OK (PostMessage VK_RETURN to {editHwnd:X8}, focusless!)");
        }

        // Step 4: Wait for tree to update
        Console.Write("[4/5] Waiting for tree update... ");
        Thread.Sleep(800);
        Console.WriteLine("OK");

        // Step 5: Double-click the matching item in the tree (aid=10001)
        if (dryRun)
        {
            Console.WriteLine("[5/5] --dry-run: skipping double-click");
            // Take OCR of the tree area to see results
            Console.WriteLine("\nCapturing tree area for verification...");
            CaptureAndOcrTreeArea(form, mainHwnd);
            return 0;
        }

        Console.WriteLine("[5/5] Double-clicking tree item...");
        var treePaneEl = FindByAid(form, "10001");
        if (treePaneEl == null) return Error("Tree pane (aid=10001) not found");

        var treeRect = treePaneEl.BoundingRectangle;

        // Get tree hWnd (IMPORTANT: must get FRESH hWnd after tab switch — MFC recreates children!)
        IntPtr treeHwnd = IntPtr.Zero;
        try
        {
            var hwndVal = treePaneEl.Properties.NativeWindowHandle.ValueOrDefault;
            if (hwndVal != 0) treeHwnd = new IntPtr((long)hwndVal);
        }
        catch { }

        if (treeHwnd == IntPtr.Zero)
        {
            // WindowFromPoint at multiple positions to find the actual control
            for (int probeY = 10; probeY < 100; probeY += 30)
            {
                treeHwnd = NativeMethods.WindowFromPoint(
                    new POINT { X = treeRect.Left + 30, Y = treeRect.Top + probeY });
                if (treeHwnd != IntPtr.Zero) break;
            }
        }
        if (treeHwnd == IntPtr.Zero) return Error("Could not find tree hWnd");

        int treeCid = NativeMethods.GetDlgCtrlID(treeHwnd);
        Console.WriteLine($"  Tree hWnd: {treeHwnd:X8} (cid={treeCid}), Rect: ({treeRect.Left},{treeRect.Top} {treeRect.Width}x{treeRect.Height})");

        // Find keyword position via OCR on the tree area
        var (clickX, clickY) = FindItemCoordsByOcr(treeHwnd, treeRect, keyword);
        if (clickX < 0)
        {
            // OCR couldn't find exact position — use heuristic: center-x, top area
            clickX = treeRect.Width / 3;  // ~1/3 from left (past icons)
            clickY = 16;  // near top, first visible item
            Console.WriteLine($"  OCR: keyword not found precisely, using heuristic ({clickX},{clickY})");
        }
        else
        {
            Console.WriteLine($"  OCR: found \"{keyword}\" at client ({clickX},{clickY})");
        }

        // === Try Approach 0A: Keyboard select + UIA add button (fully focusless!) ===
        // User insight: tree selection (no dblclk) shows params → click + button to add
        Console.WriteLine("  [Approach 0A] Keyboard select → UIA + button (aid=4009)...");
        TryKeyboardSelect(treeHwnd);
        Thread.Sleep(500);

        // Click + button (aid=4009) via UIA Invoke — fully focusless
        var addBtn = FindByAid(form, "4009");
        if (addBtn != null)
        {
            try
            {
                var inv = addBtn.Patterns.Invoke;
                if (inv.IsSupported)
                {
                    inv.Pattern.Invoke();
                    Console.WriteLine("  + button (aid=4009) invoked!");
                    Thread.Sleep(800);

                    bool added0a = CheckIndicatorAdded(form, keyword);
                    if (added0a)
                    {
                        Console.WriteLine($"\n  ✓ Indicator \"{keyword}\" added via select+add! (FULLY FOCUSLESS!)");
                        return 0;
                    }
                    Console.WriteLine("  Approach 0A: + button clicked but indicator not detected.");
                }
                else Console.WriteLine("  + button Invoke not supported.");
            }
            catch (Exception ex) { Console.WriteLine($"  + button error: {ex.Message}"); }
        }
        else Console.WriteLine("  + button (aid=4009) not found.");

        // === Try Approach 0B: Keyboard VK_RETURN (double-click equivalent, fully focusless!) ===
        Console.WriteLine("  [Approach 0B] Keyboard VK_RETURN (dblclk equivalent)...");
        PostKey(treeHwnd, 0x0D); // VK_RETURN
        Thread.Sleep(200);
        PostKey(treeHwnd, 0x20); // VK_SPACE (some controls use space)
        Thread.Sleep(800);

        bool added = CheckIndicatorAdded(form, keyword);
        if (added)
        {
            Console.WriteLine($"\n  ✓ Indicator \"{keyword}\" added via keyboard RETURN! (FULLY FOCUSLESS!)");
            return 0;
        }
        Console.WriteLine("  Approach 0B (keyboard RETURN) didn't work.");

        // === Try Approach 1: PostMessage-based double-click ===
        TryDoubleClickTreeItem(treeHwnd, clickX, clickY);
        Thread.Sleep(800);

        added = CheckIndicatorAdded(form, keyword);
        if (added)
        {
            Console.WriteLine($"\n  ✓ Indicator \"{keyword}\" added successfully!");
            return 0;
        }

        Console.WriteLine("  Approach 1 didn't add the indicator.");

        // === Try Approach 2: VK_RETURN to tree (keyboard activation) ===
        Console.WriteLine("  [Approach 2] Sending VK_RETURN to tree...");
        {
            const int VK_RETURN = 0x0D;
            uint scanCode = NativeMethods.MapVirtualKeyW((uint)VK_RETURN, 0);
            uint lDown = 1u | (scanCode << 16);
            uint lUp = 1u | (scanCode << 16) | (3u << 30);
            NativeMethods.PostMessageW(treeHwnd, NativeMethods.WM_KEYDOWN, (IntPtr)VK_RETURN, (IntPtr)lDown);
            Thread.Sleep(30);
            NativeMethods.PostMessageW(treeHwnd, NativeMethods.WM_KEYUP, (IntPtr)VK_RETURN, (IntPtr)lUp);
        }
        Thread.Sleep(800);

        added = CheckIndicatorAdded(form, keyword);
        if (added)
        {
            Console.WriteLine($"\n  ✓ Indicator \"{keyword}\" added via VK_RETURN!");
            return 0;
        }
        Console.WriteLine("  Approach 2 didn't add the indicator.");

        // === Try Approach 3: SendInput actual double-click (steals focus!) ===
        Console.WriteLine("  Falling back to SendInput (will briefly steal focus)...");
        TryDoubleClickSendInput(treeHwnd, clickX, clickY);
        Thread.Sleep(800);

        added = CheckIndicatorAdded(form, keyword);
        if (added)
        {
            Console.WriteLine($"\n  ✓ Indicator \"{keyword}\" added via SendInput!");
            return 0;
        }

        Console.WriteLine($"\n  ✗ All approaches failed. Capturing form for diagnosis...");
        CaptureAndOcrTreeArea(form, mainHwnd);

        return 0;
    }

    /// <summary>
    /// Keyboard-only tree selection (fully focusless!).
    /// After search filters the tree, send keyboard messages to SELECT first item.
    /// Selection shows parameters in right panel — then + button (aid=4009) adds it.
    ///
    /// Sequence: WM_SETFOCUS → VK_HOME (first item) → VK_DOWN (skip header if needed)
    /// All via PostMessage — no cursor movement, no foreground change.
    /// </summary>
    static bool TryKeyboardSelect(IntPtr treeHwnd)
    {
        const uint WM_SETFOCUS = 0x0007;
        const int VK_HOME = 0x24;
        const int VK_DOWN = 0x28;

        // 1. Set internal focus (no foreground change)
        NativeMethods.SendMessageW(treeHwnd, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
        Thread.Sleep(50);

        // 2. VK_HOME → jump to first item in tree
        PostKey(treeHwnd, VK_HOME);
        Thread.Sleep(100);

        // 3. VK_DOWN → move to first child/result item (in case HOME lands on category header)
        PostKey(treeHwnd, VK_DOWN);
        Thread.Sleep(100);

        Console.WriteLine($"  Sent: FOCUS→HOME→DOWN to {treeHwnd:X8} (select only, no cursor move!)");
        return true;
    }

    /// <summary>Post a key down+up sequence via PostMessage (focusless).</summary>
    static void PostKey(IntPtr hWnd, int vk)
    {
        uint scanCode = NativeMethods.MapVirtualKeyW((uint)vk, 0);
        uint lDown = 1u | (scanCode << 16);
        uint lUp = 1u | (scanCode << 16) | (3u << 30); // bit30=previous, bit31=transition
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_KEYDOWN, (IntPtr)vk, (IntPtr)lDown);
        Thread.Sleep(30);
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_KEYUP, (IntPtr)vk, (IntPtr)lUp);
    }

    /// <summary>
    /// Multi-approach double-click for custom MFC tree control.
    /// MFC custom controls often call GetCursorPos() internally, so we need to
    /// move the actual cursor before sending messages.
    ///
    /// Approaches (tried in order):
    /// 1. SetCursorPos + WM_SETFOCUS + PostMessage double-click sequence
    /// 2. SetCursorPos + WM_PARENTNOTIFY to parent (simulates child notification)
    /// 3. SendInput actual double-click (last resort, brief focus steal)
    /// </summary>
    static bool TryDoubleClickTreeItem(IntPtr hWnd, int clientX, int clientY)
    {
        const uint WM_LBUTTONDBLCLK = 0x0203;
        const uint WM_SETFOCUS = 0x0007;
        uint lParam = (uint)((clientY << 16) | (clientX & 0xFFFF));

        // Convert client coordinates to screen coordinates for SetCursorPos
        var pt = new POINT { X = clientX, Y = clientY };
        NativeMethods.ClientToScreen(hWnd, ref pt);
        Console.WriteLine($"  Tree hWnd={hWnd:X8}, client=({clientX},{clientY}), screen=({pt.X},{pt.Y})");

        // === Approach 1: SetCursorPos + WM_SETFOCUS + PostMessage ===
        Console.WriteLine("  [Approach 1] SetCursorPos + WM_SETFOCUS + PostMessage dblclk...");

        // FocuslessGuard check
        if (FocuslessGuard.IsBlocked("SetCursorPos (MFC tree GetCursorPos)"))
            return false;

        // Save + move physical cursor (MFC checks GetCursorPos!)
        NativeMethods.GetCursorPos(out var savedCursor);
        NativeMethods.SetCursorPos(pt.X, pt.Y);
        Thread.Sleep(30);

        // Send WM_SETFOCUS so control thinks it has internal focus
        // (doesn't steal foreground window — just internal MFC focus)
        NativeMethods.SendMessageW(hWnd, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
        Thread.Sleep(30);

        // WM_MOUSEMOVE to update internal hover/tracking state
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_MOUSEMOVE, IntPtr.Zero, (IntPtr)lParam);
        Thread.Sleep(50);

        // Full double-click sequence via PostMessage (queued, natural processing)
        // First click
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)0x0001, (IntPtr)lParam);
        Thread.Sleep(80);
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);
        Thread.Sleep(100);
        // Second click = double-click
        NativeMethods.PostMessageW(hWnd, WM_LBUTTONDBLCLK, (IntPtr)0x0001, (IntPtr)lParam);
        Thread.Sleep(80);
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, (IntPtr)lParam);

        // Restore cursor
        NativeMethods.SetCursorPos(savedCursor.X, savedCursor.Y);
        Console.WriteLine($"  Sent: MOVE→FOCUS→DOWN→UP→DBLCLK→UP at ({clientX},{clientY}) (cursor restored)");
        return true;
    }

    /// <summary>
    /// SendInput-based actual double-click (last resort, steals focus briefly).
    /// </summary>
    static bool TryDoubleClickSendInput(IntPtr hWnd, int clientX, int clientY)
    {
        var pt = new POINT { X = clientX, Y = clientY };
        NativeMethods.ClientToScreen(hWnd, ref pt);

        Console.WriteLine($"  [Approach 3] SendInput double-click at screen ({pt.X},{pt.Y})...");

        // Bring window to front briefly
        NativeMethods.SetForegroundWindow(hWnd);
        Thread.Sleep(100);

        // Move cursor
        WKAppBot.Win32.Input.MouseInput.MoveTo(pt.X, pt.Y);
        Thread.Sleep(50);

        // Double-click via SendInput
        WKAppBot.Win32.Input.MouseInput.DoubleClick(pt.X, pt.Y);
        Console.WriteLine("  SendInput double-click sent (focus was stolen!)");
        return true;
    }

    /// <summary>
    /// Find item coordinates in the tree by OCR.
    /// Returns (clientX, clientY) relative to the tree control, or (-1,-1) if not found.
    /// </summary>
    static (int x, int y) FindItemCoordsByOcr(IntPtr treeHwnd, System.Drawing.Rectangle treeRect, string keyword)
    {
        try
        {
            // Capture tree area
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(
                treeRect.Left, treeRect.Top, treeRect.Width, Math.Min(treeRect.Height, 400));
            if (bmp == null) return (-1, -1);

            // OCR
            using var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
            var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
            if (ocrResult == null) return (-1, -1);

            // Search for keyword in OCR words — multi-tier matching
            // MFC bitmap font OCR is imprecise (총→출, 액→팩, etc.)
            // Tier 1: exact/contains match
            foreach (var word in ocrResult.Words)
            {
                if (word.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    keyword.Contains(word.Text, StringComparison.OrdinalIgnoreCase))
                {
                    int cx = word.X + word.Width / 2;
                    int cy = word.Y + word.Height / 2;
                    Console.WriteLine($"  OCR match: \"{word.Text}\" at bitmap ({word.X},{word.Y} {word.Width}x{word.Height})");
                    return (cx, cy);
                }
            }

            // Tier 2: Korean prefix match (first 2 chars — usually reliable)
            string keyPrefix = keyword.Length >= 2 ? keyword.Substring(0, 2) : keyword;
            foreach (var word in ocrResult.Words)
            {
                if (word.Text.Length >= 2 && word.Text.Substring(0, Math.Min(2, word.Text.Length)) == keyPrefix)
                {
                    int cx = word.X + word.Width / 2;
                    int cy = word.Y + word.Height / 2;
                    Console.WriteLine($"  OCR prefix: \"{word.Text}\" (prefix=\"{keyPrefix}\") at bitmap ({word.X},{word.Y} {word.Width}x{word.Height})");
                    return (cx, cy);
                }
            }

            // Tier 3: char overlap ratio ≥ 50%
            var keyChars = keyword.Where(c => !char.IsWhiteSpace(c)).ToArray();
            foreach (var word in ocrResult.Words)
            {
                var wordChars = word.Text.Where(c => !char.IsWhiteSpace(c)).ToArray();
                if (wordChars.Length < 2) continue;
                int commonCount = keyChars.Count(c => wordChars.Contains(c));
                float ratio = (float)commonCount / keyChars.Length;
                if (ratio >= 0.5f && commonCount >= 2)
                {
                    int cx = word.X + word.Width / 2;
                    int cy = word.Y + word.Height / 2;
                    Console.WriteLine($"  OCR fuzzy: \"{word.Text}\" ({commonCount}/{keyChars.Length}={ratio:P0}) at bitmap ({word.X},{word.Y})");
                    return (cx, cy);
                }
            }

            Console.WriteLine($"  OCR: no match for \"{keyword}\" in {ocrResult.Words.Count} words");
            // Debug: print first few words
            foreach (var w in ocrResult.Words.Take(10))
                Console.WriteLine($"    [{w.X},{w.Y}] \"{w.Text}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  OCR error: {ex.Message}");
        }
        return (-1, -1);
    }

    /// <summary>
    /// Check if the indicator was added to the right panel grid.
    /// Checks: formula editor text (aid=10009) + grid content OCR (aid=4041).
    /// MFC grid has: column headers "지표 | 내용" + data rows like "A 시가총액 | ..."
    /// </summary>
    static bool CheckIndicatorAdded(AutomationElement form, string keyword)
    {
        try
        {
            // Method 1: Check formula editor (aid=10009) for any content
            var formulaDoc = FindByAid(form, "10009");
            if (formulaDoc != null)
            {
                try
                {
                    var valPat = formulaDoc.Patterns.Value;
                    if (valPat.IsSupported)
                    {
                        var text = valPat.Pattern.Value.Value ?? "";
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            Console.WriteLine($"  Formula: \"{text}\"");
                            return true; // Something was added to the formula
                        }
                    }
                }
                catch { }
            }

            // Method 2: OCR the indicator grid (지표/내용 grid)
            // The grid is below the header area with "지표 | 내용" columns
            // When indicators are added, rows appear like "A | 시가총액 | ..."
            var gridPane = FindByAid(form, "4041");
            if (gridPane != null)
            {
                var gridRect = gridPane.BoundingRectangle;
                if (gridRect.Width > 10 && gridRect.Height > 10)
                {
                    using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(
                        gridRect.Left, gridRect.Top, gridRect.Width, Math.Min(gridRect.Height, 200));
                    if (bmp != null)
                    {
                        using var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                        var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                        if (ocrResult != null)
                        {
                            Console.WriteLine($"  Grid OCR: \"{ocrResult.FullText}\"");
                            // Check if keyword (or prefix) appeared in grid (below headers)
                            string keyPrefix = keyword.Length >= 2 ? keyword.Substring(0, 2) : keyword;
                            bool hasKeyword = ocrResult.FullText.Contains(keyword, StringComparison.OrdinalIgnoreCase);
                            bool hasPrefix = ocrResult.Words.Any(w => w.Text.StartsWith(keyPrefix));
                            // Check for indicator labels like "A", "B", "C" appearing
                            bool hasLabel = ocrResult.Words.Any(w =>
                                w.Text == "A" || w.Text == "B" || w.Text == "C" ||
                                w.Text == "D" || w.Text == "E");
                            if (hasKeyword || hasPrefix || hasLabel)
                            {
                                Console.WriteLine($"  Grid detected indicator! (keyword={hasKeyword}, prefix={hasPrefix}, label={hasLabel})");
                                return true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Verify error: {ex.Message}");
        }
        return false;
    }

    /// <summary>Capture and OCR the tree area (aid=10001) for verification</summary>
    static void CaptureAndOcrTreeArea(AutomationElement form, IntPtr mainHwnd)
    {
        var treePane = FindByAid(form, "10001");
        if (treePane == null) return;

        var rect = treePane.BoundingRectangle;
        // Capture just the top portion (first few items)
        var captureRect = new System.Drawing.Rectangle(rect.Left, rect.Top, rect.Width, Math.Min(rect.Height, 200));

        try
        {
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureScreenRegion(
                captureRect.X, captureRect.Y, captureRect.Width, captureRect.Height);
            if (bmp != null)
            {
                var path = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq", "output", "screenshots", "cond_tree.png");
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"  Tree screenshot: {path}");

                // OCR
                using var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                var ocrResult = ocr.RecognizeAll(bmp).GetAwaiter().GetResult();
                if (ocrResult != null)
                {
                    Console.WriteLine($"  OCR text: {ocrResult.FullText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Screenshot/OCR failed: {ex.Message}");
        }
    }

    /// <summary>Find UIA element by AutomationId in descendants</summary>
    static AutomationElement? FindByAid(AutomationElement parent, string aid)
    {
        try
        {
            var all = parent.FindAllDescendants();
            foreach (var e in all)
            {
                if (e.Properties.AutomationId.ValueOrDefault == aid)
                    return e;
            }
        }
        catch { }
        return null;
    }

    /// <summary>Set text via Win32 WM_SETTEXT (focusless)</summary>
    static void SetTextViaWin32(AutomationElement element, string text)
    {
        try
        {
            var hwndVal = element.Properties.NativeWindowHandle.ValueOrDefault;
            if (hwndVal != 0)
            {
                var hWnd = new IntPtr((long)hwndVal);
                NativeMethods.SendMessageW(hWnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
                Console.WriteLine($"  WM_SETTEXT to hWnd {hWnd:X8}: OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  WM_SETTEXT failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Select condition row (A/B/C...) by clicking in the condition table.
    /// The table (aid=4015) is owner-drawn — no UIA children per row.
    /// Each row is ~20px tall, starting from top. A=row0, B=row1, C=row2...
    /// Uses PostMessage WM_LBUTTONDOWN/UP for focusless click.
    /// </summary>
    static int CondSelectRow(AutomationElement form, string rowLabel)
    {
        // Validate: A-Z only
        if (rowLabel.Length != 1 || rowLabel[0] < 'A' || rowLabel[0] > 'Z')
            return Error($"Invalid row label: {rowLabel}. Use A, B, C, ..., Z");

        int rowIndex = rowLabel[0] - 'A'; // A=0, B=1, C=2

        // Find the condition table pane (aid=4015)
        var condPane = FindByAid(form, "4015");
        if (condPane == null)
            return Error("Condition table (aid=4015) not found");

        var rect = condPane.BoundingRectangle;
        Console.WriteLine($"Condition table: ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

        // Get native hWnd for PostMessage
        var hwndVal = condPane.Properties.NativeWindowHandle.ValueOrDefault;
        if (hwndVal == 0) return Error("Cannot get hWnd for condition table");
        var hWnd = new IntPtr((long)hwndVal);

        // OCR-based row detection: capture + find row Y by matching label letter
        int clickY = -1;
        int clickX = 150; // "내용" column area
        try
        {
            using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hWnd);
            if (bmp != null)
            {
                var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                var ocrRes = ocr.RecognizeAll(bmp).Result;
                if (ocrRes?.Words != null)
                {
                    // Find word matching the row label (A/B/C...)
                    // MFC bitmap font might OCR "A" as "Å" etc, so also check for prefix
                    foreach (var w in ocrRes.Words)
                    {
                        var wt = w.Text.Trim();
                        // Exact match or common MFC OCR misreads
                        if (wt == rowLabel || wt == $"□{rowLabel}" || wt == $"☑{rowLabel}" ||
                            (wt.Length <= 2 && wt.EndsWith(rowLabel)))
                        {
                            clickY = w.Y + w.Height / 2; // center of word
                            Console.WriteLine($"  OCR found '{wt}' at Y={w.Y}, using clickY={clickY}");
                            break;
                        }
                    }
                }
            }
        }
        catch { }

        // Fallback: estimated positions if OCR fails
        if (clickY < 0)
        {
            // Row layout from OCR observations: header Y=0, A≈Y=16, B≈Y=32, C≈Y=56
            // Header ~16px, row A ~16px, row B ~20px, rows variable height
            int[] rowTops = { 16, 32, 52, 72, 92, 112, 132, 152 };
            int idx = Math.Min(rowIndex, rowTops.Length - 1);
            clickY = rowTops[idx] + 8; // center-ish
            Console.WriteLine($"  OCR fallback: estimated clickY={clickY}");
        }

        // DPI FIX: bitmap coords (physical) → MFC logical client coords (for PostMessage)
        var (dpiScale, mfcClientW, mfcClientH) = NativeMethods.GetDpiScaleForMfc(hWnd);
        NativeMethods.GetClientRect(hWnd, out var clientRect);
        int physClientW = clientRect.Right - clientRect.Left;
        int physClientH = clientRect.Bottom - clientRect.Top;

        // clickX/clickY are in bitmap (physical pixel) coords
        // Convert to physical client coords (for SetCursorPos)
        int physX, physY, mfcX, mfcY;
        int bmpW = 0, bmpH = 0;
        try
        {
            using var bmpCheck = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hWnd);
            if (bmpCheck != null)
            {
                bmpW = bmpCheck.Width; bmpH = bmpCheck.Height;
                physX = (int)(clickX * (double)physClientW / bmpW);
                physY = (int)(clickY * (double)physClientH / bmpH);
                mfcX = (int)(clickX * (double)mfcClientW / bmpW);
                mfcY = (int)(clickY * (double)mfcClientH / bmpH);
            }
            else { physX = clickX; physY = clickY; mfcX = clickX; mfcY = clickY; }
        }
        catch { physX = clickX; physY = clickY; mfcX = clickX; mfcY = clickY; }

        Console.WriteLine($"Selecting row {rowLabel}: bitmap({clickX},{clickY}) → mfc({mfcX},{mfcY}) [dpi={dpiScale:F2}, physClient={physClientW}x{physClientH}, mfcClient={mfcClientW}x{mfcClientH}]");

        // MFC owner-drawn controls call GetCursorPos() internally!
        // SetCursorPos: uses physical screen coords (C# DPI-aware ClientToScreen is correct)
        var pt = new POINT { X = physX, Y = physY };
        NativeMethods.ClientToScreen(hWnd, ref pt);
        Console.WriteLine($"  Screen: ({pt.X},{pt.Y})");

        // FocuslessGuard: SetCursorPos is required for this control
        if (FocuslessGuard.IsBlocked("SetCursorPos (MFC owner-drawn control requires physical cursor)"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  This MFC control calls GetCursorPos() — cannot operate without physical cursor.");
            Console.WriteLine("  Remove --force-focusless to allow cursor movement (will be restored after).");
            Console.ResetColor();
            return 1;
        }

        // Save original cursor position (restore after click to minimize disruption)
        NativeMethods.GetCursorPos(out var savedCursor);

        // SetCursorPos + WM_SETFOCUS + PostMessage (same pattern as cond-add tree click)
        NativeMethods.SetCursorPos(pt.X, pt.Y);
        Thread.Sleep(30);

        // WM_SETFOCUS — internal MFC focus, doesn't steal foreground window
        const uint WM_SETFOCUS = 0x0007;
        NativeMethods.SendMessageW(hWnd, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
        Thread.Sleep(30);

        // PostMessage WM_LBUTTONDOWN + WM_LBUTTONUP (MFC logical coords!)
        var lParam = (IntPtr)((mfcY << 16) | (mfcX & 0xFFFF));
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)0x0001, lParam);
        Thread.Sleep(50);
        NativeMethods.PostMessageW(hWnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
        Thread.Sleep(100);

        // Restore cursor to original position (minimize user disruption!)
        NativeMethods.SetCursorPos(savedCursor.X, savedCursor.Y);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✓ Row {rowLabel} selected (cursor restored)");
        Console.ResetColor();

        return 0;
    }

    /// <summary>Click 수정 button (aid=4046) via UIA Invoke — focusless!</summary>
    static int CondClickModify(AutomationElement form)
    {
        var btn = FindByAid(form, "4046");
        if (btn == null) return Error("Button 수정 (aid=4046) not found");

        Console.WriteLine("Clicking '수정'...");
        var invoked = UiaLocator.TryInvoke(btn);
        Console.ForegroundColor = invoked ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"  Invoke: {invoked} (focusless!)");
        Console.ResetColor();
        return invoked ? 0 : 1;
    }

    /// <summary>
    /// Delete the LAST condition from the formula via EM_REPLACESEL + save.
    /// The toolbar X (aid=4022) requires internal MFC selection state that cannot be set externally
    /// (tested: SendInput click, EM_SETSEL, PostMessage, UIA Invoke — all produce "선택영역을 확인하십시요" popup).
    /// Workaround: directly edit the formula text and save. Table rows persist but aren't in the formula.
    /// </summary>
    static int CondClickDelete(AutomationElement form)
    {
        // Find formula editor (cid=10009)
        var formHwndVal = form.Properties.NativeWindowHandle.ValueOrDefault;
        var formHwnd = formHwndVal != 0 ? new IntPtr((long)formHwndVal) : IntPtr.Zero;
        var topWnd = formHwnd != IntPtr.Zero ? NativeMethods.GetParent(formHwnd) : formHwnd;
        if (topWnd == IntPtr.Zero) topWnd = formHwnd;

        IntPtr formulaHwnd = IntPtr.Zero;
        NativeMethods.EnumChildWindows(topWnd, (child, _) =>
        {
            if (NativeMethods.GetDlgCtrlID(child) == 10009) formulaHwnd = child;
            return formulaHwnd == IntPtr.Zero;
        }, IntPtr.Zero);
        if (formulaHwnd == IntPtr.Zero) return Error("Formula editor (cid=10009) not found");

        string formula = GetFormulaText(formulaHwnd);
        Console.WriteLine($"Formula: \"{formula}\"");
        if (string.IsNullOrEmpty(formula)) return Error("Formula text is empty");

        // Find last condition label
        var rowLabels = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        string? lastLabel = null;
        foreach (var label in rowLabels.Reverse())
            if (formula.Contains(label)) { lastLabel = label; break; }
        if (lastLabel == null) return Error("No condition labels found in formula");

        // Calculate selection range to remove
        int labelPos = formula.LastIndexOf(lastLabel);
        int andPos = formula.LastIndexOf("and " + lastLabel);
        int selStart, selEnd;
        if (andPos > 0)
        {
            selStart = andPos - 1; // include preceding space: " and E"
            selEnd = labelPos + lastLabel.Length;
        }
        else
        {
            // First/only condition — check if followed by " and "
            int afterLabel = labelPos + lastLabel.Length;
            if (afterLabel < formula.Length && formula.Substring(afterLabel).StartsWith(" and "))
            {
                selStart = labelPos;
                selEnd = afterLabel + 5; // "E and " — remove label + trailing " and "
            }
            else
            {
                selStart = 0;
                selEnd = formula.Length;
            }
        }

        Console.Write($"Removing '{lastLabel}': EM_SETSEL({selStart},{selEnd}) → ");

        // Edit formula via EM_REPLACESEL
        NativeMethods.SetFocus(formulaHwnd);
        Thread.Sleep(100);
        NativeMethods.SendMessageW(formulaHwnd, 0x00B1 /*EM_SETSEL*/, (IntPtr)selStart, (IntPtr)selEnd);
        Thread.Sleep(100);
        var empty = System.Runtime.InteropServices.Marshal.StringToHGlobalUni("");
        NativeMethods.SendMessageW(formulaHwnd, 0x00C2 /*EM_REPLACESEL*/, (IntPtr)1, empty);
        System.Runtime.InteropServices.Marshal.FreeHGlobal(empty);
        Thread.Sleep(300);

        string newFormula = GetFormulaText(formulaHwnd);
        if (newFormula == formula)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("failed (formula unchanged)");
            Console.ResetColor();
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ \"{newFormula}\"");
        Console.ResetColor();

        // Auto-save via UIA Invoke on "내조건식 저장" (aid=4030)
        var saveBtn = FindByAid(form, "4030");
        if (saveBtn != null)
        {
            Console.Write("  Saving... ");
            bool saved = UiaLocator.TryInvoke(saveBtn);
            Thread.Sleep(500);
            DismissAnyPopup(); // dismiss any save confirmation

            if (saved)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Saved");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("save invoke failed");
            }
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Save button (aid=4030) not found — save manually!");
            Console.ResetColor();
        }

        return 0;
    }

    static string GetFormulaText(IntPtr formulaHwnd)
    {
        var buf = new System.Text.StringBuilder(1024);
        NativeMethods.SendMessageW(formulaHwnd, 0x000D /*WM_GETTEXT*/, (IntPtr)1024, buf);
        return buf.ToString();
    }

    static void DismissAnyPopup()
    {
        var fgWnd = NativeMethods.GetForegroundWindow();
        var cls = new System.Text.StringBuilder(256);
        NativeMethods.GetClassNameW(fgWnd, cls, 256);
        if (cls.ToString() == "#32770")
        {
            // Read popup text for logging
            NativeMethods.EnumChildWindows(fgWnd, (child, _) =>
            {
                var childCls = new System.Text.StringBuilder(64);
                NativeMethods.GetClassNameW(child, childCls, 64);
                if (childCls.ToString() == "Static")
                {
                    var txt = new System.Text.StringBuilder(256);
                    NativeMethods.SendMessageW(child, 0x000D, (IntPtr)256, txt);
                    if (txt.Length > 0) Console.Write($"[popup: {txt}] ");
                }
                return true;
            }, IntPtr.Zero);
            NativeMethods.PostMessageW(fgWnd, 0x0010 /*WM_CLOSE*/, IntPtr.Zero, IntPtr.Zero);
            Thread.Sleep(300);
        }
    }

    /// <summary>
    /// Show current condition items via OCR on the condition table (aid=4015).
    /// The table is owner-drawn, so we capture a screenshot and OCR it.
    /// </summary>
    static int CondListItems(AutomationElement form)
    {
        // Find the condition table pane (aid=4015)
        var condPane = FindByAid(form, "4015");
        if (condPane == null)
            return Error("Condition table (aid=4015) not found");

        var rect = condPane.BoundingRectangle;
        Console.WriteLine($"Condition table: ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

        // Get hWnd for PrintWindow capture
        var hwndVal = condPane.Properties.NativeWindowHandle.ValueOrDefault;
        if (hwndVal == 0) return Error("Cannot get hWnd for condition table");
        var hWnd = new IntPtr((long)hwndVal);

        // PrintWindow capture
        using var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hWnd);
        if (bmp == null) return Error("PrintWindow capture failed");

        Console.WriteLine($"Captured: {bmp.Width}x{bmp.Height}");

        // OCR
        var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
        var ocrResult = ocr.RecognizeAll(bmp).Result;
        if (ocrResult == null || string.IsNullOrEmpty(ocrResult.FullText))
            return Error("OCR returned no text");

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("── Condition Items (OCR) ──");
        Console.ResetColor();

        // Group words by Y coordinate (±5px tolerance)
        var grouped = ocrResult.Words
            .GroupBy(w => w.Y / 8 * 8) // quantize to 8px bands
            .OrderBy(g => g.Key);
        foreach (var line in grouped)
        {
            var text = string.Join(" ", line.OrderBy(w => w.X).Select(w => w.Text));
            Console.WriteLine($"  Y={line.Key,4} | {text}");
        }

        // Dump raw OCR words with positions (for debugging delete X button)
        Console.WriteLine("\n── Raw OCR Words ──");
        foreach (var w in ocrResult.Words.OrderBy(w => w.Y).ThenBy(w => w.X))
        {
            Console.WriteLine($"  ({w.X,4},{w.Y,4}) {w.Width,3}x{w.Height,3} \"{w.Text}\"");
        }

        // Also read the formula from aid=10009
        var formulaDoc = FindByAid(form, "10009");
        if (formulaDoc != null)
        {
            try
            {
                var val = formulaDoc.Patterns.Value.Pattern.Value.Value;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  조건식: ");
                Console.ResetColor();
                Console.WriteLine(val);
            }
            catch { }
        }

        return 0;
    }
}
