#!/usr/bin/env python3
"""All-platform QA runner for the wkappbot CLI, wkhippo and the kih harness.

Prints a per-check table for this platform, saves qa/results/<platform>.json, then prints
the per-platform (Windows / Linux / macOS) table merged from all saved results.
Exit: 0 all PASS/SKIP, 1 any FAIL, 2 runner error.
"""
import argparse
import datetime
import json
import os
import shutil
import sys
import traceback
from pathlib import Path

HERE = Path(__file__).resolve().parent
sys.path[:0] = [str(HERE / "lib"), str(HERE / "checks")]

from qa_common import FAIL, FIXTURES, PASS, QA_DIR, SKIP, Ctx, Result  # noqa: E402
import hooks, hygiene, installer, matrix, static_xplat  # noqa: E402,E401

PLATFORMS = [("windows", "Windows"), ("linux", "Linux"), ("macos", "macOS")]
RUN_CMD = {
    "windows": r"set KIH_ROOT=..\wkappbot-kih& set WKAPPBOT_ROOT=..\WKAppBot& set TRANSCRIPT=<session.jsonl>& qa\run-qa.cmd",
    "linux": "KIH_ROOT=../wkappbot-kih WKAPPBOT_ROOT=../WKAppBot WKAPPBOT_CLI=<linux-x64 wkappbot> TRANSCRIPT=<session.jsonl> qa/run-qa.sh",
    "macos": "KIH_ROOT=../wkappbot-kih WKAPPBOT_ROOT=../WKAppBot WKAPPBOT_CLI=<osx wkappbot> TRANSCRIPT=<session.jsonl> qa/run-qa.sh",
}


def resolve_wkhippo(ctx):
    """WKHIPPO -> WKAPPBOT_ROOT/bin/wkhippo[.cmd] -> bare name on PATH. Returns (tool, prefix)."""
    cands = [os.environ.get("WKHIPPO")]
    if ctx.wk_root:
        b = ctx.wk_root / "bin"
        cands += [b / "wkhippo.cmd", b / "wkhippo"] if ctx.platform == "windows" else [b / "wkhippo"]
    for c in cands:
        if c and Path(c).is_file():
            p = Path(c)
            if p.suffix.lower() == ".cmd" or (ctx.platform != "windows" and os.access(p, os.X_OK)):
                return str(p), None
            return str(p), ["bash"]
    found = shutil.which("wkhippo")
    return (found, None) if found else (None, None)


def area_cli(ctx):
    return matrix.run_matrix(ctx, "cli", ctx.cli, FIXTURES / "cli-matrix.json")


def area_encoding(ctx):
    return matrix.run_matrix(ctx, "encoding", ctx.cli, FIXTURES / "encoding-matrix.json")


def area_wkhippo(ctx):
    tool, prefix = resolve_wkhippo(ctx)
    return matrix.run_matrix(ctx, "wkhippo", tool, FIXTURES / "wkhippo-matrix.json", tool_prefix=prefix)


AREAS = {
    "static": static_xplat.run_checks,
    "hygiene": hygiene.run_checks,
    "cli": area_cli,
    "encoding": area_encoding,
    "wkhippo": area_wkhippo,
    "hooks": hooks.run_checks,
    "installer": installer.run_checks,
}


def cell(s, w):
    s = (s or "").replace("\n", " ").replace("|", "/")
    return s if len(s) <= w else s[: w - 1] + "~"


def print_detail(results):
    print(f"\n{'AREA':<14} {'CHECK':<46} {'STATUS':<6} {'MS':>6}  DETAIL / CAUSE")
    print("-" * 120)
    for r in results:
        ms = "" if r.ms is None else str(r.ms)
        info = r.cause if r.status == FAIL else r.detail
        print(f"{r.area:<14} {cell(r.name, 46):<46} {r.status:<6} {ms:>6}  {cell(info, 160)}")


def counts(rows):
    c = {PASS: 0, FAIL: 0, SKIP: 0}
    for r in rows:
        c[r["status"]] = c.get(r["status"], 0) + 1
    return c


def print_platform_table(saved, current):
    areas = [a for a in AREAS]
    print("\nPER-PLATFORM TABLE (P/F/S = pass/fail/skip)")
    head = f"| {'area':<10} |" + "".join(f" {label:<22} |" for _, label in PLATFORMS)
    print(head)
    print("|" + "-" * 12 + "|" + "|".join("-" * 24 for _ in PLATFORMS) + "|")
    for a in areas:
        line = f"| {a:<10} |"
        for key, _ in PLATFORMS:
            doc = saved.get(key)
            if not doc:
                txt = "not run here"
            else:
                rows = [r for r in doc["results"] if r["area"].startswith(AREA_PREFIX[a])]
                reported = (doc.get("area_counts") or {}).get(a)  # summary relayed from another box
                if reported:
                    c = dict(zip((PASS, FAIL, SKIP), reported))
                    verdict = "FAIL" if c[FAIL] else ("PASS" if c[PASS] else "SKIP")
                    txt = f"{verdict} {c[PASS]}/{c[FAIL]}/{c[SKIP]}"
                elif not rows:
                    txt = "-"
                else:
                    c = counts(rows)
                    verdict = "FAIL" if c[FAIL] else ("PASS" if c[PASS] else "SKIP")
                    txt = f"{verdict} {c[PASS]}/{c[FAIL]}/{c[SKIP]}"
            line += f" {txt:<22} |"
        print(line)
    print()
    for key, label in PLATFORMS:
        doc = saved.get(key)
        if doc:
            mark = " (this run)" if key == current else ""
            print(f"{label}: {doc['when']} on {doc['context'].get('host', '?')}{mark}")
            if doc.get("source"):
                print(f"  source: {doc['source']}")
        else:
            print(f"{label}: not run here -> {RUN_CMD[key]}")


AREA_PREFIX = {"static": "static-xplat", "hygiene": "hygiene", "cli": "cli", "encoding": "encoding",
               "wkhippo": "wkhippo", "hooks": "harness-hooks", "installer": "installer"}


def load_saved(rdir):
    saved = {}
    for key, _ in PLATFORMS:
        p = rdir / f"{key}.json"
        if p.is_file():
            try:
                saved[key] = json.loads(p.read_text(encoding="utf-8"))
            except ValueError:
                pass
    return saved


def main():
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--kih-root", help="wkappbot-kih checkout (else $KIH_ROOT, sibling, D:/GitHub)")
    ap.add_argument("--wkappbot-root", help="WKAppBot checkout (else $WKAPPBOT_ROOT, sibling, D:/GitHub)")
    ap.add_argument("--cli", help="built wkappbot CLI binary (else $WKAPPBOT_CLI, build dirs, PATH)")
    ap.add_argument("--transcript", help="Claude session .jsonl (else $TRANSCRIPT)")
    ap.add_argument("--areas", default=",".join(AREAS), help="comma list: " + ",".join(AREAS))
    ap.add_argument("--since", help="static check: only lines added since this git ref")
    ap.add_argument("--update-baseline", action="store_true", help="static check: freeze current hits")
    ap.add_argument("--allow-mutations", action="store_true", help="run cases that edit skills / write reflexes")
    ap.add_argument("--table-only", action="store_true", help="print the merged table from saved results, run nothing")
    ap.add_argument("--no-save", action="store_true", help="do not write qa/results/<platform>.json")
    ap.add_argument("--results-dir", default=str(QA_DIR / "results"))
    args = ap.parse_args()

    if args.table_only:
        saved = load_saved(Path(args.results_dir))
        print_platform_table(saved, None)
        return 1 if any(r["status"] == FAIL for d in saved.values() for r in d["results"]) else 0

    ctx = Ctx(args)
    print("WKAppBot all-platform QA")
    for k, v in ctx.describe().items():
        print(f"  {k:<15} {v}")

    results = []
    for a in [x.strip() for x in args.areas.split(",") if x.strip()]:
        if a not in AREAS:
            print(f"unknown area: {a}", file=sys.stderr)
            return 2
        try:
            results += AREAS[a](ctx)
        except Exception as e:  # a crashing check is a FAIL, not a runner abort
            traceback.print_exc()
            results.append(Result(AREA_PREFIX[a], f"{a} check crashed", FAIL, cause=f"{type(e).__name__}: {e}"))

    print_detail(results)
    rdir = Path(args.results_dir)
    doc = {"platform": ctx.platform, "when": datetime.datetime.now(datetime.timezone.utc).isoformat(timespec="seconds"),
           "context": dict(ctx.describe(), host=os.environ.get("QA_HOST_LABEL") or __import__("platform").node()),
           "results": [r.to_dict() for r in results]}
    if not args.no_save:
        rdir.mkdir(parents=True, exist_ok=True)
        (rdir / f"{ctx.platform}.json").write_text(json.dumps(doc, indent=2, ensure_ascii=False) + "\n",
                                                   encoding="utf-8", newline="\n")
    saved = load_saved(rdir)
    saved[ctx.platform] = doc
    print_platform_table(saved, ctx.platform)

    fails = [r for r in results if r.status == FAIL]
    c = counts([r.to_dict() for r in results])
    print(f"\nTOTAL {ctx.platform}: {c[PASS]} PASS, {c[FAIL]} FAIL, {c[SKIP]} SKIP")
    for r in fails:
        print(f"FAIL [{r.area}] {r.name}: {r.cause}")
    return 1 if fails else 0


if __name__ == "__main__":
    sys.exit(main())
