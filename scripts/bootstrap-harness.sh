#!/usr/bin/env bash
# Single-entry, Linux, (mostly) tokenless harness setup for cloud ROOT sessions.
#
# WHY: setup.ps1 is Windows-only, so new cloud (Linux) ROOT sessions came up without the harness --
# no wkappbot CLI, no guards ("roots running wild"). This one script finishes EVERYTHING a root needs
# that can be done from the PUBLIC checkout with no secret: npm deps, PATH, the tokenless wkappbot
# skill-reader, and (when the PowerShell brain is present) pwsh + wkdoctor self-heal.
#
# Wire it from the environment Setup script (runs once at root creation), e.g.:
#     bash scripts/bootstrap-harness.sh "$PWD"
# or add an npm hook so `npm install` runs it (see package.json proposal in the PR).
#
# Idempotent. Optional steps are best-effort and never fatal; only a missing bin/ is fatal.
#
# NOTE: the FULL harness (eye/cdp/wkdoctor) needs the PowerShell brain bin/wkwrap.ps1, which lives in
# the PRIVATE kih source and is NOT in the public SDK. Until a root's setup installs that brain (with a
# token, team-owned), this script still gives every root a working `wkappbot skill read/list/search`
# via the tokenless fallback, so no root is ever "wild" for skills.
set -euo pipefail

REPO_ROOT="${1:-}"
[ -n "$REPO_ROOT" ] || REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
BIN="$REPO_ROOT/bin"
echo "[..] harness setup: repo root = $REPO_ROOT"
[ -d "$BIN" ] || { echo "[FAIL] bin/ not found at $BIN -- not a wkappbot-sdk checkout?" >&2; exit 1; }

# 1) npm deps (the root runs npm install anyway; do it here so setup is one place). Best-effort.
if [ -f "$REPO_ROOT/package.json" ] && command -v npm >/dev/null 2>&1; then
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

echo "[OK] harness setup complete. Skills always work; full harness (eye/cdp/wkdoctor) activates once the private wkwrap.ps1 brain + pwsh are installed by the root's setup (team-owned)."
