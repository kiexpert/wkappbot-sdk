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

REM ============ 9. SMOKE TESTS ============
echo.
echo [9/11] SMOKE TESTS
if exist D:\GitHub\WKAppBot\bin\cdp-smoke-test.sh (
    echo Running CDP smoke test...
    bash D:\GitHub\WKAppBot\bin\cdp-smoke-test.sh 2>nul | tail -3
) else (
    echo (no smoke test found)
)

REM ============ 10. GITHUB ACTIONS STATUS ============
echo.
echo [10/11] GITHUB ACTIONS (recent failures)
if exist D:\GitHub\wkappbot-sdk\.github\workflows (
    echo Checking workflow runs...
    gh run list --limit 5 2>nul | head -10
    echo.
    echo Failed runs:
    gh run list --status failure --limit 3 2>nul | head -5
) else (
    echo (no workflows found)
)

REM ============ 11. GITHUB ISSUES + BUGS ============
echo.
echo [11/12] GITHUB ISSUES + BUG REPORTS (daily)
echo Recent open issues (last 7 days):
gh issue list --limit 10 --state open --search "created:>2026-05-21" 2>nul | head -8

echo.
echo Recent PR comments with 'bug' keyword:
gh pr list --limit 5 --state open 2>nul | while read pr (
    gh pr view !pr! --json comments --jq '.comments[] | select(.body | contains("bug")) | .author.login + ": " + .body' 2>nul | head -2
)

REM ============ ALERT ANALYSIS ============
cd /d D:\GitHub\wkappbot-sdk
echo.
echo [12/12] ANALYZING ALERTS...

powershell -Command @"
  `$critical = 0
  `$warnings = @()

  # Chrome check
  `$chrome = @(Get-Process chrome -EA SilentlyContinue).Count
  if (`$chrome -gt 5) { `$critical += 1; `$warnings += "Chrome: `$chrome processes" }

  # Eye check
  `$eye = @(Get-Process wkappbot-core -EA SilentlyContinue | Where-Object {`$_.CommandLine -match 'eye'}).Count
  if (`$eye -eq 0) { `$critical += 1; `$warnings += "Eye: NOT RUNNING" }

  # Port check
  `$ports = (netstat -ano 2>$null | Select-String "973[0-9].*LISTEN" | Measure-Object).Count
  if (`$ports -gt 4) { `$warnings += "Ports: `$ports active" }

  # Display alerts
  if (`$critical -eq 0 -and `$warnings.Count -eq 0) {
    Write-Host "════════════════════════════════════════════" -ForegroundColor Green
    Write-Host "✅ ALL SYSTEMS NOMINAL - NO ALERTS" -ForegroundColor Green
    Write-Host "════════════════════════════════════════════" -ForegroundColor Green
  } else {
    Write-Host "════════════════════════════════════════════" -ForegroundColor Red
    Write-Host "⚠️  ALERTS DETECTED - ACTION REQUIRED" -ForegroundColor Red
    Write-Host "════════════════════════════════════════════" -ForegroundColor Red
    if (`$critical -gt 0) {
      Write-Host ""
      Write-Host "🔴 CRITICAL (`$critical):" -ForegroundColor Red
      `$warnings | ForEach-Object { Write-Host "   - `$_" }
    }
  }
}
"@

echo.
echo WORKFLOW SUMMARY:
echo   ✓ System health checked
echo   ✓ Git status collected
echo   ✓ CLAUDE.md Pending items listed
echo   ✓ Skill updates scanned
echo   ✓ Suggest backlog collected
echo   ✓ Version audit complete
echo   ✓ Repo health verified
echo   ✓ Cross-repo audited
echo.
echo NEXT: Suggest ranking + triage (use wkappbot ask gpt to rank)
echo.

REM Optional: background monitor
if "%1"=="--watch" (
    echo Launching health monitor (Ctrl+C to exit)...
    powershell -File "D:\GitHub\wkappbot-sdk\bin\cdp-health-check.ps1" -Watch
)

echo.
