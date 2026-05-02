#!/usr/bin/env python3
"""
GHA Triad helper — called from .github/workflows/gha-triad.yml
Usage:
    python3 triad_gha.py thesis  <ai>   $QUESTION
    python3 triad_gha.py antithesis <ai> $QUESTION
    python3 triad_gha.py synthesis      $QUESTION
"""
import os, sys, json, urllib.request

GROQ_BASE = "https://api.groq.com/openai/v1"


def call_gemini(prompt):
    key = os.environ["GEMINI_API_KEY"]
    url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + key
    body = json.dumps({"contents": [{"parts": [{"text": prompt}]}]}).encode()
    req = urllib.request.Request(url, data=body,
        headers={"Content-Type": "application/json"}, method="POST")
    with urllib.request.urlopen(req, timeout=60) as r:
        return json.load(r)["candidates"][0]["content"]["parts"][0]["text"].strip()


def call_compat(prompt, base, key, model, max_tokens=1024):
    body = json.dumps({"model": model, "max_tokens": max_tokens,
                       "messages": [{"role": "user", "content": prompt}]}).encode()
    req = urllib.request.Request(base + "/chat/completions", data=body,
        headers={"Content-Type": "application/json",
                 "Authorization": "Bearer " + key,
                 "User-Agent": "WKAppBot-Triad/1.0"}, method="POST")
    with urllib.request.urlopen(req, timeout=120) as r:
        return json.load(r)["choices"][0]["message"]["content"].strip()


def call_groq(prompt, model="llama-3.3-70b-versatile", max_tokens=1024):
    return call_compat(prompt, GROQ_BASE, os.environ["GROQ_API_KEY"], model, max_tokens)


def call_groq_fallback_gemini(prompt):
    print("[gemini fallback] -> groq/qwen3-32b", file=sys.stderr)
    return call_groq(prompt, "qwen/qwen3-32b")


def call_groq_fallback_cerebras(prompt):
    print("[cerebras fallback] -> groq/llama-4-scout", file=sys.stderr)
    return call_groq(prompt, "meta-llama/llama-4-scout-17b-16e-instruct")


def call_ai(ai, prompt, max_tokens=1024):
    if ai == "gemini":
        try:
            return call_gemini(prompt)
        except Exception as e:
            print(f"[gemini failed: {e}]", file=sys.stderr)
            return call_groq_fallback_gemini(prompt)
    if ai == "groq":
        return call_groq(prompt, max_tokens=max_tokens)
    if ai == "cerebras":
        try:
            return call_compat(prompt, "https://api.cerebras.ai/v1",
                               os.environ["CEREBRAS_API_KEY"],
                               "qwen-3-235b-a22b-instruct-2507", max_tokens)
        except Exception as e:
            print(f"[cerebras failed: {e}]", file=sys.stderr)
            return call_groq_fallback_cerebras(prompt)
    raise ValueError(f"unknown ai: {ai}")


# ── Prompts ──────────────────────────────────────────────────────────────────

THESIS_TMPL = """\
Question / Task:
{question}

Give your best, complete answer. Be concise but thorough. Max 300 words."""

ANTITHESIS_TMPL = """\
Original question: {question}

Two other AIs answered this question:
{others}

Your task: Write a critical counter-argument or improvement.
Identify weaknesses, missing points, or alternative perspectives. Max 200 words."""

SYNTHESIS_TMPL = """\
You are the synthesis judge in a three-way AI debate.

## Original Question
{question}

## Round 1 — Independent Answers (Thesis)
### Gemini
{r1_gemini}

### Groq
{r1_groq}

### Cerebras
{r1_cerebras}

## Round 2 — Counter-Arguments (Antithesis)
### Gemini critiques the others
{r2_gemini}

### Groq critiques the others
{r2_groq}

### Cerebras critiques the others
{r2_cerebras}

## Your Task — Final Synthesis
1. Identify the strongest points from all 6 responses
2. Note where the AIs agree vs. genuinely disagree
3. Resolve disagreements with your own best judgment
4. Output a definitive synthesized answer

Structure:
**Points of consensus:** ...
**Key disagreements & resolution:** ...
**Final answer:** (clear, complete, actionable)"""


def read_file(path):
    return open(path).read() if os.path.exists(path) else "[not available]"


# ── Commands ─────────────────────────────────────────────────────────────────

def cmd_thesis(ai, question):
    prompt = THESIS_TMPL.format(question=question)
    answer = call_ai(ai, prompt)
    print(answer)
    with open(f"r1-{ai}.txt", "w") as f:
        f.write(answer)


def cmd_antithesis(ai, question):
    others_text = "\n\n".join(
        f"[{k.upper()}]\n{read_file(f'r1-{k}.txt')}"
        for k in ["gemini", "groq", "cerebras"] if k != ai
    )
    prompt = ANTITHESIS_TMPL.format(question=question, others=others_text)
    answer = call_ai(ai, prompt, max_tokens=512)
    print(answer)
    with open(f"r2-{ai}.txt", "w") as f:
        f.write(answer)


def cmd_synthesis(question):
    prompt = SYNTHESIS_TMPL.format(
        question=question,
        r1_gemini=read_file("r1-gemini.txt"),
        r1_groq=read_file("r1-groq.txt"),
        r1_cerebras=read_file("r1-cerebras.txt"),
        r2_gemini=read_file("r2-gemini.txt"),
        r2_groq=read_file("r2-groq.txt"),
        r2_cerebras=read_file("r2-cerebras.txt"),
    )

    synth_ai = "opus"
    try:
        key = os.environ.get("ANTHROPIC_API_KEY", "")
        if not key:
            raise ValueError("ANTHROPIC_API_KEY not set")
        body = json.dumps({"model": "claude-opus-4-7", "max_tokens": 2048,
                           "messages": [{"role": "user", "content": prompt}]}).encode()
        req = urllib.request.Request("https://api.anthropic.com/v1/messages", data=body,
            headers={"Content-Type": "application/json", "x-api-key": key,
                     "anthropic-version": "2023-06-01"}, method="POST")
        with urllib.request.urlopen(req, timeout=120) as r:
            synthesis = json.load(r)["content"][0]["text"].strip()
    except Exception as e:
        print(f"[Opus failed: {e}] -> Groq fallback", file=sys.stderr)
        synthesis = call_groq(prompt, max_tokens=2048)
        synth_ai = "groq"

    print(synthesis)

    md = f"# Triad Synthesis\n\n**Question:** {question}\n\n**Synthesized by:** {synth_ai}\n\n---\n\n{synthesis}\n\n"
    md += "<details><summary>Round 1 — Thesis</summary>\n\n"
    for ai in ["gemini", "groq", "cerebras"]:
        md += f"**{ai.title()}:** {read_file(f'r1-{ai}.txt')}\n\n"
    md += "</details>\n\n<details><summary>Round 2 — Antithesis</summary>\n\n"
    for ai in ["gemini", "groq", "cerebras"]:
        md += f"**{ai.title()} critique:** {read_file(f'r2-{ai}.txt')}\n\n"
    md += "</details>\n"
    with open("synthesis.md", "w") as f:
        f.write(md)


if __name__ == "__main__":
    cmd = sys.argv[1]
    if cmd == "thesis":
        cmd_thesis(sys.argv[2], sys.argv[3])
    elif cmd == "antithesis":
        cmd_antithesis(sys.argv[2], sys.argv[3])
    elif cmd == "synthesis":
        cmd_synthesis(sys.argv[2])
    else:
        print(f"unknown command: {cmd}", file=sys.stderr)
        sys.exit(1)
