"""HTML report generator using Jinja2."""

import os
from datetime import datetime
from pathlib import Path

from jinja2 import Environment, FileSystemLoader

from appbot.core.context import ScenarioResult
from appbot.report.collector import ReportData
from appbot.config import log
from appbot import __version__


# Default template directory
DEFAULT_TEMPLATE_DIR = str(Path(__file__).parent / "templates")


class HtmlReportGenerator:
    """Generates HTML test reports with embedded screenshots."""

    def __init__(self, template_dir: str = None):
        self.template_dir = template_dir or DEFAULT_TEMPLATE_DIR
        self.env = Environment(
            loader=FileSystemLoader(self.template_dir),
            autoescape=True,
        )

    def generate(self, result: ScenarioResult, output_path: str) -> str:
        """
        Generate an HTML report from scenario results.

        Args:
            result: ScenarioResult from runner
            output_path: Where to save the HTML file

        Returns:
            Path to generated report
        """
        # Prepare data
        data = ReportData(result)

        # Render template
        template = self.env.get_template("report.html")
        html = template.render(
            scenario_name=data.scenario_name,
            status=data.status,
            duration=data.duration,
            start_time=data.start_time,
            total_steps=data.total_steps,
            passed_steps=data.passed_steps,
            failed_steps=data.failed_steps,
            pass_rate=data.pass_rate,
            environment=data.environment,
            steps=data.steps,
            version=__version__,
            generated_at=datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
        )

        # Save
        os.makedirs(os.path.dirname(output_path), exist_ok=True)
        with open(output_path, "w", encoding="utf-8") as f:
            f.write(html)

        log.info(f"Report generated: {output_path}")
        return output_path
