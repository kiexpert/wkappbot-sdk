---
id: claude-md-todo-sync
app: wkappbot-workflow
description: Keep CLAUDE.md Pending in sync with actual work. CLAUDE.md is canonical -- if not there it does not exist.
tags: [todo, pending, claudemd, sync, session-end]
---

> **Refresh**: `wkappbot skill read claude-md-todo-sync --if-newer` — v1.4 (2026-05-19)

# CLAUDE.md Pending Section Sync Workflow

## Steps

1. WHEN: session end, batch work complete, or user asks for todo sync.
2. STEP 1 git log: git log --oneline --since=last-sync to list all commits.
3. STEP 2 match to pending: for each [ ] item in CLAUDE.md, if commit covers it -> mark [x].
4. STEP 3 add new items: new work not in list -> [x] done or [ ] pending.
5. STEP 4 commit: git add CLAUDE.md && git commit -m chore todo sync pending section.
6. CANONICAL RULE: CLAUDE.md is only source of truth. Never track todos in conversation only.
7. ENCODING RULE: CLAUDE.md has Korean -- use wkcodex.sh for edits not Edit/Write tool directly.
8. NEW ITEM FORMAT: - [x] short description, no dates (git log has dates).
9. SCOPE: only this repo items. Skip wkappbot core infra (belongs to wkappbot team).
10. CRLF NOTE (2026-05-25): CLAUDE.md uses CRLF line endings. wkappbot file edit multiline replacements on CRLF files cause double-insertion and whitespace stripping bugs. Use wkcodex.sh for CLAUDE.md Pending updates, or make only single-line replacements where old_string does NOT appear in new_string.
11. CRLF APPEND WORKAROUND: wkappbot file edit multiline replacements cause 4x double-insertion on CRLF files. Safe alternative for adding pending items: (1) wkappbot file write CLAUDE.md --append --text '- [ ] item' to add at file end, or (2) use --old-file/--new-file flags to avoid shell escaping. Old string must NOT appear in new string -- if it does, CRLF causes repeated replacement.
