"""Scenario runner - the central orchestrator."""

import os
import time
from datetime import datetime
from typing import Optional

from appbot.config import AppBotConfig, log, setup_logging
from appbot.core.context import RuntimeContext, StepResult, ScenarioResult
from appbot.core.action import ActionExecutor, ActionError
from appbot.locator.base import LocatorChain, ElementNotFoundException
from appbot.locator.accessibility import AccessibilityLocator
from appbot.locator.coordinate import CoordinateLocator
from appbot.accessibility.uia_wrapper import UIAWrapper
from appbot.scenario.parser import ScenarioParser
from appbot.scenario.models import Scenario, Step


class ScenarioRunner:
    """
    Orchestrates test scenario execution.
    Flow: Parse YAML -> Launch app -> Iterate steps -> Locate elements -> Execute actions -> Report
    """

    def __init__(self, config: AppBotConfig):
        self.config = config
        self.action_executor = ActionExecutor()

        # Setup logging
        if config.verbose:
            setup_logging(verbose=True)

    def run(self, scenario_path: str) -> ScenarioResult:
        """
        Execute a complete test scenario.

        Args:
            scenario_path: Path to YAML scenario file

        Returns:
            ScenarioResult with pass/fail and step details
        """
        # Parse scenario
        scenario = ScenarioParser.load(scenario_path)
        log.info(f"Starting scenario: {scenario.scenario.name}")
        log.info(f"  Description: {scenario.scenario.description}")

        # Initialize context
        context = RuntimeContext(
            default_timeout=scenario.config.timeout,
            screenshot_dir=self.config.screenshot_dir,
            report_dir=self.config.report_dir,
            variables=dict(scenario.variables),
        )
        context.ensure_dirs()

        # Build locator chain
        locator_chain = self._build_locator_chain(context)

        # Track results
        result = ScenarioResult(
            scenario_name=scenario.scenario.name,
            start_time=datetime.now().isoformat(),
            environment={
                "python": self._get_python_version(),
                "os": self._get_os_info(),
            },
        )

        start_time = time.time()

        try:
            # Setup: launch or connect to app
            self._setup_app(scenario, context)

            # Execute steps
            for i, step in enumerate(scenario.steps):
                context.step_index = i

                log.info(f"Step {i+1}/{len(scenario.steps)}: {step.name} [{step.action}]")

                step_result = self._execute_step(step, locator_chain, context, scenario)
                step_result.step_index = i
                step_result.step_name = step.name
                result.steps.append(step_result)

                status = "[PASS]" if step_result.passed else "[FAIL]"
                log.info(f"  {status} Step {i+1} ({step_result.duration:.2f}s)")

                if step_result.error_message:
                    log.error(f"    Error: {step_result.error_message}")

                if step_result.failed:
                    # Screenshot on failure
                    if scenario.config.screenshot_on_failure:
                        self._capture_failure_screenshot(context, i)

                    cont = step.continue_on_error
                    if cont is None:
                        cont = scenario.config.continue_on_error
                    if not cont:
                        break

        except Exception as e:
            log.error(f"Scenario error: {e}")
            result.steps.append(StepResult(
                step_name="<setup>",
                action="setup",
                passed=False,
                error_message=str(e),
            ))

        finally:
            # Teardown
            self._teardown(scenario, locator_chain, context)

        result.duration = time.time() - start_time
        result.passed = all(s.passed for s in result.steps)

        # Generate report
        try:
            result.report_path = self._generate_report(result, context)
        except Exception as e:
            log.warning(f"Report generation failed: {e}")

        # Summary
        log.info("=" * 60)
        return result

    def _build_locator_chain(self, context: RuntimeContext) -> LocatorChain:
        """Build the locator chain: Accessibility -> Vision -> Coordinate."""
        chain = LocatorChain()
        chain.add(AccessibilityLocator())

        # Vision locator added in Phase 5
        try:
            from appbot.locator.vision import VisionLocator
            if self.config.vision_api_key:
                from appbot.vision.analyzer import VisionAnalyzer
                from appbot.vision.capture import ScreenCapture
                analyzer = VisionAnalyzer(
                    api_key=self.config.vision_api_key,
                    model=self.config.vision_model,
                )
                capture = ScreenCapture()
                vision_locator = VisionLocator(analyzer, capture)
                chain.add(vision_locator)
                context.vision_analyzer = analyzer
                context.screen_capture = capture
                log.debug("Vision locator enabled")
        except ImportError:
            log.debug("Vision locator not available")

        chain.add(CoordinateLocator())
        return chain

    def _setup_app(self, scenario: Scenario, context: RuntimeContext):
        """Launch or connect to the target application."""
        import subprocess as sp

        app_config = scenario.app
        uia = UIAWrapper(backend="uia")
        timeout = scenario.config.timeout

        if app_config.launch:
            log.info(f"Launching: {app_config.launch}")

            # Launch via subprocess (reliable for both Win32 and UWP apps)
            cmd = [app_config.launch] + (app_config.launch_args or [])
            context.process = sp.Popen(cmd, shell=True)
            time.sleep(2)

            # Determine title to search for
            title_hint = None
            if app_config.wait_for_window:
                title_hint = (app_config.wait_for_window.title_contains
                              or app_config.wait_for_window.title_exact)
            if not title_hint:
                title_hint = os.path.splitext(os.path.basename(app_config.launch))[0]

            # Connect to the window by title
            uia.connect_by_title(title=title_hint, timeout=timeout)

        elif app_config.window_title:
            uia.connect_by_title(
                title=app_config.window_title,
                timeout=timeout,
            )
        elif app_config.process_name:
            uia.connect_by_process(
                process_name=app_config.process_name,
                timeout=timeout,
            )
        else:
            raise RuntimeError("No app launch/connect configuration specified")

        # Wait for window
        if app_config.wait_for_window:
            wfw = app_config.wait_for_window
            uia.wait_for_window(
                title_contains=wfw.title_contains,
                title_exact=wfw.title_exact,
                class_name=wfw.class_name,
                timeout=wfw.timeout,
            )

        context.uia_wrapper = uia
        context.active_window = uia.get_window()
        log.info(f"Window ready: {context.active_window.window_text()}")

        # Focus the window
        try:
            context.active_window.set_focus()
        except Exception:
            pass
        time.sleep(0.3)

    def _execute_step(self, step: Step, locator_chain: LocatorChain,
                      context: RuntimeContext, scenario: Scenario) -> StepResult:
        """Execute a single step: locate target -> perform action."""
        start = time.time()
        result = StepResult(action=step.action)

        # Resolve params
        params = {}
        if step.params:
            params = step.params.model_dump(exclude_none=True)
            # Resolve variables in text/value params
            for key in ("text", "expected", "value", "filename"):
                if key in params and isinstance(params[key], str):
                    params[key] = context.resolve_variables(params[key])

        # Locate element if target specified
        element = None
        if step.target:
            retry_count = scenario.config.retry_count
            retry_delay = scenario.config.retry_delay

            for attempt in range(retry_count + 1):
                try:
                    element = locator_chain.locate(step.target, context)
                    break
                except ElementNotFoundException:
                    if attempt < retry_count:
                        log.debug(f"    Retry {attempt+1}/{retry_count}...")
                        time.sleep(retry_delay)
                    else:
                        # For assert/not_exists actions, element not found is ok
                        if step.action == "assert" and params.get("type") == "not_exists":
                            element = None
                        else:
                            result.passed = False
                            result.error_message = f"Element not found: {step.target}"
                            result.duration = time.time() - start
                            return result

        # Execute action
        action_result = self.action_executor.execute(
            step.action, element, params, context
        )
        result.passed = action_result.passed
        result.error_message = action_result.error_message
        result.element_found = action_result.element_found

        # Screenshot after step (if enabled)
        if scenario.config.screenshot_on_step:
            should_capture = step.screenshot if step.screenshot is not None else True
            if should_capture and step.action not in ("screenshot", "wait"):
                try:
                    self._auto_screenshot(context)
                except Exception:
                    pass

        result.duration = time.time() - start
        return result

    def _teardown(self, scenario: Scenario, locator_chain: LocatorChain,
                  context: RuntimeContext):
        """Run teardown steps."""
        if not scenario.teardown:
            return

        log.info("Running teardown...")
        for step in scenario.teardown:
            try:
                log.info(f"  Teardown: {step.name or step.action}")
                params = step.params.model_dump(exclude_none=True) if step.params else {}
                element = None
                if step.target:
                    try:
                        element = locator_chain.locate(step.target, context)
                    except Exception:
                        pass

                self.action_executor.execute(step.action, element, params, context)
            except Exception as e:
                log.warning(f"  Teardown error: {e}")

    def _capture_failure_screenshot(self, context: RuntimeContext, step_index: int):
        """Capture screenshot on failure."""
        try:
            from PIL import ImageGrab
            filename = f"failure_step{step_index}_{datetime.now().strftime('%H%M%S')}.png"
            path = os.path.join(context.screenshot_dir, filename)
            img = ImageGrab.grab()
            img.save(path)
            context.screenshots.append(path)
            log.info(f"    Failure screenshot: {path}")
        except Exception:
            pass

    def _auto_screenshot(self, context: RuntimeContext):
        """Auto-capture screenshot after each step."""
        try:
            from PIL import ImageGrab
            filename = f"step_{context.step_index}_{datetime.now().strftime('%H%M%S%f')[:10]}.png"
            path = os.path.join(context.screenshot_dir, filename)
            if context.active_window:
                try:
                    rect = context.active_window.rectangle()
                    img = ImageGrab.grab(bbox=(rect.left, rect.top, rect.right, rect.bottom))
                except Exception:
                    img = ImageGrab.grab()
            else:
                img = ImageGrab.grab()
            img.save(path)
            context.screenshots.append(path)
        except Exception:
            pass

    def _generate_report(self, result: ScenarioResult, context: RuntimeContext) -> Optional[str]:
        """Generate HTML report."""
        try:
            from appbot.report.html_report import HtmlReportGenerator
            generator = HtmlReportGenerator()
            timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
            filename = f"{result.scenario_name.replace(' ', '_')}_{timestamp}.html"
            output_path = os.path.join(context.report_dir, filename)
            generator.generate(result, output_path)
            return output_path
        except Exception as e:
            log.warning(f"Report generation failed: {e}")
            return None

    @staticmethod
    def _get_python_version() -> str:
        import sys
        return f"{sys.version_info.major}.{sys.version_info.minor}.{sys.version_info.micro}"

    @staticmethod
    def _get_os_info() -> str:
        import platform
        return f"{platform.system()} {platform.release()} ({platform.architecture()[0]})"
