#!/usr/bin/env python3
"""One brief -> one model answer, with the API keys the SDK already holds as repo secrets.

Usage: model_ask.py FAMILY BRIEF_FILE OUT_FILE   (FAMILY: gemini | groq | cerebras)
The model is picked from the provider's LIVE model list (hard-coded names go stale and 404),
preferring the names in PREFER; set MODEL_ASK_MODEL to force one. A failed call exits 1.
Never prints a key: errors print only the exception type and HTTP status.
"""
import json
import os
import sys
import urllib.error
import urllib.request

UA = {"User-Agent": "wkappbot-model-ask/1.0"}
COMPAT = {"groq": ("https://api.groq.com/openai/v1", "GROQ_API_KEY"),
          "cerebras": ("https://api.cerebras.ai/v1", "CEREBRAS_API_KEY")}
KEYS = {"gemini": "GEMINI_API_KEY", "groq": "GROQ_API_KEY", "cerebras": "CEREBRAS_API_KEY"}
PREFER = {"gemini": ["flash-lite", "flash", "pro"],
          "groq": ["llama-3.3-70b", "llama", "gpt-oss", "qwen"],
          "cerebras": ["llama", "qwen", "gpt-oss"]}


def get_json(url, headers=None, body=None):
    data = json.dumps(body).encode() if body is not None else None
    h = dict(UA, **(headers or {}))
    if data is not None:
        h["Content-Type"] = "application/json"
    req = urllib.request.Request(url, data=data, headers=h, method="POST" if data else "GET")
    with urllib.request.urlopen(req, timeout=120) as r:
        return json.load(r)


def pick(family, names):
    forced = os.environ.get("MODEL_ASK_MODEL", "").strip()
    if forced:
        return forced
    names = [n for n in names if not any(x in n for x in ("embed", "tts", "image", "vision", "guard", "whisper", "audio", "live", "preview", "exp"))]
    for p in PREFER[family]:
        hits = sorted(n for n in names if p in n)
        if hits:
            return hits[-1]
    if not names:
        raise RuntimeError("no usable model in the live list")
    return sorted(names)[-1]


def ask_gemini(prompt, key):
    base = "https://generativelanguage.googleapis.com/v1beta"
    models = get_json(f"{base}/models?pageSize=200", {"x-goog-api-key": key}).get("models", [])
    names = [m["name"].split("/", 1)[-1] for m in models
             if "generateContent" in m.get("supportedGenerationMethods", []) and "gemini" in m["name"]]
    model = pick("gemini", names)
    r = get_json(f"{base}/models/{model}:generateContent", {"x-goog-api-key": key},
                 {"contents": [{"parts": [{"text": prompt}]}]})
    return model, r["candidates"][0]["content"]["parts"][0]["text"]


def ask_compat(family, prompt, key):
    base = COMPAT[family][0]
    auth = {"Authorization": "Bearer " + key}
    model = pick(family, [m["id"] for m in get_json(base + "/models", auth).get("data", [])])
    r = get_json(base + "/chat/completions", auth,
                 {"model": model, "max_tokens": 512, "messages": [{"role": "user", "content": prompt}]})
    return model, r["choices"][0]["message"]["content"]


def main():
    if len(sys.argv) != 4 or sys.argv[1] not in KEYS:
        print("usage: model_ask.py gemini|groq|cerebras BRIEF_FILE OUT_FILE", file=sys.stderr)
        return 2
    family, brief_file, out_file = sys.argv[1:]
    key = os.environ.get(KEYS[family], "").strip()
    if not key:
        print(f"::error::secret {KEYS[family]} is not set")
        return 1
    with open(brief_file, encoding="utf-8") as f:
        brief = f.read().strip()
    if not brief:
        print("::error::empty brief")
        return 1
    try:
        model, answer = ask_gemini(brief, key) if family == "gemini" else ask_compat(family, brief, key)
    except urllib.error.HTTPError as e:
        print(f"::error::{family} call failed: HTTP {e.code}")
        return 1
    except Exception as e:  # never echo str(e): it can carry a URL or header
        print(f"::error::{family} call failed: {type(e).__name__}")
        return 1
    answer = (answer or "").strip()
    if not answer:
        print(f"::error::{family} returned an empty answer")
        return 1
    with open(out_file, "w", encoding="utf-8") as f:
        f.write(answer + "\n")
    print(f"MODEL {family}: {model}")
    print(f"ANSWER {family}: {answer[:2000]}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
