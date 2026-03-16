using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Toggle: UIA Toggle -> BM_CLICK fallback --
    static bool A11yToggle(AutomationElement el, IntPtr hwnd)
    {
        var before = UiaLocator.GetToggleState(el);
        if (before != null)
            Console.WriteLine($"[A11Y] toggle state before: {before}");

        if (UiaLocator.TryToggle(el))
        {
            var after = UiaLocator.GetToggleState(el);
            Console.WriteLine($"[A11Y] toggle — UIA Toggle (now: {after})");
            return true;
        }

        Console.WriteLine("[A11Y] toggle — UIA not supported, falling back to invoke");
        return A11yInvoke(el, hwnd);
    }

    // -- Expand/Collapse: UIA ExpandCollapse --
    static bool A11yExpand(AutomationElement el)
    {
        if (UiaLocator.TryExpand(el))
        {
            Console.WriteLine("[A11Y] expand — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] expand — not supported on this element");
        return false;
    }

    static bool A11yCollapse(AutomationElement el)
    {
        if (UiaLocator.TryCollapse(el))
        {
            Console.WriteLine("[A11Y] collapse — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] collapse — not supported on this element");
        return false;
    }
}
