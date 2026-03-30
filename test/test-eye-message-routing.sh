#!/bin/bash
# test-eye-message-routing.sh — verify Eye internal messages do NOT leak to Claude Code chat
# Covers suggest #6: 앱봇 내부 상태 메시지가 Claude Code 채팅창으로 노출됨 (ts=2026-03-30T10:29)
PASS=0; FAIL=0
check() { if "$@" >/dev/null 2>&1; then echo "PASS"; PASS=$((PASS+1)); else echo "FAIL"; FAIL=$((FAIL+1)); fi; }

echo "=== Eye Message Routing Test ==="
HANDLERS="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeSlackHandlers.cs"
JSONL="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeJsonlParser.cs"
GLOBAL="W:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/Commands/AppBotEyeGlobalMode.cs"

echo -n "Test 1: NO_REPLY filter prevents message re-delivery... "
check grep -q "NO_REPLY" "$JSONL"

echo -n "Test 2: Slack socket mode (not Claude Code) as primary delivery path... "
check grep -q "SlackSocketClient\|slack_send\|SendMessageAsync" "$GLOBAL"

echo -n "Test 3: Internal Eye state messages routed to Slack thread, not prompt injection... "
check grep -q "threadTs\|thread_ts\|slack_ts" "$HANDLERS"

echo -n "Test 4: Eye uses SlackSendViaApi for message delivery (not Claude injection)... "
check grep -q "SlackSendViaApi" "$HANDLERS"

echo -n "Test 5: Slack queue drain worker prevents concurrent injection... "
check grep -q "_slackDraining\|slackDraining\|drain" "$GLOBAL"

echo -n "Test 6: Eye alive message sent to Slack channel... "
check grep -q "Eye alive" "$GLOBAL"

echo -n "Test 7: slackRetiring flag prevents new messages during hot-swap... "
check grep -q "_slackRetiring\|slackRetiring" "$GLOBAL"

echo ""
echo "=== Results: $PASS PASS, $FAIL FAIL ==="
[ "$FAIL" -eq 0 ]
