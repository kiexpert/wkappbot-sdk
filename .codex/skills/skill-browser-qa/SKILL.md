---
id: skill-browser-qa
app: wkappbot-sdk
description: "Full QA checklist for docs/skills/ static site: static file checks + localhost runtime verification. Covers Pro gate, SEO safety, T1 parent links, OAuth button, PAT unlock flow, cookie states."
tags: [skill-browser, qa, checklist, pro-gate, seo, cookie, pat, localhost, runtime, static]
---

> **Refresh**: `wkappbot skill read skill-browser-qa --if-newer` — v1.0 (2026-06-03)

# Skill Browser Page QA Checklist

## Steps

1. STATIC CHECKS (no server needed): (1) grep 'githubLogin' index.html -> 0 matches = OAuth removed. (2) grep 'blurred' premium-slug/index.html -> present. (3) grep 'data-full' detail pages -> 0 matches = SEO safe. (4) grep 'Part of:' howto/ref page -> parent link present. (5) grep 'proUnlockButton' non-premium page -> 0 matches. (6) grep 'function unlockAll() {' -> single brace not double {{.
2. BUILD VERIFY: python scripts/build-skill-page.py -> must exit 0, output line 'Generated N skills and N detail pages'. Check N > 400 for full catalog.
3. RUNTIME SETUP: python -m http.server 8888 --directory docs then open http://localhost:8888/skills/ in browser (wkappbot cdp open http://localhost:8888/skills/ or manual).
4. RUNTIME CHECK 1 - no cookie: open detail page of premium skill, confirm blur visible + lock button shown + no step text readable. localStorage.removeItem('gh_token') to reset.
5. RUNTIME CHECK 2 - invalid PAT: click lock button, enter fake token 'bad_token_123'. Confirm lock button turns red (denied state) within 2s. GitHub API returns 401.
6. RUNTIME CHECK 3 - valid PAT (collaborator): click lock button, enter real PAT with repo read scope. Confirm blur removed + green 'Pro access verified' message + lock button hidden.
7. RUNTIME CHECK 4 - PAT persists: reload page after valid unlock. Confirm page auto-unlocks from localStorage without re-entering PAT (checkGitHubAccess runs on load).
8. RUNTIME CHECK 5 - 404 behavior: navigate to http://localhost:8888/skills/nonexistent-skill/ -> Python http.server returns 404 page (expected, no fix needed).
9. RUNTIME CHECK 6 - back link: from detail page click 'Back to skill browser' -> returns to /skills/index.html correctly.
10. KNOWN BUGS HISTORY: (1) {{ double-brace bug: pro_button_and_script was a regular string with f-string escape {{}} -> output literal {{ in HTML JS. Fix: use single { in regular strings. (2) proUnlockButton on all pages: was hardcoded outside conditional -> added if premium check. (3) GitHub OAuth button broken: WKAPPBOT_GITHUB_CLIENT_ID never set -> redirected to GitHub settings instead of OAuth flow -> removed entirely.
