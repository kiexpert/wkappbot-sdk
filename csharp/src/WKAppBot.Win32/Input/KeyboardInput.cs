using System.Runtime.InteropServices;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Input;

/// <summary>
/// Physical keyboard input via SendInput.
/// Supports VK codes, scan codes, and Unicode text.
/// </summary>
public static class KeyboardInput
{
    /// <summary>
    /// Type a text string using Unicode input events.
    /// Works with any language (Korean, etc.) without VK mapping.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void TypeText(string text)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard TypeText)");
        foreach (char ch in text)
        {
            var inputs = new INPUT[2];

            // Key down
            inputs[0].type = INPUT.INPUT_KEYBOARD;
            inputs[0].u.ki.wVk = 0;
            inputs[0].u.ki.wScan = ch;
            inputs[0].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE;

            // Key up
            inputs[1].type = INPUT.INPUT_KEYBOARD;
            inputs[1].u.ki.wVk = 0;
            inputs[1].u.ki.wScan = ch;
            inputs[1].u.ki.dwFlags = KeyFlags.KEYEVENTF_UNICODE | KeyFlags.KEYEVENTF_KEYUP;

            NativeMethods.SendInput(2, inputs, Marshal.SizeOf<INPUT>());
            Thread.Sleep(10); // small delay for stability
        }
    }

    /// <summary>
    /// Type text by posting WM_CHAR messages to target window's message queue.
    /// Cross-process capable — messages go directly to the target window's queue,
    /// bypassing UIPI issues that affect SendInput.
    ///
    /// Best for: MFC CMaskEdit, owner-drawn inputs, admin-to-user process input.
    /// Gemini recommendation: WM_CHAR + focus + 10-30ms delay per character.
    /// </summary>
    public static bool TypeTextViaWmChar(IntPtr hWnd, string text, int charDelayMs = 20)
    {
        if (hWnd == IntPtr.Zero || string.IsNullOrEmpty(text))
            return false;

        foreach (char ch in text)
        {
            // WM_CHAR lParam: repeat=1, scan=0, extended=0, context=0, previous=0, transition=0
            NativeMethods.PostMessageW(hWnd, NativeMethods.WM_CHAR, (IntPtr)ch, IntPtr.Zero);
            if (charDelayMs > 0)
                Thread.Sleep(charDelayMs);
        }
        return true;
    }

    /// <summary>
    /// Set text on an Edit control via WM_SETTEXT.
    /// Replaces entire content. Works cross-process without SendInput.
    ///
    /// Best for: Standard Edit controls, RichEdit, some MFC derivatives.
    /// Not suitable for owner-drawn controls that don't process WM_SETTEXT.
    /// </summary>
    public static bool TypeTextViaWmSetText(IntPtr hWnd, string text)
    {
        if (hWnd == IntPtr.Zero) return false;

        // WM_SETTEXT = 0x000C, uses string overload (marshalled by P/Invoke)
        NativeMethods.SendMessageW(hWnd, 0x000C, IntPtr.Zero, text);
        return true;
    }

    /// <summary>
    /// Append text to an Edit control via EM_REPLACESEL.
    /// Inserts at current cursor position without replacing entire content.
    ///
    /// Best for: Inserting text at cursor in Edit/RichEdit controls.
    /// </summary>
    public static bool TypeTextViaEmReplaceSel(IntPtr hWnd, string text)
    {
        if (hWnd == IntPtr.Zero) return false;

        // EM_REPLACESEL = 0x00C2, wParam=1 (can undo), string overload
        NativeMethods.SendMessageW(hWnd, 0x00C2, (IntPtr)1, text);
        return true;
    }

    /// <summary>
    /// Press and release a single key by name.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void PressKey(string keyName)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard PressKey)");
        ushort vk = NameToVk(keyName);
        KeyDown(vk);
        Thread.Sleep(30);
        KeyUp(vk);
    }

    /// <summary>
    /// Press a hotkey combination (e.g., ["ctrl", "s"]).
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void Hotkey(IReadOnlyList<string> keys)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard Hotkey)");
        // Press modifiers first, then the final key
        var vks = keys.Select(NameToVk).ToList();

        // Press all
        foreach (var vk in vks)
        {
            KeyDown(vk);
            Thread.Sleep(20);
        }

        // Release in reverse order
        Thread.Sleep(30);
        for (int i = vks.Count - 1; i >= 0; i--)
        {
            KeyUp(vks[i]);
            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// Press a key down (no release).
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void KeyDown(ushort vk)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard KeyDown)");
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = 0;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Release a key.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void KeyUp(ushort vk)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard KeyUp)");
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = KeyFlags.KEYEVENTF_KEYUP;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Send a sequence of key actions with +/- modifier notation.
    /// Tokens (space-separated):
    ///   +Shift   → KeyDown (push to held stack)
    ///   -Shift   → KeyUp (pop from stack)
    ///   F5       → PressKey (down+up)
    ///   hello    → per-char keystroke via VkKeyScanW (auto Shift wrap)
    /// Unreleased modifiers auto-released in LIFO order at end.
    /// BLOCKED by FocuslessGuard (SendInput).
    /// </summary>
    public static void SendKeys(string sequence)
    {
        FocuslessGuard.AssertAllowed("SendInput(keyboard SendKeys)");
        var heldStack = new Stack<ushort>();
        var tokens = sequence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (token.StartsWith('+') && token.Length > 1)
            {
                // +Key → hold down
                var vk = NameToVk(token[1..]);
                KeyDown(vk);
                heldStack.Push(vk);
                Thread.Sleep(20);
            }
            else if (token.StartsWith('-') && token.Length > 1)
            {
                // -Key → release
                var vk = NameToVk(token[1..]);
                KeyUp(vk);
                // Remove from stack if present
                var temp = new Stack<ushort>();
                while (heldStack.Count > 0)
                {
                    var h = heldStack.Pop();
                    if (h != vk) temp.Push(h);
                }
                while (temp.Count > 0) heldStack.Push(temp.Pop());
                Thread.Sleep(20);
            }
            else
            {
                // Try as known key name first (F5, Enter, etc.)
                try
                {
                    var vk = NameToVk(token);
                    KeyDown(vk);
                    Thread.Sleep(30);
                    KeyUp(vk);
                    Thread.Sleep(20);
                }
                catch (ArgumentException)
                {
                    // Not a known key → type as character sequence via VkKeyScanW
                    foreach (char ch in token)
                    {
                        short vkScan = NativeMethods.VkKeyScanW(ch);
                        if (vkScan == -1) continue; // unmappable char
                        byte vk = (byte)(vkScan & 0xFF);
                        bool needShift = (vkScan >> 8 & 1) != 0;
                        bool needCtrl = (vkScan >> 8 & 2) != 0;
                        bool needAlt = (vkScan >> 8 & 4) != 0;

                        if (needCtrl) KeyDown(0x11);
                        if (needAlt) KeyDown(0x12);
                        if (needShift) KeyDown(0x10);
                        KeyDown(vk); Thread.Sleep(20); KeyUp(vk);
                        if (needShift) KeyUp(0x10);
                        if (needAlt) KeyUp(0x12);
                        if (needCtrl) KeyUp(0x11);
                        Thread.Sleep(20);
                    }
                }
            }
        }

        // Release any remaining held keys (LIFO)
        while (heldStack.Count > 0)
        {
            KeyUp(heldStack.Pop());
            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// Map key name to virtual key code.
    /// </summary>
    public static ushort NameToVk(string name) => name.ToLowerInvariant() switch
    {
        // Modifiers
        "ctrl" or "control" => 0x11,    // VK_CONTROL
        "alt" => 0x12,                  // VK_MENU
        "shift" => 0x10,               // VK_SHIFT
        "win" or "windows" => 0x5B,    // VK_LWIN

        // Navigation
        "enter" or "return" => 0x0D,
        "tab" => 0x09,
        "escape" or "esc" => 0x1B,
        "space" => 0x20,
        "backspace" or "back" => 0x08,
        "delete" or "del" => 0x2E,
        "insert" or "ins" => 0x2D,
        "home" => 0x24,
        "end" => 0x23,
        "pageup" or "pgup" => 0x21,
        "pagedown" or "pgdn" => 0x22,

        // Arrows
        "up" => 0x26,
        "down" => 0x28,
        "left" => 0x25,
        "right" => 0x27,

        // Function keys
        "f1" => 0x70, "f2" => 0x71, "f3" => 0x72, "f4" => 0x73,
        "f5" => 0x74, "f6" => 0x75, "f7" => 0x76, "f8" => 0x77,
        "f9" => 0x78, "f10" => 0x79, "f11" => 0x7A, "f12" => 0x7B,

        // Letters (A-Z = 0x41-0x5A)
        var s when s.Length == 1 && char.IsLetter(s[0])
            => (ushort)(char.ToUpper(s[0])),

        // Numbers (0-9 = 0x30-0x39)
        var s when s.Length == 1 && char.IsDigit(s[0])
            => (ushort)s[0],

        // Misc
        "+" or "plus" => 0xBB,
        "-" or "minus" => 0xBD,
        "." or "period" => 0xBE,
        "," or "comma" => 0xBC,

        _ => throw new ArgumentException($"Unknown key name: '{name}'")
    };
}
