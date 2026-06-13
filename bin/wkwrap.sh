#!/usr/bin/env bash
# wkwrap.sh -- thin bash shim for the wkwrap universal resolver (skill: wkwrap).
# Invoked via a tool symlink (e.g. codex.sh -> wkwrap.sh): forwards the invocation
# name + all args to the single wkwrap.ps1 brain.
SELF="$(basename "$0" .sh)"
DIR="$(cd "$(dirname "$(realpath "$0")")" && pwd)"
if [ "$SELF" = "wkwrap" ]; then
    echo "wkwrap: universal symlink resolver. Symlink a tool name to me (codex.sh -> wkwrap.sh) and I route to wkwrap.ps1."
    exit 0
fi
exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "${DIR}/wkwrap.ps1" "$SELF" "$@"
# (gate-validation probe -- safe to revert)
