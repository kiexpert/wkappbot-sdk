#!/usr/bin/env python3
"""
Skill JSON validator for wkappbot-sdk CI.
Checks every skills/**/*.skill.json for:
  - Valid JSON + UTF-8 encoding (BOM-tolerant)
  - Required fields present and non-empty
  - id matches filename
  - Step integrity
  - Duplicate skill detection (same title across apps)
  - Deprecated command detection in steps/desc

Exit code: 0 = all pass, 1 = one or more failures.
"""
import sys, json, os, glob, re
from collections import defaultdict

REQUIRED_FIELDS = ["id", "app", "title", "desc", "steps"]

# Commands removed from wkappbot — flag if found in skill steps/desc
# Patterns that look like actual secret VALUES (error — block merge)
CONFIDENTIAL_VALUE_PATTERNS = [
    r'sk-[a-zA-Z0-9]{20,}',          # OpenAI-style API key
    r'ghp_[a-zA-Z0-9]{36}',          # GitHub PAT
    r'xoxb-[0-9]+-[a-zA-Z0-9]+',     # Slack bot token
    r'AIza[0-9A-Za-z\-_]{35}',       # Google API key
    r'(?:password|passwd|secret|token|api[_-]?key)\s*[:=]\s*["\'][^"\']{8,}["\']',  # key=value pairs
    r'client_secret\s*[:=]\s*["\'][a-zA-Z0-9_\-]{10,}["\']',  # only quoted values
]

# Keyword patterns to warn about (review manually, not auto-blocked)
CONFIDENTIAL_WARN_PATTERNS = [
    r'\bpaypal\s+secret\b',
    r'\b비밀번호\b',          # Korean "password"
    r'\b인증번호\b',          # Korean "auth code"
]

DEPRECATED_COMMANDS = [
    "wkappbot schedule add",
    "wkappbot schedule list",
    "wkappbot schedule remove",
    "wkappbot schedule clear",
    "a11y eval ",          # replaced by a11y read --eval-js
    "web click",           # replaced by a11y click
    "web type",            # replaced by a11y type
    "web text",
    "web screenshot",
    "web wait",
    "web check",
    "web select",
    "web restore",
    "web eval",
]


def load_skill(path: str):
    """Load skill JSON, tolerating UTF-8 BOM."""
    # Try utf-8-sig first (handles BOM), fall back to latin-1
    for enc in ("utf-8-sig", "utf-8", "latin-1"):
        try:
            raw = open(path, encoding=enc).read()
            return raw, json.loads(raw), None
        except UnicodeDecodeError:
            continue
        except json.JSONDecodeError as e:
            return None, None, f"JSON parse error — {e}"
    return None, None, "Could not decode file with any encoding"


def validate_skill(path: str):
    errors = []
    warnings = []

    raw, d, load_err = load_skill(path)
    if load_err:
        return [load_err], []
    if d is None:
        return ["Could not load skill"], []

    stem = os.path.basename(path).replace(".skill.json", "")

    # Required fields
    for field in REQUIRED_FIELDS:
        val = d.get(field)
        if val is None:
            errors.append(f"MISSING: required field '{field}'")
        elif isinstance(val, str) and not val.strip():
            errors.append(f"EMPTY: field '{field}' is blank")
        elif isinstance(val, list) and len(val) == 0:
            errors.append(f"EMPTY: field '{field}' is empty list")

    # id must match filename
    skill_id = d.get("id", "")
    if skill_id and skill_id != stem:
        errors.append(f"ID_MISMATCH: id='{skill_id}' != filename='{stem}'")

    # Steps integrity
    steps = d.get("steps", [])
    if isinstance(steps, list):
        for i, step in enumerate(steps):
            if not isinstance(step, str):
                errors.append(f"STEPS[{i}]: must be string, got {type(step).__name__}")
            elif not step.strip():
                errors.append(f"STEPS[{i}]: empty step")
    elif isinstance(steps, str):
        parts = [s.strip() for s in steps.split("|")]
        if any(not p for p in parts):
            warnings.append("STEPS: pipe-separated string has empty segments")

    # Confidential check — actual value patterns (errors, block merge)
    full_text = json.dumps(d, ensure_ascii=False)
    for pattern in CONFIDENTIAL_VALUE_PATTERNS:
        if re.search(pattern, full_text, re.IGNORECASE):
            errors.append(f"SECRET_LEAK: looks like a real secret value matching '{pattern}' — remove immediately")
    # Confidential keyword warnings (review manually)
    for pattern in CONFIDENTIAL_WARN_PATTERNS:
        if re.search(pattern, full_text, re.IGNORECASE):
            warnings.append(f"CONFIDENTIAL_HINT: keyword '{pattern}' found — verify no sensitive info")

    # Deprecated command check in steps + desc
    all_text = " ".join(
        steps if isinstance(steps, list) else [steps]
    ) + " " + d.get("desc", "")
    for dep in DEPRECATED_COMMANDS:
        if dep.lower() in all_text.lower():
            warnings.append(f"DEPRECATED: contains removed command '{dep.strip()}'")

    # requirements count check (warn if fewer than 3)
    reqs = d.get("requirements", [])
    if isinstance(reqs, list):
        if len(reqs) == 0:
            warnings.append("REQUIREMENTS: no regression requirements — add at least 3 (cmd => expected)")
        elif len(reqs) < 3:
            warnings.append(f"REQUIREMENTS: only {len(reqs)} requirement(s) — 3 recommended for good coverage")

    # tags should be a list
    tags = d.get("tags")
    if tags is not None and not isinstance(tags, list):
        warnings.append(f"TAGS: expected list, got {type(tags).__name__}")

    # version numeric
    ver = d.get("version")
    if ver is not None:
        try:
            float(str(ver))
        except ValueError:
            warnings.append(f"VERSION: '{ver}' is not numeric")

    return errors, warnings


def find_duplicates(all_skills: list[tuple]) -> dict:
    """Return map of normalized_title → [paths] for duplicates."""
    by_title = defaultdict(list)
    for path, d in all_skills:
        if d:
            title = re.sub(r'\s+', ' ', d.get("title", "").lower().strip())
            if title:
                by_title[title].append(path)
    return {t: paths for t, paths in by_title.items() if len(paths) > 1}


def main():
    root = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
    skills_dir = os.path.join(root, "skills")

    if not os.path.isdir(skills_dir):
        print(f"⚠️  No skills/ directory found at {skills_dir}")
        sys.exit(0)

    files = sorted(glob.glob(os.path.join(skills_dir, "**", "*.skill.json"), recursive=True))
    if not files:
        print("ℹ️  No .skill.json files found — nothing to validate")
        sys.exit(0)

    total = len(files)
    failed = 0
    passed = 0
    all_skills = []

    print(f"🔍 Validating {total} skill file(s)...\n")

    for path in files:
        rel = os.path.relpath(path, root)
        _, d, _ = load_skill(path)
        all_skills.append((rel, d))
        errors, warnings = validate_skill(path)

        if errors:
            failed += 1
            print(f"❌ FAIL  {rel}")
            for e in errors:
                print(f"        → {e}")
            for w in warnings:
                print(f"        ⚠ {w}")
        else:
            passed += 1
            status = "✅ PASS"
            print(f"{status}  {rel}")
            for w in warnings:
                print(f"        ⚠ {w}")

    # Duplicate detection
    dupes = find_duplicates(all_skills)
    if dupes:
        print(f"\n⚠️  DUPLICATE TITLES DETECTED ({len(dupes)} group(s)) — consider merging:")
        for title, paths in dupes.items():
            print(f"  '{title}':")
            for p in paths:
                print(f"    • {p}")

    print(f"\n{'─'*60}")
    print(f"Results: {passed} passed, {failed} failed / {total} total")
    if dupes:
        print(f"Duplicates: {len(dupes)} title group(s) flagged")

    if failed > 0:
        print(f"\n❌ {failed} skill(s) failed validation — fix before merging")
        sys.exit(1)
    else:
        print("\n✅ All skills valid")
        sys.exit(0)


if __name__ == "__main__":
    main()
