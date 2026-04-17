using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Scroll: UIA Scroll -> WM_VSCROLL/WM_HSCROLL fallback --
    static bool A11yScrollAction(AutomationElement el, IntPtr hwnd, string direction, string amount)
    {
        var hAmount = ScrollAmount.NoAmount;
        var vAmount = ScrollAmount.NoAmount;

        var scrollAmt = amount == "large" ? ScrollAmount.LargeIncrement : ScrollAmount.SmallIncrement;
        var scrollAmtNeg = amount == "large" ? ScrollAmount.LargeDecrement : ScrollAmount.SmallDecrement;

        switch (direction)
        {
            case "down": vAmount = scrollAmt; break;
            case "up": vAmount = scrollAmtNeg; break;
            case "right": hAmount = scrollAmt; break;
            case "left": hAmount = scrollAmtNeg; break;
            default: return Error($"Invalid scroll direction: {direction}") != 0 ? false : false;
        }

        if (UiaLocator.TryScroll(el, hAmount, vAmount))
        {
            Console.Error.WriteLine($"[A11Y] scroll {direction} ({amount}) -- UIA Scroll");
            return true;
        }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd == IntPtr.Zero) elHwnd = hwnd;

        uint msg = (direction == "left" || direction == "right") ? 0x0114u : 0x0115u;
        int wParam = (direction == "down" || direction == "right") ? 1 : 0;
        if (amount == "large")
            wParam = (direction == "down" || direction == "right") ? 3 : 2;

        NativeMethods.PostMessageW(elHwnd, msg, (IntPtr)wParam, IntPtr.Zero);
        Console.Error.WriteLine($"[A11Y] scroll {direction} ({amount}) -- Win32 WM_{(msg == 0x0114u ? "H" : "V")}SCROLL");
        return true;
    }
}
