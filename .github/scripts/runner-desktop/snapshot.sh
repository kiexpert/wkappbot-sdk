#!/usr/bin/env bash
# SLEEP / WAKE / HANDOVER snapshot for a runner desktop. Usage: snapshot.sh baseline|save|restore|idle
# Hosted runners cannot hibernate RAM, so a "sleep" or a 6h "reboot" keeps what can be put back on a fresh machine:
#   work folder   ~/desktop-work
#   Chrome        cookies of every domain via CDP (HttpOnly included) + open tabs + the profile (extensions, bookmarks,
#                 local storage); the machine-bound Cookies / Login Data DBs are not copied (they cannot be decrypted
#                 on another machine) -- the CDP cookie export replaces them. See chrome.py.
#   software      installed since boot. Windows: footprint-win.ps1 (Program Files / ProgramData dirs, uninstall + Run
#                 keys, services). macOS: new /Applications bundles, new LaunchAgents / LaunchDaemons plists.
#   apps          the open GUI apps (and their window rectangles on Windows), relaunched after the restore
# NOT kept: RAM / unsaved in-app state; kernel drivers / kernel or system extensions; machine-bound licences
#   (they are listed under "failed" in restore.json and the status record).
# The archive is AES-256-CBC encrypted, key = PBKDF2-SHA256 (600k iterations) of the owner password
# (RUNNER_DESKTOP_PASSWORD; proof runs: of the relay token so the next proof run can wake), passed via env, never argv.
# It reaches actions/cache only encrypted. Size guard: the per-repo cache limit is 10 GB; a snapshot over
# SNAP_MAX_MB (default 9000) drops the software layer first (work folder, Chrome and app list are kept).
# idle: prints "<viewers> <input_idle_seconds>" (viewers = live VNC sessions through websockify).
set -euo pipefail
cmd=${1:?baseline|save|restore|idle}
HERE=$(cd "$(dirname "$0")" && pwd)
SNAP_DIR="${SNAP_DIR:-${RUNNER_TEMP:-/tmp}/desktop-snap}"; mkdir -p "$SNAP_DIR"
WORK="$HOME/desktop-work"; mkdir -p "$WORK"
PY=${PY:-python3}
if [ "${DESKTOP_ROLE:-}" = proof ]; then export SNAP_PASS="$TOK"; else export SNAP_PASS="$DESKTOP_PASSWORD"; fi
ENC=(openssl enc -aes-256-cbc -pbkdf2 -md sha256 -iter 600000 -pass env:SNAP_PASS)
case "$(uname -s)" in Darwin) OSN=macos;; *) OSN=windows;; esac
fp()  { powershell.exe -NoProfile -ExecutionPolicy Bypass -File "$(cygpath -w "$HERE/footprint-win.ps1")" "$@"; }
win() { powershell.exe -NoProfile -ExecutionPolicy Bypass -File "$(cygpath -w "$HERE/snapshot-win.ps1")" "$@"; }
envout() { echo "$1" >> "${GITHUB_ENV:-/dev/null}"; }
MAC_SW_DIRS=(/Applications "$HOME/Library/LaunchAgents" /Library/LaunchAgents /Library/LaunchDaemons /Library/Extensions /Library/SystemExtensions)

case "$cmd" in
  baseline)
    if [ $OSN = macos ]; then
      for d in "${MAC_SW_DIRS[@]}"; do ls -1 "$d" 2>/dev/null | sed "s|^|$d/|"; done > "$SNAP_DIR/mac.baseline"
      echo "baseline recorded ($(wc -l < "$SNAP_DIR/mac.baseline" | tr -d ' ') entries)"
    else fp baseline; fi ;;

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
    t0=$(date +%s); st="$SNAP_DIR/stage"; rm -rf "$st"; mkdir -p "$st/sw"
    if [ $OSN = macos ]; then
      osascript -e 'tell application "System Events" to get name of every application process whose background only is false' \
        | tr ',' '\n' | sed 's/^ *//' | grep -vxE 'Finder|Terminal|Screen Sharing|loginwindow|Google Chrome' > "$st/apps.txt" || true
    else
      win save "$(cygpath -w "$st/apps.json")"
    fi
    tar -C "$HOME" -czf "$st/work.tgz" desktop-work
    "$PY" "$HERE/chrome.py" save "$st" | tee "$SNAP_DIR/chrome.save.log" | grep -v '^COOKIES_SAVED=' || true
    envout "$(grep '^COOKIES_SAVED=' "$SNAP_DIR/chrome.save.log" || echo COOKIES_SAVED=0)"
    if [ $OSN = macos ]; then
      for d in "${MAC_SW_DIRS[@]}"; do ls -1 "$d" 2>/dev/null | sed "s|^|$d/|"; done | grep -vxF -f "$SNAP_DIR/mac.baseline" > "$st/sw/new.txt" || true
      grep -E '^/Library/(Extensions|SystemExtensions)/' "$st/sw/new.txt" > "$st/sw/kernel.txt" || true
      grep -vE '^/Library/(Extensions|SystemExtensions)/' "$st/sw/new.txt" > "$st/sw/pack.txt" || true
      [ -s "$st/sw/pack.txt" ] && tr '\n' '\0' < "$st/sw/pack.txt" | sudo xargs -0 tar -czPf "$st/sw/sw.tgz"
      sudo chown "$(id -u)" "$st/sw/sw.tgz" 2>/dev/null || true
      echo "footprint: $(wc -l < "$st/sw/pack.txt" | tr -d ' ') new bundles/plists, $(wc -l < "$st/sw/kernel.txt" | tr -d ' ') kernel/system extensions (not replayable)"
    else
      fp save "$(cygpath -w "$st/sw")" | tr -d '\r'
    fi
    mb=$(du -sm "$st" | cut -f1)
    if [ "$mb" -gt "${SNAP_MAX_MB:-9000}" ]; then
      echo "::warning::snapshot ${mb} MB exceeds the cache budget -- software layer dropped (work folder, Chrome, app list kept)"
      rm -rf "$st/sw"; envout "SNAP_SW_DROPPED=$mb"
    fi
    envout "SNAP_MB=$(du -sm "$st" | cut -f1)"
    tar -C "$st" -cf - . | "${ENC[@]}" -salt -out "$SNAP_DIR/snap.enc"
    rm -rf "$st"
    echo "snapshot saved: $(( $(wc -c < "$SNAP_DIR/snap.enc") / 1048576 )) MB encrypted in $(( $(date +%s) - t0 ))s" ;;

  restore)
    [ -f "$SNAP_DIR/snap.enc" ] || { echo "no snapshot: fresh boot"; "$PY" "$HERE/chrome.py" start; exit 0; }
    t0=$(date +%s); st="$SNAP_DIR/stage"; rm -rf "$st"; mkdir -p "$st"
    "${ENC[@]}" -d -in "$SNAP_DIR/snap.enc" | tar -C "$st" -xf - \
      || { echo "snapshot unreadable (password changed?): fresh boot"; "$PY" "$HERE/chrome.py" start; exit 0; }
    tar -C "$HOME" -xzf "$st/work.tgz"
    # 1) software first, so Chrome and relaunched apps find their programs
    if [ -d "$st/sw" ]; then
      if [ $OSN = macos ]; then
        ok=(); fail=()
        if [ -f "$st/sw/sw.tgz" ] && sudo tar -xzPf "$st/sw/sw.tgz"; then
          while IFS= read -r p; do
            [ -n "$p" ] || continue
            case "$p" in
              */LaunchDaemons/*) sudo launchctl load -w "$p" 2>/dev/null && ok+=("$p") || fail+=("$p: launchctl load failed") ;;
              */LaunchAgents/*)  launchctl load -w "$p" 2>/dev/null && ok+=("$p") || fail+=("$p: launchctl load failed") ;;
              *) [ -e "$p" ] && ok+=("$p") || fail+=("$p: missing after unpack") ;;
            esac
          done < "$st/sw/pack.txt"
        fi
        while IFS= read -r p; do [ -n "$p" ] && fail+=("$p: kernel/system extension (not replayable)"); done < "$st/sw/kernel.txt"
        "$PY" -c 'import json,sys;a=sys.argv[1:];i=a.index("--");print(json.dumps({"restored":a[:i],"failed":a[i+1:]}))' \
          "${ok[@]+"${ok[@]}"}" -- "${fail[@]+"${fail[@]}"}" > "$SNAP_DIR/restore.json"
      else
        fp restore "$(cygpath -w "$st/sw")" | tr -d '\r' | tail -1 > "$SNAP_DIR/restore.json"
      fi
      "$PY" -c 'import json,sys;r=json.load(open(sys.argv[1]));r={k:(v if isinstance(v,list) else ([v] if v else [])) for k,v in r.items()};json.dump(r,open(sys.argv[1],"w"));print("software restored:",len(r["restored"]),"failed:",len(r["failed"]));[print("  OK",x) for x in r["restored"]];[print("  FAILED",f) for f in r["failed"]]' "$SNAP_DIR/restore.json"
    fi
    # 2) Chrome: profile back before Chrome starts, then the CDP cookie import and the tabs
    "$PY" "$HERE/chrome.py" restore "$st" | tee "$SNAP_DIR/chrome.restore.log" | grep -v '^COOKIES_RESTORED=' || true
    envout "$(grep '^COOKIES_RESTORED=' "$SNAP_DIR/chrome.restore.log" || echo COOKIES_RESTORED=0)"
    # 3) the other open apps
    n=0
    if [ $OSN = macos ]; then
      while IFS= read -r a; do [ -n "$a" ] && open -a "$a" 2>/dev/null && n=$((n+1)); done < "$st/apps.txt" || true
    else
      n=$(win restore "$(cygpath -w "$st/apps.json")" | tr -d '\r' | tail -1)
    fi
    rm -rf "$st"
    echo "restored: work folder ($(find "$WORK" -type f | wc -l | tr -d ' ') files), $n apps relaunched, all in $(( $(date +%s) - t0 ))s"
    envout "RESTORED_APPS=$n" ;;
esac
