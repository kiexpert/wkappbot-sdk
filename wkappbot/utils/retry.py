"""Retry and polling utilities."""

import time
from typing import Callable, Any, Optional
from functools import wraps

from appbot.config import log


def retry(max_attempts: int = 3, delay: float = 1.0, exceptions: tuple = (Exception,)):
    """
    Decorator: retry a function on failure.

    Args:
        max_attempts: Maximum number of attempts
        delay: Delay between attempts in seconds
        exceptions: Tuple of exception types to catch
    """
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            last_error = None
            for attempt in range(1, max_attempts + 1):
                try:
                    return func(*args, **kwargs)
                except exceptions as e:
                    last_error = e
                    if attempt < max_attempts:
                        log.debug(f"  Retry {attempt}/{max_attempts}: {e}")
                        time.sleep(delay)
            raise last_error
        return wrapper
    return decorator


def poll_until(condition: Callable[[], Any], timeout: float = 10.0,
               interval: float = 0.5, message: str = "Condition not met") -> Any:
    """
    Poll until a condition returns a truthy value.

    Args:
        condition: Callable that returns truthy when done
        timeout: Max wait time in seconds
        interval: Poll interval in seconds
        message: Error message if timeout

    Returns:
        The truthy value returned by condition

    Raises:
        TimeoutError if condition not met within timeout
    """
    deadline = time.time() + timeout
    last_error = None

    while time.time() < deadline:
        try:
            result = condition()
            if result:
                return result
        except Exception as e:
            last_error = e
        time.sleep(interval)

    error_msg = f"{message} (timeout={timeout}s)"
    if last_error:
        error_msg += f" [last error: {last_error}]"
    raise TimeoutError(error_msg)
