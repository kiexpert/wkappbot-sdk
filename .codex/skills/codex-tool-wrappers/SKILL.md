---
id: codex-tool-wrappers
app: wkappbot-workflow
description: "Claude-named CMD wrappers for Codex: block native write_file/read_file, route through wkharness with Claude tool_name format so all existing guards fire identically."
tags: [codex, harness, wrappers, edit, write, tool-shim, wkwrap, cmd]
---

# Codex Tool Wrappers

## Steps

1. ARCHITECTURE: Codex native write_file/read_file blocked via hook exit 2. Codex must use shell to call cmd wrappers that match Claude built-in tool names.
2. WRAPPER SET: Edit.cmd Write.cmd Read.cmd Glob.cmd Grep.cmd.
3. SOURCE: personal-docs/tools/ owns .cmd and .ps1 source files. WKAppBot/bin/ holds symlinks installed by wkwrap --install.
4. INSTALL: wkwrap --install extended to mklink WKAppBot/bin/Edit.cmd -> personal-docs/tools/Edit.cmd. Symlinks not copies, no wk prefix.
5. INTERFACE: Each wrapper reads stdin JSON matching Claude tool_input format. Edit: {tool_name:'Edit',tool_input:{file_path,old_string,new_string}}. Write: {tool_name:'Write',tool_input:{file_path,content}}.
6. HARNESS INTEGRATION: wrapper pipes stdin JSON to wkharness.ps1 unchanged. wkharness sees tool_name:'Edit' same as real Claude Code Edit call. Zero harness changes needed.
7. EDIT.CMD IMPL: thin relay to Edit.ps1. Steps: (1) read stdin JSON (2) pipe to wkharness.ps1 exit 1 if blocked (3) parse file_path old_string new_string (4) .bak backup (5) UTF-8 read replace UTF-8 write.
8. WRITE.CMD IMPL: relay to Write.ps1. Steps: wkharness pre-check, .bak backup, UTF-8 full overwrite.
9. CODEX CONFIG: ~/.codex/config.toml PreToolUse hook matcher=write_file exits 2 with 'use Write.cmd via shell'. matcher=read_file exits 2 with 'use Read.cmd via shell'.
10. AGENTS.MD RULES: FORBIDDEN write_file read_file. REQUIRED shell echo json pipe to Write.cmd or Edit.cmd. Never use native Codex file tools.
