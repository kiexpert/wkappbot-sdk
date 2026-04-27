using System.Drawing;
using System.Drawing.Imaging;

namespace WKAppBot.Win32.Input;

public static partial class ScreenCapture
{
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
