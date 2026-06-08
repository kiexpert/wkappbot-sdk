---
id: on-load-ref
app: wkappbot-workflow
description: "T3 reference tier of on-load. Historical session-start fixes, dated incidents, accumulated bug notes. Read only when archaeology needed -- T1 on-load and T2 on-load-howto cover live use."
tags: [session, on-load, 3tier, t3, ref, history, incidents, archive]
---

> **Refresh**: `wkappbot skill read on-load-ref --if-newer` — v1.0 (2026-05-31)

# T3 Reference: on-load history

## Steps

1. WHY v100+ VERSION. on-load source skill hit v1.102 with 28 steps because every session-start fix accumulated as a new step. The 3-tier split preserves that history here in T3 while keeping T1 and T2 small enough to actually be read.
2. SCOPE FILTER incident. SKIP wkappbot core infra bugs (CDP, Chrome targets, CWD mismatch, grep perf, No Chrome targets) regardless of urgency -- those belong to the wkappbot team. Example: personal-docs domain = resume/mail/job-application. Acting outside scope wastes triage cycles.
3. TODO SYNC duplicate. Former on-load steps 13 and 14 were intentional emphasis -- TODO sync hit twice because losing it cost a full debugging session. Kept as single rule in howto step 9; this ref note preserves the doubled-rule history.
4. CronCreate nightly history. Former on-load step 16: Session start CronCreate nightly (13 4 * * *) before wk-only-gate. Add gg todo to Pending before Agent calls (claude-md-sync-guard). Promoted to howto step 11.
5. Gmail sync step history. Former step 17: after naver sync run python tools/gmail_mail_sync.py from personal-docs. Promoted to howto step 12.
6. Gmail sync completion note. Former step 18: Gmail sync completed successfully via wkcodex. Outputs in resume/drafts/gmail/. One-time observation, archived here.
7. Dated naver scan procedure. Former step 19: cp naver_mail/inbox.md to YYYYMMDD_naver_inbox_scan.md after sync.
8. JobKorea scan procedure. Former step 20: wkappbot cdp open -> navigate to mypage -> save findings.
9. Fetch Naver email bodies. Former step 21: use naver_mail_sync.py or imap directly to get body of specific emails by subject.
10. print_naver_jobkorea_bodies.py history. Former step 22: created by codex to fetch email bodies by subject filter.
11. SMS monitor v1. Former step 23: sms_monitor.py and sms_monitor_naver.ps1 to be created in tools/ for Naver IMAP polling and messages.google.com CDP watch.
12. SMS monitor v2 Option2+3. Former step 24: sms_monitor.py naver-poll and cdp-watch modes, sms_monitor_naver.ps1 launcher.
13. SMS monitor scripts created. Former step 25: sms_monitor.py and sms_monitor_naver.ps1 in tools/.
14. messaging_sync.py unified pipeline. Former step 26: unified SMS+KakaoTalk+LINE sync pipeline to resume/drafts/messages/.
15. messaging_sync.py KakaoTalk schema bug. Former step 27: SMS via ADB (12886 msgs), KakaoTalk/LINE via SQLite copy. KakaoTalk schema uses different column names - pick_col heuristic may miss them.
16. gg 2026-05-27 incident. Former step 28: Naver+Gmail scan + KakaoTalk web CDP sync + cross-repo achievement scan started.
