#!/bin/sh
# m22-alpine.sh -- M22: alpine:3.20, node from apk, busybox sh only, no python3, no pwsh.
# Run by the workflow inside the container:  sh /qa/harsh/m22-alpine.sh
# Env in: QA_OUT (default /qa-result), VERSION (registry road). Mounts: /qa/tgz (packed tgz, may be empty), /qa/harsh (scripts).
HG_LEG=m22-alpine; export HG_LEG
QA_OUT=${QA_OUT:-/qa-result}; export QA_OUT
HERE=$(cd "$(dirname "$0")" && pwd)
. "$HERE/common.sh"

rm -f "$QA_OUT/qa-result-$HG_LEG"
HOME=/tmp/qa-home; export HOME
mkdir -p "$HOME"
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

TGZ=$(ls /qa/tgz/*.tgz 2>/dev/null | head -n 1)
if [ -n "$TGZ" ]; then SRC="$TGZ"; else SRC="wkappbot-code${VERSION:+@$VERSION}"; fi
hg_res install_source "$SRC"

npm i -g "$SRC" > "$QA_OUT/m22-npm.log" 2>&1
hg_check npm_global_install "$?"

wkappbot-code install > "$QA_OUT/m22-install.log" 2>&1
irc=$?
hg_res install_rc "$irc"
hg_check wkappbot_code_install "$irc"
tail -n 25 "$QA_OUT/m22-install.log" | sed 's/^/INSTALL| /'

hg_tail_checks

hg_finish
exit $?
