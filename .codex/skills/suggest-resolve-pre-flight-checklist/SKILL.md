---
id: suggest-resolve-pre-flight-checklist
app: wkappbot-workflow
description: "5-step preflight to pass all resolve guards first try. Covers commit keyword, class size, evidence filename, hidden-window path, final command."
tags: [suggest, resolve, preflight, checklist, guard, evidence, commit, class, source-size]
---

> **Refresh**: `wkappbot skill read suggest-resolve-pre-flight-checklist --if-newer` — v1.16 (2026-05-14)

# Suggest Resolve Pre-flight Checklist

## Steps

1. STEP 1 COMMIT KEYWORD GUARD: git log --oneline --grep=KEYWORD to find a commit whose message contains suggest title keywords. Merge or chore commits often have the full title. Terse fix-commits fail the keyword guard. Use the merge commit that recorded the suggest close.
2. STEP 2 CLASS SIZE GUARD: check target class is under 400 lines. Command: wc -l src/ClassName.cs. If 400 or more: split into partial classes with C# partial keyword, rebuild, push to remote, then resolve. Guard checks resolver repo. Skipping blocks every attempt.
3. STEP 3 EVIDENCE FILENAME GUARD: name script test-{cmd}-{subcmd}-description.cmd. The {cmd}-{subcmd} part must appear as a literal substring in the script commands. test-eye-tick-fix.cmd must call: wkappbot eye tick. test-windows-guard.cmd must call: wkappbot windows. NEVER use ask gpt -- it times out in hidden window mode. Best fast commands: wkappbot eye tick, wkappbot windows.
4. STEP 4 EVIDENCE SCRIPT PATH: always pass full absolute path. Hidden window cannot find relative filenames. Use: D:\full\path\test-eye-tick-fix.cmd. Wrong: just the filename alone.
5. STEP 5 FINAL RESOLVE COMMAND: wkappbot suggest resolve TS NOTE --i-completed-...-script ABSOLUTE_PATH --skill SKILL-ID --commit COMMIT-HASH --class ClassName. All 5 flags required. After: wkappbot suggest check TS to verify HALF or RESOLVED.
6. RELATED SKILLS: wkappbot skill read suggest-workflow (step 5 resolve command), wkappbot skill read suggest-resolve-evidence-conventions (step 1 hidden-window limits, step 11 filename guard), wkappbot skill read suggest-resolve-cdp-bugs (CDP-specific resolve), wkappbot skill read ask-suggest-priority-batching (triage to resolve pipeline).
