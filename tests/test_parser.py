"""Tests for YAML scenario parser."""

import os
import sys
import pytest
import tempfile

# Add project root to path
sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..'))

from appbot.scenario.parser import ScenarioParser, ScenarioParseError
from appbot.scenario.models import Scenario


VALID_SCENARIO = """
scenario:
  name: "Test Scenario"
  description: "A test scenario"

app:
  launch: "notepad.exe"

steps:
  - name: "Type hello"
    action: type_text
    params:
      text: "hello world"

  - name: "Save file"
    action: hotkey
    params:
      keys: ["ctrl", "s"]
"""

MINIMAL_SCENARIO = """
scenario:
  name: "Minimal"

app:
  launch: "calc.exe"

steps:
  - name: "Press Enter"
    action: press_key
    params:
      key: "enter"
"""

SCENARIO_WITH_TARGET = """
scenario:
  name: "With Target"

app:
  launch: "calc.exe"
  wait_for_window:
    title_contains: "Calculator"
    timeout: 5

variables:
  num: "42"

steps:
  - name: "Click button"
    target:
      strategy: auto
      automation_id: "plusButton"
      name: "Plus"
      control_type: "Button"
      description: "The plus button"
    action: click

  - name: "Verify result"
    target:
      automation_id: "result"
    action: assert
    params:
      type: text_contains
      expected: "${num}"

teardown:
  - name: "Close"
    action: hotkey
    params:
      keys: ["alt", "f4"]
"""

INVALID_NO_STEPS = """
scenario:
  name: "No Steps"

app:
  launch: "calc.exe"
"""

INVALID_YAML = """
scenario:
  name: "Bad YAML
  missing: closing quote
"""


class TestScenarioParser:
    """Test YAML scenario parsing and validation."""

    def _write_temp(self, content: str, suffix=".yaml") -> str:
        """Write content to temp file, return path."""
        fd, path = tempfile.mkstemp(suffix=suffix)
        with os.fdopen(fd, 'w', encoding='utf-8') as f:
            f.write(content)
        return path

    def test_parse_valid_scenario(self):
        path = self._write_temp(VALID_SCENARIO)
        try:
            scenario = ScenarioParser.load(path)
            assert scenario.scenario.name == "Test Scenario"
            assert scenario.app.launch == "notepad.exe"
            assert len(scenario.steps) == 2
            assert scenario.steps[0].action == "type_text"
            assert scenario.steps[0].params.text == "hello world"
            assert scenario.steps[1].action == "hotkey"
            assert scenario.steps[1].params.keys == ["ctrl", "s"]
        finally:
            os.unlink(path)

    def test_parse_minimal_scenario(self):
        path = self._write_temp(MINIMAL_SCENARIO)
        try:
            scenario = ScenarioParser.load(path)
            assert scenario.scenario.name == "Minimal"
            assert len(scenario.steps) == 1
            assert scenario.config.timeout == 10.0  # default
        finally:
            os.unlink(path)

    def test_parse_scenario_with_target(self):
        path = self._write_temp(SCENARIO_WITH_TARGET)
        try:
            scenario = ScenarioParser.load(path)
            assert scenario.variables == {"num": "42"}
            assert scenario.steps[0].target.automation_id == "plusButton"
            assert scenario.steps[0].target.strategy == "auto"
            assert scenario.steps[0].target.description == "The plus button"
            assert scenario.steps[1].action == "assert"
            assert scenario.steps[1].params.type == "text_contains"
            assert scenario.teardown is not None
            assert len(scenario.teardown) == 1
        finally:
            os.unlink(path)

    def test_parse_missing_steps(self):
        path = self._write_temp(INVALID_NO_STEPS)
        try:
            with pytest.raises(ScenarioParseError):
                ScenarioParser.load(path)
        finally:
            os.unlink(path)

    def test_parse_invalid_yaml(self):
        path = self._write_temp(INVALID_YAML)
        try:
            with pytest.raises(ScenarioParseError):
                ScenarioParser.load(path)
        finally:
            os.unlink(path)

    def test_parse_file_not_found(self):
        with pytest.raises(ScenarioParseError):
            ScenarioParser.load("nonexistent.yaml")

    def test_parse_wrong_extension(self):
        path = self._write_temp(VALID_SCENARIO, suffix=".txt")
        try:
            with pytest.raises(ScenarioParseError):
                ScenarioParser.load(path)
        finally:
            os.unlink(path)

    def test_validate_valid(self):
        path = self._write_temp(VALID_SCENARIO)
        try:
            assert ScenarioParser.validate(path) is True
        finally:
            os.unlink(path)

    def test_validate_invalid(self):
        path = self._write_temp(INVALID_NO_STEPS)
        try:
            assert ScenarioParser.validate(path) is False
        finally:
            os.unlink(path)

    def test_default_config_values(self):
        path = self._write_temp(MINIMAL_SCENARIO)
        try:
            scenario = ScenarioParser.load(path)
            assert scenario.config.timeout == 10.0
            assert scenario.config.screenshot_on_step is True
            assert scenario.config.continue_on_error is False
            assert scenario.config.retry_count == 2
        finally:
            os.unlink(path)
