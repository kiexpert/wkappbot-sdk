#!/usr/bin/env bash
# wkwrap.sh -- universal bash->PS1 relay + .sh/.cmd auto-install (harness-managed)
# RELAY (called as wkXXX.sh): routes to wkXXX.ps1 via powershell
# MAINTENANCE (called as wkwrap.sh): --install / --status / help
SELF="$(basename "$0" .sh)"
DIR="$(dirname "$(realpath "$0")")"
if [ "$SELF" != "wkwrap" ]; then
    exec powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass \
         -File "${DIR}/${SELF}.ps1" "$@"
fi
case "${1:-}" in
--install)
    sh_count=0; cmd_count=0
    for ps1 in "$DIR"/wk*.ps1; do
        base="$(basename "$ps1" .ps1)"
        [ "$base" = "wkwrap" ] && continue
        sh="${DIR}/${base}.sh"; cmd="${DIR}/${base}.cmd"
        if [ ! -f "$sh" ] && [ ! -L "$sh" ]; then ln -s wkwrap.sh "$sh" && chmod +x "$sh"; echo "  [sh]  $base.sh"; ((sh_count++)); fi
        if [ ! -f "$cmd" ] && [ ! -L "$cmd" ]; then ln -s wkwrap.cmd "$cmd"; echo "  [cmd] $base.cmd"; ((cmd_count++)); fi
    done
    echo "Done. $sh_count .sh + $cmd_count .cmd created.";;
--status)
    for ps1 in "$DIR"/wk*.ps1; do
        base="$(basename "$ps1" .ps1)"; [ "$base" = "wkwrap" ] && continue
        s="$([ -f "${DIR}/${base}.sh"  ] && echo OK || echo --)"; c="$([ -f "${DIR}/${base}.cmd" ] && echo OK || echo --)"
        printf "  %-20s .sh:%-3s .cmd:%s\n" "$base" "$s" "$c"
    done;;
*)
    echo "wkwrap.sh --install | --status | (called as wkXXX.sh -> relay to wkXXX.ps1)"
    echo "bash/Claude: wkXXX.sh  |  CMD/user: wkXXX.cmd  |  PS direct: wkXXX.ps1";;
esac