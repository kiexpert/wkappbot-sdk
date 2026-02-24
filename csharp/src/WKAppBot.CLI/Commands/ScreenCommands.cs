using System.Diagnostics;
using System.Drawing;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int ScreenCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  wkappbot screen off [--no-check]");
            Console.WriteLine("      Turn off monitor immediately (WM_SYSCOMMAND/SC_MONITORPOWER=2).");
            Console.WriteLine("      Default: ensure Eye window, capture it, and fail if image is nearly black.");
            return 0;
        }

        var sub = args[0].ToLowerInvariant();
        var noCheck = args.Any(a => a.Equals("--no-check", StringComparison.OrdinalIgnoreCase));

        if (sub != "off")
            return Error($"Unknown screen subcommand: {sub}");

        var eye = FindEyeWindow();
        if (eye == IntPtr.Zero)
        {
            Console.WriteLine("[SCREEN] Eye window not found. starting background eye...");
            StartEyeBackground();
            Thread.Sleep(1500);
            eye = FindEyeWindow();
        }

        // WM_SYSCOMMAND(0x0112), SC_MONITORPOWER(0xF170), lParam=2 (power off)
        var hwndBroadcast = new IntPtr(0xFFFF);
        NativeMethods.SendMessageW(hwndBroadcast, 0x0112, new IntPtr(0xF170), new IntPtr(2));
        Console.WriteLine("[SCREEN] monitor off command sent");

        if (noCheck)
        {
            Console.WriteLine("[SCREEN] check skipped (--no-check)");
            return 0;
        }

        Thread.Sleep(3000);

        bool wkAlive = Process.GetProcessesByName("wkappbot").Length > 0;
        bool nodeAlive = Process.GetProcessesByName("node").Length > 0;

        if (eye == IntPtr.Zero)
        {
            Console.WriteLine($"[SCREEN] runtime check: wkappbot={(wkAlive ? "alive" : "down")}, node={(nodeAlive ? "alive" : "down")}, eye=missing");
            return 3;
        }

        var shot = Path.Combine(DataDir, "screen_off_eye_check.png");
        if (!TryCaptureWindow(eye, shot, out var nearBlack, out var brightRatio))
        {
            Console.WriteLine($"[SCREEN] runtime check: wkappbot={(wkAlive ? "alive" : "down")}, node={(nodeAlive ? "alive" : "down")}, eye-capture=failed");
            return 4;
        }

        Console.WriteLine($"[SCREEN] runtime check: wkappbot={(wkAlive ? "alive" : "down")}, node={(nodeAlive ? "alive" : "down")}, eye-bright-ratio={brightRatio:F4}, shot={shot}");

        if (nearBlack)
        {
            Console.WriteLine("[SCREEN] FAIL: eye shot is nearly black (likely lock-screen or inaccessible desktop)");
            return 5;
        }

        return (wkAlive && nodeAlive) ? 0 : 2;
    }

    static void StartEyeBackground()
    {
        try
        {
            var exe = Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(exe) || !File.Exists(exe))
                return;

            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = "eye --size 380x280",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process.Start(psi);
        }
        catch { }
    }

    static IntPtr FindEyeWindow()
    {
        IntPtr found = IntPtr.Zero;
        EnumWindows((hWnd, _) =>
        {
            if (!IsWindowVisible(hWnd)) return true;
            var sb = new StringBuilder(512);
            GetWindowTextW(hWnd, sb, sb.Capacity);
            var t = sb.ToString();
            if (t.StartsWith("WK AppBot Global Eye", StringComparison.OrdinalIgnoreCase) ||
                t.StartsWith("WK AppBot Eye", StringComparison.OrdinalIgnoreCase))
            {
                found = hWnd;
                return false;
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }

    static bool TryCaptureWindow(IntPtr hWnd, string outPath, out bool nearBlack, out double brightRatio)
    {
        nearBlack = true;
        brightRatio = 0;
        try
        {
            if (!GetWindowRect(hWnd, out MV_RECT rc)) return false;
            int w = Math.Max(1, rc.Right - rc.Left);
            int h = Math.Max(1, rc.Bottom - rc.Top);

            using var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
                g.CopyFromScreen(rc.Left, rc.Top, 0, 0, new Size(w, h));

            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
            bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);

            int sample = 0;
            int bright = 0;
            int step = Math.Max(1, Math.Min(w, h) / 40);
            for (int y = 0; y < h; y += step)
            {
                for (int x = 0; x < w; x += step)
                {
                    var c = bmp.GetPixel(x, y);
                    sample++;
                    if ((c.R + c.G + c.B) >= 45) bright++;
                }
            }

            brightRatio = sample > 0 ? (double)bright / sample : 0;
            nearBlack = brightRatio < 0.01;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
