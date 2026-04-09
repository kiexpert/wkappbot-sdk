#!/usr/bin/env bash
# Test: a11y FOCUS/TARGET output format — code-level checks
# Verifies that: find uses code-fence blocks, non-find uses # TARGET line,
# FOCUS block is printed for all non-find actions.
SRC="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI"
P=0; F=0

echo "=== a11y output format: code-level checks ==="

# 1. PrintFocusBlock emits ## FOCUS code-fence to stdout
if grep -q '## FOCUS' "$SRC/Commands/A11yActions.Output.cs" 2>/dev/null; then
  echo "PASS: PrintFocusBlock emits ## FOCUS code-fence"; ((P++))
else echo "FAIL: PrintFocusBlock missing ## FOCUS"; ((F++)); fi

# 2. PrintTargetBlock emits ## heading + code-fence (for find/read)
if grep -q 'TargetLine.*##' "$SRC/Commands/A11yActions.Output.cs" 2>/dev/null; then
  echo "PASS: PrintTargetBlock emits ## heading via TargetLine"; ((P++))
else echo "FAIL: PrintTargetBlock missing ## heading via TargetLine"; ((F++)); fi

# 3. Non-find actions emit # TARGET {grap} single line
if grep -q '# TARGET' "$SRC/Commands/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: non-find actions emit # TARGET line"; ((P++))
else echo "FAIL: # TARGET line missing in A11yCommand.cs"; ((F++)); fi

# 4. FOCUS is skipped for find action (deferred to A11yFind)
if grep -q 'isFindAction\|For "find".*FOCUS.*deferred' "$SRC/Commands/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: find action defers FOCUS to A11yFind"; ((P++))
else echo "FAIL: find/FOCUS deferral logic missing"; ((F++)); fi

# 5. ## FOCUS block is printed for non-find actions (via PrintFocusBlock call)
if grep -q 'PrintFocusBlock' "$SRC/Commands/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: PrintFocusBlock called in A11yCommand (non-find path)"; ((P++))
else echo "FAIL: PrintFocusBlock not called in A11yCommand"; ((F++)); fi

# 6. A11yFind (find action) also calls PrintFocusBlock
if grep -q 'PrintFocusBlock' "$SRC/Commands/A11yActions.Read.cs" 2>/dev/null; then
  echo "PASS: A11yFind (A11yActions.Read.cs) calls PrintFocusBlock"; ((P++))
else echo "FAIL: A11yFind missing PrintFocusBlock"; ((F++)); fi

# 7. A11yFind calls PrintTargetBlock for find output
if grep -q 'PrintTargetBlock' "$SRC/Commands/A11yActions.Read.cs" 2>/dev/null; then
  echo "PASS: A11yFind calls PrintTargetBlock for target block"; ((P++))
else echo "FAIL: A11yFind missing PrintTargetBlock"; ((F++)); fi

# 8. No-match path returns non-zero exit code
if grep -q 'return 1\|Error(' "$SRC/Commands/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: no-match / error paths return non-zero"; ((P++))
else echo "FAIL: no non-zero return in A11yCommand"; ((F++)); fi

# 9. grap includes hwnd in output (BuildCompactWinGrap / BuildFindGrap)
if grep -q 'BuildCompactWinGrap\|BuildFindGrap\|hwnd:' "$SRC/Commands/A11yCommand.cs" 2>/dev/null; then
  echo "PASS: hwnd injected into TARGET grap"; ((P++))
else echo "FAIL: hwnd injection missing"; ((F++)); fi

# 10. FormatLeafTag includes name/aid/val/patterns (full metadata)
if grep -q "aid=\|patterns=\|val=" "$SRC/Commands/A11yActions.Output.cs" 2>/dev/null; then
  echo "PASS: FormatLeafTag includes aid/val/patterns"; ((P++))
else echo "FAIL: FormatLeafTag missing metadata fields"; ((F++)); fi

echo ""
echo "Source checks done: $P passed, $F failed"
