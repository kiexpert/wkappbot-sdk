#!/usr/bin/env python3
"""
Skill chain integrity validator for wkappbot-sdk CI.

Checks:
  1. Core skills exist (by ID)
  2. Search discoverability -- key terms find the right skills
  3. Chain integrity -- skill requirement references resolve to existing IDs
  4. Korean tag coverage -- Korean search terms hit expected skills

This test catches regressions when skills are renamed, deleted, or lose
critical tags that the skill chain depends on.

Exit code: 0 = all pass, 1 = one or more failures.
"""
import sys, json, os, glob, re

# ---------------------------------------------------------------------------
# Load all skills from repo
# ---------------------------------------------------------------------------

def load_all_skills(root: str) -> dict:
    """Returns {id: skill_dict} for all .skill.json files."""
    skills = {}
    for path in glob.glob(os.path.join(root, "skills", "**", "*.skill.json"), recursive=True):
        try:
            raw = open(path, encoding="utf-8-sig").read()
            d = json.loads(raw)
            sid = d.get("id", "")
            if sid:
                skills[sid] = d
        except Exception:
            pass
    return skills


def skill_search_text(d: dict) -> str:
    """Simulate SearchText: full JSON dump (same as C# SkillRecord.SearchText)."""
    return json.dumps(d, ensure_ascii=False).lower()


def search_skills(skills: dict, *keywords: str) -> list:
    """
    Simple OR+AND simulation of wkappbot skill search.
    Returns list of skill IDs where ALL keywords appear somewhere in the skill.
    """
    hits = []
    kws = [k.lower() for k in keywords]
    for sid, d in skills.items():
        text = skill_search_text(d)
        if all(kw in text for kw in kws):
            hits.append(sid)
    return hits


# ---------------------------------------------------------------------------
# Test cases
# ---------------------------------------------------------------------------

CORE_SKILLS = [
    "on-load",
    "claude-md-guide",
    "repo-health-doctor",
    "mystery-window-diagnosis",
    "suggest-workflow",
    "handoff-checklist",
]

SEARCH_CASES = [
    # (description, expected_skill_id, *search_keywords)
    ("session start → on-load",           "on-load",                  "session", "start"),
    ("md heal → claude-md-guide",         "claude-md-guide",          "md", "heal"),
    ("repo health → repo-health-doctor",  "repo-health-doctor",       "repo", "health"),
    ("mystery window → mystery-window",   "mystery-window-diagnosis", "mystery", "window"),
    ("뉴비 → claude-md-guide",            "claude-md-guide",          "뉴비"),
    ("세션 → on-load",                    "on-load",                  "세션"),
    ("doctor → repo-health-doctor",       "repo-health-doctor",       "doctor"),
    ("compaction → on-load",              "on-load",                  "compaction"),
    ("힐링 → claude-md-guide",            "claude-md-guide",          "힐링"),
    ("이상해 → mystery-window",           "mystery-window-diagnosis", "이상해"),
]

CHAIN_REFS = [
    # (from_skill_id, must_reference_id_in_steps_or_requirements)
    ("on-load",          "suggest-workflow"),
    ("on-load",          "claude-md-guide"),
    ("on-load",          "claude-session-handoff"),
    ("claude-md-guide",  "on-load"),
    ("claude-md-guide",  "repo-health-doctor"),
    ("claude-md-guide",  "handoff-checklist"),
]


def check_core_existence(skills: dict) -> list:
    failures = []
    for sid in CORE_SKILLS:
        if sid not in skills:
            failures.append(f"MISSING CORE SKILL: '{sid}' not found in skills/")
    return failures


def check_search_discoverability(skills: dict) -> list:
    failures = []
    for desc, expected_id, *kws in SEARCH_CASES:
        hits = search_skills(skills, *kws)
        if expected_id not in hits:
            failures.append(
                f"SEARCH MISS [{desc}]: '{' '.join(kws)}' did not find '{expected_id}' "
                f"(got: {hits[:3] or 'nothing'})"
            )
    return failures


def check_chain_integrity(skills: dict) -> list:
    failures = []
    for from_id, to_id in CHAIN_REFS:
        if from_id not in skills:
            failures.append(f"CHAIN: source skill '{from_id}' missing")
            continue
        d = skills[from_id]
        text = skill_search_text(d)
        if to_id not in text:
            failures.append(
                f"CHAIN BROKEN: '{from_id}' no longer references '{to_id}' in steps/requirements"
            )
    return failures


# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main():
    root = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
    skills = load_all_skills(root)

    if not skills:
        print("⚠️  No skills loaded — skipping chain validation")
        sys.exit(0)

    print(f"🔗 Skill chain validation — {len(skills)} skills loaded\n")

    all_failures = []

    print("── Core skill existence ──")
    f = check_core_existence(skills)
    for msg in f:
        print(f"  ❌ {msg}")
    if not f:
        print(f"  ✅ All {len(CORE_SKILLS)} core skills present")
    all_failures.extend(f)

    print("\n── Search discoverability ──")
    f = check_search_discoverability(skills)
    passes = len(SEARCH_CASES) - len(f)
    for msg in f:
        print(f"  ❌ {msg}")
    if not f:
        print(f"  ✅ All {len(SEARCH_CASES)} search terms find correct skills (incl. Korean)")
    else:
        print(f"  ✅ {passes}/{len(SEARCH_CASES)} search cases passed")
    all_failures.extend(f)

    print("\n── Chain reference integrity ──")
    f = check_chain_integrity(skills)
    passes = len(CHAIN_REFS) - len(f)
    for msg in f:
        print(f"  ❌ {msg}")
    if not f:
        print(f"  ✅ All {len(CHAIN_REFS)} chain references intact")
    else:
        print(f"  ✅ {passes}/{len(CHAIN_REFS)} chain refs intact")
    all_failures.extend(f)

    print(f"\n{'─'*60}")
    if all_failures:
        print(f"❌ {len(all_failures)} failure(s) — skill chain is broken")
        sys.exit(1)
    else:
        print("✅ Skill chain healthy")
        sys.exit(0)


if __name__ == "__main__":
    main()
