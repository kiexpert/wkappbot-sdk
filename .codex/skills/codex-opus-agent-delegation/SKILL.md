---
id: codex-opus-agent-delegation
app: wkappbot-workflow
description: "Agent.cmd lets Codex spawn Opus/Sonnet/Haiku subagents through the same brief-guard and spec-gate as Claude Code Agent() calls. Codex becomes orchestrator, Opus handles judgment."
tags: [codex, opus, agent, delegation, brief-guard, spec-gate, orchestration, wkclaude]
---

> **Refresh**: `wkappbot skill read codex-opus-agent-delegation --if-newer` — v1.0 (2026-05-23)

# Codex to Opus Agent Delegation

## Steps

1. CHAIN: Codex (cheap execution) -> Agent.cmd stdin JSON -> wkharness brief-guard + spec-gate -> wkclaude.cmd --model opus prompt -> Opus result back to Codex -> Codex implements.
2. INTERFACE: {tool_name:'Agent', tool_input:{prompt:'SPEC block + 3 skill refs + task', model:'opus'}}. model aliases: opus/sonnet/haiku -> full claude model ID.
3. GUARDS FIRED: brief-guard requires 3 wkappbot skill read refs in prompt. spec-gate requires SPEC 1-5 numbered block for Opus. Both enforced identically to Claude Code Agent() calls.
4. CODEX USAGE PATTERN: shell 'printf json 
5.  Agent.cmd'. JSON prompt must contain SPEC block (for Opus) then skill refs then task. Same rules as Claude Code Agent() -- no shortcuts.
6. MODEL ROUTING: opus -> claude-opus-4-7. sonnet -> claude-sonnet-4-6. haiku -> claude-haiku-4-5-20251001 (default). Full model IDs passed through unchanged.
7. EXECUTION: Agent.ps1 extracts prompt+model, calls wkclaude.cmd -Model modelId prompt. wkclaude runs claude --print non-interactive. Output streams to Codex stdout.
8. DIVISION OF LABOR: Codex=cheap executor (file edits, shell, search). Agent.cmd+Opus=judgment (architecture, root-cause, design). Codex calls Opus only when genuinely stuck or need planning -- same escalation rule as Claude Code.
9. SOURCE: personal-docs/tools/Agent.ps1 + Agent.cmd. Symlink: WKAppBot/bin/Agent.cmd.
