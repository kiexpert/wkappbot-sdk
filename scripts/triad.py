#!/usr/bin/env python3
"""
Triad 정반합 — dynamic pool via `wkappbot ask <name>`
All AI calls go through the unified ask interface.
WebBot (CDP) sessions + REST APIs are treated identically.

Usage:
    python3 triad.py "question" [output_dir] [--n 3]

Pool priority (quality-ranked):
  Webbot (CDP, local only):  gpt, claude, gemini
  REST API:                  groq, cerebras, mistral, opus
  Groq sub-models:           groq-qwen3, groq-llama4, groq-gpt120b
"""
import os, sys, json, subprocess, pathlib, concurrent.futures

# ── .env loader ────────────────────────────────────────────────────────────

def _load_dotenv():
    for path in ["D:/GitHub/.env", pathlib.Path.home() / ".env"]:
        p = pathlib.Path(path)
        if not p.exists():
            continue
        for line in p.read_text(encoding="utf-8").splitlines():
            line = line.strip()
            if not line or line.startswith("#") or "=" not in line:
                continue
            k, _, v = line.partition("=")
            k, v = k.strip(), v.strip().strip('"').strip("'")
            if k and k not in os.environ:
                os.environ[k] = v

_load_dotenv()

# ── wkappbot ask caller ────────────────────────────────────────────────────

WKAPPBOT = os.environ.get("WKAPPBOT_EXE", "wkappbot")

def ask(ai: str, prompt: str, timeout: int = 60) -> str:
    """Call `wkappbot ask <ai> '<prompt>'` and return stdout."""
    ai_name, model_flag = _resolve_ai(ai)
    cmd = [WKAPPBOT, "ask", ai_name] + model_flag + [prompt]
    try:
        r = subprocess.run(cmd, capture_output=True, timeout=timeout,
                           env={**os.environ})
        # Decode with UTF-8, replacing any undecodable bytes
        out = r.stdout.decode("utf-8", errors="replace").strip()
        # Filter out wkappbot launcher JSON/log lines (start with { or [CMD] or [LAUNCH])
        lines = [l for l in out.splitlines()
                 if not l.startswith('{"_"') and not l.startswith('[CMD]')
                 and not l.startswith('[LAUNCH') and not l.startswith('[LOG]')
                 and l.strip()]
        out = "\n".join(lines).strip()
        if r.returncode != 0 and not out:
            err = r.stderr.decode("utf-8", errors="replace").strip()[:120]
            return f"[{ai} error rc={r.returncode}: {err}]"
        return out or f"[{ai}: empty response]"
    except subprocess.TimeoutExpired:
        return f"[{ai}: timeout {timeout}s]"
    except Exception as e:
        return f"[{ai}: {e}]"

def _resolve_ai(ai: str) -> tuple[str, list]:
    """Map shorthand names to (wkappbot_ai, extra_flags)."""
    MODEL_MAP = {
        "groq-qwen3":   ("groq",   ["--model", "qwen/qwen3-32b"]),
        "groq-llama4":  ("groq",   ["--model", "meta-llama/llama-4-scout-17b-16e-instruct"]),
        "groq-gpt120b": ("groq",   ["--model", "openai/gpt-oss-120b"]),
        "groq-llama31": ("groq",   ["--model", "llama-3.1-8b-instant"]),
    }
    if ai in MODEL_MAP:
        name, flags = MODEL_MAP[ai]
        return name, flags
    return ai, []

def probe(ai: str, timeout: int = 15) -> bool:
    """Quick liveness check — returns True if the AI responds."""
    ans = ask(ai, "Reply with exactly: ok", timeout=timeout)
    return bool(ans) and not (ans.startswith("[") and "error" in ans.lower())

def webbot_logged_in(ai: str) -> bool:
    """
    Cookie-based login check — reads wkappbot.hq Chrome profile SQLite DB.
    Fast (<0.1s), no CDP required. Returns False if cookie absent/expired.
    """
    import sqlite3, shutil, datetime, tempfile

    hq = pathlib.Path(os.environ.get("WKAPPBOT_HQ",
        r"D:\GitHub\WKAppBot\bin\wkappbot.hq"))
    # Main wkappbot Chrome profile first; fall back to hash-named profiles
    candidate_dbs = [hq / "chrome-profile" / "Default" / "Network" / "Cookies"]
    profiles_dir = hq / "chrome-profiles"
    if profiles_dir.exists():
        for d in sorted(profiles_dir.iterdir()):
            p = d / "Default" / "Network" / "Cookies"
            if p.exists():
                candidate_dbs.append(p)

    SESSION_COOKIES = {
        "gpt":    (["chatgpt.com", "openai.com"], ["__Secure-next-auth.session-token"]),
        "claude": (["claude.ai"],                 ["__ssid", "ARID", "anthropic-device-id"]),
        "gemini": (["google.com"],                ["__Secure-3PSID", "SID"]),
    }
    if ai not in SESSION_COOKIES:
        return False

    domains, keys = SESSION_COOKIES[ai]
    epoch = datetime.datetime(1601, 1, 1, tzinfo=datetime.timezone.utc)
    now_us = int((datetime.datetime.now(datetime.timezone.utc) - epoch).total_seconds() * 1_000_000)

    for db_path in candidate_dbs:
        tmp = tempfile.mktemp(suffix=".db")
        try:
            shutil.copy2(str(db_path), tmp)
        except (PermissionError, OSError):
            continue  # DB locked by running Chrome — try next profile
        try:
            con = sqlite3.connect(tmp)
            dom_q = " OR ".join(f"host_key LIKE '%{d}'" for d in domains)
            key_q = " OR ".join(f"name='{k}'" for k in keys)
            cur = con.execute(
                f"SELECT length(encrypted_value), expires_utc FROM cookies "
                f"WHERE ({dom_q}) AND ({key_q})"
            )
            for enc_len, exp in cur.fetchall():
                if enc_len > 0 and (exp == 0 or exp > now_us):
                    return True
            con.close()
        except Exception:
            pass
        finally:
            try: os.unlink(tmp)
            except: pass

    return False

# ── Candidate pool (quality-ranked) ───────────────────────────────────────

def build_pool() -> list[tuple[str, int]]:
    """
    Returns ordered (name, quality_score) list.
    Higher score = preferred. Probe filters to actually-available ones.
    """
    pool = []

    # Tier 1: Webbot CDP (best quality, local only)
    # Pre-check login state — skip inactive sessions immediately
    for ai, score in [("gpt", 10), ("claude", 10), ("gemini", 9)]:
        if webbot_logged_in(ai):
            pool.append((ai, score))
        else:
            print(f"  [webbot] {ai}: no active session — skipped")

    # Tier 2: Opus REST (high quality, paid)
    if os.environ.get("ANTHROPIC_API_KEY"):
        pool.append(("opus", 9))

    # Tier 3: Free REST — primary models
    if os.environ.get("GEMINI_API_KEY"):
        pool.append(("gemini", 8))      # REST fallback if CDP fails
    if os.environ.get("GROQ_API_KEY"):
        pool.append(("groq", 7))
        pool.append(("groq-qwen3", 7))
        pool.append(("groq-llama4", 7))
        pool.append(("groq-gpt120b", 6))
    if os.environ.get("CEREBRAS_API_KEY"):
        pool.append(("cerebras", 6))
    if os.environ.get("MISTRAL_API_KEY"):
        pool.append(("mistral", 7))
    if os.environ.get("TOGETHER_API_KEY"):
        pool.append(("together", 6))

    # Deduplicate by name, keep highest score
    seen, deduped = set(), []
    for name, score in sorted(pool, key=lambda x: -x[1]):
        if name not in seen:
            seen.add(name)
            deduped.append((name, score))
    return deduped

def pick_participants(n: int = 3, probe_timeout: int = 15,
                      verbose: bool = True) -> list[str]:
    """Probe pool in parallel, return first N that respond."""
    pool = build_pool()
    if verbose:
        print(f"[Pool] {len(pool)} candidates: {[name for name,_ in pool]}")

    picked = []
    # Probe in batches (don't overwhelm, but parallelise within each tier)
    with concurrent.futures.ThreadPoolExecutor(max_workers=4) as ex:
        futs = {ex.submit(probe, name, probe_timeout): (name, score)
                for name, score in pool}
        results = {}
        for fut in concurrent.futures.as_completed(futs):
            name, score = futs[fut]
            results[name] = (score, fut.result())

    for name, score in pool:
        if len(picked) >= n:
            break
        ok = results.get(name, (0, False))[1]
        status = "✅" if ok else "❌"
        if verbose:
            print(f"  {status} {name} (quality={score})")
        if ok:
            picked.append(name)

    return picked

# ── Debate rounds ──────────────────────────────────────────────────────────

THESIS_PROMPT = """\
Question / Task:
{question}

Give your best, complete answer. Be concise but thorough. Max 300 words.
"""

ANTITHESIS_PROMPT = """\
Original question: {question}

Other AIs answered:
{others}

Your task: Write a critical counter-argument or improvement to the above answers.
Identify weaknesses, missing points, or alternative perspectives. Max 200 words.
"""

SYNTHESIS_PROMPT = """\
You are the synthesis judge in a {n}-way AI debate.

## Original Question
{question}

## Round 1 — Independent Answers (정/Thesis)
{r1_block}

## Round 2 — Counter-Arguments (반/Antithesis)
{r2_block}

## Your Task — Final Synthesis (합/Synthesis)
1. Identify the strongest points from all responses
2. Note where AIs agree vs. genuinely disagree
3. Resolve disagreements with your best judgment
4. Output a definitive synthesized final answer

Format:
**Points of consensus:** ...
**Key disagreements & resolution:** ...
**Final answer:** (clear, complete, actionable)
"""

def run_triad(question: str, output_dir: str = ".", n: int = 3,
              synth_ai: str = "opus") -> dict:
    os.makedirs(output_dir, exist_ok=True)
    print(f"\n{'='*60}\nTRIAD: {question[:80]}\n{'='*60}\n")

    # Pick participants
    print("[Probing] Finding available AIs...")
    participants = pick_participants(n)
    if not participants:
        print("❌ No AI responded. Check API keys / browser sessions.")
        return {}
    print(f"\n✅ Participants ({len(participants)}): {participants}\n")

    # Round 1: Thesis (parallel)
    print(f"[Round 1] Thesis — {len(participants)} AIs in parallel...")
    with concurrent.futures.ThreadPoolExecutor(max_workers=len(participants)) as ex:
        futs = {ex.submit(ask, ai, THESIS_PROMPT.format(question=question)): ai
                for ai in participants}
        r1 = {futs[f]: f.result() for f in concurrent.futures.as_completed(futs)}

    for name, ans in r1.items():
        print(f"  [{name}] {ans[:100]}...")
        pathlib.Path(f"{output_dir}/r1-{name}.txt").write_text(ans, encoding="utf-8")

    # Round 2: Antithesis (parallel)
    print(f"\n[Round 2] Antithesis — cross-critique...")
    def critique(ai):
        others = "\n\n".join(f"[{k}]\n{v}" for k, v in r1.items() if k != ai)
        return ask(ai, ANTITHESIS_PROMPT.format(question=question, others=others))

    with concurrent.futures.ThreadPoolExecutor(max_workers=len(participants)) as ex:
        futs = {ex.submit(critique, ai): ai for ai in participants}
        r2 = {futs[f]: f.result() for f in concurrent.futures.as_completed(futs)}

    for name, ans in r2.items():
        print(f"  [{name}] {ans[:100]}...")
        pathlib.Path(f"{output_dir}/r2-{name}.txt").write_text(ans, encoding="utf-8")

    # Round 3: Synthesis — prefer opus, fall back through pool
    print(f"\n[Round 3] Synthesis — trying {synth_ai}...")
    r1_block = "\n\n".join(f"### {k}\n{v}" for k, v in r1.items())
    r2_block = "\n\n".join(f"### {k} critique\n{v}" for k, v in r2.items())
    sp = SYNTHESIS_PROMPT.format(
        n=len(participants), question=question,
        r1_block=r1_block, r2_block=r2_block)

    synthesis = ""
    for synth_candidate in [synth_ai] + [p for p in participants if p != synth_ai]:
        ans = ask(synth_candidate, sp, timeout=120)
        if not (ans.startswith("[") and "error" in ans.lower()):
            synthesis = f"[{synth_candidate}] {ans}"
            print(f"  ✅ Synthesised by {synth_candidate}")
            break
        print(f"  ❌ {synth_candidate} failed → trying next...")

    print(f"\n{'─'*60}\nSYNTHESIS:\n{synthesis}\n{'─'*60}\n")

    out_md = (f"# Triad Synthesis\n\n**Question:** {question}\n\n"
              f"**Participants:** {', '.join(participants)}\n\n---\n\n{synthesis}\n\n"
              f"## Round 1 — Thesis\n\n{r1_block}\n\n"
              f"## Round 2 — Antithesis\n\n{r2_block}\n")
    pathlib.Path(f"{output_dir}/synthesis.md").write_text(out_md, encoding="utf-8")
    print(f"✅ Done. Artifacts in: {output_dir}/")
    return {"participants": participants, "r1": r1, "r2": r2, "synthesis": synthesis}


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python3 triad.py 'question' [output_dir] [--n 3]")
        sys.exit(1)
    q = sys.argv[1]
    out = sys.argv[2] if len(sys.argv) > 2 and not sys.argv[2].startswith("--") else "triad-output"
    n_arg = 3
    for i, a in enumerate(sys.argv):
        if a == "--n" and i + 1 < len(sys.argv):
            n_arg = int(sys.argv[i + 1])
    run_triad(q, out, n=n_arg)
