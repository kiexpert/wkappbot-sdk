"""Low-level keyboard input using ctypes SendInput."""

import ctypes
import ctypes.wintypes
import time

user32 = ctypes.windll.user32

INPUT_KEYBOARD = 1
KEYEVENTF_KEYUP = 0x0002
KEYEVENTF_UNICODE = 0x0004

# Virtual key code mapping
VK_MAP = {
    # Modifier keys
    "ctrl": 0x11, "control": 0x11,
    "alt": 0x12, "menu": 0x12,
    "shift": 0x10,
    "win": 0x5B, "lwin": 0x5B, "rwin": 0x5C,

    # Navigation
    "enter": 0x0D, "return": 0x0D,
    "tab": 0x09,
    "escape": 0x1B, "esc": 0x1B,
    "backspace": 0x08, "back": 0x08,
    "delete": 0x2E, "del": 0x2E,
    "insert": 0x2D, "ins": 0x2D,
    "space": 0x20,

    # Arrow keys
    "up": 0x26, "down": 0x28, "left": 0x25, "right": 0x27,
    "home": 0x24, "end": 0x23,
    "pageup": 0x21, "pgup": 0x21,
    "pagedown": 0x22, "pgdn": 0x22,

    # Function keys
    "f1": 0x70, "f2": 0x71, "f3": 0x72, "f4": 0x73,
    "f5": 0x74, "f6": 0x75, "f7": 0x76, "f8": 0x77,
    "f9": 0x78, "f10": 0x79, "f11": 0x7A, "f12": 0x7B,

    # Number keys
    "0": 0x30, "1": 0x31, "2": 0x32, "3": 0x33, "4": 0x34,
    "5": 0x35, "6": 0x36, "7": 0x37, "8": 0x38, "9": 0x39,

    # Letter keys
    "a": 0x41, "b": 0x42, "c": 0x43, "d": 0x44, "e": 0x45,
    "f": 0x46, "g": 0x47, "h": 0x48, "i": 0x49, "j": 0x4A,
    "k": 0x4B, "l": 0x4C, "m": 0x4D, "n": 0x4E, "o": 0x4F,
    "p": 0x50, "q": 0x51, "r": 0x52, "s": 0x53, "t": 0x54,
    "u": 0x55, "v": 0x56, "w": 0x57, "x": 0x58, "y": 0x59,
    "z": 0x5A,

    # Misc
    "capslock": 0x14, "numlock": 0x90, "scrolllock": 0x91,
    "printscreen": 0x2C, "prtsc": 0x2C,
    "pause": 0x13,

    # OEM keys
    "plus": 0xBB, "=": 0xBB,
    "minus": 0xBD, "-": 0xBD,
    "comma": 0xBC, ",": 0xBC,
    "period": 0xBE, ".": 0xBE,
    "semicolon": 0xBA, ";": 0xBA,
    "slash": 0xBF, "/": 0xBF,
    "backslash": 0xDC, "\\": 0xDC,
    "bracketleft": 0xDB, "[": 0xDB,
    "bracketright": 0xDD, "]": 0xDD,
    "quote": 0xDE, "'": 0xDE,
    "backtick": 0xC0, "`": 0xC0,
}


class KEYBDINPUT(ctypes.Structure):
    _fields_ = [
        ("wVk", ctypes.wintypes.WORD),
        ("wScan", ctypes.wintypes.WORD),
        ("dwFlags", ctypes.wintypes.DWORD),
        ("time", ctypes.wintypes.DWORD),
        ("dwExtraInfo", ctypes.POINTER(ctypes.c_ulong)),
    ]


class INPUT(ctypes.Structure):
    class _INPUT_UNION(ctypes.Union):
        _fields_ = [("ki", KEYBDINPUT)]

    _fields_ = [
        ("type", ctypes.wintypes.DWORD),
        ("union", _INPUT_UNION),
    ]


class Keyboard:
    """Keyboard control using SendInput API."""

    @staticmethod
    def _send_key_event(vk: int = 0, scan: int = 0, flags: int = 0):
        """Send a single key event."""
        inp = INPUT()
        inp.type = INPUT_KEYBOARD
        inp.union.ki.wVk = vk
        inp.union.ki.wScan = scan
        inp.union.ki.dwFlags = flags
        inp.union.ki.time = 0
        inp.union.ki.dwExtraInfo = ctypes.pointer(ctypes.c_ulong(0))
        user32.SendInput(1, ctypes.byref(inp), ctypes.sizeof(inp))

    @staticmethod
    def _send_unicode_char(char: str):
        """Send a unicode character using KEYEVENTF_UNICODE."""
        code = ord(char)
        # Key down
        Keyboard._send_key_event(vk=0, scan=code, flags=KEYEVENTF_UNICODE)
        # Key up
        Keyboard._send_key_event(vk=0, scan=code, flags=KEYEVENTF_UNICODE | KEYEVENTF_KEYUP)

    @staticmethod
    def key_down(key: str):
        """Press a key down (without releasing)."""
        vk = VK_MAP.get(key.lower())
        if vk is None:
            raise ValueError(f"Unknown key: {key}")
        Keyboard._send_key_event(vk=vk)

    @staticmethod
    def key_up(key: str):
        """Release a key."""
        vk = VK_MAP.get(key.lower())
        if vk is None:
            raise ValueError(f"Unknown key: {key}")
        Keyboard._send_key_event(vk=vk, flags=KEYEVENTF_KEYUP)

    @staticmethod
    def press_key(key: str):
        """Press and release a single key."""
        vk = VK_MAP.get(key.lower())
        if vk is None:
            raise ValueError(f"Unknown key: {key}")
        Keyboard._send_key_event(vk=vk)
        time.sleep(0.02)
        Keyboard._send_key_event(vk=vk, flags=KEYEVENTF_KEYUP)

    @staticmethod
    def hotkey(*keys: str):
        """Press a key combination (e.g., hotkey('ctrl', 'c'))."""
        vk_list = []
        for key in keys:
            vk = VK_MAP.get(key.lower())
            if vk is None:
                raise ValueError(f"Unknown key: {key}")
            vk_list.append(vk)

        # Press all keys down
        for vk in vk_list:
            Keyboard._send_key_event(vk=vk)
            time.sleep(0.02)

        # Release all keys in reverse order
        for vk in reversed(vk_list):
            Keyboard._send_key_event(vk=vk, flags=KEYEVENTF_KEYUP)
            time.sleep(0.02)

    @staticmethod
    def type_text(text: str, interval: float = 0.02):
        """Type text using unicode input. Supports any language (Korean, etc.)."""
        for char in text:
            if char in VK_MAP:
                Keyboard.press_key(char)
            else:
                Keyboard._send_unicode_char(char)
            time.sleep(interval)
