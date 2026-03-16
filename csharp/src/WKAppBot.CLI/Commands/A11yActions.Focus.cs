using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // -- Focus: UIA SetFocus -> Win32 SetFocus fallback --
    static bool A11yFocusElement(AutomationElement el, IntPtr hwnd)
    {
        // Tier 1: UIA IUIAutomationElement::SetFocus (focusless — no SetForegroundWindow needed)
        try
        {
            el.Focus();
            var name = el.Properties.Name.ValueOrDefault ?? "";
            var type = "?";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            Console.WriteLine($"[A11Y] focus — UIA SetFocus → [{type}] \"{name}\"");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[A11Y] focus — UIA SetFocus failed: {ex.Message}, trying Win32");
        }

        // Tier 2: Win32 SetFocus on element hwnd
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SetFocus(elHwnd);
            Console.WriteLine($"[A11Y] focus — Win32 SetFocus 0x{elHwnd:X8}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] focus — no element hwnd, cannot set focus");
        return false;
    }

    // -- Select: UIA SelectionItem --
    static bool A11ySelectItem(AutomationElement el)
    {
        if (UiaLocator.TrySelect(el))
        {
            Console.WriteLine("[A11Y] select — UIA SelectionItem");
            return true;
        }
        Console.WriteLine("[A11Y] select — UIA not supported, falling back to invoke");
        return UiaLocator.TryInvoke(el);
    }
}
