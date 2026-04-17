using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: input command -- Type text into MDI form controls (5 strategies)
internal partial class Program
{
    // =======================================================================
    // input -- Type text into a specific control (MDI form + control ID)
    //
    // Tries multiple strategies:
    //   3. WM_SETTEXT (focusless/direct text replacement)
    //   4. PostMessage WM_KEYDOWN/WM_CHAR (focus + message hybrid)
    //   5. Click + PostMessage WM_CHAR (physical focus + message)
    //   1. Focus + WM_CHAR (AttachThread + SetFocus + SendMessage)
    //   2. Click + VK-Type (physical keyboard input)
    // =======================================================================
    static int InputCommand(string[] args)
    {
        if (args.Length < 3)
            return Error(@"Usage: wkappbot input <window-title> <form-id> <text> [--cid N] [--enter] [--method N]
  Types text into a control in an MDI form.

Strategies (tried in order, or specify --method):
A11Y: UIA Value   -- standard accessibility (focusless, works on standard edits)
 10: Focusless     -- pure PostMessage WITHOUT SetFocus (MFC custom edits, focusless!)
  9: PostMsgPipe   -- pure PostMessage: SetFocus+Home+chars+Enter to hwnd (focus-immune)
  6: EM_REPLACESEL -- select-all + EM_REPLACESEL (standard edit replace, fires EN_CHANGE)
  7: SETTEXT+NOTIFY -- WM_SETTEXT + WM_COMMAND(EN_CHANGE) to parent (force change notification)
  3: WM_SETTEXT    -- direct text replacement (updates Win32 text, not internal buffer)
  8: AtomicBatch   -- single SendInput call: Home+Shift+End+chars+Enter (no inter-key gap)
  4: PostMsg       -- SetFocus + PostMessage WM_KEYDOWN/WM_CHAR with proper lParam
  5: Click+PostMsg -- physical click (left area) -> PostMessage WM_CHAR (hybrid, best for CMaskEdit)
  1: Focus+WM_CHAR -- AttachThread + SetFocus + SendMessage WM_CHAR
  2: Click+Type    -- physical click on field -> Home+Shift+End -> SendInput VK

Options:
  --cid N           Target control ID (default: 3780, the stock code field)
  --enter           Send Enter after typing (trigger data request)
  --method M        Use only one method (number or keyword)
                    Numbers: 1..10
                    Keywords: replacesel|settext-notify|settext|atomic|postpipe|focusless|postmsg|click-post|focus-char|click-type
  --click-x N       Override click X offset from control left edge (default: 30)

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
        // 돋보기 항상 표시 (입력확보 과정 시각화)

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

        // Find target window -- try all matches until one has MDIClient
        // (e.g. "투혼" matches both main window and #32770 dialogs)
        var windows = WindowFinder.FindWindows(title);
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

        // [READINESS] 입력위치확보: 전수조사 + 노하우 + 방해꾼 + 유저 간섭 분석
        var readiness = CreateInputReadiness();
        var readinessReport = readiness.Probe(new InputReadinessRequest
        {
            TargetHwnd = win.Handle,
            IntendedAction = "input",
            MainHwnd = win.Handle,
            FormId = targetFormId,
            ControlId = targetCid,
            SkipZoom = false, // 돋보기 항상 표시
        });
        InputReadiness.PrintReport(readinessReport);

        // Check elevation (use readiness report)
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint targetPid);
        if (readinessReport.ElevationMismatch)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  X Target is elevated but wkappbot is not. Run as admin.");
            Console.ResetColor();
            readinessReport.Zoom?.ShowFail("Elevation mismatch");
            return 1;
        }
        bool weAreElevated = readinessReport.WeAreElevated;
        bool targetElevated = readinessReport.TargetElevated;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  v Elevated -- input enabled");
        Console.ResetColor();

        // Auto-dismiss blocker if detected by readiness
        if (readinessReport.ActiveBlocker != null)
        {
            readiness.TryDismissBlocker(win.Handle, readinessReport.ActiveBlocker);
            Thread.Sleep(300);
        }

        // Load ExperienceDb from matching profile (best-effort)
        ExperienceDb? expDb = null;
        try
        {
            var profileStore = new ProfileStore();
            string procName = "";
            try { procName = System.Diagnostics.Process.GetProcessById((int)targetPid).ProcessName; } catch { }
            var profileMatch = profileStore.FindByMatch(win.ClassName, "")
                ?? (!string.IsNullOrEmpty(procName) ? profileStore.FindByMatch("", procName) : null);
            if (profileMatch != null)
            {
                var expDir = Path.Combine(profileStore.ProfileDir, $"{profileMatch.Value.name}_exp");
                expDb = new ExperienceDb(expDir);
            }
        }
        catch { /* best-effort -- proceed without experience DB */ }

        // -- Pre-input: auto-dismiss "알림!!" / alert popups that block MDI access --
        TryDismissAlertPopups(win.Handle, mdiClient);

        // First check: is there a specific MDI child the user wants?
        // For now, find the FIRST visible form matching the ID (Z-order top = first)
        var mdiChildren = WindowFinder.GetChildrenZOrder(mdiClient);
        var matchingForms = mdiChildren.Where(f =>
            f.Title.Contains($"[{targetFormId}]")).ToList();
        if (matchingForms.Count == 0) return Error($"Form [{targetFormId}] not found");

        // Select the best form: MDI active -> focus ancestor -> largest visible -> Z-order first
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
                // Priority 2: GetGUIThreadInfo -- find which form contains hwndFocus
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

        // Show past failure history + knowhow for this form (if any)
        if (expDb != null)
        {
            ShowFormExperienceHints(expDb, targetFormId, actionName: "input");
        }

        // === INPUT READINESS CHECK ===
        // Verify the target form is in a state where input can succeed
        {
            bool formVisible = NativeMethods.IsWindowVisible(targetForm.Handle);
            bool formIconic = NativeMethods.IsIconic(targetForm.Handle);
            bool formEnabled = NativeMethods.IsWindowEnabled(targetForm.Handle);

            if (!formVisible)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ! Form is NOT visible -- input may go to hidden window");
                Console.ResetColor();
            }
            if (formIconic)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ! Form is minimized (iconic)");
                Console.ResetColor();
            }
            if (!formEnabled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ! Form is disabled -- a modal dialog may be blocking");
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
                Console.WriteLine($"  X BLOCKER detected: {blockerInfo}");
                Console.ResetColor();

                // Try to auto-dismiss using dialog handlers (login, alert, etc.)
                var handlersDir2 = Path.Combine(DataDir, "handlers");
                var preHandlerMgr = Directory.Exists(handlersDir2) ? new DialogHandlerManager(handlersDir2) : null;
                var (preHandled, _) = TryHandleBlocker(win.Handle, preHandlerMgr);
                if (preHandled)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  v [BLOCK] Pre-input blocker auto-dismissed");
                    Console.ResetColor();
                    Thread.Sleep(300); // wait for app to recover
                }
                else
                {
                    Console.WriteLine("    (a dialog may be blocking the app's message pump)");
                }
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
        Console.WriteLine($" -> \"{text}\"");
        Console.ResetColor();

        // Show control-level experience hints (past failures + knowhow for this cid)
        if (expDb != null)
        {
            ShowControlExperienceHints(expDb, targetFormId, targetCid, actionName: "input");
        }

        bool success = false;

        // OCR debug images -> experience DB form dir if available, else logs/ocr/
        string OcrDebugPath(string tag)
        {
            string dir;
            string prefix;
            if (expDb != null)
            {
                dir = Path.Combine(expDb.ExpDir, $"form_{targetFormId}");
                prefix = "ocr_";
            }
            else
            {
                dir = Path.Combine(DataDir, "logs", "ocr");
                prefix = "input_";
            }
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, $"{prefix}{tag}_{DateTime.Now:HHmmss}.png");
        }

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
                var ssPath = OcrDebugPath($"{methodTag}_ocr");
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
        // Used for: initial capture, post-type, OCR target, final result (quality matters)
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

        // [ZOOM] Fast capture for live preview -- CopyFromScreen (~15-25ms, no blocking!)
        // PrintWindow is too slow on MFC apps (1000ms+ due to WM_PRINT message blocking).
        // CopyFromScreen captures whatever is visible -- if control is covered by another window,
        // the preview shows the covering window, which is acceptable for live preview.
        // BMP format (no PNG compression) saves ~10-30ms per frame.
        byte[]? CaptureControlFast(IntPtr formHandle, IntPtr controlHandle)
        {
            try
            {
                NativeMethods.GetWindowRect(controlHandle, out var cr);
                if (cr.Width <= 0 || cr.Height <= 0) return null;

                using var bmp = ScreenCapture.CaptureScreenRegion(cr.Left, cr.Top, cr.Width, cr.Height);
                if (ScreenCapture.IsBlankBitmap(bmp)) return null;

                using var ms = new System.IO.MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
            catch { return null; }
        }

        // Raw BGRA pixel capture -- no codec encode/decode. ~10x faster than BMP path.
        // For live zoom updates: foreground -> CopyFromScreen, background -> PrintWindow+crop.
        (byte[] pixels, int w, int h, int stride)? CaptureControlRawFast(IntPtr formHandle, IntPtr controlHandle)
        {
            try
            {
                NativeMethods.GetWindowRect(controlHandle, out var cr);
                if (cr.Width <= 0 || cr.Height <= 0) return null;

                // Foreground check: CopyFromScreen is fastest for visible windows
                var fgHwnd = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(formHandle, out uint formPid);
                NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fgPid);
                bool isFg = (formPid == fgPid);

                System.Drawing.Bitmap? bmp = null;
                try
                {
                    if (isFg)
                    {
                        bmp = ScreenCapture.CaptureScreenRegion(cr.Left, cr.Top, cr.Width, cr.Height);
                        if (ScreenCapture.IsBlankBitmap(bmp)) { bmp?.Dispose(); bmp = null; }
                    }

                    // Background (or screen capture blank) -> PrintWindow+crop (Z-order safe)
                    if (bmp == null)
                    {
                        NativeMethods.GetWindowRect(formHandle, out var fr);
                        int rx = cr.Left - fr.Left, ry = cr.Top - fr.Top;
                        if (rx >= 0 && ry >= 0)
                        {
                            using var formBmp = ScreenCapture.TryPrintWindowOnly(formHandle);
                            if (formBmp != null)
                            {
                                int rw = Math.Min(cr.Width, formBmp.Width - rx);
                                int rh = Math.Min(cr.Height, formBmp.Height - ry);
                                if (rw > 0 && rh > 0)
                                    bmp = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                            }
                        }
                    }

                    if (bmp == null || bmp.Width <= 0 || bmp.Height <= 0) return null;

                    var lockRect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
                    var data = bmp.LockBits(lockRect,
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    try
                    {
                        var bytes = new byte[data.Stride * data.Height];
                        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                        return (bytes, bmp.Width, bmp.Height, data.Stride);
                    }
                    finally { bmp.UnlockBits(data); }
                }
                finally { bmp?.Dispose(); }
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

        // === Orchestrator: 12-tier input fallback chain ===
        // Each method extracted to TryInputMethodN() in partial class files.
        // Unpack-and-call pattern: zero behavioral change from original if-chain.
        var inputCtx = new InputContext
        {
            Title = title,
            Text = text,
            TargetFormId = targetFormId,
            TargetCid = targetCid,
            SendEnter = sendEnter,
            MethodOnly = methodOnly,
            ClickXOffset = clickXOffset,
            Win = win,
            MdiClient = mdiClient,
            TargetForm = targetForm,
            TargetHwnd = targetHwnd,
            CtlRect = ctlRect,
            ExpDb = expDb,
            ReadinessReport = readinessReport,
            OcrDebugPath = OcrDebugPath,
            ConfirmByOcrContains = ConfirmByOcrContains,
            CaptureControlPng = CaptureControlPng,
            CaptureControlFast = CaptureControlFast,
            CaptureControlRawFast = CaptureControlRawFast,
            FuzzyDigitMatch = FuzzyDigitMatch,
        };

        // Try each method in empirical priority order (hardcoded -- this IS the domain knowledge)
        var inputMethods = new (int num, string name, Func<InputContext, InputResult> fn)[]
        {
            (0,  "A11Y",         TryInputMethodA11y),
            (10, "Focusless",    TryInputMethod10),
            (6,  "EM_REPLACESEL",TryInputMethod6),
            (7,  "SETTEXT+NOTIFY",TryInputMethod7),
            (3,  "WM_SETTEXT",   TryInputMethod3),
            (8,  "AtomicBatch",  TryInputMethod8),
            (9,  "PostMsgPipe",  TryInputMethod9),
            (4,  "PhysicalInput",TryInputMethod4),
            (5,  "Click+PostMsg",TryInputMethod5),
            (1,  "Focus+WM_CHAR",TryInputMethod1),
            (2,  "Click+Type",   TryInputMethod2),
        };

        foreach (var (num, name, fn) in inputMethods)
        {
            if (success) break;
            if (inputCtx.MethodOnly != null && inputCtx.MethodOnly != num) continue;
            if (inputCtx.MethodOnly == null || inputCtx.MethodOnly == num)
            {
                var result = fn(inputCtx);
                if (result == InputResult.Success) success = true;
                else if (result == InputResult.Abort) break;
            }
        }


        // Post-input: check for alert/popup dialogs that may have appeared
        // (e.g., "알림!!" after invalid input, "로그인" reconnection, etc.)
        Thread.Sleep(200); // brief wait for dialog to appear
        try
        {
            var handlersDir = Path.Combine(DataDir, "handlers");
            var postHandlerMgr = Directory.Exists(handlersDir) ? new DialogHandlerManager(handlersDir) : null;
            var (handled, _) = TryHandleBlocker(win.Handle, postHandlerMgr);
            if (handled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[INPUT] Post-input alert dialog auto-dismissed");
                Console.ResetColor();
            }
        }
        catch { /* best-effort -- don't fail input because of dismiss failure */ }

        // Summary
        Console.WriteLine();
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  v Input successful: \"{text}\" -> cid={targetCid} in [{targetFormId}]");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  X All methods failed for cid={targetCid}");
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

        // -- ActionState IPC: share input-command info with AppBotEye --
        try
        {
            ActionState.Write(new ActionState
            {
                Source = "input",
                WindowTitle = win.Title,
                ElementName = $"cid={targetCid}",
                ActionName = "type_text",
                ActionDetail = $"Input: \"{text}\" -> cid={targetCid} in [{targetFormId}]",
                Status = success ? "pass" : "fail",
            });
        }
        catch { /* best-effort */ }

        return 0;
    }

    /// <summary>
    /// Auto-dismiss alert/error dialogs that block MDI form access.
    /// Checks both title AND message text (Static controls) for alert keywords.
    /// Uses BM_CLICK on "확인" button -- fully focusless.
    /// Handles: 알림!!, VBScript 오류, 스크립트 오류, 접속 끊김 알림 등
    /// </summary>
    /// <summary>
    /// Dismiss VBScript/alert popups in a loop until no more appear.
    /// Returns total number of popups dismissed.
    /// </summary>
    static int TryDismissAlertPopups(IntPtr mainWindow, IntPtr mdiClient, int maxRounds = 10)
    {
        int totalDismissed = 0;
        try
        {
            var titleKeywords = new[] { "알림", "안내", "경고", "alert" };
            var msgKeywords = new[] { "오류", "에러", "error", "스크립트", "script" };

            for (int round = 0; round < maxRounds; round++)
            {
                var dismissed = new List<string>();

                // Collect all #32770 dialogs: children + same-process top-level
                NativeMethods.GetWindowThreadProcessId(mainWindow, out uint targetPid);
                var dialogsToCheck = new List<IntPtr>();

                NativeMethods.EnumChildWindows(mainWindow, (hWnd, _) =>
                {
                    if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                    var sb = new StringBuilder(64);
                    NativeMethods.GetClassNameW(hWnd, sb, 64);
                    if (sb.ToString() == "#32770") dialogsToCheck.Add(hWnd);
                    return true;
                }, IntPtr.Zero);

                NativeMethods.EnumWindows((hWnd, _) =>
                {
                    if (hWnd == mainWindow) return true;
                    if (!NativeMethods.IsWindowVisible(hWnd)) return true;
                    NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                    if (pid != targetPid) return true;
                    var sb = new StringBuilder(64);
                    NativeMethods.GetClassNameW(hWnd, sb, 64);
                    if (sb.ToString() == "#32770") dialogsToCheck.Add(hWnd);
                    return true;
                }, IntPtr.Zero);

                foreach (var hDlg in dialogsToCheck)
                {
                    var sb = new StringBuilder(256);
                    NativeMethods.GetWindowTextW(hDlg, sb, 256);
                    string dlgTitle = sb.ToString();

                    // Read Static control text (dialog message body)
                    var msgParts = new List<string>();
                    NativeMethods.EnumChildWindows(hDlg, (hChild, _) =>
                    {
                        var csb = new StringBuilder(64);
                        NativeMethods.GetClassNameW(hChild, csb, 64);
                        string cc = csb.ToString();
                        if (cc == "Static" || cc.Contains("STATIC"))
                        {
                            var tsb = new StringBuilder(512);
                            NativeMethods.GetWindowTextW(hChild, tsb, 512);
                            string t = tsb.ToString();
                            if (!string.IsNullOrWhiteSpace(t)) msgParts.Add(t);
                        }
                        return true;
                    }, IntPtr.Zero);
                    string dlgMessage = string.Join(" ", msgParts);

                    bool titleMatch = titleKeywords.Any(k =>
                        dlgTitle.Contains(k, StringComparison.OrdinalIgnoreCase));
                    bool msgMatch = msgKeywords.Any(k =>
                        dlgMessage.Contains(k, StringComparison.OrdinalIgnoreCase));
                    if (!titleMatch && !msgMatch) continue;

                    // Find "확인" or "OK" button
                    IntPtr hBtn = IntPtr.Zero;
                    NativeMethods.EnumChildWindows(hDlg, (hChild, _) =>
                    {
                        var csb = new StringBuilder(64);
                        NativeMethods.GetClassNameW(hChild, csb, 64);
                        if (!csb.ToString().Contains("Button")) return true;
                        csb.Clear();
                        NativeMethods.GetWindowTextW(hChild, csb, 64);
                        string btnText = csb.ToString();
                        if (btnText == "확인" || btnText == "OK" || btnText == "예" || btnText == "Yes")
                        { hBtn = hChild; return false; }
                        return true;
                    }, IntPtr.Zero);

                    if (hBtn != IntPtr.Zero)
                    {
                        NativeMethods.PostMessageW(hBtn, 0x00F5 /*BM_CLICK*/, IntPtr.Zero, IntPtr.Zero);
                        dismissed.Add("click");
                    }
                    else
                    {
                        NativeMethods.PostMessageW(hDlg, 0x0010 /*WM_CLOSE*/, IntPtr.Zero, IntPtr.Zero);
                        dismissed.Add("close");
                    }
                }

                totalDismissed += dismissed.Count;

                if (dismissed.Count == 0) break; // no more popups -- done!
                Thread.Sleep(200); // wait for dialog close + check for more
            }

            if (totalDismissed > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  [BLOCK] ");
                Console.ResetColor();
                Console.WriteLine($"Auto-dismissed {totalDismissed} popup(s) before input");
            }
        }
        catch { /* best-effort */ }
        return totalDismissed;
    }
}

