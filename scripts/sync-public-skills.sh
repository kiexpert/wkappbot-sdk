#!/usr/bin/env bash
# Copy public-safe skills from dev WKAppBot to wkappbot-sdk with secret redaction.
# Idempotent: skips files that already exist with same redacted content.
set -e

SRC=D:/GitHub/WKAppBot/bin/wkappbot.hq/skills
DST=D:/GitHub/wkappbot-sdk/skills

# Categories that contain only public-safe content
CATEGORIES=(wkappbot wkappbot-core wkappbot-workflow wkappbot-webbot wkappbot-debug claude-desktop competitive)

# SKIP if filename matches these patterns (Korean trading / private projects + implementation internals)
SKIP_PATTERNS='kiwoom|hantoo|kis-web|super-ant|wkautoquant|nfrunlite|nkrunlite|heroes4|hero4|hero-global|tuhon|xingq|nlrc|joodeok|account-trading|sugeup|super-ant'
# Implementation-internal: "how we built it" -- not "how to use it"
IMPL_PATTERNS='admin-eye-|elevated-eye-|elevated-proxy|all-sudo-proxy|eye-handoff-age|eye-tick-elevated|eye-tick-context-warned|eye-findallprompts|eye-health-mouse|eye-slack-thread-streaming|mcp-admin-core-swap|always-on-ai-automation-daemon-appbot-eye|think-eye-card|sudo-elevation-four-layers|sudo-proxy-cp949|wknotification-unified-dispatcher|file-as-scheduler-pending|conpty-enter-intercept|wkchat-conpty|hot-swap-zombie-guard|hot-swap-stuck-orphan|launcher-direct-core-routing|launcher-sudo-cold-start|launcher-aot-build-vswhere|launcher-console-encoding-3-mode|core-worker-logging-launcher|pulsestep-force-mode|iocp-stderr-default-buffered|korean-argv-cp949|ansi-color-output-auto-disable|suggest-safety-defense|suggest-co-resolve-2of2|suggest-cmd-guard|suggest-submit-similarity|suggest-requirement-error|suggest-forward-acknowledge|suggestions-jsonl-json-escape|autoheal-userinput-protection|claude-usage-slack-alert-state|skill-reminder-news-and-invocation|taskkill-compat-shim-a11y'

# SKIP if content matches (Korean trading terms / personal info)
CONTENT_SKIP_REGEX='키움|한투|영웅문|HTS|MTS\b|nfrunlite|nkrunlite|heroes4|kiwoom-condition|super-ant|wkautoquant|hantoo|kiexpert@naver|dcmmentary@naver'

copied=0; skipped=0; redacted=0
mkdir -p "$DST"

for cat in "${CATEGORIES[@]}"; do
  src_dir="$SRC/$cat"
  dst_dir="$DST/$cat"
  [ -d "$src_dir" ] || continue
  mkdir -p "$dst_dir"

  for src_file in "$src_dir"/*.skill.json; do
    [ -f "$src_file" ] || continue
    name=$(basename "$src_file")
    dst_file="$dst_dir/$name"

    # Filename-based skip (private domain)
    if echo "$name" | grep -iqE "$SKIP_PATTERNS"; then
      skipped=$((skipped + 1))
      continue
    fi

    # Filename-based skip (implementation internals -- "how it's built", not "how to use")
    if echo "$name" | grep -iqE "$IMPL_PATTERNS"; then
      skipped=$((skipped + 1))
      continue
    fi

    # Content-based skip
    if grep -qE "$CONTENT_SKIP_REGEX" "$src_file"; then
      skipped=$((skipped + 1))
      continue
    fi

    # Redact secrets and internal paths
    redacted_content=$(sed \
      -e 's|D:\\\\GitHub\\\\WKAppBot|<repo-root>|g' \
      -e 's|D:\\GitHub\\WKAppBot|<repo-root>|g' \
      -e 's|D:/GitHub/WKAppBot|<repo-root>|g' \
      -e 's|D:\\\\SDK|<repo-root>|g' \
      -e 's|C:\\\\Users\\\\kiexp|<user-home>|g' \
      -e 's|C:\\Users\\kiexp|<user-home>|g' \
      -e 's|C:/Users/kiexp|<user-home>|g' \
      -e 's|kiexpert@kivilab\.co\.kr|<maintainer-email>|g' \
      -e 's|kiexpert@naver\.com|<contact-redacted>|g' \
      -e 's|dcmmentary@naver\.com|<contact-redacted>|g' \
      -e 's|xoxb-[a-zA-Z0-9-]\+|xoxb-REDACTED|g' \
      -e 's|xapp-[a-zA-Z0-9-]\+|xapp-REDACTED|g' \
      "$src_file")

    # Write only if changed (idempotent)
    if [ -f "$dst_file" ] && [ "$(cat "$dst_file")" = "$redacted_content" ]; then
      continue
    fi

    echo "$redacted_content" > "$dst_file"
    if [ "$redacted_content" != "$(cat "$src_file")" ]; then
      redacted=$((redacted + 1))
    fi
    copied=$((copied + 1))
  done
done

echo ""
echo "=== Sync result ==="
echo "  copied:    $copied"
echo "  redacted:  $redacted"
echo "  skipped:   $skipped (private content)"
