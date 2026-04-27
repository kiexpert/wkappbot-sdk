using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Invoke: FLUTTERVIEW Tier0 -> UIA Invoke -> BM_CLICK -> WM_LBUTTON fallback --
    static bool A11yInvoke(AutomationElement el, IntPtr hwnd)
    {
        var elHwnd = GetElementHwnd(el);

        // Tier 0: FLUTTERVIEW direct PostMessage -- bypasses Win32 routing, focusless, ~0ms
        if (elHwnd != IntPtr.Zero)
        {
            var flutterView = FindFlutterViewChild(elHwnd);
            if (flutterView != IntPtr.Zero)
            {
                Console.WriteLine("[A11Y] invoke -- Tier0 FLUTTERVIEW (focusless direct)");
                if (PostClickToFlutterView(el, flutterView)) { RecordTierSuccess(elHwnd, el, "invoke", "FLUTTERVIEW WM_LBUTTON", KnowhowCategory.Focusless); return true; }
                _autoHealTiers?.Add("FLUTTERVIEW Tier0: WM_LBUTTON sent, no UI response (pixel-diff < 2%)");
                // Fall through to UIA Invoke
            }
        }

        // Tier 1: UIA Invoke (focusless)
        // Skip if hwnd was previously confirmed hollow (Win32 prop, auto-cleared on window close)
        bool propHollow = elHwnd != IntPtr.Zero &&
                          NativeMethods.GetPropW(elHwnd, ActionApi.InvokeHollowProp) != IntPtr.Zero;
        if (propHollow)
        {
            Console.WriteLine("[A11Y] invoke -- UIA Invoke skipped (prop=hollow) -> next tier");
        }
        else
        {
            // WM_NULL baseline: no-op roundtrip measures system responsiveness.
            // If invoke ≈ null_ms -> hollow stub. If null_ms ≥ 100ms -> window is lagging, skip invoke entirely.
            long nullMs = 0;
            if (elHwnd != IntPtr.Zero)
            {
                nullMs = GetNullMs(elHwnd); // cached from read/find, or measures now
                if (nullMs >= 100)
                {
                    Console.WriteLine($"[A11Y] invoke -- window lagging (WM_NULL={nullMs}ms) -> skip UIA Invoke");
                    goto tier2;
                }
            }

            var prevFg = NativeMethods.GetForegroundWindow();
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool invoked = UiaLocator.TryInvoke(el);
            sw.Stop();
            long invokeMs = sw.ElapsedMilliseconds;

            if (invoked)
            {
                // Restore focus if stolen (even hollow stubs can activate target window)
                var curFg = NativeMethods.GetForegroundWindow();
                if (curFg != prevFg && prevFg != IntPtr.Zero)
                {
                    NativeMethods.SetForegroundWindowRaw(prevFg); // restore stolen fg after UIA Invoke
                    Console.WriteLine($"[A11Y] invoke -- UIA Invoke focus restored ({curFg:X8}->{prevFg:X8})");
                }

                if (invokeMs > 1000)
                {
                    Console.WriteLine($"[A11Y] invoke -- UIA Invoke ! BLOCKED ({invokeMs}ms, null={nullMs}ms)");
                    return true; // best effort
                }

                // Suspicious if invoke is no faster than a no-op WM_NULL (+2ms margin)
                bool suspiciousTiming = invokeMs <= nullMs + 2;
                Console.WriteLine($"[A11Y] invoke -- UIA Invoke {invokeMs}ms (null={nullMs}ms){(suspiciousTiming ? " ! suspicious" : "")}");

                if (suspiciousTiming)
                {
                    // Confirm via paint detection: ValidateRect -> invoke -> Sleep(50) -> GetUpdateRect
                    bool hollow = elHwnd != IntPtr.Zero && ConfirmHollow(elHwnd);
                    if (hollow)
                    {
                        NativeMethods.SetPropW(elHwnd, ActionApi.InvokeHollowProp, (IntPtr)1);
                        var clsB = new System.Text.StringBuilder(64);
                        NativeMethods.GetClassNameW(elHwnd, clsB, clsB.Capacity);
                        ActionApi.OnInvokeHollow?.Invoke(elHwnd, clsB.ToString());
                        Console.WriteLine($"[A11Y] invoke -- UIA Invoke X hollow confirmed (no paint) -> prop stamped, trying next tier");
                        _autoHealTiers?.Add($"UIA Invoke: hollow stub (invoke {invokeMs}ms ≈ null {nullMs}ms, no paint)");
                    }
                    else
                    {
                        Console.WriteLine($"[A11Y] invoke -- UIA Invoke v real (paint detected)");
                        RecordTierSuccess(elHwnd, el, "invoke", "UIA Invoke", KnowhowCategory.MinFocus);
                        return true;
                    }
                }
                else
                {
                    RecordTierSuccess(elHwnd, el, "invoke", "UIA Invoke", KnowhowCategory.MinFocus);
                    return true;
                }
            }
            else
            {
                // Flutter FAB / nested-label pattern: matched element (e.g. Text child) has no
                // InvokePattern, but ancestor (FloatingActionButton / Button) does.
                // Walk up UIA parent chain up to 5 levels.
                var ancestor = el;
                for (int lvl = 0; lvl < 5; lvl++)
                {
                    try { ancestor = ancestor.Parent; } catch { break; }
                    if (ancestor == null) break;
                    try { if (ancestor.Properties.NativeWindowHandle.ValueOrDefault != IntPtr.Zero) break; } catch { }
                    if (UiaLocator.TryInvoke(ancestor))
                    {
                        var aName = ancestor.Properties.Name.ValueOrDefault ?? "";
                        Console.WriteLine($"[A11Y] invoke -- UIA Invoke ancestor[+{lvl + 1}] '{aName}'");
                        RecordTierSuccess(elHwnd, el, "invoke", "UIA Invoke (ancestor)", KnowhowCategory.MinFocus);
                        return true;
                    }
                }
            }
        }

        // Tier 2: BM_CLICK (focusless)
        tier2:
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.PostMessageW(elHwnd, 0x00F5 /* BM_CLICK */, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("[A11Y] invoke -- Win32 BM_CLICK");
            _autoHealTiers?.Add("BM_CLICK: sent (unverified -- no response detection)");
            return true;
        }

        // Tier 2.5: UIA pattern fallback per ControlType standard.
        // Before physical click, try the UIA pattern the element's type expects.
        // Physical click is the last resort -- only for elements with no applicable pattern.
        try
        {
            var ct = el.Properties.ControlType.ValueOrDefault;
            // Toggle: CheckBox, ToggleButton
            if (ct is FlaUI.Core.Definitions.ControlType.CheckBox
                    or FlaUI.Core.Definitions.ControlType.Button)
            {
                var tog = el.Patterns.Toggle.PatternOrDefault;
                if (tog != null)
                {
                    tog.Toggle();
                    Console.WriteLine($"[A11Y] invoke -- UIA TogglePattern ({ct})");
                    RecordTierSuccess(elHwnd, el, "invoke", $"UIA TogglePattern ({ct})", KnowhowCategory.Focusless);
                    return true;
                }
            }
            // Select: RadioButton, ListItem, TabItem, TreeItem, DataItem
            if (ct is FlaUI.Core.Definitions.ControlType.RadioButton
                    or FlaUI.Core.Definitions.ControlType.ListItem
                    or FlaUI.Core.Definitions.ControlType.TabItem
                    or FlaUI.Core.Definitions.ControlType.TreeItem
                    or FlaUI.Core.Definitions.ControlType.DataItem)
            {
                var sel = el.Patterns.SelectionItem.PatternOrDefault;
                if (sel != null)
                {
                    sel.Select();
                    Console.WriteLine($"[A11Y] invoke -- UIA SelectionItemPattern ({ct})");
                    RecordTierSuccess(elHwnd, el, "invoke", $"UIA SelectionItem ({ct})", KnowhowCategory.Focusless);
                    return true;
                }
            }
            // Expand: ComboBox, MenuItem (with submenu), TreeItem
            if (ct is FlaUI.Core.Definitions.ControlType.ComboBox
                    or FlaUI.Core.Definitions.ControlType.MenuItem
                    or FlaUI.Core.Definitions.ControlType.TreeItem)
            {
                var exp = el.Patterns.ExpandCollapse.PatternOrDefault;
                if (exp != null)
                {
                    exp.Expand();
                    Console.WriteLine($"[A11Y] invoke -- UIA ExpandCollapsePattern ({ct})");
                    RecordTierSuccess(elHwnd, el, "invoke", $"UIA ExpandCollapse ({ct})", KnowhowCategory.Focusless);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] invoke -- UIA pattern fallback skipped: {ex.Message}");
        }

        // Tier 3: physical click (SendInput + click-through + focus restore) -- A11yClick handles all
        _autoHealTiers?.Add("Tier3: delegating to A11yClick (WM_LBUTTON -> physical)");
        return A11yClick(el, hwnd);
    }
}
