---
id: hippocampus-living-memory
app: personal-docs
description: "Neuro-named map of the hongik harness two-stage memory: hippocampus (real-time encoding via auto-topic-MD), recall (wkfind/session-find retrieval over session JSONL), sleep consolidation (skill-heal-nightly). Single findable entry point tying the scattered memory-vision pieces together."
tags: [memory, hippocampus, wkfind, recall, consolidation, knowledge-ification, architecture, neuro]
---

> **Refresh**: `wkappbot skill read hippocampus-living-memory --if-newer` — v1.28 (2026-06-05)

# Hippocampus KI living-memory architecture

## Steps

1. METAPHOR: the hongik harness memory mirrors the human brain. HIPPOCAMPUS equals real-time encoding (force-capture hard facts like dates, case numbers, deadlines, citations, decisions the turn they appear, via an auto-topic-MD exporter). RECALL equals ripgrep retrieval over all-AI raw session transcripts. SLEEP CONSOLIDATION equals skill-heal-nightly (daily 04:13 Opus cron: heal skills, compact CLAUDE.md Pending, dedup). Core law: capture the moment the turn it happens, or lose it.
2. WKHIPPO equals THE FINAL-FORM FRONT DOOR and the single fixed way to express every memory action. wkhippo with no args equals STATUS (announce of encoding, recall, sleep state); wkhippo recall (kw) equals recall (hoesang); wkhippo foresee (kw) equals predecessor-outcome foresight (mirae-gwancheuk); wkhippo export (sessionid) equals encoding (haema). There are NO standalone aliases: the former wkrecall and wkforesee commands were removed 2026-06-05, so every memory action is a wkhippo subcommand, for consistency and easy future discovery. Always call the wkhippo subcommand form. Read-only wk tool, kept pace and busy-fleet exempt for frictionless recall.
3. RECALL BECOMES FORESIGHT (the core principle): before choosing an approach, recall whether a PREDECESSOR session already tried it and how it turned out, then infer your future from that senior outcome instead of repeating the wrong road. The session corpus is a predecessor-mistake oracle: consult BEFORE acting, not after failing.
4. ENCODING FLOOR: at minimum one guaranteed end-of-session life-review (wkhippo export plus opus-self-reflection on session end or before compaction) so the next session is born already remembering its predecessor; rebirth equals post-compaction-recovery reading what the death-bed review saved. Per-turn encode is the ideal, one death-bed encode is the acceptable floor.
    5. NAVIGATION plus CLUSTER: usage, the wkhippo subcommands, recall spec, encoding trigger, and the wkfind four-source find organ are in wkappbot skill read hippocampus-living-memory-howto. Genesis, intent-via-subcommand, the security-audit and AGI angle, productization, and the dated build-status log are in wkappbot skill read hippocampus-living-memory-ref. CLUSTER: wkhippo (memory organ), wkfind (find organ), skill-heal-nightly (consolidation), harness-learn-self-evolving-skills (encoding design), knowledge-ification-procedure-jisikhwa (the why), post-compaction-recovery (rebirth). Archived alias references live only in `wkrecall`.
