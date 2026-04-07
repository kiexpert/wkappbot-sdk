using System.Drawing;
using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// X-Ray: makes overlapping windows nearly transparent so the target is visible.
/// Uses magic alpha=13 (0x0D) to identify affected windows for safe restore.
/// Dispose() restores all windows with the magic alpha back to fully opaque.
///
/// Usage:
///   using var xray = XRayHelper.Begin(targetHwnd);
///   // ... perform action, user can see through ...
///   // Dispose → auto-restore
/// </summary>
internal sealed class XRayHelper : IDisposable
{
    private const byte MagicAlpha = 13;  // 0x0D — unlucky number, nobody uses this
    private const byte XRayAlpha  = 25;  // ~90% transparent (255 * 0.10 ≈ 25)

    private readonly List<(IntPtr hwnd, int origExStyle, bool wasLayered)> _affected = new();
    private bool _disposed;

    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern bool GetLayeredWindowAttributes(
        IntPtr hwnd, IntPtr pcrKey, out byte pbAlpha, IntPtr pdwFlags);

    private XRayHelper() { }

    /// <summary>
    /// Find all visible top-level windows overlapping the target rect and make them ~90% transparent.
    /// Returns null if no windows overlap (nothing to X-ray).
    /// </summary>
    public static XRayHelper? Begin(IntPtr targetHwnd)
    {
        NativeMethods.GetWindowRect(targetHwnd, out var targetRect);
        if (targetRect.Width <= 0 || targetRect.Height <= 0) return null;

        var target = new Rectangle(targetRect.Left, targetRect.Top, targetRect.Width, targetRect.Height);

        // Get target's own top-level window (ancestor) to exclude it
        var targetRoot = GetRootOwner(targetHwnd);

        var helper = new XRayHelper();
        var myPid = (uint)Environment.ProcessId;

        NativeMethods.EnumWindows((hwnd, _) =>
        {
            // Skip invisible
            if (!IsWindowVisible(hwnd)) return true;

            // Z-order stop: EnumWindows goes top→bottom, so hitting target root
            // means everything after is BEHIND the target — no need to ghost
            if (hwnd == targetRoot || GetRootOwner(hwnd) == targetRoot)
                return false; // stop enumeration

            if (hwnd == targetHwnd) return false;

            // Skip our own process (Eye, overlays, etc.)
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            if (pid == myPid) return true;

            // Check overlap with target rect
            NativeMethods.GetWindowRect(hwnd, out var wr);
            var winRect = new Rectangle(wr.Left, wr.Top, wr.Width, wr.Height);
            if (!winRect.IntersectsWith(target)) return true;

            // Make it transparent
            int exStyle = NativeMethods.GetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE);
            bool wasLayered = (exStyle & NativeMethods.WS_EX_LAYERED) != 0;

            // Skip if already has our magic alpha (re-entrant safety)
            if (wasLayered)
            {
                if (GetLayeredWindowAttributes(hwnd, IntPtr.Zero, out byte curAlpha, IntPtr.Zero) && curAlpha == MagicAlpha)
                    return true;
            }

            if (!wasLayered)
            {
                NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE, exStyle | NativeMethods.WS_EX_LAYERED);
            }

            NativeMethods.SetLayeredWindowAttributes(hwnd, 0, MagicAlpha, NativeMethods.LWA_ALPHA);
            helper._affected.Add((hwnd, exStyle, wasLayered));

            return true;
        }, IntPtr.Zero);

        if (helper._affected.Count == 0) return null;

        Console.Error.Write($"[XRAY] {helper._affected.Count} window(s) ghosted ");
        return helper;
    }

    private static IntPtr GetRootOwner(IntPtr hwnd)
    {
        const uint GA_ROOTOWNER = 3;
        return NativeMethods.GetAncestor(hwnd, GA_ROOTOWNER);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        foreach (var (hwnd, origExStyle, wasLayered) in _affected)
        {
            try
            {
                // Verify it still has our magic alpha (another process may have changed it)
                if (GetLayeredWindowAttributes(hwnd, IntPtr.Zero, out byte curAlpha, IntPtr.Zero) && curAlpha == MagicAlpha)
                {
                    if (wasLayered)
                    {
                        // Was already layered — restore to fully opaque but keep layered style
                        NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 255, NativeMethods.LWA_ALPHA);
                    }
                    else
                    {
                        // Wasn't layered — remove WS_EX_LAYERED entirely
                        NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE, origExStyle);
                    }
                }
            }
            catch { /* window may have closed */ }
        }

        if (_affected.Count > 0)
            Console.Error.Write($"[XRAY] {_affected.Count} window(s) restored ");
    }

    /// <summary>
    /// Global cleanup: scan ALL visible top-level windows for MagicAlpha=13 and restore them.
    /// Call on process exit / Eye shutdown to recover from leaked X-ray (Dispose not called).
    /// Safe to call multiple times — only affects windows with the exact magic alpha value.
    /// </summary>
    public static int RestoreAll()
    {
        int restored = 0;
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            if (!IsWindowVisible(hwnd)) return true;
            int exStyle = NativeMethods.GetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE);
            if ((exStyle & NativeMethods.WS_EX_LAYERED) == 0) return true;

            if (GetLayeredWindowAttributes(hwnd, IntPtr.Zero, out byte alpha, IntPtr.Zero) && alpha == MagicAlpha)
            {
                // Restore to fully opaque — safest option since we don't know original state
                NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 255, NativeMethods.LWA_ALPHA);
                restored++;
            }
            return true;
        }, IntPtr.Zero);

        if (restored > 0)
            Console.Error.WriteLine($"[XRAY] RestoreAll: {restored} leaked window(s) recovered");
        return restored;
    }
}
