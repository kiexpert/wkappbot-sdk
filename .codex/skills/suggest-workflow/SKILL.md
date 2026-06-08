---
id: suggest-workflow
app: wkappbot-workflow
description: "Suggest = AI-logged bug/improvement record. Triage with merge, resolve with evidence script. SUBMIT RULE: use wkappbot-core.exe when eye blocks. NEVER edit jsonl manually."
tags: [wkappbot, suggest, workflow, operator, riqua, evidence-script, commit-guard, skill-guard]
---

> **Refresh**: `wkappbot skill read suggest-workflow --if-newer` — v1.40 (2026-04-07)

# Suggest Management Workflow

## Steps

1. Check backlog: wkappbot suggest list
2. Merge duplicates: wkappbot suggest merge --all-matching PATTERN --title X --root-cause Y --components Z --affected-cmds CMD --work Nh -- WARNING: --all-matching may match unrelated items; preview with --dry-run and narrow the pattern if needed before running for real
3. Prioritize: run wkappbot suggest read <ts> for top candidates, then SONNET (main session) MUST run wkappbot ask gpt to rank candidates by impact/urgency/effort -- this step is MANDATORY and must NOT be skipped or delegated. GPT returns ranked list -- act on rank 1 first. Heuristic: est<=2h first, high freq, risk=high.
4. Before resolve: always run wkappbot suggest check <ts>; only resolve after PASS, or fix/re-check until PASS
5. Resolve (1st party): wkappbot suggest resolve <ts> NOTE --i-completed-the-code-and-built-successfully-and-deployed-and-tested-with-real-scenarios-and-confirmed-meaningful-results-and-have-evidence-and-willkim-allowed-this-script FULL_PATH --skill ID --commit HASH --class ClassName. BEFORE running: wkappbot skill read suggest-resolve-pre-flight-checklist -- run all 5 guards first or you will fail repeatedly.
6. Resolve (2nd party confirm): wkappbot suggest resolve <ts> NOTE --confirm --skill ID -- no evidence script needed for confirm
7. NO FAKE TESTS: evidence script must actually run affected wkappbot commands (CMD guard checks output)
8. Evidence script must exit 0 -- re-runs as regression on every future resolve
9. RIQUA [evidence filename]: script name currently must follow test-{cmd}-{subcmd}-{description}.<sh/ps1/cmd>; generic names like test_kis_keepalive.cmd are rejected. INTENDED: allow any descriptive filename; rigid format adds friction without safety benefit.
10. RIQUA [skill guard cwd]: --skill ID guard fails when wkappbot-core runs from scripts/ subdir -- HQ path lookup uses cwd/.wkappbot/hq/skills/ which does not exist. INTENDED: skill guard should walk up to repo root to find .wkappbot/hq/. Workaround: run from repo root.
11. RIQUA [commit guard diverged]: --commit <hash> guard fails when local branch has commits that remote does not yet include. INTENDED: commit guard should accept any hash reachable in local git log. Workaround: merge/push remote commits first.
12. Insights: after resolution, record the reusable root cause, failed assumption, prevention rule, and any command/test that proved the fix via wkappbot skill contribute or skill edit
13. SUBMIT BLOCKED? If wkappbot suggest TEXT is silently dropped (eye intercept due to PENDING CO-RESOLVE), bypass with: wkappbot-core.exe suggest TEXT --requirement 'CMD => EXPECTED' (3 required)
14. NEVER edit suggestions.jsonl manually -- tamper detection breaks submit permanently
15. PENDING CO-RESOLVE banner = warning only, never spend time resolving it first -- just use core to submit past it
16. CROSS-RESOLVE (DGWCS): wkappbot suggest resolve --confirm will say not-found for DGWCS entries -- this is a known bug (#19). Do not attempt workarounds.
17. RIQUA [suggest cwd]: ALWAYS run wkappbot-core.exe suggest from the PROJECT REPO ROOT (e.g. D:/GitHub/WkAutoQuant). NEVER cd into the WKAppBot dir to submit -- causes git add to fail (outside repository). Stay in project root.
18. RIQUA [--confirm silent no-op]: suggest resolve --confirm returns rc=0 but state stays HALF (suggest check still shows Confirm action, history unchanged). Workaround: none known. Bug #19 variant. The Eye auto-confirm (runs hourly) is the only reliable path to RESOLVED for DGWCS items.
19. RIQUA [commit keyword guard]: --commit <hash> message must contain enough keywords from suggest title. Fix: use a merge/chore commit whose message includes the full suggest title (e.g. chore(suggest): merge [BUG-AUTO] Chrome window...). Find with: git log --oneline --grep=KEYWORD. A fix-commit with a terse message will fail (e.g. 'fix(launcher): add on-screen guard' missing 'position mismatch session restore').
20. RIQUA [class guard + source size]: --class <ClassName> is REQUIRED (not optional). The named class file must be <400 lines or resolve fails with 'Source size guard FAILED'. If over 400: (1) split into partial classes first, (2) rebuild + push, (3) THEN resolve. Check size: wc -l <file>.cs before resolving.
21. POST-SUBMIT OPS: wkappbot suggest add-requirement TS 'cmd => expected' appends requirement to already-submitted suggest. wkappbot suggest check TS runs evidence and requirements -- PASS offers confirm, FAIL blocks. Use both for iterative enrichment after initial submit.
22. STALE BUG-AUTO RESOLUTION: BUG-AUTO [ASK] TimeoutException/TaskCanceledException/OperationCanceledException burst entries are safe to batch-resolve as stale when from a past high-load or cold-cache session. Use: wkappbot suggest resolve <ts> 'Stale BUG-AUTO from <session-context>. Not actionable.' --i-completed-the-work-and-verified-with-script none --skill suggest-workflow
23. Verified 2026-05-26: test-stale-cdp-ask-bugauto-noise.sh (resolve ts=2026-05-26T04:31:06)
