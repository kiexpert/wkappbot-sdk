#!/usr/bin/env python3
"""Issue auto-triage: Gemini (primary) → Groq (fallback)"""
import os, json, urllib.request, subprocess, sys

title  = os.environ.get("ISSUE_TITLE", "")[:200]   # truncate: limit prompt injection surface
body   = os.environ.get("ISSUE_BODY",  "")[:1000]  # truncate: limit prompt injection surface
number = os.environ.get("ISSUE_NUMBER", "")
if not number.isdigit():                            # validate: prevent shell arg injection
    print(f"Invalid ISSUE_NUMBER: {number!r} — skipping")
    sys.exit(0)
gemini = os.environ.get("GEMINI_API_KEY", "")
groq   = os.environ.get("GROQ_API_KEY", "")

if not (gemini or groq):
    print("No AI key available — skipping triage")
    sys.exit(0)

DOCS = {
    "install":        "https://kiexpert.github.io/wkappbot-sdk/guide/install",
    "quickstart":     "https://kiexpert.github.io/wkappbot-sdk/guide/quickstart",
    "commands":       "https://kiexpert.github.io/wkappbot-sdk/reference/commands",
    "grap":           "https://kiexpert.github.io/wkappbot-sdk/reference/grap",
    "scenarios":      "https://kiexpert.github.io/wkappbot-sdk/reference/scenarios",
    "troubleshoot":   "https://kiexpert.github.io/wkappbot-sdk/guide/troubleshooting",
    "faq":            "https://kiexpert.github.io/wkappbot-sdk/faq",
    "pricing":        "https://kiexpert.github.io/wkappbot-sdk/pricing",
}

SYSTEM = (
    "You are a helpful support assistant for WKAppBot — a Windows UI automation tool "
    "for AI agents (Claude, GPT, Gemini). Launcher is MIT open source; core binary is closed source.\n\n"
    "Available docs:\n" +
    "\n".join(f"- {k}: {v}" for k, v in DOCS.items())
)

USER = (
    f"A user opened this GitHub issue.\nTitle: {title}\nBody: {body}\n\n"
    "Tasks:\n"
    "1. Classify as: bug / question / feature-request / documentation\n"
    "2. Pick 1-2 most relevant docs links from the list above\n"
    "3. Write a short friendly Korean reply (2-4 sentences) pointing to relevant docs. "
    "If it looks like a bug, ask for 'wkappbot --version' and 'wkappbot eye tick' output.\n\n"
    'Respond ONLY with JSON: {"type": "bug|question|feature-request|documentation", "comment": "Korean reply"}'
)

def call_gemini(prompt_user):
    payload = json.dumps({
        "system_instruction": {"parts": [{"text": SYSTEM}]},
        "contents": [{"parts": [{"text": prompt_user}]}],
        "generationConfig": {"temperature": 0.3, "maxOutputTokens": 512},
    }).encode()
    req = urllib.request.Request(
        f"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={gemini}",
        data=payload, headers={"Content-Type": "application/json"}, method="POST"
    )
    with urllib.request.urlopen(req, timeout=30) as r:
        return json.loads(r.read())["candidates"][0]["content"]["parts"][0]["text"].strip()

def call_groq(prompt_user):
    payload = json.dumps({
        "model": "llama-3.3-70b-versatile",
        "messages": [
            {"role": "system", "content": SYSTEM},
            {"role": "user",   "content": prompt_user},
        ],
        "temperature": 0.3, "max_tokens": 512,
    }).encode()
    req = urllib.request.Request(
        "https://api.groq.com/openai/v1/chat/completions",
        data=payload,
        headers={"Content-Type": "application/json", "Authorization": f"Bearer {groq}"},
        method="POST"
    )
    with urllib.request.urlopen(req, timeout=30) as r:
        return json.loads(r.read())["choices"][0]["message"]["content"].strip()

def parse_json(raw):
    text = raw.strip()
    if text.startswith("```"):
        parts = text.split("```")
        text = parts[1]
        if text.startswith("json"):
            text = text[4:]
    return json.loads(text.strip())

# Try Gemini first, fall back to Groq
raw = None
if gemini:
    try:
        raw = call_gemini(USER)
        print("Used: Gemini")
    except Exception as e:
        print(f"Gemini failed: {e}")

if raw is None and groq:
    try:
        raw = call_groq(USER)
        print("Used: Groq (fallback)")
    except Exception as e:
        print(f"Groq failed: {e}")
        sys.exit(0)

if raw is None:
    print("All AI calls failed — skipping")
    sys.exit(0)

parsed  = parse_json(raw)
comment = parsed.get("comment", "")
label   = parsed.get("type", "question")

if not comment:
    print("Empty comment — skipping")
    sys.exit(0)

footer = (
    "\n\n---\n"
    "🤖 *자동 응답 · [공식 문서](https://kiexpert.github.io/wkappbot-sdk/) · "
    "추가 문의: kiexpert@kivilab.co.kr*"
)

subprocess.run(["gh", "issue", "comment", number, "--body", comment + footer], check=True)
subprocess.run(["gh", "issue", "edit", number, "--add-label", label], capture_output=True)
print(f"Triaged #{number} as '{label}'")
