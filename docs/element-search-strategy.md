# Element Search Strategy — Triad Consensus (2026-04-02)

> Source: Triad debate G:1775127163.323079
> Participants: GPT(SKEPTIC), Gemini(EXPLORER), Claude(AUDITOR)

## Background

**Original (정)**: Top-Down — start from wide parent region, narrow down to target.
**Proposed (반)**: Bottom-Up — start from cursor/target node, expand outward in parallel.

Key insight: Bottom-Up is actually the **orthodox approach** — and what WKAppBot was already
moving toward with focus-first analysis. Triad confirmed and refined it.

---

## Why Pure Top-Down Fails

- O(depth × branching) — slow serial traversal
- MFC owner-drawn: UIA subtree **does not exist** until hover/click ("lazy materialization")
- Layout wrapper changes break the entire path
- Timeouts common in HTS/MFC environments

## Why Pure Bottom-Up Also Fails (alone)

- Ambiguity: "Submit" button exists in 5 dialogs — leaf match alone is insufficient
- MFC synthetic nodes (DYN-A11Y) may not have reliable parent linkage yet
- UIA nodes may not be exposed at all until parent is "poked"

---

## Triad Consensus: Parallel Racing (합)

**Core idea**: Launch multiple search strategies simultaneously from the cursor position.
First one to find a unique, confirmed match wins — others are cancelled.

### Race Participants (all start in parallel)

1. **Bottom-Up Fast Path** — Query by Name/AutomationID/ControlType directly
   - ExperienceDB cache check first (0ms target on hit)
   - Process-level or anchored subtree scope
   - → Done immediately if unique match found

2. **Anchor-Constrained Top-Down** — Find stable nearby anchor first
   - Stable anchors: window title, static label, toolbar, non-owner-drawn control
   - Run localized Top-Down **within** the anchor subtree only
   - → Restores context without full traversal cost

3. **OCR/CCA Visual Stream** — Start segmentation from last known coordinates
   - CCA produces bounding-rect candidates (inherently bottom-up)
   - Feed into UIA ElementFromPoint
   - → Critical fallback when UIA tree is empty

### MFC Special: "Poke" Method
If all three fail because UIA subtree is missing:
- Simulate hover/WM_GETOBJECT on parent container
- Forces MFC to materialize the UIA sub-tree
- Then retry Bottom-Up fast path

---

## Experience DB Integration

Cache key: `(Name, ControlType, BoundingRect-region-hash)`

- **Cache HIT**: skip tree entirely, return immediately
- **Cache MISS**: Bottom-Up result updates DB with new path
- Bottom-Up naturally aligns with cache key structure → hits cache on first lookup
- Top-Down must traverse full path before cache is even consulted

---

## Summary Table

| Strategy     | Speed       | MFC Robustness | False Positive Risk |
|--------------|-------------|----------------|---------------------|
| Top-Down     | Slow O(d×b) | Poor (lazy UIA)| Low                 |
| Bottom-Up    | Fast O(1)   | Medium         | Medium              |
| **Parallel Racing** | **Fastest (cache)** | **High** | **Low (anchor verification)** |

---

## Implementation Notes for WKAppBot

- Current hack command: Top-Down from parent region → should add Bottom-Up + CCA parallel lanes
- Mouse CCA worker already produces bounding-rect candidates → feed directly into race
- ExperienceDB already keyed on leaf-node identity → Bottom-Up is natural cache consumer
- "Poke" (WM_GETOBJECT / hover) already exists in DYN-A11Y pipeline → promote to explicit step
- Disambiguation: when Bottom-Up finds multiple candidates → parent-walk to confirm scope (lazy, on-demand)
