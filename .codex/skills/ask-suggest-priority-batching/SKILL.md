---
id: ask-suggest-priority-batching
app: wkappbot-workflow
description: "Reduce ask-gpt calls when triaging wkappbot suggest backlogs. Use when you have a page of suggestions and want one GPT ranking pass, then one ask per suggestion in priority order."
tags: [ask, gpt, suggest, priority, batching, triage, workflow]
---

> **Refresh**: `wkappbot skill read ask-suggest-priority-batching --if-newer` — v1.48 (2026-05-06)

# Ask Suggest Priority Batching

## Steps

1. SELF-HEAL BEFORE TRIAGE (MANDATORY): verify tools first -- (1) skill read this skill + suggest-workflow: fix stale steps with skill edit. (2) wkappbot ask speed-check: if dead or slow, reopen with --new-tab. (3) wkappbot suggest list: if malformed or errors, fix upstream. Broken tools = fake progress. Heal first, triage second.
2. Pull one page: wkappbot suggest list -- keep scope bounded. Do NOT re-triage full backlog at once.
3. Before asking GPT, run wkappbot suggest read <ts> for each candidate -- include ts, title, and current status in the GPT prompt for accurate ranking
4. Merge obvious duplicates first: wkappbot suggest merge --all-matching PATTERN --dry-run to preview, then without --dry-run. NOTE: (a) --all-matching blocked in WKAppBot maintainer channel -- run from sibling project CWD (e.g. WkAutoQuant) to bypass. (b) merge requires --root-cause, --work, --components, --affected-cmds -- always supply all four or the command errors.
5. Fire GPT batch rank once for the whole page (fire-and-forget, background): ask in English to rank by impact, risk, urgency, effort. Immediately continue -- NEVER block waiting for GPT response.
6. NO-RESPONSE HANDLING: (1) While ask gpt runs, start own codebase analysis on top items via grep + skill reads in parallel. (2) After 2 minutes without response: re-dispatch with --new-tab, continue triage. (3) If wkappbot ask log shows FindAllPrompts: 0, Chrome AI tabs are closed -- reopen with --new-tab, file BUG-AUTO suggest.
7. PER-ITEM ASK IS MANDATORY (NEVER skip): for EVERY suggest item you act on, MUST call wkappbot ask gpt --timeout 20s with ts + title + root-cause question BEFORE writing any code. No ask = no action. Triad for ambiguous items, gpt for clear ones. Fire all asks concurrently then WAIT for all responses -- do NOT proceed until ask results are in hand. If ask response reveals a problem: if small+safe, fix inline; otherwise delegate to Opus agent with exact file path, failing evidence, and expected verification step.
8. DELEGATION (MANDATORY): Sonnet orchestrates, Opus executes. Brief Opus agent with: suggest ts + title + root-cause from ask + fix path. Opus edits code, builds, writes evidence script, resolves. Sonnet never writes code inline. Pattern: Agent(model:'opus', prompt:'fix suggest <ts>: <title>. Ask result: <summary>. Fix: <steps>.')
9. ASK FAILURE RECOVERY: if ask times out or injection fails -- (1) file BUG-AUTO for the failure. (2) continue triage via codebase analysis only. (3) attempt fix: reopen with --new-tab, check Chrome. Never block on broken ask -- triage continues.
10. If ask reveals reusable pattern: wkappbot skill edit <id> to update the related skill before moving on.
11. After fix commit: wkappbot suggest check <ts> to verify regression PASS. Then close via ONE of two paths -- (A) 1st-party resolve: wkappbot suggest resolve <ts> NOTE [see suggest-workflow step 5 for the exact --i-completed-... flag; use that verbatim] --skill <id> --commit <hash> (creates HALF state with evidence script; Eye auto-confirms hourly when binary newer). (B) 2nd-party confirm (only if suggest is already HALF from another resolver): wkappbot suggest resolve <ts> NOTE --confirm --skill <id>. See suggest-workflow skill for the long --i-completed flag and DGWCS limitations.
12. Recurrence: if bug recurs AFTER fix commit, do NOT resolve old suggest -- file new suggest with post-fix repro date and uncovered code path.
13. Next page: reuse same batching prompt. Track page number to avoid re-reading already-ranked items. Repeat from step 2.
14. RIQUA [resolve pre-flight]: when ready to resolve a suggest, always read suggest-resolve-pre-flight-checklist first. It gates 5 guards: commit keyword match, class file under 400 lines, evidence filename convention, full absolute path, all required flags. Missing any one = repeated failure.
