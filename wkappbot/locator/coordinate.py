"""Coordinate-based element locator - fallback strategy."""

from typing import Optional

from appbot.locator.base import BaseLocator, FoundElement
from appbot.config import log


class CoordinateLocator(BaseLocator):
    """
    Fallback locator using explicit coordinates.
    Used when neither Accessibility nor Vision can find the element.
    """

    @property
    def strategy_name(self) -> str:
        return "coordinate"

    def locate(self, target, context) -> Optional[FoundElement]:
        """Locate element by explicit coordinate."""
        if not target.coordinate or len(target.coordinate) < 2:
            return None

        x, y = target.coordinate[0], target.coordinate[1]

        # If coordinates are relative to window, convert to absolute
        if context.active_window:
            try:
                rect = context.active_window.rectangle()
                # Assume coordinates < 10000 are relative to window
                if x < 10000 and y < 10000:
                    x += rect.left
                    y += rect.top
            except Exception:
                pass

        log.debug(f"  Coordinate locator: ({x}, {y})")

        return FoundElement(
            source="coordinate",
            uia_element=None,
            bounding_rect=(x - 5, y - 5, x + 5, y + 5),
            center=(x, y),
            name=target.name or f"coordinate({x},{y})",
            control_type="unknown",
            confidence=0.5,
        )
