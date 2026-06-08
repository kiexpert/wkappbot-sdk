# wkdoctor check: tool alias symlink integrity and auto-repair
# Repairs wkdoctor/ag y alias links when they are missing, broken, or pointing
# at the wrong target.

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

function Invoke-MkLink {
    param(
        [Parameter(Mandatory)][string]$LinkPath,
        [Parameter(Mandatory)][string]$TargetPath
    )

    & "$env:SystemRoot\System32\cmd.exe" /c mklink "$LinkPath" "$TargetPath" *> $null
    if ($LASTEXITCODE -ne 0) {
        throw "mklink failed with exit $LASTEXITCODE"
    }

    $check = Test-ToolAliasLinkHealth -LinkPath $LinkPath -TargetPath $TargetPath
    if (-not $check.Healthy) {
        throw "mklink reported success but $LinkPath was not created with target $TargetPath"
    }
}

function Get-WkAppBotBinRoot {
    $cmd = $null
    foreach ($name in @('wkappbot.exe', 'wkappbot')) {
        try {
            $cmd = Get-Command $name -ErrorAction SilentlyContinue | Select-Object -First 1
        } catch {
            $cmd = $null
        }
        if ($cmd -and $cmd.Source) {
            $path = $cmd.Source
            if (-not [System.IO.Path]::IsPathRooted($path)) {
                try { $path = (Get-Command $path -ErrorAction SilentlyContinue).Source } catch {}
            }
            if ($path) {
                $root = Split-Path -Parent $path
                if (Test-Path -LiteralPath $root -PathType Container) {
                    return [System.IO.Path]::GetFullPath($root)
                }
            }
        }
    }

    return $null
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
        if (Test-Path -LiteralPath $linkResolved) {
            Remove-Item -LiteralPath $linkResolved -Force -ErrorAction Stop
        }
        Invoke-MkLink -LinkPath $linkResolved -TargetPath $targetResolved
        Add-Check $Label 'ok' "auto-repaired -> $targetResolved"
        Emit 'ok' $Label "auto-repaired -> $targetResolved"
    } catch {
        Add-Check $Label 'fail' "repair failed: $_"
        Emit 'fail' $Label "repair failed: $_"
    }
}

$doctorDir = $PSScriptRoot
$pathBin = Get-WkAppBotBinRoot
$rootLabel = 'PATH/bin'
if (-not $pathBin) {
    $pathBin = [System.IO.Path]::GetFullPath($doctorDir)
    $rootLabel = 'doctor fallback'
}
$aliasRoots = @(
    @{ Root = $pathBin; Label = $rootLabel }
)

foreach ($root in $aliasRoots) {
    $rootPath = [System.IO.Path]::GetFullPath($root.Root)
    $aliasPairs = @(
        @{ Label = "$($root.Label) wkdoctor.cmd alias"; Link = (Join-Path $rootPath 'wkdoctor.cmd'); Target = (Join-Path $rootPath 'wkwrap.cmd') },
        @{ Label = "$($root.Label) wkdoctor.sh alias";  Link = (Join-Path $rootPath 'wkdoctor.sh');  Target = (Join-Path $rootPath 'wkwrap.sh') },
        @{ Label = "$($root.Label) agy.cmd alias";      Link = (Join-Path $rootPath 'agy.cmd');      Target = (Join-Path $rootPath 'wkwrap.cmd') },
        @{ Label = "$($root.Label) agy.sh alias";       Link = (Join-Path $rootPath 'agy.sh');       Target = (Join-Path $rootPath 'wkwrap.sh') }
    )

    foreach ($pair in $aliasPairs) {
        Ensure-ToolAliasLink -LinkPath $pair.Link -TargetPath $pair.Target -Label $pair.Label
    }
}
