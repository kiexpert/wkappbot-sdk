"""Chrome state for runner desktops: start | save <stage> | restore <stage> | probe <url>
A copied profile alone does NOT keep logins on a fresh runner: the Cookies DB is encrypted with a DPAPI (Windows) or
Keychain (macOS) key bound to the old user and machine. So cookies travel through CDP instead:
  save    -- Storage.getCookies on the browser target (every domain, HttpOnly included, no filter) + open tab URLs,
             plus the profile minus its machine-bound secrets (Cookies / Login Data DBs, keys) -> <stage>/chrome/
  restore -- profile back into place BEFORE Chrome starts, then start Chrome with a localhost-only debug port,
             normalize the cookies (SameSite=None forces secure; the read-only fields session/size dropped),
             Storage.setCookies, then reopen the tabs. Prints "COOKIES_RESTORED=<n>".
  probe   -- open <url> in Chrome and print the page text (used by the proof to confirm a site still sees its cookie).
The stage directory is encrypted by snapshot.sh (AES-256, PBKDF2 key from the owner password) before it leaves the
runner; nothing here prints a cookie value.
"""
import base64, json, os, platform, shutil, socket, struct, subprocess, sys, time, urllib.request

PORT = 9222
HOME = os.path.expanduser("~")
PROFILE = os.path.join(HOME, "desktop-chrome")
# machine-bound or credential stores that cannot be decrypted on another machine -> never copied
SKIP = {"Cookies", "Cookies-journal", "Login Data", "Login Data-journal", "Login Data For Account", "Local State.bak",
        "Safe Browsing Network", "Network Persistent State", "GPUCache", "Code Cache", "Cache", "ShaderCache",
        "GrShaderCache", "Crashpad", "SingletonLock", "SingletonSocket", "SingletonCookie"}


def chrome_bin():
    if platform.system() == "Darwin":
        return "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
    for p in (r"C:\Program Files\Google\Chrome\Application\chrome.exe",
              r"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"):
        if os.path.exists(p):
            return p
    raise SystemExit("chrome not found")


def http_json(path):
    return json.load(urllib.request.urlopen(f"http://127.0.0.1:{PORT}{path}", timeout=10))


class WS:  # minimal RFC 6455 client (masked frames, fragments, 16/64-bit lengths) -- stdlib only
    def __init__(self, url):
        host_port, path = url[len("ws://"):].split("/", 1)
        h, p = host_port.split(":")
        self.s = socket.create_connection((h, int(p)), 30)
        key = base64.b64encode(os.urandom(16)).decode()
        self.s.sendall((f"GET /{path} HTTP/1.1\r\nHost: {host_port}\r\nUpgrade: websocket\r\nConnection: Upgrade\r\n"
                        f"Sec-WebSocket-Key: {key}\r\nSec-WebSocket-Version: 13\r\n\r\n").encode())
        buf = b""
        while b"\r\n\r\n" not in buf:
            buf += self.s.recv(4096)
        if b" 101 " not in buf.split(b"\r\n")[0]:
            raise SystemExit("CDP websocket refused")
        self.rest = buf.split(b"\r\n\r\n", 1)[1]
        self.n = 0

    def _read(self, k):
        while len(self.rest) < k:
            c = self.s.recv(65536)
            if not c:
                raise EOFError
            self.rest += c
        out, self.rest = self.rest[:k], self.rest[k:]
        return out

    def _frame(self):
        b0, b1 = self._read(2)
        ln = b1 & 0x7F
        if ln == 126:
            ln = struct.unpack(">H", self._read(2))[0]
        elif ln == 127:
            ln = struct.unpack(">Q", self._read(8))[0]
        return b0 & 0x80, b0 & 0x0F, self._read(ln)

    def call(self, method, **params):
        self.n += 1
        data = json.dumps({"id": self.n, "method": method, "params": params}).encode()
        mask = os.urandom(4)
        hdr = bytes([0x81])
        ln = len(data)
        hdr += bytes([0x80 | ln]) if ln < 126 else (bytes([0x80 | 126]) + struct.pack(">H", ln) if ln < 65536
                                                       else bytes([0x80 | 127]) + struct.pack(">Q", ln))
        self.s.sendall(hdr + mask + bytes(c ^ mask[i % 4] for i, c in enumerate(data)))
        while True:
            msg = b""
            while True:
                fin, op, payload = self._frame()
                msg += payload
                if fin:
                    break
            if op in (1, 0):
                m = json.loads(msg)
                if m.get("id") == self.n:
                    if "error" in m:
                        raise RuntimeError(f"{method}: {m['error'].get('message')}")
                    return m.get("result", {})


def browser():
    return WS(http_json("/json/version")["webSocketDebuggerUrl"])


def start(url=None):
    try:
        http_json("/json/version")
        return
    except Exception:
        pass
    os.makedirs(PROFILE, exist_ok=True)
    args = [chrome_bin(), f"--user-data-dir={PROFILE}", f"--remote-debugging-port={PORT}",
            "--remote-debugging-address=127.0.0.1", "--no-first-run", "--no-default-browser-check",
            "--password-store=basic", "--restore-last-session"]
    if url:
        args.append(url)
    subprocess.Popen(args, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
    for _ in range(60):
        try:
            http_json("/json/version")
            return
        except Exception:
            time.sleep(1)
    raise SystemExit("chrome debug port never answered")


def copy_profile(src, dst):
    def ignore(_d, names):
        return [n for n in names if n in SKIP]
    if os.path.isdir(src):
        shutil.copytree(src, dst, ignore=ignore, dirs_exist_ok=True)


def save(stage):
    out = os.path.join(stage, "chrome")
    os.makedirs(out, exist_ok=True)
    cookies, tabs = [], []
    try:
        b = browser()
        cookies = b.call("Storage.getCookies").get("cookies", [])
        tabs = [t["url"] for t in http_json("/json/list") if t.get("type") == "page" and t.get("url", "").startswith("http")]
    except Exception as e:
        print(f"chrome not reachable over CDP ({e.__class__.__name__}); profile only")
    json.dump(cookies, open(os.path.join(out, "cookies.json"), "w"))
    json.dump(tabs, open(os.path.join(out, "tabs.json"), "w"))
    copy_profile(PROFILE, os.path.join(out, "profile"))
    print(f"chrome saved: {len(cookies)} cookies, {len(tabs)} tabs, profile (machine-bound stores excluded)")
    print(f"COOKIES_SAVED={len(cookies)}")


def normalize(c):
    c = {k: v for k, v in c.items() if k not in ("size", "session", "priority", "sameParty", "sourceScheme",
                                                  "sourcePort", "partitionKey", "partitionKeyOpaque")}
    if c.get("sameSite") == "None":
        c["secure"] = True
    if c.get("expires", -1) in (-1, 0) or c.get("expires", 0) < 0:
        c.pop("expires", None)          # session cookie -> stays a session cookie
    return c


def restore(stage):
    src = os.path.join(stage, "chrome")
    if not os.path.isdir(src):
        start()
        print("COOKIES_RESTORED=0")
        return
    copy_profile(os.path.join(src, "profile"), PROFILE)       # before Chrome starts
    start()
    cookies = json.load(open(os.path.join(src, "cookies.json")))
    b = browser()
    ok = 0
    norm = [normalize(c) for c in cookies]
    try:
        b.call("Storage.setCookies", cookies=norm)
        ok = len(norm)
    except Exception:
        for c in norm:                   # one bad cookie must not drop the rest
            try:
                b.call("Storage.setCookies", cookies=[c])
                ok += 1
            except Exception:
                pass
    have = len(b.call("Storage.getCookies").get("cookies", []))
    for u in json.load(open(os.path.join(src, "tabs.json"))):
        b.call("Target.createTarget", url=u)
    print(f"chrome restored: {ok}/{len(cookies)} cookies set ({have} in the jar), tabs reopened")
    print(f"COOKIES_RESTORED={ok}")


def probe(url):
    start()
    b = browser()
    tid = b.call("Target.createTarget", url=url)["targetId"]
    time.sleep(4)
    sid = b.call("Target.attachToTarget", targetId=tid, flatten=True)["sessionId"]
    # flat-mode command on the page session
    b.n += 1
    data = json.dumps({"id": b.n, "sessionId": sid, "method": "Runtime.evaluate",
                       "params": {"expression": "document.body.innerText", "returnByValue": True}}).encode()
    mask = os.urandom(4)
    ln = len(data)
    hdr = bytes([0x81]) + (bytes([0x80 | ln]) if ln < 126 else bytes([0x80 | 126]) + struct.pack(">H", ln))
    b.s.sendall(hdr + mask + bytes(c ^ mask[i % 4] for i, c in enumerate(data)))
    want = b.n
    while True:
        msg = b""
        while True:
            fin, _op, payload = b._frame()
            msg += payload
            if fin:
                break
        m = json.loads(msg)
        if m.get("id") == want:
            print(m.get("result", {}).get("result", {}).get("value", ""))
            break
    b.call("Target.closeTarget", targetId=tid)


def set_cookie(name, value, domain):
    start()
    browser().call("Storage.setCookies", cookies=[{"name": name, "value": value, "domain": domain, "path": "/",
                                                   "secure": True, "httpOnly": True, "sameSite": "Lax",
                                                   "expires": time.time() + 86400 * 30}])


if __name__ == "__main__":
    cmd = sys.argv[1]
    {"start": lambda: start(*sys.argv[2:3]), "save": lambda: save(sys.argv[2]), "restore": lambda: restore(sys.argv[2]),
     "probe": lambda: probe(sys.argv[2]), "set-cookie": lambda: set_cookie(*sys.argv[2:5])}[cmd]()
