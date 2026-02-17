using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Screen and window capture.
/// Handles dual-monitor with negative coordinates correctly.
/// </summary>
public static class ScreenCapture
{
    /// <summary>
    /// Capture a specific window's client area.
    /// Falls back to screen-region capture if PrintWindow fails (common with HTS).
    /// </summary>
    public static Bitmap CaptureWindow(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        int w = rect.Width;
        int h = rect.Height;
        if (w <= 0 || h <= 0) throw new InvalidOperationException("Window has zero size");

        // Try PrintWindow first (works for most apps)
        var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            IntPtr hdc = g.GetHdc();
            bool ok = NativeMethods.PrintWindow(hWnd, hdc, NativeMethods.PW_RENDERFULLCONTENT);
            g.ReleaseHdc(hdc);

            if (ok)
            {
                // Verify it's not all black (PrintWindow sometimes returns "success" but blank)
                var pixel = bmp.GetPixel(w / 2, h / 2);
                if (pixel.R != 0 || pixel.G != 0 || pixel.B != 0)
                    return bmp;
            }
        }
        bmp.Dispose();

        // Fallback: bring window to front → capture from screen → restore Z-order
        // (Prevents overlapping windows from appearing in the screenshot)
        return CaptureWindowWithBringToFront(hWnd);
    }

    /// <summary>
    /// Capture window by temporarily bringing it to Z-order top.
    /// Uses SWP_NOACTIVATE to avoid stealing focus from the user.
    /// Used when PrintWindow fails (common with HTS MFC apps).
    /// </summary>
    private static Bitmap CaptureWindowWithBringToFront(IntPtr hWnd)
    {
        var originalFg = NativeMethods.GetForegroundWindow();
        bool needsRestore = originalFg != IntPtr.Zero && originalFg != hWnd;

        try
        {
            // Bring target window to Z-order top without activating (no focus steal)
            NativeMethods.SetWindowPos(
                hWnd, NativeMethods.HWND_TOP,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
                NativeMethods.SWP_SHOWWINDOW | NativeMethods.SWP_NOACTIVATE);

            // Wait for window to render on top
            Thread.Sleep(100);

            // Re-read rect (in case the window was moved)
            NativeMethods.GetWindowRect(hWnd, out var rect);
            return CaptureScreenRegion(rect.Left, rect.Top, rect.Width, rect.Height);
        }
        finally
        {
            // Restore original foreground window to Z-order top (best-effort)
            if (needsRestore)
            {
                NativeMethods.SetWindowPos(
                    originalFg, NativeMethods.HWND_TOP,
                    0, 0, 0, 0,
                    NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
                    NativeMethods.SWP_SHOWWINDOW | NativeMethods.SWP_NOACTIVATE);
            }
        }
    }

    /// <summary>
    /// Capture a region of the virtual screen.
    /// Handles negative coordinates for dual-monitor setups.
    /// </summary>
    public static Bitmap CaptureScreenRegion(int x, int y, int width, int height)
    {
        var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
        }
        return bmp;
    }

    /// <summary>
    /// Capture the entire virtual desktop (all monitors).
    /// </summary>
    public static Bitmap CaptureFullScreen()
    {
        int x = NativeMethods.GetSystemMetrics(76);  // SM_XVIRTUALSCREEN
        int y = NativeMethods.GetSystemMetrics(77);  // SM_YVIRTUALSCREEN
        int w = NativeMethods.GetSystemMetrics(78);  // SM_CXVIRTUALSCREEN
        int h = NativeMethods.GetSystemMetrics(79);  // SM_CYVIRTUALSCREEN
        return CaptureScreenRegion(x, y, w, h);
    }

    /// <summary>
    /// Save bitmap to file (PNG or JPEG based on extension).
    /// </summary>
    public static void SaveToFile(Bitmap bmp, string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        var format = ext switch
        {
            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
            ".bmp" => ImageFormat.Bmp,
            _ => ImageFormat.Png,
        };
        bmp.Save(filePath, format);
    }

    /// <summary>
    /// Convert bitmap to PNG byte array (for Vision API).
    /// </summary>
    public static byte[] ToPngBytes(Bitmap bmp)
    {
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }

    /// <summary>
    /// Convert bitmap to base64-encoded PNG string (for Vision API).
    /// </summary>
    public static string ToPngBase64(Bitmap bmp)
    {
        return Convert.ToBase64String(ToPngBytes(bmp));
    }
}
