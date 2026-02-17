"""Runtime context - holds state during scenario execution."""

import re
import os
from dataclasses import dataclass, field
from typing import Any, Dict, List, Optional


@dataclass
class StepResult:
    """Result of a single step execution."""
    step_name: str = ""
    step_index: int = 0
    action: str = ""
    passed: bool = True
    error_message: str = ""
    duration: float = 0.0
    screenshot_path: Optional[str] = None
    assertion_passed: Optional[bool] = None
    element_found: Optional[str] = None  # locator strategy used
    details: Dict[str, Any] = field(default_factory=dict)

    @property
    def failed(self):
        return not self.passed


@dataclass
class ScenarioResult:
    """Result of a complete scenario execution."""
    scenario_name: str = ""
    passed: bool = True
    steps: List[StepResult] = field(default_factory=list)
    duration: float = 0.0
    start_time: str = ""
    report_path: Optional[str] = None
    environment: Dict[str, str] = field(default_factory=dict)

    @property
    def passed_count(self) -> int:
        return sum(1 for s in self.steps if s.passed)

    @property
    def failed_count(self) -> int:
        return sum(1 for s in self.steps if s.failed)

    @property
    def total_count(self) -> int:
        return len(self.steps)


@dataclass
class RuntimeContext:
    """Holds runtime state during scenario execution."""
    # Window/App references
    active_window: Any = None        # pywinauto WindowSpecification
    uia_wrapper: Any = None          # UIAWrapper instance
    process: Any = None              # subprocess.Popen or pywinauto Application

    # Configuration
    default_timeout: float = 10.0
    screenshot_dir: str = "output/screenshots"
    report_dir: str = "output/reports"

    # Variables
    variables: Dict[str, str] = field(default_factory=dict)

    # Execution state
    step_index: int = 0
    screenshots: List[str] = field(default_factory=list)

    # Vision components (set during initialization)
    vision_analyzer: Any = None
    screen_capture: Any = None

    def resolve_variables(self, text: str) -> str:
        """Replace ${var_name} placeholders with actual values."""
        if not text or "${" not in text:
            return text

        def replacer(match):
            var_name = match.group(1)
            return str(self.variables.get(var_name, match.group(0)))

        return re.sub(r'\$\{(\w+)\}', replacer, text)

    def set_variable(self, name: str, value: str):
        """Set a runtime variable."""
        self.variables[name] = value

    def ensure_dirs(self):
        """Ensure output directories exist."""
        os.makedirs(self.screenshot_dir, exist_ok=True)
        os.makedirs(self.report_dir, exist_ok=True)
