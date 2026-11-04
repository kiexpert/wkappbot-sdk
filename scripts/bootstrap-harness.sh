#!/usr/bin/env bash
# Linux, tokenless harness bootstrap for cloud ROOT sessions.
#
# WHY: setup.ps1 is Windows-only (it exits on a non-Windows OS), so new cloud (Linux) ROOT sessions
# were coming up WITHOUT the harness on PATH -- "roots running wild": no wkappbot CLI, no wkdoctor,
# no guards. This script installs the harness from the ALREADY-PRESENT public wkappbot-sdk checkout
# with NO secret, NO token, NO `git pull`, and NO PowerShell, then lets the repo's own wkdoctor
# self-repair the CLI/symlinks. The environment Setup script should call it once, e.g.:
#     bash scripts/bootstrap-harness.sh "$GITHUB_WORKSPACE"   # or the repo root path
#
# Idempotent: re-running is a no-op except the wkdoctor heal probe.
set -euo pipefail

# Resolve the repo root: explicit arg, else the parent of this script's dir.
REPO_ROOT="${1:-}"
if [ -z "$REPO_ROOT" ]; then
  REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
fi
BIN="$REPO_ROOT/bin"

echo "[..] harness bootstrap: repo root = $REPO_ROOT"
if [ ! -d "$BIN" ]; then
  echo "[FAIL] bin/ not found at $BIN -- is this the wkappbot-sdk checkout?" >&2
  exit 1
fi

# 1) Put the harness bin on PATH for this shell and for future shells (idempotent, no duplicates).
case ":${PATH}:" in
  *":${BIN}:"*) echo "[skip] bin already on PATH" ;;
  *) export PATH="${BIN}:${PATH}"; echo "[ok] bin added to PATH (this shell)" ;;
esac
for rc in "$HOME/.bashrc" "$HOME/.profile"; do
  [ -e "$rc" ] || : > "$rc"
  if ! grep -qsF "wkappbot-sdk/bin" "$rc" && ! grep -qsF "$BIN" "$rc"; then
    printf '\n# wkappbot harness\nexport PATH="%s:$PATH"\n' "$BIN" >> "$rc"
    echo "[ok] PATH line appended to $rc"
  fi
done

# 2) Make the shell harness tools executable (git may not preserve +x in every clone path).
chmod +x "$BIN"/*.sh 2>/dev/null || true
[ -d "$BIN/wkappbot.hq" ] && find "$BIN/wkappbot.hq" -type f -name '*.sh' -exec chmod +x {} + 2>/dev/null || true

# 3) Let the repo's own doctor self-repair the harness (wkappbot/claude/codex busybox symlinks,
#    guards, pace). wkdoctor is the SDK-distributed heal tool; a non-zero here is a WARN, not fatal,
#    so a single failing check never bricks session creation.
if [ -x "$BIN/wkdoctor.sh" ]; then
  echo "[..] running wkdoctor self-heal"
  if bash "$BIN/wkdoctor.sh"; then
    echo "[ok] wkdoctor heal complete"
  else
    echo "[warn] wkdoctor reported issues (exit $?) -- harness installed but review the output above"
  fi
else
  echo "[warn] $BIN/wkdoctor.sh not found -- harness bin is on PATH but self-heal was skipped"
fi

# 4) Quick visibility: show what the new root can now reach (best-effort, never fatal).
echo "[..] harness entrypoints now resolvable:"
for t in wkdoctor.sh wkharness.sh wkask.sh wkclaude.sh wkcodex.sh; do
  if [ -e "$BIN/$t" ]; then echo "    - $t"; fi
done

echo "[OK] harness bootstrap complete (tokenless, Linux-native). New shells get the harness on PATH."
