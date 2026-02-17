"""Screenshot capture utilities."""

import os
from datetime import datetime
from typing import Optional

from PIL import ImageGrab

from appbot.config import log


class ScreenCapture:
    """Captures screenshots of windows or full screen."""

    def capture_full_screen(self, save_path: Optional[str] = None) -> str:
        """Capture full screen screenshot."""
        img = ImageGrab.grab()
        path = save_path or self._default_path("fullscreen")
        os.makedirs(os.path.dirname(path), exist_ok=True)
        img.save(path)
        log.debug(f"Screenshot saved: {path}")
        return path

    def capture_window(self, window, save_dir: str = "output/screenshots") -> str:
        """
        Capture screenshot of a specific pywinauto window.

        Args:
            window: pywinauto WindowSpecification
            save_dir: Directory to save screenshot

        Returns:
            Path to saved screenshot
        """
        try:
            rect = window.rectangle()
            img = ImageGrab.grab(bbox=(rect.left, rect.top, rect.right, rect.bottom))
        except Exception:
            # Fallback to full screen
            img = ImageGrab.grab()

        path = os.path.join(save_dir, self._timestamp_filename())
        os.makedirs(os.path.dirname(path), exist_ok=True)
        img.save(path)
        log.debug(f"Window screenshot saved: {path}")
        return path

    def capture_window_by_hwnd(self, hwnd: int, save_path: Optional[str] = None) -> str:
        """
        Capture screenshot of a window by its handle.

        Args:
            hwnd: Window handle
            save_path: Output file path (optional)

        Returns:
            Path to saved screenshot
        """
        import ctypes
        import ctypes.wintypes

        rect = ctypes.wintypes.RECT()
        ctypes.windll.user32.GetWindowRect(hwnd, ctypes.byref(rect))

        img = ImageGrab.grab(bbox=(rect.left, rect.top, rect.right, rect.bottom))
        path = save_path or self._default_path("window")
        os.makedirs(os.path.dirname(path), exist_ok=True)
        img.save(path)
        log.debug(f"Window screenshot saved: {path}")
        return path

    def capture_region(self, left: int, top: int, right: int, bottom: int,
                       save_path: Optional[str] = None) -> str:
        """Capture a specific screen region."""
        img = ImageGrab.grab(bbox=(left, top, right, bottom))
        path = save_path or self._default_path("region")
        os.makedirs(os.path.dirname(path), exist_ok=True)
        img.save(path)
        return path

    @staticmethod
    def _timestamp_filename() -> str:
        return f"capture_{datetime.now().strftime('%Y%m%d_%H%M%S_%f')}.png"

    @staticmethod
    def _default_path(prefix: str) -> str:
        return os.path.join(
            "output", "screenshots",
            f"{prefix}_{datetime.now().strftime('%Y%m%d_%H%M%S_%f')}.png"
        )
