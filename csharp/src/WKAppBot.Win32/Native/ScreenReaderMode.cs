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
/// Once enabled, stays on for the lifetime of the process.
/// Restored on exit if WE enabled it (doesn't clobber real screen readers).
/// </summary>
public static class ScreenReaderMode
{
    private static bool _weEnabled;
    private static bool _checked;

    /// <summary>
    /// Check if the target window is a modern app (Chromium/Electron) that
    /// needs the screen reader flag for UIA tree activation.
    /// Enables once and stays on — safe for repeated calls.
    /// </summary>
    public static void EnableForWindow(IntPtr hWnd)
    {
        if (_weEnabled) return; // already enabled this session

        if (!IsModernApp(hWnd)) return; // native app, UIA works without this

        Enable();
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
    /// </summary>
    public static void Enable()
    {
        if (_weEnabled) return;

        // Check current state — don't clobber a real screen reader (NVDA/JAWS/Narrator)
        NativeMethods.SystemParametersInfoW(
            NativeMethods.SPI_GETSCREENREADER, 0, out int current, 0);

        if (current != 0)
        {
            // Real screen reader already running — A11Y trees are already active
            _weEnabled = true; // mark so we don't check again
            return;
        }

        // Announce ourselves + broadcast WM_SETTINGCHANGE to all windows
        bool ok = NativeMethods.SystemParametersInfoW(
            NativeMethods.SPI_SETSCREENREADER, 1, IntPtr.Zero,
            NativeMethods.SPIF_SENDCHANGE);

        if (ok)
        {
            _weEnabled = true;
            _checked = false; // we actually changed the flag
            Console.WriteLine("[A11Y] Screen reader mode enabled — Chromium/Electron A11Y trees activated");
        }
    }

    /// <summary>
    /// Restore previous state on exit. Only disables if WE enabled it.
    /// Safe: if a real screen reader was already running, we didn't change anything.
    /// </summary>
    public static void Restore()
    {
        if (!_weEnabled || _checked) return; // we didn't change the system flag

        NativeMethods.SystemParametersInfoW(
            NativeMethods.SPI_SETSCREENREADER, 0, IntPtr.Zero,
            NativeMethods.SPIF_SENDCHANGE);

        _weEnabled = false;
    }

    /// <summary>
    /// Whether screen reader mode is currently active (by us or by a real AT).
    /// </summary>
    public static bool IsEnabled => _weEnabled;
}
