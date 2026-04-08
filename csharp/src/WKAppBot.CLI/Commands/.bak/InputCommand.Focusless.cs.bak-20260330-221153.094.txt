using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{

    // ── Method A11y: UIA Value Pattern (focusless) ──
    static InputResult TryInputMethodA11y(InputContext ctx)
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
    var CaptureControlRawFast = ctx.CaptureControlRawFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

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

    return success ? InputResult.Success : InputResult.Continue;
    }

    // ── Method 10: Focusless PostMessage ──
    static InputResult TryInputMethod10(InputContext ctx)
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
    var CaptureControlRawFast = ctx.CaptureControlRawFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

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

                var perfTotal = Stopwatch.StartNew();

                // NO foreground/focus change — just MDI activate (sendmessage, not focus)
                NativeMethods.SendMessageW(mdiClient, 0x0222 /*WM_MDIACTIVATE*/, targetForm.Handle, IntPtr.Zero);
                Thread.Sleep(50); // reduced from 200ms

                // [ZOOM] Hook 1: Create adaptive overlay centered on control
                // Adaptive mode selection:
                //   Small control (w<200 && h<60): 3x magnifier overlay
                //   Large control + foreground:     highlight box (cyan 3px border only)
                //   Large control + obscured:       1:1 relay overlay (no magnification)
                if (true) // 돋보기 항상 표시
                {
                    try
                    {
                        NativeMethods.GetWindowRect(targetHwnd, out var zCtl);
                        bool isSmall = zCtl.Width < 200 && zCtl.Height < 60;

                        ZoomMode zoomMode;
                        int zW, zH, zX, zY;

                        if (isSmall)
                        {
                            // Magnifier: 3x control size + chrome (header 16px + status 18px + border/padding ~16px)
                            zoomMode = ZoomMode.Magnifier;
                            zW = zCtl.Width * 3 + 16;  // exact 3x width + border padding
                            zH = zCtl.Height * 3 + 50; // exact 3x height + header + status + padding
                            zX = zCtl.Left + (zCtl.Width / 2) - (zW / 2);
                            zY = zCtl.Top + (zCtl.Height / 2) - (zH / 2);
                        }
                        else
                        {
                            // Large control — check if foreground (visible) or obscured
                            var fgHwnd = NativeMethods.GetForegroundWindow();
                            NativeMethods.GetWindowThreadProcessId(targetHwnd, out uint zTargetPid);
                            NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint zFgPid);
                            bool isForeground = (zFgPid == zTargetPid);

                            if (isForeground)
                            {
                                // HighlightBox: exact control size + small padding for border
                                zoomMode = ZoomMode.HighlightBox;
                                int pad = 6; // 3px border on each side
                                zW = zCtl.Width + pad;
                                zH = zCtl.Height + pad + 20; // +20 for status tag
                                zX = zCtl.Left - pad / 2;
                                zY = zCtl.Top - pad / 2;
                            }
                            else
                            {
                                // Relay: 1:1 size + small header/status
                                zoomMode = ZoomMode.Relay;
                                zW = Math.Max(zCtl.Width + 20, 300);
                                zH = zCtl.Height + 50; // header + status
                                zX = zCtl.Left + (zCtl.Width / 2) - (zW / 2);
                                zY = zCtl.Top + (zCtl.Height / 2) - (zH / 2);
                            }
                        }

                        // Clamp to virtual screen bounds (multi-monitor safe)
                        int vsLeft = NativeMethods.GetSystemMetrics(76);  // SM_XVIRTUALSCREEN
                        int vsTop  = NativeMethods.GetSystemMetrics(77);  // SM_YVIRTUALSCREEN
                        int vsW    = NativeMethods.GetSystemMetrics(78);  // SM_CXVIRTUALSCREEN
                        int vsH    = NativeMethods.GetSystemMetrics(79);  // SM_CYVIRTUALSCREEN
                        if (zX < vsLeft) zX = vsLeft;
                        if (zY < vsTop)  zY = vsTop;
                        if (zX + zW > vsLeft + vsW) zX = vsLeft + vsW - zW;
                        if (zY + zH > vsTop + vsH)  zY = vsTop + vsH - zH;

                        string modeName = zoomMode switch
                        {
                            ZoomMode.Magnifier => "3x",
                            ZoomMode.HighlightBox => "HL",
                            ZoomMode.Relay => "1:1",
                            _ => "?"
                        };

                        zoomHost = new InputZoomHost();
                        zoomHost.Start(zX, zY, zW, zH, zoomMode);
                        zoomHost.UpdateHeader($"[ZOOM:{modeName}] input_m10 \"{text}\"");
                        Console.Write($"[ZOOM:{modeName}@{zX},{zY} {zW}x{zH}] ");

                        // Initial capture (Magnifier/Relay only — HighlightBox is transparent)
                        if (zoomMode != ZoomMode.HighlightBox)
                        {
                            var initPng = CaptureControlPng(targetForm.Handle, targetHwnd);
                            if (initPng != null) zoomHost.UpdateImage(initPng);
                        }
                        zoomHost.UpdateStatus($"Before → \"{text}\" ({text.Length} chars)");

                        // Start periodic live capture (fast refresh) + TOPMOST enforcement
                        // Raw pixel path: no codec overhead, ~10x faster than BMP/PNG
                        var formH = targetForm.Handle;
                        var ctlH = targetHwnd;
                        zoomHost.StartLiveCaptureFast(
                            zoomMode != ZoomMode.HighlightBox && CaptureControlRawFast != null
                                ? () => CaptureControlRawFast(formH, ctlH)
                                : (Func<(byte[], int, int, int)?>?)null);
                    }
                    catch (Exception zex)
                    {
                        Console.Write($"[ZOOM:ERR {zex.GetType().Name}: {zex.Message}] ");
                        zoomHost = null;
                    }
                }

                // Step 0b: Dismiss any VBScript popups right before typing (from previous Enter)
                TryDismissAlertPopups(win.Handle, mdiClient);

                // Step 1: Home via PostMessage (cursor to pos 0)
                var perfHome = Stopwatch.StartNew();
                NativeMethods.PostMessageW(targetHwnd, 0x0100 /*WM_KEYDOWN*/, (IntPtr)0x24 /*VK_HOME*/,
                    MkLParam10(0x24, extended: true));
                Thread.Sleep(5);
                NativeMethods.PostMessageW(targetHwnd, 0x0101 /*WM_KEYUP*/, (IntPtr)0x24,
                    MkLParam10(0x24, keyUp: true, extended: true));
                Thread.Sleep(15); // reduced from 50ms
                perfHome.Stop();

                // Step 2: WM_CHAR only per digit (no WM_KEYDOWN/UP — avoids TranslateMessage doubling)
                var perfType = Stopwatch.StartNew();
                int charIdx10 = 0;
                foreach (char ch in text)
                {
                    short vkScan = NativeMethods.VkKeyScanW(ch);
                    byte vk = (byte)(vkScan & 0xFF);
                    IntPtr lp = MkLParam10(vk);
                    NativeMethods.PostMessageW(targetHwnd, 0x0102 /*WM_CHAR*/, (IntPtr)ch, lp);
                    charIdx10++;
                    Thread.Sleep(10); // reduced from 30ms — PostMessage is async, 10ms is safe

                    // [ZOOM] Hook 2: Update status text only (NO capture per character — major perf win!)
                    if (zoomHost?.IsAlive == true)
                    {
                        zoomHost.UpdateStatus($"Typing: {charIdx10}/{text.Length}  \"{text[..charIdx10]}\"");
                    }
                }
                perfType.Stop();
                Thread.Sleep(100); // allow message pump to process WM_CHAR + repaint before OCR

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[typed {text.Length} chars, NO focus, home={perfHome.ElapsedMilliseconds}ms type={perfType.ElapsedMilliseconds}ms] ");
                Console.ResetColor();

                // Stop live capture before manual captures (OCR verification etc.)
                var ticks = zoomHost?.TickCount ?? 0;
                zoomHost?.StopLiveCapture();
                Console.Write($"[live-ticks={ticks}] ");

                // [ZOOM] Hook 2b: Single capture after all typing complete (not per-char!)
                var perfZoomPost = Stopwatch.StartNew();
                if (zoomHost?.IsAlive == true)
                {
                    try
                    {
                        var typedPng = CaptureControlPng(targetForm.Handle, targetHwnd);
                        if (typedPng != null)
                        {
                            zoomHost.UpdateImage(typedPng);
                            zoomHost.UpdateStatus($"Typed: \"{text}\" — verifying...");
                        }
                    }
                    catch { /* best-effort */ }
                }
                perfZoomPost.Stop();

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
                            var ss10 = OcrDebugPath("m10_pre");
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
                var perfEnter = Stopwatch.StartNew();
                if (sendEnter)
                {
                    NativeMethods.PostMessageW(targetHwnd, 0x0100, (IntPtr)0x0D, MkLParam10(0x0D));
                    Thread.Sleep(20); // reduced from 50ms
                    NativeMethods.PostMessageW(targetHwnd, 0x0101, (IntPtr)0x0D, MkLParam10(0x0D, keyUp: true));
                    Thread.Sleep(800); // reduced from 1500ms — data query needs time but not this much
                }
                perfEnter.Stop();

                // Step 3b: Dismiss any VBScript popups that appeared during data load
                TryDismissAlertPopups(win.Handle, mdiClient);

                // Step 4: Verify
                var perfVerify = Stopwatch.StartNew();
                Thread.Sleep(200); // reduced from 300ms
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
                else if (!string.IsNullOrEmpty(preOcr10) && FuzzyDigitMatch(preOcr10, text, 2))
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

                perfVerify.Stop();

                // [PERF] Performance summary
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[PERF] total={perfTotal.ElapsedMilliseconds}ms home={perfHome.ElapsedMilliseconds}ms type={perfType.ElapsedMilliseconds}ms zoom-post={perfZoomPost.ElapsedMilliseconds}ms enter={perfEnter.ElapsedMilliseconds}ms verify={perfVerify.ElapsedMilliseconds}ms ");
                Console.ResetColor();

                // [ZOOM] Hook 4: Show pass/fail result + desktop screenshot + quick fade
                if (zoomHost?.IsAlive == true)
                {
                    try
                    {
                        bool zoomPass = success;
                        string zoomText = zoomPass ? $"✓ PASS \"{text}\"" : $"? \"{preOcr10 ?? "?"}\"";
                        zoomHost.ShowResult(zoomPass, zoomText);

                        // Final control capture via PrintWindow (Z-order safe, ignores overlay on top)
                        try
                        {
                            var finalPng = CaptureControlPng(targetForm.Handle, targetHwnd);
                            if (finalPng != null) zoomHost.UpdateImage(finalPng);
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
                            Thread.Sleep(100); // brief wait for WPF to render result state (reduced from 300ms)
                            using var zShotBmp = ScreenCapture.CaptureScreenRegion(zShotX, zShotY, zShotW, zShotH);
                            var zShotPath = OcrDebugPath("zoom_result");
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

                        // Fire-and-forget: overlay fades on its own STA thread
                        // No Thread.Sleep — main thread proceeds immediately for fast successive inputs
                        zoomHost.BeginFadeOut(1500, 400); // 1.5s hold + 0.4s fade → faster cleanup
                        zoomHost = null; // hand off lifecycle to STA thread (IsBackground=true)
                    }
                    catch { zoomHost?.Dispose(); zoomHost = null; }
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

    return success ? InputResult.Success : InputResult.Continue;
    }

    // ── Method 6: EM_SETSEL + EM_REPLACESEL ──
    static InputResult TryInputMethod6(InputContext ctx)
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
    var CaptureControlRawFast = ctx.CaptureControlRawFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

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

    return success ? InputResult.Success : InputResult.Continue;
    }

    // ── Method 7: WM_SETTEXT + WM_COMMAND(EN_CHANGE) ──
    static InputResult TryInputMethod7(InputContext ctx)
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
    var CaptureControlRawFast = ctx.CaptureControlRawFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

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

    return success ? InputResult.Success : InputResult.Continue;
    }

    // ── Method 3: WM_SETTEXT + VK_RETURN ──
    static InputResult TryInputMethod3(InputContext ctx)
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
    var CaptureControlRawFast = ctx.CaptureControlRawFast;
    var ConfirmByOcrContains = ctx.ConfirmByOcrContains;
    var FuzzyDigitMatch = ctx.FuzzyDigitMatch;
    var OcrDebugPath = ctx.OcrDebugPath;
    var targetFormId = ctx.TargetFormId;
    var targetCid = ctx.TargetCid;
    var expDb = ctx.ExpDb;
    bool success = false;

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

    return success ? InputResult.Success : InputResult.Continue;
    }
}
