using System.Windows.Forms;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // wkappbot win-move "window title" [--x N --y N] [--right-top] [--margin N]
    static int WindowMoveCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: wkappbot win-move <window-title> [--x N --y N] [--right-top] [--margin N]");

        string title = args[0];
        bool rightTop = true;
        int? x = null, y = null;
        int margin = 8;

        for (int i = 1; i < args.Length; i++)
        {
            var a = args[i].ToLowerInvariant();
            if (a == "--x" && i + 1 < args.Length && int.TryParse(args[i + 1], out var xv)) { x = xv; i++; }
            else if (a == "--y" && i + 1 < args.Length && int.TryParse(args[i + 1], out var yv)) { y = yv; i++; }
            else if (a == "--margin" && i + 1 < args.Length && int.TryParse(args[i + 1], out var mv)) { margin = Math.Max(0, mv); i++; }
            else if (a == "--right-top") rightTop = true;
        }

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
            return Error($"Window not found: \"{title}\"");

        var win = windows[0];
        if (!NativeMethods.GetWindowRect(win.Handle, out var rc))
            return Error("GetWindowRect failed.");

        int w = rc.Right - rc.Left;
        int h = rc.Bottom - rc.Top;
        if (w <= 0 || h <= 0) { w = 380; h = 280; }

        int tx, ty;
        if (x.HasValue && y.HasValue)
        {
            tx = x.Value;
            ty = y.Value;
        }
        else if (rightTop)
        {
            var rightMost = Screen.AllScreens.OrderByDescending(s => s.Bounds.Right).FirstOrDefault() ?? Screen.PrimaryScreen;
            if (rightMost == null)
                return Error("No monitor found.");
            tx = rightMost.Bounds.Right - w - margin;
            ty = rightMost.Bounds.Top + margin;
        }
        else
        {
            tx = rc.Left;
            ty = rc.Top;
        }

        var ok = NativeMethods.SetWindowPos(
            win.Handle,
            NativeMethods.HWND_TOPMOST,
            tx, ty, 0, 0,
            NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_SHOWWINDOW);

        if (!ok)
            return Error("SetWindowPos failed.");

        Console.Error.WriteLine($"[MOVE] \"{win.Title}\" -> ({tx},{ty})");
        return 0;
    }
}
