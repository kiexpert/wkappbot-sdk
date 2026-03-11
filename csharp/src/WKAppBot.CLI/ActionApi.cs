using System.Drawing;
using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Global action API — wraps UIA/physical actions with auto-zoom policy.
/// Every UI action (focusless UIA + physical click) shows a zoom overlay
/// so the user can see what the bot is touching.
///
/// Pattern:
///   var ok = ActionApi.Select(tabItem, hwnd, "잔고(15단)");
///   // = zoom appears → UiaLocator.TrySelect() → zoom shows pass/fail → auto-fade
///
/// Modeled after FocuslessGuard: global static class, policy at API boundary.
/// Tag: [ZOOM]
/// Tag: [FOCUSSTEALER] — marks windows that steal focus during nominally-focusless UIA actions.
/// </summary>
public static class ActionApi
{
    /// <summary>Master switch for zoom overlays. Default = true.</summary>
    public static bool ZoomEnabled { get; set; } = true;

    // ── FocusStealer Win32 prop ──
    // If a nominally-focusless UIA action (invoke/toggle/select/expand/collapse) steals focus,
    // we stamp the root hwnd with "WKAppBot_FocusStealer-{action}" = 1.
    // Next time InputReadiness.Probe() runs on the same hwnd, it forces the yield popup.
    // Prop is auto-cleaned when the window closes (Win32 guarantee).

    public const string FocusStealerPropPrefix = "WKAppBot_FocusStealer-";

    /// <summary>Fired when a UIA action steals focus. Args: (rootHwnd, actionName).</summary>
    public static Action<IntPtr, string>? OnFocusStealer;

    // ── UIA Focusless Actions ──

    public static bool Invoke(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "invoke", label);
        var prevFg = NativeMethods.GetForegroundWindow();
        var ok = UiaLocator.TryInvoke(el);
        EndZoom(zoom, ok, "Invoke", label);
        CheckFocusTheft(hwnd, "invoke", prevFg);
        return ok;
    }

    public static bool Select(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "select", label);
        var prevFg = NativeMethods.GetForegroundWindow();
        var ok = UiaLocator.TrySelect(el);
        EndZoom(zoom, ok, "Select", label);
        CheckFocusTheft(hwnd, "select", prevFg);
        return ok;
    }

    public static bool Toggle(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "toggle", label);
        var prevFg = NativeMethods.GetForegroundWindow();
        var ok = UiaLocator.TryToggle(el);
        EndZoom(zoom, ok, "Toggle", label);
        CheckFocusTheft(hwnd, "toggle", prevFg);
        return ok;
    }

    public static bool Expand(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "expand", label);
        var prevFg = NativeMethods.GetForegroundWindow();
        var ok = UiaLocator.TryExpand(el);
        EndZoom(zoom, ok, "Expand", label);
        CheckFocusTheft(hwnd, "expand", prevFg);
        return ok;
    }

    public static bool Collapse(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "collapse", label);
        var prevFg = NativeMethods.GetForegroundWindow();
        var ok = UiaLocator.TryCollapse(el);
        EndZoom(zoom, ok, "Collapse", label);
        CheckFocusTheft(hwnd, "collapse", prevFg);
        return ok;
    }

    public static bool ScrollIntoView(AutomationElement el, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForElement(el, hwnd, "scroll", label);
        var ok = UiaLocator.TryScrollIntoView(el);
        EndZoom(zoom, ok, "ScrollIntoView", label);
        // Scroll doesn't steal focus — skip check
        return ok;
    }

    // ── Physical Actions (focus-stealing) ──

    public static void Click(int screenX, int screenY, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForPoint(screenX, screenY, hwnd, "click", label);
        MouseInput.Click(screenX, screenY);
        zoom?.ShowPass($"Click {label}");
    }

    public static void DoubleClick(int screenX, int screenY, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForPoint(screenX, screenY, hwnd, "dblclick", label);
        MouseInput.DoubleClick(screenX, screenY);
        zoom?.ShowPass($"DblClick {label}");
    }

    public static void RightClick(int screenX, int screenY, IntPtr hwnd, string label)
    {
        using var zoom = BeginZoomForPoint(screenX, screenY, hwnd, "rightclick", label);
        MouseInput.RightClick(screenX, screenY);
        zoom?.ShowPass($"RightClick {label}");
    }

    // ── Zoom Helpers ──

    /// <summary>
    /// Begin zoom overlay centered on UIA element's BoundingRectangle.
    /// Returns null if zoom disabled, element has no bounds, or zoom creation fails.
    /// </summary>
    private static ClickZoomHelper? BeginZoomForElement(
        AutomationElement el, IntPtr hwnd, string source, string label)
    {
        if (!ZoomEnabled) return null;
        try
        {
            var br = el.BoundingRectangle;
            if (br.Width <= 0 || br.Height <= 0) return null;
            var rect = new Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
            return ClickZoomHelper.BeginFromRect(rect, hwnd, source, label);
        }
        catch { return null; } // zoom is non-critical
    }

    /// <summary>
    /// Begin zoom overlay centered on a screen point (for physical clicks).
    /// Uses a 120x40 rect around the click point.
    /// </summary>
    private static ClickZoomHelper? BeginZoomForPoint(
        int screenX, int screenY, IntPtr hwnd, string source, string label)
    {
        if (!ZoomEnabled) return null;
        try
        {
            var rect = new Rectangle(screenX - 60, screenY - 20, 120, 40);
            return ClickZoomHelper.BeginFromRect(rect, hwnd, source, label);
        }
        catch { return null; }
    }

    /// <summary>Common zoom result handler.</summary>
    private static void EndZoom(ClickZoomHelper? zoom, bool ok, string verb, string label)
    {
        if (zoom == null) return;
        if (ok)
            zoom.ShowPass($"{verb} {label}");
        else
            zoom.ShowFail($"{verb} failed: {label}");
    }

    // ── FocusStealer Detection ──

    /// <summary>
    /// After a nominally-focusless UIA action, check if the foreground window changed.
    /// If it did, stamp the root hwnd with a Win32 prop and fire OnFocusStealer.
    /// On the next Probe() call targeting the same hwnd, InputReadiness will force yield popup.
    /// </summary>
    private static void CheckFocusTheft(IntPtr hwnd, string action, IntPtr prevFg)
    {
        try
        {
            var curFg = NativeMethods.GetForegroundWindow();
            if (curFg == prevFg || prevFg == IntPtr.Zero) return;

            // Restore previous foreground immediately
            NativeMethods.SetForegroundWindow(prevFg);

            var rootHwnd = NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);
            if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $"[FOCUSSTEALER] ⚠ {action} on hwnd=0x{hwnd:X} stole focus " +
                $"(was 0x{prevFg:X}, now 0x{curFg:X}) — marked for yield next time");
            Console.ResetColor();

            // Stamp root hwnd: prop persists until window closes (cross-process, zero file I/O)
            NativeMethods.SetPropW(rootHwnd, $"{FocusStealerPropPrefix}{action}", (IntPtr)1);

            // Notify subscribers (knowhow writer, etc.)
            OnFocusStealer?.Invoke(rootHwnd, action);
        }
        catch { /* non-critical */ }
    }

    /// <summary>Check if a given hwnd has a FocusStealer stamp for the specified action.</summary>
    public static bool HasFocusStealerProp(IntPtr hwnd, string action)
    {
        try
        {
            if (hwnd == IntPtr.Zero) return false;
            var rootHwnd = NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);
            if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;
            return NativeMethods.GetPropW(rootHwnd, $"{FocusStealerPropPrefix}{action}") != IntPtr.Zero;
        }
        catch { return false; }
    }
}
