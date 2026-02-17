"""Win32 API helper functions - DPI scaling, process info, etc."""

import ctypes
import ctypes.wintypes
import os

user32 = ctypes.windll.user32
shcore = None

try:
    shcore = ctypes.windll.shcore
except OSError:
    pass  # shcore not available on older Windows


def set_dpi_aware():
    """
    Set process as DPI-aware to get correct coordinates on high-DPI displays.
    Should be called once at startup.
    """
    try:
        # Windows 10 1607+
        ctypes.windll.user32.SetProcessDpiAwarenessContext(
            ctypes.c_void_p(-4)  # DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2
        )
    except (AttributeError, OSError):
        try:
            # Windows 8.1+
            if shcore:
                shcore.SetProcessDpiAwareness(2)  # PROCESS_PER_MONITOR_DPI_AWARE
        except (AttributeError, OSError):
            try:
                # Windows Vista+
                user32.SetProcessDPIAware()
            except (AttributeError, OSError):
                pass


def get_dpi_scale(hwnd: int = 0) -> float:
    """
    Get DPI scaling factor for a window.

    Returns:
        Scale factor (1.0 = 100%, 1.25 = 125%, 1.5 = 150%, etc.)
    """
    try:
        if hwnd and shcore:
            dpi = ctypes.c_uint()
            shcore.GetDpiForMonitor(
                user32.MonitorFromWindow(hwnd, 2),  # MONITOR_DEFAULTTONEAREST
                0,  # MDT_EFFECTIVE_DPI
                ctypes.byref(dpi),
                ctypes.byref(ctypes.c_uint()),
            )
            return dpi.value / 96.0
    except (AttributeError, OSError):
        pass

    try:
        # Fallback: get DPI from device context
        hdc = user32.GetDC(0)
        dpi = ctypes.windll.gdi32.GetDeviceCaps(hdc, 88)  # LOGPIXELSX
        user32.ReleaseDC(0, hdc)
        return dpi / 96.0
    except Exception:
        return 1.0


def get_screen_size() -> tuple:
    """Get screen size in pixels."""
    w = user32.GetSystemMetrics(0)  # SM_CXSCREEN
    h = user32.GetSystemMetrics(1)  # SM_CYSCREEN
    return (w, h)


def is_process_elevated() -> bool:
    """Check if the current process is running with admin privileges."""
    try:
        return ctypes.windll.shell32.IsUserAnAdmin() != 0
    except Exception:
        return False


# Set DPI awareness at import time
set_dpi_aware()
