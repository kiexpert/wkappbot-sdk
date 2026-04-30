#!/usr/bin/env python3
"""Generate Korean user-friendly release notes via Groq (primary) → Gemini (fallback)."""
import os, json, urllib.request, subprocess, sys

tag      = os.environ.get("RELEASE_TAG", "")
name     = os.environ.get("RELEASE_NAME", tag)
groq     = os.environ.get("GROQ_API_KEY", "")
gemini   = os.environ.get("GEMINI_API_KEY", "")

if not (groq or gemini):
    print("No AI key — skipping release notes generation")
    sys.exit(0)

# Get commits since previous tag
result = subprocess.run(
    ["git", "log", "--oneline", "--no-merges", f"$(git describe --tags --abbrev=0 {tag}^)..{tag}"],
    capture_output=True, text=True, shell=False
)
# fallback: last 30 commits
if result.returncode != 0 or not result.stdout.strip():
    result = subprocess.run(
        ["git", "log", "--oneline", "--no-merges", "-30"],
        capture_output=True, text=True
    )
commits = result.stdout.strip()

SYSTEM = (
    "You are a technical writer for WKAppBot — a Windows UI automation tool for AI agents. "
    "Write concise, user-friendly Korean release notes. Focus on what users care about: "
    "new features, bug fixes, performance improvements. Skip internal/CI/docs-only commits. "
    "Use emoji sparingly. Group by category."
)
USER = (
    f"Release: {name} ({tag})\n\nCommits:\n{commits}\n\n"
    "Write Korean release notes in this format:\n"
    "## 새 기능 (if any)\n"
    "## 버그 수정 (if any)\n"
    "## 개선 사항 (if any)\n\n"
    "Keep each item to one line. Skip trivial commits (chore, test, ci)."
)

def call_groq(system, user):
    payload = json.dumps({
        "model": "llama-3.3-70b-versatile",
        "messages": [{"role": "system", "content": system}, {"role": "user", "content": user}],
        "temperature": 0.4, "max_tokens": 800,
    }).encode()
    req = urllib.request.Request(
        "https://api.groq.com/openai/v1/chat/completions",
        data=payload,
        headers={"Content-Type": "application/json", "Authorization": f"Bearer {groq}"},
        method="POST"
    )
    with urllib.request.urlopen(req, timeout=30) as r:
        return json.loads(r.read())["choices"][0]["message"]["content"].strip()

def call_gemini(system, user):
    payload = json.dumps({
        "system_instruction": {"parts": [{"text": system}]},
        "contents": [{"parts": [{"text": user}]}],
        "generationConfig": {"temperature": 0.4, "maxOutputTokens": 800},
    }).encode()
    req = urllib.request.Request(
        f"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={gemini}",
        data=payload, headers={"Content-Type": "application/json"}, method="POST"
    )
    with urllib.request.urlopen(req, timeout=30) as r:
        return json.loads(r.read())["candidates"][0]["content"]["parts"][0]["text"].strip()

notes = None
if groq:
    try:
        notes = call_groq(SYSTEM, USER)
        print("Used: Groq")
    except Exception as e:
        print(f"Groq failed: {e}")

if notes is None and gemini:
    try:
        notes = call_gemini(SYSTEM, USER)
        print("Used: Gemini (fallback)")
    except Exception as e:
        print(f"Gemini failed: {e}")
        sys.exit(0)

if not notes:
    sys.exit(0)

footer = f"\n\n---\n📚 [전체 문서](https://kiexpert.github.io/wkappbot-sdk/) · [설치 가이드](https://kiexpert.github.io/wkappbot-sdk/guide/install)"
final = notes + footer

# Append to existing release body
subprocess.run(
    ["gh", "release", "edit", tag, "--notes", final],
    check=True
)
print(f"Release notes updated for {tag}")
