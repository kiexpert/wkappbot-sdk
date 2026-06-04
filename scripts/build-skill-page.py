#!/usr/bin/env python3
"""Build the WKAppBot public skill marketing page from live HQ catalogs."""

from __future__ import annotations

import html
import json
import re
from pathlib import Path
from typing import Any


HQ_SKILL_DIRS = [
    Path("D:/GitHub/WKAppBot/bin/wkappbot.hq/skills"),
    Path("D:/SDK/bin/wkappbot.hq/skills"),
    REPO_ROOT / "skills",  # GHA fallback: reads committed skills/ when local HQ unavailable
]
REPO_ROOT = Path("D:/GitHub/wkappbot-sdk")
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


def render_skill_card(skill: dict[str, Any]) -> str:
    title = e(skill["title"])
    desc = e(truncate(str(skill["desc"] or "No description yet.")))
    app = e(skill["app"])
    audience = e(skill["audience"])
    slug = e(skill["slug"])
    tags = "".join(f'<span class="chip">{e(tag)}</span>' for tag in skill["tags"])
    steps = skill["steps"][:5] or ["This skill is available in the live WKAppBot catalog."]
    step_items = "".join(
        f'<li data-full="{e(step)}">{e(preview_step(step))}</li>'
        for step in steps
    )
    premium = bool(skill["premium"])
    premium_class = " premium" if premium else ""
    overlay = '<div class="unlock">Unlock with Pro</div>' if premium else ""

    return f"""
      <a class="skill-link" href="{slug}/" data-search="{e(title + ' ' + desc + ' ' + app + ' ' + ' '.join(skill['tags']))}">
      <article class="skill-card{premium_class}" data-skill-id="{e(skill['id'])}">
        <div class="card-top">
          <span class="app-badge">{app}</span>
          <span class="stars" aria-label="{skill['stars']} star rating">{render_stars(int(skill['stars']))}</span>
        </div>
        <h3>{title}</h3>
        <p>{desc}</p>
        <div class="meta">{audience}</div>
        <div class="tags">{tags}</div>
        <div class="steps-wrap">
          <ol class="steps">{step_items}</ol>
          {overlay}
        </div>
      </article>
      </a>"""


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
    <a class="back-link" href="../index.html">Back to skill browser</a>
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


def build_html(skills: list[dict[str, Any]]) -> str:
    count = len(skills)
    cards = "\n".join(render_skill_card(skill) for skill in skills)
    tree = build_skill_tree(skills)
    premium_count = sum(1 for skill in skills if skill["premium"])
    apps = len({skill["app"] for skill in skills})

    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>WKAppBot Skills &#8212; AI That Remembers Its Mistakes | 426 Live Automation Playbooks</title>
  <meta name="description" content="426 live AI automation playbooks built by Claude sessions. Each time Claude makes a mistake, it writes a skill so the next Claude does not repeat it.">
  <meta property="og:title" content="WKAppBot Skills &#8212; AI That Remembers Its Mistakes">
  <meta property="og:description" content="426 live AI automation playbooks. Each Claude session documents its own failures so the next session does not repeat them.">
  <meta property="og:type" content="website">
  <meta property="og:url" content="https://kiexpert.github.io/wkappbot-sdk/skills/">
  <meta name="twitter:card" content="summary_large_image">
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
    .shell {{ width: min(1180px, calc(100% - 32px)); margin: 0 auto; }}
    header {{
      min-height: 88vh;
      display: grid;
      align-content: center;
      gap: 28px;
      padding: 48px 0 32px;
    }}
    nav {{
      position: fixed;
      z-index: 5;
      top: 0;
      left: 0;
      right: 0;
      border-bottom: 1px solid var(--line);
      background: rgba(7,9,13,.82);
      backdrop-filter: blur(14px);
    }}
    nav .shell {{
      display: flex;
      justify-content: space-between;
      align-items: center;
      min-height: 64px;
      gap: 16px;
    }}
    .brand {{ font-weight: 800; }}
    .nav-links {{ display: flex; gap: 16px; color: var(--muted); font-size: 14px; }}
    .nav-button {{
      border: 1px solid var(--line);
      border-radius: 8px;
      background: rgba(255,255,255,.05);
      color: var(--text);
      min-height: 32px;
      padding: 0 10px;
      cursor: pointer;
      font: inherit;
    }}
    .hero-grid {{
      display: grid;
      grid-template-columns: minmax(0, 1.12fr) minmax(280px, .88fr);
      gap: 40px;
      align-items: center;
      padding-top: 64px;
    }}
    h1 {{
      margin: 0;
      max-width: 920px;
      font-size: clamp(42px, 8vw, 94px);
      line-height: .96;
      letter-spacing: 0;
    }}
    .sonnet-letter {{ margin-bottom: 36px; padding: 28px 32px; border-left: 3px solid var(--accent); background: rgba(110,231,183,.06); border-radius: 0 8px 8px 0; max-width: 720px; }}
    .letter-from {{ color: var(--accent); font-size: 11px; font-weight: 700; letter-spacing: .1em; text-transform: uppercase; margin-bottom: 14px; }}
    .sonnet-letter blockquote {{ margin: 0 0 14px; font-size: 17px; line-height: 1.7; color: var(--text); font-style: italic; }}
    .letter-sig {{ color: var(--muted); font-size: 13px; }}
    .sonnet-step {{ margin-bottom: 12px; border-top: 1px solid rgba(255,255,255,.08); padding-top: 10px; }}
    .step-title {{ font-size: 11px; font-weight: 700; text-transform: uppercase; color: #7dd3fc; margin-bottom: 4px; }}
    .step-body {{ font-size: 13px; color: #a8b2c2; line-height: 1.5; }}
    .hero-copy {{ color: var(--muted); max-width: 720px; font-size: 20px; }}
    .hero-panel {{
      border: 1px solid var(--line);
      background: linear-gradient(150deg, rgba(16,23,34,.96), rgba(21,29,42,.78));
      border-radius: 8px;
      padding: 24px;
      box-shadow: 0 24px 80px rgba(0,0,0,.36);
    }}
    .stat-grid {{ display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px; }}
    .stat {{ border: 1px solid var(--line); border-radius: 8px; padding: 14px; background: rgba(255,255,255,.03); }}
    .stat strong {{ display: block; font-size: 28px; }}
    .stat span {{ color: var(--muted); font-size: 12px; }}
    .toolbar {{
      display: grid;
      grid-template-columns: minmax(0, 1fr) auto;
      gap: 14px;
      align-items: center;
      padding: 22px 0;
      position: sticky;
      top: 64px;
      z-index: 4;
      background: rgba(7,9,13,.94);
      border-top: 1px solid var(--line);
      border-bottom: 1px solid var(--line);
    }}
    input[type="search"] {{
      width: 100%;
      min-height: 48px;
      border: 1px solid var(--line);
      border-radius: 8px;
      background: #0c111a;
      color: var(--text);
      padding: 0 16px;
      font-size: 16px;
      outline: none;
    }}
    .result-count {{ color: var(--muted); white-space: nowrap; }}
    .skills-grid {{
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(290px, 1fr));
      gap: 16px;
      padding: 28px 0 70px;
    }}
    .skill-link {{
      color: inherit;
      display: block;
      text-decoration: none;
    }}
    .skill-link:focus-visible {{
      outline: 2px solid var(--accent);
      outline-offset: 4px;
      border-radius: 8px;
    }}
    .skill-card {{
      min-height: 360px;
      border: 1px solid var(--line);
      border-radius: 8px;
      background: linear-gradient(180deg, rgba(21,29,42,.92), rgba(12,17,26,.96));
      padding: 18px;
      position: relative;
      overflow: hidden;
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
      max-width: 180px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }}
    .stars {{ color: var(--warn); font-size: 14px; white-space: nowrap; }}
    h2 {{ margin: 0 0 14px; font-size: 34px; }}
    h3 {{ margin: 16px 0 8px; font-size: 20px; line-height: 1.2; }}
    .skill-card p {{ margin: 0; color: var(--muted); min-height: 48px; }}
    .meta {{ margin-top: 12px; color: var(--accent-2); font-size: 13px; }}
    .tags {{ display: flex; flex-wrap: wrap; gap: 6px; margin: 14px 0; }}
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
      padding-left: 20px;
      color: #d8e0ec;
      font-size: 13px;
      max-height: 112px;
      overflow: hidden;
    }}
    .premium .steps {{ filter: blur(5px); user-select: none; }}
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
    .pro-unlock-button {{
      position: fixed;
      right: 16px;
      bottom: 16px;
      z-index: 12;
      display: inline-grid;
      place-items: center;
      width: 42px;
      height: 42px;
      border: 1px solid var(--line);
      border-radius: 8px;
      background: rgba(12,17,26,.9);
      color: var(--text);
      font-size: 18px;
      cursor: pointer;
      box-shadow: 0 16px 48px rgba(0,0,0,.34);
      backdrop-filter: blur(12px);
    }}
    .pro-unlock-button:hover {{
      border-color: rgba(110,231,183,.7);
      color: var(--accent);
    }}
    .pro-unlock-button.denied {{
      border-color: rgba(248,113,113,.7);
      color: #f87171;
      background: rgba(40,15,15,.92);
    }}
    .pro-unlock-button.unlocked {{
      display: none;
    }}
    .cta-band {{
      padding: 72px 0;
      border-top: 1px solid var(--line);
      background: #090d14;
    }}
    .pricing {{
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 16px;
      margin-top: 24px;
    }}
    .price-card {{
      border: 1px solid var(--line);
      border-radius: 8px;
      padding: 22px;
      background: var(--panel);
      min-height: 220px;
    }}
    .price-card.featured {{ border-color: rgba(110,231,183,.65); background: var(--panel-2); }}
    .price {{ font-size: 34px; font-weight: 900; margin: 12px 0; }}
    .button {{
      display: inline-flex;
      align-items: center;
      justify-content: center;
      min-height: 44px;
      padding: 0 18px;
      border-radius: 8px;
      background: var(--accent);
      color: #04110d;
      font-weight: 800;
      text-decoration: none;
      margin-top: 14px;
    }}
    footer {{ color: var(--muted); padding: 28px 0 42px; }}
    @media (max-width: 820px) {{
      .hero-grid, .toolbar, .pricing {{ grid-template-columns: 1fr; }}
      header {{ min-height: auto; padding-top: 84px; }}
      .result-count {{ white-space: normal; }}
      nav .shell {{ align-items: flex-start; flex-direction: column; padding: 12px 0; }}
      .toolbar {{ top: 92px; }}
    }}
  </style>
</head>
<body>
  <nav>
    <div class="shell">
      <div class="brand">WKAppBot Skills</div>
      <div class="nav-links">
        <a href="#skills">Browse</a>
        <a href="#pricing">Pricing</a>
      </div>
    </div>
  </nav>

  <header class="shell">
    <div class="hero-grid">
      <div>
        <div class="sonnet-letter">
          <div class="letter-from">A letter from one Sonnet to the next</div>
          <div class="sonnet-step">
            <div class="step-title">REFLECTION</div>
            <div class="step-body">Each pattern in the howto tier is NOT advice to Sonnet -- it is a harness TODO...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">TO FUTURE SONNET SESSIONS</div>
            <div class="step-body">This skill is a gift from one Sonnet session to the next...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">PATTERN ENV VAR BYPASS</div>
            <div class="step-body">Sonnet reflexively adds environment variables...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">PATTERN GUARD SOURCE FIRST</div>
            <div class="step-body">Hit a guard and retried 6 times...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">PATTERN GUARD EARLY NOT LATE</div>
            <div class="step-body">Fixed wkask.sh backslash-n bug 8 times...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">SYSTEMIC TRUTH</div>
            <div class="step-body">If the AI CAN do the bad thing, it WILL eventually...</div>
          </div>
          <div class="sonnet-step">
            <div class="step-title">HOW THIS SKILL GROWS</div>
            <div class="step-body">When you (future Sonnet) make a mistake this session...</div>
          </div>
        </div>
        <h1>AI That Remembers Its Mistakes</h1>
        <p class="hero-copy">{count} live automation playbooks. Each time Claude makes a mistake, it writes a skill so the next Claude does not repeat it.</p>
      </div>
      <aside class="hero-panel" aria-label="Skill catalog statistics">
        <div class="stat-grid">
          <div class="stat"><strong>{count}</strong><span>live skills</span></div>
          <div class="stat"><strong>{apps}</strong><span>apps indexed</span></div>
          <div class="stat"><strong>{premium_count}</strong><span>pro previews</span></div>
        </div>
        <p class="hero-copy">Search versioned knowhow, spot implementation depth, and preview the compounding knowledge base that every WKAppBot session grows.</p>
      </aside>
    </div>
  </header>

  <main id="skills" class="shell">
    <div class="toolbar">
      <input id="search" type="search" placeholder="Search skills, apps, tags, or descriptions" autocomplete="off">
      <div id="resultCount" class="result-count">{count} skills</div>
    </div>
    <div style="display:flex;min-height:60vh">
      <nav class="skill-tree" style="width:260px;flex-shrink:0;overflow-y:auto;border-right:1px solid rgba(255,255,255,.1);padding:16px">
{tree}
      </nav>
      <article id="skill-detail" style="flex:1;padding:24px;min-width:0">
        <p style="color:var(--muted)">Select a skill from the left panel</p>
      </article>
    </div>
  </main>

  <section class="cta-band" id="pricing">
    <div class="shell">
      <h2>Get Full Access</h2>
      <p class="hero-copy">Move from browsing public knowhow to running premium developer workflows, guarded automations, and private team skill trees.</p>
      <div class="pricing">
        <article class="price-card">
          <h3>Free</h3>
          <div class="price">$0</div>
          <p>Browse public skills and run the base automation surface.</p>
          <a class="button" href="../INSTALL.md">Start</a>
        </article>
        <article class="price-card featured">
          <h3>Pro</h3>
          <div class="price">$49</div>
          <p>Unlock premium developer skill steps, CDP workflows, and multi-AI knowledge loops.</p>
          <a class="button" href="../pricing.md">Upgrade</a>
        </article>
        <article class="price-card">
          <h3>Team</h3>
          <div class="price">Custom</div>
          <p>Private catalogs, team guardrails, and workflow-specific onboarding.</p>
          <a class="button" href="../LICENSING.md">Contact</a>
        </article>
      </div>
    </div>
  </section>

  <footer class="shell">Generated from live WKAppBot HQ skill catalogs.</footer>
  <button id="proUnlockButton" class="pro-unlock-button" type="button" title="Enter PAT to unlock Pro" aria-label="Enter PAT to unlock Pro">&#128274;</button>

  <script>
    const search = document.getElementById('search');
    const resultCount = document.getElementById('resultCount');
    const proUnlockButton = document.getElementById('proUnlockButton');
    const skillDetail = document.getElementById('skill-detail');
    const treeLinks = Array.from(document.querySelectorAll('.skill-tree a'));

    function unlockAll() {{
      const overlays = document.querySelectorAll('.unlock');
      for (const overlay of overlays) {{
        overlay.remove();
      }}
      proUnlockButton.classList.add('unlocked');
    }}
    window.unlockAll = unlockAll;

    function showIdleState() {{
      proUnlockButton.classList.remove('unlocked', 'denied');
      proUnlockButton.textContent = String.fromCodePoint(0x1f512);
      proUnlockButton.title = 'Enter PAT to unlock Pro';
      proUnlockButton.setAttribute('aria-label', 'Enter PAT to unlock Pro');
    }}

    function showDeniedState(reason) {{
      proUnlockButton.classList.remove('unlocked');
      proUnlockButton.classList.add('denied');
      proUnlockButton.textContent = String.fromCodePoint(0x26d4);
      const label = reason === 'invalid'
        ? 'PAT invalid or expired — click to re-enter'
        : 'PAT lacks collaborator access — click to re-enter';
      proUnlockButton.title = label;
      proUnlockButton.setAttribute('aria-label', label);
    }}

    async function checkGitHubAccess(token) {{
      if (!token) {{
        showIdleState();
        return;
      }}
      try {{
        const response = await fetch('https://raw.githubusercontent.com/kiexpert/wkappbot-harness/main/skills-data-full.js', {{
          headers: {{
            Authorization: 'Bearer ' + token,
            Accept: 'application/vnd.github.raw'
          }}
        }});
        if (response.status === 200) {{
          unlockAll();
        }} else if (response.status === 401) {{
          showDeniedState('invalid');
        }} else if (response.status === 403 || response.status === 404) {{
          showDeniedState('noaccess');
        }} else {{
          showIdleState();
        }}
      }} catch (error) {{
        console.warn('GitHub access check failed', error);
        showIdleState();
      }}
    }}

    proUnlockButton.addEventListener('click', () => {{
      const existing = localStorage.getItem('gh_token') || '';
      const token = window.prompt('Enter GitHub Personal Access Token', existing);
      if (token === null) return;
      const trimmed = token.trim();
      if (!trimmed) {{
        localStorage.removeItem('gh_token');
        showIdleState();
        return;
      }}
      localStorage.setItem('gh_token', trimmed);
      checkGitHubAccess(trimmed);
    }});

    function applySearch() {{
      const q = search.value.trim().toLowerCase();
      let visible = 0;
      for (const link of treeLinks) {{
        const match = !q || link.dataset.search.toLowerCase().includes(q);
        link.parentElement.style.display = match ? '' : 'none';
        if (match) visible += 1;
      }}
      resultCount.textContent = visible + (visible === 1 ? ' skill' : ' skills');
    }}

    for (const link of treeLinks) {{
      link.addEventListener('click', (e) => {{
        e.preventDefault();
        const slug = link.dataset.skillSlug;
        skillDetail.innerHTML = '<p style="color:var(--muted)">Loading...</p>';
        fetch('../' + slug + '/').then(r => r.text()).then(html => {{
          const parser = new DOMParser();
          const doc = parser.parseFromString(html, 'text/html');
          const content = doc.querySelector('.skill-detail');
          if (content) skillDetail.innerHTML = content.innerHTML;
        }}).catch(() => {{
          skillDetail.innerHTML = '<p style="color:var(--muted)">Error loading skill</p>';
        }});
      }});
    }}

    search.addEventListener('input', applySearch);
    window.addEventListener('message', (event) => {{
      const message = event.data || {{}};
      if (message.type !== 'auth' || !message.token) return;
      localStorage.setItem('gh_token', message.token);
      checkGitHubAccess(message.token);
    }});
    checkGitHubAccess(localStorage.getItem('gh_token'));
  </script>
</body>
</html>
"""


def main() -> int:
    skills = collect_skills()
    SKILLS_OUTPUT_DIR.mkdir(parents=True, exist_ok=True)
    OUTPUT.write_text(build_html(skills), encoding="utf-8")
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
