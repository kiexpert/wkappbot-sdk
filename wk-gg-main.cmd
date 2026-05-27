@echo off
REM wk-gg-main.cmd - Haiku main workflow: CDP health + suggest triage + repo status
REM Collects all info needed for product quality decisions + suggest triage
REM Usage: wk-gg-main [--watch]

setlocal enabledelayedexpansion

echo.
echo ╔════════════════════════════════════════════════════════════╗
echo ║ wk-gg-main: Comprehensive Status Report ^& Suggest Triage ║
echo ║ Time: %date% %time%                                     ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

REM ============ 1. SYSTEM HEALTH ============
echo [1/8] SYSTEM HEALTH
powershell -File "D:\GitHub\wkappbot-sdk\bin\cdp-health-check.ps1" -Alert 2>nul
if !errorlevel! equ 0 (
    echo. ^& echo ✅ System healthy
) else (
    echo. ^& echo ⚠️ System health check warning
)

REM ============ 2. GIT STATUS ============
echo.
echo [2/8] GIT STATUS (last 5 commits ^& pending changes)
cd /d D:\GitHub\wkappbot-sdk
git log --oneline -5 2>nul
git status --short 2>nul | head -10

REM ============ 3. CLAUDE.MD PENDING ============
echo.
echo [3/8] CLAUDE.MD PENDING ITEMS
findstr /R "^- \[.\]" D:\GitHub\wkappbot-sdk\CLAUDE.md 2>nul | wc -l
echo pending items listed above

REM ============ 4. SKILL NEWS ============
echo.
echo [4/8] SKILL NEWS (last 7 days - top 10)
wkappbot skill news 2>nul | head -15

REM ============ 5. SUGGEST BACKLOG ============
echo.
echo [5/8] SUGGEST BACKLOG (priority breakdown)
wkappbot suggest list 2>nul | head -20

REM ============ 6. VERSION AUDIT ============
echo.
echo [6/8] VERSION AUDIT (sync check)
echo Checking README/SECURITY/AGENTS/CLAUDE version consistency...
findstr "v7\." D:\GitHub\wkappbot-sdk\README.md 2>nul | head -1
findstr "v7\." D:\GitHub\wkappbot-sdk\CLAUDE.md 2>nul | head -1
findstr "v7\." D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\Directory.Build.props 2>nul | head -1

REM ============ 7. REPO HEALTH ============
echo.
echo [7/8] REPO HEALTH (on-load skill check)
wkappbot skill read repo-health-doctor 2>nul | head -5

REM ============ 8. CROSS-REPO AUDIT ============
echo.
echo [8/8] CROSS-REPO AUDIT (오지랖)
echo.

REM Core repo
if exist D:\GitHub\WKAppBot\.git (
    echo --- WKAppBot Core ---
    cd /d D:\GitHub\WKAppBot
    git log --oneline -3 2>nul
)

REM personal-docs
if exist D:\GitHub\personal-docs\.git (
    echo.
    echo --- personal-docs ---
    cd /d D:\GitHub\personal-docs
    git log --oneline -3 2>nul
)

REM WkAutoQuant
if exist D:\GitHub\WkAutoQuant\.git (
    echo.
    echo --- WkAutoQuant ---
    cd /d D:\GitHub\WkAutoQuant
    git log --oneline -3 2>nul
)

REM ============ SUMMARY ============
cd /d D:\GitHub\wkappbot-sdk
echo.
echo ════════════════════════════════════════════
echo ✅ wk-gg-main COMPLETE
echo ════════════════════════════════════════════
echo.
echo COLLECTED:
echo   ✓ System health (Chrome, ports, Eye, memory)
echo   ✓ Local git status ^& commits
echo   ✓ CLAUDE.md Pending items count
echo   ✓ Latest skill updates (7d)
echo   ✓ Suggest backlog (긴급/중요)
echo   ✓ Version audit (README/SECURITY/CLAUDE)
echo   ✓ Repo health check
echo   ✓ Cross-repo commits (Core/personal/WkAutoQuant)
echo.
echo READY FOR: Suggest ranking + triage
echo.

REM Optional: background monitor
if "%1"=="--watch" (
    echo.
    echo Launching health monitor (Ctrl+C to exit)...
    powershell -File "D:\GitHub\wkappbot-sdk\bin\cdp-health-check.ps1" -Watch
)

echo.
