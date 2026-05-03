#!/usr/bin/env python3
"""
On skill-check CI failure: push a pending_prompts notification
to the skill author's repo so their Eye delivers it to Claude.
"""
import sys, json, os, glob, uuid, datetime, subprocess, tempfile, shutil

# app → (repo, cwd_in_repo) mapping
APP_REPO_MAP = {
    "wkappbot-sdk":      ("kiexpert/wkappbot-sdk",  "D:/GitHub/wkappbot-sdk"),
    "wkappbot-core":     ("kiexpert/WKAppBot",       "D:/GitHub/WKAppBot"),
    "wkappbot":          ("kiexpert/WKAppBot",       "D:/GitHub/WKAppBot"),
    "wkappbot-workflow": ("kiexpert/WKAppBot",       "D:/GitHub/WKAppBot"),
    "wkappbot-webbot":   ("kiexpert/WKAppBot",       "D:/GitHub/WKAppBot"),
    "paypal-developer":  ("kiexpert/wkappbot-sdk",   "D:/GitHub/wkappbot-sdk"),
}
DEFAULT_REPO = ("kiexpert/wkappbot-sdk", "D:/GitHub/wkappbot-sdk")

VALIDATOR = os.path.join(os.path.dirname(__file__), "validate_skills.py")


def run_validator():
    """Return list of (skill_path, errors) for all failing skills."""
    result = subprocess.run(
        [sys.executable, VALIDATOR],
        capture_output=True, text=True
    )
    failures = []
    current_skill = None
    errs = []
    for line in result.stdout.splitlines():
        if line.startswith("❌ FAIL"):
            if current_skill and errs:
                failures.append((current_skill, list(errs)))
            current_skill = line.replace("❌ FAIL", "").strip()
            errs = []
        elif line.strip().startswith("→") and current_skill:
            errs.append(line.strip()[1:].strip())
    if current_skill and errs:
        failures.append((current_skill, list(errs)))
    return failures


def get_skill_app(skill_path):
    try:
        with open(skill_path, encoding="utf-8-sig") as f:
            d = json.load(f)
        return d.get("app", "wkappbot-sdk")
    except Exception:
        return "wkappbot-sdk"


def push_pending_prompt(repo, target_cwd, message):
    """Clone repo, add pending_prompts file, push."""
    guid = uuid.uuid4().hex[:12]
    fname = f"prompt_NOW_{guid}.json"
    payload = {
        "prompt_text": message,
        "target_cwd": target_cwd,
    }

    tmpdir = tempfile.mkdtemp()
    try:
        # Shallow clone
        subprocess.run(
            ["git", "clone", "--depth=1",
             f"https://x-access-token:{os.environ['GH_TOKEN']}@github.com/{repo}.git",
             tmpdir],
            check=True, capture_output=True
        )

        # Create pending_prompts dir
        pending_dir = os.path.join(tmpdir, ".wkappbot", "pending_prompts")
        os.makedirs(pending_dir, exist_ok=True)

        prompt_path = os.path.join(pending_dir, fname)
        with open(prompt_path, "w", encoding="utf-8") as f:
            json.dump(payload, f, ensure_ascii=False)

        # Commit and push (force-add to bypass .gitignore on .wkappbot/)
        env = {**os.environ,
               "GIT_AUTHOR_NAME": "skill-check-bot",
               "GIT_AUTHOR_EMAIL": "ci@wkappbot",
               "GIT_COMMITTER_NAME": "skill-check-bot",
               "GIT_COMMITTER_EMAIL": "ci@wkappbot"}
        subprocess.run(["git", "-C", tmpdir, "add", "-f", ".wkappbot/"], env=env, check=True, capture_output=True)
        subprocess.run(
            ["git", "-C", tmpdir, "commit", "-m", f"ci: skill-check failure notification [{guid[:6]}]"],
            env=env, check=True, capture_output=True
        )
        subprocess.run(["git", "-C", tmpdir, "push"], env=env, check=True, capture_output=True)
        print(f"  ✅ Notified {repo} via pending_prompts/{fname}")
        return True
    except subprocess.CalledProcessError as e:
        print(f"  ❌ Failed to push to {repo}: {e.stderr.decode()[:200] if e.stderr else e}")
        return False
    finally:
        shutil.rmtree(tmpdir, ignore_errors=True)


def main():
    run_url = os.environ.get("GITHUB_SERVER_URL", "https://github.com") + "/" + \
              os.environ.get("GITHUB_REPOSITORY", "") + "/actions/runs/" + \
              os.environ.get("GITHUB_RUN_ID", "?")

    failures = run_validator()
    if not failures:
        print("No failures — nothing to notify")
        return

    # Group failures by repo
    by_repo = {}
    for skill_path, errs in failures:
        app = get_skill_app(skill_path) if os.path.exists(skill_path) else "wkappbot-sdk"
        repo, cwd = APP_REPO_MAP.get(app, DEFAULT_REPO)
        by_repo.setdefault((repo, cwd), []).append((skill_path, errs))

    for (repo, cwd), items in by_repo.items():
        lines = [f"🚨 [SKILL-CHECK CI FAILED] {len(items)} skill(s) rejected in {os.environ.get('GITHUB_REPOSITORY','?')}"]
        for skill_path, errs in items:
            lines.append(f"\n❌ {os.path.basename(skill_path)}")
            for e in errs[:3]:
                lines.append(f"  → {e}")
        lines.append(f"\n⚠️  반드시 `wkappbot skill edit <id>` 또는 `wkappbot skill contribute` 명령으로만 스킬을 수정하세요!")
        lines.append(f"직접 .skill.json 파일 편집은 엄격히 금지 — | 구분자 파괴, 인코딩 오류, 버전 누락 발생.")
        lines.append(f"\n🔗 CI 상세: {run_url}")
        message = "\n".join(lines)
        push_pending_prompt(repo, cwd, message)


if __name__ == "__main__":
    if "GH_TOKEN" not in os.environ:
        print("GH_TOKEN not set — skipping notification")
        sys.exit(0)
    main()
