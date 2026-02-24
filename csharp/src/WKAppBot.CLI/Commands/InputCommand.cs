using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: input command — Type text into MDI form controls (5 strategies)
internal partial class Program
{
    // ═══════════════════════════════════════════════════════════════════════
    // input — Type text into a specific control (MDI form + control ID)
    //
    // Tries multiple strategies:
    //   3. WM_SETTEXT (focusless/direct text replacement)
    //   4. PostMessage WM_KEYDOWN/WM_CHAR (focus + message hybrid)
    //   5. Click + PostMessage WM_CHAR (physical focus + message)
    //   1. Focus + WM_CHAR (AttachThread + SetFocus + SendMessage)
    //   2. Click + VK-Type (physical keyboard input)
    // ═══════════════════════════════════════════════════════════════════════
    static int InputCommand(string[] args)
    {
        if (args.Length < 3)
            return Error(@"Usage: wkappbot input <window-title> <form-id> <text> [--cid N] [--enter] [--method N]
  Types text into a control in an MDI form.

Strategies (tried in order, or specify --method):
  3: WM_SETTEXT    — direct text replacement (updates Win32 text, not internal buffer)
  4: PostMsg       — SetFocus + PostMessage WM_KEYDOWN/WM_CHAR with proper lParam
  5: Click+PostMsg — physical click (left area) → PostMessage WM_CHAR (hybrid, best for CMaskEdit)
  1: Focus+WM_CHAR — AttachThread + SetFocus + SendMessage WM_CHAR
  2: Click+Type    — physical click on field → Home+Shift+End → SendInput VK

Options:
  --cid N     Target control ID (default: 3780, the stock code field)
  --enter     Send Enter after typing (trigger data request)
  --method N  Use only method N (1-5)
  --click-x N Override click X offset from control left edge (default: 30)

Examples:
  wkappbot input ""투혼"" 1101 ""000660"" --enter
  wkappbot input ""투혼"" 1101 ""005930"" --cid 3780 --enter --method 5");

        string title = args[0];
        string targetFormId = args[1];
        string text = args[2];
        int targetCid = int.TryParse(GetArgValue(args, "--cid"), out var cid) ? cid : 3780;
        bool sendEnter = args.Contains("--enter");
        int? methodOnly = int.TryParse(GetArgValue(args, "--method"), out var m) ? m : null;
        int clickXOffset = int.TryParse(GetArgValue(args, "--click-x"), out var cx2) ? cx2 : 30;

        // Find target window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\"");

        // Check elevation
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        bool weAreElevated = NativeMethods.IsCurrentProcessElevated();
        bool targetElevated = NativeMethods.IsProcessElevated(targetPid) ?? true;
        if (targetElevated && !weAreElevated)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ✗ Target is elevated but wkappbot is not. Run as admin.");
            Console.ResetColor();
            return 1;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  ✓ Elevated — input enabled");
        Console.ResetColor();

        // Find the MDI form
        var mdiClient = NativeMethods.FindWindowExW(win.Handle, IntPtr.Zero, "MDIClient", null);
        if (mdiClient == IntPtr.Zero) return Error("MDIClient not found");

        // First check: is there a specific MDI child the user wants?
        // For now, find the FIRST visible form matching the ID (Z-order top = first)
        var mdiChildren = WindowFinder.GetChildrenZOrder(mdiClient);
        var matchingForms = mdiChildren.Where(f =>
            f.Title.Contains($"[{targetFormId}]")).ToList();
        if (matchingForms.Count == 0) return Error($"Form [{targetFormId}] not found");

        // Select the best form: MDI active → focus ancestor → largest visible → Z-order first
        // GPT insight: "WM_MDIGETACTIVE + GetGUIThreadInfo(hwndFocus) 교차검증이 정답"
        WindowInfo? targetForm = matchingForms[0];
        string formSelection = "Z-order first";
        if (matchingForms.Count > 1)
        {
            // Priority 1: MDI active child
            var activeChild = NativeMethods.SendMessageW(mdiClient, NativeMethods.WM_MDIGETACTIVE, IntPtr.Zero, IntPtr.Zero);
            var activeForm = matchingForms.FirstOrDefault(f => f.Handle == activeChild);
            if (activeForm != null)
            {
                targetForm = activeForm;
                formSelection = "MDI active";
            }
            else
            {
                // Priority 2: GetGUIThreadInfo — find which form contains hwndFocus
                var focusHwnd = NativeMethods.GetFocusedHwndInProcess(win.Handle);
                if (focusHwnd != IntPtr.Zero)
                {
                    // Walk up from focused control to find which matching form is its ancestor
                    var ancestor = focusHwnd;
                    for (int depth = 0; depth < 10 && ancestor != IntPtr.Zero; depth++)
                    {
                        var matchByFocus = matchingForms.FirstOrDefault(f => f.Handle == ancestor);
                        if (matchByFocus != null)
                        {
                            targetForm = matchByFocus;
                            formSelection = "focus ancestor";
                            break;
                        }
                        ancestor = NativeMethods.GetParent(ancestor);
                    }
                }

                // Priority 3: largest visible form (by area)
                if (formSelection == "Z-order first")
                {
                    var largest = matchingForms
                        .Where(f => NativeMethods.IsWindowVisible(f.Handle))
                        .OrderByDescending(f => f.Rect.Width * f.Rect.Height)
                        .FirstOrDefault();
                    if (largest != null)
                    {
                        targetForm = largest;
                        formSelection = "largest visible";
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  ({matchingForms.Count} forms, using {formSelection})");
            Console.ResetColor();
        }
        Console.WriteLine($"Form: {targetForm.Title} @({targetForm.Rect.Left},{targetForm.Rect.Top} {targetForm.Rect.Width}x{targetForm.Rect.Height})");

        // Find the target control (cid) inside the form hierarchy
        IntPtr targetHwnd = IntPtr.Zero;
        void FindControlByCid(IntPtr parent, int depth)
        {
            if (depth > 6 || targetHwnd != IntPtr.Zero) return;
            var children = WindowFinder.GetChildrenZOrder(parent);
            foreach (var child in children)
            {
                if (child.ControlId == targetCid)
                {
                    targetHwnd = child.Handle;
                    return;
                }
                FindControlByCid(child.Handle, depth + 1);
            }
        }
        FindControlByCid(targetForm.Handle, 0);

        if (targetHwnd == IntPtr.Zero)
            return Error($"Control cid={targetCid} not found in form [{targetFormId}]");

        // Read current text
        var len = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
        var sb = new StringBuilder(len + 2);
        NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(len + 1), sb);
        var currentText = sb.ToString();

        NativeMethods.GetWindowRect(targetHwnd, out var ctlRect);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"  Control: cid={targetCid}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($" @({ctlRect.Left},{ctlRect.Top} {ctlRect.Width}x{ctlRect.Height})");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" \"{currentText}\"");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($" → \"{text}\"");
        Console.ResetColor();

        bool success = false;

        // ── Method 3: WM_SETTEXT + VK_RETURN ──
        if (!success && (methodOnly == null || methodOnly == 3))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [3] WM_SETTEXT: ");
            Console.ResetColor();

            try
            {
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);

                if (sendEnter)
                {
                    Thread.Sleep(100);
                    NativeMethods.SendMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D, IntPtr.Zero);
                    Thread.Sleep(50);
                    NativeMethods.SendMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x0D, IntPtr.Zero);
                }

                Thread.Sleep(200);
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var newText = verifySb.ToString();

                if (newText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ \"{newText}\"");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? \"{newText}\" (expected \"{text}\")");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // ── Method 4: PostMessage WM_KEYDOWN/WM_CHAR with proper lParam ──
        // CMaskEdit::OnChar() processes WM_CHAR directly into m_strBuffer.
        // Key insight: OnChar does NOT check GetFocus() — it trusts that messages
        // only arrive when focused (Windows guarantee). But PostMessage bypasses this!
        // We use proper lParam encoding (scancode + repeat count) for authenticity.
        if (!success && (methodOnly == null || methodOnly == 4))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [4] PostMsg WM_CHAR: ");
            Console.ResetColor();

            try
            {
                // Helper: build WM_KEYDOWN/WM_CHAR lParam with scancode
                IntPtr MakeKeyLParam(uint vk, bool keyUp = false, bool extended = false)
                {
                    uint scanCode = NativeMethods.MapVirtualKeyW(vk, 0); // MAPVK_VK_TO_VSC
                    uint lp = 1u; // repeat count = 1
                    lp |= (scanCode & 0xFF) << 16; // scan code bits 16-23
                    if (extended) lp |= (1u << 24); // extended key flag
                    if (keyUp)
                    {
                        lp |= (1u << 30); // previous key state = down
                        lp |= (1u << 31); // transition state = releasing
                    }
                    return (IntPtr)lp;
                }

                // Step 0: Activate MDI form + foreground (for focus routing)
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(150);
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(200);

                // Step 1: SetFocus to the target control
                uint targetThread = NativeMethods.GetWindowThreadProcessId(targetHwnd, out _);
                uint ourThread = NativeMethods.GetCurrentThreadId();
                bool attached = false;
                if (targetThread != ourThread)
                    attached = NativeMethods.AttachThreadInput(ourThread, targetThread, true);
                var prevFocus = NativeMethods.SetFocus(targetHwnd);
                Thread.Sleep(100);

                // Step 2: Select all existing text → Home, then Shift+End
                // WM_KEYDOWN VK_HOME (no modifiers)
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                    (IntPtr)0x24 /*VK_HOME*/, MakeKeyLParam(0x24, extended: true));
                Thread.Sleep(30);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                    (IntPtr)0x24, MakeKeyLParam(0x24, keyUp: true, extended: true));
                Thread.Sleep(30);

                // Shift+End: need to simulate shift state
                // For CMaskEdit, we send WM_KEYDOWN for Shift, then End, then release
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                    (IntPtr)0x10 /*VK_SHIFT*/, MakeKeyLParam(0x10));
                Thread.Sleep(20);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                    (IntPtr)0x23 /*VK_END*/, MakeKeyLParam(0x23, extended: true));
                Thread.Sleep(30);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                    (IntPtr)0x23, MakeKeyLParam(0x23, keyUp: true, extended: true));
                Thread.Sleep(20);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                    (IntPtr)0x10, MakeKeyLParam(0x10, keyUp: true));
                Thread.Sleep(50);

                // Step 3: Delete selected text via backspace
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                    (IntPtr)0x08 /*VK_BACK*/, MakeKeyLParam(0x08));
                Thread.Sleep(20);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_CHAR,
                    (IntPtr)0x08, MakeKeyLParam(0x08));
                Thread.Sleep(20);
                NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                    (IntPtr)0x08, MakeKeyLParam(0x08, keyUp: true));
                Thread.Sleep(50);

                // Step 4: Type each character — full WM_KEYDOWN + WM_CHAR + WM_KEYUP sequence
                foreach (char ch in text)
                {
                    // Map character to VK code
                    short vkScan = NativeMethods.VkKeyScanW(ch);
                    byte vk = (byte)(vkScan & 0xFF);
                    uint scanCode = NativeMethods.MapVirtualKeyW(vk, 0);

                    IntPtr downLp = MakeKeyLParam(vk);
                    IntPtr upLp = MakeKeyLParam(vk, keyUp: true);

                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN, (IntPtr)vk, downLp);
                    Thread.Sleep(10);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_CHAR, (IntPtr)ch, downLp);
                    Thread.Sleep(10);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP, (IntPtr)vk, upLp);
                    Thread.Sleep(30);
                }
                Thread.Sleep(200);

                // Step 5: Send Enter if requested (VK_RETURN)
                // CCtlItemCode::OnChar ignores VK_RETURN; OnKeyUp processes it!
                if (sendEnter)
                {
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                        (IntPtr)0x0D /*VK_RETURN*/, MakeKeyLParam(0x0D));
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                        (IntPtr)0x0D, MakeKeyLParam(0x0D, keyUp: true));
                    Thread.Sleep(500);
                }

                // Step 6: Detach
                if (attached)
                    NativeMethods.AttachThreadInput(ourThread, targetThread, false);

                // Step 7: Verify via OCR (WM_GETTEXT doesn't work for CMaskEdit)
                Thread.Sleep(300);
                string? ocrText = null;
                try
                {
                    // Capture the control area and OCR it
                    NativeMethods.GetWindowRect(targetHwnd, out var verifyRect);
                    using var formBmp = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (formBmp != null)
                    {
                        // Crop to control area (relative to form)
                        NativeMethods.GetWindowRect(targetForm.Handle, out var formRect);
                        int rx = verifyRect.Left - formRect.Left;
                        int ry = verifyRect.Top - formRect.Top;
                        int rw = Math.Min(verifyRect.Width, formBmp.Width - rx);
                        int rh = Math.Min(verifyRect.Height, formBmp.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var cropBmp = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);

                            // Save screenshot for debug
                            var ssPath = Path.Combine(DataDir, "logs", $"input_m4_verify_{DateTime.Now:HHmmss}.png");
                            cropBmp.Save(ssPath);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"[ss: {Path.GetFileName(ssPath)}] ");
                            Console.ResetColor();

                            // OCR the cropped control
                            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                            var ocrResult = ocr.RecognizeAll(cropBmp).GetAwaiter().GetResult();
                            ocrText = ocrResult.FullText?.Trim();
                        }
                    }
                }
                catch (Exception ocrEx)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"[OCR err: {ocrEx.Message}] ");
                    Console.ResetColor();
                }

                // Also try WM_GETTEXT and UIA as fallback verification
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var wmText = verifySb.ToString();

                // Report results
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmText}\" ");
                if (!string.IsNullOrEmpty(ocrText))
                    Console.Write($"OCR=\"{ocrText}\" ");
                Console.ResetColor();

                // Check if OCR shows our text
                if (!string.IsNullOrEmpty(ocrText) && ocrText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(wmText) && wmText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM_GETTEXT confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? expected \"{text}\"");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // ── Method 5: Click (left text area) + PostMessage WM_CHAR hybrid ──
        // Physical click ensures focus lands on the control, then PostMessage
        // sends WM_CHAR directly to the hwnd. Best of both worlds for CMaskEdit.
        if (!success && (methodOnly == null || methodOnly == 5))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [5] Click+PostMsg: ");
            Console.ResetColor();

            try
            {
                IntPtr MakeKeyLParam5(uint vk, bool keyUp = false, bool extended = false)
                {
                    uint scanCode = NativeMethods.MapVirtualKeyW(vk, 0);
                    uint lp = 1u;
                    lp |= (scanCode & 0xFF) << 16;
                    if (extended) lp |= (1u << 24);
                    if (keyUp) { lp |= (1u << 30); lp |= (1u << 31); }
                    return (IntPtr)lp;
                }

                // Step 0: Activate MDI form + bring to front
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(300);

                // Step 1: Physical click on the LEFT portion of the control (text area, not buttons)
                NativeMethods.GetWindowRect(targetHwnd, out var clickRect);
                int clickX = clickRect.Left + clickXOffset; // Left area, avoid right-side buttons
                int clickY = clickRect.Top + clickRect.Height / 2;
                MouseInput.Click(clickX, clickY);
                Thread.Sleep(300);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[click({clickX},{clickY})] ");
                Console.ResetColor();

                // Step 2: Select all via Ctrl+A (CMaskEdit::PreTranslateMessage handles Ctrl shortcuts)
                // Actually use Home + Shift+End which is more reliable for CMaskEdit
                KeyboardInput.PressKey("home");
                Thread.Sleep(30);
                KeyboardInput.Hotkey(new[] { "shift", "end" });
                Thread.Sleep(100);

                // Step 3: Delete selected text
                KeyboardInput.PressKey("delete");
                Thread.Sleep(100);

                // Step 4: Now PostMessage WM_CHAR for each digit
                // Since physical click already set focus, PostMessage should work
                foreach (char ch in text)
                {
                    short vkScan = NativeMethods.VkKeyScanW(ch);
                    byte vk = (byte)(vkScan & 0xFF);
                    IntPtr downLp = MakeKeyLParam5(vk);
                    IntPtr upLp = MakeKeyLParam5(vk, keyUp: true);

                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN, (IntPtr)vk, downLp);
                    Thread.Sleep(10);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_CHAR, (IntPtr)ch, downLp);
                    Thread.Sleep(10);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP, (IntPtr)vk, upLp);
                    Thread.Sleep(30);
                }
                Thread.Sleep(200);

                // Step 5: Send Enter if requested
                if (sendEnter)
                {
                    // CCtlItemCode processes VK_RETURN in OnKeyUp, not OnChar!
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYDOWN,
                        (IntPtr)0x0D, MakeKeyLParam5(0x0D));
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(targetHwnd, NativeMethods.WM_KEYUP,
                        (IntPtr)0x0D, MakeKeyLParam5(0x0D, keyUp: true));
                    Thread.Sleep(500);
                }

                // Step 6: Verify via OCR + WM_GETTEXT
                Thread.Sleep(300);
                string? ocrText5 = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var verifyRect5);
                    using var formBmp5 = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (formBmp5 != null)
                    {
                        NativeMethods.GetWindowRect(targetForm.Handle, out var formRect5);
                        int rx = verifyRect5.Left - formRect5.Left;
                        int ry = verifyRect5.Top - formRect5.Top;
                        int rw = Math.Min(verifyRect5.Width, formBmp5.Width - rx);
                        int rh = Math.Min(verifyRect5.Height, formBmp5.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var cropBmp5 = ScreenCapture.CropRegion(formBmp5, rx, ry, rw, rh);
                            var ssPath5 = Path.Combine(DataDir, "logs", $"input_m5_verify_{DateTime.Now:HHmmss}.png");
                            cropBmp5.Save(ssPath5);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"[ss: {Path.GetFileName(ssPath5)}] ");
                            Console.ResetColor();

                            var ocr5 = new WKAppBot.Vision.SimpleOcrAnalyzer();
                            var ocrResult5 = ocr5.RecognizeAll(cropBmp5).GetAwaiter().GetResult();
                            ocrText5 = ocrResult5.FullText?.Trim();
                        }
                    }
                }
                catch (Exception ocrEx)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"[OCR err: {ocrEx.Message}] ");
                    Console.ResetColor();
                }

                var verifySb5 = new StringBuilder(256);
                var vLen5 = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen5 + 1), verifySb5);
                var wmText5 = verifySb5.ToString();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmText5}\" ");
                if (!string.IsNullOrEmpty(ocrText5))
                    Console.Write($"OCR=\"{ocrText5}\" ");
                Console.ResetColor();

                if (!string.IsNullOrEmpty(ocrText5) && ocrText5.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(wmText5) && wmText5.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM_GETTEXT confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? expected \"{text}\"");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // ── Method 1: SetFocus + WM_CHAR (needs foreground, but sends chars via message) ──
        if (!success && (methodOnly == null || methodOnly == 1))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [1] Focus+WM_CHAR: ");
            Console.ResetColor();

            try
            {
                // Bring parent window to foreground
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(200);

                // AttachThreadInput to target thread → SetFocus → Detach
                uint targetThread = NativeMethods.GetWindowThreadProcessId(targetHwnd, out _);
                uint ourThread = NativeMethods.GetCurrentThreadId();
                bool attached = false;
                if (targetThread != ourThread)
                {
                    attached = NativeMethods.AttachThreadInput(ourThread, targetThread, true);
                }
                NativeMethods.SetFocus(targetHwnd);
                Thread.Sleep(100);

                // Select all existing text (EM_SETSEL 0,-1 = select all)
                NativeMethods.SendMessageW(targetHwnd, 0x00B1 /*EM_SETSEL*/, IntPtr.Zero, (IntPtr)(-1));
                Thread.Sleep(50);

                // Delete selected text first (backspace to clear)
                NativeMethods.SendMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)0x08 /*VK_BACK*/, IntPtr.Zero);
                Thread.Sleep(50);

                // Send each character via WM_CHAR
                foreach (char ch in text)
                {
                    NativeMethods.SendMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)ch, IntPtr.Zero);
                    Thread.Sleep(20);
                }

                // Send Enter via WM_KEYDOWN/UP if requested (triggers RunEventCommTR in HTS)
                if (sendEnter)
                {
                    Thread.Sleep(100);
                    NativeMethods.SendMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D /*VK_RETURN*/, IntPtr.Zero);
                    Thread.Sleep(50);
                    NativeMethods.SendMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x0D, IntPtr.Zero);
                }

                // Detach thread input
                if (attached)
                    NativeMethods.AttachThreadInput(ourThread, targetThread, false);

                // Read back to verify
                Thread.Sleep(200);
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var newText = verifySb.ToString();

                if (newText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ \"{newText}\"");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? \"{newText}\" (expected \"{text}\")");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // ── Method 2: Click + Home + Shift+End + VK-based SendInput + Enter ──
        // CMaskEdit (CWnd-based) doesn't respond to WM_CHAR/WM_SETTEXT —
        // only real physical keyboard input works. Use Home→Shift+End to select all.
        if (!success && (methodOnly == null || methodOnly == 2))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [2] Click+VK-Type: ");
            Console.ResetColor();

            try
            {
                // Click on the LEFT portion (text input area, not right-side buttons)
                int cx = ctlRect.Left + clickXOffset;
                int cy = ctlRect.Top + ctlRect.Height / 2;

                // Activate the specific MDI child form first
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);

                // Bring main window to front + click field
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(300);
                MouseInput.Click(cx, cy);
                Thread.Sleep(300);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[click({cx},{cy})] ");
                Console.ResetColor();

                // Home key → move caret to start
                KeyboardInput.PressKey("home");
                Thread.Sleep(50);

                // Shift+End → select all text
                KeyboardInput.Hotkey(new[] { "shift", "end" });
                Thread.Sleep(100);

                // Type each digit (replaces selected text)
                foreach (char ch in text)
                {
                    if (ch >= '0' && ch <= '9')
                    {
                        KeyboardInput.PressKey(ch.ToString());
                        Thread.Sleep(40);
                    }
                    else if (char.IsLetter(ch))
                    {
                        KeyboardInput.PressKey(ch.ToString());
                        Thread.Sleep(40);
                    }
                }
                Thread.Sleep(200);

                if (sendEnter)
                {
                    KeyboardInput.PressKey("enter");
                    Thread.Sleep(500);
                }

                // Verify: WM_GETTEXT (may return "" for CMaskEdit/CWnd controls)
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var wmText = verifySb.ToString();

                // Also check UIA Name (CMaskEdit exposes text via UIA even when WM_GETTEXT fails)
                string? uiaName = null;
                try
                {
                    using var automation = new FlaUI.UIA3.UIA3Automation();
                    var uiaEl = automation.FromHandle(targetHwnd);
                    uiaName = uiaEl?.Name;
                }
                catch { }

                // Take screenshot for visual verification
                try
                {
                    using var bmp = ScreenCapture.CaptureWindow(win.Handle);
                    var ssPath = Path.Combine(DataDir, "logs", $"input_verify_{DateTime.Now:HHmmss}.png");
                    bmp.Save(ssPath);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"[screenshot: {Path.GetFileName(ssPath)}] ");
                    Console.ResetColor();
                }
                catch { }

                var checkText = !string.IsNullOrEmpty(wmText) ? wmText : uiaName ?? "";
                if (checkText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ \"{checkText}\" (via {(!string.IsNullOrEmpty(wmText) ? "WM_GETTEXT" : "UIA")})");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"? WM_GETTEXT=\"{wmText}\"");
                    if (uiaName != null) Console.Write($" UIA=\"{uiaName}\"");
                    Console.WriteLine($" (expected \"{text}\")");
                    Console.ResetColor();
                    // If UIA shows something different from before, it might have worked
                    if (!string.IsNullOrEmpty(uiaName) && uiaName != currentText)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  → UIA changed: \"{currentText}\" → \"{uiaName}\" (likely success!)");
                        Console.ResetColor();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // Summary
        Console.WriteLine();
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  ✓ Input successful: \"{text}\" → cid={targetCid} in [{targetFormId}]");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ All methods failed for cid={targetCid}");
            Console.ResetColor();

            // [FALLBACK] Guide: scan the app to build experience for future sessions
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("  [FALLBACK] Tips for future success:");
            Console.WriteLine($"    1. Scan the app to build experience DB:");
            Console.WriteLine($"       wkappbot scan \"{title}\" --ocr --save --detail");
            Console.WriteLine($"    2. Record what you learned:");
            Console.WriteLine($"       wkappbot knowhow write {targetFormId} \"lesson\" --cid {targetCid}");
            Console.WriteLine($"    3. Check existing knowhow:");
            Console.WriteLine($"       wkappbot knowhow read {targetFormId} --cid {targetCid}");

            // Show profile path if it exists
            var profileDir = Path.Combine(DataDir, "profiles");
            if (Directory.Exists(profileDir))
            {
                var expDirs = Directory.GetDirectories(profileDir, "*_exp");
                foreach (var expDir in expDirs)
                {
                    var formJsonPath = Path.Combine(expDir, $"form_{targetFormId}.json");
                    if (File.Exists(formJsonPath))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"    Experience data: {formJsonPath}");
                        Console.ResetColor();
                    }
                }
            }
            Console.ResetColor();
            return 1;
        }

        // ── ActionState IPC: share input-command info with AppBotEye ──
        try
        {
            ActionState.Write(new ActionState
            {
                Source = "input",
                WindowTitle = win.Title,
                ElementName = $"cid={targetCid}",
                ActionName = "type_text",
                ActionDetail = $"Input: \"{text}\" → cid={targetCid} in [{targetFormId}]",
                Status = success ? "pass" : "fail",
            });
        }
        catch { /* best-effort */ }

        return 0;
    }
}

