---
id: agent-dispatch-preflight
app: wkappbot-workflow
description: 5-step checklist before every Agent() call. Prevents claude-md-sync / delegation / pace / brief guard failures that waste tokens on repeated blocked attempts.
tags: [agent, preflight, checklist, delegation, guard, claude-md-sync, pace, brief, run-in-background, mandatory, session, claude-md]
---

> **Refresh**: `wkappbot skill read agent-dispatch-preflight --if-newer` — v1.2 (2026-06-03)

# Agent() Dispatch Pre-flight Checklist

## Steps

1. GUARD 1 - claude-md-sync-guard: Edit CLAUDE.md to add Pending entry BEFORE any Agent() call. Guard fires if no Pending edit in last ~30 turns. FIX: Edit CLAUDE.md first (add '- [ ] task'), then retry.
2. GUARD 2 - delegation guard: Agent() MUST have run_in_background: true. Foreground Agent() blocks user prompt immediately. No exceptions ever.
3. GUARD 3 - agent-brief-guard: Agent prompt MUST contain 3+ wkappbot skill read lines. Without them harness blocks spawn. Prepend skill reads before the actual task description.
4. GUARD 4 - pace guard (Sonnet-hogging): After 5+ consecutive Edit/Bash calls, next Edit is blocked. Stop retrying direct edits immediately, switch to Agent() with full task spec.
5. STEP 0 (MANDATORY BEFORE CLAUDE.MD EDIT): Read CLAUDE.md FIRST before attempting to Edit it. If you skip Read and go straight to Edit, it will fail with 'File content has changed'. Sequence is always: Read -> Edit (add Pending) -> Agent().
6. PRE-FLIGHT SEQUENCE (run before every Agent call): (0) Read CLAUDE.md. (1) Edit CLAUDE.md to add Pending entry. (2) Confirm run_in_background: true is in the Agent call. (3) Confirm prompt starts with 3 skill reads. Then fire Agent().
7. FAILURE PATTERN this session: Edit->pace-block, Agent.cmd->delegation-block, Agent()->sync-guard-block, read-CLAUDE.md->edit-CLAUDE.md->sync-guard-pass, Agent(background)->SUCCESS. 4 attempts = ~4x token waste. This checklist prevents it.
8. usage-test-guard DEADLOCK: usage-test-guard (50% random) redirects Agent() to Agent.cmd. Agent.cmd foreground is then blocked by -delegation guard. This creates a deadlock. Resolution: do NOT try Agent.cmd -- just retry Agent() with run_in_background:true. 50% rate means next attempt has 50% chance of passing. After 2 Agent.cmd fails, always retry Agent() directly.
