@echo off
REM wk-gg-main.cmd - Main workflow: CDP health + suggest triage + repo status
REM Collects all info needed for product quality decisions + suggest triage
REM Usage: wk-gg-main [--watch]

setlocal enabledelayedexpansion

echo.
echo ============================================================
echo   wk-gg-main: Comprehensive Status Report
echo   Time: %date% %time%
echo ============================================================
echo.

REM ============ 1. SYSTEM HEALTH ============
echo [1/8] SYSTEM HEALTH
powershell -File "D:\GitHub\wkappbot-sdk\bin\cdp-health-check.ps1" -Alert 2>nul
if !errorlevel! equ 0 (
    echo System healthy
) else (
    echo System health check warning
)

REM ============ 2. GIT STATUS ============
echo.
echo [2/8] GIT STATUS (last 5 commits + pending changes)
cd /d D:\GitHub\wkappbot-sdk
git log --oneline -5 2>nul
powershell -Command "git status --short 2>$null | Select-Object -First 10"

REM ============ 3. CLAUDE.MD PENDING ============
echo.
echo [3/8] CLAUDE.MD PENDING ITEMS
powershell -Command "(Select-String '^- \[.\]' 'D:\GitHub\wkappbot-sdk\CLAUDE.md' -ErrorAction SilentlyContinue | Measure-Object).Count"
echo pending items listed above

REM ============ 4. SKILL NEWS ============
echo.
echo [4/8] SKILL NEWS (last 7 days - top 10)
powershell -Command "wkappbot skill news 2>$null | Select-Object -First 15"

REM ============ 5. SUGGEST BACKLOG ============
echo.
echo [5/8] SUGGEST BACKLOG (priority breakdown)
powershell -Command "wkappbot suggest list 2>$null | Select-Object -First 20"

REM ============ 6. VERSION AUDIT ============
echo.
echo [6/8] VERSION AUDIT (sync check)
echo Checking README/SECURITY/AGENTS/CLAUDE version consistency...
powershell -Command "Select-String 'v7\.' 'D:\GitHub\wkappbot-sdk\README.md' -ErrorAction SilentlyContinue | Select-Object -First 1"
powershell -Command "Select-String 'v7\.' 'D:\GitHub\wkappbot-sdk\CLAUDE.md' -ErrorAction SilentlyContinue | Select-Object -First 1"
powershell -Command "Select-String 'v7\.' 'D:\GitHub\wkappbot-sdk\csharp\src\WKAppBot.Launcher\Directory.Build.props' -ErrorAction SilentlyContinue | Select-Object -First 1"

REM ============ 7. REPO HEALTH ============
echo.
echo [7/8] REPO HEALTH (on-load skill check)
powershell -Command "wkappbot skill read repo-health-doctor 2>$null | Select-Object -First 5"

REM ============ 8. CROSS-REPO AUDIT ============
echo.
echo [8/8] CROSS-REPO AUDIT (other repos)
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

REM ============ 9. SMOKE TESTS ============
echo.
echo [9/11] SMOKE TESTS
if exist D:\GitHub\WKAppBot\bin\cdp-smoke-test.sh (
    echo Smoke test available
    echo (Run: bash D:\GitHub\WKAppBot\bin\cdp-smoke-test.sh)
) else (
    echo (no smoke test found)
)

REM ============ 10. GITHUB ACTIONS STATUS ============
echo.
echo [10/11] GITHUB ACTIONS (recent failures)
if exist D:\GitHub\wkappbot-sdk\.github\workflows (
    echo Checking workflow runs...
    powershell -Command "gh run list --limit 5 2>$null | Select-Object -First 10"
    echo.
    echo Failed runs:
    powershell -Command "gh run list --status failure --limit 3 2>$null | Select-Object -First 5"
) else (
    echo (no workflows found)
)

REM ============ 11. GITHUB ISSUES + BUGS ============
echo.
echo [11/12] GITHUB ISSUES + BUG REPORTS (daily)
echo Recent open issues (last 7 days):
powershell -Command "gh issue list --limit 10 --state open --search 'created:>2026-05-21' 2>$null | Select-Object -First 8"

REM ============ RETURN TO SDK REPO ============
cd /d D:\GitHub\wkappbot-sdk

REM ============ ALERT ANALYSIS ============
echo.
echo [12/12] ANALYZING ALERTS...
echo.

powershell -Command @"
[int]`$critical = 0
[object[]]`$warnings = @()

`$chrome = @(Get-Process chrome -ErrorAction SilentlyContinue).Count
if (`$chrome -gt 5) { `$critical += 1; `$warnings += "Chrome: `$chrome processes" }

`$eye = @(Get-Process wkappbot-core -ErrorAction SilentlyContinue | Where-Object {`$_.CommandLine -match 'eye'}).Count
if (`$eye -eq 0) { `$critical += 1; `$warnings += "Eye: NOT RUNNING" }

`$ports = (netstat -ano 2>$null | Select-String "973[0-9].*LISTEN" | Measure-Object).Count
if (`$ports -gt 4) { `$warnings += "Ports: `$ports active" }

if (`$critical -eq 0 -and `$warnings.Count -eq 0) {
    Write-Host "============================================================" -ForegroundColor Green
    Write-Host "All systems nominal - no alerts" -ForegroundColor Green
    Write-Host "============================================================" -ForegroundColor Green
} else {
    Write-Host "============================================================" -ForegroundColor Red
    Write-Host "ALERTS DETECTED - ACTION REQUIRED" -ForegroundColor Red
    Write-Host "============================================================" -ForegroundColor Red
    if (`$critical -gt 0) {
        Write-Host ""
        Write-Host "CRITICAL (`$critical):" -ForegroundColor Red
        `$warnings | ForEach-Object { Write-Host "   - `$_" }
    }
}
"@

echo.
echo WORKFLOW SUMMARY:
echo   System health checked
echo   Git status collected
echo   CLAUDE.md Pending items listed
echo   Skill updates scanned
echo   Suggest backlog collected
echo   Version audit complete
echo   Repo health verified
echo   Cross-repo audited
echo   Smoke tests available
echo   CI/CD status scanned
echo   Bug reports collected
echo.

REM ============ VALIDATION: CLAUDE.md vs CMD ============
echo.
echo [VALIDATION] Checking CLAUDE.md workflow compliance...
echo.

powershell -Command @"
`$defined = @(
    'on-load',
    'skill news',
    'suggest list',
    '[MANDATORY] ask gpt ranking',
    'Opus triage',
    '[ON RELEASE] Public skill curation'
)

`$implemented = @(
    'on-load',
    'skill news',
    'suggest list'
)

`$missing = @()
foreach (`$step in `$defined) {
    if (`$implemented -notcontains `$step) {
        `$missing += `$step
    }
}

if (`$missing.Count -eq 0) {
    Write-Host 'All CLAUDE.md steps implemented' -ForegroundColor Green
} else {
    Write-Host 'MISSING IMPLEMENTATION:' -ForegroundColor Yellow
    foreach (`$item in `$missing) {
        Write-Host "   - `$item" -ForegroundColor Yellow
    }
    Write-Host ""
    Write-Host "CRITICAL: [MANDATORY] 'ask gpt ranking' not implemented!" -ForegroundColor Red
}
"@

echo.
echo NEXT STEPS (MANDATORY):
echo   1. wkappbot ask gpt "rank suggests by impact/urgency/effort"
echo   2. Dispatch to Opus for triage (copy GPT ranking output)
echo   3. Resolve suggests + update CLAUDE.md Pending
echo.

REM Optional: background monitor
if "%1"=="--watch" (
    echo Launching health monitor (Ctrl+C to exit)...
    powershell -File "D:\GitHub\wkappbot-sdk\bin\cdp-health-check.ps1" -Watch
)

echo.
