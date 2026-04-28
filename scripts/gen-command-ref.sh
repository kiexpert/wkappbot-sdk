#!/usr/bin/env bash
# gen-command-ref.sh -- regenerate docs/wkappbot-commands.md from the live binary
# Usage: bash scripts/gen-command-ref.sh [WKAPPBOT_CORE_EXE]
# Default exe: bin/wkappbot-core.exe (relative to repo root)

set -euo pipefail
cd "$(dirname "$0")/.."

CORE="${1:-bin/wkappbot-core.exe}"
OUT="docs/wkappbot-commands.md"
export WKAPPBOT_WORKER=1

if [[ ! -f "$CORE" ]]; then
  echo "ERROR: $CORE not found -- run build.cmd first" >&2; exit 1
fi

VER=$("$CORE" --version 2>/dev/null | grep -o 'v[0-9.]*' | head -1)

cat > "$OUT" <<HEADER
# WKAppBot Command Reference

> Auto-generated from \`wkappbot-core.exe $VER\`
> Regenerate: \`bash scripts/gen-command-ref.sh\`

Generated: $(date -u +%Y-%m-%d)

## Coverage legend (smoke-help.ps1)

| Symbol | Meaning |
|--------|---------|
| ✅ | 3+ tests |
| ⚠️ | 1-2 tests |
| ❌ | No tests yet |

---

## Global flags (apply to all commands)

\`\`\`
--timeout <duration>    Hard kill after N time (e.g. 30, 2m, 500ms)
--no-regression         Skip regression self-tests
--help                  Show help for a command
--version               Print version and exit
--nth N                 Target the Nth match (1-based)
--nth N~                Target the Nth match onwards (all remaining)
--all                   Target all matches
--worker / WKAPPBOT_WORKER=1  Suppress Eye auto-spawn
--sudo                  Elevate to admin (UAC prompt)
--stderr                Show buffered stderr (launcher relay)
\`\`\`

---

HEADER

CMDS=(a11y file skill schedule eye license suggest ask slack logcat gc validate windows run speak claude-usage mcp win-move newchat)

for cmd in "${CMDS[@]}"; do
  echo "## \`wkappbot $cmd\`" >> "$OUT"
  echo "" >> "$OUT"
  echo '```' >> "$OUT"
  "$CORE" "$cmd" --help 2>/dev/null | head -50 >> "$OUT" || true
  echo '```' >> "$OUT"
  echo "" >> "$OUT"
  echo "---" >> "$OUT"
  echo "" >> "$OUT"
done

echo "[OK] Generated $OUT ($(wc -l < "$OUT") lines, binary=$VER)"
