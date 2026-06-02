---
id: sdk-gg-main-automation
app: wkappbot-sdk
description: "Proactive system health check for product quality & friction detection. Covers: Eye process count, Chrome multiplication, CI/build status, suggest backlog, CDP anomalies, release readiness. Works WITH Opus agent to detect and autonomously fix all discovered risks per escalation rules. Exit: 0=Green (ready), 1=Amber (issues). Runs every 3 hours via CronCreate with Opus auto-remediation."
tags: [sdk, automation, gg-workflow, health-check, product-quality, anomaly-detection]
---

> **Refresh**: `wkappbot skill read sdk-gg-main-automation --if-newer` — v1.27 (2026-05-28)

# gg-main: SDK Product Manager Main Duties Automation

## Steps

1. Run: bash scripts/gg-main.sh or wk-gg-main.cmd from repo root
2. SECTION E (Eye): wkappbot-core should be 1-3. If >3: zombie lag possible
3. SECTION Z (Chrome): Chrome should be 1-3. If >5: multiplication bug (Core issue)
4. SECTION C (CI): Recent runs should all be completed success. If fail: gh run view --log-failed
5. SECTION P (Suggests): Check urgent/important counts. High (>10): batch triage or file suggest
6. SECTION D (CDP): Red flags Chrome>5, Eye>3 cores, Memory>2GB/session, off-screen TGT-POS
7. SECTION A (Remediation): Green=ready, Amber=issues detected. Resolve per escalation rules
8. SECTION R (Release): Before ship verify version refs match VERSIONING/README/CHANGELOG/SECURITY
9. ENHANCED: 8 issue categories (1)Chrome mult >5/crit>20 (2)Eye lag >3/>8 (3)Off-screen Chrome (4)Zombies >30/>50 (5)Memory >2GB/>4GB (6)IPC timeout (7)Stale BUG-AUTO >5 (8)Disk I/O >80%. Exit: 0=Green 1=Warn 2=Crit. See howto T2 smoke-loop T3 history.
10. 2026-06-02: wkappbot-the-artificial-knowledge-platform skill added to README.md pitch section. Resolving suggest ts=2026-06-02T13:55:57
11. 2026-06-02 marketing: build-skill-page.py automation script + docs/index.html + GHA workflow pending Codex
