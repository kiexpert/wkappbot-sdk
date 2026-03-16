using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    static IntPtr GetElementHwnd(AutomationElement el)
    {
        try
        {
            var nh = el.Properties.NativeWindowHandle.ValueOrDefault;
            if (nh != IntPtr.Zero) return nh;
        }
        catch { }
        return IntPtr.Zero;
    }

    static System.Drawing.Rectangle? GetBoundingRect(AutomationElement el)
    {
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0 && r.Height > 0)
                return new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
        catch { }
        return null;
    }

    // FLUTTERVIEW child = direct Flutter input pipeline (focusless, ~0ms) — no class whitelist needed
    static IntPtr FindFlutterViewChild(IntPtr hwnd) =>
        NativeMethods.FindWindowExW(hwnd, IntPtr.Zero, "FLUTTERVIEW", null);

    // TODO: when nullMs >= 100 (window lagging), skip verbose UIA tree/prop output in inspect/find
    //       GetNullMs() is already available — just gate heavy output behind nullMs < 100 check

    // Hollow invoke detection via Win32 SetProp/GetProp:
    //   GetPropW(elHwnd, InvokeHollowProp) != 0  →  UIA Invoke is hollow for this hwnd
    //   Prop is auto-cleaned when the window closes — no stale data.
    //   On hollow confirmed: SetProp stamps the hwnd + fires ActionApi.OnInvokeHollow → knowhow

    // WM_NULL baseline cache: pre-measured during read/find, reused by invoke (TTL = 3s)
    static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, (long nullMs, long ticks)> _nullCache = new();
    static readonly long NullCacheTtlTicks = 3 * System.Diagnostics.Stopwatch.Frequency; // 3 seconds

    /// <summary>Send WM_NULL to hwnd and cache the roundtrip time. Call during read/find so invoke gets it for free.</summary>
    internal static void PreheatWindow(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        NativeMethods.SendMessageTimeoutW(hwnd, NativeMethods.WM_NULL,
            IntPtr.Zero, IntPtr.Zero, NativeMethods.SMTO_ABORTIFHUNG, 100, out _);
        sw.Stop();
        _nullCache[hwnd] = (sw.ElapsedMilliseconds, System.Diagnostics.Stopwatch.GetTimestamp());
    }

    static long GetNullMs(IntPtr hwnd)
    {
        if (hwnd != IntPtr.Zero && _nullCache.TryGetValue(hwnd, out var entry))
        {
            if (System.Diagnostics.Stopwatch.GetTimestamp() - entry.ticks < NullCacheTtlTicks)
                return entry.nullMs; // fresh cache
        }
        // Cache miss or stale — measure now
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var ret = NativeMethods.SendMessageTimeoutW(hwnd, NativeMethods.WM_NULL,
            IntPtr.Zero, IntPtr.Zero, NativeMethods.SMTO_ABORTIFHUNG, 100, out _);
        sw.Stop();
        var ms = sw.ElapsedMilliseconds;
        _nullCache[hwnd] = (ms, System.Diagnostics.Stopwatch.GetTimestamp());
        return ret == IntPtr.Zero ? 100 : ms; // timeout → treat as 100ms (lagging)
    }

    // Returns true = confirmed hollow (no paint after 50ms), false = paint detected (real invoke)
    static bool ConfirmHollow(IntPtr elHwnd)
    {
        if (elHwnd == IntPtr.Zero) return true;
        NativeMethods.ValidateRect(elHwnd, IntPtr.Zero);   // clear any pre-existing dirty region
        Thread.Sleep(50);                                   // let message pump process results
        bool hasPaint = NativeMethods.GetUpdateRect(elHwnd, IntPtr.Zero, false);
        return !hasPaint; // no paint = hollow confirmed
    }

    /// <summary>
    /// Ensure target element's tab is active. Walks up UIA parent chain looking for
    /// unselected TabItem ancestors → auto-select them (focusless UIA SelectionItem).
    /// Returns true if a tab was activated (caller may need to re-resolve scope).
    /// </summary>
    static bool EnsureTabActive(AutomationElement el)
    {
        try
        {
            var current = el;
            for (int i = 0; i < 20; i++) // max 20 levels up
            {
                AutomationElement? parent;
                try { parent = current.Parent; } catch { break; }
                if (parent == null) break;

                try
                {
                    var ct = parent.Properties.ControlType.ValueOrDefault;
                    if (ct == ControlType.TabItem)
                    {
                        // Check if this TabItem is selected
                        try
                        {
                            var selPat = parent.Patterns.SelectionItem;
                            if (selPat.IsSupported && !selPat.Pattern.IsSelected.Value)
                            {
                                var tabName = parent.Properties.Name.ValueOrDefault ?? "(unnamed)";
                                selPat.Pattern.Select();
                                Console.WriteLine($"[A11Y] tab activated: \"{tabName}\"");
                                Thread.Sleep(200);
                                return true;
                            }
                        }
                        catch { }
                    }
                }
                catch { }

                current = parent;
            }
        }
        catch { }
        return false;
    }
}
