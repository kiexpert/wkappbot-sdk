"""Window management - find, focus, resize, get info."""

import ctypes
import ctypes.wintypes
import time
from typing import Optional, List

user32 = ctypes.windll.user32

SW_RESTORE = 9
SW_SHOW = 5
SW_MINIMIZE = 6
SW_MAXIMIZE = 3
GW_HWNDNEXT = 2


class WindowInfo:
    """Basic window information."""
    def __init__(self, hwnd: int, title: str, class_name: str, rect: tuple):
        self.hwnd = hwnd
        self.title = title
        self.class_name = class_name
        self.rect = rect  # (left, top, right, bottom)

    @property
    def width(self):
        return self.rect[2] - self.rect[0]

    @property
    def height(self):
        return self.rect[3] - self.rect[1]

    def __repr__(self):
        return f"WindowInfo(hwnd={self.hwnd}, title='{self.title}', size={self.width}x{self.height})"


class WindowManager:
    """Window management utilities using Win32 API."""

    @staticmethod
    def find_window(title_contains: str) -> Optional[int]:
        """Find window handle by title substring match."""
        result = []

        def enum_callback(hwnd, lparam):
            if user32.IsWindowVisible(hwnd):
                length = user32.GetWindowTextLengthW(hwnd)
                if length > 0:
                    buf = ctypes.create_unicode_buffer(length + 1)
                    user32.GetWindowTextW(hwnd, buf, length + 1)
                    if title_contains.lower() in buf.value.lower():
                        result.append(hwnd)
            return True

        WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, ctypes.wintypes.HWND, ctypes.wintypes.LPARAM)
        user32.EnumWindows(WNDENUMPROC(enum_callback), 0)

        return result[0] if result else None

    @staticmethod
    def find_windows(title_contains: str) -> List[int]:
        """Find all matching window handles."""
        result = []

        def enum_callback(hwnd, lparam):
            if user32.IsWindowVisible(hwnd):
                length = user32.GetWindowTextLengthW(hwnd)
                if length > 0:
                    buf = ctypes.create_unicode_buffer(length + 1)
                    user32.GetWindowTextW(hwnd, buf, length + 1)
                    if title_contains.lower() in buf.value.lower():
                        result.append(hwnd)
            return True

        WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.c_bool, ctypes.wintypes.HWND, ctypes.wintypes.LPARAM)
        user32.EnumWindows(WNDENUMPROC(enum_callback), 0)
        return result

    @staticmethod
    def get_window_info(hwnd: int) -> WindowInfo:
        """Get window information."""
        length = user32.GetWindowTextLengthW(hwnd)
        title_buf = ctypes.create_unicode_buffer(length + 1)
        user32.GetWindowTextW(hwnd, title_buf, length + 1)

        class_buf = ctypes.create_unicode_buffer(256)
        user32.GetClassNameW(hwnd, class_buf, 256)

        rect = ctypes.wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))

        return WindowInfo(
            hwnd=hwnd,
            title=title_buf.value,
            class_name=class_buf.value,
            rect=(rect.left, rect.top, rect.right, rect.bottom),
        )

    @staticmethod
    def get_window_rect(hwnd: int) -> tuple:
        """Get window rectangle (left, top, right, bottom)."""
        rect = ctypes.wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))
        return (rect.left, rect.top, rect.right, rect.bottom)

    @staticmethod
    def focus_window(hwnd: int):
        """Bring window to foreground and focus it."""
        # Restore if minimized
        if user32.IsIconic(hwnd):
            user32.ShowWindow(hwnd, SW_RESTORE)
        else:
            user32.ShowWindow(hwnd, SW_SHOW)

        user32.SetForegroundWindow(hwnd)
        time.sleep(0.1)

    @staticmethod
    def resize_window(hwnd: int, width: int, height: int):
        """Resize window while keeping its position."""
        rect = ctypes.wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))
        user32.MoveWindow(hwnd, rect.left, rect.top, width, height, True)

    @staticmethod
    def move_window(hwnd: int, x: int, y: int):
        """Move window to new position while keeping its size."""
        rect = ctypes.wintypes.RECT()
        user32.GetWindowRect(hwnd, ctypes.byref(rect))
        width = rect.right - rect.left
        height = rect.bottom - rect.top
        user32.MoveWindow(hwnd, x, y, width, height, True)

    @staticmethod
    def get_foreground_window() -> int:
        """Get the currently active foreground window handle."""
        return user32.GetForegroundWindow()

    @staticmethod
    def wait_for_window(title_contains: str, timeout: float = 10.0) -> int:
        """Wait for a window to appear. Returns hwnd."""
        deadline = time.time() + timeout
        while time.time() < deadline:
            hwnd = WindowManager.find_window(title_contains)
            if hwnd:
                return hwnd
            time.sleep(0.5)
        raise TimeoutError(f"Window '{title_contains}' not found within {timeout}s")
