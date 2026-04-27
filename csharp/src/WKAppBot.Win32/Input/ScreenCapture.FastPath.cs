using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public static partial class ScreenCapture
{
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

        // GPU-composited fallback (e.g. nfrunlite): try GetWindowDC + BitBlt.
        // PrintWindow with PW_RENDERFULLCONTENT returns black for DirectX/GPU windows.
        // BitBlt on the window DC sometimes captures the backbuffer composite.
        var wndDc = NativeMethods.GetDC(hWnd);
        if (wndDc != IntPtr.Zero)
        {
            try
            {
                var bmp2 = new Bitmap(w, h, PixelFormat.Format32bppArgb);
                using var g2 = Graphics.FromImage(bmp2);
                IntPtr dst = g2.GetHdc();
                bool copied = NativeMethods.BitBlt(dst, 0, 0, w, h, wndDc, 0, 0, 0x00CC0020 /*SRCCOPY*/);
                g2.ReleaseHdc(dst);
                if (copied && !IsBlankBitmap(bmp2))
                    return bmp2;
                bmp2.Dispose();
            }
            finally
            {
                NativeMethods.ReleaseDC(hWnd, wndDc);
            }
        }

        return null; // Skip this frame -- caller will retry in ~200ms
    }
}
