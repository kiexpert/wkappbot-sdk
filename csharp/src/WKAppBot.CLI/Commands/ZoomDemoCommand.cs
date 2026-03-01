using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: zoom-demo — Quick demo of adaptive zoom on any window
// Usage: wkappbot zoom-demo <window-title> [text]
// Types text into the foreground window and shows the appropriate zoom overlay.
internal partial class Program
{
    static int ZoomDemoCommand(string[] args)
    {
        if (args.Length < 1)
            return Error("Usage: wkappbot zoom-demo <window-title> [text]\n  Demo: types text with adaptive zoom overlay on any window.");

        string title = args[0];
        string text = args.Length >= 2 ? args[1] : "Hello World!";

        // Find window
        var found = WindowFinder.FindByTitle(title);
        if (found.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var winInfo = found[0];
        var hWnd = winInfo.Handle;
        Console.WriteLine($"Target: [{hWnd:X8}] \"{winInfo.Title}\"");

        // Get window rect for zoom mode detection
        NativeMethods.GetWindowRect(hWnd, out var wRect);
        Console.WriteLine($"  Window: {wRect.Width}x{wRect.Height} @({wRect.Left},{wRect.Top})");

        // Try to find the main edit/document area via UIA
        IntPtr editHwnd = hWnd;
        FlaUI.Core.AutomationElements.AutomationElement? editElement = null;
        System.Drawing.Rectangle editBounds = default;
        try
        {
            using var automation = new FlaUI.UIA3.UIA3Automation();
            var uiaWin = automation.FromHandle(hWnd);
            if (uiaWin != null)
            {
                // Look for Document or Edit control type
                var doc = uiaWin.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Document));
                var edit = doc ?? uiaWin.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
                if (edit != null)
                {
                    editElement = edit;
                    var nH = edit.Properties.NativeWindowHandle.ValueOrDefault;
                    if (nH != IntPtr.Zero) editHwnd = nH;
                    editBounds = edit.BoundingRectangle;
                    Console.WriteLine($"  Edit: [{edit.ControlType}] \"{edit.Name}\" {(int)editBounds.Width}x{(int)editBounds.Height} @({(int)editBounds.X},{(int)editBounds.Y})");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [UIA skip: {ex.Message}]");
        }

        // Get edit control rect (use UIA bounds if available, else window rect)
        int ctlLeft, ctlTop, ctlW, ctlH;
        if (editElement != null && editBounds.Width > 0)
        {
            ctlLeft = (int)editBounds.X;
            ctlTop = (int)editBounds.Y;
            ctlW = (int)editBounds.Width;
            ctlH = (int)editBounds.Height;
        }
        else
        {
            ctlLeft = wRect.Left;
            ctlTop = wRect.Top;
            ctlW = wRect.Width;
            ctlH = wRect.Height;
        }

        bool isSmall = ctlW < 200 && ctlH < 60;

        // Determine zoom mode
        var fgHwnd = NativeMethods.GetForegroundWindow();
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint tPid);
        NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fPid);
        bool isForeground = (fPid == tPid);

        ZoomMode mode;
        int zW, zH, zX, zY;

        if (isSmall)
        {
            mode = ZoomMode.Magnifier;
            zW = Math.Max(ctlW * 3 + 40, 400);
            zH = Math.Max(ctlH * 3 + 80, 180);
            zX = ctlLeft + (ctlW / 2) - (zW / 2);
            zY = ctlTop + (ctlH / 2) - (zH / 2);
        }
        else if (isForeground)
        {
            mode = ZoomMode.HighlightBox;
            int pad = 6;
            zW = ctlW + pad;
            zH = ctlH + pad + 20;
            zX = ctlLeft - pad / 2;
            zY = ctlTop - pad / 2;
        }
        else
        {
            mode = ZoomMode.Relay;
            zW = Math.Max(ctlW + 20, 300);
            zH = ctlH + 50;
            zX = ctlLeft + (ctlW / 2) - (zW / 2);
            zY = ctlTop + (ctlH / 2) - (zH / 2);
        }

        // Clamp to virtual screen bounds (supports multi-monitor with negative coordinates)
        int vsX = NativeMethods.GetSystemMetrics(76 /*SM_XVIRTUALSCREEN*/);
        int vsY = NativeMethods.GetSystemMetrics(77 /*SM_YVIRTUALSCREEN*/);
        int vsW = NativeMethods.GetSystemMetrics(78 /*SM_CXVIRTUALSCREEN*/);
        int vsH = NativeMethods.GetSystemMetrics(79 /*SM_CYVIRTUALSCREEN*/);
        if (zX < vsX) zX = vsX;
        if (zY < vsY) zY = vsY;
        if (zX + zW > vsX + vsW) zX = vsX + vsW - zW;
        if (zY + zH > vsY + vsH) zY = vsY + vsH - zH;

        string modeName = mode switch
        {
            ZoomMode.Magnifier => "3x",
            ZoomMode.HighlightBox => "HL",
            ZoomMode.Relay => "1:1",
            _ => "?"
        };

        Console.WriteLine($"  Zoom: {mode} ({modeName}) → {zW}x{zH} @({zX},{zY})");

        // Create zoom overlay
        InputZoomHost? zoomHost = null;
        try
        {
            zoomHost = new InputZoomHost();
            zoomHost.Start(zX, zY, zW, zH, mode);
            zoomHost.UpdateHeader($"[ZOOM:{modeName}] demo \"{text}\"");
            zoomHost.UpdateStatus($"Ready: \"{text}\" ({text.Length} chars)");

            // Initial capture for Magnifier/Relay
            if (mode != ZoomMode.HighlightBox)
            {
                try
                {
                    using var bmp = ScreenCapture.CaptureWindow(hWnd);
                    if (bmp != null)
                    {
                        NativeMethods.GetWindowRect(hWnd, out var fRect);
                        int rx = ctlLeft - fRect.Left;
                        int ry = ctlTop - fRect.Top;
                        int rw = Math.Min(ctlW, bmp.Width - rx);
                        int rh = Math.Min(ctlH, bmp.Height - ry);
                        if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                        {
                            using var crop = ScreenCapture.CropRegion(bmp, rx, ry, rw, rh);
                            zoomHost.UpdateImage(ScreenCapture.ToPngBytes(crop));
                        }
                    }
                }
                catch { }
            }

            Thread.Sleep(500);

            // Bring target to foreground and type via SendInput
            NativeMethods.SetForegroundWindow(hWnd);
            Thread.Sleep(300);

            // Click on the edit area to focus it
            if (editElement != null && editBounds.Width > 0)
            {
                int cx = (int)(editBounds.X + editBounds.Width / 2);
                int cy = (int)(editBounds.Y + editBounds.Height / 2);
                MouseInput.Click(cx, cy);
                Thread.Sleep(200);
            }

            // Type each character with zoom updates
            for (int i = 0; i < text.Length; i++)
            {
                // Type one char via SendInput (KEYEVENTF_UNICODE)
                KeyboardInput.TypeText(text[i].ToString());
                Thread.Sleep(50);

                zoomHost.UpdateStatus($"Typing: {i + 1}/{text.Length}  \"{text[..(i + 1)]}\"");

                // Update image for Magnifier/Relay (every 3 chars + last)
                if (mode != ZoomMode.HighlightBox && (i % 3 == 0 || i == text.Length - 1))
                {
                    try
                    {
                        using var bmp = ScreenCapture.CaptureWindow(hWnd);
                        if (bmp != null)
                        {
                            NativeMethods.GetWindowRect(hWnd, out var fRect);
                            int rx = ctlLeft - fRect.Left;
                            int ry = ctlTop - fRect.Top;
                            int rw = Math.Min(ctlW, bmp.Width - rx);
                            int rh = Math.Min(ctlH, bmp.Height - ry);
                            if (rx >= 0 && ry >= 0 && rw > 0 && rh > 0)
                            {
                                using var crop = ScreenCapture.CropRegion(bmp, rx, ry, rw, rh);
                                zoomHost.UpdateImage(ScreenCapture.ToPngBytes(crop));
                            }
                        }
                    }
                    catch { }
                }
            }

            Console.WriteLine($"  Typed: \"{text}\"");

            // Show result
            zoomHost.ShowResult(true, $"✓ DONE \"{text}\"");
            Thread.Sleep(500);

            // Save desktop screenshot (captures overlay appearance)
            var shotPath = System.IO.Path.Combine(DataDir, "logs", $"zoom_demo_{DateTime.Now:HHmmss}.png");
            try
            {
                // Capture wider area to show overlay in context
                int shotX = Math.Max(zX - 50, 0);
                int shotY = Math.Max(zY - 50, 0);
                int shotW = zW + 100;
                int shotH = zH + 100;
                using var shot = ScreenCapture.CaptureScreenRegion(shotX, shotY, shotW, shotH);
                ScreenCapture.SaveToFile(shot, shotPath);
                Console.WriteLine($"  Screenshot: {shotPath}");
            }
            catch (Exception ex) { Console.WriteLine($"  [screenshot err: {ex.Message}]"); }

            // Fire-and-forget fade
            zoomHost.BeginFadeOut(3000, 800);
            zoomHost = null;

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error: {ex.Message}");
            zoomHost?.Dispose();
            return 1;
        }
    }
}
