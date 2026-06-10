# wkdoctor check: permission-settings -- detect repeated permission-decision friction
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT: scan recent sessions for harness WARN/BLOCK patterns that recur 2+
#               times across sessions, indicating a tool-pattern the settings.json
#               does NOT auto-handle. Also audit settings.json allow/deny coverage.
#               REJECTION = SAME SIGNAL as ACCEPTANCE (user 2026-06-10 design).
#   (2) SAFE:   report only -- never modify settings.json or harness files.
#   (3) SOLVE:  emit the one-line allow/deny entry that would eliminate the friction.
#   (4) PREVENT: this check runs every session so gaps become visible immediately.
# FAIL-OPEN: any parse error / missing file => warn or na, never crash the doctor.
# Data sources:
#   - %USERPROFILE%\.claude\settings.json  (permissions.allow / permissions.deny)
#   - Session transcript attachment entries (hook_success/hook_warn with stderr WARN
#     or BLOCK messages -- these represent repeated friction patterns)

# Derive self-sufficient SDK bin path (resilient against $binDir clobber by module 06)
$_m16_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path  # .../wkappbot.hq/doctor
$_m16_hqDir  = Split-Path -Parent $_m16_myDir                   # .../wkappbot.hq
$_m16_sdkBin = Split-Path -Parent $_m16_hqDir                   # .../bin (SDK bin root)

$LOOKBACK_DAYS = 3
$REPEAT_THRESHOLD = 2  # pattern seen in 2+ sessions = recurring friction

# ── PHASE 1: DETECT ──────────────────────────────────────────────────────────

# 1a. Load current settings.json
$claudeSettingsPath = Join-Path $env:USERPROFILE '.claude\settings.json'
$allowList = @()
$denyList  = @()

if (Test-Path $claudeSettingsPath -PathType Leaf) {
    try {
        $settingsRaw = [IO.File]::ReadAllText($claudeSettingsPath, [Text.Encoding]::UTF8)
        $settings    = ConvertFrom-Json -InputObject $settingsRaw -ErrorAction Stop
        if ($settings.permissions -and $settings.permissions.allow) {
            $allowList = @($settings.permissions.allow)
        }
        if ($settings.permissions -and $settings.permissions.deny) {
            $denyList = @($settings.permissions.deny)
        }
    } catch {
        Add-Check 'settings:permissions' 'warn' "cannot parse settings.json: $_"
        Emit '!' 'settings:permissions' "settings.json parse failed: $($_)"
        return
    }
} else {
    Add-Check 'settings:permissions' 'warn' "settings.json not found at $claudeSettingsPath"
    Emit '!' 'settings:permissions' "settings.json missing"
    return
}

# 1b. Scan recent session attachments for WARN/BLOCK messages from harness hooks
$claudeProjectsRoot = Join-Path $env:USERPROFILE '.claude\projects'
$cutoff = (Get-Date).AddDays(-$LOOKBACK_DAYS).ToUniversalTime()

# Pattern buckets: key = normalized pattern, value = count of sessions it appeared in
$patternSessionCounts = @{}   # pattern -> Set of session IDs
$patternExamples      = @{}   # pattern -> example message

function Normalize-WarnPattern {
    param([string]$msg)
    # Normalize: strip file paths, line numbers, timestamps to get a stable key
    $norm = $msg.Trim()
    $norm = $norm -replace 'D:[\\\/][^\s]+', '<path>'
    $norm = $norm -replace 'C:[\\\/][^\s]+', '<path>'
    $norm = $norm -replace '\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}', '<ts>'
    $norm = $norm -replace '\b\d{5,}\b', '<num>'
    # Take first 80 chars as canonical key
    if ($norm.Length -gt 80) { $norm = $norm.Substring(0, 80) + '...' }
    return $norm
}

if (Test-Path $claudeProjectsRoot -PathType Container) {
    try {
        $sessionFiles = Get-ChildItem $claudeProjectsRoot -Recurse -File -Filter '*.jsonl' -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Name -match '^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\.jsonl$' -and
                $_.Directory.Parent.FullName -eq $claudeProjectsRoot -and
                $_.LastWriteTime.ToUniversalTime() -gt $cutoff
            }

        foreach ($sf in @($sessionFiles)) {
            $sessionId = $sf.Name -replace '\.jsonl$', ''
            $patternsThisSession = @{}  # dedup per session

            try {
                $lines = [IO.File]::ReadAllLines($sf.FullName, [Text.Encoding]::UTF8)
                foreach ($line in $lines) {
                    # Only attachment entries with hook output
                    if ($line -notmatch '"type":"attachment"') { continue }
                    if ($line -notmatch '"stderr"') { continue }

                    try {
                        $obj = $line | ConvertFrom-Json -ErrorAction Stop
                        if (-not $obj.attachment) { continue }
                        $attach = $obj.attachment
                        $stderr = if ($attach.stderr) { [string]$attach.stderr } else { '' }
                        if (-not $stderr) { continue }

                        # Extract WARN and BLOCK lines from harness stderr output
                        $stderrLines = $stderr -split '\r?\n'
                        foreach ($stderrLine in $stderrLines) {
                            $sl = $stderrLine.Trim()
                            if ($sl -match '^\[.+\]\s*(WARNING|WARN|BLOCKED|ERROR):') {
                                $norm = Normalize-WarnPattern $sl
                                if (-not $patternsThisSession.ContainsKey($norm)) {
                                    $patternsThisSession[$norm] = $sl
                                }
                            }
                        }
                    } catch {}
                }
            } catch {}

            # Add patterns from this session to the global count
            foreach ($kv in $patternsThisSession.GetEnumerator()) {
                if (-not $patternSessionCounts.ContainsKey($kv.Key)) {
                    $patternSessionCounts[$kv.Key] = [System.Collections.Generic.HashSet[string]]::new()
                    $patternExamples[$kv.Key] = $kv.Value
                }
                $null = $patternSessionCounts[$kv.Key].Add($sessionId)
            }
        }
    } catch {
        # Non-fatal: continue with settings analysis only
    }
}

# 1c. Check settings.json coverage for standard harness-handled tool classes
$hasBashWildcard        = $allowList -contains 'Bash(*)'
$hasPowerShellWildcard  = $allowList -contains 'PowerShell(*)'
$hasReadWildcard        = $allowList -contains 'Read(*)'
$hasWriteWildcard       = $allowList -contains 'Write(*)'
$hasEditWildcard        = $allowList -contains 'Edit(*)'
$hasGlobWildcard        = $allowList -contains 'Glob(*)'
$hasGrepWildcard        = $allowList -contains 'Grep(*)'
$missingCore = @()
if (-not $hasBashWildcard)        { $missingCore += 'Bash(*)' }
if (-not $hasPowerShellWildcard)  { $missingCore += 'PowerShell(*)' }
if (-not $hasReadWildcard)        { $missingCore += 'Read(*)' }

# ── PHASE 2: SAFE (report only) ──────────────────────────────────────────────

# Identify recurring patterns (seen in 2+ sessions)
$recurringPatterns = @($patternSessionCounts.GetEnumerator() |
    Where-Object { $_.Value.Count -ge $REPEAT_THRESHOLD } |
    Sort-Object { $_.Value.Count } -Descending)

$totalSessions = if (Test-Path $claudeProjectsRoot -PathType Container) {
    try {
        @(Get-ChildItem $claudeProjectsRoot -Recurse -File -Filter '*.jsonl' -ErrorAction SilentlyContinue |
            Where-Object {
                $_.Name -match '^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\.jsonl$' -and
                $_.Directory.Parent.FullName -eq $claudeProjectsRoot -and
                $_.LastWriteTime.ToUniversalTime() -gt $cutoff
            }).Count
    } catch { 0 }
} else { 0 }

# Build the check status
$checkStatus = 'ok'
$faultLines  = @()

if ($missingCore.Count -gt 0) {
    $checkStatus = 'warn'
    $faultLines += "missing core allow: $($missingCore -join ', ')"
}

if ($recurringPatterns.Count -gt 0) {
    $checkStatus = 'warn'
    $top = $recurringPatterns | Select-Object -First 3
    foreach ($p in $top) {
        $example = $patternExamples[$p.Key]
        # truncate example for display
        $shortEx = if ($example.Length -gt 70) { $example.Substring(0, 70) + '...' } else { $example }
        $faultLines += "$($p.Value.Count) sessions: $shortEx"
    }
}

$allowCount = $allowList.Count
$denyCount  = $denyList.Count

if ($faultLines.Count -eq 0) {
    $detail = "${totalSessions} sessions/${LOOKBACK_DAYS}d scanned, 0 repeated-friction patterns, allow=${allowCount} deny=${denyCount}"
    Add-Check 'settings:permissions' 'ok' $detail
    Emit 'ok' 'settings:permissions' $detail
} else {
    $K = $recurringPatterns.Count
    $detail = "$K repeated-decision pattern(s) across ${totalSessions} sessions/${LOOKBACK_DAYS}d -- allow=${allowCount} deny=${denyCount}"
    Add-Check 'settings:permissions' $checkStatus $detail
    Emit $checkStatus 'settings:permissions' $detail

    # ── PHASE 3: SOLVE (one-line heal per top pattern) ────────────────────────
    foreach ($fault in $faultLines) {
        $healNote = "heal: add to settings.json allow/deny to eliminate prompt -- $fault"
        Add-Check 'settings:permissions:heal' 'warn' $healNote
        Emit '!' 'settings:permissions:heal' $healNote
    }

    if ($missingCore.Count -gt 0) {
        $healCmd = "add to permissions.allow: $($missingCore -join ', ')"
        Add-Check 'settings:permissions:core-allow' 'warn' $healCmd
        Emit '!' 'settings:permissions:core-allow' $healCmd
    }
}

# ── PHASE 4: PREVENT (self-documenting) ──────────────────────────────────────
# This check runs every session -- permission friction cannot accumulate silently.
