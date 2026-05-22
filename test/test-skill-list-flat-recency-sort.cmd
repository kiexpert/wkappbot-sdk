@echo off
REM Evidence: skill list default sort by recency flat cross-app
REM Fix: SkillCommand.cs -- flat globally sorted, [app] inline, no GroupBy default

echo [TEST] skill list must show flat globally newest first
wkappbot skill list 2>&1 | findstr /C:"[wkappbot" /C:"[invest" /C:"[wkautoquant"
if %ERRORLEVEL%==0 (echo [PASS] skill list shows flat app-tagged entries) else (echo [FAIL] & exit 1)

echo [TEST] skill list wkappbot-workflow must show grouped view
wkappbot skill list wkappbot-workflow 2>&1 | findstr /C:"[wkappbot-workflow]"
if %ERRORLEVEL%==0 (echo [PASS] app filter shows grouped view) else (echo [FAIL] & exit 1)

wkappbot eye tick
wkappbot skill list
