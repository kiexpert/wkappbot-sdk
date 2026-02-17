"""Wrapper around pywinauto for UIA/Win32 automation."""

import time
from typing import Optional

from pywinauto import Application, Desktop
from pywinauto.findwindows import ElementNotFoundError
from pywinauto.timings import TimeoutError as PywinautoTimeout

from appbot.config import log


class UIAWrapper:
    """High-level wrapper for pywinauto connections."""

    def __init__(self, backend: str = "uia"):
        """
        Args:
            backend: "uia" (recommended, better for modern apps) or "win32" (legacy)
        """
        self.backend = backend
        self.app: Optional[Application] = None
        self.main_window = None

    def launch(self, command: str, args: list = None, working_dir: str = None,
               timeout: float = 10.0) -> 'UIAWrapper':
        """Launch an application and wait for it to be ready."""
        cmd = command
        if args:
            cmd = f"{command} {' '.join(args)}"

        log.debug(f"Launching: {cmd} (backend={self.backend})")
        self.app = Application(backend=self.backend).start(
            cmd,
            work_dir=working_dir,
            timeout=int(timeout),
        )
        return self

    def connect_by_title(self, title: str, timeout: float = 10.0) -> 'UIAWrapper':
        """Connect to an existing application by window title (substring or exact match)."""
        log.debug(f"Connecting to window: '{title}'")
        deadline = time.time() + timeout

        while time.time() < deadline:
            # Strategy 1: exact title match (works best for non-ASCII titles)
            for connect_kwargs in [
                {"title": title, "timeout": 2, "found_index": 0},
                {"title_re": f".*{title}.*", "timeout": 2, "found_index": 0},
                {"title_re": f".*{title}.*", "timeout": 2},
            ]:
                try:
                    self.app = Application(backend=self.backend).connect(**connect_kwargs)
                    return self
                except Exception:
                    continue
            time.sleep(0.5)

        raise TimeoutError(f"Could not find window containing '{title}' within {timeout}s")

    def connect_by_process(self, process_name: str, timeout: float = 10.0) -> 'UIAWrapper':
        """Connect to an existing application by process name."""
        log.debug(f"Connecting to process: {process_name}")
        deadline = time.time() + timeout

        while time.time() < deadline:
            try:
                self.app = Application(backend=self.backend).connect(
                    path=process_name,
                    timeout=2,
                )
                return self
            except (ElementNotFoundError, PywinautoTimeout):
                time.sleep(0.5)

        raise TimeoutError(f"Could not find process '{process_name}' within {timeout}s")

    def wait_for_window(self, title_contains: str = None, title_exact: str = None,
                        class_name: str = None, timeout: float = 10.0):
        """Wait for the main window to appear and be ready."""
        if not self.app:
            raise RuntimeError("Not connected to any application")

        criteria = {}
        if title_contains:
            criteria["title_re"] = f".*{title_contains}.*"
        elif title_exact:
            criteria["title"] = title_exact
        if class_name:
            criteria["class_name"] = class_name

        if criteria:
            self.main_window = self.app.window(**criteria)
        else:
            self.main_window = self.app.top_window()

        try:
            self.main_window.wait("exists visible ready", timeout=timeout)
            log.debug(f"Window ready: {self.main_window.window_text()}")
        except PywinautoTimeout:
            raise TimeoutError(f"Window not ready within {timeout}s")

        return self.main_window

    def get_window(self):
        """Get the current main window wrapper."""
        if self.main_window:
            return self.main_window
        if self.app:
            self.main_window = self.app.top_window()
            return self.main_window
        raise RuntimeError("Not connected to any application")

    def find_element(self, auto_id: str = None, name: str = None,
                     control_type: str = None, class_name: str = None,
                     timeout: float = 5.0):
        """Find a child element in the main window."""
        window = self.get_window()
        criteria = {}

        if auto_id:
            criteria["auto_id"] = auto_id
        if name:
            criteria["title"] = name
        if control_type:
            criteria["control_type"] = control_type
        if class_name:
            criteria["class_name"] = class_name

        if not criteria:
            raise ValueError("At least one search criterion required")

        element = window.child_window(**criteria)
        element.wait("exists visible", timeout=timeout)
        return element
