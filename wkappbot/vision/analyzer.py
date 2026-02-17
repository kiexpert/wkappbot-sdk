"""Claude Vision API integration for UI element recognition."""

import json
import re
from typing import Optional, Dict, Any

from appbot.utils.image_utils import image_to_base64, get_media_type
from appbot.config import log


class VisionAnalyzer:
    """
    Uses Claude Vision API to analyze screenshots and locate UI elements.
    Fallback strategy when Accessibility API cannot find elements.
    """

    def __init__(self, api_key: str, model: str = "claude-sonnet-4-20250514"):
        import anthropic
        self.client = anthropic.Anthropic(api_key=api_key)
        self.model = model

    def find_element(self, screenshot_path: str, description: str,
                     window_rect: tuple = None) -> Optional[Dict[str, Any]]:
        """
        Ask Claude to find a UI element matching a description in a screenshot.

        Args:
            screenshot_path: Path to screenshot image
            description: Natural language description of the element
            window_rect: (left, top, right, bottom) of the window for coordinate mapping

        Returns:
            Dict with found element info, or None
        """
        image_data = image_to_base64(screenshot_path)
        media_type = get_media_type(screenshot_path)

        prompt = f"""Analyze this Windows application screenshot.
Find the UI element that best matches this description: "{description}"

Return ONLY a JSON object (no markdown, no explanation):
{{
  "found": true,
  "element_name": "human-readable name",
  "element_type": "button|textbox|label|checkbox|dropdown|menu|tab|other",
  "bounding_box": {{"left": x, "top": y, "right": x2, "bottom": y2}},
  "center": {{"x": center_x, "y": center_y}},
  "confidence": 0.0 to 1.0
}}

If the element is NOT found, return:
{{"found": false, "reason": "..."}}

Coordinates must be in pixels relative to the screenshot image (top-left is 0,0).
Be precise with bounding box coordinates.
"""

        try:
            response = self.client.messages.create(
                model=self.model,
                max_tokens=1024,
                messages=[{
                    "role": "user",
                    "content": [
                        {
                            "type": "image",
                            "source": {
                                "type": "base64",
                                "media_type": media_type,
                                "data": image_data,
                            }
                        },
                        {
                            "type": "text",
                            "text": prompt,
                        }
                    ]
                }]
            )

            result_text = response.content[0].text.strip()
            result = self._parse_json(result_text)

            if result and result.get("found"):
                log.debug(f"  Vision found: {result.get('element_name')} "
                         f"(confidence={result.get('confidence', 0):.2f})")

                # Adjust coordinates to absolute screen coordinates if window_rect given
                if window_rect and "center" in result:
                    result["center"]["x"] += window_rect[0]
                    result["center"]["y"] += window_rect[1]
                    if "bounding_box" in result:
                        bb = result["bounding_box"]
                        bb["left"] += window_rect[0]
                        bb["top"] += window_rect[1]
                        bb["right"] += window_rect[0]
                        bb["bottom"] += window_rect[1]

            return result

        except Exception as e:
            log.error(f"  Vision API error: {e}")
            return None

    def describe_screen(self, screenshot_path: str) -> str:
        """
        Get a detailed description of all visible UI elements in a screenshot.
        Used for debugging and the 'capture --analyze' command.

        Returns:
            Human-readable description of the UI
        """
        image_data = image_to_base64(screenshot_path)
        media_type = get_media_type(screenshot_path)

        prompt = """Analyze this Windows application screenshot in detail.

List ALL visible UI elements with their:
1. Type (button, text field, label, menu, etc.)
2. Text/label content
3. Approximate position (top-left, center, bottom-right, etc.)
4. State (enabled/disabled, checked/unchecked, focused, etc.)

Also describe:
- The application name and window title
- The overall layout/structure
- Any notable UI patterns

Be thorough and precise. This information will be used for UI automation."""

        try:
            response = self.client.messages.create(
                model=self.model,
                max_tokens=4096,
                messages=[{
                    "role": "user",
                    "content": [
                        {
                            "type": "image",
                            "source": {
                                "type": "base64",
                                "media_type": media_type,
                                "data": image_data,
                            }
                        },
                        {
                            "type": "text",
                            "text": prompt,
                        }
                    ]
                }]
            )

            return response.content[0].text

        except Exception as e:
            return f"Vision analysis failed: {e}"

    def find_all_elements(self, screenshot_path: str) -> list:
        """
        Find all interactive UI elements in a screenshot.
        Returns a list of element descriptions with coordinates.
        """
        image_data = image_to_base64(screenshot_path)
        media_type = get_media_type(screenshot_path)

        prompt = """Analyze this Windows application screenshot.
Find ALL interactive UI elements (buttons, text fields, checkboxes, menus, etc.)

Return ONLY a JSON array (no markdown, no explanation):
[
  {
    "element_name": "...",
    "element_type": "button|textbox|label|checkbox|dropdown|menu|tab|other",
    "bounding_box": {"left": x, "top": y, "right": x2, "bottom": y2},
    "center": {"x": cx, "y": cy},
    "text": "visible text on the element"
  },
  ...
]

Coordinates in pixels relative to the screenshot (top-left is 0,0).
Include ALL clickable/interactive elements visible on screen."""

        try:
            response = self.client.messages.create(
                model=self.model,
                max_tokens=4096,
                messages=[{
                    "role": "user",
                    "content": [
                        {
                            "type": "image",
                            "source": {
                                "type": "base64",
                                "media_type": media_type,
                                "data": image_data,
                            }
                        },
                        {
                            "type": "text",
                            "text": prompt,
                        }
                    ]
                }]
            )

            result_text = response.content[0].text.strip()
            return self._parse_json(result_text) or []

        except Exception as e:
            log.error(f"Vision find_all_elements error: {e}")
            return []

    @staticmethod
    def _parse_json(text: str) -> Optional[Any]:
        """Parse JSON from Claude's response, handling markdown code blocks."""
        # Remove markdown code block if present
        text = text.strip()
        if text.startswith("```"):
            text = re.sub(r'^```(?:json)?\s*', '', text)
            text = re.sub(r'\s*```$', '', text)

        try:
            return json.loads(text)
        except json.JSONDecodeError:
            # Try to extract JSON from mixed text
            json_match = re.search(r'[\[{].*[\]}]', text, re.DOTALL)
            if json_match:
                try:
                    return json.loads(json_match.group())
                except json.JSONDecodeError:
                    pass
            log.warning(f"  Could not parse Vision response as JSON")
            return None
