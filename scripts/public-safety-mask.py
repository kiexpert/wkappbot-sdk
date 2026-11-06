#!/usr/bin/env python3
"""Publish-time mask for anything the SDK ships (npm package, release bundle, skills).

Same rules as the core `wkappbot skill public-mask` (SkillCommand.PublicMask.cs), so the SDK can mask
a build output without the private core binary:
  - every word in .github/public-safety-markers.txt (case-insensitive, longest first) becomes ***;
  - every email address becomes ***, except no-reply bot addresses
    (noreply@anthropic.com, noreply@github.com, *@users.noreply.github.com);
  - the owner's home path Users/kiexp... keeps its Users/ head and the account segment becomes ***;
  - "id" and "app" JSON values are never masked (resolution keys);
  - a clean file is left byte-identical; a masked .json file must still parse, or the run fails.

Usage: python3 -I scripts/public-safety-mask.py [--check] <file-or-dir>...
Prints "PUBLIC-MASK files=<n> changed=<k>"; --check writes nothing and exits 1 when k > 0.
Binary files (any NUL byte, or not valid UTF-8) are skipped.
"""
import json
import os
import re
import sys

TOKEN = "***"
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MARKERS_FILE = os.path.join(ROOT, ".github", "public-safety-markers.txt")

EMAIL_RX = re.compile(r"(?<![A-Za-z0-9._%+-])(?P<pre>u003[cCeE])?(?P<addr>[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,})")
ALLOW_EMAIL_RX = re.compile(
    r"^(noreply@anthropic\.com|noreply@github\.com|[0-9]+\+[A-Za-z0-9-]+@users\.noreply\.github\.com"
    r"|[A-Za-z0-9-]+@users\.noreply\.github\.com)$", re.IGNORECASE)
USER_PATH_RX = re.compile(r"(?P<head>Users(?:\\\\|\\|/)+)kiexp[A-Za-z0-9_.-]*", re.IGNORECASE)
KEY_FIELD_RX = re.compile(r'"(?:id|app)"\s*:\s*"(?:[^"\\]|\\.)*"')


def load_markers():
    words = []
    with open(MARKERS_FILE, encoding="utf-8") as f:
        for raw in f:
            w = raw.strip()
            if w and not w.startswith("#") and w.lower() not in (x.lower() for x in words):
                words.append(w)
    words.sort(key=len, reverse=True)
    return re.compile("|".join(map(re.escape, words)), re.IGNORECASE) if words else None


def mask_segment(s, marker_rx):
    if not s:
        return s
    s = EMAIL_RX.sub(lambda m: m.group(0) if ALLOW_EMAIL_RX.match(m.group("addr")) else (m.group("pre") or "") + TOKEN, s)
    s = USER_PATH_RX.sub(lambda m: m.group("head") + TOKEN, s)
    if marker_rx is not None:
        s = marker_rx.sub(TOKEN, s)
    return s


def mask_text(text, marker_rx):
    out, pos = [], 0
    for keep in KEY_FIELD_RX.finditer(text):
        out.append(mask_segment(text[pos:keep.start()], marker_rx))
        out.append(keep.group(0))
        pos = keep.end()
    out.append(mask_segment(text[pos:], marker_rx))
    return "".join(out)


def iter_files(targets):
    for t in targets:
        if os.path.isdir(t):
            for d, dirs, files in os.walk(t):
                dirs[:] = sorted(x for x in dirs if x != ".git")
                for name in sorted(files):
                    yield os.path.join(d, name)
        elif os.path.isfile(t):
            yield t
        else:
            print(f"[PUBLIC-MASK] not found: {t}", file=sys.stderr)
            sys.exit(2)


def main(argv):
    check = "--check" in argv
    targets = [a for a in argv if a != "--check"]
    if not targets:
        print(__doc__.strip().splitlines()[-3], file=sys.stderr)
        return 2
    marker_rx = load_markers()
    files = changed = 0
    for path in iter_files(targets):
        with open(path, "rb") as f:
            raw = f.read()
        if b"\0" in raw:
            continue
        bom = raw.startswith(b"\xef\xbb\xbf")
        try:
            text = raw[3 if bom else 0:].decode("utf-8")
        except UnicodeDecodeError:
            continue
        files += 1
        masked = mask_text(text, marker_rx)
        if masked == text:
            continue
        changed += 1
        if check:
            print(f"UNMASKED {path}")
            continue
        if path.endswith(".json"):
            try:
                json.loads(masked)
            except ValueError as e:
                print(f"[PUBLIC-MASK] FAIL {path}: masked text is not JSON ({e})", file=sys.stderr)
                return 1
        with open(path, "wb") as f:
            f.write((b"\xef\xbb\xbf" if bom else b"") + masked.encode("utf-8"))
        print(f"MASKED {path}")
    print(f"PUBLIC-MASK files={files} changed={changed}")
    return 1 if check and changed else 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
