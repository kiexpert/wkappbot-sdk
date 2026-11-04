#!/usr/bin/env bash
# Single-entry, Linux, (mostly) tokenless harness setup for cloud ROOT sessions.
#
# WHY: setup.ps1 is Windows-only, so new cloud (Linux) ROOT sessions came up without the harness --
# no wkappbot CLI, no guards ("roots running wild"). This one script finishes EVERYTHING a root needs:
# the PUBLIC/tokenless parts (npm deps, PATH, the tokenless wkappbot skill-reader) always, and the FULL
# harness brain when this environment's git credentials can reach the private kih source (path B, step 6).
#
# Wire it from the environment Setup script (runs once at root creation), e.g.:
#     bash scripts/bootstrap-harness.sh "$PWD"
# It also runs automatically from package.json `postinstall`, so a bare `npm install` sets up a root.
#
# Idempotent. Optional steps are best-effort and never fatal; only a missing bin/ is fatal.
#
# NOTE: the FULL harness (eye/cdp/wkdoctor) needs the PowerShell brain bin/wkwrap.ps1, which lives in the
# PRIVATE kih source and is NEVER vendored into the public SDK. Step 6 installs it from kih when reachable;
# otherwise every root still gets a working `wkappbot skill read/list/search` via the tokenless fallback,
# so no root is ever "wild" for skills.
set -euo pipefail

REPO_ROOT="${1:-}"
[ -n "$REPO_ROOT" ] || REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
BIN="$REPO_ROOT/bin"
echo "[..] harness setup: repo root = $REPO_ROOT"
[ -d "$BIN" ] || { echo "[FAIL] bin/ not found at $BIN -- not a wkappbot-sdk checkout?" >&2; exit 1; }

# 1) npm deps (the root runs npm install anyway; do it here so setup is one place). Best-effort.
# Skip when we are ourselves running inside an npm lifecycle (e.g. invoked from the postinstall hook):
# npm is already installing deps, and re-running `npm install` here would recurse infinitely.
if [ -n "${npm_lifecycle_event:-}" ]; then
  echo "[skip] npm install (already inside npm '$npm_lifecycle_event' -- avoiding re-entry)"
elif [ -f "$REPO_ROOT/package.json" ] && command -v npm >/dev/null 2>&1; then
  echo "[..] npm install"
  ( cd "$REPO_ROOT" && npm install --no-audit --no-fund --silent ) && echo "[ok] npm deps installed" \
    || echo "[warn] npm install reported issues (non-fatal)"
else
  echo "[skip] npm install (no package.json or npm) "
fi

# 2) PATH + executables (idempotent).
case ":${PATH}:" in *":${BIN}:"*) echo "[skip] bin already on PATH" ;; *) export PATH="${BIN}:${PATH}"; echo "[ok] bin on PATH (this shell)";; esac
for rc in "$HOME/.bashrc" "$HOME/.profile"; do
  [ -e "$rc" ] || : > "$rc"
  grep -qsF "$BIN" "$rc" || { printf '\n# wkappbot harness\nexport PATH="%s:$PATH"\n' "$BIN" >> "$rc"; echo "[ok] PATH appended to $rc"; }
done
chmod +x "$BIN"/*.sh 2>/dev/null || true
chmod +x "$BIN/wkappbot" 2>/dev/null || true
[ -d "$BIN/wkappbot.hq" ] && find "$BIN/wkappbot.hq" -type f -name '*.sh' -exec chmod +x {} + 2>/dev/null || true

# 3) PowerShell brain prerequisite -- install pwsh ONLY when the brain is present (or forced), since
#    pwsh is useless without bin/wkwrap.ps1. Best-effort, idempotent, tokenless (public release).
if [ -f "$BIN/wkwrap.ps1" ] || [ "${WKAPPBOT_INSTALL_PWSH:-}" = "1" ]; then
  if ! command -v powershell >/dev/null 2>&1; then
    echo "[..] installing PowerShell (pwsh) -- brain present or forced"
    PV="7.4.6"; arch="$(uname -m)"; case "$arch" in x86_64) a=x64;; aarch64|arm64) a=arm64;; *) a="";; esac
    if [ -n "$a" ] && curl -fsSL "https://github.com/PowerShell/PowerShell/releases/download/v${PV}/powershell-${PV}-linux-${a}.tar.gz" -o /tmp/pwsh.tgz 2>/dev/null; then
      mkdir -p "$HOME/.local/pwsh" "$HOME/.local/bin"
      tar -xzf /tmp/pwsh.tgz -C "$HOME/.local/pwsh" && chmod +x "$HOME/.local/pwsh/pwsh"
      ln -sf "$HOME/.local/pwsh/pwsh" "$HOME/.local/bin/pwsh"
      ln -sf "$HOME/.local/pwsh/pwsh" "$HOME/.local/bin/powershell"
      case ":${PATH}:" in *":$HOME/.local/bin:"*) : ;; *) export PATH="$HOME/.local/bin:$PATH";; esac
      grep -qsF '.local/bin' "$HOME/.bashrc" || printf '\nexport PATH="$HOME/.local/bin:$PATH"\n' >> "$HOME/.bashrc"
      echo "[ok] pwsh installed: $("$HOME/.local/bin/pwsh" --version 2>/dev/null)"
    else
      echo "[warn] pwsh download skipped/failed (arch=$arch) -- full harness will stay unavailable (non-fatal)"
    fi
  else
    echo "[skip] pwsh already present"
  fi
else
  echo "[note] bin/wkwrap.ps1 (private kih brain) absent -- skipping pwsh; skill CLI works via the tokenless fallback"
fi

# 4) Self-heal via the real doctor only when the brain can actually run.
if [ -f "$BIN/wkwrap.ps1" ] && command -v powershell >/dev/null 2>&1 && [ -x "$BIN/wkdoctor.sh" ]; then
  echo "[..] wkdoctor self-heal"
  bash "$BIN/wkdoctor.sh" && echo "[ok] wkdoctor done" || echo "[warn] wkdoctor reported issues (non-fatal)"
else
  echo "[note] skipping wkdoctor (needs pwsh + private bin/wkwrap.ps1)"
fi

# 5) Sanity: the skill CLI must work on every root, brain or not.
if [ -x "$BIN/wkappbot" ]; then
  n="$("$BIN/wkappbot" skill list 2>/dev/null | wc -l | tr -d ' ')"
  echo "[ok] wkappbot skill CLI ready (tokenless): $n skills readable"
else
  echo "[warn] bin/wkappbot not found -- skill CLI fallback missing"
fi

# 6) Full harness brain (PRIVATE kih) -- path B (owner decision via spokesman, 2026-11-04). The public SDK
#    NEVER vendors wkwrap.ps1 (that would publish private code). Instead: if THIS environment's git
#    credentials can reach kih, clone it and let kih's own installer set up the full harness and verify it.
#    If unreachable, we stay on the tokenless skill CLI. Fully guarded -- never fails npm install.
KIH_URL="https://github.com/kiexpert/wkappbot-kih.git"
KIH_DIR="${WKAPPBOT_KIH_DIR:-$HOME/wkappbot-kih}"
if git ls-remote "$KIH_URL" >/dev/null 2>&1; then
  echo "[..] private harness reachable -- installing the full brain from kih"
  if [ ! -d "$KIH_DIR/.git" ]; then
    git clone -q --depth 1 "$KIH_URL" "$KIH_DIR" || echo "[warn] kih clone failed (non-fatal)"
  fi
  if [ -f "$KIH_DIR/scripts/npm-postinstall.sh" ]; then
    sh "$KIH_DIR/scripts/npm-postinstall.sh" || echo "[warn] kih installer reported issues (non-fatal)"
  else
    echo "[warn] kih reachable but scripts/npm-postinstall.sh missing (non-fatal)"
  fi
else
  echo "[wk-boot] private harness unreachable: tokenless skills only"
fi

echo "[OK] harness setup complete. Skills always work via the tokenless CLI; the full harness (eye/cdp/wkdoctor) activates in step 6 whenever this environment can reach the private kih brain."
