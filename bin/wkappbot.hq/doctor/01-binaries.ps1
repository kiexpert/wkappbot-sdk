# wkdoctor check: binaries -- wkappbot.exe, wkappbot-core.exe, a11y.exe
# Heal: (1) attempt GitHub binary download if gh is available; (2) fallback to auto-build from source

$wkexe  = Join-Path $binDir 'wkappbot.exe'
$wkcore = Join-Path $binDir 'wkappbot-core.exe'
$a11y   = Join-Path $binDir 'a11y.exe'

# -- Helper: Try-InstallBinaryFromGitHub --------------------------------------
# Downloads the latest released binary from kiexpert/wkappbot-sdk via gh CLI and installs
# it at $ExePath. Returns $true on success, $false otherwise (fail-open -- never throws).
#
# The public release.yml publishes TWO asset shapes for each tag:
#   (1) <ExeName>.exe          -- the launcher exe uploaded as a direct asset
#   (2) wkappbot-<version>.zip -- a bundle containing wkappbot.exe + wkappbot-core.exe
# This helper tries the direct exe asset first; if absent, it falls back to the zip bundle,
# expands it, and extracts the matching exe. Either path validates VersionInfo before install.
function Try-InstallBinaryFromGitHub {
    param(
        [string]$ExePath  # Target path where binary should be installed (e.g. .../wkappbot.exe)
    )

    $tmpDir = Join-Path $env:TEMP "wkdoc-gh-bin-$([guid]::NewGuid())"
    try {
        # Check if gh CLI is available
        if (-not (Get-Command 'gh' -ErrorAction SilentlyContinue)) { return $false }

        $exeName = Split-Path -Leaf $ExePath           # e.g. 'wkappbot.exe'
        $null = New-Item -ItemType Directory -Path $tmpDir -Force -ErrorAction SilentlyContinue

        # -- Attempt 1: direct exe asset (e.g. 'wkappbot.exe') ------------------
        & gh release download -R kiexpert/wkappbot-sdk -p $exeName -D $tmpDir 2>&1 | Out-Null
        $hit = Get-ChildItem $tmpDir -Filter $exeName -ErrorAction SilentlyContinue | Select-Object -First 1

        # -- Attempt 2: zip bundle 'wkappbot-*.zip' -> expand -> find exe -------
        if (-not $hit) {
            & gh release download -R kiexpert/wkappbot-sdk -p 'wkappbot-*.zip' -D $tmpDir 2>&1 | Out-Null
            $zip = Get-ChildItem $tmpDir -Filter 'wkappbot-*.zip' -ErrorAction SilentlyContinue | Select-Object -First 1
            if ($zip) {
                $expandDir = Join-Path $tmpDir 'expanded'
                Expand-Archive -Path $zip.FullName -DestinationPath $expandDir -Force -ErrorAction SilentlyContinue
                $hit = Get-ChildItem $expandDir -Recurse -Filter $exeName -ErrorAction SilentlyContinue | Select-Object -First 1
            }
        }

        # -- Validate + install ------------------------------------------------
        if ($hit -and $hit.VersionInfo.FileVersion) {
            Copy-Item -Path $hit.FullName -Destination $ExePath -Force -ErrorAction Stop
            return $true
        }
        return $false
    } catch {
        return $false
    } finally {
        try { Remove-Item $tmpDir -Recurse -Force -ErrorAction SilentlyContinue } catch {}
    }
}

# Check 1: wkappbot.exe
if (Test-Path $wkexe -PathType Leaf) {
    $vi  = (Get-Item $wkexe).VersionInfo
    $ver = "$($vi.FileMajorPart).$($vi.FileMinorPart).$($vi.FileBuildPart)"
    Add-Check 'wkappbot.exe' 'ok' "v$ver"
    Emit 'ok' 'wkappbot.exe' "v$ver"
} else {
    Add-Check 'wkappbot.exe' 'fail' 'not found'
    Emit 'fail' 'wkappbot.exe' 'not found -- attempting GitHub download...'

    # Step 1: Try GitHub binary download (fail-open if no public binaries published)
    $ghSuccess = Try-InstallBinaryFromGitHub -ExePath $wkexe

    if ($ghSuccess) {
        $vi3 = (Get-Item $wkexe).VersionInfo
        $v3  = "$($vi3.FileMajorPart).$($vi3.FileMinorPart).$($vi3.FileBuildPart)"
        Add-Check 'wkappbot.exe' 'ok' "self-healed: downloaded v$v3 from kiexpert/wkappbot-sdk"
        Emit 'ok' 'wkappbot.exe' "self-healed: downloaded v$v3 from kiexpert/wkappbot-sdk"
    } else {
        # Step 2: Fallback to build from source
        Emit 'warn' 'wkappbot.exe' 'GitHub download unavailable -- building from source...'
        $proj   = Join-Path $repoRoot 'csharp\src\WKAppBot.Launcher\WKAppBot.Launcher.csproj'
        $dotnet = 'C:\Program Files\dotnet\dotnet.exe'
        if ((Test-Path $proj) -and (Test-Path $dotnet)) {
            $null = & $dotnet publish $proj -c Release --verbosity minimal 2>&1
            if (Test-Path $wkexe -PathType Leaf) {
                $vi2 = (Get-Item $wkexe).VersionInfo
                $v2  = "$($vi2.FileMajorPart).$($vi2.FileMinorPart).$($vi2.FileBuildPart)"
                Add-Check 'wkappbot.exe' 'ok' "self-healed: built v$v2"
                Emit 'ok' 'wkappbot.exe' "self-healed: built v$v2"
            } else {
                Add-Check 'wkappbot.exe' 'fail' 'build failed -- run: setup.ps1'
                Emit 'fail' 'wkappbot.exe' 'build failed'
            }
        } else {
            Add-Check 'wkappbot.exe' 'fail' 'csproj not found -- run: setup.ps1'
            Emit 'fail' 'wkappbot.exe' 'run: setup.ps1'
        }
    }
}

# Check 2: wkappbot-core.exe (must come from private WKAppBot repo, not public SDK)
if (Test-Path $wkcore -PathType Leaf) {
    $cv  = (Get-Item $wkcore).VersionInfo
    $ver = "$($cv.FileMajorPart).$($cv.FileMinorPart).$($cv.FileBuildPart)"
    Add-Check 'wkappbot-core.exe' 'ok' "v$ver"
    Emit 'ok' 'wkappbot-core.exe' "v$ver"
} else {
    Add-Check 'wkappbot-core.exe' 'fail' 'missing -- Eye daemon will not start'
    Emit 'fail' 'wkappbot-core.exe' 'requires private WKAppBot repo: copy from D:\GitHub\WKAppBot\bin'
}

# Check 3: a11y.exe (hardlink/symlink to wkappbot.exe)
if (Test-Path $a11y -PathType Leaf) {
    Add-Check 'a11y.exe' 'ok' 'present'
    Emit 'ok' 'a11y.exe' ''
} else {
    Add-Check 'a11y.exe' 'warn' 'missing hardlink'
    Emit '!' 'a11y.exe' 'creating hardlink...'
    if (Test-Path $wkexe -PathType Leaf) {
        try {
            New-Item -ItemType HardLink -Path $a11y -Target $wkexe -ErrorAction Stop | Out-Null
            Add-Check 'a11y.exe' 'ok' 'self-healed: hardlink created'
            Emit 'ok' 'a11y.exe' 'self-healed'
        } catch {
            Add-Check 'a11y.exe' 'warn' "hardlink failed: $($_.Exception.Message)"
            Emit '!' 'a11y.exe' 'hardlink failed -- copy manually'
        }
    } else {
        Add-Check 'a11y.exe' 'warn' 'fix wkappbot.exe first'
        Emit '!' 'a11y.exe' 'fix wkappbot.exe first'
    }
}
