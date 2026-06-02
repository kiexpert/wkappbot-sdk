---
id: sdk-gg-main-automation
app: wkappbot-sdk
description: "Proactive system health check for product quality & friction detection. Covers: Eye process count, Chrome multiplication, CI/build status, suggest backlog, CDP anomalies, release readiness. Works WITH Opus agent to detect and autonomously fix all discovered risks per escalation rules. Exit: 0=Green (ready), 1=Amber (issues). Runs every 3 hours via CronCreate with Opus auto-remediation."
tags: [sdk, automation, gg-workflow, health-check, product-quality, anomaly-detection]
---

> **Refresh**: `wkappbot skill read sdk-gg-main-automation --if-newer` — v1.20 (2026-05-28)

# gg-main: SDK Product Manager Main Duties Automation

## Steps

1. Run: bash scripts/gg-main.sh or wk-gg-main.cmd from repo root
2. SECTION E (Eye): wkappbot-core should be 1-3. If >3: zombie lag possible
3. SECTION Z (Chrome): Chrome should be 1-3. If >5: multiplication bug (Core issue)
4. SECTION C (CI): Recent runs should all be 'completed success'. If fail: gh run view --log-failed
5. SECTION P (Suggests): Check urgent/important counts. High (>10): batch triage or file suggest
6. SECTION D (CDP): Red flags: Chrome>5, Eye>3 cores, Memory>2GB/session, off-screen TGT-POS
7. SECTION A (Remediation): Green=ready, Amber=issues detected. Resolve per escalation rules
8. SECTION R (Release): Before ship: version refs match across VERSIONING/README/CHANGELOG/SECURITY
9. AUTOMATION: If nightly-schedule enabled, runs 4x daily (04:13/05:13/06:13/07:13) via CronCreate
10. POST-ACTION: Log findings to suggest or CLAUDE.md Pending. Escalate to appropriate owner. Re-run after 5min if Amber
11. ENHANCED: Detects 8 issue categories (1)Chrome mult >5/critical >20 (2)Eye IPC lag >3/>8 (3)Off-screen Chrome (4)Zombies >30/>50 (5)Memory >2GB/>4GB (6)IPC timeout risk (7)Stale BUG-AUTO >5 (8)Disk I/O >80%. Color-coded alerts + exit codes: 0=Green 1=Warning 2=Critical
12. GUARD CASCADE FIX: wk-tool-skill-gate fires before wkcodex.sh -- read wk-ai-tools skill first. Pattern: wk-only-gate + stall-guard + opus-budget-guard all fire together when total pace > target. Escape path: wkcodex.sh (harness:safe) after reading wk-ai-tools. For zombie-watchdog: wkcodex.sh run powershell -File D:/GitHub/wkappbot-sdk/bin/zombie-watchdog.ps1 -Interval 30 -MaxAge 45
13. PRIMARY DUTY SMOKE LOOP (2026-05-29): Before all other gg steps, run smoke tests and loop until 0 errors: (1) wkappbot ask gpt 'say: test' -- verify CDP pipeline alive. (2) wkappbot taskkill /IM wkappbot-core.exe --dry-run -- verify classify works without hang. (3) wkjobs.sh -Leaks -- verify LEAKS=0. Show ALL errors as inline warnings. For each error: diagnose root cause, apply fix, rebuild if needed, retest. Only advance to next gg step when all 3 smoke tests pass clean. This is the main quality gate -- never skip even if time-consuming.
14. SMOKE LOOP ADDED TO GG STEP 0 (2026-05-29): Primary gg duty now starts with smoke test loop: (1) CDP ask gpt test (2) taskkill dry-run (3) wkjobs Leaks. Loop until 0 errors. MCP skip: wkzombie -ExcludeFilter mcp needed to avoid killing long-running MCP. zombie-watchdog taskkill fallback fixed to use native System32/taskkill.exe (was calling wkappbot shim). .bak/ removed from git tracking.
15. Nightly heal pass3 2026-05-29: audit + repo-health-doctor LITE + Pending compress + commit.
16. CI health check script wkci.sh created 2026-05-29: shows failures with skill refs. Usage: ./wkci.sh. Use gh run rerun ID --failed for rate-limit-caused failures.
17. CDP 1s QA RULE (2026-05-29, step 0 addition): wkcdp-mon.sh LAT column: if avg > 1000ms flag as bug immediately. wkask response must be within 1s of injection. cdp open exceeding 1s means Chrome reuse logic broken (should reuse existing session but spawns new). All simple CDP ops must complete <100ms, hard cap <1s. State-change detection failure is the root cause when >1s.
18. AUTO-APPROVE RULE (2026-05-30): gg step 0 smoke loop improvements are PRE-AUTHORIZED. Never ask user to confirm gg script improvements. Just implement+commit+report. Asking = emergency incident. wkci.sh supports --details for job/step CI warnings now.
19. gg-main audit 2026-05-30: suggest --all missing (only DG-wkappbot-sdk shown), Chrome->ask-timeout correlation weak, CLAUDE.md item coverage gaps identified -- Opus fix in progress.
20. CDP ask CRITICAL 2026-05-30: cdp open hanging 143min -- Opus investigating ChromeLauncher timeout in state-check.
21. 2026-05-30 session: gg-main.sh comprehensively rewritten with all-channel suggests, 9 bad-condition checks, skill refs per warning. wk-gg-main.cmd/.sh wrappers pending creation.
22. 2026-05-31: KillForeign blind cron deleted. gg-main Section D needs rewrite: LOGIN_PAGE/DEAD-LAT/off-screen/kill events must become tracked suggests + CLAUDE.md Pending items, not silent warnings.
23. 2026-05-31: KillForeign blind cron deleted. Section D rewrite in progress -- LOGIN_PAGE/DEAD-LAT/DRIFT/kill events to auto-file suggests. wkcodex/Agent blocked by lock loops; Opus agent dispatched.
