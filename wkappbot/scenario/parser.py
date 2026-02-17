"""YAML scenario parser and validator."""

import yaml
from pathlib import Path
from typing import Union

from pydantic import ValidationError

from appbot.scenario.models import Scenario
from appbot.config import log


class ScenarioParseError(Exception):
    """Raised when scenario YAML is invalid."""
    pass


class ScenarioParser:
    """Parses and validates YAML test scenario files."""

    @staticmethod
    def load(path: Union[str, Path]) -> Scenario:
        """Load and validate a scenario from YAML file."""
        path = Path(path)

        if not path.exists():
            raise ScenarioParseError(f"Scenario file not found: {path}")

        if not path.suffix in ('.yaml', '.yml'):
            raise ScenarioParseError(f"Expected .yaml or .yml file, got: {path.suffix}")

        try:
            with open(path, 'r', encoding='utf-8') as f:
                raw = yaml.safe_load(f)
        except yaml.YAMLError as e:
            raise ScenarioParseError(f"YAML syntax error: {e}")

        if not isinstance(raw, dict):
            raise ScenarioParseError("Scenario must be a YAML mapping (dict)")

        try:
            scenario = Scenario(**raw)
        except ValidationError as e:
            errors = []
            for err in e.errors():
                loc = " -> ".join(str(l) for l in err["loc"])
                errors.append(f"  {loc}: {err['msg']}")
            raise ScenarioParseError(
                f"Scenario validation failed:\n" + "\n".join(errors)
            )

        log.debug(f"Loaded scenario: {scenario.scenario.name} ({len(scenario.steps)} steps)")
        return scenario

    @staticmethod
    def validate(path: Union[str, Path]) -> bool:
        """Validate a scenario file without running it. Returns True if valid."""
        try:
            scenario = ScenarioParser.load(path)
            log.info(f"[PASS] Scenario valid: {scenario.scenario.name}")
            log.info(f"  Steps: {len(scenario.steps)}")
            log.info(f"  App: {scenario.app.launch or scenario.app.window_title}")
            if scenario.teardown:
                log.info(f"  Teardown: {len(scenario.teardown)} steps")
            return True
        except ScenarioParseError as e:
            log.error(f"[FAIL] {e}")
            return False
