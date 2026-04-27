using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public static partial class ScreenCapture
{
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
}
