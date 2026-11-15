#!/bin/sh
# m23-readonly.sh -- M23: node:20-alpine under  docker run --read-only --tmpfs /tmp:size=190m,exec
# HOME and the npm prefix live on that small tmpfs. The install must either succeed under the user prefix,
# or fail LOUD: its last line names the remedy, and the user's settings.json stays byte-identical (nothing half-wired).
HG_LEG=m23-readonly; export HG_LEG
QA_OUT=${QA_OUT:-/qa-result}; export QA_OUT
HERE=$(cd "$(dirname "$0")" && pwd)
. "$HERE/common.sh"

rm -f "$QA_OUT/qa-result-$HG_LEG"
HOME=/tmp/qa-home; export HOME
mkdir -p "$HOME/.claude"
npm_config_prefix=/tmp/npm-prefix; export npm_config_prefix
npm_config_cache=/tmp/npm-cache; export npm_config_cache
mkdir -p "$npm_config_prefix" "$npm_config_cache"
PATH="/tmp/npm-prefix/bin:$HOME/.local/bin:$PATH"; export PATH

hg_res leg "$HG_LEG"
hg_res version_asked "${VERSION:-latest}"

if touch /usr/qa-probe >/dev/null 2>&1; then rm -f /usr/qa-probe; ro=1; else ro=0; fi
hg_check usr_read_only "$ro"

FREE=$(df -Pm /tmp | awk 'NR==2{print $4}')
hg_res tmpfs_free_mb "${FREE:-0}"
if [ "${FREE:-0}" -lt 200 ]; then tight=0; else tight=1; fi
hg_check tmpfs_under_200mb_free "$tight"

# seed a user settings.json that must survive a failed install byte for byte
printf '%s\n' '{"theme":"dark","hooks":{}}' > "$HOME/.claude/settings.json"
cp "$HOME/.claude/settings.json" /tmp/settings.seed

TGZ=$(ls /qa/tgz/*.tgz 2>/dev/null | head -n 1)
if [ -n "$TGZ" ]; then SRC="$TGZ"; else SRC="wkappbot-code${VERSION:+@$VERSION}"; fi
hg_res install_source "$SRC"

npm i -g "$SRC" > "$QA_OUT/m23-npm.log" 2>&1
nrc=$?
hg_res npm_global_rc "$nrc"
hg_check npm_global_install "$nrc"
[ "$nrc" = 0 ] || tail -n 15 "$QA_OUT/m23-npm.log" | sed 's/^/NPM| /'

wkappbot-code install > "$QA_OUT/m23-install.log" 2>&1
irc=$?
hg_res install_rc "$irc"
tail -n 25 "$QA_OUT/m23-install.log" | sed 's/^/INSTALL| /'

if [ "$irc" = 0 ]; then
  hg_res outcome success
  hg_tail_checks
else
  hg_res outcome loud_refusal
  last=$(tail -n 1 "$QA_OUT/m23-install.log")
  case $last in
    *remedy*|*"re-run wkappbot-code install"*) r=0 ;;
    *) r=1 ;;
  esac
  hg_check last_line_names_remedy "$r"
  hg_res last_line "$(printf '%s' "$last" | cut -c1-160)"
  cmp -s /tmp/settings.seed "$HOME/.claude/settings.json"
  hg_check settings_byte_identical "$?"
  wk=$(grep -c wkharness "$HOME/.claude/settings.json" 2>/dev/null)
  if [ "${wk:-0}" = 0 ]; then w=0; else w=1; fi
  hg_check no_half_wired_hooks "$w"
fi

hg_finish
exit $?
