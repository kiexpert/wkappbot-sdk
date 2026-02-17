"""Pydantic data models for YAML scenario definition."""

from typing import Optional, List, Dict, Any, Union
from pydantic import BaseModel, Field


class ScenarioMeta(BaseModel):
    """Scenario metadata."""
    name: str
    description: str = ""
    version: str = "1.0"


class ScenarioConfig(BaseModel):
    """Execution configuration with defaults."""
    timeout: float = 10.0
    screenshot_on_step: bool = True
    screenshot_on_failure: bool = True
    continue_on_error: bool = False
    vision_api_key_env: str = "ANTHROPIC_API_KEY"
    retry_count: int = 2
    retry_delay: float = 1.0


class WaitForWindow(BaseModel):
    """Window detection configuration."""
    title_contains: Optional[str] = None
    title_exact: Optional[str] = None
    class_name: Optional[str] = None
    timeout: float = 10.0


class AppConfig(BaseModel):
    """Application launch/connect configuration."""
    launch: Optional[str] = None
    launch_args: Optional[List[str]] = None
    window_title: Optional[str] = None
    process_name: Optional[str] = None
    wait_for_window: Optional[WaitForWindow] = None
    working_dir: Optional[str] = None


class LocatorTarget(BaseModel):
    """How to find a UI element - supports multiple strategies."""
    strategy: str = "auto"  # auto | accessibility | vision | coordinate
    automation_id: Optional[str] = None
    name: Optional[str] = None
    control_type: Optional[str] = None
    class_name: Optional[str] = None
    description: Optional[str] = None   # Natural language for Vision fallback
    coordinate: Optional[List[int]] = None  # [x, y]
    index: Optional[int] = None  # nth match


class StepParams(BaseModel):
    """Generic step parameters - varies by action type."""
    text: Optional[str] = None
    key: Optional[str] = None
    keys: Optional[List[str]] = None
    seconds: Optional[float] = None
    button: Optional[str] = "left"
    filename: Optional[str] = None
    type: Optional[str] = None          # assert type
    expected: Optional[str] = None      # assert expected
    variable_name: Optional[str] = None
    value: Optional[str] = None
    direction: Optional[str] = None     # scroll direction
    amount: Optional[int] = 3           # scroll amount
    command: Optional[str] = None       # launch command
    args: Optional[List[str]] = None
    method: Optional[str] = None        # close method
    condition: Optional[str] = None     # wait_for condition
    interval: Optional[float] = 0.02   # type interval
    timeout: Optional[float] = None     # per-action timeout


class Step(BaseModel):
    """A single test step."""
    name: str
    target: Optional[LocatorTarget] = None
    action: str
    params: Optional[StepParams] = Field(default_factory=StepParams)
    timeout: Optional[float] = None
    continue_on_error: Optional[bool] = None
    screenshot: Optional[bool] = None   # override screenshot_on_step


class Scenario(BaseModel):
    """Complete test scenario definition."""
    scenario: ScenarioMeta
    config: ScenarioConfig = Field(default_factory=ScenarioConfig)
    app: AppConfig
    variables: Dict[str, str] = Field(default_factory=dict)
    steps: List[Step]
    teardown: Optional[List[Step]] = None
