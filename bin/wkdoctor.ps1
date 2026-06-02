# wkdoctor -- wkappbot-sdk health check orchestrator
# Usage: wkdoctor [-Json]
# Drop custom checks as *.ps1 into wkappbot.hq/doctor/ for plugin extension
param([switch]$Json, [switch]$EmergencyKill)

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$binDir     = $scriptRoot
$repoRoot   = Split-Path -Parent $scriptRoot
$pass = 0; $fail = 0; $warn = 0
$items = [System.Collections.Generic.List[PSCustomObject]]::new()

function Add-Check {
    param([string]$Name, [string]$Status, [string]$Detail)
    $items.Add([PSCustomObject]@{ Name = $Name; Status = $Status; Detail = $Detail })
    if ($Status -eq 'ok')       { $script:pass++ }
    elseif ($Status -eq 'fail') { $script:fail++ }
    else                        { $script:warn++ }
}

function Emit {
    param([string]$Status, [string]$Name, [string]$Detail)
    if ($Json) { return }
    $sym   = if ($Status -eq 'ok') { '[+]' } elseif ($Status -eq 'fail') { '[x]' } else { '[!]' }
    $color = if ($Status -eq 'ok') { 'Green' } elseif ($Status -eq 'fail') { 'Red' } else { 'Yellow' }
    $line = "$sym $Name"
    if ($Detail) { $line += " -- $Detail" }
    Write-Host $line -ForegroundColor $color
}

function Get-DoctorNote {
    $warnItems = @($items | Where-Object { $_.Status -eq 'warn' })
    if ($fail -gt 0) {
        return '문제는 남아 있습니다.'
    }
    if ($warnItems.Count -eq 0) {
        return '모두 정상입니다.'
    }

    $warnNames = @($warnItems | ForEach-Object { $_.Name })
    if ($warnNames.Count -gt 0 -and (@($warnNames -join ' ') -match 'codex')) {
        return "복구는 정상이고 codex 링크 경고만 남았습니다."
    }

    return "복구는 정상이고 경고 $($warnItems.Count)건만 남았습니다."
}

# --emergency-kill: iterative hysteresis reaper.
# Trigger=60% RAM, recover=50% RAM. Safe pool only: orphaned codex/python/node + WmiPrvSE.
# NEVER kills claude.exe / wkappbot-core / wkchat / wka11y / user apps.
if ($EmergencyKill) {
    function Get-RamPct {
        try {
            $cs = Get-CimInstance -ClassName Win32_OperatingSystem -ErrorAction Stop
            return [int](100 - ($cs.FreePhysicalMemory * 100 / $cs.TotalVisibleMemorySize))
        } catch { return 0 }
    }

    function Build-SafePool {
        $pool = [System.Collections.Generic.List[PSCustomObject]]::new()
        foreach ($img in @('codex', 'python', 'pythonw', 'node')) {
            try {
                Get-Process $img -ErrorAction SilentlyContinue | ForEach-Object {
                    $p = $_
                    $parentName = try { (Get-Process -Id (
                        (Get-CimInstance Win32_Process -Filter "ProcessId=$($p.Id)" -ErrorAction Stop).ParentProcessId
                        ) -ErrorAction Stop).Name.ToLower() } catch { '' }
                    if ($parentName -notin @('claude', 'wkappbot-core', 'wkchat', 'wka11y')) {
                        $pool.Add([PSCustomObject]@{ Id = $p.Id; Name = $p.Name; WS = $p.WorkingSet64 })
                    }
                }
            } catch {}
        }
        try {
            Get-Process WmiPrvSE -ErrorAction SilentlyContinue | ForEach-Object {
                $pool.Add([PSCustomObject]@{ Id = $_.Id; Name = $_.Name; WS = $_.WorkingSet64 })
            }
        } catch {}
        return @($pool | Sort-Object WS -Descending)
    }

    $killed = 0
    $hogInfo = 'unknown'
    $pool = Build-SafePool
    foreach ($proc in $pool) {
        $ram = Get-RamPct
        if ($ram -lt 50) { break }
        try {
            & wkappbot taskkill --force $proc.Id 2>&1 | Out-Null
            $killed++
            $wsMb = [int]($proc.WS / 1MB)
            Write-Host "[ek] killed $($proc.Name) pid=$($proc.Id) ws=${wsMb}MB ram=$ram%" -ForegroundColor Yellow
        } catch {}
    }
    $finalRam = Get-RamPct
    if ($finalRam -ge 50) {
        try {
            $top = Get-Process | Where-Object { $_.Name -notmatch '^(Idle|System)$' } |
                   Sort-Object WorkingSet64 -Descending | Select-Object -First 1
            if ($top) { $hogInfo = "$($top.Name) pid=$($top.Id) $([int]($top.WorkingSet64/1MB))MB" }
            & wkappbot speak "[emergency] RAM $finalRam% -- pool exhausted, top hog: $hogInfo" --bg 2>&1 | Out-Null
        } catch {}
        Write-Host "[ek] pool exhausted -- RAM $finalRam% top hog: $hogInfo" -ForegroundColor Red
    }
    $ekColor = if ($killed -gt 0) { 'Yellow' } else { 'Green' }
    Write-Host "[emergency-kill] done killed=$killed finalRam=$finalRam%" -ForegroundColor $ekColor
    exit 0
}

# Load check modules (sorted by name, so 00- runs before 01- etc.)
$doctorDir = Join-Path $binDir 'wkappbot.hq\doctor'
if (Test-Path $doctorDir -PathType Container) {
    Get-ChildItem $doctorDir -Filter '*.ps1' | Sort-Object Name | ForEach-Object { . $_.FullName }
} else {
    Add-Check 'doctor modules' 'warn' "not found at $doctorDir -- run setup.ps1"
    Emit '!' 'doctor modules' 'missing'
}

# Summary
if (-not $Json) {
    Write-Host ''
    $color = if ($fail -gt 0) { 'Red' } elseif ($warn -gt 0) { 'Yellow' } else { 'Green' }
    Write-Host "wkdoctor: $pass ok, $warn warn, $fail fail" -ForegroundColor $color
    Write-Host "wkdoctor note: $(Get-DoctorNote)" -ForegroundColor $color
}
if ($Json) {
    [PSCustomObject]@{ pass = $pass; warn = $warn; fail = $fail; items = $items } | ConvertTo-Json -Depth 5
}
if ($fail -gt 0) { exit 1 } else { exit 0 }
