using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Physical mouse input via SendInput.
/// Handles dual-monitor and DPI correctly via absolute virtual desktop coordinates.
/// </summary>
public static class MouseInput
{
    /// <summary>
    /// Move the cursor to screen coordinates (x, y).
    /// Works correctly with negative coordinates (dual monitor).
    /// BLOCKED by FocuslessGuard (moves physical cursor).
    /// </summary>
    public static void MoveTo(int x, int y)
    {
        FocuslessGuard.AssertAllowed("SetCursorPos");
        NativeMethods.SetCursorPos(x, y);
    }

    /// <summary>
    /// Left click at screen coordinates.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void Click(int x, int y)
    {
        FocuslessGuard.AssertAllowed("SendInput(mouse click)");
        MoveTo(x, y);
        Thread.Sleep(30);

        var inputs = new INPUT[2];

        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
        inputs[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();

        inputs[1].type = INPUT.INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
        inputs[1].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();

        NativeMethods.SendInput(2, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Double-click at screen coordinates.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void DoubleClick(int x, int y)
    {
        FocuslessGuard.AssertAllowed("SendInput(mouse double-click)");
        Click(x, y);
        Thread.Sleep(50);
        Click(x, y);
    }

    /// <summary>
    /// Right-click at screen coordinates.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void RightClick(int x, int y)
    {
        FocuslessGuard.AssertAllowed("SendInput(mouse right-click)");
        MoveTo(x, y);
        Thread.Sleep(30);

        var inputs = new INPUT[2];

        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_RIGHTDOWN;
        inputs[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();

        inputs[1].type = INPUT.INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_RIGHTUP;
        inputs[1].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();

        NativeMethods.SendInput(2, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Click at the center of a window's client area.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void ClickCenter(IntPtr hWnd)
    {
        FocuslessGuard.AssertAllowed("SendInput(mouse click center)");
        NativeMethods.GetWindowRect(hWnd, out var rect);
        int cx = (rect.Left + rect.Right) / 2;
        int cy = (rect.Top + rect.Bottom) / 2;
        Click(cx, cy);
    }

    /// <summary>
    /// Scroll up/down at current cursor position.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void Scroll(int clicks)
    {
        FocuslessGuard.AssertAllowed("SendInput(mouse scroll)");
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.mouseData = (uint)(clicks * 120); // WHEEL_DELTA = 120
        inputs[0].u.mi.dwFlags = 0x0800; // MOUSEEVENTF_WHEEL
        inputs[0].u.mi.dwExtraInfo = NativeMethods.GetMessageExtraInfo();

        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }
}
