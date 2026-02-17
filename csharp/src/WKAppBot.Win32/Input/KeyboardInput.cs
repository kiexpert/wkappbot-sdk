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
    /// </summary>
    public static void TypeText(string text)
    {
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
    /// Press and release a single key by name.
    /// </summary>
    public static void PressKey(string keyName)
    {
        ushort vk = NameToVk(keyName);
        KeyDown(vk);
        Thread.Sleep(30);
        KeyUp(vk);
    }

    /// <summary>
    /// Press a hotkey combination (e.g., ["ctrl", "s"]).
    /// </summary>
    public static void Hotkey(IReadOnlyList<string> keys)
    {
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
    /// </summary>
    public static void KeyDown(ushort vk)
    {
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = 0;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
    }

    /// <summary>
    /// Release a key.
    /// </summary>
    public static void KeyUp(ushort vk)
    {
        var inputs = new INPUT[1];
        inputs[0].type = INPUT.INPUT_KEYBOARD;
        inputs[0].u.ki.wVk = vk;
        inputs[0].u.ki.wScan = (ushort)NativeMethods.MapVirtualKeyW(vk, 0);
        inputs[0].u.ki.dwFlags = KeyFlags.KEYEVENTF_KEYUP;
        NativeMethods.SendInput(1, inputs, Marshal.SizeOf<INPUT>());
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
