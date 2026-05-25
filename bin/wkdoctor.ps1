# wkdoctor -- wkappbot-sdk health check (flutter-doctor style)
# Usage: wkdoctor [-Json] [-Verbose]
param(
    [switch]$Json,
    [switch]$Verbose
)

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$binDir     = $scriptRoot
$repoRoot   = Split-Path -Parent $scriptRoot

$pass  = 0
$fail  = 0
$warn  = 0
$items = [System.Collections.Generic.List[PSCustomObject]]::new()

function Add-Check {
    param([string]$Name, [string]$Status, [string]$Detail)
    $items.Add([PSCustomObject]@{ Name = $Name; Status = $Status; Detail = $Detail })
    if ($Status -eq 'ok')   { $script:pass++ }
    elseif ($Status -eq 'fail') { $script:fail++ }
    else                    { $script:warn++ }
}

function Emit {
    param([string]$Status, [string]$Name, [string]$Detail)
    if ($Json) { return }
    $sym = if ($Status -eq 'ok') { '[+]' } elseif ($Status -eq 'fail') { '[x]' } else { '[!]' }
    $color = if ($Status -eq 'ok') { 'Green' } elseif ($Status -eq 'fail') { 'Red' } else { 'Yellow' }
    $line = "$sym $Name"
    if ($Detail) { $line += " -- $Detail" }
    Write-Host $line -ForegroundColor $color
}

# 1. wkappbot.exe
$wkexe = Join-Path $binDir 'wkappbot.exe'
if (Test-Path $wkexe -PathType Leaf) {
    $ver = & $wkexe --version 2>&1 | Select-Object -First 1
    Add-Check 'wkappbot.exe' 'ok' "$ver"
    Emit 'ok' 'wkappbot.exe' $ver
} else {
    Add-Check 'wkappbot.exe' 'fail' "not found at $wkexe"
    Emit 'fail' 'wkappbot.exe' "not found -- run setup.ps1 to build"
}

# 2. wkappbot-core.exe
$wkcore = Join-Path $binDir 'wkappbot-core.exe'
if (Test-Path $wkcore -PathType Leaf) {
    Add-Check 'wkappbot-core.exe' 'ok' ''
    Emit 'ok' 'wkappbot-core.exe' ''
} else {
    Add-Check 'wkappbot-core.exe' 'fail' "not found at $wkcore"
    Emit 'fail' 'wkappbot-core.exe' "missing -- Eye will not start"
}

# 3. a11y.exe
$a11y = Join-Path $binDir 'a11y.exe'
if (Test-Path $a11y -PathType Leaf) {
    Add-Check 'a11y.exe' 'ok' ''
    Emit 'ok' 'a11y.exe' ''
} else {
    Add-Check 'a11y.exe' 'warn' "not found (symlink to wkappbot.exe expected)"
    Emit '!' 'a11y.exe' "missing -- a11y shortcut unavailable"
}

# 4. Eye tick
if (Test-Path $wkexe -PathType Leaf) {
    $eyeOut = & $wkexe eye tick 2>&1 | Select-Object -First 3
    $eyeStr = $eyeOut -join ' '
    if ($eyeStr -match 'ctx=') {
        Add-Check 'Eye (daemon)' 'ok' ($eyeOut | Select-Object -First 1)
        Emit 'ok' 'Eye (daemon)' ($eyeOut | Select-Object -First 1)
    } elseif ($eyeStr -match 'not running|offline|timeout') {
        Add-Check 'Eye (daemon)' 'warn' 'not responding -- run: wkappbot eye'
        Emit '!' 'Eye (daemon)' 'not responding'
    } else {
        Add-Check 'Eye (daemon)' 'warn' ($eyeStr.Substring(0, [Math]::Min(80,$eyeStr.Length)))
        Emit '!' 'Eye (daemon)' 'unexpected response'
    }
} else {
    Add-Check 'Eye (daemon)' 'fail' 'skipped (wkappbot.exe missing)'
    Emit 'fail' 'Eye (daemon)' 'skipped'
}

# 5. .NET SDK
$dotnet = 'C:\Program Files\dotnet\dotnet.exe'
if (Test-Path $dotnet -PathType Leaf) {
    $dnVer = & $dotnet --version 2>&1 | Select-Object -First 1
    if ($dnVer -match '^8\.') {
        Add-Check '.NET SDK' 'ok' $dnVer
        Emit 'ok' '.NET SDK' $dnVer
    } else {
        Add-Check '.NET SDK' 'warn' "found $dnVer (expected 8.x)"
        Emit '!' '.NET SDK' "found $dnVer (expected 8.x)"
    }
} else {
    Add-Check '.NET SDK' 'fail' 'dotnet.exe not found at C:\Program Files\dotnet\'
    Emit 'fail' '.NET SDK' 'install .NET 8 SDK'
}

# 6. Skills catalog
if (Test-Path $wkexe -PathType Leaf) {
    $skillOut = & $wkexe skill list 2>&1
    $count = ($skillOut | Measure-Object -Line).Lines
    if ($count -gt 5) {
        Add-Check 'Skills catalog' 'ok' "$count entries"
        Emit 'ok' 'Skills catalog' "$count entries"
    } else {
        Add-Check 'Skills catalog' 'warn' "only $count entries -- run: wkappbot skill install"
        Emit '!' 'Skills catalog' "only $count entries"
    }
} else {
    Add-Check 'Skills catalog' 'fail' 'skipped'
    Emit 'fail' 'Skills catalog' 'skipped'
}

# 7. Nightly-guard in ~/.claude/settings.json
$globalSettings = Join-Path $env:USERPROFILE '.claude\settings.json'
$guardOk = $false
if (Test-Path $globalSettings -PathType Leaf) {
    $raw = Get-Content $globalSettings -Encoding UTF8 -Raw
    if ($raw -match 'nightly-schedule-guard') {
        $guardOk = $true
    }
}
if ($guardOk) {
    Add-Check 'Nightly guard' 'ok' '~/.claude/settings.json'
    Emit 'ok' 'Nightly guard' 'registered in ~/.claude/settings.json'
} else {
    Add-Check 'Nightly guard' 'warn' 'hook not found in ~/.claude/settings.json'
    Emit '!' 'Nightly guard' 'add nightly-schedule-guard.ps1 to ~/.claude/settings.json PreToolUse'
}

# 8. bin/ in PATH
$pathDirs = $env:PATH -split ';'
$binAbs = (Resolve-Path $binDir -ErrorAction SilentlyContinue)
$inPath = $pathDirs | Where-Object { $_ -and ((Resolve-Path $_ -ErrorAction SilentlyContinue).Path -eq $binAbs.Path) }
if ($inPath) {
    Add-Check 'PATH (bin/)' 'ok' $binDir
    Emit 'ok' 'PATH (bin/)' $binDir
} else {
    Add-Check 'PATH (bin/)' 'warn' "$binDir not in PATH"
    Emit '!' 'PATH (bin/)' "add $binDir to PATH for wkappbot/wkdoctor commands"
}

# 9. .wkappbot/config.json
$configJson = Join-Path $repoRoot '.wkappbot\config.json'
if (Test-Path $configJson -PathType Leaf) {
    Add-Check 'config.json' 'ok' $configJson
    Emit 'ok' 'config.json' $configJson
} else {
    Add-Check 'config.json' 'warn' "not found at $configJson -- run setup.ps1"
    Emit '!' 'config.json' "run setup.ps1 to create"
}

# 10. wkappbot.hq/
$hq = Join-Path $binDir 'wkappbot.hq'
if (Test-Path $hq -PathType Container) {
    Add-Check 'wkappbot.hq/' 'ok' $hq
    Emit 'ok' 'wkappbot.hq/' $hq
} else {
    Add-Check 'wkappbot.hq/' 'warn' "not found at $hq -- Eye creates it on first run"
    Emit '!' 'wkappbot.hq/' "will be created on first Eye launch"
}

# Summary
if (-not $Json) {
    Write-Host ''
    $summary = "wkdoctor: $pass ok, $warn warn, $fail fail"
    $color = if ($fail -gt 0) { 'Red' } elseif ($warn -gt 0) { 'Yellow' } else { 'Green' }
    Write-Host $summary -ForegroundColor $color
}

if ($Json) {
    [PSCustomObject]@{
        pass  = $pass
        warn  = $warn
        fail  = $fail
        items = $items
    } | ConvertTo-Json -Depth 5
}

if ($fail -gt 0) { exit 1 } else { exit 0 }
