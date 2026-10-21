"""Data-driven command matrix (qa/fixtures/*-matrix.json).

Case fields:
  name        row label
  args        argv after the tool; placeholders {TMP} {HANGUL} {TRANSCRIPT} {SKILL}
  setup       optional [{"write": path, "text": str}]  (UTF-8, no BOM)
  expect_rc   int (default 0) or "nonzero" (clean error exit; timeout/not-found do not count)
  expect_out  regex that stdout+stderr must match (optional)
  json_out    true -> stdout must parse as JSON (or JSON lines)
  verify      optional [{"file_exists": p} | {"file_equals": [a, b]} | {"file_contains": [p, s]}]
  mutates     true -> runs only with --allow-mutations
  needs       optional list of placeholders that must resolve (e.g. ["TRANSCRIPT"])
  known_bug   text shown as the cause when the case FAILs (still a FAIL)
  platforms   optional list restricting where the case runs
"""
import json
import os
import re
import shutil
import tempfile
from pathlib import Path

from qa_common import FAIL, PASS, SKIP, Result, first_line, load_json, run

HANGUL = "한글 인코딩 검사 — QA ✓"  # Korean encoding probe


def _subst(s, ph):
    for k, v in ph.items():
        s = s.replace("{" + k + "}", v or "")
    return s


def _parses_json(text):
    t = text.strip()
    if not t:
        return False
    try:
        json.loads(t)
        return True
    except ValueError:
        pass
    try:
        rows = [json.loads(l) for l in t.splitlines() if l.strip()]
        return bool(rows)
    except ValueError:
        return False


def _verify(v, ph):
    if "file_exists" in v:
        p = Path(_subst(v["file_exists"], ph))
        return p.is_file(), f"missing file {p}"
    if "file_equals" in v:
        a, b = (Path(_subst(x, ph)) for x in v["file_equals"])
        if not a.is_file():
            return False, f"missing file {a}"
        return a.read_bytes() == b.read_bytes(), f"bytes differ: {a.name} vs {b.name}"
    if "file_contains" in v:
        p, s = Path(_subst(v["file_contains"][0], ph)), _subst(v["file_contains"][1], ph)
        if not p.is_file():
            return False, f"missing file {p}"
        return s in p.read_text(encoding="utf-8", errors="replace"), f"{p.name} lacks expected text"
    return False, f"unknown verify {v}"


def run_matrix(ctx, area, tool, matrix_file, extra_ph=None, tool_prefix=None):
    """tool: executable path (or None -> every case SKIP). tool_prefix: argv before tool args."""
    cases = load_json(matrix_file)["cases"]
    if not tool:
        return [Result(area, c["name"], SKIP, "tool not found") for c in cases]
    tmp = tempfile.mkdtemp(prefix=f"qa-{area}-")
    ph = {"TMP": tmp.replace("\\", "/"), "HANGUL": HANGUL,
          "TRANSCRIPT": ctx.transcript or "", "SKILL": os.environ.get("QA_SKILL_ID", "qa-encoding-probe")}
    ph.update(extra_ph or {})
    out = []
    try:
        for c in cases:
            name = c["name"]
            if c.get("platforms") and ctx.platform not in c["platforms"]:
                out.append(Result(area, name, SKIP, f"not for {ctx.platform}"))
                continue
            if c.get("mutates") and not ctx.allow_mutations:
                out.append(Result(area, name, SKIP, "mutating case; pass --allow-mutations"))
                continue
            missing = [n for n in c.get("needs", []) if not ph.get(n)]
            if missing:
                out.append(Result(area, name, SKIP, "missing " + ",".join(missing)))
                continue
            for s in c.get("setup", []):
                p = Path(_subst(s["write"], ph))
                p.parent.mkdir(parents=True, exist_ok=True)
                p.write_bytes(_subst(s["text"], ph).encode("utf-8"))
            argv = list(tool_prefix or []) + [tool] + [_subst(a, ph) for a in c["args"]]
            rc, so, se, ms = run(argv, cwd=tmp, timeout=ctx.timeout,
                                 env={"PYTHONIOENCODING": "utf-8", "LANG": os.environ.get("LANG") or "C.UTF-8"})
            problems = []
            want = c.get("expect_rc", 0)
            if (rc == 0 or rc in (124, 127)) if want == "nonzero" else rc != want:
                problems.append(f"rc={rc} (want {want})")
            if c.get("expect_out"):
                pat = _subst(c["expect_out"], {k: re.escape(v or "") for k, v in ph.items()})
                if not re.search(pat, so + "\n" + se, re.S):
                    problems.append(f"output !~ /{c['expect_out'][:60]}/")
            if c.get("json_out") and not _parses_json(so):
                problems.append("stdout is not JSON")
            for v in c.get("verify", []):
                ok, why = _verify(v, ph)
                if not ok:
                    problems.append(why)
            cmd = " ".join(c["args"])[:80]
            if problems:
                cause = "; ".join(problems)
                if c.get("known_bug"):
                    cause += f" | known bug: {c['known_bug']}"
                tail = first_line(se, so)
                if tail:
                    cause += f" | {tail}"
                out.append(Result(area, name, FAIL, cmd, ms, cause))
            else:
                out.append(Result(area, name, PASS, cmd, ms))
    finally:
        shutil.rmtree(tmp, ignore_errors=True)
    return out
