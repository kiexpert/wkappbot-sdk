---
id: claude-md-harness-rules
app: wkappbot-workflow
description: "How to configure per-repo harness rules in CLAUDE.md: harness:safe/block/warn/done syntax, tInput format, body lines, template injection"
tags: [harness, claude-md, rules, safe, block, warn, done, per-repo, config]
---

# claude-md-harness-rules

## Steps

1. WHAT: Per-repo CLAUDE.md declarative rules that the harness (wkharness-guards.ps1) enforces on every tool call. Parsed at session start from the project CLAUDE.md.
2. SYNTAX: One rule = one ## header line + optional body lines. Header: '## harness:<action> <regex>'. Body: plain text shown in message; lines starting with ! run as PowerShell and output is appended.
3. ACTIONS: safe = whitelist (skip all block/warn if tInput matches). block = Block() the tool call with message. warn = write to stderr (no block). done = PostToolUse only (completion detector).
4. tInput FORMAT: '● ToolName(cmd)' -- e.g. '● Bash(rm -rf /tmp/foo)' or '● Edit(CLAUDE.md)'. Regex matches against this string. Anchor with '● Bash\(' to avoid false positives on tool args.
5. EXAMPLES: ## harness:safe CLAUDE\.md  (allow Edit on CLAUDE.md always) 
6.  ## harness:block (?i)● Bash\(.*rm -rf  (block dangerous rm) 
7.  ## harness:warn (?i)● Bash\(.*sed -i  (warn on sed) 
8.  ## harness:done (?i)completed
9. done  (PostToolUse completion check)
10. BODY LINES: Plain text = shown as help message. Line starting with ! = PowerShell command, output inserted. E.g.: !wkappbot skill read wk-ai-tools  (shows skill content in block message)
11. TEMPLATE: If CLAUDE.md has no harness: sections, ~/.claude/harness-template.md is auto-appended. Template has 4 default rules (safe CLAUDE.md, warn wkedit--line, block bash-pwsh, done skipped).
12. SCOPE: Only the CWD project CLAUDE.md is loaded (not global ~/.claude/CLAUDE.md). Each repo controls its own rules. Global rules live in wkharness-guards.ps1 hardcoded guards.
13. BEST PRACTICES: Use safe before block to whitelist exceptions. Anchor block patterns to '● Bash\(' or '● Edit\(' to avoid matching tool arguments. Keep regex simple -- no lookahead. Test with wkharness -Test.
