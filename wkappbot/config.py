"""Global configuration and logging setup."""

import logging
import os
import sys
from dataclasses import dataclass, field
from typing import Optional

from colorama import Fore, Style, init as colorama_init

colorama_init(autoreset=True)


@dataclass
class AppBotConfig:
    """Global configuration for AppBot."""
    timeout: float = 10.0
    screenshot_on_step: bool = True
    screenshot_on_failure: bool = True
    continue_on_error: bool = False
    retry_count: int = 2
    retry_delay: float = 1.0
    screenshot_dir: str = "output/screenshots"
    report_dir: str = "output/reports"
    verbose: bool = False
    vision_api_key: Optional[str] = None
    vision_model: str = "claude-sonnet-4-20250514"

    def __post_init__(self):
        if not self.vision_api_key:
            self.vision_api_key = os.environ.get("ANTHROPIC_API_KEY")


class ColorFormatter(logging.Formatter):
    """Colored log formatter for terminal output."""

    COLORS = {
        logging.DEBUG: Fore.CYAN,
        logging.INFO: Fore.WHITE,
        logging.WARNING: Fore.YELLOW,
        logging.ERROR: Fore.RED,
        logging.CRITICAL: Fore.RED + Style.BRIGHT,
    }

    CUSTOM_LEVELS = {
        "PASS": Fore.GREEN,
        "FAIL": Fore.RED + Style.BRIGHT,
        "STEP": Fore.CYAN + Style.BRIGHT,
    }

    def format(self, record):
        color = self.COLORS.get(record.levelno, Fore.WHITE)

        # Custom level names
        msg = record.getMessage()
        for key, c in self.CUSTOM_LEVELS.items():
            if msg.startswith(f"[{key}]"):
                color = c
                break

        timestamp = self.formatTime(record, "%H:%M:%S")
        return f"{Fore.LIGHTBLACK_EX}{timestamp}{Style.RESET_ALL} {color}{msg}{Style.RESET_ALL}"


def setup_logging(verbose: bool = False) -> logging.Logger:
    """Setup AppBot logger with colored output."""
    logger = logging.getLogger("appbot")
    logger.setLevel(logging.DEBUG if verbose else logging.INFO)

    if not logger.handlers:
        handler = logging.StreamHandler(sys.stdout)
        handler.setFormatter(ColorFormatter())
        logger.addHandler(handler)

    return logger


# Global logger
log = setup_logging()
