#!/bin/sh
# Fake claude binary for unit tests. Records its argv, echoes the prompt back.
# FAKE_CLAUDE_ARGS: file to append argv to. FAKE_CLAUDE_FAIL=1: exit 1.
[ -n "$FAKE_CLAUDE_ARGS" ] && printf '%s\n' "$*" >> "$FAKE_CLAUDE_ARGS"
[ "$FAKE_CLAUDE_FAIL" = 1 ] && { echo "boom" >&2; exit 1; }
prompt=$(cat)
printf 'fake-haiku: %s\n' "$prompt"
