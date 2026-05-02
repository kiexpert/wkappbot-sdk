#!/usr/bin/env python3
"""AI News Collector — HN + YouTube → Gemini/Groq filter → data/ai-news-latest.jsonl
GHA-friendly (no wkappbot CLI, no python.exe). Reads GEMINI_API_KEY or GROQ_API_KEY from env."""

import json, os, re, sys, datetime, urllib.request, urllib.parse

SINCE_HOURS = int(sys.argv[1]) if len(sys.argv) > 1 else 26
OUT_FILE    = sys.argv[2] if len(sys.argv) > 2 else "data/ai-news-latest.jsonl"
GEMINI_KEY  = os.environ.get("GEMINI_API_KEY", "")
GROQ_KEY    = os.environ.get("GROQ_API_KEY", "")

HN_QUERIES = [
    "AI+desktop+automation+agent",
    "Claude+computer+use+MCP+tool",
    "Windows+UI+automation+accessibility",
    "LLM+agent+framework+tool+use",
    "AI+code+agent+computer+control",
]

def fetch(url, timeout=10):
    try:
        with urllib.request.urlopen(url, timeout=timeout) as r:
            return r.read().decode("utf-8", errors="replace")
    except Exception:
        return ""

def collect_hn():
    items, seen = [], set()
    for q in HN_QUERIES:
        raw = fetch(f"https://hn.algolia.com/api/v1/search_by_date?query={q}&tags=story&hitsPerPage=3")
        titles = re.findall(r'"title":"((?:[^"\\]|\\.)*)"', raw)
        urls   = re.findall(r'"url":"((?:[^"\\]|\\.)*)"', raw)
        ids    = re.findall(r'"objectID":"(\d+)"', raw)
        for i, t in enumerate(titles):
            t = t.encode().decode("unicode_escape", errors="replace") if "\\u" in t else t
            if not t or t in seen: continue
            seen.add(t)
            u = urls[i] if i < len(urls) else (f"https://news.ycombinator.com/item?id={ids[i]}" if i < len(ids) else "")
            items.append({"title": t, "url": u, "source": "HN"})
    print(f"HN: {len(items)} stories")
    return items[:15]

def collect_youtube():
    cfg = "wkappbot.hq/config/ai-news-channels.jsonl"
    if not os.path.exists(cfg):
        cfg = os.path.join(os.path.dirname(__file__), "../wkappbot.hq/config/ai-news-channels.jsonl")
    if not os.path.exists(cfg):
        return []
    cutoff = datetime.datetime.utcnow() - datetime.timedelta(hours=SINCE_HOURS)
    items = []
    for line in open(cfg, encoding="utf-8"):
        line = line.strip()
        if not line or line.startswith("#"): continue
        try:
            ch = json.loads(line)
        except Exception:
            continue
        ch_id, ch_name = ch.get("id", ""), ch.get("name", "")
        if not ch_id: continue
        rss = fetch(f"https://www.youtube.com/feeds/videos.xml?channel_id={ch_id}", timeout=8)
        titles = re.findall(r"<title><!\[CDATA\[(.*?)\]\]></title>", rss)[1:]
        links  = re.findall(r'<link rel="alternate".*?href="(https://www\.youtube\.com/watch[^"]+)"', rss)
        dates  = re.findall(r"<published>(.*?)</published>", rss)
        for i, t in enumerate(titles):
            pub_str = dates[i] if i < len(dates) else ""
            try:
                pub = datetime.datetime.fromisoformat(pub_str[:19])
            except Exception:
                pub = datetime.datetime(2000, 1, 1)
            if pub < cutoff: continue
            url = links[i] if i < len(links) else f"https://youtube.com/channel/{ch_id}"
            items.append({"title": f"[{ch_name}] {t}", "url": url, "source": "YT"})
    print(f"YouTube: {len(items)} videos")
    return items

def gemini_filter(items):
    prompt = (
        "You analyze AI/automation news for WKAppBot (Windows desktop automation: UIA, Claude computer-use, MCP, CDP, a11y, Slack).\n"
        "Score each item 1-5 for relevance. Output ONLY items scored >= 2, EXACTLY:\n"
        "SCORE: N\nTITLE: <title>\nURL: <url>\nSOURCE: <source>\nDIRECTION: <one sentence WKAppBot improvement idea>\n\nItems:\n"
    )
    for i, it in enumerate(items):
        prompt += f"{i+1}. [{it['source']}] {it['title']}  {it['url']}\n"

    if GEMINI_KEY:
        try:
            body = json.dumps({"contents": [{"parts": [{"text": prompt}]}]}).encode()
            req  = urllib.request.Request(
                f"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={GEMINI_KEY}",
                data=body, headers={"Content-Type": "application/json"}, method="POST")
            with urllib.request.urlopen(req, timeout=30) as r:
                resp = json.loads(r.read())
            return resp["candidates"][0]["content"]["parts"][0]["text"]
        except Exception as e:
            print(f"Gemini failed: {e}", file=sys.stderr)

    if GROQ_KEY:
        try:
            body = json.dumps({"model": "llama-3.3-70b-versatile", "messages": [{"role": "user", "content": prompt}], "max_tokens": 1000}).encode()
            req  = urllib.request.Request(
                "https://api.groq.com/openai/v1/chat/completions",
                data=body, headers={"Content-Type": "application/json", "Authorization": f"Bearer {GROQ_KEY}"}, method="POST")
            with urllib.request.urlopen(req, timeout=30) as r:
                resp = json.loads(r.read())
            return resp["choices"][0]["message"]["content"]
        except Exception as e:
            print(f"Groq failed: {e}", file=sys.stderr)

    return ""

def parse_filtered(raw, orig):
    url_map = {it["title"]: it for it in orig}
    results = []
    blocks  = re.split(r"\n(?=SCORE:)", raw.strip())
    for block in blocks:
        s = re.search(r"SCORE:\s*(\d)", block)
        t = re.search(r"TITLE:\s*(.+)", block)
        u = re.search(r"URL:\s*(.+)", block)
        src = re.search(r"SOURCE:\s*(.+)", block)
        d = re.search(r"DIRECTION:\s*(.+)", block)
        if not (s and t): continue
        score = int(s.group(1))
        if score < 2: continue
        title = t.group(1).strip()
        url   = u.group(1).strip() if u else url_map.get(title, {}).get("url", "")
        results.append({
            "score": score, "title": title, "url": url,
            "source": src.group(1).strip() if src else "?",
            "direction": d.group(1).strip() if d else "",
            "collected_at": datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%SZ"),
        })
    return sorted(results, key=lambda x: -x["score"])

def main():
    items = collect_hn() + collect_youtube()
    print(f"Total raw: {len(items)} items")
    if not items:
        print("Nothing to filter.")
        return

    raw = gemini_filter(items)
    results = parse_filtered(raw, items)

    os.makedirs(os.path.dirname(OUT_FILE) if os.path.dirname(OUT_FILE) else ".", exist_ok=True)
    with open(OUT_FILE, "w", encoding="utf-8") as f:
        for r in results:
            f.write(json.dumps(r, ensure_ascii=False) + "\n")
    print(f"Saved {len(results)} filtered items to {OUT_FILE}")

if __name__ == "__main__":
    main()
