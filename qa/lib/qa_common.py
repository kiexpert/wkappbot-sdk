"""Shared helpers for the all-platform QA suite.

Every repo location is a parameter. Resolution order (the cross-platform rule,
identical on every OS):  env var -> sibling checkout next to this sdk repo ->
historical Windows default D:/GitHub/<repo>.  (qa:allow-path)
"""
import json
import os
import platform
import shlex
import shutil
import subprocess
import sys
import time
from pathlib import Path

PASS, FAIL, SKIP = "PASS", "FAIL", "SKIP"

QA_DIR = Path(__file__).resolve().parent.parent
SDK_ROOT = QA_DIR.parent
FIXTURES = QA_DIR / "fixtures"


def platform_key():
    s = platform.system().lower()
    if s.startswith("win"):
        return "windows"
    if s == "darwin":
        return "macos"
    return "linux"


def home_dir():
    """USERPROFILE then HOME (never USERPROFILE only)."""
    return os.environ.get("USERPROFILE") or os.environ.get("HOME") or str(Path.home())


def resolve_repo(env_name, repo_dir_name, override=None):
    """env var -> sibling checkout -> D:/GitHub/<repo>. Returns (path|None, source)."""
    candidates = []
    if override:
        # An explicit --*-root is the answer, found or not; it never falls through to a sibling.
        return (Path(override).resolve(), "arg") if Path(override).is_dir() else (None, f"not found (arg {override})")
    if os.environ.get(env_name):
        candidates.append((os.environ[env_name], "env " + env_name))
    candidates.append((str(SDK_ROOT.parent / repo_dir_name), "sibling"))
    candidates.append((str(Path("D:/GitHub") / repo_dir_name), "windows-default"))
    for p, src in candidates:
        if p and Path(p).is_dir():
            return Path(p).resolve(), src
    return None, "not found (tried: " + ", ".join(p for p, _ in candidates) + ")"


def resolve_cli(override, wkappbot_root):
    """WKAPPBOT_CLI -> built Linux/Windows CLI under WKAPPBOT_ROOT -> bare name on PATH."""
    cands = [override, os.environ.get("WKAPPBOT_CLI")]
    if wkappbot_root:
        r = Path(wkappbot_root)
        cands += [
            r / "csharp/src/WKAppBot.CLI.Linux/bin/publish/wkappbot",
            r / "csharp/src/WKAppBot.CLI.Linux/publish/wkappbot",
            r / "bin" / "wkappbot",
            r / "bin" / "wkappbot.exe",
        ]
    for c in cands:
        if c and Path(c).is_file():
            return str(Path(c).resolve())
    for name in ("wkappbot", "wkappbot-core"):
        found = shutil.which(name)
        if found:
            return found
    return None


def python_cmd():
    """PYTHON_CMD then python then python3; fall back to this interpreter."""
    for c in (os.environ.get("PYTHON_CMD"), "python", "python3"):
        if c and shutil.which(c):
            return c
    return sys.executable


class Result:
    def __init__(self, area, name, status, detail="", ms=None, cause=""):
        self.area, self.name, self.status = area, name, status
        self.detail, self.ms, self.cause = detail, ms, cause

    def to_dict(self):
        return {k: getattr(self, k) for k in ("area", "name", "status", "detail", "ms", "cause")}


class Ctx:
    def __init__(self, args):
        self.args = args
        self.platform = platform_key()
        self.kih_root, self.kih_src = resolve_repo("KIH_ROOT", "wkappbot-kih", args.kih_root)
        self.wk_root, self.wk_src = resolve_repo("WKAPPBOT_ROOT", "WKAppBot", args.wkappbot_root)
        self.cli = resolve_cli(args.cli, self.wk_root)
        self.transcript = args.transcript or os.environ.get("TRANSCRIPT")
        self.hook_budget_ms = int(os.environ.get("QA_HOOK_BUDGET_MS", "1000"))
        self.allow_mutations = args.allow_mutations or os.environ.get("QA_ALLOW_MUTATIONS") == "1"
        self.timeout = int(os.environ.get("QA_TIMEOUT_S", "60"))

    def describe(self):
        return {
            "platform": self.platform,
            "sdk_root": str(SDK_ROOT),
            "kih_root": f"{self.kih_root} ({self.kih_src})",
            "wkappbot_root": f"{self.wk_root} ({self.wk_src})",
            "cli": self.cli,
            "transcript": self.transcript,
            "hook_budget_ms": self.hook_budget_ms,
            "allow_mutations": self.allow_mutations,
        }


def run(cmd, cwd=None, env=None, stdin=None, timeout=60, shell=False):
    """Run a command; returns (rc, stdout, stderr, ms). Never raises on rc/timeout."""
    t0 = time.perf_counter()
    full_env = dict(os.environ)
    if env:
        full_env.update(env)
    try:
        p = subprocess.run(
            cmd, cwd=cwd, env=full_env, input=stdin, timeout=timeout, shell=shell,
            stdout=subprocess.PIPE, stderr=subprocess.PIPE,
        )
        rc, out, err = p.returncode, p.stdout, p.stderr
    except subprocess.TimeoutExpired as e:
        rc, out, err = 124, e.stdout or b"", (e.stderr or b"") + b"\n[qa] TIMEOUT"
    except OSError as e:
        rc, out, err = 127, b"", str(e).encode()
    ms = int((time.perf_counter() - t0) * 1000)
    dec = lambda b: b.decode("utf-8", "replace") if isinstance(b, (bytes, bytearray)) else (b or "")
    return rc, dec(out), dec(err), ms


def first_line(*texts, limit=200):
    for t in texts:
        for line in (t or "").splitlines():
            if line.strip():
                return line.strip()[:limit]
    return ""


def load_json(path):
    with open(path, encoding="utf-8") as f:
        return json.load(f)


def split_cmd(s):
    return shlex.split(s, posix=True)
