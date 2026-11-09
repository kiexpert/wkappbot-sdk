#!/usr/bin/env bash
# Release gate for wkappbot-code (owner 2026-11-08: "npm goes to the team -- no mistakes").
# Refuses (exit 1) unless ALL hold:
#   G1 approved : VER is listed in the approved-versions file (a version is approved by a human, then listed)
#   G2 core     : the core in the SDK releases/latest (RELEASE_CORE) is >= the kih bin core (KIH_CORE)
#   G3 qa       : for every leg in LEGS, QA_DIR/<leg>.txt (written by the pre-publish wkappbot-code-os-qa run on the
#                 PACKED tgz) says install=0, doctor_fail=0, opencode=PASS, e2e=success
# Env: VER, RELEASE_CORE, KIH_CORE ("wkappbot vX.Y.Z ..." or "X.Y.Z"), QA_DIR, APPROVED_FILE, LEGS (space list).
set -u
ver() { printf '%s' "$1" | grep -oE '[0-9]+\.[0-9]+\.[0-9]+' | head -1; }
refused=0
gate() { if [ "$2" = ok ]; then echo "GATE PASS   $1: $3"; else echo "GATE REFUSE $1: $3"; refused=1; fi; }

: "${VER:?}" "${APPROVED_FILE:?}"
if grep -qxF "$VER" <(sed 's/#.*//; s/[[:space:]]//g' "$APPROVED_FILE" 2>/dev/null); then
  gate approved ok "$VER is in $(basename "$APPROVED_FILE")"
else
  gate approved no "$VER is not in $(basename "$APPROVED_FILE") -- a human approves a version by adding it there"
fi

r="$(ver "${RELEASE_CORE:-}")"; k="$(ver "${KIH_CORE:-}")"
if [ -z "$r" ] || [ -z "$k" ]; then
  gate core no "cannot read both core versions (release='${RELEASE_CORE:-}' kih='${KIH_CORE:-}')"
elif [ "$(printf '%s\n%s\n' "$k" "$r" | sort -V | tail -1)" = "$r" ]; then
  gate core ok "releases/latest core $r >= kih bin core $k"
else
  gate core no "releases/latest core $r < kih bin core $k -- an npm install would fetch an older core"
fi

for leg in ${LEGS:-linux-x64 osx-arm64 osx-x64 win-x64 win-x64-nopy}; do
  f="${QA_DIR:-.}/$leg.txt"
  if [ ! -f "$f" ]; then gate "qa $leg" no "no pre-publish QA result ($f)"; continue; fi
  get() { sed -n "s/^$1=//p" "$f" | head -1; }
  i="$(get install)" d="$(get doctor_fail)" o="$(get opencode)" e="$(get e2e)" v="$(get version)" p="$(get python)"
  # Windows legs carry the python contract (Store stub -> python installed; real python -> install skipped).
  pok=1; case "$leg" in win-*) [ "$p" = ok ] || pok=0 ;; esac
  if [ "$v" = "$VER" ] && [ "$i" = 0 ] && [ "$d" = 0 ] && [ "$o" = PASS ] && [ "$e" = success ] && [ "$pok" = 1 ]; then
    gate "qa $leg" ok "version=$v install=$i doctor_fail=$d opencode=$o e2e=$e python=${p:-none}"
  else
    gate "qa $leg" no "version=${v:-?} install=${i:-?} doctor_fail=${d:-?} opencode=${o:-?} e2e=${e:-?} python=${p:-?} (want $VER/0/0/PASS/success, python=ok on win-*)"
  fi
done

if [ "$refused" = 0 ]; then echo "GATE RESULT: PUBLISH $VER"; exit 0; fi
echo "GATE RESULT: REFUSED -- $VER is not published"; exit 1
