#!/usr/bin/env bash
# wkhippo.sh -- bash shim that invokes the SDK-local wkhippo.ps1 brain.
# Vendored into wkappbot-sdk/bin: routes directly to the sibling wkhippo.ps1
# (no wkwrap resolver needed -- this is a standalone copy).
DIR="$(cd "$(dirname "$(realpath "$0")")" && pwd)"
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "${DIR}/wkhippo.ps1" "$@"