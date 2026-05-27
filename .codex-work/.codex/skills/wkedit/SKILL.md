---
id: wkedit
app: wkappbot-workflow
description: "wkappbot file edit supports regex substitution, auto .bak/ backup, undo, and encoding-safe transactions. Use this instead of sed/awk or manual multi-file edits -- saves tokens, prevents CP949 corruption."
tags: [wkedit, file-edit, regex, bulk, refactor, encoding, backup, undo, transaction, multi-file, developer, project]
---

> **Refresh**: `wkappbot skill read wkedit --if-newer` — v1.28 (2026-04-07)

# wkedit -- Bulk Transaction Edit (Regex, Multi-File, Backup & Undo)

## Steps

1. [WHEN-TO-USE] Prefer wkedit for: (1) regex bulk substitution across a file, (2) multi-occurrence replace, (3) any edit on CP949/EUC-KR files. Avoid sed/awk -- they ignore encoding and corrupt Korean source. Avoid Python one-liners -- they waste tokens and miss edge cases.
[BASIC] wkappbot file edit '<old>' '<new>' <path> [--replace-all]
  Literal substitution. Fails if old not found (safe by default).
  --replace-all: replace every occurrence in the file.
  Backup auto-created at .bak/<filename>.bak-<timestamp>.txt
[REGEX] wkappbot file edit '<.NET-regex>' '<replacement>' <path> --replace-all --regex
  old_string is a .NET regex. new_string may use $1/$2 capture groups.
  NOTE: Escape parens/dots. TIP: test with 'wkappbot file grep <regex> --path <dir>' first.
[MULTI-FILE] Pass multiple paths -- wkedit processes all in one call.
  GOTCHA: if ANY file has zero matches, the whole batch fails with 'validation failed'.
  SOLUTION: pre-filter: files=$(grep -rl '<pattern>' <dir> --include='*.cs' 
2.  tr '\n' ' ')
  wkappbot file edit '<regex>' '<new>' $files --replace-all --regex
[BULK-PATTERN] Real-world bulk refactor (124 files, 1353 replacements, ~2s):
  files=$(grep -rl 'Console.WriteLine($"[' ./Commands/ --include='*.cs' 
3.  tr '\n' ' ')
  wkappbot file edit 'Console\.WriteLine\(\$"\[' 'Console.Error.WriteLine($"[' $files --replace-all --regex
[DRY-RUN] Add --dry-run to preview without writing. Shows diff but does NOT write the file.
  CAUTION: dry-run output still shows 'backup -> ...' lines -- those are NOT created.
[UNDO] ALWAYS use undo when you make a mistake -- backups accumulate in .bak/ automatically.
  wkappbot file edit --undo <path>          restore most recent backup
  wkappbot file write <path> --source-file .bak/<name>.bak-<timestamp>.txt  restore specific version
  List backups: ls .bak/<filename>.bak-* 
4.  sort
  LESSON: regex that captures only part of a match and replaces with partial text will silently
  corrupt the rest. Example: 'Console.Write("  [OS]' -> 'Console.Error.Write("  [' drops 'OS]'.
  Always include the full match in both old and new string, or use capture groups ($1) to preserve.
[ENCODING] wkedit auto-detects UTF-8 vs CP949. Never use sed/awk on WKAppBot sources.
  Post-edit sanity: 'wkappbot file grep '\uFFFD' --path <dir>' checks for corruption.
[TIPS-FROM-USE] Batch > loop: 124 files in one call ~2s. grep -rl filter mandatory for multi-file.
  .NET regex escaping: escape ( ) . in single-quotes for shell AND .NET.
  Safepoint commit before bulk edit = stress-free. Do NOT git add .bak/ accidentally.
5. IMPROVEMENT #9: --dry-run misleading output + silent no-match ambiguity. Current: dry-run prints 'backup -> ...' lines that look like writes happened. No-match exits with rc=1 but error message says 'old_string not found' -- hard to distinguish from other errors in scripts. Request: --dry-run suppress backup lines + --no-match-ok flag for scripted use
6. IMPROVEMENT #1: --line N / --line-range N:M scoped replace. Missing: replace only at line N or within line range. Workaround: add unique surrounding context. Request: wkedit.sh --line 650 'old' 'new' file
7. IMPROVEMENT #2: --nth N occurrence targeting. Missing: replace only 2nd/3rd/Nth match. Workaround: add unique surrounding context. Request: wkedit.sh --nth 2 'pattern' 'new' file
8. IMPROVEMENT #3: Backslash-heavy PS1 shell corruption. Shell garbles '\','/' before wkedit.exe sees it. --old-file/--new-file works but cumbersome. Request: --literal flag or stdin-pipe mode to bypass shell escaping
9. IMPROVEMENT #4: Capture group $1 shell expansion. --regex '\(wkcodex\)' '$1_new' -- shell expands $1. Request: --raw-replacement flag or heredoc-pipe mode for replacement strings
10. IMPROVEMENT #5: --patch + --dry-run combo. --patch heredoc mode ignores --dry-run flag. Request: _do_patch forward --dry-run to wkedit.exe
11. IMPROVEMENT #6: No .NET regex mode flags. No way to pass (?s) SingleLine or (?m) MultiLine. Cross-line patterns fail silently. Request: --regex-options or --multiline flag that injects (?s)
12. IMPROVEMENT #7: No append/prepend/insert-after-line. To append need to know exact last-line content. Request: wkedit.sh --append 'line' file OR --insert-after-line N 'text' file
13. IMPROVEMENT #8: --regex not supported in --old-file mode. --old-file is literal only. Can't use file-based old pattern with regex replacement. Request: --old-file --regex combo support
14. LIVE TEST RESULTS (2026-05-18): #1 --line N: FAIL - misparses 'old' arg as file path. #2 --nth N: FAIL - same misparse. #3 backslash: PARTIAL - shell eats \ but multiple-match error caught; --old-file workaround works. #4 capture $1: FAIL - shell expands $1 AND eats backslashes -> invalid regex. #5 --patch+dry-run: FAIL - --dry-run not forwarded in _do_patch(). #6 (?s) multiline: WORKS - inline (?s) flag + \r?\n pattern matches cross-line blocks correctly. #7 --append: FAIL - not supported. #8 --old-file+--regex: PARTIAL - command accepted; regex validation on file content. #9 dry-run output: BROKEN - no stdout/stderr returned to shell, exit always 0, output swallowed by wkappbot launcher IPC
15. BUG (2026-05-25): wkappbot file edit double-insertion on CRLF files. When old_string appears within new_string (starts with old_string), the replacement gets applied TWICE on CRLF files. Root cause: after first replacement, the result still contains old_string (as prefix of new), so wkedit applies again. Fix: use trailing context (not leading) as old_string so old_string does NOT appear in new_string. Applies to CLAUDE.md and any Windows CRLF file.
