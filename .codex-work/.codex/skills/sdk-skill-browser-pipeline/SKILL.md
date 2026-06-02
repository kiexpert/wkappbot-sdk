---
id: sdk-skill-browser-pipeline
app: wkappbot-sdk
description: "Full pipeline to generate a live marketing skill browser webpage from wkappbot HQ skill catalog. Reads 416 public skills, masks .env secrets, 44-char preview truncation with GitHub OAuth Pro unlock, split-panel UI, auto-rebuilds via GHA on push."
tags: [sdk, skill-browser, marketing, github-pages, pipeline, gha, pro-unlock]
---

> **Refresh**: `wkappbot skill read sdk-skill-browser-pipeline --if-newer` — v1.3 (2026-06-02)

# SDK Skill Browser Pipeline

## Steps

1. RUN: python D:/GitHub/wkappbot-sdk/scripts/build-skill-page.py -- reads HQ catalog, filters wkappbot-* apps only, masks .env values, outputs docs/skills/index.html
2. STRUCTURE: docs/index.html (wkappbot product intro) + docs/skills/index.html (split-panel skill browser with Sonnet reflection hero). GitHub Pages source: main branch /docs
3. SPLIT-PANEL UI: left panel = skill list with category accordion + search. Right panel = skill detail on click. Pro skills show 44-char preview with blur overlay and GitHub OAuth unlock.
4. PRIVACY: app allowlist (wkappbot-* only, excludes personal-docs/wkautoquant/invest-kr). .env values masked at build time. JSON schema hidden in skills-data.js variable.
5. GHA AUTO-REBUILD: .github/workflows/build-skill-page.yml triggers on push to main. Also triggers via repository_dispatch from Core repo push (skill-catalog-updated event).
6. PRO UNLOCK: JS checks GitHub OAuth token in localStorage. Fetches api.github.com/repos/kiexpert/wkappbot-harness/collaborators. Collaborator = Pro subscriber = full step content unlocked.
7. HERO SECTION: Sonnet reflection quote from sonnet-bug-stop-policy-ref + AK Platform pitch from wkappbot-the-artificial-knowledge-platform. Scroll down to skill browser.
8. SITE: https://kiexpert.github.io/wkappbot-sdk/skills/
9. QA CHECK: bash scripts/check-skill-browser.sh -- curl-based smoke test (no CDP needed, public page). Checks: HTTP 200 for /skills/ and /, skill card count > 10, Sonnet/harness hero text, Pro lock elements, local docs/skills/index.html freshness. Exit 0 = all pass. Add to gg-main ISSUE 9 section. Run after every push to verify Pages deployment.
10. SKILL TREE QA (check-skill-tree.sh): wkappbot skill list extracts IDs via sed pattern s/.*(\([^)]*\)).*/\1/ NOT awk. Fixed pattern for T2/T3: grep -E -(howto|ref|t23-) then sed extract. Script finds skills missing parent T1 backlink. Run: bash scripts/check-skill-tree.sh
11. AUTO-HEAL backlinks: when check-skill-tree.sh finds NO_PARENT_LINK, auto-fix via: wkappbot skill edit ID --add-step 'Parent T1: wkappbot skill read BASE'. Script becomes self-healing: find orphan + auto-add parent step + report FIXED. Same pattern as wkappbot self-healing principle -- document AND enforce.
