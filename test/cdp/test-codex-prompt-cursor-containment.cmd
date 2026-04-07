@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "SRC=w:\GitHub\WKAppBot\csharp\src\WKAppBot.Win32\Window\ClaudePromptHelper.HostFinders.cs"

findstr /C:"Reject VS Code Codex Message input (cursor outside)" "%SRC%" >nul || goto fail_msg
echo PASS: codex message-input rejection log exists

findstr /C:"Reject VS Code Codex focus rect (cursor outside)" "%SRC%" >nul || goto fail_focus
echo PASS: codex focus-rect rejection log exists

findstr /C:"ContainsCurrentCursor(messageInputRect)" "%SRC%" >nul || goto fail_gate
findstr /C:"ContainsCurrentCursor(promptRect)" "%SRC%" >nul || goto fail_gate
echo PASS: codex prompt acceptance requires cursor containment

set "FOUND_CURSOR="
for /f "usebackq delims=" %%L in (`"W:\SDK\bin\wkappbot-core.exe" prompt-probe --limit 2 --depth 1 2^>^&1`) do (
  echo %%L
  echo %%L | findstr /C:"cursor=" /C:"prompt.cursor" >nul && set "FOUND_CURSOR=1"
)

if not defined FOUND_CURSOR goto fail_probe
echo PASS: prompt-probe emits cursor containment output
exit /b 0

:fail_msg
echo FAIL: missing codex message-input rejection log
exit /b 1

:fail_focus
echo FAIL: missing codex focus-rect rejection log
exit /b 1

:fail_gate
echo FAIL: missing cursor containment gate for codex prompt
exit /b 1

:fail_probe
echo FAIL: prompt-probe missing cursor containment output
exit /b 1
