"""UIElement abstraction over pywinauto elements."""

from dataclasses import dataclass, field
from typing import Optional, List, Any


@dataclass
class UIElement:
    """Abstraction over a UI Automation element."""
    name: str = ""
    control_type: str = ""
    automation_id: str = ""
    class_name: str = ""
    rect: Optional[dict] = None  # {"left": x, "top": y, "right": x2, "bottom": y2}
    is_enabled: bool = True
    is_visible: bool = True
    value: str = ""
    children: List['UIElement'] = field(default_factory=list)
    depth: int = 0
    _raw_element: Any = field(default=None, repr=False)

    @property
    def center(self) -> tuple:
        """Return center point (x, y) of the element."""
        if self.rect:
            cx = (self.rect["left"] + self.rect["right"]) // 2
            cy = (self.rect["top"] + self.rect["bottom"]) // 2
            return (cx, cy)
        return (0, 0)

    @property
    def width(self) -> int:
        if self.rect:
            return self.rect["right"] - self.rect["left"]
        return 0

    @property
    def height(self) -> int:
        if self.rect:
            return self.rect["bottom"] - self.rect["top"]
        return 0

    def to_dict(self) -> dict:
        """Convert to dictionary (for JSON serialization)."""
        d = {
            "name": self.name,
            "control_type": self.control_type,
            "automation_id": self.automation_id,
            "class_name": self.class_name,
            "rect": self.rect,
            "is_enabled": self.is_enabled,
            "is_visible": self.is_visible,
        }
        if self.value:
            d["value"] = self.value
        if self.children:
            d["children"] = [c.to_dict() for c in self.children]
        return d
