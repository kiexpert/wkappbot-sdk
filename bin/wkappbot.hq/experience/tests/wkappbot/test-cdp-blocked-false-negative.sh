#!/bin/bash
# Evidence: [BUG] CDP prompt usability detection false-negative — fixed.
# Tests prompt usability reporting via find-prompts.
# DecoratePromptInfo previously used cursor position to probe occlusion —
# any time the user's mouse wasn't inside the prompt rect, prompt was marked blocked.
# Fix: probe at prompt-rect center instead of cursor position.
WKAPPBOT="D:/GitHub/WKAppBot/bin/wkappbot-core.exe"

out=$($WKAPPBOT find-prompts 2>&1)
echo "$out" | grep -E "FindAllPrompts|\[usable\]|\[blocked\]|\[hidden\]" | tail -10

usable=$(echo "$out" | grep -c "\[usable\]")
echo "---"
echo "USABLE=$usable"

# Pass: at least one prompt marked usable (regardless of cursor position)
[ "$usable" -ge 1 ] && exit 0 || exit 1
