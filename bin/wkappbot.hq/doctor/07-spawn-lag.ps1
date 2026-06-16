# harness:skill
# wkappbot skill read wkappbot-log-search  # Usage: reading/interpreting wkappbot log files
# wkappbot skill read wkdoctor-sdk-environment-health-check  # Impl: doctor plugin Add-Check/Emit pattern
# wkappbot skill read wkappbot-taskkill-usage  # Task: classify + reap the runaway processes

# wkdoctor check: wkappbot INFINITE-SPAWN detector + keyboard-lag forensic summary.
# STANDALONE (Get-Process + Get-CimInstance + log filenames + the harness watchdog forensic
# logs; NO wkappbot subprocess for detection -- robust in any execution context). The harness
# watchdog (wkharness-guards.ps1) captures lag-forensics.jsonl + lag-procsnap.jsonl during a
# RAM storm; this plugin surfaces a runaway wkappbot/WmiPrvSE spawn + the recent storm trail so
# the cause is visible AFTER a freeze, then auto-files a wkappbot suggest BUG.
# Pattern mirrors 06-defender-exclusions.ps1: Add-Check/Emit from orchestrator scope, never throws.

$wkHome = Join-Path $env:USERPROFILE '.claude\wkharness'
$forLog = Join-Path $wkHome 'lag-forensics.jsonl'

# (1) Live spawn counts (Get-Process, no wkappbot subprocess).
$procs = @(); try { $procs = Get-Process -ErrorAction SilentlyContinue } catch {}
$wkCore = @($procs | Where-Object { $_.Name -match '(?i)^wkappbot-core$' }).Count
$wkFam  = @($procs | Where-Object { $_.Name -match '(?i)^(wkappbot|wkappbot-core|wkchat|wka11y)$' }).Count
$wmi    = @($procs | Where-Object { $_.Name -match '(?i)^WmiPrvSE$' }).Count
# A couple of wkappbot-core are normal; many = infinite-spawn (cf. MouseCCA 100+ zombie cascade).
$spawnStorm = ($wkCore -gt 3 -or $wkFam -gt 8 -or $wmi -gt 8)

# (2) LOG-FILE analysis -- the cmdline source that SURVIVES extreme lag (when live Get-Process /
# CIM hangs or returns empty). wkappbot-core logs are named
# <hq>/logs/wkappbot-core.exe.out-<ts>.<cmd>.pid=<pid>.log, so the filename alone encodes each
# child's command + pid. Group recent (60min) logs by command to see WHICH command is multiplying.
$logDir = Join-Path (Split-Path $PSScriptRoot -Parent) 'logs'
$logCmdSummary = ''; $logRecentCount = 0
try {
    if (Test-Path -LiteralPath $logDir) {
        $_cut = (Get-Date).AddMinutes(-60)
        $_recent = @(Get-ChildItem -LiteralPath $logDir -Filter 'wkappbot-core.exe*.log' -ErrorAction SilentlyContinue | Where-Object { $_.LastWriteTime -gt $_cut })
        $logRecentCount = $_recent.Count
        $_groups = @{}
        foreach ($_f in $_recent) {
            $_c = if ($_f.Name -match '\.out-\d+_\d+\.(.+?)\.pid=\d+') { $Matches[1] } else { 'other' }
            if ($_groups.ContainsKey($_c)) { $_groups[$_c]++ } else { $_groups[$_c] = 1 }
        }
        $logCmdSummary = (($_groups.GetEnumerator() | Sort-Object Value -Descending | Select-Object -First 6 | ForEach-Object { "$($_.Key)x$($_.Value)" }) -join ', ')
    }
} catch {}
# Spawn-storm if the LIVE counts OR the log trail (>20 wkappbot-core logs in 60min) say so --
# the log path is the fallback that still works when extreme lag makes Get-Process unreliable.
if ($logRecentCount -gt 20) { $spawnStorm = $true }

# (3) Most recent storm trail (last watchdog forensic entry).
$lastForensic = ''
try {
    if (Test-Path -LiteralPath $forLog) {
        $lastForensic = [string](Get-Content -LiteralPath $forLog -Tail 1 -Encoding UTF8 -ErrorAction SilentlyContinue)
    }
} catch {}

$detail = "wkappbot-core=$wkCore family=$wkFam WmiPrvSE=$wmi logs60m=$logRecentCount"
if ($logCmdSummary) { $detail += " cmds=[$logCmdSummary]" }
if ($lastForensic) {
    $_lf = if ($lastForensic.Length -gt 200) { $lastForensic.Substring(0,200) + '...' } else { $lastForensic }
    $detail += " | last-storm: $_lf"
}

# A real STORM needs RECENT churn (logs60m>0) or an EXTREME standing count. A merely-high count
# with logs60m=0 (many agent subprocesses, no ACTIVE spawning) is a WARN, not a false [x] FAIL --
# it was firing [x] on every busy multi-agent session (user 2026-06-17).
$realStorm = $spawnStorm -and ($logRecentCount -gt 0 -or $wkCore -gt 30 -or $wkFam -gt 40)
if ($realStorm) {
    # Surface the runaway wkappbot-core command lines so the user sees WHAT spawned them.
    $dupCmd = ''
    try {
        $dupCmd = (Get-CimInstance Win32_Process -Filter "Name='wkappbot-core.exe'" -ErrorAction SilentlyContinue |
                   Select-Object -First 5 | ForEach-Object { "pid$($_.ProcessId): $([string]$_.CommandLine)" }) -join '  ||  '
    } catch {}
    if (-not $dupCmd) { $dupCmd = "(live CIM empty -- see log cmds: $logCmdSummary)" }
    $msg = "INFINITE-SPAWN -- $detail`n  runaway cmdlines: $dupCmd`n  reap: wkappbot taskkill --force <pid,...>  OR  wkdoctor -EmergencyKill"
    Add-Check 'wkappbot spawn-storm' 'fail' "INFINITE-SPAWN: $detail"
    Emit 'fail' 'wkappbot spawn-storm' $msg
    # AUTO bug-suggest to wkappbot-core (confirmed wkappbot bug per the MANDATORY report rule).
    # Throttled once per 24h via a marker so repeated wkdoctor runs do not spam the queue.
    try {
        $_sugMark = Join-Path $wkHome 'spawn-storm-suggested.txt'
        $_sugNow  = [int][double]::Parse((Get-Date -UFormat %s))
        $_sugLast = if (Test-Path -LiteralPath $_sugMark) { try { [int]((Get-Content -LiteralPath $_sugMark -Raw).Trim()) } catch { 0 } } else { 0 }
        if (($_sugNow - $_sugLast) -ge 86400) {
            $_sugText = "BUG: wkappbot-core infinite-spawn -- $detail. eye guardian respawn appears to multiply mcp / whisper-ring children unbounded (keyboard-lag + RAM storm). Repro: run wkdoctor and observe the wkappbot-core count. Expected: a small bounded number. Runaway cmdlines: $dupCmd"
            $_sugOk = $false
            try { & wkappbot suggest $_sugText --requirement "wkappbot eye status => single guardian" --requirement "proposed: wkdoctor => spawn-storm ok" --requirement "proposed: Get-Process wkappbot-core => bounded count" 2>&1 | Out-Null; if ($LASTEXITCODE -eq 0) { $_sugOk = $true } } catch {}
            if (-not $_sugOk) { try { & wkappbot-core.exe suggest $_sugText --requirement "wkappbot eye status => single guardian" --requirement "proposed: wkdoctor => spawn-storm ok" --requirement "proposed: Get-Process wkappbot-core => bounded count" 2>&1 | Out-Null } catch {} }
            Set-Content -LiteralPath $_sugMark -Value $_sugNow -ErrorAction SilentlyContinue
            Emit 'warn' 'wkappbot spawn-storm' 'auto-filed a wkappbot suggest BUG (throttled 24h)'
        }
    } catch {}
} elseif ($spawnStorm) {
    Add-Check 'wkappbot spawn-storm' 'warn' "high process count but NO recent spawn churn (logs60m=$logRecentCount) -- standing count, not an active storm: $detail"
    Emit 'warn' 'wkappbot spawn-storm' "high count, no recent churn (likely agent subprocesses): $detail"
} else {
    Add-Check 'wkappbot spawn-storm' 'ok' $detail
    Emit 'ok' 'wkappbot spawn-storm' $detail
}
