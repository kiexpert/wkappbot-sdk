"""Gate proof for a booted runner desktop. Exit 0 only if every check holds.
Checks via the PUBLIC tunnel URL: unauthenticated page, root and websocket are all refused (401);
with the password the noVNC page loads (200) and the websocket hands back an RFB banner from the VNC server."""
import base64, os, socket, ssl, sys, urllib.request, urllib.error
from urllib.parse import urlparse
d = os.environ.get("DESKTOP_DIR") or os.path.join(os.environ.get("RUNNER_TEMP", "/tmp"), "desktop")
base = open(os.path.join(d, "base")).read().strip()
tok = open(os.path.join(d, "token")).read().strip()
pw = os.environ["DESKTOP_PASSWORD"]
auth = "Basic " + base64.b64encode(f"owner:{pw}".encode()).decode()
bad = "Basic " + base64.b64encode(b"owner:wrong-password-x").decode()
fails = []

def http(path, hdr=None):
    r = urllib.request.Request(base + path, headers=hdr or {})
    try:
        return urllib.request.urlopen(r, timeout=20).status
    except urllib.error.HTTPError as e:
        return e.code

def ws(path, hdr):
    u = urlparse(base); ctx = ssl.create_default_context()
    s = ctx.wrap_socket(socket.create_connection((u.hostname, 443), 20), server_hostname=u.hostname)
    key = base64.b64encode(os.urandom(16)).decode()
    req = (f"GET {path} HTTP/1.1\r\nHost: {u.hostname}\r\nUpgrade: websocket\r\nConnection: Upgrade\r\n"
           f"Sec-WebSocket-Key: {key}\r\nSec-WebSocket-Version: 13\r\nSec-WebSocket-Protocol: binary\r\n")
    if hdr: req += f"Authorization: {hdr}\r\n"
    s.sendall((req + "\r\n").encode()); s.settimeout(20)
    buf = b""
    while b"\r\n\r\n" not in buf:
        c = s.recv(4096)
        if not c: break
        buf += c
    head, _, rest = buf.partition(b"\r\n\r\n")
    code = int(head.split(b" ")[1]) if head else 0
    banner = b""
    if code == 101:
        while len(rest) < 2 + 12: rest += s.recv(4096)
        n = rest[1] & 0x7F; banner = rest[2:2 + n]
    s.close()
    return code, banner

def check(name, got, want):
    ok = got == want
    print(f"{'PASS' if ok else 'FAIL'} {name}: {got} (want {want})")
    if not ok: fails.append(name)

page = f"/{tok}/vnc.html"
check("unauth noVNC page", http(page), 401)
check("unauth root", http("/"), 401)
check("wrong-password page", http(page, {"Authorization": bad}), 401)
check("unauth websocket", ws(f"/{tok}/websockify", None)[0], 401)
check("authed noVNC page", http(page, {"Authorization": auth}), 200)
code, banner = ws(f"/{tok}/websockify", auth)
check("authed websocket", code, 101)
check("RFB banner", banner[:4], b"RFB ")
print("viewer gate:", "OK" if not fails else "FAILED " + ",".join(fails))
sys.exit(1 if fails else 0)
