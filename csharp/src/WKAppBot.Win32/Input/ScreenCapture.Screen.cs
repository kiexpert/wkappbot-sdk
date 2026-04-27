using System.Drawing;
using System.Drawing.Imaging;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public static partial class ScreenCapture
{
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
}
