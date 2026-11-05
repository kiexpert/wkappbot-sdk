#!/usr/bin/env bash
# Runner desktop BOOT (one script for both roles: a QA job's live screen and the owner's viewer).
# Brings up: OS VNC server (password-gated) -> websockify + noVNC on 127.0.0.1 (HTTP basic auth on
# every request, noVNC under a random per-boot path) -> cloudflared quick tunnel to that port only.
# Inputs (env): DESKTOP_PASSWORD (required; never echoed), RUNNER_TEMP. Writes $DESKTOP_DIR/{url,token,pids}.
# Exit !=0 = boot failed. A QA step can source $DESKTOP_DIR/env and keep running beside the viewer:
# nothing here grabs input focus or restarts the session.
set -euo pipefail
: "${DESKTOP_PASSWORD:?DESKTOP_PASSWORD missing -- refusing to start an ungated desktop}"
[ "${#DESKTOP_PASSWORD}" -ge 12 ] || { echo "DESKTOP_PASSWORD must be >= 12 chars"; exit 2; }
DESKTOP_DIR="${DESKTOP_DIR:-${RUNNER_TEMP:-/tmp}/desktop}"
mkdir -p "$DESKTOP_DIR" && cd "$DESKTOP_DIR"
PY=python3; command -v python3 >/dev/null 2>&1 && python3 -c 1 2>/dev/null || PY=python
WEB_PORT=6080; VNC_PORT=5900
VNCPW="${DESKTOP_PASSWORD:0:8}"          # classic VNC auth uses 8 chars; the full secret gates HTTP
TOKEN=$($PY -c 'import secrets;print(secrets.token_urlsafe(24))')
t0=$(date +%s)

wait_port() { # host port seconds
  $PY - "$1" "$2" "$3" <<'P'
import socket,sys,time
h,p,s=sys.argv[1],int(sys.argv[2]),int(sys.argv[3]);end=time.time()+s
while time.time()<end:
    try: socket.create_connection((h,p),2).close(); sys.exit(0)
    except OSError: time.sleep(1)
sys.exit(1)
P
}

case "$(uname -s)" in
  Darwin)
    OSN=macos
    KS=/System/Library/CoreServices/RemoteManagement/ARDAgent.app/Contents/Resources/kickstart
    sudo "$KS" -activate -configure -access -on -clientopts -setvnclegacy -vnclegacy yes \
      -setvncpw -vncpw "$VNCPW" -restart -agent -privs -all >/dev/null
    CF_URL=https://github.com/cloudflare/cloudflared/releases/latest/download/cloudflared-darwin-arm64.tgz
    [ "$(uname -m)" = x86_64 ] && CF_URL=${CF_URL/arm64/amd64}
    curl -fsSL "$CF_URL" -o cf.tgz && tar xzf cf.tgz && rm cf.tgz && chmod +x cloudflared
    CF=./cloudflared ;;
  MINGW*|MSYS*|CYGWIN*)
    OSN=windows
    for v in 2.8.85 2.8.84 2.8.81; do
      curl -fsSL "https://www.tightvnc.com/download/$v/tightvnc-$v-gpl-setup-64bit.msi" -o tvnc.msi && break
    done
    # loopback-only server: the tunnel reaches it via websockify on 127.0.0.1, nothing listens on the LAN
    MSYS2_ARG_CONV_EXCL='*' msiexec.exe /i tvnc.msi /quiet /norestart ADDLOCAL=Server \
      SET_USEVNCAUTHENTICATION=1 VALUE_OF_USEVNCAUTHENTICATION=1 SET_PASSWORD=1 VALUE_OF_PASSWORD="$VNCPW" \
      SET_ALLOWLOOPBACK=1 VALUE_OF_ALLOWLOOPBACK=1 SET_LOOPBACKONLY=1 VALUE_OF_LOOPBACKONLY=1 \
      SET_USECONTROLAUTHENTICATION=1 VALUE_OF_USECONTROLAUTHENTICATION=1 SET_CONTROLPASSWORD=1 VALUE_OF_CONTROLPASSWORD="$VNCPW"
    rm -f tvnc.msi
    curl -fsSL https://github.com/cloudflare/cloudflared/releases/latest/download/cloudflared-windows-amd64.exe -o cloudflared.exe
    CF=./cloudflared.exe ;;
  *) echo "unsupported OS $(uname -s)"; exit 2 ;;
esac
wait_port 127.0.0.1 $VNC_PORT 90 || { echo "VNC server did not come up on :$VNC_PORT"; exit 3; }

# noVNC under the random path; the web root itself serves nothing but a 401 (basic auth) or an empty page
$PY -m pip install -q --disable-pip-version-check websockify==0.12.0
git clone -q --depth 1 -b v1.5.0 https://github.com/novnc/noVNC.git novnc-src
mkdir -p web && mv novnc-src "web/$TOKEN" && rm -rf "web/$TOKEN/.git"
printf '<!doctype html><title>desktop</title>\n' > web/index.html
AUTH_SRC="owner:$DESKTOP_PASSWORD"
nohup $PY -m websockify --web "$DESKTOP_DIR/web" \
  --auth-plugin websockify.auth_plugins.BasicHTTPAuth --auth-source "$AUTH_SRC" --web-auth \
  127.0.0.1:$WEB_PORT 127.0.0.1:$VNC_PORT > websockify.log 2>&1 &
echo $! > ws.pid
wait_port 127.0.0.1 $WEB_PORT 30 || { echo "websockify did not start"; tail -5 websockify.log; exit 4; }

nohup "$CF" tunnel --no-autoupdate --url "http://127.0.0.1:$WEB_PORT" > cloudflared.log 2>&1 &
echo $! > cf.pid
BASE=""
for _ in $(seq 1 60); do
  BASE=$(grep -oE 'https://[a-z0-9-]+\.trycloudflare\.com' cloudflared.log | head -1 || true)
  [ -n "$BASE" ] && break; sleep 2
done
[ -n "$BASE" ] || { echo "tunnel URL not obtained"; tail -5 cloudflared.log; exit 5; }
URL="$BASE/$TOKEN/vnc.html?autoconnect=1&resize=scale&path=$TOKEN/websockify"
echo "::add-mask::$BASE"; echo "::add-mask::$TOKEN"
printf '%s' "$URL" > url; printf '%s' "$BASE" > base; printf '%s' "$TOKEN" > token
{ echo "DESKTOP_OS=$OSN"; echo "DESKTOP_BOOT_S=$(( $(date +%s) - t0 ))"; } > env
echo "desktop up on $OSN in $(( $(date +%s) - t0 ))s (viewer URL kept out of the log)"
