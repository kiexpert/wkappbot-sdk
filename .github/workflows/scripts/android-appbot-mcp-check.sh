#!/usr/bin/env bash
# Runs inside the emulator job: installs the debug APK, starts the MCP service,
# and asserts tools/list (six tools) with the bearer token and HTTP 401 without it.
set -eu
PKG=kr.kivilab.appbot
APK="$(ls apk/*.apk | head -n 1)"
adb wait-for-device
adb install -r "$APK"
adb shell pm grant $PKG android.permission.POST_NOTIFICATIONS || true
# First launch generates the per-install bearer token.
adb shell am start -n $PKG/.MainActivity
sleep 5
TOKEN="$(adb shell run-as $PKG cat shared_prefs/appbot.xml | sed -n 's/.*name="token">\([0-9a-f]*\)<.*/\1/p' | tr -d '\r')"
[ -n "$TOKEN" ] || { echo "FAIL: no token found in app prefs"; exit 1; }
adb shell settings put secure enabled_accessibility_services $PKG/$PKG.AppBotA11yService
adb shell settings put secure accessibility_enabled 1
adb shell am start-foreground-service -n $PKG/.McpServer
sleep 5
adb forward tcp:8765 tcp:8765
URL=http://127.0.0.1:8765/mcp
BODY='{"jsonrpc":"2.0","id":1,"method":"tools/list"}'

CODE="$(curl -s -o /dev/null -w '%{http_code}' -X POST --data "$BODY" "$URL")"
echo "tools/list without token -> HTTP $CODE"
[ "$CODE" = "401" ] || { echo "FAIL: expected 401 without a bearer token"; exit 1; }

OUT="$(curl -s -X POST -H "Authorization: Bearer $TOKEN" -H 'Content-Type: application/json' --data "$BODY" "$URL")"
echo "tools/list with token -> $OUT"
COUNT="$(printf '%s' "$OUT" | python3 -c 'import json,sys; print(len(json.load(sys.stdin)["result"]["tools"]))')"
NAMES="$(printf '%s' "$OUT" | python3 -c 'import json,sys; print(",".join(sorted(t["name"] for t in json.load(sys.stdin)["result"]["tools"])))')"
echo "tools: $NAMES"
[ "$COUNT" = "6" ] || { echo "FAIL: expected 6 tools, got $COUNT"; exit 1; }
[ "$NAMES" = "a11y_click,a11y_find,a11y_scroll,a11y_tree,a11y_type,global_action" ] || { echo "FAIL: unexpected tool names"; exit 1; }
echo "PASS: 401 without token, six tools with token"
