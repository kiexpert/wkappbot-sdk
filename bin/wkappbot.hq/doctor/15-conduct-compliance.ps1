# wkdoctor check: conduct-compliance -- per-session on-load compliance AFTER compaction
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT: for recent main Claude sessions, was 'skill read on-load' run
#               AFTER the most recent compaction boundary?
#   (2) SAFE:   report only -- never block or modify sessions.
#   (3) SOLVE:  low compliance rate => recommend installing the on-load enforcement
#               PreToolUse guard (a separate ROOT task).
#   (4) PREVENT: this check runs every session so drift is visible immediately.
# FAIL-OPEN: parse errors / missing data => warn or na, never crash.
# Data source: %USERPROFILE%\.claude\projects\<slug>\*.jsonl
#   - Top-level uuid.jsonl = main sessions (exclude subagents/ subdirs)
#   - Compaction marker: type=user with isCompactSummary=true
#   - On-load read:  assistant entry whose Bash/PowerShell tool command contains
#                    "wkappbot skill read on-load" (or the wkappbot skill read on-load pattern)

# Derive self-sufficient SDK bin path (resilient against $binDir clobber by module 06)
$_m15_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path  # .../wkappbot.hq/doctor
$_m15_hqDir  = Split-Path -Parent $_m15_myDir                   # .../wkappbot.hq
$_m15_sdkBin = Split-Path -Parent $_m15_hqDir                   # .../bin (SDK bin root)

$LOOKBACK_DAYS = 3
$WARN_THRESHOLD = 0.60  # below this rate => warn

# ── PHASE 1: DETECT ──────────────────────────────────────────────────────────
$cutoff = (Get-Date).AddDays(-$LOOKBACK_DAYS).ToUniversalTime()

# Find all Claude project slugs that correspond to the SDK CWD
$claudeProjectsRoot = Join-Path $env:USERPROFILE '.claude\projects'

$sessionFiles = @()
if (Test-Path $claudeProjectsRoot -PathType Container) {
    # Collect all top-level uuid.jsonl files across all project slugs
    try {
        $allJsonls = Get-ChildItem $claudeProjectsRoot -Recurse -File -Filter '*.jsonl' -ErrorAction SilentlyContinue |
            Where-Object {
                # Top-level = parent is a slug dir (not a uuid subdir within the slug)
                $_.Name -match '^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\.jsonl$' -and
                $_.Directory.Parent.FullName -eq $claudeProjectsRoot -and
                $_.LastWriteTime.ToUniversalTime() -gt $cutoff
            }
        $sessionFiles = @($allJsonls)
    } catch {
        Add-Check 'conduct:on-load' 'warn' "cannot enumerate sessions: $_"
        Emit '!' 'conduct:on-load' "session scan failed: $($_)"
        return
    }
}

if ($sessionFiles.Count -eq 0) {
    Add-Check 'conduct:on-load' 'na' "no recent sessions (last ${LOOKBACK_DAYS}d)"
    Emit '!' 'conduct:on-load' "no sessions in last ${LOOKBACK_DAYS}d"
    return
}

function Test-OnLoadCompliance {
    param([string]$FilePath)
    # Returns: 'compliant' | 'non-compliant' | 'no-compaction-compliant' | 'no-compaction-no-onload' | 'error'
    try {
        $lines = [IO.File]::ReadAllLines($FilePath, [Text.Encoding]::UTF8)
        $lastCompactLine = -1

        # Find the last compaction boundary
        for ($i = 0; $i -lt $lines.Count; $i++) {
            $line = $lines[$i]
            # Quick pre-filter before JSON parse
            if ($line -notmatch 'isCompactSummary') { continue }
            try {
                $obj = $line | ConvertFrom-Json -ErrorAction Stop
                if ($obj.isCompactSummary -eq $true) {
                    $lastCompactLine = $i
                }
            } catch {}
        }

        # Search for on-load read (assistant Bash call containing "skill read on-load")
        $searchFrom = if ($lastCompactLine -ge 0) { $lastCompactLine } else { 0 }
        $windowEnd  = if ($lastCompactLine -lt 0) { [Math]::Min($lines.Count, 120) } else { $lines.Count }

        $foundOnLoad = $false
        for ($i = $searchFrom; $i -lt $windowEnd; $i++) {
            $line = $lines[$i]
            if ($line -notmatch 'skill read on-load') { continue }
            # Only count assistant-originated tool calls (Bash or PowerShell command)
            if ($line -match '"type":"assistant"' -and
                ($line -match '"name":"Bash"' -or $line -match '"name":"PowerShell"') -and
                $line -match 'skill read on-load') {
                $foundOnLoad = $true
                break
            }
        }

        if ($lastCompactLine -ge 0) {
            if ($foundOnLoad) { return 'compliant' } else { return 'non-compliant' }
        } else {
            if ($foundOnLoad) { return 'no-compaction-compliant' } else { return 'no-compaction-no-onload' }
        }
    } catch {
        return 'error'
    }
}

$results = @{
    compliant           = [System.Collections.Generic.List[string]]::new()
    non_compliant       = [System.Collections.Generic.List[string]]::new()
    nc_no_onload        = [System.Collections.Generic.List[string]]::new()
    nc_compliant        = [System.Collections.Generic.List[string]]::new()
    errors              = 0
}

foreach ($sf in $sessionFiles) {
    $status = Test-OnLoadCompliance -FilePath $sf.FullName
    $shortId = $sf.Name -replace '\.jsonl$','' | ForEach-Object { $_.Substring(0, [Math]::Min(8, $_.Length)) }
    switch ($status) {
        'compliant'               { $results.compliant.Add($shortId) }
        'non-compliant'           { $results.non_compliant.Add($shortId) }
        'no-compaction-compliant' { $results.nc_compliant.Add($shortId) }
        'no-compaction-no-onload' { $results.nc_no_onload.Add($shortId) }
        'error'                   { $results.errors++ }
    }
}

$total         = $sessionFiles.Count
$withCompact   = $results.compliant.Count + $results.non_compliant.Count
$fullCompliant = $results.compliant.Count + $results.nc_compliant.Count  # on-load found in valid window
$rate          = if ($total -gt 0) { [Math]::Round($fullCompliant / $total, 2) } else { 1.0 }

# Determine overall status
$checkStatus = if ($withCompact -eq 0) {
    'ok'   # no compacted sessions -- no compaction gap possible
} elseif ($rate -ge $WARN_THRESHOLD) {
    'ok'
} else {
    'warn'
}

# ── PHASE 2: SAFE (report only) ──────────────────────────────────────────────
$detail  = "${total} sessions/${LOOKBACK_DAYS}d, ${fullCompliant} compliant (rate=$([int]($rate*100))%)"
$detail += if ($withCompact -gt 0) { ", $($results.compliant.Count)/$withCompact post-compaction" } else { ", 0 compacted" }
if ($results.non_compliant.Count -gt 0) {
    $examples = ($results.non_compliant | Select-Object -First 2) -join ', '
    $detail += " -- non-compliant: $examples"
}
if ($results.errors -gt 0) { $detail += " ($($results.errors) parse errors)" }

Add-Check 'conduct:on-load' $checkStatus $detail
Emit $checkStatus 'conduct:on-load' $detail

# ── PHASE 3: SOLVE (recommend guard if low compliance) ────────────────────────
if ($checkStatus -eq 'warn') {
    $solveMsg = "low compliance rate -- install PreToolUse on-load enforcement guard (separate ROOT task)"
    Add-Check 'conduct:on-load:solve' 'warn' $solveMsg
    Emit '!' 'conduct:on-load:solve' $solveMsg
}

# ── PHASE 4: PREVENT (self-documenting) ──────────────────────────────────────
# This check auto-runs every session -- no additional guard needed here.
