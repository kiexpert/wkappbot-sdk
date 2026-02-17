"""Locator strategy base and Chain of Responsibility."""

from abc import ABC, abstractmethod
from dataclasses import dataclass, field
from typing import Optional, List, Any

from appbot.config import log


@dataclass
class FoundElement:
    """Abstraction over a found UI element, regardless of how it was found."""
    source: str               # "accessibility" | "vision" | "coordinate"
    uia_element: Any = None   # Raw pywinauto element (if from accessibility)
    bounding_rect: tuple = (0, 0, 0, 0)  # (left, top, right, bottom)
    center: tuple = (0, 0)    # (x, y) click point
    name: str = ""
    control_type: str = ""
    confidence: float = 1.0   # 1.0 for accessibility, 0.0-1.0 for vision
    text: str = ""            # Element text/value

    def __repr__(self):
        return (f"FoundElement(source={self.source}, name='{self.name}', "
                f"type={self.control_type}, center={self.center}, "
                f"confidence={self.confidence:.2f})")


class ElementNotFoundException(Exception):
    """Raised when no locator strategy can find the target element."""
    pass


class BaseLocator(ABC):
    """Abstract base class for element locators."""

    @property
    @abstractmethod
    def strategy_name(self) -> str:
        """Name of this locator strategy."""
        pass

    @abstractmethod
    def locate(self, target, context) -> Optional[FoundElement]:
        """
        Try to locate a UI element.

        Args:
            target: LocatorTarget from scenario model
            context: RuntimeContext with active window info

        Returns:
            FoundElement if found, None if not found by this strategy
        """
        pass


class LocatorChain:
    """
    Chain of Responsibility: tries locators in order until one succeeds.
    Default order: Accessibility -> Vision -> Coordinate
    """

    def __init__(self):
        self.locators: List[BaseLocator] = []

    def add(self, locator: BaseLocator) -> 'LocatorChain':
        """Add a locator to the chain."""
        self.locators.append(locator)
        return self

    def locate(self, target, context) -> FoundElement:
        """
        Try to locate an element using the chain of strategies.

        Args:
            target: LocatorTarget from scenario model
            context: RuntimeContext

        Returns:
            FoundElement

        Raises:
            ElementNotFoundException if no strategy succeeds
        """
        strategy = getattr(target, 'strategy', 'auto')

        if strategy != "auto":
            # Use specific strategy
            for locator in self.locators:
                if locator.strategy_name == strategy:
                    result = locator.locate(target, context)
                    if result:
                        log.debug(f"  Locator [{strategy}] -> found: {result.name}")
                        return result
                    raise ElementNotFoundException(
                        f"Strategy '{strategy}' could not find element: {target}"
                    )
            raise ElementNotFoundException(f"Unknown strategy: {strategy}")

        # Auto mode: try each locator in order
        errors = []
        for locator in self.locators:
            try:
                result = locator.locate(target, context)
                if result:
                    log.debug(f"  Locator [{locator.strategy_name}] -> found: {result.name}")
                    return result
            except Exception as e:
                errors.append(f"{locator.strategy_name}: {e}")
                continue

        error_detail = "; ".join(errors) if errors else "no locators available"
        raise ElementNotFoundException(
            f"Could not locate element (tried all strategies): {error_detail}"
        )
