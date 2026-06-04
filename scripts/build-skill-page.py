#!/usr/bin/env python3
"""Build the WKAppBot public skill marketing page from live HQ catalogs."""

from __future__ import annotations

import html
import json
import re
from pathlib import Path
from typing import Any


REPO_ROOT = Path("D:/GitHub/wkappbot-sdk")
HQ_SKILL_DIRS = [
    Path("D:/GitHub/WKAppBot/bin/wkappbot.hq/skills"),
    Path("D:/SDK/bin/wkappbot.hq/skills"),
    REPO_ROOT / "skills",  # GHA fallback: reads committed skills/ when local HQ unavailable
]
OUTPUT = REPO_ROOT / "docs" / "skills" / "index.html"
SKILLS_OUTPUT_DIR = OUTPUT.parent
ENV_FILE = Path("D:/GitHub/.env")
STEP_PREVIEW_LIMIT = 44

ALLOWED_APPS = ("wkappbot",)
EXCLUDED_APPS = (
    "personal-docs",
    "WkAutoQuant",
    "wkautoquant",
    "invest-kr",
    "paypal-developer",
    "chrome-cdp",
    "competitive",
    "hantoo",
    "kiwoom",
    "DG-",
    "jobkorea",
    "naver",
    "kakao",
    "eunha",
    "willkim",
    "opengov",
    "headhunter",
    "senior",
    "unemployment",
    "youtube-trader",
)

SENSITIVE_PATTERNS = (
    re.compile(r"\b[\w.+-]+@[\w.-]+\.[A-Za-z]{2,}\b"),
    re.compile(r"\b\d{8,}\b"),
    re.compile(r"\b(?=[A-Za-z0-9+/_=-]{32,}\b)(?=[A-Za-z0-9+/_=-]*[A-Z])(?=[A-Za-z0-9+/_=-]*\d)[A-Za-z0-9+/_=-]+\b"),
)


def load_env_mask_values(path: Path = ENV_FILE) -> list[str]:
    try:
        lines = path.read_text(encoding="utf-8-sig").splitlines()
    except (OSError, UnicodeDecodeError):
        return []

    values: list[str] = []
    for line in lines:
        stripped = line.strip()
        if not stripped or stripped.startswith("#") or "=" not in stripped:
            continue
        value = stripped.split("=", 1)[1].strip()
        if len(value) >= 2 and value[0] == value[-1] and value[0] in ("'", '"'):
            value = value[1:-1]
        if value:
            values.append(value)
    return sorted(dict.fromkeys(values), key=len, reverse=True)


ENV_MASK_VALUES = load_env_mask_values()


def mask_sensitive(value: str) -> str:
    masked = value
    for secret in ENV_MASK_VALUES:
        masked = masked.replace(secret, "***")
    for pattern in SENSITIVE_PATTERNS:
        masked = pattern.sub("***", masked)
    return masked


def as_text(value: Any) -> str:
    if value is None:
        return ""
    if isinstance(value, str):
        return value.strip()
    return str(value).strip()


def as_list(value: Any) -> list[str]:
    if isinstance(value, list):
        return [as_text(item) for item in value if as_text(item)]
    if isinstance(value, str) and value.strip():
        return [value.strip()]
    return []


def load_json(path: Path) -> dict[str, Any] | None:
    try:
        return json.loads(path.read_text(encoding="utf-8-sig"))
    except (OSError, UnicodeDecodeError, json.JSONDecodeError):
        return None


def infer_audience(record: dict[str, Any], tags: list[str]) -> str:
    audience = as_text(record.get("audience"))
    if audience:
        return audience

    tag_blob = " ".join(tags).lower()
    inferred: list[str] = []
    if "user" in tag_blob:
        inferred.append("user")
    if "developer" in tag_blob or "dev" in tag_blob or "sdk" in tag_blob:
        inferred.append("developer")
    if inferred:
        return "/".join(dict.fromkeys(inferred))

    return "user/developer"


def app_allowed(app: str) -> bool:
    normalized = app.strip().lower()
    if any(normalized.startswith(excluded.lower()) for excluded in EXCLUDED_APPS):
        return False
    return any(normalized.startswith(allowed.lower()) for allowed in ALLOWED_APPS)


def record_mentions_excluded_app(record: dict[str, Any]) -> bool:
    parts: list[str] = []
    for key in ("id", "app", "title", "title_ko", "desc", "audience"):
        parts.append(as_text(record.get(key)))
    parts.extend(as_list(record.get("tags")))
    parts.extend(as_list(record.get("steps")))
    blob = "\n".join(parts).lower()
    return any(excluded.lower() in blob for excluded in EXCLUDED_APPS)


def include_skill(app: str, audience: str, tags: list[str]) -> bool:
    if not app_allowed(app):
        return False

    blob = f"{audience} {' '.join(tags)}".lower()
    has_public = "user" in blob or "developer" in blob
    has_private = any(word in blob for word in ("operator", "project", "private", "internal"))

    if has_public:
        return True
    if has_private:
        return False
    return audience.strip().lower() in ("", "unclassified", "public")


def star_count(step_count: int) -> int:
    if step_count >= 10:
        return 3
    if step_count >= 5:
        return 2
    return 1


def truncate(value: str, limit: int = 80) -> str:
    compact = " ".join(value.split())
    if len(compact) <= limit:
        return compact
    return compact[: limit - 1].rstrip() + "..."


def preview_step(value: str) -> str:
    compact = " ".join(value.split())
    if len(compact) <= STEP_PREVIEW_LIMIT:
        return compact
    return compact[:STEP_PREVIEW_LIMIT].rstrip() + "..."


def skill_page_slug(skill_id: str) -> str:
    slug = re.sub(r"[^A-Za-z0-9._-]+", "-", skill_id.strip()).strip(".-")
    return slug or "skill"


_TIER_SUFFIX = re.compile(r"(?:-t[23][-_](?:howto|ref)|[-_](?:howto|ref))$", re.I)


def infer_parent_skill_id(skill_id: str) -> str | None:
    m = _TIER_SUFFIX.search(skill_id)
    return skill_id[: m.start()] if m else None


def collect_skills() -> list[dict[str, Any]]:
    skills_by_id: dict[str, dict[str, Any]] = {}

    for root in HQ_SKILL_DIRS:
        if not root.exists():
            continue
        for path in sorted(root.rglob("*.skill.json")):
            record = load_json(path)
            if not record:
                continue

            skill_id = as_text(record.get("id")) or path.stem.replace(".skill", "")
            app = as_text(record.get("app")) or "wkappbot"
            if record_mentions_excluded_app(record):
                continue
            tags = as_list(record.get("tags"))
            audience = infer_audience(record, tags)
            if not include_skill(app, audience, tags):
                continue

            steps = as_list(record.get("steps"))
            title = mask_sensitive(as_text(record.get("title_ko")) or as_text(record.get("title")) or skill_id)
            desc = mask_sensitive(as_text(record.get("desc")))
            stars = star_count(len(steps))

            skills_by_id.setdefault(
                skill_id,
                {
                    "id": mask_sensitive(skill_id),
                    "slug": skill_page_slug(skill_id),
                    "title": title,
                    "desc": desc,
                    "steps": [mask_sensitive(step) for step in steps],
                    "tags": [mask_sensitive(tag) for tag in tags[:8]],
                    "audience": mask_sensitive(audience),
                    "app": mask_sensitive(app),
                    "stars": stars,
                    "source": str(path),
                    "premium": stars == 3 and "developer" in audience.lower(),
                },
            )

    return sorted(
        skills_by_id.values(),
        key=lambda item: (-int(item["stars"]), str(item["app"]).lower(), str(item["title"]).lower()),
    )


def e(value: Any) -> str:
    return html.escape(as_text(value), quote=True)


def render_stars(count: int) -> str:
    return "".join("&#9733;" for _ in range(count)) + "".join("&#9734;" for _ in range(3 - count))




def build_skill_detail_html(skill: dict[str, Any]) -> str:
    title = e(skill["title"])
    desc = e(skill["desc"] or "No description yet.")
    app = e(skill["app"])
    audience = e(skill["audience"])
    tags = "".join(f'<span class="chip">{e(tag)}</span>' for tag in skill["tags"])
    premium = bool(skill.get("premium", False))
    skill_id = skill.get("id", "")
    parent_id = infer_parent_skill_id(skill_id)
    parent_link = ""
    if parent_id:
        parent_slug = skill_page_slug(parent_id)
        parent_link = f'<div class="breadcrumb">Part of: <a href="../{e(parent_slug)}/">{e(parent_id)}</a></div>'

    steps = skill["steps"] or ["This skill is available in the live WKAppBot catalog."]
    if premium:
        step_items = "".join(f"<li>{e(preview_step(step))}</li>" for step in steps)
        blur_class = " blurred"
        overlay = '<div class="unlock"><div style="margin-bottom:10px;font-size:18px">&#128274; Pro Skill</div><a href="../#pricing" style="display:inline-block;padding:8px 20px;background:var(--accent);color:#04110d;font-weight:800;border-radius:6px;text-decoration:none;font-size:14px">Get Pro Access &rarr;</a></div>'
    else:
        step_items = "".join(f"<li>{e(step)}</li>" for step in steps)
        blur_class = ""
        overlay = ""

    pro_button_and_script = ""
    if premium:
        pro_button_and_script = """  <button id="proUnlockButton" class="pro-unlock-button" type="button" title="Enter PAT to unlock Pro" aria-label="Enter PAT to unlock Pro" style="position: fixed; right: 16px; bottom: 16px; z-index: 12; display: inline-grid; place-items: center; width: 42px; height: 42px; border: 1px solid var(--line); border-radius: 8px; background: rgba(12,17,26,.9); color: var(--text); font-size: 18px; cursor: pointer; box-shadow: 0 16px 48px rgba(0,0,0,.34); backdrop-filter: blur(12px);">&#128274;</button>
  <script>
    const SKILL_ID = '__SKILL_ID__';
    const proUnlockButton = document.getElementById('proUnlockButton');
    function escapeHtml(s) { return s.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;'); }
    function injectFullSteps() {
      if (!window.SKILLS_FULL) return;
      const d = window.SKILLS_FULL.find(s => s.id === SKILL_ID);
      if (!d || !d.steps) return;
      const ol = document.querySelector('ol.steps');
      if (!ol) return;
      ol.innerHTML = d.steps.map(s => '<li>' + escapeHtml(s) + '</li>').join('');
    }
    function unlockAll() {
      injectFullSteps();
      const overlays = document.querySelectorAll('.unlock');
      const stepsLists = document.querySelectorAll('.steps.blurred');
      for (const overlay of overlays) {
        overlay.remove();
      }
      for (const list of stepsLists) {
        list.classList.remove('blurred');
      }
      proUnlockButton.style.display = 'none';
      const msg = document.createElement('p');
      msg.style.cssText = 'color:#6ee7b7;font-size:13px;margin-top:8px';
      msg.textContent = 'Pro access verified';
      document.querySelector('.skill-detail').appendChild(msg);
    }
    window.unlockAll = unlockAll;
    function showIdleState() {
      proUnlockButton.classList.remove('unlocked', 'denied');
      proUnlockButton.textContent = String.fromCodePoint(0x1f512);
      proUnlockButton.title = 'Enter PAT to unlock Pro';
      proUnlockButton.setAttribute('aria-label', 'Enter PAT to unlock Pro');
    }
    function showDeniedState(reason) {
      proUnlockButton.classList.add('denied');
      proUnlockButton.textContent = String.fromCodePoint(0x26d4);
      const label = reason === 'invalid'
        ? 'PAT invalid or expired — click to re-enter'
        : 'PAT lacks collaborator access — click to re-enter';
      proUnlockButton.title = label;
      proUnlockButton.setAttribute('aria-label', label);
    }
    async function checkGitHubAccess(token) {
      if (!token) {
        showIdleState();
        return;
      }
      try {
        const response = await fetch('https://raw.githubusercontent.com/kiexpert/wkappbot-harness/main/skills-data-full.js', {
          headers: {
            Authorization: 'Bearer ' + token,
            Accept: 'application/vnd.github.raw'
          }
        });
        if (response.status === 200) {
          const js = await response.text();
          const sc = document.createElement('script'); sc.textContent = js; document.head.appendChild(sc);
          unlockAll();
        } else if (response.status === 401) {
          showDeniedState('invalid');
        } else if (response.status === 403 || response.status === 404) {
          showDeniedState('noaccess');
        } else {
          showIdleState();
        }
      } catch (error) {
        console.warn('GitHub access check failed', error);
        showIdleState();
      }
    }
    proUnlockButton.addEventListener('click', () => {
      const existing = localStorage.getItem('gh_token') || '';
      const token = window.prompt('Enter GitHub Personal Access Token', existing);
      if (token === null) return;
      const trimmed = token.trim();
      if (!trimmed) {
        localStorage.removeItem('gh_token');
        showIdleState();
        return;
      }
      localStorage.setItem('gh_token', trimmed);
      checkGitHubAccess(trimmed);
    });
    checkGitHubAccess(localStorage.getItem('gh_token'));
  </script>"""
        pro_button_and_script = pro_button_and_script.replace('__SKILL_ID__', e(skill_id))

    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>{title} | WKAppBot Skills</title>
  <meta name="description" content="{e(truncate(str(skill['desc']), 200))}">
  <link rel='canonical' href='https://kiexpert.github.io/wkappbot-sdk/skills/{e(skill["slug"])}/'>
  <script type='application/ld+json'>{{"@context":"https://schema.org","@type":"TechArticle","name":"{e(skill["title"])}","description":"{e(skill["desc"][:120])}"}}</script>
  <meta property="og:title" content="{title} | WKAppBot Skills">
  <meta property="og:description" content="{e(truncate(str(skill['desc']), 200))}">
  <meta property="og:type" content="article">
  <meta property="og:url" content="https://kiexpert.github.io/wkappbot-sdk/skills/{e(skill['slug'])}/">
  <meta name="twitter:card" content="summary">
  <style>
    :root {{
      color-scheme: dark;
      --bg: #07090d;
      --panel: #101722;
      --panel-2: #151d2a;
      --text: #f4f7fb;
      --muted: #a8b2c2;
      --line: rgba(255,255,255,.12);
      --accent: #6ee7b7;
      --accent-2: #7dd3fc;
      --warn: #facc15;
    }}
    * {{ box-sizing: border-box; }}
    body {{
      margin: 0;
      font-family: Inter, ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
      background: radial-gradient(circle at top left, rgba(125,211,252,.18), transparent 32rem), var(--bg);
      color: var(--text);
      line-height: 1.5;
    }}
    a {{ color: inherit; }}
    .shell {{ width: min(920px, calc(100% - 32px)); margin: 0 auto; }}
    .detail-page {{ padding: 36px 0 72px; }}
    .back-link {{
      display: inline-flex;
      align-items: center;
      min-height: 36px;
      margin-bottom: 34px;
      color: var(--accent-2);
      font-weight: 700;
      text-decoration: none;
    }}
    .back-link:hover {{ text-decoration: underline; }}
    .skill-detail {{
      border: 1px solid var(--line);
      border-radius: 8px;
      background: linear-gradient(180deg, rgba(21,29,42,.92), rgba(12,17,26,.96));
      padding: clamp(22px, 4vw, 40px);
    }}
    .card-top {{ display: flex; justify-content: space-between; gap: 12px; align-items: center; }}
    .app-badge {{
      display: inline-flex;
      align-items: center;
      min-height: 28px;
      padding: 0 10px;
      border: 1px solid rgba(110,231,183,.28);
      border-radius: 999px;
      color: var(--accent);
      background: rgba(110,231,183,.08);
      font-size: 12px;
      max-width: 240px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }}
    .stars {{ color: var(--warn); font-size: 14px; white-space: nowrap; }}
    h1 {{
      margin: 22px 0 12px;
      font-size: clamp(36px, 7vw, 70px);
      line-height: 1;
      letter-spacing: 0;
    }}
    h2 {{ margin: 34px 0 12px; font-size: 24px; }}
    .description {{ color: var(--muted); font-size: 18px; margin: 0; }}
    .meta {{ margin-top: 14px; color: var(--accent-2); font-size: 13px; }}
    .breadcrumb {{ margin-top: 8px; color: var(--muted); font-size: 12px; }}
    .breadcrumb a {{ color: var(--accent-2); text-decoration: none; }}
    .breadcrumb a:hover {{ text-decoration: underline; }}
    .tags {{ display: flex; flex-wrap: wrap; gap: 6px; margin: 16px 0 0; }}
    .chip {{
      border: 1px solid var(--line);
      border-radius: 999px;
      padding: 4px 8px;
      color: #d7deea;
      font-size: 12px;
      background: rgba(255,255,255,.04);
    }}
    .steps-wrap {{ position: relative; margin-top: 12px; }}
    .steps {{
      margin: 0;
      padding-left: 24px;
      color: #d8e0ec;
      font-size: 15px;
    }}
    .steps.blurred {{ filter: blur(5px); user-select: none; }}
    .steps li {{ margin: 0 0 12px; }}
    .unlock {{
      position: absolute;
      inset: 0;
      display: grid;
      place-items: center;
      border-radius: 8px;
      background: rgba(7,9,13,.46);
      color: white;
      font-weight: 800;
    }}
    @media (max-width: 640px) {{
      .card-top {{ align-items: flex-start; flex-direction: column; }}
    }}
  </style>
</head>
<body>
  <main class="shell detail-page">
    <a class="back-link" href="../">← Back to Skills Index</a>
    <article class="skill-detail">
      <div class="card-top">
        <span class="app-badge">{app}</span>
        <span class="stars" aria-label="{skill['stars']} star rating">{render_stars(int(skill['stars']))}</span>
      </div>
      <h1>{title}</h1>
      <p class="description">{desc}</p>
      <div class="meta">{audience}</div>
      {parent_link}
      <div class="tags">{tags}</div>
      <h2>Steps</h2>
      <div class="steps-wrap">
        <ol class="steps{blur_class}">{step_items}</ol>
        {overlay}
      </div>
    </article>
  </main>
  {pro_button_and_script}
</body>
</html>
"""


def build_skill_tree(skills: list[dict[str, Any]]) -> str:
    skills_by_app: dict[str, list[dict[str, Any]]] = {}
    for skill in skills:
        app = skill["app"]
        if app not in skills_by_app:
            skills_by_app[app] = []
        skills_by_app[app].append(skill)

    tree_html = ""
    for app in sorted(skills_by_app.keys()):
        app_skills = skills_by_app[app]
        tree_html += f'<details><summary style="cursor:pointer;font-weight:700;padding:6px 0">{e(app)}</summary><ul style="list-style:none;padding:0;margin:0">'
        for skill in sorted(app_skills, key=lambda s: s["title"]):
            tree_html += f'<li><a href="#skill-{e(skill["slug"])}" style="display:block;padding:4px 8px;color:#a8b2c2;text-decoration:none;font-size:13px" data-skill-slug="{e(skill["slug"])}" data-search="{e(skill["title"] + " " + skill["desc"] + " " + " ".join(skill["tags"]))}">{e(skill["title"])}</a></li>'
        tree_html += '</ul></details>'
    return tree_html


def build_index_html(skills: list[dict[str, Any]]) -> str:
    """Generate navigation-only index page with search and tree view."""
    count = len(skills)
    tree = build_skill_tree(skills)

    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>WKAppBot Skills Index</title>
  <meta name="description" content="Browse {count} live AI automation playbooks from WKAppBot skill catalog.">
  <meta property="og:title" content="WKAppBot Skills Index">
  <meta property="og:description" content="Browse live AI automation playbooks">
  <meta property="og:type" content="website">
  <meta property="og:url" content="https://kiexpert.github.io/wkappbot-sdk/skills/">
  <style>
    :root {{
      color-scheme: dark;
      --bg: #07090d;
      --text: #f4f7fb;
      --muted: #a8b2c2;
      --line: rgba(255,255,255,.12);
      --accent: #6ee7b7;
      --accent-2: #7dd3fc;
    }}
    * {{ box-sizing: border-box; }}
    body {{
      margin: 0;
      font-family: Inter, ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
      background: var(--bg);
      color: var(--text);
      line-height: 1.5;
    }}
    a {{ color: inherit; text-decoration: none; }}
    a:hover {{ text-decoration: underline; }}
    .shell {{ width: min(1200px, calc(100% - 32px)); margin: 0 auto; padding: 0 16px; }}
    header {{
      border-bottom: 1px solid var(--line);
      padding: 20px 0;
      margin-bottom: 20px;
    }}
    header h1 {{
      margin: 0 0 8px;
      font-size: 28px;
      font-weight: 800;
    }}
    header p {{
      margin: 0;
      color: var(--muted);
      font-size: 14px;
    }}
    .toolbar {{
      display: flex;
      gap: 12px;
      align-items: center;
      margin-bottom: 20px;
      padding-bottom: 16px;
      border-bottom: 1px solid var(--line);
    }}
    input[type="search"] {{
      flex: 1;
      min-height: 40px;
      padding: 0 12px;
      border: 1px solid var(--line);
      border-radius: 6px;
      background: #0c111a;
      color: var(--text);
      font-size: 14px;
    }}
    input[type="search"]:focus {{
      outline: none;
      border-color: var(--accent);
    }}
    .result-count {{
      color: var(--muted);
      font-size: 12px;
      white-space: nowrap;
    }}
    nav {{
      padding: 16px 0 40px;
    }}
    nav details {{
      margin-bottom: 8px;
    }}
    nav summary {{
      cursor: pointer;
      font-weight: 700;
      padding: 8px 0;
      user-select: none;
    }}
    nav summary:hover {{
      color: var(--accent);
    }}
    nav ul {{
      list-style: none;
      padding: 0;
      margin: 8px 0 0 0;
    }}
    nav li {{
      margin: 4px 0;
    }}
    nav a {{
      display: block;
      padding: 4px 8px;
      color: var(--muted);
      font-size: 13px;
      border-radius: 4px;
    }}
    nav a:hover {{
      background: rgba(110,231,183,.1);
      color: var(--accent);
    }}
    nav a.hidden {{
      display: none;
    }}
    footer {{
      color: var(--muted);
      font-size: 12px;
      padding: 20px 0 40px;
      border-top: 1px solid var(--line);
    }}
  </style>
</head>
<body>
  <div class="shell">
    <header>
      <h1>WKAppBot Skills</h1>
      <p>Browse {count} live AI automation playbooks from the WKAppBot catalog</p>
    </header>
    <div class="toolbar">
      <input id="search" type="search" placeholder="Search skills, tags, or descriptions..." autocomplete="off">
      <div id="resultCount" class="result-count">{count} skills</div>
    </div>
    <nav>
{tree}
    </nav>
    <footer>
      <p>Generated from live WKAppBot HQ skill catalog. <a href="../">← Back to home</a></p>
    </footer>
  </div>

  <script>
    const search = document.getElementById('search');
    const resultCount = document.getElementById('resultCount');
    const treeLinks = Array.from(document.querySelectorAll('nav a'));

    function applySearch() {{
      const q = search.value.trim().toLowerCase();
      let visible = 0;
      for (const link of treeLinks) {{
        const match = !q || link.dataset.search.toLowerCase().includes(q);
        link.classList.toggle('hidden', !match);
        if (match) visible += 1;
      }}
      resultCount.textContent = visible + (visible === 1 ? ' skill' : ' skills');
    }}

    search.addEventListener('input', applySearch);
  </script>
</body>
</html>
"""




def main() -> int:
    skills = collect_skills()
    SKILLS_OUTPUT_DIR.mkdir(parents=True, exist_ok=True)
    OUTPUT.write_text(build_index_html(skills), encoding="utf-8")
    for skill in skills:
        skill_dir = SKILLS_OUTPUT_DIR / str(skill["slug"])
        skill_dir.mkdir(parents=True, exist_ok=True)
        (skill_dir / "index.html").write_text(build_skill_detail_html(skill), encoding="utf-8")

    # Generate skills-data-full.js with full skill data (id, slug, steps)
    SKILLS_FULL_OUTPUT = SKILLS_OUTPUT_DIR / "skills-data-full.js"
    data = [{"id": s["id"], "slug": s["slug"], "steps": s["steps"]} for s in skills]
    content = "window.SKILLS_FULL=" + json.dumps(data, ensure_ascii=False, separators=(",", ":")) + ";"
    SKILLS_FULL_OUTPUT.write_text(content, encoding="utf-8")

    # Generate sitemap.xml for Google indexing
    SITEMAP_OUTPUT = REPO_ROOT / "docs" / "skills" / "sitemap.xml"
    base = "https://kiexpert.github.io/wkappbot-sdk/skills"
    urls = [f"<url><loc>{base}/</loc></url>"]
    for s in skills:
        if any(s["slug"].endswith(suffix) for suffix in ("-howto", "-ref", "-t2-howto", "-t3-ref", "-t2", "-t3")):
            continue
        urls.append(f"<url><loc>{base}/{s['slug']}/</loc></url>")
    NL = chr(10)
    sitemap = ('<?xml version="1.0" encoding="UTF-8"?>' + NL
               + '<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">' + NL
               + NL.join(urls) + NL + '</urlset>')
    SITEMAP_OUTPUT.write_text(sitemap, encoding="utf-8")

    print(f"Generated {OUTPUT} with {len(skills)} skills and {len(skills)} detail pages")
    return 0 if len(skills) > 50 else 1


if __name__ == "__main__":
    raise SystemExit(main())
