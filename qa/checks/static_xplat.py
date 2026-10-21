"""Static cross-platform rule check.

Flags in code files:
  drive-literal     a D:\\ / D:/ path, or C:\\Users / C:/Users  (qa:allow-path)
  userprofile-only  USERPROFILE used with no HOME fallback within +-3 lines

Allowed without baseline:
  - a line carrying the marker  qa:allow-path
  - D:/GitHub/... as the LAST resolver step: an env-var lookup appears within  (qa:allow-path)
    the 10 lines above it (env var -> sibling -> historical Windows default)

Existing hits are frozen in qa/baseline/xplat-<repo>.txt; only NEW hits FAIL.
With --since REF only lines added since REF are checked (baseline ignored).
"""
import hashlib
import re
from pathlib import Path

from qa_common import FAIL, PASS, QA_DIR, SKIP, Result, run

CODE_EXT = {".ps1", ".psm1", ".psd1", ".sh", ".bash", ".py", ".cs", ".cmd", ".bat",
            ".js", ".mjs", ".cjs", ".ts"}
EXCLUDE_PARTS = {".git", "node_modules", "obj", "__pycache__", ".venv", "venv"}
EXCLUDE_PREFIX = ("qa/fixtures/", "qa/baseline/", "qa/selftest.py")

RE_DRIVE = re.compile(r"(?i)(?<![A-Za-z])(?:D:[\\/]|C:[\\/]+Users)")
RE_USERPROFILE = re.compile(r"(?i)USERPROFILE")
RE_HOME = re.compile(r"(?<![A-Za-z_])HOME(?![A-Za-z_])|Path\.home\(|GetFolderPath|expanduser|\$HOME|~/")
RE_GITHUB_DEFAULT = re.compile(r"(?i)D:[\\/]+GitHub")
RE_ENV_LOOKUP = re.compile(r"(?i)\$env:|os\.environ|getenv|GetEnvironmentVariable|\$\{[A-Z_]+|%[A-Z_]+%|\$[A-Z_]{3,}")
ALLOW_MARK = "qa:allow-path"


def _norm(line):
    return " ".join(line.strip().split())[:240]


def _files(root):
    rc, out, _, _ = run(["git", "-C", str(root), "ls-files", "-z", "--cached", "--others", "--exclude-standard"],
                         timeout=60)
    if rc == 0:
        rels = [p for p in out.split("\0") if p]
    else:
        rels = [str(p.relative_to(root)).replace("\\", "/") for p in Path(root).rglob("*") if p.is_file()]
    for rel in rels:
        rel = rel.replace("\\", "/")
        if rel.startswith(EXCLUDE_PREFIX) or EXCLUDE_PARTS.intersection(rel.split("/")):
            continue
        if Path(rel).suffix.lower() in CODE_EXT:
            yield rel


def scan_lines(rel, lines, only=None):
    """Yield (rel, lineno, rule, norm_line). `only`: set of 1-based line numbers to check."""
    for i, line in enumerate(lines):
        n = i + 1
        if only is not None and n not in only:
            continue
        if ALLOW_MARK in line:
            continue
        if RE_DRIVE.search(line):
            allowed = False
            if RE_GITHUB_DEFAULT.search(line):
                above = "\n".join(lines[max(0, i - 10):i + 1])
                allowed = bool(RE_ENV_LOOKUP.search(above.replace(line, "", 1))) or \
                    bool(RE_ENV_LOOKUP.search(line.split("D:")[0]))
            if not allowed:
                yield rel, n, "drive-literal", _norm(line)
        if RE_USERPROFILE.search(line):  # qa:allow-path
            window = "\n".join(lines[max(0, i - 3):i + 4])
            if not RE_HOME.search(window):
                yield rel, n, "userprofile-only", _norm(line)  # qa:allow-path


def scan_repo(root):
    hits = []
    for rel in _files(root):
        try:
            text = (Path(root) / rel).read_text(encoding="utf-8", errors="replace")
        except OSError:
            continue
        hits.extend(scan_lines(rel, text.splitlines()))
    return hits


def added_lines(root, since):
    """{rel: set(lineno)} for lines added since REF (committed + working tree)."""
    rc, out, err, _ = run(["git", "-C", str(root), "diff", "-U0", "--no-color", since, "--"], timeout=120)
    if rc != 0:
        raise RuntimeError(err.strip() or "git diff failed")
    res, cur = {}, None
    for line in out.splitlines():
        if line.startswith("+++ "):
            p = line[4:]
            cur = None if p == "/dev/null" else p[2:] if p.startswith("b/") else p
        elif line.startswith("@@") and cur:
            m = re.search(r"\+(\d+)(?:,(\d+))?", line)
            start, cnt = int(m.group(1)), int(m.group(2) or 1)
            res.setdefault(cur, set()).update(range(start, start + cnt))
    return res


def key(hit):
    """Line text is hashed so the baseline never re-publishes the path it freezes."""
    rel, _, rule, norm = hit
    return f"{rel}\t{rule}\t{hashlib.sha1(norm.encode('utf-8')).hexdigest()[:16]}"


def baseline_path(name):
    return QA_DIR / "baseline" / f"xplat-{name}.txt"


def load_baseline(name):
    p = baseline_path(name)
    if not p.is_file():
        return None
    return {l for l in p.read_text(encoding="utf-8").splitlines() if l and not l.startswith("#")}


def write_baseline(name, hits):
    p = baseline_path(name)
    p.parent.mkdir(parents=True, exist_ok=True)
    body = sorted({key(h) for h in hits})
    p.write_text("# frozen pre-existing cross-platform hits; regenerate with --update-baseline\n"
                 + "\n".join(body) + "\n", encoding="utf-8", newline="\n")
    return p


def check_repo(name, root, since=None, update=False):
    area = "static-xplat"
    if root is None:
        return [Result(area, name, SKIP, "repo not found")]
    if since:
        if run(["git", "-C", str(root), "rev-parse", "--verify", "-q", since + "^{commit}"])[0] != 0:
            return [Result(area, name, SKIP, f"ref {since} not in this repo")]
        try:
            added = added_lines(root, since)
        except RuntimeError as e:
            return [Result(area, name, FAIL, f"git diff {since}", cause=str(e)[:200])]
        hits = []
        for rel, nums in added.items():
            if rel.startswith(EXCLUDE_PREFIX) or Path(rel).suffix.lower() not in CODE_EXT:
                continue
            fp = Path(root) / rel
            if fp.is_file():
                hits.extend(scan_lines(rel, fp.read_text(encoding="utf-8", errors="replace").splitlines(), nums))
        new = hits
        label = f"lines added since {since}"
    else:
        hits = scan_repo(root)
        if update:
            p = write_baseline(name, hits)
            return [Result(area, name, PASS, f"baseline written: {p.name} ({len(hits)} hits)")]
        base = load_baseline(name)
        new = hits if base is None else [h for h in hits if key(h) not in base]
        label = f"{len(hits)} total, baseline {'missing' if base is None else len(base)}"
    if new:
        h = new[0]
        return [Result(area, name, FAIL, f"{len(new)} new hit(s); {label}",
                       cause=f"{h[0]}:{h[1]} [{h[2]}] {h[3][:140]}")]
    return [Result(area, name, PASS, f"0 new hits; {label}")]


def run_checks(ctx):
    from qa_common import SDK_ROOT
    since = ctx.args.since
    upd = ctx.args.update_baseline
    out = check_repo("wkappbot-sdk", SDK_ROOT, since, upd)
    out += check_repo("wkappbot-kih", ctx.kih_root, since, upd)
    out += check_repo("WKAppBot", ctx.wk_root, since, upd)
    return out
