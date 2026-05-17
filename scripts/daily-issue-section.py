#!/usr/bin/env python3
"""
daily-issue-section.py  --  wkappbot-sdk daily GitHub issue section manager
Usage:
  python daily-issue-section.py ensure               # create today's issue if not exists, print number
  python daily-issue-section.py update <section> <content_file>  # update a section in today's issue
  python daily-issue-section.py get <section>        # print current section content
  python daily-issue-section.py comment <content_file>  # post a comment to today's issue

Sections: NEWS | COMPETITIVE | HEAL | SOURCE | SUMMARY

Repo target: kiexpert/wkappbot-sdk (SDK-side daily board).
Mirrors WKAppBot/bin/wkappbot.hq/scripts/daily-issue-section.py but targets
the SDK repo so AI-NEWS briefings posted from this script land on the
public SDK daily board.
"""
import sys, json, subprocess, datetime, re

REPO = "kiexpert/wkappbot-sdk"
TODAY = datetime.date.today().isoformat()
TITLE = f"[Daily] wkappbot-sdk Report — {TODAY}"

SECTION_MARKERS = {
    "NEWS":        ("<!-- NEWS_START -->",        "<!-- NEWS_END -->"),
    "COMPETITIVE": ("<!-- COMPETITIVE_START -->", "<!-- COMPETITIVE_END -->"),
    "HEAL":        ("<!-- HEAL_START -->",        "<!-- HEAL_END -->"),
    "SOURCE":      ("<!-- SOURCE_START -->",      "<!-- SOURCE_END -->"),
    "SUMMARY":     ("<!-- SUMMARY_START -->",     "<!-- SUMMARY_END -->"),
}

INITIAL_BODY = f"""# wkappbot-sdk Daily Report — {TODAY}

자동 생성된 일일 보고서입니다. 11:10 / 18:10에 AI 뉴스 분석 및 시스템 힐링 결과가 업데이트됩니다.

---

## AI Computer Use News Analysis

<!-- NEWS_START -->
(업데이트 대기 중)
<!-- NEWS_END -->

---

## Competitive Intelligence

<!-- COMPETITIVE_START -->
(업데이트 대기 중)
<!-- COMPETITIVE_END -->

---

## System Heal

<!-- HEAL_START -->
(업데이트 대기 중)
<!-- HEAL_END -->

---

## Source Changes

<!-- SOURCE_START -->
(업데이트 대기 중)
<!-- SOURCE_END -->

---

## Summary

<!-- SUMMARY_START -->
(업데이트 대기 중)
<!-- SUMMARY_END -->
"""

def gh(*args, **kwargs):
    result = subprocess.run(["gh"] + list(args), capture_output=True, text=True,
                            encoding="utf-8", errors="replace", timeout=30, **kwargs)
    return result.returncode, result.stdout.strip(), result.stderr.strip()

def find_today_issue():
    rc, out, _ = gh("issue", "list", "--repo", REPO, "--state", "open",
                    "--search", TODAY, "--json", "number,title")
    if rc != 0 or not out:
        return None
    issues = json.loads(out)
    for i in issues:
        if TODAY in i.get("title", ""):
            return i["number"]
    return None

def create_today_issue():
    rc, out, err = gh("issue", "create", "--repo", REPO,
                      "--title", TITLE,
                      "--body", INITIAL_BODY,
                      "--label", "daily-report")
    if rc != 0:
        # label might not exist, retry without label
        rc, out, err = gh("issue", "create", "--repo", REPO,
                          "--title", TITLE, "--body", INITIAL_BODY)
    if rc != 0:
        print(f"ERROR: gh issue create failed: {err}", file=sys.stderr)
        sys.exit(1)
    m = re.search(r'/issues/(\d+)', out)
    return int(m.group(1)) if m else None

def get_issue_body(num):
    rc, out, _ = gh("issue", "view", str(num), "--repo", REPO, "--json", "body")
    if rc != 0:
        return ""
    return json.loads(out).get("body", "")

def update_section(body, section, new_content):
    start_marker, end_marker = SECTION_MARKERS[section.upper()]
    pattern = re.compile(
        re.escape(start_marker) + r".*?" + re.escape(end_marker),
        re.DOTALL
    )
    replacement = f"{start_marker}\n{new_content.strip()}\n{end_marker}"
    if pattern.search(body):
        return pattern.sub(replacement, body)
    return body + f"\n\n{replacement}\n"

def cmd_ensure():
    num = find_today_issue()
    if num is None:
        num = create_today_issue()
        print(f"CREATED #{num}", file=sys.stderr)
    else:
        print(f"EXISTS #{num}", file=sys.stderr)
    print(num)

def cmd_update(section, content_file):
    num = find_today_issue()
    if num is None:
        num = create_today_issue()
    content = open(content_file, encoding="utf-8", errors="replace").read() if content_file != "-" \
              else sys.stdin.read()
    body = get_issue_body(num)
    new_body = update_section(body, section, content)
    rc, out, err = gh("issue", "edit", str(num), "--repo", REPO, "--body", new_body)
    if rc != 0:
        print(f"ERROR: edit failed: {err}", file=sys.stderr)
        sys.exit(1)
    print(f"UPDATED #{num} [{section}]", file=sys.stderr)
    print(num)

def cmd_get(section):
    num = find_today_issue()
    if num is None:
        print("NO ISSUE TODAY", file=sys.stderr)
        sys.exit(1)
    body = get_issue_body(num)
    start_marker, end_marker = SECTION_MARKERS[section.upper()]
    m = re.search(re.escape(start_marker) + r"\n(.*?)\n" + re.escape(end_marker), body, re.DOTALL)
    print(m.group(1).strip() if m else "")

def cmd_comment(content_file):
    num = find_today_issue()
    if num is None:
        num = create_today_issue()
    content = open(content_file, encoding="utf-8", errors="replace").read() if content_file != "-" \
              else sys.stdin.read()
    rc, out, err = gh("issue", "comment", str(num), "--repo", REPO, "--body", content.strip())
    if rc != 0:
        print(f"ERROR: comment failed: {err}", file=sys.stderr)
        sys.exit(1)
    print(f"COMMENTED #{num}", file=sys.stderr)
    print(num)

if __name__ == "__main__":
    cmd = sys.argv[1] if len(sys.argv) > 1 else "ensure"
    if cmd == "ensure":
        cmd_ensure()
    elif cmd == "comment":
        cmd_comment(sys.argv[2] if len(sys.argv) > 2 else "-")
    elif cmd == "update":
        cmd_update(sys.argv[2], sys.argv[3])
    elif cmd == "get":
        cmd_get(sys.argv[2])
    else:
        print(__doc__)
        sys.exit(1)
