using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // Terminal window classes that use ConPTY — WM_CHAR doesn't reach stdin, need SendInput
    static readonly HashSet<string> TerminalClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "CASCADIA_HOSTING_WINDOW_CLASS", // Windows Terminal
        "ConsoleWindowClass",            // Classic conhost.exe
        "PseudoConsoleWindow",           // ConPTY
        "VirtualConsoleClass",           // misc
    };

    // -- Type: UIA Value -> WM_CHAR -> LegacyIA SetValue --
    static bool A11yType(AutomationElement el, IntPtr hwnd, string text)
    {
        // Check if target is a terminal window — WM_CHAR bypasses ConPTY stdin
        var winClass = WindowFinder.GetClassName(hwnd);
        bool isTerminal = TerminalClasses.Contains(winClass);

        if (!isTerminal)
        {
            try
            {
                var vp = el.Patterns.Value;
                if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
                {
                    vp.Pattern.SetValue(text);
                    Console.WriteLine($"[A11Y] type — UIA Value.SetValue ({text.Length} chars)");
                    return true;
                }
            }
            catch { }

            var elHwnd = GetElementHwnd(el);
            if (elHwnd != IntPtr.Zero)
            {
                foreach (char c in text)
                    NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
                Console.WriteLine($"[A11Y] type — Win32 WM_CHAR ({text.Length} chars)");
                return true;
            }
        }
        else
        {
            Console.WriteLine($"[A11Y] type — terminal ({winClass}): skipping WM_CHAR (ConPTY), using SendInput");
        }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine($"[A11Y] type — LegacyIA SetValue ({text.Length} chars)");
                return true;
            }
        }
        catch { }

        // Tier 4: SendKeys keystroke fallback (requires focus)
        try
        {
            Console.WriteLine("[A11Y] type — focusless failed, falling back to SendKeys (focus required)");
            NativeMethods.SetForegroundWindow(hwnd);
            Thread.Sleep(100);
            // Pass hwnd for per-token mid-input focus check+restore
            WKAppBot.Win32.Input.KeyboardInput.SendKeys(text, hwnd);
            Console.WriteLine($"[A11Y] type — SendKeys keystroke ({text.Length} chars)");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] type — SendKeys failed: {ex.Message}");
        }

        Console.Error.WriteLine("[A11Y] type — no input method available");
        return false;
    }

    // -- SetValue: UIA Value -> WM_SETTEXT -> LegacyIA --
    static bool A11ySetValue(AutomationElement el, IntPtr hwnd, string text)
    {
        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — UIA Value.SetValue");
                return true;
            }
        }
        catch { }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(elHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
            Console.WriteLine("[A11Y] set-value — Win32 WM_SETTEXT");
            return true;
        }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — LegacyIA SetValue");
                return true;
            }
        }
        catch { }

        Console.Error.WriteLine("[A11Y] set-value — no method available");
        return false;
    }

    // -- SetRange: UIA RangeValue --
    static bool A11ySetRange(AutomationElement el, double value)
    {
        var info = UiaLocator.GetRangeValueInfo(el);
        if (info != null)
            Console.WriteLine($"[A11Y] range: {info.Minimum}..{info.Maximum} (current={info.Value}, step={info.SmallChange})");

        if (UiaLocator.TrySetRangeValue(el, value))
        {
            Console.WriteLine($"[A11Y] set-range — UIA RangeValue = {value}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] set-range — RangeValue not supported on this element");
        return false;
    }
}
