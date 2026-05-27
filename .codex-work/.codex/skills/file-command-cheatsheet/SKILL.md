---
id: file-command-cheatsheet
app: wkappbot-workflow
description: "wkappbot file tools for filesystem access. edit auto-creates .bak/ backup. glob MUST use **/ prefix. After edit, U+FFFD corruption triggers auto Slack alert."
tags: [file, read, write, edit, grep, glob, encoding, reference, operator]
---

> **Refresh**: `wkappbot skill read file-command-cheatsheet --if-newer` — v1.7 (2026-04-07)

# file -- Read/Write/Edit/Grep/Glob Cheatsheet

## Steps

1. READ: wkappbot file read <path> [--offset N] [--limit N]
2. GREP: wkappbot file grep "pattern" --path dir --type cs [-i] [-C 3] [--max N]
3. GLOB: wkappbot file glob "**/*.cs" --path dir -- ALWAYS use **/ prefix
4. EDIT: wkappbot file edit <path> --old "..." --new "..." (auto .bak/ backup)
5. UNDO: wkappbot file undo <path> -- restore from .bak/
6. WRITE: wkappbot file write <path> --content "..." (.bak/ created on overwrite)
7. ! CP949 files: Korean source with Han comments -- verify encoding before edit
8. ! After edit: U+FFFD detected -> auto Slack alert fired
9. wkappbot file edit PATH --old-string 'text' --new-string 'text' for INLINE string replacement. --old/--new flags take FILE PATHS (not strings). Without --old-string, value is treated as a file path relative to CWD.
