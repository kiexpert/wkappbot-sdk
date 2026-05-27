using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class FixOffScreenChrome
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);

    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    static void Main()
    {
        Console.WriteLine("[FIX] Searching for off-screen Chrome windows...");

        foreach (var proc in Process.GetProcessesByName("chrome"))
        {
            IntPtr hwnd = proc.MainWindowHandle;
            if (hwnd == IntPtr.Zero || !IsWindow(hwnd)) continue;

            if (!GetWindowRect(hwnd, out RECT rect)) continue;

            Console.WriteLine($"[CHROME] pid={proc.Id} hwnd=0x{hwnd:X} rect=({rect.Left},{rect.Top},{rect.Right},{rect.Bottom})");

            // If window is off-screen (x < -100 or y < -100), move it to (-100, -100)
            if (rect.Left < -100 || rect.Top < -100)
            {
                Console.WriteLine($"[ACTION] Moving off-screen Chrome from ({rect.Left},{rect.Top}) to (-100,-100)");
                if (SetWindowPos(hwnd, IntPtr.Zero, -100, -100, 0, 0, SWP_NOSIZE | SWP_NOZORDER))
                {
                    Console.WriteLine($"[SUCCESS] Chrome repositioned");
                }
                else
                {
                    Console.WriteLine($"[ERROR] SetWindowPos failed: {Marshal.GetLastWin32Error()}");
                }
            }
        }

        Console.WriteLine("[DONE] Off-screen Chrome fix complete");
    }
}
