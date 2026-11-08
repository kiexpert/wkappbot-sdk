#!/usr/bin/env python3
"""One brief -> one model answer, with the API keys the SDK already holds as repo secrets.

Usage: model_ask.py FAMILY BRIEF_FILE OUT_FILE   (FAMILY: gemini | groq | cerebras)
Reuses the endpoints and models of scripts/triad_gha.py, without its silent fallbacks: a failed
call exits 1. Never prints a key: errors print only the exception type and HTTP status.
"""
import os
import sys
import urllib.error

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import triad_gha  # noqa: E402

CALLS = {
    "gemini": triad_gha.call_gemini,
    "groq": lambda p: triad_gha.call_groq(p),
    "cerebras": lambda p: triad_gha.call_cerebras(p),
}
KEYS = {"gemini": "GEMINI_API_KEY", "groq": "GROQ_API_KEY", "cerebras": "CEREBRAS_API_KEY"}


def main():
    if len(sys.argv) != 4 or sys.argv[1] not in CALLS:
        print("usage: model_ask.py gemini|groq|cerebras BRIEF_FILE OUT_FILE", file=sys.stderr)
        return 2
    family, brief_file, out_file = sys.argv[1:]
    if not os.environ.get(KEYS[family], "").strip():
        print(f"::error::secret {KEYS[family]} is not set")
        return 1
    with open(brief_file, encoding="utf-8") as f:
        brief = f.read().strip()
    if not brief:
        print("::error::empty brief")
        return 1
    try:
        answer = (CALLS[family](brief) or "").strip()
    except urllib.error.HTTPError as e:
        print(f"::error::{family} call failed: HTTP {e.code}")
        return 1
    except Exception as e:  # never echo str(e): a URL in it can carry the key
        print(f"::error::{family} call failed: {type(e).__name__}")
        return 1
    if not answer:
        print(f"::error::{family} returned an empty answer")
        return 1
    with open(out_file, "w", encoding="utf-8") as f:
        f.write(answer + "\n")
    print(f"ANSWER {family}: {answer[:2000]}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
