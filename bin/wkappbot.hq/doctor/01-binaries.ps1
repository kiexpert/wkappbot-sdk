# wkdoctor check: binaries -- wkappbot.exe, wkappbot-core.exe, a11y.exe
# Heal: auto-build launcher if missing; create a11y.exe hardlink

$wkexe  = Join-Path $binDir 'wkappbot.exe'
$wkcore = Join-Path $binDir 'wkappbot-core.exe'
$a11y   = Join-Path $binDir 'a11y.exe'

# Check 1: wkappbot.exe
if (Test-Path $wkexe -PathType Leaf) {
    $vi  = (Get-Item $wkexe).VersionInfo
    $ver = "$($vi.FileMajorPart).$($vi.FileMinorPart).$($vi.FileBuildPart)"
    Add-Check 'wkappbot.exe' 'ok' "v$ver"
    Emit 'ok' 'wkappbot.exe' "v$ver"
} else {
    Add-Check 'wkappbot.exe' 'fail' 'not found'
    Emit 'fail' 'wkappbot.exe' 'not found -- attempting auto-build...'
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

# Check 2: wkappbot-core.exe
if (Test-Path $wkcore -PathType Leaf) {
    $cv  = (Get-Item $wkcore).VersionInfo
    $ver = "$($cv.FileMajorPart).$($cv.FileMinorPart).$($cv.FileBuildPart)"
    Add-Check 'wkappbot-core.exe' 'ok' "v$ver"
    Emit 'ok' 'wkappbot-core.exe' "v$ver"
} else {
    Add-Check 'wkappbot-core.exe' 'fail' 'missing -- Eye will not start'
    Emit 'fail' 'wkappbot-core.exe' 'copy from D:\GitHub\WKAppBot\bin'
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
