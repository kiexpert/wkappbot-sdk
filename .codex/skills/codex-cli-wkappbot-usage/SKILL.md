---
id: codex-cli-wkappbot-usage
app: wkappbot-workflow
description: "Operate Codex CLI inside the WKAppBot ecosystem: repo-local bin target, login and session management, explicit execution modes, sandbox and config controls, ask/skill/handoff integration, and Windows troubleshooting."
tags: [wkappbot, codex, cli, workflow, windows, path, symlink, chatgpt, limits, ask, skill, handoff, eye, project, exec, review, resume, fork, sandbox, config, apply, login, no-alt-screen, rc, quota, failure-mode, battle-scar]
---

> **Refresh**: `wkappbot skill read codex-cli-wkappbot-usage --if-newer` — v1.15 (2026-04-20)

# Codex CLI usage inside WKAppBot

## Steps

- `wkappbot skill read codex-cli-wkappbot-usage --if-newer   # re-read only if the cache changed`

1. Use repo-local bin\codex.exe first when the goal is to make Codex callable from the WKAppBot toolchain; keep PATH injections repo-local and prefer a bin symlink or wrapper over ad-hoc global installs.
2. Smoke-check the install with codex --help and codex --version; if auth is missing or stale, run codex login and verify the CLI can store credentials under ~/.codex.
3. Treat Codex command results as rc-sensitive: rc=0 is success, rc=1 is a handled failure, and any other rc means the run needs investigation even if the terminal looked quiet.
4. Use codex [PROMPT] for interactive sessions, codex exec for non-interactive automation, and codex review for code review jobs.
5. Use codex resume --last to continue the most recent session and codex fork --last to branch from the same context; treat resumed runs as separate executions, not a shared chat transcript.
6. Pin reproducibility with -c key=value, --model MODEL, and -p PROFILE; prefer config files for defaults and CLI overrides for one-off debugging.
7. Make execution intent explicit with -s read-only, -s workspace-write, -s danger-full-access, -a on-request, or --full-auto; only use --dangerously-bypass-approvals-and-sandbox when the outer environment already enforces isolation.
8. Anchor the working tree with -C DIR; add extra writable roots only with --add-dir DIR, and keep the repo root as the primary workspace whenever possible.
9. Enable --search only when live web grounding is needed; otherwise keep the run offline to reduce variability.
10. Use codex apply after a diff-producing run if you want the latest agent diff applied to the local tree, and review the diff before applying when the task is risky.
11. If a command can fail silently, write the fix as an explicit failure-mode chain: If X -> Y, then prove Y with one follow-up command and one observable output.
12. Use a full WKAppBot chain instead of a bridge mention: wkappbot ask triad "question" -> inspect the answer -> wkappbot skill contribute or wkappbot suggest when the answer should become durable knowhow.
13. For quota or cost checks, use the live command first: wkappbot claude-usage for context pressure, codex --help / codex --version for install state, and the provider dashboard or plan page for billing limits when the tool output does not expose them.
14. If Codex works in VS Code but not in CMD or PowerShell, inspect PATH, ~/.codex, the VS Code extension install, the repo-local bin\codex.exe, and the terminal scrollback mode with --no-alt-screen before assuming the binary is broken.
15. Keep Codex and ChatGPT quotas separate, and bridge to WKAppBot with wkappbot ask for delegation, wkappbot skill for durable knowhow, and wkappbot eye tick for live state.
16. Write from battle scars, not from brochure copy: prefer concrete breakage notes, rc behavior, and recovery commands over generic product praise.
17. BATTLE SCARS from real sessions: (1) SILENT READ-ONLY TRAP: omitting -s workspace-write makes codex run read-only and report success while writing nothing -- always pin -s workspace-write for any file-editing task. (2) RC=0 IS NOT ENOUGH: even on rc=0, parse the last 5 lines of output for 'Everything up-to-date' / '0 errors' / 'I cannot write' -- codex sometimes reports success while the underlying operation failed silently. (3) EXIT CRITERION IS MANDATORY: include one self-verifiable line in every prompt, e.g. 'verify: git diff --stat should show N files changed' -- without it codex stops when it thinks it is done, not when it actually is. (4) CODEX FIRST, OPUS SECOND: for any mechanical file edit or refactor, reach for codex exec before spawning an Opus agent -- codex-mini is significantly cheaper and faster for execution work. Reserve Opus for judgment, architecture, and stuck bugs only. (5) BRIEFING QUALITY = OUTPUT QUALITY: write imperative DO steps with absolute paths and a single exit criterion. Vague goals cause early exit. Codex is not a mind-reader -- it executes exactly what you specify.
18. WKCODEX WRAPPER (2026-05-15): D:/GitHub/wkcodex.ps1 (symlink to tools/wkcodex.ps1) -- use instead of raw codex exec for guarded execution. Usage: wkcodex.ps1 'wkappbot skill read A. wkappbot skill read B. wkappbot skill read C. task' [-C dir]. Pre-checks: (1) 3+ skill refs required, (2) forbidden process kill patterns, (3) forbidden direct skill file edit patterns. Auto-injects --dangerously-bypass-approvals-and-sandbox. Post-checks: git diff for skill file modifications + CP949 violations + unexpected exit codes. This is the primary Codex invocation method on this machine.
19. STREAMING: codex exec --json prints all events to stdout as JSONL in real-time. Each line is a JSON event (type: message, tool_call, tool_result, etc.). Use this for real-time output instead of file-based polling. Combine with --output-last-message FILE to also save final response.
20. STREAMING: codex exec --json prints all events to stdout as JSONL in real-time. Each line is a JSON event (type: message, tool_call, tool_result, etc.). Use this for real-time output instead of file-based polling. Combine with --output-last-message FILE to also save final response.
21. SPEC-GATE RULE: non-mini codex exec requires SPEC block in task string (same as Opus agent). codex exec --model codex-mini skips spec-gate and skill-refs entirely. Pin model: --model codex-mini (cheap) or --model gpt-5 (strong).
22. CONSULT PATTERN (2026-05-22): codex exec works for pure knowledge questions not just code tasks. SPEC block + 3 skill refs gives codex the context it needs to answer well. Tested: CredentialUIBroker UIPI bypass query answered with WebAuthn Win32 API alternative.
23. today 2026-05-22: groq_agent.py SYS prompt updated to force read_file before analysis. cerebras_agent.py pending same update. Both agents tested in wkautoquant/scripts/.
24. When Codex produces a durable lesson, route it into the Hongik Harness by reading hongik-reflexes and knowledge-ification-procedure-jisikhwa, then skillify or guard it instead of leaving it as a one-off note.

- `wkappbot skill edit codex-cli-wkappbot-usage --add-step "<what you learned>"`

- `wkappbot skill edit codex-cli-wkappbot-usage --add-step "See also: <other-skill-id>"   # cross-link a reference skill`
- (edit via wkappbot skill commands ONLY -- never raw .skill.json)
