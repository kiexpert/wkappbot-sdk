#!/usr/bin/env bash
# All-platform QA launcher (Linux / macOS / Git Bash). Python: PYTHON_CMD -> python3 -> python.
set -u
here="$(cd "$(dirname "$0")" && pwd)"
py="${PYTHON_CMD:-}"
if [ -z "$py" ]; then
  if command -v python3 >/dev/null 2>&1; then py=python3; else py=python; fi
fi
export PYTHONIOENCODING=utf-8
exec "$py" "$here/run-qa.py" "$@"
