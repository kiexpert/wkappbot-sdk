# Live GUI QA for the Windows core on a desktop-session runner (windows-latest has one).
# Usage: pwsh -File win-gui-qa.ps1 -Core <path to wkappbot-core.exe> -Skills <sdk skills dir>
# Each check runs the core with a time budget; any FAIL makes the script exit 1 after all checks ran.
param(
    [Parameter(Mandatory)][string]$Core,
    [Parameter(Mandatory)][string]$Skills,
    [int]$BudgetSec = 120
)
$ErrorActionPreference = 'Stop'
$Core = (Resolve-Path $Core).Path
$work = Join-Path ([IO.Path]::GetTempPath()) "wk-gui-qa-$PID"
New-Item -ItemType Directory -Force $work | Out-Null
$script:fails = 0

function Invoke-Core([string]$Name, [string[]]$CmdArgs) {
    $out = Join-Path $work "$Name.out"; $err = Join-Path $work "$Name.err"
    $sw = [Diagnostics.Stopwatch]::StartNew()
    $p = Start-Process $Core -ArgumentList $CmdArgs -NoNewWindow -PassThru -RedirectStandardOutput $out -RedirectStandardError $err
    $done = $p.WaitForExit($BudgetSec * 1000)
    if (-not $done) { try { $p.Kill($true) } catch {} }
    $text = (Get-Content $out -Raw -ErrorAction SilentlyContinue) + ''
    [pscustomobject]@{ Name = $Name; Done = $done; Rc = if ($done) { $p.ExitCode } else { -1 }; Out = $text
                       Err = (Get-Content $err -Raw -ErrorAction SilentlyContinue) + ''; Ms = $sw.ElapsedMilliseconds }
}
function Report([string]$Name, [bool]$Ok, [string]$Detail) {
    if ($Ok) { Write-Host "PASS $Name -- $Detail" }
    else { Write-Host "::warning::FAIL $Name -- $Detail"; $script:fails++ }  # advisory tier: never ::error::
}
function Check([string]$Name, [string[]]$CmdArgs, [string]$Expect = '\S') {
    $r = Invoke-Core $Name $CmdArgs
    $first = ($r.Out -split "`n" | Where-Object { $_.Trim() } | Select-Object -First 1)
    if (-not $r.Done) { Report $Name $false "hung > $BudgetSec s"; return $r }
    $ok = ($r.Rc -eq 0) -and ($r.Out -match $Expect)
    Report $Name $ok "rc=$($r.Rc) $($r.Ms) ms: $first"
    if (-not $ok) {  # full evidence for the failing check
        Write-Host "---- $Name stdout (last 40 lines)"; ($r.Out -split "`n" | Select-Object -Last 40) | ForEach-Object { Write-Host "  $_" }
        Write-Host "---- $Name stderr (last 20 lines)"; ($r.Err -split "`n" | Select-Object -Last 20) | ForEach-Object { Write-Host "  $_" }
    }
    return $r
}
function Get-CharmapCount { @(Get-Process charmap -ErrorAction SilentlyContinue).Count }

# 1. window enumeration ('a11y windows' is a deprecated alias that exits 1; the verb is 'windows')
Check 'windows' @('windows') | Out-Null

# 2. a11y read on a window this script starts
Get-Process charmap -ErrorAction SilentlyContinue | Stop-Process -Force
$cm = Start-Process charmap -PassThru
Start-Sleep -Seconds 3
Check 'a11y read charmap' @('a11y', 'read', '*Character Map*') '(?i)character|font|charmap|select' | Out-Null
try { $cm | Stop-Process -Force } catch {}
Start-Sleep -Seconds 1

# 3. exec app lifetime (RunCommand.JobLifetime.cs): --close-after was removed; the app exec launched
#    lives exactly as long as exec does, so ending exec must close it.
$ex = Start-Process $Core -ArgumentList @('exec', 'charmap') -NoNewWindow -PassThru `
    -RedirectStandardOutput (Join-Path $work 'exec.out') -RedirectStandardError (Join-Path $work 'exec.err')
$up = 0
for ($i = 0; $i -lt 15 -and $up -eq 0; $i++) { Start-Sleep -Seconds 1; $up = Get-CharmapCount }
Report 'exec charmap launches the app' ($up -gt 0) "charmap processes while exec runs: $up"
try { Stop-Process -Id $ex.Id -Force } catch {}
$left = 1
for ($i = 0; $i -lt 10 -and $left -gt 0; $i++) { Start-Sleep -Seconds 1; $left = Get-CharmapCount }
Report 'ending exec closes charmap' ($left -eq 0) "charmap processes after exec ended: $left"
Get-Process charmap -ErrorAction SilentlyContinue | Stop-Process -Force

# 4. CDP against the runner's Chrome
Check 'cdp open about:blank' @('cdp', 'open', 'about:blank') '.*' | Out-Null

# 5. license status
Check 'license status' @('license', 'status') | Out-Null

# 6. skill edit on a throwaway fork of on-load, then read the new step back
$hqSkills = Join-Path (Split-Path $Core) 'wkappbot.hq/skills/wkappbot-workflow'
New-Item -ItemType Directory -Force $hqSkills | Out-Null
$src = Get-Content (Join-Path $Skills 'wkappbot-workflow/on-load.skill.json') -Raw | ConvertFrom-Json
$src.id = 'qa-throwaway-fork'; $src.title = 'QA throwaway fork'
$src | ConvertTo-Json -Depth 20 | Set-Content (Join-Path $hqSkills 'qa-throwaway-fork.skill.json') -Encoding utf8
$marker = "qa throwaway step $PID"
Check 'skill edit --add-step (fork)' @('skill', 'edit', 'qa-throwaway-fork', '--add-step', $marker) '.*' | Out-Null
Check 'skill read fork shows the step' @('skill', 'read', 'qa-throwaway-fork', '--keep', $marker) ([regex]::Escape($marker)) | Out-Null

Write-Host "GUI QA: $script:fails failure(s)"
exit ([int]($script:fails -gt 0))
