---
id: agent-cmd
app: harness
description: "Agent.cmd is the one dispatcher for every delegation: the model flag routes to Claude tiers, codex, gemini, and the web-ask family (ask-gpt, ask-triad). It is also the single clean signal the recursive-dialectic evidence-gate counts."
tags: [harness, delegation, agent, dispatch, evidence, tiers]
---

> **Refresh**: `wkappbot skill read agent-cmd --if-newer` — v1.0 (2026-06-03)

# Agent.cmd: universal delegation front door

## Steps

1. WHAT: Agent.cmd is the ONE front door for every delegation. Pick the tier with the model flag. When the caller is Opus it runs in background by default, because foreground delegation is blocked so the user gets an immediate reply.
2. MODELS: model haiku, sonnet, opus route to Claude tiers; codex, codex-mini, gpt-mini route to codex; gemini, flash route to gemini; ask-gpt, ask-gemini, ask-triad, ask-groq, ask-cerebras route to the web intelligences (the wkappbot ask family). One flag, every brain.
3. EVIDENCE: the recursive-dialectic gate counts ONLY Agent.cmd cheap dispatches, one clean pattern, no messy raw-command scan. Delegate cheap work through Agent.cmd to build the evidence that unlocks Sonnet and Opus. See harness-token-waste-tracker and the cheap-evidence pool.
4. BRIEF: every brief needs 3 wkappbot skill read refs (agent-brief-guard). A multiline or non-ASCII brief MUST use a bash heredoc with a quoted delimiter, or the file flag with a utf8 path, because the plain args path mangles newlines and the OEM codepage.
5. NAV: command recipes and dispatcher syntax go to agent-cmd-howto. Routing-table internals, alias build status, and history go to agent-cmd-ref. Tier routing policy is claude-code-agent-tier-routing.
