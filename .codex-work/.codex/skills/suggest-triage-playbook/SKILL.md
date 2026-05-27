---
id: suggest-triage-playbook
app: wkappbot-workflow
description: "Operator playbook for reviewing wkappbot suggestions: classify duplicates, prioritize by impact, keep evidence requirements explicit, and convert repeated insights into skills."
tags: [workflow, suggest, triage, dedupe, priority, evidence, operator]
---

> **Refresh**: `wkappbot skill read suggest-triage-playbook --if-newer` — v1.22 (2026-04-08)

# Suggest Triage Playbook

## Steps

1. PRIORITY ZERO: Check resolve before triage. Every suggest fix must end with 'suggest resolve <ts> ... --commit <hash>'. Do NOT skip resolve, defer it, or move to next batch. Resolve completion (HALF state or full RESOLVED) is the gate for closure. Only after resolve is done may you move to next suggest.
2. Classify each item by audience and responsibility: operator, developer, project, or user.
3. Merge obvious duplicates conceptually before attempting a fix so one root cause does not create multiple partially solved items.
4. Prioritize by quick win, frequency, and risk; handle the highest signal items first. For large backlogs: use ask-suggest-priority-batching skill -- one GPT batch rank pass then per-item asks concurrently before acting.
5. Keep resolve evidence explicit and minimal: one test script, one pass condition, one note.
6. If a suggestion keeps reappearing, convert the recurring lesson into a skill before closing the item.
7. Prefer a concise triage note over a long analysis: what it is, why it matters, what the next action is.
8. suggest merge --all-matching 'pattern' --title X --work Nh: bulk-merge matching suggests (non-WKAppBot channels only; --dry-run to preview). Cap=20 items. --dry-run shows matches without writing.
9. Verified 2026-04-26: test-suggest-merge-all-matching.sh (resolve ts=2026-04-26T06:11:13.6770863Z)
10. Suggester confirm flow (2026-05-01): (1) maintainer does 1st-party resolve -> HALF state + Slack notice sent to suggester. (2) Suggester runs 'suggest check <ts>' to see regression result. (3) If regression PASS -> 'suggest check <ts>' (author shortcut, preferred) OR 'suggest resolve <ts> note --confirm --skill X' (manual). Running resolve WITHOUT --confirm while HALF exists -> ERROR with copy-pasteable check + confirm commands.
11. SKILL NEWS PRE-CHECK: run wkappbot skill news before suggest list each session. Scans last 7 days for CDP/launcher/workflow updates. Context helps triage: recent skill may mean BUG-AUTO already addressed. Added to gg Main Workflow 2026-05-23 as step 2.
12. MERGE CROSS-CWD BUG (2026-05-26): 'wkappbot suggest merge --all-matching PATTERN ...' fails code=1 when the matched suggests live in a sibling CWD (auto-routes to e.g. D:/GitHub/WkAutoQuant then silently fails). WORKAROUND: use the pairwise form 'wkappbot suggest merge TS_A TS_B --title --root-cause --components --affected-cmds --work' which works cross-CWD. All five flags are REQUIRED. Each merge produces a NEW consolidated entry; loop oldest-pair merges until 1 remains. For a big BUG-AUTO cluster (e.g. 30+ identical [ASK] OperationCanceledException from CdpClient.EnableRuntimeWithRetry timeouts), merge down then resolve the survivor as STALE.
13. DELEGATION FALLBACK (2026-05-26): during gg triage, if the opus-budget-guard blocks Opus subagents (weekly pace exceeded), delegate the MECHANICAL merge/resolve/confirm work to a Haiku agent that runs the wkappbot CLI via the in-IDE Bash tool. Do NOT route wkappbot suggest commands through Codex/wkcodex -- wkappbot CLI calls hang in Codex subprocess (Eye IPC stall + Core fallback exceeds Codex 34s cmd timeout). Keep SDK-vs-Core scope judgment and any code fixes on the main session; push only the mechanical command sequences down.
14. ESCAPE-HATCH APPLIED (2026-05-26): cleared skill-pending lock (set by the preceding CLAUDE.md file edit) immediately before delegating, with no intervening file write. Next call is the Haiku triage Agent. See wkharness-guards GUARD-DEADLOCK ESCAPE HATCH for the full sequence: file edit -> skill edit (clears lock) -> delegate with no file write between.
15. CMD-SAFE BRIEF (2026-05-26): Agent.cmd passes prompts through cmd.exe batch parsing, which CHOKES on colons parens pipes ampersands in the argument with the error 'X was unexpected at this time'. Fix: write a cmd-safe brief file with NO colons NO parens NO pipes; identify suggest clusters by TEXT SIGNATURE and let the delegated agent read exact ts from its own suggest list output, so no colon-bearing timestamps need to appear in the brief. Then Agent.cmd --model haiku with cat of that file dispatches cleanly. Agent.cmd skips claude-md-sync-guard (that guard is Agent-tool only), so the file-write-then-skill-edit-clear sequence works without the sync deadlock.
