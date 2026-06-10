# wkdoctor check: broken skill links -- knowledge-tree integrity
# Detects dead outbound skill references so prior experience is never orphaned.
# A reference is BROKEN when a skill body mentions 'wkappbot skill read <id>' and
# that <id> does not exist in the authoritative installed-skill set.
#
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT:  build the SET of all existing skill IDs once (wkappbot skill list --all).
#               For each skill body, extract outbound 'wkappbot skill read <id>' refs.
#               A ref is broken if its target is NOT in the existing-id set.
#   (2) SAFE:   report only -- read-only, never modify or delete any skill.
#   (3) SOLVE:  on any broken link, emit the offending (source -> missing-target) pairs
#               and suggest: re-point the ref to the renamed skill, create the missing skill,
#               or remove the dead ref via 'wkappbot skill edit <src>'.
#   (4) PREVENT: this check runs every wkdoctor pass; knowledge-tree regressions surface
#               immediately after any skill rename/delete.
#
# FAIL-OPEN: skill list unavailable, parse failure, or scan error
#            => emit 'n/a (fail-open)', never crash the doctor.
# SPEED:     one 'wkappbot skill list --all' call + one JSON read pass over local skill files.
#            No per-skill subprocess. Typical < 2 s on 500+ skills.

# ── Derive self-sufficient SDK bin path ───────────────────────────────────────────────────
$_m19_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path  # .../wkappbot.hq/doctor
$_m19_hqDir  = Split-Path -Parent $_m19_myDir                   # .../wkappbot.hq
$_m19_sdkBin = Split-Path -Parent $_m19_hqDir                   # .../bin (SDK bin root)

$CHECK_NAME = 'skill-links:integrity'
$MIN_REF_LEN = 6   # minimum chars for a token to be treated as a skill ID

# ── PHASE 1 DETECT -- STEP A: build authoritative existing-skill-id set ──────────────────
$existingIds = $null
try {
    # wkappbot skill list --all is the authoritative installed-skill catalog.
    # Parse lines of the form "... (skill-id) vN.N ..."
    $listOutput = & wkappbot skill list --all 2>$null
    if ($LASTEXITCODE -ne 0 -or -not $listOutput) {
        throw "skill list --all returned exit=$LASTEXITCODE or empty output"
    }
    $existingIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    foreach ($line in $listOutput) {
        # Match pattern: "(skill-id) v" where skill-id is lowercase alphanumeric+hyphen
        if ($line -match '\(([a-z][a-z0-9-]+)\)\s+v\d') {
            [void]$existingIds.Add($Matches[1])
        }
    }
} catch {
    $detail = "n/a (fail-open) -- skill list unavailable: $_"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
    return
}

if ($existingIds.Count -eq 0) {
    $detail = "n/a (fail-open) -- no skill IDs parsed from skill list --all"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
    return
}

# ── PHASE 1 DETECT -- STEP B: scan local skill files for outbound refs ───────────────────
# Scan both the HQ installed skills dir and any project skills dir co-located with the doctor.
$_m19_hqSkillsDir   = Join-Path $_m19_hqDir 'skills'
$_m19_projSkillsDir = Join-Path $_m19_sdkBin '..\..\skills'   # repo-root/skills if present

$scanDirs = @()
if (Test-Path $_m19_hqSkillsDir   -PathType Container) { $scanDirs += $_m19_hqSkillsDir }
if (Test-Path $_m19_projSkillsDir -PathType Container) { $scanDirs += (Resolve-Path $_m19_projSkillsDir -ErrorAction SilentlyContinue) }

# Also check USERPROFILE-based HQ location (deployed skill store)
$_m19_userHqSkills = Join-Path $env:USERPROFILE '.wkappbot\bin\bin\wkappbot.hq\skills'
if (Test-Path $_m19_userHqSkills -PathType Container) { $scanDirs += $_m19_userHqSkills }

# Deduplicate scan dirs
$scanDirs = $scanDirs | Sort-Object -Unique

$brokenLinks    = [System.Collections.Generic.List[string]]::new()
$scannedCount   = 0
$errorCount     = 0

# Regex for 'wkappbot skill read <id>' -- capture the id token
$readPattern = [regex]'wkappbot\s+skill\s+read\s+([a-z][a-z0-9-]+)'

foreach ($dir in $scanDirs) {
    $skillFiles = Get-ChildItem -Path $dir -Recurse -Filter '*.skill.json' -File -ErrorAction SilentlyContinue
    foreach ($sf in $skillFiles) {
        $scannedCount++
        try {
            $raw = [System.IO.File]::ReadAllText($sf.FullName, [System.Text.Encoding]::UTF8)
            $obj = $raw | ConvertFrom-Json -ErrorAction Stop

            $srcId = if ($obj.id) { [string]$obj.id } else { $sf.BaseName -replace '\.skill$', '' }

            # Collect all step + requirement text into one string for scanning
            $bodyParts = @()
            if ($obj.steps        -and $obj.steps        -is [array]) { $bodyParts += $obj.steps        | ForEach-Object { [string]$_ } }
            if ($obj.requirements -and $obj.requirements -is [array]) { $bodyParts += $obj.requirements | ForEach-Object { [string]$_ } }
            $body = $bodyParts -join ' '

            # Extract all 'wkappbot skill read <id>' references
            $matches2 = $readPattern.Matches($body)
            foreach ($m in $matches2) {
                $refId = $m.Groups[1].Value.Trim()
                if ($refId.Length -lt $MIN_REF_LEN) { continue }
                if ($refId -eq $srcId)              { continue }   # skip self-links
                if (-not $existingIds.Contains($refId)) {
                    $brokenLinks.Add("$srcId -> $refId")
                }
            }
        } catch {
            $errorCount++
        }
    }
}

# Deduplicate (same source can reference same dead target in multiple steps)
$uniqueBroken = $brokenLinks | Sort-Object -Unique

# ── PHASE 2 SAFE + PHASE 3 SOLVE -- build report ─────────────────────────────────────────
$scanSummary = "scanned=$scannedCount files  known-ids=$($existingIds.Count)"
if ($errorCount -gt 0) { $scanSummary += "  parse-errors=$errorCount" }

if ($uniqueBroken.Count -eq 0) {
    $detail = "0 broken links -- all skill refs resolve  [$scanSummary]"
    Add-Check $CHECK_NAME 'ok' $detail
    Emit 'ok' $CHECK_NAME $detail
} else {
    # Show top offenders inline (up to 5); full list in detail string
    $topN      = [math]::Min(5, $uniqueBroken.Count)
    $topLines  = ($uniqueBroken | Select-Object -First $topN) -join '; '
    $solveHint = "fix: wkappbot skill edit <src> to re-point/remove the dead ref, or create the missing skill"
    $detail    = "$($uniqueBroken.Count) broken link(s)  top: $topLines  [$scanSummary]  $solveHint"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
}

# ── PHASE 4 PREVENT -- self-documenting ──────────────────────────────────────────────────
# No additional guard needed; this check runs on every wkdoctor pass,
# surfacing regressions immediately after any skill rename or deletion.
