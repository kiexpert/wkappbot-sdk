#!/usr/bin/env bash
# SLEEP / WAKE snapshot for a runner desktop. Usage: snapshot.sh save|restore|idle
# Hosted runners cannot hibernate RAM, so "sleep" = save what can be relaunched, then let the runner end:
#   saved:     the work folder ~/desktop-work, the list of open GUI apps, their window rectangles (Windows)
#   NOT saved: RAM / unsaved in-app state, browser profiles (cookies are credentials), anything outside the work folder
# The archive is AES-256 encrypted (key = HMAC of the relay token, never stored) before it reaches actions/cache,
# because caches of a public repo are not a private store. Output: $SNAP_DIR/snap.enc (save) / restore report.
# idle: prints "<viewers> <input_idle_seconds>" (viewers = live VNC sessions through websockify).
set -euo pipefail
cmd=${1:?save|restore|idle}
HERE=$(cd "$(dirname "$0")" && pwd)
SNAP_DIR="${SNAP_DIR:-${RUNNER_TEMP:-/tmp}/desktop-snap}"; mkdir -p "$SNAP_DIR"
WORK="$HOME/desktop-work"; mkdir -p "$WORK"
PY=${PY:-python3}
key() { "$PY" -c 'import hmac,hashlib,os;print(hmac.new(os.environ["TOK"].encode(),b"runner-desktop-snapshot/1",hashlib.sha256).hexdigest())'; }
case "$(uname -s)" in Darwin) OSN=macos;; *) OSN=windows;; esac
win() { powershell.exe -NoProfile -ExecutionPolicy Bypass -File "$(cygpath -w "$HERE/snapshot-win.ps1")" "$@"; }

case "$cmd" in
  idle)
    if [ $OSN = macos ]; then
      v=$(netstat -an -p tcp | grep -cE '127\.0\.0\.1\.5900 +127\.0\.0\.1\.[0-9]+ +ESTABLISHED' || true)
      i=$(ioreg -c IOHIDSystem | awk '/HIDIdleTime/{print int($NF/1000000000); exit}')
    else
      v=$(netstat -an | grep -cE '127\.0\.0\.1:5900 +127\.0\.0\.1:[0-9]+ +ESTABLISHED' || true)
      i=$(win idle | tr -d '\r')
    fi
    echo "${v:-0} ${i:-0}" ;;
  save)
    t0=$(date +%s); st="$SNAP_DIR/stage"; rm -rf "$st"; mkdir -p "$st"
    if [ $OSN = macos ]; then
      osascript -e 'tell application "System Events" to get name of every application process whose background only is false' \
        | tr ',' '\n' | sed 's/^ *//' | grep -vxE 'Finder|Terminal|Screen Sharing|loginwindow' > "$st/apps.txt" || true
    else
      win save "$(cygpath -w "$st/apps.json")"
    fi
    tar -C "$HOME" -czf "$st/work.tgz" desktop-work
    tar -C "$st" -cf - . | openssl enc -aes-256-cbc -pbkdf2 -salt -pass "pass:$(key)" -out "$SNAP_DIR/snap.enc"
    rm -rf "$st"
    echo "snapshot saved: $(wc -c < "$SNAP_DIR/snap.enc") bytes in $(( $(date +%s) - t0 ))s" ;;
  restore)
    [ -f "$SNAP_DIR/snap.enc" ] || { echo "no snapshot: fresh boot"; exit 0; }
    t0=$(date +%s); st="$SNAP_DIR/stage"; rm -rf "$st"; mkdir -p "$st"
    openssl enc -d -aes-256-cbc -pbkdf2 -pass "pass:$(key)" -in "$SNAP_DIR/snap.enc" | tar -C "$st" -xf - \
      || { echo "snapshot unreadable (token rotated?): fresh boot"; exit 0; }
    tar -C "$HOME" -xzf "$st/work.tgz"
    n=0
    if [ $OSN = macos ]; then
      while IFS= read -r a; do [ -n "$a" ] && open -a "$a" 2>/dev/null && n=$((n+1)); done < "$st/apps.txt" || true
    else
      n=$(win restore "$(cygpath -w "$st/apps.json")" | tr -d '\r' | tail -1)
    fi
    echo "restored: work folder ($(find "$WORK" -type f | wc -l | tr -d ' ') files), $n apps relaunched in $(( $(date +%s) - t0 ))s"
    echo "RESTORED_APPS=$n" >> "${GITHUB_ENV:-/dev/null}" ;;
esac
