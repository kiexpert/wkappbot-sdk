---
id: agent-canonical-dispatch
app: wkappbot-workflow
description: "wkappbot agent 'task' - tier uses Claude-standard canonical flags as universal interface. All AI types (haiku/sonnet/opus/gpt/gemini/codex-mini/triad) share the same arg format. Translation layer converts to each backend. New tiers: gpt, gemini. Session persistence: .wkappbot/agents/last-{tier}.jsonl"
tags: [agent, dispatch, canonical, gpt, gemini, claude, codex]
---

> **Refresh**: `wkappbot skill read agent-canonical-dispatch --if-newer` — v1.3 (2026-04-21)

# Agent command: canonical opts + per-tier translation

## Steps

1. Run agent with canonical opts: wkappbot agent 'task' - opus --max-turns 5
2. Check last task: wkappbot agent last
3. Follow up: wkappbot agent resume 'follow-up question'
4. New tiers: wkappbot agent 'task' - gpt OR wkappbot agent 'task' - gemini
5. Canonical flags: --model <id> --max-turns N --system '...' --no-agent --fresh --attach <file> --timeout N --no-wait
6. Verified 2026-04-21: test-agent-canonical-dispatch.cmd (resolve ts=2026-04-21T04:38)
7. CODEX INVOCATION (2026-05-15): Direct: codex exec --dangerously-bypass-approvals-and-sandbox -C DIR 'task'. Guarded: powershell -File D:/GitHub/wkcodex.ps1 'task' (enforces 3 skill refs, bypass, post-checks). Session ops: codex resume --last (continue), codex fork --last (branch). Windows: --dangerously-bypass MANDATORY (-s modes trigger sandbox spawn error). 3 skill reads required in task string or wkcodex blocks. Sandbox modes: read-only / workspace-write / danger-full-access (only danger-full-access works without bypass on this machine). rc=0 does NOT guarantee success -- check last 5 output lines.
