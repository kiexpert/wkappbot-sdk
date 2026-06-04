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




def build_nav_tree(all_skills: list[dict[str, Any]]) -> str:
    """Generate compact navigation tree HTML grouped by app."""
    if not all_skills:
        return ""
    skills_by_app: dict[str, list[dict[str, Any]]] = {}
    for skill in all_skills:
        app = skill["app"]
        if app not in skills_by_app:
            skills_by_app[app] = []
        skills_by_app[app].append(skill)

    nav_html = ""
    for app in sorted(skills_by_app.keys()):
        app_slug = skill_page_slug(app)
        app_skills = sorted(skills_by_app[app], key=lambda s: s["title"])
        nav_html += f'<details class="nav-app" open><summary>{e(app)}</summary><ul>'
        for s in app_skills:
            skill_slug = skill_page_slug(s["id"])
            nav_html += f'<li><a href="../{app_slug}/{skill_slug}.html">{e(s["title"])}</a></li>'
        nav_html += '</ul></details>'
    return nav_html


def build_skill_detail_html(skill: dict[str, Any], all_skills: list = None, idx: int = 0) -> str:
    nav_tree_html = build_nav_tree(all_skills) if all_skills else ""
    title = e(skill["title"])
    desc = e(skill["desc"] or "No description yet.")
    app = e(skill["app"])
    app_slug = skill_page_slug(skill["app"])
    audience = e(skill["audience"])
    tags = "".join(f'<span class="chip">{e(tag)}</span>' for tag in skill["tags"])
    premium = bool(skill.get("premium", False))
    skill_id = skill.get("id", "")
    parent_id = infer_parent_skill_id(skill_id)
    parent_link = ""
    if parent_id:
        parent_slug = skill_page_slug(parent_id)
        parent_link = f'<div class="breadcrumb">Part of: <a href="{e(parent_slug)}.html">{e(parent_id)}</a></div>'

    # Build related links (siblings and prev/next)
    related_links_html = ""
    if all_skills:
        # Find sibling skills (same base ID with different tier suffixes)
        sibling_links = []
        for s in all_skills:
            s_id = s.get("id", "")
            if s_id != skill_id and infer_parent_skill_id(s_id) == parent_id and parent_id:
                sibling_slug = skill_page_slug(s_id)
                sibling_title = e(s.get("title", ""))
                sibling_links.append(f'<a href="./{sibling_slug}.html">{sibling_title}</a>')

        # Find prev/next skills within same app
        app_skills = [s for s in all_skills if s.get("app") == skill["app"]]
        app_skills_sorted = sorted(app_skills, key=lambda s: str(s.get("title", "")).lower())

        prev_next_html = ""
        for i, s in enumerate(app_skills_sorted):
            if s.get("id") == skill_id:
                prev_html = ""
                next_html = ""
                if i > 0:
                    prev_skill = app_skills_sorted[i - 1]
                    prev_slug = skill_page_slug(prev_skill.get("id", ""))
                    prev_title = e(prev_skill.get("title", ""))
                    prev_html = f'<a href="./{prev_slug}.html">← {prev_title}</a>'
                if i < len(app_skills_sorted) - 1:
                    next_skill = app_skills_sorted[i + 1]
                    next_slug = skill_page_slug(next_skill.get("id", ""))
                    next_title = e(next_skill.get("title", ""))
                    next_html = f'<a href="./{next_slug}.html">{next_title} →</a>'

                if prev_html or next_html:
                    separator = " | " if (prev_html and next_html) else ""
                    prev_next_html = f'<nav class="skill-nav">{prev_html}{separator}{next_html}</nav>'
                break

        # Combine related sections
        sections = []
        if sibling_links:
            sections.append(f'<div class="related"><strong>Related:</strong> {", ".join(sibling_links)}</div>')
        if prev_next_html:
            sections.append(prev_next_html)

        if sections:
            related_links_html = "\n      ".join(sections)

    steps = skill["steps"] or ["This skill is available in the live WKAppBot catalog."]
    if premium:
        step_items = "".join(f"<li>{e(preview_step(step))}</li>" for step in steps)
        blur_class = " blurred"
        overlay = '<div class="unlock"><div style="margin-bottom:10px;font-size:18px">&#128274; Pro Skill</div><a href="https://kiexpert.github.io/wkappbot-sdk/pricing" style="display:inline-block;padding:8px 20px;background:var(--accent);color:#04110d;font-weight:800;border-radius:6px;text-decoration:none;font-size:14px">Get Pro Access &rarr;</a></div>'
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
      const existing = sessionStorage.getItem('gh_token') || '';
      const token = window.prompt('Enter GitHub Personal Access Token', existing);
      if (token === null) return;
      const trimmed = token.trim();
      if (!trimmed) {
        sessionStorage.removeItem('gh_token');
        showIdleState();
        return;
      }
      sessionStorage.setItem('gh_token', trimmed);
      checkGitHubAccess(trimmed);
    });
    checkGitHubAccess(sessionStorage.getItem('gh_token'));
  </script>"""
        pro_button_and_script = pro_button_and_script.replace('__SKILL_ID__', e(skill_id))

    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>{title} | WKAppBot Skills</title>
  <meta name="description" content="{e(truncate(str(skill['desc']), 200))}">
  <link rel='canonical' href='https://kiexpert.github.io/wkappbot-sdk/skills/{app_slug}/{e(skill_page_slug(skill_id))}.html'>
  <script type='application/ld+json'>{{"@context":"https://schema.org","@type":"TechArticle","name":"{e(skill["title"])}","description":"{e(skill["desc"][:120])}"}}</script>
  <meta property="og:title" content="{title} | WKAppBot Skills">
  <meta property="og:description" content="{e(truncate(str(skill['desc']), 200))}">
  <meta property="og:type" content="article">
  <meta property="og:url" content="https://kiexpert.github.io/wkappbot-sdk/skills/{app_slug}/{e(skill_page_slug(skill_id))}.html">
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
    .skill-footer {{
      margin-top: 32px;
      padding-top: 16px;
      border-top: 1px solid var(--line);
    }}
    .skill-nav {{
      margin: 12px 0;
      font-size: 14px;
    }}
    .skill-nav a {{
      color: var(--accent-2);
      text-decoration: none;
    }}
    .skill-nav a:hover {{
      text-decoration: underline;
    }}
    .related {{
      margin: 8px 0;
      font-size: 14px;
    }}
    .related a {{
      color: var(--accent-2);
      text-decoration: none;
    }}
    .related a:hover {{
      text-decoration: underline;
    }}
    #sidebar-toggle {{
      position: fixed;
      top: 16px;
      left: 16px;
      z-index: 100;
      padding: 6px 12px;
      background: #f5f5f5;
      border: 1px solid #ddd;
      border-radius: 6px;
      cursor: pointer;
      font-size: 13px;
      color: #333;
      font-weight: 600;
    }}
    #sidebar-toggle:hover {{
      background: #e8e8e8;
    }}
    #sidebar {{
      position: fixed;
      top: 0;
      left: -300px;
      width: 280px;
      height: 100vh;
      background: #fff;
      border-right: 1px solid #eee;
      overflow-y: auto;
      padding: 16px;
      transition: left .2s;
      z-index: 200;
      box-shadow: 2px 0 8px rgba(0,0,0,.1);
    }}
    #sidebar.open {{
      left: 0;
    }}
    body.sidebar-open {{
      margin-left: 280px;
    }}
    #nav-search {{
      width: 100%;
      margin-bottom: 12px;
      padding: 6px 8px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 13px;
      font-family: inherit;
    }}
    #sidebar-close {{
      float: right;
      background: none;
      border: none;
      cursor: pointer;
      font-size: 16px;
      margin-bottom: 8px;
      color: #333;
    }}
    #sidebar-close:hover {{
      color: #000;
    }}
    .nav-app {{
      margin-bottom: 8px;
    }}
    .nav-app summary {{
      font-weight: 700;
      font-size: 12px;
      text-transform: uppercase;
      cursor: pointer;
      padding: 4px 0;
      color: #666;
    }}
    .nav-app summary:hover {{
      color: #333;
    }}
    .nav-app ul {{
      list-style: none;
      padding: 0 0 0 12px;
      margin: 4px 0;
    }}
    .nav-app li a {{
      display: block;
      padding: 3px 4px;
      font-size: 13px;
      color: #333;
      text-decoration: none;
    }}
    .nav-app li a:hover {{
      color: #0070f3;
    }}
    .nav-app li a.current {{
      color: #0070f3;
      font-weight: 600;
    }}
    @media (max-width: 640px) {{
      .card-top {{ align-items: flex-start; flex-direction: column; }}
      #sidebar {{
        width: 100%;
        left: -100%;
      }}
      #sidebar-toggle {{
        top: 8px;
        left: 8px;
      }}
    }}
  </style>
</head>
<body>
  <div id="sidebar" class="sidebar">
    <button id="sidebar-close" onclick="toggleSidebar()">✕</button>
    <input id="nav-search" type="search" placeholder="Search skills..." oninput="filterNav(this.value)">
    <nav id="nav-tree">
      {nav_tree_html}
    </nav>
  </div>
  <button id="sidebar-toggle" onclick="toggleSidebar()">☰ Browse Skills</button>
  <main class="shell detail-page">
    <a class="back-link" href="./">← Back to {app}</a>
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
      <footer class="skill-footer">
        {related_links_html}
        <p style="color:var(--muted);font-size:12px;margin-top:24px;margin-bottom:0;">Generated from live WKAppBot HQ skill catalog.</p>
      </footer>
    </article>
  </main>
  {pro_button_and_script}
  <script>
    if (window.self !== window.top) {{ var t = document.getElementById('sidebar-toggle'); if(t) t.style.display='none'; }}
    function toggleSidebar(){{
      var s=document.getElementById('sidebar');
      var open=s.classList.toggle('open');
      document.body.classList.toggle('sidebar-open',open);
      localStorage.setItem('sb_open',open?'1':'0');
    }}
    function filterNav(q){{
      q=q.toLowerCase();
      document.querySelectorAll('#nav-tree li').forEach(function(li){{
        var a=li.querySelector('a');
        var show=!q||a.textContent.toLowerCase().includes(q);
        li.style.display=show?'':'none';
      }});
    }}
    (function(){{
      if(localStorage.getItem('sb_open')==='1'){{
        document.getElementById('sidebar').classList.add('open');
        document.body.classList.add('sidebar-open');
      }}
      var cur=location.pathname.split('/').pop();
      document.querySelectorAll('#nav-tree a').forEach(function(a){{
        if(a.href.endsWith(cur))a.classList.add('current');
      }});
    }})();
  </script>
</body>
</html>
"""


def build_html(skills: list[dict[str, Any]]) -> str:
    """Generate app navigation index page only."""
    skills_by_app: dict[str, list[dict[str, Any]]] = {}
    for skill in skills:
        app = skill["app"]
        if app not in skills_by_app:
            skills_by_app[app] = []
        skills_by_app[app].append(skill)

    app_list = ""
    for app in sorted(skills_by_app.keys()):
        app_slug = skill_page_slug(app)
        app_count = len(skills_by_app[app])
        app_list += f'<li class="app-item"><h2><a href="{app_slug}/">{e(app)}</a></h2><p>{app_count} skills</p></li>'

    total = len(skills)
    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>WKAppBot Skills</title>
  <meta name="description" content="AI automation playbooks organized by application. Browse {total} skills total.">
  <link rel="canonical" href="https://kiexpert.github.io/wkappbot-sdk/skills/">
  <meta property="og:title" content="WKAppBot Skills">
  <meta property="og:description" content="Browse {total} live AI automation playbooks">
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
    .shell {{ width: min(900px, calc(100% - 32px)); margin: 0 auto; padding: 0 16px; }}
    header {{
      border-bottom: 1px solid var(--line);
      padding: 40px 0;
      margin-bottom: 40px;
    }}
    header h1 {{
      margin: 0 0 8px;
      font-size: 32px;
      font-weight: 800;
    }}
    header p {{
      margin: 0;
      color: var(--muted);
      font-size: 16px;
    }}
    .app-list {{
      list-style: none;
      padding: 0;
      margin: 0;
    }}
    .app-item {{
      margin: 16px 0;
      padding: 20px;
      border: 1px solid var(--line);
      border-radius: 8px;
      background: rgba(16,23,34,.5);
    }}
    .app-item h2 {{
      margin: 0 0 6px;
      font-size: 18px;
      font-weight: 700;
    }}
    .app-item a {{
      color: var(--accent);
    }}
    .app-item p {{
      margin: 0;
      color: var(--muted);
      font-size: 14px;
    }}
    footer {{
      color: var(--muted);
      font-size: 12px;
      padding: 40px 0;
      border-top: 1px solid var(--line);
    }}
  </style>
</head>
<body>
  <div class="shell">
    <header>
      <nav><a href="../">WKAppBot</a> / Skills</nav>
      <h1>WKAppBot Skills</h1>
      <p>AI automation playbooks organized by application. {total} skills total.</p>
    </header>
    <ul class="app-list">
{app_list}
    </ul>
    <footer>
      <p>Generated from live WKAppBot HQ skill catalog. <a href="../">← Back to home</a></p>
    </footer>
  </div>
</body>
</html>
"""


def build_app_index(app_name: str, app_skills: list[dict[str, Any]]) -> str:
    """Generate per-app skill listing page."""
    app_slug = skill_page_slug(app_name)
    count = len(app_skills)

    skill_items = ""
    for skill in sorted(app_skills, key=lambda s: s["title"]):
        skill_slug = skill_page_slug(skill["id"])
        desc = truncate(skill["desc"], 120) if skill["desc"] else "No description."
        skill_items += f'<li class="skill-item"><a href="{skill_slug}.html">{e(skill["title"])}</a><p>{e(desc)}</p></li>'

    return f"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>{e(app_name)} Skills — WKAppBot</title>
  <meta name="description" content="{count} automation skills for {e(app_name)}.">
  <link rel="canonical" href="https://kiexpert.github.io/wkappbot-sdk/skills/{app_slug}/">
  <meta property="og:title" content="{e(app_name)} Skills — WKAppBot">
  <meta property="og:description" content="Browse {count} automation skills">
  <meta property="og:type" content="website">
  <meta property="og:url" content="https://kiexpert.github.io/wkappbot-sdk/skills/{app_slug}/">
  <style>
    :root {{
      color-scheme: dark;
      --bg: #07090d;
      --text: #f4f7fb;
      --muted: #a8b2c2;
      --line: rgba(255,255,255,.12);
      --accent: #6ee7b7;
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
    .shell {{ width: min(900px, calc(100% - 32px)); margin: 0 auto; padding: 0 16px; }}
    header {{
      border-bottom: 1px solid var(--line);
      padding: 40px 0;
      margin-bottom: 40px;
    }}
    header nav {{
      font-size: 13px;
      color: var(--muted);
      margin-bottom: 16px;
    }}
    header nav a {{
      color: var(--accent);
    }}
    header h1 {{
      margin: 0;
      font-size: 32px;
      font-weight: 800;
    }}
    .skill-list {{
      list-style: none;
      padding: 0;
      margin: 0;
    }}
    .skill-item {{
      margin: 12px 0;
      padding: 14px 0;
      border-bottom: 1px solid var(--line);
    }}
    .skill-item:last-child {{
      border-bottom: none;
    }}
    .skill-item a {{
      display: block;
      font-size: 16px;
      font-weight: 600;
      color: var(--accent);
      margin-bottom: 4px;
    }}
    .skill-item p {{
      margin: 0;
      font-size: 13px;
      color: var(--muted);
    }}
    footer {{
      color: var(--muted);
      font-size: 12px;
      padding: 40px 0;
      border-top: 1px solid var(--line);
    }}
  </style>
</head>
<body>
  <div class="shell">
    <header>
      <nav><a href="../">WKAppBot Skills</a> / {e(app_name)}</nav>
      <h1>{e(app_name)}</h1>
    </header>
    <ul class="skill-list">
{skill_items}
    </ul>
    <footer>
      <p>Generated from live WKAppBot HQ skill catalog. <a href="../">← Back to skills</a></p>
    </footer>
  </div>
</body>
</html>
"""




def main() -> int:
    skills = collect_skills()
    SKILLS_OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

    # Build main index (app navigation only)
    OUTPUT.write_text(build_html(skills), encoding="utf-8")

    # Group skills by app and build per-app indices + detail pages
    skills_by_app: dict[str, list[dict[str, Any]]] = {}
    for skill in skills:
        app = skill["app"]
        if app not in skills_by_app:
            skills_by_app[app] = []
        skills_by_app[app].append(skill)

    for app_name, app_skills in sorted(skills_by_app.items()):
        app_slug = skill_page_slug(app_name)
        app_dir = SKILLS_OUTPUT_DIR / app_slug
        app_dir.mkdir(parents=True, exist_ok=True)

        # Build per-app index
        (app_dir / "index.html").write_text(build_app_index(app_name, app_skills), encoding="utf-8")

        # Build individual skill pages
        for i, skill in enumerate(app_skills):
            skill_slug = skill_page_slug(skill["id"])
            (app_dir / f"{skill_slug}.html").write_text(build_skill_detail_html(skill, skills, i), encoding="utf-8")

    # Generate skills-data-full.js with full skill data (id, slug, steps)
    SKILLS_FULL_OUTPUT = SKILLS_OUTPUT_DIR / "skills-data-full.js"
    data = [{"id": s["id"], "slug": s["slug"], "steps": s["steps"]} for s in skills]
    content = "window.SKILLS_FULL=" + json.dumps(data, ensure_ascii=False, separators=(",", ":")) + ";"
    SKILLS_FULL_OUTPUT.write_text(content, encoding="utf-8")

    # Generate sitemap.xml for Google indexing
    SITEMAP_OUTPUT = REPO_ROOT / "docs" / "skills" / "sitemap.xml"
    base = "https://kiexpert.github.io/wkappbot-sdk/skills"
    urls = [f"<url><loc>{base}/</loc></url>"]

    # Add per-app indices to sitemap
    for app_name in sorted(skills_by_app.keys()):
        app_slug = skill_page_slug(app_name)
        urls.append(f"<url><loc>{base}/{app_slug}/</loc></url>")

    # Add individual skill pages (exclude tier suffixes)
    for skill in skills:
        if any(skill["id"].endswith(suffix) for suffix in ("-howto", "-ref", "-t2-howto", "-t3-ref", "-t2", "-t3")):
            continue
        app_slug = skill_page_slug(skill["app"])
        skill_slug = skill_page_slug(skill["id"])
        urls.append(f"<url><loc>{base}/{app_slug}/{skill_slug}.html</loc></url>")

    NL = chr(10)
    sitemap = ('<?xml version="1.0" encoding="UTF-8"?>' + NL
               + '<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">' + NL
               + NL.join(urls) + NL + '</urlset>')
    SITEMAP_OUTPUT.write_text(sitemap, encoding="utf-8")

    total_pages = 1 + len(skills_by_app) + len(skills)
    print(f"Generated {OUTPUT} with {len(skills)} skills, {len(skills_by_app)} app indices, and {total_pages} total pages")
    return 0 if len(skills) > 50 else 1


if __name__ == "__main__":
    raise SystemExit(main())
