# wkdoctor check: binary-freshness -- detect and auto-update stale launcher/core in SDK bin
# DOCTOR 4-PHASE FLOW: (1) DETECT stale binary, (2) SAFE keep-and-warn, (3) SOLVE copy/build,
# (4) PREVENT emit guidance so it cannot drift again.
# FAIL-OPEN: any error => warn/na, never crash the doctor.

function Get-FileVer {
    param([string]$Path)
    try {
        if (-not (Test-Path $Path -PathType Leaf)) { return $null }
        $vi = (Get-Item $Path -ErrorAction Stop).VersionInfo
        return [version]"$($vi.FileMajorPart).$($vi.FileMinorPart).$($vi.FileBuildPart)"
    } catch { return $null }
}

function Copy-Binary {
    param([string]$Src, [string]$Dst)
    try {
        Copy-Item -Path $Src -Destination $Dst -Force -ErrorAction Stop
        return $true
    } catch { return $false }
}

# Compute the true SDK bin dir: this plugin lives at <sdkBin>/wkappbot.hq/doctor/14-...ps1
# $binDir may have been overwritten by an earlier module (06-defender-exclusions), so
# derive the SDK bin from this file's own path to be resilient.
$_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path           # .../wkappbot.hq/doctor
$_hqDir  = Split-Path -Parent $_myDir                                # .../wkappbot.hq
$_sdkBin = Split-Path -Parent $_hqDir                                # .../bin  (SDK bin root)
$_sdkRepoRoot = Split-Path -Parent $_sdkBin                          # repo root

$sdkBin     = $_sdkBin
$coreDevBin = 'D:\GitHub\WKAppBot\bin'                               # private dev repo bin

# -- PHASE 1: DETECT --------------------------------------------------
# Compare SDK bin versions against the freshest available source.

# wkappbot.exe: compare SDK bin vs launcher publish output (rebuild freshness)
$launcherPublish = Join-Path $_sdkRepoRoot `
    'csharp\src\WKAppBot.Launcher\bin\Release\net8.0-windows\win-x64\publish\wkappbot.exe'

$sdkLauncherVer = Get-FileVer (Join-Path $sdkBin 'wkappbot.exe')
$pubLauncherVer = Get-FileVer $launcherPublish
$devLauncherVer = Get-FileVer (Join-Path $coreDevBin 'wkappbot.exe')

# wkappbot-core.exe: compare SDK bin vs private dev repo bin (only source of latest core)
$sdkCoreVer = Get-FileVer (Join-Path $sdkBin 'wkappbot-core.exe')
$devCoreVer = Get-FileVer (Join-Path $coreDevBin 'wkappbot-core.exe')

# -- PHASE 2: SAFE -------------------------------------------------
# Never delete the working binary. Stale = warn, not fail. Missing = fail.

# --- wkappbot-core.exe freshness ---
if ($null -eq $sdkCoreVer) {
    Add-Check 'binary-freshness:core' 'fail' 'wkappbot-core.exe missing from SDK bin'
    Emit 'fail' 'binary-freshness:core' 'missing -- copy from D:\GitHub\WKAppBot\bin'
} elseif ($null -ne $devCoreVer -and $devCoreVer -gt $sdkCoreVer) {
    # PHASE 3: SOLVE -- core is private, copy from dev repo
    $copied = Copy-Binary (Join-Path $coreDevBin 'wkappbot-core.exe') `
                          (Join-Path $sdkBin      'wkappbot-core.exe')
    if ($copied) {
        # Also copy .new.exe and .bak per DEPLOY-ALL-THREE rule (wkappbot-build-verify-workflow step 14)
        Copy-Binary (Join-Path $coreDevBin 'wkappbot-core.exe') (Join-Path $sdkBin 'wkappbot-core.new.exe') | Out-Null
        Copy-Binary (Join-Path $coreDevBin 'wkappbot-core.exe') (Join-Path $sdkBin 'wkappbot-core.exe.bak') | Out-Null
        $newVer = Get-FileVer (Join-Path $sdkBin 'wkappbot-core.exe')
        Add-Check 'binary-freshness:core' 'ok' "self-healed: v$sdkCoreVer -> v$newVer (copied from dev bin)"
        Emit 'ok' 'binary-freshness:core' "self-healed: v$sdkCoreVer -> v$newVer"
    } else {
        Add-Check 'binary-freshness:core' 'warn' "stale v$sdkCoreVer (latest v$devCoreVer) -- copy failed; copy manually from $coreDevBin"
        Emit '!' 'binary-freshness:core' "stale v$sdkCoreVer < v$devCoreVer -- copy manually from $coreDevBin"
    }
} elseif ($null -eq $devCoreVer) {
    # Dev repo absent -- warn only, do not touch working binary
    Add-Check 'binary-freshness:core' 'warn' "v$sdkCoreVer (dev bin absent -- cannot compare)"
    Emit '!' 'binary-freshness:core' "v$sdkCoreVer -- dev bin absent; freshness unknown"
} else {
    Add-Check 'binary-freshness:core' 'ok' "v$sdkCoreVer (current)"
    Emit 'ok' 'binary-freshness:core' "v$sdkCoreVer"
}

# --- wkappbot.exe (launcher) freshness ---
# Freshest launcher source: publish output (if newer than SDK bin), else dev bin, else public GitHub.
# This supports both dev machines (with local bin) and public users (with only public SDK).
$bestLauncherSrc = $null
$bestLauncherVer = $null
if ($null -ne $pubLauncherVer) {
    $bestLauncherSrc = $launcherPublish
    $bestLauncherVer = $pubLauncherVer
}
# If dev bin has a newer launcher than both SDK bin AND publish output, prefer it.
if ($null -ne $devLauncherVer) {
    if ($null -eq $bestLauncherVer -or $devLauncherVer -gt $bestLauncherVer) {
        $bestLauncherSrc = Join-Path $coreDevBin 'wkappbot.exe'
        $bestLauncherVer = $devLauncherVer
    }
}

if ($null -eq $sdkLauncherVer) {
    Add-Check 'binary-freshness:launcher' 'fail' 'wkappbot.exe missing -- attempting heal...'
    Emit 'fail' 'binary-freshness:launcher' 'missing -- attempting heal...'

    # PHASE 3: SOLVE -- missing launcher: try all sources
    $healed = $false
    $healSrc = $null

    # Try best available local source first (dev bin or publish output)
    if ($null -ne $bestLauncherSrc -and (Test-Path $bestLauncherSrc)) {
        $healed = Copy-Binary $bestLauncherSrc (Join-Path $sdkBin 'wkappbot.exe')
        if ($healed) { $healSrc = $bestLauncherSrc }
    }

    # If local source unavailable/failed, try public GitHub (public user fallback)
    if (-not $healed) {
        $ghDownloadSuccess = Try-InstallBinaryFromGitHub -ExePath (Join-Path $sdkBin 'wkappbot.exe')
        if ($ghDownloadSuccess) {
            $healed = $true
            $healSrc = 'kiexpert/wkappbot-sdk (public release)'
        }
    }

    if ($healed) {
        $newVer = Get-FileVer (Join-Path $sdkBin 'wkappbot.exe')
        Add-Check 'binary-freshness:launcher' 'ok' "self-healed: v$newVer (from $healSrc)"
        Emit 'ok' 'binary-freshness:launcher' "self-healed: v$newVer"
    } else {
        Add-Check 'binary-freshness:launcher' 'fail' 'missing and all heal sources unavailable'
        Emit 'fail' 'binary-freshness:launcher' 'missing -- copy from local dev bin or download from public SDK release'
    }
} elseif ($null -ne $bestLauncherVer -and $bestLauncherVer -gt $sdkLauncherVer) {
    # PHASE 3: SOLVE -- stale launcher: copy fresher from best available source
    $copied = Copy-Binary $bestLauncherSrc (Join-Path $sdkBin 'wkappbot.exe')
    if ($copied) {
        $newVer = Get-FileVer (Join-Path $sdkBin 'wkappbot.exe')
        Add-Check 'binary-freshness:launcher' 'ok' "self-healed: v$sdkLauncherVer -> v$newVer (copied from $(Split-Path $bestLauncherSrc -Parent))"
        Emit 'ok' 'binary-freshness:launcher' "self-healed: v$sdkLauncherVer -> v$newVer"
    } else {
        Add-Check 'binary-freshness:launcher' 'warn' "stale v$sdkLauncherVer (best v$bestLauncherVer) -- copy failed; attempting GitHub fallback..."
        Emit '!' 'binary-freshness:launcher' "stale v$sdkLauncherVer < v$bestLauncherVer -- attempting GitHub fallback..."

        # Fallback to GitHub when local copy fails (public user path)
        $ghDownloadSuccess = Try-InstallBinaryFromGitHub -ExePath (Join-Path $sdkBin 'wkappbot.exe')
        if ($ghDownloadSuccess) {
            $newVer = Get-FileVer (Join-Path $sdkBin 'wkappbot.exe')
            Add-Check 'binary-freshness:launcher' 'ok' "self-healed: v$sdkLauncherVer -> v$newVer (from public SDK release)"
            Emit 'ok' 'binary-freshness:launcher' "self-healed: v$sdkLauncherVer -> v$newVer (GitHub fallback)"
        } else {
            Add-Check 'binary-freshness:launcher' 'warn' "stale v$sdkLauncherVer (best v$bestLauncherVer) -- copy and GitHub fallback both failed; run: wkdoctor -Build"
            Emit '!' 'binary-freshness:launcher' "stale v$sdkLauncherVer < v$bestLauncherVer -- GitHub fallback failed; run wkdoctor -Build"
        }
    }
} elseif ($null -eq $bestLauncherVer) {
    # No comparison source available (no dev bin, no publish output) -- launcher exists, freshness unknown
    Add-Check 'binary-freshness:launcher' 'warn' "v$sdkLauncherVer (no local source to compare -- freshness unknown)"
    Emit '!' 'binary-freshness:launcher' "v$sdkLauncherVer -- cannot verify freshness (dev bin absent)"
} else {
    Add-Check 'binary-freshness:launcher' 'ok' "v$sdkLauncherVer (current)"
    Emit 'ok' 'binary-freshness:launcher' "v$sdkLauncherVer"
}

# -- PHASE 4: PREVENT --------------------------------------------------
# This check runs every wkdoctor session -- version drift cannot go undetected.
Add-Check 'binary-freshness:guard' 'ok' 'check runs every session -- version drift auto-detected'
Emit 'ok' 'binary-freshness:guard' 'runs every session'
