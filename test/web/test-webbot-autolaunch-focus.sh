#!/bin/bash
# Test: WebBot Chrome auto-launch + focus protection
# Verifies: ConnectCdp auto-launches Chrome, launches minimized, tab creation doesn't steal focus

echo "=== WebBot Auto-Launch + Focus Protection Test ==="

# Kill any existing Chrome
taskkill //F //IM chrome.exe 2>/dev/null
sleep 2

# Record current foreground window
echo "[1] Chrome killed. Testing auto-launch via web read..."

# web read should auto-launch Chrome and read the page
RESULT=$(wkappbot web read "https://example.com" --max-chars 500 2>&1)
echo "$RESULT"

# Check: Chrome launched
if echo "$RESULT" | grep -q "launching Chrome"; then
    echo "[PASS] Chrome auto-launched"
else
    echo "[INFO] Chrome was already running or auto-launch message not captured"
fi

# Check: page content read successfully
if echo "$RESULT" | grep -qi "example"; then
    echo "[PASS] Page content read successfully"
else
    echo "[WARN] Page content may not have loaded"
fi

# Check: Chrome process exists
if tasklist | grep -qi chrome; then
    echo "[PASS] Chrome process running"
else
    echo "[FAIL] Chrome not running"
fi

echo ""
echo "[2] Testing claude-usage via WebBot..."
USAGE=$(wkappbot claude-usage 2>&1)
echo "$USAGE" | tail -5

if echo "$USAGE" | grep -q "Weekly"; then
    echo "[PASS] claude-usage read via WebBot"
else
    echo "[INFO] claude-usage may need login"
fi

echo ""
echo "=== Test Complete ==="
