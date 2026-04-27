using System.Drawing;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

public static partial class ScreenCapture
{
    /// <summary>
    /// Hide windows covering hWnd via alpha=0 (layered window trick), capture, then restore.
    /// Scans: (1) top-level windows above the target's root ancestor in Z-order that overlap
    /// the target rect; (2) sibling child windows that cover the target (MDI child case).
    /// No focus change -- works even for elevated target windows.
    /// </summary>
    private static Bitmap? CaptureWindowHideOverlays(IntPtr hWnd)
    {
        NativeMethods.GetWindowRect(hWnd, out var targetRect);
        if (targetRect.Width <= 0 || targetRect.Height <= 0) return null;

        var toHide = new List<(IntPtr hwnd, bool hadLayered)>();

        bool Overlaps(RECT r) =>
            r.Right > targetRect.Left && r.Left < targetRect.Right &&
            r.Bottom > targetRect.Top && r.Top < targetRect.Bottom;

        void MaybeHide(IntPtr w)
        {
            if (!NativeMethods.IsWindowVisible(w)) return;
            NativeMethods.GetWindowRect(w, out var wr);
            if (!Overlaps(wr)) return;
            int exStyle = NativeMethods.GetWindowLongW(w, NativeMethods.GWL_EXSTYLE);
            bool hadLayered = (exStyle & NativeMethods.WS_EX_LAYERED) != 0;
            if (!hadLayered)
                NativeMethods.SetWindowLongW(w, NativeMethods.GWL_EXSTYLE,
                    exStyle | NativeMethods.WS_EX_LAYERED);
            NativeMethods.SetLayeredWindowAttributes(w, 0, 0, NativeMethods.LWA_ALPHA);
            toHide.Add((w, hadLayered));
        }

        // (1) Top-level windows above the root ancestor in Z-order
        var root = NativeMethods.GetAncestor(hWnd, NativeMethods.GA_ROOT);
        bool passedRoot = false;
        NativeMethods.EnumWindows((w, _) =>
        {
            if (w == root) { passedRoot = true; return true; }
            if (!passedRoot) MaybeHide(w);
            return true;
        }, IntPtr.Zero);

        // (2) Sibling child windows above hWnd in Z-order (MDI child covering case)
        var parent = NativeMethods.GetParent(hWnd);
        if (parent != IntPtr.Zero)
        {
            bool passedTarget = false;
            NativeMethods.EnumChildWindows(parent, (w, _) =>
            {
                if (NativeMethods.GetParent(w) != parent) return true; // skip grandchildren
                if (w == hWnd) { passedTarget = true; return true; }
                if (!passedTarget) MaybeHide(w); // sibling is above hWnd
                return true;
            }, IntPtr.Zero);
        }

        if (toHide.Count == 0) return null;

        try
        {
            Thread.Sleep(60); // wait for DWM to apply transparency
            NativeMethods.GetWindowRect(hWnd, out var r);
            return CaptureScreenRegion(r.Left, r.Top, r.Width, r.Height);
        }
        finally
        {
            foreach (var (w, hadLayered) in toHide)
            {
                NativeMethods.SetLayeredWindowAttributes(w, 0, 255, NativeMethods.LWA_ALPHA);
                if (!hadLayered)
                {
                    int ex = NativeMethods.GetWindowLongW(w, NativeMethods.GWL_EXSTYLE);
                    NativeMethods.SetWindowLongW(w, NativeMethods.GWL_EXSTYLE,
                        ex & ~NativeMethods.WS_EX_LAYERED);
                }
            }
        }
    }

    /// <summary>
    /// Last-resort capture: briefly give focus to target window, capture screen region, restore focus.
    /// Only called if all focusless methods fail.
    /// </summary>
    private static Bitmap CaptureWindowWithFocusBorrow(IntPtr hWnd)
    {
        var prevFg = NativeMethods.GetForegroundWindow();
        try
        {
            NativeMethods.SetForegroundWindow(hWnd);
            Thread.Sleep(150); // wait for window to render in foreground
            NativeMethods.GetWindowRect(hWnd, out var r);
            return CaptureScreenRegion(r.Left, r.Top, r.Width, r.Height);
        }
        finally
        {
            if (prevFg != IntPtr.Zero && prevFg != hWnd)
                NativeMethods.SetForegroundWindow(prevFg);
        }
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
}
