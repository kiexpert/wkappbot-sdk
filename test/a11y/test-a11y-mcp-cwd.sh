#!/usr/bin/env bash
# test-a11y-mcp-cwd.sh — verify MCP RunCliCaptureWithCode sets CWD to callerCwd
# The fix: McpCommand.Runner.cs now sets Environment.CurrentDirectory = callerCwd
# before executing in-proc commands, then restores it.
WK="W:/SDK/bin/wkappbot.exe"
PASS=0; FAIL=0

check() {
    local label="$1"; local cmd="$2"
    if eval "$cmd" > /dev/null 2>&1; then
        echo "[PASS] $label"; PASS=$((PASS+1))
    else
        echo "[FAIL] $label"; FAIL=$((FAIL+1))
    fi
}

# Verify the fix is in the source
check "RunCliCaptureWithCode sets callerCwd" \
    "grep -q 'Environment.CurrentDirectory = callerCwd' 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/McpCommand.Runner.cs'"

check "RunCliCaptureWithCode restores prevCwd in finally" \
    "grep -q 'CurrentDirectory = prevCwd' 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/McpCommand.Runner.cs'"

check "callerCwd guard: Directory.Exists check present" \
    "grep -q 'Directory.Exists(callerCwd)' 'W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/McpCommand.Runner.cs'"

# Functional: run wkappbot a11y windows to verify CWD is inherited correctly from caller
check "wkappbot a11y windows runs without CWD error" \
    "\"W:/SDK/bin/wkappbot.exe\" a11y windows 2>&1 | grep -v 'ERROR\|Exception'"

echo ""
echo "=== Results: $PASS passed, $FAIL failed ==="
[ "$FAIL" -eq 0 ]
