---
id: wkappbot-public-launch-checklist
app: wkappbot
description: Pre-launch checklist before making wkappbot public + ongoing self-heal checks to verify the repo stays customer-ready. Run before public release and on every major update.
tags: [wkappbot, launch, checklist, public, license, self-heal, distribution]
---

> **Refresh**: `wkappbot skill read wkappbot-public-launch-checklist --if-newer` — v1.2 (2026-04-24)

# WkAppBot Public Launch Readiness Checklist

## Steps

1. PRE-LAUNCH SECURITY: (1) secrets scan -- no API keys/tokens/account numbers in source. (2) SALT + bucket folder names NOT in source (binary only). (3) No internal parameter leaks (FIRE_BUDGET, account numbers, strategy logic). (4) wkappbot skill read public-repo-checklist => audit passed. (5) .gitignore covers .env, *.key, hantoo_cache, wk-cache.
2. PRE-LAUNCH STRUCTURE: (1) launcher source separated from core binary -- launcher repo contains ONLY auth+license+download+autoupdate logic. (2) licenses.json created with ~100 dummy hashes, committed to public repo. (3) GitHub Action cron: delete license entries older than 30 days. (4) Beginner manual written: GitHub account -> gh CLI install -> wkappbot install -> Free tier. (5) README covers Free vs Pro vs Business vs Enterprise tiers.
3. PRE-LAUNCH LICENSE SYSTEM: (1) licenses.json HTTP GET works from raw.githubusercontent.com. (2) sha256(github_id + SALT) % 100 bucket logic tested. (3) Expired entry returns Free tier gracefully. (4) Revoked entry (deleted) returns Free tier gracefully. (5) gh api user --jq .login returns correct ID on test machine.
4. SELF-HEAL CHECKS (run ongoing): H1=licenses.json accessible via raw URL (HTTP 200). H2=no real customer hash accidentally deleted in last commit. H3=GitHub Action cron ran within last 35 days. H4=dummy hash count still ~99 (real customers not making file too short). H5=launcher binary download URL resolves (latest release asset exists).
5. CUSTOMER EXPERIENCE CHECKS: C1=fresh Windows machine can install launcher in under 5 min. C2=Free tier works without GitHub account (graceful fallback). C3=expired license shows clear upgrade prompt not a crash. C4=beginner manual URL in launcher --help output. C5=wkappbot skill read handoff-checklist works on fresh install.
6. SUPPORT CHANNEL: set up before launch -- GitHub Issues (public bugs), email kiexpert@kivilab.co.kr (license/billing), optional Slack/Discord for paying customers. Publish support URL in README and onboarding email. SLA: respond within 1 business day for paid tier issues.
7. COMPLETED 2026-04-29 (kiexpert/wkappbot-sdk): (1) git-filter-repo stripped Slack webhook.json, .mcp.json, .wkappbot/, docs/handoff/, .ci-test-tmp/ from full history. (2) AgentsPolicy/AppBotEyePromptInfo hardcoded paths -> env vars. (3) LAUNCHER_SOURCE_SEPARATION suggest filed (not yet done). (4) MIT LICENSE, CONTRIBUTING.md, CODE_OF_CONDUCT.md, SECURITY.md, CHANGELOG.md, PRICING.md, SUBSCRIBE.md, SUPPORT.md, INSTALL.md, ARCHITECTURE.md, TROUBLESHOOTING.md, FAQ.md, CI.md, LICENSING.md, UPGRADE.md, THIRD-PARTY-NOTICES.md, LICENSE-CORE.md all added. (5) CodeQL, Dependabot, lint, unit-tests, security-scan workflows added. (6) GitHub Discussions, Issue templates x3, PR template, branch protection, secret scanning enabled. (7) Repository made PUBLIC 2026-04-29. (8) Branch protection active (linear history, no force-push). (9) 85/100 checklist items done -- remaining: LAUNCHER_SOURCE_SEPARATION, fresh VM test, marketing content.
