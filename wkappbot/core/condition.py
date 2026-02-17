"""Condition and assertion evaluation."""

import re
from typing import Optional

from appbot.locator.base import FoundElement
from appbot.config import log


class AssertionError(Exception):
    """Custom assertion error with details."""
    pass


class ConditionEvaluator:
    """Evaluates assertions and conditions on UI elements."""

    @staticmethod
    def evaluate(assert_type: str, element: Optional[FoundElement],
                 expected: str, context) -> bool:
        """
        Evaluate an assertion.

        Args:
            assert_type: Type of assertion (text_contains, text_equals, exists, etc.)
            element: Found UI element (may be None for 'not_exists')
            expected: Expected value (resolved variables)
            context: RuntimeContext

        Returns:
            True if assertion passes

        Raises:
            AssertionError if assertion fails
        """
        handler = getattr(ConditionEvaluator, f"_assert_{assert_type}", None)
        if not handler:
            raise ValueError(f"Unknown assertion type: {assert_type}")

        return handler(element, expected, context)

    @staticmethod
    def _assert_text_contains(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that element text contains the expected string."""
        if not element:
            raise AssertionError("Element not found, cannot check text")

        actual = element.text or element.name or ""
        if expected in actual:
            log.info(f"    Assert text_contains: '{expected}' in '{actual}' -> PASS")
            return True
        raise AssertionError(
            f"text_contains failed: expected '{expected}' in '{actual}'"
        )

    @staticmethod
    def _assert_text_equals(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that element text exactly equals the expected string."""
        if not element:
            raise AssertionError("Element not found, cannot check text")

        actual = element.text or element.name or ""
        if actual == expected:
            log.info(f"    Assert text_equals: '{actual}' == '{expected}' -> PASS")
            return True
        raise AssertionError(
            f"text_equals failed: expected '{expected}', got '{actual}'"
        )

    @staticmethod
    def _assert_text_matches(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that element text matches a regex pattern."""
        if not element:
            raise AssertionError("Element not found, cannot check text")

        actual = element.text or element.name or ""
        if re.search(expected, actual):
            log.info(f"    Assert text_matches: '{expected}' in '{actual}' -> PASS")
            return True
        raise AssertionError(
            f"text_matches failed: pattern '{expected}' not found in '{actual}'"
        )

    @staticmethod
    def _assert_exists(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that the element exists and is visible."""
        if element:
            log.info(f"    Assert exists: '{element.name}' -> PASS")
            return True
        raise AssertionError("exists failed: element not found")

    @staticmethod
    def _assert_not_exists(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that the element does NOT exist."""
        if not element:
            log.info(f"    Assert not_exists -> PASS")
            return True
        raise AssertionError(
            f"not_exists failed: element found: '{element.name}'"
        )

    @staticmethod
    def _assert_enabled(element: Optional[FoundElement], expected: str, context) -> bool:
        """Assert that element is enabled."""
        if not element:
            raise AssertionError("Element not found, cannot check enabled state")

        if element.uia_element:
            try:
                if element.uia_element.is_enabled():
                    log.info(f"    Assert enabled: '{element.name}' -> PASS")
                    return True
            except Exception:
                pass
        raise AssertionError(f"enabled failed: element '{element.name}' is not enabled")
