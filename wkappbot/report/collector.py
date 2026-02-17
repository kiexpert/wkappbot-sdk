"""Result data collection for reporting."""

import os
import base64
from typing import List, Optional
from datetime import datetime

from appbot.core.context import ScenarioResult, StepResult


class ReportData:
    """Prepared data for report rendering."""

    def __init__(self, result: ScenarioResult):
        self.scenario_name = result.scenario_name
        self.passed = result.passed
        self.status = "PASSED" if result.passed else "FAILED"
        self.duration = f"{result.duration:.2f}s"
        self.start_time = result.start_time
        self.total_steps = result.total_count
        self.passed_steps = result.passed_count
        self.failed_steps = result.failed_count
        self.pass_rate = (
            f"{result.passed_count / result.total_count * 100:.0f}%"
            if result.total_count > 0 else "N/A"
        )
        self.environment = result.environment
        self.steps = self._prepare_steps(result.steps)

    def _prepare_steps(self, steps: List[StepResult]) -> List[dict]:
        """Prepare step data for template rendering."""
        prepared = []
        for i, step in enumerate(steps):
            data = {
                "index": i + 1,
                "name": step.step_name,
                "action": step.action,
                "status": "PASS" if step.passed else "FAIL",
                "status_class": "pass" if step.passed else "fail",
                "duration": f"{step.duration:.2f}s",
                "error": step.error_message or "",
                "locator": step.element_found or "-",
                "screenshot": None,
            }

            # Embed screenshot as base64 if available
            if step.screenshot_path and os.path.exists(step.screenshot_path):
                data["screenshot"] = self._image_to_data_uri(step.screenshot_path)

            prepared.append(data)
        return prepared

    @staticmethod
    def _image_to_data_uri(path: str) -> str:
        """Convert image to base64 data URI for HTML embedding."""
        try:
            with open(path, "rb") as f:
                data = base64.b64encode(f.read()).decode("utf-8")
            ext = os.path.splitext(path)[1].lower()
            mime = {"png": "image/png", ".jpg": "image/jpeg", ".jpeg": "image/jpeg"}.get(ext, "image/png")
            return f"data:{mime};base64,{data}"
        except Exception:
            return ""
