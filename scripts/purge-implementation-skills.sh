#!/usr/bin/env bash
# Remove implementation-internal skills from public SDK.
# Rule: "App Use" patterns stay public; "how we built it" goes private.
set -e

cd D:/GitHub/wkappbot-sdk

# Implementation internals to remove (skill IDs without .skill.json suffix)
PURGE=(
  # Eye / MCP daemon architecture
  admin-eye-broadcast-close-handover
  admin-eye-init-test-verify-sudo-pipe-encoding-after-startup
  admin-eye-hot-swap-known-gap-options-for-fix
  mcp-admin-core-swap-elevation-triggered-graceful-cutover
  elevated-eye-admin-pipe-server
  eye-handoff-age-guard
  eye-tick-context-warned-dedup-persist
  eye-tick-elevated-pipe-parallel-accept
  eye-findallprompts-blocking-fix-cooldown-10s-mcp-warmup-guard
  eye-health-mousecca-storm
  eye-slack-thread-streaming-structure
  always-on-ai-automation-daemon-appbot-eye-internals
  think-eye-card-overlay
  elevated-proxy-subprocess-timeout-hasexited-check
  all-sudo-proxy-subprocess-timeout-hasexited-check

  # Sudo elevation internals
  sudo-elevation-four-layers
  sudo-proxy-cp949-stdout-fix-elevatedeyeproxy-standardoutputencoding-utf8

  # WkNotification + file scheduler (key IP)
  wknotification-unified-dispatcher-and-file-scheduler
  file-as-scheduler-pending-prompts-pattern

  # CONPTY / wkchat internals
  conpty-enter-intercept-two-phase-decide-then-commit
  wkchat-conpty-enter-intercept-rename-first-hardlink-live-swap
  wkchat-conpty-enter-intercept-two-phase

  # Hot-swap mechanism
  hot-swap-zombie-guard-kill-stale-wkappbot-core-workers-after-eye-swap
  hot-swap-stuck-orphan-mcp-workers-kill-force-swap

  # Launcher / IOCP / encoding internals
  launcher-direct-core-routing-for-critical-cmds
  launcher-sudo-cold-start-pipe-file-exists-guard
  launcher-aot-build-vswhere-path
  launcher-console-encoding-3-mode-policy-for-interactive-piped-utf-8-terminals
  core-worker-logging-launcher-chain
  pulsestep-force-mode-ring-trail
  iocp-stderr-default-buffered
  korean-argv-cp949-fix-tryrecoverutf8argv-at-main-entry-recovers-createprocessa-mangled-args
  ansi-color-output-auto-disable-on-pipe-no-color

  # Suggest system defense (anti-abuse mechanisms)
  suggest-safety-defense-system
  suggest-co-resolve-2of2
  suggest-cmd-guard-word-boundary
  suggest-submit-similarity-nudge
  suggest-requirement-error-format
  suggest-forward-acknowledge-and-close-stale-cross-system-forwards
  suggestions-jsonl-json-escape-guard

  # Internal infra / autoheal
  autoheal-userinput-protection
  claude-usage-slack-alert-state-file-dedup-after-reset
  skill-reminder-news-and-invocation-log-discoverability-system
  taskkill-compat-shim-a11y-first-kill-routing
)

removed=0
for id in "${PURGE[@]}"; do
  for f in skills/*/$id.skill.json; do
    if [ -f "$f" ]; then
      rm "$f"
      echo "  rm $f"
      removed=$((removed + 1))
    fi
  done
done

echo ""
echo "Purged: $removed implementation-internal skills"
echo "Remaining: $(find skills -name '*.skill.json' | wc -l) skills"
