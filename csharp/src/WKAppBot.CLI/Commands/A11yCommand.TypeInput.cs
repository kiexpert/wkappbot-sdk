// A11yCommand.TypeInput.cs -- Shared UIA type+submit helper.
//
// Input mode (auto-selected by caller or text):
//   slash command (starts with "/") → char-by-char SendInput, 60ms/char + 900ms menu wait
//   short text (≤100 chars)        → char-by-char SendInput, 60ms/char
//   long text (>100 chars)         → clipboard paste (fires DOM events for any length)
//
// Enter (when submit=true): PostMessage WM_KEYDOWN+WM_KEYUP to Chrome renderer (focusless real).
// Falls back to SendInput PressKey("enter") if renderer HWND not found.
//
// Focus: BoundingRectangle click (direct, reliable for Chromium-hosted UIA panels).

using WKAppBot.Win32.Native;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    const int CharByCharThreshold = 100;

    /// <summary>
    /// Type text into a UIA element and optionally submit.
    /// Slash commands use char-by-char (triggers autocomplete menu).
    /// Long text uses clipboard paste (fires DOM events, no length limit).
    /// Enter = focusless PostMessage to Chrome renderer so focus never needs to stay stolen.
    /// </summary>
    static bool A11yTypeAndSubmit(
        FlaUI.Core.AutomationElements.AutomationElement el,
        IntPtr hwnd,
        string text,
        bool submit = true,
        bool clearFirst = true)
    {
        InputReadiness.ReadinessCalled = true;
        InputReadiness.SetApprovalGrace();
        try
        {
            // Focus: click element center via BoundingRectangle.
            // el.Focus() silently fails for Chromium-hosted UIA elements.
            NativeMethods.SmartSetForegroundWindow(hwnd);
            Thread.Sleep(150);
            var rect = el.BoundingRectangle;
            if (rect.Width > 0 && rect.Height > 0)
            {
                MouseInput.Click(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                Thread.Sleep(250);
            }
            else
            {
                NativeMethods.GetWindowRect(hwnd, out var wr);
                MouseInput.Click(wr.Left + wr.Width / 2, wr.Top + 15);
                Thread.Sleep(200);
                KeyboardInput.PressKey("escape");
                Thread.Sleep(150);
            }

            if (clearFirst)
            {
                KeyboardInput.Hotkey(new[] { "ctrl", "a" });
                Thread.Sleep(50);
                KeyboardInput.PressKey("delete");
                Thread.Sleep(100);
            }

            // -- Input --
            bool isSlash = text.StartsWith("/", StringComparison.Ordinal);
            if (isSlash || text.Length <= CharByCharThreshold)
            {
                // Char-by-char: each keystroke triggers autocomplete / UI reactions
                var typeCtx = new KeyboardInput.TypeInputContext { UserInputWait = new UserInputWaitAdapter() };
                foreach (var ch in text)
                {
                    KeyboardInput.TypeText(ch.ToString(), ctx: typeCtx);
                    Thread.Sleep(60);
                }
                Console.Error.WriteLine($"[A11Y-TYPE] char-by-char '{text}' ({text.Length} ch)");
                if (isSlash) Thread.Sleep(900); // wait for slash command autocomplete menu
                else Thread.Sleep(100);
            }
            else
            {
                // Clipboard paste: fires DOM input/change events in Chromium, any length
                using var clipGuard = new ClaudePromptHelper.ClipboardGuard();
                ClaudePromptHelper.SetClipboardTextPublic(text);
                Thread.Sleep(50);
                KeyboardInput.Hotkey(new[] { "ctrl", "v" });
                Thread.Sleep(300);
                Console.Error.WriteLine($"[A11Y-TYPE] clipboard paste {text.Length} chars");
            }

            // -- Submit --
            if (submit)
            {
                Thread.Sleep(100);
                var rendHwnd = FindChromeRendererHwnd(hwnd);
                if (rendHwnd != IntPtr.Zero)
                {
                    PostRendererEnter(rendHwnd);
                    Console.Error.WriteLine("[A11Y-TYPE] renderer Enter (focusless)");
                }
                else
                {
                    KeyboardInput.PressKey("enter"); // SendInput fallback
                    Console.Error.WriteLine("[A11Y-TYPE] SendInput Enter (fallback)");
                }
                Thread.Sleep(300);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y-TYPE] {ex.Message}");
            return false;
        }
    }

    /// <summary>Find Chrome_RenderWidgetHostHWND inside a VS Code / Electron window.</summary>
    static IntPtr FindChromeRendererHwnd(IntPtr hwnd)
    {
        var r = NativeMethods.FindWindowExW(hwnd, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null);
        if (r != IntPtr.Zero) return r;
        var ww = NativeMethods.FindWindowExW(hwnd, IntPtr.Zero, "Chrome_WidgetWin_1", null);
        return ww != IntPtr.Zero
            ? NativeMethods.FindWindowExW(ww, IntPtr.Zero, "Chrome_RenderWidgetHostHWND", null)
            : IntPtr.Zero;
    }

    /// <summary>Send a real WM_KEYDOWN+WM_KEYUP Enter to a renderer HWND (focusless).</summary>
    static void PostRendererEnter(IntPtr rendHwnd)
    {
        const int WM_KEYDOWN = 0x0100, WM_KEYUP = 0x0101, VK_RETURN = 0x0D;
        uint sc = NativeMethods.MapVirtualKeyW(VK_RETURN, 0);
        var lpDown = (IntPtr)((sc << 16) | 1);
        var lpUp   = (IntPtr)((sc << 16) | 1 | (1 << 30) | (1 << 31));
        NativeMethods.PostMessageW(rendHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, lpDown);
        Thread.Sleep(20);
        NativeMethods.PostMessageW(rendHwnd, WM_KEYUP, (IntPtr)VK_RETURN, lpUp);
    }
}
