"""Action executor - performs UI actions on elements."""

import os
import time
import subprocess
from typing import Optional
from datetime import datetime

from appbot.locator.base import FoundElement
from appbot.core.context import RuntimeContext, StepResult
from appbot.core.condition import ConditionEvaluator, AssertionError
from appbot.input.mouse import Mouse
from appbot.input.keyboard import Keyboard
from appbot.config import log


class ActionError(Exception):
    """Raised when an action fails."""
    pass


class ActionExecutor:
    """Executes UI automation actions on found elements."""

    def execute(self, action_name: str, element: Optional[FoundElement],
                params: dict, context: RuntimeContext) -> StepResult:
        """
        Execute an action.

        Args:
            action_name: Action type (click, type_text, assert, etc.)
            element: Found UI element (may be None for some actions)
            params: Action parameters from scenario step
            context: RuntimeContext

        Returns:
            StepResult with pass/fail status
        """
        handler = getattr(self, f"_do_{action_name}", None)
        if not handler:
            raise ActionError(f"Unknown action: {action_name}")

        start = time.time()
        result = StepResult(action=action_name)

        try:
            handler(element, params, context)
            result.passed = True
        except (ActionError, AssertionError) as e:
            result.passed = False
            result.error_message = str(e)
        except Exception as e:
            result.passed = False
            result.error_message = f"Unexpected error: {e}"

        result.duration = time.time() - start

        if element:
            result.element_found = element.source

        return result

    def _do_click(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Click on an element."""
        if not element:
            raise ActionError("No element to click")

        button = params.get("button", "left") if params else "left"

        # Strategy 1: Use UIA invoke pattern (most reliable, no focus needed)
        if element.source == "accessibility" and element.uia_element:
            try:
                element.uia_element.invoke()
                return
            except Exception:
                pass
            # Strategy 2: Use UIA click_input
            try:
                element.uia_element.click_input()
                return
            except Exception:
                pass  # Fall through to coordinate click

        # Strategy 3: Coordinate click (for vision/coordinate elements)
        Mouse.click(element.center[0], element.center[1], button=button)

    def _do_double_click(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Double-click on an element."""
        if not element:
            raise ActionError("No element to double-click")

        if element.source == "accessibility" and element.uia_element:
            try:
                element.uia_element.double_click_input()
                return
            except Exception:
                pass

        Mouse.double_click(element.center[0], element.center[1])

    def _do_right_click(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Right-click on an element."""
        if not element:
            raise ActionError("No element to right-click")

        Mouse.right_click(element.center[0], element.center[1])

    def _do_type_text(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Type text using keyboard."""
        text = params.get("text", "") if params else ""
        text = context.resolve_variables(text)
        interval = params.get("interval", 0.02) if params else 0.02

        if not text:
            raise ActionError("No text to type")

        # If element provided, click it first to focus
        if element:
            self._do_click(element, {"button": "left"}, context)
            time.sleep(0.1)

        # Strategy 1: Use pywinauto type_keys via active window (reliable, no focus issues)
        if context.active_window:
            try:
                context.active_window.type_keys(text, with_spaces=True, pause=interval)
                return
            except Exception:
                pass

        # Strategy 2: Raw SendInput (needs window focus)
        Keyboard.type_text(text, interval=interval)

    def _do_press_key(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Press a single key."""
        key = params.get("key", "") if params else ""
        if not key:
            raise ActionError("No key specified")

        # Map key names to pywinauto key codes
        pywinauto_keys = {
            "escape": "{ESC}", "esc": "{ESC}",
            "enter": "{ENTER}", "return": "{ENTER}",
            "tab": "{TAB}", "backspace": "{BACKSPACE}",
            "delete": "{DELETE}", "del": "{DELETE}",
            "space": " ",
            "up": "{UP}", "down": "{DOWN}", "left": "{LEFT}", "right": "{RIGHT}",
            "home": "{HOME}", "end": "{END}",
            "pageup": "{PGUP}", "pagedown": "{PGDN}",
            "f1": "{F1}", "f2": "{F2}", "f3": "{F3}", "f4": "{F4}",
            "f5": "{F5}", "f6": "{F6}", "f7": "{F7}", "f8": "{F8}",
            "f9": "{F9}", "f10": "{F10}", "f11": "{F11}", "f12": "{F12}",
        }

        # Try pywinauto first
        if context.active_window:
            pk = pywinauto_keys.get(key.lower(), key)
            try:
                context.active_window.type_keys(pk)
                return
            except Exception:
                pass

        Keyboard.press_key(key)

    def _do_hotkey(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Press a key combination."""
        keys = params.get("keys", []) if params else []
        if not keys:
            raise ActionError("No keys specified for hotkey")

        # Map modifier names for pywinauto
        mod_map = {"ctrl": "^", "alt": "%", "shift": "+"}
        pywinauto_keys = {
            "f4": "{F4}", "f1": "{F1}", "f2": "{F2}", "f3": "{F3}",
            "f5": "{F5}", "f6": "{F6}", "f7": "{F7}", "f8": "{F8}",
            "escape": "{ESC}", "enter": "{ENTER}", "tab": "{TAB}",
            "delete": "{DELETE}", "backspace": "{BACKSPACE}",
        }

        # Try pywinauto type_keys format: ^a = Ctrl+A, %{F4} = Alt+F4
        if context.active_window:
            try:
                combo = ""
                for k in keys:
                    kl = k.lower()
                    if kl in mod_map:
                        combo += mod_map[kl]
                    elif kl in pywinauto_keys:
                        combo += pywinauto_keys[kl]
                    else:
                        combo += kl
                context.active_window.type_keys(combo)
                return
            except Exception:
                pass

        Keyboard.hotkey(*keys)

    def _do_wait(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Static wait."""
        seconds = params.get("seconds", 1.0) if params else 1.0
        time.sleep(seconds)

    def _do_set_value(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Set value via UIA ValuePattern."""
        if not element or not element.uia_element:
            raise ActionError("No UIA element to set value on")

        value = params.get("value", "") if params else ""
        value = context.resolve_variables(value)

        try:
            element.uia_element.set_edit_text(value)
        except Exception as e:
            raise ActionError(f"Cannot set value: {e}")

    def _do_select(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Select an item in a dropdown/combo box."""
        if not element or not element.uia_element:
            raise ActionError("No UIA element to select from")

        item = params.get("item", "") if params else ""
        item = context.resolve_variables(item)

        try:
            element.uia_element.select(item)
        except Exception as e:
            raise ActionError(f"Cannot select '{item}': {e}")

    def _do_scroll(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Scroll at element position or screen center."""
        direction = params.get("direction", "down") if params else "down"
        amount = params.get("amount", 3) if params else 3

        if element:
            x, y = element.center
        else:
            # Scroll at active window center
            if context.active_window:
                try:
                    rect = context.active_window.rectangle()
                    x = (rect.left + rect.right) // 2
                    y = (rect.top + rect.bottom) // 2
                except Exception:
                    x, y = 500, 500
            else:
                x, y = 500, 500

        Mouse.scroll(x, y, amount=amount, direction=direction)

    def _do_assert(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Evaluate an assertion."""
        assert_type = params.get("type", "exists") if params else "exists"
        expected = params.get("expected", "") if params else ""
        expected = context.resolve_variables(expected)

        if not ConditionEvaluator.evaluate(assert_type, element, expected, context):
            raise ActionError(f"Assertion failed: {assert_type}")

    def _do_screenshot(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Capture a screenshot."""
        filename = params.get("filename", "") if params else ""
        if not filename:
            filename = f"step_{context.step_index}_{datetime.now().strftime('%H%M%S')}.png"
        filename = context.resolve_variables(filename)

        save_path = os.path.join(context.screenshot_dir, filename)
        os.makedirs(os.path.dirname(save_path), exist_ok=True)

        # Use Pillow for screenshot
        try:
            from PIL import ImageGrab
            if context.active_window:
                try:
                    rect = context.active_window.rectangle()
                    img = ImageGrab.grab(bbox=(rect.left, rect.top, rect.right, rect.bottom))
                except Exception:
                    img = ImageGrab.grab()
            else:
                img = ImageGrab.grab()
            img.save(save_path)
            context.screenshots.append(save_path)
            log.info(f"    Screenshot saved: {save_path}")
        except ImportError:
            raise ActionError("Pillow not installed, cannot capture screenshot")

    def _do_launch(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Launch an application."""
        command = params.get("command", "") if params else ""
        args = params.get("args", []) if params else []

        if not command:
            raise ActionError("No command specified for launch")

        cmd_list = [command] + (args or [])
        context.process = subprocess.Popen(cmd_list)
        log.info(f"    Launched: {command} (pid={context.process.pid})")

    def _do_close(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Close the application."""
        method = params.get("method", "hotkey") if params else "hotkey"

        if method == "hotkey":
            keys = params.get("keys", ["alt", "f4"]) if params else ["alt", "f4"]
            Keyboard.hotkey(*keys)
        elif method == "kill":
            if context.process:
                context.process.kill()
        elif method == "close_button" and element:
            self._do_click(element, {"button": "left"}, context)

    def _do_focus(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Focus the active window."""
        if context.active_window:
            try:
                context.active_window.set_focus()
            except Exception:
                from appbot.input.window import WindowManager
                hwnd = context.active_window.handle
                WindowManager.focus_window(hwnd)

    def _do_store_value(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Store element text into a variable."""
        if not element:
            raise ActionError("No element to read value from")

        var_name = params.get("variable_name", "") if params else ""
        if not var_name:
            raise ActionError("No variable_name specified")

        value = element.text or element.name or ""
        context.set_variable(var_name, value)
        log.info(f"    Stored: ${{{var_name}}} = '{value}'")

    def _do_drag_drop(self, element: Optional[FoundElement], params: dict, context: RuntimeContext):
        """Drag from element to target coordinates."""
        if not element:
            raise ActionError("No element to drag from")

        to_x = params.get("to_x", 0) if params else 0
        to_y = params.get("to_y", 0) if params else 0

        Mouse.drag(element.center[0], element.center[1], to_x, to_y)
