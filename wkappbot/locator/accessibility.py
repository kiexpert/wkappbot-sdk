"""Accessibility (UIA) based element locator - primary strategy."""

from typing import Optional

from pywinauto.timings import TimeoutError as PywinautoTimeout

from appbot.locator.base import BaseLocator, FoundElement
from appbot.config import log


class AccessibilityLocator(BaseLocator):
    """
    Primary locator using Windows UI Automation via pywinauto.
    Fast, reliable, and free - but requires apps with proper UIA support.
    """

    @property
    def strategy_name(self) -> str:
        return "accessibility"

    def locate(self, target, context) -> Optional[FoundElement]:
        """
        Find element using UIA properties (automation_id, name, control_type, etc.)
        """
        window = context.active_window
        if not window:
            return None

        # Build pywinauto search criteria
        criteria = {}
        if target.automation_id:
            criteria["auto_id"] = target.automation_id
        if target.name:
            criteria["title"] = target.name
        if target.control_type:
            criteria["control_type"] = target.control_type
        if target.class_name:
            criteria["class_name"] = target.class_name

        if not criteria:
            return None  # No accessibility criteria provided

        timeout = context.default_timeout

        try:
            element = window.child_window(**criteria)
            element.wait("exists visible", timeout=timeout)

            rect = element.rectangle()
            mid = rect.mid_point()

            # Try to get text value
            text = ""
            try:
                text = element.window_text() or ""
            except Exception:
                pass

            return FoundElement(
                source="accessibility",
                uia_element=element,
                bounding_rect=(rect.left, rect.top, rect.right, rect.bottom),
                center=(mid.x, mid.y) if hasattr(mid, 'x') else (mid[0], mid[1]),
                name=element.element_info.name or "",
                control_type=element.element_info.control_type or "",
                confidence=1.0,
                text=text,
            )

        except PywinautoTimeout:
            log.debug(f"  Accessibility: timeout searching for {criteria}")
            return None
        except Exception as e:
            log.debug(f"  Accessibility: error - {e}")
            return None
