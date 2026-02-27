using System.Drawing;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Reusable zoom overlay for click/invoke/select operations.
/// Shows a magnified (or 1:1 relayed) view of the target control during automation.
///
/// Usage:
///   using var zoom = ClickZoomHelper.Begin(hButton, hForm, "do", "매매시작");
///   // ... perform click/invoke ...
///   zoom?.ShowPass("dialog closed");  // or ShowFail("no reaction")
///
/// Three adaptive modes (same as InputZoom):
///   Magnifier  — small controls (w&lt;200 &amp;&amp; h&lt;60): 3x enlarged overlay
///   HighlightBox — large + foreground: cyan 3px border highlight
///   Relay — large + obscured: 1:1 relay via PrintWindow
///
/// Tag: [ZOOM]
/// </summary>
internal sealed class ClickZoomHelper : IDisposable
{
    private InputZoomHost? _host;
    private readonly IntPtr _formHandle;
    private readonly IntPtr _controlHandle;

    private ClickZoomHelper(InputZoomHost host, IntPtr formHandle, IntPtr controlHandle)
    {
        _host = host;
        _formHandle = formHandle;
        _controlHandle = controlHandle;
    }

    /// <summary>
    /// Start zoom overlay for a control. Returns null if zoom creation fails (non-critical).
    /// </summary>
    /// <param name="hControl">Target control handle (button, tab, etc.)</param>
    /// <param name="hFormOrParent">Parent form/window handle for PrintWindow capture</param>
    /// <param name="source">Command name: "do", "click", "uia-test", "invoke", etc.</param>
    /// <param name="actionLabel">Action description: "매매시작", "Tab '상세'", "Invoke aid=3795"</param>
    public static ClickZoomHelper? Begin(IntPtr hControl, IntPtr hFormOrParent,
        string source, string actionLabel)
    {
        try
        {
            NativeMethods.GetWindowRect(hControl, out var ctlRect);
            if (ctlRect.Width <= 0 || ctlRect.Height <= 0) return null;

            bool isSmall = ctlRect.Width < 200 && ctlRect.Height < 60;

            ZoomMode mode;
            int zW, zH, zX, zY;

            if (isSmall)
            {
                mode = ZoomMode.Magnifier;
                zW = ctlRect.Width * 3 + 16;  // exact 3x + border padding
                zH = ctlRect.Height * 3 + 50; // exact 3x + header + status + padding
                zX = ctlRect.Left + (ctlRect.Width / 2) - (zW / 2);
                zY = ctlRect.Top + (ctlRect.Height / 2) - (zH / 2);
            }
            else
            {
                var fgHwnd = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(hControl, out uint ctlPid);
                NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fgPid);
                bool isForeground = (fgPid == ctlPid);

                if (isForeground)
                {
                    mode = ZoomMode.HighlightBox;
                    int pad = 6;
                    zW = ctlRect.Width + pad;
                    zH = ctlRect.Height + pad + 20;
                    zX = ctlRect.Left - pad / 2;
                    zY = ctlRect.Top - pad / 2;
                }
                else
                {
                    mode = ZoomMode.Relay;
                    zW = ctlRect.Width + 16; // exact 1:1 + border padding
                    zH = ctlRect.Height + 50; // 1:1 + header + status
                    zX = ctlRect.Left + (ctlRect.Width / 2) - (zW / 2);
                    zY = ctlRect.Top + (ctlRect.Height / 2) - (zH / 2);
                }
            }

            // Clamp to screen bounds
            if (zX < 0) zX = 0;
            if (zY < 0) zY = 0;

            string modeName = mode switch
            {
                ZoomMode.Magnifier => "3x",
                ZoomMode.HighlightBox => "HL",
                ZoomMode.Relay => "1:1",
                _ => "?"
            };

            var host = new InputZoomHost();
            host.Start(zX, zY, zW, zH, mode);
            host.UpdateHeader($"[ZOOM:{modeName}] {source} \"{actionLabel}\"");
            Console.Write($"[ZOOM:{modeName}] ");

            // Initial capture (Magnifier/Relay only)
            if (mode != ZoomMode.HighlightBox)
            {
                var initPng = CaptureControlPng(hFormOrParent, hControl);
                if (initPng != null) host.UpdateImage(initPng);
            }
            host.UpdateStatus($"Ready: {actionLabel}");

            return new ClickZoomHelper(host, hFormOrParent, hControl);
        }
        catch (Exception ex)
        {
            Console.Write($"[ZOOM:ERR {ex.GetType().Name}] ");
            return null;
        }
    }

    /// <summary>
    /// Start zoom overlay using UIA BoundingRectangle (for UIA elements without hWnd).
    /// </summary>
    /// <param name="screenRect">Element's screen-coordinate bounding rectangle</param>
    /// <param name="hFormOrParent">Parent window handle for PrintWindow capture</param>
    /// <param name="source">Command name</param>
    /// <param name="actionLabel">Action description</param>
    public static ClickZoomHelper? BeginFromRect(Rectangle screenRect, IntPtr hFormOrParent,
        string source, string actionLabel)
    {
        try
        {
            if (screenRect.Width <= 0 || screenRect.Height <= 0) return null;

            bool isSmall = screenRect.Width < 200 && screenRect.Height < 60;

            ZoomMode mode;
            int zW, zH, zX, zY;

            if (isSmall)
            {
                mode = ZoomMode.Magnifier;
                zW = screenRect.Width * 3 + 16;  // exact 3x + border padding
                zH = screenRect.Height * 3 + 50; // exact 3x + header + status + padding
                zX = screenRect.Left + (screenRect.Width / 2) - (zW / 2);
                zY = screenRect.Top + (screenRect.Height / 2) - (zH / 2);
            }
            else
            {
                var fgHwnd = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(hFormOrParent, out uint ctlPid);
                NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fgPid);
                bool isForeground = (fgPid == ctlPid);

                if (isForeground)
                {
                    mode = ZoomMode.HighlightBox;
                    int pad = 6;
                    zW = screenRect.Width + pad;
                    zH = screenRect.Height + pad + 20;
                    zX = screenRect.Left - pad / 2;
                    zY = screenRect.Top - pad / 2;
                }
                else
                {
                    mode = ZoomMode.Relay;
                    zW = screenRect.Width + 16;  // exact 1:1 + border padding
                    zH = screenRect.Height + 50; // 1:1 + header + status
                    zX = screenRect.Left + (screenRect.Width / 2) - (zW / 2);
                    zY = screenRect.Top + (screenRect.Height / 2) - (zH / 2);
                }
            }

            if (zX < 0) zX = 0;
            if (zY < 0) zY = 0;

            string modeName = mode switch
            {
                ZoomMode.Magnifier => "3x",
                ZoomMode.HighlightBox => "HL",
                ZoomMode.Relay => "1:1",
                _ => "?"
            };

            var host = new InputZoomHost();
            host.Start(zX, zY, zW, zH, mode);
            host.UpdateHeader($"[ZOOM:{modeName}] {source} \"{actionLabel}\"");
            Console.Write($"[ZOOM:{modeName}] ");

            // Initial capture via form PrintWindow + crop
            if (mode != ZoomMode.HighlightBox && hFormOrParent != IntPtr.Zero)
            {
                var png = CaptureRectPng(hFormOrParent, screenRect);
                if (png != null) host.UpdateImage(png);
            }
            host.UpdateStatus($"Ready: {actionLabel}");

            return new ClickZoomHelper(host, hFormOrParent, IntPtr.Zero);
        }
        catch (Exception ex)
        {
            Console.Write($"[ZOOM:ERR {ex.GetType().Name}] ");
            return null;
        }
    }

    /// <summary>Update status text during action.</summary>
    public void UpdateStatus(string text)
    {
        if (_host?.IsAlive != true) return;
        _host.UpdateStatus(text);
    }

    /// <summary>Update captured image (e.g., after action to show result).</summary>
    public void UpdateImage()
    {
        if (_host?.IsAlive != true) return;
        if (_controlHandle != IntPtr.Zero)
        {
            var png = CaptureControlPng(_formHandle, _controlHandle);
            if (png != null) _host.UpdateImage(png);
        }
    }

    /// <summary>Show success result and auto-fade.</summary>
    public void ShowPass(string detail)
    {
        if (_host?.IsAlive != true) return;
        _host.ShowResult(true, $"✓ {detail}");
        // Final capture to show post-action state
        if (_controlHandle != IntPtr.Zero)
        {
            var png = CaptureControlPng(_formHandle, _controlHandle);
            if (png != null) _host.UpdateImage(png);
        }
        _host.BeginFadeOut(1500, 400); // faster cleanup
        _host = null; // release — STA thread cleans up
    }

    /// <summary>Show failure result and auto-fade.</summary>
    public void ShowFail(string detail)
    {
        if (_host?.IsAlive != true) return;
        _host.ShowResult(false, $"✗ {detail}");
        if (_controlHandle != IntPtr.Zero)
        {
            var png = CaptureControlPng(_formHandle, _controlHandle);
            if (png != null) _host.UpdateImage(png);
        }
        _host.BeginFadeOut(2000, 500); // faster cleanup
        _host = null;
    }

    public void Dispose()
    {
        _host?.Dispose();
        _host = null;
    }

    // ── Capture helpers ──

    /// <summary>
    /// Capture a specific control for zoom overlay.
    /// Strategy 1: Full PrintWindow + CropRegion (safe, no viewport tricks)
    /// Strategy 2: CopyFromScreen for GPU-composited windows
    /// NOTE: CaptureWindowRegion viewport trick skipped — Chrome/Electron ignores SetViewportOrgEx.
    /// </summary>
    internal static byte[]? CaptureControlPng(IntPtr formHandle, IntPtr controlHandle)
    {
        try
        {
            NativeMethods.GetWindowRect(controlHandle, out var cr);
            if (cr.Width <= 0 || cr.Height <= 0) return null;

            // Strategy 1: Full PrintWindow + CropRegion (safe, no viewport tricks)
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
                    {
                        using var crop = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                        if (!ScreenCapture.IsBlankBitmap(crop))
                            return ScreenCapture.ToPngBytes(crop);
                    }
                }
            }

            // Strategy 2: CopyFromScreen — for GPU-composited windows (Chrome/Electron)
            using var screenBmp = ScreenCapture.CaptureScreenRegion(cr.Left, cr.Top, cr.Width, cr.Height);
            if (screenBmp != null && !ScreenCapture.IsBlankBitmap(screenBmp))
                return ScreenCapture.ToPngBytes(screenBmp);

            return null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Capture a screen rect for zoom overlay.
    /// If the window is in the foreground → CopyFromScreen first (handles GPU compositing).
    /// If background → PrintWindow + CropRegion first (Z-order safe).
    ///
    /// WHY: Chrome/Electron renders web content via GPU compositing, which is invisible to PrintWindow.
    /// PrintWindow only captures native elements (title bar, scrollbars). For foreground windows,
    /// CopyFromScreen reliably captures everything including GPU content.
    /// For background windows, CopyFromScreen captures whatever is on TOP (wrong!), so PrintWindow
    /// is the safer choice despite its GPU limitation.
    /// </summary>
    internal static byte[]? CaptureRectPng(IntPtr formHandle, Rectangle screenRect)
    {
        try
        {
            if (screenRect.Width <= 0 || screenRect.Height <= 0) return null;

            // Check if window is in foreground (same process = counts as foreground)
            bool isForeground = false;
            try
            {
                var fgHwnd = NativeMethods.GetForegroundWindow();
                if (fgHwnd == formHandle)
                {
                    isForeground = true;
                }
                else
                {
                    NativeMethods.GetWindowThreadProcessId(formHandle, out uint formPid);
                    NativeMethods.GetWindowThreadProcessId(fgHwnd, out uint fgPid);
                    isForeground = (formPid == fgPid);
                }
            }
            catch { }

            if (isForeground)
            {
                // Foreground: CopyFromScreen is most reliable (captures GPU-composited content)
                using var screenBmp = ScreenCapture.CaptureScreenRegion(
                    screenRect.Left, screenRect.Top, screenRect.Width, screenRect.Height);
                if (screenBmp != null && !ScreenCapture.IsBlankBitmap(screenBmp))
                    return ScreenCapture.ToPngBytes(screenBmp);
            }

            // Background (or screen capture failed): PrintWindow + CropRegion (Z-order safe)
            NativeMethods.GetWindowRect(formHandle, out var fr);
            int rx = screenRect.Left - fr.Left, ry = screenRect.Top - fr.Top;
            if (rx >= 0 && ry >= 0)
            {
                using var formBmp = ScreenCapture.TryPrintWindowOnly(formHandle);
                if (formBmp != null)
                {
                    int rw = Math.Min(screenRect.Width, formBmp.Width - rx);
                    int rh = Math.Min(screenRect.Height, formBmp.Height - ry);
                    if (rw > 0 && rh > 0)
                    {
                        using var crop = ScreenCapture.CropRegion(formBmp, rx, ry, rw, rh);
                        if (!ScreenCapture.IsBlankBitmap(crop))
                            return ScreenCapture.ToPngBytes(crop);
                    }
                }
            }

            // Final fallback: CopyFromScreen even for background (best effort)
            if (!isForeground)
            {
                using var screenBmp = ScreenCapture.CaptureScreenRegion(
                    screenRect.Left, screenRect.Top, screenRect.Width, screenRect.Height);
                if (screenBmp != null && !ScreenCapture.IsBlankBitmap(screenBmp))
                    return ScreenCapture.ToPngBytes(screenBmp);
            }

            return null;
        }
        catch { return null; }
    }
}
