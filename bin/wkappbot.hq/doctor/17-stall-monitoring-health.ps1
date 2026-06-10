# wkdoctor check: stall-monitoring-health -- per-family stall-brake connectivity audit
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT: for each family, scan recent sessions for the longest unbroken solo run
#               of consecutive tool-call timestamps with NO break event between them.
#               A break = user-turn, delegation, or genuine gap > BREAK_GAP_SEC.
#               Compare longest solo run against the family's stall threshold.
#               Also check whether actual stall-guard BLOCK events appear in sessions.
#   (2) SAFE:   report only -- no blocking, no session edits.
#   (3) SOLVE:  if a family's max run > threshold and 0 stall events:
#               recommend wiring per-family session adapter (R4 gap).
#   (4) PREVENT: this check runs every session; stall disconnects become visible immediately.
# FAIL-OPEN: missing store / parse error => na/warn line, never crash the doctor.
# KEY: stall is TIME-BASED (no token data needed). Only timestamps are required.
#
# Family stores:
#   Claude  -- %USERPROFILE%\.claude\projects\<slug>\*.jsonl  (top-level main sessions)
#   Codex   -- %USERPROFILE%\.codex\sessions\<year>\<mm>\<dd>\rollout-*.jsonl
#   Gemini  -- %USERPROFILE%\.gemini\wkgemini_usage.jsonl  (per-call API log)
#
# Stall thresholds (from wkharness Get-WkStallPolicy, seconds):
#   Claude (Sonnet): 360s (6 min)  |  Codex: 420s (7 min)  |  Gemini-pro: 420s (7 min)

# Derive self-sufficient SDK bin path (resilient against $binDir clobber by other modules)
$_m17_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path  # .../wkappbot.hq/doctor
$_m17_hqDir  = Split-Path -Parent $_m17_myDir                   # .../wkappbot.hq
$_m17_sdkBin = Split-Path -Parent $_m17_hqDir                   # .../bin (SDK bin root)

$LOOKBACK_DAYS    = 3
$BREAK_GAP_SEC    = 120   # gap >= this => user turn or session boundary (break event)
$MAX_TOOL_GAP_SEC = 300   # ignore single gaps > this (LLM latency, not solo run time)

# Thresholds per family (seconds)
$THRESHOLDS = @{
    Claude = 360   # Sonnet 6 min
    Codex  = 420   # 7 min
    Gemini = 420   # gemini_pro 7 min
}

# ── Helper: compute longest solo run from a flat array of [ts, isBreak] events ────────────
function Get-LongestSoloRun {
    param([object[]]$Events)   # each: @{ ts=[datetime]; isBreak=[bool] }
    $maxSec = 0
    $runSec = 0
    for ($i = 1; $i -lt $Events.Count; $i++) {
        if ($Events[$i].isBreak) {
            $runSec = 0
        } else {
            $gap = ($Events[$i].ts - $Events[$i-1].ts).TotalSeconds
            if ($gap -lt $MAX_TOOL_GAP_SEC) { $runSec += $gap }
            else                             { $runSec = 0 }  # idle gap resets solo clock
            if ($runSec -gt $maxSec) { $maxSec = $runSec }
        }
    }
    return $maxSec
}

$cutoff = (Get-Date).AddDays(-$LOOKBACK_DAYS).ToUniversalTime()

# ── PHASE 1: DETECT -- CLAUDE ────────────────────────────────────────────────────────────
$claudeResult = [PSCustomObject]@{
    family       = 'Claude'
    storeFound   = $false
    sessions     = 0
    maxRunSec    = 0
    stallEvents  = 0
    status       = 'na'
    detail       = 'store not found'
}

try {
    $claudeRoot = Join-Path $env:USERPROFILE '.claude\projects'
    if (Test-Path $claudeRoot -PathType Container) {
        $claudeResult.storeFound = $true
        $sessionFiles = @(Get-ChildItem $claudeRoot -Recurse -File -Filter '*.jsonl' -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Name -match '^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\.jsonl$' -and
                $_.Directory.Parent.FullName -eq $claudeRoot -and
                $_.LastWriteTime.ToUniversalTime() -gt $cutoff
            })
        $claudeResult.sessions = $sessionFiles.Count

        foreach ($sf in $sessionFiles | Select-Object -First 20) {
            try {
                $lines = [IO.File]::ReadAllLines($sf.FullName, [Text.Encoding]::UTF8)

                # Stall event detection: stall-guard fires as stderr in hook_warn content
                $stallHits = ($lines | Where-Object {
                    $_ -match '\[stall-guard\]\s+BLOCKED|\[stall-guard\]\s+solo\s+loop|\[stall-guard\]\s+Gemini'
                }).Count
                $claudeResult.stallEvents += $stallHits

                # Build event list: assistant=tool-call, user=break, Agent=break
                $events = @($lines | ForEach-Object {
                    try {
                        $obj = $_ | ConvertFrom-Json -ErrorAction Stop
                        if (-not $obj.timestamp) { return }
                        $ts = [datetime]$obj.timestamp
                        if ($ts.ToUniversalTime() -lt $cutoff) { return }
                        $isBreak = $false
                        if ($obj.type -eq 'user') { $isBreak = $true }
                        elseif ($obj.type -eq 'assistant' -and $_ -match '"Agent"|"wkcodex"|"Agent\.cmd"') {
                            $isBreak = $true  # delegation = break
                        }
                        elseif ($obj.type -ne 'assistant') { return }
                        [PSCustomObject]@{ ts = $ts; isBreak = $isBreak }
                    } catch {}
                } | Where-Object { $_ -ne $null } | Sort-Object ts)

                $runSec = Get-LongestSoloRun -Events $events
                if ($runSec -gt $claudeResult.maxRunSec) { $claudeResult.maxRunSec = $runSec }
            } catch {}
        }
    }
} catch {
    $claudeResult.detail = "scan error: $_"
}

# ── PHASE 1: DETECT -- CODEX ─────────────────────────────────────────────────────────────
$codexResult = [PSCustomObject]@{
    family      = 'Codex'
    storeFound  = $false
    sessions    = 0
    maxRunSec   = 0
    stallEvents = 0
    status      = 'na'
    detail      = 'store not found'
}

try {
    $codexRoot = Join-Path $env:USERPROFILE '.codex\sessions'
    if (Test-Path $codexRoot -PathType Container) {
        $codexResult.storeFound = $true
        $codexFiles = @(Get-ChildItem $codexRoot -Recurse -File -Filter '*.jsonl' -ErrorAction SilentlyContinue |
            Where-Object { $_.LastWriteTime.ToUniversalTime() -gt $cutoff })
        $codexResult.sessions = $codexFiles.Count

        foreach ($cf in $codexFiles | Select-Object -First 20) {
            try {
                $lines = Get-Content $cf.FullName -Encoding UTF8 -ErrorAction Stop

                # Only count stall-guard BLOCK messages (not AGENTS.md embedded text)
                $stallHits = ($lines | Where-Object {
                    $_ -match '\[stall-guard\]\s+BLOCKED|\[stall-guard\]\s+solo\s+loop' -and
                    $_ -notmatch '"type"\s*:\s*"response_item"'  # exclude payloads that echo AGENTS.md
                }).Count
                $codexResult.stallEvents += $stallHits

                # Event list: user_message=break, function_call/output=tool
                $events = @($lines | ForEach-Object {
                    try {
                        $obj = $_ | ConvertFrom-Json -ErrorAction Stop
                        if (-not $obj.timestamp) { return }
                        $ts = [datetime]$obj.timestamp
                        if ($ts.ToUniversalTime() -lt $cutoff) { return }
                        $pt = if ($obj.payload) { $obj.payload.type } else { '' }
                        $isBreak = ($obj.type -eq 'event_msg' -and $pt -eq 'user_message')
                        $isTool  = ($obj.type -eq 'response_item' -and
                                    ($pt -eq 'function_call' -or $pt -eq 'function_call_output'))
                        if (-not $isBreak -and -not $isTool) { return }
                        [PSCustomObject]@{ ts = $ts; isBreak = $isBreak }
                    } catch {}
                } | Where-Object { $_ -ne $null } | Sort-Object ts)

                $runSec = Get-LongestSoloRun -Events $events
                if ($runSec -gt $codexResult.maxRunSec) { $codexResult.maxRunSec = $runSec }
            } catch {}
        }
    }
} catch {
    $codexResult.detail = "scan error: $_"
}

# ── PHASE 1: DETECT -- GEMINI ────────────────────────────────────────────────────────────
$geminiResult = [PSCustomObject]@{
    family      = 'Gemini'
    storeFound  = $false
    sessions    = 0
    maxRunSec   = 0
    stallEvents = 0
    status      = 'na'
    detail      = 'store not found'
}

try {
    $geminiUsage = Join-Path $env:USERPROFILE '.gemini\wkgemini_usage.jsonl'
    if (Test-Path $geminiUsage -PathType Leaf) {
        $geminiResult.storeFound = $true

        # Parse timestamps from usage log (per-call API entries)
        $records = @(Get-Content $geminiUsage -Encoding UTF8 -ErrorAction Stop | ForEach-Object {
            try {
                $obj = $_ | ConvertFrom-Json -ErrorAction Stop
                if ($obj.ts -and -not $obj.quota_exhausted -and $obj.session_id) {
                    $ts = [datetime]$obj.ts
                    if ($ts.ToUniversalTime() -gt $cutoff) {
                        [PSCustomObject]@{ ts = $ts; isBreak = $false }
                    }
                }
            } catch {}
        } | Where-Object { $_ -ne $null } | Sort-Object ts)

        # Inject break events at gaps > BREAK_GAP_SEC
        $events = [System.Collections.Generic.List[object]]::new()
        for ($i = 0; $i -lt $records.Count; $i++) {
            if ($i -gt 0 -and ($records[$i].ts - $records[$i-1].ts).TotalSeconds -ge $BREAK_GAP_SEC) {
                $events.Add([PSCustomObject]@{ ts = $records[$i].ts; isBreak = $true })
            }
            $events.Add($records[$i])
        }

        # Unique session_ids = approximate session count
        $uniqueSessions = @($records | Select-Object -ExpandProperty ts | ForEach-Object {
            [Math]::Floor(($_ - [datetime]'2026-01-01').TotalSeconds / 3600)  # hour buckets
        } | Sort-Object -Unique)
        $geminiResult.sessions = $uniqueSessions.Count

        # No stall-guard file for Gemini -- stall events = 0 by design (the R4 gap)
        $geminiResult.stallEvents = 0
        $geminiResult.maxRunSec = Get-LongestSoloRun -Events @($events)
    }
} catch {
    $geminiResult.detail = "scan error: $_"
}

# ── PHASE 2: SAFE (compute status + emit one line per family) ─────────────────────────────
function Format-FamilyResult {
    param($result)
    $family    = $result.family
    $threshold = $THRESHOLDS[$family]

    if (-not $result.storeFound) {
        $result.status = 'na'
        $result.detail = "store not locatable (${family}: no session data found)"
        return
    }

    $maxMin = [Math]::Round($result.maxRunSec / 60, 1)
    $thrMin = [Math]::Round($threshold / 60, 0)

    if ($result.maxRunSec -le $threshold) {
        # Runs within threshold -- stall brake appears effective (or sessions too short to test)
        $result.status = 'ok'
        $stallNote = if ($result.stallEvents -gt 0) { ", $($result.stallEvents) stall events seen" } else { ", 0 stall events (runs within limit)" }
        $result.detail = "braked -- max solo run ${maxMin}min <= ${thrMin}min threshold, $($result.sessions) sessions/${LOOKBACK_DAYS}d${stallNote}"
    } else {
        # Solo run EXCEEDED threshold
        if ($result.stallEvents -eq 0) {
            # No stall events AND exceeded threshold = brake DISCONNECTED
            $result.status = 'warn'
            $result.detail = "${maxMin}min unbraked solo run > ${thrMin}min threshold, 0 stall events -- brake DISCONNECTED ($($result.sessions) sessions/${LOOKBACK_DAYS}d)"
        } else {
            # Stall events present but run still long -- partial firing, flag as warn
            $result.status = 'ok'
            $result.detail = "braked ($($result.stallEvents) stall events) but max solo run ${maxMin}min -- check stall threshold alignment ($($result.sessions) sessions/${LOOKBACK_DAYS}d)"
        }
    }
}

foreach ($r in @($claudeResult, $codexResult, $geminiResult)) {
    Format-FamilyResult -result $r
    $label = "stall:$($r.family)"
    Add-Check $label $r.status $r.detail
    Emit $r.status $label $r.detail
}

# ── PHASE 3: SOLVE -- emit per-family recommendation when DISCONNECTED ────────────────────
$anyDisconnected = @($claudeResult, $codexResult, $geminiResult) | Where-Object { $_.status -eq 'warn' }
if ($anyDisconnected.Count -gt 0) {
    $names = ($anyDisconnected | Select-Object -ExpandProperty family) -join ', '
    $solveMsg = "wire per-family stall session parsing (R4): $names -- each family needs a Get-<Family>ActiveSession adapter so the solo clock advances and the brake can engage"
    Add-Check 'stall:solve' 'warn' $solveMsg
    Emit '!' 'stall:solve' $solveMsg
}

# ── PHASE 4: PREVENT -- self-documenting (this check runs every session) ──────────────────
# No additional guard needed here; the doctor itself is the recurring prevention mechanism.
