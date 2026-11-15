#!/bin/sh
# m22-alpine.sh -- M22: alpine:3.20, node from apk, busybox sh only, no python3, no pwsh, and at first NO curl.
# Phase A: install without curl must fail LOUD (last line names the remedy, nothing half-wired).
# Phase B: apk add curl, re-run the install, then the three tail checks (seven symbols, both --help, one Stop round trip).
# Run by the workflow inside the container:  sh /qa/harsh/m22-alpine.sh
# Env in: QA_OUT (default /qa-result), VERSION (registry road). Mounts: /qa/tgz (packed tgz, may be empty), /qa/harsh (scripts).
HG_LEG=m22-alpine; export HG_LEG
QA_OUT=${QA_OUT:-/qa-result}; export QA_OUT
HERE=$(cd "$(dirname "$0")" && pwd)
. "$HERE/common.sh"

rm -f "$QA_OUT/qa-result-$HG_LEG"
HOME=/tmp/qa-home; export HOME
mkdir -p "$HOME/.claude"
npm_config_prefix="$HOME/.npm-global"; export npm_config_prefix
PATH="$HOME/.npm-global/bin:$HOME/.local/bin:$PATH"; export PATH

hg_res leg "$HG_LEG"
hg_res version_asked "${VERSION:-latest}"

apk add --no-cache nodejs npm > "$QA_OUT/m22-apk.log" 2>&1
hg_check apk_node_npm "$?"

if command -v python3 >/dev/null 2>&1; then py=1; else py=0; fi
hg_check python3_absent "$py"
if command -v pwsh >/dev/null 2>&1; then ps=1; else ps=0; fi
hg_check pwsh_absent "$ps"
if command -v curl >/dev/null 2>&1; then cu=1; else cu=0; fi
hg_check curl_absent_at_start "$cu"

TGZ=$(ls /qa/tgz/*.tgz 2>/dev/null | head -n 1)
if [ -n "$TGZ" ]; then SRC="$TGZ"; else SRC="wkappbot-code${VERSION:+@$VERSION}"; fi
hg_res install_source "$SRC"

npm i -g "$SRC" > "$QA_OUT/m22-npm.log" 2>&1
hg_check npm_global_install "$?"

# ---- phase A: no curl ----
printf '%s\n' '{"theme":"dark","hooks":{}}' > "$HOME/.claude/settings.json"
cp "$HOME/.claude/settings.json" /tmp/settings.seed
wkappbot-code install > "$QA_OUT/m22-install-nocurl.log" 2>&1
arc=$?
hg_res nocurl_install_rc "$arc"
tail -n 12 "$QA_OUT/m22-install-nocurl.log" | sed 's/^/NOCURL| /'
last=$(tail -n 1 "$QA_OUT/m22-install-nocurl.log")
case $last in
  *curl*"apk add curl"*|*"install curl"*) r=0 ;;
  *) r=1 ;;
esac
hg_res nocurl_last_line "$(printf '%s' "$last" | cut -c1-160)"
hg_check nocurl_last_line_names_curl_remedy "$r"
if [ "$arc" != 0 ]; then a=0; else a=1; fi
hg_check nocurl_install_fails_loud "$a"
cmp -s /tmp/settings.seed "$HOME/.claude/settings.json"
hg_check nocurl_settings_byte_identical "$?"

# ---- phase B: curl installed ----
apk add --no-cache curl > "$QA_OUT/m22-apk-curl.log" 2>&1
hg_check apk_curl "$?"
wkappbot-code install > "$QA_OUT/m22-install.log" 2>&1
irc=$?
hg_res install_rc "$irc"
hg_check wkappbot_code_install "$irc"
tail -n 25 "$QA_OUT/m22-install.log" | sed 's/^/INSTALL| /'

hg_tail_checks

hg_finish
exit $?
