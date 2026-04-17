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
    /// If minimized, captures the tiny taskbar icon area (caller can detect via IsIconic beforehand).
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

            if (ok && !IsBlankBitmap(bmp))
                return bmp;
        }
        bmp.Dispose();

        // Fallback: bring window to front -> capture from screen -> restore Z-order
        // (Prevents overlapping windows from appearing in the screenshot)
        return CaptureWindowWithBringToFront(hWnd);
    }

    /// <summary>
    /// PrintWindow-only capture -- NO BringToFront fallback.
    /// Returns null if PrintWindow fails (blank bitmap).
    /// Designed for live preview where skipping a frame is acceptable (~200ms until next attempt).
    /// Cost: ~5-15ms vs ~200ms+ with BringToFront fallback.
    /// </summary>
    public static Bitmap? TryPrintWindowOnly(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        int w = rect.Width;
        int h = rect.Height;
        if (w <= 0 || h <= 0) return null;

        var bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            IntPtr hdc = g.GetHdc();
            bool ok = NativeMethods.PrintWindow(hWnd, hdc, NativeMethods.PW_RENDERFULLCONTENT);
            g.ReleaseHdc(hdc);

            if (ok && !IsBlankBitmap(bmp))
                return bmp;
        }
        bmp.Dispose();
        return null; // Skip this frame -- caller will retry in ~200ms
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
    /// Check if a bitmap is blank (all-black or nearly uniform).
    /// Samples 9 points (3x3 grid) -- if ALL are (0,0,0), the capture failed.
    /// Important: bad screenshots pollute ExperienceDB and mislead future Claude sessions.
    /// </summary>
    public static bool IsBlankBitmap(Bitmap bmp)
    {
        if (bmp.Width <= 0 || bmp.Height <= 0) return true;

        // Sample 9 points in a 3x3 grid (avoids center-only false positives)
        int[] xs = { bmp.Width / 4, bmp.Width / 2, bmp.Width * 3 / 4 };
        int[] ys = { bmp.Height / 4, bmp.Height / 2, bmp.Height * 3 / 4 };

        foreach (int x in xs)
        {
            foreach (int y in ys)
            {
                if (x >= bmp.Width || y >= bmp.Height) continue;
                var p = bmp.GetPixel(x, y);
                if (p.R != 0 || p.G != 0 || p.B != 0)
                    return false; // At least one non-black pixel -> not blank
            }
        }
        return true; // All 9 sample points are black -> blank capture
    }

    /// <summary>
    /// Capture a sub-region of a window via PrintWindow + GDI viewport offset.
    /// Creates only a regionWidth×regionHeight bitmap instead of the full window.
    /// Falls back to full PrintWindow + CropRegion if viewport trick fails.
    ///
    /// Performance: Allocates only the needed bitmap size. On non-MFC apps this can be
    /// significantly faster. On MFC apps the bottleneck is WM_PRINT message processing
    /// (1000ms+) regardless of bitmap size, so savings are in GDI rendering + allocation only.
    ///
    /// Parameters: regionX/Y are relative to the window's top-left corner (not screen coordinates).
    /// </summary>
    public static Bitmap? CaptureWindowRegion(IntPtr hWnd, int regionX, int regionY,
        int regionWidth, int regionHeight)
    {
        NativeMethods.GetWindowRect(hWnd, out var rect);
        if (rect.Width <= 0 || rect.Height <= 0) return null;

        // Clamp region to window bounds
        if (regionX < 0) { regionWidth += regionX; regionX = 0; }
        if (regionY < 0) { regionHeight += regionY; regionY = 0; }
        if (regionX + regionWidth > rect.Width) regionWidth = rect.Width - regionX;
        if (regionY + regionHeight > rect.Height) regionHeight = rect.Height - regionY;
        if (regionWidth <= 0 || regionHeight <= 0) return null;

        // Strategy 1: Small bitmap + viewport offset (GDI renders only needed region)
        var bmp = new Bitmap(regionWidth, regionHeight, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(bmp))
        {
            IntPtr hdc = g.GetHdc();

            // Shift viewport so window's (regionX, regionY) maps to bitmap's (0, 0)
            NativeMethods.SetViewportOrgEx(hdc, -regionX, -regionY, out _);

            // Set clip region to ensure only our target area is rendered
            IntPtr hRgn = NativeMethods.CreateRectRgn(regionX, regionY,
                regionX + regionWidth, regionY + regionHeight);
            NativeMethods.SelectClipRgn(hdc, hRgn);
            NativeMethods.DeleteObject(hRgn);

            bool ok = NativeMethods.PrintWindow(hWnd, hdc, NativeMethods.PW_RENDERFULLCONTENT);

            // Reset viewport origin before releasing HDC
            NativeMethods.SetViewportOrgEx(hdc, 0, 0, out _);
            g.ReleaseHdc(hdc);

            if (ok && !IsBlankBitmap(bmp))
                return bmp;
        }
        bmp.Dispose();

        // Strategy 2: Full PrintWindow + CropRegion (fallback if viewport trick fails)
        try
        {
            using var fullBmp = TryPrintWindowOnly(hWnd);
            if (fullBmp != null)
                return CropRegion(fullBmp, regionX, regionY, regionWidth, regionHeight);
        }
        catch { }

        return null;
    }

    /// <summary>
    /// Crop a sub-region from an existing bitmap (e.g., extract control from form capture).
    /// Clamps the crop region to the bitmap bounds to avoid OutOfMemory on edge controls.
    /// </summary>
    public static Bitmap CropRegion(Bitmap source, int x, int y, int width, int height)
    {
        // Clamp to source bounds
        if (x < 0) { width += x; x = 0; }
        if (y < 0) { height += y; y = 0; }
        if (x + width > source.Width) width = source.Width - x;
        if (y + height > source.Height) height = source.Height - y;
        if (width <= 0 || height <= 0)
            throw new ArgumentException($"Crop region is empty after clamping: ({x},{y} {width}x{height}) from {source.Width}x{source.Height}");

        var cropRect = new Rectangle(x, y, width, height);
        return source.Clone(cropRect, source.PixelFormat);
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
