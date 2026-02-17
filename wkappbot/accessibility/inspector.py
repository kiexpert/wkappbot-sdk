"""Accessibility tree inspector - dump UIA tree for any window."""

import json
import time
from typing import Optional

from pywinauto import Application, Desktop
from pywinauto.findwindows import ElementNotFoundError

from appbot.accessibility.element import UIElement
from appbot.config import log


class AccessibilityInspector:
    """
    Dumps the Windows UI Automation tree for an application window.
    Supports JSON, indented tree, and flat list output formats.
    """

    def __init__(self, backend: str = "uia"):
        self.backend = backend

    def dump_tree(self, target: str, depth: int = 10, fmt: str = "tree") -> str:
        """
        Find a window by title and dump its UIA element tree.

        Args:
            target: Window title (substring match) or process name
            depth: Maximum tree traversal depth
            fmt: Output format - "json", "tree", or "text"

        Returns:
            Formatted string representation of the UI tree
        """
        app = self._connect(target)
        window = app.top_window()

        log.debug(f"Inspecting: {window.window_text()} (depth={depth}, format={fmt})")

        root = self._walk_element(window.wrapper_object(), max_depth=depth)

        if fmt == "json":
            return json.dumps(root.to_dict(), indent=2, ensure_ascii=False)
        elif fmt == "tree":
            return self._format_tree(root)
        else:  # "text" - flat list
            return self._format_flat(root)

    def dump_element(self, element_wrapper, depth: int = 10, fmt: str = "tree") -> str:
        """Dump tree starting from a specific pywinauto element wrapper."""
        root = self._walk_element(element_wrapper, max_depth=depth)

        if fmt == "json":
            return json.dumps(root.to_dict(), indent=2, ensure_ascii=False)
        elif fmt == "tree":
            return self._format_tree(root)
        else:
            return self._format_flat(root)

    def _connect(self, target: str) -> Application:
        """Connect to an application by window title."""
        try:
            app = Application(backend=self.backend).connect(
                title_re=f".*{target}.*",
                timeout=5,
            )
            return app
        except ElementNotFoundError:
            raise RuntimeError(f"Window not found: '{target}'")
        except Exception as e:
            # Handle ambiguous matches - use found_index=0 to pick the first one
            if "ambiguous" in str(e).lower() or "2 elements" in str(e).lower():
                try:
                    app = Application(backend=self.backend).connect(
                        title_re=f".*{target}.*",
                        timeout=5,
                        found_index=0,
                    )
                    return app
                except Exception:
                    pass
            raise RuntimeError(f"Window connection error: {e}")

    def _walk_element(self, element, max_depth: int, current_depth: int = 0) -> UIElement:
        """Recursively walk UIA element tree and build UIElement tree."""
        try:
            info = element.element_info
            rect_obj = info.rectangle

            node = UIElement(
                name=info.name or "",
                control_type=info.control_type or "",
                automation_id=info.automation_id or "",
                class_name=info.class_name or "",
                rect={
                    "left": rect_obj.left,
                    "top": rect_obj.top,
                    "right": rect_obj.right,
                    "bottom": rect_obj.bottom,
                } if rect_obj else None,
                is_enabled=element.is_enabled() if hasattr(element, 'is_enabled') else True,
                is_visible=element.is_visible() if hasattr(element, 'is_visible') else True,
                depth=current_depth,
                _raw_element=element,
            )

            # Try to get value (for text fields, etc.)
            try:
                if hasattr(element, 'get_value'):
                    node.value = element.get_value() or ""
                elif hasattr(element, 'window_text'):
                    text = element.window_text()
                    if text and text != node.name:
                        node.value = text
            except Exception:
                pass

        except Exception as e:
            node = UIElement(
                name=f"<error: {e}>",
                depth=current_depth,
            )

        # Recurse into children
        if current_depth < max_depth:
            try:
                for child in element.children():
                    child_node = self._walk_element(child, max_depth, current_depth + 1)
                    node.children.append(child_node)
            except Exception:
                pass

        return node

    def _format_tree(self, node: UIElement, prefix: str = "", is_last: bool = True) -> str:
        """Format as indented tree (like `tree` command)."""
        lines = []

        # Build label
        connector = "└── " if is_last else "├── "
        label_parts = []
        if node.control_type:
            label_parts.append(f"[{node.control_type}]")
        if node.name:
            label_parts.append(f'"{node.name}"')
        if node.automation_id:
            label_parts.append(f"(id={node.automation_id})")
        if node.value:
            label_parts.append(f'val="{node.value}"')
        if not node.is_visible:
            label_parts.append("(hidden)")

        label = " ".join(label_parts) or "<unknown>"

        if node.depth == 0:
            lines.append(label)
        else:
            lines.append(f"{prefix}{connector}{label}")

        # Recurse children
        child_prefix = prefix + ("    " if is_last else "│   ")
        for i, child in enumerate(node.children):
            is_child_last = (i == len(node.children) - 1)
            lines.append(self._format_tree(child, child_prefix, is_child_last))

        return "\n".join(lines)

    def _format_flat(self, node: UIElement, lines: list = None) -> str:
        """Format as flat list with depth indentation."""
        if lines is None:
            lines = []

        indent = "  " * node.depth
        parts = []
        if node.control_type:
            parts.append(node.control_type)
        if node.name:
            parts.append(f'name="{node.name}"')
        if node.automation_id:
            parts.append(f'id="{node.automation_id}"')
        if node.class_name:
            parts.append(f'class="{node.class_name}"')
        if node.rect:
            r = node.rect
            parts.append(f'rect=({r["left"]},{r["top"]},{r["right"]},{r["bottom"]})')

        lines.append(f"{indent}{' | '.join(parts)}")

        for child in node.children:
            self._format_flat(child, lines)

        return "\n".join(lines) if node.depth == 0 else ""
