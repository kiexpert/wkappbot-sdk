#!/bin/bash
# test-screensaver-safety.sh — verify screensaver safety mechanisms
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== ScreenSaver Safety Test ==="
ROOT="${WKAPPBOT_ROOT:-W:/GitHub/WKAppBot}"
SS="$ROOT/csharp/src/WKAppBot.CLI/ScreenSaverOverlay.cs"
PIPE="$ROOT/csharp/src/WKAppBot.CLI/EyeCmdPipeServer.cs"
GM="$ROOT/csharp/src/WKAppBot.CLI/Commands/AppBotEyeGlobalMode.cs"

echo -n "Test 1: eye_busy suppression in Tick... "; check grep -q "eye_busy" "$SS"
echo -n "Test 2: Per-window SS-Guard threads... "; check grep -q "SS-Guard" "$SS"
echo -n "Test 3: SS-Master watchdog... "; check grep -q "SS-Master" "$SS"
echo -n "Test 4: WM_NULL Dispatcher ping... "; check grep -q "SendMessageTimeout" "$SS"
echo -n "Test 5: Topmost 5-min throttle... "; check grep -q "_lastTopmostUtc" "$SS"
echo -n "Test 6: Initial opacity=0... "; check grep -q "_currentOpacity = 0" "$SS"
echo -n "Test 7: ShowWindow SW_HIDE... "; check grep -q "ShowWindow" "$SS"
echo -n "Test 8: BeginCommand/EndCommand... "; check grep -q "BeginCommand" "$PIPE"
echo -n "Test 9: WKAPPBOT_WORKER screensaver... "; check grep -l 'EYE-SCREENSAVER' "$GM"

echo ""; echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
