using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{

    // ── Method 9: Pure PostMessage pipeline ──
    static InputResult TryInputMethod9(InputContext ctx)
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
                else if (!string.IsNullOrEmpty(preOcr9) && FuzzyDigitMatch(preOcr9, text, 2))
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

    return success ? InputResult.Success : InputResult.Continue;
    }

    // ── Method 1: SetFocus + WM_CHAR ──
    static InputResult TryInputMethod1(InputContext ctx)
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

    return success ? InputResult.Success : InputResult.Continue;
    }
}
