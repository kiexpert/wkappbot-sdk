using System.Runtime.InteropServices;
using System.Text;
using FlaUI.UIA3;
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
A11Y: UIA Value   — standard accessibility (focusless, works on standard edits)
 10: Focusless     — pure PostMessage WITHOUT SetFocus (MFC custom edits, focusless!)
  9: PostMsgPipe   — pure PostMessage: SetFocus+Home+chars+Enter to hwnd (focus-immune)
  6: EM_REPLACESEL — select-all + EM_REPLACESEL (standard edit replace, fires EN_CHANGE)
  7: SETTEXT+NOTIFY — WM_SETTEXT + WM_COMMAND(EN_CHANGE) to parent (force change notification)
  3: WM_SETTEXT    — direct text replacement (updates Win32 text, not internal buffer)
  8: AtomicBatch   — single SendInput call: Home+Shift+End+chars+Enter (no inter-key gap)
  4: PostMsg       — SetFocus + PostMessage WM_KEYDOWN/WM_CHAR with proper lParam
  5: Click+PostMsg — physical click (left area) → PostMessage WM_CHAR (hybrid, best for CMaskEdit)
  1: Focus+WM_CHAR — AttachThread + SetFocus + SendMessage WM_CHAR
  2: Click+Type    — physical click on field → Home+Shift+End → SendInput VK

Options:
  --cid N           Target control ID (default: 3780, the stock code field)
  --enter           Send Enter after typing (trigger data request)
  --method M        Use only one method (number or keyword)
                    Numbers: 1..10
                    Keywords: replacesel|settext-notify|settext|atomic|postpipe|focusless|postmsg|click-post|focus-char|click-type
  --click-x N       Override click X offset from control left edge (default: 30)
  --zoom            Show 2x magnifier overlay on the target control during input

Examples:
  wkappbot input ""투혼"" 1101 ""000660"" --enter
  wkappbot input ""투혼"" 1101 ""005930"" --cid 3780 --enter --method 5
  wkappbot input ""투혼"" 1101 ""005930"" --cid 3780 --enter --method settext");

        string title = args[0];
        string targetFormId = args[1];
        string text = args[2];
        int targetCid = int.TryParse(GetArgValue(args, "--cid"), out var cid) ? cid : 3780;
        bool sendEnter = args.Contains("--enter");
        int? methodOnly = ParseMethodOption(GetArgValue(args, "--method"));
        int clickXOffset = int.TryParse(GetArgValue(args, "--click-x"), out var cx2) ? cx2 : 30;
        bool showZoom = args.Contains("--zoom");

        var methodRaw = GetArgValue(args, "--method");
        if (!string.IsNullOrWhiteSpace(methodRaw) && methodOnly == null)
            return Error($"Invalid --method: {methodRaw} (use 1..10 or focusless|atomic|postpipe|replacesel|settext-notify|settext|postmsg|click-post|focus-char|click-type)");

        static int? ParseMethodOption(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;
            if (int.TryParse(raw, out var n) && n >= 1 && n <= 10) return n;

            return raw.Trim().ToLowerInvariant() switch
            {
                "focus-char" => 1,
                "click-type" => 2,
                "settext" => 3,
                "postmsg" => 4,
                "click-post" => 5,
                "replacesel" => 6,
                "settext-notify" => 7,
                "atomic" => 8,
                "postpipe" => 9,
                "focusless" => 10,
                _ => null,
            };
        }

        // Find target window — try all matches until one has MDIClient
        // (e.g. "투혼" matches both main window and #32770 dialogs)
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");

        WindowInfo? win = null;
        IntPtr mdiClient = IntPtr.Zero;
        foreach (var candidate in windows)
        {
            var mdi = NativeMethods.FindWindowExW(candidate.Handle, IntPtr.Zero, "MDIClient", null);
            if (mdi != IntPtr.Zero)
            {
                win = candidate;
                mdiClient = mdi;
                break;
            }
        }
        if (win == null || mdiClient == IntPtr.Zero)
        {
            // Show what we found for diagnostics
            foreach (var w in windows.Take(5))
                Console.WriteLine($"  candidate: [{w.Handle:X8}] class=\"{w.ClassName}\" \"{w.Title}\"");
            return Error($"No window with MDIClient found among {windows.Count} match(es) for \"{title}\"");
        }
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

        // === INPUT READINESS CHECK ===
        // Verify the target form is in a state where input can succeed
        {
            bool formVisible = NativeMethods.IsWindowVisible(targetForm.Handle);
            bool formIconic = NativeMethods.IsIconic(targetForm.Handle);
            bool formEnabled = NativeMethods.IsWindowEnabled(targetForm.Handle);

            if (!formVisible)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ⚠ Form is NOT visible — input may go to hidden window");
                Console.ResetColor();
            }
            if (formIconic)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ⚠ Form is minimized (iconic)");
                Console.ResetColor();
            }
            if (!formEnabled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ⚠ Form is disabled — a modal dialog may be blocking");
                Console.ResetColor();
            }

            // Check for blocking dialogs in the same process
            NativeMethods.GetWindowThreadProcessId(win.Handle, out uint procId);
            IntPtr blocker = IntPtr.Zero;
            string blockerInfo = "";
            NativeMethods.EnumWindows((h, _) =>
            {
                if (!NativeMethods.IsWindowVisible(h)) return true;
                NativeMethods.GetWindowThreadProcessId(h, out uint pid);
                if (pid != procId) return true;
                if (h == win.Handle) return true;
                var sb2 = new StringBuilder(256);
                NativeMethods.GetClassNameW(h, sb2, sb2.Capacity);
                var cls = sb2.ToString();
                if (cls == "#32770")
                {
                    var tsb = new StringBuilder(512);
                    NativeMethods.GetWindowTextW(h, tsb, tsb.Capacity);
                    var t = tsb.ToString();
                    if (!string.IsNullOrEmpty(t))
                    {
                        blocker = h;
                        blockerInfo = $"[{h:X8}] class={cls} \"{t}\"";
                        return false;
                    }
                }
                return true;
            }, IntPtr.Zero);

            if (blocker != IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ BLOCKER detected: {blockerInfo}");
                Console.ResetColor();
                Console.WriteLine("    (a dialog may be blocking the app's message pump)");
            }

            // Activate the MDI form to ensure it's on top
            NativeMethods.SendMessageW(mdiClient, 0x0222, targetForm.Handle, IntPtr.Zero); // WM_MDIACTIVATE
            Thread.Sleep(100);
        }

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
        const bool RequireOcrForNonA11y = true;

        bool ConfirmByOcrContains(string methodTag)
        {
            try
            {
                NativeMethods.GetWindowRect(targetHwnd, out var verifyRect);
                using var formBmp = ScreenCapture.CaptureWindow(targetForm.Handle);
                if (formBmp == null) return false;

                NativeMethods.GetWindowRect(targetForm.Handle, out var formRect);
                int rx = verifyRect.Left - formRect.Left;
                int ry = verifyRect.Top - formRect.Top;
                int rw = Math.Min(verifyRect.Width, formBmp.Width - rx);
                int rh = Math.Min(verifyRect.Height, formBmp.Height - ry);
                if (rx < 0 || ry < 0 || rw <= 0 || rh <= 0) return false;

                using var cropBmp = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                var ssPath = Path.Combine(DataDir, "logs", $"input_{methodTag}_ocr_{DateTime.Now:HHmmss}.png");
                cropBmp.Save(ssPath);
                var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                var ocrResult = ocr.RecognizeAll(cropBmp).GetAwaiter().GetResult();
                var ocrText = ocrResult.FullText?.Trim() ?? "";

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[ocr:{ocrText}] ");
                Console.ResetColor();
                return !string.IsNullOrEmpty(ocrText) && ocrText.Contains(text);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[ocr-err:{ex.Message}] ");
                Console.ResetColor();
                return false;
            }
        }

        // [ZOOM] Capture a control as PNG bytes (PrintWindow + CropRegion, Z-order safe)
        byte[]? CaptureControlPng(IntPtr formHandle, IntPtr controlHandle)
        {
            try
            {
                NativeMethods.GetWindowRect(controlHandle, out var cr);
                using var formBmp = ScreenCapture.CaptureWindow(formHandle);
                if (formBmp == null || ScreenCapture.IsBlankBitmap(formBmp)) return null;

                NativeMethods.GetWindowRect(formHandle, out var fr);
                int rx = cr.Left - fr.Left, ry = cr.Top - fr.Top;
                int rw = Math.Min(cr.Width, formBmp.Width - rx);
                int rh = Math.Min(cr.Height, formBmp.Height - ry);
                if (rx < 0 || ry < 0 || rw <= 0 || rh <= 0) return null;

                using var crop = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                return ScreenCapture.ToPngBytes(crop);
            }
            catch { return null; }
        }

        // Fuzzy digit match for tiny MFC bitmap OCR (130x20 controls)
        // Extracts digits from OCR text, compares to expected via Levenshtein distance
        bool FuzzyDigitMatch(string ocrText, string expected, int maxErrors)
        {
            var ocrDigits = new string(ocrText.Where(char.IsDigit).ToArray());
            if (ocrDigits.Length < expected.Length - maxErrors) return false;
            // Simple Levenshtein
            int n = expected.Length, m = ocrDigits.Length;
            var dp = new int[n + 1, m + 1];
            for (int i = 0; i <= n; i++) dp[i, 0] = i;
            for (int j = 0; j <= m; j++) dp[0, j] = j;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + (expected[i - 1] == ocrDigits[j - 1] ? 0 : 1));
            return dp[n, m] <= maxErrors;
        }

        // ── Method A11Y: UIA Value Pattern — Standard, most reliable, focusless! ──
        // Try UIA first as the standard approach. Only works on controls that expose Value pattern.
        // MFC custom controls (CMaskEditEx etc.) typically don't support this → fall through to Method 10.
        if (!success && (methodOnly == null || methodOnly == 0))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [A11Y] UIA Value: ");
            Console.ResetColor();

            try
            {
                var uia = new UIA3Automation();
                // Direct hwnd → UIA element (fastest path, no tree walk)
                FlaUI.Core.AutomationElements.AutomationElement? targetEl = null;
                try
                {
                    targetEl = uia.FromHandle(targetHwnd);
                }
                catch { }

                if (targetEl != null && targetEl.Patterns.Value.IsSupported)
                {
                    targetEl.Patterns.Value.Pattern.SetValue(text);
                    Thread.Sleep(200);

                    // Verify via UIA
                    var uiaVal = targetEl.Patterns.Value.Pattern.Value.Value ?? "";
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"[SetValue→\"{uiaVal}\"] ");
                    Console.ResetColor();

                    if (uiaVal.Contains(text))
                    {
                        // If Enter needed, send via PostMessage (no focus steal)
                        if (sendEnter)
                        {
                            uint sc = NativeMethods.MapVirtualKeyW(0x0D, 0);
                            uint downLp = 1u | ((sc & 0xFF) << 16);
                            uint upLp = downLp | (1u << 30) | (1u << 31);
                            NativeMethods.PostMessageW(targetHwnd, 0x0100, (IntPtr)0x0D, (IntPtr)downLp);
                            Thread.Sleep(50);
                            NativeMethods.PostMessageW(targetHwnd, 0x0101, (IntPtr)0x0D, (IntPtr)upLp);
                            Thread.Sleep(1500);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ UIA confirmed \"{text}\" (A11Y, focusless!)");
                        Console.ResetColor();
                        success = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"SetValue sent but value=\"{uiaVal}\"");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(targetEl == null ? "element not found via UIA" : "Value pattern not supported");
                    Console.ResetColor();
                }

                uia.Dispose();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"skip ({ex.Message})");
                Console.ResetColor();
            }
        }

        // ── Method 10: Focusless PostMessage — NO SetFocus, NO foreground steal! ──
        // Best fallback for MFC custom edits (CMaskEditEx etc.) where UIA Value isn't supported.
        // PostMessage delivers WM_CHAR directly to hwnd regardless of focus state.
        // Works even when other windows cover the target. True background automation!
        // Must be tried FIRST (before all other methods) as it's the least intrusive.
        if (!success && (methodOnly == null || methodOnly == 10))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [10] FocuslessPost: ");
            Console.ResetColor();

            InputZoomHost? zoomHost = null;
            try
            {
                IntPtr MkLParam10(uint vk, bool keyUp = false, bool extended = false)
                {
                    uint scanCode = NativeMethods.MapVirtualKeyW(vk, 0);
                    uint lp = 1u;
                    lp |= (scanCode & 0xFF) << 16;
                    if (extended) lp |= (1u << 24);
                    if (keyUp) { lp |= (1u << 30); lp |= (1u << 31); }
                    return (IntPtr)lp;
                }

                // NO foreground/focus change — just MDI activate (sendmessage, not focus)
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);

                // [ZOOM] Hook 1: Create magnifier overlay centered on control
                if (showZoom)
                {
                    try
                    {
                        NativeMethods.GetWindowRect(targetHwnd, out var zCtl);
                        // 3x control size + generous padding for header/status/border
                        int zW = Math.Max(zCtl.Width * 3 + 40, 400);
                        int zH = Math.Max(zCtl.Height * 3 + 80, 180);
                        int zX = zCtl.Left + (zCtl.Width / 2) - (zW / 2);
                        int zY = zCtl.Top + (zCtl.Height / 2) - (zH / 2);
                        // Clamp to screen bounds
                        if (zX < 0) zX = 0;
                        if (zY < 0) zY = 0;

                        zoomHost = new InputZoomHost();
                        zoomHost.Start(zX, zY, zW, zH);
                        zoomHost.UpdateHeader($"[ZOOM] input_m10 \"{text}\"");
                        Console.Write($"[ZOOM@{zX},{zY} {zW}x{zH}] ");

                        // Initial capture
                        var initPng = CaptureControlPng(targetForm.Handle, targetHwnd);
                        if (initPng != null) zoomHost.UpdateImage(initPng);
                        zoomHost.UpdateStatus($"Ready: \"{text}\" ({text.Length} chars)");
                    }
                    catch (Exception zex)
                    {
                        Console.Write($"[ZOOM:ERR {zex.GetType().Name}: {zex.Message}] ");
                        zoomHost = null;
                    }
                }

                // Step 1: Home via PostMessage (cursor to pos 0)
                NativeMethods.PostMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x24 /*VK_HOME*/,
                    MkLParam10(0x24, extended: true));
                Thread.Sleep(10);
                NativeMethods.PostMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x24,
                    MkLParam10(0x24, keyUp: true, extended: true));
                Thread.Sleep(50);

                // Step 2: WM_CHAR only per digit (no WM_KEYDOWN/UP — avoids TranslateMessage doubling)
                int charIdx10 = 0;
                foreach (char ch in text)
                {
                    short vkScan = NativeMethods.VkKeyScanW(ch);
                    byte vk = (byte)(vkScan & 0xFF);
                    IntPtr lp = MkLParam10(vk);
                    NativeMethods.PostMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)ch, lp);
                    charIdx10++;
                    Thread.Sleep(30);

                    // [ZOOM] Hook 2: Update magnifier after each character
                    if (zoomHost?.IsAlive == true)
                    {
                        try
                        {
                            var png = CaptureControlPng(targetForm.Handle, targetHwnd);
                            if (png != null) zoomHost.UpdateImage(png);
                            zoomHost.UpdateStatus($"Typing: {charIdx10}/{text.Length}  \"{text[..charIdx10]}\"");
                        }
                        catch { /* best-effort */ }
                    }
                }
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[typed {text.Length} chars, NO focus] ");
                Console.ResetColor();

                // [ZOOM] Hook 2b: After typing, capture from DESKTOP (real screen, not PrintWindow)
                // Shows what the control looks like on the actual screen — "중계방송 스샷"
                if (zoomHost?.IsAlive == true)
                {
                    try
                    {
                        NativeMethods.GetWindowRect(targetHwnd, out var deskRect);
                        using var deskBmp = ScreenCapture.CaptureScreenRegion(
                            deskRect.Left, deskRect.Top, deskRect.Width, deskRect.Height);
                        if (deskBmp != null && !ScreenCapture.IsBlankBitmap(deskBmp))
                        {
                            var deskPng = ScreenCapture.ToPngBytes(deskBmp);
                            zoomHost.UpdateImage(deskPng);
                            zoomHost.UpdateStatus($"Typed: \"{text}\" — verifying...");
                        }
                    }
                    catch { /* best-effort desktop capture */ }
                }

                // Step 2b: OCR verify BEFORE Enter
                string? preOcr10 = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var r10);
                    using var bmp10 = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (bmp10 != null)
                    {
                        NativeMethods.GetWindowRect(targetForm.Handle, out var fr10);
                        int rx = r10.Left - fr10.Left, ry = r10.Top - fr10.Top;
                        int rw = Math.Min(r10.Width, bmp10.Width - rx);
                        int rh = Math.Min(r10.Height, bmp10.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var crop10 = ScreenCapture.CropRegion(bmp10, rx, ry, rw, rh);
                            var ss10 = Path.Combine(DataDir, "logs", $"input_m10_pre_{DateTime.Now:HHmmss}.png");
                            crop10.Save(ss10);
                            var ocr10 = new WKAppBot.Vision.SimpleOcrAnalyzer();
                            var res10 = ocr10.RecognizeAll(crop10).GetAwaiter().GetResult();
                            preOcr10 = res10.FullText?.Trim();

                            // [ZOOM] Hook 3: Show OCR verification image
                            if (zoomHost?.IsAlive == true)
                            {
                                try
                                {
                                    var ocrPng = ScreenCapture.ToPngBytes(crop10);
                                    zoomHost.UpdateImage(ocrPng);
                                    zoomHost.UpdateStatus($"OCR: \"{preOcr10 ?? "?"}\"");
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[pre-OCR=\"{preOcr10 ?? "?"}\"] ");
                Console.ResetColor();

                // Step 3: Enter via PostMessage (m_bKeyDown flag needed for OnKeyUp query trigger)
                if (sendEnter)
                {
                    NativeMethods.PostMessageW(targetHwnd, 0x0100, (IntPtr)0x0D, MkLParam10(0x0D));
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(targetHwnd, 0x0101, (IntPtr)0x0D, MkLParam10(0x0D, keyUp: true));
                    Thread.Sleep(1500);
                }

                // Step 4: Verify
                Thread.Sleep(300);
                bool ocrOk10 = ConfirmByOcrContains("m10");

                var vSb10 = new StringBuilder(256);
                var vL10 = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vL10 + 1), vSb10);
                var wmT10 = vSb10.ToString();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmT10}\" ");
                Console.ResetColor();

                if (ocrOk10)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ OCR confirmed \"{text}\" (FOCUSLESS!)");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(preOcr10) && preOcr10.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ pre-OCR confirmed \"{text}\" (FOCUSLESS!)");
                    Console.ResetColor();
                    success = true;
                }
                else if (wmT10.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM confirmed \"{text}\" (FOCUSLESS!)");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(preOcr10) && FuzzyDigitMatch(preOcr10, text, maxErrors: 2))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ fuzzy OCR confirmed \"{text}\" (FOCUSLESS!, ocr:\"{preOcr10}\")");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? expected \"{text}\" (focusless might not work for this control)");
                    Console.ResetColor();
                }

                // [ZOOM] Hook 4: Show pass/fail result + desktop screenshot + slow fade
                if (zoomHost?.IsAlive == true)
                {
                    try
                    {
                        bool zoomPass = success;
                        string zoomText = zoomPass ? $"✓ PASS \"{text}\"" : $"? \"{preOcr10 ?? "?"}\"";
                        zoomHost.ShowResult(zoomPass, zoomText);

                        // Final desktop screenshot — show the real screen result
                        try
                        {
                            NativeMethods.GetWindowRect(targetHwnd, out var finalRect);
                            using var finalBmp = ScreenCapture.CaptureScreenRegion(
                                finalRect.Left, finalRect.Top, finalRect.Width, finalRect.Height);
                            if (finalBmp != null && !ScreenCapture.IsBlankBitmap(finalBmp))
                                zoomHost.UpdateImage(ScreenCapture.ToPngBytes(finalBmp));
                        }
                        catch { }

                        // Save zoom overlay screenshot (from desktop — captures the overlay itself)
                        try
                        {
                            NativeMethods.GetWindowRect(targetHwnd, out var zShotRect);
                            int zShotW = Math.Max(zShotRect.Width * 3 + 40, 400);
                            int zShotH = Math.Max(zShotRect.Height * 3 + 80, 180);
                            int zShotX = zShotRect.Left + (zShotRect.Width / 2) - (zShotW / 2);
                            int zShotY = zShotRect.Top + (zShotRect.Height / 2) - (zShotH / 2);
                            if (zShotX < 0) zShotX = 0;
                            if (zShotY < 0) zShotY = 0;
                            Thread.Sleep(300); // brief wait for WPF to render result state
                            using var zShotBmp = ScreenCapture.CaptureScreenRegion(zShotX, zShotY, zShotW, zShotH);
                            // Save to logs
                            var zShotPath = Path.Combine(DataDir, "logs", $"zoom_result_{DateTime.Now:HHmmss}.png");
                            ScreenCapture.SaveToFile(zShotBmp, zShotPath);
                            Console.Write($"[ZOOM:saved {zShotPath}] ");

                            // Also save to ExperienceDB control folder
                            try
                            {
                                // Construct exp dir: profiles/{process}_exp/form_{formId}/controls/cid_{cid}/
                                var proc = System.Diagnostics.Process.GetProcessById(
                                    NativeMethods.GetWindowThreadProcessId(targetHwnd, out var zPid) > 0 ? (int)zPid : 0);
                                var expDir = Path.Combine(DataDir, "profiles", $"{proc?.ProcessName?.ToLower()}_exp",
                                    $"form_{targetFormId}", "controls", $"cid_{targetCid}");
                                if (!Directory.Exists(expDir)) Directory.CreateDirectory(expDir);
                                var expPath = Path.Combine(expDir, "zoom_input.png");
                                ScreenCapture.SaveToFile(zShotBmp, expPath);
                            }
                            catch { /* best-effort exp save */ }
                        }
                        catch { }

                        zoomHost.BeginFadeOut(3000, 800); // 3s hold + 0.8s fade
                        Thread.Sleep(4000); // wait for fade animation to complete
                    }
                    catch { }
                    finally { zoomHost?.Dispose(); zoomHost = null; }
                }
            }
            catch (Exception ex)
            {
                // [ZOOM] Hook 5: Cleanup on error
                zoomHost?.Dispose();
                zoomHost = null;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }
        }

        // ── Method 6: EM_SETSEL + EM_REPLACESEL ──
        // Standard edit control text replacement — selects all, replaces with new text.
        // Unlike WM_SETTEXT, EM_REPLACESEL goes through the control's internal replacement
        // logic and should fire EN_CHANGE notifications to the parent.
        if (!success && (methodOnly == null || methodOnly == 6))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [6] EM_REPLACESEL: ");
            Console.ResetColor();

            try
            {
                const uint EM_SETSEL = 0x00B1;
                const uint EM_REPLACESEL = 0x00C2;

                // Step 1: Select all text
                NativeMethods.SendMessageW(targetHwnd, EM_SETSEL, IntPtr.Zero, (IntPtr)(-1));
                Thread.Sleep(50);

                // Step 2: Replace selection with new text
                NativeMethods.SendMessageW(targetHwnd, EM_REPLACESEL, (IntPtr)1 /*canUndo*/, text);
                Thread.Sleep(200);

                // Step 3: Send Enter if requested
                if (sendEnter)
                {
                    Thread.Sleep(100);
                    NativeMethods.SendMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D, IntPtr.Zero);
                    Thread.Sleep(50);
                    NativeMethods.SendMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x0D, IntPtr.Zero);
                    Thread.Sleep(500);
                }

                // Step 4: Verify
                Thread.Sleep(200);
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var newText = verifySb.ToString();

                // Also OCR verify
                bool ocrOk = ConfirmByOcrContains("m6");

                if (newText.Contains(text) || ocrOk)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM=\"{newText}\" {(ocrOk ? "OCR=✓" : "")}");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? WM=\"{newText}\" {(ocrOk ? "OCR=✓" : "")} (expected \"{text}\")");
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

        // ── Method 7: WM_SETTEXT + WM_COMMAND(EN_CHANGE) notification to parent ──
        // Sets text via WM_SETTEXT (which works for display), then manually sends
        // EN_CHANGE notification to the parent window. This might trick the control's
        // parent (the form) into reading the updated text and updating COM properties.
        if (!success && (methodOnly == null || methodOnly == 7))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [7] SETTEXT+NOTIFY: ");
            Console.ResetColor();

            try
            {
                const uint WM_COMMAND = 0x0111;
                const uint EN_CHANGE = 0x0300;
                const uint EN_UPDATE = 0x0400;

                // Step 1: Set text
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
                Thread.Sleep(100);

                // Step 2: Send EN_CHANGE and EN_UPDATE notifications to parent
                // WM_COMMAND: HIWORD(wParam)=notification, LOWORD(wParam)=controlId, lParam=controlHwnd
                IntPtr parent = NativeMethods.GetParent(targetHwnd);
                int controlId = NativeMethods.GetDlgCtrlID(targetHwnd);
                IntPtr enChangeWp = (IntPtr)((EN_CHANGE << 16) | (controlId & 0xFFFF));
                IntPtr enUpdateWp = (IntPtr)((EN_UPDATE << 16) | (controlId & 0xFFFF));

                NativeMethods.SendMessageW(parent, WM_COMMAND, enChangeWp, targetHwnd);
                Thread.Sleep(50);
                NativeMethods.SendMessageW(parent, WM_COMMAND, enUpdateWp, targetHwnd);
                Thread.Sleep(200);

                // Step 3: Send Enter if requested
                if (sendEnter)
                {
                    Thread.Sleep(100);
                    NativeMethods.SendMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D, IntPtr.Zero);
                    Thread.Sleep(50);
                    NativeMethods.SendMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x0D, IntPtr.Zero);
                    Thread.Sleep(500);
                }

                // Step 4: Verify
                Thread.Sleep(200);
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var newText = verifySb.ToString();

                bool ocrOk = ConfirmByOcrContains("m7");

                if (newText.Contains(text) || ocrOk)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM=\"{newText}\" {(ocrOk ? "OCR=✓" : "")}");
                    Console.ResetColor();
                    success = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"? WM=\"{newText}\" {(ocrOk ? "OCR=✓" : "")} (expected \"{text}\")");
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

        // ── Method 8: Atomic SendInput batch — ALL keystrokes in ONE SendInput call ──
        // Home+Shift+End+chars+Enter injected atomically into the raw input queue.
        // Theory: no inter-key gap = autocomplete can't steal focus between chars.
        // Still needs physical click first to set initial focus.
        if (!success && (methodOnly == null || methodOnly == 8))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [8] AtomicBatch: ");
            Console.ResetColor();

            try
            {
                // InputFocusGuard — pre/post batch focus verification with retry
                var guard8 = new InputFocusGuard(targetHwnd, win.Handle, maxRetries: 3);
                bool m8InputDone = false;

                while (!m8InputDone)
                {
                // Step 0: Activate + foreground + click to focus
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(300);

                NativeMethods.GetWindowRect(targetHwnd, out var m8Rect);
                int m8X = m8Rect.Left + clickXOffset;
                int m8Y = m8Rect.Top + m8Rect.Height / 2;
                MouseInput.Click(m8X, m8Y);
                Thread.Sleep(300);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[click({m8X},{m8Y})] ");
                Console.ResetColor();

                // [GUARD] Verify focus before atomic batch
                if (!guard8.EnsureBeforeKeystroke(maxWaitMs: 2000))
                {
                    if (!guard8.CanRetry()) break;
                    guard8.ClearFieldForRestart();
                    Thread.Sleep(100);
                    continue; // retry from click
                }

                // Step 1: Build single INPUT array — Home + Shift+End + chars + Enter
                var inputList = new System.Collections.Generic.List<INPUT>();

                // Helper: add VK keydown+keyup
                void AddVkKey(ushort vk, bool extended = false, uint extraDown = 0, uint extraUp = 0)
                {
                    uint sc = NativeMethods.MapVirtualKeyW(vk, 0);
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT
                        {
                            wVk = vk, wScan = (ushort)sc,
                            dwFlags = (extended ? KeyFlags.KEYEVENTF_EXTENDEDKEY : 0) | extraDown
                        }}
                    });
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT
                        {
                            wVk = vk, wScan = (ushort)sc,
                            dwFlags = KeyFlags.KEYEVENTF_KEYUP | (extended ? KeyFlags.KEYEVENTF_EXTENDEDKEY : 0) | extraUp
                        }}
                    });
                }

                // Helper: add modifier down only
                void AddModDown(ushort vk)
                {
                    uint sc = NativeMethods.MapVirtualKeyW(vk, 0);
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT { wVk = vk, wScan = (ushort)sc, dwFlags = 0 } }
                    });
                }

                // Helper: add modifier up only
                void AddModUp(ushort vk)
                {
                    uint sc = NativeMethods.MapVirtualKeyW(vk, 0);
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT { wVk = vk, wScan = (ushort)sc, dwFlags = KeyFlags.KEYEVENTF_KEYUP } }
                    });
                }

                // Home (extended)
                AddVkKey(0x24 /*VK_HOME*/, extended: true);

                // Shift down → End (extended) → Shift up = select all
                AddModDown(0x10 /*VK_SHIFT*/);
                AddVkKey(0x23 /*VK_END*/, extended: true);
                AddModUp(0x10);

                // Characters via UNICODE
                foreach (char ch in text)
                {
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT { wVk = 0, wScan = ch, dwFlags = KeyFlags.KEYEVENTF_UNICODE } }
                    });
                    inputList.Add(new INPUT
                    {
                        type = INPUT.INPUT_KEYBOARD,
                        u = new InputUnion { ki = new KEYBDINPUT { wVk = 0, wScan = ch, dwFlags = KeyFlags.KEYEVENTF_UNICODE | KeyFlags.KEYEVENTF_KEYUP } }
                    });
                }

                // Enter (if requested)
                if (sendEnter)
                    AddVkKey(0x0D /*VK_RETURN*/);

                // Step 2: Fire all at once
                var inputArray = inputList.ToArray();
                uint sent = NativeMethods.SendInput((uint)inputArray.Length, inputArray, Marshal.SizeOf<INPUT>());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[{sent}/{inputArray.Length} events] ");
                Console.ResetColor();

                m8InputDone = true; // batch sent, exit retry loop
                } // end while(!m8InputDone)

                Thread.Sleep(sendEnter ? 2000 : 500);

                // Step 3: Verify via OCR
                bool ocrOk8 = ConfirmByOcrContains("m8");

                var vSb8 = new StringBuilder(256);
                var vL8 = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vL8 + 1), vSb8);
                var wmT8 = vSb8.ToString();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmT8}\" ");
                Console.ResetColor();

                if (ocrOk8)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (wmT8.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM confirmed \"{text}\"");
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

        // ── Method 9: Pure PostMessage pipeline — NO physical input, focus-immune ──
        // CMaskEditEx is a masked edit: cursor overwrites character at current position.
        // Strategy: SetFocus(programmatic) → PostMessage Home (cursor pos 0) → PostMessage chars
        // All messages target the specific hwnd via PostMessage — immune to focus steal by autocomplete.
        // Key insight: PostMessage goes to the specified hwnd regardless of which window has focus.
        if (!success && (methodOnly == null || methodOnly == 9))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [9] PostMsgPipe: ");
            Console.ResetColor();

            try
            {
                IntPtr MakeCharLParam(uint vk, bool keyUp = false, bool extended = false)
                {
                    uint scanCode = NativeMethods.MapVirtualKeyW(vk, 0);
                    uint lp = 1u; // repeat count = 1
                    lp |= (scanCode & 0xFF) << 16;
                    if (extended) lp |= (1u << 24);
                    if (keyUp) { lp |= (1u << 30); lp |= (1u << 31); }
                    return (IntPtr)lp;
                }

                // Step 0: MDI activate + foreground (but NO click on control)
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(200);

                // Step 1: Programmatic SetFocus (no physical click = no autocomplete trigger)
                uint thr9 = NativeMethods.GetWindowThreadProcessId(targetHwnd, out _);
                uint our9 = NativeMethods.GetCurrentThreadId();
                bool att9 = thr9 != our9 && NativeMethods.AttachThreadInput(our9, thr9, true);
                var prevFocus = NativeMethods.SetFocus(targetHwnd);
                Thread.Sleep(100);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[SetFocus prev={prevFocus:X8}] ");
                Console.ResetColor();

                // Step 2: Home → move cursor to position 0 (PostMessage, not physical)
                NativeMethods.PostMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x24 /*VK_HOME*/,
                    MakeCharLParam(0x24, extended: true));
                Thread.Sleep(10);
                NativeMethods.PostMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x24,
                    MakeCharLParam(0x24, keyUp: true, extended: true));
                Thread.Sleep(50);

                // Step 3: Type each char via WM_CHAR ONLY (no WM_KEYDOWN/UP for digits!)
                // CRITICAL: TranslateMessage in target's message loop auto-generates WM_CHAR
                // from WM_KEYDOWN. If we also send explicit WM_CHAR, each char is entered TWICE.
                // For digits: WM_CHAR only → single entry into m_strBuffer
                // For Home/Enter: WM_KEYDOWN+WM_KEYUP needed (cursor nav / m_bKeyDown flag)
                foreach (char ch in text)
                {
                    short vkScan = NativeMethods.VkKeyScanW(ch);
                    byte vk = (byte)(vkScan & 0xFF);
                    IntPtr downLp = MakeCharLParam(vk);

                    // WM_CHAR only — no WM_KEYDOWN/UP to avoid TranslateMessage doubling
                    NativeMethods.PostMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)ch, downLp);
                    Thread.Sleep(30);
                }
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[typed {text.Length} chars] ");
                Console.ResetColor();

                // Step 3b: OCR verify BEFORE Enter
                string? preOcr9 = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var pre9R);
                    using var pre9Bmp = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (pre9Bmp != null)
                    {
                        NativeMethods.GetWindowRect(targetForm.Handle, out var pre9FR);
                        int rx = pre9R.Left - pre9FR.Left;
                        int ry = pre9R.Top - pre9FR.Top;
                        int rw = Math.Min(pre9R.Width, pre9Bmp.Width - rx);
                        int rh = Math.Min(pre9R.Height, pre9Bmp.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var crop9 = ScreenCapture.CropRegion(pre9Bmp, rx, ry, rw, rh);
                            var ss9 = Path.Combine(DataDir, "logs", $"input_m9_pre_{DateTime.Now:HHmmss}.png");
                            crop9.Save(ss9);
                            var ocr9 = new WKAppBot.Vision.SimpleOcrAnalyzer();
                            var res9 = ocr9.RecognizeAll(crop9).GetAwaiter().GetResult();
                            preOcr9 = res9.FullText?.Trim();
                        }
                    }
                }
                catch { }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[pre-OCR=\"{preOcr9 ?? "?"}\"] ");
                Console.ResetColor();

                // Step 4: Enter via PostMessage (CCtlItemCode::OnKeyUp(VK_RETURN) triggers query)
                if (sendEnter)
                {
                    // Must send WM_KEYDOWN first — m_bKeyDown flag required for OnKeyUp to fire
                    NativeMethods.PostMessageW(targetHwnd, 0x0100, (IntPtr)0x0D, MakeCharLParam(0x0D));
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(targetHwnd, 0x0101, (IntPtr)0x0D, MakeCharLParam(0x0D, keyUp: true));
                    Thread.Sleep(1500);
                }

                // Step 5: Detach thread
                if (att9) NativeMethods.AttachThreadInput(our9, thr9, false);

                // Step 6: Verify
                Thread.Sleep(300);
                bool ocrOk9 = ConfirmByOcrContains("m9");

                var vSb9 = new StringBuilder(256);
                var vL9 = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vL9 + 1), vSb9);
                var wmT9 = vSb9.ToString();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmT9}\" ");
                Console.ResetColor();

                if (ocrOk9)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(preOcr9) && preOcr9.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ pre-enter OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (wmT9.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ WM confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(preOcr9) && FuzzyDigitMatch(preOcr9, text, maxErrors: 2))
                {
                    // Tiny 130x20 MFC bitmap font OCR is unreliable — fuzzy match as fallback
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ fuzzy OCR confirmed \"{text}\" (ocr:\"{preOcr9}\")");
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

        // ── Method 4: 100% Physical SendInput pipeline (Click + Ctrl+A + Type + Enter) ──
        // v6: SOURCE-CODE-INFORMED approach (HTS_Project/RunControls/ScItemCodeCtl/CtlItemCode.cpp)
        //
        // KEY DISCOVERY: CCtlItemCode::OnKeyUp(VK_RETURN) directly calls:
        //   SetItemName() → InsertToHistory() → FireEditEnter() → RunEventCommTR() → ProcPutLinkData()
        //   This is the REAL data query trigger! Not WMU_EDITENTER (which is dead code).
        //
        // CMaskEditEx::OnChar() processes physical keystrokes → updates m_strBuffer → paints display.
        // CCtlItemCode::OnKeyUp(VK_RETURN) reads m_strBuffer, validates via IsValidCode(),
        //   then fires COM events and runs RunEventCommTR() for data query.
        //
        // Previous v5 problem: Physical Enter probably lost focus or went to wrong window.
        // v6 fix: Re-verify focus before Enter + multiple focus recovery attempts.
        if (!success && (methodOnly == null || methodOnly == 4))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [4] SendInput(Click+Type+Enter): ");
            Console.ResetColor();

            try
            {
                // InputFocusGuard — pre-batch focus verification with retry
                var guard4 = new InputFocusGuard(targetHwnd, win.Handle, maxRetries: 3);
                bool m4InputDone = false;

                while (!m4InputDone)
                {
                // Step 0: Activate MDI form + foreground
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(200);
                NativeMethods.SmartSetForegroundWindow(win.Handle);
                Thread.Sleep(300);

                // Step 1: Physical click on control LEFT side to focus it
                NativeMethods.GetWindowRect(targetHwnd, out var m4Rect);
                int m4ClickX = m4Rect.Left + clickXOffset;
                int m4ClickY = m4Rect.Top + m4Rect.Height / 2;
                MouseInput.Click(m4ClickX, m4ClickY);
                Thread.Sleep(300);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[click({m4ClickX},{m4ClickY})] ");

                // Step 2: Select all (Home → Shift+End is reliable for CMaskEdit)
                KeyboardInput.PressKey("home");
                Thread.Sleep(50);
                KeyboardInput.Hotkey(new[] { "shift", "end" });
                Thread.Sleep(150);

                // [GUARD] Verify focus before atomic SendInput batch
                if (!guard4.EnsureBeforeKeystroke(maxWaitMs: 2000))
                {
                    Console.ResetColor();
                    if (!guard4.CanRetry()) break;
                    guard4.ClearFieldForRestart();
                    Thread.Sleep(100);
                    continue; // retry from activation
                }

                // Step 3: Type ALL chars at once via single SendInput batch (no inter-char delay!)
                // WHY: Autocomplete dropdown steals focus during typing. By sending all chars
                // in one burst, they're all queued in the raw input queue before the message pump
                // can process any OnChar and trigger autocomplete.
                //
                // NOTE: Do NOT send ESC after! CCtlItemCode::OnKeyDown(VK_ESCAPE) calls Clear()!
                {
                    // Build a single INPUT array with all chars (keydown+keyup for each)
                    var inputs = new INPUT[text.Length * 2];
                    for (int i = 0; i < text.Length; i++)
                    {
                        inputs[i * 2].type = INPUT.INPUT_KEYBOARD;
                        inputs[i * 2].u.ki.wVk = 0;
                        inputs[i * 2].u.ki.wScan = text[i];
                        inputs[i * 2].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE;

                        inputs[i * 2 + 1].type = INPUT.INPUT_KEYBOARD;
                        inputs[i * 2 + 1].u.ki.wVk = 0;
                        inputs[i * 2 + 1].u.ki.wScan = text[i];
                        inputs[i * 2 + 1].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE | KeyFlags.KEYEVENTF_KEYUP;
                    }
                    NativeMethods.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<INPUT>());
                    Console.Write(text);
                }
                Console.Write(" ");
                Console.ResetColor();

                m4InputDone = true; // batch sent, exit retry loop
                } // end while(!m4InputDone)

                Thread.Sleep(500); // Let CMaskEdit fully process all chars + autocomplete settle

                // Step 3b: If autocomplete stole focus, just note it.
                // We'll use PostMessage for Enter so focus location doesn't matter.
                var autoFocus = NativeMethods.GetFocusedHwndInProcess(win.Handle);
                if (autoFocus != IntPtr.Zero && autoFocus != targetHwnd)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"[autocomplete={autoFocus:X8}] ");
                    Console.ResetColor();
                }

                // Step 4: OCR verify BEFORE Enter — confirm text is displayed correctly
                string? ocrBeforeEnter = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var preRect);
                    using var preBmp = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (preBmp != null)
                    {
                        NativeMethods.GetWindowRect(targetForm.Handle, out var preFormRect);
                        int rx = preRect.Left - preFormRect.Left;
                        int ry = preRect.Top - preFormRect.Top;
                        int rw = Math.Min(preRect.Width, preBmp.Width - rx);
                        int rh = Math.Min(preRect.Height, preBmp.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var cropBmp = ScreenCapture.CropRegion(preBmp, rx, ry, rw, rh);
                            var ssPath = Path.Combine(DataDir, "logs", $"input_m4_pre_{DateTime.Now:HHmmss}.png");
                            cropBmp.Save(ssPath);
                            var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                            var ocrResult = ocr.RecognizeAll(cropBmp).GetAwaiter().GetResult();
                            ocrBeforeEnter = ocrResult.FullText?.Trim();
                        }
                    }
                }
                catch { }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[pre-enter OCR=\"{ocrBeforeEnter ?? "?"}\"] ");
                Console.ResetColor();

                // Step 5: Check focus (informational). PostMessage Enter doesn't need focus.
                var focusHwnd = NativeMethods.GetFocusedHwndInProcess(win.Handle);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[focus={focusHwnd:X8} target={targetHwnd:X8}");
                if (focusHwnd == targetHwnd)
                    Console.Write(" OK");
                else
                    Console.Write(" MISMATCH!");
                Console.Write("] ");
                Console.ResetColor();

                // Step 6: Send Enter directly via PostMessage WM_KEYDOWN + WM_KEYUP
                // to the target HWND. This ensures CCtlItemCode::OnKeyUp(VK_RETURN) fires
                // on the correct control, regardless of where the keyboard focus actually is.
                // OnKeyUp(VK_RETURN) → SetItemName() → RunEventCommTR() → data query
                if (sendEnter)
                {
                    NativeMethods.PostMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x0D /*VK_RETURN*/, IntPtr.Zero);
                    Thread.Sleep(50);
                    NativeMethods.PostMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x0D /*VK_RETURN*/, IntPtr.Zero);
                    Thread.Sleep(1500); // RunEventCommTR needs time to query server
                }

                // Step 7: Verify via OCR (post-Enter)
                Thread.Sleep(300);
                string? ocrText = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var verifyRect);
                    using var formBmp = ScreenCapture.CaptureWindow(targetForm.Handle);
                    if (formBmp != null)
                    {
                        NativeMethods.GetWindowRect(targetForm.Handle, out var formRect);
                        int rx = verifyRect.Left - formRect.Left;
                        int ry = verifyRect.Top - formRect.Top;
                        int rw = Math.Min(verifyRect.Width, formBmp.Width - rx);
                        int rh = Math.Min(verifyRect.Height, formBmp.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var cropBmp = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                            var ssPath = Path.Combine(DataDir, "logs", $"input_m4_verify_{DateTime.Now:HHmmss}.png");
                            cropBmp.Save(ssPath);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"[ss: {Path.GetFileName(ssPath)}] ");
                            Console.ResetColor();

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

                // Also check WM_GETTEXT (CWnd text - may be synced after EditFullCaption path)
                var verifySb = new StringBuilder(256);
                var vLen = (int)NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                NativeMethods.SendMessageW(targetHwnd, NativeMethods.WM_GETTEXT, (IntPtr)(vLen + 1), verifySb);
                var wmText = verifySb.ToString();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"WM=\"{wmText}\" ");
                if (!string.IsNullOrEmpty(ocrText))
                    Console.Write($"OCR=\"{ocrText}\" ");
                Console.ResetColor();

                // Success check priority:
                // 1. Post-enter OCR (most reliable — visual confirmation after Enter)
                // 2. Pre-enter OCR (typing confirmed before Enter — CMaskEditEx doesn't update CWnd text)
                // 3. WM_GETTEXT (CWnd buffer — only works if SetWindowText was called)
                //
                // CMaskEditEx::OnChar() updates m_strBuffer + DrawCaption (display) but NOT SetWindowText.
                // So WM_GETTEXT/UIA Name always show stale text. Pre-enter OCR is the real truth.
                if (!string.IsNullOrEmpty(ocrText) && ocrText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ post-enter OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(ocrBeforeEnter) && ocrBeforeEnter.Contains(text))
                {
                    // CMaskEditEx: display shows correct text (m_strBuffer updated via OnChar)
                    // but CWnd text (WM_GETTEXT/UIA) is stale. Trust the pre-enter OCR.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ pre-enter OCR confirmed \"{text}\" (CWnd text stale: \"{wmText}\")");
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

                // InputFocusGuard — verify focus before SendInput portion
                var guard5 = new InputFocusGuard(targetHwnd, win.Handle, maxRetries: 3);
                bool m5InputDone = false;

                while (!m5InputDone)
                {
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

                // [GUARD] Verify focus before SendInput selection/delete
                if (!guard5.EnsureBeforeKeystroke(maxWaitMs: 2000))
                {
                    if (!guard5.CanRetry()) break;
                    guard5.ClearFieldForRestart();
                    Thread.Sleep(100);
                    continue; // retry from activation
                }

                // Step 2: Select all via Ctrl+A (CMaskEdit::PreTranslateMessage handles Ctrl shortcuts)
                // Actually use Home + Shift+End which is more reliable for CMaskEdit
                KeyboardInput.PressKey("home");
                Thread.Sleep(30);
                KeyboardInput.Hotkey(new[] { "shift", "end" });
                Thread.Sleep(100);

                // Step 3: Delete selected text
                KeyboardInput.PressKey("delete");
                Thread.Sleep(100);

                m5InputDone = true; // SendInput part done, PostMessage below is focusless
                } // end while(!m5InputDone)

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
                else if (!RequireOcrForNonA11y && !string.IsNullOrEmpty(wmText5) && wmText5.Contains(text))
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
                // InputFocusGuard — verify focus after SetFocus
                var guard1 = new InputFocusGuard(targetHwnd, win.Handle, maxRetries: 3);

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

                // [GUARD] Verify focus after SetFocus
                // Note: Method 1 uses SendMessage (not SendInput) for WM_CHAR, so guard is
                // advisory — SendMessage goes to specified hwnd regardless of focus.
                // But SetFocus failure means the control may not process input correctly.
                if (!guard1.CheckBeforeKeystroke().Ok)
                {
                    Console.WriteLine("[GUARD] Warning: focus not on target after SetFocus (SendMessage may still work)");
                }

                // Select all existing text (EM_SETSEL 0,-1 = select all)
                NativeMethods.SendMessageW(targetHwnd, 0x00B1 /*EM_SETSEL*/, IntPtr.Zero, (IntPtr)(-1));
                Thread.Sleep(50);

                // Delete selected text first (backspace to clear)
                NativeMethods.SendMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)0x08 /*VK_BACK*/, IntPtr.Zero);
                Thread.Sleep(50);

                // Send each character via WM_CHAR (SendMessage — goes to specified hwnd)
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
                    var ocrOk = !RequireOcrForNonA11y || ConfirmByOcrContains("m1");
                    if (ocrOk)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ \"{newText}\" (+OCR)");
                        Console.ResetColor();
                        success = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("? text ok but OCR mismatch");
                        Console.ResetColor();
                    }
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

                // InputFocusGuard — per-keystroke focus verification
                var guard = new InputFocusGuard(targetHwnd, win.Handle, maxRetries: 3);
                bool m2InputDone = false;

                while (!m2InputDone)
                {
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

                    // Type each digit with per-keystroke guard
                    // IMPORTANT: Use CheckBeforeKeystroke (detect-only, no recovery).
                    // If ANY interference detected, immediately restart the entire input.
                    // Reason: Autocomplete dropdown destroys selection state (Home+Shift+End)
                    // so even if focus is restored, cursor position is wrong — must restart.
                    bool interrupted = false;
                    foreach (char ch in text)
                    {
                        // [GUARD] Check focus before each keystroke — detect only
                        var guardCheck = guard.CheckBeforeKeystroke();
                        if (!guardCheck.Ok)
                        {
                            Console.WriteLine($"[GUARD] Interrupted: {guardCheck.Type} — {guardCheck.Detail}");
                            interrupted = true;
                            break;
                        }

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

                    if (interrupted)
                    {
                        // Interference during typing — dismiss autocomplete + clear + retry
                        if (!guard.CanRetry()) break;
                        // Dismiss autocomplete dropdown with ESC via PostMessage
                        NativeMethods.PostMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x1B /*VK_ESCAPE*/, IntPtr.Zero);
                        Thread.Sleep(50);
                        NativeMethods.PostMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x1B, IntPtr.Zero);
                        Thread.Sleep(200);
                        guard.ClearFieldForRestart();
                        Thread.Sleep(100);
                        continue; // restart the while loop (click + Home + Shift+End + type)
                    }

                    m2InputDone = true; // all chars typed successfully
                }

                Thread.Sleep(200);

                if (sendEnter)
                {
                    // [GUARD] Final focus check before Enter
                    if (guard.EnsureBeforeKeystroke(maxWaitMs: 1000))
                        KeyboardInput.PressKey("enter");
                    else
                        Console.WriteLine("[GUARD] Focus lost before Enter — skipping Enter key");
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
                    var ocrOk = !RequireOcrForNonA11y || ConfirmByOcrContains("m2");
                    if (ocrOk)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✓ \"{checkText}\" (via {(!string.IsNullOrEmpty(wmText) ? "WM_GETTEXT" : "UIA")} +OCR)");
                        Console.ResetColor();
                        success = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("? text ok but OCR mismatch");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"? WM_GETTEXT=\"{wmText}\"");
                    if (uiaName != null) Console.Write($" UIA=\"{uiaName}\"");
                    Console.WriteLine($" (expected \"{text}\")");
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

