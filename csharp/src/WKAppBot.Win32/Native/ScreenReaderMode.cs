using System.Diagnostics;
using System.Text;

namespace WKAppBot.Win32.Native;

/// <summary>
/// Announce WKAppBot as a screen reader to the OS — but only when needed.
///
/// Chromium/Electron apps check SPI_GETSCREENREADER to decide whether
/// to build their accessibility tree. Native Win32/MFC apps don't need this.
///
/// Strategy: "읽어보고 판단" — check the target window first.
///   If it's Chromium/Electron → enable screen reader flag → A11Y tree appears.
///   If it's native Win32/MFC → skip (UIA already works).
///
/// Once enabled, stays on PERMANENTLY (survives process exit).
/// Next run: already ON → zero broadcast delay → instant A11Y access.
/// Reboot clears it, but next wkappbot run re-enables automatically.
/// </summary>
public static class ScreenReaderMode
{
    private static bool _weEnabled;

    /// <summary>
    /// Check if the target window is a modern app (Chromium/Electron) that
    /// needs the screen reader flag for UIA tree activation.
    /// Enables once and stays on — safe for repeated calls.
    /// </summary>
    public static void EnableForWindow(IntPtr hWnd)
    {
        if (_weEnabled) return; // already enabled this session

        var sw = Stopwatch.StartNew();
        bool modern = IsModernApp(hWnd);
        sw.Stop();

        if (!modern)
        {
            Console.WriteLine($"  [PROF:A11Y] ScreenReader skip (native) classCheck={sw.ElapsedMilliseconds}ms");
            Console.Out.Flush();
            return;
        }

        Enable(); // Enable() has its own profiling
    }

    /// <summary>
    /// Check window class and decide: modern (Chromium/Electron) or native (Win32/MFC)?
    /// </summary>
    public static bool IsModernApp(IntPtr hWnd)
    {
        var classBuf = new StringBuilder(256);
        NativeMethods.GetClassNameW(hWnd, classBuf, classBuf.Capacity);
        string className = classBuf.ToString();

        // Chromium-based apps: Chrome, Edge, Electron, Claude Desktop, VS Code, etc.
        if (className.StartsWith("Chrome_WidgetWin", StringComparison.Ordinal)) return true;

        // Electron wrapper classes
        if (className.Contains("Electron", StringComparison.OrdinalIgnoreCase)) return true;

        // CEF (Chromium Embedded Framework) apps
        if (className.StartsWith("CefBrowserWindow", StringComparison.Ordinal)) return true;

        return false;
    }

    /// <summary>
    /// Force enable screen reader mode (system-wide announcement).
    /// Broadcasts WM_SETTINGCHANGE so Chromium picks it up immediately.
    /// Stays on permanently — no restore on exit.
    /// </summary>
    public static void Enable()
    {
        if (_weEnabled) return;

        var sw = Stopwatch.StartNew();

        // Check current state — already on? (previous run or real screen reader)
        NativeMethods.SystemParametersInfoW(
            NativeMethods.SPI_GETSCREENREADER, 0, out int current, 0);

        if (current != 0)
        {
            // Already enabled (previous wkappbot run or real AT) — skip broadcast
            _weEnabled = true;
            sw.Stop();
            Console.WriteLine($"  [PROF:A11Y] ScreenReader already_on={sw.ElapsedMilliseconds}ms");
            Console.Out.Flush();
            return;
        }

        // Announce ourselves + broadcast WM_SETTINGCHANGE to all windows
        bool ok = NativeMethods.SystemParametersInfoW(
            NativeMethods.SPI_SETSCREENREADER, 1, IntPtr.Zero,
            NativeMethods.SPIF_SENDCHANGE);
        sw.Stop();

        if (ok)
        {
            _weEnabled = true;
            Console.WriteLine($"[A11Y] Screen reader mode enabled — Chromium/Electron A11Y trees activated");
            Console.WriteLine($"  [PROF:A11Y] ScreenReader broadcast={sw.ElapsedMilliseconds}ms (SPIF_SENDCHANGE)");
        }
        else
        {
            Console.WriteLine($"  [PROF:A11Y] ScreenReader FAILED={sw.ElapsedMilliseconds}ms");
        }
        Console.Out.Flush();
    }

    /// <summary>
    /// Whether screen reader mode is currently active (by us or already was).
    /// </summary>
    public static bool IsEnabled => _weEnabled;
}
