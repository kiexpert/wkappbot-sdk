#!/usr/bin/env python3
"""DRY RUN for the one-home-per-skill migration (road steps 1 and 2). Moves/deletes nothing.

Lists every skill id with all its copies across the given sources, flags ids whose copies
diverge, and proposes one home per id:
  home = the sibling repo whose folder name equals the App (lowercased, '-', '_', '.' dropped);
         any other App -> the current repo the copy was found in (spec v1.9, kih b64ed94)
  install-eligible = y when no owner-mandated marker and no kih public-skill gate word (kih 20fd779)
         matches; only those may reach the SDK via `skill install`.

Sources (each optional; resolved env var -> sibling checkout -> D:/GitHub/<repo>):
  sdk  this repo                       kih  $KIH_ROOT       core $WKAPPBOT_ROOT
  hq   $WKAPPBOT_HQ / --hq DIR (the HQ skill cache; any dir holding *.skill.json)
  --source LABEL=DIR  (repeatable) for anything else

Privacy: when --out is inside this public repo, ids that exist ONLY in private sources are
written as private#<hash> so a private skill name never lands here (--no-redact to show).

Usage: python3 qa/skill_home_plan.py [--hq DIR] [--out qa/results/skill-home-plan.md] [--json F]
"""
import argparse
import hashlib
import json
import os
import re
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent
sys.path.insert(0, str(HERE / "lib"))
from qa_common import SDK_ROOT, resolve_repo  # noqa: E402

VOLATILE = {"updated", "updated_at", "last_read", "reads", "hits", "uses", "score", "app"}  # app = location, not content
# Owner-mandated public-safety markers. Any hit -> PRIVATE. Deliberately broad: a false
# PRIVATE costs a review; a false SDK publishes something private.
MARKERS = [
    ("HANTOO", re.compile(r"(?i)hantoo")),
    ("KIWOOM", re.compile(r"(?i)kiwoom")),
    ("token", re.compile(r"(?i)(?<![a-z])tokens?(?![a-z])")),  # also SLACK_BOT_TOKEN (\b fails at _)  # split below into token:credential / token:weak
    ("account", re.compile(r"(?i)(?<![a-z])accounts?(?![a-z])")),
    (".env", re.compile(r"(?i)(?<![\w.])\.env\b")),
    ("password", re.compile(r"(?i)passw(or)?d")),
    ("PIN", re.compile(r"(?<![A-Za-z])PINs?(?![A-Za-z])")),
    ("customer", re.compile(r"(?i)customer")),
    ("owner-private", re.compile(r"(?i)owner[- ]?(private|only|personal)|private[- ]rule")),
    ("machine path", re.compile(r"(?i)[A-Z]:[\\/]+Users[\\/]+(?!test\b|public\b|<|%|\$)\w+")),
]
PRIVATE_APPS = {"personal-docs", "wkautoquant", "paypal-developer"}
# kih .githooks/lib/public-skill-gate.sh (kih 20fd779): refuses these skill files on the SDK remote.
GATE = re.compile(r'(?i)"private"\s*:\s*true|repo-only|hantoo_cache|hantoo|kiwoom|KIS_WEB_PIN|KIWOOM_CERT_PIN|GH_PAT|'
                  r'SLACK_BOT_TOKEN|appsecret|wavevault|WkFeedQuant|WkAutoQuant|scalper')
# Spec v1.9 (kih b64ed94): home = the SIBLING repo whose folder name equals the App after
# lowercasing and dropping '-', '_', '.'; two siblings normalizing equal = ambiguous. Every other
# App (none, ambiguous, matching nothing) -> the CURRENT repo the copy was found in.
SOURCE_REPO = {"sdk": "wkappbot-sdk", "kih": "wkappbot-kih", "core": "WKAppBot", "hq": "HQ cache (no repo)"}


def norm_name(s):
    return re.sub(r"[-_.]", "", (s or "").lower())


def sibling_index(names):
    """{normalized: repo name or None if ambiguous}"""
    idx = {}
    for n in names:
        k = norm_name(n)
        idx[k] = None if k in idx and idx[k] != n else n
    return idx


def home_of(copy, siblings):
    app = copy.get("app_field") or copy["path"].split("/")[0]
    cur = SOURCE_REPO.get(copy["source"], copy["source"])
    hit = siblings.get(norm_name(app), "") if app else ""
    if hit:
        return hit, f"App {app} = sibling {hit}"
    why = "no App" if not app else ("ambiguous App " + app if hit is None else f"App {app} matches no sibling")
    return cur, f"{why} -> current repo"
RE_CRED_TOKEN = re.compile(r"(?i)\b(api|access|auth|bearer|refresh|slack|github|gh|oauth|secret|bot|session|app|"
                           r"personal|xox[bpa]|hantoo|kiwoom)[ _-]?tokens?\b|\btoken\s*[:=]\s*\S")
RE_ENV_TOKEN = re.compile(r"\b[A-Z][A-Z0-9_]*_TOKEN\b|\bTOKEN_[A-Z0-9_]+\b")  # case-sensitive env names


def load_skills(label, root):
    """{id: [copy]} for every *.skill.json under root."""
    out = {}
    if not root or not Path(root).is_dir():
        return out
    for f in Path(root).rglob("*.skill.json"):
        if ".git" in f.parts:
            continue
        try:
            raw = f.read_text(encoding="utf-8-sig")
            d = json.loads(raw)
        except (OSError, ValueError) as e:
            d, raw = {"_error": str(e)}, ""
        sid = d.get("id") or f.name[: -len(".skill.json")]
        norm = json.dumps({k: v for k, v in d.items() if k not in VOLATILE}, sort_keys=True, ensure_ascii=False)
        out.setdefault(sid, []).append({
            "source": label, "path": str(f.relative_to(root)).replace("\\", "/"), "app": d.get("app") or f.parent.name, "app_field": d.get("app"),
            "version": str(d.get("version", "")), "hash": hashlib.sha1(norm.encode()).hexdigest()[:10],
            "text": raw, "error": d.get("_error"),
        })
    return out


def markers(copies):
    found = set()
    for c in copies:
        folder = c["path"].split("/")[0]
        for a in {c["app"], folder} & PRIVATE_APPS:
            found.add(f"app:{a}")
        for name, rx in MARKERS:
            if rx.search(c["text"]):
                found.add(name)
    if "token" in found:
        found.discard("token")
        cred = any(RE_CRED_TOKEN.search(c["text"]) or RE_ENV_TOKEN.search(c["text"]) for c in copies)
        found.add("token:credential" if cred else "token:weak")
    return sorted(found)


def strong(mk):
    """Markers that point at real secrets / private business, not wording."""
    return [m for m in mk if m not in ("token:weak",)]


def plan(sources, siblings=None):
    siblings = sibling_index(siblings or [])
    ids = {}
    for label, root in sources:
        for sid, copies in load_skills(label, root).items():
            ids.setdefault(sid, []).extend(copies)
    rows = []
    for sid in sorted(ids):
        copies = ids[sid]
        hashes = {c["hash"] for c in copies}
        diverges = len(hashes) > 1
        mk = markers(copies)
        in_sdk = any(c["source"] == "sdk" for c in copies)
        private_srcs = sorted({c["source"] for c in copies if c["source"] != "sdk"} - {"hq"})
        gate = sorted({m.group(0).lower() for c in copies for m in GATE.finditer(c["text"])})
        gate_files = sum(1 for c in copies if c["source"] == "sdk" and GATE.search(c["text"]))
        if gate:
            mk = sorted(set(mk) | {"gate"})
        decided = [home_of(c, siblings) for c in copies]
        homes = sorted({h for h, _ in decided})
        home = homes[0] if len(homes) == 1 else "SPLIT: " + " + ".join(homes)
        home_why = "; ".join(sorted({w for _, w in decided}))
        eligible = not mk
        words = [m for m in mk if m != "gate"] + (["gate: " + ", ".join(gate)] if gate else [])
        reason = "passes markers" if eligible else "matched: " + "; ".join(words)
        if not eligible and not strong(mk):
            reason += " (only weak 'token' wording)"
        if gate:
            reason += "; SDK edits refused by kih gate"
        if not eligible and in_sdk:
            reason = "PUBLIC NOW (in sdk); " + reason
        reason += "; home: " + home_why
        if home == "wkappbot-sdk" and not eligible:
            reason += "; CONFLICT: home is the public SDK but fails the markers"
        if any(c["error"] for c in copies):
            reason += "; unparseable copy"
        rows.append({"id": sid, "copies": copies, "diverges": diverges, "home": home, "reason": reason,
                     "eligible": eligible, "words": words,
                     "gate": gate, "gate_files": gate_files,
                     "weak_only": bool(mk) and not strong(mk),
                     "private_only": not in_sdk and all(c["source"] != "sdk" for c in copies)})
    return rows


def write_md(rows, sources, out, redact, sibling_names=()):
    def copy_txt(c):
        return f"{c['source']}:{c['path']}@v{c['version'] or '?'}#{c['hash'][:6]}"

    n = len(rows)
    elig = sum(r["eligible"] for r in rows)
    inel = n - elig
    homes = {}
    for r in rows:
        homes[r["home"]] = homes.get(r["home"], 0) + 1
    div = sum(r["diverges"] for r in rows)
    leak = sum(r["reason"].startswith("PUBLIC NOW") for r in rows)
    leak_strong = sum(r["reason"].startswith("PUBLIC NOW") and not r["weak_only"] for r in rows)
    conflict = sum("CONFLICT" in r["reason"] for r in rows)
    gate_ids = sum(bool(r["gate"]) for r in rows)
    gate_files = sum(r["gate_files"] for r in rows)
    lines = [
        "# Skill home plan (DRY RUN -- nothing moved or deleted)",
        "",
        "Generated by `qa/skill_home_plan.py`. Spec: wkappbot-kih `skill-store-one-home-per-skill-spec`.",
        "",
        "Sources: " + ", ".join(f"`{l}`" + ("" if r else " (not found)") for l, r in sources),
        "",
        "| total ids | install-eligible | not eligible | diverging | not eligible, public now | of those, beyond weak 'token' wording | kih-gate hits on sdk (files / ids) | home=wkappbot-sdk but not eligible |",
        "|---|---|---|---|---|---|---|---|",
        f"| {n} | {elig} | {inel} | {div} | {leak} | {leak_strong} | {gate_files} / {gate_ids} | {conflict} |",
        "",
        "Proposed homes: " + ", ".join(f"{h} {c}" for h, c in sorted(homes.items(), key=lambda x: -x[1]))
        + ". Spec v1.9 (kih b64ed94): home = the sibling repo whose folder name equals the App after lowercasing"
        " and dropping '-', '_', '.'; any other App (none, ambiguous, no match) -> the current repo the copy was"
        " found in. A private skill reaches the SDK only by `skill install` after passing the marker list"
        " (install-eligible = y).",
        "",
        "Siblings used: " + (", ".join(f"`{s}`" for s in sibling_names) or "none") + ".",
        "",
        "Copy format: `source:path@vVERSION#contenthash` (content hash ignores location and volatile keys: `app`, `updated`, read counters).",
        "Private markers (owner mandate, any hit = PRIVATE): " + ", ".join(m for m, _ in MARKERS)
        + ", app in " + ", ".join(sorted(PRIVATE_APPS)) + ".",
        "",
        "| id | copies | diverges | proposed home | install-eligible | reason |",
        "|---|---|---|---|---|---|",
    ]
    for r in rows:
        sid = r["id"]
        copies = r["copies"]
        if redact and r["private_only"]:
            sid = "private#" + hashlib.sha1(r["id"].encode()).hexdigest()[:8]
            copies_txt = f"{len(copies)} private cop{'y' if len(copies) == 1 else 'ies'}"
        else:
            copies_txt = "<br>".join(copy_txt(c) for c in copies)
        el = "y" if r["eligible"] else "n: " + "; ".join(r["words"])
        lines.append(f"| {sid} | {copies_txt} | {'y' if r['diverges'] else 'n'} | {r['home']} | {el} | {r['reason']} |")
    Path(out).parent.mkdir(parents=True, exist_ok=True)
    Path(out).write_text("\n".join(lines) + "\n", encoding="utf-8", newline="\n")
    return {"total": n, "install_eligible": elig, "not_eligible": inel, "homes": homes, "diverging": div,
            "home_sdk_not_eligible": conflict,
            "private_marked_public_now": leak, "public_now_beyond_weak_token": leak_strong,
            "gate_files": gate_files, "gate_ids": gate_ids}


def main():
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--kih-root")
    ap.add_argument("--wkappbot-root")
    ap.add_argument("--hq", help="HQ skill cache dir (else $WKAPPBOT_HQ)")
    ap.add_argument("--source", action="append", default=[], help="extra LABEL=DIR")
    ap.add_argument("--out", default=str(HERE / "results" / "skill-home-plan.md"))
    ap.add_argument("--json", help="also write the full plan as JSON (never redacted; keep it private)")
    ap.add_argument("--no-redact", action="store_true")
    ap.add_argument("--sibling-names", help="comma list of sibling repo folder names (default: dirs next to this repo)")
    a = ap.parse_args()
    kih, _ = resolve_repo("KIH_ROOT", "wkappbot-kih", a.kih_root)
    core, _ = resolve_repo("WKAPPBOT_ROOT", "WKAppBot", a.wkappbot_root)
    hq = a.hq or os.environ.get("WKAPPBOT_HQ")
    sources = [("sdk", SDK_ROOT / "skills"), ("kih", kih), ("core", core), ("hq", Path(hq) if hq else None)]
    sources += [tuple(s.split("=", 1)) for s in a.source]
    sources = [(l, Path(r) if r and Path(r).is_dir() else None) for l, r in sources]
    if a.sibling_names:
        sibs = [s.strip() for s in a.sibling_names.split(",") if s.strip()]
    else:
        sibs = sorted(p.name for p in SDK_ROOT.parent.iterdir() if (p / ".git").exists())
    rows = plan([(l, r) for l, r in sources if r], sibs)
    out = Path(a.out).resolve()
    redact = not a.no_redact and SDK_ROOT in out.parents
    counts = write_md(rows, sources, out, redact, sibs)
    if a.json:
        Path(a.json).write_text(json.dumps([{k: (v if k != "copies" else [{x: c[x] for x in c if x != "text"}
                                                                           for c in v]) for k, v in r.items()}
                                            for r in rows], indent=1, ensure_ascii=False), encoding="utf-8")
    print(f"wrote {out}")
    print(json.dumps(counts))
    return 0


if __name__ == "__main__":
    sys.exit(main())
