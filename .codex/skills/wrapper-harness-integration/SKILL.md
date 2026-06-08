---
id: wrapper-harness-integration
app: personal-docs
description: "Each approved wrapper tool (wkedit, wkcodex, wkclaude) must call the CLAUDE.md self-harness rules directly at startup. Makes enforcement independent of Claude Code hooks."
---

> **Refresh**: `wkappbot skill read wrapper-harness-integration --if-newer` — v1.1 (2026-05-20)

# Wrapper Tools Harness Integration: wkedit wkcodex wkclaude

## Steps

1. WHY: Claude Code PreToolUse hook fires harness on every tool call. But wkedit/wkcodex/wkclaude called directly (CI, terminal, Mac) skip the hook. Wrappers must self-enforce.
2. PATTERN (PS1): at top after param block -- load Get-HarnessRules + Invoke-HarnessRules from wkharness-guards.ps1 via dot-source, then call Invoke-HarnessRules with toolName=script name, input=args joined, phase=pre.
3. PATTERN (bash): at top -- source wkharness-guards subset or call powershell -File wkharness-guards.ps1 inline check. On Windows use powershell; on Mac use pwsh or skip if unavailable.
4. TARGETS: tools/wrappers/wkedit.sh, tools/wrappers/wkcodex.sh, tools/wrappers/wkclaude.sh, tools/wrappers/wkclaude.ps1, tools/wkedit-v1.2.sh. Each adds harness pre-check at entry.
5. SAFE EXEMPTION: CLAUDE.md edits always safe. Wrappers pass their own script name as toolName so harness:safe patterns can exempt specific wrappers if needed.
6. FAILURE MODE: if CLAUDE.md missing or Get-HarnessRules fails, skip silently (try/catch). Never block the wrapper on harness load error.
7. BLOCK MESSAGE GUIDANCE: harness:block body can include '!wkappbot skill read <id>' line -- when block fires the agent reads the skill for context. Also add 'See: wkappbot skill read <id>' as static line for immediate guidance without execution.
