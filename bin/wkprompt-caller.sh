#!/bin/bash
powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass \
    -File "$(cd "$(dirname "$0")" && pwd -W)/wkprompt-caller.ps1" "$@"
