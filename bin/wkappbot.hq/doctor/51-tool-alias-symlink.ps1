# wkdoctor check: direct agy.exe alias integrity and auto-repair
# Doctor only diagnoses and re-checks; the install process owns link creation.

function Normalize-PathForCompare {
    param([string]$Path)
    if ([string]::IsNullOrWhiteSpace($Path)) { return '' }
    try {
        return ([System.IO.Path]::GetFullPath($Path)).TrimEnd('\','/').ToLowerInvariant()
    } catch {
        return ([string]$Path).TrimEnd('\','/').ToLowerInvariant()
    }
}

function Get-LinkTargetText {
    param([Parameter(Mandatory)]$Entry)

    $value = $null
    try { $value = $Entry.LinkTarget } catch {}
    if ([string]::IsNullOrWhiteSpace([string]$value)) {
        try { $value = $Entry.Target } catch {}
    }
    if ($value -is [System.Array]) {
        $value = $value[0]
    }
    return [string]$value
}

function Test-ToolAliasLinkHealth {
    param(
        [Parameter(Mandatory)][string]$LinkPath,
        [Parameter(Mandatory)][string]$TargetPath
    )

    $entry = Get-Item -LiteralPath $LinkPath -Force -ErrorAction SilentlyContinue
    if (-not $entry) {
        return @{ Healthy = $false; Target = '' }
    }

    $isLink = ($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0
    $actualTarget = Get-LinkTargetText $entry
    $actualResolved = Normalize-PathForCompare $actualTarget
    $expectedResolved = Normalize-PathForCompare $TargetPath
    $healthy = $isLink -and (Test-Path -LiteralPath $LinkPath -PathType Leaf) -and ($actualResolved -eq $expectedResolved)

    return @{ Healthy = $healthy; Target = $actualTarget }
}

function Get-WkAppBotBinRoot {
    $pathValue = [Environment]::GetEnvironmentVariable('PATH', 'Process')
    foreach ($dir in ($pathValue -split ';')) {
        if ([string]::IsNullOrWhiteSpace($dir)) {
            continue
        }
        $root = $dir.Trim()
        foreach ($candidate in @('wkappbot.exe', 'wkappbot.cmd', 'wkappbot')) {
            $path = Join-Path $root $candidate
            if (Test-Path -LiteralPath $path -PathType Leaf) {
                return [System.IO.Path]::GetFullPath($root)
            }
        }
    }

    return $null
}

function Get-AgyInstallExePath {
    $target = Join-Path $env:LOCALAPPDATA 'agy\bin\agy.exe'
    if (Test-Path -LiteralPath $target -PathType Leaf) {
        return [System.IO.Path]::GetFullPath($target)
    }
    return $null
}

$script:WrapperInstallRan = $false

function Invoke-WrapperInstall {
    param(
        [Parameter(Mandatory)][string]$Reason
    )

    if ($script:WrapperInstallRan) {
        return
    }

    $installerCandidates = @(
        (Join-Path 'D:\GitHub\wkappbot-kih\tools' 'wkharness-session-init.ps1')
    )
    if ($env:WKHARNESS_REPO_ROOT) {
        $installerCandidates = @(
            (Join-Path $env:WKHARNESS_REPO_ROOT 'tools\wkharness-session-init.ps1')
        ) + $installerCandidates
    }

    $installer = $installerCandidates | Where-Object { $_ -and (Test-Path -LiteralPath $_ -PathType Leaf) } | Select-Object -First 1
    if (-not $installer) {
        throw "wrapper installer not found for repair request: $Reason"
    }

    $psExe = Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe'
    & $psExe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $installer 2>&1 | ForEach-Object {
        Write-Host "  [wrapper-install] $_" -ForegroundColor DarkGray
    }
    if ($LASTEXITCODE -ne 0) {
        throw "wrapper installer failed with exit $LASTEXITCODE"
    }
    $script:WrapperInstallRan = $true
}

function Ensure-ToolAliasLink {
    param(
        [Parameter(Mandatory)][string]$LinkPath,
        [Parameter(Mandatory)][string]$TargetPath,
        [Parameter(Mandatory)][string]$Label
    )

    $linkResolved = [System.IO.Path]::GetFullPath($LinkPath)
    $targetResolved = [System.IO.Path]::GetFullPath($TargetPath)
    $targetExists = Test-Path -LiteralPath $targetResolved -PathType Leaf

    if (-not $targetExists) {
        Add-Check $Label 'fail' "repair source missing: $targetResolved"
        Emit 'fail' $Label "repair source missing: $targetResolved"
        return
    }

    $state = Test-ToolAliasLinkHealth -LinkPath $linkResolved -TargetPath $targetResolved
    if (-not (Get-Item -LiteralPath $linkResolved -Force -ErrorAction SilentlyContinue)) {
        Emit 'warn' $Label "missing -- reconnecting -> $targetResolved"
    } elseif ($state.Healthy) {
        Add-Check $Label 'ok' "symlink -> $($state.Target)"
        Emit 'ok' $Label "symlink -> $($state.Target)"
        return
    } else {
        Emit 'warn' $Label "broken or mismatched -> $($state.Target)"
    }

    try {
        Invoke-WrapperInstall -Reason $Label
        $recheck = Test-ToolAliasLinkHealth -LinkPath $linkResolved -TargetPath $targetResolved
        if ($recheck.Healthy) {
            Add-Check $Label 'ok' "auto-repaired via wrapper install -> $targetResolved"
            Emit 'ok' $Label "auto-repaired via wrapper install -> $targetResolved"
        } else {
            throw "wrapper install completed but $linkResolved still targets '$($recheck.Target)'"
        }
    } catch {
        Add-Check $Label 'fail' "repair failed via wrapper install: $_"
        Emit 'fail' $Label "repair failed via wrapper install: $_"
    }
}

$doctorDir = $PSScriptRoot
$doctorBin = [System.IO.Path]::GetFullPath((Split-Path -Parent (Split-Path -Parent $doctorDir)))
$pathBin = Get-WkAppBotBinRoot
$rootLabel = 'PATH appbot bin'
if (-not $pathBin) {
    $pathBin = $doctorBin
    $rootLabel = 'doctor folder'
}
$agyInstallExe = Get-AgyInstallExePath
$aliasRoots = @(
    @{ Root = $pathBin; Label = $rootLabel }
)

foreach ($root in $aliasRoots) {
    $rootPath = [System.IO.Path]::GetFullPath($root.Root)
    $aliasPairs = @(
        @{ Label = "$($root.Label) agy.exe alias";      Link = (Join-Path $rootPath 'agy.exe');      Target = $agyInstallExe }
    )

    foreach ($pair in $aliasPairs) {
        Ensure-ToolAliasLink -LinkPath $pair.Link -TargetPath $pair.Target -Label $pair.Label
    }
}
