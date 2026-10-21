"""Repo hygiene checks that hold on every platform.

  sdk version sync       CLAUDE.md header == CHANGELOG top == VERSIONING current == README
                         highlights == csharp/Directory.Build.props base (Major.Minor)
  kih .githooks exec bit every tracked .githooks/* file is mode 100755 in git (else git
                         ignores it on Linux/macOS once core.hooksPath points there)
  AutoCommit porcelain   no Trim() on a porcelain status line before slicing the path
                         (Trim eats the leading space of " M path" -> wrong path, rc=1)
"""
import re
from pathlib import Path

from qa_common import FAIL, PASS, SDK_ROOT, SKIP, Result, run

VER = r"(\d+\.\d+\.\d+)"


def _grab(path, pattern):
    p = Path(path)
    if not p.is_file():
        return None
    m = re.search(pattern, p.read_text(encoding="utf-8", errors="replace"), re.M)
    return m.group(1) if m else None


def version_sync(root=SDK_ROOT):
    found = {
        "CLAUDE.md header": _grab(root / "CLAUDE.md", r"\A#[^\n]*?v" + VER),
        "CHANGELOG top": _grab(root / "CHANGELOG.md", r"^## \[" + VER),
        "VERSIONING current": _grab(root / "VERSIONING.md", r"^## Current version\s+`v" + VER),
        "README highlights": _grab(root / "README.md", r"^### v" + VER + r"\S* highlights"),
    }
    base = _grab(root / "csharp" / "Directory.Build.props", r"<WKAppBotBaseVersion>(\d+\.\d+)<")
    vals = {k: v for k, v in found.items() if v}
    missing = [k for k, v in found.items() if not v]
    top = found["CHANGELOG top"]
    bad = [f"{k}={v}" for k, v in vals.items() if v != top]
    if base and top and not top.startswith(base + "."):
        bad.append(f"Directory.Build.props base={base}")
    detail = ", ".join(f"{k}={v}" for k, v in vals.items()) + (f", props base={base}" if base else "")
    if bad or missing:
        cause = f"drift vs CHANGELOG top {top}: " + ", ".join(bad + [f"{m} not found" for m in missing])
        return Result("hygiene", "sdk version sync", FAIL, detail, cause=cause)
    return Result("hygiene", "sdk version sync", PASS, detail)


def githooks_exec(root):
    name = "kih .githooks executable in git"
    if root is None:
        return Result("hygiene", name, SKIP, "KIH_ROOT not found")
    rc, out, err, _ = run(["git", "-C", str(root), "ls-files", "-s", "--", ".githooks"])
    if rc != 0:
        return Result("hygiene", name, FAIL, "git ls-files", cause=err.strip()[:200])
    rows = [l.split(None, 3) for l in out.splitlines() if l.strip()]
    if not rows:
        return Result("hygiene", name, SKIP, "no tracked .githooks/ files")
    bad = [r[3] for r in rows if r[0] != "100755"]
    if bad:
        return Result("hygiene", name, FAIL, f"{len(bad)}/{len(rows)} not 100755",
                      cause=f"{bad[0]} is mode {rows[[r[3] for r in rows].index(bad[0])][0]}; "
                            f"fix: git update-index --chmod=+x .githooks/*")
    return Result("hygiene", name, PASS, f"{len(rows)} hook file(s) 100755")


RE_TRIM = re.compile(r"\.Trim\(\)")
RE_SLICE = re.compile(r"Substring\(\s*3\s*\)|\[\s*3\s*\.\.\s*\]")


def autocommit_trim(root):
    name = "AutoCommit porcelain parse keeps leading space"
    if root is None:
        return Result("hygiene", name, SKIP, "WKAPPBOT_ROOT not found")
    files = [p for p in Path(root).rglob("AutoCommit*.cs") if "obj" not in p.parts]
    if not files:
        return Result("hygiene", name, SKIP, "no AutoCommit*.cs")
    for f in files:
        lines = f.read_text(encoding="utf-8", errors="replace").splitlines()
        for i, line in enumerate(lines):
            if RE_SLICE.search(line):
                window = lines[max(0, i - 3):i + 1]
                if any(RE_TRIM.search(w) for w in window):
                    rel = f.relative_to(root)
                    return Result("hygiene", name, FAIL, str(rel),
                                  cause=f"{rel}:{i + 1} Trim() before porcelain slice: {line.strip()[:120]}")
    return Result("hygiene", name, PASS, f"{len(files)} file(s) clean")


RE_USER_PATH = re.compile(r"(?i)[A-Z]:[\\/]+Users[\\/]+([A-Za-z0-9._-]+)|/(?:home|Users)/([A-Za-z0-9._-]+)/")
PLACEHOLDER_USERS = {"test", "public", "default", "user", "username", "you", "me", "runner", "xxx",
                     "example", "name", "your-name", "yourname", "foo", "alice", "bob", "someone"}


def personal_paths(root, name):
    """Public-repo hygiene: no absolute path that names a real machine user."""
    title = f"{name}: no personal machine paths"
    if root is None:
        return Result("hygiene", title, SKIP, "repo not found")
    rc, out, _, _ = run(["git", "-c", "core.quotepath=off", "-C", str(root), "grep", "-I", "-n", "-i", "-E",
                         r"[A-Z]:[\\/]+Users[\\/]+[A-Za-z0-9._-]+|/(home|Users)/[A-Za-z0-9._-]+/"], timeout=120)
    hits, files = [], set()
    for line in out.splitlines():
        rel, _, rest = line.partition(":")
        if rel.startswith(("qa/fixtures/", "qa/baseline/", "qa/selftest.py")):
            continue
        for m in RE_USER_PATH.finditer(rest):
            user = (m.group(1) or m.group(2) or "").lower()
            if user and user not in PLACEHOLDER_USERS and user not in ("root", "linuxbrew") \
                    and not user.startswith(("<", "%", "$")):
                hits.append((rel, rest.partition(":")[0], user))
                files.add(rel)
                break
    if hits:
        mask = lambda u: u[0] + "*" * (len(u) - 1)  # never echo the username itself
        users = sorted({mask(h[2]) for h in hits})
        return Result("hygiene", title, FAIL, f"{len(files)} file(s), user(s): {','.join(users)}",
                      cause=f"{hits[0][0]}:{hits[0][1]} names machine user '{mask(hits[0][2])}' "
                            f"({len(files)} files); replace with ~ / %USERPROFILE% / $HOME")
    return Result("hygiene", title, PASS, "0 personal paths")


def run_checks(ctx):
    return [version_sync(), personal_paths(SDK_ROOT, "wkappbot-sdk"), githooks_exec(ctx.kih_root),
            autocommit_trim(ctx.wk_root)]
