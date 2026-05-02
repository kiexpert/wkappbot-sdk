#!/usr/bin/env python3
"""
GHA Triad: Gemini + Groq + Cerebras → Claude Opus synthesis
정반합 (Thesis → Antithesis → Synthesis) debate pipeline.
No CDP, no browser — pure REST APIs.
"""
import os, sys, json, textwrap, pathlib
import urllib.request, urllib.error

def _load_dotenv():
    """Load D:/GitHub/.env if keys not already in environment."""
    env_file = pathlib.Path("D:/GitHub/.env")
    if not env_file.exists():
        return
    for line in env_file.read_text(encoding="utf-8").splitlines():
        line = line.strip()
        if not line or line.startswith("#") or "=" not in line:
            continue
        k, _, v = line.partition("=")
        k = k.strip()
        v = v.strip().strip('"').strip("'")
        if k and k not in os.environ:
            os.environ[k] = v

_load_dotenv()

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
        "User-Agent": "WKAppBot-Triad/1.0",
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
        model=os.environ.get("CEREBRAS_MODEL", "qwen-3-235b-a22b-instruct-2507"),
        label="Cerebras",
    )


def call_opus(prompt: str) -> str:
    """Synthesis with fallback chain: Opus → Gemini-Pro → Groq"""
    # Try Opus first
    key = os.environ.get("ANTHROPIC_API_KEY", "")
    if key:
        try:
            model = os.environ.get("OPUS_MODEL", "claude-opus-4-7")
            body = json.dumps({"model": model, "max_tokens": 2048,
                "messages": [{"role": "user", "content": prompt}]}).encode()
            req = urllib.request.Request("https://api.anthropic.com/v1/messages",
                data=body, headers={"Content-Type": "application/json",
                "x-api-key": key, "anthropic-version": "2023-06-01"}, method="POST")
            with urllib.request.urlopen(req, timeout=120) as r:
                d = json.load(r)
            return "[Opus] " + d["content"][0]["text"].strip()
        except Exception as e:
            print(f"  [Opus failed: {e}] → trying Gemini-Pro fallback...")

    # Fallback 1: Gemini Flash
    if os.environ.get("GEMINI_API_KEY"):
        try:
            result = call_gemini(prompt, model="gemini-2.0-flash")
            if not result.startswith("[Gemini error"):
                return "[Gemini fallback] " + result
            print(f"  [Gemini fallback failed: {result}] → trying Groq fallback...")
        except Exception as e:
            print(f"  [Gemini fallback error: {e}] → trying Groq fallback...")

    # Fallback 2: Groq (Llama)
    if os.environ.get("GROQ_API_KEY"):
        try:
            result = call_openai_compat(prompt,
                base_url="https://api.groq.com/openai/v1",
                api_key=os.environ["GROQ_API_KEY"],
                model="llama-3.3-70b-versatile", label="Groq-synthesis")
            return "[Groq fallback] " + result
        except Exception as e:
            print(f"  [Groq fallback failed: {e}]")

    return "[synthesis unavailable: no API keys set]"


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

# ── Pool: ordered candidates, pick N that succeed ─────────────────────────

def build_pool() -> list:
    """Return ordered list of (name, caller) candidates.
    Primary trio first; extras fill in if primaries are rate-limited."""
    pool = []
    if os.environ.get("GEMINI_API_KEY"):
        pool.append(("gemini-flash", call_gemini))
    if os.environ.get("GROQ_API_KEY"):
        pool.append(("groq-llama70b", call_groq))
    if os.environ.get("CEREBRAS_API_KEY"):
        pool.append(("cerebras-qwen", call_cerebras))
    # Extra slots: second Groq model, second Gemini model
    if os.environ.get("GROQ_API_KEY"):
        def _groq2(p): return call_openai_compat(p,
            "https://api.groq.com/openai/v1", os.environ["GROQ_API_KEY"],
            "llama-3.1-70b-versatile", "Groq-3.1")
        pool.append(("groq-llama31", _groq2))
    if os.environ.get("GEMINI_API_KEY"):
        def _gemini15(p): return call_gemini(p, model="gemini-1.5-flash")
        pool.append(("gemini-1.5flash", _gemini15))
    return pool

def _ok(ans: str) -> bool:
    return bool(ans) and not (ans.startswith("[") and "error" in ans.lower())

# ── Main ───────────────────────────────────────────────────────────────────

def run_triad(question: str, output_dir: str = ".", n: int = 3) -> dict:
    os.makedirs(output_dir, exist_ok=True)
    print(f"\n{'='*60}\nTRIAD: {question[:80]}\n{'='*60}\n")

    pool = build_pool()
    print(f"[Pool] {len(pool)} candidates: {[name for name,_ in pool]}")

    # ── Round 1: pick N working AIs ────────────────────────────────────
    print(f"\n[Round 1] Thesis — picking {n} from pool...")
    tp = THESIS_PROMPT.format(question=question)
    r1, callers = {}, {}
    for name, caller in pool:
        if len(r1) >= n:
            break
        ans = caller(tp)
        if not _ok(ans):
            print(f"  {name}: ❌ {ans[:60]} — skip")
            continue
        r1[name] = ans
        callers[name] = caller
        print(f"  {name}: ✅ {ans[:100]}...")
        with open(f"{output_dir}/r1-{name}.txt", "w", encoding="utf-8") as f:
            f.write(ans)

    if not r1:
        print("❌ No AI responded. Check API keys.")
        return {}
    print(f"\n✅ Participants ({len(r1)}): {list(r1.keys())}")

    # ── Round 2: each critiques the rest ───────────────────────────────
    print("\n[Round 2] Antithesis — cross-critique...")
    r2 = {}
    for name, caller in callers.items():
        others = "\n\n".join(f"[{k}]\n{v}" for k, v in r1.items() if k != name)
        ap = ANTITHESIS_PROMPT.format(question=question,
            other_name="the other AIs", other_answer=others)
        r2[name] = caller(ap)
        print(f"  {name}: {r2[name][:100]}...")
        with open(f"{output_dir}/r2-{name}.txt", "w", encoding="utf-8") as f:
            f.write(r2[name])

    # ── Round 3: synthesis prompt is dynamic ───────────────────────────
    print("\n[Round 3] Synthesis — Claude Opus (with fallback)...")
    r1_block = "\n\n".join(f"### {k}\n{v}" for k, v in r1.items())
    r2_block = "\n\n".join(f"### {k} critique\n{v}" for k, v in r2.items())
    sp = f"""You are the synthesis judge in a {len(r1)}-way AI debate.

## Original Question
{question}

## Round 1 — Independent Answers (정/Thesis)
{r1_block}

## Round 2 — Counter-Arguments (반/Antithesis)
{r2_block}

## Your Task — Final Synthesis (합/Synthesis)
1. Identify the strongest points
2. Note consensus vs. genuine disagreements
3. Resolve disagreements with your own judgment
4. Output a definitive synthesized final answer

Format:
**Points of consensus:** ...
**Key disagreements & resolution:** ...
**Final answer:** (clear, complete, actionable)
"""
    synthesis = call_opus(sp)
    print(f"\n{'─'*60}\nSYNTHESIS:\n{synthesis}\n{'─'*60}\n")

    with open(f"{output_dir}/synthesis.md", "w", encoding="utf-8") as f:
        f.write(f"# Triad Synthesis\n\n**Question:** {question}\n\n---\n\n{synthesis}\n\n")
        f.write("## Round 1 — Thesis\n\n" + r1_block + "\n\n")
        f.write("## Round 2 — Antithesis\n\n" + r2_block + "\n")

    return {"r1": r1, "r2": r2, "synthesis": synthesis}


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python3 triad.py 'your question here' [output_dir]")
        sys.exit(1)
    question = sys.argv[1]
    output_dir = sys.argv[2] if len(sys.argv) > 2 else "triad-output"
    result = run_triad(question, output_dir)
    print(f"\n✅ Done. Artifacts in: {output_dir}/")
