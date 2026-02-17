"""Image encoding/decoding utilities."""

import base64
from pathlib import Path
from typing import Optional


def image_to_base64(image_path: str) -> str:
    """Read an image file and return its base64 encoded string."""
    with open(image_path, "rb") as f:
        return base64.standard_b64encode(f.read()).decode("utf-8")


def get_media_type(image_path: str) -> str:
    """Get MIME type from image file extension."""
    ext = Path(image_path).suffix.lower()
    media_types = {
        ".png": "image/png",
        ".jpg": "image/jpeg",
        ".jpeg": "image/jpeg",
        ".gif": "image/gif",
        ".bmp": "image/bmp",
        ".webp": "image/webp",
    }
    return media_types.get(ext, "image/png")
