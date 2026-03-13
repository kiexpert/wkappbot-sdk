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
    public const string InvokeHollowProp       = "WKAppBot_InvokeHollow";

    /// <summary>Fired when a UIA action steals focus. Args: (rootHwnd, actionName).</summary>
    public static Action<IntPtr, string>? OnFocusStealer;

    /// <summary>Fired when UIA Invoke is confirmed hollow (no-op stub). Args: (elHwnd, elClassName).
    /// Receiver should write to knowhow so future sessions know to skip UIA Invoke.</summary>
    public static Action<IntPtr, string>? OnInvokeHollow;

    // ── UIA Focusless Actions ──
    // Zoom runs in parallel with the UIA action — ~334ms → ~0ms blocking. [PROF]
    // BoundingRect is captured on the calling thread (COM-safe), then WPF window
    // creation runs on Task.Run so it doesn't block the UIA call.

    public static bool Invoke(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "invoke", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TryInvoke(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "Invoke", label);
        CheckSideEffects(hwnd, "invoke", snap);
        return ok;
    }

    public static bool Select(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "select", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TrySelect(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "Select", label);
        CheckSideEffects(hwnd, "select", snap);
        return ok;
    }

    public static bool Toggle(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "toggle", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TryToggle(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "Toggle", label);
        CheckSideEffects(hwnd, "toggle", snap);
        return ok;
    }

    public static bool Expand(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "expand", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TryExpand(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "Expand", label);
        CheckSideEffects(hwnd, "expand", snap);
        return ok;
    }

    public static bool Collapse(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "collapse", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TryCollapse(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "Collapse", label);
        CheckSideEffects(hwnd, "collapse", snap);
        return ok;
    }

    public static bool ScrollIntoView(AutomationElement el, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForElementAsync(el, hwnd, "scroll", label);
        var snap = SnapshotState();
        var ok = UiaLocator.TryScrollIntoView(el);
        EndZoom(zoomTask.GetAwaiter().GetResult(), ok, "ScrollIntoView", label);
        CheckSideEffects(hwnd, "scroll", snap);
        return ok;
    }

    // ── Physical Actions (focus-stealing) ──

    public static void Click(int screenX, int screenY, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForPointAsync(screenX, screenY, hwnd, "click", label);
        MouseInput.Click(screenX, screenY);
        zoomTask.GetAwaiter().GetResult()?.ShowPass($"Click {label}");
    }

    public static void DoubleClick(int screenX, int screenY, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForPointAsync(screenX, screenY, hwnd, "dblclick", label);
        MouseInput.DoubleClick(screenX, screenY);
        zoomTask.GetAwaiter().GetResult()?.ShowPass($"DblClick {label}");
    }

    public static void RightClick(int screenX, int screenY, IntPtr hwnd, string label)
    {
        var zoomTask = BeginZoomForPointAsync(screenX, screenY, hwnd, "rightclick", label);
        MouseInput.RightClick(screenX, screenY);
        zoomTask.GetAwaiter().GetResult()?.ShowPass($"RightClick {label}");
    }

    // ── Zoom Helpers ──

    /// <summary>
    /// Begin zoom overlay for a UIA element — async so it runs in parallel with the UIA action.
    /// BoundingRect is captured on the calling thread (COM-safe); WPF window creation is offloaded.
    /// </summary>
    private static Task<ClickZoomHelper?> BeginZoomForElementAsync(
        AutomationElement el, IntPtr hwnd, string source, string label)
    {
        if (!ZoomEnabled) return Task.FromResult<ClickZoomHelper?>(null);
        try
        {
            var br = el.BoundingRectangle; // COM call — must stay on calling thread
            if (br.Width <= 0 || br.Height <= 0) return Task.FromResult<ClickZoomHelper?>(null);
            var rect = new Rectangle((int)br.X, (int)br.Y, (int)br.Width, (int)br.Height);
            return Task.Run(() => ClickZoomHelper.BeginFromRect(rect, hwnd, source, label));
        }
        catch { return Task.FromResult<ClickZoomHelper?>(null); }
    }

    /// <summary>
    /// Begin zoom overlay for a screen point — async so it runs in parallel with the physical click.
    /// </summary>
    private static Task<ClickZoomHelper?> BeginZoomForPointAsync(
        int screenX, int screenY, IntPtr hwnd, string source, string label)
    {
        if (!ZoomEnabled) return Task.FromResult<ClickZoomHelper?>(null);
        return Task.Run(() =>
        {
            try
            {
                var rect = new Rectangle(screenX - 60, screenY - 20, 120, 40);
                return ClickZoomHelper.BeginFromRect(rect, hwnd, source, label);
            }
            catch { return null; }
        });
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

    // ── FocusStealer / MouseStealer Detection ──
    // Nominally-focusless UIA actions (invoke/toggle/select/expand/collapse) should not
    // change foreground window OR move the mouse cursor. If they do, we:
    //   1. Restore the stolen resource immediately (focus / cursor)
    //   2. Stamp a Win32 prop on the root hwnd (auto-cleaned when window closes)
    //   3. Fire OnFocusStealer callback (knowhow writer, warning overlay)
    //   4. Next InputReadiness.Probe() detects the prop → forces yield popup

    public const string MouseStealerPropPrefix  = "WKAppBot_MouseStealer-";
    private const int   CursorMoveThresholdPx   = 4; // ignore sub-pixel jitter

    /// <summary>
    /// Snapshot focus + cursor before a UIA action, then call CheckSideEffects after.
    /// Returns an opaque snapshot to pass to CheckSideEffects.
    /// </summary>
    private static (IntPtr prevFg, int px, int py) SnapshotState()
    {
        var fg = NativeMethods.GetForegroundWindow();
        NativeMethods.GetCursorPos(out var pt);
        return (fg, pt.X, pt.Y);
    }

    /// <summary>
    /// After a nominally-focusless UIA action:
    ///   — If foreground changed → restore, stamp prop, fire callback.
    ///   — If cursor moved significantly → restore, stamp prop, fire callback.
    /// </summary>
    private static void CheckSideEffects(IntPtr hwnd, string action,
        (IntPtr prevFg, int px, int py) snap)
    {
        try
        {
            var rootHwnd = NativeMethods.GetAncestor(hwnd, NativeMethods.GA_ROOT);
            if (rootHwnd == IntPtr.Zero) rootHwnd = hwnd;

            // ── Focus theft ──
            var curFg = NativeMethods.GetForegroundWindow();
            if (snap.prevFg != IntPtr.Zero && curFg != snap.prevFg)
            {
                NativeMethods.SetForegroundWindowRaw(snap.prevFg); // restore stolen fg
                NativeMethods.SetPropW(rootHwnd, $"{FocusStealerPropPrefix}{action}", (IntPtr)1);
                NativeMethods.SetPropW(rootHwnd, "WKAppBot_FocusStealer", (IntPtr)1); // generic: cross-action detection

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"[FOCUSSTEALER] ⚠ {action} stole focus " +
                    $"(was 0x{snap.prevFg:X}, now 0x{curFg:X}) — restored + marked");
                Console.ResetColor();

                OnFocusStealer?.Invoke(rootHwnd, action);
            }

            // ── Mouse cursor theft ──
            NativeMethods.GetCursorPos(out var curPt);
            int dx = Math.Abs(curPt.X - snap.px);
            int dy = Math.Abs(curPt.Y - snap.py);
            if (dx > CursorMoveThresholdPx || dy > CursorMoveThresholdPx)
            {
                NativeMethods.SetCursorPos(snap.px, snap.py); // restore immediately
                NativeMethods.SetPropW(rootHwnd, $"{MouseStealerPropPrefix}{action}", (IntPtr)1);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    $"[MOUSESTEALER] ⚠ {action} moved cursor " +
                    $"({snap.px},{snap.py}) → ({curPt.X},{curPt.Y}), Δ({dx},{dy}) — restored + marked");
                Console.ResetColor();

                // Reuse same callback (args: rootHwnd, "mouse-{action}" distinguishes in knowhow)
                OnFocusStealer?.Invoke(rootHwnd, $"mouse-{action}");
            }
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
            return NativeMethods.GetPropW(rootHwnd, $"{FocusStealerPropPrefix}{action}") != IntPtr.Zero
                || NativeMethods.GetPropW(rootHwnd, $"{MouseStealerPropPrefix}{action}") != IntPtr.Zero;
        }
        catch { return false; }
    }
}
