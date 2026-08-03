---
id: claude-session-handoff
app: wkappbot
description: "Finds the previous Claude Code session JSONL file for the current project under ~/.claude/projects/<slug>/, reads the last ~120 lines, and extracts human/assistant text turns to reconstruct handoff context. Run at the start of a new session when the user asks to continue from the last session."
tags: [session, handoff, context, continuity, jsonl, claude]
---

> **Refresh**: `wkappbot skill read claude-session-handoff --if-newer` — v1.2 (2026-04-24)

# Claude session handoff: find previous session JSONL and extract context

## Steps

- `wkappbot skill read claude-session-handoff --if-newer   # re-read only if the cache changed`

1. 1. Derive project slug: convert CWD path (drive letter + slashes) to hyphens, e.g. D:/GitHub/WkAutoQuant -> d--GitHub-WkAutoQuant
2. 2. List JSONL files sorted by mtime descending: ls -lt ~/.claude/projects/<slug>/*.jsonl 
3.  head -5. Current session is index [0], previous session is index [1]
4. 3. Read last 120 lines of the previous session JSONL via PowerShell: Get-Content <path> -Encoding UTF8 -Tail 120
5. 4. For each line parse JSON; extract role + message.content text fields. Skip blank lines, API errors, system hooks. Print [role] <text[:600]> per substantive turn
6. 5. Synthesize handoff summary: last user request, last assistant state, unfinished action items, pending confirmations, open file edits
7. 6. Report to user: what was happening, what was left unfinished, proposed next step
8. For predecessor questions or root-cause lookups, prefer wkhippo recall/foresee before manual JSONL reconstruction; wkrecall is archived.

- `wkappbot skill edit claude-session-handoff --add-step "<what you learned>"`

- `wkappbot skill edit claude-session-handoff --add-step "See also: <other-skill-id>"   # cross-link a reference skill`
- (edit via wkappbot skill commands ONLY -- never raw .skill.json)
