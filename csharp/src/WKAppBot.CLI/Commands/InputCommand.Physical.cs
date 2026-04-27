using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{

    // -- Method 8: Atomic SendInput batch --
    static InputResult TryInputMethod8(InputContext ctx)
    {
    // Unpack context -> local variables (same names as original)
    var targetHwnd = ctx.TargetHwnd;
    var mdiClient = ctx.MdiClient;
    var targetForm = ctx.TargetForm;
    var text = ctx.Text;
    var sendEnter = ctx.SendEnter;
    var win = ctx.Win;
    var clickXOffset = ctx.ClickXOffset;
    var ctlRect = ctx.CtlRect;
    var readinessReport = ctx.ReadinessReport;
    var CaptureControlPng = ctx.CaptureControlPng;
    var CaptureControlFast = ctx.CaptureControlFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [8] AtomicBatch: ");
            Console.ResetColor();

            try
            {
                // NativeHookInputFocus -- pre/post batch focus verification with retry
                var guard8 = new NativeHookInputFocus(targetHwnd, win.Handle, maxRetries: 3);
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

                // Step 1: Build single INPUT array -- Home + Shift+End + chars + Enter
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

                // Shift down -> End (extended) -> Shift up = select all
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
                    Console.WriteLine($"v OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (wmT8.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"v WM confirmed \"{text}\"");
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
                Console.WriteLine($"X {ex.Message}");
                Console.ResetColor();
            }

    return success ? InputResult.Success : InputResult.Continue;
    }

    // -- Method 4: Physical SendInput pipeline --
    static InputResult TryInputMethod4(InputContext ctx)
    {
    // Unpack context -> local variables (same names as original)
    var targetHwnd = ctx.TargetHwnd;
    var mdiClient = ctx.MdiClient;
    var targetForm = ctx.TargetForm;
    var text = ctx.Text;
    var sendEnter = ctx.SendEnter;
    var win = ctx.Win;
    var clickXOffset = ctx.ClickXOffset;
    var ctlRect = ctx.CtlRect;
    var readinessReport = ctx.ReadinessReport;
    var CaptureControlPng = ctx.CaptureControlPng;
    var CaptureControlFast = ctx.CaptureControlFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [4] SendInput(Click+Type+Enter): ");
            Console.ResetColor();

            try
            {
                // NativeHookInputFocus -- pre-batch focus verification with retry
                var guard4 = new NativeHookInputFocus(targetHwnd, win.Handle, maxRetries: 3);
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

                // Step 2: Select all (Home -> Shift+End is reliable for CMaskEdit)
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

                // Step 4: OCR verify BEFORE Enter -- confirm text is displayed correctly
                string? ocrBeforeEnter = null;
                try
                {
                    NativeMethods.GetWindowRect(targetHwnd, out var preRect);
                    using var preBmp = ScreenCapture.CaptureWindow(targetForm.Handle, new WKAppBot.Win32.Input.CaptureOptions
                    {
                        RejectBlank = true,
                        StepLogger = s => Console.Error.WriteLine(s),
                    });
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
                // OnKeyUp(VK_RETURN) -> SetItemName() -> RunEventCommTR() -> data query
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
                    using var formBmp = ScreenCapture.CaptureWindow(targetForm.Handle, new WKAppBot.Win32.Input.CaptureOptions
                    {
                        RejectBlank = true,
                        StepLogger = s => Console.Error.WriteLine(s),
                    });
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
                // 1. Post-enter OCR (most reliable -- visual confirmation after Enter)
                // 2. Pre-enter OCR (typing confirmed before Enter -- CMaskEditEx doesn't update CWnd text)
                // 3. WM_GETTEXT (CWnd buffer -- only works if SetWindowText was called)
                //
                // CMaskEditEx::OnChar() updates m_strBuffer + DrawCaption (display) but NOT SetWindowText.
                // So WM_GETTEXT/UIA Name always show stale text. Pre-enter OCR is the real truth.
                if (!string.IsNullOrEmpty(ocrText) && ocrText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"v post-enter OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(ocrBeforeEnter) && ocrBeforeEnter.Contains(text))
                {
                    // CMaskEditEx: display shows correct text (m_strBuffer updated via OnChar)
                    // but CWnd text (WM_GETTEXT/UIA) is stale. Trust the pre-enter OCR.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"v pre-enter OCR confirmed \"{text}\" (CWnd text stale: \"{wmText}\")");
                    Console.ResetColor();
                    success = true;
                }
                else if (!string.IsNullOrEmpty(wmText) && wmText.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"v WM_GETTEXT confirmed \"{text}\"");
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
                Console.WriteLine($"X {ex.Message}");
                Console.ResetColor();
            }

    return success ? InputResult.Success : InputResult.Continue;
    }

    // -- Method 5: Click + PostMessage hybrid --
    static InputResult TryInputMethod5(InputContext ctx)
    {
    // Unpack context -> local variables (same names as original)
    var targetHwnd = ctx.TargetHwnd;
    var mdiClient = ctx.MdiClient;
    var targetForm = ctx.TargetForm;
    var text = ctx.Text;
    var sendEnter = ctx.SendEnter;
    var win = ctx.Win;
    var clickXOffset = ctx.ClickXOffset;
    var ctlRect = ctx.CtlRect;
    var readinessReport = ctx.ReadinessReport;
    var CaptureControlPng = ctx.CaptureControlPng;
    var CaptureControlFast = ctx.CaptureControlFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    const bool RequireOcrForNonA11y = true;
    bool success = false;

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

                // NativeHookInputFocus -- verify focus before SendInput portion
                var guard5 = new NativeHookInputFocus(targetHwnd, win.Handle, maxRetries: 3);
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
                    using var formBmp5 = ScreenCapture.CaptureWindow(targetForm.Handle, new WKAppBot.Win32.Input.CaptureOptions
                    {
                        RejectBlank = true,
                        StepLogger = s => Console.Error.WriteLine(s),
                    });
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
                    Console.WriteLine($"v OCR confirmed \"{text}\"");
                    Console.ResetColor();
                    success = true;
                }
                else if (!RequireOcrForNonA11y && !string.IsNullOrEmpty(wmText5) && wmText5.Contains(text))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"v WM_GETTEXT confirmed \"{text}\"");
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
                Console.WriteLine($"X {ex.Message}");
                Console.ResetColor();
            }

    return success ? InputResult.Success : InputResult.Continue;
    }

    // -- Method 2: Click + VK SendInput --
    static InputResult TryInputMethod2(InputContext ctx)
    {
    // Unpack context -> local variables (same names as original)
    var targetHwnd = ctx.TargetHwnd;
    var mdiClient = ctx.MdiClient;
    var targetForm = ctx.TargetForm;
    var text = ctx.Text;
    var sendEnter = ctx.SendEnter;
    var win = ctx.Win;
    var clickXOffset = ctx.ClickXOffset;
    var ctlRect = ctx.CtlRect;
    var readinessReport = ctx.ReadinessReport;
    var CaptureControlPng = ctx.CaptureControlPng;
    var CaptureControlFast = ctx.CaptureControlFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    const bool RequireOcrForNonA11y = true;
    bool success = false;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [2] Click+VK-Type: ");
            Console.ResetColor();

            try
            {
                // Click on the LEFT portion (text input area, not right-side buttons)
                int cx = ctlRect.Left + clickXOffset;
                int cy = ctlRect.Top + ctlRect.Height / 2;

                // NativeHookInputFocus -- per-keystroke focus verification
                var guard = new NativeHookInputFocus(targetHwnd, win.Handle, maxRetries: 3);
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

                    // Home key -> move caret to start
                    KeyboardInput.PressKey("home");
                    Thread.Sleep(50);

                    // Shift+End -> select all text
                    KeyboardInput.Hotkey(new[] { "shift", "end" });
                    Thread.Sleep(100);

                    // Type each digit with per-keystroke guard
                    // IMPORTANT: Use CheckBeforeKeystroke (detect-only, no recovery).
                    // If ANY interference detected, immediately restart the entire input.
                    // Reason: Autocomplete dropdown destroys selection state (Home+Shift+End)
                    // so even if focus is restored, cursor position is wrong -- must restart.
                    bool interrupted = false;
                    foreach (char ch in text)
                    {
                        // [GUARD] Check focus before each keystroke -- detect only
                        var guardCheck = guard.CheckBeforeKeystroke();
                        if (!guardCheck.Ok)
                        {
                            Console.Error.WriteLine($"[GUARD] Interrupted: {guardCheck.Type} -- {guardCheck.Detail}");
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
                        // Interference during typing -- dismiss autocomplete + clear + retry
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
                        Console.WriteLine("[GUARD] Focus lost before Enter -- skipping Enter key");
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
                    using var bmp = ScreenCapture.CaptureWindow(win.Handle, new WKAppBot.Win32.Input.CaptureOptions
                    {
                        RejectBlank = true,
                        StepLogger = s => Console.Error.WriteLine(s),
                    });
                    if (bmp != null)
                    {
                        var ssPath = OcrDebugPath("verify");
                        bmp.Save(ssPath);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"[screenshot: {Path.GetFileName(ssPath)}] ");
                        Console.ResetColor();
                    }
                }
                catch { }

                var checkText = !string.IsNullOrEmpty(wmText) ? wmText : uiaName ?? "";
                if (checkText.Contains(text))
                {
                    var ocrOk = !RequireOcrForNonA11y || ConfirmByOcrContains("m2");
                    if (ocrOk)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"v \"{checkText}\" (via {(!string.IsNullOrEmpty(wmText) ? "WM_GETTEXT" : "UIA")} +OCR)");
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
                Console.WriteLine($"X {ex.Message}");
                Console.ResetColor();
            }

    return success ? InputResult.Success : InputResult.Continue;
    }
}
