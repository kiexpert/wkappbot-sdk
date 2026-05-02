#!/usr/bin/env python3
"""
GHA Triad: Gemini + Groq + Cerebras → Claude Opus synthesis
정반합 (Thesis → Antithesis → Synthesis) debate pipeline.
No CDP, no browser — pure REST APIs.
"""
import os, sys, json, textwrap
import urllib.request, urllib.error

# ── API helpers ────────────────────────────────────────────────────────────

def call_gemini(prompt: str, model="gemini-2.0-flash") -> str:
    key = os.environ["GEMINI_API_KEY"]
    url = f"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={key}"
    body = json.dumps({"contents": [{"parts": [{"text": prompt}]}]}).encode()
    req = urllib.request.Request(url, data=body,
          headers={"Content-Type": "application/json"}, method="POST")
    try:
        with urllib.request.urlopen(req, timeout=60) as r:
            d = json.load(r)
            return d["candidates"][0]["content"]["parts"][0]["text"].strip()
    except Exception as e:
        return f"[Gemini error: {e}]"


def call_openai_compat(prompt: str, base_url: str, api_key: str,
                       model: str, label: str) -> str:
    url = f"{base_url.rstrip('/')}/chat/completions"
    body = json.dumps({
        "model": model,
        "messages": [{"role": "user", "content": prompt}],
        "max_tokens": 1024,
        "temperature": 0.7,
    }).encode()
    req = urllib.request.Request(url, data=body, headers={
        "Content-Type": "application/json",
        "Authorization": f"Bearer {api_key}",
    }, method="POST")
    try:
        with urllib.request.urlopen(req, timeout=60) as r:
            d = json.load(r)
            return d["choices"][0]["message"]["content"].strip()
    except Exception as e:
        return f"[{label} error: {e}]"


def call_groq(prompt: str) -> str:
    return call_openai_compat(
        prompt,
        base_url="https://api.groq.com/openai/v1",
        api_key=os.environ["GROQ_API_KEY"],
        model=os.environ.get("GROQ_MODEL", "llama-3.3-70b-versatile"),
        label="Groq",
    )


def call_cerebras(prompt: str) -> str:
    return call_openai_compat(
        prompt,
        base_url="https://api.cerebras.ai/v1",
        api_key=os.environ["CEREBRAS_API_KEY"],
        model=os.environ.get("CEREBRAS_MODEL", "llama-3.3-70b"),
        label="Cerebras",
    )


def call_opus(prompt: str) -> str:
    key = os.environ["ANTHROPIC_API_KEY"]
    model = os.environ.get("OPUS_MODEL", "claude-opus-4-7")
    url = "https://api.anthropic.com/v1/messages"
    body = json.dumps({
        "model": model,
        "max_tokens": 2048,
        "messages": [{"role": "user", "content": prompt}],
    }).encode()
    req = urllib.request.Request(url, data=body, headers={
        "Content-Type": "application/json",
        "x-api-key": key,
        "anthropic-version": "2023-06-01",
    }, method="POST")
    try:
        with urllib.request.urlopen(req, timeout=120) as r:
            d = json.load(r)
            return d["content"][0]["text"].strip()
    except Exception as e:
        return f"[Opus error: {e}]"


# ── Debate rounds ──────────────────────────────────────────────────────────

THESIS_PROMPT = """\
Question / Task:
{question}

Give your best, complete answer. Be concise but thorough. Max 300 words.
"""

ANTITHESIS_PROMPT = """\
You are reviewing a debate. The original question was:
{question}

The following answer was given:
--- {other_name} ---
{other_answer}
---

Your task: Write a critical counter-argument or improvement to the above answer.
Identify weaknesses, missing points, or alternative perspectives.
Be specific and constructive. Max 200 words.
"""

SYNTHESIS_PROMPT = """\
You are Claude Opus, the synthesis judge in a three-way AI debate.

## Original Question
{question}

## Round 1 — Independent Answers (정)
### Gemini
{r1_gemini}

### Groq (Llama)
{r1_groq}

### Cerebras (Llama)
{r1_cerebras}

## Round 2 — Counter-Arguments (반)
### Gemini critiques Groq+Cerebras
{r2_gemini}

### Groq critiques Gemini+Cerebras
{r2_groq}

### Cerebras critiques Gemini+Groq
{r2_cerebras}

## Your Task — Final Synthesis (합)
1. Identify the strongest points from all responses
2. Identify where they agree vs. where they genuinely disagree
3. Resolve the disagreements with your own judgment
4. Output a definitive, synthesized final answer

Format your response as:
**Points of consensus:** ...
**Key disagreements:** ...
**Resolution:** ...
**Final answer:** ... (clear, actionable, complete)
"""

# ── Main ───────────────────────────────────────────────────────────────────

def run_triad(question: str, output_dir: str = ".") -> dict:
    os.makedirs(output_dir, exist_ok=True)

    print(f"\n{'='*60}")
    print(f"TRIAD: {question[:80]}")
    print(f"{'='*60}\n")

    # ── Round 1: Thesis (independent, would be parallel in GHA matrix) ──
    print("[Round 1] Thesis — calling 3 free AIs...")
    tp = THESIS_PROMPT.format(question=question)
    r1 = {
        "gemini":   call_gemini(tp),
        "groq":     call_groq(tp),
        "cerebras": call_cerebras(tp),
    }
    for name, ans in r1.items():
        print(f"  {name}: {ans[:120]}...")
        with open(f"{output_dir}/r1-{name}.txt", "w", encoding="utf-8") as f:
            f.write(ans)

    # ── Round 2: Antithesis (each critiques the other two combined) ──────
    print("\n[Round 2] Antithesis — cross-critique...")
    def combined_others(name):
        others = {k: v for k, v in r1.items() if k != name}
        return "\n\n".join(f"[{k}]\n{v}" for k, v in others.items())

    r2 = {}
    for name in ["gemini", "groq", "cerebras"]:
        others_text = combined_others(name)
        caller = {"gemini": call_gemini, "groq": call_groq, "cerebras": call_cerebras}[name]
        ap = ANTITHESIS_PROMPT.format(
            question=question,
            other_name="the other AIs",
            other_answer=others_text,
        )
        r2[name] = caller(ap)
        print(f"  {name} critique: {r2[name][:100]}...")
        with open(f"{output_dir}/r2-{name}.txt", "w", encoding="utf-8") as f:
            f.write(r2[name])

    # ── Round 3: Synthesis — Claude Opus ─────────────────────────────────
    print("\n[Round 3] Synthesis — Claude Opus...")
    sp = SYNTHESIS_PROMPT.format(
        question=question,
        r1_gemini=r1["gemini"],
        r1_groq=r1["groq"],
        r1_cerebras=r1["cerebras"],
        r2_gemini=r2["gemini"],
        r2_groq=r2["groq"],
        r2_cerebras=r2["cerebras"],
    )
    synthesis = call_opus(sp)
    print(f"\n{'─'*60}")
    print("OPUS SYNTHESIS:")
    print(synthesis)
    print(f"{'─'*60}\n")

    with open(f"{output_dir}/synthesis.md", "w", encoding="utf-8") as f:
        f.write(f"# Triad Synthesis\n\n## Question\n{question}\n\n")
        f.write(f"## Final Answer\n\n{synthesis}\n\n")
        f.write("## Round 1 — Thesis\n\n")
        for k, v in r1.items():
            f.write(f"### {k.title()}\n{v}\n\n")
        f.write("## Round 2 — Antithesis\n\n")
        for k, v in r2.items():
            f.write(f"### {k.title()} critique\n{v}\n\n")

    return {"r1": r1, "r2": r2, "synthesis": synthesis}


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python3 triad.py 'your question here' [output_dir]")
        sys.exit(1)
    question = sys.argv[1]
    output_dir = sys.argv[2] if len(sys.argv) > 2 else "triad-output"
    result = run_triad(question, output_dir)
    print(f"\n✅ Done. Artifacts in: {output_dir}/")
