"""CLI entry point for AppBot."""

import argparse
import sys
import os

from appbot import __version__
from appbot.config import setup_logging, log


def cmd_run(args):
    """Execute a test scenario."""
    from appbot.core.runner import ScenarioRunner
    from appbot.config import AppBotConfig

    config = AppBotConfig(
        verbose=args.verbose,
        screenshot_dir=args.screenshot_dir,
        report_dir=args.report,
    )
    runner = ScenarioRunner(config)
    result = runner.run(args.scenario)

    if result.passed:
        log.info(f"[PASS] SCENARIO PASSED ({result.passed_count}/{result.total_count} steps) in {result.duration:.1f}s")
    else:
        log.error(f"[FAIL] SCENARIO FAILED ({result.passed_count}/{result.total_count} steps) in {result.duration:.1f}s")

    if result.report_path:
        log.info(f"Report: {result.report_path}")

    return 0 if result.passed else 1


def cmd_inspect(args):
    """Dump accessibility tree of a running application."""
    from appbot.accessibility.inspector import AccessibilityInspector

    inspector = AccessibilityInspector()
    try:
        tree = inspector.dump_tree(
            target=args.target,
            depth=args.depth,
            fmt=args.format,
        )
        if args.output:
            with open(args.output, 'w', encoding='utf-8') as f:
                f.write(tree)
            log.info(f"Tree saved to: {args.output}")
        else:
            print(tree)
    except Exception as e:
        log.error(f"Inspect failed: {e}")
        return 1
    return 0


def cmd_capture(args):
    """Capture screenshot and optionally analyze with Claude Vision."""
    from appbot.vision.capture import ScreenCapture
    from appbot.input.window import WindowManager

    wm = WindowManager()
    capture = ScreenCapture()

    try:
        hwnd = wm.find_window(args.target)
        if not hwnd:
            log.error(f"Window not found: {args.target}")
            return 1

        screenshot_path = capture.capture_window_by_hwnd(
            hwnd, save_path=args.output
        )
        log.info(f"Screenshot saved: {screenshot_path}")

        if args.analyze:
            from appbot.vision.analyzer import VisionAnalyzer
            api_key = os.environ.get("ANTHROPIC_API_KEY")
            if not api_key:
                log.error("ANTHROPIC_API_KEY not set. Cannot analyze.")
                return 1
            analyzer = VisionAnalyzer(api_key=api_key)
            result = analyzer.describe_screen(screenshot_path)
            print(result)
    except Exception as e:
        log.error(f"Capture failed: {e}")
        return 1
    return 0


def cmd_validate(args):
    """Validate a YAML scenario file."""
    from appbot.scenario.parser import ScenarioParser
    success = ScenarioParser.validate(args.scenario)
    return 0 if success else 1


def main():
    parser = argparse.ArgumentParser(
        prog="appbot",
        description=f"AppBot v{__version__} - Windows App UI Automation Test Framework",
    )
    parser.add_argument("--version", action="version", version=f"appbot {__version__}")
    subparsers = parser.add_subparsers(dest="command", help="Available commands")

    # --- run ---
    run_p = subparsers.add_parser("run", help="Execute a test scenario")
    run_p.add_argument("scenario", help="Path to YAML scenario file")
    run_p.add_argument("--report", default="output/reports", help="Report output directory")
    run_p.add_argument("--screenshot-dir", default="output/screenshots", help="Screenshot directory")
    run_p.add_argument("-v", "--verbose", action="store_true", help="Verbose logging")

    # --- inspect ---
    ins_p = subparsers.add_parser("inspect", help="Dump accessibility tree")
    ins_p.add_argument("target", help="Window title or process name")
    ins_p.add_argument("--format", choices=["json", "text", "tree"], default="tree")
    ins_p.add_argument("--depth", type=int, default=10, help="Tree traversal depth")
    ins_p.add_argument("-o", "--output", help="Save output to file")

    # --- capture ---
    cap_p = subparsers.add_parser("capture", help="Screenshot + optional Vision analysis")
    cap_p.add_argument("target", help="Window title or process name")
    cap_p.add_argument("--analyze", action="store_true", help="Run Claude Vision analysis")
    cap_p.add_argument("-o", "--output", help="Output file path")

    # --- validate ---
    val_p = subparsers.add_parser("validate", help="Validate a scenario YAML file")
    val_p.add_argument("scenario", help="Path to YAML scenario file")

    args = parser.parse_args()

    if not args.command:
        parser.print_help()
        return 0

    # Setup logging
    verbose = getattr(args, 'verbose', False)
    setup_logging(verbose)

    # Dispatch to command handler
    handlers = {
        "run": cmd_run,
        "inspect": cmd_inspect,
        "capture": cmd_capture,
        "validate": cmd_validate,
    }
    return handlers[args.command](args)


if __name__ == "__main__":
    sys.exit(main())
