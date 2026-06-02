---
id: wkharness-guards
app: wkappbot-workflow
description: Guard cluster quick-ref for wkharness.ps1. One-liner per guard + escape hatch. Details in howto and ref skills.
tags: [wkharness, guards, pace, ask, kill, skill, cp949, hook, pretooluse, sonnet, delegation]
---

> **Refresh**: `wkappbot skill read wkharness-guards --if-newer` — v1.171 (2026-05-15)

# wkharness-guards

## Steps

1. GUARD CLUSTER G1-G6: G1 ASK-GUARD blocks ask run_in_background=true; G2 KILL-GUARD blocks terminate-cmdlet on wkappbot*; G3 SKILL-SEARCH-NUDGE soft nudge no block; G4 SKILL-EDIT-GUARD blocks .skill.json direct edit; G5 PACE-GUARD blocks Edit/Write when Sonnet%>Total%; G6 CP949-GUARD blocks non-CP949 chars in Write. -> unblock: wkappbot skill read wkharness-guards-howto
2. GUARD CLUSTER G7-G12: G7 AGENT-BRIEF-GUARD blocks Agent with <3 skill reads; G8 OPUS-BUDGET-GUARD blocks Opus at 95%+ budget; G9 STALL-GUARD blocks 300s+10calls solo loop; G10 BG-TASK-GUARD blocks when bg mark >30s; G11 AGENTS-HEAL-GUARD auto-heals AGENTS.md once/session; G12 COMMIT-HOOK-GUARD auto-installs CP949 git hook. -> unblock: wkappbot skill read wkharness-guards-howto
3. AGENT GUARDS: SPEC-GATE blocks Opus Agent without SPEC 1.Goal 2.Files 3.Approach 4.Constraints 5.Exit. CLAUDE-MD-SYNC blocks Agent without ## Pending in project MD. USAGE-TEST-GUARD 50pct Opus redirect to wkclaude.sh. WKCLAUDE-DIRECT blocks direct wkclaude.sh/cmd/ps1 calls.
4. DEADLOCK ESCAPE: Delegation guards fire on Agent/Agent.cmd/wkcodex/wkclaude path ONLY. When blocked: DO WORK DIRECTLY with wkappbot/git/gh/./wk* leading token. skill* ALWAYS exempt from all guards. file-edit wrapper doubly exempt from pace+stall.
5. GUARD CASCADE ORDER: (1) skill* always exempt. (2) file-edit wrapper doubly exempt. (3) wkcodex.sh safe from stall. (4) CronCreate entries need session-start registration. (5) ./wk* camelCase only -- dash after wk breaks gate.
6. SKILL-UPDATE-PENDING: wkappbot file write/edit (non-skill) arms lock. Clear: (1) write first, (2) skill edit to clear, (3) delegate with no intervening write. Read/Grep safe.
7. -> Per-guard unblock steps + full escape procedure: wkappbot skill read wkharness-guards-howto
8. -> RIQUA/history, Codex harness, regex patterns, edge cases: wkappbot skill read wkharness-guards-ref
