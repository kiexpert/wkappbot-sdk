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
        // Tier 1: UIA IUIAutomationElement::SetFocus (focusless -- no SetForegroundWindow needed)
        try
        {
            el.Focus();
            var name = el.Properties.Name.ValueOrDefault ?? "";
            var type = "?";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            Console.Error.WriteLine($"[A11Y] focus -- UIA SetFocus -> [{type}] \"{name}\"");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] focus -- UIA SetFocus failed: {ex.Message}, trying Win32");
        }

        // Tier 2: Win32 SetFocus on element hwnd
        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SetFocus(elHwnd);
            Console.Error.WriteLine($"[A11Y] focus -- Win32 SetFocus 0x{elHwnd:X8}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] focus -- no element hwnd, cannot set focus");
        return false;
    }

    // -- Select: UIA SelectionItem -> Win32 CB_SELECTSTRING/LB_SELECTSTRING -> Invoke --
    static bool A11ySelectItem(AutomationElement el)
    {
        if (UiaLocator.TrySelect(el))
        {
            Console.WriteLine("[A11Y] select -- UIA SelectionItem");
            return true;
        }

        // Win32 폴백: MFC ComboBox/ListBox -- UIA SelectionItem 미지원 케이스
        // el = 아이템(Name만 있고 hwnd 없음) or 컨트롤 자체
        // CB_SELECTSTRING(0x014D) / LB_SELECTSTRING(0x018C): 텍스트 접두사로 항목 선택, focusless
        const uint CB_SELECTSTRING = 0x014D;
        const uint LB_SELECTSTRING = 0x018C;
        const int  CB_ERR          = -1;

        var itemName = el.Properties.Name.ValueOrDefault ?? "";
        if (!string.IsNullOrEmpty(itemName))
        {
            // 컨트롤 hwnd 확보: el 자신 또는 UIA 부모에서 찾기
            IntPtr ctrlHwnd = GetElementHwnd(el);
            string ctrlClass = ctrlHwnd != IntPtr.Zero ? WindowFinder.GetClassName(ctrlHwnd) : "";

            // el 자신이 ListItem -> 부모 ComboBox/ListBox hwnd 필요
            if (ctrlHwnd == IntPtr.Zero || (!ctrlClass.StartsWith("ComboBox", StringComparison.OrdinalIgnoreCase)
                                          && !ctrlClass.StartsWith("ListBox",  StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    var par = el.Parent;
                    if (par != null)
                    {
                        var parHwnd = GetElementHwnd(par);
                        if (parHwnd != IntPtr.Zero)
                        { ctrlHwnd = parHwnd; ctrlClass = WindowFinder.GetClassName(ctrlHwnd); }
                    }
                }
                catch { }
            }

            if (ctrlHwnd != IntPtr.Zero)
            {
                uint msg = 0;
                if (ctrlClass.StartsWith("ComboBox", StringComparison.OrdinalIgnoreCase)) msg = CB_SELECTSTRING;
                else if (ctrlClass.StartsWith("ListBox", StringComparison.OrdinalIgnoreCase)) msg = LB_SELECTSTRING;

                if (msg != 0)
                {
                    var res = (int)(long)NativeMethods.SendMessageW(ctrlHwnd, msg, (IntPtr)(-1), itemName);
                    if (res != CB_ERR)
                    {
                        Console.Error.WriteLine($"[A11Y] select -- Win32 {(msg == CB_SELECTSTRING ? "CB" : "LB")}_SELECTSTRING '{itemName}'");
                        return true;
                    }
                    Console.Error.WriteLine($"[A11Y] select -- Win32 {(msg == CB_SELECTSTRING ? "CB" : "LB")}_SELECTSTRING '{itemName}' not found");
                }
            }
        }

        Console.WriteLine("[A11Y] select -- UIA/Win32 not supported, falling back to invoke");
        return UiaLocator.TryInvoke(el);
    }
}
