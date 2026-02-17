"""Vision-based element locator using Claude Vision API."""

from typing import Optional

from appbot.locator.base import BaseLocator, FoundElement
from appbot.config import log


class VisionLocator(BaseLocator):
    """
    Secondary locator using Claude Vision API.
    Captures screenshot, sends to Claude, parses element coordinates.
    Falls back here when Accessibility cannot find elements.
    """

    def __init__(self, analyzer, capture):
        """
        Args:
            analyzer: VisionAnalyzer instance
            capture: ScreenCapture instance
        """
        self.analyzer = analyzer
        self.capture = capture

    @property
    def strategy_name(self) -> str:
        return "vision"

    def locate(self, target, context) -> Optional[FoundElement]:
        """
        Find element by capturing screenshot and asking Claude Vision.
        """
        # Need a text description for vision search
        description = target.description or target.name
        if not description:
            return None

        # Capture screenshot of the active window
        window_rect = None
        screenshot_path = None

        if context.active_window:
            try:
                screenshot_path = self.capture.capture_window(
                    context.active_window,
                    save_dir=context.screenshot_dir,
                )
                rect = context.active_window.rectangle()
                window_rect = (rect.left, rect.top, rect.right, rect.bottom)
            except Exception:
                screenshot_path = self.capture.capture_full_screen()
        else:
            screenshot_path = self.capture.capture_full_screen()

        if not screenshot_path:
            return None

        # Ask Claude Vision to find the element
        log.debug(f"  Vision: searching for '{description}'...")
        result = self.analyzer.find_element(
            screenshot_path=screenshot_path,
            description=description,
            window_rect=window_rect,
        )

        if not result or not result.get("found"):
            return None

        center = result.get("center", {})
        bb = result.get("bounding_box", {})

        return FoundElement(
            source="vision",
            uia_element=None,
            bounding_rect=(
                bb.get("left", 0), bb.get("top", 0),
                bb.get("right", 0), bb.get("bottom", 0)
            ),
            center=(center.get("x", 0), center.get("y", 0)),
            name=result.get("element_name", description),
            control_type=result.get("element_type", "unknown"),
            confidence=result.get("confidence", 0.5),
        )
