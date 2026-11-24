#!/usr/bin/env bash
# One CASE line per well-known AI CLI after an install step (owner 2026-11-22).
# RED proof: a head commit whose message carries the skip token (see the workflows) skips the install steps, so every CASE line must FAIL.
#   CASE ai-cli-<name> <PASS|FAIL|INFO> path=<resolved> version-rc=<rc> [reason]
# REQUIRED (env, space separated, default "claude codex gemini qwen") must resolve and answer --version with rc 0.
# agy is not in the installer's AI_CLIS list: always INFO, never fails the leg.
# Exit 1 with a one-line reason when a required CLI is missing.
set -u
REQUIRED="${AI_CLI_REQUIRED:-claude codex gemini qwen}"
win=0; case "$(uname -s)" in MINGW*|MSYS*|CYGWIN*) win=1 ;; esac
missing=""
for c in claude codex gemini qwen agy; do
  p="$(command -v "$c" 2>/dev/null || true)"
  if [ -z "$p" ] && [ "$win" -eq 1 ]; then p="$(where.exe "$c" 2>/dev/null | tr -d '\r' | head -1)"; fi
  rc=na
  if [ -n "$p" ]; then
    if command -v timeout >/dev/null 2>&1; then timeout 90 "$c" --version >/dev/null 2>&1; rc=$?; else "$c" --version >/dev/null 2>&1; rc=$?; fi
  fi
  req=0; case " $REQUIRED " in *" $c "*) req=1 ;; esac
  if [ "$req" -eq 1 ]; then
    if [ -n "$p" ] && [ "$rc" = 0 ]; then v=PASS; else v=FAIL; missing="$missing $c"; fi
  else v=INFO; fi
  echo "CASE ai-cli-$c $v path=${p:-none} version-rc=$rc"
done
if [ -n "$missing" ]; then echo "FAIL: required AI CLI missing or --version failed:$missing"; exit 1; fi
echo "RESULT all required AI CLIs resolve ($REQUIRED)"
# qa-proof: prove kih main fa2ad2149a3d7fd6aac2d73f54c832cba9b106fc on windows and macos (full sha)
