"""Low-level mouse input using ctypes SendInput for reliable cross-app control."""

import ctypes
import ctypes.wintypes
import time

user32 = ctypes.windll.user32

# SendInput constants
INPUT_MOUSE = 0
MOUSEEVENTF_MOVE = 0x0001
MOUSEEVENTF_LEFTDOWN = 0x0002
MOUSEEVENTF_LEFTUP = 0x0004
MOUSEEVENTF_RIGHTDOWN = 0x0008
MOUSEEVENTF_RIGHTUP = 0x0010
MOUSEEVENTF_MIDDLEDOWN = 0x0020
MOUSEEVENTF_MIDDLEUP = 0x0040
MOUSEEVENTF_WHEEL = 0x0800
MOUSEEVENTF_ABSOLUTE = 0x8000

SM_CXSCREEN = 0
SM_CYSCREEN = 1

WHEEL_DELTA = 120


# SendInput structures
class MOUSEINPUT(ctypes.Structure):
    _fields_ = [
        ("dx", ctypes.wintypes.LONG),
        ("dy", ctypes.wintypes.LONG),
        ("mouseData", ctypes.wintypes.DWORD),
        ("dwFlags", ctypes.wintypes.DWORD),
        ("time", ctypes.wintypes.DWORD),
        ("dwExtraInfo", ctypes.POINTER(ctypes.c_ulong)),
    ]


class INPUT(ctypes.Structure):
    class _INPUT_UNION(ctypes.Union):
        _fields_ = [("mi", MOUSEINPUT)]

    _fields_ = [
        ("type", ctypes.wintypes.DWORD),
        ("union", _INPUT_UNION),
    ]


class Mouse:
    """Mouse control using SendInput API for reliable input across all apps."""

    @staticmethod
    def _screen_size():
        w = user32.GetSystemMetrics(SM_CXSCREEN)
        h = user32.GetSystemMetrics(SM_CYSCREEN)
        return w, h

    @staticmethod
    def _to_normalized(x: int, y: int) -> tuple:
        """Convert pixel coordinates to normalized (0-65535) coordinates."""
        sw, sh = Mouse._screen_size()
        nx = int(x * 65535 / (sw - 1))
        ny = int(y * 65535 / (sh - 1))
        return nx, ny

    @staticmethod
    def _send_input(flags: int, dx: int = 0, dy: int = 0, mouse_data: int = 0):
        """Send a mouse input event."""
        inp = INPUT()
        inp.type = INPUT_MOUSE
        inp.union.mi.dx = dx
        inp.union.mi.dy = dy
        inp.union.mi.mouseData = mouse_data
        inp.union.mi.dwFlags = flags
        inp.union.mi.time = 0
        inp.union.mi.dwExtraInfo = ctypes.pointer(ctypes.c_ulong(0))
        ctypes.windll.user32.SendInput(1, ctypes.byref(inp), ctypes.sizeof(inp))

    @staticmethod
    def move(x: int, y: int):
        """Move cursor to absolute screen position."""
        nx, ny = Mouse._to_normalized(x, y)
        Mouse._send_input(
            MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE,
            dx=nx, dy=ny
        )

    @staticmethod
    def click(x: int, y: int, button: str = "left"):
        """Move to position and click."""
        Mouse.move(x, y)
        time.sleep(0.05)

        if button == "left":
            Mouse._send_input(MOUSEEVENTF_LEFTDOWN)
            time.sleep(0.02)
            Mouse._send_input(MOUSEEVENTF_LEFTUP)
        elif button == "right":
            Mouse._send_input(MOUSEEVENTF_RIGHTDOWN)
            time.sleep(0.02)
            Mouse._send_input(MOUSEEVENTF_RIGHTUP)
        elif button == "middle":
            Mouse._send_input(MOUSEEVENTF_MIDDLEDOWN)
            time.sleep(0.02)
            Mouse._send_input(MOUSEEVENTF_MIDDLEUP)

    @staticmethod
    def double_click(x: int, y: int):
        """Double click at position."""
        Mouse.click(x, y)
        time.sleep(0.1)
        Mouse.click(x, y)

    @staticmethod
    def right_click(x: int, y: int):
        """Right click at position."""
        Mouse.click(x, y, button="right")

    @staticmethod
    def scroll(x: int, y: int, amount: int = 3, direction: str = "down"):
        """Scroll wheel at position. amount is number of 'clicks'."""
        Mouse.move(x, y)
        time.sleep(0.05)

        delta = amount * WHEEL_DELTA
        if direction == "down":
            delta = -delta

        Mouse._send_input(MOUSEEVENTF_WHEEL, mouse_data=delta)

    @staticmethod
    def drag(from_x: int, from_y: int, to_x: int, to_y: int,
             duration: float = 0.5, steps: int = 20):
        """Drag from one position to another."""
        Mouse.move(from_x, from_y)
        time.sleep(0.05)
        Mouse._send_input(MOUSEEVENTF_LEFTDOWN)
        time.sleep(0.05)

        for i in range(1, steps + 1):
            t = i / steps
            cx = int(from_x + (to_x - from_x) * t)
            cy = int(from_y + (to_y - from_y) * t)
            Mouse.move(cx, cy)
            time.sleep(duration / steps)

        Mouse._send_input(MOUSEEVENTF_LEFTUP)
