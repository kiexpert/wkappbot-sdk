# Ghost Score Ranking System (v4.2 - Normalized Live CPU + Depth Penalty)
# Philosophy: Stability is measured by live interference, not cumulative CPU time.
# Quiet processes (<1% CPU, <100MB) are Healthy and encouraged.

$now = Get-Date
$aiProcs = @('claude', 'wkappbot-core', 'gemini', 'agent', 'wkchat', 'wka11y')
$ignoreList = @('Idle', 'System', 'Registry', 'Memory Compression')

function Get-RamPct {
    try {
        $cs = Get-CimInstance -ClassName Win32_OperatingSystem -ErrorAction SilentlyContinue
        return [int](100 - ($cs.FreePhysicalMemory * 100 / $cs.TotalVisibleMemorySize))
    } catch { return 50 }
}

function Get-LineageInfo {
    param($p, $allProcs)
    $depth = 0
    $current = $p
    $isAiDescendant = $false
    $isOrphan = $false

    while ($current.ParentProcessId -gt 1 -and $depth -lt 10) {
        $parent = $allProcs | Where-Object { $_.PID -eq $current.ParentProcessId }
        if (-not $parent) {
            $isOrphan = $true
            break
        }
        if ($parent.Name -in $aiProcs) {
            $isAiDescendant = $true
            break
        }
        $current = $parent
        $depth++
    }
    return @{ IsAi = $isAiDescendant; IsOrphan = $isOrphan; Depth = $depth }
}

function Get-CommandLineRiskScore {
    param($p)

    $cmd = [string]$p.CommandLine
    if ([string]::IsNullOrWhiteSpace($cmd)) { return 0.0 }

    $name = [string]$p.Name
    $risk = 0.0

    if ($name -match '^(powershell|pwsh|cmd|wscript|cscript|mshta|rundll32|regsvr32|python|node|java|perl|ruby|bash|sh)$') {
        $risk += 8
    }

    if ($cmd -match '(?i)(-enc|-encodedcommand|frombase64string|invoke-webrequest|invoke-expression|iwr\b|wget\b|curl\b|downloadstring|downloadfile|bypass|noprofile|hidden)') {
        $risk += 10
    }

    if ($cmd -match '(?i)(https?://|ftp://|\\\\)') {
        $risk += 8
    }

    if ($cmd -match '(?i)(\\AppData\\Local\\Temp\\|\\Temp\\|\\Downloads\\|%TEMP%|%TMP%)') {
        $risk += 8
    }

    if ($cmd.Length -gt 220) {
        $risk += 4
    }

    return [Math]::Min(20.0, $risk)
}

$systemRamPct = Get-RamPct

# 1. Fetch Process List
if (-not $taskListPath) { $taskListPath = "D:\GitHub\WKAppBot\bin\wktasklist.ps1" }
try {
    $procList = & $taskListPath -Json -ErrorAction Stop | ConvertFrom-Json
    $procList = @($procList)   # PS 5.1: normalize the CFJ array (else the foreach below iterates once on the whole array)
} catch {
    Add-Check "ghost-score" "warn" "Failed to fetch tasklist for scoring"
    return
}

$scoredItems = [System.Collections.Generic.List[PSCustomObject]]::new()

foreach ($p in $procList) {
    if ($p.Name -in $ignoreList) { continue }

    $lineage = Get-LineageInfo $p $procList

    # --- Scoring Logic ---
    $cpuPct = if ($null -ne $p.PSObject.Properties['CpuPct']) { [double]$p.CpuPct } else { 0.0 }
    $cpuRisk = [Math]::Min(45.0, $cpuPct * 1.8)
    $memRisk = [Math]::Min(25.0, [double]$p.WorkingSetMB / 32.0)
    $depthRisk = [Math]::Min(20.0, [Math]::Pow([Math]::Max(0, [double]$lineage.Depth), 2) * 4.0)
    $cmdRisk = Get-CommandLineRiskScore $p
    $orphanRisk = if ($lineage.IsOrphan) { 8.0 } else { 0.0 }
    $score = [Math]::Min(100.0, $cpuRisk + $memRisk + $depthRisk + $cmdRisk + $orphanRisk)

    $isLean = ($cpuPct -lt 1.0 -and $p.WorkingSetMB -lt 100)
    if ($isLean) {
        $score -= 50
    }

    if ($p.Name -in $aiProcs) { $score -= 1000 }
    if ($lineage.IsAi) { $score -= 500 }
    if ($p.Name -match '^(taskmgr|taskhostw|explorer|conhost)$' -and -not $lineage.IsOrphan) {
        $score -= 15
    }

    $scoredItems.Add([PSCustomObject]@{
        PID    = $p.PID
        Name   = $p.Name
        Score  = [Math]::Round($score, 1)
        IsAi   = $lineage.IsAi
        Orphan = $lineage.IsOrphan
        Depth  = $lineage.Depth
        MB     = $p.WorkingSetMB
        CPU    = $cpuPct
        IsLean = $isLean
    })
}

# 2. Output and Reporting
$threshold = 80
# In PowerShell, the default for Sort-Object is Ascending. Do not use -Ascending.
$topGhosts = $scoredItems | Sort-Object Score -Descending | Select-Object -First 5
$bestCitizens = $scoredItems | Sort-Object Score | Select-Object -First 3

if ($topGhosts -and $topGhosts[0].Score -gt $threshold) {
    Add-Check "ghost-score" "warn" "Interference detected (System RAM: $systemRamPct%)"

    if (-not $Json) {
        Write-Host "`n  TOP INTERFERENCE (High Score = Bad)" -ForegroundColor Yellow
        foreach ($proc in $topGhosts) {
            if ($proc.Score -le 0) { continue }
            $color = if ($proc.Score -gt 1000) { 'Red' } else { 'Yellow' }
            $tag = if ($proc.Orphan) { "[ORPHAN]" } else { "[ROOT]" }
            Write-Host ("     {0,7:N1} | D:{1,2} | PID:{2,7} | {3,-20} | {4}" -f $proc.Score, $proc.Depth, $proc.PID, $proc.Name, $tag) -ForegroundColor $color
        }

        Write-Host "`n  HEALTHY DAEMONS (Encouraged / Negative Score)" -ForegroundColor Green
        foreach ($proc in $bestCitizens) {
            if ($proc.Score -ge 0) { continue }
            Write-Host ("     {0,7:N1} | D:{1,2} | PID:{2,7} | {3,-20} | [HEALTHY]" -f $proc.Score, $proc.Depth, $proc.PID, $proc.Name) -ForegroundColor Green
        }
    }
} else {
    Add-Check "ghost-score" "ok" "System is healthy (RAM: $systemRamPct%)"
}
