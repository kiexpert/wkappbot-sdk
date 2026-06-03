# harness:skill
# wkappbot skill read wkdoctor-sdk-environment-health-check
# wkappbot skill read crash-reboot-recovery-cascade
# wkappbot skill read wkappbot-taskkill-usage

# wkdoctor check: wkappbot INFINITE-SPAWN detector + keyboard-lag forensic summary.
# STANDALONE (Get-Process + Get-CimInstance + the watchdog forensic logs; NO wkappbot
# subprocess -- robust in any execution context). The harness watchdog
# (wkharness-guards.ps1) captures lag-forensics.jsonl + lag-procsnap.jsonl during a RAM
# storm; this plugin surfaces a runaway wkappbot/WmiPrvSE spawn + the recent storm trail so
# the cause is visible AFTER the freeze (the user cannot prompt while frozen).
# Pattern mirrors 06-defender-exclusions.ps1: Add-Check/Emit from orchestrator scope, never throws.

$wkHome  = Join-Path $env:USERPROFILE '.claude\wkharness'
$forLog  = Join-Path $wkHome 'lag-forensics.jsonl'

# (1) Live spawn counts (Get-Process, no wkappbot subprocess).
$procs = @(); try { $procs = Get-Process -ErrorAction SilentlyContinue } catch {}
$wkCore = @($procs | Where-Object { $_.Name -match '(?i)^wkappbot-core$' }).Count
$wkFam  = @($procs | Where-Object { $_.Name -match '(?i)^(wkappbot|wkappbot-core|wkchat|wka11y)$' }).Count
$wmi    = @($procs | Where-Object { $_.Name -match '(?i)^WmiPrvSE$' }).Count
# A couple of wkappbot-core are normal; many = infinite-spawn (cf. MouseCCA 100+ zombie cascade).
$spawnStorm = ($wkCore -gt 3 -or $wkFam -gt 8 -or $wmi -gt 8)

# (2) Most recent storm trail (last watchdog forensic entry).
$lastForensic = ''
try {
    if (Test-Path -LiteralPath $forLog) {
        $lastForensic = [string](Get-Content -LiteralPath $forLog -Tail 1 -Encoding UTF8 -ErrorAction SilentlyContinue)
    }
} catch {}

$detail = "wkappbot-core=$wkCore family=$wkFam WmiPrvSE=$wmi"
if ($lastForensic) {
    $_lf = if ($lastForensic.Length -gt 220) { $lastForensic.Substring(0,220) + '...' } else { $lastForensic }
    $detail += " | last-storm: $_lf"
}

if ($spawnStorm) {
    # Surface the runaway wkappbot-core command lines so the user sees WHAT spawned them.
    $dupCmd = ''
    try {
        $dupCmd = (Get-CimInstance Win32_Process -Filter "Name='wkappbot-core.exe'" -ErrorAction SilentlyContinue |
                   Select-Object -First 5 | ForEach-Object { "pid$($_.ProcessId): $([string]$_.CommandLine)" }) -join '  ||  '
    } catch {}
    $msg = "INFINITE-SPAWN -- $detail`n  runaway cmdlines: $dupCmd`n  reap: wkappbot taskkill --force <pid,...>  OR  wkdoctor -EmergencyKill"
    Add-Check 'wkappbot spawn-storm' 'fail' "INFINITE-SPAWN: $detail"
    Emit 'fail' 'wkappbot spawn-storm' $msg
} else {
    Add-Check 'wkappbot spawn-storm' 'ok' $detail
    Emit 'ok' 'wkappbot spawn-storm' $detail
}
