# 41-cross-family-harness-wiring.ps1

Promoted 2026-06-17 from `38-family-harness-wiring-proto.ps1` (proto + its 3 bloat docs
archived to `.bak/`). Asserts the 5 harness-wiring invariants (from
`34-gemini-settings-bom.ps1`) across all 4 families: claude, gemini, codex, agy.

Fixed the proto false-positive: enumeration now descends into the inner `.hooks[]`
array (settings shape is `{ <Key>: [ { id, hooks: [ {command,timeout} ] } ] }`), and
per-family main/post patterns accept the rename `wkharness-<family>-main/post.ps1`
plus the codex `Codex(Pre|Post)ToolUseAdapter.ps1` adapters. Result: claude PASS,
gemini PASS, codex PASS; agy correctly FLAGGED for the retired stray
`D:\GitHub\wkharness.ps1` (real INVARIANT 2/5 violation, not a false positive).

`34-gemini-settings-bom.ps1` is KEPT (NOT archived): it uniquely owns BOM detection/strip,
YOLO approvalMode validation, and the HEAL path (`wkharness-gemini-install.ps1`). 41 only
DETECTS wiring; archiving 34 would drop BOM-strip + heal = regression. 41 supersedes only
34's wiring-detection overlap (harmless).
